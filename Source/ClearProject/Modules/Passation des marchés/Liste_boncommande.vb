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

    Public Sub LoadColonneBonCommande()
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
        dtListeBonCommande.Columns.Add("Référence", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("Désignation", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("Quantité", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("Prix Unitaire", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("Montant Rabais", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("Ajustement", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("MontantBCHT", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("Montant", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("PcrtTVA", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("PcrtREMISE", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("LibelleAutreTaxe", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("PcrtAutreTaxe", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("Editeur", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("Date d'édition", Type.GetType("System.String"))
        dtListeBonCommande.Columns.Add("BonValider", Type.GetType("System.String"))

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
        ViewBoncommande.Columns("Référence").Visible = False
        ViewBoncommande.Columns("Désignation").Visible = False
        ViewBoncommande.Columns("Quantité").Visible = False
        ViewBoncommande.Columns("Prix Unitaire").Visible = False
        ViewBoncommande.Columns("Montant Rabais").Visible = False
        ViewBoncommande.Columns("Ajustement").Visible = False
        ViewBoncommande.Columns("MontantBCHT").Visible = False
        ViewBoncommande.Columns("Montant").Width = 200
        ViewBoncommande.Columns("PcrtTVA").Visible = False
        ViewBoncommande.Columns("PcrtREMISE").Visible = False
        ViewBoncommande.Columns("LibelleAutreTaxe").Visible = False
        ViewBoncommande.Columns("PcrtAutreTaxe").Visible = False
        ViewBoncommande.Columns("Editeur").Width = 350
        ViewBoncommande.Columns("Date d'édition").Width = 220
        ViewBoncommande.Columns("BonValider").Visible = False

        ViewBoncommande.Columns("Montant").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewBoncommande.Columns("Editeur").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        ViewBoncommande.Appearance.Row.Font = New Font("Times New Roman", 12, FontStyle.Regular)
        ColorRowGrid(ViewBoncommande, "[Code]='x'", Color.LightGray, "Times New Roman", 12, FontStyle.Regular, Color.Black)
    End Sub

    Public Sub RemplirDataGrid()

        query = "SELECT RefBonCommande,CodeFournisseur,TypeElabBC,NumeroDAO,RefLot,IntituleMarche,DateCommande,ConditionsPaiement,DelaiLivraison,LieuLivraison,InstructionSpeciale,RefArticle,Designation,Quantite,PrixUnitaire,MontantRabais,Ajustement,MontantBCHT,PcrtTVA,PcrtRemise,AutreTaxe,PcrtAutreTaxe,MontantTotalTTC,BonValider,EMP_ID FROM t_boncommande "
        query &= "where CodeProjet = '" & ProjetEnCours & "' AND EMP_ID = '" & cur_User & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        Dim cptr As Integer = 0
        Dim NomEditeur As String = ""
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
            drS("Référence") = MettreApost(rw("RefArticle"))
            drS("Désignation") = MettreApost(rw("Designation"))
            drS("Quantité") = rw("Quantite").ToString
            drS("Prix Unitaire") = rw("PrixUnitaire").ToString
            drS("Montant Rabais") = rw("MontantRabais").ToString
            drS("Ajustement") = rw("Ajustement").ToString
            drS("MontantBCHT") = rw("MontantBCHT")
            drS("Montant") = AfficherMonnaie(rw("MontantTotalTTC"))
            drS("PcrtTVA") = rw("PcrtTVA")
            drS("PcrtREMISE") = rw("PcrtRemise")
            drS("LibelleAutreTaxe") = MettreApost(rw("AutreTaxe"))
            drS("PcrtAutreTaxe") = rw("PcrtAutreTaxe")
            drS("Editeur") = NomEditeur.ToString
            drS("BonValider") = rw("BonValider").ToString
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
        ViewBoncommande.Columns("Référence").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Désignation").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Quantité").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Prix Unitaire").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Montant Rabais").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Ajustement").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("MontantBCHT").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Montant").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("PcrtTVA").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("PcrtREMISE").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("LibelleAutreTaxe").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("PcrtAutreTaxe").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Editeur").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Date d'édition").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("BonValider").OptionsColumn.AllowEdit = False

        Dim nbre As Integer = cptr
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

        If ViewBoncommande.RowCount > 0 Then

            Dim impr As Boolean = False

            For i = 0 To ViewBoncommande.RowCount - 1

                If CBool(ViewBoncommande.GetRowCellValue(i, "Choix")) = True Then

                    Dim reportfeuilletps As New ReportDocument
                    Dim crtableLogoninfos As New TableLogOnInfos
                    Dim crtableLogoninfo As New TableLogOnInfo
                    Dim crConnectionInfo As New ConnectionInfo
                    Dim CrTables As Tables
                    Dim CrTable As Table

                    'récupération du numéro du bon de commande
                    Dim NumBonCommande As String = ""
                    NumBonCommande = ViewBoncommande.GetRowCellValue(i, "N° Bon Commande")

                    'récupération du type de marché
                    Dim TypeMarche As String = ""
                    Dim NumDAO As String = ""
                    NumDAO = ViewBoncommande.GetRowCellValue(i, "NumeroDAO")
                    query = "SELECT TypeMarche FROM t_dao WHERE CodeProjet = '" & ProjetEnCours & "' AND NumeroDAO = '" & NumDAO & "'"
                    TypeMarche = ExecuteScallar(query)

                    DebutChargement(True, "Le traitement de votre demande est en cours...")
                    Dim Chemin As String = ""
                    If TypeMarche = "Fournitures" Or TypeMarche.ToLower.Contains("Service") Then
                        Chemin = lineEtat & "\Bon_Commande\Etat_BonCommande_Fournitures.rpt"
                    ElseIf TypeMarche = "Travaux" Then
                        Chemin = lineEtat & "\Bon_Commande\Etat_BonCommande_Travaux.rpt"
                    Else
                        Chemin = lineEtat & "\Bon_Commande\Etat_BonCommande.rpt"
                    End If

                    Dim DatSet = New DataSet
                    reportfeuilletps.Load(Chemin)

                    With crConnectionInfo
                        .ServerName = ODBCNAME
                        .DatabaseName = DB
                        .UserID = USERNAME
                        .Password = PWD
                    End With

                    CrTables = reportfeuilletps.Database.Tables
                    For Each CrTable In CrTables
                        crtableLogoninfo = CrTable.LogOnInfo
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo
                        CrTable.ApplyLogOnInfo(crtableLogoninfo)
                    Next

                    reportfeuilletps.SetDataSource(DatSet)
                    reportfeuilletps.SetParameterValue("NumBonCommande", NumBonCommande)
                    reportfeuilletps.SetParameterValue("CodeProjet", ProjetEnCours)
                    FullScreenReport.FullView.ReportSource = reportfeuilletps
                    FinChargement()
                    FullScreenReport.ShowDialog()

                    impr = True
                End If

            Next

            If impr = False Then
                SuccesMsg("Veuillez cocher un bon de commande")
            End If

        Else
            SuccesMsg("Veuillez élaborer un bon de commande")
        End If

    End Sub

    Private Sub BtSupprimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSupprimer.Click

        If ViewBoncommande.RowCount > 0 Then

            Dim supp As String = "FAUX"
            Dim VerifSupBon As String = ""
            For i = 0 To ViewBoncommande.RowCount - 1

                If CBool(ViewBoncommande.GetRowCellValue(i, "Choix")) = True Then

                    VerifSupBon = ViewBoncommande.GetRowCellValue(i, "BonValider")

                    If VerifSupBon.ToUpper = "OUI" Then
                        If ConfirmMsg("Voulez-vous vraiment supprimer ?") = DialogResult.Yes Then
                            Dim NumBC As String = ""
                            'Dim NumDAO As String = ""
                            Dim TypeElab As String = ""
                            Dim CodeFournisseur As String = ""
                            NumBC = ViewBoncommande.GetRowCellValue(i, "N° Bon Commande").ToString
                            'NumDAO = ViewBoncommande.GetRowCellValue(i, "NumeroDAO").ToString
                            TypeElab = ViewBoncommande.GetRowCellValue(i, "TypeElabBC").ToString
                            CodeFournisseur = ViewBoncommande.GetRowCellValue(i, "CodeFournisseur").ToString

                            query = "delete from t_bc_listebesoins where RefBonCommande='" & NumBC & "'"
                            ExecuteNonQuery(query)

                            query = "delete from t_bc_signataire where RefBonCommande ='" & NumBC & "'"
                            ExecuteNonQuery(query)

                            If TypeElab = "Sans Passation de Marché" Then
                                query = "delete from t_fournisseur where CodeFournis = '" & CodeFournisseur & "'"
                                ExecuteNonQuery(query)
                            End If

                            query = "delete from t_boncommande where RefBonCommande='" & NumBC & "'"
                            ExecuteNonQuery(query)

                            supp = "VRAI"
                        Else
                            supp = ""
                        End If
                    Else
                        SuccesMsg("Ce bon de commande a été validé au niveau des engagements. Suppression impossible.")
                        Exit Sub
                    End If

                End If
            Next

            If supp = "FAUX" Then
                SuccesMsg("Veuillez cocher un bon de commande")
            ElseIf supp = "VRAI" Then
                SuccesMsg("Suppression effectuée avec succès")
                BtActualiser_Click(sender, e)
            End If

        Else
            SuccesMsg("Vous n'avez pas encore élaboré de bon de commande.")
        End If

    End Sub

    Private Sub BtModifier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtModifier.Click
        If ViewBoncommande.RowCount > 0 Then
            Dim bool As Boolean = False
            Dim VerifModifBon As String = ""

            For i = 0 To ViewBoncommande.RowCount - 1
                VerifModifBon = ViewBoncommande.GetRowCellValue(i, "BonValider")

                If VerifModifBon.ToUpper = "OUI" Then
                    If CBool(ViewBoncommande.GetRowCellValue(i, "Choix")) = True Then
                        BonCommande.Size = New Point(1130, 822)
                        AjoutModif = "Modifier"
                        j = i
                        Dialog_form(BonCommande)
                        bool = True
                    End If
                Else
                    SuccesMsg("Ce bon de commande a été validé au niveau des engagements. Modification impossible.")
                    Exit Sub
                End If
            Next

            If bool = False Then
                SuccesMsg("Veuillez cocher un bon de commande")
            End If
        Else
            SuccesMsg("Veuillez élaborer un bon de commande")
        End If

    End Sub

    Private Sub BtAjouter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjouter.Click
        BonCommande.Size = New Point(1130, 822)
        AjoutModif = "Ajout"
        Dialog_form(BonCommande)
    End Sub

    Private Sub BtActualiser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtActualiser.Click
        LoadColonneBonCommande()
        RemplirDataGrid()

        If Checktous.Checked Then
            Checktous.Checked = False
        End If
    End Sub

    Private Sub RemplirDataGridCocherTous()
        query = "SELECT RefBonCommande,CodeFournisseur,TypeElabBC,NumeroDAO,RefLot,IntituleMarche,DateCommande,ConditionsPaiement,DelaiLivraison,LieuLivraison,InstructionSpeciale,RefArticle,Designation,Quantite,PrixUnitaire,MontantRabais,Ajustement,MontantBCHT,PcrtTVA,PcrtRemise,AutreTaxe,PcrtAutreTaxe,MontantTotalTTC,BonValider,EMP_ID FROM t_boncommande "
        query &= "where CodeProjet = '" & ProjetEnCours & "' AND EMP_ID = '" & cur_User & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        Dim cptr As Integer = 0
        Dim NomEditeur As String = ""
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
            Dim drS = NewLine.NewRow()
            'drS("Choix") = TabTrue(cpt - 1)
            drS("Choix") = TabTrue(1)
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
            drS("Référence") = MettreApost(rw("RefArticle"))
            drS("Désignation") = MettreApost(rw("Designation"))
            drS("Quantité") = rw("Quantite").ToString
            drS("Prix Unitaire") = rw("PrixUnitaire").ToString
            drS("Montant Rabais") = rw("MontantRabais").ToString
            drS("Ajustement") = rw("Ajustement").ToString
            drS("MontantBCHT") = rw("MontantBCHT")
            drS("Montant") = AfficherMonnaie(rw("MontantTotalTTC"))
            drS("PcrtTVA") = rw("PcrtTVA")
            drS("PcrtREMISE") = rw("PcrtRemise")
            drS("LibelleAutreTaxe") = MettreApost(rw("AutreTaxe"))
            drS("PcrtAutreTaxe") = rw("PcrtAutreTaxe")
            drS("Editeur") = NomEditeur.ToString
            drS("BonValider") = rw("BonValider").ToString
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
        ViewBoncommande.Columns("Référence").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Désignation").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Quantité").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Prix Unitaire").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Montant Rabais").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Ajustement").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("MontantBCHT").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Montant").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("PcrtTVA").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("PcrtREMISE").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("LibelleAutreTaxe").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("PcrtAutreTaxe").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Editeur").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Date d'édition").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("BonValider").OptionsColumn.AllowEdit = False

        Dim nbre As Integer = cptr
        If nbre = 0 Then
            LblNombre.Text = "Aucun enregistrement"
        ElseIf nbre = 1 Then
            LblNombre.Text = nbre & " enregistrement"
        Else
            LblNombre.Text = nbre & " enregistrements"
        End If
    End Sub

    Private Sub Checktous_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Checktous.CheckedChanged
        Try
            If ViewBoncommande.RowCount > 0 Then
                For k = 0 To ViewBoncommande.RowCount - 1
                    ViewBoncommande.SetRowCellValue(k, "Choix", Checktous.Checked)
                Next
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub RemplirdatagridRechercher()
        query = "SELECT RefBonCommande,CodeFournisseur,TypeElabBC,NumeroDAO,RefLot,IntituleMarche,DateCommande,ConditionsPaiement,DelaiLivraison,LieuLivraison,InstructionSpeciale,RefArticle,Designation,Quantite,PrixUnitaire,MontantRabais,Ajustement,MontantBCHT,PcrtTVA,PcrtRemise,AutreTaxe,PcrtAutreTaxe,MontantTotalTTC,BonValider,EMP_ID FROM t_boncommande "
        query &= "where CodeProjet = '" & ProjetEnCours & "' AND EMP_ID = '" & cur_User & "' AND RefBonCommande LIKE'" & TxtRechercher.Text & "%'"
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
            drS("Référence") = MettreApost(rw("RefArticle"))
            drS("Désignation") = MettreApost(rw("Designation"))
            drS("Quantité") = rw("Quantite").ToString
            drS("Prix Unitaire") = rw("PrixUnitaire").ToString
            drS("Montant Rabais") = rw("MontantRabais").ToString
            drS("Ajustement") = rw("Ajustement").ToString
            drS("MontantBCHT") = rw("MontantBCHT")
            drS("Montant") = AfficherMonnaie(rw("MontantTotalTTC"))
            drS("PcrtTVA") = rw("PcrtTVA")
            drS("PcrtREMISE") = rw("PcrtRemise")
            drS("LibelleAutreTaxe") = MettreApost(rw("AutreTaxe"))
            drS("PcrtAutreTaxe") = rw("PcrtAutreTaxe")
            drS("Editeur") = NomEditeur.ToString
            drS("BonValider") = rw("BonValider").ToString
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
        ViewBoncommande.Columns("Référence").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Désignation").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Quantité").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Prix Unitaire").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Montant Rabais").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Ajustement").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("MontantBCHT").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Montant").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("PcrtTVA").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("PcrtREMISE").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("LibelleAutreTaxe").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("PcrtAutreTaxe").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Editeur").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("Date d'édition").OptionsColumn.AllowEdit = False
        ViewBoncommande.Columns("BonValider").OptionsColumn.AllowEdit = False

        Dim nbre As Integer = cptr.ToString
        If nbre = 0 Then
            LblNombre.Text = "Aucun enregistrement"
        ElseIf nbre = 1 Then
            LblNombre.Text = nbre & " enregistrement"
        Else
            LblNombre.Text = nbre & " enregistrements"
        End If
    End Sub

    Private Sub TxtRechercher_TextChanged(sender As Object, e As EventArgs) Handles TxtRechercher.TextChanged
        Try
            If TxtRechercher.Text = "" Or TxtRechercher.Text = "Rechercher" Then
                RemplirDataGrid()
            Else
                RemplirdatagridRechercher()
                'ViewBoncommande.Columns("Choix").Visible = False
                'ViewBoncommande.Columns("Date d'édition").Width = 294
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TxtRechercher_Enter(sender As Object, e As EventArgs) Handles TxtRechercher.Enter
        If TxtRechercher.Text = "Rechercher" Then
            TxtRechercher.Text = ""
            RemplirDataGrid()
        End If
    End Sub

    Private Sub TxtRechercher_Leave(sender As Object, e As EventArgs) Handles TxtRechercher.Leave
        If TxtRechercher.Text <> "Rechercher" Then
            TxtRechercher.Text = "Rechercher"
        End If
    End Sub
End Class