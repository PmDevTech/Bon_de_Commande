Imports MySql.Data.MySqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Service

    Dim dtService = New DataTable()
    Dim DrX As DataRow

    Dim CodeLocal As String = ""
    Dim CodeSup As String = "0"
    Dim CodeDivAdmin As String = "0"
    Dim ModifEnCours As Boolean = False
    Dim CodeModif As String = ""
    Dim CodeZone As String = ""
    Dim Niveau As Decimal = 0

    Private Sub Service_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        InitFormulaire()
        CmbDivAdmin.ResetText()
        CmbDivAdmin.SelectedIndex = -1
        ChargerDivAdmin()
        CmbDivAdmin_SelectedIndexChanged(sender, e)
        CodeLocal = ""
        CodeSup = "0"
        ModifEnCours = False
        CodeModif = ""
        CodeZone = ""
    End Sub
    Private Sub ChargerDivAdmin()
        query = "select D.LibelleDivision, P.OrdrePlan, D.RefDecoupSup from T_DivisionAdministrative as D, T_PlanAdministratif as P where D.RefPlan=P.RefPlan and P.CodeProjet='" & ProjetEnCours & "' order by P.OrdrePlan, D.RefDecoupSup, D.LibelleDivision"
        CmbDivAdmin.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbDivAdmin.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next
    End Sub
    Private Sub ChargerZone(ByVal CodeZoneMere As String, ByVal NiveauStr As Decimal)
        CmbLocalisation.Text = ""
        CmbLocalisation.Properties.Items.Clear()
        'If NiveauStr = 1 Or NiveauStr = 2 Or NiveauStr = 3 Or NiveauStr = 4 Then 'On charge les villes
        '    If NiveauStr = 1 Then
        '        query = "select LibelleZone from t_zonegeo where NiveauStr= '4' and CodeZoneMere IN (select CodeZone from t_zonegeo where NiveauStr= '3' and CodeZoneMere IN (select CodeZone from t_zonegeo where NiveauStr= '2' and CodeZoneMere IN (select CodeZone from t_zonegeo where NiveauStr= '1' and CodeZone='" & CodeZoneMere & "'))) ORDER BY LibelleZone ASC"
        '
        '    ElseIf NiveauStr = 2 Then
        '        query = "select LibelleZone from t_zonegeo where NiveauStr= '4' and CodeZoneMere IN (select CodeZone from t_zonegeo where NiveauStr= '3' and CodeZoneMere IN (select CodeZone from t_zonegeo where NiveauStr= '2' and CodeZone='" & CodeZoneMere & "')) ORDER BY LibelleZone ASC"
        '
        '    ElseIf NiveauStr = 3 Then
        '        query = "select LibelleZone from t_zonegeo where NiveauStr= '4' and CodeZoneMere IN (select CodeZone from t_zonegeo where NiveauStr= '3' and CodeZone='" & CodeZoneMere & "') ORDER BY LibelleZone ASC"
        '
        '    End If
        'ElseIf NiveauStr = 4 Then 'On charge les sous-prefectures
        '    query = "select LibelleZone from t_zonegeo where NiveauStr= '5' and CodeZoneMere IN (select CodeZone from t_zonegeo where NiveauStr= '4' and CodeZone='" & CodeZoneMere & "') ORDER BY LibelleZone ASC"
        '
        'ElseIf NiveauStr = 5 Then 'On charge les communes sous-prefectures
        '    query = "select LibelleZone from t_zonegeo where NiveauStr= '6' and CodeZoneMere IN (select CodeZone from t_zonegeo where NiveauStr= '5' and CodeZone='" & CodeZoneMere & "') ORDER BY LibelleZone ASC"
        '
        'ElseIf NiveauStr = 6 Then 'On charge les villages
        '    query = "select LibelleZone from t_zonegeo where NiveauStr= '7' and CodeZoneMere IN (select CodeZone from t_zonegeo where NiveauStr= '6' and CodeZone='" & CodeZoneMere & "') ORDER BY LibelleZone ASC"
        '
        'Else ' On charge les pays car on a une erreur
        '    query = "select LibelleZone from t_zonegeo where NiveauStr= '1' ORDER BY LibelleZone ASC"
        '
        'End If
        query = "select LibelleZone from t_zonegeo ORDER BY LibelleZone ASC"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbLocalisation.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next
    End Sub
    Private Sub ChargerVille(ByVal indiczone As String)
        query = "select AbregeZone, LibelleZone from T_ZoneGeo where IndicZone='" & indiczone & "' order by LibelleZone ASC"

        CmbLocalisation.Text = ""
        CmbLocalisation.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbLocalisation.Properties.Items.Add(MettreApost(rw(1).ToString))
        Next

    End Sub
    Private Sub ChargerSup(Optional CodeService As Decimal = 0)
        query = "select AbregeService, NomService from T_Service where RefDecoupAdmin='" & CodeDivAdmin & "' and CodeProjet='" & ProjetEnCours & "' and CodeService<>" & CodeService & " order by NomService ASC"
        CmbServ.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbServ.Properties.Items.Add(MettreApost(rw(1).ToString))
        Next
    End Sub

    Private Sub TxtService_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtService.TextChanged

        If BtEnrg.Enabled Then

            If (TxtService.Text.Replace(" ", "") <> "") Then

                Dim partS() As String = TxtService.Text.ToLower().Replace("'", "").Replace("  ", " ").Replace(" le", "").Replace(" la", "").Replace(" les", "").Replace(" l'", "").Replace(" de", "").Replace(" du", "").Replace(" des", "").Replace(" d'", "").Replace(" et", "").Split(" "c)
                Dim CodeS As String = ""
                For Each elt In partS
                    CodeS = CodeS & Mid(elt, 1, 1).ToUpper
                Next
                TxtCodeServ.Text = CodeS

            Else
                TxtCodeServ.Text = ""
            End If
        End If

    End Sub

    Private Sub CmbLocalisation_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbLocalisation.SelectedValueChanged
        CodeLocal = ""
        If (CmbLocalisation.Text <> "") Then
            query = "select CodeZone from T_ZoneGeo where LibelleZone='" & EnleverApost(CmbLocalisation.Text) & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                Dim rw As DataRow = dt.Rows(0)
                CodeLocal = rw(0).ToString
            End If
        End If

    End Sub
    Private Sub CmbSup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbServ.SelectedIndexChanged
        CodeSup = "0"
        If (CmbServ.SelectedIndex <> -1) Then
            query = "select CodeService from T_Service where NomService='" & EnleverApost(CmbServ.Text) & "' and RefDecoupAdmin='" & CodeDivAdmin & "' and CodeProjet='" & ProjetEnCours & "'"
            CodeSup = ExecuteScallar(query)
        End If
    End Sub

    Private Sub RemplirService()

        dtService.Columns.Clear()

        dtService.Columns.Add("Code", Type.GetType("System.String"))
        dtService.Columns.Add("Ref", Type.GetType("System.String"))
        dtService.Columns.Add("Service", Type.GetType("System.String"))
        dtService.Columns.Add("*", Type.GetType("System.String"))
        dtService.Columns.Add("Localisé à", Type.GetType("System.String"))
        dtService.Columns.Add("Dépend de", Type.GetType("System.String"))
        dtService.Columns.Add("**", Type.GetType("System.String"))
        dtService.Columns.Add("Div", Type.GetType("System.String"))

        Dim cptr As Decimal = 0

        query = "select CodeService, AbregeService, NomService, CodeZone, CodeServiceSup, RefDecoupAdmin from T_Service where RefDecoupAdmin='" & CodeDivAdmin & "' and CodeProjet='" & ProjetEnCours & "' order by NomService ASC"
        dtService.Rows.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtService.NewRow()

            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = rw(0).ToString
            drS(2) = MettreApost(rw(2).ToString)
            drS(3) = rw(1).ToString
            drS(4) = Localisation(rw(3).ToString)
            Dim ServiceSup() As String = ServSup(rw(4).ToString)
            drS(5) = ServiceSup(1)
            drS(6) = ServiceSup(0)
            drS(7) = rw(5).ToString

            dtService.Rows.Add(drS)
        Next
        GridService.DataSource = dtService

        ViewService.Columns(0).Visible = False
        ViewService.Columns(1).Visible = False
        ViewService.Columns(2).Width = 250
        ViewService.Columns(3).Width = 60
        ViewService.Columns(4).Width = 250
        ViewService.Columns(5).Width = 250
        ViewService.Columns(6).Width = 60
        ViewService.Columns(7).Visible = False

        ViewService.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewService, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub

    Private Function ServSup(ByVal CodeService As String) As String()

        Dim Nom0 As String = ""
        Dim Code0 As String = ""
        query = "select AbregeService, NomService from T_Service where CodeService=" & CodeService & " and CodeProjet='" & ProjetEnCours & "' Order by NomService ASC"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Code0 = rw(0).ToString
            Nom0 = MettreApost(rw(1).ToString)
        Next
        Return {Code0, Nom0}

    End Function

    Private Function Localisation(ByVal code As String) As String

        Dim Local As String = ""

        query = "select LibelleZone from T_ZoneGeo where CodeZone='" & code & "' order by LibelleZone ASC"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            Dim rw As DataRow = dt.Rows(0)
            Local = MettreApost(rw(0).ToString)
        End If

        Return Local

    End Function

    Private Sub InitFormulaire()
        TxtService.Text = ""
        TxtService.Enabled = True
        TxtCodeServ.Text = ""
        TxtCodeServ.Enabled = True
        CmbServ.Text = ""
        CmbServ.Enabled = True
        CmbLocalisation.Text = ""
        CmbServ.Properties.Items.Clear()
        CmbLocalisation.Properties.Items.Clear()
        BtEnrg.Enabled = True
        ModifEnCours = False
        BtAjoutZone.Enabled = True

        BtModif.Enabled = False
        BtSupp.Enabled = False
        BtEnrg.Enabled = True
        CmbDivAdmin.Enabled = True
        CmbServ.Enabled = True
        Me.AcceptButton = BtEnrg
    End Sub
    Private Function NomDiv(ByVal CodeDiv As String) As String
        Dim ValRet As String = ""
        query = "select LibelleDivision from T_DivisionAdministrative where RefDecoupAdmin='" & CodeDiv & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            ValRet = MettreApost(rw(0).ToString)
        Next
        Return ValRet
    End Function

    Private Sub BtAnnuler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtAnnuler.Click
        InitFormulaire()
        CmbDivAdmin.Text = ""
        CmbDivAdmin.SelectedIndex = -1
        CodeDivAdmin = "0"
        RemplirService()
    End Sub

    Private Sub BtAjoutZone_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtAjoutZone.Click
        Dialog_form(Zonegeo)
        CmbLocalisation.Text = ""
        CmbDivAdmin_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub CmbDivAdmin_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbDivAdmin.SelectedIndexChanged
        CodeDivAdmin = "0"
        CodeZone = "0"
        Niveau = 0
        If CmbDivAdmin.SelectedIndex > -1 Then
            query = "select RefDecoupAdmin,CodeZone from T_DivisionAdministrative where LibelleDivision='" & EnleverApost(CmbDivAdmin.Text) & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                CodeDivAdmin = rw(0).ToString
                CodeZone = rw(1).ToString
            Next
            query = "select NiveauStr from t_zonegeo where CodeZone='" & CodeZone & "'"
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                Niveau = rw(0).ToString
            Next
            ChargerZone(CodeZone, Niveau)
            ChargerSup()

            If (ModifEnCours = False) Then
                RemplirService()
            End If
        Else
            CmbLocalisation.Text = ""
            CmbLocalisation.Properties.Items.Clear()
            ChargerSup()
            RemplirService()
        End If
    End Sub
    Private Sub GridService_Click(sender As System.Object, e As System.EventArgs) Handles GridService.Click

        If (ViewService.RowCount > 0) Then
            DrX = ViewService.GetDataRow(ViewService.FocusedRowHandle)

            Dim IDL = DrX(1).ToString
            ColorRowGrid(ViewService, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewService, "[Ref]='" & IDL & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)

            BtModif.Enabled = True
            BtSupp.Enabled = True
            BtEnrg.Enabled = False
            CmbDivAdmin.Enabled = False
            CmbServ.Enabled = False

            BtAjoutZone.Enabled = True
            TxtService.Enabled = True
            TxtCodeServ.Enabled = True

            CmbDivAdmin.Text = NomDiv(DrX(7).ToString)
            CodeDivAdmin = DrX(7).ToString
            TxtService.Text = DrX(2).ToString
            TxtCodeServ.Text = DrX(3).ToString

            CodeZone = "0"
            Niveau = 0
            query = "select RefDecoupAdmin,CodeZone from T_DivisionAdministrative where LibelleDivision='" & EnleverApost(CmbDivAdmin.Text) & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                CodeZone = rw(1).ToString
            Next

            query = "select NiveauStr from t_zonegeo where CodeZone='" & CodeZone & "'"
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                Niveau = rw(0).ToString
            Next

            ChargerZone(CodeZone, Niveau)
            ChargerSup(DrX(1))
            CmbLocalisation_SelectedValueChanged(sender, e)

            CmbServ.Text = DrX(5).ToString
            CmbServ.Enabled = True
            CmbLocalisation.Text = DrX(4).ToString
        End If
    End Sub
    Private Sub BtEnrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnrg.Click
        If CmbDivAdmin.SelectedIndex = -1 Then
            MessageBox.Show("Veuillez choisir une division administrative svp.", "Formulaire incomplet", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        ElseIf Trim(TxtService.Text) = "" Then
            MessageBox.Show("Veuillez entrer le nom du service svp.", "Formulaire incomplet", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtService.Focus()
            Exit Sub
        ElseIf Trim(TxtCodeServ.Text) = "" Then
            MessageBox.Show("Veuillez entrer le code du service svp.", "Formulaire incomplet", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtCodeServ.Focus()
            Exit Sub
        ElseIf CmbLocalisation.SelectedIndex = -1 Then
            MessageBox.Show("Veuillez choisir une localité svp.", "Formulaire incomplet", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CmbLocalisation.Focus()
            Exit Sub
        End If

        Try
            Dim servExist As Boolean = False

            query = "select * from T_Service where AbregeService='" & TxtCodeServ.Text & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                servExist = True
            End If


            If (servExist = True) Then
                FailMsg("Ce code existe déjà.")
                TxtCodeServ.Focus()
                Exit Sub
            End If

            If ViewService.RowCount = 0 Then 'Creation du premier service
                query = "insert into T_Service values(null," & CodeDivAdmin & ",'" & EnleverApost(Trim(TxtService.Text)) & "','" & TxtCodeServ.Text & "'," & CodeLocal & "," & CodeSup & ",'" & ProjetEnCours & "','" & Now.ToShortDateString & " " & Now.ToLongTimeString & "','" & Now.ToShortDateString & " " & Now.ToLongTimeString & "','" & CodeUtilisateur & "')"
                ExecuteNonQuery(query)
                'Dim str As String = "Chef de " & txtDebutService.Text + Trim(TxtService.Text)
                'Dim partS() As String = str.Replace("'", "").Replace("  ", " ").Replace(" le", "").Replace(" la", "").Replace(" les", "").Replace(" l'", "").Replace(" de", "").Replace(" du", "").Replace(" des", "").Replace(" d'", "").Split(" "c)
                'Dim CodeS As String = ""
                'For Each elt In partS
                '    CodeS = CodeS & Mid(elt, 1, 1).ToUpper
                'Next

                'query = "select CodeFonction from t_fonction where CodeFonction='" & CodeS & "'"
                'Dim res As String
                'res = ExecuteScallar(query)
                'Dim cpte As Decimal = 0
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
                'Dim LastServiceCode = ExecuteScallar("Select Max(CodeService) as Code from t_service")

                'If Val(CodeSup) <> 0 Then
                '    query = "select RefFonction from t_fonction where LibelleFonction LIKE 'Chef de %' and CodeService=" & CodeSup
                '    Dim CodeBoss As String = ""
                '    CodeBoss = ExecuteScallar(query)
                '    query = "INSERT INTO t_fonction VALUES(null,'" & res & "','Chef de " & txtDebutService.Text + EnleverApost(Trim(TxtService.Text)) & "'," & CodeBoss & "," & LastServiceCode & "," & CodeDivAdmin & ",'" & Now & "','" & Now & "','" & CodeUtilisateur & "')"
                'Else
                '    query = "select RefFonction from t_fonction where LibelleFonction LIKE 'DIRECTEUR %' and RefDecoupAdmin=" & CodeDivAdmin
                '    Dim CodeBoss As String = ""
                '    CodeBoss = ExecuteScallar(query)
                '    query = "INSERT INTO t_fonction VALUES(null,'" & res & "','Chef de " & txtDebutService.Text + EnleverApost(Trim(TxtService.Text)) & "'," & CodeBoss & "," & LastServiceCode & "," & CodeDivAdmin & ",'" & Now & "','" & Now & "','" & CodeUtilisateur & "')"
                'End If
                'ExecuteNonQuery(query)
                MessageBox.Show("Service enregistré avec succès.", "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                If CmbServ.SelectedIndex = -1 Then
                    FailMsg("Veuillez identifier le service dont il dépend svp")
                    CmbServ.Focus()
                    Exit Sub
                End If
                query = "insert into T_Service values(null," & CodeDivAdmin & ",'" & EnleverApost(Trim(TxtService.Text)) & "','" & TxtCodeServ.Text & "'," & CodeLocal & "," & CodeSup & ",'" & ProjetEnCours & "','" & Now.ToShortDateString & " " & Now.ToLongTimeString & "','" & Now.ToShortDateString & " " & Now.ToLongTimeString & "','" & CodeUtilisateur & "')"
                ExecuteNonQuery(query)


            End If

            InitFormulaire()
            CmbDivAdmin_SelectedIndexChanged(sender, e)
            RemplirService()

        Catch my As MySqlException
            MessageBox.Show("Erreur : Imformation non disponible." & vbNewLine & my.ToString(), "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'InputBox(my.ToString, 0, query)
        Catch ex As Exception
            MessageBox.Show("L'enregistrement à échoué!", "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub BtModif_Click(sender As System.Object, e As System.EventArgs) Handles BtModif.Click, ModifierService.Click
        If Trim(TxtService.Text) = "" Then
            MessageBox.Show("Veuillez entrer le nom du service svp.", "Formulaire incomplet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf Trim(TxtCodeServ.Text) = "" Then
            MessageBox.Show("Veuillez entrer le code du service svp.", "Formulaire incomplet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Try
            DrX = ViewService.GetDataRow(ViewService.FocusedRowHandle)
            query = "select CodeService from T_Service where AbregeService='" & TxtCodeServ.Text & "'"
            Dim verif As String = ExecuteScallar(query)
            If Len(verif) <> 0 And verif <> DrX(1) Then
                MsgBox("Ce code est déjà utilisé.")
                TxtCodeServ.Focus()
                TxtCodeServ.SelectAll()
                Exit Sub
            End If

            query = "select CodeServiceSup from T_Service where CodeService='" & DrX(1).ToString & "'"
            Dim verif1 As String = ExecuteScallar(query)
            Try
                If Val(verif1) = 0 Then
                    CodeSup = 0
                End If
            Catch ex As Exception

            End Try
            If CmbServ.SelectedIndex = -1 And Val(verif1) <> 0 Then
                FailMsg("Veuillez identifier le service dont il dépend svp")
                CmbServ.Focus()
                Exit Sub
            End If
           query= "Update T_Service set NomService='" & EnleverApost(Trim(TxtService.Text)) & "', AbregeService='" & TxtCodeServ.Text & "', CodeZone='" & CodeLocal & "',CodeServiceSup=" & CodeSup & ", DateModif='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "', Operateur='" & CodeUtilisateur & "'  where CodeProjet='" & ProjetEnCours & "' and CodeService='" & DrX(1).ToString & "'"
            ExecuteNonQuery(query)
            InitFormulaire()
            CmbDivAdmin_SelectedIndexChanged(sender, e)
            RemplirService()

        Catch my As MySqlException
            Failmsg("Erreur : Information non disponible : " & my.Message)
        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub
    Public Sub PerpetualDeleteService(codeService As Decimal)
        Try
            query = "select CodeService from T_Service where CodeServiceSup=" & codeService
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw As DataRow In dt.Rows
                    query = "delete from T_Service where CodeProjet='" & ProjetEnCours & "' and CodeService=" & rw(0)
                    ExecuteNonQuery(query)
                    query = "Delete from t_fonction where CodeService=" & rw(0)
                    ExecuteNonQuery(query)
                    PerpetualDeleteService(rw(0))
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("Erreur : Information non disponible." & vbNewLine & ex.ToString(), "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub BtSupp_Click(sender As System.Object, e As System.EventArgs) Handles BtSupp.Click
        If ViewService.RowCount > 0 Then
            DrX = ViewService.GetDataRow(ViewService.FocusedRowHandle)
            Dim Reponse As DialogResult = MessageBox.Show("Attention !!!" & vbNewLine & "Supprimer un service aura pour effet de supprimer les autres services qui dépendent de celui-ci ainsi que leurs fonctions associées." & vbNewLine & "Voulez-vous continuer le processus de suppression de [" & DrX(2).ToString & "] ?", "ClearProject", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If Reponse = DialogResult.Yes Then
                Dim CodeServiceASupp As Decimal = Val(DrX(1))
                Dim ServiceASupp As String = DrX(2)
                query = "delete from T_Service where CodeProjet='" & ProjetEnCours & "' and CodeService=" & CodeServiceASupp
                ExecuteNonQuery(query)
                query = "Delete from t_fonction where CodeService=" & CodeServiceASupp
                ExecuteNonQuery(query)
                PerpetualDeleteService(CodeServiceASupp)

                InitFormulaire()
                CmbDivAdmin_SelectedIndexChanged(sender, e)
                RemplirService()
                MessageBox.Show(ServiceASupp & " supprimé avec succès.", "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Else
            MsgBox("Suppression Impossible!", MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Sub SupprimerServiceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupprimerServiceToolStripMenuItem.Click
        If ViewService.RowCount > 0 Then

            DrX = ViewService.GetDataRow(ViewService.FocusedRowHandle)
            Dim IDL = DrX(1).ToString
            ColorRowGrid(ViewService, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewService, "[Ref]='" & IDL & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)
            If BtSupp.Enabled = False Then
                BtSupp.Enabled = True
                BtSupp.PerformClick()
                BtSupp.Enabled = False
            Else
                BtSupp.PerformClick()
            End If
        End If
    End Sub

    Private Sub Service_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        InitFormulaire()

    End Sub
    Private Sub Service_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        CmbDivAdmin.Focus()
    End Sub
End Class