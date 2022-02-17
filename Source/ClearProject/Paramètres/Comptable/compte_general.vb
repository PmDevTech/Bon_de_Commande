Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors
Imports DevExpress.Utils

Public Class compte_general

    Dim dtcomptable = New DataTable
    Dim DrX As DataRow
    Private Class3 As New PlanComptableClass

    Private Sub compte_general_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        EffacerTexBox10(GroupBox3)
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        txtCompte.Focus()

        txtCompte.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        txtCompte.Properties.Mask.EditMask = "d"
        txtCompte.Properties.Mask.UseMaskAsDisplayFormat = True
    End Sub

    Private Sub compte_general_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        CmbPageSize.SelectedIndex = 0
        CmbPageSize.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        LoadComptes()
    End Sub
    Private Sub LoadComptes(Optional Page As Decimal = 1)
        If Page = 1 Then
            Dim nbre As Decimal = 0
            query = "select count(*) from t_comp_classe0"
            nbre += Val(ExecuteScallar(query))
            query = "select count(*) from t_comp_classe"
            nbre += Val(ExecuteScallar(query))
            query = "select count(*) from t_comp_classen1"
            nbre += Val(ExecuteScallar(query))
            query = "select count(*) from t_comp_classen2"
            nbre += Val(ExecuteScallar(query))
            query = "select count(*) from t_comp_sous_classe"
            nbre += Val(ExecuteScallar(query))

            With Class3
                .PageSize = IIf(CmbPageSize.Text = "", 1, CmbPageSize.Text)
                .PageCount = nbre \ .PageSize
                If nbre Mod .PageSize <> 0 Then
                    .PageCount += 1
                End If
                TxtPage.Text = "Page 1" & "/" & .PageCount
                .Resqlconno = 0

            End With
        End If

        With Class3
            .CurrentPage = Page
            .LoadPage(LgListComptable, .CurrentPage)
        End With
    End Sub

    Public Sub CheckEdit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Edit As CheckEdit = CType(sender, CheckEdit)
        Select Case Edit.Checked
            Case True
                DrX = ViewComptable.GetDataRow(ViewComptable.FocusedRowHandle)
                InitialiserForm()
                ActiverChamps10(GroupBox3)

                If Len(DrX("Code").ToString) = "1" Then

                    query = "select code_cl0, libelle_cl0 from t_comp_classe0 where t_comp_classe0.code_cl0='" & DrX("Code").ToString & "'"
                    Dim dt = ExcecuteSelectQuery(query)
                    For Each rwx As DataRow In dt.Rows
                        txtCompte.Text = rwx(0).ToString
                        txtLibelleCompte.Text = MettreApost(rwx(1).ToString)
                    Next

                ElseIf Len(DrX("Code").ToString) = "2" Then

                    query = "select code_cl, libelle_cl from t_comp_classe where t_comp_classe.code_cl='" & DrX("Code").ToString & "'"
                    Dim dt = ExcecuteSelectQuery(query)
                    For Each rwx As DataRow In dt.Rows
                        txtCompte.Text = rwx(0).ToString
                        txtLibelleCompte.Text = MettreApost(rwx(1).ToString)
                    Next

                ElseIf Len(DrX("Code").ToString) = "3" Then

                    query = "select code_cln1, libelle_cln1 from t_comp_classen1 where t_comp_classen1.code_cln1='" & DrX("Code").ToString & "'"
                    Dim dt = ExcecuteSelectQuery(query)
                    For Each rwx As DataRow In dt.Rows
                        txtCompte.Text = rwx(0).ToString
                        txtLibelleCompte.Text = MettreApost(rwx(1).ToString)
                    Next

                ElseIf Len(DrX("Code").ToString) = "4" Then

                    query = "select code_cln2, libelle_cln2 from t_comp_classen2 where t_comp_classen2.code_cln2='" & DrX("Code").ToString & "'"
                    Dim dt = ExcecuteSelectQuery(query)
                    For Each rwx As DataRow In dt.Rows
                        txtCompte.Text = rwx(0).ToString
                        txtLibelleCompte.Text = MettreApost(rwx(1).ToString)
                    Next

                ElseIf Len(DrX("Code").ToString) > "4" Then

                    query = "select code_cl, code_sc ,libelle_sc from t_comp_sous_classe where t_comp_sous_classe.code_sc='" & DrX("Code").ToString & "'"
                    Dim dt = ExcecuteSelectQuery(query)
                    For Each rwx As DataRow In dt.Rows
                        txtCompte.Text = rwx(1).ToString
                        txtLibelleCompte.Text = MettreApost(rwx(2).ToString)
                    Next

                End If

            Case False

                EffacerTexBox1(GroupBox3)

        End Select

    End Sub

    Private Sub insertcompte()
        Try
            'vérification des champ text
            Dim erreur As String = ""

            If txtCompte.Text = "" Or txtCompte.Text = "0" Then
                erreur += "- Code Sous-Classe" & ControlChars.CrLf
            End If

            If txtLibelleCompte.Text = "" Then
                erreur += "- Libellé Sous-Classe" & ControlChars.CrLf
            End If

            If erreur = "" Then
                'recuperer la classe
                Dim cl0 As String
                cl0 = Mid(txtCompte.Text, 1, 1)

                Dim cl As String
                cl = Mid(txtCompte.Text, 1, 2)

                Dim cl1 As String
                cl1 = Mid(txtCompte.Text, 1, 3)

                Dim cl2 As String
                cl2 = Mid(txtCompte.Text, 1, 4)

                If Len(txtCompte.Text) = 1 Then
                    '
                    query = "select * from T_COMP_CLASSE0 where CODE_CL0='" & txtCompte.Text & "'"
                    Dim dt = ExcecuteSelectQuery(query)
                    If dt.Rows.Count = 0 Then
                        'insertion de la classe niveau1
                        query = "insert into T_COMP_CLASSE0 values('" & txtCompte.Text & "','" & EnleverApost(txtLibelleCompte.Text) & "')"
                        ExecuteNonQuery(query)
                    Else
                        'modification de la classe niveau1
                        query = "Update T_COMP_CLASSE0 set LIBELLE_CL0='" & EnleverApost(txtLibelleCompte.Text) & "' where CODE_CL0='" & txtCompte.Text & "'"
                        ExecuteNonQuery(query)
                    End If

                ElseIf Len(txtCompte.Text) = 2 Then
                    query = "select * from T_COMP_CLASSE where CODE_CL='" & txtCompte.Text & "'"
                    Dim dt = ExcecuteSelectQuery(query)
                    If dt.Rows.Count = 0 Then
                        'insertion de la classe niveau1
                        query = "select * from T_COMP_CLASSE0 where CODE_CL0='" & cl0.ToString & "'"
                        Dim dt1 = ExcecuteSelectQuery(query)
                        If dt1.Rows.Count = 0 Then
                            query = "insert into T_COMP_CLASSE0 values('" & cl0 & "','')"
                            ExecuteNonQuery(query)
                        End If

                        'insertion de la classe niveau2
                        query = "insert into T_COMP_CLASSE values('" & txtCompte.Text & "','" & EnleverApost(txtLibelleCompte.Text) & "','" & cl0 & "')"
                        ExecuteNonQuery(query)
                    Else
                        'modification de la classe niveau2
                        query = "Update T_COMP_CLASSE set LIBELLE_CL='" & EnleverApost(txtLibelleCompte.Text) & "' where CODE_CL='" & txtCompte.Text & "'"
                        ExecuteNonQuery(query)
                    End If

                ElseIf Len(txtCompte.Text) = 3 Then
                    'insertion compte classe niveau3
                    query = "select * from T_COMP_CLASSEN1 where CODE_CLN1='" & cl1.ToString & "'"
                    Dim dt = ExcecuteSelectQuery(query)
                    If dt.Rows.Count = 0 Then

                        'insertion de la classe niveau1
                        query = "select * from T_COMP_CLASSE0 where CODE_CL0='" & cl0.ToString & "'"
                        Dim dt1 = ExcecuteSelectQuery(query)
                        If dt1.Rows.Count = 0 Then
                            query = "insert into T_COMP_CLASSE0 values('" & cl0 & "','')"
                            ExecuteNonQuery(query)
                        End If

                        'insertion de la classe niveau2
                        query = "select * from T_COMP_CLASSE where CODE_CL='" & cl.ToString & "'"
                        Dim dt2 = ExcecuteSelectQuery(query)
                        If dt2.Rows.Count = 0 Then
                            query = "insert into T_COMP_CLASSE values('" & cl & "','','" & cl0 & "')"
                            ExecuteNonQuery(query)
                        End If

                        'insertion de la classe niveau3
                        query = "insert into T_COMP_CLASSEN1 values('" & txtCompte.Text & "','" & cl & "','" & EnleverApost(txtLibelleCompte.Text) & "')"
                        ExecuteNonQuery(query)

                    Else
                        'modification de la sous classe
                        query = "Update T_COMP_CLASSEN1 set LIBELLE_CLN1='" & EnleverApost(txtLibelleCompte.Text) & "' where CODE_CLN1='" & txtCompte.Text & "'"
                        ExecuteNonQuery(query)
                    End If
                ElseIf Len(txtCompte.Text) = 4 Then
                    'insertion compte classe niveau4
                    query = "select * from T_COMP_CLASSEN2 where CODE_CLN2='" & cl2.ToString & "'"
                    Dim dt = ExcecuteSelectQuery(query)
                    If dt.Rows.Count = 0 Then

                        'insertion de la classe niveau1
                        query = "select * from T_COMP_CLASSE0 where CODE_CL0='" & cl0.ToString & "'"
                        dt = ExcecuteSelectQuery(query)
                        If dt.Rows.Count = 0 Then
                            query = "insert into T_COMP_CLASSE0 values('" & cl0 & "','')"
                            ExecuteNonQuery(query)
                        End If
                        '

                        'insertion de la classe niveau2
                        query = "select * from T_COMP_CLASSE where CODE_CL='" & cl.ToString & "'"
                        dt = ExcecuteSelectQuery(query)
                        If dt.Rows.Count = 0 Then
                            query = "insert into T_COMP_CLASSE values('" & cl & "','','" & cl0 & "')"
                            ExecuteNonQuery(query)
                        End If
                        '

                        'insertion compte classe niveau3
                        query = "select * from T_COMP_CLASSEN1 where CODE_CLN1='" & cl1.ToString & "'"
                        dt = ExcecuteSelectQuery(query)
                        If dt.Rows.Count = 0 Then
                            'insertion de la sous classe
                            query = "insert into T_COMP_CLASSEN1 values('" & cl1 & "','" & cl & "','')"
                            ExecuteNonQuery(query)
                        End If
                        '
                        'insertion de la classe niveau4
                        query = "insert into T_COMP_CLASSEN2 values('" & txtCompte.Text & "','" & cl1 & "','" & EnleverApost(txtLibelleCompte.Text) & "')"
                        ExecuteNonQuery(query)
                    Else
                        'modification de la classe niveau2
                        query = "Update T_COMP_CLASSEN2 set LIBELLE_CLN2='" & EnleverApost(txtLibelleCompte.Text) & "' where CODE_CLN2='" & txtCompte.Text & "'"
                        ExecuteNonQuery(query)
                    End If
                ElseIf Len(txtCompte.Text) > 4 Then

                    'insertion de la classe niveau1
                    query = "select * from T_COMP_CLASSE0 where CODE_CL0='" & cl0.ToString & "'"
                    Dim dt = ExcecuteSelectQuery(query)
                    If dt.Rows.Count = 0 Then
                        query = "insert into T_COMP_CLASSE0 values('" & cl0 & "','')"
                        ExecuteNonQuery(query)
                    End If

                    'insertion de la classe niveau2
                    query = "select * from T_COMP_CLASSE where CODE_CL='" & cl.ToString & "'"
                    dt = ExcecuteSelectQuery(query)
                    If dt.Rows.Count = 0 Then
                        query = "insert into T_COMP_CLASSE values('" & cl & "','','" & cl0 & "')"
                        ExecuteNonQuery(query)
                    End If

                    'insertion compte classe niveau3
                    query = "select * from T_COMP_CLASSEN1 where CODE_CLN1='" & cl1.ToString & "'"
                    dt = ExcecuteSelectQuery(query)
                    If dt.Rows.Count = 0 Then
                        query = "insert into T_COMP_CLASSEN1 values('" & cl1 & "','" & cl & "','')"
                        ExecuteNonQuery(query)
                    End If

                    'insertion compte classe niveau4
                    query = "select * from T_COMP_CLASSEN2 where CODE_CLN2='" & cl2.ToString & "'"
                    dt = ExcecuteSelectQuery(query)
                    If dt.Rows.Count = 0 Then
                        '
                        query = "insert into T_COMP_CLASSEN2 values('" & cl2 & "','" & cl1 & "','')"
                        ExecuteNonQuery(query)
                        '
                    End If

                    'insertion de la classe niveau5
                    query = "select * from T_COMP_SOUS_CLASSE where CODE_SC='" & txtCompte.Text & "'"
                    dt = ExcecuteSelectQuery(query)
                    If dt.Rows.Count = 0 Then

                        query = "insert into T_COMP_SOUS_CLASSE values('" & txtCompte.Text & "','" & cl & "','" & EnleverApost(txtLibelleCompte.Text) & "','" & cl2 & "','','N')"
                        ExecuteNonQuery(query)

                        'date
                        Dim datdeb As String = ExerciceComptable.Rows(0).Item("datedebut")

                        'convertion de la date en date anglaise
                        Dim str(3) As String
                        str = CDate(datdeb).ToString("dd/MM/yyyy").Split("/")
                        Dim tempdt As String = String.Empty
                        For j As Integer = 2 To 0 Step -1
                            tempdt += str(j) & "-"
                        Next
                        tempdt = tempdt.Substring(0, 10)

                        'On insert le nouveau compte comptable dans la table de report de tous les exercices
                        query = "SELECT * FROM t_comp_exercice"
                        Dim dtExercice As DataTable = ExcecuteSelectQuery(query)
                        For Each rwExo In dtExercice.Rows
                            query = "SELECT * FROM report_sc WHERE code_sc='" & txtCompte.Text & "' AND DATE_LE='" & dateconvert(CDate(rwExo("datedebut"))) & "'"
                            Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
                            If dtVerif.Rows.Count = 0 Then
                                query = "insert into Report_sc values (NULL, '" & txtCompte.Text & "','0', '0','0','0','" & dateconvert(CDate(rwExo("datedebut"))) & "')"
                                ExecuteNonQuery(query)
                            End If
                        Next

                    Else
                        'modification de la classe niveau5
                        query = "Update T_COMP_SOUS_CLASSE set LIBELLE_SC='" & EnleverApost(txtLibelleCompte.Text) & "' where CODE_SC='" & txtCompte.Text & "'"
                        ExecuteNonQuery(query)
                    End If

                End If

                txtCompte.Focus()

                TxtRechercher_TextChanged(TxtRechercher, New EventArgs)

                'Try
                '    Class3.LoadPage(LgListComptable, Class3.CurrentPage)
                'Catch ex As Exception
                '    TxtRechercher_TextChanged(TxtRechercher, New EventArgs)
                'End Try

                ''remplir le datagrid
                'query = "select count(*) from t_comp_classe0"
                'Dim nbre = Val(ExecuteScallar(query))

                'With Class3

                '    .PageSize = TxtPageSize.Text
                '    .MaxRec = nbre \ .PageSize
                '    .PageCount = .MaxRec \ .PageSize
                '    If (.MaxRec Mod .PageSize) > 0 Then
                '        .PageCount = .PageCount + 1
                '    End If

                '    .CurrentPage = 1
                '    .Resqlconno = 0

                '    .LoadPage(LgListComptable, .CurrentPage)
                'End With

                EffacerTexBox10(GroupBox3)
                ListBox2.Items.Clear()
                ListBox3.Items.Clear()
                txtCompte.Focus()

            Else
                SuccesMsg("Veuillez remplir ces champs : " & ControlChars.CrLf + erreur)
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & vbNewLine & ex.ToString())
        End Try
    End Sub

    Private Sub btengsc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btengsc.Click
        insertcompte()
    End Sub

    Private Sub btsuppr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btsuppr.Click
        Try
            Dim cpte As Decimal = 0
            Dim str As String = String.Empty
            Dim warning As Boolean = False
            For i = 0 To (ViewComptable.RowCount - 1)
                '
                If CBool(ViewComptable.GetRowCellValue(i, "Choix")) = True Then
                    Dim CodeSC As String = ViewComptable.GetRowCellValue(i, "Code").ToString
                    cpte += 1

                    If CodeSC.Length < 6 And Not warning Then
                        warning = True
                    End If

                    query = "select * from T_COMP_LIGNE_ECRITURE where code_sc LIKE '" & CodeSC & "%' AND (DEBIT_LE<>'0' OR CREDIT_LE<>'0')"
                    Dim dtVerif = ExcecuteSelectQuery(query)
                    If dtVerif.Rows.Count > 0 Then
                        str &= CodeSC & vbNewLine
                        Continue For
                    End If

                    'Le budget
                    query = "select * from t_besoinpartition where NumeroComptable LIKE '" & CodeSC & "%' AND PUNature<>'0'"
                    dtVerif = ExcecuteSelectQuery(query)
                    If dtVerif.Rows.Count > 0 Then
                        str &= CodeSC & vbNewLine
                        Continue For
                    End If

                    'Les marches
                    query = "select * from t_acteng where NumeroComptable LIKE '" & CodeSC & "%'"
                    dtVerif = ExcecuteSelectQuery(query)
                    If dtVerif.Rows.Count > 0 Then
                        str &= CodeSC & vbNewLine
                        Continue For
                    End If

                End If
            Next

            If cpte = 0 Then
                SuccesMsg("Veuillez cocher un compte comptable")
            Else
                If str.Length > 0 Then
                    FailMsg("Les comptes suivant ne peuvent pas être supprimé." & vbNewLine & str)
                    Exit Sub
                End If
                If warning Then
                    str = "Attention." & vbNewLine & "Ce compte sera supprimé avec tous ces comptes divisionnaires." & vbNewLine & "Voulez-vous vraiment supprimer ce compte?"
                Else
                    str = "Voulez-vous vraiment supprimer ce compte"
                End If
                If cpte > 1 Then
                    If warning Then
                        str = "Attention." & vbNewLine & "Le système supprimera tous les comptes divisionnaires associés aux comptes sélectionnés." & vbNewLine & "Voulez-vous vraiment les supprimer?"
                    Else
                        str = "Voulez-vous vraiment supprimer ces comptes"
                    End If
                End If

                Dim Reponse As DialogResult
                If Mid(str, 1, 9) = "Attention" Then
                    Reponse = ConfirmMsgWarning(str)
                Else
                    Reponse = ConfirmMsg(str)
                End If
                If Reponse = DialogResult.Yes Then
                    For i = 0 To (ViewComptable.RowCount - 1)
                        If CBool(ViewComptable.GetRowCellValue(i, "Choix")) = True Then
                            Dim CodeSC As String = ViewComptable.GetRowCellValue(i, "Code").ToString

                            If Len(CodeSC) = "1" Then
                                'suppression de la classe niveau4
                                query = "DELETE FROM T_COMP_SOUS_CLASSE WHERE CODE_SC LIKE '" & CodeSC & "%'"
                                ExecuteNonQuery(query)
                                query = "DELETE FROM T_COMP_CLASSEN2 WHERE CODE_CLN2 LIKE '" & CodeSC & "%'"
                                ExecuteNonQuery(query)
                                query = "DELETE FROM T_COMP_CLASSEN1 WHERE CODE_CLN1 LIKE '" & CodeSC & "%'"
                                ExecuteNonQuery(query)
                                query = "DELETE FROM T_COMP_CLASSE WHERE CODE_CL LIKE '" & CodeSC & "%'"
                                ExecuteNonQuery(query)
                                query = "DELETE FROM T_COMP_CLASSE0 WHERE CODE_CL0='" & CodeSC & "'"
                                ExecuteNonQuery(query)
                                'DeleteRecords("T_COMP_CLASSE0", "CODE_CL0", CodeSC)

                            ElseIf Len(CodeSC) = "2" Then
                                'suppression de la classe niveau4
                                query = "DELETE FROM T_COMP_SOUS_CLASSE WHERE CODE_SC LIKE '" & CodeSC & "%'"
                                ExecuteNonQuery(query)
                                query = "DELETE FROM T_COMP_CLASSEN2 WHERE CODE_CLN2 LIKE '" & CodeSC & "%'"
                                ExecuteNonQuery(query)
                                query = "DELETE FROM T_COMP_CLASSEN1 WHERE CODE_CLN1 LIKE '" & CodeSC & "%'"
                                ExecuteNonQuery(query)
                                query = "DELETE FROM T_COMP_CLASSE WHERE CODE_CL='" & CodeSC & "'"
                                ExecuteNonQuery(query)
                                'DeleteRecords("T_COMP_CLASSE", "CODE_CL", CodeSC)
                            ElseIf Len(CodeSC) = "3" Then
                                'suppression de la classe niveau4
                                query = "DELETE FROM T_COMP_SOUS_CLASSE WHERE CODE_SC LIKE '" & CodeSC & "%'"
                                ExecuteNonQuery(query)
                                query = "DELETE FROM T_COMP_CLASSEN2 WHERE CODE_CLN2 LIKE '" & CodeSC & "%'"
                                ExecuteNonQuery(query)
                                query = "DELETE FROM T_COMP_CLASSEN1 WHERE CODE_CLN1='" & CodeSC & "'"
                                ExecuteNonQuery(query)
                                'DeleteRecords("T_COMP_CLASSEN1", "CODE_CLN1", CodeSC)
                            ElseIf Len(CodeSC) = "4" Then
                                'suppression de la classe niveau4
                                query = "DELETE FROM T_COMP_SOUS_CLASSE WHERE CODE_SC LIKE '" & CodeSC & "%'"
                                ExecuteNonQuery(query)
                                query = "DELETE FROM T_COMP_CLASSEN2 WHERE CODE_CLN2='" & CodeSC & "'"
                                ExecuteNonQuery(query)
                                'DeleteRecords("T_COMP_CLASSEN2", "CODE_CLN2", CodeSC)
                            ElseIf Len(CodeSC) > "4" Then
                                'suppression de la classe niveau4
                                query = "DELETE FROM T_COMP_SOUS_CLASSE WHERE CODE_SC='" & CodeSC & "'"
                                ExecuteNonQuery(query)
                                'DeleteRecords("T_COMP_SOUS_CLASSE", "CODE_SC", CodeSC)
                            End If
                            query = "DELETE FROM report_sc WHERE code_sc LIKE '" & CodeSC & "%'"
                            ExecuteNonQuery(query)
                            query = "DELETE FROM t_comp_activite WHERE CODE_SC LIKE '" & CodeSC & "%'"
                            ExecuteNonQuery(query)
                        End If
                    Next
                    txtCompte.ResetText()
                    txtLibelleCompte.ResetText()
                    SuccesMsg("Suppression effectuée avec succès.")
                    TxtRechercher_TextChanged(TxtRechercher, New EventArgs)

                End If
            End If

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & vbNewLine & ex.ToString())
        End Try

    End Sub

    Private Sub btnewsc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnewsc.Click
        Try
            ActiverChamps10(GroupBox3)
            EffacerTexBox10(GroupBox3)
            ListBox2.Items.Clear()
            ListBox3.Items.Clear()
            txtCompte.Focus()
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & vbNewLine & ex.ToString())
        End Try

    End Sub

    Private Sub btansc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btansc.Click
        InitialiserForm()
    End Sub
    Private Sub InitialiserForm()
        On Error Resume Next
        EffacerTexBox10(GroupBox3)
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
    End Sub
    Private Sub txtCompte_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCompte.KeyPress
        Try
            Select Case e.KeyChar
                Case ControlChars.CrLf
                    insertcompte()
            End Select
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & vbNewLine & ex.ToString())
        End Try
    End Sub

    Private Sub txtCompte_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCompte.Leave
        Try
            'insertion de la classe niveau1
            '
            query = "select * from T_COMP_CLASSE0 where CODE_CL0='" & txtCompte.Text & "'"
            Dim dt = ExcecuteSelectQuery(query)
            If dt.Rows.Count = 0 Then
            Else
                SuccesMsg("Le Compte Existe Déjà.")
                txtCompte.Text = ""
                txtCompte.Focus()
            End If
            '

            'insertion de la classe niveau2
            '
            query = "select * from T_COMP_CLASSE where CODE_CL='" & txtCompte.Text & "'"
            dt = ExcecuteSelectQuery(query)
            If dt.Rows.Count = 0 Then
            Else
                SuccesMsg("Le Compte Existe Déjà.")
                txtCompte.Text = ""
                txtCompte.Focus()
            End If
            '

            'insertion compte classe niveau3
            '
            query = "select * from T_COMP_CLASSEN1 where CODE_CLN1='" & txtCompte.Text & "'"
            dt = ExcecuteSelectQuery(query)
            If dt.Rows.Count = 0 Then
            Else
                SuccesMsg("Le Compte Existe Déjà.")
                txtCompte.Text = ""
                txtCompte.Focus()
            End If
            '

            'insertion compte classe niveau4
            '
            query = "select * from T_COMP_CLASSEN2 where CODE_CLN2='" & txtCompte.Text & "'"
            dt = ExcecuteSelectQuery(query)
            If dt.Rows.Count = 0 Then
            Else
                SuccesMsg("Le Compte Existe Déjà.")
                txtCompte.Text = ""
                txtCompte.Focus()
            End If
            '

            'insertion compte classe niveau4
            query = "select * from T_COMP_SOUS_CLASSE where CODE_SC='" & txtCompte.Text & "'"
            dt = ExcecuteSelectQuery(query)
            If dt.Rows.Count = 0 Then
            Else
                SuccesMsg("Le Compte Existe Déjà.")
                txtCompte.Text = ""
                txtCompte.Focus()
            End If
            '
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & vbNewLine & ex.ToString())
        End Try
    End Sub

    Private Sub txtCompte_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtCompte.Validating
        txtLibelleCompte.Focus()
    End Sub

    Private Sub Combct_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        txtCompte.Focus()
    End Sub

    Private Sub txtLibelleCompte_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtLibelleCompte.KeyPress
        Try
            Select Case e.KeyChar
                Case ControlChars.CrLf
                    insertcompte()
                Case Else
            End Select
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & vbNewLine & ex.ToString())
        End Try
    End Sub

    Private Sub BtFrist_Click(sender As System.Object, e As System.EventArgs) Handles BtFrist.Click
        If Class3.CurrentPage > 1 Then
            If TxtRechercher.Text <> "" And TxtRechercher.Text <> "Rechercher" Then
                Class3.CurrentPage = 1
                TxtPage.Text = "Page " & Class3.CurrentPage & "/" & Class3.PageCount
                Class3.RechPage(LgListComptable, Class3.CurrentPage)
            Else
                Class3.CurrentPage = 1
                TxtPage.Text = "Page " & Class3.CurrentPage & "/" & Class3.PageCount
                Class3.LoadPage(LgListComptable, Class3.CurrentPage)
            End If
        End If

        'Class3.CurrentPage = 1
        'Class3.LoadPage(LgListComptable, Class3.CurrentPage)
    End Sub

    Private Sub BtPrev_Click(sender As System.Object, e As System.EventArgs) Handles BtPrev.Click
        If TxtRechercher.Text <> "" And TxtRechercher.Text <> "Rechercher" Then
            If (Class3.CurrentPage > 1) Then
                Class3.CurrentPage = Class3.CurrentPage - 1
                TxtPage.Text = "Page " & Class3.CurrentPage & "/" & Class3.PageCount
                Class3.RechPage(LgListComptable, Class3.CurrentPage)

            End If
        Else
            If (Class3.CurrentPage > 1) Then
                Class3.CurrentPage = Class3.CurrentPage - 1
                TxtPage.Text = "Page " & Class3.CurrentPage & "/" & Class3.PageCount
                Class3.LoadPage(LgListComptable, Class3.CurrentPage)

            End If
        End If

        'If (Class3.CurrentPage > 1) Then
        '    Class3.CurrentPage = Class3.CurrentPage - 1
        '    Class3.LoadPage(LgListComptable, Class3.CurrentPage)
        'End If
    End Sub

    Private Sub BtNext_Click(sender As System.Object, e As System.EventArgs) Handles BtNext.Click
        If TxtRechercher.Text <> "" And TxtRechercher.Text <> "Rechercher" Then
            If (Class3.CurrentPage < Class3.PageCount) Then
                Class3.CurrentPage = Class3.CurrentPage + 1
                TxtPage.Text = "Page " & Class3.CurrentPage & "/" & Class3.PageCount
                Class3.RechPage(LgListComptable, Class3.CurrentPage)

            End If
        Else
            If (Class3.CurrentPage < Class3.PageCount) Then
                Class3.CurrentPage = Class3.CurrentPage + 1
                TxtPage.Text = "Page " & Class3.CurrentPage & "/" & Class3.PageCount
                Class3.LoadPage(LgListComptable, Class3.CurrentPage)
            End If
        End If
        'If (Class3.CurrentPage < Class3.MaxRec) Then
        '    Class3.CurrentPage = Class3.CurrentPage + 1
        '    Class3.LoadPage(LgListComptable, Class3.CurrentPage)
        'End If
    End Sub

    Private Sub BtLast_Click(sender As System.Object, e As System.EventArgs) Handles BtLast.Click
        If Class3.CurrentPage < Class3.PageCount Then
            If TxtRechercher.Text <> "" And TxtRechercher.Text <> "Rechercher" Then
                Class3.CurrentPage = Class3.PageCount
                TxtPage.Text = "Page " & Class3.CurrentPage & "/" & Class3.PageCount
                Class3.RechPage(LgListComptable, Class3.CurrentPage)

            Else
                Class3.CurrentPage = Class3.PageCount
                TxtPage.Text = "Page " & Class3.CurrentPage & "/" & Class3.PageCount
                If Class3.PageCount > 0 Then
                    Class3.LoadPage(LgListComptable, Class3.CurrentPage)
                End If

            End If
        End If

        'Class3.CurrentPage = Class3.MaxRec
        'Class3.LoadPage(LgListComptable, Class3.CurrentPage)
    End Sub

    Private Sub TxtRechercher_TextChanged(sender As Object, e As System.EventArgs) Handles TxtRechercher.TextChanged
        Try

            If TxtRechercher.Text = "" Or TxtRechercher.Text = "Rechercher" Then
                'TxtRechercher.Text = "Rechercher"
                'Plan_tiers_Load(Me, e)
                LoadComptes()
            Else
                Class3.CurrentPage = 1
                Class3.RechPage(LgListComptable, Class3.CurrentPage)
                TxtPage.Text = "Page " & Class3.CurrentPage & "/" & Class3.PageCount
            End If

        Catch ex As Exception
            'SuccesMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

        'Try

        '    If TxtRechercher.Text = "" Or TxtRechercher.Text = "Rechercher" Then
        '        CmbPageSize.Text = 1
        '        TxtPageSize.Properties.Sorted = True
        '        TxtPageSize.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor

        '        query = "select count(*) from t_comp_classe0"
        '        Dim nbre = ExecuteScallar(query)

        '        With Class3

        '            .PageSize = TxtPageSize.Text
        '            .MaxRec = nbre \ .PageSize
        '            .PageCount = .MaxRec \ .PageSize
        '            If (.MaxRec Mod .PageSize) > 0 Then
        '                .PageCount = .PageCount + 1
        '            End If

        '            .CurrentPage = 1
        '            .Resqlconno = 0

        '            .LoadPage(LgListComptable, .CurrentPage)
        '        End With
        '    Else
        '        Class3.CurrentPage = 1
        '        Class3.RechPage(LgListComptable, Class3.CurrentPage)
        '    End If

        'Catch ex As Exception
        '    FailMsg("Erreur : Information non disponible : " & vbNewLine & ex.ToString())
        'End Try
    End Sub

    Private Sub CmbPageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPageSize.SelectedIndexChanged
        If CmbPageSize.SelectedIndex > -1 Then
            Dim ElementNumber As Decimal = Val(CmbPageSize.Text)
            With Class3
                .PageSize = ElementNumber
            End With
            If TxtRechercher.Text = "" Or TxtRechercher.Text = "Rechercher" Then
                LoadComptes(1)
            Else
                Class3.CurrentPage = 1
                TxtPage.Text = "Page " & Class3.CurrentPage & "/" & Class3.PageCount
                Class3.RechPage(LgListComptable, Class3.CurrentPage)
            End If
        Else
            CmbPageSize.SelectedIndex = 0
        End If
    End Sub

    Private Sub compte_general_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim LimitTextBox As Integer = 0
        Try
            query = "SELECT LENGTH(CODE_SC) AS L,CODE_SC FROM t_comp_sous_classe GROUP BY L ORDER BY L DESC LIMIT 1"
            LimitTextBox = Val(ExecuteScallar(query))
        Catch ex As Exception
        End Try
        If LimitTextBox = 0 Then
            LimitTextBox = 6
        End If
        txtCompte.Properties.MaxLength = LimitTextBox
    End Sub
End Class