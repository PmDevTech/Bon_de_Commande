Imports MySql.Data.MySqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Fonction

    Dim dtFonction = New DataTable()
    Dim DrX As DataRow

    Dim CodeDivAdm As String = "0"
    Dim CodeService As String = "0"
    Dim ModifEnCours As Boolean = False
    Dim CodeModif As String = ""
    Dim CodeBoss As String = "-1"
    Dim CodeDivSup As String = "0"
    Dim LibFonction As String
    Dim LesDivs As Decimal()
    Dim LesServices As Decimal()

    Private Sub Fonction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        InitFormulaire()
        CmbDivAdmin.ResetText()
        CmbDivAdmin.SelectedIndex = -1
        ChargerDivAdmin()
        CmbDivAdmin_SelectedIndexChanged(sender, e)
        CodeDivAdm = "0"
        CodeBoss = "-1"
        ModifEnCours = False
        CodeModif = ""
        CmbService_SelectedIndexChanged(sender, e)
    End Sub
    Private Sub ChargerDivAdmin()
        query = "select D.RefDecoupAdmin,D.LibelleDivision from T_DivisionAdministrative as D, T_PlanAdministratif as P where D.RefPlan=P.RefPlan and P.CodeProjet='" & ProjetEnCours & "' order by D.RefDecoupAdmin, D.LibelleDivision"
        CmbService.ResetText()
        CmbDivAdmin.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        ReDim LesDivs(dt.Rows.Count)
        Dim i As Decimal = 0
        For Each rw As DataRow In dt.Rows
            CmbDivAdmin.Properties.Items.Add(MettreApost(rw(1).ToString))
            LesDivs(i) = MettreApost(rw(0).ToString)
            i += 1
        Next
    End Sub
    Private Sub ChargerService(RefDiviAdmin As Decimal)
        CmbService.Properties.Items.Clear()
        CmbService.ResetText()
        query = "select CodeService,NomService from T_Service where CodeProjet='" & ProjetEnCours & "' and RefDecoupAdmin=" & RefDiviAdmin & " order by NomService ASC"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        ReDim LesServices(dt.Rows.Count)
        Dim i As Decimal = 0
        For Each rw As DataRow In dt.Rows
            CmbService.Properties.Items.Add(MettreApost(rw(1).ToString))
            LesServices(i) = MettreApost(rw(0).ToString)
            i += 1
        Next
    End Sub
    Private Sub BtAjoutService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtAjoutService.Click
        Dialog_form(Service)
        CmbDivAdmin_SelectedIndexChanged(sender, e)
    End Sub
    Private Sub CmbService_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbService.SelectedIndexChanged
        If CmbService.SelectedIndex <> -1 Then
            CodeService = LesServices(CmbService.SelectedIndex)
            query = "select RefFonction from t_fonction where ChefService=1 and CodeService=" & CodeService
            CodeBoss = ExecuteScallar(query)
            Try
                If Val(CodeBoss).ToString() <> 0 Then
                    CodeBoss = Val(CodeBoss).ToString()
                Else
                    CodeBoss = -1
                End If
            Catch ex As Exception
                CodeBoss = "-1"
            End Try
            ChargerGridFonction(CodeService, CodeDivAdm)
        Else
            ChargerGridFonction(-1, -1)
        End If
    End Sub

    Private Sub ChargerSup(ByVal Serv As String, ByVal Divis As String)
        'Dim Reader As MySqlDataReader
        query = "select LibelleFonction from T_Fonction where CodeService='" & Serv & "' and RefDecoupAdmin='" & Divis & "' Order by LibelleFonction"
        CmbSup.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbSup.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next
    End Sub
    Private Sub TxtFonction_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFonction.TextChanged
        If BtEnrg.Enabled Then
            If (TxtFonction.Text.Replace(" ", "") <> "") Then

                Dim partS() As String = (TxtFonction.Text.ToLower().Replace("'", "").Replace("  ", " ").Replace(" le", "").Replace(" la", "").Replace(" les", "").Replace(" l'", "").Replace(" de", "").Replace(" du", "").Replace(" des", "").Replace(" d'", "").Replace(" et", "")).Split(" "c)
                Dim CodeS As String = ""
                For Each elt In partS
                    CodeS = CodeS & Mid(elt, 1, 1).ToUpper
                Next
                TxtCodeFonction.Text = CodeS

            Else
                TxtCodeFonction.Text = ""
            End If
        End If

    End Sub
    Private Sub InitFormulaire()

        CmbService.Enabled = True
        CmbDivAdmin.Enabled = True
        TxtFonction.Text = ""
        TxtFonction.Enabled = True
        TxtCodeFonction.Text = ""
        TxtCodeFonction.Enabled = True
        chkChef.Checked = False
        CmbSup.Text = ""
        CmbSup.Enabled = True
        CmbSup.Properties.Items.Clear()
        CodeBoss = "-1"

        BtAvantage.Enabled = False
        BtModif.Enabled = False
        BtSupp.Enabled = False
        BtEnrg.Enabled = True

        BtAjoutService.Enabled = True

        'ChargerGridFonction(CodeServ, CodeDivAdm)
        'ChargerSup(CodeServ, CodeDivAdm)


    End Sub
    Private Sub ChargerGridFonction(ByVal CodeService As Decimal, ByVal CodeDiviAdmin As Decimal)

        dtFonction.Columns.Clear()

        dtFonction.Columns.Add("Code", Type.GetType("System.String"))
        dtFonction.Columns.Add("Ref", Type.GetType("System.String"))
        dtFonction.Columns.Add("Poste", Type.GetType("System.String"))
        dtFonction.Columns.Add("*", Type.GetType("System.String"))
        dtFonction.Columns.Add("Supérieur", Type.GetType("System.String"))
        dtFonction.Columns.Add("**", Type.GetType("System.String"))
        dtFonction.Columns.Add("ChefService", Type.GetType("System.String"))

        Dim cptr As Decimal = 0

        query = "select RefFonction, LibelleFonction, CodeFonction, CodeBoss, ChefService from T_Fonction where CodeService=" & CodeService & " and RefDecoupAdmin=" & CodeDiviAdmin & " order by LibelleFonction"
        dtFonction.Rows.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtFonction.NewRow()

            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = rw(0).ToString
            drS(2) = MettreApost(rw(1).ToString)
            drS(3) = rw(2).ToString
            Dim Superieur As Object = SupHierarch(rw(3).ToString())
            drS(4) = Superieur(1)
            drS(5) = Superieur(0)
            drS(6) = rw(4).ToString()

            dtFonction.Rows.Add(drS)
        Next

        GridFonction.DataSource = dtFonction

        ViewFonction.Columns(0).Visible = False
        ViewFonction.Columns(1).Visible = False
        ViewFonction.Columns(6).Visible = False
        ViewFonction.Columns(2).Width = 250
        ViewFonction.Columns(3).Width = 60
        ViewFonction.Columns(4).Width = 250
        ViewFonction.Columns(5).Width = 60

        ViewFonction.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewFonction, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub
    Private Function SupHierarch(ByVal Code As String) As String()
        Dim Nom0 As String = ""
        Dim Code0 As String = ""
        query = "select CodeFonction, LibelleFonction from T_Fonction where RefFonction=" & Code
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Code0 = rw(0).ToString
            Nom0 = MettreApost(rw(1).ToString)
        Next

        Return {Code0, Nom0}

    End Function
    Private Sub BtEnrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnrg.Click

        If CmbDivAdmin.SelectedIndex = -1 Then
            MessageBox.Show("Veuillez choisir une division administrative svp.", "Formulaire incomplet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf CmbService.SelectedIndex = -1 Then
            MessageBox.Show("Veuillez choisir un service svp.", "Formulaire incomplet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf TxtFonction.Text = "" Then
            MessageBox.Show("Veuillez entrer le nom de la fonction svp.", "Formulaire incomplet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtFonction.Focus()
            Exit Sub
        ElseIf TxtCodeFonction.Text = "" Then
            MessageBox.Show("Veuillez entrer le code de la fonction svp.", "Formulaire incomplet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtCodeFonction.Focus()
            Exit Sub
        End If

        Try
            Dim foncExist As Boolean = False

            query = "select * from T_Fonction where CodeService='" & CodeService & "' and RefDecoupAdmin='" & CodeDivAdm & "' and CodeFonction='" & TxtCodeFonction.Text & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                foncExist = True
            End If

            If (foncExist = True) Then
                FailMsg("Ce code existe déjà.")
                TxtCodeFonction.Focus()
                Exit Sub
            End If

            Dim Chef As Decimal = 0
            If chkChef.Checked = True Then
                Chef = 1
                CodeBoss = -1
            End If
            Dim data As Object = {"null", TxtCodeFonction.Text, EnleverApost(TxtFonction.Text), CodeBoss, CodeService, Chef, CodeDivAdm, Now.ToShortDateString & " " & Now.ToLongTimeString, Now.ToShortDateString & " " & Now.ToLongTimeString, CodeUtilisateur}
            If CreateFunction(data, Chef) = True Then
                ChargerGridFonction(Me.CodeService, CodeDivAdm)
                MessageBox.Show("Fonction enregistrée avec succès.", "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Information)
                InitFormulaire()
                CmbService_SelectedIndexChanged(sender, e)
            Else
                FailMsg("L'enregistrement à échoué.")
            End If
        Catch ex As Exception
            FailMsg("L'enregistrement à échoué!" & vbNewLine & ex.ToString)
        End Try

    End Sub
    Private Function CreateFunction(Data As Object, Chef As Decimal, Optional op As String = "Add") As Boolean
        Try
            If op = "Add" Then 'Ajout d'un nouveau poste
                If Chef = 1 Then
                    query = "insert into T_Fonction values(" & Data(0) & ",'" & Data(1) & "','" & Data(2) & "'," & Data(3) & ",'" & Data(4) & "'," & Data(5) & ",'" & Data(6) & "','" & Data(7) & "','" & Data(8) & "','" & Data(9) & "')"
                    ExecuteNonQuery(query)
                    Dim LastFonction As String
                    Try
                        LastFonction = ExecuteScallar("select max(RefFonction) from T_Fonction")
                    Catch ex As Exception
                        LastFonction = ""
                    End Try

                    query = "update T_Fonction set CodeBoss=" & LastFonction & ", ChefService=0 where CodeService=" & CodeService & " and RefFonction<>" & LastFonction 'On Reinitialise le chef du service
                    ExecuteNonQuery(query) 'On reinitialise les anciens chef et on fix le nouveau chef
                    query = "select CodeServiceSup,RefDecoupAdmin from t_service where CodeService=" & CodeService 'On get le superieur du service
                    Dim dt As DataTable = ExcecuteSelectQuery(query)
                    If dt.Rows.Count > 0 Then 'Identification du superieur
                        Dim CodeSceSup As String = dt.Rows(0).Item("CodeServiceSup").ToString()
                        Try
                            If Val(CodeSceSup) <> 0 Then
                                query = "select RefFonction from t_fonction where ChefService=1 and CodeService=" & Val(CodeSceSup)
                                Dim NewBoss = ExecuteScallar(query)
                                Try
                                    query = "update T_Fonction set CodeBoss=" & NewBoss & " where RefFonction=" & LastFonction
                                    ExecuteNonQuery(query)
                                    Return True
                                Catch ex As Exception
                                    Return True
                                End Try
                            Else
                                If RefDecoupSup(Val(dt.Rows(0).Item("RefDecoupAdmin").ToString())) = 0 Then 'On est au 1er Responsable du projet
                                    query = "update T_Fonction set CodeBoss=0 where RefFonction=" & LastFonction
                                    ExecuteNonQuery(query) 'On met 0 en CodeBoss pour le 1er responsable
                                    Return True
                                Else 'On va recuperer le departement superieur a celui du service
                                    query = "SELECT `RefDecoupAdmin` FROM `t_divisionadministrative` WHERE `RefDecoupAdmin`=(select RefDecoupSup from t_divisionadministrative where `RefDecoupAdmin`=" & dt.Rows(0).Item("RefDecoupAdmin").ToString() & ")"
                                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                                    If dt0.Rows.Count > 0 Then
                                        query = "select RefFonction from t_fonction where ChefService=1 and CodeService=(select CodeService from t_service where CodeServiceSup=0 and RefDecoupAdmin=" & dt0.Rows(0).Item(0) & ")"
                                        Dim dtx As DataTable = ExcecuteSelectQuery(query)
                                        If dtx.Rows.Count > 0 Then 'On a recuperer le responsable de la direction
                                            query = "update T_Fonction set CodeBoss=" & dtx.Rows(0).Item(0) & " where RefFonction=" & LastFonction
                                            ExecuteNonQuery(query)
                                            Return True
                                        Else
                                            Return True
                                        End If
                                    Else
                                        Return True
                                    End If
                                End If
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.ToString)
                            Return False
                        End Try
                    End If
                    query = "select CodeService from t_service where CodeServiceSup=" & CodeService 'On get le superieur du service
                    dt = ExcecuteSelectQuery(query) 'Identification du chef inferieur
                    If dt.Rows.Count > 0 Then
                        For Each rw As DataRow In dt.Rows
                            query = "update t_fonction set CodeBoss=" & LastFonction & " where ChefService=1 and CodeService=" & rw(0)
                            ExecuteNonQuery(query)
                        Next
                    End If
                    Return True
                ElseIf Chef = 0 Then
                    query = "insert into T_Fonction values(" & Data(0) & ",'" & Data(1) & "','" & Data(2) & "'," & Data(3) & ",'" & Data(4) & "'," & Data(5) & ",'" & Data(6) & "','" & Data(7) & "','" & Data(8) & "','" & Data(9) & "')"
                    ExecuteNonQuery(query)
                    Return True
                End If

            ElseIf op = "Modif" Then 'Modification d'un poste
                Dim OldChef = ExecuteScallar("select ChefService from T_Fonction where RefFonction=" & Data(0))
                If CBool(OldChef) = True Then 'On Modifie un chef de service
                    If Chef = 1 Then
                        query = "update T_Fonction set CodeFonction='" & Data(1) & "', LibelleFonction='" & Data(2) & "', CodeService='" & Data(4) & "', ChefService=" & Data(5) & ", RefDecoupAdmin='" & Data(6) & "',DateModif='" & Data(8) & "',Operateur='" & Data(9) & "' where RefFonction=" & Data(0)
                        ExecuteNonQuery(query) 'On modifie les données du chef de service
                        'Complete(Data(4))
                        Return True
                    Else
                        query = "update T_Fonction set CodeFonction='" & Data(1) & "', LibelleFonction='" & Data(2) & "', CodeBoss=-1, CodeService='" & Data(4) & "', ChefService=" & Data(5) & ", RefDecoupAdmin='" & Data(6) & "',DateModif='" & Data(8) & "',Operateur='" & Data(9) & "' where RefFonction=" & Data(0)
                        ExecuteNonQuery(query) 'On lui retranche le role de chef de service
                        query = "update T_Fonction set CodeBoss=-1, ChefService=0 where CodeService=" & CodeService & " and RefFonction<>" & Data(0)
                        ExecuteNonQuery(query) 'On Reinitialise le chef du service des autres postes
                        query = "update T_Fonction set CodeBoss=-1 where CodeBoss=" & Data(0)
                        ExecuteNonQuery(query) 'On Reinitialise le chef du service des autres postes
                        Return True
                    End If
                Else 'On Modifie un simple poste
                    If Chef = 1 Then
                        query = "update T_Fonction set CodeFonction='" & Data(1) & "', LibelleFonction='" & Data(2) & "', CodeBoss=" & Data(3) & ", CodeService='" & Data(4) & "', ChefService=" & Data(5) & ", RefDecoupAdmin='" & Data(6) & "',DateModif='" & Data(8) & "',Operateur='" & Data(9) & "' where RefFonction=" & Data(0)
                        ExecuteNonQuery(query) 'On lui donne le role de chef de service

                        query = "update T_Fonction set CodeBoss=" & Data(0) & ", ChefService=0 where CodeService=" & CodeService & " and RefFonction<>" & Data(0) 'On Reinitialise le chef du service
                        ExecuteNonQuery(query) 'On réinitialise les anciens chef et on fix le nouveau chef
                        'Complete(Data(4))

                        query = "select CodeServiceSup,RefDecoupAdmin from t_service where CodeService=" & CodeService 'On get le superieur du service
                        Dim dt As DataTable = ExcecuteSelectQuery(query)
                        If dt.Rows.Count > 0 Then
                            Dim CodeSceSup As String = dt.Rows(0).Item("CodeServiceSup").ToString()
                            Try
                                If Val(CodeSceSup) <> 0 Then
                                    query = "select RefFonction from t_fonction where ChefService=1 and CodeService=" & Val(CodeSceSup)
                                    Dim NewBoss = ExecuteScallar(query)
                                    Try
                                        query = "update T_Fonction set CodeBoss=" & NewBoss & " where RefFonction=" & Data(0)
                                        ExecuteNonQuery(query)
                                        Return True
                                    Catch ex As Exception
                                        Return True
                                    End Try
                                Else
                                    If RefDecoupSup(Val(dt.Rows(0).Item("RefDecoupAdmin").ToString())) = 0 Then 'On est au 1er Responsable du projet
                                        query = "update T_Fonction set CodeBoss=0 where RefFonction=" & Data(0)
                                        ExecuteNonQuery(query) 'On met 0 en CodeBoss pour le 1er responsable
                                        Return True
                                    Else 'On va recuperer le departement superieur a celui du service
                                        query = "SELECT `RefDecoupAdmin` FROM `t_divisionadministrative` WHERE `RefDecoupAdmin`=(select RefDecoupSup from t_divisionadministrative where `RefDecoupAdmin`=" & dt.Rows(0).Item("RefDecoupAdmin").ToString() & ")"
                                        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                                        If dt0.Rows.Count > 0 Then
                                            query = "select RefFonction from t_fonction where ChefService=1 and CodeService=(select CodeService from t_service where CodeServiceSup=0 and RefDecoupAdmin=" & dt0.Rows(0).Item(0) & ")"
                                            Dim dtx As DataTable = ExcecuteSelectQuery(query)
                                            If dtx.Rows.Count > 0 Then 'On a recuperer le responsable de la direction
                                                query = "update T_Fonction set CodeBoss=" & dtx.Rows(0).Item(0) & " where RefFonction=" & Data(0)
                                                ExecuteNonQuery(query)
                                                Return True
                                            End If
                                        End If
                                        Return True
                                    End If
                                End If
                            Catch ex As Exception
                                MessageBox.Show(ex.ToString)
                                Return False
                            End Try
                        End If
                        query = "select CodeService from t_service where CodeServiceSup=" & CodeService 'On get le superieur du service
                        dt = ExcecuteSelectQuery(query) 'Identification du chef inferieur
                        If dt.Rows.Count > 0 Then
                            For Each rw As DataRow In dt.Rows
                                query = "update t_fonction set CodeBoss=" & Data(0) & " where ChefService=1 and CodeService=" & rw(0)
                                ExecuteNonQuery(query)
                            Next
                        End If

                    Else
                        query = "update T_Fonction set CodeFonction='" & Data(1) & "', LibelleFonction='" & Data(2) & "', CodeBoss=" & Data(3) & ", CodeService='" & Data(4) & "', ChefService=" & Data(5) & ", RefDecoupAdmin='" & Data(6) & "',DateModif='" & Data(8) & "',Operateur='" & Data(9) & "' where RefFonction=" & Data(0)
                        ExecuteNonQuery(query) 'On modifie juste les données d'un simple poste
                        Return True
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            Return False
        End Try
    End Function
    Private Function RefDecoupSup(CodeDivision As Decimal) As Decimal
        Return Val(ExecuteScallar("SELECT RefDecoupSup FROM t_divisionadministrative WHERE RefDecoupAdmin='" & CodeDivision & "'"))
    End Function
    Private Sub BtAnnuler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtAnnuler.Click
        InitFormulaire()
        CmbDivAdmin.Text = ""
        CmbDivAdmin.SelectedIndex = -1
        CodeDivAdm = "0"
        ChargerService(CodeDivAdm)
        CmbService_SelectedIndexChanged(sender, e)
    End Sub
    Private Sub GridFonction_Click(sender As System.Object, e As System.EventArgs) Handles GridFonction.Click
        If (ViewFonction.RowCount > 0) Then

            DrX = ViewFonction.GetDataRow(ViewFonction.FocusedRowHandle)

            Dim IDL = DrX(1).ToString
            ColorRowGrid(ViewFonction, "[Code]='x'", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewFonction, "[Ref]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)

            CodeModif = DrX(1).ToString
            ModifEnCours = True
            BtEnrg.Enabled = True
            BtAjoutService.Enabled = True

            TxtFonction.Text = DrX(2).ToString
            TxtCodeFonction.Text = DrX(3).ToString
            CmbSup.Text = DrX(4).ToString

            TxtFonction.Enabled = True
            TxtCodeFonction.Enabled = True
            CmbSup.Enabled = True
            CmbService.Enabled = True

            BtModif.Enabled = True
            BtAvantage.Enabled = True
            BtEnrg.Enabled = False
            BtSupp.Enabled = True
            If CBool(DrX(6).ToString()) = True Then
                chkChef.Checked = True
            Else
                chkChef.Checked = False
            End If

            CmbService.Enabled = False
            CmbDivAdmin.Enabled = False
        End If
    End Sub
    Private Sub BtModif_Click(sender As System.Object, e As System.EventArgs) Handles BtModif.Click, ModifierFonction.Click
        If ViewFonction.RowCount > 0 Then
            If Trim(TxtFonction.Text) = "" Then
                MessageBox.Show("Veuillez entrer le nom de la fonction svp.", "Formulaire incomplet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TxtFonction.Focus()
                Exit Sub
            ElseIf Trim(TxtCodeFonction.Text) = "" Then
                MessageBox.Show("Veuillez entrer le code de la fonction svp.", "Formulaire incomplet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Try

                DrX = ViewFonction.GetDataRow(ViewFonction.FocusedRowHandle)
                query = "select RefFonction from T_Fonction where CodeFonction='" & TxtCodeFonction.Text & "' And CodeService=" & CodeService
                Dim verif As String = ExecuteScallar(query)
                If Len(verif) <> 0 And verif <> DrX(1) Then
                    SuccesMsg("Ce code est déjà utilisé.")
                    TxtCodeFonction.Focus()
                    TxtCodeFonction.SelectAll()
                    Exit Sub
                End If
                Dim Chef As Decimal = 0
                If chkChef.Checked = True Then
                    Chef = 1
                    CodeBoss = -1
                End If
                Dim data As Object = {DrX(1).ToString(), TxtCodeFonction.Text, EnleverApost(TxtFonction.Text), CodeBoss, CodeService, Chef, CodeDivAdm, Now.ToShortDateString & " " & Now.ToLongTimeString, Now.ToShortDateString & " " & Now.ToLongTimeString, CodeUtilisateur}
                If CreateFunction(data, Chef, "Modif") = True Then
                    ChargerGridFonction(Me.CodeService, CodeDivAdm)
                    MessageBox.Show("Fonction modifiée avec succès.", "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InitFormulaire()
                    CmbService_SelectedIndexChanged(sender, e)
                Else
                    FailMsg("Modification échouée.")
                End If

            Catch ex As Exception
                FailMsg("La mise à jour échouée." & vbNewLine & ex.ToString)
            End Try
        End If
    End Sub
    Private Sub BtSupp_Click(sender As System.Object, e As System.EventArgs) Handles BtSupp.Click
        If (ViewFonction.RowCount > 0) Then

            If ConfirmMsg("Voulez-vous vraiment supprimer?") = DialogResult.Yes Then

                DrX = ViewFonction.GetDataRow(ViewFonction.FocusedRowHandle)

                Dim RefFonction As Decimal = CInt(DrX(1).ToString())
                query = "delete from T_Fonction where RefFonction=" & RefFonction
                ExecuteNonQuery(query)

                query = "delete FROM `t_grh_travailler` where CodeService=" & RefFonction
                ExecuteNonQuery(query)

                InitFormulaire()
                ChargerGridFonction(Me.CodeService, CodeDivAdm)

            End If
        Else
            FailMsg("Suppression Impossible!")
        End If
    End Sub
    Private Sub CmbDivAdmin_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbDivAdmin.SelectedIndexChanged
        InitFormulaire()
        If CmbDivAdmin.SelectedIndex > -1 Then
            CodeDivAdm = "0"
            query = "select RefDecoupAdmin from T_DivisionAdministrative where LibelleDivision='" & EnleverApost(CmbDivAdmin.Text) & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                CodeDivAdm = rw(0).ToString
            Next
            ChargerService(CodeDivAdm)
        Else
            ChargerService(0)
        End If
        CmbService_SelectedIndexChanged(sender, e)
    End Sub
    Private Sub SupprimerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupprimerToolStripMenuItem.Click
        If ViewFonction.RowCount > 0 Then
            DrX = ViewFonction.GetDataRow(ViewFonction.FocusedRowHandle)

            Dim IDL = DrX(1).ToString
            ColorRowGrid(ViewFonction, "[Code]='x'", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewFonction, "[Ref]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)

            BtSupp.PerformClick()
        End If
    End Sub
    Private Sub Fonction_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        CmbDivAdmin.Focus()
    End Sub
    Private Sub BtAvantage_Click(sender As Object, e As EventArgs) Handles BtAvantage.Click
        'GRHAvantage.RefFonction = CInt(ViewFonction.GetFocusedDataRow(1))
        'GRHAvantage.gcAvantage.Text = "du poste de " & TxtFonction.Text
        'Dialog_form(GRHAvantage)
    End Sub
End Class