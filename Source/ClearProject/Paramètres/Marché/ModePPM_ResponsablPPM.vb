Imports MySql.Data.MySqlClient
Imports ClearProject.GestComptable

Public Class ModePPM_ResponsablPPM
    Private Sub ModePPM_ResponsablPPM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ModePPM()
    End Sub

    Private Sub ModePPM()
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
    End Sub

    Private Sub XtraTabControl1_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles XtraTabControl1.SelectedPageChanged
        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            ModePPM()
        ElseIf XtraTabControl1.SelectedTabPageIndex = 1 Then
            CmbResposable.Text = ""
            CmbResposable.Properties.Items.Clear()
            Dim ResponsablePM As String = ""
            query = "SELECT EMP_ID, EMP_NOM, EMP_PRENOMS, ResponsablePM FROM t_grh_employe where PROJ_ID='" & ProjetEnCours & "' AND EMP_EMAIL<>''" 'Tout les employé qui ont au un email
            Dim dt1 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt1.Rows
                CmbResposable.Properties.Items.Add(GetNewCode(rw("EMP_ID").ToString) & " | " & MettreApost(rw("EMP_NOM").ToString) & " " & MettreApost(rw("EMP_PRENOMS").ToString))

                If rw("ResponsablePM") Then
                    ResponsablePM = GetNewCode(rw("EMP_ID").ToString) & " | " & MettreApost(rw("EMP_NOM").ToString) & " " & MettreApost(rw("EMP_PRENOMS").ToString)
                End If
            Next
            CmbResposable.Text = ResponsablePM.ToString
        End If
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
                        If PlanMarche.CanFocus() Then PlanMarche.Close()
                    End If
                End If
            ElseIf ModePlan = "PPSD" Then
                If ConfirmMsg("Voulez-vous vraiment enregistré ?") = DialogResult.Yes Then
                    query = "UPDATE t_paramtechprojet SET ModePlanMarche='" & ModePlan & "' WHERE CodeProjet='" & ProjetEnCours & "'"
                    ExecuteNonQuery(query)
                    SuccesMsg("Enregistrement effectué avec succès")
                    rdGenere.Enabled = False
                    If PlanMarche.CanFocus() Then PlanMarche.Close()
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
            SuccesMsg("Impossible d'apporter des modifications")
        End If

    End Sub

    Private Sub BtEnregistrer_Click(sender As Object, e As EventArgs) Handles BtEnregistrer.Click
        Try
            If CmbResposable.SelectedIndex <> -1 Then
                ExecuteNonQuery("update t_grh_employe set ResponsablePM=FALSE")
                ExecuteNonQuery("update t_grh_employe set ResponsablePM=TRUE WHERE EMP_ID='" & CInt(CmbResposable.Text.Split(" | ")(0)) & "'")

                SuccesMsg("Enregistrement effectué avec succès.")
            Else
                SuccesMsg("Veuillez selectionné un responsable dans la liste")
                CmbResposable.Select()
            End If
        Catch ex As Exception
            FailMsg(ex.ToString())
        End Try
    End Sub

    Private Sub rdPPSD_CheckedChanged(sender As Object, e As EventArgs) Handles rdPPSD.CheckedChanged
        cmbModePlan.Text = ""
        cmbModePlan.Enabled = False
    End Sub

    Private Sub rdGenere_CheckedChanged(sender As Object, e As EventArgs) Handles rdGenere.CheckedChanged
        cmbModePlan.Enabled = True
    End Sub
End Class