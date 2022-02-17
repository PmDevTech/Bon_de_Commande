Imports System.Globalization
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Layout
Imports DevExpress.XtraGrid.Views.Card
Imports MySql.Data.MySqlClient
Imports System.IO
Imports Microsoft.Office.Interop
Imports DevExpress.XtraSplashScreen
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions
Imports System.Math
Imports DevExpress.XtraRichEdit

Public Class Afficher_doc

    Dim dtdoc = New DataTable()
    Dim drx As DataRow


    Private Sub GridArchives_DoubleClick(sender As Object, e As System.EventArgs) Handles GridArchives.DoubleClick
        If LayoutView1.RowCount > 0 Then
            drx = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
            Dim PathFile As String = line & "\documentsProjet\" & drx(0).ToString & "\" & drx(1).ToString
            If File.Exists(PathFile) Then
                Process.Start(PathFile)
            Else
                If ConfirmMsg("Le fichier spécifique n'existe pas." & vbNewLine & "Voulez-vous supprimer ce fichier?") = DialogResult.Yes Then
                    If File.Exists(PathFile) Then
                        Try
                            File.Delete(PathFile)
                        Catch ex As Exception
                        End Try
                    End If
                    query = "DELETE FROM t_manuel WHERE id='" & drx("id") & "'"
                    ExecuteNonQuery(query)
                    Afficher_doc_Load(Me, New EventArgs)
                End If
            End If
        End If
    End Sub

    Private Sub Afficher_doc_Load(sender As System.Object, e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        Try

            dtdoc.Columns.Clear()
            dtdoc.Columns.Add("Type de documents", Type.GetType("System.String"))
            dtdoc.Columns.Add("Nom", Type.GetType("System.String"))
            dtdoc.Columns.Add("Id", Type.GetType("System.String"))
            dtdoc.Rows.Clear()

            query = "select m.nom_manuel, t.libelle_tm, m.id from t_manuel m, t_typedoc t where m.id_tm=t.id_tm and  t.libelle_tm='" & ClearMdi.BarListItem3.Strings(ClearMdi.BarListItem3.DataIndex) & "' order by m.nom_manuel"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                Dim drs = dtdoc.NewRow()
                drs(0) = MettreApost(rw(1).ToString)
                drs(1) = MettreApost(rw(0).ToString)
                drs(2) = MettreApost(rw("id").ToString)
                dtdoc.Rows.Add(drs)
            Next

            GridArchives.DataSource = dtdoc
            LayoutViewCard1.Items(2).Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SuppressionDocToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SuppressionDocToolStripMenuItem.Click
        If LayoutView1.RowCount > 0 Then
            drx = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
            If ConfirmMsg("Voulez-vous supprimer " & MettreApost(drx(0)) & " ?") = DialogResult.Yes Then
                Dim PathFile As String = line & "\documentsProjet\" & drx(0).ToString & "\" & drx(1).ToString
                If File.Exists(PathFile) Then
                    Try
                        File.Delete(PathFile)
                    Catch ex As Exception
                    End Try
                End If
                query = "DELETE FROM t_manuel WHERE id='" & drx(2) & "'"
                ExecuteNonQuery(query)
                Afficher_doc_Load(Me, New EventArgs)
            End If

        End If
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If LayoutView1.RowCount <= 0 Then
            e.Cancel = True
        End If
    End Sub
End Class