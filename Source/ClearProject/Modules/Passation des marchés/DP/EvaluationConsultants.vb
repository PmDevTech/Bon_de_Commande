Imports System.IO
Imports System.Math
Imports ClearProject.PassationMarche
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraEditors.Repository
Imports Microsoft.Office.Interop
Imports MySql.Data.MySqlClient

Public Class EvaluationConsultants

    Dim dtComm As DataTable = New DataTable()
    Dim dtMoy As DataTable = New DataTable()

    Dim ListeCodeDoss As New List(Of String)
    Dim CurrenRefMarche As Decimal = 0

    Dim DrX As DataRow
    Public MonnaieEvaluation As String = ""

    'varaible rapportd'évaluation technique
    Dim RapportModif As Boolean = False
    Dim CheminRapportEvalTech As String = ""
    Dim ExisteTexteGeneralites As Boolean = False
    Dim RaportEvalTechValider As String = ""
    Dim DateEnvoiRapport As String() = {"", ""}

    'varaible evaluation financiere
    Dim rwDossOffreFin As DataRow

    'Variable rapport combine
    Dim rwDossRapCombine As DataRow
    Dim ModifRapCombine As Boolean = False

    'Varieble negociation
    Dim DejaSaveNego As Boolean = False
    Dim DoubleClicks As Boolean = False
    Dim NomGridView As String = ""
    Dim LignModif As Decimal = 0

    'Variable contrat
    Dim RefSoumisRetenuContrat As String = ""
    Dim RefConsults As String = ""
    Dim DejaEnregistrer As Boolean = False
    Dim TypeConvention As String = ""
    Dim dtAnnexe As New DataTable()
    Dim IndexLignArticle As Integer = 0

    'Variable Impremer contrat
    Dim ModifsContrats As Boolean = False
    Dim CheminContrat As String = ""
    Dim TypeRenumerationContrat As String = ""

    'Table a mettre a jour Index 0= raport tech, 1=eval fin, 2=rapport combine, 3=negociation, 4=contrat ImpContrat
    Dim TablBoutonClik As Boolean() = {False, False, False, False, False, False}

    Private Sub EvaluationConsultants_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ChargerDossier()
    End Sub


#Region "Action de base"

    Private Sub ChargerDossier()
        query = "select NumeroDp from T_DP where EvalTechnique<>'' and Statut<>'Annulé' and CodeProjet='" & ProjetEnCours & "' ORDER BY DateEdition DESC"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        CmbNumDoss.Properties.Items.Clear()
        CmbNumDoss.Text = ""
        For Each rw As DataRow In dt.Rows
            CmbNumDoss.Properties.Items.Add(MettreApost(rw("NumeroDp").ToString))
        Next

        'Ajout des AMI de methode 3CV
        query = "select NumeroDAMI from t_ami where StatutDoss <>'Annuler' and MethodeSelection='3CV' and EvalTechnique IS NOT NULL order by NumeroDAMI ASC"
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt1.Rows
            CmbNumDoss.Properties.Items.Add(MettreApost(rw("NumeroDAMI").ToString))
            ListeCodeDoss.Add(MettreApost(rw("NumeroDAMI").ToString))
        Next
    End Sub

    Private Sub GetVisiblePanel(ByVal value As Boolean, Optional Affich As String = "")
        'Cacher eux tous et afficher le concerner
        PanelRapportEvaluationTech.Visible = Not value
        PanelOffreFinanciere.Visible = Not value
        PanelRapportCombinet.Visible = Not value
        PanelNegociation.Visible = Not value
        PanelAccueilEvalTech.Visible = Not value
        PanelImpressionContrat.Visible = Not value
        PanelEditionMarche.Visible = Not value

        If Affich = "Accueil" Then
            PanelAccueilEvalTech.Visible = value
        ElseIf Affich = "EvalTech" Then
            PanelRapportEvaluationTech.Visible = value
        ElseIf Affich = "OffreFinance" Then
            PanelOffreFinanciere.Visible = value
        ElseIf Affich = "Combine" Then
            PanelRapportCombinet.Visible = value
        ElseIf Affich = "Negociation" Then
            PanelNegociation.Visible = value
        ElseIf Affich = "Marche" Then
            PanelEditionMarche.Visible = value
        ElseIf Affich = "ImprimerContrat" Then
            PanelImpressionContrat.Visible = value
        End If
    End Sub

    Private Sub GetActiveBouton(ByVal value As Boolean)
        BtRapportEvalTech.Enabled = value
        BtEvalautionFinanciere.Enabled = value
        BtRapportCombinet.Enabled = value
        BtEditionContrat.Enabled = value
        BtNegociation.Enabled = value
        BtImprimerContrat.Enabled = value
    End Sub

    Private Sub NewInitialiserDonne()
        TxtLibelleDoss.Text = ""
        TxtDateOuvert.Text = ""
        TxtMethode.Text = ""

        'Initialiser Info rapport evaluation technique
        RapportModif = False
        CurrenRefMarche = 0
        CheminRapportEvalTech = ""
        ExisteTexteGeneralites = False
        RaportEvalTechValider = ""
        DateEnvoiRapport = {"", ""}
        TypeRenumerationContrat = ""

        'Evaluation financières
        'Negociation
        TablBoutonClik = {False, False, False, False, False, False}
    End Sub

    Private Sub EvaluationConsultants_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        FinChargement()
    End Sub

    Private Function TypeDossier(NumDoss As String) As String
        If ListeCodeDoss.Count > 0 Then
            For i = 0 To ListeCodeDoss.Count - 1
                If ListeCodeDoss.Item(i).ToString = NumDoss.ToString Then
                    Return "AMI"
                End If
            Next
        End If
        Return "DP"
    End Function

    Private Sub CmbNumDoss_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumDoss.SelectedValueChanged

        EtapeTechnique.ImageIndex = 1
        EtapeFinanciere.ImageIndex = 1

        EtapeTechnique.ForeColor = Color.Silver
        EtapeFinanciere.ForeColor = Color.Silver

        GetVisiblePanel(True, "Accueil")
        GetActiveBouton(False)
        NewInitialiserDonne()
        dtMoy.Rows.Clear()
        dtComm.Rows.Clear()

        If CmbNumDoss.SelectedIndex <> -1 Then

            DebutChargement(True, "Chargement des données en cours...")

            If TypeDossier(CmbNumDoss.Text) = "AMI" Then
                query = "select * from t_ami where NumeroDAMI='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)

                For Each rw As DataRow In dt.Rows
                    TxtLibelleDoss.Text = MettreApost(rw("LibelleMiss").ToString)
                    TxtDateOuvert.Text = CDate(rw("DateOuvertureEffective").ToString).ToShortDateString
                    TxtMethode.Text = rw("MethodeSelection").ToString
                    CurrenRefMarche = rw("RefMarche").ToString
                    TypeRenumerationContrat = rw("TypeRemune").ToString

                    EtapeTechnique.ImageIndex = 0
                    EtapeTechnique.ForeColor = Color.Black

                    'Activiver le bouton de la negociation
                    BtNegociation.Enabled = True
                    query = "select count(*) from t_dp_negociation where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
                    If Val(ExecuteScallar(query)) > 0 Then
                        'Active le bouton du contrat
                        BtEditionContrat.Enabled = True
                        If Val(ExecuteScallar("select count(*) From t_dp_contrat where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'")) > 0 Then
                            BtImprimerContrat.Enabled = True
                        End If
                    End If
                Next
            Else
                query = "select * from T_DP where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows

                    TxtLibelleDoss.Text = MettreApost(rw("LibelleMiss").ToString)
                    TxtDateOuvert.Text = CDate(rw("DateOuvertureEffective").ToString).ToShortDateString
                    TxtMethode.Text = rw("MethodeSelection").ToString
                    TypeRenumerationContrat = rw("TypeRemune").ToString

                    'Monnaie
                    MonnaieEvaluation = rw("MonnaieEval").ToString
                    EtapeTechnique.ImageIndex = 0
                    EtapeTechnique.ForeColor = Color.Black

                    'Activer le bouton rapport d'eval tech
                    BtRapportEvalTech.Enabled = True

                    'Verifier s'il y a des consultants retenu
                    Dim dtDoss As DataTable = ExcecuteSelectQuery("select * from T_SoumissionConsultant where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and EvalTechOk='OUI' ORDER BY RangConsult ASC")
                    If dtDoss.Rows.Count > 0 Then

                        'Cas evaluation financière
                        rwDossOffreFin = dtDoss.Rows(0)
                        If rw("RaportEvalTechBailleur").ToString = "Valider" Then
                            BtEvalautionFinanciere.Enabled = True

                            'Savoir si l'evaluation financières est terminer
                            If rwDossOffreFin("FinEvalFinanciere").ToString <> "" Then
                                EtapeFinanciere.ImageIndex = 0
                                EtapeFinanciere.ForeColor = Color.Black
                                'Active le bouton rapport combinet
                                BtRapportCombinet.Enabled = True

                                'Rechercher le rapport combinet d'un consultant valider pour activer le bouton
                                'de la negociation

                                Dim Cpte As Integer = 0
                                For Each rw1 In dtDoss.Rows
                                    If rw1("EtatRapportCombine").ToString = "Valider" Then
                                        'Cas de negociation
                                        BtNegociation.Enabled = True

                                        'Verifier si les negociation ont eux lieux
                                        query = "select count(*) from t_dp_negociation where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
                                        If Val(ExecuteScallar(query)) > 0 Then
                                            'Active le bouton du contrat
                                            BtEditionContrat.Enabled = True
                                            If Val(ExecuteScallar("select count(*) From t_dp_contrat where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'")) > 0 Then
                                                BtImprimerContrat.Enabled = True
                                            End If
                                        End If
                                        'Sortir de la boucle
                                        Exit For
                                    End If
                                Next
                            End If
                        End If
                    Else
                        EtapeFinanciere.ImageIndex = 2
                        EtapeFinanciere.ForeColor = Color.Black
                    End If

                    'Donnée rapport evaluation technique
                    CheminRapportEvalTech = rw("CheminRapportEvalTech").ToString
                    If rw("TexteGeneralites").ToString <> "" Then ExisteTexteGeneralites = True
                    RaportEvalTechValider = rw("RaportEvalTechBailleur").ToString
                    DateEnvoiRapport(0) = rw("DateSoumRapTechBail").ToString
                    DateEnvoiRapport(1) = rw("DateAviObjectionRapTech").ToString

                Next
            End If
            FinChargement()
            RemplirCojo()
            RemplirMoyenne()
            FinChargement()
            End If
    End Sub

    Private Sub RemplirCojo()
        If CmbNumDoss.SelectedIndex <> -1 Then
            dtComm.Columns.Clear()
            dtComm.Columns.Add("Commission", Type.GetType("System.String"))
            dtComm.Rows.Clear()
            query = "select NomMem, TitreMem, CodeMem from T_Commission where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "' ORDER BY NomMem ASC" 'and TypeComm='EVAC' and Evaluation<>''"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                Dim drS = dtComm.NewRow()
                ' DernierEval = rw("CodeMem").ToString
                drS("Commission") = MettreApost(rw("NomMem").ToString) & " (" & rw("TitreMem").ToString & ")"
                dtComm.Rows.Add(drS)
            Next
            GridCojo.DataSource = dtComm
        End If
    End Sub

    Private Sub RemplirMoyenne()
        dtMoy.Columns.Clear()
        dtMoy.Columns.Add("Code", Type.GetType("System.String"))
        dtMoy.Columns.Add("CodeX", Type.GetType("System.String"))
        dtMoy.Columns.Add("Consultant", Type.GetType("System.String"))
        dtMoy.Columns.Add("Score technique(moyenne)", Type.GetType("System.String"))
        dtMoy.Columns.Add("Rang", Type.GetType("System.String"))
        dtMoy.Columns.Add("Décision", Type.GetType("System.String"))
        dtMoy.Rows.Clear()

        If TypeDossier(CmbNumDoss.Text) = "AMI" Then 'Cas de la Methode 3CV
            query = "select S.RefSoumis,C.NomConsult,S.NoteConsult,S.ReferenceNote,S.RangConsult,S.EvalTechOk from T_Consultant as C,T_SoumissionConsultant as S where S.RefConsult=C.RefConsult and S.NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and S.NoteConsult IS NOT NULL and S.EvalTechOk='OUI' ORDER BY S.RangConsult ASC LIMIT 3"
        Else
            query = "select S.RefSoumis,C.NomConsult,S.NoteConsult,S.ReferenceNote,S.RangConsult,S.EvalTechOk from T_Consultant as C,T_SoumissionConsultant as S where S.RefConsult=C.RefConsult and S.NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and S.NoteConsult IS NOT NULL ORDER BY S.RangConsult ASC"
        End If
        dt = ExcecuteSelectQuery(query)
        Dim cpt2 As Integer = 0
        For Each rw As DataRow In dt.Rows
            Dim DrE = dtMoy.NewRow()
            cpt2 += 1

            DrE("Code") = rw("RefSoumis").ToString
            DrE("CodeX") = IIf(CDec(cpt2 / 2) = CDec(cpt2 \ 2), "x", "")
            DrE("Consultant") = MettreApost(rw("NomConsult").ToString)

            DrE("Score technique(moyenne)") = IIf(rw("NoteConsult").ToString.Replace(".", ",") <> "", rw("NoteConsult").ToString.Replace(".", ","), "0").ToString & " / " & IIf(rw("ReferenceNote").ToString <> "", rw("ReferenceNote").ToString, "0").ToString
            DrE("Rang") = IIf(rw("RangConsult").ToString <> "0", rw("RangConsult").ToString & IIf(rw("RangConsult").ToString = "1", "er", "ème").ToString, "-").ToString
            DrE("Décision") = IIf(rw("EvalTechOk").ToString <> "", IIf(rw("EvalTechOk").ToString = "OUI", "ACCEPTE", "REFUSE").ToString, "-").ToString

            dtMoy.Rows.Add(DrE)
        Next
        GridMoyenne.DataSource = dtMoy

        ViewMoyenne.Columns("Rang").Width = 50
        ViewMoyenne.Columns("Décision").Width = 50
        ViewMoyenne.Columns("Score technique(moyenne)").Width = 100

        ViewMoyenne.Columns("Code").Visible = False
        ViewMoyenne.Columns("CodeX").Visible = False
        ViewMoyenne.OptionsView.ColumnAutoWidth = True

        ViewMoyenne.Columns("Rang").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewMoyenne.Columns("Décision").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewMoyenne.Columns("Score technique(moyenne)").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

        ColorRowGrid(ViewMoyenne, "[CodeX]='x'", Color.LightGray, "Tahoma", 10, FontStyle.Regular, Color.Black)
        ColorRowGridAnal(ViewMoyenne, "[Décision]='REFUSE'", Color.White, "Tahoma", 10, FontStyle.Regular, Color.Red, False)

    End Sub

#End Region

#Region "Code non utilser"

    Private Sub AfficherGrid()

        'PanelEditionMarche.Visible = False
        'If (EtapeActuelle = "Technique") Then
        '    GridMoyenne.Visible = Not ActiverNote
        '    ' GridNote.Visible = ActiverNote
        '    GridSaisieOffreFinance.Visible = False
        '    GridBilanOffreFinancier.Visible = False

        'ElseIf (EtapeActuelle = "Finance") Then
        '    GridMoyenne.Visible = False
        '    'GridNote.Visible = False
        '    GridSaisieOffreFinance.Visible = True
        '    GridBilanOffreFinancier.Visible = False

        'ElseIf (EtapeActuelle = "Terminé") Then
        '    GridMoyenne.Visible = False
        '    ' GridNote.Visible = False
        '    GridSaisieOffreFinance.Visible = False
        '    GridBilanOffreFinancier.Visible = True

        'Else
        '    GridMoyenne.Visible = False
        '    ' GridNote.Visible = False
        '    GridSaisieOffreFinance.Visible = False
        '    GridBilanOffreFinancier.Visible = False

        'End If
    End Sub

    Private Function Points(ByVal Consult As String, Optional ByVal Evaluateur As String = "") As String

        Dim ValRet As String = ""
        Dim NbreEval As Decimal = 0

        Dim PtsTotal As Decimal = 0
        query = "select PointCritere from T_DP_CritereEval where NumeroDp='" & CmbNumDoss.Text & "' and CritereParent='0'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            PtsTotal += CDec(rw(0))
        Next

        'Requete = "select N.NoteConsult,C.PointCritere from T_SoumisNoteConsult as N,T_DP_CritereEval as C where N.RefCritere=C.RefCritere and N.CodeMem in (select CodeMem from T_Commission where NumeroDAO='" & CmbNumDoss.Text & "' and TypeComm='EVAC')"
        Dim Requete As String = ""
        If (Evaluateur = "") Then
            Requete = "select CodeMem from T_Commission where NumeroDAO='" & CmbNumDoss.Text & "' and TypeComm='EVAC'"
        Else
            Requete = "select CodeMem from T_Commission where NumeroDAO='" & CmbNumDoss.Text & "' and TypeComm='EVAC' and CodeMem='" & Evaluateur & "'"
        End If

        Dim PtsConsultant As Decimal = 0
        dt = ExcecuteSelectQuery(Requete)
        For Each rw As DataRow In dt.Rows

            Dim PtsConsEval As Decimal = 0
            Dim PtsTotalEval As Decimal = 0

            query = "select N.NoteConsult,C.PointCritere from T_SoumisNoteConsult as N, T_DP_CritereEval as C where N.RefCritere=C.RefCritere and C.TypeCritere='Note' and N.CodeMem='" & rw(0).ToString & "' and N.RefSoumis='" & Consult & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw0 As DataRow In dt.Rows
                PtsConsEval += CDec(IIf(IsNumeric(rw0(0).ToString), rw0(0).ToString, 0))
                'PtsTotalEval += CDec(IIf(IsNumeric(ReaderPtsEval.GetValue(1).ToString), ReaderPtsEval.GetValue(1).ToString, 0))
            Next

            PtsConsultant += PtsConsEval
            'PtsTotal += PtsTotalEval
            NbreEval += 1
        Next

        If (NbreEval > 0) Then
            PtsConsultant = PtsConsultant / NbreEval
            'PtsTotal = PtsTotal / NbreEval
        End If
        ValRet = Math.Round(PtsConsultant, 2).ToString & " / " & PtsTotal.ToString

        If (Evaluateur = "") Then

            ' On enregistre dans la table soumis *****
            Dim DatSet = New DataSet
            query = "select * from T_SoumissionConsultant where RefSoumis='" & Consult & "'"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Fill(DatSet, "T_SoumissionConsultant")

            DatSet.Tables!T_SoumissionConsultant.Rows(0)!NoteConsult = Math.Round(PtsConsultant, 2).ToString
            DatSet.Tables!T_SoumissionConsultant.Rows(0)!ReferenceNote = PtsTotal.ToString

            DatAdapt.Update(DatSet, "T_SoumissionConsultant")
            DatSet.Clear()
            BDQUIT(sqlconn)
        End If

        Return ValRet
    End Function

    Private Sub BtNoter_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If (GridViewComJugmt.RowCount = 1) Then
            'CodeEvaluateur = DernierEval
            'MsgBox("Eval=" & CodeEvaluateur, MsgBoxStyle.Information)
        Else
            ReponseDialog = ""
            ExceptRevue = ""
            ExceptRevue2 = ""
            ' CodeEvaluateur = ""
            Dim EvTrouve As Boolean = False
            While (EvTrouve = False)

                Dialog_form(EvaluateurPresent)
                If (ExceptRevue2 = "OUT") Then
                    Exit Sub
                End If

                query = "select NomMem,TitreMem,CodeMem from T_Commission where PasseMem='" & ReponseDialog & "' and NumeroDAO='" & CmbNumDoss.Text & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                If dt.Rows.Count > 0 Then
                    Dim NomEv As String = MettreApost(dt.Rows(0).Item(0).ToString) & " (" & dt.Rows(0).Item(1).ToString & ")"

                    For k As Integer = 0 To GridViewComJugmt.RowCount - 1
                        If (dtComm.Rows(k).Item(0).ToString = NomEv) Then
                            EvTrouve = True
                            '  CodeEvaluateur = dt.Rows(0).Item(2).ToString
                            ReponseDialog = ""
                            Exit For
                        Else
                            ExceptRevue = "NON"
                        End If
                    Next
                    'If (EvTrouve = True) Then Exit While
                Else
                    ExceptRevue = "NON"
                End If

                'Dim NomEv As String = NomDe(ReponseDialog)


            End While

        End If

        'ActiverNote = True
        ' BtNoter.Visible = False

        AfficherGrid()
        ' RemplirNote()

    End Sub

    Private Function NomDe(ByVal Code As String) As String

        Dim ValRet As String = ""
        query = "select NomMem,TitreMem from T_Commission where CodeMem='" & Code & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            ValRet = MettreApost(rw(0).ToString) & " (" & rw(1).ToString & ")"
        Next
        Return ValRet

    End Function

    Private Sub ClassementMoy()
        Dim LesEval(10) As String
        Dim NumEval As Decimal = 0
        query = "select CodeMem from T_Commission where NumeroDAO='" & CmbNumDoss.Text & "' and TypeComm='EVAC'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            LesEval(NumEval) = rw(0).ToString
            NumEval += 1
        Next

        Dim LesCritere(100) As String
        Dim NumCrit As Decimal = 0
        query = "select RefCritere from T_DP_CritereEval where NumeroDp='" & CmbNumDoss.Text & "' and TypeCritere='Note'"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            LesCritere(NumCrit) = rw("RefCritere").ToString
            NumCrit += 1
        Next

        Dim LesSoum(20) As String
        Dim NumSoum As Decimal = 0
        query = "select S.RefSoumis from T_Consultant as C,T_SoumissionConsultant as S where C.RefConsult=S.RefConsult and S.NumeroDp='" & CmbNumDoss.Text & "'"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            LesSoum(NumSoum) = rw("RefSoumis").ToString
            NumSoum += 1
        Next

        'vérification dans les notes
        For i As Integer = 0 To NumEval - 1
            For j As Integer = 0 To NumCrit - 1
                For k As Integer = 0 To NumSoum - 1
                    query = "select * from T_SoumisNoteConsult where RefSoumis='" & LesSoum(k) & "' and RefCritere='" & LesCritere(j) & "' and CodeMem='" & LesEval(i) & "'"
                    dt = ExcecuteSelectQuery(query)
                    If dt.Rows.Count > 0 Then

                    Else
                        Exit Sub
                    End If
                Next
            Next
        Next
        Dim ScorMinAdmis As Decimal = 0
        query = "select ScoreTechMin from T_DP where NumeroDp='" & CmbNumDoss.Text & "'"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            If (IsNumeric(rw(0)) = True) Then
                ScorMinAdmis = CDec(rw(0))
            End If
        Next

        Dim Tamp As String = ""
        Dim TampDec As Decimal = 0
        Dim LesCodeSoum(20) As String
        Dim LesMoy(20) As Decimal
        Dim NbSoum As Decimal = 0
        query = "select S.RefSoumis,S.NoteConsult from T_Consultant as C,T_SoumissionConsultant as S where C.RefConsult=S.RefConsult and S.NumeroDp='" & CmbNumDoss.Text & "'"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            LesCodeSoum(NbSoum) = rw("RefSoumis").ToString
            LesMoy(NbSoum) = CDec(rw("NoteConsult"))
            NbSoum += 1
        Next


        For i As Integer = 0 To NbSoum - 2
            For j As Integer = i + 1 To NbSoum - 1
                If (LesMoy(j) > LesMoy(i)) Then
                    Tamp = LesCodeSoum(i)
                    LesCodeSoum(i) = LesCodeSoum(j)
                    LesCodeSoum(j) = Tamp

                    TampDec = LesMoy(i)
                    LesMoy(i) = LesMoy(j)
                    LesMoy(j) = TampDec
                End If
            Next
        Next

        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)
        For k As Integer = 0 To NbSoum - 1

            Dim DatSet = New DataSet
            query = "select * from T_SoumissionConsultant where RefSoumis='" & LesCodeSoum(k) & "'"
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Fill(DatSet, "T_SoumissionConsultant")

            DatSet.Tables!T_SoumissionConsultant.Rows(0)!RangConsult = (k + 1).ToString
            DatSet.Tables!T_SoumissionConsultant.Rows(0)!EvalTechOk = IIf(LesMoy(k) >= ScorMinAdmis, "OUI", "NON").ToString

            DatAdapt.Update(DatSet, "T_SoumissionConsultant")
            DatSet.Clear()

        Next
        BDQUIT(sqlconn)

        BtOuvertureOffre.Enabled = True
    End Sub

    Private Sub MAJ_Pts_Soumis()
        DebutChargement(True, "Calcul et mise à jour des notes définitives...")
        query = "select S.RefSoumis from T_SoumissionConsultant as S,T_Consultant as C where S.RefConsult=C.RefConsult and S.NumeroDp='" & CmbNumDoss.Text & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Points(rw(0).ToString)
        Next
        ClassementMoy()

        FinChargement()
        MsgBox("Mise à jour terminée avec succès!", MsgBoxStyle.Information)
    End Sub

    'Private Sub BtEnrgNotes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgNotes.Click

    '    MAJ_Pts_Soumis()
    '    ActiverNote = False
    '    AfficherGrid()
    '    ' BtNoter.Visible = True
    '    BtEnrgNotes.Visible = False
    '    'RemplirMoyenne()
    'End Sub

    'Private Sub EvaluerCriteres_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EvaluerCriteres.Click
    '    If (ViewNote.RowCount > 0) Then
    '        DrX = ViewNote.GetDataRow(ViewNote.FocusedRowHandle)

    '        ExceptRevue = DrX(0).ToString
    '        ReponseDialog = DrX(2).ToString
    '        Dialog_form(FicheEvaluation)
    '        RemplirNote()
    '    End If
    'End Sub


    Dim xCritere(20) As String
    Dim xNbCritere As Decimal = 0

    Dim xSoumis(20) As String
    Dim xNbSoumis As Decimal = 0
    Dim xListeSoumis(20) As String
    Dim xRang(20) As Decimal

    Dim xEval(10) As String
    Dim xNbEval As Decimal = 0
    Dim xListeEval(6) As String

    Private Sub ChargerCritere()

        xNbCritere = 0
        query = "select RefCritere from T_DP_CritereEval where NumeroDp='" & CmbNumDoss.Text & "' and CritereParent='0' order by RefCritere"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            xCritere(xNbCritere) = rw(0).ToString
            xNbCritere += 1
        Next
    End Sub

    Private Sub ChargerConsult()

        xNbSoumis = 0
        query = "select S.RefSoumis,C.NomConsult,S.RangConsult from T_Consultant as C,T_SoumissionConsultant as S where S.RefConsult=C.RefConsult and C.NumeroDp='" & CmbNumDoss.Text & "' order by S.RefSoumis"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            xSoumis(xNbSoumis) = rw(0).ToString
            xListeSoumis(xNbSoumis) = rw(1).ToString
            xRang(xNbSoumis) = CInt(rw(2))

            xNbSoumis += 1
        Next

        query = "DELETE from t_tamp_consultrangtech"
        ExecuteNonQuery(query)

        Dim DatSet = New DataSet
        query = "select * from t_tamp_consultrangtech"
        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)
        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd)
        DatAdapt.Fill(DatSet, "t_tamp_consultrangtech")
        Dim DatTable = DatSet.Tables("t_tamp_consultrangtech")
        Dim DatRow = DatSet.Tables("t_tamp_consultrangtech").NewRow()
        For k As Integer = 0 To xNbSoumis - 1
            DatRow("Nom" & (k + 1).ToString) = xListeSoumis(k)
            DatRow("Rang" & (k + 1).ToString) = xRang(k)
        Next
        DatSet.Tables("t_tamp_consultrangtech").Rows.Add(DatRow)
        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        DatAdapt.Update(DatSet, "t_tamp_consultrangtech")
        DatSet.Clear()
        BDQUIT(sqlconn)
    End Sub

    Private Sub ChargerEval()
        xNbEval = 0
        query = "select CodeMem,NomMem,FoncMem,TitreMem from T_Commission where NumeroDAO='" & CmbNumDoss.Text & "' and TypeComm='EVAC' order by CodeMem"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            xEval(xNbEval) = rw(0).ToString
            xListeEval(xNbEval) = rw(1).ToString & IIf(rw(2).ToString <> "", " / " & rw(2).ToString, "").ToString & IIf(rw(3).ToString <> "", " (" & rw(3).ToString & ")", "").ToString
            xNbEval += 1
        Next
        query = "DELETE from T_TampEvalNom"
        ExecuteNonQuery(query)

        Dim DatSet = New DataSet
        query = "select * from T_TampEvalNom"
        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)
        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd)
        DatAdapt.Fill(DatSet, "T_TampEvalNom")
        Dim DatTable = DatSet.Tables("T_TampEvalNom")
        Dim DatRow = DatSet.Tables("T_TampEvalNom").NewRow()
        For k As Integer = 0 To xNbEval - 1
            DatRow("NomEval" & (k + 1).ToString) = xListeEval(k)
        Next
        DatSet.Tables("T_TampEvalNom").Rows.Add(DatRow)
        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        DatAdapt.Update(DatSet, "T_TampEvalNom")
        DatSet.Clear()
        BDQUIT(sqlconn)
    End Sub

    Private Sub ChargerCritereNote(ByVal Critere As String, ByRef Tab As String(), ByRef cpt As Decimal)
        cpt = 0
        query = "select RefCritere,TypeCritere from T_DP_CritereEval where CritereParent='" & Critere & "' order by RefCritere"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            If (rw(1).ToString = "Note") Then
                Tab(cpt) = rw(0).ToString
                cpt += 1
            End If

            query = "select RefCritere,TypeCritere from T_DP_CritereEval where CritereParent='" & rw(0).ToString & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw0 As DataRow In dt0.Rows
                If (rw0(1).ToString = "Note") Then
                    Tab(cpt) = rw0(0).ToString
                    cpt += 1
                End If
            Next
        Next

    End Sub

    Private Sub NoteEvalParConsult()
        Dim CritereNote(50) As String
        Dim NbCritereNote As Decimal = 0

        ChargerCritere()
        ChargerConsult()
        ChargerEval()

        query = "DELETE from T_NoteEvalParConsult"
        ExecuteNonQuery(query)

        For i As Integer = 0 To xNbCritere - 1
            ChargerCritereNote(xCritere(i), CritereNote, NbCritereNote)

            For j As Integer = 0 To xNbSoumis - 1
                Dim xNote(10) As String
                Dim xNbNote As Decimal = 0

                Dim xNoteTamp(10, 6) As String
                Dim xNbNoteLigNe As Integer = 0
                Dim xNbNoteColon As Decimal = 0

                For k As Integer = 0 To xNbEval - 1
                    Dim TampNote As Decimal = 0
                    xNbNoteLigNe = 0

                    For n As Decimal = 0 To NbCritereNote - 1
                        Dim TampNote2 As Decimal = 0

                        query = "select NoteConsult from T_SoumisNoteConsult where RefSoumis='" & xSoumis(j) & "' and CodeMem='" & xEval(k) & "' and RefCritere='" & CritereNote(n) & "'"
                        Dim dt As DataTable = ExcecuteSelectQuery(query)
                        For Each rw As DataRow In dt.Rows
                            TampNote += CDec(rw(0))
                            TampNote2 += CDec(rw(0))
                        Next
                        xNoteTamp(xNbNoteLigNe, xNbNoteColon) = TampNote2
                        xNbNoteLigNe += 1
                    Next
                    xNbNoteColon += 1
                    xNote(xNbNote) = TampNote.ToString
                    xNbNote += 1
                Next

                Dim DatSet = New DataSet
                query = "select * from T_NoteEvalParConsult"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "T_NoteEvalParConsult")
                Dim DatTable = DatSet.Tables("T_NoteEvalParConsult")

                Dim DatRow = DatSet.Tables("T_NoteEvalParConsult").NewRow()
                DatRow("RefCritere") = xCritere(i)
                DatRow("RefSoumis") = xSoumis(j)
                For m As Decimal = 0 To xNbEval - 1
                    DatRow("NoteEval" & (m + 1).ToString) = xNote(m)
                Next
                DatSet.Tables("T_NoteEvalParConsult").Rows.Add(DatRow)

                For w As Decimal = 0 To xNbNoteLigNe - 1
                    DatRow = DatSet.Tables("T_NoteEvalParConsult").NewRow()
                    DatRow("RefCritere") = CritereNote(w)
                    DatRow("RefSoumis") = xSoumis(j)
                    For m As Decimal = 0 To xNbNoteColon - 1
                        DatRow("NoteEval" & (m + 1).ToString) = xNoteTamp(w, m)
                    Next
                    DatSet.Tables("T_NoteEvalParConsult").Rows.Add(DatRow)
                Next
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "T_NoteEvalParConsult")
                DatSet.Clear()
                BDQUIT(sqlconn)
            Next
        Next
    End Sub

    Private Sub NoteConsultParCritere()

        Dim CritereNote(50) As String
        Dim NbCritereNote As Decimal = 0

        ChargerCritere()
        ChargerConsult()
        ChargerEval()
        query = "DELETE from T_NoteConsultParCritere"
        ExecuteNonQuery(query)
        Dim TabEval(10, 6) As Decimal

        For i As Integer = 0 To xNbCritere - 1
            ChargerCritereNote(xCritere(i), CritereNote, NbCritereNote)
            Dim xNote(20) As String
            Dim xNbNote As Decimal = 0

            For x As Decimal = 0 To 9
                For y As Decimal = 0 To 5
                    TabEval(x, y) = 0
                Next
            Next

            For j As Integer = 0 To xNbSoumis - 1
                Dim TampNote As Decimal = 0
                For n As Decimal = 0 To NbCritereNote - 1
                    For z As Decimal = 0 To xNbEval - 1
                        Dim Tamp2 As Decimal = 0
                        query = "select NoteConsult from T_SoumisNoteConsult where RefSoumis='" & xSoumis(j) & "' and RefCritere='" & CritereNote(n) & "' and CodeMem='" & xEval(z) & "'"
                        Dim dt As DataTable = ExcecuteSelectQuery(query)
                        For Each rw As DataRow In dt.Rows
                            TampNote += CDec(rw(0))
                            Tamp2 += CDec(rw(0))
                        Next
                        TabEval(j, z) += Tamp2
                    Next
                Next
                xNote(xNbNote) = Math.Round(TampNote / (xNbEval), 2).ToString
                xNbNote += 1
            Next

            Dim DatSet = New DataSet
            query = "select * from T_NoteConsultParCritere"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_NoteConsultParCritere")
            Dim DatTable = DatSet.Tables("T_NoteConsultParCritere")
            Dim DatRow = DatSet.Tables("T_NoteConsultParCritere").NewRow()

            DatRow("RefCritere") = xCritere(i)
            DatRow("NumeroDp") = CmbNumDoss.Text
            For m As Decimal = 0 To xNbSoumis - 1
                DatRow("NoteCons" & (m + 1).ToString) = xNote(m)
                For p As Integer = 0 To xNbEval - 1
                    DatRow("nEval" & (p + 1).ToString & (m + 1).ToString) = Math.Round(TabEval(m, p), 2)
                Next
            Next

            DatSet.Tables("T_NoteConsultParCritere").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_NoteConsultParCritere")
            DatSet.Clear()
            BDQUIT(sqlconn)
        Next

    End Sub


#End Region

#Region "Rapport d'évaluation technique"

    Private Sub BtRapportEvalTech_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtRapportEvalTech.Click
        Try
            'Bouton du rapport d'evaluation technique non cliqué
            If TablBoutonClik(0) = False Then
                DateSoumRapTech.Text = DateEnvoiRapport(0).ToString
                DateAviObj.Text = DateEnvoiRapport(1).ToString

                If Not ExisteTexteGeneralites Then
                    ReponseDialog = ""
                    EvalConsult_TexteGeneralites.ShowDialog()
                    ExecuteNonQuery("Update t_dp set TexteGeneralites='" & EnleverApost(ReponseDialog) & "' where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'")
                    ExisteTexteGeneralites = True
                End If

                Dim CheminDoss = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\Evaluation_Technique" & "\" & CheminRapportEvalTech.ToString

                If CheminRapportEvalTech.ToString = "" Then
                    BtGenerers_Click(Me, e)
                ElseIf Not File.Exists(CheminDoss) Then
                    If ConfirmMsg("Le rapport d'évaluation technique n'existe pas ou a été supprimé" & vbNewLine & "Voulez-vous le générer à nouveau ?") = DialogResult.Yes Then
                        BtGenerers_Click(Me, e)
                    End If
                ElseIf File.Exists(CheminDoss) Then
                    DebutChargement(True, "Chargement du rapport d'évaluation technique en cours...")
                    WebBrowser2.Navigate(CheminDoss.ToString)
                    Threading.Thread.Sleep(5000)
                    FinChargement()
                End If

                If RaportEvalTechValider.ToString = "Valider" Then
                    DateSoumRapTech.Enabled = False
                    DateAviObj.Enabled = False
                    BoutonEvalTech(False)
                Else
                    BoutonEvalTech(True)
                    DateSoumRapTech.Enabled = True
                    DateAviObj.Enabled = True
                End If

                TablBoutonClik(0) = True
            End If

            GetVisiblePanel(True, "EvalTech")
        Catch ex As Exception
            TablBoutonClik(0) = False
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub DateSoumRapTech_LostFocus(sender As Object, e As EventArgs) Handles DateSoumRapTech.LostFocus
        If DateSoumRapTech.Text <> "" Then
            DateEnvoiRapport(0) = DateSoumRapTech.Text
            ExecuteNonQuery("Update t_dp set DateSoumRapTechBail='" & DateSoumRapTech.Text & "' where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'")
        End If
    End Sub
    Private Sub DateAviObj_LostFocus(sender As Object, e As EventArgs) Handles DateAviObj.LostFocus
        If DateAviObj.Text <> "" Then
            DateEnvoiRapport(1) = DateAviObj.Text
            ExecuteNonQuery("Update t_dp set DateAviObjectionRapTech='" & DateAviObj.Text & "' where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'")
        End If
    End Sub

    Private Sub BoutonEvalTech(ByVal value As Boolean)
        BtEnvoieBailleurs.Enabled = value
        BtValiderRaports.Enabled = value
        BtRejetterRapports.Enabled = value
        BtGenerers.Enabled = value
        BtModifiers.Enabled = value
        BtActualisers.Enabled = value
    End Sub

    'Voir le resultat de l'evaluation technique
    Private Sub BtResultEvalTechniq_Click(sender As Object, e As EventArgs) Handles BtResultEvalTechniq.Click
        GetVisiblePanel(True, "Accueil")
    End Sub

    Private Sub BtGenerers_Click(sender As Object, e As EventArgs) Handles BtGenerers.Click
        ChargerRapportEvaluationTech()
    End Sub

    Private Sub BtModifiers_Click(sender As Object, e As EventArgs) Handles BtModifiers.Click
        If CheminRapportEvalTech.ToString = "" Then
            FailMsg("Aucun rapport à modifier")
            Exit Sub
        End If
        Dim CheminDoss As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\Evaluation_Technique\RapportEvaluationTechnique.doc"
        If File.Exists(CheminDoss.ToString) Then
            DebutChargement(True, "Chargement du rapport d'évaluation technique en cours...")
            Process.Start(CheminDoss.ToString)
            RapportModif = True
            FinChargement()
        ElseIf ConfirmMsg("Le rapport d'évaluation technique n'existe pas ou a été supprimé" & vbNewLine & "Voulez-vous le générer ?") = DialogResult.Yes Then
            BtGenerers_Click(Me, e)
        End If
    End Sub

    Private Sub BtActualisers_Click(sender As Object, e As EventArgs) Handles BtActualisers.Click
        Try

            If CheminRapportEvalTech.ToString = "" Then
                FailMsg("Aucun rapport à actualiser")
                Exit Sub
            End If

            If RapportModif = True Then 'Modification appliquer
                Dim CheminDoss As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\Evaluation_Technique"

                If File.Exists(CheminDoss & "\RapportEvaluationTechnique.doc") = True Then

                    DebutChargement(True, "Actualisation du rapport d'évaluation en cours...")
                    Dim NewCheminpdf As String = "RapportEvalTech_" & FormatFileName(Now.ToString.Replace(" ", ""), "") & ".pdf"

                    Dim WdApp As New Word.Application
                    Dim WdDoc As Word.Document = WdApp.Documents.Add(CheminDoss & "\RapportEvaluationTechnique.doc")

                    Try
                        WdDoc.SaveAs2(FileName:=CheminDoss.ToString & "\" & NewCheminpdf.ToString, FileFormat:=Word.WdSaveFormat.wdFormatPDF)
                        WdDoc.Close(True)
                        WdApp.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                    Catch ep As IO.IOException
                        FinChargement()
                        SuccesMsg("Un exemplaire du rapport d'évaluation technique est ouvert dans une autre application" & vbNewLine & "Veuillez le fermer svp.")
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

                    ExecuteNonQuery("Update t_dp Set CheminRapportEvalTech= '" & NewCheminpdf & "' where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'")
                    WebBrowser2.Navigate(CheminDoss.ToString & "\" & NewCheminpdf.ToString)
                    Threading.Thread.Sleep(5000)
                    FinChargement()
                    RapportModif = False
                    CheminRapportEvalTech = NewCheminpdf
                Else
                    SuccesMsg("Le chemin spécifié n'existe pas")
                End If
            ElseIf RapportModif = False Then
                SuccesMsg("Veuillez modifier le rapport avant d'actualiser")
            End If
        Catch ep As IOException
            FinChargement()
            SuccesMsg("Un exemplaire du rapport d'évaluation technique est ouvert dans une autre application" & vbNewLine & "Veuillez le fermer svp.")
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub BtEnvoieBailleurs_Click(sender As Object, e As EventArgs) Handles BtEnvoieBailleurs.Click
        If CheminRapportEvalTech.ToString = "" Then
            FailMsg("Aucun rapport à envoyer au bailleur")
            Exit Sub
        End If

        Try
            'Info de l'envoi de l'email
            If ChargerLesDonneEmail_AMI_DP_SERVICEAUTRES(CmbNumDoss.Text, "DP") = False Then
                Exit Sub
            End If

            If ConfirmMsg("Confirmez-vous l'envoi du rapport d'évaluation" & vbNewLine & "technique au bailleur [ " & MettreApost(rwDossDPAMISA.Rows(0)("InitialeBailleur").ToString) & " ]") = DialogResult.Yes Then
                Try
                    Dim CheminDoc As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\Evaluation_Technique\RapportEvaluationTechnique.doc"

                    If File.Exists(CheminDoc) = True Then
                        DebutChargement(True, "Envoi du rapport d'évaluation technique au bailleur...")

                        'Envoi du rapport au bailleur
                        If EnvoiMailRapport(NomBailleurRetenu, CmbNumDoss.Text, EmailDestinatauer, CheminDoc, EmailCoordinateurProjet, EmailResponsablePM, "Rapport d'évaluation technique") = False Then Exit Sub

                        SuccesMsg("Le rapport d'évaluation technique a été envoye avec succès")
                        FinChargement()
                    Else
                        FinChargement()
                        SuccesMsg("Le rapport à envoyer n'existe pas ou a été supprimé")
                    End If
                Catch ep As IO.IOException
                    SuccesMsg("Le fichier est utilisé par une autre application" & vbNewLine & "Veuillez le fermer svp.")
                    FinChargement()
                Catch ex As Exception
                    FailMsg(ex.ToString())
                    FinChargement()
                End Try
            End If
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub BtValiderRaports_Click(sender As Object, e As EventArgs) Handles BtValiderRaports.Click
        Try
            If CheminRapportEvalTech.ToString = "" Then
                FailMsg("Aucun rapport à valider")
                Exit Sub
            End If

            If ConfirmMsg("Confirmez-vous la validation du rapport ?") = DialogResult.Yes Then
                ExecuteNonQuery("Update t_dp set RaportEvalTechBailleur='Valider' where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'")
                SuccesMsg("Rapport d'évaluation technique validé")
                BoutonEvalTech(False)
                DateSoumRapTech.Enabled = False
                DateAviObj.Enabled = False
                RaportEvalTechValider = "Valider"
                'Verifier s'il y a des consultant retenu sur la liste pour activer le bouton de l'evaluation fin
                'Etape evaluation financières
                If Val(ExecuteScallar("select count(*) from t_soumissionconsultant where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and EvalTechOk='OUI'")) > 0 Then
                    BtEvalautionFinanciere.Enabled = True
                End If
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub BtRejetterRapports_Click(sender As Object, e As EventArgs) Handles BtRejetterRapports.Click
        Try
            If CheminRapportEvalTech.ToString = "" Then
                FailMsg("Aucun rapport à rejeter")
                Exit Sub
            End If

            If ConfirmMsg("Voulez-vous vraiment rejeter ce rapport ?") = DialogResult.Yes Then

                DebutChargement(True, "Rejete du rapport d'évaluation en cours...")

                ExecuteNonQuery("Update t_dp set TexteGeneralites=NULL,CheminRapportEvalTech=NULL, EvalTechnique=NULL, RaportEvalTechBailleur='Rejeter'  where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'")
                ExecuteNonQuery("Update t_soumi_note_consultant_parcriteresdp set ValidationNote='' where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'")
                ExecuteNonQuery("Update t_soumissionconsultant set NoteConsult=NULL, ReferenceNote=NULL, RangConsult=NULL, EvalTechOk=NULL where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'")

                FinChargement()
                SuccesMsg("Rapport d'évaluation rejeté")
                BoutonEvalTech(False)
                ChargerDossier()
            End If
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub Btpdf_Click(sender As Object, e As EventArgs) Handles Btpdf.Click
        Try

            If CheminRapportEvalTech.ToString = "" Then
                FailMsg("Il n'existe aucun rapport d'évaluation technique")
                Exit Sub
            End If
            Dim CheminDoss As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\Evaluation_Technique\" & CheminRapportEvalTech.ToString
            If File.Exists(CheminDoss.ToString) Then
                If ExporterPDF(CheminDoss.ToString, "RapportEvalTechnique.pdf") = False Then
                    Exit Sub
                End If
            Else
                FailMsg("La version du rapport à exporter" & vbNewLine & "n'existe pas ou a été supprimé")
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub BtWord_Click(sender As Object, e As EventArgs) Handles BtWord.Click
        Try
            If CheminRapportEvalTech.ToString = "" Then
                FailMsg("Il n'existe aucun rapport d'évaluation technique")
                Exit Sub
            End If
            Dim CheminDoss As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\Evaluation_Technique\RapportEvaluationTechnique.doc"
            If File.Exists(CheminDoss.ToString) Then
                If ExporterWORD(CheminDoss.ToString, "Rapport_Evaluation_Technique.doc") = False Then
                    Exit Sub
                End If
            Else
                FailMsg("La version du rapport à exporter" & vbNewLine & "n'existe pas ou a été supprimé")
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub EnregistreNometRangCons()
        'Nom et rang des consultants
        query = "SELECT c.RefConsult, c.NomConsult, s.RangConsult FROM T_consultant as c, t_soumissionconsultant as s where c.RefConsult=s.RefConsult and s.NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and s.RangConsult IS NOT NULL ORDER BY s.RangConsult ASC"
        Dim dtConsult As DataTable = ExcecuteSelectQuery(query)

        Dim DatSet = New DataSet
        query = "select * from t_tamp_consultrangtech"
        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)

        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd)
        DatAdapt.Fill(DatSet, "t_tamp_consultrangtech")
        Dim DatTable = DatSet.Tables("t_tamp_consultrangtech")
        Dim DatRow = DatSet.Tables("t_tamp_consultrangtech").NewRow()

        DatRow("CodeProjet") = ProjetEnCours
        DatRow("CodeUtils") = CodeUtilisateur

        Dim cons As Integer = 0

        For Each rw In dtConsult.Rows
            cons += 1
            DatRow("Nom" & cons.ToString) = rw("NomConsult").ToString
            DatRow("Rang" & cons.ToString) = rw("RangConsult").ToString.Replace(".", ",")
        Next

        DatSet.Tables("t_tamp_consultrangtech").Rows.Add(DatRow)
        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        DatAdapt.Update(DatSet, "t_tamp_consultrangtech")
        DatSet.Clear()
        BDQUIT(sqlconn)
    End Sub

    Private Function EnregistreMoyenneetNote() As Decimal
        Dim Cons As Integer = 0
        Dim Note As Integer = 0
        Dim MoyenneConsul As Decimal = 0

        'moyenne et notes des consultants.
        query = "SELECT RefSoumis FROM t_soumissionconsultant where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and RangConsult IS NOT NULL ORDER BY RangConsult ASC"
        Dim dtConsult As DataTable = ExcecuteSelectQuery(query)

        query = "SELECT DISTINCT RefCritere  FROM t_soumi_note_consultant_parcriteresdp where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dtRefCritere As DataTable = ExcecuteSelectQuery(query)

        Dim DatSet = New DataSet
        query = "select * from T_NoteConsultParCritere"
        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)
        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd)
        DatAdapt.Fill(DatSet, "T_NoteConsultParCritere")
        Dim DatTable = DatSet.Tables("T_NoteConsultParCritere")

        For Each rwRefCritere In dtRefCritere.Rows
            Cons = 0

            Dim DatRow = DatSet.Tables("T_NoteConsultParCritere").NewRow()

            DatRow("NumeroDp") = EnleverApost(CmbNumDoss.Text)
            DatRow("RefCritere") = rwRefCritere("RefCritere").ToString

            For Each rwConsult In dtConsult.Rows
                Cons += 1
                MoyenneConsul = 0

                query = "SELECT NoteConsult from t_soumi_note_consultant_parcriteresdp where RefSoumis='" & rwConsult("RefSoumis") & "' and RefCritere='" & rwRefCritere("RefCritere") & "' and NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dtNotes As DataTable = ExcecuteSelectQuery(query)

                Note = 0
                For Each rw In dtNotes.Rows
                    Note += 1
                    DatRow("nEval" & Note.ToString & Cons.ToString) = rw("NoteConsult").ToString.Replace(".", ",")
                    MoyenneConsul += CDec(rw("NoteConsult").ToString.Replace(".", ","))
                Next

                DatRow("NoteCons" & Cons.ToString) = Round(MoyenneConsul / Note, 2)
            Next

            DatSet.Tables("T_NoteConsultParCritere").Rows.Add(DatRow)
        Next

        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        DatAdapt.Update(DatSet, "T_NoteConsultParCritere")
        DatSet.Clear()
        BDQUIT(sqlconn)

        Return dtConsult.Rows.Count
    End Function

    Private Sub ChargerRapportEvaluationTech()
        Try
            DebutChargement(True, "Génération du rapport d'évaluation technique en cours...")

            ExecuteNonQuery("delete from t_tamp_consultrangtech")
            ExecuteNonQuery("delete from t_noteconsultparcritere")

            'Enregistrement des nom et des rangs
            EnregistreNometRangCons()

            'Enregistrement des notes et des moyennes
            Dim NbreConsult As Decimal = EnregistreMoyenneetNote()

            'Afficharge de l'etat
            Dim NumDoss As String = EnleverApost(CmbNumDoss.Text)

            Dim RapportEvalTech As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table

            Dim DatSet = New DataSet

            Dim Chemin As String = lineEtat & "\Marches\DP\Rapport Evaluation Consultant\"

            ' RapportEvalTech.Load(Chemin & "RapportEvaluationTechnique.rpt")
            RapportEvalTech.Load(Chemin & "RapportEvalTechnique.rpt")

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = RapportEvalTech.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            RapportEvalTech.SetDataSource(DatSet)

            RapportEvalTech.SetParameterValue("CodeProjet", ProjetEnCours)
            RapportEvalTech.SetParameterValue("NumDP", NumDoss)
            Dim NbreConsultRecu As Decimal = Val(ExecuteScallar("SELECT COUNT(*) from t_consultant where NumeroDp='" & NumDoss & "' and DateDepot<>''"))
            Dim NbreConsultRetenu As Decimal = Val(ExecuteScallar("SELECT COUNT(*) from t_consultant where NumeroDp='" & NumDoss & "'"))
            Dim NbreEvaluateurs As Decimal = Val(ExecuteScallar("select count(*) from t_commission where NumeroDAO='" & EnleverApost(CmbNumDoss.Text) & "'"))
            RapportEvalTech.SetParameterValue("NbreEvaluateur", NbreEvaluateurs)
            RapportEvalTech.SetParameterValue("NbreConsultantRetenu", NbreConsultRetenu)
            RapportEvalTech.SetParameterValue("NbrePropositionRecues", NbreConsultRecu)
            RapportEvalTech.SetParameterValue("NbreConsultant", NbreConsult)
            Dim ScoreTechMin As Decimal = Val(ExecuteScallar("select ScoreTechMin from t_dp where NumeroDp='" & NumDoss & "'"))
            RapportEvalTech.SetParameterValue("ScoreTechMin", ScoreTechMin.ToString)

            Dim CheminDoss = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\Evaluation_Technique"
            If (Directory.Exists(CheminDoss) = False) Then
                Directory.CreateDirectory(CheminDoss)
            End If

            Dim NewCheminpdf As String = "RapportEvalTech_" & FormatFileName(Now.ToString.Replace(" ", ""), "") & ".pdf"
            Dim NewCheminpdf2 As String = CheminDoss & "\" & NewCheminpdf.ToString

            RapportEvalTech.ExportToDisk(ExportFormatType.WordForWindows, CheminDoss & "\RapportEvaluationTechnique.doc")
            RapportEvalTech.ExportToDisk(ExportFormatType.PortableDocFormat, NewCheminpdf2)
            ' ViewRapportEval.ReportSource = RapportEvalTech
            ExecuteNonQuery("Update t_dp set CheminRapportEvalTech='" & NewCheminpdf.ToString & "' where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'")
            CheminRapportEvalTech = NewCheminpdf.ToString
            FinChargement()

            DebutChargement(True, "Chargement du rapport d'évaluation technique...")
            WebBrowser2.Navigate(NewCheminpdf2.ToString)
            Threading.Thread.Sleep(5000)
            FinChargement()
        Catch ep As IOException
            FinChargement()
            SuccesMsg("Un exemplaire du rapport d'évaluation technique est ouvert dans une autre application" & vbNewLine & "Veuillez le fermer svp.")
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

#End Region

#Region "Evaluation Financiere"

    Private Sub BtEvalautionFinanciere_Click(sender As Object, e As EventArgs) Handles BtEvalautionFinanciere.Click
        Try
            If TablBoutonClik(1) = False Then 'Bouton deja cliquer
                'Verifier s'il y a des consultants retenu
                Dim dtDoss As DataTable = ExcecuteSelectQuery("select * from T_SoumissionConsultant where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and EvalTechOk='OUI' ORDER BY RangConsult ASC")
                If dtDoss.Rows.Count > 0 Then rwDossOffreFin = dtDoss.Rows(0)

                'Cas des methodes *************** SFQ, SQC 
                'Seul l'offres du premier est ouvert

                If VerifiersMetohd(TxtMethode.Text) = False Then
                    GroupBoxOuvertureOffres.Visible = True 'afficher le groupBox des overtures
                    RemplirListeOuverture() 'Remplir la liste des ouverture effectué

                    'Afficher l'offre du premier consultant
                    If rwDossOffreFin("FinEvalFinanciere").ToString <> "" Then 'Evaluation financière terminer
                        RemplirBilanEvalConsult(rwDossOffreFin("RefSoumis"))
                    Else
                        RemplirOffreFinanciere(rwDossOffreFin("RefSoumis")) 'l'offre du premier
                    End If

                    ' Cas des methodes *************** SFQC SCBD SMC
                ElseIf VerifiersMetohd(TxtMethode.Text) = True Then
                    GroupBoxOuvertureOffres.Visible = False
                    If rwDossOffreFin("FinEvalFinanciere").ToString <> "" Then
                        RemplirBilanEvalConsult()
                    Else
                        RemplirOffreFinanciere()
                    End If
                Else
                    FailMsg("Impossible d'accéder à ce bouton" & vbNewLine & "Aucun traitement prévu pour la méthode [" & TxtMethode.Text & "]")
                    Exit Sub
                End If

                PersonaliserTexte()
                FinChargement()

                TablBoutonClik(1) = True
            End If

            GetVisiblePanel(True, "OffreFinance")
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub PersonaliserTexte()
        'Personnalisation du texte
        If rwDossOffreFin("DateOuvertureEvalFin").ToString = "" Then 'Ouverture non demarrer
            BtOuvertureOffre.Text = "Démarrer l'ouverture" & vbNewLine & "des offres financières"
        ElseIf rwDossOffreFin("DateOuvertureEvalFin").ToString <> "" And rwDossOffreFin("DateFinOuvertEvalFin").ToString = "" Then 'Ouverture demarrer mais non terminer
            BtOuvertureOffre.Text = "Fin ouverture" & vbNewLine & "des offres financières"
        ElseIf rwDossOffreFin("DateOuvertureEvalFin").ToString <> "" And rwDossOffreFin("DateFinOuvertEvalFin").ToString <> "" Then
            BtOuvertureOffre.Text = "Etat PV d'ouverture" & vbNewLine & "des offres financières"
        End If

        BtValiderEvalOffresFin.Enabled = IIf(rwDossOffreFin("DateOuvertureEvalFin").ToString = "", False, IIf(rwDossOffreFin("DateFinOuvertEvalFin").ToString = "", True, IIf(rwDossOffreFin("FinEvalFinanciere").ToString <> "", False, True).ToString).ToString).ToString
        BtResultatEvalFin.Enabled = IIf(rwDossOffreFin("FinEvalFinanciere").ToString <> "", True, False).ToString
    End Sub

    Private Sub RemplirListeOuverture()
        CmbNumOuvertureOffre.Text = ""
        CmbNumOuvertureOffre.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery("select RefOuverture from t_soumissionconsultant where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' AND EvalTechOk='OUI' and RefOuverture IS NOT NULL")
        For Each rw In dt.Rows
            CmbNumOuvertureOffre.Properties.Items.Add(rw("RefOuverture").ToString)
        Next
    End Sub

    Private Sub SaisirOffre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaisirOffre.Click
        If (ViewSaisiOffreFinance.RowCount > 0) Then
            DrX = ViewSaisiOffreFinance.GetDataRow(ViewSaisiOffreFinance.FocusedRowHandle)
            Dim IndexSelec As Integer = ViewSaisiOffreFinance.FocusedRowHandle

            'verifier s'il existe une monnaie d'évaluation
            If MonnaieEvaluation.ToString = "" Then
                FailMsg("Veuillez ajouter une monnaie pour l'évaluation des offres")
                Exit Sub
            End If

            MonnaieEvalOffre = MonnaieEvaluation.ToString
            ExceptRevue = DrX("RefSoumis").ToString
            ReponseDialog = DrX("Consultant").ToString
            Dim NewOffreFinanciere As New EvalOffreFinanciereFinal
            NewOffreFinanciere.NumDossDp = CmbNumDoss.Text
            NewOffreFinanciere.ShowDialog()
        End If
    End Sub

    Private Sub RésultatCalculOffreFinancièreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RésultatCalculOffreFinancièreToolStripMenuItem.Click
        CalculerToolStripMenuItem_Click(Me, e)
    End Sub

    Private Sub CalculerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CalculerToolStripMenuItem.Click
        If (ViewSaisiOffreFinance.RowCount > 0) Then
            DrX = ViewSaisiOffreFinance.GetDataRow(ViewSaisiOffreFinance.FocusedRowHandle)
            MonnaieEvalOffre = MonnaieEvaluation.ToString
            ReponseDialog = DrX("RefSoumis").ToString
            Dim NewCalculOffrs As New CalculOffreFinanciere
            If DrX("Statut de l'offre").ToString = "Calculé" Then
                NewCalculOffrs.StatutOffres = True
                NewCalculOffrs.BtEnrgCalcul.Enabled = False
            Else
                NewCalculOffrs.StatutOffres = False
                NewCalculOffrs.BtEnrgCalcul.Enabled = True
            End If
            NewCalculOffrs.NumDos = CmbNumDoss.Text
            NewCalculOffrs.MethodeMarches = TxtMethode.Text
            NewCalculOffrs.TxtNomSoumis.Text = DrX("Consultant").ToString
            NewCalculOffrs.ShowDialog()
        End If
    End Sub

    Private Sub GridSaisieOffreFinance_MouseUp(sender As Object, e As MouseEventArgs) Handles GridSaisieOffreFinance.MouseUp
        If (ViewSaisiOffreFinance.RowCount > 0) Then
            DrX = ViewSaisiOffreFinance.GetDataRow(ViewSaisiOffreFinance.FocusedRowHandle)
            Dim LigneSelect = ViewSaisiOffreFinance.FocusedRowHandle

            If DrX("Statut de l'offre").ToString = "..." Then
                ContextMenuStrip2.Items(0).Text = "Saisir Offre Financière"
                ContextMenuStrip2.Items(0).Enabled = True
                ContextMenuStrip2.Items(1).Enabled = False
                ContextMenuStrip2.Items(2).Enabled = False
            ElseIf DrX("Statut de l'offre").ToString = "A calculer" And rwDossOffreFin("DateFinOuvertEvalFin").ToString = "" Then 'Fin ouverture des offres non terminer
                ContextMenuStrip2.Items(0).Text = "Saisir Offre Financière"
                ContextMenuStrip2.Items(0).Enabled = True
                ContextMenuStrip2.Items(1).Enabled = False
                ContextMenuStrip2.Items(2).Enabled = False
            ElseIf DrX("Statut de l'offre").ToString = "A calculer" And rwDossOffreFin("DateFinOuvertEvalFin").ToString <> "" Then 'Fin ouverture des offres terminer
                ContextMenuStrip2.Items(0).Text = "Saisir Offre terminée"
                ContextMenuStrip2.Items(0).Enabled = False
                ContextMenuStrip2.Items(1).Enabled = True
                ContextMenuStrip2.Items(2).Enabled = False
            ElseIf DrX("Statut de l'offre").ToString = "Calculé" Then
                ContextMenuStrip2.Items(0).Text = "L'ofrre de " & DrX("Consultant").ToString & " a été saisie et calculée"
                ContextMenuStrip2.Items(0).Enabled = False
                ContextMenuStrip2.Items(1).Enabled = False
                ContextMenuStrip2.Items(2).Enabled = True
            End If
        End If
    End Sub

    Private Sub ContextMenuStrip2_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip2.Opening
        If (ViewSaisiOffreFinance.RowCount = 0 Or rwDossOffreFin("DateOuvertureEvalFin").ToString = "") Then
            e.Cancel = True
        End If
    End Sub

    Private Sub RemplirBilanEvalConsult(Optional RefSoumis As String = "")
        Dim dtBilanCons As DataTable = New DataTable()
        dtBilanCons.Columns.Clear()
        dtBilanCons.Columns.Add("Code", Type.GetType("System.String"))
        dtBilanCons.Columns.Add("CodeX", Type.GetType("System.String"))
        dtBilanCons.Columns.Add("Consultant", Type.GetType("System.String"))
        dtBilanCons.Columns.Add("Score Technique (T)", Type.GetType("System.String"))
        dtBilanCons.Columns.Add("Offre financière", Type.GetType("System.String"))
        dtBilanCons.Columns.Add("Score Financier (F)", Type.GetType("System.String"))

        Dim NewAddColum As String = ""
        'Les coefficients
        Dim CoefTech As String = ""
        Dim CoefFin As String = ""
        query = "select PoidsTech,PoidsFin from T_DP where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CoefTech = rw("PoidsTech").ToString
            CoefFin = rw("PoidsFin").ToString
        Next
        NewAddColum = "Score Pondéré (P=T x " & CoefTech & " & F x " & CoefFin & ")"
        dtBilanCons.Columns.Add(NewAddColum, Type.GetType("System.String"))
        dtBilanCons.Columns.Add("Rang", Type.GetType("System.String"))
        dtBilanCons.Columns.Add("Décision", Type.GetType("System.String"))
        dtBilanCons.Rows.Clear()

        If RefSoumis.ToString = "" Then
            query = "Select S.RefSoumis, C.NomConsult, S.NoteConsult, S.MontantAjusterLocal, S.ScoreFinancier, S.MoyPonderee, S.RangFinal, S.EvalFinOk from T_Consultant As C, T_SoumissionConsultant as S where S.RefConsult=C.RefConsult And C.NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and S.EvalTechOk='OUI' and S.RangFinal IS NOT NULL order by S.RangFinal"
        Else
            query = "Select S.RefSoumis, C.NomConsult, S.NoteConsult, S.MontantAjusterLocal, S.ScoreFinancier, S.MoyPonderee, S.RangFinal, S.EvalFinOk from T_Consultant As C, T_SoumissionConsultant as S where S.RefConsult=C.RefConsult And C.NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and S.EvalTechOk='OUI' and S.RangFinal IS NOT NULL and S.RefSoumis='" & RefSoumis & "'"
        End If
        dt = ExcecuteSelectQuery(query)

        Dim cpt2 As Decimal = 0
        For Each rw As DataRow In dt.Rows
            cpt2 += 1
            Dim DrE = dtBilanCons.NewRow()

            DrE("Code") = rw("RefSoumis").ToString
            DrE("CodeX") = IIf(CDec(cpt2 / 2) = CDec(cpt2 \ 2), "x", "")
            DrE("Consultant") = MettreApost(rw("NomConsult").ToString)
            DrE("Score Technique (T)") = rw("NoteConsult").ToString.Replace(".", ",")
            DrE("Offre financière") = AfficherMonnaie(rw("MontantAjusterLocal").ToString) & " " & MonnaieEvaluation.ToString
            DrE("Rang") = rw("RangFinal").ToString & IIf(rw("RangFinal").ToString = "1", "er", "ème").ToString
            DrE("Décision") = IIf(rw("EvalFinOk").ToString <> "", IIf(rw("EvalFinOk").ToString = "OUI", "ACCEPTE", "REFUSE").ToString, "-").ToString
            DrE("Score Financier (F)") = rw("ScoreFinancier").ToString
            DrE(NewAddColum) = rw("MoyPonderee").ToString
            dtBilanCons.Rows.Add(DrE)
        Next

        GridBilanOffreFinancier.DataSource = dtBilanCons
        ViewBilanOffre.Columns("Code").Visible = False
        ViewBilanOffre.Columns("CodeX").Visible = False
        ViewBilanOffre.Columns("Décision").Width = 50
        ViewBilanOffre.Columns("Rang").Width = 50
        ViewBilanOffre.Columns("Offre financière").Width = 150
        ViewBilanOffre.Columns("Score Technique (T)").Width = 100
        ViewBilanOffre.Columns("Score Financier (F)").Width = 100
        ViewBilanOffre.Columns(NewAddColum).Width = 100

        ViewBilanOffre.OptionsView.ColumnAutoWidth = True
        ViewBilanOffre.Columns("Offre financière").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewBilanOffre.Columns("Score Technique (T)").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewBilanOffre.Columns("Offre financière").AppearanceCell.Font = New Font("Tahoma", 8, FontStyle.Bold)

        ViewBilanOffre.Columns("Rang").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewBilanOffre.Columns("Décision").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewBilanOffre.Columns("Rang").AppearanceCell.Font = New Font("Tahoma", 8, FontStyle.Bold)

        ViewBilanOffre.Columns("Score Financier (F)").AppearanceCell.Font = New Font("Tahoma", 8, FontStyle.Bold)
        ViewBilanOffre.Columns("Score Financier (F)").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewBilanOffre.Columns(NewAddColum).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ColorRowGrid(ViewBilanOffre, "[CodeX]='x'", Color.LightGray, "Tahoma", 10, FontStyle.Regular, Color.Black)
        ColorRowGridAnal(ViewBilanOffre, "[Décision]='REFUSE'", Color.White, "Tahoma", 10, FontStyle.Regular, Color.Red, False)

        Dim MethodsTraites As Boolean = VerifiersMetohd(TxtMethode.Text)
        'Cas de SFQC, SCBD, SMC ******************
        If TxtMethode.Text.ToUpper = "SFQC" Then
            ViewBilanOffre.Columns("Décision").Visible = True
            ViewBilanOffre.Columns("Rang").Visible = True
            ViewBilanOffre.Columns(6).Visible = True
            ViewBilanOffre.Columns("Score Financier (F)").Visible = True
        ElseIf MethodsTraites = True And TxtMethode.Text.ToUpper <> "SFQC" Then
            ViewBilanOffre.Columns("Score Financier (F)").Visible = False
            ViewBilanOffre.Columns(6).Visible = False
        ElseIf MethodsTraites = False Then
            'Cas de SFQ, SQC ******************
            ViewBilanOffre.Columns("Score Financier (F)").Visible = False
            ViewBilanOffre.Columns(6).Visible = False
            ViewBilanOffre.Columns("Rang").Visible = False
            ViewBilanOffre.Columns("Décision").Visible = False
        End If

        If ViewBilanOffre.RowCount > 0 Then
            BtOuvertureOffre.Text = "Etat PV d'ouverture" & vbNewLine & "des offres financières"
            'Active le bouton du rapport combinet et negociation
            BtRapportCombinet.Enabled = True
            'Cas de SFQC
            If TxtMethode.Text.ToUpper = "SFQC" Then
                TxtTypeExamen.Text = "SCORES PONDERES DES EVALUATIONS TECHNIQUES ET FINANCIERES"
            Else
                TxtTypeExamen.Text = "RESULTATS DES EVALUATIONS TECHNIQUES ET FINANCIERES"
            End If
        End If

        GridBilanOffreFinancier.Visible = True
        GridSaisieOffreFinance.Visible = False

        BtValiderEvalOffresFin.Enabled = False 'Desactive le bouton validation evaluation
        BtResultatEvalFin.Enabled = True 'Activer le bouton resultat
    End Sub

    Public Sub RemplirOffreFinanciere(Optional RefSoumis As String = "")
        Dim dtFinance As DataTable = New DataTable()
        dtFinance.Columns.Clear()
        dtFinance.Columns.Add("RefSoumis", Type.GetType("System.String"))
        dtFinance.Columns.Add("CodeX", Type.GetType("System.String"))
        dtFinance.Columns.Add("Consultant", Type.GetType("System.String"))
        dtFinance.Columns.Add("Offre financière", Type.GetType("System.String"))
        dtFinance.Columns.Add("Statut de l'offre", Type.GetType("System.String"))
        dtFinance.Columns.Add("Score financier", Type.GetType("System.String"))
        dtFinance.Rows.Clear()

        ''Requette a executer Cas de SFQC, SCBD, SMC ******************
        If RefSoumis.ToString = "" Then
            query = "select S.RefSoumis, C.NomConsult, S.MontantProposeDevise, S.MontantOffresLocal, S.MontantAjusterDevise, S.MontantAjusterLocal, S.ScoreFinancier, S.TauxJournalierLocal, S.Monnaie, S.NbreJrsTravail from T_Consultant as C,T_SoumissionConsultant as S where S.RefConsult=C.RefConsult and C.NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and S.EvalTechOk='OUI' order by C.NomConsult"
        Else
            ''Requette a executer Cas de SFQ * -- * SQC ******************
            query = "select S.RefSoumis, C.NomConsult, S.MontantProposeDevise, S.MontantOffresLocal, S.MontantAjusterDevise, S.MontantAjusterLocal, S.ScoreFinancier, S.TauxJournalierLocal, S.Monnaie, S.NbreJrsTravail from T_Consultant as C,T_SoumissionConsultant as S where S.RefConsult=C.RefConsult and C.NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and S.EvalTechOk='OUI' and S.RefSoumis='" & RefSoumis & "'"
        End If

        Dim dt As DataTable = ExcecuteSelectQuery(query)
        Dim cpt2 As Decimal = 0

        For Each rw As DataRow In dt.Rows
            cpt2 += 1
            Dim DrE = dtFinance.NewRow()
            DrE("CodeX") = IIf(CDec(cpt2 / 2) = CDec(cpt2 \ 2), "x", "")
            DrE("RefSoumis") = rw("RefSoumis").ToString
            DrE("Statut de l'offre") = IIf(rw("MontantOffresLocal").ToString = "", "...", IIf(rw("MontantAjusterLocal").ToString <> "", "Calculé", "A calculer").ToString).ToString
            DrE("Consultant") = MettreApost(rw("NomConsult").ToString)

            If rw("MontantOffresLocal").ToString <> "" Then
                If rw("MontantAjusterLocal").ToString <> "" Then
                    DrE("Offre financière") = AfficherMonnaie(rw("MontantAjusterLocal").ToString.Replace("?", "")) & " " & MonnaieEvaluation.ToString
                Else
                    DrE("Offre financière") = AfficherMonnaie(rw("MontantOffresLocal").ToString.Replace("?", "")) & " " & MonnaieEvaluation.ToString
                End If
            Else
                DrE("Offre financière") = "0"
            End If

            DrE("Score financier") = IIf(rw("ScoreFinancier").ToString <> "", rw("ScoreFinancier").ToString, "-").ToString
            dtFinance.Rows.Add(DrE)
        Next
        GridSaisieOffreFinance.DataSource = dtFinance

        ViewSaisiOffreFinance.Columns("RefSoumis").Visible = False
        ViewSaisiOffreFinance.Columns("CodeX").Visible = False
        ViewSaisiOffreFinance.Columns("Consultant").Width = GridSaisieOffreFinance.Width - 368
        ViewSaisiOffreFinance.Columns("Offre financière").Width = 230
        ViewSaisiOffreFinance.Columns("Statut de l'offre").Width = 100
        ViewSaisiOffreFinance.Columns("Score financier").Width = 150
        ViewSaisiOffreFinance.Columns("Offre financière").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewSaisiOffreFinance.Columns("Statut de l'offre").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewSaisiOffreFinance.Columns("Score financier").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

        'A mettre en forme Cas de SFQC
        ViewSaisiOffreFinance.Columns("Score financier").Visible = IIf(TxtMethode.Text.ToUpper <> "SFQC", False, True).ToString

        ColorRowGrid(ViewSaisiOffreFinance, "[CodeX]='x'", Color.LightGray, "Tahoma", 10, FontStyle.Regular, Color.Black)
        GridBilanOffreFinancier.Visible = False
        GridSaisieOffreFinance.Visible = True
        TxtTypeExamen.Text = "EVALUATION FINANCIERE (SAISIE DES OFFRES)"
    End Sub

    Private Sub BtResultatSaisiOffre_Click(sender As Object, e As EventArgs) Handles BtResultatSaisiOffre.Click
        Try
            DebutChargement(True, "Chargement des offres en cours...")
            If GroupBoxOuvertureOffres.Visible = True Then
                'Requette a executer Cas de SFQ * -- * SQC ******************
                RemplirOffreFinanciere(rwDossOffreFin("RefSoumis"))
            Else
                'Requette a executer Cas de SFQC, SCBD, SMC ******************
                RemplirOffreFinanciere()
            End If
            PersonaliserTexte()
            GetVisiblePanel(True, "OffreFinance")
            FinChargement()
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub BtResultatEvalFin_Click(sender As Object, e As EventArgs) Handles BtResultatEvalFin.Click
        Try
            DebutChargement(True, "Chargement des resultats de l'evaluation financière...")
            If GroupBoxOuvertureOffres.Visible = True Then
                'Requette a executer Cas de SFQ * --- * SQC ******************
                Dim RefSoumis As String = ""
                If CmbNumOuvertureOffre.SelectedIndex <> -1 Then
                    RefSoumis = CInt(CmbNumOuvertureOffre.Text.Split("_")(0))
                Else
                    RefSoumis = rwDossOffreFin("RefSoumis")
                End If
                RemplirBilanEvalConsult(RefSoumis)
            Else
                'Requette a executer Cas de SFQC, SCBD, SMC ******************
                RemplirBilanEvalConsult()
                GetVisiblePanel(True, "OffreFinance")
            End If
            FinChargement()
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub CmbNumOuvertureOffre_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbNumOuvertureOffre.SelectedIndexChanged
        Try
            If GroupBoxOuvertureOffres.Visible = True Then
                If CmbNumOuvertureOffre.SelectedIndex <> -1 Then
                    ''Requette a executer Cas de SFQ * --- * SQC ******************
                    DebutChargement()
                    rwDossOffreFin = ExcecuteSelectQuery("select * from t_soumissionconsultant where RefSoumis='" & CInt(CmbNumOuvertureOffre.Text.Split("_")(0)) & "' and NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'").Rows(0)
                    RemplirOffreFinanciere(rwDossOffreFin("RefSoumis"))
                    PersonaliserTexte()
                    GetVisiblePanel(True, "OffreFinance")
                    FinChargement()
                End If
            End If

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub NouvelOuvertureOffres_Click(sender As Object, e As EventArgs) Handles NouvelOuvertureOffres.Click
        Try
            If GroupBoxOuvertureOffres.Visible = True Then
                Dim dt As DataTable = ExcecuteSelectQuery("select S.*, C.NomConsult from T_SoumissionConsultant as S, t_consultant as C where S.RefConsult=C.RefConsult and S.NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and S.EvalTechOk='OUI' and S.RangConsult IS NOT NULL ORDER BY S.RangConsult ASC")
                If dt.Rows.Count > 0 Then
                    Dim NbrsDisqualifier As Integer = 0
                    Dim Cpte As Integer = 0
                    For Each rw In dt.Rows
                        'Verifier si l'ouverture de l'offre est dejà effectué
                        If IsDBNull(rw("DateOuvertureEvalFin")) Then
                            rwDossOffreFin = dt.Rows(Cpte)
                            Exit For
                        End If
                        Cpte += 1

                        'Verifier si l'ouverture est termié et l'on a valider l'evaluation financière
                        If IsDBNull(rw("DateFinOuvertEvalFin")) Or IsDBNull(rw("FinEvalFinanciere")) Then 'Une evaluation est en cours
                            SuccesMsg("Veuillez terminer l'évaluation en cours")
                            Exit Sub
                        End If

                        'Verifier si le soumissionnaire est disqualifier
                        If IsDBNull(rw("MotifDisqualification")) Then
                            ReponseDialog = ""
                            Dim NewMotifDias As New MotifDisqualification
                            NewMotifDias.TxtNomConslt.Text = MettreApost(rw("NomConsult").ToString)
                            NewMotifDias.ShowDialog()
                            If ReponseDialog.ToString = "" Then
                                Exit Sub
                            End If

                            ExecuteNonQuery("update t_soumissionconsultant set ConsultDisqualifie='OUI', MotifDisqualification='" & EnleverApost(ReponseDialog.ToString) & "' where RefSoumis='" & rw("RefSoumis") & "' and NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'")
                            SuccesMsg("Disqualification effectuée avec succès")
                            NbrsDisqualifier += 1
                        Else
                            'Consultant disqualifier
                            NbrsDisqualifier += 1
                        End If
                    Next

                    'Tous les consultant de la liste restriente ont été disqualifier
                    If NbrsDisqualifier = dt.Rows.Count Then
                        FailMsg("Tous les consultants retenus après l'évaluation" & vbNewLine & "technique pour ce dossier sont disqualifiés")
                        Exit Sub
                    End If

                    CmbNumOuvertureOffre.Text = ""
                    RemplirOffreFinanciere(rwDossOffreFin("RefSoumis"))
                    PersonaliserTexte()
                Else
                    FailMsg("Impossible de faire une ouverture car" & vbNewLine & "aucun consultant n'est retenu sur cette DP")
                End If
            End If
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub


    Private Sub BtValiderEvalOffresFin_Click(sender As Object, e As EventArgs) Handles BtValiderEvalOffresFin.Click
        Try
            ''Requette a executer Cas de SFQ * --- * SQC ******************
            Dim RefSoumi As String = ""
            If GroupBoxOuvertureOffres.Visible = True Then
                If CmbNumOuvertureOffre.SelectedIndex <> -1 Then
                    RefSoumi = CInt(CmbNumOuvertureOffre.Text.Split("_")(0))
                Else
                    RefSoumi = rwDossOffreFin("RefSoumis")
                End If
                query = "SELECT COUNT(*) FROM t_soumissionconsultant WHERE RefSoumis='" & RefSoumi & "' and NumeroDp ='" & EnleverApost(CmbNumDoss.Text) & "' and EvalTechOk='OUI' AND MontantAjusterLocal IS NULL"
            Else
                'Requette a executer Cas de SFQC, SCBD, SMC ******************
                query = "SELECT COUNT(*) FROM t_soumissionconsultant WHERE NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and EvalTechOk='OUI' AND MontantAjusterLocal IS NULL"
            End If

            If Val(ExecuteScallar(query)) > 0 Then
                SuccesMsg("Veuillez calculer toutes les offres financières des consultants")
                Exit Sub
            End If

            If ConfirmMsg("Confirmez-vous la clôture de l'évaluation financière ?") = DialogResult.Yes Then
                DebutChargement(True, "Traitement des offres en cours...")

                If GroupBoxOuvertureOffres.Visible = True Then
                    If CalculerScoreFinancier(RefSoumi) = True Then
                        ExecuteNonQuery("Update t_soumissionconsultant set FinEvalFinanciere='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' where  RefSoumis='" & RefSoumi & "'")
                        RemplirBilanEvalConsult(RefSoumi)

                        Dim NomConsulat As String = ExecuteScallar("select C.NomConsult from T_SoumissionConsultant as S, t_consultant as C where S.RefConsult=C.RefConsult and S.NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and S.EvalTechOk='OUI' AND S.RefSoumis='" & RefSoumi & "'")
                        'Ajout dans la liste de selection des generation des rapport combines
                        CombRapport.Properties.Items.Add(GetNewCode(RefSoumi) & " | " & EnleverApost(NomConsulat.ToString))
                    End If
                ElseIf CalculerScoreFinancier() = True Then
                    ExecuteNonQuery("Update t_soumissionconsultant set FinEvalFinanciere='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' where  NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' AND EvalTechOk='OUI'")
                    RemplirBilanEvalConsult()
                End If
                rwDossOffreFin("FinEvalFinanciere") = dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString
                BtRapportCombinet.Enabled = True
                EtapeFinanciere.ImageIndex = 0
                EtapeFinanciere.ForeColor = Color.Black
                FinChargement()
            End If
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub BtOuvertureOffre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtOuvertureOffre.Click
        If ViewSaisiOffreFinance.RowCount > 0 Or ViewBilanOffre.RowCount > 0 Then
            Try
                'Update Ouverture des offres financières
                If Mid(BtOuvertureOffre.Text, 1, 3) = "Dém" Then
                    If rwDossOffreFin("DateOuvertureEvalFin").ToString = "" Then
                        Dim DateOuverture As String = dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString

                        If GroupBoxOuvertureOffres.Visible = True Then 'Cas des methodes SFQ * --- * SQC ******************
                            Dim RefOuvr As String = DateOuverture.ToString.Replace("-", "").Replace(" ", "").Replace(":", "")
                            RefOuvr = GetNewCode(rwDossOffreFin("RefSoumis")) & "_" & RefOuvr.ToString

                            ExecuteNonQuery("Update t_soumissionconsultant set RefOuverture='" & RefOuvr.ToString & "', DateOuvertureEvalFin='" & DateOuverture & "' Where RefSoumis='" & rwDossOffreFin("RefSoumis") & "'")
                            'Ajouter dans la CmbNumOuvertureOffre
                            CmbNumOuvertureOffre.Properties.Items.Add(RefOuvr.ToString)
                        Else
                            ExecuteNonQuery("Update t_soumissionconsultant set DateOuvertureEvalFin='" & DateOuverture & "' Where NumeroDp ='" & EnleverApost(CmbNumDoss.Text) & "' AND EvalTechOk='OUI'")
                        End If
                        rwDossOffreFin("DateOuvertureEvalFin") = DateOuverture
                    End If

                    BtOuvertureOffre.Text = "Fin ouverture" & vbNewLine & "des offres financières"
                ElseIf Mid(BtOuvertureOffre.Text, 1, 3) = "Fin" Then

                    'Cas des methodes SFQ * --- * SQC ******************  
                    If GroupBoxOuvertureOffres.Visible = True Then 'Cas des methodes SFQ * --- * SQC ******************  
                        query = "SELECT COUNT(*) FROM t_soumissionconsultant WHERE RefSoumis='" & rwDossOffreFin("RefSoumis") & "' and NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and EvalTechOk='OUI' AND MontantOffresLocal IS NULL"
                    Else
                        query = "SELECT COUNT(*) FROM t_soumissionconsultant WHERE NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and EvalTechOk='OUI' AND MontantOffresLocal IS NULL"
                    End If

                    If Val(ExecuteScallar(query)) > 0 Then
                        SuccesMsg("Il existe des offres financières à ouvrir")
                        Exit Sub
                    End If

                    If ConfirmMsg("Confirmez-vous la clôture de l'ouverture" & vbNewLine & "des offres financières ?") = DialogResult.Yes Then

                        'Cas des methodes SFQ * --- * SQC ****************** 
                        If GroupBoxOuvertureOffres.Visible = True Then 'Cas des methodes SFQ * -- * SQC ******************  
                            ExecuteNonQuery("Update t_soumissionconsultant set DateFinOuvertEvalFin='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' where RefSoumis='" & rwDossOffreFin("RefSoumis") & "' and NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'")
                        Else
                            ExecuteNonQuery("Update t_soumissionconsultant set DateFinOuvertEvalFin='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' where  NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' AND EvalTechOk='OUI'")
                        End If
                        rwDossOffreFin("DateFinOuvertEvalFin") = dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString

                        SuccesMsg("Ouverture des offres financières terminées")
                        BtOuvertureOffre.Text = "Etat PV d'ouverture" & vbNewLine & "des offres financières"
                        BtValiderEvalOffresFin.Enabled = True
                    End If
                ElseIf Mid(BtOuvertureOffre.Text, 1, 3) = "Eta" Then

                    Try
                        DebutChargement(True, "Chargement du Pv d'ouverture en cours...")

                        Dim RapportPVOffres As New ReportDocument
                        Dim crtableLogoninfos As New TableLogOnInfos
                        Dim crtableLogoninfo As New TableLogOnInfo
                        Dim crConnectionInfo As New ConnectionInfo
                        Dim CrTables As Tables
                        Dim CrTable As Table

                        Dim DatSet = New DataSet

                        Dim Chemin As String = lineEtat & "\\Marches\DP\PV Ouverture Offres financieres\"
                        'Cas des methodes SFQ * -- * SQC ****************** 
                        If GroupBoxOuvertureOffres.Visible = True Then
                            RapportPVOffres.Load(Chemin & "PvOuverture_OffresFinanciere_3CV_SFQ_SQC.rpt")
                        Else
                            RapportPVOffres.Load(Chemin & "PvOuverture_OffresFinanciere_SFQC_SCBD_SMC.rpt")
                        End If

                        With crConnectionInfo
                            .ServerName = ODBCNAME
                            .DatabaseName = DB
                            .UserID = USERNAME
                            .Password = PWD
                        End With

                        CrTables = RapportPVOffres.Database.Tables
                        For Each CrTable In CrTables
                            crtableLogoninfo = CrTable.LogOnInfo
                            crtableLogoninfo.ConnectionInfo = crConnectionInfo
                            CrTable.ApplyLogOnInfo(crtableLogoninfo)
                        Next

                        RapportPVOffres.SetDataSource(DatSet)

                        Dim RefSoumis As String = ""
                        Dim DateOuverture As String = ""
                        Dim DateFinOuverture As String = ""
                        Dim NbConsultRetenu As Integer = 0

                        'Cas des methodes SFQ * --- * SQC ******************  
                        If GroupBoxOuvertureOffres.Visible = True Then
                            If CmbNumOuvertureOffre.SelectedIndex <> -1 Then
                                RefSoumis = CInt(CmbNumOuvertureOffre.Text.Split("_")(0))
                            Else
                                RefSoumis = rwDossOffreFin("RefSoumis")
                            End If
                            query = "select DateOuvertureEvalFin, DateFinOuvertEvalFin from t_soumissionconsultant where RefSoumis='" & RefSoumis & "' and NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'"
                            RapportPVOffres.SetParameterValue("RefSoumis", RefSoumis)
                            NbConsultRetenu = 1
                        Else
                            query = "select DateOuvertureEvalFin, DateFinOuvertEvalFin from t_soumissionconsultant where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and EvalTechOk='OUI' and MontantOffresLocal IS NOT NULL LIMIT 1"
                            NbConsultRetenu = Val(ExecuteScallar("SELECT COUNT(*) FROM t_soumissionconsultant WHERE NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' AND EvalTechOk='OUI' and MontantOffresLocal IS NOT NULL"))
                        End If

                        dt = ExcecuteSelectQuery(query)
                        For Each rw0 In dt.Rows
                            DateOuverture = rw0("DateOuvertureEvalFin")
                            DateFinOuverture = rw0("DateFinOuvertEvalFin")
                        Next

                        Dim NbConsultDepo As Integer = Val(ExecuteScallar("SELECT COUNT(*) FROM t_consultant WHERE NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' AND DateDepot IS NOT NULL"))

                        RapportPVOffres.SetParameterValue("CodeProjet", ProjetEnCours)
                        RapportPVOffres.SetParameterValue("NumDp", EnleverApost(CmbNumDoss.Text))
                        RapportPVOffres.SetParameterValue("AnneeEnLettre", MontantLettre(CDate(DateOuverture.ToString).Year))
                        RapportPVOffres.SetParameterValue("NbConsulDepotLettre", MontantLettre(NbConsultDepo))
                        RapportPVOffres.SetParameterValue("NbConsultDepot", NbConsultDepo)
                        RapportPVOffres.SetParameterValue("NbConsultRetenuLettre", MontantLettre(NbConsultRetenu))
                        RapportPVOffres.SetParameterValue("NbConsultRetenu", NbConsultRetenu)
                        RapportPVOffres.SetParameterValue("DateFormatLong", Now.ToShortDateString & " " & Now.ToShortTimeString)
                        RapportPVOffres.SetParameterValue("DateEdition", Now.ToShortDateString, "PvOuverturePageGarde.rpt")
                        RapportPVOffres.SetParameterValue("DateOuverture", DateOuverture, "PvOuverturePageGarde.rpt")
                        RapportPVOffres.SetParameterValue("DateOuverture", DateOuverture)
                        RapportPVOffres.SetParameterValue("DateFinOuverture", DateFinOuverture)

                        FinChargement()

                        With FullScreenReport
                            .FullView.ReportSource = RapportPVOffres
                            .Text = "PV D'OUVERTURE DES OFFRES FINANCIERES DU DOSSIER N°" & EnleverApost(CmbNumDoss.Text)
                            .ShowDialog()
                        End With
                    Catch ex As Exception
                        FinChargement()
                        FailMsg(ex.ToString)
                    End Try
                End If
            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub

    Public Function CalculerScoreFinancier(Optional RefSoumis As String = "") As Boolean
        Try
            'query = "select count(*) from T_SoumissionConsultant where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and EvalTechOk='OUI' and MontantAjusterLocal IS NULL"
            'If Val(ExecuteScallar(query)) > 0 Then
            '    Return False
            'End If

            'Cas des methodes SFQC, SCBD, SMC ******************
            If RefSoumis.ToString = "" Then

                'Cas de la methode SFQC ********** Classement **************************
                If TxtMethode.Text.ToUpper = "SFQC" Then

                    'Recup des infos
                    Dim LesScore(20) As String
                    Dim LesRef(20) As String
                    Dim LesMont(20) As Decimal
                    Dim LesNote(20) As Decimal
                    Dim LesMoyPond(20) As Decimal
                    Dim Tamp As String = ""
                    Dim TampDec As Decimal = 0

                    query = "select RefSoumis, MontantAjusterLocal, NoteConsult from T_SoumissionConsultant where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and EvalTechOk='OUI'"
                    dt = ExcecuteSelectQuery(query)

                    Dim NbRef As Decimal = 0
                    For Each rw As DataRow In dt.Rows
                        LesRef(NbRef) = rw("RefSoumis").ToString
                        LesMont(NbRef) = CDec(rw("MontantAjusterLocal"))
                        LesNote(NbRef) = CDec(rw("NoteConsult").ToString.Replace(".", ","))
                        NbRef += 1
                    Next

                    'Les coefficients
                    Dim CoefTech As Decimal = 1
                    Dim CoefFin As Decimal = 1

                    query = "select PoidsTech,PoidsFin from T_DP where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
                    dt = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        CoefTech = CDec(rw("PoidsTech"))
                        CoefFin = CDec(rw("PoidsFin"))
                    Next

                    'recherche du min
                    Dim MontMin As Decimal = LesMont(0)
                    For k As Integer = 1 To NbRef - 1
                        If (MontMin > LesMont(k)) Then
                            MontMin = LesMont(k)
                        End If
                    Next

                    'Calcul des scores
                    For k As Integer = 0 To NbRef - 1
                        LesScore(k) = Math.Round((100 * MontMin) / LesMont(k), 2).ToString
                    Next

                    'Calcul moy ponderee
                    For k As Integer = 0 To NbRef - 1
                        LesMoyPond(k) = ((LesNote(k) * CoefTech) + (CDec(LesScore(k)) * CoefFin)) / (CoefFin + CoefTech)
                    Next

                    'Classement
                    For i As Integer = 0 To NbRef - 2
                        For j As Integer = i + 1 To NbRef - 1
                            If (LesMoyPond(i) < LesMoyPond(j)) Then
                                Tamp = LesRef(i)
                                LesRef(i) = LesRef(j)
                                LesRef(j) = Tamp

                                Tamp = LesScore(i)
                                LesScore(i) = LesScore(j)
                                LesScore(j) = Tamp

                                TampDec = LesMoyPond(i)
                                LesMoyPond(i) = LesMoyPond(j)
                                LesMoyPond(j) = TampDec
                            End If
                        Next
                    Next

                    'MAJ
                    For k As Integer = 0 To NbRef - 1
                        ExecuteNonQuery("Update T_SoumissionConsultant set ScoreFinancier='" & LesScore(k).Replace(",00", "") & "', MoyPonderee='" & Math.Round(LesMoyPond(k), 2).ToString.Replace(",00", "") & "', RangFinal='" & (k + 1).ToString & "', EvalFinOk='OUI' where RefSoumis='" & LesRef(k) & "'")
                    Next

                    'Cas de la methode ***  SMC ******************** Classements *****************
                    'Classé du Min au Maxi des montants
                ElseIf TxtMethode.Text.ToUpper = "SMC" Then
                    query = "select RefSoumis, MontantAjusterLocal from T_SoumissionConsultant where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and EvalTechOk='OUI' ORDER BY MontantAjusterLocal ASC"
                    dt = ExcecuteSelectQuery(query)
                    Dim RangCon As Decimal = 0
                    For Each rw In dt.Rows
                        RangCon += 1
                        ExecuteNonQuery("Update T_SoumissionConsultant set RangFinal='" & RangCon.ToString & "', EvalFinOk='OUI' where RefSoumis='" & rw("RefSoumis").ToString & "'")
                    Next

                    'Cas de la methode SCBD *********** 
                    'Seul ceux qui entre dans le budgé sont retenu
                ElseIf TxtMethode.Text.ToUpper = "SCBD" Then
                    Dim dt As DataTable = ExcecuteSelectQuery("Select RefSoumis, MontantAjusterLocal, RangConsult from T_SoumissionConsultant where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and EvalTechOk='OUI' ORDER BY RangConsult ASC")
                    Dim MontantMarche As Decimal = CDec(ExecuteScallar("select MontantMarche from t_dp where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'"))
                    Dim ListFinal(20, 1) As String
                    Dim ListNonRetenu(20, 1) As String
                    Dim NbrLigne As Integer = 0
                    Dim Nbrs As Integer = 0

                    For Each rw In dt.Rows
                        If CDec(rw("MontantAjusterLocal")) <= MontantMarche Then
                            'Consultant retenu
                            ListFinal(NbrLigne, 0) = rw("RefSoumis")
                            ListFinal(NbrLigne, 1) = "OUI"
                            NbrLigne += 1
                        Else
                            'Consultant non retenu
                            ListNonRetenu(Nbrs, 0) = rw("RefSoumis")
                            ListNonRetenu(Nbrs, 1) = "NON"
                            Nbrs += 1
                        End If
                    Next

                    If Nbrs > 0 Then
                        For i = 0 To Nbrs - 1
                            'Ramener les consultants non retenu dans la liste fianle
                            ListFinal(NbrLigne, 0) = ListNonRetenu(i, 0)
                            ListFinal(NbrLigne, 1) = ListNonRetenu(i, 1)
                            NbrLigne += 1
                        Next
                    End If

                    'MJRS
                    Dim RangCon As Decimal = 0
                    For j = 0 To NbrLigne - 1
                        RangCon += 1
                        ExecuteNonQuery("Update T_SoumissionConsultant set RangFinal='" & RangCon.ToString & "', EvalFinOk='" & ListFinal(j, 1).ToString & "' where RefSoumis='" & ListFinal(j, 0) & "'")
                    Next
                End If

                'Cas des methodes ---, SQC, SFQ  '*****************************************************************************
                'Validation et classement evaluation financières 
                'Seul l'offre fiancières du premier consultant après l'évaluation technique est ouvert.
            Else
                ' Dim RangConsult As String = ExecuteScallar("SELECT RangConsult from t_soumissionconsultant where RefSoumis='" & RefSoumis & "' and EvalTechOk='OUI'")
                ExecuteNonQuery("UPDATE t_soumissionconsultant set RangFinal=RangConsult, EvalFinOk='OUI' where RefSoumis='" & RefSoumis & "'")
            End If
            Return True
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Function

#End Region

#Region "Rapport combine"

    Private Sub EnebledBoutonRapCombine()
        If rwDossRapCombine("EtatRapportCombine").ToString = "Valider" Then
            modifierrc.Enabled = False
            Actualiserrc.Enabled = False
            Envoibailleurrc.Enabled = False
            Validerrc.Enabled = False
            Rejeterrc.Enabled = False
            DateSoumiRC.Enabled = False
            DateReponRC.Enabled = False
            pdfrc.Enabled = True
            Wordrc.Enabled = True
        Else
            modifierrc.Enabled = True
            Actualiserrc.Enabled = True
            Envoibailleurrc.Enabled = True
            Validerrc.Enabled = True
            Rejeterrc.Enabled = True
            DateSoumiRC.Enabled = True
            DateReponRC.Enabled = True
            pdfrc.Enabled = True
            Wordrc.Enabled = True
        End If
    End Sub

    'Private Sub RemplirNumRapCombine()
    '    ' CombRapport.Text = ""
    '    'CombRapport.Properties.Items.Clear()
    '    'Dim dt As DataTable = ExcecuteSelectQuery("select NumRapportCombine from t_soumissionconsultant where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' AND EvalTechOk='OUI' and NumRapportCombine IS NOT NULL")
    '    'For Each rw In dt.Rows
    '    '    CombRapport.Properties.Items.Add(rw("NumRapportCombine").ToString)
    '    'Next
    'End Sub

    Private Sub RemplirListeRapport()
        Try
            Dim dtDoss As DataTable = ExcecuteSelectQuery("select S.RefSoumis, C.NomConsult from T_SoumissionConsultant as S, t_consultant as C where S.RefConsult=C.RefConsult and S.NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and S.EvalTechOk='OUI' AND S.FinEvalFinanciere IS NOT NULL and S.RangFinal IS NOT NULL ORDER BY S.RangConsult ASC")
            CombRapport.Text = ""
            CombRapport.Properties.Items.Clear()
            For Each rw In dtDoss.Rows
                CombRapport.Properties.Items.Add(GetNewCode(rw("RefSoumis")) & " | " & EnleverApost(rw("NomConsult").ToString))
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub BtRapportCombinet_Click(sender As Object, e As EventArgs) Handles BtRapportCombinet.Click

        Try
            If TablBoutonClik(2) = False Then 'Verifier si le bouton à été cliquer

                If VerifiersMetohd(TxtMethode.Text) = True Then

                    Dim dtDoss As DataTable = ExcecuteSelectQuery("select * from T_SoumissionConsultant where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and EvalTechOk='OUI' and FinEvalFinanciere IS NOT NULL AND RangFinal IS NOT NULL ORDER BY RangConsult ASC")
                    If dtDoss.Rows.Count > 0 Then rwDossRapCombine = dtDoss.Rows(0)

                    DateSoumiRC.Text = rwDossRapCombine("DateEnvoiRapComb").ToString
                    DateReponRC.Text = rwDossRapCombine("DateRepoRapComb").ToString
                    DateSoumiRC.Enabled = True
                    DateReponRC.Enabled = True

                    Dim CheminDoss As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\Rapport_Combine\"

                    If rwDossRapCombine("CheminRapportCombine").ToString = "" Then
                        'Gereartion du rapport *** on arrete en cas d'eurreur
                        If GenererLoadRapCombinet() = False Then Exit Sub
                    ElseIf Not File.Exists(CheminDoss & rwDossRapCombine("CheminRapportCombine").ToString) Then
                        If ConfirmMsg("Le rapport n'existe pas ou a été supprimer" & vbNewLine & "Voulez-vous le généré à nouveau ?") = DialogResult.Yes Then
                            If GenererLoadRapCombinet() = False Then Exit Sub
                        End If
                    ElseIf (File.Exists(CheminDoss & rwDossRapCombine("CheminRapportCombine").ToString)) Then
                        DebutChargement(True, "Chargement du rapport combiné en cours...")
                        WebBrowser1.Navigate(CheminDoss & rwDossRapCombine("CheminRapportCombine").ToString)
                        Threading.Thread.Sleep(5000)
                        FinChargement()
                    End If

                    GroupBoxRapportCombine.Visible = False
                    PanelControl11.Size = New Point(974, 68)
                    EnebledBoutonRapCombine() 'Activer ou desactiver les boutons

                Else 'cas de SQC ET SFQ
                    PanelControl11.Size = New Point(974, 118)
                    GroupBoxRapportCombine.Visible = True
                    RemplirListeRapport()
                    DateSoumiRC.Enabled = False
                    DateReponRC.Enabled = False
                End If

                TablBoutonClik(2) = True
            End If

            GetVisiblePanel(True, "Combine")
        Catch ex As Exception
            FailMsg(ex.ToString)
            FinChargement()
        End Try
    End Sub

    Private Function GenererLoadRapCombinet() As Boolean
        Try
            DebutChargement(True, "Génération du rapport combiné en cours...")

            '***** Info du rapport d'evaluation technique *********************
            ExecuteNonQuery("delete from t_tamp_consultrangtech")
            ExecuteNonQuery("delete from t_noteconsultparcritere")
            'Enregistrement des noms et des rangs
            EnregistreNometRangCons()
            'Enregistrement des notes et des moyennes
            Dim NbrConsult As Decimal = EnregistreMoyenneetNote()

            '****   rapport combine
            ExecuteNonQuery("delete from t_noteevalparcritereperscle")
            ExecuteNonQuery("delete from t_noteevalparconsult")
            ExecuteNonQuery("delete from t_tampevalnom")

            'Afficharge de l'etat
            Dim NumDoss As String = EnleverApost(CmbNumDoss.Text)
            Dim TotalPtsCriterePersCle As Decimal = 0
            ' ***** Noms des membres de la commission
            Dim NomCojo As DataTable = EnregistrerNomCojo(NumDoss)
            Dim TableEval As DataTable = ExcecuteSelectQuery("SELECT RefSoumis, CodeMem, RefCritere, NoteConsult from t_soumi_note_consultant_parcriteresdp where NumeroDp='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'")
            Dim TableSoumi As DataTable = ExcecuteSelectQuery("SELECT RefSoumis, RangConsult, RangFinal from t_soumissionconsultant where NumeroDp='" & NumDoss & "' and EvalTechOk='OUI' and RangFinal IS NOT NULL")
            'Dim TableSoumi As DataTable = ExcecuteSelectQuery("SELECT RefSoumis, RangConsult, RangFinal from t_soumissionconsultant where NumeroDp='" & NumDoss & "' and EvalFinOk='OUI' and RangFinal IS NOT NULL")
            ' ******* t_noteevalparconsult *************  t_noteevalparcritereperscle ************
            ' Enregistrement des differentes notes

            Dim dt0 As DataTable = ExcecuteSelectQuery("select RefCritere, CriterePersonnelCle,PointCritere from T_DP_CritereEval where NumeroDp='" & NumDoss & "' and CritereParent='0' and CodeProjet='" & ProjetEnCours & "'")
            ' 1eme niveau ********************************
            For Each rw As DataRow In dt0.Rows
                SaveDifferenteNoteEval(TableSoumi, NomCojo, rw("RefCritere"), TableEval)
                If rw("CriterePersonnelCle").ToString = "OUI" Then SaveDifferenteNoteEval1(TableSoumi, NomCojo, rw("RefCritere"), TableEval)
                If rw("CriterePersonnelCle").ToString = "OUI" Then TotalPtsCriterePersCle += CDbl(rw("PointCritere").ToString.Replace(".", ",").Replace(" ", ""))

                ' 2eme niveau ********************************
                Dim dt1 As DataTable = ExcecuteSelectQuery("Select RefCritere from T_DP_CritereEval where NumeroDp='" & NumDoss & "' and CritereParent='" & rw("RefCritere").ToString & "' and TypeCritere<>'Bareme' And CodeProjet='" & ProjetEnCours & "'")
                For Each rw1 As DataRow In dt1.Rows
                    SaveDifferenteNoteEval(TableSoumi, NomCojo, rw1("RefCritere"), TableEval)
                    If rw("CriterePersonnelCle").ToString = "OUI" Then SaveDifferenteNoteEval1(TableSoumi, NomCojo, rw1("RefCritere"), TableEval)

                    ' 3eme niveau **************************************
                    Dim dt2 As DataTable = ExcecuteSelectQuery("Select RefCritere from T_DP_CritereEval where NumeroDp='" & NumDoss & "' and CritereParent='" & rw1("RefCritere").ToString & "' and TypeCritere<>'Bareme' And CodeProjet='" & ProjetEnCours & "'")
                    For Each rw2 As DataRow In dt2.Rows
                        SaveDifferenteNoteEval(TableSoumi, NomCojo, rw2("RefCritere"), TableEval)
                        If rw("CriterePersonnelCle").ToString = "OUI" Then SaveDifferenteNoteEval1(TableSoumi, NomCojo, rw2("RefCritere"), TableEval)
                    Next
                Next
            Next

            Dim RapportsCombine As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table

            Dim DatSet = New DataSet

            RapportsCombine.Load(lineEtat & "\Marches\DP\Rapport Evaluation Consultant\RapportConsolide.rpt")

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = RapportsCombine.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            RapportsCombine.SetDataSource(DatSet)

            '***** Paramettre evaluation technique *****************
            RapportsCombine.SetParameterValue("CodeProjet", ProjetEnCours)
            RapportsCombine.SetParameterValue("NumDP", NumDoss)
            Dim ScoreTechMin As Decimal = Val(ExecuteScallar("select ScoreTechMin from t_dp where NumeroDp='" & NumDoss & "'"))
            RapportsCombine.SetParameterValue("ScoreTechMin", ScoreTechMin.ToString)

            '***** Paramettre rapport combine
            Dim NbrePropoRecu As Decimal = Val(ExecuteScallar("SELECT COUNT(*) from t_consultant where NumeroDp='" & NumDoss & "' and DateDepot<>''"))
            Dim NbreConsultantRetenu As Decimal = Val(ExecuteScallar("SELECT COUNT(*) from t_consultant where NumeroDp='" & NumDoss & "'"))

            RapportsCombine.SetParameterValue("NbreEvaluateur", NomCojo.Rows.Count)
            RapportsCombine.SetParameterValue("NbreConsultant", NbrConsult)
            RapportsCombine.SetParameterValue("NbrePropositionRecues", NbrePropoRecu)
            RapportsCombine.SetParameterValue("NbreConsultantRetenu", NbreConsultantRetenu)
            RapportsCombine.SetParameterValue("ModifRangParMontant", VerifierModifRang(TableSoumi))
            RapportsCombine.SetParameterValue("TotalPtsCriterePersCle", AfficherMonnaie(TotalPtsCriterePersCle))

            RapportsCombine.SetParameterValue("RefSoumis", rwDossRapCombine("RefSoumis"))
            RapportsCombine.SetParameterValue("DateSoumRapCombFinal", rwDossRapCombine("DateEnvoiRapComb").ToString)
            RapportsCombine.SetParameterValue("DateAvisNOBanque", rwDossRapCombine("DateRepoRapComb").ToString)
            RapportsCombine.SetParameterValue("DateOuvPropoFinanciere", CDate(rwDossRapCombine("DateOuvertureEvalFin").ToString).ToShortDateString)
            RapportsCombine.SetParameterValue("DateOuvPropoFinHeure", CDate(rwDossRapCombine("DateOuvertureEvalFin").ToString).ToLongTimeString)

            Dim CheminDoss = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\Rapport_Combine"
            If (Directory.Exists(CheminDoss) = False) Then
                Directory.CreateDirectory(CheminDoss)
            End If

            Dim NomRapportword As String = "RapportCombine.doc"
            Dim NomRapportpdf As String = "RapportCombine_" & FormatFileName(Now.ToString.Replace(" ", ""), "") & ".pdf"

            'Cas des methodes 3CV SFQ SQC
            Dim TraitMethod As Boolean = VerifiersMetohd(TxtMethode.Text)
            If TraitMethod = False Then
                NomRapportword = rwDossRapCombine("RefSoumis").ToString & "_RapportCombine\" & NomRapportword.ToString
                NomRapportpdf = rwDossRapCombine("RefSoumis").ToString & "_RapportCombine\" & NomRapportpdf.ToString

                If (Directory.Exists(CheminDoss & "\" & rwDossRapCombine("RefSoumis").ToString & "_RapportCombine") = False) Then
                    Directory.CreateDirectory(CheminDoss & "\" & rwDossRapCombine("RefSoumis").ToString & "_RapportCombine")
                End If
            End If

            RapportsCombine.ExportToDisk(ExportFormatType.WordForWindows, CheminDoss & "\" & NomRapportword.ToString)
            RapportsCombine.ExportToDisk(ExportFormatType.PortableDocFormat, CheminDoss & "\" & NomRapportpdf.ToString)
            rwDossRapCombine("CheminRapportCombine") = NomRapportpdf.ToString

            If TraitMethod = False Then
                'CombRapport.Text = ""
                Dim NumRapport As String = GetNewCode(rwDossRapCombine("RefSoumis").ToString) & "_" & FormatFileName(Now.ToString.Replace(" ", ""), "")
                ExecuteNonQuery("Update t_soumissionconsultant set NumRapportCombine='" & NumRapport.ToString & "', CheminRapportCombine='" & rwDossRapCombine("CheminRapportCombine").ToString.Replace("\", "\\") & "' where RefSoumis='" & rwDossRapCombine("RefSoumis") & "'")
                'RemplirNumRapCombine()
            Else
                ExecuteNonQuery("Update t_soumissionconsultant set CheminRapportCombine='" & rwDossRapCombine("CheminRapportCombine").ToString.Replace("\", "\\") & "' where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and EvalTechOk='OUI' and RangFinal IS NOT NULL")
            End If
            FinChargement()

            DebutChargement(True, "Chargement du rapport combine en cours...")
            WebBrowser1.Navigate(CheminDoss & "\" & NomRapportpdf.ToString)
            Threading.Thread.Sleep(5000)
            FinChargement()

        Catch exs As IO.IOException
            FinChargement()
            SuccesMsg("Un exemplaire du rapport est ouvert dans une autre application" & vbNewLine & "Veuillez le fermer svp")
            Return False
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
            Return False
        End Try
        Return True
    End Function

    Private Function VerifierModifRang(ByVal TableRang As DataTable) As Boolean
        Try
            For Each rw In TableRang.Rows
                If rw("RangConsult") <> rw("RangFinal") Then
                    Return True
                End If
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        Return False
    End Function

    Private Sub SaveDifferenteNoteEval1(ByVal TableSoumi As DataTable, ByVal TableCojo As DataTable, ByVal RefCritere As Decimal, ByVal TableEval As DataTable)
        Try
            Dim Nbre As Integer = 0
            Dim DatSet = New DataSet
            query = "select * from t_noteevalparcritereperscle"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)

            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "t_noteevalparcritereperscle")
            Dim DatTable = DatSet.Tables("t_noteevalparcritereperscle")

            For Each rWs In TableSoumi.Rows
                Dim DatRow = DatSet.Tables("t_noteevalparcritereperscle").NewRow()
                DatRow("RefCritere") = RefCritere
                DatRow("RefSoumis") = rWs("RefSoumis")
                Nbre = 0
                For Each rw0 In TableCojo.Rows
                    Nbre += 1
                    DatRow("NoteEval" & Nbre.ToString) = RecherCheNoteEval(rWs("RefSoumis"), RefCritere, rw0("CodeMem"), TableEval)
                Next
                DatSet.Tables("t_noteevalparcritereperscle").Rows.Add(DatRow)
            Next

            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "t_noteevalparcritereperscle")
            DatSet.Clear()
            BDQUIT(sqlconn)

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub SaveDifferenteNoteEval(ByVal TableSoumi As DataTable, ByVal TableCojo As DataTable, ByVal RefCritere As Decimal, ByVal TableEval As DataTable)
        Try
            Dim Nbre As Integer = 0
            Dim DatSet = New DataSet
            query = "select * from t_noteevalparconsult"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)

            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "t_noteevalparconsult")
            Dim DatTable = DatSet.Tables("t_noteevalparconsult")

            For Each rWs In TableSoumi.Rows
                Dim DatRow = DatSet.Tables("t_noteevalparconsult").NewRow()
                DatRow("RefCritere") = RefCritere
                DatRow("RefSoumis") = rWs("RefSoumis")
                Nbre = 0
                For Each rw0 In TableCojo.Rows
                    Nbre += 1
                    DatRow("NoteEval" & Nbre.ToString) = RecherCheNoteEval(rWs("RefSoumis"), RefCritere, rw0("CodeMem"), TableEval)
                Next
                DatSet.Tables("t_noteevalparconsult").Rows.Add(DatRow)
            Next

            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "t_noteevalparconsult")
            DatSet.Clear()
            BDQUIT(sqlconn)

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Function RecherCheNoteEval(ByVal RefSoumis As Decimal, ByVal RefCritere As Decimal, ByVal CodeMem As Decimal, ByVal TableEval As DataTable) As Decimal
        Dim NoteEvaluateur As Decimal = 0
        Try
            For Each rw In TableEval.Rows
                If rw("RefSoumis") = RefSoumis And rw("RefCritere") = RefCritere And rw("CodeMem") = CodeMem Then
                    Return rw("NoteConsult").ToString.Replace(".", ", ").Replace(" ", "")
                End If
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        Return NoteEvaluateur
    End Function
    Private Function EnregistrerNomCojo(ByVal NumDoss As String) As DataTable

        Dim NomCojo As DataTable = ExcecuteSelectQuery("Select CodeMem, NomMem FROM t_commission WHERE NumeroDAO='" & NumDoss & "'")
        If NomCojo.Rows.Count <= 6 Then
            Dim DatSet = New DataSet
            query = "select * from t_tampevalnom"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)

            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "t_tampevalnom")
            Dim DatTable = DatSet.Tables("t_tampevalnom")
            Dim DatRow = DatSet.Tables("t_tampevalnom").NewRow()
            Dim cons As Integer = 0
            For Each rw In NomCojo.Rows
                cons += 1
                DatRow("NomEval" & cons.ToString) = rw("NomMem").ToString
            Next
            DatSet.Tables("t_tampevalnom").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "t_tampevalnom")
            DatSet.Clear()
            BDQUIT(sqlconn)
        End If
        Return NomCojo
    End Function


    'Private Sub BtNewRapport_Click(sender As Object, e As EventArgs) Handles BtNewRapport.Click
    '    Try
    '        If GroupBoxRapportCombine.Visible = True Then
    '            Dim dt As DataTable = ExcecuteSelectQuery("select * from T_SoumissionConsultant where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and EvalTechOk='OUI' and FinEvalFinanciere IS NOT NULL and RangFinal IS NOT NULL and CheminRapportCombine IS NULL ORDER BY RangFinal ASC LIMIT 1")
    '            If dt.Rows.Count > 0 Then
    '                CombRapport.Text = ""
    '                WebBrowser1.Navigate("")
    '                rwDossRapCombine = dt.Rows(0)

    '                'Generation du rapport **** arreter en cas d'erreur
    '                If GenererLoadRapCombinet() = False Then
    '                    Exit Sub
    '                End If

    '                DebutChargement()
    '                DateSoumiRC.Text = rwDossRapCombine("DateEnvoiRapComb").ToString
    '                DateReponRC.Text = rwDossRapCombine("DateRepoRapComb").ToString
    '                EnebledBoutonRapCombine()
    '                FinChargement()
    '            Else
    '                FailMsg("Impossible de faire un autre rapport" & vbNewLine & "car tous les rapports des consultants" & vbNewLine & "sur la liste restriente ont été générés")
    '            End If
    '        End If
    '    Catch ex As Exception
    '        FinChargement()
    '        FailMsg(ex.ToString)
    '    End Try
    'End Sub

    Private Sub CombRapport_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CombRapport.SelectedIndexChanged
        If GroupBoxRapportCombine.Visible = True Then
            If CombRapport.SelectedIndex <> -1 Then
                Try
                    rwDossRapCombine = ExcecuteSelectQuery("select * from t_soumissionconsultant where RefSoumis='" & CInt(CombRapport.Text.Split("|")(0)) & "' and NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'").Rows(0)

                    DateSoumiRC.Text = rwDossRapCombine("DateEnvoiRapComb").ToString
                    DateReponRC.Text = rwDossRapCombine("DateRepoRapComb").ToString

                    DateSoumiRC.Enabled = True
                    DateReponRC.Enabled = True
                    Dim CheminDoss As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\Rapport_Combine\"

                    If rwDossRapCombine("CheminRapportCombine").ToString = "" Then
                        'Gereartion du rapport *** on arrete en cas d'eurreur
                        If GenererLoadRapCombinet() = False Then Exit Sub
                    ElseIf Not File.Exists(CheminDoss & rwDossRapCombine("CheminRapportCombine").ToString) Then
                        If ConfirmMsg("Le rapport n'existe pas ou a été supprimer" & vbNewLine & "Voulez-vous le généré à nouveau ?") = DialogResult.Yes Then
                            If GenererLoadRapCombinet() = False Then Exit Sub
                        End If
                    ElseIf (File.Exists(CheminDoss & rwDossRapCombine("CheminRapportCombine").ToString)) Then
                        DebutChargement(True, "Chargement du rapport combiné en cours...")
                        WebBrowser1.Navigate(CheminDoss & rwDossRapCombine("CheminRapportCombine").ToString)
                        Threading.Thread.Sleep(3000)
                        FinChargement()
                    End If
                    EnebledBoutonRapCombine()

                Catch ex As Exception
                    FinChargement()
                    FailMsg(ex.ToString)
                End Try
            End If
        End If
    End Sub

    Private Sub Actualiserrc_Click(sender As Object, e As EventArgs) Handles Actualiserrc.Click
        Try
            If rwDossRapCombine("CheminRapportCombine").ToString = "" Then
                FailMsg("Aucun rapport à actualiser")
                Exit Sub
            End If

            If ModifRapCombine = False Then
                SuccesMsg("Veuillez modifier le rapport avant d'actualiser")
                Exit Sub
            End If

            Dim CheminDoss As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\Rapport_Combine"
            Dim NomRapportword As String = "RapportCombine.doc"
            Dim SaveRapportpdf As String = "RapportCombine_" & FormatFileName(Now.ToString.Replace(" ", ""), "") & ".pdf"

            Dim TraitMethod As Boolean = VerifiersMetohd(TxtMethode.Text)
            If TraitMethod = False Then
                NomRapportword = rwDossRapCombine("RefSoumis").ToString & "_RapportCombine\" & NomRapportword.ToString
                SaveRapportpdf = rwDossRapCombine("RefSoumis").ToString & "_RapportCombine\" & SaveRapportpdf.ToString
            End If

            If Not File.Exists(CheminDoss.ToString & "\" & NomRapportword) Then
                SuccesMsg("Le rapport à actualisé n'existe pas ou a été supprimé")
            ElseIf File.Exists(CheminDoss.ToString & "\" & NomRapportword) Then
                DebutChargement(True, "Actualisation du rapport combine en cours...")

                If Directory.Exists(CheminDoss) = False Then Directory.CreateDirectory(CheminDoss)
                If TraitMethod = False Then
                    If (Directory.Exists(CheminDoss & "\" & rwDossRapCombine("RefSoumis").ToString & "_RapportCombine") = False) Then Directory.CreateDirectory(CheminDoss & "\" & rwDossRapCombine("RefSoumis").ToString & "_RapportCombine")
                End If

                Dim WdApp As New Word.Application
                Dim WdDoc As New Word.Document

                Try
                    WdDoc = WdApp.Documents.Add(CheminDoss.ToString & "\" & NomRapportword)
                    WdDoc.SaveAs2(FileName:=CheminDoss.ToString & "\" & SaveRapportpdf.ToString, FileFormat:=Word.WdSaveFormat.wdFormatPDF)
                    WdDoc.Close(True)
                    WdApp.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                Catch ep As IO.IOException
                    FinChargement()
                    SuccesMsg("Un exemplaire du rapport est ouvert dans une autre application" & vbNewLine & "Veuillez le fermer svp.")
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

                rwDossRapCombine("CheminRapportCombine") = SaveRapportpdf.ToString
                If TraitMethod = False Then
                    ExecuteNonQuery("Update t_soumissionconsultant set CheminRapportCombine='" & SaveRapportpdf.ToString.Replace("\", "\\") & "' where RefSoumis='" & rwDossRapCombine("RefSoumis") & "'")
                Else
                    ExecuteNonQuery("Update t_soumissionconsultant set CheminRapportCombine='" & SaveRapportpdf.ToString.Replace("\", "\\") & "' where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and RangFinal IS NOT NULL")
                End If

                DebutChargement(True, "Chargement du rapport en cours...")
                WebBrowser1.Navigate(CheminDoss.ToString & "\" & rwDossRapCombine("CheminRapportCombine").ToString)
                Threading.Thread.Sleep(5000)
                ModifRapCombine = False
                FinChargement()
            End If
        Catch exs As IOException
            FinChargement()
            SuccesMsg("Le fichier est utiliser dans une autre application" & vbNewLine & "Veuillez le fermer svp.")
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub Envoibailleurrc_Click(sender As Object, e As EventArgs) Handles Envoibailleurrc.Click
        Try
            If rwDossRapCombine("CheminRapportCombine").ToString = "" Then
                FailMsg("Aucun rapport à envoyé au bailleur")
                Exit Sub
            End If

            Dim CheminDoss As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\Rapport_Combine"
            Dim NomRapportword As String = "RapportCombine.doc"
            'Cas des methodes 3CV SFQ SQC
            Dim TraitMethod As Boolean = VerifiersMetohd(TxtMethode.Text)
            If TraitMethod = False Then
                NomRapportword = rwDossRapCombine("RefSoumis").ToString & "_RapportCombine\RapportCombine.doc"
            End If
            CheminDoss = CheminDoss & "\" & NomRapportword

            If Not File.Exists(CheminDoss.ToString) Then
                FailMsg("Le rapport n'existe pas ou à été supprimé")
            ElseIf File.Exists(CheminDoss.ToString) Then

                If ChargerLesDonneEmail_AMI_DP_SERVICEAUTRES(CmbNumDoss.Text, "DP") = False Then
                    Exit Sub
                End If

                'Info de l'envoi de l'email
                If ConfirmMsg("Confirmez-vous l'envoi du rapport combiné" & vbNewLine & "au bailleur [ " & MettreApost(rwDossDPAMISA.Rows(0)("InitialeBailleur").ToString) & " ]") = DialogResult.Yes Then
                    DebutChargement(True, "Envoi du rapport combine au bailleur...")
                    'Envoi du rapport au bailleur
                    If EnvoiMailRapport(NomBailleurRetenu, CmbNumDoss.Text, EmailDestinatauer, CheminDoss.ToString, EmailCoordinateurProjet, EmailResponsablePM, "Rapport combiné") = False Then Exit Sub
                    SuccesMsg("Le rapport combiné a été envoye avec succès")
                    FinChargement()
                End If
            End If
        Catch exs As IOException
            FinChargement()
            FailMsg("Un exemplaire du rapport est ouvert dans une autre application" & vbNewLine & "Veuillez le fermer svp")
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub Validerrc_Click(sender As Object, e As EventArgs) Handles Validerrc.Click
        Try
            If rwDossRapCombine("CheminRapportCombine").ToString = "" Then
                FailMsg("Aucun rapport à valider")
                Exit Sub
            End If

            Dim CheminDoss As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\Rapport_Combine\"
            If Not File.Exists(CheminDoss & rwDossRapCombine("CheminRapportCombine").ToString) Then
                FailMsg("Le rapport que vous avez essayer de valider" & vbNewLine & "n'existe pas ou a été supprimé")
            ElseIf ConfirmMsg("La validation du rapport empêchera sa modification" & vbNewLine & "Voulez-vous continuez ?") = DialogResult.Yes Then
                Dim TraitMethod As Boolean = VerifiersMetohd(TxtMethode.Text)
                    If TraitMethod = False Then
                        ExecuteNonQuery("Update t_soumissionconsultant set EtatRapportCombine='Valider' where RefSoumis='" & rwDossRapCombine("RefSoumis") & "'")
                    Else
                        ExecuteNonQuery("Update t_soumissionconsultant set EtatRapportCombine='Valider' where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and EvalTechOk='OUI' and RangFinal IS NOT NULL")
                    End If

                    SuccesMsg("Rapport validé avec succès")
                    rwDossRapCombine("EtatRapportCombine") = "Valider"
                    EnebledBoutonRapCombine()
                    'Activer la bouton de la negociation
                    BtNegociation.Enabled = True
                End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub modifierrc_Click(sender As Object, e As EventArgs) Handles modifierrc.Click
        Try
            If rwDossRapCombine("CheminRapportCombine").ToString = "" Then
                FailMsg("Aucun rapport à modifier")
                Exit Sub
            End If
            DebutChargement()

            Dim CheminDoss As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\Rapport_Combine\"
            Dim CheminRapport As String = ""

            Dim TraitMethod As Boolean = VerifiersMetohd(TxtMethode.Text)
            If TraitMethod = False Then
                CheminRapport = rwDossRapCombine("RefSoumis").ToString & "_RapportCombine\RapportCombine.doc"
            Else
                CheminRapport = "RapportCombine.doc"
            End If

            If File.Exists(CheminDoss & CheminRapport.ToString) Then
                Process.Start(CheminDoss & CheminRapport.ToString)
                ModifRapCombine = True
                FinChargement()
            Else
                FinChargement()
                FailMsg("Le rapport à modifier n'existe pas ou à été supprimé")
            End If
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub pdfrc_Click(sender As Object, e As EventArgs) Handles pdfrc.Click
        Try
            If GetExportationRapport("pdf") = False Then Exit Sub
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub Wordrc_Click(sender As Object, e As EventArgs) Handles Wordrc.Click
        Try
            If GetExportationRapport("word") = False Then Exit Sub
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Function GetExportationRapport(TypeExportation As String) As Boolean
        Try
            If rwDossRapCombine("CheminRapportCombine").ToString = "" Then
                FailMsg("Aucun rapport à exporter")
                Return False
            End If

            Dim CheminDoss As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\Rapport_Combine\"
            Dim CheminRapport As String = ""

            Dim TraitMethod As Boolean = VerifiersMetohd(TxtMethode.Text)
            If TraitMethod = False Then
                CheminRapport = IIf(TypeExportation = "pdf", rwDossRapCombine("CheminRapportCombine").ToString, rwDossRapCombine("RefSoumis").ToString & "_RapportCombine\RapportCombine.doc").ToString
            Else
                CheminRapport = IIf(TypeExportation = "pdf", rwDossRapCombine("CheminRapportCombine").ToString, "RapportCombine.doc").ToString
            End If

            If File.Exists(CheminDoss & CheminRapport.ToString) Then
                If TypeExportation = "pdf" Then
                    Return ExporterPDF(CheminDoss & CheminRapport.ToString, "RapportCombine.pdf")
                Else
                    Return ExporterWORD(CheminDoss & CheminRapport.ToString, "Rapport_Combine.doc")
                End If
            Else
                FailMsg("La version du rapport à exporter" & vbNewLine & " n'existe pas ou à été supprimé")
                Return False
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
            Return False
        End Try
    End Function

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        GetVisiblePanel(True, "Accueil")
    End Sub

    Private Sub DateSoumiRC_LostFocus(sender As Object, e As EventArgs) Handles DateSoumiRC.LostFocus
        If DateSoumiRC.Text <> "" Then
            rwDossRapCombine("DateEnvoiRapComb") = DateSoumiRC.Text

            If GroupBoxRapportCombine.Visible = True Then
                Dim RefSoumis As Decimal = 0
                If CombRapport.SelectedIndex <> -1 Then
                    RefSoumis = CInt(CombRapport.Text.Split("|")(0))
                End If
                query = "Update t_soumissionconsultant set DateEnvoiRapComb='" & DateSoumiRC.Text & "' where RefSoumis ='" & RefSoumis & "'"
            Else
                query = "Update t_soumissionconsultant set DateEnvoiRapComb='" & DateSoumiRC.Text & "' where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and RangFinal IS NOT NULL"
            End If
            ExecuteNonQuery(query)
        End If
    End Sub

    Private Sub DateReponRC_LostFocus(sender As Object, e As EventArgs) Handles DateReponRC.LostFocus
        If DateReponRC.Text <> "" Then
            rwDossRapCombine("DateRepoRapComb") = DateReponRC.Text

            If GroupBoxRapportCombine.Visible = True Then
                Dim RefSoumis As Decimal = 0
                If CombRapport.SelectedIndex <> -1 Then
                    RefSoumis = CInt(CombRapport.Text.Split("|")(0))
                End If
                query = "Update t_soumissionconsultant set DateRepoRapComb='" & DateReponRC.Text & "' where RefSoumis ='" & RefSoumis & "'"
            Else
                query = "Update t_soumissionconsultant set DateRepoRapComb='" & DateReponRC.Text & "' where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and RangFinal IS NOT NULL"
            End If
            ExecuteNonQuery(query)
        End If
    End Sub
#End Region

#Region "Negociation"

    Private Sub BtNegociation_Click(sender As Object, e As EventArgs) Handles BtNegociation.Click
        If TablBoutonClik(3) = False Then
            'Cas de trois 3CV *** verifier s'il ya des consult retenu
            If TypeDossier(CmbNumDoss.Text) = "AMI" Then
                If Val(ExecuteScallar("select count(*) from t_soumissionconsultant where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and EvalTechOk='OUI' and RangConsult IS NOT NULL")) = 0 Then
                    FailMsg("Impossible d'accédé au contenu de ce bouton car aucun" & vbNewLine & "consultant n'est reteneu après l'évaluation technique")
                    Exit Sub
                End If
                CmbDevise.Visible = True
                LabelDevise.Visible = True
                RemplirCombo2(CmbDevise, "T_Devise", "AbregeDevise")
            Else
                CmbDevise.Visible = False
                LabelDevise.Visible = False
            End If

            InitialiserlesDonnesNego(True)
            ChargerlesNegociation()
            ChargerComiteMembre()
            'Bouton de la negociation cliquer
            TablBoutonClik(3) = True
            End If
            GetVisiblePanel(True, "Negociation")
    End Sub

    Private Sub ChargerlesNegociation()
        query = "select RefNego, NumeroNego from t_dp_negociation where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "' order by NumeroNego ASC"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        CmbNegoEdite.Properties.Items.Clear()
        CmbNegoEdite.Text = ""
        For Each rw As DataRow In dt.Rows
            CmbNegoEdite.Properties.Items.Add(GetNewCode(rw("RefNego").ToString) & " | " & MettreApost(rw("NumeroNego").ToString))
        Next
    End Sub

    Private Sub ChargerComiteMembre()
        query = "select RefComite, NomPren from t_dp_comitenegociation where CodeProjet='" & ProjetEnCours & "' GROUP BY NomPren ASC"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        MembreComite.Properties.Items.Clear()
        MembreComite.Text = ""
        For Each rw As DataRow In dt.Rows
            MembreComite.Properties.Items.Add(GetNewCode(rw("RefComite").ToString) & " | " & MettreApost(rw("NomPren").ToString))
        Next
    End Sub

    Private Sub InitialiserlesDonnesNego(value As Boolean)
        'CmbNegoEdite.Text = ""
        InitConsultRetenu()
        InitInfoNego()
        GetReadOnlyInifonego(value)
        InitialiserComite()
        GetReadonlyComite(value)
        InitPourcentage()
        GetReadonlyPourcentage(value)
        GridComite.Rows.Clear()
        GridPctNego.Rows.Clear()
    End Sub

    Private Sub InitConsultRetenu()
        NomConsultnego.ResetText()
        Contconsultnego.ResetText()
        Adressnego.ResetText()
        EmailCnsNego.ResetText()
        TxtStatut.ResetText()
    End Sub

    Private Sub InitInfoNego()
        NumeroNego.ResetText()
        DateNego.ResetText()
        HeureNegos.EditValue = Nothing
        MontantNego.ResetText()
        MoyenNego.ResetText()
        LieuNego.ResetText()
        CmbDevise.ResetText()
    End Sub

    Private Sub GetReadOnlyInifonego(value As Boolean)
        NumeroNego.Properties.ReadOnly = value
        DateNego.Enabled = Not value
        HeureNegos.Enabled = Not value
        MontantNego.Properties.ReadOnly = value
        MoyenNego.Properties.ReadOnly = value
        LieuNego.Properties.ReadOnly = value
        CmbDevise.Properties.ReadOnly = value
    End Sub

    Private Sub InitialiserComite()
        MembreComite.ResetText()
        NomPrenom.ResetText()
        ContactNego.ResetText()
        Organismenego.ResetText()
        FonctionNego.ResetText()
        ChkAutorite.Checked = False
    End Sub

    Private Sub GetReadonlyComite(value As Boolean)
        MembreComite.Properties.ReadOnly = value
        NomPrenom.Properties.ReadOnly = value
        ContactNego.Properties.ReadOnly = value
        Organismenego.Properties.ReadOnly = value
        FonctionNego.Properties.ReadOnly = value
        ChkAutorite.Properties.ReadOnly = value
        GridComite.Enabled = Not value
    End Sub

    Private Sub InitPourcentage()
        PctNego.ResetText()
        DescriptionPctNego.ResetText()
    End Sub

    Private Sub GetReadonlyPourcentage(value As Boolean)
        PctNego.Properties.ReadOnly = value
        DescriptionPctNego.Properties.ReadOnly = value
        GridPctNego.Enabled = Not value
    End Sub

    Private Sub NewNego_Click(sender As Object, e As EventArgs) Handles NewNego.Click
        Try
            Dim MessageTest As String = ""
            If TypeDossier(CmbNumDoss.Text) = "AMI" Then 'Cas de la methode 3CV
                query = "select C.RefConsult, C.NomConsult, C.TelConsult, C.AdressConsult, C.EmailConsult, S.RefSoumis, S.Negociation from t_consultant as C, t_soumissionconsultant as S where C.RefConsult=S.RefConsult and S.RangConsult IS NOT NULL and S.NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and S.EvalTechOk='OUI' and S.ConsultDisqualifie IS NULL ORDER BY S.RangConsult ASC LIMIT 3"
                MessageTest = "Tous les consultants retenus après l'évaluation" & vbNewLine & "technique pour ce dossier sont disqualifiés"
            Else
                query = "select C.RefConsult, C.NomConsult, C.TelConsult, C.AdressConsult, C.EmailConsult, S.RefSoumis, S.Negociation from t_consultant as C, t_soumissionconsultant as S where C.RefConsult=S.RefConsult and S.RangFinal IS NOT NULL and S.NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and S.EvalFinOk='OUI' and S.MontantAjusterLocal IS NOT NULL and S.EtatRapportCombine='Valider' and S.ConsultDisqualifie IS NULL ORDER BY S.RangFinal ASC"
                MessageTest = "Impossible de faire une négociation sur cette DP" & vbNewLine & "Raison, soit:" & vbNewLine & "- Aucun consultant n'est retenu après l'évaluation financière" & vbNewLine & "- Tous les rapports combinés des consultants retenus" & vbNewLine & "   non disqualifié n'ont pas été validés" & vbNewLine & "- Tous les consultants retenus pour ce dossier sont" & vbNewLine & "   disqualifiés"
            End If

            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then

                Dim NbrsDisqualifier As Integer = 0
                Dim Cpte As Integer = 0
                For Each rw In dt.Rows

                    'Négociation non efefectué
                    If IsDBNull(rw("Negociation")) Then
                        CmbNegoEdite.Text = ""
                        InitialiserlesDonnesNego(False)
                        TxtRefSoumis.Text = rw("RefSoumis").ToString
                        NomConsultnego.Text = MettreApost(rw("NomConsult").ToString)
                        Contconsultnego.Text = MettreApost(rw("TelConsult").ToString)
                        Adressnego.Text = MettreApost(rw("AdressConsult").ToString)
                        EmailCnsNego.Text = MettreApost(rw("EmailConsult").ToString)
                        TxtStatut.Text = "Consultant non disqualifié"
                        DejaSaveNego = False
                        DoubleClicks = False
                        NomGridView = ""
                        Exit For
                    Else
                        'Consultant en cours de disqualification
                        ReponseDialog = ""
                        Dim NewMotifDias As New MotifDisqualification
                        NewMotifDias.TxtNomConslt.Text = MettreApost(rw("NomConsult").ToString)
                        NewMotifDias.ShowDialog()
                        If ReponseDialog.ToString = "" Then
                            Exit Sub
                        End If
                        ExecuteNonQuery("update t_soumissionconsultant set ConsultDisqualifie='OUI', MotifDisqualification='" & EnleverApost(ReponseDialog.ToString) & "' where RefSoumis='" & rw("RefSoumis") & "' and NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'")
                        SuccesMsg("Disqualification effectuée avec succès")
                        NbrsDisqualifier += 1
                    End If
                Next
                'Tous les consultant de la liste restriente ont été disqualifier
                If NbrsDisqualifier = dt.Rows.Count Then
                    If TypeDossier(CmbNumDoss.Text) = "AMI" Then
                        FailMsg("Tous les consultants retenus après l'évaluation" & vbNewLine & "technique pour ce dossier sont disqualifiés")
                    Else
                        FailMsg("Tous les consultants retenus après l'évaluation" & vbNewLine & "financière dont le rapport combiné est validé" & vbNewLine & "pour ce dossier sont disqualifiés")
                    End If
                    Exit Sub
                End If
            Else
                SuccesMsg(MessageTest.ToString)
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub ModifNego_Click(sender As Object, e As EventArgs) Handles ModifNego.Click
        If CmbNegoEdite.Properties.Items.Count = 0 Then
            SuccesMsg("Aucune négociation à modifier")
            CmbNegoEdite.Select()
            Exit Sub
        End If
        If CmbNegoEdite.SelectedIndex = -1 Then
            SuccesMsg("Veuillez selectionner un dossier")
            CmbNegoEdite.Select()
            Exit Sub
        End If

        If TxtStatut.Text = "Consultant disqualifié" Then
            SuccesMsg("Impossible de modifier les informations" & vbNewLine & "d'un consultant disqualifié")
            Exit Sub
        End If

        GetReadOnlyInifonego(False)
        GetReadonlyComite(False)
        GetReadonlyPourcentage(False)
        DejaSaveNego = True
        DoubleClicks = False
        NomGridView = ""
    End Sub

    Private Sub CmbNegoEdite_SelectedValueChanged(sender As Object, e As EventArgs) Handles CmbNegoEdite.SelectedValueChanged
        InitialiserlesDonnesNego(True)

        If CmbNegoEdite.SelectedIndex <> -1 Then
            ChargerNegociationEdite()
            Dim ReNego As Decimal = CmbNegoEdite.Text.Split("|")(0)
            SaveComiteNego(CInt(ReNego), True)
            SaveModalite(CInt(ReNego), True)
        End If

        DoubleClicks = False
        NomGridView = ""
    End Sub

    Private Sub GridPctNego_DoubleClick(sender As Object, e As EventArgs) Handles GridPctNego.DoubleClick
        If GridPctNego.Rows.Count > 0 Then
            LignModif = GridPctNego.CurrentRow.Index
            PctNego.Text = GridPctNego.Rows.Item(LignModif).Cells("Pourcentage").Value
            DescriptionPctNego.Text = GridPctNego.Rows.Item(LignModif).Cells("Descriptionspct").Value
            DoubleClicks = True
            NomGridView = "P"
        End If
    End Sub

    Private Sub PctNego_KeyDown(sender As Object, e As KeyEventArgs) Handles PctNego.KeyDown, DescriptionPctNego.KeyDown
        If e.KeyCode = Keys.Enter Then
            If PctNego.IsRequiredControl("Veuillez saisir le pourcentage") Then
                PctNego.Select()
                Exit Sub
            End If
            If DescriptionPctNego.IsRequiredControl("Veuilles saisir la description") Then
                DescriptionPctNego.Select()
                Exit Sub
            End If

            Dim n As Integer
            Dim TotalPct As Decimal = 0

            If DoubleClicks = True And NomGridView = "P" Then
                'Index du tabaleau de la ligne  selectionné

                'Verification pourcentage 100%
                If GridPctNego.RowCount > 0 Then
                    For i = 0 To GridPctNego.RowCount - 1
                        If i <> LignModif Then
                            TotalPct += CDec(GridPctNego.Rows.Item(i).Cells("Pourcentage").Value.ToString.Replace(".", ","))
                        End If
                    Next
                End If

                TotalPct += CDec(PctNego.Text.Replace(".", ","))
                If TotalPct > 100 Then
                    FailMsg("Le pourcentage ne doit pas exceder 100%")
                    Exit Sub
                End If

                n = LignModif
                GridPctNego.Rows.Item(n).Cells("Modifpct").Value = "Modifier"
            Else
                'Verification pourcentage 100%
                If GridPctNego.RowCount > 0 Then
                    For i = 0 To GridPctNego.RowCount - 1
                        TotalPct += CDec(GridPctNego.Rows.Item(i).Cells("Pourcentage").Value.ToString.Replace(".", ","))
                    Next
                End If

                TotalPct += CDec(PctNego.Text.Replace(".", ","))
                If TotalPct > 100 Then
                    FailMsg("Le pourcentage ne doit pas exceder 100%")
                    Exit Sub
                End If

                n = GridPctNego.Rows.Add()
                GridPctNego.Rows.Item(n).Cells("Refpct").Value = ""

                GridPctNego.Rows.Item(n).Cells("Modifpct").Value = ""
            End If
            GridPctNego.Rows.Item(n).Cells("Pourcentage").Value = PctNego.Text.Replace(".", ",").Replace(".00", "").Replace(",00", "")
            GridPctNego.Rows.Item(n).Cells("Descriptionspct").Value = DescriptionPctNego.Text

            DoubleClicks = False
            NomGridView = ""
            InitPourcentage()
        End If
    End Sub

    Private Sub NomPrenom_KeyDown(sender As Object, e As KeyEventArgs) Handles Organismenego.KeyDown, NomPrenom.KeyDown, FonctionNego.KeyDown, ContactNego.KeyDown
        If e.KeyCode = Keys.Enter Then
            If NomPrenom.IsRequiredControl("Veuillez saisir le nom") Then
                NomPrenom.Select()
                Exit Sub
            End If
            If Organismenego.IsRequiredControl("Veuilles saisir l'organisme") Then
                Organismenego.Select()
                Exit Sub
            End If
            If FonctionNego.IsRequiredControl("Veuillez saisir la fonction") Then
                FonctionNego.Select()
                Exit Sub
            End If

            Dim n As Integer
            If DoubleClicks = True And NomGridView = "C" Then
                'Index du tabaleau de la ligne  selectionné
                n = LignModif
                GridComite.Rows.Item(n).Cells("modifcmte").Value = "Modifier"
            Else
                'Verifier
                Dim TypeCom As String = ""
                If GridComite.RowCount > 0 Then
                    For i = 0 To GridComite.RowCount - 1
                        TypeCom = IIf(ChkAutorite.Checked = True, "OUI", "NON").ToString()
                        If NomPrenom.Text = GridComite.Rows.Item(i).Cells("NomPrencmte").Value.ToString And Organismenego.Text = GridComite.Rows.Item(i).Cells("Organismecmte").Value.ToString And FonctionNego.Text = GridComite.Rows.Item(i).Cells("Fonctioncmte").Value.ToString And ContactNego.Text = GridComite.Rows.Item(i).Cells("Contactcmte").Value.ToString And GridComite.Rows.Item(i).Cells("TypeComite").Value.ToString = TypeCom.ToString Then
                            FailMsg("Ce mêmbre existe déjà")
                            Exit Sub
                        End If
                    Next
                End If

                n = GridComite.Rows.Add()
                GridComite.Rows.Item(n).Cells("Refcmte").Value = ""

                GridComite.Rows.Item(n).Cells("modifcmte").Value = ""
            End If
            GridComite.Rows.Item(n).Cells("NomPrencmte").Value = NomPrenom.Text
            GridComite.Rows.Item(n).Cells("Contactcmte").Value = ContactNego.Text
            GridComite.Rows.Item(n).Cells("Fonctioncmte").Value = FonctionNego.Text
            GridComite.Rows.Item(n).Cells("Organismecmte").Value = Organismenego.Text
            GridComite.Rows.Item(n).Cells("TypeComite").Value = IIf(ChkAutorite.Checked = True, "OUI", "NON").ToString

            DoubleClicks = False
            NomGridView = ""

            InitialiserComite()
        End If
    End Sub

    Private Sub GridComite_DoubleClick(sender As Object, e As EventArgs) Handles GridComite.DoubleClick
        If GridComite.RowCount > 0 Then
            LignModif = GridComite.CurrentRow.Index
            NomPrenom.Text = GridComite.Rows.Item(LignModif).Cells("NomPrencmte").Value
            ContactNego.Text = GridComite.Rows.Item(LignModif).Cells("Contactcmte").Value
            FonctionNego.Text = GridComite.Rows.Item(LignModif).Cells("Fonctioncmte").Value
            Organismenego.Text = GridComite.Rows.Item(LignModif).Cells("Organismecmte").Value
            ChkAutorite.Checked = IIf(GridComite.Rows.Item(LignModif).Cells("TypeComite").Value = "OUI", True, False).ToString
            DoubleClicks = True
            NomGridView = "C"
        End If
    End Sub

    Private Sub MembreComite_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MembreComite.SelectedIndexChanged
        If MembreComite.SelectedIndex <> -1 Then
            query = "select * from t_dp_comitenegociation where RefComite='" & CInt(MembreComite.Text.Split("|")(0)) & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                NomPrenom.Text = MettreApost(rw("NomPren").ToString)
                ContactNego.Text = MettreApost(rw("Contact").ToString)
                FonctionNego.Text = MettreApost(rw("Fonction").ToString)
                Organismenego.Text = MettreApost(rw("Organisme").ToString)
                ChkAutorite.Checked = IIf(rw("TypeComite").ToString = "OUI", True, False).ToString
            Next
        End If
    End Sub

    Private Sub GridComite_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles GridComite.CellMouseDown
        Try
            NomGridView = IIf(GridComite.RowCount > 0, "C", "").ToString

            If e.RowIndex <> -1 And e.ColumnIndex <> -1 Then
                If (e.Button = MouseButtons.Right) Then
                    Try
                        GridComite.CurrentCell = GridComite.Rows(e.RowIndex).Cells(e.ColumnIndex)
                        GridComite.Rows(e.RowIndex).Selected = True
                        GridComite.Focus()
                    Catch ex As Exception
                        FailMsg(ex.ToString)
                    End Try
                End If
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub GridPctNego_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles GridPctNego.CellMouseDown
        Try
            NomGridView = IIf(GridPctNego.RowCount > 0, "P", "").ToString

            If e.RowIndex <> -1 And e.ColumnIndex <> -1 Then
                If (e.Button = MouseButtons.Right) Then
                    Try
                        GridPctNego.CurrentCell = GridPctNego.Rows(e.RowIndex).Cells(e.ColumnIndex)
                        GridPctNego.Rows(e.RowIndex).Selected = True
                        GridPctNego.Focus()
                    Catch ex As Exception
                        FailMsg(ex.ToString)
                    End Try
                End If
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub


    Private Sub SupprimerToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles SupprimerToolStripMenuItem3.Click
        If NomGridView = "C" Then
            If GridComite.RowCount > 0 Then
                Dim Index As Integer = GridComite.CurrentRow.Index
                If ConfirmMsg("Voulez-vous supprimer ce mêmbre ?") = DialogResult.Yes Then
                    If GridComite.Rows.Item(Index).Cells("Refcmte").Value <> "" Then
                        ExecuteNonQuery("delete from t_dp_comitenegociation where RefComite='" & GridComite.Rows.Item(Index).Cells("Refcmte").Value & "'")
                    End If
                    GridComite.Rows.RemoveAt(Index)
                    DoubleClicks = False
                    NomGridView = ""
                End If
            End If
        ElseIf NomGridView = "P" Then
            If GridPctNego.RowCount > 0 Then
                Dim Index As Integer = GridPctNego.CurrentRow.Index
                If ConfirmMsg("Êtes-vous sûrs de vouloir supprimer ?") = DialogResult.Yes Then
                    If GridPctNego.Rows.Item(Index).Cells("Refpct").Value <> "" Then
                        ExecuteNonQuery("delete from t_dp_modalitenegociation where RefModalite='" & GridPctNego.Rows.Item(Index).Cells("Refpct").Value & "'")
                    End If
                    GridPctNego.Rows.RemoveAt(Index)
                    DoubleClicks = False
                    NomGridView = ""
                End If
            End If
        End If
    End Sub

    Private Sub ContextMenuStrip3_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip3.Opening
        If GridComite.RowCount = 0 And GridPctNego.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub Btsavenego_Click(sender As Object, e As EventArgs) Handles Btsavenego.Click
        'Aucune action effectué
        If NumeroNego.Properties.ReadOnly = True Or TxtStatut.Text = "Consultant disqualifié" Then Exit Sub
        'If TxtStatut.Text = "Consultant disqualifié" Then
        '    SuccesMsg("Impossible de modifier les informations" & vbNewLine & "d'un consultant disqualifié")
        '    Exit Sub
        'End If

        Try
            If NumeroNego.IsRequiredControl("Veuillez saisir le numero de la négociation") Then
                NumeroNego.Select()
                Exit Sub
            End If
            If DateNego.IsRequiredControl("Veuillez saisir la date de la négociation") Then
                DateNego.Select()
                Exit Sub
            End If
            If HeureNegos.IsRequiredControl("Veuillez saisir l'heure de la négociation") Then
                HeureNegos.Select()
                Exit Sub
            End If
            If MontantNego.IsRequiredControl("Veuillez saisir le montant de la négociation") Then
                MontantNego.Select()
                Exit Sub
            End If

            If MoyenNego.Text.Trim = "" And LieuNego.Text.Trim = "" Then
                SuccesMsg("Veuillez saisir le moyen ou le lieu de la négociation")
                MoyenNego.Select()
                Exit Sub
            End If
            If GridComite.RowCount = 0 Then
                SuccesMsg("Veuillez saisir les mêmbres de la comité de négociation")
                Exit Sub
            End If
            If GridPctNego.RowCount = 0 Then
                SuccesMsg("Veuillez decrire le modalité de paiement")
                Exit Sub
            End If

            If CmbDevise.Visible = True Then
                If CmbDevise.IsRequiredControl("Veuillez selectionné la devise") Then
                    CmbDevise.Select()
                    Exit Sub
                End If
            End If

            'Verification pourcentage 100%
            Dim TotalPct As Decimal = 0
            If GridPctNego.RowCount > 0 Then
                For i = 0 To GridPctNego.RowCount - 1
                    TotalPct += CDec(GridPctNego.Rows.Item(i).Cells("Pourcentage").Value.ToString.Replace(".", ","))
                Next
            End If
            If TotalPct < 100 Then
                FailMsg("Le pourcentage de modalité de paiement doit être egal 100%")
                Exit Sub
            End If

            DebutChargement(True, "Enregistrement en cours...")

            Dim Devise As String = IIf(CmbDevise.Visible = True, EnleverApost(CmbDevise.Text), "").ToString
            Dim RefNego As Decimal = 0
            'Nouvo nego
            If DejaSaveNego = False Then
                If Val(ExecuteScallar("select count(*) from t_dp_negociation where NumeroNego='" & EnleverApost(NumeroNego.Text) & "'")) > 0 Then
                    FailMsg("Le numéro de la négociation existe déjà")
                    Exit Sub
                End If

                ExecuteNonQuery("INSERT INTO t_dp_negociation VALUES(NULL,'" & EnleverApost(NumeroNego.Text) & "', '" & EnleverApost(CmbNumDoss.Text) & "', '" & TxtRefSoumis.Text & "', '" & dateconvert(DateNego.Text) & " " & HeureNegos.Text & "', '" & MontantNego.Text.Replace(" ", "").Replace(",", ".") & "', '" & EnleverApost(MoyenNego.Text) & "', '" & EnleverApost(LieuNego.Text) & "', '" & ProjetEnCours & "', '" & CodeOperateurEnCours & "', '" & Devise.ToString & "')")
                ExecuteNonQuery("Update t_soumissionconsultant set Negociation='OUI' where RefSoumis='" & TxtRefSoumis.Text & "'")

                RefNego = ExecuteScallar("SELECT MAX(RefNego) from t_dp_negociation")
            ElseIf DejaSaveNego = True Then
                RefNego = CInt(CmbNegoEdite.Text.Split("|")(0))
                ExecuteNonQuery("UPDATE t_dp_negociation SET DateHeureNego='" & dateconvert(DateNego.Text) & " " & HeureNegos.Text & "', MontantNego='" & MontantNego.Text.Replace(" ", "").Replace(",", ".") & "', MoyenNego='" & EnleverApost(MoyenNego.Text) & "', LieuNego='" & EnleverApost(LieuNego.Text) & "', Operateur='" & CodeOperateurEnCours & "', AbregeDevise='" & Devise.ToString & "' where RefNego='" & RefNego & "'")
                DejaSaveNego = True
            End If

            'Save comite et Update
            SaveComiteNego(RefNego)
            'Save pct et Update
            SaveModalite(RefNego)

            If DejaSaveNego = False Then
                Dim NewCodeNego As String = GetNewCode(RefNego) & " | " & NumeroNego.Text
                CmbNegoEdite.Properties.Items.Add(NewCodeNego)
                CmbNegoEdite.Text = NewCodeNego.ToString
                'Modif Nego
                DejaSaveNego = True
            End If

            FinChargement()
            SuccesMsg("Enregistrement effectué avec succès")
            'Activer le bouton de l'edition du contrat
            BtEditionContrat.Enabled = True
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try

    End Sub

    Private Sub ChargerNegociationEdite()
        Try
            Dim dt As DataTable = ExcecuteSelectQuery("select * from t_dp_negociation where RefNego='" & CInt(CmbNegoEdite.Text.Split("|")(0)) & "'")
            For Each rw In dt.Rows
                NumeroNego.Text = MettreApost(rw("NumeroNego").ToString)
                DateNego.Text = CDate(rw("DateHeureNego").ToString).ToShortDateString
                HeureNegos.EditValue = CDate(rw("DateHeureNego").ToString).ToLongTimeString
                MontantNego.Text = AfficherMonnaie(rw("MontantNego").ToString.Replace(".00", "").Replace(",00", ""))
                MoyenNego.Text = MettreApost(rw("MoyenNego").ToString)
                LieuNego.Text = MettreApost(rw("LieuNego").ToString)
                CmbDevise.Text = MettreApost(rw("AbregeDevise").ToString)

                InfoConsultRetenu(rw("RefSoumis").ToString)
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub InfoConsultRetenu(RefSoumis As String)
        Try
            query = "select C.RefConsult, C.NomConsult, C.TelConsult, C.AdressConsult, C.EmailConsult, S.RefSoumis, S.ConsultDisqualifie from t_consultant as C, t_soumissionconsultant as S where C.RefConsult=S.RefConsult and S.RefSoumis='" & RefSoumis & "' and S.NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw In dt.Rows
                    TxtRefSoumis.Text = rw("RefSoumis").ToString
                    NomConsultnego.Text = MettreApost(rw("NomConsult").ToString)
                    Contconsultnego.Text = MettreApost(rw("TelConsult").ToString)
                    Adressnego.Text = MettreApost(rw("AdressConsult").ToString)
                    EmailCnsNego.Text = MettreApost(rw("EmailConsult").ToString)
                    TxtStatut.Text = IIf(IsDBNull(rw("ConsultDisqualifie")), "Consultant non disqualifié", "Consultant disqualifié").ToString
                Next
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub


    Private Sub SaveComiteNego(ByVal RefNego As String, Optional Afficher As Boolean = False)
        Try
            If Afficher = False Then
                If GridComite.RowCount > 0 Then
                    For n = 0 To GridComite.Rows.Count - 1
                        If GridComite.Rows.Item(n).Cells("Refcmte").Value.ToString = "" Then
                            ExecuteNonQuery("Insert into t_dp_comitenegociation values(NULL,'" & RefNego & "','" & EnleverApost(GridComite.Rows.Item(n).Cells("NomPrencmte").Value.ToString) & "','" & EnleverApost(GridComite.Rows.Item(n).Cells("Contactcmte").Value.ToString) & "', '" & EnleverApost(GridComite.Rows.Item(n).Cells("TypeComite").Value.ToString) & "', '" & EnleverApost(GridComite.Rows.Item(n).Cells("Fonctioncmte").Value.ToString) & "', '" & EnleverApost(GridComite.Rows.Item(n).Cells("Organismecmte").Value.ToString) & "', '" & ProjetEnCours & "')")
                            Afficher = True
                        ElseIf GridComite.Rows.Item(n).Cells("modifcmte").Value.ToString = "Modifier" Then
                            ExecuteNonQuery("Update t_dp_comitenegociation set NomPren='" & EnleverApost(GridComite.Rows.Item(n).Cells("NomPrencmte").Value.ToString) & "', Contact='" & EnleverApost(GridComite.Rows.Item(n).Cells("Contactcmte").Value.ToString) & "', TypeComite='" & EnleverApost(GridComite.Rows.Item(n).Cells("TypeComite").Value.ToString) & "', Fonction='" & EnleverApost(GridComite.Rows.Item(n).Cells("Fonctioncmte").Value.ToString) & "', Organisme='" & EnleverApost(GridComite.Rows.Item(n).Cells("Organismecmte").Value.ToString) & "' where RefComite='" & GridComite.Rows.Item(n).Cells("Refcmte").Value & "'")
                            Afficher = True
                        End If
                    Next
                End If
                'Ajouter oçu modifier actualiser le combo
                If Afficher = True Then ChargerComiteMembre()
            End If

            If Afficher = True Then
                query = "Select * from t_dp_comitenegociation where RefNego='" & RefNego & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)

                GridComite.Rows.Clear()
                For Each rw In dt.Rows
                    Dim n As Integer = GridComite.Rows.Add()
                    GridComite.Rows.Item(n).Cells("Refcmte").Value = rw("RefComite").ToString
                    GridComite.Rows.Item(n).Cells("modifcmte").Value = "Enregistrer"
                    GridComite.Rows.Item(n).Cells("NomPrencmte").Value = MettreApost(rw("NomPren").ToString)
                    GridComite.Rows.Item(n).Cells("Contactcmte").Value = MettreApost(rw("Contact").ToString)
                    GridComite.Rows.Item(n).Cells("Fonctioncmte").Value = MettreApost(rw("Fonction").ToString)
                    GridComite.Rows.Item(n).Cells("Organismecmte").Value = MettreApost(rw("Organisme").ToString)
                    GridComite.Rows.Item(n).Cells("TypeComite").Value = rw("TypeComite").ToString
                    n += 1
                Next
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub SaveModalite(ByVal RefNego As String, Optional Afficher As Boolean = False)
        Try
            If Afficher = False Then
                If GridPctNego.RowCount > 0 Then
                    For n = 0 To GridPctNego.Rows.Count - 1
                        If GridPctNego.Rows.Item(n).Cells("Refpct").Value.ToString = "" Then
                            ExecuteNonQuery("Insert into t_dp_modalitenegociation values(NULL,'" & RefNego & "','" & GridPctNego.Rows.Item(n).Cells("Pourcentage").Value.ToString.Replace(",", ".").Replace(" ", "") & "','" & EnleverApost(GridPctNego.Rows.Item(n).Cells("Descriptionspct").Value.ToString) & "', '" & ProjetEnCours & "')")
                            Afficher = True
                        ElseIf GridPctNego.Rows.Item(n).Cells("Modifpct").Value.ToString = "Modifier" Then
                            ExecuteNonQuery("Update t_dp_modalitenegociation set Prctage='" & GridPctNego.Rows.Item(n).Cells("Pourcentage").Value.ToString.Replace(",", ".").Replace(" ", "") & "', Description='" & EnleverApost(GridPctNego.Rows.Item(n).Cells("Descriptionspct").Value.ToString) & "' where RefModalite ='" & GridPctNego.Rows.Item(n).Cells("Refpct").Value & "'")
                            Afficher = True
                        End If
                    Next
                End If
            End If

            If Afficher = True Then
                query = "Select * from t_dp_modalitenegociation where RefNego='" & RefNego & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)

                GridPctNego.Rows.Clear()
                For Each rw In dt.Rows
                    Dim n As Integer = GridPctNego.Rows.Add()
                    GridPctNego.Rows.Item(n).Cells("Refpct").Value = rw("RefModalite").ToString
                    GridPctNego.Rows.Item(n).Cells("Modifpct").Value = "Enregistrer"
                    GridPctNego.Rows.Item(n).Cells("Pourcentage").Value = AfficherMonnaie(rw("Prctage").ToString.Replace(".00", "").Replace(",00", ""))
                    GridPctNego.Rows.Item(n).Cells("Descriptionspct").Value = MettreApost(rw("Description").ToString)
                    n += 1
                Next
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub Btprintnego_Click(sender As Object, e As EventArgs) Handles Btprintnego.Click
        Try
            If CmbNegoEdite.Properties.Items.Count = 0 Then
                SuccesMsg("Aucune négociation à imprimer")
                CmbNegoEdite.Select()
                Exit Sub
            End If
            If CmbNegoEdite.SelectedIndex = -1 Then
                SuccesMsg("Veuillez selectionner un dossier")
                CmbNegoEdite.Select()
                Exit Sub
            End If

            DebutChargement(True, "Chargement du Pv de négociation en cours...")
            Dim RapportPVNego As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table

            Dim DatSet = New DataSet

            Dim Chemin As String = lineEtat & "\Marches\DP\PV Ouverture Offres financieres\"
            If TypeDossier(CmbNumDoss.Text) = "AMI" Then 'Cas de la methode 3CV
                RapportPVNego.Load(Chemin & "PvNegociation_Methode_3CV.rpt")
            Else
                RapportPVNego.Load(Chemin & "PvNegociation.rpt")
            End If

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = RapportPVNego.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next
            RapportPVNego.SetDataSource(DatSet)

            RapportPVNego.SetParameterValue("NumNegociation", EnleverApost(EnleverApost(CmbNegoEdite.Text.Split("|")(1).Trim)))
            RapportPVNego.SetParameterValue("NumDP", EnleverApost(CmbNumDoss.Text))
            RapportPVNego.SetParameterValue("CodeProjet", ProjetEnCours)

            FinChargement()
            With FullScreenReport
                .FullView.ReportSource = RapportPVNego
                .Text = "PV DE NEGOCIATION N° " & EnleverApost(CmbNegoEdite.Text.Split("|")(1).Trim)
                .ShowDialog()
            End With
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub
#End Region

#Region "Edition du marche"

    Private Sub ChargerListeContrat(Controls As DevExpress.XtraEditors.ComboBoxEdit)
        'query = "Select NumeroMarche from t_marchesigne where TypeMarche='Consultants' and TypeMarche1='Consultants' and RefSoumis IS NOT NULL and CodeProjet='" & ProjetEnCours & "'"
        query = "Select NumContrat from t_dp_contrat where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dtt As DataTable = ExcecuteSelectQuery(query)
        Controls.Text = ""
        Controls.Properties.Items.Clear()
        Dim i As Integer = 0
        For Each rw In dtt.Rows
            Controls.Properties.Items.Add(MettreApost(rw("NumContrat").ToString))
        Next
    End Sub

    Private Sub BtEditionContrat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEditionContrat.Click
        If CmbNumDoss.SelectedIndex <> -1 Then
            'Bouton du contrat non contrat
            If TablBoutonClik(4) = False Then
                DebutChargement()
                'Verification
                GetInitialiserContrat()
                ChargerListeContrat(CmbContrat)
                NewLoadAnnexe()
                NewReadOnly(True)
                'Changement de l'etat
                TablBoutonClik(4) = True
                If TxtMethode.Text.ToUpper = "3CV" Then
                    TypeRenumeration.Visible = True
                    LabelControl40.Visible = True
                    LabelControl39.Visible = True
                    GroupControlRepresenLegal.Enabled = False
                    NomChefFil.Enabled = False
                    Representantcheffil.Enabled = False
                Else
                    GroupControlRepresenLegal.Enabled = True
                    NomChefFil.Enabled = True
                    Representantcheffil.Enabled = True
                    TypeRenumeration.Visible = False
                    LabelControl40.Visible = False
                    LabelControl39.Visible = False
                End If
                FinChargement()
            End If
            GetVisiblePanel(True, "Marche")
        End If
    End Sub

    Private Sub BtSaveContrat_Click(sender As Object, e As EventArgs) Handles BtSaveContrat.Click
        'Aucune action effectuer
        If (ViewRepartion.OptionsBehavior.Editable = False) Then
            FailMsg("Aucune action effectuée")
            Exit Sub
        End If
        If StatutConsult.Text = "Disqualifié" Then
            SuccesMsg("Impossible d'enregister les informations" & vbNewLine & "d'un consultant disqualifié")
            Exit Sub
        End If

        If NumContrat.IsRequiredControl("Veuillez saisir le numéro du contrat") Then
            NumContrat.Focus()
            Exit Sub
        End If

        If TypeRenumeration.Visible = True Then
            If TypeRenumeration.IsRequiredControl("Veuillez selectionner le type de rénumération") Then
                TypeRenumeration.Select()
                Exit Sub
            End If
        End If

        If GroupControlRepresenLegal.Enabled = True Then 'A ignorer cas de 3CV consultant individuel
            If TxtNomRepLegal.IsRequiredControl("Veuillez saisir le nom du répresentant") Then
                TxtNomRepLegal.Focus()
                Exit Sub
            End If
            'If TxtBpRepLegal.IsRequiredControl("Veuillez saisir la boîte postale du representant") Then
            '    TxtBpRepLegal.Focus()
            '    Exit Sub
            'End If

            If TxtContactRepLegal.IsRequiredControl("Veuillez saisir le contact du répresentant") Then
                TxtContactRepLegal.Focus()
                Exit Sub
            End If
        End If


        If delairesiliation.Text = "" And cmbdelairesi.Text <> "" Then
            SuccesMsg("Veuillez saisir le délai de resiliation")
            delairesiliation.Select()
            Exit Sub
        End If

        If delairesiliation.Text <> "" And cmbdelairesi.Text = "" Then
            SuccesMsg("Saisir incorrect.")
            cmbdelairesi.Select()
            Exit Sub
        End If
        If (DateAchev1.Text = "" And DateAchev2.Text <> "") Or (DateAchev1.Text <> "" And DateAchev2.Text = "") Then
            SuccesMsg("La période d'achèvement du contrat est incorrect")
            DateAchev1.Select()
            Exit Sub
        End If
        If (DateAchev1.Text <> "" And DateAchev2.Text <> "") Then
            If DateTime.Compare(CDate(DateAchev1.Text), CDate(DateAchev2.Text)) > 0 Then
                SuccesMsg("La période d'achèvement du contrat est incorrect")
                DateAchev1.Select()
                Exit Sub
            End If
        End If

        If Disqualification.Checked = True And MotifDisqualif.Text = "" Then
            SuccesMsg("Veuillez saisir le motif de disqualification")
            MotifDisqualif.Select()
            Exit Sub
        End If

        If (NbrExecution.Text.Trim = "") Then
            SuccesMsg("Veuillez saisir le délai d'execution")
            NbrExecution.Select()
            Exit Sub
        End If

        If JoursExecution.IsRequiredControl("Veuillez selectionné un element dans la liste") Then
            JoursExecution.Select()
            Exit Sub
        End If

        If ViewRepartion.RowCount = 0 Then
            SuccesMsg("Faite la repartion du montant du contrat")
            Exit Sub
        End If

        Dim MontantRepartion As Decimal = 0
        Dim BienRenseigner As Boolean = False
        Dim NombrConvenVide As Integer = 0
        Dim Cpte As Integer = 0
        If ViewRepartion.RowCount > 0 Then
            For i = 0 To ViewRepartion.RowCount - 1 'Parcourir les ligne
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
            SuccesMsg("Veuillez bien renseigné le tableau" & vbNewLine & "de la repartition du montant du contrat")
            Exit Sub
        End If

        If CDec(MontantRepartion) <> CDec(TxtMontantMarche.Text) Then
            SuccesMsg("La repartition du montant du marche est incorrect")
            Exit Sub
        End If

        'Contrat non enregistrer
        If DejaEnregistrer = False Then
            If Val(ExecuteScallar("SELECT COUNT(*) from t_marchesigne where NumeroMarche='" & EnleverApost(NumContrat.Text) & "'")) > 0 Then
                SuccesMsg("Le numéro du contrat existe déjà")
                NumContrat.Focus()
                Exit Sub
            End If

            DebutChargement(True, "Enregistrement du contrat en cours...")

            Dim DatSet = New DataSet
            query = "select * from t_marchesigne"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "t_marchesigne")
            Dim DatTable = DatSet.Tables("t_marchesigne")
            Dim DatRow = DatSet.Tables("t_marchesigne").NewRow()

            DatRow("RefConsult") = RefConsults
            DatRow("RefSoumis") = RefSoumisRetenuContrat
            DatRow("NumeroMarche") = EnleverApost(NumContrat.Text)
            DatRow("NumeroDAO") = EnleverApost(CmbNumDoss.Text)
            DatRow("DateMarche") = CDate(Now.ToString).ToShortDateString
            DatRow("DateMarche") = CDate(Now.ToString).ToShortDateString
            DatRow("EtatMarche") = "En cours"
            DatRow("TypeMarche") = "Consultants"
            DatRow("MontantHT") = Round(CDbl(TxtMontantMarche.Text))
            If TxtPrctCautionDef.Text <> "" Then DatRow("PrctCautionDef") = TxtPrctCautionDef.Text '.Replace(",", "").Replace(".00", "")
            If TxtPrctAvance.Text <> "" Then DatRow("PrctAvance") = TxtPrctAvance.Text '.Replace(",00", "").Replace(".00", "")
            DatRow("ImputBudgetaire") = EnleverApost(TxtImputBudgetaire.Text)
            DatRow("CodeProjet") = ProjetEnCours
            DatSet.Tables("t_marchesigne").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "t_marchesigne")
            DatSet.Clear()
            BDQUIT(sqlconn)

            EnregistreRepartition(NumContrat.Text)
            Save_LesInfoContrat(NumContrat.Text)
            'Enregistrement
            Save_ChargerLesArticles(NumContrat.Text)
            EnregistretePiece(NumContrat.Text)

            ExecuteNonQuery("update t_soumissionconsultant set AttribueContrat='OUI' where RefSoumis='" & RefSoumisRetenuContrat & "' and NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'")

            ' ChargerListeContrat()
            CmbContrat.Properties.Items.Add(NumContrat.Text)
            NumContrat.Enabled = False
            FinChargement()
            SuccesMsg("Contrat enregistré avec succès")
            DejaEnregistrer = True
            TablBoutonClik(5) = False
            BtImprimerContrat.Enabled = True

        ElseIf DejaEnregistrer = True Then

            DebutChargement(True, "Modification du contrat en cours...")
            Dim d As String = ""
            ExecuteNonQuery("Update t_marchesigne Set MontantHT ='" & Val(TxtMontantMarche.Text.Replace(" ", "")) & "', PrctCautionDef= '" & IIf(TxtPrctCautionDef.Text <> "", TxtPrctCautionDef.Text.Replace(",", "."), 0).ToString & "', PrctAvance='" & IIf(TxtPrctAvance.Text <> "", TxtPrctAvance.Text.Replace(",", "."), 0).ToString & "', ImputBudgetaire='" & EnleverApost(TxtImputBudgetaire.Text) & "' where NumeroMarche='" & EnleverApost(NumContrat.Text) & "'")

            Save_ChargerLesArticles(NumContrat.Text)
            EnregistreRepartition(NumContrat.Text)
            Save_LesInfoContrat(NumContrat.Text, "Update")
            EnregistretePiece(NumContrat.Text)
            FinChargement()
            SuccesMsg("Contrat modifié avec succès")
        End If

        If TypeRenumeration.Visible = True Then
            ExecuteNonQuery("update t_ami set TypeRemune='" & EnleverApost(TypeRenumeration.Text) & "' where NumeroDAMI='" & EnleverApost(CmbNumDoss.Text) & "'")
            ExecuteNonQuery("update t_marche set Forfait_TpsPasse='" & EnleverApost(TypeRenumeration.Text) & "' where RefMarche='" & CurrenRefMarche & "'")
        End If
    End Sub

    Private Sub EnregistreRepartition(ByVal NumeroContrat As String)
        Try
            ExecuteNonQuery("delete from t_dp_repartition_montant_contrat where NumeroContrat='" & EnleverApost(NumeroContrat.ToString) & "' and CodeProjet='" & ProjetEnCours & "'")

            If ViewRepartion.RowCount > 0 Then
                Dim Ligne As Integer = 0

                Dim DatSet = New DataSet
                query = "select * from t_dp_repartition_montant_contrat"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "t_dp_repartition_montant_contrat")
                Dim DatTable = DatSet.Tables("t_dp_repartition_montant_contrat")

                For i = 0 To ViewRepartion.RowCount - 1
                    Dim DatRow = DatSet.Tables("t_dp_repartition_montant_contrat").NewRow()

                    DatRow("NumeroContrat") = EnleverApost(NumeroContrat.ToString)
                    DatRow("CodeProjet") = ProjetEnCours
                    DatRow("Annee") = ViewRepartion.GetRowCellValue(i, "Année").ToString
                    Ligne = 0

                    For j = 2 To ViewRepartion.Columns.Count - 1
                        Ligne += 1
                        DatRow("CodeConvention" & Ligne) = MettreApost(ViewRepartion.Columns(j).GetTextCaption)
                        DatRow("MontantConvention" & Ligne) = ViewRepartion.GetDataRow(i).Item(j).ToString.Replace(" ", "")
                    Next

                    DatSet.Tables("t_dp_repartition_montant_contrat").Rows.Add(DatRow)
                Next

                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "t_dp_repartition_montant_contrat")
                DatSet.Clear()
                BDQUIT(sqlconn)

                LoadRepartionMontantMarche("Load")
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub


    Private Sub Save_LesInfoContrat(ByVal NumeroContrat As String, Optional Afficher As String = "")
        Try
            Dim DelaiExecution As String = IIf(NbrExecution.Text <> "", NbrExecution.Text & " " & JoursExecution.Text, "").ToString
            Dim delairesiliations As String = IIf(delairesiliation.Text <> "", delairesiliation.Text & " " & cmbdelairesi.Text, "").ToString
            Dim DateAchev As String = IIf(DateAchev1.Text <> "", DateAchev1.Text & " " & DateAchev2.Text, "").ToString

            If Afficher.ToString = "" Then
                query = "INSERT INTO t_dp_contrat values(NULL, '" & EnleverApost(NumeroContrat.ToString) & "', '" & EnleverApost(CmbNumDoss.Text) & "', '" & EnleverApost(TxtNomRepLegal.Text) & "', '" & EnleverApost(TxtBpRepLegal.Text) & "', '" & TxtContactRepLegal.Text & "', '" & EnleverApost(EmailRepresentant.Text) & "', '" & EnleverApost(TxtContribuable.Text) & "', '" & EnleverApost(TxtRegCommerce.Text) & "', '" & EnleverApost(TxtNomBanqueFournisDevise.Text) & "', '" & EnleverApost(NomBanqMonaiLocal.Text) & "', '" & EnleverApost(NumCptedevise.Text) & "', '" & EnleverApost(NumCpteLocal.Text) & "', '" & EnleverApost(TxtBailleurMarche.Text) & "', '" & EnleverApost(TxtConventionMarche.Text) & "', '" & DelaiExecution.ToString & "', '" & dateconvert(Now.ToShortDateString) & " " & Now.ToShortTimeString & "', '" & dateconvert(Now.ToShortDateString) & " " & Now.ToShortTimeString & "', '" & CodeUtilisateur & "', '" & ProjetEnCours & "', NULL, NULL, '" & EnleverApost(NomChefFil.Text) & "', '" & EnleverApost(Representantcheffil.Text) & "', '" & delairesiliations.ToString & "', '" & TauxAnuel.Text.Replace(".", ",").Replace(".00", "").Replace(",00", "") & "', '" & DateAchev.ToString & "', '" & IIf(Disqualification.Checked = True, "OUI", "NON").ToString & "', '" & IIf(Disqualification.Checked = True, EnleverApost(MotifDisqualif.Text), "").ToString & "', '" & MantantTaxe.Text.Replace(".", ",").Replace(".00", "").Replace(",00", "") & "')"
                ExecuteNonQuery(query)
            ElseIf Afficher.ToString = "Update" Then
                query = "Update t_dp_contrat Set NomPrenRepre = '" & EnleverApost(TxtNomRepLegal.Text) & "', EmailRepresentant='" & EnleverApost(EmailRepresentant.Text) & "',  BoitePostalRepr= '" & EnleverApost(TxtBpRepLegal.Text) & "', ContactRepr='" & TxtContactRepLegal.Text & "', CompteContribuabl = '" & EnleverApost(TxtContribuable.Text) & "', RegistreCommerce='" & EnleverApost(TxtRegCommerce.Text) & "',  MotifDisqualif='" & IIf(Disqualification.Checked = True, EnleverApost(MotifDisqualif.Text), "").ToString & "', "
                query &= " NomBanqDevise='" & EnleverApost(TxtNomBanqueFournisDevise.Text) & "', DelaiExecution='" & DelaiExecution.ToString & "', NomBanqMonaiLocal='" & EnleverApost(NomBanqMonaiLocal.Text) & "', NumCptedeviseConsult='" & EnleverApost(NumCptedevise.Text) & "', NumCpteLocalConsult='" & EnleverApost(NumCpteLocal.Text) & "', PeriodeAchev='" & DateAchev.ToString & "', Disqualification='" & IIf(Disqualification.Checked = True, "OUI", "NON").ToString & "',"
                query &= " DateModif ='" & dateconvert(Now.ToShortDateString) & " " & Now.ToShortTimeString & "', Operateur= '" & CodeUtilisateur & "', NomChefFil='" & EnleverApost(NomChefFil.Text) & "', Representantcheffil='" & EnleverApost(Representantcheffil.Text) & "', delairesiliation='" & delairesiliations.ToString & "', TauxAnuel='" & TauxAnuel.Text.Replace(".", ",").Replace(".00", "").Replace(",00", "") & "', MantantTaxe='" & MantantTaxe.Text.Replace(".", ",").Replace(".00", "").Replace(",00", "") & "' where NumContrat ='" & EnleverApost(NumContrat.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub BtModifContrat_Click(sender As Object, e As EventArgs) Handles BtModifContrat.Click
        If CmbContrat.Properties.Items.Count = 0 Then
            SuccesMsg("Aucun contrat à modofier")
            CmbContrat.Focus()
            Exit Sub
        End If
        If CmbContrat.SelectedIndex = -1 Then
            SuccesMsg("Veuillez selectionner le contrat à modofier")
            CmbContrat.Focus()
            Exit Sub
        End If
        If StatutConsult.Text = "Disqualifié" Then
            SuccesMsg("Impossible de modifier les informations d'un consultant disqualifié")
            Exit Sub
        End If

        BtAjoutArticle.Text = "AJOUTER"
        DejaEnregistrer = True
        NewReadOnly(False)
        NumContrat.Properties.ReadOnly = True
        ViewRepartion.OptionsBehavior.Editable = True
    End Sub

    Private Sub TxtPrctAvance_EditValueChanged(sender As Object, e As EventArgs) Handles TxtPrctAvance.EditValueChanged
        If TxtPrctAvance.Text <> "" And TxtMontantMarche.Text <> "" Then
            If CDec(TxtPrctAvance.Text) > 0 Then
                TxtMontAvance.Text = AfficherMonnaie((CDec(TxtPrctAvance.Text.Replace(".", ",")) / 100) * CDec(TxtMontantMarche.Text.Replace(".", ",")))
            Else
                TxtMontAvance.Text = ""
            End If
        Else
            TxtMontAvance.Text = ""
        End If
    End Sub

    Private Sub TxtPrctCautionDef_EditValueChanged(sender As Object, e As EventArgs) Handles TxtPrctCautionDef.EditValueChanged
        If TxtPrctCautionDef.Text <> "" And TxtMontantMarche.Text <> "" Then
            If CDec(TxtPrctCautionDef.Text) > 0 Then
                TxtMontCautionDef.Text = AfficherMonnaie((CDec(TxtPrctCautionDef.Text.Replace(".", ",")) / 100) * CDec(TxtMontantMarche.Text.Replace(".", ",")))
            Else
                TxtMontCautionDef.Text = ""
            End If
        Else
            TxtMontCautionDef.Text = ""
        End If
    End Sub

    Private Sub GetInitialiserContrat()
        TypeConvention = ""
        DejaEnregistrer = False

        NumContrat.Text = ""
        TxtConsultContrat.Text = ""
        TxtContactRepre.Text = ""
        TxtAdresseConsContrat.Text = ""
        TxtEmail.Text = ""
        TxtNomRepLegal.Text = ""
        TxtBpRepLegal.Text = ""
        TxtContactRepLegal.Text = ""
        EmailRepresentant.Text = ""
        TxtContribuable.Text = ""
        TxtRegCommerce.Text = ""
        TxtNomBanqueFournisDevise.Text = ""
        NomBanqMonaiLocal.Text = ""
        TxtMontantMarche.Text = ""
        TxtMontMarcheLettre.Text = ""
        TxtPrctCautionDef.Text = ""
        TxtMontCautionDef.Text = ""
        TxtPrctAvance.Text = ""
        TxtMontAvance.Text = ""
        TxtBailleurMarche.Text = ""
        TxtConventionMarche.Text = ""
        TxtImputBudgetaire.Text = ""
        StatutConsult.Text = ""

        NbrExecution.Text = ""
        JoursExecution.Text = ""
        NumCptedevise.Text = ""
        NumCpteLocal.Text = ""
        CmbArticle.Text = ""
        TxtArticle.Text = ""

        NomChefFil.ResetText()
        Representantcheffil.ResetText()
        delairesiliation.ResetText()
        cmbdelairesi.ResetText()
        TauxAnuel.ResetText()
        DateAchev1.EditValue = Nothing
        DateAchev2.EditValue = Nothing
        Disqualification.Checked = False
        MotifDisqualif.ResetText()
        MantantTaxe.ResetText()
        TypeRenumeration.ResetText()

        GridArticle.Rows.Clear()
        dtAnnexe.Rows.Clear()
        GridAnnexe.DataSource = Nothing

        ViewRepartion.Columns.Clear()
        ListeRepartion.DataSource = Nothing
    End Sub

    Private Sub NewReadOnly(ByVal value As Boolean)

        NumContrat.Properties.ReadOnly = value
        NumCptedevise.Properties.ReadOnly = value
        NumCpteLocal.Properties.ReadOnly = value

        TxtNomRepLegal.Properties.ReadOnly = value
        TxtBpRepLegal.Properties.ReadOnly = value
        TxtContactRepLegal.Properties.ReadOnly = value
        EmailRepresentant.Properties.ReadOnly = value
        TxtContribuable.Properties.ReadOnly = value
        TxtRegCommerce.Properties.ReadOnly = value

        TxtNomBanqueFournisDevise.Properties.ReadOnly = value
        NomBanqMonaiLocal.Properties.ReadOnly = value

        TxtPrctCautionDef.Properties.ReadOnly = value
        TxtPrctAvance.Properties.ReadOnly = value
        TxtImputBudgetaire.Properties.ReadOnly = value
        NbrExecution.Properties.ReadOnly = value
        JoursExecution.Properties.ReadOnly = value

        NomChefFil.Properties.ReadOnly = value
        Representantcheffil.Properties.ReadOnly = value
        delairesiliation.Properties.ReadOnly = value
        cmbdelairesi.Properties.ReadOnly = value
        TauxAnuel.Properties.ReadOnly = value
        DateAchev1.Enabled = Not value
        DateAchev2.Enabled = Not value
        Disqualification.Enabled = Not value
        ' MotifDisqualif.Properties.ReadOnly = value
        MantantTaxe.Properties.ReadOnly = value
        BtDelete.Enabled = Not value
        AddLigneRepartition.Enabled = Not value
        CmbArticle.Enabled = Not value
        BtAjoutArticle.Enabled = Not value
        BtSelectAnnexe.Enabled = Not value
        GridAnnexe.Enabled = Not value
        IntituleAnnexe.Enabled = Not value
        TxtArticle.Properties.ReadOnly = value
        TypeRenumeration.Properties.ReadOnly = value
        GridArticle.Enabled = Not value

    End Sub

    Private Sub CmbContrat_SelectedValueChanged_1(sender As Object, e As EventArgs) Handles CmbContrat.SelectedValueChanged
        'Initialiser
        GetInitialiserContrat()
        If CmbContrat.SelectedIndex <> -1 Then

            DebutChargement(True, "Chargement des données en cours...")

            NumContrat.Text = CmbContrat.Text
            query = "select m.ImputBudgetaire, m.RefSoumis, m.MontantHT, m.PrctCautionDef, m.PrctAvance, c.* from t_marchesigne as m, t_dp_contrat as c where m.NumeroMarche=c.NumContrat and m.NumeroMarche='" & EnleverApost(CmbContrat.Text) & "' and c.NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and c.CodeProjet='" & ProjetEnCours & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw0 In dt1.Rows
                RefSoumisRetenuContrat = rw0("RefSoumis").ToString
                ReponseDialog = rw0("MontantHT").ToString.Replace(" ", "")
                TxtMontantMarche.Text = AfficherMonnaie(ReponseDialog)
                TxtMontMarcheLettre.Text = MontantLettre(ReponseDialog)
                Dim PctCaution As String = ""
                Dim PctAvance As String = ""
                PctCaution = IIf(rw0("PrctCautionDef").ToString = "0.00", "", rw0("PrctCautionDef").ToString.Replace(".", ",")).ToString
                PctAvance = IIf(rw0("PrctAvance").ToString = "0.00", "", rw0("PrctAvance").ToString.Replace(".", ",")).ToString
                TxtPrctAvance.Text = PctAvance
                TxtPrctCautionDef.Text = PctCaution
                If PctCaution.ToString <> "" Then TxtMontCautionDef.Text = AfficherMonnaie((CDec(PctCaution) / 100) * CDec(rw0("MontantHT").ToString.Replace(".", ",")))
                If PctAvance.ToString <> "" Then TxtMontAvance.Text = AfficherMonnaie((CDec(PctAvance) / 100) * CDec(rw0("MontantHT")))
                TxtImputBudgetaire.Text = rw0("ImputBudgetaire").ToString

                TxtNomRepLegal.Text = MettreApost(rw0("NomPrenRepre").ToString)
                TxtBpRepLegal.Text = MettreApost(rw0("BoitePostalRepr").ToString)
                TxtContactRepLegal.Text = rw0("ContactRepr").ToString
                TxtContribuable.Text = MettreApost(rw0("CompteContribuabl").ToString)
                TxtRegCommerce.Text = MettreApost(rw0("RegistreCommerce").ToString)
                TxtNomBanqueFournisDevise.Text = MettreApost(rw0("NomBanqDevise").ToString)
                NomBanqMonaiLocal.Text = MettreApost(rw0("NomBanqMonaiLocal").ToString)
                NumCptedevise.Text = MettreApost(rw0("NumCptedeviseConsult").ToString)
                EmailRepresentant.Text = MettreApost(rw0("EmailRepresentant").ToString)
                NumCpteLocal.Text = MettreApost(rw0("NumCpteLocalConsult").ToString)
                TxtBailleurMarche.Text = MettreApost(rw0("SFinancBailleurMarche").ToString)

                TxtConventionMarche.Text = rw0("CodeConvention").ToString
                TypeConvention = ExecuteScallar("select TypeConvention from t_convention where CodeConvention='" & rw0("CodeConvention") & "'")

                If rw0("DelaiExecution").ToString <> "" Then
                    NbrExecution.Text = rw0("DelaiExecution").ToString.Split(" ")(0)
                    JoursExecution.Text = rw0("DelaiExecution").ToString.Split(" ")(1)
                End If

                NomChefFil.Text = MettreApost(rw0("NomChefFil").ToString)
                Representantcheffil.Text = MettreApost(rw0("Representantcheffil").ToString)
                If rw0("delairesiliation").ToString <> "" Then
                    delairesiliation.Text = rw0("delairesiliation").ToString.Split(" ")(0)
                    cmbdelairesi.Text = rw0("delairesiliation").ToString.Split(" ")(1)
                End If
                TauxAnuel.Text = rw0("TauxAnuel").ToString
                If rw0("PeriodeAchev").ToString <> "" Then
                    DateAchev1.Text = rw0("PeriodeAchev").ToString.Split(" ")(0)
                    DateAchev2.Text = rw0("PeriodeAchev").Split(" ")(1)
                End If
                Disqualification.Checked = IIf(rw0("Disqualification").ToString = "OUI", True, False).ToString
                MotifDisqualif.Text = IIf(rw0("Disqualification").ToString = "OUI", MettreApost(rw0("MotifDisqualif").ToString), "").ToString
                MantantTaxe.Text = rw0("MantantTaxe").ToString
            Next

            query = "select c.RefConsult, c.NomConsult, c.TelConsult, c.AdressConsult, c.EmailConsult, s.ConsultDisqualifie from t_consultant as c, t_soumissionconsultant as s where c.RefConsult=s.RefConsult and s.RefSoumis='" & RefSoumisRetenuContrat & "' and s.NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                TxtConsultContrat.Text = MettreApost(rw("NomConsult").ToString)
                TxtContactRepre.Text = rw("TelConsult").ToString
                TxtAdresseConsContrat.Text = rw("AdressConsult").ToString
                TxtEmail.Text = MettreApost(rw("EmailConsult").ToString)
                RefConsults = rw("RefConsult").ToString
                StatutConsult.Text = IIf(IsDBNull(rw("ConsultDisqualifie")), "Non disqualifié", "Disqualifié").ToString
            Next

            LoadRepartionMontantMarche("Load")
            Save_ChargerLesArticles(CmbContrat.Text, True)
            EnregistretePiece(CmbContrat.Text, True)

            If TypeRenumeration.Visible = True Then
                TypeRenumeration.Text = MettreApost(ExecuteScallar("select TypeRemune from t_ami where NumeroDAMI='" & EnleverApost(CmbNumDoss.Text) & "'").ToString)
            End If

            FinChargement()
        End If
        ViewRepartion.OptionsBehavior.Editable = False
        NewReadOnly(True)
        BtAjoutArticle.Text = "AJOUTER"

    End Sub

    Private Sub BtNouveauContrat_Click(sender As Object, e As EventArgs) Handles BtNouveauContrat.Click
        If TypeDossier(CmbNumDoss.Text) = "AMI" Then
            query = "select C.RefConsult, C.NomConsult, C.TelConsult, C.AdressConsult, C.EmailConsult, N.MontantNego, S.RefSoumis, S.AttribueContrat from t_consultant as C, t_soumissionconsultant as S, t_dp_negociation as N where C.RefConsult=S.RefConsult and S.RefSoumis=N.RefSoumis and S.Negociation='OUI' and S.RangConsult IS NOT NULL and S.EvalTechOk='OUI' and S.NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and S.ConsultDisqualifie IS NULL ORDER BY S.RangConsult ASC LIMIT 3"
        Else
            query = "select C.RefConsult, C.NomConsult, C.TelConsult, C.AdressConsult, C.EmailConsult, N.MontantNego, S.RefSoumis, S.AttribueContrat from t_consultant as C, t_soumissionconsultant as S, t_dp_negociation as N where C.RefConsult=S.RefConsult and S.RefSoumis=N.RefSoumis and S.Negociation='OUI' and S.RangFinal IS NOT NULL and S.EvalFinOk='OUI' and S.NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and S.ConsultDisqualifie IS NULL ORDER BY S.RangFinal ASC"
        End If

        Dim dt As DataTable = ExcecuteSelectQuery(query)

        If dt.Rows.Count > 0 Then
            DebutChargement(True, "Initialisation des données en cours...")
            Dim NbrsDisqualifier As Integer = 0
            For Each rw In dt.Rows

                'Contrat non attribuer
                If IsDBNull(rw("AttribueContrat")) Then
                    CmbContrat.Text = ""
                    GetInitialiserContrat()
                    NewReadOnly(False)

                    ReponseDialog = rw("MontantNego").ToString.Replace(" ", "")
                    TxtMontantMarche.Text = AfficherMonnaie(rw("MontantNego").ToString)
                    TxtMontMarcheLettre.Text = MontantLettre(rw("MontantNego").ToString.Replace(".00", "").Replace(".", ","))

                    TxtConsultContrat.Text = MettreApost(rw("NomConsult").ToString)
                    TxtContactRepre.Text = MettreApost(rw("TelConsult").ToString)
                    TxtAdresseConsContrat.Text = MettreApost(rw("AdressConsult").ToString)
                    TxtEmail.Text = MettreApost(rw("EmailConsult").ToString)
                    RefSoumisRetenuContrat = rw("RefSoumis").ToString
                    RefConsults = rw("RefConsult").ToString
                    StatutConsult.Text = "Non disqualifié"

                    'Info marche
                    Dim ListeConvention As String = ""

                    If TypeDossier(CmbNumDoss.Text) = "AMI" Then 'Cas de la methode 3CV
                        query = "Select m.MontantEstimatif,m.Convention_ChefFile, m.CodeConvention, b.NomBailleur, b.InitialeBailleur, c.TypeConvention, c.TitreConvention from t_marche as m, t_bailleur as b, t_convention as c, T_ami as a  where a.RefMarche=m.RefMarche and m.Convention_ChefFile=c.CodeConvention and c.CodeBailleur=b.CodeBailleur and a.NumeroDAMI='" & EnleverApost(CmbNumDoss.Text) & "'"
                    Else
                        query = "Select m.MontantEstimatif,m.Convention_ChefFile, m.CodeConvention, b.NomBailleur, b.InitialeBailleur, c.TypeConvention, c.TitreConvention from t_marche as m, t_bailleur as b, t_convention as c, t_dp as d  where d.RefMarche=m.RefMarche and m.Convention_ChefFile=c.CodeConvention and c.CodeBailleur=b.CodeBailleur and d.NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'"
                    End If

                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 In dt1.Rows
                        TxtBailleurMarche.Text = IIf(rw1("NomBailleur").ToString <> "", MettreApost(rw1("InitialeBailleur").ToString) & " - " & MettreApost(rw1("NomBailleur").ToString), MettreApost(rw1("InitialeBailleur").ToString)).ToString
                        TxtConventionMarche.Text = IIf(rw1("TitreConvention").ToString <> "", MettreApost(rw1("Convention_ChefFile").ToString) & " - " & MettreApost(rw1("TitreConvention").ToString), MettreApost(rw1("Convention_ChefFile").ToString)).ToString
                        TypeConvention = MettreApost(rw1("TypeConvention").ToString)
                        ListeConvention = rw1("CodeConvention").ToString
                    Next

                    NumContrat.Text = ""
                    NumContrat.Select()
                    BtAjoutArticle.Text = "AJOUTER"
                    DejaEnregistrer = False
                    LoadRepartionMontantMarche("CreateTable", ListeConvention)
                    FinChargement()

                    Exit For
                Else
                    'Consultant en cours de disqualification
                    FinChargement()
                    ReponseDialog = ""
                    Dim NewMotifDias As New MotifDisqualification
                    NewMotifDias.TxtNomConslt.Text = MettreApost(rw("NomConsult").ToString)
                    NewMotifDias.ShowDialog()
                    If ReponseDialog.ToString = "" Then
                        Exit Sub
                    End If
                    ExecuteNonQuery("update t_soumissionconsultant set ConsultDisqualifie='OUI', MotifDisqualification='" & EnleverApost(ReponseDialog.ToString) & "' where RefSoumis='" & rw("RefSoumis") & "' and NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'")
                    SuccesMsg("Disqualification effectuée avec succès")
                    NbrsDisqualifier += 1
                End If
            Next

            'Tous les consultant de la liste restriente ont été disqualifier
            If NbrsDisqualifier = dt.Rows.Count Then
                FailMsg("Tous les consultants retenus pour" & vbNewLine & "ce marché sont disqualifiés")
                Exit Sub
            End If
        Else
            FailMsg("Impossible d'élaboré un autre contrat sur ce dossier")
        End If
    End Sub

    Private Sub LoadRepartionMontantMarche(TypaChargement As String, Optional ListeConvention As String = "")
        Try
            Dim dtRepart = New DataTable()
            dtRepart.Columns.Clear()
            ViewRepartion.Columns.Clear()
            dtRepart.Columns.Add("RefRepartion", Type.GetType("System.String"))
            dtRepart.Columns.Add("Année", Type.GetType("System.String"))

            If TypaChargement = "CreateTable" Then
                Dim ListeCon As String() = ListeConvention.Split("|")
                For i = 0 To ListeCon.Length - 1
                    If ListeCon(i).ToString.Trim <> "" Then
                        dtRepart.Columns.Add(ListeCon(i).ToString, Type.GetType("System.String"))
                    End If
                Next
                dtRepart.Rows.Clear()
            Else

                Dim dtt As DataTable = ExcecuteSelectQuery("select * from t_dp_repartition_montant_contrat where NumeroContrat='" & EnleverApost(NumContrat.Text) & "'")
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
                ViewRepartion.Columns(i).Width = 250
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
                        ExecuteNonQuery("delete from t_dp_repartition_montant_contrat where RefRepartion='" & RefRepartion & "'")
                    End If
                    ViewRepartion.GetDataRow(ViewRepartion.FocusedRowHandle).Delete()
                End If
            Else
                FailMsg("Aucune ligne à supprimé")
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub Save_ChargerLesArticles(ByVal NumeroContrat As String, Optional Afficher As Boolean = False)
        Try
            If Afficher = False Then
                If GridArticle.RowCount > 0 Then
                    For n = 0 To GridArticle.Rows.Count - 1
                        If GridArticle.Rows.Item(n).Cells("RefArticle").Value.ToString = "" Then
                            query = "Insert into t_dp_articlecontrat values(NULL,'" & EnleverApost(NumeroContrat) & "', '" & GridArticle.Rows.Item(n).Cells("CodeArticle").Value.ToString & "','" & EnleverApost(GridArticle.Rows.Item(n).Cells("Description").Value.ToString) & "', '" & ProjetEnCours & "')"
                            ExecuteNonQuery(query)
                            Afficher = True
                        ElseIf GridArticle.Rows.Item(n).Cells("LigneModif").Value.ToString = "Modifier" Then
                            query = "Update t_dp_articlecontrat set CodeArticle='" & GridArticle.Rows.Item(n).Cells("CodeArticle").Value.ToString & "', DescriptionArticle='" & EnleverApost(GridArticle.Rows.Item(n).Cells("Description").Value.ToString) & "' where RefArticle='" & GridArticle.Rows.Item(n).Cells("RefArticle").Value.ToString & "' and  CodeProjet='" & ProjetEnCours & "'"
                            ExecuteNonQuery(query)
                            Afficher = True
                        End If
                    Next
                End If
            End If

            If Afficher = True Then
                query = "Select * from t_dp_articlecontrat where NumeroContrat='" & EnleverApost(NumeroContrat) & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)

                GridArticle.Rows.Clear()
                For Each rw In dt.Rows
                    Dim n As Integer = GridArticle.Rows.Add()
                    GridArticle.Rows.Item(n).Cells("RefArticle").Value = rw("RefArticle").ToString
                    GridArticle.Rows.Item(n).Cells("CodeArticle").Value = MettreApost(rw("CodeArticle").ToString)
                    GridArticle.Rows.Item(n).Cells("Description").Value = MettreApost(rw("DescriptionArticle").ToString)
                    GridArticle.Rows.Item(n).Cells("LigneModif").Value = "Enregistrer"
                    n += 1
                Next
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub NewLoadAnnexe()
        ' Dim dtAnnexe = New DataTable()
        dtAnnexe.Columns.Clear()
        dtAnnexe.Columns.Add("Code", Type.GetType("System.String"))
        dtAnnexe.Columns.Add("RefAnnexe", Type.GetType("System.String"))
        dtAnnexe.Columns.Add("Intitule annexe", Type.GetType("System.String"))
        dtAnnexe.Columns.Add("Nom du fichier", Type.GetType("System.String"))
        dtAnnexe.Columns.Add("Chemin", Type.GetType("System.String"))
        dtAnnexe.Rows.Clear()
        GridAnnexe.DataSource = dtAnnexe
        ViewAnnexe.Columns("Code").Visible = False
        ViewAnnexe.Columns("RefAnnexe").Visible = False
        ViewAnnexe.Columns("Nom du fichier").Width = 300
        ViewAnnexe.Columns("Chemin").Visible = False
        ViewAnnexe.Columns("Intitule annexe").Width = 200
    End Sub

    Private Sub EnregistretePiece(ByVal NumeroContrat As String, Optional afficher As Boolean = False)
        Try

            If afficher = False And ViewAnnexe.RowCount > 0 Then

                Dim fichier As String = ""
                Dim NomComp As String()
                Dim NomCourt As String = ""
                Dim NomDossier As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\" & FormatFileName(NumContrat.Text, "") & "\Annexe"

                If (Directory.Exists(NomDossier) = False) Then
                    Directory.CreateDirectory(NomDossier)
                End If

                For i = 0 To ViewAnnexe.RowCount - 1
                    If ViewAnnexe.GetRowCellValue(i, "RefAnnexe").ToString = "" Then
                        fichier = ViewAnnexe.GetRowCellValue(i, "Chemin").ToString
                        NomComp = fichier.Split("\"c)
                        NomCourt = FormatFileName(NomComp(NomComp.Length - 1), "")

                        If (File.Exists(NomDossier & "\" & NomCourt) = False) Then
                            File.Copy(fichier, NomDossier & "\" & NomCourt, True)
                            ReponseDialog = NomDossier & "\" & NomCourt
                            query = "INSERT INTO t_dp_annexepj VALUES(NULL, '" & EnleverApost(NumeroContrat) & "', '" & EnleverApost(NomCourt) & "', '" & EnleverApost(ReponseDialog.Replace("\", "\\")) & "', '" & EnleverApost(ViewAnnexe.GetRowCellValue(i, "Intitule annexe").ToString) & "', '" & ProjetEnCours & "')"
                            ExecuteNonQuery(query)
                            afficher = True
                        End If
                    End If
                Next
            End If

            If afficher = True Then
                query = "select * from t_dp_annexepj where NumeroContrat='" & EnleverApost(NumeroContrat.ToString) & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                dtAnnexe.Rows.Clear()

                If dt.Rows.Count > 0 Then
                    Dim nbreP As Decimal = 0
                    For Each rw In dt.Rows
                        nbreP += 1
                        Dim drS = dtAnnexe.NewRow()
                        drS("Code") = IIf(CDec(nbreP / 2) <> CDec(nbreP \ 2), "x", "").ToString
                        drS("RefAnnexe") = rw("RefAnnexe").ToString
                        drS("Intitule annexe") = MettreApost(rw("IntituleAnnexe").ToString)
                        drS("Nom du fichier") = MettreApost(rw("NomPJ").ToString)
                        drS("Chemin") = MettreApost(rw("CheminPJ").ToString)
                        dtAnnexe.Rows.Add(drS)
                    Next
                    GridAnnexe.DataSource = dtAnnexe
                    ViewAnnexe.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
                    ColorRowGrid(ViewAnnexe, "[Code]='x'", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.Black)
                End If
            End If
        Catch eur As IOException
            FailMsg("Un exemplaire des fichiers [annexe] est ouvert dans une autre application" & vbNewLine & "Veuillez le fermer svp")
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub BtSelectAnnexe_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles BtSelectAnnexe.LinkClicked
        If IntituleAnnexe.Text.Trim = "" Then
            SuccesMsg("Veuillez saisir l'intitulé de l'annexe")
            IntituleAnnexe.Select()
            Exit Sub
        End If

        Dim dlg As New OpenFileDialog
        'type du document a ouvrir'
        dlg.Filter = "Documents |*.pdf;*.png;*.jpeg;*.jpg:*.docx;*.txt;*.xlsx;*.doc;*.rtf;*.xls;*.pptx;*.pptm;*.ppt;*.xps;*.pot;*.odp;*.docm;*.dot;*.dotx;*.dotm;*.xps;"
        If (dlg.ShowDialog() = DialogResult.OK) Then
            Dim k As Integer = 0
            Dim fichier As String = dlg.FileName
            Dim NomCourt As String = dlg.SafeFileName

            For i As Integer = 0 To ViewAnnexe.RowCount - 1
                k += 1
                If (ViewAnnexe.GetRowCellValue(i, "Chemin").ToString = fichier) Then
                    SuccesMsg("Ce fichier à déjà été ajouter !")
                    Exit Sub
                End If
            Next

            Dim drS = dtAnnexe.NewRow()
            drS("Code") = IIf(ViewAnnexe.RowCount Mod 2 = 0, "x", "").ToString
            drS("RefAnnexe") = ""
            drS("Intitule annexe") = IntituleAnnexe.Text
            drS("Nom du fichier") = NomCourt
            drS("Chemin") = fichier
            dtAnnexe.Rows.Add(drS)
            GridAnnexe.DataSource = dtAnnexe
            ViewAnnexe.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
            ColorRowGrid(ViewAnnexe, "[Code]='x'", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            IntituleAnnexe.Text = ""
        End If
    End Sub

    Private Sub BtAjoutArticle_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles BtAjoutArticle.LinkClicked
        If CmbArticle.IsRequiredControl("Veuillez selectionné un article") Then
            CmbArticle.Focus()
            Exit Sub
        End If

        If TxtArticle.Text.Trim = "" Then
            SuccesMsg("Veuillez saisir la description de l'article")
            TxtArticle.Focus()
            Exit Sub
        End If

        If BtAjoutArticle.Text = "AJOUTER" Then
            IndexLignArticle = GridArticle.Rows.Add()
            GridArticle.Rows.Item(IndexLignArticle).Cells("RefArticle").Value = ""
            GridArticle.Rows.Item(IndexLignArticle).Cells("LigneModif").Value = ""
        Else
            GridArticle.Rows.Item(IndexLignArticle).Cells("LigneModif").Value = "Modifier"
        End If

        GridArticle.Rows.Item(IndexLignArticle).Cells("CodeArticle").Value = CmbArticle.Text.Replace("Article ", "")
        GridArticle.Rows.Item(IndexLignArticle).Cells("Description").Value = TxtArticle.Text

        TxtArticle.Text = ""
        BtAjoutArticle.Text = "AJOUTER"
        IndexLignArticle = 0
    End Sub

    Private Sub GridArticle_DoubleClick(sender As Object, e As EventArgs) Handles GridArticle.DoubleClick
        If GridArticle.RowCount > 0 Then
            BtAjoutArticle.Text = "MODIFIER"
            IndexLignArticle = GridArticle.CurrentRow.Index
            CmbArticle.Text = "Article " & GridArticle.Rows.Item(IndexLignArticle).Cells("CodeArticle").Value.ToString
            TxtArticle.Text = GridArticle.Rows.Item(IndexLignArticle).Cells("Description").Value.ToString
        End If
    End Sub

    Private Sub ImprimerToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try
            If (ViewAnnexe.RowCount > 0) Then

                DrX = ViewAnnexe.GetDataRow(ViewAnnexe.FocusedRowHandle)
                Dim monProcess As New Process()
                monProcess.StartInfo.FileName = DrX("Chemin").ToString
                monProcess.StartInfo.Verb = "Print"
                monProcess.StartInfo.CreateNoWindow = True
                Try
                    If File.Exists(DrX("Chemin").ToString) Then
                        monProcess.Start()
                    Else
                        FailMsg("Le fichier spécifié n'existe pas.")
                    End If
                Catch ex As Exception
                    monProcess = New Process()
                    monProcess.StartInfo.FileName = DrX("Chemin").ToString
                    If File.Exists(DrX("Chemin").ToString) Then
                        monProcess.Start()
                    Else
                        FailMsg("Le fichier spécifié n'existe pas.")
                    End If
                End Try
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If GridArticle.RowCount = 0 And ViewAnnexe.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub GridArticle_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles GridArticle.CellMouseDown
        Try
            If GridArticle.Rows.Count > 0 Then
                If e.RowIndex <> -1 And e.ColumnIndex <> -1 Then
                    If (e.Button = MouseButtons.Right) Then
                        GridArticle.CurrentCell = GridArticle.Rows(e.RowIndex).Cells(e.ColumnIndex)
                        GridArticle.Rows(e.RowIndex).Selected = True
                        GridArticle.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub GridArticle_MouseUp(sender As Object, e As MouseEventArgs) Handles GridArticle.MouseUp
        ContextMenuStrip1.Items(0).Visible = False
        ContextMenuStrip1.Items(1).Visible = False
        ContextMenuStrip1.Items(2).Visible = False
        If GridArticle.RowCount > 0 Then ContextMenuStrip1.Items(0).Visible = True
    End Sub

    Private Sub GridAnnexe_MouseDown(sender As Object, e As MouseEventArgs) Handles GridAnnexe.MouseDown
        ContextMenuStrip1.Items(0).Visible = False
        ContextMenuStrip1.Items(1).Visible = False
        ContextMenuStrip1.Items(2).Visible = False
        If ViewAnnexe.RowCount > 0 Then ContextMenuStrip1.Items(1).Visible = True
        If ViewAnnexe.RowCount > 0 Then ContextMenuStrip1.Items(2).Visible = True
    End Sub

    Private Sub ArticleSiupprimerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ArticleSiupprimerToolStripMenuItem.Click
        If GridArticle.RowCount > 0 Then
            Dim Index As Integer = GridArticle.CurrentRow.Index
            If ConfirmMsg("Voulez-vous vraiment supprimer cet article ?") = DialogResult.Yes Then
                Dim RefArcticle As String = GridArticle.Rows.Item(Index).Cells("RefArticle").Value.ToString
                GridArticle.Rows.RemoveAt(Index)

                If RefArcticle.ToString <> "" Then
                    ExecuteNonQuery("delete from t_dp_articlecontrat where RefArticle='" & RefArcticle & "' and CodeProjet='" & ProjetEnCours & "'")
                End If
                BtAjoutArticle.Text = "AJOUTER"
                IndexLignArticle = 0
            End If
        End If
    End Sub

    Private Sub OuvrirPjToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OuvrirPjToolStripMenuItem.Click
        If (ViewAnnexe.RowCount > 0) Then
            DrX = ViewAnnexe.GetDataRow(ViewAnnexe.FocusedRowHandle)
            If File.Exists(DrX("Chemin").ToString) = True Then
                Process.Start(DrX("Chemin").ToString)
            ElseIf ConfirmMsg("Le fichier que vous tentez d'ouvrir n'existe pas ou été supprimer" & vbNewLine & "Voulez-vous le supprimer de la liste ?") = DialogResult.Yes Then
                If DrX("RefAnnexe").ToString <> "" Then
                    ExecuteNonQuery("delete from t_dp_annexepj where RefAnnexe='" & DrX("RefAnnexe").ToString & "'")
                End If
                ViewAnnexe.GetDataRow(ViewAnnexe.FocusedRowHandle).Delete()
            End If
        End If
    End Sub

    Private Sub SupprimerPjToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupprimerPjToolStripMenuItem.Click
        If ViewAnnexe.RowCount > 0 Then
            DrX = ViewAnnexe.GetDataRow(ViewAnnexe.FocusedRowHandle)
            If ConfirmMsg("Voulez-vous vraiment supprimer cet annexe ?") = DialogResult.Yes Then
                If DrX("RefAnnexe").ToString <> "" Then
                    ExecuteNonQuery("delete from t_dp_annexepj where RefAnnexe='" & DrX("RefAnnexe").ToString & "'")
                End If

                ViewAnnexe.GetDataRow(ViewAnnexe.FocusedRowHandle).Delete()
            End If
        End If
    End Sub

#End Region

#Region "Impression du contrat"
    Private Sub BtImprimerContrat_Click(sender As Object, e As EventArgs) Handles BtImprimerContrat.Click
        Try
            If TablBoutonClik(5) = False Then
                ChargerListeContrat(CombContratImp)
                TablBoutonClik(5) = True
                WebBrowser3.Navigate("")
                CheminContrat = ""
                EnebledBoutonImpContrat(False)
            End If

            GetVisiblePanel(True, "ImprimerContrat")
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub EnebledBoutonImpContrat(value As Boolean)
        GeneContrat.Enabled = value
        ImpContrat.Enabled = value
        EnvoiContraBailleur.Enabled = value
        ModifImpContrat.Enabled = value
        ActuaImpContrat.Enabled = value
        PdfContratImp.Enabled = value
        wordimpContrat.Enabled = value
        BtValContrat.Enabled = value
    End Sub

    Private Sub GetValidContrat(value As Boolean)
        GeneContrat.Enabled = value
        EnvoiContraBailleur.Enabled = value
        ModifImpContrat.Enabled = value
        ActuaImpContrat.Enabled = value
        BtValContrat.Enabled = value
    End Sub

    Private Sub CombContrat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CombContratImp.SelectedIndexChanged
        Try
            EnebledBoutonImpContrat(False)
            WebBrowser3.Navigate("")
            CheminContrat = ""

            If CombContratImp.SelectedIndex <> -1 Then

                Dim dt As DataTable = ExcecuteSelectQuery("SELECT * FROM t_dp_contrat where NumContrat='" & EnleverApost(CombContratImp.Text) & "' and CodeProjet='" & ProjetEnCours & "'")
                For Each rw In dt.Rows

                    If rw("CheminContrat").ToString = "" Then
                        If GetGenererContrat() = False Then Exit Sub
                        EnebledBoutonImpContrat(True)
                    ElseIf rw("CheminContrat").ToString <> "" Then
                        Dim ChemeniContrat As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\" & FormatFileName(CombContratImp.Text, "")
                        If File.Exists(ChemeniContrat.ToString & "\" & rw("CheminContrat").ToString) Then
                            DebutChargement(True, "Chargement du contrat en cours...")
                            WebBrowser3.Navigate(ChemeniContrat.ToString & "\" & rw("CheminContrat").ToString)
                            Threading.Thread.Sleep(5000)
                            CheminContrat = rw("CheminContrat").ToString
                            FinChargement()

                        ElseIf ConfirmMsg("Le fichier spécifier n'existe pas ou a été supprimer" & vbNewLine & "Voulez-vous le regénérer à nouveau ?") = DialogResult.Yes Then
                            If GetGenererContrat() = False Then Exit Sub
                        End If

                        If rw("EtatContrat").ToString = "Valider" Then
                            GetValidContrat(False)
                            ImpContrat.Enabled = True
                            PdfContratImp.Enabled = True
                            wordimpContrat.Enabled = True
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub


    Private Sub ModifImpContrat_Click(sender As Object, e As EventArgs) Handles ModifImpContrat.Click
        Try
            If ExportContrat("modifier", "word") = False Then Exit Sub
            ModifsContrats = True
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Function ExportContrat(Textes As String, TypeFichier As String) As Boolean
        Try
            If CombContratImp.Properties.Items.Count = 0 Then
                SuccesMsg("Aucun contrat à " & Textes.ToString)
                CombContratImp.Select()
                Return False
            End If
            If CombContratImp.SelectedIndex = -1 Then
                SuccesMsg("Veuillez selectionner le contrat à " & Textes.ToString)
                CombContratImp.Select()
                Return False
            End If

            Dim ChemeniContrat As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\" & FormatFileName(CombContratImp.Text, "")
            Dim NomFichiers As String = IIf(TypeFichier = "word", "Contrat.docx", CheminContrat.ToString).ToString

            If File.Exists(ChemeniContrat & "\" & NomFichiers.ToString) Then
                DebutChargement(True, "Chargement du contrat en cours...")
                Process.Start(ChemeniContrat & "\" & NomFichiers.ToString)
                FinChargement()
                Return True
            Else
                SuccesMsg("Le fichier spécifié n'existe pas ou a été supprimé")
                Return False
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
            Return False
        End Try
    End Function

    Private Sub PdfContratImp_Click(sender As Object, e As EventArgs) Handles PdfContratImp.Click
        Try
            If CombContratImp.Properties.Items.Count = 0 Then
                SuccesMsg("Aucun contrat à exporter")
                CombContratImp.Select()
                Exit Sub
            End If
            If CombContratImp.SelectedIndex = -1 Then
                SuccesMsg("Veuillez selectionner le contrat à exporter")
                CombContratImp.Select()
                Exit Sub
            End If

            Dim ChemeniContrat As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\" & FormatFileName(CombContratImp.Text, "") & "\" & CheminContrat.ToString
            If File.Exists(ChemeniContrat) Then
                If ExporterPDF(ChemeniContrat.ToString, "Contrat.pdf") = False Then
                    Exit Sub
                End If
            Else
                SuccesMsg("Le fichier à exporter n'existe pas ou a été supprimé")
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub ImpContrat_Click(sender As Object, e As EventArgs) Handles ImpContrat.Click
        Try
            If ExportContrat("imprimer", "pdf") = False Then Exit Sub
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub EnvoiContraBailleur_Click(sender As Object, e As EventArgs) Handles EnvoiContraBailleur.Click
        Try
            If CombContratImp.Properties.Items.Count = 0 Or CheminContrat.ToString = "" Then
                SuccesMsg("Aucun contrat à envoyer au balleur de fonds")
                CombContratImp.Select()
                Exit Sub
            End If
            If CombContratImp.SelectedIndex = -1 Then
                SuccesMsg("Veuillez selectionner le contrat à envoyer au bailleur")
                CombContratImp.Select()
                Exit Sub
            End If

            Dim ChemeniContrat As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\" & FormatFileName(CombContratImp.Text, "") & "\Contrat.docx"
            If Not File.Exists(ChemeniContrat.ToString) Then
                FailMsg("Le contrat n'existe pas ou à été supprimer")
            ElseIf File.Exists(ChemeniContrat.ToString) Then
                If ChargerLesDonneEmail_AMI_DP_SERVICEAUTRES(CmbNumDoss.Text, "DP", False) = False Then
                    Exit Sub
                End If

                'Info de l'envoi de l'email
                If ConfirmMsg("Confirmez-vous l'envoi du contrat au bailleur [ " & MettreApost(rwDossDPAMISA.Rows(0)("InitialeBailleur").ToString) & " ]") = DialogResult.Yes Then
                    DebutChargement(True, "Envoi du contrat au bailleur en cours...")
                    'Envoi du rapport au bailleur
                    If EnvoiMailRapport(NomBailleurRetenu, CmbNumDoss.Text, EmailDestinatauer, ChemeniContrat.ToString, EmailCoordinateurProjet, EmailResponsablePM, "Contrat") = False Then Exit Sub
                    SuccesMsg("Le contrat a été envoyé avec succès")
                    FinChargement()
                End If
            End If
        Catch exs As IOException
            FinChargement()
            FailMsg("Un exemplaire du contrat est ouvert dans une autre application" & vbNewLine & "Veuillez le fermer svp")
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub BtValContrat_Click(sender As Object, e As EventArgs) Handles BtValContrat.Click
        Try
            If CombContratImp.Properties.Items.Count = 0 Or CheminContrat.ToString = "" Then
                SuccesMsg("Aucun contrat à valider")
                CombContratImp.Select()
                Exit Sub
            End If
            If CombContratImp.SelectedIndex = -1 Then
                SuccesMsg("Veuillez selectionner le contrat à valider")
                CombContratImp.Select()
                Exit Sub
            End If

            Dim ChemeniContrat As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\" & FormatFileName(CombContratImp.Text, "")
            If Not File.Exists(ChemeniContrat & "\" & CheminContrat.ToString) Then
                FailMsg("Le contrat que vous avez essayer de valider" & vbNewLine & "n'existe pas ou a été supprimé")
            ElseIf ConfirmMsg("La validation du contrat êmpechera sa modification" & vbNewLine & "Voulez-vous continuez ?") = DialogResult.Yes Then
                ExecuteNonQuery("Update t_dp_contrat set EtatContrat='Valider' where NumContrat='" & EnleverApost(CombContratImp.Text) & "' and CodeProjet='" & ProjetEnCours & "'")
                SuccesMsg("Contrat validé avec succès")
                GetValidContrat(False)
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub ActuaImpContrat_Click(sender As Object, e As EventArgs) Handles ActuaImpContrat.Click
        Try
            If CombContratImp.Properties.Items.Count = 0 Or CheminContrat.ToString = "" Then
                SuccesMsg("Aucun contrat à actualiser")
                CombContratImp.Select()
                Exit Sub
            End If
            If CombContratImp.SelectedIndex = -1 Then
                SuccesMsg("Veuillez selectionner le contrat à actualiser")
                CombContratImp.Select()
                Exit Sub
            End If

            If ModifsContrats = False Then
                SuccesMsg("Veuillez modifier le contrat avant d'actualiser")
                Exit Sub
            End If

            Dim ChemeniContrat As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\" & FormatFileName(CombContratImp.Text, "")
            Dim SaveContratpdf As String = "Contrat_" & FormatFileName(Now.ToString.Replace(" ", ""), "") & ".pdf"
            If Not File.Exists(ChemeniContrat.ToString & "\Contrat.doc") Then
                SuccesMsg("Le contrat à actualiser n'existe pas ou a été supprimé")
            ElseIf File.Exists(ChemeniContrat.ToString & "\Contrat.doc") Then
                DebutChargement(True, "Actualisation du contrat en cours...")

                If Directory.Exists(ChemeniContrat) = False Then Directory.CreateDirectory(ChemeniContrat)

                Dim WdApp As New Word.Application
                Dim WdDoc As New Word.Document

                Try
                    WdDoc = WdApp.Documents.Add(ChemeniContrat.ToString & "\Contrat.docx")
                    WdDoc.SaveAs2(FileName:=ChemeniContrat.ToString & "\" & SaveContratpdf.ToString, FileFormat:=Word.WdSaveFormat.wdFormatPDF)
                    WdDoc.Close(True)
                    WdApp.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                Catch ep As IO.IOException
                    FinChargement()
                    SuccesMsg("Un exemplaire du contrat est ouvert dans une autre application" & vbNewLine & "Veuillez le fermer svp.")
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

                DebutChargement(True, "Chargement du contrat en cours...")
                ExecuteNonQuery("Update t_dp_contrat set CheminContrat='" & SaveContratpdf.ToString & "' where NumContrat='" & EnleverApost(CombContratImp.Text) & "' and CodeProjet='" & ProjetEnCours & "'")
                WebBrowser3.Navigate(ChemeniContrat.ToString & "\" & SaveContratpdf.ToString)
                Threading.Thread.Sleep(5000)
                ModifsContrats = False
                CheminContrat = SaveContratpdf.ToString
                FinChargement()
            End If
        Catch exs As IOException
            FinChargement()
            SuccesMsg("Un exemplaire du contrat est ouvert dans une autre application" & vbNewLine & "Veuillez le fermer svp.")
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub wordimpContrat_Click(sender As Object, e As EventArgs) Handles wordimpContrat.Click
        Try
            If CombContratImp.Properties.Items.Count = 0 Then
                SuccesMsg("Aucun contrat à exporter")
                CombContratImp.Select()
                Exit Sub
            End If
            If CombContratImp.SelectedIndex = -1 Then
                SuccesMsg("Veuillez selectionner le contrat à exporter")
                CombContratImp.Select()
                Exit Sub
            End If

            Dim ChemeniContrat As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\" & FormatFileName(CombContratImp.Text, "") & "\Contrat.docx"
            If File.Exists(ChemeniContrat) Then
                If ExporterWORDfOrmatDocx(ChemeniContrat.ToString, "Contrat001.docx") = False Then
                    Exit Sub
                End If
            Else
                SuccesMsg("Le fichier à exporter n'existe pas ou a été supprimé")
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub GeneContrat_Click(sender As Object, e As EventArgs) Handles GeneContrat.Click
        Try
            If CombContratImp.Properties.Items.Count = 0 Then
                SuccesMsg("Aucun contrat à générer")
                CombContratImp.Select()
                Exit Sub
            End If

            If CombContratImp.SelectedIndex = -1 Then
                SuccesMsg("Veuillez selectionner un contrat dans la liste")
                CombContratImp.Select()
                Exit Sub
            End If
            If GetGenererContrat() = False Then Exit Sub
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Function GetGenererContrat() As Boolean
        SuccesMsg("Etat en cours de réalisation")
        Return False
        Try
            DebutChargement(True, "Génération du contrat en cours...")
            ExecuteNonQuery("delete from t_dp_tamparticlecontrat where CodeOperateur='" & CodeOperateurEnCours & "' and CodeProjet='" & ProjetEnCours & "'")
            'Enregistrement des article
            EnregistreArticle()

            Dim Contrat, Contrat1, Contrat2, Contrat3, Contrat4, Contrat5 As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table

            Dim DatSet = New DataSet

            Dim Chemin As String = lineEtat & "\Marches\DP\Contrats\"
            Dim TypeRenumeration As String = ""
            If TypeDossier(CmbNumDoss.Text) = "AMI" Then
                TypeRenumeration = ExecuteScallar("select TypeRemune from t_ami where NumeroDAMI='" & EnleverApost(CmbNumDoss.Text) & "'")
            Else
                TypeRenumeration = ExecuteScallar("select TypeRemune from t_dp where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'")
            End If

            If TypeRenumeration = "Temps passé" Then
                Contrat.Load(Chemin & "\Temps passé\Contrat_Dp_TempsPasse.rpt")
                'Contrat1.Load(Chemin & "\Temps passé\Contrat_Dp_TempsPasse.rpt")
                'Contrat2.Load(Chemin & "\Temps passé\Contrat_Dp_TempsPasse.rpt")
                'Contrat3.Load(Chemin & "\Temps passé\Contrat_Dp_TempsPasse.rpt")
                'Contrat4.Load(Chemin & "\Temps passé\Contrat_Dp_TempsPasse.rpt")
                'Contrat5.Load(Chemin & "\Temps passé\Contrat_Dp_TempsPasse.rpt")
            Else
            End If

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = Contrat.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            Contrat.SetDataSource(DatSet)

            Contrat.SetParameterValue("CodeProjet", ProjetEnCours)
            Contrat.SetParameterValue("NumeroContrat", EnleverApost(CombContratImp.Text))
            Contrat.SetParameterValue("NumeroDp", EnleverApost(CmbNumDoss.Text))
            ' Contrat.SetParameterValue("CodeOperateur", CodeOperateurEnCours)

            'Dim rwDevise As DataRow = ExcecuteSelectQuery("SELECT s.MontantOffresLocal,s.MontantAjusterLocal from t_soumissionconsultant as s, t_marchesigne as m where m.RefSoumis=s.RefSoumis and m.NumeroMarche='" & EnleverApost(CombContratImp.Text) & "' and s.NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "'").Rows(0)
            'Dim MontantDevisePropo As Decimal = Val(Mid(rwDevise("MontantAjuster").ToString, 3))
            'If Mid(rwDevise("MontantAjuster"), 1, 1) = "+" Then
            '    MontantDevisePropo = Val(rwDevise("MontantPropose")) + MontantDevisePropo
            'Else
            '    MontantDevisePropo = Val(rwDevise("MontantPropose")) - MontantDevisePropo
            'End If

            'Contrat.SetParameterValue("MontantDevisePropo", AfficherMonnaie(MontantDevisePropo))

            'Annexe.SetParameterValue("CodeProjet", ProjetEnCours)
            'Annexe.SetParameterValue("NumeroContrat", EnleverApost(CombContratImp.Text))

            Dim CheminDocTDR As String = ExecuteScallar("select CheminDocTDR from t_dp where NumeroDp='" & EnleverApost(CmbNumDoss.Text) & "' and CodeProjet='" & ProjetEnCours & "'")

            Dim Chemin1 As String = Path.GetTempFileName & ".doc"
            Dim Chemin2 As String = Path.GetTempFileName & ".doc"
            Contrat.ExportToDisk(ExportFormatType.WordForWindows, Chemin1)
            'Annexe.ExportToDisk(ExportFormatType.WordForWindows, Chemin2)

            Dim NomDossier As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\" & FormatFileName(CombContratImp.Text, "")
            If (Directory.Exists(NomDossier) = False) Then
                Directory.CreateDirectory(NomDossier)
            End If

            Dim oWord As New Word.Application
            Try
                Dim currentDoc As New Word.Document

                currentDoc = oWord.Documents.Add(Chemin1)
                Dim myRange As Word.Range = currentDoc.Bookmarks.Item("\endofdoc").Range
                Dim mySection1 As Word.Section = AjouterNouvelleSectionDocument(currentDoc, myRange)
                'Insertion des TDR
                If CheminDocTDR.ToString <> "" Then
                    Dim CheminTDR As String = line & "\DP\" & FormatFileName(CmbNumDoss.Text, "_") & "\TDR1.Rtf"
                    myRange.InsertFile(CheminTDR)
                End If
                If CheminDocTDR.ToString <> "" Then
                    mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                End If
                myRange.InsertFile(Chemin2)

                Dim SaveNomPdf As String = "Contrat_" & FormatFileName(Now.ToString.Replace(" ", ""), "") & ".pdf"

                currentDoc.SaveAs2(FileName:=NomDossier & "\Contrat.docx", FileFormat:=Word.WdSaveFormat.wdFormatDocumentDefault)
                currentDoc.SaveAs2(FileName:=NomDossier & "\" & SaveNomPdf.ToString, FileFormat:=Word.WdSaveFormat.wdFormatPDF)
                oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)

                ExecuteNonQuery("update t_dp_contrat set CheminContrat='" & SaveNomPdf.ToString & "' where NumContrat='" & EnleverApost(CombContratImp.Text) & "' and CodeProjet='" & ProjetEnCours & "'")
                CheminContrat = SaveNomPdf.ToString
            Catch exs As IOException
                FinChargement()
                SuccesMsg("Un exemplaire du contrat est ouvert dans une autre application" & vbNewLine & "Veuillez le fermer svp.")
                oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                Return False
            Catch ex As Exception
                FinChargement()
                FailMsg("Erreur de traitement " & ex.ToString)
                oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                Return False
            End Try

            FinChargement()
            DebutChargement(True, "Chargement du contrat en cours...")
            Process.Start(NomDossier & "\" & CheminContrat.ToString)
            Threading.Thread.Sleep(5000)
            FinChargement()
            Return True

        Catch exd As IOException
            FinChargement()
            SuccesMsg("Un exemplaire du contrat est ouvert dans une autre application" & vbNewLine & "Veuillez le fermer svp.")
            Return False
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
            Return False
        End Try
    End Function

    Private Sub EnregistreArticle()
        Try
            Dim dt0 As DataTable
            Dim TabCodeArticle As Array = {"2.1", "2.2", "2.3", "2.4", "3.5(a)", "3.5(b)", "3.5(c)", "3.5(d)", "3.5(E)", "3.9", "6.4(a)(1)", "6.4(a)(2)", "6.4(b)", "6.4(c)", "8.2"}
            For i = 0 To 14
                query = "SELECT DescriptionArticle from t_dp_articlecontrat where CodeArticle='" & TabCodeArticle(i) & "' and NumeroContrat='" & EnleverApost(NumContrat.Text) & "' and CodeProjet ='" & ProjetEnCours & "'"
                dt0 = ExcecuteSelectQuery(query)
                If dt0.Rows.Count > 0 Then
                    For Each rw0 In dt0.Rows
                        ExecuteNonQuery("Insert into t_dp_tamparticlecontrat values('" & EnleverApost(NumContrat.Text) & "', '" & TabCodeArticle(i) & "', '" & EnleverApost(rw0("DescriptionArticle").ToString) & "', '" & CodeOperateurEnCours & "', '" & ProjetEnCours & "')")
                    Next
                Else
                    ExecuteNonQuery("Insert into t_dp_tamparticlecontrat values('" & EnleverApost(NumContrat.Text) & "', '" & TabCodeArticle(i) & "', 'Sans Objet', '" & CodeOperateurEnCours & "', '" & ProjetEnCours & "')")
                End If
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub
#End Region


    Private Sub Disqualification_CheckedChanged(sender As Object, e As EventArgs) Handles Disqualification.CheckedChanged
        If Disqualification.Checked = True Then
            MotifDisqualif.Enabled = True
        Else
            MotifDisqualif.Enabled = False
        End If
    End Sub

End Class