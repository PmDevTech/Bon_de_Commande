Imports ClearProject.GestComptable
Public Class Equilibrer_Cloture
    Public Compte As String

    Private Sub LoadCompte()
        Try
            cmbCompte.Properties.Items.Clear()
            query = "select * from t_comp_sous_classe where CODE_SC LIKE '1%' ORDER BY CODE_SC"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cmbCompte.Properties.Items.Add(rw("CODE_SC").ToString & " - " & MettreApost(rw("LIBELLE_SC")))
            Next
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & vbNewLine & ex.ToString())
        End Try
    End Sub

    Private Sub Equilibrer_Cloture_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        LoadCompte()
    End Sub

    Private Sub BtEnrg_Click(sender As Object, e As EventArgs) Handles BtEnrg.Click
        If cmbCompte.SelectedIndex = -1 Then
            SuccesMsg("Veuillez choisir un compte.")
            Exit Sub
        End If
        Compte = Split(cmbCompte.Text, " - ")(0)
        Me.DialogResult = DialogResult.OK
    End Sub
End Class