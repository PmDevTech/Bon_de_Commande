Imports System.IO
Imports DevExpress.Skins
Imports MySql.Data.MySqlClient

Public Class CodeAcces
    Dim loc As Point
    Dim ChemLogo As String = ""
    Dim ListBdname As New List(Of String)
    Dim ListCodeProjet As New List(Of String)

    Private Sub CodeAcces_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        'affectation des éléments du formulaire par les informations du connecté
        'Dim bytes() As Byte = System.Text.Encoding.UTF8.GetBytes("ABcd1234")
        'Dim hashOfBytes() As Byte = New System.Security.Cryptography.SHA1Managed().ComputeHash(bytes)
        'Dim strHash As String = Convert.ToBase64String(hashOfBytes)
        'hashOfBytes = Convert.FromBase64String(strHash)
        AccessWall = ""
        Try
            ChargerCmbProjet()
            PasswordTextBox.UseSystemPasswordChar = True
            Try
                If (ClearMdi.Visible = True) Then
                    ComboProjet.Text = ProjetEnCours
                    ComboProjet.Enabled = False
                    UsernameTextBox.Text = CodeUtilisateur
                    UsernameTextBox.Enabled = False
                    PasswordTextBox.Focus()
                    Cancel.Enabled = False
                    Cancel.Visible = False
                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception
            FailMsg("Problème de connexion à la Base de Données" & vbNewLine & ex.ToString)
        End Try
    End Sub
    Public Sub ChargerCmbProjet()
        query = "SHOW DATABASES " & DB
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        ComboProjet.Items.Clear()
        ListBdname.Clear()
        ListCodeProjet.Clear()
        For Each rw As DataRow In dt.Rows
            Dim BDName As String = rw(0)
            query = "SELECT CodeProjet, bdname FROM t_paramtechprojet"
            Try
                Dim dtBD As DataTable = ExcecuteSelectQuery(query, BDName)
                For Each rwBD As DataRow In dtBD.Rows
                    If ComboProjet.Items.IndexOf(rwBD("CodeProjet").ToString().ToUpper()) <> -1 Then
                        ComboProjet.Items.Add(Mid(rwBD("bdname").ToString(), 3).ToUpper())
                    Else
                        ComboProjet.Items.Add(rwBD("CodeProjet").ToString().ToUpper())
                    End If
                    ListBdname.Add(rwBD("bdname"))
                    ListCodeProjet.Add(rwBD("CodeProjet").ToString().ToUpper())
                Next
            Catch ex As Exception
            End Try
        Next
    End Sub
    Private Sub UsernameTextBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsernameTextBox.Click
        'Exécution d'une condition
        If UsernameTextBox.Text = "Nom Utilisateur" Then
            UsernameTextBox.Text = ""
        End If
    End Sub
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If ComboProjet.SelectedIndex = -1 Then
            Exit Sub
        End If
        Dim LeCode As String
        Try
            LeCode = ListCodeProjet(ComboProjet.SelectedIndex)
        Catch ex As Exception
            Exit Sub
        End Try
        Dim NbreLigne As Byte
        Dim Password As String
        If UsernameTextBox.Text <> "" And PasswordTextBox.Text <> "" Then
            Try

                'On vérifie l'existentiabilité des champs renseignés
                query = "SELECT Count(*) FROM T_Operateur WHERE UtilOperateur ='" & UsernameTextBox.Text & "'"
                'Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                'Dim DatAdapt = New MySqlDataAdapter(Cmd)
                'DatAdapt.Fill(DatSet, "T_Operateur")
                'Dim DatTable = DatSet.Tables("T_Operateur")
                'Dim DatRow = DatSet.Tables("T_Operateur").NewRow()
                NbreLigne = Val(ExecuteScallar(query))
                If NbreLigne > 0 Then
                    'recherche des informations du connecté lors du remplissage du login et du mot de passe
                    'DatSet = New DataSet
                    query = "SELECT MdpOperateur, NomOperateur, PrenOperateur, FonctionOperateur, UtilOperateur, AccesOperateur,CodeOperateur,EMP_ID FROM T_Operateur WHERE UtilOperateur ='" & UsernameTextBox.Text & "' and CodeProjet='" & LeCode & "'"
                    'Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                    'Dim DatAdapt = New MySqlDataAdapter(Cmd)
                    'DatAdapt.Fill(DatSet, "MdpOperateur, NomOperateur, PrenOperateur, FonctionOperateur, UtilOperateur, AccesOperateur,CodeOperateur")
                    'Dim DatTable = DatSet.Tables("T_Operateur")
                    Dim dt As DataTable = ExcecuteSelectQuery(query)
                    If dt.Rows.Count > 0 Then
                        Dim rw As DataRow = dt.Rows(0)
                        Password = rw(0)
                        NomUtilisateur = MettreApost(rw(1).ToString)
                        PrenUtilisateur = MettreApost(rw(2).ToString)
                        FonctionUtilisateur = MettreApost(rw(3).ToString)
                        CodeUtilisateur = rw(4)
                        NiveauAcces1 = rw(5)
                        CodeOperateurEnCours = rw("CodeOperateur")
                        CurrEmpId = rw("EMP_ID")
                        cur_User = rw("EMP_ID")

                        'rendre l'operateur actif
                        query = "update t_operateur set opinterne='1' where codeoperateur='" & CodeOperateurEnCours.ToString & "'"
                        ExecuteNonQuery(query)
                    Else
                        Password = ""
                        NomUtilisateur = ""
                        PrenUtilisateur = ""
                        FonctionUtilisateur = ""
                        CodeUtilisateur = ""
                        NiveauAcces1 = ""
                        CodeOperateurEnCours = -1
                        CurrEmpId = -1
                        cur_User = -1
                    End If
                    'Charger Access ***************
                    '
                    query = "select AttributGroup from T_GroupUtils where CodeGroup='" & NiveauAcces1 & "' and CodeProjet='" & LeCode & "'"
                    dt = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        AccessWall = rw(0).ToString
                    Next
                    '******************************

                    If Password = PasswordTextBox.Text Then
                        'on lui ouvre la connexion
                        If ComboProjet.SelectedIndex > -1 Then
                            ProjetEnCours = ListCodeProjet(ComboProjet.SelectedIndex)
                            '*************************************
                        Else
                            ProjetEnCours = "PAS DE PROJET"
                        End If

                        'EspionData(Me)
                        OkDate = True
                        If (ClearMdi.Visible = False) Then

                            If Not Directory.Exists(line) Then
                                FailMsg("Nous n'arrivons pas à récupérer le dossier racine du projet." & vbNewLine & "Veuillez contacter votre fournisseur svp.")
                                Exit Sub
                            End If

                            ClearMdi.Show()
                            SessionID = Now.Ticks.ToString()
                            Me.Close()
                        Else
                            TpsInactif = 0
                        End If

                    Else
                        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Hand) 'Beep()
                        'MsgBox("Mot de Passe Incorrect", MsgBoxStyle.Critical)
                        PasswordTextBox.Text = ""
                        PasswordTextBox.BackColor = Color.Red
                        PasswordTextBox.Focus()
                    End If

                Else
                    My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation) 'Beep()
                    'MsgBox("Identifiant Incorrect", MsgBoxStyle.Critical)
                End If


            Catch ex As Exception
                MsgBox(ex.ToString())
            End Try
        Else
            MsgBox("Veuillez remplir correctement les champs", MsgBoxStyle.Exclamation)

        End If
    End Sub
    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub PasswordTextBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PasswordTextBox.Click
        'exécution d'une condition
        If (PasswordTextBox.ReadOnly = True) Then
            UsernameTextBox.Focus()
        End If
    End Sub

    Private Sub PasswordTextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles PasswordTextBox.KeyDown
        'exécution d'une condition du clavier
        If (e.KeyCode = Keys.Enter) Then
            OK_Click(Me, e)
        End If
    End Sub

    Private Sub PasswordTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasswordTextBox.TextChanged
        'exécution d'une condition
        If PasswordTextBox.Text <> "" And UsernameTextBox.Text <> "" And UsernameTextBox.Text <> "Nom Utilisateur" Then
            OK.Enabled = True
        Else
            OK.Enabled = False
        End If
    End Sub

    Private Sub ComboProjet_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboProjet.SelectedIndexChanged
        'renseignement de la clé lors de la création d'un nouveau projet
        If ComboProjet.SelectedIndex = -1 Then
            Exit Sub
        End If

        Proj_mailHote = ""
        Proj_mailPort = ""
        Proj_mailSecur = False
        Proj_mailAuthent = False
        Proj_mailCompte = ""
        Proj_mailPasse = ""

        Proj_smsTerminal = ""
        Proj_smsVitesse = 0
        Proj_smsCodePin = ""
        Proj_smsEncodage = 0
        Proj_smsModele = ""
        Try
            database = ListBdname(ComboProjet.SelectedIndex)
            DB = database
        Catch ex As Exception
            Exit Sub
        End Try
        UsernameTextBox_TextChanged(UsernameTextBox, New EventArgs)

        LibelleProjet.Visible = True

        Dim LeCode As String
        Try
            LeCode = ListCodeProjet(ComboProjet.SelectedIndex)
        Catch ex As Exception
            Exit Sub
        End Try

        If Application.ProductName = "ClearProject" Or Application.ProductName = "ClearGestionPro" Or Application.ProductName = "ClearGestion" Then
            'Bailleurs de fonds
            Dim Bailleur As String = ""
            query = "select InitialeBailleur from T_Bailleur where CodeProjet='" & LeCode & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                Bailleur = Bailleur & rw(0).ToString & " / "
            Next
            Bailleur = Bailleur & "."
            Bailleur = Bailleur.Replace(" / .", "")
            LblBailleurs.Visible = True
            LblBailleurs.Text = MettreApost(Bailleur)

            ' Montant financement
            Dim MontProjet As Decimal = 0
            query = "select C.MontantConvention from T_Convention as C,T_Bailleur as B where C.CodeBailleur=B.CodeBailleur and B.CodeProjet='" & LeCode & "'"
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                MontProjet = MontProjet + CDec(rw(0))
            Next
            LblFinGlob.Visible = True
            LblFinGlob.Text = AfficherMonnaie(MontProjet.ToString) & " FCFA"
        End If

        Souligne.Visible = True
        Dim ServMail As String = ""

        query = "select A.NomProjet, A.IdentifiantProjet, A.NomProjet, A.LogoProjet, A.CodeProjet, B.RacineDocument, B.Serv_Nom, "
        query &= "B.Mail_Account, B.Mail_PassWord, B.Sms_Terminal, B.Sms_Vitesse, B.Sms_Pin, B.Sms_Encodage, B.Sms_Modele, A.LogoImage, B.RacineEtat from T_Projet as A, T_ParamTechProjet as B where A.CodeProjet=B.CodeProjet And A.CodeProjet='" & LeCode & "'"
        dt = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            Dim rw As DataRow = dt.Rows(0)
            LibelleProjet.Text = MettreApost(rw("NomProjet").ToString)
            IdentifiantProjetEnCours = rw("IdentifiantProjet").ToString
            NomProjetEnCours = MettreApost(rw("NomProjet").ToString)

            line = rw("RacineDocument").ToString
            lineEtat = rw("RacineEtat").ToString
            ChemLogo = rw("LogoProjet").ToString
            PbLogoProjet.Image = Bitmap.FromStream(New MemoryStream(CType(rw("LogoImage"), Byte())))

            ServMail = rw("Serv_Nom").ToString
            Proj_mailPasse = rw("Mail_PassWord").ToString
            Proj_mailCompte = rw("Mail_Account").ToString
            Proj_smsTerminal = rw("Sms_Terminal").ToString
            Proj_smsModele = rw("Sms_Modele").ToString

            If (rw("Sms_Vitesse").ToString <> "") Then
                If (rw("Sms_Vitesse").ToString = "Défaut") Then
                    Proj_smsVitesse = 0
                Else
                    Proj_smsVitesse = CInt(rw("Sms_Vitesse"))
                End If

            End If
            Proj_smsCodePin = rw("Sms_Pin").ToString
            If (rw("Sms_Encodage").ToString <> "") Then Proj_smsEncodage = CInt(rw("Sms_Encodage"))
        Else
            FailMsg("Dossier introuvable.")
            Me.Close()
        End If


        If (ServMail <> "") Then
            query = "select Serv_Hote, Serv_Port, Serv_Secur, Serv_Authent from T_ParamMailServeur where Serv_Nom='" & ServMail & "'"
            dt = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                Proj_mailHote = dt.Rows(0).Item("Serv_Hote").ToString
                Proj_mailPort = dt.Rows(0).Item("Serv_Port").ToString
                Proj_mailSecur = IIf(dt.Rows(0).Item("Serv_Secur").ToString = "O", True, False)
                Proj_mailAuthent = IIf(dt.Rows(0).Item("Serv_Authent").ToString = "O", True, False)
            End If
        End If
        UsernameTextBox.Visible = True
        UsernameTextBox.Focus()
    End Sub

    Private Sub UsernameTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsernameTextBox.TextChanged
        Dim Trouver As Boolean = False
        Dim Actif As Boolean = False
        If ComboProjet.SelectedIndex = -1 Then
            Exit Sub
        End If
        Dim LeCode As String
        Try
            LeCode = ListCodeProjet(ComboProjet.SelectedIndex)
        Catch ex As Exception
            Exit Sub
        End Try
        query = "SELECT NomOperateur, PrenOperateur, FonctionOperateur, CodeSkin, DebutAccesOperateur, FinAccesOperateur FROM T_Operateur WHERE UtilOperateur ='" & EnleverApost(UsernameTextBox.Text) & "' and CodeProjet='" & LeCode & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            Dim rw As DataRow = dt.Rows(0)
            Dim NomComp As String = rw("NomOperateur").ToString & " " & rw("PrenOperateur").ToString
            TxtNomComplet.Visible = True
            If MettreApost(rw("FonctionOperateur").ToString).Length = 0 Then
                TxtNomComplet.Text = MettreApost(NomComp)
            Else
                TxtNomComplet.Text = MettreApost(NomComp) & vbNewLine & "(" & MettreApost(rw("FonctionOperateur").ToString) & ")"
            End If
            Trouver = True

            If (rw("DebutAccesOperateur").ToString <> "" And rw("FinAccesOperateur").ToString <> "") Then
                If (DateTime.Compare(CDate(rw("DebutAccesOperateur").ToString), Now.ToShortDateString) <= 0 And DateTime.Compare(CDate(rw("FinAccesOperateur").ToString), Now.ToShortDateString) >= 0) Then
                    Actif = True
                End If
            ElseIf (rw("FinAccesOperateur").ToString <> "") Then
                If (DateTime.Compare(CDate(rw("DebutAccesOperateur").ToString), Now.ToShortDateString) <= 0) Then
                    Actif = True
                End If
            ElseIf (rw("FinAccesOperateur").ToString = "" And rw("FinAccesOperateur").ToString = "") Then
                Actif = True
            Else
                Actif = False
            End If

            query = "SELECT LibelleSkin from T_OperateurSkin where CodeSkin='" & rw("CodeSkin").ToString & "'"
            dt = ExcecuteSelectQuery(query)
            For Each rwx As DataRow In dt.Rows
                SkinActu = rwx("LibelleSkin").ToString
            Next

        End If

        TxtInactif.Visible = False
        If (Trouver = True) Then
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
            TxtNomComplet.BackColor = Color.GreenYellow

            If (Actif = True) Then
                PasswordTextBox.Visible = True
                chkPassword.Visible = True
                PasswordTextBox.ReadOnly = False
                PasswordTextBox.Focus()
            Else
                PasswordTextBox.Visible = False
                chkPassword.Visible = False
                TxtInactif.Visible = True
            End If

        Else
            TxtNomComplet.Text = ""
            TxtNomComplet.BackColor = Color.White
            TxtNomComplet.Visible = False
            PasswordTextBox.Visible = False
            chkPassword.Visible = False
        End If
    End Sub

    Private Sub chkPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkPassword.CheckedChanged
        If chkPassword.Checked Then
            PasswordTextBox.UseSystemPasswordChar = False
            PasswordTextBox.Select()
        Else
            PasswordTextBox.UseSystemPasswordChar = True
            PasswordTextBox.Select()
            'PasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(88)
        End If
    End Sub
    Private Sub CodeAcces_MouseMove(sender As Object, e As MouseEventArgs) Handles LblBailleurs.MouseMove, LblFinGlob.MouseMove, MyBase.MouseMove, PbLogoProjet.MouseMove, Label1.MouseMove
        If e.Button = MouseButtons.Left Then
            Dim newLoc As Point
            newLoc.X = Location.X + (e.X - loc.X)
            newLoc.Y = Location.Y + (e.Y - loc.Y)
            Location = newLoc
        End If
    End Sub
    Private Sub CodeAcces_MouseDown(sender As Object, e As MouseEventArgs) Handles LblBailleurs.MouseDown, LblFinGlob.MouseDown, MyBase.MouseDown, PbLogoProjet.MouseDown, Label1.MouseDown
        loc = New Point(e.X, e.Y)
    End Sub

End Class
