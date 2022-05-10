Imports MySql.Data.MySqlClient
Imports System.IO
Imports Microsoft.Office.Interop
Imports ClearProject.PassationMarche
Imports DevExpress.XtraEditors
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraEditors.Controls
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class NewDao
    Dim PourAjoutModifDansDB As Integer = 0 '0 action de base, ajout=1, deja dans la DB, modif=2

    Dim NumDoss As String = ""
    Dim TypeMarche As String = ""
    Dim MethodMarche As String = ""
    Dim RefMarche As String()
    Dim CurrentMarche As DataRow = Nothing
    Dim CurrentDao As DataRow = Nothing
    Dim CurrentDossMarche As DataRow = Nothing

    Dim TabAMettreAJour As Boolean() = {False, False, False, False, False, False, False}
    Dim LstTabName As New List(Of String) From {"PageDonneBase", "PageDonnePartic", "PageDQE", "PageConformTechnique", "PageSpecTech", "PagePostQualif", "PageApercu"}
    Dim ListeMethodePrevue As New List(Of String) From {"AOI", "AON", "PSC", "PSL", "PSO", "DC"}
    Dim CvConcil As String = String.Empty
    'Dim TypeCategorieSpecTech As String = String.Empty
    ' Dim SpecTech As New List(Of DaoSpecTechLot)
    '  Dim CodeCojoSup As New ArrayList
    Dim CodePostQualifSup As New ArrayList
    ' Dim CodeSpecTechSup As New ArrayList
    Dim NodeModPost As TreeListNode
    ' Dim NodeModSpec As TreeListNode
    Dim modifPostQualif As Boolean = False
    Dim modifSpecTech As Boolean = False
    Dim NewAddSpecTechClik As Boolean = False

    Dim LigneaModifier As Boolean = False
    Dim NomGridView As String = ""
    Dim IndexActive As Integer = 0
    Dim AfficherDossier As Boolean = False

    Private Sub NewDao_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RibbonDAO.Minimized = True

        TxtNumDao.Enabled = False
        TxtLibelleDao.Enabled = False
        cmbTypeMarche.Enabled = False
        cmbMarches.Enabled = False
        PageDQE.PageVisible = False
        PageApercu.PageVisible = False
        PageSpecTech.PageVisible = False

        LoadLangues(CmbLangue)
        ItemDevise()
        MajCmbCompte()
        ChargerEnteteTableaux()
        UniteNaturePrix()
        LoadTypeMarche()
        LoadArchivesDao()
    End Sub

    Private Sub BtNouveau_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtNouveau.ItemClick
        If PourAjoutModifDansDB = 0 Then 'Aucune action
            PourAjoutModifDansDB = 1
            NouveauDossier()
        ElseIf (PourAjoutModifDansDB = 1) Then ' Bouton nouvo clique
            SuccesMsg("Veuillez enregistrer le dossier en cours.")
        ElseIf PourAjoutModifDansDB = 2 Then ' Deja dans la DB ou en cours de modification
            SuccesMsg("Veuillez enregistrer et fermer le dossier en cours.")
        End If
    End Sub

    Private Sub BtFermerDAO_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtFermerDAO.ItemClick
        FermerDossier()
        PourAjoutModifDansDB = 0
    End Sub

#Region "Methodes"

    Private Sub LoadPage(ByVal PageName As String, ByVal NumDossier As String)
        If PageName = "PageDonneBase" Then
            LoadPageDonneBase(NumDossier)
        End If
        If PageName = "PageDonnePartic" Then
            LoadPageDonnePartic(NumDossier)
        End If
        If PageName = "PageConformTechnique" Then
            LoadPageConformTechnique(NumDossier)
        End If
        If PageName = "PageDQE" Then
            LoadPageDQE(NumDossier)
        End If
        If PageName = "PageSpecTech" Then
            LoadPageSpecTech(NumDossier)
        End If
        If PageName = "PagePostQualif" Then
            LoadPagePostQualif(NumDossier)
        End If
        If PageName = "PageApercu" Then

        End If
    End Sub


    Private Sub NewDao_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub cmbTypeMarche_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTypeMarche.SelectedIndexChanged
        Try
            LoadMarches(cmbTypeMarche.Text)
            If cmbTypeMarche.Text.Contains("Fourniture") Then
                PageDQE.PageVisible = False
                PageSpecTech.PageVisible = True
            ElseIf cmbTypeMarche.Text.Contains("Travaux") Then
                PageDQE.PageVisible = True
                PageSpecTech.PageVisible = False
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub MajCmbCompte()
        CmbCompte.ResetText()
        CmbCompte.Properties.Items.Clear()
        'Compte bancaire
        query = "select * from T_CompteBancaire where CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbCompte.Properties.Items.Add(MettreApost(rw("LibelleCompte").ToString) & " - " & rw("NumeroCompte"))
        Next

        'Charger les Caisse
        query = "select * from T_COMP_JOURNAL where CODE_SC LIKE '57%'" '='" & drx(1).ToString & "'"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbCompte.Properties.Items.Add(MettreApost(rw("LIBELLE_J").ToString) & " - " & rw("CODE_SC"))
        Next
    End Sub

    Private Sub LoadTypeMarche()
        query = "select TypeMarche from T_TypeMarche WHERE TypeMarche LIKE 'Fourniture%' OR TypeMarche LIKE 'Travaux%' order by TypeMarche"
        cmbTypeMarche.ResetText()
        cmbTypeMarche.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cmbTypeMarche.Properties.Items.Add(MettreApost(rw("TypeMarche").ToString))
        Next
    End Sub
    Private Sub UniteNaturePrix()

        query = "select * from t_unite"
        NaturePrix.ResetText()
        NaturePrix.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            NaturePrix.Properties.Items.Add(MettreApost(rw("LibelleCourtUnite").ToString & " - " & rw("LibelleUnite").ToString))
        Next
    End Sub

    Private Sub LoadMarches(ByVal TypeMarche As String)
        Try

            cmbMarches.ResetText()
            cmbMarches.Properties.Items.Clear()

            query = "Select * From T_Marche Where CodeProjet ='" & ProjetEnCours & "' AND TypeMarche='" & EnleverApost(TypeMarche.ToString) & "' AND NumeroMarche IS NULL and RefMarche NOT IN(SELECT RefMarche from t_dao where CodeProjet='" & ProjetEnCours & "' and TypeMarche='" & EnleverApost(TypeMarche.ToString) & "' and statut_DAO<>'Annulé')"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            ReDim RefMarche(dt.Rows.Count)
            Dim Taille As Integer = 0

            For Each rw As DataRow In dt.Rows
                RefMarche(Taille) = rw("RefMarche")
                Taille += 1
                cmbMarches.Properties.Items.Add(MettreApost(rw("DescriptionMarche")) & " | " & rw("MontantEstimatif") & " | " & MettreApost(rw("InitialeBailleur").ToString) & " (" & MettreApost(rw("CodeConvention").ToString) & ")")
            Next

            '** Afficharge des ligne du PPM en fonction des montants consomé

            'query = "Select * from T_Marche where CodeProjet='" & ProjetEnCours & "' AND TypeMarche='" & EnleverApost(TypeMarche) & "' AND NumeroMarche IS NULL"
            'Dim dt As DataTable = ExcecuteSelectQuery(query)
            'Dim MontantMarcheRestant As Decimal = 0

            'cmbMarches.ResetText()
            'cmbMarches.Properties.Items.Clear()
            'Dim Taille As Integer = 0

            'For Each rw As DataRow In dt.Rows
            '    'Montant marche restant (à utiliser)
            '    MontantMarcheRestant = CDec(rw("MontantEstimatif").ToString.Replace(" ", "")) - GetMontantMarcheConsomme(rw("RefMarche"), EnleverApost(TypeMarche)) 'Montant consomé
            '    If MontantMarcheRestant > 0 Then
            '        ReDim Preserve RefMarche(Taille)
            '        RefMarche(Taille) = rw("RefMarche")
            '        Taille += 1
            '        cmbMarches.Properties.Items.Add(MettreApost(rw("DescriptionMarche")) & " | " & MontantMarcheRestant & " | " & MettreApost(rw("InitialeBailleur").ToString) & " (" & MettreApost(rw("CodeConvention").ToString) & ")")
            '    End If
            'Next

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Function GetMontantMarcheConsomme(ByVal RefMarche As String, ByVal TypeMarche As String) As Decimal
        Dim MontantMarcheConsome As Decimal = 0
        Try
            'Marche utiliser pour elaborer un NumeroDAO
            ' query = "select SUM(S.PrixCorrigeOffre) from t_soumissionfournisseurclassement as S, t_DAO as D where S.NumeroDAO=D.NumeroDAO and S.AcceptationExamDetaille='OUI' and S.PrixCorrigeOffre IS NOT NULL and S.ExamPQValide='OUI' AND S.RangPostQualif IS NOT NULL and S.FournisDisqualifie IS NULL AND S.Selectionne='OUI' and S.Attribue='OUI' and  D.CodeProjet='" & ProjetEnCours & "' and D.statut_DAO<>'Annulé' and D.RefMarche='" & RefMarche & "' and D.TypeMarche='" & TypeMarche & "'"
            query = "select SUM(S.PrixCorrigeOffre) from t_soumissionfournisseurclassement as S, t_DAO as D where S.NumeroDAO=D.NumeroDAO and S.AcceptationExamDetaille='OUI' and S.PrixCorrigeOffre IS NOT NULL and S.RangExamDetaille IS NOT NULL and S.FournisDisqualifie IS NULL AND S.Selectionne='OUI' and S.Attribue='OUI' and  D.CodeProjet='" & ProjetEnCours & "' and D.statut_DAO<>'Annulé' and D.RefMarche='" & RefMarche & "' and D.TypeMarche='" & TypeMarche & "'"
            MontantMarcheConsome = Val(ExecuteScallar(query).ToString.Replace(".", ","))
            If MontantMarcheConsome = 0 Then
                query = "select SUM(MontantMarche) from t_dao where RefMarche='" & RefMarche & "' and TypeMarche='" & TypeMarche & "' and statut_DAO<>'Annulé'"
                MontantMarcheConsome = Val(ExecuteScallar(query).ToString.Replace(".", ","))
            End If

            Return MontantMarcheConsome
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try

    End Function

    Private Sub VisibleOtherTabs(ByVal Value As Boolean)
        'On désactive les autres tabs pour amener l'user à enregister les données de base.
        PageDonnePartic.PageEnabled = Value
        PageDQE.PageEnabled = Value
        'PageConformTechnique.PageEnabled = Value
        PageSpecTech.PageEnabled = Value
        PagePostQualif.PageEnabled = Value
        PageApercu.PageEnabled = Value
    End Sub

    Private Sub NouveauDossier()
        LigneaModifier = False
        NomGridView = ""
        IndexActive = 0F
        If Not TxtNbreLot.Enabled Then TxtNbreLot.Enabled = True
        TxtNbreLot.Value = 1
        CmbNumLotDB.Text = ""
        CmbNumLotDB.Properties.Items.Clear()
        CmbNumLotDB.Properties.Items.Add("1")
        If Not CmbNumLotDB.Enabled Then CmbNumLotDB.Enabled = True
        If Not BtFermerDAO.Enabled Then BtFermerDAO.Enabled = True
        If Not BtEnregistrer.Enabled Then BtEnregistrer.Enabled = True
        TxtNumDao.Enabled = True
        TxtNumDao.Focus()
        VisibleOtherTabs(False)
        PageDonneBase.PageEnabled = True
        InitDonneesBase()
        If Not IsNothing(CurrentDao) Then
            CurrentDao = Nothing
        End If
        AfficherDossier = False
        'Initialiser Donnée particulier
        InitDonneesPartic()
        InitDQE()
        'Initialiser Donnée specification technique
        InitSpecTechnq()
        'Initialiser Donnée post qualification
        InitPostQualif()
    End Sub

    Private Sub FermerDossier()
        If NumDoss <> "" Then
            DebutChargement(True, "Fermeture dossier " & NumDoss & " en cours...")
            NumDoss = ""
            InitDonneesBase()
            InitDonneesPartic()
            InitDQE()
            InitSpecTechnq()
            InitPostQualif()

            PageApercu.PageVisible = False
            PageSpecTech.PageVisible = False
            PageDQE.PageVisible = False
            FinChargement()
            VisibleOtherTabs(False)
            PageDonneBase.PageEnabled = False
            TabAMettreAJour = {False, False, False, False, False, False, False}
        Else

            InitDonneesBase()
            PageApercu.PageVisible = False
            PageDonneBase.PageEnabled = False
            PageSpecTech.PageVisible = False
            PageDQE.PageVisible = False
        End If

        TabAMettreAJour = {False, False, False, False, False, False, False}
        CurrentMarche = Nothing
        CurrentDao = Nothing
        CurrentDossMarche = Nothing
        LigneaModifier = False
        NomGridView = ""
        IndexActive = 0
        AfficherDossier = False
    End Sub
#End Region

#Region "Données de base"

    Private Sub InitDonneesBase()
        NumDoss = ""
        CurrentMarche = Nothing
        CurrentDao = Nothing

        TxtNumDao.ResetText()
        TxtLibelleDao.ResetText()
        DateDepot.ResetText()
        cmbTypeMarche.ResetText()
        cmbMarches.ResetText()
        TxtMethodeMarche.ResetText()
        MontantMarche.ResetText()
        LieurRemiseFourniture.ResetText()
        LigneBudgetaire.ResetText()
        NomJournal.ResetText()
        DatePublication.EditValue = Nothing
        HeurePub.EditValue = Nothing
        NbreDelaiPub.ResetText()
        JoursDelaiPub.ResetText()
        DateDepot.EditValue = Nothing
        HeureDepot.EditValue = Nothing
        DateReporte.EditValue = Nothing
        HeureReporte.EditValue = Nothing
        DateOuverture.EditValue = Nothing
        HeureOuverture.EditValue = Nothing
        Dim dtLots As DataTable = LgListLots.DataSource
        dtLots.Rows.Clear()
        TxtPrixDao.ResetText()
        CmbCompte.ResetText()
        NaturePrix.ResetText()
        TxtAdresseCompte.ResetText()
        InitEditionLot()

        GetEnebledAffichageBouton(True)
    End Sub

    Private Sub ViderChampsSaisieLot()
        TxtLibLot.ResetText()
        TxtCautionLot.ResetText()
        NumGarantiLot.Value = 0
        CmbGarantiLot.ResetText()
        GridSousLot.Rows.Clear()
        TxtSaisiSouLot.ResetText()
    End Sub

    Private Sub BtEnrgLot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgLot.Click

        If CmbNumLotDB.Text.Trim = "" Then
            SuccesMsg("Veuillez sélectionner un numéro dans la liste.")
            CmbNumLotDB.Select()
            Exit Sub
        End If
        If TxtLibLot.IsRequiredControl("Veuillez saisir le libellé du lot.") Then
            TxtLibLot.Select()
            Exit Sub
        End If

        If Val(TxtCautionLot.Text) > 0 Then
            If Val(NumGarantiLot.Value) <= 0 Then
                SuccesMsg("Veuillez saisir la caution.")
                NumGarantiLot.Select()
                Exit Sub
            End If
            If CmbGarantiLot.IsRequiredControl("Veuillez choisir une durée.") Then
                CmbGarantiLot.Select()
                Exit Sub
            End If
        End If

        Dim NbreSousLot As Integer = GridSousLot.Rows.Count
        Dim Updated As Boolean = False
        Dim Garantie As String = String.Empty
        If NumGarantiLot.Value <> 0 Then
            Garantie = NumGarantiLot.Value & " " & CmbGarantiLot.Text
        End If
        Dim NumLot As Decimal = Val(CmbNumLotDB.Text)
        Dim ListeSousLot As String = GetConcatListeLot()
        If ViewLots.RowCount > 0 Then
            For i = 0 To ViewLots.RowCount - 1
                If Val(ViewLots.GetRowCellValue(i, "N°")) = NumLot Then
                    ViewLots.SetRowCellValue(i, "Libellé", TxtLibLot.Text)
                    ViewLots.SetRowCellValue(i, "Caution", Val(TxtCautionLot.Text))
                    ViewLots.SetRowCellValue(i, "Garantie", Garantie.ToString)
                    ViewLots.SetRowCellValue(i, "Sous lots", IIf(NbreSousLot > 0, NbreSousLot & " Sous lot(s)", "").ToString)
                    ViewLots.SetRowCellValue(i, "ListeSousLot", ListeSousLot.ToString)
                    Updated = True
                    Exit For
                End If
            Next
        End If

        If Updated = False Then
            Dim dt As DataTable = LgListLots.DataSource
            Dim drS As DataRow = dt.NewRow
            drS("IdLot") = ""
            drS("N°") = NumLot
            drS("Libellé") = TxtLibLot.Text
            drS("Caution") = Val(TxtCautionLot.Text)
            drS("Garantie") = Garantie.ToString
            drS("Sous lots") = IIf(NbreSousLot > 0, NbreSousLot & " Sous lot(s)", "").ToString
            drS("ListeSousLot") = ListeSousLot.ToString
            dt.Rows.Add(drS)
        End If

        LigneaModifier = False
        NomGridView = ""
        IndexActive = 0
        ViderChampsSaisieLot()
    End Sub

    Private Sub CmbNumLotDB_TextChanged(sender As Object, e As EventArgs) Handles CmbNumLotDB.TextChanged
        If CmbNumLotDB.Text = "" Then
            ViderChampsSaisieLot()
        ElseIf CmbNumLotDB.Text <> "" Then
            ChargerSousLot(Val(CmbNumLotDB.Text))
        End If
    End Sub

    Private Sub TxtSaisiSouLot_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtSaisiSouLot.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then

                If CmbNumLotDB.Text.Trim = "" Then
                    SuccesMsg("Veuillez sélectionner un lot.")
                    CmbNumLotDB.Select()
                    Exit Sub
                End If
                If TxtSaisiSouLot.IsRequiredControl("Veuillez saisir le libellé du sous lot.") Then
                    TxtSaisiSouLot.Select()
                    Exit Sub
                End If

                If LigneaModifier = True And NomGridView = "GridSousLot" Then
                    GridSousLot.Rows(IndexActive).Cells("LibelleSousLots").Value = TxtSaisiSouLot.Text
                Else
                    Dim n = GridSousLot.Rows.Add()
                    GridSousLot.Rows(n).Cells("RefSousLots").Value = ""
                    GridSousLot.Rows(n).Cells("RefLot").Value = ""
                    GridSousLot.Rows(n).Cells("LibelleSousLots").Value = TxtSaisiSouLot.Text
                End If

                If ViewLots.RowCount > 0 Then
                    For j = 0 To ViewLots.RowCount - 1
                        If ViewLots.GetRowCellValue(j, "N°") = CmbNumLotDB.Text Then
                            ViewLots.SetRowCellValue(j, "ListeSousLot", GetConcatListeLot)
                            ViewLots.SetRowCellValue(j, "Sous lots", IIf(GridSousLot.Rows.Count > 0, GridSousLot.Rows.Count & " Sous lot(s)", "").ToString)
                            Exit For
                        End If
                    Next
                End If
                TxtSaisiSouLot.ResetText()
                LigneaModifier = False
                NomGridView = ""
                IndexActive = 0
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub ChargerSousLot(CodeLot As Integer)
        Try
            GridSousLot.Rows.Clear()
            ' Dim dtRwLot As DataTable = ExcecuteSelectQuery("select RefLot, LibelleLot, MontantGarantie, DelaiDeGarantie from t_lotdao  where NumeroDAO='" & EnleverApost(TxtNumDao.Text) & "' and CodeLot='" & CodeLot & "'")
            ' If dtRwLot.Rows.Count > 0 Then
            'For Each rw In dtRwLot.Rows
            '    TxtLibLot.Text = MettreApost(rw("LibelleLot").ToString)
            '    TxtCautionLot.Text = Val(rw("MontantGarantie").ToString)
            '    If rw("DelaiDeGarantie").ToString <> "" Then
            '        NumGarantiLot.Value = Val(rw("DelaiDeGarantie").ToString.Split(" ")(0))
            '        CmbGarantiLot.Text = rw("DelaiDeGarantie").ToString.Split(" ")(1)
            '    Else
            '        NumGarantiLot.Value = 0
            '        CmbGarantiLot.Text = ""
            '    End If
            'Next

            'Dim dtSousLot As DataTable = ExcecuteSelectQuery("select S.RefSousLot, S.LibelleSousLot, L.RefLot, L.LibelleLot, L.MontantGarantie, L.DelaiDeGarantie from t_lotdao_souslot AS S, t_lotdao AS L where S.RefLot=L.RefLot and S.NumeroDAO='" & EnleverApost(TxtNumDao.Text) & "' and L.CodeLot='" & CodeLot & "'")
            'For Each rw1 In dtSousLot.Rows
            '    Dim n = GridSousLot.Rows.Add()
            '    GridSousLot.Rows(n).Cells("RefLot").Value = rw1("RefLot")
            '    GridSousLot.Rows(n).Cells("RefSousLots").Value = rw1("RefSousLot")
            '    GridSousLot.Rows(n).Cells("LibelleSousLots").Value = MettreApost(rw1("LibelleSousLot").ToString)
            'Next
            If ViewLots.RowCount > 0 Then
                For i = 0 To ViewLots.RowCount - 1
                    If Val(ViewLots.GetRowCellValue(i, "N°")) = CodeLot Then
                        TxtLibLot.Text = ViewLots.GetRowCellValue(i, "Libellé").ToString
                        TxtCautionLot.Text = Val(ViewLots.GetRowCellValue(i, "Caution").ToString)
                        If ViewLots.GetRowCellValue(i, "Garantie").ToString <> "" Then
                            NumGarantiLot.Value = Val(ViewLots.GetRowCellValue(i, "Garantie").ToString.Split(" ")(0))
                            CmbGarantiLot.Text = ViewLots.GetRowCellValue(i, "Garantie").ToString.Split(" ")(1)
                        Else
                            NumGarantiLot.Value = 0
                            CmbGarantiLot.Text = ""
                        End If
                        If ViewLots.GetRowCellValue(i, "ListeSousLot").ToString <> "" Then
                            Dim ListeSousLot As String() = ViewLots.GetRowCellValue(i, "ListeSousLot").ToString.Split("#")
                            GridSousLot.Rows.Clear()

                            For j = 0 To ListeSousLot.Length - 1
                                Dim n = GridSousLot.Rows.Add
                                GridSousLot.Rows(n).Cells("RefLot").Value = ""
                                GridSousLot.Rows(n).Cells("RefSousLots").Value = ""
                                GridSousLot.Rows(n).Cells("LibelleSousLots").Value = ListeSousLot(j).ToString
                            Next
                        End If
                        Exit For
                    End If
                Next
            Else
                ViderChampsSaisieLot()
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub GridSousLot_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridSousLot.CellDoubleClick
        If GridSousLot.RowCount > 0 And AfficherDossier = False Then
            LigneaModifier = True
            NomGridView = "GridSousLot"
            IndexActive = GridSousLot.CurrentRow.Index
            TxtSaisiSouLot.Text = GridSousLot.Rows(IndexActive).Cells("LibelleSousLots").Value.ToString
        End If
    End Sub

    Private Sub LgListLots_DoubleClick(sender As Object, e As EventArgs) Handles LgListLots.DoubleClick
        If ViewLots.RowCount > 0 And AfficherDossier = False Then
            CmbNumLotDB.Text = ""
            CmbNumLotDB.Text = ViewLots.GetFocusedRowCellValue("N°").ToString
        End If
    End Sub

    Private Function GetConcatListeLot() As String
        Dim ListeLot As String = ""
        Try
            If GridSousLot.RowCount > 0 Then
                ListeLot = GridSousLot.Rows(0).Cells("LibelleSousLots").Value.ToString
                For j = 0 To GridSousLot.RowCount - 1
                    If j > 0 Then
                        ListeLot = ListeLot & "#" & GridSousLot.Rows(j).Cells("LibelleSousLots").Value.ToString
                    End If
                Next
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        Return ListeLot
    End Function

    Private Sub InitEditionLot()
        ViderChampsSaisieLot()
        ItemCmbLot()
    End Sub

    Private Sub DatePublication_EditValueChanged(sender As Object, e As EventArgs) Handles DatePublication.EditValueChanged, HeureDepot.EditValueChanged, NbreDelaiPub.EditValueChanged, JoursDelaiPub.EditValueChanged
        If DatePublication.Text.Trim <> "" And NbreDelaiPub.Text.Trim <> "" And JoursDelaiPub.Text.Trim <> "" Then
            GetDateAutomatique()
        End If
    End Sub

    Private Sub DateReporte_EditValueChanged(sender As Object, e As EventArgs) Handles DateReporte.EditValueChanged, HeureReporte.EditValueChanged
        If DateReporte.Text.Trim <> "" Then
            DateOuverture.Text = CDate(DateReporte.Text)
            If HeureReporte.Text.Trim <> "" Then
                Dim HeurOuvertur As DateTime = CDate(DateReporte.Text) & " " & HeureReporte.Text
                HeurOuvertur = HeurOuvertur.AddMinutes(30)
                DateOuverture.Text = CDate(HeurOuvertur).ToShortDateString
                HeureOuverture.EditValue = CDate(HeurOuvertur).ToLongTimeString
            End If
        End If
    End Sub

    Private Sub GetDateAutomatique()
        Dim datefindepot As Date = CDate(DatePublication.Text)

        If JoursDelaiPub.Text.ToLower = "jours" Then
            datefindepot = datefindepot.AddDays(CInt(NbreDelaiPub.Text))
        ElseIf JoursDelaiPub.Text.ToLower = "semaines" Then
            datefindepot = datefindepot.AddDays(CInt(NbreDelaiPub.Text) * 7)
        ElseIf JoursDelaiPub.Text.ToLower = "mois" Then
            datefindepot = datefindepot.AddMonths(CInt(NbreDelaiPub.Text))
        End If

        DateDepot.Text = datefindepot
        If DateReporte.Text = "" And HeureReporte.Text = "" Then DateOuverture.Text = datefindepot
        If HeureDepot.Text.Trim <> "" And DateReporte.Text = "" And HeureReporte.Text = "" Then
            Dim HeurOuvertur As DateTime = datefindepot & " " & HeureDepot.Text
            HeurOuvertur = HeurOuvertur.AddMinutes(30)
            DateOuverture.Text = CDate(HeurOuvertur).ToShortDateString
            HeureOuverture.EditValue = CDate(HeurOuvertur).ToLongTimeString
        End If
    End Sub

    Private Sub ItemCmbLot()
        query = "select CodeLot from T_LotDAO where NumeroDAO='" & NumDoss & "' order by CodeLot"
        CmbLotDQE.ResetText()
        CmbNumLot2.ResetText()
        CmbNumLot.ResetText()
        CmbLotDQE.Properties.Items.Clear()
        CmbNumLot2.Properties.Items.Clear()
        CmbNumLot.Properties.Items.Clear()
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbLotDQE.Properties.Items.Add(rw("CodeLot").ToString)
            CmbNumLot2.Properties.Items.Add(rw("CodeLot").ToString)
            CmbNumLot.Properties.Items.Add(rw("CodeLot").ToString)
        Next
    End Sub

    Private Sub ChargerEnteteTableaux()

        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("N°", Type.GetType("System.String"))
        dt.Columns.Add("Edité le", Type.GetType("System.String"))
        dt.Columns.Add("Type", Type.GetType("System.String"))
        dt.Columns.Add("Méthode", Type.GetType("System.String"))
        dt.Columns.Add("Nbre lot", Type.GetType("System.String"))
        dt.Columns.Add("Ouverture", Type.GetType("System.String"))
        dt.Columns.Add("Date", Type.GetType("System.String"))
        dt.Columns.Add("Libellé", Type.GetType("System.String"))
        dt.Columns.Add("Statut", Type.GetType("System.String"))
        dt.Columns.Add("DossValider", Type.GetType("System.Boolean"))
        dt.Columns.Add("DateLimitePropo", Type.GetType("System.String"))
        GridArchives.DataSource = dt
        LayoutView1.Columns("DateLimitePropo").Visible = False
        LayoutView1.Columns("DossValider").Visible = False

        dt = New DataTable()
        dt.Columns.Add("IdLot", Type.GetType("System.String"))
        dt.Columns.Add("N°", Type.GetType("System.String"))
        dt.Columns.Add("Libellé", Type.GetType("System.String"))
        dt.Columns.Add("Caution", Type.GetType("System.String"))
        dt.Columns.Add("Garantie", Type.GetType("System.String"))
        dt.Columns.Add("Sous lots", Type.GetType("System.String"))
        dt.Columns.Add("ListeSousLot", Type.GetType("System.String"))
        LgListLots.DataSource = dt
        ViewLots.Columns("IdLot").Visible = False
        ViewLots.Columns("ListeSousLot").Visible = False
        ViewLots.Columns("N°").Width = 30
        ViewLots.Columns("Libellé").Width = 250
        ViewLots.OptionsView.ColumnAutoWidth = True
        ViewLots.Columns("N°").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewLots.Columns("Caution").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewLots.Columns("Sous lots").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewLots.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

        ' **************** Données particulière ******************* '

        'Cojo
        dt = New DataTable()
        dt.Columns.Add("IdCommission", Type.GetType("System.String"))
        dt.Columns.Add("Nom et prénoms", Type.GetType("System.String"))
        dt.Columns.Add("Organisme", Type.GetType("System.String"))
        dt.Columns.Add("Titre", Type.GetType("System.String"))
        dt.Columns.Add("Téléphone", Type.GetType("System.String"))
        dt.Columns.Add("Email", Type.GetType("System.String"))
        dt.Columns.Add("LigneModif", Type.GetType("System.String"))
        LgCojo.DataSource = dt
        dt.DefaultView.Sort = "Nom et prénoms ASC"
        ViewCojo.Columns("IdCommission").Visible = False
        ViewCojo.Columns("LigneModif").Visible = False
        ViewCojo.Columns("Nom et prénoms").Width = 280
        ViewCojo.OptionsView.ColumnAutoWidth = True
        ViewCojo.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)


        ' **************** Conformité technique ******************* '
        'Critère

        dt = New DataTable()
        dt.Columns.Add("Code", Type.GetType("System.String"))
        dt.Columns.Add("N°", Type.GetType("System.String"))
        dt.Columns.Add("Description", Type.GetType("System.String"))
        dt.Columns.Add("Eliminatoire", Type.GetType("System.String"))
        dt.DefaultView.Sort = "N° ASC"
        GridCritere.DataSource = dt
        ViewCritere.Columns("Code").Visible = False
        ViewCritere.Columns("Description").Width = 280
        ViewCritere.OptionsView.ColumnAutoWidth = True
        ViewCritere.Columns("N°").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewCritere.Columns("Eliminatoire").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewCritere.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

        ' **************** Spécification Technique **************** '
        dt = New DataTable
        dt.Columns.Add("Id", Type.GetType("System.String"))
        dt.Columns.Add("Code", Type.GetType("System.String"))
        dt.Columns.Add("Libellé", Type.GetType("System.String"))
        dt.Columns.Add("Quantité", Type.GetType("System.String"))
        dt.Columns.Add("Lieu de livraison", Type.GetType("System.String"))
        dt.Columns.Add("CodeCateg", Type.GetType("System.String"))
        dt.Columns.Add("NumLot", Type.GetType("System.String"))
        dt.Columns.Add("NumSousLot", Type.GetType("System.String"))
        dt.Columns.Add("Edit", Type.GetType("System.Boolean"))

        GridSpecifTech.DataSource = dt
        ViewSpecTechn.Columns("Id").Visible = False
        ViewSpecTechn.Columns("CodeCateg").Visible = False
        ViewSpecTechn.Columns("NumLot").Visible = False
        ViewSpecTechn.Columns("NumSousLot").Visible = False
        ViewSpecTechn.Columns("Edit").Visible = False
        ViewSpecTechn.Columns("Code").Width = 40
        ViewSpecTechn.Columns("Libellé").Width = 410
        ViewSpecTechn.Columns("Quantité").Width = 100
        ViewSpecTechn.OptionsView.ColumnAutoWidth = True
        ViewSpecTechn.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewSpecTechn, "[Quantité]<>''", Color.LightBlue, "Tahoma", 8, FontStyle.Bold, Color.Black)

        ' **************** Examen Post Qualification **************** '
        dt = New DataTable()
        dt.Columns.Add("Id", Type.GetType("System.String"))
        dt.Columns.Add("Code", Type.GetType("System.String"))
        'dt.Columns.Add("N°", Type.GetType("System.String"))
        dt.Columns.Add("Description", Type.GetType("System.String"))
        dt.Columns.Add("Eliminatoire", Type.GetType("System.String"))
        dt.Columns.Add("Groupe", Type.GetType("System.String"))

        GridPostQualif.DataSource = dt
        ViewPostQualif.Columns("Id").Visible = True
        ViewPostQualif.Columns("Code").Visible = True
        ViewPostQualif.Columns("Groupe").Visible = True
        'ViewPostQualif.Columns("N°").Width = 50
        ViewPostQualif.Columns("Description").Width = GridPostQualif.Width - 168
        ViewPostQualif.Columns("Eliminatoire").Width = 100

        'ViewPostQualif.Columns("N°").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center                  'Drawing.StringAlignment.Center
        ViewPostQualif.Columns("Eliminatoire").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        ViewPostQualif.OptionsView.ColumnAutoWidth = True
        'ColorRowGrid(ViewPostQualif, "[Code]='x'", Color.LightGray, "Tahoma", 8, FontStyle.Regular, Color.Black)
        ColorRowGridAnal(ViewPostQualif, "[Code]='G'", Color.Navy, "Tahoma", 9, FontStyle.Bold, Color.White, True)
    End Sub

    Private Sub LoadArchivesDao()
        Dim dt As DataTable = GridArchives.DataSource
        dt.Rows.Clear()
        query = "select * from T_DAO where CodeProjet='" & ProjetEnCours & "' order by DateSaisie DESC"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            Dim dr = dt.NewRow()
            dr("N°") = rw("NumeroDAO").ToString
            dr("Edité le") = CDate(rw("DateEdition").ToString()).ToString("dd/MM/yyyy hh:mm:ss")
            dr("Type") = rw("TypeMarche").ToString
            dr("Méthode") = rw("MethodePDM").ToString
            dr("Nbre lot") = rw("NbreLotDAO").ToString & " lot(s)"
            dr("Libellé") = MettreApost(rw("IntituleDAO").ToString)

            dr("Statut") = MettreApost(rw("statut_DAO").ToString)
            dr("DossValider") = CBool(rw("DossValider"))
            dr("DateLimitePropo") = IIf(rw("DateReport").ToString <> "", rw("DateReport").ToString, rw("DateLimiteRemise").ToString).ToString

            If (rw("DateFinOuverture").ToString <> "") Then
                dr("Ouverture") = "Effectuée"
                ' dr("Date") = CDate(rw("DateFinOuverture").ToString()).ToShortDateString() & " à " & CDate(rw("DateFinOuverture").ToString()).ToShortTimeString()
                dr("Date") = CDate(rw("DateDebutOuverture").ToString()).ToShortDateString() & " à " & CDate(rw("DateDebutOuverture").ToString()).ToLongTimeString()
            Else
                If (rw("DateOuverture").ToString <> "") Then
                    dr("Ouverture") = "Non effectuée"
                    dr("Date") = CDate(rw("DateOuverture").ToString()).ToShortDateString() & " à " & CDate(rw("DateOuverture").ToString()).ToLongTimeString()
                Else
                    dr("Ouverture") = "Non Prévue"
                    dr("Date") = "__/__/____"
                End If
            End If
            dt.Rows.Add(dr)
        Next
    End Sub

    Private Sub BtArchives_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtArchives.ItemClick
        SplitContainerControl1.Collapsed = False
    End Sub

    Private Sub TxtNbreLot_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNbreLot.EditValueChanged
        If Val(TxtNbreLot.Value) >= 1 Then
            Dim oldText As String = CmbNumLotDB.Text
            CmbNumLotDB.Properties.Items.Clear()
            For i = 1 To TxtNbreLot.Value
                CmbNumLotDB.Properties.Items.Add(i.ToString())
            Next
            CmbNumLotDB.Text = oldText
        End If
    End Sub

    Private Sub TxtNumDao_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNumDao.TextChanged
        If PourAjoutModifDansDB = 1 Then
            If (TxtNumDao.Text <> "") Then
                TxtLibelleDao.Enabled = True

                If (TxtLibelleDao.Text <> "") Then
                    cmbTypeMarche.Enabled = True
                    cmbMarches.Enabled = True
                Else
                    cmbTypeMarche.Enabled = False
                    cmbMarches.Enabled = False
                End If
            Else
                TxtLibelleDao.Enabled = False
                If (TxtLibelleDao.Text <> "") Then
                    cmbTypeMarche.Enabled = True
                    cmbMarches.Enabled = True
                Else
                    cmbTypeMarche.Enabled = False
                    cmbMarches.Enabled = False
                End If
            End If
        End If

    End Sub

    Private Sub TxtLibelleDao_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtLibelleDao.TextChanged
        If PourAjoutModifDansDB = 1 Then
            If (TxtLibelleDao.Text.Trim() <> "") Then
                cmbTypeMarche.Enabled = True
                cmbMarches.Enabled = True
            Else
                cmbTypeMarche.Enabled = False
                cmbMarches.Enabled = False
            End If
        End If
    End Sub


    Private Sub LoadPageDonneBase(ByVal NumDossier As String)
        'InitDonneesBase()
        If Not PageDonneBase.PageEnabled Then PageDonneBase.PageEnabled = True
        If IsNothing(CurrentDao) Then
            Dim dtDao As DataTable = ExcecuteSelectQuery("SELECT * FROM t_dao WHERE NumeroDAO='" & NumDossier & "'")
            If dtDao.Rows.Count = 0 Then
                CurrentDao = Nothing
                Exit Sub
            End If
            CurrentDao = dtDao.Rows(0)
        End If

        If IsNothing(CurrentMarche) Then
            Dim dtmarche As DataTable = ExcecuteSelectQuery("SELECT * FROM t_marche WHERE RefMarche='" & CurrentDao("RefMarche") & "'")
            If dtmarche.Rows.Count > 0 Then
                CurrentMarche = dtmarche.Rows(0)
            Else
                CurrentMarche = Nothing
                FailMsg("Le marché associé a été supprimé.")
                Exit Sub
            End If
        End If

        Try
            'Verrouillage des champs
            TxtNumDao.Enabled = False
            cmbTypeMarche.Enabled = False
            cmbMarches.Enabled = False
            TxtNbreLot.Enabled = False

            TxtNumDao.Text = MettreApost(CurrentDao("NumeroDAO").ToString)
            TxtLibelleDao.Text = MettreApost(CurrentDao("IntituleDAO").ToString)
            cmbTypeMarche.Text = MettreApost(CurrentDao("TypeMarche").ToString)
            cmbMarches.Text = MettreApost(CurrentMarche("DescriptionMarche")) & " | " & AfficherMonnaie(CurrentDao("MontantMarche").ToString) & " | " & MettreApost(CurrentMarche("InitialeBailleur").ToString) & " (" & MettreApost(CurrentMarche("CodeConvention").ToString) & ")"

            If CurrentDao("Attribution").ToString() = "Lot" Then
                rdAttrLot.Checked = True
            End If

            TxtMethodeMarche.Text = CurrentDao("MethodePDM").ToString
            MontantMarche.Text = CurrentDao("MontantMarche").ToString.Replace(" ", "")
            TxtNbreLot.Value = MettreApost(CurrentDao("NbreLotDAO"))
            LigneBudgetaire.Text = MettreApost(CurrentDao("LigneBudgetaire").ToString)
            LieurRemiseFourniture.Text = MettreApost(CurrentDao("LieurRemiseFourniture").ToString)

            If IsDBNull(CurrentDao("DateLimiteRemise")) Then
                DateDepot.EditValue = Nothing
                HeureDepot.EditValue = Nothing
            Else
                DateDepot.EditValue = CDate(CurrentDao("DateLimiteRemise")).ToShortDateString
                HeureDepot.EditValue = CDate(CurrentDao("DateLimiteRemise")).ToLongTimeString
            End If

            If CurrentDao("DateOuverture").ToString() <> "" Then
                DateOuverture.EditValue = CDate(CurrentDao("DateOuverture")).ToShortDateString
                HeureOuverture.EditValue = CDate(CurrentDao("DateOuverture")).ToLongTimeString
            Else
                DateOuverture.EditValue = Nothing
                HeureOuverture.EditValue = Nothing
            End If

            If IsDBNull(CurrentDao("DatePublication")) Then
                DatePublication.EditValue = Nothing
                HeurePub.EditValue = Nothing
            Else
                DatePublication.EditValue = CDate(CurrentDao("DatePublication")).ToShortDateString
                HeurePub.EditValue = CDate(CurrentDao("DatePublication").ToString).ToLongTimeString
            End If

            If CurrentDao("JournalPublication").ToString().Trim <> "" Then
                NomJournal.Text = MettreApost(CurrentDao("JournalPublication").ToString)
            Else
                NomJournal.ResetText()
            End If
            If CurrentDao("DateReport").ToString.Trim <> "" Then
                DateReporte.EditValue = CDate(CurrentDao("DateReport").ToString).ToShortDateString
                HeureReporte.EditValue = CDate(CurrentDao("DateReport").ToString).ToLongTimeString
            Else
                DateReporte.EditValue = Nothing
                HeureReporte.EditValue = Nothing
            End If

            If CurrentDao("DelaiPublication").ToString.Trim <> "" Then
                NbreDelaiPub.Text = Val(CurrentDao("DelaiPublication").ToString.Split(" ")(0))
                JoursDelaiPub.Text = CurrentDao("DelaiPublication").ToString.Split(" ")(1)
            Else
                NbreDelaiPub.Text = ""
                JoursDelaiPub.Text = ""
            End If

            'Remplir le tableau des lots
            ChargerLesLots(NumDossier)

            'Remplir la liste des lots
            CmbNumLotDB.ResetText()
            CmbNumLotDB.Properties.Items.Clear()
            For i = 1 To TxtNbreLot.Value
                CmbNumLotDB.Properties.Items.Add(i)
            Next

            If CurrentDao("PrixDAO").ToString <> "" Then
                TxtPrixDao.Text = CurrentDao("PrixDAO").ToString
                ' TxtPrixDao.Text = AfficherMonnaie(CurrentDao("PrixDAO").ToString) ' IIf(PrixDAO = 0, "", AfficherMonnaie(PrixDAO))
            Else
                TxtPrixDao.ResetText()
            End If

            NaturePrix.Text = ExecuteScallar("select CONCAT(LibelleCourtUnite,' - ', LibelleUnite) from t_unite where LibelleCourtUnite='" & CurrentDao("NaturePrix") & "'")

            If CurrentDao("CompteAchat").ToString() = "" Then
                CmbCompte.Text = String.Empty
            Else
                Dim Comptes As String = ""
                query = "select CONCAT(LibelleCompte,' - ', NumeroCompte) from T_CompteBancaire where CodeProjet='" & ProjetEnCours & "' AND NumeroCompte='" & CurrentDao("CompteAchat") & "'"
                Comptes = ExecuteScallar(query)
                If Comptes.ToString = "" Then
                    Comptes = ExecuteScallar("select CONCAT(LIBELLE_J,' - ', CODE_SC) from T_COMP_JOURNAL where CODE_SC='" & CurrentDao("CompteAchat") & "'")
                End If
                CmbCompte.Text = MettreApost(Comptes.ToString)
            End If
            TabAMettreAJour(0) = True
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub ChargerLesLots(NumeroDAO As String)
        Try
            'Remplir le tableau des lots
            Dim dtLots As DataTable = LgListLots.DataSource
            dtLots.Rows.Clear()
            query = "SELECT * FROM t_lotdao WHERE NumeroDAO='" & NumeroDAO & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            Dim NbSousLots As Integer = 0
            Dim SousLots As String = String.Empty

            For Each rw As DataRow In dt.Rows
                Dim drS = dtLots.NewRow
                NbSousLots = 0
                SousLots = ""
                NbSousLots = Val(ExecuteScallar("SELECT COUNT(LibelleSousLot) FROM t_lotdao_souslot WHERE RefLot='" & rw("RefLot") & "'"))
                SousLots = MettreApost(ExecuteScallar("SELECT GROUP_CONCAT(LibelleSousLot SEPARATOR '#') FROM t_lotdao_souslot WHERE RefLot='" & rw("RefLot") & "'"))

                drS("IdLot") = rw("RefLot")
                drS("N°") = rw("CodeLot")
                drS("Libellé") = MettreApost(rw("LibelleLot"))
                drS("Caution") = rw("MontantGarantie")
                drS("Garantie") = rw("DelaiDeGarantie")
                If NbSousLots = 0 Then
                    drS("Sous lots") = ""
                Else
                    drS("Sous lots") = NbSousLots & " Sous lot(s)"
                End If

                drS("ListeSousLot") = SousLots
                dtLots.Rows.Add(drS)
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Function SavePageDonneBase(ByVal NumDossier As String) As Boolean

        Dim CompteVenteDAO As String = String.Empty
        If CmbCompte.SelectedIndex <> -1 Then
            CompteVenteDAO = Split(CmbCompte.Text, " - ")(1)
        End If

        Dim AttributionMarche As String = "Lot"
        If Not rdAttrLot.Checked Then
            AttributionMarche = "Sous Lot"
        End If
        Dim DateReports As String = ""
        If DateReporte.Text <> "" And HeureReporte.Text <> "" Then
            DateReports = dateconvert(DateReporte.Text) & " " & HeureReporte.Text
        End If

        Dim DelaiPublication As String = NbreDelaiPub.Text & " " & JoursDelaiPub.Text

        query = "UPDATE `t_dao` SET `IntituleDAO`='" & EnleverApost(TxtLibelleDao.Text) & "', `MontantMarche`='" & MontantMarche.Text.Replace(" ", "").Replace(",", ".") & "', `TypeMarche`='" & CurrentMarche("TypeMarche") & "', "
        query &= "`MethodePDM`='" & GetMethode(CurrentMarche("CodeProcAO")) & "', `NbreLotDAO`='" & TxtNbreLot.Value & "', `DateModif`='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', `PrixDAO`='" & TxtPrixDao.Text & "', "
        query &= "`CompteAchat`='" & CompteVenteDAO & "', `DateOuverture`='" & dateconvert(DateOuverture.Text) & " " & HeureOuverture.Text & "', DelaiPublication='" & DelaiPublication & "', DateReport='" & DateReports & "', NaturePrix='" & EnleverApost(NaturePrix.Text.Split("-")(0).Trim) & "',"
        query &= "`DateLimiteRemise`='" & dateconvert(DateDepot.Text) & " " & HeureDepot.Text & "', `CodeConvention`='" & CurrentMarche("Convention_ChefFile") & "', LieurRemiseFourniture='" & EnleverApost(LieurRemiseFourniture.Text) & "', LigneBudgetaire='" & EnleverApost(LigneBudgetaire.Text) & "', "
        query &= "`Attribution`='" & AttributionMarche & "', DatePublication='" & dateconvert(DatePublication.Text) & " " & HeurePub.Text & "', JournalPublication='" & EnleverApost(NomJournal.Text) & "' WHERE `NumeroDAO`='" & NumDossier & "'"

        Try
            ExecuteNonQuery(query)

            'Modification des lots existants
            GetSaveLot(NumDossier)

            LoadArchivesDao()
            'Chargement des lots
            ChargerLesLots(NumDossier)
            Return True
        Catch ex As Exception
            FailMsg("Impossible d'enregistrer ce DAO" & vbNewLine & "Contactez votre fournisseur" & vbNewLine & ex.ToString)
            Return False
        End Try
    End Function

    Private Sub GetSaveLot(ByVal NumeroDAO As String)
        Try
            Dim NumeroLot As String = ""
            Dim ListeSousLots As String = ""
            Dim RefLot As Decimal = 0
            Dim LastNumLot As Decimal = 0


            For i = 0 To (ViewLots.RowCount - 1)
                NumeroLot = ViewLots.GetRowCellValue(i, "N°").ToString
                ListeSousLots = ViewLots.GetRowCellValue(i, "ListeSousLot").ToString()

                If ViewLots.GetRowCellValue(i, "IdLot").ToString = "" Then
                    query = "INSERT INTO t_lotdao(RefLot,NumeroDAO,CodeLot,LibelleLot,MontantGarantie,DelaiDeGarantie,DateSaisie,DateModif,Operateur) "
                    query &= "VALUES(NULL,'" & NumeroDAO & "','" & ViewLots.GetRowCellValue(i, "N°") & "','" & EnleverApost(ViewLots.GetRowCellValue(i, "Libellé")) & "','" & Val(ViewLots.GetRowCellValue(i, "Caution")) & "','" & ViewLots.GetRowCellValue(i, "Garantie") & "','" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "','" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "','" & CodeUtilisateur & "')"
                    ExecuteNonQuery(query)

                    If ListeSousLots.ToString <> "" Then
                        LastNumLot = Val(ExecuteScallar("SELECT MAX(RefLot) FROM t_lotdao"))
                        GetSaveSousLot(ListeSousLots, NumeroDAO, LastNumLot, NumeroLot) 'Enregistrement lot
                    End If
                Else
                    RefLot = Val(ViewLots.GetRowCellValue(i, "IdLot"))
                    ExecuteNonQuery("UPDATE t_lotdao SET LibelleLot='" & EnleverApost(ViewLots.GetRowCellValue(i, "Libellé")) & "', MontantGarantie='" & Val(ViewLots.GetRowCellValue(i, "Caution")) & "', DelaiDeGarantie='" & ViewLots.GetRowCellValue(i, "Garantie") & "', DateModif='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' WHERE RefLot='" & RefLot & "'")
                    'Suppression des lots
                    If ListeSousLots.ToString <> "" Then
                        ExecuteNonQuery("delete from t_lotdao_souslot where NumeroDAO='" & NumeroDAO & "' and RefLot='" & RefLot & "'")
                        GetSaveSousLot(ListeSousLots.ToString, NumeroDAO, RefLot, NumeroLot) 'Enregistrement a nouveau des lots
                    End If
                End If
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub GetSaveSousLot(ListeSousLot As String, NumeroDAO As String, RefLot As Decimal, NumeroLot As Integer)
        Try
            If ListeSousLot.ToString <> "" Then
                Dim SousLots As String() = ListeSousLot.ToString().Split("#")
                Dim cpteSousLot As Integer = 1
                Dim NumSousLot As String = ""
                For i = 0 To SousLots.Length - 1
                    If SousLots(i).ToString <> "" Then
                        NumSousLot = NumeroLot & "." & cpteSousLot
                        query = "INSERT INTO t_lotdao_souslot(RefSousLot,RefLot,NumeroDAO,CodeSousLot,LibelleSousLot) "
                        query &= "VALUES(NULL,'" & RefLot & "','" & EnleverApost(NumeroDAO) & "','" & NumSousLot & "','" & EnleverApost(SousLots(i).ToString) & "')"
                        ExecuteNonQuery(query)
                        cpteSousLot += 1
                    End If
                Next
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub cmbMarches_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMarches.SelectedIndexChanged
        If PourAjoutModifDansDB = 1 Then
            If cmbMarches.SelectedIndex = -1 Then
                GroupMarche.Enabled = False
                GroupControlPub.Enabled = False
                TxtPrixDao.Enabled = False
                CmbCompte.Enabled = False

                TxtMethodeMarche.ResetText()
                CurrentMarche = Nothing
            Else
                query = "SELECT * FROM t_marche WHERE RefMarche='" & RefMarche(cmbMarches.SelectedIndex) & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                If dt.Rows.Count > 0 Then
                    CurrentMarche = dt.Rows(0)
                    TxtMethodeMarche.Text = GetMethode(CurrentMarche("CodeProcAO"))
                    ' MontantMarche.Text = CurrentMarche("MontantEstimatif").ToString
                    MontantMarche.Text = cmbMarches.Text.Split("|")(1).Replace(" ", "")
                Else
                    CurrentMarche = Nothing
                    TxtMethodeMarche.ResetText()
                    MontantMarche.ResetText()
                End If
                rdAttrLot.Enabled = True
                GroupMarche.Enabled = True
                GroupControlPub.Enabled = True
                TxtPrixDao.Enabled = True
                CmbCompte.Enabled = True
            End If
        End If
    End Sub

    Private Sub ContextMenuStripSousLotDB_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStripSousLotDB.Opening
        Try
            If AfficherDossier = True Or GridSousLot.RowCount = 0 Then
                e.Cancel = True
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub DeleteSousLot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteSousLot.Click
        Try
            If GridSousLot.Rows.Count > 0 Then
                GridSousLot.Rows.Remove(GridSousLot.CurrentRow)
                If ViewLots.RowCount > 0 Then
                    Dim ListeSousLot As String = GetConcatListeLot()
                    For i = 0 To ViewLots.RowCount - 1
                        If Val(ViewLots.GetRowCellValue(i, "N°")) = Val(CmbNumLotDB.Text) Then
                            ViewLots.SetRowCellValue(i, "ListeSousLot", ListeSousLot)
                            ViewLots.SetRowCellValue(i, "Sous lots", IIf(GridSousLot.Rows.Count > 0, GridSousLot.Rows.Count & " Sous lot(s)", "").ToString)
                            Exit For
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub TxtNbreLot_EditValueChanging(ByVal sender As Object, ByVal e As ChangingEventArgs) Handles TxtNbreLot.EditValueChanging
        If Val(e.NewValue) <> 0 Then
            If ViewLots.RowCount > 1 And ViewLots.RowCount > e.NewValue Then
                If ConfirmMsg("Plus de " & e.NewValue & " lot(s) ont déjà été enregistré." & vbNewLine & "Si vous confirmer la modification du nombre de lots. Certains lots serons supprimés." & vbNewLine & "Voulez-vous continuer ?") = DialogResult.Yes Then
                    Dim OldNum As Integer = Val(e.NewValue) - 1
                    For i = (ViewLots.RowCount - 1) To 0 Step -1
                        If i > OldNum Then
                            ViewLots.GetDataRow(i).Delete()
                        End If
                    Next
                Else
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    Private Sub XtraTabControl1_SelectedPageChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles XtraTabControl1.SelectedPageChanged
        If XtraTabControl1.SelectedTabPageIndex <> -1 Then
            If PourAjoutModifDansDB = 2 Then
                If Not TabAMettreAJour(LstTabName.IndexOf(e.Page.Name)) Then
                    TabAMettreAJour(LstTabName.IndexOf(e.Page.Name)) = True
                    LoadPage(e.Page.Name, NumDoss)
                End If
            End If
        End If
    End Sub


    Private Sub BtEnregistrer_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtEnregistrer.ItemClick
        'Vérification des champs
        If TxtNumDao.IsRequiredControl("Veuillez saisir le numéro du DAO.") Then
            TxtNumDao.Select()
            Exit Sub
        End If
        If TxtLibelleDao.IsRequiredControl("Veuillez saisir le libellé du DAO.") Then
            TxtLibelleDao.Select()
            Exit Sub
        End If
        If cmbTypeMarche.IsRequiredControl("Veuillez sélectionner le type de marché.") Then
            cmbTypeMarche.Select()
            Exit Sub
        End If

        If PourAjoutModifDansDB = 1 Then
            If cmbMarches.IsRequiredControl("Veuillez sélectionner un marché.") Then
                cmbMarches.Select()
                Exit Sub
            End If
        ElseIf cmbMarches.Text.Trim = "" Then
            SuccesMsg("Veuillez sélectionner un marché.")
            cmbMarches.Select()
            Exit Sub
        End If

        If Val(TxtNbreLot.Text) <= 0 Then
            SuccesMsg("Veuillez ajouter des lots.")
            TxtNbreLot.Select()
            Exit Sub
        End If

        If NomJournal.IsRequiredControl("Veuillez saisir le nom du journal.") Then
            NomJournal.Select()
            Exit Sub
        End If

        If DatePublication.IsRequiredControl("Veuillez indiquer la date de publication.") Then
            DatePublication.Select()
            Exit Sub
        End If

        If HeurePub.IsRequiredControl("Veuillez indiquer l'heure de publication.") Then
            DatePublication.Select()
            Exit Sub
        End If

        If NbreDelaiPub.IsRequiredControl("Veuillez indiquer le délai de publication.") Then
            NbreDelaiPub.Select()
            Exit Sub
        End If
        If JoursDelaiPub.IsRequiredControl("Veuillez indiquer le délai de publication.") Then
            JoursDelaiPub.Select()
            Exit Sub
        End If

        If LieurRemiseFourniture.IsRequiredControl("Veuillez indiquer le lieu de remise des offres.") Then ' & cmbTypeMarche.Text.ToLower)
            LieurRemiseFourniture.Select()
            Exit Sub
        End If

        ' LigneBudgetaire
        'LieurRemiseFourniture

        If (DateReporte.Text <> "" And HeureReporte.Text = "") Or (DateReporte.Text = "" And HeureReporte.Text <> "") Then
            SuccesMsg("Veuillez bien indiquer la date de report.")
            If DateReporte.Text = "" Then DateReporte.Select()
            If HeureReporte.Text = "" Then DateReporte.Select()
            Exit Sub
        End If

        If DateOuverture.IsRequiredControl("Veuillez indiquer la date d'ouverture.") Then
            DateOuverture.Select()
            Exit Sub
        End If
        If HeureOuverture.IsRequiredControl("Veuillez indiquer l'heure d'ouverture.") Then
            HeureOuverture.Select()
            Exit Sub
        End If

        If TxtNbreLot.Value > ViewLots.RowCount Then
            FailMsg("Veuillez enregistrer tous les lots.")
            Exit Sub
        End If

        If TxtPrixDao.Text.Trim <> "" Then
            If TxtPrixDao.IsRequiredControl("Veuillez saisir le prix de vente du dossier.") Then
                TxtPrixDao.Select()
                Exit Sub
            End If

            If Not IsNumeric(TxtPrixDao.Text) Then
                SuccesMsg("Saisie incorrect.")
                TxtPrixDao.Select()
                Exit Sub
            End If

            If Val(TxtPrixDao.Text) <> 0 Then
                If CmbCompte.IsRequiredControl("Veuillez sélectionner le compte bancaire ou la caisse à disposition pour les frais de dossier.") Then
                    CmbCompte.Select()
                    Exit Sub
                End If
            End If
        End If

        If NaturePrix.IsRequiredControl("Veuillez sélectionner la nature des prix.") Then
            NaturePrix.Select()
            Exit Sub
        End If


        If IsNothing(CurrentMarche) Then
            FailMsg("Nous n'avons pas pu récupérer le marché.")
            cmbMarches.Select()
            Exit Sub
        End If

        NumDoss = EnleverApost(TxtNumDao.Text.Replace(":", "")) 'A cause des deux (:) dans la clé des membre de la commission

        If PourAjoutModifDansDB = 1 Then 'En cours d'enregistrement

            query = "SELECT COUNT(*) FROM t_dao WHERE NumeroDAO='" & NumDoss & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                FailMsg("Ce numéro existe déjà.")
                TxtNumDao.Select()
                Exit Sub
            End If

            'dans la table AMI
            query = "select count(*) from t_ami where NumeroDAMI='" & NumDoss & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                FailMsg("Ce numéro existe déjà.")
                TxtNumDao.Select()
                Exit Sub
            End If

            'Dans la table DP
            query = "select count(*) from t_dp where NumeroDp='" & NumDoss & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                FailMsg("Ce numéro existe déjà.")
                TxtNumDao.Select()
                Exit Sub
            End If

            Dim CompteVenteDAO As String = String.Empty
            If CmbCompte.SelectedIndex <> -1 Then
                CompteVenteDAO = Split(CmbCompte.Text, " - ")(1)
            End If

            Dim AttributionMarche As String = "Lot"
            If Not rdAttrLot.Checked Then
                AttributionMarche = "Sous Lot"
            End If

            Dim DateReports As String = ""
            If DateReporte.Text <> "" And HeureReporte.Text <> "" Then
                DateReports = dateconvert(DateReporte.Text) & " " & HeureReporte.Text
            End If

            Dim DelaiPublication As String = NbreDelaiPub.Text & " " & JoursDelaiPub.Text
            query = "INSERT INTO t_dao (NumeroDAO, IntituleDAO, RefMarche, MontantMarche, TypeMarche, MethodePDM, NbreLotDAO, DateSaisie, DateModif, Operateur, PrixDAO, CompteAchat, CodeProjet, DateOuverture, DateLimiteRemise, CodeConvention, Attribution, DateEdition, DatePublication, JournalPublication, DelaiPublication,DateReport,statut_DAO, LigneBudgetaire, LieurRemiseFourniture, NaturePrix) VALUES"
            query &= "('" & NumDoss & "','" & EnleverApost(TxtLibelleDao.Text) & "','" & CurrentMarche("RefMarche") & "','" & MontantMarche.Text.Replace(" ", "").Replace(",", ".") & "', '" & CurrentMarche("TypeMarche") & "', '" & GetMethode(CurrentMarche("CodeProcAO")) & "','" & TxtNbreLot.Value & "','" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "','" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "','" & CodeOperateurEnCours & "','" & TxtPrixDao.Text & "','" & CompteVenteDAO & "','" & ProjetEnCours & "','" & dateconvert(DateOuverture.Text) & " " & HeureOuverture.Text & "','" & dateconvert(DateDepot.Text) & " " & HeureDepot.Text & "','" & CurrentMarche("Convention_ChefFile") & "','" & AttributionMarche & "','" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "','" & dateconvert(DatePublication.Text) & " " & HeurePub.Text & "','" & EnleverApost(NomJournal.Text) & "','" & DelaiPublication & "', '" & DateReports & "','En cours', '" & EnleverApost(LigneBudgetaire.Text) & "', '" & EnleverApost(LieurRemiseFourniture.Text) & "', '" & EnleverApost(NaturePrix.Text.Split("-")(0).Trim) & "')"
            'LigneBudgetaire
            'LigneBudgetaire LieurRemiseFourniture
            Try
                DebutChargement(True, "Enregistrement du dossier en cours...")
                Dim NumLot As Integer = 0
                Dim RefLot As Decimal = 0
                Dim ListesSousLots As String = ""

                ExecuteNonQuery(query)
                ExecuteNonQuery("UPDATE t_marche SET NumeroDAO='" & NumDoss & "' WHERE RefMarche='" & CurrentMarche("RefMarche") & "'")
                ExecuteNonQuery("INSERT INTO t_dao_donneesparticuliers(RefDP,NumeroDAO,DateSaisie,CodeProjet) VALUES(NULL, '" & NumDoss & "', '" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "','" & ProjetEnCours & "')")

                'Enregistrements des lots
                GetSaveLot(NumDoss)

                PourAjoutModifDansDB = 2
                TabAMettreAJour(0) = True
                TypeMarche = CurrentMarche("TypeMarche").ToString
                MethodMarche = GetMethode(CurrentMarche("CodeProcAO"))
                'Verouillage des champs
                TxtNumDao.Enabled = False
                cmbTypeMarche.Enabled = False
                cmbMarches.Enabled = False
                TxtNbreLot.Enabled = False
                FinChargement()
                SuccesMsg("Dossier enregistré avec succès.")
                LoadArchivesDao()
                'Charger les lots
                ChargerLesLots(NumDoss)
                VisibleOtherTabs(True)

            Catch ex As Exception
                FinChargement()
                FailMsg("Impossible d'enregistrer ce DAO" & vbNewLine & "Contactez votre fournisseur" & vbNewLine & ex.ToString)
            End Try

        ElseIf PourAjoutModifDansDB = 2 Then
            'Le DAO est déjà dans la BD, il faut enregistrer toutes les tabs modifées
            DebutChargement(True, "Enregistrement du dossier en cours...")

            For i = 0 To XtraTabControl1.TabPages.Count - 1
                If TabAMettreAJour(i) Then 'On doit mettre à jour les données de cette tab
                    Dim CurrentTab As DevExpress.XtraTab.XtraTabPage = XtraTabControl1.TabPages(i)
                    Select Case CurrentTab.Name
                        Case "PageDonneBase"
                            If SavePageDonneBase(NumDoss) Then
                                Exit Select
                            Else
                                FinChargement()
                                Exit Sub
                            End If
                        Case "PageDonnePartic"
                            If SavePageDonnePartic(NumDoss) Then
                                Exit Select
                            Else
                                FinChargement()
                                Exit Sub
                            End If
                        Case "PageDQE"
                            If SavePageDQE(NumDoss) Then
                                Exit Select
                            Else
                                FinChargement()
                                Exit Sub
                            End If
                        'Case "PageConformTechnique"
                        '    If SavePageConformTechnique(NumDoss) Then
                        '        Exit Select
                        '    Else
                        '        FinChargement()
                        '        Exit Sub
                        '    End If
                        'Case "PageSpecTech"
                        '    If SavePageSpecTech(NumDoss) Then
                        '        Exit Select
                        '    Else
                        '        FinChargement()
                        '        Exit Sub
                        '    End If
                        Case "PagePostQualif"
                            If SavePagePostQualif(NumDoss) Then
                                Exit Select
                            Else
                                FinChargement()
                                Exit Sub
                            End If
                        Case "PageApercu"
                            Exit Select
                        Case Else
                            Exit Select
                    End Select
                End If
            Next
            FinChargement()
            SuccesMsg("Enregistrement effectué avec succès.")
        End If
    End Sub
#End Region

#Region "Données particulières"

    Private Sub InitDonneesPartic()
        CmbLangue.ResetText()
        CmbDevise.ResetText()
        TxtDevise.ResetText()
        NumValidite.Value = 0
        CmbValidite.ResetText()
        NumNbreCopie.Value = 1
        NumDelai.Value = 0
        CvConcil = ""
        NumEclaircissement.Value = 0
        CmbEclaircissement.ResetText()
        CmbDelai.ResetText()
        'ChecRevisionPrix.Checked = False
        DateReunion.EditValue = Nothing
        HeureReunion.EditValue = Nothing
        NumGroupement.Value = 0
        ModeAchemin.ResetText()
        FormePaiement.ResetText()
        PrctLot.ResetText()
        PrctArticle.ResetText()
        'MonaieAcheteur.Checked = False
        ' AutorisationFabrican.Checked = False
        ' ServiceVente.Checked = False
        ' MargePreferenc.Checked = False
        CheckNonEntite.Checked = True
        AdresslieuOuvr.ResetText()
        VillelieuOuvr.ResetText()
        BuroOuver.ResetText()
        PaysOuvertur.ResetText()
        DateSource.EditValue = Nothing
        SourceOfficielle.ResetText()
        NomReclama.ResetText()
        TitreReclam.ResetText()
        AdresseReclam.ResetText()
        TelecopieReclam.ResetText()
        CombSection.ResetText()
        TxtTextSection.ResetText()
        TxtSaisiTextSection.ResetText()
        NomEmprunteur.ResetText()
        TxtMaitreOuvrag.ResetText()
        ProjetSimilaireJustificatif.ResetText()
        GridSection.Rows.Clear()
        InitCojo()
        Dim dtCojo As DataTable = LgCojo.DataSource
        dtCojo.Rows.Clear()
        RdConcilNon.Checked = True
        TxtNomConcil.ResetText()
        TxtRemunConcil.ResetText()
        CmbRemunConcil.ResetText()
        TxtDesigneConcil.ResetText()
        TxtAdresseDesigneConcil.ResetText()
        TxtCvConcil.ResetText()
        CmbTitreCojo.ResetText()
        VisiteduSite.Checked = False
        Variante.Checked = False
        CheckVoiElectro.Checked = False
        DocumentExige.Checked = False
        GarantiExige.Checked = False
        AutorisationFabrican.Checked = False
        FormationUtilisateur.Checked = False
        ServiceApresVente.Checked = False
        MaterielRequis.Checked = False
        AttestationCNPS.Checked = False
        AttestationReguFiscal.Checked = False

    End Sub

    Private Sub ItemDevise()
        query = "select AbregeDevise from T_Devise"
        CmbDevise.ResetText()
        CmbDevise.Properties.Items.Clear()
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbDevise.Properties.Items.Add(rw("AbregeDevise").ToString)
        Next
    End Sub

    Private Sub CmbDevise_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbDevise.SelectedValueChanged
        If (CmbDevise.Text <> "") Then
            query = "select LibelleDevise from T_Devise where AbregeDevise='" & EnleverApost(CmbDevise.Text) & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtDevise.Text = rw("LibelleDevise").ToString
            Next
        End If
    End Sub

    Private Sub RdConcilOui_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdConcilOui.CheckedChanged
        If (RdConcilOui.Checked = True) Then
            TxtNomConcil.Enabled = True
            TxtRemunConcil.Enabled = True
            CmbRemunConcil.Enabled = True
            TxtDesigneConcil.Enabled = True
            TxtAdresseDesigneConcil.Enabled = True
            BtCvConcil.Enabled = True
        Else
            TxtNomConcil.Enabled = False
            TxtRemunConcil.Enabled = False
            CmbRemunConcil.Enabled = False
            TxtDesigneConcil.Enabled = False
            TxtAdresseDesigneConcil.Enabled = False
            BtCvConcil.Enabled = False
            TxtNomConcil.Text = ""
            TxtRemunConcil.Text = ""
            CmbRemunConcil.Text = ""
            TxtDesigneConcil.Text = ""
            TxtAdresseDesigneConcil.Text = ""
        End If
    End Sub

    Private Sub BtCvConcil_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtCvConcil.Click
        Dim dlg As New OpenFileDialog
        If dlg.ShowDialog() = DialogResult.OK Then
            TxtCvConcil.Text = dlg.SafeFileName
            CvConcil = dlg.FileName
        End If
    End Sub

    Private Sub BtAjoutCojo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjoutCojo.Click
        If CmbCivCojo.IsRequiredControl("Veuillez choisir la civilité") Then
            CmbCivCojo.Select()
            Exit Sub
        End If
        If TxtCojo.IsRequiredControl("Veuillez entrer le nom") Then
            TxtCojo.Select()
            Exit Sub
        End If
        If TxtFonctionCojo.IsRequiredControl("Veuillez entrer l'organisme") Then
            TxtFonctionCojo.Select()
            Exit Sub
        End If
        If CmbTitreCojo.IsRequiredControl("Veuillez choisir le titre") Then
            CmbTitreCojo.Select()
            Exit Sub
        End If
        If TxtMailCojo.IsRequiredControl("Veuillez entrer l'email") Then
            TxtMailCojo.Select()
            Exit Sub
        End If

        If BtAjoutCojo.Text = "Ajouter" Then
            Dim dt As DataTable = LgCojo.DataSource
            Dim drS As DataRow = dt.NewRow
            drS("IdCommission") = ""
            drS("Nom et prénoms") = (CmbCivCojo.Text & " " & TxtCojo.Text.Trim()).Trim()
            drS("Organisme") = TxtFonctionCojo.Text.Trim()
            drS("Titre") = CmbTitreCojo.Text.Trim()
            drS("Téléphone") = TxtContactCojo.Text.Trim()
            drS("Email") = TxtMailCojo.Text.Trim()
            drS("LigneModif") = ""
            dt.Rows.Add(drS)
        ElseIf NomGridView = "Cojo" And LigneaModifier = True Then
            ViewCojo.SetRowCellValue(IndexActive, "Nom et prénoms", (CmbCivCojo.Text & " " & TxtCojo.Text.Trim()).Trim())
            ViewCojo.SetRowCellValue(IndexActive, "Organisme", TxtFonctionCojo.Text.Trim())
            ViewCojo.SetRowCellValue(IndexActive, "Titre", CmbTitreCojo.Text.Trim())
            ViewCojo.SetRowCellValue(IndexActive, "Téléphone", TxtContactCojo.Text.Trim())
            ViewCojo.SetRowCellValue(IndexActive, "Email", TxtMailCojo.Text.Trim())
            ViewCojo.SetRowCellValue(IndexActive, "LigneModif", "Modifier")
        End If
        InitCojo()
        NomGridView = ""
        LigneaModifier = False
        IndexActive = 0
    End Sub

    Private Sub InitCojo()
        BtAjoutCojo.Text = "Ajouter"
        CmbCivCojo.ResetText()
        TxtCojo.ResetText()
        TxtFonctionCojo.ResetText()
        TxtContactCojo.ResetText()
        TxtMailCojo.ResetText()
        CmbTitreCojo.ResetText()
        CmbCivCojo.Focus()
    End Sub

    Private Sub ContextMenuStripCojo_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStripCojo.Opening
        If AfficherDossier = True Or ViewCojo.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub ToolStripMenuModifierCojo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuModifierCojo.Click
        If ViewCojo.RowCount > 0 Then
            IndexActive = ViewCojo.FocusedRowHandle
            BtAjoutCojo.Text = "Modifier"
            Dim Drx As DataRow = ViewCojo.GetFocusedDataRow
            CmbCivCojo.Text = Split(Drx("Nom et prénoms").ToString(), " ")(0)
            TxtCojo.Text = Drx("Nom et prénoms").ToString().Replace(CmbCivCojo.Text & " ", "")
            CmbTitreCojo.Text = Drx("Titre").ToString()
            TxtContactCojo.Text = Drx("Téléphone").ToString()
            TxtFonctionCojo.Text = Drx("Organisme").ToString()
            TxtMailCojo.Text = Drx("Email").ToString()
            CmbCivCojo.Focus()
            NomGridView = "Cojo"
            LigneaModifier = True
        End If
    End Sub

    Private Sub ToolStripMenuSupprimerCojo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuSupprimerCojo.Click
        If ViewCojo.RowCount > 0 Then
            If ConfirmMsg("Confirmez-vous la suppression de ce membre ?") = DialogResult.Yes Then
                Dim IdCojo As String = ViewCojo.GetFocusedRowCellValue("IdCommission")
                ViewCojo.GetFocusedDataRow().Delete()
                If IdCojo <> "" Then
                    ExecuteNonQuery("delete from t_commission where CodeMem='" & IdCojo & "'")
                End If
                If BtAjoutCojo.Text = "Modifier" Then BtAjoutCojo.Text = "Ajouter"
                NomGridView = ""
                LigneaModifier = False
                IndexActive = 0
            End If
        End If
    End Sub

    Private Sub ChargerLesMembreCojo(NumeroDao As String)
        Try
            'Chargement des cojos
            query = "SELECT * FROM t_commission WHERE NumeroDAO='" & NumeroDao & "'" 'AND TypeComm='COJO'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            Dim dtCojo As DataTable = LgCojo.DataSource
            dtCojo.Rows.Clear()

            For Each rw As DataRow In dt.Rows
                Dim drS = dtCojo.NewRow()
                drS("IdCommission") = rw("CodeMem")
                drS("Nom et prénoms") = MettreApost(rw("NomMem"))
                drS("Téléphone") = MettreApost(rw("TelMem"))
                drS("Email") = MettreApost(rw("EmailMem"))
                drS("Organisme") = MettreApost(rw("FoncMem"))
                drS("Titre") = MettreApost(rw("TitreMem"))
                drS("LigneModif") = "Enregistrer"
                dtCojo.Rows.Add(drS)
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub LoadPageDonnePartic(ByVal NumDossier As String)
        InitDonneesPartic()
        If Not PageDonnePartic.PageEnabled Then PageDonnePartic.PageEnabled = True

        'Remplir ComboSection
        RemplirCombSection(cmbTypeMarche.Text)

        If IsNothing(CurrentDao) Then
            Dim dtDao As DataTable = ExcecuteSelectQuery("SELECT * FROM t_dao WHERE NumeroDAO='" & NumDossier & "'")
            If dtDao.Rows.Count = 0 Then
                CurrentDao = Nothing
                Exit Sub
            End If
            CurrentDao = dtDao.Rows(0)
        End If

        If IsNothing(CurrentMarche) Then
            Dim dtmarche As DataTable = ExcecuteSelectQuery("SELECT * FROM t_marche WHERE RefMarche='" & CurrentDao("RefMarche") & "'")
            If dtmarche.Rows.Count > 0 Then
                CurrentMarche = dtmarche.Rows(0)
            Else
                CurrentMarche = Nothing
                FailMsg("Le marché associé a été supprimé")
                Exit Sub
            End If
        End If

        Try
            CmbLangue.Text = CurrentDao("LangueSoumission").ToString
            Dim Validite As String = CurrentDao("ValiditeOffre").ToString()
            If Validite <> String.Empty Then
                NumValidite.Value = Val(Split(Validite, " ")(0))
                CmbValidite.Text = Split(Validite, " ")(1)
            Else
                NumValidite.Value = 0
                CmbValidite.ResetText()
            End If

            CmbDevise.Text = CurrentDao("MonnaieEvalDAO").ToString()
            NumNbreCopie.Value = Val(CurrentDao("NbCopieSoumission").ToString())
            If MettreApost(CurrentDao("MeOuvrageDelegue").ToString).Trim <> "" Then
                CheckEntite.Checked = True
                TxtMaitreOuvrag.Text = MettreApost(CurrentDao("MeOuvrageDelegue").ToString)
            End If
            NomEmprunteur.Text = MettreApost(CurrentDao("NomEmprunteur").ToString)

            Dim DelaiExec As String = CurrentDao("DelaiExecution").ToString()
            If DelaiExec <> String.Empty Then
                NumDelai.Value = Val(Split(DelaiExec, " ")(0))
                CmbDelai.Text = Split(DelaiExec, " ")(1)
            Else
                NumDelai.Value = 0
                CmbDelai.ResetText()
            End If

            If CurrentDao("DateReunionPrepa").ToString.Trim <> "" Then
                DateReunion.EditValue = CDate(CurrentDao("DateReunionPrepa").ToString()).ToShortDateString()
                HeureReunion.EditValue = CDate(CurrentDao("DateReunionPrepa").ToString()).ToLongTimeString
            End If
            AttestationCNPS.Checked = IIf(CurrentDao("AttestationCNPS").ToString = "OUI", True, False).ToString
            AttestationReguFiscal.Checked = IIf(CurrentDao("AttestationReguFiscal").ToString = "OUI", True, False).ToString

            'If Val(CurrentDao("NbreMembregroup").ToString()) = 0 Then
            NumGroupement.Value = Val(CurrentDao("NbreMembregroup").ToString())
            'End If

            If CurrentDao("NomConciliateur").ToString().Trim <> "" Then
                RdConcilOui.Checked = True
                TxtNomConcil.Text = MettreApost(CurrentDao("NomConciliateur").ToString())
                Dim Remun As String = MettreApost(CurrentDao("MontConciliateur").ToString())
                If Remun <> String.Empty Then
                    TxtRemunConcil.Text = Split(Remun, "/")(0)
                    CmbRemunConcil.Text = Split(Remun, "/")(1)
                Else
                    TxtRemunConcil.ResetText()
                    CmbRemunConcil.ResetText()
                End If

                TxtDesigneConcil.Text = MettreApost(CurrentDao("DesignConciliateur").ToString())
                TxtAdresseDesigneConcil.Text = MettreApost(CurrentDao("DesignAdresse").ToString())
                TxtCvConcil.Text = MettreApost(CurrentDao("CvConciliateur").ToString())
            End If

            'Chargement des cojos
            ChargerLesMembreCojo(NumDossier)
            ChargerLesSection(NumDossier)
            UpdateDAO_DonneeParticulier(NumDossier, "Load")
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Function SavePageDonnePartic(ByVal NumDossier As String) As Boolean
        Try
            'Vérification des champs
            If CmbLangue.IsRequiredControl("Veuillez choisir une langue dans la liste.") Then
                CmbLangue.Select()
                Return False
            End If
            If CmbDevise.IsRequiredControl("Veuillez choisir une devise dans la liste.") Then
                CmbDevise.Select()
                Return False
            End If
            If NumValidite.Value <> 0 Then
                If CmbValidite.IsRequiredControl("Veuillez bien définir la validité.") Then
                    CmbValidite.Select()
                    Return False
                End If
            ElseIf Val(NumValidite.Value) <= 0 Then
                SuccesMsg("Veuillez entrer la validité")
                NumValidite.Select()
                Return False
            End If

            If NumDelai.Value <> 0 Then
                If CmbDelai.IsRequiredControl("Veuillez bien définir le délai.") Then
                    CmbDelai.Select()
                    Return False
                End If
            ElseIf Val(NumDelai.Value) <= 0 Then
                SuccesMsg("Veuillez entrer le délai")
                NumDelai.Select()
                Return False
            End If

            If Val(NumNbreCopie.Value) <= 0 Then
                SuccesMsg("Veuillez entrer le nombre de copies")
                NumNbreCopie.Select()
                Return False
            End If

            If RdConcilOui.Checked = True Then
                If TxtNomConcil.IsRequiredControl("Veuillez saisir le nom du conciliateur") Then
                    TxtNomConcil.Select()
                    Return False
                End If
                If TxtRemunConcil.IsRequiredControl("Veuillez saisir la rémunération du conciliateur") Then
                    TxtRemunConcil.Select()
                    Return False
                End If
                If CmbRemunConcil.IsRequiredControl("Veuillez selectionner le type de rémunération du conciliateur") Then
                    CmbRemunConcil.Select()
                    Return False
                End If
            End If

            If CheckEntite.Checked = True And TxtMaitreOuvrag.Text = "" Then
                SuccesMsg("Veuillez saisir le maitre d'ouvrage délégue")
                TxtMaitreOuvrag.Select()
                Return False
            End If

            If ViewCojo.RowCount = 0 Then
                SuccesMsg("Veuillez ajouter un membre de la commission")
                Return False
            End If

            If (DateReunion.Text.Trim = "" And HeureReunion.Text <> "") Or (DateReunion.Text <> "" And HeureReunion.Text = "") Then
                SuccesMsg("Veuillez bien definir la date et l'heure de la reunion préparatoire")
                DateReunion.Select()
                Return False
            End If

            If (Val(NumEclaircissement.Value.ToString) > 0 And CmbEclaircissement.Text = "") Or (Val(NumEclaircissement.Value.ToString) <= 0 And CmbEclaircissement.Text <> "") Then
                SuccesMsg("Veuillez bien definir l'eclaircisement")
                NumEclaircissement.Select()
                Return False
            End If
            If DocumentExige.Checked = True And NatureDocument.Text = "" Then
                SuccesMsg("Veuillez saisir la nature de la documentation exigée.")
                NatureDocument.Select()
                Return False
            End If
            If GarantiExige.Checked = True And Val(DelaiGarantie.Text) <= 0 Then
                SuccesMsg("Veuillez definir le délai de la garantie.")
                DelaiGarantie.Select()
                Return False
            End If
            If CheckVoiElectro.Checked = True And PrecedurVoiElectronic.Text = "" Then
                SuccesMsg("Veuillez decrire la procédure de remise des offres par voie electronique.")
                PrecedurVoiElectronic.Select()
                Return False
            End If
            If Variante.Checked = True And ListeVariante.Text = "" Then
                SuccesMsg("Veuillez saisir les variantes autorisées.")
                ListeVariante.Select()
                Return False
            End If
            If VisiteduSite.Checked = True And DateVisite.Text = "" Then
                SuccesMsg("Veuillez sélectionner la date de la visite.")
                DateVisite.Select()
                Return False
            End If

            If RdConcilOui.Checked = True Then
                If TxtCvConcil.Text.Trim() <> String.Empty Then
                    If File.Exists(CvConcil) Then
                        Try
                            Dim DossierDAO As String = line & "\DAO\" & TypeMarche & "\" & MethodMarche & "\" & FormatFileName(TxtNumDao.Text.Replace(":", ""), "")
                            If Not Directory.Exists(DossierDAO) Then
                                Directory.CreateDirectory(DossierDAO)
                            End If

                            If Not File.Exists(DossierDAO & "\" & TxtCvConcil.Text) Then
                                File.Copy(CvConcil, DossierDAO & "\" & TxtCvConcil.Text, True)
                            End If

                        Catch exs As IOException
                            SuccesMsg("Un exemplaire du fichier est uliser par une autre application" & vbNewLine & "Veuillez le fermer svp")
                            Return False
                        Catch ex As Exception
                            FailMsg(ex.ToString)
                            Return False
                        End Try
                    End If
                End If
            End If

            'Mise a jour dans table Dao ********
            query = "Update T_DAO set NomConciliateur='" & EnleverApost(TxtNomConcil.Text) & "', MontConciliateur='" & TxtRemunConcil.Text.Trim().Replace(" ", "") & "/" & CmbRemunConcil.Text.Trim() & "', DesignConciliateur='" & EnleverApost(TxtDesigneConcil.Text) & "', DesignAdresse='" & EnleverApost(TxtAdresseDesigneConcil.Text) & "', AttestationCNPS = '" & IIf(AttestationCNPS.Checked = True, "OUI", "NON").ToString & "', AttestationReguFiscal ='" & IIf(AttestationReguFiscal.Checked = True, "OUI", "NON").ToString & "',"
            query &= "DelaiExecution='" & NumDelai.Value.ToString & " " & CmbDelai.Text & "', NbCopieSoumission='" & NumNbreCopie.Value.ToString & "', ValiditeOffre='" & NumValidite.Value.ToString & " " & CmbValidite.Text & "', MonnaieEvalDAO='" & CmbDevise.Text & "', LangueSoumission='" & CmbLangue.Text & "', MeOuvrageDelegue='" & IIf(CheckEntite.Checked = True, EnleverApost(TxtMaitreOuvrag.Text), "").ToString & "', "
            query &= " CvConciliateur='" & IIf(RdConcilOui.Checked = True, EnleverApost(TxtCvConcil.Text.Trim()), "").ToString & "', NbreMembreGroup='" & NumGroupement.Value.ToString & "', NomEmprunteur='" & EnleverApost(NomEmprunteur.Text) & "', DateReunionPrepa='" & DateReunion.Text & " " & HeureReunion.Text & "', NbCopieSoumission='" & NumNbreCopie.Value.ToString & "' where NumeroDAO='" & NumDossier & "' and CodeProjet='" & ProjetEnCours & "'"
            ExecuteNonQuery(query)

            'Enregistrement des cojos
            Dim Actuliser As Boolean = False
            If ViewCojo.RowCount > 0 Then
                Dim IdCojo As String = ""
                For i = 0 To ViewCojo.RowCount - 1
                    IdCojo = ViewCojo.GetRowCellValue(i, "IdCommission").ToString()
                    If IdCojo = "" Then
                        query = "INSERT INTO t_commission(CodeMem,NomMem,TelMem,EmailMem,FoncMem,TitreMem,NumeroDAO,TypeComm,DateSaisie,DateModif,Operateur,PasseMem,AuthKey)"
                        query &= " VALUES(NULL,'" & EnleverApost(ViewCojo.GetRowCellValue(i, "Nom et prénoms").ToString()) & "','" & EnleverApost(ViewCojo.GetRowCellValue(i, "Téléphone").ToString()) & "',"
                        query &= "'" & EnleverApost(ViewCojo.GetRowCellValue(i, "Email").ToString()) & "','" & EnleverApost(ViewCojo.GetRowCellValue(i, "Organisme").ToString()) & "',"
                        query &= "'" & EnleverApost(ViewCojo.GetRowCellValue(i, "Titre").ToString()) & "','" & NumDossier & "','COJO','" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "','" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "','" & CodeOperateurEnCours & "', NULL,NULL)"
                        ExecuteNonQuery(query)
                        Actuliser = True
                    ElseIf ViewCojo.GetRowCellValue(i, "LigneModif").ToString = "Modifier" Then
                        query = "UPDATE t_commission SET NomMem='" & EnleverApost(ViewCojo.GetRowCellValue(i, "Nom et prénoms").ToString()) & "', TelMem='" & EnleverApost(ViewCojo.GetRowCellValue(i, "Téléphone").ToString()) & "',"
                        query &= "EmailMem='" & EnleverApost(ViewCojo.GetRowCellValue(i, "Email").ToString()) & "', FoncMem='" & EnleverApost(ViewCojo.GetRowCellValue(i, "Organisme").ToString()) & "',TitreMem='" & EnleverApost(ViewCojo.GetRowCellValue(i, "Titre").ToString()) & "',"
                        query &= "DateModif='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' WHERE CodeMem='" & IdCojo & "'"
                        ExecuteNonQuery(query)
                    End If
                Next
                If Actuliser = True Then
                    ChargerLesMembreCojo(NumDossier)
                End If
            End If

            Actuliser = False
            If GridSection.RowCount > 0 Then
                For k = 0 To GridSection.RowCount - 1
                    If GridSection.Rows.Item(k).Cells("RefSection").Value.ToString = "" Then
                        query = "insert into t_dao_section values(NULL, '" & NumDossier & "', '" & EnleverApost(GridSection.Rows.Item(k).Cells("CodeSection").Value) & "', '" & EnleverApost(GridSection.Rows.Item(k).Cells("DescriptionSection").Value) & "', '" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', '" & ProjetEnCours & "')"
                        ExecuteNonQuery(query)
                        Actuliser = True
                        'cojo modifier
                    ElseIf (GridSection.Rows.Item(k).Cells("LigneModif").Value.ToString = "Modifier") Then
                        query = "update t_dao_section set Description='" & EnleverApost(GridSection.Rows.Item(k).Cells("DescriptionSection").Value) & "',  DateSaisie='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' where RefSection='" & GridSection.Rows.Item(k).Cells("RefSection").Value & "'"
                        ExecuteNonQuery(query)
                    End If
                Next

                If Actuliser = True Then
                    ChargerLesSection(NumDossier)
                End If
            End If

            UpdateDAO_DonneeParticulier(NumDossier)
        Catch ex As Exception
            FailMsg(ex.ToString)
            Return False
        End Try
        Return True
    End Function

    Private Sub UpdateDAO_DonneeParticulier(ByVal NumeroDAO As String, Optional TypeRequette As String = "")
        Try
            If TypeRequette = "" Then
                query = "UPDATE t_dao_donneesparticuliers set DocumentExige='" & IIf(DocumentExige.Checked = True, "OUI", "NON").ToString & "', NatureDocument='" & IIf(DocumentExige.Checked = True, EnleverApost(NatureDocument.Text), "").ToString & "', AutorisationFabrican='" & IIf(AutorisationFabrican.Checked = True, "OUI", "NON").ToString & "', MaterielRequis='" & IIf(MaterielRequis.Checked = True, "OUI", "NON").ToString & "', FormationUtilisateur='" & IIf(FormationUtilisateur.Checked = True, "OUI", "NON").ToString & "', ServiceApresVente='" & IIf(ServiceApresVente.Checked = True, "OUI", "NON").ToString & "',"
                query &= "GarantiExige='" & IIf(GarantiExige.Checked = True, "OUI", "NON").ToString & "', DelaiGarantie='" & IIf(GarantiExige.Checked = True, DelaiGarantie.Text, 0).ToString & "', CheckVoiElectro='" & IIf(CheckVoiElectro.Checked = True, "OUI", "NON").ToString & "', AdresslieuOuvr='" & EnleverApost(AdresslieuOuvr.Text) & "', VillelieuOuvr='" & EnleverApost(VillelieuOuvr.Text) & "', BuroOuver='" & EnleverApost(BuroOuver.Text) & "', PaysOuvertur='" & EnleverApost(PaysOuvertur.Text) & "',  DateSource='" & DateSource.Text & "', SourceOfficielle='" & EnleverApost(SourceOfficielle.Text) & "', "
                query &= "PrecedurVoiElectronic='" & IIf(CheckVoiElectro.Checked = True, EnleverApost(PrecedurVoiElectronic.Text), "").ToString & "', Variante='" & IIf(Variante.Checked = True, "OUI", "NON").ToString & "', ListeVariante='" & IIf(Variante.Checked = True, EnleverApost(ListeVariante.Text), "").ToString & "', VisiteduSite='" & IIf(VisiteduSite.Checked = True, "OUI", "NON").ToString & "', DateVisite='" & IIf(VisiteduSite.Checked = True, DateVisite.Text, "").ToString & "', "
                query &= "ModeAchemin='" & EnleverApost(ModeAchemin.Text) & "', FormePaiement='" & EnleverApost(FormePaiement.Text) & "', PrctLot='" & EnleverApost(PrctLot.Text) & "', PrctArticle='" & EnleverApost(PrctArticle.Text) & "',  NomReclama='" & EnleverApost(NomReclama.Text) & "', TitreReclam='" & EnleverApost(TitreReclam.Text) & "', ProjetSimilaireJustificatif='" & EnleverApost(ProjetSimilaireJustificatif.Text) & "', "
                query &= "NumEclaircissement='" & NumEclaircissement.Value.ToString & " " & CmbEclaircissement.Text & "', AdresseReclam='" & EnleverApost(AdresseReclam.Text) & "', AgenceReclam='" & EnleverApost(AgenceReclam.Text) & "', TelecopieReclam ='" & EnleverApost(TelecopieReclam.Text) & "', DateSaisie='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' where NumeroDAO='" & NumeroDAO & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            Else
                query = "SELECT * FROM t_dao_donneesparticuliers WHERE NumeroDAO='" & NumeroDAO & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw In dt.Rows
                    AutorisationFabrican.Checked = IIf(rw("AutorisationFabrican").ToString = "OUI", True, False).ToString
                    MaterielRequis.Checked = IIf(rw("MaterielRequis").ToString = "OUI", True, False).ToString
                    FormationUtilisateur.Checked = IIf(rw("FormationUtilisateur").ToString = "OUI", True, False).ToString
                    ServiceApresVente.Checked = IIf(rw("ServiceApresVente").ToString = "OUI", True, False).ToString

                    DocumentExige.Checked = IIf(rw("DocumentExige").ToString = "OUI", True, False).ToString
                    NatureDocument.Text = IIf(rw("DocumentExige").ToString = "OUI", MettreApost(rw("NatureDocument").ToString), "").ToString
                    GarantiExige.Checked = IIf(rw("GarantiExige").ToString = "OUI", True, False).ToString
                    DelaiGarantie.Value = IIf(rw("GarantiExige").ToString = "OUI", Val(rw("DelaiGarantie").ToString), 0).ToString
                    CheckVoiElectro.Checked = IIf(rw("CheckVoiElectro").ToString = "OUI", True, False).ToString
                    PrecedurVoiElectronic.Text = IIf(rw("CheckVoiElectro").ToString = "OUI", MettreApost(rw("PrecedurVoiElectronic").ToString), "").ToString

                    Variante.Checked = IIf(rw("Variante").ToString = "OUI", True, False).ToString
                    ListeVariante.Text = IIf(rw("Variante").ToString = "OUI", MettreApost(rw("ListeVariante").ToString), "").ToString
                    VisiteduSite.Checked = IIf(rw("VisiteduSite").ToString = "OUI", True, False).ToString
                    DateVisite.Text = IIf(rw("VisiteduSite").ToString = "OUI", MettreApost(rw("DateVisite").ToString), "").ToString

                    ModeAchemin.Text = MettreApost(rw("ModeAchemin").ToString)
                    FormePaiement.Text = MettreApost(rw("FormePaiement").ToString)
                    PrctLot.Text = MettreApost(rw("PrctLot").ToString)
                    PrctArticle.Text = MettreApost(rw("PrctArticle").ToString)
                    AdresslieuOuvr.Text = MettreApost(rw("AdresslieuOuvr").ToString)
                    VillelieuOuvr.Text = MettreApost(rw("VillelieuOuvr").ToString)
                    BuroOuver.Text = MettreApost(rw("BuroOuver").ToString)
                    PaysOuvertur.Text = MettreApost(rw("PaysOuvertur").ToString)
                    DateSource.Text = rw("DateSource").ToString
                    SourceOfficielle.Text = MettreApost(rw("SourceOfficielle").ToString)
                    NomReclama.Text = MettreApost(rw("NomReclama").ToString)
                    TitreReclam.Text = MettreApost(rw("TitreReclam").ToString)
                    AdresseReclam.Text = MettreApost(rw("AdresseReclam").ToString)
                    AgenceReclam.Text = MettreApost(rw("AgenceReclam").ToString)
                    TelecopieReclam.Text = MettreApost(rw("TelecopieReclam").ToString)
                    ProjetSimilaireJustificatif.Text = MettreApost(rw("ProjetSimilaireJustificatif").ToString)

                    Dim NumEclai As String() = rw("NumEclaircissement").ToString.Split(" ")
                    NumEclaircissement.Text = Val(NumEclai(0))
                    If NumEclai.Length > 1 Then
                        CmbEclaircissement.Text = NumEclai(1)
                    End If
                Next
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub


    Private Sub BtAjoutSection_Click(sender As Object, e As EventArgs) Handles BtAjoutSection.Click

        If TxtSaisiTextSection.IsRequiredControl("Veuillez saisir la description de la section") Then
            TxtSaisiTextSection.Focus()
            Exit Sub
        End If

        Dim n As Integer
        If LigneaModifier = True And NomGridView = "GridSection" Then
            n = IndexActive
            GridSection.Rows.Item(n).Cells("LigneModif").Value = "Modifier"
        Else
            If CombSection.SelectedIndex = -1 Then
                SuccesMsg("Veuillez selectionner la section à saisir")
                CombSection.Focus()
                Exit Sub
            End If
            n = GridSection.Rows.Add()
            GridSection.Rows.Item(n).Cells("RefSection").Value = ""
            GridSection.Rows.Item(n).Cells("LigneModif").Value = "Ajouter"
        End If

        GridSection.Rows.Item(n).Cells("CodeSection").Value = CombSection.Text.Replace("Section ", "")
        GridSection.Rows.Item(n).Cells("DescriptionSection").Value = TxtSaisiTextSection.Text

        CombSection.Enabled = True
        ' CombSection.Text = ""
        TxtSaisiTextSection.Text = ""
        NomGridView = ""
        LigneaModifier = False
        IndexActive = 0
    End Sub

    Private Sub BtSupSection_Click(sender As Object, e As EventArgs) Handles BtSupSection.Click
        Try
            If GridSection.RowCount > 0 Then
                Dim Index = GridSection.CurrentRow.Index
                If ConfirmMsg("Êtes-vous sûr de vouloir supprimer la ligne N° " & Index + 1 & "?.") = DialogResult.Yes Then
                    Dim RefSection As String = GridSection.Rows.Item(Index).Cells("RefSection").Value.ToString
                    If RefSection.ToString <> "" Then
                        ExecuteNonQuery("delete from t_dao_section where RefSection='" & RefSection & "'")
                    End If
                    GridSection.Rows.RemoveAt(Index)
                    NomGridView = ""
                    LigneaModifier = False
                    CombSection.Enabled = True
                    IndexActive = 0
                End If
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub RemplirCombSection(TypeMarche As String)
        Try
            Dim CodeSection As Array
            Dim ok As Boolean = False
            If (TypeMarche.ToString.ToLower = "fournitures") Then
                If TxtMethodeMarche.Text.ToUpper = "AOI" Or TxtMethodeMarche.Text.ToUpper = "AON" Then
                    CodeSection = {"IS 1.2(a)", "IS 4.5", "IS 4.8(a), 4.8(b) et 5.1", "IS 11.1(j)", "IS 13.1", "IS 14.5", "IS 14.7", "IS 14.8(b)(i) et (c)(v)", "IS 14.8(a)(iii)(ii) et (c)(v)", "IS 15.1", "IS 16.4", "IS 17.2(a)", "IS 17.2(b)", "IS 18.3(a)", "IS 19.1", "IS 19.3(d)", "IS 19.9(a)", "IS 19.9(b)", "IS 20.3", "IS 22.1(a)", "IS 22.1(b)", "IS 25.1", "IS 25.6", "IS 30.3", "IS 33.1", "IS 34.6(a)", "IS 34.6(b)", "IS 34.6(c)", "IS 34.6(d)", "IS 34.6(e)", "IS 34.6(f)", "IS 34.6(g)", "IS 42.1(a)", "IS 42.1(b)", "IS 45.1"}
                    ok = True
                End If
            ElseIf (TypeMarche.ToString.ToLower = "Travaux".ToLower) Then
                If TxtMethodeMarche.Text.ToUpper = "AOI" Or TxtMethodeMarche.Text.ToUpper = "AON" Then
                    CodeSection = {"IS 1.2(a)", "IS 4.5", "IS 11.1(h)", "IS 13.1", "IS 13.2", "IS 13.4", "IS 14.5", "IS 18.3(a)", "IS 19.3(d)", "IS 19.9", "IS 20.3", "IS 25.6", "IS 30.3", "IS 33.1", "IS 34.2", "IS 47.1"}
                    ok = True
                End If
            End If
            CombSection.ResetText()
            CombSection.Properties.Items.Clear()

            If ok = True Then
                For j = 0 To CodeSection.Length - 1
                    CombSection.Properties.Items.Add("Section " & CodeSection(j).ToString)
                Next
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub GridSection_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridSection.CellDoubleClick
        If GridSection.RowCount > 0 And AfficherDossier = False Then
            IndexActive = GridSection.CurrentRow.Index
            Dim CodeSection As String = GridSection.Rows.Item(IndexActive).Cells("CodeSection").Value
            NomGridView = "GridSection"
            LigneaModifier = True
            TxtSaisiTextSection.Text = GridSection.Rows.Item(IndexActive).Cells("DescriptionSection").Value
            CombSection.Text = IIf(CodeSection = "Attestation CNPS" Or CodeSection = "Attestation de régularité fiscale", CodeSection, "Section " & CodeSection).ToString
            CombSection.Enabled = False
        End If
    End Sub

    Private Sub CombSection_TextChanged(sender As Object, e As EventArgs) Handles CombSection.TextChanged
        TxtTextSection.Text = GetTextSection(CombSection.Text.Replace("Section ", "").Trim, cmbTypeMarche.Text)
        If CombSection.Text.Trim <> "" Then
            TxtSection.Properties.ReadOnly = False
        Else
            TxtSection.Properties.ReadOnly = True
        End If
    End Sub

    Private Function GetTextSection(CodeSection As String, TypeMarche As String) As String
        Try
            'If CodeSection.ToString = "ST(Spécification techique)" Then
            'Return "[insérer une description détaillée des specification technique]"
            'ElseIf CodeSection.ToString = "IE (Inspections et Essais)" Then
            'Return "[insérer la liste des inspections et des tests]."

            If TypeMarche.ToString.ToLower = "fournitures" Then
                'If TxtMethodeMarche.Text.ToUpper = "AOI" Or TxtMethodeMarche.Text.ToUpper = "AON" Then

                If CodeSection.ToString = "IS 1.2(a)" Then
                    Return "[Insérer l'identification du système électronique et l’adresse url ou le lien, Insérer lesdits aspects]"
                ElseIf CodeSection.ToString = "IS 4.8(a),4.8(b) et 5.1" Then
                    Return "[insérer la liste des pays inéligibles, ou s'il n’y en a pas, indiquer «aucun»]"
                ElseIf CodeSection.ToString = "IS 11.1(j)" Then
                    Return "[Insérer la liste des documents, si nécessaire, autres que ceux déjà mentionnés à l'article 11.1 des IS]"
                ElseIf CodeSection.ToString = "IS 13.1" Then
                    Return "[Les variantes [insérer « seront » ou « ne seront pas »] prises en compte]."
                ElseIf CodeSection.ToString = "IS 14.5" Then
                    Return "[Les prix proposés par le Soumissionnaire [insérer « seront » ou « ne seront pas »] des prix révisables]."
                ElseIf CodeSection.ToString = "IS 14.7" Then
                    Return "[L’édition des Incoterms à laquelle se référer est : [insérer la date d’édition en vigueur]."
                ElseIf CodeSection.ToString = "IS 14.8(b)(i) et (c)(v)" Then
                    Return "[Le lieu de destination est : [insérer le nom ; assurer la cohérence avec la définition de l‘ Incoterm utilisé]."
                ElseIf CodeSection.ToString = "IS 14.8(a)(iii), b(ii) et (c)(v)" Then
                    Return "[Insérer le nom du lieu d’utilisation des Fournitures]"
                ElseIf CodeSection.ToString = "IS 15.1" Then
                    Return "Le Soumissionnaire [insérer « est » ou « n’est pas »] tenu d’exprimer dans la monnaie du pays de l’Acheteur la fraction du prix de son offre correspondant à des dépenses encourues dans cette même monnaie."
                ElseIf CodeSection.ToString = "IS 16.4" Then
                    Return "[Insérer la période de fonctionnement prévue pour les fournitures (en vue des besoins en pièces de rechange)]"
                ElseIf CodeSection.ToString = "IS 17.2(a)" Then
                    Return "L ‘Autorisation du Fabriquant [insérer « est » ou « n’est pas »] requise."
                ElseIf CodeSection.ToString = "IS 17.2(b)" Then
                    Return "Un service après-vente [insérer « est » ou « n’est pas »] requis."
                ElseIf CodeSection.ToString = "IS 18.3(a)" Then
                    Return "[Insérer la méthode ou indiquer,comment il sera indiqué dans la demande de prorogation de validité des offres]. "
                ElseIf CodeSection.ToString = "IS 19.1" Then
                    Return "[Une garantie d’offre [Insérer « est » ou « n’est pas »] requise]."
                ElseIf CodeSection.ToString = "IS 19.3(d)" Then
                    Return "[Insérer les noms des autres types de garanties acceptables ou insérer «Néant» si une garantie d’offre n’est pas requise sous IS 19.1 ou si aucune forme de garantie d’offre autre que celles listées sous IS 19.3(a) à (c) n’est acceptable.]"
                ElseIf CodeSection.ToString = "IS 19.9(a)" Then
                    Return "[Inclure la disposition suivante et les informations correspondantes uniquement dans le cas où, conformément à l’article 19.1 des IS, une garantie d’offre n’est pas requise et que l’Acheteur prévoit d’exclure, pour une durée déterminée, le Soumissionnaire qui a commis un des actes mentionnés à l’article 19.9 (a) et (b) des IS. Dans le cas contraire, omettre cette disposition.]"
                ElseIf CodeSection.ToString = "IS 19.9(b)" Then
                    Return "[Si le Soumissionnaire commet un des actes décrits aux paragraphes (a) ou (b) du présent article, l’Acheteur l’exclura de toute attribution de marché(s) pour une période de [insérer le nombre d’années] ______  ans.]"
                ElseIf CodeSection.ToString = "IS 20.3" Then
                    Return "[insérer l’intitulé et la description des documents nécessaires à titre d’attestation de procuration (ou pouvoir) du signataire de l’offre.]"
                ElseIf CodeSection.ToString = "IS 22.1(a)" Then
                    Return "[Le soumissionnaire [insérer « aura » ou « n’aura pas »] l’option de soumettre son offre par voie électronique.]"
                ElseIf CodeSection.ToString = "IS 22.1(b)" Then
                    Return "[Si les Soumissionnaires peuvent soumettre leurs offres par voie électronique, la procédure de soumission est la suivante : [insérer une description de la procédure de soumission des offres par voie électronique le cas échéant]"
                ElseIf CodeSection.ToString = "IS 25.1" Then
                    Return "[Les procédures d’ouverture des plis remis par voie électronique, lorsqu’elles sont applicables, sont les suivantes : [insérer une description des procédures d’ouverture des plis par voie électronique.] "
                ElseIf CodeSection.ToString = "IS 25.6" Then
                    Return "[La Soumission et les Bordereaux des Prix seront paraphés par les [insérer le nombre des représentants.]"
                ElseIf CodeSection.ToString = "IS 30.3" Then
                    Return "[L’ajustement sera calculé comme étant [insérer « la moyenne » ou « la valeur la plus élevée »] _____________ du prix proposé par les autres soumissionnaires ayant présenté une offre conforme.]"
                ElseIf CodeSection.ToString = "IS 33.1" Then
                    Return "[Une marge de préférence [insérer « sera » ou « ne sera pas » appliquée.]"
                ElseIf CodeSection.ToString = "IS 34.6(a)" Then
                    Return "[Variation par rapport au calendrier de livraison : [insérer « oui » ou « non ». Si oui insérer le facteur d’ajustement dans la Section III, critères d’évaluation et de qualification].]"
                ElseIf CodeSection.ToString = "IS 34.6(b)" Then
                    Return "[Variation par rapport au calendrier de paiement : [insérer « oui »ou « non ». Si oui, insérer le facteur d’ajustement dans la Section III, critères d’évaluation et de qualification].]"
                ElseIf CodeSection.ToString = "IS 34.6(c)" Then
                    Return "[Le coût de remplacement des composants clés, des pièces détachées, et du service : [insérer « oui » ou « non ». Si oui, insérer méthodologie et critères dans la Section III, critères d’évaluation et de qualification.]]"
                ElseIf CodeSection.ToString = "IS 34.6(d)" Then
                    Return "[Disponibilité dans le Pays de l’Acheteur des pièces détachées et du service après-vente pour les équipements offerts dans l’offre : [insérer « oui » ou « non ». Si oui, insérer la méthodologie et les critères dans la Section III, critères d’évaluation et de qualification].]"
                ElseIf CodeSection.ToString = "IS 34.6(e)" Then
                    Return "[Coûts de fonctionnement et d’entretien pendant la durée de vie des équipements : [insérer « oui » ou « non ». Si oui, insérer la méthodologie et les critères d’évaluation dans la Section III, critères d’évaluation et de qualification.]]"
                ElseIf CodeSection.ToString = "IS 34.6(f)" Then
                    Return "[Performance et productivité des équipements offerts [insérer « oui »ou »non ». Si oui, insérer la méthodologie et les critères dans la Section III, critères d’évaluation et de qualification]]"
                ElseIf CodeSection.ToString = "IS 34.6(g)" Then
                    Return "[Insérer tout autre critère. Si autre(s) critère(s), insérer la méthodologie(s) et les critères d’évaluation dans la Section III, critères d’évaluation et de qualification]"
                ElseIf CodeSection.ToString = "IS 42.1(a)" Then
                    Return "[Les quantités peuvent être augmentées d’un pourcentage maximum égal à : [insérer pourcentage].]"
                ElseIf CodeSection.ToString = "IS 42.1(b)" Then
                    Return "[Les quantités peuvent être réduites d’un pourcentage maximum égal à : [insérer pourcentage].]"
                ElseIf CodeSection.ToString = "IS 45.1" Then
                    Return "[Le Soumissionnaire retenu [Insérer « aura » ou « n'aura pas » ] à fournir le Formulaire de divulgation des bénéficiaires effectifs.]"
                End If

                'ElseIf CodeSection.ToString = "Attestion CNPS" Then
                '    Return "[Avez-vous besion d'une attestion CNPS ? [Insérer « OUI » ou « NON » ]]"
                'ElseIf CodeSection.ToString = "Attestation de régularité fiscale" Then
                '    Return "[Avez-vous besion d'une attestation de régularité fiscale ? [Insérer « OUI » ou « NON » ]]"
                'End If

            ElseIf TypeMarche.ToLower = "Travaux".ToLower Then
                ' If TxtMethodeMarche.Text.ToUpper = "AOI" Or TxtMethodeMarche.Text.ToUpper = "AON" Then

                If CodeSection.ToString = "IS 1.2(a)" Then
                        Return "[Insérer la description du système d’achat électronique utilisé par le Maître de l’Ouvrage]"
                    ElseIf CodeSection.ToString = "IS 4.5" Then
                        Return "[Indiquer l’adresse électronique]"
                    ElseIf CodeSection.ToString = "IS 11.1(h)" Then
                        Return ": [Insérer la liste des documents, si nécessaire, autres que ceux déjà mentionnés à l’article 11.1 des IS et qui doivent obligatoirement être joints à l’offre.]"
                    ElseIf CodeSection.ToString = "IS 13.1" Then
                        Return "[Les variantes [insérer « sont » ou « ne sont pas »] autorisées]."
                    ElseIf CodeSection.ToString = "IS 13.2" Then
                        Return "Des délais d’exécution des travaux différents de celui mentionné [insérer « sont » ou « ne sont pas » autorisés.]"
                    ElseIf CodeSection.ToString = "IS 13.4" Then
                        Return "[Insérer les éléments des travaux et les variantes spécifiées]."
                    ElseIf CodeSection.ToString = "IS 14.5" Then
                        Return "[Les prix proposés par le Soumissionnaire seront [insérer « révisables » ou « fermes »]]."
                    ElseIf CodeSection.ToString = "IS 18.3(a)" Then
                        Return "Dans le cas d’un marché à prix ferme, le Montant du marché sera le Montant de l’Offre actualisée de la manière suivante : [insérer la méthode ou indiquer « comme il sera indiqué dans la demande de prorogation de validité des offres »]."
                    ElseIf CodeSection.ToString = "IS 19.3(d)" Then
                        Return "[Insérer les noms des autres types de garanties acceptables ou insérer « Néant » si une garantie d’offre n’est pas requise sous IS 19.1 ou si aucune forme de garantie d’offre autre que celles listées sous IS 19.3(a) à (c) n’est acceptable.]"
                    ElseIf CodeSection.ToString = "IS 19.9" Then
                        Return "Si le Soumissionnaire commet un des actes décrits aux paragraphes (a) ou (b) du présent article, le Maître de l’Ouvrage l’exclura de toute attribution de marché(s) pour une période de [insérer le nombre d’années] ans."
                    ElseIf CodeSection.ToString = "IS 20.3" Then
                        Return "[Insérer l’intitulé et la description des documents nécessaires à titre d’attestation de procuration (ou pouvoir) du signataire de l’offre.]"
                    ElseIf CodeSection.ToString = "IS 25.6" Then
                        Return "[Insérer le nombre des représentants]"
                    ElseIf CodeSection.ToString = "IS 30.3" Then
                        Return "[L’ajustement sera calculé comme étant la [insérer soit « valeur moyenne »] ou [« valeur la plus élevée »] des prix proposés par les autres soumissionnaires ayant présenté une offre conforme pour l’élément en question.]"
                    ElseIf CodeSection.ToString = "IS 33.1" Then
                        Return "[Une marge de préférence [Insérer « sera » ou « ne sera pas »] accordée aux entreprises nationales]"
                    ElseIf CodeSection.ToString = "IS 34.2" Then
                        Return "Le pourcentage maximum des Travaux pouvant être sous-traités par l’Entrepreneur est de [spécifier ___%_ « du montant total du Marché » ]"
                    ElseIf CodeSection.ToString = "IS 47.1" Then
                        Return "Le Soumissionnaire retenu [aura] ou [n’aura pas] à fournir le Formulaire de divulgation des bénéficiaires effectifs."
                    End If

                'ElseIf CodeSection.ToString = "Attestion CNPS" Then
                '    Return "[Avez-vous besion d'une attestion CNPS ? [Insérer « OUI » ou « NON » ]]"
                'ElseIf CodeSection.ToString = "Attestation de régularité fiscale" Then
                '    Return "[Avez-vous besion d'une attestation de régularité fiscale ? [Insérer « OUI » ou « NON » ]]"
                'End If

                'ElseIf CodeSection.ToString = "IS 34.6 (i)" Then
                '    Return "[Insérer le nombre d’années de la vie utile]"
                'ElseIf CodeSection.ToString = "IS 34.6 (ii)" Then
                '    Return "[Insérer le taux d’actualisation]"
                'ElseIf CodeSection.ToString = "IS 34.6 (iii)" Then
                '    Return "[Insérer la méthodologie indiquer comment les coûts seront calculés] "
                'ElseIf CodeSection.ToString = "IS 34.6 (iv)" Then
                '    Return "[Insérer les renseignements que le soumissionnaire devra fournir dans son offres, incluant des prix] "
                'ElseIf CodeSection.ToString = "IS 34.6 (f)" Then
                '    Return "[Insérer la méthode et les facteurs applicables, le cas échéant]"
                'ElseIf CodeSection.ToString = "IS 37.1 (a) (1)" Then
                '    Return "[Insérer la liste des exigences]"
                'ElseIf CodeSection.ToString = "IS 37.1 (a) (2)" Then
                '    Return "[Insérer la/les condition(s) d’utilisation]"
                'ElseIf CodeSection.ToString = "CCAG 26.1" Then
                '    Return "[Décrire les types, fréquences, procédures utilisées pour réaliser ces inspections et ces essais]"
                'ElseIf CodeSection.ToString = "CCAG 26.2" Then
                '    Return "[Insérer les lieux de réalisation des inspections et les essais] "
                'ElseIf CodeSection.ToString = "CCAG 27.1" Then
                '    Return "[Insérer le nombre % des pénalités de retard]"
                'ElseIf CodeSection.ToString = "CCAG 28.3" Then
                '    Return "[Insérer le(s) nombre(s) des périodes de garantie] "
                'ElseIf CodeSection.ToString = "CCAG 28.5, CCAG 28.6" Then
                '    Return "[Insérer le nombre de délai de réparation ou de remplacement (jours)] "
                'ElseIf CodeSection.ToString = "CCAG 33.4" Then
                '    Return "[Insérer le pourcentage approprié,de la diminution du Montant du Marché]"
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        Return ""
    End Function

    Private Sub ChargerLesSection(ByVal NumDoss As String)
        CombSection.Text = ""
        CombSection.Enabled = True
        TxtSaisiTextSection.Text = ""
        GridSection.Rows.Clear()

        query = "Select * from t_dao_section where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            Dim n As Decimal = GridSection.Rows.Add()
            GridSection.Rows.Item(n).Cells("RefSection").Value = rw("RefSection").ToString
            GridSection.Rows.Item(n).Cells("CodeSection").Value = MettreApost(rw("CodeSection").ToString)
            GridSection.Rows.Item(n).Cells("DescriptionSection").Value = MettreApost(rw("Description").ToString)
            GridSection.Rows.Item(n).Cells("LigneModif").Value = "Enregistrer"
        Next
    End Sub

    Private Sub CheckEntite_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEntite.CheckedChanged
        If CheckEntite.Checked = True Then
            TxtMaitreOuvrag.Enabled = True
        Else
            TxtMaitreOuvrag.Enabled = False
        End If
    End Sub

    Private Sub DocumentExige_CheckedChanged(sender As Object, e As EventArgs) Handles DocumentExige.CheckedChanged
        If DocumentExige.Checked = True Then
            NatureDocument.Enabled = True
            NatureDocument.Select()
        Else
            NatureDocument.Enabled = False
            NatureDocument.ResetText()
        End If
    End Sub

    Private Sub GarantiExige_CheckedChanged(sender As Object, e As EventArgs) Handles GarantiExige.CheckedChanged
        If GarantiExige.Checked = True Then
            DelaiGarantie.Enabled = True
            DelaiGarantie.Select()
        Else
            DelaiGarantie.Enabled = False
            DelaiGarantie.Value = 0
        End If
    End Sub

    Private Sub CheckVoiElectro_CheckedChanged(sender As Object, e As EventArgs) Handles CheckVoiElectro.CheckedChanged
        If CheckVoiElectro.Checked = True Then
            PrecedurVoiElectronic.Enabled = True
            PrecedurVoiElectronic.Select()
        Else
            PrecedurVoiElectronic.Enabled = False
            PrecedurVoiElectronic.ResetText()
        End If
    End Sub

    Private Sub Variante_CheckedChanged(sender As Object, e As EventArgs) Handles Variante.CheckedChanged
        If Variante.Checked = True Then
            ListeVariante.Enabled = True
            ListeVariante.Select()
        Else
            ListeVariante.Enabled = False
            ListeVariante.ResetText()
        End If
    End Sub

    Private Sub VisiteduSite_CheckedChanged(sender As Object, e As EventArgs) Handles VisiteduSite.CheckedChanged
        If VisiteduSite.Checked = True Then
            DateVisite.Enabled = True
            DateVisite.Select()
        Else
            DateVisite.Enabled = False
            DateVisite.EditValue = Nothing
        End If
    End Sub
#End Region

#Region "Conformité technique"

    Private Sub LoadPageConformTechnique(ByVal NumDossier As String)
        If Not PageConformTechnique.PageEnabled Then PageConformTechnique.PageEnabled = True
        If IsNothing(CurrentDao) Then
            query = "SELECT * FROM t_dao WHERE NumeroDAO='" & NumDoss & "'"
            Dim dtDao As DataTable = ExcecuteSelectQuery(query)
            If dtDao.Rows.Count = 0 Then
                CurrentDao = Nothing
                Exit Sub
            End If
            CurrentDao = dtDao.Rows(0)
        End If

        If IsNothing(CurrentMarche) Then
            query = "SELECT * FROM t_marche WHERE RefMarche='" & CurrentDao("RefMarche") & "'"
            Dim dtmarche As DataTable = ExcecuteSelectQuery(query)
            If dtmarche.Rows.Count > 0 Then
                CurrentMarche = dtmarche.Rows(0)
            Else
                CurrentMarche = Nothing
                FailMsg("Le marché associé a été supprimé")
                Exit Sub
            End If
        End If


    End Sub
    Private Function SavePageConformTechnique(ByVal NumDossier As String) As Boolean
        Return True
    End Function

    Private Sub CmbNumLot_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbNumLot.SelectedIndexChanged
        If (NumDoss <> "" And CmbNumLot.SelectedIndex <> -1) Then
            query = "select LibelleLot,RefLot from T_LotDAO where NumeroDAO='" & NumDoss & "' and CodeLot='" & CmbNumLot.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtLibelleLot.Text = MettreApost(rw("LibelleLot").ToString)
            Next
            chargerGroupConform()
            ChargerCritereGrid()
        End If
    End Sub
    Private Sub chargerGroupConform()
        CmbGroupConform.Properties.Items.Clear()
        query = "select LibelleConformTech from T_DAO_ConformTech where NumeroDAO='" & NumDoss & "' and RefConformMere='0' and CodeLot in ('" & CmbNumLot.Text & "','x') order by LibelleConformTech"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbGroupConform.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next
    End Sub
    Private Sub ChargerCritereGrid()

        Dim dt As DataTable = GridCritere.DataSource
        dt.Rows.Clear()
        Dim cptr1 As Decimal = 0
        Dim cptrG As Decimal = 0

        query = "select LibelleConformTech,RefConformTech from T_DAO_ConformTech where NumeroDAO='" & NumDoss & "' and CodeLot in ('" & CmbNumLot.Text & "','x') and RefConformMere='0'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            cptr1 = 0
            cptrG += 1

            Dim drS = dt.NewRow()

            drS(0) = "G"
            drS(1) = "C" & cptrG.ToString
            drS(2) = MettreApost(rw(0).ToString)
            drS(3) = ""

            dt.Rows.Add(drS)

            query = "select LibelleConformTech,Eliminatoire from T_DAO_ConformTech where NumeroDAO='" & NumDoss & "' and RefConformMere='" & rw(1).ToString & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw1 As DataRow In dt1.Rows
                cptr1 += 1
                Dim drS2 = dt.NewRow()

                drS2(0) = IIf(CDec(cptr1 / 2) = CDec(cptr1 \ 2), "x", "").ToString
                drS2(1) = ""
                drS2(2) = "C" & cptrG.ToString & "." & cptr1.ToString & " : " & MettreApost(rw1(0).ToString)
                drS2(3) = rw1(1).ToString

                dt.Rows.Add(drS2)
            Next
        Next

        ColorRowGrid(ViewCritere, "[Code]='x'", Color.LightGray, "Tahoma", 8, FontStyle.Regular, Color.Black)
        ColorRowGridAnal(ViewCritere, "[Code]='G'", Color.Navy, "Tahoma", 9, FontStyle.Bold, Color.White, True)
    End Sub
    Private Sub BtAjoutCritere_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjoutCritere.Click

        If (TxtCritere.Properties.ReadOnly = False Or TxtGroupConform.Properties.ReadOnly = False) Then
            If (CmbNumLot.Text <> "" And (TxtCritere.Text <> "" Or TxtGroupConform.Text <> "")) Then

                Dim DatSet = New DataSet
                query = "select * from T_DAO_ConformTech"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "T_DAO_ConformTech")
                Dim DatTable = DatSet.Tables("T_DAO_ConformTech")
                Dim DatRow = DatSet.Tables("T_DAO_ConformTech").NewRow()

                DatRow("NumeroDAO") = NumDoss
                DatRow("CodeLot") = IIf(ChkTousLots.Checked = True, "x", CmbNumLot.Text).ToString
                DatRow("LibelleConformTech") = EnleverApost(IIf(TxtCritere.Properties.ReadOnly = False, TxtCritere.Text, TxtGroupConform.Text).ToString)
                DatRow("Eliminatoire") = IIf(ChkCritereEliminatoire.Checked = True, "OUI", "NON").ToString
                DatRow("RefConformMere") = IIf(TxtCritere.Properties.ReadOnly = False, RefGroupConf.Text, "0").ToString

                DatSet.Tables("T_DAO_ConformTech").Rows.Add(DatRow)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "T_DAO_ConformTech")
                DatSet.Clear()
                BDQUIT(sqlconn)

                ChargerCritereGrid()
                TxtCritere.Text = ""
                TxtGroupConform.Text = ""
                CmbGroupConform.Enabled = True
                chargerGroupConform()
                If (CmbGroupConform.Text = "") Then
                    TxtCritere.Properties.ReadOnly = True
                Else
                    TxtCritere.Properties.ReadOnly = False
                    TxtCritere.Focus()
                End If
                TxtGroupConform.Properties.ReadOnly = True
                ChkCritereEliminatoire.Checked = True
                ChkCritereEliminatoire.Enabled = True
                ChkTousLots.Checked = False
                ChkTousLots.Enabled = True

            End If
        End If

    End Sub
    Private Sub CmbGroupConform_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbGroupConform.SelectedValueChanged
        query = "select RefConformTech from T_DAO_ConformTech where NumeroDAO='" & NumDoss & "' and RefConformMere='0' and CodeLot in ('" & CmbNumLot.Text & "','x') and LibelleConformTech='" & EnleverApost(CmbGroupConform.Text) & "'"
        RefGroupConf.Text = ""
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            RefGroupConf.Text = rw(0).ToString
        Next

        If (CmbGroupConform.Text <> "") Then
            TxtGroupConform.Text = CmbGroupConform.Text
            TxtGroupConform.Properties.ReadOnly = True
            TxtCritere.Properties.ReadOnly = False
            TxtCritere.Focus()
            ChkCritereEliminatoire.Enabled = True
            ChkCritereEliminatoire.Checked = True
            ChkTousLots.Enabled = True
            ChkTousLots.Checked = False
        End If

    End Sub

    Private Sub BtAjoutGroupConform_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtAjoutGroupConform.Click

        CmbGroupConform.Enabled = False
        CmbGroupConform.Text = ""
        TxtGroupConform.Properties.ReadOnly = False
        TxtGroupConform.Focus()
        TxtGroupConform.Text = ""
        TxtCritere.Properties.ReadOnly = True
        TxtCritere.Text = ""
        RefGroupConf.Text = ""
        ChkCritereEliminatoire.Enabled = False
        ChkCritereEliminatoire.Checked = False
        ChkTousLots.Checked = False
        ChkTousLots.Enabled = True

    End Sub



#End Region

#Region "Spécifications techniques"

    Private Sub InitSpecTechnq()

        Dim dt As DataTable = GridSpecifTech.DataSource
        dt.Rows.Clear()
        TxtUniteBien.Text = ""
        NumQteBien.Enabled = False
        BtCategBien.Enabled = False
        TxtLibCategBien.Text = ""

        ViderSaisieBien()
        ViderSaisieCaract()
        LockSaisieBien(False)

        'LoadSpecTech()
        TxtLibCategBien.ResetText()
        cmbLotSpecTech.ResetText()
        TxtLibLotSpecTech.ResetText()
        CmbSousLotSpecTech.ResetText()
        TxtSousLotSpecTech.ResetText()
        cmbLotSpecTech.Focus()
        'CodeSpecTechSup.Clear()
        modifSpecTech = False
    End Sub

    Private Sub cmbLotSpecTech_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLotSpecTech.SelectedIndexChanged
        TxtLibLotSpecTech.ResetText()
        CmbSousLotSpecTech.Properties.Items.Clear()
        CmbSousLotSpecTech.ResetText()
        TxtSousLotSpecTech.ResetText()

        If cmbLotSpecTech.SelectedIndex <> -1 Then
            If (NumDoss <> "") Then
                query = "select LibelleLot,SousLot,RefLot from T_LotDAO where NumeroDAO='" & NumDoss & "' and CodeLot='" & cmbLotSpecTech.Text & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                Dim RefLot As Decimal = 0
                For Each rw As DataRow In dt.Rows
                    TxtLibLotSpecTech.Text = MettreApost(rw("LibelleLot").ToString)
                    RefLot = rw("RefLot")
                    If Val(GetSousLot(cmbLotSpecTech.Text, NumDoss)(0)) = 0 Then
                        CmbSousLotSpecTech.Enabled = False
                    Else
                        CmbSousLotSpecTech.Enabled = True
                    End If
                Next

                If (CmbSousLotSpecTech.Enabled = False) Then
                    If AfficherDossier = False Then BtCategBien.Enabled = True
                    MajCmbUnite()
                    'Chargement des caracteristiq
                    ChargerLesSpecificationTechnique(cmbLotSpecTech.Text, "")
                    ChargerListeConnexe(cmbLotSpecTech.Text, "")
                Else
                    If AfficherDossier = False Then BtCategBien.Enabled = False
                    LesSousLots(RefLot, CmbSousLotSpecTech)
                End If
                ViderSaisieBien()
                TxtLibCategBien.ResetText()
            End If
        End If
    End Sub
    Private Sub CmbSousLotSpecTech_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSousLotSpecTech.SelectedValueChanged
        If CmbSousLotSpecTech.SelectedIndex = -1 Then
            TxtSousLotSpecTech.ResetText()
            If AfficherDossier = False Then BtCategBien.Enabled = False
            Exit Sub
        Else
            query = "select LibelleSousLot from T_LotDAO_SousLot where CodeSousLot='" & CmbSousLotSpecTech.Text & "' and NumeroDAO='" & NumDoss & "' AND RefLot='" & GetRefLot(Val(cmbLotSpecTech.Text), NumDoss) & "' LIMIT 1"
            TxtSousLotSpecTech.Text = MettreApost(ExecuteScallar(query))
            If AfficherDossier = False Then BtCategBien.Enabled = True
        End If
        ViderSaisieBien()
        TxtLibCategBien.ResetText()

        ChargerLesSpecificationTechnique(cmbLotSpecTech.Text, CmbSousLotSpecTech.Text)
        ChargerListeConnexe(cmbLotSpecTech.Text, CmbSousLotSpecTech.Text)
        MajCmbUnite()

    End Sub

    Private Sub BtCategBien_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtCategBien.Click
        Dim NewCat As New DiagChoixTypeFourniture
        Dim drRetour As DataRow = Nothing
        If NewCat.ShowDialog() = DialogResult.OK Then
            drRetour = NewCat.drRetour
        End If

        If Not IsNothing(drRetour) Then

            Dim nbFournit As Integer = Val(ExecuteScallar("select Count(*) from T_SpecTechFourniture where NumeroDAO='" & NumDoss & "' and CodeLot='" & cmbLotSpecTech.Text & "' and CodeSousLot='" & CmbSousLotSpecTech.Text & "'"))

            'For i = 0 To SaveDonnee.Nodes.Count - 1
            '    If SaveDonnee.Nodes(i).GetValue("NumLotSav") = cmbLotSpecTech.Text And SaveDonnee.Nodes(i).GetValue("NumSousLotSav") = CmbSousLotSpecTech.Text Then
            '        nbFournit += 1
            '    End If
            'Next
            nbFournit += 1

            'ViderSaisieBien()
            If modifSpecTech = False Then
                LockSaisieBien(True)
                TxtRefArticle.Text = "E" & nbFournit.ToString
            Else
                txtCodeCateg.Text = drRetour("IdItem") & "-" & drRetour("Type")
            End If
            ' TypeCategorieSpecTech = drRetour("IdItem") & "-" & drRetour("Type")
            CodeCategorie.Text = drRetour("IdItem") & "-" & drRetour("Type")
            TxtLibCategBien.Text = drRetour("Libellé").ToString().Replace("     - ", "")
            TxtRefArticle.Select()

            If CmbSousLotSpecTech.Text.Trim <> "" Then
                CmbSousLotSpecTech.Enabled = True
            End If
            TxtLibelleBien.ResetText()
            CmbUniteBien.ResetText()
            TxtLieuLivraison.ResetText()
            NumQteBien.Value = 1

            ViderSaisieCaract()
        Else
            If modifSpecTech = False Then
                LockSaisieBien(False)
                ViderSaisieBien()
                TxtLibCategBien.ResetText()
            End If
        End If
    End Sub

    Private Sub LockSaisieBien(value As Boolean)
        TxtRefArticle.Enabled = value
        TxtLibelleBien.Enabled = value
        NumQteBien.Enabled = value
        CmbUniteBien.Enabled = value
        TxtLieuLivraison.Enabled = value
        TxtLibelleCaract.Enabled = value
        TxtValeurCaract.Enabled = value
        BtEnregBien.Enabled = value
        btRetourBien.Enabled = value
    End Sub

    Private Sub UnlockModBien()
        TxtRefArticle.Enabled = False
        BtCategBien.Enabled = False

        TxtLibelleBien.Enabled = True
        NumQteBien.Enabled = True
        CmbUniteBien.Enabled = True
        TxtLieuLivraison.Enabled = True
        TxtLibelleCaract.Enabled = False
        TxtValeurCaract.Enabled = False
        BtEnregBien.Enabled = True
        btRetourBien.Enabled = True
        cmbLotSpecTech.Enabled = False
        CmbSousLotSpecTech.Enabled = False
    End Sub

    Private Sub UnlockModCaract()
        BtCategBien.Enabled = False
        TxtRefArticle.Enabled = False
        cmbLotSpecTech.Enabled = False
        CmbSousLotSpecTech.Enabled = False
        TxtLibelleBien.Enabled = False
        NumQteBien.Enabled = False
        CmbUniteBien.Enabled = False
        TxtLieuLivraison.Enabled = False
        TxtLibelleCaract.Enabled = True
        TxtValeurCaract.Enabled = True
        BtEnregBien.Enabled = True
        btRetourBien.Enabled = True
    End Sub

    Private Sub ViderSaisieBien()
        TxtRefArticle.ResetText()
        TxtLibelleBien.ResetText()
        NumQteBien.Value = 1
        CmbUniteBien.ResetText()
        TxtLieuLivraison.ResetText()
        RefSpecificationModif.ResetText()
        TxtLibelleserviceconnexe.ResetText()
        ViderSaisieCaract()
    End Sub

    'Private Sub actualiserListe(ByVal NumLot As String, ByVal NumSousLot As String)
    '    ListeSpecTech.Nodes.Clear()
    '    ListeSpecTech.BeginUnboundLoad()
    '    For i = 0 To SaveDonnee.Nodes.Count - 1
    '        If SaveDonnee.Nodes(i).GetValue("NumLotSav") = NumLot And SaveDonnee.Nodes(i).GetValue("NumSousLotSav") = NumSousLot Then
    '            Dim parentForRootNodes As TreeListNode = Nothing
    '            Dim rootNode As TreeListNode = ListeSpecTech.AppendNode(New Object() {SaveDonnee.Nodes(i).GetValue("IdentifiantSav"), SaveDonnee.Nodes(i).GetValue("CodeSav"), SaveDonnee.Nodes(i).GetValue("LibelleSav"), SaveDonnee.Nodes(i).GetValue("QuantiteSav"), SaveDonnee.Nodes(i).GetValue("LieuLivreSav"), SaveDonnee.Nodes(i).GetValue("CodeCategSav"), SaveDonnee.Nodes(i).GetValue("NumLotSav"), SaveDonnee.Nodes(i).GetValue("NumSousLotSav"), SaveDonnee.Nodes(i).GetValue("EditSav")}, parentForRootNodes)
    '            For j = 0 To SaveDonnee.Nodes(i).Nodes.Count - 1
    '                ListeSpecTech.AppendNode(New Object() {SaveDonnee.Nodes(i).Nodes(j).GetValue("IdentifiantSav"), SaveDonnee.Nodes(i).Nodes(j).GetValue("CodeSav"), SaveDonnee.Nodes(i).Nodes(j).GetValue("LibelleSav"), SaveDonnee.Nodes(i).Nodes(j).GetValue("QuantiteSav"), SaveDonnee.Nodes(i).Nodes(j).GetValue("LieuLivreSav"), SaveDonnee.Nodes(i).Nodes(j).GetValue("CodeCategSav"), SaveDonnee.Nodes(i).Nodes(j).GetValue("NumLotSav"), SaveDonnee.Nodes(i).Nodes(j).GetValue("NumSousLotSav"), SaveDonnee.Nodes(i).Nodes(j).GetValue("EditSav"), False}, rootNode)
    '            Next
    '        End If
    '    Next
    '    ListeSpecTech.EndUnboundLoad()
    'End Sub

    Private Sub ChargerLesSpecificationTechnique(ByVal CodeLot As String, ByVal CodeSousLot As String)
        Try
            ListeSpecTech.Nodes.Clear()
            ListeSpecTech.BeginUnboundLoad()
            Dim dt As DataTable = ExcecuteSelectQuery("select * from t_spectechfourniture where NumeroDAO='" & NumDoss & "' and CodeLot='" & CodeLot & "' and CodeSousLot='" & CodeSousLot & "'")
            For Each rw In dt.Rows
                Dim parentForRootNodes As TreeListNode = Nothing
                Dim rootNode As TreeListNode = ListeSpecTech.AppendNode(New Object() {rw("RefSpecFournit").ToString, MettreApost(rw("CodeFournit").ToString), MettreApost(rw("DescripFournit").ToString), rw("QteFournit").ToString & " " & rw("UniteFournit").ToString, MettreApost(rw("LieuLivraison").ToString), rw("CodeCategorie").ToString, rw("CodeLot").ToString, rw("CodeSousLot").ToString}, parentForRootNodes)
                'Chargement des caracteristiques
                Dim dt1 As DataTable = ExcecuteSelectQuery("select * from t_spectechcaract where RefSpecFournit='" & rw("RefSpecFournit") & "'")
                For Each rw1 In dt1.Rows
                    ListeSpecTech.AppendNode(New Object() {rw1("RefSpecCaract").ToString, "", "   - " & MettreApost(rw1("LibelleCaract").ToString) & "  :  " & MettreApost(rw1("ValeurCaract").ToString), "", "", "", "", ""}, rootNode)
                Next
            Next
            ListeSpecTech.EndUnboundLoad()
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub BtEnregBien_Click(sender As Object, e As EventArgs) Handles BtEnregBien.Click
        If modifSpecTech = False Then
            If cmbLotSpecTech.IsRequiredControl("Veuillez choisir un lot.") Then
                cmbLotSpecTech.Select()
                Exit Sub
            End If

            If CmbSousLotSpecTech.Enabled Then
                If CmbSousLotSpecTech.IsRequiredControl("Veuillez choisir un sous lot.") Then
                    CmbSousLotSpecTech.Select()
                    Exit Sub
                End If
            End If
            If TxtLibCategBien.IsRequiredControl("Veuillez choisir une catégorie.") Then
                TxtLibCategBien.Select()
                Exit Sub
            End If
            If TxtRefArticle.IsRequiredControl("Veuillez entrer le code du bien.") Then
                TxtRefArticle.Select()
                Exit Sub
            End If
            If TxtLibelleBien.IsRequiredControl("Veuillez entrer le libellé du bien.") Then
                TxtLibelleBien.Select()
                Exit Sub
            End If
            If NumQteBien.Value <= 0 Then
                SuccesMsg("Veuillez entrer la quantité.")
                NumQteBien.Select()
                Exit Sub
            End If
            If CmbUniteBien.IsRequiredControl("Veuillez choisir une unité.") Then
                CmbUniteBien.Select()
                Exit Sub
            End If
            If TxtLieuLivraison.IsRequiredControl("Veuillez entrer le lieu de livraison.") Then
                TxtLieuLivraison.Select()
                Exit Sub
            End If
            If TxtLibelleCaract.IsRequiredControl("Veuillez saisir la caractéristique.") Then
                TxtLibelleCaract.Select()
                Exit Sub
            End If
            If TxtValeurCaract.IsRequiredControl("Veuillez saisir la valeur de la caractéristique demandée.") Then
                TxtValeurCaract.Select()
                Exit Sub
            End If

            'Nouveau Enregistrement
            If TxtRefArticle.Enabled = True Then
                'Verification de l'exitance de la reference de l'article
                If Val(ExecuteScallar("select count(*) From t_spectechfourniture where NumeroDAO='" & NumDoss & "' and CodeLot='" & cmbLotSpecTech.Text & "' and CodeSousLot='" & CmbSousLotSpecTech.Text & "' and CodeFournit='" & EnleverApost(TxtRefArticle.Text) & "'")) > 0 Then
                    SuccesMsg("La reference de l'article existe déjà")
                    TxtRefArticle.Select()
                    Exit Sub
                End If

                'add dans t_spectechfourniture
                query = "INSERT INTO T_SpecTechFourniture(RefSpecFournit,CodeCategorie,NumeroDAO,CodeLot,CodeSousLot,CodeFournit,DescripFournit,QteFournit,UniteFournit,LieuLivraison) VALUES(NULL,'" & EnleverApost(CodeCategorie.Text) & "', '" & NumDoss & "','" & cmbLotSpecTech.Text & "','" & EnleverApost(CmbSousLotSpecTech.Text) & "', '" & EnleverApost(TxtRefArticle.Text) & "', '" & EnleverApost(TxtLibelleBien.Text) & "', '" & NumQteBien.Text.Replace(" ", "") & "', '" & EnleverApost(CmbUniteBien.Text) & "','" & EnleverApost(TxtLieuLivraison.Text) & "')"
                ExecuteNonQuery(query)
                Dim IdBien As Decimal = Val(ExecuteScallar("SELECT MAX(RefSpecFournit) FROM T_SpecTechFourniture WHERE CodeLot='" & cmbLotSpecTech.Text & "' AND CodeSousLot='" & CmbSousLotSpecTech.Text & "' AND NumeroDAO='" & NumDoss & "'"))

                'add dans t_spectechcaract
                query = "INSERT INTO t_spectechcaract(RefSpecCaract,RefSpecFournit,LibelleCaract,ValeurCaract)"
                query &= " VALUES(NULL,'" & IdBien & "','" & EnleverApost(TxtLibelleCaract.Text) & "','" & EnleverApost(TxtValeurCaract.Text) & "')"
                ExecuteNonQuery(query)

                TxtRefArticle.Enabled = False
                'Ajout caracteristique après enregistrement de la ligne principal
            ElseIf TxtRefArticle.Enabled = False Then
                Dim RefSpecFournit As Decimal = ExecuteScallar("select RefSpecFournit From t_spectechfourniture where NumeroDAO='" & NumDoss & "' and CodeLot='" & cmbLotSpecTech.Text & "' and CodeSousLot='" & CmbSousLotSpecTech.Text & "' and CodeFournit='" & EnleverApost(TxtRefArticle.Text) & "'")

                query = "UPDATE T_SpecTechFourniture SET CodeFournit='" & EnleverApost(TxtRefArticle.Text) & "', DescripFournit='" & EnleverApost(TxtLibelleBien.Text) & "',"
                query &= "QteFournit='" & NumQteBien.Text.Replace(" ", "") & "', UniteFournit='" & EnleverApost(CmbUniteBien.Text) & "', LieuLivraison='" & EnleverApost(TxtLieuLivraison.Text) & "' WHERE RefSpecFournit='" & RefSpecFournit & "'"
                ExecuteNonQuery(query)

                query = "INSERT INTO t_spectechcaract(RefSpecCaract,RefSpecFournit,LibelleCaract,ValeurCaract)"
                query &= " VALUES(NULL,'" & RefSpecFournit & "','" & EnleverApost(TxtLibelleCaract.Text) & "','" & EnleverApost(TxtValeurCaract.Text) & "')"
                ExecuteNonQuery(query)
            End If

            ' ChargerLesSpecificationTechnique(cmbLotSpecTech.Text, CmbSousLotSpecTech.Text)
        Else
            'Modification d'une ligne principal
            If TxtLibelleCaract.Enabled = False Then
                If cmbLotSpecTech.IsRequiredControl("Veuillez choisir un lot.") Then
                    cmbLotSpecTech.Select()
                    Exit Sub
                End If

                If CmbSousLotSpecTech.Enabled Then
                    If CmbSousLotSpecTech.IsRequiredControl("Veuillez choisir un sous lot.") Then
                        CmbSousLotSpecTech.Select()
                        Exit Sub
                    End If
                End If

                If TxtLibCategBien.IsRequiredControl("Veuillez choisir une catégorie.") Then
                    TxtLibCategBien.Select()
                    Exit Sub
                End If
                If TxtRefArticle.IsRequiredControl("Veuillez entrer le code du bien.") Then
                    TxtRefArticle.Select()
                    Exit Sub
                End If
                If TxtLibelleBien.IsRequiredControl("Veuillez entrer le libellé du bien.") Then
                    TxtLibelleBien.Select()
                    Exit Sub
                End If
                If NumQteBien.Value <= 0 Then
                    SuccesMsg("Veuillez entrer la quantité.")
                    NumQteBien.Select()
                    Exit Sub
                End If
                If CmbUniteBien.IsRequiredControl("Veuillez choisir une unité.") Then
                    CmbUniteBien.Select()
                    Exit Sub
                End If
                If TxtLieuLivraison.IsRequiredControl("Veuillez entrer le lieu de livraison.") Then
                    TxtLieuLivraison.Select()
                    Exit Sub
                End If

                query = "UPDATE T_SpecTechFourniture SET CodeFournit='" & EnleverApost(TxtRefArticle.Text) & "', DescripFournit='" & EnleverApost(TxtLibelleBien.Text) & "', QteFournit='" & NumQteBien.Text.Replace(" ", "") & "', UniteFournit='" & EnleverApost(CmbUniteBien.Text) & "', LieuLivraison='" & EnleverApost(TxtLieuLivraison.Text) & "' WHERE RefSpecFournit='" & RefSpecificationModif.Text & "'"
                ExecuteNonQuery(query)
            Else
                'Modification d'un critères
                If TxtLibelleCaract.IsRequiredControl("Veuillez saisir la caractéristique.") Then
                    TxtLibelleCaract.Select()
                    Exit Sub
                End If
                If TxtValeurCaract.IsRequiredControl("Veuillez saisir la valeur de la caractéristique demandée.") Then
                    TxtValeurCaract.Select()
                    Exit Sub
                End If

                ExecuteNonQuery("Update t_spectechcaract set LibelleCaract='" & EnleverApost(TxtLibelleCaract.Text) & "', ValeurCaract='" & EnleverApost(TxtValeurCaract.Text) & "' where RefSpecCaract='" & RefSpecificationModif.Text & "'")
            End If

            txtCodeCateg.Text = ""
            TxtLibCategBien.Text = ""
            RefSpecificationModif.Text = ""
            ViderSaisieBien()
            cmbLotSpecTech.Enabled = True

            If CmbSousLotSpecTech.Text.Trim <> "" Then CmbSousLotSpecTech.Enabled = True
            BtCategBien.Enabled = True
            LockSaisieBien(False)
        End If
        modifSpecTech = False
        ViderSaisieCaract()
        ChargerLesSpecificationTechnique(cmbLotSpecTech.Text, CmbSousLotSpecTech.Text)

        '    TxtRefArticle.Enabled = False
        '    For j = 0 To SaveDonnee.Nodes(i).Nodes.Count - 1
        '        Dim Libelle As String = SaveDonnee.Nodes(i).Nodes(j).GetValue("LibelleSav").ToString().Replace("   - ", "")
        '        Dim LibelleCaract As String = Split(Libelle, " : ")(0)
        '        Dim ValueurCaract As String = Split(Libelle, " : ")(1)
        '        If SaveDonnee.Nodes(i).Nodes(j).Item("IdentifiantSav").ToString = "##" Then
        '            If SaveDonnee.Nodes(i).Nodes(j).Item("IdentifiantSav").ToString = "##" Then
        '                query = "INSERT INTO t_spectechcaract(RefSpecCaract,RefSpecFournit,LibelleCaract,ValeurCaract)"
        '                query &= " VALUES(NULL,'" & IdBien & "','" & EnleverApost(LibelleCaract) & "','" & EnleverApost(ValueurCaract) & "')"
        '                ExecuteNonQuery(query)
        '                Dim Id As String = ExecuteScallar("SELECT MAX(RefSpecCaract) FROM t_spectechcaract WHERE RefSpecFournit='" & IdBien & "'")
        '                SaveDonnee.Nodes(i).Nodes(j).SetValue("IdentifiantSav", Id)
        '            End If
        '        Else
        '            query = "UPDATE t_spectechcaract SET LibelleCaract='" & EnleverApost(LibelleCaract) & "', ValeurCaract='" & EnleverApost(ValueurCaract) & "'"
        '            query &= " WHERE RefSpecCaract='" & SaveDonnee.Nodes(i).Nodes(j).GetValue("IdentifiantSav") & "'"
        '            ExecuteNonQuery(query)
        '        End If
        '    Next
        '    Next




        '    Dim dtSpecTech As DataTable = GridSpecifTech.DataSource
        '    Dim drS As DataRow = dtSpecTech.NewRow

        '    Dim NumIndex As Integer = IsSavedItemInGridView(TxtRefArticle.Text.Trim(), ViewSpecTechn, "Code")
        '    If NumIndex = -1 Then
        '        drS("Id") = "##"
        '        drS("Code") = TxtRefArticle.Text.Trim()
        '        drS("Libellé") = TxtLibelleBien.Text.Trim()
        '        drS("Quantité") = NumQteBien.Value & " " & CmbUniteBien.Text
        '        drS("Lieu de livraison") = TxtLieuLivraison.Text.Trim()
        '        drS("CodeCateg") = TypeCategorieSpecTech
        '        drS("NumLot") = cmbLotSpecTech.Text
        '        drS("NumSousLot") = CmbSousLotSpecTech.Text
        '        drS("Edit") = False
        '        dtSpecTech.Rows.Add(drS)

        '        drS = dtSpecTech.NewRow()
        '        drS("Id") = "##"
        '        drS("Code") = ""
        '        drS("Libellé") = "   - " & TxtLibelleCaract.Text.Trim() & "  :  " & TxtValeurCaract.Text.Trim()
        '        drS("Quantité") = ""
        '        drS("Lieu de livraison") = ""
        '        drS("CodeCateg") = ""
        '        drS("NumLot") = cmbLotSpecTech.Text
        '        drS("NumSousLot") = CmbSousLotSpecTech.Text
        '        drS("Edit") = False
        '        dtSpecTech.Rows.Add(drS)
        '    Else
        '        drS("Id") = "##"
        '        drS("Code") = ""
        '        drS("Libellé") = "   - " & TxtLibelleCaract.Text.Trim() & "  :  " & TxtValeurCaract.Text.Trim()
        '        drS("Quantité") = ""
        '        drS("Lieu de livraison") = ""
        '        drS("CodeCateg") = ""
        '        drS("NumLot") = cmbLotSpecTech.Text
        '        drS("NumSousLot") = CmbSousLotSpecTech.Text
        '        drS("Edit") = False
        '        dtSpecTech.Rows.InsertAt(drS, NumIndex + 1)
        '    End If
        '    ViewSpecTechn.OptionsView.ColumnAutoWidth = True
        '    ColorRowGrid(ViewSpecTechn, "[Quantité]<>''", Color.LightBlue, "Tahoma", 8, FontStyle.Bold, Color.Black)

        '    For i = 0 To SaveDonnee.Nodes.Count - 1
        '        If SaveDonnee.Nodes(i).Item("CodeSav") = TxtRefArticle.Text.Trim() And SaveDonnee.Nodes(i).Item("LibelleSav").ToString.ToLower <> TxtLibelleBien.Text.Trim().ToLower And SaveDonnee.Nodes(i).Item("NumLotSav") = cmbLotSpecTech.Text And SaveDonnee.Nodes(i).Item("NumSousLotSav") = CmbSousLotSpecTech.Text Then
        '            SuccesMsg("Ce code de bien existe déjà")
        '            Exit Sub
        '        End If
        '        If SaveDonnee.Nodes(i).Item("CodeSav") = TxtRefArticle.Text.Trim() Then
        '            If SaveDonnee.Nodes(i).Item("LibelleSav").ToString.ToLower = TxtLibelleBien.Text.Trim().ToLower Then
        '                Dim CatNode As TreeListNode = SaveDonnee.Nodes(i)
        '                SaveDonnee.AppendNode(New Object() {"##", "", "     - " & TxtLibelleCaract.Text.Trim() & "  :  " & TxtValeurCaract.Text.Trim(), "", "", "", cmbLotSpecTech.Text, CmbSousLotSpecTech.Text, False}, CatNode)
        '                ViderSaisieCaract()
        '                TxtLibelleCaract.Focus()
        '                actualiserListe(cmbLotSpecTech.Text, CmbSousLotSpecTech.Text)
        '                SuccesMsg("ok1")

        '                Exit Sub
        '            End If
        '        End If
        '    Next

        '    Dim parentForRootNodes As TreeListNode = Nothing
        '    Dim rootNode As TreeListNode = SaveDonnee.AppendNode(New Object() {"##", TxtRefArticle.Text.Trim(), TxtLibelleBien.Text.Trim(), NumQteBien.Value & " " & CmbUniteBien.Text, TxtLieuLivraison.Text.Trim(), TypeCategorieSpecTech, cmbLotSpecTech.Text, CmbSousLotSpecTech.Text, False}, parentForRootNodes)
        '    SaveDonnee.AppendNode(New Object() {"##", "", "     - " & TxtLibelleCaract.Text.Trim() & "  :  " & TxtValeurCaract.Text.Trim(), "", "", "", cmbLotSpecTech.Text, CmbSousLotSpecTech.Text, False}, rootNode)
        '    SuccesMsg("ok2")
        '    actualiserListe(cmbLotSpecTech.Text, CmbSousLotSpecTech.Text)
        '    ViderSaisieCaract()
        '    TxtLibelleCaract.Focus()
        '    Else

        '    If TxtLibelleCaract.Enabled = False Then
        '        If cmbLotSpecTech.IsRequiredControl("Veuillez choisir un lot.") Then
        '            cmbLotSpecTech.Select()
        '            Exit Sub
        '        End If
        '        If CmbSousLotSpecTech.Enabled Then
        '            If CmbSousLotSpecTech.IsRequiredControl("Veuillez choisir un sous lot.") Then
        '                CmbSousLotSpecTech.Select()
        '                Exit Sub
        '            End If
        '        End If

        '        If TxtLibCategBien.IsRequiredControl("Veuillez choisir une catégorie.") Then
        '            TxtLibCategBien.Select()
        '            Exit Sub
        '        End If
        '        If TxtRefArticle.IsRequiredControl("Veuillez entrer le code du bien.") Then
        '            TxtRefArticle.Select()
        '            Exit Sub
        '        End If
        '        If TxtLibelleBien.IsRequiredControl("Veuillez entrer le libellé du bien.") Then
        '            TxtLibelleBien.Select()
        '            Exit Sub
        '        End If
        '        If NumQteBien.Value <= 0 Then
        '            SuccesMsg("Veuillez entrer la quantité.")
        '            NumQteBien.Select()
        '            Exit Sub
        '        End If
        '        If CmbUniteBien.IsRequiredControl("Veuillez choisir une unité.") Then
        '            CmbUniteBien.Select()
        '            Exit Sub
        '        End If
        '        If TxtLieuLivraison.IsRequiredControl("Veuillez entrer le lieu de livraison.") Then
        '            TxtLieuLivraison.Select()
        '            Exit Sub
        '        End If
        '        For i = 0 To SaveDonnee.Nodes.Count - 1
        '            If SaveDonnee.Nodes(i).GetValue("IdentifiantSav") = NodeModSpec.GetValue("Identifiant") And SaveDonnee.Nodes(i).GetValue("CodeSav") = NodeModSpec.GetValue("Code") And SaveDonnee.Nodes(i).GetValue("LibelleSav") = NodeModSpec.GetValue("Libelle") And SaveDonnee.Nodes(i).GetValue("NumLotSav") = NodeModSpec.GetValue("NumLot") And SaveDonnee.Nodes(i).GetValue("NumSousLotSav") = NodeModSpec.GetValue("NumSousLot") Then
        '                SaveDonnee.Nodes(i).SetValue("CodeSav", TxtRefArticle.Text.Trim())
        '                SaveDonnee.Nodes(i).SetValue("LibelleSav", TxtLibelleBien.Text.Trim())
        '                SaveDonnee.Nodes(i).SetValue("QuantiteSav", NumQteBien.Value & " " & CmbUniteBien.Text)
        '                SaveDonnee.Nodes(i).SetValue("LieuLivreSav", TxtLieuLivraison.Text.Trim())
        '                SaveDonnee.Nodes(i).SetValue("CodeCategSav", txtCodeCateg.Text)
        '                Exit For
        '            End If
        '        Next
        '        NodeModSpec.SetValue("Code", TxtRefArticle.Text.Trim())
        '        NodeModSpec.SetValue("Libelle", TxtLibelleBien.Text.Trim())
        '        NodeModSpec.SetValue("Quantite", NumQteBien.Value & " " & CmbUniteBien.Text)
        '        NodeModSpec.SetValue("LieuLivre", TxtLieuLivraison.Text.Trim())
        '        NodeModSpec.SetValue("CodeCateg", txtCodeCateg.Text)
        '    Else
        '        If TxtLibelleCaract.IsRequiredControl("Veuillez saisir la caractéristique.") Then
        '            TxtLibelleCaract.Select()
        '            Exit Sub
        '        End If
        '        If TxtValeurCaract.IsRequiredControl("Veuillez saisir la valeur de la caractéristique demandée.") Then
        '            TxtValeurCaract.Select()
        '            Exit Sub
        '        End If
        '        For i = 0 To SaveDonnee.Nodes.Count - 1
        '            If SaveDonnee.Nodes(i).GetValue("IdentifiantSav") = NodeModSpec.ParentNode.GetValue("Identifiant") And SaveDonnee.Nodes(i).GetValue("CodeSav") = NodeModSpec.ParentNode.GetValue("Code") And SaveDonnee.Nodes(i).GetValue("LibelleSav") = NodeModSpec.ParentNode.GetValue("Libelle") And SaveDonnee.Nodes(i).GetValue("NumLotSav") = NodeModSpec.ParentNode.GetValue("NumLot") And SaveDonnee.Nodes(i).GetValue("NumSousLotSav") = NodeModSpec.ParentNode.GetValue("NumSousLot") Then
        '                For j = 0 To SaveDonnee.Nodes(i).Nodes.Count - 1
        '                    If SaveDonnee.Nodes(i).Nodes(j).GetValue("IdentifiantSav") = NodeModSpec.GetValue("Identifiant") And SaveDonnee.Nodes(i).Nodes(j).GetValue("LibelleSav") = NodeModSpec.GetValue("Libelle") Then
        '                        SaveDonnee.Nodes(i).Nodes(j).SetValue("LibelleSav", "    - " & TxtLibelleCaract.Text.Trim() & "  :  " & TxtValeurCaract.Text.Trim())
        '                        Exit For
        '                    End If
        '                Next
        '                Exit For
        '            End If
        '        Next
        '        NodeModSpec.SetValue("Libelle", "    - " & TxtLibelleCaract.Text.Trim() & "  :  " & TxtValeurCaract.Text.Trim())

        '    End If
        '    txtCodeCateg.Text = ""
        '    TxtLibCategBien.Text = ""
        '    ViderSaisieBien()
        '    modifSpecTech = False
        '    cmbLotSpecTech.Enabled = True
        '    CmbSousLotSpecTech.Enabled = True
        '    BtCategBien.Enabled = True
        '    LockSaisieBien(False)
        'End If
    End Sub

    Private Sub ViderSaisieCaract()
        TxtLibelleCaract.ResetText()
        TxtValeurCaract.ResetText()
    End Sub

    Private Sub CmbUniteBien_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbUniteBien.SelectedValueChanged
        If CmbUniteBien.SelectedIndex > -1 Then
            query = "select LibelleUnite from T_Unite where LibelleCourtUnite='" & CmbUniteBien.Text & "' LIMIT 1"
            TxtUniteBien.Text = MettreApost(ExecuteScallar(query))
        Else
            TxtUniteBien.ResetText()
        End If
    End Sub

    Private Sub ChargerGridSpecif(LotIndex As Integer, SousLotIndex As Integer)
        Dim dt As DataTable = GridSpecifTech.DataSource
        If LotIndex < 0 Then
            GridSpecifTech.DataSource = New DataTable
            'dt.Rows.Clear()
            Exit Sub
        End If
        'Dim CurrentLot As DaoSpecTechLot = SpecTech(LotIndex)
        'If CurrentLot.AreSousLot Then
        '    If SousLotIndex < 0 Then
        '        GridSpecifTech.DataSource = New DataTable

        '        'dt.Rows.Clear()
        '        Exit Sub
        '    Else
        '        Dim CurrentSousLot As DaoSpecTechSousLot = CurrentLot.GetSousLot(SousLotIndex)
        '        If IsNothing(CurrentSousLot.DataTable) Then
        '            GridSpecifTech.DataSource = New DataTable

        '            'dt.Rows.Clear()
        '            Exit Sub
        '        Else
        '            GridSpecifTech.DataSource = CurrentSousLot.DataTable

        '        End If
        '    End If
        'Else
        '    If IsNothing(CurrentLot.DataTable) Then
        '        GridSpecifTech.DataSource = New DataTable


        '        'dt.Rows.Clear()
        '        Exit Sub
        '    Else
        '        GridSpecifTech.DataSource = CurrentLot.DataTable
        '    End If
        'End If

        ViewSpecTechn.OptionsView.ColumnAutoWidth = True
        ColorRowGrid(ViewSpecTechn, "[Quantité]<>''", Color.LightBlue, "Tahoma", 8, FontStyle.Bold, Color.Black)
    End Sub

    Private Sub LoadSpecTech()
        ' SpecTech.Clear()
        'For Each rw As DataRow In dt2.Rows
        'Dim rootNode As TreeListNode = ListePostQualif.AppendNode(New Object() {rw("RefCritere").ToString, "G", MettreApost(rw("LibelleCritere").ToString), ""}, parentForRootNodes)
        '    query = "select RefCritere, LibelleCritere, CritereElimine from T_DAO_PostQualif where NumeroDAO='" & NumDoss & "' and RefCritereMere='" & rw("RefCritere").ToString & "'"
        '    Dim dt3 As DataTable = ExcecuteSelectQuery(query)
        ''For Each rw1 As DataRow In dt3.Rows
        'ListePostQualif.AppendNode(New Object() {rw1("RefCritere").ToString, "", "      " & MettreApost(rw1("LibelleCritere").ToString), rw1("CritereElimine").ToString}, rootNode)
        ''Next
        ''Next
        SaveDonnee.Nodes.Clear()
        SaveDonnee.BeginUnboundLoad()
        cmbLotSpecTech.Properties.Items.Clear()
        query = "select RefLot,CodeLot from T_LotDAO where NumeroDAO='" & NumDoss & "' order by CodeLot"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rwLot As DataRow In dt.Rows
            cmbLotSpecTech.Properties.Items.Add(rwLot("CodeLot"))

            Dim NewLotSpecTech As New DaoSpecTechLot
            NewLotSpecTech.CodeLot = rwLot("CodeLot")
            NewLotSpecTech.RefLot = rwLot("RefLot")

            query = "SELECT * FROM t_lotdao_souslot WHERE RefLot='" & rwLot("RefLot") & "'"
            Dim dtSousLot As DataTable = ExcecuteSelectQuery(query)
            Dim NbreSousLot As Integer = dtSousLot.Rows.Count
            If NbreSousLot > 0 Then
                NewLotSpecTech.AreSousLot = True
                For Each rwSousLot As DataRow In dtSousLot.Rows
                    Dim dtSpecTech As New DataTable
                    dtSpecTech.Columns.Add("Id", Type.GetType("System.String"))
                    dtSpecTech.Columns.Add("Code", Type.GetType("System.String"))
                    dtSpecTech.Columns.Add("Libellé", Type.GetType("System.String"))
                    dtSpecTech.Columns.Add("Quantité", Type.GetType("System.String"))
                    dtSpecTech.Columns.Add("Lieu de livraison", Type.GetType("System.String"))
                    dtSpecTech.Columns.Add("CodeCateg", Type.GetType("System.String"))
                    dtSpecTech.Columns.Add("NumLot", Type.GetType("System.String"))
                    dtSpecTech.Columns.Add("NumSousLot", Type.GetType("System.String"))
                    dtSpecTech.Columns.Add("Edit", Type.GetType("System.Boolean"))

                    query = "select * from T_SpecTechFourniture where NumeroDAO='" & NumDoss & "' and CodeLot='" & rwLot("CodeLot") & "' and CodeSousLot='" & rwSousLot("CodeSousLot") & "' order by CodeFournit"
                    Dim dtBiens As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dtBiens.Rows
                        Dim drS1 = dtSpecTech.NewRow()
                        drS1("Id") = rw("RefSpecFournit").ToString
                        drS1("Code") = MettreApost(rw("CodeFournit").ToString)
                        drS1("Libellé") = MettreApost(rw("DescripFournit").ToString)
                        drS1("Quantité") = rw("QteFournit").ToString & " " & MettreApost(rw("UniteFournit").ToString)
                        drS1("Lieu de livraison") = MettreApost(rw("LieuLivraison").ToString)
                        drS1("CodeCateg") = rw("CodeCategorie").ToString
                        drS1("NumLot") = rw("CodeLot").ToString
                        drS1("NumSousLot") = rw("CodeSousLot").ToString
                        drS1("Edit") = False
                        dtSpecTech.Rows.Add(drS1)

                        query = "select * from T_SpecTechCaract where RefSpecFournit='" & rw("RefSpecFournit").ToString & "'"
                        Dim dt1 As DataTable = ExcecuteSelectQuery(query)

                        For Each rw1 As DataRow In dt1.Rows
                            drS1 = dtSpecTech.NewRow()

                            drS1("Id") = rw1("RefSpecCaract").ToString
                            drS1("Code") = ""
                            drS1("Libellé") = "   - " & MettreApost(rw1("LibelleCaract").ToString) & "  :  " & MettreApost(rw1("ValeurCaract").ToString)
                            drS1("Quantité") = ""
                            drS1("Lieu de livraison") = ""
                            drS1("CodeCateg") = ""
                            drS1("NumLot") = ""
                            drS1("NumSousLot") = ""
                            drS1("Edit") = False

                            dtSpecTech.Rows.Add(drS1)
                        Next

                    Next

                    Dim parentForRootNodes As TreeListNode = Nothing
                    query = "select * from T_SpecTechFourniture where NumeroDAO='" & NumDoss & "' and CodeLot='" & rwLot("CodeLot") & "' and CodeSousLot='" & rwSousLot("CodeSousLot") & "' order by CodeFournit"
                    Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw In dt2.Rows
                        Dim rootNode As TreeListNode = SaveDonnee.AppendNode(New Object() {rw("RefSpecFournit").ToString, MettreApost(rw("CodeFournit").ToString), MettreApost(rw("DescripFournit").ToString), rw("QteFournit").ToString & " " & MettreApost(rw("UniteFournit").ToString), MettreApost(rw("LieuLivraison").ToString), rw("CodeCategorie").ToString, rw("CodeLot").ToString, rw("CodeSousLot").ToString, False}, parentForRootNodes)
                        query = "select * from T_SpecTechCaract where RefSpecFournit='" & rw("RefSpecFournit").ToString & "'"
                        Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw1 As DataRow In dt3.Rows
                            SaveDonnee.AppendNode(New Object() {rw1("RefSpecCaract").ToString, "", "     - " & MettreApost(rw1("LibelleCaract").ToString) & "  :  " & MettreApost(rw1("ValeurCaract").ToString), "", "", "", "", "", False}, rootNode)
                        Next
                    Next
                    Dim NewSousLot As New DaoSpecTechSousLot
                    NewSousLot.CodeSousLot = rwSousLot("CodeSousLot")
                    NewSousLot.RefSousLot = rwSousLot("RefSousLot")
                    NewSousLot.DataTable = dtSpecTech
                    NewLotSpecTech.AddSousLot(NewSousLot)
                Next
            Else
                Dim dtSpecTech As New DataTable
                dtSpecTech.Columns.Add("Id", Type.GetType("System.String"))
                dtSpecTech.Columns.Add("Code", Type.GetType("System.String"))
                dtSpecTech.Columns.Add("Libellé", Type.GetType("System.String"))
                dtSpecTech.Columns.Add("Quantité", Type.GetType("System.String"))
                dtSpecTech.Columns.Add("Lieu de livraison", Type.GetType("System.String"))
                dtSpecTech.Columns.Add("CodeCateg", Type.GetType("System.String"))
                dtSpecTech.Columns.Add("NumLot", Type.GetType("System.String"))
                dtSpecTech.Columns.Add("NumSousLot", Type.GetType("System.String"))
                dtSpecTech.Columns.Add("Edit", Type.GetType("System.Boolean"))

                query = "select * from T_SpecTechFourniture where NumeroDAO='" & NumDoss & "' and CodeLot='" & rwLot("CodeLot") & "' and CodeSousLot='' order by CodeFournit"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    Dim drS1 = dtSpecTech.NewRow()
                    drS1("Id") = rw("RefSpecFournit").ToString
                    drS1("Code") = MettreApost(rw("CodeFournit").ToString)
                    drS1("Libellé") = MettreApost(rw("DescripFournit").ToString)
                    drS1("Quantité") = rw("QteFournit").ToString & " " & MettreApost(rw("UniteFournit").ToString)
                    drS1("Lieu de livraison") = MettreApost(rw("LieuLivraison").ToString)
                    drS1("CodeCateg") = rw("CodeCategorie").ToString
                    drS1("NumLot") = rw("CodeLot").ToString
                    drS1("NumSousLot") = rw("CodeSousLot").ToString
                    drS1("Edit") = False
                    dtSpecTech.Rows.Add(drS1)

                    query = "select * from T_SpecTechCaract where RefSpecFournit='" & rw("RefSpecFournit").ToString & "'"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)

                    For Each rw1 As DataRow In dt1.Rows
                        drS1 = dtSpecTech.NewRow()

                        drS1("Id") = rw1("RefSpecCaract").ToString
                        drS1("Code") = ""
                        drS1("Libellé") = "   - " & MettreApost(rw1("LibelleCaract").ToString) & "  :  " & MettreApost(rw1("ValeurCaract").ToString)
                        drS1("Quantité") = ""
                        drS1("Lieu de livraison") = ""
                        drS1("CodeCateg") = ""
                        drS1("NumLot") = ""
                        drS1("NumSousLot") = ""
                        drS1("Edit") = False

                        dtSpecTech.Rows.Add(drS1)
                    Next

                Next
                Dim parentForRootNodes As TreeListNode = Nothing
                query = "select * from T_SpecTechFourniture where NumeroDAO='" & NumDoss & "' and CodeLot='" & rwLot("CodeLot") & "' and CodeSousLot='' order by CodeFournit"
                Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                For Each rw3 In dt2.Rows
                    Dim rootNode As TreeListNode = SaveDonnee.AppendNode(New Object() {rw3("RefSpecFournit").ToString, MettreApost(rw3("CodeFournit").ToString), MettreApost(rw3("DescripFournit").ToString), rw3("QteFournit").ToString & " " & MettreApost(rw3("UniteFournit").ToString), MettreApost(rw3("LieuLivraison").ToString), rw3("CodeCategorie").ToString, rw3("CodeLot").ToString, rw3("CodeSousLot").ToString, False}, parentForRootNodes)
                    query = "select * from T_SpecTechCaract where RefSpecFournit='" & rw3("RefSpecFournit").ToString & "'"
                    Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt3.Rows
                        SaveDonnee.AppendNode(New Object() {rw1("RefSpecCaract").ToString, "", "     - " & MettreApost(rw1("LibelleCaract").ToString) & "  :  " & MettreApost(rw1("ValeurCaract").ToString), "", "", "", "", "", False}, rootNode)
                    Next
                Next
                NewLotSpecTech.DataTable = dtSpecTech
            End If

            'SpecTech.Add(NewLotSpecTech)

        Next
        SaveDonnee.EndUnboundLoad()
    End Sub

    Private Sub LoadPageSpecTech(ByVal NumDossier As String)
        InitSpecTechnq()
        If Not PageSpecTech.PageEnabled Then PageSpecTech.PageEnabled = True
        If IsNothing(CurrentDao) Then
            query = "SELECT * FROM t_dao WHERE NumeroDAO='" & NumDoss & "'"
            Dim dtDao As DataTable = ExcecuteSelectQuery(query)
            If dtDao.Rows.Count = 0 Then
                CurrentDao = Nothing
                Exit Sub
            End If
            CurrentDao = dtDao.Rows(0)
        End If

        If IsNothing(CurrentMarche) Then
            query = "SELECT * FROM t_marche WHERE RefMarche='" & CurrentDao("RefMarche") & "'"
            Dim dtmarche As DataTable = ExcecuteSelectQuery(query)
            If dtmarche.Rows.Count > 0 Then
                CurrentMarche = dtmarche.Rows(0)
            Else
                CurrentMarche = Nothing
                FailMsg("Le marché associé a été supprimé")
                Exit Sub
            End If
        End If
        If cmbLotSpecTech.Text = "" Then
            ListeSpecTech.Nodes.Clear()
        End If
        LoadSpecTech()

    End Sub

    Private Function SavePageSpecTech(ByVal NumDossier As String) As Boolean
        'For Each rwLot As DaoSpecTechLot In SpecTech
        '    Dim dtSpec As DataTable
        '    Dim NumLot As String = rwLot.CodeLot
        '    Dim NumSousLot As String = String.Empty

        '    If Not rwLot.AreSousLot Then
        '        dtSpec = rwLot.DataTable
        '        'SuccesMsg("false : " & dtSpec.Rows.Count)
        '        Dim CodeBiens As String = String.Empty
        '        Dim CodeSpecTech As String = String.Empty
        '        Dim IdBien As String = String.Empty
        '        'Nettoyage des éléments supprimés
        '        For i = 0 To dtSpec.Rows.Count - 1
        '            Dim rw As DataRow = dtSpec.Rows(i)
        '            Dim CodeBien As String = rw("Code").ToString()
        '            Dim Id As String = rw("Id").ToString()
        '            If CodeBien.Length > 0 Then 'Cas d'un bien
        '                If Id <> "##" Then
        '                    CodeBiens &= "'" & Id & "',"
        '                End If
        '            Else 'Cas d'une caractéristique
        '                If Id <> "##" Then
        '                    CodeSpecTech &= "'" & Id & "',"
        '                End If
        '            End If
        '        Next
        '        'Suppression des éléments dans la BD
        '        If CodeSpecTech.Length > 0 Then
        '            CodeSpecTech = Mid(CodeSpecTech, 1, (CodeSpecTech.Length - 1))
        '            query = "DELETE FROM t_spectechcaract WHERE RefSpecCaract NOT IN(" & CodeSpecTech & ") AND RefSpecFournit IN(SELECT RefSpecFournit FROM t_spectechfourniture WHERE CodeLot='" & NumLot & "' AND CodeSousLot='" & NumSousLot & "' AND NumeroDAO='" & NumDoss & "')"
        '            ExecuteNonQuery(query)
        '        Else
        '            query = "DELETE FROM t_spectechcaract WHERE RefSpecFournit IN(SELECT RefSpecFournit FROM t_spectechfourniture WHERE CodeLot='" & NumLot & "' AND CodeSousLot='" & NumSousLot & "' AND NumeroDAO='" & NumDoss & "')"
        '            ExecuteNonQuery(query)
        '        End If
        '        If CodeBiens.Length > 0 Then
        '            CodeBiens = Mid(CodeBiens, 1, (CodeBiens.Length - 1))
        '            query = "DELETE FROM t_spectechcaract WHERE RefSpecFournit IN(SELECT RefSpecFournit FROM t_spectechfourniture WHERE RefSpecFournit NOT IN(" & CodeBiens & ") AND CodeLot='" & NumLot & "' AND CodeSousLot='" & NumSousLot & "' AND NumeroDAO='" & NumDoss & "')"
        '            ExecuteNonQuery(query)
        '            query = "DELETE FROM T_SpecTechFourniture WHERE RefSpecFournit NOT IN(" & CodeBiens & ") AND CodeLot='" & NumLot & "' AND CodeSousLot='" & NumSousLot & "' AND NumeroDAO='" & NumDoss & "'"
        '            ExecuteNonQuery(query)
        '        Else
        '            query = "DELETE FROM t_spectechcaract WHERE RefSpecFournit IN(SELECT RefSpecFournit FROM t_spectechfourniture WHERE CodeLot='" & NumLot & "' AND CodeSousLot='" & NumSousLot & "' AND NumeroDAO='" & NumDoss & "')"
        '            ExecuteNonQuery(query)
        '            query = "DELETE FROM T_SpecTechFourniture WHERE CodeLot='" & NumLot & "' AND CodeSousLot='" & NumSousLot & "' AND NumeroDAO='" & NumDoss & "'"
        '            ExecuteNonQuery(query)
        '        End If

        '        For i = 0 To dtSpec.Rows.Count - 1
        '            Dim rw As DataRow = dtSpec.Rows(i)
        '            Dim CodeBien As String = rw("Code").ToString()
        '            Dim Id As String = rw("Id").ToString()
        '            If CodeBien.Length > 0 Then 'Cas d'un bien
        '                Dim Qte As String = Split(rw("Quantité").ToString(), " ")(0)
        '                Dim Unite As String = Split(rw("Quantité").ToString(), " ")(1)
        '                If Id = "##" Then
        '                    query = "INSERT INTO T_SpecTechFourniture(RefSpecFournit,CodeCategorie,NumeroDAO,CodeLot,CodeSousLot,CodeFournit,DescripFournit,QteFournit,UniteFournit,LieuLivraison)"
        '                    query &= " VALUES(NULL,'" & EnleverApost(rw("CodeCateg")) & "','" & NumDoss & "','" & rw("NumLot") & "','" & rw("NumSousLot") & "'"
        '                    query &= ",'" & rw("Code") & "','" & EnleverApost(rw("Libellé")) & "','" & Qte & "','" & EnleverApost(Unite) & "','" & EnleverApost(rw("Lieu de livraison")) & "')"
        '                    ExecuteNonQuery(query)
        '                    query = "SELECT MAX(RefSpecFournit) FROM T_SpecTechFourniture WHERE CodeLot='" & rw("NumLot") & "' AND CodeSousLot='" & rw("NumSousLot") & "' AND NumeroDAO='" & NumDoss & "'"
        '                    IdBien = ExecuteScallar(query)
        '                    rw("Id") = IdBien
        '                Else
        '                    IdBien = dtSpec.Rows(i)("Id").ToString()
        '                    query = "UPDATE T_SpecTechFourniture SET CodeFournit='" & EnleverApost(rw("Code")) & "', DescripFournit='" & EnleverApost(rw("Libellé")) & "',"
        '                    query &= "QteFournit='" & Qte & "', UniteFournit='" & EnleverApost(Unite) & "', LieuLivraison='" & EnleverApost(rw("Lieu de livraison")) & "' WHERE RefSpecFournit='" & Id & "'"
        '                    ExecuteNonQuery(query)
        '                End If
        '            Else 'Cas d'une caractéristique
        '                Dim Libelle As String = rw("Libellé").ToString().Replace("   - ", "")
        '                Dim LibelleCaract As String = Split(Libelle, " : ")(0)
        '                Dim ValueurCaract As String = Split(Libelle, " : ")(1)
        '                If Id = "##" Then
        '                    query = "INSERT INTO t_spectechcaract(RefSpecCaract,RefSpecFournit,LibelleCaract,ValeurCaract)"
        '                    query &= " VALUES(NULL,'" & IdBien & "','" & EnleverApost(LibelleCaract) & "','" & EnleverApost(ValueurCaract) & "')"
        '                    ExecuteNonQuery(query)
        '                    query = "SELECT MAX(RefSpecCaract) FROM t_spectechcaract WHERE RefSpecFournit='" & IdBien & "'"
        '                    rw("Id") = ExecuteScallar(query)
        '                Else
        '                    query = "UPDATE t_spectechcaract SET LibelleCaract='" & EnleverApost(LibelleCaract) & "', ValeurCaract='" & EnleverApost(ValueurCaract) & "'"
        '                    query &= " WHERE RefSpecCaract='" & Id & "'"
        '                    ExecuteNonQuery(query)
        '                End If
        '            End If
        '        Next
        '    Else

        '        For Each rwSousLot As DaoSpecTechSousLot In rwLot.GetSousLot
        '            dtSpec = rwSousLot.DataTable
        '            'SuccesMsg("True : " & dtSpec.Rows.Count)

        '            NumSousLot = rwSousLot.CodeSousLot
        '            Dim CodeBiens As String = String.Empty
        '            Dim CodeSpecTech As String = String.Empty
        '            Dim IdBien As String = String.Empty
        '            'Nettoyage des éléments supprimés
        '            For i = 0 To dtSpec.Rows.Count - 1
        '                Dim rw As DataRow = dtSpec.Rows(i)
        '                Dim CodeBien As String = rw("Code").ToString()
        '                Dim Id As String = rw("Id").ToString()
        '                If CodeBien.Length > 0 Then 'Cas d'un bien
        '                    If Id <> "##" Then
        '                        CodeBiens &= "'" & Id & "',"
        '                    End If
        '                Else 'Cas d'une caractéristique
        '                    If Id <> "##" Then
        '                        CodeSpecTech &= "'" & Id & "',"
        '                    End If
        '                End If
        '            Next
        '            'Suppression des éléments dans la BD
        '            If CodeSpecTech.Length > 0 Then
        '                CodeSpecTech = Mid(CodeSpecTech, 1, (CodeSpecTech.Length - 1))
        '                query = "DELETE FROM t_spectechcaract WHERE RefSpecCaract NOT IN(" & CodeSpecTech & ") AND RefSpecFournit IN(SELECT RefSpecFournit FROM t_spectechfourniture WHERE CodeLot='" & NumLot & "' AND CodeSousLot='" & NumSousLot & "' AND NumeroDAO='" & NumDoss & "')"
        '                ExecuteNonQuery(query)
        '            Else
        '                query = "DELETE FROM t_spectechcaract WHERE RefSpecFournit IN(SELECT RefSpecFournit FROM t_spectechfourniture WHERE CodeLot='" & NumLot & "' AND CodeSousLot='" & NumSousLot & "' AND NumeroDAO='" & NumDoss & "')"
        '                ExecuteNonQuery(query)
        '            End If
        '            If CodeBiens.Length > 0 Then
        '                CodeBiens = Mid(CodeBiens, 1, (CodeBiens.Length - 1))
        '                query = "DELETE FROM t_spectechcaract WHERE RefSpecFournit IN(SELECT RefSpecFournit FROM t_spectechfourniture WHERE RefSpecFournit NOT IN(" & CodeBiens & ") AND CodeLot='" & NumLot & "' AND CodeSousLot='" & NumSousLot & "' AND NumeroDAO='" & NumDoss & "')"
        '                ExecuteNonQuery(query)
        '                query = "DELETE FROM T_SpecTechFourniture WHERE RefSpecFournit NOT IN(" & CodeBiens & ") AND CodeLot='" & NumLot & "' AND CodeSousLot='" & NumSousLot & "' AND NumeroDAO='" & NumDoss & "'"
        '                ExecuteNonQuery(query)
        '            Else
        '                query = "DELETE FROM t_spectechcaract WHERE RefSpecFournit IN(SELECT RefSpecFournit FROM t_spectechfourniture WHERE CodeLot='" & NumLot & "' AND CodeSousLot='" & NumSousLot & "' AND NumeroDAO='" & NumDoss & "')"
        '                ExecuteNonQuery(query)
        '                query = "DELETE FROM T_SpecTechFourniture WHERE CodeLot='" & NumLot & "' AND CodeSousLot='" & NumSousLot & "' AND NumeroDAO='" & NumDoss & "'"
        '                ExecuteNonQuery(query)
        '            End If

        '            For i = 0 To dtSpec.Rows.Count - 1
        '                Dim rw As DataRow = dtSpec.Rows(i)
        '                Dim CodeBien As String = rw("Code").ToString()
        '                Dim Id As String = rw("Id").ToString()
        '                If CodeBien.Length > 0 Then 'Cas d'un bien
        '                    Dim Qte As String = Split(rw("Quantité").ToString(), " ")(0)
        '                    Dim Unite As String = Split(rw("Quantité").ToString(), " ")(1)
        '                    If Id = "##" Then
        '                        query = "INSERT INTO T_SpecTechFourniture(RefSpecFournit,CodeCategorie,NumeroDAO,CodeLot,CodeSousLot,CodeFournit,DescripFournit,QteFournit,UniteFournit,LieuLivraison)"
        '                        query &= " VALUES(NULL,'" & EnleverApost(rw("CodeCateg")) & "','" & NumDoss & "','" & rw("NumLot") & "','" & rw("NumSousLot") & "'"
        '                        query &= ",'" & rw("Code") & "','" & EnleverApost(rw("Libellé")) & "','" & Qte & "','" & EnleverApost(Unite) & "','" & EnleverApost(rw("Livré à")) & "')"
        '                        ExecuteNonQuery(query)
        '                        query = "SELECT MAX(RefSpecFournit) FROM T_SpecTechFourniture WHERE CodeLot='" & rw("NumLot") & "' AND CodeSousLot='" & rw("NumSousLot") & "' AND NumeroDAO='" & NumDoss & "'"
        '                        IdBien = ExecuteScallar(query)
        '                        rw("Id") = IdBien
        '                    Else
        '                        IdBien = dtSpec.Rows(i)("Id").ToString()
        '                        query = "UPDATE T_SpecTechFourniture SET CodeFournit='" & EnleverApost(rw("Code")) & "', DescripFournit='" & EnleverApost(rw("Libellé")) & "',"
        '                        query &= "QteFournit='" & Qte & "', UniteFournit='" & EnleverApost(Unite) & "', LieuLivraison='" & EnleverApost(rw("Lieu de livraison")) & "' WHERE RefSpecFournit='" & Id & "'"
        '                        ExecuteNonQuery(query)
        '                    End If
        '                Else 'Cas d'une caractéristique
        '                    Dim Libelle As String = rw("Libellé").ToString().Replace("   - ", "")
        '                    Dim LibelleCaract As String = Split(Libelle, " : ")(0)
        '                    Dim ValueurCaract As String = Split(Libelle, " : ")(1)
        '                    If Id = "##" Then
        '                        query = "INSERT INTO t_spectechcaract(RefSpecCaract,RefSpecFournit,LibelleCaract,ValeurCaract)"
        '                        query &= " VALUES(NULL,'" & IdBien & "','" & EnleverApost(LibelleCaract) & "','" & EnleverApost(ValueurCaract) & "')"
        '                        ExecuteNonQuery(query)
        '                        query = "SELECT MAX(RefSpecCaract) FROM t_spectechcaract WHERE RefSpecFournit='" & IdBien & "'"
        '                        rw("Id") = ExecuteScallar(query)
        '                    Else
        '                        query = "UPDATE t_spectechcaract SET LibelleCaract='" & EnleverApost(LibelleCaract) & "', ValeurCaract='" & EnleverApost(ValueurCaract) & "'"
        '                        query &= " WHERE RefSpecCaract='" & Id & "'"
        '                        ExecuteNonQuery(query)
        '                    End If
        '                End If
        '            Next
        '        Next
        '    End If
        'Next

        Dim IdBien As String = String.Empty
        Dim Qte As String = String.Empty
        Dim Unite As String = String.Empty
        For i = 0 To SaveDonnee.Nodes.Count - 1
            Qte = Split(SaveDonnee.Nodes(i).GetValue("QuantiteSav"), " ")(0)
            Unite = Split(SaveDonnee.Nodes(i).GetValue("QuantiteSav"), " ")(1)
            If SaveDonnee.Nodes(i).GetValue("IdentifiantSav").ToString = "##" And SaveDonnee.Nodes(i).GetValue("CodeSav").ToString <> "" Then
                query = "INSERT INTO T_SpecTechFourniture(RefSpecFournit,CodeCategorie,NumeroDAO,CodeLot,CodeSousLot,CodeFournit,DescripFournit,QteFournit,UniteFournit,LieuLivraison)"
                query &= " VALUES(NULL,'" & EnleverApost(SaveDonnee.Nodes(i).GetValue("CodeCategSav")) & "','" & NumDossier & "','" & SaveDonnee.Nodes(i).GetValue("NumLotSav") & "','" & SaveDonnee.Nodes(i).GetValue("NumSousLotSav") & "'"
                query &= ",'" & SaveDonnee.Nodes(i).GetValue("CodeSav") & "','" & EnleverApost(SaveDonnee.Nodes(i).GetValue("LibelleSav")) & "','" & Qte & "','" & EnleverApost(Unite) & "','" & EnleverApost(SaveDonnee.Nodes(i).GetValue("LieuLivreSav")) & "')"
                ExecuteNonQuery(query)
                query = "SELECT MAX(RefSpecFournit) FROM T_SpecTechFourniture WHERE CodeLot='" & SaveDonnee.Nodes(i).GetValue("NumLotSav") & "' AND CodeSousLot='" & SaveDonnee.Nodes(i).GetValue("NumSousLotSav") & "' AND NumeroDAO='" & NumDossier & "'"
                IdBien = ExecuteScallar(query)
                SaveDonnee.Nodes(i).SetValue("IdentifiantSav", IdBien)

                For j = 0 To SaveDonnee.Nodes(i).Nodes.Count - 1
                    Dim Libelle As String = SaveDonnee.Nodes(i).Nodes(j).GetValue("LibelleSav").ToString().Replace("   - ", "")
                    Dim LibelleCaract As String = Split(Libelle, " : ")(0)
                    Dim ValueurCaract As String = Split(Libelle, " : ")(1)
                    If SaveDonnee.Nodes(i).Nodes(j).Item("IdentifiantSav").ToString = "##" Then
                        query = "INSERT INTO t_spectechcaract(RefSpecCaract,RefSpecFournit,LibelleCaract,ValeurCaract)"
                        query &= " VALUES(NULL,'" & IdBien & "','" & EnleverApost(LibelleCaract) & "','" & EnleverApost(ValueurCaract) & "')"
                        ExecuteNonQuery(query)

                        Dim Id As String = ExecuteScallar("SELECT MAX(RefSpecCaract) FROM t_spectechcaract WHERE RefSpecFournit='" & IdBien & "'")

                        SaveDonnee.Nodes(i).Nodes(j).SetValue("IdentifiantSav", Id)
                    End If
                Next
            Else
                IdBien = SaveDonnee.Nodes(i).GetValue("IdentifiantSav")
                query = "UPDATE T_SpecTechFourniture SET CodeFournit='" & EnleverApost(SaveDonnee.Nodes(i).GetValue("CodeSav")) & "', DescripFournit='" & EnleverApost(SaveDonnee.Nodes(i).GetValue("LibelleSav")) & "',"
                query &= "QteFournit='" & Qte & "', UniteFournit='" & EnleverApost(Unite) & "', LieuLivraison='" & EnleverApost(SaveDonnee.Nodes(i).GetValue("LieuLivreSav")) & "' WHERE RefSpecFournit='" & IdBien & "'"
                ExecuteNonQuery(query)

                For j = 0 To SaveDonnee.Nodes(i).Nodes.Count - 1
                    Dim Libelle As String = SaveDonnee.Nodes(i).Nodes(j).GetValue("LibelleSav").ToString().Replace("   - ", "")
                    Dim LibelleCaract As String = Split(Libelle, " : ")(0)
                    Dim ValueurCaract As String = Split(Libelle, " : ")(1)
                    If SaveDonnee.Nodes(i).Nodes(j).Item("IdentifiantSav").ToString = "##" Then
                        If SaveDonnee.Nodes(i).Nodes(j).Item("IdentifiantSav").ToString = "##" Then
                            query = "INSERT INTO t_spectechcaract(RefSpecCaract,RefSpecFournit,LibelleCaract,ValeurCaract)"
                            query &= " VALUES(NULL,'" & IdBien & "','" & EnleverApost(LibelleCaract) & "','" & EnleverApost(ValueurCaract) & "')"
                            ExecuteNonQuery(query)
                            Dim Id As String = ExecuteScallar("SELECT MAX(RefSpecCaract) FROM t_spectechcaract WHERE RefSpecFournit='" & IdBien & "'")
                            SaveDonnee.Nodes(i).Nodes(j).SetValue("IdentifiantSav", Id)
                        End If
                    Else
                        query = "UPDATE t_spectechcaract SET LibelleCaract='" & EnleverApost(LibelleCaract) & "', ValeurCaract='" & EnleverApost(ValueurCaract) & "'"
                        query &= " WHERE RefSpecCaract='" & SaveDonnee.Nodes(i).Nodes(j).GetValue("IdentifiantSav") & "'"
                        ExecuteNonQuery(query)
                    End If
                Next
            End If
        Next

        'If CodeSpecTechSup.Count > 0 Then
        '    For k = 0 To CodeSpecTechSup.Count - 1
        '        If CodeSpecTechSup.Item(k).ToString.Split("-")(0) <> "##" Then
        '            Dim Cat = CodeSpecTechSup.Item(k).ToString.Split("-")(1)
        '            Dim Code = CInt(CodeSpecTechSup.Item(k).ToString.Split("-")(0))
        '            If Cat <> "" Then
        '                query = "DELETE FROM T_SpecTechFourniture WHERE NumeroDAO='" & NumDossier & "' AND RefSpecFournit='" & Code & "'"
        '                ExecuteNonQuery(query)
        '            Else
        '                ExecuteNonQuery("DELETE FROM t_spectechcaract WHERE RefSpecCaract='" & Code & "'")
        '            End If
        '        End If
        '    Next
        'End If
        'CodeSpecTechSup.Clear()
        ' actualiserListe(cmbLotSpecTech.Text, CmbSousLotSpecTech.Text)
        Return True
    End Function

    Private Sub ToolStripMenuModifierSpecTech_Click(sender As Object, e As EventArgs) Handles ToolStripMenuModifierSpecTech.Click
        If ListeSpecTech.Nodes.Count > 0 Then
            'NodeModSpec = ListeSpecTech.FocusedNode
            Dim node1 As TreeListNode = ListeSpecTech.FocusedNode.ParentNode

            'Modification d'un caracteristique
            If Not ListeSpecTech.FocusedNode.ParentNode Is Nothing Then
                UnlockModCaract()
                Dim cat = Split(node1.GetValue("CodeCateg").ToString, "-")(1).ToString
                If cat = "Cat" Then
                    query = "SELECT LibelleCat FROM t_predfournitures_groupe WHERE IdCat='" & Split(node1.GetValue("CodeCateg").ToString, "-")(0).ToString & "'"
                Else
                    query = "SELECT LibelleSousCat FROM t_predfournitures_sous_groupe WHERE IdSousCat='" & Split(node1.GetValue("CodeCateg").ToString, "-")(0).ToString & "'"
                End If

                TxtLibCategBien.Text = MettreApost(ExecuteScallar(query))
                Dim Quantite() = Split(node1.GetValue("Quantite").ToString, " ")
                TxtRefArticle.Text = node1.GetValue("RefArticle").ToString.Trim()
                TxtLibelleBien.Text = node1.GetValue("LibelleArticle").ToString.Trim()
                NumQteBien.Value = Quantite(0).ToString
                CmbUniteBien.Text = Quantite(1).ToString
                TxtLieuLivraison.Text = node1.GetValue("LieuLivre").ToString
                Dim libelleCaracteristique() = ListeSpecTech.FocusedNode.GetValue("LibelleArticle").ToString.Trim().Split(":")
                TxtLibelleCaract.Text = libelleCaracteristique(0).ToString.Split("-")(1).ToString.Trim
                TxtValeurCaract.Text = libelleCaracteristique(1).ToString.Trim()
                txtCodeCateg.Text = node1.GetValue("CodeCateg").ToString
                CodeCategorie.Text = node1.GetValue("CodeCateg").ToString
                RefSpecificationModif.Text = ListeSpecTech.FocusedNode.GetValue("Identifiant").ToString

            Else
                'Modification d'une ligne principale
                ' NodeModSpec = ListeSpecTech.FocusedNode
                Dim cat = Split(ListeSpecTech.FocusedNode.GetValue("CodeCateg").ToString, "-")(1).ToString
                If cat = "Cat" Then
                    query = "SELECT LibelleCat FROM t_predfournitures_groupe WHERE IdCat='" & Split(ListeSpecTech.FocusedNode.GetValue("CodeCateg").ToString, "-")(0).ToString & "'"
                Else
                    query = "SELECT LibelleSousCat FROM t_predfournitures_sous_groupe WHERE IdSousCat='" & Split(ListeSpecTech.FocusedNode.GetValue("CodeCateg").ToString, "-")(0).ToString & "'"
                End If
                TxtLibCategBien.Text = MettreApost(ExecuteScallar(query))
                TxtLibelleCaract.Text = ""
                TxtValeurCaract.Text = ""
                Dim Quantite() = Split(ListeSpecTech.FocusedNode.GetValue("Quantite").ToString, " ")
                TxtRefArticle.Text = ListeSpecTech.FocusedNode.GetValue("RefArticle").ToString.Trim()
                TxtLibelleBien.Text = ListeSpecTech.FocusedNode.GetValue("LibelleArticle").ToString.Trim()
                NumQteBien.Value = Quantite(0).ToString
                CmbUniteBien.Text = Quantite(1).ToString
                TxtLieuLivraison.Text = ListeSpecTech.FocusedNode.GetValue("LieuLivre").ToString
                txtCodeCateg.Text = ListeSpecTech.FocusedNode.GetValue("CodeCateg").ToString
                CodeCategorie.Text = ListeSpecTech.FocusedNode.GetValue("CodeCateg").ToString
                RefSpecificationModif.Text = ListeSpecTech.FocusedNode.GetValue("Identifiant").ToString
                UnlockModBien()
            End If

            modifSpecTech = True
        End If
    End Sub

    Private Sub AjouterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AjouterToolStripMenuItem.Click
        If ListeSpecTech.Nodes.Count > 0 Then
            Dim cat = Split(ListeSpecTech.FocusedNode.GetValue("CodeCateg").ToString, "-")(1).ToString
            If cat = "Cat" Then
                query = "SELECT LibelleCat FROM t_predfournitures_groupe WHERE IdCat='" & Split(ListeSpecTech.FocusedNode.GetValue("CodeCateg").ToString, "-")(0).ToString & "'"
            Else
                query = "SELECT LibelleSousCat FROM t_predfournitures_sous_groupe WHERE IdSousCat='" & Split(ListeSpecTech.FocusedNode.GetValue("CodeCateg").ToString, "-")(0).ToString & "'"
            End If
            TxtLibCategBien.Text = MettreApost(ExecuteScallar(query))
            TxtLibelleCaract.Text = ""
            TxtValeurCaract.Text = ""
            Dim Quantite() = Split(ListeSpecTech.FocusedNode.GetValue("Quantite").ToString, " ")
            TxtRefArticle.Text = ListeSpecTech.FocusedNode.GetValue("RefArticle").ToString.Trim()
            TxtLibelleBien.Text = ListeSpecTech.FocusedNode.GetValue("LibelleArticle").ToString.Trim()
            NumQteBien.Value = Quantite(0).ToString
            CmbUniteBien.Text = Quantite(1).ToString
            TxtLieuLivraison.Text = ListeSpecTech.FocusedNode.GetValue("LieuLivre").ToString
            txtCodeCateg.Text = ListeSpecTech.FocusedNode.GetValue("CodeCateg").ToString
            CodeCategorie.Text = ListeSpecTech.FocusedNode.GetValue("CodeCateg").ToString
            RefSpecificationModif.Text = ListeSpecTech.FocusedNode.GetValue("Identifiant").ToString

            modifSpecTech = False

            LockSaisieBien(True)
            TxtRefArticle.Enabled = False
            BtCategBien.Enabled = False
            TxtLibCategBien.Enabled = False
            CmbSousLotSpecTech.Enabled = False
            cmbLotSpecTech.Enabled = True

            'If ListeSpecTech.FocusedNode.GetValue("NumSousLot").ToString <> "" Then
            '    CmbSousLotSpecTech.Enabled = True
            'Else
            'End If
            NewAddSpecTechClik = True
        End If
    End Sub


    Private Sub ListeSpecTech_MouseUp(sender As Object, e As MouseEventArgs) Handles ListeSpecTech.MouseUp
        If ListeSpecTech.Nodes.Count > 0 Then
            If Not ListeSpecTech.FocusedNode.ParentNode Is Nothing Then
                ContextMenuSpectTech.Items(0).Visible = False
            Else
                ContextMenuSpectTech.Items(0).Visible = True
            End If
        End If
    End Sub

    Private Sub ToolStripMenuSupprimerSpecTech_Click(sender As Object, e As EventArgs) Handles ToolStripMenuSupprimerSpecTech.Click
        If ListeSpecTech.Nodes.Count > 0 Then

            Dim node As TreeListNode = ListeSpecTech.FocusedNode
            Dim node1 As TreeListNode = ListeSpecTech.FocusedNode.ParentNode

            'Suppression d'un caractères
            If Not node.ParentNode Is Nothing Then
                ExecuteNonQuery("delete from t_spectechcaract where RefSpecCaract='" & node.GetValue("Identifiant") & "'")
                'node.ParentNode.Nodes.Remove(node)
                ListeSpecTech.DeleteNode(node)

                'CodeSpecTechSup.Add(node.GetValue("Identifiant").ToString & "-" & node.GetValue("Code").ToString)
                'For i = 0 To SaveDonnee.Nodes.Count - 1
                '    If SaveDonnee.Nodes(i).GetValue("IdentifiantSav") = node.ParentNode.GetValue("Identifiant") And SaveDonnee.Nodes(i).GetValue("CodeSav") = node.ParentNode.GetValue("Code") And SaveDonnee.Nodes(i).GetValue("LibelleSav") = node.ParentNode.GetValue("Libelle") And SaveDonnee.Nodes(i).GetValue("NumLotSav") = node.ParentNode.GetValue("NumLot") And SaveDonnee.Nodes(i).GetValue("NumSousLotSav") = node.ParentNode.GetValue("NumSousLot") Then
                '        For j = 0 To SaveDonnee.Nodes(i).Nodes.Count - 1
                '            If SaveDonnee.Nodes(i).Nodes(j).GetValue("IdentifiantSav") = node.GetValue("Identifiant") And SaveDonnee.Nodes(i).Nodes(j).GetValue("LibelleSav") = node.GetValue("Libelle") Then
                '                SaveDonnee.Nodes(i).Nodes(j).ParentNode.Nodes.Remove(SaveDonnee.Nodes(i).Nodes(j))
                '                Exit For
                '            End If
                '        Next
                '        Exit For
                '    End If
                'Next
                'node.ParentNode.Nodes.Remove(node)

                'If Not node1.HasChildren Then
                '    CodeSpecTechSup.Add(node1.GetValue("Identifiant").ToString & "-" & node1.GetValue("Code").ToString)
                '    For i = 0 To SaveDonnee.Nodes.Count - 1
                '        If SaveDonnee.Nodes(i).GetValue("IdentifiantSav") = node1.GetValue("Identifiant") And SaveDonnee.Nodes(i).GetValue("CodeSav") = node1.GetValue("Code") And SaveDonnee.Nodes(i).GetValue("LibelleSav") = node1.GetValue("Libelle") And SaveDonnee.Nodes(i).GetValue("NumLotSav") = node1.GetValue("NumLot") And SaveDonnee.Nodes(i).GetValue("NumSousLotSav") = node1.GetValue("NumSousLot") Then
                '            SaveDonnee.Nodes.Remove(SaveDonnee.Nodes(i))
                '            Exit For
                '        End If
                '    Next
                '    ListeSpecTech.DeleteNode(node1)
                'End If
            Else
                'Bouton d'ajout cliqué
                If NewAddSpecTechClik = True And modifSpecTech = False Then
                    SuccesMsg("Veuillez terminer ou annuler l'action en cours.")
                    Exit Sub
                End If

                If ConfirmMsg("Voulez-vous supprimer cette catégorie avec ses caractéristiques ?") = DialogResult.Yes Then
                    ExecuteNonQuery("delete from t_spectechcaract where RefSpecFournit='" & node.GetValue("Identifiant") & "'")
                    ExecuteNonQuery("delete from t_spectechfourniture where NumeroDAO='" & NumDoss & "' and RefSpecFournit='" & node.GetValue("Identifiant") & "'")

                    'CodeSpecTechSup.Add(node.GetValue("Identifiant").ToString & "-" & node.GetValue("Code").ToString)
                    'For i = 0 To ListeSpecTech.Nodes.Count - 1
                    '    If ListeSpecTech.Nodes(i).GetValue("Identifiant").ToString = node.GetValue("Identifiant").ToString Then
                    '        For j = 0 To ListeSpecTech.Nodes(i).Nodes.Count - 1
                    '            CodeSpecTechSup.Add(ListeSpecTech.Nodes(i).Nodes(j).GetValue("Identifiant").ToString & "-" & node.GetValue("Code").ToString)
                    '        Next
                    '        Exit For
                    '    End If
                    'Next
                    'For i = 0 To SaveDonnee.Nodes.Count - 1
                    '    If SaveDonnee.Nodes(i).GetValue("IdentifiantSav") = node.GetValue("Identifiant") And SaveDonnee.Nodes(i).GetValue("CodeSav") = node.GetValue("Code") And SaveDonnee.Nodes(i).GetValue("LibelleSav") = node.GetValue("Libelle") And SaveDonnee.Nodes(i).GetValue("NumLotSav") = node.GetValue("NumLot") And SaveDonnee.Nodes(i).GetValue("NumSousLotSav") = node.GetValue("NumSousLot") Then
                    '        SaveDonnee.Nodes.Remove(SaveDonnee.Nodes(i))
                    '        Exit For
                    '    End If
                    'Next
                    'ListeSpecTech.Nodes.Remove(node)
                    ChargerLesSpecificationTechnique(cmbLotSpecTech.Text, CmbSousLotSpecTech.Text)
                End If
            End If
        End If
    End Sub

    Private Sub TxtLibelleserviceconnexe_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtLibelleserviceconnexe.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                If cmbLotSpecTech.Text.Trim = "" Then
                    SuccesMsg("Veuillez selectionner un lot.")
                    cmbLotSpecTech.Select()
                    Exit Sub
                End If

                If CmbSousLotSpecTech.Enabled Then
                    If CmbSousLotSpecTech.Text.Trim = "" Then
                        SuccesMsg("Veuillez selectionner un sous lot")
                        CmbSousLotSpecTech.Select()
                        Exit Sub
                    End If
                End If

                If TxtLibelleserviceconnexe.IsRequiredControl("Veuillez saisir le libellé du service connexe") Then
                    TxtLibelleserviceconnexe.Select()
                    Exit Sub
                End If

                If LigneaModifier = True And NomGridView = "GridServiConnex" Then
                    'Update 
                    ExecuteNonQuery("Update t_dao_service_connexe set LibelleService='" & EnleverApost(TxtLibelleserviceconnexe.Text) & "' where IdService='" & GridServiConnex.Rows.Item(IndexActive).Cells("IdService").Value & "'")
                    GridServiConnex.Rows.Item(IndexActive).Cells("Libelles").Value = TxtLibelleserviceconnexe.Text
                Else
                    'Insertion
                    If Val(ExecuteScallar("select count(*) from t_dao_service_connexe where NumeroDAO='" & NumDoss & "' and CodeLot='" & cmbLotSpecTech.Text & "' and CodeSousLot='" & CmbSousLotSpecTech.Text & "' and LibelleService='" & EnleverApost(TxtLibelleserviceconnexe.Text) & "'")) > 0 Then
                        SuccesMsg("Ce service existe déjà")
                        Exit Sub
                    End If

                    ExecuteNonQuery("INSERT INTO t_dao_service_connexe values(NULL,'" & NumDoss & "','" & cmbLotSpecTech.Text & "','" & CmbSousLotSpecTech.Text & "','" & EnleverApost(TxtLibelleserviceconnexe.Text) & "','" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "','" & ProjetEnCours & "')")
                    ChargerListeConnexe(cmbLotSpecTech.Text, CmbSousLotSpecTech.Text)
                End If
                LigneaModifier = False
                NomGridView = ""
                IndexActive = 0
                TxtLibelleserviceconnexe.Text = ""

            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub ChargerListeConnexe(ByVal CodeLot As String, ByVal CodeSousLot As String)
        Try
            GridServiConnex.Rows.Clear()
            Dim dtt As DataTable = ExcecuteSelectQuery("SELECT * FROM t_dao_service_connexe WHERE NumeroDAO='" & NumDoss & "' and CodeLot='" & CodeLot & "' and CodeSousLot='" & CodeSousLot & "' and CodeProjet='" & ProjetEnCours & "'")
            Dim Nbres As Decimal = 0
            For Each rw In dtt.Rows
                Nbres += 1
                Dim NewLigne = GridServiConnex.Rows.Add
                GridServiConnex.Rows.Item(NewLigne).Cells("IdService").Value = rw("IdService").ToString
                GridServiConnex.Rows.Item(NewLigne).Cells("Num").Value = Nbres.ToString
                GridServiConnex.Rows.Item(NewLigne).Cells("Libelles").Value = MettreApost(rw("LibelleService").ToString)
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub
    Private Sub BtSupServicConnex_Click(sender As Object, e As EventArgs) Handles BtSupServicConnex.Click
        If GridServiConnex.Rows.Count > 0 Then
            Dim index = GridServiConnex.CurrentRow.Index
            If ConfirmMsg("Êtes-vous sûrs de vouloir supprimer la ligne N° " & index + 1 & " ?") = DialogResult.Yes Then
                ExecuteNonQuery("delete from t_dao_service_connexe where IdService='" & GridServiConnex.Rows.Item(index).Cells("IdService").Value & "'")
                GridServiConnex.Rows.RemoveAt(index)
                If GridServiConnex.RowCount > 0 Then
                    For Nbres = 0 To GridServiConnex.RowCount - 1
                        GridServiConnex.Rows.Item(Nbres).Cells("Num").Value = Nbres + 1
                    Next
                End If

                LigneaModifier = False
                NomGridView = ""
                IndexActive = 0
            End If
        End If

    End Sub

    Private Sub GridServiConnex_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridServiConnex.CellDoubleClick
        If GridServiConnex.Rows.Count > 0 And AfficherDossier = False Then
            IndexActive = GridServiConnex.CurrentRow.Index
            TxtLibelleserviceconnexe.Text = GridServiConnex.Rows.Item(IndexActive).Cells("Libelles").Value.ToString
            LigneaModifier = True
            NomGridView = "GridServiConnex"
        End If
    End Sub

    Private Sub ContextMenuSpectTech_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuSpectTech.Opening
        If AfficherDossier = True Or ListeSpecTech.Nodes.Count = 0 Then
            e.Cancel = True
        End If
    End Sub
#End Region

#Region "DQE"
    'Les commentaires ['] sont pris en compte pour l'importation et la saisie manuel des DQE
    'Les commentaires [''] ne sont pas pris en compte pour l'importation des DQE

    Private Sub InitDQE()
        CmbLotDQE.Text = ""
        txtLibLotDQE.ResetText()
        CmbLotDQE.Enabled = True
        CmbSousLotDQE.Text = ""
        TxtSousLotDQE.ResetText()
        txtFilePathDQE.ResetText()
        CmbSousLotDQE.Enabled = False
        LoadLotDQE()
        ' CmbSousLot1.Text = ""
        'CmbSousLot1.Enabled = False
        'TxtImportDQE.Text = ""
        ' 'btOpenDQE.Enabled = False
        'GroupControl17.Width = GroupControl13.Width - (GbItemDQE.Width + 8)
        'GroupControl17.Location = New System.Drawing.Point(GbItemDQE.Width + 5, 24)
        'CmbNumLot2.Text = ""
        'CmbNumLot2.Enabled = False
        'RdSection.Checked = True
        DataGridView1.Rows.Clear()
        'GbItemDQE.Enabled = False
        'GbItemDQE.Visible = False
        'GridSousSection.Rows.Clear()
        'GbSousSection.Visible = False
        'ChkSousSection.Checked = False
    End Sub

    Private Sub LoadLotDQE()
        CmbLotDQE.ResetText()
        CmbLotDQE.Properties.Items.Clear()
        query = "select RefLot,CodeLot from T_LotDAO where NumeroDAO='" & NumDoss & "' order by CodeLot"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rwLot As DataRow In dt.Rows
            CmbLotDQE.Properties.Items.Add(rwLot("CodeLot"))
        Next
    End Sub

    Private Sub LoadPageDQE(ByVal NumDossier As String)
        InitDQE()
        If Not PageDQE.PageEnabled Then PageDQE.PageEnabled = True
        If IsNothing(CurrentDao) Then
            query = "SELECT * FROM t_dao WHERE NumeroDAO='" & NumDoss & "'"
            Dim dtDao As DataTable = ExcecuteSelectQuery(query)
            If dtDao.Rows.Count = 0 Then
                CurrentDao = Nothing
                Exit Sub
            End If
            CurrentDao = dtDao.Rows(0)
        End If

        If IsNothing(CurrentMarche) Then
            query = "SELECT * FROM t_marche WHERE RefMarche='" & CurrentDao("RefMarche") & "'"
            Dim dtmarche As DataTable = ExcecuteSelectQuery(query)
            If dtmarche.Rows.Count > 0 Then
                CurrentMarche = dtmarche.Rows(0)
            Else
                CurrentMarche = Nothing
                FailMsg("Le marché associé a été supprimé.")
                Exit Sub
            End If
        End If

        If AfficherDossier = True Then
            SimpleButton1.Enabled = False
            btImportDQE.Enabled = False
            Panel3Ecraser.Enabled = False
        Else
            SimpleButton1.Enabled = True
            btImportDQE.Enabled = True
            Panel3Ecraser.Enabled = True
        End If
    End Sub

    Private Function SavePageDQE(ByVal NumDossier As String) As Boolean
        Return True
    End Function

    Private Sub CmbLotDQE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbLotDQE.SelectedIndexChanged
        Try
            txtLibLotDQE.ResetText()
            CmbSousLotDQE.Properties.Items.Clear()
            CmbSousLotDQE.ResetText()
            TxtSousLotDQE.ResetText()
            DataGridView1.Rows.Clear()
            RefLotDQE.ResetText()

            If CmbLotDQE.SelectedIndex <> -1 Then
                If (NumDoss <> "") Then
                    query = "select LibelleLot,SousLot,RefLot from T_LotDAO where NumeroDAO='" & NumDoss & "' and CodeLot='" & CmbLotDQE.Text & "'"
                    Dim dt As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        'TxtRefLot1.Text = rw(1).ToString
                        RefLotDQE.Text = rw("RefLot").ToString
                        txtLibLotDQE.Text = MettreApost(rw("LibelleLot").ToString)
                        If Val(GetSousLot(CmbLotDQE.Text, NumDoss)(0)) = 0 Then
                            CmbSousLotDQE.Enabled = False
                        Else
                            CmbSousLotDQE.Enabled = True
                        End If
                    Next

                    If (CmbSousLotDQE.Enabled = False) Then
                        MajGridDQE()
                    Else
                        ' LesSousLots(GetRefLot(Val(CmbLotDQE.Text), NumDoss), CmbSousLotDQE)
                        LesSousLots(Val(RefLotDQE.Text), CmbSousLotDQE)
                    End If
                End If
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub CmbSousLotDQE_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSousLotDQE.SelectedValueChanged
        Try
            'CmbSousLot1.Text = CmbSousLotDQE.Text
            If CmbLotDQE.SelectedIndex <> -1 Then
                query = "select LibelleSousLot from T_LotDAO_SousLot where RefLot='" & RefLotDQE.Text & "' and  CodeSousLot='" & CmbSousLotDQE.Text & "' and NumeroDAO='" & NumDoss & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    TxtSousLotDQE.Text = MettreApost(rw("LibelleSousLot").ToString)
                Next

                ' GbSectionDQE.Enabled = True
                ' GbItemDQE.Enabled = True
                ' 'btOpenDQE.Enabled = True
                MajGridDQE()
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim NewOpenFile As New OpenFileDialog
        NewOpenFile.Filter = "Fichier d'importation (Excel) | *.xls;*.xlsx"
        If NewOpenFile.ShowDialog() = DialogResult.OK Then
            txtFilePathDQE.Text = NewOpenFile.FileName
        End If
    End Sub

    Private Sub BtImportDQE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btImportDQE.Click
        If CmbLotDQE.IsRequiredControl("Veuillez choisir un lot.") Then
            CmbLotDQE.Focus()
            Exit Sub
        End If

        If CmbSousLotDQE.Properties.Items.Count > 0 Then
            If CmbSousLotDQE.IsRequiredControl("Veuillez choisir un sous lot.") Then
                CmbSousLotDQE.Select()
                Exit Sub
            End If
        End If

        If txtFilePathDQE.Text.Trim() = String.Empty Then
            SuccesMsg("Veuillez choisir le fichier à importer.")
            txtFilePathDQE.Select()
            Exit Sub
        End If

        If Not File.Exists(txtFilePathDQE.Text) Then
            FailMsg("Le fichier n'existe pas.")
            Exit Sub
        End If

        ' Vérification du format du fichier
        DebutChargement(True, "Vérification du format du fichier en cours...")
        Dim FileName As String = txtFilePathDQE.Text
        Dim app As New Excel.Application
        app.Workbooks.Open(FileName)

        For i As Integer = 1 To app.Workbooks(1).Worksheets.Count()
            Dim Feuille = app.Workbooks(1).Worksheets(i)
            Dim FeuilleName = Feuille.Name
            Dim ColCount = Feuille.Cells.Find("*", , , , Excel.XlSearchOrder.xlByColumns, Excel.XlSearchDirection.xlPrevious).Column
            Dim RowCount = Feuille.Cells(Feuille.Rows.Count, 1).End(Excel.XlDirection.xlUp).Row

            If RowCount < 4 Then
                app.Quit()
                FinChargement()
                FailMsg("La feuille de calcul """ & FeuilleName & """" & " n'a pas le bon format d'importation.")
                Exit Sub
            End If
            If ColCount < 8 Then 'Vérifier le nombre de colonne
                app.Quit()
                FinChargement()
                FailMsg("La feuille de calcul """ & FeuilleName & """" & " n'a pas le bon format d'importation.")
                Exit Sub
            End If

            Dim Titre As String = Feuille.Range("A4").Value
            Dim NumeroPrix As String = Feuille.Range("B4").Value
            Dim Designation As String = Feuille.Range("C4").Value
            Dim LibUnite As String = Feuille.Range("D4").Value
            Dim LibQuantite As String = Feuille.Range("E4").Value
            Dim LibPrixUnitaire As String = Feuille.Range("G4").Value
            Dim LibMontantTotal As String = Feuille.Range("H4").Value

            If IsNothing(Titre) Or IsNothing(NumeroPrix) Or IsNothing(Designation) Or IsNothing(LibUnite) Or IsNothing(LibQuantite) Or IsNothing(LibPrixUnitaire) Or IsNothing(LibMontantTotal) Then
                app.Quit()
                FinChargement()
                FailMsg("La feuille de calcul """ & FeuilleName & """" & " n'a pas le bon format d'importation.")
                Exit Sub
            End If

            If Titre.ToLower() <> "type" Or NumeroPrix <> "N° de prix" Or Designation.ToUpper <> "DESIGNATIONS" Or LibUnite.ToLower <> "unités" Or LibQuantite.ToLower <> "quantités" Or LibPrixUnitaire <> "Prix Unitaires en FCFA" Or LibMontantTotal <> "Montant total en FCFA" Then
                app.Quit()
                FinChargement()
                FailMsg("La feuille de calcul """ & FeuilleName & """" & " n'a pas le bon format d'importation.")
                Exit Sub
            End If

            Dim Section As String = Feuille.Range("A5").Value

            If IsNothing(Section) Then
                app.Quit()
                FinChargement()
                FailMsg("Le premier élément du tableau doit être une section. A vérifier sur la feuille """ & FeuilleName & """")
                Exit Sub
            End If

            If Section.Trim().Length = 0 Then
                app.Quit()
                FinChargement()
                FailMsg("Le premier élément du tableau doit être une section. A vérifier sur la feuille """ & FeuilleName & """")
                Exit Sub
            End If

            For l = 5 To RowCount
                Dim CurrentType As String = Feuille.Range("A" & l).Value
                Dim CurrentNumero As String = Feuille.Range("B" & l).Value
                Dim CurrentLib As String = Feuille.Range("C" & l).Value
                Dim Unites As String = Feuille.Range("D" & l).Value
                Dim Qtes As String = Feuille.Range("E" & l).Value
                ' Dim Ptus As String = Feuille.Range("G" & l).Value

                If IsNothing(CurrentType) Then
                    app.Quit()
                    FinChargement()
                    FailMsg("La colonne ""Type"" doit être renseigné à la cellule A" & l & " sur la feuille """ & FeuilleName & """" & """")
                    Exit Sub
                End If

                If IsNothing(CurrentNumero) Then
                    app.Quit()
                    FinChargement()
                    FailMsg("La colonne ""N° de prix"" doit être renseigné à la cellule B" & l & " sur la feuille """ & FeuilleName & """")
                    Exit Sub
                End If

                If IsNothing(CurrentLib) Then
                    app.Quit()
                    FinChargement()
                    FailMsg("La colonne ""Designations"" ne peut pas être null à la cellule C" & l & " sur la feuille """ & FeuilleName & """")
                    Exit Sub
                End If

                If l = 5 Then
                    If CurrentType.Trim().ToLower() <> "sec" Then
                        app.Quit()
                        FinChargement()
                        FailMsg("La colonne ""Type"" doit être ""SEC"" à la cellule A" & l & " sur la feuille """ & FeuilleName & """")
                        Exit Sub
                    End If

                    If l = RowCount Then 'Definition d'une section à la dernière ligne
                        app.Quit()
                        FinChargement()
                        FailMsg("Veuillez décrire les elements de la section sur la ligne " & RowCount & " de la feuille """ & FeuilleName & """")
                        Exit Sub
                    Else
                        'Insertion de deux section successive
                        Dim LastSection As String = Feuille.Range("A" & l + 1).Value
                        If LastSection.ToString.Trim().ToLower() <> "elt" Then
                            app.Quit()
                            FinChargement()
                            FailMsg("La colonne ""Type"" doit être ""ELT"" à la cellule A" & l + 1 & " sur la feuille """ & FeuilleName & """")
                            Exit Sub
                        End If
                    End If

                Else
                    If CurrentType.Trim().ToLower() <> "elt" And CurrentType.Trim().ToLower <> "sec" Then
                        app.Quit()
                        FinChargement()
                        FailMsg("""" & CurrentType.Trim() & """ n'est pas un code reconnu pour la colonne ""Type"" à la cellule A" & l & " sur la feuille """ & FeuilleName & """")
                        Exit Sub
                    End If

                    'Verification des sections
                    If CurrentType.Trim().ToLower() = "sec" Then
                        If l = RowCount Then 'Definition d'une section à la dernière ligne
                            app.Quit()
                            FinChargement()
                            FailMsg("Veuillez décrire les elements de la section sur la ligne " & RowCount & " de la feuille """ & FeuilleName & """")
                            Exit Sub
                        Else
                            'Insertion de deux section successive
                            Dim LastSection As String = Feuille.Range("A" & l + 1).Value
                            If LastSection.ToString.Trim().ToLower() <> "elt" Then
                                app.Quit()
                                FinChargement()
                                FailMsg("La colonne ""Type"" doit être ""ELT"" à la cellule A" & l + 1 & " sur la feuille """ & FeuilleName & """")
                                Exit Sub
                            End If
                        End If

                    ElseIf CurrentType.Trim().ToLower() = "elt" Then 'Insertion de deux section successive

                        If IsNothing(Unites) Then
                            app.Quit()
                            FinChargement()
                            FailMsg("La colonne ""Unités"" doit être renseigné à la cellule D" & l & " sur la feuille """ & FeuilleName & """")
                            Exit Sub
                        End If
                        If IsNothing(Qtes) Then
                            app.Quit()
                            FinChargement()
                            FailMsg("La colonne ""Quantités"" doit être renseigné à la cellule E" & l & " sur la feuille """ & FeuilleName & """")
                            Exit Sub
                        End If
                        If Not IsNumeric(Qtes.ToString) Then
                            app.Quit()
                            FinChargement()
                            FailMsg("La colonne ""Quantités"" doit être une valeur numérique à la cellule E" & l & " sur la feuille """ & FeuilleName & """")
                            Exit Sub
                        End If

                        'If IsNothing(Ptus) Then
                        '    app.Quit()
                        '    FinChargement()
                        '    FailMsg("La colonne ""Prix Unitaires en FCFA"" doit être renseigné à la cellule G" & l & " sur la feuille """ & FeuilleName & """")
                        '    Exit Sub
                        'End If
                        'If Not IsNumeric(Ptus.ToString) Then
                        '    app.Quit()
                        '    FinChargement()
                        '    FailMsg("La colonne ""Prix Unitaires en FCFA"" doit être une valeur numérique à la cellule G" & l & " sur la feuille """ & FeuilleName & """")
                        '    Exit Sub
                        'End If
                    End If
                End If
            Next

            'Verification de l'unicité des N° des sections
            For k = 5 To RowCount - 1
                For j = k + 1 To RowCount
                    If Feuille.Range("B" & k).Value = Feuille.Range("B" & j).Value Then
                        app.Quit()
                        FinChargement()
                        FailMsg("Les valeurs inscrites dans la colonne ""N° de prix"" doit être unique à vérifié dans les cellules B" & k & " et B" & j & " sur la feuille """ & FeuilleName & """")
                        Exit Sub
                    End If
                Next
            Next

            FinChargement()
            Dim EraseOldData As Boolean = chkEraseDQE.Checked

            Dim ResultDialog As DialogResult
            If Not EraseOldData Then
                ResultDialog = ConfirmMsg("Vérification terminée." & vbNewLine & "Voulez-vous commencer l'importation ?")
            Else
                ResultDialog = ConfirmMsgWarning("Attention les anciennes données seront supprimées !!!" & vbNewLine & "Voulez-vous importer ce fichier ?")
            End If

            If ResultDialog <> DialogResult.Yes Then
                app.Quit()
                Exit Sub
            End If

            Dim LastIdSection As String = String.Empty
            For l = 5 To RowCount
                Dim CurrentType As String = Feuille.Range("A" & l).Value
                Dim CurrentNumero As String = Feuille.Range("B" & l).Value
                Dim CurrentLib As String = Feuille.Range("C" & l).Value
                Dim Unite As String = String.Empty
                Dim Qte As String = String.Empty
                Dim Pu As String = String.Empty
                Dim MontantTotal As String = String.Empty

                If IsNothing(CurrentType) Then
                    app.Quit()
                    FinChargement()
                    FailMsg("La colonne ""Type"" doit être renseigné à la cellule A" & l & " sur la feuille """ & FeuilleName & """" & """")
                    Exit Sub
                End If

                If IsNothing(CurrentNumero) Then
                    app.Quit()
                    FinChargement()
                    FailMsg("La colonne ""N° de prix"" doit être renseigné à la cellule B" & l & " sur la feuille """ & FeuilleName & """")
                    Exit Sub
                End If

                If IsNothing(CurrentLib) Then
                    app.Quit()
                    FinChargement()
                    FailMsg("La colonne ""Designations"" ne peut pas être null à la cellule C" & l & " sur la feuille """ & FeuilleName & """")
                    Exit Sub
                End If

                If l = 5 Then
                    If CurrentType.Trim().ToLower() <> "sec" Then
                        FinChargement()
                        FailMsg("La colonne ""Type"" doit être ""SEC"" à la cellule A" & l & " sur la feuille """ & FeuilleName & """")
                        app.Quit()
                        Exit Sub
                    End If

                    Try
                        Unite = Feuille.Range("D" & l).Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        Qte = Feuille.Range("E" & l).Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        Pu = Feuille.Range("G" & l).Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        MontantTotal = Feuille.Range("H" & l).Value.ToString()
                    Catch ex As Exception
                    End Try

                    If Qte <> String.Empty And Pu <> String.Empty Then
                        MontantTotal = (Val(Qte) * Val(Pu)).ToString()
                    End If

                    'Enregistement de la section...
                    If EraseOldData Then
                        query = "DELETE FROM t_dqeitem WHERE RefSection IN(SELECT RefSection FROM t_dqesection WHERE NumeroDAO='" & NumDoss & "' AND CodeLot='" & CmbLotDQE.Text & "' AND CodeSousLot='" & CmbSousLotDQE.Text & "')"
                        ExecuteNonQuery(query)
                        query = "DELETE FROM t_dqesection WHERE NumeroDAO='" & NumDoss & "' AND CodeLot='" & CmbLotDQE.Text & "' AND CodeSousLot='" & CmbSousLotDQE.Text & "'"
                        ExecuteNonQuery(query)
                    End If

                    query = "INSERT INTO t_dqesection VALUES(NULL,'" & NumDoss & "','" & CurrentNumero.EnleverApostrophe() & "','" & CurrentLib.EnleverApostrophe() & "','" & CmbLotDQE.Text & "','" & CmbSousLotDQE.Text & "')"
                    ExecuteNonQuery(query)
                    LastIdSection = ExecuteScallar("SELECT MAX(RefSection) FROM t_dqesection WHERE NumeroDAO='" & NumDoss & "' AND CodeLot='" & CmbLotDQE.Text & "' AND CodeSousLot='" & CmbSousLotDQE.Text & "'")
                Else

                    If CurrentType.Trim().ToLower() <> "elt" And CurrentType.Trim().ToLower <> "sec" Then
                        FinChargement()
                        FailMsg("""" & CurrentType.Trim() & """ n'est pas un code reconnu pour la colonne ""Type"" à la cellule A" & l & " sur la feuille """ & FeuilleName & """")
                        app.Quit()
                        Exit Sub
                    End If

                    Try
                        Unite = Feuille.Range("D" & l).Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        Qte = Feuille.Range("E" & l).Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        Pu = Feuille.Range("G" & l).Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        MontantTotal = Feuille.Range("H" & l).Value.ToString()
                    Catch ex As Exception
                    End Try

                    If Qte <> String.Empty And Pu <> String.Empty Then
                        MontantTotal = (Val(Qte) * Val(Pu)).ToString()
                    End If

                    If CurrentType.Trim().ToLower() = "elt" Then
                        'Enregistement de la prevision...
                        ' query = "INSERT INTO t_dqeitem VALUES(NULL,'" & LastIdSection & "','','" & CurrentNumero.EnleverApostrophe() & "','" & CurrentLib.EnleverApostrophe() & "','" & Unite.EnleverApostrophe() & "','" & CDbl(Qte.EnleverApostrophe()) & "','" & CDbl(Pu.EnleverApostrophe()) & "','" & CDbl(MontantTotal.EnleverApostrophe()) & "','" & MontantLettre(Pu.EnleverApostrophe()) & "')"

                        query = "INSERT INTO t_dqeitem VALUES(NULL,'" & LastIdSection & "','','" & CurrentNumero.EnleverApostrophe() & "','" & CurrentLib.EnleverApostrophe() & "','" & Unite.EnleverApostrophe() & "','" & CDbl(Qte.EnleverApostrophe()) & "','','','')"
                        ExecuteNonQuery(query)
                    ElseIf CurrentType.Trim().ToLower() = "sec" Then
                        'Enregistement de la section...
                        query = "INSERT INTO t_dqesection VALUES(NULL,'" & NumDoss & "','" & CurrentNumero.EnleverApostrophe() & "','" & CurrentLib.EnleverApostrophe() & "','" & CmbLotDQE.Text & "','" & CmbSousLotDQE.Text & "')"
                        ExecuteNonQuery(query)
                        LastIdSection = ExecuteScallar("SELECT MAX(RefSection) FROM t_dqesection WHERE NumeroDAO='" & NumDoss & "' AND CodeLot='" & CmbLotDQE.Text & "' AND CodeSousLot='" & CmbSousLotDQE.Text & "'")
                    End If
                End If
            Next
        Next

        app.Quit()
        'GroupControl17.Width = GroupControl13.Width - 4
        'GroupControl17.Left = 2
        ' GroupControl17.BringToFront()
        MajGridDQE()
        FinChargement()
        SuccesMsg("Importation effectuée avec succès.")

#Region "Code Non utiliser"
        ''DebutChargement(True, "Importation des données Excel en cours...")

        ''Dim partFichier() As String = FileName.ToString.Split("."c)
        ''If (partFichier(1).ToLower <> "xlsx" And partFichier(1).ToLower <> "xls") Then
        ''    MsgBox("Ce fichier n'est pas un fichier MS Excel!", MsgBoxStyle.Exclamation)
        ''    Exit Sub
        ''End If

        ''TxtImportDQE.Text = FileName
        ''Dim NomDossier As String = FormatFileName(line & "\DAO\" & TypeMarche & "\" & MethodMarche & "\" & NumDoss, "")
        ''If (Directory.Exists(NomDossier) = True) Then
        ''    File.Copy(FileName, NomDossier & "\FichierDQE_L" & CmbLotDQE.Text & IIf(CmbSousLotDQE.Enabled = True, "SL" & CmbSousLotDQE.Text.Replace(".", ""), "") & "." & partFichier(1), True)
        ''End If

        ''app = New Excel.Application
        ''app.Workbooks.Open(FileName)
        ''For i As Integer = 1 To 4
        ''    If (app.Workbooks(1).Worksheets(1).Cells(2, i).value = Nothing) Then
        ''        MsgBox("Format incorrect!", MsgBoxStyle.Exclamation)
        ''        app.Quit()
        ''        Exit Sub
        ''    End If
        ''Next
        ''If (Mid(app.Workbooks(1).Worksheets(1).Cells(3, 1).value.ToString, 1, 7).ToLower <> "section") Then
        ''    MsgBox("Format incorrect!", MsgBoxStyle.Exclamation)
        ''    app.Quit()
        ''    Exit Sub
        ''End If

        ''Dim LesRef(100) As String
        ''Dim NbSect As Decimal = 0
        ''query = "select RefSection from T_DQESection where NumeroDAO='" & NumDoss & "' and CodeLot='" & CmbLotDQE.Text & "' and CodeSousLot='" & IIf(CmbSousLotDQE.Enabled = True, CmbSousLotDQE.Text, "") & "'"
        ''Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        ''For Each rw As DataRow In dt0.Rows
        ''    LesRef(NbSect) = rw(0).ToString
        ''    NbSect += 1
        ''Next

        ''For k As Integer = 0 To NbSect - 1
        ''    query = "DELETE from T_DQEItem where RefSection='" & LesRef(k) & "'"
        ''    ExecuteNonQuery(query)

        ''    query = "DELETE from T_DQESection_SousSection where RefSection='" & LesRef(k) & "'"
        ''    ExecuteNonQuery(query)

        ''    query = "DELETE from T_DQESection where RefSection='" & LesRef(k) & "'"
        ''    ExecuteNonQuery(query)
        ''Next

        ''Dim SectionEncours As Decimal = 0
        ''Dim SSenCours As String = ""
        ''Dim MarkSection As Boolean = False
        ''Dim YaSousSect As Boolean = False
        ''Dim sqlconn As New MySqlConnection
        ''BDOPEN(sqlconn)
        ''For LigNe As Integer = 3 To 1000
        ''    If (app.Workbooks(1).Worksheets(1).Cells(LigNe, 1).value = Nothing) Then MsgBox("Fin du fichier à la ligne " & LigNe.ToString, MsgBoxStyle.Information) : Exit For
        ''    Dim partTyp() As String = app.Workbooks(1).Worksheets(1).Cells(LigNe, 1).value.ToString.Split(" "c)
        ''    If (partTyp(0).ToLower = "section") Then   'Pour les sections ***********************
        ''        If (app.Workbooks(1).Worksheets(1).Cells(LigNe, 2).value = Nothing) Then MsgBox("Format incorrect! (ligne " & LigNe.ToString & ")", MsgBoxStyle.Exclamation) : Exit For
        ''        If (partTyp(1) <> "") Then
        ''            Dim DatSet = New DataSet
        ''            query = "select * from T_DQESection"

        ''            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        ''            Dim DatAdapt = New MySqlDataAdapter(Cmd)
        ''            DatAdapt.Fill(DatSet, "T_DQESection")
        ''            Dim DatTable = DatSet.Tables("T_DQESection")
        ''            Dim DatRow = DatSet.Tables("T_DQESection").NewRow()

        ''            DatRow("NumeroDAO") = NumDoss
        ''            DatRow("NumeroSection") = partTyp(1)
        ''            DatRow("Designation") = EnleverApost(app.Workbooks(1).Worksheets(1).Cells(LigNe, 2).value.ToString)
        ''            DatRow("CodeLot") = CmbLotDQE.Text
        ''            DatRow("CodeSousLot") = IIf(CmbSousLotDQE.Enabled = True, CmbSousLotDQE.Text, "").ToString

        ''            DatSet.Tables("T_DQESection").Rows.Add(DatRow)
        ''            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        ''            DatAdapt.Update(DatSet, "T_DQESection")
        ''            DatSet.Clear()

        ''            query = "select RefSection from T_DQESection where NumeroDAO='" & NumDoss & "' and CodeLot='" & CmbLotDQE.Text & "' and NumeroSection='" & partTyp(1) & "'"
        ''            dt0 = ExcecuteSelectQuery(query)
        ''            For Each rw As DataRow In dt0.Rows
        ''                SectionEncours = CInt(rw(0))
        ''                MarkSection = True
        ''            Next

        ''        Else
        ''            MsgBox("Importation interrompue! (ligne " & LigNe.ToString & ")", MsgBoxStyle.Exclamation)
        ''            app.Quit()
        ''            Exit For
        ''        End If

        ''    ElseIf (partTyp(0).ToLower = "sous") Then  'Pour les sous sections ****************

        ''        If (MarkSection = True) Then
        ''            YaSousSect = True
        ''            MarkSection = False
        ''        End If

        ''        If (YaSousSect = True) Then
        ''            If (app.Workbooks(1).Worksheets(1).Cells(LigNe, 2).value = Nothing) Then MsgBox("Format incorrect! (ligne " & LigNe.ToString & ")", MsgBoxStyle.Exclamation) : Exit For
        ''            If (partTyp(2) <> "") Then
        ''                Dim DatSet = New DataSet
        ''                query = "select * from T_DQESection_SousSection"

        ''                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        ''                Dim DatAdapt = New MySqlDataAdapter(Cmd)
        ''                DatAdapt.Fill(DatSet, "T_DQESection_SousSection")
        ''                Dim DatTable = DatSet.Tables("T_DQESection_SousSection")
        ''                Dim DatRow = DatSet.Tables("T_DQESection_SousSection").NewRow()

        ''                DatRow("RefSection") = SectionEncours.ToString
        ''                DatRow("NumeroDAO") = NumDoss
        ''                DatRow("NumeroSousSection") = partTyp(2)
        ''                DatRow("LibelleSousSection") = EnleverApost(app.Workbooks(1).Worksheets(1).Cells(LigNe, 2).value.ToString)

        ''                DatSet.Tables("T_DQESection_SousSection").Rows.Add(DatRow)
        ''                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        ''                DatAdapt.Update(DatSet, "T_DQESection_SousSection")
        ''                DatSet.Clear()

        ''            Else
        ''                MsgBox("Importation interrompue! (ligne " & LigNe.ToString & ")", MsgBoxStyle.Exclamation)
        ''                app.Quit()
        ''                Exit Sub
        ''            End If

        ''        Else
        ''            MsgBox("Disposition données incorrecte! (ligne " & LigNe.ToString & ")", MsgBoxStyle.Exclamation)
        ''            MsgBox("Importation interrompue! (ligne " & LigNe.ToString & ")", MsgBoxStyle.Exclamation)
        ''            Exit For
        ''        End If

        ''    Else   'Pour les items ************************

        ''        If (MarkSection = True) Then
        ''            YaSousSect = False
        ''            MarkSection = False
        ''        End If

        ''        If (partTyp(0) <> "") Then
        ''            If (app.Workbooks(1).Worksheets(1).Cells(LigNe, 3).value <> Nothing Or app.Workbooks(1).Worksheets(1).Cells(LigNe, 4).value <> Nothing) Then
        ''                Dim DatSet = New DataSet
        ''                query = "select * from T_DQEItem"

        ''                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        ''                Dim DatAdapt = New MySqlDataAdapter(Cmd)
        ''                DatAdapt.Fill(DatSet, "T_DQEItem")
        ''                Dim DatTable = DatSet.Tables("T_DQEItem")
        ''                Dim DatRow = DatSet.Tables("T_DQEItem").NewRow()

        ''                DatRow("RefSection") = SectionEncours.ToString
        ''                DatRow("NumeroItem") = partTyp(0)
        ''                DatRow("Designation") = EnleverApost(app.Workbooks(1).Worksheets(1).Cells(LigNe, 2).value.ToString)
        ''                DatRow("NumeroSousSection") = SSenCours

        ''                If (app.Workbooks(1).Worksheets(1).Cells(LigNe, 3).value <> Nothing) Then
        ''                    DatRow("UniteItem") = IIf(app.Workbooks(1).Worksheets(1).Cells(LigNe, 3).value.ToString.Replace(" ", "") <> "", app.Workbooks(1).Worksheets(1).Cells(LigNe, 3).value.ToString, "F").ToString
        ''                Else
        ''                    DatRow("UniteItem") = "F"
        ''                End If
        ''                Dim Qte As Decimal = 1
        ''                If (app.Workbooks(1).Worksheets(1).Cells(LigNe, 4).value <> Nothing) Then
        ''                    If (app.Workbooks(1).Worksheets(1).Cells(LigNe, 4).value.ToString.Replace(" ", "") <> "") Then
        ''                        If (IsNumeric(app.Workbooks(1).Worksheets(1).Cells(LigNe, 4).value.ToString.Replace(" ", "")) = True) Then
        ''                            DatRow("QteItem") = CDec(app.Workbooks(1).Worksheets(1).Cells(LigNe, 4).value.ToString.Replace(" ", "")).ToString
        ''                            Qte = CDec(app.Workbooks(1).Worksheets(1).Cells(LigNe, 4).value.ToString.Replace(" ", "")).ToString
        ''                        Else
        ''                            DatSet.Clear()

        ''                            MsgBox("Quantité non numérique! (ligne " & LigNe.ToString & ")", MsgBoxStyle.Exclamation)
        ''                            Exit For
        ''                        End If

        ''                    Else
        ''                        DatRow("QteItem") = "1"
        ''                    End If
        ''                Else
        ''                    DatRow("QteItem") = "1"
        ''                End If
        ''                If (app.Workbooks(1).Worksheets(1).Cells(LigNe, 5).value <> Nothing) Then
        ''                    If (IsNumeric(app.Workbooks(1).Worksheets(1).Cells(LigNe, 5).value) = True And app.Workbooks(1).Worksheets(1).Cells(LigNe, 5).value.ToString.Replace(" ", "") <> "") Then
        ''                        DatRow("PuHtva") = AfficherMonnaie(CDec(app.Workbooks(1).Worksheets(1).Cells(LigNe, 5).value.ToString.Replace(" ", "")).ToString)
        ''                        DatRow("PuHtvaLettre") = MontantLettre(CDec(app.Workbooks(1).Worksheets(1).Cells(LigNe, 5).value.ToString.Replace(" ", "")).ToString)
        ''                        DatRow("MontHtva") = AfficherMonnaie(Math.Round(CDec(app.Workbooks(1).Worksheets(1).Cells(LigNe, 5).value.ToString.Replace(" ", "")) * Qte, 2).ToString)
        ''                    ElseIf (IsNumeric(app.Workbooks(1).Worksheets(1).Cells(LigNe, 5).value) = False And app.Workbooks(1).Worksheets(1).Cells(LigNe, 5).value.ToString.Replace(" ", "") <> "") Then
        ''                        MsgBox("Prix unitaire ligne " & LigNe.ToString & " : " & app.Workbooks(1).Worksheets(1).Cells(LigNe, 5).value.ToString & " incorrect!", MsgBoxStyle.Exclamation)
        ''                    End If
        ''                End If

        ''                DatSet.Tables("T_DQEItem").Rows.Add(DatRow)
        ''                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        ''                DatAdapt.Update(DatSet, "T_DQEItem")
        ''                DatSet.Clear()


        ''            Else
        ''                MsgBox("La ligne " & LigNe.ToString & " ne sera pas prise en compte, elle n'a ni unité ni quantité!", MsgBoxStyle.Exclamation)
        ''            End If

        ''        End If
        ''    End If
        ''Next
        ''BDQUIT(sqlconn)
        ''GroupControl17.Width = GroupControl13.Width - 4
        ''GroupControl17.Left = 2
        ''GroupControl17.BringToFront()
        ''MajGridDQE()
        ''app.Quit()
        ''FinChargement()
#End Region
    End Sub

    Public Sub MajGridDQE()
        Dim NumSection As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim cptr As Decimal = 0
        DataGridView1.Rows.Clear()
        CmbNumSection.Properties.Items.Clear()

        query = "select RefSection,NumeroSection,Designation from T_DQESection where NumeroDAO='" & NumDoss & "' and CodeLot='" & CmbLotDQE.Text & "' and CodeSousLot='" & CmbSousLotDQE.Text & "' order by NumeroSection"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr = cptr + 1
            Dim RefSect As Decimal = rw("RefSection")
            CmbNumSection.Properties.Items.Add(rw("NumeroSection").ToString)

            Dim n As Decimal = DataGridView1.Rows.Add()
            DataGridView1.Rows.Item(n).Cells(0).Value = "S" & rw("RefSection").ToString
            DataGridView1.Rows.Item(n).Cells(1).Value = "SECTION " & rw("NumeroSection").ToString
            DataGridView1.Rows.Item(n).Cells(2).Value = MettreApost(rw("Designation").ToString)
            DataGridView1.Rows.Item(n).DefaultCellStyle.BackColor = Color.LightBlue

            For i As Integer = 1 To 2
                DataGridView1.Rows.Item(n).Cells(i).Style.Font = New Font("Tahoma", 9, FontStyle.Bold)
            Next

            Dim NbSS As Decimal = 0

            query = "select Count(*) from T_DQESection_SousSection where RefSection='" & rw("RefSection").ToString & "' and NumeroDAO='" & NumDoss & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw1 As DataRow In dt1.Rows
                NbSS = CInt(rw1(0))
            Next

            If (NbSS > 1) Then

                query = "select RefSousSection,NumeroSousSection,LibelleSousSection from T_DQESection_SousSection where RefSection='" & RefSect.ToString & "' order by NumeroSousSection"
                dt1 = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows
                    Dim z As Decimal = DataGridView1.Rows.Add()
                    DataGridView1.Rows.Item(z).Cells(0).Value = "X" & rw1(0).ToString
                    DataGridView1.Rows.Item(z).Cells(1).Value = "S/S " & rw1(1).ToString
                    DataGridView1.Rows.Item(z).Cells(2).Value = MettreApost(rw1(2).ToString)

                    For i As Integer = 1 To 2
                        DataGridView1.Rows.Item(z).Cells(i).Style.Font = New Font("Tahoma", 8, FontStyle.Bold)
                    Next

                    query = "select RefItem,NumeroItem,Designation,UniteItem,QteItem,PuHtva,MontHtva from T_DQEItem where RefSection='" & RefSect.ToString & "' and NumeroSousSection='" & rw1(1).ToString & "' order by NumeroItem"
                    Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw2 As DataRow In dt2.Rows
                        Dim x As Decimal = DataGridView1.Rows.Add()
                        DataGridView1.Rows.Item(x).Cells(0).Value = "I" & rw2(0).ToString
                        DataGridView1.Rows.Item(x).Cells(1).Value = rw2(1).ToString
                        DataGridView1.Rows.Item(x).Cells(2).Value = MettreApost(rw2(2).ToString)
                        DataGridView1.Rows.Item(x).Cells(3).Value = rw2(3).ToString
                        DataGridView1.Rows.Item(x).Cells(4).Value = AfficherMonnaie(rw2(4).ToString)
                        DataGridView1.Rows.Item(x).Cells(5).Value = AfficherMonnaie(rw2(5).ToString)
                        DataGridView1.Rows.Item(x).Cells(6).Value = AfficherMonnaie(rw2(6).ToString)
                    Next
                Next
            Else

                query = "select RefItem,NumeroItem,Designation,UniteItem,QteItem,PuHtva,MontHtva from T_DQEItem where RefSection='" & RefSect.ToString & "' order by NumeroItem"
                dt1 = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows
                    Dim x As Decimal = DataGridView1.Rows.Add()
                    DataGridView1.Rows.Item(x).Cells(0).Value = "I" & rw1("RefItem").ToString
                    DataGridView1.Rows.Item(x).Cells(1).Value = rw1("NumeroItem").ToString
                    DataGridView1.Rows.Item(x).Cells(2).Value = MettreApost(rw1("Designation").ToString)
                    DataGridView1.Rows.Item(x).Cells(3).Value = rw1("UniteItem").ToString
                    DataGridView1.Rows.Item(x).Cells(4).Value = AfficherMonnaie(rw1("QteItem").ToString)
                    DataGridView1.Rows.Item(x).Cells(5).Value = AfficherMonnaie(rw1("PuHtva").ToString)
                    DataGridView1.Rows.Item(x).Cells(6).Value = AfficherMonnaie(rw1("MontHtva").ToString)
                Next
            End If
        Next

        'DataGridView1.Columns(2).Width = DataGridView1.Width - (DataGridView1.Columns(0).Width + DataGridView1.Columns(1).Width + DataGridView1.Columns(3).Width + DataGridView1.Columns(4).Width + DataGridView1.Columns(5).Width + DataGridView1.Columns(6).Width)

        'TxtNumSection.Text = NumSection(cptr)

        'If (GbItemDQE.Visible = True) Then
        '    TxtDesigneItem.Focus()
        'Else
        '    TxtDesigneSection.Focus()
        'End If

        'MajCmbUnite()
    End Sub

    Private Sub LesSousLots(ByVal Lot As String, ByRef Combo As ComboBoxEdit)
        If (Lot <> "" And Combo.Enabled = True) Then
            Combo.Properties.Items.Clear()
            query = "select CodeSousLot from T_LotDAO_SousLot where RefLot='" & Lot & "' and NumeroDAO='" & NumDoss & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                Combo.Properties.Items.Add(rw("CodeSousLot").ToString)
            Next
        End If
    End Sub

    Private Sub ToolStripMenuItemModifLignDQE_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemModifLignDQE.Click
        Try
            If DataGridView1.Rows.Count > 0 Then
                Dim Index = DataGridView1.CurrentRow.Index
                Dim NewModifLigneDQE As New ModifLigneDQE
                NewModifLigneDQE.RefSection = Val(Mid(DataGridView1.Rows.Item(Index).Cells(0).Value, 2))
                If Mid(DataGridView1.Rows.Item(Index).Cells(0).Value, 1, 1) = "I" Then
                    NewModifLigneDQE.TypeModification = ""
                Else
                    NewModifLigneDQE.TypeModification = "Section"
                    NewModifLigneDQE.Size = New Point(413, 124)
                    NewModifLigneDQE.Label2.Visible = False
                    NewModifLigneDQE.NumQteBien.Visible = False
                    NewModifLigneDQE.Label1.Visible = False
                    NewModifLigneDQE.Unites.Visible = False
                    NewModifLigneDQE.Label3.Visible = False
                    NewModifLigneDQE.PrixUnitaire.Visible = False
                    NewModifLigneDQE.Label5.Visible = False
                    NewModifLigneDQE.MontantTotal.Visible = False
                    NewModifLigneDQE.Label6.Visible = False
                    NewModifLigneDQE.Label7.Visible = False
                End If
                NewModifLigneDQE.Designation.Text = DataGridView1.Rows.Item(Index).Cells(2).Value
                NewModifLigneDQE.NumQteBien.Text = DataGridView1.Rows.Item(Index).Cells(4).Value
                NewModifLigneDQE.Unites.Text = DataGridView1.Rows.Item(Index).Cells(3).Value
                ' NewModifLigneDQE.PrixUnitaire.Text = DataGridView1.Rows.Item(Index).Cells(5).Value
                NewModifLigneDQE.ShowDialog()
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub
    Private Sub DataGridView1_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDown
        NewGridLigneSelected(DataGridView1, e)
    End Sub

    Private Sub ContextMenuStriptDQE_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStriptDQE.Opening
        If AfficherDossier = True Or DataGridView1.Rows.Count = 0 Then
            e.Cancel = True
        End If
    End Sub

#Region "Code saisie manuel des DQE"
    Private Sub RdItem_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdItem.CheckedChanged
        If (RdItem.Checked = True) Then
            GbItemDQE.Visible = True
        Else
            GbItemDQE.Visible = False
            TxtDesigneSection.Focus()
        End If
    End Sub

    Private Sub CmbNumLot2_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumLot2.SelectedValueChanged
        If (CmbNumLot2.Text <> "") Then
            query = "select LibelleLot,RefLot,SousLot from T_LotDAO where NumeroDAO='" & NumDoss & "' and CodeLot='" & CmbNumLot2.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtRefLot1.Text = rw(1).ToString
                If (rw(2).ToString = "OUI") Then
                    CmbSousLotDQE.Enabled = True
                    CmbSousLot1.Enabled = True
                Else
                    CmbSousLotDQE.Enabled = False
                    CmbSousLot1.Enabled = False
                End If
            Next

            CmbNumSection.Text = ""
            TxtSection.Text = ""
            TxtNumItem.Text = ""

            If (CmbSousLot1.Enabled = False) Then
                GbSectionDQE.Enabled = True
                GbItemDQE.Enabled = True
            Else
                GbSectionDQE.Enabled = False
                GbItemDQE.Enabled = False
                LesSousLots(TxtRefLot1.Text, CmbSousLotDQE)
                LesSousLots(TxtRefLot1.Text, CmbSousLot1)
            End If
            MajGridDQE()

            CmbLotDQE.Text = CmbNumLot2.Text
        Else
            GbSectionDQE.Enabled = False
            GbItemDQE.Enabled = False
        End If
    End Sub

    ' Private Sub CmbLotDQE_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbLotDQE.SelectedValueChanged
    'TxtRefLot1.Text = ""
    'CmbSousLotDQE.Text = ""
    'CmbSousLot1.Text = ""
    'TxtSousLotDQE.Text = ""

    'If (CmbLotDQE.Text <> "") Then
    '    query = "select LibelleLot,RefLot,SousLot from T_LotDAO where NumeroDAO='" & NumDoss & "' and CodeLot='" & CmbLotDQE.Text & "'"
    '    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
    '    For Each rw As DataRow In dt0.Rows
    '        txtLibLotDQE.Text = MettreApost(rw(0).ToString)
    '        TxtRefLot1.Text = rw(1).ToString
    '        If (rw(2).ToString = "OUI") Then
    '            CmbSousLotDQE.Enabled = True
    '            CmbSousLot1.Enabled = True
    '        Else
    '            CmbSousLotDQE.Enabled = False
    '            CmbSousLot1.Enabled = False
    '        End If
    '    Next

    '    If (CmbSousLotDQE.Enabled = False) Then
    '        'btOpenDQE.Enabled = True
    '    Else
    '        'btOpenDQE.Enabled = False
    '        LesSousLots(TxtRefLot1.Text, CmbSousLotDQE)
    '        LesSousLots(TxtRefLot1.Text, CmbSousLot1)
    '    End If

    '    CmbNumLot2.Text = CmbLotDQE.Text
    'Else
    '    'btOpenDQE.Enabled = False
    'End If
    'MajGridDQE()
    ' End Sub

    Private Sub CmbSousLot1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSousLot1.SelectedValueChanged

        If (CmbSousLot1.Text <> "") Then
            CmbSousLotDQE.Text = CmbSousLot1.Text

            GbSectionDQE.Enabled = True
            GbItemDQE.Enabled = True
        End If

    End Sub

    Private Sub ChkSousSection_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkSousSection.CheckedChanged

        If (ChkSousSection.Checked = True) Then
            GbSousSection.Visible = True
        Else
            GbSousSection.Visible = False
        End If

    End Sub

    Private Sub TxtDesigneSection_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtDesigneSection.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            BtEnrgSection_Click(Me, e)
        End If
    End Sub

    Private Sub CmbNumSection_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumSection.SelectedValueChanged
        If (CmbNumSection.Text <> "") Then
            Dim refSection As Decimal = 0
            query = "select RefSection,Designation from T_DQESection where NumeroDAO='" & NumDoss & "' and CodeLot='" & CmbNumLot2.Text & "' and NumeroSection='" & CmbNumSection.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                refSection = CInt(rw(0))
                TxtSection.Text = MettreApost(rw(1).ToString)
            Next

            Dim NbSS As Decimal = 0
            query = "select Count(*) from T_DQESection_SousSection where RefSection='" & refSection & "' and NumeroDAO='" & NumDoss & "'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                NbSS = CInt(rw(0))
            Next

            If (NbSS > 1) Then
                CmbSousSection.Enabled = True
                LesSousSection(refSection.ToString)
            Else
                CmbSousSection.Enabled = False
                TxtDesigneItem.Focus()
            End If


            Dim nbreItem As Decimal = 0
            query = "select Count(*) from T_DQEItem where RefSection='" & refSection & "'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                nbreItem = CInt(rw(0))
            Next

            Dim codeItem As String = (nbreItem + 1).ToString
            If (nbreItem <10) Then codeItem = "0" & codeItem
            TxtNumItem.Text = CmbNumSection.Text & codeItem
            RefSectionItemCache.Text = refSection.ToString

        End If
    End Sub

    Private Sub CmbSousSection_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSousSection.SelectedValueChanged

        If (CmbSousSection.Text <> "") Then
            query = "select LibelleSousSection from T_DQESection_SousSection where NumeroDAO='" & NumDoss & "' and NumeroSousSection='" & CmbSousSection.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows

                TxtSousSection.Text = MettreApost(rw(0).ToString)
                TxtDesigneItem.Focus()
            Next
        End If

    End Sub

    Private Sub LesSousSection(ByVal Sect As String)
        CmbSousSection.Properties.Items.Clear()
        query = "select NumeroSousSection from T_DQESection_SousSection where NumeroDAO='" & NumDoss & "' and RefSection='" & Sect & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbSousSection.Properties.Items.Add(rw(0).ToString)
        Next
    End Sub

    Private Sub MajCmbUnite()
        CmbUnite.Properties.Items.Clear()
        CmbUniteBien.Properties.Items.Clear()
        query = "select LibelleCourtUnite from T_Unite"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbUnite.Properties.Items.Add(rw("LibelleCourtUnite").ToString)
            CmbUniteBien.Properties.Items.Add(rw("LibelleCourtUnite").ToString)
        Next
        CmbUnite.Properties.Items.Add("...")
    End Sub

    Private Sub CmbUnite_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbUnite.SelectedValueChanged
        query = "select LibelleUnite from T_Unite where LibelleCourtUnite='" & CmbUnite.Text & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            TxtUnite.Text = MettreApost(rw("LibelleUnite").ToString)
            TxtQte.Focus()
        Next
    End Sub

    Private Sub TxtQte_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtQte.KeyDown
        If (e.KeyCode = Keys.Enter And TxtQte.Text <> "") Then
            TxtPunit.Focus()
        End If
    End Sub

    Private Sub TxtQte_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtQte.TextChanged
        If (TxtQte.Text <> "") Then
            TxtQte.Text = AfficherMonnaie(TxtQte.Text.Replace(" ", ""))
        End If
    End Sub

    Private Sub TxtPunit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPunit.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            If (TxtPunit.Text = "") Then
                BtEnrgItem.Focus()
            Else
                BtEnrgItem_Click(Me, e)
            End If
        End If
    End Sub

    Private Sub TxtPunit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPunit.TextChanged
        If (TxtPunit.Text <> "") Then
            TxtPunit.Text = AfficherMonnaie(TxtPunit.Text.Replace(" ", ""))
        End If
    End Sub

    Private Sub BtEnrgSection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgSection.Click
        If (TxtNumSection.Text <> "" And TxtDesigneSection.Text <> "") Then
            Dim DatSet = New DataSet
            query = "select * from T_DQESection"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_DQESection")
            Dim DatTable = DatSet.Tables("T_DQESection")
            Dim DatRow = DatSet.Tables("T_DQESection").NewRow()

            DatRow("NumeroDAO") = NumDoss
            DatRow("NumeroSection") = TxtNumSection.Text
            DatRow("Designation") = EnleverApost(TxtDesigneSection.Text)
            DatRow("CodeLot") = CmbNumLot2.Text
            If (CmbSousLot1.Text <> "") Then DatRow("CodeSousLot") = CmbSousLot1.Text

            DatSet.Tables("T_DQESection").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_DQESection")
            DatSet.Clear()

            If (ChkSousSection.Checked = True) Then

                Dim DernRef As String = ""
                query = "select RefSection from T_DQESection where NumeroSection='" & TxtNumSection.Text & "' and NumeroDAO='" & NumDoss & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    DernRef = rw(0).ToString
                Next

                Dim NumSS() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}

                For k As Integer = 0 To GridSousSection.RowCount - 1

                    If (GridSousSection.Rows(k).Cells(0).Value <> Nothing) Then

                        If (GridSousSection.Rows(k).Cells(0).Value.ToString.Replace(" ", "") <> "") Then

                            DatSet = New DataSet
                            query = "select * from T_DQESection_SousSection"

                            Cmd = New MySqlCommand(query, sqlconn)
                            DatAdapt = New MySqlDataAdapter(Cmd)
                            DatAdapt.Fill(DatSet, "T_DQESection_SousSection")
                            DatTable = DatSet.Tables("T_DQESection_SousSection")
                            DatRow = DatSet.Tables("T_DQESection_SousSection").NewRow()

                            DatRow("RefSection") = DernRef
                            DatRow("NumeroDAO") = NumDoss
                            DatRow("NumeroSousSection") = TxtNumSection.Text & "." & NumSS(k)
                            DatRow("LibelleSousSection") = EnleverApost(GridSousSection.Rows(k).Cells(0).Value.ToString)

                            DatSet.Tables("T_DQESection_SousSection").Rows.Add(DatRow)
                            CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                            DatAdapt.Update(DatSet, "T_DQESection_SousSection")
                            DatSet.Clear()


                        End If

                    End If

                Next
            End If
            BDQUIT(sqlconn)

            TxtDesigneSection.Text = ""

            MajGridDQE()

        End If
    End Sub

    Private Sub BtEnrgItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgItem.Click
        If (CmbNumSection.Text <> "" And TxtDesigneItem.Text <> "" And CmbUnite.Text <> "" And TxtQte.Text <> "") Then
            Dim DatSet = New DataSet
            query = "select * from T_DQEItem"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_DQEItem")
            Dim DatTable = DatSet.Tables("T_DQEItem")
            Dim DatRow = DatSet.Tables("T_DQEItem").NewRow()

            DatRow("RefSection") = RefSectionItemCache.Text
            DatRow("NumeroItem") = TxtNumItem.Text
            DatRow("Designation") = EnleverApost(TxtDesigneItem.Text)
            DatRow("UniteItem") = CmbUnite.Text
            DatRow("QteItem") = TxtQte.Text.Replace(" ", "")
            If (TxtPunit.Text <> "") Then
                DatRow("PuHtva") = AfficherMonnaie(TxtPunit.Text.Replace(" ", ""))
                DatRow("PuHtvaLettre") = MontantLettre(TxtPunit.Text.Replace(" ", ""))
                DatRow("MontHtva") = AfficherMonnaie(Math.Round(CDec(TxtQte.Text.Replace(" ", "")) * CDec(TxtPunit.Text.Replace(" ", "")), 2).ToString)
            End If
            If (CmbSousSection.Enabled = True) Then DatRow("NumeroSousSection") = CmbSousSection.Text

            DatSet.Tables("T_DQEItem").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_DQEItem")
            DatSet.Clear()


            TxtDesigneItem.Text = ""
            CmbUnite.Text = ""
            TxtUnite.Text = ""
            TxtQte.Text = ""
            TxtPunit.Text = ""
            CmbNumSection_SelectedValueChanged(Me, e)
            MajGridDQE()

            BDQUIT(sqlconn)
        End If
    End Sub
#End Region

#End Region

#Region "Post Qualification"
    Private Sub BtAnnulPostQualif_Click(sender As Object, e As EventArgs) Handles BtAnnulPostQualif.Click
        initChampPostQualif()
    End Sub

    Private Sub initChampPostQualif()
        TxtCriterePost.Text = ""
        txtGroupe.Text = ""
        ActiveControl = txtGroupe
        modifPostQualif = False
        ChkCritElimine.Enabled = True
        TxtCriterePost.Enabled = True
        txtGroupe.Enabled = True
        ChkCritElimine.Checked = False

    End Sub

    Private Sub ToolStripMenuModifierPostQualif_Click(sender As Object, e As EventArgs) Handles ToolStripMenuModifierPostQualif.Click
        If ListePostQualif.Nodes.Count > 0 Then
            NodeModPost = ListePostQualif.FocusedNode
            Dim node1 As TreeListNode = ListePostQualif.FocusedNode.ParentNode
            If Not NodeModPost.ParentNode Is Nothing Then
                modifPostQualif = True
                TxtCriterePost.Enabled = True
                ChkCritElimine.Enabled = True
                txtGroupe.Enabled = False
                TxtCriterePost.Text = NodeModPost.GetValue("Description").ToString.Trim()
                txtGroupe.Text = node1.GetValue("Description").ToString
                If NodeModPost.GetValue("Eliminatoire").ToString = "OUI" Then
                    ChkCritElimine.Checked = True
                Else
                    ChkCritElimine.Checked = False
                End If
            Else
                TxtCriterePost.Text = ""
                txtGroupe.Enabled = True
                modifPostQualif = True
                TxtCriterePost.Enabled = False
                ChkCritElimine.Enabled = False
                ChkCritElimine.Checked = False
                txtGroupe.Text = ListePostQualif.FocusedNode.GetValue("Description")
            End If
        End If
    End Sub

    Private Sub ToolStripMenuSupprimerPostQualif_Click(sender As Object, e As EventArgs) Handles ToolStripMenuSupprimerPostQualif.Click
        If ListePostQualif.Nodes.Count > 0 Then
            Dim node As TreeListNode = ListePostQualif.FocusedNode
            Dim node1 As TreeListNode = ListePostQualif.FocusedNode.ParentNode
            If Not node.ParentNode Is Nothing Then
                CodePostQualifSup.Add(node.GetValue("IdCol").ToString)
                node.ParentNode.Nodes.Remove(node)
                If Not node1.HasChildren Then
                    CodePostQualifSup.Add(node1.GetValue("IdCol").ToString)
                    ListePostQualif.DeleteNode(node1)
                End If
            Else
                If ConfirmMsg("Voulez-vous supprimer ce groupe avec ses critères ?") = DialogResult.Yes Then

                    CodePostQualifSup.Add(node.GetValue("IdCol").ToString)
                    For i = 0 To ListePostQualif.Nodes.Count - 1
                        If ListePostQualif.Nodes(i).GetValue("IdCol").ToString = node.GetValue("IdCol").ToString Then
                            For j = 0 To ListePostQualif.Nodes(i).Nodes.Count - 1
                                CodePostQualifSup.Add(ListePostQualif.Nodes(i).Nodes(j).GetValue("IdCol").ToString)
                            Next
                            Exit For
                        End If
                    Next
                    ListePostQualif.Nodes.Remove(node)
                End If
            End If
        End If

    End Sub


    Private Sub InitPostQualif()

        Dim dt As DataTable = GridPostQualif.DataSource
        dt.Rows.Clear()
        ListePostQualif.Nodes.Clear()
        txtGroupe.ResetText()
        TxtCriterePost.ResetText()
        ChkCritElimine.Checked = False
        CodePostQualifSup.Clear()
    End Sub

    Private Sub BtEnrgPostQualif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgPostQualif.Click
        If modifPostQualif = False Then
            If txtGroupe.IsRequiredControl("Veuillez saisir un groupe") Then
                txtGroupe.Select()
                Exit Sub
            End If
            If TxtCriterePost.IsRequiredControl("Veuillez saisir un critère") Then
                TxtCriterePost.Select()
                Exit Sub
            End If

            'Dim dtExamen As DataTable = GridPostQualif.DataSource
            'Dim drS As DataRow = dtExamen.NewRow
            'Dim Index As Integer = IsSavedItemInGridView(txtGroupe.Text.ToLower, ViewPostQualif, "Description")
            'If Index = -1 Then
            '    drS("Id") = "##"
            '    drS("Code") = "G"
            '    drS("Description") = txtGroupe.Text.Trim()
            '    drS("Eliminatoire") = ""
            '    drS("Groupe") = ""
            '    dtExamen.Rows.Add(drS)

            '    drS = dtExamen.NewRow
            '    drS("Id") = "##"
            '    drS("Code") = ""
            '    drS("Description") = "      " & TxtCriterePost.Text.Trim()
            '    drS("Eliminatoire") = IIf(ChkCritElimine.Checked = True, "OUI", "NON").ToString
            '    drS("Groupe") = txtGroupe.Text.Trim()
            '    dtExamen.Rows.Add(drS)
            'Else
            '    drS("Id") = "##"
            '    drS("Code") = ""
            '    drS("Description") = "      " & TxtCriterePost.Text.Trim()
            '    drS("Eliminatoire") = IIf(ChkCritElimine.Checked = True, "OUI", "NON").ToString
            '    drS("Groupe") = txtGroupe.Text.Trim()
            '    dtExamen.Rows.InsertAt(drS, (Index + 1))
            'End If
            'ColorRowGridAnal(ViewPostQualif, "[Code]='G'", Color.Navy, "Tahoma", 9, FontStyle.Bold, Color.White, True)

            For i = 0 To ListePostQualif.Nodes.Count - 1
                If ListePostQualif.Nodes(i).Item("Code") = "G" Then
                    If ListePostQualif.Nodes(i).Item("Description").ToString.ToLower = txtGroupe.Text.Trim().ToLower Then
                        Dim CatNode As TreeListNode = ListePostQualif.Nodes(i)
                        ListePostQualif.AppendNode(New Object() {"##", "", "      " & MettreApost(TxtCriterePost.Text.Trim()), IIf(ChkCritElimine.Checked = True, "OUI", "NON").ToString}, CatNode)
                        TxtCriterePost.ResetText()
                        TxtCriterePost.Focus()
                        Exit Sub
                    End If
                End If
            Next

            Dim parentForRootNodes As TreeListNode = Nothing
            Dim rootNode As TreeListNode = ListePostQualif.AppendNode(New Object() {"##", "G", MettreApost(txtGroupe.Text), ""}, parentForRootNodes)
            ListePostQualif.AppendNode(New Object() {"##", "", "      " & MettreApost(TxtCriterePost.Text.Trim()), IIf(ChkCritElimine.Checked = True, "OUI", "NON").ToString}, rootNode)
            TxtCriterePost.ResetText()
            TxtCriterePost.Focus()
        Else
            If TxtCriterePost.Enabled = False Then
                If txtGroupe.IsRequiredControl("Veuillez saisir un groupe") Then
                    txtGroupe.Select()
                    Exit Sub
                End If

                'For i = 0 To ViewPostQualif.RowCount - 1
                '    If ViewPostQualif.GetRowCellValue(i, "Description").ToString = txtGroupe.Text And i <> IdModPost Then
                '        SuccesMsg("Ce groupe existe déjà")
                '        Exit Sub
                '    End If
                'Next
                'For i = 0 To ViewPostQualif.RowCount - 1
                '    If ViewPostQualif.GetRowCellValue(i, "Id").ToString = "##" Then
                '        If ViewPostQualif.GetRowCellValue(IdModPost, "Description").ToString = ViewPostQualif.GetRowCellValue(i, "Groupe").ToString Then
                '            ViewPostQualif.SetRowCellValue(i, "Groupe", txtGroupe.Text)
                '        End If
                '    End If
                'Next
                NodeModPost.SetValue("Description", txtGroupe.Text)
                'ViewPostQualif.SetRowCellValue(IdModPost, "Description", txtGroupe.Text)
            Else
                If TxtCriterePost.IsRequiredControl("Veuillez saisir un critère") Then
                    TxtCriterePost.Select()
                    Exit Sub
                End If
                NodeModPost.SetValue("Description", "      " & TxtCriterePost.Text)
                NodeModPost.SetValue("Eliminatoire", IIf(ChkCritElimine.Checked = True, "OUI", "NON").ToString)
                'ViewPostQualif.SetRowCellValue(IdModPost, "Description", "      " & TxtCriterePost.Text)
                'ViewPostQualif.SetRowCellValue(IdModPost, "Eliminatoire", IIf(ChkCritElimine.Checked = True, "OUI", "NON").ToString)
            End If
            initChampPostQualif()

        End If
    End Sub

    Private Sub ChkCritElimine_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCritElimine.CheckedChanged
        If TxtCriterePost.Text.Trim().Length = 0 Then
            TxtCriterePost.ResetText()
            TxtCriterePost.Focus()
        End If
    End Sub
    Private Sub LoadExamPostQualif()

        'Dim cptr1 As Decimal = 0

        'Dim dtExamen As DataTable = GridPostQualif.DataSource
        'query = "select * from T_DAO_PostQualif where NumeroDAO='" & NumDoss & "' and RefCritereMere='0'"
        'Dim dt As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt.Rows
        '    cptr1 = 0
        '    Dim drS = dtExamen.NewRow()

        '    drS("Id") = rw("RefCritere")
        '    drS("Code") = "G"
        '    'drS("N°") = "G" & CptrG.ToString
        '    drS("Description") = MettreApost(rw("LibelleCritere").ToString)
        '    dtExamen.Rows.Add(drS)

        '    query = "select LibelleCritere,CritereElimine from T_DAO_PostQualif where NumeroDAO='" & NumDoss & "' and RefCritereMere='" & rw("RefCritere").ToString & "'"
        '    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        '    For Each rw1 As DataRow In dt1.Rows

        '        cptr1 += 1
        '        drS = dtExamen.NewRow()

        '        drS("Id") = rw("RefCritere")
        '        drS("Code") = IIf(CDec(cptr1 / 2) = CDec(cptr1 \ 2), "x", "").ToString
        '        'drS("N°") = ""
        '        drS("Description") = "      " & MettreApost(rw1("LibelleCritere").ToString)
        '        drS("Eliminatoire") = rw1("CritereElimine").ToString

        '        dtExamen.Rows.Add(drS)
        '    Next
        'Next
        'ViewPostQualif.OptionsView.ColumnAutoWidth = True
        'ColorRowGrid(ViewPostQualif, "[Code]='x'", Color.LightGray, "Tahoma", 8, FontStyle.Regular, Color.Black)
        'ColorRowGridAnal(ViewPostQualif, "[Code]='G'", Color.Navy, "Tahoma", 9, FontStyle.Bold, Color.White, True)

        ListePostQualif.Nodes.Clear()
        query = "select * from T_DAO_PostQualif where NumeroDAO='" & NumDoss & "' and RefCritereMere='0'"
        Dim dt2 As DataTable = ExcecuteSelectQuery(query)
        ListePostQualif.BeginUnboundLoad()
        Dim parentForRootNodes As TreeListNode = Nothing
        For Each rw As DataRow In dt2.Rows
            Dim rootNode As TreeListNode = ListePostQualif.AppendNode(New Object() {rw("RefCritere").ToString, "G", MettreApost(rw("LibelleCritere").ToString), ""}, parentForRootNodes)
            query = "select RefCritere, LibelleCritere, CritereElimine from T_DAO_PostQualif where NumeroDAO='" & NumDoss & "' and RefCritereMere='" & rw("RefCritere").ToString & "'"
            Dim dt3 As DataTable = ExcecuteSelectQuery(query)
            For Each rw1 As DataRow In dt3.Rows
                ListePostQualif.AppendNode(New Object() {rw1("RefCritere").ToString, "", "      " & MettreApost(rw1("LibelleCritere").ToString), rw1("CritereElimine").ToString}, rootNode)
            Next
        Next
        ListePostQualif.EndUnboundLoad()
    End Sub

    Private Sub LoadPagePostQualif(ByVal NumDossier As String)
        If Not PagePostQualif.PageEnabled Then PagePostQualif.PageEnabled = True
        If IsNothing(CurrentDao) Then
            query = "SELECT * FROM t_dao WHERE NumeroDAO='" & NumDoss & "'"
            Dim dtDao As DataTable = ExcecuteSelectQuery(query)
            If dtDao.Rows.Count = 0 Then
                CurrentDao = Nothing
                Exit Sub
            End If
            CurrentDao = dtDao.Rows(0)
        End If

        If IsNothing(CurrentMarche) Then
            query = "SELECT * FROM t_marche WHERE RefMarche='" & CurrentDao("RefMarche") & "'"
            Dim dtmarche As DataTable = ExcecuteSelectQuery(query)
            If dtmarche.Rows.Count > 0 Then
                CurrentMarche = dtmarche.Rows(0)
            Else
                CurrentMarche = Nothing
                FailMsg("Le marché associé a été supprimé")
                Exit Sub
            End If
        End If
        InitPostQualif()
        LoadExamPostQualif()
        txtGroupe.Focus()
    End Sub

    Private Function SavePagePostQualif(ByVal NumDossier As String) As Boolean
        Dim RefGroupe As String = String.Empty
        'For i = 0 To ViewPostQualif.RowCount - 1
        '    Dim rw As DataRow = ViewPostQualif.GetDataRow(i)
        '    If rw("Id") = "##" Then
        '        If rw("Code") = "G" Then
        '            query = "INSERT INTO T_DAO_PostQualif(RefCritere,NumeroDAO,LibelleCritere,CritereElimine,RefCritereMere) "
        '            query &= "VALUES(NULL,'" & NumDoss & "','" & rw("Description").ToString().EnleverApostrophe() & "','NON','0')"
        '            ExecuteNonQuery(query)
        '            RefGroupe = ExecuteScallar("SELECT MAX(RefCritere) FROM t_dao_postqualif WHERE NumeroDAO='" & NumDoss & "'")
        '            ViewPostQualif.SetRowCellValue(i, "Id", RefGroupe)
        '        Else
        '            query = "INSERT INTO T_DAO_PostQualif(RefCritere,NumeroDAO,LibelleCritere,CritereElimine,RefCritereMere) "
        '            query &= "VALUES(NULL,'" & NumDoss & "','" & rw("Description").ToString().Replace("      ", "").EnleverApostrophe & "',"
        '            query &= "'" & IIf(ChkCritElimine.Checked = True, "OUI", "NON").ToString & "','" & RefGroupe & "')"
        '            ExecuteNonQuery(query)
        '            Dim Id As String = ExecuteScallar("SELECT MAX(RefCritere) FROM t_dao_postqualif WHERE NumeroDAO='" & NumDoss & "'")
        '            ViewPostQualif.SetRowCellValue(i, "Id", Id)
        '        End If
        '    Else
        '        If rw("Code") = "G" Then
        '            query = "UPDATE T_DAO_PostQualif SET LibelleCritere='" & rw("Description").ToString().EnleverApostrophe & "' "
        '            query &= "WHERE RefCritere='" & rw("Id") & "'"
        '        Else
        '            query = "UPDATE T_DAO_PostQualif SET LibelleCritere='" & rw("Description").ToString().EnleverApostrophe & "', "
        '            query &= "CritereElimine='" & IIf(ChkCritElimine.Checked = True, "OUI", "NON").ToString & "' WHERE RefCritere='" & rw("Id") & "'"
        '        End If
        '        ExecuteNonQuery(query)
        '    End If
        'Next

        For i = 0 To ListePostQualif.Nodes.Count - 1
            If ListePostQualif.Nodes(i).GetValue("IdCol").ToString = "##" And ListePostQualif.Nodes(i).GetValue("Code").ToString = "G" Then
                query = "INSERT INTO T_DAO_PostQualif(RefCritere,NumeroDAO,LibelleCritere,CritereElimine,RefCritereMere) "
                query &= "VALUES(NULL,'" & NumDossier & "','" & ListePostQualif.Nodes(i).Item("Description").ToString().EnleverApostrophe() & "','NON','0')"
                ExecuteNonQuery(query)
                RefGroupe = ExecuteScallar("SELECT MAX(RefCritere) FROM t_dao_postqualif WHERE NumeroDAO='" & NumDossier & "'")
                ListePostQualif.Nodes(i).SetValue("IdCol", RefGroupe)
                For j = 0 To ListePostQualif.Nodes(i).Nodes.Count - 1
                    If ListePostQualif.Nodes(i).Nodes(j).Item("IdCol").ToString = "##" Then
                        query = "INSERT INTO T_DAO_PostQualif(RefCritere,NumeroDAO,LibelleCritere,CritereElimine,RefCritereMere) "
                        query &= "VALUES(NULL,'" & NumDossier & "','" & ListePostQualif.Nodes(i).Nodes(j).GetValue("Description").ToString().Replace("      ", "").EnleverApostrophe & "',"
                        query &= "'" & ListePostQualif.Nodes(i).Nodes(j).GetValue("Eliminatoire").ToString() & "','" & RefGroupe & "')"
                        ExecuteNonQuery(query)
                        Dim Id As String = ExecuteScallar("SELECT MAX(RefCritere) FROM t_dao_postqualif WHERE NumeroDAO='" & NumDossier & "'")
                        ListePostQualif.Nodes(i).Nodes(j).SetValue("IdCol", Id)
                    End If
                Next
            Else
                query = "UPDATE T_DAO_PostQualif SET LibelleCritere='" & ListePostQualif.Nodes(i).GetValue("Description").ToString().EnleverApostrophe & "' WHERE RefCritere='" & ListePostQualif.Nodes(i).GetValue("IdCol") & "'"
                ExecuteNonQuery(query)
                For j = 0 To ListePostQualif.Nodes(i).Nodes.Count - 1
                    If ListePostQualif.Nodes(i).Nodes(j).Item("IdCol").ToString = "##" Then
                        query = "INSERT INTO T_DAO_PostQualif(RefCritere,NumeroDAO,LibelleCritere,CritereElimine,RefCritereMere) "
                        query &= "VALUES(NULL,'" & NumDossier & "','" & ListePostQualif.Nodes(i).Nodes(j).GetValue("Description").ToString().Replace("      ", "").EnleverApostrophe & "',"
                        query &= "'" & ListePostQualif.Nodes(i).Nodes(j).GetValue("Eliminatoire").ToString() & "','" & ListePostQualif.Nodes(i).GetValue("IdCol") & "')"
                        ExecuteNonQuery(query)

                        Dim Id As String = ExecuteScallar("SELECT MAX(RefCritere) FROM t_dao_postqualif WHERE NumeroDAO='" & NumDoss & "'")
                        ListePostQualif.Nodes(i).Nodes(j).SetValue("IdCol", Id)
                    Else
                        query = "UPDATE T_DAO_PostQualif SET LibelleCritere='" & ListePostQualif.Nodes(i).Nodes(j).GetValue("Description").ToString().Replace("      ", "").EnleverApostrophe & "', "
                        query &= "CritereElimine='" & ListePostQualif.Nodes(i).Nodes(j).GetValue("Eliminatoire").ToString() & "' WHERE RefCritere='" & ListePostQualif.Nodes(i).Nodes(j).GetValue("IdCol") & "'"
                        ExecuteNonQuery(query)
                    End If
                Next
            End If
        Next

        If CodePostQualifSup.Count > 0 Then
            For k = 0 To CodePostQualifSup.Count - 1
                If CodePostQualifSup.Item(k).ToString <> "##" Then
                    query = "DELETE FROM T_DAO_PostQualif WHERE NumeroDAO='" & NumDossier & "' AND RefCritere='" & CodePostQualifSup.Item(k) & "'"
                    ExecuteNonQuery(query)
                End If
            Next
        End If
        CodePostQualifSup.Clear()
        Return True
    End Function
#End Region

#Region "Code non Utiliser"


    Private Sub ChkNumDaoAuto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdAttrLot.CheckedChanged
        'If (ChkNumDaoAuto.Checked = True) Then
        '    If (TxtNumDao.Text <> "") Then
        '        Dim Rep1 As MsgBoxResult = MsgBox("Le numéro entré sera remplacer!" & vbNewLine & "Voulez-vous continuer?", MsgBoxStyle.YesNo)
        '        If (Rep1 = MsgBoxResult.No) Then
        '            ChkNumDaoAuto.Checked = False
        '            Exit Sub
        '        End If
        '    End If

        '    TxtNumDao.Enabled = False
        '    TxtLibelleDao.Enabled = True
        '    ChkLibDaoAuto.Enabled = True
        '    TxtLibelleDao.Focus()
        'Else
        '    TxtNumDao.Enabled = True
        'End If
    End Sub

    Private Sub ChkLibDaoAuto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If (ChkLibDaoAuto.Checked = True) Then
        '    If (TxtLibelleDao.Text <> "") Then
        '        Dim Rep1 As MsgBoxResult = MsgBox("Le libellé entré sera remplacer!" & vbNewLine & "Voulez-vous continuer?", MsgBoxStyle.YesNo)
        '        If (Rep1 = MsgBoxResult.No) Then
        '            ChkLibDaoAuto.Checked = False
        '            Exit Sub
        '        End If
        '    End If
        '    TxtLibelleDao.Enabled = False
        '    'GridMarcheDao.Enabled = True
        '    TxtLibelleDao.Text = "********   Ajoutez un (des) marché(s)   ********"
        '    'GridMarcheDao.Rows.Clear()
        '    'Dim n As Decimal = 'GridMarcheDao.Rows.Add()
        '    'GridMarcheDao.Rows.Item(n).Cells(0).Value = "Ajouter"
        'Else
        '    If (Mid(TxtLibelleDao.Text, 1, 4) = "****") Then
        '        TxtLibelleDao.Text = ""
        '    End If
        '    TxtLibelleDao.Enabled = True
        'End If
    End Sub


    'Private Sub NumNbreCopie_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumNbreCopie.EditValueChanged
    '    If (NumNbreCopie.Value <> 0) Then
    '        TxtMontCaution.Enabled = True
    '        TxtPrctCaution.Enabled = True
    '    Else
    '        TxtMontCaution.Enabled = False
    '        TxtPrctCaution.Enabled = False
    '    End If
    'End Sub

    'Private Sub TxtMontCaution_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If (TxtPrctCaution.Text <> "" And NumDoss <> "") Then
    '        If TxtMethodeMarche.Text = "CF" Then

    '            query = "Update T_DAO set PourcGarantie='0' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
    '            ExecuteNonQuery(query)

    '        Else
    '            If (CDec(TxtPrctCaution.Text) >= 0.01) Then
    '                'Mise a jour dans table Dao ********
    '                query = "Update T_DAO set PourcGarantie='" & Math.Round(CDec(TxtPrctCaution.Text), 2).ToString & "' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
    '                ExecuteNonQuery(query)
    '            Else
    '                MsgBox("Le montant est insignifiant!", MsgBoxStyle.Exclamation)
    '                TxtMontCaution.Focus()
    '            End If
    '        End If

    '    End If
    'End Sub

    'Private Sub TxtMontCaution_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If (TxtMontCaution.Text <> "") Then
    '        NumValideCaution.Enabled = True
    '        CmbValideCaution.Enabled = True
    '        VerifSaisieMontant(TxtMontCaution)

    '        If (montMarc <> 0) Then
    '            TxtPrctCaution.Text = (Math.Round((CDec(TxtMontCaution.Text.Replace(" ", "")) * 100) / montMarc, 5)).ToString
    '        End If
    '    Else
    '        NumValideCaution.Enabled = False
    '        CmbValideCaution.Enabled = False
    '    End If
    'End Sub

    'Private Sub NumValideCaution_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If (NumValideCaution.Value <> 0 And CmbValideCaution.Text <> "" And NumDoss <> "") Then
    '        'Mise a jour dans table Dao ********
    '        query = "Update T_DAO set ValiditeCaution='" & NumValideCaution.Value.ToString & " " & CmbValideCaution.Text & "' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
    '        ExecuteNonQuery(query)
    '    End If
    'End Sub

    'Private Sub CmbValideCaution_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If (NumValideCaution.Value <> 0 And CmbValideCaution.Text <> "" And NumDoss <> "") Then
    '        'Mise a jour dans table Dao ********
    '        query = "Update T_DAO set ValiditeCaution='" & NumValideCaution.Value.ToString & " " & CmbValideCaution.Text & "' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
    '        ExecuteNonQuery(query)
    '    End If
    'End Sub

    'Private Sub CmbValideCaution_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If (CmbValideCaution.Text <> "") Then
    '        NumDelai.Enabled = True
    '        CmbDelai.Enabled = True
    '    Else
    '        NumDelai.Enabled = False
    '        CmbDelai.Enabled = False
    '    End If
    'End Sub

    Private Sub MajPlanMarche(ByVal DelaiExec As String)

        'RefMarche et Code procedure
        Dim codMarche(10) As Decimal
        Dim codProced(10) As Decimal
        Dim typMarche As String = ""
        Dim cptMarche As Decimal = 0
        query = "select RefMarche,CodeProcAO,TypeMarche from T_Marche where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            codMarche(cptMarche) = CInt(rw(0))
            codProced(cptMarche) = CInt(rw(1))
            cptMarche = cptMarche + 1
            typMarche = rw(2).ToString
        Next

        If (cptMarche > 0) Then
            For k As Integer = 0 To cptMarche - 1
                Dim refEtape As Decimal = 0
                Dim numOrdre As Decimal = 0
                query = "select E.RefEtape,E.NumeroOrdre from T_EtapeMarche as E,T_DelaiEtape as D where E.RefEtape=D.RefEtape and D.DelaiEtape='DE-DAO' and D.CodeProcAO='" & codProced(k) & "' and E.TypeMarche='" & typMarche & "' and E.CodeProjet='" & ProjetEnCours & "'"
                dt0 = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    refEtape = CInt(rw(0))
                    numOrdre = CInt(rw(1))
                Next

                Dim dernDate As String = ""
                Dim dernNum As Decimal = numOrdre - 1
                While (dernDate = "" And dernNum > 0)
                    query = "select FinPrevue from T_PlanMarche where RefMarche='" & codMarche(k) & "' and NumeroOrdre='" & dernNum & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        dernDate = rw(0).ToString
                    Next

                    If (dernDate = "") Then dernNum = dernNum - 1

                End While

                If (dernDate <> "") Then
                    Dim partDate() As String = DelaiExec.Split(" "c)
                    Dim nDuree As Decimal = CInt(partDate(0))
                    Dim dateFin As String = ""
                    If (partDate(1) = "Jours") Then
                        dateFin = CDate(dernDate).AddDays(nDuree).ToShortDateString
                    ElseIf (partDate(1) = "Semaines") Then
                        dateFin = CDate(dernDate).AddDays(nDuree * 7).ToShortDateString
                    ElseIf (partDate(1) = "Mois") Then
                        dateFin = CDate(dernDate).AddMonths(nDuree).ToShortDateString
                    ElseIf (partDate(1) = "Ans") Then
                        dateFin = CDate(dernDate).AddYears(nDuree).ToShortDateString
                    End If

                    query = "Update T_PlanMarche set DebutPrevu='" & dernDate & "', FinPrevue='" & dateFin & "' where RefMarche='" & codMarche(k) & "' and NumeroOrdre='" & numOrdre & "'"
                    ExecuteNonQuery(query)

                    Dim etapeFini As Boolean = False
                    While etapeFini = False

                        etapeFini = True
                        Dim dureeEtap As String = ""
                        numOrdre = numOrdre + 1
                        query = "select D.DelaiEtape from T_EtapeMarche as E,T_DelaiEtape as D where E.RefEtape=D.RefEtape and D.CodeProcAO='" & codProced(k) & "' and E.TypeMarche='" & typMarche & "' and E.CodeProjet='" & ProjetEnCours & "' and E.NumeroOrdre='" & numOrdre & "'"
                        dt0 = ExcecuteSelectQuery(query)
                        For Each rw As DataRow In dt0.Rows
                            etapeFini = False
                            dureeEtap = rw(0)
                        Next

                        If (etapeFini = False) Then
                            dernDate = dateFin
                            Dim partDate1() As String = dureeEtap.Split(" "c)
                            Dim nDuree1 As Decimal = CInt(partDate1(0))
                            If (partDate1(1) = "Jours") Then
                                dateFin = CDate(dernDate).AddDays(nDuree1).ToShortDateString
                            ElseIf (partDate1(1) = "Semaines") Then
                                dateFin = CDate(dernDate).AddDays(nDuree1 * 7).ToShortDateString
                            ElseIf (partDate1(1) = "Mois") Then
                                dateFin = CDate(dernDate).AddMonths(nDuree1).ToShortDateString
                            End If


                            query = "Update T_PlanMarche set DebutPrevu='" & dernDate & "', FinPrevue='" & dateFin & "' where RefMarche='" & codMarche(k) & "' and NumeroOrdre='" & numOrdre & "'"
                            ExecuteNonQuery(query)


                        End If

                    End While

                End If

            Next

        End If
        '************************************

    End Sub

    'Private Sub CmbDelai_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbDelai.SelectedValueChanged
    '    If (CmbDelai.Text <> "") Then
    '        CmbCivCojo.Enabled = True
    '        TxtFonctionCojo.Enabled = True
    '        TxtContactCojo.Enabled = True
    '        TxtMailCojo.Enabled = True
    '        TxtCojo.Enabled = True
    '        GridCommission.Enabled = True
    '        CmbTitreCojo.Enabled = True
    '    Else
    '        CmbCivCojo.Enabled = False
    '        TxtFonctionCojo.Enabled = False
    '        TxtContactCojo.Enabled = False
    '        TxtMailCojo.Enabled = False
    '        TxtCojo.Enabled = False
    '        GridCommission.Enabled = False
    '        CmbTitreCojo.Enabled = False
    '    End If
    'End Sub
#End Region

#Region "Context MenuScript"

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If LayoutView1.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub ModifierLeDossier_Click(sender As Object, e As EventArgs) Handles ModifierLeDossier.Click
        Try
            If LayoutView1.RowCount > 0 Then
                If PourAjoutModifDansDB = 1 Then
                    SuccesMsg("Veuillez enregistrer le dossier en cours.")
                    Exit Sub
                ElseIf (PourAjoutModifDansDB = 2) Then
                    SuccesMsg("Veuillez enregistrer et fermer le dossier en cours.")
                    Exit Sub
                End If

                Dim drx = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)

                If AfficherDossier = False Then 'Au cas de non afficharge du dossier
                    If drx("Statut").ToString = "Annulé" Then
                        FailMsg("Impossible de modifier un dossier annulé.")
                        Exit Sub
                    End If

                    If DateTime.Compare(CDate(drx("DateLimitePropo").ToString), Now) < 0 And CBool(drx("DossValider")) = True Then
                        FailMsg("Impossible de modifier un dossier validé.")
                        Exit Sub
                    End If
                End If

                PourAjoutModifDansDB = 2
                NumDoss = EnleverApost(drx("N°").ToString)
                TypeMarche = drx("Type").ToString
                MethodMarche = drx("Méthode").ToString

                cmbTypeMarche.Text = TypeMarche
                TxtMethodeMarche.Text = MethodMarche
                CurrentDao = Nothing
                CurrentDao = Nothing
                LoadPageDonneBase(NumDoss)
                VisibleOtherTabs(True)
                If Not BtFermerDAO.Enabled Then BtFermerDAO.Enabled = True
                If Not BtEnregistrer.Enabled Then BtEnregistrer.Enabled = True
                TabAMettreAJour(0) = True
                GroupMarche.Enabled = True
                GroupControlPub.Enabled = True
                TxtLibelleDao.Enabled = True
                TxtPrixDao.Enabled = True
                CmbCompte.Enabled = True
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub AfficherLeDossier_Click(sender As Object, e As EventArgs) Handles AfficherLeDossier.Click
        'Dim FFF As String = MontantLettre("858 750")
        'InputBox("foj", "fodj", FFF)
        'Exit Sub
        Try
            If PourAjoutModifDansDB = 1 Then
                SuccesMsg("Veuillez enregistrer le dossier en cours.")
                Exit Sub
            ElseIf (PourAjoutModifDansDB = 2) Then
                SuccesMsg("Veuillez enregistrer et fermer le dossier en cours.")
                Exit Sub
            End If

            AfficherDossier = True
            ModifierLeDossier_Click(Me, e)
            GetEnebledAffichageBouton(False)

            'If LayoutView1.RowCount > 0 Then
            '    Dim drx = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
            '    Dim NewApercuDAO As New ApercuDAO
            '    NewApercuDAO.NumDoss = drx("N°")
            '    Disposer_form(NewApercuDAO)
            'End If
        Catch ex As Exception
            AfficherDossier = False
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub GetEnebledAffichageBouton(value As Boolean)
        BtEnregistrer.Enabled = value
        BtEnrgLot.Enabled = value
        BtAjoutCojo.Enabled = value
        BtAjoutSection.Enabled = value
        BtSupSection.Enabled = value
        BtAnnulPostQualif.Enabled = value
        BtEnrgPostQualif.Enabled = value
        BtEnregBien.Enabled = value
        btRetourBien.Enabled = value
        BtCategBien.Enabled = value
        BtSupServicConnex.Enabled = value

        NaturePrix.Properties.ReadOnly = Not value
        NomJournal.Properties.ReadOnly = Not value
        LigneBudgetaire.Properties.ReadOnly = Not value
        LieurRemiseFourniture.Properties.ReadOnly = Not value

        DatePublication.Properties.ReadOnly = Not value
        HeurePub.Properties.ReadOnly = Not value
        NbreDelaiPub.Properties.ReadOnly = Not value
        JoursDelaiPub.Properties.ReadOnly = Not value
        HeureDepot.Properties.ReadOnly = Not value
        DateReporte.Properties.ReadOnly = Not value
        HeureReporte.Properties.ReadOnly = Not value

        TxtLibelleDao.ReadOnly = Not value
        TxtLibLot.ReadOnly = Not value
        TxtCautionLot.Properties.ReadOnly = Not value
        NumGarantiLot.Properties.ReadOnly = Not value
        CmbGarantiLot.Properties.ReadOnly = Not value
        TxtSaisiSouLot.Properties.ReadOnly = Not value
        TxtPrixDao.ReadOnly = Not value
        CmbCompte.Properties.ReadOnly = Not value
        CombSection.Properties.ReadOnly = Not value
        TxtLibelleserviceconnexe.Properties.ReadOnly = Not value
    End Sub

    Private Sub SupprimerLeDossier_Click(sender As Object, e As EventArgs) Handles SupprimerLeDossier.Click
        If LayoutView1.RowCount > 0 Then
            Try
                If PourAjoutModifDansDB = 1 Then
                    SuccesMsg("Veuillez enregistrer le dossier en cours.")
                    Exit Sub
                ElseIf (PourAjoutModifDansDB = 2) Then
                    SuccesMsg("Veuillez enregistrer et fermer le dossier en cours.")
                    Exit Sub
                End If

                Dim drx = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)

                'query = "SELECT DossValider FROM t_dao WHERE NumeroDAO='" & EnleverApost(drx("N°").ToString) & "'"
                ' Dim test As Boolean = ExecuteScallar(query)
                If CBool(drx("DossValider")) = True Then
                    FailMsg("Impossible de supprimer ce dossier, car il a déjà été validé.")
                    Exit Sub
                End If

                If ConfirmMsg("Voulez-vous supprimer ce dossier ?") = DialogResult.Yes Then
                    DebutChargement(True, "Suppression du dossier en cours...")

                    Dim NumeroDAO As String = EnleverApost(drx("N°").ToString)

                    'Suppression des données post qualification
                    ExecuteNonQuery("DELETE FROM t_dao_postqualif WHERE NumeroDAO='" & NumeroDAO & "'")

                    If drx("Type").ToString.ToLower = "Fournitures".ToLower Then
                        'Suppresion des données de spécification technique
                        ExecuteNonQuery("DELETE FROM t_spectechcaract WHERE RefSpecFournit IN(SELECT RefSpecFournit FROM t_spectechfourniture WHERE NumeroDAO='" & NumeroDAO & "')")

                        ExecuteNonQuery("DELETE FROM T_SpecTechFourniture WHERE NumeroDAO='" & NumeroDAO & "'")
                        'Suppression dE LA LISTE DES ERVICES CONNEXES
                        ExecuteNonQuery("delete from t_dao_service_connexe where NumeroDAO='" & NumeroDAO & "'")
                    Else
                        'Suppresion des Items des DQE
                        ExecuteNonQuery("DELETE FROM t_dqeitem WHERE RefSection IN(SELECT RefSection FROM t_dqesection WHERE NumeroDAO='" & NumeroDAO & "')")
                        'Suppresion des sections
                        ExecuteNonQuery("DELETE FROM t_dqesection WHERE NumeroDAO='" & NumeroDAO & "'")
                    End If

                    'Supression des données dans membre de la comission
                    ExecuteNonQuery("DELETE FROM t_commission WHERE NumeroDAO='" & NumeroDAO & "'")
                    'Suppression des fournisseurs
                    ExecuteNonQuery("DELETE FROM T_Fournisseur where NumeroDAO='" & NumeroDAO & "' and CodeProjet='" & ProjetEnCours & "'")

                    'Supprimer les sous-lots
                    ExecuteNonQuery("DELETE FROM t_lotdao_souslot WHERE NumeroDAO='" & NumeroDAO & "'")

                    'Supprimer les lots
                    ExecuteNonQuery("DELETE FROM t_lotdao WHERE NumeroDAO='" & NumeroDAO & "'")

                    'Recuperation de reference marche
                    Dim RefMarche = Val(ExecuteScallar("SELECT RefMarche FROM t_dao WHERE NumeroDAO='" & NumeroDAO & "'"))
                    'Suppression du DAO
                    ExecuteNonQuery("DELETE FROM t_dao WHERE NumeroDAO='" & NumeroDAO & "'")

                    'Mise à jour du marché
                    ExecuteNonQuery("UPDATE t_marche SET NumeroDAO=NULL WHERE RefMarche='" & RefMarche & "'")

                    SuccesMsg("Dosssier supprimé avec succès.")
                    'LoadArchivesDao()
                    LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle).Delete()
                End If
            Catch ex As Exception
                FinChargement()
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub ImprimerLeDossier_Click(sender As Object, e As EventArgs) Handles ImprimerLeDossier.Click
        If LayoutView1.RowCount > 0 Then
            Try
                If PourAjoutModifDansDB = 1 Then
                    SuccesMsg("Veuillez enregistrer le dossier en cours.")
                    Exit Sub
                ElseIf (PourAjoutModifDansDB = 2) Then
                    SuccesMsg("Veuillez enregistrer et fermer le dossier en cours.")
                    Exit Sub
                End If

                Dim drx = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)

                If ListeMethodePrevue.Contains(drx("Méthode").ToString.ToUpper) = False Then
                    FailMsg("Aucun état prévu pour la méthode [" & drx("Méthode").ToString.ToUpper & "]")
                    Exit Sub
                End If

                Dim DossierGenerer As Boolean = False
                Dim FicheiExiste As Boolean = False
                Dim dtSoumis As New DataTable
                Dim NomFichierpdf As String = ""

                Dim NomRepCheminSauve As String = line & "\DAO\" & drx("Type").ToString & "\" & drx("Méthode").ToString & "\" & FormatFileName(drx("N°").ToString, "")

                If drx("Méthode").ToString.ToUpper = "PSL" Then ' And drx("Type").ToString.ToLower = "Fournitures".ToLower Then
                    query = "select * from T_Fournisseur where NumeroDAO='" & EnleverApost(drx("N°").ToString) & "' and CodeProjet='" & ProjetEnCours & "'" ' and DateDepotDAO<>''"
                    dtSoumis = ExcecuteSelectQuery(query)
                    If dtSoumis.Rows.Count = 0 Then
                        FailMsg("Aucun soumissoinnaire enregistré.")
                        Exit Sub
                    End If

                    'Verifier si la génération a été effectué
                    FicheiExiste = True
                    For Each rw As DataRow In dtSoumis.Rows
                        NomFichierpdf = "DAO N°_" & rw("CodeFournis").ToString & FormatFileName(drx("N°").ToString, "") & ".pdf"
                        If Not File.Exists(NomRepCheminSauve & "\" & NomFichierpdf) Then
                            FicheiExiste = False
                        End If
                    Next
                ElseIf File.Exists(NomRepCheminSauve & "\DAO N°_" & FormatFileName(drx("N°").ToString, "") & ".pdf") Then
                    FicheiExiste = True
                End If

                'Recherche date limite de depot des dossiers
                'Dossier valider par la bailleur de fond et fichier existant on actualise plus
                If DateTime.Compare(CDate(drx("DateLimitePropo")), Now) < 0 And CBool(drx("DossValider")) = True And FicheiExiste = True Then
                    DossierGenerer = True
                End If

                If DossierGenerer = False Then
                    If NewGenereDossDAO(drx("N°").ToString) = False Then
                        FinChargement()
                        Exit Sub
                    End If

                ElseIf ("Voulez-vous actualiser les données du dossier ?") = DialogResult.Yes Then
                    If NewGenereDossDAO(drx("N°").ToString, "Actualisation des données du dossier en cours...") = False Then
                        FinChargement()
                        Exit Sub
                    End If
                End If
                FinChargement()

                If dtSoumis.Rows.Count > 0 Then 'Cas de PSL
                    For Each rw1 As DataRow In dtSoumis.Rows
                        NomFichierpdf = "DAO N°_" & rw1("CodeFournis").ToString & FormatFileName(drx("N°").ToString, "") & ".pdf"
                        If File.Exists(NomRepCheminSauve & "\" & NomFichierpdf) = True Then
                            DebutChargement(True, "Chargement du dossier de " & MettreApost(rw1("NomFournis").ToString.Split(" ")(0)) & " en cours...")
                            Process.Start(NomRepCheminSauve & "\" & NomFichierpdf)
                            FinChargement()
                        End If
                    Next
                Else
                    If File.Exists(NomRepCheminSauve & "\DAO N°_" & FormatFileName(drx("N°").ToString, "") & ".pdf") = True Then
                        DebutChargement(True, "Chargement du dossier d'appel d'offre...")
                        Process.Start(NomRepCheminSauve & "\DAO N°_" & FormatFileName(drx("N°").ToString, "") & ".pdf")
                        FinChargement()
                    Else
                        SuccesMsg("Le fichier spécifié n'existe pas ou a été supprimé.")
                    End If
                End If

                'ApercuDAO.ImpressionDAO(NumDossier)
            Catch ex As Exception
                FinChargement()
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub

    Private Function NewGenereDossDAO(ByVal NumeroDoss As String, Optional TextActualisation As String = "") As Boolean
        Try
            Dim dt As DataTable = ExcecuteSelectQuery("select * from t_dao where NumeroDAO='" & EnleverApost(NumeroDoss.ToString) & "' and CodeProjet='" & ProjetEnCours & "'")
            If dt.Rows.Count = 0 Then
                Return False
            End If
            Dim CurrenDoss As DataRow = dt.Rows(0)

            Dim TypeMarches As String = MettreApost(CurrenDoss("TypeMarche").ToString)
            Dim Methodes As String = MettreApost(CurrenDoss("MethodePDM").ToString)
            Dim NumeroDAO As String = MettreApost(CurrenDoss("NumeroDAO").ToString)

            DebutChargement(True, IIf(TextActualisation = "", "Consolidation du dossier d'appel d'offre...", TextActualisation).ToString)

            Dim PageGarde, report0, report1, report2, report3, report4, report5, report6, report7, report8, report9, report10, report11, report12, report13 As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim DatSet = New DataSet

            Dim Chemin As String = lineEtat & "\Marches\DAO\Fournitures\DTAO\"

            If TypeMarches.ToLower = "Travaux".ToLower Then
                Chemin = lineEtat & "\Marches\DAO\Travaux\"
            End If

            'Remplir la table tempo des sections.
            If RemplirTempSection(NumeroDoss, TypeMarches, Methodes) = False Then
                FinChargement()
                Return False
            End If

            If TypeMarches.ToLower = "fournitures" Then
                If Methodes.ToUpper = "AOI" Or Methodes.ToUpper = "AON" Then
                    PageGarde.Load(Chemin & "AOI_AON\DTAO.rpt")
                    report0.Load(Chemin & "AOI_AON\DTAO_Section_0_IDA.rpt")
                    report1.Load(Chemin & "AOI_AON\DTAO_Section_I_IDA.rpt")
                    report2.Load(Chemin & "AOI_AON\DTAO_Section_II_IDA.rpt")
                    report3.Load(Chemin & "AOI_AON\DTAO_Section_III_IDA.rpt")
                    report4.Load(Chemin & "AOI_AON\DTAO_Section_IV.1_IDA.rpt")
                    report5.Load(Chemin & "AOI_AON\DTAO_Section_IV.2_IDA.rpt")
                    report6.Load(Chemin & "AOI_AON\DTAO_Section_IV.3_IDA.rpt")
                    report7.Load(Chemin & "AOI_AON\DTAO_Section_IX.1_IDA.rpt")
                    report8.Load(Chemin & "AOI_AON\DTAO_Section_V.1_IDA.rpt")
                    report9.Load(Chemin & "AOI_AON\DTAO_Section_VI.1_IDA.rpt")
                    report10.Load(Chemin & "AOI_AON\DTAO_Section_VII.1_IDA.rpt")
                    report11.Load(Chemin & "AOI_AON\DTAO_Section_VIII.1_IDA.rpt")
                    report12.Load(Chemin & "AOI_AON\DTAO_Section_X.1_IDA.rpt")

                ElseIf Methodes.ToUpper = "PSC" Then
                    GetSaveDonneesPSC(CurrenDoss("NumeroDAO").ToString)
                    report0.Load(Chemin & "PSC\DAO_Fourniture_PSC.rpt")

                ElseIf Methodes.ToUpper = "PSO" Then
                    GetSaveTampServiceConnexe(CurrenDoss("NumeroDAO").ToString)
                    report0.Load(Chemin & "PSO\DAO_Fourniture_1_PS0.rpt")
                    report1.Load(Chemin & "PSO\DAO_Fourniture_2_PSO.rpt") 'Paysage
                    report2.Load(Chemin & "PSO\DAO_Fourniture_3_PSO.rpt")
                    report3.Load(Chemin & "PSO\DAO_Fourniture_4_PSO.rpt")

                ElseIf Methodes.ToUpper = "PSL" Then
                    Return GetImpEnvoiDossPSL(NumeroDoss.ToString, TypeMarches)

                ElseIf Methodes.ToUpper = "DC" Then
                    FailMsg("Etat en cours de réalisation.")
                    Return False
                End If

            ElseIf TypeMarches.ToLower = "travaux" Then ' ********************* travaux
                If Methodes.ToUpper = "AOI" Or Methodes.ToUpper = "AON" Then
                    PageGarde.Load(Chemin & "AOI_AON\DTAO_PageGarde.rpt")
                    report0.Load(Chemin & "AOI_AON\DTAO_0_Travaux_IDA.rpt")
                    report1.Load(Chemin & "AOI_AON\DTAO_1_Travaux_IDA.rpt")
                    report2.Load(Chemin & "AOI_AON\DTAO_2_Travaux_IDA.rpt")
                    report3.Load(Chemin & "AOI_AON\DTAO_3_Travaux_IDA.rpt")
                    report4.Load(Chemin & "AOI_AON\DTAO_4_Travaux_IDA.rpt")
                    report5.Load(Chemin & "AOI_AON\DTAO_5_Travaux_IDA.rpt")
                    report6.Load(Chemin & "AOI_AON\DTAO_6_Travaux_IDA.rpt")
                    report7.Load(Chemin & "AOI_AON\DTAO_7_Travaux_IDA.rpt")
                    report8.Load(Chemin & "AOI_AON\DTAO_8_Travaux_IDA.rpt")
                    report9.Load(Chemin & "AOI_AON\DTAO_9_Travaux_IDA.rpt")
                    report10.Load(Chemin & "AOI_AON\DTAO_10_Travaux_IDA.rpt")
                    report11.Load(Chemin & "AOI_AON\DTAO_11_Travaux_IDA.rpt")

                ElseIf Methodes.ToUpper = "PSC" Then
                    report0.Load(Chemin & "PSC\Travaux_RCI_PSC.rpt")

                ElseIf Methodes.ToUpper = "PSO" Then
                    report0.Load(Chemin & "PSO\1_Travaux_RCI_PSO.rpt")
                    report1.Load(Chemin & "PSO\2_Travaux_RCI_PSO.rpt")
                    report2.Load(Chemin & "PSO\3_Travaux_RCI_PSO.rpt")
                    report3.Load(Chemin & "PSO\4_Travaux_RCI_PSO.rpt")

                ElseIf Methodes.ToUpper = "PSL" Then
                    Return GetImpEnvoiDossPSL(NumeroDoss.ToString, TypeMarches)

                ElseIf Methodes.ToUpper = "DC" Then
                    FailMsg("Etat en cours de réalisation.")
                    Return False
                End If
            Else
                FailMsg("L'impression du type de marché [" & TypeMarches & "] n'est pas prévu dans cet onglé.")
                Return False
            End If

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            If TypeMarches.ToLower = "fournitures" Then
                If Methodes.ToUpper = "AOI" Or Methodes.ToUpper = "AON" Then
                    CrTables = PageGarde.Database.Tables
                    For Each CrTable In CrTables
                        crtableLogoninfo = CrTable.LogOnInfo
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo
                        CrTable.ApplyLogOnInfo(crtableLogoninfo)
                    Next

                    CrTables = report2.Database.Tables
                    For Each CrTable In CrTables
                        crtableLogoninfo = CrTable.LogOnInfo
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo
                        CrTable.ApplyLogOnInfo(crtableLogoninfo)
                    Next
                    PageGarde.SetDataSource(DatSet)
                    report2.SetDataSource(DatSet)

                ElseIf Methodes = "PSO" Then
                    CrTables = report2.Database.Tables
                    For Each CrTable In CrTables
                        crtableLogoninfo = CrTable.LogOnInfo
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo
                        CrTable.ApplyLogOnInfo(crtableLogoninfo)
                    Next
                    CrTables = report3.Database.Tables
                    For Each CrTable In CrTables
                        crtableLogoninfo = CrTable.LogOnInfo
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo
                        CrTable.ApplyLogOnInfo(crtableLogoninfo)
                    Next
                    report2.SetDataSource(DatSet)
                    report3.SetDataSource(DatSet)
                    report2.SetParameterValue("NumDAO", CurrenDoss("NumeroDAO").ToString)
                    report2.SetParameterValue("CodeProjet", ProjetEnCours)
                    report3.SetParameterValue("NumDAO", CurrenDoss("NumeroDAO").ToString)
                    report3.SetParameterValue("CodeProjet", ProjetEnCours)
                End If

            ElseIf TypeMarches.ToLower = "travaux" Then ' *************************************
                If Methodes.ToUpper = "AOI" Or Methodes.ToUpper = "AON" Then
                    CrTables = PageGarde.Database.Tables
                    For Each CrTable In CrTables
                        crtableLogoninfo = CrTable.LogOnInfo
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo
                        CrTable.ApplyLogOnInfo(crtableLogoninfo)
                    Next
                    PageGarde.SetDataSource(DatSet)
                    CrTables = report1.Database.Tables
                    For Each CrTable In CrTables
                        crtableLogoninfo = CrTable.LogOnInfo
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo
                        CrTable.ApplyLogOnInfo(crtableLogoninfo)
                    Next
                    report1.SetDataSource(DatSet)

                ElseIf Methodes.ToUpper = "PSL" Then
                ElseIf Methodes.ToUpper = "DC" Then
                End If
            End If

            CrTables = report0.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            report0.SetDataSource(DatSet)
            'Paramettre valable pour toutes les méthodes
            report0.SetParameterValue("NumDAO", CurrenDoss("NumeroDAO").ToString)
            report0.SetParameterValue("CodeProjet", ProjetEnCours)

            Dim NaturePrix As String = ExecuteScallar("select LibelleUnite from t_unite where LibelleCourtUnite='" & CurrenDoss("NaturePrix") & "'")
            Dim MontTaux As Decimal = 1

            ' ****************************** Paramètres page de présentations ***********************************
            If TypeMarches.ToLower = "fournitures" Then
                If Methodes.ToUpper = "AOI" Or Methodes.ToUpper = "AON" Then

                    PageGarde.SetParameterValue("NumDAO", CurrenDoss("NumeroDAO").ToString)
                    PageGarde.SetParameterValue("CodeProjet", ProjetEnCours)

                    report2.SetParameterValue("NumDAO", CurrenDoss("NumeroDAO").ToString)
                    report2.SetParameterValue("CodeProjet", ProjetEnCours)

                    query = "select TauxDevise from T_Devise where AbregeDevise='US$'"
                    Dim dt0 = ExcecuteSelectQuery(query)
                    For Each rw0 As DataRow In dt0.Rows
                        MontTaux = CDec(rw0("TauxDevise"))
                    Next
                    Dim MontantMarché_Devise_Dollar As String = ""
                    Dim MontDollar As Decimal = CDec(IIf(CurrenDoss("MontantMarche").ToString.Replace(".", ",") = "", 0, CurrenDoss("MontantMarche").ToString.Replace(".", ",")))
                    MontantMarché_Devise_Dollar = Math.Round(MontDollar / MontTaux, 2)
                    report2.SetParameterValue("MontantMarché_Devise_Dollar", AfficherMonnaie(MontantMarché_Devise_Dollar))

                ElseIf Methodes = "PSC" Or Methodes = "PSO" Then
                    report0.SetParameterValue("NaturePrix", MettreApost(NaturePrix.ToString))
                End If

            ElseIf TypeMarches.ToLower = "travaux" Then
                If Methodes.ToUpper = "AOI" Or Methodes.ToUpper = "AON" Then
                    PageGarde.SetParameterValue("NumDAO", CurrenDoss("NumeroDAO").ToString)
                    PageGarde.SetParameterValue("CodeProjet", ProjetEnCours)

                    report1.SetParameterValue("NumDAO", CurrenDoss("NumeroDAO").ToString)
                    report1.SetParameterValue("CodeProjet", ProjetEnCours)

                    query = "select TauxDevise from T_Devise where AbregeDevise='US$'"
                    Dim dt0 = ExcecuteSelectQuery(query)
                    For Each rw0 As DataRow In dt0.Rows
                        MontTaux = CDec(rw0("TauxDevise"))
                    Next
                    Dim EquivalentDollar As String = ""
                    Dim MontDollar As Decimal = Val(ExecuteScallar("select c.MontantConvention from t_convention as c, t_marche as m where m.Convention_ChefFile=c.CodeConvention and m.RefMarche='" & CurrenDoss("RefMarche") & "' and m.TypeMarche='" & CurrenDoss("TypeMarche") & "' and m.CodeProjet='" & ProjetEnCours & "'"))
                    EquivalentDollar = Math.Round(MontDollar / MontTaux, 2)
                    report1.SetParameterValue("EquivalentDollar", EquivalentDollar)

                ElseIf Methodes.ToUpper = "PSC" Then
                    report0.SetParameterValue("NaturePrix", MettreApost(NaturePrix.ToString))
                ElseIf Methodes.ToUpper = "PSO" Then
                    report0.SetParameterValue("NaturePrix", MettreApost(NaturePrix.ToString))
                ElseIf Methodes.ToUpper = "PSL" Then
                ElseIf Methodes.ToUpper = "DC" Then

                End If
            End If

            'Enregistrement automatique *************************
            Dim CheminSauvGarde As String = ""
            Dim NomDossier As String = ""

            NomDossier = Environ$("TEMP") & "\DAO\" & TypeMarches & "\" & Methodes & "\" & FormatFileName(NumeroDAO.ToString, "")
            CheminSauvGarde = line & "\DAO\" & TypeMarches & "\" & Methodes & "\" & FormatFileName(NumeroDAO.ToString, "")

            If (Directory.Exists(NomDossier) = False) Then
                Directory.CreateDirectory(NomDossier)
            End If
            If (Directory.Exists(CheminSauvGarde) = False) Then
                Directory.CreateDirectory(CheminSauvGarde)
            End If

            Dim PageGard = NomDossier & "\" & "PageGarde.doc"
            Dim page0 = NomDossier & "\" & "DTAO_Section_0.doc"
            Dim page1 = NomDossier & "\" & "DTAO_Section_I.doc"
            Dim page2 = NomDossier & "\" & "DTAO_Section_II.doc"
            Dim page3 = NomDossier & "\" & "DTAO_Section_III.doc"
            Dim page4 = NomDossier & "\" & "DTAO_Section_IV.1.doc"
            Dim page5 = NomDossier & "\" & "DTAO_Section_IV.2.doc"
            Dim page6 = NomDossier & "\" & "DTAO_Section_IV.3.doc"
            Dim page7 = NomDossier & "\" & "DTAO_Section_IX.1.doc"
            Dim page8 = NomDossier & "\" & "DTAO_Section_V.1.doc"
            Dim page9 = NomDossier & "\" & "DTAO_Section_VI.1.doc"
            Dim page10 = NomDossier & "\" & "DTAO_Section_VII.1.doc"
            Dim page11 = NomDossier & "\" & "DTAO_Section_VIII.1.doc"
            Dim page12 = NomDossier & "\" & "DTAO_Section_X.1.doc"

            If TypeMarches.ToLower = "fournitures" Then
                If Methodes.ToUpper = "AOI" Or Methodes.ToUpper = "AON" Then
                    PageGarde.ExportToDisk(ExportFormatType.WordForWindows, PageGard)
                    report0.ExportToDisk(ExportFormatType.WordForWindows, page0)
                    report1.ExportToDisk(ExportFormatType.WordForWindows, page1)
                    report2.ExportToDisk(ExportFormatType.WordForWindows, page2)
                    report3.ExportToDisk(ExportFormatType.WordForWindows, page3)
                    report4.ExportToDisk(ExportFormatType.WordForWindows, page4)
                    report5.ExportToDisk(ExportFormatType.WordForWindows, page5)
                    report6.ExportToDisk(ExportFormatType.WordForWindows, page6)
                    report7.ExportToDisk(ExportFormatType.WordForWindows, page7)
                    report8.ExportToDisk(ExportFormatType.WordForWindows, page8)
                    report9.ExportToDisk(ExportFormatType.WordForWindows, page9)
                    report10.ExportToDisk(ExportFormatType.WordForWindows, page10)
                    report11.ExportToDisk(ExportFormatType.WordForWindows, page11)
                    report12.ExportToDisk(ExportFormatType.WordForWindows, page12)
                ElseIf Methodes.ToUpper = "PSC" Then
                    report0.ExportToDisk(ExportFormatType.WordForWindows, PageGard)
                ElseIf Methodes.ToUpper = "PSO" Then
                    report0.ExportToDisk(ExportFormatType.WordForWindows, PageGard)
                    report1.ExportToDisk(ExportFormatType.WordForWindows, page0)
                    report2.ExportToDisk(ExportFormatType.WordForWindows, page1)
                    report3.ExportToDisk(ExportFormatType.WordForWindows, page2)
                End If

            ElseIf TypeMarches.ToLower = "travaux" Then
                If Methodes.ToUpper = "AOI" Or Methodes.ToUpper = "AON" Then
                    PageGarde.ExportToDisk(ExportFormatType.WordForWindows, PageGard)
                    report0.ExportToDisk(ExportFormatType.WordForWindows, page0)
                    report1.ExportToDisk(ExportFormatType.WordForWindows, page1)
                    report2.ExportToDisk(ExportFormatType.WordForWindows, page2)
                    report3.ExportToDisk(ExportFormatType.WordForWindows, page3)
                    report4.ExportToDisk(ExportFormatType.WordForWindows, page4)
                    report5.ExportToDisk(ExportFormatType.WordForWindows, page5)
                    report6.ExportToDisk(ExportFormatType.WordForWindows, page6)
                    report7.ExportToDisk(ExportFormatType.WordForWindows, page7)
                    report8.ExportToDisk(ExportFormatType.WordForWindows, page8)
                    report9.ExportToDisk(ExportFormatType.WordForWindows, page9)
                    report10.ExportToDisk(ExportFormatType.WordForWindows, page10)
                    report11.ExportToDisk(ExportFormatType.WordForWindows, page11)
                ElseIf Methodes.ToUpper = "PSC" Then
                    report0.ExportToDisk(ExportFormatType.WordForWindows, PageGard)
                ElseIf Methodes.ToUpper = "PSO" Then
                    report0.ExportToDisk(ExportFormatType.WordForWindows, PageGard)
                    report1.ExportToDisk(ExportFormatType.WordForWindows, page0)
                    report2.ExportToDisk(ExportFormatType.WordForWindows, page1)
                    report3.ExportToDisk(ExportFormatType.WordForWindows, page2)
                ElseIf Methodes.ToUpper = "PSL" Then
                ElseIf Methodes.ToUpper = "DC" Then
                End If
            End If

            Dim oWord As New Word.Application
            Dim currentDoc As New Word.Document

            Dim NomFichierpdf As String = "DAO N°_" & FormatFileName(NumeroDAO.ToString, "") & ".pdf"
            Dim NomFichierWord As String = "DAO N°_" & FormatFileName(NumeroDAO.ToString, "") & ".docx"

            Try
                'Ajout de la page de garde
                currentDoc = oWord.Documents.Add(PageGard)
                Dim myRange As Word.Range = currentDoc.Bookmarks.Item("\endofdoc").Range

                If TypeMarches.ToLower = "fournitures" Then
                    If Methodes.ToUpper = "AOI" Or Methodes.ToUpper = "AON" Then
                        Dim mySection1 As Word.Section = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        'mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                        myRange.InsertFile(page0)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        'mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                        myRange.InsertFile(page1)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        myRange.InsertFile(page2)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        myRange.InsertFile(page3)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        'mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                        myRange.InsertFile(page4)
                        '
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                        myRange.InsertFile(page5)

                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                        myRange.InsertFile(page6)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                        myRange.InsertFile(page7)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                        myRange.InsertFile(page8)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                        myRange.InsertFile(page9)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                        myRange.InsertFile(page10)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                        myRange.InsertFile(page11)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                        myRange.InsertFile(page12)
                    ElseIf Methodes.ToUpper = "PSO" Then
                        Dim mySection1 As Word.Section = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        myRange.InsertFile(page0)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        myRange.InsertFile(page1)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        myRange.InsertFile(page2)
                    End If

                ElseIf TypeMarches.ToLower = "travaux" Then
                    If Methodes.ToUpper = "AOI" Or Methodes.ToUpper = "AON" Then
                        Dim mySection1 As Word.Section = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        'mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                        myRange.InsertFile(page0)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        myRange.InsertFile(page1)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        myRange.InsertFile(page2)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        myRange.InsertFile(page3)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        myRange.InsertFile(page4)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        ' mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                        myRange.InsertFile(page5)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        'mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                        myRange.InsertFile(page6)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        ' mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                        myRange.InsertFile(page7)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        ' mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                        myRange.InsertFile(page8)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        ' mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                        myRange.InsertFile(page9)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        'mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                        myRange.InsertFile(page10)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        'mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                        myRange.InsertFile(page11)
                    ElseIf Methodes.ToUpper = "PSO" Then
                        Dim mySection1 As Word.Section = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        myRange.InsertFile(page0)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        myRange.InsertFile(page1)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        myRange.InsertFile(page2)
                    End If
                End If

                Try
                    currentDoc.SaveAs2(FileName:=CheminSauvGarde & "\" & NomFichierWord.ToString, FileFormat:=Word.WdSaveFormat.wdFormatDocumentDefault)
                    currentDoc.SaveAs2(FileName:=CheminSauvGarde & "\" & NomFichierpdf.ToString, FileFormat:=Word.WdSaveFormat.wdFormatPDF)
                    currentDoc.Close(True)
                    oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)

                Catch exp As IOException
                    FinChargement()
                    FailMsg("Un exemplaire du dossier est ouvert par une auttre applicattion. Veuillez le fermer svp.")
                    currentDoc.Close(True)
                    oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                    Return False
                Catch ex As Exception
                    FinChargement()
                    FailMsg("Erreur de traitement" & ex.ToString)
                    currentDoc.Close(True)
                    oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                    Return False
                End Try

            Catch ex As Exception
                FinChargement()
                FailMsg("Erreur de traitement " & ex.ToString)
                currentDoc.Close(True)
                oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                Return False
            End Try

            Return True
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
            Return False
        End Try
    End Function

    Private Function GetImpEnvoiDossPSL(ByVal NumeroDAO As String, TypeMarche As String) As Boolean
        Try
            query = "select * from T_Fournisseur where NumeroDAO='" & EnleverApost(NumeroDAO.ToString) & "' and CodeProjet='" & ProjetEnCours & "'" ' and DateDepotDAO<>''"
                    Dim dtSoumis As DataTable = ExcecuteSelectQuery(query)
            If dtSoumis.Rows.Count = 0 Then
                FinChargement()
                FailMsg("Aucun soumissoinnaire enregistré.")
                Return False
            End If

            Dim Chemin As String = lineEtat & "\Marches\DAO\Fournitures\DTAO\"

            'Enregistrement des services connexes
            If TypeMarche.ToLower = "fournitures" Then
                GetSaveTampServiceConnexe(NumeroDAO)
            Else
                Chemin = "\Marches\DAO\Travaux\PSL\"
            End If
            FinChargement()

            For Each rw As DataRow In dtSoumis.Rows
                DebutChargement(True, "Consolidation du dossier de " & MettreApost(rw("NomFournis").ToString.Split(" ")(0)) & " en cours...")

                Dim report1, report2, report3, report4 As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table
                Dim DatSet = New DataSet

                If TypeMarche.ToLower = "fournitures" Then
                    report1.Load(Chemin & "PSL\DAO_Fourniture_1_PSL_Fournisseur.rpt")
                    report2.Load(Chemin & "PSL\DAO_Fourniture_2_PSL.rpt")
                    report3.Load(Chemin & "PSL\DAO_Fourniture_3_PSL.rpt")
                    report4.Load(Chemin & "PSL\DAO_Fourniture_4_PSL.rpt")
                Else
                    report1.Load(Chemin & "1_Travaux_RCI_PSL.rpt")
                    report2.Load(Chemin & "2_Travaux_RCI_PSL.rpt")
                    report3.Load(Chemin & "3_Travaux_RCI_PSL.rpt")
                    report4.Load(Chemin & "4_Travaux_RCI_PSL.rpt")
                End If

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = report1.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next
                report1.SetDataSource(DatSet)
                report1.SetParameterValue("NumDAO", EnleverApost(NumeroDAO))
                report1.SetParameterValue("CodeProjet", ProjetEnCours)
                report1.SetParameterValue("IdFournisseur", rw("CodeFournis"))

                If TypeMarche.ToLower = "fournitures" Then
                    CrTables = report3.Database.Tables
                    For Each CrTable In CrTables
                        crtableLogoninfo = CrTable.LogOnInfo
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo
                        CrTable.ApplyLogOnInfo(crtableLogoninfo)
                    Next
                    report3.SetDataSource(DatSet)
                    report3.SetParameterValue("NumDAO", EnleverApost(NumeroDAO))
                    report3.SetParameterValue("CodeProjet", ProjetEnCours)
                    'Else
                End If

                'Enregistrement automatique *************************
                Dim CheminSauvGarde As String = ""
                Dim NomDossier As String = ""

                NomDossier = Environ$("TEMP") & "\DAO\" & TypeMarche.ToString & "\PSL\" & FormatFileName(NumeroDAO.ToString, "")
                CheminSauvGarde = line & "\DAO\" & TypeMarche.ToString & "\PSL\" & FormatFileName(NumeroDAO.ToString, "")

                If (Directory.Exists(NomDossier) = False) Then
                    Directory.CreateDirectory(NomDossier)
                End If
                If (Directory.Exists(CheminSauvGarde) = False) Then
                    Directory.CreateDirectory(CheminSauvGarde)
                End If

                Dim page1 = NomDossier & "\" & "DAO_Fourniture_1_PSL_Fournisseur.doc"
                Dim page2 = NomDossier & "\" & "DAO_Fourniture_2_PSL.doc"
                Dim page3 = NomDossier & "\" & "DAO_Fourniture_3_PSL.doc"
                Dim page4 = NomDossier & "\" & "DAO_Fourniture_4_PSL.doc"

                report1.ExportToDisk(ExportFormatType.WordForWindows, page1)
                report2.ExportToDisk(ExportFormatType.WordForWindows, page2)
                report3.ExportToDisk(ExportFormatType.WordForWindows, page3)
                report4.ExportToDisk(ExportFormatType.WordForWindows, page4)

                Dim oWord As New Word.Application
                Dim currentDoc As New Word.Document

                Dim NomFichierpdf As String = "DAO N°_" & rw("CodeFournis").ToString & FormatFileName(NumeroDAO.ToString, "") & ".pdf"
                Dim NomFichierWord As String = "DAO N°_" & rw("CodeFournis").ToString & FormatFileName(NumeroDAO.ToString, "") & ".docx"

                Try
                    'Ajout de la premiere page
                    currentDoc = oWord.Documents.Add(page1)
                    Dim myRange As Word.Range = currentDoc.Bookmarks.Item("\endofdoc").Range
                    Dim mySection1 As Word.Section = AjouterNouvelleSectionDocument(currentDoc, myRange)
                    ' mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                    myRange.InsertFile(page2)
                    mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                    'mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                    myRange.InsertFile(page3)
                    mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                    myRange.InsertFile(page4)

                    Try
                        currentDoc.SaveAs2(FileName:=CheminSauvGarde & "\" & NomFichierWord.ToString, FileFormat:=Word.WdSaveFormat.wdFormatDocumentDefault)
                        currentDoc.SaveAs2(FileName:=CheminSauvGarde & "\" & NomFichierpdf.ToString, FileFormat:=Word.WdSaveFormat.wdFormatPDF)
                        currentDoc.Close(True)
                        oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)

                    Catch exp As IOException
                        FinChargement()
                        FailMsg("Un exemplaire du dossier est ouvert par une auttre applicattion. Veuillez le fermer svp.")
                        currentDoc.Close(True)
                        oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                        Return False
                    Catch ex As Exception
                        FinChargement()
                        FailMsg("Erreur de traitement" & ex.ToString)
                        currentDoc.Close(True)
                        oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                        Return False
                    End Try

                Catch ex As Exception
                    FinChargement()
                    FailMsg("Erreur de traitement " & ex.ToString)
                    currentDoc.Close(True)
                    oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                    Return False
                End Try
                FinChargement()
            Next

        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
            Return False
        End Try
        Return True
    End Function

    Private Sub GetSaveDonneesPSC(ByVal NumeroDAO As String)
        Try
            ExecuteNonQuery("delete from t_tamp_lotdao where NumDAO='" & NumeroDAO & "' and CodeProjet='" & ProjetEnCours & "' and CodeUtils='" & CodeOperateurEnCours & "'")
            ExecuteNonQuery("delete from t_tamp_lotdao_souslot where NumDAO='" & NumeroDAO & "' and CodeProjet='" & ProjetEnCours & "' and CodeUtils='" & CodeOperateurEnCours & "'")

            Dim dt As DataTable = ExcecuteSelectQuery("select * from t_spectechfourniture where NumeroDAO='" & NumeroDAO & "'")
            For Each rw In dt.Rows
                If rw("CodeSousLot").ToString = "" Then
                    ExecuteNonQuery("insert into t_tamp_lotdao values('" & rw("CodeLot") & "', '" & GetLibelleLot(rw("CodeLot"), NumeroDAO) & "', '" & NumeroDAO & "', '" & rw("RefSpecFournit") & "', '" & rw("DescripFournit") & "', '" & GetListeSpec(rw("RefSpecFournit")) & "', '" & rw("QteFournit") & "', '" & ProjetEnCours & "', '" & CodeOperateurEnCours & "')")
                Else
                    query = "select count(*) from t_tamp_lotdao where CodeLot='" & rw("CodeLot") & "' and LibelleLot='" & GetLibelleLot(rw("CodeLot"), NumeroDAO) & "' and NumDAO='" & NumeroDAO & "' AND CodeProjet='" & ProjetEnCours & "' and CodeUtils='" & CodeOperateurEnCours & "'"
                    If Val(ExecuteScallar(query)) = 0 Then
                        ExecuteNonQuery("insert into t_tamp_lotdao values('" & rw("CodeLot") & "', '" & GetLibelleLot(rw("CodeLot"), NumeroDAO) & "', '" & NumeroDAO & "', NULL, NULL, NULL, NULL, '" & ProjetEnCours & "', '" & CodeOperateurEnCours & "')")
                    End If
                    ExecuteNonQuery("insert into t_tamp_lotdao_souslot values('" & rw("CodeLot") & "', '" & rw("CodeSousLot") & "', '" & GetLibelleLot(rw("CodeLot"), NumeroDAO, rw("CodeSousLot")) & "', '" & NumeroDAO & "', '" & rw("RefSpecFournit") & "', '" & rw("DescripFournit") & "', '" & GetListeSpec(rw("RefSpecFournit")) & "', '" & rw("QteFournit") & "', '" & ProjetEnCours & "', '" & CodeOperateurEnCours & "')")
                End If
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub GetSaveTampServiceConnexe(ByVal NumeroDAO As String)
        Try
            ExecuteNonQuery("delete from t_tamp_lotdao where NumDAO='" & NumeroDAO & "' and CodeProjet='" & ProjetEnCours & "' and CodeUtils='" & CodeOperateurEnCours & "'")
            ExecuteNonQuery("delete from t_tamp_lotdao_souslot where NumDAO='" & NumeroDAO & "' and CodeProjet='" & ProjetEnCours & "' and CodeUtils='" & CodeOperateurEnCours & "'")

            Dim dtLot As DataTable = ExcecuteSelectQuery("select * from t_lotdao where NumeroDAO='" & NumeroDAO & "'")

            For Each rwLot In dtLot.Rows
                Dim dtSousLot As DataTable = ExcecuteSelectQuery("select S.* from t_lotdao_souslot as S, t_lotdao as L where S.RefLot=L.RefLot and S.NumeroDAO='" & NumeroDAO & "' AND L.RefLot='" & rwLot("RefLot") & "'")

                If dtSousLot.Rows.Count > 0 Then
                    query = "select count(*) from t_tamp_lotdao where CodeLot='" & rwLot("CodeLot") & "' and LibelleLot='" & rwLot("LibelleLot") & "' and NumDAO='" & NumeroDAO & "' AND CodeProjet='" & ProjetEnCours & "' and CodeUtils='" & CodeOperateurEnCours & "'"
                    If Val(ExecuteScallar(query)) = 0 Then
                        ExecuteNonQuery("insert into t_tamp_lotdao values('" & rwLot("CodeLot") & "', '" & rwLot("LibelleLot") & "', '" & NumeroDAO & "', NULL, NULL, NULL, NULL, '" & ProjetEnCours & "', '" & CodeOperateurEnCours & "')")
                    End If
                    For Each rwSousLot In dtSousLot.Rows
                        ExecuteNonQuery("insert into t_tamp_lotdao_souslot values('" & rwLot("CodeLot") & "', '" & rwSousLot("CodeSousLot") & "', '" & rwSousLot("LibelleSousLot") & "', '" & NumeroDAO & "', NULL, NULL, '" & ListeServiceConnexe(NumeroDAO, rwLot("CodeLot"), rwSousLot("CodeSousLot")) & "', NULL, '" & ProjetEnCours & "', '" & CodeOperateurEnCours & "')")
                    Next
                Else
                    ExecuteNonQuery("insert into t_tamp_lotdao values('" & rwLot("CodeLot") & "', '" & rwLot("LibelleLot") & "', '" & NumeroDAO & "', NULL, NULL, '" & ListeServiceConnexe(NumeroDAO, rwLot("CodeLot"), "") & "', NULL, '" & ProjetEnCours & "', '" & CodeOperateurEnCours & "')")
                End If
            Next

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Function ListeServiceConnexe(ByVal NumeroDAO As String, CodeLot As String, CodeSousLot As String) As String
        Dim ListeService As String = ""
        Try
            Dim dt As DataTable = ExcecuteSelectQuery("select * from t_dao_service_connexe where NumeroDAO='" & NumeroDAO & "' and CodeLot='" & CodeLot & "' and CodeSousLot='" & CodeSousLot & "'")
            For Each rw In dt.Rows
                If ListeService = "" Then
                    ListeService = rw("LibelleService").ToString
                Else
                    ListeService = ListeService & vbNewLine & rw("LibelleService").ToString
                End If
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        Return ListeService
    End Function

    Private Function GetListeSpec(ByVal RefSpecFournit As Decimal) As String
        Dim LibelleLot As String = ""
        Try
            Dim dt As DataTable = ExcecuteSelectQuery("select * from t_spectechcaract where RefSpecFournit='" & RefSpecFournit & "'")
            For Each rw In dt.Rows
                If LibelleLot = "" Then
                    LibelleLot = rw("LibelleCaract") & " : " & rw("ValeurCaract")
                Else
                    LibelleLot = LibelleLot & vbNewLine & rw("LibelleCaract") & " : " & rw("ValeurCaract")
                End If
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        Return LibelleLot
    End Function

    Private Function GetLibelleLot(ByVal CodeLot As String, ByVal NumeroDAO As String, Optional CodeSousLot As String = "") As String
        Dim LibelleLot As String = ""
        Try
            Dim RefLot = ExecuteScallar("select RefLot from t_lotdao where CodeLot='" & CodeLot & "' and  NumeroDAO='" & NumeroDAO & "'")
            If CodeSousLot = "" Then
                LibelleLot = ExecuteScallar("select LibelleLot from t_lotdao where RefLot='" & RefLot & "'")
            Else
                LibelleLot = ExecuteScallar("select LibelleSousLot from t_lotdao_souslot where RefLot='" & RefLot & "' and  NumeroDAO='" & NumeroDAO & "' and CodeSousLot='" & CodeSousLot & "'")
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        Return LibelleLot
    End Function

    Private Function RemplirTempSection(ByVal NumeroDoss As String, ByVal TypeMarche As String, ByVal Methode As String) As Boolean
        Try
            ExecuteNonQuery("delete from t_dao_section_tampon where NumeroDAO='" & NumeroDoss & "' and CodeProjet='" & ProjetEnCours & "' and CodeUtils='" & CodeOperateurEnCours & "'")
            Dim CodeSection As Array
            Dim OK As Boolean = False

            If (TypeMarche.ToLower = "fournitures") Then
                If Methode.ToUpper = "AOI" Or Methode.ToUpper = "AON" Then
                    CodeSection = {"IS 1.2(a)", "IS 4.5", "IS 4.8(a), 4.8(b) et 5.1", "IS 11.1(j)", "IS 13.1", "IS 14.5", "IS 14.7", "IS 14.8(b)(i) et (c)(v)", "IS 14.8(a)(iii)(ii) et (c)(v)", "IS 15.1", "IS 16.4", "IS 17.2(a)", "IS 17.2(b)", "IS 18.3(a)", "IS 19.1", "IS 19.3(d)", "IS 19.9(a)", "IS 19.9(b)", "IS 20.3", "IS 22.1(a)", "IS 22.1(b)", "IS 25.1", "IS 25.6", "IS 30.3", "IS 33.1", "IS 34.6(a)", "IS 34.6(b)", "IS 34.6(c)", "IS 34.6(d)", "IS 34.6(e)", "IS 34.6(f)", "IS 34.6(g)", "IS 42.1(a)", "IS 42.1(b)", "IS 45.1"}
                    OK = True
                End If
            ElseIf (TypeMarche.ToLower = "Travaux".ToLower) Then
                If Methode.ToUpper = "AOI" Or Methode.ToUpper = "AON" Then 'Travaux
                    CodeSection = {"IS 1.2(a)", "IS 4.5", "IS 11.1(h)", "IS 13.1", "IS 13.2", "IS 13.4", "IS 14.5", "IS 18.3(a)", "IS 19.3(d)", "IS 19.9", "IS 20.3", "IS 25.6", "IS 30.3", "IS 33.1", "IS 34.2", "IS 47.1"}
                    OK = True
                End If
            End If

            Dim dt0 As New DataTable
            If OK = True Then
                For i = 0 To CodeSection.Length - 1
                    dt0 = ExcecuteSelectQuery("SELECT Description from t_dao_section_tampon where CodeSection='" & CodeSection(i) & "' and NumeroDAO='" & NumeroDoss.ToString & "' and CodeProjet ='" & ProjetEnCours & "'")

                    If dt0.Rows.Count > 0 Then
                        For Each rw0 In dt0.Rows
                            ExecuteNonQuery("Insert into t_dao_section_tampon values(NULL, '" & NumeroDoss.ToString & "', '" & CodeSection(i) & "', '" & EnleverApost(rw0("Description").ToString) & "', '" & CodeOperateurEnCours & "', '" & ProjetEnCours & "')")
                        Next
                    Else
                        ExecuteNonQuery("Insert into t_dao_section_tampon values(NULL, '" & NumeroDoss.ToString & "', '" & CodeSection(i) & "', '" & GetValDefautSection(CodeSection(i), TypeMarche, Methode) & "', '" & CodeOperateurEnCours & "',  '" & ProjetEnCours & "')")
                    End If
                Next
            End If
        Catch ex As Exception
            FinChargement()
            FailMsg("Erreur dans l'enregistrement des sections." & ex.ToString)
            Return False
        End Try
        Return True
    End Function

    Private Function GetValDefautSection(ByVal CodeSection As String, ByVal TypeMarche As String, ByVal Methode As String) As String
        Dim DescriptionSection As String = ""
        ' CodeSection = { "IS 4.8(a), 4.8(b) et 5.1"} ******** Code non retrouver **********

        If TypeMarche.ToLower = "fournitures" Then
            If CodeSection.ToString = "IS 30.3" Then ' « la moyenne » ou « la valeur la plus élevée »] 
                DescriptionSection = "la valeur la plus élevée"
            ElseIf CodeSection.ToString = "IS 4.5" Then
                DescriptionSection = "http://www.worldbank.org/debarr"
            ElseIf CodeSection.ToString = "IS 13.1" Or CodeSection.ToString = "IS 14.5" Then 'Variante
                DescriptionSection = "ne seront pas"
            ElseIf CodeSection.ToString = "IS 15.1" Or CodeSection.ToString = "IS 17.2(a)" Or CodeSection.ToString = "IS 17.2(b)" Then
                DescriptionSection = "n’est pas"
            ElseIf CodeSection.ToString = "IS 19.1" Then 'Garantie d'offre
                DescriptionSection = " n’est pas"
            ElseIf CodeSection.ToString = "IS 19.3(d)" Then
                DescriptionSection = "Néant"
            ElseIf CodeSection.ToString = "IS 22.1(a)" Or CodeSection.ToString = "IS 45.1" Then
                DescriptionSection = "n’aura pas"
            ElseIf CodeSection.ToString = "IS 33.1" Then
                DescriptionSection = "ne sera pas"
            ElseIf CodeSection.ToString = "IS 34.6(a)" Or CodeSection.ToString = "IS 34.6(b)" Or CodeSection.ToString = "IS 34.6(c)" Or CodeSection.ToString = "IS 34.6(d)" Or CodeSection.ToString = "IS 34.6(e)" Or CodeSection.ToString = "IS 34.6(f)" Then
                DescriptionSection = "non"
            Else
                DescriptionSection = "Sans Objet"
            End If

        ElseIf (TypeMarche.ToLower = "travaux") Then

            If Methode.ToUpper = "AOI" Or Methode.ToUpper = "AON" Then
                If CodeSection.ToString = "IS 4.5" Then
                    DescriptionSection = "http://www.worldbank.org/debarr"
                ElseIf CodeSection.ToString = "IS 13.1" Or CodeSection = "IS 13.2" Or CodeSection = "IS 13.4" Then
                    DescriptionSection = "ne sont pas"
                ElseIf CodeSection.ToString = "IS 14.5" Then '[révisables/fermes]. 
                    DescriptionSection = "fermes"
                ElseIf CodeSection.ToString = "IS 19.3(d)" Then
                    Return "Néant"
                ElseIf CodeSection.ToString = "IS 30.3" Then '« valeur moyenne »] ou [« valeur la plus élevée »] 
                    DescriptionSection = "valeur moyenne"
                ElseIf CodeSection.ToString = "IS 33.1" Then '[sera/ne sera pas] 
                    DescriptionSection = "ne sera pas"
                ElseIf CodeSection.ToString = "IS 47.1" Then '[aura] ou [n’aura pas] 
                    DescriptionSection = "n’aura pas"
                Else
                    DescriptionSection = "Sans Objet"
                End If
            End If
        End If

        Return DescriptionSection
    End Function

    Private Sub PdfToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PdfToolStripMenuItem.Click
        If LayoutView1.RowCount > 0 Then
            Try
                If PourAjoutModifDansDB = 1 Then
                    SuccesMsg("Veuillez enregistrer le dossier en cours.")
                    Exit Sub
                ElseIf (PourAjoutModifDansDB = 2) Then
                    SuccesMsg("Veuillez enregistrer et fermer le dossier en cours.")
                    Exit Sub
                End If

                Dim drx = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
                Dim NumeroDAO As String = drx("N°").ToString
                Dim NomRepCheminSauve As String = line & "\DAO\" & drx("Type").ToString & "\" & drx("Méthode").ToString & "\" & FormatFileName(NumeroDAO.ToString, "")
                Dim NomFichier As String = "\DAO N°_" & FormatFileName(NumeroDAO.ToString, "") & ".pdf"

                If drx("Méthode").ToString.ToUpper = "PSL" Then ' drx("Type").ToString.ToLower = "Fournitures".ToLower And
                    Dim NewEnvoiDoss As New EnvoiDossSoumissionnaire
                    NewEnvoiDoss.NumeroDAO = drx("N°").ToString
                    NewEnvoiDoss.ExtensionExport = ".pdf"
                    NewEnvoiDoss.TypesMarches = drx("Type").ToString
                    NewEnvoiDoss.Text = "Soumissoinnaire"
                    NewEnvoiDoss.BtEnregComm.Text = "Exporter"
                    NewEnvoiDoss.BtEnregComm.ToolTip = "Exporter"
                    NewEnvoiDoss.BtEnregComm.Image = My.Resources.Resources.ExportToPDF_16x16
                    NewEnvoiDoss.ShowDialog()
                Else
                    If File.Exists(NomRepCheminSauve & NomFichier) = True Then
                        If ExporterPDF(NomRepCheminSauve.ToString & NomFichier.ToString, "DossierAppelOffre.pdf") = False Then
                            Exit Sub
                        End If
                    Else
                        FailMsg("Le fichier à exporter n'existe pas ou a été supprimé.")
                    End If
                End If

            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub WordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WordToolStripMenuItem.Click
        If LayoutView1.RowCount > 0 Then
            Try
                If PourAjoutModifDansDB = 1 Then
                    SuccesMsg("Veuillez enregistrer le dossier en cours.")
                    Exit Sub
                ElseIf (PourAjoutModifDansDB = 2) Then
                    SuccesMsg("Veuillez enregistrer et fermer le dossier en cours.")
                    Exit Sub
                End If

                Dim drx = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
                Dim NumeroDAO As String = drx("N°").ToString
                Dim NomRepCheminSauve As String = line & "\DAO\" & drx("Type").ToString & "\" & drx("Méthode").ToString & "\" & FormatFileName(NumeroDAO.ToString, "") & "\DAO N°_" & FormatFileName(NumeroDAO.ToString, "") & ".docx"

                If drx("Méthode").ToString.ToUpper = "PSL" Then 'drx("Type").ToString.ToLower = "Fournitures".ToLower And
                    Dim NewEnvoiDoss As New EnvoiDossSoumissionnaire
                    NewEnvoiDoss.NumeroDAO = drx("N°").ToString
                    NewEnvoiDoss.ExtensionExport = ".docx"
                    NewEnvoiDoss.TypesMarches = drx("Type").ToString
                    NewEnvoiDoss.Text = "Soumissoinnaire"
                    NewEnvoiDoss.BtEnregComm.Text = "Exporter"
                    NewEnvoiDoss.BtEnregComm.ToolTip = "Exporter"
                    NewEnvoiDoss.BtEnregComm.Image = My.Resources.Resources.ExportToRTF_16x16
                    NewEnvoiDoss.ShowDialog()
                Else
                    If File.Exists(NomRepCheminSauve) = True Then
                        If ExporterWORDfOrmatDocx(NomRepCheminSauve.ToString, "Dossier_Appel_Offre.docx") = False Then
                            Exit Sub
                        End If
                    Else
                        FailMsg("Le fichier à exporter n'existe pas ou a été supprimé.")
                    End If
                End If

            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub ValiderLeDossierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValiderLeDossierToolStripMenuItem.Click
        If LayoutView1.RowCount > 0 Then
            Try
                If PourAjoutModifDansDB = 1 Then
                    SuccesMsg("Veuillez enregistrer le dossier en cours.")
                    Exit Sub
                ElseIf (PourAjoutModifDansDB = 2) Then
                    SuccesMsg("Veuillez enregistrer et fermer le dossier en cours.")
                    Exit Sub
                End If

                Dim drx = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
                Dim NumeroDAO As String = EnleverApost(drx("N°").ToString)

                DebutChargement(True, "Vérification des données en cours...")

                If drx("Statut").ToString = "Annulé" Then
                    FinChargement()
                    FailMsg("Impossible de valider un dossier annulé.")
                    Exit Sub
                End If

                If drx("Statut").ToString = "Terminé" Then
                    FinChargement()
                    FailMsg("Impossible de valider un marche executé.")
                    Exit Sub
                End If

                If CBool(drx("DossValider")) = True Then
                    FinChargement()
                    FailMsg("Ce dossier a été validé.")
                    Exit Sub
                End If

                'Verifier si tous les articles ont des caracteristique
                If drx("Type").ToString.ToLower = "Fournitures".ToLower Then
                    If Val(ExecuteScallar("select count(*) from t_spectechfourniture where NumeroDAO='" & NumeroDAO & "'")) = 0 Then
                        FinChargement()
                        FailMsg("Impossible de valider ce dossier. Car" & vbNewLine & "aucune spécification technique définie.")
                        Exit Sub
                    End If

                    Dim ExisteCara As Decimal = Val(ExecuteScallar("Select count(*) from t_spectechfourniture where NumeroDAO='" & NumeroDAO & "' and RefSpecFournit NOT IN(select RefSpecFournit from t_spectechcaract)"))
                    If ExisteCara > 0 Then
                        FinChargement()
                        FailMsg("Impossible de valider ce dossier. Car il" & vbNewLine & "existe des articles sans caractéristiques.")
                        Exit Sub
                    End If

                Else 'Travaux
                    query = "SELECT COUNT(CodeLot) FROM `t_lotdao` WHERE NumeroDAO='" & NumeroDAO & "'"
                    p1 = "SELECT COUNT(DISTINCT CodeLot) FROM `t_dqesection` WHERE NumeroDAO='" & NumeroDAO & "'"
                    If Val(ExecuteScallar(query)) <> Val(ExecuteScallar(p1)) Then
                        FinChargement()
                        FailMsg("Impossible de valider ce dossier. Car il existe" & vbNewLine & "des lots qui ne sont pas liés à des DQE.")
                        Exit Sub
                    End If

                    query = "SELECT COUNT(CodeSousLot) FROM `t_lotdao_souslot` WHERE NumeroDAO='" & NumeroDAO & "'"
                    p1 = "SELECT COUNT(DISTINCT CodeSousLot) FROM `t_dqesection` WHERE NumeroDAO='" & NumeroDAO & "' and CodeSousLot <>''"

                    If Val(ExecuteScallar(query)) <> Val(ExecuteScallar(p1)) Then
                        FinChargement()
                        FailMsg("Impossible de valider ce dossier. Car il existe" & vbNewLine & "des sous lots qui ne sont pas liés à des DQE.")
                        Exit Sub
                    End If
                End If

                Dim MessageValidation As String = "Voulez-vous valider ce dossier ?"
                query = "select count(*) from T_DAO_PostQualif where NumeroDAO='" & NumeroDAO & "'"
                If Val(ExecuteScallar(query)) = 0 Then
                    MessageValidation = "Votre dossier ne subira pas d'examen post qualification." & vbNewLine & "Car aucun critère post qualification definie." & vbNewLine & "Êtes-vous sûrs de vouloir valider ce dossier ?"
                    'FinChargement()
                    'FailMsg("Impossible de valider ce dossier. Car" & vbNewLine & "aucun critère post qualification définie.")
                    'Exit Sub
                End If
                FinChargement()

                If ConfirmMsg(MessageValidation) = DialogResult.Yes Then

                    DebutChargement(True, "Traitement de la validation du dossier en cours...")

                    Dim dt As DataTable = ExcecuteSelectQuery("SELECT * FROM t_commission WHERE NumeroDAO='" & NumeroDAO & "'") 'AND TypeComm='COJO'"
                    For Each rw In dt.Rows
                        Dim Authkey = GenererToken(NumeroDAO, rw("CodeMem").ToString, "DAO", DB)
                        Dim ID() = Authkey.Split(":")
                        Dim token = ID(0).ToString
                        ExecuteNonQuery("UPDATE t_commission SET AuthKey='" & token & "' WHERE CodeMem='" & rw("CodeMem").ToString & "'")

                        'InputBox("Cle", "dd", Authkey)

                        If envoieMail(rw("NomMem").ToString, rw("EmailMem").ToString, Authkey) = False Then
                            FinChargement()
                            Exit Sub
                        End If
                        'FinChargement()
                        'SuccesMsg("Dossier validé avec succès.")
                        'Exit Sub

                        ExecuteNonQuery("INSERT INTO t_dao_evalcojo(NumeroDAO, id_cojo) VALUES('" & NumeroDAO & "','" & rw("CodeMem").ToString & "')")
                    Next
                    ExecuteNonQuery("UPDATE t_dao SET DossValider=TRUE WHERE NumeroDAO='" & NumeroDAO & "'")

                    FinChargement()
                    SuccesMsg("Dossier validé avec succès.")

                    LayoutView1.SetFocusedRowCellValue("DossValider", True)
                End If
            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub BailleurToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BailleurToolStripMenuItem.Click
        If LayoutView1.RowCount > 0 Then
            Try
                If PourAjoutModifDansDB = 1 Then
                    SuccesMsg("Veuillez enregistrer le dossier en cours.")
                    Exit Sub
                ElseIf (PourAjoutModifDansDB = 2) Then
                    SuccesMsg("Veuillez enregistrer et fermer le dossier en cours.")
                    Exit Sub
                End If

                Dim drx = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
                Dim NumeroDAO As String = drx("N°").ToString

                If drx("Statut").ToString = "Annulé" Then
                    FailMsg("Ce marché a été annulé.")
                    Exit Sub
                End If

                If drx("Statut").ToString = "Terminé" Then
                    FailMsg("Ce marché a été executer.")
                    Exit Sub
                End If

                'Info de l'envoi de l'email
                If GetVerifDonneEmailBailleur(drx("N°").ToString) = False Then
                    FinChargement()
                    Exit Sub
                End If

                Dim CheminFile As String = line & "\DAO\" & drx("Type").ToString & "\" & drx("Méthode").ToString & "\" & FormatFileName(NumeroDAO.ToString, "")
                Dim NomFichierWord As String = "\DAO N°_" & FormatFileName(NumeroDAO.ToString, "") & ".docx"

                Dim dtSoumis As New DataTable
                If drx("Méthode").ToString.ToUpper = "PSL" Then ' And drx("Type").ToString.ToLower = "Fournitures".ToLower 
                    query = "select * from T_Fournisseur where NumeroDAO='" & EnleverApost(drx("N°").ToString) & "' and CodeProjet='" & ProjetEnCours & "'" ' and DateDepotDAO<>''"
                    dtSoumis = ExcecuteSelectQuery(query)
                    If dtSoumis.Rows.Count = 0 Then
                        FailMsg("Aucun soumissoinnaire enregistré.")
                        Exit Sub
                    End If

                    NomFichierWord = "\DAO N°_Bailleur" & FormatFileName(drx("N°").ToString, "") & ".docx"
                    If File.Exists(CheminFile & NomFichierWord) = False Then
                        'Generer le dossier du bailleur
                        If GetGenererDossBailleur(drx("N°").ToString, drx("Type").ToString) = False Then
                            FinChargement()
                            Exit Sub
                        End If
                    End If
                End If

                If Not File.Exists(CheminFile & NomFichierWord) Then
                    FinChargement()
                    FailMsg("Le dossier à envoyer au bailleur de fonds  n'existe pas ou a été supprimé.")
                    Exit Sub
                End If

                Dim MessageText As String = ""
                If CBool(drx("DossValider")) = True Then
                    MessageText = "Le bailleur de fonds a déjà validé le dossier." & vbNewLine & "Voulez-vous l'envoyer à nouveau ?"
                Else
                    MessageText = "Confirmez-vous l'envoi du dossier d'appel d'offre au bailleur [ " & MettreApost(rwDossDAO.Rows(0)("InitialeBailleur").ToString) & " ] ?"
                End If

                If ConfirmMsg(MessageText) = DialogResult.Yes Then
                    Try
                        DebutChargement(True, "Envoi du dossier d'appel d'offre au bailleur...")
                        'Envoi du dossier
                        If EnvoiMailRapport(NomBailleurRetenuDAO, NumeroDAO.ToString, EmailDestinatauerDAO, CheminFile & NomFichierWord, EmailCoordinateurProjetDAO, EmailResponsablePMDAO, "Dossier d'appel d'offre", "DAO") = False Then
                            FinChargement()
                            Exit Sub
                        End If

                        FinChargement()
                        SuccesMsg("Dossier envoyé avec succès.")
                    Catch ep As IOException
                        FinChargement()
                        SuccesMsg("Le fichier est utilisé par une autre application" & vbNewLine & "Veuillez le fermer svp.")
                    Catch ex As Exception
                        FinChargement()
                        FailMsg(ex.ToString)
                    End Try
                End If
            Catch exs As Exception
                FailMsg(exs.ToString)
            End Try
        End If
    End Sub

    Private Function GetGenererDossBailleur(ByVal NumeroDAO As String, ByVal TypeMarche As String) As Boolean
        Try
            DebutChargement(True, "Vérification des informations en cours...")

            'Enregistrement des services connexes
            Dim Chemin As String = lineEtat & "\Marches\DAO\Fournitures\DTAO\"
            If TypeMarche.ToLower = "fournitures" Then
                GetSaveTampServiceConnexe(NumeroDAO)
            Else
                Chemin = "\Marches\DAO\Travaux\PSL\"
            End If

            Dim report1, report2, report3, report4 As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim DatSet = New DataSet

            If TypeMarche.ToLower = "fournitures" Then
                report1.Load(Chemin & "PSL\DAO_Fourniture_1_PSL_Bailleur.rpt")
                report2.Load(Chemin & "PSL\DAO_Fourniture_2_PSL.rpt")
                report3.Load(Chemin & "PSL\DAO_Fourniture_3_PSL.rpt")
                report4.Load(Chemin & "PSL\DAO_Fourniture_4_PSL.rpt")
            Else
                report1.Load(Chemin & ".rpt")
                report2.Load(Chemin & ".rpt")
                report3.Load(Chemin & ".rpt")
                report4.Load(Chemin & ".rpt")
            End If

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = report1.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next
            CrTables = report3.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next
            report1.SetDataSource(DatSet)
            report3.SetDataSource(DatSet)
            report1.SetParameterValue("NumDAO", EnleverApost(NumeroDAO))
            report1.SetParameterValue("CodeProjet", ProjetEnCours)
            report3.SetParameterValue("NumDAO", EnleverApost(NumeroDAO))
            report3.SetParameterValue("CodeProjet", ProjetEnCours)

            'Enregistrement automatique *************************
            Dim CheminSauvGarde As String = ""
            Dim NomDossier As String = ""

            NomDossier = Environ$("TEMP") & "\DAO\" & MettreApost(TypeMarche.ToString) & "\PSL\" & FormatFileName(NumeroDAO.ToString, "")
            CheminSauvGarde = line & "\DAO\" & EnleverApost(TypeMarche.ToString) & "\PSL\" & FormatFileName(NumeroDAO.ToString, "")

            If (Directory.Exists(NomDossier) = False) Then
                Directory.CreateDirectory(NomDossier)
            End If
            If (Directory.Exists(CheminSauvGarde) = False) Then
                Directory.CreateDirectory(CheminSauvGarde)
            End If

            Dim page1 = NomDossier & "\" & "DAO_Fourniture_1_PSL_Bailleur.doc"
            Dim page2 = NomDossier & "\" & "DAO_Fourniture_2_PSL.doc"
            Dim page3 = NomDossier & "\" & "DAO_Fourniture_3_PSL.doc"
            Dim page4 = NomDossier & "\" & "DAO_Fourniture_4_PSL.doc"

            report1.ExportToDisk(ExportFormatType.WordForWindows, page1)
            report2.ExportToDisk(ExportFormatType.WordForWindows, page2)
            report3.ExportToDisk(ExportFormatType.WordForWindows, page3)
            report4.ExportToDisk(ExportFormatType.WordForWindows, page4)

            Dim oWord As New Word.Application
            Dim currentDoc As New Word.Document

            Dim NomFichierWord As String = "DAO N°_Bailleur" & FormatFileName(NumeroDAO.ToString, "") & ".docx"

            Try
                'Ajout de la premiere page
                currentDoc = oWord.Documents.Add(page1)
                Dim myRange As Word.Range = currentDoc.Bookmarks.Item("\endofdoc").Range
                Dim mySection1 As Word.Section = AjouterNouvelleSectionDocument(currentDoc, myRange)
                'mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                myRange.InsertFile(page2)
                mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                'mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                myRange.InsertFile(page3)
                mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                myRange.InsertFile(page4)

                Try
                    currentDoc.SaveAs2(FileName:=CheminSauvGarde & "\" & NomFichierWord.ToString, FileFormat:=Word.WdSaveFormat.wdFormatDocumentDefault)
                    currentDoc.Close(True)
                    oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)

                Catch exp As IOException
                    FinChargement()
                    FailMsg("Un exemplaire du dossier est ouvert par une auttre applicattion. Veuillez le fermer svp.")
                    currentDoc.Close(True)
                    oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                    Return False
                Catch ex As Exception
                    FinChargement()
                    FailMsg("Erreur de traitement" & ex.ToString)
                    currentDoc.Close(True)
                    oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                    Return False
                End Try

            Catch ex As Exception
                FinChargement()
                FailMsg("Erreur de traitement " & ex.ToString)
                currentDoc.Close(True)
                oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                Return False
            End Try
            FinChargement()
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
            Return False
        End Try
        Return True
    End Function

    Private Sub SoumissionnaireToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SoumissionnaireToolStripMenuItem.Click
        If LayoutView1.RowCount > 0 Then
            Try
                If PourAjoutModifDansDB = 1 Then
                    SuccesMsg("Veuillez enregistrer le dossier en cours.")
                    Exit Sub
                ElseIf (PourAjoutModifDansDB = 2) Then
                    SuccesMsg("Veuillez enregistrer et fermer le dossier en cours.")
                    Exit Sub
                End If

                Dim drx = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
                Dim NumeroDAO As String = drx("N°").ToString

                If drx("Statut").ToString = "Annulé" Then
                    FailMsg("Ce marché a été annulé.")
                    Exit Sub
                End If

                If drx("Statut").ToString = "Terminé" Then
                    FailMsg("Ce marché a été executer.")
                    Exit Sub
                End If

                If ListeMethodePrevue.Contains(drx("Méthode").ToString.ToUpper) = False Then
                    FailMsg("Aucun état prévu pour la méthode [" & drx("Méthode").ToString.ToUpper & "]")
                    Exit Sub
                End If

                'verifer si le bailleur de fonds a valider en tenant compte de la revu
                Dim Revu As String = ExecuteScallar("select RevuePrioPost from t_marche as m, t_dao as d where d.RefMarche=m.RefMarche and d.NumeroDAO='" & EnleverApost(drx("N°").ToString) & "'")
                If Revu.ToString = "Priori" And CBool(drx("DossValider").ToString) = False Then
                    FailMsg("Le bailleur de fonds doit valider le dossier avant d'envoyer aux soumissoinnaires.")
                    Exit Sub
                End If

                If Revu.ToString <> "Priori" And CBool(drx("DossValider").ToString) = False Then
                    FailMsg("Vous devez valider le dossier avant d'envoyer aux soumissoinnaires.")
                    Exit Sub
                End If

                Dim FichierExiste As Boolean = False
                Dim NomFichierWord As String = "\DAO N°_" & FormatFileName(drx("N°").ToString, "") & ".docx"

                query = "select * from T_Fournisseur where NumeroDAO='" & EnleverApost(drx("N°").ToString) & "' and CodeProjet='" & ProjetEnCours & "'" ' and DateDepotDAO<>''"
                Dim dtSoumis As DataTable = ExcecuteSelectQuery(query)
                If dtSoumis.Rows.Count = 0 Then
                    FailMsg("Aucun soumissoinnaire enregistré.")
                    Exit Sub
                End If

                Dim NomRepCheminSauve As String = line & "\DAO\" & drx("Type").ToString & "\" & drx("Méthode").ToString & "\" & FormatFileName(drx("N°").ToString, "")

                If drx("Méthode").ToString.ToUpper = "PSL" Then ' And drx("Type").ToString.ToLower = "Fournitures".ToLower 
                    'Verifier si le dossier de chaque soumissoinnaire existe
                    FichierExiste = True
                    For Each rw As DataRow In dtSoumis.Rows
                        NomFichierWord = "DAO N°_" & rw("CodeFournis").ToString & FormatFileName(drx("N°").ToString, "") & ".docx"
                        If Not File.Exists(NomRepCheminSauve & "\" & NomFichierWord) Then
                            FichierExiste = False
                        End If
                    Next
                ElseIf File.Exists(NomRepCheminSauve & NomFichierWord) Then
                    FichierExiste = True
                End If

                If FichierExiste = False Then 'Dossier Non genéré
                    If NewGenereDossDAO(drx("N°").ToString) = False Then
                        Exit Sub
                    End If
                End If

                Dim CheminEnvoiDoss As String = ""
                For Each rw1 As DataRow In dtSoumis.Rows
                    'Cas de dossier de PSL
                    If drx("Méthode").ToString.ToUpper = "PSL" Then ' And drx("Type").ToString.ToLower = "Fournitures".ToLower
                        NomFichierWord = "\DAO N°_" & rw1("CodeFournis").ToString & FormatFileName(drx("N°").ToString, "") & ".docx"
                    End If
                    CheminEnvoiDoss = NomRepCheminSauve & NomFichierWord

                    Try
                        If File.Exists(CheminEnvoiDoss) = True Then
                            DebutChargement(True, "Envoi du dossier d'appel d'offre à " & MettreApost(rw1("NomFournis").ToString.Split(" ")(0)) & "...")
                            'Envoi du dossier
                            If EnvoiMailRapport(MettreApost(rw1("NomFournis").ToString), NumeroDAO.ToString, MettreApost(rw1("MailFournis").ToString), CheminEnvoiDoss, "", "", "Dossier d'appel d'offre", "DAO", True) = False Then
                                FinChargement()
                                Exit Sub
                            End If
                            FinChargement()
                        Else
                            FinChargement()
                            FailMsg("Le dossier du soumissoinnaire " & MettreApost(rw1("NomFournis").ToString.Split(" ")(0)) & " n'existe pas ou a été supprimé.")
                        End If
                    Catch ep As IOException
                        FinChargement()
                        SuccesMsg("Un exemplaire du fichier est utilisé par une autre application" & vbNewLine & "Veuillez le fermer svp.")
                    Catch ex As Exception
                        FinChargement()
                        FailMsg(ex.ToString)
                    End Try
                Next

                FinChargement()
                SuccesMsg("Dossier envoyé avec succès.")

                'Dim NewEnvoiDoss As New EnvoiDossSoumissionnaire
                'NewEnvoiDoss.NumeroDAO = drx("N°").ToString
                'NewEnvoiDoss.ShowDialog()
            Catch ex As Exception
                FinChargement()
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub


    Private Sub AnnulerLeDossierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnnulerLeDossierToolStripMenuItem.Click

        If LayoutView1.RowCount > 0 Then
            Try
                If PourAjoutModifDansDB = 1 Then
                    SuccesMsg("Veuillez enregistrer le dossier en cours.")
                    Exit Sub
                ElseIf (PourAjoutModifDansDB = 2) Then
                    SuccesMsg("Veuillez enregistrer et fermer le dossier en cours.")
                    Exit Sub
                End If

                Dim drx = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
                If drx("Statut").ToString = "Annulé" Then
                    FailMsg("Ce marché a été annulé.")
                    Exit Sub
                End If

                'Verifier si le marche a ete engager
                If drx("Statut").ToString = "Terminé" Then
                    FailMsg("Impossible d'annuler un marché déjà executé.")
                    Exit Sub
                End If

                'Verifier si tous les fournisseurs retenus sont disqualifiés

                'Verifier s'il existe des critères post qualifications
                If Val(ExecuteScallar("select count(*) from T_DAO_PostQualif where NumeroDAO='" & EnleverApost(drx("N°").ToString) & "'")) > 0 Then
                    query = "select count(*) from t_soumissionfournisseurclassement as S, t_fournisseur as F where S.CodeFournis=F.CodeFournis and S.NumeroDAO=F.NumeroDAO AND F.NumeroDAO='" & EnleverApost(drx("N°").ToString) & "' and F.CodeProjet='" & ProjetEnCours & "' and S.AcceptationExamDetaille='OUI' and S.PrixCorrigeOffre IS NOT NULL and S.ExamPQValide='OUI' AND S.RangPostQualif IS NOT NULL and S.FournisDisqualifie IS NULL"
                Else
                    query = "select count(*) from t_soumissionfournisseurclassement as S, t_fournisseur as F where S.CodeFournis=F.CodeFournis and S.NumeroDAO=F.NumeroDAO AND F.NumeroDAO='" & EnleverApost(drx("N°").ToString) & "' and F.CodeProjet='" & ProjetEnCours & "' and S.AcceptationExamDetaille='OUI' and S.PrixCorrigeOffre IS NOT NULL AND S.RangExamDetaille IS NOT NULL and S.FournisDisqualifie IS NULL"
                End If

                If Val(ExecuteScallar(query)) > 0 Then
                    FailMsg("Impossible d'annuler ce marché, car il existe" & vbNewLine & "des fournisseurs retenus non disqualifié.")
                    Exit Sub
                End If

                If ConfirmMsg("Voulez-vous vraiment annuler ce dossier ?") = DialogResult.Yes Then
                    ReponseDialog = ""
                    Dim NewMotifAnnulDoss As New MotifAnnulationDossier
                    NewMotifAnnulDoss.TxtTextDoss.Text = "Annulation du dossier N° " & drx("N°").ToString
                    NewMotifAnnulDoss.ShowDialog()
                    If ReponseDialog.ToString = "" Then
                        Exit Sub
                    End If

                    ExecuteNonQuery("Update t_dao Set statut_DAO='Annulé', MotifAnnulationDossier='" & EnleverApost(ReponseDialog.ToString) & "' where NumeroDAO='" & EnleverApost(drx("N°").ToString) & "'")

                    Dim dts As DataTable = ExcecuteSelectQuery("select RefMarche from t_dao where NumeroDAO='" & EnleverApost(drx("N°").ToString) & "'")
                    For Each rw In dts.Rows
                        ExecuteNonQuery("Update t_marche set NumeroDAO=NULL where RefMarche='" & rw("RefMarche") & "'")

                        'Annulation des dates de réalisations.
                        GetAnnuleDateRealisationPPM(rw("RefMarche"))
                    Next

                    SuccesMsg("Dossier annulé avec succès.")
                    LayoutView1.SetFocusedRowCellValue("Statut", "Annulé")
                    'Fermeture des formulaires
                    FermerForm({"RetraitEtDepotDAO", "OuvertureOffres", "SaisieOffres", "JugementOffres"})
                End If
            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub

#End Region

    Private Sub btRetourBien_Click(sender As Object, e As EventArgs) Handles btRetourBien.Click
        ViderSaisieBien()
        cmbLotSpecTech.Enabled = True
        'CmbSousLotSpecTech.Enabled = True
        BtCategBien.Enabled = True
        NewAddSpecTechClik = False
        modifSpecTech = False
        TxtLibCategBien.Text = ""
        txtCodeCateg.Text = ""
        CodeCategorie.Text = ""
        LockSaisieBien(False)
    End Sub

    Private Sub ContextMenuPostQualif_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuPostQualif.Opening
        If AfficherDossier = True Or ListePostQualif.Nodes.Count = 0 Then
            e.Cancel = True
        End If
    End Sub
End Class
