Imports System.Data
Imports System.IO
Imports System
Imports System.Drawing
Imports Microsoft
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.Math
Imports System.Text.RegularExpressions
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen
Imports AxSms
Imports AxEmail
Imports DevExpress.XtraEditors.Repository
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography

Module CONNEXION

    Public largeur As Decimal = ecran.Bounds.Width
    Public hauteur As Decimal = ecran.Bounds.Height
    Public line1 As String
    Public Property CheckBoxSelectorField As String
    Public Property ShowCheckBoxSelectorInColumnHeader As Boolean

    Public Sub SuccesMsg(ByVal myMsg As String)
        MessageBox.Show(myMsg, "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Public Sub AlertMsg(ByVal myMsg As String)
        MessageBox.Show(myMsg, "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub

    Public Sub FailMsg(ByVal myMsg As String)
        MessageBox.Show(myMsg, "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Function ConfirmMsg(ByVal myMsg As String) As DialogResult
        Return MessageBox.Show(myMsg, "ClearProject", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    End Function
    Public Function ConfirmMsgWarning(ByVal myMsg As String) As DialogResult
        Return MessageBox.Show(myMsg, "ClearProject", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
    End Function
    Public Function ConfirmCancelMsgWarning(ByVal myMsg As String) As DialogResult
        Return MessageBox.Show(myMsg, "ClearProject", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
    End Function
    Public Function ConfirmCancelMsg(ByVal myMsg As String) As DialogResult
        Return MessageBox.Show(myMsg, "ClearProject", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
    End Function

    Sub InitTabTrue()
        For n As Decimal = 0 To 49999
            TabTrue(n) = False
        Next
        nbTab = 0
    End Sub

    Public Sub Info(ByVal Type As String, ByVal Exped As String, ByVal Message As String, ByVal Tps As Decimal)
        'ClearInfo.Location = New System.Drawing.Point(largeur - 303, hauteur - 167)
        'ClearInfo.CsprCollapisblePanel2.TitleText = Type
        'ClearInfo.MsgExped.Text = Exped
        'ClearInfo.MsgNews.Text = Message
        'TpsInfo = Tps
        'ClearInfo.Opacity = 0
        'ClearInfo.Show()

    End Sub

    Public Sub DebutChargement(Optional ByVal traitEnCours As Boolean = False, Optional ByVal TxtTrait As String = "")
        Try
            If (SplasFerme = True) Then
                SplasFerme = False
                TextChargmt = TxtTrait
                If (traitEnCours = False) Then
                    SplashScreenManager.ShowForm(ClearMdi, GetType(WaitForm1), False, False)
                Else
                    SplashScreenManager.ShowForm(ClearMdi, GetType(SplashScreen2), False, False)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    'Public Sub DebutChargement1(Optional ByVal traitEnCours As Boolean = False, Optional ByVal TxtTrait As String = "")
    '    Try
    '        If (SplasFerme = True) Then
    '            SplasFerme = False
    '            TextChargmt = TxtTrait
    '            If (traitEnCours = False) Then
    '                SplashScreenManager.ShowForm(ClearMdi, GetType(SplashScreen3), False, False)
    '            Else
    '                SplashScreenManager.ShowForm(ClearMdi, GetType(SplashScreen3), False, False)
    '            End If

    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Public Sub FinChargement()
        Try
            If (SplasFerme = False) Then
                SplashScreenManager.CloseForm()
                SplasFerme = True
            End If
        Catch ex As Exception
        End Try

    End Sub

    Public Sub InitialisationRadioBtn()
        EditionFicheActivite = False
        EditionFicheBudgParActivité = False
        EditionFicheBudgParComp = False
        EditionFicheSuiviBudg = False
        EditionchainelogiquePFCTCAL = False
        EditionPAActivité = False
        EditionChronoPTA = False
        EditionFinancementPTA = False
        EditionRapportMissionSuiv = False
        EditionFicheSuiviActiv = False
        EditionRapportTrimestriel = False
        EditionRapportAnnuel = False
        EditionCalendExecut = False
        EditionBudgPComp = False
        EditionBudgPSComp = False
        EditionPrévBudg = False
    End Sub

    Sub remplirjournal(ByVal mondg As DevExpress.XtraGrid.GridControl, ByVal grid As DevExpress.XtraGrid.Views.Grid.GridView)
        dtcombj.Columns.Clear()
        dtcombj.Columns.Add("Choix", Type.GetType("System.Boolean"))
        dtcombj.Columns.Add("Code Journal", Type.GetType("System.String"))
        dtcombj.Columns.Add("Libellé Journal", Type.GetType("System.String"))
        dtcombj.Rows.Clear()

        Dim cptr As Decimal = 0
        query = "select code_j, libelle_j from t_comp_journal"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            cptr += 1
            Dim drS = dtcombj.NewRow()
            drS(0) = TabTrue(cptr - 1)
            drS(1) = rw(0).ToString
            drS(2) = MettreApost(rw(1).ToString)
            dtcombj.Rows.Add(drS)
        Next

        mondg.DataSource = dtcombj
        grid.Columns(0).Width = 50
        grid.Columns(1).Width = 100
        grid.Columns(2).Width = 300
        grid.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(grid, "[Selected]", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
        ColorRowGridAnal(grid, "[Selected]=true", Color.LightGray, "Times New Roman", 11, FontStyle.Bold, Color.Black, False)
    End Sub

    Sub remplirOP2(ByVal requete As String, ByVal mondg As DevExpress.XtraGrid.GridControl, ByVal grid As DevExpress.XtraGrid.Views.Grid.GridView)
        'date
        Dim dateop As Date
        'sql = "select datedebut, datefin from T_COMP_EXERCICE where Etat<>'2' and encours='1'"
        'Dim dt As DataTable = ExcecuteSelectQuery(query)
        'For Each rw In dt.Rows
        'Next
        dateop = ExerciceComptable.Rows(0).Item("datedebut")

        Dim str1(3) As String
        str1 = dateop.ToString.Split("/")
        Dim tempdt1 As String = String.Empty
        For j As Integer = 2 To 0 Step -1
            tempdt1 += str1(j) & "-"
        Next
        tempdt1 = tempdt1.Substring(0, 10)

        dtop.Columns.Clear()
        dtop.Columns.Add("Selected", Type.GetType("System.Boolean"))
        dtop.Columns.Add("Exercice", Type.GetType("System.String"))
        dtop.Columns.Add("Numéro OP", Type.GetType("System.String"))
        dtop.Columns.Add("Référence du bénéficiaire", Type.GetType("System.String"))
        dtop.Columns.Add("Bénéficiaire", Type.GetType("System.String"))
        dtop.Columns.Add("Compte de disponibilité", Type.GetType("System.String"))
        dtop.Columns.Add("Mode de règlement", Type.GetType("System.String"))
        dtop.Columns.Add("Imputation Budgétaire", Type.GetType("System.String"))
        dtop.Columns.Add("Dotation Budgétaire", Type.GetType("System.String"))
        dtop.Columns.Add("Engagment antérieurs", Type.GetType("System.String"))
        dtop.Columns.Add("Engagement actuel", Type.GetType("System.String"))
        dtop.Columns.Add("Engagements cumulés", Type.GetType("System.String"))
        dtop.Columns.Add("Disponible budgétaire", Type.GetType("System.String"))
        dtop.Rows.Clear()

        Dim cptr As Decimal = 0
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        For Each rw1 In dt1.Rows
            cptr += 1
            Dim drS = dtop.NewRow()
            drS(0) = TabTrue(cptr - 1)
            drS(1) = Year(rw1(0).ToString)
            drS(2) = rw1(1).ToString
            drS(3) = rw1(2).ToString
            drS(4) = MettreApost(rw1(3).ToString)
            drS(5) = rw1(4).ToString
            drS(6) = MettreApost(rw1(5).ToString)
            drS(7) = rw1(6).ToString
            drS(8) = AfficherMonnaie(rw1(7).ToString)
            drS(9) = AfficherMonnaie(rw1(8).ToString)
            drS(10) = AfficherMonnaie(rw1(9).ToString)
            drS(11) = AfficherMonnaie(rw1(10).ToString)
            drS(12) = AfficherMonnaie(rw1(11).ToString)
            dtop.Rows.Add(drS)
        Next

        mondg.DataSource = dtop
        grid.Columns(0).Visible = False
        grid.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
    End Sub

    Sub RemplirDatagridSF(ByVal mondg As DevExpress.XtraGrid.GridControl, ByVal grid As DevExpress.XtraGrid.Views.Grid.GridView)
        Try
            dtService.Columns.Clear()
            dtService.Columns.Add("Choix", Type.GetType("System.Boolean"))
            dtService.Columns.Add("Identifiant", Type.GetType("System.String"))
            dtService.Columns.Add("Libellé", Type.GetType("System.String"))
            dtService.Rows.Clear()

            Dim cptr As Decimal = 0
            Dim cpt1 As Decimal = 1
            Dim cpt2 As Decimal = 2

            query = "select SIGFCOMPTE1, SIGFLIBELLE1 from T_PlanSigFip1"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                cptr += 1
                Dim drS = dtService.NewRow()
                drS(0) = TabTrue(cptr - 1)
                drS(1) = MettreApost(rw(0).ToString)
                drS(2) = MettreApost(rw(1).ToString)
                dtService.Rows.Add(drS)

                query = "select SIGFCOMPTE2, SIGFLIBELLE2 from T_PlanSigFip2 Where SIGFCOMPTE1='" & rw(0).ToString & "'"
                dt0 = ExcecuteSelectQuery(query)
                For Each rw0 As DataRow In dt0.Rows
                    cptr += 1
                    drS = dtService.NewRow()
                    drS(0) = TabTrue(cptr - 1)
                    drS(1) = MettreApost(rw0(0).ToString)
                    drS(2) = MettreApost(rw0(1).ToString)
                    dtService.Rows.Add(drS)

                    query = "select SIGFCOMPTE3, SIGFLIBELLE3 from T_PlanSigFip3 Where SIGFCOMPTE2='" & rw0(0).ToString & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt0.Rows
                        cptr += 1
                        drS = dtService.NewRow()
                        drS(0) = TabTrue(cptr - 1)
                        drS(1) = MettreApost(rw1(0).ToString)
                        drS(2) = MettreApost(rw1(1).ToString)
                        dtService.Rows.Add(drS)

                        query = "select SIGFCOMPTE4, SIGFLIBELLE4 from T_PlanSigFip4 Where SIGFCOMPTE3='" & rw1(0).ToString & "'"
                        dt0 = ExcecuteSelectQuery(query)
                        For Each rw2 As DataRow In dt0.Rows
                            cptr += 1
                            drS = dtService.NewRow()
                            drS(0) = TabTrue(cptr - 1)
                            drS(1) = MettreApost(rw2(0).ToString)
                            drS(2) = MettreApost(rw2(1).ToString)
                            dtService.Rows.Add(drS)

                            query = "select SIGFCOMPTE, SIGFLIBELLE from T_PlanSigFip Where SIGFCOMPTE4='" & rw2(0).ToString & "'"
                            dt0 = ExcecuteSelectQuery(query)
                            For Each rw3 As DataRow In dt0.Rows
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
            Next

            grid.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
            mondg.DataSource = dtService

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub

    Sub RemplirDatagridCTT(ByVal mondg As DevExpress.XtraGrid.GridControl, ByVal grid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal etatrub As String)
        Try
            dtService.Columns.Clear()
            dtService.Columns.Add("Choix", Type.GetType("System.Boolean"))
            dtService.Columns.Add("Type", Type.GetType("System.String"))
            dtService.Columns.Add("Identifiant", Type.GetType("System.String"))
            dtService.Columns.Add("Libellé", Type.GetType("System.String"))
            dtService.Columns.Add("Etat", Type.GetType("System.String"))
            dtService.Rows.Clear()
            Dim cptr As Decimal = 0

            query = "SELECT * FROM t_comp_rubriqueent WHERE ETAT_RUB='" & etatrub.ToString & "' order by ID_RUBENT"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                Dim drS = dtService.NewRow()
                drS(0) = False
                drS(1) = "En-tête"
                drS(2) = MettreApost(rw("ID_RUBENT").ToString)
                drS(3) = MettreApost(rw("LIBELLE_RUBENT").ToString)
                drS(4) = MettreApost(rw("ETAT_RUB").ToString)
                dtService.Rows.Add(drS)
                'On selectionne les details qui sont directement lies au entete
                query = "SELECT * FROM t_comp_rubrique WHERE ID_RUBST='" & rw("ID_RUBENT") & "' AND ETAT_RUB='" & etatrub.ToString & "' ORDER BY CODE_RUB"
                Dim dtD As DataTable = ExcecuteSelectQuery(query)
                For Each rwD As DataRow In dtD.Rows
                    drS = dtService.NewRow()
                    drS(0) = False
                    drS(1) = "Détails"
                    drS(2) = MettreApost(rwD("CODE_RUB").ToString)
                    drS(3) = MettreApost(rwD("LIBELLE_RUB").ToString)
                    drS(4) = MettreApost(rwD("ETAT_RUB").ToString)
                    dtService.Rows.Add(drS)
                Next
                query = "SELECT * FROM t_comp_rubriquest WHERE ID_RUBENT='" & rw("ID_RUBENT") & "' AND ETAT_RUB='" & etatrub.ToString & "'"
                Dim dtSousTotal As DataTable = ExcecuteSelectQuery(query)
                For Each rwST As DataRow In dtSousTotal.Rows
                    drS = dtService.NewRow()
                    drS(0) = False
                    drS(1) = "Sous Total"
                    drS(2) = MettreApost(rwST("ID_RUBST").ToString)
                    drS(3) = MettreApost(rwST("LIBELLE_RUBST").ToString)
                    drS(4) = MettreApost(rwST("ETAT_RUB").ToString)
                    dtService.Rows.Add(drS)

                    query = "SELECT * FROM t_comp_rubrique WHERE ID_RUBST='" & rwST("ID_RUBST") & "' AND ETAT_RUB='" & etatrub.ToString & "' ORDER BY CODE_RUB"
                    Dim dtDetail As DataTable = ExcecuteSelectQuery(query)
                    For Each rwDetail As DataRow In dtDetail.Rows
                        drS = dtService.NewRow()
                        drS(0) = False
                        drS(1) = "Détails"
                        drS(2) = MettreApost(rwDetail("CODE_RUB").ToString)
                        drS(3) = MettreApost(rwDetail("LIBELLE_RUB").ToString)
                        drS(4) = MettreApost(rwDetail("ETAT_RUB").ToString)
                        dtService.Rows.Add(drS)
                    Next
                Next
            Next
            'On selectionne les details qui n'appartient pas a un entete
            query = "SELECT * FROM t_comp_rubrique WHERE ID_RUBST='' AND ETAT_RUB='" & etatrub.ToString & "' ORDER BY CODE_RUB"
            dt = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                Dim drS = dtService.NewRow()
                drS(0) = False
                drS(1) = ""
                drS(2) = ""
                drS(3) = ""
                drS(4) = ""
                dtService.Rows.Add(drS)
                For Each rwDetail As DataRow In dt.Rows
                    drS = dtService.NewRow()
                    drS(0) = False
                    drS(1) = "Détails"
                    drS(2) = MettreApost(rwDetail("CODE_RUB").ToString)
                    drS(3) = MettreApost(rwDetail("LIBELLE_RUB").ToString)
                    drS(4) = MettreApost(rwDetail("ETAT_RUB").ToString)
                    dtService.Rows.Add(drS)
                Next
            End If

            mondg.DataSource = dtService
            grid.Columns(0).Visible = False
            grid.Columns(0).Width = 50
            grid.Columns(1).Width = 100
            grid.Columns(2).Width = 100
            grid.Columns(3).Width = 430
            grid.Columns(4).Width = 150
            grid.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
            ColorRowGridAnal(grid, "[Type]='En-tête'", Color.SteelBlue, "Times New Roman", 10, FontStyle.Bold, Color.Black)
            ColorRowGridAnal(grid, "[Type]='Sous Total'", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub

    Sub RemplirDatagridCT(ByVal mondg As DevExpress.XtraGrid.GridControl, ByVal grid As DevExpress.XtraGrid.Views.Grid.GridView)
        Try
            Dim dtChargement = New DataTable()
            dtChargement.Columns.Clear()
            dtChargement.Columns.Add("Choix", Type.GetType("System.Boolean"))
            dtChargement.Columns.Add("Type", Type.GetType("System.String"))
            dtChargement.Columns.Add("Identifiant", Type.GetType("System.String"))
            dtChargement.Columns.Add("Libellé", Type.GetType("System.String"))
            dtChargement.Rows.Clear()
            Dim cptr As Decimal = 0

            query = "SELECT * FROM t_comp_rubriqueent WHERE ETAT_RUB='Tableau Emplois Ressources' order by ID_RUBENT"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                Dim drS = dtChargement.NewRow()
                drS(0) = False
                drS(1) = "En-tête"
                drS(2) = MettreApost(rw("ID_RUBENT").ToString)
                drS(3) = MettreApost(rw("LIBELLE_RUBENT").ToString)
                dtChargement.Rows.Add(drS)
                'On selectionne les details qui sont directement lies au entete
                query = "SELECT * FROM t_comp_rubrique WHERE ID_RUBST='" & rw("ID_RUBENT") & "' AND ETAT_RUB='Tableau Emplois Ressources' ORDER BY CODE_RUB"
                Dim dtD As DataTable = ExcecuteSelectQuery(query)
                For Each rwD As DataRow In dtD.Rows
                    drS = dtChargement.NewRow()
                    drS(0) = False
                    drS(1) = "Détails"
                    drS(2) = MettreApost(rwD("CODE_RUB").ToString)
                    drS(3) = MettreApost(rwD("LIBELLE_RUB").ToString)
                    dtChargement.Rows.Add(drS)
                Next
                query = "SELECT * FROM t_comp_rubriquest WHERE ID_RUBENT='" & rw("ID_RUBENT") & "' AND ETAT_RUB='Tableau Emplois Ressources'"
                Dim dtSousTotal As DataTable = ExcecuteSelectQuery(query)
                For Each rwST As DataRow In dtSousTotal.Rows
                    drS = dtChargement.NewRow()
                    drS(0) = False
                    drS(1) = "Sous Total"
                    drS(2) = MettreApost(rwST("ID_RUBST").ToString)
                    drS(3) = MettreApost(rwST("LIBELLE_RUBST").ToString)
                    dtChargement.Rows.Add(drS)

                    query = "SELECT * FROM t_comp_rubrique WHERE ID_RUBST='" & rwST("ID_RUBST") & "' AND ETAT_RUB='Tableau Emplois Ressources' ORDER BY CODE_RUB"
                    Dim dtDetail As DataTable = ExcecuteSelectQuery(query)
                    For Each rwDetail As DataRow In dtDetail.Rows
                        drS = dtChargement.NewRow()
                        drS(0) = False
                        drS(1) = "Détails"
                        drS(2) = MettreApost(rwDetail("CODE_RUB").ToString)
                        drS(3) = MettreApost(rwDetail("LIBELLE_RUB").ToString)
                        dtChargement.Rows.Add(drS)
                    Next
                Next
            Next
            'On selectionne les details qui n'appartient pas a un entete
            query = "SELECT * FROM t_comp_rubrique WHERE ID_RUBST='' AND ETAT_RUB='Tableau Emplois Ressources' ORDER BY CODE_RUB"
            dt = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                Dim drS = dtChargement.NewRow()
                drS(0) = False
                drS(1) = ""
                drS(2) = ""
                drS(3) = ""
                dtChargement.Rows.Add(drS)
                For Each rwDetail As DataRow In dt.Rows
                    drS = dtChargement.NewRow()
                    drS(0) = False
                    drS(1) = "Détails"
                    drS(2) = MettreApost(rwDetail("CODE_RUB").ToString)
                    drS(3) = MettreApost(rwDetail("LIBELLE_RUB").ToString)
                    dtChargement.Rows.Add(drS)
                Next
            End If

            mondg.DataSource = dtChargement
            grid.Columns(0).Visible = False
            grid.Columns(1).MaxWidth = 100
            grid.Columns(2).MaxWidth = 100
            grid.OptionsView.ColumnAutoWidth = True
            'grid.Columns(3).Width = 430
            grid.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
            ColorRowGridAnal(grid, "[Type]='En-tête'", Color.SteelBlue, "Times New Roman", 10, FontStyle.Bold, Color.Black)
            ColorRowGridAnal(grid, "[Type]='Sous Total'", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub

    Sub RemplirCombo(ByVal comb As DevExpress.XtraEditors.ComboBoxEdit, ByVal matable As String, ByVal col As String, ByVal col1 As String)
        Try
            comb.Properties.Items.Clear()
            query = "select " & col & ", " & col1 & " from " & matable & " order by " & col
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                comb.Properties.Items.Add(MettreApost(rw(col).ToString) & "  " & MettreApost(rw(col1).ToString))
            Next
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Sub RemplirCombosc(ByVal comb As DevExpress.XtraEditors.ComboBoxEdit, ByVal matable As String, ByVal col As String, ByVal col1 As String)
        Try
            comb.Properties.Items.Clear()
            query = "select " & col & ", " & col1 & " from " & matable & " where " & col & " like '5%' order by " & col
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                comb.Properties.Items.Add(MettreApost(rw(col).ToString) & "  " & MettreApost(rw(col1).ToString))
            Next
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Sub RemplirComboText(ByVal txt As DevExpress.XtraEditors.ComboBoxEdit, ByVal matable As String, ByVal col As String, ByVal col1 As String, ByVal recev As String)
        Try
            txt.Text = ""
            query = "select " & col & ", " & col1 & " from " & matable & " where " & col & " = '" & EnleverApost(recev.ToString) & "' order by " & col
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                txt.Text = MettreApost(rw(col).ToString) & "  " & MettreApost(rw(col1).ToString)
            Next
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Sub RemplirCombo5(ByVal comb As DevExpress.XtraEditors.CheckedComboBoxEdit, ByVal matable As String, ByVal col As String, ByVal col1 As String, ByVal col2 As String, ByVal Codeprojet As String, ByVal CodeProjet1 As String)
        Try
            comb.Properties.Items.Clear()
            query = "select " & col & ", " & col1 & ", " & col2 & " from " & matable & " where " & Codeprojet & " = '" & CodeProjet1 & "' order by " & col
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw As DataRow In dt.Rows
                    comb.Properties.Items.Add(MettreApost(rw(col).ToString) & "  " & MettreApost(rw(col1).ToString) & " " & MettreApost(rw(col2).ToString))
                Next
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Sub RemplirCombo3(ByVal comb As ComboBox, ByVal matable As String, ByVal col As String, ByVal col1 As String, ByVal col2 As String, ByVal Codeprojet As String, ByVal CodeProjet1 As String)
        Try
            comb.Items.Clear()
            query = "select " & col & ", " & col1 & ", " & col2 & " from " & matable & " where " & Codeprojet & " = '" & CodeProjet1 & "' order by " & col
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw As DataRow In dt.Rows
                    comb.Items.Add(MettreApost(rw(col).ToString) & "  " & MettreApost(rw(col1).ToString) & " " & MettreApost(rw(col2).ToString))
                Next
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Sub RemplirCombo4(ByVal comb As DevExpress.XtraEditors.ComboBoxEdit, ByVal matable As String, ByVal col As String, ByVal col1 As String, ByVal Codeprojet As String, ByVal CodeProjet1 As String)
        Try
            comb.Properties.Items.Clear()
            query = "select " & col & ", " & col1 & " from " & matable & " where " & Codeprojet & " = '" & CodeProjet1 & "' order by " & col
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw As DataRow In dt.Rows
                    comb.Properties.Items.Add(MettreApost(rw(col).ToString) & "  " & MettreApost(rw(col1).ToString))
                Next
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Sub remplirDataGridimmo3(ByVal requete As String, ByVal mondg As DevExpress.XtraGrid.GridControl, ByVal dt As DataTable, ByVal nbre As DevExpress.XtraEditors.LabelControl, ByVal grid As DevExpress.XtraGrid.Views.Grid.GridView)
        Try
            'Dim a As Boolean
            dt.Columns.Clear()
            dt.Columns.Add("Code", Type.GetType("System.Boolean"))
            dt.Columns.Add("Identifiant Bien", Type.GetType("System.String"))
            dt.Columns.Add("Libellé du Bien", Type.GetType("System.String"))
            dt.Columns.Add("Date d'acquisition", Type.GetType("System.String"))
            dt.Columns.Add("Date de mise en service", Type.GetType("System.String"))
            dt.Columns.Add("Valeur d'acquisition", Type.GetType("System.String"))
            dt.Columns.Add("Etat", Type.GetType("System.String"))
            dt.Rows.Clear()


            Dim cptr As Decimal = 0
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw1 As DataRow In dt1.Rows
                cptr += 1
                Dim drS = dt.NewRow()
                drS(0) = TabTrue(cptr - 1)
                drS(1) = rw1(0).ToString
                drS(2) = MettreApost(rw1(1).ToString)
                drS(3) = CDate(rw1(2)).ToString("dd/MM/yyyy")
                drS(4) = CDate(rw1(3)).ToString("dd/MM/yyyy")
                drS(5) = AfficherMonnaie(Round(CDbl(rw1(4).ToString)))
                drS(6) = rw1(5).ToString
                dt.Rows.Add(drS)
            Next

            mondg.DataSource = dt
            nbre.Text = cptr.ToString & " Enregistrements"
            grid.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
            grid.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            grid.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
            grid.OptionsView.ColumnAutoWidth = True
            grid.OptionsBehavior.AutoExpandAllGroups = True
            grid.VertScrollVisibility = True
            grid.HorzScrollVisibility = True
            grid.BestFitColumns()
            grid.Columns(0).Width = 20

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub

    Sub remplirDataGridBoncommande(ByVal requete As String, ByVal mondg As DevExpress.XtraGrid.GridControl, ByVal nbre As DevExpress.XtraEditors.LabelControl, ByVal grid As DevExpress.XtraGrid.Views.Grid.GridView)
        Try
            dtimmo.Columns.Clear()
            dtimmo.Columns.Add("Code", Type.GetType("System.Boolean"))
            dtimmo.Columns.Add("Date", Type.GetType("System.String"))
            dtimmo.Columns.Add("Numéro", Type.GetType("System.String"))
            dtimmo.Columns.Add("Description du marché", Type.GetType("System.String"))
            dtimmo.Columns.Add("Demandeur", Type.GetType("System.String"))
            dtimmo.Columns.Add("Fournisseur", Type.GetType("System.String"))
            dtimmo.Columns.Add("Activité(s)", Type.GetType("System.String"))
            dtimmo.Columns.Add("Montant", Type.GetType("System.String"))
            dtimmo.Rows.Clear()

            Dim cptr As Decimal = 0
            Dim dt As DataTable = ExcecuteSelectQuery(requete)
            For Each rw As DataRow In dt.Rows
                cptr += 1
                Dim drS = dtimmo.NewRow()
                drS("Code") = TabTrue(cptr - 1)
                drS("Date") = rw(3).ToString
                drS("Numéro") = rw(1).ToString
                drS("Description du marché") = MettreApost(rw(2).ToString)
                drS("Demandeur") = rw(5).ToString
                drS("Fournisseur") = rw(4).ToString
                drS("Activité(s)") = ""
                drS("Montant") = AfficherMonnaie(Round(CDbl(rw(6).ToString)))
                dtimmo.Rows.Add(drS)
            Next

            mondg.DataSource = dtimmo
            nbre.Text = cptr.ToString & " Enregistrements"
            Dim edit As RepositoryItemCheckEdit = New RepositoryItemCheckEdit()
            edit.ValueChecked = True
            edit.ValueUnchecked = False
            grid.Columns("Code").ColumnEdit = edit
            mondg.RepositoryItems.Add(edit)
            grid.OptionsBehavior.Editable = True

            grid.Columns("Date").OptionsColumn.AllowEdit = False
            grid.Columns("Numéro").OptionsColumn.AllowEdit = False
            grid.Columns("Description du marché").OptionsColumn.AllowEdit = False
            grid.Columns("Demandeur").OptionsColumn.AllowEdit = False
            grid.Columns("Fournisseur").OptionsColumn.AllowEdit = False
            grid.Columns("Activité(s)").OptionsColumn.AllowEdit = False
            grid.Columns("Montant").OptionsColumn.AllowEdit = False

            grid.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
            grid.OptionsView.ColumnAutoWidth = True
            grid.OptionsBehavior.AutoExpandAllGroups = True
            grid.VertScrollVisibility = True
            grid.HorzScrollVisibility = True
            grid.BestFitColumns()

            grid.Columns("Date").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Columns("Numéro").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            'grid.Columns("Description du marché").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Columns("Demandeur").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Columns("Fournisseur").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Columns("Activité(s)").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Columns("Montant").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            grid.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub

    Sub remplirDataGridarticlestock(ByVal requete As String, ByVal mondg As DevExpress.XtraGrid.GridControl, ByVal nbre As DevExpress.XtraEditors.LabelControl, ByVal grid As DevExpress.XtraGrid.Views.Grid.GridView)
        Try

            dtarticle.Columns.Clear()
            dtarticle.Columns.Add("Code", Type.GetType("System.Boolean"))
            dtarticle.Columns.Add("CodePDT", Type.GetType("System.String"))
            dtarticle.Columns.Add("Famille", Type.GetType("System.String"))
            dtarticle.Columns.Add("Libellé", Type.GetType("System.String"))
            dtarticle.Columns.Add("Quantité", Type.GetType("System.String"))
            dtarticle.Columns.Add("Prix Unitaire", Type.GetType("System.String"))
            dtarticle.Columns.Add("StockMin", Type.GetType("System.String"))
            dtarticle.Columns.Add("UnitéM", Type.GetType("System.String"))
            dtarticle.Columns.Add("périssable", Type.GetType("System.String"))
            dtarticle.Columns.Add("date", Type.GetType("System.String"))
            dtarticle.Columns.Add("Image", GetType(Bitmap))
            dtarticle.Rows.Clear()


            Dim cptr As Decimal = 0
            Dim dt As DataTable = ExcecuteSelectQuery(requete)
            For Each rw In dt.Rows
                cptr += 1
                Dim drS = dtarticle.NewRow()
                drS(0) = TabTrue(cptr - 1)
                drS(1) = rw(0).ToString
                drS(2) = MettreApost(rw(1)).ToString
                drS(3) = MettreApost(rw(2)).ToString
                drS(4) = rw(8).ToString
                drS(5) = AfficherMonnaie(Round(CDbl(rw(9).ToString), 0))
                drS(6) = rw(3).ToString
                drS(7) = rw(4).ToString
                drS(8) = rw(5).ToString
                If rw(6).ToString = "" Then
                    drS(9) = rw(6).ToString
                Else
                    drS(9) = CDate(rw(6)).ToString("dd/MM/yyyy")
                End If
                drS(10) = Bitmap.FromStream(New MemoryStream(CType(rw(7), Byte())))
                dtarticle.Rows.Add(drS)
            Next

            mondg.DataSource = dtarticle
            nbre.Text = cptr.ToString & " Enregistrements"
            Dim edit As RepositoryItemCheckEdit = New RepositoryItemCheckEdit()
            edit.ValueChecked = True
            edit.ValueUnchecked = False
            grid.Columns("Code").ColumnEdit = edit
            mondg.RepositoryItems.Add(edit)
            grid.OptionsBehavior.Editable = True

            Dim imageraph As RepositoryItemPictureEdit = New RepositoryItemPictureEdit()
            imageraph.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
            imageraph.BestFitWidth = 200
            grid.Columns("Image").ColumnEdit = imageraph

            grid.Columns("Famille").OptionsColumn.AllowEdit = False
            grid.Columns("Libellé").OptionsColumn.AllowEdit = False
            grid.Columns("Quantité").OptionsColumn.AllowEdit = False
            grid.Columns("Prix Unitaire").OptionsColumn.AllowEdit = False
            grid.Columns("StockMin").OptionsColumn.AllowEdit = False
            grid.Columns("UnitéM").OptionsColumn.AllowEdit = False
            grid.Columns("périssable").OptionsColumn.AllowEdit = False
            grid.Columns("date").OptionsColumn.AllowEdit = False
            grid.Columns("Image").OptionsColumn.AllowEdit = False

            grid.Columns(1).Visible = False
            grid.Columns(8).Visible = False
            'grid.Columns(4).Visible = False
            'grid.Columns(5).Visible = False
            grid.OptionsView.ColumnAutoWidth = True
            grid.OptionsBehavior.AutoExpandAllGroups = True
            grid.VertScrollVisibility = True
            grid.HorzScrollVisibility = True
            grid.BestFitColumns()
            grid.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            grid.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Columns(7).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Columns(8).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Columns(9).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Columns(10).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub

    Sub remplirDataGridfamstock(ByVal requete As String, ByVal mondg As DevExpress.XtraGrid.GridControl, ByVal nbre As DevExpress.XtraEditors.LabelControl, ByVal grid As DevExpress.XtraGrid.Views.Grid.GridView)
        Try

            dtfamm.Columns.Clear()
            dtfamm.Columns.Add("Choix", Type.GetType("System.Boolean"))
            dtfamm.Columns.Add("Code", Type.GetType("System.String"))
            dtfamm.Columns.Add("Libellé Famille", Type.GetType("System.String"))
            dtfamm.Rows.Clear()
            Dim cptr As Decimal = 0

            Dim dt As DataTable = ExcecuteSelectQuery(requete)
            For Each rw In dt.Rows
                cptr += 1
                Dim drS = dtfamm.NewRow()
                drS(0) = TabTrue(cptr - 1)
                drS(1) = rw(0).ToString
                drS(2) = MettreApost(rw(1).ToString)
                dtfamm.Rows.Add(drS)
            Next

            mondg.DataSource = dtfamm
            nbre.Text = cptr.ToString & " Enregistrements"
            Dim edit As RepositoryItemCheckEdit = New RepositoryItemCheckEdit()
            edit.ValueChecked = True
            edit.ValueUnchecked = False
            grid.Columns("Choix").ColumnEdit = edit
            mondg.RepositoryItems.Add(edit)
            grid.OptionsBehavior.Editable = True

            grid.Columns(1).Visible = False
            grid.OptionsView.ColumnAutoWidth = True
            grid.OptionsBehavior.AutoExpandAllGroups = True
            grid.VertScrollVisibility = True
            grid.HorzScrollVisibility = True
            grid.BestFitColumns()
            grid.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
            grid.Columns(0).Width = 20

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub


    Sub RemplirDatagridcpttiers(ByVal mondg As DevExpress.XtraGrid.GridControl)

        dtCompteTier.Columns.Clear()
        dtCompteTier.Columns.Add("Choix", Type.GetType("System.Boolean"))
        dtCompteTier.Columns.Add("Code", Type.GetType("System.String"))
        dtCompteTier.Columns.Add("Intitulé", Type.GetType("System.String"))
        dtCompteTier.Columns.Add("Abréviation", Type.GetType("System.String"))
        dtCompteTier.Columns.Add("Adresse", Type.GetType("System.String"))
        dtCompteTier.Columns.Add("Email", Type.GetType("System.String"))
        dtCompteTier.Columns.Add("Compte Collectif", Type.GetType("System.String"))
        dtCompteTier.Rows.Clear()

        Dim cptr As Decimal = 0
        query = "select t_comp_rattach_tiers.CODE_SC, t_comp_rattach_tiers.CPT_TIER, t_comp_rattach_tiers.CODE_CPT, t_comp_type_compte.LIBELLE_TCPT from t_comp_rattach_tiers, t_comp_type_compte where t_comp_rattach_tiers.CODE_TCPT=t_comp_type_compte.CODE_TCPT  and t_comp_rattach_tiers.code_projet='" & ProjetEnCours & "' order by t_comp_rattach_tiers.CODE_CPT"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows

            query = "select NOM_CPT,ADRESSE_CPT,EMAIL,CODE_CPT from t_comp_compte where ABREGE_CPT='" & rw(1).ToString & "' and CODE_CPT='" & rw(2).ToString & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw1 In dt1.Rows
                cptr += 1
                Dim drS = dtCompteTier.NewRow()
                drS(0) = TabTrue(cptr - 1)
                drS(1) = rw(2).ToString
                drS(2) = MettreApost(rw1(0).ToString)
                drS(3) = MettreApost(rw(1).ToString)
                drS(4) = MettreApost(rw1(1).ToString)
                drS(5) = MettreApost(rw1(2).ToString)
                drS(6) = MettreApost(rw(0).ToString)
                dtCompteTier.Rows.Add(drS)
            Next
        Next

        mondg.DataSource = dtCompteTier
        Plan_tiers.ViewCptTiers.Columns(0).Visible = False
        Plan_tiers.ViewCptTiers.OptionsView.ColumnAutoWidth = True
        Plan_tiers.ViewCptTiers.OptionsBehavior.AutoExpandAllGroups = True
        Plan_tiers.ViewCptTiers.VertScrollVisibility = True
        Plan_tiers.ViewCptTiers.HorzScrollVisibility = True
        Plan_tiers.ViewCptTiers.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)

    End Sub


    Sub remplirDataGridimmo(ByVal datagridimmo As DataGridView)

        Try
            datagridimmo.Rows.Clear()
            Dim nb As Decimal = 0
            query = "select t_im_biens.CODE_BIENS,t_im_biens.LIBELLE_BIENS,t_im_biens.DATE_ACQUISITION,t_im_mise_en_service.DATE_MES,t_im_biens.VALEUR_ACQUISITION,t_im_biens.ETAT_SORTIES from t_im_biens,t_im_mise_en_service where t_im_biens.CODE_BIENS=t_im_mise_en_service.CODE_BIENS and t_im_biens.codeprojet='" & ProjetEnCours & "' ORDER BY t_im_biens.CODE_BIENS"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                etat = rw(5).ToString
                If etat <> "" Then
                    datagridimmo.Rows.Add()
                    datagridimmo.Rows(nb).Cells(0).Value = False
                    datagridimmo.Rows(nb).Cells(1).Value = rw(0).ToString
                    datagridimmo.Rows(nb).Cells(2).Value = rw(1).ToString
                    datagridimmo.Rows(nb).Cells(3).Value = CDate(rw(2)).ToString("dd/MM/yyyy")
                    datagridimmo.Rows(nb).Cells(4).Value = CDate(rw(3)).ToString("dd/MM/yyyy")
                    datagridimmo.Rows(nb).Cells(5).Value = rw(4).ToString
                    datagridimmo.Rows(nb).Cells(6).Value = "oui"
                    nb = nb + 1
                Else
                    datagridimmo.Rows.Add()
                    datagridimmo.Rows(nb).Cells(0).Value = False
                    datagridimmo.Rows(nb).Cells(1).Value = rw(0).ToString
                    datagridimmo.Rows(nb).Cells(2).Value = rw(1).ToString
                    datagridimmo.Rows(nb).Cells(3).Value = CDate(rw(2)).ToString("dd/MM/yyyy")
                    datagridimmo.Rows(nb).Cells(4).Value = CDate(rw(3)).ToString("dd/MM/yyyy")
                    datagridimmo.Rows(nb).Cells(5).Value = rw(4).ToString
                    datagridimmo.Rows(nb).Cells(6).Value = ""
                    nb = nb + 1
                End If
            Next

        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Public Sub CentrerForm(ByVal F As Form)
        F.Location = New Point((
     Screen.PrimaryScreen.Bounds.Width - F.Width) / 2,
     (Screen.PrimaryScreen.Bounds.Height - F.Height) / 2)
    End Sub

    Public Sub CorrectionChaine(ByRef TexteACorriger As String)
        If (TexteACorriger <> "") Then
            TexteACorriger = TexteACorriger.Replace("'", "&apost;").Replace("\", "\\")
        Else
            TexteACorriger = ""
        End If
    End Sub

    Public Function EnleverApost(ByVal TexteA As String) As String
        Return TexteA.Replace("'", "&apost;").Replace("\", "\\")
    End Function

    Public Function MettreApost(ByVal TexteB As String) As String
        TexteB = TexteB.Replace("&apost;", "'")
        TexteB = TexteB.Replace("\'", "'")
        Return TexteB.Replace("&APOST;", "'")
    End Function

    Public Sub RestaurerChaine(ByRef TexteARestaurer As String)
        If (TexteARestaurer <> "") Then
            TexteARestaurer = TexteARestaurer.Replace("&apost;", "'")
        Else
            TexteARestaurer = ""
        End If

    End Sub

    Public Sub EspionData(ByVal form As Object)
        Dim Line1 As String
        Dim DernierAcces, Actu As String
        'Dim Rep As Decimal
        DernierAcces = "01/01/1900 00:00:00"

        FileOpen(1, "C:\Windows\System32\espion.txt", OpenMode.Input)
        While Not EOF(1)
            Line1 = LineInput(1)
            If DernierAcces <> "01/01/1900 00:00:00" Then
                Exit While
            Else
                DernierAcces = Line1
            End If
        End While
        FileClose(1)

        Actu = My.Computer.Clock.LocalTime.ToString()

        If DateTime.Compare(DernierAcces, Actu) > 0 Then
            Beep()
            OkDate = False
        Else
            OkDate = True
        End If


    End Sub

    Public Sub ReduirePourPanel(ByVal form1 As Object)
        If (form1.Size.Width = largeur - 29 And form1.Size.Height = hauteur - 171 And form1.FormBorderStyle = Windows.Forms.FormBorderStyle.None) Then
            form1.Size = New System.Drawing.Size(largeur - 29 - 260, hauteur - 171)
            form1.Location = New System.Drawing.Point(260, 0)
        End If

    End Sub

    Public Sub RestaurerPourPanel(ByVal form2 As Object)
        If ((form2.Size.Width = largeur - 29 - 260 And form2.Size.Height = hauteur - 171 And form2.FormBorderStyle = Windows.Forms.FormBorderStyle.None)) Then 'Or ForcerAction = True) Then
            form2.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            form2.Size = New System.Drawing.Size(largeur - 29, hauteur - 171)
            form2.Location = New System.Drawing.Point(0, 0)
        End If

    End Sub

    Public Sub ReduireFenetre(ByVal form2 As Object)
        form2.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        form2.WindowState = FormWindowState.Minimized
    End Sub

    Public Sub FaireFlotter(ByVal form2 As Object)
        form2.Size = New System.Drawing.Size(700, 500)
        form2.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
        form2.WindowState = FormWindowState.Normal
    End Sub

    Public Sub AgrandirFenetre(ByVal form2 As Object)
        form2.WindowState = FormWindowState.Maximized
    End Sub

    Public Function TrouverDate(ByVal Date1 As String)
        Dim Partie() As String = Date1.Split("/"c)
        Dim DateExacte As String = ""
        Dim DateRetour As Date = "01/01/1900"
        'My.Computer.Clock.LocalTime.ToString
        'DateRetour = DateRetour.ToShortDateString
        Dim NbElt As Decimal = 0
        For Each Elt As String In Partie
            NbElt = NbElt + 1
            If (Len(Partie(NbElt - 1)) < 1) Then
                Return DateRetour
                Exit Function
            ElseIf (Len(Partie(NbElt - 1)) < 2 And NbElt <> 3) Then
                DateExacte = DateExacte & "0" & Partie(NbElt - 1)
                If (NbElt < 3) Then DateExacte = DateExacte & "/"
            ElseIf (Len(Partie(NbElt - 1)) = 2 And NbElt <> 3) Then
                DateExacte = DateExacte & Partie(NbElt - 1)
                If (NbElt < 3) Then DateExacte = DateExacte & "/"
            ElseIf (Len(Partie(NbElt - 1)) > 2 And NbElt <> 3) Then
                Return DateRetour
                Exit Function
            ElseIf (Len(Partie(NbElt - 1)) < 2 And NbElt = 3) Then
                Return DateRetour
                Exit Function
            ElseIf (Len(Partie(NbElt - 1)) = 2 And NbElt = 3) Then
                DateExacte = DateExacte & "20" & Partie(NbElt - 1)
            ElseIf (Len(Partie(NbElt - 1)) = 4 And NbElt = 3) Then
                DateExacte = DateExacte & Partie(NbElt - 1)
            Else
                Return DateRetour

            End If
        Next
        If (NbElt = 3) Then
            DateRetour = CDate(DateExacte)
        End If
        Return DateRetour
    End Function

    Public Function AfficherMonnaie(ByVal montant As String) As String
        If montant.Trim().Length = 0 Then
            Return 0
        End If
        Dim isdecimal As Boolean = False
        If montant.Contains(",") Then
            isdecimal = True
        End If
        If montant.Contains(".") Then
            isdecimal = True
        End If
        If isdecimal Then
            Return FormatNumber(CDec(montant.Replace(".", ",")), 2).Replace(Chr(160), " ")
        Else
            Return FormatNumber(CDec(montant.Replace(".", ",")), 0).Replace(Chr(160), " ")
        End If
        Dim Nb As Decimal = 0
        Dim Valeur As String = "0"
        Dim ValDecim As String = "-"
        Dim ValRet As String = ""
        Dim DifPartie() As String
        montant = montant.Replace(" ", "")
        DifPartie = montant.Split(","c)
        For Each p As String In DifPartie
            Nb = Nb + 1
            If (Nb = 2) Then
                ValDecim = DifPartie(1)
            End If
        Next
        montant = DifPartie(0)
        If (Len(montant) <= 3) Then
            Valeur = montant
        Else

            If (Len(montant) = 4) Then Valeur = Mid(montant, 1, 1) & " " & Mid(montant, 2, 3)
            If (Len(montant) = 5) Then Valeur = Mid(montant, 1, 2) & " " & Mid(montant, 3, 3)
            If (Len(montant) = 6) Then Valeur = Mid(montant, 1, 3) & " " & Mid(montant, 4, 3)
            If (Len(montant) = 7) Then Valeur = Mid(montant, 1, 1) & " " & Mid(montant, 2, 3) & " " & Mid(montant, 5, 3)
            If (Len(montant) = 8) Then Valeur = Mid(montant, 1, 2) & " " & Mid(montant, 3, 3) & " " & Mid(montant, 6, 3)
            If (Len(montant) = 9) Then Valeur = Mid(montant, 1, 3) & " " & Mid(montant, 4, 3) & " " & Mid(montant, 7, 3)
            If (Len(montant) = 10) Then Valeur = Mid(montant, 1, 1) & " " & Mid(montant, 2, 3) & " " & Mid(montant, 5, 3) & " " & Mid(montant, 8, 3)
            If (Len(montant) = 11) Then Valeur = Mid(montant, 1, 2) & " " & Mid(montant, 3, 3) & " " & Mid(montant, 6, 3) & " " & Mid(montant, 9, 3)
            If (Len(montant) = 12) Then Valeur = Mid(montant, 1, 3) & " " & Mid(montant, 4, 3) & " " & Mid(montant, 7, 3) & " " & Mid(montant, 10, 3)
            If (Len(montant) = 13) Then Valeur = Mid(montant, 1, 1) & " " & Mid(montant, 2, 3) & " " & Mid(montant, 5, 3) & " " & Mid(montant, 8, 3) & " " & Mid(montant, 11, 3)
            If (Len(montant) = 14) Then Valeur = Mid(montant, 1, 2) & " " & Mid(montant, 3, 3) & " " & Mid(montant, 6, 3) & " " & Mid(montant, 9, 3) & " " & Mid(montant, 12, 3)
            If (Len(montant) = 15) Then Valeur = Mid(montant, 1, 3) & " " & Mid(montant, 4, 3) & " " & Mid(montant, 7, 3) & " " & Mid(montant, 10, 3) & " " & Mid(montant, 13, 3)

        End If
        ValRet = Valeur
        If (ValDecim <> "-") Then ValRet = Valeur & "," & ValDecim
        Return ValRet
        'Return FormatNumber(montant, 0)
    End Function

    Public Function MontantLettre(ByVal Valeur As String, Optional ByVal virgule As String = " virgule ") As String
        Valeur = Valeur.Replace(".", ",")
        Dim partval() As String = Valeur.Split(","c)
        Dim RepVal As String = ""
        If (partval.Length > 1) Then
            RepVal = MontantBrut(partval(0)).Replace(" zero", "") & virgule & MontantBrut(partval(1)).Replace(" zero", "")
        Else
            RepVal = MontantBrut(Valeur).Replace(" zero", "")
        End If

        Return RepVal
    End Function

    Public Function MontantBrut(ByVal ValBrut As String) As String

        If (ValBrut = "" Or ValBrut = "-") Then Return ""

        Dim NewVal As Decimal = CDec(ValBrut.Replace("-", ""))
        ValBrut = NewVal.ToString

        If (ValBrut = "0") Then Return "zero"
        If (ValBrut = "1") Then Return "un"
        If (ValBrut = "2") Then Return "deux"
        If (ValBrut = "3") Then Return "trois"
        If (ValBrut = "4") Then Return "quatre"
        If (ValBrut = "5") Then Return "cinq"
        If (ValBrut = "6") Then Return "six"
        If (ValBrut = "7") Then Return "sept"
        If (ValBrut = "8") Then Return "huit"
        If (ValBrut = "9") Then Return "neuf"
        If (ValBrut = "10") Then Return "dix"
        If (ValBrut = "11") Then Return "onze"
        If (ValBrut = "12") Then Return "douze"
        If (ValBrut = "13") Then Return "treize"
        If (ValBrut = "14") Then Return "quatorze"
        If (ValBrut = "15") Then Return "quinze"
        If (ValBrut = "16") Then Return "seize"
        If (ValBrut = "17") Then Return "dix-sept"
        If (ValBrut = "18") Then Return "dix-huit"
        If (ValBrut = "19") Then Return "dix-neuf"
        If (ValBrut = "20") Then Return "vingt"
        If (ValBrut = "30") Then Return "trente"
        If (ValBrut = "40") Then Return "quarante"
        If (ValBrut = "50") Then Return "cinquante"
        If (ValBrut = "60") Then Return "soixante"
        If (ValBrut = "70") Then Return "soixante-dix"
        If (ValBrut = "80") Then Return "quatre-vingt"
        If (ValBrut = "90") Then Return "quatre-vingt-dix"
        If (ValBrut = "100") Then Return "cent"
        If (ValBrut = "1000") Then Return "mille"
        If (ValBrut = "1000000") Then Return "million"
        If (ValBrut = "1000000000") Then Return "milliard"

        If (Len(ValBrut) = 2 And CDec(ValBrut) > 20) Then
            If (CDec(ValBrut) = 0) Then
                Return ""
            Else
                If (Mid(ValBrut, 1, 1) <> "7" And Mid(ValBrut, 1, 1) <> "9" And Mid(ValBrut, 2, 1) = "1") Then
                    Return (MontantLettre(Mid(ValBrut, 1, 1) & "0") & " et " & MontantLettre(Mid(ValBrut, 2, 1)))
                ElseIf (Mid(ValBrut, 1, 1) <> "7" And Mid(ValBrut, 1, 1) <> "9" And Mid(ValBrut, 2, 1) <> "1") Then
                    Return (MontantLettre(Mid(ValBrut, 1, 1) & "0") & " " & MontantLettre(Mid(ValBrut, 2, 1)))
                ElseIf (Mid(ValBrut, 1, 1) = "7" Or Mid(ValBrut, 1, 1) = "9") Then
                    Return (MontantLettre((CInt(Mid(ValBrut, 1, 1)) - 1).ToString & "0") & " " & MontantLettre("1" & Mid(ValBrut, 2, 1)))
                End If
            End If

        ElseIf (Len(ValBrut) = 3 And CDec(ValBrut) > 100) Then
            If (CDec(ValBrut) = 0) Then
                Return ""
            Else
                If (CDec(Mid(ValBrut, 2, 2)) > 20) Then
                    If (CDec(Mid(ValBrut, 1, 1)) > 1) Then
                        Return (MontantLettre(Mid(ValBrut, 1, 1)) & " " & MontantLettre("100") & " " & MontantLettre(Mid(ValBrut, 2)))
                    Else
                        Return (MontantLettre("100") & " " & MontantLettre(Mid(ValBrut, 2)))
                    End If
                Else
                    If (CDec(Mid(ValBrut, 1, 1)) > 1) Then
                        Return (MontantLettre(Mid(ValBrut, 1, 1)) & " " & MontantLettre("100") & " " & MontantLettre(Mid(ValBrut, 2, 2)))
                    Else
                        Return (MontantLettre("100") & " " & MontantLettre(Mid(ValBrut, 2, 2)))
                    End If
                End If
            End If

        ElseIf (Len(ValBrut) > 3 And Len(ValBrut) <= 6) Then
            If (CDec(ValBrut) = 0) Then
                Return ""
            Else
                If (Len(ValBrut) = 4 And CDec(Mid(ValBrut, 1, 1)) = 1) Then
                    Return (MontantLettre("1000") & " " & MontantLettre(Mid(ValBrut, Len(ValBrut) - 2)))
                Else
                    Return (MontantLettre(Mid(ValBrut, 1, Len(ValBrut) - 3)) & " " & MontantLettre("1000") & " " & MontantLettre(Mid(ValBrut, Len(ValBrut) - 2)))
                End If
            End If

        ElseIf (Len(ValBrut) > 6 And Len(ValBrut) <= 9) Then
            If (CDec(ValBrut) = 0) Then
                Return ""
            Else
                If (Len(ValBrut) = 7 And Mid(ValBrut, 1, 1) = "1") Then
                    Return (MontantLettre(Mid(ValBrut, 1, Len(ValBrut) - 6)) & " " & MontantLettre("1000000") & " " & MontantLettre(Mid(ValBrut, Len(ValBrut) - 5)))
                Else
                    Return (MontantLettre(Mid(ValBrut, 1, Len(ValBrut) - 6)) & " " & MontantLettre("1000000") & "s " & MontantLettre(Mid(ValBrut, Len(ValBrut) - 5)))
                End If
            End If

        ElseIf (Len(ValBrut) > 9 And Len(ValBrut) <= 15) Then
            If (CDec(ValBrut) = 0) Then
                Return ""
            Else
                If (Len(ValBrut) = 10 And Mid(ValBrut, 1, 1) = "1") Then
                    Return (MontantLettre(Mid(ValBrut, 1, Len(ValBrut) - 9)) & " " & MontantLettre("1000000000") & " " & MontantLettre(Mid(ValBrut, Len(ValBrut) - 8)))
                Else
                    Return (MontantLettre(Mid(ValBrut, 1, Len(ValBrut) - 9)) & " " & MontantLettre("1000000000") & "s " & MontantLettre(Mid(ValBrut, Len(ValBrut) - 8)))
                End If
            End If
        ElseIf (CDec(ValBrut) = 0) Then
            Return ""
        Else
            Return "Non Traité"
        End If
        Return ""

    End Function

    Public Function NbreJourDansPeriode(ByVal Date1 As Date, ByVal Date2 As Date, ByVal Lun As Boolean, ByVal Mar As Boolean, ByVal Mer As Boolean, ByVal Jeu As Boolean, ByVal Ven As Boolean, ByVal Sam As Boolean, ByVal Dima As Boolean) As Decimal
        Dim Tamp As Date
        If (Date.Compare(Date1, Date2) > 0) Then
            Tamp = Date1
            Date1 = Date2
            Date2 = Tamp
        End If

        Dim NbreX As Decimal = 0
        While (DateTime.Compare(Date1, Date2) < 0)

            If (Lun = False And Date1.DayOfWeek = DayOfWeek.Monday) Then NbreX = NbreX + 1
            If (Mar = False And Date1.DayOfWeek = DayOfWeek.Tuesday) Then NbreX = NbreX + 1
            If (Mer = False And Date1.DayOfWeek = DayOfWeek.Wednesday) Then NbreX = NbreX + 1
            If (Jeu = False And Date1.DayOfWeek = DayOfWeek.Thursday) Then NbreX = NbreX + 1
            If (Ven = False And Date1.DayOfWeek = DayOfWeek.Friday) Then NbreX = NbreX + 1
            If (Sam = False And Date1.DayOfWeek = DayOfWeek.Saturday) Then NbreX = NbreX + 1
            If (Dima = False And Date1.DayOfWeek = DayOfWeek.Sunday) Then NbreX = NbreX + 1
            Date1 = Date1.AddDays(1)

        End While

        Return NbreX

    End Function
    Public Sub Progression(ByVal Prct As Decimal)
        While Prct > 100
            Prct = Prct - 100
        End While
    End Sub
    Public Sub VerifSaisieMontant(ByRef Zone As Object)

        Try
            Zone.Text = Zone.Text.Replace(".", ",")
            'If ((Zone.Text = DialogSeuil.MontantPlanche.Text Or Zone.Text = DialogSeuil.MontantPlafond.Text) And (Zone.Text = "NL" Or Zone.Text = "TM")) Then
            If ((Zone.Text = DialogSeuil.MontantPlanche.Text Or Zone.Text = DialogSeuil.MontantPlafond.Text) And (Zone.Text = "NL" Or Zone.Text = "TM")) Then
            Else
                If (Zone.Text <> "") Then
                    If (IsNumeric(Zone.Text.Replace(" ", ""))) Then
                        Zone.Text = AfficherMonnaie(Zone.Text.Replace(" ", ""))
                        Zone.Select(Zone.Text.Length, 0)
                    Else
                        SuccesMsg("Vous avez tapé un caractère non autorisé !")
                        Zone.Text = Mid(Zone.Text, 1, Zone.Text.Length - 1)
                    End If
                End If
            End If
        Catch ex As Exception
            SuccesMsg(ex.ToString)
        End Try

    End Sub

    Public Function ExtensionImage(ByVal Chemin As String) As String
        'Dim ValRet As String = ""
        'Dim Nbre As Decimal = 0
        'If (Chemin <> "") Then
        '    Dim Parties() As String = Chemin.Split("."c)
        '    For Each elt In Parties
        '        Nbre = Nbre + 1
        '    Next
        '    ValRet = Parties(Nbre - 1)
        'End If
        'Return ValRet

        Return New IO.FileInfo(Chemin).Extension

    End Function

    Public Sub Disposer_form(ByVal form As Object)

        If (AccessWall = "&Admin&" Or AccessWall.Contains(form.Name) = True) Then
            If (form.Visible = False) Then
                DebutChargement()
                form.mdiparent = ClearMdi
                form.StartPosition = FormStartPosition.CenterParent
                form.TopMost = True
                form.show()
            Else
                form.BringToFront()
            End If

        Else
            FailMsg("Accès non autorisé.")
        End If
    End Sub

    Public Sub Dialog_form(ByVal form As Object)
        If (AccessWall = "&Admin&" Or AccessWall.Contains(form.Name) = True) Then
            form.ShowDialog()
        Else
            FailMsg("Accès non autorisé.")
        End If
    End Sub
    Public Function Access_Btn(ByVal Name As String) As Boolean
        If (AccessWall = "&Admin&" Or AccessWall.Contains(Name) = True) Then
            Return True
        Else
            FailMsg("Accès non autorisé.")
            Return False
        End If
    End Function

    'Shared

    Public Function GetRandomGuid() As Guid
        Dim bytes = New Byte(15) {}
        Dim generator = New RNGCryptoServiceProvider()
        generator.GetBytes(bytes)
        Return New Guid(bytes)
    End Function

    'Public Function AESCounter(ByVal key As Byte(), ByVal counter As ULong) As Byte()
    '    Dim InputBlock As Byte() = New Byte(15) {}
    '    InputBlock(0) = CByte(counter And &HFFL)
    '    InputBlock(1) = CByte((counter And &HFF00L) >> 8)
    '    InputBlock(2) = CByte((counter And &HFF0000L) >> 16)
    '    InputBlock(3) = CByte((counter And &HFF000000L) >> 24)
    '    InputBlock(4) = CByte((counter And &HFF00000000L) >> 32)
    '    InputBlock(5) = CByte((counter And &HFF0000000000L) >> 40)
    '    InputBlock(6) = CByte((counter And &HFF000000000000L) >> 48)
    '    InputBlock(7) = CByte((counter And &HFF00000000000000L) >> 54)

    '    Using AES As AesCryptoServiceProvider = New AesCryptoServiceProvider()
    '        AES.Key = key
    '        AES.Mode = CipherMode.ECB
    '        AES.Padding = PaddingMode.None

    '        Using Encryptor As ICryptoTransform = AES.CreateEncryptor()
    '            Return Encryptor.TransformFinalBlock(InputBlock, 0, 16)
    '        End Using
    '    End Using
    'End Function


    Public Sub EtatPleinEcran(ByRef Etat As CrystalDecisions.Windows.Forms.CrystalReportViewer, ByVal Titre As String)
        FullScreenReport.FullView.ReportSource = Etat.ReportSource
        FullScreenReport.Text = Titre & " [MODE PLEIN ECRAN]"
        FullScreenReport.ShowDialog()
    End Sub

    Public Function GenererCode(ByVal LongCode As Decimal) As String
        Dim EnsCar As String = "aA1bB#2cC@3dD4$eE5fF&6gG7h!H{}8iI9jJAkKB=lLC%mMDnNEo*OFpPGqQHrRIsSJtTKuULvVMwWNxXOyYPzZQ"
        Dim LeCode As String = ""

        While Len(LeCode) < LongCode
            If LeCode.Length >= 1 Then
                Dim Generator As System.Random = New System.Random()
                Dim nb As Decimal = Generator.Next(79)
                If nb = 0 Then nb = 1
                Dim NewChar As String = Mid(EnsCar, nb, 1)
                While LeCode.Contains(NewChar)
                    nb = Generator.Next(79)
                    If nb = 0 Then nb = 1
                    NewChar = Mid(EnsCar, nb, 1)
                End While
                LeCode = LeCode & NewChar
            Else
                Dim Generator As System.Random = New System.Random()
                Dim nb As Decimal = Generator.Next(79)
                If nb = 0 Then nb = 1
                Dim NewChar As String = Mid(EnsCar, nb, 1)
                LeCode = LeCode & NewChar
            End If
        End While
        Return LeCode
    End Function

    Public Function CreerTableFournitures(ByVal nomFourniture As String) As String

        Dim nomTable As String = (((nomFourniture.Replace(" ", "_")).Replace("é", "e")).Replace("è", "e")).Replace("à", "a").Replace(",", "").Replace("'", "").Replace("-", "")
        If (Len(nomTable) > 18) Then
            nomTable = Mid(nomTable, 1, 18)
        End If
        Dim nomT_Carac As String = "T_PredCaract_" & nomTable
        If (Len(nomT_Carac) > 25) Then nomT_Carac = Mid(nomT_Carac, 1, 25)

        nomTable = "T_Pred_" & nomTable

        'Code de creation de table pour les fournitures predefinies **********************
        Try

            query = "CREATE TABLE " & nomTable & " (RefPredFItem INT NOT NULL AUTO_INCREMENT,RefPredFGroupe INT NOT NULL ,LibellePredFItem VARCHAR(500) NOT NULL, PRIMARY KEY (RefPredFItem));"
            ExecuteNonQuery(query)

            query = "ALTER TABLE " & nomTable & " ADD CONSTRAINT FK_" & nomTable & "_T_P_Groupe FOREIGN KEY (RefPredFGroupe) REFERENCES T_PredFournitures_Groupe (RefPredFGroupe)"
            ExecuteNonQuery(query)

            '******* Table des caractéristiques *************
            query = "CREATE TABLE " & nomT_Carac & " (RefPredFCaract INT NOT NULL AUTO_INCREMENT,RefPredFItem INT NOT NULL ,LibellePredFCaract VARCHAR(500) NOT NULL, PRIMARY KEY (RefPredFCaract));"
            ExecuteNonQuery(query)

            query = "ALTER TABLE " & nomT_Carac & " ADD CONSTRAINT FK_" & nomT_Carac & "_" & nomTable & " FOREIGN KEY (RefPredFItem) REFERENCES " & nomTable & " (RefPredFItem)"
            ExecuteNonQuery(query)

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        End Try
        '************************************************************************************

        'Retour du nom de la table créée 
        Return nomTable

    End Function

    Public Sub ColorRowGrid(ByRef LeGrid As GridView, ByVal ColonneCible As String, ByVal CouleurLigne As Color, ByVal LaPolice As String, ByVal TaillePolice As Decimal, ByVal LeStyle As FontStyle, ByVal CouleurPolice As Color)
        Try
            If (LeGrid.RowCount > 0) Then
                Dim styleFormatCondition1 As New DevExpress.XtraGrid.StyleFormatCondition
                LeGrid.FormatConditions.Clear()
                styleFormatCondition1.Appearance.BackColor = CouleurLigne
                styleFormatCondition1.Appearance.Options.UseBackColor = True
                styleFormatCondition1.Appearance.ForeColor = CouleurPolice
                styleFormatCondition1.Appearance.Options.UseFont = True
                styleFormatCondition1.Appearance.Font = New Font(LaPolice, TaillePolice, LeStyle)
                styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression
                styleFormatCondition1.Expression = ColonneCible
                LeGrid.FormatConditions.Add(styleFormatCondition1)
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try


    End Sub


    Public Sub actualiser()

        'actualiser la table t_im_biens
        query = "select * from t_im_biens"
        MySqlDataAdapterr(query, "t_im_biens")

        'actualiser la table t_im_mise_en_service
        query = "select * from t_im_mise_en_service"
        MySqlDataAdapterr(query, "t_im_mise_en_service")


        'actualiser la table t_im_amort_economiq
        query = "select * from t_im_amort_economiq"
        MySqlDataAdapterr(query, "t_im_amort_economiq")

        'actualiser la table t_im_amort_fiscal
        query = "select * from t_im_amort_fiscal"
        MySqlDataAdapterr(query, "t_im_amort_fiscal")

        'actualiser la table t_im_calcul_amort_economiq
        query = "select * from t_im_calcul_amort_economiq"
        MySqlDataAdapterr(query, "t_im_calcul_amort_economiq")

        'actualiser la table  t_im_calcul_amort_fiscal
        query = "select * from  t_im_calcul_amort_fiscal"
        MySqlDataAdapterr(query, " t_im_calcul_amort_fiscal")

        'actualiser la table  t_im_creditbail
        query = "select * from  t_im_creditbail"
        MySqlDataAdapterr(query, " t_im_creditbail")

        'actualiser la table t_im_echeance
        query = "select * from t_im_echeance"
        MySqlDataAdapterr(query, "t_im_echeance")

        'actualiser la table t_im_echeance_cb
        query = "select * from t_im_echeance_cb"
        MySqlDataAdapterr(query, "t_im_echeance_cb")

        'actualiser la table t_im_location
        query = "select * from t_im_location"
        MySqlDataAdapterr(query, "t_im_location")

        'actualiser la table  t_im_mutation
        query = "select * from  t_im_mutation"
        MySqlDataAdapterr(query, " t_im_mutation")

        'actualiser la table  t_im_rachat
        query = "select * from  t_im_rachat"
        MySqlDataAdapterr(query, " t_im_rachat")

        'actualiser la table t_pa_vehicule
        query = "select * from t_pa_vehicule"
        MySqlDataAdapterr(query, "t_pa_vehicule")

        'actualiser la table t_pa_entretien
        query = "select * from t_pa_entretien"
        MySqlDataAdapterr(query, "t_pa_entretien")

        'actualiser la table t_pa_reparation
        query = "select * from t_pa_reparation"
        MySqlDataAdapterr(query, "t_pa_reparation")

        'actualiser la table  t_pa_reparation
        query = "select * from  t_pa_reparation"
        MySqlDataAdapterr(query, " t_pa_reparation")

        'actualiser la table t_pa_vente
        query = "select * from t_pa_vente"
        MySqlDataAdapterr(query, "t_pa_vente")

        'actualiser la table t_pa_visite
        query = "select * from t_pa_visite"
        MySqlDataAdapterr(query, "t_pa_visite")

        'actualiser la table t_pa_visite
        query = "select * from t_pa_mise_au_rebut"
        MySqlDataAdapterr(query, "t_pa_mise_au_rebut")

    End Sub

    Public Sub ColorRowGridAnal(ByRef LeGrid As GridView, ByVal ColonneCible As String, ByVal CouleurLigne As Color, ByVal LaPolice As String, ByVal TaillePolice As Decimal, ByVal LeStyle As FontStyle, ByVal CouleurPolice As Color, Optional ByVal ChangeCoulLg As Boolean = True)

        If (LeGrid.RowCount > 0) Then

            Dim styleFormatCondition1 As New DevExpress.XtraGrid.StyleFormatCondition
            styleFormatCondition1.Appearance.BackColor = CouleurLigne
            styleFormatCondition1.Appearance.ForeColor = CouleurPolice
            styleFormatCondition1.Appearance.Options.UseForeColor = True
            styleFormatCondition1.Appearance.Options.UseBackColor = ChangeCoulLg
            styleFormatCondition1.Appearance.Options.UseFont = True
            styleFormatCondition1.Appearance.Font = New Font(LaPolice, TaillePolice, LeStyle)
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression
            styleFormatCondition1.Expression = ColonneCible
            LeGrid.FormatConditions.Add(styleFormatCondition1)
        End If

    End Sub

    Public Function CodeNouvelleActivite(ByVal CMB As String) As String

        Dim Combo As String = CMB.ToString
        Dim ValRetour As String = Combo & "001"
        Dim Nbre As Decimal = 0
        Dim CodeExist As Boolean = True
        Dim codepart As String = ""

        query = "select CodePartition from T_Partition where  CodeClassePartition=2 and LibelleCourt='" & Combo.ToString & "'"
        codepart = ExecuteScallar(query)

        query = "select COUNT(*) from T_Partition where CodePartitionMere ='" & codepart & "' and CodeClassePartition=5 and CodeProjet='" & ProjetEnCours & "'"
        Nbre = ExecuteScallar(query)


        While CodeExist = True
            If (Nbre < 9) Then
                ValRetour = Combo & "00" & (Nbre + 1).ToString
            ElseIf (Nbre < 99) Then
                ValRetour = Combo & "0" & (Nbre + 1).ToString
            Else
                ValRetour = Combo & (Nbre + 1).ToString
            End If

            CodeExist = False
            query = "select * from T_Partition where LibelleCourt='" & ValRetour & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                CodeExist = True
            Next
            If (CodeExist = True) Then Nbre = Nbre + 1

        End While
        Return ValRetour

    End Function

    Public Sub CodeNouvelleActivite2(ByVal CMB1 As String)
        query = "select libellecourt from T_Partition where  CodeClassePartition=2 and libellepartition='" & CMB1.ToString & "'"
        Dim codelib1 As String = ExecuteScallar(query)
    End Sub

    Public Function CodeNouvelleActivite1(ByVal SousComposante As String) As String

        query = "select LibelleCourt from T_Partition where  CodeClassePartition=2 and libellepartition='" & SousComposante & "'"
        Dim codelib As String = ExecuteScallar(query)

        query = "select CodePartition from T_Partition where  CodeClassePartition=2 and libellepartition='" & SousComposante & "'"
        Dim codepart As String = ExecuteScallar(query)

        Dim Combo As String = codelib.ToString
        Dim ValRetour As String = Combo & "001"
        Dim Nbre As Decimal = 0
        Dim CodeExist As Boolean = True

        query = "select COUNT(*) from T_Partition where CodePartitionMere ='" & codepart & "' and CodeClassePartition=5 and CodeProjet='" & ProjetEnCours & "'"
        Nbre = ExecuteScallar(query)

        While CodeExist = True
            If (Nbre < 9) Then
                ValRetour = Combo & "00" & (Nbre + 1).ToString
            ElseIf (Nbre < 99) Then
                ValRetour = Combo & "0" & (Nbre + 1).ToString
            Else
                ValRetour = Combo & (Nbre + 1).ToString
            End If

            CodeExist = False
            query = "select * from T_Partition where LibelleCourt='" & ValRetour & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                CodeExist = True
            Next
            If (CodeExist = True) Then Nbre = Nbre + 1

        End While

        Return ValRetour
    End Function

    Private Function NomEtAdresseDest(ByVal code As String, Optional ByVal TypeMsg As String = "Mail") As String()

        Dim nomDest As String = ""
        Dim adrDest As String = ""
        Dim telDest As String = ""

        query = "select NomOperateur, PrenOperateur, mailOperateur, TelOperateur from T_Operateur where CodeOperateur='" & code & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            For Each rw In dt.Rows
                nomDest = MettreApost(rw(0).ToString & " " & rw(1).ToString)
                adrDest = rw(2).ToString
                telDest = rw(3).ToString
            Next
        End If
        Return {nomDest, IIf(TypeMsg = "Mail", adrDest, telDest).ToString}

    End Function

    Public Function EnvoiMail(ByVal Dest As String, ByVal Message As String, ByRef Pj As String) As String
        If (Proj_mailHote = "" Or Proj_mailCompte = "") Then
            Return "errMail"
        End If

        ' Mail: Clear (good pratise)
        objMail.Clear()

        ' Mail: From
        objMail.FromName = ProjetEnCours
        objMail.FromAddress = ""  'Mail du Projet

        ' Mail: Subject
        objMail.Subject = "Notification " & ProjetEnCours & " (ClearProject)"

        ' Mail: Priority
        objMail.Priority = objConstants.EMAIL_MESSAGE_PRIORITY_HIGH

        ' Mail: Encoding
        objMail.Encoding = objConstants.EMAIL_MESSAGE_ENCODING_DEFAULT

        ' Mail: Body
        objMail.BodyPlainText = Message

        ' Mail: TO recipient(s)
        Dim adrsDestin() As String = NomEtAdresseDest(Dest)
        objMail.AddTo(adrsDestin(0), adrsDestin(1))

        ' Mail: CC recipient(s)
        If (objMail.LastError <> 0) Then
            Return objMail.LastError.ToString
        End If

        ' Mail: BCC recipient(s)
        If (objMail.LastError <> 0) Then
            Return objMail.LastError.ToString
        End If

        ' Mail: Attachments
        If (objMail.LastError <> 0) Then
            Return objMail.LastError.ToString
        End If

        ' Mail: Add attachment(s)
        Dim Attachs As String = Pj
        objMail.AddAttachment(Attachs)

        ' Mail: If a function failed then BDQUIT
        If (objMail.LastError <> 0) Then
            objSmtpServer.Clear()
            Return objMail.LastError.ToString
        End If

        ' Smtp: Clear (good practise)        
        objSmtpServer.Clear()
        If (Proj_mailSecur = True) Then
            objSmtpServer.SetSecure(Int32.Parse(Proj_mailPort))
        Else
            objSmtpServer.HostPort = Int32.Parse(Proj_mailPort)
        End If

        ' Smtp: Account and Password - if any
        Dim strAccount As String
        Dim strPassword As String

        If (Proj_mailAuthent = True) Then
            strAccount = Proj_mailCompte
            strPassword = Proj_mailPasse
        Else
            strAccount = String.Empty
            strPassword = String.Empty
        End If

        ' Smtp: Connect
        objSmtpServer.Connect(Proj_mailHote, strAccount, strPassword)

        ' Smtp: Send
        If (objSmtpServer.LastError <> 0) Then
            objSmtpServer.Disconnect()
            Return objSmtpServer.LastError.ToString
        End If

        objSmtpServer.Send(objMail)
        If (objSmtpServer.LastError <> 0) Then
            objSmtpServer.Disconnect()
            Return objSmtpServer.LastError.ToString
        End If

        ' Smtp: Disconnect
        objSmtpServer.Disconnect()
        Return adrsDestin(1)

    End Function

    Dim objGsm As AxSms.Gsm = New AxSms.Gsm
    Dim objSmsConstants As AxSms.Constants = New AxSms.Constants

    Public Function EnvoiSms(ByVal code As String, ByVal Message As String) As String
        If (Proj_smsTerminal = "") Then
            Return "errTerminal"
        End If

        objGsm.Open(Proj_smsTerminal, Proj_smsCodePin, Proj_smsVitesse)

        If objGsm.LastError <> 0 Then
            objGsm.Close()
            Return objGsm.LastError.ToString
        End If

        Dim objSms As New AxSms.Message()
        Dim telDestinat As String = Trim(NomEtAdresseDest(code, "Sms")(1).Split("/"c)(0))

        If (telDestinat <> "") Then

            objSms.ToAddress() = telDestinat
            objSms.DataCoding = Proj_smsEncodage
            objSms.Body = Message

            ' Set the SMS properties from the advanced dialog            
            objSms.BodyFormat = objSmsConstants.BODYFORMAT_TEXT

            objSms.ToAddressTON = objSmsConstants.TON_UNKNOWN
            objSms.ToAddressNPI = objSmsConstants.NPI_UNKNOWN

            objSms.RequestDeliveryReport = True
            objSms.HasUdh = False

            Dim strReference As [String] = objGsm.SendSms(objSms, objSmsConstants.MULTIPART_ACCEPT, 0)

            ' There was a problem sending the SMS message return early and don't add the 
            ' message to the listbox.
            If objGsm.LastError <> 0 Then
                ' Close the GSM object.
                objGsm.Close()
                Return objGsm.LastError.ToString
            End If

            objGsm.Close()

        End If

        Return telDestinat

    End Function

    Public Function NomDe(ByVal Kod As String) As String

        Dim NomOp As String = ""
        Dim foncOp As String = ""

        query = "select NomOperateur, PrenOperateur, FonctionOperateur from T_Operateur where CodeOperateur='" & Kod & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            NomOp = MettreApost(rw(0).ToString & " " & rw(1).ToString)
            foncOp = MettreApost(rw(2).ToString)
        Next

        Dim foncServ As String = ""
        query = "select F.CodeFonction from T_Operateur as O, T_grh_travailler as T, T_Fonction as F where O.EMP_ID=T.EMP_ID and T.CodeService=F.RefFonction and T.PosteActu='O' and O.CodeOperateur='" & Kod & "'"
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        For Each rw1 In dt1.Rows
            foncServ = MettreApost(rw1(0).ToString)
        Next

        If (foncServ <> "") Then
            NomOp = NomOp & " (" & foncServ & ")"
        Else
            NomOp = NomOp & " (" & foncOp & ")"
        End If

        Return NomOp

    End Function

    Dim dtDecalage = New DataTable()

    Public Sub DecalerActivites(ByVal Marche As String, ByVal DateDecalage As Date)

        query = "select MethodeMarche, TypeMarche, DescriptionMarche from T_Marche where RefMarche='" & Marche & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            DecalageActivites.GbMarche.Text = rw(0).ToString & "  " & rw(1).ToString
            DecalageActivites.LblMarche.Text = MettreApost(rw(2).ToString)
        Next

        DecalageActivites.TxtDateNotif.Text = DateDecalage.ToShortDateString
        dtDecalage.Columns.Clear()
        dtDecalage.Columns.Add("CodeX", Type.GetType("System.String"))
        dtDecalage.Columns.Add("Ref", Type.GetType("System.String"))
        dtDecalage.Columns.Add("*", Type.GetType("System.Boolean"))
        dtDecalage.Columns.Add("Code", Type.GetType("System.String"))
        dtDecalage.Columns.Add("Libellé", Type.GetType("System.String"))
        dtDecalage.Columns.Add("Date début", Type.GetType("System.String"))
        dtDecalage.Columns.Add("Date fin", Type.GetType("System.String"))
        dtDecalage.Columns.Add("Durée", Type.GetType("System.String"))

        dtDecalage.Rows.Clear()

        Dim cpt As Decimal = 0
        query = "select P.CodePartition, P.LibelleCourt, P.LibellePartition, P.DateDebutPartition, P.DateFinPartition, P.DureePartitionPrevue from T_Partition as P, T_BesoinPartition as B, T_RepartitionParBailleur as R where R.RefMarche='" & Marche & "' and R.RefBesoinPartition=B.RefBesoinPartition and B.CodePartition=P.CodePartition and P.CodeProjet='" & ProjetEnCours & "' order by P.LibelleCourt"
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt1.Rows

            cpt += 1
            Dim drS = dtDecalage.NewRow()
            drS(0) = IIf(CDec(cpt / 2) <> CDec(cpt \ 2), "x", "").ToString
            drS(1) = rw(0).ToString
            drS(2) = False
            drS(3) = rw(1).ToString
            drS(4) = MettreApost(rw(2).ToString)
            drS(5) = CDate(rw(3)).ToString("dd/MM/yyyy")
            drS(6) = CDate(rw(4)).ToString("dd/MM/yyyy")
            drS(7) = Trim(rw(5).ToString.Split(" "c)(0))
            dtDecalage.Rows.Add(drS)

        Next

        DecalageActivites.GridDecalage.DataSource = dtDecalage
        DecalageActivites.ViewDecalage.Columns(0).Visible = False
        DecalageActivites.ViewDecalage.Columns(1).Visible = False
        DecalageActivites.ViewDecalage.Columns(2).Width = 30
        DecalageActivites.ViewDecalage.Columns(3).Width = 50
        DecalageActivites.ViewDecalage.Columns(4).Width = DecalageActivites.GridDecalage.Width - 298
        DecalageActivites.ViewDecalage.Columns(5).Width = 100
        DecalageActivites.ViewDecalage.Columns(6).Width = 100
        DecalageActivites.ViewDecalage.Columns(7).Visible = False
        DecalageActivites.ViewDecalage.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(DecalageActivites.ViewDecalage, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
        DecalageActivites.LblCpteActiv.Text = "Activités sélectionnées : 0 / " & cpt.ToString
        DecalageActivites.RdDebutNotif.Checked = True
        Dialog_form(DecalageActivites)

    End Sub

    Public Function EnvoiMailDirect(ByVal MailDest As String, ByVal Cc As String, ByVal Message As String, ByRef Pj As String) As String
        If (Proj_mailHote = "" Or Proj_mailCompte = "") Then
            Return ""
        End If

        objMail.Clear()

        ' Mail: From
        objMail.FromName = ProjetEnCours
        objMail.FromAddress = Proj_mailCompte 'Mail désigné

        ' Mail: Subject
        objMail.Subject = "Notification " & ProjetEnCours & " (ClearProject)"

        ' Mail: Priority
        objMail.Priority = objConstants.EMAIL_MESSAGE_PRIORITY_HIGH

        ' Mail: Encoding
        objMail.Encoding = objConstants.EMAIL_MESSAGE_ENCODING_DEFAULT

        ' Mail: Body
        objMail.BodyPlainText = Message

        ' Mail: TO recipient(s)
        objMail.AddTo(MailDest)

        ' Mail: CC recipient(s)
        objMail.AddCc(Cc)
        If (objMail.LastError <> 0) Then
            Return ""
        End If

        ' Mail: BCC recipient(s)
        If (objMail.LastError <> 0) Then
            Return ""
        End If

        ' Mail: Add attachment(s)
        Dim Attachs As String = Pj
        objMail.AddAttachment(Attachs)


        ' Mail: If a function failed then BDQUIT
        If (objMail.LastError <> 0) Then
            objSmtpServer.Clear()
            Return ""
        End If

        ' Smtp: Clear (good practise)        
        objSmtpServer.Clear()

        ' Smtp: Set Secure if secure communications is required
        If (Proj_mailSecur = True) Then
            objSmtpServer.SetSecure(Int32.Parse(Proj_mailPort))
        Else
            objSmtpServer.HostPort = Int32.Parse(Proj_mailPort)
        End If


        ' Smtp: Account and Password - if any
        Dim strAccount As String
        Dim strPassword As String

        If (Proj_mailAuthent = True) Then
            strAccount = Proj_mailCompte
            strPassword = Proj_mailPasse
        Else
            strAccount = String.Empty
            strPassword = String.Empty
        End If

        ' Smtp: Connect
        objSmtpServer.Connect(Proj_mailHote, strAccount, strPassword)

        ' Smtp: Send
        If (objSmtpServer.LastError <> 0) Then
            objSmtpServer.Disconnect()
            Return ""
        End If

        objSmtpServer.Send(objMail)

        If (objSmtpServer.LastError <> 0) Then
            objSmtpServer.Disconnect()
            Return ""
        End If
        ' Smtp: Disconnect
        objSmtpServer.Disconnect()

        Return Now.ToShortDateString & " " & Now.ToLongTimeString

    End Function

    Sub RemplirComboVehDispo(ByVal comb As ComboBox)
        Try
            comb.Items.Clear()
            query = "SELECT veh_matricule from t_pa_vehicule WHERE veh_status='Disponible'  "
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw In dt.Rows
                    comb.Items.Add(rw(0).ToString)
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Sub AjoutParam2(ByVal matable As String, ByVal libelle As DevExpress.XtraEditors.TextEdit)
        Try
            'vérification des champ text
            Dim erreur As String = ""

            If libelle.Text = "" Then
                erreur += "- Libelle" & ControlChars.CrLf
            End If
            If erreur = "" Then
                query = "INSERT INTO " & matable & " values (NULL,'" & EnleverApost(libelle.Text) & "')"
                ExecuteNonQuery(query)
                MsgBox("Enregistrement effectué avec succès", MsgBoxStyle.Information, "ClearProject")
                libelle.Text = ""
            Else
                MsgBox("Veuillez remplir ces champs : " & ControlChars.CrLf + erreur, MsgBoxStyle.Exclamation)
            End If

        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Sub ModifParam(ByVal matable As String, ByVal idlibelle As TextBox, ByVal libelle As TextBox)
        Try
            Dim sqlTableCol As String = ""
            Dim temp As String = ""
            Dim colonneTab(3) As String

            'Sélectionne le nom des colonnes de la table 
            query = " Select COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME ='" & matable & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                temp = rw(0).ToString & "," & temp
            Next
            colonneTab = temp.Split(",")

            'execute la requete de mise à jour par rapport au colonne de la table
            query = "UPDATE " & matable & " SET " & colonneTab(0) & " = '" & libelle.Text & "' WHERE " & colonneTab(1) & " = '" & idlibelle.Text & "'"
            ExecuteNonQuery(query)
            MsgBox("Modification effectué avec succès", MsgBoxStyle.Information, "ClearProject")
            libelle.Text = ""
            majParam = False
        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Sub fonctionSupp(ByVal matable As String, ByVal idlibelle As String)
        Try

            Dim sqlTableCol As String = ""
            Dim temp As String = ""
            Dim colonneTab() As String
            Dim nb As Decimal = 0

            'Sélectionne le nom des colonnes de la table 
            query = " Select COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME ='" & matable & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                temp = rw(0).ToString & "," & temp
                nb = nb + 1
            Next
            nb = CInt(nb - 1)
            colonneTab = temp.Split(",")
            query = "DELETE FROM " & matable & " WHERE " & colonneTab(nb) & " = '" & EnleverApost(idlibelle) & "'"
            ExecuteNonQuery(query)
            MsgBox("Suppression effectué avec succès", MsgBoxStyle.Information, "ClearProject")

        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Function TotalEntretienVeh(ByVal idVeh As String)

        Dim montant As String = ""
        query = " SELECT SUM(ent_montant) as Montant FROM t_pa_entretien  WHERE veh_id_vehicule ='" & idVeh & "'  "
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            montant = rw(0).ToString
        Next
        If montant = "" Then
            montant = 0
        End If
        Return montant

    End Function

    Function TotalReparationVeh(ByVal idVeh As String)

        Dim montant As String = ""
        query = " SELECT SUM(montant)as montant FROM t_pa_reparation r, t_pa_panne p WHERE r.pa_id=p.pa_id AND p.veh_id_vehicule ='" & idVeh & "'  "
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            montant = rw(0).ToString
        Next

        If montant = "" Then
            montant = 0
        End If
        Return montant

    End Function

    Function TotalMontant(ByVal Table As String, ByVal Champ As String)

        Dim montant As String = ""
        query = " SELECT SUM(" & Champ & ")as montant FROM " & Table & "  "
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            montant = rw(0).ToString
        Next
        If montant = "" Then
            montant = 0
        End If
        Return montant

    End Function

    Sub RemplirDatagrid2(ByVal matable As String, ByVal critere As String, ByVal mondg As DataGridView)
        Try
            mondg.Rows.Clear()
            Dim nligne = 0
            Dim dgcoltail = mondg.ColumnCount
            query = "select * from " & matable & " order by " & critere
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                'on parcourt les colonnes de notre datagridview
                Dim ncol = 0
                mondg.Rows.Add()
                While ncol < dgcoltail
                    mondg.Rows(nligne).Cells(ncol).Value = MettreApost(rw(ncol).ToString)
                    ncol = ncol + 1
                End While
                nligne = nligne + 1
            Next
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Sub DesactiveSelectDataGrid(ByVal mondg As DataGridView)
        Try
            mondg.CurrentRow.Cells(1).Selected = False
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Sub loadCombo(ByVal comb As ComboBox, ByVal matable As String, ByVal col As String)
        Try
            comb.Items.Clear()
            query = "select * from " & matable & " WHERE codeProjet='" & ProjetEnCours & "'  "
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw In dt.Rows
                    If (rw(col).ToString <> "") Then
                        comb.Items.Add(rw(col).ToString)
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Sub loadCombo2(ByVal comb As DevExpress.XtraEditors.ComboBoxEdit, ByVal matable As String, ByVal col As String)
        Try
            comb.Properties.Items.Clear()
            query = "select * from " & matable & " WHERE codeProjet='" & ProjetEnCours & "'  "
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw In dt.Rows
                    If (rw(col).ToString <> "") Then
                        comb.Properties.Items.Add(rw(col).ToString)
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Public Function CTemps(ByVal Temps1, ByVal Temps2)
        On Error GoTo Erreur
        Dim h1
        Dim h2
        Dim m1
        Dim m2
        Dim s1
        Dim s2
        h1 = Hour(Temps1)
        h2 = Hour(Temps2)
        m1 = Minute(Temps1)
        m2 = Minute(Temps2)
        s1 = Second(Temps1)
        s2 = Second(Temps2)
        If h1 > h2 Then
            CTemps = 1
        End If
        If h2 > h1 Then
            CTemps = 2
        End If
        If h1 = h2 And m1 > m2 Then
            CTemps = 1
        End If
        If h1 = h2 And m2 > m1 Then
            CTemps = 2
        End If
        If h1 = h2 And m1 = m2 And s1 > s2 Then
            CTemps = 1
        End If
        If h1 = h2 And m1 = m2 And s2 > s1 Then
            CTemps = 2
        End If
Erreur:
        Exit Function
    End Function

    Sub RemplirComboZone(ByVal comb As ComboBox)

        Try
            comb.Items.Clear()
            query = "SELECT z.codeZone, z.AbregeZone, z.libelleZone from t_zoneGeo z " +
                " WHERE z.codeZoneMere = 0 "
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw In dt.Rows
                    comb.Items.Add(rw(1).ToString & " - " & MettreApost(rw(2).ToString))

                    query = "SELECT z.codeZone, z.AbregeZone, z.libelleZone from t_zoneGeo z WHERE z.codeZoneMere = '" & rw(0).ToString & "' "
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 In dt1.Rows
                        comb.Items.Add(rw1(1).ToString & " - " & MettreApost(rw1(2).ToString))
                    Next
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try

    End Sub
    Sub RemplirComboPartition(ByVal comb As ComboBox, ByVal tCodeLibCourt As Decimal)
        Try
            comb.Items.Clear()
            query = "select p.libelleCourt, p.LibellePartition from t_partition p WHERE { fn LENGTH(LibelleCourt) } = '" & tCodeLibCourt.ToString & "' AND p.codeProjet = '" & ProjetEnCours & "' "
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw In dt.Rows
                    comb.Items.Add(rw(0).ToString & " - " & MettreApost(rw(1).ToString))
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Sub RemplirComboPartition1(ByVal comb As DevExpress.XtraEditors.ComboBoxEdit, ByVal tCodeLibCourt As Decimal)

        Try
            comb.Properties.Items.Clear()
            query = "select p.libelleCourt, p.LibellePartition from t_partition p WHERE CodeClassePartition = '" & tCodeLibCourt.ToString & "' AND p.codeProjet = '" & ProjetEnCours & "' Order by p.libelleCourt"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw In dt.Rows
                    comb.Properties.Items.Add(rw(0).ToString & " - " & MettreApost(rw(1).ToString))
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Sub RemplirComboPartition2(ByVal comb As DevExpress.XtraEditors.ComboBoxEdit)

        Try
            comb.Properties.Items.Clear()
            query = "select CodeService, NomService from t_service WHERE codeProjet = '" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                comb.Properties.Items.Add(rw(0).ToString & " - " & MettreApost(rw(1).ToString))
            Next
        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Sub RemplirComboPartition3(ByVal comb As DevExpress.XtraEditors.ComboBoxEdit)

        Try
            comb.Properties.Items.Clear()
            query = "select CodeZone, LibelleZone from t_zonegeo"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                comb.Properties.Items.Add(rw(0).ToString & " - " & MettreApost(rw(1).ToString))
            Next
        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Sub RemplirComboPartition4(ByVal comb As DevExpress.XtraEditors.ComboBoxEdit)

        Try
            comb.Properties.Items.Clear()
            query = "select CodeCateg, LibelleCateg from t_categoriedepense d, t_convention c, t_bailleur b where b.CodeBailleur=c.CodeBailleur and c.CodeConvention=d.CodeConvention and b.CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                comb.Properties.Items.Add(rw(0).ToString & " - " & MettreApost(rw(1).ToString))
            Next
        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Sub RemplirComboConvBail(ByVal comb As ComboBox)
        Try
            comb.Items.Clear()
            query = "select c.codeConvention, b.InitialeBailleur from t_convention c, t_bailleur b " +
                "WHERE c.codeBailleur = b.codeBailleur AND b.codeProjet = '" & ProjetEnCours & "' "
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw In dt.Rows
                    comb.Items.Add(rw(0).ToString & " -/- " & rw(1).ToString)
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Sub RemplirComboBail(ByVal comb As ComboBox)

        Try
            comb.Items.Clear()
            query = "select distinct b.InitialeBailleur, b.NomBailleur from t_convention c, t_bailleur b " +
                "WHERE c.codeBailleur = b.codeBailleur AND b.codeProjet = '" & ProjetEnCours & "' "
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw In dt.Rows
                    comb.Items.Add(rw(0).ToString & " - " & MettreApost(rw(1).ToString))
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Sub RemplirComboBail1(ByVal comb As DevExpress.XtraEditors.ComboBoxEdit)

        Try
            comb.Properties.Items.Clear()
            query = "select distinct b.InitialeBailleur, b.NomBailleur from t_convention c, t_bailleur b " +
                "WHERE c.codeBailleur = b.codeBailleur AND b.codeProjet = '" & ProjetEnCours & "' "
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw In dt.Rows
                    comb.Properties.Items.Add(rw(0).ToString & " - " & MettreApost(rw(1).ToString))
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Public Sub EffacerTexBox(ByVal Pan As DevExpress.XtraEditors.GroupControl)
        For Each TB As Control In Pan.Controls
            If TypeOf (TB) Is TextBox Then
                TB.Text = ""
            End If
        Next
        For Each CB As Control In Pan.Controls
            If TypeOf (CB) Is ComboBox Then
                CB.Text = ""
            End If
        Next
        For Each RB As Control In Pan.Controls
            If TypeOf (RB) Is RichTextBox Then
                RB.Text = ""
            End If
        Next

        For Each TB As Control In Pan.Controls
            If TypeOf (TB) Is DevExpress.XtraEditors.TextEdit Then
                TB.Text = ""
            End If
        Next
        For Each CBE As Control In Pan.Controls
            If TypeOf (CBE) Is DevExpress.XtraEditors.ComboBoxEdit Then
                CBE.Text = ""
            End If
        Next
        For Each CB As Control In Pan.Controls
            If TypeOf (CB) Is ComboBox Then
                CB.Text = ""
            End If
        Next
        For Each DT As Control In Pan.Controls
            If TypeOf (DT) Is DateTimePicker Then
                DT.Text = ""
            End If
        Next

        For Each RT As Control In Pan.Controls
            If TypeOf (RT) Is RichTextBox Then
                RT.Text = ""
            End If
        Next
        For Each LB As Control In Pan.Controls
            If TypeOf (LB) Is ListBox Then
                LB.Text = ""
            End If
        Next
    End Sub

    Sub ActiverChamps(ByVal Pan As DevExpress.XtraEditors.GroupControl)
        For Each TB As Control In Pan.Controls
            If TypeOf (TB) Is TextBox Then
                TB.Enabled = True
            End If
        Next
        For Each CB As Control In Pan.Controls
            If TypeOf (CB) Is ComboBox Then
                CB.Enabled = True
            End If
        Next
        For Each DT As Control In Pan.Controls
            If TypeOf (DT) Is DateTimePicker Then
                DT.Enabled = True
            End If
        Next
        For Each SB As Control In Pan.Controls
            If TypeOf (SB) Is DevExpress.XtraEditors.SimpleButton Then
                SB.Enabled = True
            End If
        Next
    End Sub

    Public Function SearchFile(ByVal MonDossier As String, ByVal MonFichier As String) As String

        Dim sFiles() As String
        Dim nbFile As Decimal
        Dim tail = MonDossier.Length
        Dim Lefichier = ""
        sFiles = Directory.GetFiles(MonDossier)
        Dim p As Decimal = 0
        For nbFile = 0 To sFiles.GetUpperBound(0)
            Dim ext As String = System.IO.Path.GetExtension(sFiles(nbFile))
            If File.Exists(MonDossier & "\" & MonFichier + ext) Then
                Lefichier = MonDossier & "\" & MonFichier + ext
            End If
        Next
        Return Lefichier

    End Function

    Function ExtraireNom(ByVal chemin As String) As String
        If chemin <> "" And chemin <> " " Then
            Dim str1 As String() = chemin.Split("\"c)
            Dim Nom_fichier As String = ""
            Nom_fichier = str1(str1.Length - 1)
            Return Nom_fichier
        Else
            Return ""
        End If
    End Function

    Public Function SplitFileName(file As String) As String()
        Try
            Dim ext As String = New FileInfo(file).Extension
            Dim paths As String() = file.Split("\"c)
            Dim FileName As String = Mid(paths(paths.Length - 1), 1, paths(paths.Length - 1).Length - ext.Length)
            Return {FileName, ext}
        Catch ex As Exception
            Return {"", ""}
        End Try
    End Function
    Function AnnulerFichier(ByVal monbt As DevExpress.XtraEditors.SimpleButton, ByVal monfichier As String, ByVal monlab As Label)
        monbt.Text = "Charger"
        monbt.ForeColor = Color.Black
        monfichier = vbNullChar
        monlab.Enabled = False
        Return monfichier
    End Function

    Sub RemplirCombo(ByVal comb As ComboBox, ByVal matable As String, ByVal col As String)
        Try
            comb.Items.Clear()
            query = "select distinct(" & col & ") from " & matable & " order by " & col
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw As DataRow In dt.Rows
                    comb.Items.Add(MettreApost(rw(col).ToString))
                Next
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Sub RemplirCombo6(ByVal comb As DevExpress.XtraEditors.ComboBoxEdit, ByVal matable As String, ByVal col As String, ByVal col1 As String)
        Try
            comb.Properties.Items.Clear()
            query = "select distinct(" & col & ") from " & matable & " order by " & col1
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw In dt.Rows
                    comb.Properties.Items.Add(MettreApost(rw(col).ToString))
                Next
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Sub RemplirDatagrid(ByVal marekete As String, ByVal mondg As DataGridView)
        Try
            Dim nligne = 0
            Dim dgcoltail = mondg.ColumnCount
            Dim dt As DataTable = ExcecuteSelectQuery(marekete)
            For Each rw In dt.Rows
                'on parcourt les colonnes de notre datagridview
                Dim ncol = 1
                mondg.Rows.Add()
                mondg.Rows(nligne).Cells(0).Value = nligne + 1
                While ncol < dgcoltail
                    Dim Mavaleur = rw(ncol - 1)
                    If (IsDate(Mavaleur)) Then
                        mondg.Rows(nligne).Cells(ncol).Value = Mid(Mavaleur, 1, 10)
                    Else
                        mondg.Rows(nligne).Cells(ncol).Value = MettreApost(Mavaleur.ToString)
                    End If
                    'mondg.Rows(nligne).Cells(ncol).Value = rw(ncol - 1)
                    ncol = ncol + 1
                End While
                nligne = nligne + 1
            Next
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Sub RemplirComboDevX(ByVal comb As DevExpress.XtraEditors.ComboBoxEdit, ByVal matable As String, ByVal col As String)
        Try
            comb.Properties.Items.Clear()
            query = "select * from " & matable & " order by " & col
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw As DataRow In dt.Rows
                    comb.Properties.Items.Add(rw(col).ToString)
                Next
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Public Function SeardID(ByVal matable As String, ByVal champsRetour As String, ByVal critere As String, ByVal valcritere As String)
        Dim valeurRetour = ""
        query = "select " & champsRetour & " from " & matable & " where " & critere & " = '" & valcritere & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                valeurRetour = rw(0).ToString
            Next
        Else
            valeurRetour = Nothing
        End If
        Return valeurRetour
    End Function

    Public Function SeardID5(ByVal matable As String, ByVal champsRetour As String, ByVal critere As String, ByVal valcritere As String)
        query = "select " & champsRetour & " from " & matable & " where " & critere & " = '" & valcritere & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        Dim valeurRetour = ""
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                valeurRetour = rw(0).ToString
            Next
        Else
            valeurRetour = Nothing
        End If
        Return valeurRetour
    End Function

    Sub RemplirLixbox(ByVal lboxLIB As ListBox, ByVal rekete As String)
        Try

            Dim lg = 0
            lboxLIB.Items.Clear()
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                lboxLIB.Items.Add(rw(0).ToString & "-" & rw(1).ToString & " " & rw(2).ToString)
                lg = lg + 1
            Next

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub
    Sub AffecterEmploye(ByVal lbEmp As ListBox, ByVal lbEmpID As ListBox, ByVal lbEmpAdd As ListBox, ByVal lbEmpAddID As ListBox)
        Try
            If lbEmp.Items.Count = 0 Then
                FailMsg("Erreur: aucun employé sélectionné")
            Else
                Dim tail = lbEmpAdd.Items.Count
                Dim i = 0
                Dim verif As Boolean = False
                For i = 0 To tail - 1
                    If lbEmpAdd.Items(i).ToString = lbEmp.SelectedItem Then
                        verif = True
                    End If
                Next
                If verif = True Then
                    FailMsg("Erreur: cet employé a déjà été sélectionné")
                Else
                    lbEmpAdd.Items.Insert(tail, lbEmp.SelectedItem)
                    Dim ind = lbEmpAddID.Items.Count
                    lbEmpAddID.Items.Insert(ind, lbEmpID.Items(lbEmp.SelectedIndex))
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Sub DesaffecterEmploye(ByVal lbEmpAdd As ListBox, ByVal lbEmpAddID As ListBox)
        Try
            If lbEmpAddID.Items.Count = 0 Then
                FailMsg("Erreur: aucun employé sélectionné")
            Else
                lbEmpAddID.Items.RemoveAt(lbEmpAdd.SelectedIndex)
                lbEmpAdd.Items.RemoveAt(lbEmpAdd.SelectedIndex)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub DeleteRecords(ByVal matable As String, ByVal Critere As String, ByVal ValCritere As String)
        Try
            query = "delete from " & matable & " where " & Critere & "='" & ValCritere & "'"
            ExecuteNonQuery(query)
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub

    Sub DeleteRecords2(ByVal matable As String, ByVal Critere As String, ByVal ValCritere As String)
        Try
            query = "delete from " & matable & " where " & Critere & "='" & ValCritere & "'"
            ExecuteNonQuery(query)
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub

    Sub DeleteRecords3(ByVal matable As String, ByVal Critere As String, ByVal ValCritere As String, ByVal Critere1 As String, ByVal ValCritere1 As String)
        Try
            query = "delete from " & matable & " where " & Critere & "='" & ValCritere & "' and " & Critere1 & "='" & ValCritere1 & "'"
            ExecuteNonQuery(query)
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub
    Public Function NbreJours(ByVal datedeb As Date, ByVal datefin As Date) As Decimal
        Dim NbJrs As Decimal = CDec(DateDiff(DateInterval.DayOfYear, datedeb, CDate(datefin)))
        Return NbJrs
    End Function

    Public Function MoisEnLettre(ByVal MoisId As Decimal) As String
        Dim Mois As String = ""
        While Mois = ""
            If MoisId = 1 Then
                Mois = "Janvier"
            End If
            If MoisId = 2 Then
                Mois = "Février"
            End If
            If MoisId = 3 Then
                Mois = "Mars"
            End If
            If MoisId = 4 Then
                Mois = "Avril"
            End If
            If MoisId = 5 Then
                Mois = "Mai"
            End If
            If MoisId = 6 Then
                Mois = "Juin"
            End If
            If MoisId = 7 Then
                Mois = "Juillet"
            End If
            If MoisId = 8 Then
                Mois = "Août"
            End If
            If MoisId = 9 Then
                Mois = "Septembre"
            End If
            If MoisId = 10 Then
                Mois = "Octobre"
            End If
            If MoisId = 11 Then
                Mois = "Novembre"
            End If
            If MoisId = 12 Then
                Mois = "Décembre"
            End If
            If MoisId > 12 Then
                MoisId = MoisId - 12
            End If
        End While
        Return Mois
    End Function

    Public Function Trimestre(ByVal ladate As Date)
        Dim month = ladate.Month.ToString

        Dim mois = "00"
        If month.Length = 1 Then
            mois = "0" & month
        Else
            mois = month
        End If

        Dim Trimes = ""
        If month <= 3 Then
            Trimes = "1T"
        Else
            If month <= 6 Then
                Trimes = "2T"
            Else
                If month <= 9 Then
                    Trimes = "3T"
                Else
                    If month <= 12 Then
                        Trimes = "4T"
                    End If
                End If
            End If
        End If
        Dim Val_retour = mois.ToString + Trimes.ToString
        Return Val_retour
    End Function
    Sub RemplirComboConv(ByVal comb As ComboBox)
        Try
            comb.Items.Clear()

            query = "select c.codeConvention from t_convention c, t_bailleur b " +
                "WHERE c.codeBailleur = b.codeBailleur AND b.codeProjet = '" & ProjetEnCours & "' "
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw In dt.Rows
                    comb.Items.Add(rw(0).ToString)
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Sub RemplirComboConv1(ByVal comb As DevExpress.XtraEditors.ComboBoxEdit)
        Try
            comb.Properties.Items.Clear()
            query = "select c.codeConvention from t_convention c, t_bailleur b " +
                "WHERE c.codeBailleur = b.codeBailleur AND b.codeProjet = '" & ProjetEnCours & "' "
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw In dt.Rows
                    comb.Properties.Items.Add(rw(0).ToString)
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Sub ViewDocument(ByVal document As String)
        Try
            If document = "" Or document = " " Then
                FailMsg("Erreur: fichier introuvable")
            Else
                Visionneuse.Close()
                Visionneuse.pbImpDoc.Enabled = True
                Dim ext = New System.IO.FileInfo(document).Extension
                If ext = ".docx" Or ext = ".doc" Then
                    Visionneuse.Visioword.LoadDocument(document)
                    Visionneuse.Visioword.Visible = True
                    Visionneuse.VisionPdf.Visible = False
                End If
                If ext = ".pdf" Then
                    Visionneuse.pbImpDoc.Enabled = False
                    Visionneuse.VisionPdf.Navigate(document)
                    Visionneuse.VisionPdf.Visible = True
                    Visionneuse.Visioword.Visible = False
                End If
                If ext = ".jpg" Or ext = ".JPG" Or ext = ".png" Or ext = ".jpeg" Or ext = ".JPEG" Then
                    Dim img = Image.FromFile(document).GetThumbnailImage(600, 800, Nothing, IntPtr.Zero)
                    Visionneuse.Visioword.Document.AppendImage(img)
                    Visionneuse.Visioword.Visible = True
                    Visionneuse.VisionPdf.Visible = False
                End If
                Disposer_form(Visionneuse)
            End If
        Catch ex As Exception
            FailMsg("Erreur: pilote introuvable")
        End Try
    End Sub

    Public Sub EnregistrerProjet()
        'on instancie l'objet DataSet avec de l'utiliser
        Dim DatSet = New DataSet
        query = "select * from T_Projet"
        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)
        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd)
        DatAdapt.Fill(DatSet, "T_Projet")
        Dim DatTable = DatSet.Tables("T_Projet")
        Dim DatRow = DatSet.Tables("T_Projet").NewRow()

        DatRow("CodeProjet") = NouveauProjetClear.TxtAbrege.Text
        DatRow("NomProjet") = EnleverApost(NouveauProjetClear.TxtIntitule.Text)
        DatRow("AdresseProjet") = EnleverApost(NouveauProjetClear.TxtAdresse.Text)
        DatRow("TelProjet") = NouveauProjetClear.TxtTelCoordo.Text
        DatRow("FaxProjet") = NouveauProjetClear.TxtFaxCoordo.Text
        DatRow("MailProjet") = NouveauProjetClear.TxtMailCoordo.Text
        DatRow("SiteWebProjet") = NouveauProjetClear.TxtSiteWeb.Text
        DatRow("DateDebutProjetMO") = NouveauProjetClear.TxtDateDebutMO.Text
        DatRow("DateDebutProjetMV") = NouveauProjetClear.TxtDateDebutMV.Text
        DatRow("IdentifiantProjet") = IIf(NouveauProjetClear.TxtIdentifiant.Text <> "", NouveauProjetClear.TxtIdentifiant.Text, "001").ToString
        DatRow("BoitePostaleProjet") = EnleverApost(NouveauProjetClear.TxtBp.Text)
        DatRow("MinistereTutelle") = EnleverApost(NouveauProjetClear.TxtMinistere.Text)
        DatRow("PaysProjet") = EnleverApost(NouveauProjetClear.ComboPays.Text)
        DatRow("DateFinProjetMO") = NouveauProjetClear.TxtDateFinMO.Text
        DatRow("DateFinProjetMV") = NouveauProjetClear.TxtDateFinMV.Text
        DatRow("EtatProjet") = "En cours"
        If (NouveauProjetClear.TxtChemin.Text <> "" And NouveauProjetClear.TxtExt.Text <> "") Then
            DatRow("LogoProjet") = NouveauProjetClear.TxtAbrege.Text & "." & NouveauProjetClear.TxtExt.Text
            DatRow("LogoImage") = File.ReadAllBytes(NouveauProjetClear.TxtChemin.Text)
        Else
            DatRow("LogoProjet") = "vide.png"
            DatRow("LogoImage") = File.ReadAllBytes(NouveauProjetClear.TxtRepertoire.Text & "\LogoProjet\vide.png")
        End If

        DatSet.Tables("T_Projet").Rows.Add(DatRow)
        Dim CmdBuilder = New MySql.Data.MySqlClient.MySqlCommandBuilder(DatAdapt)
        DatAdapt.Update(DatSet, "T_Projet")
        DatSet.Clear()

        ' Table Paramètre projet ******
        DatSet = New DataSet
        query = "select * from T_ParamTechProjet"
        Dim Cmd1 As MySqlCommand = New MySqlCommand(query, sqlconn)
        DatAdapt = New MySqlDataAdapter(Cmd1)
        DatAdapt.Fill(DatSet, "T_ParamTechProjet")
        DatTable = DatSet.Tables("T_ParamTechProjet")
        DatRow = DatSet.Tables("T_ParamTechProjet").NewRow()

        DatRow("RacineDocument") = NouveauProjetClear.TxtRepertoire.Text
        DatRow("CodeProjet") = NouveauProjetClear.TxtAbrege.Text

        DatRow("UniteRepartitionBudget") = ""
        DatRow("MethodeMarcheAuto") = ""
        DatRow("NbreValideMarche") = "0"
        DatRow("Serv_Nom") = ""
        DatRow("Mail_Account") = ""
        DatRow("Mail_PassWord") = ""
        DatRow("Mail_Format") = ""
        DatRow("Mail_CharSet") = ""
        DatRow("Sms_Terminal") = ""
        DatRow("Sms_Isdn") = ""
        DatRow("Sms_Imei") = ""
        DatRow("Sms_Modele") = ""
        DatRow("Sms_Vitesse") = ""
        DatRow("Sms_Pin") = ""
        DatRow("Sms_Encodage") = ""

        DatSet.Tables("T_ParamTechProjet").Rows.Add(DatRow)
        CmdBuilder = New MySql.Data.MySqlClient.MySqlCommandBuilder(DatAdapt)
        DatAdapt.Update(DatSet, "T_ParamTechProjet")
        DatSet.Clear()

        ' Table Utils projet ******
        DatSet = New DataSet
        query = "select * from T_GroupUtils where codeprojet='" & ProjetEnCours & "'"
        Dim Cmd2 As MySqlCommand = New MySqlCommand(query, sqlconn)
        DatAdapt = New MySqlDataAdapter(Cmd2)
        DatAdapt.Fill(DatSet, "T_GroupUtils")
        DatTable = DatSet.Tables("T_GroupUtils")

        DatRow = DatSet.Tables("T_GroupUtils").NewRow()
        DatRow("CodeGroup") = "Administrateur"
        DatRow("AttributGroup") = "&Admin&"
        DatRow("CodeProjet") = NouveauProjetClear.TxtAbrege.Text
        DatSet.Tables("T_GroupUtils").Rows.Add(DatRow)

        DatRow = DatSet.Tables("T_GroupUtils").NewRow()
        DatRow("CodeGroup") = "Niveau0"
        DatRow("AttributGroup") = "&"
        DatRow("CodeProjet") = NouveauProjetClear.TxtAbrege.Text
        DatSet.Tables("T_GroupUtils").Rows.Add(DatRow)

        CmdBuilder = New MySql.Data.MySqlClient.MySqlCommandBuilder(DatAdapt)
        DatAdapt.Update(DatSet, "T_GroupUtils")
        DatSet.Clear()

        BDQUIT(sqlconn)
    End Sub

    Sub RemplirCombo2(ByVal comb As DevExpress.XtraEditors.ComboBoxEdit, ByVal matable As String, ByVal col As String)
        Try
            comb.Properties.Items.Clear()
            query = "select * from " & matable & " order by " & col
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            If dt0.Rows.Count > 0 Then
                For Each rw As DataRow In dt0.Rows
                    comb.Properties.Items.Add(MettreApost(rw(col).ToString))
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Function AnnulerFichier2(ByVal List As ListBox, ByVal monfichier As String, ByVal monlab As Label)
        List.Items.Clear()
        monfichier = vbNullChar
        monlab.Enabled = False
        Return monfichier
    End Function

    Function AnnulerFichier3(ByVal List As DevExpress.XtraEditors.ListBoxControl, ByVal monfichier As String, ByVal monlab As Label)
        List.Items.Clear()
        monfichier = vbNullChar
        monlab.Enabled = False
        Return monfichier
    End Function

    Sub remplirDG(ByVal ColonneMatable As String, ByVal ColonneMatable2 As String, ByVal ColonneMatable3 As String, ByVal matable As String, ByVal matable2 As String, ByVal ChampMatable As String, ByVal ChampMatable2 As String, ByVal dg As DataGridView)
        dg.Rows.Clear()
        query = "select " & ColonneMatable & "," & ColonneMatable2 & "," & ColonneMatable3 & " from " & matable & "," & matable2 & " WHERE " & ChampMatable & "=" & ChampMatable2 & ""
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            dg.Rows.Add(rw(0).ToString, rw(1).ToString, MettreApost(rw(2).ToString))
        Next
    End Sub

    Sub remplirDG3(ByVal ColonneMatable As String, ByVal ColonneMatable2 As String, ByVal matable As String, ByVal dg As DataGridView)
        dg.Rows.Clear()
        query = "select " & ColonneMatable & "," & ColonneMatable2 & " from " & matable & ""
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            dg.Rows.Add(rw(0).ToString, MettreApost(rw(1).ToString))
        Next
    End Sub

    Sub remplirDG2(ByVal ColonneMatable As String, ByVal ColonneMatable2 As String, ByVal ColonneMatable3 As String, ByVal ColonneMatable4 As String, ByVal matable As String, ByVal matable2 As String, ByVal dg As DataGridView, Optional ByVal Ordre1 As String = "", Optional ByVal Ordre2 As String = "")
        dg.Rows.Clear()
        query = "Select " & ColonneMatable & ", " & ColonneMatable2 & " from " & matable & "" & IIf(Ordre1 <> "", " order by " & Ordre1 & "", "").ToString
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            dg.Rows.Add(rw(0).ToString, MettreApost(rw(1).ToString))
            query = "select " & ColonneMatable3 & ", " & ColonneMatable4 & " from " & matable2 & " where " & ColonneMatable3 & " like '" & rw(0).ToString & "%'" & IIf(Ordre2 <> "", " order by " & Ordre2 & "", "").ToString
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw1 In dt1.Rows
                dg.Rows.Add(rw1(0).ToString, MettreApost(rw1(1).ToString))
            Next
        Next
    End Sub

    Sub remplirDataGridCateg(ByVal mondg As DevExpress.XtraGrid.GridControl, ByVal grid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal requete As String)
        Try
            dtcateg.Columns.Clear()
            dtcateg.Columns.Add("Niveau", Type.GetType("System.String"))
            dtcateg.Columns.Add("Numéro Catégorie", Type.GetType("System.String"))
            dtcateg.Columns.Add("Code Convention", Type.GetType("System.String"))
            dtcateg.Columns.Add("Libelle Catégorie", Type.GetType("System.String"))
            dtcateg.Columns.Add("Montant Catégorie", Type.GetType("System.String"))
            dtcateg.Rows.Clear()

            Dim dt As DataTable = ExcecuteSelectQuery(requete)
            For Each rw In dt.Rows
                Dim drS = dtcateg.NewRow()
                drS(0) = rw(5).ToString
                drS(1) = rw(1).ToString
                drS(2) = rw(2).ToString
                drS(3) = rw(3).ToString
                drS(4) = rw(4).ToString
                dtcateg.Rows.Add(drS)
            Next

            mondg.DataSource = dtcateg
            grid.OptionsView.ColumnAutoWidth = True
            grid.OptionsBehavior.AutoExpandAllGroups = True
            grid.VertScrollVisibility = True
            grid.HorzScrollVisibility = True
            grid.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try
    End Sub


    Sub MissId_automatique(ByVal montext As TextBox)
        Try
            query = "select MIS_ID from  t_grh_mission"
            MySqlDataAdapterr(query, " t_grh_mission")

            If rownum > dt.Rows.Count - 1 Then
                montext.Text = "MIS1001"
            Else
                query = "select MAX(MIS_ID) from  t_grh_mission"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw In dt.Rows

                    Dim strNombre As String
                    Dim SpaceIndex As Decimal
                    Dim caractere As String
                    Dim intNombre, nbr As Decimal

                    strNombre = ""
                    For SpaceIndex = 1 To Len(rw(0).ToString)
                        caractere = Mid$(rw(0).ToString, SpaceIndex, 1)
                        If caractere >= "0" And caractere <= "9" Then
                            strNombre = strNombre + caractere
                        End If
                    Next
                    intNombre = CInt(strNombre)
                    nbr = intNombre + Int(1)
                    montext.Text = "MIS" & "" & nbr.ToString

                Next
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Public Sub EffacerTexBox10(ByVal Pan As GroupBox)
        For Each TB As Control In Pan.Controls
            If TypeOf (TB) Is DevExpress.XtraEditors.TextEdit Then
                TB.Text = ""
            End If
        Next
        For Each CBE As Control In Pan.Controls
            If TypeOf (CBE) Is DevExpress.XtraEditors.ComboBoxEdit Then
                CBE.Text = ""
            End If
        Next
        For Each CB As Control In Pan.Controls
            If TypeOf (CB) Is ComboBox Then
                CB.Text = ""
            End If
        Next
        For Each DT As Control In Pan.Controls
            If TypeOf (DT) Is DateTimePicker Then
                DT.Text = ""
            End If
        Next

        For Each RT As Control In Pan.Controls
            If TypeOf (RT) Is RichTextBox Then
                RT.Text = ""
            End If
        Next
        For Each LB As Control In Pan.Controls
            If TypeOf (LB) Is ListBox Then
                LB.Text = ""
            End If
        Next
    End Sub

    Public Sub EffacerTexBox3(ByVal Pan As Panel)
        For Each TB As Control In Pan.Controls
            If TypeOf (TB) Is DevExpress.XtraEditors.TextEdit Then
                TB.Text = ""
            End If
        Next
        For Each SB As Control In Pan.Controls
            If TypeOf (SB) Is DevExpress.XtraEditors.SimpleButton Then
                SB.Text = ""
            End If
        Next
        For Each CBE As Control In Pan.Controls
            If TypeOf (CBE) Is DevExpress.XtraEditors.ComboBoxEdit Then
                CBE.Text = ""
            End If
        Next
        For Each CB As Control In Pan.Controls
            If TypeOf (CB) Is ComboBox Then
                CB.Text = ""
            End If
        Next
        For Each DT As Control In Pan.Controls
            If TypeOf (DT) Is DateTimePicker Then
                DT.Text = ""
            End If
        Next

        For Each RT As Control In Pan.Controls
            If TypeOf (RT) Is RichTextBox Then
                RT.Text = ""
            End If
        Next
        For Each LB As Control In Pan.Controls
            If TypeOf (LB) Is ListBox Then
                LB.Text = ""
            End If
        Next
    End Sub

    Public Sub EffacerTexBox4(ByVal Pan As DevExpress.XtraEditors.PanelControl)
        If Pan.Controls.Count > 0 Then
            For Each TB As Control In Pan.Controls
                If TypeOf (TB) Is DevExpress.XtraEditors.TextEdit Then
                    TB.Text = ""
                End If
            Next
            For Each CBE As Control In Pan.Controls
                If TypeOf (CBE) Is DevExpress.XtraEditors.ComboBoxEdit Then
                    CBE.Text = ""
                End If
            Next
            For Each CB As Control In Pan.Controls
                If TypeOf (CB) Is ComboBox Then
                    CB.Text = ""
                End If
            Next
            For Each DT As Control In Pan.Controls
                If TypeOf (DT) Is DateTimePicker Then
                    DT.Text = ""
                End If
            Next

            For Each RT As Control In Pan.Controls
                If TypeOf (RT) Is RichTextBox Then
                    RT.Text = ""
                End If
            Next
            For Each LB As Control In Pan.Controls
                If TypeOf (LB) Is ListBox Then
                    LB.Text = ""
                End If
            Next
        End If
    End Sub

    Public Sub EffacerTexBox6(ByVal Pan As DevExpress.XtraEditors.PanelControl)
        For Each TB As Control In Pan.Controls
            If TypeOf (TB) Is DevExpress.XtraEditors.TextEdit Then
                TB.Text = ""
            End If
        Next

        For Each CBE As Control In Pan.Controls
            If TypeOf (CBE) Is DevExpress.XtraEditors.ComboBoxEdit Then
                CBE.Text = ""
            End If
        Next
        For Each CB As Control In Pan.Controls
            If TypeOf (CB) Is ComboBox Then
                CB.Text = ""
            End If
        Next
        For Each DT As Control In Pan.Controls
            If TypeOf (DT) Is DateTimePicker Then
                DT.Text = ""
            End If
        Next

        For Each RT As Control In Pan.Controls
            If TypeOf (RT) Is RichTextBox Then
                RT.Text = ""
            End If
        Next
        For Each LB As Control In Pan.Controls
            If TypeOf (LB) Is ListBox Then
                LB.Text = ""
            End If
        Next
    End Sub

    Public Sub EffacerTexBox5(ByVal Pan As DevExpress.XtraEditors.PanelControl)
        For Each TB As Control In Pan.Controls
            If TypeOf (TB) Is DevExpress.XtraEditors.TextEdit Then
                TB.Text = ""
            End If
        Next
        For Each SB As Control In Pan.Controls
            If TypeOf (SB) Is DevExpress.XtraEditors.SimpleButton Then
                SB.Text = ""
            End If
        Next
        For Each CBE As Control In Pan.Controls
            If TypeOf (CBE) Is DevExpress.XtraEditors.ComboBoxEdit Then
                CBE.Text = ""
            End If
        Next
        For Each DT As Control In Pan.Controls
            If TypeOf (DT) Is DateTimePicker Then
                DT.Text = ""
            End If
        Next

        For Each RT As Control In Pan.Controls
            If TypeOf (RT) Is RichTextBox Then
                RT.Text = ""
            End If
        Next
        For Each LB As Control In Pan.Controls
            If TypeOf (LB) Is ListBox Then
                LB.Text = ""
            End If
        Next
    End Sub

    Public Sub EffacerTexBox1(ByVal Pan As GroupBox)
        For Each TB As Control In Pan.Controls
            If TypeOf (TB) Is DevExpress.XtraEditors.TextEdit Then
                TB.Text = ""
            End If
        Next
        For Each CBE As Control In Pan.Controls
            If TypeOf (CBE) Is DevExpress.XtraEditors.ComboBoxEdit Then
                CBE.Text = ""
            End If
        Next
        For Each CB As Control In Pan.Controls
            If TypeOf (CB) Is ComboBox Then
                CB.Text = ""
            End If
        Next
        For Each DT As Control In Pan.Controls
            If TypeOf (DT) Is DateTimePicker Then
                DT.Text = ""
            End If
        Next

        For Each RT As Control In Pan.Controls
            If TypeOf (RT) Is RichTextBox Then
                RT.Text = ""
            End If
        Next
        For Each LB As Control In Pan.Controls
            If TypeOf (LB) Is ListBox Then
                LB.Text = ""
            End If
        Next
    End Sub

    Public Sub EffacerTexBox2(ByVal Pan As DevExpress.XtraEditors.GroupControl)
        For Each TB As Control In Pan.Controls
            If TypeOf (TB) Is DevExpress.XtraEditors.TextEdit Then
                TB.Text = ""
            End If
        Next
        For Each SB As Control In Pan.Controls
            If TypeOf (SB) Is DevExpress.XtraEditors.SimpleButton Then
                SB.Text = ""
            End If
        Next
        For Each CBE As Control In Pan.Controls
            If TypeOf (CBE) Is DevExpress.XtraEditors.ComboBoxEdit Then
                CBE.Text = ""
            End If
        Next
        For Each CB As Control In Pan.Controls
            If TypeOf (CB) Is ComboBox Then
                CB.Text = ""
            End If
        Next
        For Each DT As Control In Pan.Controls
            If TypeOf (DT) Is DateTimePicker Then
                DT.Text = ""
            End If
        Next

        For Each RT As Control In Pan.Controls
            If TypeOf (RT) Is RichTextBox Then
                RT.Text = ""
            End If
        Next
        For Each LB As Control In Pan.Controls
            If TypeOf (LB) Is ListBox Then
                LB.Text = ""
            End If
        Next
    End Sub

    Sub ActiverChamps10(ByVal Pan As GroupBox)
        For Each TB As Control In Pan.Controls
            If TypeOf (TB) Is DevExpress.XtraEditors.TextEdit Then
                TB.Enabled = True
            End If
        Next
        For Each SB As Control In Pan.Controls
            If TypeOf (SB) Is DevExpress.XtraEditors.SimpleButton Then
                SB.Enabled = True
            End If
        Next
        For Each CE As Control In Pan.Controls
            If TypeOf (CE) Is DevExpress.XtraEditors.CheckEdit Then
                CE.Enabled = True
            End If
        Next
        For Each CBE As Control In Pan.Controls
            If TypeOf (CBE) Is DevExpress.XtraEditors.ComboBoxEdit Then
                CBE.Enabled = True
            End If
        Next
        For Each CB As Control In Pan.Controls
            If TypeOf (CB) Is ComboBox Then
                CB.Enabled = True
            End If
        Next
        For Each DT As Control In Pan.Controls
            If TypeOf (DT) Is DateTimePicker Then
                DT.Enabled = True
            End If
        Next

        For Each RT As Control In Pan.Controls
            If TypeOf (RT) Is RichTextBox Then
                RT.Enabled = True
            End If
        Next

    End Sub

    Sub ActiverChamps2(ByVal Pan As DevExpress.XtraEditors.GroupControl)
        For Each TB As Control In Pan.Controls
            If TypeOf (TB) Is DevExpress.XtraEditors.TextEdit Then
                TB.Enabled = True
            End If
        Next
        For Each SB As Control In Pan.Controls
            If TypeOf (SB) Is DevExpress.XtraEditors.SimpleButton Then
                SB.Enabled = True
            End If
        Next
        For Each CE As Control In Pan.Controls
            If TypeOf (CE) Is DevExpress.XtraEditors.CheckEdit Then
                CE.Enabled = True
            End If
        Next
        For Each CBE As Control In Pan.Controls
            If TypeOf (CBE) Is DevExpress.XtraEditors.ComboBoxEdit Then
                CBE.Enabled = True
            End If
        Next
        For Each CB As Control In Pan.Controls
            If TypeOf (CB) Is ComboBox Then
                CB.Enabled = True
            End If
        Next
        For Each DT As Control In Pan.Controls
            If TypeOf (DT) Is DateTimePicker Then
                DT.Enabled = True
            End If
        Next

        For Each RT As Control In Pan.Controls
            If TypeOf (RT) Is RichTextBox Then
                RT.Enabled = True
            End If
        Next
    End Sub

    Sub ActiverChamps5(ByVal Pan As DevExpress.XtraEditors.GroupControl)
        For Each TB As Control In Pan.Controls
            If TypeOf (TB) Is DevExpress.XtraEditors.TextEdit Then
                TB.Enabled = True
            End If
        Next
        For Each SB As Control In Pan.Controls
            If TypeOf (SB) Is DevExpress.XtraEditors.SimpleButton Then
                SB.Enabled = True
            End If
        Next
        For Each CE As Control In Pan.Controls
            If TypeOf (CE) Is DevExpress.XtraEditors.CheckEdit Then
                CE.Enabled = True
            End If
        Next
        For Each CBE As Control In Pan.Controls
            If TypeOf (CBE) Is DevExpress.XtraEditors.ComboBoxEdit Then
                CBE.Enabled = True
            End If
        Next
        For Each CB As Control In Pan.Controls
            If TypeOf (CB) Is ComboBox Then
                CB.Enabled = True
            End If
        Next
        For Each DT As Control In Pan.Controls
            If TypeOf (DT) Is DateTimePicker Then
                DT.Enabled = True
            End If
        Next

        For Each RT As Control In Pan.Controls
            If TypeOf (RT) Is RichTextBox Then
                RT.Enabled = True
            End If
        Next
        For Each LB As Control In Pan.Controls
            If TypeOf (LB) Is ListBox Then
                LB.Enabled = True
            End If
        Next
        For Each DG As Control In Pan.Controls
            If TypeOf (DG) Is DataGridView Then
                DG.Enabled = True
            End If
        Next
    End Sub

    Sub ActiverChamps4(ByVal Pan As DevExpress.XtraEditors.PanelControl)
        For Each TB As Control In Pan.Controls
            If TypeOf (TB) Is DevExpress.XtraEditors.TextEdit Then
                TB.Enabled = True
            End If
        Next
        For Each SB As Control In Pan.Controls
            If TypeOf (SB) Is DevExpress.XtraEditors.SimpleButton Then
                SB.Enabled = True
            End If
        Next
        For Each LB As Control In Pan.Controls
            If TypeOf (LB) Is ListBox Then
                LB.Enabled = True
            End If
        Next
        For Each CE As Control In Pan.Controls
            If TypeOf (CE) Is DevExpress.XtraEditors.CheckEdit Then
                CE.Enabled = True
            End If
        Next
        For Each CBE As Control In Pan.Controls
            If TypeOf (CBE) Is DevExpress.XtraEditors.ComboBoxEdit Then
                CBE.Enabled = True
            End If
        Next
        For Each CB As Control In Pan.Controls
            If TypeOf (CB) Is ComboBox Then
                CB.Enabled = True
            End If
        Next
        For Each DT As Control In Pan.Controls
            If TypeOf (DT) Is DateTimePicker Then
                DT.Enabled = True
            End If
        Next

        For Each RT As Control In Pan.Controls
            If TypeOf (RT) Is RichTextBox Then
                RT.Enabled = True
            End If
        Next
    End Sub

    Sub ActiverChamps3(ByVal Pan As Panel)
        For Each TB As Control In Pan.Controls
            If TypeOf (TB) Is DevExpress.XtraEditors.TextEdit Then
                TB.Enabled = True
            End If
        Next
        For Each SB As Control In Pan.Controls
            If TypeOf (SB) Is DevExpress.XtraEditors.SimpleButton Then
                SB.Enabled = True
            End If
        Next
        For Each CE As Control In Pan.Controls
            If TypeOf (CE) Is DevExpress.XtraEditors.CheckEdit Then
                CE.Enabled = True
            End If
        Next
        For Each CBE As Control In Pan.Controls
            If TypeOf (CBE) Is DevExpress.XtraEditors.ComboBoxEdit Then
                CBE.Enabled = True
            End If
        Next
        For Each CB As Control In Pan.Controls
            If TypeOf (CB) Is ComboBox Then
                CB.Enabled = True
            End If
        Next
        For Each DT As Control In Pan.Controls
            If TypeOf (DT) Is DateTimePicker Then
                DT.Enabled = True
            End If
        Next

        For Each RT As Control In Pan.Controls
            If TypeOf (RT) Is RichTextBox Then
                RT.Enabled = True
            End If
        Next
    End Sub

    Sub DesactiverChamps(ByVal Pan As DevExpress.XtraEditors.GroupControl)
        For Each TB As Control In Pan.Controls
            If TypeOf (TB) Is DevExpress.XtraEditors.TextEdit Then
                TB.Enabled = False
            End If
        Next
        For Each SB As Control In Pan.Controls
            If TypeOf (SB) Is DevExpress.XtraEditors.SimpleButton Then
                SB.Enabled = False
            End If
        Next
        For Each CE As Control In Pan.Controls
            If TypeOf (CE) Is DevExpress.XtraEditors.CheckEdit Then
                CE.Enabled = False
            End If
        Next
        For Each CBE As Control In Pan.Controls
            If TypeOf (CBE) Is DevExpress.XtraEditors.ComboBoxEdit Then
                CBE.Enabled = False
            End If
        Next
        For Each CB As Control In Pan.Controls
            If TypeOf (CB) Is ComboBox Then
                CB.Enabled = False
            End If
        Next
        For Each DT As Control In Pan.Controls
            If TypeOf (DT) Is DateTimePicker Then
                DT.Enabled = False
            End If
        Next

        For Each RT As Control In Pan.Controls
            If TypeOf (RT) Is RichTextBox Then
                RT.Enabled = False
            End If
        Next
    End Sub

    Sub DesactiverChamps2(ByVal Pan As GroupBox)
        For Each TB As Control In Pan.Controls
            If TypeOf (TB) Is DevExpress.XtraEditors.TextEdit Then
                TB.Enabled = False
            End If
        Next
        For Each SB As Control In Pan.Controls
            If TypeOf (SB) Is DevExpress.XtraEditors.SimpleButton Then
                SB.Enabled = False
            End If
        Next
        For Each CE As Control In Pan.Controls
            If TypeOf (CE) Is DevExpress.XtraEditors.CheckEdit Then
                CE.Enabled = False
            End If
        Next
        For Each CBE As Control In Pan.Controls
            If TypeOf (CBE) Is DevExpress.XtraEditors.ComboBoxEdit Then
                CBE.Enabled = False
            End If
        Next
        For Each CB As Control In Pan.Controls
            If TypeOf (CB) Is ComboBox Then
                CB.Enabled = False
            End If
        Next
        For Each DT As Control In Pan.Controls
            If TypeOf (DT) Is DateTimePicker Then
                DT.Enabled = False
            End If
        Next

        For Each RT As Control In Pan.Controls
            If TypeOf (RT) Is RichTextBox Then
                RT.Enabled = False
            End If
        Next
    End Sub

    Sub DesactiverChamps3(ByVal Pan As DevExpress.XtraEditors.PanelControl)
        For Each TB As Control In Pan.Controls
            If TypeOf (TB) Is DevExpress.XtraEditors.TextEdit Then
                TB.Enabled = False
            End If
        Next
        For Each SB As Control In Pan.Controls
            If TypeOf (SB) Is DevExpress.XtraEditors.SimpleButton Then
                SB.Enabled = False
            End If
        Next
        For Each CE As Control In Pan.Controls
            If TypeOf (CE) Is DevExpress.XtraEditors.CheckEdit Then
                CE.Enabled = False
            End If
        Next
        For Each CBE As Control In Pan.Controls
            If TypeOf (CBE) Is DevExpress.XtraEditors.ComboBoxEdit Then
                CBE.Enabled = False
            End If
        Next
        For Each CB As Control In Pan.Controls
            If TypeOf (CB) Is ComboBox Then
                CB.Enabled = False
            End If
        Next
        For Each DT As Control In Pan.Controls
            If TypeOf (DT) Is DateTimePicker Then
                DT.Enabled = False
            End If
        Next

        For Each RT As Control In Pan.Controls
            If TypeOf (RT) Is RichTextBox Then
                RT.Enabled = False
            End If
        Next
    End Sub

    Public Sub EffacerXT(ByVal xtra As DevExpress.XtraTab.XtraTabControl)
        For Each TB As Control In xtra.Controls
            If TypeOf (TB) Is DevExpress.XtraEditors.TextEdit Then
                TB.Text = ""
            End If
        Next
        For Each BE As Control In xtra.Controls
            If TypeOf (BE) Is DevExpress.XtraEditors.ButtonEdit Then
                BE.Text = ""
            End If
        Next
        For Each FE As Control In xtra.Controls
            If TypeOf (FE) Is DevExpress.XtraEditors.FontEdit Then
                FE.Text = ""
            End If
        Next
        For Each CE As Control In xtra.Controls
            If TypeOf (CE) Is DevExpress.XtraEditors.CalcEdit Then
                CE.Text = ""
            End If
        Next
        For Each CB As Control In xtra.Controls
            If TypeOf (CB) Is ComboBox Then
                CB.Text = ""
            End If
        Next
    End Sub

    Public Sub EffacerGC(ByVal grp As DevExpress.XtraEditors.GroupControl)
        For Each TB As Control In grp.Controls
            If TypeOf (TB) Is DevExpress.XtraEditors.TextEdit Then
                TB.Text = ""
            End If
        Next
        For Each BE As Control In grp.Controls
            If TypeOf (BE) Is DevExpress.XtraEditors.ButtonEdit Then
                BE.Text = ""
            End If
        Next
        For Each FE As Control In grp.Controls
            If TypeOf (FE) Is DevExpress.XtraEditors.FontEdit Then
                FE.Text = "0"
            End If
        Next
        For Each CE As Control In grp.Controls
            If TypeOf (CE) Is DevExpress.XtraEditors.CalcEdit Then
                CE.Text = "0"
            End If
        Next
        For Each CB As Control In grp.Controls
            If TypeOf (CB) Is ComboBox Then
                CB.Text = ""
            End If
        Next
    End Sub

    Function dateconvert(ByVal datetext As String)
        'conversion de la date
        If datetext.Length < 10 Then
            Return datetext
        End If
        Dim str(3) As String
        Dim tempdt As String = String.Empty
        If datetext.Length > 10 Then
            str = datetext.Split(" ")(0).Split("/")
            For j As Integer = 2 To 0 Step -1
                tempdt += str(j) & "-"
            Next
            tempdt = tempdt.Substring(0, 10)
            tempdt &= " " & datetext.Split(" ")(1)
        Else
            str = datetext.Split("/")
            For j As Integer = 2 To 0 Step -1
                tempdt += str(j) & "-"
            Next
            tempdt = tempdt.Substring(0, 10)
        End If
        Return tempdt
    End Function

    Function dateconvertpasse(ByVal datetext As Date)
        'conversion de la date
        Dim str(3) As String
        str = datetext.AddYears(-1).ToString("dd/MM/yyyy").Split("/")
        Dim tempdt As String = String.Empty
        For j As Double = 2 To 0 Step -1
            tempdt += str(j) & "-"
        Next
        tempdt = tempdt.Substring(0, 10)
        Return tempdt
    End Function

    Function limiter(ByVal caract As String)
        If caract.Length > 50 Then
            caract = caract.Substring(0, 50)
        End If
        Return MettreApost(caract)
    End Function
    Function limiter(ByVal text As String, Limit As Decimal) As String
        If text.Length > 0 Then
            If text.Length > Limit Then
                Return Mid(text, 1, Limit)
            End If
        End If
        Return text
    End Function


    Public Function DateToEngFormat(ByRef myDate As String) As String
        Dim str() As String = {"", "", ""}
        str = myDate.Split("/")
        If str(0) <> "" Then
            Return str(1) & "/" & str(0) & "/" & str(2)
        Else
            str = myDate.Split("-")
            Return str(1) & "/" & str(0) & "/" & str(2)
        End If
    End Function

    Sub RemplirDatagrid8(ByVal marekete As String, ByVal mondg As DevExpress.XtraGrid.GridControl)
        Try
            dtService.Columns.Clear()
            dtService.Columns.Add("N°", Type.GetType("System.String"))
            dtService.Columns.Add("Service", Type.GetType("System.String"))
            dtService.Columns.Add("Fonction", Type.GetType("System.String"))
            dtService.Columns.Add("Date d'attribution", Type.GetType("System.String"))
            dtService.Columns.Add("RefDecoupAdmin", Type.GetType("System.String"))
            dtService.Columns.Add("Poste Actuel", Type.GetType("System.Boolean"))
            dtService.Rows.Clear()

            Dim nligne = 0
            Dim dt As DataTable = ExcecuteSelectQuery(marekete)
            For Each rw In dt.Rows

                Dim drS = dtService.NewRow()
                drS(0) = rw(3).ToString
                drS(1) = MettreApost(rw(2).ToString)
                drS(2) = MettreApost(rw(0).ToString)
                drS(3) = CDate(rw(1)).ToString("dd/MM/yyyy")
                drS(4) = rw(4)
                drS(5) = IIf(rw(5) = "O", True, False)
                dtService.Rows.Add(drS)
                nligne = nligne + 1

            Next
            mondg.DataSource = dtService

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Sub RemplirDatagrid1(ByVal marekete As String, ByVal mondg As DevExpress.XtraGrid.GridControl, ByRef leView As DevExpress.XtraGrid.Views.Grid.GridView)
        'Try
        dtListEmploye.Columns.Clear()

        dtListEmploye.Columns.Add("N°", Type.GetType("System.String"))                                 '1
        dtListEmploye.Columns.Add("id", Type.GetType("System.String"))                                 '2
        dtListEmploye.Columns.Add("Matricule", Type.GetType("System.String"))                          '3
        dtListEmploye.Columns.Add("Nom", Type.GetType("System.String"))                                '4
        dtListEmploye.Columns.Add("Prénoms", Type.GetType("System.String"))                            '5
        dtListEmploye.Columns.Add("Sexe", Type.GetType("System.String"))                               '6
        dtListEmploye.Columns.Add("Date de naissance", Type.GetType("System.String"))                  '7
        dtListEmploye.Columns.Add("Lieu de naissance", Type.GetType("System.String"))                  '8
        dtListEmploye.Columns.Add("Nationalité", Type.GetType("System.String"))                        '9
        dtListEmploye.Columns.Add("Spécialité", Type.GetType("System.String"))                         '10
        dtListEmploye.Columns.Add("Contact", Type.GetType("System.String"))                            '11
        dtListEmploye.Columns.Add("Adresse", Type.GetType("System.String"))                            '12
        dtListEmploye.Columns.Add("E-mail", Type.GetType("System.String"))                             '13
        dtListEmploye.Columns.Add("Situation Matrimoniale", Type.GetType("System.String"))             '14
        dtListEmploye.Columns.Add("Nb enfant", Type.GetType("System.String"))                          '15
        dtListEmploye.Columns.Add("Données", Type.GetType("System.String"))                            '16
        dtListEmploye.Columns.Add("N° CNPS", Type.GetType("System.String"))                            '17
        dtListEmploye.Columns.Add("Diplôme", Type.GetType("System.String"))                            '18
        dtListEmploye.Columns.Add("CV", Type.GetType("System.String"))                                 '19
        dtListEmploye.Columns.Add("LM", Type.GetType("System.String"))                                 '20
        dtListEmploye.Columns.Add("DiplômeBlob", Type.GetType("System.String"))                        '21
        dtListEmploye.Columns.Add("Civilite", Type.GetType("System.String"))                           '22
        dtListEmploye.Columns.Add("EMP_CNAM", Type.GetType("System.String"))                           '23
        dtListEmploye.Rows.Clear()

        Dim nligNe As Integer = 0
        Dim dt As DataTable = ExcecuteSelectQuery(marekete)
        For Each rw In dt.Rows

            nligNe += 1
            Dim drS = dtListEmploye.NewRow()
            drS(0) = IIf(CDec(nligNe / 2) <> CDec(nligNe \ 2), "x", "").ToString
            drS(1) = rw(0).ToString
            drS(2) = rw(1).ToString
            drS(3) = MettreApost(rw(2).ToString)
            drS(4) = MettreApost(rw(3).ToString)
            drS(5) = rw(4).ToString
            drS(6) = rw(5).ToString
            drS(7) = rw(6).ToString
            drS(8) = MettreApost(rw(7).ToString)
            drS(9) = MettreApost(rw(8).ToString)
            drS(10) = rw(9).ToString
            drS(11) = MettreApost(rw(10).ToString)
            drS(12) = MettreApost(rw(11).ToString)
            drS(13) = MettreApost(rw(12).ToString)
            drS(14) = rw(13).ToString
            drS(15) = MettreApost(rw(14).ToString)
            drS(16) = rw(15).ToString
            drS(17) = MettreApost(rw(16).ToString)
            drS(18) = MettreApost(rw(17).ToString)
            drS(19) = MettreApost(rw(18).ToString)
            drS(20) = MettreApost(rw(19).ToString)
            drS(21) = MettreApost(rw(20).ToString)
            drS(22) = MettreApost(rw(21).ToString)
            dtListEmploye.Rows.Add(drS)

        Next

        mondg.DataSource = dtListEmploye
        leView.Columns(0).Visible = False
        leView.Columns(1).Visible = False
        leView.Columns(2).Width = 100
        leView.Columns(3).Width = 150
        leView.Columns(4).Width = 150
        leView.Columns(5).Width = 50
        leView.Columns(6).Width = 100
        leView.Columns(7).Width = 150
        leView.Columns(8).Width = 150
        leView.Columns(9).Width = 150
        leView.Columns(10).Width = 100
        leView.Columns(11).Width = 150
        leView.Columns(12).Width = 150
        leView.Columns(13).Width = 150
        leView.Columns(14).Width = 50
        leView.Columns(15).Visible = False
        leView.Columns(16).Width = 120
        leView.Columns(17).Width = 150
        leView.Columns(18).Visible = False
        leView.Columns(19).Visible = False
        leView.Columns(20).Visible = False
        leView.Columns(21).Visible = False
        leView.Columns(22).Visible = False
        leView.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        leView.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        leView.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        leView.Columns(10).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        leView.Columns(14).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        leView.Columns(16).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        leView.Appearance.Row.Font = New Font("Times New Roman", 12, FontStyle.Regular)
        ColorRowGrid(leView, "[N°]='x'", Color.LightGray, "Times New Roman", 12, FontStyle.Regular, Color.Black)

        'Catch ex As Exception
        '    Failmsg("Erreur : Information non disponible : " & ex.ToString())
        'End Try
    End Sub
    Public Function ReduireMontant(Montant As Decimal, Limite As Decimal) As Decimal
        If Montant = 0 Then
            Return Montant
        End If
        Dim LesZeros As String = String.Empty
        If Limite >= 3 Then
            For i = 1 To Limite
                LesZeros += "0"
            Next
        End If
        Dim diviseur As Decimal = Val("1" & LesZeros)
        Return Round((Montant / diviseur), 2)
    End Function

    Public Function HaveInternetConnection() As Boolean
        Try
            Return My.Computer.Network.Ping("www.google.com")
        Catch
            Return False
        End Try
    End Function
    Public Function GetFullComputerName() As String
        Dim domaine As String = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName
        Dim computername As String = System.Net.Dns.GetHostName
        Dim ComputerFullName As String = computername & "." & domaine
        Return ComputerFullName
    End Function


    Public Function FormatFileName(ByVal FileName As String, ByVal Separateur As String) As String
        Return FileName.Replace("/", Separateur).Replace("\", Separateur).Replace(":", Separateur).Replace("*", Separateur).Replace("?", Separateur).Replace("""", Separateur).Replace("<", Separateur).Replace(">", Separateur).Replace("|", Separateur)
    End Function
    Public Function SplitString(ByVal Str As String, ByVal Separateur As String) As String()
        Return Split(Str, Separateur)
    End Function

    Public Function GetNewCode(ByVal CodeRetounrer As String) As String
        Try
            While (CodeRetounrer.Length < 4)
                CodeRetounrer = "0" & CodeRetounrer
            End While
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        Return CodeRetounrer
    End Function
End Module
