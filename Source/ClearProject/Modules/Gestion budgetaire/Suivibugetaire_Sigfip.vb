Imports System.Math
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Suivibugetaire_Sigfip
    Dim dtbdgetcompte = New DataTable
    Dim DrX As DataRow
    Dim NbTotal As Decimal = 0
    Dim dateDebutpartition As Date = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
    Dim dateFinpartition As Date = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")
    Dim CodePartitions As String()

    Private Sub cmbBudget_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBudget.SelectedIndexChanged
        CmbCritere.Text = ""
        CmbRech.Text = ""
        If cmbBudget.SelectedIndex <> -1 Then

            dtbdgetcompte.Columns.Clear()
            dtbdgetcompte.Columns.Add("CodeX", Type.GetType("System.String"))
            dtbdgetcompte.Columns.Add("N° Compte", Type.GetType("System.String"))
            dtbdgetcompte.Columns.Add("Libellé", Type.GetType("System.String"))
            dtbdgetcompte.Columns.Add("Dotation", Type.GetType("System.String"))
            dtbdgetcompte.Columns.Add("Réalisation", Type.GetType("System.String"))
            dtbdgetcompte.Columns.Add("Solde", Type.GetType("System.String"))
            dtbdgetcompte.Columns.Add("% Réalisation", Type.GetType("System.String"))
            dtbdgetcompte.Rows.Clear()
            LgListBudgetCompte.DataSource = dtbdgetcompte

            Try
                ViewBudgetCompte.Columns(0).Visible = False
                ViewBudgetCompte.Columns("Libellé").MaxWidth = 800
                ViewBudgetCompte.OptionsView.ColumnAutoWidth = True
                ViewBudgetCompte.OptionsBehavior.AutoExpandAllGroups = True
                ViewBudgetCompte.VertScrollVisibility = True
                ViewBudgetCompte.HorzScrollVisibility = True
                ViewBudgetCompte.BestFitColumns()

                ViewBudgetCompte.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                ViewBudgetCompte.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                ViewBudgetCompte.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                ColorRowGridAnal(ViewBudgetCompte, "[N° Compte]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
                ColorRowGridAnal(ViewBudgetCompte, "[Dotation]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)

            Catch ex As Exception

            End Try

            If CmbCritere.SelectedIndex <> -1 Then
                CmbCritere_SelectedIndexChanged(Me, e)
            End If
        End If
    End Sub

    Private Sub CmbCritere_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbCritere.SelectedIndexChanged
        If cmbBudget.SelectedIndex <> -1 Then
            CmbRech.Text = ""
            CmbRech.Enabled = True

            Select Case CmbCritere.Text
                'Par Projet
                Case "Par Projet"
                    Label3.Text = "Sélectionner Projet"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeCompProjEng()
                        CmbRech.Enabled = False
                    Else
                        DebutChargement()
                        RemplirListeCompProjEng("Dépense")
                        'RemplirListeCompProjDep()
                        CmbRech.Enabled = False
                    End If
                    Exit Select

                'Par Composante
                Case "Par Composante"
                    Label3.Text = "Sélectionner Composante"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeCompEng()
                    Else
                        DebutChargement()
                        RemplirListeCompEng("Dépense")
                        'RemplirListeCompDep()
                    End If
                    RemplirComboPartition1(CmbRech, 1)
                    Exit Select

                    'Par Sous composante
                Case "Par Sous Composante"
                    Label3.Text = "Sélectionner Sous Composante"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeSousCompEng()
                    Else
                        DebutChargement()
                        'RemplirListeSousCompDep()
                        RemplirListeSousCompEng("Dépense")
                    End If
                    RemplirComboPartition1(CmbRech, 2)
                    Exit Select

                    'Par Activité
                Case "Par Activité"
                    Label3.Text = "Sélectionner Activité"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeActivEng()
                    Else
                        DebutChargement()
                        RemplirListeActivEng("Dépense")
                        'RemplirListeActivDep()
                    End If
                    LoadActivites(CmbRech)
                    Exit Select

                Case "Par Bailleur"
                    Label3.Text = "Sélectionner Activité"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeBailEng()
                    Else
                        DebutChargement()
                        RemplirListeBailEng("Dépense")
                        'RemplirListeBailDep()
                    End If
                    RemplirComboBail1(CmbRech)
                    Exit Select

                Case "Par Convention"
                    Label3.Text = "Sélectionner Convention"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeConvEng()
                    Else
                        DebutChargement()
                        RemplirListeConvEng("Dépense")
                        'RemplirListeConvDep()
                    End If
                    RemplirComboConv1(CmbRech)
                    Exit Select

                Case Else
                    dtbdgetcompte.Rows.Clear()
                    LgListBudgetCompte.DataSource = dtbdgetcompte

            End Select
        Else
            dtbdgetcompte.Rows.Clear()
            LgListBudgetCompte.DataSource = dtbdgetcompte
        End If
    End Sub
    Private Sub LoadActivites(ByRef Combo As DevExpress.XtraEditors.ComboBoxEdit)
        Try
            Combo.Properties.Items.Clear()
            query = "select p.libelleCourt, p.LibellePartition, p.CodePartition from t_partition p WHERE CodeClassePartition = '5' AND p.codeProjet = '" & ProjetEnCours & "' AND p.DateDebutPartition>='" & dateconvert(dateDebutpartition) & "' AND  p.DateFinPartition<='" & dateconvert(dateFinpartition) & "' Order by p.libelleCourt"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            Dim i = 0
            ReDim CodePartitions(dt.Rows.Count)
            If dt.Rows.Count > 0 Then
                For Each rw In dt.Rows
                    CodePartitions(i) = rw("CodePartition")
                    i += 1
                    Combo.Properties.Items.Add(rw(0).ToString & " - " & MettreApost(rw(1).ToString))
                Next
            End If

        Catch ex As Exception
            SuccesMsg(ex.ToString())
        End Try
    End Sub
    Private Sub RemplirListeCompProjEng(Optional opt As String = "Engagement")
        'Déclaration variable
        Dim tDotation As Double = 0
        Dim soldeCompt As Double = 0
        Dim totalSoldeCompt As Double = 0
        Dim totalRealisation As Double = 0
        Dim prcentCompt As Double
        Dim prcentTotal As Double
        Dim clause As String = ""
        Dim clause1 As String = ""
        Dim temp(2) As String
        Dim factSansMarche As Double = 0

        'vider le datagrid
        dtbdgetcompte.Rows.Clear()

        'On efface les donnees tamporaires de l'utilisateur
        query = "DELETE FROM tampon6 WHERE CodeUtils='" & SessionID & "'" 'CodeUtils='" & SessionID & "'"
        ExecuteNonQuery(query)

        Try

            'Composante
            Dim lg As Decimal = 0
            Dim tDotComp As Double = 0
            Dim tRealComp As Double = 0
            Dim tSoldeComp As Double = 0
            Dim montMarche As Double = 0

            'Requete Date
            If DateTime.Compare(dateconvert(dpFin.Text), dateconvert(dpDebut.Text)) >= 0 Then
                clause = " AND p.dateDebutPartition>='" & dateconvert(dpDebut.Text) & "' AND p.dateDebutPartition <='" & dateconvert(dpFin.Text) & "'"
            Else
                SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
            End If

            'Récuperation des allocations
            query = "SELECT DISTINCT SIGFCOMPTE FROM T_CORRESPONDANCE_SIGFIP ORDER BY SIGFCOMPTE" 'On recherche toutes les correspondance des comptes SigFip
            Dim dtCorrespondant As DataTable = ExcecuteSelectQuery(query)
            If dtCorrespondant.Rows.Count > 0 Then 'On a pu retrouver des correspondances SigFip <=> Syscohada
                For Each rwSigFip As DataRow In dtCorrespondant.Rows
                    'Recuperation des allocations en fonction des correspondances SigFip trouvé
                    query = "SELECT b.numeroComptable, b.PUNature, b.QteNature, sc.libelle_sc, b.RefBesoinPartition,p.libellecourt,p.codepartition FROM t_besoinPartition b, t_comp_sous_classe sc, t_partition p WHERE b.numeroComptable=sc.code_sc and b.NumeroComptable IN (SELECT COMPTE FROM T_CORRESPONDANCE_SIGFIP WHERE SIGFCOMPTE='" & rwSigFip("SIGFCOMPTE").ToString & "') and p.CodeProjet='" & ProjetEnCours & "' and b.CodePartition = p.CodePartition" & clause
                    Dim dtAllocation = ExcecuteSelectQuery(query)
                    For Each rwAllocation As DataRow In dtAllocation.Rows 'dtAllocation
                        Dim dt4 As DataTable
                        Dim Facture As Decimal = 0
                        If opt = "Engagement" Then
                            'On va recuperer les marches
                            query = "SELECT sum(Montant_libellecourt) as Sum FROM t_acteng WHERE libellecourt='" & rwAllocation("libellecourt") & "' and NumeroComptable='" & rwAllocation("numeroComptable") & "'" ' AND RefMarche IN (SELECT RefMarche FROM t_repartitionparbailleur WHERE RefBesoinPartition='" & rwAllocation("RefBesoinPartition").ToString() & "' and RefMarche<>'0')"
                            'query = "SELECT sum(Montant_libellecourt) as Sum FROM t_acteng WHERE libellecourt='" & rwAllocation("libellecourt") & "' and RefMarche IN (SELECT RefMarche FROM t_marche WHERE NumeroComptable='" & rwAllocation("numeroComptable") & "')" ' AND RefMarche IN (SELECT RefMarche FROM t_repartitionparbailleur WHERE RefBesoinPartition='" & rwAllocation("RefBesoinPartition").ToString() & "' and RefMarche<>'0')"
                            dt4 = ExcecuteSelectQuery(query)
                            For Each rw4 As DataRow In dt4.Rows 'dt4
                                If Not IsDBNull(rw4("Sum")) Then
                                    montMarche = Round(CDec(rw4("Sum").ToString()), 0)
                                Else
                                    montMarche = 0
                                End If
                            Next 'dt4
                            'Recherche du montant des factures qui n'ont pas fait l'objet de marché
                            query = "SELECT sum(Montant_act) as Sum, CODE_SC, LibelleCourt FROM T_COMP_ACTIVITE WHERE CODE_SC='" & rwAllocation("numeroComptable").ToString & "' and codepartition='" & rwAllocation("codepartition") & "' and Date_act >='" & dateconvert(dpDebut.Text) & "' and Date_act <= '" & dateconvert(dpFin.Text) & "' and NumeroMarche='' group by code_sc, LibelleCourt"
                            dt4 = ExcecuteSelectQuery(query)
                            For Each rw4 As DataRow In dt4.Rows 'dt4
                                If Not IsDBNull(rw4("Sum")) Then
                                    Facture = CDec(rw4("Sum").ToString)
                                Else
                                    Facture = 0
                                End If
                            Next 'dt4
                        Else
                            'Recherche du montant de toutes les factures
                            query = "SELECT sum(Montant_act) as Sum, CODE_SC, LibelleCourt FROM T_COMP_ACTIVITE WHERE CODE_SC='" & rwAllocation("numeroComptable").ToString & "' and codepartition='" & rwAllocation("codepartition") & "' and Date_act >='" & dateconvert(dpDebut.Text) & "' and Date_act <= '" & dateconvert(dpFin.Text) & "' group by code_sc, LibelleCourt"
                            dt4 = ExcecuteSelectQuery(query)
                            For Each rw4 As DataRow In dt4.Rows 'dt4
                                If Not IsDBNull(rw4("Sum")) Then
                                    Facture = CDec(rw4("Sum").ToString)
                                Else
                                    Facture = 0
                                End If
                            Next 'dt4
                        End If

                        Try
                            Dim LibelleSigFip As String = ExecuteScallar("SELECT SIGFLIBELLE FROM t_plansigfip WHERE SIGFCOMPTE='" & rwSigFip("SIGFCOMPTE") & "'").ToString()
                            query = "insert into tampon6 values (NULL,'" & rwSigFip("SIGFCOMPTE").ToString & "','" & EnleverApost(LibelleSigFip) & "','" & rwAllocation("PUNature").ToString & "','" & rwAllocation("QteNature").ToString & "','','','" & ProjetEnCours & "','" & montMarche & "','" & facture & "','','" & SessionID & "')"
                            ExecuteNonQuery(query)
                        Catch ex As Exception

                        End Try

                    Next 'dtAllocation
                Next 'dtCorrespondance
            End If

            query = "SELECT numerosigfip, SUM(PUNature * QteNature), libelleBesoin, SUM(Realisation), SUM(facture), count(numerosigfip), LibelleCourt FROM tampon6 WHERE CodeUtils='" & SessionID & "' Group by numerosigfip, libelleBesoin Order by numerosigfip"
            Dim dotCompte As Double = 0
            Dim dtx As DataTable = ExcecuteSelectQuery(query)
            For Each rwx As DataRow In dtx.Rows
                'dotation
                dotCompte = CDbl(rwx(1).ToString)
                dotCompte = Round(dotCompte, 2)
                tDotation = Round(tDotation + dotCompte)
                tDotComp = Round(tDotComp + dotCompte)

                'realisation
                montMarche = CDec(rwx(3).ToString()) + CDec(rwx(4).ToString())
                tRealComp = Round(tRealComp + montMarche)
                totalRealisation = Round(totalRealisation + montMarche)

                Dim montMarche1 As Decimal = 0
                Try
                    montMarche1 = montMarche '/ rwx(5).ToString
                    'montMarche1 = montMarche / rwx(5).ToString
                Catch ex As Exception
                    montMarche1 = 0
                End Try

                'Calcul solde du compte
                soldeCompt = Round(dotCompte - montMarche)

                'Calcul total projet
                totalSoldeCompt = Round(totalSoldeCompt + soldeCompt)

                'Calcul pourcentage
                If dotCompte <> 0 Then
                    prcentCompt = (montMarche / dotCompte) * 100
                End If

                'ajout des lignes dans le viewgrid
                NbTotal += 1
                Dim drS = dtbdgetcompte.NewRow()
                drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                drS(1) = rwx("numerosigfip").ToString
                drS(2) = MettreApost(rwx(2).ToString)
                drS(3) = AfficherMonnaie(dotCompte.ToString)
                drS(4) = AfficherMonnaie(montMarche.ToString)
                drS(5) = AfficherMonnaie(soldeCompt)
                drS(6) = Round(prcentCompt, 2) & " % "
                dtbdgetcompte.Rows.Add(drS)
                LgListBudgetCompte.DataSource = dtbdgetcompte
                lg = lg + 1
            Next

            'Calcul Pourcentage total
            If tDotation <> 0 Then
                prcentTotal = (totalRealisation / tDotation) * 100
            End If
            If lg <> 0 Then
                NbTotal += 1
                Dim drS = dtbdgetcompte.NewRow()
                drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                If CmbRech.SelectedIndex = -1 Then
                    drS(1) = ""
                    drS(2) = "TOTAL PROJET " & ProjetEnCours
                End If
                'ajout des lignes dans le viewgrid
                drS(3) = AfficherMonnaie(tDotation.ToString)
                drS(4) = AfficherMonnaie(totalRealisation.ToString)
                drS(5) = AfficherMonnaie(tDotation.ToString - totalRealisation.ToString)
                drS(6) = Round(prcentTotal, 2) & " % "
                dtbdgetcompte.Rows.Add(drS)
                LgListBudgetCompte.DataSource = dtbdgetcompte

                ViewBudgetCompte.Columns(0).Visible = False
                ViewBudgetCompte.Columns("Libellé").MaxWidth = 800
                ViewBudgetCompte.OptionsView.ColumnAutoWidth = True
                ViewBudgetCompte.OptionsBehavior.AutoExpandAllGroups = True
                ViewBudgetCompte.VertScrollVisibility = True
                ViewBudgetCompte.HorzScrollVisibility = True
                ViewBudgetCompte.BestFitColumns()

                ViewBudgetCompte.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                ViewBudgetCompte.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                ViewBudgetCompte.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                ColorRowGridAnal(ViewBudgetCompte, "[N° Compte]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
                ColorRowGridAnal(ViewBudgetCompte, "[Dotation]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
            End If
            lblNbLign.Text = "Nbre de ligne(s) : " & lg.ToString
            FinChargement()

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub
    Private Sub RemplirListeCompEng(Optional opt As String = "Engagement")

        'Déclaration variable
        Dim tDotation As Double = 0
        Dim soldeCompt As Double = 0
        Dim totalSoldeCompt As Double = 0
        Dim totalRealisation As Double = 0
        Dim prcentCompt As Double
        Dim prcentTotal As Double
        Dim clause As String = ""
        Dim clause1 As String = ""
        Dim temp(2) As String
        Dim factSansMarche As Double = 0

        'vider le datagrid
        dtbdgetcompte.Rows.Clear()

        'On efface les donnees tamporaires de l'utilisateur
        query = "DELETE FROM tampon6 WHERE CodeUtils='" & SessionID & "'" 'CodeUtils='" & SessionID & "'"
        ExecuteNonQuery(query)
        Try
            'Requete Date
            If DateTime.Compare(dateconvert(dpFin.Text), dateconvert(dpDebut.Text)) >= 0 Then
                clause = "AND dateDebutPartition>='" & dateconvert(dpDebut.Text) & "' AND dateDebutPartition <='" & dateconvert(dpFin.Text) & "'"
            Else
                SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
            End If

            'sélection de la composante concernée
            If CmbRech.Text <> "" Or CmbRech.Text.Trim().Length <> 0 Then
                temp = CmbRech.Text.Split(" - ")
                clause1 = " AND libelleCourt LIKE '" & temp(0).ToString & "%' "
            End If

            Dim lg As Decimal = 0
            Dim tDotComp As Double = 0
            Dim tRealComp As Double = 0
            Dim tSoldeComp As Double = 0
            Dim montMarche As Double = 0
            'Composante
            query = "SELECT libelleCourt, LibellePartition, CodePartition from t_partition WHERE codeProjet = '" & ProjetEnCours & "' AND { fn LENGTH(LibelleCourt) } = 1" & clause1
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rwComp As DataRow In dt.Rows 'dtComposante
                query = "SELECT libelleCourt, LibellePartition, CodePartition from t_partition WHERE codeProjet = '" & ProjetEnCours & "' AND CodeClassePartition=2 And CodePartitionMere='" & rwComp("CodePartition").ToString & "' " & clause1
                Dim dtSComp As DataTable = ExcecuteSelectQuery(query)
                For Each rwSComp As DataRow In dtSComp.Rows 'dtSousComposante
                    'Activités
                    query = "SELECT codePartition,libellecourt from t_partition WHERE codeProjet = '" & ProjetEnCours & "'  AND CodeClassePartition=5 AND CodePartitionMere='" & rwSComp("CodePartition").ToString & "' " & clause
                    Dim dtActivites As DataTable = ExcecuteSelectQuery(query)
                    For Each rwActivites As DataRow In dtActivites.Rows 'dtActivites
                        'Récuperation des allocations
                        query = "SELECT DISTINCT SIGFCOMPTE FROM T_CORRESPONDANCE_SIGFIP ORDER BY SIGFCOMPTE" 'On recherche toutes les correspondance des comptes SigFip
                        Dim dtCorrespondant As DataTable = ExcecuteSelectQuery(query)
                        If dtCorrespondant.Rows.Count > 0 Then 'On a pu retrouver des correspondances SigFip <=> Syscohada
                            For Each rwSigFip As DataRow In dtCorrespondant.Rows
                                'Recuperation des allocations en fonction des correspondances SigFip trouvé
                                query = "SELECT b.numeroComptable, b.PUNature, b.QteNature, sc.libelle_sc, b.RefBesoinPartition FROM t_besoinPartition b, t_comp_sous_classe sc WHERE b.numeroComptable=sc.code_sc and b.NumeroComptable IN (SELECT COMPTE FROM T_CORRESPONDANCE_SIGFIP WHERE SIGFCOMPTE='" & rwSigFip("SIGFCOMPTE").ToString & "') and CodePartition='" & rwActivites("CodePartition").ToString & "'"
                                Dim dtAllocation = ExcecuteSelectQuery(query)
                                For Each rwAllocation As DataRow In dtAllocation.Rows 'dtAllocation
                                    'On va recuperer les marches
                                    Dim dt4 As DataTable
                                    Dim Facture As Decimal = 0
                                    If opt = "Engagement" Then
                                        query = "SELECT sum(Montant_libellecourt) as Sum FROM t_acteng WHERE libellecourt='" & rwActivites("libellecourt") & "' and NumeroComptable='" & rwAllocation("numeroComptable") & "'" ' AND RefMarche IN (SELECT RefMarche FROM t_repartitionparbailleur WHERE RefBesoinPartition='" & rwAllocation("RefBesoinPartition").ToString() & "' and RefMarche<>'0')"
                                        'query = "SELECT sum(Montant_libellecourt) as Sum FROM t_acteng WHERE RefMarche IN (SELECT RefMarche FROM t_marche WHERE NumeroComptable='" & rwAllocation("numeroComptable") & "') AND LibelleCourt='" & rwActivites("libellecourt") & "'" ' AND RefMarche IN (SELECT RefMarche FROM t_repartitionparbailleur WHERE RefBesoinPartition='" & rwAllocation("RefBesoinPartition").ToString() & "' and RefMarche<>'0')"
                                        dt4 = ExcecuteSelectQuery(query)
                                        For Each rw4 As DataRow In dt4.Rows 'dt4
                                            If Not IsDBNull(rw4("Sum")) Then
                                                montMarche = Round(CDec(rw4("Sum").ToString()), 0)
                                            Else
                                                montMarche = 0
                                            End If
                                        Next 'dt4
                                        'Recherche du montant des factures qui n'ont pas fait l'objet de marché
                                        query = "SELECT sum(Montant_act) as Sum, CODE_SC, LibelleCourt FROM T_COMP_ACTIVITE WHERE CODE_SC='" & rwAllocation("numeroComptable").ToString & "' and codepartition='" & rwActivites("codepartition") & "' and Date_act >='" & dateconvert(dpDebut.Text) & "' and Date_act <= '" & dateconvert(dpFin.Text) & "' and NumeroMarche='' group by code_sc, LibelleCourt"
                                        dt4 = ExcecuteSelectQuery(query)
                                        For Each rw4 As DataRow In dt4.Rows 'dt4
                                            If Not IsDBNull(rw4("Sum")) Then
                                                Facture = CDec(rw4("Sum").ToString)
                                            Else
                                                Facture = 0
                                            End If
                                        Next 'dt4
                                    Else
                                        montMarche = 0
                                        'Recherche du montant de toutes les factures
                                        query = "SELECT sum(Montant_act) as Sum, CODE_SC, LibelleCourt FROM T_COMP_ACTIVITE WHERE CODE_SC='" & rwAllocation("numeroComptable").ToString & "' and codepartition='" & rwActivites("codepartition") & "' and Date_act >='" & dateconvert(dpDebut.Text) & "' and Date_act <= '" & dateconvert(dpFin.Text) & "' group by code_sc, LibelleCourt"
                                        dt4 = ExcecuteSelectQuery(query)
                                        For Each rw4 As DataRow In dt4.Rows 'dt4
                                            If Not IsDBNull(rw4("Sum")) Then
                                                Facture = CDec(rw4("Sum").ToString)
                                            Else
                                                Facture = 0
                                            End If
                                        Next 'dt4
                                    End If

                                    Try
                                        Dim LibelleSigFip As String = ExecuteScallar("SELECT SIGFLIBELLE FROM t_plansigfip WHERE SIGFCOMPTE='" & rwSigFip("SIGFCOMPTE") & "'").ToString()
                                        query = "insert into tampon6 values (NULL,'" & rwSigFip("SIGFCOMPTE").ToString & "','" & EnleverApost(LibelleSigFip) & "','" & rwAllocation("PUNature").ToString & "','" & rwAllocation("QteNature").ToString & "','" & rwComp("libelleCourt").ToString & "','" & rwActivites("libellecourt").ToString & "','" & ProjetEnCours & "','" & montMarche & "','" & facture & "','','" & SessionID & "')"
                                        ExecuteNonQuery(query)
                                    Catch ex As Exception

                                    End Try

                                Next 'dtAllocation
                            Next 'dtCorrespondance
                        End If

                    Next 'dtActivites

                Next 'dtSousComposante

                query = "SELECT p.libelleCourt, p.LibellePartition from t_partition p, tampon6 t WHERE p.codeProjet = '" & ProjetEnCours & "' AND t.LibelleCourt=p.LibelleCourt AND t.libellecourt = '" & rwComp("libelleCourt").ToString & "' and CodeUtils='" & SessionID & "' GROUP BY p.libelleCourt, p.LibellePartition"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rwx As DataRow In dt1.Rows
                    'ajout des lignes dans le viewgrid
                    NbTotal += 1
                    Dim drS = dtbdgetcompte.NewRow()
                    drS(0) = "x"
                    drS(1) = rwx(0).ToString
                    drS(2) = ""
                    drS(3) = ""
                    drS(4) = ""
                    drS(5) = ""
                    drS(6) = ""
                    dtbdgetcompte.Rows.Add(drS)
                    'LgListBudgetCompte.DataSource = dtbdgetcompte
                    lg = lg + 1
                Next

                query = "SELECT numerosigfip, SUM(PUNature * QteNature), libelleBesoin, SUM(Realisation), SUM(facture), count(numerosigfip), LibelleCourt FROM tampon6 WHERE libellecourt = '" & rwComp("libelleCourt").ToString & "' and CodeUtils='" & SessionID & "' Group by numerosigfip, libelleBesoin Order by numerosigfip"
                Dim dotCompte As Double = 0
                dt1 = ExcecuteSelectQuery(query)
                For Each rwx As DataRow In dt1.Rows
                    'dotation
                    dotCompte = CDbl(rwx(1).ToString)
                    dotCompte = Round(dotCompte, 2)
                    tDotation = Round(tDotation + dotCompte)
                    tDotComp = Round(tDotComp + dotCompte)

                    'realisation
                    montMarche = CDec(rwx(3).ToString()) + CDec(rwx(4).ToString())
                    tRealComp = Round(tRealComp + montMarche)
                    totalRealisation = Round(totalRealisation + montMarche)

                    Dim montMarche1 As Decimal = 0
                    Try
                        montMarche1 = montMarche '/ rwx(5).ToString
                        'montMarche1 = montMarche / rwx(5).ToString
                    Catch ex As Exception
                        montMarche1 = 0
                    End Try

                    'Calcul solde du compte
                    soldeCompt = Round(dotCompte - montMarche)

                    'Calcul total projet
                    totalSoldeCompt = Round(totalSoldeCompt + soldeCompt)

                    'Calcul pourcentage
                    If dotCompte <> 0 Then
                        prcentCompt = (montMarche / dotCompte) * 100
                    End If

                    'ajout des lignes dans le viewgrid
                    NbTotal += 1
                    Dim drS = dtbdgetcompte.NewRow()
                    drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                    drS(1) = rwx("numerosigfip").ToString
                    drS(2) = MettreApost(rwx(2).ToString)
                    drS(3) = AfficherMonnaie(dotCompte.ToString)
                    drS(4) = AfficherMonnaie(montMarche.ToString)
                    drS(5) = AfficherMonnaie(soldeCompt)
                    drS(6) = Round(prcentCompt, 2) & " % "
                    dtbdgetcompte.Rows.Add(drS)
                    LgListBudgetCompte.DataSource = dtbdgetcompte
                    lg = lg + 1

                Next

                If CmbRech.SelectedIndex <> -1 Then
                    If tDotComp <> 0 Then
                        prcentTotal = (totalRealisation / tDotComp) * 100
                    End If

                    If lg <> 0 Then
                        'ajout des lignes dans le viewgrid
                        NbTotal += 1
                        Dim drS = dtbdgetcompte.NewRow()
                        drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                        drS(1) = ""
                        drS(2) = "TOTAL " & MettreApost(SearchTable2("LibellePartition ", "T_Partition", "LibelleCourt", temp(0).ToString))
                        drS(3) = AfficherMonnaie(tDotComp.ToString)
                        drS(4) = AfficherMonnaie(tRealComp.ToString)
                        drS(5) = AfficherMonnaie(tDotComp.ToString - tRealComp.ToString)
                        drS(6) = Round(prcentTotal, 2) & " % "
                        dtbdgetcompte.Rows.Add(drS)
                        LgListBudgetCompte.DataSource = dtbdgetcompte
                    End If
                    lblNbLign.Text = "Nbre de ligne(s) : " & lg.ToString
                    FinChargement()
                    Exit Sub
                End If

                If CmbRech.SelectedIndex = -1 Then
                    query = "SELECT p.libelleCourt, p.LibellePartition from t_partition p, tampon6 t WHERE p.codeProjet = '" & ProjetEnCours & "' and CodeUtils='" & SessionID & "' AND t.LibelleCourt=p.LibelleCourt AND t.libellecourt = '" & rwComp("libelleCourt").ToString & "' GROUP BY p.libelleCourt, p.LibellePartition"
                    dt1 = ExcecuteSelectQuery(query)
                    For Each rwx As DataRow In dt1.Rows
                        tSoldeComp = tDotComp - tRealComp
                        If tDotComp <> 0 Then
                            prcentCompt = (tRealComp / tDotComp) * 100S
                        End If
                        'ajout des lignes dans le viewgrid
                        NbTotal += 1
                        Dim drS = dtbdgetcompte.NewRow()
                        drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                        drS(1) = ""
                        drS(2) = "TOTAL " & MettreApost(rwx("LibellePartition").ToString)
                        drS(3) = AfficherMonnaie(tDotComp.ToString)
                        drS(4) = AfficherMonnaie(tRealComp.ToString)
                        drS(5) = AfficherMonnaie(tSoldeComp.ToString)
                        drS(6) = Round(prcentCompt, 2) & " % "
                        dtbdgetcompte.Rows.Add(drS)
                        LgListBudgetCompte.DataSource = dtbdgetcompte

                        tDotComp = 0
                        tRealComp = 0
                        lg = lg + 1
                    Next
                End If
            Next 'dt

            'Calcul Pourcentage total
            If tDotation <> 0 Then
                prcentTotal = (totalRealisation / tDotation) * 100
            End If

            If lg <> 0 Then
                NbTotal += 1
                Dim drS = dtbdgetcompte.NewRow()
                drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                If CmbRech.SelectedIndex = -1 Then
                    drS(1) = ""
                    drS(2) = "TOTAL PROJET " & ProjetEnCours
                Else
                    drS(1) = ""
                    drS(2) = "TOTAL " & MettreApost(SearchTable("LibellePartition ", "T_Partition", "LibelleCourt", temp(0).ToString))
                End If
                'ajout des lignes dans le viewgrid
                drS(3) = AfficherMonnaie(tDotation.ToString)
                drS(4) = AfficherMonnaie(totalRealisation.ToString)
                drS(5) = AfficherMonnaie(tDotation.ToString - totalRealisation.ToString)
                drS(6) = Round(prcentTotal, 2) & " % "
                dtbdgetcompte.Rows.Add(drS)
                LgListBudgetCompte.DataSource = dtbdgetcompte

                ViewBudgetCompte.Columns(0).Visible = False
                ViewBudgetCompte.Columns("Libellé").MaxWidth = 800
                ViewBudgetCompte.OptionsView.ColumnAutoWidth = True
                ViewBudgetCompte.OptionsBehavior.AutoExpandAllGroups = True
                ViewBudgetCompte.VertScrollVisibility = True
                ViewBudgetCompte.HorzScrollVisibility = True
                ViewBudgetCompte.BestFitColumns()

                ViewBudgetCompte.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                ViewBudgetCompte.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                ViewBudgetCompte.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                ColorRowGridAnal(ViewBudgetCompte, "[N° Compte]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
                ColorRowGridAnal(ViewBudgetCompte, "[Dotation]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)

            End If
            lblNbLign.Text = "Nbre de ligne(s) : " & lg.ToString
            FinChargement()

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub
    Private Sub RemplirListeSousCompEng(Optional opt As String = "Engagement")

        'Déclaration variable
        Dim tDotation As Double = 0
        Dim soldeCompt As Double = 0
        Dim totalSoldeCompt As Double = 0
        Dim totalRealisation As Double = 0
        Dim prcentCompt As Double
        Dim dot As Double = 0
        Dim real As Double = 0
        Dim prcentTotal As Double
        Dim clause As String = ""
        Dim clause1 As String = ""
        Dim temp(2) As String
        Dim factSansMarche As Double = 0

        'Vider le datagrid
        dtbdgetcompte.Rows.Clear()

        'On efface les donnees tamporaires de l'utilisateur
        query = "DELETE FROM tampon6 WHERE CodeUtils='" & SessionID & "'" 'CodeUtils='" & SessionID & "'"
        ExecuteNonQuery(query)
        Try
            'Requete Date
            If DateTime.Compare(dateconvert(dpFin.Text), dateconvert(dpDebut.Text)) >= 0 Then
                clause = "AND dateDebutPartition>='" & dateconvert(dpDebut.Text) & "' AND dateDebutPartition <='" & dateconvert(dpFin.Text) & "'"
            Else
                SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
            End If

            'sélection de la composante concernée
            If CmbRech.Text <> "" Or CmbRech.Text.Trim().Length <> 0 Then
                temp = CmbRech.Text.Split(" - ")
                clause1 = " AND libelleCourt LIKE '" & temp(0).ToString & "%' "
            End If

            Dim lg As Decimal = 0
            Dim tDotComp As Double = 0
            Dim tRealComp As Double = 0
            Dim tSoldeComp As Double = 0
            'Composante
            query = "SELECT libelleCourt, LibellePartition, CodePartition from t_partition WHERE codeProjet = '" & ProjetEnCours & "' AND { fn LENGTH(LibelleCourt) } = 1"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rwComp As DataRow In dt.Rows 'dtComposante
                'On enregistre une ligne pour la composante en cours
                Dim drS = dtbdgetcompte.NewRow()
                If CmbRech.SelectedIndex = -1 Then
                    NbTotal += 1
                    drS(0) = "x"
                    drS(1) = rwComp("libelleCourt").ToString
                    drS(2) = MettreApost(rwComp("LibellePartition").ToString())
                    drS(3) = ""
                    drS(4) = ""
                    drS(5) = ""
                    drS(6) = ""
                    dtbdgetcompte.Rows.Add(drS)
                    LgListBudgetCompte.DataSource = dtbdgetcompte
                    lg = lg + 1
                End If

                'Variables Sous Composante
                Dim tDotSComp As Double = 0
                Dim tRealSComp As Double = 0
                Dim tSoldeSComp As Double = 0
                Dim montMarche As Double = 0

                'Les sous composantes
                query = "SELECT libelleCourt, LibellePartition, CodePartition from t_partition WHERE codeProjet = '" & ProjetEnCours & "' AND CodeClassePartition=2 And CodePartitionMere='" & rwComp("CodePartition").ToString & "'" & clause1
                Dim dtSComp As DataTable = ExcecuteSelectQuery(query)
                For Each rwSComp As DataRow In dtSComp.Rows 'dtSousComposante
                    'Activités
                    query = "SELECT codePartition,libellecourt from t_partition WHERE codeProjet = '" & ProjetEnCours & "'  AND CodeClassePartition=5 AND CodePartitionMere='" & rwSComp("CodePartition").ToString & "' " & clause
                    Dim dtActivites As DataTable = ExcecuteSelectQuery(query)
                    For Each rwActivites As DataRow In dtActivites.Rows 'dtActivites
                        'Récuperation des allocations
                        query = "SELECT DISTINCT SIGFCOMPTE FROM T_CORRESPONDANCE_SIGFIP ORDER BY SIGFCOMPTE" 'On recherche toutes les correspondance des comptes SigFip
                        Dim dtCorrespondant As DataTable = ExcecuteSelectQuery(query)
                        If dtCorrespondant.Rows.Count > 0 Then 'On a pu retrouver des correspondances SigFip <=> Syscohada
                            For Each rwSigFip As DataRow In dtCorrespondant.Rows
                                'Recuperation des allocations en fonction des correspondances SigFip trouvé
                                query = "SELECT b.numeroComptable, b.PUNature, b.QteNature, sc.libelle_sc, b.RefBesoinPartition FROM t_besoinPartition b, t_comp_sous_classe sc WHERE b.numeroComptable=sc.code_sc and b.NumeroComptable IN (SELECT COMPTE FROM T_CORRESPONDANCE_SIGFIP WHERE SIGFCOMPTE='" & rwSigFip("SIGFCOMPTE").ToString & "') and CodePartition='" & rwActivites("CodePartition").ToString & "'"
                                Dim dtAllocation = ExcecuteSelectQuery(query)
                                For Each rwAllocation As DataRow In dtAllocation.Rows 'dtAllocation
                                    'On va recuperer les marches
                                    Dim dt4 As DataTable
                                    Dim Facture As Decimal = 0
                                    If opt = "Engagement" Then
                                        query = "SELECT sum(Montant_libellecourt) as Sum FROM t_acteng WHERE libellecourt='" & rwActivites("libellecourt") & "' and NumeroComptable='" & rwAllocation("numeroComptable") & "'" ' AND RefMarche IN (SELECT RefMarche FROM t_repartitionparbailleur WHERE RefBesoinPartition='" & rwAllocation("RefBesoinPartition").ToString() & "' and RefMarche<>'0')"
                                        'query = "SELECT sum(Montant_libellecourt) as Sum FROM t_acteng WHERE RefMarche IN (SELECT RefMarche FROM t_marche WHERE NumeroComptable='" & rwAllocation("numeroComptable") & "') AND LibelleCourt='" & rwActivites("libellecourt") & "'" ' AND RefMarche IN (SELECT RefMarche FROM t_repartitionparbailleur WHERE RefBesoinPartition='" & rwAllocation("RefBesoinPartition").ToString() & "' and RefMarche<>'0')"
                                        dt4 = ExcecuteSelectQuery(query)
                                        For Each rw4 As DataRow In dt4.Rows 'dt4
                                            If Not IsDBNull(rw4("Sum")) Then
                                                montMarche = Round(CDec(rw4("Sum").ToString()), 0)
                                            Else
                                                montMarche = 0
                                            End If
                                        Next 'dt4
                                        'Recherche du montant des factures qui n'ont pas fait l'objet de marché
                                        query = "SELECT sum(Montant_act) as Sum, CODE_SC, LibelleCourt FROM T_COMP_ACTIVITE WHERE CODE_SC='" & rwAllocation("numeroComptable").ToString & "' and codepartition='" & rwActivites("codepartition") & "' and Date_act >='" & dateconvert(dpDebut.Text) & "' and Date_act <= '" & dateconvert(dpFin.Text) & "' and NumeroMarche='' group by code_sc, LibelleCourt"
                                        dt4 = ExcecuteSelectQuery(query)
                                        For Each rw4 As DataRow In dt4.Rows 'dt4
                                            If Not IsDBNull(rw4("Sum")) Then
                                                Facture = CDec(rw4("Sum").ToString)
                                            Else
                                                Facture = 0
                                            End If
                                        Next 'dt4
                                    Else
                                        montMarche = 0
                                        'Recherche du montant de toutes les factures
                                        query = "SELECT sum(Montant_act) as Sum, CODE_SC, LibelleCourt FROM T_COMP_ACTIVITE WHERE CODE_SC='" & rwAllocation("numeroComptable").ToString & "' and codepartition='" & rwActivites("codepartition") & "' and Date_act >='" & dateconvert(dpDebut.Text) & "' and Date_act <= '" & dateconvert(dpFin.Text) & "' and NumeroMarche='' group by code_sc, LibelleCourt"
                                        dt4 = ExcecuteSelectQuery(query)
                                        For Each rw4 As DataRow In dt4.Rows 'dt4
                                            If Not IsDBNull(rw4("Sum")) Then
                                                Facture = CDec(rw4("Sum").ToString)
                                            Else
                                                Facture = 0
                                            End If
                                        Next 'dt4
                                    End If

                                    Try
                                        Dim LibelleSigFip As String = ExecuteScallar("SELECT SIGFLIBELLE FROM t_plansigfip WHERE SIGFCOMPTE='" & rwSigFip("SIGFCOMPTE") & "'").ToString()
                                        query = "insert into tampon6 values (NULL,'" & rwSigFip("SIGFCOMPTE").ToString & "','" & EnleverApost(LibelleSigFip) & "','" & rwAllocation("PUNature").ToString & "','" & rwAllocation("QteNature").ToString & "','" & rwComp("libelleCourt").ToString & "','" & rwSComp("libellecourt").ToString & "','" & ProjetEnCours & "','" & montMarche & "','" & facture & "','" & rwActivites("libellecourt").ToString() & "','" & SessionID & "')"
                                        ExecuteNonQuery(query)
                                    Catch ex As Exception

                                    End Try

                                Next 'dtAllocation
                            Next 'dtCorrespondance SigFip
                        End If

                    Next 'dtActivites

                    query = "SELECT p.libelleCourt, p.LibellePartition from t_partition p, tampon6 t WHERE p.codeProjet = '" & ProjetEnCours & "' AND t.LibelleCourt1=p.LibelleCourt AND t.libellecourt1 = '" & rwSComp("libelleCourt").ToString & "' and CodeUtils='" & SessionID & "' GROUP BY p.libelleCourt, p.LibellePartition"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rwx As DataRow In dt1.Rows
                        'ajout des lignes dans le viewgrid
                        NbTotal += 1
                        drS = dtbdgetcompte.NewRow()
                        drS(0) = "x"
                        drS(1) = rwx("libelleCourt").ToString
                        drS(2) = MettreApost(rwx("LibellePartition").ToString)
                        drS(3) = ""
                        drS(4) = ""
                        drS(5) = ""
                        drS(6) = ""
                        dtbdgetcompte.Rows.Add(drS)
                        'LgListBudgetCompte.DataSource = dtbdgetcompte
                        lg = lg + 1
                    Next

                    query = "SELECT numerosigfip, SUM(PUNature * QteNature), libelleBesoin, SUM(Realisation), SUM(facture), count(numerosigfip), LibelleCourt FROM Tampon6 WHERE libellecourt1 = '" & rwSComp("libelleCourt").ToString & "' and CodeUtils='" & SessionID & "' Group by numerosigfip, libelleBesoin Order by numerosigfip"
                    Dim dotCompte As Double = 0
                    dt1 = ExcecuteSelectQuery(query)
                    For Each rwx As DataRow In dt1.Rows
                        'dotation
                        dotCompte = CDbl(rwx(1).ToString)
                        dotCompte = Round(dotCompte, 2)
                        tDotation = Round(tDotation + dotCompte)
                        tDotComp = Round(tDotComp + dotCompte)
                        tDotSComp = Round(tDotSComp + dotCompte)
                        dot = dot + dotCompte
                        dot = Round(dot)

                        'realisation
                        montMarche = CDec(rwx(3).ToString()) + CDec(rwx(4).ToString())
                        tRealComp = Round(tRealComp + montMarche)
                        totalRealisation = Round(totalRealisation + montMarche)
                        real = Round(real + montMarche)
                        tRealSComp = Round(tRealSComp + montMarche)
                        tRealComp = Round(tRealComp + montMarche)

                        Dim montMarche1 As Decimal = 0
                        Try
                            montMarche1 = montMarche '/ rwx(5).ToString
                            'montMarche1 = montMarche / rwx(5).ToString
                        Catch ex As Exception
                            montMarche1 = 0
                        End Try

                        'Calcul solde du compte
                        soldeCompt = Round(dotCompte - montMarche)

                        'Calcul total projet
                        totalSoldeCompt = Round(totalSoldeCompt + soldeCompt)

                        'Calcul pourcentage
                        If dotCompte <> 0 Then
                            prcentCompt = (montMarche / dotCompte) * 100
                        End If

                        'ajout des lignes dans le viewgrid
                        NbTotal += 1
                        drS = dtbdgetcompte.NewRow()
                        drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                        drS(1) = rwx("numerosigfip").ToString
                        drS(2) = MettreApost(rwx(2).ToString)
                        drS(3) = AfficherMonnaie(dotCompte.ToString)
                        drS(4) = AfficherMonnaie(montMarche.ToString)
                        drS(5) = AfficherMonnaie(soldeCompt)
                        drS(6) = Round(prcentCompt, 2) & " % "
                        dtbdgetcompte.Rows.Add(drS)
                        LgListBudgetCompte.DataSource = dtbdgetcompte
                        lg = lg + 1

                    Next

                    If CmbRech.SelectedIndex <> -1 Then
                        If tDotSComp <> 0 Then
                            prcentTotal = (tRealSComp / tDotSComp) * 100
                        End If
                        If lg <> 0 Then
                            'ajout des lignes dans le viewgrid
                            NbTotal += 1
                            drS = dtbdgetcompte.NewRow()
                            drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                            drS(1) = ""
                            drS(2) = "TOTAL " & (SearchTable("LibellePartition ", "T_Partition", "LibelleCourt", temp(0).ToString))
                            drS(3) = AfficherMonnaie(tDotSComp.ToString)
                            drS(4) = AfficherMonnaie(tRealSComp.ToString)
                            drS(5) = AfficherMonnaie(tDotSComp.ToString - tRealSComp.ToString)
                            drS(6) = Round(prcentTotal, 2) & " % "
                            dtbdgetcompte.Rows.Add(drS)
                            LgListBudgetCompte.DataSource = dtbdgetcompte
                        End If
                        lblNbLign.Text = "Nbre de ligne(s) : " & lg.ToString
                        FinChargement()
                        Exit Sub
                    End If

                    If CmbRech.SelectedIndex = -1 Then
                        query = "SELECT p.libelleCourt, p.LibellePartition from t_partition p, tampon6 t WHERE p.codeProjet = '" & ProjetEnCours & "' and CodeUtils='" & SessionID & "' AND t.LibelleCourt1=p.LibelleCourt AND t.libellecourt1 = '" & rwSComp("libelleCourt").ToString & "' GROUP BY p.libelleCourt, p.LibellePartition"
                        dt1 = ExcecuteSelectQuery(query)
                        For Each rwx As DataRow In dt1.Rows
                            If tDotSComp <> 0 Then
                                prcentTotal = (tRealSComp / tDotSComp) * 100
                            End If
                            'ajout des lignes dans le viewgrid
                            NbTotal += 1
                            drS = dtbdgetcompte.NewRow()
                            drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                            drS(1) = ""
                            drS(2) = "TOTAL " & (MettreApost(rwx("LibellePartition")))
                            drS(3) = AfficherMonnaie(tDotSComp.ToString)
                            drS(4) = AfficherMonnaie(tRealSComp.ToString)
                            drS(5) = AfficherMonnaie(tDotSComp.ToString - tRealSComp.ToString)
                            drS(6) = Round(prcentTotal, 2) & " % "
                            dtbdgetcompte.Rows.Add(drS)
                            LgListBudgetCompte.DataSource = dtbdgetcompte
                            tDotSComp = 0
                            tRealSComp = 0
                            lg = lg + 1
                        Next
                    End If

                Next 'dtSousComposante

                If CmbRech.SelectedIndex = -1 Then
                    query = "SELECT p.libelleCourt, p.LibellePartition from t_partition p, tampon6 t WHERE p.codeProjet = '" & ProjetEnCours & "' and CodeUtils='" & SessionID & "' AND t.LibelleCourt=p.LibelleCourt AND t.libellecourt = '" & rwComp("libelleCourt").ToString & "' GROUP BY p.libelleCourt, p.LibellePartition"
                    Dim dtx As DataTable = ExcecuteSelectQuery(query)
                    For Each rwx As DataRow In dtx.Rows
                        If dot <> 0 Then
                            prcentTotal = (real / dot) * 100
                        End If

                        'ajout des lignes dans le viewgrid
                        NbTotal += 1
                        drS = dtbdgetcompte.NewRow()
                        drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                        drS(1) = ""
                        drS(2) = "TOTAL COMPOSANTE " & rwx("libelleCourt") & " : " & (MettreApost(rwx("LibellePartition")))
                        drS(3) = AfficherMonnaie(dot.ToString)
                        drS(4) = AfficherMonnaie(real.ToString)
                        drS(5) = AfficherMonnaie(dot.ToString - real.ToString)
                        drS(6) = Round(prcentTotal, 2) & " % "
                        dtbdgetcompte.Rows.Add(drS)
                        LgListBudgetCompte.DataSource = dtbdgetcompte

                        dot = 0
                        real = 0
                        lg = lg + 1
                    Next
                End If

            Next 'dtComposante

            'Calcul Pourcentage total
            If tDotation <> 0 Then
                prcentTotal = (totalRealisation / tDotation) * 100
            End If

            If lg <> 0 Then
                NbTotal += 1
                Dim drS = dtbdgetcompte.NewRow()
                drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                If CmbRech.SelectedIndex = -1 Then
                    drS(1) = ""
                    drS(2) = "TOTAL PROJET " & ProjetEnCours
                Else
                    drS(1) = ""
                    drS(2) = "TOTAL " & MettreApost(SearchTable("LibellePartition ", "T_Partition", "LibelleCourt", temp(0).ToString))
                End If
                'ajout des lignes dans le viewgrid
                drS(3) = AfficherMonnaie(tDotation.ToString)
                drS(4) = AfficherMonnaie(totalRealisation.ToString)
                drS(5) = AfficherMonnaie(tDotation.ToString - totalRealisation.ToString)
                drS(6) = Round(prcentTotal, 2) & " % "
                dtbdgetcompte.Rows.Add(drS)
                LgListBudgetCompte.DataSource = dtbdgetcompte

                ViewBudgetCompte.Columns(0).Visible = False
                ViewBudgetCompte.Columns("Libellé").MaxWidth = 800
                ViewBudgetCompte.OptionsView.ColumnAutoWidth = True
                ViewBudgetCompte.OptionsBehavior.AutoExpandAllGroups = True
                ViewBudgetCompte.VertScrollVisibility = True
                ViewBudgetCompte.HorzScrollVisibility = True
                ViewBudgetCompte.BestFitColumns()

                ViewBudgetCompte.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                ViewBudgetCompte.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                ViewBudgetCompte.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                ColorRowGridAnal(ViewBudgetCompte, "[N° Compte]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
                ColorRowGridAnal(ViewBudgetCompte, "[Dotation]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)

            End If
            lblNbLign.Text = "Nbre de ligne(s) : " & lg.ToString
            FinChargement()

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub
    Private Sub RemplirListeActivEng(Optional opt As String = "Engagement")
        'Déclaration variable
        Dim tDotation As Double = 0
        Dim soldeCompt As Double = 0
        Dim totalSoldeCompt As Double = 0
        Dim totalRealisation As Double = 0
        Dim prcentCompt As Double
        Dim dot As Double = 0
        Dim real As Double = 0
        Dim prcentTotal As Double
        Dim clause As String = ""
        Dim clause1 As String = ""
        Dim temp(2) As String
        Dim factSansMarche As Double = 0

        'Vider le datagrid
        dtbdgetcompte.Rows.Clear()

        'On efface les donnees tamporaires de l'utilisateur
        query = "DELETE FROM tampon6 WHERE CodeUtils='" & SessionID & "'" 'CodeUtils='" & SessionID & "'"
        ExecuteNonQuery(query)
        Try
            'Requete Date
            If DateTime.Compare(dateconvert(dateFinpartition), dateconvert(dateDebutpartition)) >= 0 Then
                clause = "AND dateDebutPartition >='" & dateconvert(dateDebutpartition) & "' AND dateDebutPartition <='" & dateconvert(dateFinpartition) & "'"
            Else
                SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
                Exit Sub
            End If

            'sélection de la composante concernée
            If CmbRech.SelectedIndex <> -1 Then
                clause1 = "AND p.CodePartition='" & CodePartitions(CmbRech.SelectedIndex) & "'"
            End If

            Dim lg As Decimal = 0
            Dim tDotComp As Double = 0
            Dim tRealComp As Double = 0
            Dim tSoldeComp As Double = 0
            'Composante
            query = "SELECT libelleCourt, LibellePartition, CodePartition from t_partition WHERE codeProjet = '" & ProjetEnCours & "' AND { fn LENGTH(LibelleCourt) } = 1"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rwComp As DataRow In dt.Rows 'dtComposante
                'On enregistre une ligne pour la composante en cours
                Dim drS = dtbdgetcompte.NewRow()
                If CmbRech.SelectedIndex = -1 Then
                    NbTotal += 1
                    drS(0) = "x"
                    drS(1) = rwComp("libelleCourt").ToString
                    drS(2) = MettreApost(rwComp("LibellePartition").ToString())
                    drS(3) = ""
                    drS(4) = ""
                    drS(5) = ""
                    drS(6) = ""
                    dtbdgetcompte.Rows.Add(drS)
                    LgListBudgetCompte.DataSource = dtbdgetcompte
                    lg = lg + 1
                End If

                'Variables Sous Composante
                Dim tDotSComp As Double = 0
                Dim tRealSComp As Double = 0
                Dim tSoldeSComp As Double = 0
                Dim montMarche As Double = 0

                'Les sous composantes
                query = "SELECT libelleCourt, LibellePartition, CodePartition from t_partition WHERE codeProjet = '" & ProjetEnCours & "' AND CodeClassePartition=2 And CodePartitionMere='" & rwComp("CodePartition").ToString & "' "
                Dim dtSComp As DataTable = ExcecuteSelectQuery(query)
                For Each rwSComp As DataRow In dtSComp.Rows 'dtSousComposante
                    If CmbRech.SelectedIndex = -1 Then
                        NbTotal += 1
                        drS = dtbdgetcompte.NewRow()
                        drS(0) = "x"
                        drS(1) = rwSComp("libelleCourt").ToString
                        drS(2) = (MettreApost(rwSComp("LibellePartition")))
                        drS(3) = ""
                        drS(4) = ""
                        drS(5) = ""
                        drS(6) = ""
                        dtbdgetcompte.Rows.Add(drS)
                        LgListBudgetCompte.DataSource = dtbdgetcompte
                        lg = lg + 1
                    End If
                    'Activités
                    If CmbRech.SelectedIndex <> -1 Then
                        query = "SELECT p.libelleCourt, p.LibellePartition, p.CodePartition from t_partition p WHERE p.codeProjet = '" & ProjetEnCours & "' AND { fn LENGTH(LibelleCourt) } >= 5  " & clause1
                    End If
                    If CmbRech.SelectedIndex = -1 Then
                        query = "SELECT codePartition,libellecourt from t_partition WHERE codeProjet = '" & ProjetEnCours & "'  AND CodeClassePartition=5 AND CodePartitionMere='" & rwSComp("CodePartition").ToString & "' " & clause + clause1
                        'query = "SELECT libelleCourt, LibellePartition, CodePartition from t_partition WHERE codeProjet = '" & ProjetEnCours & "' AND codeclassepartition= 2 AND CodePartitionMere = '" & rw0(2).ToString & "'"
                    End If
                    'query = "SELECT codePartition,libellecourt from t_partition WHERE codeProjet = '" & ProjetEnCours & "'  AND CodeClassePartition=5 AND CodePartitionMere='" & rwSComp("CodePartition").ToString & "' " & clause + clause1
                    Dim dtActivites As DataTable = ExcecuteSelectQuery(query)
                    For Each rwActivites As DataRow In dtActivites.Rows 'dtActivites
                        'Récuperation des allocations
                        query = "SELECT DISTINCT SIGFCOMPTE FROM T_CORRESPONDANCE_SIGFIP ORDER BY SIGFCOMPTE" 'On recherche toutes les correspondances des comptes SigFip
                        Dim dtCorrespondant As DataTable = ExcecuteSelectQuery(query)
                        If dtCorrespondant.Rows.Count > 0 Then 'On a pu retrouver des correspondances SigFip <=> Syscohada
                            For Each rwSigFip As DataRow In dtCorrespondant.Rows
                                'Recuperation des allocations en fonction des correspondances SigFip trouvé
                                query = "SELECT b.numeroComptable, b.PUNature, b.QteNature, sc.libelle_sc, b.RefBesoinPartition FROM t_besoinPartition b, t_comp_sous_classe sc WHERE b.numeroComptable=sc.code_sc and b.NumeroComptable IN (SELECT COMPTE FROM T_CORRESPONDANCE_SIGFIP WHERE SIGFCOMPTE='" & rwSigFip("SIGFCOMPTE").ToString & "') and CodePartition='" & rwActivites("CodePartition").ToString & "'"
                                Dim dtAllocation = ExcecuteSelectQuery(query)
                                For Each rwAllocation As DataRow In dtAllocation.Rows 'dtAllocation
                                    'On va recuperer les marches
                                    Dim dt4 As DataTable
                                    Dim Facture As Decimal = 0
                                    If opt = "Engagement" Then
                                        query = "SELECT sum(Montant_libellecourt) as Sum FROM t_acteng WHERE libellecourt='" & rwActivites("libellecourt") & "' and NumeroComptable='" & rwAllocation("numeroComptable") & "'" ' AND RefMarche IN (SELECT RefMarche FROM t_repartitionparbailleur WHERE RefBesoinPartition='" & rwAllocation("RefBesoinPartition").ToString() & "' and RefMarche<>'0')"
                                        'query = "SELECT sum(Montant_libellecourt) as Sum FROM t_acteng WHERE RefMarche IN (SELECT RefMarche FROM t_marche WHERE NumeroComptable='" & rwAllocation("numeroComptable") & "') AND LibelleCourt='" & rwActivites("libellecourt") & "'" ' AND RefMarche IN (SELECT RefMarche FROM t_repartitionparbailleur WHERE RefBesoinPartition='" & rwAllocation("RefBesoinPartition").ToString() & "' and RefMarche<>'0')"
                                        dt4 = ExcecuteSelectQuery(query)
                                        For Each rw4 As DataRow In dt4.Rows 'dt4
                                            If Not IsDBNull(rw4("Sum")) Then
                                                montMarche = Round(CDec(rw4("Sum").ToString()), 0)
                                            Else
                                                montMarche = 0
                                            End If
                                        Next 'dt4
                                        'Recherche du montant des factures qui n'ont pas fait l'objet de marché
                                        query = "SELECT sum(Montant_act) as Sum, CODE_SC, LibelleCourt FROM T_COMP_ACTIVITE WHERE CODE_SC='" & rwAllocation("numeroComptable").ToString & "' and codepartition='" & rwActivites("codepartition") & "' and Date_act >='" & dateconvert(dpDebut.Text) & "' and Date_act <= '" & dateconvert(dpFin.Text) & "' and NumeroMarche='' group by code_sc, LibelleCourt"
                                        dt4 = ExcecuteSelectQuery(query)
                                        For Each rw4 As DataRow In dt4.Rows 'dt4
                                            If Not IsDBNull(rw4("Sum")) Then
                                                Facture = CDec(rw4("Sum").ToString)
                                            Else
                                                Facture = 0
                                            End If
                                        Next 'dt4
                                    Else
                                        montMarche = 0
                                        'Recherche du montant de toutes les factures
                                        query = "SELECT sum(Montant_act) as Sum, CODE_SC, LibelleCourt FROM T_COMP_ACTIVITE WHERE CODE_SC='" & rwAllocation("numeroComptable").ToString & "' and codepartition='" & rwActivites("codepartition") & "' and Date_act >='" & dateconvert(dpDebut.Text) & "' and Date_act <= '" & dateconvert(dpFin.Text) & "' group by code_sc, LibelleCourt"
                                        dt4 = ExcecuteSelectQuery(query)
                                        For Each rw4 As DataRow In dt4.Rows 'dt4
                                            If Not IsDBNull(rw4("Sum")) Then
                                                Facture = CDec(rw4("Sum").ToString)
                                            Else
                                                Facture = 0
                                            End If
                                        Next 'dt4
                                    End If

                                    Try
                                        Dim LibelleSigFip As String = ExecuteScallar("SELECT SIGFLIBELLE FROM t_plansigfip WHERE SIGFCOMPTE='" & rwSigFip("SIGFCOMPTE") & "'").ToString()
                                        query = "insert into tampon6 values (NULL,'" & rwSigFip("SIGFCOMPTE").ToString & "','" & EnleverApost(LibelleSigFip) & "','" & rwAllocation("PUNature").ToString & "','" & rwAllocation("QteNature").ToString & "','" & rwComp("libelleCourt").ToString & "','" & rwSComp("libellecourt").ToString & "','" & ProjetEnCours & "','" & montMarche & "','" & facture & "','" & rwActivites("libellecourt").ToString() & "','" & SessionID & "')"
                                        ExecuteNonQuery(query)
                                    Catch ex As Exception

                                    End Try

                                Next 'dtAllocation
                            Next 'dtCorrespondance SigFip
                        End If

                    Next 'dtActivites

                    query = "SELECT p.libelleCourt, p.LibellePartition from t_partition p, tampon6 t WHERE p.codeProjet = '" & ProjetEnCours & "' AND t.LibelleCourt2=p.LibelleCourt AND t.libellecourt1 = '" & rwSComp("libelleCourt").ToString & "' and CodeUtils='" & SessionID & "' GROUP BY p.libelleCourt, p.LibellePartition"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rwx As DataRow In dt1.Rows
                        'ajout des lignes dans le viewgrid
                        NbTotal += 1
                        drS = dtbdgetcompte.NewRow()
                        drS(0) = "x"
                        drS(1) = rwx("libelleCourt").ToString
                        drS(2) = MettreApost(rwx("LibellePartition").ToString)
                        drS(3) = ""
                        drS(4) = ""
                        drS(5) = ""
                        drS(6) = ""
                        dtbdgetcompte.Rows.Add(drS)
                        'LgListBudgetCompte.DataSource = dtbdgetcompte
                        lg = lg + 1

                        query = "SELECT numerosigfip, SUM(PUNature * QteNature), libelleBesoin, SUM(Realisation), SUM(facture), count(numerosigfip), libellecourt2 FROM Tampon6 WHERE libellecourt2 = '" & rwx("libelleCourt").ToString & "' and CodeUtils='" & SessionID & "' Group by numerosigfip, libelleBesoin, libellecourt2 Order by numerosigfip"
                        Dim dotCompte As Double = 0
                        dt1 = ExcecuteSelectQuery(query)
                        For Each rwActiv As DataRow In dt1.Rows
                            'dotation
                            dotCompte = CDbl(rwActiv(1).ToString)
                            dotCompte = Round(dotCompte, 2)
                            tDotation = Round(tDotation + dotCompte)
                            tDotComp = Round(tDotComp + dotCompte)
                            tDotSComp = Round(tDotSComp + dotCompte)
                            dot = dot + dotCompte
                            dot = Round(dot)

                            'realisation
                            montMarche = CDec(rwActiv(3).ToString()) + CDec(rwActiv(4).ToString())
                            tRealComp = Round(tRealComp + montMarche)
                            totalRealisation = Round(totalRealisation + montMarche)
                            real = Round(real + montMarche)
                            tRealSComp = Round(tRealSComp + montMarche)
                            tRealComp = Round(tRealComp + montMarche)

                            Dim montMarche1 As Decimal = 0
                            Try
                                montMarche1 = montMarche '/ rwActiv(5).ToString
                                'montMarche1 = montMarche / rwActiv(5).ToString
                            Catch ex As Exception
                                montMarche1 = 0
                            End Try

                            'Calcul solde du compte
                            soldeCompt = Round(dotCompte - montMarche)

                            'Calcul total projet
                            totalSoldeCompt = Round(totalSoldeCompt + soldeCompt)

                            'Calcul pourcentage
                            If dotCompte <> 0 Then
                                prcentCompt = (montMarche / dotCompte) * 100
                            End If

                            'ajout des lignes dans le viewgrid
                            NbTotal += 1
                            drS = dtbdgetcompte.NewRow()
                            drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                            drS(1) = rwActiv("numerosigfip").ToString
                            drS(2) = MettreApost(rwActiv(2).ToString)
                            drS(3) = AfficherMonnaie(dotCompte.ToString)
                            drS(4) = AfficherMonnaie(montMarche.ToString)
                            drS(5) = AfficherMonnaie(soldeCompt)
                            drS(6) = Round(prcentCompt, 2) & " % "
                            dtbdgetcompte.Rows.Add(drS)
                            LgListBudgetCompte.DataSource = dtbdgetcompte
                            lg = lg + 1

                        Next
                    Next


                    If CmbRech.SelectedIndex <> -1 Then
                        If tDotSComp <> 0 Then
                            prcentTotal = (tRealSComp / tDotSComp) * 100
                        End If
                        If lg <> 0 Then
                            'ajout des lignes dans le viewgrid
                            NbTotal += 1
                            drS = dtbdgetcompte.NewRow()
                            drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                            drS(1) = ""
                            drS(2) = "TOTAL " & (SearchTable("LibellePartition ", "T_Partition", "CodePartition", CodePartitions(CmbRech.SelectedIndex)))
                            drS(3) = AfficherMonnaie(tDotSComp.ToString)
                            drS(4) = AfficherMonnaie(tRealSComp.ToString)
                            drS(5) = AfficherMonnaie(tDotSComp.ToString - tRealSComp.ToString)
                            drS(6) = Round(prcentTotal, 2) & " % "
                            dtbdgetcompte.Rows.Add(drS)
                            LgListBudgetCompte.DataSource = dtbdgetcompte
                        End If
                        lblNbLign.Text = "Nbre de ligne(s) : " & lg.ToString
                        FinChargement()
                        Exit Sub
                    End If

                    If CmbRech.SelectedIndex = -1 Then
                        query = "SELECT p.libelleCourt, p.LibellePartition from t_partition p, tampon6 t WHERE p.codeProjet = '" & ProjetEnCours & "' and CodeUtils='" & SessionID & "' AND t.LibelleCourt1=p.LibelleCourt AND t.libellecourt1 = '" & rwSComp("libelleCourt").ToString & "' GROUP BY p.libelleCourt, p.LibellePartition"
                        dt1 = ExcecuteSelectQuery(query)
                        For Each rwx As DataRow In dt1.Rows
                            If tDotSComp <> 0 Then
                                prcentTotal = (tRealSComp / tDotSComp) * 100
                            End If
                            'ajout des lignes dans le viewgrid
                            NbTotal += 1
                            drS = dtbdgetcompte.NewRow()
                            drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                            drS(1) = ""
                            drS(2) = "TOTAL SOUS COMPOSANTE " & rwSComp("LibelleCourt") & " : " & (MettreApost(rwx("LibellePartition")))
                            drS(3) = AfficherMonnaie(tDotSComp.ToString)
                            drS(4) = AfficherMonnaie(tRealSComp.ToString)
                            drS(5) = AfficherMonnaie(tDotSComp.ToString - tRealSComp.ToString)
                            drS(6) = Round(prcentTotal, 2) & " % "
                            dtbdgetcompte.Rows.Add(drS)
                            LgListBudgetCompte.DataSource = dtbdgetcompte
                            tDotSComp = 0
                            tRealSComp = 0
                            lg = lg + 1
                        Next
                    End If

                Next 'dtSousComposante

                If CmbRech.SelectedIndex = -1 Then
                    query = "SELECT p.libelleCourt, p.LibellePartition from t_partition p, tampon6 t WHERE p.codeProjet = '" & ProjetEnCours & "' and CodeUtils='" & SessionID & "' AND t.LibelleCourt=p.LibelleCourt AND t.libellecourt = '" & rwComp("libelleCourt").ToString & "' GROUP BY p.libelleCourt, p.LibellePartition"
                    Dim dtx As DataTable = ExcecuteSelectQuery(query)
                    For Each rwx As DataRow In dtx.Rows
                        If dot <> 0 Then
                            prcentTotal = (real / dot) * 100
                        End If

                        'ajout des lignes dans le viewgrid
                        NbTotal += 1
                        drS = dtbdgetcompte.NewRow()
                        drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                        drS(1) = ""
                        drS(2) = "TOTAL COMPOSANTE " & rwx("libelleCourt") & " : " & (MettreApost(rwx("LibellePartition")))
                        drS(3) = AfficherMonnaie(dot.ToString)
                        drS(4) = AfficherMonnaie(real.ToString)
                        drS(5) = AfficherMonnaie(dot.ToString - real.ToString)
                        drS(6) = Round(prcentTotal, 2) & " % "
                        dtbdgetcompte.Rows.Add(drS)
                        LgListBudgetCompte.DataSource = dtbdgetcompte

                        dot = 0
                        real = 0
                        lg = lg + 1
                    Next
                End If

            Next 'dtComposante



            'Calcul Pourcentage total
            If tDotation <> 0 Then
                prcentTotal = (totalRealisation / tDotation) * 100
            End If

            If lg <> 0 Then
                NbTotal += 1
                Dim drS = dtbdgetcompte.NewRow()
                drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                If CmbRech.SelectedIndex = -1 Then
                    drS(1) = ""
                    drS(2) = "TOTAL PROJET " & ProjetEnCours
                Else
                    drS(1) = ""
                    drS(2) = "TOTAL " & MettreApost(SearchTable("LibellePartition ", "T_Partition", "CodePartition", CodePartitions(CmbRech.SelectedIndex)))
                End If
                'ajout des lignes dans le viewgrid
                drS(3) = AfficherMonnaie(tDotation.ToString)
                drS(4) = AfficherMonnaie(totalRealisation.ToString)
                drS(5) = AfficherMonnaie(tDotation.ToString - totalRealisation.ToString)
                drS(6) = Round(prcentTotal, 2) & " % "
                dtbdgetcompte.Rows.Add(drS)
                LgListBudgetCompte.DataSource = dtbdgetcompte

                ViewBudgetCompte.Columns(0).Visible = False
                ViewBudgetCompte.Columns("Libellé").MaxWidth = 800
                ViewBudgetCompte.OptionsView.ColumnAutoWidth = True
                ViewBudgetCompte.OptionsBehavior.AutoExpandAllGroups = True
                ViewBudgetCompte.VertScrollVisibility = True
                ViewBudgetCompte.HorzScrollVisibility = True
                ViewBudgetCompte.BestFitColumns()

                ViewBudgetCompte.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                ViewBudgetCompte.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                ViewBudgetCompte.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                ColorRowGridAnal(ViewBudgetCompte, "[N° Compte]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
                ColorRowGridAnal(ViewBudgetCompte, "[Dotation]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)

            End If
            lblNbLign.Text = "Nbre de ligne(s) : " & lg.ToString
            FinChargement()

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())

        End Try
    End Sub
    Private Sub RemplirListeBailEng(Optional opt As String = "Engagement")
        'Déclaration variable
        Dim tDotation As Double = 0
        Dim soldeCompt As Double = 0
        Dim totalSoldeCompt As Double = 0
        Dim totalRealisation As Double = 0
        Dim prcentCompt As Double
        Dim prcentTotal As Double
        Dim clause As String = ""
        Dim clause1 As String = ""
        Dim temp(2) As String
        Dim factSansMarche As Double = 0
        Dim mBailleur As Double = 0
        Dim prcentBailleur As Double = 0
        Dim realBailleur As Double = 0

        Try

            dtbdgetcompte.Rows.Clear()

            'On efface les donnees tamporaires de l'utilisateur
            query = "DELETE FROM tampon6 WHERE CodeUtils='" & SessionID & "'" 'CodeUtils='" & SessionID & "'"
            ExecuteNonQuery(query)

            'Requete Date
            If DateTime.Compare(dateconvert(dpFin.Text), dateconvert(dpDebut.Text)) >= 0 Then
                clause = "AND dateDebutPartition >='" & dateconvert(dpDebut.Text) & "' AND dateDebutPartition <='" & dateconvert(dpFin.Text) & "'"
            Else
                SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
            End If

            'sélection du Bailleur concerné
            If CmbRech.SelectedIndex <> -1 Then
                temp = CmbRech.Text.Split(" - ")
                clause1 = " AND initialeBailleur='" & temp(0).ToString & "' "
            End If

            'initialisation des variables
            Dim tDotBail As Double = 0
            Dim tRealBail As Double = 0
            Dim tReal As Double = 0
            Dim tSoldeBail As Double = 0
            Dim lg As Decimal = 0
            query = "SELECT codeBailleur, initialeBailleur from t_bailleur WHERE codeProjet = '" & ProjetEnCours & "'" & clause1
            Dim dtBailleur As DataTable = ExcecuteSelectQuery(query)
            For Each rwBailleur In dtBailleur.Rows

                If CmbRech.SelectedIndex = -1 Then
                    'ajout des lignes dans le viewgrid
                    Dim drS = dtbdgetcompte.NewRow()
                    drS(0) = "x"
                    drS(1) = MettreApost(rwBailleur("initialeBailleur").ToString)
                    drS(2) = ""
                    drS(3) = ""
                    drS(4) = ""
                    drS(5) = ""
                    drS(6) = ""
                    dtbdgetcompte.Rows.Add(drS)
                    LgListBudgetCompte.DataSource = dtbdgetcompte
                    lg = lg + 1
                End If

                'Convention 
                Dim tDotConv As Double = 0
                Dim tRealConv As Double = 0
                Dim tSoldeConv As Double = 0
                query = "SELECT codeConvention FROM T_convention WHERE codeBailleur='" & rwBailleur("codeBailleur").ToString & "' "
                Dim dtConvention As DataTable = ExcecuteSelectQuery(query)
                For Each rwConvention In dtConvention.Rows
                    Dim drS = dtbdgetcompte.NewRow()
                    drS(0) = "x"
                    drS(1) = rwConvention("codeConvention").ToString
                    drS(2) = ""
                    drS(3) = ""
                    drS(4) = ""
                    drS(5) = ""
                    drS(6) = ""
                    dtbdgetcompte.Rows.Add(drS)
                    LgListBudgetCompte.DataSource = dtbdgetcompte
                    lg = lg + 1

                    'Activités
                    query = "SELECT codePartition,libellecourt from t_partition WHERE codeProjet = '" & ProjetEnCours & "'  AND CodeClassePartition=5 " & clause
                    Dim dtActivites As DataTable = ExcecuteSelectQuery(query)
                    For Each rwActivite In dtActivites.Rows
                        'Récupération des allocations des ***~ressources~***
                        query = "SELECT DISTINCT SIGFCOMPTE FROM T_CORRESPONDANCE_SIGFIP ORDER BY SIGFCOMPTE" 'On recherche toutes les correspondances des comptes SigFip
                        Dim dtCorrespondant As DataTable = ExcecuteSelectQuery(query)
                        If dtCorrespondant.Rows.Count > 0 Then 'On a pu retrouver des correspondances SigFip <=> Syscohada
                            For Each rwSigFip As DataRow In dtCorrespondant.Rows
                                'Recuperation des allocations en fonction des correspondances SigFip trouvé
                                query = "SELECT b.numeroComptable, b.PUNature, b.QteNature, sc.libelle_sc, b.RefBesoinPartition, r.montantBailleur, codeconvention FROM t_besoinPartition b, t_comp_sous_classe sc, t_repartitionParBailleur r WHERE b.numeroComptable=sc.code_sc and b.NumeroComptable IN (SELECT COMPTE FROM T_CORRESPONDANCE_SIGFIP WHERE SIGFCOMPTE='" & rwSigFip("SIGFCOMPTE").ToString & "') and b.CodePartition='" & rwActivite("CodePartition").ToString & "' AND b.refBesoinPartition=r.refBesoinPartition AND r.codeconvention='" & rwConvention("codeconvention").ToString & "' AND r.codeBailleur = '" & rwBailleur("codeBailleur").ToString & "'"
                                Dim dtAllocation = ExcecuteSelectQuery(query)
                                For Each rwAllocation As DataRow In dtAllocation.Rows 'dtAllocation
                                    Dim montbailleur As Double = 0
                                    Dim montMarche As Double = 0
                                    If rwAllocation("montantBailleur").ToString().Length <> 0 Then
                                        montbailleur = CDbl(rwAllocation("montantBailleur"))
                                    End If
                                    'On va recuperer les marches
                                    Dim dt4 As DataTable
                                    Dim Facture As Decimal = 0
                                    If opt = "Engagement" Then
                                        'query = "SELECT sum(Montant_libellecourt) as Sum FROM t_acteng WHERE libellecourt='" & rwActivite("libellecourt") & "'" ' AND RefMarche IN (SELECT RefMarche FROM t_repartitionparbailleur WHERE RefBesoinPartition='" & rwAllocation("RefBesoinPartition").ToString() & "' and RefMarche<>'0')"
                                        query = "SELECT sum(Montant_libellecourt) as Sum FROM t_acteng WHERE (RefMarche IN (SELECT RefMarche FROM t_marche WHERE CodeConvention='" & rwConvention("codeconvention") & "')) AND LibelleCourt='" & rwActivite("libellecourt") & "' and NumeroComptable='" & rwAllocation("numeroComptable") & "'" ' AND RefMarche IN (SELECT RefMarche FROM t_repartitionparbailleur WHERE RefBesoinPartition='" & rwAllocation("RefBesoinPartition").ToString() & "' and RefMarche<>'0')"
                                        dt4 = ExcecuteSelectQuery(query)
                                        For Each rw4 As DataRow In dt4.Rows 'dt4
                                            If Not IsDBNull(rw4("Sum")) Then
                                                montMarche = Round(CDec(rw4("Sum").ToString()), 0)
                                            Else
                                                montMarche = 0
                                            End If
                                        Next 'dt4
                                        'Recherche du montant des factures qui n'ont pas fait l'objet de marché
                                        query = "SELECT sum(Montant_act) as Sum, CODE_SC, LibelleCourt FROM T_COMP_ACTIVITE a, T_CategorieDepense c WHERE a.NumCateg=c.NumCateg and c.CodeConvention='" & rwConvention("codeconvention") & "' and a.CODE_SC='" & rwAllocation("numeroComptable").ToString() & "' and codepartition='" & rwActivite("codepartition") & "' and a.InitialeBailleur='" & rwBailleur("initialeBailleur").ToString & "' and a.Date_act >='" & dateconvert(dpDebut.Text) & "' and a.Date_act <= '" & dateconvert(dpFin.Text) & "' and NumeroMarche='' and a.code_Projet = '" & ProjetEnCours & "' group by code_sc, LibelleCourt"
                                        dt4 = ExcecuteSelectQuery(query)
                                        For Each rw4 As DataRow In dt4.Rows 'dt4
                                            If Not IsDBNull(rw4("Sum")) Then
                                                Facture = CDec(rw4("Sum").ToString)
                                            Else
                                                Facture = 0
                                            End If
                                        Next 'dt4
                                    Else
                                        'Recherche du montant de toutes les factures
                                        query = "SELECT sum(Montant_act) as Sum, CODE_SC, LibelleCourt FROM T_COMP_ACTIVITE a, T_CategorieDepense c WHERE a.NumCateg=c.NumCateg and c.CodeConvention='" & rwConvention("codeconvention") & "' and a.CODE_SC='" & rwAllocation("numeroComptable").ToString() & "' and codepartition='" & rwActivite("codepartition") & "' and a.InitialeBailleur='" & rwBailleur("initialeBailleur").ToString & "' and a.Date_act >='" & dateconvert(dpDebut.Text) & "' and a.Date_act <= '" & dateconvert(dpFin.Text) & "' and a.code_Projet = '" & ProjetEnCours & "' group by code_sc, LibelleCourt"
                                        dt4 = ExcecuteSelectQuery(query)
                                        For Each rw4 As DataRow In dt4.Rows 'dt4
                                            If Not IsDBNull(rw4("Sum")) Then
                                                Facture = CDec(rw4("Sum").ToString)
                                            Else
                                                Facture = 0
                                            End If
                                        Next 'dt4
                                    End If

                                    Try
                                        Dim LibelleSigFip As String = ExecuteScallar("SELECT SIGFLIBELLE FROM t_plansigfip WHERE SIGFCOMPTE='" & rwSigFip("SIGFCOMPTE") & "'").ToString()
                                        query = "insert into tampon6 values (NULL,'" & rwSigFip("SIGFCOMPTE").ToString & "','" & EnleverApost(LibelleSigFip) & "','" & montbailleur & "','" & rwAllocation("QteNature").ToString & "','" & rwBailleur("codeBailleur").ToString & "','" & rwBailleur("initialeBailleur").ToString & "','" & ProjetEnCours & "','" & montMarche & "','" & facture & "','" & rwConvention("codeconvention").ToString() & "','" & SessionID & "')"
                                        'query = "insert into tampon6 values (NULL,'" & rwSigFip("SIGFCOMPTE").ToString & "','" & EnleverApost(LibelleSigFip) & "','" & rwAllocation("PUNature").ToString & "','" & rwAllocation("QteNature").ToString & "','" & rwBailleur("codeBailleur").ToString & "','" & rwBailleur("initialeBailleur").ToString & "','" & ProjetEnCours & "','" & montMarche & "','" & Facture & "','" & rwConvention("codeconvention").ToString() & "','" & SessionID & "')"
                                        ExecuteNonQuery(query)
                                    Catch ex As Exception
                                        FailMsg(ex.ToString())
                                        InputBox(0, 0, query)
                                    End Try

                                Next 'dtAllocation
                            Next 'dtCorrespondance SigFip
                        End If
                    Next


                    Dim dotCompte As Double = 0
                    Dim montMarche1 As Double = 0
                    query = "SELECT numerosigfip, SUM(PUNature), libelleBesoin, SUM(Realisation), SUM(facture), count(numerosigfip), libellecourt2 FROM tampon6 WHERE libellecourt2 = '" & rwConvention("codeconvention").ToString & "' and CodeUtils='" & SessionID.ToString & "' Group by numerosigfip, libelleBesoin, libellecourt2 Order by numerosigfip"
                    Dim dtx As DataTable = ExcecuteSelectQuery(query)
                    For Each rwx In dtx.Rows

                        'dotation
                        dotCompte = CDbl(rwx(1).ToString)
                        dotCompte = Round(dotCompte)
                        tDotation = Round(tDotation + dotCompte)
                        tDotBail = Round(tDotBail + dotCompte)
                        tDotConv = Round(tDotConv + dotCompte)

                        'realisation
                        montMarche1 = Round(CDbl(rwx(3).ToString()) + CDbl(rwx(4).ToString()))
                        totalRealisation = Round(totalRealisation + montMarche1)
                        tRealBail = Round(tRealBail + montMarche1)
                        tRealConv = Round(tRealConv + montMarche1)
                        tReal = Round(tReal + montMarche1)

                        'Calcul solde du compte
                        soldeCompt = dotCompte - montMarche1

                        'Calcul total compte
                        totalSoldeCompt = totalSoldeCompt + soldeCompt

                        'Calcul pourcentage
                        If dotCompte <> 0 Then
                            prcentCompt = (montMarche1 / dotCompte) * 100
                        End If

                        'ajout des lignes dans le viewgrid
                        Dim drS1 = dtbdgetcompte.NewRow()
                        drS1(0) = "x"
                        drS1(1) = rwx(0).ToString
                        drS1(2) = MettreApost(rwx(2).ToString)
                        drS1(3) = AfficherMonnaie(dotCompte.ToString)
                        drS1(4) = AfficherMonnaie(tRealBail.ToString)
                        drS1(5) = AfficherMonnaie(soldeCompt)
                        drS1(6) = Round(prcentCompt, 2) & " % "
                        dtbdgetcompte.Rows.Add(drS1)
                        LgListBudgetCompte.DataSource = dtbdgetcompte
                        lg = lg + 1
                        dotCompte = 0
                        tRealBail = 0

                    Next

                    If CmbRech.SelectedIndex = -1 Then
                        tSoldeConv = tDotConv - tRealConv
                        If tDotConv <> 0 Then
                            prcentCompt = (tRealConv / tDotConv) * 100
                        End If
                        'ajout des lignes dans le viewgrid
                        NbTotal += 1
                        Dim drS2 = dtbdgetcompte.NewRow()
                        drS2(0) = "x"
                        drS2(1) = ""
                        drS2(2) = "TOTAL " & MettreApost(rwConvention(0).ToString)
                        drS2(3) = AfficherMonnaie(tDotConv.ToString)
                        drS2(4) = AfficherMonnaie(tRealConv.ToString)
                        drS2(5) = AfficherMonnaie(tSoldeConv.ToString)
                        drS2(6) = Round(prcentCompt, 2) & " % "
                        dtbdgetcompte.Rows.Add(drS2)
                        LgListBudgetCompte.DataSource = dtbdgetcompte
                        tDotConv = 0
                        tRealConv = 0
                        tSoldeConv = 0
                        prcentCompt = 0
                        lg = lg + 1
                    End If

                Next

                If CmbRech.SelectedIndex <> -1 Then

                    If tDotBail <> 0 Then
                        prcentTotal = (totalRealisation / tDotBail) * 100
                    End If

                    If lg <> 0 Then

                        'ajout des lignes dans le viewgrid
                        NbTotal += 1
                        Dim drS = dtbdgetcompte.NewRow()
                        drS(0) = "x"
                        drS(1) = ""
                        drS(2) = "TOTAL " & MettreApost(SearchTable("NomBailleur", "T_bailleur", "initialeBailleur", temp(0).ToString))
                        drS(3) = AfficherMonnaie(tDotBail.ToString)
                        drS(4) = AfficherMonnaie(totalRealisation.ToString)
                        drS(5) = AfficherMonnaie(tDotBail.ToString - totalRealisation.ToString)
                        drS(6) = Round(prcentTotal, 2) & " % "
                        dtbdgetcompte.Rows.Add(drS)
                        LgListBudgetCompte.DataSource = dtbdgetcompte

                    End If
                    lblNbLign.Text = "Nbre de ligne(s) : " & lg.ToString
                    FinChargement()
                    Exit Sub

                End If

                If CmbRech.SelectedIndex = -1 Then
                    tSoldeBail = tDotBail - tReal
                    If tDotBail <> 0 Then
                        prcentCompt = (tReal / tDotBail) * 100
                    End If
                    'ajout des lignes dans le viewgrid
                    NbTotal += 1
                    Dim drS = dtbdgetcompte.NewRow()
                    drS(0) = "x"
                    drS(1) = ""
                    drS(2) = "TOTAL " & MettreApost(rwBailleur(1).ToString)
                    drS(3) = AfficherMonnaie(tDotBail.ToString)
                    drS(4) = AfficherMonnaie(tReal.ToString)
                    drS(5) = AfficherMonnaie(tSoldeBail.ToString)
                    drS(6) = Round(prcentCompt, 2) & " % "
                    dtbdgetcompte.Rows.Add(drS)
                    LgListBudgetCompte.DataSource = dtbdgetcompte
                    tDotBail = 0
                    tReal = 0
                    prcentCompt = 0
                    lg = lg + 1
                End If

            Next

            'Calcul Pourcentage total
            If tDotation <> 0 Then
                prcentTotal = (totalRealisation / tDotation) * 100
            End If

            If lg <> 0 Then

                Dim drS = dtbdgetcompte.NewRow()
                drS(0) = "x"
                If CmbRech.SelectedIndex = -1 Then
                    drS(1) = ""
                    drS(2) = "TOTAL PROJET " & ProjetEnCours
                Else
                    drS(1) = ""

                    drS(2) = "TOTAL " & MettreApost(SearchTable("NomBailleur", "T_bailleur", "initialeBailleur", temp(0).ToString))

                End If

                drS(3) = AfficherMonnaie(tDotation.ToString)
                drS(4) = AfficherMonnaie(totalRealisation.ToString)
                drS(5) = AfficherMonnaie(tDotation.ToString - totalRealisation.ToString)
                drS(6) = Round(prcentTotal, 2) & " % "
                dtbdgetcompte.Rows.Add(drS)
                LgListBudgetCompte.DataSource = dtbdgetcompte

                ViewBudgetCompte.Columns(0).Visible = False
                ViewBudgetCompte.Columns("Libellé").MaxWidth = 800
                ViewBudgetCompte.OptionsView.ColumnAutoWidth = True
                ViewBudgetCompte.OptionsBehavior.AutoExpandAllGroups = True
                ViewBudgetCompte.VertScrollVisibility = True
                ViewBudgetCompte.HorzScrollVisibility = True
                ViewBudgetCompte.BestFitColumns()

                ViewBudgetCompte.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                ViewBudgetCompte.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                ViewBudgetCompte.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                ColorRowGridAnal(ViewBudgetCompte, "[N° Compte]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
                ColorRowGridAnal(ViewBudgetCompte, "[Dotation]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)

            End If
            lblNbLign.Text = "Nbre de ligne(s) : " & lg.ToString

            ViewBudgetCompte.Columns(0).Visible = False
            ViewBudgetCompte.Columns("Libellé").MaxWidth = 800
            ViewBudgetCompte.OptionsView.ColumnAutoWidth = True
            ViewBudgetCompte.OptionsBehavior.AutoExpandAllGroups = True
            ViewBudgetCompte.VertScrollVisibility = True
            ViewBudgetCompte.HorzScrollVisibility = True
            ViewBudgetCompte.BestFitColumns()
            FinChargement()


        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub
    Private Sub RemplirListeConvEng(Optional opt As String = "Engagement")
        'Déclaration variable
        Dim tDotation As Double = 0
        Dim soldeCompt As Double = 0
        Dim totalSoldeCompt As Double = 0
        Dim totalRealisation As Double = 0
        Dim prcentCompt As Double
        Dim prcentTotal As Double
        Dim clause As String = ""
        Dim clause1 As String = ""
        Dim temp(2) As String
        Dim factSansMarche As Double = 0
        Dim mBailleur As Double = 0
        Dim prcentBailleur As Double = 0
        Dim realBailleur As Double = 0

        Try

            dtbdgetcompte.Rows.Clear()

            'On efface les donnees tamporaires de l'utilisateur
            query = "DELETE FROM tampon6 WHERE CodeUtils='" & SessionID & "'" 'CodeUtils='" & SessionID & "'"
            ExecuteNonQuery(query)

            'Requete Date
            If DateTime.Compare(dateconvert(dpFin.Text), dateconvert(dpDebut.Text)) >= 0 Then
                clause = "AND dateDebutPartition >='" & dateconvert(dpDebut.Text) & "' AND dateDebutPartition <='" & dateconvert(dpFin.Text) & "'"
            Else
                SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
            End If

            'sélection du Bailleur concerné
            If CmbRech.SelectedIndex <> -1 Then
                temp = CmbRech.Text.Split(" - ")
                clause1 = " AND codeConvention='" & temp(0).ToString & "' "
            End If

            'initialisation des variables
            Dim tDotBail As Double = 0
            Dim tRealBail As Double = 0
            Dim tReal As Double = 0
            Dim tSoldeBail As Double = 0
            Dim lg As Decimal = 0
            query = "SELECT codeBailleur, initialeBailleur from t_bailleur WHERE codeProjet = '" & ProjetEnCours & "'"
            Dim dtBailleur As DataTable = ExcecuteSelectQuery(query)
            For Each rwBailleur In dtBailleur.Rows

                'Convention 
                Dim tDotConv As Double = 0
                Dim tRealConv As Double = 0
                Dim tSoldeConv As Double = 0
                query = "SELECT codeConvention FROM T_convention WHERE codeBailleur='" & rwBailleur("codeBailleur").ToString & "' " & clause1
                Dim dtConvention As DataTable = ExcecuteSelectQuery(query)
                For Each rwConvention In dtConvention.Rows
                    Dim drS = dtbdgetcompte.NewRow()
                    drS(0) = "x"
                    drS(1) = rwConvention("codeConvention").ToString
                    drS(2) = ""
                    drS(3) = ""
                    drS(4) = ""
                    drS(5) = ""
                    drS(6) = ""
                    dtbdgetcompte.Rows.Add(drS)
                    LgListBudgetCompte.DataSource = dtbdgetcompte
                    lg = lg + 1

                    'Activités
                    query = "SELECT codePartition,libellecourt from t_partition WHERE codeProjet = '" & ProjetEnCours & "'  AND CodeClassePartition=5 " & clause
                    Dim dtActivites As DataTable = ExcecuteSelectQuery(query)
                    For Each rwActivite In dtActivites.Rows
                        'Récupération des allocations des ***~ressources~***
                        query = "SELECT DISTINCT SIGFCOMPTE FROM T_CORRESPONDANCE_SIGFIP ORDER BY SIGFCOMPTE" 'On recherche toutes les correspondances des comptes SigFip
                        Dim dtCorrespondant As DataTable = ExcecuteSelectQuery(query)
                        If dtCorrespondant.Rows.Count > 0 Then 'On a pu retrouver des correspondances SigFip <=> Syscohada
                            For Each rwSigFip As DataRow In dtCorrespondant.Rows
                                'Recuperation des allocations en fonction des correspondances SigFip trouvé
                                query = "SELECT b.numeroComptable, b.PUNature, b.QteNature, sc.libelle_sc, b.RefBesoinPartition, r.montantBailleur, codeconvention FROM t_besoinPartition b, t_comp_sous_classe sc, t_repartitionParBailleur r WHERE b.numeroComptable=sc.code_sc and b.NumeroComptable IN (SELECT COMPTE FROM T_CORRESPONDANCE_SIGFIP WHERE SIGFCOMPTE='" & rwSigFip("SIGFCOMPTE").ToString & "') and b.CodePartition='" & rwActivite("CodePartition").ToString & "' AND b.refBesoinPartition=r.refBesoinPartition AND r.codeconvention='" & rwConvention("codeconvention").ToString & "' AND r.codeBailleur = '" & rwBailleur("codeBailleur").ToString & "'"
                                Dim dtAllocation = ExcecuteSelectQuery(query)
                                For Each rwAllocation As DataRow In dtAllocation.Rows 'dtAllocation
                                    Dim montbailleur As Double = 0
                                    Dim montMarche As Double = 0
                                    Dim Facture As Decimal = 0
                                    If rwAllocation("montantBailleur").ToString().Length <> 0 Then
                                        montbailleur = CDbl(rwAllocation("montantBailleur"))
                                    End If
                                    'On va recuperer les marches
                                    Dim dt4 As DataTable
                                    If opt = "Engagement" Then
                                        query = "SELECT sum(Montant_libellecourt) as Sum FROM t_acteng WHERE RefMarche IN (SELECT RefMarche FROM t_marche WHERE CodeConvention='" & rwConvention("codeconvention") & "') AND LibelleCourt='" & rwActivite("libellecourt") & "' and NumeroComptable='" & rwAllocation("numeroComptable") & "'" ' AND RefMarche IN (SELECT RefMarche FROM t_repartitionparbailleur WHERE RefBesoinPartition='" & rwAllocation("RefBesoinPartition").ToString() & "' and RefMarche<>'0')"
                                        dt4 = ExcecuteSelectQuery(query)
                                        For Each rw4 As DataRow In dt4.Rows 'dt4
                                            If Not IsDBNull(rw4("Sum")) Then
                                                montMarche = Round(CDec(rw4("Sum").ToString()), 0)
                                            Else
                                                montMarche = 0
                                            End If
                                        Next 'dt4
                                        'Recherche du montant des factures qui n'ont pas fait l'objet de marché
                                        query = "SELECT sum(Montant_act) as Sum, CODE_SC, LibelleCourt FROM T_COMP_ACTIVITE a, T_CategorieDepense c WHERE a.NumCateg=c.NumCateg and c.CodeConvention='" & rwConvention("codeconvention") & "' and a.CODE_SC='" & rwAllocation("numeroComptable").ToString() & "' and codepartition='" & rwActivite("codepartition") & "' and a.InitialeBailleur='" & rwBailleur("initialeBailleur").ToString & "' and a.Date_act >='" & dateconvert(dpDebut.Text) & "' and a.Date_act <= '" & dateconvert(dpFin.Text) & "' and NumeroMarche='' and a.code_Projet = '" & ProjetEnCours & "' group by code_sc, LibelleCourt"
                                        dt4 = ExcecuteSelectQuery(query)
                                        For Each rw4 As DataRow In dt4.Rows 'dt4
                                            If Not IsDBNull(rw4("Sum")) Then
                                                Facture = CDec(rw4("Sum").ToString)
                                            Else
                                                Facture = 0
                                            End If
                                        Next 'dt4
                                    Else
                                        'Recherche du montant de toutes les factures
                                        query = "SELECT sum(Montant_act) as Sum, CODE_SC, LibelleCourt FROM T_COMP_ACTIVITE a, T_CategorieDepense c WHERE a.NumCateg=c.NumCateg and c.CodeConvention='" & rwConvention("codeconvention") & "' and a.CODE_SC='" & rwAllocation("numeroComptable").ToString() & "' and codepartition='" & rwActivite("codepartition") & "' and a.InitialeBailleur='" & rwBailleur("initialeBailleur").ToString & "' and a.Date_act >='" & dateconvert(dpDebut.Text) & "' and a.Date_act <= '" & dateconvert(dpFin.Text) & "' and a.code_Projet = '" & ProjetEnCours & "' group by code_sc, LibelleCourt"
                                        dt4 = ExcecuteSelectQuery(query)
                                        For Each rw4 As DataRow In dt4.Rows 'dt4
                                            If Not IsDBNull(rw4("Sum")) Then
                                                Facture = CDec(rw4("Sum").ToString)
                                            Else
                                                Facture = 0
                                            End If
                                        Next 'dt4
                                    End If

                                    Try
                                        Dim LibelleSigFip As String = ExecuteScallar("SELECT SIGFLIBELLE FROM t_plansigfip WHERE SIGFCOMPTE='" & rwSigFip("SIGFCOMPTE") & "'").ToString()
                                        query = "insert into tampon6 values (NULL,'" & rwSigFip("SIGFCOMPTE").ToString & "','" & EnleverApost(LibelleSigFip) & "','" & montbailleur & "','" & rwAllocation("QteNature").ToString & "','" & rwBailleur("codeBailleur").ToString & "','" & rwConvention("codeconvention").ToString & "','" & ProjetEnCours & "','" & montMarche & "','" & facture & "','" & rwActivite("codepartition").ToString() & "','" & SessionID & "')"
                                        'query = "insert into tampon6 values (NULL,'" & rwSigFip("SIGFCOMPTE").ToString & "','" & EnleverApost(LibelleSigFip) & "','" & rwAllocation("PUNature").ToString & "','" & rwAllocation("QteNature").ToString & "','" & rwBailleur("codeBailleur").ToString & "','" & rwBailleur("initialeBailleur").ToString & "','" & ProjetEnCours & "','" & montMarche & "','" & Facture & "','" & rwConvention("codeconvention").ToString() & "','" & SessionID & "')"
                                        ExecuteNonQuery(query)
                                    Catch ex As Exception
                                        FailMsg(ex.ToString())
                                        InputBox(0, 0, query)
                                    End Try

                                Next 'dtAllocation
                            Next 'dtCorrespondance SigFip
                        End If
                    Next


                    Dim dotCompte As Double = 0
                    Dim montMarche1 As Double = 0
                    query = "SELECT numerosigfip, SUM(PUNature), libelleBesoin, SUM(Realisation), SUM(facture), count(numerosigfip), libellecourt1 FROM tampon6 WHERE libellecourt1 = '" & rwConvention("codeconvention").ToString & "' and CodeUtils='" & SessionID.ToString & "' Group by numerosigfip, libelleBesoin Order by numerosigfip"
                    Dim dtx As DataTable = ExcecuteSelectQuery(query)
                    For Each rwx In dtx.Rows

                        'dotation
                        dotCompte = CDbl(rwx(1).ToString)
                        dotCompte = Round(dotCompte)
                        tDotation = Round(tDotation + dotCompte)
                        tDotBail = Round(tDotBail + dotCompte)
                        tDotConv = Round(tDotConv + dotCompte)

                        'realisation
                        montMarche1 = Round(CDbl(rwx(3).ToString()) + CDbl(rwx(4).ToString()))
                        totalRealisation = Round(totalRealisation + montMarche1)
                        tRealBail = Round(tRealBail + montMarche1)
                        tRealConv = Round(tRealConv + montMarche1)
                        tReal = Round(tReal + montMarche1)

                        'Calcul solde du compte
                        soldeCompt = dotCompte - montMarche1

                        'Calcul total compte
                        totalSoldeCompt = totalSoldeCompt + soldeCompt

                        'Calcul pourcentage
                        If dotCompte <> 0 Then
                            prcentCompt = (montMarche1 / dotCompte) * 100
                        End If

                        'ajout des lignes dans le viewgrid
                        Dim drS1 = dtbdgetcompte.NewRow()
                        drS1(0) = "x"
                        drS1(1) = rwx(0).ToString
                        drS1(2) = MettreApost(rwx(2).ToString)
                        drS1(3) = AfficherMonnaie(dotCompte.ToString)
                        drS1(4) = AfficherMonnaie(tRealBail.ToString)
                        drS1(5) = AfficherMonnaie(soldeCompt)
                        drS1(6) = Round(prcentCompt, 2) & " % "
                        dtbdgetcompte.Rows.Add(drS1)
                        LgListBudgetCompte.DataSource = dtbdgetcompte
                        lg = lg + 1
                        dotCompte = 0
                        tRealBail = 0

                    Next

                    If CmbRech.SelectedIndex = -1 Then
                        tSoldeConv = tDotConv - tRealConv
                        If tDotConv <> 0 Then
                            prcentCompt = (tRealConv / tDotConv) * 100
                        End If
                        'ajout des lignes dans le viewgrid
                        NbTotal += 1
                        Dim drS2 = dtbdgetcompte.NewRow()
                        drS2(0) = "x"
                        drS2(1) = ""
                        drS2(2) = "TOTAL " & MettreApost(rwConvention(0).ToString)
                        drS2(3) = AfficherMonnaie(tDotConv.ToString)
                        drS2(4) = AfficherMonnaie(tRealConv.ToString)
                        drS2(5) = AfficherMonnaie(tSoldeConv.ToString)
                        drS2(6) = Round(prcentCompt, 2) & " % "
                        dtbdgetcompte.Rows.Add(drS2)
                        LgListBudgetCompte.DataSource = dtbdgetcompte
                        tDotConv = 0
                        tRealConv = 0
                        tSoldeConv = 0
                        prcentCompt = 0
                        lg = lg + 1
                    End If

                Next

            Next

            'Calcul Pourcentage total
            If tDotation <> 0 Then
                prcentTotal = (totalRealisation / tDotation) * 100
            End If

            If lg <> 0 Then

                Dim drS = dtbdgetcompte.NewRow()
                drS(0) = "x"
                If CmbRech.SelectedIndex = -1 Then
                    drS(1) = ""
                    drS(2) = "TOTAL PROJET " & ProjetEnCours
                Else
                    drS(1) = ""

                    drS(2) = "TOTAL " & MettreApost(SearchTable("NomBailleur", "T_bailleur", "initialeBailleur", temp(0).ToString))

                End If

                drS(3) = AfficherMonnaie(tDotation.ToString)
                drS(4) = AfficherMonnaie(totalRealisation.ToString)
                drS(5) = AfficherMonnaie(tDotation.ToString - totalRealisation.ToString)
                drS(6) = Round(prcentTotal, 2) & " % "
                dtbdgetcompte.Rows.Add(drS)
                LgListBudgetCompte.DataSource = dtbdgetcompte

                ViewBudgetCompte.Columns(0).Visible = False
                ViewBudgetCompte.OptionsView.ColumnAutoWidth = True
                ViewBudgetCompte.OptionsBehavior.AutoExpandAllGroups = True
                ViewBudgetCompte.VertScrollVisibility = True
                ViewBudgetCompte.HorzScrollVisibility = True
                ViewBudgetCompte.BestFitColumns()

                ViewBudgetCompte.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                ViewBudgetCompte.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                ViewBudgetCompte.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBudgetCompte.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                ColorRowGridAnal(ViewBudgetCompte, "[N° Compte]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
                ColorRowGridAnal(ViewBudgetCompte, "[Dotation]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)

            End If
            lblNbLign.Text = "Nbre de ligne(s) : " & lg.ToString

            ViewBudgetCompte.Columns(0).Visible = False
            ViewBudgetCompte.Columns("Libellé").MaxWidth = 800
            ViewBudgetCompte.OptionsView.ColumnAutoWidth = True
            ViewBudgetCompte.OptionsBehavior.AutoExpandAllGroups = True
            ViewBudgetCompte.VertScrollVisibility = True
            ViewBudgetCompte.HorzScrollVisibility = True
            ViewBudgetCompte.BestFitColumns()
            FinChargement()


        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub
    Private Sub BtAppercu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAppercu.Click
        If Not Access_Btn("BtnPrintSBSigfip") Then
            Exit Sub
        End If
        If ViewBudgetCompte.RowCount = 0 Then
            Exit Sub
        End If

        Dim temp() As String
        temp = CmbRech.Text.Split(" - ")

        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table

        Dim param As String = cmbBudget.Text.ToUpper()

        Select Case CmbCritere.Text
            'Par Composante
            Case "Par Projet"

                Dim Projet As New ReportDocument
                Dim Chemin As String = lineEtat & "\Budget\Par_Sigfip\"
                Dim DatSet = New DataSet

                Projet.Load(Chemin & "Par_Projet.rpt")
                'If CmbRech.Text = "" Then
                'Else
                'Projet.Load(Chemin & "ParProjet_critere.rpt")
                'End If

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = Projet.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                Projet.SetDataSource(DatSet)
                Projet.SetParameterValue("Codeprojet", ProjetEnCours)
                Projet.SetParameterValue("parametre", param.ToString)
                Projet.SetParameterValue("CodeUtils", SessionID)

                Try
                    Projet.SetParameterValue("Date1", dpDebut.Text)
                Catch ex As Exception
                End Try

                Try
                    Projet.SetParameterValue("Date2", dpFin.Text)
                Catch ex As Exception
                End Try

                FullScreenReport.FullView.ReportSource = Projet
                FullScreenReport.ShowDialog()
                Exit Select

                'Par Composante
            Case "Par Composante"

                Dim Composante As New ReportDocument
                Dim Chemin As String = lineEtat & "\Budget\Par_Sigfip\"
                Dim DatSet = New DataSet

                If CmbRech.Text = "" Then
                    Composante.Load(Chemin & "ParComposante.rpt")
                Else
                    Composante.Load(Chemin & "ParComposante_critere.rpt")
                End If

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = Composante.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                Composante.SetDataSource(DatSet)
                Composante.SetParameterValue("Codeprojet", ProjetEnCours)
                If CmbRech.Text = "" Then
                Else
                    Composante.SetParameterValue("libellecourt", temp(0).ToString)
                End If
                Composante.SetParameterValue("parametre", param.ToString)
                Composante.SetParameterValue("CodeUtils", SessionID)

                Try
                    Composante.SetParameterValue("Date1", dpDebut.Text)
                Catch ex As Exception
                End Try

                Try
                    Composante.SetParameterValue("Date2", dpFin.Text)
                Catch ex As Exception
                End Try

                FullScreenReport.FullView.ReportSource = Composante
                FullScreenReport.ShowDialog()
                Exit Select

                'Par Sous composante
            Case "Par Sous Composante"

                Dim SousComposante As New ReportDocument
                Dim Chemin As String = lineEtat & "\Budget\Par_Sigfip\"
                Dim DatSet = New DataSet

                If CmbRech.Text = "" Then
                    SousComposante.Load(Chemin & "ParSousComposante.rpt")
                Else
                    SousComposante.Load(Chemin & "ParSousComposante_critere.rpt")
                End If

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = SousComposante.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                SousComposante.SetDataSource(DatSet)
                SousComposante.SetParameterValue("Codeprojet", ProjetEnCours)
                If CmbRech.Text = "" Then
                Else
                    SousComposante.SetParameterValue("libellecourt", temp(0).ToString)
                End If
                SousComposante.SetParameterValue("parametre", param.ToString)
                SousComposante.SetParameterValue("CodeUtils", SessionID)

                Try
                    SousComposante.SetParameterValue("Date1", dpDebut.Text)
                Catch ex As Exception
                End Try

                Try
                    SousComposante.SetParameterValue("Date2", dpFin.Text)
                Catch ex As Exception
                End Try

                FullScreenReport.FullView.ReportSource = SousComposante
                FullScreenReport.ShowDialog()
                Exit Select

                'Par Activité
            Case "Par Activité"

                Dim activite As New ReportDocument
                Dim Chemin As String = lineEtat & "\Budget\Par_Sigfip\"
                Dim DatSet = New DataSet

                If CmbRech.Text = "" Then
                    activite.Load(Chemin & "Activites.rpt")
                Else
                    activite.Load(Chemin & "Activites_critere.rpt")
                End If

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = activite.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                activite.SetDataSource(DatSet)
                activite.SetParameterValue("Codeprojet", ProjetEnCours)
                If CmbRech.Text = "" Then
                Else
                    activite.SetParameterValue("libellecourt", temp(0).ToString)
                End If
                activite.SetParameterValue("parametre", param.ToString)
                activite.SetParameterValue("CodeUtils", SessionID)

                Try
                    activite.SetParameterValue("Date1", dpDebut.Text)
                Catch ex As Exception
                End Try

                Try
                    activite.SetParameterValue("Date2", dpFin.Text)
                Catch ex As Exception
                End Try

                FullScreenReport.FullView.ReportSource = activite
                FullScreenReport.ShowDialog()
                Exit Select

            Case "Par Bailleur"

                Dim bailleur As New ReportDocument
                Dim Chemin As String = lineEtat & "\Budget\Par_Sigfip\"
                Dim DatSet = New DataSet

                If CmbRech.Text = "" Then
                    bailleur.Load(Chemin & "Par_Bailleur.rpt")
                Else
                    bailleur.Load(Chemin & "Par_Bailleur_critere.rpt")
                End If

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = bailleur.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                bailleur.SetDataSource(DatSet)
                bailleur.SetParameterValue("Codeprojet", ProjetEnCours)
                If CmbRech.Text = "" Then
                Else
                    bailleur.SetParameterValue("bailleur", temp(0).ToString)
                End If
                bailleur.SetParameterValue("parametre", param.ToString)
                bailleur.SetParameterValue("CodeUtils", SessionID)

                Try
                    bailleur.SetParameterValue("Date1", dpDebut.Text)
                Catch ex As Exception
                End Try

                Try
                    bailleur.SetParameterValue("Date2", dpFin.Text)
                Catch ex As Exception
                End Try

                FullScreenReport.FullView.ReportSource = bailleur
                FullScreenReport.ShowDialog()

                Exit Select

            Case "Par Convention"

                Dim convention As New ReportDocument
                Dim Chemin As String = lineEtat & "\Budget\Par_Sigfip\"
                Dim DatSet = New DataSet

                If CmbRech.Text = "" Then
                    convention.Load(Chemin & "Par_Convention.rpt")
                Else
                    convention.Load(Chemin & "Par_Convention_critere.rpt")
                End If

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = convention.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                convention.SetDataSource(DatSet)
                convention.SetParameterValue("Codeprojet", ProjetEnCours)
                If CmbRech.Text = "" Then
                Else
                    convention.SetParameterValue("Convention", temp(0).ToString)
                End If
                convention.SetParameterValue("parametre", param.ToString)
                convention.SetParameterValue("CodeUtils", SessionID)

                Try
                    convention.SetParameterValue("Date1", dpDebut.Text)
                Catch ex As Exception
                End Try

                Try
                    convention.SetParameterValue("Date2", dpFin.Text)
                Catch ex As Exception
                End Try

                FullScreenReport.FullView.ReportSource = convention
                FullScreenReport.ShowDialog()

                Exit Select

        End Select
    End Sub

    Private Sub CmbRech_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbRech.SelectedIndexChanged
        If CmbCritere.SelectedIndex <> -1 Then

            Select Case CmbCritere.Text
            'Par Composante
                Case "Par Composante"
                    Label3.Text = "Sélectionner Composante"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeCompEng()
                    Else
                        DebutChargement()
                        RemplirListeCompEng("Dépense")
                        'RemplirListeCompDep()
                    End If
                    Exit Select

                Case "Par Sous Composante"
                    Label3.Text = "Sélectionner Sous Composante"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeSousCompEng()
                    Else
                        DebutChargement()
                        RemplirListeSousCompEng("Dépense")
                        'RemplirListeSousCompDep()
                    End If
                    Exit Select

                Case "Par Activité"
                    Label3.Text = "Sélectionner Activité"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeActivEng()
                    Else
                        DebutChargement()
                        RemplirListeActivEng("Dépense")
                        'RemplirListeActivDep()
                    End If
                    Exit Select

                Case "Par Bailleur"
                    Label3.Text = "Sélectionner Activité"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeBailEng()
                    Else
                        DebutChargement()
                        RemplirListeBailEng("Dépense")
                        'RemplirListeBailDep()
                    End If
                    Exit Select

                Case "Par Convention"
                    Label3.Text = "Sélectionner Convention"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeConvEng()
                    Else
                        DebutChargement()
                        'RemplirListeConvDep()
                        RemplirListeConvEng("Dépense")
                    End If
                    Exit Select

            End Select
        Else
            dtbdgetcompte.Rows.Clear()
            LgListBudgetCompte.DataSource = dtbdgetcompte
        End If
    End Sub

    Private Sub Suivibugetaire_Sigfip_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        'date
        dpDebut.Text = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
        dpFin.Text = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")
        dpDebut.Properties.MinValue = ExerciceComptable.Rows(0).Item("datedebut").ToString
        dpFin.Properties.MinValue = ExerciceComptable.Rows(0).Item("datefin").ToString
        dpDebut.Properties.MaxValue = ExerciceComptable.Rows(0).Item("datefin").ToString
        dpFin.Properties.MaxValue = ExerciceComptable.Rows(0).Item("datefin").ToString
        dateDebutpartition = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
        dateFinpartition = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")
        'sql = "select datedebut, datefin from T_COMP_EXERCICE where Etat<>'2' and encours='1'"
        'Dim dt As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt.Rows
        'Next
        'dt.Dispose()
        dtbdgetcompte.Columns.Clear()
        dtbdgetcompte.Columns.Add("CodeX", Type.GetType("System.String"))
        dtbdgetcompte.Columns.Add("N° Compte", Type.GetType("System.String"))
        dtbdgetcompte.Columns.Add("Libellé", Type.GetType("System.String"))
        dtbdgetcompte.Columns.Add("Dotation", Type.GetType("System.String"))
        dtbdgetcompte.Columns.Add("Réalisation", Type.GetType("System.String"))
        dtbdgetcompte.Columns.Add("Solde", Type.GetType("System.String"))
        dtbdgetcompte.Columns.Add("% Réalisation", Type.GetType("System.String"))
        dtbdgetcompte.Rows.Clear()
        'LgListBudgetCompte.DataSource = dtbdgetcompte
        ViewBudgetCompte.Columns.Clear()
        Dim Col1 = ViewBudgetCompte.Columns.AddField("CodeX")
        Dim Col2 = ViewBudgetCompte.Columns.AddField("N° Compte")
        Dim Col3 = ViewBudgetCompte.Columns.AddField("Libellé")
        Dim Col4 = ViewBudgetCompte.Columns.AddField("Dotation")
        Dim Col5 = ViewBudgetCompte.Columns.AddField("Réalisation")
        Dim Col6 = ViewBudgetCompte.Columns.AddField("Solde")
        Dim Col7 = ViewBudgetCompte.Columns.AddField("% Réalisation")
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In ViewBudgetCompte.Columns
            col.Visible = True
        Next
        Try
            ViewBudgetCompte.Columns(0).Visible = False
            ViewBudgetCompte.Columns("Libellé").MaxWidth = 800
            ViewBudgetCompte.OptionsView.ColumnAutoWidth = True
            ViewBudgetCompte.OptionsBehavior.AutoExpandAllGroups = True
            ViewBudgetCompte.VertScrollVisibility = True
            ViewBudgetCompte.HorzScrollVisibility = True
            ViewBudgetCompte.BestFitColumns()

            ViewBudgetCompte.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewBudgetCompte.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            ViewBudgetCompte.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewBudgetCompte.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewBudgetCompte.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewBudgetCompte.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ColorRowGridAnal(ViewBudgetCompte, "[N° Compte]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
            ColorRowGridAnal(ViewBudgetCompte, "[Dotation]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Suivibugetaire_Sigfip_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Date
        dpDebut.Text = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
        dpFin.Text = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")
        dpDebut.Properties.MinValue = ExerciceComptable.Rows(0).Item("datedebut").ToString
        dpFin.Properties.MinValue = ExerciceComptable.Rows(0).Item("datedebut").ToString
        dpDebut.Properties.MaxValue = ExerciceComptable.Rows(0).Item("datefin").ToString
        dpFin.Properties.MaxValue = ExerciceComptable.Rows(0).Item("datefin").ToString
        cmbBudget.Focus()
    End Sub
End Class