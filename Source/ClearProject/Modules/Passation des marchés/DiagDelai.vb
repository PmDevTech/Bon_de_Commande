Public Class DiagDelai

    Private Sub ValideD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ValideD.Click
        If (RdDelaiNormal.Checked = True) Then
            If (NbreD.Value <> 0 And UniteD.Text <> "") Then
                DelaiEtap = NbreD.Value.ToString & " " & UniteD.Text
            Else
                DelaiEtap = ""
            End If
        ElseIf (RdDelaiDAO.Checked = True) Then
            DelaiEtap = "DE-DAO"
        End If

        Me.Close()
    End Sub

    Private Sub DiagDelai_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (DelaiEtap <> "") Then
            If (DelaiEtap = "DE-DAO") Then
                RdDelaiDAO.Checked = True
            Else
                RdDelaiNormal.Checked = True
                Dim Part() As String = DelaiEtap.Split(" "c)
                NbreD.Value = CInt(Part(0))
                UniteD.Text = Part(1)
                SuppD.Visible = True
            End If
        Else
            RdDelaiNormal.Checked = True
            SuppD.Visible = False
        End If
    End Sub

    Private Sub SuppD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SuppD.Click
        DelaiEtap = ""
        Me.Close()
    End Sub

    Private Sub RdDelaiNormal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdDelaiNormal.CheckedChanged
        VerifRadio()
    End Sub
    Private Sub VerifRadio()
        If (RdDelaiNormal.Checked = True) Then
            NbreD.Enabled = True
            UniteD.Enabled = True
        Else
            NbreD.Value = 0
            NbreD.Enabled = False
            UniteD.Text = ""
            UniteD.Enabled = False
        End If
    End Sub
    Private Sub RdDelaiDAO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdDelaiDAO.CheckedChanged
        VerifRadio()
    End Sub
End Class