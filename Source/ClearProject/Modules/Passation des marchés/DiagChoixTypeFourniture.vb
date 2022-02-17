Imports MySql.Data.MySqlClient
Imports System.Math
Imports Microsoft.Office.Interop
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class DiagChoixTypeFourniture
    Public drRetour As DataRow = Nothing
    Private Sub DiagChoixTypeFourniture_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        LoadEntete()
        LoadComboCategorie()
    End Sub
    Private Sub LoadEntete()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Clear()
        dt.Columns.Add("IdItem", Type.GetType("System.String"))
        dt.Columns.Add("Libellé", Type.GetType("System.String"))
        dt.Columns.Add("Catégorie", Type.GetType("System.String"))
        dt.Columns.Add("Sous catégorie", Type.GetType("System.String"))
        dt.Columns.Add("Type", Type.GetType("System.String"))
        dt.Columns.Add("Edit", Type.GetType("System.Boolean"))
        dt.DefaultView.Sort = "Catégorie ASC, Sous catégorie ASC"
        GridCategorie.DataSource = dt
        GridViewCategorie.Columns("IdItem").Visible = False
        GridViewCategorie.Columns("Catégorie").Visible = False
        GridViewCategorie.Columns("Sous catégorie").Visible = False
        GridViewCategorie.Columns("Type").Visible = False
        GridViewCategorie.Columns("Edit").Visible = False
        GridViewCategorie.OptionsView.ColumnAutoWidth = True
    End Sub
    Private Sub ChargerGrid(Optional TextRecherche As String = "")
        Dim dts As DataTable = GridCategorie.DataSource
        dts.Rows.Clear()
        If TextRecherche = "" Then
            query = "select * from T_PredFournitures_Groupe ORDER BY LibelleCat ASC"
        Else
            query = "select * from T_PredFournitures_Groupe WHERE LibelleCat LIKE '%" & TextRecherche & "%' OR IdCat IN(SELECT IdCat FROM t_predfournitures_sous_groupe WHERE LibelleSousCat LIKE '%" & TextRecherche & "%') ORDER BY LibelleCat ASC"
        End If
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Dim drS1 = dts.NewRow()
            drS1(0) = rw("IdCat").ToString
            drS1(1) = MettreApost(rw("LibelleCat"))
            drS1(2) = MettreApost(rw("LibelleCat"))
            drS1(3) = ""
            drS1(4) = "Cat"
            drS1(5) = False
            dts.Rows.Add(drS1)

            If TextRecherche = "" Then
                query = "select * from t_predfournitures_sous_groupe WHERE IdCat='" & rw("IdCat") & "' ORDER BY LibelleSousCat ASC"
            Else
                query = "select * from t_predfournitures_sous_groupe WHERE IdCat='" & rw("IdCat") & "' AND LibelleSousCat LIKE '%" & TextRecherche & "%' ORDER BY LibelleSousCat ASC"
            End If

            Dim dtSousCat As DataTable = ExcecuteSelectQuery(query)
            For Each rwSousCat As DataRow In dtSousCat.Rows
                drS1 = dts.NewRow()
                drS1(0) = rwSousCat("IdSousCat").ToString
                drS1(1) = "     - " & MettreApost(rwSousCat("LibelleSousCat"))
                drS1(2) = MettreApost(rw("LibelleCat"))
                drS1(3) = MettreApost(rwSousCat("LibelleSousCat"))
                drS1(4) = "SousCat"
                drS1(5) = False
                dts.Rows.Add(drS1)
            Next
        Next
        ColorRowGrid(GridViewCategorie, "Type='Cat'", Color.LightBlue, "Tahoma", 8, FontStyle.Bold, Color.Black)

    End Sub
    Private Sub LoadComboCategorie()
        query = "select * from T_PredFournitures_Groupe ORDER BY LibelleCat ASC"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        cmbCategorie.Properties.Items.Clear()
        For Each rw As DataRow In dt.Rows
            cmbCategorie.Properties.Items.Add(MettreApost(rw("LibelleCat")))
        Next
    End Sub
    Private Sub GridCategorie_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridCategorie.DoubleClick
        BtSelectItem.PerformClick()
    End Sub

    Private Sub TxtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtSearch.TextChanged
        ChargerGrid(TxtSearch.Text)
    End Sub

    Private Sub DiagChoixTypeFourniture_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        DebutChargement()
        ChargerGrid()
        FinChargement()
    End Sub

    Private Sub cmbCategorie_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbCategorie.KeyDown
        If e.KeyCode = Keys.Enter Then
            If cmbCategorie.Text.Trim() = String.Empty Then
                SuccesMsg("Veuillez saisir une catégorie")
                cmbCategorie.Select()
                Exit Sub
            End If

            Dim EditRowIndex As Integer = IsEditMod()
            If EditRowIndex <> -1 Then 'Modification
                Dim drx As DataRow = GridViewCategorie.GetDataRow(EditRowIndex)
                query = "UPDATE t_predfournitures_groupe SET LibelleCat='" & EnleverApost(cmbCategorie.Text.Trim()) & "' WHERE IdCat='" & drx("IdItem") & "'"
                ExecuteNonQuery(query)
                GridViewCategorie.SetRowCellValue(EditRowIndex, "Libellé", cmbCategorie.Text.Trim())
                GridViewCategorie.SetRowCellValue(EditRowIndex, "Edit", False)
                LoadComboCategorie()
                EnableEditControl(cmbCategorie)
                EnableEditControl(txtSousCategorie)
                cmbCategorie.Focus()
                Exit Sub
            End If

            query = "SELECT COUNT(*) FROM t_predfournitures_groupe WHERE LibelleCat='" & EnleverApost(cmbCategorie.Text.Trim()) & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                SuccesMsg("La catégorie « " & cmbCategorie.Text & " » est déjà enregistré.")
                Exit Sub
            Else
                query = "INSERT INTO t_predfournitures_groupe VALUES(NULL,'" & EnleverApost(cmbCategorie.Text.Trim()) & "')"
                ExecuteNonQuery(query)
                query = "SELECT MAX(IdCat) FROM t_predfournitures_groupe"
                LoadComboCategorie()
                Dim LastID As Integer = Val(ExecuteScallar(query))
                Dim dts As DataTable = GridCategorie.DataSource
                Dim drS1 = dts.NewRow()
                drS1(0) = LastID
                drS1(1) = cmbCategorie.Text.Trim()
                drS1(2) = cmbCategorie.Text.Trim()
                drS1(3) = ""
                drS1(4) = "Cat"
                drS1(5) = False
                dts.Rows.Add(drS1)
                ColorRowGrid(GridViewCategorie, "Type='Cat'", Color.LightBlue, "Tahoma", 8, FontStyle.Bold, Color.Black)
            End If
        End If
    End Sub
    Private Sub txtSousCategorie_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSousCategorie.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim EditRowIndex As Integer = IsEditMod()
            If EditRowIndex = -1 Then
                If cmbCategorie.Text.Trim() = String.Empty Then
                    SuccesMsg("Veuillez saisir une catégorie")
                    cmbCategorie.Select()
                    Exit Sub
                End If
            End If

            If txtSousCategorie.IsRequiredControl("Veuillez saisir une sous catégorie") Then
                Exit Sub
            End If

            If EditRowIndex <> -1 Then 'Modification
                Dim drx As DataRow = GridViewCategorie.GetDataRow(EditRowIndex)
                query = "UPDATE t_predfournitures_sous_groupe SET LibelleSousCat='" & EnleverApost(txtSousCategorie.Text.Trim()) & "' WHERE IdSousCat='" & drx("IdItem") & "'"
                ExecuteNonQuery(query)
                GridViewCategorie.SetRowCellValue(EditRowIndex, "Libellé", "     - " & txtSousCategorie.Text.Trim())
                GridViewCategorie.SetRowCellValue(EditRowIndex, "Edit", False)
                EnableEditControl(cmbCategorie)
                EnableEditControl(txtSousCategorie)
                cmbCategorie.Focus()
                Exit Sub
            End If

            'Ajout
            Dim IDCat As Integer = 0
            query = "SELECT IdCat FROM t_predfournitures_groupe WHERE LibelleCat='" & EnleverApost(cmbCategorie.Text.Trim()) & "'"
            IDCat = Val(ExecuteScallar(query))
            If IDCat = 0 Then
                query = "INSERT INTO t_predfournitures_groupe VALUES(NULL,'" & EnleverApost(cmbCategorie.Text.Trim()) & "')"
                ExecuteNonQuery(query)
                query = "SELECT MAX(IdCat) FROM t_predfournitures_groupe"
                IDCat = Val(ExecuteScallar(query))
                LoadComboCategorie()
                Dim dts As DataTable = GridCategorie.DataSource
                Dim drS1 = dts.NewRow()
                drS1(0) = IDCat
                drS1(1) = cmbCategorie.Text.Trim()
                drS1(2) = cmbCategorie.Text.Trim()
                drS1(3) = ""
                drS1(4) = "Cat"
                drS1(5) = False
                dts.Rows.Add(drS1)
                ColorRowGrid(GridViewCategorie, "Type='Cat'", Color.LightBlue, "Tahoma", 8, FontStyle.Bold, Color.Black)
            End If

            query = "SELECT COUNT(*) FROM t_predfournitures_sous_groupe WHERE IdCat='" & IDCat & "' AND LibelleSousCat='" & EnleverApost(txtSousCategorie.Text.Trim()) & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                SuccesMsg("La sous catégorie « " & txtSousCategorie.Text & " » est déjà enregistré sur la catégorie « " & cmbCategorie.Text & " ».")
                Exit Sub
            Else
                query = "INSERT INTO t_predfournitures_sous_groupe VALUES(NULL,'" & EnleverApost(txtSousCategorie.Text.Trim()) & "','" & IDCat & "')"
                ExecuteNonQuery(query)
                query = "SELECT MAX(IdCat) FROM t_predfournitures_sous_groupe"
                Dim LastID As Integer = Val(ExecuteScallar(query))
                Dim dts As DataTable = GridCategorie.DataSource
                Dim drS1 = dts.NewRow()
                drS1(0) = LastID
                drS1(1) = "     - " & txtSousCategorie.Text.Trim()
                drS1(2) = cmbCategorie.Text.Trim()
                drS1(3) = txtSousCategorie.Text.Trim()
                drS1(4) = "SousCat"
                drS1(5) = False
                dts.Rows.Add(drS1)
                ColorRowGrid(GridViewCategorie, "Type='Cat'", Color.LightBlue, "Tahoma", 8, FontStyle.Bold, Color.Black)
            End If
        End If
    End Sub
    Private Sub CancelEdit()
        For i = 0 To GridViewCategorie.RowCount - 1
            If CBool(GridViewCategorie.GetRowCellValue(i, "Edit")) Then
                GridViewCategorie.SetRowCellValue(i, "Edit", False)
            End If
        Next
    End Sub
    Private Function IsEditMod() As Integer
        Dim index As Integer = -1
        For i = 0 To GridViewCategorie.RowCount - 1
            If CBool(GridViewCategorie.GetRowCellValue(i, "Edit")) Then
                Return i
            End If
        Next
        Return index
    End Function
    Private Sub SetControlEdit(ByRef TextBox As DevExpress.XtraEditors.TextEdit, ByVal Libelle As String)
        TextBox.Enabled = True
        TextBox.Text = Libelle
        TextBox.Focus()
    End Sub
    Private Sub DisableEditControl(ByRef TextBox As DevExpress.XtraEditors.TextEdit)
        TextBox.Enabled = False
        TextBox.ResetText()
    End Sub
    Private Sub EnableEditControl(ByRef TextBox As DevExpress.XtraEditors.TextEdit)
        TextBox.Enabled = True
        TextBox.ResetText()
    End Sub
    Private Sub EnableEditControl(ByRef ComboBox As DevExpress.XtraEditors.ComboBoxEdit)
        ComboBox.Enabled = True
        ComboBox.ResetText()
    End Sub
    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If GridViewCategorie.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub
    Private Sub btModifierItem_Click(sender As Object, e As EventArgs) Handles btModifierItem.Click
        Dim drx As DataRow
        If IsEditMod() <> -1 Then
            drx = GridViewCategorie.GetDataRow(IsEditMod())
            If drx("Type") = "Cat" Then
                SuccesMsg("La catégorie « " & drx("Libellé") & " » est déjà en mode modification." & vbNewLine & "Terminer la modification indiquée ou cliquer sur le bouton Annuler.")
            Else
                SuccesMsg("La sous catégorie « " & drx("Libellé").ToString().Replace("     - ", "") & " » est déjà en mode modification." & vbNewLine & "Terminer la modification indiquée ou cliquer sur le bouton Annuler.")
            End If
            Exit Sub
        End If

        drx = GridViewCategorie.GetDataRow(GridViewCategorie.FocusedRowHandle)
        If drx("Type") = "Cat" Then
            SetControlEdit(cmbCategorie, drx("Libellé").ToString())
            DisableEditControl(txtSousCategorie)
            GridViewCategorie.SetRowCellValue(GridViewCategorie.FocusedRowHandle, "Edit", True)
        ElseIf drx("Type") = "SousCat" Then
            SetControlEdit(txtSousCategorie, drx("Libellé").ToString().Replace("     - ", ""))
            DisableEditControl(cmbCategorie)
            GridViewCategorie.SetRowCellValue(GridViewCategorie.FocusedRowHandle, "Edit", True)
        Else
            SuccesMsg("Impossible de modifier cette ligne.")
        End If
    End Sub
    Private Sub btSupprimerItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSupprimerItem.Click
        If (GridViewCategorie.RowCount > 0) Then
            Dim drx As DataRow = GridViewCategorie.GetDataRow(GridViewCategorie.FocusedRowHandle)
            If drx("Type") = "Cat" Then
                If ConfirmMsg("Voulez-vous supprimer cette catégorie et ces éléments?") = DialogResult.Yes Then
                    For i = GridViewCategorie.RowCount - 1 To GridViewCategorie.FocusedRowHandle Step -1
                        Dim dr As DataRow = GridViewCategorie.GetDataRow(i)
                        If dr("Type") = "SousCat" And dr("Catégorie") = drx("Libellé") Then
                            query = "DELETE FROM t_predfournitures_sous_groupe WHERE IdSousCat='" & GridViewCategorie.GetDataRow(i)("IdItem") & "'"
                            ExecuteNonQuery(query)
                            dr.Delete()
                        End If
                    Next
                    query = "DELETE FROM t_predfournitures_groupe WHERE IdCat='" & drx("IdItem") & "'"
                    ExecuteNonQuery(query)
                    drx.Delete()
                    LoadComboCategorie()
                End If
            ElseIf drx("Type") = "SousCat" Then
                If ConfirmMsg("Voulez-vous supprimer cette sous catégorie?") = DialogResult.Yes Then
                    query = "DELETE FROM t_predfournitures_sous_groupe WHERE IdSousCat='" & drx("IdItem") & "'"
                    ExecuteNonQuery(query)
                    drx.Delete()
                End If
            Else
                SuccesMsg("Impossible de supprimer cette ligne.")
            End If
        End If
    End Sub
    Private Sub BtSelectItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSelectItem.Click
        If GridViewCategorie.RowCount > 0 Then
            If GridViewCategorie.FocusedRowHandle > -1 Then
                Dim drx As DataRow = GridViewCategorie.GetDataRow(GridViewCategorie.FocusedRowHandle)
                drRetour = drx
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        End If
    End Sub
    Private Sub btAnnuler_Click(sender As Object, e As EventArgs) Handles btAnnuler.Click
        EnableEditControl(cmbCategorie)
        EnableEditControl(txtSousCategorie)
        cmbCategorie.Focus()
        CancelEdit()
    End Sub
    Private Sub btClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btClose.Click
        Me.DialogResult = DialogResult.Abort
        Me.Close()
    End Sub

    Private Sub btSelectionnerItem_Click(sender As Object, e As EventArgs) Handles btSelectionnerItem.Click
        BtSelectItem.PerformClick()
    End Sub
End Class