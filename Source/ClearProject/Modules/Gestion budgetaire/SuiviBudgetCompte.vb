Imports System.Math
Imports Microsoft.Office.Interop
Imports System.IO
Imports System.Linq
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports MySql.Data.MySqlClient
Imports ClearProject.EvalProjet

Public Class SuiviBudgetCompte
    Dim dtbdgetcompte As New DataTable
    Dim DrX As DataRow
    Dim NbTotal As Decimal = 0
    Dim dateDebutpartition As Date = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
    Dim dateFinpartition As Date = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")
    Dim CodePartitions As String()
    Private Sub SuiviBudgetCompte_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        cmbBudget.Text = ""
        CmbCritere.Text = ""
        CmbRech.Text = ""
        lblNbLign.Text = "Nbre de ligne(s) : "
    End Sub

    Private Sub SuiviBudgetCompte_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        dpDebut.Text = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
        dpFin.Text = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")
        dpDebut.Properties.MinValue = ExerciceComptable.Rows(0).Item("datedebut").ToString
        dpFin.Properties.MinValue = ExerciceComptable.Rows(0).Item("datedebut").ToString
        dpDebut.Properties.MaxValue = ExerciceComptable.Rows(0).Item("datefin").ToString
        dpFin.Properties.MaxValue = ExerciceComptable.Rows(0).Item("datefin").ToString
        dateDebutpartition = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
        dateFinpartition = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")

        dtbdgetcompte.Columns.Clear()
        dtbdgetcompte.Columns.Add("CodeX", Type.GetType("System.String"))
        dtbdgetcompte.Columns.Add("N° Compte", Type.GetType("System.String"))
        dtbdgetcompte.Columns.Add("Libellé", Type.GetType("System.String"))
        dtbdgetcompte.Columns.Add("Dotation", Type.GetType("System.String"))
        dtbdgetcompte.Columns.Add("Réalisation", Type.GetType("System.String"))
        dtbdgetcompte.Columns.Add("Solde", Type.GetType("System.String"))
        dtbdgetcompte.Columns.Add("% Réalisation", Type.GetType("System.String"))
        dtbdgetcompte.Rows.Clear()

        'ViewBudgetCompte.Columns.AddRange({Col1, Col2, Col3, Col4, Col5, Col6, Col7})
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
        'sql = "select datedebut, datefin from T_COMP_EXERCICE where Etat<>'2' and encours='1'"
        'Dim dt As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt.Rows
        'Next
        'dt.Dispose()
    End Sub

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
                'Par Composante
                Case "Par Projet"
                    Label3.Text = "Sélectionner Composante"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeCompProj()
                        CmbRech.Enabled = False
                    Else
                        DebutChargement()
                        RemplirListeCompProj("Dépense")
                        CmbRech.Enabled = False
                    End If
                    Exit Select

                Case "Par Composante"
                    Label3.Text = "Sélectionner Composante"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeComp()
                        CmbRech.Enabled = True
                    Else
                        DebutChargement()
                        RemplirListeComp("Dépense")
                        CmbRech.Enabled = True
                    End If
                    RemplirComboPartition1(CmbRech, 1)
                    Exit Select

                    'Par Sous composante
                Case "Par Sous Composante"
                    Label3.Text = "Sélectionner Sous Composante"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeSousComp()
                        CmbRech.Enabled = True
                    Else
                        DebutChargement()
                        RemplirListeSousComp("Dépense")
                        CmbRech.Enabled = True
                    End If
                    RemplirComboPartition1(CmbRech, 2)
                    Exit Select

                    'Par Activité
                Case "Par Activité"
                    Label3.Text = "Sélectionner Activité"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeActiv()
                        CmbRech.Enabled = True
                    Else
                        DebutChargement()
                        RemplirListeActiv("Dépense")
                        CmbRech.Enabled = True
                    End If
                    LoadActivites(CmbRech)
                    Exit Select

                Case "Par Bailleur"
                    Label3.Text = "Sélectionner Activité"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeBail()
                        CmbRech.Enabled = True
                    Else
                        DebutChargement()
                        RemplirListeBail("Dépense")
                        CmbRech.Enabled = True
                    End If
                    RemplirComboBail1(CmbRech)
                    Exit Select

                Case "Par Convention"
                    Label3.Text = "Sélectionner Convention"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeConv()
                        CmbRech.Enabled = True
                    Else
                        DebutChargement()
                        RemplirListeConv("Dépense")
                        CmbRech.Enabled = True
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
    Private Sub LoadActivites(Combo As DevExpress.XtraEditors.ComboBoxEdit)
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
    Private Sub RemplirListeCompProj(Optional opt As String = "Engagement")

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
        query = "DELETE FROM Tampon WHERE CodeUtils='" & SessionID & "'" 'CodeUtils='" & SessionID & "'" 'CodeUtils='" & SessionID & "'"
        ExecuteNonQuery(query)
        Try

            'Composante
            Dim lg As Decimal = 0
            Dim tDotComp As Double = 0
            Dim tRealComp As Double = 0
            Dim tSoldeComp As Double = 0
            Dim montMarche As Double = 0

            query = "SELECT b.numeroComptable, b.PUNature, b.QteNature, sc.libelle_sc, b.RefBesoinPartition FROM t_besoinPartition b, t_comp_sous_classe sc, t_partition p WHERE b.numeroComptable=sc.code_sc and p.CodeProjet='" & ProjetEnCours & "' and b.CodePartition = p.CodePartition and p.dateDebutPartition>='" & dateconvert(dateDebutpartition) & "' AND p.dateDebutPartition <='" & dateconvert(dateFinpartition) & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw0 As DataRow In dt.Rows 'dt
                query = "insert into Tampon values (NULL,'" & rw0(0).ToString & "','" & rw0(3).ToString & "','" & rw0(1).ToString & "','" & rw0(2).ToString & "','','','" & ProjetEnCours & "','0','0','', '" & SessionID.ToString & "')"
                ExecuteNonQuery(query)

                If opt = "Engagement" Then
                    query = "SELECT sum(a.Montant_libellecourt), a.numerocomptable FROM `t_marche` m, t_marchesigne s, t_acteng a WHERE m.CodeProjet='" & ProjetEnCours & "' and m.refmarche=s.RefMarche and s.Refmarche=a.RefMarche and a.NumeroComptable='" & rw0(0).ToString & "' and STR_TO_DATE(s.DateMarche,'%d/%m/%Y') >='" & dateconvert(dpDebut.Text) & "' and STR_TO_DATE(s.DateMarche,'%d/%m/%Y') <= '" & dateconvert(dpFin.Text) & "' group by a.NumeroComptable"
                    Dim dt6 = ExcecuteSelectQuery(query)
                    For Each rw6 As DataRow In dt6.Rows 'dt6
                        query = "update tampon set Realisation=" & rw6(0).ToString & " where numeroComptable='" & rw6(1).ToString & "' and CodeUtils='" & SessionID.ToString & "'"
                        ExecuteNonQuery(query)
                    Next 'dt6

                    'Recherche du montant des factures qui n'ont pas fait l'objet de marché
                    query = "SELECT sum(Montant_act), CODE_SC FROM T_COMP_ACTIVITE WHERE code_projet='" & ProjetEnCours & "' and  CODE_SC='" & rw0(0).ToString & "'  and Date_act >='" & dateconvert(dpDebut.Text) & "' and Date_act <= '" & dateconvert(dpFin.Text) & "' and NumeroMarche='' group by code_sc"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt1.Rows 'dt1
                        query = "update tampon set facture='" & rw1(0).ToString & "' where numeroComptable='" & rw0(0).ToString & "' and CodeUtils='" & SessionID.ToString & "'"
                        ExecuteNonQuery(query)
                    Next 'dt1
                Else
                    'Recherche du montant de toutes les factures
                    query = "SELECT sum(Montant_act), CODE_SC FROM T_COMP_ACTIVITE WHERE code_projet='" & ProjetEnCours & "' and  CODE_SC='" & rw0(0).ToString & "'  and Date_act >='" & dateconvert(dpDebut.Text) & "' and Date_act <= '" & dateconvert(dpFin.Text) & "' group by code_sc"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt1.Rows 'dt1
                        query = "update tampon set facture='" & rw1(0).ToString & "' where numeroComptable='" & rw0(0).ToString & "' and CodeUtils='" & SessionID.ToString & "'"
                        ExecuteNonQuery(query)
                    Next 'dt1
                End If

            Next 'dt

            query = "SELECT numeroComptable, SUM(PUNature * QteNature), libelleBesoin, SUM(Realisation), SUM(facture), count(numeroComptable) FROM Tampon where CodeUtils='" & SessionID.ToString & "' Group by numeroComptable, libelleBesoin Order by numeroComptable"
            Dim dotCompte As Double = 0
            Dim dt7 As DataTable = ExcecuteSelectQuery(query)
            For Each rw7 As DataRow In dt7.Rows
                'dotation
                dotCompte = CDbl(rw7(1).ToString)
                dotCompte = Round(dotCompte, 2)
                tDotation = Round(tDotation + dotCompte)
                tDotComp = Round(tDotComp + dotCompte)

                'realisation
                montMarche = (rw7(3).ToString / rw7(5).ToString) + (rw7(4).ToString / rw7(5).ToString)
                tRealComp = Round(tRealComp + montMarche)
                totalRealisation = Round(totalRealisation + montMarche)

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
                drS(1) = rw7(0).ToString
                drS(2) = MettreApost(rw7(2).ToString)
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

    Sub RemplirListeComp(Optional opt As String = "Engagement")

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
        query = "DELETE FROM Tampon WHERE CodeUtils='" & SessionID & "'" 'CodeUtils='" & SessionID & "'" 'CodeUtils='" & SessionID & "'"
        ExecuteNonQuery(query)
        Try
            'Requete Date
            If DateTime.Compare(dateconvert(dateFinpartition), dateconvert(dateDebutpartition)) >= 0 Then
                clause = "AND dateDebutPartition>='" & dateconvert(dateDebutpartition) & "' AND dateFinpartition<='" & dateconvert(dateFinpartition) & "'"
            Else
                SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
            End If

            'sélection de la composante concernée
            If CmbRech.Text <> "" Or CmbRech.Text.Trim().Length <> 0 Then
                temp = CmbRech.Text.Split(" - ")
                clause1 = " AND libelleCourt LIKE '" & temp(0).ToString & "%' "
            End If

            'Composantes
            query = "SELECT libelleCourt, LibellePartition, CodePartition from t_partition WHERE codeProjet = '" & ProjetEnCours & "' AND { fn LENGTH(LibelleCourt) } = 1" & clause1
            Dim lg As Decimal = 0
            Dim tDotComp As Double = 0
            Dim tRealComp As Double = 0
            Dim tSoldeComp As Double = 0
            Dim montMarche As Double = 0
            Dim nbre As Double = 0
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw0 As DataRow In dt0.Rows 'dt0
                'Sous composantes
                query = "SELECT codePartition, libellecourt from t_partition WHERE codeProjet = '" & ProjetEnCours & "'  AND  CodePartitionMere='" & rw0(2).ToString & "'"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows 'dt1
                    'Activités
                    query = "SELECT codePartition, libellecourt from t_partition WHERE codeProjet = '" & ProjetEnCours & "'  AND CodeClassePartition = 5 AND CodePartitionMere = '" & rw1(0).ToString & "'" & clause
                    Dim dt2 = ExcecuteSelectQuery(query)
                    For Each rw2 As DataRow In dt2.Rows 'dt2
                        query = "SELECT b.numeroComptable, b.PUNature, b.QteNature, sc.libelle_sc, b.RefBesoinPartition FROM t_besoinPartition b, t_comp_sous_classe sc WHERE b.numeroComptable=sc.code_sc and b.CodePartition = '" & rw2(0).ToString & "'"
                        Dim dt3 = ExcecuteSelectQuery(query)
                        For Each rw3 As DataRow In dt3.Rows 'dt3

                            query = "insert into Tampon values (NULL,'" & rw3(0).ToString & "','" & rw3(3).ToString & "','" & rw3(1).ToString & "','" & rw3(2).ToString & "','" & rw0(0).ToString & "','" & rw2(1).ToString & "','" & ProjetEnCours & "','0','0','','" & SessionID.ToString & "')"
                            ExecuteNonQuery(query)

                            If opt = "Engagement" Then
                                'Selectionne les lots de chaque Marché en passant par le numDAO
                                query = "SELECT sum(a.Montant_libellecourt), mid(a.LibelleCourt,1,1), a.NumeroComptable FROM T_Marche m, T_MarcheSigne s, t_acteng a WHERE m.codeprojet='" & ProjetEnCours & "' and m.RefMarche=s.RefMarche and s.Refmarche=a.RefMarche and a.NumeroComptable='" & rw3(0).ToString & "' and STR_TO_DATE(s.DateMarche,'%d/%m/%Y') >='" & dateconvert(dpDebut.Text) & "' and STR_TO_DATE(s.DateMarche,'%d/%m/%Y') <= '" & dateconvert(dpFin.Text) & "' Group By a.NumeroComptable, mid(a.LibelleCourt,1,1)"
                                Dim dt6 = ExcecuteSelectQuery(query)
                                For Each rw6 As DataRow In dt6.Rows 'dt6
                                    query = "update tampon set Realisation='" & rw6(0).ToString & "' where numeroComptable='" & rw6(2).ToString & "' and libellecourt='" & rw6(1).ToString & "' and CodeUtils='" & SessionID.ToString & "'"
                                    ExecuteNonQuery(query)
                                Next 'dt6
                                'Recherche du montant des factures qui n'ont pas fait l'objet de marché
                                query = "SELECT sum(Montant_act), CODE_SC, mid(LibelleCourt,1,1) FROM T_COMP_ACTIVITE WHERE code_projet='" & ProjetEnCours & "' and CODE_SC='" & rw3(0).ToString & "' and Date_act >='" & dateconvert(dpDebut.Text) & "' and Date_act <= '" & dateconvert(dpFin.Text) & "' and NumeroMarche='' group by code_sc, mid(LibelleCourt,1,1)"
                                Dim dt4 As DataTable = ExcecuteSelectQuery(query)
                                For Each rw7 As DataRow In dt4.Rows 'dt4
                                    query = "update tampon set facture='" & rw7(0).ToString & "' where numeroComptable='" & rw7(1).ToString & "' and LibelleCourt='" & rw7(2).ToString & "' and CodeUtils='" & SessionID.ToString & "'"
                                    ExecuteNonQuery(query)
                                Next 'dt4
                            Else
                                'Recherche du montant de toutes les factures
                                query = "SELECT sum(Montant_act), CODE_SC, mid(LibelleCourt,1,1) FROM T_COMP_ACTIVITE WHERE code_projet='" & ProjetEnCours & "' and CODE_SC='" & rw3(0).ToString & "' and Date_act >='" & dateconvert(dpDebut.Text) & "' and Date_act <= '" & dateconvert(dpFin.Text) & "' group by code_sc, mid(LibelleCourt,1,1)"
                                Dim dt4 As DataTable = ExcecuteSelectQuery(query)
                                For Each rw7 As DataRow In dt4.Rows 'dt4
                                    query = "update tampon set facture='" & rw7(0).ToString & "' where numeroComptable='" & rw7(1).ToString & "' and LibelleCourt='" & rw7(2).ToString & "' and CodeUtils='" & SessionID.ToString & "'"
                                    ExecuteNonQuery(query)
                                Next 'dt4
                            End If


                        Next 'dt3
                    Next 'dt2
                Next 'dt1

                query = "SELECT p.libelleCourt, p.LibellePartition from t_partition p, tampon t WHERE p.codeProjet = '" & ProjetEnCours & "' AND t.LibelleCourt=p.LibelleCourt AND t.libellecourt = '" & rw0(0).ToString & "' and t.CodeUtils='" & SessionID.ToString & "' GROUP BY p.libelleCourt, p.LibellePartition"
                dt1 = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt1.Rows
                    'ajout des lignes dans le viewgrid
                    NbTotal += 1
                    Dim drS = dtbdgetcompte.NewRow()
                    drS(0) = "x"
                    drS(1) = rw("LibelleCourt").ToString
                    drS(2) = MettreApost(rw("LibellePartition"))
                    drS(3) = ""
                    drS(4) = ""
                    drS(5) = ""
                    drS(6) = ""
                    dtbdgetcompte.Rows.Add(drS)
                    LgListBudgetCompte.DataSource = dtbdgetcompte
                    lg = lg + 1
                Next

                query = "SELECT numeroComptable, SUM(PUNature * QteNature), libelleBesoin, SUM(Realisation), SUM(facture), count(*), LibelleCourt FROM Tampon WHERE libellecourt = '" & rw0(0).ToString & "' and CodeUtils='" & SessionID.ToString & "' Group by numeroComptable, libelleBesoin, LibelleCourt Order by numeroComptable"
                Dim dotCompte As Double = 0
                Dim montMarchereal As Double = 0
                dt1 = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt1.Rows

                    'dotation
                    dotCompte = CDbl(rw(1).ToString)
                    dotCompte = Round(dotCompte, 2)
                    tDotation = Round(tDotation + dotCompte)
                    tDotComp = Round(tDotComp + dotCompte)

                    'realisation
                    montMarchereal = (rw(3).ToString / rw(5).ToString) + (rw(4).ToString / rw(5).ToString)
                    tRealComp = Round(tRealComp + montMarchereal)
                    totalRealisation = Round(totalRealisation + montMarchereal)

                    'Calcul solde du compte
                    soldeCompt = Round(dotCompte - montMarchereal)

                    'Calcul total projet
                    totalSoldeCompt = Round(totalSoldeCompt + soldeCompt)

                    'Calcul pourcentage
                    If dotCompte <> 0 Then
                        prcentCompt = (montMarchereal / dotCompte) * 100
                    End If

                    'ajout des lignes dans le viewgrid
                    NbTotal += 1
                    Dim drS = dtbdgetcompte.NewRow()
                    drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                    drS(1) = rw(0).ToString
                    drS(2) = MettreApost(rw(2).ToString)
                    drS(3) = AfficherMonnaie(dotCompte.ToString)
                    drS(4) = AfficherMonnaie(montMarchereal.ToString)
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
                    query = "SELECT p.libelleCourt, p.LibellePartition from t_partition p, tampon t WHERE p.codeProjet = '" & ProjetEnCours & "' AND t.LibelleCourt=p.LibelleCourt AND t.libellecourt = '" & rw0(0).ToString & "' and t.CodeUtils='" & SessionID.ToString & "' GROUP BY p.libelleCourt, p.LibellePartition"
                    dt1 = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt1.Rows
                        tSoldeComp = tDotComp - tRealComp
                        If tDotComp <> 0 Then
                            prcentCompt = (tRealComp / tDotComp) * 100S
                        End If
                        'ajout des lignes dans le viewgrid
                        NbTotal += 1
                        Dim drS = dtbdgetcompte.NewRow()
                        drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                        drS(1) = ""
                        drS(2) = "TOTAL " & MettreApost(rw(1).ToString)
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
            Next 'dt0

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

    Sub RemplirListeSousComp(Optional opt As String = "Engagement")

        'Déclaration variable
        Dim tDotation As Double = 0
        Dim soldeCompt As Double = 0
        Dim totalSoldeCompt As Double = 0
        Dim totalRealisation As Double = 0
        Dim dot As Double = 0
        Dim real As Double = 0
        Dim prcentCompt As Double
        Dim prcentTotal As Double
        Dim clause As String = ""
        Dim clause1 As String = ""
        Dim temp(2) As String
        Dim factSansMarche As Double = 0

        'vider le datagrid et le listbox
        dtbdgetcompte.Rows.Clear()
        ListBox1.Items.Clear()

        Try

            'On efface les donnees tamporaires de l'utilisateur
            query = "DELETE FROM Tampon WHERE CodeUtils='" & SessionID & "'" 'CodeUtils='" & SessionID & "'" 'CodeUtils='" & SessionID & "'"
            ExecuteNonQuery(query)
            'Requete Date
            If DateTime.Compare(dateconvert(dateFinpartition), dateconvert(dateDebutpartition)) >= 0 Then
                clause = "and p.dateDebutPartition >='" & dateconvert(dateDebutpartition) & "' and p.dateDebutPartition <='" & dateconvert(dateFinpartition) & "'"
            Else
                SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
            End If

            'sélection de la composante concernée
            If CmbRech.Text <> "" Or CmbRech.Text.Trim().Length <> 0 Then
                temp = CmbRech.Text.Split(" - ")
                clause1 = " AND libelleCourt LIKE '" & temp(0).ToString & "%' "
            End If

            'Composante
            Dim lg As Decimal = 0
            Dim tDotComp As Double = 0
            Dim tRealComp As Double = 0
            Dim tSoldeComp As Double = 0

            query = "SELECT p.libelleCourt from t_partition p WHERE p.codeProjet = '" & ProjetEnCours & "' AND { fn LENGTH(LibelleCourt) } >= 5 ORDER BY p.libelleCourt"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                If ListBox1.FindString(Mid(rw(0).ToString, 1, 1)) <> -1 Then
                Else
                    ListBox1.Items.Add(Mid(rw(0).ToString, 1, 1))
                End If
            Next

            For i = 0 To ListBox1.Items.Count - 1
                query = "SELECT p.libelleCourt,p.LibellePartition from t_partition p " +
                    "WHERE p.codeProjet = '" & ProjetEnCours & "' AND p.libelleCourt='" & ListBox1.Items(i).ToString & "' GROUP BY p.libelleCourt, p.LibellePartition"
                dt = ExcecuteSelectQuery(query)
                For Each rw0 As DataRow In dt.Rows

                    Dim drS = dtbdgetcompte.NewRow()
                    If CmbRech.SelectedIndex = -1 Then
                        NbTotal += 1
                        drS(0) = "x"
                        drS(1) = rw0("LibelleCourt").ToString
                        drS(2) = MettreApost(rw0("LibellePartition"))
                        drS(3) = ""
                        drS(4) = ""
                        drS(5) = ""
                        drS(6) = ""
                        dtbdgetcompte.Rows.Add(drS)
                        LgListBudgetCompte.DataSource = dtbdgetcompte
                        lg = lg + 1
                    End If

                    'Sous Composante
                    Dim tDotSComp As Double = 0
                    Dim tRealSComp As Double = 0
                    Dim tSoldeSComp As Double = 0
                    Dim montMarche As Double = 0

                    If CmbRech.SelectedIndex <> -1 Then
                        query = "SELECT p.libelleCourt from t_partition p " +
                    " WHERE p.codeProjet = '" & ProjetEnCours & "' AND { fn LENGTH(LibelleCourt) }  = 2  AND p.libelleCourt LIKE '" & temp(0).ToString & "%'"
                    End If

                    If CmbRech.SelectedIndex = -1 Then
                        query = "SELECT p.libelleCourt,p.LibellePartition from t_partition p " +
                    " WHERE p.codeProjet = '" & ProjetEnCours & "' AND { fn LENGTH(LibelleCourt) }  = 2 AND p.libelleCourt LIKE '" & rw0(0).ToString & "%' "
                    End If

                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt1.Rows

                        'Activités
                        query = "SELECT p.codePartition, p.libelleCourt from t_partition p WHERE p.codeProjet = '" & ProjetEnCours & "'  AND { fn LENGTH(LibelleCourt) } >= 5 AND p.libelleCourt LIKE '" & rw1(0).ToString & "%'  " & clause
                        Dim dt2 = ExcecuteSelectQuery(query)
                        For Each rw2 As DataRow In dt2.Rows

                            query = "SELECT b.numeroComptable, b.PUNature, b.QteNature, sc.libelle_sc, b.RefBesoinPartition FROM t_besoinPartition b, t_comp_sous_classe sc WHERE b.numeroComptable=sc.code_sc and CodePartition = '" & rw2(0).ToString & "'"
                            Dim dt3 = ExcecuteSelectQuery(query)
                            For Each rw3 As DataRow In dt3.Rows

                                query = "insert into Tampon values (NULL,'" & rw3(0).ToString & "','" & rw3(3).ToString & "','" & rw3(1).ToString & "','" & rw3(2).ToString & "','" & rw0(0).ToString & "','" & rw1(0).ToString & "','" & ProjetEnCours & "','0','0','" & rw2(1).ToString & "', '" & SessionID.ToString & "')"
                                ExecuteNonQuery(query)

                                If opt = "Engagement" Then
                                    'Selectionne les lots de chaque Marché en passant par le numDAO
                                    query = "SELECT sum(a.Montant_libellecourt), mid(a.LibelleCourt,1,2), a.NumeroComptable FROM T_Marche m, T_MarcheSigne s, t_acteng a WHERE m.RefMarche=s.RefMarche and s.Refmarche=a.RefMarche and m.codeprojet='" & ProjetEnCours & "' and a.NumeroComptable='" & rw3(0).ToString & "' and STR_TO_DATE(s.DateMarche,'%d/%m/%Y') >='" & dateconvert(dpDebut.Text) & "' and STR_TO_DATE(s.DateMarche,'%d/%m/%Y') <= '" & dateconvert(dpFin.Text) & "' Group By a.NumeroComptable, mid(a.LibelleCourt,1,2)"
                                    Dim dt6 = ExcecuteSelectQuery(query)
                                    For Each rw6 As DataRow In dt6.Rows 'dt6
                                        query = "update tampon set Realisation='" & rw6(0).ToString & "' where numeroComptable='" & rw6(2).ToString & "' and libellecourt1='" & rw6(1).ToString & "' and CodeUtils='" & SessionID.ToString & "'"
                                        ExecuteNonQuery(query)
                                    Next 'dt6
                                    'Recherche du montant des factures qui n'ont pas fait l'objet de marché
                                    query = "SELECT sum(Montant_act), CODE_SC, mid(LibelleCourt,1,2) FROM T_COMP_ACTIVITE WHERE code_projet='" & ProjetEnCours & "' and CODE_SC='" & rw3(0).ToString & "' and Date_act >='" & dateconvert(dpDebut.Text) & "' and Date_act <= '" & dateconvert(dpFin.Text) & "' and NumeroMarche='' group by code_sc, mid(LibelleCourt,1,2)"
                                    Dim dt4 As DataTable = ExcecuteSelectQuery(query)
                                    For Each rw7 As DataRow In dt4.Rows 'dt4
                                        query = "update tampon set facture='" & rw7(0).ToString & "' where numeroComptable='" & rw7(1).ToString & "' and LibelleCourt1='" & rw7(2).ToString & "' and CodeUtils='" & SessionID.ToString & "'"
                                        ExecuteNonQuery(query)
                                    Next 'dt4
                                Else
                                    'Recherche du montant de toutes les factures
                                    query = "SELECT sum(Montant_act), CODE_SC, mid(LibelleCourt,1,2) FROM T_COMP_ACTIVITE WHERE code_projet='" & ProjetEnCours & "' and CODE_SC='" & rw3(0).ToString & "' and Date_act >='" & dateconvert(dpDebut.Text) & "' and Date_act <= '" & dateconvert(dpFin.Text) & "' group by code_sc, mid(LibelleCourt,1,2)"
                                    Dim dt4 As DataTable = ExcecuteSelectQuery(query)
                                    For Each rw7 As DataRow In dt4.Rows 'dt4
                                        query = "update tampon set facture='" & rw7(0).ToString & "' where numeroComptable='" & rw7(1).ToString & "' and LibelleCourt1='" & rw7(2).ToString & "' and CodeUtils='" & SessionID.ToString & "'"
                                        ExecuteNonQuery(query)
                                    Next 'dt4
                                End If


                            Next
                        Next

                        query = "SELECT p.libelleCourt, p.LibellePartition from t_partition p, tampon t WHERE p.codeProjet = '" & ProjetEnCours & "' AND t.LibelleCourt1=p.LibelleCourt AND t.libellecourt1 = '" & rw1(0).ToString & "' and t.CodeUtils='" & SessionID.ToString & "' GROUP BY p.libelleCourt, p.LibellePartition"
                        dt2 = ExcecuteSelectQuery(query)
                        For Each rw2 As DataRow In dt2.Rows
                            'ajout des lignes dans le viewgrid
                            NbTotal += 1
                            drS = dtbdgetcompte.NewRow()
                            drS(0) = "x"
                            drS(1) = rw2(0).ToString
                            drS(2) = MettreApost(rw2(1))
                            drS(3) = ""
                            drS(4) = ""
                            drS(5) = ""
                            drS(6) = ""
                            dtbdgetcompte.Rows.Add(drS)
                            LgListBudgetCompte.DataSource = dtbdgetcompte
                            lg = lg + 1
                        Next

                        query = "SELECT numeroComptable, SUM(PUNature * QteNature), libelleBesoin, SUM(Realisation), SUM(facture), count(numeroComptable), LibelleCourt1 FROM Tampon WHERE libellecourt1 = '" & rw1(0).ToString & "' and CodeUtils='" & SessionID.ToString & "' Group by numeroComptable, libelleBesoin,libellecourt1 Order by numeroComptable"
                        dt2 = ExcecuteSelectQuery(query)
                        Dim dotCompte As Double = 0
                        For Each rw2 As DataRow In dt2.Rows
                            'dotation
                            dotCompte = CDbl(rw2(1).ToString)
                            dotCompte = Round(dotCompte, 2)
                            tDotation = tDotation + dotCompte
                            tDotation = Round(tDotation)
                            tDotComp = Round(tDotComp + dotCompte)
                            tDotSComp = Round(tDotSComp + dotCompte)
                            dot = dot + dotCompte
                            dot = Round(dot)

                            'realisation
                            montMarche = (rw2(3).ToString / rw2(5).ToString) + (rw2(4).ToString / rw2(5).ToString)
                            montMarche = Round(montMarche)
                            tRealComp = Round(tRealComp + montMarche)
                            totalRealisation = Round(totalRealisation + montMarche)
                            real = Round(real + montMarche)
                            tRealSComp = Round(tRealSComp + montMarche)
                            tRealComp = Round(tRealComp + montMarche)

                            'Calcul solde du compte
                            soldeCompt = dotCompte - montMarche

                            'Calcul total projet
                            totalSoldeCompt = totalSoldeCompt + soldeCompt

                            'Calcul pourcentage
                            If dotCompte <> 0 Then
                                prcentCompt = (montMarche / dotCompte) * 100
                            End If

                            'ajout des lignes dans le viewgrid
                            NbTotal += 1
                            drS = dtbdgetcompte.NewRow()
                            drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                            drS(1) = rw2(0).ToString
                            drS(2) = MettreApost(rw2(2))
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
                                drS(2) = "TOTAL " & MettreApost(SearchTable("LibellePartition ", "T_Partition", "LibelleCourt", temp(0).ToString))
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
                            query = "SELECT p.libelleCourt, p.LibellePartition from t_partition p, tampon t WHERE p.codeProjet = '" & ProjetEnCours & "' AND t.LibelleCourt1=p.LibelleCourt AND t.libellecourt1 = '" & rw1(0).ToString & "' and t.CodeUtils='" & SessionID.ToString & "' GROUP BY p.libelleCourt, p.LibellePartition"
                            dt2 = ExcecuteSelectQuery(query)
                            For Each rw2 As DataRow In dt2.Rows
                                If tDotSComp <> 0 Then
                                    prcentTotal = (tRealSComp / tDotSComp) * 100
                                End If
                                'ajout des lignes dans le viewgrid
                                NbTotal += 1
                                drS = dtbdgetcompte.NewRow()
                                drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                                drS(1) = ""
                                drS(2) = "TOTAL " & MettreApost(rw2(1))
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
                    Next

                    If CmbRech.SelectedIndex = -1 Then
                        query = "SELECT p.libelleCourt, p.LibellePartition from t_partition p, tampon t WHERE p.codeProjet = '" & ProjetEnCours & "' AND t.LibelleCourt=p.LibelleCourt AND t.libellecourt = '" & rw0(0).ToString & "' and t.CodeUtils='" & SessionID.ToString & "' GROUP BY p.libelleCourt, p.LibellePartition"
                        dt1 = ExcecuteSelectQuery(query)
                        For Each rw1 As DataRow In dt1.Rows
                            If dot <> 0 Then
                                prcentTotal = (real / dot) * 100
                            End If

                            'ajout des lignes dans le viewgrid
                            NbTotal += 1
                            drS = dtbdgetcompte.NewRow()
                            drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                            drS(1) = ""
                            drS(2) = "TOTAL COMPOSANTE " & MettreApost(rw1(1))
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
                Next
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

    Sub RemplirListeActiv(Optional opt As String = "Engagement")
        'Déclaration variable
        Dim tDotation As Double = 0
        Dim soldeCompt As Double = 0
        Dim totalSoldeCompt As Double = 0
        Dim totalRealisation As Double = 0
        Dim dot As Double = 0
        Dim real As Double = 0
        Dim prcentCompt As Double
        Dim prcentTotal As Double
        Dim clause As String = ""
        Dim clause1 As String = ""
        Dim temp(2) As String
        Dim factSansMarche As Double = 0

        'Try
        'On efface les donnees tamporaires de l'utilisateur
        query = "DELETE FROM Tampon WHERE CodeUtils='" & SessionID & "'" 'CodeUtils='" & SessionID & "'" 'CodeUtils='" & SessionID & "'"
        ExecuteNonQuery(query)
        dtbdgetcompte.Rows.Clear()
        ListBox1.Items.Clear()

        'Requete Date
        If DateTime.Compare(dateconvert(dateFinpartition), dateconvert(dateDebutpartition)) >= 0 Then
            clause = "AND dateDebutPartition >='" & dateconvert(dateDebutpartition) & "' AND dateDebutPartition <='" & dateconvert(dateFinpartition) & "'"
        Else
            SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
            Exit Sub
        End If

        'sélection de l'activite concernée
        If CmbRech.SelectedIndex <> -1 Then
            clause1 = "AND p.CodePartition='" & CodePartitions(CmbRech.SelectedIndex) & "'"
        End If

        'Composante
        Dim lg As Decimal = 0
        Dim tDotComp As Double = 0
        Dim tRealComp As Double = 0
        Dim tSoldeComp As Double = 0

        'Enregistrement des composantes qui ont des activités
        query = "SELECT p.libelleCourt from t_partition p WHERE p.codeProjet = '" & ProjetEnCours & "' AND { fn LENGTH(LibelleCourt) } = 5 ORDER BY p.libelleCourt " & clause & " " & clause1
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            If ListBox1.FindString(Mid(rw(0).ToString, 1, 1)) <> -1 Then
            Else
                ListBox1.Items.Add(Mid(rw(0).ToString, 1, 1))
            End If
        Next

        For i = 0 To ListBox1.Items.Count - 1
            query = "SELECT p.libelleCourt, p.LibellePartition, p.CodePartition from t_partition p WHERE p.codeProjet = '" & ProjetEnCours & "' AND p.libelleCourt='" & ListBox1.Items(i).ToString & "' GROUP BY p.libelleCourt, p.LibellePartition, p.CodePartition"
            Dim dt0 = ExcecuteSelectQuery(query)

            For Each rw0 As DataRow In dt0.Rows
                NbTotal += 1
                Dim drS = dtbdgetcompte.NewRow()
                If CmbRech.SelectedIndex = -1 Then
                    drS(0) = "x"
                    drS(1) = rw0(0).ToString
                    drS(2) = MettreApost(rw0(1))
                    drS(3) = ""
                    drS(4) = ""
                    drS(5) = ""
                    drS(6) = ""
                    dtbdgetcompte.Rows.Add(drS)
                    LgListBudgetCompte.DataSource = dtbdgetcompte
                    lg = lg + 1
                End If

                'Sous Composante
                Dim tDotSComp As Double = 0
                Dim tRealSComp As Double = 0
                Dim tSoldeSComp As Double = 0
                Dim montMarche As Double = 0
                If CmbRech.SelectedIndex <> -1 Then
                    query = "SELECT p.libelleCourt, p.LibellePartition, p.CodePartition from t_partition p WHERE p.codeProjet = '" & ProjetEnCours & "' AND { fn LENGTH(LibelleCourt) } >= 5  " & clause1
                End If
                If CmbRech.SelectedIndex = -1 Then
                    query = "SELECT libelleCourt, LibellePartition, CodePartition from t_partition WHERE codeProjet = '" & ProjetEnCours & "' AND codeclassepartition= 2 AND CodePartitionMere = '" & rw0(2).ToString & "'"
                End If

                Dim dt1 = ExcecuteSelectQuery(query)

                For Each rw1 As DataRow In dt1.Rows
                    If CmbRech.SelectedIndex = -1 Then
                        NbTotal += 1
                        drS = dtbdgetcompte.NewRow()
                        drS(0) = "x"
                        drS(1) = rw1(0).ToString
                        drS(2) = MettreApost(rw1(1))
                        drS(3) = ""
                        drS(4) = ""
                        drS(5) = ""
                        drS(6) = ""
                        dtbdgetcompte.Rows.Add(drS)
                        LgListBudgetCompte.DataSource = dtbdgetcompte
                        lg = lg + 1
                    End If
                    'Activités
                    If CmbRech.SelectedIndex = -1 Then
                        query = "SELECT codePartition, libelleCourt from t_partition" +
                            " WHERE codeProjet = '" & ProjetEnCours & "'  AND { fn LENGTH(LibelleCourt) } >= 5 AND CodePartitionMere = '" & rw1(2).ToString & "'  " & clause
                    Else
                        query = "SELECT codePartition, libelleCourt from t_partition" +
                           " WHERE codeProjet = '" & ProjetEnCours & "'  AND { fn LENGTH(LibelleCourt) } >= 5 AND CodePartition= '" & CodePartitions(CmbRech.SelectedIndex) & "'  " & clause
                    End If
                    Dim dtActivite = ExcecuteSelectQuery(query)

                    For Each rwActivite As DataRow In dtActivite.Rows
                        query = "SELECT b.numeroComptable, b.PUNature, b.QteNature, sc.libelle_sc, b.RefBesoinPartition FROM t_besoinPartition b, t_comp_sous_classe sc WHERE b.numeroComptable=sc.code_sc and CodePartition = '" & rwActivite(0).ToString & "'"
                        Dim dt3 = ExcecuteSelectQuery(query)
                        For Each rw3 As DataRow In dt3.Rows

                            query = "insert into Tampon values (NULL,'" & rw3(0).ToString & "','" & rw3(3).ToString & "','" & rw3(1).ToString & "','" & rw3(2).ToString & "','" & rw0(0).ToString & "','" & rw1(0).ToString & "','" & ProjetEnCours & "','0','0','" & rwActivite(1).ToString & "', '" & SessionID.ToString & "')"
                            ExecuteNonQuery(query)

                            If opt = "Engagement" Then
                                'Selectionne les lots de chaque Marché en passant par le numDAO
                                query = "SELECT sum(a.Montant_libellecourt), a.LibelleCourt, a.NumeroComptable FROM T_Marche m, T_MarcheSigne s, t_acteng a WHERE m.RefMarche=s.RefMarche and s.Refmarche=a.RefMarche and a.NumeroComptable='" & rw3(0).ToString & "' and m.codeprojet='" & ProjetEnCours & "' and STR_TO_DATE(s.DateMarche,'%d/%m/%Y') >='" & dateconvert(dpDebut.Text) & "' and STR_TO_DATE(s.DateMarche,'%d/%m/%Y') <= '" & dateconvert(dpFin.Text) & "' Group By a.NumeroComptable, a.LibelleCourt"
                                Dim dt6 = ExcecuteSelectQuery(query)
                                For Each rw6 As DataRow In dt6.Rows 'dt6
                                    query = "update tampon set Realisation='" & rw6(0).ToString & "' where numeroComptable='" & rw6(2).ToString & "' and libellecourt2='" & rw6(1).ToString & "' and CodeUtils='" & SessionID.ToString & "'"
                                    ExecuteNonQuery(query)
                                Next 'dt6
                                'Recherche du montant des factures qui n'ont pas fait l'objet de marché
                                query = "SELECT sum(Montant_act), CODE_SC, LibelleCourt FROM T_COMP_ACTIVITE WHERE CODE_SC='" & rw3(0).ToString & "'  and Date_act >='" & dateconvert(dpDebut.Text) & "' and Date_act <= '" & dateconvert(dpFin.Text) & "' and NumeroMarche='' and code_projet='" & ProjetEnCours & "' group by code_sc, LibelleCourt"
                                Dim dt4 As DataTable = ExcecuteSelectQuery(query)
                                For Each rw4 As DataRow In dt4.Rows
                                    query = "update tampon set facture='" & rw4(0).ToString & "' where numeroComptable='" & rw4(1).ToString & "' and LibelleCourt2='" & rw4(2).ToString & "' and CodeUtils='" & SessionID.ToString & "'"
                                    ExecuteNonQuery(query)
                                Next
                            Else
                                'Recherche du montant de toutes les factures
                                query = "SELECT sum(Montant_act), CODE_SC, LibelleCourt FROM T_COMP_ACTIVITE WHERE CODE_SC='" & rw3(0).ToString & "'  and Date_act >='" & dateconvert(dpDebut.Text) & "' and Date_act <= '" & dateconvert(dpFin.Text) & "' and code_projet='" & ProjetEnCours & "' group by code_sc, LibelleCourt"
                                Dim dt4 As DataTable = ExcecuteSelectQuery(query)
                                For Each rw4 As DataRow In dt4.Rows
                                    query = "update tampon set facture='" & rw4(0).ToString & "' where numeroComptable='" & rw4(1).ToString & "' and LibelleCourt2='" & rw4(2).ToString & "' and CodeUtils='" & SessionID.ToString & "'"
                                    ExecuteNonQuery(query)
                                Next
                            End If

                        Next

                    Next

                    'liste des numeros comptables avec leur montant
                    query = "SELECT p.libelleCourt, p.LibellePartition from t_partition p, tampon t WHERE p.codeProjet = '" & ProjetEnCours & "' AND t.LibelleCourt2=p.LibelleCourt AND t.libellecourt1 = '" & rw1(0).ToString & "' and t.CodeUtils='" & SessionID.ToString & "' GROUP BY p.libelleCourt, p.LibellePartition"
                    dtActivite = ExcecuteSelectQuery(query)
                    For Each rwActivite As DataRow In dtActivite.Rows
                        'ajout des lignes dans le viewgrid
                        NbTotal += 1
                        drS = dtbdgetcompte.NewRow()
                        drS(0) = "x"
                        drS(1) = rwActivite(0).ToString
                        drS(2) = MettreApost(rwActivite(1))
                        drS(3) = ""
                        drS(4) = ""
                        drS(5) = ""
                        drS(6) = ""
                        dtbdgetcompte.Rows.Add(drS)
                        LgListBudgetCompte.DataSource = dtbdgetcompte
                        lg = lg + 1

                        query = "SELECT numeroComptable, SUM(PUNature * QteNature), libelleBesoin, SUM(Realisation), SUM(facture), count(numeroComptable), libellecourt2 FROM Tampon WHERE libellecourt2 = '" & rwActivite(0).ToString & "' and CodeUtils='" & SessionID.ToString & "' Group by numeroComptable, libelleBesoin, libellecourt2 Order by numeroComptable"
                        Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                        Dim dotCompte As Double = 0
                        For Each rw3 As DataRow In dt3.Rows
                            'dotation
                            dotCompte = CDbl(rw3(1).ToString)
                            dotCompte = Round(dotCompte)
                            tDotation = Round(tDotation + dotCompte)
                            tDotComp = Round(tDotComp + dotCompte)
                            tDotSComp = Round(tDotSComp + dotCompte)
                            dot = dot + dotCompte
                            dot = Round(dot)

                            'realisation
                            montMarche = (rw3(3).ToString / rw3(5).ToString) + (rw3(4).ToString / rw3(5).ToString)
                            montMarche = Round(montMarche, 0)
                            tRealComp = Round(tRealComp + montMarche)
                            totalRealisation = totalRealisation + montMarche
                            totalRealisation = Round(totalRealisation)
                            tRealSComp = Round(tRealSComp + montMarche)
                            real = Round(real + montMarche)

                            'Calcul solde du compte
                            soldeCompt = dotCompte - montMarche

                            'Calcul total compte
                            totalSoldeCompt = totalSoldeCompt + soldeCompt

                            'Calcul pourcentage
                            If dotCompte <> 0 Then
                                prcentCompt = (montMarche / dotCompte) * 100
                            End If

                            'ajout des lignes dans le viewgrid
                            NbTotal += 1
                            Dim drS1 = dtbdgetcompte.NewRow()
                            drS1(0) = "x"
                            drS1(1) = rw3(0).ToString
                            drS1(2) = MettreApost(rw3(2))
                            drS1(3) = AfficherMonnaie(dotCompte.ToString)
                            drS1(4) = AfficherMonnaie(montMarche.ToString)
                            drS1(5) = AfficherMonnaie(soldeCompt)
                            drS1(6) = Round(prcentCompt, 2) & " % "
                            dtbdgetcompte.Rows.Add(drS1)
                            LgListBudgetCompte.DataSource = dtbdgetcompte
                            lg = lg + 1
                        Next
                    Next

                    If CmbRech.SelectedIndex <> -1 Then

                        If tDotation <> 0 Then
                            prcentTotal = (totalRealisation / tDotation) * 100
                        End If

                        If lg <> 0 Then
                            'ajout des lignes dans le viewgrid
                            NbTotal += 1
                            drS = dtbdgetcompte.NewRow()
                            drS(0) = "x"
                            drS(1) = ""
                            drS(2) = "TOTAL " & MettreApost(SearchTable("LibellePartition ", "T_Partition", "CodePartition", CodePartitions(CmbRech.SelectedIndex)))
                            drS(3) = AfficherMonnaie(tDotation.ToString)
                            drS(4) = AfficherMonnaie(totalRealisation.ToString)
                            drS(5) = AfficherMonnaie(tDotation.ToString - totalRealisation.ToString)
                            drS(6) = Round(prcentTotal, 2) & " % "
                            dtbdgetcompte.Rows.Add(drS)
                            LgListBudgetCompte.DataSource = dtbdgetcompte
                        End If
                        lblNbLign.Text = "Nbre de ligne(s) : " & lg.ToString
                        FinChargement()
                        Exit Sub

                    End If

                    If CmbRech.SelectedIndex = -1 Then
                        query = "SELECT p.libelleCourt, p.LibellePartition from t_partition p, tampon t WHERE p.codeProjet = '" & ProjetEnCours & "' AND t.LibelleCourt1=p.LibelleCourt AND t.libellecourt1 = '" & rw1(0).ToString & "' and t.CodeUtils='" & SessionID.ToString & "' GROUP BY p.libelleCourt, p.LibellePartition"
                        Dim dt4 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw4 In dt4.Rows
                            If tDotSComp <> 0 Then
                                prcentTotal = (tRealSComp / tDotSComp) * 100
                            End If
                            'ajout des lignes dans le viewgrid
                            NbTotal += 1
                            drS = dtbdgetcompte.NewRow()
                            drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                            drS(1) = ""
                            drS(2) = "TOTAL " & MettreApost(rw4(1))
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

                Next

                If CmbRech.SelectedIndex = -1 Then
                    query = "SELECT p.libelleCourt, p.LibellePartition from t_partition p, tampon t WHERE p.codeProjet = '" & ProjetEnCours & "' AND t.LibelleCourt=p.LibelleCourt AND t.libellecourt = '" & rw0(0).ToString & "' and t.CodeUtils='" & SessionID.ToString & "' GROUP BY p.libelleCourt, p.LibellePartition"
                    Dim dt5 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw5 In dt5.Rows
                        If dot <> 0 Then
                            prcentTotal = (real / dot) * 100
                        End If
                        'ajout des lignes dans le viewgrid
                        NbTotal += 1
                        drS = dtbdgetcompte.NewRow()
                        drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                        drS(1) = ""
                        drS(2) = "TOTAL COMPOSANTE " & MettreApost(rw5(1))
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
            Next
        Next
        'Calcul Pourcentage total

        If tDotation <> 0 Then
            prcentTotal = (totalRealisation / tDotation) * 100
        End If

        If lg <> 0 Then
            Dim drS = dtbdgetcompte.NewRow()
            drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
            If CmbRech.SelectedIndex = -1 Then
                drS(1) = ""
                drS(2) = "TOTAL PROJET " & ProjetEnCours
            Else
                drS(1) = ""

                drS(2) = "TOTAL " & MettreApost(SearchTable("LibellePartition ", "T_Partition", "CodePartition", CodePartitions(CmbRech.SelectedIndex)))

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
        FinChargement()

        'Catch ex As Exception
        '    Failmsg("Erreur : Information non disponible : " & ex.ToString())

        'End Try
    End Sub

    Sub RemplirListeBail(Optional opt As String = "Engagement")

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
            'On efface les donnees tamporaires de l'utilisateur
            query = "DELETE FROM Tampon WHERE CodeUtils='" & SessionID & "'" 'CodeUtils='" & SessionID & "'" 'CodeUtils='" & SessionID & "'"
            ExecuteNonQuery(query)
            dtbdgetcompte.Rows.Clear()

            'Requete Date
            If DateTime.Compare(dateconvert(dateFinpartition), dateconvert(dateDebutpartition)) >= 0 Then
                clause = "AND dateDebutPartition >='" & dateconvert(dateDebutpartition) & "' AND dateDebutPartition <='" & dateconvert(dateFinpartition) & "'"
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
            query = "SELECT codeBailleur, initialeBailleur, NomBailleur from t_bailleur WHERE codeProjet = '" & ProjetEnCours & "'" & clause1 & " ORDER BY initialeBailleur"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows

                If CmbRech.SelectedIndex = -1 Then
                    'ajout des lignes dans le viewgrid
                    Dim drS = dtbdgetcompte.NewRow()
                    drS(0) = "x"
                    drS(1) = MettreApost(rw("initialeBailleur").ToString)
                    drS(2) = MettreApost(rw("NomBailleur"))
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
                query = " SELECT codeConvention FROM T_convention WHERE codeBailleur='" & rw(0).ToString & "' "
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw1 In dt1.Rows
                    Dim drS = dtbdgetcompte.NewRow()
                    drS(0) = "x"
                    drS(1) = rw1(0).ToString
                    drS(2) = ""
                    drS(3) = ""
                    drS(4) = ""
                    drS(5) = ""
                    drS(6) = ""
                    dtbdgetcompte.Rows.Add(drS)
                    LgListBudgetCompte.DataSource = dtbdgetcompte
                    lg = lg + 1

                    'Activités
                    query = "SELECT codePartition, LibelleCourt from t_partition" +
                    " WHERE codeProjet = '" & ProjetEnCours & "'  AND { fn LENGTH(LibelleCourt) } >= 5   " & clause
                    Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw2 In dt2.Rows

                        Dim montbaill As Double = 0
                        query = "SELECT b.numeroComptable, b.PUNature, b.QteNature, sc.libelle_sc, b.RefBesoinPartition, r.montantBailleur, codeconvention FROM t_besoinPartition b, t_comp_sous_classe sc, t_repartitionParBailleur r WHERE b.numeroComptable=sc.code_sc and b.CodePartition = '" & rw2(0).ToString & "' AND b.refBesoinPartition=r.refBesoinPartition AND r.codeconvention='" & rw1(0).ToString & "' AND r.codeBailleur = '" & rw(0).ToString & "'"
                        Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw3 In dt3.Rows

                            montbaill = IIf(IsDBNull(CDbl(rw3(5))), 0, CDbl(rw3(5)))
                            query = "insert into Tampon values (NULL,'" & rw3(0).ToString & "','" & rw3(3).ToString & "','" & montbaill.ToString & "','" & rw3(2).ToString & "','" & rw(0).ToString & "','" & rw(1).ToString & "','" & ProjetEnCours & "','0','0','" & rw3(6).ToString & "', '" & SessionID.ToString & "')"
                            ExecuteNonQuery(query)

                            If opt = "Engagement" Then
                                'Selectionne les lots de chaque Marché en passant par le numDAO
                                query = "SELECT sum(a.Montant_libellecourt), m.InitialeBailleur, a.NumeroComptable, m.CodeConvention FROM T_Marche m, T_MarcheSigne s, t_acteng a WHERE m.RefMarche=s.RefMarche and s.Refmarche=a.RefMarche and a.NumeroComptable='" & rw3(0).ToString & "' and m.CodeConvention='" & rw1(0).ToString & "' and STR_TO_DATE(s.DateMarche,'%d/%m/%Y') >='" & dateconvert(dpDebut.Text) & "' and STR_TO_DATE(s.DateMarche,'%d/%m/%Y') <= '" & dateconvert(dpFin.Text) & "' and m.codeProjet = '" & ProjetEnCours & "' Group By a.NumeroComptable, m.CodeConvention, m.InitialeBailleur"
                                Dim dt6 = ExcecuteSelectQuery(query)
                                For Each rw6 As DataRow In dt6.Rows 'dt6
                                    query = "update tampon set Realisation='" & rw6(0).ToString & "' where numeroComptable='" & rw6(2).ToString & "' and libellecourt2='" & rw6(3).ToString & "' and CodeUtils='" & SessionID.ToString & "'"
                                    ExecuteNonQuery(query)
                                Next 'dt6
                                'Recherche du montant des factures qui n'ont pas fait l'objet de marché
                                query = "SELECT sum(a.Montant_act), a.CODE_SC, a.InitialeBailleur, c.CodeConvention FROM T_COMP_ACTIVITE a, T_CategorieDepense c  WHERE a.NumCateg=c.NumCateg and c.CodeConvention='" & rw1(0).ToString & "'  and a.CODE_SC='" & rw3(0).ToString & "' and  a.Date_act>='" & dateconvert(dpDebut.Text) & "' and a.Date_act<='" & dateconvert(dpFin.Text) & "' and a.InitialeBailleur='" & rw(1).ToString & "' and a.NumeroMarche='' and a.code_Projet = '" & ProjetEnCours & "' group by a.code_sc, c.CodeConvention"
                                Dim dt7 As DataTable = ExcecuteSelectQuery(query)
                                For Each rw7 In dt7.Rows
                                    query = "update tampon set facture='" & rw7(0).ToString & "' where numeroComptable='" & rw7(1).ToString & "' and libellecourt2='" & rw7(3).ToString & "' and CodeUtils='" & SessionID.ToString & "'"
                                    ExecuteNonQuery(query)
                                Next
                            Else
                                'Recherche du montant de toutes les factures
                                query = "SELECT sum(a.Montant_act), a.CODE_SC, a.InitialeBailleur, c.CodeConvention FROM T_COMP_ACTIVITE a, T_CategorieDepense c  WHERE a.NumCateg=c.NumCateg and c.CodeConvention='" & rw1(0).ToString & "'  and a.CODE_SC='" & rw3(0).ToString & "' and  a.Date_act>='" & dateconvert(dpDebut.Text) & "' and a.Date_act<='" & dateconvert(dpFin.Text) & "' and a.InitialeBailleur='" & rw(1).ToString & "' and a.code_Projet = '" & ProjetEnCours & "' group by a.code_sc, c.CodeConvention"
                                Dim dt7 As DataTable = ExcecuteSelectQuery(query)
                                For Each rw7 In dt7.Rows
                                    query = "update tampon set facture='" & rw7(0).ToString & "' where numeroComptable='" & rw7(1).ToString & "' and libellecourt2='" & rw7(3).ToString & "' and CodeUtils='" & SessionID.ToString & "'"
                                    ExecuteNonQuery(query)
                                Next
                            End If

                        Next
                    Next


                    Dim dotCompte As Double = 0
                    Dim montMarche1 As Double = 0
                    query = "SELECT numeroComptable, SUM(PUNature), libelleBesoin, SUM(Realisation), SUM(facture), count(numeroComptable), libellecourt2 FROM Tampon WHERE libellecourt2 = '" & rw1(0).ToString & "' and CodeUtils='" & SessionID.ToString & "' Group by numeroComptable, libelleBesoin, libellecourt2 Order by numeroComptable"
                    Dim dt8 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw8 In dt8.Rows

                        'dotation
                        dotCompte = CDbl(rw8(1).ToString)
                        dotCompte = Round(dotCompte)
                        tDotation = Round(tDotation + dotCompte)
                        tDotBail = Round(tDotBail + dotCompte)
                        tDotConv = Round(tDotConv + dotCompte)

                        'realisation
                        montMarche1 = (rw8(3).ToString / rw8(5).ToString) + (rw8(4).ToString / rw8(5).ToString)
                        montMarche1 = Round(montMarche1)
                        totalRealisation = totalRealisation + montMarche1
                        totalRealisation = Round(totalRealisation)
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
                        drS1(1) = rw8(0).ToString
                        drS1(2) = MettreApost(rw8(2).ToString)
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
                        drS2(2) = "TOTAL " & MettreApost(rw1(0).ToString)
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
                    drS(2) = "TOTAL " & MettreApost(rw(1).ToString)
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

    Sub RemplirListeConv(Optional opt As String = "Engagement")

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
            'On efface les donnees tamporaires de l'utilisateur
            query = "DELETE FROM Tampon WHERE CodeUtils='" & SessionID & "'" 'CodeUtils='" & SessionID & "'" 'CodeUtils='" & SessionID & "'"
            ExecuteNonQuery(query)
            dtbdgetcompte.Rows.Clear()

            'Requete Date
            If DateTime.Compare(dateconvert(dateFinpartition), dateconvert(dateDebutpartition)) >= 0 Then
                clause = "AND dateDebutPartition>='" & dateconvert(dateDebutpartition) & "' AND dateFinpartition<='" & dateconvert(dateFinpartition) & "'"
            Else
                SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
                Exit Sub
            End If

            'sélection du Bailleur concerné
            If CmbRech.SelectedIndex <> -1 Then
                temp = CmbRech.Text.Split(" - ")
                clause1 = " and codeConvention='" & temp(0).ToString & "'"
            End If

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
                query = "SELECT codeConvention, codeBailleur FROM T_convention WHERE codeBailleur='" & rwBailleur(0).ToString & "' " & clause1
                Dim dtConvention As DataTable = ExcecuteSelectQuery(query)
                For Each rwConvention In dtConvention.Rows

                    Dim drS = dtbdgetcompte.NewRow()
                    drS(0) = "x"
                    drS(1) = rwConvention(0).ToString
                    drS(2) = ""
                    drS(3) = ""
                    drS(4) = ""
                    drS(5) = ""
                    drS(6) = ""
                    dtbdgetcompte.Rows.Add(drS)
                    LgListBudgetCompte.DataSource = dtbdgetcompte
                    lg = lg + 1

                    'Activités
                    query = "SELECT codePartition from t_partition" +
                    " WHERE codeProjet = '" & ProjetEnCours & "' AND { fn LENGTH(LibelleCourt) } >= 5 " & clause
                    Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw2 In dt2.Rows

                        Dim montbaill As Double = 0
                        query = "SELECT b.numeroComptable, b.PUNature, b.QteNature, sc.libelle_sc, b.RefBesoinPartition, r.montantBailleur, r.codeconvention FROM t_besoinPartition b, t_comp_sous_classe sc, t_repartitionParBailleur r WHERE b.numeroComptable=sc.code_sc and b.CodePartition = '" & rw2(0).ToString & "' AND b.refBesoinPartition=r.refBesoinPartition AND r.codeconvention='" & rwConvention(0).ToString & "' AND r.codeBailleur = '" & rwConvention(1).ToString & "'"
                        Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw3 In dt3.Rows

                            montbaill = IIf(IsDBNull(CDbl(rw3(5))), 0, CDbl(rw3(5)))
                            query = "insert into Tampon values (NULL,'" & rw3(0).ToString & "','" & rw3(3).ToString & "','" & montbaill.ToString & "','" & rw3(2).ToString & "','" & rwBailleur(0).ToString & "','" & rw3(6).ToString & "','" & ProjetEnCours & "','0','0','" & rw2(0).ToString & "', '" & SessionID.ToString & "')"
                            ExecuteNonQuery(query)

                            If opt = "Engagement" Then
                                'Selectionne les lots de chaque Marché en passant par le numDAO
                                query = "SELECT sum(a.Montant_libellecourt), m.CodeConvention, a.NumeroComptable FROM T_Marche m, T_MarcheSigne s, t_acteng a WHERE m.RefMarche=s.RefMarche and s.Refmarche=a.RefMarche and a.NumeroComptable='" & rw3(0).ToString & "' and STR_TO_DATE(s.DateMarche,'%d/%m/%Y') >='" & dateconvert(dpDebut.Text) & "' and STR_TO_DATE(s.DateMarche,'%d/%m/%Y') <= '" & dateconvert(dpFin.Text) & "' and m.codeProjet = '" & ProjetEnCours & "' Group By a.NumeroComptable, m.CodeConvention"
                                Dim dt6 = ExcecuteSelectQuery(query)
                                For Each rw6 As DataRow In dt6.Rows 'dt6
                                    query = "update tampon set Realisation='" & rw6(0).ToString & "' where numeroComptable='" & rw6(2).ToString & "' and libellecourt1='" & rw6(1).ToString & "' and CodeUtils='" & SessionID.ToString & "'"
                                    ExecuteNonQuery(query)
                                Next 'dt6
                                'Recherche du montant des factures qui n'ont pas fait l'objet de marché
                                query = "SELECT sum(a.Montant_act), a.CODE_SC, c.CodeConvention FROM T_COMP_ACTIVITE a, T_CategorieDepense c  WHERE a.NumCateg=c.NumCateg and c.CodeConvention='" & rwConvention(0).ToString & "'  and a.CODE_SC='" & rw3(0).ToString & "' and  a.Date_act>='" & dateconvert(dpDebut.Text) & "' and a.Date_act<='" & dateconvert(dpFin.Text) & "' and a.InitialeBailleur='" & rwBailleur(1).ToString & "' and a.NumeroMarche='' and a.code_Projet = '" & ProjetEnCours & "' group by a.code_sc,c.CodeConvention"
                                Dim dt7 As DataTable = ExcecuteSelectQuery(query)
                                For Each rw7 In dt7.Rows
                                    query = "update tampon set facture='" & rw7(0).ToString & "' where numeroComptable='" & rw7(1).ToString & "' and libellecourt1='" & rw7(2).ToString & "' and CodeUtils='" & SessionID.ToString & "'"
                                    ExecuteNonQuery(query)
                                Next
                            Else
                                'Recherche du montant de toutes les factures
                                query = "SELECT sum(a.Montant_act), a.CODE_SC, c.CodeConvention FROM T_COMP_ACTIVITE a, T_CategorieDepense c  WHERE a.NumCateg=c.NumCateg and c.CodeConvention='" & rwConvention(0).ToString & "'  and a.CODE_SC='" & rw3(0).ToString & "' and  a.Date_act>='" & dateconvert(dpDebut.Text) & "' and a.Date_act<='" & dateconvert(dpFin.Text) & "' and a.InitialeBailleur='" & rwBailleur(1).ToString & "' and a.code_Projet = '" & ProjetEnCours & "' group by a.code_sc,c.CodeConvention"
                                Dim dt7 As DataTable = ExcecuteSelectQuery(query)
                                For Each rw7 In dt7.Rows
                                    query = "update tampon set facture='" & rw7(0).ToString & "' where numeroComptable='" & rw7(1).ToString & "' and libellecourt1='" & rw7(2).ToString & "' and CodeUtils='" & SessionID.ToString & "'"
                                    ExecuteNonQuery(query)
                                Next
                            End If

                        Next
                    Next

                    Dim dotCompte As Double = 0
                    Dim montMarche1 As Double = 0
                    query = "SELECT numeroComptable, SUM(PUNature), libelleBesoin, SUM(Realisation), SUM(facture), count(numeroComptable), libellecourt1 FROM Tampon WHERE libellecourt1 = '" & rwConvention(0).ToString & "' and CodeUtils='" & SessionID.ToString & "' Group by numeroComptable, libelleBesoin, libellecourt1 Order by numeroComptable"
                    Dim dt8 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw8 In dt8.Rows

                        'dotation
                        dotCompte = CDbl(rw8(1).ToString)
                        dotCompte = Round(dotCompte)
                        tDotation = Round(tDotation + dotCompte)
                        tDotBail = Round(tDotBail + dotCompte)
                        tDotConv = Round(tDotConv + dotCompte)

                        'realisation
                        montMarche1 = (rw8(3).ToString / rw8(5).ToString) + (rw8(4).ToString / rw8(5).ToString)
                        montMarche1 = Round(montMarche1)
                        totalRealisation = totalRealisation + montMarche1
                        totalRealisation = Round(totalRealisation)
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
                        drS1(1) = rw8(0).ToString
                        drS1(2) = MettreApost(rw8(2).ToString)
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

                'If CmbRech.SelectedIndex <> -1 Then

                '    If tDotBail <> 0 Then
                '        prcentTotal = (totalRealisation / tDotBail) * 100
                '    End If

                '    If lg <> 0 Then

                '        'ajout des lignes dans le viewgrid
                '        NbTotal += 1
                '        Dim drS = dtbdgetcompte.NewRow()
                '        drS(0) = "x"
                '        drS(1) = ""
                '        drS(2) = "TOTAL " & MettreApost(SearchTable("NomBailleur", "T_bailleur", "initialeBailleur", temp(0).ToString))
                '        drS(3) = AfficherMonnaie(tDotBail.ToString)
                '        drS(4) = AfficherMonnaie(totalRealisation.ToString)
                '        drS(5) = AfficherMonnaie(tDotBail.ToString - totalRealisation.ToString)
                '        drS(6) = Round(prcentTotal, 2) & " % "
                '        dtbdgetcompte.Rows.Add(drS)
                '        LgListBudgetCompte.DataSource = dtbdgetcompte

                '    End If
                '    lblNbLign.Text = "Nbre de ligne(s) : " & lg.ToString
                '    FinChargement()
                '    Exit Sub
                'End If

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
                ViewBudgetCompte.Columns(1).Width = 70
                ViewBudgetCompte.Columns(2).Width = 262
                ViewBudgetCompte.Columns(3).Width = 150
                ViewBudgetCompte.Columns(4).Width = 150
                ViewBudgetCompte.Columns(5).Width = 150
                ViewBudgetCompte.Columns(6).Width = 110

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

    Private Sub CmbRech_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbRech.SelectedIndexChanged
        If CmbCritere.SelectedIndex <> -1 Then
            Select Case CmbCritere.Text
                'Par Composante
                Case "Par Composante"
                    Label3.Text = "Sélectionner Composante"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeComp()
                    Else
                        DebutChargement()
                        RemplirListeComp("Dépense")
                    End If
                    Exit Select

                Case "Par Sous Composante"
                    Label3.Text = "Sélectionner Sous Composante"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeSousComp()
                    Else
                        DebutChargement()
                        RemplirListeSousComp("Dépense")
                    End If
                    Exit Select

                Case "Par Activité"
                    Label3.Text = "Sélectionner Activité"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeActiv()
                    Else
                        DebutChargement()
                        RemplirListeActiv("Dépense")
                    End If
                    Exit Select

                Case "Par Bailleur"
                    Label3.Text = "Sélectionner Activité"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeBail()
                    Else
                        DebutChargement()
                        RemplirListeBail("Dépense")
                    End If
                    Exit Select

                Case "Par Convention"
                    Label3.Text = "Sélectionner Convention"
                    If cmbBudget.Text = "Engagements" Then
                        DebutChargement()
                        RemplirListeConv()
                    Else
                        DebutChargement()
                        RemplirListeConv("Dépense")
                    End If
                    Exit Select

            End Select
        Else
            dtbdgetcompte.Rows.Clear()
            LgListBudgetCompte.DataSource = dtbdgetcompte
        End If
    End Sub

    Private Sub LgListBudgetCompte_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LgListBudgetCompte.MouseUp
        If (ViewBudgetCompte.RowCount > 0) Then
            DrX = ViewBudgetCompte.GetDataRow(ViewBudgetCompte.FocusedRowHandle)
            Dim NCOMP = DrX(1).ToString
            ColorRowGrid(ViewBudgetCompte, "[N° Compte]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewBudgetCompte, "[N° Compte]='" & NCOMP & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
            ColorRowGridAnal(ViewBudgetCompte, "[N° Compte]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
            ColorRowGridAnal(ViewBudgetCompte, "[Dotation]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
            ColorRowGridAnal(ViewBudgetCompte, "[Solde]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
        End If
    End Sub

    Private Sub LgListBudgetCompte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LgListBudgetCompte.Click
        If (ViewBudgetCompte.RowCount > 0) Then
            DrX = ViewBudgetCompte.GetDataRow(ViewBudgetCompte.FocusedRowHandle)
            Dim NCOMP = DrX(1).ToString
            ColorRowGrid(ViewBudgetCompte, "[N° Compte]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewBudgetCompte, "[N° Compte]='" & NCOMP & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
            ColorRowGridAnal(ViewBudgetCompte, "[N° Compte]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
            ColorRowGridAnal(ViewBudgetCompte, "[Dotation]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
            ColorRowGridAnal(ViewBudgetCompte, "[Solde]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
        End If
    End Sub

    Private Sub BtAppercu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAppercu.Click
        If Not Access_Btn("BtnPrintSBNature") Then
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

            'Par Projet
            Case "Par Projet"

                Dim Composante As New ReportDocument
                Dim Chemin As String = lineEtat & "\Budget\Par_Nature\"
                Dim DatSet = New DataSet

                If CmbRech.Text = "" Then
                    Composante.Load(Chemin & "Par_Projet.rpt")
                Else
                    Composante.Load(Chemin & "Par_Projet_critere.rpt")
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
                Composante.SetParameterValue("CodeUtils", SessionID.ToString)
                If CmbRech.Text = "" Then
                Else
                    Composante.SetParameterValue("Compte", temp(0).ToString)
                End If
                Composante.SetParameterValue("parametre", param.ToString)

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

                'Par Composante
            Case "Par Composante"

                Dim Composante As New ReportDocument
                Dim Chemin As String = lineEtat & "\Budget\Par_Nature\"
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
                Composante.SetParameterValue("CodeUtils", SessionID.ToString)
                If CmbRech.Text = "" Then
                Else
                    Composante.SetParameterValue("libellecourt", temp(0).ToString)
                End If
                Composante.SetParameterValue("parametre", param.ToString)

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
                Dim Chemin As String = lineEtat & "\Budget\Par_Nature\"
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
                SousComposante.SetParameterValue("CodeUtils", SessionID.ToString)
                If CmbRech.Text = "" Then
                Else
                    SousComposante.SetParameterValue("libellecourt", temp(0).ToString)
                End If
                SousComposante.SetParameterValue("parametre", param.ToString)

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
                Dim Chemin As String = lineEtat & "\Budget\Par_Nature\"
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
                activite.SetParameterValue("CodeUtils", SessionID.ToString)
                If CmbRech.Text = "" Then
                Else
                    activite.SetParameterValue("libellecourt", temp(0).ToString)
                End If
                activite.SetParameterValue("parametre", param.ToString)

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
                Dim Chemin As String = lineEtat & "\Budget\Par_Nature\"
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
                bailleur.SetParameterValue("CodeUtils", SessionID.ToString)
                If CmbRech.Text = "" Then
                Else
                    bailleur.SetParameterValue("bailleur", temp(0).ToString)
                End If
                bailleur.SetParameterValue("parametre", param.ToString)

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
                Dim Chemin As String = lineEtat & "\Budget\Par_Nature\"
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
                convention.SetParameterValue("CodeUtils", SessionID.ToString)
                If CmbRech.Text = "" Then
                Else
                    convention.SetParameterValue("Convention", temp(0).ToString)
                End If
                convention.SetParameterValue("parametre", param.ToString)

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

    Private Sub dpFin_TextChanged(sender As Object, e As System.EventArgs) Handles dpFin.TextChanged, dpDebut.TextChanged
        On Error Resume Next
        If dateDebutpartition <> "" And dateFinpartition <> "" Then
            CmbCritere_SelectedIndexChanged(Me, e)
        Else
            dtbdgetcompte.Rows.Clear()
            LgListBudgetCompte.DataSource = dtbdgetcompte
        End If
    End Sub

    Private Sub SuiviBudgetCompte_Shown(sender As Object, e As EventArgs) Handles Me.Shown
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