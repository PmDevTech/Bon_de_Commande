Imports System.Windows.Forms

Public Class DialogDate

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        ReponseDialog = DateAjout.Text
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click

        Me.Close()
    End Sub

    Private Sub DialogDate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        DateAjout.Text = My.Computer.Clock.GmtTime.Date
        If (ReponseDialog <> "" And ReponseDialog <> "__/__/____") Then
            DateAjout.Text = CDate(ReponseDialog)
        End If
        DateAjout.MaxDate = Now.ToShortDateString
        ReponseDialog = ""

    End Sub
End Class