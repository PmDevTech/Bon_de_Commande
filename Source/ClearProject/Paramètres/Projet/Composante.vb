Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient

Public Class Composante
    Dim dtCompo = New DataTable()
    Dim drx As DataRow
    Dim Code() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}
    Dim Modif As Boolean = False

    Private Sub Composante_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Rafraichir_Click(Me, e)
    End Sub

    Private Sub Composante_SousComposante_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ChargerCompo()
    End Sub

    Private Sub ChargerCompo()
        If Modif Then
            Modif = False
        End If
        dtCompo.Columns.Clear()
        dtCompo.Columns.Add("Choix", Type.GetType("System.String"))
        dtCompo.Columns.Add("Code", Type.GetType("System.String"))
        dtCompo.Columns.Add("Libellé", Type.GetType("System.String"))
        Dim cptr As Decimal = 0
        query = "select LibelleCourt, LibellePartition from T_Partition where LENGTH(LibelleCourt)=1 and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
        dtCompo.Rows.Clear()
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtCompo.NewRow()
            drS(0) = cptr
            drS(1) = rw(0).ToString
            drS(2) = MettreApost(rw(1).ToString)
            dtCompo.Rows.Add(drS)
        Next
        GridCompo.DataSource = dtCompo

        If ViewCompo.Columns("Choix").Visible = True Then
            ViewCompo.Columns("Choix").Visible = False
            ViewCompo.Columns("Code").Width = 50
            ViewCompo.OptionsView.ColumnAutoWidth = True
            ViewCompo.OptionsBehavior.AutoExpandAllGroups = True
            ViewCompo.VertScrollVisibility = True
            ViewCompo.HorzScrollVisibility = True
            ViewCompo.BestFitColumns()

            ViewCompo.Columns("Code").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewCompo.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        End If

    End Sub

    Private Sub TxtCompo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtCompo.KeyDown
        Try
            If (e.KeyCode = Keys.Enter) Then

                If TxtCompo.Text.Trim().Length = 0 Then
                    SuccesMsg("Veuillez saisir le libellé de la composante.")
                    Exit Sub
                End If

                If Modif Then
                    drx = ViewCompo.GetFocusedDataRow
                    Dim CodeComposante As String = drx("Code")
                    query = "Update T_Partition set LibellePartition='" & EnleverApost(TxtCompo.Text.Trim()) & "' where CodeProjet='" & ProjetEnCours & "' and CodeClassePartition='1' and LibelleCourt='" & CodeComposante & "'"
                    ExecuteNonQuery(query)
                    ViewCompo.SetFocusedRowCellValue("Libellé", TxtCompo.Text.Trim())
                    'ChargerCompo()
                    TxtCompo.ResetText()
                    TxtCompo.Focus()
                Else
                    query = "select Count(*) from T_Partition where CodeClassePartition=1 and CodeProjet='" & ProjetEnCours & "'"
                    Dim NbComposante As Decimal = Val(ExecuteScallar(query))
                    If NbComposante >= 26 Then
                        FailMsg("Vous avez atteint le nombre de composante autorisé." & vbNewLine & "Veuillez migrer vers une version supérieure.")
                        Exit Sub
                    End If
                    Dim NewCodeComposante As String = Code(NbComposante)
                    query = "insert into T_Partition values (NULL,'" & EnleverApost(TxtCompo.Text.Trim()) & "','','','','',NULL,'','',NULL,'1','" & ProjetEnCours & "','0','0','','" & NewCodeComposante & "','','','','','','','')"
                    ExecuteNonQuery(query)
                    ChargerCompo()
                    TxtCompo.ResetText()
                    TxtCompo.Focus()
                End If
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        
    End Sub

    Private Sub GridCompo_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridCompo.DoubleClick
        Try
            If ViewCompo.RowCount > 0 Then
                drx = ViewCompo.GetFocusedDataRow
                Dim IDL = drx(0).ToString
                ColorRowGrid(ViewCompo, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
                ColorRowGridAnal(ViewCompo, "[Choix]=" & IDL & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)
                drx = ViewCompo.GetDataRow(ViewCompo.FocusedRowHandle)
                Modif = True
                TxtCompo.Text = drx("Libellé").ToString
                TxtCompo.Select()
                TxtCompo.SelectionStart = TxtCompo.Text.Length
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub SupprimerComposanteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupprimerComposanteToolStripMenuItem.Click
        Try
            If ViewCompo.RowCount > 0 Then
                drx = ViewCompo.GetDataRow(ViewCompo.FocusedRowHandle)
                query = "select count(*) from T_Partition where LibelleCourt like '" & drx("Code").ToString & "%' AND LENGTH(LibelleCourt)>1 AND CodeProjet='" & ProjetEnCours & "'"
                Dim nbre As Decimal = ExecuteScallar(query)

                If (nbre > 0) Then
                    FailMsg("Cette composante contient des sous composantes.")
                Else
                    If ConfirmMsg("Voulez-vous vraiment supprimer la composante " & drx("Libellé") & "?") = DialogResult.Yes Then
                        query = "delete from T_Partition where CodeProjet='" & ProjetEnCours & "' and CodeClassePartition='1' and LibelleCourt='" & drx("Code").ToString & "'"
                        ExecuteNonQuery(query)
                        Modif = False
                        'ChargerCompo()
                        ViewCompo.DeleteRow(ViewCompo.FocusedRowHandle)
                        TxtCompo.Focus()
                    End If

                End If
            End If

        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Rafraichir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rafraichir.Click
        Modif = False
        TxtCompo.ResetText()
        ChargerCompo()
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If ViewCompo.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub GridCompo_Click(sender As Object, e As EventArgs) Handles GridCompo.Click
        If ViewCompo.RowCount > 0 Then
            drx = ViewCompo.GetFocusedDataRow
            Dim IDL = drx(0).ToString
            ColorRowGrid(ViewCompo, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewCompo, "[Choix]=" & IDL & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)
        End If
    End Sub
End Class