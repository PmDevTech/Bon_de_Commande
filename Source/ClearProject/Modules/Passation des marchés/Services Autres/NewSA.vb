Imports MySql.Data.MySqlClient
Imports System.IO
Imports Microsoft.Office.Interop
Imports ClearProject.PassationMarche
Imports DevExpress.XtraEditors
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraEditors.Controls

Public Class NewSA
    Dim PourAjout As Boolean = False
    Dim PourModif As Boolean = False
    Dim NumDoss As String = ""
    Dim TypeMarche As String = ""
    Dim MethodMarche As String = ""
    Dim RefMarche As String()
    Dim CurrentMarche As DataRow = Nothing
    Dim CurrentDao As DataRow = Nothing
    Dim DejaDansLaBD As Boolean = False
    Dim TabAMettreAJour As Boolean() = {False, False, False, False, False, False, False}
    Dim LstTabName As New List(Of String) From {"PageDonneBase", "PageDonnePartic", "PageDQE", "PageConformTechnique", "PageSpecTech", "PagePostQualif", "PageApercu"}
    Dim CvConcil As String = String.Empty
    'Dim ListSpecTech As New List(Of DataTable)
    'Dim ListSpecTechLotSousLot(1, 1) As String
    Dim TypeCategorieSpecTech As String = String.Empty
    Dim SpecTech As New List(Of DaoSpecTechLot)
    Dim CodeCojoSup As New ArrayList
    Dim CodePostQualifSup As New ArrayList
    Dim CodeSpecTechSup As New ArrayList
    Dim NodeModPost As TreeListNode
    Dim NodeModSpec As TreeListNode
    Dim modifPostQualif As Boolean = False
    Dim modifSpecTech As Boolean = False
    Private Sub NewDao_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        TxtNumDao.Enabled = False
        TxtLibelleDao.Enabled = False
        cmbTypeMarche.Enabled = False
        cmbMarches.Enabled = False
        RibbonDAO.Minimized = True

        ' PageDQE.PageVisible = False
        ' PageApercu.PageVisible = False
        'PageSpecTech.PageVisible = False
        LoadLangues(CmbLangue)
        ' VisibleOtherTabs(False)
        PageDonneBase.PageEnabled = True
        ChargerEnteteTableaux()
        ItemDevise()
        VerifGroupMarche()
        LoadArchivesDao()
    End Sub

    Private Sub BtNouveau_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtNouveau.ItemClick
        If (PourAjout = False And PourModif = False) Then
            NouveauDossier()
        ElseIf (PourAjout = True) Then
            SuccesMsg("Veuillez enregistrer le dossier en cours.")
        ElseIf (PourModif = True) Then
            SuccesMsg("Veuillez enregistrer et fermer le dossier en cours.")
        End If
    End Sub

    Private Sub BtFermerDAO_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtFermerDAO.ItemClick
        FermerDossier()
    End Sub

#Region "Methodes"
    Private Sub LoadTypeMarche()
        query = "select TypeMarche from T_TypeMarche WHERE TypeMarche LIKE 'Fourniture%' OR TypeMarche LIKE 'Travaux%' order by TypeMarche"
        cmbTypeMarche.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cmbTypeMarche.Properties.Items.Add(MettreApost(rw("TypeMarche").ToString))
        Next
    End Sub
    Private Sub LoadMarches(ByVal TypeMarche As String)
        query = "Select * from T_Marche where CodeProjet='" & ProjetEnCours & "' AND TypeMarche='" & EnleverApost(TypeMarche) & "' AND NumeroDAO IS NULL"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        ReDim RefMarche(dt.Rows.Count)
        Dim i As Integer = 0
        cmbMarches.Properties.Items.Clear()
        cmbMarches.ResetText()
        For Each rw As DataRow In dt.Rows
            RefMarche(i) = rw("RefMarche")
            i += 1
            cmbMarches.Properties.Items.Add(MettreApost(rw("DescriptionMarche")) & " | " & AfficherMonnaie(rw("MontantEstimatif")) & " | " & rw("InitialeBailleur") & "(" & rw("CodeConvention") & ")")
        Next
    End Sub

    Private Sub VisibleOtherTabs(ByVal Value As Boolean)
        'On désactive les autres tabs pour amener l'user à enregister les données de base.
        PageDonnePartic.PageEnabled = Value
        PageConformTechnique.PageEnabled = Value
        PageSpecTech.PageEnabled = Value
        PagePostQualif.PageEnabled = Value
        PageApercu.PageEnabled = Value
    End Sub

    Private Sub NouveauDossier()
        PourAjout = True
        If Not TxtNbreLot.Enabled Then TxtNbreLot.Enabled = True
        TxtNbreLot.Value = 1
        CmbNumLotDB.Properties.Items.Clear()
        CmbNumLotDB.Properties.Items.Add("1")
        If Not CmbNumLotDB.Enabled Then CmbNumLotDB.Enabled = True
        If Not BtFermerDAO.Enabled Then BtFermerDAO.Enabled = True
        If Not BtEnregistrer.Enabled Then BtEnregistrer.Enabled = True
        TxtNumDao.Enabled = True
        ChkNumDaoAuto.Enabled = True
        DejaDansLaBD = False
        TxtNumDao.Focus()
        VisibleOtherTabs(False)
        PageDonneBase.PageEnabled = True
        InitDonneesBase()
        If Not IsNothing(CurrentDao) Then
            CurrentDao = Nothing
        End If
    End Sub
    Private Sub FermerDossier()
        If NumDoss <> "" Then
            DebutChargement(True, "Fermeture dossier " & NumDoss & " en cours...")
            NumDoss = ""
            PourAjout = False
            PourModif = False
            InitDonneesBase()
            InitDonneesPartic()
            InitDQE()
            InitSpecTechnq()
            InitPostQualif()

            PageApercu.PageVisible = False
            PageSpecTech.PageVisible = False
            FinChargement()
            VisibleOtherTabs(False)
            PageDonneBase.PageEnabled = False
            TabAMettreAJour = {False, False, False, False, False, False, False}
        Else
            PourAjout = False
            PourModif = False
            InitDonneesBase()

            PageApercu.PageVisible = False
            PageDonneBase.PageEnabled = False
            PageSpecTech.PageVisible = False
            TabAMettreAJour(0) = False
        End If
        DejaDansLaBD = False
        CurrentMarche = Nothing
        CurrentDao = Nothing
    End Sub
#End Region

#Region "Données de base"

    Private Sub InitDonneesBase()

        DateDepot.ResetText()
        HeureDepot.EditValue = Nothing
        DateOuverture.ResetText()
        HeureOuverture.EditValue = Nothing
        TxtNumDao.ResetText()
        ChkNumDaoAuto.Checked = False
        TxtLibelleDao.ResetText()
        ChkLibDaoAuto.Checked = False
        Dim dtLots As DataTable = LgListLots.DataSource
        dtLots.Rows.Clear()
        cmbTypeMarche.ResetText()
        TxtMethodeMarche.ResetText()
        TxtNbreLot.Value = 1
        ChkEditionLot.Checked = False
        TxtPrixDao.ResetText()
        MajCmbCompte()
        TxtNumCompte.ResetText()
        TxtAdresseCompte.ResetText()
        CmbNumLotDB.ResetText()

        'ItemCmbLot()
        InitEditionLot()

        'ArchivesDao()

    End Sub
    Private Sub ViderChampsSaisieLot()
        TxtLibLot.ResetText()
        TxtCautionLot.ResetText()
        NumGarantiLot.Value = 0
        CmbGarantiLot.ResetText()
        GridSousLot.Rows.Clear()
    End Sub
    Private Sub TxtCautionLot_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        VerifSaisieMontant(TxtCautionLot)
    End Sub
    Private Sub BtEnrgLot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgLot.Click
        'Mise a jour dans table Dao ********
        'If Not DejaDansLaBD Then 'On est dans le cas où il faut enregister les données de base
        If CmbNumLotDB.IsRequiredControl("Veuillez sélectionner un numéro dans la liste") Then
            Exit Sub
        End If
        Dim NumLot As Integer = Val(CmbNumLotDB.Text)
        Dim Updated As Boolean = False
        For i = 0 To ViewLots.RowCount - 1
            If Val(ViewLots.GetRowCellValue(i, "N°")) = NumLot Then
                If TxtLibLot.IsRequiredControl("Veuillez saisir le libellé du lot") Then
                    Exit Sub
                End If
                If Val(TxtCautionLot.Text) <> 0 Then
                    If Val(NumGarantiLot.Value) = 0 Then
                        SuccesMsg("Veuillez saisir la caution.")
                        NumGarantiLot.Select()
                        Exit Sub
                    End If
                    If CmbGarantiLot.IsRequiredControl("Veuillez choisir une durée.") Then
                        Exit Sub
                    End If
                End If

                Dim SousLotsId As String = String.Empty
                Dim SousLots As String = String.Empty
                Dim NbSousLots As Integer = 0
                For j = 0 To GridSousLot.Rows.Count - 1
                    If Not GridSousLot.Rows(j).IsNewRow Then
                        SousLots &= GridSousLot.Rows(j).Cells("SousLots").Value & ";"
                        NbSousLots += 1
                        Dim Id As String = GridSousLot.Rows(j).Cells("Id").Value
                        If Id = "" Then
                            SousLotsId &= "#" & NbSousLots & ","
                        Else
                            SousLotsId &= GridSousLot.Rows(j).Cells("Id").Value.ToString() & ","
                        End If
                    Else
                        Exit For
                    End If
                Next
                ViewLots.SetRowCellValue(i, "Libellé", TxtLibLot.Text)
                ViewLots.SetRowCellValue(i, "Caution", Val(TxtCautionLot.Text))
                Dim Garantie As String = String.Empty
                If NumGarantiLot.Value <> 0 Then
                    Garantie = NumGarantiLot.Value & " " & CmbGarantiLot.Text
                End If
                ViewLots.SetRowCellValue(i, "Garantie", Garantie)
                If NbSousLots = 0 Then
                    ViewLots.SetRowCellValue(i, "Sous lots", "")
                    ViewLots.SetRowCellValue(i, "SousLotsValues", "")
                    ViewLots.SetRowCellValue(i, "SousLotsId", "")
                Else
                    SousLots = Mid(SousLots, 1, (SousLots.Length - 1))
                    SousLotsId = Mid(SousLotsId, 1, (SousLotsId.Length - 1))
                    ViewLots.SetRowCellValue(i, "Sous lots", NbSousLots & " Sous lot(s)")
                    ViewLots.SetRowCellValue(i, "SousLotsValues", SousLots)
                    ViewLots.SetRowCellValue(i, "SousLotsId", SousLotsId)
                End If
                Updated = True
                Exit For
            End If
        Next

        If Not Updated Then
            If TxtLibLot.IsRequiredControl("Veuillez saisir le libellé du lot") Then
                Exit Sub
            End If
            If Val(TxtCautionLot.Text) <> 0 Then
                If Val(NumGarantiLot.Value) = 0 Then
                    SuccesMsg("Veuillez saisir la caution.")
                    NumGarantiLot.Select()
                    Exit Sub
                End If
                If CmbGarantiLot.IsRequiredControl("Veuillez choisir une durée.") Then
                    Exit Sub
                End If
            End If
            Dim dt As DataTable = LgListLots.DataSource
            Dim drS As DataRow = dt.NewRow
            drS("IdLot") = "#" & (CmbNumLotDB.Text)
            drS("N°") = CmbNumLotDB.Text
            drS("Libellé") = TxtLibLot.Text
            drS("Caution") = Val(TxtCautionLot.Text)
            Dim Garantie As String = String.Empty
            If NumGarantiLot.Value <> 0 Then
                Garantie = NumGarantiLot.Value & " " & CmbGarantiLot.Text
            End If
            drS("Garantie") = Garantie
            Dim SousLotsId As String = String.Empty
            Dim SousLots As String = String.Empty
            Dim NbSousLots As Integer = 0
            For i = 0 To GridSousLot.Rows.Count - 1
                If Not GridSousLot.Rows(i).IsNewRow Then
                    SousLots &= GridSousLot.Rows(i).Cells("SousLots").Value & ";"
                    NbSousLots += 1
                    Dim Id As String = GridSousLot.Rows(i).Cells("Id").Value
                    If Id = "" Then
                        SousLotsId &= "#" & NbSousLots & ","
                    Else
                        SousLotsId &= GridSousLot.Rows(i).Cells("Id").Value.ToString() & ","
                    End If
                End If
            Next
            If NbSousLots = 0 Then
                drS("Sous lots") = ""
                drS("SousLotsValues") = ""
                drS("SousLotsId") = ""
            Else
                drS("Sous lots") = NbSousLots & " Sous lot(s)"
                SousLots = Mid(SousLots, 1, (SousLots.Length - 1))
                SousLotsId = Mid(SousLotsId, 1, (SousLotsId.Length - 1))
                drS("SousLotsValues") = SousLots
                drS("SousLotsId") = SousLotsId
            End If
            dt.Rows.Add(drS)
        End If


        Exit Sub


        'If (TxtCautionLot.Text <> "") Then
        '    query = "Update T_LotDAO set LibelleLot='" & EnleverApost(TxtLibLot.Text) & "', MontantGarantie='" & TxtCautionLot.Text.Replace(" ", "") & "' where NumeroDAO='" & NumDoss & "' and CodeLot='" & CmbNumLotDB.Text & "'"
        '    ExecuteNonQuery(query)
        'End If
        'If (NumGarantiLot.Value <> 0 And CmbGarantiLot.Text <> "") Then
        '    query = "Update T_LotDAO set LibelleLot='" & EnleverApost(TxtLibLot.Text) & "', DelaiDeGarantie='" & NumGarantiLot.Value.ToString & " " & CmbGarantiLot.Text & "' where NumeroDAO='" & NumDoss & "' and CodeLot='" & CmbNumLotDB.Text & "'"
        '    ExecuteNonQuery(query)
        'End If


        ''Enregistrement des sous lots
        'Dim DernRef As String = ""
        'query = "select RefLot from T_LotDAO where NumeroDAO='" & NumDoss & "' and CodeLot='" & CmbNumLotDB.Text & "'"
        'Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt0.Rows
        '    DernRef = rw(0).ToString
        'Next

        'query = "DELETE from T_LotDAO_SousLot where RefLot='" & DernRef & "'"
        'ExecuteNonQuery(query)


        'For k As Integer = 0 To GridSousLot.RowCount - 1

        '    If (GridSousLot.Rows(k).Cells(0).Value <> Nothing) Then
        '        If (GridSousLot.Rows(k).Cells(0).Value.ToString.Replace(" ", "") <> "") Then

        '            Dim nbOccur As Decimal = 0
        '            query = "select Count(*) from T_LotDAO_SousLot where RefLot='" & DernRef & "'"
        '            dt0 = ExcecuteSelectQuery(query)
        '            If dt.Rows.Count > 0 Then
        '                nbOccur = CInt(dt.Rows(0).Item(0))
        '            End If
        '            Dim DatSet = New DataSet
        '            query = "select * from T_LotDAO_SousLot"
        '            Dim sqlconn As New MySqlConnection
        '            BDOPEN(sqlconn)
        '            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        '            Dim DatAdapt = New MySqlDataAdapter(Cmd)
        '            DatAdapt.Fill(DatSet, "T_LotDAO_SousLot")
        '            Dim DatTable = DatSet.Tables("T_LotDAO_SousLot")
        '            Dim DatRow = DatSet.Tables("T_LotDAO_SousLot").NewRow()

        '            DatRow("RefLot") = DernRef
        '            DatRow("CodeSousLot") = CmbNumLotDB.Text & "." & (nbOccur + 1).ToString
        '            DatRow("LibelleSousLot") = MettreApost(GridSousLot.Rows(k).Cells(0).Value.ToString)
        '            DatRow("NumeroDAO") = TxtNumDao.Text

        '            DatSet.Tables("T_LotDAO_SousLot").Rows.Add(DatRow)
        '            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        '            DatAdapt.Update(DatSet, "T_LotDAO_SousLot")
        '            DatSet.Clear()
        '            query = "update t_dqesection set CodeSousLot='" & CmbNumLotDB.Text & "." & (nbOccur + 1).ToString & "' where codelot='" & CmbNumLotDB.Text & "'"
        '            ExecuteNonQuery(query)
        '            If (nbOccur >= 1) Then
        '                query = "Update T_DAO set SousLot='OUI' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        '                ExecuteNonQuery(query)
        '            End If
        '            BDQUIT(sqlconn)
        '        End If
        '    End If
        'Next
        'InitEditionLot()
    End Sub
    Private Sub InitEditionLot()
        CmbNumLotDB.Text = ""
        ItemCmbLot()
        TxtLibLot.Text = ""
        TxtCautionLot.Text = ""
        NumGarantiLot.Value = 0
        CmbGarantiLot.Text = ""

        GridSousLot.Rows.Clear()
    End Sub
    Private Sub TxtLibLot_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtLibLot.TextChanged
        If (TxtLibLot.Text <> "") Then
            BtEnrgLot.Enabled = True
        Else
            BtEnrgLot.Enabled = False
        End If
    End Sub
    Private Sub CmbNumLotDB_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumLotDB.SelectedIndexChanged
        If CmbNumLotDB.SelectedIndex = -1 Then
            ViderChampsSaisieLot()
        Else
            Dim NumIndex As Integer = IsSavedItemInGridView(CmbNumLotDB.Text, ViewLots, "N°")
            If NumIndex = -1 Then
                ViderChampsSaisieLot()
            Else
                TxtLibLot.Text = ViewLots.GetRowCellValue(NumIndex, "Libellé")
                TxtCautionLot.Text = ViewLots.GetRowCellValue(NumIndex, "Caution").ToString()
                Dim Garantie As String = ViewLots.GetRowCellValue(NumIndex, "Garantie").ToString
                If Garantie = "" Then
                    NumGarantiLot.Value = 0
                    CmbGarantiLot.ResetText()
                Else
                    NumGarantiLot.Value = Split(Garantie, " ")(0)
                    CmbGarantiLot.Text = Split(Garantie, " ")(1)
                End If
                Dim SousLots As String() = ViewLots.GetRowCellValue(NumIndex, "SousLotsValues").ToString().Split(";")
                Dim SousLotsId As String() = ViewLots.GetRowCellValue(NumIndex, "SousLotsId").ToString().Split(",")
                Dim cpteSouslot As Integer = 0
                GridSousLot.Rows.Clear()
                For i = 0 To (SousLots.Length - 1)
                    If SousLots(i) <> "" Then
                        cpteSouslot += 1
                        Try
                            GridSousLot.Rows.Add(SousLots(i), SousLotsId(i))
                        Catch ex As Exception
                            GridSousLot.Rows.Add(SousLots(i), "#" & cpteSouslot)
                        End Try
                    End If
                Next
            End If
        End If
    End Sub
    Private Sub ItemCmbLot()
        'query = "select CodeLot from T_LotDAO where NumeroDAO='" & NumDoss & "' order by CodeLot"
        'CmbLotDQE.Properties.Items.Clear()
        'CmbNumLot2.Properties.Items.Clear()
        'CmbNumLot.Properties.Items.Clear()
        'Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt0.Rows
        '    CmbLotDQE.Properties.Items.Add(rw(0).ToString)
        '    CmbNumLot2.Properties.Items.Add(rw(0).ToString)
        '    CmbNumLot.Properties.Items.Add(rw(0).ToString)
        'Next
    End Sub
    Private Sub VerifGroupMarche()
        If (GbEditionLot.Visible = False) Then
            GroupControl3.Width = GroupControl1.Width + 296
        Else
            GroupControl3.Width = GroupControl1.Width
        End If
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
        GridArchives.DataSource = dt

        dt = New DataTable()
        dt.Columns.Add("IdLot", Type.GetType("System.String"))
        dt.Columns.Add("N°", Type.GetType("System.String"))
        dt.Columns.Add("Libellé", Type.GetType("System.String"))
        dt.Columns.Add("Caution", Type.GetType("System.String"))
        dt.Columns.Add("Garantie", Type.GetType("System.String"))
        dt.Columns.Add("Sous lots", Type.GetType("System.String"))
        dt.Columns.Add("SousLotsValues", Type.GetType("System.String"))
        dt.Columns.Add("SousLotsId", Type.GetType("System.String"))
        LgListLots.DataSource = dt
        Dim Keys(1) As DataColumn
        Keys(0) = dt.Columns("IdLot")
        dt.PrimaryKey = Keys
        dt.DefaultView.Sort = "N° ASC"
        ViewLots.Columns("IdLot").Visible = False
        ViewLots.Columns("SousLotsValues").Visible = False
        ViewLots.Columns("SousLotsId").Visible = False
        ViewLots.Columns("N°").Width = 30
        ViewLots.Columns("Libellé").Width = 250
        ViewLots.OptionsView.ColumnAutoWidth = True
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
        dt.Columns.Add("Mot de passe", Type.GetType("System.String"))
        dt.Columns.Add("CanDeleted", Type.GetType("System.Boolean"))
        dt.Columns.Add("Edit", Type.GetType("System.Boolean"))
        LgCojo.DataSource = dt
        dt.DefaultView.Sort = "Nom et prénoms ASC"
        ViewCojo.Columns("IdCommission").Visible = False
        ViewCojo.Columns("CanDeleted").Visible = False
        ViewCojo.Columns("Edit").Visible = False
        ViewCojo.Columns("Nom et prénoms").Width = 280
        ViewCojo.Columns("Mot de passe").Visible = False
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
        query = "select NumeroDAO,DateEdition,TypeMarche,MethodePDM,NbreLotDAO,DateFinOuverture,DateOuverture,IntituleDAO from T_DAO where CodeProjet='" & ProjetEnCours & "' order by DateSaisie DESC"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            Dim dr = dt.NewRow()
            dr("N°") = rw("NumeroDAO").ToString
            If rw("DateEdition").ToString() = "" Then
                dr("Edité le") = ""
            Else
                dr("Edité le") = CDate(rw("DateEdition").ToString()).ToString("dd/MM/yyyy hh:mm:ss")
            End If
            dr("Type") = rw("TypeMarche").ToString
            dr("Méthode") = rw("MethodePDM").ToString
            dr("Nbre lot") = rw("NbreLotDAO").ToString & " lot(s)"
            If (rw("DateFinOuverture").ToString <> "") Then
                dr("Ouverture") = "Effectuée"
                dr("Date") = CDate(rw("DateFinOuverture").ToString()).ToShortDateString() & " à " & CDate(rw("DateFinOuverture").ToString()).ToShortTimeString()
            Else
                If (rw("DateOuverture").ToString <> "") Then
                    dr("Ouverture") = "Non effectuée"
                    dr("Date") = CDate(rw("DateOuverture").ToString()).ToShortDateString() & " à " & CDate(rw("DateOuverture").ToString()).ToShortTimeString()
                Else
                    dr("Ouverture") = "Non Prévue"
                    dr("Date") = "__/__/____"
                End If
            End If
            dr("Libellé") = MettreApost(rw("IntituleDAO").ToString)
            dt.Rows.Add(dr)
        Next
    End Sub
    Private Sub BtArchives_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtArchives.ItemClick
        SplitContainerControl1.Collapsed = False
    End Sub
    Private Sub ChkNumDaoAuto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkNumDaoAuto.CheckedChanged, rdAttrSousLot.CheckedChanged, rdAttrLot.CheckedChanged
        If (ChkNumDaoAuto.Checked = True) Then
            If (TxtNumDao.Text <> "") Then
                Dim Rep1 As MsgBoxResult = MsgBox("Le numéro entré sera remplacer!" & vbNewLine & "Voulez-vous continuer?", MsgBoxStyle.YesNo)
                If (Rep1 = MsgBoxResult.No) Then
                    ChkNumDaoAuto.Checked = False
                    Exit Sub
                End If
            End If

            TxtNumDao.Enabled = False
            TxtLibelleDao.Enabled = True
            ChkLibDaoAuto.Enabled = True
            TxtLibelleDao.Focus()
        Else
            TxtNumDao.Enabled = True
        End If
    End Sub

    Private Sub ChkLibDaoAuto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkLibDaoAuto.CheckedChanged
        If (ChkLibDaoAuto.Checked = True) Then
            If (TxtLibelleDao.Text <> "") Then
                Dim Rep1 As MsgBoxResult = MsgBox("Le libellé entré sera remplacer!" & vbNewLine & "Voulez-vous continuer?", MsgBoxStyle.YesNo)
                If (Rep1 = MsgBoxResult.No) Then
                    ChkLibDaoAuto.Checked = False
                    Exit Sub
                End If
            End If
            TxtLibelleDao.Enabled = False
            'GridMarcheDao.Enabled = True
            TxtLibelleDao.Text = "********   Ajoutez un (des) marché(s)   ********"
            'GridMarcheDao.Rows.Clear()
            'Dim n As Decimal = 'GridMarcheDao.Rows.Add()
            'GridMarcheDao.Rows.Item(n).Cells(0).Value = "Ajouter"
        Else
            If (Mid(TxtLibelleDao.Text, 1, 4) = "****") Then
                TxtLibelleDao.Text = ""
            End If
            TxtLibelleDao.Enabled = True
        End If
    End Sub

    Private Sub MajCmbCompte()
        CmbCompte.Properties.Items.Clear()
        query = "select * from T_CompteBancaire where CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbCompte.Properties.Items.Add(MettreApost(rw("LibelleCompte").ToString) & " - " & rw("NumeroCompte"))
        Next
    End Sub

    Private Sub TxtNbreLot_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNbreLot.EditValueChanged
        If (TxtNbreLot.Value = 1) Then
            'Beep()
            Exit Sub
        End If

        Dim oldText As String = CmbNumLotDB.Text
        CmbNumLotDB.Properties.Items.Clear()
        For i = 1 To TxtNbreLot.Value
            CmbNumLotDB.Properties.Items.Add(i.ToString())
        Next
        CmbNumLotDB.Text = oldText
    End Sub

    Private Sub DateDepot_DateTimeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateDepot.DateTimeChanged
        'HeureDepot.Focus()
        If (TxtNumDao.Text <> "") Then
            TxtPrixDao.Enabled = True
        End If
    End Sub

    Private Sub HeureDepot_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles HeureDepot.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            DateOuverture.Focus()
        End If
    End Sub

    Private Sub DateOuverture_DateTimeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateOuverture.DateTimeChanged
        'HeureOuverture.Focus()
    End Sub

    Private Sub HeureOuverture_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles HeureOuverture.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            TxtPrixDao.Focus()
        End If
    End Sub

    Private Sub HeureOuverture_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles HeureOuverture.TextChanged
        If (TxtNumDao.Text <> "") Then
            TxtPrixDao.Enabled = True
        End If

    End Sub

    Private Sub TxtPrixDao_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPrixDao.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            CmbCompte.Focus()
        End If
    End Sub

    Private Sub TxtPrixDao_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPrixDao.LostFocus
        If (TxtPrixDao.Text.Replace(" ", "") <> "" And NumDoss <> "") Then
            'Mise a jour dans table Dao ********
            query = "Update T_DAO set PrixDAO='" & TxtPrixDao.Text.Replace(" ", "") & "' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
            ExecuteNonQuery(query)
        End If
    End Sub

    Private Sub TxtPrixDao_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPrixDao.TextChanged
        Try
            VerifSaisieMontant(TxtPrixDao)
            If (TxtPrixDao.Text <> "") Then
                CmbCompte.Enabled = True
            Else
                CmbCompte.Enabled = False
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        End Try

    End Sub

    Private Sub CmbCompte_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCompte.SelectedValueChanged
        If (CmbCompte.Text <> "" And NumDoss <> "") Then
            query = "select C.NumeroCompte,B.CodeBanque,B.AdresseBanque from T_CompteBancaire as C,T_Banque as B where C.TypeCompte='" & CmbCompte.Text & "' and C.RefBanque=B.RefBanque and C.CodeProjet='" & ProjetEnCours & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtNumCompte.Text = rw(0).ToString
                TxtAdresseCompte.Text = MettreApost(rw(1).ToString & " " & rw(2).ToString)
            Next
            'Mise a jour dans table Dao ********
            query = "Update T_DAO set CompteAchat='" & CmbCompte.Text & "' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
            ExecuteNonQuery(query)
        End If
    End Sub

    Private Sub GridMarcheDao_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
        'GridMarcheDao.Focus()
    End Sub

    Private Sub TxtNumDao_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNumDao.TextChanged
        If Not PourModif Then
            If (ChkNumDaoAuto.Checked = False) Then
                If (TxtNumDao.Text <> "") Then
                    TxtLibelleDao.Enabled = True
                    'ChkLibDaoAuto.Enabled = True
                    If (TxtLibelleDao.Text <> "") Then
                        cmbTypeMarche.Enabled = True
                        cmbMarches.Enabled = True
                    Else
                        cmbTypeMarche.Enabled = False
                        cmbMarches.Enabled = False
                    End If
                Else
                    TxtLibelleDao.Enabled = False
                    'ChkLibDaoAuto.Enabled = False
                    If (TxtLibelleDao.Text <> "") Then
                        cmbTypeMarche.Enabled = True
                        cmbMarches.Enabled = True
                    Else
                        cmbTypeMarche.Enabled = False
                        cmbMarches.Enabled = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub TxtLibelleDao_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtLibelleDao.TextChanged
        If Not PourModif Then
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
        InitDonneesBase()
        If Not PageDonneBase.PageEnabled Then PageDonneBase.PageEnabled = True
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

        Try
            'Verrouillage des champs
            TxtNumDao.Enabled = False
            cmbTypeMarche.Enabled = False
            cmbMarches.Enabled = False
            TxtNbreLot.Enabled = False

            TxtNumDao.Text = MettreApost(CurrentDao("NumeroDAO"))
            TxtLibelleDao.Text = MettreApost(CurrentDao("IntituleDAO"))
            cmbTypeMarche.Text = MettreApost(CurrentDao("TypeMarche"))
            cmbMarches.Text = MettreApost(CurrentMarche("DescriptionMarche")) & " | " & AfficherMonnaie(CurrentMarche("MontantEstimatif"))
            If CurrentDao("Attribution").ToString() = "Lot" Then
                rdAttrLot.Checked = True
            Else
                rdAttrSousLot.Checked = True
            End If

            TxtMethodeMarche.Text = CurrentDao("MethodePDM")
            TxtNbreLot.Value = MettreApost(CurrentDao("NbreLotDAO"))
            If IsDBNull(CurrentDao("DateLimiteRemise")) Then
                DateDepot.ResetText()
                HeureDepot.ResetText()
            Else
                If CurrentDao("DateLimiteRemise").ToString() <> "" Then
                    DateDepot.Text = CDate(CurrentDao("DateLimiteRemise")).ToString("dd/MM/yy")
                    HeureDepot.Time = CDate(CurrentDao("DateLimiteRemise")) '.ToString("HH:mm:ss")
                    'HeureDepot.Text = CDate(CurrentDao("DateLimiteRemise")).ToString("HH:mm:ss")
                Else
                    DateDepot.ResetText()
                    HeureDepot.ResetText()
                End If
            End If
            If IsDBNull(CurrentDao("DateOuverture")) Then
                DateOuverture.ResetText()
                HeureOuverture.ResetText()
            Else
                If CurrentDao("DateOuverture").ToString() <> "" Then
                    DateOuverture.Text = CDate(CurrentDao("DateOuverture")).ToString("dd/MM/yy")
                    HeureOuverture.Time = CDate(CurrentDao("DateOuverture")) '.ToString("HH:mm:ss")
                Else
                    DateOuverture.ResetText()
                    HeureOuverture.ResetText()
                End If
            End If
            If IsDBNull(CurrentDao("DatePublication")) Then
                DatePublication.ResetText()
            Else
                If CurrentDao("DatePublication").ToString() <> "" Then
                    DatePublication.Text = CDate(CurrentDao("DatePublication")).ToString("dd/MM/yy")
                Else
                    DatePublication.ResetText()
                End If
            End If
            If IsDBNull(CurrentDao("JournalPublication")) Then
                NomJournal.ResetText()
            Else
                If CurrentDao("JournalPublication").ToString() <> "" Then
                    NomJournal.Text = MettreApost(CurrentDao("JournalPublication"))
                Else
                    NomJournal.ResetText()
                End If
            End If
            'Remplir le tableau des lots
            Dim dtLots As DataTable = LgListLots.DataSource
            dtLots.Rows.Clear()
            query = "SELECT * FROM t_lotdao WHERE NumeroDAO='" & NumDoss & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                Dim drS = dtLots.NewRow
                Dim NbSousLots As Integer
                Dim SousLots As String = String.Empty
                Dim SousLotsValues As String = String.Empty
                query = "SELECT COUNT(LibelleSousLot) FROM t_lotdao_souslot WHERE RefLot='" & rw("RefLot") & "'"
                NbSousLots = Val(ExecuteScallar(query))
                query = "SELECT GROUP_CONCAT(LibelleSousLot SEPARATOR ';') FROM t_lotdao_souslot WHERE RefLot='" & rw("RefLot") & "'"
                SousLots = MettreApost(ExecuteScallar(query))
                query = "SELECT GROUP_CONCAT(RefSousLot SEPARATOR ',') FROM t_lotdao_souslot WHERE RefLot='" & rw("RefLot") & "'"
                SousLotsValues = ExecuteScallar(query)

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
                drS("SousLotsValues") = SousLots
                drS("SousLotsId") = SousLotsValues
                dtLots.Rows.Add(drS)
            Next

            'Remplir la liste des lots
            CmbNumLotDB.ResetText()
            CmbNumLotDB.Properties.Items.Clear()
            For i = 1 To TxtNbreLot.Value
                CmbNumLotDB.Properties.Items.Add(i)
            Next

            Dim PrixDAO As Decimal = Val(CurrentDao("PrixDAO"))
            TxtPrixDao.Text = IIf(PrixDAO = 0, "", AfficherMonnaie(PrixDAO))
            If CurrentDao("CompteAchat").ToString() = "" Then
                CmbCompte.Text = String.Empty
            Else
                query = "select CONCAT(LibelleCompte,' - ',NumeroCompte) from T_CompteBancaire where CodeProjet='" & ProjetEnCours & "' AND NumeroCompte='" & CurrentDao("CompteAchat") & "'"
                CmbCompte.Text = MettreApost(ExecuteScallar(query))
            End If
            TabAMettreAJour(0) = True
        Catch ex As Exception
            FailMsg("Impossible d'enregistrer ce DAO" & vbNewLine & "Contactez votre fournisseur" & vbNewLine & ex.ToString)
        End Try
    End Sub
    Private Function SavePageDonneBase(ByVal NumDossier As String) As Boolean
        'Vérification des champs
        If TxtNumDao.IsRequiredControl("Veuillez saisir le numéro du DAO") Then
            Return False
        End If
        If TxtLibelleDao.IsRequiredControl("Veuillez saisir le libellé du DAO") Then
            Return False
        End If
        If DateDepot.IsRequiredControl("Veuillez indiquer la date de fin de dépôt") Then
            Return False
        End If
        If HeureDepot.IsRequiredControl("Veuillez indiquer l'heure de fin de dépôt") Then
            Return False
        End If
        If DateOuverture.IsRequiredControl("Veuillez indiquer la date d'ouverture") Then
            Return False
        End If
        If HeureOuverture.IsRequiredControl("Veuillez indiquer l'heure d'ouverture") Then
            Return False
        End If
        If DatePublication.IsRequiredControl("Veuillez indiquer la date de publication") Then
            Return False
        End If
        If NomJournal.IsRequiredControl("Veuillez indiquer le nom du journal de publication") Then
            Return False
        End If
        If Val(TxtPrixDao.Text) <> 0 Then
            If CmbCompte.IsRequiredControl("Veuillez sélectionner le compte bancaire pour les frais de dossier") Then
                Return False
            End If
        End If

        If TxtNbreLot.Value > ViewLots.RowCount Then
            SuccesMsg("Veuillez enregistrer tous les lots")
            Return False
        End If

        If IsNothing(CurrentMarche) Then
            FailMsg("Nous n'avons pas pu récupérer le marché")
            Return False
        End If

        Dim CompteVenteDAO As String = String.Empty
        If CmbCompte.SelectedIndex <> -1 Then
            CompteVenteDAO = Split(CmbCompte.Text, " - ")(1)
        End If

        Dim AttributionMarche As String = "Lot"
        If Not rdAttrLot.Checked Then
            AttributionMarche = "Sous Lot"
        End If

        query = "UPDATE `t_dao`SET `IntituleDAO`='" & EnleverApost(TxtLibelleDao.Text) & "', `MontantMarche`='" & CurrentMarche("MontantEstimatif") & "', `TypeMarche`='" & CurrentMarche("TypeMarche") & "', "
        query &= "`MethodePDM`='" & GetMethode(CurrentMarche("CodeProcAO")) & "', `NbreLotDAO`='" & TxtNbreLot.Value & "', `DateModif`='" & dateconvert(Now) & "', `PrixDAO`='" & Val(TxtPrixDao.Text) & "', "
        query &= "`CompteAchat`='" & CompteVenteDAO & "', `DateOuverture`='" & dateconvert(DateOuverture.DateTime.ToShortDateString) & " " & HeureOuverture.Text & "', "
        query &= "`DateLimiteRemise`='" & dateconvert(DateDepot.DateTime.ToShortDateString()) & " " & HeureDepot.Text & "', `CodeConvention`='" & CurrentMarche("CodeConvention") & "', "
        query &= "`Attribution`='" & AttributionMarche & "', DatePublication='" & dateconvert(DatePublication.DateTime.ToShortDateString()) & "' ,JournalPublication='" & NomJournal.Text & "' WHERE `NumeroDAO`='" & NumDossier & "'"
        Try
            ExecuteNonQuery(query)

            'Modification des lots existants
            Dim LesCodesDesLots As String = String.Empty
            For i = 0 To (ViewLots.RowCount - 1)
                Dim NumLot As Decimal = Val(ViewLots.GetRowCellValue(i, "IdLot"))
                LesCodesDesLots &= NumLot & ","
                Dim NumeroLot As String = ViewLots.GetRowCellValue(i, "N°")
                query = "UPDATE t_lotdao SET LibelleLot='" & EnleverApost(ViewLots.GetRowCellValue(i, "Libellé")) & "', MontantGarantie='" & Val(ViewLots.GetRowCellValue(i, "Caution")) & "', DelaiDeGarantie='" & ViewLots.GetRowCellValue(i, "Garantie") & "', DateModif='" & dateconvert(Now) & "' WHERE RefLot='" & NumLot & "'"
                ExecuteNonQuery(query)

                'Modification des sous-lots existants
                Dim SousLotsId As String() = ViewLots.GetRowCellValue(i, "SousLotsId").ToString().Split(",")
                Dim SousLots As String() = ViewLots.GetRowCellValue(i, "SousLotsValues").ToString().Split(";")
                Dim cpteSousLot As Integer = 1
                Dim NewSousLotsId As String = String.Empty
                Dim NewSousLots As String = String.Empty
                For j = 0 To SousLots.Length - 1
                    Dim NumSousLot As String = NumeroLot & "." & cpteSousLot
                    Try
                        If SousLotsId(j).Length >= 1 Then
                            If Mid(SousLotsId(j), 1, 1) = "#" Then
                                query = "INSERT INTO t_lotdao_souslot VALUES(NULL,'" & NumLot & "','" & NumDossier & "','" & NumSousLot & "','" & EnleverApost(SousLots(j)) & "')"
                                ExecuteNonQuery(query)
                                Dim LastNumSousLotID As String = Val(ExecuteScallar("SELECT MAX(RefSousLot) FROM t_lotdao_souslot WHERE RefLot='" & NumLot & "' AND NumeroDAO='" & NumDossier & "'"))
                                NewSousLotsId &= LastNumSousLotID & ","
                                NewSousLots &= SousLots(j) & ";"
                            Else
                                query = "UPDATE t_lotdao_souslot SET CodeSousLot='" & NumSousLot & "', LibelleSousLot='" & EnleverApost(SousLots(j)) & "' WHERE RefSousLot='" & SousLotsId(j) & "'"
                                ExecuteNonQuery(query)
                                NewSousLotsId &= SousLotsId(j) & ","
                                NewSousLots &= SousLots(j) & ";"
                            End If
                            'Else
                            '    query = "INSERT INTO t_lotdao_souslot VALUES(NULL,'" & NumLot & "','" & NumDossier & "','" & NumSousLot & "','" & EnleverApost(SousLots(j)) & "')"
                            '    ExecuteNonQuery(query)
                            '    Dim LastNumSousLotID As String = Val(ExecuteScallar("SELECT MAX(RefSousLot) FROM t_lotdao_souslot WHERE RefLot='" & NumLot & "' AND NumeroDAO='" & NumDossier & "'"))
                            '    NewSousLotsId &= LastNumSousLotID & ","
                            '    NewSousLots &= SousLots(j) & ";"
                        End If
                    Catch ex As Exception
                        FailMsg(ex.ToString)
                    End Try
                    cpteSousLot += 1
                Next
                If NewSousLotsId.Length > 0 Then
                    NewSousLots = Mid(NewSousLots, 1, (NewSousLots.Length - 1))
                    NewSousLotsId = Mid(NewSousLotsId, 1, (NewSousLotsId.Length - 1))
                End If
                ViewLots.SetRowCellValue(i, "SousLotsValues", NewSousLots)
                ViewLots.SetRowCellValue(i, "SousLotsId", NewSousLotsId)

                'Supprimer les sous-lots qui ont été supprimé par l'user
                If NewSousLotsId <> String.Empty Then
                    query = "DELETE FROM t_lotdao_souslot WHERE RefLot='" & NumLot & "' AND NumeroDAO='" & NumDossier & "' AND RefSousLot NOT IN(" & NewSousLotsId & ")"
                Else
                    query = "DELETE FROM t_lotdao_souslot WHERE RefLot='" & NumLot & "' AND NumeroDAO='" & NumDossier & "'"
                End If
                ExecuteNonQuery(query)
            Next

            'Supprimer les lots et leurs sous-lots qui ont été supprimé par l'user
            If LesCodesDesLots <> String.Empty Then
                LesCodesDesLots = Mid(LesCodesDesLots, 1, (LesCodesDesLots.Length - 1))
                query = "SELECT RefLot FROM t_lotdao WHERE NumeroDAO='" & NumDossier & "' AND RefLot NOT IN(" & LesCodesDesLots & ")"
            Else
                query = "SELECT RefLot FROM t_lotdao WHERE NumeroDAO='" & NumDossier & "'"
            End If

            Dim dtDeleteLots As DataTable = ExcecuteSelectQuery(query)
            For Each rwDelLot As DataRow In dtDeleteLots.Rows
                query = "DELETE FROM t_lotdao_souslot WHERE RefLot='" & rwDelLot("RefLot") & "' AND NumeroDAO='" & NumDossier & "'"
                ExecuteNonQuery(query)
                query = "DELETE FROM t_lotdao WHERE RefLot='" & rwDelLot("RefLot") & "' AND NumeroDAO='" & NumDossier & "'"
                ExecuteNonQuery(query)
            Next
            LoadArchivesDao()
            Return True
        Catch ex As Exception
            FailMsg("Impossible d'enregistrer ce DAO" & vbNewLine & "Contactez votre fournisseur" & vbNewLine & ex.ToString)
            Return False
        End Try
    End Function

#End Region

#Region "Données particulières"

    Private Sub InitDonneesPartic()
        CmbLangue.ResetText()
        CmbDevise.ResetText()
        TxtDevise.ResetText()
        NumValidite.Value = 1
        CmbValidite.ResetText()
        NumNbreCopie.Value = 1
        NumDelai.Value = 1
        CmbDelai.ResetText()
        CmbCivCojo.ResetText()
        TxtCojo.ResetText()
        TxtFonctionCojo.ResetText()
        TxtContactCojo.ResetText()
        TxtMailCojo.ResetText()
        Dim dtCojo As DataTable = LgCojo.DataSource
        dtCojo.Rows.Clear()
        RdReunionNon.Checked = True
        DateReunion.EditValue = Nothing
        HeureReunion.EditValue = Nothing
        RdGroupNon.Checked = True
        NumGroupement.Value = 1
        RdConcilNon.Checked = True
        TxtNomConcil.ResetText()
        TxtRemunConcil.ResetText()
        CmbRemunConcil.ResetText()
        TxtDesigneConcil.ResetText()
        TxtAdresseDesigneConcil.ResetText()
        TxtCvConcil.ResetText()
        CmbTitreCojo.ResetText()
    End Sub

    Private Sub ItemDevise()
        query = "select AbregeDevise from T_Devise"
        CmbDevise.Properties.Items.Clear()
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbDevise.Properties.Items.Add(rw("AbregeDevise").ToString)
        Next
    End Sub

    Private Sub CmbLangue_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbLangue.SelectedValueChanged
        If (CmbLangue.Text <> "") Then
            CmbDevise.Enabled = True
        Else
            CmbDevise.Enabled = False
        End If
    End Sub

    Private Sub CmbDevise_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbDevise.SelectedValueChanged
        If (CmbDevise.Text <> "") Then
            NumValidite.Enabled = True
            CmbValidite.Enabled = True
            query = "select LibelleDevise from T_Devise where AbregeDevise='" & CmbDevise.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtDevise.Text = rw(0).ToString
            Next
        Else
            NumValidite.Enabled = False
            CmbValidite.Enabled = False
        End If
    End Sub

    Private Sub CmbValidite_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbValidite.SelectedValueChanged
        If (CmbValidite.Text <> "") Then
            NumNbreCopie.Enabled = True
        Else
            NumNbreCopie.Enabled = False
        End If
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

    Private Sub RdReunionOui_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdReunionOui.CheckedChanged
        If (RdReunionOui.Checked = True) Then
            DateReunion.Enabled = True
            HeureReunion.Enabled = True
        Else
            DateReunion.Enabled = False
            HeureReunion.Enabled = False
        End If
    End Sub

    Private Sub RdGroupOui_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdGroupOui.CheckedChanged
        If (RdGroupOui.Checked = True) Then
            NumGroupement.Enabled = True
        Else
            NumGroupement.Enabled = False
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
            Exit Sub
        End If
        If TxtCojo.IsRequiredControl("Veuillez entrer le nom") Then
            Exit Sub
        End If
        If TxtFonctionCojo.IsRequiredControl("Veuillez entrer l'organisme") Then
            Exit Sub
        End If
        If CmbTitreCojo.IsRequiredControl("Veuillez choisir le titre") Then
            Exit Sub
        End If
        If TxtMailCojo.IsRequiredControl("Veuillez entrer l'email") Then
            Exit Sub
        End If
        If BtAjoutCojo.Text = "Ajouter" Then
            Dim dt As DataTable = LgCojo.DataSource
            Dim drS As DataRow = dt.NewRow
            drS("IdCommission") = "##"
            drS("Nom et prénoms") = (CmbCivCojo.Text & " " & TxtCojo.Text.Trim()).Trim()
            drS("Organisme") = TxtFonctionCojo.Text.Trim()
            drS("Titre") = CmbTitreCojo.Text.Trim()
            drS("Téléphone") = TxtContactCojo.Text.Trim()
            drS("Email") = TxtMailCojo.Text.Trim()
            drS("Mot de passe") = GenererCode(8)
            drS("CanDeleted") = True
            drS("Edit") = False
            dt.Rows.Add(drS)
            InitCojo()
        Else
            Dim CurrentIndex As Integer = -1
            For i = 0 To ViewCojo.RowCount - 1
                If CBool(ViewCojo.GetRowCellValue(i, "Edit")) = True Then
                    CurrentIndex = i
                    Exit For
                End If
            Next
            If CurrentIndex = -1 Then
                BtAjoutCojo.Text = "Ajouter"
                SuccesMsg("Aucune ligne n'a été modifiée")
                Exit Sub
            End If
            ViewCojo.SetRowCellValue(CurrentIndex, "Nom et prénoms", (CmbCivCojo.Text & " " & TxtCojo.Text.Trim()).Trim())
            ViewCojo.SetRowCellValue(CurrentIndex, "Organisme", TxtFonctionCojo.Text.Trim())
            ViewCojo.SetRowCellValue(CurrentIndex, "Titre", CmbTitreCojo.Text.Trim())
            ViewCojo.SetRowCellValue(CurrentIndex, "Téléphone", TxtContactCojo.Text.Trim())
            ViewCojo.SetRowCellValue(CurrentIndex, "Email", TxtMailCojo.Text.Trim())
            ViewCojo.SetRowCellValue(CurrentIndex, "Edit", False)
            'ViewCojo.RefreshData()
            InitCojo()
        End If

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

    Private Sub ChkOuvrageDelegueOui_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkOuvrageDelegueOui.CheckedChanged
        If (ChkOuvrageDelegueOui.Checked = True) Then
            TxtMaitreOuvrageDelegue.Enabled = True
        Else
            TxtMaitreOuvrageDelegue.Enabled = False
        End If
    End Sub
    Private Sub ContextMenuStripCojo_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStripCojo.Opening
        If ViewCojo.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub ToolStripMenuModifierCojo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuModifierCojo.Click
        If ViewCojo.RowCount > 0 Then
            ViewCojo.SetRowCellValue(ViewCojo.FocusedRowHandle, "Edit", True)
            BtAjoutCojo.Text = "Modifier"
            Dim Drx As DataRow = ViewCojo.GetFocusedDataRow
            CmbCivCojo.Text = Split(Drx("Nom et prénoms").ToString(), " ")(0)
            TxtCojo.Text = Drx("Nom et prénoms").ToString().Replace(CmbCivCojo.Text & " ", "")
            CmbTitreCojo.Text = Drx("Titre").ToString()
            TxtContactCojo.Text = Drx("Téléphone").ToString()
            TxtFonctionCojo.Text = Drx("Organisme").ToString()
            TxtMailCojo.Text = Drx("Email").ToString()
            CmbCivCojo.Focus()
        End If
    End Sub

    Private Sub ToolStripMenuSupprimerCojo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuSupprimerCojo.Click
        If ViewCojo.RowCount > 0 Then
            If CBool(ViewCojo.GetFocusedDataRow()("CanDeleted")) Then
                If ConfirmMsg("Voulez-vous continuer la suppression?") = DialogResult.Yes Then
                    Dim IdCojo = ViewCojo.GetFocusedRowCellValue("IdCommission")
                    CodeCojoSup.Add(IdCojo)
                    ViewCojo.GetFocusedDataRow().Delete()
                    If BtAjoutCojo.Text = "Modifier" Then
                        BtAjoutCojo.Text = "Ajouter"
                    End If
                End If
            Else
                FailMsg("Vous ne pouvez pas supprimer cet élément.")
            End If
        End If
    End Sub
    Private Sub LoadPageDonnePartic(ByVal NumDossier As String)
        InitDonneesPartic()
        If Not PageDonnePartic.PageEnabled Then PageDonnePartic.PageEnabled = True
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

        Try
            CmbLangue.Text = CurrentDao("LangueSoumission").ToString
            Dim Validite As String = CurrentDao("ValiditeOffre").ToString()
            If Validite <> String.Empty Then
                NumValidite.Value = Val(Split(Validite, " ")(0))
                CmbValidite.Text = Split(Validite, " ")(1)
            Else
                NumValidite.Value = 1
                CmbValidite.ResetText()
            End If
            CmbDevise.Text = CurrentDao("MonnaieEvalDAO").ToString()
            NumNbreCopie.Value = Val(CurrentDao("NbCopieSoumission").ToString())

            Dim DelaiExec As String = CurrentDao("DelaiExecution").ToString()
            If DelaiExec <> String.Empty Then
                NumDelai.Value = Val(Split(DelaiExec, " ")(0))
                CmbDelai.Text = Split(DelaiExec, " ")(1)
            Else
                NumDelai.Value = 1
                CmbDelai.ResetText()
            End If

            If CurrentDao("DateReunionPrepa").ToString() = "" Then
                RdReunionNon.Checked = True
            Else
                RdReunionOui.Checked = True
                DateReunion.Text = CDate(CurrentDao("DateReunionPrepa").ToString()).ToShortDateString()
                HeureReunion.EditValue = CDate(CurrentDao("DateReunionPrepa").ToString())
            End If

            If Val(CurrentDao("NbreMembregroup").ToString()) = 0 Then
                RdGroupNon.Checked = True
            Else
                RdGroupOui.Checked = True
                NumGroupement.Value = Val(CurrentDao("NbreMembregroup").ToString())
            End If

            If CurrentDao("NomConciliateur").ToString() = "" And Val(CurrentDao("MontConciliateur").ToString()) = 0 And CurrentDao("DesignConciliateur").ToString() = "" And CurrentDao("DesignAdresse").ToString() = "" And CurrentDao("CvConciliateur").ToString() = "" Then
                RdConcilNon.Checked = True
            Else
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
            query = "SELECT * FROM t_commission WHERE NumeroDAO='" & NumDossier & "' AND TypeComm='COJO'"
            Using dt As DataTable = ExcecuteSelectQuery(query)
                Dim dtCojo As DataTable = LgCojo.DataSource
                For Each rw As DataRow In dt.Rows
                    Dim drS = dtCojo.NewRow()
                    drS("IdCommission") = rw("CodeMem")
                    drS("Nom et prénoms") = MettreApost(rw("NomMem"))
                    drS("Téléphone") = MettreApost(rw("TelMem"))
                    drS("Email") = MettreApost(rw("EmailMem"))
                    drS("Organisme") = MettreApost(rw("FoncMem"))
                    drS("Titre") = MettreApost(rw("TitreMem"))
                    drS("Mot de passe") = rw("PasseMem")
                    If rw("Evaluation").ToString() = String.Empty Then
                        drS("CanDeleted") = True
                    Else
                        drS("CanDeleted") = False
                    End If
                    drS("Edit") = False
                    dtCojo.Rows.Add(drS)
                Next
            End Using
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try


    End Sub
    Private Function SavePageDonnePartic(ByVal NumDossier As String) As Boolean
        'Vérification des champs
        If CmbLangue.IsRequiredControl("Veuillez choisir une langue dans la liste.") Then
            Return False
        End If
        If CmbDevise.IsRequiredControl("Veuillez choisir une devise dans la liste.") Then
            Return False
        End If
        If NumValidite.Value <> 0 Then
            If CmbValidite.IsRequiredControl("Veuillez bien définir la validité.") Then
                Return False
            End If
        ElseIf Val(NumValidite.Value) <= 0 Then
            SuccesMsg("Veuillez entrer la validité")
            Return False
        End If
        If NumDelai.Value <> 0 Then
            If CmbDelai.IsRequiredControl("Veuillez bien définir le délai.") Then
                Return False
            End If
        ElseIf Val(NumDelai.Value) <= 0 Then
            SuccesMsg("Veuillez entrer le délai")
            Return False
        End If
        If Val(NumNbreCopie.Value) <= 0 Then
            SuccesMsg("Veuillez entrer le nombre de copies")
            Return False
        End If

        If RdReunionOui.Checked = True Then
            If DateReunion.IsRequiredControl("Veuillez définir la date de la réunion préparatoire") Then
                Return False
            End If
            If HeureReunion.IsRequiredControl("Veuillez définir l'heure de la réunion préparatoire") Then
                Return False
            End If
        End If

        If RdGroupOui.Checked = True Then
            If Val(NumGroupement.Value) <= 0 Then
                SuccesMsg("Veuillez indiquer le nombre de membre par groupement")
                Return False
            End If
        End If

        If ChkOuvrageDelegueOui.Checked = True Then
            If (TxtMaitreOuvrageDelegue.IsRequiredControl("Veuillez saisir le maître d'ouvrage délégué")) Then
                Return False
            End If
        End If

        'Mise a jour du DAO
        If RdReunionNon.Checked = True Then
            query = "Update T_DAO set DateReunionPrepa=NULL where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        ElseIf RdReunionOui.Checked = True Then
            query = "Update T_DAO set DateReunionPrepa='" & dateconvert(CDate(DateReunion.Text).ToShortDateString()) & " " & CDate(HeureReunion.Text).ToLongTimeString & "' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        End If
        ExecuteNonQuery(query)

        If RdConcilNon.Checked = True Then
            'Mise a jour dans table Dao ********
            query = "Update T_DAO set NomConciliateur='', MontConciliateur='0', DesignConciliateur='', DesignAdresse='', CvConciliateur='' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
            ExecuteNonQuery(query)
        Else
            If TxtNomConcil.Text.Trim() <> "" Then
                query = "Update T_DAO set NomConciliateur='" & EnleverApost(TxtNomConcil.Text) & "' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If

            If (TxtRemunConcil.Text.Trim() <> "" And CmbRemunConcil.Text.Trim() <> "" And TxtNomConcil.Text.Trim() <> "") Then
                query = "Update T_DAO set MontConciliateur='" & TxtRemunConcil.Text.Trim().Replace(" ", "") & "/" & CmbRemunConcil.Text.Trim() & "' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If

            If (TxtDesigneConcil.Text.Trim() <> "" And TxtNomConcil.Text.Trim() <> "") Then
                query = "Update T_DAO set DesignConciliateur='" & EnleverApost(TxtDesigneConcil.Text.Trim()) & "' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If

            If (TxtAdresseDesigneConcil.Text.Trim() <> "" And TxtDesigneConcil.Text.Trim() <> "" And TxtNomConcil.Text.Trim() <> "") Then
                query = "Update T_DAO set DesignAdresse='" & EnleverApost(TxtAdresseDesigneConcil.Text.Trim()) & "' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If

            If TxtCvConcil.Text.Trim() <> String.Empty Then
                If File.Exists(CvConcil) Then
                    Try
                        Dim DossierDAO As String = FormatFileName(line & "\DAO\" & TypeMarche & "\" & MethodMarche & "\" & NumDoss, "")
                        If Not Directory.Exists(DossierDAO) Then
                            Directory.CreateDirectory(DossierDAO)
                        End If
                        If Not File.Exists(DossierDAO & "\" & TxtCvConcil.Text) Then
                            File.Copy(CvConcil, DossierDAO & "\" & TxtCvConcil.Text, True)
                            query = "Update T_DAO set CvConciliateur='" & EnleverApost(TxtCvConcil.Text.Trim()) & "' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
                            ExecuteNonQuery(query)
                        End If
                    Catch ex As Exception

                    End Try
                End If
            End If

        End If

        If RdGroupNon.Checked = True Then
            query = "Update T_DAO set NbreMembregroup='0' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        Else
            query = "Update T_DAO set NbreMembregroup='" & NumGroupement.Value.ToString & "' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        End If
        ExecuteNonQuery(query)

        query = "Update T_DAO set LangueSoumission='" & CmbLangue.Text & "' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        ExecuteNonQuery(query)

        query = "Update T_DAO set MonnaieEvalDAO='" & CmbDevise.Text & "' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        ExecuteNonQuery(query)

        query = "Update T_DAO set ValiditeOffre='" & NumValidite.Value.ToString & " " & CmbValidite.Text & "' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        ExecuteNonQuery(query)

        query = "Update T_DAO set NbCopieSoumission='" & NumNbreCopie.Value.ToString & "' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        ExecuteNonQuery(query)

        query = "Update T_DAO set DelaiExecution='" & NumDelai.Value.ToString & " " & CmbDelai.Text & "' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        ExecuteNonQuery(query)

        If ChkOuvrageDelegueOui.Checked = True Then
            'Mise a jour dans table Dp ********
            query = "Update T_DAO set MeOuvrageDelegue='" & EnleverApost(TxtMaitreOuvrageDelegue.Text) & "' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
            ExecuteNonQuery(query)
        ElseIf ChkOuvrageDelegueNon.Checked = True Then
            query = "Update T_DAO set MeOuvrageDelegue='' where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
            ExecuteNonQuery(query)
        End If

        'Enregistrement des cojos
        If ViewCojo.RowCount = 0 Then
            query = "DELETE FROM t_commission WHERE NumeroDAO='" & NumDossier & "'"
            ExecuteNonQuery(query)
            'query = "DELETE FROM t_dao_evalcojo WHERE NumeroDAO='" & NumDossier & "'"
            'ExecuteNonQuery(query)
        Else
            Dim CodeCojo As String = String.Empty
            For i = 0 To ViewCojo.RowCount - 1
                Dim IdCojo As String = ViewCojo.GetRowCellValue(i, "IdCommission").ToString()
                If IdCojo <> "##" Then
                    CodeCojo &= "'" & IdCojo & "',"
                End If
            Next
            If CodeCojo <> String.Empty Then
                CodeCojo = Mid(CodeCojo, 1, (CodeCojo.Length - 1))
                query = "DELETE FROM t_commission WHERE NumeroDAO='" & NumDossier & "' AND TypeComm='COJO' AND CodeMem NOT IN(" & CodeCojo & ")"
                ExecuteNonQuery(query)
                'query = "DELETE FROM t_dao_evalcojo WHERE NumeroDAO='" & NumDossier & "' AND id_cojo NOT IN(" & CodeCojo & ")"
                'ExecuteNonQuery(query)
            End If

            For i = 0 To ViewCojo.RowCount - 1
                Dim IdCojo As String = ViewCojo.GetRowCellValue(i, "IdCommission").ToString()
                If IdCojo = "##" Then
                    query = "INSERT INTO t_commission(CodeMem,NomMem,TelMem,EmailMem,FoncMem,TitreMem,NumeroDAO,TypeComm,DateSaisie,DateModif,Operateur,PasseMem,AuthKey)"
                    query &= " VALUES(NULL,'" & EnleverApost(ViewCojo.GetRowCellValue(i, "Nom et prénoms").ToString()) & "','" & EnleverApost(ViewCojo.GetRowCellValue(i, "Téléphone").ToString()) & "',"
                    query &= "'" & EnleverApost(ViewCojo.GetRowCellValue(i, "Email").ToString()) & "','" & EnleverApost(ViewCojo.GetRowCellValue(i, "Organisme").ToString()) & "',"
                    query &= "'" & EnleverApost(ViewCojo.GetRowCellValue(i, "Titre").ToString()) & "','" & NumDossier & "','COJO','" & dateconvert(Now) & "','" & dateconvert(Now) & "','" & CodeOperateurEnCours & "',NULL" & ",NULL)"
                    ExecuteNonQuery(query)
                    Dim GetLastCojoId As String = ExecuteScallar("SELECT MAX(CodeMem) FROM t_commission WHERE NumeroDAO='" & NumDossier & "' AND Operateur='" & CodeOperateurEnCours & "'")
                    ViewCojo.SetRowCellValue(i, "IdCommission", GetLastCojoId)
                Else
                    'query = "SELECT EmailMem, AuthKey FROM t_commission WHERE CodeMem='" & IdCojo & "'"
                    'Dim dt0 = ExcecuteSelectQuery(query)
                    'For Each rw1 In dt0.Rows
                    '    If rw1("EmailMem").ToString <> ViewCojo.GetRowCellValue(i, "Email").ToString() Then
                    '        Dim Authkey = GenererToken(NumDossier, IdCojo, "DAO", DB)
                    '        Dim ID() = Authkey.Split(":")
                    '        Dim token = ID(0).ToString
                    '        envoieMail(ViewCojo.GetRowCellValue(i, "Nom et prénoms").ToString(), ViewCojo.GetRowCellValue(i, "Email").ToString(), Authkey)
                    '        query = "UPDATE t_commission SET AuthKey='" & token & "' WHERE CodeMem='" & IdCojo & "'"
                    '        ExecuteNonQuery(query)
                    '    End If
                    'Next
                    query = "UPDATE t_commission SET NomMem='" & EnleverApost(ViewCojo.GetRowCellValue(i, "Nom et prénoms").ToString()) & "', TelMem='" & EnleverApost(ViewCojo.GetRowCellValue(i, "Téléphone").ToString()) & "',"
                    query &= "EmailMem='" & EnleverApost(ViewCojo.GetRowCellValue(i, "Email").ToString()) & "',FoncMem='" & EnleverApost(ViewCojo.GetRowCellValue(i, "Organisme").ToString()) & "',TitreMem='" & EnleverApost(ViewCojo.GetRowCellValue(i, "Titre").ToString()) & "',"
                    query &= "DateModif='" & dateconvert(Now) & "' WHERE CodeMem='" & IdCojo & "'"
                    ExecuteNonQuery(query)
                End If
            Next
            If CodeCojoSup.Count > 0 Then
                For k = 0 To CodeCojoSup.Count - 1
                    query = "DELETE FROM t_commission WHERE NumeroDAO='" & NumDossier & "' AND TypeComm='COJO' AND CodeMem='" & CodeCojoSup.Item(k) & "'"
                    ExecuteNonQuery(query)
                    query = "DELETE FROM t_dao_evalcojo WHERE NumeroDAO='" & NumDossier & "' AND id_cojo='" & CodeCojoSup.Item(k) & "'"
                    ExecuteNonQuery(query)
                Next
            End If
            CodeCojoSup.Clear()
        End If
        Try
            'MajPlanMarche(NumDelai.Value.ToString & " " & CmbDelai.Text)
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        Return True
    End Function

#End Region

#Region "Conformité technique"
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
        TxtLieuLivraison.Text = ""
        TxtLieuLivraison.Enabled = False
        TxtUniteBien.Text = ""
        CmbUniteBien.Text = ""
        CmbUniteBien.Enabled = False
        NumQteBien.Value = 1
        NumQteBien.Enabled = False
        TxtLibelleBien.Text = ""
        TxtLibelleBien.Enabled = False
        TxtCodeBien.Text = ""
        BtCategBien.Enabled = False
        TxtLibCategBien.Text = ""
        ViderSaisieBien()
        ViderSaisieCaract()
        LockSaisieBien()
        LoadSpecTech()
        TxtLibCategBien.ResetText()
        cmbLotSpecTech.ResetText()
        TxtLibLotSpecTech.ResetText()
        CmbSousLotSpecTech.ResetText()
        TxtSousLotSpecTech.ResetText()
        cmbLotSpecTech.Focus()
        CodeSpecTechSup.Clear()
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
                For Each rw As DataRow In dt.Rows
                    TxtLibLotSpecTech.Text = MettreApost(rw("LibelleLot").ToString)
                    If Val(GetSousLot(cmbLotSpecTech.Text, NumDoss)(0)) = 0 Then
                        CmbSousLotSpecTech.Enabled = False
                    Else
                        CmbSousLotSpecTech.Enabled = True
                    End If
                Next

                If (CmbSousLotSpecTech.Enabled = False) Then
                    BtCategBien.Enabled = True
                    MajCmbUnite()
                Else
                    BtCategBien.Enabled = False
                    LesSousLots(GetRefLot(Val(cmbLotSpecTech.Text), NumDoss), CmbSousLotSpecTech)
                End If
                ViderSaisieBien()
                TxtLibCategBien.ResetText()
            End If
        End If

        If BtCategBien.Enabled = False Then
            ListeSpecTech.Nodes.Clear()
            ChargerGridSpecif(-1, -1) 'Pour vider
        Else
            ChargerGridSpecif(cmbLotSpecTech.SelectedIndex, CmbSousLotSpecTech.SelectedIndex)
            actualiserListe(cmbLotSpecTech.Text, CmbSousLotSpecTech.Text)
            'ListeSpecTech.Nodes.Clear()
            'ListeSpecTech.BeginUnboundLoad()
            'Dim parentForRootNodes As TreeListNode = Nothing
            'query = "select * from T_SpecTechFourniture where NumeroDAO='" & NumDoss & "' and CodeLot='" & cmbLotSpecTech.Text & "' and CodeSousLot='" & CmbSousLotSpecTech.Text & "' order by CodeFournit"
            'Dim dt2 As DataTable = ExcecuteSelectQuery(query)
            'For Each rw In dt2.Rows
            '    Dim rootNode As TreeListNode = ListeSpecTech.AppendNode(New Object() {rw("RefSpecFournit").ToString, MettreApost(rw("CodeFournit").ToString), MettreApost(rw("DescripFournit").ToString), rw("QteFournit").ToString & " " & MettreApost(rw("UniteFournit").ToString), MettreApost(rw("LieuLivraison").ToString), rw("CodeCategorie").ToString, rw("CodeLot").ToString, rw("CodeSousLot").ToString, False}, parentForRootNodes)
            '    query = "select * from T_SpecTechCaract where RefSpecFournit='" & rw("RefSpecFournit").ToString & "'"
            '    Dim dt3 As DataTable = ExcecuteSelectQuery(query)
            '    For Each rw1 As DataRow In dt3.Rows
            '        ListeSpecTech.AppendNode(New Object() {rw1("RefSpecCaract").ToString, "", "     - " & MettreApost(rw1("LibelleCaract").ToString) & "  :  " & MettreApost(rw1("ValeurCaract").ToString), "", "", "", "", "", False}, rootNode)
            '    Next
            'Next
            'ListeSpecTech.EndUnboundLoad()
        End If

    End Sub
    Private Sub CmbSousLotSpecTech_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSousLotSpecTech.SelectedValueChanged
        If CmbSousLotSpecTech.SelectedIndex = -1 Then
            TxtSousLotSpecTech.ResetText()
            ChargerGridSpecif(-1, -1) 'Pour vider
            BtCategBien.Enabled = False
            Exit Sub
        Else
            query = "select LibelleSousLot from T_LotDAO_SousLot where CodeSousLot='" & CmbSousLotSpecTech.Text & "' and NumeroDAO='" & NumDoss & "' AND RefLot='" & GetRefLot(Val(cmbLotSpecTech.Text), NumDoss) & "' LIMIT 1"
            TxtSousLotSpecTech.Text = MettreApost(ExecuteScallar(query))
            BtCategBien.Enabled = True
        End If

        ChargerGridSpecif(cmbLotSpecTech.SelectedIndex, CmbSousLotSpecTech.SelectedIndex)
        actualiserListe(cmbLotSpecTech.Text, CmbSousLotSpecTech.Text)
        'ListeSpecTech.Nodes.Clear()
        'ListeSpecTech.BeginUnboundLoad()
        'Dim parentForRootNodes As TreeListNode = Nothing
        'query = "select * from T_SpecTechFourniture where NumeroDAO='" & NumDoss & "' and CodeLot='" & cmbLotSpecTech.Text & "' and CodeSousLot='" & CmbSousLotSpecTech.Text & "' order by CodeFournit"
        'Dim dt2 As DataTable = ExcecuteSelectQuery(query)
        'For Each rw In dt2.Rows
        '    Dim rootNode As TreeListNode = ListeSpecTech.AppendNode(New Object() {rw("RefSpecFournit").ToString, MettreApost(rw("CodeFournit").ToString), MettreApost(rw("DescripFournit").ToString), rw("QteFournit").ToString & " " & MettreApost(rw("UniteFournit").ToString), MettreApost(rw("LieuLivraison").ToString), rw("CodeCategorie").ToString, rw("CodeLot").ToString, rw("CodeSousLot").ToString, False}, parentForRootNodes)
        '    query = "select * from T_SpecTechCaract where RefSpecFournit='" & rw("RefSpecFournit").ToString & "'"
        '    Dim dt3 As DataTable = ExcecuteSelectQuery(query)
        '    For Each rw1 As DataRow In dt3.Rows
        '        ListeSpecTech.AppendNode(New Object() {rw1("RefSpecCaract").ToString, "", "     - " & MettreApost(rw1("LibelleCaract").ToString) & "  :  " & MettreApost(rw1("ValeurCaract").ToString), "", "", "", "", "", False}, rootNode)
        '    Next
        'Next
        'ListeSpecTech.EndUnboundLoad()
        MajCmbUnite()

    End Sub
    Private Sub BtCategBien_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtCategBien.Click
        Dim NewCat As New DiagChoixTypeFourniture
        Dim drRetour As DataRow = Nothing
        If NewCat.ShowDialog() = DialogResult.OK Then
            drRetour = NewCat.drRetour
        End If

        If Not IsNothing(drRetour) Then
            Dim nbFournit As Decimal = 0
            'query = "select Count(*) from T_SpecTechFourniture where NumeroDAO='" & NumDoss & "' and CodeLot='" & cmbLotSpecTech.Text & "' and CodeSousLot='" & CmbSousLotSpecTech.Text & "'"
            'Dim dt As DataTable = ExcecuteSelectQuery(query)
            'For Each rw As DataRow In dt.Rows
            '    nbFournit = CDec(rw(0))
            'Next
            For i = 0 To SaveDonnee.Nodes.Count - 1
                If SaveDonnee.Nodes(i).GetValue("NumLotSav") = cmbLotSpecTech.Text And SaveDonnee.Nodes(i).GetValue("NumSousLotSav") = CmbSousLotSpecTech.Text Then
                    nbFournit += 1
                End If
            Next
            nbFournit += 1
            'ViderSaisieBien()
            If modifSpecTech = False Then
                UnlockSaisieBien()
                TxtCodeBien.Text = "E" & nbFournit.ToString
            Else
                txtCodeCateg.Text = drRetour("IdItem") & "-" & drRetour("Type")
            End If
            TypeCategorieSpecTech = drRetour("IdItem") & "-" & drRetour("Type")
            TxtLibCategBien.Text = drRetour("Libellé").ToString().Replace("     - ", "")
            TxtCodeBien.Select()
        Else
            If modifSpecTech = False Then
                LockSaisieBien()
                ViderSaisieBien()
                TxtLibCategBien.ResetText()
            End If
        End If
    End Sub
    Private Sub LockSaisieBien()
        TxtCodeBien.Enabled = False
        TxtLibelleBien.Enabled = False
        NumQteBien.Enabled = False
        CmbUniteBien.Enabled = False
        TxtLieuLivraison.Enabled = False
        ChkLieuLivraison.Enabled = False
        TxtLibelleCaract.Enabled = False
        TxtValeurCaract.Enabled = False
        BtEnregBien.Enabled = False
        btRetourBien.Enabled = False
    End Sub
    Private Sub UnlockSaisieBien()
        TxtCodeBien.Enabled = True
        TxtLibelleBien.Enabled = True
        NumQteBien.Enabled = True
        CmbUniteBien.Enabled = True
        TxtLieuLivraison.Enabled = True
        ChkLieuLivraison.Enabled = True
        TxtLibelleCaract.Enabled = True
        TxtValeurCaract.Enabled = True
        BtEnregBien.Enabled = True
        btRetourBien.Enabled = True
    End Sub
    Private Sub UnlockModBien()
        TxtCodeBien.Enabled = True
        TxtLibelleBien.Enabled = True
        NumQteBien.Enabled = True
        CmbUniteBien.Enabled = True
        TxtLieuLivraison.Enabled = True
        ChkLieuLivraison.Enabled = True
        TxtLibelleCaract.Enabled = False
        TxtValeurCaract.Enabled = False
        BtEnregBien.Enabled = True
        btRetourBien.Enabled = True
        cmbLotSpecTech.Enabled = False
        CmbSousLotSpecTech.Enabled = False
        BtCategBien.Enabled = True
    End Sub
    Private Sub UnlockModCaract()
        BtCategBien.Enabled = False
        cmbLotSpecTech.Enabled = False
        CmbSousLotSpecTech.Enabled = False
        TxtCodeBien.Enabled = False
        TxtLibelleBien.Enabled = False
        NumQteBien.Enabled = False
        CmbUniteBien.Enabled = False
        TxtLieuLivraison.Enabled = False
        ChkLieuLivraison.Enabled = False
        TxtLibelleCaract.Enabled = True
        TxtValeurCaract.Enabled = True
        BtEnregBien.Enabled = True
        btRetourBien.Enabled = True
    End Sub
    Private Sub ViderSaisieBien()
        TxtCodeBien.ResetText()
        TxtLibelleBien.ResetText()
        NumQteBien.Value = 1
        CmbUniteBien.ResetText()
        TxtLieuLivraison.ResetText()
        ViderSaisieCaract()
    End Sub
    Private Sub actualiserListe(ByVal NumLot As String, ByVal NumSousLot As String)
        ListeSpecTech.Nodes.Clear()
        ListeSpecTech.BeginUnboundLoad()
        For i = 0 To SaveDonnee.Nodes.Count - 1
            If SaveDonnee.Nodes(i).GetValue("NumLotSav") = NumLot And SaveDonnee.Nodes(i).GetValue("NumSousLotSav") = NumSousLot Then
                Dim parentForRootNodes As TreeListNode = Nothing
                Dim rootNode As TreeListNode = ListeSpecTech.AppendNode(New Object() {SaveDonnee.Nodes(i).GetValue("IdentifiantSav"), SaveDonnee.Nodes(i).GetValue("CodeSav"), SaveDonnee.Nodes(i).GetValue("LibelleSav"), SaveDonnee.Nodes(i).GetValue("QuantiteSav"), SaveDonnee.Nodes(i).GetValue("LieuLivreSav"), SaveDonnee.Nodes(i).GetValue("CodeCategSav"), SaveDonnee.Nodes(i).GetValue("NumLotSav"), SaveDonnee.Nodes(i).GetValue("NumSousLotSav"), SaveDonnee.Nodes(i).GetValue("EditSav")}, parentForRootNodes)
                For j = 0 To SaveDonnee.Nodes(i).Nodes.Count - 1
                    ListeSpecTech.AppendNode(New Object() {SaveDonnee.Nodes(i).Nodes(j).GetValue("IdentifiantSav"), SaveDonnee.Nodes(i).Nodes(j).GetValue("CodeSav"), SaveDonnee.Nodes(i).Nodes(j).GetValue("LibelleSav"), SaveDonnee.Nodes(i).Nodes(j).GetValue("QuantiteSav"), SaveDonnee.Nodes(i).Nodes(j).GetValue("LieuLivreSav"), SaveDonnee.Nodes(i).Nodes(j).GetValue("CodeCategSav"), SaveDonnee.Nodes(i).Nodes(j).GetValue("NumLotSav"), SaveDonnee.Nodes(i).Nodes(j).GetValue("NumSousLotSav"), SaveDonnee.Nodes(i).Nodes(j).GetValue("EditSav"), False}, rootNode)
                Next
            End If
        Next
        ListeSpecTech.EndUnboundLoad()
    End Sub
    Private Sub BtEnregBien_Click(sender As Object, e As EventArgs) Handles BtEnregBien.Click
        If modifSpecTech = False Then
            If cmbLotSpecTech.IsRequiredControl("Veuillez choisir un lot.") Then
                Exit Sub
            End If
            If CmbSousLotSpecTech.Enabled Then
                If CmbSousLotSpecTech.IsRequiredControl("Veuillez choisir un sous lot.") Then
                    Exit Sub
                End If
            End If
            If TxtLibCategBien.IsRequiredControl("Veuillez choisir une catégorie.") Then
                Exit Sub
            End If
            If TxtCodeBien.IsRequiredControl("Veuillez entrer le code du bien.") Then
                Exit Sub
            End If
            If TxtLibelleBien.IsRequiredControl("Veuillez entrer le libellé du bien.") Then
                Exit Sub
            End If
            If NumQteBien.Value <= 0 Then
                SuccesMsg("Veuillez entrer la quantité.")
                Exit Sub
            End If
            If CmbUniteBien.IsRequiredControl("Veuillez choisir une unité.") Then
                Exit Sub
            End If
            If TxtLieuLivraison.IsRequiredControl("Veuillez entrer le lieu de livraison.") Then
                Exit Sub
            End If
            If TxtLibelleCaract.IsRequiredControl("Veuillez saisir la caractéristique.") Then
                Exit Sub
            End If
            If TxtValeurCaract.IsRequiredControl("Veuillez saisir la valeur de la caractéristique demandée.") Then
                Exit Sub
            End If

            Dim dtSpecTech As DataTable = GridSpecifTech.DataSource
            Dim drS As DataRow = dtSpecTech.NewRow

            Dim NumIndex As Integer = IsSavedItemInGridView(TxtCodeBien.Text.Trim(), ViewSpecTechn, "Code")
            If NumIndex = -1 Then
                drS("Id") = "##"
                drS("Code") = TxtCodeBien.Text.Trim()
                drS("Libellé") = TxtLibelleBien.Text.Trim()
                drS("Quantité") = NumQteBien.Value & " " & CmbUniteBien.Text
                drS("Lieu de livraison") = TxtLieuLivraison.Text.Trim()
                drS("CodeCateg") = TypeCategorieSpecTech
                drS("NumLot") = cmbLotSpecTech.Text
                drS("NumSousLot") = CmbSousLotSpecTech.Text
                drS("Edit") = False
                dtSpecTech.Rows.Add(drS)

                drS = dtSpecTech.NewRow()
                drS("Id") = "##"
                drS("Code") = ""
                drS("Libellé") = "   - " & TxtLibelleCaract.Text.Trim() & "  :  " & TxtValeurCaract.Text.Trim()
                drS("Quantité") = ""
                drS("Lieu de livraison") = ""
                drS("CodeCateg") = ""
                drS("NumLot") = cmbLotSpecTech.Text
                drS("NumSousLot") = CmbSousLotSpecTech.Text
                drS("Edit") = False
                dtSpecTech.Rows.Add(drS)
            Else
                drS("Id") = "##"
                drS("Code") = ""
                drS("Libellé") = "   - " & TxtLibelleCaract.Text.Trim() & "  :  " & TxtValeurCaract.Text.Trim()
                drS("Quantité") = ""
                drS("Lieu de livraison") = ""
                drS("CodeCateg") = ""
                drS("NumLot") = cmbLotSpecTech.Text
                drS("NumSousLot") = CmbSousLotSpecTech.Text
                drS("Edit") = False
                dtSpecTech.Rows.InsertAt(drS, NumIndex + 1)
            End If
            ViewSpecTechn.OptionsView.ColumnAutoWidth = True
            ColorRowGrid(ViewSpecTechn, "[Quantité]<>''", Color.LightBlue, "Tahoma", 8, FontStyle.Bold, Color.Black)
            For i = 0 To SaveDonnee.Nodes.Count - 1
                If SaveDonnee.Nodes(i).Item("CodeSav") = TxtCodeBien.Text.Trim() And SaveDonnee.Nodes(i).Item("LibelleSav").ToString.ToLower <> TxtLibelleBien.Text.Trim().ToLower And SaveDonnee.Nodes(i).Item("NumLotSav") = cmbLotSpecTech.Text And SaveDonnee.Nodes(i).Item("NumSousLotSav") = CmbSousLotSpecTech.Text Then
                    SuccesMsg("Ce code de bien exitse déjà")
                    Exit Sub
                End If
                If SaveDonnee.Nodes(i).Item("CodeSav") = TxtCodeBien.Text.Trim() Then
                    If SaveDonnee.Nodes(i).Item("LibelleSav").ToString.ToLower = TxtLibelleBien.Text.Trim().ToLower Then
                        Dim CatNode As TreeListNode = SaveDonnee.Nodes(i)
                        SaveDonnee.AppendNode(New Object() {"##", "", "     - " & TxtLibelleCaract.Text.Trim() & "  :  " & TxtValeurCaract.Text.Trim(), "", "", "", cmbLotSpecTech.Text, CmbSousLotSpecTech.Text, False}, CatNode)
                        ViderSaisieCaract()
                        TxtLibelleCaract.Focus()
                        actualiserListe(cmbLotSpecTech.Text, CmbSousLotSpecTech.Text)
                        Exit Sub
                    End If
                End If
            Next
            Dim parentForRootNodes As TreeListNode = Nothing
            Dim rootNode As TreeListNode = SaveDonnee.AppendNode(New Object() {"##", TxtCodeBien.Text.Trim(), TxtLibelleBien.Text.Trim(), NumQteBien.Value & " " & CmbUniteBien.Text, TxtLieuLivraison.Text.Trim(), TypeCategorieSpecTech, cmbLotSpecTech.Text, CmbSousLotSpecTech.Text, False}, parentForRootNodes)
            SaveDonnee.AppendNode(New Object() {"##", "", "     - " & TxtLibelleCaract.Text.Trim() & "  :  " & TxtValeurCaract.Text.Trim(), "", "", "", cmbLotSpecTech.Text, CmbSousLotSpecTech.Text, False}, rootNode)
            actualiserListe(cmbLotSpecTech.Text, CmbSousLotSpecTech.Text)
            ViderSaisieCaract()
            TxtLibelleCaract.Focus()
        Else
            If TxtLibelleCaract.Enabled = False Then
                If cmbLotSpecTech.IsRequiredControl("Veuillez choisir un lot.") Then
                    Exit Sub
                End If
                If CmbSousLotSpecTech.Enabled Then
                    If CmbSousLotSpecTech.IsRequiredControl("Veuillez choisir un sous lot.") Then
                        Exit Sub
                    End If
                End If
                If TxtLibCategBien.IsRequiredControl("Veuillez choisir une catégorie.") Then
                    Exit Sub
                End If
                If TxtCodeBien.IsRequiredControl("Veuillez entrer le code du bien.") Then
                    Exit Sub
                End If
                If TxtLibelleBien.IsRequiredControl("Veuillez entrer le libellé du bien.") Then
                    Exit Sub
                End If
                If NumQteBien.Value <= 0 Then
                    SuccesMsg("Veuillez entrer la quantité.")
                    Exit Sub
                End If
                If CmbUniteBien.IsRequiredControl("Veuillez choisir une unité.") Then
                    Exit Sub
                End If
                If TxtLieuLivraison.IsRequiredControl("Veuillez entrer le lieu de livraison.") Then
                    Exit Sub
                End If
                For i = 0 To SaveDonnee.Nodes.Count - 1
                    If SaveDonnee.Nodes(i).GetValue("IdentifiantSav") = NodeModSpec.GetValue("Identifiant") And SaveDonnee.Nodes(i).GetValue("CodeSav") = NodeModSpec.GetValue("Code") And SaveDonnee.Nodes(i).GetValue("LibelleSav") = NodeModSpec.GetValue("Libelle") And SaveDonnee.Nodes(i).GetValue("NumLotSav") = NodeModSpec.GetValue("NumLot") And SaveDonnee.Nodes(i).GetValue("NumSousLotSav") = NodeModSpec.GetValue("NumSousLot") Then
                        SaveDonnee.Nodes(i).SetValue("CodeSav", TxtCodeBien.Text.Trim())
                        SaveDonnee.Nodes(i).SetValue("LibelleSav", TxtLibelleBien.Text.Trim())
                        SaveDonnee.Nodes(i).SetValue("QuantiteSav", NumQteBien.Value & " " & CmbUniteBien.Text)
                        SaveDonnee.Nodes(i).SetValue("LieuLivreSav", TxtLieuLivraison.Text.Trim())
                        SaveDonnee.Nodes(i).SetValue("CodeCategSav", txtCodeCateg.Text)
                        Exit For
                    End If
                Next
                NodeModSpec.SetValue("Code", TxtCodeBien.Text.Trim())
                NodeModSpec.SetValue("Libelle", TxtLibelleBien.Text.Trim())
                NodeModSpec.SetValue("Quantite", NumQteBien.Value & " " & CmbUniteBien.Text)
                NodeModSpec.SetValue("LieuLivre", TxtLieuLivraison.Text.Trim())
                NodeModSpec.SetValue("CodeCateg", txtCodeCateg.Text)
            Else
                If TxtLibelleCaract.IsRequiredControl("Veuillez saisir la caractéristique.") Then
                    Exit Sub
                End If
                If TxtValeurCaract.IsRequiredControl("Veuillez saisir la valeur de la caractéristique demandée.") Then
                    Exit Sub
                End If
                For i = 0 To SaveDonnee.Nodes.Count - 1
                    If SaveDonnee.Nodes(i).GetValue("IdentifiantSav") = NodeModSpec.ParentNode.GetValue("Identifiant") And SaveDonnee.Nodes(i).GetValue("CodeSav") = NodeModSpec.ParentNode.GetValue("Code") And SaveDonnee.Nodes(i).GetValue("LibelleSav") = NodeModSpec.ParentNode.GetValue("Libelle") And SaveDonnee.Nodes(i).GetValue("NumLotSav") = NodeModSpec.ParentNode.GetValue("NumLot") And SaveDonnee.Nodes(i).GetValue("NumSousLotSav") = NodeModSpec.ParentNode.GetValue("NumSousLot") Then
                        For j = 0 To SaveDonnee.Nodes(i).Nodes.Count - 1
                            If SaveDonnee.Nodes(i).Nodes(j).GetValue("IdentifiantSav") = NodeModSpec.GetValue("Identifiant") And SaveDonnee.Nodes(i).Nodes(j).GetValue("LibelleSav") = NodeModSpec.GetValue("Libelle") Then
                                SaveDonnee.Nodes(i).Nodes(j).SetValue("LibelleSav", "    - " & TxtLibelleCaract.Text.Trim() & "  :  " & TxtValeurCaract.Text.Trim())
                                Exit For
                            End If
                        Next
                        Exit For
                    End If
                Next
                NodeModSpec.SetValue("Libelle", "    - " & TxtLibelleCaract.Text.Trim() & "  :  " & TxtValeurCaract.Text.Trim())

            End If
            txtCodeCateg.Text = ""
            TxtLibCategBien.Text = ""
            ViderSaisieBien()
            modifSpecTech = False
            cmbLotSpecTech.Enabled = True
            CmbSousLotSpecTech.Enabled = True
            BtCategBien.Enabled = True
            LockSaisieBien()
        End If
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
        Dim CurrentLot As DaoSpecTechLot = SpecTech(LotIndex)
        If CurrentLot.AreSousLot Then
            If SousLotIndex < 0 Then
                GridSpecifTech.DataSource = New DataTable

                'dt.Rows.Clear()
                Exit Sub
            Else
                Dim CurrentSousLot As DaoSpecTechSousLot = CurrentLot.GetSousLot(SousLotIndex)
                If IsNothing(CurrentSousLot.DataTable) Then
                    GridSpecifTech.DataSource = New DataTable

                    'dt.Rows.Clear()
                    Exit Sub
                Else
                    GridSpecifTech.DataSource = CurrentSousLot.DataTable

                End If
            End If
        Else
            If IsNothing(CurrentLot.DataTable) Then
                GridSpecifTech.DataSource = New DataTable


                'dt.Rows.Clear()
                Exit Sub
            Else
                GridSpecifTech.DataSource = CurrentLot.DataTable
            End If
        End If

        ViewSpecTechn.OptionsView.ColumnAutoWidth = True
        ColorRowGrid(ViewSpecTechn, "[Quantité]<>''", Color.LightBlue, "Tahoma", 8, FontStyle.Bold, Color.Black)
    End Sub
    Private Sub LoadSpecTech()
        SpecTech.Clear()
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

            SpecTech.Add(NewLotSpecTech)

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
        For i = 0 To SaveDonnee.Nodes.Count - 1
            Dim IdBien As String = String.Empty
            Dim Qte As String = Split(SaveDonnee.Nodes(i).GetValue("QuantiteSav"), " ")(0)
            Dim Unite As String = Split(SaveDonnee.Nodes(i).GetValue("QuantiteSav"), " ")(1)
            If SaveDonnee.Nodes(i).GetValue("IdentifiantSav").ToString = "##" And SaveDonnee.Nodes(i).GetValue("CodeSav").ToString <> "" Then
                query = "INSERT INTO T_SpecTechFourniture(RefSpecFournit,CodeCategorie,NumeroDAO,CodeLot,CodeSousLot,CodeFournit,DescripFournit,QteFournit,UniteFournit,LieuLivraison)"
                query &= " VALUES(NULL,'" & EnleverApost(SaveDonnee.Nodes(i).GetValue("CodeCategSav")) & "','" & NumDoss & "','" & SaveDonnee.Nodes(i).GetValue("NumLotSav") & "','" & SaveDonnee.Nodes(i).GetValue("NumSousLotSav") & "'"
                query &= ",'" & SaveDonnee.Nodes(i).GetValue("CodeSav") & "','" & EnleverApost(SaveDonnee.Nodes(i).GetValue("LibelleSav")) & "','" & Qte & "','" & EnleverApost(Unite) & "','" & EnleverApost(SaveDonnee.Nodes(i).GetValue("LieuLivreSav")) & "')"
                ExecuteNonQuery(query)
                query = "SELECT MAX(RefSpecFournit) FROM T_SpecTechFourniture WHERE CodeLot='" & SaveDonnee.Nodes(i).GetValue("NumLotSav") & "' AND CodeSousLot='" & SaveDonnee.Nodes(i).GetValue("NumSousLotSav") & "' AND NumeroDAO='" & NumDoss & "'"
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
                query = "UPDATE T_SpecTechFourniture SET CodeFournit='" & EnleverApost(SaveDonnee.Nodes(i).GetValue("CodeSav")) & "', DescripFournit='" & EnleverApost(SaveDonnee.Nodes(i).GetValue("LibelleSav")) & "',"
                query &= "QteFournit='" & Qte & "', UniteFournit='" & EnleverApost(Unite) & "', LieuLivraison='" & EnleverApost(SaveDonnee.Nodes(i).GetValue("LieuLivreSav")) & "' WHERE RefSpecFournit='" & SaveDonnee.Nodes(i).GetValue("IdentifiantSav") & "'"
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
        If CodeSpecTechSup.Count > 0 Then
            For k = 0 To CodeSpecTechSup.Count - 1
                If CodeSpecTechSup.Item(k).ToString.Split("-")(0) <> "##" Then
                    Dim Cat = CodeSpecTechSup.Item(k).ToString.Split("-")(1)
                    Dim Code = CInt(CodeSpecTechSup.Item(k).ToString.Split("-")(0))
                    If Cat <> "" Then
                        query = "DELETE FROM T_SpecTechFourniture WHERE NumeroDAO='" & NumDossier & "' AND RefSpecFournit='" & Code & "'"
                        ExecuteNonQuery(query)
                    Else
                        query = "DELETE FROM t_spectechcaract WHERE NumeroDAO='" & NumDossier & "' AND RefSpecCaract='" & Code & "'"
                        ExecuteNonQuery(query)
                    End If
                End If
            Next
        End If
        CodeSpecTechSup.Clear()
        actualiserListe(cmbLotSpecTech.Text, CmbSousLotSpecTech.Text)
        Return True
    End Function
#End Region

#Region "DQE"

    Private Sub InitDQE()
        ' CmbLotDQE.Text = ""
        'CmbLotDQE.Enabled = True
        'CmbSousLotDQE.Text = ""
        'CmbSousLotDQE.Enabled = False
        LoadLotDQE()
        ' CmbSousLot1.Text = ""
        ' CmbSousLot1.Enabled = False
        'TxtImportDQE.Text = ""
        'btOpenDQE.Enabled = False
        ' GroupControl17.Width = GroupControl13.Width - (GbItemDQE.Width + 8)
        'GroupControl17.Location = New System.Drawing.Point(GbItemDQE.Width + 5, 24)
        ' CmbNumLot2.Text = ""
        ' CmbNumLot2.Enabled = False
        ' RdSection.Checked = True
        ' DataGridView1.Rows.Clear()
        'GbItemDQE.Enabled = False
        'GbItemDQE.Visible = False
        'GridSousSection.Rows.Clear()
        'GbSousSection.Visible = False
        'ChkSousSection.Checked = False
    End Sub
    Private Sub LoadLotDQE()
        'CmbLotDQE.ResetText()
        ' CmbLotDQE.Properties.Items.Clear()
        query = "select RefLot,CodeLot from T_LotDAO where NumeroDAO='" & NumDoss & "' order by CodeLot"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rwLot As DataRow In dt.Rows
            ' CmbLotDQE.Properties.Items.Add(rwLot("CodeLot"))
        Next
    End Sub
    Private Sub CmbLotDQE_SelectedIndexChanged(sender As Object, e As EventArgs)
        'txtLibLotDQE.ResetText()
        'CmbSousLotDQE.Properties.Items.Clear()
        'CmbSousLotDQE.ResetText()
        'TxtSousLotDQE.ResetText()
        'If CmbLotDQE.SelectedIndex <> -1 Then
        '    If (NumDoss <> "") Then
        '        query = "select LibelleLot,SousLot,RefLot from T_LotDAO where NumeroDAO='" & NumDoss & "' and CodeLot='" & CmbLotDQE.Text & "'"
        '        Dim dt As DataTable = ExcecuteSelectQuery(query)
        '        For Each rw As DataRow In dt.Rows
        '            TxtRefLot1.Text = rw(1).ToString
        '            txtLibLotDQE.Text = MettreApost(rw("LibelleLot").ToString)
        '            If Val(GetSousLot(CmbLotDQE.Text, NumDoss)(0)) = 0 Then
        '                CmbSousLotDQE.Enabled = False
        '            Else
        '                CmbSousLotDQE.Enabled = True
        '            End If
        '        Next

        '        If (CmbSousLotDQE.Enabled = False) Then

        '        Else
        '            LesSousLots(GetRefLot(Val(CmbLotDQE.Text), NumDoss), CmbSousLotDQE)
        '        End If
        '    End If
        'End If
        'MajGridDQE()
    End Sub

    Private Sub BtImportDQE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If CmbLotDQE.IsRequiredControl("Veuillez choisir un lot.") Then
        '    Exit Sub
        'End If
        'If CmbSousLotDQE.Properties.Items.Count > 0 Then
        '    If CmbSousLotDQE.IsRequiredControl("Veuillez choisir un sous lot.") Then
        '        Exit Sub
        '    End If
        'End If

        'If txtFilePathDQE.Text.Trim() = String.Empty Then
        '    SuccesMsg("Veuillez choisir le fichier à importer.")
        '    Exit Sub
        'End If

        'If Not File.Exists(txtFilePathDQE.Text) Then
        '    FailMsg("Le fichier n'existe pas.")
        '    Exit Sub
        'End If

        '' Vérification du format du fichier
        'DebutChargement(True, "Vérification du format du fichier en cours...")
        Dim FileName As String = ""
        Dim app As New Excel.Application
        app.Workbooks.Open(FileName)

        For i As Integer = 1 To app.Workbooks(1).Worksheets.Count()
            Dim Feuille = app.Workbooks(1).Worksheets(i)
            Dim FeuilleName = Feuille.Name
            Dim RowCount = Feuille.Cells(Feuille.Rows.Count, 1).End(Excel.XlDirection.xlUp).Row
            If RowCount < 4 Then
                app.Quit()
                FinChargement()
                FailMsg("La feuille de calcul """ & FeuilleName & """" & " n'a pas le bon format d'importation")
                Exit Sub
            End If

            Dim Titre As String = Feuille.Range("A4").Value
            If IsNothing(Titre) Then
                app.Quit()
                FinChargement()
                FailMsg("La feuille de calcul """ & FeuilleName & """" & " n'a pas le bon format d'importation")
                Exit Sub
            End If

            If Titre.ToLower() <> "type" Then
                app.Quit()
                FinChargement()
                FailMsg("La feuille de calcul """ & FeuilleName & """" & " n'a pas le bon format d'importation")
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
                Dim CurrentLib As String = Feuille.Range("C" & l).Value
                Dim CurrentNumero As String = Feuille.Range("B" & l).Value
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
                Else
                    If CurrentType.Trim().ToLower() <> "elt" And CurrentType.Trim().ToLower <> "sec" Then
                        app.Quit()
                        FinChargement()
                        FailMsg("""" & CurrentType.Trim() & """ n'est pas un code reconnu pour la colonne ""Type"" à la cellule A" & l & " sur la feuille """ & FeuilleName & """")
                        Exit Sub
                    End If
                End If
            Next

            FinChargement()
            Dim EraseOldData As Boolean = "" ' chkEraseDQE.Checked
            Dim ResultDialog As DialogResult
            If Not EraseOldData Then
                ResultDialog = ConfirmMsg("Vérification terminée." & vbNewLine & "Voulez-vous commencer l'importation?")
            Else
                ResultDialog = ConfirmMsgWarning("Attention les anciennes données seront supprimées !!!" & vbNewLine & "Voulez-vous importer ce fichier?")
            End If

            If ResultDialog <> DialogResult.Yes Then
                app.Quit()
                Exit Sub
            End If

            Dim LastIdSection As String = String.Empty
            For l = 5 To RowCount
                Dim CurrentType As String = Feuille.Range("A" & l).Value
                Dim CurrentLib As String = Feuille.Range("C" & l).Value
                Dim CurrentNumero As String = Feuille.Range("B" & l).Value
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
                    '    If EraseOldData Then
                    '        query = "DELETE FROM t_dqeitem WHERE RefSection IN(SELECT RefSection FROM t_dqesection WHERE NumeroDAO='" & NumDoss & "' AND CodeLot='" & CmbLotDQE.Text & "' AND CodeSousLot='" & CmbSousLotDQE.Text & "')"
                    '        ExecuteNonQuery(query)
                    '        query = "DELETE FROM t_dqesection WHERE NumeroDAO='" & NumDoss & "' AND CodeLot='" & CmbLotDQE.Text & "' AND CodeSousLot='" & CmbSousLotDQE.Text & "'"
                    '        ExecuteNonQuery(query)
                    '    End If
                    '    query = "INSERT INTO t_dqesection VALUES(NULL,'" & NumDoss & "','" & CurrentNumero.EnleverApostrophe() & "','" & CurrentLib.EnleverApostrophe() & "','" & CmbLotDQE.Text & "','" & CmbSousLotDQE.Text & "')"
                    '    ExecuteNonQuery(query)
                    '    LastIdSection = ExecuteScallar("SELECT MAX(RefSection) FROM t_dqesection WHERE NumeroDAO='" & NumDoss & "' AND CodeLot='" & CmbLotDQE.Text & "' AND CodeSousLot='" & CmbSousLotDQE.Text & "'")
                    'Else

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
                        query = "INSERT INTO t_dqeitem VALUES(NULL,'" & LastIdSection & "','','" & CurrentNumero.EnleverApostrophe() & "','" & CurrentLib.EnleverApostrophe() & "','" & Unite.EnleverApostrophe() & "','" & Qte.EnleverApostrophe() & "','" & AfficherMonnaie(Pu.EnleverApostrophe()) & "','" & MontantTotal.EnleverApostrophe() & "','" & MontantLettre(Pu.EnleverApostrophe()) & "')"
                        ExecuteNonQuery(query)
                    ElseIf CurrentType.Trim().ToLower() = "sec" Then
                        'Enregistement de la section...
                        '    query = "INSERT INTO t_dqesection VALUES(NULL,'" & NumDoss & "','" & CurrentNumero.EnleverApostrophe() & "','" & CurrentLib.EnleverApostrophe() & "','" & CmbLotDQE.Text & "','" & CmbSousLotDQE.Text & "')"
                        '    ExecuteNonQuery(query)
                        '    LastIdSection = ExecuteScallar("SELECT MAX(RefSection) FROM t_dqesection WHERE NumeroDAO='" & NumDoss & "' AND CodeLot='" & CmbLotDQE.Text & "' AND CodeSousLot='" & CmbSousLotDQE.Text & "'")
                    End If

                End If
            Next
        Next

        app.Quit()
        ' GroupControl17.Width = GroupControl13.Width - 4
        'GroupControl17.Left = 2
        'GroupControl17.BringToFront()
        MajGridDQE()
        FinChargement()
        SuccesMsg("Importation effectuée avec succès.")



        'DebutChargement(True, "Importation des données Excel en cours...")

        'Dim partFichier() As String = FileName.ToString.Split("."c)
        'If (partFichier(1).ToLower <> "xlsx" And partFichier(1).ToLower <> "xls") Then
        '    MsgBox("Ce fichier n'est pas un fichier MS Excel!", MsgBoxStyle.Exclamation)
        '    Exit Sub
        'End If

        'TxtImportDQE.Text = FileName
        'Dim NomDossier As String = FormatFileName(line & "\DAO\" & TypeMarche & "\" & MethodMarche & "\" & NumDoss, "")
        'If (Directory.Exists(NomDossier) = True) Then
        '    File.Copy(FileName, NomDossier & "\FichierDQE_L" & CmbLotDQE.Text & IIf(CmbSousLotDQE.Enabled = True, "SL" & CmbSousLotDQE.Text.Replace(".", ""), "") & "." & partFichier(1), True)
        'End If

        'app = New Excel.Application
        'app.Workbooks.Open(FileName)
        'For i As Integer = 1 To 4
        '    If (app.Workbooks(1).Worksheets(1).Cells(2, i).value = Nothing) Then
        '        MsgBox("Format incorrect!", MsgBoxStyle.Exclamation)
        '        app.Quit()
        '        Exit Sub
        '    End If
        'Next
        'If (Mid(app.Workbooks(1).Worksheets(1).Cells(3, 1).value.ToString, 1, 7).ToLower <> "section") Then
        '    MsgBox("Format incorrect!", MsgBoxStyle.Exclamation)
        '    app.Quit()
        '    Exit Sub
        'End If

        'Dim LesRef(100) As String
        'Dim NbSect As Decimal = 0
        'query = "select RefSection from T_DQESection where NumeroDAO='" & NumDoss & "' and CodeLot='" & CmbLotDQE.Text & "' and CodeSousLot='" & IIf(CmbSousLotDQE.Enabled = True, CmbSousLotDQE.Text, "") & "'"
        'Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt0.Rows
        '    LesRef(NbSect) = rw(0).ToString
        '    NbSect += 1
        'Next

        'For k As Integer = 0 To NbSect - 1
        '    query = "DELETE from T_DQEItem where RefSection='" & LesRef(k) & "'"
        '    ExecuteNonQuery(query)

        '    query = "DELETE from T_DQESection_SousSection where RefSection='" & LesRef(k) & "'"
        '    ExecuteNonQuery(query)

        '    query = "DELETE from T_DQESection where RefSection='" & LesRef(k) & "'"
        '    ExecuteNonQuery(query)
        'Next

        'Dim SectionEncours As Decimal = 0
        'Dim SSenCours As String = ""
        'Dim MarkSection As Boolean = False
        'Dim YaSousSect As Boolean = False
        'Dim sqlconn As New MySqlConnection
        'BDOPEN(sqlconn)
        'For LigNe As Integer = 3 To 1000
        '    If (app.Workbooks(1).Worksheets(1).Cells(LigNe, 1).value = Nothing) Then MsgBox("Fin du fichier à la ligne " & LigNe.ToString, MsgBoxStyle.Information) : Exit For
        '    Dim partTyp() As String = app.Workbooks(1).Worksheets(1).Cells(LigNe, 1).value.ToString.Split(" "c)
        '    If (partTyp(0).ToLower = "section") Then   'Pour les sections ***********************
        '        If (app.Workbooks(1).Worksheets(1).Cells(LigNe, 2).value = Nothing) Then MsgBox("Format incorrect! (ligne " & LigNe.ToString & ")", MsgBoxStyle.Exclamation) : Exit For
        '        If (partTyp(1) <> "") Then
        '            Dim DatSet = New DataSet
        '            query = "select * from T_DQESection"

        '            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        '            Dim DatAdapt = New MySqlDataAdapter(Cmd)
        '            DatAdapt.Fill(DatSet, "T_DQESection")
        '            Dim DatTable = DatSet.Tables("T_DQESection")
        '            Dim DatRow = DatSet.Tables("T_DQESection").NewRow()

        '            DatRow("NumeroDAO") = NumDoss
        '            DatRow("NumeroSection") = partTyp(1)
        '            DatRow("Designation") = EnleverApost(app.Workbooks(1).Worksheets(1).Cells(LigNe, 2).value.ToString)
        '            DatRow("CodeLot") = CmbLotDQE.Text
        '            DatRow("CodeSousLot") = IIf(CmbSousLotDQE.Enabled = True, CmbSousLotDQE.Text, "").ToString

        '            DatSet.Tables("T_DQESection").Rows.Add(DatRow)
        '            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        '            DatAdapt.Update(DatSet, "T_DQESection")
        '            DatSet.Clear()

        '            query = "select RefSection from T_DQESection where NumeroDAO='" & NumDoss & "' and CodeLot='" & CmbLotDQE.Text & "' and NumeroSection='" & partTyp(1) & "'"
        '            dt0 = ExcecuteSelectQuery(query)
        '            For Each rw As DataRow In dt0.Rows
        '                SectionEncours = CInt(rw(0))
        '                MarkSection = True
        '            Next

        '        Else
        '            MsgBox("Importation interrompue! (ligne " & LigNe.ToString & ")", MsgBoxStyle.Exclamation)
        '            app.Quit()
        '            Exit For
        '        End If

        '    ElseIf (partTyp(0).ToLower = "sous") Then  'Pour les sous sections ****************

        '        If (MarkSection = True) Then
        '            YaSousSect = True
        '            MarkSection = False
        '        End If

        '        If (YaSousSect = True) Then
        '            If (app.Workbooks(1).Worksheets(1).Cells(LigNe, 2).value = Nothing) Then MsgBox("Format incorrect! (ligne " & LigNe.ToString & ")", MsgBoxStyle.Exclamation) : Exit For
        '            If (partTyp(2) <> "") Then
        '                Dim DatSet = New DataSet
        '                query = "select * from T_DQESection_SousSection"

        '                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        '                Dim DatAdapt = New MySqlDataAdapter(Cmd)
        '                DatAdapt.Fill(DatSet, "T_DQESection_SousSection")
        '                Dim DatTable = DatSet.Tables("T_DQESection_SousSection")
        '                Dim DatRow = DatSet.Tables("T_DQESection_SousSection").NewRow()

        '                DatRow("RefSection") = SectionEncours.ToString
        '                DatRow("NumeroDAO") = NumDoss
        '                DatRow("NumeroSousSection") = partTyp(2)
        '                DatRow("LibelleSousSection") = EnleverApost(app.Workbooks(1).Worksheets(1).Cells(LigNe, 2).value.ToString)

        '                DatSet.Tables("T_DQESection_SousSection").Rows.Add(DatRow)
        '                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        '                DatAdapt.Update(DatSet, "T_DQESection_SousSection")
        '                DatSet.Clear()

        '            Else
        '                MsgBox("Importation interrompue! (ligne " & LigNe.ToString & ")", MsgBoxStyle.Exclamation)
        '                app.Quit()
        '                Exit Sub
        '            End If

        '        Else
        '            MsgBox("Disposition données incorrecte! (ligne " & LigNe.ToString & ")", MsgBoxStyle.Exclamation)
        '            MsgBox("Importation interrompue! (ligne " & LigNe.ToString & ")", MsgBoxStyle.Exclamation)
        '            Exit For
        '        End If

        '    Else   'Pour les items ************************

        '        If (MarkSection = True) Then
        '            YaSousSect = False
        '            MarkSection = False
        '        End If

        '        If (partTyp(0) <> "") Then
        '            If (app.Workbooks(1).Worksheets(1).Cells(LigNe, 3).value <> Nothing Or app.Workbooks(1).Worksheets(1).Cells(LigNe, 4).value <> Nothing) Then
        '                Dim DatSet = New DataSet
        '                query = "select * from T_DQEItem"

        '                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        '                Dim DatAdapt = New MySqlDataAdapter(Cmd)
        '                DatAdapt.Fill(DatSet, "T_DQEItem")
        '                Dim DatTable = DatSet.Tables("T_DQEItem")
        '                Dim DatRow = DatSet.Tables("T_DQEItem").NewRow()

        '                DatRow("RefSection") = SectionEncours.ToString
        '                DatRow("NumeroItem") = partTyp(0)
        '                DatRow("Designation") = EnleverApost(app.Workbooks(1).Worksheets(1).Cells(LigNe, 2).value.ToString)
        '                DatRow("NumeroSousSection") = SSenCours

        '                If (app.Workbooks(1).Worksheets(1).Cells(LigNe, 3).value <> Nothing) Then
        '                    DatRow("UniteItem") = IIf(app.Workbooks(1).Worksheets(1).Cells(LigNe, 3).value.ToString.Replace(" ", "") <> "", app.Workbooks(1).Worksheets(1).Cells(LigNe, 3).value.ToString, "F").ToString
        '                Else
        '                    DatRow("UniteItem") = "F"
        '                End If
        '                Dim Qte As Decimal = 1
        '                If (app.Workbooks(1).Worksheets(1).Cells(LigNe, 4).value <> Nothing) Then
        '                    If (app.Workbooks(1).Worksheets(1).Cells(LigNe, 4).value.ToString.Replace(" ", "") <> "") Then
        '                        If (IsNumeric(app.Workbooks(1).Worksheets(1).Cells(LigNe, 4).value.ToString.Replace(" ", "")) = True) Then
        '                            DatRow("QteItem") = CDec(app.Workbooks(1).Worksheets(1).Cells(LigNe, 4).value.ToString.Replace(" ", "")).ToString
        '                            Qte = CDec(app.Workbooks(1).Worksheets(1).Cells(LigNe, 4).value.ToString.Replace(" ", "")).ToString
        '                        Else
        '                            DatSet.Clear()

        '                            MsgBox("Quantité non numérique! (ligne " & LigNe.ToString & ")", MsgBoxStyle.Exclamation)
        '                            Exit For
        '                        End If

        '                    Else
        '                        DatRow("QteItem") = "1"
        '                    End If
        '                Else
        '                    DatRow("QteItem") = "1"
        '                End If
        '                If (app.Workbooks(1).Worksheets(1).Cells(LigNe, 5).value <> Nothing) Then
        '                    If (IsNumeric(app.Workbooks(1).Worksheets(1).Cells(LigNe, 5).value) = True And app.Workbooks(1).Worksheets(1).Cells(LigNe, 5).value.ToString.Replace(" ", "") <> "") Then
        '                        DatRow("PuHtva") = AfficherMonnaie(CDec(app.Workbooks(1).Worksheets(1).Cells(LigNe, 5).value.ToString.Replace(" ", "")).ToString)
        '                        DatRow("PuHtvaLettre") = MontantLettre(CDec(app.Workbooks(1).Worksheets(1).Cells(LigNe, 5).value.ToString.Replace(" ", "")).ToString)
        '                        DatRow("MontHtva") = AfficherMonnaie(Math.Round(CDec(app.Workbooks(1).Worksheets(1).Cells(LigNe, 5).value.ToString.Replace(" ", "")) * Qte, 2).ToString)
        '                    ElseIf (IsNumeric(app.Workbooks(1).Worksheets(1).Cells(LigNe, 5).value) = False And app.Workbooks(1).Worksheets(1).Cells(LigNe, 5).value.ToString.Replace(" ", "") <> "") Then
        '                        MsgBox("Prix unitaire ligne " & LigNe.ToString & " : " & app.Workbooks(1).Worksheets(1).Cells(LigNe, 5).value.ToString & " incorrect!", MsgBoxStyle.Exclamation)
        '                    End If
        '                End If

        '                DatSet.Tables("T_DQEItem").Rows.Add(DatRow)
        '                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        '                DatAdapt.Update(DatSet, "T_DQEItem")
        '                DatSet.Clear()


        '            Else
        '                MsgBox("La ligne " & LigNe.ToString & " ne sera pas prise en compte, elle n'a ni unité ni quantité!", MsgBoxStyle.Exclamation)
        '            End If

        '        End If
        '    End If
        'Next
        'BDQUIT(sqlconn)
        'GroupControl17.Width = GroupControl13.Width - 4
        'GroupControl17.Left = 2
        'GroupControl17.BringToFront()
        'MajGridDQE()
        'app.Quit()
        'FinChargement()


    End Sub
    Private Sub PanelControl2_Click(sender As Object, e As EventArgs)
        Dim NewOpenFile As New OpenFileDialog
        NewOpenFile.Filter = "Fichier d'importation (Excel) | *.xls;*.xlsx"
        If NewOpenFile.ShowDialog() = DialogResult.OK Then
            'txtFilePathDQE.Text = NewOpenFile.FileName
        End If

    End Sub
    Private Sub RdItem_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If (RdItem.Checked = True) Then
        '    GbItemDQE.Visible = True
        'Else
        '    GbItemDQE.Visible = False
        '    TxtDesigneSection.Focus()
        'End If
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

    Private Sub CmbNumLot2_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'If (CmbNumLot2.Text <> "") Then
        '    query = "select LibelleLot,RefLot,SousLot from T_LotDAO where NumeroDAO='" & NumDoss & "' and CodeLot='" & CmbNumLot2.Text & "'"
        '    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        '    For Each rw As DataRow In dt0.Rows
        '        TxtRefLot1.Text = rw(1).ToString
        '        If (rw(2).ToString = "OUI") Then
        '            CmbSousLotDQE.Enabled = True
        '            CmbSousLot1.Enabled = True
        '        Else
        '            CmbSousLotDQE.Enabled = False
        '            CmbSousLot1.Enabled = False
        '        End If
        '    Next

        '    CmbNumSection.Text = ""
        '    TxtSection.Text = ""
        '    TxtNumItem.Text = ""

        '    If (CmbSousLot1.Enabled = False) Then
        '        GbSectionDQE.Enabled = True
        '        GbItemDQE.Enabled = True
        '    Else
        '        GbSectionDQE.Enabled = False
        '        GbItemDQE.Enabled = False
        '        LesSousLots(TxtRefLot1.Text, CmbSousLotDQE)
        '        LesSousLots(TxtRefLot1.Text, CmbSousLot1)
        '    End If
        '    MajGridDQE()

        '    CmbLotDQE.Text = CmbNumLot2.Text
        'Else
        '    GbSectionDQE.Enabled = False
        '    GbItemDQE.Enabled = False
        'End If
    End Sub

    Private Sub CmbLotDQE_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
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
    End Sub

    Private Sub CmbSousLotDQE_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'CmbSousLot1.Text = CmbSousLotDQE.Text
        'query = "select LibelleSousLot from T_LotDAO_SousLot where CodeSousLot='" & CmbSousLotDQE.Text & "' and NumeroDAO='" & NumDoss & "'"
        'Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt0.Rows
        '    TxtSousLotDQE.Text = MettreApost(rw(0).ToString)
        'Next

        'GbSectionDQE.Enabled = True
        'GbItemDQE.Enabled = True
        'btOpenDQE.Enabled = True
        MajGridDQE()

    End Sub

    Private Sub CmbSousLot1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        'If (CmbSousLot1.Text <> "") Then
        '    CmbSousLotDQE.Text = CmbSousLot1.Text

        '    GbSectionDQE.Enabled = True
        '    GbItemDQE.Enabled = True
        'End If

    End Sub

    Private Sub ChkSousSection_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        'If (ChkSousSection.Checked = True) Then
        '    GbSousSection.Visible = True
        'Else
        '    GbSousSection.Visible = False
        'End If

    End Sub

    Private Sub TxtDesigneSection_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If (e.KeyCode = Keys.Enter) Then
            BtEnrgSection_Click(Me, e)
        End If
    End Sub

    Private Sub MajGridDQE()
        Dim NumSection As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        'Dim cptr As Decimal = 0
        'DataGridView1.Rows.Clear()
        'CmbNumSection.Properties.Items.Clear()
        'query = "select RefSection,NumeroSection,Designation from T_DQESection where NumeroDAO='" & NumDoss & "' and CodeLot='" & CmbLotDQE.Text & "' and CodeSousLot='" & CmbSousLotDQE.Text & "' order by NumeroSection"
        'Dim dt As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt.Rows
        '    cptr = cptr + 1
        '    Dim RefSect As Decimal = rw(0)
        '    CmbNumSection.Properties.Items.Add(rw(1).ToString)

        '    Dim n As Decimal = DataGridView1.Rows.Add()
        '    DataGridView1.Rows.Item(n).Cells(0).Value = "S" & rw(0).ToString
        '    DataGridView1.Rows.Item(n).Cells(1).Value = "SECTION " & rw(1).ToString
        '    DataGridView1.Rows.Item(n).Cells(2).Value = MettreApost(rw(2).ToString)
        '    For i As Integer = 1 To 2
        '        DataGridView1.Rows.Item(n).Cells(i).Style.Font = New Font("Tahoma", 9, FontStyle.Bold)
        '    Next

        '    Dim NbSS As Decimal = 0

        '    query = "select Count(*) from T_DQESection_SousSection where RefSection='" & rw(0).ToString & "' and NumeroDAO='" & NumDoss & "'"
        '    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        '    For Each rw1 As DataRow In dt1.Rows
        '        NbSS = CInt(rw1(0))
        '    Next


        '    If (NbSS > 1) Then

        '        query = "select RefSousSection,NumeroSousSection,LibelleSousSection from T_DQESection_SousSection where RefSection='" & RefSect.ToString & "' order by NumeroSousSection"
        '        dt1 = ExcecuteSelectQuery(query)
        '        For Each rw1 As DataRow In dt1.Rows
        '            Dim z As Decimal = DataGridView1.Rows.Add()
        '            DataGridView1.Rows.Item(z).Cells(0).Value = "X" & rw1(0).ToString
        '            DataGridView1.Rows.Item(z).Cells(1).Value = "S/S " & rw1(1).ToString
        '            DataGridView1.Rows.Item(z).Cells(2).Value = MettreApost(rw1(2).ToString)
        '            For i As Integer = 1 To 2
        '                DataGridView1.Rows.Item(z).Cells(i).Style.Font = New Font("Tahoma", 8, FontStyle.Bold)
        '            Next

        '            query = "select RefItem,NumeroItem,Designation,UniteItem,QteItem,PuHtva,MontHtva from T_DQEItem where RefSection='" & RefSect.ToString & "' and NumeroSousSection='" & rw1(1).ToString & "' order by NumeroItem"
        '            Dim dt2 As DataTable = ExcecuteSelectQuery(query)
        '            For Each rw2 As DataRow In dt2.Rows
        '                Dim x As Decimal = DataGridView1.Rows.Add()
        '                DataGridView1.Rows.Item(x).Cells(0).Value = "I" & rw2(0).ToString
        '                DataGridView1.Rows.Item(x).Cells(1).Value = rw2(1).ToString
        '                DataGridView1.Rows.Item(x).Cells(2).Value = MettreApost(rw2(2).ToString)
        '                DataGridView1.Rows.Item(x).Cells(3).Value = rw2(3).ToString
        '                DataGridView1.Rows.Item(x).Cells(4).Value = AfficherMonnaie(rw2(4).ToString)
        '                DataGridView1.Rows.Item(x).Cells(5).Value = AfficherMonnaie(rw2(5).ToString)
        '                DataGridView1.Rows.Item(x).Cells(6).Value = AfficherMonnaie(rw2(6).ToString)
        '            Next
        '        Next

        '    Else

        '        query = "select RefItem,NumeroItem,Designation,UniteItem,QteItem,PuHtva,MontHtva from T_DQEItem where RefSection='" & RefSect.ToString & "' order by NumeroItem"
        '        dt1 = ExcecuteSelectQuery(query)
        '        For Each rw1 As DataRow In dt1.Rows
        '            Dim x As Decimal = DataGridView1.Rows.Add()
        '            DataGridView1.Rows.Item(x).Cells(0).Value = "I" & rw1(0).ToString
        '            DataGridView1.Rows.Item(x).Cells(1).Value = rw1(1).ToString
        '            DataGridView1.Rows.Item(x).Cells(2).Value = MettreApost(rw1(2).ToString)
        '            DataGridView1.Rows.Item(x).Cells(3).Value = rw1(3).ToString
        '            DataGridView1.Rows.Item(x).Cells(4).Value = AfficherMonnaie(rw1(4).ToString)
        '            DataGridView1.Rows.Item(x).Cells(5).Value = AfficherMonnaie(rw1(5).ToString)
        '            DataGridView1.Rows.Item(x).Cells(6).Value = AfficherMonnaie(rw1(6).ToString)
        '        Next

        '    End If
        'Next

        'DataGridView1.Columns(2).Width = DataGridView1.Width - (DataGridView1.Columns(0).Width + DataGridView1.Columns(1).Width + DataGridView1.Columns(3).Width + DataGridView1.Columns(4).Width + DataGridView1.Columns(5).Width + DataGridView1.Columns(6).Width)

        'TxtNumSection.Text = NumSection(cptr)

        'If (GbItemDQE.Visible = True) Then
        '    TxtDesigneItem.Focus()
        'Else
        '    TxtDesigneSection.Focus()
        'End If

        MajCmbUnite()
    End Sub

    Private Sub CmbNumSection_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ' If (CmbNumSection.Text <> "") Then
        '    Dim refSection As Decimal = 0
        '    query = "select RefSection,Designation from T_DQESection where NumeroDAO='" & NumDoss & "' and CodeLot='" & CmbNumLot2.Text & "' and NumeroSection='" & CmbNumSection.Text & "'"
        '    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        '    For Each rw As DataRow In dt0.Rows
        '        refSection = CInt(rw(0))
        '        TxtSection.Text = MettreApost(rw(1).ToString)
        '    Next

        '    Dim NbSS As Decimal = 0
        '    query = "select Count(*) from T_DQESection_SousSection where RefSection='" & refSection & "' and NumeroDAO='" & NumDoss & "'"
        '    dt0 = ExcecuteSelectQuery(query)
        '    For Each rw As DataRow In dt0.Rows
        '        NbSS = CInt(rw(0))
        '    Next

        '    If (NbSS > 1) Then
        '        CmbSousSection.Enabled = True
        '        LesSousSection(refSection.ToString)
        '    Else
        '        CmbSousSection.Enabled = False
        '        TxtDesigneItem.Focus()
        '    End If


        '    Dim nbreItem As Decimal = 0
        '    query = "select Count(*) from T_DQEItem where RefSection='" & refSection & "'"
        '    dt0 = ExcecuteSelectQuery(query)
        '    For Each rw As DataRow In dt0.Rows
        '        nbreItem = CInt(rw(0))
        '    Next

        '    Dim codeItem As String = (nbreItem + 1).ToString
        '    If (nbreItem < 10) Then codeItem = "0" & codeItem
        '    TxtNumItem.Text = CmbNumSection.Text & codeItem
        '    RefSectionItemCache.Text = refSection.ToString

        ' End If
    End Sub

    Private Sub CmbSousSection_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        'If (CmbSousSection.Text <> "") Then
        '    query = "select LibelleSousSection from T_DQESection_SousSection where NumeroDAO='" & NumDoss & "' and NumeroSousSection='" & CmbSousSection.Text & "'"
        '    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        '    For Each rw As DataRow In dt0.Rows

        '        TxtSousSection.Text = MettreApost(rw(0).ToString)
        '        TxtDesigneItem.Focus()
        '    Next
        'End If

    End Sub

    Private Sub LesSousSection(ByVal Sect As String)
        'CmbSousSection.Properties.Items.Clear()
        'query = "select NumeroSousSection from T_DQESection_SousSection where NumeroDAO='" & NumDoss & "' and RefSection='" & Sect & "'"
        'Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt0.Rows
        '    CmbSousSection.Properties.Items.Add(rw(0).ToString)
        'Next
    End Sub

    Private Sub MajCmbUnite()
        ' CmbUnite.Properties.Items.Clear()
        CmbUniteBien.Properties.Items.Clear()
        query = "select LibelleCourtUnite from T_Unite"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            ' CmbUnite.Properties.Items.Add(rw(0).ToString)
            CmbUniteBien.Properties.Items.Add(rw(0).ToString)
        Next
        'CmbUnite.Properties.Items.Add("...")
    End Sub

    Private Sub CmbUnite_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ' query = "select LibelleUnite from T_Unite where LibelleCourtUnite='" & CmbUnite.Text & "'"
        ' Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        ' For Each rw As DataRow In dt0.Rows
        'TxtUnite.Text = MettreApost(rw(0).ToString)
        ' TxtQte.Focus()
        ' Next
    End Sub

    Private Sub TxtQte_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'If (e.KeyCode = Keys.Enter And TxtQte.Text <> "") Then
        ' TxtPunit.Focus()
        'End If
    End Sub

    Private Sub TxtQte_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'If (TxtQte.Text <> "") Then
        'TxtQte.Text = AfficherMonnaie(TxtQte.Text.Replace(" ", ""))
        'End If
    End Sub

    Private Sub TxtPunit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        ' If (e.KeyCode = Keys.Enter) Then
        'If (TxtPunit.Text = "") Then
        'BtEnrgItem.Focus()
        ' Else
        BtEnrgItem_Click(Me, e)
        '  End If
        ' End If
    End Sub

    Private Sub TxtPunit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'If (TxtPunit.Text <> "") Then
        '    TxtPunit.Text = AfficherMonnaie(TxtPunit.Text.Replace(" ", ""))
        'End If
    End Sub

    Private Sub BtEnrgSection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If (TxtNumSection.Text <> "" And TxtDesigneSection.Text <> "") Then
        '    Dim DatSet = New DataSet
        '    query = "select * from T_DQESection"
        '    Dim sqlconn As New MySqlConnection
        '    BDOPEN(sqlconn)
        '    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        '    Dim DatAdapt = New MySqlDataAdapter(Cmd)
        '    DatAdapt.Fill(DatSet, "T_DQESection")
        '    Dim DatTable = DatSet.Tables("T_DQESection")
        '    Dim DatRow = DatSet.Tables("T_DQESection").NewRow()

        '    DatRow("NumeroDAO") = NumDoss
        '    DatRow("NumeroSection") = TxtNumSection.Text
        '    DatRow("Designation") = EnleverApost(TxtDesigneSection.Text)
        '    DatRow("CodeLot") = CmbNumLot2.Text
        '    If (CmbSousLot1.Text <> "") Then DatRow("CodeSousLot") = CmbSousLot1.Text

        '    DatSet.Tables("T_DQESection").Rows.Add(DatRow)
        '    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        '    DatAdapt.Update(DatSet, "T_DQESection")
        '    DatSet.Clear()

        '    If (ChkSousSection.Checked = True) Then

        '        Dim DernRef As String = ""
        '        query = "select RefSection from T_DQESection where NumeroSection='" & TxtNumSection.Text & "' and NumeroDAO='" & NumDoss & "'"
        '        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        '        For Each rw As DataRow In dt0.Rows
        '            DernRef = rw(0).ToString
        '        Next

        '        Dim NumSS() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}

        '        For k As Integer = 0 To GridSousSection.RowCount - 1

        '            If (GridSousSection.Rows(k).Cells(0).Value <> Nothing) Then

        '                If (GridSousSection.Rows(k).Cells(0).Value.ToString.Replace(" ", "") <> "") Then

        '                    DatSet = New DataSet
        '                    query = "select * from T_DQESection_SousSection"

        '                    Cmd = New MySqlCommand(query, sqlconn)
        '                    DatAdapt = New MySqlDataAdapter(Cmd)
        '                    DatAdapt.Fill(DatSet, "T_DQESection_SousSection")
        '                    DatTable = DatSet.Tables("T_DQESection_SousSection")
        '                    DatRow = DatSet.Tables("T_DQESection_SousSection").NewRow()

        '                    DatRow("RefSection") = DernRef
        '                    DatRow("NumeroDAO") = NumDoss
        '                    DatRow("NumeroSousSection") = TxtNumSection.Text & "." & NumSS(k)
        '                    DatRow("LibelleSousSection") = EnleverApost(GridSousSection.Rows(k).Cells(0).Value.ToString)

        '                    DatSet.Tables("T_DQESection_SousSection").Rows.Add(DatRow)
        '                    CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        '                    DatAdapt.Update(DatSet, "T_DQESection_SousSection")
        '                    DatSet.Clear()


        '                End If

        '            End If

        '        Next
        '    End If
        '    BDQUIT(sqlconn)

        '    TxtDesigneSection.Text = ""

        MajGridDQE()

        ' End If
    End Sub

    Private Sub BtEnrgItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If (CmbNumSection.Text <> "" And TxtDesigneItem.Text <> "" And CmbUnite.Text <> "" And TxtQte.Text <> "") Then
        '    Dim DatSet = New DataSet
        '    query = "select * from T_DQEItem"
        '    Dim sqlconn As New MySqlConnection
        '    BDOPEN(sqlconn)
        '    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        '    Dim DatAdapt = New MySqlDataAdapter(Cmd)
        '    DatAdapt.Fill(DatSet, "T_DQEItem")
        '    Dim DatTable = DatSet.Tables("T_DQEItem")
        '    Dim DatRow = DatSet.Tables("T_DQEItem").NewRow()

        '    DatRow("RefSection") = RefSectionItemCache.Text
        '    DatRow("NumeroItem") = TxtNumItem.Text
        '    DatRow("Designation") = EnleverApost(TxtDesigneItem.Text)
        '    DatRow("UniteItem") = CmbUnite.Text
        '    DatRow("QteItem") = TxtQte.Text.Replace(" ", "")
        '    If (TxtPunit.Text <> "") Then
        '        DatRow("PuHtva") = AfficherMonnaie(TxtPunit.Text.Replace(" ", ""))
        '        DatRow("PuHtvaLettre") = MontantLettre(TxtPunit.Text.Replace(" ", ""))
        '        DatRow("MontHtva") = AfficherMonnaie(Math.Round(CDec(TxtQte.Text.Replace(" ", "")) * CDec(TxtPunit.Text.Replace(" ", "")), 2).ToString)
        '    End If
        '    If (CmbSousSection.Enabled = True) Then DatRow("NumeroSousSection") = CmbSousSection.Text

        '    DatSet.Tables("T_DQEItem").Rows.Add(DatRow)
        '    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        '    DatAdapt.Update(DatSet, "T_DQEItem")
        '    DatSet.Clear()


        '    TxtDesigneItem.Text = ""
        '    CmbUnite.Text = ""
        '    TxtUnite.Text = ""
        '    TxtQte.Text = ""
        '    TxtPunit.Text = ""
        '    CmbNumSection_SelectedValueChanged(Me, e)
        '    MajGridDQE()

        'BDQUIT(sqlconn)
        'End If
    End Sub



#End Region

#Region "Post Qualification"

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
                Exit Sub
            End If
            If TxtCriterePost.IsRequiredControl("Veuillez saisir un critère") Then
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
                query &= "VALUES(NULL,'" & NumDoss & "','" & ListePostQualif.Nodes(i).Item("Description").ToString().EnleverApostrophe() & "','NON','0')"
                ExecuteNonQuery(query)
                RefGroupe = ExecuteScallar("SELECT MAX(RefCritere) FROM t_dao_postqualif WHERE NumeroDAO='" & NumDoss & "'")
                ListePostQualif.Nodes(i).SetValue("IdCol", RefGroupe)
                For j = 0 To ListePostQualif.Nodes(i).Nodes.Count - 1
                    If ListePostQualif.Nodes(i).Nodes(j).Item("IdCol").ToString = "##" Then
                        query = "INSERT INTO T_DAO_PostQualif(RefCritere,NumeroDAO,LibelleCritere,CritereElimine,RefCritereMere) "
                        query &= "VALUES(NULL,'" & NumDoss & "','" & ListePostQualif.Nodes(i).Nodes(j).GetValue("Description").ToString().Replace("      ", "").EnleverApostrophe & "',"
                        query &= "'" & ListePostQualif.Nodes(i).Nodes(j).GetValue("Eliminatoire").ToString() & "','" & RefGroupe & "')"
                        ExecuteNonQuery(query)
                        Dim Id As String = ExecuteScallar("SELECT MAX(RefCritere) FROM t_dao_postqualif WHERE NumeroDAO='" & NumDoss & "'")
                        ListePostQualif.Nodes(i).Nodes(j).SetValue("IdCol", Id)
                    End If
                Next
            Else
                query = "UPDATE T_DAO_PostQualif SET LibelleCritere='" & ListePostQualif.Nodes(i).GetValue("Description").ToString().EnleverApostrophe & "' "
                query &= "WHERE RefCritere='" & ListePostQualif.Nodes(i).GetValue("IdCol") & "'"
                ExecuteNonQuery(query)
                For j = 0 To ListePostQualif.Nodes(i).Nodes.Count - 1
                    If ListePostQualif.Nodes(i).Nodes(j).Item("IdCol").ToString = "##" Then
                        query = "INSERT INTO T_DAO_PostQualif(RefCritere,NumeroDAO,LibelleCritere,CritereElimine,RefCritereMere) "
                        query &= "VALUES(NULL,'" & NumDoss & "','" & ListePostQualif.Nodes(i).Nodes(j).GetValue("Description").ToString().Replace("      ", "").EnleverApostrophe & "',"
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
    Private Sub LoadPageDQE(ByVal NumDossier As String)
        InitDQE()
        'If Not PageDQE.PageEnabled Then PageDQE.PageEnabled = True
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

        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Function SavePageDQE(ByVal NumDossier As String) As Boolean

        Return True
    End Function
#End Region

    Private Sub NewDao_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub NewDao_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        LoadTypeMarche()
        PageDonneBase.PageEnabled = False
    End Sub

    Private Sub cmbTypeMarche_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTypeMarche.SelectedIndexChanged
        LoadMarches(cmbTypeMarche.Text)
        If cmbTypeMarche.Text.Contains("Fourniture") Then
            ' PageDQE.PageVisible = False
            PageSpecTech.PageVisible = True
        ElseIf cmbTypeMarche.Text.Contains("Travaux") Then
            ' PageDQE.PageVisible = True
            PageSpecTech.PageVisible = False
        End If
    End Sub

    Private Sub cmbMarches_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMarches.SelectedIndexChanged
        If cmbMarches.SelectedIndex = -1 Then
            TxtMethodeMarche.ResetText()
            CurrentMarche = Nothing
            rdAttrLot.Enabled = False
            rdAttrSousLot.Enabled = False
        Else
            query = "SELECT * FROM t_marche WHERE RefMarche='" & RefMarche(cmbMarches.SelectedIndex) & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                CurrentMarche = dt.Rows(0)
                TxtMethodeMarche.Text = GetMethode(CurrentMarche("CodeProcAO"))
            Else
                CurrentMarche = Nothing
                TxtMethodeMarche.ResetText()
            End If
            rdAttrLot.Enabled = True
            rdAttrSousLot.Enabled = True
        End If

    End Sub

    Private Sub ContextMenuStripSousLotDB_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStripSousLotDB.Opening
        Try
            If GridSousLot.CurrentRow.Cells(0).Value = "" Then
                e.Cancel = True
            End If
        Catch ex As Exception
            e.Cancel = True
        End Try
    End Sub

    Private Sub DeleteSousLot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteSousLot.Click
        GridSousLot.Rows.Remove(GridSousLot.CurrentRow)
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
            If DejaDansLaBD Then
                If Not TabAMettreAJour(LstTabName.IndexOf(e.Page.Name)) Then
                    TabAMettreAJour(LstTabName.IndexOf(e.Page.Name)) = True
                    If PourModif Or PourAjout Then
                        LoadPage(e.Page.Name, NumDoss)
                    End If
                End If
            End If
        End If
    End Sub
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
    Private Sub BtEnregistrer_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtEnregistrer.ItemClick
        If Not DejaDansLaBD Then 'On est dans le cas où il faut enregister les données de base
            'Vérification des champs
            If TxtNumDao.IsRequiredControl("Veuillez saisir le numéro du DAO") Then
                Exit Sub
            End If
            If TxtLibelleDao.IsRequiredControl("Veuillez saisir le libellé du DAO") Then
                Exit Sub
            End If
            If cmbMarches.IsRequiredControl("Veuillez sélectionner un marché") Then
                Exit Sub
            End If
            If DateDepot.IsRequiredControl("Veuillez indiquer la date de fin de dépôt") Then
                Exit Sub
            End If
            If HeureDepot.IsRequiredControl("Veuillez indiquer l'heure de fin de dépôt") Then
                Exit Sub
            End If
            If DateOuverture.IsRequiredControl("Veuillez indiquer la date d'ouverture") Then
                Exit Sub
            End If
            If HeureOuverture.IsRequiredControl("Veuillez indiquer l'heure d'ouverture") Then
                Exit Sub
            End If
            If DatePublication.IsRequiredControl("Veuillez indiquer la date de publication") Then
                Exit Sub
            End If
            If NomJournal.IsRequiredControl("Veuillez indiquer le nom du journal de publication") Then
                Exit Sub
            End If
            If Val(TxtPrixDao.Text) <> 0 Then
                If CmbCompte.IsRequiredControl("Veuillez sélectionner le compte bancaire pour les frais de dossier") Then
                    Exit Sub
                End If
            End If

            If TxtNbreLot.Value > ViewLots.RowCount Then
                FailMsg("Veuillez enregistrer tous les lots")
                Exit Sub
            End If

            If IsNothing(CurrentMarche) Then
                FailMsg("Nous n'avons pas pu récupérer le marché")
                Exit Sub
            End If
            NumDoss = EnleverApost(TxtNumDao.Text)
            query = "SELECT COUNT(*) FROM t_dao WHERE NumeroDAO='" & NumDoss & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                FailMsg("Ce numéro existe déjà")
                Exit Sub
            End If
            'dans la table AMI
            query = "select count(*) from t_ami where NumeroDAMI='" & NumDoss & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                FailMsg("Ce numéro existe déjà")
                Exit Sub
            End If
            'Dans la table DP
            query = "select count(*) from t_dp where NumeroDp='" & NumDoss & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                FailMsg("Ce numéro existe déjà")
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

            query = "INSERT INTO t_dao (NumeroDAO, IntituleDAO, RefMarche, MontantMarche, TypeMarche, MethodePDM, NbreLotDAO, DateSaisie, DateModif, Operateur, PrixDAO, CompteAchat, CodeProjet, DateOuverture, DateLimiteRemise, CodeConvention, Attribution, DateEdition, DatePublication, JournalPublication) VALUES"
            query &= "('" & NumDoss & "','" & EnleverApost(TxtLibelleDao.Text) & "','" & CurrentMarche("RefMarche") & "','" & CurrentMarche("MontantEstimatif") & "','" & CurrentMarche("TypeMarche") & "','" & GetMethode(CurrentMarche("CodeProcAO")) & "','" & TxtNbreLot.Value & "','" & dateconvert(Now) & "','" & dateconvert(Now) & "','" & CodeOperateurEnCours & "','" & Val(TxtPrixDao.Text) & "','" & CompteVenteDAO & "','" & ProjetEnCours & "','" & dateconvert(DateOuverture.DateTime.ToShortDateString) & " " & HeureOuverture.Text & "','" & dateconvert(DateDepot.DateTime.ToShortDateString()) & " " & HeureDepot.Text & "','" & CurrentMarche("CodeConvention") & "','" & AttributionMarche & "','" & dateconvert(Now) & "','" & dateconvert(DatePublication.DateTime.ToShortDateString) & "','" & NomJournal.Text & "')"
            Try
                ExecuteNonQuery(query)
                query = "UPDATE t_marche SET NumeroDAO='" & NumDoss & "' WHERE RefMarche='" & CurrentMarche("RefMarche") & "'"
                ExecuteNonQuery(query)
                For i = 0 To (ViewLots.RowCount - 1)
                    query = "INSERT INTO t_lotdao(RefLot,NumeroDAO,CodeLot,LibelleLot,MontantGarantie,DelaiDeGarantie,DateSaisie,DateModif,Operateur) "
                    query &= "VALUES(NULL,'" & NumDoss & "','" & ViewLots.GetRowCellValue(i, "N°") & "','" & EnleverApost(ViewLots.GetRowCellValue(i, "Libellé")) & "','" & Val(ViewLots.GetRowCellValue(i, "Caution")) & "','" & ViewLots.GetRowCellValue(i, "Garantie") & "','" & dateconvert(Now) & "','" & dateconvert(Now) & "','" & CodeUtilisateur & "')"
                    ExecuteNonQuery(query)
                    Dim LastNumLot As Decimal
                    query = "SELECT MAX(RefLot) FROM t_lotdao WHERE Operateur='" & CodeUtilisateur & "'"
                    LastNumLot = Val(ExecuteScallar(query))
                    Dim NewSousLotsId As String = String.Empty
                    Dim NewSousLots As String = String.Empty
                    Dim SousLots As String() = ViewLots.GetRowCellValue(i, "SousLotsValues").ToString().Split(";")
                    Dim cpteSousLot As Integer = 1
                    For Each item As String In SousLots
                        If item <> String.Empty Then
                            Dim NumSousLot As String = ViewLots.GetRowCellValue(i, "N°") & "." & cpteSousLot
                            query = "INSERT INTO t_lotdao_souslot(RefSousLot,RefLot,NumeroDAO,CodeSousLot,LibelleSousLot) "
                            query &= "VALUES(NULL,'" & LastNumLot & "','" & NumDoss & "','" & NumSousLot & "','" & EnleverApost(item) & "')"
                            ExecuteNonQuery(query)
                            cpteSousLot += 1
                            NewSousLotsId &= NumSousLot & ","
                        End If
                    Next
                    If NewSousLotsId.Length > 1 Then
                        NewSousLotsId = Mid(NewSousLotsId, 1, (NewSousLotsId.Length - 1))
                    End If
                    'Modification de l'IdLot généré auto par le Id de la BD
                    ViewLots.SetRowCellValue(i, "IdLot", LastNumLot)
                    ViewLots.SetRowCellValue(i, "SousLotsId", NewSousLotsId)
                Next
            Catch ex As Exception
                FailMsg("Impossible d'enregistrer ce DAO" & vbNewLine & "Contactez votre fournisseur" & vbNewLine & ex.ToString)
            End Try
            DejaDansLaBD = True
            TypeMarche = CurrentMarche("TypeMarche")
            MethodMarche = GetMethode(CurrentMarche("CodeProcAO"))
            LoadArchivesDao()
            VisibleOtherTabs(True)
        Else
            'Le DAO est déjà dans la BD, il faut enregistrer toutes les tabs modifées
            DebutChargement(True, "Enregistrement du dossier en cours")

            For i = 0 To XtraTabControl1.TabPages.Count - 1
                If TabAMettreAJour(i) Then 'On doit mettre à jour les données de cette tab
                    Dim CurrentTab As DevExpress.XtraTab.XtraTabPage = XtraTabControl1.TabPages(i)
                    Select Case CurrentTab.Name
                        Case "PageDonneBase"
                            If SavePageDonneBase(NumDoss) Then
                                Exit Select
                            Else
                                Exit Sub
                            End If
                        Case "PageDonnePartic"
                            If SavePageDonnePartic(NumDoss) Then
                                Exit Select
                            Else
                                Exit Sub
                            End If
                        Case "PageDQE"
                            If SavePageDQE(NumDoss) Then
                                Exit Select
                            Else
                                Exit Sub
                            End If
                        Case "PageConformTechnique"
                            If SavePageConformTechnique(NumDoss) Then
                                Exit Select
                            Else
                                Exit Sub
                            End If
                        Case "PageSpecTech"
                            If SavePageSpecTech(NumDoss) Then
                                Exit Select
                            Else
                                Exit Sub
                            End If
                        Case "PagePostQualif"
                            If SavePagePostQualif(NumDoss) Then
                                Exit Select
                            Else
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
        End If
    End Sub
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
    Private Sub ModifierLeDossier_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ModifierLeDossier.Click
        If PourAjout Or PourModif Then
            SuccesMsg("Veuillez enregistrer le dossier en cours")
            Exit Sub
        End If

        If (PourAjout = False And PourModif = False) Then
            If LayoutView1.RowCount > 0 Then
                Dim drx = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
                NumDoss = drx("N°")
                TypeMarche = drx("Type")
                MethodMarche = drx("Méthode")
                query = "SELECT DossValider FROM t_dao WHERE NumeroDAO='" & NumDoss & "'"
                Dim test As Boolean = ExecuteScallar(query)
                If test = True Then
                    SuccesMsg("Impossible de modifier le dossier car il a dejà été valider")
                    Exit Sub
                End If
                TxtNumDao.Text = NumDoss
                'ChkNumDaoAuto.Enabled = False
                cmbTypeMarche.Text = TypeMarche
                TxtMethodeMarche.Text = MethodMarche

                PourModif = True
                DejaDansLaBD = True
                LoadPageDonneBase(NumDoss)
                VisibleOtherTabs(True)
                If Not BtFermerDAO.Enabled Then BtFermerDAO.Enabled = True
                If Not BtEnregistrer.Enabled Then BtEnregistrer.Enabled = True
                'AjoutInfoDao()
            End If

        Else
            FailMsg("Impossible de charger le dossier.")
        End If
    End Sub

    Private Sub AfficherLeDossier_Click(sender As Object, e As EventArgs) Handles AfficherLeDossier.Click
        If LayoutView1.RowCount > 0 Then
            Dim drx = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
            Dim NewApercuDAO As New ApercuDAO
            NewApercuDAO.NumDoss = drx("N°")
            Disposer_form(NewApercuDAO)
        End If
    End Sub

    Private Sub SupprimerLeDossier_Click(sender As Object, e As EventArgs) Handles SupprimerLeDossier.Click
        If PourAjout Or PourModif Then
            SuccesMsg("Veuillez enregistrer et fermer le dossier en cours")
            Exit Sub
        End If
        If (PourAjout = False And PourModif = False) Then
            If ConfirmMsg("Voulez-vous supprimer ce dossier ?") = DialogResult.Yes Then
                Dim drx = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
                NumDoss = drx("N°")
                query = "SELECT DossValider FROM t_dao WHERE NumeroDAO='" & NumDoss & "'"
                Dim test As Boolean = ExecuteScallar(query)
                If test = True Then
                    SuccesMsg("Impossible de supprimer le dossier car il a dejà été validé")
                    Exit Sub
                End If
                'Suppression des données post qualification
                query = "DELETE FROM T_DAO_PostQualif WHERE NumeroDAO='" & NumDoss & "'"
                ExecuteNonQuery(query)
                'Suppresion des données de spécification technique
                query = "DELETE FROM t_spectechcaract WHERE RefSpecFournit IN(SELECT RefSpecFournit FROM t_spectechfourniture WHERE NumeroDAO='" & NumDoss & "')"
                ExecuteNonQuery(query)
                query = "DELETE FROM T_SpecTechFourniture WHERE NumeroDAO='" & NumDoss & "'"
                ExecuteNonQuery(query)
                'Supression des données dans membre de la comission
                query = "DELETE FROM t_commission WHERE NumeroDAO='" & NumDoss & "'"
                ExecuteNonQuery(query)
                'Supprimer les sous-lots
                query = "DELETE FROM t_lotdao_souslot WHERE NumeroDAO='" & NumDoss & "'"
                ExecuteNonQuery(query)
                'Supprimer les lots
                query = "DELETE FROM t_lotdao WHERE NumeroDAO='" & NumDoss & "'"
                ExecuteNonQuery(query)
                'Recuperation de reference marche
                query = "SELECT RefMarche FROM t_dao WHERE NumeroDAO='" & NumDoss & "'"
                Dim RefMarche = Val(ExecuteScallar(query))
                'Suppression du DAO
                query = "DELETE FROM t_dao WHERE NumeroDAO='" & NumDoss & "'"
                ExecuteNonQuery(query)
                'Mise à jour qu marché
                query = "UPDATE t_marche SET NumeroDAO=NULL WHERE RefMarche='" & RefMarche & "'"
                ExecuteNonQuery(query)
                SuccesMsg("Suppression du dosssier effectué")
                LoadArchivesDao()
            End If
        Else
            FailMsg("Impossible de supprimer le dossier.")
        End If
    End Sub

    Private Sub ValiderLeDossierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValiderLeDossierToolStripMenuItem.Click
        If PourAjout Or PourModif Then
            SuccesMsg("Veuillez enregister et fermer le dossier en cours")
            Exit Sub
        End If
        If (PourAjout = False And PourModif = False) Then
            If ConfirmMsg("Voulez-vous valider ce dossier ?") = DialogResult.Yes Then
                Dim drx = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
                NumDoss = drx("N°")
                DebutChargement()
                query = "SELECT * FROM t_commission WHERE NumeroDAO='" & NumDoss & "' AND TypeComm='COJO'"
                Dim dt = ExcecuteSelectQuery(query)
                For Each rw In dt.Rows
                    Dim Authkey = GenererToken(NumDoss, rw("CodeMem").ToString, "DAO", DB)
                    Dim ID() = Authkey.Split(":")
                    Dim token = ID(0).ToString
                    query = "UPDATE t_commission SET AuthKey='" & token & "' WHERE CodeMem='" & rw("CodeMem").ToString & "'"
                    ExecuteNonQuery(query)
                    envoieMail(rw("NomMem").ToString, rw("EmailMem").ToString, Authkey)
                    query = "INSERT INTO t_dao_evalcojo(NumeroDAO,id_cojo) VALUES('" & NumDoss & "','" & rw("CodeMem").ToString & "')"
                    ExecuteNonQuery(query)
                Next
                query = "UPDATE t_dao SET DossValider=TRUE WHERE NumeroDAO='" & NumDoss & "'"
                ExecuteNonQuery(query)
                FinChargement()
                SuccesMsg("Dossier validé avec succès")
            End If
        Else
            FailMsg("Impossible de valider le dossier.")
        End If
    End Sub
    Private Sub PermttreLaModificationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PermttreLaModificationToolStripMenuItem.Click
        query = "UPDATE t_dao SET DossValider=FALSE WHERE NumeroDAO='" & NumDoss & "'"
        ExecuteNonQuery(query)

        SuccesMsg("C'est possible de modifier le dossier")
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

    Private Sub ToolStripMenuModifierSpecTech_Click(sender As Object, e As EventArgs) Handles ToolStripMenuModifierSpecTech.Click
        If ListeSpecTech.Nodes.Count > 0 Then
            modifSpecTech = True
            NodeModSpec = ListeSpecTech.FocusedNode
            Dim node1 As TreeListNode = ListeSpecTech.FocusedNode.ParentNode
            If Not NodeModSpec.ParentNode Is Nothing Then
                UnlockModCaract()
                Dim cat = Split(node1.GetValue("CodeCateg").ToString, "-")(1).ToString
                If cat = "Cat" Then
                    query = "SELECT LibelleCat FROM t_predfournitures_groupe WHERE IdCat='" & Split(node1.GetValue("CodeCateg").ToString, "-")(0).ToString & "'"
                Else
                    query = "SELECT LibelleSousCat FROM t_predfournitures_sous_groupe WHERE IdSousCat='" & Split(node1.GetValue("CodeCateg").ToString, "-")(0).ToString & "'"
                End If
                TxtLibCategBien.Text = MettreApost(ExecuteScallar(query))
                Dim Quantite() = Split(node1.GetValue("Quantite").ToString, " ")
                TxtCodeBien.Text = node1.GetValue("Code").ToString.Trim()
                TxtLibelleBien.Text = node1.GetValue("Libelle").ToString.Trim()
                NumQteBien.Value = Quantite(0).ToString
                CmbUniteBien.Text = Quantite(1).ToString
                TxtLieuLivraison.Text = node1.GetValue("LieuLivre").ToString
                Dim libelle() = NodeModSpec.GetValue("Libelle").ToString.Trim().Split(":")
                TxtLibelleCaract.Text = libelle(0).ToString.Split("-")(1).ToString
                TxtValeurCaract.Text = libelle(1).ToString.Trim()
                txtCodeCateg.Text = node1.GetValue("CodeCateg").ToString
            Else
                modifSpecTech = True
                NodeModSpec = ListeSpecTech.FocusedNode
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
                TxtCodeBien.Text = ListeSpecTech.FocusedNode.GetValue("Code").ToString.Trim()
                TxtLibelleBien.Text = ListeSpecTech.FocusedNode.GetValue("Libelle").ToString.Trim()
                NumQteBien.Value = Quantite(0).ToString
                CmbUniteBien.Text = Quantite(1).ToString
                TxtLieuLivraison.Text = ListeSpecTech.FocusedNode.GetValue("LieuLivre").ToString
                txtCodeCateg.Text = ListeSpecTech.FocusedNode.GetValue("CodeCateg").ToString
                UnlockModBien()
            End If
        End If
    End Sub

    Private Sub ToolStripMenuSupprimerSpecTech_Click(sender As Object, e As EventArgs) Handles ToolStripMenuSupprimerSpecTech.Click
        If ListeSpecTech.Nodes.Count > 0 Then
            Dim node As TreeListNode = ListeSpecTech.FocusedNode
            Dim node1 As TreeListNode = ListeSpecTech.FocusedNode.ParentNode
            If Not node.ParentNode Is Nothing Then
                CodeSpecTechSup.Add(node.GetValue("Identifiant").ToString & "-" & node.GetValue("Code").ToString)
                For i = 0 To SaveDonnee.Nodes.Count - 1
                    If SaveDonnee.Nodes(i).GetValue("IdentifiantSav") = node.ParentNode.GetValue("Identifiant") And SaveDonnee.Nodes(i).GetValue("CodeSav") = node.ParentNode.GetValue("Code") And SaveDonnee.Nodes(i).GetValue("LibelleSav") = node.ParentNode.GetValue("Libelle") And SaveDonnee.Nodes(i).GetValue("NumLotSav") = node.ParentNode.GetValue("NumLot") And SaveDonnee.Nodes(i).GetValue("NumSousLotSav") = node.ParentNode.GetValue("NumSousLot") Then
                        For j = 0 To SaveDonnee.Nodes(i).Nodes.Count - 1
                            If SaveDonnee.Nodes(i).Nodes(j).GetValue("IdentifiantSav") = node.GetValue("Identifiant") And SaveDonnee.Nodes(i).Nodes(j).GetValue("LibelleSav") = node.GetValue("Libelle") Then
                                SaveDonnee.Nodes(i).Nodes(j).ParentNode.Nodes.Remove(SaveDonnee.Nodes(i).Nodes(j))
                                Exit For
                            End If
                        Next
                        Exit For
                    End If
                Next
                node.ParentNode.Nodes.Remove(node)

                If Not node1.HasChildren Then
                    CodeSpecTechSup.Add(node1.GetValue("Identifiant").ToString & "-" & node1.GetValue("Code").ToString)
                    For i = 0 To SaveDonnee.Nodes.Count - 1
                        If SaveDonnee.Nodes(i).GetValue("IdentifiantSav") = node1.GetValue("Identifiant") And SaveDonnee.Nodes(i).GetValue("CodeSav") = node1.GetValue("Code") And SaveDonnee.Nodes(i).GetValue("LibelleSav") = node1.GetValue("Libelle") And SaveDonnee.Nodes(i).GetValue("NumLotSav") = node1.GetValue("NumLot") And SaveDonnee.Nodes(i).GetValue("NumSousLotSav") = node1.GetValue("NumSousLot") Then
                            SaveDonnee.Nodes.Remove(SaveDonnee.Nodes(i))
                            Exit For
                        End If
                    Next
                    ListeSpecTech.DeleteNode(node1)
                End If
            Else
                If ConfirmMsg("Voulez-vous supprimer cette catégorie avec ses caractéristiques ?") = DialogResult.Yes Then
                    CodeSpecTechSup.Add(node.GetValue("Identifiant").ToString & "-" & node.GetValue("Code").ToString)
                    For i = 0 To ListeSpecTech.Nodes.Count - 1
                        If ListeSpecTech.Nodes(i).GetValue("Identifiant").ToString = node.GetValue("Identifiant").ToString Then
                            For j = 0 To ListeSpecTech.Nodes(i).Nodes.Count - 1
                                CodeSpecTechSup.Add(ListeSpecTech.Nodes(i).Nodes(j).GetValue("Identifiant").ToString & "-" & node.GetValue("Code").ToString)
                            Next
                            Exit For
                        End If
                    Next
                    For i = 0 To SaveDonnee.Nodes.Count - 1
                        If SaveDonnee.Nodes(i).GetValue("IdentifiantSav") = node.GetValue("Identifiant") And SaveDonnee.Nodes(i).GetValue("CodeSav") = node.GetValue("Code") And SaveDonnee.Nodes(i).GetValue("LibelleSav") = node.GetValue("Libelle") And SaveDonnee.Nodes(i).GetValue("NumLotSav") = node.GetValue("NumLot") And SaveDonnee.Nodes(i).GetValue("NumSousLotSav") = node.GetValue("NumSousLot") Then
                            SaveDonnee.Nodes.Remove(SaveDonnee.Nodes(i))
                            Exit For
                        End If
                    Next
                    ListeSpecTech.Nodes.Remove(node)
                End If
            End If
        End If

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
    Private Sub BtAnnulPostQualif_Click(sender As Object, e As EventArgs) Handles BtAnnulPostQualif.Click
        initChampPostQualif()
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs)
        'If GridPostQualif.Visible = False Then
        '    GridPostQualif.Visible = True
        '    ListePostQualif.Visible = False
        'Else
        '    GridPostQualif.Visible = False
        '    ListePostQualif.Visible = True
        'End If
        'For i = 0 To ListePostQualif.Nodes.Count - 1
        '    MsgBox(ListePostQualif.Nodes(i).GetValue("IdCol").ToString())
        'Next
        For i = 0 To CodePostQualifSup.Count - 1
            MsgBox(CodePostQualifSup.Item(i))
        Next
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs)
        If GridSpecifTech.Visible = False Then
            GridSpecifTech.Visible = True
            ListeSpecTech.Visible = False
            SaveDonnee.Visible = False
        Else
            GridSpecifTech.Visible = False
            ListeSpecTech.Visible = True
            SaveDonnee.Visible = False
        End If
        'For i = 0 To CodeSpecTechSup.Count - 1
        '    MsgBox(CodeSpecTechSup.Item(i))
        'Next
    End Sub

    Private Sub btRetourBien_Click(sender As Object, e As EventArgs) Handles btRetourBien.Click
        ViderSaisieBien()
        cmbLotSpecTech.Enabled = True
        'CmbSousLotSpecTech.Enabled = True
        BtCategBien.Enabled = True
        modifSpecTech = False
        TxtLibCategBien.Text = ""
        txtCodeCateg.Text = ""
        LockSaisieBien()
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs)
        GridSpecifTech.Visible = False
        ListeSpecTech.Visible = False
        SaveDonnee.Visible = True
    End Sub

    Private Sub ReporterLouvertureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReporterLouvertureToolStripMenuItem.Click
        ReportDate.ShowDialog()
    End Sub

    Private Sub ImprimerLeDossier_Click(sender As Object, e As EventArgs) Handles ImprimerLeDossier.Click
        Dim drx = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
        Dim NumDossier = drx("N°")
        ApercuDAO.ImpressionDAO(NumDossier)
    End Sub

    Private Sub GroupControl4_Paint(sender As Object, e As PaintEventArgs) Handles GroupControl4.Paint

    End Sub
End Class
