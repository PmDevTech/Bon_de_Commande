Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class LierCompteEmploye
    Public OperateurID As Decimal
    Public OperateurNomPren As String
    Dim IdEmploye As String()
    Private Sub Etat_sigfip_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        cmbEmploye.ResetText()
        LoadEmploye()
        cmbEmploye.Focus()
    End Sub
    Private Sub LoadEmploye()
        query = "SELECT * FROM t_grh_employe WHERE EMP_ID NOT IN(SELECT EMP_ID FROM t_operateur WHERE CodeOperateur<>'" & OperateurID & "')"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        cmbEmploye.Properties.Items.Clear()
        cmbEmploye.Properties.Items.Add("Non défini")
        ReDim IdEmploye(dt.Rows.Count)
        IdEmploye(0) = "-1"
        Dim cpte As Integer = 1
        Dim NomPrenoms As String
        For Each rw As DataRow In dt.Rows
            NomPrenoms = rw("EMP_MAT") & " " & MettreApost(rw("EMP_NOM")) & " " & MettreApost(rw("EMP_PRENOMS"))
            cmbEmploye.Properties.Items.Add(NomPrenoms.Trim())
            IdEmploye(cpte) = rw("EMP_ID")
            cpte += 1
        Next
        cmbEmploye.Text = OperateurNomPren
    End Sub

    Private Sub btOK_Click(sender As Object, e As EventArgs) Handles btOK.Click
        If cmbEmploye.SelectedIndex = -1 Then
            SuccesMsg("Sélectionner un employé dans la liste")
            cmbEmploye.Focus()
            Exit Sub
        End If

        Dim EMP_ID As String = "-1"
        If cmbEmploye.SelectedIndex > 0 Then
            EMP_ID = IdEmploye(cmbEmploye.SelectedIndex)
        End If

        query = "UPDATE t_operateur SET EMP_ID='" & EMP_ID & "' WHERE CodeOperateur='" & OperateurID & "'"
        ExecuteNonQuery(query)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class