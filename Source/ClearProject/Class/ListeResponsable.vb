Imports DevExpress.XtraGrid

Public Class ListeResponsable
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
        Dim dtListeRespo As New DataTable
        dtListeRespo.Columns.Clear()
        dtListeRespo.Columns.Add("Choix", Type.GetType("System.Boolean"))
        dtListeRespo.Columns.Add("Code", Type.GetType("System.String"))
        dtListeRespo.Columns.Add("Nom & prénoms", Type.GetType("System.String"))
        dtListeRespo.Columns.Add("Structure", Type.GetType("System.String"))
        dtListeRespo.Columns.Add("Fonction", Type.GetType("System.String"))
        dtListeRespo.Columns.Add("Portable", Type.GetType("System.String"))
        dtListeRespo.Columns.Add("Email", Type.GetType("System.String"))

        dtListeRespo.Rows.Clear()

        If (Page = 1) Then
            query = "select * from t_ppm_responsableetape where CodeProjet='" & ProjetEnCours & "' order by ID Limit " & PageSize.ToString & ""
        Else
            Dim PreviousPageOffSet = (Page - 1) * PageSize
            query = "select * from t_ppm_responsableetape where CodeProjet='" & ProjetEnCours & "' order by ID Limit " & PageSize & " OFFSET " & PreviousPageOffSet
        End If

        Dim cptr As Decimal = 0
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtListeRespo.NewRow()
            drS(0) = False
            drS(1) = rw("ID").ToString
            drS(2) = MettreApost(rw("Nom").ToString) & " " & MettreApost(rw("Prenoms").ToString)
            drS(3) = MettreApost(rw("Service").ToString)
            drS(4) = MettreApost(rw("Fonction").ToString)
            drS(5) = MettreApost(rw("Portable").ToString)
            drS(6) = MettreApost(rw("EMAIL").ToString)
            dtListeRespo.Rows.Add(drS)
        Next

        GridControl.DataSource = dtListeRespo
    End Sub

    Public Sub RechPage(ByVal GridControl As GridControl, ByVal Page As Decimal)
        On Error Resume Next
        Dim dtListeRespo As New DataTable
        dtListeRespo.Columns.Clear()
        dtListeRespo.Columns.Add("Choix", Type.GetType("System.Boolean"))
        dtListeRespo.Columns.Add("Code", Type.GetType("System.String"))
        dtListeRespo.Columns.Add("Nom & prénoms", Type.GetType("System.String"))
        dtListeRespo.Columns.Add("Structure", Type.GetType("System.String"))
        dtListeRespo.Columns.Add("Fonction", Type.GetType("System.String"))
        dtListeRespo.Columns.Add("Portable", Type.GetType("System.String"))
        dtListeRespo.Columns.Add("Email", Type.GetType("System.String"))

        dtListeRespo.Rows.Clear()
        Dim cptr As Decimal = 0
        query = "select COUNT(ID) from t_ppm_responsableetape where CodeProjet='" & ProjetEnCours & "' AND (Nom like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or Prenoms like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or Service like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or Email like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or Fonction like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%')"
        Dim nbre = Val(ExecuteScallar(query))
        PageCount = nbre \ PageSize
        If nbre Mod PageSize <> 0 Then
            PageCount += 1
        End If

        Plan_tiers.TxtPage.Text = "Page " & Page & "/" & PageCount

        If (Page = 1) Then
            query = "select * from t_ppm_responsableetape where CodeProjet='" & ProjetEnCours & "' AND (Nom like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or Prenoms like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or Service like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or Email like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or Fonction like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%') Limit " & PageSize.ToString
        Else
            Dim PreviousPageOffSet = (Page - 1) * PageSize
            query = "select * from t_ppm_responsableetape where CodeProjet='" & ProjetEnCours & "' AND (Nom like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or Prenoms like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or Service like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or Email like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%' or Fonction like '%" & EnleverApost(Plan_tiers.TxtRechecher.Text) & "%') Limit " & PageSize.ToString & " OFFSET " & PreviousPageOffSet
        End If

        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtListeRespo.NewRow()
            drS(0) = False
            drS(1) = rw("ID").ToString
            drS(2) = MettreApost(rw("Nom").ToString) & " " & MettreApost(rw("Prenoms").ToString)
            drS(3) = MettreApost(rw("Service").ToString)
            drS(4) = MettreApost(rw("Fonction").ToString)
            drS(4) = MettreApost(rw("Portable").ToString)
            drS(5) = MettreApost(rw("EMAIL").ToString)
            dtListeRespo.Rows.Add(drS)
        Next
        GridControl.DataSource = dtListeRespo
    End Sub
End Class
