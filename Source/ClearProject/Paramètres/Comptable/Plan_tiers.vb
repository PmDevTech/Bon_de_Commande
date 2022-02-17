Imports System.Math
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Plan_tiers
    Dim dtcontact = New DataTable()
    Dim dtBanque = New DataTable()
    Dim dtcumuls = New DataTable()
    Private LesTiers As New TiersClass


    Private Sub Plan_tiers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        On Error Resume Next
        If TxtRechecher.Text <> "Rechercher" Then
            TxtRechecher.Text = "Rechercher"
        End If
        CmbPageSize.SelectedIndex = 0
        CmbPageSize.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor

        loadTiers()
        If Checktous.Checked Then Checktous.Checked = False

        ViewCptTiers.Columns(0).Visible = True
        ViewCptTiers.Columns(0).Width = 20
        ViewCptTiers.OptionsView.ColumnAutoWidth = True
        ViewCptTiers.OptionsBehavior.AutoExpandAllGroups = True
        ViewCptTiers.VertScrollVisibility = True
        ViewCptTiers.HorzScrollVisibility = True
        ViewCptTiers.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
        ViewCptTiers.Columns("Code").OptionsColumn.AllowEdit = False
        ViewCptTiers.Columns("Intitulé").OptionsColumn.AllowEdit = False
        ViewCptTiers.Columns("Abréviation").OptionsColumn.AllowEdit = False
        ViewCptTiers.Columns("Adresse").OptionsColumn.AllowEdit = False
        ViewCptTiers.Columns("Email").OptionsColumn.AllowEdit = False
        ViewCptTiers.Columns("Abréviation").Visible = False
        ViewCptTiers.OptionsCustomization.AllowSort = False
        ViewCptTiers.OptionsNavigation.AutoMoveRowFocus = False
        ViewCptTiers.OptionsCustomization.AllowColumnMoving = False
        ViewCptTiers.OptionsCustomization.AllowColumnResizing = False
    End Sub

    Private Sub AjoutPlanDeTiersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AjoutPlanDeTiersToolStripMenuItem.Click
        Dim NewTiers As New Creer_compte_tier
        NewTiers.Size = New Point(710, 450)
        Dialog_form(NewTiers)
    End Sub
    Private Sub loadTiers(Optional Page As Decimal = 1)

        If Page = 1 Then
           query= "select count(*) from t_comp_compte where CODE_PROJET='" & ProjetEnCours & "'"
            Dim nbre = ExecuteScallar(query)

            With LesTiers
                .PageSize = IIf(CmbPageSize.Text = "", 1, CmbPageSize.Text)
                .PageCount = nbre \ .PageSize
                If nbre Mod .PageSize <> 0 Then
                    .PageCount += 1
                End If
                TxtPage.Text = "Page 1" & "/" & LesTiers.PageCount
                .Resqlconno = 0
            End With
        End If

        With LesTiers
            .CurrentPage = Page
            .LoadPage(LgListCompteTier, .CurrentPage)
        End With

    End Sub
    Private Sub SuppressionCompteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SuppressionCompteToolStripMenuItem.Click
        Try
            If (ViewCptTiers.RowCount > 0) Then
                For i = 0 To ViewCptTiers.RowCount - 1
                    If CBool(ViewCptTiers.GetRowCellValue(i, "Choix")) = True Then
                        query = "select * from T_COMP_LIGNE_ECRITURE where CODE_CPT='" & ViewCptTiers.GetRowCellValue(i, "Code").ToString & "' AND (DEBIT_LE<>0 OR CREDIT_LE<>0)"
                        Dim dt = ExcecuteSelectQuery(query)
                        If dt.Rows.Count > 0 Then
                            SuccesMsg("Ce compte ne peut pas être supprimé." & vbNewLine & ViewCptTiers.GetRowCellValue(i, "Code").ToString & " utilisé dans la comptabilité")
                            Exit Sub
                        End If

                        query = "select * from t_comp_ordrepaiement where CODE_CPT='" & ViewCptTiers.GetRowCellValue(i, "Code").ToString & "'"
                        dt = ExcecuteSelectQuery(query)
                        If dt.Rows.Count > 0 Then
                            SuccesMsg("Ce compte ne peut pas être supprimé." & vbNewLine & ViewCptTiers.GetRowCellValue(i, "Code").ToString & " utilisé dans les ordres de paiement")
                            Exit Sub
                        End If

                        query = "select * from t_im_biens where CODE_CPT='" & ViewCptTiers.GetRowCellValue(i, "Code").ToString & "'"
                        dt = ExcecuteSelectQuery(query)
                        If dt.Rows.Count > 0 Then
                            SuccesMsg("Ce compte ne peut pas être supprimé." & vbNewLine & ViewCptTiers.GetRowCellValue(i, "Code").ToString & " utilisé dans les immobilisations")
                            Exit Sub
                        End If

                        query = "select * from t_pa_reparation where CODE_CPT='" & ViewCptTiers.GetRowCellValue(i, "Code").ToString & "'"
                        dt = ExcecuteSelectQuery(query)
                        If dt.Rows.Count > 0 Then
                            SuccesMsg("Ce compte ne peut pas être supprimé." & vbNewLine & ViewCptTiers.GetRowCellValue(i, "Code").ToString & " utilisé dans les immobilisations")
                            Exit Sub
                        End If

                    End If
                Next

                Dim cpte As Decimal = 0
                'Vérificationn des selections
                For i = 0 To ViewCptTiers.RowCount - 1
                    If CBool(ViewCptTiers.GetRowCellValue(i, "Choix")) = True Then
                        cpte += 1
                    End If
                Next
                If cpte = 0 Then
                    SuccesMsg("Veuillez cocher au moins un compte de tiers")
                    Exit Sub
                End If

                If ConfirmMsg("Voulez-vous vraiment supprimer?") = DialogResult.Yes Then
                    'Suppression des données 
                    For i = 0 To ViewCptTiers.RowCount - 1

                        If CBool(ViewCptTiers.GetRowCellValue(i, "Choix")) = True Then
                            cpte += 1
                            Dim CodeTiers As String = ViewCptTiers.GetRowCellValue(i, "Code").ToString()
                            ExecuteNonQuery("DELETE FROM t_comp_compte WHERE CODE_CPT='" & CodeTiers & "'")
                            ExecuteNonQuery("DELETE FROM report_cpt WHERE CODE_CPT='" & CodeTiers & "'")
                            ExecuteNonQuery("DELETE FROM t_comp_rattach_tiers WHERE CODE_CPT='" & CodeTiers & "'")
                            ExecuteNonQuery("DELETE FROM t_comp_identifiant WHERE CODE_CPT='" & CodeTiers & "'")
                            ExecuteNonQuery("DELETE FROM T_COMP_RATTACH_BANQ WHERE CODE_CPT='" & CodeTiers & "'")
                            Try
                                ExecuteNonQuery("DELETE FROM t_comp_banque WHERE CODE_CPT='" & CodeTiers & "'")
                            Catch ex As Exception
                            End Try
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
            If (ViewCptTiers.RowCount > 0) Then
                Dim cpte As Decimal = 0
                For i = 0 To ViewCptTiers.RowCount - 1
                    'If cpte >= 3 Then
                    '    Continue For
                    'End If
                    If CBool(ViewCptTiers.GetRowCellValue(i, "Choix")) = True Then
                        cpte += 1
                    End If
                Next
                Dim rep As New DialogResult
                Dim alert As Boolean = False
                If cpte > 3 Then
                    alert = True
                    rep = ConfirmCancelMsg("Vous avez sélectionné plus de 3 comptes." & vbNewLine & "Voulez-vous que le système modifie les 3 premiers comptes?")
                End If

                If alert Then
                    If rep = DialogResult.No Then
                        For i = 0 To ViewCptTiers.RowCount - 1
                            If CBool(ViewCptTiers.GetRowCellValue(i, "Choix")) = True Then
                                Dim ModifTiers As New Modif_PlanTiers
                                ActiverChamps5(ModifTiers.gcSaisieBanque)
                                ActiverChamps4(ModifTiers.PanelControl5)
                                EffacerTexBox5(ModifTiers.PanelControl5)
                                EffacerTexBox5(ModifTiers.PanelControl4)
                                EffacerTexBox1(ModifTiers.GroupBox1)
                                EffacerTexBox1(ModifTiers.GroupBox3)
                                EffacerTexBox1(ModifTiers.GroupBox4)

                                ActiverChamps4(ModifTiers.PanelControl4)
                                ActiverChamps10(ModifTiers.GroupBox1)
                                ActiverChamps10(ModifTiers.GroupBox3)
                                ActiverChamps10(ModifTiers.GroupBox4)

                                dtcumuls.Columns.clear()
                                dtcumuls.Columns.Add("Période", Type.GetType("System.String"))
                                dtcumuls.Columns.Add("Mouvement Débit", Type.GetType("System.String"))
                                dtcumuls.Columns.Add("Mouvement Crédit", Type.GetType("System.String"))
                                dtcumuls.Columns.Add("Solde Débit", Type.GetType("System.String"))
                                dtcumuls.Columns.Add("Solde Crédit", Type.GetType("System.String"))
                                dtcumuls.Rows.clear()

                                dtcontact.Columns.clear()
                                dtcontact.Columns.Add("Nom & Prénoms", Type.GetType("System.String"))
                                dtcontact.Columns.Add("Service", Type.GetType("System.String"))
                                dtcontact.Columns.Add("Fonction", Type.GetType("System.String"))
                                dtcontact.Columns.Add("Téléphone", Type.GetType("System.String"))
                                dtcontact.Columns.Add("Portable", Type.GetType("System.String"))
                                dtcontact.Columns.Add("Fax", Type.GetType("System.String"))
                                dtcontact.Columns.Add("Email", Type.GetType("System.String"))
                                dtcontact.Rows.clear()

                                dtBanque.Columns.clear()
                                dtBanque.Columns.Add("Banque", Type.GetType("System.String"))
                                dtBanque.Columns.Add("RIB", Type.GetType("System.String"))
                                dtBanque.Columns.Add("Devise", Type.GetType("System.String"))
                                dtBanque.Rows.clear()


                                query = "select t.libelle_tcpt,c.abrege_cpt,c.nom_cpt,c.adresse_cpt,c.telephone,c.email,c.rc,c.cc,c.telephone2,c.fax,c.site,c.qualite,c.code_cpt,t.Code_CL from t_comp_compte c, t_comp_type_compte t where t.code_tcpt=c.code_tcpt and c.code_cpt='" & ViewCptTiers.GetRowCellValue(i, "Code").ToString & "' and c.code_projet='" & ProjetEnCours & "'"
                                Dim dt = ExcecuteSelectQuery(query)
                                For Each rwx As DataRow In dt.Rows
                                    ModifTiers.txtCodeTiers.Text = ViewCptTiers.GetRowCellValue(i, "Code").ToString
                                    ModifTiers.CodeCptCahe.Text = ViewCptTiers.GetRowCellValue(i, "Code").ToString
                                    ModifTiers.cmbType.Text = rwx("Code_CL") & " | " & MettreApost(rwx("libelle_tcpt").ToString)
                                    ModifTiers.Txtabr.Text = rwx(1).ToString
                                    ModifTiers.txtIntitule.Text = MettreApost(rwx(2).ToString)
                                    ModifTiers.txtadresse.Text = MettreApost(rwx(3).ToString)
                                    ModifTiers.txtcontact.Text = rwx(4).ToString
                                    ModifTiers.txtmail.Text = MettreApost(rwx(5).ToString)
                                    ModifTiers.Txtrc.Text = rwx(6).ToString
                                    ModifTiers.Txtcc.Text = rwx(7).ToString
                                    ModifTiers.Txttel.Text = rwx(8).ToString
                                    ModifTiers.Txtfax.Text = rwx(9).ToString
                                    ModifTiers.Txtsite.Text = MettreApost(rwx(10).ToString)
                                    ModifTiers.Txtqualite.Text = rwx(11).ToString
                                Next



                                query = "select code_v FROM t_comp_compte WHERE code_cpt='" & ViewCptTiers.GetRowCellValue(i, "Code").ToString & "' and code_projet='" & ProjetEnCours & "'"
                                dt = ExcecuteSelectQuery(query)
                                For Each rwx As DataRow In dt.Rows
                                    ModifTiers.Combville.Text = MettreApost(rwx("code_v").ToString)
                                Next


                                query = "select sc.code_sc,sc.libelle_sc from t_comp_rattach_tiers rt, t_comp_sous_classe sc where rt.code_sc=sc.code_sc and rt.code_cpt='" & ViewCptTiers.GetRowCellValue(i, "Code").ToString & "' and rt.code_projet='" & ProjetEnCours & "'"
                                dt = ExcecuteSelectQuery(query)
                                For Each rwx As DataRow In dt.Rows
                                    ModifTiers.Combsc.Text = rwx(0).ToString & "   " & MettreApost(rwx(1).ToString)
                                Next

                                query = "select * from t_comp_identifiant where code_cpt='" & ViewCptTiers.GetRowCellValue(i, "Code").ToString & "' and codeprojet='" & ProjetEnCours & "'"
                                dt = ExcecuteSelectQuery(query)
                                For Each rwx As DataRow In dt.Rows
                                    Dim drS = dtcontact.NewRow()
                                    drS(0) = MettreApost(rwx(2).ToString)
                                    drS(1) = MettreApost(rwx(9).ToString)
                                    drS(2) = MettreApost(rwx(3).ToString)
                                    drS(3) = MettreApost(rwx(8).ToString)
                                    drS(4) = MettreApost(rwx(4).ToString)
                                    drS(5) = MettreApost(rwx(5).ToString)
                                    drS(6) = MettreApost(rwx(6).ToString)
                                    dtcontact.Rows.Add(drS)
                                Next
                                ModifTiers.LgListContact.DataSource = dtcontact

                                ModifTiers.ViewContact.Columns(0).Width = 150
                                ModifTiers.ViewContact.Columns(1).Width = 150
                                ModifTiers.ViewContact.Columns(2).Width = 100
                                ModifTiers.ViewContact.Columns(3).Width = 150
                                ModifTiers.ViewContact.Columns(4).Width = 150
                                ModifTiers.ViewContact.Columns(5).Width = 100
                                ModifTiers.ViewContact.Columns(6).Width = 100

                                query = "select * from t_comp_annee"
                                dt = ExcecuteSelectQuery(query)
                                For Each rwx As DataRow In dt.Rows

                                    'date
                                    Dim date_ecriture As String = ExerciceComptable.Rows(0).Item("datedebut")

                                    'query = "select datedebut, datefin from T_COMP_EXERCICE where Etat<>'2' and encours='1'"
                                    'dt = ExcecuteSelectQuery(query)
                                    'For Each rwx0 As DataRow In dt.Rows
                                    '    date_ecriture = rwx0(0).ToString
                                    'Next


                                    Dim datedeb As String = ""
                                    Dim datefin As String = ""

                                    If Len(rwx(3).ToString) = 1 Then
                                        datedeb = "01/0" & rwx(3).ToString & "/" & Format(CDate(date_ecriture), "yyyy")
                                        datefin = rwx(2).ToString & "/0" & rwx(3).ToString & "/" & Format(CDate(date_ecriture), "yyyy")
                                    ElseIf Len(rwx(3).ToString) = 2 Then
                                        datedeb = "01/" & rwx(3).ToString & "/" & Format(CDate(date_ecriture), "yyyy")
                                        datefin = rwx(2).ToString & "/" & rwx(3).ToString & "/" & Format(CDate(date_ecriture), "yyyy")
                                    End If


                                    Dim str(3) As String
                                    str = datedeb.ToString.Split("/")
                                    Dim tempdt As String = String.Empty
                                    For j As Integer = 2 To 0 Step -1
                                        tempdt += str(j) & "-"
                                    Next
                                    tempdt = tempdt.Substring(0, 10)

                                    Dim str1(3) As String
                                    str1 = datefin.ToString.Split("/")
                                    Dim tempdt1 As String = String.Empty
                                    For j As Integer = 2 To 0 Step -1
                                        tempdt1 += str1(j) & "-"
                                    Next
                                    tempdt1 = tempdt1.Substring(0, 10)


                                    query = "select sum(debit_le), sum(credit_le) from t_comp_ligne_ecriture where date_le>='" & tempdt & "' and date_le<='" & tempdt1 & "' and code_cpt='" & ViewCptTiers.GetRowCellValue(i, "Code").ToString & "' and code_projet='" & ProjetEnCours & "'"
                                    dt = ExcecuteSelectQuery(query)
                                    For Each rwx1 As DataRow In dt.Rows
                                        Dim cumuldeb As Decimal = 0
                                        Dim cumulcred As Decimal = 0
                                        Dim soldedeb As Decimal = 0
                                        Dim soldecred As Decimal = 0
                                        cumuldeb = IIf(rwx1(0).ToString = "", 0, rwx1(0))
                                        cumulcred = IIf(rwx1(1).ToString = "", 0, rwx1(1))
                                        Dim total As Decimal = 0
                                        total = cumuldeb - cumulcred

                                        If total < 0 Then
                                            Dim drS = dtcumuls.NewRow()
                                            drS(0) = rwx1(1).ToString & " " & Format(CDate(date_ecriture), "yy")
                                            drS(1) = AfficherMonnaie(cumuldeb)
                                            drS(2) = AfficherMonnaie(cumulcred)
                                            drS(3) = 0
                                            drS(4) = AfficherMonnaie(cumulcred - cumuldeb)
                                            dtcumuls.Rows.Add(drS)
                                        Else
                                            Dim drS = dtcumuls.NewRow()
                                            drS(0) = rwx1(1).ToString & " " & Format(CDate(date_ecriture), "yy")
                                            drS(1) = AfficherMonnaie(cumuldeb)
                                            drS(2) = AfficherMonnaie(cumulcred)
                                            drS(3) = AfficherMonnaie(cumuldeb - cumulcred)
                                            drS(4) = 0
                                            dtcumuls.Rows.Add(drS)
                                        End If

                                    Next
                                Next
                                ModifTiers.LgListCumul.DataSource = dtcumuls


                                ModifTiers.ViewCumul.Columns(0).Width = 110
                                ModifTiers.ViewCumul.Columns(1).Width = 140
                                ModifTiers.ViewCumul.Columns(2).Width = 140
                                ModifTiers.ViewCumul.Columns(3).Width = 140
                                ModifTiers.ViewCumul.Columns(4).Width = 140

                                ModifTiers.LabelControl34.Text = 0
                                ModifTiers.LabelControl35.Text = 0
                                ModifTiers.LabelControl36.Text = 0
                                ModifTiers.LabelControl37.Text = 0

                                For m = 0 To ModifTiers.ViewCumul.RowCount - 1
                                    ModifTiers.LabelControl34.Text = AfficherMonnaie(CInt(ModifTiers.LabelControl34.Text) + CInt(dtcumuls.rows(m).item(1)))
                                    ModifTiers.LabelControl35.Text = AfficherMonnaie(CInt(ModifTiers.LabelControl35.Text) + CInt(dtcumuls.rows(m).item(2)))
                                    ModifTiers.LabelControl36.Text = AfficherMonnaie(CInt(ModifTiers.LabelControl36.Text) + CInt(dtcumuls.rows(m).item(3)))
                                    ModifTiers.LabelControl37.Text = AfficherMonnaie(CInt(ModifTiers.LabelControl37.Text) + CInt(dtcumuls.rows(m).item(4)))
                                Next

                                ModifTiers.Size = New Point(710, 450)
                                Dialog_form(ModifTiers)
                            End If
                        Next
                    ElseIf rep = DialogResult.YES Then
                        cpte = 0
                        For i = 0 To ViewCptTiers.RowCount - 1
                            If cpte >= 3 Then
                                Exit For
                            End If
                            If CBool(ViewCptTiers.GetRowCellValue(i, "Choix")) = True Then
                                Dim ModifTiers As New Modif_PlanTiers
                                ActiverChamps5(ModifTiers.gcSaisieBanque)
                                ActiverChamps4(ModifTiers.PanelControl5)
                                EffacerTexBox5(ModifTiers.PanelControl5)
                                EffacerTexBox5(ModifTiers.PanelControl4)
                                EffacerTexBox1(ModifTiers.GroupBox1)
                                EffacerTexBox1(ModifTiers.GroupBox3)
                                EffacerTexBox1(ModifTiers.GroupBox4)

                                ActiverChamps4(ModifTiers.PanelControl4)
                                ActiverChamps10(ModifTiers.GroupBox1)
                                ActiverChamps10(ModifTiers.GroupBox3)
                                ActiverChamps10(ModifTiers.GroupBox4)

                                dtcumuls.Columns.clear()
                                dtcumuls.Columns.Add("Période", Type.GetType("System.String"))
                                dtcumuls.Columns.Add("Mouvement Débit", Type.GetType("System.String"))
                                dtcumuls.Columns.Add("Mouvement Crédit", Type.GetType("System.String"))
                                dtcumuls.Columns.Add("Solde Débit", Type.GetType("System.String"))
                                dtcumuls.Columns.Add("Solde Crédit", Type.GetType("System.String"))
                                dtcumuls.Rows.clear()

                                dtcontact.Columns.clear()
                                dtcontact.Columns.Add("Nom & Prénoms", Type.GetType("System.String"))
                                dtcontact.Columns.Add("Service", Type.GetType("System.String"))
                                dtcontact.Columns.Add("Fonction", Type.GetType("System.String"))
                                dtcontact.Columns.Add("Téléphone", Type.GetType("System.String"))
                                dtcontact.Columns.Add("Portable", Type.GetType("System.String"))
                                dtcontact.Columns.Add("Fax", Type.GetType("System.String"))
                                dtcontact.Columns.Add("Email", Type.GetType("System.String"))
                                dtcontact.Rows.clear()

                                dtBanque.Columns.clear()
                                dtBanque.Columns.Add("Banque", Type.GetType("System.String"))
                                dtBanque.Columns.Add("RIB", Type.GetType("System.String"))
                                dtBanque.Columns.Add("Devise", Type.GetType("System.String"))
                                dtBanque.Rows.clear()


                                query = "select t.libelle_tcpt,c.abrege_cpt,c.nom_cpt,c.adresse_cpt,c.telephone,c.email,c.rc,c.cc,c.telephone2,c.fax,c.site,c.qualite,c.code_cpt, t.Code_CL from t_comp_compte c, t_comp_type_compte t where t.code_tcpt=c.code_tcpt and c.code_cpt='" & ViewCptTiers.GetRowCellValue(i, "Code").ToString & "' and c.code_projet='" & ProjetEnCours & "'"
                                Dim dt = ExcecuteSelectQuery(query)
                                For Each rwx As DataRow In dt.Rows
                                    ModifTiers.txtCodeTiers.Text = ViewCptTiers.GetRowCellValue(i, "Code").ToString
                                    ModifTiers.CodeCptCahe.Text = ViewCptTiers.GetRowCellValue(i, "Code").ToString
                                    ModifTiers.cmbType.Text = rwx("Code_CL") & " | " & MettreApost(rwx("libelle_tcpt").ToString)
                                    ModifTiers.Txtabr.Text = rwx(1).ToString
                                    ModifTiers.txtIntitule.Text = MettreApost(rwx(2).ToString)
                                    ModifTiers.txtadresse.Text = MettreApost(rwx(3).ToString)
                                    ModifTiers.txtcontact.Text = rwx(4).ToString
                                    ModifTiers.txtmail.Text = MettreApost(rwx(5).ToString)
                                    ModifTiers.Txtrc.Text = rwx(6).ToString
                                    ModifTiers.Txtcc.Text = rwx(7).ToString
                                    ModifTiers.Txttel.Text = rwx(8).ToString
                                    ModifTiers.Txtfax.Text = rwx(9).ToString
                                    ModifTiers.Txtsite.Text = MettreApost(rwx(10).ToString)
                                    ModifTiers.Txtqualite.Text = rwx(11).ToString
                                Next



                                query = "select code_v FROM t_comp_compte WHERE code_cpt='" & ViewCptTiers.GetRowCellValue(i, "Code").ToString & "' and code_projet='" & ProjetEnCours & "'"
                                dt = ExcecuteSelectQuery(query)
                                For Each rwx As DataRow In dt.Rows
                                    ModifTiers.Combville.Text = MettreApost(rwx("code_v").ToString)
                                Next




                                query = "select sc.code_sc,sc.libelle_sc from t_comp_rattach_tiers rt, t_comp_sous_classe sc where rt.code_sc=sc.code_sc and rt.code_cpt='" & ViewCptTiers.GetRowCellValue(i, "Code").ToString & "' and rt.code_projet='" & ProjetEnCours & "'"
                                dt = ExcecuteSelectQuery(query)
                                For Each rwx As DataRow In dt.Rows
                                    ModifTiers.Combsc.Text = rwx(0).ToString & "   " & MettreApost(rwx(1).ToString)
                                Next



                                query = "select * from t_comp_identifiant where code_cpt='" & ViewCptTiers.GetRowCellValue(i, "Code").ToString & "' and codeprojet='" & ProjetEnCours & "'"
                                dt = ExcecuteSelectQuery(query)
                                For Each rwx As DataRow In dt.Rows
                                    Dim drS = dtcontact.NewRow()
                                    drS(0) = MettreApost(rwx(2).ToString)
                                    drS(1) = MettreApost(rwx(9).ToString)
                                    drS(2) = MettreApost(rwx(3).ToString)
                                    drS(3) = MettreApost(rwx(8).ToString)
                                    drS(4) = MettreApost(rwx(4).ToString)
                                    drS(5) = MettreApost(rwx(5).ToString)
                                    drS(6) = MettreApost(rwx(6).ToString)
                                    dtcontact.Rows.Add(drS)
                                Next
                                ModifTiers.LgListContact.DataSource = dtcontact


                                ModifTiers.ViewContact.Columns(0).Width = 150
                                ModifTiers.ViewContact.Columns(1).Width = 150
                                ModifTiers.ViewContact.Columns(2).Width = 100
                                ModifTiers.ViewContact.Columns(3).Width = 150
                                ModifTiers.ViewContact.Columns(4).Width = 150
                                ModifTiers.ViewContact.Columns(5).Width = 100
                                ModifTiers.ViewContact.Columns(6).Width = 100


                                query = "select * from t_comp_annee"
                                dt = ExcecuteSelectQuery(query)
                                For Each rwx As DataRow In dt.Rows

                                    'date
                                    Dim date_ecriture As String = ExerciceComptable.Rows(0).Item("datedebut")

                                    'query = "select datedebut, datefin from T_COMP_EXERCICE where Etat<>'2' and encours='1'"
                                    'dt = ExcecuteSelectQuery(query)
                                    'For Each rwx0 As DataRow In dt.Rows
                                    '    date_ecriture = rwx0(0).ToString
                                    'Next


                                    Dim datedeb As String = ""
                                    Dim datefin As String = ""

                                    If Len(rwx(3).ToString) = 1 Then
                                        datedeb = "01/0" & rwx(3).ToString & "/" & Format(CDate(date_ecriture), "yyyy")
                                        datefin = rwx(2).ToString & "/0" & rwx(3).ToString & "/" & Format(CDate(date_ecriture), "yyyy")
                                    ElseIf Len(rwx(3).ToString) = 2 Then
                                        datedeb = "01/" & rwx(3).ToString & "/" & Format(CDate(date_ecriture), "yyyy")
                                        datefin = rwx(2).ToString & "/" & rwx(3).ToString & "/" & Format(CDate(date_ecriture), "yyyy")
                                    End If


                                    Dim str(3) As String
                                    str = datedeb.ToString.Split("/")
                                    Dim tempdt As String = String.Empty
                                    For j As Integer = 2 To 0 Step -1
                                        tempdt += str(j) & "-"
                                    Next
                                    tempdt = tempdt.Substring(0, 10)

                                    Dim str1(3) As String
                                    str1 = datefin.ToString.Split("/")
                                    Dim tempdt1 As String = String.Empty
                                    For j As Integer = 2 To 0 Step -1
                                        tempdt1 += str1(j) & "-"
                                    Next
                                    tempdt1 = tempdt1.Substring(0, 10)


                                    query = "select sum(debit_le), sum(credit_le) from t_comp_ligne_ecriture where date_le>='" & tempdt & "' and date_le<='" & tempdt1 & "' and code_cpt='" & ViewCptTiers.GetRowCellValue(i, "Code").ToString & "' and code_projet='" & ProjetEnCours & "'"
                                    dt = ExcecuteSelectQuery(query)
                                    For Each rwx1 As DataRow In dt.Rows
                                        Dim cumuldeb As Decimal = 0
                                        Dim cumulcred As Decimal = 0
                                        Dim soldedeb As Decimal = 0
                                        Dim soldecred As Decimal = 0
                                        cumuldeb = IIf(rwx1(0).ToString = "", 0, rwx1(0))
                                        cumulcred = IIf(rwx1(1).ToString = "", 0, rwx1(1))
                                        Dim total As Decimal = 0
                                        total = cumuldeb - cumulcred

                                        If total < 0 Then
                                            Dim drS = dtcumuls.NewRow()
                                            drS(0) = rwx1(1).ToString & " " & Format(CDate(date_ecriture), "yy")
                                            drS(1) = AfficherMonnaie(cumuldeb)
                                            drS(2) = AfficherMonnaie(cumulcred)
                                            drS(3) = 0
                                            drS(4) = AfficherMonnaie(cumulcred - cumuldeb)
                                            dtcumuls.Rows.Add(drS)
                                        Else
                                            Dim drS = dtcumuls.NewRow()
                                            drS(0) = rwx1(1).ToString & " " & Format(CDate(date_ecriture), "yy")
                                            drS(1) = AfficherMonnaie(cumuldeb)
                                            drS(2) = AfficherMonnaie(cumulcred)
                                            drS(3) = AfficherMonnaie(cumuldeb - cumulcred)
                                            drS(4) = 0
                                            dtcumuls.Rows.Add(drS)
                                        End If

                                    Next
                                Next
                                ModifTiers.LgListCumul.DataSource = dtcumuls


                                ModifTiers.ViewCumul.Columns(0).Width = 110
                                ModifTiers.ViewCumul.Columns(1).Width = 140
                                ModifTiers.ViewCumul.Columns(2).Width = 140
                                ModifTiers.ViewCumul.Columns(3).Width = 140
                                ModifTiers.ViewCumul.Columns(4).Width = 140

                                ModifTiers.LabelControl34.Text = 0
                                ModifTiers.LabelControl35.Text = 0
                                ModifTiers.LabelControl36.Text = 0
                                ModifTiers.LabelControl37.Text = 0

                                For m = 0 To ModifTiers.ViewCumul.RowCount - 1
                                    ModifTiers.LabelControl34.Text = AfficherMonnaie(CInt(ModifTiers.LabelControl34.Text) + CInt(dtcumuls.rows(m).item(1)))
                                    ModifTiers.LabelControl35.Text = AfficherMonnaie(CInt(ModifTiers.LabelControl35.Text) + CInt(dtcumuls.rows(m).item(2)))
                                    ModifTiers.LabelControl36.Text = AfficherMonnaie(CInt(ModifTiers.LabelControl36.Text) + CInt(dtcumuls.rows(m).item(3)))
                                    ModifTiers.LabelControl37.Text = AfficherMonnaie(CInt(ModifTiers.LabelControl37.Text) + CInt(dtcumuls.rows(m).item(4)))
                                Next

                                ModifTiers.Size = New Point(710, 450)
                                Dialog_form(ModifTiers)
                                cpte += 1
                            End If
                        Next
                    End If
                Else
                    For i = 0 To ViewCptTiers.RowCount - 1
                        If CBool(ViewCptTiers.GetRowCellValue(i, "Choix")) = True Then
                            Dim ModifTiers As New Modif_PlanTiers
                            ActiverChamps5(ModifTiers.gcSaisieBanque)
                            ActiverChamps4(ModifTiers.PanelControl5)
                            EffacerTexBox5(ModifTiers.PanelControl5)
                            EffacerTexBox5(ModifTiers.PanelControl4)
                            EffacerTexBox1(ModifTiers.GroupBox1)
                            EffacerTexBox1(ModifTiers.GroupBox3)
                            EffacerTexBox1(ModifTiers.GroupBox4)

                            ActiverChamps4(ModifTiers.PanelControl4)
                            ActiverChamps10(ModifTiers.GroupBox1)
                            ActiverChamps10(ModifTiers.GroupBox3)
                            ActiverChamps10(ModifTiers.GroupBox4)

                            dtcumuls.Columns.clear()
                            dtcumuls.Columns.Add("Période", Type.GetType("System.String"))
                            dtcumuls.Columns.Add("Mouvement Débit", Type.GetType("System.String"))
                            dtcumuls.Columns.Add("Mouvement Crédit", Type.GetType("System.String"))
                            dtcumuls.Columns.Add("Solde Débit", Type.GetType("System.String"))
                            dtcumuls.Columns.Add("Solde Crédit", Type.GetType("System.String"))
                            dtcumuls.Rows.clear()

                            dtcontact.Columns.clear()
                            dtcontact.Columns.Add("Nom & Prénoms", Type.GetType("System.String"))
                            dtcontact.Columns.Add("Service", Type.GetType("System.String"))
                            dtcontact.Columns.Add("Fonction", Type.GetType("System.String"))
                            dtcontact.Columns.Add("Téléphone", Type.GetType("System.String"))
                            dtcontact.Columns.Add("Portable", Type.GetType("System.String"))
                            dtcontact.Columns.Add("Fax", Type.GetType("System.String"))
                            dtcontact.Columns.Add("Email", Type.GetType("System.String"))
                            dtcontact.Rows.clear()

                            dtBanque.Columns.clear()
                            dtBanque.Columns.Add("Banque", Type.GetType("System.String"))
                            dtBanque.Columns.Add("RIB", Type.GetType("System.String"))
                            dtBanque.Columns.Add("Devise", Type.GetType("System.String"))
                            dtBanque.Rows.clear()


                            query = "select t.libelle_tcpt,c.abrege_cpt,c.nom_cpt,c.adresse_cpt,c.telephone,c.email,c.rc,c.cc,c.telephone2,c.fax,c.site,c.qualite,c.code_cpt,t.Code_CL from t_comp_compte c, t_comp_type_compte t where t.code_tcpt=c.code_tcpt and c.code_cpt='" & ViewCptTiers.GetRowCellValue(i, "Code").ToString & "' and c.code_projet='" & ProjetEnCours & "'"
                            Dim dt = ExcecuteSelectQuery(query)
                            For Each rwx As DataRow In dt.Rows
                                ModifTiers.txtCodeTiers.Text = ViewCptTiers.GetRowCellValue(i, "Code").ToString
                                ModifTiers.CodeCptCahe.Text = ViewCptTiers.GetRowCellValue(i, "Code").ToString
                                ModifTiers.cmbType.Text = rwx("Code_CL") & " | " & MettreApost(rwx("libelle_tcpt").ToString)
                                ModifTiers.Txtabr.Text = rwx(1).ToString
                                ModifTiers.txtIntitule.Text = MettreApost(rwx(2).ToString)
                                ModifTiers.txtadresse.Text = MettreApost(rwx(3).ToString)
                                ModifTiers.txtcontact.Text = rwx(4).ToString
                                ModifTiers.txtmail.Text = MettreApost(rwx(5).ToString)
                                ModifTiers.Txtrc.Text = rwx(6).ToString
                                ModifTiers.Txtcc.Text = rwx(7).ToString
                                ModifTiers.Txttel.Text = rwx(8).ToString
                                ModifTiers.Txtfax.Text = rwx(9).ToString
                                ModifTiers.Txtsite.Text = MettreApost(rwx(10).ToString)
                                ModifTiers.Txtqualite.Text = rwx(11).ToString
                            Next

                            query = "select code_v FROM t_comp_compte WHERE code_cpt='" & ViewCptTiers.GetRowCellValue(i, "Code").ToString & "' and code_projet='" & ProjetEnCours & "'"
                            dt = ExcecuteSelectQuery(query)
                            For Each rwx As DataRow In dt.Rows
                                ModifTiers.Combville.Text = MettreApost(rwx("code_v").ToString)
                            Next

                            query = "select sc.code_sc,sc.libelle_sc from t_comp_rattach_tiers rt, t_comp_sous_classe sc where rt.code_sc=sc.code_sc and rt.code_cpt='" & ViewCptTiers.GetRowCellValue(i, "Code").ToString & "' and rt.code_projet='" & ProjetEnCours & "'"
                            dt = ExcecuteSelectQuery(query)
                            For Each rwx As DataRow In dt.Rows
                                ModifTiers.Combsc.Text = rwx(0).ToString & "   " & MettreApost(rwx(1).ToString)
                            Next

                            query = "select * from t_comp_identifiant where code_cpt='" & ViewCptTiers.GetRowCellValue(i, "Code").ToString & "' and codeprojet='" & ProjetEnCours & "'"
                            dt = ExcecuteSelectQuery(query)
                            For Each rwx As DataRow In dt.Rows
                                Dim drS = dtcontact.NewRow()
                                drS(0) = MettreApost(rwx(2).ToString)
                                drS(1) = MettreApost(rwx(9).ToString)
                                drS(2) = MettreApost(rwx(3).ToString)
                                drS(3) = MettreApost(rwx(8).ToString)
                                drS(4) = MettreApost(rwx(4).ToString)
                                drS(5) = MettreApost(rwx(5).ToString)
                                drS(6) = MettreApost(rwx(6).ToString)
                                dtcontact.Rows.Add(drS)
                            Next
                            ModifTiers.LgListContact.DataSource = dtcontact


                            ModifTiers.ViewContact.Columns(0).Width = 150
                            ModifTiers.ViewContact.Columns(1).Width = 150
                            ModifTiers.ViewContact.Columns(2).Width = 100
                            ModifTiers.ViewContact.Columns(3).Width = 150
                            ModifTiers.ViewContact.Columns(4).Width = 150
                            ModifTiers.ViewContact.Columns(5).Width = 100
                            ModifTiers.ViewContact.Columns(6).Width = 100

                            query = "select * from t_comp_annee"
                            dt = ExcecuteSelectQuery(query)
                            For Each rwx As DataRow In dt.Rows

                                'date
                                Dim date_ecriture As String = ExerciceComptable.Rows(0).Item("datedebut")

                                'query = "select datedebut, datefin from T_COMP_EXERCICE where Etat<>'2' and encours='1'"
                                'dt = ExcecuteSelectQuery(query)
                                'For Each rwx0 As DataRow In dt.Rows
                                '    date_ecriture = rwx0(0).ToString
                                'Next


                                Dim datedeb As String = ""
                                Dim datefin As String = ""

                                If Len(rwx(3).ToString) = 1 Then
                                    datedeb = "01/0" & rwx(3).ToString & "/" & Format(CDate(date_ecriture), "yyyy")
                                    datefin = rwx(2).ToString & "/0" & rwx(3).ToString & "/" & Format(CDate(date_ecriture), "yyyy")
                                ElseIf Len(rwx(3).ToString) = 2 Then
                                    datedeb = "01/" & rwx(3).ToString & "/" & Format(CDate(date_ecriture), "yyyy")
                                    datefin = rwx(2).ToString & "/" & rwx(3).ToString & "/" & Format(CDate(date_ecriture), "yyyy")
                                End If


                                Dim str(3) As String
                                str = datedeb.ToString.Split("/")
                                Dim tempdt As String = String.Empty
                                For j As Integer = 2 To 0 Step -1
                                    tempdt += str(j) & "-"
                                Next
                                tempdt = tempdt.Substring(0, 10)

                                Dim str1(3) As String
                                str1 = datefin.ToString.Split("/")
                                Dim tempdt1 As String = String.Empty
                                For j As Integer = 2 To 0 Step -1
                                    tempdt1 += str1(j) & "-"
                                Next
                                tempdt1 = tempdt1.Substring(0, 10)


                                query = "select sum(debit_le), sum(credit_le) from t_comp_ligne_ecriture where date_le>='" & tempdt & "' and date_le<='" & tempdt1 & "' and code_cpt='" & ViewCptTiers.GetRowCellValue(i, "Code").ToString & "' and code_projet='" & ProjetEnCours & "'"
                                dt = ExcecuteSelectQuery(query)
                                For Each rwx1 As DataRow In dt.Rows
                                    Dim cumuldeb As Decimal = 0
                                    Dim cumulcred As Decimal = 0
                                    Dim soldedeb As Decimal = 0
                                    Dim soldecred As Decimal = 0
                                    cumuldeb = IIf(rwx1(0).ToString = "", 0, rwx1(0))
                                    cumulcred = IIf(rwx1(1).ToString = "", 0, rwx1(1))
                                    Dim total As Decimal = 0
                                    total = cumuldeb - cumulcred

                                    If total < 0 Then
                                        Dim drS = dtcumuls.NewRow()
                                        drS(0) = rwx1(1).ToString & " " & Format(CDate(date_ecriture), "yy")
                                        drS(1) = AfficherMonnaie(cumuldeb)
                                        drS(2) = AfficherMonnaie(cumulcred)
                                        drS(3) = 0
                                        drS(4) = AfficherMonnaie(cumulcred - cumuldeb)
                                        dtcumuls.Rows.Add(drS)
                                    Else
                                        Dim drS = dtcumuls.NewRow()
                                        drS(0) = rwx1(1).ToString & " " & Format(CDate(date_ecriture), "yy")
                                        drS(1) = AfficherMonnaie(cumuldeb)
                                        drS(2) = AfficherMonnaie(cumulcred)
                                        drS(3) = AfficherMonnaie(cumuldeb - cumulcred)
                                        drS(4) = 0
                                        dtcumuls.Rows.Add(drS)
                                    End If

                                Next
                            Next
                            ModifTiers.LgListCumul.DataSource = dtcumuls


                            ModifTiers.ViewCumul.Columns(0).Width = 110
                            ModifTiers.ViewCumul.Columns(1).Width = 140
                            ModifTiers.ViewCumul.Columns(2).Width = 140
                            ModifTiers.ViewCumul.Columns(3).Width = 140
                            ModifTiers.ViewCumul.Columns(4).Width = 140

                            ModifTiers.LabelControl34.Text = 0
                            ModifTiers.LabelControl35.Text = 0
                            ModifTiers.LabelControl36.Text = 0
                            ModifTiers.LabelControl37.Text = 0

                            For m = 0 To ModifTiers.ViewCumul.RowCount - 1
                                ModifTiers.LabelControl34.Text = AfficherMonnaie(CInt(ModifTiers.LabelControl34.Text) + CInt(dtcumuls.rows(m).item(1)))
                                ModifTiers.LabelControl35.Text = AfficherMonnaie(CInt(ModifTiers.LabelControl35.Text) + CInt(dtcumuls.rows(m).item(2)))
                                ModifTiers.LabelControl36.Text = AfficherMonnaie(CInt(ModifTiers.LabelControl36.Text) + CInt(dtcumuls.rows(m).item(3)))
                                ModifTiers.LabelControl37.Text = AfficherMonnaie(CInt(ModifTiers.LabelControl37.Text) + CInt(dtcumuls.rows(m).item(4)))
                            Next

                            ModifTiers.Size = New Point(710, 450)
                            ModifTiers.ShowDialog()
                        End If
                    Next
                End If

            End If

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub ImprimerCompteDeTiersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImprimerCompteDeTiersToolStripMenuItem.Click
        Try
            If Not Access_Btn("BtnPrintLstTiers") Then
                Exit Sub
            End If

           query= "select count(*) from t_comp_compte"
            Dim nbre = ExecuteScallar(query)
            If nbre = 0 Then
                SuccesMsg("Aucune donnée à imprimer")
            Else
                Dim Plantier As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table

                Dim Chemin As String = lineEtat & "\Comptabilite\"

                Dim DatSet = New DataSet
                Plantier.Load(Chemin & "Plantiers.rpt")

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = Plantier.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                Plantier.SetDataSource(DatSet)
                Plantier.SetParameterValue("CodeProjet", ProjetEnCours)
                FullScreenReport.FullView.ReportSource = Plantier

                FinChargement()
                FullScreenReport.ShowDialog()
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub BtNext_Click(sender As System.Object, e As System.EventArgs) Handles BtNext.Click
        If TxtRechecher.Text <> "" And TxtRechecher.Text <> "Rechercher" Then
            If (LesTiers.CurrentPage < LesTiers.PageCount) Then
                LesTiers.CurrentPage = LesTiers.CurrentPage + 1
                TxtPage.Text = "Page " & LesTiers.CurrentPage & "/" & LesTiers.PageCount
                LesTiers.RechPage(LgListCompteTier, LesTiers.CurrentPage)
                If Checktous.Checked Then Checktous.Checked = False
            End If
        Else
            If (LesTiers.CurrentPage < LesTiers.PageCount) Then
                LesTiers.CurrentPage = LesTiers.CurrentPage + 1
                TxtPage.Text = "Page " & LesTiers.CurrentPage & "/" & LesTiers.PageCount
                LesTiers.LoadPage(LgListCompteTier, LesTiers.CurrentPage)
                If Checktous.Checked Then Checktous.Checked = False
            End If
        End If
    End Sub

    Private Sub BtLast_Click(sender As System.Object, e As System.EventArgs) Handles BtLast.Click
        If LesTiers.CurrentPage < LesTiers.PageCount Then
            If TxtRechecher.Text <> "" And TxtRechecher.Text <> "Rechercher" Then
                LesTiers.CurrentPage = LesTiers.PageCount
                TxtPage.Text = "Page " & LesTiers.CurrentPage & "/" & LesTiers.PageCount
                LesTiers.RechPage(LgListCompteTier, LesTiers.CurrentPage)
                If Checktous.Checked Then Checktous.Checked = False
            Else
                LesTiers.CurrentPage = LesTiers.PageCount
                TxtPage.Text = "Page " & LesTiers.CurrentPage & "/" & LesTiers.PageCount
                If LesTiers.PageCount > 0 Then
                    LesTiers.LoadPage(LgListCompteTier, LesTiers.CurrentPage)
                End If
                If Checktous.Checked Then Checktous.Checked = False
            End If
        End If
    End Sub

    Private Sub BtPrev_Click(sender As System.Object, e As System.EventArgs) Handles BtPrev.Click
        If TxtRechecher.Text <> "" And TxtRechecher.Text <> "Rechercher" Then
            If (LesTiers.CurrentPage > 1) Then
                LesTiers.CurrentPage = LesTiers.CurrentPage - 1
                TxtPage.Text = "Page " & LesTiers.CurrentPage & "/" & LesTiers.PageCount
                LesTiers.RechPage(LgListCompteTier, LesTiers.CurrentPage)
                If Checktous.Checked Then Checktous.Checked = False
            End If
        Else
            If (LesTiers.CurrentPage > 1) Then
                LesTiers.CurrentPage = LesTiers.CurrentPage - 1
                TxtPage.Text = "Page " & LesTiers.CurrentPage & "/" & LesTiers.PageCount
                LesTiers.LoadPage(LgListCompteTier, LesTiers.CurrentPage)
                If Checktous.Checked Then Checktous.Checked = False
            End If
        End If

    End Sub

    Private Sub BtFrist_Click(sender As System.Object, e As System.EventArgs) Handles BtFrist.Click
        If LesTiers.CurrentPage > 1 Then
            If TxtRechecher.Text <> "" And TxtRechecher.Text <> "Rechercher" Then
                LesTiers.CurrentPage = 1
                TxtPage.Text = "Page " & LesTiers.CurrentPage & "/" & LesTiers.PageCount
                LesTiers.RechPage(LgListCompteTier, LesTiers.CurrentPage)
                If Checktous.Checked Then Checktous.Checked = False
            Else
                LesTiers.CurrentPage = 1
                TxtPage.Text = "Page " & LesTiers.CurrentPage & "/" & LesTiers.PageCount
                LesTiers.LoadPage(LgListCompteTier, LesTiers.CurrentPage)
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
        Plan_tiers_Load(Me, e)
    End Sub

    Private Sub BtImprimer_Click(sender As System.Object, e As System.EventArgs) Handles BtImprimer.Click
        ImprimerCompteDeTiersToolStripMenuItem_Click(Me, e)
    End Sub

    Private Sub Checktous_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Checktous.CheckedChanged
        Try
            If (ViewCptTiers.RowCount > 0) Then
                If Checktous.Checked = True Then
                    For k As Integer = 0 To ViewCptTiers.RowCount - 1
                        ViewCptTiers.SetRowCellValue(k, "Choix", True)
                    Next
                Else
                    For k As Integer = 0 To ViewCptTiers.RowCount - 1
                        ViewCptTiers.SetRowCellValue(k, "Choix", False)
                    Next
                End If
                'For k As Integer = 0 To ViewCptTiers.RowCount - 1
                '    TabTrue(k) = Checktous.Checked
                'Next

                'If (Checktous.Checked = True) Then
                '    nbTab = ViewCptTiers.RowCount
                'Else
                '    nbTab = 0
                'End If

                'Plan_tiers_Load(Me, e)
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
                loadTiers()
            Else
                LesTiers.CurrentPage = 1
                LesTiers.RechPage(LgListCompteTier, LesTiers.CurrentPage)
                TxtPage.Text = LesTiers.CurrentPage & "/" & LesTiers.PageCount
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
            With LesTiers
                .PageSize = ElementNumber
            End With
            If TxtRechecher.Text = "" Or TxtRechecher.Text = "Rechercher" Then
                loadTiers(1)
            Else
                LesTiers.CurrentPage = 1
                TxtPage.Text = LesTiers.CurrentPage & "/" & LesTiers.PageCount
                LesTiers.RechPage(LgListCompteTier, LesTiers.CurrentPage)
            End If
        Else
            CmbPageSize.SelectedIndex = 0
        End If
        If Checktous.Checked Then Checktous.Checked = False
    End Sub

End Class