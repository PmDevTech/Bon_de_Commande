Public Class ClearLicense 

    Private Sub ClearLicense_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        TxtCleLic.Text = ""
    End Sub

    Private Sub BtValider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtValider.Click
        Me.Close()
    End Sub
End Class