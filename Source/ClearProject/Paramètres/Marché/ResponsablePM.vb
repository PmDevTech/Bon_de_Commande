Public Class ResponsablePM

    Private Sub BtEnregistrer_Click(sender As System.Object, e As System.EventArgs) Handles BtEnregistrer.Click
        Dim EmailEmp As String = ""
        Try
            If CmbResposable.SelectedIndex <> -1 Then
                'EmailEmp = ExecuteScallar("select EMP_EMAIL from t_grh_employe WHERE EMP_ID='" & CInt(CmbResposable.Text.Split(" | ")(0)) & "'")
                'If EmailEmp.ToString = "" Then
                '    FailMsg("Le responsable selectionné n'à pas d'email")
                '    CmbResposable.Select()
                '    Exit Sub
                'End If

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

    Private Sub ResponsablePM_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        CmbResposable.Text = ""
        CmbResposable.Properties.Items.Clear()
        query = "SELECT EMP_ID, EMP_NOM, EMP_PRENOMS, ResponsablePM FROM t_grh_employe where PROJ_ID='" & ProjetEnCours & "' AND EMP_EMAIL<>''" 'Tout les employé qui ont au un email
        Dim dt1 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt1.Rows
            CmbResposable.Properties.Items.Add(GetNewCode(rw("EMP_ID").ToString) & " | " & MettreApost(rw("EMP_NOM").ToString) & " " & MettreApost(rw("EMP_PRENOMS").ToString))

            If rw("ResponsablePM").ToString = "1" Then
                CmbResposable.Text = GetNewCode(rw("EMP_ID").ToString) & " | " & MettreApost(rw("EMP_NOM").ToString) & " " & MettreApost(rw("EMP_PRENOMS").ToString)
            End If
        Next
    End Sub
End Class