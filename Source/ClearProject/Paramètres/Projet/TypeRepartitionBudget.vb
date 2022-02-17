Imports MySql.Data.MySqlClient

Public Class TypeRepartitionBudget

    Private Sub TypeRepartitionBudget_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        VerifRepartition()
    End Sub

    Private Sub VerifRepartition()
        query = "select UniteRepartitionBudget from T_ParamTechProjet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 = ExcecuteSelectQuery(query)
        If dt0.Rows.Count > 0 Then
            Dim rw As DataRow = dt0.Rows(0)
            If (rw(0).ToString <> "") Then
                CmbTypeRepart.Text = rw(0).ToString

                ''Si déjà fait alors étape 2
                RepartitionMontantConvention.Size = New Point(1000, 500)
                RepartitionMontantConvention.ShowDialog()
                Me.Close()
                '**************************
            Else
                CmbTypeRepart.Text = ""
            End If

        End If
        query = "select count(*) from T_Partition as P, T_Partition_Budget as B where P.CodePartition=B.CodePartition and P.CodeProjet='" & ProjetEnCours & "'"
        Dim nbre = Val(ExecuteScallar(query))
        If nbre > 0 Then
            CmbTypeRepart.Enabled = False
            BtEnrg.Enabled = False
        Else
            CmbTypeRepart.Enabled = True
            BtEnrg.Enabled = True
        End If
    End Sub

    Private Sub BtEnrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnrg.Click
        If (CmbTypeRepart.Text <> "") Then
            query = "update T_ParamTechProjet set UniteRepartitionBudget='" & CmbTypeRepart.Text & "' where CodeProjet='" & ProjetEnCours & "'"
            ExecuteNonQuery(query)
            RepartitionMontantConvention.Size = New Point(1000, 500)
            RepartitionMontantConvention.ShowDialog()
            Me.Close()
        End If
    End Sub
End Class