Public Class EvaluateurPresent

    Private Sub EvaluateurPresent_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If (ReponseDialog = "") Then
            ExceptRevue2 = "OUT"
        Else
            ExceptRevue2 = ""
        End If
    End Sub

    Private Sub EvaluateurPresent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        TxtCode.Text = ""
        ReponseDialog = ""
        If (ExceptRevue = "NON") Then
            LblRefuse.Visible = True
        Else
            LblRefuse.Visible = False
        End If
        TxtCode.Focus()
    End Sub

    Private Sub TxtCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtCode.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            ReponseDialog = TxtCode.Text
            Me.Close()
        End If
    End Sub
End Class