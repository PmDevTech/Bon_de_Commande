Public Class Exercice_Par_Defaut
    Private Sub Exercice_Par_Defaut_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        loadExercice()
        If cmbExercice.Properties.Items.Count = 0 Or cmbExercice.Text.Trim().Length = 0 Then
            DesactiveBtn(btnDefinir)
        End If
    End Sub
    Private Sub DesactiveBtn(btn As DevExpress.XtraEditors.SimpleButton)
        btn.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat
        btn.Appearance.BackColor = Color.Bisque
        btn.Enabled = False
    End Sub
    Private Sub ActiveBtn(btn As DevExpress.XtraEditors.SimpleButton)
        btn.Appearance.BackColor = Color.Empty
        btn.Enabled = True
        btn.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default
    End Sub
    Private Sub loadExercice()
        Try
            cmbExercice.Properties.Items.Clear()
            cmbExercice.ResetText()
            query = "select * from T_COMP_EXERCICE where etat <='1' ORDER BY libelle"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cmbExercice.Properties.Items.Add(rw("libelle").ToString)
                If Val(rw("Encours")) = 1 Then
                    cmbExercice.Text = rw("libelle").ToString
                End If
            Next
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & vbNewLine & ex.ToString())
        End Try
    End Sub

    Private Sub cmbExercice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbExercice.SelectedIndexChanged
        DesactiveBtn(btnDefinir)
        If cmbExercice.SelectedIndex > -1 Then
            query = "SELECT Encours FROM t_comp_exercice WHERE libelle='" & EnleverApost(cmbExercice.Text) & "'"
            Dim Statut As String = ExecuteScallar(query)
            If Val(Statut) = 0 Then
                ActiveBtn(btnDefinir)
            End If
        End If
    End Sub

    Private Sub btnDefinir_Click(sender As Object, e As EventArgs) Handles btnDefinir.Click
        If cmbExercice.SelectedIndex = -1 Then
            SuccesMsg("Veuillez choisir un exercice")
            Exit Sub
        End If
        query = "UPDATE t_comp_exercice SET Encours=0"
        ExecuteNonQuery(query)
        query = "UPDATE t_comp_exercice SET Encours=1 WHERE libelle='" & EnleverApost(cmbExercice.Text) & "'"
        ExecuteNonQuery(query)
        DesactiveBtn(btnDefinir)
    End Sub
End Class