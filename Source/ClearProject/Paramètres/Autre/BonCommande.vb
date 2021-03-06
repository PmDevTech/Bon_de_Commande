Imports System.Math
Imports DevExpress.XtraEditors.Repository
Imports MySql.Data.MySqlClient

Public Class BonCommande

    Dim tauxDollar As Decimal = 1
    Dim CfaGere As Boolean = True
    Dim dtboncommande = New DataTable
    Dim dtSignataire = New DataTable
    Dim ID_NumDAO() As String
    Dim TypeDossier As New List(Of String)
    Dim RefSoumis As New List(Of String)
    Dim ID_CodeLot() As String
    Dim CodeFournis As String = ""
    Dim MontantTotalDossier As String = ""
    Dim NumeroBonCommande As String = ""
    Dim NumDAO As String = ""
    Dim RefLot As String = ""
    Dim CodeFournisseur As String = ""
    Dim ConditionPaiement As String = ""
    Dim init As Integer = 0

    Private Sub ChargerNumDAO()
        CmbNumDAO.ResetText()
        CmbNumDAO.Properties.Items.Clear()

        Dim VerifMarche As Double = 0
        Dim VerifBonCommande As Double = 0
        Dim VerifResult As Integer = 0
        init = 0

        'DAO (Fournitures, Travaux, Autres Services)
        query = "SELECT d.NumeroDAO, l.RefLot FROM t_dao d, t_lotdao l, t_soumissionfournisseurclassement s WHERE d.NumeroDAO = l.NumeroDAO AND l.NumeroDAO = s.NumeroDAO AND d.DateFinJugement is not null AND d.statut_DAO <> 'Annulé' and d.CodeProjet = '" & ProjetEnCours & "' and s.Selectionne = 'OUI' and s.Attribue = 'OUI' GROUP by l.RefLot ORDER BY d.NumeroDAO"
        VerifExist_DAO_AMI_DP("DAO", query)

        'AMI
        query = "select N.NumeroDp, N.RefSoumis from t_ami A, t_consultant as C, t_soumissionconsultant as S, t_dp_negociation as N where C.NumeroDp = A.NumeroDAMI and C.RefConsult=S.RefConsult and S.RefSoumis=N.RefSoumis and S.Negociation='OUI' and S.RangConsult IS NOT NULL and S.EvalTechOk='OUI' and S.ConsultDisqualifie IS NULL and A.MethodeSelection = '3CV' ORDER BY S.RangConsult ASC LIMIT 3"
        VerifExist_DAO_AMI_DP("AMI", query)

        'DP
        query = "select N.NumeroDp, N.RefSoumis from t_consultant as C, t_soumissionconsultant as S, t_dp_negociation as N where C.RefConsult=S.RefConsult and S.RefSoumis=N.RefSoumis and S.Negociation='OUI' and S.RangFinal IS NOT NULL and S.EvalFinOk='OUI' and S.ConsultDisqualifie IS NULL ORDER BY S.RangFinal ASC"
        VerifExist_DAO_AMI_DP("DP", query)

    End Sub

    Private Sub VerifExist_DAO_AMI_DP(TypeRequete As String, requete As String)
        Try
            Dim VerifMarche As Double = 0
            Dim VerifBonCommande As Double = 0

            Dim dt As DataTable = ExcecuteSelectQuery(requete)
            ReDim Preserve ID_NumDAO(dt.Rows.Count)

            For Each rw As DataRow In dt.Rows

                If TypeRequete = "DAO" Then
                    query = "SELECT count(NumeroMarche) as Result from t_marchesigne WHERE NumeroDAO = '" & rw("NumeroDAO").ToString & "' and RefLot = '" & rw("RefLot").ToString & "' and CodeProjet = '" & ProjetEnCours & "'"
                Else
                    query = "SELECT count(NumeroMarche) as Result from t_marchesigne WHERE NumeroDAO = '" & rw("NumeroDp").ToString & "' and RefSoumis = '" & rw("RefSoumis").ToString & "' and CodeProjet = '" & ProjetEnCours & "'"
                End If

                VerifMarche = Val(ExecuteScallar(query))
                If VerifMarche > 0 Then
                    Continue For
                End If

                query = "SELECT count(RefBonCommande) as Result from t_boncommande WHERE NumeroDAO = '" & rw(0).ToString & "' and RefLot = '" & rw(1).ToString & "' and CodeProjet = '" & ProjetEnCours & "' and (Statut = 'Signé' or Statut = 'En cours')"
                VerifBonCommande = Val(ExecuteScallar(query))
                If VerifBonCommande > 0 Then
                    Continue For
                End If

                CmbNumDAO.Properties.Items.Add(MettreApost(rw(0).ToString))
                RefSoumis.Add(rw(1).ToString)
                If TypeRequete = "DAO" Then
                    TypeDossier.Add("DAO")
                ElseIf TypeRequete = "DP" Then
                    TypeDossier.Add("DP")
                Else
                    TypeDossier.Add("AMI")
                End If
                init += 1
            Next

        Catch ex As Exception
            SuccesMsg(ex.ToString)
        End Try

    End Sub

    Private Sub ChargerSignataire()
        CmbSignataire.ResetText()
        CmbSignataire.Properties.Items.Clear()

        query = "SELECT NomPren, Fonction FROM t_signataire WHERE CodeProjet = '" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbSignataire.Properties.Items.Add(MettreApost(rw("NomPren").ToString) & ", " & MettreApost(rw("Fonction").ToString))
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
            If RdParPassMarche.Checked Then
                CmbNumDAO.Enabled = True
                CmbCodeLot.Enabled = True
            End If
            Initialiser()
            LoadColonneListeBesoins()
            ChargerNumDAO()
            LoadColonneSignataire()
        ElseIf Liste_boncommande.AjoutModif = "Modifier" Then
            BtEnregistrer.Enabled = False
            BtModifier.Enabled = True
            Txtboncmde.Enabled = False
            Initialiser()
            Chargement()
        End If
    End Sub

    Private Sub Chargement()
        NumeroBonCommande = Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "N° Bon Commande").ToString
        NumDAO = Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "NumeroDAO").ToString
        RefLot = Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "RefLot").ToString
        CodeFournisseur = Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "CodeFournisseur").ToString
        ConditionPaiement = Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "ConditionPaiement").ToString
        CmbNumDAO.Text = NumDAO

        'Vérification du type de marché : Fournitures, Travaux, Services autres, Consultants
        query = "SELECT TypeMarche FROM t_dao WHERE CodeProjet = '" & ProjetEnCours & "' AND NumeroDAO = '" & EnleverApost(NumDAO) & "'"
        Dim TypeMarche As String = ExecuteScallar(query)

        If TypeMarche = "Fournitures" Or TypeMarche = "Travaux" Or TypeMarche.Contains("Service") Then
            'Récupération du code du lot
            query = "select CodeLot from t_lotdao where NumeroDAO = '" & EnleverApost(NumDAO) & "' and RefLot = '" & RefLot & "'"
            Dim CodeLot As String = ExecuteScallar(query)
            CmbCodeLot.Text = CodeLot
        Else
            CmbCodeLot.Text = ""
        End If

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
            Checktous.Visible = False

            query = "SELECT PrixOffreCorrigerRabaiCompris FROM t_soumissionfournisseurclassement WHERE CodeFournis = '" & CodeFournisseur & "' and CodeLot = '" & CmbCodeLot.Text & "' AND Selectionne = 'OUI' AND Attribue = 'OUI'"
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                MontantTotalDossier = rw("PrixOffreCorrigerRabaiCompris").ToString
            Next
        Else
            RdSansPassMarche.Checked = True
            RdSansPassMarche.Enabled = True
            RdParPassMarche.Enabled = False
            Checktous.Visible = True
        End If

        Dateboncmde.Text = Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "Date d'édition").ToString
        Txtboncmde.Text = Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "N° Bon Commande").ToString

        'Informations liées au fournisseur
        query = "SELECT NomFournis,AdresseCompleteFournis,TelFournis,CompteContribuableFournis,RegistreCommerceFournis FROM t_fournisseur WHERE CodeFournis = '" & CodeFournisseur & "'"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            TxtFournisseur.Text = MettreApost(rw("NomFournis").ToString)
            TxtAdresseFour.Text = MettreApost(rw("AdresseCompleteFournis").ToString)
            TxtTelFour.Text = MettreApost(rw("TelFournis").ToString)
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

        'Liste des besoins
        LoadColonneListeBesoins()
        RemplirDatagridListeBesoins()

        'Liste des signataires
        LoadColonneSignataire()
        RemplirDatagridSignataire()

        TxtDelaiLivraison.Text = MettreApost(Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "DelaiLivraison").ToString)
        TxtLieuLivraison.Text = MettreApost(Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "LieuLivraison").ToString)
        TxtIsntructionSpec.Text = MettreApost(Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "InstructionSpeciale").ToString)
        TxtIntituleMarche.Text = MettreApost(Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "Intitulé du marché").ToString)
        TxtReference.Text = MettreApost(Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "Référence").ToString)
        TxtDesignation.Text = MettreApost(Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "Désignation").ToString)
        TxtMontRabais.Text = Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "MontantRabais").ToString
        TxtAjustement.Text = Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "Ajustement").ToString
        TxtNewMont.Text = AfficherMonnaie(Liste_boncommande.ViewBoncommande.GetRowCellValue(Liste_boncommande.j, "MontantBCHT").ToString)
        TxtMontLettre.Text = MontantLettre(TxtNewMont.Text)
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

    Private Sub RemplirDatagridSignataire()
        query = "SELECT NomPren FROM t_bc_signataire WHERE CodeProjet = '" & ProjetEnCours & "' AND RefBonCommande = '" & NumeroBonCommande & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        Dim NewLine As DataTable = GCSignataire.DataSource
        Dim cpt As Decimal = 0

        For Each rw As DataRow In dt.Rows
            Dim drs = NewLine.NewRow()
            cpt += 1
            drs("N°") = cpt
            drs("Nom, Prénoms et Titre") = MettreApost(rw("NomPren").ToString)
            NewLine.Rows.Add(drs)
        Next
    End Sub

    Private Sub Initialiser()
        CmbCodeLot.ResetText()
        CmbNumDAO.ResetText()
        CmbSignataire.ResetText()
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
        TxtMontRabais.Text = ""
        TxtAjustement.Text = ""
        TxtNewMont.Text = ""
        TxtMontLettre.Text = ""
        TxtTVA.Text = ""
        TxtRemise.Text = ""
        TxtLibAutreTaxe.Text = ""
        TxtAutreTaxe.Text = ""
        Checktous.Checked = False
        dtboncommande.Rows.clear()
        dtSignataire.Rows.clear()
    End Sub

    Private Sub Initialiser2()
        CmbCodeLot.ResetText()
        CmbCodeLot.Properties.Items.Clear()
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
        TxtMontRabais.Text = ""
        TxtAjustement.Text = ""
        TxtNewMont.Text = ""
        TxtMontLettre.Text = ""
        TxtTVA.Text = ""
        TxtRemise.Text = ""
        TxtLibAutreTaxe.Text = ""
        TxtAutreTaxe.Text = ""
        Checktous.Checked = False
        dtboncommande.Rows.clear()
        CmbSignataire.ResetText()
        dtSignataire.Rows.clear()
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

        If RdParPassMarche.Checked Then
            ViewLstCmde.Columns("Choix").Visible = False
        ElseIf RdSansPassMarche.Checked Then
            ViewLstCmde.Columns("Choix").Visible = True
        End If

        ViewLstCmde.Columns("Quantité").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewLstCmde.Columns("Prix Unitaire").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewLstCmde.Columns("Montant").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    End Sub

    Private Sub GridBonCommande()

        Dim NewLine As DataTable = ListBonCmde.DataSource
        Dim cpt As Decimal = 0

        Dim drS = NewLine.NewRow()
        cpt += 1
        drS("Choix") = TabTrue(cpt - 1)
        drS("Référence") = MettreApost(TxtReference.Text)
        drS("Désignation") = MettreApost(TxtDesignation.Text)
        drS("Quantité") = AfficherMonnaie(TxtQte.Text)
        drS("Prix Unitaire") = AfficherMonnaie(TxtPu.Text)
        drS("Montant") = AfficherMonnaie(TxtNewMont.Text)
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

    Private Sub LoadColonneSignataire()
        dtSignataire.Columns.Clear()
        dtSignataire.Columns.Add("N°", Type.GetType("System.String"))
        dtSignataire.Columns.Add("Nom, Prénoms et Titre", Type.GetType("System.String"))
        GCSignataire.DataSource = dtSignataire

        GVSignataire.Columns("N°").Width = 3

        GVSignataire.Columns("N°").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        GVSignataire.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
        ColorRowGrid(GVSignataire, "[N°]='x'", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.Black)
    End Sub

    Private Sub RechargerSpecifications()

        Dim TypeMarche As String = ""
        Dim Reference As String = ""
        Dim Designation As String = ""
        Dim QTE As String = ""
        Dim PU As String = ""
        dtboncommande.Rows.clear()

        'récupération du type de marché
        query = "SELECT TypeMarche FROM t_dao WHERE CodeProjet = '" & ProjetEnCours & "' AND NumeroDAO = '" & EnleverApost(CmbNumDAO.Text) & "'"
        TypeMarche = ExecuteScallar(query)

        If TypeMarche = "Fournitures" Or TypeMarche.Contains("Service") Then
            query = "SELECT * FROM t_spectechfourniture WHERE NumeroDAO = '" & EnleverApost(CmbNumDAO.Text) & "' AND CodeLot = '" & CmbCodeLot.Text & "'"
        End If

        Dim NewLine As DataTable = ListBonCmde.DataSource
        Dim dt As DataTable = ExcecuteSelectQuery(query)

        For Each rw As DataRow In dt.Rows

            If TypeMarche = "Fournitures" Or TypeMarche.Contains("Service") Then
                Reference = rw("CodeCategorie").ToString
                Designation = MettreApost(rw("DescripFournit").ToString) & " (" & RechargerListeSpecifications(rw("RefSpecFournit").ToString) & ")"
                QTE = rw("QteFournit").ToString
                PU = AfficherMonnaie(RechargerPrixUnitaire(rw("RefSpecFournit").ToString))
            ElseIf TypeMarche = "Travaux" Then
                Reference = "Lot " & CmbCodeLot.Text
                Designation = TxtDesignation.Text
                QTE = "1"
                PU = TxtNewMont.Text
            End If

            Dim drS = NewLine.NewRow()
            drS("Choix") = TabTrue(0)
            drS("Référence") = Reference
            drS("Désignation") = Designation
            drS("Quantité") = AfficherMonnaie(QTE)
            drS("Prix Unitaire") = AfficherMonnaie(PU)
            drS("Montant") = AfficherMonnaie(CDbl(QTE) * CDbl(PU))
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

    Private Sub AfficherListeBesoinsAMI_DP()
        dtboncommande.Rows.clear()
        Dim QTE As String = "1"
        Dim PU As String = TxtNewMont.Text

        Dim NewLine As DataTable = ListBonCmde.DataSource
        Dim drS = NewLine.NewRow()
        drS("Choix") = TabTrue(0)
        drS("Référence") = ""
        drS("Désignation") = TxtIntituleMarche.Text
        drS("Quantité") = AfficherMonnaie(QTE)
        drS("Prix Unitaire") = AfficherMonnaie(PU)
        drS("Montant") = AfficherMonnaie(CDbl(QTE) * CDbl(PU))
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

    Private Function RechargerListeSpecifications(RefSpecFourniture As String) As String
        Dim Specification As String = ""

        Try
            query = "SELECT LibelleCaract, ValeurCaract FROM t_spectechcaract WHERE RefSpecFournit = '" & RefSpecFourniture & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                Specification += MettreApost(rw("LibelleCaract").ToString) & ":" & MettreApost(rw("ValeurCaract").ToString) & ", "
            Next
        Catch ex As Exception
            SuccesMsg(ex.ToString)
        End Try

        If Specification.Length >= 1 Then
            Specification = Mid(Specification, 1, (Specification.Length - 2))
        End If

        Return Specification
    End Function

    Private Function RechargerPrixUnitaire(RefSpecification As Decimal) As Decimal

        Dim PrixUnitaire As Decimal = 0

        Try
            query = "SELECT sf.PrixUnitaire FROM t_soumisprixfourniture sf, t_soumissionfournisseur s WHERE sf.RefSoumis = s.RefSoumis AND sf.RefSpecFournit = '" & RefSpecification & "' AND s.CodeFournis = '" & CodeFournis & "' AND s.CodeLot = '" & CmbCodeLot.Text & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                PrixUnitaire = CDbl(rw("PrixUnitaire").ToString)
            Next
        Catch ex As Exception
            SuccesMsg(ex.ToString)
        End Try

        Return PrixUnitaire

    End Function

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
                AlertMsg("Attention chiffre trop énorme!")
            End Try
        End If
    End Sub

    Private Sub TxtPu_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPu.KeyPress
        Select Case e.KeyChar
            Case ControlChars.CrLf
                If TxtIntituleMarche.Text = "" Then
                    SuccesMsg("Veuillez saisir l'intitulé du marché")
                    TxtIntituleMarche.Focus()
                ElseIf TxtDesignation.Text = "" Then
                    SuccesMsg("Veuillez saisir la désignation")
                    TxtDesignation.Focus()
                ElseIf TxtQte.Text = "" Then
                    SuccesMsg("Veuillez saisir la quantité")
                    TxtQte.Focus()
                ElseIf TxtPu.Text = "" Then
                    SuccesMsg("Veuillez saisir le prix unitaire")
                    TxtPu.Focus()
                ElseIf TxtNewMont.Text = "" Then
                    SuccesMsg("Le montant à payer n'a pas été calculé. Veuillez renseigner la quantité ou le prix unitaire.")
                Else
                    GridBonCommande()
                    InitFormulaireListeBesoins()
                    TxtReference.Focus()
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
            TxtRemise.Enabled = False
            TxtLibAutreTaxe.Enabled = False
            Checktous.Visible = False
            lc1.Visible = True
            lc2.Visible = True
            lc7.Visible = False
            lc8.Visible = False

            Initialiser()
            ChargerNumDAO()
            ChargerSignataire()
            LoadColonneListeBesoins()
        End If
    End Sub

    Private Sub RdSansPassMarche_CheckedChanged(sender As Object, e As EventArgs) Handles RdSansPassMarche.CheckedChanged
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
            TxtRemise.Enabled = True
            TxtLibAutreTaxe.Enabled = True
            Checktous.Visible = True
            lc1.Visible = False
            lc2.Visible = False
            lc7.Visible = True
            lc8.Visible = True

            Initialiser()
            ChargerSignataire()
            LoadColonneListeBesoins()
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

        Initialiser2()

        Dim VerifMarche As Double = 0
        Dim VerifBonCommande As Double = 0
        Dim TypeMarche As String = ""

        If CmbNumDAO.SelectedIndex <> -1 Then

            If TypeDossier(CmbNumDAO.SelectedIndex) = "DAO" Then
                CmbCodeLot.Enabled = True
                TxtTVA.Enabled = True
                TxtLibAutreTaxe.Enabled = True
            Else
                CmbCodeLot.Enabled = False
                TxtTVA.Enabled = False
                TxtLibAutreTaxe.Enabled = False
            End If

            Dim VerifTypeDossier As String = TypeDossier(CmbNumDAO.SelectedIndex)

            If VerifTypeDossier = "DAO" Then

                query = "SELECT TypeMarche FROM t_dao WHERE CodeProjet = '" & ProjetEnCours & "' AND NumeroDAO = '" & EnleverApost(CmbNumDAO.Text) & "'"
                TypeMarche = ExecuteScallar(query)

                If TypeMarche = "Fournitures" Or TypeMarche = "Travaux" Or TypeMarche.Contains("Service") Then
                    query = "SELECT IntituleDAO FROM t_dao WHERE CodeProjet = '" & ProjetEnCours & "' AND NumeroDAO = '" & EnleverApost(CmbNumDAO.Text) & "'"
                    Dim Intitule = ExecuteScallar(query)
                    TxtIntituleMarche.Text = MettreApost(Intitule.ToString)

                    query = "SELECT * from t_lotdao WHERE NumeroDAO = '" & EnleverApost(CmbNumDAO.Text) & "'"
                    Dim dt As DataTable = ExcecuteSelectQuery(query)
                    ReDim ID_CodeLot(dt.Rows.Count)
                    Dim i As Integer = 0
                    For Each rw As DataRow In dt.Rows
                        query = "SELECT count(NumeroMarche) as Result from t_marchesigne WHERE NumeroDAO = '" & rw("NumeroDAO").ToString & "' and RefLot = '" & rw("RefLot").ToString & "' and CodeProjet = '" & ProjetEnCours & "'"
                        VerifMarche = Val(ExecuteScallar(query))
                        If VerifMarche > 0 Then
                            Continue For
                        End If

                        query = "SELECT count(RefBonCommande) as Result from t_boncommande WHERE NumeroDAO = '" & rw("NumeroDAO").ToString & "' and RefLot = '" & rw("RefLot").ToString & "' and CodeProjet = '" & ProjetEnCours & "' and (Statut = 'Signé' or Statut = 'En cours')"
                        VerifBonCommande = Val(ExecuteScallar(query))
                        If VerifBonCommande > 0 Then
                            Continue For
                        End If

                        CmbCodeLot.Properties.Items.Add(rw("CodeLot").ToString)
                        ID_CodeLot(i) = rw("CodeLot").ToString
                        i += 1
                    Next

                End If

            Else
                CmbCodeLot.Text = ""
                CmbCodeLot.Enabled = False
                GroupControl6.Text = "Identification du Consultant"
                Label11.Text = "Consultant"
                TxtTVA.Enabled = False
                TxtLibAutreTaxe.Enabled = True

                'récupération de l'intitulé du marché
                If VerifTypeDossier = "AMI" Then
                    query = "SELECT LibelleMiss FROM t_ami WHERE NumeroDAMI = '" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet = '" & ProjetEnCours & "'"
                    Dim IntituleMarche As String = ExecuteScallar(query)
                    TxtIntituleMarche.Text = MettreApost(IntituleMarche)
                    'TxtDesignation.Text = MettreApost(IntituleMarche)
                ElseIf VerifTypeDossier = "DP" Then
                    query = "SELECT LibelleMiss FROM t_dp WHERE NumeroDp = '" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet = '" & ProjetEnCours & "'"
                    Dim IntituleMarche As String = ExecuteScallar(query)
                    TxtIntituleMarche.Text = MettreApost(IntituleMarche)
                    'TxtDesignation.Text = MettreApost(IntituleMarche)
                End If

                'Récupération des identifiants du consultant
                query = "select C.NomConsult, C.AdressConsult, C.TelConsult from t_consultant C, t_soumissionconsultant S where C.RefConsult = S.RefConsult AND C.NumeroDp = '" & EnleverApost(CmbNumDAO.Text) & "' AND S.RefSoumis = '" & RefSoumis(CmbNumDAO.SelectedIndex) & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    TxtFournisseur.Text = MettreApost(rw("NomConsult").ToString)
                    TxtAdresseFour.Text = MettreApost(rw("AdressConsult").ToString)
                    TxtTelFour.Text = MettreApost(rw("TelConsult").ToString)
                Next

                query = "SELECT MontantNego FROM t_dp_negociation WHERE CodeProjet = '" & ProjetEnCours & "' AND NumeroDp = '" & EnleverApost(CmbNumDAO.Text) & "' AND RefSoumis = '" & RefSoumis(CmbNumDAO.SelectedIndex) & "'"
                Dim MontantNego As String = ExecuteScallar(query)
                If MontantNego <> "" Then
                    TxtNewMont.Text = AfficherMonnaie(CDbl(MontantNego))
                End If

                AfficherListeBesoinsAMI_DP()

            End If
        End If

    End Sub

    Private Sub CmbCodeLot_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbCodeLot.SelectedIndexChanged

        TxtFournisseur.Text = ""
        TxtAdresseFour.Text = ""
        TxtTelFour.Text = ""
        TxtCCFour.Text = ""
        TxtRCCM.Text = ""
        TxtDesignation.Text = ""

        'Dim CodeFournis As String = ""
        Dim LibelleLot As String = ""
        Dim ResultQTE As Double = 0
        Dim MontantRabais As Double = 0
        Dim Ajustements As Double = 0

        query = "SELECT CodeFournis, NomFournis, AdresseCompleteFournis, TelFournis, CompteContribuableFournis, RegistreCommerceFournis FROM t_fournisseur WHERE CodeProjet = '" & ProjetEnCours & "' and CodeFournis IN (SELECT CodeFournis FROM t_soumissionfournisseurclassement where CodeLot = '" & CmbCodeLot.Text & "' and NumeroDAO = '" & EnleverApost(CmbNumDAO.Text) & "' and Selectionne = 'OUI' and Attribue = 'OUI')"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CodeFournis = rw("CodeFournis").ToString
            TxtFournisseur.Text = MettreApost(rw("NomFournis").ToString)
            TxtAdresseFour.Text = MettreApost(rw("AdresseCompleteFournis").ToString)
            TxtTelFour.Text = MettreApost(rw("TelFournis").ToString)
            TxtCCFour.Text = MettreApost(rw("CompteContribuableFournis").ToString)
            TxtRCCM.Text = MettreApost(rw("RegistreCommerceFournis").ToString)
        Next

        query = "SELECT PrixCorrigeOffre, PrixOffreCorrigerRabaiCompris, MontantRabais, (AjoutOmission + Ajustements + VariationMineure) as Ajustements FROM t_soumissionfournisseurclassement WHERE CodeFournis = '" & CodeFournis & "' and CodeLot = '" & CmbCodeLot.Text & "' AND Selectionne = 'OUI' AND Attribue = 'OUI'"
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt1.Rows
            TxtNewMont.Text = AfficherMonnaie(rw("PrixCorrigeOffre").ToString)
            TxtMontRabais.Text = AfficherMonnaie(rw("MontantRabais").ToString)
            TxtAjustement.Text = AfficherMonnaie(rw("Ajustements").ToString)
            MontantTotalDossier = rw("PrixOffreCorrigerRabaiCompris").ToString
        Next

        query = "SELECT LibelleLot from t_lotdao WHERE NumeroDAO = '" & CmbNumDAO.Text & "' AND CodeLot = '" & CmbCodeLot.Text & "'"
        LibelleLot = ExecuteScallar(query)
        TxtDesignation.Text = MettreApost(LibelleLot)

        RechargerSpecifications()

    End Sub

    Private Sub TxtNewMont_TextChanged(sender As Object, e As EventArgs) Handles TxtNewMont.TextChanged
        If RdParPassMarche.Checked Then
            Try
                TxtMontLettre.Text = MontantLettre(TxtNewMont.Text)
            Catch ex As Exception
                AlertMsg("Attention dépassement de caractère. Chiffre trop énorme!")
            End Try
        End If
    End Sub

    Private Sub BtEnregistrer_Click(sender As Object, e As EventArgs) Handles BtEnregistrer.Click
        If RdParPassMarche.Checked Then

            If CmbNumDAO.SelectedIndex = -1 Then
                SuccesMsg("Veuillez sélectionner le numéro du DAO")
                CmbNumDAO.Focus()
            ElseIf CmbCodeLot.Enabled = True Then
                If CmbCodeLot.SelectedIndex = -1 Then
                    SuccesMsg("Veuillez sélectionner le code du lot")
                    CmbCodeLot.Focus()
                End If
            ElseIf Dateboncmde.Text = "" Then
                SuccesMsg("Veuillez choisir la date d'élaboration du bon de commande")
                Dateboncmde.Focus()
            ElseIf TxtLibAutreTaxe.Text <> "" And TxtAutreTaxe.Text = "" Then
                SuccesMsg("Veuillez saisir le pourcentage de la taxe correspondant aux autres taxes")
                TxtAutreTaxe.Focus()
            Else

                If GVSignataire.RowCount > 0 Then
                    Dim Save2 As String = ""
                    For i = 0 To GVSignataire.RowCount - 1
                        Save2 = "insert into t_bc_signataire values(NULL,'" & EnleverApost(Txtboncmde.Text) & "','" & EnleverApost(GVSignataire.GetRowCellValue(i, "Nom, Prénoms et Titre")) & "','" & GVSignataire.GetRowCellValue(i, "N°") & "','" & ProjetEnCours & "')"
                        ExecuteNonQuery(Save2)
                    Next
                Else
                    SuccesMsg("Veuillez ajouter au moins un signataire avant l'enregistrement")
                    CmbSignataire.Focus()
                    Exit Sub
                End If

                If ViewLstCmde.RowCount > 0 Then
                    'Dim sauver As String
                    For i = 0 To ViewLstCmde.RowCount - 1
                        Dim sauver As String = "insert into t_bc_listebesoins values(NULL,'" & EnleverApost(Txtboncmde.Text) & "','" & EnleverApost(ViewLstCmde.GetRowCellValue(i, "Référence")) & "','" & EnleverApost(ViewLstCmde.GetRowCellValue(i, "Désignation")) & "','" & ViewLstCmde.GetRowCellValue(i, "Quantité") & "','" & ViewLstCmde.GetRowCellValue(i, "Prix Unitaire") & "','" & CDbl(ViewLstCmde.GetRowCellValue(i, "Montant")) & "')"
                        ExecuteNonQuery(sauver)
                        'ExecuteNonQuery("insert into t_bc_listebesoins values(NULL,'" & EnleverApost(Txtboncmde.Text) & "','" & EnleverApost(ViewLstCmde.GetRowCellValue(i, "Référence")) & "','" & EnleverApost(ViewLstCmde.GetRowCellValue(i, "Désignation")) & "','" & ViewLstCmde.GetRowCellValue(i, "Quantité") & "','" & ViewLstCmde.GetRowCellValue(i, "Prix Unitaire") & "','" & CDbl(ViewLstCmde.GetRowCellValue(i, "Montant")) & "')")
                        'InputBox("", "", sauver)
                    Next
                Else
                    SuccesMsg("Veuillez ajouter la liste des besoins avant l'enregistrement")
                    Exit Sub
                End If

                Dim Annee As String = CStr(Now.Year)
                Dim Dossier As String = TypeDossier(CmbNumDAO.SelectedIndex)

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
                Dim MontantOffre As Double = 0
                Dim MontantRabais As Double = 0

                'récupération du montant hors taxe
                Dim Requete As String = ""
                Requete = "SELECT SUM(PrixTotal) as MontantHT FROM t_bc_listebesoins WHERE RefBonCommande = '" & EnleverApost(Txtboncmde.Text) & "'"
                MontantHT = ExecuteScallar(Requete)

                If TxtMontRabais.Text = "" Then
                    MontantOffre = MontantHT - MontantRabais
                Else
                    MontantOffre = MontantHT - CDbl(TxtMontRabais.Text)
                End If

                If TxtRemise.Text = "" Then
                    Remise = ""
                    MontantRemise = 0
                End If

                MontantNetHT = CDbl(TxtNewMont.Text)

                If TxtTVA.Text = "" Then
                    TVA = ""
                    MontantTVA = 0
                Else
                    TVA = TxtTVA.Text
                    MontantTVA = Math.Round(MontantNetHT * (CDbl(TVA) / 100))
                End If

                If TxtLibAutreTaxe.Text = "" Then
                    AutreTaxe = ""
                    MontantAutreTaxe = 0
                Else
                    AutreTaxe = TxtAutreTaxe.Text
                    MontantAutreTaxe = Math.Round(MontantNetHT * (CDbl(AutreTaxe) / 100))
                End If

                If MontantTotalDossier = "" Then
                    MontantTOTAL = 0
                Else
                    MontantTOTAL = CDbl(MontantTotalDossier)
                End If

                MontantTotalTTC = MontantNetHT + MontantTVA + MontantAutreTaxe

                'Vérification du type de marché : Fournitures, Travaux, Services autres, Consultants
                query = "SELECT TypeMarche FROM t_dao WHERE CodeProjet = '" & ProjetEnCours & "' AND NumeroDAO = '" & EnleverApost(CmbNumDAO.Text) & "'"
                Dim TypeMarche As String = ExecuteScallar(query)

                If Dossier = "DAO" Then
                    'récupération de la référence du lot
                    Dim Receive1 As String = ""
                    Receive1 = "SELECT RefLot FROM t_lotdao WHERE CodeLot = '" & EnleverApost(CmbCodeLot.Text) & "' AND NumeroDAO = '" & EnleverApost(CmbNumDAO.Text) & "'"
                    RefLot = ExecuteScallar(Receive1)

                    'Mise à jour dans la table t_fournisseur
                    Dim Modif As String = ""
                    Modif = "UPDATE t_fournisseur SET AdresseCompleteFournis = '" & EnleverApost(TxtAdresseFour.Text) & "', TelFournis = '" & EnleverApost(TxtTelFour.Text) & "', CompteContribuableFournis = '" & EnleverApost(TxtCCFour.Text) & "', RegistreCommerceFournis = '" & EnleverApost(TxtRCCM.Text) & "' WHERE CodeFournis = '" & CodeFournis.ToString & "'"
                    ExecuteNonQuery(Modif)
                Else
                    RefLot = RefSoumis(CmbNumDAO.SelectedIndex)

                    'Enregistrement du Fournisseur
                    Dim Save4 As String = ""
                    Save4 = "INSERT INTO t_fournisseur (CodeFournis,NomFournis,AdresseCompleteFournis,TelFournis,CompteContribuableFournis,RegistreCommerceFournis,NumeroDAO,NomAch,CodeProjet) VALUES (NULL,'" & EnleverApost(TxtFournisseur.Text) & "','" & EnleverApost(TxtAdresseFour.Text) & "','" & EnleverApost(TxtTelFour.Text) & "','" & EnleverApost(TxtCCFour.Text) & "','" & EnleverApost(TxtRCCM.Text) & "','" & EnleverApost(Txtboncmde.Text) & "','" & "" & "','" & ProjetEnCours & "')"
                    ExecuteNonQuery(Save4)

                    Dim Receive2 As String = ""
                    Receive2 = "SELECT CodeFournis FROM t_fournisseur WHERE NumeroDAO = '" & EnleverApost(Txtboncmde.Text) & "' and CodeProjet = '" & ProjetEnCours & "'"
                    CodeFournis = ExecuteScallar(Receive2)
                End If

                Dim Save3 As String = ""
                'insertion dans la table t_boncommande
                Save3 = "INSERT INTO t_boncommande values('" & nbBonCommande & "','" & EnleverApost(Txtboncmde.Text) & "','" & annee & "', '" & CInt(CodeFournis) & "','" & ChoixElabBC & "','" & EnleverApost(CmbNumDAO.Text) & "','" & RefLot & "','" & EnleverApost(TxtIntituleMarche.Text) & "','" & DateBC & "','" & ConditionPaiement & "','" & EnleverApost(TxtDelaiLivraison.Text) & "','"
                Save3 &= EnleverApost(TxtLieuLivraison.Text) & "','" & EnleverApost(TxtIsntructionSpec.Text) & "','" & EnleverApost(TxtReference.Text) & "','" & EnleverApost(TxtDesignation.Text) & "','" & TxtMontRabais.Text & "','" & MontantOffre & "','" & TxtAjustement.Text & "','" & CDbl(MontantHT) & "','" & TVA & "','" & MontantTVA.ToString.Replace(",", ".") & "','" & Remise & "','" & MontantRemise.ToString.Replace(",", ".") & "','" & EnleverApost(TxtLibAutreTaxe.Text) & "','" & AutreTaxe & "','" & MontantAutreTaxe.ToString.Replace(",", ".") & "','" & MontantNetHT.ToString.Replace(",", ".") & "','" & MontantTOTAL.ToString.Replace(",", ".") & "','" & MontantTotalTTC.ToString.Replace(",", ".") & "', 'En cours','" & cur_User & "','" & ProjetEnCours & "','" & Dossier & "')"
                ExecuteNonQuery(Save3)

                SuccesMsg("Enregistrement effectué avec succès")
                Initialiser()
                ChargerNumDAO()
                Liste_boncommande.LoadColonneBonCommande()
                Liste_boncommande.RemplirDataGrid()
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
                    'Dim sauvegarde As String = ""
                    For i = 0 To ViewLstCmde.RowCount - 1
                        If CBool(ViewLstCmde.GetRowCellValue(i, "Choix")) = True Then
                            Dim sauvegarde As String = "insert into t_bc_listebesoins values(NULL,'" & EnleverApost(Txtboncmde.Text) & "','" & EnleverApost(ViewLstCmde.GetRowCellValue(i, "Référence")) & "','" & EnleverApost(ViewLstCmde.GetRowCellValue(i, "Désignation")) & "','" & ViewLstCmde.GetRowCellValue(i, "Quantité") & "','" & ViewLstCmde.GetRowCellValue(i, "Prix Unitaire") & "','" & CDbl(ViewLstCmde.GetRowCellValue(i, "Montant")) & "')"
                            ExecuteNonQuery(sauvegarde)

                            bool = True
                        End If
                    Next

                    If bool = False Then
                        SuccesMsg("Veuillez cocher au moins une ligne dans la liste des besoins")
                        Exit Sub
                    End If
                Else
                    SuccesMsg("Veuillez ajouter la liste de vos besoins avant l'enregistrement")
                    TxtIntituleMarche.Focus()
                    Exit Sub
                End If

                If GVSignataire.RowCount > 0 Then
                    Dim Save2 As String = ""
                    For i = 0 To GVSignataire.RowCount - 1
                        Save2 = "insert into t_bc_signataire values(NULL,'" & EnleverApost(Txtboncmde.Text) & "','" & EnleverApost(GVSignataire.GetRowCellValue(i, "Nom, Prénoms et Titre")) & "','" & GVSignataire.GetRowCellValue(i, "N°") & "','" & ProjetEnCours & "')"
                        ExecuteNonQuery(Save2)
                    Next
                Else
                    SuccesMsg("Veuillez ajouter au moins un signataire avant l'enregistrement")
                    CmbSignataire.Focus()
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
                Dim MontantOffre As Double = 0

                'récupération du montant hors taxe
                Dim Receive1 As String = ""
                Receive1 = "SELECT SUM(PrixTotal) as MontantHT FROM t_bc_listebesoins WHERE RefBonCommande = '" & EnleverApost(Txtboncmde.Text) & "'"
                MontantHT = ExecuteScallar(Receive1)

                If TxtRemise.Text = "" Then
                    Remise = ""
                    MontantRemise = 0
                Else
                    Remise = TxtRemise.Text
                    MontantRemise = Math.Round(CDbl(MontantHT) * (CDbl(Remise) / 100))
                End If

                MontantNetHT = CDbl(MontantHT) - MontantRemise

                If TxtTVA.Text = "" Then
                    TVA = ""
                    MontantTVA = 0
                Else
                    TVA = TxtTVA.Text
                    MontantTVA = Math.Round(MontantNetHT * (CDbl(TVA) / 100))
                End If

                MontantTOTAL = MontantNetHT + MontantTVA

                If TxtLibAutreTaxe.Text = "" Then
                    AutreTaxe = ""
                    MontantAutreTaxe = 0
                Else
                    AutreTaxe = TxtAutreTaxe.Text
                    MontantAutreTaxe = Math.Round(MontantNetHT * (CDbl(AutreTaxe) / 100))
                End If

                MontantTotalTTC = MontantNetHT + MontantTVA + MontantAutreTaxe

                'Enregistrement du Fournisseur
                Dim Save3 As String = ""
                Save3 = "INSERT INTO t_fournisseur (CodeFournis,NomFournis,AdresseCompleteFournis,TelFournis,CompteContribuableFournis,RegistreCommerceFournis,NumeroDAO,NomAch,CodeProjet) VALUES (NULL,'" & EnleverApost(TxtFournisseur.Text) & "','" & EnleverApost(TxtAdresseFour.Text) & "','" & EnleverApost(TxtTelFour.Text) & "','" & EnleverApost(TxtCCFour.Text) & "','" & EnleverApost(TxtRCCM.Text) & "','" & EnleverApost(Txtboncmde.Text) & "','" & "" & "','" & ProjetEnCours & "')"
                ExecuteNonQuery(Save3)

                Dim Receive2 As String = ""
                Receive2 = "SELECT CodeFournis FROM t_fournisseur WHERE NumeroDAO = '" & EnleverApost(Txtboncmde.Text) & "' and CodeProjet = '" & ProjetEnCours & "'"
                CodeFournis = ExecuteScallar(Receive2)

                'insertion dans la table t_boncommande
                Dim Save4 As String = ""
                Save4 = "INSERT INTO t_boncommande values('" & nbBonCommande & "','" & EnleverApost(Txtboncmde.Text) & "','" & Annee & "','" & CInt(CodeFournis) & "','" & ChoixElabBC & "','" & "" & "','" & "" & "','" & EnleverApost(TxtIntituleMarche.Text) & "','" & DateBC & "','" & ConditionPaiement & "','" & EnleverApost(TxtDelaiLivraison.Text) & "','"
                Save4 &= EnleverApost(TxtLieuLivraison.Text) & "','" & EnleverApost(TxtIsntructionSpec.Text) & "','" & "" & "','" & "" & "','" & "" & "','" & MontantOffre & "','" & "" & "','" & CDbl(MontantHT) & "','" & TVA & "','" & MontantTVA.ToString.Replace(",", ".") & "','" & Remise & "','" & MontantRemise.ToString.Replace(",", ".") & "','" & EnleverApost(TxtLibAutreTaxe.Text) & "','" & AutreTaxe & "','" & MontantAutreTaxe.ToString.Replace(",", ".") & "','" & MontantNetHT.ToString.Replace(",", ".") & "','" & MontantTOTAL.ToString.Replace(",", ".") & "','" & MontantTotalTTC.ToString.Replace(",", ".") & "', 'En cours','" & cur_User & "','" & ProjetEnCours & "','" & "" & "')"
                ExecuteNonQuery(Save4)

                SuccesMsg("Enregistrement effectué avec succès")
                Initialiser()
                Liste_boncommande.LoadColonneBonCommande()
                Liste_boncommande.RemplirDataGrid()
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
                If ViewLstCmde.RowCount > 0 Then
                    Dim Supp As String = ""
                    'suppression puis ajout dans la table des besoins 
                    Supp = "delete from t_bc_listebesoins where RefBonCommande = '" & NumeroBonCommande & "'"
                    ExecuteNonQuery(Supp)

                    'Dim Save1 As String = ""
                    For i = 0 To ViewLstCmde.RowCount - 1
                        'Save1 = "insert into t_bc_listebesoins values(NULL,'" & EnleverApost(Txtboncmde.Text) & "','" & EnleverApost(ViewLstCmde.GetRowCellValue(i, "Référence")) & "','" & EnleverApost(ViewLstCmde.GetRowCellValue(i, "Désignation")) & "','" & ViewLstCmde.GetRowCellValue(i, "Quantité") & "','" & ViewLstCmde.GetRowCellValue(i, "Prix Unitaire") & "','" & CDbl(ViewLstCmde.GetRowCellValue(i, "Montant")) & "')"
                        ExecuteNonQuery("insert into t_bc_listebesoins values(NULL,'" & EnleverApost(Txtboncmde.Text) & "','" & EnleverApost(ViewLstCmde.GetRowCellValue(i, "Référence")) & "','" & EnleverApost(ViewLstCmde.GetRowCellValue(i, "Désignation")) & "','" & ViewLstCmde.GetRowCellValue(i, "Quantité") & "','" & ViewLstCmde.GetRowCellValue(i, "Prix Unitaire") & "','" & CDbl(ViewLstCmde.GetRowCellValue(i, "Montant")) & "')")
                    Next
                Else
                    SuccesMsg("Veuillez ajouter la liste des besoins avant la modification")
                    Exit Sub
                End If

                'suppression puis ajout dans la table des signataires
                Dim Supprime As String = ""
                Supprime = "delete from t_bc_signataire where RefBonCommande = '" & NumeroBonCommande & "'"
                ExecuteNonQuery(Supprime)

                If GVSignataire.RowCount > 0 Then
                    Dim Save2 As String = ""
                    For i = 0 To GVSignataire.RowCount - 1
                        Save2 = "insert into t_bc_signataire values(NULL,'" & EnleverApost(Txtboncmde.Text) & "','" & EnleverApost(GVSignataire.GetRowCellValue(i, "Nom, Prénoms et Titre")) & "','" & GVSignataire.GetRowCellValue(i, "N°") & "','" & ProjetEnCours & "')"
                        ExecuteNonQuery(Save2)
                    Next
                Else
                    SuccesMsg("Veuillez ajouter au moins un signataire avant la modification")
                    CmbSignataire.Focus()
                    Exit Sub
                End If

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

                'Dim MontantHT As String = TxtNewMont.Text
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
                Dim Receive1 As String = ""
                Receive1 = "SELECT SUM(PrixTotal) as MontantHT FROM t_bc_listebesoins WHERE RefBonCommande = '" & EnleverApost(Txtboncmde.Text) & "'"
                MontantHT = ExecuteScallar(Receive1)

                If TxtRemise.Text = "" Then
                    Remise = ""
                    MontantRemise = 0
                End If

                MontantNetHT = CDbl(TxtNewMont.Text)

                If TxtTVA.Text = "" Then
                    TVA = ""
                    MontantTVA = 0
                Else
                    TVA = TxtTVA.Text
                    MontantTVA = Math.Round(MontantNetHT * (CDbl(TVA) / 100))
                End If

                If TxtLibAutreTaxe.Text = "" Then
                    AutreTaxe = ""
                    MontantAutreTaxe = 0
                End If

                If MontantTotalDossier = "" Then
                    MontantTOTAL = 0
                Else
                    MontantTOTAL = CDbl(MontantTotalDossier)
                End If

                MontantTotalTTC = MontantNetHT + MontantTVA

                'Mise à jour dans la table t_fournisseur
                Dim Modif As String = ""
                Modif = "UPDATE t_fournisseur SET AdresseCompleteFournis = '" & EnleverApost(TxtAdresseFour.Text) & "', TelFournis = '" & EnleverApost(TxtTelFour.Text) & "', CompteContribuableFournis = '" & EnleverApost(TxtCCFour.Text) & "', RegistreCommerceFournis = '" & EnleverApost(TxtRCCM.Text) & "' WHERE CodeFournis = '" & CodeFournisseur & "'"
                ExecuteNonQuery(Modif)

                Dim verif As String = ""
                'mise à jour dans la table t_boncommande
                verif = "UPDATE t_boncommande set DateCommande = '" & DateBC & "', ConditionsPaiement = '" & ConditionPaiement & "', DelaiLivraison = '" & EnleverApost(TxtDelaiLivraison.Text) & "'"
                verif &= ", LieuLivraison = '" & EnleverApost(TxtLieuLivraison.Text) & "', InstructionSpeciale = '" & EnleverApost(TxtIsntructionSpec.Text) & "', MontantBCHT = '" & CDbl(MontantHT) & "', PcrtTVA='" & TVA & "', MontantTVA='" & MontantTVA.ToString.Replace(",", ".") & "', PcrtRemise='" & Remise & "', MontantRemise = '" & MontantRemise.ToString.Replace(",", ".") & "'"
                verif &= ", AutreTaxe='" & EnleverApost(TxtLibAutreTaxe.Text) & "', PcrtAutreTaxe = '" & AutreTaxe & "', MontantAutreTaxe = '" & MontantAutreTaxe.ToString.Replace(",", ".") & "', MontantNetHT = '" & MontantNetHT.ToString.Replace(",", ".") & "', MontantTotal = '" & MontantTOTAL.ToString.Replace(",", ".") & "', MontantTotalTTC = '" & MontantTotalTTC.ToString.Replace(",", ".") & "', EMP_ID = '" & cur_User & "', CodeProjet = '" & ProjetEnCours & "' where RefBonCommande = '" & NumeroBonCommande & "'"
                ExecuteNonQuery(verif)

                SuccesMsg("Modification effectuée avec succès")
                Initialiser()
                Liste_boncommande.LoadColonneBonCommande()
                Liste_boncommande.RemplirDataGrid()
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
                    Dim Supp As String = ""
                    Supp = "delete from t_bc_listebesoins where RefBonCommande = '" & NumeroBonCommande & "'"
                    ExecuteNonQuery(Supp)

                    Dim bool As Boolean = False
                    For i = 0 To ViewLstCmde.RowCount - 1
                        Dim Save1 As String = ""
                        If CBool(ViewLstCmde.GetRowCellValue(i, "Choix")) = True Then
                            Save1 = "insert into t_bc_listebesoins values(NULL,'" & EnleverApost(Txtboncmde.Text) & "','" & EnleverApost(ViewLstCmde.GetRowCellValue(i, "Référence")) & "','" & EnleverApost(ViewLstCmde.GetRowCellValue(i, "Désignation")) & "','" & ViewLstCmde.GetRowCellValue(i, "Quantité") & "','" & ViewLstCmde.GetRowCellValue(i, "Prix Unitaire") & "','" & CDbl(ViewLstCmde.GetRowCellValue(i, "Montant")) & "')"
                            ExecuteNonQuery(Save1)

                            bool = True
                        End If
                    Next

                    If bool = False Then
                        SuccesMsg("Veuillez cocher au moins une ligne dans la liste des besoins")
                        Exit Sub
                    End If
                Else
                    SuccesMsg("Veuillez ajouter la liste de vos besoins avant la modification")
                    TxtReference.Focus()
                    Exit Sub
                End If

                'suppression puis ajout dans la table des signataires
                Dim Supprime As String = ""
                Supprime = "delete from t_bc_signataire where RefBonCommande = '" & NumeroBonCommande & "'"
                ExecuteNonQuery(Supprime)

                If GVSignataire.RowCount > 0 Then
                    Dim Save2 As String = ""
                    For i = 0 To GVSignataire.RowCount - 1
                        Save2 = "insert into t_bc_signataire values(NULL,'" & EnleverApost(Txtboncmde.Text) & "','" & EnleverApost(GVSignataire.GetRowCellValue(i, "Nom, Prénoms et Titre")) & "','" & GVSignataire.GetRowCellValue(i, "N°") & "','" & ProjetEnCours & "')"
                        ExecuteNonQuery(Save2)
                    Next
                Else
                    SuccesMsg("Veuillez ajouter au moins un signataire avant la modification")
                    CmbSignataire.Focus()
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
                Dim Receive As String = ""
                Receive = "SELECT SUM(PrixTotal) as MontantHT FROM t_bc_listebesoins WHERE RefBonCommande = '" & EnleverApost(Txtboncmde.Text) & "'"
                MontantHT = ExecuteScallar(Receive)

                If TxtRemise.Text = "" Then
                    Remise = ""
                    MontantRemise = 0
                Else
                    Remise = TxtRemise.Text
                    MontantRemise = Math.Round(CDbl(MontantHT) * (CDbl(Remise) / 100))
                End If

                MontantNetHT = CDbl(MontantHT) - MontantRemise

                If TxtTVA.Text = "" Then
                    TVA = ""
                    MontantTVA = 0
                Else
                    TVA = TxtTVA.Text
                    MontantTVA = Math.Round(MontantNetHT * (CDbl(TVA) / 100))
                End If

                MontantTOTAL = MontantNetHT + MontantTVA

                If TxtLibAutreTaxe.Text = "" Then
                    AutreTaxe = ""
                    MontantAutreTaxe = 0
                Else
                    AutreTaxe = TxtAutreTaxe.Text
                    MontantAutreTaxe = Math.Round(MontantNetHT * (CDbl(AutreTaxe) / 100))
                End If

                MontantTotalTTC = MontantNetHT + MontantTVA + MontantAutreTaxe

                'modification du Fournisseur
                Dim Modif As String = ""
                Modif = "UPDATE t_fournisseur set NomFournis='" & EnleverApost(TxtFournisseur.Text) & "',AdresseCompleteFournis='" & EnleverApost(TxtAdresseFour.Text) & "',TelFournis='" & EnleverApost(TxtTelFour.Text) & "',CompteContribuableFournis='" & EnleverApost(TxtCCFour.Text) & "',RegistreCommerceFournis='" & EnleverApost(TxtRCCM.Text) & "',NumeroDAO='" & EnleverApost(Txtboncmde.Text) & "', CodeProjet='" & ProjetEnCours & "' where CodeFournis = '" & CodeFournisseur & "'"
                ExecuteNonQuery(Modif)

                'mise à jour dans la table t_boncommande
                Dim verif As String = ""
                verif = "UPDATE t_boncommande set IntituleMarche='" & EnleverApost(TxtIntituleMarche.Text) & "', DateCommande = '" & DateBC & "', ConditionsPaiement = '" & ConditionPaiement & "', DelaiLivraison = '" & EnleverApost(TxtDelaiLivraison.Text) & "'"
                verif &= ", LieuLivraison = '" & EnleverApost(TxtLieuLivraison.Text) & "', InstructionSpeciale = '" & EnleverApost(TxtIsntructionSpec.Text) & "', MontantBCHT = '" & CDbl(MontantHT) & "', PcrtTVA='" & TVA & "', MontantTVA='" & MontantTVA.ToString.Replace(",", ".") & "', PcrtRemise='" & Remise & "', MontantRemise = '" & MontantRemise.ToString.Replace(",", ".") & "'"
                verif &= ", AutreTaxe='" & EnleverApost(TxtLibAutreTaxe.Text) & "', PcrtAutreTaxe = '" & AutreTaxe & "', MontantAutreTaxe = '" & MontantAutreTaxe.ToString.Replace(",", ".") & "', MontantNetHT = '" & MontantNetHT.ToString.Replace(",", ".") & "', MontantTotal = '" & MontantTOTAL.ToString.Replace(",", ".") & "', MontantTotalTTC = '" & MontantTotalTTC.ToString.Replace(",", ".") & "', EMP_ID = '" & cur_User & "', CodeProjet = '" & ProjetEnCours & "' where RefBonCommande = '" & NumeroBonCommande & "'"
                ExecuteNonQuery(verif)

                SuccesMsg("Modification effectuée avec succès")
                Initialiser()
                Liste_boncommande.LoadColonneBonCommande()
                Liste_boncommande.RemplirDataGrid()
            End If
        End If
    End Sub

    Private Sub ModifierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModifierToolStripMenuItem.Click
        If RdSansPassMarche.Checked Then
            If ViewLstCmde.RowCount > 0 Then
                Dim bool As Boolean = False

                For i = 0 To ViewLstCmde.RowCount - 1
                    If CBool(ViewLstCmde.GetRowCellValue(i, "Choix")) = True Then
                        If TxtDesignation.Text = "" And TxtQte.Text = "" And TxtPu.Text = "" Then
                            TxtReference.Text = ViewLstCmde.GetRowCellValue(i, "Référence").ToString()
                            TxtDesignation.Text = MettreApost(ViewLstCmde.GetRowCellValue(i, "Désignation").ToString())
                            TxtQte.Text = ViewLstCmde.GetRowCellValue(i, "Quantité").ToString()
                            TxtPu.Text = ViewLstCmde.GetRowCellValue(i, "Prix Unitaire").ToString()
                            TxtNewMont.Text = ViewLstCmde.GetRowCellValue(i, "Montant").ToString()
                            ViewLstCmde.DeleteSelectedRows()
                            bool = True
                        Else
                            SuccesMsg("Veuillez terminer l'ajout du besoin en cours")
                            Exit Sub
                        End If
                    End If
                Next

                If bool = False Then
                    SuccesMsg("Veuillez cocher une ligne dans la liste des besoins")
                End If

            Else
                SuccesMsg("Veuillez ajouter au moins un besoin dans la liste des besoins")
            End If
        End If
    End Sub

    Private Sub SupprimerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupprimerToolStripMenuItem.Click

        If RdSansPassMarche.Checked Then
            If ViewLstCmde.RowCount > 0 Then
                Dim bool As Boolean = False

                For i = 0 To ViewLstCmde.RowCount - 1

                    If CBool(ViewLstCmde.GetRowCellValue(i, "Choix")) = True Then
                        If TxtDesignation.Text = "" And TxtQte.Text = "" And TxtPu.Text = "" Then
                            ViewLstCmde.DeleteSelectedRows()
                            bool = True
                        Else
                            SuccesMsg("Veuillez terminer l'ajout du besoin en cours")
                            Exit Sub
                        End If
                    End If
                Next

                If bool = False Then
                    SuccesMsg("Veuillez cocher une ligne avant la suppression")
                End If
            Else
                SuccesMsg("Veuillez ajouter au moins un besoin dans la liste des besoins")
            End If
        End If
    End Sub

    Private Sub BtnAjouter_Click(sender As Object, e As EventArgs) Handles BtnAjouter.Click

        Dim NomSignataire As String = ""
        Dim Ajout As Boolean = True
        Dim NewLine As DataTable = GCSignataire.DataSource
        Dim drS = NewLine.NewRow()

        If CmbSignataire.SelectedIndex = -1 Then
            SuccesMsg("Veuillez sélectionner un signataire")
            CmbSignataire.Focus()
        Else
            If GVSignataire.RowCount > 0 Then

                If GVSignataire.RowCount > 3 Then
                    SuccesMsg("Nombre maximum de signataires atteint! Vous ne pouvez plus ajouter de signataire.")
                    CmbSignataire.Text = ""
                    Exit Sub
                Else
                    For i = 0 To GVSignataire.RowCount - 1
                        NomSignataire = GVSignataire.GetRowCellValue(i, "Nom, Prénoms et Titre")
                        If NomSignataire = CmbSignataire.Text Then
                            SuccesMsg("Vous avez déjà ajouté ce nom à la liste des signataires")
                            Ajout = False
                            Exit For
                        End If
                    Next
                End If
            End If

            If Ajout = True Then
                drS("N°") = GVSignataire.RowCount + 1
                drS("Nom, Prénoms et Titre") = CmbSignataire.Text
                NewLine.Rows.Add(drS)
                CmbSignataire.Text = ""
            End If
        End If

    End Sub

    Private Sub SupprimerLaLigneToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupprimerLaLigneToolStripMenuItem.Click
        If GVSignataire.RowCount > 0 Then
            If ConfirmMsg("Voulez-vous vraiment supprimer la ligne?") = DialogResult.Yes Then
                GVSignataire.GetDataRow(GVSignataire.FocusedRowHandle).Delete()
                RechargerNumSignataire(GVSignataire, "N°")
            End If
        End If
    End Sub

    Private Sub GCSignataire_Click(sender As Object, e As EventArgs) Handles GCSignataire.Click
        If GVSignataire.RowCount > 0 Then
            drx = GVSignataire.GetDataRow(GVSignataire.FocusedRowHandle)
            Dim IDL = drx("N°").ToString
            ColorRowGrid(GVSignataire, "[N°]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(GVSignataire, "[N°]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
        End If
    End Sub

    Private Sub Checktous_CheckedChanged(sender As Object, e As EventArgs) Handles Checktous.CheckedChanged
        Try
            If ViewLstCmde.RowCount > 0 Then
                For k = 0 To ViewLstCmde.RowCount - 1
                    ViewLstCmde.SetRowCellValue(k, "Choix", Checktous.Checked)
                Next
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If RdParPassMarche.Checked Then
            ModifierToolStripMenuItem.Enabled = False
            SupprimerToolStripMenuItem.Enabled = False
        Else
            ModifierToolStripMenuItem.Enabled = True
            SupprimerToolStripMenuItem.Enabled = True
        End If
    End Sub
End Class