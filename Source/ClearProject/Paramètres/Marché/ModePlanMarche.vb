Imports ClearProject.GestComptable
Public Class ModePlanMarche
    Private Sub ModePlanMarche_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        query = "SELECT ModePlanMarche FROM t_paramtechprojet WHERE CodeProjet='" & ProjetEnCours & "'"
        Dim ModePPM As String = ExecuteScallar(query)
        If ModePPM = "Genere" Then
            rdGenere.Checked = True
            rdPPSD.Enabled = False
        ElseIf ModePPM = "PPSD" Then
            rdPPSD.Checked = True
            rdGenere.Enabled = False
        Else
            rdGenere.Enabled = True
            rdGenere.Checked = True
            rdPPSD.Enabled = True
            rdPPSD.Checked = False
        End If
        If rdGenere.Checked Then
            cmbModePlan.Enabled = True
        Else
            cmbModePlan.Enabled = False
            Exit Sub
        End If
        'cmbModePlan.Properties.Items.Clear()
        cmbModePlan.Text = ""
        query = "SELECT ElaboPPM FROM t_paramtechprojet WHERE CodeProjet='" & ProjetEnCours & "'"
        Dim ElaboPPM As String = ExecuteScallar(query)
        cmbModePlan.Text = ElaboPPM
        'If ElaboPPM = "" Then
        '    cmbModePlan.Properties.Items.Add("Tous les bailleurs")
        '    cmbModePlan.Properties.Items.Add("Bailleur")
        'BtEnrg.Enabled = True
        'Else
        '    cmbModePlan.Text = ElaboPPM
        '    cmbModePlan.Properties.Items.Add(ElaboPPM)
        'BtEnrg.Enabled = False
        'End If
    End Sub

    Private Sub BtEnrg_Click(sender As Object, e As EventArgs) Handles BtEnrg.Click
        query = "SELECT ModePlanMarche FROM t_paramtechprojet WHERE CodeProjet='" & ProjetEnCours & "'"
        Dim ModePPM As String = ExecuteScallar(query)
        If ModePPM = "" Then
            Dim ModePlan As String = ""
            If rdGenere.Checked Then
                ModePlan = "Genere"
            ElseIf rdPPSD.Checked Then
                ModePlan = "PPSD"
            End If
            If ModePlan = "Genere" Then
                If cmbModePlan.SelectedIndex = -1 Then
                    SuccesMsg("Veuillez selectionner un élément dans la liste")
                Else
                    If ConfirmMsg("Voulez-vous vraiment enregistré ?") = DialogResult.Yes Then
                        query = "UPDATE t_paramtechprojet SET ElaboPPM ='" & cmbModePlan.Text & "', ModePlanMarche='" & ModePlan & "' WHERE CodeProjet='" & ProjetEnCours & "'"
                        ExecuteNonQuery(query)
                        SuccesMsg("Enregistrement effectué avec succès")
                        rdPPSD.Enabled = False
                    End If
                End If
            ElseIf ModePlan = "PPSD" Then
                If ConfirmMsg("Voulez-vous vraiment enregistré ?") = DialogResult.Yes Then
                    query = "UPDATE t_paramtechprojet SET ModePlanMarche='" & ModePlan & "' WHERE CodeProjet='" & ProjetEnCours & "'"
                    ExecuteNonQuery(query)
                    SuccesMsg("Enregistrement effectué avec succès")
                    rdGenere.Enabled = False
                End If
            End If
        ElseIf ModePPM = "Genere" Then
            If ConfirmMsg("Voulez-vous vraiment modifié la génération du PPM ?") = DialogResult.Yes Then
                query = "UPDATE t_paramtechprojet SET ElaboPPM='" & cmbModePlan.Text & "' WHERE CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
                SuccesMsg("Enregistrement effectué avec succès")
                If PlanMarche.CanFocus() Then
                    PlanMarche.Close()
                End If
            End If
        Else
            SuccesMsg("Impossible d'apporter des modfications")
        End If

    End Sub

    Private Sub rdPPSD_CheckedChanged(sender As Object, e As EventArgs) Handles rdPPSD.CheckedChanged
        cmbModePlan.Text = ""
        cmbModePlan.Enabled = False
    End Sub

    Private Sub rdGenere_CheckedChanged(sender As Object, e As EventArgs) Handles rdGenere.CheckedChanged
        cmbModePlan.Enabled = True
    End Sub
End Class