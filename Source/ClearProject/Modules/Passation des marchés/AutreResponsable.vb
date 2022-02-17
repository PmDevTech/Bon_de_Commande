Public Class AutreResponsable
    Public InfosResponsable As String = String.Empty
    Private Sub AutreResponsable_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        txtNom.Focus()
    End Sub

    Private Sub btOK_Click(sender As Object, e As EventArgs) Handles btOK.Click
        If txtNom.Text.Trim().Length = 0 Then
            FailMsg("Veuillez saisir le nom")
            txtNom.Select()
            Exit Sub
        End If

        If txtPrenom.Text.Trim().Length = 0 Then
            FailMsg("Veuillez saisir le(s) prénom(s)")
            txtPrenom.Select()
            Exit Sub
        End If

        If txtContact.Text.Trim().Length = 0 Then
            FailMsg("Veuillez saisir le contact")
            txtContact.Select()
            Exit Sub
        End If

        If txtFonction.Text.Trim().Length = 0 Then
            FailMsg("Veuillez saisir la fonction")
            txtFonction.Select()
            Exit Sub
        End If

        Me.DialogResult = DialogResult.OK
        Me.InfosResponsable = txtNom.Text.Trim() & ";" & txtPrenom.Text.Trim() & ";" & txtContact.Text.Trim() & ";" & txtFonction.Text.Trim() & ";" & txtStructure.Text.Trim()
        Me.Close()

    End Sub

    Private Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Me.DialogResult = DialogResult.Abort
        Me.Close()
    End Sub

    Private Sub AutreResponsable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
    End Sub
End Class