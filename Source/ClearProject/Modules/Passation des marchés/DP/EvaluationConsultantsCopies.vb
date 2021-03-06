Imports MySql.Data.MySqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO

Public Class EvaluationConsultantsCopies

    Public EtapeActuelle As String = ""
    Dim dtComm = New DataTable()
    Dim dtMoy = New DataTable()
    Dim dtNote = New DataTable()
    Dim dtFinance = New DataTable()
    Dim dtBilanCons = New DataTable()
    Dim ActiverNote As Boolean = False
    Dim DernierEval As String = ""
    Public CodeEvaluateur As String = ""
    Dim SoumisEnCours As String = ""
    Dim DrX As DataRow

    Private Sub EvaluationConsultants_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        PnlEditionMarche.Visible = False
        ChargerDossier()
        TxtTypeExamen.Text = ""
    End Sub

    Private Sub ChargerDossier()
        query = "select NumeroDp from T_DP where DateFinOuverture<>'' and CodeProjet='" & ProjetEnCours & "' order by NumeroDp"
        CmbNumDoss.Properties.Items.Clear()
        CmbNumDoss.Text = ""
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbNumDoss.Properties.Items.Add(MettreApost(rw("NumeroDp").ToString))
        Next
    End Sub

    Private Sub EvaluationConsultants_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub CmbNumDoss_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumDoss.SelectedValueChanged

        EtapeActuelle = ""
        EtapeTechnique.ImageIndex = 1
        EtapeFinanciere.ImageIndex = 1

        EtapeTechnique.ForeColor = Color.Silver
        EtapeFinanciere.ForeColor = Color.Silver

        TxtTypeExamen.Text = ""

        GbCojo.Enabled = False
        BtNoter.Visible = False
        BtAction.Text = ""
        BtAction.Enabled = True
        GbMarche.Enabled = False

        BtPtsFortsFaibles.Visible = False

        query = "select LibelleMiss,MethodeSelection,DateFinOuverture,EvalTechnique,EvalFinanciere from T_DP where NumeroDp='" & CmbNumDoss.Text & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows

            TxtLibelleDoss.Text = MettreApost(rw("LibelleMiss").ToString)
            TxtDateOuvert.Text = CDate(rw("DateFinOuverture").ToString).ToShortDateString
            TxtMethode.Text = rw("MethodeSelection").ToString
            GbCojo.Enabled = True

            EtapeActuelle = "Technique"
            BtAction.Text = "VALIDER L'EVALUATION TECHNIQUE"
            TxtTypeExamen.Text = "EVALUATION TECHNIQUE"
            If (rw("EvalTechnique").ToString <> "") Then
                EtapeTechnique.ImageIndex = 0
                EtapeActuelle = "Finance"
                EtapeTechnique.ForeColor = Color.Black
                TxtTypeExamen.Text = "EVALUATION FINANCIERE"
                BtAction.Text = "VALIDER L'EVALUATION FINANCIERE"

                If (rw("EvalFinanciere").ToString <> "") Then
                    EtapeFinanciere.ImageIndex = 0
                    EtapeActuelle = "Terminé"
                    EtapeFinanciere.ForeColor = Color.Black
                    TxtTypeExamen.Text = "SCORES PONDERES DES EVALUATIONS TECHNIQUE ET FINANCIERE"
                    BtAction.Text = "AFFICHER LE RAPPORT COMBINE"
                    BtPtsFortsFaibles.Visible = True

                    'If (rw(5).ToString <> "") Then
                    '    EtapePostQualif.ImageIndex = 0
                    '    EtapeActuelle = "Termine"
                    '    EtapePostQualif.ForeColor = Color.Black
                    '    TxtTypeExamen.Text = "BILAN DE L'EVALUATION DES CONSULTANTS"
                    '    BtAction.Text = "RAPPORT D'EVALUATION"

                    'Else
                    '    EtapePostQualif.ImageIndex = 2
                    '    EtapePostQualif.ForeColor = Color.Black
                    'End If

                Else
                    EtapeFinanciere.ImageIndex = 2
                    EtapeFinanciere.ForeColor = Color.Black
                End If

            Else
                EtapeTechnique.ImageIndex = 2
                EtapeTechnique.ForeColor = Color.Black
                'BtNoter.Visible = True
                'BtNoter.Enabled = True
            End If
        Next

        AfficherGrid()
        RemplirCojo()

        If (EtapeActuelle = "Technique") Then
            If (ActiverNote = False) Then
                ClassementMoy()
                RemplirMoyenne()
            Else
                RemplirNote()
            End If
        ElseIf (EtapeActuelle = "Finance") Then
            RemplirOffreFinanciere()

        ElseIf (EtapeActuelle = "Terminé") Then
            RemplirBilanEvalConsult()

            GbMarche.Enabled = True
            BtAction.Enabled = True
        End If

    End Sub

    Private Sub TxtCodePresence_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtCodePresence.KeyDown
        If (e.KeyCode = Keys.Enter And CmbNumDoss.Text <> "") Then
            If (TxtCodePresence.Text <> "") Then
                Dim CodMembre As String = ""
                query = "select CodeMem,Evaluation from T_Commission where NumeroDAO='" & CmbNumDoss.Text & "' and TypeComm='EVAC' and PasseMem='" & TxtCodePresence.Text & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    If (rw(1).ToString <> "") Then
                        MsgBox("Code déjà entré!", MsgBoxStyle.Information)
                        TxtCodePresence.Text = ""
                        Exit Sub
                    End If
                    CodMembre = rw(0).ToString
                Next
                If (CodMembre <> "") Then

                    'DernierEval = CodMembre

                    Dim DatSet = New DataSet
                    query = "select * from T_Commission where CodeMem='" & CodMembre & "'"
                    Dim sqlconn As New MySqlConnection
                    BDOPEN(sqlconn)
                    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                    Dim DatAdapt = New MySqlDataAdapter(Cmd)
                    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                    DatAdapt.Fill(DatSet, "T_Commission")

                    DatSet.Tables!T_Commission.Rows(0)!Evaluation = Now.ToShortDateString & " " & Now.ToLongTimeString

                    DatAdapt.Update(DatSet, "T_Commission")
                    DatSet.Clear()
                    BDQUIT(sqlconn)
                    TxtCodePresence.Text = ""
                    RemplirCojo()
                Else
                    MsgBox("Accès réfusé!", MsgBoxStyle.Critical)
                End If

            End If

        End If
    End Sub

    Private Sub RemplirCojo()
        If (CmbNumDoss.Text <> "") Then

            dtComm.Columns.Clear()

            dtComm.Columns.Add("Commission", Type.GetType("System.String"))

            dtComm.Rows.Clear()
            query = "select NomMem,TitreMem,CodeMem from T_Commission where NumeroDAO='" & CmbNumDoss.Text & "' and TypeComm='EVAC' and Evaluation<>''"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                Dim drS = dtComm.NewRow()

                DernierEval = rw("CodeMem").ToString
                drS(0) = MettreApost(rw("NomMem").ToString) & " (" & rw("TitreMem").ToString & ")"

                dtComm.Rows.Add(drS)
            Next

            GridCojo.DataSource = dtComm

            If (GridViewComJugmt.RowCount > 0) Then
                SplitContainerControl1.Panel2.Enabled = True
            Else
                SplitContainerControl1.Panel2.Enabled = False
            End If
        End If
    End Sub

    Private Sub AfficherGrid()

        PnlEditionMarche.Visible = False
        If (EtapeActuelle = "Technique") Then
            GridMoyenne.Visible = Not ActiverNote
            GridNote.Visible = ActiverNote
            GridOffreFinance.Visible = False
            GridBilanCons.Visible = False

        ElseIf (EtapeActuelle = "Finance") Then
            GridMoyenne.Visible = False
            GridNote.Visible = False
            GridOffreFinance.Visible = True
            GridBilanCons.Visible = False

        ElseIf (EtapeActuelle = "Terminé") Then
            GridMoyenne.Visible = False
            GridNote.Visible = False
            GridOffreFinance.Visible = False
            GridBilanCons.Visible = True

        Else
            GridMoyenne.Visible = False
            GridNote.Visible = False
            GridOffreFinance.Visible = False
            GridBilanCons.Visible = False

        End If
    End Sub

    Private Sub RemplirMoyenne()

        Dim codEval(10) As String
        Dim nbEval As Integer = 0

        dtMoy.Columns.Clear()

        dtMoy.Columns.Add("Code", Type.GetType("System.String"))
        dtMoy.Columns.Add("CodeX", Type.GetType("System.String"))
        dtMoy.Columns.Add("Cabinet", Type.GetType("System.String"))
        query = "select CodeMem,NomMem from T_Commission where NumeroDAO='" & CmbNumDoss.Text & "' and TypeComm='EVAC' order by CodeMem"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            codEval(nbEval) = rw("CodeMem").ToString
            dtMoy.Columns.Add("Note de " & MettreApost(rw("NomMem").ToString), Type.GetType("System.String"))
            nbEval += 1
        Next

        dtMoy.Columns.Add("Moyenne", Type.GetType("System.String"))
        dtMoy.Columns.Add("Rang", Type.GetType("System.String"))
        dtMoy.Columns.Add("Décision", Type.GetType("System.String"))

        Dim cpt2 As Decimal = 0
        dtMoy.Rows.Clear()

        query = "select S.RefSoumis,C.NomConsult,S.NoteConsult,S.ReferenceNote,S.RangConsult,S.EvalTechOk from T_Consultant as C,T_SoumissionConsultant as S where S.RefConsult=C.RefConsult and S.NumeroDp='" & CmbNumDoss.Text & "' order by S.RangConsult,C.NomConsult"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cpt2 += 1
            Dim DrE = dtMoy.NewRow()

            DrE(0) = rw("RefSoumis").ToString
            DrE(1) = IIf(CDec(cpt2 / 2) = CDec(cpt2 \ 2), "x", "")
            DrE(2) = MettreApost(rw("NomConsult").ToString)

            For k As Integer = 0 To nbEval - 1
                DrE(k + 3) = Points(rw("RefSoumis").ToString, codEval(k)).Split("/"c)(0).Replace(" ", "")
            Next

            DrE(nbEval + 3) = IIf(rw("NoteConsult").ToString <> "", rw("NoteConsult").ToString, "0").ToString & " / " & IIf(rw("ReferenceNote").ToString <> "", rw("ReferenceNote").ToString, "0").ToString
            DrE(nbEval + 4) = IIf(rw("RangConsult").ToString <> "0", rw("RangConsult").ToString & IIf(rw("RangConsult").ToString = "1", "er", "ème").ToString, "-").ToString
            DrE(nbEval + 5) = IIf(rw("EvalTechOk").ToString <> "", IIf(rw("EvalTechOk").ToString = "OUI", "ACCEPTE", "REFUSE").ToString, "-").ToString

            dtMoy.Rows.Add(DrE)
        Next
        GridMoyenne.DataSource = dtMoy

        ViewMoyenne.Columns(0).Visible = False
        ViewMoyenne.Columns(1).Visible = False
        ViewMoyenne.Columns(2).Width = 350

        For k As Integer = 0 To nbEval - 1
            ViewMoyenne.Columns(k + 3).Width = 120
            ViewMoyenne.Columns(k + 3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Next

        ViewMoyenne.Columns(nbEval + 3).Width = 150
        ViewMoyenne.Columns(nbEval + 4).Width = 50
        ViewMoyenne.Columns(nbEval + 5).Width = 120

        ViewMoyenne.Columns(nbEval + 3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewMoyenne.Columns(nbEval + 4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewMoyenne.Columns(nbEval + 5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        ViewMoyenne.Columns(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        ViewMoyenne.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        ViewMoyenne.Columns(2).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        ViewMoyenne.Columns(nbEval + 5).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        ViewMoyenne.Columns(nbEval + 4).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        ViewMoyenne.Columns(nbEval + 3).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right

        ColorRowGrid(ViewMoyenne, "[CodeX]='x'", Color.LightGray, "Tahoma", 10, FontStyle.Regular, Color.Black)
        ColorRowGridAnal(ViewMoyenne, "[Décision]='REFUSE'", Color.White, "Tahoma", 10, FontStyle.Regular, Color.Red, False)

        If (ViewMoyenne.RowCount > 0) Then
            BtNoter.Visible = True
        Else
            BtNoter.Visible = False
        End If

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

    Private Sub BtNoter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtNoter.Click

        If (GridViewComJugmt.RowCount = 1) Then
            CodeEvaluateur = DernierEval
            'MsgBox("Eval=" & CodeEvaluateur, MsgBoxStyle.Information)
        Else
            ReponseDialog = ""
            ExceptRevue = ""
            ExceptRevue2 = ""
            CodeEvaluateur = ""
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
                            CodeEvaluateur = dt.Rows(0).Item(2).ToString
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

        ActiverNote = True
        BtNoter.Visible = False
        LblEvaluateurEnCours.Text = "Evaluateur en cours : " & NomDe(CodeEvaluateur)

        AfficherGrid()
        RemplirNote()

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

    Public Sub RemplirNote()

        dtNote.Columns.Clear()

        dtNote.Columns.Add("Code", Type.GetType("System.String"))
        dtNote.Columns.Add("CodeX", Type.GetType("System.String"))
        dtNote.Columns.Add("Cabinet", Type.GetType("System.String"))
        dtNote.Columns.Add("Points obtenus", Type.GetType("System.String"))

        Dim cpt2 As Decimal = 0
        dtNote.Rows.Clear()
        query = "select S.RefSoumis,C.NomConsult,S.NoteConsult from T_Consultant as C,T_SoumissionConsultant as S where S.RefConsult=C.RefConsult and C.NumeroDp='" & CmbNumDoss.Text & "' order by C.NomConsult"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cpt2 += 1
            Dim DrE = dtNote.NewRow()

            DrE(0) = rw(0).ToString
            DrE(1) = IIf(CDec(cpt2 / 2) = CDec(cpt2 \ 2), "x", "")
            DrE(2) = MettreApost(rw(1).ToString)
            DrE(3) = Points(rw(0).ToString, CodeEvaluateur)

            dtNote.Rows.Add(DrE)
        Next

        GridNote.DataSource = dtNote

        ViewNote.Columns(0).Visible = False
        ViewNote.Columns(1).Visible = False
        ViewNote.Columns(2).Width = GridNote.Width - 168
        ViewNote.Columns(3).Width = 150
        ViewNote.Columns(3).AppearanceCell.Font = New Font("Tahoma", 8, FontStyle.Bold)

        ViewNote.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

        ColorRowGrid(ViewNote, "[CodeX]='x'", Color.LightGray, "Tahoma", 10, FontStyle.Regular, Color.Black)
        'ColorRowGridAnal(ViewNote, "[Décision]='REFUSE'", Color.White, "Tahoma", 10, FontStyle.Regular, Color.Red, False)

        If (ViewNote.RowCount > 0) Then
            BtEnrgNotes.Visible = True
            PnlEvalEnCours.Visible = True
        Else
            BtEnrgNotes.Visible = False
            BtNoter.Visible = True
            AfficherGrid()
            RemplirMoyenne()
            PnlEvalEnCours.Visible = False
        End If

    End Sub
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

        BtAction.Enabled = True
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

    Private Sub BtEnrgNotes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgNotes.Click

        MAJ_Pts_Soumis()
        ActiverNote = False
        AfficherGrid()
        BtNoter.Visible = True
        BtEnrgNotes.Visible = False
        LblEvaluateurEnCours.Text = ""
        PnlEvalEnCours.Visible = False
        RemplirMoyenne()


    End Sub

    Private Sub EvaluerCriteres_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EvaluerCriteres.Click
        If (ViewNote.RowCount > 0) Then
            DrX = ViewNote.GetDataRow(ViewNote.FocusedRowHandle)

            ExceptRevue = DrX(0).ToString
            ReponseDialog = DrX(2).ToString
            Dialog_form(FicheEvaluation)
            RemplirNote()
        End If
    End Sub

    Private Sub BtAction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAction.Click

        If (BtAction.Text = "VALIDER L'EVALUATION TECHNIQUE") Then
            EvalValidationMoyenne.ShowDialog()
            CmbNumDoss_SelectedValueChanged(Me, e)

        ElseIf (BtAction.Text = "VALIDER L'EVALUATION FINANCIERE") Then
            Dim RepMsg As MsgBoxResult = MsgBox("Veuillez confirmer la clôture de l'évaluation financière.", MsgBoxStyle.OkCancel)
            If (RepMsg = MsgBoxResult.Ok) Then

                Dim DatSet = New DataSet
                query = "select * from T_DP where NumeroDp='" & CmbNumDoss.Text & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Fill(DatSet, "T_DP")

                DatSet.Tables!T_DP.Rows(0)!EvalFinanciere = Now.ToShortDateString & " " & Now.ToLongTimeString

                DatAdapt.Update(DatSet, "T_DP")
                DatSet.Clear()
                BDQUIT(sqlconn)
                CmbNumDoss_SelectedValueChanged(Me, e)
            End If

        ElseIf (BtAction.Text = "AFFICHER LE RAPPORT COMBINE") Then

            BtAfficheRapport_Click(Me, e)

        End If

    End Sub

    Private Sub CalculerScoreFinancier()
        query = "select * from T_Consultant as C,T_SoumissionConsultant as S where S.RefConsult=C.RefConsult and C.NumeroDp='" & CmbNumDoss.Text & "' and S.EvalTechOk='OUI' and S.MontantPropose=''"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            Exit Sub
        End If


        'Recup des infos
        Dim LesScore(20) As String
        Dim LesRef(20) As String
        Dim LesMont(20) As Decimal
        Dim LesNote(20) As Decimal
        Dim LesMoyPond(20) As Decimal
        Dim NbRef As Decimal = 0
        Dim Tamp As String = ""
        Dim TampDec As Decimal = 0
        query = "select S.RefSoumis,S.MontantPropose,S.NoteConsult from T_Consultant as C,T_SoumissionConsultant as S where S.RefConsult=C.RefConsult and C.NumeroDp='" & CmbNumDoss.Text & "' and S.EvalTechOk='OUI'"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            LesRef(NbRef) = rw("RefSoumis").ToString
            LesMont(NbRef) = CDec(rw("MontantPropose"))
            LesNote(NbRef) = CDec(rw("NoteConsult"))
            NbRef += 1
        Next


        'Les coefficients
        Dim CoefTech As Decimal = 1
        Dim CoefFin As Decimal = 1
        query = "select PoidsTech,PoidsFin from T_DP where NumeroDp='" & CmbNumDoss.Text & "' and CodeProjet='" & ProjetEnCours & "'"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CoefTech = CDec(rw(0))
            CoefFin = CDec(rw(1))
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
            LesScore(k) = Math.Round((100 / LesMont(k)) * MontMin, 2).ToString
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
            Dim DatSet = New DataSet
            query = "select * from T_SoumissionConsultant where RefSoumis='" & LesRef(k) & "'"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Fill(DatSet, "T_SoumissionConsultant")

            DatSet.Tables!T_SoumissionConsultant.Rows(0)!ScoreFinancier = LesScore(k).Replace(",00", "")
            DatSet.Tables!T_SoumissionConsultant.Rows(0)!MoyPonderee = Math.Round(LesMoyPond(k), 2).ToString.Replace(",00", "")
            DatSet.Tables!T_SoumissionConsultant.Rows(0)!RangFinal = (k + 1).ToString

            DatAdapt.Update(DatSet, "T_SoumissionConsultant")
            DatSet.Clear()
            BDQUIT(sqlconn)
        Next

        BtAction.Enabled = True

    End Sub

    Private Sub RemplirOffreFinanciere()
        DebutChargement(True, "Traitement des offres en cours...")

        BtAction.Enabled = False
        CalculerScoreFinancier()

        dtFinance.Columns.Clear()

        dtFinance.Columns.Add("Code", Type.GetType("System.String"))
        dtFinance.Columns.Add("CodeX", Type.GetType("System.String"))
        dtFinance.Columns.Add("Cabinet", Type.GetType("System.String"))
        dtFinance.Columns.Add("Offre financière", Type.GetType("System.String"))
        dtFinance.Columns.Add("Score financier", Type.GetType("System.String"))

        Dim cpt2 As Decimal = 0
        query = "select S.RefSoumis,C.NomConsult,S.MontantPropose,S.ScoreFinancier from T_Consultant as C,T_SoumissionConsultant as S where S.RefConsult=C.RefConsult and C.NumeroDp='" & CmbNumDoss.Text & "' and S.EvalTechOk='OUI' order by C.NomConsult"
        dtFinance.Rows.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cpt2 += 1
            Dim DrE = dtFinance.NewRow()

            DrE(0) = rw(0).ToString
            DrE(1) = IIf(CDec(cpt2 / 2) = CDec(cpt2 \ 2), "x", "")
            DrE(2) = MettreApost(rw(1).ToString)
            DrE(3) = IIf(rw(2).ToString <> "", AfficherMonnaie(rw(2).ToString) & " HT (FCFA)", "0").ToString
            DrE(4) = IIf(rw(3).ToString <> "", rw(3).ToString, "-").ToString

            dtFinance.Rows.Add(DrE)
            'MsgBox(cpt2.ToString, MsgBoxStyle.Information)
        Next

        GridOffreFinance.DataSource = dtFinance

        ViewOffreFinance.Columns(0).Visible = False
        ViewOffreFinance.Columns(1).Visible = False
        ViewOffreFinance.Columns(2).Width = GridOffreFinance.Width - 368
        ViewOffreFinance.Columns(3).Width = 200
        ViewOffreFinance.Columns(4).Width = 150

        ViewOffreFinance.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewOffreFinance.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

        ColorRowGrid(ViewOffreFinance, "[CodeX]='x'", Color.LightGray, "Tahoma", 10, FontStyle.Regular, Color.Black)
        'ColorRowGridAnal(ViewOffreFinance, "[Décision]='REFUSE'", Color.White, "Tahoma", 10, FontStyle.Regular, Color.Red, False)

        'If (ViewOffreFinance.RowCount > 0) Then
        '    BtNoter.Visible = True
        'Else
        BtNoter.Visible = False
        'End If

        FinChargement()
    End Sub

    Private Sub GridNote_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridNote.DoubleClick
        EvaluerCriteres_Click(Me, e)
    End Sub

    Private Sub SaisirOffre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaisirOffre.Click

        If (ViewOffreFinance.RowCount > 0) Then
            DrX = ViewOffreFinance.GetDataRow(ViewOffreFinance.FocusedRowHandle)

            ExceptRevue = DrX(0).ToString
            ReponseDialog = DrX(2).ToString
            'EvalOffreFinanciere.ShowDialog()
            RemplirOffreFinanciere()
        End If

    End Sub

    Private Sub RemplirBilanEvalConsult()
        DebutChargement(True, "Traitement des offres en cours...")

        Dim codEval(10) As String
        Dim nbEval As Integer = 0

        dtBilanCons.Columns.Clear()

        dtBilanCons.Columns.Add("Code", Type.GetType("System.String"))
        dtBilanCons.Columns.Add("CodeX", Type.GetType("System.String"))
        dtBilanCons.Columns.Add("Cabinet", Type.GetType("System.String"))
        'dtBilanCons.Columns.Add("Offre financière", Type.GetType("System.String"))
        'dtBilanCons.Columns.Add("Score financier", Type.GetType("System.String"))

        query = "select CodeMem,NomMem from T_Commission where NumeroDAO='" & CmbNumDoss.Text & "' and TypeComm='EVAC' order by CodeMem"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            codEval(nbEval) = rw(0).ToString
            dtBilanCons.Columns.Add("Note de " & MettreApost(rw(1).ToString), Type.GetType("System.String"))
            nbEval += 1
        Next

        dtBilanCons.Columns.Add("Score Technique (T)", Type.GetType("System.String"))
        dtBilanCons.Columns.Add("Offre financière", Type.GetType("System.String"))
        dtBilanCons.Columns.Add("Score Financier (F)", Type.GetType("System.String"))

        'Les coefficients
        Dim CoefTech As String = ""
        Dim CoefFin As String = ""
        query = "select PoidsTech,PoidsFin from T_DP where NumeroDp='" & CmbNumDoss.Text & "' and CodeProjet='" & ProjetEnCours & "'"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CoefTech = rw(0).ToString
            CoefFin = rw(1).ToString
        Next

        dtBilanCons.Columns.Add("Score Pondéré (P=T x " & CoefTech & " & F x " & CoefFin & ")", Type.GetType("System.String"))
        dtBilanCons.Columns.Add("Rang", Type.GetType("System.String"))
        Dim cpt2 As Decimal = 0
        dtBilanCons.Rows.Clear()
        query = "select S.RefSoumis,C.NomConsult,S.NoteConsult,S.MontantPropose,S.ScoreFinancier,S.MoyPonderee,S.RangFinal from T_Consultant as C,T_SoumissionConsultant as S where S.RefConsult=C.RefConsult and C.NumeroDp='" & CmbNumDoss.Text & "' and S.EvalTechOk='OUI' order by S.RangFinal"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cpt2 += 1
            Dim DrE = dtBilanCons.NewRow()

            DrE(0) = rw(0).ToString
            DrE(1) = IIf(CDec(cpt2 / 2) = CDec(cpt2 \ 2), "x", "")
            DrE(2) = MettreApost(rw(1).ToString)
            For k As Integer = 0 To nbEval - 1
                DrE(k + 3) = Points(rw(0).ToString, codEval(k)).Split("/"c)(0).Replace(" ", "")
            Next
            DrE(nbEval + 3) = rw(2).ToString
            DrE(nbEval + 4) = AfficherMonnaie(rw(3).ToString)
            DrE(nbEval + 5) = rw(4).ToString
            DrE(nbEval + 6) = rw(5).ToString
            DrE(nbEval + 7) = rw(6).ToString & IIf(rw(6).ToString = "1", "er", "ème").ToString

            dtBilanCons.Rows.Add(DrE)
        Next
        GridBilanCons.DataSource = dtBilanCons

        ViewBilanCons.Columns(0).Visible = False
        ViewBilanCons.Columns(1).Visible = False
        ViewBilanCons.Columns(2).Width = 250
        For k As Integer = 0 To nbEval - 1
            ViewBilanCons.Columns(k + 3).Width = 100
            ViewBilanCons.Columns(k + 3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Next
        ViewBilanCons.Columns(nbEval + 3).Width = 120
        ViewBilanCons.Columns(nbEval + 4).Width = 150
        ViewBilanCons.Columns(nbEval + 5).Width = 120
        ViewBilanCons.Columns(nbEval + 6).Width = 200
        ViewBilanCons.Columns(nbEval + 7).Width = 60

        ViewBilanCons.Columns(nbEval + 3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewBilanCons.Columns(nbEval + 4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewBilanCons.Columns(nbEval + 5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewBilanCons.Columns(nbEval + 6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewBilanCons.Columns(nbEval + 7).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

        ViewBilanCons.Columns(nbEval + 3).AppearanceCell.Font = New Font("Tahoma", 10, FontStyle.Bold)
        ViewBilanCons.Columns(nbEval + 5).AppearanceCell.Font = New Font("Tahoma", 10, FontStyle.Bold)
        ViewBilanCons.Columns(nbEval + 6).AppearanceCell.Font = New Font("Tahoma", 10, FontStyle.Bold)

        ViewBilanCons.Columns(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        ViewBilanCons.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        ViewBilanCons.Columns(2).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        ViewBilanCons.Columns(nbEval + 7).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right

        ColorRowGrid(ViewBilanCons, "[CodeX]='x'", Color.LightGray, "Tahoma", 10, FontStyle.Regular, Color.Black)
        'ColorRowGridAnal(ViewBilanCons, "[Décision]='REFUSE'", Color.White, "Tahoma", 10, FontStyle.Regular, Color.Red, False)

        'If (ViewBilanCons.RowCount > 0) Then
        '    BtNoter.Visible = True
        'Else
        BtNoter.Visible = False
        'End If

        FinChargement()
    End Sub

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

        query = "DELETE from T_TampConsultRangTech"
        ExecuteNonQuery(query)

        Dim DatSet = New DataSet
        query = "select * from T_TampConsultRangTech"
        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)
        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd)
        DatAdapt.Fill(DatSet, "T_TampConsultRangTech")
        Dim DatTable = DatSet.Tables("T_TampConsultRangTech")
        Dim DatRow = DatSet.Tables("T_TampConsultRangTech").NewRow()
        For k As Integer = 0 To xNbSoumis - 1
            DatRow("Nom" & (k + 1).ToString) = xListeSoumis(k)
            DatRow("Rang" & (k + 1).ToString) = xRang(k)
        Next
        DatSet.Tables("T_TampConsultRangTech").Rows.Add(DatRow)
        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        DatAdapt.Update(DatSet, "T_TampConsultRangTech")
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

    Private Sub BtAfficheRapport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtAfficheRapport.Click
        AfficherRapport()
    End Sub

    Private Sub AfficherRapport(Optional ByVal Action As String = "Afficher")
        DebutChargement()

        NoteEvalParConsult()
        NoteConsultParCritere()

        ' Affichage du rapport *****************
        Dim reportRapEval, reportCouv As New ReportDocument
        Dim Chemin As String = lineEtat & "\Rapport Evaluation Consultant\"

        Dim DatSet = New DataSet
        reportRapEval.Load(Chemin & "RapportEvalConsultContenu.rpt")
        reportRapEval.SetDataSource(DatSet)
        reportRapEval.SetParameterValue("NumDp1", CmbNumDoss.Text)
        reportRapEval.SetParameterValue("CodeProjet", ProjetEnCours)
        reportRapEval.SetParameterValue("NombreConsult", xNbSoumis)
        reportRapEval.SetParameterValue("NombreEval10", xNbEval, "EvaluationIndividuelle.rpt")
        reportRapEval.SetParameterValue("NombreEval10", xNbEval, "ScoreIndivCompare3.rpt")
        reportRapEval.SetParameterValue("NombreEval10", xNbEval, "ScoreIndivCompare4.rpt")
        reportRapEval.SetParameterValue("NombreEval10", xNbEval, "ScoreIndivCompare5.rpt")
        reportRapEval.SetParameterValue("NombreEval10", xNbEval, "ScoreIndivCompare6.rpt")
        reportRapEval.SetParameterValue("NombreEval10", xNbEval, "ScoreIndivCompare7.rpt")
        reportRapEval.SetParameterValue("NombreEval10", xNbEval, "ScoreIndivCompare8.rpt")
        reportRapEval.SetParameterValue("NombreEval10", xNbEval, "ScoreIndivCompare9.rpt")
        reportRapEval.SetParameterValue("NombreEval10", xNbEval, "ScoreIndivCompare10.rpt")
        reportRapEval.SetParameterValue("NombreEval10", xNbEval, "ScoreIndivNomEval.rpt")

        'reportRapEval.Subreports.Item("EvaluationIndividuelle.rpt").SetParameterValue("NombreEval10", xNbEval)
        'For k As Integer = 3 To 10
        '    reportRapEval.Subreports.Item("ScoreIndivCompare" & k.ToString & ".rpt").SetParameterValue("NombreEval10", xNbEval)
        'Next

        Dim MontModif As Boolean = False
        query = "select S.RangConsult,S.RangFinal from T_Consultant as C,T_SoumissionConsultant as S where S.RefConsult=C.RefConsult and C.NumeroDp='" & CmbNumDoss.Text & "' and S.EvalTechOk='OUI'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            If (rw(0) <> rw(1)) Then
                MontModif = True
                Exit For
            End If
        Next

        reportRapEval.SetParameterValue("ModifRangParMontant", MontModif)

        Dim TxtGenerExiste As Boolean = True
        query = "select TexteGeneralites from T_DP where NumeroDp='" & CmbNumDoss.Text & "' and CodeProjet='" & ProjetEnCours & "'"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            If (rw(0).ToString = "") Then
                TxtGenerExiste = False
                ReponseDialog = ""
                EvalConsult_TexteGeneralites.ShowDialog()
            Else
                ReponseDialog = MettreApost(rw(0).ToString)
            End If
        Next

        If (TxtGenerExiste = False) Then

            DatSet = New DataSet
            query = "select * from T_DP where NumeroDp='" & CmbNumDoss.Text & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Fill(DatSet, "T_DP")

            DatSet.Tables!T_DP.Rows(0)!TexteGeneralites = EnleverApost(ReponseDialog)

            DatAdapt.Update(DatSet, "T_DP")
            DatSet.Clear()
            BDQUIT(sqlconn)
        End If

        reportRapEval.SetParameterValue("TexteGeneralites", ReponseDialog)
        ReponseDialog = ""

        If (Action = "Imprimer") Then
            DatSet1 = New DataSet
            reportCouv.Load(Chemin & "RapportEvalConsult.rpt")
            reportCouv.SetDataSource(DatSet1)
            reportCouv.SetParameterValue("NumDp", CmbNumDoss.Text)
            reportCouv.SetParameterValue("CodeProjet", ProjetEnCours)

            reportRapEval.PrintToPrinter(1, True, 0, 0)
            reportCouv.PrintToPrinter(1, True, 0, 0)

            ' Enregistrement automatique *************************
            Dim NomDossier As String = line & "\RapoortEvaluation\Consultants\" & TxtMethode.Text
            If (Directory.Exists(NomDossier) = False) Then
                Directory.CreateDirectory(NomDossier)
            End If

            NomDossier = NomDossier & "\" & CmbNumDoss.Text.Replace("/", "_")
            If (Directory.Exists(NomDossier) = False) Then
                Directory.CreateDirectory(NomDossier)
            End If

            reportRapEval.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, NomDossier & "\Rapport.pdf")
            reportCouv.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, NomDossier & "\Couverture.pdf")
            '******************************************************************
        Else
            FullScreenReport.FullView.ReportSource = reportRapEval
            FullScreenReport.ShowDialog()
        End If
        FinChargement()
    End Sub

    Private Sub BtImpRapport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtImpRapport.Click
        AfficherRapport("Imprimer")
    End Sub

    Private Sub BtPtsFortsFaibles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtPtsFortsFaibles.Click
        EvalConsult_PtsFortFaible.ShowDialog()
    End Sub

    Private Sub BtEditionContrat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEditionContrat.Click
        PnlEditionMarche.Visible = True
    End Sub
End Class