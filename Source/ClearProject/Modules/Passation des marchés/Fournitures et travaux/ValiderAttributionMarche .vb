Public Class ValiderAttributionMarche
    Dim NomListe As New ArrayList
    Dim ListeValide() As String
    Dim OkValide As New ArrayList
    Dim NONValide As New ArrayList
    Dim NbreListe As Integer = 0
    Dim NbreCOJO As Integer = 0
    Dim NbreCOJOEval As Integer = 0
    Private Sub ValiderAttributionMarche_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ChargerListe()
        NbreChecboxAffiher()
        VerifierListe()
        ListeOk()
    End Sub

    Private Sub ListeOk()
        Eval3.Checked = False
        Eval1.Checked = False
        Eval2.Checked = False
        Eval4.Checked = False
        Eval5.Checked = False
        Dim ValRet As Boolean = True

        If NbreCOJOEval = 0 Then
            ValRet = False
            Avertissement.Text = "Les membres de la commission n'ont pas encore validé le rapport d'évaluation."
        ElseIf NbreCOJO > NbreCOJOEval Then
            ValRet = False
            Avertissement.Text = "Certains membres de la commission n'ont pas encore validé le rapport d'évaluation."
        End If
        If NONValide.Count > OkValide.Count Then
            ValRet = False
            Avertissement.Text = "Les membres de la commission ne sont pas d'accord avec le rapport d'évaluation."
        End If
        For i = 1 To NbreCOJOEval
            If "Eval" & i = "Eval1" Then
                Eval1.Checked = True
            End If
            If "Eval" & i = "Eval2" Then
                Eval2.Checked = True
            End If
            If "Eval" & i = "Eval3" Then
                Eval3.Checked = True
            End If
            If "Eval" & i = "Eval4" Then
                Eval4.Checked = True
            End If
            If "Eval" & i = "Eval5" Then
                Eval5.Checked = True
            End If
        Next
        If (ValRet = True) Then
            Avertissement.Text = "Après la validation, le seul moyen de changer d'avis est de reprendre tout le processus de marché."
            BtAttribuer.Enabled = True
            BtAttribuer.Focus()
        Else
            BtAttribuer.Enabled = False
        End If
        'If (ValRet = True) Then
        '    Attente.Visible = True
        '    Avertissement.Visible = True
        'Else
        '    Attente.Visible = False
        '    Avertissement.Visible = False
        'End If
    End Sub

    Private Sub ChargerListe()
        query = "select CodeMem,NomMem from T_Commission where NumeroDAO='" & EnleverApost(JugementOffres.CmbNumDoss.Text) & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        NbreCOJO = dt.Rows.Count
        NomListe.Clear()
        For Each rw As DataRow In dt.Rows
            NomListe.Add(rw("CodeMem").ToString & "_" & MettreApost(rw("NomMem").ToString))
        Next
    End Sub

    Private Sub VerifierListe()
        query = "select S.CodeMem,S.NomMem, F.Validation from T_Commission as S , t_lotvalidationrapportcojo as F where F.Id_COJO=S.CodeMem and S.NumeroDAO='" & EnleverApost(JugementOffres.CmbNumDoss.Text) & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        NbreCOJOEval = dt.Rows.Count
        OkValide.Clear()
        NONValide.Clear()
        For Each rw As DataRow In dt.Rows
            If rw("Validation".ToString) = "OUI" Then
                OkValide.Add(rw(0).ToString & "_" & MettreApost(rw(1).ToString))
            ElseIf rw("Validation".ToString) = "NON" Then
                NONValide.Add(rw(0).ToString & "_" & MettreApost(rw(1).ToString))
            End If
        Next
    End Sub

    Private Sub NbreChecboxAffiher()
        For i = 0 To NbreCOJO
            If "Eval" & i = "Eval1" Then
                Eval1.Visible = True
            End If
            If "Eval" & i = "Eval2" Then
                Eval2.Visible = True
            End If
            If "Eval" & i = "Eval3" Then
                Eval3.Visible = True
            End If
            If "Eval" & i = "Eval4" Then
                Eval4.Visible = True
            End If
            If "Eval" & i = "Eval5" Then
                Eval5.Visible = True
            End If
        Next
    End Sub
    Private Sub BtAnnuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAnnuler.Click
        ReponseDialog = ""
        Me.Close()
    End Sub
    Private Sub BtAttribuer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAttribuer.Click
        ReponseDialog = "OK"
        Me.Close()
    End Sub
    Private Sub Avertissement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Avertissement.Click
        BtAttribuer.Focus()
    End Sub
End Class