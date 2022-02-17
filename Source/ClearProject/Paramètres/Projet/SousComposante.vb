Imports MySql.Data.MySqlClient
Imports Microsoft.Office.Interop
Imports CrystalDecisions.CrystalReports.Engine

Public Class SousComposante

    Dim dtSousCompo = New DataTable()
    Dim drx As DataRow
    Dim Modif As Boolean = False

    Private Sub SousComposante_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        RafraichirToolStripMenuItem_Click(Me, e)
    End Sub

    Private Sub SousComposante_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ChargerCompo()
    End Sub

    Private Sub ChargerCompo()
        query = "select LibelleCourt, LibellePartition from T_Partition where LENGTH(LibelleCourt)=1 and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
        CmbCompo.Properties.Items.Clear()
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbCompo.Properties.Items.Add(rw(0).ToString & " : " & MettreApost(rw(1).ToString))
        Next
    End Sub
    Private Sub ChargerSousCompo(ByVal CodeComposante As String)
        If Modif Then
            Modif = False
        End If
        dtSousCompo.Columns.Clear()
        dtSousCompo.Columns.Add("Choix", Type.GetType("System.String"))
        dtSousCompo.Columns.Add("Code", Type.GetType("System.String"))
        dtSousCompo.Columns.Add("Libellé", Type.GetType("System.String"))
        Dim cptr As Decimal = 0
        query = "select LibelleCourt, LibellePartition from T_Partition where CodeClassePartition=2 and LibelleCourt like '" & CodeComposante & "%' and CodeProjet='" & ProjetEnCours & "' order by libellecourt"
        dtSousCompo.Rows.Clear()
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtSousCompo.NewRow()
            drS(0) = cptr
            drS(1) = rw(0).ToString
            drS(2) = MettreApost(rw(1).ToString)

            dtSousCompo.Rows.Add(drS)
        Next
        GridSousCompo.DataSource = dtSousCompo

        If ViewSousCompo.Columns("Choix").Visible = True Then
            ViewSousCompo.Columns("Choix").Visible = False
            ViewSousCompo.OptionsView.ColumnAutoWidth = True
            ViewSousCompo.OptionsBehavior.AutoExpandAllGroups = True
            ViewSousCompo.VertScrollVisibility = True
            ViewSousCompo.HorzScrollVisibility = True
            ViewSousCompo.BestFitColumns()

            ViewSousCompo.Columns("Code").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewSousCompo.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

        End If
    End Sub
    Private Sub CmbCompo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCompo.SelectedValueChanged
        If CmbCompo.SelectedIndex = -1 Then
            ChargerSousCompo("-1")
        Else
            Dim CodeComposante As String = Mid(CmbCompo.Text, 1, 1)
            ChargerSousCompo(CodeComposante)
        End If
    End Sub

    Private Sub TxtSousCompo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtSousCompo.KeyDown
        If (e.KeyCode = Keys.Enter) Then

            If CmbCompo.SelectedIndex = -1 Then
                SuccesMsg("Veuillez choisir d'abord une composante.")
                CmbCompo.Select()
                Exit Sub
            End If

            If TxtSousCompo.Text.Trim().Length = 0 Then
                SuccesMsg("Veuillez saisir le libellé de la sous composante.")
                TxtSousCompo.Select()
                Exit Sub
            End If

            Dim CodeComposante As String = Mid(CmbCompo.Text, 1, 1)

            If Modif Then
                drx = ViewSousCompo.GetFocusedDataRow
                query = "Update T_Partition set LibellePartition='" & EnleverApost(TxtSousCompo.Text.Trim()) & "' where CodeProjet='" & ProjetEnCours & "' and CodeClassePartition='2' and LibelleCourt='" & drx("Code").ToString & "'"
                ExecuteNonQuery(query)
                ViewSousCompo.SetFocusedRowCellValue("Libellé", TxtSousCompo.Text.Trim())
                'ChargerSousCompo(CodeComposante)
                TxtSousCompo.ResetText()
                TxtSousCompo.Focus()
            Else
                query = "select Count(*) from T_Partition where CodeClassePartition=2 and LibelleCourt like '" & CodeComposante & "%' and CodeProjet='" & ProjetEnCours & "'"
                Dim NbSousComposante As Decimal = Val(ExecuteScallar(query))
                Dim NewCodeSousComposante As String = CodeComposante & (NbSousComposante + 1)
                Dim CodeMere As String = ExecuteScallar("SELECT CodePartition FROM t_partition WHERE LibelleCourt='" & CodeComposante & "' and CodeProjet='" & ProjetEnCours & "'")
                query = "insert into T_Partition values (NULL,'" & EnleverApost(TxtSousCompo.Text.Trim()) & "','','','','',NULL,'','',NULL,'2','" & ProjetEnCours & "','" & CodeMere.ToString & "','0','','" & NewCodeSousComposante & "','','','','','','','" & CodeUtilisateur & "')"
                ExecuteNonQuery(query)
                ChargerSousCompo(CodeComposante)
                TxtSousCompo.ResetText()
                TxtSousCompo.Focus()
            End If


        End If

    End Sub

    Private Sub SupprimerSousComposanteToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SupprimerSousComposanteToolStripMenuItem.Click
        Try
            If ViewSousCompo.RowCount > 0 Then

                drx = ViewSousCompo.GetDataRow(ViewSousCompo.FocusedRowHandle)
                query = "select count(*) from T_Partition where LibelleCourt like '" & drx("Code").ToString & "%' AND LENGTH(LibelleCourt)=5 AND CodeProjet='" & ProjetEnCours & "'"
                Dim nbre As Decimal = ExecuteScallar(query)

                If (nbre > 0) Then
                    FailMsg("Cette sous composante contient des activités.")
                Else
                    If ConfirmMsg("Voulez-vous vraiment supprimer la sous composante " & drx("Libellé") & " ?") = DialogResult.Yes Then
                        query = "delete from T_Partition where CodeProjet='" & ProjetEnCours & "' and CodeClassePartition='2' and LibelleCourt='" & drx("Code").ToString & "'"
                        ExecuteNonQuery(query)
                        TxtSousCompo.ResetText()
                        ViewSousCompo.DeleteRow(ViewSousCompo.FocusedRowHandle)
                        'CmbCompo_SelectedValueChanged(CmbCompo, New EventArgs)
                    End If
                End If
            End If

        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub GridSousCompo_DoubleClick(sender As System.Object, e As System.EventArgs) Handles GridSousCompo.DoubleClick
        Try
            If ViewSousCompo.RowCount > 0 Then
                drx = ViewSousCompo.GetFocusedDataRow
                Dim IDL = drx(0).ToString
                ColorRowGrid(ViewSousCompo, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
                ColorRowGridAnal(ViewSousCompo, "[Choix]=" & IDL & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)
                Modif = True
                TxtSousCompo.Text = drx("Libellé").ToString
                TxtSousCompo.Select()
                TxtSousCompo.SelectionStart = TxtSousCompo.Text.Length
                'TxtSousCompo.Select(TxtSousCompo.Text.Length,)
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub

    Private Sub RafraichirToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RafraichirToolStripMenuItem.Click
        TxtSousCompo.ResetText()
        CmbCompo_SelectedValueChanged(CmbCompo, New EventArgs)
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If ViewSousCompo.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub GridSousCompo_Click(sender As Object, e As EventArgs) Handles GridSousCompo.Click
        If ViewSousCompo.RowCount > 0 Then
            drx = ViewSousCompo.GetFocusedDataRow
            Dim IDL = drx(0).ToString
            ColorRowGrid(ViewSousCompo, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewSousCompo, "[Choix]=" & IDL & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)
        End If
    End Sub
End Class