Imports MySql.Data.MySqlClient

Public Class MotifDisqualification
    Public TypeDossier As String = ""

    Private Sub EvalOffreFinanciere_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        TxtMotif.ResetText()
        TxtMotif.Select()
        If TypeDossier.ToString = "DAO" Then
            LabelControl1.Text = "Spécifiez la raison de la disqualification du soumissionaire"
        Else
            LabelControl1.Text = "Spécifiez la raison de la disqualification du consultant"
        End If
    End Sub

    Private Sub BtQuitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        ReponseDialog = ""
        Me.Close()
    End Sub

    Private Sub BtEnregMotif_Click(sender As Object, e As EventArgs) Handles BtEnregMotif.Click
        If TypeDossier = "DAO" Then
            If TxtMotif.IsRequiredControl("Veuillez saisir la raison de disqualification de ce soumissionnaire.") Then
                TxtMotif.Select()
                Exit Sub
            End If
            If ConfirmMsg("Confirmez-vous la disqualification de ce soumissionnaire?") = DialogResult.Yes Then
                ReponseDialog = TxtMotif.Text
                Me.Close()
            End If
        Else
            If TxtMotif.IsRequiredControl("Veuillez saisir la raison de disqualification de ce consultant.") Then
                TxtMotif.Select()
                Exit Sub
            End If
            If ConfirmMsg("Confirmez-vous la disqualification de ce consultant?") = DialogResult.Yes Then
                ReponseDialog = TxtMotif.Text
                Me.Close()
            End If
        End If
    End Sub
End Class