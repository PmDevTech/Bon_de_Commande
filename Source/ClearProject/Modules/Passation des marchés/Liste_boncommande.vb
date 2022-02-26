Imports System.IO
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports Microsoft.Office.Interop
Imports System.Math
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Liste_boncommande
    Dim drx As DataRow
    Dim dtdoc = New DataTable()
    Dim dtboncommande = New DataTable()

    Private Sub RemplirBonCommande()
        Try
            dtboncommande.Columns.Clear()
            dtboncommande.Columns.Add("Code", Type.GetType("System.Boolean"))
            dtboncommande.Columns.Add("Date", Type.GetType("System.String"))
            dtboncommande.Columns.Add("Numéro", Type.GetType("System.String"))
            dtboncommande.Columns.Add("Description du marché", Type.GetType("System.String"))
            dtboncommande.Columns.Add("Demandeur", Type.GetType("System.String"))
            dtboncommande.Columns.Add("Fournisseur", Type.GetType("System.String"))
            dtboncommande.Columns.Add("Activité(s)", Type.GetType("System.String"))
            dtboncommande.Columns.Add("Montant", Type.GetType("System.String"))
            dtboncommande.Rows.Clear()

            Dim cptr As Integer = 0
            query = "select * from t_bon_commandes"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cptr += 1
                Dim drs = dtboncommande.NewRow()
                drs("Code") = TabTrue(cptr - 1)
                drs("Date") = rw(2).ToString
                drs("Numéro") = rw(3).ToString
                drs("Description du marché") = MettreApost(rw(4).ToString)
                drs("Demandeur") = rw(5).ToString
                drs("Fournisseur") = rw(6).ToString
                drs("Activité(s)") = MettreApostrophe(rw(7).ToString)
                drs("Montant") = AfficherMonnaie(Round(CDbl(rw(10).ToString)))
                dtboncommande.Rows.Add(drs)
            Next

            LgListBoncommande.DataSource = dtboncommande

            ViewBoncommande.Columns("Date").OptionsColumn.AllowEdit = False
            ViewBoncommande.Columns("Numéro").OptionsColumn.AllowEdit = False
            ViewBoncommande.Columns("Description du marché").OptionsColumn.AllowEdit = False
            ViewBoncommande.Columns("Demandeur").OptionsColumn.AllowEdit = False
            ViewBoncommande.Columns("Fournisseur").OptionsColumn.AllowEdit = False
            ViewBoncommande.Columns("Activité(s)").OptionsColumn.AllowEdit = False
            ViewBoncommande.Columns("Montant").OptionsColumn.AllowEdit = False

            ViewBoncommande.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
            ViewBoncommande.OptionsView.ColumnAutoWidth = True
            ViewBoncommande.OptionsBehavior.AutoExpandAllGroups = True
            ViewBoncommande.VertScrollVisibility = True
            ViewBoncommande.HorzScrollVisibility = True
            ViewBoncommande.BestFitColumns()

            ViewBoncommande.Columns("Date").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewBoncommande.Columns("Numéro").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            'grid.Columns("Description du marché").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewBoncommande.Columns("Demandeur").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewBoncommande.Columns("Fournisseur").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewBoncommande.Columns("Activité(s)").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewBoncommande.Columns("Montant").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewBoncommande.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Liste_boncommande_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        Try
            'query = "select * from t_boncommande where CodeProjet='" & ProjetEnCours & "'" ' ORDER BY length(CodeBon), CodeBon"
            'remplirDataGridBoncommande(query, LgListBoncommande, LblNombre, ViewBoncommande)
            RemplirBonCommande()
        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Liste_boncommande_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub BtImprimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtImprimer.Click
        Dialog_form(Etat_eng)
    End Sub

    Private Sub BtSupprimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSupprimer.Click
        Dim supp As Boolean = False

        If ConfirmMsg("Voulez-vous vraiment supprimer?") = DialogResult.Yes Then

            'suppression des données 
            For i = 0 To ViewBoncommande.RowCount - 1

                If CBool(ViewBoncommande.GetRowCellValue(i, "Code")) = True Then

                    Dim val As String
                    Dim nbre As Decimal = 0
                    val = ViewBoncommande.GetRowCellValue(i, "Numéro Marché").ToString

                    query = "select count(*) from t_gf_demandepd where NumeroMarche='" & val.ToString & "'"
                    nbre = ExecuteScallar(query)

                    If nbre = 0 Then
                        query = "select refmarche from t_marche where NumeroMarche='" & val.ToString & "'"
                        Dim nummarche As String = ExecuteScallar(query)
                        DeleteRecords2("t_marche", "NumeroMarche", val)
                        DeleteRecords2("t_marchesigne", "NumeroMarche", val)
                        DeleteRecords2("t_acteng", "Refmarche", nummarche)
                        supp = True
                    Else
                        SuccesMsg("Ce marché ne peut être supprimé")
                    End If
                End If

            Next

            If supp = False Then
                SuccesMsg("Veuillez cocher un marché/bon de commande")
            Else
                SuccesMsg("Suppression effectué avec succès")
                query = "select s.TypeMarche, s.NumeroMarche, m.DescriptionMarche, s.MontantHT, s.DateMarche, c.NOM_CPT, s.EtatMarche  from t_marchesigne s, t_marche m, t_comp_compte c  where s.refmarche=m.refmarche and s.attributaire=c.CODE_CPT and s.codeprojet='" & ProjetEnCours & "' ORDER BY length(s.NumeroMarche), s.NumeroMarche"
                'remplirDataGridimmo4(query, LgListBoncommande, LblNombre, ViewBoncommande)
            End If
        End If
    End Sub

    Private Sub BtModifier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtModifier.Click

        Dim bool As Boolean = False
        For i = 0 To ViewBoncommande.RowCount - 1

            If CBool(ViewBoncommande.GetRowCellValue(i, "Code")) = True Then

                query = "select * from t_marche where NumeroMarche='" & ViewBoncommande.GetRowCellValue(i, "Numéro Marché").ToString & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows

                    Modif_engagement.txtnbon.Text = ViewBoncommande.GetRowCellValue(i, "Numéro Marché").ToString
                    Modif_engagement.TxtLotMarche.Text = MettreApost(rw(5).ToString)
                    Modif_engagement.txtmontant.Text = rw(9).ToString
                    Modif_engagement.txttypemarche.Text = rw(4).ToString
                    Modif_engagement.CmbBaill.Text = rw(14).ToString
                    Modif_engagement.CmbConv.Text = rw(15).ToString
                    Modif_engagement.TxtPieceJointe.Text = ViewBoncommande.GetRowCellValue(i, "Numéro Marché").ToString & ".pdf"

                    query = "select AbregeAO, LibelleAO from T_ProcAO where AbregeAO ='" & rw(10).ToString & "'"
                    Dim dt5 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw5 As DataRow In dt5.Rows
                        Modif_engagement.txtmethode.Text = rw5(0).ToString & " | " & MettreApost(rw5(1).ToString)
                    Next

                   query= "select DateMarche from t_marchesigne where NumeroMarche='" & ViewBoncommande.GetRowCellValue(i, "Numéro Marché").ToString & "'"
                    Modif_engagement.DateMarche.Text = ExecuteScallar(query)

                    'remplir les sous classe du plan comptable
                    Modif_engagement.txtcompte.Properties.Items.Clear()
                    query = "select * from T_COMP_SOUS_CLASSE where code_sc='" & rw(3).ToString & "' ORDER BY code_sc"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt1.Rows
                        Modif_engagement.txtcompte.Text = rw1(0).ToString & " | " & MettreApost(rw1(2).ToString)
                    Next

                    query = "select c.NumCateg, c.LibelleCateg from t_marchesigne m, t_CategorieDepense c where m.CodeCateg=c.CodeCateg and m.NumeroMarche ='" & ViewBoncommande.GetRowCellValue(i, "Numéro Marché").ToString & "'"
                    Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw2 As DataRow In dt2.Rows
                        Modif_engagement.CmbCatDep.Text = rw2(0).ToString & " | " & MettreApost(rw2(1).ToString)
                    Next

                    Dim codefrs As String = ""
                   query= "select Attributaire from t_marchesigne where NumeroMarche ='" & ViewBoncommande.GetRowCellValue(i, "Numéro Marché").ToString & "'"
                    codefrs = ExecuteScallar(query)

                    query = "select * from T_COMP_COMPTE where code_cpt='" & codefrs.ToString & "' and Code_Projet='" & ProjetEnCours & "' order by code_cpt"
                    Dim dt4 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw4 As DataRow In dt4.Rows
                        Modif_engagement.TxtFournisMarche.Text = rw4(0).ToString & " | " & MettreApost(rw4(4).ToString)
                    Next

                    dtdoc.Columns.Clear()
                    dtdoc.Columns.Add("Activité", Type.GetType("System.String"))
                    dtdoc.Columns.Add("Libellé de l'activité", Type.GetType("System.String"))
                    dtdoc.Rows.Clear()
                    query = "select p.libellecourt, p.libellepartition from t_acteng a, t_partition p where a.LibelleCourt = p.LibelleCourt and a.RefMarche ='" & rw(0).ToString & "'"
                    Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw3 As DataRow In dt3.Rows
                        Dim drs = dtdoc.NewRow()
                        drs(0) = rw3(0).ToString
                        drs(1) = rw3(1).ToString
                        dtdoc.Rows.Add(drs)
                    Next

                    Modif_engagement.LgListAct.DataSource = dtdoc
                    Modif_engagement.Viewact.OptionsView.ColumnAutoWidth = True
                    Modif_engagement.Viewact.OptionsBehavior.AutoExpandAllGroups = True
                    Modif_engagement.Viewact.VertScrollVisibility = True
                    Modif_engagement.Viewact.HorzScrollVisibility = True
                    Modif_engagement.Viewact.BestFitColumns()
                    Modif_engagement.Size = New Point(950, 575)
                    Modif_engagement.ShowDialog()
                Next

                bool = True
            End If

        Next

        If bool = False Then
            SuccesMsg("Veuillez cocher un marché/bon de commande")
        End If
    End Sub

    Private Sub BtAjouter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjouter.Click
        BonCommande.Size = New Point(1010, 470)
        Dialog_form(BonCommande)
    End Sub

    Private Sub BtActualiser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtActualiser.Click
        Try
            query = "select * from t_boncommande where CodeProjet='" & ProjetEnCours & "'" ' ORDER BY length(CodeBon), CodeBon"
            'remplirDataGridBoncommande(query, LgListBoncommande, LblNombre, ViewBoncommande)
            RemplirBonCommande()
        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Checktous_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Checktous.CheckedChanged
        Try
            If (ViewBoncommande.RowCount > 0 And Checktous.Enabled = True) Then
                For k As Integer = 0 To ViewBoncommande.RowCount - 1
                    TabTrue(k) = Checktous.Checked
                Next

                If (Checktous.Checked = True) Then
                    nbTab = ViewBoncommande.RowCount
                Else
                    nbTab = 0
                End If

                query = "select * from t_boncommande where CodeProjet='" & ProjetEnCours & "' ORDER BY length(CodeBon), CodeBon"
                'remplirDataGridimmo4(query, LgListBoncommande, LblNombre, ViewBoncommande)

            End If
        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub
End Class