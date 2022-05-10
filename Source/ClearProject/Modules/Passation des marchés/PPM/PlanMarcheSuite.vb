Imports Microsoft
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.Math
Imports System.Drawing.Printing
Imports System.IO
Imports ClearProject.PassationMarche
Imports DevExpress.XtraEditors.Repository

Public Class PlanMarcheSuite

    Public Bailleur As String
    Public CodeConvention As String
    Public RefPPM As String
    Public TypesMarches As String

    Dim IdEmpTab As String()
    Dim NomEmpTab As New List(Of String)
    Dim CurrentCodeProcAO As String = String.Empty
    Dim CurrentRefMarche As String = String.Empty
    Dim Editable As Boolean = False
    Dim DrX As DataRow
    Dim TypeMarches As String = ""
    Dim SavePlanMarche As Boolean = False

    Private Sub PlanMarcheSuite_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        LoadMarche(Bailleur, CodeConvention)

        If PlanMarche.ElaboPPM = "Tous les bailleurs" Then
            cmbBailleur.Visible = False
            cmbConvention.Visible = False
        Else
            cmbBailleur.Visible = True
            cmbConvention.Visible = True
            cmbBailleur.Text = Bailleur
            cmbConvention.Text = CodeConvention
        End If

        Dim dtEtape = New DataTable()
        dtEtape.Columns.Clear()
        dtEtape.Columns.Add("IdEtape", Type.GetType("System.String"))
        dtEtape.Columns.Add("N°", Type.GetType("System.Int32"))
        dtEtape.Columns.Add("Etape", Type.GetType("System.String"))
        dtEtape.Columns.Add("Responsable", Type.GetType("System.String"))
        dtEtape.Columns.Add("Début", Type.GetType("System.String"))
        dtEtape.Columns.Add("Fin", Type.GetType("System.String"))
        ' dtEtape.Columns.Add("IdResponsable", Type.GetType("System.String"))
        dtEtape.Columns.Add("Duree", Type.GetType("System.String"))
        dtEtape.Columns.Add("Réalisation", Type.GetType("System.String"))
        dtEtape.Columns.Add("Action", Type.GetType("System.String"))

        Dim Keys(0) As DataColumn
        Keys(0) = dtEtape.Columns("IdEtape")
        dtEtape.PrimaryKey = Keys 'Definir une cle primaire pour le datatable pour utiliser LoadDataRow()
        dtEtape.DefaultView.Sort = "N° ASC"
        LgEtape.DataSource = dtEtape
        ViewEtape.OptionsBehavior.Editable = True
        ViewEtape.OptionsBehavior.ReadOnly = False
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In ViewEtape.Columns
            col.OptionsColumn.AllowEdit = False
        Next

        If ViewEtape.Columns("IdEtape").Visible = True Then
            ViewEtape.Columns("IdEtape").Visible = False
            'ViewEtape.Columns("IdResponsable").Visible = False
            ViewEtape.Columns("Duree").Visible = False
            ViewEtape.Columns("Action").Visible = False

            ViewEtape.OptionsView.ColumnAutoWidth = True
            ViewEtape.Columns("N°").MaxWidth = 30
            ViewEtape.Columns("Responsable").MaxWidth = 160
            ViewEtape.Columns("Début").MaxWidth = 85
            ViewEtape.Columns("Fin").MaxWidth = 85
            ViewEtape.Columns("Réalisation").MaxWidth = 85
            'ViewEtape.Columns("Action").MaxWidth = 30

            ViewEtape.Columns("Responsable").OptionsColumn.AllowEdit = True
            ViewEtape.Columns("Début").OptionsColumn.AllowEdit = True
            ViewEtape.Columns("Réalisation").OptionsColumn.AllowEdit = True

            ViewEtape.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
            ViewEtape.Columns("Début").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewEtape.Columns("Fin").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewEtape.Columns("N°").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewEtape.Columns("Réalisation").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            Dim cmbEtape As RepositoryItemComboBox = New RepositoryItemComboBox()
            Dim dtDebut As RepositoryItemDateEdit = New RepositoryItemDateEdit()
            Dim dtRealisation As RepositoryItemDateEdit = New RepositoryItemDateEdit()

            GetEmploye(IdEmpTab, NomEmpTab, cmbEtape)
            ' cmbEtape.Items.Add("Autre")
            AddHandler cmbEtape.EditValueChanged, AddressOf cmbEtape_CheckedChanged
            AddHandler dtDebut.EditValueChanging, AddressOf dtDebut_EditValueChanging
            AddHandler dtDebut.EditValueChanged, AddressOf dtDebut_CheckedChanged
            AddHandler dtRealisation.EditValueChanged, AddressOf dtRealisation_EditValueChanged
            AddHandler dtRealisation.EditValueChanging, AddressOf dtRealisation_EditValueChanging

            ViewEtape.Columns("Responsable").ColumnEdit = cmbEtape
            ViewEtape.Columns("Début").ColumnEdit = dtDebut
            ViewEtape.Columns("Réalisation").ColumnEdit = dtRealisation
        End If
    End Sub

    Private Sub GetEmploye(ByRef IndexTab As String(), ByRef NomEmpTab As List(Of String), ByRef OutPut As RepositoryItemComboBox)
        ' Dim ListEmploye As Object()
        Dim dt1 As DataTable = ExcecuteSelectQuery("SELECT * FROM t_grh_employe WHERE PROJ_ID='" & ProjetEnCours & "' and EMP_EMAIL<>''")
        Dim dt2 As DataTable = ExcecuteSelectQuery("SELECT * FROM t_ppm_responsableetape WHERE CodeProjet='" & ProjetEnCours & "'")

        ReDim IndexTab(dt1.Rows.Count + dt2.Rows.Count)
        'ReDim ListEmploye(dt1.Rows.Count + dt2.Rows.Count)

        Dim i As Integer = 0
        NomEmpTab.Clear()
        OutPut.Items.Clear()
        Dim CodeEmp As String = ""

        For Each rw As DataRow In dt1.Rows 'Ajout des reponsables internes dans la liste
            CodeEmp = GetNewCode(rw("EMP_ID"))
            OutPut.Items.Add("I" & CodeEmp & " | " & MettreApost(rw("EMP_NOM") & " " & rw("EMP_PRENOMS")).Trim())
            NomEmpTab.Add("I" & CodeEmp & " | " & MettreApost(rw("EMP_NOM") & " " & rw("EMP_PRENOMS")).Trim())
            IndexTab(i) = "I" & CodeEmp
            '  ListEmploye(i) = MettreApost(rw("EMP_NOM") & " " & rw("EMP_PRENOMS")).Trim()
            i += 1
        Next

        For Each rw As DataRow In dt2.Rows 'Ajout des reponsables externes dans la liste
            CodeEmp = GetNewCode(rw("ID"))
            OutPut.Items.Add("E" & CodeEmp & " | " & MettreApost(rw("Nom") & " " & rw("Prenoms")).Trim())
            IndexTab(i) = "E" & CodeEmp
            NomEmpTab.Add("E" & CodeEmp & " | " & MettreApost(rw("Nom") & " " & rw("Prenoms")).Trim())
            ' ListEmploye(i) = MettreApost(rw("Nom") & " " & rw("Prenoms")).Trim()
            i += 1
        Next
    End Sub

    Private Sub cmbEtape_CheckedChanged(sender As Object, e As EventArgs)
        Dim obj As DevExpress.XtraEditors.ComboBoxEdit = CType(sender, DevExpress.XtraEditors.ComboBoxEdit)
        If obj.Text <> "" Then
            ' If obj.Text <> "Autre" And Not NomEmpTab.Contains(obj.Text) Then

            If obj.Text.Trim <> "" And Not NomEmpTab.Contains(obj.Text) Then
                ViewEtape.SetFocusedRowCellValue("Responsable", "")

                'Cas autre responsabe déjà prevu dans les paramettres
                'ElseIf obj.Text = "Autre" Then
                '    Dim NewAutreResponsable As New AutreResponsable
                '    Dim reslut As DialogResult = NewAutreResponsable.ShowDialog()
                '    If reslut = DialogResult.OK Then
                '        Dim Responsable As String = NewAutreResponsable.InfosResponsable
                '        ViewEtape.SetFocusedRowCellValue("Responsable", Responsable.Split(";")(0).Trim() & " " & Responsable.Split(";")(1).Trim())
                '        ViewEtape.SetFocusedRowCellValue("IdResponsable", Responsable)
                '    Else
                '        ViewEtape.SetFocusedRowCellValue("Responsable", "")
                '    End If
                ' Else
                '    ViewEtape.SetFocusedRowCellValue("IdResponsable", IdEmpTab(obj.SelectedIndex))
            End If
        End If
    End Sub

    Private Sub dtDebut_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        If ViewEtape.FocusedRowHandle <> 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub dtDebut_CheckedChanged(sender As Object, e As EventArgs)
        Dim obj As DevExpress.XtraEditors.DateEdit = CType(sender, DevExpress.XtraEditors.DateEdit)
        If obj.Text <> "" Then
            If ViewEtape.FocusedRowHandle = 0 Then
                AutoDate(obj.Text)
            End If
        Else
            For i = 0 To ViewEtape.RowCount - 1
                ViewEtape.SetRowCellValue(i, "Début", "")
                ViewEtape.SetRowCellValue(i, "Fin", "")
            Next
        End If
    End Sub

    Private Sub dtRealisation_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)

        If ViewEtape.RowCount > 0 Then
            Dim Text2 As Boolean = False
            DrX = ViewEtape.GetDataRow(ViewEtape.FocusedRowHandle)

            If ViewEtape.FocusedRowHandle > 0 Then
                If ViewEtape.GetRowCellValue(ViewEtape.FocusedRowHandle - 1, "Réalisation").ToString = "" Then
                    SuccesMsg("Veuillez saisir la date de réalisation de l'étape N° " & ViewEtape.FocusedRowHandle)
                    ViewEtape.SetFocusedRowCellValue("Réalisation", "")
                    Text2 = True
                End If
            End If

            If (CBool(DrX("Action")) = True) Or (SavePlanMarche = False) Or (Text2 = True) Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub dtRealisation_EditValueChanged(sender As Object, e As EventArgs)
        Dim obj As DevExpress.XtraEditors.DateEdit = CType(sender, DevExpress.XtraEditors.DateEdit)
        Dim IndexActive As Integer = ViewEtape.FocusedRowHandle
        DrX = ViewEtape.GetDataRow(ViewEtape.FocusedRowHandle)

        If obj.Text <> "" And ViewEtape.FocusedRowHandle <> 0 Then
            Dim Date1 As Date = CDate(ViewEtape.GetRowCellValue(IndexActive - 1, "Réalisation").ToString)
            Dim Date2 As Date = CDate(obj.Text)

            If DateTime.Compare(Date1, Date2) > 0 Then
                SuccesMsg("La date de réalisation de l'étape N° " & IndexActive + 1 & " doit être " & vbNewLine & "supérieure à la date de réalisation de l'étape N° " & IndexActive)
                ViewEtape.SetFocusedRowCellValue("Réalisation", "")
            End If
        ElseIf ViewEtape.GetRowCellValue(IndexActive, "Action") = False And obj.Text = "" Then
            For i = IndexActive To ViewEtape.RowCount - 1
                ViewEtape.SetRowCellValue(i, "Réalisation", "")
            Next
        End If
    End Sub


    Private Sub AutoDate(DateDebut1ereEtape As String)
        If Not IsDate(DateDebut1ereEtape) Then
            For i = 0 To ViewEtape.RowCount - 1
                ViewEtape.SetRowCellValue(i, "Début", "")
                ViewEtape.SetRowCellValue(i, "Fin", "")
            Next
        Else
            Dim datedebut As Date = CDate(DateDebut1ereEtape)
            For i = 0 To ViewEtape.RowCount - 1
                Dim nbjourdureeEtape As Integer = Val(ViewEtape.GetRowCellValue(i, "Duree"))
                ViewEtape.SetRowCellValue(i, "Début", datedebut.ToShortDateString())
                datedebut = datedebut.AddDays(nbjourdureeEtape)
                ViewEtape.SetRowCellValue(i, "Fin", datedebut.ToShortDateString())
                datedebut = datedebut.AddDays(1)
            Next
        End If
    End Sub

    Private Sub LoadMarche(ByVal Bailleur As String, ByVal CodeConvention As String)
        CleanMarcheItems()

        If PlanMarche.ModePPM = "Genere" Then
            If PlanMarche.ElaboPPM = "Tous les bailleurs" Then
                query = "Select * from T_Marche where CodeProjet='" & ProjetEnCours & "' AND RefPPM='" & RefPPM & "' AND ModePPM ='Tous_Bailleurs' and CodeProcAO>0 and RevuePrioPost<>'' ORDER BY DescriptionMarche"
            Else
                query = "Select * from T_Marche where CodeProjet='" & ProjetEnCours & "' AND InitialeBailleur='" & Bailleur & "' AND CodeConvention='" & CodeConvention & "' AND RefPPM='" & RefPPM & "' AND ModePPM ='Bailleur' and CodeProcAO>0 and RevuePrioPost<>'' ORDER BY DescriptionMarche"
            End If
        Else
            query = "Select * from T_Marche where CodeProjet='" & ProjetEnCours & "' AND RefPPM='" & RefPPM & "' AND ModePPM ='PPSD' and CodeProcAO>0 and RevuePrioPost<>'' ORDER BY RefMarche"
        End If

        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            AddMarcheItem(rw("RefMarche"), MettreApost(rw("TypeMarche").ToString))
        Next
    End Sub

    Private Sub CleanMarcheItems()
        NavBarConsultants.ItemLinks.Clear()
        NavBarFournitures.ItemLinks.Clear()
        NavBarAutresServices.ItemLinks.Clear()
        NavBarTravaux.ItemLinks.Clear()
    End Sub

    Private Sub AddMarcheItem(ByVal RefMarche As String, ByVal TypeMarche As String)
        Dim LibelleMarche As String = String.Empty
        LibelleMarche = MettreApost(ExecuteScallar("SELECT DescriptionMarche FROM t_marche WHERE RefMarche='" & RefMarche & "'"))
        If TypeMarche.ToLower = "Consultants".ToLower Then
            Dim NewMarche As New DevExpress.XtraNavBar.NavBarItem With {
                .Caption = LibelleMarche, .Name = RefMarche
            }
            Dim NewLink = NavBarConsultants.ItemLinks.Add(NewMarche)
            NavBarConsultants.ItemLinks.Add(NewLink)
        ElseIf TypeMarche.ToLower = "Fournitures".ToLower Then
            Dim NewMarche As New DevExpress.XtraNavBar.NavBarItem With {
               .Caption = LibelleMarche, .Name = RefMarche
           }
            Dim NewLink = NavBarFournitures.ItemLinks.Add(NewMarche)
            NavBarFournitures.ItemLinks.Add(NewLink)
        ElseIf TypeMarche.ToLower = "Services autres que les services de consultants".ToLower Then
            Dim NewMarche As New DevExpress.XtraNavBar.NavBarItem With {
               .Caption = LibelleMarche, .Name = RefMarche
           }
            Dim NewLink = NavBarAutresServices.ItemLinks.Add(NewMarche)
            NavBarAutresServices.ItemLinks.Add(NewLink)
        ElseIf TypeMarche.ToLower = "Travaux".ToLower Then
            Dim NewMarche As New DevExpress.XtraNavBar.NavBarItem With {
               .Caption = LibelleMarche, .Name = RefMarche
           }
            Dim NewLink = NavBarTravaux.ItemLinks.Add(NewMarche)
            NavBarTravaux.ItemLinks.Add(NewLink)
        End If
    End Sub

    Private Sub GetInfoMarche(ByVal RefMarche As String)
        Dim OldDataSource As DataTable = LgEtape.DataSource
        OldDataSource.Rows.Clear()
        txtMarche.ResetText()
        txtMethode.ResetText()
        txtMontant.ResetText()
        cmbRevue.ResetText()
        txtStatut.ResetText()
        Dim StatuMarche As Boolean = False

        query = "SELECT * FROM t_marche WHERE RefMarche='" & RefMarche & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            txtMarche.Text = MettreApost(rw("DescriptionMarche"))
            txtMontant.Text = AfficherMonnaie(rw("MontantEstimatif"))
            txtMethode.Text = GetMethode(rw("CodeProcAO"))
            cmbRevue.Text = rw("RevuePrioPost").ToString

            'Chercher si ce marché a été utiliser pour elaborer un dossier en fonction du type de marché (Pour determiner son statut)

            If rw("TypeMarche").ToString.ToLower = "Consultants".ToLower Then
                If Val(ExecuteScallar("SELECT COUNT(*) FROM t_marche as M, t_ami as D WHERE M.RefMarche=D.RefMarche AND M.RefMarche='" & RefMarche & "' and D.StatutDoss<>'Annulé'")) > 0 Then StatuMarche = True
                If Val(ExecuteScallar("SELECT COUNT(*) FROM t_marche as M, t_dp as D WHERE M.RefMarche=D.RefMarche AND D.Statut<>'Annulé' AND M.RefMarche='" & RefMarche & "'")) > 0 Then StatuMarche = True
            Else
                If Val(ExecuteScallar("SELECT COUNT(*) FROM t_marche as M, t_dao as D WHERE D.statut_DAO<>'Annulé' AND M.RefMarche=D.RefMarche AND M.RefMarche='" & RefMarche & "'")) > 0 Then StatuMarche = True
            End If

            txtStatut.Text = IIf(StatuMarche = False, "Non exécuté", "Exécuté").ToString

            'If IsDBNull(rw("NumeroDAO")) Then
            '    txtStatut.Text = "Non exécuté"
            'ElseIf rw("NumeroDAO") = String.Empty Then
            '    txtStatut.Text = "Non exécuté"
            'Else
            '    txtStatut.Text = "Exécuté"
            'End If

            CurrentCodeProcAO = rw("CodeProcAO")
            TypeMarches = rw("TypeMarche").ToString
            LoadEtape(rw("CodeProcAO"), rw("RevuePrioPost").ToString().Replace("é", "e"), rw("TypeMarche").ToString, RefMarche)
        Next
        'ViewEtape.OptionsView.ColumnAutoWidth = True

    End Sub

    Private Sub LoadEtape(CodeProcAO As String, Revue As String, ByVal TypeMarche As String, Optional RefMarche As String = "")
        Dim OldDataSource As DataTable = LgEtape.DataSource

        OldDataSource.BeginLoadData()
        'Verifier si l'on a déjà saisir les date de prevision
        If Not VerifEtapePlan(CurrentRefMarche) Then
            SavePlanMarche = False
            cmbRevue.Enabled = True
            ViewEtape.Columns("Responsable").OptionsColumn.AllowEdit = True
            ViewEtape.Columns("Début").OptionsColumn.AllowEdit = True

            query = "SELECT E.* FROM t_etapemarche AS E, t_liaisonetape as L WHERE L.RefEtape=E.RefEtape and E.TypeMarche='" & EnleverApost(TypeMarche) & "' and L.CodeProcAO='" & CodeProcAO & "' AND  E." & Revue & "='OUI' AND E.CodeProjet='" & ProjetEnCours & "' ORDER BY E.NumeroOrdre ASC"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            Dim Numéro As Integer = 1
            For Each rw As DataRow In dt.Rows
                Dim drS() As Object = {"", "", "", "", "", "", "", "", ""}
                drS(0) = rw("RefEtape")
                drS(1) = Numéro 'rw("NumeroOrdre") 
                drS(2) = MettreApost(rw("TitreEtape"))
                drS(3) = String.Empty
                drS(4) = String.Empty
                drS(5) = String.Empty
                drS(6) = GetDureeEtatpeEnJour(rw("DelaiEtape"))
                drS(7) = String.Empty
                drS(8) = False
                OldDataSource.LoadDataRow(drS, True)
                Numéro += 1
            Next
        Else
            SavePlanMarche = True
            cmbRevue.Enabled = False
            ViewEtape.Columns("Responsable").OptionsColumn.AllowEdit = False
            ViewEtape.Columns("Début").OptionsColumn.AllowEdit = False

            query = "SELECT * FROM t_planmarche WHERE RefMarche='" & RefMarche & "'  ORDER BY NumeroOrdre ASC"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            'Dim Numéro As Integer = 1
            For Each rw As DataRow In dt.Rows
                Dim nomRespo = String.Empty

                Dim drS() As Object = {"", "", "", "", "", "", "", "", ""}
                drS(0) = rw("RefEtape")
                drS(1) = rw("NumeroOrdre") 'Numéro 
                drS(2) = MettreApost(GetEtapeInfo(rw("RefEtape"))("Libelle"))
                drS(3) = NewNomResponsable(rw("ResponsableEtape"), rw("StatutRespoEtape"))
                drS(4) = IIf(IsDBNull(rw("DebutPrevu")), "", CDate(rw("DebutPrevu")).ToShortDateString())
                drS(5) = IIf(IsDBNull(rw("FinPrevue")), "", CDate(rw("FinPrevue")).ToShortDateString())
                drS(6) = GetDureeEtatpeEnJour(MettreApost(GetEtapeInfo(rw("RefEtape"))("Delai")))
                drS(7) = rw("FinEffective").ToString
                drS(8) = IIf(rw("FinEffective").ToString <> "", True, False).ToString
                OldDataSource.LoadDataRow(drS, True)
            Next
        End If
        OldDataSource.EndLoadData()

    End Sub

    Private Function NewNomResponsable(ByVal IdResponsable As String, ByVal TypeResponsable As String) As String
        Dim nomRespo As String = ""
        Try
            If TypeResponsable = "Interne" Then
                query = "SELECT EMP_NOM, EMP_PRENOMS FROM t_grh_employe WHERE EMP_ID='" & IdResponsable.ToString & "'"
                Dim dt0 = ExcecuteSelectQuery(query)
                For Each rw1 In dt0.Rows
                    nomRespo = MettreApost(rw1("EMP_NOM").ToString) & " " & MettreApost(rw1("EMP_PRENOMS").ToString)
                Next
            Else
                query = "SELECT Nom, Prenoms FROM t_ppm_responsableetape WHERE ID='" & IdResponsable.ToString & "'"
                Dim dt0 = ExcecuteSelectQuery(query)
                For Each rw1 In dt0.Rows
                    nomRespo = MettreApost(rw1("Nom").ToString) & " " & MettreApost(rw1("Prenoms").ToString)
                Next
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try

        Return nomRespo
    End Function


    Private Function GetResponsable(Value As String) As String
        If Value.Contains(";") Then
            Return Value.Split(";")(0).Trim() & " " & Value.Split(";")(1).Trim()
        Else
            Return GetInfoEmp(Value)("NomPrenoms")
        End If
    End Function


    Private Function GetDureeEtatpeEnJour(duree As String) As Integer
        If duree <> "DAO" And duree <> "" Then
            Dim Type As String = Split(duree, " "c)(1)
            Dim Value As Integer = Val(Split(duree, " "c)(0))
            If Type.ToLower() = "jours" Then
                Return Value
            ElseIf Type.ToLower() = "semaines" Then
                Return Value * 7
            ElseIf Type.ToLower() = "mois" Then
                Return Value * 30
            Else
                Return -1
            End If
        End If
    End Function

    Private Sub NavBarControlTypeMarche_LinkClicked(ByVal sender As System.Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavBarControlTypeMarche.LinkClicked
        If CurrentRefMarche <> e.Link.ItemName Then
            CurrentRefMarche = e.Link.ItemName
            GetInfoMarche(e.Link.ItemName)
        End If
    End Sub

    Private Sub cmbRevue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles cmbRevue.EditValueChanging
        On Error Resume Next
        If cmbRevue.SelectedIndex <> -1 Then
            If ViewEtape.RowCount > 0 Then
                If ConfirmMsgWarning("Changer la revue rechargera les étapes dans le tableau ci-dessous." & vbNewLine & "Voulez-vous continuer ?") = DialogResult.Yes Then
                    LoadEtape(CurrentCodeProcAO, e.NewValue.ToString().Replace("é", "e"), TypeMarches)
                Else
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        Try

            If ViewEtape.RowCount = 0 Then
                Exit Sub
            End If
            Dim CodeExcecuter As Boolean = False
            Dim drx As DataRow

            If txtMarche.Text.Trim().Length = 0 Then
                SuccesMsg("Entrer la désignation du marché")
                txtMarche.Select()
                Exit Sub
            End If

            If cmbRevue.SelectedIndex = -1 Then
                SuccesMsg("Veuillez selectioner la revue")
                cmbRevue.Select()
                Exit Sub
            End If

            If Not VerifEtapePlan(CurrentRefMarche) Then
                Dim IsGood As Boolean = True
                Dim str As String = String.Empty
                For i = 0 To ViewEtape.RowCount - 1
                    drx = ViewEtape.GetDataRow(i)

                    If drx("Responsable").ToString().Length = 0 Then
                        str &= "- Le responsable de l'étape " & drx("N°") & vbNewLine
                    End If
                    If drx("Début").ToString().Length = 0 Then
                        str &= "- La date de début de l'étape " & drx("N°") & vbNewLine
                    End If
                    If drx("Fin").ToString().Length = 0 Then
                        str &= "- La date de fin de l'étape " & drx("N°") & vbNewLine
                    End If
                Next

                If str <> String.Empty Then
                    SuccesMsg("Veuillez renseigner correctement : " & vbNewLine & str)
                    Exit Sub
                End If

                DebutChargement(True, "Enregistrement en cours...")

                query = "UPDATE t_marche SET DescriptionMarche='" & EnleverApost(txtMarche.Text.Trim()) & "', RevuePrioPost='" & EnleverApost(cmbRevue.Text) & "' WHERE CodeProjet='" & ProjetEnCours & "' AND RefMarche='" & CurrentRefMarche & "'"
                ExecuteNonQuery(query)

                For i = 0 To ViewEtape.RowCount - 1
                    drx = ViewEtape.GetDataRow(i)
                    ' query = "INSERT INTO t_planmarche VALUES(NULL,'" & CurrentRefMarche & "','" & drx("IdEtape") & "','" & drx("N°") & "','" & drx("Début") & "',NULL,'" & drx("Fin") & "',NULL,'" & drx("IdResponsable") & "','" & CodeOperateurEnCours & "','Non')"

                    Dim tabRespo As String() = drx("Responsable").ToString.Split("|")
                    query = "INSERT INTO t_planmarche VALUES(NULL,'" & CurrentRefMarche & "','" & drx("IdEtape") & "','" & drx("N°") & "','" & drx("Début") & "',NULL,'" & drx("Fin") & "',NULL,'" & CInt(Mid(tabRespo(0).ToString, 2)) & "', '" & IIf(Mid(tabRespo(0).ToString, 1, 1) = "E", "Exterieur", "Interne").ToString & "', '" & CodeOperateurEnCours & "','Non', '" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', '" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "')"
                    ExecuteNonQuery(query)
                    CodeExcecuter = True
                Next
                cmbRevue.Enabled = False
            Else
                DebutChargement(True, "Enregistrement en cours...")

                ExecuteNonQuery("UPDATE t_marche SET DescriptionMarche='" & EnleverApost(txtMarche.Text.Trim()) & "' WHERE CodeProjet='" & ProjetEnCours & "' AND RefMarche='" & CurrentRefMarche & "'")

                'Dim CurrentNav = NavBarControlTypeMarche.SelectedLink.NavBar
                'SuccesMsg(CurrentNav.Items(0).Caption)
                'CurrentNav.Items(0).Caption = txtMarche.Text.Trim()

                For i = 0 To ViewEtape.RowCount - 1
                    drx = ViewEtape.GetDataRow(i)
                    If drx("Réalisation").ToString <> "" And CBool(drx("Action")) = False Then
                        ExecuteNonQuery("UPDATE t_planmarche SET FinEffective='" & CDate(drx("Réalisation").ToString).ToShortDateString & "', CodeOperateur='" & CodeOperateurEnCours & "', DateModif='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' WHERE RefMarche='" & CurrentRefMarche & "' and RefEtape='" & drx("IdEtape") & "'")
                        CodeExcecuter = True
                    End If
                Next
            End If
            FinChargement()

            If CodeExcecuter = True Then
                SuccesMsg("Enregistrement effectué avec succès.")
                GetInfoMarche(CurrentRefMarche)
            End If

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

End Class