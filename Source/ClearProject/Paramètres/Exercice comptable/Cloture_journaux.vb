Public Class Cloture_journaux

    Private Sub Cloture_journaux_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        InitTabTrue()
        remplirjournal(LgListJournal, GridView1)
    End Sub
    Private Sub InitTabTrue()
        For n As Decimal = 0 To 499
            TabTrue(n) = False
        Next
        nbTab = 0
    End Sub
    Private Sub BtEnrg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrg.Click
        Try
            If Not IsDate(Datelimite.Text) Then
                SuccesMsg("Veuillez entrer une date.")
                Exit Sub
            End If
            'convertion de la date en date anglaise
            Dim str(3) As String
            str = Datelimite.Text.Split("/")
            Dim tempdt As String = String.Empty
            For j As Integer = 2 To 0 Step -1
                tempdt += str(j) & "-"
            Next
            tempdt = tempdt.Substring(0, 10)

            Dim clotpart As Boolean = False
            Dim clotdef As Boolean = False

            If (GridView1.RowCount > 0) Then

                For i As Integer = 0 To GridView1.RowCount - 1

                    If GridView1.GetRowCellValue(i, "Choix") = True Then

                        If RadioButton1.Checked = True Then

                           query= "insert into T_COMP_CLOTURE values (NULL,'" & dtcombj.Rows(i).Item(1).ToString & "','" & RadioButton1.Text & "','" & tempdt & "')"
                            ExecuteNonQuery(query)

                           query= "update T_COMP_JOURNAL set CLOTURE_J='partiel' where CODE_J='" & dtcombj.Rows(i).Item(1).ToString & "'"
                            ExecuteNonQuery(query)

                            clotpart = True

                        ElseIf RadioButton2.Checked = True Then

                           query= "insert into T_COMP_CLOTURE values (NULL,'" & dtcombj.Rows(i).Item(1).ToString & "','" & RadioButton2.Text & "','" & tempdt & "')"
                            ExecuteNonQuery(query)

                           query= "update T_COMP_JOURNAL set CLOTURE_J='cloturer' where CODE_J='" & dtcombj.Rows(i).Item(1).ToString & "'"
                            ExecuteNonQuery(query)

                            clotdef = True

                        End If

                    End If
                Next

                If RadioButton1.Checked = True Then
                    If clotpart = False Then
                        SuccesMsg("Veuillez cocher un journal.")
                    Else
                        SuccesMsg("Clôture partielle effectuée.")
                    End If
                ElseIf RadioButton2.Checked = True Then
                    If clotdef = False Then
                        SuccesMsg("Veuillez cocher un journal")
                    Else
                        SuccesMsg("Clôture définitive effectuée.")
                    End If
                End If

            End If

        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub LgListJournal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LgListJournal.Click
        If (GridView1.RowCount > 0) Then
            drx = GridView1.GetDataRow(GridView1.FocusedRowHandle)
            TabTrue(GridView1.FocusedRowHandle) = Not (TabTrue(GridView1.FocusedRowHandle))
            remplirjournal(LgListJournal, GridView1)
            nbTab = GridView1.RowCount
        End If
    End Sub
End Class