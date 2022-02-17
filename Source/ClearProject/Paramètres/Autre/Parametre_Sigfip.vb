Public Class Parametre_Sigfip
    Dim dtcompteSigfiprattach = New DataTable

    Private Sub Combsc2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Combsc2.KeyPress
        Try
            Select Case e.KeyChar
                Case ControlChars.CrLf
                    'vérification des champ text
                    Dim erreur As String = ""

                    If Combsc.Text = "" Then
                        erreur += "- Code Sous-Classe " & ControlChars.CrLf
                    End If
                    If Combsc2.Text = "" Then
                        erreur += "- Code Sous-Classe" & ControlChars.CrLf
                    End If

                    If erreur = "" Then
                        Dim id, id2 As String()
                        id = Combsc.Text.Split("  ")
                        id2 = Combsc2.Text.Split("  ")

                        If (id(0).ToString.Length = 2 And id2(0).ToString.Length = 2) Then

                            query = "select t_comp_sous_classe.code_sc,t_comp_sous_classe.libelle_sc from t_comp_sous_classe, t_comp_classe where t_comp_sous_classe.code_cl=t_comp_classe.code_cl and t_comp_classe.code_cl  between '" & id(0).ToString & "' and '" & id2(0).ToString & "'"
                            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                            For Each rw As DataRow In dt0.Rows
                                If ViewCptSigfiprattach.RowCount > 0 Then
                                    Dim trouv As Boolean = False
                                    For i = 0 To ViewCptSigfiprattach.RowCount - 1
                                        Select Case dtcompteSigfiprattach.Rows(i).Item(0).ToString
                                            Case rw(0).ToString
                                                trouv = True
                                                Exit For
                                        End Select

                                    Next
                                    If Not trouv Then
                                        Dim drS = dtcompteSigfiprattach.NewRow()
                                        drS(0) = rw(0).ToString
                                        drS(1) = MettreApost(rw(1).ToString)
                                        dtcompteSigfiprattach.Rows.Add(drS)
                                    End If
                                Else
                                    Dim drS = dtcompteSigfiprattach.NewRow()
                                    drS(0) = rw(0).ToString
                                    drS(1) = MettreApost(rw(1).ToString)
                                    dtcompteSigfiprattach.Rows.Add(drS)
                                End If

                            Next
                            LgListCompteSigfiprattach.DataSource = dtcompteSigfiprattach
                            ViewCptSigfiprattach.Columns(0).Width = 100
                            ViewCptSigfiprattach.Columns(1).Width = 250

                        End If

                        If (id(0).ToString.Length = 3 And id2(0).ToString.Length = 3) Then

                            query = "select t_comp_sous_classe.code_sc,t_comp_sous_classe.libelle_sc from t_comp_sous_classe, t_comp_classe, t_comp_classen1 where t_comp_sous_classe.code_cl=t_comp_classe.code_cl and t_comp_classe.code_cl=t_comp_classen1.code_cl and t_comp_classen1.code_cln1  between '" & id(0).ToString & "' and '" & id2(0).ToString & "'"
                            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                            For Each rw As DataRow In dt0.Rows
                                If ViewCptSigfiprattach.RowCount > 0 Then
                                    Dim trouv As Boolean = False
                                    For i = 0 To ViewCptSigfiprattach.RowCount - 1
                                        Select Case dtcompteSigfiprattach.Rows(i).Item(0).ToString
                                            Case rw(0).ToString
                                                trouv = True
                                                Exit For
                                        End Select

                                    Next
                                    If Not trouv Then
                                        Dim drS = dtcompteSigfiprattach.NewRow()
                                        drS(0) = rw(0).ToString
                                        drS(1) = MettreApost(rw(1).ToString)
                                        dtcompteSigfiprattach.Rows.Add(drS)
                                    End If
                                Else
                                    Dim drS = dtcompteSigfiprattach.NewRow()
                                    drS(0) = rw(0).ToString
                                    drS(1) = MettreApost(rw(1).ToString)
                                    dtcompteSigfiprattach.Rows.Add(drS)
                                End If
                            Next

                            LgListCompteSigfiprattach.DataSource = dtcompteSigfiprattach
                            ViewCptSigfiprattach.Columns(0).Width = 100
                            ViewCptSigfiprattach.Columns(1).Width = 250

                        End If

                        If (id(0).ToString.Length = 4 And id2(0).ToString.Length = 4) Then

                            query = "select t_comp_sous_classe.code_sc,t_comp_sous_classe.libelle_sc from t_comp_sous_classe, t_comp_classen2 where t_comp_sous_classe.code_cln2=t_comp_classen2.code_cln2 and t_comp_classen2.code_cln2  between '" & id(0).ToString & "' and '" & id2(0).ToString & "'"
                            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                            For Each rw As DataRow In dt0.Rows

                                If ViewCptSigfiprattach.RowCount > 0 Then
                                    Dim trouv As Boolean = False
                                    For i = 0 To ViewCptSigfiprattach.RowCount - 1
                                        Select Case dtcompteSigfiprattach.Rows(i).Item(0).ToString
                                            Case rw(0).ToString
                                                trouv = True
                                                Exit For
                                        End Select

                                    Next
                                    If Not trouv Then
                                        Dim drS = dtcompteSigfiprattach.NewRow()
                                        drS(0) = rw(0).ToString
                                        drS(1) = MettreApost(rw(1).ToString)
                                        dtcompteSigfiprattach.Rows.Add(drS)
                                    End If
                                Else
                                    Dim drS = dtcompteSigfiprattach.NewRow()
                                    drS(0) = rw(0).ToString
                                    drS(1) = MettreApost(rw(1).ToString)
                                    dtcompteSigfiprattach.Rows.Add(drS)
                                End If

                            Next

                            LgListCompteSigfiprattach.DataSource = dtcompteSigfiprattach
                            ViewCptSigfiprattach.Columns(0).Width = 100
                            ViewCptSigfiprattach.Columns(1).Width = 250

                        End If

                        If (id(0).ToString.Length > 4 And id2(0).ToString.Length > 4) Then

                            query = "select t_comp_sous_classe.code_sc,t_comp_sous_classe.libelle_sc from t_comp_sous_classe where t_comp_sous_classe.code_sc  between '" & id(0).ToString & "' and '" & id2(0).ToString & "'"
                            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                            For Each rw As DataRow In dt0.Rows

                                If ViewCptSigfiprattach.RowCount > 0 Then
                                    Dim trouv As Boolean = False
                                    For i = 0 To ViewCptSigfiprattach.RowCount - 1
                                        Select Case dtcompteSigfiprattach.Rows(i).Item(0).ToString
                                            Case rw(0).ToString
                                                trouv = True
                                                Exit For
                                        End Select

                                    Next
                                    If Not trouv Then
                                        Dim drS = dtcompteSigfiprattach.NewRow()
                                        drS(0) = rw(0).ToString
                                        drS(1) = MettreApost(rw(1).ToString)
                                        dtcompteSigfiprattach.Rows.Add(drS)
                                    End If
                                Else
                                    Dim drS = dtcompteSigfiprattach.NewRow()
                                    drS(0) = rw(0).ToString
                                    drS(1) = MettreApost(rw(1).ToString)
                                    dtcompteSigfiprattach.Rows.Add(drS)
                                End If

                            Next

                            LgListCompteSigfiprattach.DataSource = dtcompteSigfiprattach
                            ViewCptSigfiprattach.Columns(0).Width = 100
                            ViewCptSigfiprattach.Columns(1).Width = 250

                        End If
                    Else
                        MsgBox("Veuillez remplir ces champs : " & ControlChars.CrLf + erreur, MsgBoxStyle.Exclamation)
                    End If
                Case Else
            End Select
        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Parametre_Sigfip_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            EffacerTexBox4(PanelControl1)
            EffacerTexBox4(GroupControl1)
            EffacerTexBox10(GroupBox2)
            dtcompteSigfiprattach.Rows.Clear()
        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Parametre_Sigfip_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        Combsc.Properties.Items.Clear()
        Combsc2.Properties.Items.Clear()

        query = "select code_cl, libelle_cl from t_comp_classe"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows

            Combsc.Properties.Items.Add(rw(0).ToString & "  " & MettreApost(rw(1).ToString))
            Combsc2.Properties.Items.Add(rw(0).ToString & "  " & MettreApost(rw(1).ToString))

            query = "select code_sc, libelle_sc from t_comp_sous_classe where code_sc like '" & rw(0).ToString & "%'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw0 As DataRow In dt0.Rows
                Combsc.Properties.Items.Add(rw0(0).ToString & "  " & MettreApost(rw0(1).ToString))
                Combsc2.Properties.Items.Add(rw0(0).ToString & "  " & MettreApost(rw0(1).ToString))
            Next

        Next

        'remplir le datagrid
        RemplirDatagridSF(LgListParamSigfip, ViewSigfip)
        dtcompteSigfiprattach.Columns.Clear()
        dtcompteSigfiprattach.Columns.Add("N° Comptable", Type.GetType("System.String"))
        dtcompteSigfiprattach.Columns.Add("Libellé", Type.GetType("System.String"))

        ViewSigfip.Columns(0).Visible = False
        ViewSigfip.Columns(0).Width = 50
        ViewSigfip.Columns(1).Width = 100
        ViewSigfip.Columns(2).Width = 250
    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        Try
            'vérification des champ text
            Dim erreur As String = ""
            Dim compt As String = ""

            If txtcode.Text = "" Then
                erreur += "- Code parametre" & ControlChars.CrLf
            End If

            If txtint.Text = "" Then
                erreur += "- Libellé parametre" & ControlChars.CrLf
            End If

            If erreur = "" Then
                'insertion de la sous classe

                Dim pl0 As String
                pl0 = Mid(txtcode.Text, 1, 1)

                Dim pl As String
                pl = Mid(txtcode.Text, 1, 2)

                Dim pl1 As String
                pl1 = Mid(txtcode.Text, 1, 3)

                Dim pl2 As String
                pl2 = Mid(txtcode.Text, 1, 4)

                If Len(txtcode.Text) = 1 Then

                    query = "select * from T_PlanSigFip1 where SigfCompte1='" & txtcode.Text & "'"
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    If dt0.Rows.Count = 0 Then

                        'insertion de la classe niveau1
                       query= "insert into T_PlanSigFip1 values('" & txtcode.Text & "','" & EnleverApost(txtint.Text) & "')"
                        ExecuteNonQuery(query)

                    Else

                        'modification de la classe niveau1
                       query= "Update T_PlanSigFip1 set SigfLibelle1='" & EnleverApost(txtint.Text) & "' where SigfCompte1='" & txtcode.Text & "'"
                        ExecuteNonQuery(query)

                    End If


                ElseIf Len(txtcode.Text) = 2 Then

                    query = "select * from T_PlanSigFip2 where SigfCompte2='" & txtcode.Text & "'"
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    If dt0.Rows.Count = 0 Then

                        'insertion de la classe niveau1
                        query = "select * from T_PlanSigFip1 where SigfCompte1='" & pl0.ToString & "'"
                        dt0 = ExcecuteSelectQuery(query)
                        If dt0.Rows.Count = 0 Then

                           query= "insert into T_PlanSigFip1 values('" & pl0 & "','')"
                            ExecuteNonQuery(query)

                        End If


                        'insertion de la classe niveau2
                       query= "insert into T_PlanSigFip2 values('" & txtcode.Text & "','" & pl0 & "','" & EnleverApost(txtint.Text) & "')"
                        ExecuteNonQuery(query)


                    Else

                        'modification de la classe niveau2
                       query= "Update T_PlanSigFip2 set SigfLibelle2='" & EnleverApost(txtint.Text) & "' where SigfCompte2='" & txtcode.Text & "'"
                        ExecuteNonQuery(query)

                    End If


                ElseIf Len(txtcode.Text) = 3 Then

                    'insertion compte classe niveau3
                    query = "select * from T_PlanSigFip3 where SigfCompte3='" & pl1.ToString & "'"
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    If dt0.Rows.Count = 0 Then

                        'insertion de la classe niveau1
                        query = "select * from T_PlanSigFip1 where SigfCompte1='" & pl0.ToString & "'"
                        dt0 = ExcecuteSelectQuery(query)
                        If dt0.Rows.Count = 0 Then

                           query= "insert into T_PlanSigFip1 values('" & pl0 & "','')"
                            ExecuteNonQuery(query)

                        End If

                        'insertion de la classe niveau2
                        query = "select * from T_PlanSigFip2 where SigfCompte2='" & pl.ToString & "'"
                        dt0 = ExcecuteSelectQuery(query)
                        If dt0.Rows.Count = 0 Then

                           query= "insert into T_PlanSigFip2 values('" & pl & "','" & pl0 & "','')"
                            ExecuteNonQuery(query)

                        End If

                        'insertion de la classe niveau3
                       query= "insert into T_PlanSigFip3 values('" & txtcode.Text & "','" & pl & "','" & EnleverApost(txtint.Text) & "')"
                        ExecuteNonQuery(query)


                    Else

                        'modification de la sous classe
                       query= "Update T_PlanSigFip3 set SigfLibelle3='" & EnleverApost(txtint.Text) & "' where SigfCompte3='" & txtcode.Text & "'"
                        ExecuteNonQuery(query)

                    End If
                ElseIf Len(txtcode.Text) = 4 Then

                    'insertion compte classe niveau4
                    query = "select * from T_PlanSigFip4 where SigfCompte4='" & pl2.ToString & "'"
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    If dt0.Rows.Count = 0 Then

                        'insertion de la classe niveau1
                        query = "select * from T_PlanSigFip1 where SigfCompte1='" & pl0.ToString & "'"
                        dt0 = ExcecuteSelectQuery(query)
                        If dt0.Rows.Count = 0 Then

                           query= "insert into T_PlanSigFip1 values('" & pl0 & "','')"
                            ExecuteNonQuery(query)

                        End If


                        'insertion de la classe niveau2
                        query = "select * from T_PlanSigFip2 where SigfCompte2='" & pl.ToString & "'"
                        dt0 = ExcecuteSelectQuery(query)
                        If dt0.Rows.Count = 0 Then

                           query= "insert into T_PlanSigFip2 values('" & pl & "','" & pl0 & "','')"
                            ExecuteNonQuery(query)

                        End If


                        'insertion compte classe niveau3
                        query = "select * from T_PlanSigFip3 where SigfCompte3='" & pl1.ToString & "'"
                        dt0 = ExcecuteSelectQuery(query)
                        If dt0.Rows.Count = 0 Then

                            'insertion de la sous classe
                           query= "insert into T_PlanSigFip3 values('" & pl1 & "','" & pl & "','')"
                            ExecuteNonQuery(query)

                        End If

                        'insertion de la classe niveau4
                       query= "insert into T_PlanSigFip4 values('" & txtcode.Text & "','" & pl1 & "','" & EnleverApost(txtint.Text) & "')"
                        ExecuteNonQuery(query)

                    Else

                        'modification de la classe niveau2
                       query= "Update T_PlanSigFip4 set SigfLibelle4='" & EnleverApost(txtint.Text) & "' where SigfCompte4='" & txtcode.Text & "'"
                        ExecuteNonQuery(query)

                    End If
                ElseIf Len(txtcode.Text) > 4 Then

                    'insertion de la classe niveau1
                    query = "select * from T_PlanSigFip1 where SigfCompte1='" & pl0.ToString & "'"
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    If dt0.Rows.Count = 0 Then

                       query= "insert into T_PlanSigFip1 values('" & pl0 & "','')"
                        ExecuteNonQuery(query)

                    End If


                    'insertion de la classe niveau2
                    query = "select * from T_PlanSigFip2 where SigfCompte2='" & pl.ToString & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    If dt0.Rows.Count = 0 Then

                       query= "insert into T_PlanSigFip2 values('" & pl & "','" & pl0 & "','')"
                        ExecuteNonQuery(query)

                    End If


                    'insertion compte classe niveau3
                    query = "select * from T_PlanSigFip3 where SigfCompte3='" & pl1.ToString & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    If dt0.Rows.Count = 0 Then

                       query= "insert into T_PlanSigFip3 values('" & pl1 & "','" & pl & "','')"
                        ExecuteNonQuery(query)

                    End If


                    'insertion compte classe niveau4
                    query = "select * from T_PlanSigFip4 where SigfCompte4='" & pl2.ToString & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    If dt0.Rows.Count = 0 Then

                       query= "insert into T_PlanSigFip4 values('" & pl2 & "','" & pl1 & "','')"
                        ExecuteNonQuery(query)

                    End If



                    'insertion de la classe niveau5
                    query = "select * from T_PlanSigFip where SigfCompte='" & txtcode.Text & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    If dt0.Rows.Count = 0 Then

                       query= " INSERT INTO T_PlanSigFip VALUES('" & txtcode.Text & "','" & EnleverApost(txtint.Text) & "','" & pl2 & "')"
                        ExecuteNonQuery(query)
                       
                        For i = 0 To ViewCptSigfiprattach.RowCount - 1
                           query= "insert into T_correspondance_sigfip values('" & txtcode.Text & "','" & dtcompteSigfiprattach.Rows(i).item(0).ToString & "')"
                            ExecuteNonQuery(query)
                        Next

                    Else

                        'modification de la classe niveau5
                       query= "Update T_PlanSigFip set SigfLibelle='" & EnleverApost(txtint.Text) & "' where SigfCompte='" & txtcode.Text & "'"
                        ExecuteNonQuery(query)

                       query= "delete from T_correspondance_sigfip where SigfCompte='" & txtcode.Text & "'"
                        ExecuteNonQuery(query)

                        For i = 0 To ViewCptSigfiprattach.RowCount - 1
                           query= "insert into T_correspondance_sigfip values('" & txtcode.Text & "','" & dtcompteSigfiprattach.Rows(i).item(0).ToString & "')"
                            ExecuteNonQuery(query)
                        Next

                    End If

                End If


                'remplir le datagrid
                RemplirDatagridSF(LgListParamSigfip, ViewSigfip)
                dtcompteSigfiprattach.Rows.Clear()

                ViewSigfip.Columns(0).Visible = False
                ViewSigfip.Columns(0).Width = 50
                ViewSigfip.Columns(1).Width = 100
                ViewSigfip.Columns(2).Width = 250

                'Initialisation
                txtcode.Text = ""
                txtint.Text = ""
                Combsc.Text = ""
                Combsc2.Text = ""

            Else
                MsgBox("Veuillez remplir ces champs : " & ControlChars.CrLf + erreur, MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
            FailMsg("Erreur:" & vbNewLine & ex.ToString())
        End Try
    End Sub

    Private Sub LgListParamSigfip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LgListParamSigfip.Click
        Try
            If ViewSigfip.RowCount > 0 Then
                drx = ViewSigfip.GetDataRow(ViewSigfip.FocusedRowHandle)

                If drx(1).ToString <> "" Then
                    dtcompteSigfiprattach.Rows.Clear()
                    Combsc.Text = ""
                    Combsc2.Text = ""


                    query = "select SIGFCOMPTE1, SIGFLIBELLE1 from T_PLANSIGFIP1 where SIGFCOMPTE1='" & drx(1).ToString & "'"
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        txtcode.Text = rw(0).ToString
                        txtint.Text = MettreApost(rw(1).ToString)
                    Next

                    query = "select SIGFCOMPTE2, SIGFLIBELLE2 from T_PLANSIGFIP2 where SIGFCOMPTE2='" & drx(1).ToString & "'"
                    Dim dt As DataTable = ExcecuteSelectQuery(query)
                    For Each rw0 As DataRow In dt.Rows
                        txtcode.Text = rw0(0).ToString
                        txtint.Text = MettreApost(rw0(1).ToString)
                    Next

                    query = "select SIGFCOMPTE3, SIGFLIBELLE3 from T_PLANSIGFIP3 where SIGFCOMPTE3='" & drx(1).ToString & "'"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt1.Rows
                        txtcode.Text = rw1(0).ToString
                        txtint.Text = MettreApost(rw1(1).ToString)
                    Next

                    query = "select SIGFCOMPTE4, SIGFLIBELLE4 from T_PLANSIGFIP4 where SIGFCOMPTE4='" & drx(1).ToString & "'"
                    Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw2 As DataRow In dt2.Rows
                        txtcode.Text = rw2(0).ToString
                        txtint.Text = MettreApost(rw2(1).ToString)
                    Next

                    query = "select SIGFCOMPTE, SIGFLIBELLE from T_PLANSIGFIP where SIGFCOMPTE='" & drx(1).ToString & "'"
                    Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw3 As DataRow In dt3.Rows
                        txtcode.Text = rw3(0).ToString
                        txtint.Text = MettreApost(rw3(1).ToString)
                    Next

                    query = "select tr.COMPTE, sc.libelle_sc from T_CORRESPONDANCE_SIGFIP tr,t_comp_sous_classe sc where tr.COMPTE=sc.code_sc and tr.SIGFCOMPTE='" & drx(1).ToString & "'"
                    Dim dt4 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw4 As DataRow In dt4.Rows
                        Dim drS = dtcompteSigfiprattach.NewRow()
                        drS(0) = rw4(0).ToString
                        drS(1) = MettreApost(rw4(1).ToString)
                        dtcompteSigfiprattach.Rows.Add(drS)
                    Next

                    LgListCompteSigfiprattach.DataSource = dtcompteSigfiprattach
                    ViewCptSigfiprattach.Columns(0).Width = 100
                    ViewCptSigfiprattach.Columns(1).Width = 250

                End If

                If (ViewSigfip.RowCount > 0) Then
                    drx = ViewSigfip.GetDataRow(ViewSigfip.FocusedRowHandle)
                    Dim NCOMP = drx(1).ToString
                    ColorRowGrid(ViewSigfip, "[Identifiant]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
                    ColorRowGridAnal(ViewSigfip, "[Identifiant]='" & NCOMP & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
                End If
            End If
        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        Try
            EffacerTexBox4(PanelControl1)
            EffacerTexBox4(GroupControl1)
            EffacerTexBox10(GroupBox2)
            dtcompteSigfiprattach.Rows.Clear()
        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub txtcode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcode.KeyPress
        Select Case e.KeyChar
            Case ControlChars.CrLf
                SimpleButton1_Click(Me, e)
            Case Else
        End Select
    End Sub

    Private Sub txtcode_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcode.Leave

        query = "select * from T_PlanSigFip where sigfcompte='" & txtcode.Text & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        If dt0.Rows.Count = 0 Then
        Else
            MsgBox("Ce Compte existe déjà")
            txtcode.Text = ""
            txtcode.Focus()
        End If

    End Sub

    Private Sub txtcode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcode.TextChanged
        If Len(txtcode.Text) <= 4 Then
            Combsc.Enabled = False
            Combsc2.Enabled = False
            dtcompteSigfiprattach.Rows.Clear()
        Else
            Combsc.Enabled = True
            Combsc2.Enabled = True
        End If
    End Sub

    Private Sub TextEdit1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextEdit1.EditValueChanged
        Try
            If TextEdit1.Text = "" Then
                'remplir le datagrid
                RemplirDatagridSF(LgListParamSigfip, ViewSigfip)


                ViewSigfip.Columns(0).Visible = False
                ViewSigfip.Columns(0).Width = 50
                ViewSigfip.Columns(1).Width = 100
                ViewSigfip.Columns(2).Width = 250
            Else


                If Len(TextEdit1.Text) = 1 Then


                    dtService.Columns.Clear()
                    dtService.Columns.Add("Choix", Type.GetType("System.Boolean"))
                    dtService.Columns.Add("Identifiant", Type.GetType("System.String"))
                    dtService.Columns.Add("Libellé", Type.GetType("System.String"))
                    dtService.Rows.Clear()

                    Dim cptr As Decimal = 0
                    Dim cpt1 As Decimal = 1
                    Dim cpt2 As Decimal = 2
                    query = "select SIGFCOMPTE1, SIGFLIBELLE1 from T_PlanSigFip1 where SIGFCOMPTE1 like '%" & EnleverApost(TextEdit1.Text) & "%' or SIGFLIBELLE1 like '%" & EnleverApost(TextEdit1.Text) & "%'"
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        cptr += 1
                        Dim drS = dtService.NewRow()
                        drS(0) = TabTrue(cptr - 1)
                        drS(1) = MettreApost(rw(0).ToString)
                        drS(2) = MettreApost(rw(1).ToString)
                        dtService.Rows.Add(drS)

                        query = "select SIGFCOMPTE2, SIGFLIBELLE2 from T_PlanSigFip2 Where SIGFCOMPTE1='" & EnleverApost(rw(0).ToString) & "'"
                        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw1 As DataRow In dt1.Rows
                            cptr += 1
                            drS = dtService.NewRow()
                            drS(0) = TabTrue(cptr - 1)
                            drS(1) = MettreApost(rw1(0).ToString)
                            drS(2) = MettreApost(rw1(1).ToString)
                            dtService.Rows.Add(drS)

                            query = "select SIGFCOMPTE3, SIGFLIBELLE3 from T_PlanSigFip3 Where SIGFCOMPTE2='" & EnleverApost(rw1(0).ToString) & "'"
                            Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                            For Each rw2 As DataRow In dt2.Rows
                                cptr += 1
                                drS = dtService.NewRow()
                                drS(0) = TabTrue(cptr - 1)
                                drS(1) = MettreApost(rw2(0).ToString)
                                drS(2) = MettreApost(rw2(1).ToString)
                                dtService.Rows.Add(drS)

                                query = "select SIGFCOMPTE4, SIGFLIBELLE4 from T_PlanSigFip4 Where SIGFCOMPTE3='" & EnleverApost(rw2(0).ToString) & "'"
                                Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                                For Each rw3 As DataRow In dt3.Rows
                                    cptr += 1
                                    drS = dtService.NewRow()
                                    drS(0) = TabTrue(cptr - 1)
                                    drS(1) = MettreApost(rw3(0).ToString)
                                    drS(2) = MettreApost(rw3(1).ToString)
                                    dtService.Rows.Add(drS)

                                    query = "select SIGFCOMPTE, SIGFLIBELLE from T_PlanSigFip Where SIGFCOMPTE4='" & EnleverApost(rw3(0).ToString) & "'"
                                    Dim dt5 As DataTable = ExcecuteSelectQuery(query)
                                    For Each rw5 As DataRow In dt5.Rows
                                        cptr += 1
                                        drS = dtService.NewRow()
                                        drS(0) = TabTrue(cptr - 1)
                                        drS(1) = MettreApost(rw5(0).ToString)
                                        drS(2) = MettreApost(rw5(1).ToString)
                                        dtService.Rows.Add(drS)
                                    Next
                                Next
                            Next
                        Next
                    Next

                    ViewSigfip.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
                    ColorRowGrid(ViewSigfip, "[Choix]", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
                    ColorRowGridAnal(ViewSigfip, "[Choix]=true", Color.LightGray, "Times New Roman", 11, FontStyle.Bold, Color.Black, False)

                    LgListParamSigfip.DataSource = dtService
                    
                    ViewSigfip.Columns(0).Visible = False
                    ViewSigfip.Columns(0).Width = 50
                    ViewSigfip.Columns(1).Width = 100
                    ViewSigfip.Columns(2).Width = 250

                ElseIf Len(TextEdit1.Text) = 2 Then


                    dtService.Columns.Clear()
                    dtService.Columns.Add("Choix", Type.GetType("System.Boolean"))
                    dtService.Columns.Add("Identifiant", Type.GetType("System.String"))
                    dtService.Columns.Add("Libellé", Type.GetType("System.String"))
                    dtService.Rows.Clear()

                    Dim cptr As Decimal = 0
                    Dim cpt1 As Decimal = 1
                    Dim cpt2 As Decimal = 2

                    query = "select SIGFCOMPTE2, SIGFLIBELLE2 from T_PlanSigFip2 Where SIGFCOMPTE2 like '%" & EnleverApost(TextEdit1.Text) & "%'"
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        cptr += 1
                        Dim drS = dtService.NewRow()
                        drS(0) = TabTrue(cptr - 1)
                        drS(1) = MettreApost(rw(0).ToString)
                        drS(2) = MettreApost(rw(1).ToString)
                        dtService.Rows.Add(drS)

                        query = "select SIGFCOMPTE3, SIGFLIBELLE3 from T_PlanSigFip3 Where SIGFCOMPTE2='" & rw(0).ToString & "'"
                        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw1 As DataRow In dt1.Rows
                            cptr += 1
                            drS = dtService.NewRow()
                            drS(0) = TabTrue(cptr - 1)
                            drS(1) = MettreApost(rw1(0).ToString)
                            drS(2) = MettreApost(rw1(1).ToString)
                            dtService.Rows.Add(drS)

                            query = "select SIGFCOMPTE4, SIGFLIBELLE4 from T_PlanSigFip4 Where SIGFCOMPTE3='" & rw1(0).ToString & "'"
                            Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                            For Each rw2 As DataRow In dt2.Rows
                                cptr += 1
                                drS = dtService.NewRow()
                                drS(0) = TabTrue(cptr - 1)
                                drS(1) = MettreApost(rw2(0).ToString)
                                drS(2) = MettreApost(rw2(1).ToString)
                                dtService.Rows.Add(drS)

                                query = "select SIGFCOMPTE, SIGFLIBELLE from T_PlanSigFip Where SIGFCOMPTE4='" & rw2(0).ToString & "'"
                                Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                                For Each rw3 As DataRow In dt3.Rows
                                    cptr += 1
                                    drS = dtService.NewRow()
                                    drS(0) = TabTrue(cptr - 1)
                                    drS(1) = MettreApost(rw3(0).ToString)
                                    drS(2) = MettreApost(rw3(1).ToString)
                                    dtService.Rows.Add(drS)
                                Next
                            Next
                        Next
                    Next

                    ViewSigfip.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
                    ColorRowGrid(ViewSigfip, "[Choix]", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
                    ColorRowGridAnal(ViewSigfip, "[Choix]=true", Color.LightGray, "Times New Roman", 11, FontStyle.Bold, Color.Black, False)

                    LgListParamSigfip.DataSource = dtService

                    ViewSigfip.Columns(0).Visible = False
                    ViewSigfip.Columns(0).Width = 50
                    ViewSigfip.Columns(1).Width = 100
                    ViewSigfip.Columns(2).Width = 250

                ElseIf Len(TextEdit1.Text) = 3 Then


                        dtService.Columns.Clear()
                        dtService.Columns.Add("Choix", Type.GetType("System.Boolean"))
                        dtService.Columns.Add("Identifiant", Type.GetType("System.String"))
                        dtService.Columns.Add("Libellé", Type.GetType("System.String"))
                        dtService.Rows.Clear()

                        Dim cptr As Decimal = 0
                        Dim cpt1 As Decimal = 1
                        Dim cpt2 As Decimal = 2

                    query = "select SIGFCOMPTE3, SIGFLIBELLE3 from T_PlanSigFip3 Where SIGFCOMPTE3 like '%" & EnleverApost(TextEdit1.Text) & "%'"
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        cptr += 1
                        Dim drS = dtService.NewRow()
                        drS(0) = TabTrue(cptr - 1)
                        drS(1) = MettreApost(rw(0).ToString)
                        drS(2) = MettreApost(rw(1).ToString)
                        dtService.Rows.Add(drS)

                        query = "select SIGFCOMPTE4, SIGFLIBELLE4 from T_PlanSigFip4 Where SIGFCOMPTE3='" & rw(0).ToString & "'"
                        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw1 As DataRow In dt1.Rows
                            cptr += 1
                            drS = dtService.NewRow()
                            drS(0) = TabTrue(cptr - 1)
                            drS(1) = MettreApost(rw1(0).ToString)
                            drS(2) = MettreApost(rw1(1).ToString)
                            dtService.Rows.Add(drS)

                            query = "select SIGFCOMPTE, SIGFLIBELLE from T_PlanSigFip Where SIGFCOMPTE4='" & rw1(0).ToString & "'"
                            Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                            For Each rw2 As DataRow In dt2.Rows
                                cptr += 1
                                drS = dtService.NewRow()
                                drS(0) = TabTrue(cptr - 1)
                                drS(1) = MettreApost(rw2(0).ToString)
                                drS(2) = MettreApost(rw2(1).ToString)
                                dtService.Rows.Add(drS)
                            Next
                        Next
                    Next


                    ViewSigfip.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
                    ColorRowGrid(ViewSigfip, "[Choix]", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
                    ColorRowGridAnal(ViewSigfip, "[Choix]=true", Color.LightGray, "Times New Roman", 11, FontStyle.Bold, Color.Black, False)

                    LgListParamSigfip.DataSource = dtService

                    ViewSigfip.Columns(0).Visible = False
                    ViewSigfip.Columns(0).Width = 50
                    ViewSigfip.Columns(1).Width = 100
                    ViewSigfip.Columns(2).Width = 250

                ElseIf Len(TextEdit1.Text) = 4 Then

                        dtService.Columns.Clear()
                        dtService.Columns.Add("Choix", Type.GetType("System.Boolean"))
                        dtService.Columns.Add("Identifiant", Type.GetType("System.String"))
                        dtService.Columns.Add("Libellé", Type.GetType("System.String"))
                        dtService.Rows.Clear()

                        Dim cptr As Decimal = 0
                        Dim cpt1 As Decimal = 1
                        Dim cpt2 As Decimal = 2

                    query = "select SIGFCOMPTE4, SIGFLIBELLE4 from T_PlanSigFip4 Where SIGFCOMPTE4 like '%" & EnleverApost(TextEdit1.Text) & "%'"
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        cptr += 1
                        Dim drS = dtService.NewRow()
                        drS(0) = TabTrue(cptr - 1)
                        drS(1) = MettreApost(rw(0).ToString)
                        drS(2) = MettreApost(rw(1).ToString)
                        dtService.Rows.Add(drS)

                        query = "select SIGFCOMPTE, SIGFLIBELLE from T_PlanSigFip Where SIGFCOMPTE4='" & rw(0).ToString & "'"
                        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw1 As DataRow In dt1.Rows
                            cptr += 1
                            drS = dtService.NewRow()
                            drS(0) = TabTrue(cptr - 1)
                            drS(1) = MettreApost(rw1(0).ToString)
                            drS(2) = MettreApost(rw1(1).ToString)
                            dtService.Rows.Add(drS)
                        Next
                    Next

                    ViewSigfip.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
                    ColorRowGrid(ViewSigfip, "[Choix]", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
                    ColorRowGridAnal(ViewSigfip, "[Choix]=true", Color.LightGray, "Times New Roman", 11, FontStyle.Bold, Color.Black, False)

                    LgListParamSigfip.DataSource = dtService

                    ViewSigfip.Columns(0).Visible = False
                    ViewSigfip.Columns(0).Width = 50
                    ViewSigfip.Columns(1).Width = 100
                    ViewSigfip.Columns(2).Width = 250

                ElseIf Len(TextEdit1.Text) > 4 Then

                        dtService.Columns.Clear()
                        dtService.Columns.Add("Choix", Type.GetType("System.Boolean"))
                        dtService.Columns.Add("Identifiant", Type.GetType("System.String"))
                        dtService.Columns.Add("Libellé", Type.GetType("System.String"))
                        dtService.Rows.Clear()

                        Dim cptr As Decimal = 0
                        Dim cpt1 As Decimal = 1
                        Dim cpt2 As Decimal = 2

                    query = "select SIGFCOMPTE, SIGFLIBELLE from T_PlanSigFip Where SIGFCOMPTE like '%" & EnleverApost(TextEdit1.Text) & "%'"
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        cptr += 1
                        Dim drS = dtService.NewRow()
                        drS(0) = TabTrue(cptr - 1)
                        drS(1) = MettreApost(rw(0).ToString)
                        drS(2) = MettreApost(rw(1).ToString)
                        dtService.Rows.Add(drS)
                    Next

                    ViewSigfip.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
                    ColorRowGrid(ViewSigfip, "[Choix]", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
                    ColorRowGridAnal(ViewSigfip, "[Choix]=true", Color.LightGray, "Times New Roman", 11, FontStyle.Bold, Color.Black, False)

                    LgListParamSigfip.DataSource = dtService

                    ViewSigfip.Columns(0).Visible = False
                    ViewSigfip.Columns(0).Width = 50
                    ViewSigfip.Columns(1).Width = 100
                    ViewSigfip.Columns(2).Width = 250
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation, "ClearProject")
        End Try
    End Sub

    Private Sub txtint_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtint.KeyPress
        Select Case e.KeyChar
            Case ControlChars.CrLf
                SimpleButton1_Click(Me, e)
            Case Else
        End Select
    End Sub

    Private Sub LgListCompteSigfiprattach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LgListCompteSigfiprattach.Click
        If (ViewCptSigfiprattach.RowCount > 0) Then
            drx = ViewCptSigfiprattach.GetDataRow(ViewCptSigfiprattach.FocusedRowHandle)
            Dim NCOMP = drx(0).ToString
            ColorRowGrid(ViewCptSigfiprattach, "[N° Comptable]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewCptSigfiprattach, "[N° Comptable]='" & NCOMP & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
        End If
    End Sub

    Private Sub LgListParamSigfip_EditorKeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles LgListParamSigfip.EditorKeyUp
        If (ViewSigfip.RowCount > 0) Then
            drx = ViewSigfip.GetDataRow(ViewSigfip.FocusedRowHandle)
            Dim NCOMP = drx(1).ToString
            ColorRowGrid(ViewSigfip, "[Identifiant]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewSigfip, "[Identifiant]='" & NCOMP & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
        End If
    End Sub

    Private Sub LgListCompteSigfiprattach_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LgListCompteSigfiprattach.MouseUp
        If (ViewCptSigfiprattach.RowCount > 0) Then
            drx = ViewCptSigfiprattach.GetDataRow(ViewCptSigfiprattach.FocusedRowHandle)
            Dim NCOMP = drx(0).ToString
            ColorRowGrid(ViewCptSigfiprattach, "[N° Comptable]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewCptSigfiprattach, "[N° Comptable]='" & NCOMP & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
        End If
    End Sub

    Private Sub SupprimerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupprimerToolStripMenuItem.Click
        Try
            If ViewSigfip.RowCount > 0 Then
                drx1 = ViewSigfip.GetDataRow(ViewSigfip.FocusedRowHandle)
                If ViewCptSigfiprattach.RowCount > 0 Then
                    drx = ViewCptSigfiprattach.GetDataRow(ViewCptSigfiprattach.FocusedRowHandle)
                   query= "delete from T_CORRESPONDANCE_SIGFIP where SIGFCOMPTE='" & drx1(1).ToString & "' and COMPTE='" & drx(0).ToString & "'"
                    ExecuteNonQuery(query)
                    ViewCptSigfiprattach.GetDataRow(ViewCptSigfiprattach.FocusedRowHandle).Delete()
                End If
            End If
        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

End Class