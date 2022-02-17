Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports MySql.Data.MySqlClient

Public Class EditionBudgetaire

    Dim dtEtat = New DataTable()
    Dim DrX As DataRow
    Dim Fichier As String = ""
    Dim CodeDirect As Decimal = -1
    Dim CodeService As Decimal = -1
    Dim CodeRespo As Decimal = -1
    Dim LesResponsables As String()
    Dim LesServices As String()
    Dim LesDirections As String()
    Private Sub EditionBudgetaire_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        Fichier = ""
        RemplirListEtat()
        ChargerRespo()

        'Charger Service
        query = "select CodeService, NomService from T_Service where CodeProjet='" & ProjetEnCours & "' AND CodeService IN(SELECT DISTINCT f.CodeService FROM t_grh_travailler t, t_fonction f, t_operateurpartition p WHERE p.EMP_ID=t.EMP_ID AND t.CodeService=f.RefFonction) ORDER BY CodeService"
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        ReDim LesServices(dt1.Rows.Count)
        Dim i = 0
        For Each rw1 As DataRow In dt1.Rows
            LesServices(i) = rw1("CodeService")
            i += 1
            Combserv.Properties.Items.Add(MettreApost(rw1("NomService").ToString))
        Next

        'Charger Direction
        query = "select RefDecoupAdmin, LibelleDivision from t_divisionadministrative where CodeProjet='" & ProjetEnCours & "' AND RefDecoupAdmin IN(SELECT DISTINCT f.RefDecoupAdmin FROM t_grh_travailler t, t_fonction f, t_operateurpartition p WHERE p.EMP_ID=t.EMP_ID AND t.CodeService=f.RefFonction) ORDER BY LibelleDivision"
        Dim dt2 As DataTable = ExcecuteSelectQuery(query)
        ReDim LesDirections(dt2.Rows.Count)
        i = 0
        For Each rw2 As DataRow In dt2.Rows
            LesDirections(i) = rw2("RefDecoupAdmin")
            i += 1
            Combdirect.Properties.Items.Add(MettreApost(rw2(1).ToString))
        Next

    End Sub

    Private Sub ChargerRespo()

        'rechercher les informations de l'opérateur connecté
        CombResp.Properties.Items.Clear()
        query = "select EMP_ID,CONCAT(EMP_NOM,' ',EMP_PRENOMS) As NomPrenoms from t_grh_employe WHERE EMP_ID IN( SELECT DISTINCT EMP_ID FROM t_operateurpartition WHERE TitreOpPart='Responsable' and PROJ_ID='" & ProjetEnCours & "') ORDER BY NomPrenoms"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        ReDim LesResponsables(dt.Rows.Count)
        Dim i = 0
        For Each rw In dt.Rows
            LesResponsables(i) = rw("EMP_ID").ToString
            i += 1
            CombResp.Properties.Items.Add(MettreApost(rw("NomPrenoms").ToString.Trim()))
        Next
    End Sub

    Private Sub RemplirListEtat()

        dtEtat.Columns.Clear()
        dtEtat.Columns.Add("Code", Type.GetType("System.String"))
        dtEtat.Columns.Add("Ref", Type.GetType("System.String"))
        dtEtat.Columns.Add("Etat", Type.GetType("System.String"))
        dtEtat.Columns.Add("Chemin", Type.GetType("System.String"))
        dtEtat.Rows.Clear()
        Dim cptr As Decimal = 0

        query = "select RefEtat, LibelleEtat, NomEtat from T_EditionBudget order by LibelleEtat"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            If rw("RefEtat") = "C05" Or rw("RefEtat") = "C06" Or rw("RefEtat") = "C07" Or rw("RefEtat") = "C08" Then
                cptr += 1
                Dim drS = dtEtat.NewRow()
                drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
                drS(1) = rw(0).ToString
                drS(2) = MettreApost(rw(1).ToString).Replace("Projet", ProjetEnCours)
                drS(3) = rw(2).ToString
                dtEtat.Rows.Add(drS)
            End If
        Next

        GridEdition.DataSource = dtEtat
        ViewEdition.Columns(0).Visible = False
        ViewEdition.Columns(1).Visible = False
        ViewEdition.Columns(2).Width = largeur - 18
        ViewEdition.Columns(3).Visible = False
        ViewEdition.Appearance.Row.Font = New Font("Times New Roman", 14, FontStyle.Regular)
        ColorRowGrid(ViewEdition, "[Code]='x'", Color.LightGray, "Times New Roman", 14, FontStyle.Regular, Color.Black)

    End Sub

    Private Sub GridEdition_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEdition.DoubleClick

        Me.Cursor = Cursors.WaitCursor
        If (ViewEdition.RowCount > 0) Then

            DrX = ViewEdition.GetDataRow(ViewEdition.FocusedRowHandle)
            Fichier = DrX(3).ToString
            ColorRowGrid(ViewEdition, "[Code]='x'", Color.LightGray, "Times New Roman", 14, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewEdition, "[Ref]='" & DrX(1).ToString & "'", Color.Navy, "Times New Roman", 14, FontStyle.Bold, Color.White, True)
            PnlC04.Visible = False
            If DrX(1) = "C07" Then
                CmbNiveauEdit.Enabled = False
            Else
                If DrX(1) = "C08" Then
                    CmbNiveauEdit.Properties.Items.Clear()
                    CmbNiveauEdit.Properties.Items.Add("Activité")
                Else
                    CmbNiveauEdit.Properties.Items.Clear()
                    CmbNiveauEdit.Properties.Items.AddRange({"Composante", "Sous composante", "Activité"})
                End If
                CmbNiveauEdit.Enabled = True
            End If

            If (DrX(1).ToString = "C03") Then

                EffacerTexBox4(PnlPartitionPlan)
                EffacerTexBox4(PnlTypePlan)

                query = "select * from  t_besoinpartition as B, T_Partition as P where B.CodePartition=P.CodePartition and P.CodeProjet='" & ProjetEnCours & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                If dt.Rows.Count = 0 Then
                    SuccesMsg("Aucun découpage effectué.")
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                PnlTypePlan.Visible = True
                PnlPartitionPlan.Visible = True
                BtActualiserPlan.Visible = True
                BtImprimer.Enabled = False

            ElseIf (DrX(1).ToString = "C02") Then

                query = "select count(*) from T_Partition_Budget as B, T_Partition as P, T_Convention as C where B.CodePartition=P.CodePartition and B.CodeConvention=C.CodeConvention and P.CodeProjet='" & ProjetEnCours & "'"
                Dim nbre = Val(ExecuteScallar(query))
                If nbre = 0 Then
                    SuccesMsg("Aucune allocation effectuée.")
                    Me.Cursor = Cursors.Default
                    BtActualiserPlan.PerformClick()
                    Exit Sub
                End If

            ElseIf (DrX(1).ToString = "C04" Or DrX(1).ToString = "C05" Or DrX(1).ToString = "C06" Or DrX(1).ToString = "C07" Or DrX(1).ToString = "C08") Then

                LabelControl4.Visible = False
                Combserv.Visible = False
                LabelControl3.Visible = True
                CombResp.Visible = True
                Combdirect.Visible = False
                LabelControl5.Visible = False
                EffacerTexBox4(PnlC04)

                'date
                dtd.Text = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
                dtf.Text = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")
                'query = "select datedebut, datefin from T_COMP_EXERCICE where Etat<>'2' and encours='1'"
                'Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                'For Each rw3 As DataRow In dt3.Rows

                'Next

                If (DrX(1).ToString = "C04") Then
                    PnlC04.Visible = False
                ElseIf (DrX(1).ToString = "C05") Then
                    LabelControl4.Visible = True
                    Combserv.Visible = True
                    LabelControl3.Visible = False
                    CombResp.Visible = False
                    Combdirect.Visible = False
                    LabelControl5.Visible = False
                    query = "select * from t_operateurpartition as O, T_grh_travailler as T, T_Fonction as F, t_partition P where O.EMP_ID=T.EMP_ID and F.RefFonction=T.CodeService AND P.CodePartition=O.CodePartition and T.PosteActu='O' and P.CodeProjet='" & ProjetEnCours & "'"
                    Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                    If dt2.Rows.Count = 0 Then
                        SuccesMsg("Aucun enregistrement trouvé.")
                        Me.Cursor = Cursors.Default
                        BtActualiserPlan.PerformClick()
                        Exit Sub
                    End If

                ElseIf (DrX(1).ToString = "C07" Or DrX(1).ToString = "C08") Then
                    LabelControl4.Visible = False
                    Combserv.Visible = False
                    LabelControl3.Visible = False
                    CombResp.Visible = False
                    Combdirect.Visible = True
                    LabelControl5.Visible = True
                End If

                PnlC04.Visible = True
                AfficherEtat(Fichier, DrX(1).ToString)
            Else
                PnlTypePlan.Visible = False
                PnlPartitionPlan.Visible = False
                BtActualiserPlan.Visible = False
                BtImprimer.Enabled = True
                AfficherEtat(Fichier, DrX(1).ToString)
            End If

            RemplirBailleur()
            GridEdition.Enabled = False


        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub RemplirBailleur()

        Dim MontTot As Decimal = 0
        CmbBailleur.Properties.Items.Clear()
        query = "select InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' order by InitialeBailleur"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbBailleur.Properties.Items.Add(rw(0).ToString)
        Next

    End Sub

    Private Sub AfficherEtat(ByVal CheminFichier As String, ByVal Ref As String)

        DebutChargement(True, "Le traitement de votre demande est en cours...")
        Dim reportConvention As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table
        Dim Chemin As String = lineEtat & "\EditionBudgetaire\" & CheminFichier

        Dim DatSet = New DataSet
        reportConvention.Load(Chemin)

        With crConnectionInfo
            .ServerName = ODBCNAME
            .DatabaseName = DB
            .UserID = USERNAME
            .Password = PWD
        End With

        CrTables = reportConvention.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        reportConvention.SetDataSource(DatSet)
        reportConvention.SetParameterValue("CodeProjet", ProjetEnCours)

        If (Ref <> "C04" And Ref <> "C05" And Ref <> "C06" And Ref <> "C07" And Ref <> "C08") Then
            reportConvention.SetParameterValue("Bailleur", IIf(CmbBailleur.Text <> "", CmbBailleur.Text, "").ToString)
        ElseIf Ref <> "C07" And Ref <> "C08" Then
            reportConvention.SetParameterValue("NiveauEdit", CmbNiveauEdit.Text)
        End If

        If (Ref = "C02") Then

            RemplirAllocAttrib()
            Dim Repart As String = "Composante"
            query = "select UniteRepartitionBudget from T_ParamTechProjet where CodeProjet='" & ProjetEnCours & "'"
            Repart = ExecuteScallar(query)
            reportConvention.SetParameterValue("TypeRepart", Repart)
            reportConvention.SetParameterValue("Bailleur", IIf(CmbBailleur.Text <> "", CmbBailleur.Text, "").ToString, "FinancementEtPrevisionsRecap.rpt")
        End If

        If (Ref = "C01") Then

            Dim MontTot As Decimal = 0
            query = "select C.MontantConvention from T_Bailleur as B, T_Convention as C where B.CodeBailleur=C.CodeBailleur and B.CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                MontTot += CDec(rw(0))
            Next
            reportConvention.SetParameterValue("MontantTotal", MontTot)

        End If

        If (Ref = "C04" Or Ref = "C05" Or Ref = "C06") Then

            reportConvention.SetParameterValue("Date1", dtd.Text)
            reportConvention.SetParameterValue("Date2", dtf.Text)

            Dim MontTot As Decimal = 0
            query = "select C.MontantConvention from T_Bailleur as B, T_Convention as C where B.CodeBailleur=C.CodeBailleur and B.CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                MontTot += CDec(rw(0))
            Next
            reportConvention.SetParameterValue("MontantTotal", MontTot)

        End If

        If (Ref = "C07") Or (Ref = "C08") Then
            'Chargement de la table tamponbudgetairedirection
            Dim parametre As String = "Cumulé"
            If Ref = "C08" Then
                parametre = "Détaillé"
            End If

            Dim MontantTotal As Decimal = 0
            query = "DELETE FROM tamponbudgetairedirection WHERE CodeProjet='" & ProjetEnCours & "' AND CodeUtils='" & SessionID & "'" 'CodeUtils='" & SessionID & "'"
            ExecuteNonQuery(query)

            If Ref = "C07" Then 'Charger les données par Direction et Employé
                'Get le montant total des activites
                If CodeDirect = -1 Then
                    query = "SELECT SUM(QteNature*PUNature) As Total FROM t_besoinpartition WHERE CodePartition IN(SELECT O.CodePartition FROM t_operateurpartition O, t_partition P, t_grh_travailler T, t_fonction F, t_grh_employe E, t_service S, t_divisionadministrative D WHERE PosteActu='O' AND S.CodeService=F.CodeService AND S.RefDecoupAdmin=D.RefDecoupAdmin AND P.CodePartition=O.CodePartition AND O.TitreOpPart='Responsable' AND F.RefFonction=T.CodeService AND O.EMP_ID=E.EMP_ID AND P.DateDebutPartition>='" & dateconvert(dtd.Text) & "' AND P.DateFinPartition<='" & dateconvert(dtf.Text) & "')"
                Else
                    query = "SELECT SUM(QteNature*PUNature) As Total FROM t_besoinpartition WHERE CodePartition IN(SELECT O.CodePartition FROM t_operateurpartition O, t_partition P, t_grh_travailler T, t_fonction F, t_grh_employe E, t_service S, t_divisionadministrative D WHERE PosteActu='O' AND S.CodeService=F.CodeService AND S.RefDecoupAdmin=D.RefDecoupAdmin AND D.RefDecoupAdmin='" & CodeDirect & "' AND P.CodePartition=O.CodePartition AND O.TitreOpPart='Responsable' AND F.RefFonction=T.CodeService AND O.EMP_ID=E.EMP_ID AND P.DateDebutPartition>='" & dateconvert(dtd.Text) & "' AND P.DateFinPartition<='" & dateconvert(dtf.Text) & "')"
                End If
                MontantTotal = Val(ExecuteScallar(query))

                If CodeDirect = -1 Then
                    query = "SELECT * FROM t_divisionadministrative WHERE CodeProjet='" & ProjetEnCours & "' ORDER BY RefDecoupSup ASC"
                Else
                    query = "SELECT * FROM t_divisionadministrative WHERE CodeProjet='" & ProjetEnCours & "' AND RefDecoupAdmin='" & CodeDirect & "' ORDER BY RefDecoupSup ASC"
                End If
                Dim dtDivision As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dtDivision.Rows
                    Dim MontantDivision As Decimal = 0
                    query = "SELECT SUM(QteNature*PUNature) As Total  FROM t_besoinpartition "
                    query &= "WHERE CodePartition IN "
                    query &= "(SELECT DISTINCT(O.CodePartition) "
                    query &= "FROM t_operateurpartition O, t_partition P, t_grh_travailler T, t_fonction F, t_service S "
                    query &= " WHERE PosteActu='O' AND P.CodePartition=O.CodePartition AND O.TitreOpPart='Responsable' AND T.EMP_ID=O.EMP_ID AND "
                    query &= "F.RefFonction=T.CodeService AND S.CodeService=F.CodeService AND S.RefDecoupAdmin='" & rw("RefDecoupAdmin") & "' "
                    query &= "AND P.DateDebutPartition>='" & dateconvert(dtd.Text) & "' AND P.DateFinPartition<='" & dateconvert(dtf.Text) & "')"
                    MontantDivision = Val(ExecuteScallar(query))
                    If MontantDivision > 0 Then
                        Dim CodeSup As Decimal = Val(rw("RefDecoupSup"))
                        Dim Type As String
                        If CodeSup > 0 Then
                            'query = "SELECT RefDecoupSup FROM t_divisionadministrative WHERE RefDecoupAdmin='" & CodeSup & "'"
                            'If Val(ExecuteScallar(query)) = 0 Then
                            '    Type = "Direction"
                            'Else
                            'End If
                            Type = "SDirection"
                        Else
                            Type = "Direction"
                        End If
                        Dim PourcentageDivision As Decimal = Math.Round(((MontantDivision * 100) / MontantTotal), 2)
                        query = "INSERT INTO tamponbudgetairedirection VALUES(NULL,'" & EnleverApost(rw("LibelleDivision")) & "','" & Type & "','" & MontantDivision & "','" & PourcentageDivision.ToString().Replace(",", ".") & "','" & dateconvert(dtd.Text) & "','" & dateconvert(dtf.Text) & "','" & ProjetEnCours & "','" & SessionID & "')"
                        ExecuteNonQuery(query)
                    End If

                    query = "SELECT *  FROM t_service S "
                    query &= "WHERE CodeService IN "
                    query &= "(Select DISTINCT(S.CodeService) "
                    query &= "FROM t_service S, t_operateurpartition O, t_partition P, t_grh_travailler T, t_fonction F "
                    query &= "WHERE PosteActu='O' AND P.CodePartition=O.CodePartition AND O.TitreOpPart='Responsable' AND "
                    query &= "F.RefFonction=T.CodeService AND S.CodeService=F.CodeService AND P.DateDebutPartition>='" & dateconvert(dtd.Text) & "' AND "
                    query &= "P.DateFinPartition<='" & dateconvert(dtf.Text) & "' AND S.RefDecoupAdmin='" & rw("RefDecoupAdmin") & "' AND S.CodeProjet='" & ProjetEnCours & "') ORDER BY CodeServiceSup ASC"
                    Dim dtService As DataTable = ExcecuteSelectQuery(query)
                    For Each rwServcie As DataRow In dtService.Rows
                        Dim MontantService As Decimal = 0
                        query = "SELECT SUM(QteNature*PUNature) As Total FROM t_besoinpartition "
                        query &= "WHERE CodePartition IN "
                        query &= "(SELECT DISTINCT(O.CodePartition) "
                        query &= "FROM t_operateurpartition O, t_partition P, t_grh_travailler T, t_fonction F, t_service S "
                        query &= "WHERE PosteActu='O' AND P.CodePartition=O.CodePartition AND T.EMP_ID=O.EMP_ID AND O.TitreOpPart='Responsable' AND "
                        query &= "F.RefFonction=T.CodeService AND S.CodeService=F.CodeService AND P.DateDebutPartition>='" & dateconvert(dtd.Text) & "' AND "
                        query &= "P.DateFinPartition<='" & dateconvert(dtf.Text) & "' AND S.CodeService='" & rwServcie("CodeService") & "' AND S.CodeProjet='" & ProjetEnCours & "')"
                        MontantService = Val(ExecuteScallar(query))
                        If MontantService > 0 Then
                            query = "INSERT INTO tamponbudgetairedirection VALUES(NULL,'" & EnleverApost(rwServcie("NomService")) & "','Service','" & MontantService & "','" & Math.Round(((MontantService * 100) / MontantTotal), 2).ToString().Replace(",", ".") & "','" & dateconvert(dtd.Text) & "','" & dateconvert(dtf.Text) & "','" & ProjetEnCours & "','" & SessionID & "')"
                            ExecuteNonQuery(query)
                                End If

                        query = "SELECT * FROM t_grh_employe WHERE EMP_ID IN "
                        query &= "(SELECT DISTINCT(O.EMP_ID) "
                        query &= "FROM t_operateurpartition O, t_partition P, t_grh_travailler T, t_fonction F "
                        query &= "WHERE PosteActu='O' AND P.CodePartition=O.CodePartition AND T.EMP_ID=O.EMP_ID AND "
                        query &= "F.RefFonction=T.CodeService AND O.TitreOpPart='Responsable' AND "
                        query &= "P.DateDebutPartition>='" & dateconvert(dtd.Text) & "' AND P.DateFinPartition<='" & dateconvert(dtf.Text) & "' AND "
                        query &= "F.CodeService='" & rwServcie("CodeService") & "')"
                        Dim dtEmploye As DataTable = ExcecuteSelectQuery(query)
                        For Each rwEmploye As DataRow In dtEmploye.Rows
                            Dim MontantEmploye As Decimal = 0
                            query = "SELECT SUM(QteNature*PUNature) As Total FROM t_besoinpartition WHERE CodePartition IN "
                            query &= "(SELECT DISTINCT(O.CodePartition) "
                            query &= "FROM t_operateurpartition O, t_partition P, t_grh_travailler T, t_fonction F "
                            query &= "WHERE PosteActu='O' AND P.CodePartition=O.CodePartition AND F.RefFonction=T.CodeService AND "
                            query &= "O.EMP_ID='" & rwEmploye("EMP_ID") & "' AND P.DateDebutPartition>='" & dateconvert(dtd.Text) & "' AND "
                            query &= "P.DateFinPartition<='" & dateconvert(dtf.Text) & "' AND O.TitreOpPart='Responsable' AND F.CodeService='" & rwServcie("CodeService") & "')"
                            Dim dtAllocation As DataTable = ExcecuteSelectQuery(query)
                            Dim rwAllocation As DataRow = dtAllocation.Rows(0)
                            Dim NomEtPrenoms As String = rwEmploye("EMP_NOM") & " " & rwEmploye("EMP_PRENOMS")
                            If Not IsDBNull(rwAllocation("Total")) Then
                                MontantEmploye = CDec(rwAllocation("Total"))
                                query = "INSERT INTO tamponbudgetairedirection VALUES(NULL,'" & EnleverApost(NomEtPrenoms.Trim()) & "','Employé','" & MontantEmploye & "','" & Math.Round(((MontantEmploye * 100) / MontantTotal), 2).ToString().Replace(",", ".") & "','" & dateconvert(dtd.Text) & "','" & dateconvert(dtf.Text) & "','" & ProjetEnCours & "','" & SessionID & "')"
                                ExecuteNonQuery(query)
                                            End If
                        Next
                    Next
                Next

            ElseIf (Ref = "C08") Then
                'Get le montant total des activites
                If CodeDirect = -1 Then
                    query = "SELECT SUM(QteNature*PUNature) As Total FROM t_besoinpartition WHERE CodePartition IN(SELECT O.CodePartition FROM t_operateurpartition O, t_partition P, t_grh_travailler T, t_fonction F, t_grh_employe E, t_service S, t_divisionadministrative D WHERE PosteActu='O' AND S.CodeService=F.CodeService AND S.RefDecoupAdmin=D.RefDecoupAdmin AND P.CodePartition=O.CodePartition AND F.RefFonction=T.CodeService AND O.EMP_ID=E.EMP_ID AND O.TitreOpPart='Responsable' AND P.DateDebutPartition>='" & dateconvert(dtd.Text) & "' AND P.DateFinPartition<='" & dateconvert(dtf.Text) & "')"
                Else
                    query = "SELECT SUM(QteNature*PUNature) As Total FROM t_besoinpartition WHERE CodePartition IN(SELECT O.CodePartition FROM t_operateurpartition O, t_partition P, t_grh_travailler T, t_fonction F, t_grh_employe E, t_service S, t_divisionadministrative D WHERE PosteActu='O' AND S.CodeService=F.CodeService AND S.RefDecoupAdmin=D.RefDecoupAdmin AND D.RefDecoupAdmin='" & CodeDirect & "' AND P.CodePartition=O.CodePartition AND O.TitreOpPart='Responsable' AND F.RefFonction=T.CodeService AND O.EMP_ID=E.EMP_ID AND P.DateDebutPartition>='" & dateconvert(dtd.Text) & "' AND P.DateFinPartition<='" & dateconvert(dtf.Text) & "')"
                End If
                MontantTotal = Val(ExecuteScallar(query))

                If CodeDirect = -1 Then
                    query = "SELECT * FROM t_divisionadministrative WHERE CodeProjet='" & ProjetEnCours & "' ORDER BY RefDecoupSup ASC"
                Else
                    query = "SELECT * FROM t_divisionadministrative WHERE CodeProjet='" & ProjetEnCours & "' AND RefDecoupAdmin='" & CodeDirect & "' ORDER BY RefDecoupSup ASC"
                End If
                Dim dtDivision As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dtDivision.Rows
                    Dim MontantDivision As Decimal = 0
                    query = "SELECT SUM(QteNature*PUNature) As Total FROM t_besoinpartition WHERE CodePartition IN "
                    query &= "(SELECT DISTINCT(O.CodePartition) "
                    query &= "FROM t_operateurpartition O, t_partition P, t_grh_travailler T, t_fonction F, t_service S "
                    query &= "WHERE PosteActu='O' AND P.CodePartition=O.CodePartition AND T.EMP_ID=O.EMP_ID AND "
                    query &= "F.RefFonction=T.CodeService AND S.CodeService=F.CodeService AND S.RefDecoupAdmin='" & rw("RefDecoupAdmin") & "' "
                    query &= "AND P.DateDebutPartition>='" & dateconvert(dtd.Text) & "' AND O.TitreOpPart='Responsable' AND  P.DateFinPartition<='" & dateconvert(dtf.Text) & "')"
                    MontantDivision = Val(ExecuteScallar(query))
                    If MontantDivision > 0 Then
                        Dim CodeSup As Decimal = Val(rw("RefDecoupSup"))
                        Dim Type As String
                        If CodeSup > 0 Then
                            'query = "SELECT RefDecoupSup FROM t_divisionadministrative WHERE RefDecoupAdmin='" & CodeSup & "'"
                            'If Val(ExecuteScallar(query)) = 0 Then
                            '    Type = "Direction"
                            'Else
                            'End If
                            Type = "SDirection"
                        Else
                            Type = "Direction"
                        End If
                        Dim PourcentageDivision As Decimal = Math.Round(((MontantDivision * 100) / MontantTotal), 2)
                        query = "INSERT INTO tamponbudgetairedirection VALUES(NULL,'" & EnleverApost(rw("LibelleDivision")) & "','" & Type & "','" & MontantDivision & "','" & PourcentageDivision.ToString().Replace(",", ".") & "','" & dateconvert(dtd.Text) & "','" & dateconvert(dtf.Text) & "','" & ProjetEnCours & "','" & SessionID & "')"
                        ExecuteNonQuery(query)
                    End If

                    query = "SELECT * FROM t_service S WHERE CodeService IN "
                    query &= "(SELECT DISTINCT(S.CodeService) "
                    query &= "FROM t_service S, t_operateurpartition O, t_partition P, t_grh_travailler T, t_fonction F "
                    query &= "WHERE PosteActu='O' AND P.CodePartition=O.CodePartition AND O.TitreOpPart='Responsable' AND "
                    query &= "F.RefFonction=T.CodeService AND S.CodeService=F.CodeService AND P.DateDebutPartition>='" & dateconvert(dtd.Text) & "' AND "
                    query &= "P.DateFinPartition<='" & dateconvert(dtf.Text) & "' AND S.RefDecoupAdmin='" & rw("RefDecoupAdmin") & "' AND S.CodeProjet='" & ProjetEnCours & "') ORDER BY CodeServiceSup ASC"
                    Dim dtService As DataTable = ExcecuteSelectQuery(query)
                    For Each rwServcie As DataRow In dtService.Rows
                        Dim MontantService As Decimal = 0
                        query = "SELECT SUM(QteNature*PUNature) As Total FROM t_besoinpartition "
                        query &= "WHERE CodePartition IN "
                        query &= "(SELECT DISTINCT(O.CodePartition) "
                        query &= "FROM t_operateurpartition O, t_partition P, t_grh_travailler T, t_fonction F, t_service S "
                        query &= "WHERE PosteActu='O' AND P.CodePartition=O.CodePartition AND O.TitreOpPart='Responsable' AND T.EMP_ID=O.EMP_ID AND "
                        query &= "F.RefFonction=T.CodeService AND S.CodeService=F.CodeService AND P.DateDebutPartition>='" & dateconvert(dtd.Text) & "' AND "
                        query &= "P.DateFinPartition<='" & dateconvert(dtf.Text) & "' AND S.CodeService='" & rwServcie("CodeService") & "' AND S.CodeProjet='" & ProjetEnCours & "')"
                        MontantService = Val(ExecuteScallar(query))
                        If MontantService > 0 Then
                            query = "INSERT INTO tamponbudgetairedirection VALUES(NULL,'" & EnleverApost(rwServcie("NomService")) & "','Service','" & MontantService & "','" & Math.Round(((MontantService * 100) / MontantTotal), 2).ToString().Replace(",", ".") & "','" & dateconvert(dtd.Text) & "','" & dateconvert(dtf.Text) & "','" & ProjetEnCours & "','" & SessionID & "')"
                            ExecuteNonQuery(query)
                        End If

                        Dim NiveauEdition As String = "Activité"
                        If CmbNiveauEdit.Text = "Composante" Then
                            NiveauEdition = "Composante"
                        ElseIf CmbNiveauEdit.Text = "Sous composante" Then
                            NiveauEdition = "Sous composante"
                        End If

                        If NiveauEdition = "Activité" Then
                            query = "SELECT * FROM t_partition WHERE CodePartition IN "
                            query &= "(SELECT DISTINCT(O.CodePartition) "
                            query &= "FROM t_operateurpartition O, t_partition P, t_grh_travailler T, t_fonction F "
                            query &= "WHERE PosteActu='O' AND P.CodePartition=O.CodePartition AND T.EMP_ID=O.EMP_ID AND "
                            query &= "F.RefFonction=T.CodeService AND O.TitreOpPart='Responsable' AND "
                            query &= "P.DateDebutPartition>='" & dateconvert(dtd.Text) & "' AND P.DateFinPartition<='" & dateconvert(dtf.Text) & "' AND "
                            query &= "F.CodeService='" & rwServcie("CodeService") & "')"
                        ElseIf NiveauEdition = "Composante" Then
                            query = "SELECT DISTINCT(MID(LibelleCourt,1,1)) As LibelleCourt,LibellePartition FROM t_partition "
                            query &= "WHERE CodePartition IN "
                            query &= "(SELECT DISTINCT(O.CodePartition) "
                            query &= "FROM t_operateurpartition O, t_partition P, t_grh_travailler T, t_fonction F "
                            query &= "WHERE PosteActu='O' AND P.CodePartition=O.CodePartition AND T.EMP_ID=O.EMP_ID AND "
                            query &= "F.RefFonction=T.CodeService AND O.TitreOpPart='Responsable' AND  .DateDebutPartition>='" & dateconvert(dtd.Text) & "' AND "
                            query &= "P.DateFinPartition<='" & dateconvert(dtf.Text) & "' AND F.CodeService='" & rwServcie("CodeService") & "')"
                        ElseIf NiveauEdition = "Sous Composante" Then
                            query = "SELECT DISTINCT(MID(LibelleCourt,1,2)) As LibelleCourt,LibellePartition "
                            query &= "FROM t_partition WHERE CodePartition IN "
                            query &= "(SELECT DISTINCT(O.CodePartition) "
                            query &= "FROM t_operateurpartition O, t_partition P, t_grh_travailler T, t_fonction F "
                            query &= "WHERE PosteActu='O' AND P.CodePartition=O.CodePartition AND T.EMP_ID=O.EMP_ID AND "
                            query &= "F.RefFonction=T.CodeService AND O.TitreOpPart='Responsable' AND P.DateDebutPartition>='" & dateconvert(dtd.Text) & "' AND "
                            query &= "P.DateFinPartition<='" & dateconvert(dtf.Text) & "' AND F.CodeService='" & rwServcie("CodeService") & "') AND LENGTH(LibelleCourt)=2"
                        End If
                        Dim dtPartition As DataTable = ExcecuteSelectQuery(query)
                        For Each rwPartition As DataRow In dtPartition.Rows
                            Dim MontantPartition As Decimal = 0
                            If NiveauEdition = "Activité" Then
                                query = "SELECT SUM(QteNature*PUNature) As Total FROM t_besoinpartition "
                                query &= "WHERE CodePartition='" & rwPartition("CodePartition") & "'"
                            ElseIf NiveauEdition = "Composante" Or NiveauEdition = "Sous Composante" Then
                                query = "SELECT SUM(QteNature*PUNature) As Total FROM t_besoinpartition WHERE CodePartition IN "
                                query &= "(SELECT DISTINCT(O.CodePartition) "
                                query &= "FROM t_operateurpartition O, t_partition P, t_grh_travailler T, t_fonction F "
                                query &= "WHERE PosteActu='O' AND P.CodePartition=O.CodePartition AND F.RefFonction=T.CodeService AND O.EMP_ID=T.EMP_ID AND "
                                query &= "P.LibelleCourt LIKE '" & rwPartition("LibelleCourt") & "%' AND P.DateDebutPartition>='" & dateconvert(dtd.Text) & "' AND "
                                query &= "P.DateFinPartition<='" & dateconvert(dtf.Text) & "' AND O.TitreOpPart='Responsable' AND "
                                query &= "F.CodeService='" & rwServcie("CodeService") & "')"
                            End If

                            Dim dtAllocation As DataTable = ExcecuteSelectQuery(query)
                            Dim rwAllocation As DataRow = dtAllocation.Rows(0)
                            Dim LibellePartition As String = rwPartition("LibelleCourt") & "  " & rwPartition("LibellePartition")
                            If Not IsDBNull(rwAllocation("Total")) Then
                                MontantPartition = CDec(rwAllocation("Total"))
                                query = "INSERT INTO tamponbudgetairedirection VALUES(NULL,'" & EnleverApost(LibellePartition.Trim()) & "','Partition','" & MontantPartition & "','" & Math.Round(((MontantPartition * 100) / MontantTotal), 2).ToString().Replace(",", ".") & "','" & dateconvert(dtd.Text) & "','" & dateconvert(dtf.Text) & "','" & ProjetEnCours & "','" & SessionID & "')"
                                ExecuteNonQuery(query)
                            End If
                        Next
                    Next
                Next
            End If

            query = "SELECT COUNT(*) FROM tamponbudgetairedirection WHERE CodeProjet='" & ProjetEnCours & "' AND CodeUtils='" & SessionID & "' AND DateDebut='" & dateconvert(dtd.Text) & "' AND DateFin='" & dateconvert(dtf.Text) & "'"
            If Val(ExecuteScallar(query)) = 0 Then
                SuccesMsg("Aucune donné trouvé")
                Me.Cursor = Cursors.Default
                BtActualiserPlan.PerformClick()
                Exit Sub
            End If
            reportConvention.SetParameterValue("CodeProjet", ProjetEnCours)
            reportConvention.SetParameterValue("CodeUtils", SessionID)
            reportConvention.SetParameterValue("DateDeb", dtd.Text)
            reportConvention.SetParameterValue("DateFin", dtf.Text)
            reportConvention.SetParameterValue("MontantTotal", AfficherMonnaie(MontantTotal))
            Try
                reportConvention.SetParameterValue("Parametre", parametre.ToUpper())
            Catch ex As Exception
            End Try

        End If

        reportAppercu.ReportSource = reportConvention
        DrX = ViewEdition.GetDataRow(ViewEdition.FocusedRowHandle)
        Fichier = DrX(3).ToString
        FinChargement()

    End Sub

    Private Sub CmbBailleur_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbBailleur.SelectedValueChanged

        If (GridEdition.Enabled = False) Then

            query = "select count(*) from T_Bailleur b, T_Convention c where b.CodeBailleur=c.CodeBailleur and b.InitialeBailleur='" & CmbBailleur.Text & "'"
            Dim nbre = ExecuteScallar(query)
            If nbre = 0 Then
                MsgBox("Aucune convention créé pour ce bailleur")
            Else
                If (CmbBailleur.Text <> "") Then
                    Dim MontTot As Decimal = 0
                    query = "select NomBailleur, AdresseCompleteBailleur, SiteWeb from T_Bailleur where CodeProjet='" & ProjetEnCours & "' and InitialeBailleur='" & CmbBailleur.Text & "'"
                    Dim dt As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        TxtBailleur.Text = MettreApost(rw(0).ToString)
                        TxtInfoBailleur.Text = MettreApost(rw(1).ToString) & vbNewLine & rw(2).ToString
                    Next

                    If (DrX(1).ToString = "C03") Then
                        BtImprimer.Enabled = False
                    Else
                        AfficherEtat(Fichier, DrX(1).ToString)
                    End If


                Else
                    TxtBailleur.Text = ""
                    TxtInfoBailleur.Text = ""

                    If (DrX(1).ToString = "C03") Then
                        BtImprimer.Enabled = False
                    Else
                        AfficherEtat(Fichier, DrX(1).ToString)
                    End If
                End If
            End If

        End If

    End Sub

    Private Sub BtRetour_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtRetour.Click

        GridEdition.Enabled = True
        TxtBailleur.Text = ""
        TxtInfoBailleur.Text = ""
        CmbBailleur.Text = ""
        ChkDecaisProjet.Checked = False
        ChkDecaisAnCour.Checked = False
        ChkParCompo.Checked = False
        ChkParSousCompo.Checked = False
        ChkParActivite.Checked = False
        PnlTypePlan.Visible = False
        PnlPartitionPlan.Visible = False
        BtActualiserPlan.Visible = False
        BtImprimer.Enabled = True
        reportAppercu.ReportSource = Nothing
        reportAppercu.Refresh()

    End Sub

    Private Sub BtImprimer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtImprimer.Click

        If (GridEdition.Enabled = False) Then
            FullScreenReport.FullView.ReportSource = reportAppercu.ReportSource
            FullScreenReport.ShowDialog()
        End If

    End Sub

    Private Sub EditionBudgetaire_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub ActualiserPlanDecaissement()

        query = "DELETE from T_PlanDecaisDate"
        ExecuteScallar(query)

        query = "DELETE from T_PlanDecaissement"
        ExecuteScallar(query)

        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)
        If (ChkDecaisAnCour.Checked = True) Then

            RechercheMontant("01/01/" & Now.Year.ToString)
            Dim DatSet = New DataSet
            query = "select * from T_PlanDecaisDate"

            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_PlanDecaisDate")
            Dim DatTable = DatSet.Tables("T_PlanDecaisDate")
            Dim DatRow = DatSet.Tables("T_PlanDecaisDate").NewRow()

            For k As Integer = 1 To 12
                DatRow("D" & k.ToString) = MonthName(k).ToUpper '& " " & Now.Year.ToString
            Next

            DatSet.Tables("T_PlanDecaisDate").Rows.Add(DatRow) 'ajout d'une nouvelle ligne 
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt) 'execution de l'enregistrement
            DatAdapt.Update(DatSet, "T_PlanDecaisDate")
            DatSet.Clear()


        ElseIf (ChkDecaisProjet.Checked = True) Then

            Dim DateDebb As Date = "01/01/3000"
            Dim DateFinn As Date = "01/01/2000"

            query = "select DateDebutPartition, DateFinPartition from T_Partition where CodeClassePartition='5' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                If (Date.Compare(DateDebb, CDate(rw(0).ToString)) > 0) Then
                    DateDebb = CDate(rw(0))
                End If

                If (Date.Compare(DateFinn, CDate(rw(1).ToString)) < 0) Then
                    DateFinn = CDate(rw(1))
                End If
            Next

            Dim AnC As Decimal = DateDebb.Year
            Dim AnX As Decimal = DateFinn.Year
            RechercheMontant(AnC.ToString, AnX.ToString)

            Dim DatSet = New DataSet

            query = "select * from T_PlanDecaisDate"
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_PlanDecaisDate")
            Dim DatTable = DatSet.Tables("T_PlanDecaisDate")
            Dim DatRow = DatSet.Tables("T_PlanDecaisDate").NewRow()

            Dim Cptr As Decimal = 1
            While (AnC <= AnX And Cptr <= 5)
                DatRow("D" & Cptr.ToString) = AnC.ToString
                AnC += 1
                Cptr += 1
            End While

            DatSet.Tables("T_PlanDecaisDate").Rows.Add(DatRow) 'ajout d'une nouvelle ligne 
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt) 'execution de l'enregistrement
            DatAdapt.Update(DatSet, "T_PlanDecaisDate")
            DatSet.Clear()
        End If

        BDQUIT(sqlconn)
    End Sub

    Private Sub RechercheMontant(ByVal DtDeb As String, Optional ByVal DtFin As String = "")

        Dim Periode(12) As String
        Dim nbPeriode As Decimal = 0
        If (DtFin = "") Then
            For k As Integer = 0 To 11
                Periode(k) = CDate("01/" & (k + 1).ToString & "/" & Now.Year).ToShortDateString
            Next
            nbPeriode = 12
        Else
            While CInt(DtDeb) <= DtFin And nbPeriode <= 11
                Periode(nbPeriode) = "01/01/" & DtDeb
                DtDeb = (CInt(DtDeb) + 1).ToString
                nbPeriode += 1
            End While
        End If

        Dim BailEch(5) As String
        Dim nbBail As Decimal = 0
        query = "select CodeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' and InitialeBailleur like '" & CmbBailleur.Text & "%'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows

            BailEch(nbBail) = rw(0).ToString
            nbBail += 1

        Next

        Dim LigneCompo(30, 14) As String
        Dim LigneSousCompo(500, 14) As String
        Dim LigneActivite(500, 14) As String

        Dim SommeSousCompo(12) As Decimal
        Dim SommeCompo(12) As Decimal

        For i As Integer = 0 To 99
            For j As Integer = 0 To 13
                LigneSousCompo(i, j) = "0"
            Next
        Next

        For i As Integer = 0 To 29
            For j As Integer = 0 To 13
                LigneCompo(i, j) = "0"
            Next
        Next


        Dim cptCompo As Decimal = 0
        Dim cptSouComp As Decimal = 0
        Dim cptActiv As Decimal = 0

        For k As Integer = 0 To nbBail - 1

            query = "select CodePartition, LibelleCourt from T_Partition where LENGTH(LibelleCourt)='1' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt3 As DataTable = ExcecuteSelectQuery(query)
            For Each rw3 As DataRow In dt3.Rows
                If (ChkParCompo.Checked = True) Then
                    LigneCompo(cptCompo, 0) = rw3(0).ToString
                    LigneCompo(cptCompo, 1) = BailEch(k)

                    For x As Decimal = 0 To 11
                        SommeCompo(x) = 0
                    Next
                End If


                query = "select CodePartition, LibelleCourt from T_Partition where CodeClassePartition=2 and LibelleCourt like '" & rw3(1).ToString & "%' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows
                    If (ChkParSousCompo.Checked = True) Then
                        LigneSousCompo(cptSouComp, 0) = rw1(0).ToString
                        LigneSousCompo(cptSouComp, 1) = BailEch(k)

                        For x As Decimal = 0 To 11
                            SommeSousCompo(x) = 0
                        Next

                    End If

                    query = "select CodePartition from T_Partition where CodeClassePartition='5' and LibelleCourt like '" & rw1(1).ToString & "%' and CodeProjet='" & ProjetEnCours & "'"
                    Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw2 As DataRow In dt2.Rows
                        If (ChkParActivite.Checked = True) Then
                            LigneActivite(cptActiv, 0) = rw2(0).ToString
                            LigneActivite(cptActiv, 1) = BailEch(k)
                        End If

                        For n As Decimal = 0 To nbPeriode - 1

                            'datedebut
                            Dim str(3) As String
                            str = Periode(n).ToString.Split("/")
                            Dim tempdt As String = String.Empty
                            For j As Integer = 2 To 0 Step -1
                                tempdt += str(j) & "-"
                            Next
                            tempdt = tempdt.Substring(0, 10)


                            'datefin
                            Dim str3(3) As String
                            str3 = CDate(Periode(n)).AddMonths(11).AddDays(30).ToString("dd/MM/yyyy").Split("/")
                            Dim tempdt3 As String = String.Empty
                            For j As Integer = 2 To 0 Step -1
                                tempdt3 += str3(j) & "-"
                            Next
                            tempdt3 = tempdt3.Substring(0, 10)

                            Dim str4(3) As String
                            str4 = CDate(Periode(n)).AddMonths(1).ToString("dd/MM/yyyy").Split("/")
                            Dim tempdt4 As String = String.Empty
                            For j As Integer = 2 To 0 Step -1
                                tempdt4 += str4(j) & "-"
                            Next
                            tempdt4 = tempdt4.Substring(0, 10)

                            Dim codepart As String = ""
                            codepart = rw2(0).ToString
                            Dim MontPeriode As Decimal = 0
                            Dim MontPeriode1 As Decimal = 0


                            'Dim Reader10 As MySqlDataReader
                            query = "select R.MontantBailleur, P.DateFinPartition from t_repartitionparbailleur as R, T_Partition as P, T_Besoinpartition B where B.CodePartition=P.CodePartition and B.RefBesoinPartition = R.RefBesoinPartition and P.CodePartition='" & codepart.ToString & "' and R.CodeBailleur='" & BailEch(k) & "' and P.DateFinPartition >= '" & tempdt & "' and P.DateFinPartition <= '" & IIf(DtFin <> "", tempdt3, tempdt4) & "'"
                            Dim dt4 As DataTable = ExcecuteSelectQuery(query)
                            For Each rw4 As DataRow In dt4.Rows
                                MontPeriode += CDec(rw4(0))
                                MontPeriode1 = CDec(rw4(0))
                            Next

                            If DtFin <> "" Then
                                SommeCompo(n) += MontPeriode / 2
                                SommeSousCompo(n) += MontPeriode / 2
                            Else
                                SommeCompo(n) += MontPeriode1 / 2
                                SommeSousCompo(n) += MontPeriode1 / 2
                            End If


                            If (ChkParActivite.Checked = True) Then
                                LigneActivite(cptActiv, n + 2) = MontPeriode / 2
                            End If

                        Next

                        cptActiv += 1
                    Next


                    If (ChkParSousCompo.Checked = True) Then
                        For n As Decimal = 0 To nbPeriode - 1
                            LigneSousCompo(cptSouComp, n + 2) = SommeSousCompo(n).ToString
                        Next

                        cptSouComp += 1
                    End If

                Next

                If (ChkParCompo.Checked = True) Then
                    For n As Decimal = 0 To nbPeriode - 1
                        LigneCompo(cptCompo, n + 2) = SommeCompo(n).ToString
                    Next

                    cptCompo += 1
                End If

            Next

        Next

        Dim nbX As Decimal = CInt(IIf(ChkParActivite.Checked = True, cptActiv, IIf(ChkParSousCompo.Checked = True, cptSouComp, cptCompo)))
        Dim Tab As Array = IIf(ChkParActivite.Checked = True, LigneActivite, IIf(ChkParSousCompo.Checked = True, LigneSousCompo, LigneCompo))

        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)
        For k As Integer = 0 To nbX - 1

            Dim DatSet = New DataSet

            query = "select * from T_PlanDecaissement"
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_PlanDecaissement")
            Dim DatTable = DatSet.Tables("T_PlanDecaissement")
            Dim DatRow = DatSet.Tables("T_PlanDecaissement").NewRow()

            DatRow("CodePartition") = Tab(k, 0)
            DatRow("CodeBailleur") = Tab(k, 1)
            For w As Decimal = 0 To nbPeriode - 1
                DatRow("Montant" & (w + 1).ToString) = Tab(k, w + 2)
            Next

            DatSet.Tables("T_PlanDecaissement").Rows.Add(DatRow) 'ajout d'une nouvelle ligne 
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt) 'execution de l'enregistrement
            DatAdapt.Update(DatSet, "T_PlanDecaissement")
            DatSet.Clear()

        Next
        BDQUIT(sqlconn)

    End Sub

    Private Sub BtActualiserPlan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtActualiserPlan.Click

        If (GridEdition.Enabled = False) Then
            DebutChargement(True, "Recherche du découpage budgétaire en cours...")
            ActualiserPlanDecaissement()
            BtImprimer.Enabled = True
            If (ChkDecaisAnCour.Checked = True) Then
                Fichier = Fichier.Replace(".rpt", "Projet.rpt")
            Else
                Fichier = Fichier.Replace("Projet.rpt", ".rpt")
            End If
            FinChargement()
            AfficherEtat(Fichier, DrX(1).ToString)
        End If

    End Sub

    Private Sub CmbNiveauEdit_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNiveauEdit.SelectedValueChanged
        If (GridEdition.Enabled = False) Then
            AfficherEtat(Fichier, DrX(1).ToString)
        End If
    End Sub

    Private Sub RemplirAllocAttrib()

        Dim repartType As String = ""
        query = "select UniteRepartitionBudget from T_ParamTechProjet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            repartType = rw(0).ToString
        Next

        'Conventions
        Dim Convent(20) As String
        Dim nbConvent As Decimal = 0
        query = "select C.CodeConvention from T_Convention as C, T_Bailleur as B where C.CodeBailleur=B.CodeBailleur and B.CodeProjet='" & ProjetEnCours & "'"
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        For Each rw1 As DataRow In dt1.Rows
            Convent(nbConvent) = rw1(0).ToString
            nbConvent += 1
        Next

        'Attrib & Alloc
        query = "DELETE from T_TampAllocAttrib"
        ExecuteNonQuery(query)

        For k As Integer = 0 To nbConvent - 1

            Dim ReqType As String = ""
            If (repartType = "Composante") Then
                ReqType = "select CodePartition, LibelleCourt from T_Partition where LENGTH(LibelleCourt)='1' and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
            ElseIf (repartType = "Sous composante") Then
                ReqType = "select CodePartition, LibelleCourt from T_Partition where CodeClassePartition=2 and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
            ElseIf (repartType = "Activité") Then
                ReqType = "select CodePartition, LibelleCourt from T_Partition where CodeClassePartition='5' and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
            End If


            query = ReqType
            Dim dt2 As DataTable = ExcecuteSelectQuery(query)
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            For Each rw As DataRow In dt2.Rows

                Dim montAttrib As Decimal = 0
                query = "select R.MontantBailleur from T_Partition as P, T_BesoinPartition as B, T_RepartitionParBailleur as R where P.CodePartition=B.CodePartition and B.RefBesoinPartition=R.RefBesoinPartition and LENGTH(P.LibelleCourt)>='5' and P.LibelleCourt like '" & rw(1).ToString & "%' and R.CodeConvention='" & Convent(k) & "' and P.CodeProjet='" & ProjetEnCours & "'"
                Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                For Each rw3 As DataRow In dt3.Rows
                    montAttrib += CDec(rw3(0))
                Next

                Dim montAlloue As Decimal = 0
                Dim bailleur As String = ""
                query = "select distinct P.MontantAlloue, B.InitialeBailleur from T_Partition_Budget P, T_Convention C, T_Bailleur B where P.CodeConvention=C.CodeConvention and C.CodeBailleur=B.CodeBailleur and P.CodePartition='" & rw(0).ToString & "' and P.CodeConvention='" & Convent(k) & "'"
                Dim dt4 As DataTable = ExcecuteSelectQuery(query)
                For Each rw4 As DataRow In dt4.Rows
                    montAlloue += CDec(rw4(0))
                    bailleur = rw(1).ToString
                Next

                Dim DatSet = New DataSet
                query = "select * from T_TampAllocAttrib"
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "T_TampAllocAttrib")
                Dim DatTable = DatSet.Tables("T_TampAllocAttrib")
                Dim DatRow = DatSet.Tables("T_TampAllocAttrib").NewRow()

                DatRow("CodePartition") = rw(0).ToString
                DatRow("CodeConvention") = Convent(k)
                DatRow("Attribution") = montAttrib
                DatRow("Allocation") = montAlloue
                DatRow("InitialeBailleur") = bailleur

                DatSet.Tables("T_TampAllocAttrib").Rows.Add(DatRow)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "T_TampAllocAttrib")
                DatSet.Clear()

            Next
            BDQUIT(sqlconn)
        Next

    End Sub

    Private Sub Combdirect_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles Combdirect.SelectedValueChanged

        'Dim nbre As Decimal = 0
        'query = "select count(*) from t_divisionadministrative d, t_service s, t_fonction f, t_grh_travailler t, t_operateurpartition op, t_partition p "
        'query &= "where d.RefDecoupAdmin=s.RefDecoupAdmin and s.CodeService=f.CodeService and f.RefFonction=t.CodeService and t.EMP_ID=op.EMP_ID and op.CodePartition=p.CodePartition "
        'query &= "and d.RefDecoupAdmin='" & codedirect.ToString & "'"
        'nbre = ExecuteScallar(query)

        'If nbre = 0 Then
        '    SuccesMsg("Aucune donnée.")
        'Else

        '    If DrX(1).ToString = "C07" Then

        '        Dim reportDirection As New ReportDocument
        '        Dim crtableLogoninfos As New TableLogOnInfos
        '        Dim crtableLogoninfo As New TableLogOnInfo
        '        Dim crConnectionInfo As New ConnectionInfo
        '        Dim CrTables As Tables
        '        Dim CrTable As Table
        '        Dim Chemin As String = lineEtat & "\EditionBudgetaire\"

        '        Dim DatSet = New DataSet
        '        reportDirection.Load(Chemin & "EtatBudgetparDirection2.rpt")

        '        With crConnectionInfo
        '            .ServerName = ODBCNAME
        '            .DatabaseName = DB
        '            .UserID = USERNAME
        '            .Password = PWD
        '        End With

        '        CrTables = reportDirection.Database.Tables
        '        For Each CrTable In CrTables
        '            crtableLogoninfo = CrTable.LogOnInfo
        '            crtableLogoninfo.ConnectionInfo = crConnectionInfo
        '            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        '        Next

        '        reportDirection.SetDataSource(DatSet)
        '        reportDirection.SetParameterValue("DateDeb", dtd.Text)
        '        reportDirection.SetParameterValue("DateFin", dtf.Text)
        '        reportDirection.SetParameterValue("CodeProjet", ProjetEnCours)
        '        reportDirection.SetParameterValue("NiveauEdit", CmbNiveauEdit.Text)
        '        reportDirection.SetParameterValue("Direction", codedirect.ToString)
        '        reportAppercu.ReportSource = reportDirection

        '    ElseIf DrX(1).ToString = "C08" Then
        '        Dim reportDirection As New ReportDocument
        '        Dim crtableLogoninfos As New TableLogOnInfos
        '        Dim crtableLogoninfo As New TableLogOnInfo
        '        Dim crConnectionInfo As New ConnectionInfo
        '        Dim CrTables As Tables
        '        Dim CrTable As Table
        '        Dim Chemin As String = lineEtat & "\EditionBudgetaire\"

        '        Dim DatSet = New DataSet
        '        reportDirection.Load(Chemin & "EtatBudgetparDirection6.rpt")

        '        With crConnectionInfo
        '            .ServerName = ODBCNAME
        '            .DatabaseName = DB
        '            .UserID = USERNAME
        '            .Password = PWD
        '        End With

        '        CrTables = reportDirection.Database.Tables
        '        For Each CrTable In CrTables
        '            crtableLogoninfo = CrTable.LogOnInfo
        '            crtableLogoninfo.ConnectionInfo = crConnectionInfo
        '            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        '        Next

        '        reportDirection.SetDataSource(DatSet)
        '        reportDirection.SetParameterValue("DateDeb", dtd.Text)
        '        reportDirection.SetParameterValue("DateFin", dtf.Text)
        '        reportDirection.SetParameterValue("CodeProjet", ProjetEnCours)
        '        reportDirection.SetParameterValue("NiveauEdit", CmbNiveauEdit.Text)
        '        reportDirection.SetParameterValue("Direction", codedirect.ToString)
        '        reportAppercu.ReportSource = reportDirection
        '    End If
        'End If

    End Sub

    Private Sub Combdirect_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles Combdirect.SelectedIndexChanged
        If Combdirect.SelectedIndex <> -1 Then
            CodeDirect = LesDirections(Combdirect.SelectedIndex)
        Else
            CodeDirect = -1
        End If
        If (GridEdition.Enabled = False) Then
            AfficherEtat(Fichier, DrX(1).ToString)
        End If
    End Sub

    Private Sub Combserv_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles Combserv.SelectedIndexChanged
        Try
            If Combserv.SelectedIndex <> -1 Then

                CodeService = LesServices(Combserv.SelectedIndex)

                Dim nbre As Decimal = 0
                query = "select count(*) from t_service s, t_fonction f, t_grh_travailler t, t_operateurpartition op, t_partition p "
                query &= "where s.CodeService=f.CodeService and f.RefFonction=t.CodeService and t.EMP_ID=op.EMP_ID and op.CodePartition=p.CodePartition"
                query &= " and s.CodeService='" & CodeService & "'"
                nbre = ExecuteScallar(query)

                If nbre = 0 Then
                    SuccesMsg("Aucune donnée.")
                Else
                    Dim reportService As New ReportDocument
                    Dim crtableLogoninfos As New TableLogOnInfos
                    Dim crtableLogoninfo As New TableLogOnInfo
                    Dim crConnectionInfo As New ConnectionInfo
                    Dim CrTables As Tables
                    Dim CrTable As Table
                    Dim Chemin As String = lineEtat & "\EditionBudgetaire\"

                    Dim DatSet = New DataSet
                    reportService.Load(Chemin & "EtatBudgetParService1.rpt")

                    With crConnectionInfo
                        .ServerName = ODBCNAME
                        .DatabaseName = DB
                        .UserID = USERNAME
                        .Password = PWD
                    End With

                    CrTables = reportService.Database.Tables
                    For Each CrTable In CrTables
                        crtableLogoninfo = CrTable.LogOnInfo
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo
                        CrTable.ApplyLogOnInfo(crtableLogoninfo)
                    Next

                    reportService.SetDataSource(DatSet)
                    reportService.SetParameterValue("Date1", dtd.Text)
                    reportService.SetParameterValue("Date2", dtf.Text)
                    reportService.SetParameterValue("CodeProjet", ProjetEnCours)
                    reportService.SetParameterValue("NiveauEdit", CmbNiveauEdit.Text)
                    reportService.SetParameterValue("Service", CodeService)

                    Dim MontTot As Decimal = 0
                    query = "select C.MontantConvention from T_Bailleur as B, T_Convention as C where B.CodeBailleur=C.CodeBailleur and B.CodeProjet='" & ProjetEnCours & "'"
                    Dim dt As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        MontTot += CDec(rw(0))
                    Next
                    reportService.SetParameterValue("MontantTotal", MontTot)
                    reportAppercu.ReportSource = reportService
                End If
            Else
                CodeService = -1
                If (GridEdition.Enabled = False) Then
                    AfficherEtat(Fichier, DrX(1).ToString)
                End If
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub CombResp_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CombResp.SelectedIndexChanged
        Try
            If CombResp.SelectedIndex <> -1 Then

                CodeRespo = LesResponsables(CombResp.SelectedIndex)

                Dim nbre As Decimal = 0
                query = "select count(*) from t_grh_employe e, t_operateurpartition op, t_partition p "
                query &= "where e.EMP_ID=op.EMP_ID and op.CodePartition=p.CodePartition "
                query &= "and e.EMP_ID='" & CodeRespo & "'"
                nbre = ExecuteScallar(query)

                If nbre = 0 Then
                    SuccesMsg("Aucune donnée.")
                Else
                    Dim reportResponsable As New ReportDocument
                    Dim crtableLogoninfos As New TableLogOnInfos
                    Dim crtableLogoninfo As New TableLogOnInfo
                    Dim crConnectionInfo As New ConnectionInfo
                    Dim CrTables As Tables
                    Dim CrTable As Table
                    Dim Chemin As String = lineEtat & "\EditionBudgetaire\"

                    Dim DatSet = New DataSet
                    reportResponsable.Load(Chemin & "EtatBudgetParRespo1.rpt")

                    With crConnectionInfo
                        .ServerName = ODBCNAME
                        .DatabaseName = DB
                        .UserID = USERNAME
                        .Password = PWD
                    End With

                    CrTables = reportResponsable.Database.Tables
                    For Each CrTable In CrTables
                        crtableLogoninfo = CrTable.LogOnInfo
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo
                        CrTable.ApplyLogOnInfo(crtableLogoninfo)
                    Next

                    reportResponsable.SetDataSource(DatSet)
                    reportResponsable.SetParameterValue("Date1", dtd.Text)
                    reportResponsable.SetParameterValue("Date2", dtf.Text)
                    reportResponsable.SetParameterValue("CodeProjet", ProjetEnCours)
                    reportResponsable.SetParameterValue("NiveauEdit", CmbNiveauEdit.Text)
                    reportResponsable.SetParameterValue("Responsable", CodeRespo.ToString())

                    Dim MontTot As Decimal = 0
                    query = "select C.MontantConvention from T_Bailleur as B, T_Convention as C where B.CodeBailleur=C.CodeBailleur and B.CodeProjet='" & ProjetEnCours & "'"
                    Dim dt As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        MontTot += CDec(rw(0))
                    Next

                    reportResponsable.SetParameterValue("MontantTotal", MontTot)
                    reportAppercu.ReportSource = reportResponsable
                End If
            Else
                CodeRespo = -1
                If (GridEdition.Enabled = False) Then
                    AfficherEtat(Fichier, DrX(1).ToString)
                End If
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

End Class