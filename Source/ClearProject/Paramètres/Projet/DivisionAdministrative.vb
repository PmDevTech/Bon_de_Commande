Imports MySql.Data.MySqlClient

Public Class DivisionAdministrative

    Dim dtGeo = New DataTable()
    Dim dtPlan = New DataTable()
    Dim dtDivision = New DataTable()
    Dim DrX As DataRow

    Dim CodeGeo As String = ""
    Dim CodePlan As String = ""
    Dim CodeSup As String = "0"
    Dim codeZone() As Decimal
    Private Sub DivisionAdministrative_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ChargerGeo()
        ChargerPlan()
        ChargerDivision()
    End Sub

    Private Sub ChargerGeo()
        dtGeo.Columns.Clear()

        dtGeo.Columns.Add("Code", Type.GetType("System.String"))
        dtGeo.Columns.Add("Ref", Type.GetType("System.String"))
        dtGeo.Columns.Add("Libellé", Type.GetType("System.String"))

        Dim cptr As Decimal = 0

        'Dim Reader As MySqlDataReader

        query = "select NiveauStr, LibelleStr from T_StructGeo order by NiveauStr"
        dtGeo.Rows.Clear()
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtGeo.NewRow()

            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = rw(0).ToString
            drS(2) = MettreApost(rw(1).ToString)

            dtGeo.Rows.Add(drS)
        Next
        GridGeo.DataSource = dtGeo

        ViewGeo.Columns(0).Visible = False
        ViewGeo.Columns(1).Width = 40
        ViewGeo.Columns(2).Width = GridGeo.Width - 62

        ViewGeo.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

        ColorRowGrid(ViewGeo, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub

    Private Sub ChargerPlan()

        dtPlan.Columns.Clear()

        dtPlan.Columns.Add("Code", Type.GetType("System.String"))
        dtPlan.Columns.Add("Ref", Type.GetType("System.String"))
        dtPlan.Columns.Add("*", Type.GetType("System.String"))
        dtPlan.Columns.Add("Etendue", Type.GetType("System.String"))

        Dim cptr As Decimal = 0

        query = "select RefPlan, OrdrePlan, LibellePlan from T_PlanAdministratif where CodeProjet='" & ProjetEnCours & "' order by OrdrePlan"
        dtPlan.Rows.Clear()
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtPlan.NewRow()

            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = rw(0).ToString
            drS(2) = rw(1).ToString
            drS(3) = MettreApost(rw(2).ToString)

            dtPlan.Rows.Add(drS)
        Next

        GridPlan.DataSource = dtPlan

        ViewPlan.Columns(0).Visible = False
        ViewPlan.Columns(1).Visible = False
        ViewPlan.Columns(2).Width = 40
        ViewPlan.Columns(3).Width = GridPlan.Width - 62 '-142+80

        ViewPlan.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

        ColorRowGrid(ViewPlan, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

        GbAjoutDecoup.Visible = False
        LblNomPlan.Text = "..."
        CodePlan = ""

    End Sub
    Private Sub BtAjoutDecoup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtAjoutDecoup.Click
        If (LblNomPlan.Text <> "..." And CodePlan <> "") Then
            GbAjoutDecoup.Visible = True
            ChargerDivSup()
            Me.AcceptButton = BtEnrgDecoup
        Else
            SuccesMsg("Sélectionnez un élément dans le découpage analytique svp.")
        End If

    End Sub
    Private Sub ChargerDivSup()

        CmbDecoupSup.Properties.Items.Clear()
        CmbDecoupSup.Text = ""
        cmbZone.Text = ""
        TxtLibDecoup.Text = ""
        CodeSup = "0"
        query = "select NiveauStr from T_PlanAdministratif where RefPlan='" & CodePlan & "'"
        Dim niveau = Val(ExecuteScallar(query))
        'If niveau = 1 Then
        'ElseIf niveau > 1 Then
        '    query = "select RefPlan from T_PlanAdministratif where NiveauStr='" & niveau - 1 & "' OR NiveauStr='" & niveau & "'"
        '    Dim dtx As DataTable = ExcecuteSelectQuery(query)
        '    CmbDecoupSup.Properties.Items.Clear()
        '    For Each rwx As DataRow In dtx.Rows
        '        query = "select LibelleDivision, RefDecoupSup from T_DivisionAdministrative where RefPlan='" & rwx(0) & "' order by RefDecoupSup, LibelleDivision"
        '        Dim dt = ExcecuteSelectQuery(query)
        '        For Each rw As DataRow In dt.Rows
        '            CmbDecoupSup.Properties.Items.Add(MettreApost(rw(0).ToString))
        '        Next
        '    Next
        'Else
        '    MessageBox.Show("Quelque chose n'a bien fonctionné.", "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End If
        query = "select LibelleDivision, RefDecoupSup from T_DivisionAdministrative order by RefDecoupSup, LibelleDivision ASC"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbDecoupSup.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next
        'Charger la zone géo
        query = "SELECT LibelleZone,CodeZone FROM `t_zonegeo` where NiveauStr='" & niveau & "' order by LibelleZone"
        cmbZone.Properties.Items.Clear()
        Dim dt0 = ExcecuteSelectQuery(query)
        ReDim codeZone(dt0.Rows.Count)
        For i = 0 To dt0.Rows.Count - 1
            cmbZone.Properties.Items.Add(MettreApost(dt0.Rows(i)(0).ToString))
            codeZone(i) = dt0.Rows(i)(1)
        Next
    End Sub
    Private Sub GridGeo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridGeo.Click
        If (ViewGeo.RowCount > 0) Then
            DrX = ViewGeo.GetDataRow(ViewGeo.FocusedRowHandle)
            CodeGeo = DrX(1).ToString
            ColorRowGrid(ViewGeo, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewGeo, "[Ref]='" & CodeGeo & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)
        End If
    End Sub
    Private Sub BtSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtSelect.Click

        If (ViewGeo.RowCount > 0 And CodeGeo <> "") Then

            Dim PlanExist As Boolean = False

            'Dim Reader As MySqlDataReader

            query = "select * from T_PlanAdministratif where NiveauStr='" & CodeGeo & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                PlanExist = True
            End If

            If (PlanExist = True) Then
                MsgBox("Ce découpage a déjà été pris en compte!", MsgBoxStyle.Information)
            Else
                'Dim Denomination As String = InputBox("Entrez la dénomination", "Découpage Analytique", "")

                Dim ordre As Decimal = 0

                Dim DatSet = New DataSet
                query = "select * from T_PlanAdministratif"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "T_PlanAdministratif")
                Dim DatTable = DatSet.Tables("T_PlanAdministratif")
                Dim DatRow = DatSet.Tables("T_PlanAdministratif").NewRow()

                DatRow("OrdrePlan") = ordre.ToString
                DatRow("NiveauStr") = CodeGeo
                DatRow("CodeProjet") = ProjetEnCours
                query = "select LibelleStr from T_StructGeo where NiveauStr='" & CodeGeo & "'"
                Dim libelle = ExecuteScallar(query)
                DatRow("LibellePlan") = EnleverApost(libelle)

                DatSet.Tables("T_PlanAdministratif").Rows.Add(DatRow)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "T_PlanAdministratif")

                DatSet.Clear()
                BDQUIT(sqlconn)

                'Mise à jour ordre **************************
                MajOrdre()
                '********************************************

                ChargerPlan()

            End If

        End If

    End Sub

    Private Sub MajOrdre()
        Dim RefPlan(10) As String
        Dim nbRef As Decimal = 0

        query = "select P.RefPlan, S.NiveauStr from T_PlanAdministratif as P, T_StructGeo as S where P.NiveauStr=S.NiveauStr and P.CodeProjet='" & ProjetEnCours & "' order by S.NiveauStr"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            RefPlan(nbRef) = rw(0).ToString
            nbRef += 1
        Next

        For k As Integer = 0 To nbRef - 1
            Dim DatSet = New DataSet
            query = "select * from T_PlanAdministratif where RefPlan='" & RefPlan(k) & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Fill(DatSet, "T_PlanAdministratif")

            DatSet.Tables!T_PlanAdministratif.Rows(0)!OrdrePlan = (k + 1).ToString

            DatAdapt.Update(DatSet, "T_PlanAdministratif")
            DatSet.Clear()
            BDQUIT(sqlconn)
        Next
    End Sub

    Private Sub GridPlan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridPlan.Click

        If (ViewPlan.RowCount > 0) Then
            DrX = ViewPlan.GetDataRow(ViewPlan.FocusedRowHandle)
            CodePlan = DrX(1).ToString

            LblNomPlan.Text = "NIVEAU D'ADMINISTRATION : " & DrX(3).ToString
            GbAjoutDecoup.Visible = False

            ColorRowGrid(ViewPlan, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewPlan, "[Ref]='" & CodePlan & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)
        End If

    End Sub

    Private Sub BtDeselect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtDeselect.Click

        If (ViewPlan.RowCount > 0 And CodePlan <> "") Then

            Dim PlanOQP As Boolean = False

            'Dim Reader As MySqlDataReader

            query = "select * from T_DivisionAdministrative where RefPlan='" & CodePlan & "'"
            Dim dt = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                PlanOQP = True
            End If


            If (PlanOQP = True) Then
                MsgBox("Enregistrement en cours d'utilisation!", MsgBoxStyle.Information)
            Else


                query = "DELETE from T_PlanAdministratif where RefPlan='" & CodePlan & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)


                LblNomPlan.Text = "..."
                CodePlan = ""

                'Mise à jour ordre **************************
                MajOrdre()
                '********************************************

                ChargerPlan()

            End If

        End If

    End Sub

    Private Sub BtEnrgDecoup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgDecoup.Click
        If (TxtLibDecoup.Text <> "" And CodePlan <> "" And cmbZone.SelectedIndex <> -1) Then

            Try
                Dim DivExist As Boolean = False

                query = "select * from T_DivisionAdministrative where LibelleDivision='" & EnleverApost(TxtLibDecoup.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt = ExcecuteSelectQuery(query)
                If dt.Rows.Count > 0 Then
                    DivExist = True
                End If

                If (DivExist = True) Then
                    FailMsg("Cette Division existe déjà.")
                Else

                    If ViewDecoup.RowCount = 0 Then 'Création de la 1ere division administrative
                        query = "insert into T_DivisionAdministrative values(null," & CodePlan & ",'" & EnleverApost(Trim(TxtLibDecoup.Text)) & "',0,'" & ProjetEnCours & "','" & EnleverApost(cmbZone.Text) & "'," & codeZone(cmbZone.SelectedIndex) & ")"
                        ExecuteNonQuery(query)
                        'query = "select max(RefDecoupAdmin) from T_DivisionAdministrative"
                        'Dim RefDecoupAdmin = ExecuteScallar(query)

                        'Dim str As String = "PERSONNEL RATTACHÉ " & txtDebutDivis.Text & TxtLibDecoup.Text
                        'Dim partS() As String = str.Replace("'", "").Replace("  ", " ").Replace(" le", "").Replace(" la", "").Replace(" les", "").Replace(" l'", "").Replace(" de", "").Replace(" du", "").Replace(" des", "").Replace(" d'", "").Split(" "c)
                        'Dim CodeS As String = ""
                        'For Each elt In partS
                        '    CodeS = CodeS & Mid(elt, 1, 1).ToUpper
                        'Next

                        'query = "select AbregeService from T_Service where AbregeService='" & CodeS & "'"
                        'Dim res = ExecuteScallar(query)
                        'Dim cpte As Decimal = 0
                        'While Len(res) <> 0
                        '    cpte += 1
                        '    query = "select AbregeService from T_Service where AbregeService='" & CodeS & cpte & "'"
                        '    res = ExecuteScallar(query)
                        '    If Len(res) = 0 Then
                        '        res = CodeS & cpte
                        '        Exit While
                        '    End If
                        'End While
                        'If Len(res) = 0 Then
                        '    res = CodeS
                        'End If
                        'query = "INSERT INTO T_Service VALUES(null," & RefDecoupAdmin & ",'PERSONNEL RATTACHÉ A LA " & txtDebutDivis.Text & EnleverApost(Trim(TxtLibDecoup.Text)) & "','" & res & "'," & codeZone(cmbZone.SelectedIndex) & ",0,'" & ProjetEnCours & "','" & Now & "','" & Now & "','" & CodeUtilisateur & "')"
                        'ExecuteNonQuery(query)
                        'Dim LastServiceCode = ExecuteScallar("Select Max(CodeService) as Code from t_service")

                        'str = "DIRECTEUR " & Trim(TxtLibDecoup.Text)
                        'partS = str.Replace("'", "").Replace("  ", " ").Replace(" le", "").Replace(" la", "").Replace(" les", "").Replace(" l'", "").Replace(" de", "").Replace(" du", "").Replace(" des", "").Replace(" d'", "").Split(" "c)
                        'CodeS = ""
                        'For Each elt In partS
                        '    CodeS = CodeS & Mid(elt, 1, 1).ToUpper
                        'Next

                        'query = "select CodeFonction from t_fonction where CodeFonction='" & CodeS & "'"
                        'res = ExecuteScallar(query)
                        'cpte = 0
                        'While Len(res) <> 0
                        '    cpte += 1
                        '    query = "select CodeFonction from t_fonction where CodeFonction='" & CodeS & cpte & "'"
                        '    res = ExecuteScallar(query)
                        '    If Len(res) = 0 Then
                        '        res = CodeS & cpte
                        '        Exit While
                        '    End If
                        'End While
                        'If Len(res) = 0 Then
                        '    res = CodeS
                        'End If

                        'partS = Trim(TxtLibDecoup.Text).Split(" "c)
                        'Dim NewLibDecoup As String = ""
                        'If Mid(partS(0), Len(partS(0))).ToUpper() = "S" Then
                        '    partS(0) = Mid(partS(0), 1, Len(partS(0)) - 1) 'Enlève S si il n'y en a
                        'End If
                        'If Len(partS(0)) > 3 Then
                        '    If Mid(partS(0), Len(partS(0)) - 3).ToUpper() = "IERE" Or Mid(partS(0), Len(partS(0)) - 3).ToUpper() = "IÈRE" Then
                        '        partS(0) = Mid(partS(0), 1, Len(partS(0)) - 4) & "IER"
                        '        For i = 0 To partS.Length - 1
                        '            NewLibDecoup += partS(i) & " "
                        '        Next
                        '    ElseIf Mid(partS(0), Len(partS(0)) - 1).ToUpper() = "VE" Then
                        '        partS(0) = Mid(partS(0), 1, Len(partS(0)) - 2) & "F"
                        '        For i = 0 To partS.Length - 1
                        '            NewLibDecoup += partS(i) & " "
                        '        Next
                        '    ElseIf Mid(partS(0), Len(partS(0)) - 3).ToUpper() = "ELLE" Then
                        '        partS(0) = Mid(partS(0), 1, Len(partS(0)) - 4) & "EL"
                        '        For i = 0 To partS.Length - 1
                        '            NewLibDecoup += partS(i) & " "
                        '        Next
                        '    Else
                        '        For i = 0 To partS.Length - 1
                        '            NewLibDecoup += partS(i) & " "
                        '        Next
                        '    End If
                        'Else
                        '    For i = 0 To partS.Length - 1
                        '        NewLibDecoup += partS(i) & " "
                        '    Next
                        'End If

                        'If Val(CodeSup) <> 0 Then
                        '    query = "select RefFonction from t_fonction where LibelleFonction LIKE 'DIRECTEUR%' and RefDecoupAdmin=" & CodeSup
                        '    Dim CodeBoss As String = ""
                        '    CodeBoss = ExecuteScallar(query)
                        '    query = "INSERT INTO t_fonction VALUES(null,'" & res & "','DIRECTEUR " & EnleverApost(Trim(NewLibDecoup)) & "'," & CodeBoss & "," & LastServiceCode & "," & RefDecoupAdmin & ",'" & Now & "','" & Now & "','" & CodeUtilisateur & "')"
                        'Else
                        '    query = "INSERT INTO t_fonction VALUES(null,'" & res & "','DIRECTEUR " & EnleverApost(Trim(NewLibDecoup)) & "',0," & LastServiceCode & "," & RefDecoupAdmin & ",'" & Now & "','" & Now & "','" & CodeUtilisateur & "')"
                        'End If
                        'ExecuteNonQuery(query)

                        MessageBox.Show("Division enregistrée avec succès.", "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Else
                        If Val(CodeSup) = 0 Then
                            FailMsg("Veuillez identifier le supérieur de cette division administrative svp")
                            CmbDecoupSup.Focus()
                            Exit Sub
                        End If
                        query = "insert into T_DivisionAdministrative values(null," & CodePlan & ",'" & EnleverApost(Trim(TxtLibDecoup.Text)) & "'," & CodeSup & ",'" & ProjetEnCours & "','" & EnleverApost(cmbZone.Text) & "'," & codeZone(cmbZone.SelectedIndex) & ")"
                        ExecuteNonQuery(query)

                        'query = "select max(RefDecoupAdmin) from T_DivisionAdministrative"
                        'Dim RefDecoupAdmin = ExecuteScallar(query)
                        'Dim str As String = "PERSONNEL RATTACHÉ " & txtDebutDivis.Text & TxtLibDecoup.Text
                        'Dim partS() As String = str.Replace("'", "").Replace("  ", " ").Replace(" le", "").Replace(" la", "").Replace(" les", "").Replace(" l'", "").Replace(" de", "").Replace(" du", "").Replace(" des", "").Replace(" d'", "").Split(" "c)
                        'Dim CodeS As String = ""
                        'For Each elt In partS
                        '    CodeS = CodeS & Mid(elt, 1, 1).ToUpper
                        'Next

                        'query = "select AbregeService from T_Service where AbregeService='" & CodeS & "'"
                        'Dim res = ExecuteScallar(query)
                        'Dim cpte As Decimal = 0
                        'While Len(res) <> 0
                        '    cpte += 1
                        '    query = "select AbregeService from T_Service where AbregeService='" & CodeS & cpte & "'"
                        '    res = ExecuteScallar(query)
                        '    If Len(res) = 0 Then
                        '        res = CodeS & cpte
                        '        Exit While
                        '    End If
                        'End While
                        'If Len(res) = 0 Then
                        '    res = CodeS
                        'End If
                        'query = "INSERT INTO T_Service VALUES(null," & RefDecoupAdmin & ",'PERSONNEL RATTACHÉ A LA " & txtDebutDivis.Text & EnleverApost(Trim(TxtLibDecoup.Text)) & "','" & res & "'," & codeZone(cmbZone.SelectedIndex) & ",0,'" & ProjetEnCours & "','" & Now & "','" & Now & "','" & CodeUtilisateur & "')"
                        'ExecuteNonQuery(query)
                        'Dim LastServiceCode = ExecuteScallar("Select Max(CodeService) as Code from t_service")

                        'str = "DIRECTEUR " & Trim(TxtLibDecoup.Text)
                        'partS = str.Replace("'", "").Replace("  ", " ").Replace(" le", "").Replace(" la", "").Replace(" les", "").Replace(" l'", "").Replace(" de", "").Replace(" du", "").Replace(" des", "").Replace(" d'", "").Split(" "c)
                        'CodeS = ""
                        'For Each elt In partS
                        '    CodeS = CodeS & Mid(elt, 1, 1).ToUpper
                        'Next

                        'query = "select CodeFonction from t_fonction where CodeFonction='" & CodeS & "'"
                        'res = ExecuteScallar(query)
                        'cpte = 0
                        'While Len(res) <> 0
                        '    cpte += 1
                        '    query = "select CodeFonction from t_fonction where CodeFonction='" & CodeS & cpte & "'"
                        '    res = ExecuteScallar(query)
                        '    If Len(res) = 0 Then
                        '        res = CodeS & cpte
                        '        Exit While
                        '    End If
                        'End While
                        'If Len(res) = 0 Then
                        '    res = CodeS
                        'End If

                        'partS = Trim(TxtLibDecoup.Text).Split(" "c)
                        'Dim NewLibDecoup As String = ""
                        'If Mid(partS(0), Len(partS(0))).ToUpper() = "S" Then
                        '    partS(0) = Mid(partS(0), 1, Len(partS(0)) - 1) 'Enlève S si il n'y en a
                        'End If
                        'If Len(partS(0)) > 3 Then
                        '    If Mid(partS(0), Len(partS(0)) - 3).ToUpper() = "IERE" Or Mid(partS(0), Len(partS(0)) - 3).ToUpper() = "IÈRE" Then
                        '        partS(0) = Mid(partS(0), 1, Len(partS(0)) - 4) & "IER"
                        '        For i = 0 To partS.Length - 1
                        '            NewLibDecoup += partS(i) & " "
                        '        Next
                        '    ElseIf Mid(partS(0), Len(partS(0)) - 1).ToUpper() = "VE" Then
                        '        partS(0) = Mid(partS(0), 1, Len(partS(0)) - 2) & "F"
                        '        For i = 0 To partS.Length - 1
                        '            NewLibDecoup += partS(i) & " "
                        '        Next
                        '    ElseIf Mid(partS(0), Len(partS(0)) - 3).ToUpper() = "ELLE" Then
                        '        partS(0) = Mid(partS(0), 1, Len(partS(0)) - 4) & "EL"
                        '        For i = 0 To partS.Length - 1
                        '            NewLibDecoup += partS(i) & " "
                        '        Next
                        '    Else
                        '        For i = 0 To partS.Length - 1
                        '            NewLibDecoup += partS(i) & " "
                        '        Next
                        '    End If
                        'Else
                        '    For i = 0 To partS.Length - 1
                        '        NewLibDecoup += partS(i) & " "
                        '    Next
                        'End If

                        'If Val(CodeSup) <> 0 Then
                        '    query = "select RefFonction from t_fonction where LibelleFonction LIKE 'DIRECTEUR%' and RefDecoupAdmin=" & CodeSup
                        '    Dim CodeBoss As String = ""
                        '    CodeBoss = ExecuteScallar(query)
                        '    query = "INSERT INTO t_fonction VALUES(null,'" & res & "','DIRECTEUR " & EnleverApost(Trim(NewLibDecoup)) & "'," & CodeBoss & "," & LastServiceCode & "," & RefDecoupAdmin & ",'" & Now & "','" & Now & "','" & CodeUtilisateur & "')"
                        'Else
                        '    query = "INSERT INTO t_fonction VALUES(null,'" & res & "','DIRECTEUR " & EnleverApost(Trim(NewLibDecoup)) & "',0," & LastServiceCode & "," & RefDecoupAdmin & ",'" & Now & "','" & Now & "','" & CodeUtilisateur & "')"
                        'End If
                        'ExecuteNonQuery(query)

                        MessageBox.Show("Division enregistrée avec succès.", "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    TxtLibDecoup.Text = ""
                    'Rechargement du combo sup et du grid division
                    ChargerDivision()
                    ChargerDivSup()

                End If
            Catch my As MySqlException
                If my.ErrorCode = -2147467259 Then
                    MessageBox.Show("La division " & Trim(TxtLibDecoup.Text) & " existe déjà." & vbNewLine & my.ToString(), "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    MessageBox.Show("Erreur : Information non disponible." & vbNewLine & my.ToString(), "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                MessageBox.Show("Erreur : " & vbNewLine & ex.ToString, "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        Else
            MessageBox.Show("Veuillez entrer correctement les informations svp.", "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub

    Private Sub ChargerDivision()

        dtDivision.Columns.Clear()

        dtDivision.Columns.Add("Code", Type.GetType("System.String"))
        dtDivision.Columns.Add("Ref", Type.GetType("System.String"))
        dtDivision.Columns.Add("Libellé", Type.GetType("System.String"))
        dtDivision.Columns.Add("Couverture", Type.GetType("System.String"))
        dtDivision.Columns.Add("Zone", Type.GetType("System.String"))
        dtDivision.Columns.Add("Dépend de", Type.GetType("System.String"))

        Dim cptr As Decimal = 0

        'Dim Reader As MySqlDataReader

        query = "select D.RefDecoupAdmin, D.LibelleDivision, D.RefDecoupSup, P.NiveauStr, P.LibellePlan, D.LibZone from T_DivisionAdministrative as D, T_PlanAdministratif as P where D.RefPlan=P.RefPlan and P.CodeProjet=D.CodeProjet and P.CodeProjet='" & ProjetEnCours & "' order by P.NiveauStr, D.RefDecoupSup, D.LibelleDivision"
        dtDivision.Rows.Clear()
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtDivision.NewRow()

            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = rw(0).ToString
            drS(2) = MettreApost(rw(1).ToString)
            drS(3) = MettreApost(rw(4).ToString)
            drS(4) = MettreApost(rw(5).ToString)
            drS(5) = DivSup(rw(2).ToString)

            dtDivision.Rows.Add(drS)
        Next

        GridDecoup.DataSource = dtDivision

        ViewDecoup.Columns(0).Visible = False
        ViewDecoup.Columns(1).Visible = False
        ViewDecoup.Columns(2).Width = GridDecoup.Width - 272
        ViewDecoup.Columns(3).Width = 100
        ViewDecoup.Columns(4).Width = 170
        ViewDecoup.Columns(5).Width = 200

        'ViewDecoup.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        ViewDecoup.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

        ColorRowGrid(ViewDecoup, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub

    Private Function DivSup(ByVal CodSup As String) As String

        Dim TexteRet As String = ""

        'Dim Reader As MySqlDataReader

        query = "select LibelleDivision from T_DivisionAdministrative where RefDecoupAdmin='" & CodSup & "'"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            TexteRet = MettreApost(rw(0).ToString)
        Next
        Return TexteRet

    End Function

    Private Sub BtQuitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        TxtLibDecoup.Text = ""
        CmbDecoupSup.Text = ""
        cmbZone.Text = ""
        CmbDecoupSup.Properties.Items.Clear()
        cmbZone.Properties.Items.Clear()
        CodeSup = "0"
        GbAjoutDecoup.Visible = False
    End Sub
    Private Sub PerpetualDeleteDiviAdmin(RefDiviAdmin As Decimal)
        Try
            query = "select RefDecoupAdmin from t_divisionadministrative where RefDecoupSup=" & RefDiviAdmin
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw As DataRow In dt.Rows
                    query = "select CodeService from T_Service where CodeProjet='" & ProjetEnCours & "' and RefDecoupAdmin=" & rw(0)
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    If dt0.Rows.Count > 0 Then
                        For Each rw0 As DataRow In dt0.Rows
                            DeleteDiviAdminService(rw0(0))
                        Next
                    End If
                    query = "DELETE from T_DivisionAdministrative where RefDecoupAdmin=" & rw(0)
                    ExecuteNonQuery(query)
                    PerpetualDeleteDiviAdmin(CInt(rw(0)))
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("Erreur : Information non disponible." & vbNewLine & ex.ToString(), "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub DeleteDiviAdminService(codeService As Decimal)
        Try
            query = "delete from T_Service where CodeService=" & codeService
            ExecuteNonQuery(query)
            query = "Delete from t_fonction where CodeService=" & codeService
            ExecuteNonQuery(query)
            Service.PerpetualDeleteService(codeService)
        Catch ex As Exception
            MessageBox.Show("Erreur : Information non disponible." & vbNewLine & ex.ToString(), "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub GridDecoup_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridDecoup.DoubleClick

        Dim DecoupASupp As String = ""
        If (ViewDecoup.RowCount > 0) Then
            DrX = ViewDecoup.GetDataRow(ViewDecoup.FocusedRowHandle)
            DecoupASupp = DrX(1).ToString
            Dim Decoup As String = DrX(2).ToString()

            Dim RepConf As DialogResult = MessageBox.Show("Attention !!!" & vbNewLine & "Supprimer une division administrative aura pour effet la suppression de toutes les directions rattachées à celle-ci et leurs différents services." & vbNewLine & "Voulez-vous continuer le processus de suppression de [" & Decoup.ToString & "] ?", "ClearProject", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)

            If (RepConf = DialogResult.Yes) Then

                query = "select * from T_Service where RefDecoupAdmin='" & DecoupASupp & "' and CodeProjet='" & ProjetEnCours & "' and NomService<>'PERSONNEL RATTACHÉ A LA " & EnleverApost(Decoup.ToString()) & "'"
                Dim dt = ExcecuteSelectQuery(query)
                If dt.Rows.Count > 0 Then
                    MsgBox("Suppression réfusée, " & Decoup & " est en cours d'utilisation!", MsgBoxStyle.Information)
                    Exit Sub
                End If

                query = "DELETE from T_DivisionAdministrative where RefDecoupAdmin=" & DecoupASupp
                ExecuteNonQuery(query)

                query = "select CodeService from T_Service where CodeProjet='" & ProjetEnCours & "' and RefDecoupAdmin=" & DecoupASupp
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                If dt0.Rows.Count > 0 Then
                    For Each rw0 As DataRow In dt0.Rows
                        DeleteDiviAdminService(rw0(0))
                    Next
                End If
                PerpetualDeleteDiviAdmin(DecoupASupp)
                MessageBox.Show("Division [" & Decoup & "] supprimée avec succès.", "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Information)

                DecoupASupp = ""
                ChargerDivision()

            End If
        End If

    End Sub
    Private Sub CmbDecoupSup_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbDecoupSup.SelectedIndexChanged
        query = "select RefDecoupAdmin from T_DivisionAdministrative where LibelleDivision='" & EnleverApost(CmbDecoupSup.Text) & "'"
        CodeSup = ExecuteScallar(query)
    End Sub

    Private Sub GridDecoup_Click(sender As Object, e As EventArgs) Handles GridDecoup.Click
        If (ViewDecoup.RowCount > 0) Then
            DrX = ViewDecoup.GetDataRow(ViewDecoup.FocusedRowHandle)
            If GbAjoutDecoup.Visible Then BtQuitter.PerformClick()
            ColorRowGrid(ViewDecoup, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewDecoup, "[Ref]='" & DrX(1) & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)
        End If

    End Sub
End Class