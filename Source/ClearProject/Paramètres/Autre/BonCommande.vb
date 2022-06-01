Imports System.Math
Imports DevExpress.XtraEditors.Repository
Imports MySql.Data.MySqlClient

Public Class BonCommande

    Dim tauxDollar As Decimal = 1
    Dim CfaGere As Boolean = True
    Dim dtboncommande = New DataTable
    Dim ID_NumDAO() As String
    Dim ID_CodeLot() As String
    Dim CodeFournis As String = ""
    Dim NumeroBonCommande As String = ""
    Dim NumDAO As String = ""
    Dim RefLot As String = ""
    Dim CodeFournisseur As String = ""
    Dim ConditionPaiement As String = ""

    Private Sub ChargerService()
        'CmbService.Properties.Items.Clear()
        'CmbService.ResetText()
        'query = "select NomService from T_Service where CodeProjet='" & ProjetEnCours & "' order by NomService"
        'Dim dt As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt.Rows
        '    CmbService.Properties.Items.Add(MettreApost(rw(0).ToString))
        'Next
    End Sub

    Private Sub Chargertiers()
        'Cmbctfour.Properties.Items.Clear()
        'Cmbctfour.ResetText()
        'query = "select r.CPT_TIER, r.CODE_CPT, r.CODE_TCPT,c.NOM_CPT from T_COMP_RATTACH_TIERS r, T_COMP_COMPTE c where c.CODE_CPT=r.CODE_CPT AND r.code_sc like '401%'"
        'Dim dt2 As DataTable = ExcecuteSelectQuery(query)
        'For Each rw2 As DataRow In dt2.Rows
        '    Cmbctfour.Properties.Items.Add(rw2(1).ToString & "   " & MettreApost(rw2(3).ToString))
        'Next
    End Sub

    Private Sub RemplirListeActivites()
        '    CmbActivite.Properties.Items.Clear()
        '    CmbActivite.ResetText()
        '    query = "select LibelleCourt,LibellePartition from T_Partition where CodeClassePartition='5' and CodeProjet='" & ProjetEnCours & "'"
        '    Dim dt As DataTable = ExcecuteSelectQuery(query)
        '    For Each rw In dt.Rows
        '        CmbActivite.Properties.Items.Add(rw(0).ToString & "-" & MettreApost(rw(1).ToString))
        '    Next
    End Sub

    Private Sub ChargerNumDAO()
        CmbNumDAO.ResetText()
        CmbNumDAO.Properties.Items.Clear()

        Dim VerifMarche As Double = 0
        Dim VerifBonCommande As Double = 0

        'récupération des DAO dont l'évaluation et l'attribution ont été faite
        query = "SELECT d.NumeroDAO, l.RefLot FROM t_dao d, t_lotdao l, t_soumissionfournisseurclassement s WHERE d.NumeroDAO = l.NumeroDAO AND l.NumeroDAO = s.NumeroDAO AND d.DateFinJugement is not null AND d.statut_DAO <> 'Annulé' and d.CodeProjet = '" & ProjetEnCours & "' and s.Selectionne = 'OUI' and s.Attribue = 'OUI' GROUP by l.RefLot ORDER BY d.NumeroDAO"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        ReDim ID_NumDAO(dt.Rows.Count)
        Dim i As Integer = 0
        For Each rw As DataRow In dt.Rows
            query = "SELECT count(NumeroMarche) as Result from t_marchesigne WHERE NumeroDAO = '" & rw("NumeroDAO").ToString & "' and RefLot = '" & rw("RefLot").ToString & "' and CodeProjet = '" & ProjetEnCours & "'"
            VerifMarche = Val(ExecuteScallar(query))
            If VerifMarche > 0 Then
                Continue For
            End If

            query = "SELECT count(RefBonCommande) as Result from t_boncommande WHERE NumeroDAO = '" & rw("NumeroDAO").ToString & "' and RefLot = '" & rw("RefLot").ToString & "' and CodeProjet = '" & ProjetEnCours & "'"
            VerifBonCommande = Val(ExecuteScallar(query))
            If VerifBonCommande > 0 Then
                Continue For
            End If

            CmbNumDAO.Properties.Items.Add(rw("NumeroDAO").ToString)
            ID_NumDAO(i) = rw("NumeroDAO").ToString
            i += 1
        Next

    End Sub

    Private Sub BonCommande_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RdParPassMarche.Checked = True
        RdParPassMarche.Enabled = True
        RdSansPassMarche.Checked = False
        RdSansPassMarche.Enabled = True
        TxtAutreTaxe.Enabled = False

        If Liste_boncommande.AjoutModif = "Ajout" Then
            BtEnregistrer.Enabled = True
            BtModifier.Enabled = False
            Initialiser()
            NumBonCommande_Auto(Txtboncmde)
            LoadColonneListeBesoins()
        ElseIf Liste_boncommande.AjoutModif = "Modifier" Then
            BtEnregistrer.Enabled = False
            BtModifier.Enabled = True
            Txtboncmde.Enabled = False
            Chargement()
        End If

    End Sub

    Private Sub Chargement()
        NumeroBonCommande = Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "N° Bon Commande").ToString
        NumDAO = Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "NumeroDAO").ToString
        RefLot = Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "RefLot").ToString
        CodeFournisseur = Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "CodeFournisseur").ToString
        ConditionPaiement = Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "ConditionPaiement").ToString

        Dim dt As DataTable = New DataTable()

        'vérification du choix d'élaboration enregistré
        query = "select TypeElabBC from t_boncommande where RefBonCommande = '" & NumeroBonCommande & "'"
        Dim TypeElabBC As String = ExecuteScallar(query)

        If TypeElabBC = "Par Passation de Marché" Then
            RdParPassMarche.Checked = True
            RdParPassMarche.Enabled = True
            RdSansPassMarche.Enabled = False
            CmbNumDAO.Enabled = False
            CmbCodeLot.Enabled = False

            'récupération de la référence saisie
            query = "select RefListeBesoins,Designation,PrixTotal from t_bc_listebesoins where RefBonCommande = '" & NumeroBonCommande & "'"
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                TxtReference.Text = MettreApost(rw("RefListeBesoins").ToString)
                TxtDesignation.Text = MettreApost(rw("Designation").ToString)
                TxtNewMont.Text = AfficherMonnaie(rw("PrixTotal").ToString)
            Next
        Else
            RdSansPassMarche.Checked = True
            RdSansPassMarche.Enabled = True
            RdParPassMarche.Enabled = False
            LoadColonneListeBesoins()
            RemplirDatagridListeBesoins()
        End If

        CmbNumDAO.Text = NumDAO

        'Récupération du code du lot
        query = "select CodeLot from t_lotdao where NumeroDAO = '" & NumDAO & "' and RefLot = '" & RefLot & "'"
        Dim CodeLot As String = ExecuteScallar(query)
        CmbCodeLot.Text = CodeLot.ToString

        Dateboncmde.Text = Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "Date d'édition").ToString
        Txtboncmde.Text = Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "N° Bon Commande").ToString

        'Informations liées au fournisseur
        query = "SELECT NomFournis,AdresseCompleteFournis,TelFournis,CompteContribuableFournis,RegistreCommerceFournis FROM t_fournisseur WHERE CodeFournis = '" & CodeFournisseur & "'"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            TxtFournisseur.Text = MettreApost(rw("NomFournis").ToString)
            TxtAdresseFour.Text = MettreApost(rw("AdresseCompleteFournis").ToString)
            TxtTelFour.Text = rw("TelFournis").ToString
            TxtCCFour.Text = MettreApost(rw("CompteContribuableFournis").ToString)
            TxtRCCM.Text = MettreApost(rw("RegistreCommerceFournis").ToString)
        Next

        'Condition de paiement
        If ConditionPaiement = "Chèque" Then
            RdCheque.Checked = True
            RdVirement.Checked = False
            RdEspeces.Checked = False
        ElseIf ConditionPaiement = "Virement" Then
            RdVirement.Checked = True
            RdCheque.Checked = False
            RdEspeces.Checked = False
        Else
            RdEspeces.Checked = True
            RdCheque.Checked = False
            RdVirement.Checked = False
        End If

        TxtDelaiLivraison.Text = MettreApost(Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "DelaiLivraison").ToString)
        TxtLieuLivraison.Text = MettreApost(Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "LieuLivraison").ToString)
        TxtIsntructionSpec.Text = MettreApost(Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "InstructionSpeciale").ToString)
        TxtIntituleMarche.Text = MettreApost(Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "Intitulé du marché").ToString)
        TxtTVA.Text = MettreApost(Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "PcrtTVA").ToString)
        TxtRemise.Text = MettreApost(Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "PcrtREMISE").ToString)
        TxtLibAutreTaxe.Text = MettreApost(Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "LibelleAutreTaxe").ToString)
        TxtAutreTaxe.Text = MettreApost(Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "PcrtAutreTaxe").ToString)

    End Sub

    Private Sub RemplirDatagridListeBesoins()
        query = "select RefListeBesoins,Designation,Quantite,PrixUnitaire,PrixTotal from t_bc_listebesoins where RefBonCommande = '" & NumeroBonCommande & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        Dim NewLine As DataTable = ListBonCmde.DataSource
        Dim cpt As Decimal = 0

        For Each rw As DataRow In dt.Rows
            Dim drS = NewLine.NewRow()
            'cpt += 1
            drS("Choix") = TabTrue(0)
            drS("Référence") = MettreApost(rw("RefListeBesoins").ToString)
            drS("Désignation") = MettreApost(rw("Designation").ToString)
            drS("Quantité") = AfficherMonnaie(rw("Quantite").ToString)
            drS("Prix Unitaire") = AfficherMonnaie(rw("PrixUnitaire").ToString.Replace(",00", ""))
            drS("Montant") = AfficherMonnaie(rw("PrixTotal").ToString)
            NewLine.Rows.Add(drS)
        Next

        Dim edit As RepositoryItemCheckEdit = New RepositoryItemCheckEdit()
        edit.ValueChecked = True
        edit.ValueUnchecked = False
        ViewLstCmde.Columns("Choix").ColumnEdit = edit
        ListBonCmde.RepositoryItems.Add(edit)
        ViewLstCmde.OptionsBehavior.Editable = True

        ViewLstCmde.Columns("Référence").OptionsColumn.AllowEdit = False
        ViewLstCmde.Columns("Désignation").OptionsColumn.AllowEdit = False
        ViewLstCmde.Columns("Quantité").OptionsColumn.AllowEdit = False
        ViewLstCmde.Columns("Prix Unitaire").OptionsColumn.AllowEdit = False
        ViewLstCmde.Columns("Montant").OptionsColumn.AllowEdit = False
    End Sub

    Private Sub Initialiser()
        CmbCodeLot.ResetText()
        CmbNumDAO.ResetText()
        Dateboncmde.Text = ""
        NumBonCommande_Auto(Txtboncmde)
        TxtFournisseur.Text = ""
        TxtAdresseFour.Text = ""
        TxtTelFour.Text = ""
        TxtCCFour.Text = ""
        TxtRCCM.Text = ""
        RdCheque.Checked = True
        RdVirement.Checked = False
        RdEspeces.Checked = False
        TxtDelaiLivraison.Text = ""
        TxtLieuLivraison.Text = ""
        TxtIsntructionSpec.Text = ""
        TxtIntituleMarche.Text = ""
        TxtReference.Text = ""
        TxtDesignation.Text = ""
        TxtQte.Text = ""
        TxtPu.Text = ""
        TxtNewMont.Text = ""
        TxtMontLettre.Text = ""
        TxtTVA.Text = ""
        TxtRemise.Text = ""
        TxtLibAutreTaxe.Text = ""
        TxtAutreTaxe.Text = ""
        dtboncommande.Rows.clear()
    End Sub

    Sub codeauto(ByVal montext As DevExpress.XtraEditors.TextEdit)
        Try
            Dim nbre As Decimal = 0
            query = "select count(CodeBon) from t_boncommande where CodeProjet='" & ProjetEnCours & "'"
            nbre = Val(ExecuteScallar(query))

            If nbre = 0 Then
                montext.Text = "1"
            Else
                query = "select count(CodeBon) from t_boncommande where CodeProjet='" & ProjetEnCours & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw In dt.Rows

                    Dim strNombre As String
                    Dim SpaceIndex As Decimal
                    Dim caractere As String
                    Dim intNombre, nbr As Decimal

                    strNombre = ""
                    For SpaceIndex = 1 To Len(rw(0).ToString)
                        caractere = Mid$(rw(0).ToString, SpaceIndex, 1)
                        If caractere >= "0" And caractere <= "9" Then
                            strNombre = strNombre + caractere
                        End If
                    Next
                    intNombre = CInt(strNombre)
                    nbr = intNombre + Int(1)
                    montext.Text = nbr.ToString
                Next

            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub

    Private Sub LoadColonneListeBesoins()
        dtboncommande.Columns.Clear()
        dtboncommande.Columns.Add("Choix", Type.GetType("System.Boolean"))
        dtboncommande.Columns.Add("Référence", Type.GetType("System.String"))
        dtboncommande.Columns.Add("Désignation", Type.GetType("System.String"))
        dtboncommande.Columns.Add("Quantité", Type.GetType("System.String"))
        dtboncommande.Columns.Add("Prix Unitaire", Type.GetType("System.String"))
        dtboncommande.Columns.Add("Montant", Type.GetType("System.String"))
        ListBonCmde.DataSource = dtboncommande
    End Sub

    Private Sub GridBonCommande()

        Dim NewLine As DataTable = ListBonCmde.DataSource
        Dim cpt As Decimal = 0

        Dim drS = NewLine.NewRow()
        cpt += 1
        drS("Choix") = TabTrue(cpt - 1)
        drS("Référence") = MettreApost(TxtReference.Text)
        drS("Désignation") = MettreApost(TxtDesignation.Text)
        drS("Quantité") = AfficherMonnaie(CDbl(TxtQte.Text))
        drS("Prix Unitaire") = AfficherMonnaie(CDbl(TxtPu.Text))
        drS("Montant") = AfficherMonnaie(CDbl(TxtNewMont.Text))
        NewLine.Rows.Add(drS)

        Dim edit As RepositoryItemCheckEdit = New RepositoryItemCheckEdit()
        edit.ValueChecked = True
        edit.ValueUnchecked = False
        ViewLstCmde.Columns("Choix").ColumnEdit = edit
        ListBonCmde.RepositoryItems.Add(edit)
        ViewLstCmde.OptionsBehavior.Editable = True

        ViewLstCmde.Columns("Référence").OptionsColumn.AllowEdit = False
        ViewLstCmde.Columns("Désignation").OptionsColumn.AllowEdit = False
        ViewLstCmde.Columns("Quantité").OptionsColumn.AllowEdit = False
        ViewLstCmde.Columns("Prix Unitaire").OptionsColumn.AllowEdit = False
        ViewLstCmde.Columns("Montant").OptionsColumn.AllowEdit = False

    End Sub

    Private Sub TxtQte_TextChanged(sender As Object, e As EventArgs) Handles TxtQte.TextChanged, TxtPu.TextChanged
        If TxtQte.Text <> "" And TxtPu.Text <> "" Then
            Dim qte As Double = 0
            Dim pu As Double = 0
            qte = IIf(TxtQte.Text = "", 0, TxtQte.Text)
            pu = IIf(TxtPu.Text = "", 0, TxtPu.Text)
            TxtNewMont.Text = AfficherMonnaie(CStr(CDbl(qte.ToString) * CDbl(pu.ToString)))
            Try
                TxtMontLettre.Text = MontantLettre(TxtNewMont.Text)
            Catch ex As Exception
                AlertMsg("Dépassement de caractère!")
            End Try
        End If
    End Sub

    Private Sub TxtPu_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPu.KeyPress
        Select Case e.KeyChar
            Case ControlChars.CrLf
                If TxtIntituleMarche.Text = "" Then
                    SuccesMsg("Veuillez saisir l'intitulé du marché")
                ElseIf TxtDesignation.Text = "" Then
                    SuccesMsg("Veuillez saisir la désignation")
                ElseIf TxtQte.Text = "" Then
                    SuccesMsg("Veuillez saisir la quantité")
                ElseIf TxtPu.Text = "" Then
                    SuccesMsg("Veuillez saisir le prix unitaire")
                Else
                    GridBonCommande()
                    InitFormulaireListeBesoins()
                End If
            Case Else
        End Select
    End Sub

    Private Sub InitFormulaireListeBesoins()
        TxtReference.Text = ""
        TxtDesignation.Text = ""
        TxtQte.Text = ""
        TxtPu.Text = ""
        TxtNewMont.Text = ""
        TxtMontLettre.Text = ""
    End Sub

    Private Sub BtAnnuler_Click(sender As Object, e As EventArgs) Handles BtAnnuler.Click
        Initialiser()
    End Sub

    Private Sub RdParPassMarche_CheckedChanged(sender As Object, e As EventArgs) Handles RdParPassMarche.CheckedChanged
        If RdParPassMarche.Checked Then
            RdSansPassMarche.Checked = False
            TxtIntituleMarche.Enabled = False
            TxtFournisseur.Enabled = False
            CmbNumDAO.Enabled = True
            CmbNumDAO.Properties.Items.Clear()
            CmbCodeLot.Enabled = True
            CmbCodeLot.Properties.Items.Clear()
            TxtQte.Enabled = False
            TxtPu.Enabled = False
            TxtDesignation.Enabled = False
            lc1.Visible = True
            lc2.Visible = True
            'lc3.Visible = True
            'lc4.Visible = True
            'lc5.Visible = True
            'lc6.Visible = True
            'lc7.Visible = True
            'lc8.Visible = True

            Initialiser()
            ChargerNumDAO()
        End If
    End Sub

    Private Sub RdSansPassMarche_CheckedChanged(sender As Object, e As EventArgs) Handles RdSansPassMarche.CheckedChanged
        On Error Resume Next
        If RdSansPassMarche.Checked Then
            RdParPassMarche.Checked = False
            TxtIntituleMarche.Enabled = True
            TxtFournisseur.Enabled = True
            CmbNumDAO.Enabled = False
            CmbNumDAO.Properties.Items.Clear()
            CmbCodeLot.Enabled = False
            CmbCodeLot.Properties.Items.Clear()
            TxtQte.Enabled = True
            TxtPu.Enabled = True
            TxtDesignation.Enabled = True
            lc1.Visible = False
            lc2.Visible = False
            'lc3.Visible = True
            'lc4.Visible = True
            'lc5.Visible = True
            'lc6.Visible = True
            'lc7.Visible = True
            'lc8.Visible = True

            Initialiser()
        End If
    End Sub

    Private Sub RdCheque_CheckedChanged(sender As Object, e As EventArgs) Handles RdCheque.CheckedChanged
        If RdCheque.Checked Then
            RdVirement.Checked = False
            RdEspeces.Checked = False
        End If
    End Sub

    Private Sub RdVirement_CheckedChanged(sender As Object, e As EventArgs) Handles RdVirement.CheckedChanged
        If RdVirement.Checked Then
            RdCheque.Checked = False
            RdEspeces.Checked = False
        End If
    End Sub

    Private Sub RdEspeces_CheckedChanged(sender As Object, e As EventArgs) Handles RdEspeces.CheckedChanged
        If RdEspeces.Checked Then
            RdCheque.Checked = False
            RdVirement.Checked = False
        End If
    End Sub

    Private Sub TxtLibAutreTaxe_EditValueChanged(sender As Object, e As EventArgs) Handles TxtLibAutreTaxe.EditValueChanged
        If TxtLibAutreTaxe.Text <> "" Then
            TxtAutreTaxe.Enabled = True
        Else
            TxtAutreTaxe.Enabled = False
            TxtAutreTaxe.Text = ""
        End If
    End Sub

    Private Sub CmbNumDAO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbNumDAO.SelectedIndexChanged

        CmbCodeLot.ResetText()
        CmbCodeLot.Properties.Items.Clear()

        Dim VerifMarche As Double = 0
        Dim VerifBonCommande As Double = 0

        If CmbNumDAO.SelectedIndex <> -1 Then

            query = "SELECT IntituleDAO FROM t_dao WHERE CodeProjet = '" & ProjetEnCours & "' AND NumeroDAO = '" & ID_NumDAO(CmbNumDAO.SelectedIndex) & "'"
            Dim Intitule = ExecuteScallar(query)
            TxtIntituleMarche.Text = MettreApost(Intitule.ToString)

            query = "SELECT * from t_lotdao WHERE NumeroDAO = '" & ID_NumDAO(CmbNumDAO.SelectedIndex) & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            ReDim ID_CodeLot(dt.Rows.Count)
            Dim i As Integer = 0
            For Each rw As DataRow In dt.Rows
                query = "SELECT count(NumeroMarche) as Result from t_marchesigne WHERE NumeroDAO = '" & rw("NumeroDAO").ToString & "' and RefLot = '" & rw("RefLot").ToString & "' and CodeProjet = '" & ProjetEnCours & "'"
                VerifMarche = Val(ExecuteScallar(query))
                If VerifMarche > 0 Then
                    Continue For
                End If

                query = "SELECT count(RefBonCommande) as Result from t_boncommande WHERE NumeroDAO = '" & rw("NumeroDAO").ToString & "' and RefLot = '" & rw("RefLot").ToString & "' and CodeProjet = '" & ProjetEnCours & "'"
                VerifBonCommande = Val(ExecuteScallar(query))
                If VerifBonCommande > 0 Then
                    Continue For
                End If

                CmbCodeLot.Properties.Items.Add(rw("CodeLot").ToString)
                ID_CodeLot(i) = rw("CodeLot").ToString
                i += 1
            Next

        End If
    End Sub

    Private Sub CmbCodeLot_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbCodeLot.SelectedIndexChanged
        On Error Resume Next

        TxtFournisseur.Text = ""
        TxtAdresseFour.Text = ""
        TxtTelFour.Text = ""
        TxtCCFour.Text = ""
        TxtRCCM.Text = ""
        TxtDesignation.Text = ""

        'Dim CodeFournis As String = ""
        Dim LibelleLot As String = ""
        Dim ResultQTE As Double = 0

        query = "SELECT CodeFournis, NomFournis, AdresseCompleteFournis, TelFournis, CompteContribuableFournis, RegistreCommerceFournis FROM t_fournisseur WHERE CodeProjet = '" & ProjetEnCours & "' and CodeFournis IN (SELECT CodeFournis FROM t_soumissionfournisseurclassement where CodeLot = '" & ID_CodeLot(CmbCodeLot.SelectedIndex) & "' and NumeroDAO = '" & ID_NumDAO(CmbNumDAO.SelectedIndex) & "')"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CodeFournis = rw("CodeFournis").ToString
            TxtFournisseur.Text = MettreApost(rw("NomFournis").ToString)
            TxtAdresseFour.Text = MettreApost(rw("AdresseCompleteFournis").ToString)
            TxtTelFour.Text = MettreApost(rw("TelFournis").ToString)
            TxtCCFour.Text = MettreApost(rw("CompteContribuableFournis").ToString)
            TxtRCCM.Text = MettreApost(rw("RegistreCommerceFournis").ToString)
        Next

        query = "SELECT PrixCorrigeOffre FROM t_soumissionfournisseurclassement WHERE CodeFournis = '" & CodeFournis.ToString & "' and CodeLot = '" & ID_CodeLot(CmbCodeLot.SelectedIndex) & "'"
        ResultQTE = ExecuteScallar(query)
        TxtNewMont.Text = AfficherMonnaie(ResultQTE.ToString)

        query = "SELECT LibelleLot from t_lotdao WHERE NumeroDAO = '" & ID_NumDAO(CmbNumDAO.SelectedIndex) & "' AND CodeLot = '" & ID_CodeLot(CmbCodeLot.SelectedIndex) & "'"
        LibelleLot = ExecuteScallar(query)
        TxtDesignation.Text = MettreApost(LibelleLot.ToString)

        'dt = ExcecuteSelectQuery(query)
        'For Each rw1 As DataRow In dt.Rows
        '    TxtNewMont.Text = AfficherMonnaie(rw1("PrixCorrigeOffre").ToString)
        'Next


        'query = "SELECT l.LibelleLot, SUM(s.QteFournit) as Qte FROM t_lotdao l, t_spectechfourniture s WHERE l.NumeroDAO = s.NumeroDAO and l.CodeLot = s.CodeLot and l.NumeroDAO = '" & CmbNumDAO.Text & "' and l.CodeLot = '" & CmbCodeLot.Text & "'"
        'dt = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt.Rows
        '    TxtDesignation.Text = EnleverApost(rw("LibelleLot").ToString)
        '    TxtQte.Text = AfficherMonnaie(rw("QteFournit").ToString)
        'Next

        'query = "SELECT RefSoumis FROM t_soumissionfournisseur WHERE CodeFournis = '" & CodeFournis.ToString & "' and CodeLot = '" & CmbCodeLot.SelectedIndex & "'"
        'dt = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt.Rows
        '    query = ""
        'Next
    End Sub

    Private Sub TxtNewMont_TextChanged(sender As Object, e As EventArgs) Handles TxtNewMont.TextChanged
        If RdParPassMarche.Checked Then
            Try
                TxtMontLettre.Text = MontantLettre(TxtNewMont.Text)
            Catch ex As Exception
                AlertMsg("Dépassement de caractère. Chiffre trop énorme!")
            End Try
        End If
    End Sub

    Private Sub BtEnregistrer_Click(sender As Object, e As EventArgs) Handles BtEnregistrer.Click
        If RdParPassMarche.Checked Then

            If CmbNumDAO.SelectedIndex = -1 Then
                SuccesMsg("Veuillez sélectionner le numéro du DAO")
                CmbNumDAO.Focus()
            ElseIf CmbCodeLot.SelectedIndex = -1 Then
                SuccesMsg("Veuillez sélectionner le code du lot")
                CmbCodeLot.Focus()
            ElseIf Dateboncmde.Text = "" Then
                SuccesMsg("Veuillez choisir la date d'élaboration du bon de commande")
                Dateboncmde.Focus()
            ElseIf TxtLibAutreTaxe.Text <> "" And TxtAutreTaxe.Text = "" Then
                SuccesMsg("Veuillez saisir le pourcentage de la taxe correspondant aux autres taxes")
                TxtAutreTaxe.Focus()
            Else
                NumBonCommande_Auto(Txtboncmde)
                Dim Annee As String = CStr(Now.Year)

                Dim ChoixElabBC As String = "Par Passation de Marché"
                Dim dd As String = CDate(Dateboncmde.Text).ToString("dd/MM/yyy")
                Dim DateBC As String = dateconvert(dd)
                Dim RefLot As String = ""

                Dim ConditionPaiement As String = ""
                If RdCheque.Checked Then
                    ConditionPaiement = "Chèque"
                ElseIf RdVirement.Checked Then
                    ConditionPaiement = "Virement"
                Else
                    ConditionPaiement = "Espèces"
                End If

                Dim MontantHT As String = TxtNewMont.Text
                Dim TVA As String = ""
                Dim Remise As String = ""
                Dim AutreTaxe As String = ""
                Dim MontantTVA As Double = 0
                Dim MontantRemise As Double = 0
                Dim MontantAutreTaxe As Double = 0
                Dim MontantNetHT As Double = 0
                Dim MontantTOTAL As Double = 0
                Dim MontantTotalTTC As Double = 0

                If TxtRemise.Text = "" Then
                    Remise = ""
                    MontantRemise = 0
                Else
                    Remise = TxtRemise.Text
                    MontantRemise = Math.Round(CDbl(MontantHT.ToString) * (CDbl(Remise) / 100))
                End If

                MontantNetHT = CDbl(MontantHT.ToString) - MontantRemise

                If TxtTVA.Text = "" Then
                    TVA = ""
                    MontantTVA = 0
                Else
                    TVA = TxtTVA.Text
                    MontantTVA = Math.Round(MontantNetHT * (CDbl(TVA) / 100))
                End If

                MontantTOTAL = MontantNetHT - MontantTVA

                If TxtLibAutreTaxe.Text = "" Then
                    AutreTaxe = ""
                    MontantAutreTaxe = 0
                Else
                    AutreTaxe = TxtAutreTaxe.Text
                    MontantAutreTaxe = Math.Round(MontantTOTAL * (CDbl(AutreTaxe) / 100))
                End If

                MontantTotalTTC = MontantTOTAL - MontantAutreTaxe

                'récupération de la référence du lot
                query = "SELECT RefLot FROM t_lotdao WHERE CodeLot = '" & ID_CodeLot(CmbCodeLot.SelectedIndex) & "' AND NumeroDAO = '" & ID_NumDAO(CmbNumDAO.SelectedIndex) & "'"
                RefLot = ExecuteScallar(query)

                'Mise à jour dans la table t_fournisseur
                query = "UPDATE t_fournisseur SET AdresseCompleteFournis = '" & EnleverApost(TxtAdresseFour.Text) & "', TelFournis = '" & EnleverApost(TxtTelFour.Text) & "', CompteContribuableFournis = '" & EnleverApost(TxtCCFour.Text) & "', RegistreCommerceFournis = '" & EnleverApost(TxtRCCM.Text) & "' WHERE CodeFournis = '" & CodeFournis.ToString & "'"
                ExecuteNonQuery(query)

                'insertion dans la table liste des besoins
                Dim ListeBesoins As String = ""
                ListeBesoins = "insert into t_bc_listebesoins values (NULL,'" & EnleverApost(Txtboncmde.Text) & "','" & EnleverApost(TxtReference.Text) & "','" & EnleverApost(TxtDesignation.Text) & "','" & "" & "','" & "" & "','" & CDbl(MontantHT.ToString) & "')"
                ExecuteNonQuery(ListeBesoins)

                Dim verif As String = ""
                'insertion dans la table t_boncommande
                verif = "INSERT INTO t_boncommande values(NULL,'" & EnleverApost(Txtboncmde.Text) & "','" & Annee & "', '" & CInt(CodeFournis.ToString) & "','" & ChoixElabBC.ToString & "','" & CmbNumDAO.Text & "','" & RefLot.ToString & "','" & EnleverApost(TxtIntituleMarche.Text) & "','" & DateBC & "','" & ConditionPaiement & "','" & EnleverApost(TxtDelaiLivraison.Text) & "','"
                verif &= EnleverApost(TxtLieuLivraison.Text) & "','" & EnleverApost(TxtIsntructionSpec.Text) & "', '" & CDbl(MontantHT) & "','" & TVA.ToString & "','" & MontantTVA.ToString.Replace(",", ".") & "','" & Remise.ToString & "','" & MontantRemise.ToString.Replace(",", ".") & "','" & EnleverApost(TxtLibAutreTaxe.Text) & "','" & AutreTaxe.ToString & "','" & MontantAutreTaxe.ToString.Replace(",", ".") & "','" & MontantNetHT.ToString.Replace(",", ".") & "','" & MontantTOTAL.ToString.Replace(",", ".") & "','" & MontantTotalTTC.ToString.Replace(",", ".") & "', 'OUI','" & cur_User & "','" & ProjetEnCours & "')"
                ExecuteNonQuery(verif)

                SuccesMsg("Elaboration du Bon de commande enregistré avec succès")
                Initialiser()
                ChargerNumDAO()
            End If
        Else
            If Dateboncmde.Text = "" Then
                SuccesMsg("Veuillez choisir la date d'élaboration du bon de commande")
                Dateboncmde.Focus()
            ElseIf TxtLibAutreTaxe.Text <> "" And TxtAutreTaxe.Text = "" Then
                SuccesMsg("Veuillez saisir le pourcentage de la taxe correspondant aux autres taxes")
                TxtAutreTaxe.Focus()
            ElseIf TxtFournisseur.Text = "" Then
                SuccesMsg("Veuillez saisir le nom du fournisseur")
                TxtFournisseur.Focus()
            ElseIf TxtIntituleMarche.Text = "" Then
                SuccesMsg("Veuillez saisir l'intitulé du marché")
                TxtIntituleMarche.Focus()
            Else
                If ViewLstCmde.RowCount > 0 Then
                    Dim bool As Boolean = False
                    For i = 0 To ViewLstCmde.RowCount - 1
                        If CBool(ViewLstCmde.GetRowCellValue(i, "Choix")) = True Then
                            query = "insert into t_bc_listebesoins values(NULL,'" & EnleverApost(Txtboncmde.Text) & "','" & EnleverApost(ViewLstCmde.GetRowCellValue(i, "Référence")) & "','" & EnleverApost(ViewLstCmde.GetRowCellValue(i, "Désignation")) & "','" & ViewLstCmde.GetRowCellValue(i, "Quantité") & "','" & ViewLstCmde.GetRowCellValue(i, "Prix Unitaire") & "','" & CDbl(ViewLstCmde.GetRowCellValue(i, "Montant")) & "')"
                            ExecuteNonQuery(query)

                            bool = True
                        End If
                    Next

                    If bool = False Then
                        SuccesMsg("Veuillez cocher une ligne dans la liste des besoins")
                        Exit Sub
                    End If

                    Dim Annee As String = CStr(Now.Year)
                    Dim ChoixElabBC As String = "Sans Passation de Marché"
                    Dim dd As String = CDate(Dateboncmde.Text).ToString("dd/MM/yyy")
                    Dim DateBC As String = dateconvert(dd)
                    Dim RefLot As String = ""

                    Dim ConditionPaiement As String = ""
                    If RdCheque.Checked Then
                        ConditionPaiement = "Chèque"
                    ElseIf RdVirement.Checked Then
                        ConditionPaiement = "Virement"
                    Else
                        ConditionPaiement = "Espèces"
                    End If

                    Dim MontantHT As String = ""
                    Dim TVA As String = ""
                    Dim Remise As String = ""
                    Dim AutreTaxe As String = ""
                    Dim MontantTVA As Double = 0
                    Dim MontantRemise As Double = 0
                    Dim MontantAutreTaxe As Double = 0
                    Dim MontantNetHT As Double = 0
                    Dim MontantTOTAL As Double = 0
                    Dim MontantTotalTTC As Double = 0

                    'récupération du montant hors taxe
                    query = "SELECT SUM(PrixTotal) as MontantHT FROM t_bc_listebesoins WHERE RefBonCommande = '" & EnleverApost(Txtboncmde.Text) & "'"
                    MontantHT = ExecuteScallar(query)

                    If TxtRemise.Text = "" Then
                        Remise = ""
                        MontantRemise = 0
                    Else
                        Remise = TxtRemise.Text
                        MontantRemise = Math.Round(CDbl(MontantHT.ToString) * (CDbl(Remise) / 100))
                    End If

                    MontantNetHT = CDbl(MontantHT.ToString) - MontantRemise

                    If TxtTVA.Text = "" Then
                        TVA = ""
                        MontantTVA = 0
                    Else
                        TVA = TxtTVA.Text
                        MontantTVA = Math.Round(MontantNetHT * (CDbl(TVA) / 100))
                    End If

                    MontantTOTAL = MontantNetHT - MontantTVA

                    If TxtLibAutreTaxe.Text = "" Then
                        AutreTaxe = ""
                        MontantAutreTaxe = 0
                    Else
                        AutreTaxe = TxtAutreTaxe.Text
                        MontantAutreTaxe = Math.Round(MontantTOTAL * (CDbl(AutreTaxe) / 100))
                    End If

                    MontantTotalTTC = MontantTOTAL - MontantAutreTaxe

                    'Enregistrement du Fournisseur
                    query = "INSERT INTO t_fournisseur (CodeFournis,NomFournis,AdresseCompleteFournis,TelFournis,CompteContribuableFournis,RegistreCommerceFournis,NumeroDAO,NomAch,CodeProjet) VALUES (NULL,'" & EnleverApost(TxtFournisseur.Text) & "','" & EnleverApost(TxtAdresseFour.Text) & "','" & EnleverApost(TxtTelFour.Text) & "','" & EnleverApost(TxtCCFour.Text) & "','" & EnleverApost(TxtRCCM.Text) & "','" & EnleverApost(Txtboncmde.Text) & "','" & "" & "','" & ProjetEnCours & "')"
                    ExecuteNonQuery(query)

                    query = "SELECT CodeFournis FROM t_fournisseur WHERE NumeroDAO = '" & EnleverApost(Txtboncmde.Text) & "' and CodeProjet = '" & ProjetEnCours & "'"
                    CodeFournis = ExecuteScallar(query)

                    Dim verif As String = ""
                    'insertion dans la table t_boncommande
                    verif = "INSERT INTO t_boncommande values(NULL,'" & EnleverApost(Txtboncmde.Text) & "','" & Annee & "','" & CInt(CodeFournis.ToString) & "','" & ChoixElabBC.ToString & "','" & "" & "','" & "" & "','" & EnleverApost(TxtIntituleMarche.Text) & "','" & DateBC & "','" & ConditionPaiement & "','" & EnleverApost(TxtDelaiLivraison.Text) & "','"
                    verif &= EnleverApost(TxtLieuLivraison.Text) & "','" & EnleverApost(TxtIsntructionSpec.Text) & "', '" & CDbl(MontantHT) & "','" & TVA.ToString & "','" & MontantTVA.ToString.Replace(",", ".") & "','" & Remise.ToString & "','" & MontantRemise.ToString.Replace(",", ".") & "','" & EnleverApost(TxtLibAutreTaxe.Text) & "','" & AutreTaxe.ToString & "','" & MontantAutreTaxe.ToString.Replace(",", ".") & "','" & MontantNetHT.ToString.Replace(",", ".") & "','" & MontantTOTAL.ToString.Replace(",", ".") & "','" & MontantTotalTTC.ToString.Replace(",", ".") & "', 'OUI','" & cur_User & "','" & ProjetEnCours & "')"
                    ExecuteNonQuery(verif)

                    SuccesMsg("Elaboration du Bon de commande enregistré avec succès")
                    dtboncommande.Rows.clear()
                    Initialiser()
                Else
                    SuccesMsg("Veuillez ajouter la liste de vos besoins avant l'enregistrement")
                End If
            End If
        End If
    End Sub

    Private Sub BtModifier_Click(sender As Object, e As EventArgs) Handles BtModifier.Click
        If RdParPassMarche.Checked Then

            If Dateboncmde.Text = "" Then
                SuccesMsg("Veuillez choisir la date d'élaboration du bon de commande")
                Dateboncmde.Focus()
            ElseIf TxtLibAutreTaxe.Text <> "" And TxtAutreTaxe.Text = "" Then
                SuccesMsg("Veuillez saisir le pourcentage de la taxe correspondant aux autres taxes")
                TxtAutreTaxe.Focus()
            Else
                Dim ChoixElabBC As String = RdParPassMarche.Text
                Dim dd As String = CDate(Dateboncmde.Text).ToString("dd/MM/yyy")
                Dim DateBC As String = dateconvert(dd)

                Dim ConditionPaiement As String = ""
                If RdCheque.Checked Then
                    ConditionPaiement = "Chèque"
                ElseIf RdVirement.Checked Then
                    ConditionPaiement = "Virement"
                Else
                    ConditionPaiement = "Espèces"
                End If

                Dim MontantHT As String = TxtNewMont.Text
                Dim TVA As String = ""
                Dim Remise As String = ""
                Dim AutreTaxe As String = ""
                Dim MontantTVA As Double = 0
                Dim MontantRemise As Double = 0
                Dim MontantAutreTaxe As Double = 0
                Dim MontantNetHT As Double = 0
                Dim MontantTOTAL As Double = 0
                Dim MontantTotalTTC As Double = 0

                If TxtRemise.Text = "" Then
                    Remise = ""
                    MontantRemise = 0
                Else
                    Remise = TxtRemise.Text
                    MontantRemise = Math.Round(CDbl(MontantHT.ToString) * (CDbl(Remise) / 100))
                End If

                MontantNetHT = CDbl(MontantHT.ToString) - MontantRemise

                If TxtTVA.Text = "" Then
                    TVA = ""
                    MontantTVA = 0
                Else
                    TVA = TxtTVA.Text
                    MontantTVA = Math.Round(MontantNetHT * (CDbl(TVA) / 100))
                End If

                MontantTOTAL = MontantNetHT - MontantTVA

                If TxtLibAutreTaxe.Text = "" Then
                    AutreTaxe = ""
                    MontantAutreTaxe = 0
                Else
                    AutreTaxe = TxtAutreTaxe.Text
                    MontantAutreTaxe = Math.Round(MontantTOTAL * (CDbl(AutreTaxe) / 100))
                End If

                MontantTotalTTC = MontantTOTAL - MontantAutreTaxe

                'Mise à jour dans la table t_fournisseur
                query = "UPDATE t_fournisseur SET AdresseCompleteFournis = '" & EnleverApost(TxtAdresseFour.Text) & "', TelFournis = '" & EnleverApost(TxtTelFour.Text) & "', CompteContribuableFournis = '" & EnleverApost(TxtCCFour.Text) & "', RegistreCommerceFournis = '" & EnleverApost(TxtRCCM.Text) & "' WHERE CodeFournis = '" & CodeFournisseur.ToString & "'"
                ExecuteNonQuery(query)

                'insertion dans la table liste des besoins
                query = "update t_bc_listebesoins set RefListeBesoins = '" & EnleverApost(TxtReference.Text) & "' where RefBonCommande = '" & NumeroBonCommande & "'"
                ExecuteNonQuery(query)

                Dim verif As String = ""
                'mise à jour dans la table t_boncommande
                verif = "UPDATE t_boncommande set DateCommande = '" & DateBC & "', ConditionsPaiement = '" & ConditionPaiement & "', DelaiLivraison = '" & EnleverApost(TxtDelaiLivraison.Text) & "'"
                verif &= ", LieuLivraison = '" & EnleverApost(TxtLieuLivraison.Text) & "', InstructionSpeciale = '" & EnleverApost(TxtIsntructionSpec.Text) & "', MontantBCHT = '" & CDbl(MontantHT) & "', PcrtTVA='" & TVA.ToString & "', MontantTVA='" & MontantTVA.ToString.Replace(",", ".") & "', PcrtRemise='" & Remise.ToString & "', MontantRemise = '" & MontantRemise.ToString.Replace(",", ".") & "'"
                verif &= ", AutreTaxe='" & EnleverApost(TxtLibAutreTaxe.Text) & "', PcrtAutreTaxe = '" & AutreTaxe.ToString & "', MontantAutreTaxe = '" & MontantAutreTaxe.ToString.Replace(",", ".") & "', MontantNetHT = '" & MontantNetHT.ToString.Replace(",", ".") & "', MontantTotal = '" & MontantTOTAL.ToString.Replace(",", ".") & "', MontantTotalTTC = '" & MontantTotalTTC.ToString.Replace(",", ".") & "', EMP_ID = '" & cur_User & "', CodeProjet = '" & ProjetEnCours & "' where RefBonCommande = '" & NumeroBonCommande & "'"
                ExecuteNonQuery(verif)

                SuccesMsg("Modification effectuée avec succès")
                Initialiser()
                Me.Close()
            End If
        Else
            If Dateboncmde.Text = "" Then
                SuccesMsg("Veuillez choisir la date d'élaboration du bon de commande")
                Dateboncmde.Focus()
            ElseIf TxtLibAutreTaxe.Text <> "" And TxtAutreTaxe.Text = "" Then
                SuccesMsg("Veuillez saisir le pourcentage de la taxe correspondant aux autres taxes")
                TxtAutreTaxe.Focus()
            ElseIf TxtFournisseur.Text = "" Then
                SuccesMsg("Veuillez saisir le nom du fournisseur")
                TxtFournisseur.Focus()
            ElseIf TxtIntituleMarche.Text = "" Then
                SuccesMsg("Veuillez saisir l'intitulé du marché")
                TxtIntituleMarche.Focus()
            Else
                If ViewLstCmde.RowCount > 0 Then

                    'suppression dans la liste des besoins
                    query = "delete from t_bc_listebesoins where RefBonCommande = '" & NumeroBonCommande & "'"
                    ExecuteNonQuery(query)

                    Dim bool As Boolean = False
                    For i = 0 To ViewLstCmde.RowCount - 1
                        If CBool(ViewLstCmde.GetRowCellValue(i, "Choix")) = True Then
                            query = "insert into t_bc_listebesoins values(NULL,'" & EnleverApost(Txtboncmde.Text) & "','" & EnleverApost(ViewLstCmde.GetRowCellValue(i, "Référence")) & "','" & EnleverApost(ViewLstCmde.GetRowCellValue(i, "Désignation")) & "','" & ViewLstCmde.GetRowCellValue(i, "Quantité") & "','" & ViewLstCmde.GetRowCellValue(i, "Prix Unitaire") & "','" & CDbl(ViewLstCmde.GetRowCellValue(i, "Montant")) & "')"
                            ExecuteNonQuery(query)

                            bool = True
                        End If
                    Next

                    If bool = False Then
                        SuccesMsg("Veuillez cocher une ligne dans la liste des besoins")
                        Exit Sub
                    End If

                    Dim ChoixElabBC As String = RdSansPassMarche.Text
                    Dim dd As String = CDate(Dateboncmde.Text).ToString("dd/MM/yyy")
                    Dim DateBC As String = dateconvert(dd)
                    Dim RefLot As String = ""

                    Dim ConditionPaiement As String = ""
                    If RdCheque.Checked Then
                        ConditionPaiement = "Chèque"
                    ElseIf RdVirement.Checked Then
                        ConditionPaiement = "Virement"
                    Else
                        ConditionPaiement = "Espèces"
                    End If

                    Dim MontantHT As String = ""
                    Dim TVA As String = ""
                    Dim Remise As String = ""
                    Dim AutreTaxe As String = ""
                    Dim MontantTVA As Double = 0
                    Dim MontantRemise As Double = 0
                    Dim MontantAutreTaxe As Double = 0
                    Dim MontantNetHT As Double = 0
                    Dim MontantTOTAL As Double = 0
                    Dim MontantTotalTTC As Double = 0

                    'récupération du montant hors taxe
                    query = "SELECT SUM(PrixTotal) as MontantHT FROM t_bc_listebesoins WHERE RefBonCommande = '" & EnleverApost(Txtboncmde.Text) & "'"
                    MontantHT = ExecuteScallar(query)

                    If TxtRemise.Text = "" Then
                        Remise = ""
                        MontantRemise = 0
                    Else
                        Remise = TxtRemise.Text
                        MontantRemise = Math.Round(CDbl(MontantHT.ToString) * (CDbl(Remise) / 100))
                    End If

                    MontantNetHT = CDbl(MontantHT.ToString) - MontantRemise

                    If TxtTVA.Text = "" Then
                        TVA = ""
                        MontantTVA = 0
                    Else
                        TVA = TxtTVA.Text
                        MontantTVA = Math.Round(MontantNetHT * (CDbl(TVA) / 100))
                    End If

                    MontantTOTAL = MontantNetHT - MontantTVA

                    If TxtLibAutreTaxe.Text = "" Then
                        AutreTaxe = ""
                        MontantAutreTaxe = 0
                    Else
                        AutreTaxe = TxtAutreTaxe.Text
                        MontantAutreTaxe = Math.Round(MontantTOTAL * (CDbl(AutreTaxe) / 100))
                    End If

                    MontantTotalTTC = MontantTOTAL - MontantAutreTaxe

                    'modification du Fournisseur
                    query = "UPDATE t_fournisseur set NomFournis='" & EnleverApost(TxtFournisseur.Text) & "',AdresseCompleteFournis='" & EnleverApost(TxtAdresseFour.Text) & "',TelFournis='" & EnleverApost(TxtTelFour.Text) & "',CompteContribuableFournis='" & EnleverApost(TxtCCFour.Text) & "',RegistreCommerceFournis='" & EnleverApost(TxtRCCM.Text) & "',NumeroDAO='" & EnleverApost(Txtboncmde.Text) & "', CodeProjet='" & ProjetEnCours & "' where CodeFournis = '" & CodeFournisseur & "'"
                    ExecuteNonQuery(query)

                    'query = "SELECT CodeFournis FROM t_fournisseur WHERE NumeroDAO = '" & EnleverApost(Txtboncmde.Text) & "' and CodeProjet = '" & ProjetEnCours & "'"
                    'CodeFournis = ExecuteScallar(query)

                    Dim verif As String = ""
                    'mise à jour dans la table t_boncommande
                    verif = "UPDATE t_boncommande set IntituleMarche='" & EnleverApost(TxtIntituleMarche.Text) & "', DateCommande = '" & DateBC & "', ConditionsPaiement = '" & ConditionPaiement & "', DelaiLivraison = '" & EnleverApost(TxtDelaiLivraison.Text) & "'"
                    verif &= ", LieuLivraison = '" & EnleverApost(TxtLieuLivraison.Text) & "', InstructionSpeciale = '" & EnleverApost(TxtIsntructionSpec.Text) & "', MontantBCHT = '" & CDbl(MontantHT) & "', PcrtTVA='" & TVA.ToString & "', MontantTVA='" & MontantTVA.ToString.Replace(",", ".") & "', PcrtRemise='" & Remise.ToString & "', MontantRemise = '" & MontantRemise.ToString.Replace(",", ".") & "'"
                    verif &= ", AutreTaxe='" & EnleverApost(TxtLibAutreTaxe.Text) & "', PcrtAutreTaxe = '" & AutreTaxe.ToString & "', MontantAutreTaxe = '" & MontantAutreTaxe.ToString.Replace(",", ".") & "', MontantNetHT = '" & MontantNetHT.ToString.Replace(",", ".") & "', MontantTotal = '" & MontantTOTAL.ToString.Replace(",", ".") & "', MontantTotalTTC = '" & MontantTotalTTC.ToString.Replace(",", ".") & "', EMP_ID = '" & cur_User & "', CodeProjet = '" & ProjetEnCours & "' where RefBonCommande = '" & NumeroBonCommande & "'"
                    ExecuteNonQuery(verif)

                    SuccesMsg("Modification effectuée avec succès")
                    Initialiser()
                    Me.Close()
                Else
                    SuccesMsg("Veuillez ajouter la liste de vos besoins avant la modification")
                End If
            End If
        End If
    End Sub

    Private Sub ListBonCmde_DoubleClick(sender As Object, e As EventArgs) Handles ListBonCmde.DoubleClick
        If ViewLstCmde.RowCount > 0 Then
            Dim bool As Boolean = False

            For i = 0 To ViewLstCmde.RowCount - 1

                If CBool(ViewLstCmde.GetRowCellValue(i, "Choix")) = True Then
                    TxtReference.Text = ViewLstCmde.GetRowCellValue(i, "Référence").ToString()
                    TxtDesignation.Text = MettreApost(ViewLstCmde.GetRowCellValue(i, "Désignation").ToString())
                    TxtQte.Text = ViewLstCmde.GetRowCellValue(i, "Quantité").ToString()
                    TxtPu.Text = ViewLstCmde.GetRowCellValue(i, "Prix Unitaire").ToString()
                    TxtNewMont.Text = ViewLstCmde.GetRowCellValue(i, "Montant").ToString()
                    ViewLstCmde.DeleteSelectedRows()
                    bool = True
                End If
            Next

            If bool = False Then
                SuccesMsg("Veuillez cocher une ligne dans la liste des besoins")
            End If

        Else
            SuccesMsg("Veuillez ajouter une ligne dans la liste des besoins")
        End If

    End Sub

    Private Sub ModifierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModifierToolStripMenuItem.Click
        ListBonCmde_DoubleClick(sender, e)
    End Sub

    Private Sub SupprimerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupprimerToolStripMenuItem.Click
        If ViewLstCmde.RowCount > 0 Then
            Dim bool As Boolean = False

            For i = 0 To ViewLstCmde.RowCount - 1

                If CBool(ViewLstCmde.GetRowCellValue(i, "Choix")) = True Then
                    ViewLstCmde.DeleteSelectedRows()
                    bool = True
                End If
            Next

            If bool = False Then
                SuccesMsg("Veuillez cocher une ligne avant la suppression")
            End If

        Else
            SuccesMsg("Vous n'avez pas ajouté de ligne dans la liste des besoins")
        End If
    End Sub
End Class