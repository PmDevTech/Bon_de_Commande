Public Class Journal
    Dim dtjournaux = New DataTable
    Dim DrX As DataRow


    Private Sub Journal_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        btannulerj_Click(Me, e)
    End Sub

    Private Sub Journal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide 

        'remplir le tpe journal
        RemplirCombo(combtj, "T_COMP_TYPE_JOURNAL", "CODE_TJ", "LIBELLE_TJ")

        'remplir les sous classe du plan comptable
        RemplirCombosc(combsc, "T_COMP_SOUS_CLASSE", "CODE_SC", "LIBELLE_SC")

        remplirdatagridjournal()

    End Sub

    Private Sub remplirdatagridjournal()
        dtjournaux.Columns.Clear()
        dtjournaux.Columns.Add("Type", Type.GetType("System.String"))
        dtjournaux.Columns.Add("Code", Type.GetType("System.String"))
        dtjournaux.Columns.Add("Libelle", Type.GetType("System.String"))
        dtjournaux.Rows.Clear()

        query = "Select t.LIBELLE_TJ, j.CODE_J, j.LIBELLE_J from T_COMP_JOURNAL j, T_COMP_TYPE_JOURNAL t where j.code_tj=t.code_tj"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rwx As DataRow In dt.Rows

            Dim drS = dtjournaux.NewRow()
            drS(0) = rwx(0).ToString
            drS(1) = MettreApost(rwx(1).ToString)
            drS(2) = MettreApost(rwx(2).ToString)
            dtjournaux.Rows.Add(drS)

        Next
        LgListJournaux.DataSource = dtjournaux

        ViewJournaux.OptionsView.ColumnAutoWidth = True
        ViewJournaux.OptionsBehavior.AutoExpandAllGroups = True
        ViewJournaux.VertScrollVisibility = True
        ViewJournaux.HorzScrollVisibility = True

    End Sub


    Private Sub btnewj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnewj.Click
        txtcodej.Enabled = True
        txtlibj.Enabled = True
        Combjp.Enabled = True
        combtj.Enabled = True
        checkasscpt.Enabled = True
        EffacerTexBox10(GroupBox1)
        EffacerTexBox10(GroupBox2)
        txtcodej.Focus()
    End Sub

    Private Sub checkasscpt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkasscpt.CheckedChanged
        If checkasscpt.Checked = True Then
            combsc.Enabled = True
        ElseIf checkasscpt.Checked = False Then
            combsc.Enabled = False
        End If
    End Sub

    Private Sub btenregisterj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btenregisterj.Click

        Try
            'vérification des champ text
            Dim erreur As String = ""
            Dim sc() As String
            sc = combsc.Text.Split("   ")

            Dim tj() As String
            tj = combtj.Text.Split("   ")

            If Combjp.SelectedIndex = -1 Then
                erreur += "- Numérotation de la Pièce" & ControlChars.CrLf
            End If

            If combtj.SelectedIndex = -1 Then
                erreur += "- Type de Journal" & ControlChars.CrLf
            End If

            If txtcodej.Text = "" Then
                erreur += "- Code du Journal" & ControlChars.CrLf
            End If

            If checkasscpt.Checked = True Then
                If combsc.SelectedIndex = -1 Then
                    erreur += "- Compte Comptable" & ControlChars.CrLf
                End If
            End If

            If erreur = "" Then

                Dim act As String = ""
                If chkApplyActivity.Checked = True Then
                    act = "0"
                Else
                    act = "1"
                End If

                query = "select count(*) from T_COMP_JOURNAL where code_j='" & txtcodej.Text & "'"
                Dim nbre As Decimal = Val(ExecuteScallar(query))
                If nbre = 0 Then

                    If checkasscpt.Checked = True Then
                       query= "insert into T_COMP_JOURNAL values('" & txtcodej.Text & "','" & sc(0).ToString & "','" & tj(0).ToString & "','" & EnleverApost(txtlibj.Text) & "','" & Combjp.Text & "','non cloturer','" & act.ToString & "')"
                        ExecuteNonQuery(query)
                    Else
                       query= "insert into T_COMP_JOURNAL values('" & txtcodej.Text & "','','" & tj(0).ToString & "','" & EnleverApost(txtlibj.Text) & "','" & Combjp.Text & "','non cloturer','" & act.ToString & "')"
                        ExecuteNonQuery(query)
                    End If


                Else

                    If checkasscpt.Checked = True Then
                       query= "update T_COMP_JOURNAL set code_sc='" & sc(0).ToString & "', libelle_j='" & EnleverApost(txtlibj.Text) & "', numeritation_j='" & Combjp.Text & "', Code_Tj='" & tj(0).ToString & "', etat_j='" & act.ToString & "' where code_j='" & txtcodej.Text & "'"
                        ExecuteNonQuery(query)
                    Else
                       query= "update T_COMP_JOURNAL set libelle_j='" & EnleverApost(txtlibj.Text) & "', numeritation_j='" & Combjp.Text & "', Code_Tj='" & tj(0).ToString & "', etat_j='" & act.ToString & "' where code_j='" & txtcodej.Text & "'"
                        ExecuteNonQuery(query)
                    End If

                End If

                Dim currentData As String = txtcodej.Text

                'initialiser le formulaire
                EffacerTexBox10(GroupBox1)
                EffacerTexBox10(GroupBox2)
                checkasscpt.Checked = False
                txtcodej.Focus()

                'remplir datagrid
                remplirdatagridjournal()

            Else
                SuccesMsg("Veuillez remplir ces champs : " & ControlChars.CrLf + erreur)
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub

    Private Sub btsuppr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btsuppr.Click

        If (ViewJournaux.RowCount > 0) Then
            If txtcodej.Text.Trim().Length > 0 Then
                DrX = ViewJournaux.GetDataRow(ViewJournaux.FocusedRowHandle)
                'Vérification des écritures sur le journal
                query = "SELECT COUNT(*) FROM t_comp_ligne_ecriture WHERE CODE_J='" & txtcodej.Text.Trim() & "' AND (DEBIT_LE<>0 OR CREDIT_LE<>0) AND CODE_PROJET='" & ProjetEnCours & "'"
                Dim NbEcritures As Decimal = Val(ExecuteScallar(query))
                If NbEcritures > 0 Then
                    FailMsg("Vous ne pouvez pas supprimer ce journal." & vbNewLine & "Il contient des écritures.")
                    Exit Sub
                End If
                If DrX(1).ToString() = txtcodej.Text.Trim() Then
                    DeleteRecords("T_COMP_JOURNAL", "CODE_J", txtcodej.Text.Trim())
                    ViewJournaux.GetDataRow(ViewJournaux.FocusedRowHandle).Delete()
                    btannulerj_Click(Me, e)
                End If
            Else
                SuccesMsg("Veuillez sélectionner un journal")
            End If
        Else
            SuccesMsg("Veuillez sélectionner un journal")
        End If
    End Sub

    Private Sub btannulerj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btannulerj.Click
        EffacerTexBox10(GroupBox1)
        EffacerTexBox10(GroupBox2)
        DesactiverChamps2(GroupBox1)
        DesactiverChamps2(GroupBox2)
        checkasscpt.Checked = False
    End Sub

    Private Sub LgListJournaux_Click(sender As System.Object, e As System.EventArgs) Handles LgListJournaux.Click
        If (ViewJournaux.RowCount > 0) Then

            DrX = ViewJournaux.GetDataRow(ViewJournaux.FocusedRowHandle)

            txtcodej.Enabled = True
            txtlibj.Enabled = True
            Combjp.Enabled = True
            combtj.Enabled = True

            Dim IDL = DrX(1).ToString
            Dim recevtj As String = ""
            Dim recevsc As String = ""
            Dim act As Decimal = 0
            ColorRowGrid(ViewJournaux, "[Libelle]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewJournaux, "[Code]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)

            query = "select * from T_COMP_JOURNAL where code_j='" & DrX(1).ToString & "'"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rwx As DataRow In dt.Rows
                txtcodej.Text = rwx(0).ToString
                txtlibj.Text = MettreApost(rwx(3).ToString)
                Combjp.Text = rwx(4).ToString
                recevtj = rwx(2).ToString
                recevsc = rwx(1).ToString
                act = rwx(6).ToString
            Next

            If act.ToString = "1" Then
                chkApplyActivity.Checked = False
            Else
                chkApplyActivity.Checked = True
            End If

            'remplir type journal
            RemplirComboText(combtj, "T_COMP_TYPE_JOURNAL", "CODE_TJ", "LIBELLE_TJ", recevtj)

            'remplir compte comptable
            RemplirComboText(combsc, "T_COMP_SOUS_CLASSE", "CODE_SC", "LIBELLE_SC", recevsc)
        End If
    End Sub

    Private Sub combtj_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles combtj.SelectedIndexChanged
        Dim tj() As String
        tj = combtj.Text.Split("   ")

        If tj(0).ToString = "TRE" Then
            checkasscpt.Enabled = True
            checkasscpt.Checked = True
            chkApplyActivity.Enabled = True
        Else
            checkasscpt.Enabled = False
            chkApplyActivity.Enabled = True
            checkasscpt.Checked = False
            combsc.Text = ""
        End If
    End Sub
End Class