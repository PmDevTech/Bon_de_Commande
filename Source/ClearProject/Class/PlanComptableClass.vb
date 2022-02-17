Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports System.Data.SqlClient
Imports DevExpress.XtraEditors.Repository

Public Class PlanComptableClass
    Private _dtSource As DataTable
    Private _resqlconno As Decimal
    Private _currentPage As Decimal
    Private _pageSize As Decimal
    Private _CpteMin As Decimal
    Private _CpteMax As Decimal
    'Private _maxRec As Decimal
    Private _pageCount As Decimal

    Public Property dtSource As DataTable
        Get
            Return _dtSource
        End Get
        Set(ByVal value As DataTable)
            _dtSource = value
        End Set
    End Property

    Public Property CpteMin As Decimal
        Get
            Return _CpteMin
        End Get
        Set(ByVal value As Decimal)
            _CpteMin = value
        End Set
    End Property
    Public Property CpteMax As Decimal
        Get
            Return _CpteMax
        End Get
        Set(ByVal value As Decimal)
            _CpteMax = value
        End Set
    End Property
    Public Property Resqlconno As Decimal
        Get
            Return _resqlconno
        End Get
        Set(ByVal value As Decimal)
            _resqlconno = value
        End Set
    End Property

    Public Property CurrentPage As Decimal
        Get
            Return _currentPage
        End Get
        Set(ByVal value As Decimal)
            _currentPage = value
        End Set
    End Property

    Public Property PageSize As Decimal
        Get
            Return _pageSize
        End Get
        Set(ByVal value As Decimal)
            _pageSize = value
        End Set
    End Property

    'Public Property MaxRec As Decimal
    '    Get
    '        Return _maxRec
    '    End Get
    '    Set(ByVal value As Decimal)
    '        _maxRec = value
    '    End Set
    'End Property

    Public Property PageCount As Decimal
        Get
            Return _pageCount
        End Get
        Set(ByVal value As Decimal)
            _pageCount = value
        End Set
    End Property

    Public Sub LoadPage(ByVal GridControl As GridControl, ByVal Page As Decimal)

        dtcomptable.Columns.Clear()
        dtcomptable.Columns.Add("Choix", Type.GetType("System.Boolean"))
        dtcomptable.Columns.Add("Code", Type.GetType("System.String"))
        dtcomptable.Columns.Add("Libelle", Type.GetType("System.String"))
        dtcomptable.Rows.Clear()

        If Page = 1 Then
            CpteMin = 1
            CpteMax = PageSize + 1
        Else
            CpteMin = ((Page - 1) * PageSize) + 2
            CpteMax = (Page * PageSize) + 1
        End If
        Dim cpt As Decimal = 0
        Dim Counted As Decimal = 0
        'Dim i As Decimal = 0
        query = "Select code_cl0, libelle_cl0  from t_comp_classe0 ORDER BY code_cl0"
        Dim dt = ExcecuteSelectQuery(query)
        'InputBox(dt.Rows.Count, "dt", query)
        For Each rwx0 As DataRow In dt.Rows
            Counted += 1
            'i += 1
            If Counted > CpteMax Then
                FormatView()
                Exit For
            End If
            If Counted < CpteMax And Counted >= CpteMin Then
                'SuccesMsg("dt :" & Counted)
                cpt += 1
                Dim drS = dtcomptable.NewRow()
                drS(0) = False
                drS(1) = rwx0(0).ToString
                drS(2) = StrConv(MettreApost(rwx0(1).ToString), VbStrConv.ProperCase)
                dtcomptable.Rows.Add(drS)
                compte_general.LgListComptable.DataSource = dtcomptable
            End If

            query = "select code_cl, libelle_cl from t_comp_classe where code_cl0 = '" & rwx0(0).ToString & "' order by code_cl" ' Limit " & Limite.ToString
            Dim dt1 = ExcecuteSelectQuery(query)
            'InputBox(dt1.Rows.Count, "dt1", query)
            For Each rwx1 As DataRow In dt1.Rows
                Counted += 1
                If Counted > CpteMax Then
                    FormatView()
                    Exit For
                End If
                If Counted < CpteMax And Counted >= CpteMin Then
                    'SuccesMsg("dt1 :" & Counted)
                    cpt += 1
                    Dim drS1 = dtcomptable.NewRow()
                    drS1(0) = False
                    drS1(1) = rwx1(0).ToString
                    drS1(2) = StrConv(MettreApost(rwx1(1).ToString), VbStrConv.ProperCase)
                    dtcomptable.Rows.Add(drS1)
                    compte_general.LgListComptable.DataSource = dtcomptable
                End If

                query = "select code_cln1, libelle_cln1 from t_comp_classen1 where code_cl = '" & rwx1(0).ToString & "' order by code_cln1" ' Limit " & Limite.ToString
                Dim dt2 = ExcecuteSelectQuery(query)
                'InputBox(dt2.Rows.Count, "dt2", query)
                For Each rwx2 As DataRow In dt2.Rows
                    Counted += 1
                    If Counted > CpteMax Then
                        FormatView()
                        Exit For
                    End If
                    If Counted < CpteMax And Counted >= CpteMin Then
                        'SuccesMsg("dt2 :" & Counted)
                        cpt += 1
                        Dim drS2 = dtcomptable.NewRow()
                        drS2(0) = False
                        drS2(1) = rwx2(0).ToString
                        drS2(2) = StrConv(MettreApost(rwx2(1).ToString), VbStrConv.ProperCase)
                        dtcomptable.Rows.Add(drS2)
                        compte_general.LgListComptable.DataSource = dtcomptable
                    End If

                    query = "select code_cln2, libelle_cln2 from t_comp_classen2 where code_cln1 = '" & rwx2(0).ToString & "' order by code_cln2" ' Limit " & Limite.ToString
                    Dim dt3 = ExcecuteSelectQuery(query)
                    'InputBox(dt3.Rows.Count, "dt3", query)
                    For Each rwx3 As DataRow In dt3.Rows
                        Counted += 1
                        If Counted > CpteMax Then
                            FormatView()
                            Exit For
                        End If
                        If Counted < CpteMax And Counted >= CpteMin Then
                            'SuccesMsg("dt3 :" & Counted)
                            cpt += 1
                            Dim drS3 = dtcomptable.NewRow()
                            drS3(0) = False
                            drS3(1) = rwx3(0).ToString
                            drS3(2) = StrConv(MettreApost(rwx3(1).ToString), VbStrConv.ProperCase)
                            dtcomptable.Rows.Add(drS3)
                            compte_general.LgListComptable.DataSource = dtcomptable
                        End If

                        query = "select code_sc, libelle_sc from t_comp_sous_classe where code_cln2 = '" & rwx3(0).ToString & "' order by code_sc" ' Limit " & Limite.ToString
                        Dim dt4 = ExcecuteSelectQuery(query)
                        'InputBox(dt4.Rows.Count, "dt4", query)
                        For Each rwx4 As DataRow In dt4.Rows
                            Counted += 1
                            If Counted > CpteMax Then
                                FormatView()
                                Exit For
                            End If
                            If Counted < CpteMax And Counted >= CpteMin Then
                                'SuccesMsg("dt4 :" & Counted)
                                cpt += 1
                                Dim drS4 = dtcomptable.NewRow()
                                drS4(0) = False
                                drS4(1) = rwx4(0).ToString
                                drS4(2) = StrConv(MettreApost(rwx4(1).ToString), VbStrConv.ProperCase)
                                dtcomptable.Rows.Add(drS4)
                                compte_general.LgListComptable.DataSource = dtcomptable
                            End If
                        Next
                    Next
                Next
            Next
        Next
        FormatView()
    End Sub
    Private Sub FormatView()
        On Error Resume Next
        Dim edit As RepositoryItemCheckEdit = New RepositoryItemCheckEdit()
        edit.ValueChecked = True
        edit.ValueUnchecked = False
        AddHandler edit.CheckedChanged, AddressOf compte_general.CheckEdit_CheckedChanged
        compte_general.ViewComptable.Columns("Choix").ColumnEdit = edit
        compte_general.LgListComptable.RepositoryItems.Add(edit)
        compte_general.ViewComptable.OptionsBehavior.Editable = True

        compte_general.ViewComptable.Columns(0).Width = 20
        compte_general.ViewComptable.Columns("Code").OptionsColumn.AllowEdit = False
        compte_general.ViewComptable.Columns("Libelle").OptionsColumn.AllowEdit = False

        compte_general.ViewComptable.OptionsView.ColumnAutoWidth = True
        compte_general.ViewComptable.OptionsBehavior.AutoExpandAllGroups = True
        compte_general.ViewComptable.VertScrollVisibility = True
        compte_general.ViewComptable.HorzScrollVisibility = True
    End Sub
    Public Sub RechPage(ByVal GridControl As GridControl, ByVal Page As Decimal)

        Dim cpt As Decimal = 0

        dtcomptable.Columns.Clear()
        dtcomptable.Columns.Add("Choix", Type.GetType("System.Boolean"))
        dtcomptable.Columns.Add("Code", Type.GetType("System.String"))
        dtcomptable.Columns.Add("Libelle", Type.GetType("System.String"))
        dtcomptable.Rows.Clear()
        Dim nbre As Decimal = 0
        If Len(compte_general.TxtRechercher.Text) = 1 Then
            query = "Select COUNT(code_cl0) from t_comp_classe0 where code_cl0 = '" & EnleverApost(compte_general.TxtRechercher.Text) & "' or libelle_cl0='%" & EnleverApost(compte_general.TxtRechercher.Text) & "%' ORDER BY code_cl0"
            nbre += Val(ExecuteScallar(query))
            query = "SELECT COUNT(code_sc) from t_comp_sous_classe c5, t_comp_classen2 c4, t_comp_classen1 c3, t_comp_classe c2 WHERE c5.CODE_CLN2=c4.CODE_CLN2 AND c4.CODE_CLN1=c3.CODE_CLN1 AND c3.CODE_CL=c2.CODE_CL AND (CODE_CL0 IN (Select code_cl0 from t_comp_classe0 where code_cl0 = '" & EnleverApost(compte_general.TxtRechercher.Text) & "' or libelle_cl0='%" & EnleverApost(compte_general.TxtRechercher.Text) & "%'))"
            nbre += Val(ExecuteScallar(query))

        ElseIf Len(compte_general.TxtRechercher.Text) = 2 Then
            query = "select count(code_cl) from t_comp_classe where code_cl = '" & EnleverApost(compte_general.TxtRechercher.Text) & "' or libelle_cl LIKE '%" & EnleverApost(compte_general.TxtRechercher.Text) & "%'"
            nbre += Val(ExecuteScallar(query))
            query = "SELECT COUNT(code_sc) from t_comp_sous_classe c5, t_comp_classen2 c4, t_comp_classen1 c3 WHERE c5.CODE_CLN2=c4.CODE_CLN2 AND c4.CODE_CLN1=c3.CODE_CLN1 AND c3.CODE_CL IN (Select code_cl from t_comp_classe where code_cl = '" & EnleverApost(compte_general.TxtRechercher.Text) & "' or libelle_cl LIKE '%" & EnleverApost(compte_general.TxtRechercher.Text) & "%')"
            nbre += Val(ExecuteScallar(query))

        ElseIf Len(compte_general.TxtRechercher.Text) = 3 Then
            query = "select COUNT(code_cln1) from t_comp_classen1 where code_cln1 = '" & EnleverApost(compte_general.TxtRechercher.Text) & "' or libelle_cln1 LIKE '%" & EnleverApost(compte_general.TxtRechercher.Text) & "%'"
            nbre += Val(ExecuteScallar(query))
            query = "SELECT COUNT(code_sc) from t_comp_sous_classe c5, t_comp_classen2 c4 WHERE c5.CODE_CLN2=c4.CODE_CLN2 AND c4.CODE_CLN1 IN (Select code_cln1 from t_comp_classen1 where code_cln1 = '" & EnleverApost(compte_general.TxtRechercher.Text) & "' or libelle_cln1 LIKE '%" & EnleverApost(compte_general.TxtRechercher.Text) & "%')"
            nbre += Val(ExecuteScallar(query))

        ElseIf Len(compte_general.TxtRechercher.Text) = 4 Then
            query = "select COUNT(code_cln2) from t_comp_classen2 where code_cln2 = '" & EnleverApost(compte_general.TxtRechercher.Text) & "' or libelle_cln2 LIKE '%" & EnleverApost(compte_general.TxtRechercher.Text) & "%'"
            nbre += Val(ExecuteScallar(query))
            query = "SELECT COUNT(code_sc) from t_comp_sous_classe c5 WHERE CODE_CLN2 IN (Select CODE_CLN2 from t_comp_classen2 where code_cln2 = '" & EnleverApost(compte_general.TxtRechercher.Text) & "' or libelle_cln2 LIKE '%" & EnleverApost(compte_general.TxtRechercher.Text) & "%')"
            nbre += Val(ExecuteScallar(query))

        ElseIf Len(compte_general.TxtRechercher.Text) > 4 Then
            query = "Select COUNT(code_sc) from t_comp_sous_classe where code_sc = '" & EnleverApost(compte_general.TxtRechercher.Text) & "' or libelle_sc LIKE '%" & EnleverApost(compte_general.TxtRechercher.Text) & "%'"
            nbre += Val(ExecuteScallar(query))
        End If

        PageCount = nbre \ PageSize
        If nbre Mod PageSize <> 0 Then
            PageCount += 1
        End If
        If Page = 1 Then
            CpteMin = 1
            CpteMax = PageSize + 1
        Else
            CpteMin = ((Page - 1) * PageSize) + 1
            CpteMax = (Page * PageSize) + 1
        End If
        compte_general.TxtPage.Text = "Page " & Page & "/" & PageCount

        If Len(compte_general.TxtRechercher.Text) = 1 Then

            Dim Counted As Decimal = 0
            'Dim i As Decimal = 0
            query = "Select code_cl0, libelle_cl0  from t_comp_classe0 where code_cl0 = '" & EnleverApost(compte_general.TxtRechercher.Text) & "' or libelle_cl0='%" & EnleverApost(compte_general.TxtRechercher.Text) & "%' ORDER BY code_cl0"
            Dim dt = ExcecuteSelectQuery(query)

            For Each rwx0 As DataRow In dt.Rows
                Counted += 1
                'i += 1
                If Counted > CpteMax Then
                    FormatView()
                    Exit For
                End If
                If Counted < CpteMax And Counted >= CpteMin Then
                    'SuccesMsg("dt :" & Counted)
                    cpt += 1
                    Dim drS = dtcomptable.NewRow()
                    drS(0) = False
                    drS(1) = rwx0(0).ToString
                    drS(2) = StrConv(MettreApost(rwx0(1).ToString), VbStrConv.ProperCase)
                    dtcomptable.Rows.Add(drS)
                    compte_general.LgListComptable.DataSource = dtcomptable
                End If

                'If (Page = 1) Then
                '    query = "select code_cl, libelle_cl from t_comp_classe where code_cl0 = '" & rwx0(0).ToString & "' order by code_cl Limit " & Limite.ToString
                'Else
                '    Dim PreviousPageOffSet = (Page - 1) * Limite
                '    Dim PageOffSet = Page * Limite
                '    query = "select code_cl, libelle_cl from t_comp_classe where code_cl0 = '" & rwx0(0).ToString & "' order by code_cl Limit " & PreviousPageOffSet.ToString & ", " & PageOffSet.ToString
                'End If

                query = "select code_cl, libelle_cl from t_comp_classe where code_cl0 = '" & rwx0(0).ToString & "' order by code_cl" ' Limit " & Limite.ToString
                Dim dt1 = ExcecuteSelectQuery(query)
                'InputBox(dt1.Rows.Count, "dt1", query)
                For Each rwx1 As DataRow In dt1.Rows
                    Counted += 1
                    If Counted > CpteMax Then
                        FormatView()
                        Exit For
                    End If
                    If Counted < CpteMax And Counted >= CpteMin Then
                        'SuccesMsg("dt1 :" & Counted)
                        cpt += 1
                        Dim drS1 = dtcomptable.NewRow()
                        drS1(0) = False
                        drS1(1) = rwx1(0).ToString
                        drS1(2) = StrConv(MettreApost(rwx1(1).ToString), VbStrConv.ProperCase)
                        dtcomptable.Rows.Add(drS1)
                        compte_general.LgListComptable.DataSource = dtcomptable
                    End If

                    'If (Page = 1) Then
                    '    query = "select code_cln1, libelle_cln1 from t_comp_classen1 where code_cl = '" & rwx1(0).ToString & "' order by code_cln1 Limit " & Limite.ToString
                    'Else
                    '    Dim PreviousPageOffSet = (Page - 1) * Limite
                    '    Dim PageOffSet = Page * Limite
                    '    query = "select code_cln1, libelle_cln1 from t_comp_classen1 where code_cl = '" & rwx1(0).ToString & "' order by code_cln1 Limit " & PreviousPageOffSet.ToString & ", " & PageOffSet.ToString
                    'End If

                    query = "select code_cln1, libelle_cln1 from t_comp_classen1 where code_cl = '" & rwx1(0).ToString & "' order by code_cln1" ' Limit " & Limite.ToString
                    Dim dt2 = ExcecuteSelectQuery(query)
                    'InputBox(dt2.Rows.Count, "dt2", query)
                    For Each rwx2 As DataRow In dt2.Rows
                        Counted += 1
                        If Counted > CpteMax Then
                            FormatView()
                            Exit For
                        End If
                        If Counted < CpteMax And Counted >= CpteMin Then
                            'SuccesMsg("dt2 :" & Counted)
                            cpt += 1
                            Dim drS2 = dtcomptable.NewRow()
                            drS2(0) = False
                            drS2(1) = rwx2(0).ToString
                            drS2(2) = StrConv(MettreApost(rwx2(1).ToString), VbStrConv.ProperCase)
                            dtcomptable.Rows.Add(drS2)
                            compte_general.LgListComptable.DataSource = dtcomptable
                        End If


                        'If (Page = 1) Then
                        '    query = "select code_cln2, libelle_cln2 from t_comp_classen2 where code_cln1 = '" & rwx2(0).ToString & "' order by code_cln2 Limit " & Limite.ToString
                        'Else
                        '    Dim PreviousPageOffSet = (Page - 1) * Limite
                        '    Dim PageOffSet = Page * Limite
                        '    query = "select code_cln2, libelle_cln2 from t_comp_classen2 where code_cln1 = '" & rwx2(0).ToString & "' order by code_cln2 Limit " & PreviousPageOffSet.ToString & ", " & PageOffSet.ToString
                        'End If

                        query = "select code_cln2, libelle_cln2 from t_comp_classen2 where code_cln1 = '" & rwx2(0).ToString & "' order by code_cln2" ' Limit " & Limite.ToString
                        Dim dt3 = ExcecuteSelectQuery(query)
                        'InputBox(dt3.Rows.Count, "dt3", query)
                        For Each rwx3 As DataRow In dt3.Rows
                            Counted += 1
                            If Counted > CpteMax Then
                                FormatView()
                                Exit For
                            End If
                            If Counted < CpteMax And Counted >= CpteMin Then
                                'SuccesMsg("dt3 :" & Counted)
                                cpt += 1
                                Dim drS3 = dtcomptable.NewRow()
                                drS3(0) = False
                                drS3(1) = rwx3(0).ToString
                                drS3(2) = StrConv(MettreApost(rwx3(1).ToString), VbStrConv.ProperCase)
                                dtcomptable.Rows.Add(drS3)
                                compte_general.LgListComptable.DataSource = dtcomptable
                            End If

                            'If (Page = 1) Then
                            '    query = "select code_sc, libelle_sc from t_comp_sous_classe where code_cln2 = '" & rwx3(0).ToString & "' order by code_sc Limit " & Limite.ToString
                            'Else
                            '    Dim PreviousPageOffSet = (Page - 1) * Limite
                            '    Dim PageOffSet = Page * Limite
                            '    query = "select code_sc, libelle_sc from t_comp_sous_classe where code_cln2 = '" & rwx3(0).ToString & "' order by code_sc Limit " & PreviousPageOffSet.ToString & ", " & PageOffSet.ToString
                            'End If

                            query = "select code_sc, libelle_sc from t_comp_sous_classe where code_cln2 = '" & rwx3(0).ToString & "' order by code_sc" ' Limit " & Limite.ToString
                            Dim dt4 = ExcecuteSelectQuery(query)
                            'InputBox(dt4.Rows.Count, "dt4", query)
                            For Each rwx4 As DataRow In dt4.Rows
                                Counted += 1
                                If Counted > CpteMax Then
                                    FormatView()
                                    Exit For
                                End If
                                If Counted < CpteMax And Counted >= CpteMin Then
                                    'SuccesMsg("dt4 :" & Counted)
                                    cpt += 1
                                    Dim drS4 = dtcomptable.NewRow()
                                    drS4(0) = False
                                    drS4(1) = rwx4(0).ToString
                                    drS4(2) = StrConv(MettreApost(rwx4(1).ToString), VbStrConv.ProperCase)
                                    dtcomptable.Rows.Add(drS4)
                                    compte_general.LgListComptable.DataSource = dtcomptable
                                End If
                            Next
                        Next
                    Next
                Next
            Next
            FormatView()

        ElseIf Len(compte_general.TxtRechercher.Text) = 2 Then
            Dim Counted As Decimal = 0
            query = "select code_cl, libelle_cl from t_comp_classe where code_cl = '" & EnleverApost(compte_general.TxtRechercher.Text) & "' or libelle_cl LIKE '%" & EnleverApost(compte_general.TxtRechercher.Text) & "%'"
            Dim dt1 = ExcecuteSelectQuery(query)
            'InputBox(dt1.Rows.Count, "dt1", query)
            For Each rwx1 As DataRow In dt1.Rows
                Counted += 1
                If Counted > CpteMax Then
                    FormatView()
                    Exit For
                End If
                If Counted < CpteMax And Counted >= CpteMin Then
                    'SuccesMsg("dt1 :" & Counted)
                    cpt += 1
                    Dim drS1 = dtcomptable.NewRow()
                    drS1(0) = False
                    drS1(1) = rwx1(0).ToString
                    drS1(2) = StrConv(MettreApost(rwx1(1).ToString), VbStrConv.ProperCase)
                    dtcomptable.Rows.Add(drS1)
                    compte_general.LgListComptable.DataSource = dtcomptable
                End If

                query = "select code_cln1, libelle_cln1 from t_comp_classen1 where code_cl = '" & rwx1(0).ToString & "' order by code_cln1" ' Limit " & Limite.ToString
                Dim dt2 = ExcecuteSelectQuery(query)
                'InputBox(dt2.Rows.Count, "dt2", query)
                For Each rwx2 As DataRow In dt2.Rows
                    Counted += 1
                    If Counted > CpteMax Then
                        FormatView()
                        Exit For
                    End If
                    If Counted < CpteMax And Counted >= CpteMin Then
                        'SuccesMsg("dt2 :" & Counted)
                        cpt += 1
                        Dim drS2 = dtcomptable.NewRow()
                        drS2(0) = False
                        drS2(1) = rwx2(0).ToString
                        drS2(2) = StrConv(MettreApost(rwx2(1).ToString), VbStrConv.ProperCase)
                        dtcomptable.Rows.Add(drS2)
                        compte_general.LgListComptable.DataSource = dtcomptable
                    End If


                    query = "select code_cln2, libelle_cln2 from t_comp_classen2 where code_cln1 = '" & rwx2(0).ToString & "' order by code_cln2" ' Limit " & Limite.ToString
                    Dim dt3 = ExcecuteSelectQuery(query)
                    'InputBox(dt3.Rows.Count, "dt3", query)
                    For Each rwx3 As DataRow In dt3.Rows
                        Counted += 1
                        If Counted > CpteMax Then
                            FormatView()
                            Exit For
                        End If
                        If Counted < CpteMax And Counted >= CpteMin Then
                            'SuccesMsg("dt3 :" & Counted)
                            cpt += 1
                            Dim drS3 = dtcomptable.NewRow()
                            drS3(0) = False
                            drS3(1) = rwx3(0).ToString
                            drS3(2) = StrConv(MettreApost(rwx3(1).ToString), VbStrConv.ProperCase)
                            dtcomptable.Rows.Add(drS3)
                            compte_general.LgListComptable.DataSource = dtcomptable
                        End If


                        query = "select code_sc, libelle_sc from t_comp_sous_classe where code_cln2 = '" & rwx3(0).ToString & "' order by code_sc" ' Limit " & Limite.ToString
                        Dim dt4 = ExcecuteSelectQuery(query)
                        'InputBox(dt4.Rows.Count, "dt4", query)
                        For Each rwx4 As DataRow In dt4.Rows
                            Counted += 1
                            If Counted > CpteMax Then
                                FormatView()
                                Exit For
                            End If
                            If Counted < CpteMax And Counted >= CpteMin Then
                                'SuccesMsg("dt4 :" & Counted)
                                cpt += 1
                                Dim drS4 = dtcomptable.NewRow()
                                drS4(0) = False
                                drS4(1) = rwx4(0).ToString
                                drS4(2) = StrConv(MettreApost(rwx4(1).ToString), VbStrConv.ProperCase)
                                dtcomptable.Rows.Add(drS4)
                                compte_general.LgListComptable.DataSource = dtcomptable
                            End If
                        Next
                    Next
                Next
            Next
            FormatView()

        ElseIf Len(compte_general.TxtRechercher.Text) = 3 Then
            Dim Counted As Decimal = 0
            query = "select code_cln1, libelle_cln1 from t_comp_classen1 where code_cln1 = '" & EnleverApost(compte_general.TxtRechercher.Text) & "' or libelle_cln1 LIKE '%" & EnleverApost(compte_general.TxtRechercher.Text) & "%'"
            Dim dt2 = ExcecuteSelectQuery(query)
            For Each rwx2 As DataRow In dt2.Rows
                Counted += 1
                If Counted > CpteMax Then
                    FormatView()
                    Exit For
                End If
                If Counted < CpteMax And Counted >= CpteMin Then
                    cpt += 1
                    Dim drS2 = dtcomptable.NewRow()
                    drS2(0) = False
                    drS2(1) = rwx2(0).ToString
                    drS2(2) = StrConv(MettreApost(rwx2(1).ToString), VbStrConv.ProperCase)
                    dtcomptable.Rows.Add(drS2)
                    compte_general.LgListComptable.DataSource = dtcomptable
                End If

                query = "select code_cln2, libelle_cln2 from t_comp_classen2 where code_cln1 = '" & rwx2(0).ToString & "' order by code_cln2" ' Limit " & Limite.ToString
                Dim dt3 = ExcecuteSelectQuery(query)
                'InputBox(dt3.Rows.Count, "dt3", query)
                For Each rwx3 As DataRow In dt3.Rows
                    Counted += 1
                    If Counted > CpteMax Then
                        FormatView()
                        Exit For
                    End If
                    If Counted < CpteMax And Counted >= CpteMin Then
                        'SuccesMsg("dt3 :" & Counted)
                        cpt += 1
                        Dim drS3 = dtcomptable.NewRow()
                        drS3(0) = False
                        drS3(1) = rwx3(0).ToString
                        drS3(2) = StrConv(MettreApost(rwx3(1).ToString), VbStrConv.ProperCase)
                        dtcomptable.Rows.Add(drS3)
                        compte_general.LgListComptable.DataSource = dtcomptable
                    End If


                    query = "select code_sc, libelle_sc from t_comp_sous_classe where code_cln2 = '" & rwx3(0).ToString & "' order by code_sc" ' Limit " & Limite.ToString
                    Dim dt4 = ExcecuteSelectQuery(query)
                    'InputBox(dt4.Rows.Count, "dt4", query)
                    For Each rwx4 As DataRow In dt4.Rows
                        Counted += 1
                        If Counted > CpteMax Then
                            FormatView()
                            Exit For
                        End If
                        If Counted < CpteMax And Counted >= CpteMin Then
                            'SuccesMsg("dt4 :" & Counted)
                            cpt += 1
                            Dim drS4 = dtcomptable.NewRow()
                            drS4(0) = False
                            drS4(1) = rwx4(0).ToString
                            drS4(2) = StrConv(MettreApost(rwx4(1).ToString), VbStrConv.ProperCase)
                            dtcomptable.Rows.Add(drS4)
                            compte_general.LgListComptable.DataSource = dtcomptable
                        End If
                    Next
                Next
            Next
            FormatView()

        ElseIf Len(compte_general.TxtRechercher.Text) = 4 Then
            Dim Counted As Decimal = 0
            query = "select code_cln2, libelle_cln2 from t_comp_classen2 where code_cln2 = '" & EnleverApost(compte_general.TxtRechercher.Text) & "' or libelle_cln2 LIKE '%" & EnleverApost(compte_general.TxtRechercher.Text) & "%'"
            Dim dt3 = ExcecuteSelectQuery(query)
            'InputBox(dt3.Rows.Count, "dt3", query)
            For Each rwx3 As DataRow In dt3.Rows
                Counted += 1
                If Counted > CpteMax Then
                    FormatView()
                    Exit For
                End If
                If Counted < CpteMax And Counted >= CpteMin Then
                    'SuccesMsg("dt3 :" & Counted)
                    cpt += 1
                    Dim drS3 = dtcomptable.NewRow()
                    drS3(0) = False
                    drS3(1) = rwx3(0).ToString
                    drS3(2) = StrConv(MettreApost(rwx3(1).ToString), VbStrConv.ProperCase)
                    dtcomptable.Rows.Add(drS3)
                    compte_general.LgListComptable.DataSource = dtcomptable
                End If


                query = "select code_sc, libelle_sc from t_comp_sous_classe where code_cln2 = '" & rwx3(0).ToString & "' order by code_sc" ' Limit " & Limite.ToString
                Dim dt4 = ExcecuteSelectQuery(query)
                'InputBox(dt4.Rows.Count, "dt4", query)
                For Each rwx4 As DataRow In dt4.Rows
                    Counted += 1
                    If Counted > CpteMax Then
                        FormatView()
                        Exit For
                    End If
                    If Counted < CpteMax And Counted >= CpteMin Then
                        'SuccesMsg("dt4 :" & Counted)
                        cpt += 1
                        Dim drS4 = dtcomptable.NewRow()
                        drS4(0) = False
                        drS4(1) = rwx4(0).ToString
                        drS4(2) = StrConv(MettreApost(rwx4(1).ToString), VbStrConv.ProperCase)
                        dtcomptable.Rows.Add(drS4)
                        compte_general.LgListComptable.DataSource = dtcomptable
                    End If
                Next
            Next
            FormatView()

        ElseIf Len(compte_general.TxtRechercher.Text) > 4 Then

            Dim Counted As Decimal = 0
            query = "select code_sc, libelle_sc from t_comp_sous_classe where code_sc = '" & EnleverApost(compte_general.TxtRechercher.Text) & "' or libelle_sc LIKE '%" & EnleverApost(compte_general.TxtRechercher.Text) & "%'"
            Dim dt4 = ExcecuteSelectQuery(query)
            'InputBox(dt4.Rows.Count, "dt4", query)
            For Each rwx4 As DataRow In dt4.Rows
                Counted += 1
                If Counted > CpteMax Then
                    FormatView()
                    Exit For
                End If
                If Counted < CpteMax And Counted >= CpteMin Then
                    'SuccesMsg("dt4 :" & Counted)
                    cpt += 1
                    Dim drS4 = dtcomptable.NewRow()
                    drS4(0) = False
                    drS4(1) = rwx4(0).ToString
                    drS4(2) = StrConv(MettreApost(rwx4(1).ToString), VbStrConv.ProperCase)
                    dtcomptable.Rows.Add(drS4)
                    compte_general.LgListComptable.DataSource = dtcomptable
                End If
            Next
            FormatView()

        End If


    End Sub
End Class
