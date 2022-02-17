Imports MySql.Data.MySqlClient
Imports System.IO
Public Class AccesEtOperateur
    Dim dtOper = New DataTable()
    Dim DrX As DataRow
    Public LicenceKey As String = String.Empty
    Dim ForEmploye As Boolean = True
    Private Sub AccesEtOperateur_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        InitFormulaire()
        If DB.Length > 3 Then
            Dim Init1 As String = Mid(DB, 1, 2)
            Dim Init2 As String = Mid(DB, 1, 3)
            If Init1 = "bd" Or Init2 = "gspro" Or Init2 = "gst" Or Init2 = "crh" Then
                LierLeCompteÀUnEmployéToolStripMenuItem.Visible = True
                ForEmploye = True
            Else
                LierLeCompteÀUnEmployéToolStripMenuItem.Visible = False
                ForEmploye = False
            End If
        End If
        ChargerOperateur()
        ChargerGroupe()
        BtPhoto.Visible = True
    End Sub

    Private Sub ChargerOperateur()
        dtOper.Columns.Clear()
        dtOper.Columns.Add("CodeX", Type.GetType("System.String"))
        dtOper.Columns.Add("CodeOp", Type.GetType("System.String"))
        dtOper.Columns.Add("Nom et prénoms", Type.GetType("System.String"))
        dtOper.Columns.Add("Date nais.", Type.GetType("System.String"))
        dtOper.Columns.Add("Lieu nais.", Type.GetType("System.String"))
        dtOper.Columns.Add("Nationalité", Type.GetType("System.String"))
        dtOper.Columns.Add("Adresse", Type.GetType("System.String"))
        dtOper.Columns.Add("Contact", Type.GetType("System.String"))
        If ForEmploye Then
            dtOper.Columns.Add("Employé", Type.GetType("System.Boolean"))
        End If
        dtOper.Columns.Add("E-mail", Type.GetType("System.String"))
        dtOper.Columns.Add("Groupe utilisateurs", Type.GetType("System.String"))
        dtOper.Columns.Add("Mise en service", Type.GetType("System.String"))
        dtOper.Columns.Add("Fermeture", Type.GetType("System.String"))
        dtOper.Columns.Add("Photo", Type.GetType("System.String"))
        dtOper.Columns.Add("Type", Type.GetType("System.String"))
        dtOper.Columns.Add("Spécialite", Type.GetType("System.String"))
        dtOper.Columns.Add("Login", Type.GetType("System.String"))
        dtOper.Rows.Clear()

        Dim NbTotal As Decimal = 0
        query = "select * from T_Operateur where CodeProjet='" & ProjetEnCours & "' order by NomOperateur,PrenOperateur"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows

            NbTotal += 1
            Dim drS = dtOper.NewRow()

            drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
            drS(1) = rw("CodeOperateur").ToString
            drS(2) = MettreApost(rw("CiviliteOperateur").ToString & " " & rw("NomOperateur").ToString & " " & rw("PrenOperateur").ToString)
            drS(3) = rw("NaisOperateur").ToString
            drS(4) = MettreApost(rw("LieuNaisOperateur").ToString)
            drS(5) = MettreApost(rw("NationaliteOperateur").ToString)
            drS(6) = MettreApost(rw("AdresseOperateur").ToString)
            drS(7) = rw("TelOperateur").ToString
            If ForEmploye Then
                drS(8) = IIf(rw("EMP_ID").ToString() = "-1", False, True)
                drS(9) = rw("MailOperateur").ToString
                drS(10) = rw("AccesOperateur").ToString
                drS(11) = rw("DebutAccesOperateur").ToString
                drS(12) = rw("FinAccesOperateur").ToString
                drS(13) = rw("PhotoOperateur").ToString
                drS(14) = MettreApost(rw("TypeOperateur").ToString)
                drS(15) = MettreApost(rw("FonctionOperateur").ToString)
                drS(16) = MettreApost(rw("UtilOperateur").ToString)
            Else
                drS(8) = rw("MailOperateur").ToString
                drS(9) = rw("AccesOperateur").ToString
                drS(10) = rw("DebutAccesOperateur").ToString
                drS(11) = rw("FinAccesOperateur").ToString
                drS(12) = rw("PhotoOperateur").ToString
                drS(13) = MettreApost(rw("TypeOperateur").ToString)
                drS(14) = MettreApost(rw("FonctionOperateur").ToString)
                drS(15) = MettreApost(rw("UtilOperateur").ToString)
            End If

            dtOper.Rows.Add(drS)

        Next


        GridOperateur.DataSource = dtOper

        If ViewOperateur.Columns("CodeX").Visible = True Then
            ViewOperateur.Columns("CodeX").Visible = False
            ViewOperateur.Columns("CodeOp").Visible = False
            ViewOperateur.Columns("Nom et prénoms").Width = 350
            ViewOperateur.Columns("Date nais.").Width = 100
            ViewOperateur.Columns("Lieu nais.").Width = 150
            ViewOperateur.Columns("Nationalité").Width = 150
            ViewOperateur.Columns("Adresse").Width = 150
            ViewOperateur.Columns("Contact").Width = 100
            ViewOperateur.Columns("E-mail").Width = 200
            ViewOperateur.Columns("Groupe utilisateurs").Width = 200
            ViewOperateur.Columns("Mise en service").Width = 150
            ViewOperateur.Columns("Fermeture").Width = 100
            ViewOperateur.Columns("Photo").Width = 100
            ViewOperateur.Columns("Type").Visible = False
            ViewOperateur.Columns("Login").Visible = False
            ViewOperateur.Columns("Type").Width = 100
            ViewOperateur.Columns("Spécialite").Width = 100
            If ForEmploye Then
                ViewOperateur.Columns("Employé").Width = 60
                ViewOperateur.Columns("Employé").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
            End If
            ViewOperateur.Columns("Date nais.").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewOperateur.Columns("Contact").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewOperateur.Columns("Mise en service").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewOperateur.Columns("Fermeture").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            ViewOperateur.Columns("Nom et prénoms").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

            ViewOperateur.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
            ColorRowGrid(ViewOperateur, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
        End If

    End Sub

    Private Sub GbOperateur_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GbOperateur.VisibleChanged

        If (GbOperateur.Visible = True) Then
            PnlAjout.Enabled = False
        Else
            PnlAjout.Enabled = True
        End If

    End Sub

    Private Sub BtAccesEmploye_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtNewUser.Click
        Dim NewUser As New LicenceUtilisateur
        Dialog_form(NewUser)
        If NewUser.DialogResult = DialogResult.OK Then
            LicenceKey = NewUser.txtKey.Text
            GbOperateur.Visible = True
        Else
            LicenceKey = String.Empty
        End If
    End Sub

    Private Sub ChargerGroupe()
        'CodeGroup not in ('Administrateur','Niveau0') and 
        query = "select CodeGroup from T_GroupUtils where CodeProjet='" & ProjetEnCours & "' order by CodeGroup"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        CmbGroup.Properties.Items.Clear()
        For Each rw As DataRow In dt.Rows
            CmbGroup.Properties.Items.Add(MettreApost(rw(0)).ToString)
        Next

    End Sub

    Private Sub InitFormulaire()

        PnlAjout.Enabled = True
        GbOperateur.Visible = False
        CmbCiv.Text = ""
        TxtNom.Text = ""
        TxtPrenom.Text = ""
        DtNaiss.Text = ""
        TxtLieuNais.Text = ""
        TxtNationalite.Text = ""
        TxtAdresse.Text = ""
        TxtTelFax.Text = ""
        TxtMail.Text = ""
        CmbGroup.Text = ""
        DtOuverture.Text = ""
        DtFermeture.Text = ""
        TxtCodOp.Text = ""
        ChkModif.Checked = False
        PnlIdentite.Enabled = True
        PhotoOperateur.Image = Nothing
        TxtPhoto.Text = ""
        TxtCheminImage.Text = ""
        cmbTypeUtilisateur.ResetText()
        txtSpecialite.ResetText()
        TxtLogin.ResetText()

    End Sub

    Private Sub BtQuitter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        InitFormulaire()
    End Sub

    Private Sub BtEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click

        If (CmbCiv.SelectedIndex > -1 And TxtNom.Text.Trim() <> "" And TxtPrenom.Text.Trim() <> "" And CmbGroup.SelectedIndex > -1 And cmbTypeUtilisateur.SelectedIndex > -1 And txtSpecialite.Text.Trim().Length > 0) Then


            If (ChkModif.Checked = False) Then
                If (TxtPhoto.Text <> "" And TxtCheminImage.Text <> "") Then
                    File.Copy(TxtCheminImage.Text, line & "\Photos\" & TxtPhoto.Text, True)

                    'Dim monImage As String = line & "\Photos\" & TxtPhoto.Text

                    'If File.Exists(monImage) Then
                    '    Dim fichier As FileStream = New FileStream(monImage, FileMode.Open)
                    '    PhotoOperateur.Image = Image.FromStream(fichier)
                    '    If TxtCodOp.Text = CodeOperateurEnCours Then
                    '        ClearMdi.PictureEdit1.Image = Image.FromStream(fichier)
                    '    End If
                    '    fichier.Close()
                    'Else
                    '    PhotoOperateur.Image = Image.FromFile(line & "\employe.png")
                    'End If

                    'Dim ext As String = New System.IO.FileInfo(TxtCheminImage.Text).Extension
                    'File.Copy(TxtCheminImage.Text, line & "\Photos\" & TxtPhoto.Text & ext, True)
                End If

                DebutChargement(True, "Création du compte en cours")
                Dim DatSet = New DataSet
                'Ajout de l'utilisateur en ligne
                query = "SELECT MAX(CONVERT(CodeOperateur,UNSIGNED INTEGER)) As Max FROM t_operateur"
                Dim NewUser As Decimal = 0
                Try
                    NewUser = Val(LicenceUtilisateur.ExecuteScalarServer(query))
                Catch ex As Exception
                    FinChargement()
                    FailMsg("Impossible de se connecter au serveur de licence")
                    Exit Sub
                End Try
                NewUser += 1
                query = "select * from T_Operateur"
                Dim ConServer As New MySqlConnection
                If Not ConnecteServer(ConServer, True) Then
                    FinChargement()
                    FailMsg("Impossible de se connecter au serveur de licence")
                    Exit Sub
                End If
                Dim Cmd As MySqlCommand = New MySqlCommand(query, ConServer)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "T_Operateur")
                Dim DatTable = DatSet.Tables("T_Operateur")
                Dim DatRow = DatSet.Tables("T_Operateur").NewRow()
                Dim LoginUser As String = TxtLogin.Text
                Dim MotDePasse As String = GenererCode(8)
                Dim Type As String = String.Empty
                If cmbTypeUtilisateur.SelectedIndex > -1 Then
                    Type = cmbTypeUtilisateur.Text
                End If
                Dim Spec As String = txtSpecialite.Text

                Try
                    DatRow("CodeOperateur") = NewUser
                    DatRow("EMP_ID") = -1
                    DatRow("CiviliteOperateur") = CmbCiv.Text
                    DatRow("NomOperateur") = EnleverApost(TxtNom.Text)
                    DatRow("PrenOperateur") = EnleverApost(TxtPrenom.Text)
                    DatRow("MatriculeOperateur") = ""
                    DatRow("FonctionOperateur") = EnleverApost(Spec)
                    DatRow("TypeOperateur") = EnleverApost(Type)
                    DatRow("StatutOperateur") = ""
                    DatRow("OpInterne") = ""
                    DatRow("ChangeMDP") = ""
                    DatRow("MatrimoniOperateur") = ""
                    DatRow("codeService") = "0"
                    DatRow("CodeSkin") = "S20"
                    DatRow("licence_key") = LicenceKey
                    If (DtNaiss.Text <> "") Then
                        DatRow("NaisOperateur") = DtNaiss.DateTime.ToShortDateString
                    Else
                        DatRow("NaisOperateur") = Now.ToShortDateString
                    End If
                    DatRow("PhotoOperateur") = TxtPhoto.Text
                    DatRow("LieuNaisOperateur") = EnleverApost(TxtLieuNais.Text)
                    DatRow("NationaliteOperateur") = EnleverApost(TxtNationalite.Text)
                    DatRow("TelOperateur") = TxtTelFax.Text
                    DatRow("AdresseOperateur") = EnleverApost(TxtAdresse.Text)
                    DatRow("MailOperateur") = EnleverApost(TxtMail.Text)
                    If (DtOuverture.Text <> "") Then
                        DatRow("DebutAccesOperateur") = DtOuverture.DateTime.ToShortDateString
                    Else
                        If CmbGroup.Text = "Administrateur" Then
                            DatRow("DebutAccesOperateur") = ""
                        Else
                            DatRow("DebutAccesOperateur") = Now.ToShortDateString
                        End If
                    End If
                    If (DtFermeture.Text <> "") Then
                        DatRow("FinAccesOperateur") = DtFermeture.DateTime.ToShortDateString
                    Else
                        If CmbGroup.Text = "Administrateur" Then
                            DatRow("FinAccesOperateur") = ""
                        Else
                            DatRow("FinAccesOperateur") = Now.ToShortDateString
                        End If
                    End If
                    DatRow("UtilOperateur") = LoginUser
                    DatRow("MdpOperateur") = MotDePasse
                    DatRow("AccesOperateur") = EnleverApost(CmbGroup.Text)
                    DatRow("DateSaisie") = Now.ToShortDateString & " " & Now.ToLongTimeString
                    DatRow("DateModif") = Now.ToShortDateString & " " & Now.ToLongTimeString
                    DatRow("Operateur") = CodeUtilisateur
                    DatRow("CodeProjet") = ProjetEnCours
                Catch ex As Exception
                    FinChargement()
                    FailMsg("Impossible de se connecter au serveur de licence")
                    Exit Sub
                End Try

                DatSet.Tables("T_Operateur").Rows.Add(DatRow)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                Try
                    DatAdapt.Update(DatSet, "T_Operateur")
                Catch ex As Exception
                    FailMsg("Impossible de se connecter au serveur de licence")
                    Exit Sub
                End Try
                DatSet.Clear()

                'Ajout de l'utilisateur en local
                DatSet = New DataSet
                query = "select * from T_Operateur"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Cmd = New MySqlCommand(query, sqlconn)
                DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "T_Operateur")
                DatTable = DatSet.Tables("T_Operateur")
                DatRow = DatSet.Tables("T_Operateur").NewRow()

                DatRow("EMP_ID") = -1
                DatRow("licence_key") = LicenceKey
                DatRow("CiviliteOperateur") = CmbCiv.Text
                DatRow("NomOperateur") = EnleverApost(TxtNom.Text)
                DatRow("PrenOperateur") = EnleverApost(TxtPrenom.Text)
                DatRow("MatriculeOperateur") = ""
                DatRow("FonctionOperateur") = EnleverApost(Spec)
                DatRow("TypeOperateur") = EnleverApost(Type)
                DatRow("StatutOperateur") = ""
                DatRow("OpInterne") = ""
                DatRow("ChangeMDP") = ""
                DatRow("MatrimoniOperateur") = ""
                DatRow("codeService") = "0"
                DatRow("CodeSkin") = "S20"
                If (DtNaiss.Text <> "") Then
                    DatRow("NaisOperateur") = DtNaiss.DateTime.ToShortDateString
                Else
                    DatRow("NaisOperateur") = Now.ToShortDateString
                End If
                DatRow("PhotoOperateur") = TxtPhoto.Text
                DatRow("LieuNaisOperateur") = EnleverApost(TxtLieuNais.Text)
                DatRow("NationaliteOperateur") = EnleverApost(TxtNationalite.Text)
                DatRow("TelOperateur") = TxtTelFax.Text
                DatRow("AdresseOperateur") = EnleverApost(TxtAdresse.Text)
                DatRow("MailOperateur") = EnleverApost(TxtMail.Text)
                If (DtOuverture.Text <> "") Then
                    DatRow("DebutAccesOperateur") = DtOuverture.DateTime.ToShortDateString
                Else
                    If CmbGroup.Text = "Administrateur" Then
                        DatRow("DebutAccesOperateur") = ""
                    Else
                        DatRow("DebutAccesOperateur") = Now.ToShortDateString
                    End If
                End If
                If (DtFermeture.Text <> "") Then
                    DatRow("FinAccesOperateur") = DtFermeture.DateTime.ToShortDateString
                Else
                    If CmbGroup.Text = "Administrateur" Then
                        DatRow("FinAccesOperateur") = ""
                    Else
                        DatRow("FinAccesOperateur") = Now.ToShortDateString
                    End If
                End If
                DatRow("UtilOperateur") = LoginUser
                DatRow("MdpOperateur") = MotDePasse
                DatRow("AccesOperateur") = EnleverApost(CmbGroup.Text)
                DatRow("DateSaisie") = Now.ToShortDateString & " " & Now.ToLongTimeString
                DatRow("DateModif") = Now.ToShortDateString & " " & Now.ToLongTimeString
                DatRow("Operateur") = CodeUtilisateur
                DatRow("CodeProjet") = ProjetEnCours

                DatSet.Tables("T_Operateur").Rows.Add(DatRow)
                CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                Try
                    DatAdapt.Update(DatSet, "T_Operateur")
                Catch ex As Exception
                    FailMsg(ex.ToString)
                End Try
                DatSet.Clear()
                BDQUIT(sqlconn)

                query = "UPDATE t_licences_users SET ResteUser=ResteUser-1 WHERE licence_key='" & LicenceKey & "' AND CodeProjet='" & ProjetEnCours & "'"
                Try
                    LicenceUtilisateur.ExecuteNonQueryServer(query)
                Catch ex As Exception
                    'FailMsg("Impossible de se connecter au serveur de licence")
                    'Exit Sub
                End Try

                FinChargement()
                SuccesMsg("Compte créé avec succès." & vbNewLine & "Login : " & LoginUser & vbNewLine & "Mot de passe : " & MotDePasse)

            Else
                If (TxtPhoto.Text <> "" And TxtCheminImage.Text <> "") Then
                    Dim ext As String = New FileInfo(TxtCheminImage.Text).Extension
                    File.Copy(TxtCheminImage.Text, line & "\Photos\" & TxtPhoto.Text, True)

                    Dim monImage As String = line & "\Photos\" & TxtPhoto.Text

                    If File.Exists(monImage) Then
                        Dim fichier As FileStream = New FileStream(monImage, FileMode.Open)
                        PhotoOperateur.Image = Image.FromStream(fichier)
                        If TxtCodOp.Text = CodeOperateurEnCours Then
                            ClearMdi.PictureEdit1.Image = Image.FromStream(fichier)
                        End If
                        fichier.Close()
                    Else
                        PhotoOperateur.Image = Image.FromFile(line & "\employe.png")
                    End If
                End If

                Dim Type As String = String.Empty
                If cmbTypeUtilisateur.SelectedIndex > -1 Then
                    Type = cmbTypeUtilisateur.Text
                End If
                Dim Spec As String = txtSpecialite.Text

                If (PnlIdentite.Enabled = True) Then

                    If (DtOuverture.Text <> "" And DtFermeture.Text <> "") Then
                        query = "UPDATE t_operateur SET DebutAccesOperateur='" & DtOuverture.DateTime.ToShortDateString & "',FinAccesOperateur='" & DtFermeture.DateTime.ToShortDateString & "' WHERE CodeOperateur='" & TxtCodOp.Text & "'"
                        ExecuteNonQuery(query)
                    ElseIf (DtOuverture.Text <> "" Or DtFermeture.Text <> "") Then
                        SuccesMsg("Veuillez entrer correctement la période d'accès du compte.")
                        Exit Sub
                    Else
                        query = "UPDATE t_operateur SET DebutAccesOperateur='',FinAccesOperateur='' WHERE CodeOperateur='" & TxtCodOp.Text & "'"
                        ExecuteNonQuery(query)
                    End If

                    query = "update T_Operateur set CiviliteOperateur='" & CmbCiv.Text & "',NomOperateur='" & EnleverApost(TxtNom.Text) & "',PrenOperateur='" & EnleverApost(TxtPrenom.Text) & "',LieuNaisOperateur='" & EnleverApost(TxtLieuNais.Text) & "',NationaliteOperateur='" & EnleverApost(TxtNationalite.Text) & "',TelOperateur='" & TxtTelFax.Text & "',AdresseOperateur='" & EnleverApost(TxtAdresse.Text) & "',MailOperateur='" & TxtMail.Text & "',AccesOperateur='" & EnleverApost(CmbGroup.Text) & "',DateModif='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "', FonctionOperateur='" & EnleverApost(Spec) & "', TypeOperateur='" & EnleverApost(Type) & "' where CodeOperateur='" & TxtCodOp.Text & "'"
                    ExecuteNonQuery(query)
                    If DtNaiss.Text.Length > 0 Then
                        query = "UPDATE t_operateur SET NaisOperateur='" & DtNaiss.DateTime.ToShortDateString & "' WHERE CodeOperateur='" & TxtCodOp.Text & "'"
                        ExecuteNonQuery(query)
                    Else
                        query = "UPDATE t_operateur SET NaisOperateur='' WHERE CodeOperateur='" & TxtCodOp.Text & "'"
                        ExecuteNonQuery(query)
                    End If

                End If

            End If

            InitFormulaire()
            ChargerOperateur()

        Else
            SuccesMsg("Veuillez renseigner tous les champs obligatoires.")
        End If


    End Sub
    Public Function GetNewLogin(ByVal Nom As String, ByVal Prenom As String) As String
        Dim Login As String = String.Empty
        Try
            Login = Mid(Nom.Trim, 1, 1) & Split(Prenom.Trim(), " "c)(0).Replace("'", "").Replace("/", "").Replace("\", "")
            query = "select UtilOperateur from T_Operateur where UtilOperateur ='" & Login & "'"
            Dim dts As DataTable = ExcecuteSelectQuery(query)
            Dim Trouver As Decimal = dts.Rows.Count
            Dim cpte As Decimal = 1

            While Trouver <> 0
                Login = Mid(Nom.Trim, 1, 1) & Split(Prenom.Trim(), " "c)(0).Replace("'", "").Replace("/", "").Replace("\", "") & cpte
                query = "select UtilOperateur from T_Operateur where UtilOperateur ='" & Login & "'"
                dts = ExcecuteSelectQuery(query)
                Trouver = dts.Rows.Count
                cpte += 1
            End While
        Catch ex As Exception
        End Try
        Return Login
    End Function
    Private Sub GridOperateur_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridOperateur.DoubleClick

        InitFormulaire()
        If (ViewOperateur.RowCount > 0 And GbOperateur.Visible = False) Then
            DrX = ViewOperateur.GetDataRow(ViewOperateur.FocusedRowHandle)
            ColorRowGrid(ViewOperateur, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewOperateur, "[CodeOp]='" & TxtCodOp.Text & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)

            TxtCodOp.Text = DrX("CodeOp").ToString
            NomPrenoms()

            TxtPhoto.Text = DrX("Photo").ToString
            Dim monImage As String = line & "\Photos\" & DrX("Photo").ToString

            If File.Exists(monImage) Then
                Dim fichier As FileStream = New FileStream(monImage, FileMode.Open)
                PhotoOperateur.Image = Image.FromStream(fichier)
                fichier.Close()
            Else
                TxtPhoto.Text = Now.ToString("yyyyMMddHHmmss") & ".png"
                query = "UPDATE t_operateur SET PhotoOperateur='" & TxtPhoto.Text & "' WHERE CodeOperateur='" & TxtCodOp.Text & "'"
                ExecuteNonQuery(query)
                ViewOperateur.SetRowCellValue(ViewOperateur.FocusedRowHandle, "Photo", TxtPhoto.Text)
                PhotoOperateur.Image = Image.FromFile(line & "\employe.png")
            End If


            DtNaiss.Text = DrX("Date nais.").ToString
            TxtLieuNais.Text = DrX("Lieu nais.").ToString
            TxtNationalite.Text = DrX("Nationalité").ToString
            TxtAdresse.Text = DrX("Adresse").ToString
            TxtTelFax.Text = DrX("Contact").ToString
            TxtMail.Text = DrX("E-mail").ToString
            CmbGroup.Text = DrX("Groupe utilisateurs").ToString
            DtOuverture.Text = DrX("Mise en service").ToString
            DtFermeture.Text = DrX("Fermeture").ToString
            cmbTypeUtilisateur.Text = DrX("Type")
            txtSpecialite.Text = DrX("Spécialite")
            TxtLogin.Text = DrX("Login")

            If Not PnlIdentite.Visible Then PnlIdentite.Visible = True
            ChkModif.Checked = True
            GbOperateur.Visible = True


        End If

    End Sub
    Private Sub NomPrenoms()

        query = "select CiviliteOperateur, NomOperateur, PrenOperateur from T_Operateur where CodeOperateur='" & TxtCodOp.Text & "'"
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        For Each rw0 As DataRow In dt1.Rows
            CmbCiv.Text = rw0(0).ToString
            TxtNom.Text = MettreApost(rw0(1).ToString)
            TxtPrenom.Text = MettreApost(rw0(2).ToString)
        Next

    End Sub
    Private Sub BtPhoto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtPhoto.Click

        Dim dlg As New OpenFileDialog
        dlg.FileName = String.Empty
        dlg.Filter = "Images|*.jpg;*.jpeg;*.png;*.gif;*.bmp"

        If (dlg.ShowDialog() = DialogResult.OK) Then

            Dim fichier1 As String = dlg.FileName
            TxtCheminImage.Text = fichier1

            Dim ExtImg As String = ExtensionImage(fichier1)
            Dim nomPhoto As String = ""
            Dim fichier As FileStream = New FileStream(dlg.FileName, FileMode.Open)
            PhotoOperateur.Image = Image.FromStream(fichier)
            fichier.Close()
            If (ChkModif.Checked = False) Then
                nomPhoto = Now.ToString("yyyyMMddHHmmss")
                TxtPhoto.Text = nomPhoto & ExtImg
            End If

        End If


    End Sub

    'Private Sub SupprimerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupprimerToolStripMenuItem.Click
    '    If ViewOperateur.RowCount > 0 Then
    '        If ViewOperateur.GetFocusedRow > -1 Then
    '            If ConfirmMsg("Voulez-vous vraiment supprimer?") = DialogResult.Yes Then
    '                DrX = ViewOperateur.GetDataRow(ViewOperateur.FocusedRowHandle)

    '                query = "DELETE FROM T_Operateur WHERE CodeOperateur = '" & DrX(1).ToString & "'"
    '                ExecuteNonQuery(query)

    '                SuccesMsg("Suppression effectuée avec succès.")
    '                TxtNom.Text = ""
    '                TxtPrenom.Text = ""
    '                DtNaiss.Text = ""
    '                TxtLieuNais.Text = ""
    '                TxtNationalite.Text = ""
    '                TxtAdresse.Text = ""
    '                TxtTelFax.Text = ""
    '                TxtMail.Text = ""
    '                CmbGroup.Text = ""
    '                DtOuverture.Text = ""
    '                DtFermeture.Text = ""
    '                ChargerOperateur()
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub GridOperateur_Click(sender As System.Object, e As System.EventArgs) Handles GridOperateur.Click
        Try
            If ViewOperateur.RowCount > 0 Then
                DrX = ViewOperateur.GetDataRow(ViewOperateur.FocusedRowHandle)

                Dim IDL = DrX("CodeOp").ToString
                ColorRowGrid(ViewOperateur, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
                ColorRowGridAnal(ViewOperateur, "[CodeOp]='" & IDL & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)

                ViewOperateur.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If ViewOperateur.RowCount <= 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub ReinitialiserPassToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReinitialiserPassToolStripMenuItem.Click
        If ViewOperateur.RowCount > 0 Then
            DrX = ViewOperateur.GetDataRow(ViewOperateur.FocusedRowHandle)
            TxtCodOp.Text = DrX("CodeOp").ToString

            ColorRowGrid(ViewOperateur, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewOperateur, "[CodeOp]='" & TxtCodOp.Text & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)

            If ConfirmMsg("Voulez-vous réinitialiser le mot de passe de cet utilisateur?") = DialogResult.Yes Then
                Dim NewPassword As String = GenererCode(8)
                query = "update T_Operateur set MdpOperateur='" & NewPassword & "' where CodeOperateur='" & TxtCodOp.Text & "'"
                ExecuteNonQuery(query)
                SuccesMsg("Mot de passe réinitialisé." & vbNewLine & "Nouveau mot de passe : " & NewPassword)
            End If
        End If
    End Sub

    Private Sub TxtNom_TextChanged(sender As Object, e As EventArgs) Handles TxtPrenom.TextChanged, TxtNom.TextChanged
        If Not ChkModif.Checked Then
            TxtLogin.Text = GetNewLogin(TxtNom.Text, TxtPrenom.Text)
        End If
    End Sub

    Private Sub LierLeCompteÀUnEmployéToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LierLeCompteÀUnEmployéToolStripMenuItem.Click
        DrX = ViewOperateur.GetDataRow(ViewOperateur.FocusedRowHandle)
        TxtCodOp.Text = DrX("CodeOp").ToString

        ColorRowGrid(ViewOperateur, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
        ColorRowGridAnal(ViewOperateur, "[CodeOp]='" & TxtCodOp.Text & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)
        Dim NewLiaisonEmploye As New LierCompteEmploye
        DrX = ViewOperateur.GetDataRow(ViewOperateur.FocusedRowHandle)
        NewLiaisonEmploye.OperateurID = DrX("CodeOp")
        Dim NomPrenoms As String = ExecuteScallar("SELECT CONCAT(EMP_MAT,' ',EMP_NOM,' ',EMP_PRENOMS) As NomPren FROM t_grh_employe WHERE EMP_ID=(SELECT EMP_ID FROM t_operateur WHERE CodeOperateur='" & TxtCodOp.Text & "')")
        NewLiaisonEmploye.OperateurNomPren = NomPrenoms.Trim()
        If NomPrenoms = "" Then
            NewLiaisonEmploye.OperateurNomPren = "Non défini"
        End If
        If NewLiaisonEmploye.ShowDialog() = DialogResult.OK Then
            ChargerOperateur()
        End If
    End Sub

End Class