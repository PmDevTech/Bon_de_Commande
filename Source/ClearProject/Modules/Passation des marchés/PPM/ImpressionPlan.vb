Public Class ImpressionPlan
    Public IDPlan As Decimal = 0

    Private Sub ModifMethode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        If IDPlan <= 0 Then
            Me.Close()
        End If
        CmbTypeplan.ResetText()
        CmbTypeplan.Select()
    End Sub

    Private Sub BtEnregComm_Click(sender As Object, e As EventArgs) Handles BtEnregComm.Click
        If CmbTypeplan.IsRequiredControl("Veuillez choisir le type de plan à imprimer") Then
            CmbTypeplan.Select()
        End If
        If CmbTypeplan.Text = "Plan résumé" Then

        ElseIf CmbTypeplan.Text = "Plan détaillé" Then

        End If
    End Sub
End Class