Public Class TypeTiers
    Private Sub GRHPret_Load(sender As Object, e As EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        init()
        LoadType()
    End Sub
    Private Sub LoadType()
        Try
            LoadCompte()
            query = "SELECT CODE_TCPT,replace(LIBELLE_TCPT,'&apost;',''''),Code_CL FROM t_comp_type_compte ORDER BY LIBELLE_TCPT ASC"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            dt.Columns(0).Caption = "Id"
            dt.Columns(1).Caption = "Libellé"
            dt.Columns(2).Caption = "Compte"
            dgPrets.DataSource = dt
            GridView1.Columns(0).Visible = False
            GridView1.OptionsView.ColumnAutoWidth = True
            GridView1.OptionsBehavior.AutoExpandAllGroups = True
            GridView1.VertScrollVisibility = True
            GridView1.HorzScrollVisibility = True
            GridView1.BestFitColumns()
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub
    Private Sub init()
        btEnregistrer.Enabled = True
        cmbCompte.Enabled = True
        btModifier.Enabled = False
        txtLibelle.ResetText()
        cmbCompte.ResetText()
    End Sub

    Private Sub dgPrets_Click(sender As Object, e As EventArgs) Handles dgPrets.Click
        If (GridView1.RowCount > 0) Then
            drx = GridView1.GetDataRow(GridView1.FocusedRowHandle)
            Dim IDL = drx(0).ToString
            ColorRowGrid(GridView1, "[CODE_TCPT]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(GridView1, "[CODE_TCPT]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
            btModifier.Enabled = True
            btEnregistrer.Enabled = False
            cmbCompte.Enabled = False
            txtLibelle.Text = MettreApost(drx(1).ToString())
            Try
                cmbCompte.Text = drx(2) & " | " & MettreApost(ExecuteScallar("SELECT LIBELLE_CL FROM t_comp_classe WHERE CODE_CL='" & drx(2) & "'"))
            Catch ex As Exception
                cmbCompte.Text = drx(2)
            End Try
        End If
    End Sub

    Private Sub SupprimerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupprimerToolStripMenuItem.Click
        If GridView1.FocusedRowHandle > -1 And GridView1.RowCount > 0 Then
            drx = GridView1.GetDataRow(GridView1.FocusedRowHandle)
            Dim IDL = drx(0).ToString()
            Dim Compte = drx(2).ToString()
            ColorRowGrid(GridView1, "[CODE_TCPT]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(GridView1, "[CODE_TCPT]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
            query = "select * from t_comp_ligne_ecriture where CODE_CPT LIKE '" & Compte & "%'"
            Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
            If dtVerif.Rows.Count > 0 Then
                SuccesMsg("Ce type ne peut pas être supprimé.")
                Exit Sub
            End If
            If ConfirmMsg("Voulez-vous vraiment supprimer?") = DialogResult.Yes Then
                Try
                    query = "delete from t_comp_type_compte where CODE_TCPT=" & drx(0).ToString
                    ExecuteNonQuery(query)
                    SuccesMsg("Type supprimé avec succès")
                    LoadType()
                    init()
                Catch ex As Exception
                    SuccesMsg("Suppression Impossible." & vbNewLine & ex.ToString())
                End Try
            End If
        End If
    End Sub

    Private Sub btModifier_Click(sender As Object, e As EventArgs) Handles btModifier.Click
        If Len(Trim(txtLibelle.Text)) = 0 Then
            SuccesMsg("Veuillez saisir le libellé.")
            txtLibelle.Focus()
            Exit Sub
        End If
        'If cmbCompte.SelectedIndex = -1 Then
        '    SuccesMsg("Veuillez choisir le compte.")
        '    cmbCompte.Select()
        '    Exit Sub
        'End If
        Try
            Dim inde As Decimal = GridView1.FocusedRowHandle
            drx = GridView1.GetDataRow(inde)
            query = "update t_comp_type_compte set LIBELLE_TCPT='" & EnleverApost(txtLibelle.Text.Trim) & "' where CODE_TCPT=" & drx(0)
            ExecuteNonQuery(query)
            SuccesMsg("Type modifié avec succès")
            LoadType()
            init()
            GridView1.FocusedRowHandle = inde
        Catch ex As Exception
            FailMsg("Modification Impossible!" & vbNewLine & ex.ToString())
        End Try
    End Sub

    Private Sub btEnregistrer_Click(sender As Object, e As EventArgs) Handles btEnregistrer.Click
        If Len(Trim(txtLibelle.Text)) = 0 Then
            SuccesMsg("Veuillez saisir le libellé.")
            txtLibelle.Focus()
            Exit Sub
        End If
        If cmbCompte.SelectedIndex = -1 Then
            SuccesMsg("Veuillez choisir le compte.")
            cmbCompte.Select()
            Exit Sub
        End If
        Try
            Dim Compte As String = cmbCompte.Text.Split(" | ")(0)
            'query = "SELECT * FROM t_comp_type_compte WHERE Code_CL='" & Compte & "'"
            'Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
            'If dtVerif.Rows.Count > 0 Then
            '    SuccesMsg(" saisir le libellé.")
            '    cmbCompte.Select()
            '    Exit Sub
            'End If
            query = "insert into t_comp_type_compte values(null,'" & EnleverApost(txtLibelle.Text.Trim()) & "','" & Compte & "')"
            ExecuteNonQuery(query)
            SuccesMsg("Type ajouté avec succès")
            LoadType()
            init()
        Catch ex As Exception
            FailMsg("Impossible d'ajouter le prêt." & vbNewLine & ex.ToString())
        End Try
    End Sub

    Private Sub btRetour_Click(sender As Object, e As EventArgs) Handles btRetour.Click
        init()
    End Sub
    Private Sub LoadCompte()
        query = "SELECT DISTINCT CODE_CL, LIBELLE_CL FROM t_comp_classe WHERE CODE_CL NOT IN(SELECT Code_CL FROM t_comp_type_compte)"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        cmbCompte.Properties.Items.Clear()
        For Each rw In dt.Rows
            cmbCompte.Properties.Items.Add(rw("CODE_CL") & " | " & MettreApost(rw("LIBELLE_CL")))
        Next
    End Sub
End Class