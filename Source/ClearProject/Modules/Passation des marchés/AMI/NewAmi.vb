Imports System.Globalization
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Layout
Imports DevExpress.XtraGrid.Views.Card
Imports MySql.Data.MySqlClient
Imports System.IO
Imports Microsoft.Office.Interop
Imports DevExpress.XtraSplashScreen
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions
Imports System.Math
Imports DevExpress.XtraRichEdit
Imports CrystalDecisions.Shared
Imports ClearProject.PassationMarche
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.IO.Compression
Imports GemBox.Document

Public Class NewAmi

    Dim TabRefMarche As String()
    'Pour la modification de l'AMI
    Dim RefMarcheModif As Integer
    Dim NumAMIEnCours As String = ""
    Dim CheminPublDoc As String = ""

    Dim dr As DataRow
    Dim dt = New DataTable()

    '-1 etat initial
    ' 0 bouton nouvo clické
    ' 1 enregistrer dans la BD
    ' 2 en cours d'afficharge
    Dim ActionTous As Integer = -1
    Dim ModifTous As Boolean = False
    Dim AffichDoss As Boolean = False

    ' Dim ActualiserPub As Boolean = False


    Dim TypeModif As String = ""
    Dim NumDoss As String = ""
    Dim Index As Integer = -1

    Dim typeMarc As String = ""
    Dim methodMarc As String = ""
    Dim montMarc As Decimal = 0
    Dim nomEtAdrConsImpression As String = ""
    Dim nomConsImprim As String = ""
    Dim ValSal As String = ""

    Private IV() As Byte = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}

    Private Sub NewAmi_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RibbonDP.Minimized = True

        'Charger les ami
        ArchivesAMI()
        'Charger marche de type consultant
        ChargerMarcher()
        'initialiser()
        LoadColumOuverturcomi()
        LoadColunCritere()
        cmbMarches.ResetText()
    End Sub

    Private Sub ChargerMarcher()

        'query = "Select RefMarche, DescriptionMarche, MontantEstimatif, InitialeBailleur, CodeConvention from T_Marche where CodeProjet='" & ProjetEnCours & "' AND TypeMarche LIKE 'Consultants%' AND NumeroDAO IS NULL order by TypeMarche ASC"

        query = "Select RefMarche, DescriptionMarche, MontantEstimatif, Convention_ChefFile from T_Marche where CodeProjet='" & ProjetEnCours & "' AND TypeMarche LIKE 'Consultants%' and NumeroMarche IS NULL"
        Dim dt As DataTable = ExcecuteSelectQuery(query)

        Dim Taille As Integer = 0
        cmbMarches.Properties.Items.Clear()
        ' cmbMarches.ResetText()

        Dim MontantMarcheRestant As Decimal = 0
        For Each rw As DataRow In dt.Rows
            'Montant marche restant (à utiliser)
            MontantMarcheRestant = CDec(rw("MontantEstimatif").ToString.Replace(" ", "")) - NewVerifierMontMarche(rw("RefMarche")) 'Montant consomé
            If MontantMarcheRestant > 0 Then
                ReDim Preserve TabRefMarche(Taille)
                TabRefMarche(Taille) = rw("RefMarche")
                Taille += 1
                cmbMarches.Properties.Items.Add(MettreApost(rw("DescriptionMarche").ToString) & " | " & MontantMarcheRestant & " | " & GetInitialbailleur(rw("Convention_ChefFile").ToString) & "(" & rw("Convention_ChefFile").ToString & ")")
            End If
        Next
    End Sub

    Private Function NewVerifieMontantMarche() As Boolean
        Dim tabl As String() = cmbMarches.Text.Split("|")
        If CDec(MontantMarche.Text.Replace(" ", "")) > CDec(tabl(1).ToString.Replace(" ", "")) Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub LoadColunCritere()
        Dim dts = New DataTable
        dts.Columns.Add("N°", Type.GetType("System.String"))
        dts.Columns.Add("Libellé critère", Type.GetType("System.String"))
        dts.Columns.Add("Valeurs", Type.GetType("System.String"))
        dts.Columns.Add("Refcritere", Type.GetType("System.String"))
        dts.Columns.Add("Note", Type.GetType("System.String"))
        dts.Columns.Add("LigneModif", Type.GetType("System.String"))
        LgCritere.DataSource = dts

        ViewCritere.OptionsView.ColumnAutoWidth = True
        ViewCritere.Columns("N°").Width = 7
        ViewCritere.Columns("Note").Width = 7
        ViewCritere.Columns("Refcritere").Visible = False
        ViewCritere.Columns("LigneModif").Visible = False
        ViewCritere.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ViewCritere.Columns("N°").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewCritere.Columns("Note").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    End Sub
    Private Sub LoadColumOuverturcomi()
        Dim dt = New DataTable()

        dt.Columns.Add("CodeMembre", Type.GetType("System.String"))
        dt.Columns.Add("Civilité", Type.GetType("System.String"))
        dt.Columns.Add("Nom membre", Type.GetType("System.String"))
        dt.Columns.Add("Titre", Type.GetType("System.String"))
        dt.Columns.Add("Fonction", Type.GetType("System.String"))
        dt.Columns.Add("Téléphone", Type.GetType("System.String"))
        dt.Columns.Add("E-mail", Type.GetType("System.String"))
        dt.Columns.Add("Type commission", Type.GetType("System.String"))
        dt.Columns.Add("LigneModif", Type.GetType("System.String"))
        GridAmi.DataSource = dt

        Viewami.OptionsView.ColumnAutoWidth = True
        Viewami.Columns("CodeMembre").Visible = False
        Viewami.Columns("Type commission").Visible = False
        Viewami.Columns("LigneModif").Visible = False
        Viewami.Columns("Civilité").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Viewami.Columns("Téléphone").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Viewami.Columns("Type commission").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Viewami.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
    End Sub

    Private Sub initialiser()
        TxtNumDp.Text = ""
        TxtCojo.Text = ""
        TxtContactCojo.Text = ""
        TxtFonctionCojo.Text = ""
        TxtMailCojo.Text = ""
        TxtLibDp.Text = ""
        cmbMarches.Text = ""
        CmbCivCojo.Text = ""
        CmbTitreCojo.Text = ""
        Datedepot.Text = ""
        DateOuverture.Text = ""
        HeureDepot.EditValue = Nothing
        HeureOuverture.EditValue = Nothing
        MethodeMarche.Text = ""
        BtAjoutCojo.Enabled = True
        NoteMinimaleAMI.EditValue = Nothing
        DatePub1.EditValue = Nothing
        LibellePub.Text = ""
        DocTDR.Text = ""
        CheminPublDoc = ""

        ' CheminPublPdf = ""
        ' ActualiserPub = False
        ' BtSaisiAnonoce.Text = "Saisisser l'annonce"
        ' WebBrowser1.Navigate("")

        DatePub2.EditValue = Nothing
        NbreDelaiPub.Text = ""
        JoursDelaiPub.Text = ""
        DateReporte.EditValue = Nothing
        HeureReporte.EditValue = Nothing
        MontantMarche.ResetText()
        AffichDoss = False
    End Sub
    Private Sub InitialiserMemebredelaCommission()
        CmbCivCojo.Text = ""
        TxtCojo.Text = ""
        TxtContactCojo.Text = ""
        TxtFonctionCojo.Text = ""
        TxtMailCojo.Text = ""
        CmbTitreCojo.Text = ""
    End Sub

    Private Sub NewAmi_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub InitialiseBouton(ByVal Value As Boolean)
        AjouterCriter.Enabled = Value
        BtAjoutCojo.Enabled = Value
        BtEnregistrer.Enabled = Value
        ' BtImportAnnonce.Enabled = Value
        ' BtSaisiAnonoce.Enabled = Value
        'BtActualiserAnnonce.Enabled = Value
    End Sub

    Private Sub BtNouveau_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtNouveau.ItemClick
        'If (ActionTous = -1 And PourModif = False And DejaDansLaBD = False) Then
        If (ActionTous = -1) Then
            ActionTous = 0
            PageDonneesBase.PageEnabled = True
            InitialiseBouton(True)
            If Not TxtNumDp.Enabled Then TxtNumDp.Enabled = True
            cmbMarches.Properties.ReadOnly = False
            cmbMarches.ResetText()
            Datedepot.Text = ""
            DateOuverture.Text = ""
        ElseIf (ActionTous = 0) Then
            SuccesMsg("Veuillez enregistrer le dossier en cours.")
        ElseIf ActionTous = 1 Or ActionTous = 2 Then
            SuccesMsg("Veuillez fermer le dossier en cours.")
        End If
    End Sub

    Private Sub BtRetour_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtRetour.ItemClick
        DebutChargement()
        initialiser()
        Dim dts As DataTable = GridAmi.DataSource
        dts.Rows.Clear()
        Dim dtts As DataTable = LgCritere.DataSource
        dtts.Rows.Clear()
        PageDonneesBase.PageEnabled = False
        PageTDR.PageEnabled = False
        ActionTous = -1
        TxtNumDp.Enabled = False
        NumAMIEnCours = ""
        ModifTous = False

        ' affichDossier = False
        ' PourAjout = False
        ' PourModif = False
        'DejaDansLaBD = False
        TypeModif = ""
        cmbMarches.Properties.ReadOnly = False
        cmbMarches.ResetText()
        MontantMarche.ResetText()
        GetEnabledBouton(True)
        FinChargement()
    End Sub

    '#Region "Données de base"

    Private Sub ArchivesAMI()
        dt.Columns.Clear()

        dt.Columns.Add("N°", Type.GetType("System.String"))
        dt.Columns.Add("Edité le", Type.GetType("System.String"))
        dt.Columns.Add("Méthode", Type.GetType("System.String"))
        dt.Columns.Add("Liste", Type.GetType("System.String"))
        dt.Columns.Add("Ouverture", Type.GetType("System.String"))
        dt.Columns.Add("Date", Type.GetType("System.String"))
        dt.Columns.Add("Mission", Type.GetType("System.String"))
        dt.Columns.Add("NoteMinimaleAMI", Type.GetType("System.String"))
        dt.Columns.Add("DateLimitePropo", Type.GetType("System.String"))
        dt.Columns.Add("ValiderEditionAmi", Type.GetType("System.String"))
        dt.Columns.Add("Statut", Type.GetType("System.String"))

        query = "select * from T_AMI where CodeProjet='" & ProjetEnCours & "' ORDER BY DateEdition DESC"
        dt.Rows.Clear()
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            Dim dr1 = dt.NewRow()

            dr1("N°") = MettreApost(rw("NumeroDAMI").ToString)
            dr1("Edité le") = CDate(rw("DateEdition")).ToShortDateString
            dr1("Méthode") = rw("MethodeSelection").ToString
            dr1("Liste") = rw("ListeRestreinte").ToString & " Consultants"
            dr1("NoteMinimaleAMI") = rw("NoteMinimaleAMI")
            dr1("DateLimitePropo") = IIf(rw("DateReporte").ToString <> "", rw("DateReporte").ToString, rw("DateLimitePropo").ToString).ToString
            dr1("ValiderEditionAmi") = rw("ValiderEditionAmi").ToString
            dr1("Statut") = rw("StatutDoss").ToString

            If (rw("DateOuvertureEffective").ToString <> "") Then
                dr1("Ouverture") = "Effectuée"
                dr1("Date") = CDate(rw("DateOuvertureEffective")).ToShortDateString & " à " & CDate(rw("DateOuvertureEffective")).ToLongTimeString 'Replace(":", " h ") & " mn"
            Else
                If (rw("DateOuverture").ToString <> "") Then
                    dr1("Ouverture") = "Non effectuée"
                    dr1("Date") = CDate(rw("DateOuverture")).ToShortDateString & " à " & CDate(rw("DateOuverture")).ToLongTimeString 'Replace(":", " h ") & " mn"
                Else
                    dr1("Ouverture") = "Non Prévue"
                    dr1("Date") = "__/__/____"
                End If
            End If

            dr1("Mission") = MettreApost(rw("LibelleMiss").ToString)
            dt.Rows.Add(dr1)
        Next

        GridArchAMI.DataSource = dt
        LayoutViewAMI.Columns("NoteMinimaleAMI").Visible = False
        LayoutViewAMI.Columns("DateLimitePropo").Visible = False
        LayoutViewAMI.Columns("ValiderEditionAmi").Visible = False
    End Sub

#Region "CODE NON UTILIER"
    Private Sub BtAppercu_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtAppercu.ItemClick
        'If (TxtNumDp.Text <> "" And NumDoss <> "") Then
        '    query = "Select Count(*) from T_AMI where NumeroDAMI='" & TxtNumDp.Text & "' and CodeProjet='" & ProjetEnCours & "'"
        '    Dim nbEnrg As Decimal = 0
        '    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        '    For Each rw As DataRow In dt0.Rows
        '        nbEnrg = CInt(rw(0))
        '    Next

        '    If (nbEnrg <= 0) Then
        '        SuccesMsg("Dossier inexistant!")
        '        Exit Sub
        '    End If

        '    ReponseDialog = NumDoss
        '    SelectConsultant.ShowDialog()
        '    If (ReponseDialog = "") Then
        '        Exit Sub
        '    Else
        '        nomEtAdrConsImpression = ReponseDialog
        '        nomConsImprim = ExceptRevue
        '        ExceptRevue = ""
        '        ReponseDialog = ""
        '    End If
        '    BtEnregistrer.Enabled = True
        'Else
        '    SuccesMsg("Aucun enregistrement!")
        'End If
    End Sub
    Private Sub VerifRapporteur()
        If (TxtNumDp.Text <> "") Then
            Dim RapporteurExist As Boolean = False
            query = "select * from T_Commission where NumeroDAO='" & TxtNumDp.Text & "' and TitreMem='Rapporteur'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                RapporteurExist = True
            Else
                RapporteurExist = False
            End If

            If (RapporteurExist = False) Then
                LblControl.Text = "* Rapporteur pas encore enregistré !"
            Else
                LblControl.Text = ""
            End If
        End If
    End Sub

    Private Sub CritèreToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CritèreToolStripMenuItem.Click
        ReponseDialog = NumDoss
        AjoutCritereConsult.ShowDialog()
    End Sub

    Private Sub SousCritèreToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SousCritèreToolStripMenuItem.Click
        ReponseDialog = NumDoss
        AjoutSousCritereConsult.ShowDialog()
    End Sub

    Private Sub EnregistrerPJ(ByVal NomPJ As String)
        Dim NomDossier As String = line & "\DP\" & typeMarc & "\" & methodMarc & "\" & NumDoss.Replace("/", "_")
        If (Directory.Exists(NomDossier) = True) Then
            Dim partNomPj() As String = NomPJ.Split("\"c)
            Dim NomCourtPJ As String = ""
            For Each part As String In partNomPj
                NomCourtPJ = part
            Next
            File.Copy(NomPJ, NomDossier & "\" & NomCourtPJ, True)
        End If
    End Sub

    Private Sub AjoutInfoDp()
        If (NumDoss <> "") Then
            '   If (PourModif = True) Then
            'BtRetour.Enabled = True
            '    GridArchAMI.Enabled = False
            'ChkNumDpAuto.Checked = False
            'ChkNumDpAuto.Enabled = False
            'TxtLibDp.Enabled = False
            'ChkLibDpAuto.Enabled = False
            ' GridMarcheDp.Enabled = True
            'Datedepot.Enabled = True
            '    HeureDepot.Enabled = True
            '    DateOuverture.Enabled = True
            '    HeureOuverture.Enabled = True
            ' OuvrirGroupPartic()
            'NumPoidsTech.Enabled = True
            'TxtScoreMinimum.Enabled = True
            'End If

            'MajGridMarche()
            'MajGridCojo()
            'MajGridEvaluation()

            query = "select LibelleMiss,TypeRemune,ConfPrea,DelaiEclaircissement,DebutMiss,DureeMiss,RessPersonnel,FormationIntrinsq,ImpotRembourse,PropoTech,PropoFin,PoidsTech,PoidsFin,ScoreTechMin,LangueDp,MonnaieEval,ValiditePropo,ModalitePropo,DateLimitePropo,AssoListeRest,MeOuvrageDelegue,DateOuverture from T_AMI where NumeroDAMI='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows

                If (rw(0).ToString <> "") Then TxtLibDp.Text = MettreApost(rw(0).ToString)
                If (rw(18).ToString <> "") Then
                    Dim partDepot() As String = rw(18).ToString.Split(" "c)
                    Datedepot.DateTime = CDate(partDepot(0)).ToShortDateString
                    HeureDepot.Time = CDate(partDepot(1)).ToLongTimeString
                Else
                    Datedepot.DateTime = "01/01/2000"
                    HeureDepot.Time = "00:00:00"
                End If
                If (rw(21).ToString <> "") Then
                    Dim partOuvert() As String = rw(21).ToString.Split(" "c)
                    DateOuverture.DateTime = CDate(partOuvert(0)).ToShortDateString
                    HeureOuverture.Time = CDate(partOuvert(1)).ToLongTimeString
                Else
                    DateOuverture.DateTime = "01/01/2000"
                    HeureOuverture.Time = "00:00:00"
                End If

                'If (rw(11).ToString <> "") Then
                '    NumPoidsTech.Value = CDec(rw(11))
                'End If
                'If (rw(13).ToString <> "") Then
                '    TxtScoreMinimum.Text = rw(13).ToString
            Next

            Dim NomDossier As String = line & "\DP\" & typeMarc & "\" & methodMarc & "\" & NumDoss.Replace("/", "_") & "\TDR.docx"
            'MsgBox("Type:" & typeMarc & " Methode:" & methodMarc, MsgBoxStyle.Information)
            If (File.Exists(NomDossier) = True) Then
                'MsgBox("Le TDR existe!", MsgBoxStyle.Information)   'Vérif du TDR
                Dim nfStream As FileStream
                nfStream = New FileStream(NomDossier, FileMode.Open)
                DocTDR.LoadDocument(nfStream, DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
            End If

        End If
    End Sub
#End Region

    Private Function VerifierTraitementMethode(CodeMethod As String) As Boolean
        Try
            Dim ListeMethode As New List(Of String) From {"SFQC", "SCBD", "SMC", "3CV", "SFQ", "SQC"}
            For i = 0 To ListeMethode.Count - 1
                If ListeMethode(i) = CodeMethod.ToString.ToUpper Then
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Function

    Private Sub BtEnregistrer_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtEnregistrer.ItemClick
        If TxtNumDp.Text.Trim = "" Then
            SuccesMsg("Veuillez saisir le numéro de l'AMI")
            TxtNumDp.Select()
            Exit Sub
        End If

        If VerifierTraitementMethode(MethodeMarche.Text) = False Then
            FailMsg("Aucun traitement prévu pour la méthode [" & MethodeMarche.Text & "]")
            Exit Sub
        End If

        If TxtLibDp.IsRequiredControl("Veuillez saisir la description de l'AMI") Then
            TxtLibDp.Select()
            Exit Sub
        End If

        If DatePub1.IsRequiredControl("Veuillez saisir la date de publication") Then
            DatePub1.Focus()
            Exit Sub
        End If

        If Val(NbreDelaiPub.Text) = 0 Or NbreDelaiPub.Text.Trim = "" Then
            SuccesMsg("Veuillez saisir le delai de publication")
            NbreDelaiPub.Focus()
            Exit Sub
        End If

        If JoursDelaiPub.Text.Trim = "" Then
            SuccesMsg("Veuillez saisir le delai de publication")
            JoursDelaiPub.Focus()
            Exit Sub
        End If

        If HeureDepot.IsRequiredControl("Veuillez saisir l'heure de depot") Then
            HeureDepot.Select()
            Exit Sub
        End If

        If cmbMarches.Text.Trim = "" Then
            SuccesMsg("Veuillez selectionner un marché")
            cmbMarches.Focus()
            Exit Sub
        End If

        'If DateDepot.IsRequiredControl("Veuillez saisir la date de depot") Then
        'If HeureDepot.IsRequiredControl("Veuillez saisir l'heure de depot") Then
        '    HeureDepot.Select()
        '    Exit Sub
        'End If

        If DateOuverture.IsRequiredControl("Veuillez saisir la date d'ouverture") Then
            DateOuverture.Select()
            Exit Sub
        End If
        If HeureOuverture.IsRequiredControl("Veuillez saisir l'heure d'ouverture") Then
            HeureOuverture.Select()
            Exit Sub
        End If

        If LibellePub.IsRequiredControl("Veuillez saisir le moyen de publication") Then
            LibellePub.Focus()
            Exit Sub
        End If

        If ViewCritere.RowCount = 0 Then
            SuccesMsg("Veuillez ajouter un critère d'évaluation")
            Exit Sub
        End If

        If MontantMarche.Text.Trim = "" Or Val(MontantMarche.Text) = 0 Then
            SuccesMsg("Veuillez saisir le montant du marche")
            MontantMarche.Focus()
            Exit Sub
        End If

        'Verification du montant du marche saisie
        If NewVerifieMontantMarche() = True Then
            SuccesMsg("Le montant du marché saisie est suppérieur au montant du marché prévu")
            MontantMarche.Focus()
            Exit Sub
        End If

        If NoteMinimaleAMI.Text.Trim = "" Or Val(NoteMinimaleAMI.Text) = 0 Then
            SuccesMsg("Veuillez saisir la note minumale")
            NoteMinimaleAMI.Focus()
            Exit Sub
        End If

        If (DateReporte.Text.Trim = "" And HeureReporte.Text.Trim <> "") Or (DateReporte.Text.Trim <> "" And HeureReporte.Text.Trim = "") Then
            SuccesMsg("Veuillez bien definir la date de report")
            DateReporte.Focus()
            Exit Sub
        End If

        If DatePub1.Text.Trim <> "" And DatePub2.Text.Trim <> "" Then
            If DateTime.Compare(CDate(DatePub1.Text), CDate(DatePub2.Text)) > 0 Then
                SuccesMsg("La prémière date de publication ne doit être " & vbNewLine & "suppérieure à la deuxième date de publication")
                Exit Sub
            End If
        End If


        'If Datedepot.Text.Trim <> "" And HeureDepot.Text.Trim <> "" And DateReporte.Text.Trim <> "" And HeureReporte.Text.Trim <> "" Then
        '    Dim datedepo = Datedepot.Text '& " " & HeureDepot.Text
        '    Dim datereport = DateReporte.Text '& " " & HeureReporte.Text
        '    If DateTime.Compare(CDate(datedepo), CDate(datereport)) > 0 Then
        '        SuccesMsg("La date de limite de dépot doit être inférieur à la date de reporte")
        '        Exit Sub
        '    End If
        'End If

        If Viewami.RowCount = 0 Then
            SuccesMsg("Veuillez ajouter un membre de la commission d'ouverture")
            Exit Sub
        End If

        'Nouveau dossier
        If ActionTous = 0 Then

            'verification de l'existence du numero du dossier
            'dans la table AMI
            query = "select count(NumeroDAMI) from t_ami where NumeroDAMI='" & EnleverApost(TxtNumDp.Text) & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                SuccesMsg("Le numéro de l'AMI existe déjà")
                TxtNumDp.Select()
                Exit Sub
            End If

            'Dans la table DP
            query = "select count(NumeroDp) from t_dp where NumeroDp='" & EnleverApost(TxtNumDp.Text) & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                SuccesMsg("Le numéro de l'AMI existe déjà")
                TxtNumDp.Select()
                Exit Sub
            End If

            'dans la table DAO
            query = "select count(NumeroDAO) from t_dao where NumeroDAO='" & EnleverApost(TxtNumDp.Text) & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                SuccesMsg("Le numéro de l'AMI existe déjà")
                TxtNumDp.Select()
                Exit Sub
            End If

            DebutChargement(True, "Enregistrements des données en cours...")

            NumAMIEnCours = EnleverApost(TxtNumDp.Text)

            Dim DatSet = New DataSet

            query = "select * from T_AMI"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_AMI")
            Dim DatTable = DatSet.Tables("T_AMI")
            Dim DatRow = DatSet.Tables("T_AMI").NewRow()

            DatRow("NumeroDAMI") = NumAMIEnCours
            DatRow("RefMarche") = TabRefMarche(cmbMarches.SelectedIndex)
            Dim CodeCoven As String = cmbMarches.Text.Split("|")(2)
            DatRow("CodeConvention") = CodeCoven.ToString.Split("(")(1).Replace(")", "").Replace("(", "")

            DatRow("DateEdition") = dateconvert(Now.ToShortDateString)
            DatRow("DateModif") = dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString

            DatRow("DateSaisie") = dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString
            DatRow("Operateur") = CodeUtilisateur
            DatRow("CodeProjet") = ProjetEnCours

            DatRow("LibelleMiss") = EnleverApost(TxtLibDp.Text)
            DatRow("MethodeSelection") = MethodeMarche.Text
            'DatRow("CheminDocAMI") = CheminDocAMI.ToString

            DatRow("MontantMarche") = MontantMarche.Text.Replace(" ", "")
            If DateReporte.Text <> "" And HeureReporte.Text <> "" Then DatRow("DateReporte") = dateconvert(DateReporte.Text) & " " & HeureReporte.Text
            If DatePub1.Text <> "" Then DatRow("DatePub") = dateconvert(DatePub1.Text)
            If DatePub2.Text <> "" Then DatRow("DatePub2") = dateconvert(DatePub2.Text)
            DatRow("MoyenPublication") = EnleverApost(LibellePub.Text)
            DatRow("DateOuverture") = dateconvert(DateOuverture.Text) & " " & HeureOuverture.Text
            DatRow("DateLimitePropo") = CDate(Datedepot.DateTime).ToShortDateString & " " & CDate(HeureDepot.Time).ToLongTimeString
            DatRow("DelaiPub") = NbreDelaiPub.Text & " " & JoursDelaiPub.Text
            DatRow("NoteMinimaleAMI") = NoteMinimaleAMI.Text.Replace(".", ",")

            ' DatRow("ValidationMoyenne") = "En cours"
            DatRow("ValiderEditionAmi") = "En cours"
            DatRow("StatutDoss") = "En cours"


            DatSet.Tables("T_AMI").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_AMI")
            BDQUIT(sqlconn)
            DatSet.Clear()

            For i = 0 To ViewCritere.RowCount - 1
                query = "insert into t_critereami values(NULL,'" & NumAMIEnCours & "', '" & EnleverApost(ViewCritere.GetRowCellValue(i, "Libellé critère").ToString) & "', '" & EnleverApost(ViewCritere.GetRowCellValue(i, "Valeurs").ToString) & "','" & ViewCritere.GetRowCellValue(i, "Note").ToString.Split(" ")(0).Replace(",", ".").Replace(" ", "") & "')"
                ExecuteNonQuery(query)
            Next

            For i = 0 To Viewami.RowCount - 1
                query = "insert into T_Commission values(NULL,'" & EnleverApost(Viewami.GetRowCellValue(i, "Nom membre").ToString) & "', '" & EnleverApost(Viewami.GetRowCellValue(i, "Téléphone").ToString) & "', '" & EnleverApost(Viewami.GetRowCellValue(i, "E-mail").ToString) & "', '" & EnleverApost(Viewami.GetRowCellValue(i, "Fonction").ToString) & "', '',  '" & EnleverApost(Viewami.GetRowCellValue(i, "Titre").ToString) & "', '" & NumAMIEnCours & "', '" & EnleverApost(Viewami.GetRowCellValue(i, "Type commission").ToString) & "', '" & Viewami.GetRowCellValue(i, "Civilité").ToString & "', '" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', '" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', '" & CodeUtilisateur & "', '', '','', '', '','')"
                ExecuteNonQuery(query)
            Next

            ' ExecuteNonQuery("Update t_marche set NumeroDAO='" & NumAMIEnCours & "' Where RefMarche='" & RefMarche(cmbMarches.SelectedIndex) & "' and CodeProjet='" & ProjetEnCours & "'")
            cmbMarches.Properties.ReadOnly = True

            ArchivesAMI()
            ChargementdesDonnees(NumAMIEnCours)
            FinChargement()
            SuccesMsg("Enregistrement effectué avec succès")
            ChargerMarcher()
            PageTDR.PageEnabled = True
            TxtNumDp.Enabled = False

            ' XtraTabControl1.SelectedTabPage = "PageTDR"
            'deja enregistrer
            ActionTous = 1
            CheminPublDoc = ""
            ' CheminPublPdf = ""
            'En cours de modification
        ElseIf ActionTous = 1 Then
            'mise a jour
            Dim TrouverModif As Boolean = False
            DebutChargement(True, "Enregistrements des modifications...")

            'Dim CodeCoven As String = cmbMarches.Text.Split("|")(2)
            'query = "update t_ami set RefMarche ='" & RefMarche(cmbMarches.SelectedIndex) & "', LibelleMiss ='" & EnleverApost(TxtLibDp.Text) & "', MontantMarche ='" & cmbMarches.Text.Split("|")(1) & "',  CodeConvention ='" & CodeCoven.ToString.Split("(")(1).Replace(")", "") & "', DateOuverture ='" & dateconvert(DateOuverture.Text & " " & HeureOuverture.Text) & "', DateModif ='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', Operateur ='" & CodeUtilisateur & "', MethodeSelection = '" & MethodeMarche.Text & "', DateLimitePropo = '" & dateconvert(DateDepot.Text & " " & HeureDepot.Text) & "', NoteMinimaleAMI='" & NoteMinimaleAMI.Text & "', MoyenPublication= '" & EnleverApost(LibellePub.Text) & "', DatePub='" & dateconvert(DatePub.Text) & "' where NumeroDAMI='" & NumAMIEnCours & "' and CodeProjet='" & ProjetEnCours & "'"

            query = "update t_ami set LibelleMiss ='" & EnleverApost(TxtLibDp.Text) & "', DateOuverture ='" & dateconvert(DateOuverture.Text) & " " & HeureOuverture.Text & "', DateModif ='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', Operateur ='" & CodeUtilisateur & "', DateLimitePropo ='" & dateconvert(Datedepot.Text) & " " & CDate(HeureDepot.Time).ToLongTimeString & "', NoteMinimaleAMI='" & NoteMinimaleAMI.Text.Replace(".", ",") & "', MoyenPublication='" & EnleverApost(LibellePub.Text) & "', DatePub='" & dateconvert(DatePub1.Text) & "', DatePub2 ='" & IIf(DatePub2.Text.Trim <> "", dateconvert(DatePub2.Text), "").ToString & "', MontantMarche ='" & MontantMarche.Text.Replace(" ", "") & "', DelaiPub='" & NbreDelaiPub.Text & " " & JoursDelaiPub.Text & "', DateReporte='" & IIf(DateReporte.Text <> "" And HeureReporte.Text <> "", dateconvert(DateReporte.Text) & " " & HeureReporte.Text, "").ToString & "' where NumeroDAMI='" & NumAMIEnCours & "' and CodeProjet='" & ProjetEnCours & "'"
            ExecuteNonQuery(query)

            For i = 0 To ViewCritere.RowCount - 1
                If ViewCritere.GetRowCellValue(i, "Refcritere").ToString = "" Then
                    query = "insert into t_critereami values(NULL,'" & NumAMIEnCours & "', '" & EnleverApost(ViewCritere.GetRowCellValue(i, "Libellé critère").ToString) & "', '" & EnleverApost(ViewCritere.GetRowCellValue(i, "Valeurs").ToString) & "','" & ViewCritere.GetRowCellValue(i, "Note").ToString.Split(" ")(0).Replace(",", ".").Replace(" ", "") & "')"
                    ExecuteNonQuery(query)
                    TrouverModif = True
                ElseIf ViewCritere.GetRowCellValue(i, "LigneModif").ToString = "Modifier" Then
                    query = "Update t_critereami set Libellecritere='" & EnleverApost(ViewCritere.GetRowCellValue(i, "Libellé critère").ToString) & "', Valeurcritere='" & EnleverApost(ViewCritere.GetRowCellValue(i, "Valeurs").ToString) & "', Note='" & ViewCritere.GetRowCellValue(i, "Note").ToString.Split(" ")(0).Replace(",", ".").Replace(" ", "") & "' where Refcritere = '" & ViewCritere.GetRowCellValue(i, "Refcritere") & "'"
                    ExecuteNonQuery(query)
                    TrouverModif = True
                End If
            Next

            For i = 0 To Viewami.RowCount - 1
                If Viewami.GetRowCellValue(i, "CodeMembre").ToString = "" Then

                    query = "insert into T_Commission values(NULL,'" & EnleverApost(Viewami.GetRowCellValue(i, "Nom membre").ToString) & "', '" & EnleverApost(Viewami.GetRowCellValue(i, "Téléphone").ToString) & "', '" & EnleverApost(Viewami.GetRowCellValue(i, "E-mail").ToString) & "', '" & EnleverApost(Viewami.GetRowCellValue(i, "Fonction").ToString) & "', '',  '" & EnleverApost(Viewami.GetRowCellValue(i, "Titre").ToString) & "', '" & NumAMIEnCours & "', '" & EnleverApost(Viewami.GetRowCellValue(i, "Type commission").ToString) & "', '" & Viewami.GetRowCellValue(i, "Civilité").ToString & "', '" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', '" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', '" & CodeUtilisateur & "', '', '','','', '','')"
                    ExecuteNonQuery(query)

                    TrouverModif = True
                ElseIf Viewami.GetRowCellValue(i, "LigneModif").ToString = "Modifier" Then
                    query = "update T_Commission set NomMem='" & EnleverApost(Viewami.GetRowCellValue(i, "Nom membre").ToString) & "', TelMem='" & EnleverApost(Viewami.GetRowCellValue(i, "Téléphone").ToString) & "', EmailMem='" & EnleverApost(Viewami.GetRowCellValue(i, "E-mail").ToString) & "', FoncMem='" & EnleverApost(Viewami.GetRowCellValue(i, "Fonction").ToString) & "', TitreMem='" & EnleverApost(Viewami.GetRowCellValue(i, "Titre").ToString) & "', TypeComm='" & EnleverApost(Viewami.GetRowCellValue(i, "Type commission").ToString) & "', Civil='" & Viewami.GetRowCellValue(i, "Civilité").ToString & "', DateModif='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', Operateur='" & CodeUtilisateur & "' where CodeMem='" & Viewami.GetRowCellValue(i, "CodeMembre").ToString & "'"
                    ExecuteNonQuery(query)
                    TrouverModif = True
                End If
            Next
            If TrouverModif = True Then
                ArchivesAMI()
                ChargementdesDonnees(NumAMIEnCours)
            End If
            FinChargement()
            If TrouverModif = True Then
                SuccesMsg("Modification effectuer avec succès")
            End If
        End If
    End Sub


    Private Sub ContextMenuStrip4_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip4.Opening
        If LayoutViewAMI.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub AfficherLeDossier_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AfficherLeDossier.Click
        'If PourModif = True Or PourAjout = True Or DejaDansLaBD = True Then
        '    SuccesMsg("Veuillez fermer le dossier en cours")
        '    Exit Sub
        'End If

        If LayoutViewAMI.RowCount > 0 Then
            AffichDoss = True
            ModifierLeDossier.PerformClick()
            If AffichDoss = False Then
                Exit Sub
            End If
            AffichDoss = False
            InitialiseBouton(False)
            ActionTous = 2
        End If
    End Sub

    Private Sub ModifierLeDossier_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ModifierLeDossier.Click

        ' If pourmodif = True Or PourAjout = True Or DejaDansLaBD = True Then

        If (ActionTous = 0) Then
            SuccesMsg("Veuillez enregistrer le dossier en cours.")
            AffichDoss = False
            Exit Sub
        ElseIf ActionTous = 1 Or ActionTous = 2 Then
            SuccesMsg("Veuillez fermer le dossier en cours.")
            AffichDoss = False
            Exit Sub
        End If

        If LayoutViewAMI.RowCount > 0 Then
            dr = LayoutViewAMI.GetDataRow(LayoutViewAMI.FocusedRowHandle)

            If AffichDoss = False Then
                If dr("Statut").ToString = "Annulé" Then
                    FailMsg("Impossible de modifier un dossier annulé")
                    Exit Sub
                End If
                If DateTime.Compare(CDate(dr("DateLimitePropo").ToString), Now) < 0 And dr("ValiderEditionAmi").ToString = "Valider" Then
                    SuccesMsg("Impossible de modifier ce dossier")
                    Exit Sub
                End If
            End If

            If AffichDoss = False And dr("ValiderEditionAmi").ToString = "Valider" Then 'Dossier valider, Modification impossible 
                GetEnabledBouton(False)
            End If

            DebutChargement(True, "Chargement des données en cours...")
            PageDonneesBase.PageEnabled = True
            PageTDR.PageEnabled = True
            BtRetour.Enabled = True
            TxtNumDp.Enabled = False
            ActionTous = 1

            'DejaDansLaBD = True
            ' PourModif = True
            InitialiseBouton(True)

            query = "select * from t_ami where NumeroDAMI='" & EnleverApost(dr("N°").ToString) & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)

            For Each rw In dt1.Rows
                RefMarcheModif = rw("RefMarche")
                TxtNumDp.Text = dr("N°").ToString
                NumAMIEnCours = dr("N°").ToString
                TxtLibDp.Text = MettreApost(rw("LibelleMiss").ToString)
                NoteMinimaleAMI.Text = rw("NoteMinimaleAMI").ToString.Replace(".", ",")
                ' Dim partOuverture() As String = rw("DateOuverture").ToString.Split(" "c)
                ' Dim partDepot() As String = rw("DateLimitePropo").ToString.Split(" "c)
                Datedepot.DateTime = CDate(rw("DateLimitePropo").ToString).ToShortDateString
                HeureDepot.Time = CDate(rw("DateLimitePropo").ToString).ToLongTimeString
                DateOuverture.DateTime = CDate(rw("DateOuverture").ToString).ToShortDateString
                HeureOuverture.Time = CDate(rw("DateOuverture").ToString).ToLongTimeString
                LibellePub.Text = MettreApost(rw("MoyenPublication").ToString)
                DatePub1.DateTime = CDate(rw("DatePub").ToString).ToShortDateString
                MontantMarche.Text = AfficherMonnaie(rw("MontantMarche").ToString)
                MethodeMarche.Text = rw("MethodeSelection").ToString

                If rw("DatePub2").ToString <> "" Then DatePub2.Text = CDate(rw("DatePub2").ToString).ToShortDateString
                If rw("DateReporte").ToString <> "" Then
                    DateReporte.DateTime = CDate(rw("DateReporte")).ToShortDateString
                    HeureReporte.Time = CDate(rw("DateReporte")).ToLongTimeString
                End If

                If rw("DelaiPub").ToString <> "" Then
                    NbreDelaiPub.EditValue = Val(rw("DelaiPub").ToString.Split(" "c)(0))
                    JoursDelaiPub.Text = rw("DelaiPub").ToString.Split(" "c)(1)
                End If

                If rw("CheminPubAMI").ToString <> "" Then
                    CheminPublDoc = rw("CheminPubAMI").ToString
                    ' Dim NewChem As String() = .Split("|")
                    ' Dim NewChem As String() = rw("CheminPubAMI").ToString.Split("|")
                    'If NewChem(1).ToString <> "" Then
                    '    CheminPublPdf = line & "\AMI\" & FormatFileName(TxtNumDp.Text, "_") & "\" & NewChem(1).ToString
                    'End If
                    '  BtSaisiAnonoce.Text = "Modifier l'annonce"
                End If
            Next

            query = "Select DescriptionMarche, MontantEstimatif, Convention_ChefFile  from T_Marche where CodeProjet='" & ProjetEnCours & "' AND RefMarche='" & RefMarcheModif & "'"
            Dim dts As DataTable = ExcecuteSelectQuery(query)

            For Each rw In dts.Rows
                cmbMarches.Text = MettreApost(rw("DescriptionMarche")) & " | " & AfficherMonnaie(rw("MontantEstimatif").ToString.Replace(" ", "")) & " | " & GetInitialbailleur(rw("Convention_ChefFile")) & "(" & rw("Convention_ChefFile") & ")"
            Next

            ChargementdesDonnees(NumAMIEnCours)

            'If CheminPublDoc.ToString <> "" Then
            '    DocTDR.LoadDocument(CheminPublDoc, DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
            'End If
            cmbMarches.Properties.ReadOnly = True
            XtraTabControl1.SelectedTabPage = PageDonneesBase
            FinChargement()
        End If
    End Sub

    Private Sub GetEnabledBouton(value As Boolean)
        DatePub1.Enabled = value
        HeureDepot.Enabled = value
        NbreDelaiPub.Enabled = value
        JoursDelaiPub.Enabled = value
    End Sub

    Private Sub ChargementdesDonnees(ByVal DossierAMI As String)

        'chargement des critere des d'evaluations
        query = "select * from t_critereami where NumeroDp='" & DossierAMI & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        Dim Newcritere As DataTable = LgCritere.DataSource
        Newcritere.Rows.Clear()
        Dim nbrs As Integer = 0
        For Each rw In dt.Rows
            Dim DrS = Newcritere.NewRow
            nbrs += 1
            DrS("N°") = nbrs
            DrS("Refcritere") = rw("Refcritere")
            DrS("Libellé critère") = MettreApost(rw("Libellecritere").ToString)
            DrS("Valeurs") = MettreApost(rw("Valeurcritere").ToString)
            DrS("Note") = rw("Note").ToString & " Points"
            DrS("LigneModif") = "Enregistrer"
            Newcritere.Rows.Add(DrS)
        Next

        query = "select * from t_commission where NumeroDAO='" & DossierAMI & "'"
        Dim dt2 As DataTable = ExcecuteSelectQuery(query)
        Dim NewMem As DataTable = GridAmi.DataSource
        NewMem.Rows.Clear()

        For Each rw In dt2.Rows
            Dim DrS = NewMem.NewRow

            DrS("CodeMembre") = rw("CodeMem")
            DrS("Civilité") = rw("Civil").ToString
            DrS("Nom membre") = MettreApost(rw("NomMem").ToString)
            DrS("Titre") = MettreApost(rw("TitreMem").ToString)
            DrS("Fonction") = MettreApost(rw("FoncMem").ToString)
            DrS("Téléphone") = MettreApost(rw("TelMem").ToString)
            DrS("E-mail") = MettreApost(rw("EmailMem").ToString)
            DrS("Type commission") = rw("TypeComm").ToString
            DrS("LigneModif") = "Enregistrer"
            NewMem.Rows.Add(DrS)
        Next
    End Sub


    Private Sub SupprimerLeDossier_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SupprimerLeDossier.Click
        If LayoutViewAMI.RowCount > 0 Then

            If (ActionTous = 0) Then
                SuccesMsg("Veuillez enregistrer le dossier en cours.")
                Exit Sub
            ElseIf ActionTous = 1 Or ActionTous = 2 Then
                SuccesMsg("Veuillez fermer le dossier en cours.")
                Exit Sub
            End If

            dr = LayoutViewAMI.GetDataRow(LayoutViewAMI.FocusedRowHandle)

            If dr("ValiderEditionAmi").ToString = "Valider" Then
                SuccesMsg("Impossible de supprimer ce dossier")
                Exit Sub
            End If

            If ConfirmMsg("Voulez-vous supprimer ce dossier ?") = DialogResult.Yes Then
                DebutChargement()

                ' Dim RefMacheres As String = ExecuteScallar("Select RefMarche from t_ami Where NumeroDAMI='" & EnleverApost(dr("N°").ToString) & "'")

                ExecuteNonQuery("delete from t_ami where NumeroDAMI='" & EnleverApost(dr("N°").ToString) & "' and CodeProjet='" & ProjetEnCours & "'")
                ExecuteNonQuery("delete from t_commission where NumeroDAO='" & EnleverApost(dr("N°").ToString) & "'")
                ExecuteNonQuery("delete from t_critereami where NumeroDp='" & EnleverApost(dr("N°").ToString) & "'")
                ExecuteNonQuery("delete from t_consultant where NumeroDp='" & EnleverApost(dr("N°").ToString) & "'")

                'ExecuteNonQuery("Update t_marche set NumeroDAO=NULL where RefMarche='" & RefMacheres & "'")

                FinChargement()
                SuccesMsg("Suppression effectuée avec succès")
                ChargerMarcher()
                ArchivesAMI()
            End If
        End If
    End Sub

    Private Sub ImprimerLeDossier_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ImprimerLeDossier.Click
        If LayoutViewAMI.RowCount > 0 Then
            If (ActionTous = 0) Then
                SuccesMsg("Veuillez enregistrer le dossier en cours.")
                Exit Sub
            ElseIf ActionTous = 1 Or ActionTous = 2 Then
                SuccesMsg("Veuillez fermer le dossier en cours.")
                Exit Sub
            End If

            dr = LayoutViewAMI.GetDataRow(LayoutViewAMI.FocusedRowHandle)
            Dim CheminDoc As String = ""
            CheminDoc = ExecuteScallar("Select CheminPubAMI from t_ami where NumeroDAMI='" & EnleverApost(dr("N°").ToString) & "'")
            If CheminDoc.ToString = "" Then
                SuccesMsg("Aucune annonce a imprimer !")
                Exit Sub
            End If

            Dim CheminPdf As String = line & "\AMI\" & FormatFileName(dr("N°").ToString, "_") & "\PublicationAMI.pdf"
            If File.Exists(CheminPdf) Then
                Process.Start(CheminPdf)
            Else
                SuccesMsg("Le fichier à imprimer n'existe pas ou a été supprimé")
            End If

            'CheminPublDoc = rw("CheminPubAMI").ToString
            'Exit Sub

            'Dim reportApproDetail As New ReportDocument
            'Dim crtableLogoninfos As New TableLogOnInfos
            'Dim crtableLogoninfo As New TableLogOnInfo
            'Dim crConnectionInfo As New ConnectionInfo
            'Dim CrTables As Tables
            'Dim CrTable As Table

            'Dim Chemin As String = lineEtat & "\ \"

            'Dim DatSet = New DataSet
            'reportApproDetail.Load(Chemin & "  ")

            'With crConnectionInfo
            '    .ServerName = ODBCNAME
            '    .DatabaseName = DB
            '    .UserID = USERNAME
            '    .Password = PWD
            'End With

            'CrTables = reportApproDetail.Database.Tables
            'For Each CrTable In CrTables
            '    crtableLogoninfo = CrTable.LogOnInfo
            '    crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '    CrTable.ApplyLogOnInfo(crtableLogoninfo)
            'Next

            'reportApproDetail.SetDataSource(DatSet)
            'reportApproDetail.SetParameterValue("CodeProjet", ProjetEnCours)

            'FullScreenReport.FullView.ReportSource = reportApproDetail
            'FinChargement()
            'FullScreenReport.ShowDialog()

        End If
    End Sub

    'Private Sub BEgistrerPublication_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BEgistrerPublication.Click

    'If (TxtNumDp.Text.Trim <> "") Then

    '    If CheminDocAMI.ToString = "" Then
    '        If DocTDR.Text.Trim = "" Then
    '            SuccesMsg("Veuillez saisir l'annonce de publication")
    '            DocTDR.Focus()
    '            Exit Sub
    '        End If

    '        Try
    '            Dim NumDoss = EnleverApost(TxtNumDp.Text)

    '            DebutChargement(True, "Visualisation de l'annonce en cours...")

    '            Dim Chemin As String = lineEtat & "\Marches\AMI\"

    '            Dim PageAnnonceAMI As New ReportDocument
    '            Dim crtableLogoninfos As New TableLogOnInfos
    '            Dim crtableLogoninfo As New TableLogOnInfo
    '            Dim crConnectionInfo As New ConnectionInfo
    '            Dim CrTables As Tables

    '            Dim DatSet = New DataSet

    '            Try
    '                PageAnnonceAMI.Load(Chemin & "AMI_Annonce.rpt")

    '                With crConnectionInfo
    '                    .ServerName = ODBCNAME
    '                    .DatabaseName = DB
    '                    .UserID = USERNAME
    '                    .Password = PWD
    '                End With

    '                CrTables = PageAnnonceAMI.Database.Tables
    '                For Each CrTable In CrTables
    '                    crtableLogoninfo = CrTable.LogOnInfo
    '                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
    '                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
    '                Next

    '                PageAnnonceAMI.SetDataSource(DatSet)

    '                PageAnnonceAMI.SetParameterValue("CodeProjet", ProjetEnCours)
    '                PageAnnonceAMI.SetParameterValue("NumDp", NumDoss)

    '            Catch ex As Exception
    '                FailMsg(ex.ToString)
    '            End Try

    '            CheminDocAMI = line & "\AMI\" & FormatFileName(TxtNumDp.Text, "_")

    '            If (Directory.Exists(CheminDocAMI) = False) Then
    '                Directory.CreateDirectory(CheminDocAMI)
    '            End If

    '            CheminDocAMI = CheminDocAMI & "\TexteSaisie_" & FormatFileName(Now.ToString.Replace(" ", "_"), "_")

    '            'Dim TmpFileAnnonceAMI = Path.GetTempFileName & ".rtf"

    '            PageAnnonceAMI.ExportToDisk([Shared].ExportFormatType.WordForWindows, CheminDocAMI & ".doc")

    '            Dim wordApp As Word.ApplicationClass = New Word.ApplicationClass
    '            Dim doc As Word.Document = wordApp.Documents.Open(CheminDocAMI & ".doc")
    '            Clipboard.SetText(DocTDR.Text)
    '            doc.SaveAs2(FileName:=CheminDocAMI & "\Publication.pdf", FileFormat:=Word.WdSaveFormat.wdFormatPDF)

    '            'doc.ActiveWindow.Selection.WholeStory()
    '            'doc.ActiveWindow.Selection.Copy()
    '            'Dim Data As IDataObject = Clipboard.GetDataObject()

    '            'DocTDR.Copy = Data.GetData(DataFormats.Html)
    '            doc.Close()


    '            ' ExportToDisks(CheminDocAMI)
    '            'DocTDR.LoadDocument(CheminDocAMI, DevExpress.XtraRichEdit.DocumentFormat.Rtf)

    '            'Dim WdApp As New Word.Application
    '            'Dim WdDoc As Word.Document = WdApp.Documents.Add(TmpFileAnnonceAMI)

    '            'Dim CurrentRange As Word.Range = WdDoc.Bookmarks.Item("\endofdoc").Range
    '            'Dim CurrentSection As Word.Section = AjouterNouvelleSectionDocument(WdDoc, CurrentRange)

    '            'CurrentRange.InsertFile(FileName:=CheminDocAMI & "\TexteSaisie.doc")

    '            'WdDoc.SaveAs2(FileName:=CheminDocAMI & "\Publication.pdf", FileFormat:=Word.WdSaveFormat.wdFormatPDF)
    '            'WdDoc.Close(True)
    '            'WdApp.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)

    '            'WebBrowser1.Navigate(CheminDocAMI & "\Publication.pdf")
    '            'Threading.Thread.Sleep(5000)
    '            'Panel3Visualiser.Visible = True
    '            'PanelSaisieText.Visible = False

    '            'Dim wordApp As Word.ApplicationClass = New Word.ApplicationClass
    '            'Dim doc As Word.Document = wordApp.Documents.Open(CheminDocAMI)
    '            'doc.ActiveWindow.Selection.WholeStory()
    '            'doc.ActiveWindow.Selection.Copy()
    '            'Dim Data As IDataObject = Clipboard.GetDataObject()

    '            'DocTDR.Text = Data.GetData(DataFormats.Html)
    '            'doc.Close()

    '            Dim DescriptionAMI As String = IIf(DocTDR.Text.Length <= 500, EnleverApost(DocTDR.Text), Mid(EnleverApost(DocTDR.Text), 1, 500)).ToString
    '            query = "UPDATE t_ami set CheminDocAMI='" & CheminDocAMI & "', DescriptionAMI='" & DescriptionAMI.ToString & "' where NumeroDAMI ='" & NumAMIEnCours & "'"
    '            ' ExecuteNonQuery(query)

    '            FinChargement()
    '        Catch ex As Exception
    '            FailMsg(ex.ToString)
    '        End Try
    '    Else
    '    End If
    'End If
    'End Sub

    Private Sub cmbMarches_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbMarches.SelectedValueChanged
        If cmbMarches.Text.Trim = "" Then
            MethodeMarche.ResetText()
            MontantMarche.ResetText()
        ElseIf ActionTous = 0 Then
            query = "SELECT CodeProcAO FROM t_marche WHERE RefMarche='" & TabRefMarche(cmbMarches.SelectedIndex) & "'"
            Dim CodeProcAO As String = ExecuteScallar(query)
            If CodeProcAO <> "" Then
                MethodeMarche.Text = GetMethode(CodeProcAO)
                MontantMarche.Text = AfficherMonnaie(cmbMarches.Text.Split("|")(1)).Replace(" ", "")
            Else
                MethodeMarche.ResetText()
            End If
        End If
    End Sub

    Private Sub BtAjoutCojo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtAjoutCojo.Click
        If CmbCivCojo.IsRequiredControl("Veuillez selectionner la civilité") Then
            CmbCivCojo.Focus()
            Exit Sub
        End If
        If TxtCojo.IsRequiredControl("Veuillez saisir le nom") Then
            TxtCojo.Focus()
            Exit Sub
        End If
        If TxtFonctionCojo.IsRequiredControl("Veuillez saisir l'organimse") Then
            TxtFonctionCojo.Focus()
            Exit Sub
        End If
        If CmbTitreCojo.IsRequiredControl("Veuillez selectionner un tire") Then
            CmbTitreCojo.Focus()
            Exit Sub
        End If
        If TxtContactCojo.IsRequiredControl("Veuillez saisir le contact/fax/cel") Then
            TxtContactCojo.Focus()
            Exit Sub
        End If
        If TxtMailCojo.IsRequiredControl("Veuillez saisir l'adresse mail") Then
            TxtMailCojo.Focus()
            Exit Sub
        End If

        'verification de l'exitence du president
        If Viewami.RowCount > 0 Then
            For i = 0 To Viewami.RowCount - 1
                If ((Viewami.GetRowCellValue(i, "Titre").ToString = "Président") And CmbTitreCojo.Text = "Président" And ModifTous = False And TypeModif = "") Or (ModifTous = True And TypeModif = "Cojo" And Viewami.GetRowCellValue(i, "Titre").ToString = "Président" And i <> Index And CmbTitreCojo.Text = "Président") Then
                    SuccesMsg("Il existe déjà un président dans la liste")
                    Exit Sub
                End If
            Next
        End If

        If ModifTous = True And TypeModif = "Cojo" Then
            Viewami.GetDataRow(Index).Item("Civilité") = CmbCivCojo.Text
            Viewami.GetDataRow(Index).Item("Nom membre") = TxtCojo.Text
            Viewami.GetDataRow(Index).Item("Titre") = CmbTitreCojo.Text
            Viewami.GetDataRow(Index).Item("Fonction") = TxtFonctionCojo.Text
            Viewami.GetDataRow(Index).Item("Téléphone") = TxtContactCojo.Text
            Viewami.GetDataRow(Index).Item("E-mail") = TxtMailCojo.Text
            Viewami.GetDataRow(Index).Item("Type commission") = IIf(ChkEvaluateur.Checked = True, "Evaluateur", "COJO").ToString
            Viewami.GetDataRow(Index).Item("LigneModif") = "Modifier"
        Else
            Dim dt As DataTable = GridAmi.DataSource
            Dim drS = dt.NewRow()

            drS("CodeMembre") = ""
            drS("Civilité") = CmbCivCojo.Text
            drS("Nom membre") = TxtCojo.Text
            drS("Titre") = CmbTitreCojo.Text
            drS("Fonction") = TxtFonctionCojo.Text
            drS("Téléphone") = TxtContactCojo.Text
            drS("E-mail") = TxtMailCojo.Text
            drS("Type commission") = IIf(ChkEvaluateur.Checked = True, "Evaluateur", "COJO").ToString
            drS("LigneModif") = "Ajouter"
            dt.Rows.Add(drS)
        End If
        InitialiserMemebredelaCommission()
        ModifTous = False
        TypeModif = ""
    End Sub

    Private Sub GridAmi_DoubleClick(sender As Object, e As EventArgs) Handles GridAmi.DoubleClick
        If Viewami.RowCount > 0 Then
            ModifTous = True
            TypeModif = "Cojo"
            dr = Viewami.GetDataRow(Viewami.FocusedRowHandle)
            Index = Viewami.FocusedRowHandle
            CmbCivCojo.Text = dr("Civilité").ToString
            TxtCojo.Text = dr("Nom membre").ToString()
            CmbTitreCojo.Text = dr("Titre").ToString
            TxtFonctionCojo.Text = dr("Fonction").ToString
            TxtContactCojo.Text = dr("Téléphone").ToString
            TxtMailCojo.Text = dr("E-mail").ToString
        End If
    End Sub

    Private Sub LgCritere_DoubleClick(sender As Object, e As EventArgs) Handles LgCritere.DoubleClick
        If ViewCritere.RowCount > 0 Then
            ModifTous = True
            TypeModif = "Critere"
            dr = ViewCritere.GetDataRow(ViewCritere.FocusedRowHandle)
            Index = ViewCritere.FocusedRowHandle
            libellecritere.Text = dr("Libellé critère").ToString
            valeurcritere.Text = dr("Valeurs").ToString
            Note.Text = dr("Note").ToString.Split(" ")(0)
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If ViewCritere.RowCount > 0 Then
            dr = ViewCritere.GetDataRow(ViewCritere.FocusedRowHandle)
            If ConfirmMsg("Voulez-vraiment suppression le critère d'évaluation ?") = DialogResult.Yes Then
                If dr("Refcritere") <> "" Then
                    query = "delete from t_critereami where Refcritere='" & dr("Refcritere") & "' "
                    ExecuteNonQuery(query)
                End If
                ViewCritere.GetDataRow(ViewCritere.FocusedRowHandle).Delete()
                For i = 0 To ViewCritere.RowCount - 1
                    ViewCritere.GetDataRow(i).Item("N°") = i + 1
                Next
                ModifTous = False
                TypeModif = ""
            End If
        End If
    End Sub
    Private Sub SupprimerToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SupprimerToolStripMenuItem.Click
        If Viewami.RowCount > 0 Then
            dr = Viewami.GetDataRow(Viewami.FocusedRowHandle)
            If ConfirmMsg("Voulez-vous vraiment supprimer ?") = DialogResult.Yes Then
                If dr("CodeMembre") <> "" Then
                    query = "delete from t_commission where CodeMem='" & dr("CodeMembre") & "' "
                    ExecuteNonQuery(query)
                End If
                Viewami.GetDataRow(Viewami.FocusedRowHandle).Delete()
                ModifTous = False
                TypeModif = ""
            End If
        End If
    End Sub

    Private Sub ContextMenuStrip5_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip5.Opening
        If ViewCritere.RowCount = 0 Or ActionTous = 2 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub ContextMenuStrip3_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip3.Opening
        If Viewami.RowCount = 0 Or ActionTous = 2 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub AjouterCriter_Click(sender As Object, e As EventArgs) Handles AjouterCriter.Click
        If libellecritere.IsRequiredControl("Veuillez saisir le libellé du critère") Then
            libellecritere.Focus()
            Exit Sub
        End If
        If valeurcritere.IsRequiredControl("Information incomplète !") Then
            valeurcritere.Focus()
            Exit Sub
        End If
        If Note.IsRequiredControl("Veuillez saisir la note") Then
            Note.Focus()
            Exit Sub
        End If

        Dim NewCrite As DataTable = LgCritere.DataSource

        Dim NumCritere As Integer = ViewCritere.RowCount

        If ModifTous = True And TypeModif = "Critere" Then
            'Verification des points totals
            If TotaleDesPoints(Index) = True Then
                SuccesMsg("Le total des points des critères d'évaluation ne doit pas excéder 100")
                Exit Sub
            End If

            ViewCritere.GetDataRow(Index).Item("Libellé critère") = libellecritere.Text
            ViewCritere.GetDataRow(Index).Item("Valeurs") = valeurcritere.Text
            ViewCritere.GetDataRow(Index).Item("Note") = Note.Text.Replace(".", ",") & " Points"
            ViewCritere.GetDataRow(Index).Item("LigneModif") = "Modifier"
        Else
            'Verification des points totals
            If TotaleDesPoints() = True Then
                SuccesMsg("Le total des points des critères d'évaluation ne doit pas excéder 100")
                Exit Sub
            End If

            Dim DrS = NewCrite.NewRow
            DrS("Refcritere") = ""
            DrS("N°") = NumCritere + 1
            DrS("Libellé critère") = libellecritere.Text
            DrS("Valeurs") = valeurcritere.Text
            DrS("Note") = Note.Text.Replace(".", ",") & " Points"
            DrS("LigneModif") = "Ajouter"
            NewCrite.Rows.Add(DrS)
        End If

        libellecritere.Text = ""
        valeurcritere.Text = ""
        Note.Text = ""
        ModifTous = False
        TypeModif = ""
    End Sub

    Private Function TotaleDesPoints(Optional Index As Integer = -1) As Boolean
        Dim TotalPoints As Decimal = 0

        Try
            If ViewCritere.RowCount > 0 Then
                For i = 0 To ViewCritere.RowCount - 1
                    If Index = -1 Then
                        TotalPoints += CDec(ViewCritere.GetRowCellValue(i, "Note").ToString.Split(" ")(0).Replace(".", ","))
                    Else
                        'Ligne en cours de Modification
                        If i <> Index Then
                            TotalPoints += CDec(ViewCritere.GetRowCellValue(i, "Note").ToString.Split(" ")(0).Replace(".", ","))
                        End If
                    End If
                Next
            End If

            TotalPoints += CDec(Note.Text.Replace(".", ",").Replace(" ", ""))

            If TotalPoints > 100 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try

    End Function

    'Private Sub BtSaisiAnonoce_Click(sender As Object, e As EventArgs)
    '    'afficharge d'un dossier
    '    If ActionTous = 2 = True Then
    '        Exit Sub
    '    End If

    '    If (TxtNumDp.Text.Trim <> "") Then

    '        If CheminPublDoc.ToString = "" Then
    '            Try
    '                Dim NumDoss = EnleverApost(TxtNumDp.Text)

    '                DebutChargement(True, "Génération de la page de garde en cours...")

    '                Dim Chemin As String = lineEtat & "\Marches\AMI\"

    '                Dim PageAnnonceAMI As New ReportDocument
    '                Dim crtableLogoninfos As New TableLogOnInfos
    '                Dim crtableLogoninfo As New TableLogOnInfo
    '                Dim crConnectionInfo As New ConnectionInfo
    '                Dim CrTables As Tables

    '                Dim DatSet = New DataSet

    '                Try
    '                    PageAnnonceAMI.Load(Chemin & "AMI_Annonce.rpt")

    '                    With crConnectionInfo
    '                        .ServerName = ODBCNAME
    '                        .DatabaseName = DB
    '                        .UserID = USERNAME
    '                        .Password = PWD
    '                    End With

    '                    CrTables = PageAnnonceAMI.Database.Tables
    '                    For Each CrTable In CrTables
    '                        crtableLogoninfo = CrTable.LogOnInfo
    '                        crtableLogoninfo.ConnectionInfo = crConnectionInfo
    '                        CrTable.ApplyLogOnInfo(crtableLogoninfo)
    '                    Next

    '                    PageAnnonceAMI.SetDataSource(DatSet)

    '                    PageAnnonceAMI.SetParameterValue("CodeProjet", ProjetEnCours)
    '                    PageAnnonceAMI.SetParameterValue("NumDp", NumDoss)

    '                Catch ex As Exception
    '                    FailMsg(ex.ToString)
    '                End Try

    '                CheminPublDoc = line & "\AMI\" & FormatFileName(TxtNumDp.Text, "_")

    '                If (Directory.Exists(CheminPublDoc) = False) Then
    '                    Directory.CreateDirectory(CheminPublDoc)
    '                End If

    '                'CheminPublDoc = CheminPublDoc & "\PublicationAMI" & FormatFileName(Now.ToString.Replace(" ", "_"), "_") & ".docx"
    '                CheminPublDoc = CheminPublDoc & "\PublicationAMI.doc"
    '                PageAnnonceAMI.ExportToDisk(ExportFormatType.WordForWindows, CheminPublDoc)

    '                ExecuteNonQuery("UPDATE t_ami set CheminPubAMI='" & CheminPublDoc.ToString.Replace("\", "\\") & "' where NumeroDAMI ='" & EnleverApost(TxtNumDp.Text) & "'")
    '                Process.Start(CheminPublDoc.ToString)

    '                ActualiserPub = True
    '                BtSaisiAnonoce.Text = "Modifier l'annonce"
    '                FinChargement()
    '            Catch ex As Exception
    '                FailMsg(ex.ToString)
    '            End Try
    '        Else
    '            'modification annonce
    '            If (File.Exists(CheminPublDoc.ToString) = True) Then
    '                Process.Start(CheminPublDoc.ToString)
    '                ActualiserPub = True
    '            Else
    '                SuccesMsg("Le chemin spécifier n'existe pas")
    '                Exit Sub
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub BtActualiserAnnonce_Click(sender As Object, e As EventArgs)
    '    'afficharge d'un dossier
    '    If ActionTous = 2 = True Then
    '        Exit Sub
    '    End If

    '    If ActualiserPub = True Or CheminPublPdf.ToString = "" Then
    '        If File.Exists(CheminPublDoc.ToString) = True Then
    '            DebutChargement(True, "Actualisation de l'annonce en cours...")

    '            Dim NewCheminPDF1 As String = "Publication_" & FormatFileName(Now.ToString.Replace(" ", "_"), "_") & ".pdf"
    '            Dim NewCheminPDF As String = line & "\AMI\" & FormatFileName(TxtNumDp.Text, "_") & "\" & NewCheminPDF1

    '            Dim WdApp As New Word.Application
    '            Dim WdDoc As Word.Document = WdApp.Documents.Add(CheminPublDoc.ToString)
    '            WdDoc.SaveAs2(FileName:=NewCheminPDF, FileFormat:=Word.WdSaveFormat.wdFormatPDF)
    '            WdDoc.Close(True)
    '            WdApp.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)

    '            CheminPublPdf = NewCheminPDF.ToString

    '            query = "UPDATE t_ami set CheminPubAMI='" & CheminPublDoc.ToString.Replace("\", "\\") & "|" & NewCheminPDF1.ToString & "' where NumeroDAMI ='" & EnleverApost(TxtNumDp.Text) & "'"
    '            ExecuteNonQuery(query)

    '            WebBrowser1.Navigate(CheminPublPdf.ToString)
    '            Threading.Thread.Sleep(5000)
    '            FinChargement()
    '        End If
    '    End If
    'End Sub

    Private Sub ValiderLeDossierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValiderLeDossierToolStripMenuItem.Click
        If LayoutViewAMI.RowCount > 0 Then
            dr = LayoutViewAMI.GetDataRow(LayoutViewAMI.FocusedRowHandle)

            If dr("ValiderEditionAmi").ToString = "Valider" Then
                SuccesMsg("Ce dossier a été validé")
                Exit Sub
            End If
            If dr("Statut").ToString = "Annulé" Then
                FailMsg("Impossible de valider un dossier Annulé")
                Exit Sub
            End If

            If ConfirmMsg("Confirmez-vous la validation du dossier ?") = DialogResult.Yes Then

                DebutChargement()

                ExecuteNonQuery("UPDATE t_ami set ValiderEditionAmi='Valider' where NumeroDAMI='" & EnleverApost(dr("N°").ToString) & "' and CodeProjet='" & ProjetEnCours & "'")

                query = "SELECT CodeMem, Civil, NomMem, EmailMem FROM T_Commission WHERE NumeroDAO='" & EnleverApost(dr("N°").ToString) & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)

                Dim CodeCrypter As String = String.Empty
                For Each rw0 In dt0.Rows
                    CodeCrypter = GenererToken(dr("N°").ToString, rw0("CodeMem"), "AMI", DB)
                    ExecuteNonQuery("Update T_Commission set AuthKey='" & CodeCrypter.ToString.Split(":")(0) & "' where CodeMem='" & rw0("CodeMem") & "' and NumeroDAO='" & EnleverApost(dr("N°").ToString) & "'")
                    envoieMail(rw0("Civil").ToString & " " & MettreApost(rw0("NomMem").ToString), MettreApost(rw0("EmailMem").ToString), CodeCrypter)
                Next
                FinChargement()
                SuccesMsg("Dossier validé avec succès")
                ArchivesAMI()
            End If
        End If
    End Sub

    Private Sub DatePub1_EditValueChanged(sender As Object, e As EventArgs) Handles DatePub1.EditValueChanged, NbreDelaiPub.EditValueChanged, JoursDelaiPub.EditValueChanged, HeureDepot.EditValueChanged
        If DatePub1.Text.Trim <> "" And NbreDelaiPub.Text.Trim <> "" And JoursDelaiPub.Text.Trim <> "" Then
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

        ' If DatePub1.Text.Trim <> "" And NbreDelaiPub.Text.Trim <> "" And JoursDelaiPub.Text.Trim <> "" Then

        'If DateReporte.Text = "" And HeureReporte.Text = "" Then 'pas de reports
        Dim datefindepot As Date = GetDatefinDepot(CDate(DatePub1.Text))
        Datedepot.Text = datefindepot
        If DateReporte.Text = "" And HeureReporte.Text = "" Then DateOuverture.Text = datefindepot
        If HeureDepot.Text.Trim <> "" And DateReporte.Text = "" And HeureReporte.Text = "" Then
            Dim HeurOuvertur As DateTime = datefindepot & " " & HeureDepot.Text
            HeurOuvertur = HeurOuvertur.AddMinutes(30)
            DateOuverture.Text = CDate(HeurOuvertur).ToShortDateString
            HeureOuverture.EditValue = CDate(HeurOuvertur).ToLongTimeString
        End If
        ' End If
        'Else  'en cas de reports
        '    If DateReporte.Text = "" And HeureReporte.Text = "" Then
        '    End If
        'End If

    End Sub

    Private Function GetDatefinDepot(ByVal datefindepo As Date) As Date

        If JoursDelaiPub.Text = "Jours" Then
            datefindepo = datefindepo.AddDays(CInt(NbreDelaiPub.Text))
        ElseIf JoursDelaiPub.Text = "Semaines" Then
            datefindepo = datefindepo.AddDays(CInt(NbreDelaiPub.Text) * 7)
        ElseIf JoursDelaiPub.Text = "Mois" Then
            '  datefindepo = datefindepo.AddDays(CInt(NbreDelaiPub.Text) * 30)
            datefindepo = datefindepo.AddMonths(CInt(NbreDelaiPub.Text))
        End If
        Return datefindepo
    End Function

    Private Sub XtraTabControl1_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles XtraTabControl1.SelectedPageChanged
        If XtraTabControl1.SelectedTabPageIndex = 1 Then
            If CheminPublDoc.ToString <> "" And File.Exists(CheminPublDoc.ToString) Then
                DebutChargement(True, "Chargement de l'annonce en cours..")
                DocTDR.LoadDocument(line & "\AMI\" & FormatFileName(TxtNumDp.Text, "_") & "\PublicationAMI.docx", DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
                FinChargement()
            End If

            If ActionTous = 1 Then
                BtImportAnnonce.Enabled = True
                BtModifAnnonce.Enabled = True
            Else
                BtImportAnnonce.Enabled = False
                BtModifAnnonce.Enabled = False
            End If

        End If

    End Sub

    Private Sub BtModifAnnonce_Click(sender As Object, e As EventArgs) Handles BtModifAnnonce.Click
        If TxtNumDp.Text.Trim <> "" And CheminPublDoc.ToString <> "" Then

            If DocTDR.Text.Trim = "" Then
                SuccesMsg("Veuillez importez un fichier")
                Exit Sub
            End If

            DebutChargement()
            ReponseDialog = CheminPublDoc.ToString
            ExceptRevue = TxtNumDp.Text
            ExceptRevue2 = "AMI"
            SaisieTexte.ShowDialog()
            FinChargement()
            DocTDR.LoadDocument(CheminPublDoc, DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
            ReponseDialog = ""
            ExceptRevue = ""
            ExceptRevue2 = ""
        End If
    End Sub

    Private Sub BtImportAnnonce_Click(sender As Object, e As EventArgs) Handles BtImportAnnonce.Click
        If (TxtNumDp.Text.Trim <> "") Then
            Dim dlg As New OpenFileDialog
            dlg.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            'dlg.Filter = "Documents Word (*.doc; *.docx)|*.doc;*.docx"
            dlg.Filter = "Documents Word (*.docx)|*.docx"

            If dlg.ShowDialog() = DialogResult.OK Then
                If (dlg.FileName.ToString = "") Then
                    Exit Sub
                End If

                DebutChargement(True, "Importation de l'annonce en cours...")
                Try
                    ' Dim fStream As FileStream
                    'fStream = New FileStream(dlg.FileName, FileMode.Open)
                    'DocTDR.LoadDocument(fStream, DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
                    DocTDR.LoadDocument(dlg.FileName, DevExpress.XtraRichEdit.DocumentFormat.OpenXml)

                    Dim CheminPdf As String = line & "\AMI\" & FormatFileName(TxtNumDp.Text, "_") & " \PublicationAMI.pdf"
                    If CheminPublDoc.ToString = "" Then
                        CheminPublDoc = line & "\AMI\" & FormatFileName(TxtNumDp.Text, "_")
                        If (Directory.Exists(CheminPublDoc) = False) Then
                            Directory.CreateDirectory(CheminPublDoc)
                        End If
                        CheminPublDoc = CheminPublDoc & "\PublicationAMI.docx"
                        ExecuteNonQuery("UPDATE t_ami Set CheminPubAMI='" & CheminPublDoc.ToString.Replace("\", "\\") & "' where NumeroDAMI ='" & EnleverApost(TxtNumDp.Text) & "'")
                    End If

                    DocTDR.SaveDocument(CheminPublDoc, DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
                    DocTDR.ExportToPdf(CheminPdf)
                Catch exs As IOException
                    FinChargement()
                    SuccesMsg("Impossible de sauvegardé ce fichier. car un exemplaire du fichier à sauvegardé est ouvert dans une autre application. Veuillez le fermé svp.")
                    DocTDR.ResetText()
                Catch ex As Exception
                    FinChargement()
                    FailMsg(ex.ToString)
                End Try
                BtModifAnnonce.Enabled = True
                FinChargement()
            End If
        End If
    End Sub

    Private Sub AnnulerLAMIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnnulerLAMIToolStripMenuItem.Click
        Try
            If LayoutViewAMI.RowCount > 0 Then
                If (ActionTous = 0) Then
                    SuccesMsg("Veuillez enregistrer le dossier en cours.")
                    Exit Sub
                ElseIf ActionTous = 1 Or ActionTous = 2 Then
                    SuccesMsg("Veuillez fermer le dossier en cours.")
                    Exit Sub
                End If

                dr = LayoutViewAMI.GetDataRow(LayoutViewAMI.FocusedRowHandle)
                If dr("Statut").ToString = "Annulé" Then
                    FailMsg("Ce dossier a été Annulé")
                    Exit Sub
                End If

                'Verifier si le marche a ete engager
                If dr("Statut").ToString = "Terminé" And dr("Méthode").ToString.ToUpper = "3CV" Then
                    FailMsg("Impossible d'annuler un marché déjà executé")
                    Exit Sub
                End If

                If dr("Statut").ToString = "Terminé" Then
                    FailMsg("Impossible d'annuler ce dossier," & vbNewLine & "car il est utilisé pour élaborer une DP")
                    Exit Sub
                End If

                'Tous les consultants sur la liste restriente doivent être disqualifié pour permettre l'annulation
                Dim NbreConsultDisq As Integer = 0
                Dim NbreConsultRetenu As Integer = 0
                NbreConsultRetenu = Val(ExecuteScallar("select count(*) from t_soumissionconsultant where NumeroDp='" & EnleverApost(dr("N°").ToString) & "' and RangConsult IS NOT NULL and EvalTechOk='OUI'"))

                If NbreConsultRetenu > 0 Then
                    NbreConsultDisq = Val(ExecuteScallar("select count(*) from t_soumissionconsultant where NumeroDp='" & EnleverApost(dr("N°").ToString) & "' and RangConsult IS NOT NULL and EvalTechOk='OUI' and ConsultDisqualifie IS NOT NULL"))
                    Dim MessageText As String = "Impossible d'annuler ce dossier, car il existe des" & vbNewLine & "consultants sur la liste restriente non disqualifié"
                    If NbreConsultRetenu <> NbreConsultDisq Then
                        FailMsg("Impossible d'annuler ce dossier, car il existe des" & vbNewLine & "consultants sur la liste restriente non disqualifié")
                        Exit Sub
                    End If
                End If

                If ConfirmMsg("Voulez-vous vraiment annuler ce dossier ?") = DialogResult.Yes Then
                    ReponseDialog = ""
                    Dim NewMotifAnnulDoss As New MotifAnnulationDossier
                    NewMotifAnnulDoss.TxtTextDoss.Text = "Annulation du dossier N° " & dr("N°").ToString
                    NewMotifAnnulDoss.ShowDialog()
                    If ReponseDialog.ToString = "" Then
                        Exit Sub
                    End If
                    'DossUtiliser=NULL,

                    ExecuteNonQuery("Update t_ami set StatutDoss='Annulé', MotifAnnulationDossier='" & EnleverApost(ReponseDialog.ToString) & "' where NumeroDAMI='" & EnleverApost(dr("N°").ToString) & "'")
                    If dr("Méthode").ToString.ToUpper = "3CV" Then
                        Dim RefMarch As String = ExecuteScallar("select RefMarche from t_ami where NumeroDAMI='" & EnleverApost(dr("N°").ToString) & "'")
                        ExecuteNonQuery("Update t_marche Set Forfait_TpsPasse=NULL, NumeroDAO=NULL where RefMarche='" & RefMarch & "'")
                    End If

                    SuccesMsg("Dossier annulé avec succès")
                    ChargerMarcher()
                    LayoutViewAMI.SetFocusedRowCellValue("Statut", "Annulé")
                    'Fermeture des formulaires
                    FermerForm()
                End If
            Else
                FailMsg("Aucun dossier à Annuler")
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try

    End Sub

    Private Sub FermerForm()
        Try
            'Arret du processus et fermetures des formulairs ouverts
            'For Each child As Object In Me.MdiChildren
            For Each child As Object In ClearMdi.MdiChildren
                If (child.Name = "DepotAMI") Or (child.Name = "OuvertureAmi") Or (child.Name = "RapportEvaluationMI") Or (child.Name = "ListeRestreindreAMI") Then
                    child.Close()
                End If
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

End Class
