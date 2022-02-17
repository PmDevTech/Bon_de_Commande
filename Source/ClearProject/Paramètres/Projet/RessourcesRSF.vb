Imports MySql.Data.MySqlClient

Public Class RessourcesRSF
    Private Sub CompteBailleur_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ChargerNumComptable()
        ChargerBailleur()
        ChargerGridCompte()
        InitForm()
    End Sub
    Private Sub InitForm()
        CmbBailleur.ResetText()
        CmbNumComptable.ResetText()
        txtLibelle.ResetText()
        cmbCondition.ResetText()
        BtEnregistrer.Enabled = True
        btModifier.Enabled = False
        btDel.Enabled = False
        CmbBailleur.Focus()
    End Sub
    Private Sub ChargerGridCompte()
        Dim dtCompte = New DataTable()
        dtCompte.Columns.Clear()
        dtCompte.Columns.Add("Id", Type.GetType("System.String"))
        dtCompte.Columns.Add("Bailleur", Type.GetType("System.String"))
        dtCompte.Columns.Add("Libellé", Type.GetType("System.String"))
        dtCompte.Columns.Add("Compte", Type.GetType("System.String"))
        dtCompte.Columns.Add("Condition", Type.GetType("System.String"))

        dtCompte.Rows.Clear()

        query = "SELECT * FROM `t_rsf_ressources` WHERE CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Dim drS = dtCompte.NewRow()

            drS(0) = rw("Id").ToString
            drS(1) = rw("InitialeBailleur").ToString
            drS(2) = MettreApost(rw("Libelle").ToString)
            drS(3) = rw("Code_SC").ToString
            drS(4) = rw("Type").ToString

            dtCompte.Rows.Add(drS)
        Next
        GridCompte.DataSource = dtCompte
        ViewCompte.OptionsView.ColumnAutoWidth = True
        ViewCompte.Columns("Id").Visible = False
        ViewCompte.Columns("Bailleur").Width = 75
        ViewCompte.Columns("Compte").Width = 85
        ViewCompte.Columns("Libellé").Width = 200
        ViewCompte.OptionsCustomization.AllowColumnMoving = False
        ViewCompte.OptionsCustomization.AllowColumnResizing = False

        ViewCompte.Columns("Bailleur").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewCompte.Columns("Compte").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        ViewCompte.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewCompte, "[Id]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub
    Private Sub ChargerNumComptable()
        query = "select CODE_SC, LIBELLE_SC from T_COMP_SOUS_CLASSE WHERE CODE_SC NOT LIKE '2%' AND CODE_SC NOT LIKE '6%' order by CODE_SC"
        CmbNumComptable.Text = ""
        CmbNumComptable.Properties.Items.Clear()
        Dim dt0 = ExcecuteSelectQuery(query)
        For Each rw In dt0.Rows
            CmbNumComptable.Properties.Items.Add(rw(0).ToString & " - " & MettreApost(rw(1).ToString))
        Next
    End Sub
    Private Sub ChargerBailleur()
        query = "select InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' order by InitialeBailleur"
        Dim dt = ExcecuteSelectQuery(query)
        CmbBailleur.Properties.Items.Clear()
        For Each rw In dt.Rows
            CmbBailleur.Properties.Items.Add(rw("InitialeBailleur").ToString)
        Next
    End Sub
    Private Function LoadCodeSC(CodeSC As String) As String
        Dim Libelle As String = MettreApost(ExecuteScallar("SELECT LIBELLE_SC FROM T_COMP_SOUS_CLASSE WHERE CODE_SC='" & CodeSC & "'"))
        If Libelle.Length > 0 Then
            Return CodeSC & " - " & Libelle
        End If
        Return CodeSC
    End Function
    Private Sub BtEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click
        If CmbBailleur.SelectedIndex = -1 Then
            SuccesMsg("Veuillez choisir un bailleur.")
            CmbBailleur.Focus()
            Exit Sub
        End If
        If CmbNumComptable.SelectedIndex = -1 Then
            SuccesMsg("Veuillez choisir le compte comptable.")
            CmbNumComptable.Focus()
            Exit Sub
        End If
        If cmbCondition.SelectedIndex = -1 Then
            SuccesMsg("veuillez choisir le type d'opération à récupérer.")
            cmbCondition.Focus()
            Exit Sub
        End If
        If txtLibelle.Text.Trim.Length = 0 Then
            SuccesMsg("Entrer le libellé du compte svp.")
            txtLibelle.Focus()
            Exit Sub
        End If

        Dim CodeSC As String = CmbNumComptable.Text.Split(" - ")(0)
        Dim Bailleur As String = CmbBailleur.Text

        query = "SELECT COUNT(*) FROM t_rsf_ressources WHERE CodeProjet='" & ProjetEnCours & "' and CODE_SC='" & CodeSC & "'"
        If Val(ExecuteScallar(query)) > 0 Then
            FailMsg("Le compte " & CodeSC & " est déjà enregistré")
            Exit Sub
        End If

        query = "INSERT INTO t_rsf_ressources VALUES(NULL,'" & Bailleur & "','" & CodeSC & "','" & EnleverApost(txtLibelle.Text) & "','" & cmbCondition.Text & "','" & ProjetEnCours & "')"
        ExecuteNonQuery(query)
        SuccesMsg("Enregistrement effectué avec succès.")
        ChargerGridCompte()
        InitForm()

    End Sub
    Private Sub GridCompte_Click(sender As System.Object, e As System.EventArgs) Handles GridCompte.Click
        If (ViewCompte.RowCount > 0) Then
            Dim drx = ViewCompte.GetDataRow(ViewCompte.FocusedRowHandle)
            Dim IDL = drx("Compte").ToString
            ColorRowGrid(ViewCompte, "[Id]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewCompte, "[Compte]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
            CmbBailleur.Text = drx("Bailleur")
            CmbNumComptable.Text = LoadCodeSC(drx("Compte"))
            cmbCondition.Text = drx("Condition")
            txtLibelle.Text = drx("Libellé")
            BtEnregistrer.Enabled = False
            btModifier.Enabled = True
            btDel.Enabled = True
        End If
    End Sub
    Private Sub btModifier_Click(sender As System.Object, e As System.EventArgs) Handles btModifier.Click
        'Code de modification de l'enregistrement choisi
        If (ViewCompte.RowCount > 0) And ViewCompte.FocusedRowHandle > -1 Then
            If CmbBailleur.SelectedIndex = -1 Then
                SuccesMsg("Veuillez choisir un bailleur.")
                CmbBailleur.Focus()
                Exit Sub
            End If
            If CmbNumComptable.SelectedIndex = -1 Then
                SuccesMsg("Veuillez choisir le compte comptable.")
                CmbNumComptable.Focus()
                Exit Sub
            End If
            If cmbCondition.SelectedIndex = -1 Then
                SuccesMsg("veuillez choisir le type d'opération à récupérer.")
                cmbCondition.Focus()
                Exit Sub
            End If
            If txtLibelle.Text.Trim.Length = 0 Then
                SuccesMsg("Entrer le libellé du compte svp.")
                txtLibelle.Focus()
                Exit Sub
            End If

            Dim CodeSC As String = CmbNumComptable.Text.Split(" - ")(0)
            Dim Bailleur As String = CmbBailleur.Text
            Dim drx = ViewCompte.GetDataRow(ViewCompte.FocusedRowHandle)
            query = "UPDATE t_rsf_ressources SET InitialeBailleur = '" & Bailleur & "', Code_SC = '" & CodeSC & "', Libelle = '" & EnleverApost(txtLibelle.Text) & "',Type='" & cmbCondition.Text & "',CodeProjet ='" & ProjetEnCours & "' WHERE Id= '" & drx("Id").ToString & "'"
            Try
                ExecuteNonQuery(query)
                SuccesMsg("Modification effectuée avec succès.")
                ChargerGridCompte()
                InitForm()
            Catch ex As Exception
                FailMsg("Impossible de modifier : " & vbNewLine & ex.ToString())
            End Try
        End If

    End Sub
    Private Sub btDel_Click(sender As System.Object, e As System.EventArgs) Handles btDel.Click
        If ViewCompte.FocusedRowHandle <> -1 And ViewCompte.RowCount > 0 Then
            If ConfirmMsg("Voulez-vous vraiment supprimer?") = DialogResult.Yes Then
                Dim drx = ViewCompte.GetDataRow(ViewCompte.FocusedRowHandle)
                query = "DELETE FROM t_rsf_ressources WHERE Id = '" & drx("Id").ToString & "'"
                ExecuteNonQuery(query)

                SuccesMsg("Suppression effectuée avec succès")
                ChargerGridCompte()
                InitForm()
            End If
        End If
    End Sub

    Private Sub btRetour_Click(sender As Object, e As EventArgs) Handles btRetour.Click
        InitForm()
    End Sub
End Class