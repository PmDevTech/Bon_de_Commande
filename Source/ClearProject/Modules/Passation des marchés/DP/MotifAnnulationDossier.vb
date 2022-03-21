Imports MySql.Data.MySqlClient

Public Class MotifAnnulationDossier
    Private Sub EvalOffreFinanciere_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        TxtTextAnnul.ResetText()
        TxtTextAnnul.Select()
    End Sub

    Private Sub BtQuitter_Click(sender As Object, e As EventArgs) Handles BtQuitter.Click
        ReponseDialog = ""
        Me.Close()
    End Sub

    Private Sub BtEnreMotifAnnul_Click(sender As Object, e As EventArgs) Handles BtEnreMotifAnnul.Click
        If TxtTextAnnul.IsRequiredControl("Veuillez saisir le motif d'annulation du dossier") Then
            TxtTextAnnul.Select()
            Exit Sub
        End If
        If ConfirmMsg("Confirmez-vous l'annulation de ce dossier?") = DialogResult.Yes Then
            ReponseDialog = TxtTextAnnul.Text
            Me.Close()
        End If
    End Sub
End Class