Public Class NouveauGroupeCriterePostQ 

    Private Sub NouveauGroupeCriterePostQ_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        TxtGroupe.Text = ""
        ReponseDialog = ""
    End Sub

    Private Sub BtEnrgGroupe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgGroupe.Click
        ReponseDialog = TxtGroupe.Text
        Me.Close()
    End Sub

    Private Sub BtQuitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        ReponseDialog = ""
        Me.Close()
    End Sub
End Class