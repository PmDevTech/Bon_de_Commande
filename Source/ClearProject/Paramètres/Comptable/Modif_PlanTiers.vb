Public Class Modif_PlanTiers 
    Dim dtcontact = New DataTable()
    Dim dtBanque = New DataTable()
    Dim idtcpt As String
    Private Class1 As New TiersClass

    Private Sub Modif_PlanTiers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        Me.Text = "Modifier un compte de tiers"

        'remplir localité
        RemplirCombo2(Combville, "t_zonegeo", "libellezone")
        RemplirCombo2(cmbLocaliteBanque, "t_zonegeo", "libellezone")

        'remplir service
        RemplirCombo2(Combserv, "t_service", "nomservice")

        'remplir service
        RemplirCombo2(cmbDeviseBanque, "T_Devise", "AbregeDevise")

        'remplir les sous classe du plan comptable
        Combsc.Properties.Items.Clear()
        query = "select * from T_COMP_SOUS_CLASSE where code_sc like '4%' ORDER BY code_sc"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rwx As DataRow In dt.Rows
            Combsc.Properties.Items.Add(rwx(0).ToString & "   " & MettreApost(rwx(2).ToString))
        Next

        'remplir le type compte
        cmbType.Properties.Items.Clear()
        query = "select * from T_COMP_TYPE_COMPTE ORDER BY Code_CL"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cmbType.Properties.Items.Add(rw("Code_CL") & " | " & MettreApost(rw("libelle_tcpt")).ToString)
        Next
        txtCodeTiers.Enabled = False
        cmbType.Enabled = False
        txtIntitule.Focus()

        dtBanque.Columns.Add("Id", Type.GetType("System.String"))
        dtBanque.Columns.Add("Banque", Type.GetType("System.String"))
        dtBanque.Columns.Add("SWIFT", Type.GetType("System.String"))
        dtBanque.Columns.Add("Intitulé", Type.GetType("System.String"))
        dtBanque.Columns.Add("Type", Type.GetType("System.String"))
        dtBanque.Columns.Add("Numéro", Type.GetType("System.String"))
        dtBanque.Columns.Add("Devise", Type.GetType("System.String"))
        dtBanque.Columns.Add("Localité", Type.GetType("System.String"))
        dtBanque.Columns.Add("Adresse", Type.GetType("System.String"))
        dtBanque.Columns.Add("Sigle", Type.GetType("System.String"))
        dtBanque.Columns.Add("Commentaire", Type.GetType("System.String"))
        dtBanque.Columns.Add("Edit", Type.GetType("System.Boolean"))
        LgListBanque.DataSource = dtBanque
        ViewBanques.Columns("Id").Visible = False
        ViewBanques.Columns("Sigle").Visible = False
        ViewBanques.Columns("Edit").Visible = False

        ViewBanques.Columns("Banque").MaxWidth = 200
        ViewBanques.Columns("Devise").MaxWidth = 50
        ViewBanques.Columns("Type").MaxWidth = 60
        'ViewBanques.OptionsView.ColumnAutoWidth = True
        ViewBanques.Columns("Banque").Width = 200
        ViewBanques.Columns("Devise").Width = 50
        ViewBanques.Columns("Type").Width = 60
        'ViewBanques.OptionsBehavior.AutoExpandAllGroups = True
        ViewBanques.VertScrollVisibility = True
        ViewBanques.HorzScrollVisibility = True
        'ViewBanques.BestFitColumns()
        LoadOldBanque(EnleverApost(txtCodeTiers.Text))
        'txtCodeTiers.Enabled = False
    End Sub
    Private Sub LoadOldBanque(CodeCPT)
        query = "SELECT * FROM t_comp_banque WHERE CODE_CPT='" & CodeCPT & "' AND CODE_PROJET='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Dim drS = dtBanque.NewRow()
            drS("Id") = rw("REFBANQ")
            drS("Banque") = rw("LibelleBanque").ToString().MettreApostrophe()
            drS("Type") = rw("TypeCompte").ToString().MettreApostrophe()
            drS("Numéro") = rw("RIBBANQ").ToString().MettreApostrophe()
            drS("Devise") = rw("DEVISEBANQ").ToString().MettreApostrophe()
            drS("Sigle") = rw("CODEBANQ").ToString().MettreApostrophe()
            drS("SWIFT") = rw("CodeSWIFT").ToString().MettreApostrophe()
            drS("Localité") = rw("VillePaysBanque").ToString().MettreApostrophe()
            drS("Adresse") = rw("AdresseBanque").ToString().MettreApostrophe()
            drS("Intitulé") = rw("IntituleCompte").ToString().MettreApostrophe()
            Dim Type As String = rw("NatureCompte").ToString
            If Type = "Default" Then
                Type = "Par défaut"
            ElseIf Type = "Corresp" Then
                Type = "Correspondant bancaire"
            End If
            drS("Commentaire") = Type.MettreApostrophe()
            drS("Edit") = False
            dtBanque.Rows.Add(drS)
        Next
    End Sub
    Private Sub btenregisterj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btenregisterj.Click
        Try

            Dim erreur As String = ""
            Dim sc() As String
            sc = Combsc.Text.Split("   ")

            If txtCodeTiers.Text = "" Then
                erreur += "- Numéro compte" & ControlChars.CrLf
            End If
            'If Combsc.SelectedIndex = -1 Then
            '    erreur += "- Compte sélectif" & ControlChars.CrLf
            'End If
            If txtIntitule.Text = "" Then
                erreur += "- Nom compte tiers" & ControlChars.CrLf
            End If

            If cmbType.SelectedIndex = -1 Then
                erreur += "- Type compte tiers" & ControlChars.CrLf
            End If

            If erreur = "" Then
                query = "Update t_comp_compte set abrege_cpt='" & EnleverApost(Txtabr.Text) & "', CODE_V='" & Combville.Text.EnleverApostrophe() & "', nom_cpt='" & EnleverApost(txtIntitule.Text) & "', adresse_cpt='" & EnleverApost(txtadresse.Text) & "', telephone='" & txtcontact.Text & "', email='" & EnleverApost(txtmail.Text) & "', rc='" & Txtrc.Text & "', cc='" & Txtcc.Text & "', telephone2='" & Txttel.Text & "', fax='" & Txtfax.Text & "', site='" & Txtsite.Text & "', qualite='" & Txtqualite.Text & "' where code_cpt='" & txtCodeTiers.Text & "' and code_projet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
                'insertion de la banque et leur rattache
                If PageBanque.PageVisible Then
                    Dim IdBanque As String = String.Empty
                    Dim CodeCPT As String = EnleverApost(txtCodeTiers.Text).ToUpper()

                    'Vérifications des champs supprimés
                    For i = 0 To ViewBanques.RowCount - 1
                        Dim rw As DataRow = ViewBanques.GetDataRow(i)
                        If rw("Id") <> "##" Then
                            IdBanque &= "'" & rw("Id") & "',"
                        End If
                    Next
                    If IdBanque = String.Empty Then
                        query = "DELETE FROM t_comp_banque WHERE CODE_CPT='" & CodeCPT & "' AND CODE_PROJET='" & ProjetEnCours & "'"
                        ExecuteNonQuery(query)
                    Else
                        IdBanque = Mid(IdBanque, 1, (IdBanque.Length - 1))
                        query = "DELETE FROM t_comp_banque WHERE REFBANQ NOT IN(" & IdBanque & ") AND CODE_CPT='" & CodeCPT & "' AND CODE_PROJET='" & ProjetEnCours & "'"
                        ExecuteNonQuery(query)
                    End If

                    'Modification des champes et insertion des nouvelles saisies
                    For i = 0 To ViewBanques.RowCount - 1
                        Dim rw As DataRow = ViewBanques.GetDataRow(i)
                        Dim Type As String = String.Empty
                        If rw("Commentaire").ToString() = "Par défaut" Then
                            Type = "Default"
                        ElseIf rw("Commentaire").ToString() = "Correspondant bancaire" Then
                            Type = "Corresp"
                        End If
                        If rw("Id") = "##" Then
                            query = "insert into T_COMP_BANQUE values (NULL,'" & rw("Sigle").ToString().EnleverApostrophe() & "',"
                            query &= "'" & rw("Banque").ToString().Replace(" (" & rw("Sigle").ToString() & ")", "").EnleverApostrophe() & "',"
                            query &= "'" & rw("Numéro").ToString().EnleverApostrophe() & "','" & rw("SWIFT").ToString.EnleverApostrophe() & "',"
                            query &= "'" & rw("Intitulé").ToString.EnleverApostrophe() & "','" & rw("Adresse").ToString.EnleverApostrophe() & "',"
                            query &= "'" & rw("Localité").ToString.EnleverApostrophe() & "','" & CodeCPT & "','" & rw("Devise").ToString() & "','" & rw("Type").ToString() & "','" & Type.EnleverApostrophe() & "','" & ProjetEnCours & "')"
                            ExecuteNonQuery(query)
                            query = "SELECT MAX(REFBANQ) FROM t_comp_banque WHERE CODE_CPT='" & CodeCPT & "' AND CODE_PROJET='" & ProjetEnCours & "'"
                            Dim LastId As String = ExecuteScallar(query)
                            ViewBanques.SetRowCellValue(i, "Id", LastId)
                        Else
                            query = "UPDATE T_COMP_BANQUE SET CODEBANQ='" & rw("Sigle").ToString().EnleverApostrophe() & "', "
                            query &= "LibelleBanque='" & rw("Banque").ToString().Replace(" (" & rw("Sigle").ToString() & ")", "").EnleverApostrophe() & "', "
                            query &= "RIBBANQ='" & rw("Numéro").ToString().EnleverApostrophe() & "', DEVISEBANQ='" & rw("Devise").ToString() & "', "
                            query &= "TypeCompte='" & rw("Type").ToString() & "', CodeSWIFT='" & rw("SWIFT").ToString.EnleverApostrophe() & "', "
                            query &= "IntituleCompte='" & rw("Intitulé").ToString.EnleverApostrophe() & "', AdresseBanque='" & rw("Adresse").ToString.EnleverApostrophe() & "', "
                            query &= "VillePaysBanque='" & rw("Localité").ToString.EnleverApostrophe() & "', NatureCompte='" & Type.EnleverApostrophe() & "' WHERE REFBANQ='" & rw("Id") & "'"
                            ExecuteNonQuery(query)
                        End If
                    Next
                End If

                If PageContact.PageVisible Then
                    For i = 0 To ViewContact.RowCount - 1
                        'insertion dans la table banque
                        query = "insert into T_COMP_IDENTIFIANT values (NULL,'" & EnleverApost(txtCodeTiers.Text) & "','" & EnleverApost(dtcontact.Rows(i).item(0).ToString) & "','" & EnleverApost(dtcontact.Rows(i).item(2).ToString) & "','" & EnleverApost(dtcontact.Rows(i).item(4).ToString) & "','" & EnleverApost(dtcontact.Rows(i).item(5).ToString) & "','" & EnleverApost(dtcontact.Rows(i).item(6).ToString) & "','" & ProjetEnCours & "','" & EnleverApost(dtcontact.Rows(i).item(3).ToString) & "','" & EnleverApost(dtcontact.Rows(i).item(1).ToString) & "')"
                        ExecuteNonQuery(query)
                    Next
                End If

                EffacerTexBox4(PanelControl5)
                'actualiser la table
                query = "select count(*) from t_comp_compte where CODE_PROJET='" & ProjetEnCours & "'"
                Dim nbre = ExecuteScallar(query)
                Plan_tiers.CmbPageSize.Text = 25
                With Class1

                    .PageSize = Plan_tiers.CmbPageSize.Text
                    .MaxRec = nbre \ .PageSize
                    .PageCount = .MaxRec \ .PageSize
                    If (.MaxRec Mod .PageSize) > 0 Then
                        .PageCount = .PageCount + 1
                    End If

                    .CurrentPage = 1
                    .Resqlconno = 0
                    .LoadPage(Plan_tiers.LgListCompteTier, .CurrentPage)
                End With

                Plan_tiers.ViewCptTiers.Columns(0).Visible = True
                Plan_tiers.ViewCptTiers.Columns(0).Width = 20
                Plan_tiers.ViewCptTiers.OptionsView.ColumnAutoWidth = True
                Plan_tiers.ViewCptTiers.OptionsBehavior.AutoExpandAllGroups = True
                Plan_tiers.ViewCptTiers.VertScrollVisibility = True
                Plan_tiers.ViewCptTiers.HorzScrollVisibility = True
                Plan_tiers.ViewCptTiers.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)

                SuccesMsg("Modification Effectuée avec Succès.")
                Me.Close()
            Else
                SuccesMsg("Veuillez remplir ces champs : " & ControlChars.CrLf + erreur)
            End If

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub txtnom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIntitule.TextChanged
        If (txtIntitule.Text.Replace(" ", "") <> "") Then

            Dim partS() As String = (txtIntitule.Text.Replace("'", "").Replace("  ", " ").Replace(" le", "").Replace(" la", "").Replace(" les", "").Replace(" l'", "").Replace(" de", "").Replace(" du", "").Replace(" des", "").Replace(" d'", "")).Split(" "c)
            Dim CodeS As String = ""
            For Each elt In partS
                CodeS = CodeS & Mid(elt, 1, 1).ToUpper
            Next
            Txtabr.Text = CodeS
        Else
            Txtabr.Text = ""
        End If
    End Sub

    Private Sub LgListCumul_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LgListCumul.Click
        Try
            If (ViewCumul.RowCount > 0) Then
                drx = ViewCumul.GetDataRow(ViewCumul.FocusedRowHandle)
                Dim ID = drx(0).ToString
                ColorRowGrid(ViewCumul, "[Période]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
                ColorRowGridAnal(ViewCumul, "[Période]='" & ID & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub LgListCumul_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LgListCumul.MouseUp
        Try
            If (ViewCumul.RowCount > 0) Then
                drx = ViewCumul.GetDataRow(ViewCumul.FocusedRowHandle)
                Dim ID = drx(0).ToString
                ColorRowGrid(ViewCumul, "[Période]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
                ColorRowGridAnal(ViewCumul, "[Période]='" & ID & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Combserv_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Combserv.SelectedIndexChanged
        Try

            query = "select f.LibelleFonction from t_service s,t_fonction f where s.codeservice=f.codeservice and s.NomService='" & EnleverApost(Combserv.Text) & "'"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rwx As DataRow In dt.Rows
                Txtfonct.Text = MettreApost(rwx(0).ToString)
            Next

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Txtemail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txtemail.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter

                dtcontact.Columns.Add("Nom & Prénoms", Type.GetType("System.String"))
                dtcontact.Columns.Add("Service", Type.GetType("System.String"))
                dtcontact.Columns.Add("Fonction", Type.GetType("System.String"))
                dtcontact.Columns.Add("Téléphone", Type.GetType("System.String"))
                dtcontact.Columns.Add("Portable", Type.GetType("System.String"))
                dtcontact.Columns.Add("Fax", Type.GetType("System.String"))
                dtcontact.Columns.Add("Email", Type.GetType("System.String"))

                Dim drS = dtcontact.NewRow()
                drS(0) = Txtnomc.Text & " " & Txtprenom.Text
                drS(1) = Combserv.Text
                drS(2) = Txtfonct.Text
                drS(3) = Txttelc.Text
                drS(4) = TxtPort.Text
                drS(5) = Txtfaxc.Text
                drS(6) = Txtemail.Text
                dtcontact.Rows.Add(drS)
                LgListContact.DataSource = dtcontact

                ViewContact.OptionsView.ColumnAutoWidth = True
                ViewContact.OptionsBehavior.AutoExpandAllGroups = True
                ViewContact.VertScrollVisibility = True
                ViewContact.HorzScrollVisibility = True
                ViewContact.BestFitColumns()

                EffacerTexBox10(GroupBox4)
                EffacerTexBox4(PanelControl4)
            Case Else
        End Select
    End Sub

    Private Sub Txtfaxc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txtfaxc.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter

                dtcontact.Columns.Add("Nom & Prénoms", Type.GetType("System.String"))
                dtcontact.Columns.Add("Service", Type.GetType("System.String"))
                dtcontact.Columns.Add("Fonction", Type.GetType("System.String"))
                dtcontact.Columns.Add("Téléphone", Type.GetType("System.String"))
                dtcontact.Columns.Add("Portable", Type.GetType("System.String"))
                dtcontact.Columns.Add("Fax", Type.GetType("System.String"))
                dtcontact.Columns.Add("Email", Type.GetType("System.String"))

                Dim drS = dtcontact.NewRow()
                drS(0) = Txtnomc.Text & " " & Txtprenom.Text
                drS(1) = Combserv.Text
                drS(2) = Txtfonct.Text
                drS(3) = Txttelc.Text
                drS(4) = TxtPort.Text
                drS(5) = Txtfaxc.Text
                drS(6) = Txtemail.Text
                dtcontact.Rows.Add(drS)
                LgListContact.DataSource = dtcontact

                ViewContact.OptionsView.ColumnAutoWidth = True
                ViewContact.OptionsBehavior.AutoExpandAllGroups = True
                ViewContact.VertScrollVisibility = True
                ViewContact.HorzScrollVisibility = True
                ViewContact.BestFitColumns()

                EffacerTexBox10(GroupBox4)
                EffacerTexBox4(PanelControl4)
            Case Else
        End Select
    End Sub

    Private Sub TxtPort_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPort.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter

                dtcontact.Columns.Add("Nom & Prénoms", Type.GetType("System.String"))
                dtcontact.Columns.Add("Service", Type.GetType("System.String"))
                dtcontact.Columns.Add("Fonction", Type.GetType("System.String"))
                dtcontact.Columns.Add("Téléphone", Type.GetType("System.String"))
                dtcontact.Columns.Add("Portable", Type.GetType("System.String"))
                dtcontact.Columns.Add("Fax", Type.GetType("System.String"))
                dtcontact.Columns.Add("Email", Type.GetType("System.String"))

                Dim drS = dtcontact.NewRow()
                drS(0) = Txtnomc.Text & " " & Txtprenom.Text
                drS(1) = Combserv.Text
                drS(2) = Txtfonct.Text
                drS(3) = Txttelc.Text
                drS(4) = TxtPort.Text
                drS(5) = Txtfaxc.Text
                drS(6) = Txtemail.Text
                dtcontact.Rows.Add(drS)
                LgListContact.DataSource = dtcontact

                ViewContact.OptionsView.ColumnAutoWidth = True
                ViewContact.OptionsBehavior.AutoExpandAllGroups = True
                ViewContact.VertScrollVisibility = True
                ViewContact.HorzScrollVisibility = True
                ViewContact.BestFitColumns()

                EffacerTexBox10(GroupBox4)
                EffacerTexBox4(PanelControl4)
            Case Else
        End Select
    End Sub

    Private Sub Txttelc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txttelc.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter

                dtcontact.Columns.Add("Nom & Prénoms", Type.GetType("System.String"))
                dtcontact.Columns.Add("Service", Type.GetType("System.String"))
                dtcontact.Columns.Add("Fonction", Type.GetType("System.String"))
                dtcontact.Columns.Add("Téléphone", Type.GetType("System.String"))
                dtcontact.Columns.Add("Portable", Type.GetType("System.String"))
                dtcontact.Columns.Add("Fax", Type.GetType("System.String"))
                dtcontact.Columns.Add("Email", Type.GetType("System.String"))

                Dim drS = dtcontact.NewRow()
                drS(0) = Txtnomc.Text & " " & Txtprenom.Text
                drS(1) = Combserv.Text
                drS(2) = Txtfonct.Text
                drS(3) = Txttelc.Text
                drS(4) = TxtPort.Text
                drS(5) = Txtfaxc.Text
                drS(6) = Txtemail.Text
                dtcontact.Rows.Add(drS)
                LgListContact.DataSource = dtcontact

                ViewContact.OptionsView.ColumnAutoWidth = True
                ViewContact.OptionsBehavior.AutoExpandAllGroups = True
                ViewContact.VertScrollVisibility = True
                ViewContact.HorzScrollVisibility = True
                ViewContact.BestFitColumns()

                EffacerTexBox10(GroupBox4)
                EffacerTexBox4(PanelControl4)
            Case Else
        End Select
    End Sub

    Private Sub Combville_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles Combville.SelectedIndexChanged
        Try

            query = "select codezone from t_zonegeo where libellezone='" & EnleverApost(Combville.Text) & "'"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rwx As DataRow In dt.Rows
                id_zone = rwx(0).ToString
            Next

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub SupprimerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SupprimerToolStripMenuItem.Click
        Try
            If ViewContact.RowCount > 0 Then
                drx = ViewContact.GetDataRow(ViewContact.FocusedRowHandle)
                ViewContact.GetDataRow(ViewContact.FocusedRowHandle).Delete()
            End If

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Combtct_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbType.SelectedIndexChanged
        'Try

        '   query= "select * from T_COMP_TYPE_COMPTE where LIBELLE_TCPT ='" & EnleverApost(cmbType.Text) & "'"
        '    Dim dt = ExcecuteSelectQuery(query)
        '    For Each rwx As DataRow In dt.Rows
        '        idtcpt = rwx(0).ToString
        '    Next

        'Catch ex As Exception
        '    FailMsg("Erreur : Information non disponible : " & ex.ToString())
        'End Try
    End Sub

    Private Sub Combville_TextChanged(sender As Object, e As System.EventArgs) Handles Combville.TextChanged
        Try

           query= "select codezone from t_zonegeo where libellezone='" & EnleverApost(Combville.Text) & "'"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rwx As DataRow In dt.Rows
                id_zone = rwx(0).ToString
            Next

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Combtct_TextChanged(sender As Object, e As System.EventArgs) Handles cmbType.TextChanged
        Try

            query = "select * from T_COMP_TYPE_COMPTE where LIBELLE_TCPT ='" & EnleverApost(cmbType.Text) & "'"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rwx As DataRow In dt.Rows
                idtcpt = rwx(0).ToString
            Next

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub
    Private Sub LgListBanque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LgListBanque.Click
        Try
            If (ViewBanques.RowCount > 0) Then
                drx = ViewBanques.GetDataRow(ViewBanques.FocusedRowHandle)
                Dim ID = drx("Numéro").ToString
                ColorRowGrid(ViewBanques, "[Numéro]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
                ColorRowGridAnal(ViewBanques, "[Numéro]='" & ID & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub
    Private Sub cmbTypeBanque_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTypeBanque.SelectedIndexChanged
        If cmbTypeBanque.Text = "AUTRE" Then
            txtAutreBanque.Visible = True
            txtNumCompte.Location = New Point(247, 116)
            txtNumCompte.Size = New Size(397, 20)
        Else
            txtNumCompte.Location = New Point(112, 116)
            txtNumCompte.Size = New Size(532, 20)
            txtAutreBanque.Visible = True
        End If
    End Sub
    Private Sub txtAutreBanque_VisibleChanged(sender As Object, e As EventArgs) Handles txtAutreBanque.VisibleChanged
        If txtAutreBanque.Visible = True Then
            txtAutreBanque.TabStop = False
        Else
            txtAutreBanque.TabStop = True
        End If
    End Sub
    Private Sub ViderSaisies()
        txtSigleBanque.ResetText()
        txtLibelleBanque.ResetText()
        cmbTypeBanque.ResetText()
        cmbDeviseBanque.ResetText()
        txtAutreBanque.ResetText()
        txtNumCompte.ResetText()
        txtIntituleCompte.ResetText()
        txtSWIFT.ResetText()
        txtAdresseBanque.ResetText()
        cmbLocaliteBanque.ResetText()
    End Sub
    Private Sub BtEnregBanque_Click(sender As Object, e As EventArgs) Handles BtEnregBanque.Click
        If txtSigleBanque.IsRequiredControl("Veuillez saisir le sigle de la banque") Then
            Exit Sub
        End If
        If txtLibelleBanque.IsRequiredControl("Veuillez saisir le nom de la banque") Then
            Exit Sub
        End If
        If txtSWIFT.IsRequiredControl("Veuillez saisir le code SWIFT de la banque") Then
            Exit Sub
        End If
        If cmbTypeBanque.IsRequiredControl("Veuillez sélectionner le type de compte") Then
            Exit Sub
        End If
        If cmbDeviseBanque.IsRequiredControl("Veuillez sélectionner une dévise dans la liste") Then
            Exit Sub
        End If
        If cmbTypeBanque.Text.ToLower() = "autre" Then
            If txtAutreBanque.IsRequiredControl("Veuillez saisir le type de compte") Then
                Exit Sub
            End If
        End If
        If txtNumCompte.IsRequiredControl("Veuillez saisir le numéro du compte") Then
            Exit Sub
        End If

        Dim EditRowIndex As Integer = IsGridEditMod(ViewBanques, "Edit")
        If EditRowIndex = -1 Then
            'query = "SELECT CODE_CPT FROM t_comp_banque WHERE RIBBANQ='" & txtNumCompte.Text.Trim() & "'"
            'Dim CodeCPT As String = ExecuteScallar(query)
            'If CodeCPT <> String.Empty Then
            '    SuccesMsg("Le compte " & txtNumCompte.Text.Trim() & " est déjà enregistré sur le tiers " & CodeCPT)
            '    txtNumCompte.Select()
            '    Exit Sub
            'End If

            If IsSavedItemInGridView(txtNumCompte.Text.Trim(), ViewBanques, "Numéro") <> -1 Then
                SuccesMsg("Le compte " & txtNumCompte.Text.Trim() & " a déjà été ajouté")
                txtNumCompte.Select()
                Exit Sub
            End If
            Dim drS = dtBanque.NewRow()
            drS("Id") = "##"
            drS("Banque") = txtLibelleBanque.Text.Trim() & " (" & txtSigleBanque.Text.Trim() & ")"
            If cmbTypeBanque.Text.ToLower() = "autre" Then
                drS("Type") = txtAutreBanque.Text.Trim()
            Else
                drS("Type") = cmbTypeBanque.Text
            End If
            drS("SWIFT") = txtSWIFT.Text.Trim()
            drS("Intitulé") = txtIntituleCompte.Text.Trim()
            drS("Adresse") = txtAdresseBanque.Text.Trim()
            drS("Localité") = cmbLocaliteBanque.Text.Trim()
            drS("Numéro") = txtNumCompte.Text.Trim()
            drS("Devise") = cmbDeviseBanque.Text
            drS("Sigle") = txtSigleBanque.Text
            drS("Edit") = False
            dtBanque.Rows.Add(drS)
        Else
            'query = "SELECT CODE_CPT FROM t_comp_banque WHERE RIBBANQ='" & txtNumCompte.Text.Trim() & "'"
            'Dim CodeCPT As String = ExecuteScallar(query)
            'If CodeCPT <> String.Empty Then
            '    If CodeCPT.MettreApostrophe() <> txtCodeTiers.Text.ToUpper Then
            '        SuccesMsg("Le compte " & txtNumCompte.Text.Trim() & " est déjà enregistré sur le tiers " & CodeCPT)
            '        txtNumCompte.Select()
            '        Exit Sub
            '    End If
            'End If

            ViewBanques.SetRowCellValue(EditRowIndex, "Banque", txtLibelleBanque.Text.Trim() & " (" & txtSigleBanque.Text.Trim() & ")")
            If cmbTypeBanque.Text.ToLower() = "autre" Then
                ViewBanques.SetRowCellValue(EditRowIndex, "Type", txtAutreBanque.Text.Trim())
            Else
                ViewBanques.SetRowCellValue(EditRowIndex, "Type", cmbTypeBanque.Text)
            End If
            ViewBanques.SetRowCellValue(EditRowIndex, "Devise", cmbDeviseBanque.Text)
            ViewBanques.SetRowCellValue(EditRowIndex, "Sigle", txtSigleBanque.Text)
            ViewBanques.SetRowCellValue(EditRowIndex, "Numéro", txtNumCompte.Text.Trim())
            ViewBanques.SetRowCellValue(EditRowIndex, "SWIFT", txtSWIFT.Text.Trim())
            ViewBanques.SetRowCellValue(EditRowIndex, "Intitulé", txtIntituleCompte.Text.Trim())
            ViewBanques.SetRowCellValue(EditRowIndex, "Adresse", txtAdresseBanque.Text.Trim())
            ViewBanques.SetRowCellValue(EditRowIndex, "Localité", cmbLocaliteBanque.Text.Trim())
            ViewBanques.SetRowCellValue(EditRowIndex, "Edit", False)
        End If
        ViderSaisies()
        txtSigleBanque.Select()
    End Sub

    Private Sub btRetourBanque_Click(sender As Object, e As EventArgs) Handles btRetourBanque.Click
        ViderSaisies()
        CancelGridEditMode(ViewBanques, "Edit")
    End Sub
    Private Sub ContextMenuBanque_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuBanque.Opening
        If ViewBanques.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub ModifierToolStripBanque_Click(sender As Object, e As EventArgs) Handles ModifierToolStripBanque.Click
        If ViewBanques.RowCount > 0 Then
            If IsGridEditMod(ViewBanques, "Edit") <> -1 Then
                SuccesMsg("Une ligne est déjà en modification.")
                Exit Sub
            End If
            Dim drx As DataRow = ViewBanques.GetDataRow(ViewBanques.FocusedRowHandle)
            Dim Sigle As String = drx("Sigle").ToString()
            Dim Type As String = drx("Type").ToString()
            If Type <> "LOCALE" And Type <> "BBAN" And Type <> "IBAN" Then
                cmbTypeBanque.Text = "AUTRE"
                txtAutreBanque.Text = Type
            Else
                cmbTypeBanque.Text = Type
                txtAutreBanque.ResetText()
            End If
            txtSigleBanque.Text = Sigle
            txtLibelleBanque.Text = drx("Banque").ToString().Replace(" (" & Sigle & ")", "")
            cmbDeviseBanque.Text = drx("Devise").ToString()
            txtNumCompte.Text = drx("Numéro").ToString()
            txtIntituleCompte.Text = drx("Intitulé")
            cmbLocaliteBanque.Text = drx("Localité")
            txtSWIFT.Text = drx("SWIFT")
            txtAdresseBanque.Text = drx("Adresse")
            ViewBanques.SetFocusedRowCellValue("Edit", True)
            txtSigleBanque.Focus()
        End If
    End Sub

    Private Sub SupprimerToolStripBanque_Click(sender As Object, e As EventArgs) Handles SupprimerToolStripBanque.Click
        Try
            If ViewBanques.RowCount > 0 Then
                If ConfirmMsg("Voulez-vous supprimer l'élément sélectionné?") = DialogResult.Yes Then
                    ViewBanques.GetDataRow(ViewBanques.FocusedRowHandle).Delete()
                End If
            End If

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub txtSigleBanque_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSigleBanque.KeyDown, txtNumCompte.KeyDown, txtLibelleBanque.KeyDown, cmbTypeBanque.KeyDown, cmbDeviseBanque.KeyDown, txtSWIFT.KeyDown, txtIntituleCompte.KeyDown, txtAdresseBanque.KeyDown, cmbLocaliteBanque.KeyDown
        If e.KeyCode = Keys.Enter Then
            BtEnregBanque.PerformClick()
        End If
    End Sub
    Private Sub ParDefautToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ParDefautToolStripMenuItem.Click
        If ViewBanques.RowCount > 0 Then
            If ViewBanques.RowCount > 0 Then
                Dim currentindex As Integer = ViewBanques.FocusedRowHandle
                Dim rw As DataRow = ViewBanques.GetDataRow(currentindex)
                If rw("Commentaire").ToString() = "Par défaut" Then
                    Exit Sub
                End If

                For i = 0 To ViewBanques.RowCount - 1
                    If ViewBanques.GetRowCellValue(i, "Commentaire").ToString() = "Par défaut" Then
                        ViewBanques.SetRowCellValue(i, "Commentaire", "")
                    End If
                Next
                ViewBanques.SetRowCellValue(currentindex, "Commentaire", "Par défaut")
            End If
        End If
    End Sub

    Private Sub CorrespondantBancaireToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CorrespondantBancaireToolStripMenuItem.Click
        If ViewBanques.RowCount > 0 Then
            Dim currentindex As Integer = ViewBanques.FocusedRowHandle
            Dim rw As DataRow = ViewBanques.GetDataRow(currentindex)
            If rw("Commentaire").ToString() = "Correspondant bancaire" Then
                Exit Sub
            End If

            For i = 0 To ViewBanques.RowCount - 1
                If ViewBanques.GetRowCellValue(i, "Commentaire").ToString() = "Correspondant bancaire" Then
                    ViewBanques.SetRowCellValue(i, "Commentaire", "")
                End If
            Next
            ViewBanques.SetRowCellValue(currentindex, "Commentaire", "Correspondant bancaire")
        End If
    End Sub

    Private Sub Modif_PlanTiers_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        cmbTypeBanque_SelectedIndexChanged(cmbTypeBanque, New EventArgs)
    End Sub
End Class