Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports MySql.Data.MySqlClient
Imports DevExpress.XtraEditors.Repository

Public Class FicheActivite

    Dim dtActivite = New DataTable()
    Dim DrX As DataRow

    'Dim TabTrue(1500) As Boolean
    'Dim nbTab As Decimal = 0

    Private Sub FicheActivite_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        'date
        Datedebub.Text = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
        DateFin.Text = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")
        'query = "select datedebut, datefin from T_COMP_EXERCICE where encours='1'"
        'Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt0.Rows
        'Next

        query = "select COUNT(*) from T_Partition where CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        If Not IsDBNull(dt.Rows(0).Item(0)) Then
            ' 'InitTabTrue()
            ChargerCompo()
            ChargerRespo()

            CmbCompo.Text = "Toutes"
            CmbSousCompo.Text = "Toutes"
        End If
    End Sub

    'Private Sub 'InitTabTrue()
    '    For n As Decimal = 0 To 499
    '        TabTrue(n) = False
    '    Next
    '    nbTab = 0
    'End Sub

    Private Sub ChargerCompo()
        query = "select LibelleCourt, LibellePartition from T_Partition where LENGTH(LibelleCourt)=1 and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
        CmbCompo.Properties.Items.Clear()
        CmbCompo.Properties.Items.Add("Toutes")
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbCompo.Properties.Items.Add(rw(0).ToString & " : " & MettreApost(rw(1).ToString))
        Next
    End Sub

    Private Sub CmbCompo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCompo.SelectedValueChanged

        GridActivites.DataSource = Nothing
        GridActivites.Refresh()

        ''InitTabTrue()

        CmbSousCompo.Properties.Items.Clear()
        CmbSousCompo.Text = ""

        If (CmbCompo.SelectedIndex <> -1) Then
            If (CmbCompo.Text = "Toutes") Then
                ChargerSousCompo("")
            Else
                ChargerSousCompo(Mid(CmbCompo.Text, 1, 1))
            End If
        End If

    End Sub

    Private Sub ChargerRespo()

        query = "select distinct(E.EMP_ID), EMP_NOM, EMP_PRENOMS from t_grh_employe as E, T_OperateurPartition as P where E.EMP_ID=P.EMP_ID and P.TitreOpPart='Responsable' and E.PROJ_ID='" & ProjetEnCours & "' ORDER BY EMP_NOM,EMP_PRENOMS ASC"
        CmbRespo.Properties.Items.Clear()
        CmbRespo.Properties.Items.Add("Sans")
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbRespo.Properties.Items.Add(MettreApost(rw(1).ToString & " " & rw(2).ToString) & " : " & IIf(Len(rw(0).ToString) = 1, "00", IIf(Len(rw(0).ToString) = 2, "0", "").ToString).ToString & rw(0).ToString)
        Next
    End Sub

    Private Sub ChargerSousCompo(ByVal Compo As String)

        query = "select LibelleCourt, LibellePartition from T_Partition where CodeClassePartition=2 and LibelleCourt like '" & Compo & "%' and CodeProjet='" & ProjetEnCours & "' order by length(Libellecourt),Libellecourt"
        CmbSousCompo.Properties.Items.Clear()
        CmbSousCompo.Properties.Items.Add("Toutes")
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbSousCompo.Properties.Items.Add(rw(0).ToString & " : " & MettreApost(rw(1).ToString))
        Next
    End Sub

    Private Sub CmbSousCompo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSousCompo.SelectedValueChanged

        GridActivites.DataSource = Nothing
        GridActivites.Refresh()

        'InitTabTrue()

        If (CmbSousCompo.SelectedIndex <> -1) Then
            ChkTous.Checked = False
            ChargerActivites()
        End If

    End Sub

    Private Sub ChargerActivites()
        'Dim TousVrai As Boolean = True
        'ChkTous.Enabled = False
        ChkTous.Checked = False

        dtActivite.Columns.Clear()

        dtActivite.Columns.Add("Code", Type.GetType("System.String"))
        dtActivite.Columns.Add("Code Partition", Type.GetType("System.String"))
        dtActivite.Columns.Add("Choix", Type.GetType("System.Boolean"))
        dtActivite.Columns.Add("Référence", Type.GetType("System.String"))
        dtActivite.Columns.Add("Libellé", Type.GetType("System.String"))
        dtActivite.Columns.Add("Date début", Type.GetType("System.String"))
        dtActivite.Columns.Add("Date fin", Type.GetType("System.String"))
        dtActivite.Columns.Add("Besoin", Type.GetType("System.String"))

        Dim clause As String = ""
        Dim clause1 As String = ""

        'conversion de la date
        Dim str(3) As String
        str = Datedebub.Text.Split("/")
        Dim tempdt As String = String.Empty
        For j As Integer = 2 To 0 Step -1
            tempdt += str(j) & "-"
        Next
        tempdt = tempdt.Substring(0, 10)

        Dim str1(3) As String
        str1 = DateFin.Text.Split("/")
        Dim tempdt1 As String = String.Empty
        For j As Integer = 2 To 0 Step -1
            tempdt1 += str1(j) & "-"
        Next
        tempdt1 = tempdt1.Substring(0, 10)

        'Requete Date
        If DateTime.Compare(tempdt1, tempdt) >= 0 Then
            clause = "AND dateDebutPartition >='" & tempdt & "' AND dateFinPartition <='" & tempdt1 & "' order by LibelleCourt"
        Else
            SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
        End If

        If DateTime.Compare(tempdt1, tempdt) >= 0 Then
            clause1 = "AND P.dateDebutPartition >='" & tempdt & "' AND P.dateFinPartition <='" & tempdt1 & "' order by P.LibelleCourt"
        Else
            SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
        End If

        Dim codeAct() As String
        codeAct = CmbSousCompo.Text.Split(" : ")
        If (CmbRespo.Text = "") Then
            query = "select CodePartition, LibelleCourt, LibellePartition, DateDebutPartition, DureePartitionPrevue, DateFinPartition from T_Partition where LENGTH(LibelleCourt)>4 and LibelleCourt like '" & IIf(CmbSousCompo.Text = "Toutes", IIf(CmbCompo.Text = "Toutes", "", Mid(CmbCompo.Text, 1, 1)).ToString, codeAct(0).ToString).ToString & "%' and CodeProjet='" & ProjetEnCours & "'" & clause
        ElseIf (CmbRespo.Text = "Sans") Then
            query = "select CodePartition, LibelleCourt, LibellePartition, DateDebutPartition, DureePartitionPrevue, DateFinPartition from T_Partition where LENGTH(LibelleCourt)>4 and LibelleCourt like '" & IIf(CmbSousCompo.Text = "Toutes", IIf(CmbCompo.Text = "Toutes", "", Mid(CmbCompo.Text, 1, 1)).ToString, codeAct(0)).ToString & "%' and CodeProjet='" & ProjetEnCours & "' and CodePartition Not In (select CodePartition from T_OperateurPartition where TitreOpPart='Responsable')" & clause
        Else
            If CmbRespo.Text.Contains(":") Then
                Dim partResp() As String = CmbRespo.Text.Split(":")
                Dim CodOp As Decimal = CInt(Trim(partResp(1)))
                query = "select P.CodePartition, P.LibelleCourt, P.LibellePartition, P.DateDebutPartition, P.DureePartitionPrevue, DateFinPartition from T_Partition as P, T_OperateurPartition as O where P.CodePartition=O.CodePartition and O.EMP_ID='" & CodOp.ToString & "' and O.TitreOpPart='Responsable' and LENGTH(P.LibelleCourt)>=5 and P.LibelleCourt like '" & IIf(CmbSousCompo.Text = "Toutes", IIf(CmbCompo.Text = "Toutes", "", Mid(CmbCompo.Text, 1, 1)).ToString, Mid(CmbSousCompo.Text, 1, 2)).ToString & "%' and P.CodeProjet='" & ProjetEnCours & "'" & clause1
            Else
                Dim CodOp As Decimal = 0
                query = "select P.CodePartition, P.LibelleCourt, P.LibellePartition, P.DateDebutPartition, P.DureePartitionPrevue, DateFinPartition from T_Partition as P, T_OperateurPartition as O where P.CodePartition=O.CodePartition and O.EMP_ID='" & CodOp.ToString & "' and O.TitreOpPart='Responsable' and LENGTH(P.LibelleCourt)>=5 and P.LibelleCourt like '" & IIf(CmbSousCompo.Text = "Toutes", IIf(CmbCompo.Text = "Toutes", "", Mid(CmbCompo.Text, 1, 1)).ToString, Mid(CmbSousCompo.Text, 1, 2)).ToString & "%' and P.CodeProjet='" & ProjetEnCours & "'" & clause1
            End If
        End If

        Dim cptr As Decimal = 0
        dtActivite.Rows.Clear()
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            query = "select Count(*) from T_BesoinPartition where CodePartition='" & rw(0).ToString & "'"
            Dim dt1 = ExcecuteSelectQuery(query)
            Dim nbBes As Decimal = 0
            If dt1.Rows.Count > 0 Then
                nbBes = CInt(dt1.Rows(0).Item(0))
            End If

            If ((nbBes = 0 And CmbMont.Text <> "Avec") Or (nbBes > 0 And CmbMont.Text <> "Sans")) Then

                cptr += 1
                Dim drS = dtActivite.NewRow()

                drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
                drS(1) = rw(0).ToString
                drS(2) = False 'TabTrue(cptr - 1)
                drS(3) = rw(1).ToString
                drS(4) = MettreApost(rw(2).ToString)
                drS(5) = CDate(rw(3)).ToString("dd/MM/yyyy")
                Dim partdurr() As String = rw(4).ToString.Split(" "c)
                drS(6) = CDate(rw(5)).ToString("dd/MM/yyyy")

                'Existance de besoin ****************
                drS(7) = IIf(nbBes > 0, "x", "").ToString

                'If (TabTrue(cptr - 1) = False) Then
                '    TousVrai = False
                'End If

                dtActivite.Rows.Add(drS)

            End If

        Next

        LblNombre.Text = cptr.ToString & " Enregistrements"

        GridActivites.DataSource = dtActivite

        Dim edit As RepositoryItemCheckEdit = New RepositoryItemCheckEdit()
        ViewActivites.Columns("Choix").ColumnEdit = edit
        GridActivites.RepositoryItems.Add(edit)
        ViewActivites.OptionsBehavior.Editable = True
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In ViewActivites.Columns
            col.OptionsColumn.AllowEdit = False 'On desactive la modife des colonnes pour activer uniquement celle des checkbox
        Next
        ViewActivites.Columns("Choix").OptionsColumn.AllowEdit = True 'Ici on active la modification de la colonne du checkbox
        'ViewActivites.OptionsSelection.MultiSelect = True
        ViewActivites.Columns(0).Visible = False
        ViewActivites.Columns(1).Visible = False
        ViewActivites.Columns(7).Visible = False
        ViewActivites.OptionsView.ColumnAutoWidth = True
        ViewActivites.OptionsBehavior.AutoExpandAllGroups = True
        ViewActivites.VertScrollVisibility = True
        ViewActivites.HorzScrollVisibility = True
        ViewActivites.BestFitColumns()
        ViewActivites.Columns("Choix").Width = 20
        ViewActivites.Columns("Choix").Caption = "..."
        ViewActivites.Columns(3).Width = 50
        ViewActivites.Columns(4).Width = GridActivites.Width - 408
        ViewActivites.Columns(5).Width = 60
        ViewActivites.Columns(6).Width = 60

        ViewActivites.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewActivites.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewActivites.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewActivites.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        ViewActivites.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

        ColorRowGrid(ViewActivites, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
        ColorRowGridAnal(ViewActivites, "[Choix]=true", Color.LightGray, "Times New Roman", 11, FontStyle.Bold, Color.Black, False)
        ColorRowGridAnal(ViewActivites, "[Besoin]<>'x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Gray, False)

        'If (ViewActivites.RowCount > 0) Then
        '    If (TousVrai = True) Then
        '        ChkTous.Checked = True
        '    Else
        '        ChkTous.Checked = False
        '    End If
        '    ChkTous.Enabled = True
        'Else
        '    ChkTous.Checked = False
        'End If

    End Sub

    Private Sub GridActivites_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'If (ViewActivites.RowCount > 0) Then
        '    DrX = ViewActivites.GetDataRow(ViewActivites.FocusedRowHandle)
        '    If (DrX(7).ToString <> "x") Then
        '        If TabTrue(ViewActivites.FocusedRowHandle) = False Then
        '            Dim RepImp As MsgBoxResult = MsgBox("Aucun coût enregistré sur cette activité!" & vbNewLine & "Voulez-vous l'imprimer?", MsgBoxStyle.YesNo)
        '            If (RepImp = MsgBoxResult.Yes) Then
        '                TabTrue(ViewActivites.FocusedRowHandle) = Not (TabTrue(ViewActivites.FocusedRowHandle))
        '                ChargerActivites()
        '                nbTab = ViewActivites.RowCount
        '            End If
        '            Exit Sub
        '        End If
        '    End If
        '    TabTrue(ViewActivites.FocusedRowHandle) = Not (TabTrue(ViewActivites.FocusedRowHandle))
        'ChargerActivites()
        '    nbTab = ViewActivites.RowCount
        'End If
    End Sub

    Private Sub BtAppercu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtAppercu.Click
        If Not Access_Btn("BtnPrintFicheActivite") Then
            Exit Sub
        End If

        If (ViewActivites.RowCount > 0) Then
            'If (nbTab > 0 And ViewActivites.RowCount > 0) Then
            DebutChargement(True, "Recherche des informations demandées en cours...")

            'Chargement des conventions
            query = "SELECT COUNT(*) FROM t_convention c, t_bailleur b WHERE b.CodeBailleur=c.CodeBailleur AND b.CodeProjet='" & ProjetEnCours & "'"
            Dim ConvCount As Decimal = Val(ExecuteScallar(query))
            query = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS  WHERE table_name = 't_tampconvention' AND table_schema = '" & DB & "'"
            Dim OldConvCount As Decimal = Val(ExecuteScallar(query))
            If ConvCount <> OldConvCount Then
                'Suppression des conventions tamporaires
                If ConvCount > 1 Then
                    Dim ConvName As String = "conv"
                    If OldConvCount > 1 Then
                        For i = 2 To OldConvCount
                            ConvName = "conv" & i
                            query = "ALTER TABLE `t_tampconvention` DROP `" & ConvName & "`;"
                            ExecuteNonQuery(query)
                        Next
                    End If

                    For i = 2 To ConvCount
                        ConvName = "conv" & i
                        Dim Convaftername As String = "conv" & (i - 1)
                        query = "ALTER TABLE `t_tampconvention` ADD `" & ConvName & "` VARCHAR(255) NOT NULL AFTER `" & Convaftername & "`"
                        ExecuteNonQuery(query)
                    Next
                ElseIf ConvCount = 1 Then
                    Dim ConvName As String = "conv"
                    If OldConvCount > 1 Then
                        For i = 2 To OldConvCount
                            ConvName = "conv" & i
                            query = "ALTER TABLE `t_tampconvention` DROP `" & ConvName & "`;"
                            ExecuteNonQuery(query)
                        Next
                    End If
                End If
            End If
            'Insertion des conventions
            query = "SELECT c.CodeConvention FROM t_convention c, t_bailleur b WHERE b.CodeBailleur=c.CodeBailleur AND b.CodeProjet='" & ProjetEnCours & "' ORDER BY CodeConvention ASC"
            Dim dtConvention As DataTable = ExcecuteSelectQuery(query)
            Dim ConvString As String = String.Empty
            For Each rw As DataRow In dtConvention.Rows
                ConvString &= "'" & rw("CodeConvention") & "',"
            Next
            ConvString = Mid(ConvString, 1, (ConvString.Length - 1)) 'Enlever le dernier ';'

            ExecuteNonQuery("TRUNCATE t_tampconvention") 'Vider la table
            query = "INSERT INTO t_tampconvention VALUES(" & ConvString & ")"
            ExecuteNonQuery(query)

            'Chargement des colonnes de la table tampon
            query = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS  WHERE table_name = 't_tampficheactivite' AND table_schema = '" & DB & "' AND COLUMN_NAME='Conv'"
            If Val(ExecuteScallar(query)) > 0 Then 'Retire la colonne Conv de la table 't_tampficheactivite'
                query = "ALTER TABLE `t_tampficheactivite` DROP `Conv`;"
                ExecuteNonQuery(query)
            End If


            query = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS  WHERE table_name = 't_tampficheactivite' AND table_schema = '" & DB & "'"
            Dim OldTampCount As Decimal = Val(ExecuteScallar(query)) - 9

            If ConvCount <> OldTampCount Then
                'Suppression des conventions tamporaires
                If ConvCount > 1 Then
                    Dim ConvName As String = "MontBail"
                    If OldTampCount > 1 Then
                        For i = 2 To OldTampCount
                            ConvName = "MontBail" & i
                            query = "ALTER TABLE `t_tampficheactivite` DROP `" & ConvName & "`;"
                            ExecuteNonQuery(query)
                        Next
                    End If

                    For i = 2 To ConvCount
                        ConvName = "MontBail" & i
                        Dim Convaftername As String = "MontBail" & (i - 1)
                        query = "ALTER TABLE `t_tampficheactivite` ADD `" & ConvName & "` DECIMAL(20.0) NOT NULL DEFAULT 0 AFTER `" & Convaftername & "`"
                        ExecuteNonQuery(query)
                    Next
                ElseIf ConvCount = 1 Then
                    Dim ConvName As String = "MontBail"
                    If OldTampCount > 1 Then
                        For i = 2 To OldTampCount
                            ConvName = "MontBail" & i
                            query = "ALTER TABLE `t_tampficheactivite` DROP `" & ConvName & "`;"
                            ExecuteNonQuery(query)
                        Next
                    End If
                End If
            End If

            query = "DELETE FROM T_TampFicheActivite WHERE CodeUtils='" & SessionID & "' AND CodeProjet='" & ProjetEnCours & "'"
            ExecuteNonQuery(query)

            query = "DELETE FROM t_tamppartition WHERE CodeUtils='" & SessionID & "' AND CodeProjet='" & ProjetEnCours & "'"
            ExecuteNonQuery(query)

            'query = "TRUNCATE T_TampFicheActivite"
            'ExecuteNonQuery(query)

            'query = "TRUNCATE t_tamppartition"
            'ExecuteNonQuery(query)

            Dim countChecked As Decimal = 0
            For k As Integer = 0 To (ViewActivites.RowCount - 1)

                If (ViewActivites.GetRowCellValue(k, "Choix") = True) Then
                    'Insertion des activités selectionnées dans la table tamppartition
                    Dim LibelleCourt As String = ExecuteScallar("SELECT LibelleCourt FROM t_partition WHERE CodePartition='" & ViewActivites.GetRowCellValue(k, "Code Partition") & "'")
                    query = "INSERT INTO t_tamppartition VALUES(NULL,'" & LibelleCourt & "','" & ProjetEnCours & "','" & SessionID & "')"
                    ExecuteNonQuery(query)

                    countChecked += 1
                    query = "select RefBesoinPartition, NumeroComptable, LibelleBesoin, QteNature, UniteBesoin, PUNature from T_BesoinPartition where CodePartition='" & ViewActivites.GetRowCellValue(k, "Code Partition") & "'"
                    Dim dt As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        Dim MontConvString As String = String.Empty
                        Dim Tamp As Double = 0
                        query = "SELECT c.CodeConvention FROM t_convention c, t_bailleur b WHERE b.CodeBailleur=c.CodeBailleur AND b.CodeProjet='" & ProjetEnCours & "' ORDER BY CodeConvention ASC"
                        dtConvention = ExcecuteSelectQuery(query)
                        Dim Count As Decimal = 0
                        For Each rwConv As DataRow In dtConvention.Rows
                            Count += 1
                            query = "select MontantBailleur from T_RepartitionParBailleur where CodeConvention='" & rwConv("CodeConvention") & "' and RefBesoinPartition='" & rw("RefBesoinPartition").ToString & "'"
                            Dim dtRepartition As DataTable = ExcecuteSelectQuery(query)
                            If dtRepartition.Rows.Count > 0 Then
                                MontConvString &= "'" & CDec(dtRepartition.Rows(0)("MontantBailleur")) & "',"
                            Else
                                MontConvString &= "'0',"
                            End If
                        Next
                        MontConvString = Mid(MontConvString, 1, (MontConvString.Length - 1)) 'Enlever le dernier ','

                        query = "INSERT INTO T_TampFicheActivite VALUES(NULL,'" & rw("RefBesoinPartition") & "','" & rw("NumeroComptable") & "','" & EnleverApost(rw("LibelleBesoin")) & "','" & rw("QteNature") & "','" & rw("UniteBesoin") & "','" & rw("PUNature") & "','" & SessionID & "','" & ProjetEnCours & "'," & MontConvString & ")"
                        ExecuteNonQuery(query)
                    Next
                End If
            Next

            If countChecked = 0 Then
                FinChargement()
                SuccesMsg("Veuillez cocher au moins une activité.")
                Exit Sub
            End If
            ' Affichage état ***************************
            query = "SELECT COUNT(*) FROM t_convention c, t_bailleur b WHERE b.CodeBailleur=c.CodeBailleur AND b.CodeProjet='" & ProjetEnCours & "'"
            ' Dim ConvCount As Decimal = Val(ExecuteScallar(query))
            Dim EtatToLoad As String = lineEtat & "\FicheActivite\FicheActivite" & ConvCount & ".rpt"
            If IO.File.Exists(EtatToLoad) Then
                Dim reportActivite As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table

                Dim DatSet = New DataSet
                reportActivite.Load(EtatToLoad)

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = reportActivite.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                reportActivite.SetDataSource(DatSet)
                reportActivite.SetParameterValue("CodeProjet", ProjetEnCours)
                Try
                    reportActivite.SetParameterValue("CodeUtils", SessionID, "FicheActivite" & ConvCount & "_SousRapport")
                Catch ex As Exception
                    reportActivite.SetParameterValue("CodeUtils", SessionID, "FicheActivite" & ConvCount & "_SousRapport.rpt")
                End Try
                reportActivite.SetParameterValue("CodeUtils", SessionID)

                FullScreenReport.FullView.ReportSource = reportActivite

                FinChargement()
                FullScreenReport.ShowDialog()
            Else
                FailMsg("Trop de conventions enregistrées" & vbNewLine & "Veuillez migrer sur une ligne supérieure")
            End If

        Else
            'MsgBox("Aucune fiche d'activité selectionnée", MsgBoxStyle.Information, "ClearProject")
        End If

    End Sub

    Private Sub ChargerBailleur(ByRef TabBail() As String, ByRef nB As Decimal)
        nB = 0
        Dim NomBail(10) As String
        query = "select CodeBailleur, InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' order by InitialeBailleur"
        Dim dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            NomBail(nB) = rw(1).ToString
            TabBail(nB) = rw(0).ToString
            nB += 1
        Next
        query = "DELETE from T_TampBailleur"
        ExecuteNonQuery(query)

        Dim DatSet = New DataSet
        query = "select * from T_TampBailleur"
        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)
        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd)
        DatAdapt.Fill(DatSet, "T_TampBailleur")
        Dim DatTable = DatSet.Tables("T_TampBailleur")
        Dim DatRow = DatSet.Tables("T_TampBailleur").NewRow()

        For n As Decimal = 0 To nB - 1
            DatRow("Bail" & (n + 1).ToString) = NomBail(n)
        Next

        DatSet.Tables("T_TampBailleur").Rows.Add(DatRow)
        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        DatAdapt.Update(DatSet, "T_TampBailleur")
        BDQUIT(sqlconn)
        DatSet.Clear()
    End Sub
    'Private Sub ChargerConvention(ByRef TabConv() As String, ByRef nB As Decimal)
    '    nB = 0
    '    query = "select c.CodeConvention from T_Convention c, T_Bailleur b where b.CodeBailleur=c.CodeBailleur and b.CodeProjet='" & ProjetEnCours & "' order by c.CodeConvention"
    '    Dim dt0 = ExcecuteSelectQuery(query)
    '    For Each rw As DataRow In dt0.Rows
    '        TabConv(nB) = rw(0).ToString
    '        nB += 1
    '    Next

    '    query = "TRUNCATE T_TampConvention"
    '    ExecuteNonQuery(query)

    '    Dim DatSet = New DataSet
    '    query = "select * from T_TampConvention"
    '    Dim sqlconn As New MySqlConnection
    '    BDOPEN(sqlconn)
    '    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
    '    Dim DatAdapt = New MySqlDataAdapter(Cmd)
    '    DatAdapt.Fill(DatSet, "T_TampConvention")
    '    Dim DatTable = DatSet.Tables("T_TampConvention")
    '    Dim DatRow = DatSet.Tables("T_TampConvention").NewRow()

    '    For n As Decimal = 0 To nB - 1
    '        DatRow("conv" & (n + 1).ToString) = TabConv(n)
    '    Next

    '    DatSet.Tables("T_TampConvention").Rows.Add(DatRow)
    '    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
    '    DatAdapt.Update(DatSet, "T_TampConvention")
    '    BDQUIT(sqlconn)
    '    DatSet.Clear()
    'End Sub

    Private Sub FicheActivite_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub ChkTous_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkTous.CheckedChanged

        If (ViewActivites.RowCount > 0) Then
            If ChkTous.Checked Then
                For i As Integer = 0 To (ViewActivites.RowCount - 1)
                    ViewActivites.SetRowCellValue(i, "Choix", True)
                Next
            Else
                For i As Integer = 0 To (ViewActivites.RowCount - 1)
                    ViewActivites.SetRowCellValue(i, "Choix", False)
                Next
            End If
        Else
            ChkTous.Checked = False
        End If

    End Sub

    Private Sub CmbMont_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbMont.SelectedValueChanged
        ChargerActivites()
    End Sub

    Private Sub CmbRespo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbRespo.SelectedValueChanged
        ChargerActivites()
    End Sub

    Private Sub Datedebub_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles Datedebub.EditValueChanged
        If Datedebub.Text <> "" And DateFin.Text <> "" Then
            ChargerActivites()
        End If
    End Sub

    Private Sub DateFin_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles DateFin.EditValueChanged
        If Datedebub.Text <> "" And DateFin.Text <> "" Then
            ChargerActivites()
        End If
    End Sub

End Class