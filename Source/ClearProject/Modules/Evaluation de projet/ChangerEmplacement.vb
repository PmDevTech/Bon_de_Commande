Imports MySql.Data.MySqlClient

Public Class ChangerEmplacement

    Private Sub ChangerEmplacement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        TxtNewCode.Text = ""
        CodeMere.Text = ""
        CmbCompo.Text = ""
        CmbSousCompo.Text = ""
        CmbSousCompo.Properties.Items.Clear()

        If (ReponseDialog <> "") Then

            TxtCodeInit.Text = ReponseDialog
            TxtLibInit.Text = Libelle(ReponseDialog)
            TxtNewLib.Text = Libelle(ReponseDialog)
            TxtSousCompo.Text = Mid(ReponseDialog, 1, 2) & " : " & Libelle(Mid(ReponseDialog, 1, 2))
            TxtCompo.Text = Mid(ReponseDialog, 1, 1) & " : " & Libelle(Mid(ReponseDialog, 1, 1))
            ChargerCompo()

        End If

    End Sub

    Private Function Libelle(ByVal strCode As String) As String

        Dim ValLib As String = ""
        query = "select LibellePartition from T_Partition where LibelleCourt='" & strCode & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            ValLib = MettreApost(rw(0).ToString)
        Next
        Return ValLib

    End Function

    Private Sub ChargerCompo()

        query = "select LibelleCourt, LibellePartition from T_Partition where LENGTH(LibelleCourt)='1' and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
        CmbCompo.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbCompo.Properties.Items.Add(rw(0).ToString & " : " & MettreApost(rw(1).ToString))
        Next


    End Sub

    Private Sub ChargerSousCompo(ByVal StrCode As String)

        query = "select LibelleCourt, LibellePartition from T_Partition where CodeClassePartition=2 and LibelleCourt<>'" & StrCode & "' and LibelleCourt like '" & Mid(CmbCompo.Text, 1, 1) & "%' and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
        CmbSousCompo.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbSousCompo.Properties.Items.Add(rw(0).ToString & " : " & MettreApost(rw(1).ToString))
        Next

    End Sub

    Private Sub CmbCompo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCompo.SelectedValueChanged
        ChargerSousCompo(Mid(ReponseDialog, 1, 2))
    End Sub

    Private Sub CmbSousCompo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSousCompo.SelectedValueChanged
        Dim cmbscomp() As String
        cmbscomp = CmbSousCompo.Text.Split(" : ")

        CodeMere.Text = ""
        TxtNewCode.Text = ""

        query = "select CodePartition from T_Partition where CodeClassePartition=2 and LibelleCourt='" & cmbscomp(0).ToString & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CodeMere.Text = rw(0).ToString
        Next
        TxtNewCode.Text = CodeNouvelleActivite(cmbscomp(0).ToString)

    End Sub

    Private Sub BtValider_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtValider.Click

        If (TxtNewCode.Text <> "" And CodeMere.Text <> "") Then


           query= "update T_Partition set CodePartitionMere='" & CodeMere.Text & "', LibelleCourt='" & TxtNewCode.Text & "' where LibelleCourt='" & TxtCodeInit.Text & "' and CodeProjet='" & ProjetEnCours & "'"
            ExecuteNonQuery(query)


            Me.Close()

        End If

    End Sub

    Private Sub BtQuitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        Me.Close()
    End Sub

End Class