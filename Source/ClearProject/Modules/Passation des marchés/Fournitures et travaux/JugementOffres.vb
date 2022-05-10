Imports MySql.Data.MySqlClient
Imports ClearProject.PassationMarche
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Imports Word = Microsoft.Office.Interop.Word
Imports DevExpress.XtraEditors.Repository

Public Class JugementOffres
    Dim modif As Boolean = False
    Dim dt = New DataTable()
    Dim dtExam = New DataTable()
    Dim dtTraite = New DataTable()
    Public EtapeActuelle As String = ""
    Dim CodeActuel As String = ""
    Dim SpecDemande As String = ""
    Public ValeurActuelle As String = ""
    Dim Accord As String = ""
    Dim DrX As DataRow
    Dim NumMarche As String
    Dim DoublCick As Boolean = False
    Dim IndexSelectionne As Integer = 0
    Dim NomGridView As String = ""
    Public CodeCritere(100) As String
    Public TableCritere(100) As String
    Public CritereElimine(100) As String
    Public GroupeCritere(100) As String
    Public NombreCritere As Decimal = 0
    Public AttributionMarche As String = ""
    Dim CheminRaportEvalPdf As String = ""
    Dim EtatRapportEvalOffres As String = ""
    Dim ListeTbaleClicke As Boolean() = {False, False, False, False}
    Dim BtModifRapportClick As Boolean = False
    Public ModificationAnalyseNonTerminer As Boolean = False
    Public ExisteDesCriterePostQualifications As Boolean = False

    Private Sub JugementOffres_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ChargerDossier()
        TxtTypeExamen.Text = ""
    End Sub

    Private Sub ChargerDossier()
        CmbNumDoss.Text = ""
        CmbNumDoss.Properties.Items.Clear()
        query = "select NumeroDAO from T_DAO where DossValider=true and statut_DAO<>'Annulé' and DateFinouverture<>'' and CodeProjet='" & ProjetEnCours & "' ORDER BY DateEdition DESC"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbNumDoss.Properties.Items.Add(MettreApost(rw("NumeroDAO").ToString))
        Next
    End Sub

    'EtapeActuelle->"ExamPrelim" =>"EXAMEN DETAILLE" ** GridDetail **  = "ExamDetail" = "EXAMEN POST QUALIFICATION" ** GridPost ** -- "ExamPost" --BILAN ** GridBilan **

#Region "Methodes"

    Private Sub ChargerGridExam(ByVal TypeExam As String)
        'If (TypeExam = "") Then
        '    ChargerAnalyse()
        'ElseIf (TypeExam = "Analyse") Then
        '    ChargerExamPrelim()
        If (TypeExam = "ExamPrelim") Then
            ChargerExamDetaille()
        ElseIf (TypeExam = "ExamDetail") Then
            ChargerExamPostQualif()
        ElseIf (TypeExam = "ExamPost") Then
            BilanExamOffres()
        End If
    End Sub

    Private Sub AfficherGrid(ByVal Examen As String)
        GridTravail.Visible = False
        GridPrelim.Visible = False
        GridDetail.Visible = False
        GridPostFinal.Visible = False
        GridBilan.Visible = False
        BilansOffres.Visible = False
        PnlEditionMarche.Visible = False
        PanelRapportEvaluation.Visible = False
        TabRapportEval.Visible = False
        FullCouverture.Visible = False

        GbTraites.Visible = True

        'If (Examen = "") Then
        ' GetVisibleGrid("GridTravail")
        'ElseIf (Examen = "Analyse") Then
        'GetVisibleGrid("GridPrelim")
        If (Examen = "ExamPrelim") Then
            GridDetail.Visible = True
        ElseIf (Examen = "ExamDetail") Then
            GridPostFinal.Visible = True
            BilansOffres.Visible = True
            TxtTypeExamen.Text = "EXAMEN POST QUALIFICATION"
            BilansOffres.Text = "[Bilan des offres]"
            BilansOffres.ToolTip = "Bilan des offres"

        ElseIf (Examen = "ExamPost") Then
            GridBilan.Visible = True
            BilansOffres.Visible = True
            BilansOffres.Enabled = True
            TxtTypeExamen.Text = "BILAN DU JUGEMENT DES OFFRES" 
            BilansOffres.Text = "[ Resultat ]"

            BilansOffres.ToolTip = "Resultat examen post qualification"
        ElseIf (Examen = "ConsolidationRapportEval") Then
            PanelRapportEvaluation.Visible = True
            GbTraites.Visible = False
        ElseIf (Examen = "ElaborationMarche") Then
            PnlEditionMarche.Visible = True
            GbTraites.Visible = False
        ElseIf (Examen = "Rapport en detaille") Then
            TabRapportEval.Visible = True
            FullCouverture.Visible = True
            GbTraites.Visible = False
        End If
    End Sub

    Private Sub InitFormMarche()

        'CmbLotMarche.Text = ""
        TxtLotMarche.Text = ""
        NumerosMarche.Text = ""
        TxtRefLotMarche.Text = ""
        TxtCodeFournisMarche.Text = ""

        TxtFournisMarche.Text = ""
        TxtAdresseFournisMarche.Text = ""
        TxtContactFournisMarche.Text = ""
        TxtNomRepLegal.Text = ""
        TxtBpRepLegal.Text = ""
        TxtContactRepLegal.Text = ""

        TxtContribuable.Text = ""
        TxtRegCommerce.Text = ""
        TxtNomBanqueFournis.Text = ""
        TxtNumCompteFournis.Text = ""

        TxtMontantMarche.Text = ""
        TxtMontMarcheLettre.Text = ""
        TxtExecutionMarche.Text = ""
        TxtPrctCautionDef.Text = 0
        TxtMontCautionDef.Text = ""
        TxtPrctAvance.Text = 0
        TxtMontAvance.Text = ""

        TxtBailleurMarche.Text = ""
        TxtConventionMarche.Text = ""
        TxtImputBudgetaire.Text = ""
        MontantTVA.Text = ""

        ViewRepartion.Columns.Clear()
        ListeRepartion.DataSource = Nothing
        GridArticle.Rows.Clear()
        IndexSelectionne = 0
        DoublCick = False
        IndexSelectionne = 0
        NomGridView = ""
        CombArticle.Text = ""
        TxtTextArticle.Text = ""
        TxtSaisiTextArticle.Text = ""
    End Sub

    Private Sub Initialisation()
        ListeTbaleClicke = {False, False, False, False}

        If modif = False Then
            EtapeActuelle = ""
            AttributionMarche = ""
            EtapeExamDetail.ForeColor = Color.Gray
            EtapeExamPost.ForeColor = Color.Gray
            EtapeAnalyse.ForeColor = Color.Gray
            EtapeExamDetail.ImageIndex = 1
            EtapeExamPost.ImageIndex = 1
            EtapeAnalyse.ImageIndex = 1
            GridTravail.Refresh()
            BtExecuter.Text = "Début examen" & vbNewLine & "detaille"
            CmbNumLot.Text = ""
            cmbSousLot.Text = ""
            TxtLibelleSousLot.Text = ""
            TxtLibelleLot.Text = ""
            CmbSoumis.Text = ""
            TxtAdresseSoumis.Text = ""
            TxtTypeExamen.Text = "VERIFICATION DES OFFRES"
            CmbLotMarche.Text = ""
            TxtNbLot.Text = ""
            TxtLibelleDoss.Text = ""
            TxtDateOuvert.Text = ""
            TxtMethode.Text = ""
            TxtTypeMarche.Text = ""
            dtTraite.Columns.Clear()
            dtTraite.Rows.Clear()
            dtExam.Columns.Clear()
            dtExam.Rows.Clear()
            GridPostFinal.Columns.Clear()
            GridPostFinal.Rows.Clear()

            GridPrelim.Visible = False
            GridDetail.Visible = False
            ' GridPost.Visible = False
            GridPostFinal.Visible = False
            GridBilan.Visible = False
            PnlEditionMarche.Visible = False
            PanelRapportEvaluation.Visible = False
            TabRapportEval.Visible = False
            FullCouverture.Visible = False
            BilansOffres.Visible = False

            GbTraites.Visible = True
            GridTravail.Visible = True
            BtExecuter.Enabled = False
            PanelLots.Enabled = False
            PanelVerdict.Enabled = False
            PanelVerdict.Visible = False
            btnModifAnalyse.Enabled = False
            ExisteDesCriterePostQualifications = False
            DesactiverBouton(False)
        End If
    End Sub

    Private Sub JugementOffres_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

#End Region

#Region "Dossier DAO"

    Private Sub CmbNumDoss_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbNumDoss.SelectedIndexChanged
        Initialisation()
        If CmbNumDoss.SelectedIndex <> -1 Then
            Dim FinUpdateDate As String = ""

            If modif = False Then
                'AttributionMarche = ""
                'GbCojo.Enabled = false

                query = "select * from T_DAO where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    TxtNbLot.Text = rw("NbreLotDAO").ToString
                    TxtLibelleDoss.Text = MettreApost(rw("IntituleDAO").ToString)
                    TxtDateOuvert.Text = CDate(rw("DateDebutOuverture").ToString).ToShortDateString
                    TxtMethode.Text = rw("MethodePDM").ToString
                    TxtTypeMarche.Text = MettreApost(rw("TypeMarche").ToString)

                    AttributionMarche = rw("Attribution").ToString

                    'GbCojo.Enabled = True
                    CmbNumLot.ResetText()
                    CmbNumLotAttrib.ResetText()
                    CmbNumLot.Properties.Items.Clear()
                    CmbNumLotAttrib.Properties.Items.Clear()
                    For i As Integer = 1 To CInt(rw("NbreLotDAO").ToString)
                        CmbNumLot.Properties.Items.Add(i.ToString)
                        CmbNumLotAttrib.Properties.Items.Add(i.ToString)
                    Next

                    TxtTypeExamen.Text = "EXAMEN DETAILLE"
                    EtapeActuelle = "ExamPrelim"
                    PanelVerdict.Visible = False

                    FinUpdateDate = rw("ExamDetailOffres").ToString
                    EtatRapportEvalOffres = rw("EtatRapportEvalOffre").ToString
                    CheminRaportEvalPdf = rw("CheminRapport").ToString

                    If (rw("ExamDetailOffres").ToString <> "") Then
                        EtapeExamDetail.ImageIndex = 0
                        EtapeExamDetail.ForeColor = Color.Black
                        EtapeActuelle = "ExamDetail"
                        'TxtTypeExamen.Text = "EXAMEN POST QUALIFICATION"
                        CmbSoumis.Enabled = False

                        If (rw("ExamPostQualifOffres").ToString <> "") Then
                            EtapeExamPost.ImageIndex = 0
                            EtapeActuelle = "ExamPost"
                            EtapeExamPost.ForeColor = Color.Black
                            TxtTypeExamen.Text = "BILAN DU JUGEMENT DES OFFRES" ' DU DAO N°" & CmbNumDoss.Text
                        Else
                            EtapeExamPost.ImageIndex = 2
                            EtapeExamPost.ForeColor = Color.Black
                        End If
                    Else
                        EtapeExamDetail.ImageIndex = 2
                        EtapeExamDetail.ForeColor = Color.Black
                    End If
                Next

                If FinUpdateDate.ToString = "" Then 'Examen "EXAMEN DETAILLE" non effectué
                    'Verifier si les membres de la cojo ont finis l'annalye des offres et l'analyse preleminaire
                    query = "SELECT COUNT(*) FROM t_dao_evalcojo as F , t_dao as S WHERE F.NumeroDAO=S.NumeroDAO AND S.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and S.CodeProjet='" & ProjetEnCours & "' AND F.ExamPrelimOffres is NULL"
                    Dim NbreCOJOEval = Val(ExecuteScallar(query))
                    'Si tous les membres de la commission ont finis l'analyse
                    If NbreCOJOEval = 0 Then 'Analyse terminé
                        'Verifier si tous les soumissionnaire sont disqualifier ou pas
                        If GetUpdateExamenPreleminaire() = True Then 'Tous les soumissionnaire sont disqualifier
                            TxtTypeExamen.Text = "Tous les soumissionnaires de ce dossier sont disqualifiés"
                            TxtTypeExamen.ForeColor = Color.Red
                            BtExecuter.Enabled = Enabled = False
                        Else
                            BtExecuter.Enabled = True
                            TxtTypeExamen.ForeColor = Color.Black
                        End If
                    End If
                End If

                OffresTraitees()

                'Verifier s'il existe des critères post qualification pour le dossier selectionné
                If Val(ExecuteScallar("select count(*) from T_DAO_PostQualif where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "'")) > 0 Then
                    ExisteDesCriterePostQualifications = True
                Else
                    ExisteDesCriterePostQualifications = False
                End If

                If (EtapeActuelle = "ExamDetail") Then
                    'BtExecuter.Text = "Début examen" & vbNewLine & "post qualification"
                    PanelLots.Enabled = False
                    BtExecuter.Enabled = True
                    TxtTypeExamen.ForeColor = Color.Black
                    ' ChargerExamPostQualif()
                    ChargerExamDetaille(True)
                ElseIf (EtapeActuelle = "ExamPost") Then
                    GetTerminerModifExamen()
                    BilanExamOffres()
                End If

            Else 'Modification

                'Initialisation
                If EtapeActuelle = "ExamPrelim" Then
                    Dim Reponse As Boolean = GetUpdateExamenPreleminaire() 'Update de fin evaluation cojo
                    BtExecuter.Text = "Début examen" & vbNewLine & "detaillé"
                    TxtTypeExamen.Text = "EXAMEN DETAILLE"
                    BtExecuter.Enabled = True
                    btnModifAnalyse.Enabled = True
                    DesactiverBouton(False)
                    ' Dim dtExaDetail As DataTable = GridDetail.DataSource
                    GridDetail.DataSource = Nothing
                    'dtExaDetail.Columns.Clear()
                    'dtExaDetail.Rows.Clear()
                ElseIf EtapeActuelle = "ExamDetail" Then
                    TxtTypeExamen.Text = "EXAMEN POST QUALIFICATION"
                End If
            End If
        End If
    End Sub

    Private Sub GetTerminerModifExamen()
        TxtTypeExamen.ForeColor = Color.Black
        BtExecuter.Text = "RAPPORT"
        PanelLots.Enabled = False
        BtExecuter.Enabled = True
        BtRapportEval.Enabled = True
        BtPVAttribution.Enabled = True

        'Rapport d'évaluation validé par la bailleur.
        If EtatRapportEvalOffres.ToString = "Valider" Then
            BtEtatMarche.Enabled = True
            btnModifAnalyse.Enabled = False
            'Marché editer
            If Val(ExecuteScallar("select count(*) from t_marchesigne where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'")) > 0 Then
                BtImpMarche.Enabled = True
            Else
                BtImpMarche.Enabled = False
            End If
        Else
            btnModifAnalyse.Enabled = True
        End If
    End Sub

    Private Sub DesactiverBouton(value As Boolean)
        BtRapportEval.Enabled = value
        BtPVAttribution.Enabled = value
        BtEtatMarche.Enabled = value
        BtImpMarche.Enabled = value
    End Sub

    Private Function GetUpdateExamenPreleminaire() As Boolean
        Dim AcceptExam As String
        Dim ConformTech As String
        Dim Verif As String
        Dim ConformPro As String
        Dim ConformGarant As String
        Dim ExausTiOffre As String
        Dim ConforEssent As String

        Dim ListeCodeFour As New ArrayList
        Dim ExisteCodFour As Boolean = False

        query = "select S.* from T_Fournisseur as F, T_SoumissionFournisseur as S where S.CodeFournis=F.CodeFournis and F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "'"
        Dim dt1 = ExcecuteSelectQuery(query)
        For Each rw In dt1.Rows
            ExisteCodFour = False
            If ListeCodeFour.Count = 0 Then
                ListeCodeFour.Add(rw("CodeFournis"))
            Else
                For j = 0 To ListeCodeFour.Count - 1
                    If ListeCodeFour(j) = rw("CodeFournis") Then
                        ExisteCodFour = True
                    End If
                Next
                If ExisteCodFour = False Then
                    ListeCodeFour.Add(rw("CodeFournis"))
                End If
            End If

            AcceptExam = "OUI"
            ConformTech = "OUI"
            Verif = "OUI"
            ConformPro = "OUI"
            ConformGarant = "OUI"
            ExausTiOffre = "OUI"
            ConforEssent = "OUI"

            query = "SELECT N.Verification, N.ConformiteProvenance, N.ConformiteGarantie,N.AcceptationExamDetaille, N.ExhaustiviteOffre, N.ConformiteEssentiel, N.ConformiteTechnique from T_Fournisseur as F,T_SoumissionFournisseur as S, t_soumissionfournisseur_cojo as N where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and F.CodeProjet='" & ProjetEnCours & "' AND S.RefSoumis=N.RefSoumis and S.RefSoumis='" & rw("RefSoumis").ToString & "'"
            Dim dt2 = ExcecuteSelectQuery(query)
            For Each rw1 In dt2.Rows
                If rw1("AcceptationExamDetaille") = "NON" Then
                    AcceptExam = "NON"
                End If
                If rw1("ConformiteTechnique") = "NON" Then
                    ConformTech = "NON"
                End If
                If rw1("Verification") = "NON" Then
                    Verif = "NON"
                End If
                If rw1("ConformiteProvenance") = "NON" Then
                    ConformPro = "NON"
                End If
                If rw1("ConformiteGarantie") = "NON" Then
                    ConformGarant = "NON"
                End If
                If rw1("ExhaustiviteOffre") = "NON" Then
                    ExausTiOffre = "NON"
                End If
                If rw1("ConformiteEssentiel") = "NON" Then
                    ConforEssent = "NON"
                End If
            Next
            ExecuteNonQuery("UPDATE T_SoumissionFournisseur SET AcceptationExamDetaille ='" & AcceptExam & "', ConformiteTechnique='" & ConformTech & "',Verification='" & Verif & "',ConformiteProvenance='" & ConformPro & "',ConformiteGarantie='" & ConformGarant & "',ExhaustiviteOffre='" & ExausTiOffre & "',ConformiteEssentiel='" & ConforEssent & "'  WHERE RefSoumis='" & rw("RefSoumis").ToString & "'")
        Next

        'Verfier si le soummissionnaire respect tout les critères demandé
        Dim rWlisteLot As DataTable = ExcecuteSelectQuery("select * from t_lotdao where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "'")

        Dim AceptExamen As Boolean = True
        Dim NombreDisqualifier As Integer = 0
        Dim NombreSoumission As Integer = 0
        For k = 0 To ListeCodeFour.Count - 1 'Liste Code fournisseur
            For Each rwLot As DataRow In rWlisteLot.Rows 'Liste Lot
                Dim dt3 As DataTable = ExcecuteSelectQuery("select S.AcceptationExamDetaille from T_Fournisseur as F, T_SoumissionFournisseur as S where S.CodeFournis=F.CodeFournis and F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and F.CodeProjet='" & ProjetEnCours & "' and S.RefLot='" & rwLot("RefLot") & "' and S.CodeFournis='" & ListeCodeFour(k) & "'") '("CodeFournis") &
                AceptExamen = True

                If dt3.Rows.Count > 0 Then
                    NombreSoumission += 1 'Nbre soumissionnaire
                    For Each rw3 As DataRow In dt3.Rows
                        If rw3("AcceptationExamDetaille").ToString = "NON" Then
                            AceptExamen = False
                        End If
                    Next

                    'Il ne respect pas tous les critères demandés pour le lot
                    'Disqualifier pour le lot
                    If AceptExamen = False Then
                        NombreDisqualifier += 1
                        ExecuteNonQuery("UPDATE T_SoumissionFournisseur SET AcceptationExamDetaille='NON' WHERE RefLot='" & rwLot("RefLot") & "' and CodeFournis='" & ListeCodeFour(k) & "'") 'CodeFour("CodeFournis")
                    End If
                End If
            Next
        Next
        Return IIf(NombreSoumission = NombreDisqualifier, True, False).ToString
    End Function
#End Region

#Region "Bouton debut"

    Private Sub BtExecuter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtExecuter.Click
        '  If modif = False Then
        Try

            If BtExecuter.Text.Contains("Début") = True Then

                If EtapeActuelle = "ExamPrelim" Then
                    BtExecuter.Text = "Fin examen" & vbNewLine & "detaillé"
                ElseIf EtapeActuelle = "ExamDetail" Then
                    TxtTypeExamen.Text = "EXAMEN POST QUALIFICATION"
                    TxtTypeExamen.ForeColor = Color.Black
                    ChargerExamPostQualif()
                    BtExecuter.Text = "Fin examen" & vbNewLine & "post qualification"
                End If

                If EtapeActuelle = "ExamPrelim" Or EtapeActuelle = "ExamDetail" Then
                    LabelSoumis.Enabled = False
                    CmbSoumis.Enabled = False
                    TxtAdresseSoumis.Enabled = False
                    PanelLots.Enabled = True
                    CmbNumLot.Enabled = True
                    TxtLibelleLot.Enabled = True
                Else
                    LabelSoumis.Enabled = False
                    CmbSoumis.Enabled = False
                    TxtAdresseSoumis.Enabled = False
                    CmbNumLot.Enabled = False
                    TxtLibelleLot.Enabled = False
                End If

            ElseIf (BtExecuter.Text = "RAPPORT") Then
                Try
                    DebutChargement(True, "Chargement du rapport d'évaluation en cours...")
                    If RapportEvaluation() = False Then
                        FinChargement()
                        Exit Sub
                    End If

                    ' BtExecuter.Text = "IMPRIMER"
                    PanelLots.Enabled = False
                    FinChargement()
                Catch ex As Exception
                    FinChargement()
                    FailMsg(ex.ToString)
                End Try

            ElseIf (BtExecuter.Text = "IMPRIMER") Then

                DebutChargement(True, "Impression du rapport d'évaluation en cours...")
                Dim NomDossier As String = line & "\DAO\" & TxtTypeMarche.Text & "\" & TxtMethode.Text & "\" & FormatFileName(CmbNumDoss.Text, "") & "\RaportEvaluation"
                If Not File.Exists(NomDossier.ToString & "\" & CheminRaportEvalPdf.ToString) Then
                    FinChargement()
                    ConsoliderRapportEvaluation()
                End If
                Try
                    Dim printer As New Process
                    printer.StartInfo.Verb = "Print"
                    printer.StartInfo.FileName = NomDossier.ToString & "\" & CheminRaportEvalPdf.ToString
                    printer.StartInfo.CreateNoWindow = True
                    FinChargement()
                    printer.Start()
                Catch ex As Exception
                    FinChargement()
                    FailMsg(ex.ToString)
                End Try
                PanelLots.Enabled = False
                FinChargement()

            Else 'Fin...... *********

                'If (EtapeActuelle = "") Then
                '    'Code pour la fin de la verification *****************
                '    Dim AnalTerminee As Boolean = True
                '    query = "select * from T_Fournisseur as F,T_SoumissionFournisseur as S where S.CodeFournis=F.CodeFournis and F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and S.ConformiteTechnique=''"
                '    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                '    For Each rw As DataRow In dt0.Rows
                '        AnalTerminee = False
                '        Exit For
                '    Next

                '    If (AnalTerminee = True) Then
                '        query = "update T_DAO set AnalyseOffres='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
                '        ExecuteNonQuery(query)
                '        CmbNumDoss_SelectedIndexChanged(Me, e)
                '        BtExecuter.Text = "DEBUT"
                '        PanelVerdict.Enabled = False
                '    Else
                '        SuccesMsg("Vérification inachevée!.")
                '        Exit Sub
                '    End If

                'ElseIf (EtapeActuelle = "Analyse") Then

                '    Dim ExamComplet As Boolean = True
                '    query = "select * from T_Fournisseur as F,T_SoumissionFournisseur as S where S.CodeFournis=F.CodeFournis and F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and S.ConformiteTechnique='OUI' and S.AcceptationExamDetaille=''"
                '    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                '    For Each rw As DataRow In dt0.Rows
                '        ExamComplet = False
                '        Exit For
                '    Next

                '    If (ExamComplet = True) Then
                '        If ConfirmMsg("Voulez-vous valider l'examen préliminaire?.") = DialogResult.Yes Then
                '            ExecuteNonQuery("update T_DAO set ExamPrelimOffres='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'")
                '            SuccesMsg("Traitement effectué avec succès!.")
                '            dtExam.Columns.Clear()
                '            dtExam.Rows.Clear()
                '            GridTravail.Refresh()
                '            TxtLibelleLot.Text = ""
                '            CmbNumLot.Text = ""
                '            CmbNumDoss_SelectedIndexChanged(Me, e)
                '            BtExecuter.Text = "DEBUT"
                '        End If
                '    Else
                '        SuccesMsg("Tous les soumissionnaires n'ont pas été examinés!.")
                '        Exit Sub
                '    End If

                If (EtapeActuelle = "ExamPrelim") Then
                    query = "select COUNT(*) from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and F.CodeProjet='" & ProjetEnCours & "' and S.AcceptationExamDetaille='OUI' and PrixCorrigeOffre IS NULL"
                    If Val(ExecuteScallar(query)) > 0 Then
                        SuccesMsg("Tous les soumissionnaires n'ont pas été examinés!")
                        Exit Sub
                    End If

                    'Cloture de l'examen ExamDetail
                    'Pour personnaliser le message
                    Dim Message1 As String = ""
                    Dim Message2 As String = ""
                    If modif = False Then
                        Message1 = "Voulez-vous terminer l'examen détaillé?"
                        Message2 = "La clôture de l'examen detaillé" & vbNewLine & "a été effectuée avec succès."
                    Else
                        Message1 = "Voulez-vous terminer la modification de l'examen détaillée?"
                        Message2 = "La clôture de la modification de l'examen" & vbNewLine & "detaillé a été effectuée avec succès."
                    End If

                    If ConfirmMsg(Message1.ToString) = DialogResult.Yes Then
                        DebutChargement(True, "Clôture de l'examen detaillé en cours...")
                        If Classement() = 0 Then
                            FinChargement()
                            Exit Sub
                        End If

                        ExecuteNonQuery("update T_DAO set ExamDetailOffres='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', ExamPostQualifOffres=NULL where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'")
                        OffresTraitees()
                        FinChargement()

                        SuccesMsg(Message2.ToString)

                        DebutChargement(True, "Chargement des données traités en cours...")
                        dtExam.Columns.Clear()
                        dtExam.Rows.Clear()
                        GridTravail.Refresh()
                        EtapeExamDetail.ImageIndex = 0
                        EtapeExamDetail.ForeColor = Color.Black
                        ChargerExamDetaille(True)
                        EtapeActuelle = "ExamDetail"

                        'Cas de modification et il n'existe pas de critère pos qualification
                        If ExisteDesCriterePostQualifications = False And modif = True Then
                            CmbNumDoss.Enabled = True
                            modif = False
                            btnModifAnalyse.Text = "MODIFIER L'ANALYSE"
                            ModificationAnalyseNonTerminer = False
                        End If
                        FinChargement()
                    End If

                ElseIf (EtapeActuelle = "ExamDetail") Then
                    'Cloture de l'examen post qualification
                    query = "select COUNT(*) from t_soumissionfournisseurclassement where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and RangExamDetaille<>'' and ExamPQValide is NULL"
                    If Val(ExecuteScallar(query)) > 0 Then
                        SuccesMsg("Tous les soumissionnaires n'ont pas été examinés!")
                        Exit Sub
                    End If

                    Dim Message1 As String = ""
                    Dim Message2 As String = ""
                    If modif = False Then
                        Message1 = "Voulez-vous terminer l'examen post qualification?"
                        Message2 = "La clôture de l'examen post qualification" & vbNewLine & "a été effectuée avec succès."
                    Else
                        Message1 = "Voulez-vous terminer la modification" & vbNewLine & "de l'examen post qualification?"
                        Message2 = "La clôture de la modification de l'examen post" & vbNewLine & "qualification a été effectuée avec succès."
                    End If

                    If ConfirmMsg(Message1.ToString) = DialogResult.Yes Then
                        DebutChargement(True, "Clôture de l'examen post qualification en cours...")
                        If ClassementPostQualif() = False Then
                            FinChargement()
                            Exit Sub
                        End If
                        ExecuteNonQuery("update T_DAO set ExamPostQualifOffres='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'")
                        OffresTraitees()
                        FinChargement()

                        SuccesMsg(Message2.ToString)

                        DebutChargement(True, "Chargement des données traités en cours...")
                        dtExam.Columns.Clear()
                        dtExam.Rows.Clear()
                        GridTravail.Refresh()
                        TxtLibelleLot.Text = ""
                        TxtLibelleSousLot.Text = ""
                        cmbSousLot.Text = ""
                        CmbNumLot.Text = ""

                        GetTerminerModifExamen()

                        EtapeExamDetail.ImageIndex = 0
                        EtapeExamDetail.ForeColor = Color.Black
                        EtapeExamPost.ImageIndex = 0
                        EtapeExamPost.ForeColor = Color.Black
                        EtapeActuelle = "ExamPost"
                        TxtTypeExamen.Text = "BILAN DU JUGEMENT DES OFFRES"

                        BilanExamOffres()

                        If modif = True Then
                            CmbNumDoss.Enabled = True
                            modif = False
                            btnModifAnalyse.Text = "MODIFIER L'ANALYSE"
                            ModificationAnalyseNonTerminer = False
                        End If
                        FinChargement()

                    End If
                End If
            End If

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

#End Region

#Region "Lot **** Sous lot *** Soumissionnaires"
    Private Sub CmbNumLot_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumLot.SelectedValueChanged
        If CmbNumLot.SelectedIndex <> -1 Then
            dtExam.Rows.Clear()
            cmbSousLot.Text = ""
            cmbSousLot.Properties.Items.Clear()
            TxtLibelleSousLot.Text = ""
            cmbSousLot.Enabled = False
            TxtLibelleSousLot.Enabled = False
            TxtAdresseSoumis.Text = ""
            CmbSoumis.Text = ""

            query = "select LibelleLot,RefLot from T_LotDAO where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and CodeLot='" & CmbNumLot.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtLibelleLot.Text = MettreApost(rw("LibelleLot").ToString)
                TxtRefLot.Text = rw("RefLot").ToString
            Next

            Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDoss.Text)
            Dim nbsouslot As Integer = Val(Resultat(0))

            If nbsouslot > 0 Then
                cmbSousLot.Enabled = True
                TxtLibelleSousLot.Enabled = True
                Dim dt As DataTable = CType(Resultat(1), DataTable)
                For Each rw As DataRow In dt.Rows
                    cmbSousLot.Properties.Items.Add(rw("CodeSousLot").ToString)
                Next

                'If AttributionMarche = "Lot" Then
                '    cmbSousLot.Enabled = False
                'Else
                '    cmbSousLot.Enabled = True
                'End If
            Else
                'ChargerSoumis(EtapeActuelle)
            End If

            If EtapeActuelle = "ExamPrelim" Then
                If nbsouslot = 0 Then
                    ChargerGridExam(EtapeActuelle)
                End If
            ElseIf EtapeActuelle = "ExamDetail" Then
                ChargerGridExam(EtapeActuelle)
                cmbSousLot.Enabled = False
                TxtLibelleSousLot.Enabled = False
            End If
        End If
    End Sub

    Private Sub cmbSousLot_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbSousLot.SelectedValueChanged
        If cmbSousLot.SelectedIndex <> -1 And CmbNumLot.SelectedIndex <> -1 Then
            dtExam.Rows.Clear()
            CmbSoumis.Text = ""
            TxtAdresseSoumis.Text = ""
            query = "select * from t_lotdao_souslot where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and RefLot='" & TxtRefLot.Text & "' and CodeSousLot='" & cmbSousLot.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtLibelleSousLot.Text = MettreApost(rw("LibelleSousLot").ToString)
                TxtRefSousLot.Text = rw("CodeSousLot").ToString
                ' ChargerSoumis(EtapeActuelle)
            Next
            ChargerGridExam(EtapeActuelle)
        End If
    End Sub
#End Region

#Region "Contexte MenuScript"

    Private Sub ExaminerToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExaminerToolStripMenuItem.Click
        'If (EtapeActuelle = "Analyse") Then

        '    If (ViewPrelim.RowCount > 0) Then
        '        DrX = ViewPrelim.GetDataRow(ViewPrelim.FocusedRowHandle)
        '        CodeActuel = DrX(0).ToString
        '        ReponseDialog = CodeActuel
        '        SpecDemande = DrX(6).ToString
        '        ExceptRevue = DrX(1).ToString
        '        ExamenDetaille.ShowDialog()
        '        If (ReponseDialog = "") Then
        '            ChargerGridExam(EtapeActuelle)
        '            OffresTraitees()
        '        End If
        '    End If

        If (EtapeActuelle = "ExamDetail") Then
            If (GridPostFinal.RowCount > 0) Then
                Dim Index As Integer = GridPostFinal.CurrentRow.Index
                If GridPostFinal.Rows.Item(Index).Cells("CodeRef").ToString <> "" Then
                    CodeActuel = GridPostFinal.Rows.Item(Index).Cells("CodeRef").Value 'CodeFournis
                    ReponseDialog = CodeActuel
                    ExceptRevue = GridPostFinal.Rows.Item(Index).Cells("Soumissionnaire").Value.ToString 'Soumissionnaire
                    Dim NewExamenPostQualificat As New ExamPostQualif
                    NewExamenPostQualificat.ShowDialog()

                    'If (ReponseDialog = "") Then
                    ' ChargerExamPostQualif()
                    ' OffresTraitees()
                    'End If
                End If
            End If
        End If
    End Sub

    Private Sub CalculerToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CalculerToolStripMenuItem.Click
        If (ViewDetail.RowCount > 0) Then
            If (EtapeActuelle = "ExamPrelim") Then
                DrX = ViewDetail.GetDataRow(ViewDetail.FocusedRowHandle)
                If DrX("CodeRef").ToString <> "" Then
                    CodeActuel = DrX("CodeRef").ToString
                    ReponseDialog = CodeActuel
                    ExceptRevue = DrX("Soumissionnaire").ToString
                    Dim GetNewCalculer As New CalculDetaille
                    GetNewCalculer.ShowDialog()

                    If (ReponseDialog = "") Then
                        If modif = True Then ModificationAnalyseNonTerminer = True
                        ' ChargerGridExam(EtapeActuelle)
                        ' OffresTraitees()
                    End If
                End If
            End If
        End If
    End Sub


    Private Sub DéciderToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DéciderToolStripMenuItem.Click
        If (TxtTypeMarche.Text = "Fournitures") Then
            Dim vMin As Decimal = 10000000000000
            Dim vMax As Decimal = 0
            Dim vMoy As Decimal = 0
            Dim coef As Decimal = 0

            Dim PosRow As Decimal = ViewTravail.GetDataSourceRowIndex(ViewTravail.FocusedRowHandle)
            Dim ChapRub() As String = dtExam.Rows(PosRow).Item(1).ToString.Split(":"c)
            Dim CodeRub As String = ChapRub(0).Replace(" ", "")
            Dim RefRubrq As String = ""

            query = "select P.PrixUnitaire,T.RefSpecFournit from T_SpecTechFourniture as T,T_SoumisPrixFourniture as P where P.RefSpecFournit=T.RefSpecFournit and T.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and T.CodeFournit='" & CodeRub & "' and P.PrixUnitaire<>'' and P.RefSoumis<>'" & TxtRefSoumis.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                RefRubrq = rw(1).ToString

                Dim valCompare As Decimal
                If (IsNumeric(rw(0).ToString) = True) Then
                    valCompare = CDec(rw(0).ToString)
                Else
                    valCompare = 0
                End If

                If (vMin > valCompare) Then vMin = valCompare
                If (vMax < valCompare) Then vMax = valCompare
                vMoy += valCompare
                coef += 1
            Next
            vMoy = Math.Round(vMoy / coef)

            AccordCommentaire.TxtValMin.Text = AfficherMonnaie(vMin.ToString)
            AccordCommentaire.TxtValMax.Text = AfficherMonnaie(vMax.ToString)
            AccordCommentaire.TxtValMoy.Text = AfficherMonnaie(vMoy.ToString)

            ReponseDialog = EtapeActuelle
            ExceptRevue = ""
            ExceptRevue2 = ""
            AccordCommentaire.ShowDialog()

            If (ReponseDialog <> "") Then
                Dim Validation As String = ""
                For k As Integer = 0 To GridViewComJugmt.RowCount - 1
                    Dim partValid() As String = dt.rows(k).item(0).ToString.Split("("c)
                    If (k > 0) Then Validation = Validation & ";"
                    Validation = Validation & partValid(0)
                Next

                If (ExceptRevue2 = "OUI") Then
                    query = "update T_SoumisPrixFourniture set PrixUnitaire='" & AfficherMonnaie(ReponseDialog.Replace(" ", "")) & "', Decision='" & EnleverApost(Validation) & "' where RefSpecFournit='" & RefRubrq & "' and PrixUnitaire=''"
                    ExecuteNonQuery(query)
                Else
                    query = "update T_SoumisPrixFourniture set PrixUnitaire='" & AfficherMonnaie(ReponseDialog.Replace(" ", "")) & "', Decision='" & EnleverApost(Validation) & "' where RefSpecFournit='" & RefRubrq & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                    ExecuteNonQuery(query)
                End If
            End If
            ReponseDialog = ""
            ExceptRevue = ""
            ExceptRevue2 = ""
            ChargerGridExam(EtapeActuelle)
        End If

    End Sub

#End Region

#Region "Grid MouseUp"
    'Private Sub GridBilan_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridBilan.MouseUp
    '    If (ViewBilan.RowCount > 0) Then
    '        ChoixContext()
    '    Else
    '        ContextMenuStrip1.Items(3).Enabled = False
    '        ContextMenuStrip1.Items(4).Enabled = False
    '    End If
    'End Sub

    Private Sub ChoixContext()
        If (EtapeActuelle = "ExamPrelim") Then
            ContextMenuStrip1.Items(3).Enabled = True
            ContextMenuStrip1.Items(4).Enabled = False
        ElseIf EtapeActuelle = "ExamDetail" Then
            ContextMenuStrip1.Items(3).Enabled = False
            ContextMenuStrip1.Items(4).Enabled = True
        Else
            ContextMenuStrip1.Items(3).Enabled = False
            ContextMenuStrip1.Items(4).Enabled = False
        End If
    End Sub

    Private Sub GridDetail_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridDetail.MouseUp
        If (ViewDetail.RowCount > 0) Then
            ChoixContext()
        Else
            ContextMenuStrip1.Items(3).Enabled = False
            ContextMenuStrip1.Items(4).Enabled = False
        End If
    End Sub

    'Private Sub GridPrelim_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridPrelim.MouseUp
    '    If (ViewPrelim.RowCount > 0) Then
    '        ChoixContext()
    '    Else
    '        ContextMenuStrip1.Items(3).Enabled = False
    '        ContextMenuStrip1.Items(4).Enabled = False
    '    End If
    'End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        'Annulé le contexMenuScript après analyse. Non en cours de modification
        'Annulé au cas de cette condition (EtapeActuelle = "ExamDetail" And BtExecuter.Text = "DEBUT") pour qu'ont puisse mettre fin à l'analyse
        If (BtExecuter.Text.Contains("Début") And PanelLots.Enabled = False) Or (BtExecuter.Text = "RAPPORT" And PanelLots.Enabled = False) Or (BtExecuter.Text = "IMPRIMER" And PanelLots.Enabled = False) Then
            e.Cancel = True
        End If
    End Sub

    Private Sub JugementOffres_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ModificationAnalyseNonTerminer = True Then
            FailMsg("Veuillez terminer les modifications en" & vbNewLine & "cours avant de fermer le formulaire.")
            e.Cancel = True
        End If
    End Sub

    Private Sub GridPostFinal_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles GridPostFinal.CellMouseDown
        If GridPostFinal.Rows.Count > 0 Then
            If e.RowIndex <> -1 And e.ColumnIndex <> -1 Then
                If (e.Button = MouseButtons.Right) Then
                    Try
                        GridPostFinal.CurrentCell = GridPostFinal.Rows(e.RowIndex).Cells(e.ColumnIndex)
                        GridPostFinal.Rows(e.RowIndex).Selected = True
                        GridPostFinal.Focus()
                    Catch ex As Exception
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub GridPostFinal_MouseUp(sender As Object, e As MouseEventArgs) Handles GridPostFinal.MouseUp
        If (GridPostFinal.RowCount > 0) Then
            ChoixContext()
        Else
            ContextMenuStrip1.Items(3).Enabled = False
            ContextMenuStrip1.Items(4).Enabled = False
        End If
    End Sub
#End Region

#Region "Modification Analyse"
    Private Sub btnModifAnalyse_Click(sender As Object, e As EventArgs) Handles btnModifAnalyse.Click
        Try
            If btnModifAnalyse.Text = "MODIFIER L'ANALYSE" Then
                DebutChargement()
                modif = True
                TabRapportEval.Visible = False
                GbTraites.Visible = True
                FullCouverture.Visible = False
                PnlEditionMarche.Visible = False
                EtapeActuelle = "ExamPrelim"
                AfficherGrid(EtapeActuelle)
                CmbNumDoss_SelectedIndexChanged(Me, e)
                CmbNumDoss.Enabled = False
                btnModifAnalyse.Text = "ANNULER LA MODIFICATION"
                FinChargement()
            ElseIf btnModifAnalyse.Text = "ANNULER LA MODIFICATION" Then
                If ModificationAnalyseNonTerminer = True Then
                    FailMsg("Impossible d'annuler la modification en cours. Vous" & vbNewLine & "devez allez jusqu'au boût du proccessus de traitement.")
                    Exit Sub
                End If
                DebutChargement()
                modif = False
                CmbNumDoss_SelectedIndexChanged(Me, e)
                CmbNumDoss.Enabled = True
                ModificationAnalyseNonTerminer = False
                btnModifAnalyse.Text = "MODIFIER L'ANALYSE"
                FinChargement()
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub
#End Region

#Region "Rapport d'evaluation"
    Private Sub BtRapportEval_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtRapportEval.Click
        Try
            If ListeTbaleClicke(0) = False Then
                EtatsRapports.Text = ""
                GetEnebledBoutonRaport(False)
                WebBrowser3.Navigate("")
                EtatsRapports.Text = ""
                BtModifRapportClick = False

                Dim NomDossier As String = line & "\DAO\" & TxtTypeMarche.Text & "\" & TxtMethode.Text & "\" & FormatFileName(CmbNumDoss.Text, "") & "\RaportEvaluation"

                If EtatRapportEvalOffres.ToString = "Valider" And File.Exists(NomDossier.ToString & "\" & CheminRaportEvalPdf.ToString) Then 'Ficher existant et rap valider
                    DebutChargement(True, "Chargement du rapport d'évaluation en cours...")
                    WebBrowser3.Navigate(NomDossier.ToString & "\" & CheminRaportEvalPdf.ToString)
                    FinChargement()
                    GetTjrsActifBtRap(True)
                ElseIf EtatRapportEvalOffres.ToString = "" And File.Exists(NomDossier.ToString & "\" & CheminRaportEvalPdf.ToString) Then 'Ficher existant et rap non valider
                    DebutChargement(True, "Chargement du rapport d'évaluation en cours...")
                    WebBrowser3.Navigate(NomDossier.ToString & "\" & CheminRaportEvalPdf.ToString)
                    FinChargement()
                    GetEnebledBoutonRaport(True)
                ElseIf EtatRapportEvalOffres.ToString = "Valider" And Not File.Exists(NomDossier.ToString & "\" & CheminRaportEvalPdf.ToString) Then 'Regénérer
                    If ConfirmMsg("Le rapport d'évaluation n'existe plus ou a été supprimé. voulez-vous le regénérer à nouveau ?") = DialogResult.Yes Then
                        If ConsoliderRapportEvaluation() = False Then
                            Exit Sub
                        End If
                        GetTjrsActifBtRap(True)
                    End If
                Else 'Raport non générer
                    If ConsoliderRapportEvaluation() = False Then
                        Exit Sub
                    End If
                    GetEnebledBoutonRaport(True)
                End If
                EtatsRapports.Text = IIf(EtatRapportEvalOffres.ToString = "", "En cours de validation", EtatRapportEvalOffres.ToString.Replace("er", "é").ToString).ToString
                ListeTbaleClicke(0) = True
            End If

            TxtTypeExamen.Text = "RAPPORT D'EVALUATION DES OFFRES"
            AfficherGrid("ConsolidationRapportEval")
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub GetEnebledBoutonRaport(value As Boolean)
        GetValiderRap(value)
        GetTjrsActifBtRap(value)
    End Sub
    Private Sub GetValiderRap(value As Boolean)
        GeneRap.Enabled = value
        EnvoiContraBailleur.Enabled = value
        ModifImpRap.Enabled = value
        ActuaImpRap.Enabled = value
        BtValRap.Enabled = value
    End Sub

    Private Sub GetTjrsActifBtRap(value As Boolean)
        ImpRap.Enabled = value
        WordimpRapport.Enabled = value
        PdfRapport.Enabled = value
    End Sub

    Private Sub GeneRap_Click(sender As Object, e As EventArgs) Handles GeneRap.Click
        Try
            If ConsoliderRapportEvaluation() = False Then
                Exit Sub
            End If
            GetEnebledBoutonRaport(True)
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub ImpRap_Click(sender As Object, e As EventArgs) Handles ImpRap.Click
        Try
            If CheminRaportEvalPdf.ToString = "" Then
                SuccesMsg("Aucun rapport à imprimer.")
                Exit Sub
            End If

            Dim NomDossier As String = line & "\DAO\" & TxtTypeMarche.Text & "\" & TxtMethode.Text & "\" & FormatFileName(CmbNumDoss.Text, "") & "\RaportEvaluation"
            If File.Exists(NomDossier.ToString & "\" & CheminRaportEvalPdf.ToString) Then
                Dim printer As New Process
                printer.StartInfo.Verb = "Print"
                printer.StartInfo.FileName = NomDossier.ToString & "\" & CheminRaportEvalPdf.ToString
                printer.StartInfo.CreateNoWindow = True
                printer.Start()
            Else
                SuccesMsg("Le fichier à imprimer n'existe pas ou a été supprimé.")
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub EnvoiContraBailleur_Click(sender As Object, e As EventArgs) Handles EnvoiContraBailleur.Click
        Try
            If CheminRaportEvalPdf.ToString = "" Then
                SuccesMsg("Aucun rapport à envoyer au bailleur de fonds.")
                Exit Sub
            End If

            Dim NomDossier As String = line & "\DAO\" & TxtTypeMarche.Text & "\" & TxtMethode.Text & "\" & FormatFileName(CmbNumDoss.Text, "") & "\RaportEvaluation"
            If Not File.Exists(NomDossier.ToString & "\" & CheminRaportEvalPdf.ToString) Then
                SuccesMsg("Le rapport à envoyer au bailleur de fonds n'existe pas ou a été supprimé.")
                Exit Sub
            End If

            If Not File.Exists(NomDossier.ToString & "\RaportEvaluation.docx") Then
                SuccesMsg("Le format du rapport à envoyer au bailleur de fonds n'existe pas ou a été supprimé.")
                Exit Sub
            End If

            'Info de l'envoi de l'email
            If GetVerifDonneEmailBailleur(CmbNumDoss.Text) = False Then
                Exit Sub
            End If

            Dim MessageText = "Confirmez-vous l'envoi du rapport d'évaluation des offres au bailleur [ " & MettreApost(rwDossDAO.Rows(0)("InitialeBailleur").ToString) & " ] ?."
            If ConfirmMsg(MessageText) = DialogResult.Yes Then
                Dim CheminFile As String = NomDossier.ToString & "\RaportEvaluation.docx"
                Try
                    DebutChargement(True, "Envoi du rapport d'évaluation au bailleur...")

                    If EnvoiMailRapport(NomBailleurRetenuDAO, CmbNumDoss.Text, EmailDestinatauerDAO, CheminFile, EmailCoordinateurProjetDAO, EmailResponsablePMDAO, "Rapport d'évalaution des offres", "DAO") = False Then
                        FinChargement()
                        Exit Sub
                    End If
                    FinChargement()
                    SuccesMsg("Rapport envoyé avec succès.")
                Catch ep As IOException
                    FinChargement()
                    SuccesMsg("Un exemplaire du rapport est ouvert dans une autre application" & vbNewLine & "Veuillez le fermer svp.")
                Catch ex As Exception
                    FinChargement()
                    FailMsg(ex.ToString)
                End Try
            End If
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub ModifImpRap_Click(sender As Object, e As EventArgs) Handles ModifImpRap.Click
        Try
            If CheminRaportEvalPdf.ToString = "" Then
                SuccesMsg("Aucun rapport à modifier.")
                Exit Sub
            End If

            Dim NomDossier As String = line & "\DAO\" & TxtTypeMarche.Text & "\" & TxtMethode.Text & "\" & FormatFileName(CmbNumDoss.Text, "") & "\RaportEvaluation"

            If File.Exists(NomDossier.ToString & "\" & CheminRaportEvalPdf.ToString) Then
                If Not File.Exists(NomDossier.ToString & "\RaportEvaluation.docx") Then
                    SuccesMsg("Le format du rapport à modifier n'existe pas ou a été supprimé.")
                    Exit Sub
                End If
                DebutChargement(True, "Chargement du rapport d'évaluation en cours...")
                Process.Start(NomDossier.ToString & "\RaportEvaluation.docx")
                FinChargement()
                BtModifRapportClick = True
            Else
                SuccesMsg("Le rapport à modifier n'existe pas ou a été supprimé.")
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub ActuaImpRap_Click(sender As Object, e As EventArgs) Handles ActuaImpRap.Click
        Try
            If CheminRaportEvalPdf.ToString = "" Then
                SuccesMsg("Aucun rapport à actualiser.")
                Exit Sub
            End If

            Dim NomDossier As String = line & "\DAO\" & TxtTypeMarche.Text & "\" & TxtMethode.Text & "\" & FormatFileName(CmbNumDoss.Text, "") & "\RaportEvaluation"

            If Not File.Exists(NomDossier.ToString & "\" & CheminRaportEvalPdf.ToString) Then
                SuccesMsg("Le rapport à actualiser n'existe pas ou a été supprimé.")
                Exit Sub
            End If

            If Not File.Exists(NomDossier.ToString & "\RaportEvaluation.docx") Then
                SuccesMsg("Le format du rapport à actualiser n'existe pas ou a été supprimé.")
                Exit Sub
            End If
            If BtModifRapportClick = False Then
                SuccesMsg("Veuillez modifier le rapport avant d'actualiser.")
                Exit Sub
            End If

            DebutChargement(True, "Actualisation du rapport en cours...")

            If Directory.Exists(NomDossier) = False Then Directory.CreateDirectory(NomDossier)

            Dim WdApp As New Word.Application
            Dim WdDoc As New Word.Document
            Dim NewNomFichierPdf As String = "RaportEvaluation_" & FormatFileName(Now.ToString, "") & ".pdf"
            Try
                WdDoc = WdApp.Documents.Add(NomDossier.ToString & "\RaportEvaluation.docx")
                WdDoc.SaveAs2(FileName:=NomDossier.ToString & "\" & NewNomFichierPdf.ToString, FileFormat:=Word.WdSaveFormat.wdFormatPDF)
                WdDoc.Close(True)
                WdApp.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
            Catch ep As IO.IOException
                FinChargement()
                FailMsg("Un exemplaire du rapport est ouvert dans une autre application" & vbNewLine & "Veuillez le fermer svp.")
                WdDoc.Close(True)
                WdApp.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                Exit Sub
            Catch ex As Exception
                FinChargement()
                FailMsg(ex.ToString)
                WdDoc.Close(True)
                WdApp.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                Exit Sub
            End Try
            FinChargement()

            DebutChargement(True, "Chargement du rapport en cours...")
            ExecuteNonQuery("Update T_dao set CheminRapport='" & NewNomFichierPdf.ToString & "' where NumeroDAO ='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'")
            WebBrowser3.Navigate(NomDossier.ToString & "\" & NewNomFichierPdf.ToString)
            Threading.Thread.Sleep(3000)
            CheminRaportEvalPdf = NewNomFichierPdf.ToString

            BtModifRapportClick = False
            GetEnebledBoutonRaport(True)
            FinChargement()

        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub BtValRap_Click(sender As Object, e As EventArgs) Handles BtValRap.Click
        Try
            If CheminRaportEvalPdf.ToString = "" Then
                SuccesMsg("Aucun rapport à valider.")
                Exit Sub
            End If

            Dim NomDossier As String = line & "\DAO\" & TxtTypeMarche.Text & "\" & TxtMethode.Text & "\" & FormatFileName(CmbNumDoss.Text, "") & "\RaportEvaluation"

            If File.Exists(NomDossier.ToString & "\" & CheminRaportEvalPdf.ToString) Then
                If ConfirmMsg("La validation du rapport empechera sa modification" & vbNewLine & "êtes-vous sûrs de vouloir validé le contenu de ce rapport ?.") = DialogResult.Yes Then
                    ExecuteNonQuery("update t_dao set EtatRapportEvalOffre='Valider', DateFinJugement='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "'")
                    SuccesMsg("Le rapport a été validé avec succès.")
                    EtatsRapports.Text = "Validé"
                    btnModifAnalyse.Enabled = False
                    BtEtatMarche.Enabled = True
                    GetValiderRap(False)
                    GetTjrsActifBtRap(True)
                End If
            Else
                SuccesMsg("Le rapport à valider n'existe pas ou a été supprimé.")
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub PdfRapport_Click(sender As Object, e As EventArgs) Handles PdfRapport.Click
        Try
            If CheminRaportEvalPdf.ToString = "" Then
                SuccesMsg("Aucun rapport à exporter.")
                Exit Sub
            End If

            Dim NomDossier As String = line & "\DAO\" & TxtTypeMarche.Text & "\" & TxtMethode.Text & "\" & FormatFileName(CmbNumDoss.Text, "") & "\RaportEvaluation"
            If File.Exists(NomDossier.ToString & "\" & CheminRaportEvalPdf.ToString) Then
                If ExporterPDF(NomDossier.ToString & "\" & CheminRaportEvalPdf.ToString, "RapportEvaluationOffres.pdf") = False Then
                    Exit Sub
                End If
            Else
                SuccesMsg("Le fichier à exporter au format pdf n'existe pas ou a été supprimé.")
            End If

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub WordimpRapport_Click(sender As Object, e As EventArgs) Handles WordimpRapport.Click
        Try
            If CheminRaportEvalPdf.ToString = "" Then
                SuccesMsg("Aucun rapport à exporter.")
                Exit Sub
            End If

            Dim NomDossier As String = line & "\DAO\" & TxtTypeMarche.Text & "\" & TxtMethode.Text & "\" & FormatFileName(CmbNumDoss.Text, "") & "\RaportEvaluation"
            If File.Exists(NomDossier.ToString & "\" & CheminRaportEvalPdf.ToString) Then
                If ExporterWORDfOrmatDocx(NomDossier.ToString & "\RaportEvaluation.docx", "RaportEvaluation.docx") = False Then
                    Exit Sub
                End If
            Else
                SuccesMsg("Le fichier à exporter au format word n'existe pas ou a été supprimé.")
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub FullFavoris_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FullCouverture.Click

        For Each Rapport In TabRapportEval.SelectedTabPage.Controls
            If (TypeOf (Rapport) Is CrystalDecisions.Windows.Forms.CrystalReportViewer) Then
                FullScreenReport.FullView.ReportSource = Rapport.ReportSource
                FullScreenReport.Text = TabRapportEval.SelectedTabPage.Text
            End If
        Next
        FullScreenReport.ShowDialog()
    End Sub


    Private Function ConsoliderRapportEvaluation() As Boolean
        Try
            DebutChargement(True, "Génération du rapport d'évaluation en cours...")

            Dim Chemin As String = lineEtat & "\Marches\DAO\Fournitures\Evaluation\"
            If TxtTypeMarche.Text.ToLower = "Travaux".ToLower Then
                Chemin = lineEtat & "\Marches\DAO\Fournitures\Evaluation\"
            End If

            Dim reportCouv, report0, report1, report2, report3, report4, report5 As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim DatSet = New DataSet

            'Fournitures
            'Dim Methode As Boolean = False

            If TxtTypeMarche.Text.ToLower = "Fournitures".ToLower Then
                If TxtMethode.Text.ToUpper = "AOI" Or TxtMethode.Text.ToUpper = "AON" Then
                    reportCouv.Load(Chemin & "Rapport_EvaluationDAO_PageGarde.rpt")
                    report0.Load(Chemin & "Rapport_EvaluationDAO_0.rpt")
                    report1.Load(Chemin & "Rapport_EvaluationDAO_1.rpt")
                    report2.Load(Chemin & "Rapport_EvaluationDAO_2.rpt")
                    report3.Load(Chemin & "Rapport_EvaluationDAO_3.rpt")
                    report4.Load(Chemin & "Rapport_EvaluationDAO_4.rpt")
                    report5.Load(Chemin & "Rapport_EvaluationDAO_5.rpt")
                Else
                    Chemin = Chemin & "\AutreMethode\"
                    reportCouv.Load(Chemin & "PageGardeRapport.rpt") 'CodeProjet NumDAO
                    report0.Load(Chemin & "Rapport_EvaluationCotation_0.rpt") ''CodeProjet NumDAO
                    report1.Load(Chemin & "Rapport_EvaluationCotation_1.rpt") 'NumDAO
                    report2.Load(Chemin & "Rapport_EvaluationCotation_2.rpt") 'NumDAO
                    report3.Load(Chemin & "Rapport_EvaluationCotation_3.rpt") 'CodeProjet NumDAO
                    report4.Load(Chemin & "Rapport_EvaluationCotation_4.rpt") 'CodeProjet NumDAO
                    report5.Load(Chemin & "Rapport_EvaluationCotation_5.rpt") 'CodeProjet NumDAO
                End If

            Else
                FinChargement()
                FailMsg("Etat en cours de réalisation.")
                Return False
            End If

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = reportCouv.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            CrTables = report0.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            CrTables = report1.Database.Tables
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

            CrTables = report3.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            CrTables = report4.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            'If TxtTypeMarche.Text.ToLower = "Fournitures".ToLower Then
            'If TxtMethode.Text.ToUpper = "AOI" Or TxtMethode.Text.ToUpper = "AON" Then
            CrTables = report5.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next
            ' End If
            'End If
            If TxtTypeMarche.Text.ToLower = "Fournitures".ToLower Then
                If TxtMethode.Text.ToUpper <> "AOI" Or TxtMethode.Text.ToUpper <> "AON" Then
                    reportCouv.SetDataSource(DatSet)
                End If
            End If

            report0.SetDataSource(DatSet)
            report1.SetDataSource(DatSet)
            report2.SetDataSource(DatSet)
            report3.SetDataSource(DatSet)
            report4.SetDataSource(DatSet)
            report5.SetDataSource(DatSet)

            'NomProjet et ministere garde
            query = "select MinistereTutelle,NomProjet,AdresseProjet,BoitePostaleProjet,TelProjet,FaxProjet,MailProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            If dt0.Rows.Count = 0 Then
                FinChargement()
                Return False
            End If
            Dim rwProjet As DataRow = dt0.Rows(0)
            query = "select * from T_DAO where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
            dt0 = ExcecuteSelectQuery(query)
            If dt0.Rows.Count = 0 Then
                FinChargement()
                Return False
            End If
            Dim rwDAO As DataRow = dt0.Rows(0)

            If TxtTypeMarche.Text.ToLower = "Fournitures".ToLower Then
                If TxtMethode.Text.ToUpper = "AOI" Or TxtMethode.Text.ToUpper = "AON" Then

                    reportCouv.SetParameterValue("NumDao", EnleverApost(CmbNumDoss.Text))
                    reportCouv.SetParameterValue("Ministere", MettreApost(rwProjet("MinistereTutelle").ToString))
                    reportCouv.SetParameterValue("NomProjet", MettreApost(rwProjet("NomProjet").ToString).ToUpper)
                    reportCouv.SetParameterValue("CodeProjet", ProjetEnCours)
                    If rwDAO("ExamPostQualifOffres").ToString <> "" Then
                        reportCouv.SetParameterValue("DateFormatLong", CDate(Mid(rwDAO("ExamPostQualifOffres").ToString, 1, 10)).ToLongDateString.ToUpper)
                    Else
                        reportCouv.SetParameterValue("DateFormatLong", "-")
                    End If
                    reportCouv.SetParameterValue("MethodePdm", rwDAO("MethodePDM").ToString)
                    reportCouv.SetParameterValue("NbLots", rwDAO("NbreLotDAO").ToString & IIf(CDec(rwDAO("NbreLotDAO")) > 1, " lots", " lot").ToString)
                    reportCouv.SetParameterValue("DateOuverture", CDate(Mid(rwDAO("DateDebutOuverture").ToString, 1, 10)).ToLongDateString)

                    report0.SetParameterValue("CodeProjet", ProjetEnCours)
                    report4.SetParameterValue("CodeProjet", ProjetEnCours)
                    report5.SetParameterValue("CodeProjet", ProjetEnCours)

                    '***********************
                    report3.SetParameterValue("NumDao", EnleverApost(CmbNumDoss.Text))
                    report3.SetParameterValue("CodeProjet", ProjetEnCours)
                End If
            End If

            report3.SetParameterValue("NomProjet", MettreApost(rwProjet("NomProjet").ToString), "RapportEvaluationTableau1a3.rpt")
            report3.SetParameterValue("AdresseProjet", MettreApost(rwProjet("AdresseProjet").ToString), "RapportEvaluationTableau1a3.rpt")
            report3.SetParameterValue("BpProjet", rwProjet("BoitePostaleProjet").ToString, "RapportEvaluationTableau1a3.rpt")
            report3.SetParameterValue("TelProjet", rwProjet("TelProjet").ToString, "RapportEvaluationTableau1a3.rpt")
            report3.SetParameterValue("FaxProjet", rwProjet("FaxProjet").ToString, "RapportEvaluationTableau1a3.rpt")
            report3.SetParameterValue("MailProjet", rwProjet("MailProjet").ToString, "RapportEvaluationTableau1a3.rpt")

            '*****************
            'For Each rw As DataRow In dt0.Rows

            report3.SetParameterValue("Emprunteur", MettreApost(rwDAO("NomEmprunteur").ToString), "RapportEvaluationTableau1a3.rpt")
            Dim partValid() As String = IIf(rwDAO("ValiditeOffre").ToString <> "", rwDAO("ValiditeOffre").ToString.Split(" "c), {"", ""})
            report3.SetParameterValue("ValiditeOffres", IIf(partValid(1) = "Semaines", (CDec(partValid(0)) * 7).ToString & " Jours", IIf(partValid(1) = "Mois", (CDec(partValid(0)) * 30).ToString & " Jours", rwDAO("ValiditeOffre").ToString).ToString).ToString, "RapportEvaluationTableau1a3.rpt")
            report3.SetParameterValue("ExamPrealOUI", IIf(rwDAO("PreQualif").ToString = "OUI", "X", "").ToString, "RapportEvaluationTableau1a3.rpt")
            report3.SetParameterValue("ExamPrealNON", IIf(rwDAO("PreQualif").ToString = "NON", "X", "").ToString, "RapportEvaluationTableau1a3.rpt")
            report3.SetParameterValue("DatePub", rwDAO("DatePublication").ToString, "RapportEvaluationTableau1a3.rpt")
            report3.SetParameterValue("JournalPub", MettreApost(rwDAO("JournalPublication").ToString), "RapportEvaluationTableau1a3.rpt")
            report3.SetParameterValue("DateHeureDepot", IIf(rwDAO("DateReport").ToString = "", rwDAO("DateLimiteRemise").ToString.Replace(" ", " à "), rwDAO("DateReport").ToString.Replace(" ", " à ")).ToString, "RapportEvaluationTableau1a3.rpt")
            report3.SetParameterValue("CoutEstime", AfficherMonnaie(rwDAO("MontantMarche").ToString), "RapportEvaluationTableau1a3.rpt")
            report3.SetParameterValue("AON_X", IIf(rwDAO("MethodePDM").ToString = "AON", "X", "").ToString, "RapportEvaluationTableau1a3.rpt")
            report3.SetParameterValue("AOI_X", IIf(rwDAO("MethodePDM").ToString = "AOI", "X", "").ToString, "RapportEvaluationTableau1a3.rpt")
            report3.SetParameterValue("Autres_X", IIf(rwDAO("MethodePDM").ToString <> "AON" And rwDAO("MethodePDM").ToString <> "AOI", "X", "").ToString, "RapportEvaluationTableau1a3.rpt")

            'Dim partDate() As String = rwDAO("DateFinOuverture").ToString.Split(" "c)
            'Dim duree As String = rwDAO("DureeSeance").ToString
            'Dim heureOuv As DateTime = CDate(partDate(1)).AddHours(-CInt(Mid(duree, 1, 2))).AddMinutes(-CInt(Mid(duree, 4, 2))).AddSeconds(-CInt(Mid(duree, 7, 2)))
            'report3.SetParameterValue("DateHeureOuverture", partDate(0) & " à " & heureOuv.ToLongTimeString, "RapportEvaluationTableau1a3.rpt")
            report3.SetParameterValue("DateHeureOuverture", rwDAO("DateDebutOuverture").ToString.Replace(" ", " à "))

            report3.SetParameterValue("FaveurPaysOUI", "", "RapportEvaluationTableau1a3.rpt")
            report3.SetParameterValue("FaveurPaysNON", "", "RapportEvaluationTableau1a3.rpt")
            report3.SetParameterValue("MarcheForfaitOUI", IIf(TxtTypeMarche.Text = "Fournitures", "X", ""), "RapportEvaluationTableau1a3.rpt")
            report3.SetParameterValue("MarcheForfaitNON", IIf(TxtTypeMarche.Text = "Travaux", "X", ""), "RapportEvaluationTableau1a3.rpt")
            'Next

            'Données du marché *********************
            Dim CodeMarche As Decimal = 0
            Dim LeBaill As String = ""
            Dim LibMarc As String = ""
            Dim AvisGlePDM As String = ""
            Dim DateAvisNonObjBanque As String = ""

            query = "select M.RefMarche,M.DescriptionMarche,M.InitialeBailleur, P.DateApprobation, P.DateAvisGle from T_Marche as M, t_dao as D, t_ppm_marche as P where D.RefMarche=M.RefMarche and M.RefPPM=P.RefPPM and M.RefMarche='" & rwDAO("RefMarche").ToString & "' and D.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and D.CodeProjet='" & ProjetEnCours & "'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                CodeMarche = rw("RefMarche")
                LeBaill = rw("InitialeBailleur")

                If (LibMarc <> "") Then
                    LibMarc = LibMarc & vbNewLine & " et " & vbNewLine
                End If
                LibMarc = LibMarc & rw("DescriptionMarche").ToString

                DateAvisNonObjBanque = rw("DateApprobation").ToString
                AvisGlePDM = rw("DateAvisGle").ToString
            Next

            If TxtTypeMarche.Text.ToLower = "Fournitures".ToLower Then
                If TxtMethode.Text.ToUpper = "AOI" Or TxtMethode.Text.ToUpper = "AON" Then
                    reportCouv.SetParameterValue("LibelleMarche", MettreApost(LibMarc).ToUpper)
                Else
                    report3.SetParameterValue("DateAvisNonObjBanque", DateAvisNonObjBanque.ToString, "RapportEvaluationTableau1a3.rpt")
                End If
            End If

            report3.SetParameterValue("LibelleMarche", MettreApost(LibMarc), "RapportEvaluationTableau1a3.rpt")
            report3.SetParameterValue("AvisGlePDM", AvisGlePDM.ToString, "RapportEvaluationTableau1a3.rpt")

            ' La convention ****************************
            query = "select C.CodeConvention,C.TypeConvention,C.MontantConvention,C.EntreeEnVigueur,C.DateCloture from T_Convention as C, T_Bailleur as B, t_marche as M where M.Convention_ChefFile=C.CodeConvention and C.CodeBailleur=B.CodeBailleur and M.RefMarche='" & CodeMarche & "' and B.CodeProjet='" & ProjetEnCours & "'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows

                '*****************************
                If TxtTypeMarche.Text.ToLower = "Fournitures".ToLower Then
                    If TxtMethode.Text.ToUpper = "AOI" Or TxtMethode.Text.ToUpper = "AON" Then
                        reportCouv.SetParameterValue("TypeConv", MettreApost(rw("TypeConvention").ToString).ToUpper)
                        reportCouv.SetParameterValue("NumConv", rw("CodeConvention").ToString)
                        reportCouv.SetParameterValue("Bailleur", LeBaill)
                    End If
                End If

                report3.SetParameterValue("NumConv", rw("CodeConvention").ToString, "RapportEvaluationTableau1a3.rpt")
                report3.SetParameterValue("DateVigueurConv", rw("EntreeEnVigueur").ToString, "RapportEvaluationTableau1a3.rpt")
                report3.SetParameterValue("DateClotureConv", rw("DateCloture").ToString, "RapportEvaluationTableau1a3.rpt")
            Next

            'Dossiers retirés
            query = "select Count(*) from T_Fournisseur where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                report3.SetParameterValue("NbreDossierRetires", rw(0).ToString, "RapportEvaluationTableau1a3.rpt")
            Next
            'Offres recues
            query = "select Count(*) from T_Fournisseur where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "' and DateDepotDAO<>''"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                report3.SetParameterValue("NbreOffresRecues", rw(0).ToString, "RapportEvaluationTableau1a3.rpt")
            Next

            If TxtTypeMarche.Text.ToLower = "Fournitures".ToLower Then
                If TxtMethode.Text.ToUpper = "AOI" Or TxtMethode.Text.ToUpper = "AON" Then

                    report0.SetParameterValue("NumDao", EnleverApost(CmbNumDoss.Text))
                    report1.SetParameterValue("NumDao", EnleverApost(CmbNumDoss.Text))
                    report2.SetParameterValue("NumDao", EnleverApost(CmbNumDoss.Text))
                    report4.SetParameterValue("NumDao", EnleverApost(CmbNumDoss.Text))
                    report5.SetParameterValue("NumDao", EnleverApost(CmbNumDoss.Text))

                Else
                    reportCouv.SetParameterValue("CodeProjet", ProjetEnCours)
                    reportCouv.SetParameterValue("NumDAO", EnleverApost(CmbNumDoss.Text))

                    report0.SetParameterValue("NumDAO", EnleverApost(CmbNumDoss.Text))
                    report0.SetParameterValue("CodeProjet", ProjetEnCours)
                    report1.SetParameterValue("NumDAO", EnleverApost(CmbNumDoss.Text))
                    report2.SetParameterValue("NumDAO", EnleverApost(CmbNumDoss.Text))
                    report3.SetParameterValue("CodeProjet", ProjetEnCours)
                    report3.SetParameterValue("NumDAO", EnleverApost(CmbNumDoss.Text))
                    report4.SetParameterValue("CodeProjet", ProjetEnCours)
                    report4.SetParameterValue("NumDAO", EnleverApost(CmbNumDoss.Text))
                    report5.SetParameterValue("CodeProjet", ProjetEnCours)
                    report5.SetParameterValue("NumDAO", EnleverApost(CmbNumDoss.Text))
                End If

            Else '********* Travaux

            End If

            'RepCouverture.ReportSource = reportCouv
            'RepTab1a3.ReportSource = report0
            'RepTab4.ReportSource = report1
            'RepTab5.ReportSource = report2
            'RepTab6.ReportSource = report3
            'RepTab8A.ReportSource = report4
            'RepTab9.ReportSource = report5

            Dim NomRepertoire As String = Environ$("TEMP") & "\RapportEvaluation\" & FormatFileName(CmbNumDoss.Text, "")
            If Not System.IO.Directory.Exists(NomRepertoire) Then
                Directory.CreateDirectory(NomRepertoire)
            End If

            Try
                reportCouv.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, NomRepertoire & "\Couverture.doc")
                report0.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, NomRepertoire & "\report0.doc")
                report1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, NomRepertoire & "\report1.doc")
                report2.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, NomRepertoire & "\report2.doc")
                report3.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, NomRepertoire & "\report3.doc")
                report4.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, NomRepertoire & "\report4.doc")
                report5.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, NomRepertoire & "\report5.doc")

            Catch ex As Exception
                FinChargement()
                FailMsg(ex.ToString)
                Return False
            End Try

            Dim oWord As New Word.Application
            Dim currentDoc As New Word.Document
            Dim NomDossier As String = ""
            Dim CheminRappPdf As String = ""

            Try
                Dim NomPageGarde As String = NomRepertoire & "\Couverture.doc"
                Dim rapport0 As String = NomRepertoire & "\report0.doc"
                Dim rapport1 As String = NomRepertoire & "\report1.doc"
                Dim rapport2 As String = NomRepertoire & "\report2.doc"
                Dim rapport3 As String = NomRepertoire & "\report3.doc"
                Dim rapport4 As String = NomRepertoire & "\report4.doc"
                Dim rapport5 As String = NomRepertoire & "\report5.doc"

                NomDossier = line & "\DAO\" & TxtTypeMarche.Text & "\" & TxtMethode.Text & "\" & FormatFileName(CmbNumDoss.Text, "") & "\RaportEvaluation"
                If Not System.IO.Directory.Exists(NomDossier) Then
                    System.IO.Directory.CreateDirectory(NomDossier)
                End If

                Dim CheminRappWord As String = NomDossier & "\RaportEvaluation.docx"
                CheminRappPdf = "RaportEvaluation_" & FormatFileName(Now.ToString, "") & ".pdf"

                'Ajout de la page de garde
                currentDoc = oWord.Documents.Add(NomPageGarde)
                Dim myRange As Word.Range = currentDoc.Bookmarks.Item("\endofdoc").Range
                Dim mySection1 As Word.Section = AjouterNouvelleSectionDocument(currentDoc, myRange)
                'Ajout du rapport 0
                myRange.InsertFile(rapport0)
                'Ajout du rapport 1
                mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                'mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                myRange.InsertFile(rapport1)
                'Ajout du rapport 2
                mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                myRange.InsertFile(rapport2)
                'Ajout du rapport 3
                mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                myRange.InsertFile(rapport3)
                'Ajout du rapport 4
                mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                myRange.InsertFile(rapport4)
                mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                myRange.InsertFile(rapport5)

                currentDoc.SaveAs2(FileName:=CheminRappWord.ToString, FileFormat:=Word.WdSaveFormat.wdFormatDocumentDefault)
                currentDoc.SaveAs2(FileName:=NomDossier & "\" & CheminRappPdf.ToString, FileFormat:=Word.WdSaveFormat.wdFormatPDF)
                currentDoc.Close(True)
                oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)

            Catch exp As IOException
                FinChargement()
                FailMsg("Un exemplaire du rapport est ouvert dans une autre application" & vbNewLine & "Veuillez le fermer svp.")
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

            FinChargement()
            DebutChargement(True, "Chargement du rapport en cours...")
            ExecuteNonQuery("Update T_dao set CheminRapport='" & CheminRappPdf.ToString & "' where NumeroDAO ='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'")
            WebBrowser3.Navigate(NomDossier.ToString & "\" & CheminRappPdf.ToString)
            Threading.Thread.Sleep(3000)
            CheminRaportEvalPdf = CheminRappPdf.ToString
            FinChargement()
            Return True
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
            Return False
        End Try
    End Function
#End Region

#Region "Afficharge Rapport en detaille"

    Private Sub GetPageRapportVisible(value As Boolean)
        TabRapportEval.Visible = value
        Tableau1a3.PageVisible = value
        Tableau4.PageVisible = value
        Tableau5.PageVisible = value
        Tableau6.PageVisible = value
        Tableau8A.PageVisible = value
        Tableau9.PageVisible = value
        Rang1.PageVisible = value
        Rang2.PageVisible = ExisteDesCriterePostQualifications
        PostQualif.PageVisible = ExisteDesCriterePostQualifications
        Proposition.PageVisible = value
    End Sub

    Private Function RapportEvaluation(Optional ByVal Traitement As String = "Afficher") As Boolean
        Try
            AfficherGrid("Rapport en detaille")
            GetPageRapportVisible(True)
            Couverture.Text = "Couverture"
            TxtTypeExamen.Text = "RAPPORT D'EVALUATION ET PROPOSITION DE MARCHE"

            Dim Chemin As String = lineEtat & "\Marches\DAO\Fournitures\Evaluation\Sous_Rapport\"

            If TxtTypeMarche.Text.ToLower = "Travaux".ToLower Then
                Chemin = lineEtat & "\Marches\DAO\Fournitures\Evaluation\Sous_Rapport\"
            End If

            Dim reportCouv, reportTab1a3, reportTab4, reportTab5, reportTab6, reportTab8A, reportTab9, reportRang1, reportPost, reportRang2, reportFavoris, reportProposition As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim DatSet = New DataSet

            If TxtTypeMarche.Text.ToLower = "Fournitures".ToLower Then
                If TxtMethode.Text.ToUpper = "AOI" Or TxtMethode.Text.ToUpper = "AON" Then
                    reportCouv.Load(Chemin & "RapportEvaluationPageGarde.rpt")
                    reportTab1a3.Load(Chemin & "RapportEvaluationTableau1a3.rpt")
                    reportTab4.Load(Chemin & "RapportEvaluationTableau4.rpt")
                    reportTab5.Load(Chemin & "RapportEvaluationTableau5.rpt")
                    reportTab6.Load(Chemin & "RapportEvaluationTableau6.rpt")
                    reportTab8A.Load(Chemin & "RapportEvaluationTableau8A.rpt")
                    reportTab9.Load(Chemin & "RapportEvaluationTableau9.rpt")
                    reportRang1.Load(Chemin & "RapportEvaluationClassement1.rpt")
                    reportPost.Load(Chemin & "RapportEvaluationPostQualif.rpt")
                    reportRang2.Load(Chemin & "RapportEvaluationClassement2.rpt")
                    reportProposition.Load(Chemin & "RapportEvaluationProposition.rpt")
                    reportFavoris.Load(Chemin & "RapportEvaluationFavoris.rpt")

                Else ' **** Les autres méthode ** PSL, PSO, PSC
                    Chemin = lineEtat & "\Marches\DAO\Fournitures\Evaluation\AutreMethode\SousRapport\"

                    reportCouv.Load(Chemin & "PageGardeRapport.rpt") ' NumDAO CodeProjet
                    reportTab1a3.Load(Chemin & "RapportEvaluationTableau1a3.rpt") ' meme chose
                    reportTab4.Load(Chemin & "RapportEvaluationTableau4.rpt") ' NumDaoTab4
                    reportTab5.Load(Chemin & "RapportEvaluationTableau5.rpt") ' NumDaoTab5
                    reportTab6.Load(Chemin & "RapportEvaluationTableau6.rpt") ' NumDaoTab6
                    reportTab8A.Load(Chemin & "RapportEvaluationTableau8A.rpt") 'NumDaoTab8A
                    reportTab9.Load(Chemin & "RapportEvaluationTableau9.rpt") ' NumDaoTab9
                    reportRang1.Load(Chemin & "RapportEvaluationClassement1.rpt") ' NumDaoClass1
                    reportPost.Load(Chemin & "RapportEvaluationPostQualif.rpt") ' NumDaoPost
                    reportRang2.Load(Chemin & "RapportEvaluationClassement2.rpt") ' NumDaoClass2
                    reportProposition.Load(Chemin & "RapportEvaluationProposition.rpt") ' NumDaoPropo
                End If

            Else ' **************** Travaux 
                FinChargement()
                FailMsg("Etat en cours de réalisation.")
                Return False
            End If

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = reportCouv.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            CrTables = reportTab1a3.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            CrTables = reportTab4.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            CrTables = reportTab5.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            CrTables = reportTab6.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            CrTables = reportTab8A.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            CrTables = reportTab9.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            CrTables = reportRang1.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            CrTables = reportPost.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            CrTables = reportRang2.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            CrTables = reportProposition.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            'CrTables = reportFavoris.Database.Tables
            'For Each CrTable In CrTables
            '    crtableLogoninfo = CrTable.LogOnInfo
            '    crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '    CrTable.ApplyLogOnInfo(crtableLogoninfo)
            'Next

            reportCouv.SetDataSource(DatSet)
            'reportTab1a3.SetDataSource(DatSet)
            reportTab4.SetDataSource(DatSet)
            reportTab5.SetDataSource(DatSet)
            reportTab6.SetDataSource(DatSet)
            reportTab8A.SetDataSource(DatSet)
            reportTab9.SetDataSource(DatSet)
            reportRang1.SetDataSource(DatSet)
            reportPost.SetDataSource(DatSet)
            reportRang2.SetDataSource(DatSet)
            reportProposition.SetDataSource(DatSet)
            'reportFavoris.SetDataSource(DatSet)

            'NomProjet et ministere garde
            query = "select MinistereTutelle,NomProjet,AdresseProjet,BoitePostaleProjet,TelProjet,FaxProjet,MailProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            If dt0.Rows.Count = 0 Then
                FinChargement()
                Return False
            End If
            Dim rwProjet As DataRow = dt0.Rows(0)

            query = "select * from T_DAO where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
            dt0 = ExcecuteSelectQuery(query)
            If dt0.Rows.Count = 0 Then
                FinChargement()
                Return False
            End If
            Dim rwDao As DataRow = dt0.Rows(0)

            'For Each rw As DataRow In dt0.Rows

            reportCouv.SetParameterValue("CodeProjet", ProjetEnCours)

            If TxtTypeMarche.Text.ToLower = "Fournitures".ToLower Then
                If TxtMethode.Text.ToUpper = "AOI" Or TxtMethode.Text.ToUpper = "AON" Then
                    reportCouv.SetParameterValue("NumDao", EnleverApost(CmbNumDoss.Text))
                    reportCouv.SetParameterValue("Ministere", MettreApost(rwProjet("MinistereTutelle").ToString))
                    reportCouv.SetParameterValue("NomProjet", MettreApost(rwProjet("NomProjet").ToString).ToUpper)

                    '**********************
                    reportCouv.SetParameterValue("MethodePdm", rwDao("MethodePDM").ToString)
                    reportCouv.SetParameterValue("NbLots", rwDao("NbreLotDAO").ToString & IIf(CDec(rwDao("NbreLotDAO")) > 1, " lots", " lot").ToString)
                    reportCouv.SetParameterValue("DateOuverture", CDate(Mid(rwDao("DateDebutOuverture").ToString, 1, 10)).ToLongDateString)
                    If rwDao("ExamPostQualifOffres").ToString = "" Then
                        reportCouv.SetParameterValue("DateFormatLong", "-")
                    Else
                        reportCouv.SetParameterValue("DateFormatLong", CDate(Mid(rwDao("ExamPostQualifOffres").ToString, 1, 10)).ToLongDateString.ToUpper)
                    End If

                Else '************************
                    reportCouv.SetParameterValue("NumDAO", EnleverApost(CmbNumDoss.Text))
                End If

            Else ' Travaux *******************************
            End If

            '*****************************
            reportTab1a3.SetParameterValue("NomProjet", MettreApost(rwProjet("NomProjet").ToString))
            reportTab1a3.SetParameterValue("CodeProjet", ProjetEnCours)
            reportTab1a3.SetParameterValue("AdresseProjet", MettreApost(rwProjet("AdresseProjet").ToString))
            reportTab1a3.SetParameterValue("BpProjet", rwProjet("BoitePostaleProjet").ToString)
            reportTab1a3.SetParameterValue("TelProjet", rwProjet("TelProjet").ToString)
            reportTab1a3.SetParameterValue("FaxProjet", rwProjet("FaxProjet").ToString)
            reportTab1a3.SetParameterValue("MailProjet", rwProjet("MailProjet").ToString)

            '*****************

            reportTab1a3.SetParameterValue("Emprunteur", MettreApost(rwDao("NomEmprunteur").ToString))
            Dim partValid() As String = IIf(rwDao("ValiditeOffre").ToString <> "", rwDao("ValiditeOffre").ToString.Split(" "c), {"", ""})
            reportTab1a3.SetParameterValue("ValiditeOffres", IIf(partValid(1) = "Semaines", (CDec(partValid(0)) * 7).ToString & " Jours", IIf(partValid(1) = "Mois", (CDec(partValid(0)) * 30).ToString & " Jours", rwDao("ValiditeOffre").ToString).ToString).ToString)
            reportTab1a3.SetParameterValue("ExamPrealOUI", IIf(rwDao("PreQualif").ToString = "OUI", "X", "").ToString)
            reportTab1a3.SetParameterValue("ExamPrealNON", IIf(rwDao("PreQualif").ToString = "NON", "X", "").ToString)
            reportTab1a3.SetParameterValue("DatePub", rwDao("DatePublication").ToString)
            reportTab1a3.SetParameterValue("JournalPub", MettreApost(rwDao("JournalPublication").ToString))
            reportTab1a3.SetParameterValue("DateHeureDepot", IIf(rwDao("DateReport").ToString = "", rwDao("DateLimiteRemise").ToString.Replace(" ", " à "), rwDao("DateReport").ToString.Replace(" ", " à ")).ToString)
            reportTab1a3.SetParameterValue("CoutEstime", AfficherMonnaie(rwDao("MontantMarche").ToString))
            reportTab1a3.SetParameterValue("AON_X", IIf(rwDao("MethodePDM").ToString = "AON", "X", "").ToString)
            reportTab1a3.SetParameterValue("AOI_X", IIf(rwDao("MethodePDM").ToString = "AOI", "X", "").ToString)
            reportTab1a3.SetParameterValue("Autres_X", IIf(rwDao("MethodePDM").ToString <> "AON" And rwDao("MethodePDM").ToString <> "AOI", "X", "").ToString)

            ' Dim partDate() As String = rwDao("DateFinOuverture").ToString.Split(" "c)
            'Dim duree As String = rwDao("DureeSeance").ToString
            'Dim heureOuv As DateTime = CDate(partDate(1)).AddHours(-CInt(Mid(duree, 1, 2))).AddMinutes(-CInt(Mid(duree, 4, 2))).AddSeconds(-CInt(Mid(duree, 7, 2)))

            'reportTab1a3.SetParameterValue("DateHeureOuverture", partDate(0) & " à " & heureOuv.ToLongTimeString)
            reportTab1a3.SetParameterValue("DateHeureOuverture", rwDao("DateDebutOuverture").ToString.Replace(" ", " à "))
            reportTab1a3.SetParameterValue("NumDao", EnleverApost(CmbNumDoss.Text))
            reportTab1a3.SetParameterValue("FaveurPaysOUI", "")
            reportTab1a3.SetParameterValue("FaveurPaysNON", "")
            reportTab1a3.SetParameterValue("MarcheForfaitOUI", IIf(TxtTypeMarche.Text = "Fournitures", "X", ""))
            reportTab1a3.SetParameterValue("MarcheForfaitNON", IIf(TxtTypeMarche.Text = "Travaux", "X", ""))
            'reportTab1a3.SetParameterValue("AvisGlePDM", "")

            'Données du marché *********************
            Dim CodeMarche As Decimal = 0
            Dim LeBaill As String = ""
            Dim LibMarc As String = ""
            Dim AvisGlePDM As String = ""
            Dim DateAvisNonObjBanque As String = ""

            query = "select M.RefMarche,M.DescriptionMarche,M.InitialeBailleur, P.DateApprobation, P.DateAvisGle from T_Marche as M, t_dao as D, t_ppm_marche as P where D.RefMarche=M.RefMarche and M.RefPPM=P.RefPPM and M.RefMarche='" & rwDao("RefMarche").ToString & "' and D.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and D.CodeProjet='" & ProjetEnCours & "'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                CodeMarche = rw("RefMarche")
                LeBaill = rw("InitialeBailleur")

                If (LibMarc <> "") Then
                    LibMarc = LibMarc & vbNewLine & " et " & vbNewLine
                End If
                LibMarc = LibMarc & rw("DescriptionMarche").ToString

                DateAvisNonObjBanque = rw("DateApprobation").ToString
                AvisGlePDM = rw("DateAvisGle").ToString
            Next

            If TxtTypeMarche.Text.ToLower = "Fournitures".ToLower Then
                If TxtMethode.Text.ToUpper = "AOI" Or TxtMethode.Text.ToUpper = "AON" Then
                    reportCouv.SetParameterValue("LibelleMarche", MettreApost(LibMarc).ToUpper)
                Else
                    reportTab1a3.SetParameterValue("DateAvisNonObjBanque", DateAvisNonObjBanque.ToString)
                End If
            End If

            reportTab1a3.SetParameterValue("LibelleMarche", MettreApost(LibMarc))
            reportTab1a3.SetParameterValue("AvisGlePDM", AvisGlePDM.ToString)


            ' La convention ****************************
            query = "select C.CodeConvention,C.TypeConvention,C.MontantConvention,C.EntreeEnVigueur,C.DateCloture from T_Convention as C, T_Bailleur as B, t_marche as M where M.Convention_ChefFile=C.CodeConvention and C.CodeBailleur=B.CodeBailleur and M.RefMarche='" & CodeMarche & "' and B.CodeProjet='" & ProjetEnCours & "'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows

                '***********************
                If TxtTypeMarche.Text.ToLower = "Fournitures".ToLower Then
                    If TxtMethode.Text.ToUpper = "AOI" Or TxtMethode.Text.ToUpper = "AON" Then
                        reportCouv.SetParameterValue("TypeConv", rw("TypeConvention").ToString.ToUpper)
                        reportCouv.SetParameterValue("NumConv", rw("CodeConvention").ToString)
                        reportCouv.SetParameterValue("Bailleur", LeBaill)
                    End If
                End If

                reportTab1a3.SetParameterValue("NumConv", rw("CodeConvention").ToString)
                reportTab1a3.SetParameterValue("DateVigueurConv", rw("EntreeEnVigueur").ToString)
                reportTab1a3.SetParameterValue("DateClotureConv", rw("DateCloture").ToString)
            Next

            'Dossiers retirés
            query = "select Count(*) from T_Fournisseur where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                reportTab1a3.SetParameterValue("NbreDossierRetires", rw(0).ToString)
            Next
            'Offres recues
            query = "select Count(*) from T_Fournisseur where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "' and DateDepotDAO<>''"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                reportTab1a3.SetParameterValue("NbreOffresRecues", rw(0).ToString)
            Next

            reportTab4.SetParameterValue("NumDaoTab4", EnleverApost(CmbNumDoss.Text))
            reportTab5.SetParameterValue("NumDaoTab5", EnleverApost(CmbNumDoss.Text))
            reportTab6.SetParameterValue("NumDaoTab6", EnleverApost(CmbNumDoss.Text))
            reportTab8A.SetParameterValue("NumDaoTab8A", EnleverApost(CmbNumDoss.Text))
            reportTab9.SetParameterValue("NumDaoTab9", EnleverApost(CmbNumDoss.Text))
            reportRang1.SetParameterValue("NumDaoClass1", EnleverApost(CmbNumDoss.Text))
            reportPost.SetParameterValue("NumDaoPost", EnleverApost(CmbNumDoss.Text))
            reportRang2.SetParameterValue("NumDaoClass2", EnleverApost(CmbNumDoss.Text))
            reportProposition.SetParameterValue("NumDaoPropo", EnleverApost(CmbNumDoss.Text))


            If (Traitement = "Imprimer") Then
                'If (AutreChoix = True) Then
                '    reportFavoris.PrintToPrinter(1, True, 0, 0)
                'End If
                reportProposition.PrintToPrinter(1, True, 0, 0)
                reportRang2.PrintToPrinter(1, True, 0, 0)
                reportPost.PrintToPrinter(1, True, 0, 0)
                reportRang1.PrintToPrinter(1, True, 0, 0)
                reportTab9.PrintToPrinter(1, True, 0, 0)
                reportTab8A.PrintToPrinter(1, True, 0, 0)
                reportTab6.PrintToPrinter(1, True, 0, 0)
                reportTab5.PrintToPrinter(1, True, 0, 0)
                reportTab4.PrintToPrinter(1, True, 0, 0)
                reportTab1a3.PrintToPrinter(1, True, 0, 0)
                reportCouv.PrintToPrinter(1, True, 0, 0)
            Else
                RepCouverture.ReportSource = reportCouv
                RepTab1a3.ReportSource = reportTab1a3
                RepTab4.ReportSource = reportTab4
                RepTab5.ReportSource = reportTab5
                RepTab6.ReportSource = reportTab6
                RepTab8A.ReportSource = reportTab8A
                RepTab9.ReportSource = reportTab9
                RepRang1.ReportSource = reportRang1
                RepPost.ReportSource = reportPost
                RepRang2.ReportSource = reportRang2
                RepProposition.ReportSource = reportProposition

                'If (AutreChoix = True) Then
                '    RepFavoris.ReportSource = reportFavoris
                'End If
            End If

        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
            Return False
        End Try
        Return True
    End Function

#End Region

#Region "Attribution du marché"
    Private Sub CmbNumLotAttrib_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumLotAttrib.SelectedValueChanged
        'query = "select F.NomFournis,S.RefSoumis,S.Attribue from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and S.CodeLot='" & CmbNumLotAttrib.Text & "' and S.Selectionne='OUI' and F.NumeroDAO='" & CmbNumDoss.Text & "'"

        query = "select F.NomFournis,S.Attribue,S.CodeFournis from T_Fournisseur as F,t_soumissionfournisseurclassement as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO=S.NumeroDAO and S.CodeLot='" & CmbNumLotAttrib.Text & "' and S.Selectionne='OUI' and F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        If dt0.Rows.Count > 0 Then
            Dim rw As DataRow = dt0.Rows(0)
            BtOuiAttrib.Enabled = True
            BtSuivantAttrib.Enabled = True

            TxtSoumisAttrib.Text = MettreApost(rw("NomFournis").ToString)
            RefSoumisFavoris.Text = rw("CodeFournis").ToString

            If (rw("Attribue").ToString = "OUI") Then
                BtOuiAttrib.Enabled = False
                BtSuivantAttrib.Enabled = True
            Else
                BtOuiAttrib.Enabled = True
                BtSuivantAttrib.Enabled = True
            End If
            TxtSoumisAttrib.ForeColor = Color.Black
        Else
            TxtSoumisAttrib.Text = "Aucun enregistrement trouvé!"
            BtOuiAttrib.Enabled = False
            BtSuivantAttrib.Enabled = False
            TxtSoumisAttrib.ForeColor = Color.Red
        End If
    End Sub

    Private Sub BtOuiAttrib_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtOuiAttrib.Click
        ReponseDialog = ""
        ValiderAttributionMarche.ShowDialog()
        If (ReponseDialog = "OK") Then
            query = "update t_soumissionfournisseurclassement set Attribue='OUI' where CodeFournis='" & RefSoumisFavoris.Text & "' AND CodeLot='" & CmbNumLotAttrib.Text & "' AND NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "'"
            ExecuteNonQuery(query)

            'Verification
            If EtatRapportEvalOffres.ToString = "Valider" Then
                SuccesMsg("Le marché a été attribué avec succès." & vbNewLine & "Vous avez la possibilité d'élaborer le marché.")
                BtEtatMarche.Enabled = True
            Else 'Rapport non valider par le bailleur de fonds
                SuccesMsg("Le marché a été attribué avec succès.")
            End If
            BtOuiAttrib.Enabled = False
            BtSuivantAttrib.Enabled = True
            BtPVAttribution.Enabled = True
            BtRapportEval.Enabled = True
        End If
    End Sub

    Private Sub BtSuivantAttrib_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtSuivantAttrib.Click

        Try
            ReponseDialog = ""
            Dim GetNewDisqua As New MotifDisqualification
            GetNewDisqua.TypeDossier = "DAO"
            GetNewDisqua.TxtNomConslt.Text = TxtSoumisAttrib.Text
            GetNewDisqua.ShowDialog()
            'Disqualification confirmé
            If ReponseDialog.ToString <> "" Then
                DebutChargement(True, "Traitement en cours...")

                ExecuteNonQuery("Update t_soumissionfournisseurclassement set Selectionne='NON', MotifSelect='" & EnleverApost(ReponseDialog.ToString) & "', FournisDisqualifie='OUI' where CodeFournis='" & RefSoumisFavoris.Text & "' and CodeLot='" & CmbNumLotAttrib.Text & "' and NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "'")
                'recherche soumissionnaire suivant

                'Il existe des critères post qualifications
                If ExisteDesCriterePostQualifications = True Then
                    query = "select F.NomFournis,S.CodeFournis from T_Fournisseur as F,t_soumissionfournisseurclassement as S where F.CodeFournis=S.CodeFournis And F.NumeroDAO=S.NumeroDAO And S.CodeLot='" & CmbNumLotAttrib.Text & "' and S.Selectionne='NON' and F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and S.FournisDisqualifie IS NULL ORDER BY S.RangPostQualif ASC LIMIT 1"
                Else
                    query = "select F.NomFournis,S.CodeFournis from T_Fournisseur as F,t_soumissionfournisseurclassement as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO=S.NumeroDAO and S.CodeLot='" & CmbNumLotAttrib.Text & "' and S.Selectionne='NON' and F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and S.FournisDisqualifie IS NULL ORDER BY S.RangExamDetaille ASC LIMIT 1"
                End If

                Dim dt As DataTable = ExcecuteSelectQuery(query)

                If dt.Rows.Count > 0 Then
                    For Each rw In dt.Rows
                        ExecuteNonQuery("Update t_soumissionfournisseurclassement set Selectionne='OUI', MotifSelect='Classement évaluateurs' where CodeFournis='" & rw("CodeFournis") & "' and CodeLot='" & CmbNumLotAttrib.Text & "' and NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "'")

                        RefSoumisFavoris.Text = rw("CodeFournis").ToString
                        TxtSoumisAttrib.Text = MettreApost(rw("NomFournis").ToString)
                        BtOuiAttrib.Enabled = True
                        BtSuivantAttrib.Enabled = True
                        TxtSoumisAttrib.ForeColor = Color.Black
                    Next
                    FinChargement()

                    DebutChargement(True, "Chargement du rapport d'évaluation en cours...")
                    'CmbNumLotAttrib.Text = ""
                    'TxtSoumisAttrib.Text = ""
                    'RefSoumisFavoris.Text = ""
                    RapportEvaluation()
                    FinChargement()
                Else
                    FinChargement()
                    TxtSoumisAttrib.Text = "Tous les soumissionnaires retenus pour ce lot sont disqualifiés."
                    BtOuiAttrib.Enabled = False
                    BtSuivantAttrib.Enabled = False
                    TxtSoumisAttrib.ForeColor = Color.Red
                End If
            End If
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

#End Region

#Region "PV Attribution Marché"
    Private Sub BtPVAttribution_Click(sender As Object, e As EventArgs) Handles BtPVAttribution.Click
        Try

            'AfficherGrid("Rapport en detaille")
            'TabRapportEval.Visible = True
            'Tableau1a3.PageVisible = False
            'Tableau4.PageVisible = False
            'Tableau5.PageVisible = False
            'Tableau6.PageVisible = False
            'Tableau8A.PageVisible = False
            'Tableau9.PageVisible = False
            'Rang1.PageVisible = False
            'Rang2.PageVisible = False
            'PostQualif.PageVisible = False
            'Proposition.PageVisible = False
            'Couverture.Text = "PV D'ATTRIUBUTION"

            DebutChargement(True, "Chargement du PV d'attribution de marché.") 'du DAO N°" & CmbNumDoss.Text)
            Dim Chemin As String = lineEtat & "\Marches\DAO\PV_dattribution\"
            Dim reportPVAttribution As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            reportPVAttribution.Load(Chemin & "PV_attribution.rpt")

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = reportPVAttribution.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next
            reportPVAttribution.SetParameterValue("NumDao", CmbNumDoss.Text)
            reportPVAttribution.SetParameterValue("CodeProjet", ProjetEnCours)
            ' RepCouverture.ReportSource = reportPVAttribution

            FinChargement()
            FullScreenReport.Text = "PV D'ATTRIUBUTION DE MARCHE DU DAO N° " & CmbNumDoss.Text
            FullScreenReport.FullView.ReportSource = reportPVAttribution
            FullScreenReport.ShowDialog()

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub
#End Region

#Region "Elaboration de Marché"
    Private Sub BtEtatMarche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEtatMarche.Click
        Try
            If ListeTbaleClicke(2) = False Then
                TxtTypeExamen.Text = "EDITION DES MARCHES DU DAO N° " & CmbNumDoss.Text
                BtDelete.Enabled = False
                AddLigneRepartition.Enabled = False

                CmbLotMarche.ResetText()
                CmbLotMarche.Properties.Items.Clear()
                For k As Integer = 1 To CInt(TxtNbLot.Text)
                    CmbLotMarche.Properties.Items.Add(k.ToString)
                Next
                'Remplir combo article
                RemplirComboArticle()
                ListeTbaleClicke(2) = True
            End If

            AfficherGrid("ElaborationMarche")
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub RemplirComboArticle()
        Try
            CombArticle.Text = ""
            CombArticle.Properties.Items.Clear()

            If TxtTypeMarche.Text.ToLower = "Fournitures".ToLower Then
                If TxtMethode.Text.ToUpper = "AON" Or TxtMethode.Text.ToUpper = "AOI" Then
                    Dim CodeSection = {"CCAG 4.2(b)", "CCAG 13.1", "CCAG 15.1", "CCAG 16.5(a)", "CCAG 16.5(b)", "CCAG 18.1(a)", "CCAG 18.1(b)", "CCAG 18.3(a)", "CCAG 18.3(b)", "CCAG 18.4", "CCAG 23.2", "CCAG 24.1", "CCAG 25.1", "CCAG 26.1", "CCAG 26.2", "CCAG 27.1(a)", "CCAG 27.1(b)", "CCAG 28.5, CCAG 28.6", "CCAG 33.4"}
                    For i = 0 To CodeSection.Length - 1
                        CombArticle.Properties.Items.Add("Section " & CodeSection(i).ToString)
                    Next
                ElseIf TxtMethode.Text.ToUpper = "PSL" Or TxtMethode.Text.ToUpper = "PSO" Then
                    Dim CodeSection = {"6", "8", "13", "14", "15"}
                    For i = 0 To CodeSection.Length - 1
                        CombArticle.Properties.Items.Add("Section " & CodeSection(i).ToString)
                    Next
                End If
            ElseIf TxtTypeMarche.Text.ToLower = "Travaux".ToLower Then

            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub
    Private Sub CmbLotMarche_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbLotMarche.SelectedIndexChanged
        Try

            InitFormMarche()
            If (CmbLotMarche.SelectedIndex <> -1) Then
                DebutChargement(True, "Chargement des données en cours...")
                Dim ListeConvention As String = ""

                query = "select LibelleLot, Reflot, CodeLot from T_LotDAO where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and CodeLot='" & CmbLotMarche.Text & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw In dt0.Rows
                    TxtLotMarche.Text = MettreApost(rw("LibelleLot").ToString)
                    'TxtLotMarche.Text = IIf(rw("LibelleLot").ToString <> "Lot N°" & CmbLotMarche.Text, MettreApost(rw("LibelleLot").ToString), TxtLibelleDoss.Text & " (" & rw("LibelleLot").ToString & ")")
                    TxtRefLotMarche.Text = rw("Reflot").ToString
                Next

                query = "select distinct S.BanqueCaution, S.DelaiLivraison,F.PrixCorrigeOffre,F.CodeFournis, S.NumCompteBanque from T_SoumissionFournisseur as S, t_soumissionfournisseurclassement F where F.CodeFournis=S.CodeFournis AND S.CodeLot=F.CodeLot and F.Attribue='OUI' and F.Selectionne='OUI' and F.FournisDisqualifie IS NULL and F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' AND F.CodeLot='" & CmbLotMarche.Text & "'"
                Dim dt1 = ExcecuteSelectQuery(query)
                If dt1.Rows.Count > 0 Then
                    BtEnregistrerMarche.Enabled = True

                    For Each rw1 In dt1.Rows
                        TxtCodeFournisMarche.Text = rw1("CodeFournis").ToString

                        Dim NomBank As String = ""

                        query = "select NomCompletBanque from T_Banque where CodeProjet='" & ProjetEnCours & "' and CodeBanque='" & rw1("BanqueCaution").ToString & "'"
                        NomBank = MettreApost(ExecuteScallar(query))
                        'TxtNomBanqueFournis.Text = MettreApost(rw1("BanqueCaution").ToString) & " (" & NomBank & ")"
                        'TxtNumCompteFournis.Text = rw1("NumCompteBanque").ToString
                        TxtExecutionMarche.Text = rw1("DelaiLivraison").ToString
                        TxtMontantMarche.Text = AfficherMonnaie(rw1("PrixCorrigeOffre").ToString.Replace(" ", ""))

                        'infos fournis *****************
                        query = "select NomFournis,PaysFournis,AdresseCompleteFournis,TelFournis,FaxFournis,MailFournis,CompteContribuableFournis,RegistreCommerceFournis,NomRep,AdresseRep,TelRep from T_Fournisseur where CodeFournis='" & rw1("CodeFournis").ToString & "' and CodeProjet='" & ProjetEnCours & "' and NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "'"
                        Dim dt2 = ExcecuteSelectQuery(query)
                        For Each rw2 In dt2.Rows
                            TxtFournisMarche.Text = MettreApost(rw2("NomFournis").ToString & " (" & rw2("PaysFournis").ToString & ")")
                            TxtAdresseFournisMarche.Text = MettreApost(rw2("AdresseCompleteFournis").ToString)
                            TxtContactFournisMarche.Text = IIf(rw2("TelFournis").ToString <> "", "Tel : " & rw2("TelFournis").ToString & "  ", "") & IIf(rw2("FaxFournis").ToString <> "", "Fax : " & rw2("FaxFournis").ToString & "  ", "") & IIf(rw2("MailFournis").ToString <> "", "E-mail : " & rw2("MailFournis").ToString, "")
                            TxtContribuable.Text = rw2("CompteContribuableFournis").ToString
                            TxtRegCommerce.Text = rw2("RegistreCommerceFournis").ToString
                            TxtNomRepLegal.Text = MettreApost(rw2("NomRep").ToString)
                            TxtBpRepLegal.Text = MettreApost(rw2("AdresseRep").ToString)
                            TxtContactRepLegal.Text = MettreApost(rw2("TelRep").ToString)
                        Next
                    Next

                    query = "select B.InitialeBailleur,B.NomBailleur,B.CodeBailleur, C.TypeConvention, M.* from T_Bailleur as B,T_Convention as C, T_Marche as M, t_dao as D where D.RefMarche=M.RefMarche and M.Convention_ChefFile=C.CodeConvention and C.CodeBailleur=B.CodeBailleur and D.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and M.CodeProjet='" & ProjetEnCours & "' and B.CodeProjet=M.CodeProjet"
                    dt0 = ExcecuteSelectQuery(query)
                    If dt0.Rows.Count > 0 Then
                        Dim rw As DataRow = dt0.Rows(0)
                        TxtBailleurMarche.Text = MettreApost(rw("InitialeBailleur").ToString & " (" & rw("NomBailleur").ToString & ")")
                        TxtConventionMarche.Text = MettreApost(rw("TypeConvention").ToString & " " & rw("Convention_ChefFile").ToString)
                        ListeConvention = MettreApost(rw("CodeConvention").ToString)
                    End If

                    query = "select NumeroMarche,PrctCautionDef,PrctAvance,ImputBudgetaire from T_MarcheSigne where CodeFournis='" & EnleverApost(TxtCodeFournisMarche.Text) & "' and RefLot='" & TxtRefLotMarche.Text & "'"
                    Dim dt3 = ExcecuteSelectQuery(query)

                    If dt3.Rows.Count > 0 Then 'Impossible de modifier le numero du marché saisie
                        NumerosMarche.Properties.ReadOnly = True

                        For Each rw3 In dt3.Rows
                            TxtPrctCautionDef.Text = IIf(Val(rw3("PrctCautionDef").ToString.Replace(".", ",")) > 0, rw3("PrctCautionDef").ToString.Replace(".", ","), 0).ToString
                            TxtPrctAvance.Text = IIf(Val(rw3("PrctAvance").ToString.Replace(".", ",")) > 0, rw3("PrctAvance").ToString.Replace(".", ","), 0).ToString
                            TxtImputBudgetaire.Text = rw3("ImputBudgetaire").ToString
                            NumerosMarche.Text = rw3("NumeroMarche").ToString
                        Next

                        GetDonneInfoDAO(NumerosMarche.Text, "")
                        LoadRepartionMontantMarche("Load")
                    Else
                        NumerosMarche.Properties.ReadOnly = False
                        NumerosMarche.Text = NumeroMarche_automatique() 'Numero de marché par defaut
                        LoadRepartionMontantMarche("CreateColumn", ListeConvention.ToString)
                    End If

                    BtDelete.Enabled = True
                    AddLigneRepartition.Enabled = True

                    ChargerLesArticle(NumerosMarche.Text, "") 'Chargement des articles
                Else 'Aucun marché a enregistrer
                    BtEnregistrerMarche.Enabled = False
                End If

                FinChargement()
            End If

        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub GetDonneInfoDAO(NumeroMarche As String, Optional typeRequete As String = "")
        Try
            If typeRequete = "Save" Then
                query = "SELECT count(*) FROM t_dao_marche where NumeroMarche='" & EnleverApost(NumeroMarche.ToString) & "' and CodeProjet='" & ProjetEnCours & "'"
                If Val(ExecuteScallar(query)) > 0 Then
                    query = "update t_dao_marche set NomBanqueFournis='" & EnleverApost(TxtNomBanqueFournis.Text) & "', NumCompteFournis='" & EnleverApost(TxtNumCompteFournis.Text) & "', MontantTVA='" & IIf(MontantTVA.Text <> "", MontantTVA.Text.Replace(" ", ""), 0).ToString & "' where NumeroMarche='" & EnleverApost(NumeroMarche.ToString) & "' and NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
                Else
                    query = "insert into t_dao_marche values(NULL, '" & EnleverApost(NumeroMarche.ToString) & "', '" & EnleverApost(CmbNumDoss.Text) & "', '" & EnleverApost(TxtNumCompteFournis.Text) & "', '" & EnleverApost(TxtNomBanqueFournis.Text) & "', '" & ProjetEnCours & "', '" & IIf(MontantTVA.Text <> "", MontantTVA.Text.Replace(" ", ""), 0).ToString & "')"
                End If
                ExecuteNonQuery(query)
            Else
                query = "SELECT * FROM t_dao_marche where NumeroMarche='" & EnleverApost(NumeroMarche.ToString) & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw In dt.Rows
                    TxtNomBanqueFournis.Text = MettreApost(rw("NomBanqueFournis").ToString)
                    TxtNumCompteFournis.Text = MettreApost(rw("NumCompteFournis").ToString)
                    MontantTVA.Text = AfficherMonnaie(rw("MontantTVA").ToString)
                Next
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub
    Private Sub LoadRepartionMontantMarche(TypeChargement As String, Optional ListeConvention As String = "")
        Try
            Dim dtRepart = New DataTable()
            dtRepart.Columns.Clear()
            ViewRepartion.Columns.Clear()
            dtRepart.Columns.Add("RefRepartion", Type.GetType("System.String"))
            dtRepart.Columns.Add("Année", Type.GetType("System.String"))

            If TypeChargement = "CreateColumn" Then
                Dim ListeCon As String() = ListeConvention.Split("|")
                For i = 0 To ListeCon.Length - 1
                    If ListeCon(i).ToString.Trim <> "" Then
                        dtRepart.Columns.Add(ListeCon(i).ToString, Type.GetType("System.String"))
                    End If
                Next
                dtRepart.Rows.Clear()
            Else

                Dim dtt As DataTable = ExcecuteSelectQuery("Select * from t_dao_repartition_montant_marche where NumeroMarche='" & EnleverApost(NumerosMarche.Text) & "'")
                Dim Ajouter As Boolean = False
                Dim NombrConvenAjouter As Integer = 0
                For Each rw In dtt.Rows
                    If Ajouter = False Then
                        For i = 1 To 10 'Ajout des convention == *** ligne des convention
                            If rw(i + 2).ToString.Trim <> "" Then
                                dtRepart.Columns.Add(MettreApost(rw(i + 2).ToString), Type.GetType("System.String"))
                                NombrConvenAjouter += 1
                            End If
                        Next
                        Ajouter = True
                        dtRepart.Rows.Clear()
                    End If

                    Dim drS = dtRepart.NewRow()
                    drS("RefRepartion") = rw("RefRepartion").ToString
                    drS("Année") = rw("Annee").ToString

                    'Renseigné la valeur de la convention
                    'Dim ValConven As Integer = 12
                    For Cols = 0 To NombrConvenAjouter - 1
                        drS(Cols + 2) = rw(13 + Cols).ToString
                    Next
                    dtRepart.Rows.Add(drS)
                Next
            End If

            ListeRepartion.DataSource = dtRepart
            '  ViewRepartion.Columns("RefRepartion").MaxWidth = 100
            ViewRepartion.Columns("RefRepartion").Visible = False
            ViewRepartion.Columns("Année").Width = 100
            ViewRepartion.OptionsBehavior.Editable = True

            ViewRepartion.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
            ViewRepartion.Columns("Année").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
            For i = 2 To ViewRepartion.Columns.Count - 1
                ViewRepartion.Columns(i).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewRepartion.Columns(i).Width = 150
            Next

            Dim cmbAnne As RepositoryItemComboBox = New RepositoryItemComboBox()
            cmbAnne.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            GetRemplirAnne(cmbAnne)
            AddHandler cmbAnne.EditValueChanged, AddressOf cmbAnne_CheckedChanged
            ViewRepartion.Columns("Année").ColumnEdit = cmbAnne
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub cmbAnne_CheckedChanged(sender As Object, e As EventArgs)
        Dim obj As DevExpress.XtraEditors.ComboBoxEdit = CType(sender, DevExpress.XtraEditors.ComboBoxEdit)
    End Sub

    Private Sub GetRemplirAnne(ByRef OutPut As RepositoryItemComboBox)
        OutPut.Items.Clear()
        Dim Anne As Integer = 5
        For i = 1 To 10
            If i <= 5 Then
                OutPut.Items.Add(Now.Year - Anne)
                Anne -= 1
            Else
                OutPut.Items.Add(Now.Year + Anne)
                Anne += 1
            End If
        Next
    End Sub


    Private Sub AddLigneRepartition_Click(sender As Object, e As EventArgs) Handles AddLigneRepartition.Click
        Try
            Dim NewLign As DataTable = ListeRepartion.DataSource
            Dim drs = NewLign.NewRow()
            For i = 0 To ViewRepartion.Columns.Count - 1
                drs(i) = ""
            Next

            NewLign.Rows.Add(drs)
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub BtDelete_Click(sender As Object, e As EventArgs) Handles BtDelete.Click
        Try
            If ViewRepartion.RowCount > 0 Then
                If ConfirmMsg("Êtes-vous sûrs de vouloir supprimer ?") Then
                    Dim RefRepartion = ViewRepartion.GetFocusedRowCellValue("RefRepartion").ToString
                    If RefRepartion.ToString <> "" Then
                        ExecuteNonQuery("delete from t_dao_repartition_montant_marche where RefRepartion='" & RefRepartion & "'")
                    End If
                    ViewRepartion.GetDataRow(ViewRepartion.FocusedRowHandle).Delete()
                End If
            Else
                FailMsg("Aucune ligne à supprimer")
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub


    Private Sub EnregistreRepartition(ByVal NumeroMarche As String)
        Try
            ExecuteNonQuery("delete from t_dao_repartition_montant_marche where NumeroMarche='" & EnleverApost(NumeroMarche.ToString) & "' and CodeProjet='" & ProjetEnCours & "'")

            If ViewRepartion.RowCount > 0 Then
                Dim Ligne As Integer = 0

                Dim DatSet = New DataSet
                query = "select * from t_dao_repartition_montant_marche"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "t_dao_repartition_montant_marche")
                Dim DatTable = DatSet.Tables("t_dao_repartition_montant_marche")

                For i = 0 To ViewRepartion.RowCount - 1
                    Dim DatRow = DatSet.Tables("t_dao_repartition_montant_marche").NewRow()

                    DatRow("NumeroMarche") = EnleverApost(NumeroMarche.ToString)
                    DatRow("CodeProjet") = ProjetEnCours
                    DatRow("Annee") = ViewRepartion.GetRowCellValue(i, "Année").ToString
                    Ligne = 0

                    For j = 2 To ViewRepartion.Columns.Count - 1
                        Ligne += 1
                        DatRow("CodeConvention" & Ligne) = MettreApost(ViewRepartion.Columns(j).GetTextCaption)
                        DatRow("MontantConvention" & Ligne) = ViewRepartion.GetDataRow(i).Item(j).ToString.Replace(" ", "")
                    Next

                    DatSet.Tables("t_dao_repartition_montant_marche").Rows.Add(DatRow)
                Next

                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "t_dao_repartition_montant_marche")
                DatSet.Clear()
                BDQUIT(sqlconn)

                LoadRepartionMontantMarche("Load")
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub TxtMontantMarche_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontantMarche.TextChanged
        If (TxtMontantMarche.Text <> "") Then
            TxtMontMarcheLettre.Text = MontantLettre(TxtMontantMarche.Text.Replace(" ", ""))
        Else
            TxtMontMarcheLettre.Text = ""
        End If
    End Sub

    Private Sub MontantTVA_TextChanged(sender As Object, e As EventArgs) Handles MontantTVA.TextChanged
        If MontantTVA.Text.Trim <> "" Then
            MontantTVALettre.Text = MontantLettre(MontantTVA.Text.Replace(" ", ""))
        Else
            MontantTVALettre.Text = ""
        End If
    End Sub
    Private Sub TxtPrctCautionDef_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPrctCautionDef.TextChanged
        If (TxtPrctCautionDef.Text.Trim <> "") Then
            If (TxtMontantMarche.Text <> "") Then
                TxtMontCautionDef.Text = AfficherMonnaie(Math.Round((CDec(TxtMontantMarche.Text) * CDec(TxtPrctCautionDef.Text.Replace(".", ","))) / 100, 0).ToString)
            End If
        Else
            TxtMontCautionDef.Text = ""
        End If
    End Sub

    Private Sub TxtPrctAvance_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPrctAvance.TextChanged
        If (TxtPrctAvance.Text.Trim <> "") Then
            If (TxtMontantMarche.Text <> "") Then
                TxtMontAvance.Text = AfficherMonnaie(Math.Round((CDec(TxtMontantMarche.Text) * CDec(TxtPrctAvance.Text.Replace(".", ","))) / 100, 0).ToString)
            End If
        Else
            TxtMontAvance.Text = ""
        End If
    End Sub

    Private Sub TxtPrctCautionDef_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles TxtPrctCautionDef.EditValueChanging
        If TxtPrctCautionDef.Text.Trim <> "" Then
            If CDbl(e.NewValue.ToString.Replace(".", ",")) > 100 Then
                SuccesMsg("Le pourcentage de cautionnement" & vbNewLine & "[" & e.NewValue.ToString.Replace(".", ",") & "] ne doit pas exécédé 100%")
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub TxtPrctAvance_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles TxtPrctAvance.EditValueChanging
        If TxtPrctAvance.Text.Trim <> "" Then
            If CDbl(e.NewValue.ToString.Replace(".", ",")) > 100 Then
                SuccesMsg("Le pourcentage de l'avance [" & e.NewValue.ToString.Replace(".", ",") & "] ne doit pas exécédé 100%")
                e.Cancel = True
            End If
        End If
    End Sub


    Private Sub BtEnregistrerMarche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnregistrerMarche.Click
        Try

            If CmbLotMarche.IsRequiredControl("Veuillez sélectionner un lot.") Then
                CmbLotMarche.Select()
                Exit Sub
            End If
            If NumerosMarche.IsRequiredControl("Veuillez saisir le numéro du marché.") Then
                NumerosMarche.Select()
                Exit Sub
            End If
            If TxtNomRepLegal.IsRequiredControl("Veuillez saisir le nom et prénoms du représentant légal.") Then
                TxtNomRepLegal.Select()
                Exit Sub
            End If
            'If TxtBpRepLegal.IsRequiredControl("Veuillez saisir la boîte postale du représentant légal") Then
            '    Exit Sub
            'End If
            If TxtContactRepLegal.IsRequiredControl("Veuillez saisir le contact du représentant légal.") Then
                TxtContactRepLegal.Select()
                Exit Sub
            End If
            If TxtContribuable.IsRequiredControl("Veuillez saisir le compte contribuable.") Then
                TxtContribuable.Select()
                Exit Sub
            End If
            If TxtRegCommerce.IsRequiredControl("Veuillez saisir le registre de commerce.") Then
                TxtRegCommerce.Select()
                Exit Sub
            End If
            If TxtNumCompteFournis.IsRequiredControl("Veuillez saisir le numéro de compte du fournisseur.") Then
                TxtNumCompteFournis.Select()
                Exit Sub
            End If
            If TxtNomBanqueFournis.IsRequiredControl("Veuillez saisir le nom du compte du fournisseur.") Then
                TxtNomBanqueFournis.Select()
                Exit Sub
            End If

            'If TxtPrctCautionDef.IsRequiredControl("Veuillez saisir le pourcentage de cautionnement définitif.") Then
            '    TxtPrctCautionDef.Select()
            '    Exit Sub
            'End If
            'If TxtPrctAvance.IsRequiredControl("Veuillez saisir le pourcentage d'avance de démarrage.") Then
            '    TxtPrctAvance.Select()
            '    Exit Sub
            'End If

            If TxtImputBudgetaire.IsRequiredControl("Veuillez saisir l'imputation bugétaire.") Then
                TxtImputBudgetaire.Select()
                Exit Sub
            End If

            'Verification de la repartion du montant du marché.
            If GetVerifierMontantRepartion() = False Then
                Exit Sub
            End If

            'Verification de l'existene du numero du marché
            If NumerosMarche.Properties.ReadOnly = False Then 'Nouveau marché
                query = "select COUNT(*) from T_MarcheSigne where NumeroMarche='" & EnleverApost(NumerosMarche.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
                If Val(ExecuteScallar(query)) > 0 Then
                    SuccesMsg("Le numéro du marché existe déjà.")
                    NumerosMarche.Focus()
                    Exit Sub
                End If
            End If

            DebutChargement(True, "Enregistrement du marché en cours...")

            ' Maj Fournisseur *********************
            query = "update T_Fournisseur set CompteContribuableFournis='" & EnleverApost(TxtContribuable.Text) & "', RegistreCommerceFournis='" & EnleverApost(TxtRegCommerce.Text) & "', NomRep='" & EnleverApost(TxtNomRepLegal.Text) & "', AdresseRep='" & EnleverApost(TxtBpRepLegal.Text) & "', TelRep='" & EnleverApost(TxtContactRepLegal.Text) & "' where CodeFournis='" & TxtCodeFournisMarche.Text & "'"
            ExecuteNonQuery(query)

            ' Maj Soumission *********************
            'query = "update T_SoumissionFournisseur set NumCompteBanque='" & EnleverApost(TxtNumCompteFournis.Text) & "' where CodeFournis='" & TxtCodeFournisMarche.Text & "' and RefLot='" & TxtRefLotMarche.Text & "'"
            'ExecuteNonQuery(query)

            ' Existance marche ********************
            Dim MarcheExiste As Boolean = False
            query = "select count(*) from T_MarcheSigne where NumeroMarche='" & EnleverApost(NumerosMarche.Text) & "' and CodeFournis='" & TxtCodeFournisMarche.Text & "' and RefLot='" & TxtRefLotMarche.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            If Val(ExecuteScallar(query)) > 0 Then
                MarcheExiste = True
            End If

            'Marche existe ************
            If (MarcheExiste = True) Then 'MJ
                query = "update T_MarcheSigne set PrctCautionDef='" & TxtPrctCautionDef.Text.Replace(".", ",") & "', PrctAvance='" & TxtPrctAvance.Text.Replace(".", ",") & "', ImputBudgetaire='" & EnleverApost(TxtImputBudgetaire.Text) & "' where NumeroMarche='" & EnleverApost(NumerosMarche.Text) & "' and CodeFournis='" & TxtCodeFournisMarche.Text & "' and RefLot='" & TxtRefLotMarche.Text & "'"
                ExecuteNonQuery(query)
            Else 'Save

                Dim DatSet As New DataSet
                Dim DatAdapt As MySqlDataAdapter
                Dim DatTable As DataTable
                Dim DatRow As DataRow
                Dim CmdBuilder As MySqlCommandBuilder
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)

                query = "select * from T_MarcheSigne"
                Dim Cmd = New MySqlCommand(query, sqlconn)
                DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "T_MarcheSigne")
                DatTable = DatSet.Tables("T_MarcheSigne")
                DatRow = DatSet.Tables("T_MarcheSigne").NewRow()

                DatRow("NumeroMarche") = EnleverApost(NumerosMarche.Text)
                DatRow("NumeroDAO") = EnleverApost(CmbNumDoss.Text)
                'DatRow("NumMarcheDMP") = ""
                DatRow("DateMarche") = Now.ToShortDateString
                ' DatRow("RefMarche") = MarcheRef
                DatRow("TypeMarche") = EnleverApost(TxtTypeMarche.Text)
                DatRow("MontantHT") = TxtMontantMarche.EditValue.ToString.Replace(" ", "")
                DatRow("CodeFournis") = TxtCodeFournisMarche.Text
                DatRow("RefLot") = TxtRefLotMarche.Text
                'DatRow("RefSoumis") = TxtRefSoumisMarche.Text
                DatRow("PrctCautionDef") = TxtPrctCautionDef.Text.Replace(".", ",")
                DatRow("PrctAvance") = TxtPrctAvance.Text.Replace(".", ",")
                DatRow("ImputBudgetaire") = TxtImputBudgetaire.Text
                DatRow("CodeProjet") = ProjetEnCours
                DatRow("EtatMarche") = "En cours"

                DatSet.Tables("T_MarcheSigne").Rows.Add(DatRow)
                CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "T_MarcheSigne")
                DatSet.Clear()
                BDQUIT(sqlconn)
            End If

            'Save repartion montant marché
            GetDonneInfoDAO(NumerosMarche.Text, "Save")
            EnregistreRepartition(NumerosMarche.Text)
            ChargerLesArticle(NumerosMarche.Text, "Save") 'Save des articles
            FinChargement()

            SuccesMsg("Marché enregistré avec succès.")
            NumerosMarche.Properties.ReadOnly = True
            BtImpMarche.Enabled = True

        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Function GetVerifierMontantRepartion()
        Try
            Dim MontantRepartion As Decimal = 0
            Dim BienRenseigner As Boolean = False
            Dim NombrConvenVide As Integer = 0
            Dim Cpte As Integer = 0
            If ViewRepartion.RowCount > 0 Then
                For i = 0 To ViewRepartion.RowCount - 1 'Parcourir les lignes
                    NombrConvenVide = 0
                    Cpte = 0
                    For j = 2 To ViewRepartion.Columns.Count - 1 'Parcourir les colonnes des conventions
                        Cpte += 1
                        'Bien renseigner
                        If (ViewRepartion.GetDataRow(i).Item(j).ToString) = "" Then
                            NombrConvenVide += 1
                        End If

                        If ViewRepartion.GetDataRow(i).Item(j).ToString <> "" And (Not IsNumeric(ViewRepartion.GetDataRow(i).Item(j).ToString)) Then
                            BienRenseigner = True
                        End If

                        If IsNumeric(ViewRepartion.GetDataRow(i).Item(j).ToString) Then
                            MontantRepartion += CDec(ViewRepartion.GetDataRow(i).Item(j).ToString)
                        End If
                    Next

                    If (ViewRepartion.GetDataRow(i).Item(1).ToString = "") Or (NombrConvenVide = Cpte) Then
                        BienRenseigner = True
                    End If
                Next
            End If

            If BienRenseigner = True Then
                SuccesMsg("Veuillez bien renseigner le tableau" & vbNewLine & "de la répartition du montant du marché.")
                Return False
            End If

            If CDec(MontantRepartion) <> CDec(TxtMontantMarche.Text) Then
                SuccesMsg("La répartition du montant du marché est incorrect.")
                Return False
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
            Return False
        End Try
        Return True
    End Function

    Private Function NumeroMarche_automatique()
        Dim NumMarche = String.Empty
        Try
            Dim CodeProjet = ExecuteScallar("SELECT CodeProjet FROM t_projet WHERE CodeProjet='" & ProjetEnCours & "'")
            query = "select COUNT(NumeroMarche) from t_marchesigne where NumeroMarche LIKE '" & CodeProjet & "/" & Now.Year & "/" & Now.Month & "/" & "%'"
            Dim Result = Val(ExecuteScallar(query))
            If Result = 0 Then
                NumMarche = CodeProjet & "/" & Now.Year & "/" & Now.Month & "/" & "001"
            Else
                query = "select MAX(NumeroMarche) from t_marchesigne WHERE NumeroMarche LIKE '" & CodeProjet & "/" & Now.Year & "/" & Now.Month & "/" & "%'"
                Dim NumeroMarche_MAX = ExecuteScallar(query)
                Dim LastId As Double

                Dim nbreNumMarche() = NumeroMarche_MAX.Split("/")
                Dim idNumMarche = nbreNumMarche(3).ToString
                LastId = idNumMarche + 1
                If LastId.ToString().Length < 3 Then
                    idNumMarche = ""
                    For i = LastId.ToString().Length To 2
                        idNumMarche &= "0"
                    Next
                    idNumMarche &= LastId
                Else
                    idNumMarche = LastId.ToString()
                End If

                NumMarche = CodeProjet & "/" & Now.Year & "/" & Now.Month & "/" & idNumMarche
            End If
        Catch ex As Exception
            FailMsg("Erreur: Information non disponible !." & ex.ToString())
        End Try
        Return NumMarche
    End Function

    Private Sub BtAjoutSection_Click(sender As Object, e As EventArgs) Handles BtAjoutArticle.Click
        If TxtSaisiTextArticle.IsRequiredControl("Veuillez saisir la description de l'article.") Then
            TxtSaisiTextArticle.Focus()
            Exit Sub
        End If

        Dim n As Integer
        If DoublCick = True And NomGridView = "GridArticle" Then
            n = IndexSelectionne
            GridArticle.Rows.Item(n).Cells("LigneModif").Value = "Modifier"
        Else
            If CombArticle.SelectedIndex = -1 Then
                SuccesMsg("Veuillez sélectionner le code de l'article à saisir.")
                CombArticle.Focus()
                Exit Sub
            End If
            n = GridArticle.Rows.Add()
            GridArticle.Rows.Item(n).Cells("RefArticle").Value = ""
            GridArticle.Rows.Item(n).Cells("LigneModif").Value = "Ajouter"
        End If

        GridArticle.Rows.Item(n).Cells("CodeArticle").Value = CombArticle.Text.Replace("Section ", "")
        GridArticle.Rows.Item(n).Cells("DescriptionArticle").Value = TxtSaisiTextArticle.Text

        CombArticle.Enabled = True
        TxtSaisiTextArticle.Text = ""
        DoublCick = False
        IndexSelectionne = 0
        NomGridView = ""

    End Sub

    Private Sub BtSupArticle_Click(sender As Object, e As EventArgs) Handles BtSupArticle.Click
        Try
            If GridArticle.RowCount > 0 Then
                Dim Index = GridArticle.CurrentRow.Index
                If ConfirmMsg("Êtes-vous sûr de vouloir supprimé la ligne N° " & Index + 1 & "?") = DialogResult.Yes Then
                    Dim RefSection As String = GridArticle.Rows.Item(Index).Cells("RefArticle").Value.ToString
                    If RefSection.ToString <> "" Then
                        ExecuteNonQuery("delete from t_dao_Article where RefArticle='" & RefSection & "'")
                    End If
                    GridArticle.Rows.RemoveAt(Index)
                    CombArticle.Enabled = True
                    DoublCick = False
                    IndexSelectionne = 0
                    NomGridView = ""
                End If
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub GridSection_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridArticle.CellDoubleClick
        If GridArticle.RowCount > 0 Then
            IndexSelectionne = GridArticle.CurrentRow.Index
            Dim CodeSection As String = GridArticle.Rows.Item(IndexSelectionne).Cells("CodeArticle").Value
            DoublCick = True
            NomGridView = "GridArticle"
            TxtSaisiTextArticle.Text = GridArticle.Rows.Item(IndexSelectionne).Cells("DescriptionArticle").Value
            CombArticle.Text = "Section " & CodeSection.ToString
            CombArticle.Enabled = False
        End If
    End Sub

    Private Sub CombSection_TextChanged(sender As Object, e As EventArgs) Handles CombArticle.TextChanged
        TxtTextArticle.Text = GetTextSection(CombArticle.Text.Replace("Section ", "").Trim)
        If CombArticle.Text.Trim <> "" Then
            TxtSaisiTextArticle.Properties.ReadOnly = False
        Else
            TxtSaisiTextArticle.Properties.ReadOnly = True
        End If
    End Sub

    Private Sub ChargerLesArticle(ByVal NumeroMarche As String, Optional TypeRequette As String = "")
        Try
            Dim Inserer As Boolean = False

            If TypeRequette = "Save" Then
                If GridArticle.RowCount > 0 Then
                    For n = 0 To GridArticle.Rows.Count - 1
                        If GridArticle.Rows.Item(n).Cells("RefArticle").Value.ToString = "" Then
                            ExecuteNonQuery("insert into t_dao_article values(NULL, '" & NumeroMarche & "',  '" & EnleverApost(GridArticle.Rows.Item(n).Cells("CodeArticle").Value.ToString) & "',  '" & EnleverApost(GridArticle.Rows.Item(n).Cells("DescriptionArticle").Value.ToString) & "',  '" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "',  '" & ProjetEnCours & "')")
                            Inserer = True
                        ElseIf GridArticle.Rows.Item(n).Cells("LigneModif").Value.ToString = "Modifier" Then
                            ExecuteNonQuery("update t_dao_article set CodeArticle='" & EnleverApost(GridArticle.Rows.Item(n).Cells("CodeArticle").Value.ToString) & "',  Description='" & EnleverApost(GridArticle.Rows.Item(n).Cells("DescriptionArticle").Value.ToString) & "' where RefArticle='" & GridArticle.Rows.Item(n).Cells("RefArticle").Value & "' and CodeProjet='" & ProjetEnCours & "'")
                        End If
                    Next
                End If
            End If

            If Inserer = True Or TypeRequette = "" Then
                GridArticle.Rows.Clear()
                query = "Select * from t_dao_article where NumeroMarche='" & NumeroMarche & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    Dim n As Decimal = GridArticle.Rows.Add()
                    GridArticle.Rows.Item(n).Cells("RefArticle").Value = rw("RefArticle").ToString
                    GridArticle.Rows.Item(n).Cells("CodeArticle").Value = MettreApost(rw("CodeArticle").ToString)
                    GridArticle.Rows.Item(n).Cells("DescriptionArticle").Value = MettreApost(rw("Description").ToString)
                    GridArticle.Rows.Item(n).Cells("LigneModif").Value = "Enregistrer"
                Next
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Function GetTextSection(CodeSection As String) As String

        If TxtTypeMarche.Text.ToLower = "Fournitures".ToLower Then
            If TxtMethode.Text.ToUpper = "AON" Or TxtMethode.Text.ToUpper = "AOI" Then
                If CodeSection = "CCAG 4.2(b)" Then
                    Return "Les termes commerciaux auront la signification prescrite par les Incoterms. Si la signification d’un terme de commerce, et si les droits et obligations des parties ne sont pas prescrits par les Incoterms, ils seront prescrits par :"
                ElseIf CodeSection = "CCAG 13.1" Then
                    Return "Détails concernant les documents d’embarquement et autres documents à fournir par le Fournisseur sont : [insérer la liste des documents requis]"
                ElseIf CodeSection = "CCAG 15.1" Then
                    Return "Les prix des Fournitures livrées et Services connexes exécutés [insérer « ne seront pas » ou « seront » révisables]"
                ElseIf CodeSection = "CCAG 16.5(a)" Then
                    Return "Le délai au-delà duquel l’Acheteur paiera des intérêts au Fournisseur est de [nombre] ____ jours. "
                ElseIf CodeSection = "CCAG 16.5(b)" Then
                    Return "Le taux des intérêts de retard applicable sera de [insérer le nombre] ____%."
                ElseIf CodeSection = "CCAG 18.1(a)" Then
                    Return "Une garantie de bonne exécution [insérer « sera » ou « ne sera pas » requise]"
                ElseIf CodeSection = "CCAG 18.1(b)" Then
                    Return "[si une garantie de bonne exécution est requise, insérer [« le montant de la garantie de bonne exécution sera de : « insérer le montant »]]"
                ElseIf CodeSection = "CCAG 18.3(a)" Then
                    Return "Si requise, la garantie de bonne exécution sera : [insérer « une garantie bancaire » ou « un cautionnement d’une compagnie de garantie »]"
                ElseIf CodeSection = "CCAG 18.3(b)" Then
                    Return "Si requise, la garantie de bonne exécution sera libellée dans : [insérer « une monnaie librement convertible acceptable par l’Acheteur » ou « les monnaies de paiement du Marché, en pourcentage(s) du Prix du Marché]."
                ElseIf CodeSection = "CCAG 18.4" Then
                    Return "La garantie de bonne exécution sera libérée : [insérer une date si différente de celle résultant de l’application de la Clause 18.4 du CCAG]"
                ElseIf CodeSection = "CCAG 23.2" Then
                    Return "L’emballage, le marquage et les documents placés à l’intérieur et à l’extérieur des caisses seront : [insérer les informations]"
                ElseIf CodeSection = "CCAG 24.1" Then
                    Return "[insérer les caractéristiques de l’assurance définies d’un commun accord, y compris couverture, monnaie, et montant]"
                ElseIf CodeSection = "CCAG 25.1" Then
                    Return "[insérer les responsabilités]"
                ElseIf CodeSection = "CCAG 26.1" Then
                    Return "Les Inspections et Essais sont : [décrire les types, fréquences, procédures utilisées pour réaliser ces inspections et ces essais]"
                ElseIf CodeSection = "CCAG 26.2" Then
                    Return "Les inspections et les essais seront réalisés à :_ [insérer les lieux] "
                ElseIf CodeSection = "CCAG 27.1(a)" Then
                    Return "Les pénalités de retard s’élèveront à : [insérer le nombre] % par semaine."
                ElseIf CodeSection = "CCAG 27.1(b)" Then
                    Return "Le montant maximum des pénalités de retard sera de : [insérer le nombre] %"
                ElseIf CodeSection = "CCAG 28.5, CCAG 28.6" Then
                    Return "Le délai de réparation ou de remplacement sera de : [insérer le nombre] jours."
                ElseIf CodeSection = "CCAG 33.4" Then
                    Return "Dans le cas où la proposition fondée sur l’analyse de la valeur serait approuvée par l’Acheteur la rémunération versée au Fournisseur, qui sera incluse dans le Montant du Marché, sera de ____ (insérer le pourcentage approprié, usuellement de 50%) de la diminution du Montant du Marché)"
                End If
            ElseIf TxtMethode.Text.ToUpper = "PSL" Or TxtMethode.Text.ToUpper = "PSO" Then
                If CodeSection = "6" Then
                    Return "Indiquer le calendrier de paiement"
                ElseIf CodeSection = "8" Then
                    Return "Insérer le cas échéant, la liste des inspections et essais à effectuer par le titulaire à sa charge avant l’acceptation des livraisons."
                ElseIf CodeSection = "13" Then
                    Return "Indiquer les facteurs pouvant entraîner une prolongation des délais de livraison."
                ElseIf CodeSection = "14" Then
                    Return "Indiquer le délai cumulé de suspension des livraisons ouvrant droit à la résiliation du contrat."
                ElseIf CodeSection = "15" Then
                    Return "Indiquer les cas de force majeur pouvant entrainer l’arrêt des livraisons."
                End If
            End If

        ElseIf TxtTypeMarche.Text.ToLower = "Travaux".ToLower Then

        End If
        Return ""
    End Function

#End Region

#Region "Impression du marché"

    Private Sub BtImpMarche_Click(sender As Object, e As EventArgs) Handles BtImpMarche.Click
        If CmbNumDoss.SelectedIndex <> -1 Then
            Dim NewMarcheSigne As New MarcheSigne
            NewMarcheSigne.ChkDAO.Checked = True
            NewMarcheSigne.CmbDAO.Text = CmbNumDoss.Text
            NewMarcheSigne.NumeroDAO = CmbNumDoss.Text
            NewMarcheSigne.ShowDialog()
        Else
            SuccesMsg("Veuillez sélectionner un dossier.")
            CmbNumDoss.Select()
        End If
    End Sub
#End Region

#Region "Liste des Methodes Principaux"

    Public Sub ChargerExamPostQualif(Optional TypeAffiharge As Boolean = False)
        AfficherGrid("ExamDetail")
        If (CmbNumDoss.Text <> "") Then
            Dim ExamTerminee As Boolean = True

            GridPostFinal.Columns.Clear()
            GridPostFinal.Rows.Clear()
            Dim ColonneNum As New DataGridViewTextBoxColumn
            With ColonneNum
                .HeaderText = "CodeRef"
                .Name = "CodeRef"
                .Width = 50
                .HeaderCell.Style.Font = New Font("Tahoma", 8, FontStyle.Bold)
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Visible = False
                .Frozen = True
                .Resizable = False
            End With
            GridPostFinal.Columns.Insert(0, ColonneNum)

            Dim ColonneNum1 As New DataGridViewTextBoxColumn
            With ColonneNum1
                .HeaderText = "Soumissionnaire"
                .HeaderCell.Style.Font = New Font("Tahoma", 8, FontStyle.Bold)
                '.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Name = "Soumissionnaire"
                .Width = 250
                '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                ' .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Frozen = True
                .Resizable = False
            End With
            GridPostFinal.Columns.Insert(1, ColonneNum1)

            Dim nbCrit As Integer = 0
            Dim ColCritere(200) As String
            NombreCritere = 0

            'Dim Reader As MySqlDataReader
            query = "select LibelleCritere,CritereElimine,RefCritere,RefCritereMere from T_DAO_PostQualif where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and RefCritereMere<>'0' order by RefCritere ASC"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                nbCrit += 1
                Dim Mark As String = IIf(rw("CritereElimine").ToString = "OUI", "*", "").ToString
                ColCritere(NombreCritere) = "[CRITERE N°" & nbCrit.ToString & "]" & Mark

                CodeCritere(NombreCritere) = rw("RefCritere").ToString
                TableCritere(NombreCritere) = MettreApost(rw("LibelleCritere").ToString)
                CritereElimine(NombreCritere) = rw("CritereElimine").ToString
                GroupeCritere(NombreCritere) = rw("RefCritereMere").ToString
                NombreCritere += 1
            Next

            For i As Integer = 0 To NombreCritere - 1
                Dim ColonneNum2 As New DataGridViewTextBoxColumn
                With ColonneNum2
                    .HeaderText = ColCritere(i).ToString
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Name = "Critere" & i
                    .Width = 150
                    .HeaderCell.Style.Font = New Font("Tahoma", 8, FontStyle.Bold)
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    '.Frozen = False
                    .SortMode = DataGridViewColumnSortMode.NotSortable
                    .Resizable = False
                End With
                GridPostFinal.Columns.Insert(2 + i, ColonneNum2)

                query = "select LibelleCritere from T_DAO_PostQualif where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and RefCritere='" & GroupeCritere(i) & "'"
                dt0 = ExcecuteSelectQuery(query)
                If dt0.Rows.Count > 0 Then
                    GroupeCritere(i) = MettreApost(dt0.Rows(0).Item("LibelleCritere").ToString)
                End If
            Next

            Dim ColonneNum3 As New DataGridViewTextBoxColumn
            With ColonneNum3
                .HeaderText = "Conclusion"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Name = "Conclusion"
                .HeaderCell.Style.Font = New Font("Tahoma", 8, FontStyle.Bold)
                .Width = 150
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                ' .Frozen = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Resizable = False
            End With
            GridPostFinal.Columns.Insert(1 + nbCrit + 1, ColonneNum3)

            Dim cpt As Decimal = 0
            If TypeAffiharge = True Then
                Dim ListeLot As DataTable = ExcecuteSelectQuery("select * from t_lotdao where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "'")
                For Each rwLot As DataRow In ListeLot.Rows

                    query = "select F.CodeFournis,F.NomFournis, S.ExamPQValide from T_Fournisseur as F,t_soumissionfournisseurclassement as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO=S.NumeroDAO and F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and S.CodeLot='" & rwLot("CodeLot") & "' AND S.CodeSousLot='' order by S.RangExamDetaille"
                    dt0 = ExcecuteSelectQuery(query)
                    If dt0.Rows.Count > 0 Then
                        Dim m = GridPostFinal.Rows.Add
                        GridPostFinal.Rows.Item(m).Cells("CodeRef").Value = ""
                        GridPostFinal.Rows.Item(m).Cells("Soumissionnaire").Value = "Lot N°" & rwLot("CodeLot").ToString
                        GridPostFinal.Rows.Item(m).DefaultCellStyle.BackColor = Color.LightBlue
                        GridPostFinal.Rows.Item(m).Cells("Soumissionnaire").Style.Font = New Font("Tahoma", 8, FontStyle.Bold) 'Times New Roman

                        For Each rw As DataRow In dt0.Rows
                            cpt += 1
                            Dim n = GridPostFinal.Rows.Add
                            GridPostFinal.Rows.Item(n).Cells("CodeRef").Value = rw("CodeFournis").ToString
                            GridPostFinal.Rows.Item(n).Cells("Soumissionnaire").Value = MettreApost(rw("NomFournis").ToString)

                            Dim nbCrit2 As Integer = 1
                            query = "select Verdict,Commentaire from T_SoumisFournisPostQualif where CodeFournis='" & rw("CodeFournis").ToString & "' AND CodeLot='" & rwLot("CodeLot") & "' order by RefCritere"
                            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                            For Each rw1 As DataRow In dt1.Rows
                                nbCrit2 = nbCrit2 + 1
                                GridPostFinal.Rows.Item(n).Cells(nbCrit2).Value = IIf(Not IsDBNull(rw1("Verdict")), rw1("Verdict").ToString, "-")
                            Next
                            GridPostFinal.Rows.Item(n).Cells("Conclusion").Value = IIf(rw("ExamPQValide").ToString.Replace(" ", "") <> "", IIf(rw("ExamPQValide").ToString = "OUI", "QUALIFIE", "DISQUALIFIE").ToString, "-").ToString

                            If (rw("ExamPQValide").ToString = "") Then ExamTerminee = False 'Evaluation examen post qualification validé
                        Next
                    End If
                Next

            Else

                query = "select F.CodeFournis,F.NomFournis, S.ExamPQValide from T_Fournisseur as F,t_soumissionfournisseurclassement as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO=S.NumeroDAO and F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and S.CodeLot='" & CmbNumLot.Text & "' AND S.CodeSousLot='' order by S.RangExamDetaille"
                dt0 = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    cpt += 1

                    Dim n = GridPostFinal.Rows.Add
                    GridPostFinal.Rows.Item(n).Cells("CodeRef").Value = rw("CodeFournis").ToString
                    GridPostFinal.Rows.Item(n).Cells("Soumissionnaire").Value = MettreApost(rw("NomFournis").ToString)

                    Dim nbCrit2 As Integer = 1
                    query = "select Verdict,Commentaire from T_SoumisFournisPostQualif where CodeFournis='" & rw("CodeFournis").ToString & "' AND CodeLot='" & CmbNumLot.Text & "' order by RefCritere"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt1.Rows
                        nbCrit2 = nbCrit2 + 1
                        GridPostFinal.Rows.Item(n).Cells(nbCrit2).Value = IIf(Not IsDBNull(rw1("Verdict")), rw1("Verdict").ToString, "-")
                    Next
                    GridPostFinal.Rows.Item(n).Cells("Conclusion").Value = IIf(rw("ExamPQValide").ToString.Replace(" ", "") <> "", IIf(rw("ExamPQValide").ToString = "OUI", "QUALIFIE", "DISQUALIFIE").ToString, "-").ToString

                    If (rw("ExamPQValide").ToString = "") Then ExamTerminee = False 'Evaluation examen post qualification validé
                Next
            End If

            If GridPostFinal.RowCount > 0 Then
                For j = 0 To GridPostFinal.RowCount - 1
                    If GridPostFinal.Rows.Item(j).Cells("Conclusion").Value = "DISQUALIFIE" Then
                        GridPostFinal.Rows.Item(j).DefaultCellStyle.ForeColor = Color.Red
                    End If
                Next
            End If

            If (ExamTerminee = True And cpt > 0 And TypeAffiharge = True) Then
                BilansOffres.Enabled = True
            Else
                BilansOffres.Enabled = False
            End If
        End If
    End Sub

    Public Sub ChargerExamDetaille(Optional TypeAfficharge As Boolean = False)
        AfficherGrid("ExamPrelim")
        If (CmbNumDoss.Text <> "") Then

            dtExam.Columns.Clear()
            dtExam.Columns.Add("Code", Type.GetType("System.String"))
            dtExam.Columns.Add("CodeRef", Type.GetType("System.String"))
            dtExam.Columns.Add("Soumissionnaire", Type.GetType("System.String"))
            dtExam.Columns.Add("Monnaie", Type.GetType("System.String"))
            dtExam.Columns.Add("Taux de change", Type.GetType("System.String"))
            dtExam.Columns.Add("Montant de l'offre", Type.GetType("System.String"))
            dtExam.Columns.Add("Montant corrigé en monnaie d'évaluation", Type.GetType("System.String"))
            dtExam.Columns.Add("Erreurs de calcul", Type.GetType("System.String"))
            dtExam.Columns.Add("Sommes provisionnelles", Type.GetType("System.String"))
            dtExam.Columns.Add("Montant rabais", Type.GetType("System.String"))
            dtExam.Columns.Add("Ajouts pour omission", Type.GetType("System.String"))
            dtExam.Columns.Add("Ajustements", Type.GetType("System.String"))
            dtExam.Columns.Add("Variations mineures", Type.GetType("System.String"))
            dtExam.Columns.Add("Prix Total de l'Offre", Type.GetType("System.String"))
            dtExam.Columns.Add("Rang", Type.GetType("System.String"))
            dtExam.Columns.Add("CodeX", Type.GetType("System.String"))
            dtExam.Rows.Clear()

            Dim cpt2 As Decimal = 0
            If AttributionMarche = "Lot" Then 'Attribution par lot
                'Fin verification examen detaille
                'query = "select F.CodeFournis,F.NomFournis, S.* from T_Fournisseur as F,t_soumissionfournisseurclassement as S where F.CodeFournis=S.CodeFournis and S.NumeroDAO=F.NumeroDAO and F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and F.CodeProjet='" & ProjetEnCours & "' and S.RangExamDetaille IS NOT NULL"
                ' Dim dtExamenDetail As DataTable = ExcecuteSelectQuery(query)
                'If dtExamenDetail.Rows.Count > 0 Then

                If TypeAfficharge = True Then
                    Dim ListeLot As DataTable = ExcecuteSelectQuery("select * from t_lotdao where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "'")
                    For Each rwLot As DataRow In ListeLot.Rows
                        Dim dtClassement As DataTable = ExcecuteSelectQuery("select F.CodeFournis,F.NomFournis, S.* from T_Fournisseur as F,t_soumissionfournisseurclassement as S where F.CodeFournis=S.CodeFournis and S.NumeroDAO=F.NumeroDAO and F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and F.CodeProjet='" & ProjetEnCours & "' and S.CodeLot='" & rwLot("CodeLot") & "' ORDER BY S.RangExamDetaille ASC") ' and CodeSousLot=''"
                        If dtClassement.Rows.Count > 0 Then
                            Dim Dr = dtExam.NewRow()
                            Dr("CodeX") = "xx"
                            Dr("Code") = "xxxx"
                            Dr("CodeRef") = ""
                            Dr("Soumissionnaire") = "Lot N°" & rwLot("CodeLot").ToString
                            dtExam.Rows.Add(Dr)

                            For Each rwClass As DataRow In dtClassement.Rows
                                cpt2 += 1
                                Dim DrE = dtExam.NewRow()
                                DrE("Code") = IIf(CDec(cpt2 / 2) = CDec(cpt2 \ 2), "x", "").ToString
                                DrE("CodeX") = ""
                                DrE("CodeRef") = ""
                                DrE("Soumissionnaire") = MettreApost(rwClass("NomFournis").ToString)
                                DrE("Monnaie") = rwClass("Monnaie").ToString

                                Dim leTaux As String = ""
                                query = "select TauxDevise from T_Devise where AbregeDevise='" & rwClass("Monnaie").ToString & "'"
                                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                                For Each rwClass1 As DataRow In dt1.Rows
                                    leTaux = rwClass1("TauxDevise").ToString
                                Next
                                Dim SignErreur As String = Mid(rwClass("ErreurCalcul").ToString, 1, 1)
                                DrE("Taux de change") = AfficherMonnaie(leTaux)
                                DrE("Montant de l'offre") = AfficherMonnaie(rwClass("MontantPropose").ToString)
                                DrE("Montant corrigé en monnaie d'évaluation") = AfficherMonnaie(rwClass("MontantAvecMonnaie").ToString)
                                DrE("Erreurs de calcul") = IIf(SignErreur = "-", SignErreur & " ", "").ToString & AfficherMonnaie(rwClass("ErreurCalcul").ToString.Replace("-", ""))
                                DrE("Sommes provisionnelles") = AfficherMonnaie(rwClass("SomProvision").ToString)
                                DrE("Montant rabais") = AfficherMonnaie(rwClass("MontantRabais").ToString)
                                DrE("Ajouts pour omission") = AfficherMonnaie(rwClass("AjoutOmission").ToString)
                                DrE("Ajustements") = AfficherMonnaie(rwClass("Ajustements").ToString)
                                DrE("Variations mineures") = AfficherMonnaie(rwClass("VariationMineure").ToString)
                                DrE("Prix Total de l'Offre") = AfficherMonnaie(rwClass("PrixCorrigeOffre").ToString)
                                DrE("Rang") = IIf(rwClass("RangExamDetaille").ToString <> "", rwClass("RangExamDetaille") & IIf(rwClass("RangExamDetaille") = "1", "er", "ème"), "-").ToString

                                dtExam.Rows.Add(DrE)
                            Next
                        End If
                    Next

                    TxtTypeExamen.Text = "RESULTAT EXAMEN DETAILLE"

                    'Il existe des critères post qualifications
                    If ExisteDesCriterePostQualifications = True Then
                        BtExecuter.Text = "Début examen" & vbNewLine & "post qualification"
                    Else
                        GetTerminerModifExamen()
                    End If

                    BtExecuter.Enabled = True
                    PanelLots.Enabled = False
                    CmbNumLot.Text = ""
                    cmbSousLot.Text = ""
                    TxtLibelleLot.Text = ""
                    TxtLibelleSousLot.Text = ""
                    TxtRefLot.Text = ""
                    TxtRefSousLot.Text = ""
                    TxtTypeExamen.ForeColor = Color.Black

                Else

                    Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDoss.Text)
                    Dim nbsouslot As Integer = Val(Resultat(0))

                    If nbsouslot > 0 Then
                        query = "select F.CodeFournis, F.NomFournis,S.* from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and F.CodeProjet='" & ProjetEnCours & "' and S.CodeLot='" & CmbNumLot.Text & "' and S.CodeSouslot='" & cmbSousLot.Text & "' and S.AcceptationExamDetaille='OUI' order by S.PrixCorrigeOffre,F.Nomfournis"
                    Else
                        query = "select F.CodeFournis, F.NomFournis, S.* from T_Fournisseur as F,T_SoumissionFournisseur AS S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and F.CodeProjet='" & ProjetEnCours & "' and S.CodeLot='" & CmbNumLot.Text & "' and S.AcceptationExamDetaille='OUI' order by S.PrixCorrigeOffre,F.Nomfournis"
                    End If

                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        Dim DrE = dtExam.NewRow()
                        cpt2 += 1
                        DrE("Code") = IIf(CDec(cpt2 / 2) = CDec(cpt2 \ 2), "x", "").ToString
                        DrE("CodeX") = ""

                        DrE("CodeRef") = rw("RefSoumis").ToString
                        DrE("Soumissionnaire") = MettreApost(rw("NomFournis").ToString)
                        DrE("Monnaie") = rw("Monnaie").ToString

                        Dim leTaux As String = ""
                        query = "select TauxDevise from T_Devise where AbregeDevise='" & rw("Monnaie").ToString & "'"
                        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw1 As DataRow In dt1.Rows
                            leTaux = rw1("TauxDevise").ToString
                        Next

                        Dim SignErreur As String = rw("SigneErreur").ToString
                        Dim RANG As String = rw("RangExamDetaille").ToString

                        DrE("Taux de change") = AfficherMonnaie(leTaux)
                        DrE("Montant de l'offre") = AfficherMonnaie(rw("MontantPropose").ToString)
                        DrE("Montant corrigé en monnaie d'évaluation") = AfficherMonnaie(rw("MontantAvecMonnaie").ToString)
                        DrE("Erreurs de calcul") = IIf(SignErreur = "-", SignErreur & " ", "").ToString & AfficherMonnaie(rw("ErreurCalcul").ToString.Replace("-", ""))
                        DrE("Sommes provisionnelles") = AfficherMonnaie(rw("SomProvision").ToString)
                        DrE("Montant rabais") = AfficherMonnaie(rw("MontantRabais").ToString)
                        DrE("Ajouts pour omission") = AfficherMonnaie(rw("AjoutOmission").ToString)
                        DrE("Ajustements") = AfficherMonnaie(rw("Ajustements").ToString)
                        DrE("Variations mineures") = AfficherMonnaie(rw("VariationMineure").ToString)
                        DrE("Prix Total de l'Offre") = AfficherMonnaie(rw("PrixCorrigeOffre").ToString)
                        DrE("Rang") = IIf(RANG.ToString <> "", RANG & IIf(RANG = "1", "er", "ème"), "-").ToString

                        dtExam.Rows.Add(DrE)
                    Next
                End If
            End If

            GridDetail.DataSource = dtExam

            ViewDetail.Columns("CodeX").Visible = False
            ViewDetail.Columns("Code").Visible = False
            ViewDetail.Columns("CodeRef").Visible = False

            If TypeAfficharge = True Then
                ViewDetail.Columns("Rang").Visible = True
            Else
                ViewDetail.Columns("Rang").Visible = False
            End If

            ViewDetail.Columns("Soumissionnaire").Width = 250
            ViewDetail.Columns(3).Width = 80
            ViewDetail.Columns(4).Width = 60
            ViewDetail.Columns(5).Width = 100
            ViewDetail.Columns(6).Width = 100
            ViewDetail.Columns(7).Width = 100
            ViewDetail.Columns(8).Width = 100
            ViewDetail.Columns(9).Width = 100
            ViewDetail.Columns(10).Width = 100
            ViewDetail.Columns(11).Width = 100
            ViewDetail.Columns(12).Width = 100
            ViewDetail.Columns(13).Width = 150
            ViewDetail.Columns("Rang").Width = 60

            ViewDetail.Columns("Code").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewDetail.Columns("CodeRef").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewDetail.Columns("Soumissionnaire").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewDetail.Columns("Rang").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right

            ViewDetail.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            For k As Integer = 4 To 14
                ViewDetail.Columns(k).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            Next
            ColorRowGrid(ViewDetail, "[Code]='x'", Color.LightGray, "Tahoma", 8, FontStyle.Regular, Color.Black)
            ColorRowGrid(ViewDetail, "[CodeX]='xx'", Color.LightBlue, "Tahoma", 8, FontStyle.Bold, Color.Black)
        End If
    End Sub

    Public Sub OffresTraitees()
        If (CmbNumDoss.Text <> "") Then
            dtTraite.Columns.Clear()
            dtTraite.Columns.Add("Code", Type.GetType("System.String"))
            dtTraite.Columns.Add("Fournisseur", Type.GetType("System.String"))
            dtTraite.Columns.Add("Accepté pour Examen détaillé", Type.GetType("System.String"))
            dtTraite.Columns.Add("Montant lu", Type.GetType("System.String"))
            dtTraite.Columns.Add("Montant corrigé", Type.GetType("System.String"))
            dtTraite.Columns.Add("Classement", Type.GetType("System.String"))
            dtTraite.Columns.Add("Post qualifié", Type.GetType("System.String"))
            dtTraite.Rows.Clear()

            query = "select RefLot,CodeLot from T_LotDAO where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' order by CodeLot"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows

                Dim DrT = dtTraite.NewRow()
                DrT("Code") = "x"
                DrT("Fournisseur") = "Lot N°" & rw("CodeLot").ToString
                dtTraite.Rows.Add(DrT)

                Dim dtSousLot As DataTable = ExcecuteSelectQuery("SELECT * FROM t_lotdao_souslot WHERE NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' AND RefLot='" & rw("RefLot") & "'")

                If dtSousLot.Rows.Count > 0 Then
                    For Each rw2 As DataRow In dtSousLot.Rows
                        Dim dtS = dtTraite.NewRow()
                        dtS("Code") = "x"
                        dtS("Fournisseur") = "Sous lot N°" & rw2("CodeSousLot").ToString
                        dtTraite.Rows.Add(dtS)

                        query = "select F.NomFournis,S.RefSoumis,S.AcceptationExamDetaille,S.ConformiteTechnique,S.MontantPropose,S.PrixCorrigeOffre,S.RangExamDetaille,S.ExamPQValide from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and S.RefLot='" & rw("RefLot").ToString & "' AND S.CodeSousLot='" & rw2("CodeSousLot").ToString & "' order by F.NomFournis"
                        Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw1 As DataRow In dt2.Rows
                            If (rw1("ConformiteTechnique").ToString <> "") Then

                                Dim DrT2 = dtTraite.NewRow()
                                DrT2("Code") = rw1("RefSoumis").ToString
                                DrT2("Fournisseur") = "  - " & MettreApost(rw1("NomFournis").ToString)
                                DrT2("Accepté pour Examen détaillé") = IIf(rw1("AcceptationExamDetaille").ToString <> "", IIf(rw1("AcceptationExamDetaille").ToString = "OUI", "OUI", "NON").ToString, "-").ToString
                                If (rw1("AcceptationExamDetaille").ToString = "OUI") Then
                                    DrT2("Montant lu") = AfficherMonnaie(rw1("MontantPropose").ToString)
                                    DrT2("Montant corrigé") = AfficherMonnaie(rw1("PrixCorrigeOffre").ToString)
                                    DrT2("Classement") = IIf(Val(rw1("RangExamDetaille").ToString) > 0, rw1("RangExamDetaille").ToString & IIf(rw1("RangExamDetaille").ToString = "1", "er", "ème").ToString, "-").ToString
                                    DrT2("Post qualifié") = rw1("ExamPQValide").ToString
                                Else
                                    DrT2("Montant lu") = "-"
                                    DrT2("Montant corrigé") = "-"
                                    DrT2("Classement") = "-"
                                    DrT2("Post qualifié") = "-"
                                End If
                                dtTraite.Rows.Add(DrT2)
                            End If
                        Next
                    Next

                Else

                    query = "select F.NomFournis,S.RefSoumis,S.ConformiteTechnique,S.AcceptationExamDetaille,S.MontantPropose,S.PrixCorrigeOffre,S.RangExamDetaille,S.ExamPQValide from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and S.RefLot='" & rw("RefLot").ToString & "' order by F.NomFournis"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt1.Rows
                        If (rw1("ConformiteTechnique").ToString <> "") Then

                            Dim DrT2 = dtTraite.NewRow()
                            DrT2("Code") = rw1("RefSoumis").ToString
                            DrT2("Fournisseur") = "  - " & MettreApost(rw1("NomFournis").ToString)
                            DrT2("Accepté pour Examen détaillé") = IIf(rw1("AcceptationExamDetaille").ToString <> "", IIf(rw1("AcceptationExamDetaille").ToString = "OUI", "OUI", "NON").ToString, "-").ToString
                            If (rw1("AcceptationExamDetaille").ToString = "OUI") Then
                                DrT2("Montant lu") = AfficherMonnaie(rw1("MontantPropose").ToString)
                                DrT2("Montant corrigé") = AfficherMonnaie(rw1("PrixCorrigeOffre").ToString)
                                DrT2("Classement") = IIf(Val(rw1("RangExamDetaille").ToString) > 0, rw1("RangExamDetaille").ToString & IIf(rw1("RangExamDetaille").ToString = "1", "er", "ème").ToString, "-").ToString
                                DrT2("Post qualifié") = rw1("ExamPQValide").ToString
                            Else
                                DrT2("Montant lu") = "-"
                                DrT2("Montant corrigé") = "-"
                                DrT2("Classement") = "-"
                                DrT2("Post qualifié") = "-"
                            End If

                            dtTraite.Rows.Add(DrT2)
                        End If
                    Next
                End If
            Next

            GridTraité.DataSource = dtTraite

            ViewTraité.Columns("Code").Visible = False
            ViewTraité.Columns("Post qualifié").Visible = False
            ViewTraité.Columns("Classement").Visible = False
            ViewTraité.Columns(1).Width = 250
            ViewTraité.Columns(2).Width = 150
            ViewTraité.Columns(3).Width = 150
            ViewTraité.Columns(4).Width = 150
            ViewTraité.Columns(5).Width = 150
            ViewTraité.Columns(6).Width = 150

            ViewTraité.Columns(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewTraité.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

            ViewTraité.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center                  'Drawing.StringAlignment.Center
            ViewTraité.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewTraité.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewTraité.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewTraité.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            ColorRowGrid(ViewTraité, "[Code]='x'", Color.LightGray, "Tahoma", 8, FontStyle.Bold, Color.Black)
            ColorRowGridAnal(ViewTraité, "[Post qualifié]='NON'", Color.White, "Tahoma", 8, FontStyle.Strikeout, Color.Black, False)
            ColorRowGridAnal(ViewTraité, "[Accepté pour Examen détaillé]='NON'", Color.White, "Tahoma", 8, FontStyle.Strikeout, Color.Black, False)
        End If
    End Sub

    Private Function InfosConform(ByVal Soumis As String, ByVal Critere As String) As String()

        Dim Mention As String = "-"
        Dim Comment As String = "-"
        Dim ValRet As String = ""
        query = "select Mention,Commentaire from T_SoumisFournisConformTech where RefConformTech='" & Critere & "' and RefSoumis='" & Soumis & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            Mention = rw(0).ToString
            Comment = MettreApost(rw(1).ToString)
        Next
        ValRet = Mention & "#" & Comment
        Return ValRet.Split("#"c)

    End Function

    Private Function Classement() As Integer
        Try
            Dim NbreClassement As Integer = 0

            If AttributionMarche = "Lot" Then
                Dim ListeLot As DataTable = ExcecuteSelectQuery("select * from t_lotdao where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "'")

                Dim RaisonChoix As String = "Classement évaluateurs"
                Dim Choix As String = ""
                Dim Rang As Integer = 1

                For Each rwLot As DataRow In ListeLot.Rows
                    Dim Resultat As Object() = GetSousLot(rwLot("CodeLot"), CmbNumDoss.Text)
                    Dim nbsouslot As Integer = Val(Resultat(0))

                    If nbsouslot > 0 Then
                        query = "SELECT S.CodeFournis, S.CodeLot, S.CodeSousLot, S.AcceptationExamDetaille, S.Monnaie, SUM(S.MontantPropose) as MontantPropose, SUM(S.MontantAvecMonnaie) as MontantAvecMonnaie, SUM(CAST(CONCAT(S.SigneErreur,ErreurCalcul) as DECIMAL(22,2))) as ErreurCalcul, SUM(S.SomProvision) as SomProvision, (SUM(S.MontantRabais)*100/SUM(S.PrixCorrigeOffre)) as PrctRabais, SUM(S.MontantRabais) as MontantRabais, SUM(`AjoutOmission`) as AjoutOmission, SUM(S.Ajustements) as Ajustements, SUM(S.VariationMineure) as VariationMineure, SUM(S.PrixCorrigeOffre) as PrixCorrigeOffre ,S.RangExamDetaille FROM t_soumissionfournisseur as S , t_fournisseur as F WHERE S.CodeLot='" & rwLot("CodeLot") & "' AND S.CodeFournis=F.CodeFournis AND S.AcceptationExamDetaille='OUI' AND F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' AND S.CodeFournis in (SELECT S.CodeFournis FROM t_soumissionfournisseur as S, t_fournisseur as F WHERE S.CodeFournis=F.CodeFournis AND S.AcceptationExamDetaille='OUI' AND F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' AND S.CodeLot='" & rwLot("CodeLot") & "' GROUP BY S.CodeFournis HAVING  COUNT(S.CodeFournis)=(SELECT COUNT(*) FROM t_lotdao_souslot WHERE RefLot=(SELECT RefLot from t_lotdao l WHERE CodeLot='" & rwLot("CodeLot") & "' and l.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "'))) GROUP BY CodeLot, S.CodeFournis ORDER BY PrixCorrigeOffre ASC;"
                        ' InputBox("Fodj0", "Fodj0", query)
                    Else
                        query = "SELECT S.CodeFournis, S.CodeLot, S.CodeSousLot, S.AcceptationExamDetaille, S.Monnaie, S.MontantPropose, S.MontantAvecMonnaie, CAST(CONCAT(S.SigneErreur,S.ErreurCalcul) as DECIMAL(22,2)) as ErreurCalcul, S.SomProvision, S.PrctRabais, S.MontantRabais, S.AjoutOmission, S.Ajustements, S.VariationMineure, S.PrixCorrigeOffre , S.RangExamDetaille FROM t_soumissionfournisseur as S , t_fournisseur as F WHERE S.CodeLot='" & rwLot("CodeLot") & "' AND S.CodeFournis=F.CodeFournis AND S.AcceptationExamDetaille='OUI' AND F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' ORDER BY S.PrixCorrigeOffre ASC"
                        'InputBox("Fodj1", "Fodj1", query)
                    End If

                    Dim dtClassement As DataTable = ExcecuteSelectQuery(query)
                    ' ExecuteNonQuery("DELETE FROM t_soumissionfournisseurclassement WHERE NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' AND CodeLot='" & CmbNumLot.Text & "' AND CodeSousLot='" & cmbSousLot.Text & "'")

                    ExecuteNonQuery("DELETE FROM t_soumissionfournisseurclassement WHERE NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' AND CodeLot='" & rwLot("CodeLot") & "'")

                    'S'il n'existe pas de critères post qualification
                    Rang = 1

                    If ExisteDesCriterePostQualifications = False Then
                        For Each rw In dtClassement.Rows
                            Choix = IIf(Rang = 1, "OUI", "NON").ToString

                            query = "INSERT INTO t_soumissionfournisseurclassement VALUES(NULL,'" & rw("CodeFournis").ToString & "','" & rwLot("CodeLot") & "','','" & rw("AcceptationExamDetaille").ToString & "','" & rw("Monnaie").ToString & "','" & CDbl(rw("MontantPropose").ToString) & "','" & CDbl(rw("MontantAvecMonnaie").ToString) & "','" & CDbl(rw("ErreurCalcul").ToString) & "','" & CDbl(rw("SomProvision").ToString) & "','" & rw("PrctRabais").ToString.Replace(",", ".") & "','" & CDbl(rw("MontantRabais").ToString) & "','" & CDbl(rw("AjoutOmission").ToString) & "','" & CDbl(rw("Ajustements").ToString) & "','" & CDbl(rw("VariationMineure").ToString) & "','" & CDbl(rw("PrixCorrigeOffre").ToString) & "','" & Rang & "','" & EnleverApost(CmbNumDoss.Text) & "',NULL,NULL,NULL,'" & Choix & "', '" & RaisonChoix & "',NULL,NULL)"
                            ExecuteNonQuery(query)
                            Rang += 1
                            NbreClassement += 1
                        Next

                    Else

                        For Each rw In dtClassement.Rows
                            query = "INSERT INTO t_soumissionfournisseurclassement VALUES(NULL,'" & rw("CodeFournis").ToString & "','" & rwLot("CodeLot") & "','','" & rw("AcceptationExamDetaille").ToString & "','" & rw("Monnaie").ToString & "','" & CDbl(rw("MontantPropose").ToString) & "','" & CDbl(rw("MontantAvecMonnaie").ToString) & "','" & CDbl(rw("ErreurCalcul").ToString) & "','" & CDbl(rw("SomProvision").ToString) & "','" & rw("PrctRabais").ToString.Replace(",", ".") & "','" & CDbl(rw("MontantRabais").ToString) & "','" & CDbl(rw("AjoutOmission").ToString) & "','" & CDbl(rw("Ajustements").ToString) & "','" & CDbl(rw("VariationMineure").ToString) & "','" & CDbl(rw("PrixCorrigeOffre").ToString) & "','" & Rang & "','" & EnleverApost(CmbNumDoss.Text) & "',NULL,NULL,NULL,NULL,NULL,NULL,NULL)"
                            ExecuteNonQuery(query)
                            Rang += 1
                            NbreClassement += 1
                        Next
                    End If

                Next
            End If

            Return NbreClassement

        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
            Return 0
        End Try
    End Function

    Private Function ClassementPostQualif() As Boolean
        Try
            Dim ListeLot As DataTable = ExcecuteSelectQuery("select * from t_lotdao where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "'")
            Dim RaisonChoix As String = "Classement évaluateurs"
            Dim Choix As String = ""
            Dim rang As Decimal = 0
            For Each rwLot As DataRow In ListeLot.Rows
                rang = 0
                Choix = ""

                query = "select CodeFournis, PrixCorrigeOffre from t_soumissionfournisseurclassement where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and CodeLot='" & rwLot("CodeLot") & "' AND ExamPQValide='OUI' ORDER BY PrixCorrigeOffre ASC" 'CodeSouslot='" & cmbSousLot.Text & "'
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                If dt0.Rows.Count > 0 Then
                    For Each rw As DataRow In dt0.Rows
                        rang += 1
                        Choix = IIf(rang = 1, "OUI", "NON").ToString
                        query = "update t_soumissionfournisseurclassement set RangPostQualif='" & rang.ToString & "', Selectionne='" & Choix & "', MotifSelect='" & RaisonChoix & "' where CodeFournis='" & rw("CodeFournis") & "' AND CodeLot='" & rwLot("CodeLot") & "' AND NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "'" 'AND CodeSousLot='" & cmbSousLot.Text & "'
                        ExecuteNonQuery(query)
                    Next
                End If
            Next
            Return True
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
            Return False
        End Try
    End Function

    Private Sub BilanExamOffres()
        AfficherGrid(EtapeActuelle)

        If (CmbNumDoss.Text <> "") Then
            dtExam.Columns.Clear()
            dtExam.Columns.Add("Code", Type.GetType("System.String"))
            dtExam.Columns.Add("Soumissionnaire", Type.GetType("System.String"))
            dtExam.Columns.Add("Prix de l'offre en chiffre", Type.GetType("System.String"))
            dtExam.Columns.Add("Prix de l'offre en lettre", Type.GetType("System.String"))
            dtExam.Columns.Add("Classement", Type.GetType("System.String"))
            dtExam.Rows.Clear()

            query = "select RefLot,CodeLot from T_LotDAO where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' order by CodeLot"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                Dim DrT = dtExam.NewRow()

                DrT("Code") = "x"
                DrT("Soumissionnaire") = "Lot N°" & rw("CodeLot").ToString
                dtExam.Rows.Add(DrT)

                query = "select F.NomFournis,'',S.PrixCorrigeOffre,S.RangPostQualif from T_Fournisseur as F,t_soumissionfournisseurclassement as S where F.CodeFournis=S.CodeFournis and S.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and S.CodeLot='" & rw("CodeLot").ToString & "' and S.ExamPQValide='OUI' order by S.RangPostQualif"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows

                    Dim DrT2 = dtExam.NewRow()
                    DrT2("Code") = rw1(1).ToString
                    DrT2("Soumissionnaire") = "  - " & MettreApost(rw1("NomFournis").ToString)
                    DrT2("Prix de l'offre en chiffre") = AfficherMonnaie(rw1("PrixCorrigeOffre").ToString.Replace(" ", "")) & "  HT"
                    DrT2("Prix de l'offre en lettre") = MontantLettre(rw1("PrixCorrigeOffre").ToString.Replace(" ", ""))
                    DrT2("Classement") = rw1("RangPostQualif").ToString & IIf(rw1("RangPostQualif").ToString = "1", "er", "ème").ToString

                    dtExam.Rows.Add(DrT2)
                Next
            Next
            GridBilan.DataSource = dtExam

            ViewBilan.Columns(0).Visible = False
            ViewBilan.Columns(1).Width = 250
            ViewBilan.Columns(2).Width = 150
            ViewBilan.Columns(3).Width = GridBilan.Width - 518
            ViewBilan.Columns(4).Width = 100

            ViewBilan.Columns(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewBilan.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

            ViewBilan.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far                  'Drawing.StringAlignment.Center
            ViewBilan.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            ColorRowGrid(ViewBilan, "[Code]='x'", Color.LightGray, "Tahoma", 8, FontStyle.Bold, Color.Black)
            ColorRowGridAnal(ViewBilan, "[Classement]='1er'", Color.White, "Tahoma", 8, FontStyle.Bold, Color.Navy, False)
            'ColorRowGridAnal(ViewBilan, "[Classement]<>'1er'", Color.White, "Tahoma", 8, FontStyle.Strikeout, Color.Black, False)
        End If
    End Sub

#End Region

#Region "Liste des methodes et code non utilisés"

#Region "Information Cojo"

    Private Sub TxtCodePresence_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtCodePresence.KeyDown
        If (e.KeyCode = Keys.Enter And CmbNumDoss.Text <> "") Then
            If (TxtCodePresence.Text <> "") Then
                Dim CodMembre As String = ""
                Dim PASS = TxtCodePresence.Text
                query = "select CodeMem,Evaluation from T_Commission where NumeroDAO='" & CmbNumDoss.Text & "' and PasseMem='" & PASS & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    If (rw(1).ToString <> "") Then
                        SuccesMsg("Code déjà entré!")
                        TxtCodePresence.Text = ""
                        Exit Sub
                    End If
                    CodMembre = rw(0).ToString
                Next

                If (CodMembre <> "") Then
                    query = "update T_Commission set Evaluation='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' where CodeMem='" & CodMembre & "'"
                    ExecuteNonQuery(query)
                    TxtCodePresence.Text = ""
                    RemplirCojo()
                Else
                    SuccesMsg("Accès réfusé!")
                End If

            End If

        End If
    End Sub

    Private Sub RemplirCojo()
        If (CmbNumDoss.Text <> "") Then
            dt.Columns.Clear()
            dt.Columns.Add("Commission", Type.GetType("System.String"))
            'Dim Reader As MySqlDataReader
            dt.Rows.Clear()
            query = "select NomMem,TitreMem from T_Commission where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and Evaluation<>''"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                Dim drS = dt.NewRow()
                drS(0) = MettreApost(rw(0).ToString) & " (" & rw(1).ToString & ")"
                dt.Rows.Add(drS)
            Next
            GridCojo.DataSource = dt
            If (GridViewComJugmt.RowCount > 0) Then
                BtExecuter.Text = "DEBUT"
                BtExecuter.Enabled = True
            Else
                BtExecuter.Enabled = False
            End If
        End If
    End Sub

#End Region

#Region "Valider le Verdit"
    Private Sub BtEnrgVerdict_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgVerdict.Click
        If (ChkConforme.Checked = True Or ChkNonConforme.Checked = True) Then

            If (TxtTypeMarche.Text = "Fournitures") Then

                Dim AnalyseOk As Boolean = True
                query = "select * from T_SoumisCaractFournit where RefSoumis='" & TxtRefSoumis.Text & "' and MentionValeur=''"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    AnalyseOk = False
                    Exit For
                Next
                If (AnalyseOk = True) Then
                    If (ViewTravail.RowCount > 0) Then

                        If ConfirmMsg("Confirmation du verdict.") = DialogResult.Yes Then
                            query = "update T_SoumissionFournisseur set ConformiteTechnique='" & IIf(ChkConforme.Checked = True, "OUI", "NON").ToString & "' where RefSoumis='" & TxtRefSoumis.Text & "'"
                            ExecuteNonQuery(query)
                            SuccesMsg("Verdict enregistré avec succès!.")
                            ChkConforme.Checked = False
                            ChkNonConforme.Checked = False
                            PanelVerdict.Enabled = False
                            dtExam.Columns.Clear()
                            dtExam.Rows.Clear()
                            GridTravail.Refresh()
                            CmbSoumis.Properties.Items.Remove(CmbSoumis.Text)

                            TxtAdresseSoumis.Text = ""
                            CmbSoumis.Text = ""
                        End If
                    Else
                        SuccesMsg("Aucune information dans la grille!.")
                        Exit Sub
                    End If
                Else
                    SuccesMsg("Analyse incomplète!.")
                    Exit Sub
                End If

                OffresTraitees()

            Else                 'Travaux **************************

                Dim AnalyseOk As Boolean = True
                'Dim CmdAO As MySqlCommand = sqlconn.CreateCommand
                'Dim ReaderAO As MySqlDataReader
                'CmdAO.CommandText = "select Commentaire from T_SoumisPrixItemDQE where RefSoumis='" & TxtRefSoumis.Text & "' and Mention=''"
                '
                'ReaderAO = CmdAO.ExecuteReader()
                'If ReaderAO.Read() Then
                '    If (ReaderAO.GetValue(0).ToString <> "Somme provisionnelle") Then
                '        AnalyseOk = False
                '    End If
                'End If
                'ReaderAO.Close()
                '

                For k As Integer = 0 To ViewTravail.RowCount - 1
                    If (dtExam.Rows(k).Item(0).ToString <> "x") Then
                        If (dtExam.Rows(k).Item(3).ToString <> "Conforme" And dtExam.Rows(k).Item(3).ToString <> "Non Conforme") Then
                            AnalyseOk = False
                            Exit For
                        End If
                    End If
                Next

                If (AnalyseOk = True) Then

                    If (ViewTravail.RowCount > 0) Then

                        Dim RepVerdict As MsgBoxResult = MsgBox("Confirmation du verdict.", MsgBoxStyle.OkCancel)

                        If (RepVerdict = MsgBoxResult.Ok) Then
                            query = "update T_SoumissionFournisseur set ConformiteTechnique='" & IIf(ChkConforme.Checked = True, "OUI", "NON").ToString & "' where RefSoumis='" & TxtRefSoumis.Text & "'"
                            ExecuteNonQuery(query)
                            MsgBox("Verdict enregistré avec succès!", MsgBoxStyle.Information)
                            ChkConforme.Checked = False
                            'ChkExhaustif.Checked = False
                            ChkNonConforme.Checked = False
                            'ChkNonExhaustif.Checked = False
                            PanelVerdict.Enabled = False
                            dtExam.Columns.Clear()
                            dtExam.Rows.Clear()
                            GridTravail.Refresh()
                            CmbSoumis.Properties.Items.Remove(CmbSoumis.Text)

                            TxtAdresseSoumis.Text = ""
                            CmbSoumis.Text = ""
                            'TxtLibelleLot.Text = ""
                            'CmbNumLot.Text = ""
                        End If
                    Else
                        MsgBox("Aucune information dans la grille!", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                Else
                    MsgBox("Analyse incomplète!", MsgBoxStyle.Information)
                    Exit Sub
                End If

                OffresTraitees()
            End If
        End If
    End Sub

#End Region

#Region "Traitement effectué. Mais code non utiliser dans ClearProject"

    Private Sub ChargerExamPrelim()
        If (CmbNumLot.Text <> "") Then
            AfficherGrid(EtapeActuelle)

            dtExam.Columns.Clear()
            dtExam.Columns.Add("CodeRef", Type.GetType("System.String"))
            dtExam.Columns.Add("Soumissionnaire", Type.GetType("System.String"))
            dtExam.Columns.Add("Vérification", Type.GetType("System.String"))
            dtExam.Columns.Add("Critères de provenance", Type.GetType("System.String"))
            dtExam.Columns.Add("Conformité aux spécifications techniques", Type.GetType("System.String"))
            dtExam.Columns.Add("Garantie de l'offre", Type.GetType("System.String"))
            dtExam.Columns.Add("Exhaustivité de l'offre", Type.GetType("System.String"))
            dtExam.Columns.Add("Conformité pour l'essentiel", Type.GetType("System.String"))
            dtExam.Columns.Add("Acceptation pour examen détaillé", Type.GetType("System.String"))
            dtExam.Columns.Add("Code", Type.GetType("System.String"))

            Dim cpt2 As Decimal = 0
            dtExam.Rows.Clear()
            query = "select F.NomFournis,F.PaysFournis,S.RefSoumis,S.ConformiteTechnique,S.ConformiteGarantie,S.CautionBancaire,S.ExhaustiviteOffre,S.ConformiteEssentiel,S.AcceptationExamDetaille,S.Verification,S.ConformiteProvenance from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and F.CodeProjet='" & ProjetEnCours & "' and S.CodeLot='" & CmbNumLot.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                cpt2 += 1
                Dim DrE = dtExam.NewRow()
                DrE("Code") = IIf(CDec(cpt2 / 2) = CDec(cpt2 \ 2), "x", "").ToString
                DrE("Soumissionnaire") = MettreApost(rw(0).ToString)
                DrE("Vérification") = IIf(rw(9).ToString <> "", rw(9).ToString, "-")    'IIf(rw(9).ToString <> "", rw(9).ToString, "-").ToString

                Dim ProvOk As Boolean = True
                query = "select DateDebutSanction,DateFinSanction from T_SanctionPays where PaysSanction='" & rw("PaysFournis").ToString & "'"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows
                    If (DateTime.Compare(Now.ToShortDateString, CDate(rw(0).ToString)) >= 0 And DateTime.Compare(Now.ToShortDateString, CDate(rw(1).ToString)) <= 0) Then
                        ProvOk = False
                    End If
                Next

                If (rw(1).ToString = "") Then ProvOk = False

                DrE("Critères de provenance") = IIf(rw(10).ToString <> "", rw(10).ToString, IIf(ProvOk = True, "OUI", "NON").ToString).ToString
                DrE("Conformité aux spécifications techniques") = IIf(rw("ConformiteTechnique").ToString <> "", rw("ConformiteTechnique").ToString, "-")

                Dim GarantiOk As Boolean = False
                query = "select MontantGarantie from T_LotDAO where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and CodeLot='" & CmbNumLot.Text & "'"
                dt1 = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows
                    Dim montGar As Decimal = 0
                    If (rw1("MontantGarantie").ToString <> "") Then
                        If (IsNumeric(rw1("MontantGarantie").ToString) = True) Then
                            montGar = CDec(rw1("MontantGarantie").ToString)
                        Else
                            montGar = 0
                        End If
                    Else
                        montGar = 0
                    End If

                    Dim montGarOffre As Decimal = 0
                    If (rw1("MontantGarantie").ToString <> "") Then
                        If rw("CautionBancaire").ToString <> "" Then
                            montGarOffre = IIf(rw("CautionBancaire").ToString = "", 0, IIf(IsNumeric(rw("CautionBancaire").ToString) = True, CDec(rw("CautionBancaire").ToString), 0))
                        End If
                    End If

                    If (montGarOffre >= montGar) Then
                        GarantiOk = True
                    End If
                Next

                DrE("Garantie de l'offre") = IIf(GarantiOk = True, "OUI", "NON").ToString
                DrE("Exhaustivité de l'offre") = IIf(rw("ExhaustiviteOffre").ToString <> "", rw("ExhaustiviteOffre").ToString, "-")
                DrE("Conformité pour l'essentiel") = IIf(rw("ConformiteEssentiel").ToString <> "", rw("ConformiteEssentiel").ToString, "-")
                DrE("Acceptation pour examen détaillé") = IIf(rw("AcceptationExamDetaille").ToString <> "", rw("AcceptationExamDetaille").ToString, "-")
                DrE("CodeRef") = rw("RefSoumis").ToString
                dtExam.Rows.Add(DrE)
            Next

            GridPrelim.DataSource = dtExam

            ViewPrelim.Columns(0).Visible = False
            ViewPrelim.OptionsView.ColumnAutoWidth = True
            ViewPrelim.Columns(9).Visible = False

            For k As Integer = 2 To 8
                ViewPrelim.Columns(k).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            Next
            ColorRowGrid(ViewPrelim, "[Code]='x'", Color.LightGray, "Tahoma", 8, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewPrelim, "[Acceptation pour examen détaillé]='NON'", Color.White, "Tahoma", 8, FontStyle.Regular, Color.Red, False)
        End If
    End Sub

    Private Sub AccepterAvecCommentaire_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AccepterAvecCommentaire.Click
        If (TxtTypeMarche.Text = "Fournitures") Then

            Accord = "Conforme"
            ReponseDialog = EtapeActuelle
            ExceptRevue = ""
            ExceptRevue2 = ""
            AccordCommentaire.ShowDialog()

            If (ReponseDialog <> "") Then
                If SpecDemande = "oui" Then
                    VerifCaract(CodeActuel, TxtRefSoumis.Text)
                ElseIf SpecDemande = "non" Then
                    VerifCaractPropose(CodeActuel, TxtRefSoumis.Text)
                End If
                If (ExceptRevue <> "OUI" And ExceptRevue2 <> "OUI") Then

                    If SpecDemande = "oui" Then
                        query = "update T_SoumisCaractFournit set MentionValeur='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefSpecCaract='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                        ExecuteNonQuery(query)
                    ElseIf SpecDemande = "non" Then
                        query = "update t_soumiscaractfournitsupl set MentionValeur='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefSpecCaract='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                        ExecuteNonQuery(query)
                    End If
                Else

                    If (ExceptRevue = "OUI") Then        'Cas similaires

                        For k As Integer = 0 To ViewTravail.RowCount - 1
                            If (dtExam.Rows(k).Item(3).ToString = "...") Then
                                Dim laRef As String = dtExam.Rows(k).Item(0).ToString
                                VerifCaract(laRef, TxtRefSoumis.Text)
                                query = "update T_SoumisCaractFournit set MentionValeur='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefSpecCaract='" & laRef & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                                ExecuteNonQuery(query)
                            Else
                            End If
                        Next
                        ExceptRevue = ""

                    End If

                    If (ExceptRevue2 = "OUI") Then       'toute la rubrique

                        Dim CodeRubrique As String = ""
                        Dim CodeTrouve As Boolean = False
                        Dim PosRow As Decimal = ViewTravail.GetDataSourceRowIndex(ViewTravail.FocusedRowHandle)

                        While CodeTrouve = False
                            PosRow = PosRow - 1
                            If (dtExam.Rows(PosRow).Item(0).ToString = "x") Then
                                CodeTrouve = True
                                Dim CodePart() As String = dtExam.Rows(PosRow).Item(1).ToString.Split(":"c)
                                CodeRubrique = CodePart(0).Replace(" ", "")
                            End If
                        End While

                        Dim RefRub(100) As String
                        Dim cpt1 As Decimal = 0
                        query = "select C.RefSpecCaract from T_SpecTechFourniture as F,T_SpecTechCaract as C where F.RefSpecFournit=C.RefSpecFournit and F.NumeroDAO='" & CmbNumDoss.Text & "' and F.CodeFournit='" & CodeRubrique & "' and F.CodeLot='" & CmbNumLot.Text & "'"
                        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw As DataRow In dt0.Rows
                            RefRub(cpt1) = rw(0).ToString
                            cpt1 += 1
                        Next

                        For k As Integer = 0 To cpt1 - 1
                            VerifCaract(RefRub(k), TxtRefSoumis.Text)
                            query = "update T_SoumisCaractFournit set MentionValeur='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefSpecCaract='" & RefRub(k) & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                            ExecuteNonQuery(query)
                        Next
                        ExceptRevue2 = ""
                    End If

                End If
                ReponseDialog = ""
                ExceptRevue = ""
                ExceptRevue2 = ""
            End If

            Accord = ""
            ChargerGridExam(EtapeActuelle)

        Else           'Travaux *************************

            Accord = "Conforme"
            ReponseDialog = EtapeActuelle
            ExceptRevue = ""
            ExceptRevue2 = ""
            AccordCommentaire.ShowDialog()

            VerifItem(CodeActuel, TxtRefSoumis.Text)

            If (ReponseDialog <> "") Then

                If (ExceptRevue <> "OUI" And ExceptRevue2 <> "OUI") Then
                    query = "update T_SoumisFournisConformTech set Mention='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "'  where RefConformTech='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                    ExecuteNonQuery(query)
                Else

                    If (ExceptRevue2 = "OUI") Then       'toute la rubrique

                        Dim CodeRubrique As String = ""
                        Dim CodeTrouve As Boolean = False
                        Dim PosRow As Decimal = ViewTravail.GetDataSourceRowIndex(ViewTravail.FocusedRowHandle)

                        While CodeTrouve = False
                            PosRow = PosRow - 1
                            If (dtExam.Rows(PosRow).Item(0).ToString = "x") Then
                                CodeTrouve = True
                                CodeRubrique = dtExam.Rows(PosRow).Item(1).ToString
                            End If
                        End While

                        Dim RefRub(100) As String
                        Dim cpt1 As Decimal = 0
                        query = "select RefConformTech from T_DAO_ConformTech where RefConformMere='" & CodeRubrique & "' and NumeroDAO='" & CmbNumDoss.Text & "'"
                        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw As DataRow In dt0.Rows
                            RefRub(cpt1) = rw(0).ToString
                            cpt1 += 1
                        Next

                        For k As Integer = 0 To cpt1 - 1
                            VerifItem(RefRub(k), TxtRefSoumis.Text)
                            query = "update T_SoumisFournisConformTech set Mention='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefConformTech='" & RefRub(k) & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                            ExecuteNonQuery(query)
                        Next
                        ExceptRevue2 = ""
                    End If
                End If
                ReponseDialog = ""
                ExceptRevue = ""
                ExceptRevue2 = ""
            End If

            Accord = ""
            ChargerGridExam(EtapeActuelle)
        End If
    End Sub

    Private Sub AccepterSansCommentaire_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AccepterSansCommentaire.Click
        If (TxtTypeMarche.Text = "Fournitures") Then
            Accord = "Conforme"
            If SpecDemande = "oui" Then
                VerifCaract(CodeActuel, TxtRefSoumis.Text)
                query = "update T_SoumisCaractFournit set MentionValeur='" & Accord & "', Commentaire='' where RefSpecCaract='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                ExecuteNonQuery(query)
            ElseIf SpecDemande = "non" Then
                VerifCaractPropose(CodeActuel, TxtRefSoumis.Text)
                query = "update t_soumiscaractfournitsupl set MentionValeur='" & Accord & "', Commentaire='' where RefSpecCaract='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                ExecuteNonQuery(query)
            End If
            Accord = ""
            ChargerGridExam(EtapeActuelle)

        Else                    'Travaux ************************

            Accord = "Conforme"

            VerifItem(CodeActuel, TxtRefSoumis.Text)
            query = "update T_SoumisFournisConformTech set Mention='" & Accord & "', Commentaire='' where RefConformTech='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
            ExecuteNonQuery(query)
            Accord = ""
            ChargerGridExam(EtapeActuelle)
        End If
    End Sub

    Private Sub RejeterAvecCommentaire_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RejeterAvecCommentaire.Click

        If (TxtTypeMarche.Text = "Fournitures") Then

            Accord = "Non Conforme"
            ReponseDialog = EtapeActuelle
            ExceptRevue = ""
            ExceptRevue2 = ""
            AccordCommentaire.ShowDialog()

            If (ReponseDialog <> "") Then

                If SpecDemande = "oui" Then
                    VerifCaract(CodeActuel, TxtRefSoumis.Text)
                ElseIf SpecDemande = "non" Then
                    VerifCaractPropose(CodeActuel, TxtRefSoumis.Text)
                End If
                If (ExceptRevue <> "OUI" And ExceptRevue2 <> "OUI") Then
                    If SpecDemande = "oui" Then
                        query = "update T_SoumisCaractFournit set MentionValeur='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefSpecCaract='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                        ExecuteNonQuery(query)
                    ElseIf SpecDemande = "non" Then
                        query = "update t_soumiscaractfournitsupl set MentionValeur='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefSpecCaract='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                        ExecuteNonQuery(query)
                    End If
                Else

                    If (ExceptRevue = "OUI") Then        'Cas similaires

                        For k As Integer = 0 To ViewTravail.RowCount - 1
                            If (dtExam.Rows(k).Item(3).ToString = "...") Then
                                Dim laRef As String = dtExam.Rows(k).Item(0).ToString

                                VerifCaract(laRef, TxtRefSoumis.Text)
                                query = "update T_SoumisCaractFournit set MentionValeur='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefSpecCaract='" & laRef & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                                ExecuteNonQuery(query)
                            Else
                            End If
                        Next
                        ExceptRevue = ""

                    End If

                    If (ExceptRevue2 = "OUI") Then       'toute la rubrique

                        Dim CodeRubrique As String = ""
                        Dim CodeTrouve As Boolean = False
                        Dim PosRow As Decimal = ViewTravail.GetDataSourceRowIndex(ViewTravail.FocusedRowHandle)

                        While CodeTrouve = False
                            PosRow = PosRow - 1
                            If (dtExam.Rows(PosRow).Item(0).ToString = "x") Then
                                CodeTrouve = True
                                Dim CodePart() As String = dtExam.Rows(PosRow).Item(1).ToString.Split(":"c)
                                CodeRubrique = CodePart(0).Replace(" ", "")
                            End If
                        End While

                        Dim RefRub(100) As String
                        Dim cpt1 As Decimal = 0
                        query = "select C.RefSpecCaract from T_SpecTechFourniture as F,T_SpecTechCaract as C where F.RefSpecFournit=C.RefSpecFournit and F.NumeroDAO='" & CmbNumDoss.Text & "' and F.CodeFournit='" & CodeRubrique & "'"
                        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw As DataRow In dt0.Rows
                            RefRub(cpt1) = rw(0).ToString
                            cpt1 += 1
                        Next

                        For k As Integer = 0 To cpt1 - 1

                            VerifCaract(RefRub(k), TxtRefSoumis.Text)
                            query = "update T_SoumisCaractFournit set MentionValeur='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefSpecCaract='" & RefRub(k) & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                            ExecuteNonQuery(query)
                        Next

                        ExceptRevue2 = ""
                    End If
                End If
                ReponseDialog = ""
                ExceptRevue = ""
                ExceptRevue2 = ""
            End If

            Accord = ""
            ChargerGridExam(EtapeActuelle)

        Else               'Travaux *********************

            Accord = "Non Conforme"
            ReponseDialog = EtapeActuelle
            ExceptRevue = ""
            ExceptRevue2 = ""
            AccordCommentaire.ShowDialog()

            VerifItem(CodeActuel, TxtRefSoumis.Text)

            If (ReponseDialog <> "") Then

                If (ExceptRevue <> "OUI" And ExceptRevue2 <> "OUI") Then
                    query = "update T_SoumisFournisConformTech set Mention='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefConformTech='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                    ExecuteNonQuery(query)
                Else

                    If (ExceptRevue2 = "OUI") Then       'toute la rubrique

                        Dim CodeRubrique As String = ""
                        Dim CodeTrouve As Boolean = False
                        Dim PosRow As Decimal = ViewTravail.GetDataSourceRowIndex(ViewTravail.FocusedRowHandle)

                        While CodeTrouve = False
                            PosRow = PosRow - 1
                            If (dtExam.Rows(PosRow).Item(0).ToString = "x") Then
                                CodeTrouve = True
                                CodeRubrique = dtExam.Rows(PosRow).Item(1).ToString
                            End If
                        End While

                        Dim RefRub(100) As String
                        Dim cpt1 As Decimal = 0
                        query = "select RefConformTech from T_DAO_ConformTech where RefConformMere='" & CodeRubrique & "' and NumeroDAO='" & CmbNumDoss.Text & "'"
                        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw As DataRow In dt0.Rows
                            RefRub(cpt1) = rw(0).ToString
                            cpt1 += 1
                        Next

                        For k As Integer = 0 To cpt1 - 1

                            VerifItem(RefRub(k), TxtRefSoumis.Text)
                            query = "update T_SoumisFournisConformTech set Mention='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefConformTech='" & RefRub(k) & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                            ExecuteNonQuery(query)
                        Next

                        ExceptRevue2 = ""
                    End If

                End If
                ReponseDialog = ""
                ExceptRevue = ""
                ExceptRevue2 = ""
            End If

            Accord = ""
            ChargerGridExam(EtapeActuelle)
        End If
    End Sub

    Private Sub RejeterSansCommentaire_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RejeterSansCommentaire.Click

        If (TxtTypeMarche.Text = "Fournitures") Then

            Accord = "Non Conforme"
            If SpecDemande = "oui" Then
                VerifCaract(CodeActuel, TxtRefSoumis.Text)
                query = "update T_SoumisCaractFournit set MentionValeur='" & Accord & "', Commentaire='' where RefSpecCaract='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                ExecuteNonQuery(query)
            ElseIf SpecDemande = "non" Then
                VerifCaractPropose(CodeActuel, TxtRefSoumis.Text)
                query = "update t_soumiscaractfournitsupl set MentionValeur='" & Accord & "', Commentaire='' where RefSpecCaract='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                ExecuteNonQuery(query)
            End If
            Accord = ""
            ChargerGridExam(EtapeActuelle)

        Else         'Travaux **********************

            Accord = "Non Conforme"

            VerifItem(CodeActuel, TxtRefSoumis.Text)
            query = "update T_SoumisFournisConformTech set Mention='" & Accord & "', Commentaire='' where RefConformTech='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
            ExecuteNonQuery(query)
            Accord = ""
            ChargerGridExam(EtapeActuelle)
        End If
    End Sub

    Private Sub VerifItem(ByVal ItemCible As String, ByVal Soumis As String)

        Dim ligneExist As Boolean = False
        query = "select * from T_SoumisFournisConformTech where RefConformTech='" & ItemCible & "' and RefSoumis='" & Soumis & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            ligneExist = True
            Exit For
        Next

        If (ligneExist = False) Then

            Dim DatSet = New DataSet
            query = "select * from T_SoumisFournisConformTech"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_SoumisFournisConformTech")
            Dim DatTable = DatSet.Tables("T_SoumisFournisConformTech")
            Dim DatRow = DatSet.Tables("T_SoumisFournisConformTech").NewRow()

            DatRow("RefConformTech") = ItemCible
            DatRow("RefSoumis") = Soumis

            DatSet.Tables("T_SoumisFournisConformTech").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_SoumisFournisConformTech")
            DatSet.Clear()
            BDQUIT(sqlconn)
        End If

    End Sub
    Private Sub ChargerAnalyse()
        If CmbSoumis.Text = "" Then
            dtExam.Rows.Clear()
        End If
        If (CmbSoumis.Text <> "") Then
            AfficherGrid(EtapeActuelle)

            If (TxtTypeMarche.Text = "Fournitures") Then

                dtExam.Columns.Clear()
                dtExam.Columns.Add("Code", Type.GetType("System.String"))
                dtExam.Columns.Add("Spécifications techniques", Type.GetType("System.String"))
                dtExam.Columns.Add("Valeurs demandées", Type.GetType("System.String"))
                dtExam.Columns.Add("Valeurs offertes", Type.GetType("System.String"))
                dtExam.Columns.Add("Conformité / Commentaire", Type.GetType("System.String"))
                dtExam.Columns.Add("NC", Type.GetType("System.String"))
                dtExam.Columns.Add("SpecDemandé", Type.GetType("System.String"))
                dtExam.Rows.Clear()
                Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDoss.Text)
                Dim nbsouslot As Integer = Val(Resultat(0))
                If nbsouslot > 0 Then
                    query = "select RefSpecFournit,CodeFournit,DescripFournit from T_SpecTechFourniture where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and CodeLot='" & CmbNumLot.Text & "' and CodeSousLot='" & cmbSousLot.Text & "'"
                Else
                    query = "select RefSpecFournit,CodeFournit,DescripFournit from T_SpecTechFourniture where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and CodeLot='" & CmbNumLot.Text & "'"
                End If
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows

                    Dim FournitExist As Boolean = True
                    query = "select RefSpecCaract,LibelleCaract,ValeurCaract from T_SpecTechCaract where RefSpecFournit='" & rw("RefSpecFournit").ToString & "'"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)

                    For Each rw1 As DataRow In dt1.Rows
                        If (FournitExist = True) Then
                            Dim drS = dtExam.NewRow()
                            drS("Code") = "x"
                            drS("Spécifications techniques") = rw("CodeFournit").ToString.ToUpper & " : " & MettreApost(rw("DescripFournit").ToString).ToUpper
                            drS("Valeurs demandées") = ""
                            drS("Valeurs offertes") = ""

                            query = "select PrixUnitaire from T_SoumisPrixFourniture where RefSpecFournit='" & rw("RefSpecFournit").ToString & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                            Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                            If dt2.Rows.Count > 0 Then
                                If (dt2.Rows(0).Item(0).ToString <> "") Then
                                    drS("Conformité / Commentaire") = "Prix unitaire Htva : " & AfficherMonnaie(dt2.Rows(0).Item(0).ToString)
                                Else
                                    drS("Conformité / Commentaire") = ""
                                End If
                            Else
                                drS("Conformité / Commentaire") = ""
                            End If
                            dtExam.Rows.Add(drS)
                            FournitExist = False
                        End If

                        Dim drC = dtExam.NewRow()
                        drC("Code") = rw1("RefSpecCaract").ToString
                        drC("Spécifications techniques") = MettreApost(rw1("LibelleCaract").ToString)
                        drC("Valeurs demandées") = MettreApost(rw1("ValeurCaract").ToString)

                        query = "select ValeurOfferte,MentionValeur,Commentaire from T_SoumisCaractFournit where RefSpecCaract='" & rw1("RefSpecCaract").ToString & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                        Dim dtx As DataTable = ExcecuteSelectQuery(query)
                        If dtx.Rows.Count > 0 Then
                            If (dtx.Rows(0).Item(0).ToString.Replace(" ", "") <> "") Then
                                drC("Valeurs offertes") = MettreApost(dtx.Rows(0).Item(0).ToString)
                            Else
                                drC("Valeurs offertes") = "..."
                            End If
                            drC("Conformité / Commentaire") = IIf(dtx.Rows(0).Item(1).ToString <> "", dtx.Rows(0).Item(1).ToString, "").ToString & IIf(dtx.Rows(0).Item(2).ToString <> "", " : " & MettreApost(dtx.Rows(0).Item(2).ToString), "")
                            drC("NC") = IIf(dtx.Rows(0).Item(1).ToString = "Non Conforme", "x", "").ToString
                        Else
                            drC("Valeurs offertes") = "..."
                            drC("Conformité / Commentaire") = "..."
                        End If
                        drC("SpecDemandé") = "oui"
                        dtExam.Rows.Add(drC)

                    Next

                    query = "select a.LibelleCaract,a.RefSpecCaractPro, b.ValeurOfferte, b.MentionValeur, b.Commentaire from t_spectechcaractpropose a, t_soumiscaractfournitsupl b where a.RefSpecFournit='" & rw(0).ToString & "' And a.RefSpecCaractPro=b.RefSpecCaract and b.RefSoumis='" & TxtRefSoumis.Text & "'"
                    Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw3 As DataRow In dt3.Rows
                        Dim drS = dtExam.NewRow()
                        drS("Code") = rw3("RefSpecCaractPro").ToString
                        drS("Spécifications techniques") = MettreApost(rw3("LibelleCaract").ToString)
                        drS("Valeurs demandées") = "-"
                        drS("Valeurs offertes") = MettreApost(rw3("ValeurOfferte").ToString)
                        drS("Conformité / Commentaire") = IIf(rw3("MentionValeur").ToString <> "", rw3("MentionValeur").ToString, "").ToString & IIf(rw3("Commentaire").ToString <> "", " :    " & MettreApost(rw3("Commentaire").ToString), "")
                        drS("NC") = IIf(rw3("MentionValeur").ToString = "Non Conforme", "x", "").ToString
                        drS("SpecDemandé") = "non"
                        dtExam.Rows.Add(drS)
                    Next

                Next
                GridTravail.DataSource = dtExam

                ViewTravail.Columns(0).Visible = False
                ViewTravail.OptionsView.ColumnAutoWidth = True
                'ViewTravail.Columns(1).Width = 300
                'ViewTravail.Columns(2).Width = 200
                'ViewTravail.Columns(3).Width = 200
                'ViewTravail.Columns(4).Width = 350
                ViewTravail.Columns(5).Visible = False
                ViewTravail.Columns(6).Visible = False

                'ViewTravail.Columns(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
                'ViewTravail.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                ColorRowGrid(ViewTravail, "[Code]='x'", Color.LightGray, "Tahoma", 8, FontStyle.Bold, Color.Black)
                ColorRowGridAnal(ViewTravail, "[Valeurs offertes]='...'", Color.Yellow, "Tahoma", 8, FontStyle.Regular, Color.Black)
                ColorRowGridAnal(ViewTravail, "[NC]='x'", Color.White, "Tahoma", 8, FontStyle.Bold, Color.Red)

            Else            'Travaux **************

                Dim TampVerdict As Boolean = True
                Dim ToutAnalyser As Boolean = True
                Dim VerdictVerouille As Boolean = False

                dtExam.Columns.Clear()

                dtExam.Columns.Add("Code", Type.GetType("System.String"))
                dtExam.Columns.Add("CodeX", Type.GetType("System.String"))
                dtExam.Columns.Add("Critère de conformité", Type.GetType("System.String"))
                dtExam.Columns.Add("Conforme", Type.GetType("System.String"))
                dtExam.Columns.Add("Commentaire", Type.GetType("System.String"))

                'Dim CptLg As Decimal = -1
                'Dim PosLg As Decimal = -1
                dtExam.Rows.Clear()
                query = "select LibelleConformTech,RefConformTech from T_DAO_ConformTech where NumeroDAO='" & CmbNumDoss.Text & "' and CodeLot in ('" & CmbNumLot.Text & "','x') and RefConformMere='0'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    Dim cptr1 As Decimal = 0
                    Dim drS = dtExam.NewRow()

                    drS(0) = "x"
                    drS(1) = rw(1).ToString
                    drS(2) = MettreApost(rw(0).ToString)
                    drS(3) = ""
                    drS(4) = ""

                    dtExam.Rows.Add(drS)

                    query = "select LibelleConformTech,Eliminatoire,RefConformTech from T_DAO_ConformTech where NumeroDAO='" & CmbNumDoss.Text & "' and RefConformMere='" & rw(1).ToString & "'"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt1.Rows
                        cptr1 += 1
                        Dim drS2 = dtExam.NewRow()

                        drS2(0) = rw1(2).ToString
                        drS2(1) = IIf(CDec(cptr1 / 2) = CDec(cptr1 \ 2), "x", "").ToString
                        drS2(2) = "     " & MettreApost(rw1(0).ToString)
                        drS2(3) = InfosConform(TxtRefSoumis.Text, rw1(2).ToString)(0)
                        drS2(4) = InfosConform(TxtRefSoumis.Text, rw1(2).ToString)(1)

                        dtExam.Rows.Add(drS2)

                        If (InfosConform(TxtRefSoumis.Text, rw1(2).ToString)(0) <> "Conforme") Then
                            TampVerdict = False
                            If (rw1(1).ToString = "OUI") Then
                                VerdictVerouille = True
                            End If
                        End If

                        If (InfosConform(TxtRefSoumis.Text, rw1(2).ToString)(0).Replace(" ", "").Replace("-", "") = "") Then
                            ToutAnalyser = False
                        End If
                    Next
                Next

                GridTravail.DataSource = dtExam

                ViewTravail.Columns(0).Visible = False
                ViewTravail.Columns(1).Visible = False
                ViewTravail.Columns(2).Width = GridTravail.Width - 318
                ViewTravail.Columns(3).Width = 100
                ViewTravail.Columns(4).Width = 200

                'ViewTravail.Columns(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
                'ViewTravail.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
                'ViewTravail.Columns(2).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                ViewTravail.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

                ColorRowGrid(ViewTravail, "[CodeX]='x'", Color.LightGray, "Tahoma", 8, FontStyle.Regular, Color.Black)
                ColorRowGridAnal(ViewTravail, "[Code]='x'", Color.Navy, "Tahoma", 9, FontStyle.Bold, Color.White)
                ColorRowGridAnal(ViewTravail, "[Conforme]='Non Conforme'", Color.White, "Tahoma", 8, FontStyle.Regular, Color.Red, False)


                If (ToutAnalyser = True) Then

                    If (TampVerdict = True) Then
                        ChkConforme.Checked = True

                        ChkConforme.Properties.ReadOnly = True
                        ChkNonConforme.Properties.ReadOnly = True
                    Else
                        ChkNonConforme.Checked = True

                        If (VerdictVerouille = True) Then
                            ChkConforme.Properties.ReadOnly = True
                            ChkNonConforme.Properties.ReadOnly = True
                        Else
                            ChkConforme.Properties.ReadOnly = False
                            ChkNonConforme.Properties.ReadOnly = False
                        End If
                    End If

                End If

            End If

        End If

        If (ViewTravail.RowCount > 1) Then
            PanelVerdict.Enabled = True
        Else
            PanelVerdict.Enabled = False
        End If
    End Sub
    Private Sub VerifCaract(ByVal Caract As String, ByVal Soumis As String)
        Dim ligneExist As Boolean = False
        query = "select * from T_SoumisCaractFournit where RefSpecCaract='" & Caract & "' and RefSoumis='" & Soumis & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            ligneExist = True
        Next
        If (ligneExist = False) Then

            Dim DatSet = New DataSet
            query = "select * from T_SoumisCaractFournit"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_SoumisCaractFournit")
            Dim DatTable = DatSet.Tables("T_SoumisCaractFournit")
            Dim DatRow = DatSet.Tables("T_SoumisCaractFournit").NewRow()

            DatRow("RefSpecCaract") = Caract
            DatRow("RefSoumis") = Soumis

            DatSet.Tables("T_SoumisCaractFournit").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_SoumisCaractFournit")
            DatSet.Clear()
            BDQUIT(sqlconn)
        End If
    End Sub

    Private Sub VerifCaractPropose(ByVal Caract As String, ByVal Soumis As String)
        Dim ligneExist As Boolean = False
        query = "select * from t_soumiscaractfournitsupl where RefSpecCaract='" & Caract & "' and RefSoumis='" & Soumis & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            ligneExist = True
        Next
        If (ligneExist = False) Then

            Dim DatSet = New DataSet
            query = "select * from t_soumiscaractfournitsupl"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "t_soumiscaractfournitsupl")
            Dim DatTable = DatSet.Tables("t_soumiscaractfournitsupl")
            Dim DatRow = DatSet.Tables("t_soumiscaractfournitsupl").NewRow()

            DatRow("RefSpecCaract") = Caract
            DatRow("RefSoumis") = Soumis

            DatSet.Tables("t_soumiscaractfournitsupl").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "t_soumiscaractfournitsupl")
            DatSet.Clear()
            BDQUIT(sqlconn)
        End If
    End Sub

    Private Sub ChargerSoumis(ByVal ActuEtape As String)

        If ActuEtape = "" Then
            Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDoss.Text)
            Dim nbsouslot As Integer = Val(Resultat(0))
            If nbsouslot > 0 Then
                query = "select F.NomFournis from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and S.CodeLot='" & CmbNumLot.Text & "' and F.CodeProjet='" & ProjetEnCours & "' and F.DateDepotDAO<>'' AND S.CodeSousLot='" & cmbSousLot.Text & "'"
            Else
                query = "select F.NomFournis from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' and S.CodeLot='" & CmbNumLot.Text & "' and F.CodeProjet='" & ProjetEnCours & "' and F.DateDepotDAO<>''"
            End If
            CmbSoumis.Properties.Items.Clear()
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                CmbSoumis.Properties.Items.Add(MettreApost(rw("NomFournis").ToString))
            Next

            For k As Integer = 0 To ViewTraité.RowCount - 1
                If (dtTraite.Rows(k).Item(0).ToString <> "x") Then
                    If (CmbSoumis.Properties.Items.Contains(dtTraite.Rows(k).Item(1).ToString.Replace("  - ", ""))) Then
                        Dim OkLot As Boolean = False
                        Dim posLot As Decimal = k

                        While (OkLot = False And posLot > 0)
                            posLot = posLot - 1
                            If (dtTraite.Rows(posLot).Item(0).ToString = "x") Then
                                If (Mid(dtTraite.Rows(posLot).Item(1).ToString, 7) = CmbNumLot.Text) Then
                                    OkLot = True
                                Else
                                    Exit While
                                End If
                            End If
                        End While

                        If (OkLot = True) Then CmbSoumis.Properties.Items.Remove(dtTraite.Rows(k).Item(1).ToString.Replace("  - ", ""))

                    End If
                End If
            Next

        ElseIf (ActuEtape = "Analyse") Then
            'ChargerGridExam(EtapeActuelle)

        ElseIf (ActuEtape = "ExamPrelim") Then

        ElseIf (ActuEtape = "ExamDetail") Then

        ElseIf (ActuEtape = "ExamPost") Then

        End If
    End Sub

    Private Sub ChkConforme_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkConforme.CheckedChanged, ChkNonConforme.CheckedChanged
        If (ChkConforme.Checked = True Or ChkNonConforme.Checked = True) Then
            BtEnrgVerdict.Enabled = True
        Else
            BtEnrgVerdict.Enabled = False
        End If
    End Sub

    'Private Sub GridTravail_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridTravail.MouseUp

    '    If (ViewTravail.RowCount > 0) Then

    '        DrX = ViewTravail.GetDataRow(ViewTravail.FocusedRowHandle)

    '        If (EtapeActuelle = "") Then
    '            If (TxtTypeMarche.Text = "Fournitures") Then
    '                If (DrX(0).ToString <> "x") Then
    '                    CodeActuel = DrX(0).ToString
    '                    SpecDemande = DrX(6).ToString
    '                    ValeurActuelle = DrX(3).ToString
    '                    ContextMenuStrip1.Items(0).Enabled = True
    '                    ContextMenuStrip1.Items(1).Enabled = True
    '                    ContextMenuStrip1.Items(3).Enabled = False
    '                    ContextMenuStrip1.Items(5).Enabled = False
    '                Else
    '                    ValeurActuelle = ""
    '                    ContextMenuStrip1.Items(0).Enabled = False
    '                    ContextMenuStrip1.Items(1).Enabled = False
    '                    If (Mid(DrX(4).ToString, 1, 4).ToLower = "prix") Then
    '                        ContextMenuStrip1.Items(3).Enabled = False
    '                    Else
    '                        ContextMenuStrip1.Items(3).Enabled = True
    '                    End If
    '                    ContextMenuStrip1.Items(5).Enabled = False
    '                End If

    '            Else
    '                ContextMenuStrip1.Items(0).Enabled = False
    '                ContextMenuStrip1.Items(1).Enabled = False
    '                ContextMenuStrip1.Items(3).Enabled = False
    '                ContextMenuStrip1.Items(5).Enabled = False

    '                If (DrX(0).ToString <> "x") Then
    '                    CodeActuel = DrX(0).ToString
    '                    ValeurActuelle = DrX(3).ToString
    '                    If (ValeurActuelle = "Conforme") Then
    '                        ContextMenuStrip1.Items(1).Enabled = True

    '                    ElseIf (ValeurActuelle = "Non Conforme") Then
    '                        ContextMenuStrip1.Items(0).Enabled = True

    '                    Else
    '                        ContextMenuStrip1.Items(1).Enabled = True
    '                        ContextMenuStrip1.Items(0).Enabled = True
    '                    End If
    '                    ContextMenuStrip1.Items(5).Enabled = True
    '                    'Else
    '                    '    ContextMenuStrip1.Items(0).Enabled = False
    '                    '    ContextMenuStrip1.Items(1).Enabled = False
    '                    '    ContextMenuStrip1.Items(3).Enabled = False
    '                    '    ContextMenuStrip1.Items(5).Enabled = False

    '                End If

    '            End If
    '            ContextMenuStrip1.Items(7).Enabled = False
    '            ContextMenuStrip1.Items(9).Enabled = False

    '        End If
    '    End If

    'End Sub
#End Region

#Region "Code Non Utiliser"
    Private Sub CmbSoumis_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSoumis.SelectedValueChanged
        query = "select F.CodeFournis,F.PaysFournis,S.RefSoumis from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and S.CodeLot='" & CmbNumLot.Text & "' and F.NomFournis='" & EnleverApost(CmbSoumis.Text) & "' and F.NumeroDAO='" & CmbNumDoss.Text & "' and F.CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            TxtCodeFournis.Text = rw(0).ToString
            TxtRefSoumis.Text = rw(2).ToString
            TxtAdresseSoumis.Text = MettreApost(rw(1).ToString)
        Next
        ChargerGridExam(EtapeActuelle)
    End Sub

    Private Sub Classement2()

        Dim lesRef(500) As String
        Dim lesPrix(500) As String
        Dim Tamp As String = ""
        Dim nbSoum As Decimal = 0
        Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDoss.Text)
        Dim nbsouslot As Integer = Val(Resultat(0))
        Dim Verif As String = ""
        If nbsouslot > 0 Then
            query = "SELECT Attribution FROM t_dao WHERE NumeroDAO='" & CmbNumDoss.Text & "' AND CodeProjet='" & ProjetEnCours & "'"
            Verif = ExecuteScallar(query)
            If Verif = "Lot" Then
                query = "select F.CodeFournis,F.NomFournis,S.RefSoumis,S.Monnaie,SUM(S.MontantPropose),SUM(S.MontantAvecMonnaie),SUM(S.ErreurCalcul),SUM(S.SomProvision),SUM(S.PrctRabais),SUM(S.MontantRabais),SUM(S.AjoutOmission),SUM(S.Ajustements),SUM(S.VariationMineure),SUM(S.PrixCorrigeOffre),S.SigneErreur from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & CmbNumDoss.Text & "' and F.CodeProjet='" & ProjetEnCours & "' and S.CodeLot='" & CmbNumLot.Text & "' and AcceptationExamDetaille='OUI' GROUP by F.Nomfournis HAVING COUNT(F.Nomfournis) > 1"
                Dim dt1 = ExcecuteSelectQuery(query)
                For Each rw In dt1.Rows
                    If modif = False Then
                        query = "INSERT INTO t_soumissionfournisseurexamdetail(CodeFournis,RefSoumis,NumeroDAO,CodeLot,Monnaie,MontantPropose,MontantAvecMonnaie,ErreurCalcul,SomProvision,PrctRabais,MontantRabais,AjoutOmission,Ajustements,VariationMineure,PrixCorrigeOffre,DateModif,Operateur)"
                        query &= " VALUES('" & rw("CodeFournis").ToString & "','" & rw("RefSoumis").ToString & "','" & CmbNumDoss.Text & "','" & CmbNumLot.Text & "','" & rw("Monnaie").ToString & "','" & CDbl(rw(4).ToString) & "','" & CDbl(rw(5).ToString) & "','" & CDbl(rw(6).ToString) & "','" & CDbl(rw(7).ToString) & "','" & CDbl(rw(8).ToString) & "','" & CDbl(rw(9).ToString) & "','" & CDbl(rw(10).ToString) & "','" & CDbl(rw(11).ToString) & "','" & CDbl(rw(12).ToString) & "','" & CDbl(rw(13).ToString) & "','" & dateconvert(Now.ToShortDateString) & " " & Now.ToShortTimeString & "','" & CodeOperateurEnCours & "')"
                        ExecuteNonQuery(query)
                    End If
                Next
            Else
                query = "select S.RefSoumis,S.PrixCorrigeOffre from T_SoumissionFournisseur as S,T_Fournisseur as F where S.CodeFournis=F.CodeFournis and F.NumeroDAO='" & CmbNumDoss.Text & "' and S.CodeLot='" & CmbNumLot.Text & "' and S.CodeSousLot='" & cmbSousLot.Text & "' and S.AcceptationExamDetaille='OUI' and S.PrixCorrigeOffre<>'0'"
            End If
        Else
            query = "select S.RefSoumis,S.PrixCorrigeOffre from T_SoumissionFournisseur as S,T_Fournisseur as F where S.CodeFournis=F.CodeFournis and F.NumeroDAO='" & CmbNumDoss.Text & "' and S.CodeLot='" & CmbNumLot.Text & "' and S.AcceptationExamDetaille='OUI' and S.PrixCorrigeOffre<>'0'"
        End If
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            lesRef(nbSoum) = rw(0).ToString
            lesPrix(nbSoum) = rw(1).ToString.Replace(" ", "")
            nbSoum += 1
        Next

        For i As Integer = 0 To nbSoum - 2
            For j As Integer = i + 1 To nbSoum - 1
                If (IIf(lesPrix(j) = "", 0, CDec(lesPrix(j))) < CDec(lesPrix(i))) Then
                    Tamp = lesPrix(i)
                    lesPrix(i) = lesPrix(j)
                    lesPrix(j) = Tamp

                    Tamp = lesRef(i)
                    lesRef(i) = lesRef(j)
                    lesRef(j) = Tamp
                End If
            Next
        Next

        For k As Integer = 0 To nbSoum - 1

            Dim rang As Decimal = 0
            rang = k + 1
            If nbsouslot > 0 Then
                If Verif = "Lot" Then
                    query = "update t_soumissionfournisseurexamdetail set RangExamDetaille='" & rang.ToString & "' where CodeFournis='" & lesRef(k) & "'"
                    ExecuteNonQuery(query)
                Else
                    query = "update T_SoumissionFournisseur set RangExamDetaille='" & rang.ToString & "' where RefSoumis='" & lesRef(k) & "'"
                    ExecuteNonQuery(query)
                End If
            Else
                query = "update T_SoumissionFournisseur set RangExamDetaille='" & rang.ToString & "' where RefSoumis='" & lesRef(k) & "'"
                ExecuteNonQuery(query)
            End If
        Next
        SuccesMsg("Classement terminé avec succès!")
    End Sub

    Private Sub OffresTraitees1()

        If (CmbNumDoss.Text <> "") Then
            dtTraite.Columns.Clear()

            dtTraite.Columns.Add("Code", Type.GetType("System.String"))
            dtTraite.Columns.Add("Fournisseur", Type.GetType("System.String"))
            dtTraite.Columns.Add("Accepté pour Examen détaillé", Type.GetType("System.String"))
            'dtTraite.Columns.Add("Conformité administrative", Type.GetType("System.String"))
            dtTraite.Columns.Add("Montant lu", Type.GetType("System.String"))
            dtTraite.Columns.Add("Montant corrigé", Type.GetType("System.String"))
            dtTraite.Columns.Add("Classement", Type.GetType("System.String"))
            dtTraite.Columns.Add("Post qualifié", Type.GetType("System.String"))

            dtTraite.Rows.Clear()
            query = "select RefLot,CodeLot from T_LotDAO where NumeroDAO='" & CmbNumDoss.Text & "' order by CodeLot"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                Dim Resultat As Object() = GetSousLot(rw(1).ToString, CmbNumDoss.Text)
                Dim nbsouslot As Integer = Val(Resultat(0))
                Dim DrT = dtTraite.NewRow()
                If nbsouslot > 0 Then
                    Dim dt As DataTable = CType(Resultat(1), DataTable)
                    For Each rw2 As DataRow In dt.Rows
                        cmbSousLot.Properties.Items.Add(rw2("CodeSousLot").ToString)
                    Next
                Else
                    DrT(0) = "x"
                    DrT(1) = "Lot N°" & rw(1).ToString
                End If

                dtTraite.Rows.Add(DrT)

                query = "select F.NomFournis,S.RefSoumis,S.AcceptationExamDetaille,S.ConformiteTechnique,S.MontantPropose,S.PrixCorrigeOffre,S.RangExamDetaille,S.ExamPQValide from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and S.RefLot='" & rw(0).ToString & "' order by F.NomFournis"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows
                    If (rw1(3).ToString <> "") Then

                        Dim DrT2 = dtTraite.NewRow()
                        DrT2(0) = rw1(1).ToString
                        DrT2(1) = "  - " & MettreApost(rw1(0).ToString)
                        DrT2(2) = IIf(rw1(2).ToString <> "", IIf(rw1(2).ToString = "OUI", "OUI", "NON").ToString, "-").ToString
                        'DrT2(3) = IIf(rw1(3).ToString <> "", IIf(rw1(3).ToString = "OUI", "Conforme", "Non Conforme").ToString, "-").ToString
                        If (rw1(2).ToString = "OUI") Then
                            DrT2(3) = AfficherMonnaie(rw1(4).ToString)
                            DrT2(4) = AfficherMonnaie(rw1(5).ToString)
                            DrT2(5) = IIf(rw1(6).ToString <> "0", rw1(6).ToString & IIf(rw1(6).ToString = "1", "er", "ème").ToString, "-").ToString
                            DrT2(6) = rw1(7).ToString
                        Else
                            DrT2(3) = "-"
                            DrT2(4) = "-"
                            DrT2(5) = "-"
                            DrT2(6) = "-"
                        End If

                        dtTraite.Rows.Add(DrT2)
                    End If
                Next
            Next

            GridTraité.DataSource = dtTraite

            ViewTraité.Columns(0).Visible = False
            ViewTraité.Columns(1).Width = 250
            ViewTraité.Columns(2).Width = 150
            ViewTraité.Columns(3).Width = 150
            ViewTraité.Columns(4).Width = 150
            ViewTraité.Columns(5).Width = 150
            ViewTraité.Columns(6).Width = 150

            ViewTraité.Columns(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewTraité.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

            ViewTraité.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center                  'Drawing.StringAlignment.Center
            ViewTraité.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewTraité.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewTraité.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewTraité.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            ColorRowGrid(ViewTraité, "[Code]='x'", Color.LightGray, "Tahoma", 8, FontStyle.Bold, Color.Black)
            ColorRowGridAnal(ViewTraité, "[Post qualifié]='NON'", Color.White, "Tahoma", 8, FontStyle.Strikeout, Color.Black, False)
            ColorRowGridAnal(ViewTraité, "[Accepté pour Examen détaillé]='NON'", Color.White, "Tahoma", 8, FontStyle.Strikeout, Color.Black, False)
        End If

    End Sub
    Private Sub ClassementPostQualif_Old()

        For y As Decimal = 1 To CInt(TxtNbLot.Text.Replace(" ", ""))

            Dim lesRef(500) As String
            Dim lesPrix(500) As String
            Dim Tamp As String = ""
            Dim nbSoum As Decimal = 0

            query = "select S.RefSoumis,S.PrixCorrigeOffre from T_SoumissionFournisseur as S,T_Fournisseur as F where S.CodeFournis=F.CodeFournis and F.NumeroDAO='" & CmbNumDoss.Text & "' and S.CodeLot='" & y.ToString & "' and S.RangExamDetaille<>'0' and S.ExamPQValide='OUI'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                lesRef(nbSoum) = rw(0).ToString
                lesPrix(nbSoum) = rw(1).ToString.Replace(" ", "")
                nbSoum += 1
            Next

            For i As Integer = 0 To nbSoum - 2
                For j As Integer = i + 1 To nbSoum - 1
                    If (CDec(lesPrix(j)) < CDec(lesPrix(i))) Then
                        Tamp = lesPrix(i)
                        lesPrix(i) = lesPrix(j)
                        lesPrix(j) = Tamp

                        Tamp = lesRef(i)
                        lesRef(i) = lesRef(j)
                        lesRef(j) = Tamp
                    End If
                Next
            Next

            Dim Choix As String = ""
            Dim RaisonChoix As String = ""
            For k As Integer = 0 To nbSoum - 1

                Dim rang As Decimal = 0
                rang = k + 1
                Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDoss.Text)
                Dim nbsouslot As Integer = Val(Resultat(0))
                If nbsouslot > 0 Then
                    query = "update t_soumissionfournisseurexamdetail set RangPostQualif='" & rang.ToString & "', Selectionne='" & Choix & "',MotifSelect='" & RaisonChoix & "'  where RefSoumis='" & lesRef(k) & "'"
                    ExecuteNonQuery(query)
                Else
                    query = "update T_SoumissionFournisseur set RangPostQualif='" & rang.ToString & "', Selectionne='" & Choix & "',MotifSelect='" & RaisonChoix & "'  where RefSoumis='" & lesRef(k) & "'"
                    ExecuteNonQuery(query)
                End If
                If (k = 0) Then
                    Choix = "OUI"
                    RaisonChoix = "Classement évaluateurs"
                Else
                    Choix = "NON"
                    RaisonChoix = "Classement évaluateurs"
                End If
            Next
        Next
        SuccesMsg("Classement post qualification terminé avec succès!")
    End Sub

    'Private Sub CmbLotMarche_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbLotMarche.SelectedValueChanged
    '    InitFormMarche()

    '    If (CmbLotMarche.Text <> "") Then
    '        query = "select LibelleLot,RefLot from T_LotDAO where NumeroDAO='" & CmbNumDoss.Text & "' and CodeLot='" & CmbLotMarche.Text & "'"
    '        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
    '        If dt0.Rows.Count > 0 Then
    '            Dim rw As DataRow = dt0.Rows(0)
    '            TxtLotMarche.Text = IIf(rw(0).ToString <> "Lot N°" & CmbLotMarche.Text, MettreApost(rw(0).ToString), TxtLibelleDoss.Text & " (" & rw(0).ToString & ")")
    '            TxtRefLotMarche.Text = rw(1).ToString

    '            query = "select BanqueCaution,DelaiLivraison,PrixCorrigeOffre,CodeFournis,RefSoumis,NumCompteBanque from T_SoumissionFournisseur where RefLot='" & rw(1).ToString & "' and Attribue='OUI'"
    '            dt0 = ExcecuteSelectQuery(query)
    '            If dt0.Rows.Count > 0 Then
    '                Dim rw1 As DataRow = dt0.Rows(0)
    '                TxtRefSoumisMarche.Text = rw1(4).ToString
    '                Dim NomBank As String = ""
    '                query = "select NomCompletBanque from T_Banque where CodeProjet='" & ProjetEnCours & "' and CodeBanque='" & rw1(0).ToString & "'"
    '                dt0 = ExcecuteSelectQuery(query)
    '                If dt0.Rows.Count > 0 Then
    '                    NomBank = MettreApost(dt0.Rows(0).Item(0).ToString)
    '                End If

    '                TxtNomBanqueFournis.Text = MettreApost(rw1(0).ToString) & " (" & NomBank & ")"
    '                TxtNumCompteFournis.Text = rw1(5).ToString
    '                TxtExecutionMarche.Text = rw1(1).ToString
    '                TxtMontantMarche.Text = AfficherMonnaie(rw1(2).ToString.Replace(" ", ""))
    '                TxtCodeFournisMarche.Text = rw1(3).ToString

    '                TxtTotalReparti.Text = AfficherMonnaie(rw1(2).ToString.Replace(" ", ""))

    '                ' infos fournis *****************
    '                query = "select NomFournis,PaysFournis,AdresseCompleteFournis,TelFournis,FaxFournis,MailFournis,CompteContribuableFournis,RegistreCommerceFournis,NomRep,AdresseRep,TelRep from T_Fournisseur where CodeFournis='" & rw1(3).ToString & "' and CodeProjet='" & ProjetEnCours & "' and NumeroDAO='" & CmbNumDoss.Text & "'"
    '                dt0 = ExcecuteSelectQuery(query)
    '                If dt0.Rows.Count > 0 Then
    '                    Dim rw2 As DataRow = dt0.Rows(0)
    '                    TxtFournisMarche.Text = MettreApost(rw2(0).ToString & " (" & rw2(1).ToString & ")")
    '                    TxtAdresseFournisMarche.Text = MettreApost(rw2(2).ToString)
    '                    TxtContactFournisMarche.Text = IIf(rw2(3).ToString <> "", "Tel : " & rw2(3).ToString & "  ", "") & IIf(rw2(4).ToString <> "", "Fax : " & rw2(4).ToString & "  ", "") & IIf(rw2(5).ToString <> "", "E-mail : " & rw2(5).ToString, "")
    '                    TxtContribuable.Text = rw2(6).ToString
    '                    TxtRegCommerce.Text = rw2(7).ToString
    '                    TxtNomRepLegal.Text = MettreApost(rw2(8).ToString)
    '                    TxtBpRepLegal.Text = MettreApost(rw2(9).ToString)
    '                    TxtContactRepLegal.Text = rw2(10).ToString
    '                End If
    '            End If
    '        End If
    '        query = "select B.InitialeBailleur,B.NomBailleur,B.CodeBailleur from T_Bailleur as B,T_Marche as M where M.NumeroDAO='" & CmbNumDoss.Text & "' and M.CodeProjet='" & ProjetEnCours & "' and B.InitialeBailleur=M.InitialeBailleur and B.CodeProjet=M.CodeProjet"
    '        dt0 = ExcecuteSelectQuery(query)
    '        If dt0.Rows.Count > 0 Then
    '            Dim rw1 As DataRow = dt0.Rows(0)
    '            TxtBailleurMarche.Text = MettreApost(rw1(0).ToString & " (" & rw1(1).ToString & ")")
    '            If (rw1(0).ToString = "ETAT") Then
    '                RdTresorAnPrec.Enabled = True
    '                RdTresorAnCour.Enabled = True
    '                RdTresorAnSuiv.Enabled = True
    '            Else
    '                RdTresorAnPrec.Enabled = False
    '                RdTresorAnCour.Enabled = False
    '                RdTresorAnSuiv.Enabled = False
    '            End If

    '            query = "select C.TypeConvention,C.CodeConvention from T_Convention as C, T_Marche as M where M.NumeroDAO='" & CmbNumDoss.Text & "' and M.CodeConvention=C.CodeConvention and M.CodeProjet='" & ProjetEnCours & "' and C.CodeBailleur='" & rw1(2).ToString & "'"
    '            dt0 = ExcecuteSelectQuery(query)
    '            If dt0.Rows.Count > 0 Then
    '                rw1 = dt0.Rows(0)
    '                TxtConventionMarche.Text = rw1(0).ToString & " " & rw1(1).ToString
    '                If (rw1(0).ToString.ToLower = "don") Then
    '                    RdDonAnPrec.Enabled = True
    '                    RdDonAnCour.Enabled = True
    '                    RdDonAnSuiv.Enabled = True

    '                    RdEmpruntAnPrec.Enabled = False
    '                    RdEmpruntAnCour.Enabled = False
    '                    RdEmpruntAnSuiv.Enabled = False
    '                Else
    '                    RdEmpruntAnPrec.Enabled = True
    '                    RdEmpruntAnCour.Enabled = True
    '                    RdEmpruntAnSuiv.Enabled = True

    '                    RdDonAnPrec.Enabled = False
    '                    RdDonAnCour.Enabled = False
    '                    RdDonAnSuiv.Enabled = False
    '                End If
    '            End If
    '        End If

    '        query = "select NumeroMarche,PrctCautionDef,PrctAvance,ImputBudgetaire from T_MarcheSigne where CodeFournis='" & TxtCodeFournisMarche.Text & "' and RefLot='" & TxtRefLotMarche.Text & "' and RefSoumis='" & TxtRefSoumisMarche.Text & "'"
    '        dt0 = ExcecuteSelectQuery(query)
    '        If dt0.Rows.Count > 0 Then
    '            Dim rw1 As DataRow = dt0.Rows(0)
    '            TxtPrctCautionDef.Text = IIf(IsNumeric(rw1(1).ToString) = True, rw1(1).ToString, "0").ToString
    '            TxtPrctAvance.Text = IIf(IsNumeric(rw1(2).ToString) = True, rw1(2).ToString, "0").ToString
    '            TxtImputBudgetaire.Text = rw1(3).ToString
    '            TxtNumeroMarche.Text = rw1(0).ToString

    '            Dim n As Decimal = 0
    '            query = "select AnneeRepart,MontantRepart,SujetImputation from T_Marche_Repartition where NumeroMarche='" & rw1(0).ToString & "' order by AnneeRepart"
    '            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
    '            For Each rw As DataRow In dt1.Rows
    '                n += 1
    '                If (n = 1) Then
    '                    LblAnPrec.Text = "(" & rw(0).ToString & ")"
    '                    TxtMontAnPrec.Text = rw(1).ToString
    '                    Select Case rw(2).ToString
    '                        Case "Trésor"
    '                            RdTresorAnPrec.Checked = True
    '                        Case "Don"
    '                            RdDonAnPrec.Checked = True
    '                        Case "Emprunt"
    '                            RdEmpruntAnPrec.Checked = True
    '                    End Select

    '                ElseIf (n = 2) Then

    '                    LblAnCour.Text = "(" & rw(0).ToString & ")"
    '                    TxtMontAnCour.Text = rw(1).ToString
    '                    Select Case rw(2).ToString
    '                        Case "Trésor"
    '                            RdTresorAnCour.Checked = True
    '                        Case "Don"
    '                            RdDonAnCour.Checked = True
    '                        Case "Emprunt"
    '                            RdEmpruntAnCour.Checked = True
    '                    End Select

    '                ElseIf (n = 3) Then

    '                    LblAnSuiv.Text = "(" & rw(0).ToString & ")"
    '                    TxtMontAnSuiv.Text = rw(1).ToString
    '                    Select Case rw(2).ToString
    '                        Case "Trésor"
    '                            RdTresorAnSuiv.Checked = True
    '                        Case "Don"
    '                            RdDonAnSuiv.Checked = True
    '                        Case "Emprunt"
    '                            RdEmpruntAnSuiv.Checked = True
    '                    End Select

    '                Else

    '                    MsgBox("Y a problème!", MsgBoxStyle.Exclamation)

    '                End If
    '            Next

    '        End If

    '        Dim Annee As Decimal = Now.Year
    '        LblAnPrec.Text = "(" & (Annee - 1).ToString & ")"
    '        LblAnCour.Text = "(" & Annee.ToString & ")"
    '        LblAnSuiv.Text = "(" & (Annee + 1).ToString & ")"

    '    End If
    'End Sub

#End Region

#End Region

    Private Sub BtOuvFerm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtOuvFerm.Click
        If (BtOuvFerm.Text = "<<") Then
            BtOuvFerm.Text = ">>"
            GbTraites.Width = 188 + GbTraitement.Width - 20
        Else
            BtOuvFerm.Text = "<<"
            GbTraites.Width = 188
        End If
    End Sub

    Private Sub BilanOffres_Click(sender As Object, e As EventArgs) Handles BilansOffres.Click
        Try
            DebutChargement()

            If BilansOffres.Text = "[Bilan des offres]" Then
                BilansOffres.Text = "[ Resultat ]"
                BilansOffres.ToolTip = "Resultat examen post qualification"
                TxtTypeExamen.Text = "BILAN DU JUGEMENT DES OFFRES"
                BilanExamOffres()
            ElseIf BilansOffres.Text = "[ Resultat ]" Then
                BilansOffres.Text = "[Bilan des offres]"
                BilansOffres.ToolTip = "Bilan des offres"
                TxtTypeExamen.Text = "EXAMEN POST QUALIFICATION"
                ChargerExamPostQualif(True)
            End If
            TxtTypeExamen.ForeColor = Color.Black
            FinChargement()
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

End Class