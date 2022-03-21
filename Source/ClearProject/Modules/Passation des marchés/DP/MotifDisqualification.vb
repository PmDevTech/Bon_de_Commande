Imports MySql.Data.MySqlClient

Public Class MotifDisqualification
    Private Sub EvalOffreFinanciere_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        TxtMotif.ResetText()
        TxtMotif.Select()
    End Sub

    Private Sub BtQuitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        ReponseDialog = ""
        Me.Close()
    End Sub

    Private Sub BtEnregMotif_Click(sender As Object, e As EventArgs) Handles BtEnregMotif.Click
        If TxtMotif.IsRequiredControl("Veuillez saisir le motif de disqualification") Then
            TxtMotif.Select()
            Exit Sub
        End If
        If ConfirmMsg("Confirmez-vous la disqualification de ce consultant?") = DialogResult.Yes Then
            ReponseDialog = TxtMotif.Text
            Me.Close()
        End If
    End Sub
End Class