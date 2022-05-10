Imports System.Math
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraEditors.Repository

Public Class ListesSignataires
    Dim Drx As DataRow

    Private Sub ResponsableEtape_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        If Checktous.Checked Then Checktous.Checked = False
        ChargerSignataire()
    End Sub

    Public Sub ChargerSignataire(Optional TextRechercher As String = "")
        Dim dtsign = New DataTable()
        dtsign.Columns.Clear()
        dtsign.Columns.Add("CodeX", Type.GetType("System.String"))
        dtsign.Columns.Add("Choix", Type.GetType("System.Boolean"))
        dtsign.Columns.Add("RefSignataire", Type.GetType("System.String"))
        dtsign.Columns.Add("Nom & Prénoms", Type.GetType("System.String"))
        dtsign.Columns.Add("Fonction", Type.GetType("System.String"))
        dtsign.Columns.Add("Contact", Type.GetType("System.String"))
        dtsign.Columns.Add("Adresse", Type.GetType("System.String"))
        dtsign.Columns.Add("E-mail", Type.GetType("System.String"))
        dtsign.Columns.Add("Type signataire", Type.GetType("System.String"))
        dtsign.Rows.Clear()

        If TextRechercher = "" Then
            query = "SELECT * FROM t_signataire WHERE CodeProjet='" & ProjetEnCours & "'"
        Else
            query = "SELECT * FROM t_signataire WHERE CodeProjet='" & ProjetEnCours & "' and ((NomPren LIKE '" & TextRechercher & "%') or (Fonction LIKE '" & TextRechercher & "%') or (TypeSignataire LIKE '" & TextRechercher & "%') or (Email LIKE '" & TextRechercher & "%') or (Contact LIKE '" & TextRechercher & "%'))"
        End If

        Dim dt As DataTable = ExcecuteSelectQuery(query)
        Dim Nbrs As Integer = 0
        For Each rw In dt.Rows
            Nbrs += 1
            Dim ds = dtsign.NewRow()
            ds("Choix") = False
            ds("CodeX") = IIf(Nbrs Mod 2 = 0, "x", "").ToString
            ds("RefSignataire") = rw("RefSignataire")
            ds("Nom & Prénoms") = MettreApost(rw("NomPren").ToString)
            ds("Fonction") = MettreApost(rw("Fonction").ToString)
            ds("Contact") = MettreApost(rw("Contact").ToString)
            ds("Adresse") = MettreApost(rw("Adresse").ToString)
            ds("E-mail") = MettreApost(rw("Email").ToString)
            ds("Type signataire") = MettreApost(rw("TypeSignataire").ToString)

            dtsign.Rows.Add(ds)
        Next
        ListeSignataite.DataSource = dtsign

        Dim edit As RepositoryItemCheckEdit = New RepositoryItemCheckEdit()
        edit.ValueChecked = True
        edit.ValueUnchecked = False
        ViewSignataire.Columns("Choix").ColumnEdit = edit
        ' ViewSignataire.RepositoryItems.Add(edit)
        ViewSignataire.OptionsBehavior.Editable = True

        ViewSignataire.Columns("CodeX").Visible = False
        ViewSignataire.Columns("RefSignataire").Visible = False
        ViewSignataire.OptionsView.ColumnAutoWidth = True
        ViewSignataire.Columns("Choix").Width = 50
        ' ViewSignataire.BestFitColumns()

        ViewSignataire.Columns("CodeX").OptionsColumn.AllowEdit = False
        ViewSignataire.Columns("RefSignataire").OptionsColumn.AllowEdit = False
        ViewSignataire.Columns("Nom & Prénoms").OptionsColumn.AllowEdit = False
        ViewSignataire.Columns("Fonction").OptionsColumn.AllowEdit = False
        ViewSignataire.Columns("Contact").OptionsColumn.AllowEdit = False
        ViewSignataire.Columns("Adresse").OptionsColumn.AllowEdit = False
        ViewSignataire.Columns("E-mail").OptionsColumn.AllowEdit = False

        ViewSignataire.Columns("Contact").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewSignataire.Columns("Type signataire").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewSignataire.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
        ColorRowGrid(ViewSignataire, "[CodeX]=''", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
    End Sub

    'Private Sub ListeSignataite_Click(sender As Object, e As EventArgs) Handles ListeSignataite.Click
    '    If ViewSignataire.RowCount > 0 Then
    '        Try
    '            Drx = ViewSignataire.GetDataRow(ViewSignataire.FocusedRowHandle)
    '            Dim ID = Drx("RefSignataire").ToString
    '            ColorRowGrid(ViewSignataire, "[CodeX]=''", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
    '            ColorRowGridAnal(ViewSignataire, "[RefSignataire]='" & ID & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White)
    '        Catch ex As Exception
    '            FailMsg("Erreur : Information non disponible : " & ex.ToString())
    '        End Try
    '    End If
    'End Sub

    Private Sub BtActualiser_Click(sender As Object, e As EventArgs) Handles BtActualiser.Click
        ChargerSignataire()
        Checktous.Checked = False
    End Sub

    Private Sub BtAjouter_Click(sender As Object, e As EventArgs) Handles BtAjouter.Click
        ReponseDialog = ""
        Dim NewSign As New AjoutSignataire
        Dialog_form(NewSign)
        ReponseDialog = ""
    End Sub

    Private Sub BtModifier_Click(sender As Object, e As EventArgs) Handles BtModifier.Click
        If (ViewSignataire.RowCount > 0) Then
            Dim NbresCocher As Boolean = False
            For i = 0 To ViewSignataire.RowCount - 1
                If CBool(ViewSignataire.GetRowCellValue(i, "Choix")) = True Then
                    NbresCocher = True
                    Exit For
                End If
            Next

            If NbresCocher = False Then
                SuccesMsg("Veuillez coher une ligne du tableau")
                Exit Sub
            End If

            For i = 0 To ViewSignataire.RowCount - 1
                If CBool(ViewSignataire.GetRowCellValue(i, "Choix")) = True Then
                    Dim NewSign As New AjoutSignataire
                    ReponseDialog = ViewSignataire.GetRowCellValue(i, "RefSignataire").ToString
                    NewSign.NomSignatairedp.Text = ViewSignataire.GetRowCellValue(i, "Nom & Prénoms").ToString
                    NewSign.ContactSigndp.Text = ViewSignataire.GetRowCellValue(i, "Contact").ToString
                    NewSign.AdresseSigndp.Text = ViewSignataire.GetRowCellValue(i, "Adresse").ToString
                    NewSign.EmailSigndp.Text = ViewSignataire.GetRowCellValue(i, "E-mail").ToString
                    NewSign.Txtfonctiondp.Text = ViewSignataire.GetRowCellValue(i, "Fonction").ToString
                    NewSign.TypeSignatairedp.Text = ViewSignataire.GetRowCellValue(i, "Type signataire").ToString
                    Dialog_form(NewSign)
                    ReponseDialog = ""
                End If
            Next
            ChargerSignataire()
            Checktous.Checked = False
        Else
            SuccesMsg("Aucun signatiare à modifier")
        End If
    End Sub

    Private Sub BtSupprimer_Click(sender As Object, e As EventArgs) Handles BtSupprimer.Click
        If (ViewSignataire.RowCount > 0) Then
            'Ref des signataires a supprimer
            Dim RefSignatair(ViewSignataire.RowCount - 1) As String
            Dim Nbres As Integer = 0
            For i = 0 To ViewSignataire.RowCount - 1
                If CBool(ViewSignataire.GetRowCellValue(i, "Choix")) = True Then
                    RefSignatair(Nbres) = ViewSignataire.GetRowCellValue(i, "RefSignataire").ToString
                    Nbres += 1
                End If
            Next

            If Nbres = 0 Then
                SuccesMsg("Veuillez coher une ligne du tableau")
                Exit Sub
            End If

            If ConfirmMsg("Voulez-vous vraiment supprimer ?") = DialogResult.Yes Then
                For i = 0 To Nbres - 1
                    ExecuteNonQuery("DELETE FROM t_signataire WHERE RefSignataire='" & RefSignatair(i) & "'")
                Next
                SuccesMsg("Suppresion effectuée avec succès")
                ChargerSignataire()
                If Checktous.Checked Then Checktous.Checked = False
            End If
        End If
    End Sub

    Private Sub BtImprimer_Click(sender As Object, e As EventArgs) Handles BtImprimer.Click
        If (ViewSignataire.RowCount > 0) Then
            SuccesMsg("En cour de réalisation")
        End If
    End Sub

    Private Sub Checktous_CheckedChanged(sender As Object, e As EventArgs) Handles Checktous.CheckedChanged
        Try
            If (ViewSignataire.RowCount > 0 And Checktous.Enabled = True) Then
                For i = 0 To ViewSignataire.RowCount - 1
                    ViewSignataire.SetRowCellValue(i, "Choix", Checktous.Checked)
                Next
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub TxtRechecher_Leave(sender As Object, e As EventArgs) Handles TxtRechecher.Leave
        If TxtRechecher.Text = "" Then
            TxtRechecher.Text = "Rechercher"
        End If
    End Sub

    Private Sub TxtRechecher_EditValueChanged(sender As Object, e As EventArgs) Handles TxtRechecher.EditValueChanged
        Try
            If TxtRechecher.Text <> "Rechercher" Then
                ChargerSignataire(EnleverApost(TxtRechecher.Text))
            End If
        Catch ex As Exception
            FailMsg(ex.ToString())
        End Try
    End Sub

    Private Sub TxtRechecher_Click(sender As Object, e As EventArgs) Handles TxtRechecher.Click
        TxtRechecher.Text = vbNullString
    End Sub
End Class