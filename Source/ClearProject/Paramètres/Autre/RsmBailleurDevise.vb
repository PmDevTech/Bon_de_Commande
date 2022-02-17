Imports MySql.Data.MySqlClient

Public Class RsmBailleurDevise

    Private Sub RsmBailleurDevise_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        Bailleur()
        Devise()

    End Sub
    Private Sub Bailleur()

        CmbBailleur.Items.Clear()
        query = "select InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbBailleur.Items.Add(rw(0))
        Next
        CmbBailleur.Items.Add("Tous")

    End Sub
    Private Sub Devise()

        CmbDevise.Items.Clear()
        query = "select AbregeDevise from T_Devise"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbDevise.Items.Add(rw(0))
        Next

    End Sub

    Private Sub CmbBailleur_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbBailleur.SelectedIndexChanged
        If (CmbBailleur.Text = "Tous") Then
            TxtBailleur.Text = "TOUS LES BAILLEURS"
        Else

            query = "select NomBailleur from T_Bailleur where InitialeBailleur='" & CmbBailleur.Text & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                TxtBailleur.Text = MettreApost(rw(0).ToString)
            Next

        End If

    End Sub

    Private Sub CmbDevise_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbDevise.SelectedIndexChanged

        query = "select LibelleDevise from T_Devise where AbregeDevise='" & CmbDevise.Text & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            TxtDevise.Text = MettreApost(rw(0).ToString)
        Next

    End Sub

    Private Sub BtAnnuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAnnuler.Click
        ReponseDialog = ""
        ExceptRevue = ""
        Me.Close()
    End Sub

    Private Sub BtOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtOk.Click
        If (CmbBailleur.Text <> "" And CmbDevise.Text <> "") Then
            ReponseDialog = CmbBailleur.Text
            ExceptRevue = CmbDevise.Text
            Me.Close()
        End If

    End Sub
End Class