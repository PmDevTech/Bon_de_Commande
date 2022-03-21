Imports MySql.Data.MySqlClient

Public Class FicheEvaluation

    Dim CritereChap() As String = {"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X"}
    Dim NumChap As Decimal = 0
    Dim Niv2 As Decimal = 1
    Dim Niv3 As Decimal = 1
    Dim Sequence(4) As String
    Dim NumSeq As Decimal = 0
    Dim CodeSoum As String = ""

    Private Sub FicheEvaluation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        Me.Height = 235
        TxtNom.Text = ReponseDialog
        CodeSoum = ExceptRevue

        NumChap = 0
        Niv2 = 1
        Niv3 = 1
        ChargerNote()

    End Sub

    Private Sub ChargerNote()

        Me.Height = 235
        PnlChk.Visible = False
        Chk1.Visible = False
        Chk2.Visible = False
        Chk3.Visible = False
        Chk4.Visible = False
        Chk5.Visible = False
        Chk6.Visible = False
        Chk7.Visible = False
        Chk8.Visible = False
        Chk9.Visible = False
        Chk10.Visible = False

        NumSeq = 0
        For k As Integer = 0 To 3
            Sequence(k) = ""
        Next

        Dim Requete As String = ""
        Dim cpt2 As Decimal = 0

        query = "select RefCritere,IntituleCritere,TypeCritere,PointCritere,CodeCritere from T_DP_CritereEval where NumeroDp='" & EvaluationConsultants.CmbNumDoss.Text & "' and CodeCritere='" & CritereChap(NumChap) & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows

            If (rw(2).ToString = "Etiquette") Then
                Sequence(NumSeq) = rw(2).ToString
                NumSeq += 1
                Pnl1.Visible = True

                Lbl1.Text = CritereChap(NumChap) & "/  " & MettreApost(rw(1).ToString)
                TxtPt1.Text = "/ " & rw(3).ToString

                query = "select RefCritere,IntituleCritere,TypeCritere,PointCritere,CodeCritere from T_DP_CritereEval where NumeroDp='" & EvaluationConsultants.CmbNumDoss.Text & "' and CodeCritere='" & CritereChap(NumChap) & "." & Niv2.ToString & "'"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows

                    If (rw1(2).ToString = "Etiquette") Then
                        Sequence(NumSeq) = rw1(2).ToString
                        NumSeq += 1
                        Pnl2.Visible = True
                        Lbl2.Text = CritereChap(NumChap) & "." & Niv2.ToString & "/  " & MettreApost(rw1(1).ToString)
                        TxtPt2.Text = "/ " & rw1(3).ToString

                        query = "select RefCritere,IntituleCritere,TypeCritere,PointCritere,CodeCritere from T_DP_CritereEval where NumeroDp='" & EvaluationConsultants.CmbNumDoss.Text & "' and CodeCritere='" & CritereChap(NumChap) & "." & Niv2.ToString & "." & Niv3.ToString & "'"
                        Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw2 As DataRow In dt2.Rows

                            If (rw2(2).ToString = "Note") Then
                                Sequence(NumSeq) = rw2(2).ToString
                                NumSeq += 1

                                Lbl3.Text = CritereChap(NumChap) & "." & Niv2.ToString & "." & Niv3.ToString & "/  " & MettreApost(rw2(1).ToString) & " [/ " & rw2(3).ToString & " pts]"
                                TxtNote.Properties.ReadOnly = False
                                'TxtNote.Text = InfosCritere(CodeSoum, EvaluationConsultants.CodeEvaluateur, rw2(0).ToString)(0)
                                'TxtCommentaire.Text = InfosCritere(CodeSoum, EvaluationConsultants.CodeEvaluateur, rw2(0).ToString)(1)

                                cpt2 = 0

                                query = "select RefCritere,IntituleCritere,TypeCritere,PointCritere,CodeCritere from T_DP_CritereEval where NumeroDp='" & EvaluationConsultants.CmbNumDoss.Text & "' and CodeCritere like '" & CritereChap(NumChap) & "." & Niv2.ToString & "." & Niv3.ToString & ".%'"
                                Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                                For Each rw3 As DataRow In dt3.Rows
                                    cpt2 += 1
                                    Me.Height = 372
                                    PnlChk.Visible = True
                                    TxtNote.Properties.ReadOnly = True

                                    If cpt2 = 1 Then
                                        Sequence(NumSeq) = rw3(2).ToString
                                        NumSeq += 1

                                        InfosChk(Chk1, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                        PtChk1.Text = rw3(3).ToString
                                        If (TxtNote.Text = rw3(3).ToString) Then
                                            Chk1.Checked = True
                                        Else
                                            Chk1.Checked = False
                                        End If
                                    ElseIf cpt2 = 2 Then
                                        InfosChk(Chk2, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                        PtChk2.Text = rw3(3).ToString
                                        If (TxtNote.Text = rw3(3).ToString) Then
                                            Chk2.Checked = True
                                        Else
                                            Chk2.Checked = False
                                        End If
                                    ElseIf cpt2 = 3 Then
                                        InfosChk(Chk3, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                        PtChk3.Text = rw3(3).ToString
                                        If (TxtNote.Text = rw3(3).ToString) Then
                                            Chk3.Checked = True
                                        Else
                                            Chk3.Checked = False
                                        End If
                                    ElseIf cpt2 = 4 Then
                                        InfosChk(Chk4, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                        PtChk4.Text = rw3(3).ToString
                                        If (TxtNote.Text = rw3(3).ToString) Then
                                            Chk4.Checked = True
                                        Else
                                            Chk4.Checked = False
                                        End If
                                    ElseIf cpt2 = 5 Then
                                        InfosChk(Chk5, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                        PtChk5.Text = rw3(3).ToString
                                        If (TxtNote.Text = rw3(3).ToString) Then
                                            Chk5.Checked = True
                                        Else
                                            Chk5.Checked = False
                                        End If
                                    ElseIf cpt2 = 6 Then
                                        InfosChk(Chk6, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                        PtChk6.Text = rw3(3).ToString
                                        If (TxtNote.Text = rw3(3).ToString) Then
                                            Chk6.Checked = True
                                        Else
                                            Chk6.Checked = False
                                        End If
                                    ElseIf cpt2 = 7 Then
                                        InfosChk(Chk7, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                        PtChk7.Text = rw3(3).ToString
                                        If (TxtNote.Text = rw3(3).ToString) Then
                                            Chk7.Checked = True
                                        Else
                                            Chk7.Checked = False
                                        End If
                                    ElseIf cpt2 = 8 Then
                                        InfosChk(Chk8, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                        PtChk8.Text = rw3(3).ToString
                                        If (TxtNote.Text = rw3(3).ToString) Then
                                            Chk8.Checked = True
                                        Else
                                            Chk8.Checked = False
                                        End If
                                    ElseIf cpt2 = 9 Then
                                        InfosChk(Chk9, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                        PtChk9.Text = rw3(3).ToString
                                        If (TxtNote.Text = rw3(3).ToString) Then
                                            Chk9.Checked = True
                                        Else
                                            Chk9.Checked = False
                                        End If
                                    ElseIf cpt2 = 10 Then
                                        InfosChk(Chk10, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                        PtChk10.Text = rw3(3).ToString
                                        If (TxtNote.Text = rw3(3).ToString) Then
                                            Chk10.Checked = True
                                        Else
                                            Chk10.Checked = False
                                        End If
                                    End If
                                Next
                            End If
                        Next

                    ElseIf (rw1(2).ToString = "Note") Then
                        Sequence(NumSeq) = rw1(2).ToString
                        NumSeq += 1
                        Pnl2.Visible = False
                        Pnl3.Visible = True

                        Lbl3.Text = rw1(4).ToString & "/  " & MettreApost(rw1(1).ToString) & " [/" & rw1(3).ToString & " pts]"
                        TxtNote.Properties.ReadOnly = False
                        ' TxtNote.Text = InfosCritere(CodeSoum, EvaluationConsultants.CodeEvaluateur, rw1(0).ToString)(0)
                        ' TxtCommentaire.Text = InfosCritere(CodeSoum, EvaluationConsultants.CodeEvaluateur, rw1(0).ToString)(1)
                        cpt2 = 0

                        query = "select RefCritere,IntituleCritere,TypeCritere,PointCritere,CodeCritere from T_DP_CritereEval where NumeroDp='" & EvaluationConsultants.CmbNumDoss.Text & "' and CodeCritere like '" & CritereChap(NumChap) & "." & Niv2.ToString & ".%'"
                        Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw3 As DataRow In dt3.Rows
                            cpt2 += 1
                            Me.Height = 372
                            PnlChk.Visible = True
                            TxtNote.Properties.ReadOnly = True

                            If cpt2 = 1 Then
                                Sequence(NumSeq) = rw3(2).ToString
                                NumSeq += 1

                                InfosChk(Chk1, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                PtChk1.Text = rw3(3).ToString
                                If (TxtNote.Text = rw3(3).ToString) Then
                                    Chk1.Checked = True
                                Else
                                    Chk1.Checked = False
                                End If
                            ElseIf cpt2 = 2 Then
                                InfosChk(Chk2, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                PtChk2.Text = rw3(3).ToString
                                If (TxtNote.Text = rw3(3).ToString) Then
                                    Chk2.Checked = True
                                Else
                                    Chk2.Checked = False
                                End If
                            ElseIf cpt2 = 3 Then
                                InfosChk(Chk3, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                PtChk3.Text = rw3(3).ToString
                                If (TxtNote.Text = rw3(3).ToString) Then
                                    Chk3.Checked = True
                                Else
                                    Chk3.Checked = False
                                End If
                            ElseIf cpt2 = 4 Then
                                InfosChk(Chk4, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                PtChk4.Text = rw3(3).ToString
                                If (TxtNote.Text = rw3(3).ToString) Then
                                    Chk4.Checked = True
                                Else
                                    Chk4.Checked = False
                                End If
                            ElseIf cpt2 = 5 Then
                                InfosChk(Chk5, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                PtChk5.Text = rw3(3).ToString
                                If (TxtNote.Text = rw3(3).ToString) Then
                                    Chk5.Checked = True
                                Else
                                    Chk5.Checked = False
                                End If
                            ElseIf cpt2 = 6 Then
                                InfosChk(Chk6, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                PtChk6.Text = rw3(3).ToString
                                If (TxtNote.Text = rw3(3).ToString) Then
                                    Chk6.Checked = True
                                Else
                                    Chk6.Checked = False
                                End If
                            ElseIf cpt2 = 7 Then
                                InfosChk(Chk7, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                PtChk7.Text = rw3(3).ToString
                                If (TxtNote.Text = rw3(3).ToString) Then
                                    Chk7.Checked = True
                                Else
                                    Chk7.Checked = False
                                End If
                            ElseIf cpt2 = 8 Then
                                InfosChk(Chk8, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                PtChk8.Text = rw3(3).ToString
                                If (TxtNote.Text = rw3(3).ToString) Then
                                    Chk8.Checked = True
                                Else
                                    Chk8.Checked = False
                                End If
                            ElseIf cpt2 = 9 Then
                                InfosChk(Chk9, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                PtChk9.Text = rw3(3).ToString
                                If (TxtNote.Text = rw3(3).ToString) Then
                                    Chk9.Checked = True
                                Else
                                    Chk9.Checked = False
                                End If
                            ElseIf cpt2 = 10 Then
                                InfosChk(Chk10, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                                PtChk10.Text = rw3(3).ToString
                                If (TxtNote.Text = rw3(3).ToString) Then
                                    Chk10.Checked = True
                                Else
                                    Chk10.Checked = False
                                End If
                            End If
                        Next
                    End If
                Next

            ElseIf (rw(2).ToString = "Note") Then
                Sequence(NumSeq) = rw(2).ToString
                NumSeq += 1

                Pnl1.Visible = False
                Pnl2.Visible = False
                Pnl3.Visible = True

                Lbl3.Text = CritereChap(NumChap) & "/  " & MettreApost(rw(1).ToString) & " [/ " & rw(3).ToString & " pts]"
                TxtNote.Properties.ReadOnly = False
                'TxtNote.Text = InfosCritere(CodeSoum, EvaluationConsultants.CodeEvaluateur, rw(0).ToString)(0)
                'TxtCommentaire.Text = InfosCritere(CodeSoum, EvaluationConsultants.CodeEvaluateur, rw(0).ToString)(1)
                cpt2 = 0

                query = "select RefCritere,IntituleCritere,TypeCritere,PointCritere,CodeCritere from T_DP_CritereEval where NumeroDp='" & EvaluationConsultants.CmbNumDoss.Text & "' and CodeCritere like '" & CritereChap(NumChap) & ".%'"
                Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                For Each rw3 As DataRow In dt3.Rows
                    cpt2 += 1
                    Me.Height = 372
                    PnlChk.Visible = True
                    TxtNote.Properties.ReadOnly = True

                    If cpt2 = 1 Then
                        Sequence(NumSeq) = rw3(2).ToString
                        NumSeq += 1

                        InfosChk(Chk1, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                        PtChk1.Text = rw3(3).ToString
                        If (TxtNote.Text = rw3(3).ToString) Then
                            Chk1.Checked = True
                        Else
                            Chk1.Checked = False
                        End If
                    ElseIf cpt2 = 2 Then
                        InfosChk(Chk2, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                        PtChk2.Text = rw3(3).ToString
                        If (TxtNote.Text = rw3(3).ToString) Then
                            Chk2.Checked = True
                        Else
                            Chk2.Checked = False
                        End If
                    ElseIf cpt2 = 3 Then
                        InfosChk(Chk3, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                        PtChk3.Text = rw3(3).ToString
                        If (TxtNote.Text = rw3(3).ToString) Then
                            Chk3.Checked = True
                        Else
                            Chk3.Checked = False
                        End If
                    ElseIf cpt2 = 4 Then
                        InfosChk(Chk4, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                        PtChk4.Text = rw3(3).ToString
                        If (TxtNote.Text = rw3(3).ToString) Then
                            Chk4.Checked = True
                        Else
                            Chk4.Checked = False
                        End If
                    ElseIf cpt2 = 5 Then
                        InfosChk(Chk5, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                        PtChk5.Text = rw3(3).ToString
                        If (TxtNote.Text = rw3(3).ToString) Then
                            Chk5.Checked = True
                        Else
                            Chk5.Checked = False
                        End If
                    ElseIf cpt2 = 6 Then
                        InfosChk(Chk6, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                        PtChk6.Text = rw3(3).ToString
                        If (TxtNote.Text = rw3(3).ToString) Then
                            Chk6.Checked = True
                        Else
                            Chk6.Checked = False
                        End If
                    ElseIf cpt2 = 7 Then
                        InfosChk(Chk7, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                        PtChk7.Text = rw3(3).ToString
                        If (TxtNote.Text = rw3(3).ToString) Then
                            Chk7.Checked = True
                        Else
                            Chk7.Checked = False
                        End If
                    ElseIf cpt2 = 8 Then
                        InfosChk(Chk8, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                        PtChk8.Text = rw3(3).ToString
                        If (TxtNote.Text = rw3(3).ToString) Then
                            Chk8.Checked = True
                        Else
                            Chk8.Checked = False
                        End If
                    ElseIf cpt2 = 9 Then
                        InfosChk(Chk9, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                        PtChk9.Text = rw3(3).ToString
                        If (TxtNote.Text = rw3(3).ToString) Then
                            Chk9.Checked = True
                        Else
                            Chk9.Checked = False
                        End If
                    ElseIf cpt2 = 10 Then
                        InfosChk(Chk10, rw3(4).ToString & "/  " & MettreApost(rw3(1).ToString) & " [" & rw3(3).ToString & " pts]")
                        PtChk10.Text = rw3(3).ToString
                        If (TxtNote.Text = rw3(3).ToString) Then
                            Chk10.Checked = True
                        Else
                            Chk10.Checked = False
                        End If
                    End If
                Next
            End If
        Next

        TxtNote.Select()

    End Sub

    Private Function InfosCritere(ByVal Soum As String, ByVal Eval As String, ByVal Critere As String) As String()

        Dim ValRet() As String = {"0", "Commentaire"}
        query = "select NoteConsult,Remarque from T_SoumisNoteConsult where RefCritere='" & Critere & "' and CodeMem='" & Eval & "' and RefSoumis='" & Soum & "'"
       Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            ValRet(0) = rw(0).ToString
            ValRet(1) = MettreApost(rw(1).ToString)
        Next
        Return ValRet

    End Function

    Private Sub InfosChk(ByRef Chk As DevExpress.XtraEditors.CheckEdit, ByVal Libelle As String)
        Chk.Visible = True
        Chk.Text = Libelle
        Chk.Checked = False
    End Sub

    Private Sub BtQuitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtQuitter.Click

        Me.Close()

    End Sub

    Private Function RefDe(ByVal Code As String) As String

        Dim ValRet As String = ""
        query = "select RefCritere from T_DP_CritereEval where CodeCritere='" & Code & "' and NumeroDp='" & EvaluationConsultants.CmbNumDoss.Text & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            ValRet = rw(0).ToString
        Next
        Return ValRet

    End Function

    Private Sub BtEnrgCont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgCont.Click

        If (NumSeq <> 0 And TxtNote.Visible = True And TxtNote.Text <> "") Then

            If (IsNumeric(TxtNote.Text) = True) Then
                Dim ptsMax As String = Lbl3.Text.Split("["c)(1)
                ptsMax = ptsMax.Replace("pts]", "").Replace("/", "").Replace(" ", "")
                If (CDec(TxtNote.Text) > CDec(ptsMax)) Then
                    MsgBox("Note supérieure à la référence!", MsgBoxStyle.Exclamation)
                    TxtNote.Select()
                    Exit Sub
                End If
            Else
                MsgBox("Format note incorrect!", MsgBoxStyle.Exclamation)
                TxtNote.Select()
                Exit Sub
            End If

            Dim codeNote() As String = Lbl3.Text.Split("/"c)
            Dim ligneExist As Boolean = False

            'query = "select Count(*) from T_SoumisNoteConsult where RefSoumis='" & CodeSoum & "' and CodeMem='" & EvaluationConsultants.CodeEvaluateur & "' and RefCritere='" & RefDe(codeNote(0)) & "'"
            Dim Nbres As Decimal = Val(ExecuteScallar(query))
            If Nbres > 0 Then
                ligneExist = True
            End If

            If (ligneExist = False) Then

                ' query = "insert into T_SoumisNoteConsult values('" & CodeSoum & "','" & EvaluationConsultants.CodeEvaluateur & "','" & RefDe(codeNote(0)) & "','" & TxtNote.Text & "', '" & IIf(TxtCommentaire.Text <> "Commentaire", EnleverApost(TxtCommentaire.Text), "") & "')"
                ExecuteNonQuery(query)

                Else

                '  query = "Update T_SoumisNoteConsult set NoteConsult='" & TxtNote.Text & "', Remarque='" & IIf(TxtCommentaire.Text <> "Commentaire", EnleverApost(TxtCommentaire.Text), "") & "' where RefSoumis='" & CodeSoum & "' and CodeMem='" & EvaluationConsultants.CodeEvaluateur & "' and RefCritere='" & RefDe(codeNote(0)) & "'"
                ExecuteNonQuery(query)

                End If

            End If


            Dim Termine As Boolean = False
        If (NumSeq >= 2) Then
            If (Sequence(2) <> "Bareme") Then
                Niv3 += 1
                If (LigneExiste(CritereChap(NumChap) & "." & Niv2.ToString & "." & Niv3.ToString) = False) Then
                    Niv2 += 1
                    Niv3 = 1
                    If (LigneExiste(CritereChap(NumChap) & "." & Niv2.ToString) = False) Then
                        NumChap += 1
                        Niv2 = 1
                        Niv3 = 1
                        If (LigneExiste(CritereChap(NumChap)) = False) Then
                            'Passer au total des pts
                            'MsgBox("Terminé!", MsgBoxStyle.Information)

                            Me.Close()
                        End If
                    End If
                End If
            Else
                Niv2 += 1
                Niv3 = 1
                If (LigneExiste(CritereChap(NumChap) & "." & Niv2.ToString) = False) Then
                    NumChap += 1
                    Niv2 = 1
                    Niv3 = 1
                    If (LigneExiste(CritereChap(NumChap)) = False) Then
                        'Passer au total des pts
                        'MsgBox("Terminé!", MsgBoxStyle.Information)

                        Me.Close()
                    End If
                End If
            End If
        ElseIf (NumSeq = 1) Then
            If (Sequence(1) <> "Bareme") Then
                Niv2 += 1
                Niv3 = 1
                If (LigneExiste(CritereChap(NumChap) & "." & Niv2.ToString) = False) Then
                    NumChap += 1
                    Niv2 = 1
                    Niv3 = 1
                    If (LigneExiste(CritereChap(NumChap)) = False) Then
                        'Passer au total des pts
                        'MsgBox("Terminé!", MsgBoxStyle.Information)

                        Me.Close()
                    End If
                End If
            Else
                NumChap += 1
                Niv2 = 1
                Niv3 = 1
                If (LigneExiste(CritereChap(NumChap)) = False) Then
                    'Passer au total des pts
                    'MsgBox("Terminé!", MsgBoxStyle.Information)

                    Me.Close()
                End If
            End If
        Else
            NumChap += 1
            Niv2 = 1
            Niv3 = 1
            If (LigneExiste(CritereChap(NumChap)) = False) Then
                'Passer au total des pts
                'MsgBox("Terminé!", MsgBoxStyle.Information)

                Me.Close()
            End If
        End If
        ChargerNote()
        TxtNote.Select(0, TxtNote.Text.Length)

    End Sub

    Private Function LigneExiste(ByVal numLg As String) As Boolean

        Dim NbLg As Decimal = 0
        query = "select Count(*) from T_DP_CritereEval where NumeroDp='" & EvaluationConsultants.CmbNumDoss.Text & "' and CodeCritere like '" & numLg & "%'"
        NbLg = ExecuteNonQuery(query)

        If (NbLg = 0) Then
            Return False
        Else
            Return True
        End If


    End Function

    Private Sub TxtCommentaire_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCommentaire.GotFocus

        If (TxtCommentaire.Text = "Commentaire") Then
            TxtCommentaire.Text = ""
            TxtCommentaire.Select()
        End If

    End Sub

    Private Sub TxtCommentaire_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCommentaire.LostFocus

        If (TxtCommentaire.Text = "") Then
            TxtCommentaire.Text = "Commentaire"
        End If

    End Sub

    Private Sub TxtCommentaire_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCommentaire.TextChanged
        If (TxtCommentaire.Text <> "Commentaire") Then
            TxtCommentaire.ForeColor = Color.Black
        Else
            TxtCommentaire.ForeColor = Color.Silver
        End If
    End Sub

    Private Sub Chk1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Chk1.CheckedChanged
        If (Chk1.Checked = True) Then TxtNote.Text = PtChk1.Text
    End Sub

    Private Sub Chk2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Chk2.CheckedChanged
        If (Chk2.Checked = True) Then TxtNote.Text = PtChk2.Text
    End Sub

    Private Sub Chk3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Chk3.CheckedChanged
        If (Chk3.Checked = True) Then TxtNote.Text = PtChk3.Text
    End Sub

    Private Sub Chk4_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Chk4.CheckedChanged
        If (Chk4.Checked = True) Then TxtNote.Text = PtChk4.Text
    End Sub

    Private Sub Chk5_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Chk5.CheckedChanged
        If (Chk5.Checked = True) Then TxtNote.Text = PtChk5.Text
    End Sub

    Private Sub Chk6_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Chk6.CheckedChanged
        If (Chk6.Checked = True) Then TxtNote.Text = PtChk6.Text
    End Sub

    Private Sub Chk7_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Chk7.CheckedChanged
        If (Chk7.Checked = True) Then TxtNote.Text = PtChk7.Text
    End Sub

    Private Sub Chk8_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Chk8.CheckedChanged
        If (Chk8.Checked = True) Then TxtNote.Text = PtChk8.Text
    End Sub

    Private Sub Chk9_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Chk9.CheckedChanged
        If (Chk9.Checked = True) Then TxtNote.Text = PtChk9.Text
    End Sub

    Private Sub Chk10_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Chk10.CheckedChanged
        If (Chk10.Checked = True) Then TxtNote.Text = PtChk10.Text
    End Sub

    Private Sub TxtNote_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtNote.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            BtEnrgCont_Click(Me, e)
            TxtNote.Select(0, TxtNote.Text.Length)
        End If
    End Sub
End Class