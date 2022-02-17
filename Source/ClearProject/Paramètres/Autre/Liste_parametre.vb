Public Class Liste_parametre

    Private Sub AjouterUneRubriqueToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AjouterUneRubriqueToolStripMenuItem.Click
        Try
            Me.Close()
            Parametrage.ShowDialog()
        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Liste_parametre_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        Try
            'remplir le datagrid
            remplirDG3("TYPE_RUB", "LIBELLE_RUB", "T_COMP_RUBRIQUE", dglistparam)
        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub
End Class