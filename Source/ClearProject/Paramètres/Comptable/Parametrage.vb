Public Class Parametrage
    Dim dtcompterattachSYS = New DataTable
    Dim dtcompterattachTER = New DataTable
    Dim drxSYS As DataRow
    Dim drxTER As DataRow
    Dim rowIndex As Decimal = -1
    Private Sub PageManager_SelectedPageChanging(sender As Object, e As DevExpress.XtraTab.TabPageChangingEventArgs) Handles PageManager.SelectedPageChanging
        If e.Page.Name = "CfgSyscoa" Then
            'On reinitialise l'affichage des autres onlets
            If pnlAddCompteTER.Visible Then
                btQuitTER.PerformClick()
            End If

            If pnlAddElmt.Visible Then
                btQuitElmt.PerformClick()
            End If
            LoadPageSYS()
        ElseIf e.Page.Name = "CfgTER" Then
            'On reinitialise l'affichage des autres onlets
            If pnlAddCompteSYS.Visible Then
                btCloseSYS.PerformClick()
            End If
            LoadPageTER()
        End If
    End Sub
    Private Sub Parametrage_Load(sender As Object, e As EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        PageManager.SelectedTabPageIndex = 0
        LoadPageSYS()
    End Sub
    Private Sub LoadCompteComptable(Etat As String)
        cmbCondition.ResetText()
        cmbSC.ResetText()
        cmbSC.Properties.Items.Clear()
        cmbSC.Properties.Sorted = True
        If Etat = "Bilan" Then
            query = "SELECT * FROM t_comp_classe WHERE (CODE_CL LIKE '1%' OR CODE_CL LIKE '2%' OR CODE_CL LIKE '3%' OR CODE_CL LIKE '4%' OR CODE_CL LIKE '5%')"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cmbSC.Properties.Items.Add(rw("CODE_CL") & " | " & MettreApost(rw("LIBELLE_CL")))
            Next
            query = "SELECT * FROM t_comp_classen1 WHERE (CODE_CLN1 LIKE '1%' OR CODE_CLN1 LIKE '2%' OR CODE_CLN1 LIKE '3%' OR CODE_CLN1 LIKE '4%' OR CODE_CLN1 LIKE '5%')"
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cmbSC.Properties.Items.Add(rw("CODE_CLN1") & " | " & MettreApost(rw("LIBELLE_CLN1")))
            Next
            query = "SELECT * FROM t_comp_classen2 WHERE (CODE_CLN2 LIKE '1%' OR CODE_CLN2 LIKE '2%' OR CODE_CLN2 LIKE '3%' OR CODE_CLN2 LIKE '4%' OR CODE_CLN2 LIKE '5%')"
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cmbSC.Properties.Items.Add(rw("CODE_CLN2") & " | " & MettreApost(rw("LIBELLE_CLN2")))
            Next
            query = "SELECT * FROM t_comp_sous_classe WHERE (CODE_SC LIKE '1%' OR CODE_SC LIKE '2%' OR CODE_SC LIKE '3%' OR CODE_SC LIKE '4%' OR CODE_SC LIKE '5%')"
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cmbSC.Properties.Items.Add(rw("CODE_SC") & " | " & MettreApost(rw("LIBELLE_SC")))
            Next
        ElseIf Etat = "Compte de résultat" Then
            query = "SELECT * FROM t_comp_classe WHERE MID(CODE_CL,1,1)>5"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cmbSC.Properties.Items.Add(rw("CODE_CL") & " | " & MettreApost(rw("LIBELLE_CL")))
            Next
            query = "SELECT * FROM t_comp_classen1 WHERE MID(CODE_CLN1,1,1)>5"
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cmbSC.Properties.Items.Add(rw("CODE_CLN1") & " | " & MettreApost(rw("LIBELLE_CLN1")))
            Next
            query = "SELECT * FROM t_comp_classen2 WHERE MID(CODE_CLN2,1,1)>5" 'CODE_CLN2 NOT IN(SELECT CODE_SC FROM t_comp_type_rubrique Liens,t_comp_rubrique rb WHERE rb.CODE_RUB=Liens.CODE_RUB AND (ETAT_RUB='Bilan' OR ETAT_RUB='Compte de résultat'))
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cmbSC.Properties.Items.Add(rw("CODE_CLN2") & " | " & MettreApost(rw("LIBELLE_CLN2")))
            Next
            query = "SELECT * FROM t_comp_sous_classe WHERE MID(CODE_SC,1,1)>5"
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cmbSC.Properties.Items.Add(rw("CODE_SC") & " | " & MettreApost(rw("LIBELLE_SC")))
            Next
        ElseIf Etat = "Tableau Emplois Ressources" Then
            query = "SELECT * FROM t_comp_classe"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cmbSCTER.Properties.Items.Add(rw("CODE_CL") & " | " & MettreApost(rw("LIBELLE_CL")))
            Next
            query = "SELECT * FROM t_comp_classen1"
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cmbSCTER.Properties.Items.Add(rw("CODE_CLN1") & " | " & MettreApost(rw("LIBELLE_CLN1")))
            Next
            query = "SELECT * FROM t_comp_classen2"
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cmbSCTER.Properties.Items.Add(rw("CODE_CLN2") & " | " & MettreApost(rw("LIBELLE_CLN2")))
            Next
            query = "SELECT * FROM t_comp_sous_classe"
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cmbSCTER.Properties.Items.Add(rw("CODE_SC") & " | " & MettreApost(rw("LIBELLE_SC")))
            Next
        End If
    End Sub
    Private Function GetLibelleCodeSC(CodeSC As String) As String
        Dim libelle As String = String.Empty
        If CodeSC.Length = 2 Then
            query = "SELECT LIBELLE_CL from t_comp_classe WHERE CODE_CL='" & CodeSC & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count <> 0 Then
                libelle = MettreApost(dt.Rows(0).Item("LIBELLE_CL"))
            End If
        ElseIf CodeSC.Length = 3 Then
            query = "SELECT LIBELLE_CLN1 from t_comp_classen1 WHERE CODE_CLN1='" & CodeSC & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count <> 0 Then
                libelle = MettreApost(dt.Rows(0).Item("LIBELLE_CLN1"))
            End If
        ElseIf CodeSC.Length = 4 Then
            query = "SELECT LIBELLE_CLN2 from t_comp_classen2 WHERE CODE_CLN2='" & CodeSC & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count <> 0 Then
                libelle = MettreApost(dt.Rows(0).Item("LIBELLE_CLN2"))
            End If
        ElseIf CodeSC.Length > 4 Then
            query = "SELECT LIBELLE_SC from t_comp_sous_classe WHERE CODE_SC='" & CodeSC & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count <> 0 Then
                libelle = MettreApost(dt.Rows(0).Item("LIBELLE_SC"))
            End If
        End If
        Return libelle
    End Function

#Region "SYSCOA"

    Private Sub btSaveSYS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSaveSYS.Click
        If drxSYS("Type") = "Détails" Then
            Dim IDRebrique As String = drxSYS("Identifiant")

            query = "delete from T_COMP_TYPE_RUBRIQUE where code_rub='" & IDRebrique & "'"
            ExecuteNonQuery(query)

            For i = 0 To ViewCompteRattach.RowCount - 1
                query = "insert into T_COMP_TYPE_RUBRIQUE values('" & IDRebrique & "','" & ViewCompteRattach.GetRowCellValue(i, "N° Comptable").ToString & "','" & ViewCompteRattach.GetRowCellValue(i, "Condition").ToString & "','" & ViewCompteRattach.GetRowCellValue(i, "Type").ToString & "','" & drxSYS("Etat") & "')"
                ExecuteNonQuery(query)
            Next

        End If

        'vider le formulaire
        EffacerTexBox2(GroupControl1)
        EffacerTexBox2(GroupControl2)
        pnlRubrique.Visible = True
        pnlAddCompteSYS.Visible = False

        'remplir le datagrid
        LoadPageSYS()

    End Sub
    Private Sub LoadPageSYS()
        DebutChargement()
        RemplirDatagridCTT(LgListParamEtat, ViewActivite, cmbRubrique.Text)

        dtcompterattachSYS.Columns.Clear()
        dtcompterattachSYS.Columns.Add("Type", Type.GetType("System.String"))
        dtcompterattachSYS.Columns.Add("N° Comptable", Type.GetType("System.String"))
        dtcompterattachSYS.Columns.Add("Libellé", Type.GetType("System.String"))
        dtcompterattachSYS.Columns.Add("Condition", Type.GetType("System.String"))
        dtcompterattachSYS.Rows.Clear()
        FinChargement()
        cmbRubrique.Select()
    End Sub
    Private Sub cmbSC_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbSC.KeyPress, cmbCondition.KeyPress, cmbType.KeyPress
        Try
            Select Case e.KeyChar
                Case ControlChars.CrLf
                    'Vérification des champ text
                    Dim erreur As String = ""

                    If cmbSC.SelectedIndex = -1 Then
                        erreur += "- Numéro comptable " & ControlChars.CrLf
                    End If

                    If cmbCondition.SelectedIndex = -1 Then
                        erreur += "- Condition" & ControlChars.CrLf
                    End If
                    If cmbType.Visible And cmbType.SelectedIndex = -1 Then
                        erreur += "- Type" & ControlChars.CrLf
                    End If

                    If erreur = "" Then
                        Dim id As String()
                        id = cmbSC.Text.Split(" | ")
                        Dim CodeSC As String = id(0)

                        'On verifie dans la base de données, si un compte est partiellement utilise
                        query = "SELECT CODE_SC,Liens.CODE_RUB,Liens.condition FROM t_comp_type_rubrique Liens,t_comp_rubrique rb WHERE rb.CODE_RUB=Liens.CODE_RUB AND (Liens.ETAT_RUB='Bilan' OR Liens.ETAT_RUB='Compte de résultat') AND CODE_SC LIKE '" & CodeSC & "%'"
                        Dim dt As DataTable = ExcecuteSelectQuery(query)
                        If dt.Rows.Count > 0 Then
                            Dim OldCondition = dt.Rows(0)("condition")
                            If OldCondition = "Les Deux" Or cmbCondition.Text = "Les Deux" Then
                                FailMsg("Le compte comptable : " & CodeSC & " est déjà paramétré partiellement sur " & dt.Rows(0)("CODE_RUB") & ".")
                                Exit Sub
                            ElseIf OldCondition = cmbCondition.Text Then
                                FailMsg("Le compte comptable : " & CodeSC & " est déjà paramétré partiellement sur " & dt.Rows(0)("CODE_RUB") & ".")
                                Exit Sub
                            End If
                        Else
                            Dim Code As String = String.Empty
                            For i = 1 To CodeSC.Length
                                Code += Mid(CodeSC, i, 1)
                                query = "SELECT CODE_SC,Liens.CODE_RUB,Liens.condition FROM t_comp_type_rubrique Liens,t_comp_rubrique rb WHERE rb.CODE_RUB=Liens.CODE_RUB AND (Liens.ETAT_RUB='Bilan' OR Liens.ETAT_RUB='Compte de résultat') AND CODE_SC='" & Code & "'"
                                dt = ExcecuteSelectQuery(query)
                                If dt.Rows.Count > 0 Then
                                    Dim OldCondition = dt.Rows(0)("condition")
                                    If OldCondition = "Les Deux" Or cmbCondition.Text = "Les Deux" Then
                                        FailMsg("Le compte comptable : " & CodeSC & " est déjà paramétré partiellement sur " & dt.Rows(0)("CODE_RUB") & ".")
                                        Exit Sub
                                    ElseIf OldCondition = cmbCondition.Text Then
                                        FailMsg("Le compte comptable : " & CodeSC & " est déjà paramétré partiellement sur " & dt.Rows(0)("CODE_RUB") & ".")
                                        Exit Sub
                                    End If
                                End If
                            Next
                        End If

                        For i = 0 To ViewCompteRattach.RowCount - 1
                            If (CodeSC = ViewCompteRattach.GetRowCellValue(i, "N° Comptable")) Then
                                SuccesMsg(CodeSC & " déjà ajouté.")
                                Exit Sub
                            End If
                        Next

                        'On verifie dans la liste, si un compte est partiellement utilise
                        For i = 0 To ViewCompteRattach.RowCount - 1
                            'SuccesMsg(ViewCompteRattach.GetRowCellValue(i, "N° Comptable") & vbNewLine & CodeSC)
                            Dim Code As String = String.Empty
                            For j = 1 To ViewCompteRattach.GetRowCellValue(i, "N° Comptable").Length
                                Code += Mid(ViewCompteRattach.GetRowCellValue(i, "N° Comptable"), j, 1)
                                If Code = CodeSC Then
                                    FailMsg("Le compte comptable : " & CodeSC & " est déjà paramétré partiellement.")
                                    Exit Sub
                                End If
                            Next
                        Next

                        For i = 0 To ViewCompteRattach.RowCount - 1
                            Dim Code As String = String.Empty
                            For j = 1 To CodeSC.Length
                                Code += Mid(CodeSC, j, 1)
                                If Code = ViewCompteRattach.GetRowCellValue(i, "N° Comptable") Then
                                    FailMsg("Le compte comptable : " & CodeSC & " est déjà paramétré partiellement.")
                                    Exit Sub
                                End If
                            Next
                        Next
                        Dim drS = dtcompterattachSYS.NewRow()
                        Try
                            If drxSYS("Etat") = "Bilan" And IsActif(drxSYS("Identifiant")) Then
                                drS(0) = cmbType.Text
                            Else
                                drS(0) = ""
                            End If
                        Catch ex As Exception
                            dtcompterattachSYS.rows.clear()
                            If pnlAddCompteSYS.Visible Then
                                btCloseSYS.PerformClick()
                            End If
                            Exit Sub
                        End Try

                        drS(1) = CodeSC
                        drS(2) = GetLibelleCodeSC(CodeSC)
                        drS(3) = cmbCondition.Text
                        dtcompterattachSYS.Rows.Add(drS)
                        LgListCompteRattach.DataSource = dtcompterattachSYS

                        cmbCondition.ResetText()
                        cmbSC.ResetText()
                        cmbType.ResetText()
                        If cmbType.Visible Then
                            cmbType.Select()
                        Else
                            cmbSC.Select()
                        End If

                    Else
                        SuccesMsg("Veuillez remplir ces champs : " & ControlChars.CrLf + erreur)
                    End If
                Case Else
            End Select
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub
    Private Sub LgListParamEtat_DoubleClick(sender As Object, e As System.EventArgs) Handles LgListParamEtat.DoubleClick
        Try
            If ViewActivite.RowCount > 0 Then

                dtcompterattachSYS.rows.clear()

                drxSYS = ViewActivite.GetDataRow(ViewActivite.FocusedRowHandle)

                If drxSYS("Type").ToString = "Détails" Then
                    If drxSYS("Etat") = "Bilan" And IsActif(drxSYS("Identifiant")) Then
                        cmbSC.Dock = DockStyle.Right
                        cmbType.Visible = True
                        cmbType.SelectedIndex = 0
                    Else
                        cmbType.Visible = False
                        cmbSC.Dock = DockStyle.Fill
                    End If
                    pnlAddCompteSYS.Visible = True
                    pnlRubrique.Visible = False
                    cmbCondition.ResetText()
                    cmbSC.ResetText()
                    cmbType.ResetText()
                    If cmbType.Visible Then
                        cmbType.Select()
                    Else
                        cmbSC.Select()
                    End If

                    dtcompterattachSYS.Rows.Clear()

                    LoadCompteComptable(drxSYS("Etat").ToString)

                    query = "select * from T_COMP_TYPE_RUBRIQUE tr where CODE_RUB='" & drxSYS(2).ToString & "' AND ETAT_RUB='" & drxSYS("Etat") & "'"
                    Dim dt = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        If GetLibelleCodeSC(rw("CODE_SC")).Length = 0 Then 'Permettra d'ignorer les comptes qui ne sont plus dans le compte comptable
                            Continue For
                        End If
                        Dim drS = dtcompterattachSYS.NewRow()
                        drS(0) = rw("TYPE").ToString()
                        drS(1) = rw("CODE_SC").ToString
                        drS(2) = GetLibelleCodeSC(rw("CODE_SC"))
                        drS(3) = rw("condition").ToString
                        dtcompterattachSYS.Rows.Add(drS)
                    Next

                    LgListCompteRattach.DataSource = dtcompterattachSYS
                    ViewCompteRattach.OptionsView.ColumnAutoWidth = True
                    If cmbType.Visible Then
                        ViewCompteRattach.Columns(0).Visible = True
                        ViewCompteRattach.Columns(0).Width = 100
                        ViewCompteRattach.Columns(1).Width = 100
                        ViewCompteRattach.Columns(2).Width = 352
                    Else
                        ViewCompteRattach.Columns(0).Visible = False
                        ViewCompteRattach.Columns(0).Width = 100
                        ViewCompteRattach.Columns(1).Width = 100
                        ViewCompteRattach.Columns(2).Width = 452
                    End If
                    LgListCompteRattach.Refresh()
                Else
                    If pnlAddCompteSYS.Visible Then
                        btCloseSYS.PerformClick()
                    End If
                End If

                Dim ID = drxSYS(2).ToString
                ColorRowGrid(ViewActivite, "[Choix]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
                ColorRowGridAnal(ViewActivite, "[Identifiant]='" & ID & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
                ColorRowGridAnal(ViewActivite, "[Type]='En-tête'", Color.SteelBlue, "Times New Roman", 10, FontStyle.Bold, Color.Black)
                ColorRowGridAnal(ViewActivite, "[Type]='Sous Total'", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
            End If

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub
    Private Function IsActif(ID As String) As Boolean
        If ID.Length = 2 Then
            Dim FirstChar As String = Mid(ID, 1, 1)
            If FirstChar = "A" Or FirstChar = "B" Then
                Return True
            End If
        End If
        Return False
    End Function
    Private Sub LgListCompteRattach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LgListCompteRattach.Click
        If (ViewCompteRattach.RowCount > 0) Then
            Dim drx = ViewCompteRattach.GetDataRow(ViewCompteRattach.FocusedRowHandle)
            Dim NCOMP = drx("N° Comptable").ToString
            ColorRowGrid(ViewCompteRattach, "[N° Comptable]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewCompteRattach, "[N° Comptable]='" & NCOMP & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
        End If
    End Sub

    Private Sub SupprimerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupprimerToolStripMenuItem.Click
        Try
            If ViewActivite.RowCount > 0 Then
                If ViewCompteRattach.FocusedRowHandle <> -1 Then
                    ViewCompteRattach.GetDataRow(ViewCompteRattach.FocusedRowHandle).Delete()
                End If
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub cmbRubrique_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbRubrique.SelectedIndexChanged
        If cmbRubrique.SelectedIndex > -1 Then
            RemplirDatagridCTT(LgListParamEtat, ViewActivite, cmbRubrique.Text)
        Else
            RemplirDatagridCTT(LgListParamEtat, ViewActivite, "")
        End If
    End Sub

    Private Sub btCloseSYS_Click(sender As Object, e As EventArgs) Handles btCloseSYS.Click
        pnlRubrique.Visible = True
        pnlAddCompteSYS.Visible = False
    End Sub

    Private Sub ContextMenuComptes_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuComptesSYS.Opening
        If ViewCompteRattach.RowCount = 0 Then
            e.Cancel = True
        Else
            Dim drx = ViewCompteRattach.GetDataRow(ViewCompteRattach.FocusedRowHandle)
            Dim NCOMP = drx("N° Comptable").ToString
            ColorRowGrid(ViewCompteRattach, "[N° Comptable]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewCompteRattach, "[N° Comptable]='" & NCOMP & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
        End If
    End Sub

#End Region
#Region "Tableau Emplois Ressources"
    Private Sub btAddElmTER_Click(sender As Object, e As EventArgs) Handles btAddElmTER.Click
        AddElmt()
        pnlAddElmt.Visible = True
        pnlOptionTER.Visible = False
        cmbTypeTER.ResetText()
        cmbEnteteTER.ResetText()
        txtLibelleTER.ResetText()
        txtIdTER.ResetText()
        cmbEnteteTER.Select()
    End Sub

    Private Sub btModifElmt_Click(sender As Object, e As EventArgs) Handles btModifElmt.Click
        If ViewElementsTER.RowCount > 0 Then
            If ViewElementsTER.FocusedRowHandle <> -1 Then
                Dim drX = ViewElementsTER.GetFocusedDataRow
                If drX("Type") <> "En-tête" Then
                    ModifElmt(drX)
                End If
            End If
        End If
    End Sub
    Private Sub ModifElmt(Element As DataRow)
        Dim ID As String = Element("Identifiant")
        Dim IDPere As String = String.Empty
        If ID.Length = 0 Then
            FailMsg("Imposssible de modifier cet élément.")
            Exit Sub
        End If
        'Recuperation du conteneur
        Try
            IDPere = ExecuteScallar("SELECT ID_RUBENT FROM t_comp_rubriquest WHERE ID_RUBST='" & ID & "' AND ETAT_RUB='Tableau Emplois Ressources'")
        Catch ex As Exception
        End Try

        If IDPere.Length = 0 Then
            Try
                IDPere = ExecuteScallar("SELECT ID_RUBST FROM t_comp_rubrique WHERE CODE_RUB='" & ID & "' AND ETAT_RUB='Tableau Emplois Ressources'")
            Catch ex As Exception
            End Try
            If IDPere.Length = 0 Then
                FailMsg("Imposssible de modifier cet élément.")
                Exit Sub
            End If
        End If
        gcAddModifElmt.Text = "Modification " & ID
        If Element("Type") = "Détails" Then
            lblTypeElmt.Text = "Sous Total"
            cmbTypeTER.Enabled = True
            lblTypeElmt.Location = New Point(237, 34)

            'Recuperation des donnees
            Dim Entete As String = String.Empty
            Dim soustotal As String = String.Empty
            Try
                Entete = MettreApost(ExecuteScallar("SELECT LIBELLE_RUBENT FROM t_comp_rubriqueent WHERE ID_RUBENT='" & IDPere & "' AND ETAT_RUB='Tableau Emplois Ressources'"))
            Catch ex As Exception
            End Try
            If Entete.Length = 0 Then 'Le ID du conteneur n'ai pas un entete, mais un sous total
                soustotal = IDPere & " - " & MettreApost(ExecuteScallar("SELECT LIBELLE_RUBST FROM t_comp_rubriquest WHERE ID_RUBST='" & IDPere & "' AND ETAT_RUB='Tableau Emplois Ressources'"))
                Entete = MettreApost(ExecuteScallar("SELECT LIBELLE_RUBENT FROM t_comp_rubriqueent WHERE ID_RUBENT='" & ExecuteScallar("SELECT ID_RUBENT FROM t_comp_rubriquest WHERE ID_RUBST='" & IDPere & "' AND ETAT_RUB='Tableau Emplois Ressources'") & "' AND ETAT_RUB='Tableau Emplois Ressources'"))
            End If
            LoadSousTotal(Entete)
            cmbEnteteTER.Text = Entete
            cmbTypeTER.Text = soustotal
            txtLibelleTER.Text = Element("Libellé")
            txtIdTER.Text = Element("Identifiant")
        ElseIf Element("Type") = "Sous Total" Then
            lblTypeElmt.Text = "Type"
            lblTypeElmt.Location = New Point(261, 34)
            cmbTypeTER.Enabled = False

            'Recuperation des donnees
            cmbEnteteTER.Text = MettreApost(ExecuteScallar("SELECT LIBELLE_RUBENT FROM t_comp_rubriqueent WHERE ID_RUBENT='" & IDPere & "' AND ETAT_RUB='Tableau Emplois Ressources'"))
            cmbTypeTER.Text = "Sous Total"
            txtLibelleTER.Text = Element("Libellé")
            txtIdTER.Text = Element("Identifiant")
        End If

        pnlAddElmt.Visible = True
        pnlOptionTER.Visible = False
        cmbEnteteTER.Select()
    End Sub
    Private Sub AddElmt()
        gcAddModifElmt.Text = "Nouvel Element"
        lblTypeElmt.Text = "Type"
        lblTypeElmt.Location = New Point(261, 34)
        cmbTypeTER.Enabled = True
        cmbTypeTER.ResetText()
        cmbTypeTER.Properties.Items.Clear()
        cmbTypeTER.Properties.Items.AddRange({"Sous Total", "Détails"})
        cmbEnteteTER.Select()
    End Sub
    Private Sub LoadSousTotal(Entete As String)
        cmbTypeTER.ResetText()
        cmbTypeTER.Properties.Items.Clear()
        If Entete = "RESSOURCES" Then
            query = "SELECT * FROM t_comp_rubriquest WHERE ID_RUBENT='RE' AND ETAT_RUB='Tableau Emplois Ressources'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cmbTypeTER.Properties.Items.Add(rw("ID_RUBST") & " - " & MettreApost(rw("LIBELLE_RUBST")))
            Next
        ElseIf Entete = "EMPLOIS" Then
            query = "SELECT * FROM t_comp_rubriquest WHERE ID_RUBENT='EM' AND ETAT_RUB='Tableau Emplois Ressources'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cmbTypeTER.Properties.Items.Add(rw("ID_RUBST") & " - " & MettreApost(rw("LIBELLE_RUBST")))
            Next
        End If
    End Sub
    Private Sub btDelElmt_Click(sender As Object, e As EventArgs) Handles btDelElmt.Click
        If ViewElementsTER.RowCount > 0 Then
            If ViewElementsTER.FocusedRowHandle <> -1 Then
                Dim drx = ViewElementsTER.GetDataRow(ViewElementsTER.FocusedRowHandle)
                Dim ID As String = drx("Identifiant")
                If drx("Type") = "En-tête" Then
                    If ConfirmMsg("Voulez-vous supprimer tous les éléments des " & drx("Libellé") & "?") = DialogResult.Yes Then

                        query = "SELECT ID_RUBST FROM t_comp_rubriquest WHERE ID_RUBENT='" & ID & "' AND ETAT_RUB='Tableau Emplois Ressources'"
                        Dim dtParcours As DataTable = ExcecuteSelectQuery(query)
                        For Each rw As DataRow In dtParcours.Rows
                            query = "DELETE FROM t_comp_type_rubrique WHERE CODE_RUB IN(SELECT CODE_RUB FROM t_comp_rubrique WHERE ID_RUBST='" & rw("ID_RUBST") & "') AND ETAT_RUB='Tableau Emplois Ressources'"
                            ExecuteNonQuery(query)
                            query = "DELETE FROM t_comp_rubrique WHERE ID_RUBST='" & rw("ID_RUBST") & "' AND ETAT_RUB='Tableau Emplois Ressources'"
                            ExecuteNonQuery(query)
                        Next
                        query = "DELETE FROM t_comp_rubriquest WHERE ID_RUBENT='" & ID & "' AND ETAT_RUB='Tableau Emplois Ressources'"
                        ExecuteNonQuery(query)
                        query = "DELETE FROM t_comp_rubrique WHERE ID_RUBST='" & ID & "' AND ETAT_RUB='Tableau Emplois Ressources'"
                        ExecuteNonQuery(query)

                        LoadPageTER()
                        SuccesMsg("Suppression effectuée avec succès")
                    End If
                ElseIf drx("Type") = "Sous Total" Then
                    If ConfirmMsg("Voulez-vous supprimer le sous total " & drx("Libellé") & " et ses éléments?") = DialogResult.Yes Then
                        query = "DELETE FROM t_comp_type_rubrique WHERE CODE_RUB IN(SELECT CODE_RUB FROM t_comp_rubrique WHERE ID_RUBST='" & ID & "' AND ETAT_RUB='Tableau Emplois Ressources') AND ETAT_RUB='Tableau Emplois Ressources'"
                        ExecuteNonQuery(query)
                        query = "DELETE FROM t_comp_rubrique WHERE ID_RUBST='" & ID & "' AND ETAT_RUB='Tableau Emplois Ressources'"
                        ExecuteNonQuery(query)
                        query = "DELETE FROM t_comp_rubriquest WHERE ID_RUBST='" & ID & "' AND ETAT_RUB='Tableau Emplois Ressources'"
                        ExecuteNonQuery(query)

                        LoadPageTER()
                        SuccesMsg("Suppression effectuée avec succès")
                    End If
                ElseIf drx("Type") = "Détails" Then
                    If ConfirmMsg("Voulez-vous supprimer " & drx("Libellé") & "?") = DialogResult.Yes Then
                        query = "DELETE FROM t_comp_type_rubrique WHERE CODE_RUB='" & ID & "' AND ETAT_RUB='Tableau Emplois Ressources'"
                        ExecuteNonQuery(query)
                        query = "DELETE FROM t_comp_rubrique WHERE CODE_RUB='" & ID & "' AND ETAT_RUB='Tableau Emplois Ressources'"
                        ExecuteNonQuery(query)

                        LoadPageTER()
                        SuccesMsg("Suppression effectuée avec succès")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btQuitTER_Click(sender As Object, e As EventArgs) Handles btQuitTER.Click
        pnlOptionTER.Visible = True
        pnlAddCompteTER.Visible = False
    End Sub

    Private Sub btQuitElmt_Click(sender As Object, e As EventArgs) Handles btQuitElmt.Click
        pnlOptionTER.Visible = True
        pnlAddElmt.Visible = False
    End Sub

    Private Sub LgListElmTER_DoubleClick(sender As Object, e As EventArgs) Handles LgListElmTER.DoubleClick
        If ViewElementsTER.RowCount > 0 Then
            If ViewElementsTER.FocusedRowHandle <> -1 Then
                drxTER = ViewElementsTER.GetDataRow(ViewElementsTER.FocusedRowHandle)
                If drxTER("Type").ToString = "Détails" Then
                    If pnlAddElmt.Visible Then
                        pnlAddElmt.Visible = False
                    End If
                    pnlAddCompteTER.Visible = True
                    pnlOptionTER.Visible = False
                    cmbSCTER.ResetText()
                    cmbConditionTER.ResetText()
                    cmbSCTER.Select()

                    dtcompterattachTER.Rows.Clear()

                    LoadCompteComptable("Tableau Emplois Ressources")

                    query = "select * from T_COMP_TYPE_RUBRIQUE tr where CODE_RUB='" & drxTER("Identifiant").ToString & "' AND ETAT_RUB='Tableau Emplois Ressources'"
                    Dim dt = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        If GetLibelleCodeSC(rw("CODE_SC")).Length = 0 Then 'Permettra d'ignorer les comptes qui ne sont plus dans le compte comptable
                            Continue For
                        End If
                        Dim drS = dtcompterattachTER.NewRow()
                        drS(0) = rw("TYPE").ToString()
                        drS(1) = rw("CODE_SC").ToString
                        drS(2) = GetLibelleCodeSC(rw("CODE_SC"))
                        drS(3) = rw("condition").ToString
                        dtcompterattachTER.Rows.Add(drS)
                    Next

                    LgListCompteRattachTER.DataSource = dtcompterattachTER
                    ViewCompteComptableTER.OptionsView.ColumnAutoWidth = True
                    If cmbType.Visible Then
                        ViewCompteComptableTER.Columns(0).Visible = True
                        ViewCompteComptableTER.Columns(0).Width = 100
                        ViewCompteComptableTER.Columns(1).Width = 100
                        ViewCompteComptableTER.Columns(2).Width = 352
                    Else
                        ViewCompteComptableTER.Columns(0).Visible = False
                        ViewCompteComptableTER.Columns(0).Width = 100
                        ViewCompteComptableTER.Columns(1).Width = 100
                        ViewCompteComptableTER.Columns(2).Width = 452
                    End If
                Else
                    If pnlAddCompteTER.Visible Then
                        btQuitTER.PerformClick()
                    End If
                    If pnlAddElmt.Visible Then
                        btQuitElmt.PerformClick()
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub LoadPageTER()
        DebutChargement()
        RemplirDatagridCT(LgListElmTER, ViewElementsTER)

        dtcompterattachTER.Columns.Clear()
        dtcompterattachTER.Columns.Add("Type", Type.GetType("System.String"))
        dtcompterattachTER.Columns.Add("N° Comptable", Type.GetType("System.String"))
        dtcompterattachTER.Columns.Add("Libellé", Type.GetType("System.String"))
        dtcompterattachTER.Columns.Add("Condition", Type.GetType("System.String"))
        dtcompterattachTER.Rows.Clear()
        FinChargement()
        btAddElmTER.Select()
    End Sub

    Private Sub LgListElmTER_Click(sender As Object, e As EventArgs) Handles LgListElmTER.Click
        If ViewElementsTER.RowCount > 0 Then
            If ViewElementsTER.FocusedRowHandle <> -1 Then
                Dim drX = ViewElementsTER.GetFocusedDataRow
                Dim ID = drX(2).ToString
                ColorRowGrid(ViewElementsTER, "[Identifiant]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
                ColorRowGridAnal(ViewElementsTER, "[Identifiant]='" & ID & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
                ColorRowGridAnal(ViewElementsTER, "[Type]='En-tête'", Color.SteelBlue, "Times New Roman", 10, FontStyle.Bold, Color.Black)
                ColorRowGridAnal(ViewElementsTER, "[Type]='Sous Total' AND [Identifiant]<>'" & ID & "'", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
            End If
        End If
    End Sub

    Private Sub cmbEnteteTER_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEnteteTER.SelectedIndexChanged
        If Mid(gcAddModifElmt.Text, 1, 12) = "Modification" Then
            If cmbEnteteTER.SelectedIndex > -1 Then
                If cmbTypeTER.Enabled Then
                    LoadSousTotal(cmbEnteteTER.Text)
                End If
            End If
        Else
            If cmbTypeTER.Properties.Items.Count > 0 And cmbTypeTER.Properties.Items(0) <> "Sous Total" Then
                cmbTypeTER.Properties.Items.Clear()
                cmbTypeTER.Properties.Items.AddRange({"Sous Total", "Détails"})
            End If
        End If
    End Sub

    Private Sub btSaveElmt_Click(sender As Object, e As EventArgs) Handles btSaveElmt.Click
        'Verification des champs
        If cmbEnteteTER.SelectedIndex = -1 Then
            FailMsg("Veuillez choisir un en-tête.")
            cmbEnteteTER.Select()
            Exit Sub
        End If
        If txtIdTER.Text.Length <> 2 Then
            FailMsg("Veuillez entrer un identiifant de 2 caractères.")
            txtIdTER.Select()
            Exit Sub
        End If
        If txtLibelleTER.Text.Trim().Length = 0 Then
            FailMsg("Veuillez entrer le libellé.")
            txtLibelleTER.Select()
            Exit Sub
        End If

        If Mid(gcAddModifElmt.Text, 1, 12) = "Modification" Then
            'Vérification de la disponibilité de l'ID
            Dim OldID As String = Mid(gcAddModifElmt.Text, 14, 2)
            Dim ID As String = txtIdTER.Text.ToUpper()
            If VerifID(OldID, ID) Then
                FailMsg("L'identifiant " & ID & " existe déjà.")
                txtIdTER.Select()
                Exit Sub
            End If

            Dim Type As String = String.Empty
            If cmbTypeTER.Enabled Then
                Type = "Détails"
            Else
                Type = "Sous Total"
            End If
            'Cas de modification
            Dim IDPere As String
            If Type = "Détails" Then
                If cmbEnteteTER.Text = "EMPLOIS" Then
                    IDPere = "EM"
                ElseIf cmbEnteteTER.Text = "RESSOURCES" Then
                    IDPere = "RE"
                Else
                    FailMsg("Impossible de modifier " & OldID & "")
                    Exit Sub
                End If
                If cmbTypeTER.SelectedIndex <> -1 Then
                    IDPere = cmbTypeTER.Text.Split(" - ")(0) 'On charge le ID du sous total
                End If

                query = "UPDATE t_comp_rubrique SET CODE_RUB='" & ID & "', LIBELLE_RUB='" & limiter(EnleverApost(txtLibelleTER.Text.Trim()), 500) & "', ID_RUBST='" & IDPere & "' WHERE ETAT_RUB='Tableau Emplois Ressources' AND CODE_RUB='" & OldID & "'"
                ExecuteNonQuery(query)
                If OldID <> ID Then
                    'On met à jour le nouveau ID
                    query = "UPDATE t_comp_type_rubrique SET CODE_RUB='" & ID & "' WHERE CODE_RUB='" & OldID & "' AND ETAT_RUB='Tableau Emplois Ressources'"
                    ExecuteNonQuery(query)
                End If
                LoadPageTER()
                SuccesMsg("Modification effectuée avec succès")
                btQuitElmt.PerformClick()
            ElseIf Type = "Sous Total" Then
                If cmbEnteteTER.Text = "EMPLOIS" Then
                    IDPere = "EM"
                ElseIf cmbEnteteTER.Text = "RESSOURCES" Then
                    IDPere = "RE"
                Else
                    FailMsg("Impossible de modifier " & OldID & "")
                    Exit Sub
                End If

                query = "UPDATE t_comp_rubriquest SET ID_RUBST='" & ID & "', LIBELLE_RUBST='" & limiter(EnleverApost(txtLibelleTER.Text.Trim()), 500) & "', ID_RUBENT='" & IDPere & "' WHERE ETAT_RUB='Tableau Emplois Ressources' AND ID_RUBST='" & OldID & "'"
                ExecuteNonQuery(query)
                If OldID <> ID Then
                    'On met à jour le nouveau ID
                    query = "UPDATE t_comp_rubrique SET ID_RUBST='" & ID & "' WHERE ID_RUBST='" & OldID & "' AND ETAT_RUB='Tableau Emplois Ressources'"
                    ExecuteNonQuery(query)
                End If
                LoadPageTER()
                SuccesMsg("Modification effectuée avec succès")
                btQuitElmt.PerformClick()
            Else
                FailMsg("Rien à modifier")
            End If
        Else
            'Cas d'ajout
            'Vérification de la disponibilité de l'ID
            Dim OldID As String = String.Empty
            Dim ID As String = txtIdTER.Text.ToUpper()
            If VerifID(OldID, ID) Then
                FailMsg("L'identifiant " & ID & " existe déjà.")
                txtIdTER.Select()
                Exit Sub
            End If

            Dim Type As String = String.Empty
            If cmbTypeTER.SelectedIndex = -1 Then
                FailMsg("Veuillez choisir un type.")
                cmbTypeTER.Select()
                Exit Sub
            End If
            Type = cmbTypeTER.Text
            Dim IDPere As String = String.Empty
            If cmbEnteteTER.Text = "EMPLOIS" Then
                IDPere = "EM"
            ElseIf cmbEnteteTER.Text = "RESSOURCES" Then
                IDPere = "RE"
            Else
                FailMsg("Impossible d'ajouter l'élément")
                Exit Sub
            End If
            If Type = "Détails" Then
                query = "INSERT INTO t_comp_rubrique VALUES('" & ID & "','Détails','" & limiter(EnleverApost(txtLibelleTER.Text.Trim()), 500) & "','Tableau Emplois Ressources','" & IDPere & "')"
                ExecuteNonQuery(query)
                LoadPageTER()
                ViderElmtTER()
                SuccesMsg("Enregistrement effectué avec succès")
                cmbEnteteTER.Select()
            ElseIf Type = "Sous Total" Then
                query = "INSERT INTO t_comp_rubriquest VALUES('" & ID & "','" & limiter(EnleverApost(txtLibelleTER.Text.Trim()), 500) & "','" & IDPere & "','Tableau Emplois Ressources','')"
                ExecuteNonQuery(query)
                LoadPageTER()
                ViderElmtTER()
                SuccesMsg("Enregistrement effectué avec succès")
                cmbEnteteTER.Select()
            Else
                FailMsg("Rien à ajouter")
            End If
        End If
    End Sub
    Private Function VerifID(OldID As String, ID As String) As Boolean
        query = "SELECT ID_RUBENT FROM t_comp_rubriqueent WHERE ID_RUBENT='" & ID & "' AND ETAT_RUB='Tableau Emplois Ressources'" 'Les entetes
        Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
        If dtVerif.Rows.Count <> 0 Then
            If OldID <> ID Then
                Return True
            End If
        End If

        query = "SELECT ID_RUBST FROM t_comp_rubriquest WHERE ID_RUBST='" & ID & "' AND ETAT_RUB='Tableau Emplois Ressources'" 'Les Sous-Totaux
        dtVerif = ExcecuteSelectQuery(query)
        If dtVerif.Rows.Count <> 0 Then
            If OldID <> ID Then
                Return True
            End If
        End If

        query = "SELECT CODE_RUB FROM t_comp_rubrique WHERE CODE_RUB='" & ID & "' AND ETAT_RUB='Tableau Emplois Ressources'" 'Les Détails
        dtVerif = ExcecuteSelectQuery(query)
        If dtVerif.Rows.Count <> 0 Then
            If OldID <> ID Then
                Return True
            End If
        End If
        Return False
    End Function
    Private Sub ViderElmtTER()
        cmbEnteteTER.ResetText()
        cmbTypeTER.ResetText()
        txtIdTER.ResetText()
        txtLibelleTER.ResetText()
    End Sub

    Private Sub btSaveTER_Click(sender As Object, e As EventArgs) Handles btSaveTER.Click
        If drxTER("Type") = "Détails" Then
            Dim IDRebrique As String = drxTER("Identifiant")

            query = "delete from T_COMP_TYPE_RUBRIQUE where code_rub='" & IDRebrique & "' AND ETAT_RUB='Tableau Emplois Ressources'"
            ExecuteNonQuery(query)

            For i = 0 To ViewCompteComptableTER.RowCount - 1
                query = "insert into T_COMP_TYPE_RUBRIQUE values('" & IDRebrique & "','" & ViewCompteComptableTER.GetRowCellValue(i, "N° Comptable").ToString & "','" & ViewCompteComptableTER.GetRowCellValue(i, "Condition").ToString & "','" & ViewCompteComptableTER.GetRowCellValue(i, "Type").ToString & "','Tableau Emplois Ressources')"
                ExecuteNonQuery(query)
            Next
            LoadPageTER()
            btQuitTER.PerformClick()
        End If
    End Sub

    Private Sub cmbSCTER_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSCTER.KeyPress, cmbConditionTER.KeyPress
        Try
            Select Case e.KeyChar
                Case ControlChars.CrLf
                    'Vérification des champ text
                    Dim erreur As String = ""

                    If cmbSCTER.SelectedIndex = -1 Then
                        erreur += "- Numéro comptable " & ControlChars.CrLf
                    End If

                    If cmbConditionTER.SelectedIndex = -1 Then
                        erreur += "- Condition" & ControlChars.CrLf
                    End If

                    If erreur = "" Then
                        Dim id As String()
                        id = cmbSCTER.Text.Split(" | ")
                        Dim CodeSC As String = id(0)

                        'On verifie dans la base de données, si un compte est partiellement utilise
                        query = "SELECT CODE_SC,Liens.CODE_RUB,Liens.condition FROM t_comp_type_rubrique Liens,t_comp_rubrique rb WHERE rb.CODE_RUB=Liens.CODE_RUB AND (Liens.ETAT_RUB='Tableau Emplois Ressources') AND CODE_SC LIKE '" & CodeSC & "%'"
                        Dim dt As DataTable = ExcecuteSelectQuery(query)
                        If dt.Rows.Count > 0 Then
                            Dim OldCondition = dt.Rows(0)("condition")
                            If OldCondition = "Les Deux" Or cmbConditionTER.Text = "Les Deux" Then
                                FailMsg("Le compte comptable : " & CodeSC & " est déjà paramétré partiellement sur " & dt.Rows(0)("CODE_RUB") & ".")
                                Exit Sub
                            ElseIf OldCondition = cmbConditionTER.Text Then
                                FailMsg("Le compte comptable : " & CodeSC & " est déjà paramétré partiellement sur " & dt.Rows(0)("CODE_RUB") & ".")
                                Exit Sub
                            End If
                        Else
                            Dim Code As String = String.Empty
                            For i = 1 To CodeSC.Length
                                Code += Mid(CodeSC, i, 1)
                                query = "SELECT CODE_SC,Liens.CODE_RUB,Liens.condition FROM t_comp_type_rubrique Liens,t_comp_rubrique rb WHERE rb.CODE_RUB=Liens.CODE_RUB AND (Liens.ETAT_RUB='Tableau Emplois Ressources') AND CODE_SC='" & Code & "'"
                                dt = ExcecuteSelectQuery(query)
                                If dt.Rows.Count > 0 Then
                                    Dim OldCondition = dt.Rows(0)("condition")
                                    If OldCondition = "Les Deux" Or cmbConditionTER.Text = "Les Deux" Then
                                        FailMsg("Le compte comptable : " & CodeSC & " est déjà paramétré partiellement sur " & dt.Rows(0)("CODE_RUB") & ".")
                                        Exit Sub
                                    ElseIf OldCondition = cmbConditionTER.Text Then
                                        FailMsg("Le compte comptable : " & CodeSC & " est déjà paramétré partiellement sur " & dt.Rows(0)("CODE_RUB") & ".")
                                        Exit Sub
                                    End If
                                End If
                            Next
                        End If

                        For i = 0 To ViewCompteComptableTER.RowCount - 1
                            If (CodeSC = ViewCompteComptableTER.GetRowCellValue(i, "N° Comptable")) Then
                                SuccesMsg(CodeSC & " déjà ajouté.")
                                Exit Sub
                            End If
                        Next

                        'On verifie dans la liste, si un compte est partiellement utilise
                        For i = 0 To ViewCompteComptableTER.RowCount - 1
                            'SuccesMsg(ViewCompteComptableTER.GetRowCellValue(i, "N° Comptable") & vbNewLine & CodeSC)
                            Dim Code As String = String.Empty
                            For j = 1 To ViewCompteComptableTER.GetRowCellValue(i, "N° Comptable").Length
                                Code += Mid(ViewCompteComptableTER.GetRowCellValue(i, "N° Comptable"), j, 1)
                                If Code = CodeSC Then
                                    FailMsg("Le compte comptable : " & CodeSC & " est déjà paramétré partiellement.")
                                    Exit Sub
                                End If
                            Next
                        Next

                        For i = 0 To ViewCompteComptableTER.RowCount - 1
                            Dim Code As String = String.Empty
                            For j = 1 To CodeSC.Length
                                Code += Mid(CodeSC, j, 1)
                                If Code = ViewCompteComptableTER.GetRowCellValue(i, "N° Comptable") Then
                                    FailMsg("Le compte comptable : " & CodeSC & " est déjà paramétré partiellement.")
                                    Exit Sub
                                End If
                            Next
                        Next

                        Dim drS = dtcompterattachTER.NewRow()
                        drS(0) = ""
                        drS(1) = CodeSC
                        drS(2) = GetLibelleCodeSC(CodeSC)
                        drS(3) = cmbConditionTER.Text
                        dtcompterattachTER.Rows.Add(drS)
                        LgListCompteRattachTER.DataSource = dtcompterattachTER

                        cmbConditionTER.ResetText()
                        cmbSCTER.ResetText()
                        cmbSCTER.Select()

                    Else
                        SuccesMsg("Veuillez remplir ces champs : " & ControlChars.CrLf + erreur)
                    End If
                Case Else
            End Select
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub ContextMenuComptesTER_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuComptesTER.Opening
        If ViewCompteComptableTER.RowCount = 0 Then
            e.Cancel = True
        Else
            Dim drx = ViewCompteComptableTER.GetDataRow(ViewCompteComptableTER.FocusedRowHandle)
            Dim NCOMP = drx("N° Comptable").ToString
            ColorRowGrid(ViewCompteComptableTER, "[N° Comptable]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewCompteComptableTER, "[N° Comptable]='" & NCOMP & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
        End If
    End Sub

    Private Sub LgListCompteRattachTER_Click(sender As Object, e As EventArgs) Handles LgListCompteRattachTER.Click
        If (ViewCompteComptableTER.RowCount > 0) Then
            Dim drx = ViewCompteComptableTER.GetDataRow(ViewCompteComptableTER.FocusedRowHandle)
            Dim NCOMP = drx("N° Comptable").ToString
            ColorRowGrid(ViewCompteComptableTER, "[N° Comptable]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewCompteComptableTER, "[N° Comptable]='" & NCOMP & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
        End If
    End Sub

    Private Sub btDelContextMenu_Click(sender As Object, e As EventArgs) Handles btDelContextMenu.Click
        Try
            If ViewCompteComptableTER.RowCount > 0 Then
                If ViewCompteComptableTER.FocusedRowHandle <> -1 Then
                    ViewCompteComptableTER.GetDataRow(ViewCompteComptableTER.FocusedRowHandle).Delete()
                End If
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub
#End Region
End Class