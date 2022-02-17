Imports System.Windows.Forms
Public Class ChoixFrequenceCollecte

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If Not RbChoix1.Checked And Not RbChoix2.Checked And Not RbChoix3.Checked And Not RbChoix4.Checked Then
            SuccesMsg("Veuillez choisir une fréquence.")
            Exit Sub
        End If

        If (RbChoix1.Checked = True) Then
            If TbChoix1.Text.Trim().Length = 0 Then
                Exit Sub
            End If
            If CbChoix1.SelectedIndex = -1 Then
                Exit Sub
            End If
            If Val(TbChoix1.Text.Trim()) = 0 Then
                SuccesMsg("Veuillez entrer un nombre correcte")
                TbChoix1.Select()
                Exit Sub
            End If
            FrequenceDialogResult = "Tous les " & Val(TbChoix1.Text.Trim()) & " " & CbChoix1.Text

        ElseIf (RbChoix2.Checked = True) Then
            If Val(TbChoix2.Text.Trim()) = 0 Then
                SuccesMsg("Veuillez entrer un nombre correcte")
                TbChoix2.Select()
                Exit Sub
            End If
            FrequenceDialogResult = "Chaque " & TbChoix2.Text & " de mois"

        ElseIf (RbChoix3.Checked = True) Then

            If (RbLundi.Checked = True) Then FrequenceDialogResult = "Chaque Lundi"
            If (RbMardi.Checked = True) Then FrequenceDialogResult = "Chaque Mardi"
            If (RbMercredi.Checked = True) Then FrequenceDialogResult = "Chaque Mercredi"
            If (RbJeudi.Checked = True) Then FrequenceDialogResult = "Chaque Jeudi"
            If (RbVendredi.Checked = True) Then FrequenceDialogResult = "Chaque Vendredi"
            If (RbSamedi.Checked = True) Then FrequenceDialogResult = "Chaque Samedi"
            If (RbDimanche.Checked = True) Then FrequenceDialogResult = "Chaque Dimanche"

        ElseIf (RbChoix4.Checked = True) Then
            FrequenceDialogResult = "A la fin de l'activité"
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub RbChoix1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbChoix1.CheckedChanged, RbChoix2.CheckedChanged, RbChoix3.CheckedChanged, RbChoix4.CheckedChanged
        If RbChoix1.Checked Then
            TbChoix1.Enabled = True
            CbChoix1.Enabled = True
            TbChoix2.Enabled = False
            GroupBox1.Enabled = False
        ElseIf RbChoix2.Checked Then
            TbChoix1.Enabled = False
            CbChoix1.Enabled = False
            GroupBox1.Enabled = False
            TbChoix2.Enabled = True
        ElseIf RbChoix3.Checked Then
            TbChoix1.Enabled = False
            CbChoix1.Enabled = False
            TbChoix2.Enabled = False
            GroupBox1.Enabled = True
        ElseIf RbChoix4.Checked Then
            TbChoix1.Enabled = False
            CbChoix1.Enabled = False
            TbChoix2.Enabled = False
            GroupBox1.Enabled = False
        Else
            TbChoix1.Enabled = False
            CbChoix1.Enabled = False
            TbChoix2.Enabled = False
            GroupBox1.Enabled = False
        End If
    End Sub

    Private Sub ChoixFrequenceCollecte_Load(sender As Object, e As EventArgs) Handles Me.Load
        RbChoix1.Checked = False
        RbChoix2.Checked = False
        RbChoix3.Checked = False
        RbChoix4.Checked = False
        TbChoix1.ResetText()
        CbChoix1.SelectedIndex = -1
        CbChoix1.ResetText()
        TbChoix2.ResetText()
        RbLundi.Checked = True
        RbChoix1_CheckedChanged(RbChoix1, e)
    End Sub
End Class