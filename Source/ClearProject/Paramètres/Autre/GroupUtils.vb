Imports MySql.Data.MySqlClient

Public Class GroupUtils

    Dim dtGroup = New DataTable()
    Dim DrX As DataRow

    Dim CodeG As String

    Private Sub GroupUtils_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RemplirDossier()
        RemplirGroup()
    End Sub

    Private Sub RemplirDossier(Optional ByVal Appercu As String = "")
        ListeDossier.Nodes.Clear()
        Dim cptr As Decimal = 0


        query = "select LibelleRubrique, CodeRubrique from T_Rubrique order by Ordre"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            ListeDossier.Nodes.Add({MettreApost(rw(0).ToString), MettreApost(rw(1).ToString)})

            Dim Enfts As Decimal = 0
            Dim TouChk As String = "O"
            Dim nbChk As Decimal = 0

            query = "select LibelleFenetre, CodeFenetre, NomFenetre from T_Fenetre where CodeRubrique='" & rw(1).ToString & "' order by LibelleFenetre"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw0 As DataRow In dt0.Rows
                ListeDossier.Nodes(cptr).Nodes.Add({MettreApost(rw0(0).ToString), MettreApost(rw0(2).ToString)})

                If (Appercu = "Visuel") Then
                    Dim CaseC As String = Coche(rw0(2).ToString)
                    If (CaseC = "O") Then
                        ListeDossier.Nodes(cptr).Nodes(Enfts).CheckState = CheckState.Checked
                        ListeDossier.Nodes(cptr).Expanded = True
                        nbChk += 1
                    ElseIf (CaseC = "X") Then
                        ListeDossier.Nodes(cptr).Nodes(Enfts).CheckState = CheckState.Indeterminate
                        ListeDossier.Nodes(cptr).Expanded = True
                        TouChk = "X"
                        nbChk += 1
                    End If
                End If

                Enfts += 1
            Next

            If (TouChk = "O" And nbChk = Enfts And nbChk > 0) Then
                ListeDossier.Nodes(cptr).CheckState = CheckState.Checked
            ElseIf (nbChk <= 0) Then
                ListeDossier.Nodes(cptr).CheckState = CheckState.Unchecked
            Else
                ListeDossier.Nodes(cptr).CheckState = CheckState.Indeterminate
            End If

            If (Enfts = 0) Then
                ListeDossier.Nodes(cptr).Nodes.Add({MettreApost(rw(0).ToString), MettreApost(rw(0).ToString)})
            End If

            cptr += 1
        Next
    End Sub

    Private Function Coche(ByVal fen As String) As String

        Dim Niv As String = "N"
        query = "select AttributGroup from T_GroupUtils where CodeGroup='" & CodeG & "' AND AttributGroup LIKE '%&" & fen & "&%'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            Niv = "O"
            'Else
            'Niv = "X"
        End If

        Return Niv
    End Function

    Private Sub RemplirGroup()

        dtGroup.Columns.Clear()
        dtGroup.Columns.Add("CodeX", Type.GetType("System.String"))
        dtGroup.Columns.Add("Groupe", Type.GetType("System.String"))
        dtGroup.Columns.Add("Attributs", Type.GetType("System.String"))
        dtGroup.Rows.Clear()

        Dim NbTotal As Decimal = 0
        query = "select CodeGroup, AttributGroup from T_GroupUtils where CodeProjet='" & ProjetEnCours & "' and CodeGroup not in ('Niveau0','Administrateur') order by CodeGroup"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows

            NbTotal += 1
            Dim drS = dtGroup.NewRow()

            drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
            drS(1) = rw(0).ToString
            drS(2) = rw(1).ToString

            dtGroup.Rows.Add(drS)

        Next

        GridGroup.DataSource = dtGroup

        ViewGroup.Columns(0).Visible = False
        ViewGroup.Columns(1).Width = 206
        ViewGroup.Columns(2).Visible = False
        ViewGroup.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewGroup, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

        CodeG = ""
    End Sub

    Private Sub GridGroup_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridGroup.DoubleClick

        If (ViewGroup.RowCount > 0) Then
            DrX = ViewGroup.GetDataRow(ViewGroup.FocusedRowHandle)
            CodeG = DrX(1).ToString

            TxtGroup.Properties.ReadOnly = True
            TxtGroup.Text = CodeG
            BtRetour.Visible = True

            ColorRowGrid(ViewGroup, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewGroup, "[Groupe]='" & CodeG & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)

            RemplirDossier("Visuel")
        Else
            CodeG = ""
        End If

    End Sub

    Private Sub BtRetour_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtRetour.Click

        TxtGroup.Properties.ReadOnly = False
        TxtGroup.Text = ""
        RemplirGroup()
        RemplirDossier()
        BtRetour.Visible = False
        CodeG = ""

    End Sub

    Private Sub TxtGroup_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtGroup.TextChanged

        If (TxtGroup.Text.ToLower = "administrateur" Or TxtGroup.Text.ToLower = "niveau0") Then
            BtEnregistrer.Enabled = False
        Else
            BtEnregistrer.Enabled = True
        End If

    End Sub

    Private Sub BtEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click

        If (TxtGroup.Text <> "") Then

            Dim Attribut As String = ""
            For k As Integer = 0 To ListeDossier.Nodes.Count - 1

                For n As Decimal = 0 To ListeDossier.Nodes(k).Nodes.Count - 1

                    If (ListeDossier.Nodes(k).Nodes(n).CheckState = CheckState.Indeterminate) Then
                        Attribut = Attribut & "&" & "RO_" & ListeDossier.Nodes(k).Nodes(n).Item(1).ToString & "&"
                    ElseIf (ListeDossier.Nodes(k).Nodes(n).CheckState = CheckState.Checked) Then
                        Attribut = Attribut & "&" & ListeDossier.Nodes(k).Nodes(n).Item(1).ToString & "&"
                    End If

                Next

            Next

            If (Attribut = "") Then
                SuccesMsg("Aucun accès attribué à ce groupe.")
            Else

                If (TxtGroup.Properties.ReadOnly = False) Then

                    Dim nbOc As Decimal = 0
                    query = "select Count(*) from T_GroupUtils where CodeGroup='" & TxtGroup.Text & "'"
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    Dim rw As DataRow = dt0.Rows(0)
                    nbOc = CInt(rw(0))

                    If (nbOc > 0) Then
                        SuccesMsg("Ce code existe déjà.")
                        TxtGroup.Focus()
                        Exit Sub
                    End If

                    query = "INSERT INTO T_GroupUtils VALUES('" & TxtGroup.Text & "','" & Attribut & "','" & ProjetEnCours & "')"
                    ExecuteNonQuery(query)
                Else
                    query = "UPDATE T_GroupUtils SET AttributGroup='" & Attribut & "' where CodeGroup='" & TxtGroup.Text & "'"
                    ExecuteNonQuery(query)
                End If

                BtRetour_Click(Me, e)
                TxtGroup.Focus()

            End If

        End If

    End Sub

    Private Sub SupprimerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SupprimerToolStripMenuItem.Click
        If ViewGroup.RowCount > 0 And ViewGroup.FocusedRowHandle > -1 Then
            DrX = ViewGroup.GetDataRow(ViewGroup.FocusedRowHandle)
            CodeG = DrX(1).ToString

            query = "SELECT * FROM t_operateur WHERE AccesOperateur='" & CodeG & "'"
            Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
            If dtVerif.Rows.Count > 0 Then
                FailMsg("Ce groupe contient des utilisateurs.")
                Exit Sub
            End If
            If ConfirmMsg("Voulez-vous vraiment supprimer?") = DialogResult.Yes Then
                DeleteRecords2("T_GroupUtils", "CodeGroup", CodeG.ToString)
                SuccesMsg("Groupe supprimé avec succès")
            End If
        End If
    End Sub
End Class