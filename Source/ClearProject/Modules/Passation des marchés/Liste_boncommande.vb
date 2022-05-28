Imports System.IO
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports Microsoft.Office.Interop
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraEditors.Repository

Public Class Liste_boncommande
    Dim drx As DataRow
    Dim dtListeBonCommande = New DataTable()
    Public AjoutModif As String = String.Empty
    Public j As Integer = 0

    Private Sub Liste_boncommande_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        LoadColonneBonCommande()
        RemplirDataGrid()

    End Sub

    Private Sub LoadColonneBonCommande()
        dtListeBonCommande.Columns.Clear()
        dtListeBonCommande.Columns.Add("Choix", Type.GetType("System.Boolean"))
        dtListeBonCommande.Columns.Add("N° Bon Commande", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("CodeFournisseur", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("TypeElabBC", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("NumeroDAO", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("RefLot", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("Intitulé du marché", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("Fournisseur", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("ConditionPaiement", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("DelaiLivraison", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("LieuLivraison", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("InstructionSpeciale", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("Montant", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("PcrtTVA", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("PcrtREMISE", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("LibelleAutreTaxe", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("PcrtAutreTaxe", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("Editeur", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("Date d'édition", Type.GetType("System.String"))

        GCListBoncommande.DataSource = dtListeBonCommande

        ViewBoncommande.Columns("N° Bon Commande").Width = 300
        ViewBoncommande.Columns("CodeFournisseur").Visible = False
        ViewBoncommande.Columns("TypeElabBC").Visible = False
        ViewBoncommande.Columns("NumeroDAO").Visible = False
        ViewBoncommande.Columns("RefLot").Visible = False
        ViewBoncommande.Columns("Intitulé du marché").Width = 350
        ViewBoncommande.Columns("Fournisseur").Width = 350
        ViewBoncommande.Columns("ConditionPaiement").Visible = False
        ViewBoncommande.Columns("DelaiLivraison").Visible = False
        ViewBoncommande.Columns("LieuLivraison").Visible = False
        ViewBoncommande.Columns("InstructionSpeciale").Visible = False
        ViewBoncommande.Columns("Montant").Width = 200
        ViewBoncommande.Columns("PcrtTVA").Visible = False
        ViewBoncommande.Columns("PcrtREMISE").Visible = False
        ViewBoncommande.Columns("LibelleAutreTaxe").Visible = False
        ViewBoncommande.Columns("PcrtAutreTaxe").Visible = False
        ViewBoncommande.Columns("Editeur").Width = 350
        ViewBoncommande.Columns("Date d'édition").Width = 220

        ViewBoncommande.Columns("Montant").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewBoncommande.Columns("Editeur").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        ViewBoncommande.Appearance.Row.Font = New Font("Times New Roman", 12, FontStyle.Regular)
        ColorRowGrid(ViewBoncommande, "[Code]='x'", Color.LightGray, "Times New Roman", 12, FontStyle.Regular, Color.Black)
    End Sub

    Private Sub RemplirDataGrid()

        query = "SELECT RefBonCommande,CodeFournisseur,TypeElabBC,NumeroDAO,RefLot,IntituleMarche,DateCommande,ConditionsPaiement,DelaiLivraison,LieuLivraison,InstructionSpeciale,PcrtTVA,PcrtRemise,AutreTaxe,PcrtAutreTaxe,MontantTotalTTC,EMP_ID FROM t_boncommande "
        query &= "where CodeProjet = '" & ProjetEnCours & "' AND EMP_ID = '" & cur_User.ToString() & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        Dim cptr As Integer = 0
        Dim NomEditeur As String = ""
        'Dim cpt As Decimal = 0
        Dim NewLine As DataTable = GCListBoncommande.DataSource
        NewLine.Rows.Clear()

        For Each rw As DataRow In dt.Rows
            query = "SELECT NomFournis FROM t_fournisseur WHERE CodeFournis = '" & rw("CodeFournisseur") & "'"
            Dim NomFournisseur As String = MettreApost(ExecuteScallar(query))

            query = "SELECT EMP_NOM, EMP_PRENOMS FROM t_grh_employe WHERE EMP_ID = '" & rw("EMP_ID") & "'"
            dt = ExcecuteSelectQuery(query)
            For Each rwNom As DataRow In dt.Rows
                NomEditeur = MettreApost(rwNom("EMP_NOM") & " " & rwNom("EMP_PRENOMS"))
            Next

            cptr += 1
            'cpt += 1
            Dim drS = NewLine.NewRow()
            'drS("Choix") = TabTrue(cpt - 1)
            drS("Choix") = TabTrue(0)
            drS("N° Bon Commande") = rw("RefBonCommande").ToString
            drS("CodeFournisseur") = rw("CodeFournisseur").ToString
            drS("Fournisseur") = NomFournisseur.ToString
            drS("TypeElabBC") = rw("TypeElabBC").ToString
            drS("NumeroDAO") = rw("NumeroDAO").ToString
            drS("RefLot") = rw("RefLot").ToString
            drS("Intitulé du marché") = MettreApost(rw("IntituleMarche").ToString)
            drS("Date d'édition") = CDate(rw("DateCommande")).ToString("dd/MM/yyyy")
            drS("ConditionPaiement") = rw("ConditionsPaiement")
            drS("DelaiLivraison") = MettreApost(rw("DelaiLivraison"))
            drS("LieuLivraison") = MettreApost(rw("LieuLivraison"))
            drS("InstructionSpeciale") = MettreApost(rw("InstructionSpeciale"))
            drS("Montant") = AfficherMonnaie(rw("MontantTotalTTC"))
            drS("PcrtTVA") = rw("PcrtTVA")
            drS("PcrtREMISE") = rw("PcrtRemise")
            drS("LibelleAutreTaxe") = MettreApost(rw("AutreTaxe"))
            drS("PcrtAutreTaxe") = rw("PcrtAutreTaxe")
            drS("Editeur") = NomEditeur.ToString
            NewLine.Rows.Add(drS)
        Next

        Dim edit As RepositoryItemCheckEdit = New RepositoryItemCheckEdit()
        edit.ValueChecked = True
        edit.ValueUnchecked = False
        ViewBoncommande.Columns("Choix").ColumnEdit = edit
        GCListBoncommande.RepositoryItems.Add(edit)
        ViewBoncommande.OptionsBehavior.Editable = True

        ViewBoncommande.Columns("N° Bon Commande").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("CodeFournisseur").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("TypeElabBC").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("NumeroDAO").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("RefLot").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Intitulé du marché").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Fournisseur").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("ConditionPaiement").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("DelaiLivraison").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("LieuLivraison").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("InstructionSpeciale").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Montant").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("PcrtTVA").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("PcrtREMISE").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("LibelleAutreTaxe").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("PcrtAutreTaxe").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Editeur").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Date d'édition").OptionsColumn.AllowEdit = False

        Dim nbre As Integer = cptr.ToString
        If nbre = 0 Then
            LblNombre.Text = "Aucun enregistrement"
        ElseIf nbre = 1 Then
            LblNombre.Text = nbre & " enregistrement"
        Else
            LblNombre.Text = nbre & " enregistrements"
        End If
    End Sub

    Private Sub Liste_boncommande_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub BtImprimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtImprimer.Click
        'Dialog_form(Etat_eng)
    End Sub

    Private Sub BtSupprimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSupprimer.Click
        Dim supp As Boolean = False

        If ConfirmMsg("Voulez-vous vraiment supprimer?") = DialogResult.Yes Then

            'suppression des données 
            For i = 0 To ViewBoncommande.RowCount - 1

                If CBool(ViewBoncommande.GetRowCellValue(i, "Choix")) = True Then

                    Dim NumBC As String = ""
                    Dim NumDAO As String = ""
                    Dim TypeElab As String = ""
                    NumBC = ViewBoncommande.GetRowCellValue(i, "N° Bon Commande").ToString
                    NumDAO = ViewBoncommande.GetRowCellValue(i, "NumeroDAO").ToString
                    TypeElab = ViewBoncommande.GetRowCellValue(i, "TypeElabBC").ToString

                    query = "delete from t_bc_listebesoins where RefBonCommande='" & NumBC & "'"
                    ExecuteNonQuery(query)

                    If TypeElab = "Sans Passation de Marché" Then
                        query = "delete from t_fournisseur where NumeroDAO='" & NumDAO & "'"
                        ExecuteNonQuery(query)
                    End If

                    query = "delete from t_boncommande where RefBonCommande='" & NumBC & "'"
                    ExecuteNonQuery(query)

                    supp = True

                    'query = "select count(*) from t_gf_demandepd where NumeroMarche='" & val.ToString & "'"
                    'nbre = ExecuteScallar(query)

                    'If nbre = 0 Then
                    '    query = "select refmarche from t_marche where NumeroMarche='" & val.ToString & "'"
                    '    Dim nummarche As String = ExecuteScallar(query)
                    '    DeleteRecords2("t_marche", "NumeroMarche", val)
                    '    DeleteRecords2("t_marchesigne", "NumeroMarche", val)
                    '    DeleteRecords2("t_acteng", "Refmarche", nummarche)
                    'Else
                    '    SuccesMsg("Ce marché ne peut être supprimé")
                    'End If
                End If

            Next

            If supp = False Then
                SuccesMsg("Veuillez cocher un bon de commande")
                BtActualiser_Click(sender, e)
            Else
                SuccesMsg("Suppression effectuée avec succès")
                'query = "select s.TypeMarche, s.NumeroMarche, m.DescriptionMarche, s.MontantHT, s.DateMarche, c.NOM_CPT, s.EtatMarche  from t_marchesigne s, t_marche m, t_comp_compte c  where s.refmarche=m.refmarche and s.attributaire=c.CODE_CPT and s.codeprojet='" & ProjetEnCours & "' ORDER BY length(s.NumeroMarche), s.NumeroMarche"
                'remplirDataGridimmo4(query, LgListBoncommande, LblNombre, ViewBoncommande)
            End If
        End If
    End Sub

    Private Sub BtModifier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtModifier.Click

        Dim bool As Boolean = False
        For i = 0 To ViewBoncommande.RowCount - 1

            If CBool(ViewBoncommande.GetRowCellValue(i, "Choix")) = True Then
                BonCommande.Size = New Point(1071, 786)
                AjoutModif = "Modifier"
                j = i
                Dialog_form(BonCommande)

                '        query = "select * from t_marche where NumeroMarche='" & ViewBoncommande.GetRowCellValue(i, "Numéro Marché").ToString & "'"
                '        Dim dt As DataTable = ExcecuteSelectQuery(query)
                '        For Each rw As DataRow In dt.Rows

                '            Modif_engagement.txtnbon.Text = ViewBoncommande.GetRowCellValue(i, "Numéro Marché").ToString
                '            Modif_engagement.TxtLotMarche.Text = MettreApost(rw(5).ToString)
                '            Modif_engagement.txtmontant.Text = rw(9).ToString
                '            Modif_engagement.txttypemarche.Text = rw(4).ToString
                '            Modif_engagement.CmbBaill.Text = rw(14).ToString
                '            Modif_engagement.CmbConv.Text = rw(15).ToString
                '            Modif_engagement.TxtPieceJointe.Text = ViewBoncommande.GetRowCellValue(i, "Numéro Marché").ToString & ".pdf"

                '            query = "select AbregeAO, LibelleAO from T_ProcAO where AbregeAO ='" & rw(10).ToString & "'"
                '            Dim dt5 As DataTable = ExcecuteSelectQuery(query)
                '            For Each rw5 As DataRow In dt5.Rows
                '                Modif_engagement.txtmethode.Text = rw5(0).ToString & " | " & MettreApost(rw5(1).ToString)
                '            Next

                '            query = "select DateMarche from t_marchesigne where NumeroMarche='" & ViewBoncommande.GetRowCellValue(i, "Numéro Marché").ToString & "'"
                '            Modif_engagement.DateMarche.Text = ExecuteScallar(query)

                '            'remplir les sous classe du plan comptable
                '            Modif_engagement.txtcompte.Properties.Items.Clear()
                '            query = "select * from T_COMP_SOUS_CLASSE where code_sc='" & rw(3).ToString & "' ORDER BY code_sc"
                '            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                '            For Each rw1 As DataRow In dt1.Rows
                '                Modif_engagement.txtcompte.Text = rw1(0).ToString & " | " & MettreApost(rw1(2).ToString)
                '            Next

                '            query = "select c.NumCateg, c.LibelleCateg from t_marchesigne m, t_CategorieDepense c where m.CodeCateg=c.CodeCateg and m.NumeroMarche ='" & ViewBoncommande.GetRowCellValue(i, "Numéro Marché").ToString & "'"
                '            Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                '            For Each rw2 As DataRow In dt2.Rows
                '                Modif_engagement.CmbCatDep.Text = rw2(0).ToString & " | " & MettreApost(rw2(1).ToString)
                '            Next

                '            Dim codefrs As String = ""
                '            query = "select Attributaire from t_marchesigne where NumeroMarche ='" & ViewBoncommande.GetRowCellValue(i, "Numéro Marché").ToString & "'"
                '            codefrs = ExecuteScallar(query)

                '            query = "select * from T_COMP_COMPTE where code_cpt='" & codefrs.ToString & "' and Code_Projet='" & ProjetEnCours & "' order by code_cpt"
                '            Dim dt4 As DataTable = ExcecuteSelectQuery(query)
                '            For Each rw4 As DataRow In dt4.Rows
                '                Modif_engagement.TxtFournisMarche.Text = rw4(0).ToString & " | " & MettreApost(rw4(4).ToString)
                '            Next

                '            dtdoc.Columns.Clear()
                '            dtdoc.Columns.Add("Activité", Type.GetType("System.String"))
                '            dtdoc.Columns.Add("Libellé de l'activité", Type.GetType("System.String"))
                '            dtdoc.Rows.Clear()
                '            query = "select p.libellecourt, p.libellepartition from t_acteng a, t_partition p where a.LibelleCourt = p.LibelleCourt and a.RefMarche ='" & rw(0).ToString & "'"
                '            Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                '            For Each rw3 As DataRow In dt3.Rows
                '                Dim drs = dtdoc.NewRow()
                '                drs(0) = rw3(0).ToString
                '                drs(1) = rw3(1).ToString
                '                dtdoc.Rows.Add(drs)
                '            Next

                '            Modif_engagement.LgListAct.DataSource = dtdoc
                '            Modif_engagement.Viewact.OptionsView.ColumnAutoWidth = True
                '            Modif_engagement.Viewact.OptionsBehavior.AutoExpandAllGroups = True
                '            Modif_engagement.Viewact.VertScrollVisibility = True
                '            Modif_engagement.Viewact.HorzScrollVisibility = True
                '            Modif_engagement.Viewact.BestFitColumns()
                '            Modif_engagement.Size = New Point(950, 575)
                '            Modif_engagement.ShowDialog()
                '        Next

                bool = True
            End If

        Next

        If bool = False Then
            SuccesMsg("Veuillez cocher un bon de commande")
        End If
    End Sub

    Private Sub BtAjouter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjouter.Click
        BonCommande.Size = New Point(1071, 786)
        AjoutModif = "Ajout"
        Dialog_form(BonCommande)
    End Sub

    Private Sub BtActualiser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtActualiser.Click
        LoadColonneBonCommande()
        RemplirDataGrid()
    End Sub

    Private Sub Checktous_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Checktous.CheckedChanged
        Try
            If (ViewBoncommande.RowCount > 0 And Checktous.Checked = True) Then
                Dim edit As RepositoryItemCheckEdit = New RepositoryItemCheckEdit()
                edit.ValueChecked = True
                edit.ValueUnchecked = False
                ViewBoncommande.Columns("Choix").ColumnEdit = edit
                GCListBoncommande.RepositoryItems.Add(edit)

                'For k As Integer = 0 To ViewBoncommande.RowCount - 1
                '    TabTrue(k) = Checktous.Checked
                'Next

                'If (Checktous.Checked = True) Then
                '    nbTab = ViewBoncommande.RowCount
                'Else
                '    nbTab = 0
                'End If

                'query = "select * from t_boncommande where CodeProjet='" & ProjetEnCours & "' ORDER BY length(CodeBon), CodeBon"
                'remplirDataGridimmo4(query, LgListBoncommande, LblNombre, ViewBoncommande)

            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Checktous_Click(sender As Object, e As EventArgs) Handles Checktous.Click
        'Try
        '    If (ViewBoncommande.RowCount > 0 And Checktous.Checked = True) Then
        '        Dim edit As RepositoryItemCheckEdit = New RepositoryItemCheckEdit()
        '        edit.ValueChecked = True
        '        ViewBoncommande.Columns("Choix").ColumnEdit = edit
        '        GCListBoncommande.RepositoryItems.Add(edit)
        '    End If
        'Catch ex As Exception
        '    FailMsg("Erreur : Information non disponible : " & ex.ToString())
        'End Try
    End Sub
End Class