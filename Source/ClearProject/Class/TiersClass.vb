Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports System.Data.SqlClient

Public Class TiersClass
    Private _dtSource As DataTable
    Private _resqlconno As Decimal
    Private _currentPage As Decimal
    Private _pageSize As Decimal
    Private _maxRec As Decimal
    Private _pageCount As Decimal

    Public Property dtSource As DataTable
        Get
            Return _dtSource
        End Get
        Set(ByVal value As DataTable)
            _dtSource = value
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

    Public Property MaxRec As Decimal
        Get
            Return _maxRec
        End Get
        Set(ByVal value As Decimal)
            _maxRec = value
        End Set
    End Property

    Public Property PageCount As Decimal
        Get
            Return _pageCount
        End Get
        Set(ByVal value As Decimal)
            _pageCount = value
        End Set
    End Property

    Public Sub LoadPage(ByVal GridControl As GridControl, ByVal Page As Decimal)
        Dim dtCompteTier As New DataTable
        dtCompteTier.Columns.Clear()
        dtCompteTier.Columns.Add("Choix", Type.GetType("System.Boolean"))
        dtCompteTier.Columns.Add("Code", Type.GetType("System.String"))
        dtCompteTier.Columns.Add("Intitulé", Type.GetType("System.String"))
        dtCompteTier.Columns.Add("Abréviation", Type.GetType("System.String"))
        dtCompteTier.Columns.Add("Adresse", Type.GetType("System.String"))
        dtCompteTier.Columns.Add("Email", Type.GetType("System.String"))

        'dtCompteTier.Columns.Add("Compte Collectif", Type.GetType("System.String"))
        dtCompteTier.Rows.Clear()

        If (Page = 1) Then
            query = "select c.ABREGE_CPT, c.CODE_CPT, t.LIBELLE_TCPT, NOM_CPT, ADRESSE_CPT, EMAIL, CODE_CPT from t_comp_compte c, t_comp_type_compte t where c.CODE_TCPT=t.CODE_TCPT and c.code_projet='" & ProjetEnCours & "' order by c.CODE_CPT Limit " & PageSize.ToString & ""
        Else
            Dim PreviousPageOffSet = (Page - 1) * PageSize
            query = "select c.ABREGE_CPT, c.CODE_CPT, t.LIBELLE_TCPT, NOM_CPT, ADRESSE_CPT, EMAIL, CODE_CPT from t_comp_compte c, t_comp_type_compte t where c.CODE_TCPT=t.CODE_TCPT and c.code_projet='" & ProjetEnCours & "' order by c.CODE_CPT Limit " & PageSize & " OFFSET " & PreviousPageOffSet
        End If

        'If MaxRec = 0 Then
        '    Plan_tiers.TxtPage.Text = Page & "/" & MaxRec + 1
        'Else
        '    Plan_tiers.TxtPage.Text = Page & "/" & MaxRec
        'End If

        'Plan_tiers.TxtPage.Text = Page & "/" & MaxRec

        Dim cptr As Decimal = 0
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtCompteTier.NewRow()
            drS(0) = False
            drS(1) = rw("CODE_CPT").ToString
            drS(2) = MettreApost(rw("NOM_CPT").ToString)
            drS(3) = MettreApost(rw("ABREGE_CPT").ToString)
            drS(4) = MettreApost(rw("ADRESSE_CPT").ToString)
            drS(5) = MettreApost(rw("EMAIL").ToString)
            dtCompteTier.Rows.Add(drS)
            'query = "select NOM_CPT,ADRESSE_CPT,EMAIL,CODE_CPT from t_comp_compte where ABREGE_CPT='" & rwx("ABREGE_CPT").ToString & "' and CODE_CPT='" & rwx("CODE_CPT").ToString & "'"
            'dt = ExcecuteSelectQuery(query)
            'For Each rwx0 As DataRow In dt.Rows

            'Next
        Next

        GridControl.DataSource = dtCompteTier
        'Plan_tiers.ViewCptTiers.Columns("Compte Collectif").OptionsColumn.AllowEdit = False
    End Sub

    Public Sub RechPage(ByVal GridControl As GridControl, ByVal Page As Decimal)
        On Error Resume Next
        Dim dtCompteTier As New DataTable
        dtCompteTier.Columns.Clear()
        dtCompteTier.Columns.Add("Choix", Type.GetType("System.Boolean"))
        dtCompteTier.Columns.Add("Code", Type.GetType("System.String"))
        dtCompteTier.Columns.Add("Intitulé", Type.GetType("System.String"))
        dtCompteTier.Columns.Add("Abréviation", Type.GetType("System.String"))
        dtCompteTier.Columns.Add("Adresse", Type.GetType("System.String"))
        dtCompteTier.Columns.Add("Email", Type.GetType("System.String"))
        'dtCompteTier.Columns.Add("Compte Collectif", Type.GetType("System.String"))
        dtCompteTier.Rows.Clear()

        Dim cptr As Decimal = 0
        query = "select COUNT(c.NOM_CPT) from t_comp_compte c, t_comp_type_compte t where c.CODE_TCPT=t.CODE_TCPT and (c.NOM_CPT like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or c.ADRESSE_CPT like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or t.LIBELLE_TCPT like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or c.EMAIL like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or c.CODE_CPT like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%')" ',t.LIBELLE_TCPT,r.CPT_TIER,r.CODE_SC
        Dim nbre = Val(ExecuteScallar(query))
        PageCount = nbre \ PageSize
        If nbre Mod PageSize <> 0 Then
            PageCount += 1
        End If

        Plan_tiers.TxtPage.Text = "Page " & Page & "/" & PageCount

        If (Page = 1) Then
            query = "select c.ABREGE_CPT, c.CODE_CPT, t.LIBELLE_TCPT, NOM_CPT, ADRESSE_CPT, EMAIL, CODE_CPT from t_comp_compte c, t_comp_type_compte t where c.CODE_TCPT=t.CODE_TCPT and (c.NOM_CPT like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or c.ADRESSE_CPT like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or t.LIBELLE_TCPT like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or c.EMAIL like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or c.CODE_CPT like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%') Limit " & PageSize.ToString  ',t.LIBELLE_TCPT,r.CPT_TIER,r.CODE_SC
        Else
            Dim PreviousPageOffSet = (Page - 1) * PageSize
            query = "select c.ABREGE_CPT, c.CODE_CPT, t.LIBELLE_TCPT, NOM_CPT, ADRESSE_CPT, EMAIL, CODE_CPT from t_comp_compte c, t_comp_type_compte t where c.CODE_TCPT=t.CODE_TCPT and (c.NOM_CPT like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or c.ADRESSE_CPT like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or t.LIBELLE_TCPT like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or c.EMAIL like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or c.CODE_CPT like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%') Limit " & PageSize & " OFFSET " & PreviousPageOffSet  ',t.LIBELLE_TCPT,r.CPT_TIER,r.CODE_SC
        End If

        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtCompteTier.NewRow()
            drS(0) = False
            drS(1) = rw("CODE_CPT").ToString
            drS(2) = MettreApost(rw("NOM_CPT").ToString)
            drS(3) = MettreApost(rw("ABREGE_CPT").ToString)
            drS(4) = MettreApost(rw("ADRESSE_CPT").ToString)
            drS(5) = MettreApost(rw("EMAIL").ToString)
            dtCompteTier.Rows.Add(drS)
            'cptr += 1
            'Dim drS = dtCompteTier.NewRow()
            'drS(0) = TabTrue(cptr - 1)
            'drS(1) = MettreApost(rwx(3).ToString)
            'drS(2) = MettreApost(rwx(0).ToString)
            'drS(3) = MettreApost(rwx(5).ToString)
            'drS(4) = MettreApost(rwx(1).ToString)
            'drS(5) = MettreApost(rwx(2).ToString)
            'drS(6) = MettreApost(rwx(6).ToString)
            'dtCompteTier.Rows.Add(drS)
        Next
        GridControl.DataSource = dtCompteTier
    End Sub

End Class
