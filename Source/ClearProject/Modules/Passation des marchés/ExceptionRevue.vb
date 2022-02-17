Public Class ExceptionRevue

    Private Sub RdMarcheExcep_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdMarcheExcep.CheckedChanged
        If (RdMarcheExcep.Checked = True) Then
            TxtNbMarche.Enabled = True
            GbExecMarche.Enabled = True
            ChkMarcheCC.Checked = True
        Else
            ChkMarcheCC.Checked = False
            TxtNbMarche.Text = ""
            TxtNbMarche.Enabled = False
            GbExecMarche.Enabled = False
        End If
    End Sub

    Private Sub ExceptionRevue_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        GbExecMarche.Enabled = False
        TxtNbMarche.Text = ""
        TxtNbMarche.Enabled = False
        RdAucuneExcep.Checked = True
        RdMarcheExcep.Checked = False
    End Sub

    Private Sub BtAnnuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAnnuler.Click
        ExceptRevue = ""
        Me.Close()
    End Sub

    Private Sub BtValider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtValider.Click
        ExceptRevue = ""
        If (RdMarcheExcep.Checked = True) Then

            If (TxtNbMarche.Text <> "") Then
                ExceptRevue = TxtNbMarche.Text

                If (ChkMarcheCC.Checked = True Or ChkMarcheAgence.Checked = True) Then
                    If (ChkMarcheCC.Checked = True) Then
                        ExceptRevue = ExceptRevue & " CC"
                    End If
                    If (ChkMarcheAgence.Checked = True) Then
                        ExceptRevue = ExceptRevue & "\AE"
                    End If
                Else
                    MsgBox("Veuillez renseigner les marchés concernés.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If

            Else
                MsgBox("Veuillez renseigner le nombre de marchés.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

        End If

        Me.Close()
    End Sub

    Private Sub TxtNbMarche_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNbMarche.TextChanged
        VerifSaisieMontant(TxtNbMarche)
    End Sub
End Class