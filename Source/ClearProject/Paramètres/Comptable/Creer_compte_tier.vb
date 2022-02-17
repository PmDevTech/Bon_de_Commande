Public Class Creer_compte_tier

    Dim dtCompteTier As New DataTable()
    Dim dtcontact As New DataTable()
    Dim dtBanque As New DataTable
    Dim idtcpt As String
    Dim idcodezoNe As Integer = 0
    Private Class1 As New TiersClass

    Private Sub btnewj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnewj.Click
        ActiverChamps4(PanelControl5)
        ActiverChamps4(PanelControl4)
        ActiverChamps5(GroupControl2)
        ActiverChamps10(GroupBox1)
        ActiverChamps5(gcSaisieBanque)
        ActiverChamps10(GroupBox3)
        ActiverChamps10(GroupBox4)
        txtCodeTiers.Enabled = False
        cmbType.Select()
    End Sub
    Private Sub Creer_compte_tier_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        EffacerTexBox10(GroupBox1)
        'EffacerTexBox2(gcSaisieBanque)
        EffacerTexBox10(GroupBox3)
        EffacerTexBox10(GroupBox4)
        EffacerTexBox2(GroupControl2)
        EffacerTexBox4(PanelControl5)
        EffacerTexBox4(PanelControl4)
        dtBanque.Rows.Clear()
        dtcontact.Rows.Clear()
    End Sub
    Private Sub Creer_compte_tier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        Try

            EffacerTexBox10(GroupBox1)
            'EffacerTexBox2(gcSaisieBanque)
            EffacerTexBox2(GroupControl2)
            EffacerTexBox4(PanelControl5)
            dtBanque.Rows.Clear()

            'remplir le type compte
            cmbType.Properties.Items.Clear()
            query = "select * from T_COMP_TYPE_COMPTE ORDER BY Code_CL"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cmbType.Properties.Items.Add(rw("Code_CL") & " | " & MettreApost(rw("libelle_tcpt")).ToString)
            Next

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
            Dim dt1 = ExcecuteSelectQuery(query)
            For Each rw1 As DataRow In dt1.Rows
                Combsc.Properties.Items.Add(rw1(0).ToString & "   " & MettreApost(rw1(2).ToString))
            Next

            dtcontact.Columns.Add("Nom & Prénoms", Type.GetType("System.String"))
            dtcontact.Columns.Add("Service", Type.GetType("System.String"))
            dtcontact.Columns.Add("Fonction", Type.GetType("System.String"))
            dtcontact.Columns.Add("Téléphone", Type.GetType("System.String"))
            dtcontact.Columns.Add("Portable", Type.GetType("System.String"))
            dtcontact.Columns.Add("Fax", Type.GetType("System.String"))
            dtcontact.Columns.Add("Email", Type.GetType("System.String"))
            LgListContact.DataSource = dtcontact

            ViewContact.OptionsView.ColumnAutoWidth = True
            ViewContact.OptionsBehavior.AutoExpandAllGroups = True
            ViewContact.VertScrollVisibility = True
            ViewContact.HorzScrollVisibility = True
            ViewContact.BestFitColumns()

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
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub btenregisterj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btenregisterj.Click
        Try
            'vérification des champ text
            Dim erreur As String = ""
            Dim sc() As String
            sc = Combsc.Text.Split("   ")

            If txtCodeTiers.Text.ToUpper() = "" Then
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

                'date
                Dim datedebut As String = ExerciceComptable.Rows(0).Item("datedebut")

                'Conversion date
                Dim tempdt1 As String = dateconvert(datedebut.ToString)

                query = "select CODE_CPT from T_COMP_COMPTE where code_cpt='" & txtCodeTiers.Text.ToUpper() & "' and code_projet='" & ProjetEnCours & "'"
                Dim dt1 = ExcecuteSelectQuery(query)
                If dt1.Rows.Count > 0 Then
                    SuccesMsg("Le compte existe déjà.")
                Else

                    'insertion compte de tiers
                    query = "insert into T_COMP_COMPTE values('" & EnleverApost(txtCodeTiers.Text.ToUpper()) & "','" & EnleverApost(Txtabr.Text) & "','" & idtcpt.ToString & "','" & Combville.Text.EnleverApostrophe & "','" & EnleverApost(txtIntitule.Text) & "','" & EnleverApost(txtadresse.Text) & "','" & txtcontact.Text & "','" & EnleverApost(txtmail.Text) & "','" & ProjetEnCours & "','" & EnleverApost(Txtrc.Text) & "','" & EnleverApost(Txtcc.Text) & "','" & Txttel.Text & "','" & Txtfax.Text & "','" & EnleverApost(Txtsite.Text) & "','" & EnleverApost(txtqualite.Text) & "')"
                    ExecuteNonQuery(query)

                    'On insert le nouveau compte de tiers dans la table de report de tous les exercices
                    query = "SELECT * FROM t_comp_exercice"
                    Dim dtExercice As DataTable = ExcecuteSelectQuery(query)
                    For Each rwExo In dtExercice.Rows
                        query = "SELECT * FROM report_cpt WHERE CODE_CPT='" & txtCodeTiers.Text.ToUpper() & "' AND DATE_LE='" & dateconvert(CDate(rwExo("datedebut"))) & "'"
                        Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
                        If dtVerif.Rows.Count = 0 Then
                            query = "insert into report_cpt values (NULL, '" & txtCodeTiers.Text.ToUpper() & "','0', '0','0','0','" & dateconvert(CDate(rwExo("datedebut"))) & "')"
                            ExecuteNonQuery(query)
                        End If
                    Next

                    'insertion rattaché compte tiers à compte selectif
                    'query = "insert into T_COMP_RATTACH_TIERS values('" & sc(0).ToString & "','" & Txtabr.Text & "','" & idtcpt.ToString & "','" & ProjetEnCours & "','" & txtcpt.Text & "')"
                    'ExecuteNonQuery(query)

                    'insertion de la banque et leur rattache
                    If PageBanque.PageVisible Then
                        For i = 0 To ViewBanques.RowCount - 1
                            'insertion dans la table banque
                            Dim rw As DataRow = dtBanque.Rows(i)
                            Dim Type As String = String.Empty
                            If rw("Commentaire").ToString() = "Par défaut" Then
                                Type = "Default"
                            ElseIf rw("Commentaire").ToString() = "Correspondant bancaire" Then
                                Type = "Corresp"
                            End If
                            query = "insert into T_COMP_BANQUE values (NULL,'" & rw("Sigle").ToString().EnleverApostrophe() & "','" & rw("Banque").ToString().Replace(" (" & rw("Sigle").ToString() & ")", "").EnleverApostrophe() & "','" & rw("Numéro").ToString().EnleverApostrophe() & "','" & rw("SWIFT").ToString.EnleverApostrophe() & "','" & rw("Intitulé").ToString.EnleverApostrophe() & "','" & rw("Adresse").ToString.EnleverApostrophe() & "','" & rw("Localité").ToString.EnleverApostrophe() & "','" & txtCodeTiers.Text.EnleverApostrophe.ToUpper & "','" & rw("Devise").ToString() & "','" & rw("Type").ToString() & "','" & Type.EnleverApostrophe() & "','" & ProjetEnCours & "')"
                            ExecuteNonQuery(query)
                        Next
                    End If

                    If PageContact.PageVisible Then
                        For i = 0 To ViewContact.RowCount - 1
                            'insertion dans la table banque
                            query = "insert into T_COMP_IDENTIFIANT values (NULL,'" & EnleverApost(txtCodeTiers.Text.ToUpper()) & "','" & EnleverApost(dtcontact.Rows(i).Item(0).ToString) & "','" & EnleverApost(dtcontact.Rows(i).Item(2).ToString) & "','" & EnleverApost(dtcontact.Rows(i).Item(4).ToString) & "','" & EnleverApost(dtcontact.Rows(i).Item(5).ToString) & "','" & EnleverApost(dtcontact.Rows(i).Item(6).ToString) & "','" & ProjetEnCours & "','" & EnleverApost(dtcontact.Rows(i).Item(3).ToString) & "','" & EnleverApost(dtcontact.Rows(i).Item(1).ToString) & "')"
                            ExecuteNonQuery(query)
                        Next
                    End If

                    txtCodeTiers.Focus()
                    EffacerTexBox10(GroupBox1)
                    'EffacerTexBox2(gcSaisieBanque)
                    EffacerTexBox10(GroupBox3)
                    EffacerTexBox10(GroupBox4)
                    EffacerTexBox2(GroupControl2)
                    EffacerTexBox4(PanelControl5)
                    EffacerTexBox4(PanelControl4)
                    dtBanque.Rows.Clear()
                    dtcontact.Rows.Clear()

                    Plan_tiers.CmbPageSize_SelectedIndexChanged(sender, e)

                    SuccesMsg("Enregistrement effectué avec succès.")
                End If
            Else
                AlertMsg("Veuillez remplir ces champs : " & ControlChars.CrLf + erreur)
            End If

        Catch ex As Exception
            SuccesMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    'Private Sub RemplirFRS()

    '    Saisie_engagement.TxtFournisMarche.Properties.Items.Clear()
    '    Modif_engagement.TxtFournisMarche.Properties.Items.Clear()
    '    query = "select * from T_COMP_COMPTE where Code_Projet='" & ProjetEnCours & "' order by code_cpt"
    '    Dim dt As DataTable = ExcecuteSelectQuery(query)
    '    For Each rw As DataRow In dt.Rows
    '        Saisie_engagement.TxtFournisMarche.Properties.Items.Add(rw(0).ToString & " | " & MettreApost(rw(4).ToString))
    '        Modif_engagement.TxtFournisMarche.Properties.Items.Add(rw(0).ToString & " | " & MettreApost(rw(4).ToString))
    '    Next

    'End Sub

    Private Sub btannulerj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btannulerj.Click
        EffacerTexBox10(GroupBox1)
        'EffacerTexBox2(gcSaisieBanque)
        EffacerTexBox10(GroupBox3)
        EffacerTexBox10(GroupBox4)
        EffacerTexBox2(GroupControl2)
        EffacerTexBox4(PanelControl5)
        EffacerTexBox4(PanelControl4)
        dtBanque.Rows.Clear()
        txtCodeTiers.Focus()
    End Sub

    Private Sub txtnom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIntitule.TextChanged
        txtCodeTiers.Text = GenreateNumTiers()
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

    Private Sub txtCodeTiers_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodeTiers.TextChanged
        txtCodeTiers.Text = GenreateNumTiers()
        txtCodeTiers.Properties.CharacterCasing = CharacterCasing.Upper
        'If cmbType.Text = "Fournisseurs" Then

        '    query = "select t_fournisseur.NomFournis,t_fournisseur.AdresseCompleteFournis,t_fournisseur.MailFournis,t_fournisseur.TelFournis,t_fournisseur.PaysFournis from t_fournisseur,T_SoumissionFournisseur where T_SoumissionFournisseur.codefournis=t_fournisseur.codefournis and t_fournisseur.CodeFournis='" & txtCodeTiers.Text.ToUpper() & "' and t_fournisseur.codeprojet='" & ProjetEnCours & "' and T_SoumissionFournisseur.attribue='oui'"
        '    Dim dt = ExcecuteSelectQuery(query)
        '    For Each rwx As DataRow In dt.Rows
        '        txtIntitule.Text = rwx(0).ToString
        '        txtadresse.Text = rwx(1).ToString
        '        txtmail.Text = rwx(2).ToString
        '        txtcontact.Text = rwx(3).ToString
        '        Combville.Text = rwx(4).ToString
        '    Next

        'ElseIf cmbType.Text = "Consultants" Then

        '    query = "select T_Consultant.NomConsult,T_Consultant.AdressConsult,T_Consultant.EmailConsult,T_Consultant.TelConsult,T_Consultant.PaysConsult from T_Consultant,T_SoumissionConsultant where T_SoumissionConsultant.RefConsult=T_Consultant.RefConsult and T_SoumissionConsultant.attribue='oui' and T_Consultant.RefConsult='" & txtCodeTiers.Text.ToUpper() & "'"
        '    Dim dt = ExcecuteSelectQuery(query)
        '    For Each rwx As DataRow In dt.Rows
        '        txtIntitule.Text = rwx(0).ToString
        '        txtadresse.Text = rwx(1).ToString
        '        txtmail.Text = rwx(2).ToString
        '        txtcontact.Text = rwx(3).ToString
        '        Combville.Text = rwx(4).ToString
        '    Next

        'End If

    End Sub

    Private Sub cmbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbType.SelectedIndexChanged
        Try
            txtCodeTiers.Text = GenreateNumTiers()
            If cmbType.SelectedIndex <> -1 Then
                idtcpt = ExecuteScallar("SELECT CODE_TCPT FROM t_comp_type_compte WHERE Code_CL='" & cmbType.Text.Split(" | ")(0) & "'")
                'query = "select * from T_COMP_TYPE_COMPTE where LIBELLE_TCPT ='" & EnleverApost(cmbType.Text) & "'"
                'Dim dt = ExcecuteSelectQuery(query)
                'For Each rwx As DataRow In dt.Rows
                'Next
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub cmbTypeBanque_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTypeBanque.SelectedIndexChanged
        If cmbTypeBanque.Text = "AUTRE" Then
            txtAutreBanque.Visible = True
            txtNumCompte.Location = New Point(239, 115)
            txtNumCompte.Size = New Size(397, 20)
        Else
            txtNumCompte.Location = New Point(104, 115)
            txtNumCompte.Size = New Size(532, 20)
            txtAutreBanque.Visible = True
        End If
    End Sub

    Private Sub Combserv_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Combserv.SelectedIndexChanged
        Try

            query = "select f.LibelleFonction from t_service s,t_fonction f where s.codeservice=f.codeservice and s.NomService='" & EnleverApost(Combserv.Text) & "'"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rwx As DataRow In dt.Rows
                Txtfonct.Properties.Items.Add(MettreApost(rwx(0).ToString))
            Next

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Combville_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Combville.SelectedIndexChanged
        Try

            query = "select codezone from t_zonegeo where libellezone='" & EnleverApost(Combville.Text) & "'"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rwx As DataRow In dt.Rows
                idcodezoNe = rwx(0).ToString
            Next

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Txttelc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txtemail.KeyDown, Txtfaxc.KeyDown, Txtprenom.KeyDown, TxtPort.KeyDown, Txttelc.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                Dim drS = dtcontact.NewRow()
                drS(0) = Txtnomc.Text & " " & Txtprenom.Text
                drS(1) = Combserv.Text
                drS(2) = Txtfonct.Text
                drS(3) = Txttelc.Text
                drS(4) = TxtPort.Text
                drS(5) = Txtfaxc.Text
                drS(6) = Txtemail.Text
                dtcontact.Rows.Add(drS)
                'LgListContact.DataSource = dtcontact

                ViewContact.OptionsView.ColumnAutoWidth = True

                EffacerTexBox10(GroupBox4)
                EffacerTexBox4(PanelControl4)
            Case Else
        End Select
    End Sub

    Private Sub LgListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LgListContact.Click
        Try
            If (ViewContact.RowCount > 0) Then
                drx = ViewContact.GetDataRow(ViewContact.FocusedRowHandle)
                Dim ID = drx(6).ToString
                ColorRowGrid(ViewContact, "[Email]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
                ColorRowGridAnal(ViewContact, "[Email]='" & ID & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
            End If
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

    Private Sub SupprimerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupprimerToolStripMenuItem.Click
        Try
            If ViewContact.RowCount > 0 Then
                drx = ViewContact.GetDataRow(ViewContact.FocusedRowHandle)
                ViewContact.GetDataRow(ViewContact.FocusedRowHandle).Delete()
            End If

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub

    Private Function GenreateNumTiers() As String
        If cmbType.SelectedIndex <> -1 Then
            If txtIntitule.Text.Trim().Length > 0 And txtIntitule.Text.Trim().Length >= 3 Then
                Dim type As String = cmbType.Text.Split(" | ")(0)
                Dim Code As String = ""
                Dim cpte As Decimal = 0
                For i = 1 To txtIntitule.Text.Length
                    If Char.IsLetter(Mid(txtIntitule.Text, i, 1)) Or Char.IsNumber(Mid(txtIntitule.Text, i, 1)) Then
                        Code &= Mid(txtIntitule.Text, i, 1)
                        cpte += 1
                        If cpte = 3 Then
                            Exit For
                        End If
                    End If
                Next
                'For i = 1 To Code.Length
                '    If Char.IsLetter(Mid(Code, 1, 1)) Then
                '        cpte += 1
                '    End If
                'Next
                If cpte = 3 Then
                    Dim disponible As Boolean = False  'Permettra de verifier la disponibilite du code
                    Dim decal As String = "000"
                    Dim NewTiers As String = type & Code & decal
                    While Not disponible
                        query = "SELECT CODE_CPT FROM t_comp_compte WHERE CODE_CPT='" & NewTiers & "'"
                        Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
                        If dtVerif.Rows.Count = 0 Then
                            disponible = True
                        Else
                            Dim newDecal As String = (Val(decal) + 1).ToString()
                            If newDecal.Length = 1 Then
                                decal = "00" & newDecal
                            ElseIf decal.Length = 2 Then
                                decal = "0" & newDecal
                            ElseIf decal.Length = 3 Then
                                decal = newDecal
                            Else
                                Exit While
                            End If
                            NewTiers = type & Code & decal
                        End If
                    End While
                    Return NewTiers
                End If
            End If
        End If
        Return String.Empty
    End Function

    Private Sub btNewType_Click(sender As Object, e As EventArgs) Handles btNewType.Click
        Dim NewType As New TypeTiers
        Dialog_form(NewType)
        Dim OldText As String = cmbType.Text
        'remplir le type compte
        cmbType.Properties.Items.Clear()
        query = "select * from T_COMP_TYPE_COMPTE ORDER BY Code_CL"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cmbType.Properties.Items.Add(rw("Code_CL") & " | " & MettreApost(rw("libelle_tcpt")).ToString)
        Next
        cmbType.Text = OldText
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
    Private Sub btRetourBanque_Click(sender As Object, e As EventArgs) Handles btRetourBanque.Click
        ViderSaisies()
        CancelGridEditMode(ViewBanques, "Edit")
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
            '    SuccesMsg("Le compte " & txtNumCompte.Text.Trim() & " est déjà enregistré sur le tiers " & CodeCPT)
            '    txtNumCompte.Select()
            '    Exit Sub
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
                For i = 0 To ViewBanques.RowCount - 1
                    If ViewBanques.GetRowCellValue(i, "Commentaire").ToString() = "Par défaut" Then
                        ViewBanques.SetRowCellValue(i, "Commentaire", "")
                    End If
                Next
                ViewBanques.SetRowCellValue(ViewBanques.FocusedRowHandle, "Commentaire", "Par défaut")
            End If
        End If
    End Sub

    Private Sub CorrespondantBancaireToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CorrespondantBancaireToolStripMenuItem.Click
        If ViewBanques.RowCount > 0 Then
            For i = 0 To ViewBanques.RowCount - 1
                If ViewBanques.GetRowCellValue(i, "Commentaire").ToString() = "Correspondant bancaire" Then
                    ViewBanques.SetRowCellValue(i, "Commentaire", "")
                End If
            Next
            ViewBanques.SetRowCellValue(ViewBanques.FocusedRowHandle, "Commentaire", "Correspondant bancaire")
        End If
    End Sub
End Class