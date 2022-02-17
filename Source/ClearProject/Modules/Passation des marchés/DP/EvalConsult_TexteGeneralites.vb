Public Class EvalConsult_TexteGeneralites 

    Private Sub EvalConsult_TexteGeneralites_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        TxtGeneralites.Text = ReponseDialog
        ReponseDialog = ""
    End Sub


    Private Sub BtEnrg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrg.Click
        ReponseDialog = TxtGeneralites.Text
        Me.Close()
    End Sub
End Class