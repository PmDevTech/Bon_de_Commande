Imports MySql.Data.MySqlClient

Public Class DecalageActivites

    Dim nbSelect As Decimal = 0

    Private Sub DecalageActivites_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        nbSelect = 0
    End Sub

    Private Sub RdDebutApresNotif_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdDebutApresNotif.CheckedChanged
        NumDelai.Enabled = RdDebutApresNotif.Checked
        CmbUnitDelai.Enabled = RdDebutApresNotif.Checked

        If (RdDebutApresNotif.Checked = False) Then
            NumDelai.EditValue = 0
            CmbUnitDelai.Text = ""
        End If
    End Sub

    Private Sub GridDecalage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridDecalage.Click

        If (ViewDecalage.RowCount > 0) Then
            If (ViewDecalage.GetRow(ViewDecalage.FocusedRowHandle)(2) = True) Then
                nbSelect = nbSelect - 1
            Else
                nbSelect = nbSelect + 1
            End If
            ViewDecalage.GetRow(ViewDecalage.FocusedRowHandle)(2) = Not ViewDecalage.GetRow(ViewDecalage.FocusedRowHandle)(2)
            ColorRowGrid(ViewDecalage, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewDecalage, "[*]=true", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)

            LblCpteActiv.Text = "Activités sélectionnées : " & nbSelect.ToString & " / " & ViewDecalage.RowCount.ToString
        End If

    End Sub

    Private Sub BtQuitter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        Me.Close()
    End Sub
    Dim DrX As DataRow
    Private Sub BtValider_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtValider.Click

        DebutChargement()
        For LigNe As Integer = 0 To ViewDecalage.RowCount - 1

            If (ViewDecalage.GetRow(Ligne)(2) = True) Then

                DrX = ViewDecalage.GetDataRow(Ligne)
                Dim xDebut As Date = CDate(TxtDateNotif.Text)

                If (RdDebutApresNotif.Checked = True) Then
                    If (NumDelai.EditValue = 0 Or CmbUnitDelai.Text = "") Then
                        MsgBox("Définir correctement la période de latence!", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If

                    If (CmbUnitDelai.Text = "Ans") Then
                        xDebut = xDebut.AddYears(NumDelai.EditValue)
                    ElseIf (CmbUnitDelai.Text = "Mois") Then
                        xDebut = xDebut.AddMonths(NumDelai.EditValue)
                    ElseIf (CmbUnitDelai.Text = "Semaines") Then
                        xDebut = xDebut.AddDays(NumDelai.EditValue * 7)
                    Else
                        xDebut = xDebut.AddDays(NumDelai.EditValue)
                    End If
                End If

                'conversion de la date
                Dim str2(3) As String
                str2 = xDebut.ToShortDateString.Split("/")
                Dim tempdt2 As String = String.Empty
                For j As Integer = 2 To 0 Step -1
                    tempdt2 += str2(j) & "-"
                Next
                tempdt2 = tempdt2.Substring(0, 10)

                Dim str3(3) As String
                str3 = xDebut.AddDays(CInt(DrX(7))).ToShortDateString.Split("/")
                Dim tempdt3 As String = String.Empty
                For j As Integer = 2 To 0 Step -1
                    tempdt3 += str3(j) & "-"
                Next
                tempdt3 = tempdt3.Substring(0, 10)


               query= "update T_Partition set DateDebutPartition='" & tempdt2 & "', DateFinPartition='" & tempdt3 & "', DateModif='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "', Operateur='" & CodeUtilisateur & "' where CodePartition='" & DrX(1).ToString & "'"
                ExecuteNonQuery(query)



            End If

        Next
        FinChargement()

        MsgBox("Mise à jour terminée avec succès.", MsgBoxStyle.Information)
        Me.Close()

    End Sub
End Class