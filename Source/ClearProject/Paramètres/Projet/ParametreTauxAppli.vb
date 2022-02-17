Imports DevExpress.XtraEditors.Repository

Public Class ParametreTauxAppli
    Dim ID_Bailleur() As String
    Dim ID_cat() As String
    Private Sub ParametreTauxAppli_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
                ComboBailleur.Text = ""
        cmbConvention.Text = ""
        cmbCategorie.Text = ""
        txtTaux.EditValue = "100"
        cmbConvention.Properties.Items.Clear()
        cmbCategorie.Properties.Items.Clear()
        'remplir bailleur
        ComboBailleur.Properties.Items.Clear()
        query = "select * from t_bailleur where CodeProjet='" & ProjetEnCours.ToString & "' order by CodeBailleur"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        ReDim ID_Bailleur(dt.Rows.Count)
        Dim i As Integer
        For Each rw As DataRow In dt.Rows
            ComboBailleur.Properties.Items.Add(rw(2).ToString & "--" & rw(1).ToString.Replace("&apost;", "'"))
            ID_Bailleur(i) = rw(0)
            i += 1
        Next
        query = "select * from t_convention where CodeBailleur=''"
        remplirTab(query)
        btEnregTaux.Enabled = True
        btnSupprimer.Enabled = False
        btnModifier.Enabled = False
    End Sub
    Private Sub InitChamps()

    End Sub

    Private Sub btEnregDPD_Click(sender As Object, e As EventArgs) Handles btEnregTaux.Click
        Dim erreur As String = ""
        If ComboBailleur.SelectedIndex = -1 Then
            erreur += "- Sélectionner un bailleur" & ControlChars.CrLf
        End If
        If cmbCategorie.SelectedIndex = -1 Then
            erreur += "- Sélectionner une catégorie de dépense" & ControlChars.CrLf
        End If
        If cmbConvention.SelectedIndex = -1 Then
            erreur += "- Sélectionner une convention" & ControlChars.CrLf
        End If
        If txtTaux.Text < 0 Or txtTaux.Text > 100 Then
            erreur += "- Choisir un taux correcte" & ControlChars.CrLf
        End If
        If erreur = "" Then
            Dim nbre As Integer = 0
            query = "select count(*) from t_categoriedepense a, t_gf_param_taux_categorie b where a.codeCateg=b.id_categorie and a.codeCateg='" & ID_cat(cmbCategorie.SelectedIndex) & "'"
            nbre = Val(ExecuteScallar(query))

            If nbre > 0 Then
                FailMsg("Cette catégorie a dejà été enregistrée")
            Else
                query = "insert into t_gf_param_taux_categorie values (NULL,'" & ID_cat(cmbCategorie.SelectedIndex) & "','" & ID_Bailleur(ComboBailleur.SelectedIndex) & "','" & txtTaux.Text & "')"
                ExecuteNonQuery(query)
                If ComboBailleur.SelectedIndex <> -1 Then
                    cmbCategorie.Text = ""
                    query = "select a.*, b.* from t_categoriedepense a, t_gf_param_taux_categorie b where a.codeCateg=b.id_categorie and b.id_bailleur='" & ID_Bailleur(ComboBailleur.SelectedIndex) & "'"
                    remplirTab(query)
                End If
                SuccesMsg("taux enregistré avec succèss")
                txtTaux.EditValue = "100"
            End If
        Else
            FailMsg("Veuillez : " & ControlChars.CrLf & erreur)
        End If
    End Sub

    Private Sub cmbConvention_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbConvention.SelectedIndexChanged
        If cmbConvention.SelectedIndex <> -1 Then
            cmbCategorie.Text = ""
            cmbCategorie.Properties.Items.Clear()
            query = "select a.* from t_categoriedepense a, t_convention b where a.CodeConvention=b.codeConvention and b.codeConvention='" & EnleverApost(cmbConvention.Text) & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            ReDim ID_cat(dt.Rows.Count)
            Dim i As Integer
            For Each rw As DataRow In dt.Rows
                cmbCategorie.Properties.Items.Add(rw(1).ToString & "--" & rw(3).ToString.Replace("&apost;", "'"))
                ID_cat(i) = rw(0)
                i += 1
            Next
        End If
    End Sub

    Private Sub ComboBailleur_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBailleur.SelectedIndexChanged
        If ComboBailleur.SelectedIndex <> -1 Then
            cmbConvention.Properties.Items.Clear()
            query = "select * from t_convention where CodeBailleur='" & ID_Bailleur(ComboBailleur.SelectedIndex) & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cmbConvention.Properties.Items.Add(rw(0).ToString.Replace("&apost;", "'"))
                cmbConvention.Text = rw(0).ToString.Replace("&apost;", "'")
            Next
            query = "select a.*, b.* from t_categoriedepense a, t_gf_param_taux_categorie b where a.codeCateg=b.id_categorie and b.id_bailleur='" & ID_Bailleur(ComboBailleur.SelectedIndex) & "'"
            remplirTab(query)
        End If


    End Sub
    Sub remplirTab(ByVal requete As String)

        Dim dtCat As New DataTable
        Try
            dtCat.Columns.Clear()

            dtCat.Columns.Add("Code", Type.GetType("System.String"))
            dtCat.Columns.Add("id", Type.GetType("System.String"))
            dtCat.Columns.Add("CodeRef", Type.GetType("System.String"))
            dtCat.Columns.Add("référence", Type.GetType("System.String"))
            dtCat.Columns.Add("Catégorie de dépenses", Type.GetType("System.String"))
            dtCat.Columns.Add("Taux applicable", Type.GetType("System.String"))
            dtCat.Columns.Add("Taux", Type.GetType("System.String"))
            Dim cptr As Integer = 0
            Dim dt As DataTable = ExcecuteSelectQuery(requete)
            For Each rw In dt.Rows
                Dim DrE = dtCat.NewRow()
                cptr += 1
                DrE(0) = TabTrue(cptr - 1)
                DrE(1) = rw("Id_param_taux").ToString
                DrE(2) = rw("CodeCateg").ToString
                DrE(3) = rw("NumCateg").ToString
                DrE(4) = MettreApost(rw("LibelleCateg").ToString)
                DrE(5) = rw("taux").ToString & " %"
                DrE(6) = rw("taux").ToString
                dtCat.Rows.Add(DrE)
            Next

            GridCategorie.DataSource = dtCat

            If ViewCategorie.Columns(1).Visible Then
                ViewCategorie.Columns("CodeRef").OptionsColumn.AllowEdit = False
                ViewCategorie.Columns("référence").OptionsColumn.AllowEdit = False
                ViewCategorie.Columns("Catégorie de dépenses").OptionsColumn.AllowEdit = False
                ViewCategorie.Columns("Taux applicable").OptionsColumn.AllowEdit = False
                ViewCategorie.Columns(0).Visible = False
                ViewCategorie.Columns(6).Visible = False
                ViewCategorie.Columns(1).Visible = False
                ViewCategorie.Columns(2).Visible = False
                ViewCategorie.Columns(3).Width = 100
                ViewCategorie.Columns(4).Width = 300
                ViewCategorie.Columns(5).Width = 100

                ViewCategorie.Appearance.Row.Font = New Font("Times New Roman", 12, FontStyle.Regular)

                ViewCategorie.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            End If
        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub ViewCategorie_DoubleClick(sender As Object, e As EventArgs) Handles ViewCategorie.DoubleClick

        'If (ViewCategorie.RowCount > 0) Then
        '    drx = ViewCategorie.GetDataRow(ViewCategorie.FocusedRowHandle)
        '    Dim id As String = drx(1).ToString



        '    If (MsgBox("Voulez-vous supprimer la catégorie de dépense" & vbNewLine & drx(3).ToString & " - " & drx(4).ToString & " ?", MsgBoxStyle.YesNo, "Suppression de catégorie") = MsgBoxResult.Yes) Then


        '        query = "DELETE from t_gf_param_taux_categorie where Id_param_taux='" & id & "'"
        '        ExecuteNonQuery(query)
        '        SuccesMsg("Suppression effectuée avec succès")
        '        query = "select a.*, b.* from t_categoriedepense a, t_gf_param_taux_categorie b where a.codeCateg=b.id_categorie and b.id_bailleur='" & ID_Bailleur(ComboBailleur.SelectedIndex) & "'"
        '        remplirTab(query)
        '    End If
        'End If
        If ViewCategorie.RowCount > 0 Then
            txtTaux.Value = ViewCategorie.GetFocusedRowCellDisplayText("Taux")
            cmbCategorie.Text = ViewCategorie.GetFocusedRowCellDisplayText("référence") & "--" & ViewCategorie.GetFocusedRowCellDisplayText("Catégorie de dépenses")
            txtCode.Text = ViewCategorie.GetFocusedRowCellDisplayText("id")
            btnModifier.Enabled = True
            btEnregTaux.Enabled = False
            btnSupprimer.Enabled = True
        End If
    End Sub

    Private Sub ViewCategorie_Click(sender As Object, e As EventArgs) Handles ViewCategorie.Click
        'If ViewCategorie.RowCount > 0 Then
        '    txtTaux.Value = ViewCategorie.GetFocusedRowCellDisplayText("Taux")
        '    cmbCategorie.Text = ViewCategorie.GetFocusedRowCellDisplayText("référence") & "--" & ViewCategorie.GetFocusedRowCellDisplayText("Catégorie de dépenses")
        '    btnModifier.Enabled = True
        '    btEnregTaux.Enabled = False
        '    txtCode.Text = ViewCategorie.GetFocusedRowCellDisplayText("id")
        'End If
    End Sub

    Private Sub btnModifier_Click(sender As Object, e As EventArgs) Handles btnModifier.Click
        Dim erreur As String = ""
        If ComboBailleur.SelectedIndex = -1 Then
            erreur += "- Sélectionner un bailleur" & ControlChars.CrLf
        End If
        If cmbCategorie.SelectedIndex = -1 Then
            erreur += "- Sélectionner une catégorie de dépense" & ControlChars.CrLf
        End If
        If cmbConvention.SelectedIndex = -1 Then
            erreur += "- Sélectionner une convention" & ControlChars.CrLf
        End If
        If txtTaux.Text < 0 Or txtTaux.Text > 100 Then
            erreur += "- Choisir un taux correcte" & ControlChars.CrLf
        End If
        If erreur = "" Then
            query = "update t_gf_param_taux_categorie set taux='" & txtTaux.Value & "' where Id_param_taux='" & txtCode.Text & "'"
            ExecuteNonQuery(query)
            SuccesMsg("taux modifié avec succès")
            If ComboBailleur.SelectedIndex <> -1 Then
                query = "select a.*, b.* from t_categoriedepense a, t_gf_param_taux_categorie b where a.codeCateg=b.id_categorie and b.id_bailleur='" & ID_Bailleur(ComboBailleur.SelectedIndex) & "'"
                remplirTab(query)
            End If
            cmbCategorie.Text = ""
            txtTaux.Value = "100"
            btnModifier.Enabled = False
            btnSupprimer.Enabled = False
            btEnregTaux.Enabled = True
        Else
            FailMsg("Veuillez : " & ControlChars.CrLf & erreur)
        End If
    End Sub

    Private Sub btnSupprimer_Click(sender As Object, e As EventArgs) Handles btnSupprimer.Click
        If (MsgBox("Voulez-vous supprimer cette catégorie de dépense ?", MsgBoxStyle.YesNo, "Suppression de catégorie") = MsgBoxResult.Yes) Then
            query = "DELETE from t_gf_param_taux_categorie where Id_param_taux='" & txtCode.Text & "'"
            ExecuteNonQuery(query)
            SuccesMsg("Suppression effectuée avec succès")
            query = "select a.*, b.* from t_categoriedepense a, t_gf_param_taux_categorie b where a.codeCateg=b.id_categorie and b.id_bailleur='" & ID_Bailleur(ComboBailleur.SelectedIndex) & "'"
            remplirTab(query)
            btnModifier.Enabled = False
            btnSupprimer.Enabled = False
            btEnregTaux.Enabled = True
            cmbCategorie.Text = ""
            txtTaux.Value = "100"
        End If
    End Sub

    Private Sub txtTaux_EditValueChanged(sender As Object, e As EventArgs) Handles txtTaux.EditValueChanged

    End Sub
End Class