Public Class AjoutMontantSousLot
    Private Sub AjoutMontantSousLot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbSousLot.Text = ""
        txtLibelleSousLot.Text = ""
        cmbSousLot.Properties.Items.Clear()
        Dim Resultat As Object() = PassationMarche.GetSousLot(OuvertureOffres.CmbNumLot.Text, OuvertureOffres.CmbNumDAO.Text)
        Dim dt As DataTable = CType(Resultat(1), DataTable)
        For Each rw As DataRow In dt.Rows
            cmbSousLot.Properties.Items.Add(rw("CodeSousLot").ToString)
        Next

        If OuvertureOffres.ViewMontantDesSL.RowCount > 0 Then
            If OuvertureOffres.BtModOffre.Enabled = True Then
                GridMontantSLCreat()
                Dim dts As DataTable = GridMontantSL.DataSource
                For i = 0 To OuvertureOffres.ViewMontantDesSL.RowCount - 1
                    Dim drS As DataRow = dts.NewRow
                    drS("Code Sous lot") = OuvertureOffres.ViewMontantDesSL.GetRowCellValue(i, "Code Sous lot").ToString
                    drS("Montant soumission") = OuvertureOffres.ViewMontantDesSL.GetRowCellValue(i, "Montant soumission").ToString
                    If OuvertureOffres.BtEnrgOffre.Enabled = True Then
                        drS("RefSoumis") = ""
                    Else
                        drS("RefSoumis") = OuvertureOffres.ViewMontantDesSL.GetRowCellValue(i, "RefSoumis").ToString
                    End If
                    dts.Rows.Add(drS)
                Next
            End If
        Else
            GridMontantSLCreat()
        End If
    End Sub

    Private Sub GridMontantSLCreat()
        Dim dt2 As New DataTable
        dt2.Columns.Clear()
        dt2.Columns.Add("Code sous lot", Type.GetType("System.String"))
        dt2.Columns.Add("Montant soumission", Type.GetType("System.String"))
        dt2.Columns.Add("RefSoumis", Type.GetType("System.String"))
        dt2.Rows.Clear()
        GridMontantSL.DataSource = dt2
        ViewMontantSL.Columns("RefSoumis").Visible = False
        ViewMontantSL.OptionsView.ColumnAutoWidth = True
        'GridView2.OptionsBehavior.AutoExpandAllGroups = True
        ViewMontantSL.VertScrollVisibility = True
        ViewMontantSL.HorzScrollVisibility = True
        ViewMontantSL.BestFitColumns()
        ViewMontantSL.Columns("Montant soumission").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewMontantSL.Appearance.Row.Font = New Font("Times New Roman", 9, FontStyle.Regular)
    End Sub

    Private Sub BtAjoutMontant_Click(sender As Object, e As EventArgs) Handles BtAjoutMontant.Click
        Dim erreur As String = ""
        'si le sous lot n'est pas renseigné
        If cmbSousLot.SelectedIndex = -1 Then
            erreur += "- Sous lot" + ControlChars.CrLf
        End If
        'si la montant n'est pas renseigné
        If txtMontantSousLot.Text = "" Then
            erreur += "- Montant" + ControlChars.CrLf
        End If
        'End If
        If erreur = "" Then
            For i = 0 To ViewMontantSL.RowCount - 1
                If cmbSousLot.Text = ViewMontantSL.GetRowCellValue(i, "Code sous lot") Then
                    SuccesMsg("Ce sous lot est déjà enregistré.")
                    Exit Sub
                End If
            Next

            Dim dt As DataTable = GridMontantSL.DataSource
            Dim drS As DataRow = dt.NewRow
            drS("Code Sous lot") = cmbSousLot.Text
            drS("Montant soumission") = txtMontantSousLot.Text
            If OuvertureOffres.BtEnrgOffre.Enabled = True Then
                drS("RefSoumis") = ""
            Else
                drS("RefSoumis") = OuvertureOffres.txtRefSoumisSup.Text
            End If
            dt.Rows.Add(drS)

            Dim dt1 As DataTable = OuvertureOffres.GridMontantDesSL.DataSource
            Dim drC As DataRow = dt1.NewRow
            drC("Code Sous lot") = cmbSousLot.Text
            drC("Montant soumission") = txtMontantSousLot.Text
            If OuvertureOffres.BtEnrgOffre.Enabled = True Then
                drC("RefSoumis") = ""
            Else
                drC("RefSoumis") = OuvertureOffres.txtRefSoumisSup.Text
            End If
            dt1.Rows.Add(drC)
            OuvertureOffres.TxtMontantOffre.Text = UpdateMontantLot()
            'OuvertureOffres.UpdateMontantLot()
        Else
            SuccesMsg("Veuillez renseignés le champ : " + ControlChars.CrLf + erreur)
        End If
    End Sub

    Private Sub cmbSousLot_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSousLot.SelectedIndexChanged
        txtMontantSousLot.Text = ""
        If cmbSousLot.SelectedIndex <> -1 Then
            query = "select LibelleSousLot,CodeSousLot from t_lotdao_souslot where NumeroDAO='" & OuvertureOffres.CmbNumDAO.Text & "' and RefLot='" & OuvertureOffres.TxtRefLot.Text & "' And CodeSousLot='" & cmbSousLot.Text & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            txtLibelleSousLot.Text = ""
            For Each rw As DataRow In dt1.Rows
                txtLibelleSousLot.Text = MettreApost(rw("LibelleSousLot").ToString)
            Next
        End If
    End Sub

    Public Function UpdateMontantLot()
        Dim montant As Double = 0
        For i = 0 To OuvertureOffres.ViewMontantDesSL.RowCount - 1
            montant += CDbl(OuvertureOffres.ViewMontantDesSL.GetRowCellValue(i, "Montant soumission").ToString)
        Next
        Return montant
    End Function

    Private Sub SupprimerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupprimerToolStripMenuItem.Click
        If ViewMontantSL.RowCount > 0 Then
            OuvertureOffres.txtRefSoumisSup.Text = ViewMontantSL.GetRowCellValue(ViewMontantSL.FocusedRowHandle, "RefSoumis").ToString
            If OuvertureOffres.ViewMontantDesSL.RowCount > 0 Then
                For i = 0 To ViewMontantSL.RowCount - 1
                    If OuvertureOffres.ViewMontantDesSL.GetRowCellValue(i, "Code Sous lot").ToString = ViewMontantSL.GetRowCellValue(ViewMontantSL.FocusedRowHandle, "Code sous lot").ToString Then
                        OuvertureOffres.ViewMontantDesSL.DeleteRow(i)
                        Exit For
                    End If
                Next
            End If
            ViewMontantSL.DeleteRow(ViewMontantSL.FocusedRowHandle)
            OuvertureOffres.TxtMontantOffre.Text = UpdateMontantLot()
        End If

    End Sub
End Class