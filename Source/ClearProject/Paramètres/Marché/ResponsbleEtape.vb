Public Class ResponsableEtape

    Private LesResponsable As New ListeResponsable

    Private Sub ResponsableEtape_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        On Error Resume Next
        If TxtRechecher.Text <> "Rechercher" Then
            TxtRechecher.Text = "Rechercher"
        End If
        CmbPageSize.SelectedIndex = 0
        CmbPageSize.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor

        loadRespoEtape()
        If Checktous.Checked Then Checktous.Checked = False
        ViewRespoEtape.Columns(0).Visible = True
        ViewRespoEtape.Columns(0).Width = 20
        ViewRespoEtape.OptionsView.ColumnAutoWidth = True
        ViewRespoEtape.OptionsBehavior.AutoExpandAllGroups = True
        ViewRespoEtape.VertScrollVisibility = True
        ViewRespoEtape.HorzScrollVisibility = True
        ViewRespoEtape.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
        ViewRespoEtape.Columns("Code").OptionsColumn.AllowEdit = False
        ViewRespoEtape.Columns("Nom & prénoms").OptionsColumn.AllowEdit = False
        ViewRespoEtape.Columns("Structure").OptionsColumn.AllowEdit = False
        ViewRespoEtape.Columns("Email").OptionsColumn.AllowEdit = False
        ViewRespoEtape.Columns("Fonction").OptionsColumn.AllowEdit = False
        ViewRespoEtape.Columns("Portable").OptionsColumn.AllowEdit = False
        ViewRespoEtape.Columns("Code").Visible = False
        ViewRespoEtape.OptionsCustomization.AllowSort = False
        ViewRespoEtape.OptionsNavigation.AutoMoveRowFocus = False
        ViewRespoEtape.OptionsCustomization.AllowColumnMoving = False
        ViewRespoEtape.OptionsCustomization.AllowColumnResizing = False

    End Sub
    Private Sub AjoutPlanDeTiersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AjoutPlanDeTiersToolStripMenuItem.Click
        Dim NewRespo As New AjoutRespoEtape
        'NewRespo.Size = New Point(710, 450)
        Dialog_form(NewRespo)
    End Sub
    Private Sub loadRespoEtape(Optional Page As Decimal = 1)

        If Page = 1 Then
            query = "select count(*) from t_ppm_responsableetape where CodeProjet='" & ProjetEnCours & "'"
            Dim nbre = ExecuteScallar(query)

            With LesResponsable
                .PageSize = IIf(CmbPageSize.Text = "", 1, CmbPageSize.Text)
                .PageCount = nbre \ .PageSize
                If nbre Mod .PageSize <> 0 Then
                    .PageCount += 1
                End If
                TxtPage.Text = "Page 1" & "/" & LesResponsable.PageCount
                .Resqlconno = 0
            End With
        End If

        With LesResponsable
            .CurrentPage = Page
            .LoadPage(LgListRespoEtape, .CurrentPage)
        End With
    End Sub
    Private Sub SuppressionCompteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SuppressionCompteToolStripMenuItem.Click
        Try
            If (ViewRespoEtape.RowCount > 0) Then
                For i = 0 To ViewRespoEtape.RowCount - 1
                    If CBool(ViewRespoEtape.GetRowCellValue(i, "Choix")) = True Then
                        query = "Select * FROM t_planmarche WHERE ResponsableEtape='" & ViewRespoEtape.GetRowCellValue(i, "Code").ToString & "' AND StatutRespoEtape='Exterieur'"
                        Dim dt = ExcecuteSelectQuery(query)
                        If dt.Rows.Count > 0 Then
                            SuccesMsg("Ce responsable ne peut pas être supprimé car il est déjà utilisé dans une étape.")
                            Exit Sub
                        End If
                    End If
                Next

                Dim cpte As Decimal = 0
                'Vérificationn des selections
                For i = 0 To ViewRespoEtape.RowCount - 1
                    If CBool(ViewRespoEtape.GetRowCellValue(i, "Choix")) = True Then
                        cpte += 1
                    End If
                Next
                If cpte = 0 Then
                    SuccesMsg("Veuillez cocher au moins un responsable")
                    Exit Sub
                End If

                If ConfirmMsg("Voulez-vous vraiment supprimer?") = DialogResult.Yes Then
                    'Suppression des données 
                    For i = 0 To ViewRespoEtape.RowCount - 1

                        If CBool(ViewRespoEtape.GetRowCellValue(i, "Choix")) = True Then
                            cpte += 1
                            Dim CodeRespo As String = ViewRespoEtape.GetRowCellValue(i, "Code").ToString()
                            ExecuteNonQuery("DELETE FROM t_ppm_responsableetape WHERE ID='" & CodeRespo & "'")
                        End If

                    Next
                    SuccesMsg("Suppression effectuée avec succès")
                    CmbPageSize_SelectedIndexChanged(sender, e)
                End If
            End If

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub ModificationPlanDeTiersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ModificationPlanDeTiersToolStripMenuItem.Click
        Try
            If (ViewRespoEtape.RowCount > 0) Then
                Dim cpte As Decimal = 0
                For i = 0 To ViewRespoEtape.RowCount - 1
                    If CBool(ViewRespoEtape.GetRowCellValue(i, "Choix")) = True Then
                        cpte += 1
                    End If
                Next
                Dim rep As New DialogResult
                Dim alert As Boolean = False
                If cpte > 3 Then
                    alert = True
                    rep = ConfirmCancelMsg("Vous avez sélectionné plus de 3 responsables." & vbNewLine & "Voulez-vous que le système modifie les 3 premiers responsables sélectionnés?")
                End If

                If alert Then
                    If rep = DialogResult.No Then
                        For i = 0 To ViewRespoEtape.RowCount - 1
                            If CBool(ViewRespoEtape.GetRowCellValue(i, "Choix")) = True Then
                                AjoutRespoEtape.Modif = True

                                query = "select * from t_ppm_responsableetape where ID='" & ViewRespoEtape.GetRowCellValue(i, "Code").ToString & "' AND CodeProjet='" & ProjetEnCours & "'"
                                Dim dt = ExcecuteSelectQuery(query)
                                For Each rwx As DataRow In dt.Rows
                                    AjoutRespoEtape.IDCahe.Text = ViewRespoEtape.GetRowCellValue(i, "Code").ToString
                                    AjoutRespoEtape.txtNom.Text = MettreApost(rwx("Nom").ToString)
                                    AjoutRespoEtape.txtPrenom.Text = MettreApost(rwx("Prenoms").ToString)
                                    AjoutRespoEtape.cmbStructure.Text = MettreApost(rwx("Service").ToString)
                                    AjoutRespoEtape.cmbFonction.Text = MettreApost(rwx("Fonction").ToString)
                                    AjoutRespoEtape.txtTelephone.Text = MettreApost(rwx("Telephone").ToString)
                                    AjoutRespoEtape.txtPortable.Text = MettreApost(rwx("Portable").ToString)
                                    AjoutRespoEtape.txtFax.Text = MettreApost(rwx("Fax").ToString)
                                    AjoutRespoEtape.txtMail.Text = MettreApost(rwx("Email").ToString)

                                Next

                                Dialog_form(AjoutRespoEtape)
                            End If
                        Next
                    ElseIf rep = DialogResult.Yes Then
                        cpte = 0
                        For i = 0 To ViewRespoEtape.RowCount - 1
                            If cpte >= 3 Then
                                Exit For
                            End If
                            If CBool(ViewRespoEtape.GetRowCellValue(i, "Choix")) = True Then
                                AjoutRespoEtape.Modif = True

                                query = "select * from t_ppm_responsableetape where ID='" & ViewRespoEtape.GetRowCellValue(i, "Code").ToString & "' AND CodeProjet='" & ProjetEnCours & "'"
                                Dim dt = ExcecuteSelectQuery(query)
                                For Each rwx As DataRow In dt.Rows
                                    AjoutRespoEtape.IDCahe.Text = ViewRespoEtape.GetRowCellValue(i, "Code").ToString
                                    AjoutRespoEtape.txtNom.Text = MettreApost(rwx("Nom").ToString)
                                    AjoutRespoEtape.txtPrenom.Text = MettreApost(rwx("Prenoms").ToString)
                                    AjoutRespoEtape.cmbStructure.Text = MettreApost(rwx("Service").ToString)
                                    AjoutRespoEtape.cmbFonction.Text = MettreApost(rwx("Fonction").ToString)
                                    AjoutRespoEtape.txtTelephone.Text = MettreApost(rwx("Telephone").ToString)
                                    AjoutRespoEtape.txtPortable.Text = MettreApost(rwx("Portable").ToString)
                                    AjoutRespoEtape.txtFax.Text = MettreApost(rwx("Fax").ToString)
                                    AjoutRespoEtape.txtMail.Text = MettreApost(rwx("Email").ToString)
                                Next

                                Dialog_form(AjoutRespoEtape)
                                cpte += 1
                            End If
                        Next
                    End If
                    BtActualiser.PerformClick()
                Else
                    For i = 0 To ViewRespoEtape.RowCount - 1
                        If CBool(ViewRespoEtape.GetRowCellValue(i, "Choix")) = True Then
                            AjoutRespoEtape.Modif = True

                            query = "select * from t_ppm_responsableetape where ID='" & ViewRespoEtape.GetRowCellValue(i, "Code").ToString & "' AND CodeProjet='" & ProjetEnCours & "'"
                            Dim dt = ExcecuteSelectQuery(query)
                            For Each rwx As DataRow In dt.Rows
                                AjoutRespoEtape.IDCahe.Text = ViewRespoEtape.GetRowCellValue(i, "Code").ToString
                                AjoutRespoEtape.txtNom.Text = MettreApost(rwx("Nom").ToString)
                                AjoutRespoEtape.txtPrenom.Text = MettreApost(rwx("Prenoms").ToString)
                                AjoutRespoEtape.cmbStructure.Text = MettreApost(rwx("Service").ToString)
                                AjoutRespoEtape.cmbFonction.Text = MettreApost(rwx("Fonction").ToString)
                                AjoutRespoEtape.txtTelephone.Text = MettreApost(rwx("Telephone").ToString)
                                AjoutRespoEtape.txtPortable.Text = MettreApost(rwx("Portable").ToString)
                                AjoutRespoEtape.txtFax.Text = MettreApost(rwx("Fax").ToString)
                                AjoutRespoEtape.txtMail.Text = MettreApost(rwx("Email").ToString)
                            Next

                            Dialog_form(AjoutRespoEtape)
                        End If
                    Next
                    BtActualiser.PerformClick()

                End If

            End If

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub ImprimerCompteDeTiersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImprimerCompteDeTiersToolStripMenuItem.Click

    End Sub

    Private Sub BtNext_Click(sender As System.Object, e As System.EventArgs) Handles BtNext.Click
        If TxtRechecher.Text <> "" And TxtRechecher.Text <> "Rechercher" Then
            If (LesResponsable.CurrentPage < LesResponsable.PageCount) Then
                LesResponsable.CurrentPage = LesResponsable.CurrentPage + 1
                TxtPage.Text = "Page " & LesResponsable.CurrentPage & "/" & LesResponsable.PageCount
                LesResponsable.RechPage(LgListRespoEtape, LesResponsable.CurrentPage)
                If Checktous.Checked Then Checktous.Checked = False
            End If
        Else
            If (LesResponsable.CurrentPage < LesResponsable.PageCount) Then
                LesResponsable.CurrentPage = LesResponsable.CurrentPage + 1
                TxtPage.Text = "Page " & LesResponsable.CurrentPage & "/" & LesResponsable.PageCount
                LesResponsable.LoadPage(LgListRespoEtape, LesResponsable.CurrentPage)
                If Checktous.Checked Then Checktous.Checked = False
            End If
        End If
    End Sub

    Private Sub BtLast_Click(sender As System.Object, e As System.EventArgs) Handles BtLast.Click
        If LesResponsable.CurrentPage < LesResponsable.PageCount Then
            If TxtRechecher.Text <> "" And TxtRechecher.Text <> "Rechercher" Then
                LesResponsable.CurrentPage = LesResponsable.PageCount
                TxtPage.Text = "Page " & LesResponsable.CurrentPage & "/" & LesResponsable.PageCount
                LesResponsable.RechPage(LgListRespoEtape, LesResponsable.CurrentPage)
                If Checktous.Checked Then Checktous.Checked = False
            Else
                LesResponsable.CurrentPage = LesResponsable.PageCount
                TxtPage.Text = "Page " & LesResponsable.CurrentPage & "/" & LesResponsable.PageCount
                If LesResponsable.PageCount > 0 Then
                    LesResponsable.LoadPage(LgListRespoEtape, LesResponsable.CurrentPage)
                End If
                If Checktous.Checked Then Checktous.Checked = False
            End If
        End If
    End Sub

    Private Sub BtPrev_Click(sender As System.Object, e As System.EventArgs) Handles BtPrev.Click
        If TxtRechecher.Text <> "" And TxtRechecher.Text <> "Rechercher" Then
            If (LesResponsable.CurrentPage > 1) Then
                LesResponsable.CurrentPage = LesResponsable.CurrentPage - 1
                TxtPage.Text = "Page " & LesResponsable.CurrentPage & "/" & LesResponsable.PageCount
                LesResponsable.RechPage(LgListRespoEtape, LesResponsable.CurrentPage)
                If Checktous.Checked Then Checktous.Checked = False
            End If
        Else
            If (LesResponsable.CurrentPage > 1) Then
                LesResponsable.CurrentPage = LesResponsable.CurrentPage - 1
                TxtPage.Text = "Page " & LesResponsable.CurrentPage & "/" & LesResponsable.PageCount
                LesResponsable.LoadPage(LgListRespoEtape, LesResponsable.CurrentPage)
                If Checktous.Checked Then Checktous.Checked = False
            End If
        End If

    End Sub

    Private Sub BtFrist_Click(sender As System.Object, e As System.EventArgs) Handles BtFrist.Click
        If LesResponsable.CurrentPage > 1 Then
            If TxtRechecher.Text <> "" And TxtRechecher.Text <> "Rechercher" Then
                LesResponsable.CurrentPage = 1
                TxtPage.Text = "Page " & LesResponsable.CurrentPage & "/" & LesResponsable.PageCount
                LesResponsable.RechPage(LgListRespoEtape, LesResponsable.CurrentPage)
                If Checktous.Checked Then Checktous.Checked = False
            Else
                LesResponsable.CurrentPage = 1
                TxtPage.Text = "Page " & LesResponsable.CurrentPage & "/" & LesResponsable.PageCount
                LesResponsable.LoadPage(LgListRespoEtape, LesResponsable.CurrentPage)
                If Checktous.Checked Then Checktous.Checked = False
            End If
        End If
    End Sub

    Private Sub BtAjouter_Click(sender As System.Object, e As System.EventArgs) Handles BtAjouter.Click
        AjoutPlanDeTiersToolStripMenuItem_Click(Me, e)
    End Sub

    Private Sub BtModifier_Click(sender As System.Object, e As System.EventArgs) Handles BtModifier.Click
        ModificationPlanDeTiersToolStripMenuItem_Click(Me, e)
    End Sub

    Private Sub BtSupprimer_Click(sender As System.Object, e As System.EventArgs) Handles BtSupprimer.Click
        SuppressionCompteToolStripMenuItem_Click(Me, e)
    End Sub

    Private Sub BtActualiser_Click(sender As System.Object, e As System.EventArgs) Handles BtActualiser.Click
        ResponsableEtape_Load(Me, e)
    End Sub

    Private Sub BtImprimer_Click(sender As System.Object, e As System.EventArgs) Handles BtImprimer.Click
        'ImprimerCompteDeTiersToolStripMenuItem_Click(Me, e)
    End Sub

    Private Sub Checktous_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Checktous.CheckedChanged
        Try
            If (ViewRespoEtape.RowCount > 0) Then
                If Checktous.Checked = True Then
                    For k As Integer = 0 To ViewRespoEtape.RowCount - 1
                        ViewRespoEtape.SetRowCellValue(k, "Choix", True)
                    Next
                Else
                    For k As Integer = 0 To ViewRespoEtape.RowCount - 1
                        ViewRespoEtape.SetRowCellValue(k, "Choix", False)
                    Next
                End If

            End If
        Catch ex As Exception
            FailMsg(ex.ToString())
        End Try
    End Sub

    Private Sub TxtRechecher_TextChanged(sender As Object, e As System.EventArgs) Handles TxtRechecher.TextChanged
        Try

            If TxtRechecher.Text = "" Or TxtRechecher.Text = "Rechercher" Then
                'TxtRechecher.Text = "Rechercher"
                'Plan_tiers_Load(Me, e)
                loadRespoEtape()
            Else
                LesResponsable.CurrentPage = 1
                LesResponsable.RechPage(LgListRespoEtape, LesResponsable.CurrentPage)
                TxtPage.Text = LesResponsable.CurrentPage & "/" & LesResponsable.PageCount
            End If

        Catch ex As Exception
            SuccesMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub TxtRechecher_Enter(sender As Object, e As EventArgs) Handles TxtRechecher.Enter
        If TxtRechecher.Text = "Rechercher" Then
            TxtRechecher.ResetText()
        End If
    End Sub

    Private Sub TxtRechecher_Leave(sender As Object, e As EventArgs) Handles TxtRechecher.Leave
        If TxtRechecher.Text = "" Then
            TxtRechecher.Text = "Rechercher"
        End If
    End Sub

    Public Sub CmbPageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPageSize.SelectedIndexChanged
        If CmbPageSize.SelectedIndex > -1 Then
            Dim ElementNumber As Decimal = Val(CmbPageSize.Text)
            With LesResponsable
                .PageSize = ElementNumber
            End With
            If TxtRechecher.Text = "" Or TxtRechecher.Text = "Rechercher" Then
                loadRespoEtape(1)
            Else
                LesResponsable.CurrentPage = 1
                TxtPage.Text = LesResponsable.CurrentPage & "/" & LesResponsable.PageCount
                LesResponsable.RechPage(LgListRespoEtape, LesResponsable.CurrentPage)
            End If
        Else
            CmbPageSize.SelectedIndex = 0
        End If
        If Checktous.Checked Then Checktous.Checked = False
    End Sub


End Class