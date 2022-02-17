Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.IO

Public Class NouveauProjetClear

    Private Sub RemplirComboPays()

        ComboPays.Properties.Items.Clear()
        query = "select LibelleZone from T_ZoneGeo WHERE CodeZoneMere='0'"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows 'dt
            ComboPays.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next

    End Sub

    Private Sub RemplirComboSpecialite()

        Combospe.Properties.Items.Clear()
        query = "select SPE_LIB from T_grh_specialite order by SPE_LIB"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows 'dt
            Combospe.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next

    End Sub

    Private Sub ComboPays_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboPays.SelectedValueChanged
        DonnerIndicatif()
    End Sub

    Private Sub DonnerIndicatif()

        query = "Select IndicZone From T_ZoneGeo Where LibelleZone='" & EnleverApost(ComboPays.Text) & "'"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows 'dt
            TxtIndic1.Text = rw(0).ToString
            TxtIndic2.Text = rw(0).ToString
            TxtIndic3.Text = rw(0).ToString
        Next


    End Sub

    Private Sub RemplirComboVille()

        Dim CodePays As String = "1"
        query = "Select CodeZone From T_ZoneGeo Where LibelleZone='" & EnleverApost(ComboPays.Text) & "'"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows 'dt
            CodePays = rw(0).ToString
        Next

        query = "select LibelleZone from T_ZoneGeo where CodeZoneMere='" & CodePays & "' and CodeZone<>CodeZoneMere"
        Dim dt5 = ExcecuteSelectQuery(query)
        For Each rw5 As DataRow In dt5.Rows 'dt
            ComboVille.Properties.Items.Add(MettreApost(rw5(0).ToString))
        Next

    End Sub

    Private Sub BtEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click
        database = "bd" & TxtAbrege.Text

        If (TxtAbrege.Text <> "" And TxtIntitule.Text <> "" _
           And TxtDateDebutMO.Text <> "" And TxtDateFinMO.Text <> "" And TxtDateDebutMV.Text <> "" _
           And TxtDateFinMV.Text <> "" And ComboPays.Text <> "" And TxtAdresse.Text <> "" And TxtBp.Text <> "" _
           And ComboTitreCoordo.Text <> "" And TxtNomCoordo.Text <> "" And TxtPrenomCoordo.Text <> "" And TxtTelAdmin.Text <> "" _
           And TxtMailAdmin.Text <> "" And TxtLoginAdmin.Text <> "" And TxtPasseAdmin.Text <> "" And ComboVille.Text <> "") Then

            If (Directory.Exists(TxtRepertoire.Text) = True) Then

                'enregistrement du projet
                If (TxtExt.Text <> "") Then
                    File.Copy(TxtChemin.Text, TxtRepertoire.Text & "\LogoProjet\" & TxtAbrege.Text & "." & TxtExt.Text, True)
                End If

                Dim TofOp As String = Now.ToString("yyyyMMddHHmmss")
                If (TxtPhotoAdmin.Text <> "") Then
                    Dim ext As String = New System.IO.FileInfo(TxtPhotoAdmin.Text).Extension
                    File.Copy(TxtPhotoAdmin.Text, TxtRepertoire.Text & "\Photos\" & TofOp & ext, True)
                End If

                EnregistrerProjet()
                EnregistrerCP(TofOp)

                MsgBox("Bienvenue dans le système ClearProject!" & vbNewLine & "Voici votre identifiant et mot de passe:" & vbNewLine & "Nom d'utilisateur : [" & TxtLoginAdmin.Text & "]" & vbNewLine & "Mot de passe : [" & TxtPasseAdmin.Text & "]" & vbNewLine & "Vous pourrez les modifier ultérieurement.", MsgBoxStyle.Information)
                CodeAcces.ChargerCmbProjet()
                Me.Close()

            Else
                MsgBox("{0001X01CD} : Répertoire Système Inexistant!", MsgBoxStyle.Critical)
            End If

        Else
            MsgBox("Formulaire incomplet!", MsgBoxStyle.Information)
        End If

    End Sub

    Private Sub EnregistrerCP(ByVal ImgAdmin As String)

        'enregistrer employé
        Dim madate As Date
        Dim photo = ""
        Dim ext = ""
        madate = Now
        Dim dd = madate.ToString("yyyyMMddHHmmss")

        Dim spec = SeardID("t_grh_specialite", "spe_id", "spe_lib", EnleverApost(Combospe.Text))
        Dim DatSet1 = New DataSet
        query = "select * from T_grh_employe"
        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)
        Dim Cmd1 As MySqlCommand = New MySqlCommand(query, sqlconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd1)
        DatAdapt.Fill(DatSet1, "T_grh_employe")
        Dim DatTable = DatSet1.Tables("T_grh_employe")
        Dim DatRow = DatSet1.Tables("T_grh_employe").NewRow()

        DatRow("SPE_ID") = spec
        DatRow("EMP_MAT") = ""
        DatRow("EMP_NOM") = EnleverApost(TxtNomCoordo.Text)
        DatRow("EMP_PRENOMS") = EnleverApost(TxtPrenomCoordo.Text)
        DatRow("EMP_SEXE") = ""
        DatRow("EMP_DATENAIS") = ""
        DatRow("EMP_LIEUNAIS") = ""
        DatRow("EMP_NATION") = ""
        DatRow("EMP_CONTACT") = TxtTelAdmin.Text
        DatRow("EMP_ADRESSE") = EnleverApost(TxtAdresseAdmin.Text)
        DatRow("EMP_EMAIL") = TxtMailAdmin.Text
        DatRow("EMP_SITUAT") = ""
        DatRow("EMP_NB_ENF") = "0"
        DatRow("EMP_DATA") = dd
        DatRow("PROJ_ID") = TxtAbrege.Text
        DatRow("EMP_CNPS") = ""
        DatRow("EMP_DIPLOME") = ""

        DatSet1.Tables("T_grh_employe").Rows.Add(DatRow)
        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        DatAdapt.Update(DatSet1, "T_grh_employe")
        DatSet1.Clear()

        Dim codeemp As String = ""
        query = "select max(emp_id) from T_grh_employe"
        codeemp = Val(ExecuteScallar(query))

        'enregistrement opérateur
        DatSet1 = New DataSet
        query = "select * from T_Operateur"
        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        DatAdapt = New MySqlDataAdapter(Cmd)
        DatAdapt.Fill(DatSet1, "T_Operateur")
        DatTable = DatSet1.Tables("T_Operateur")
        DatRow = DatSet1.Tables("T_Operateur").NewRow()

        DatRow("MatriculeOperateur") = ""
        DatRow("CiviliteOperateur") = Me.ComboTitreCoordo.Text
        DatRow("NomOperateur") = EnleverApost(TxtNomCoordo.Text)
        DatRow("PrenOperateur") = EnleverApost(TxtPrenomCoordo.Text)
        DatRow("NaisOperateur") = ""
        DatRow("LieuNaisOperateur") = ""
        DatRow("NationaliteOperateur") = EnleverApost(ComboPays.Text)
        DatRow("TelOperateur") = "+" & TxtIndic3.Text & " " & TxtTelCoordo.Text
        DatRow("AdresseOperateur") = EnleverApost(TxtAdresseAdmin.Text)
        DatRow("MailOperateur") = TxtMailAdmin.Text
        DatRow("FonctionOperateur") = "ADMINISTRATEUR " & TxtAbrege.Text
        DatRow("StatutOperateur") = "PERMANENT"
        DatRow("AccesOperateur") = "Administrateur"
        DatRow("PhotoOperateur") = ImgAdmin
        DatRow("CodeProjet") = TxtAbrege.Text
        DatRow("UtilOperateur") = TxtLoginAdmin.Text
        DatRow("MdpOperateur") = TxtPasseAdmin.Text
        DatRow("DateModif") = Now.ToShortDateString & " " & Now.ToLongTimeString
        DatRow("DateSaisie") = Now.ToShortDateString & " " & Now.ToLongTimeString
        DatRow("Operateur") = TxtLoginAdmin.Text
        DatRow("codeService") = "0"
        DatRow("CodeSkin") = "S18"
        DatRow("EMP_ID") = codeemp.ToString
        DatRow("TypeOperateur") = ""

        DatRow("ChangeMDP") = ""
        DatRow("DebutAccesOperateur") = ""
        DatRow("FinAccesOperateur") = ""
        DatRow("MatrimoniOperateur") = ""
        DatRow("OpInterne") = ""

        DatSet1.Tables("T_Operateur").Rows.Add(DatRow)
        CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        DatAdapt.Update(DatSet1, "T_Operateur")
        DatSet1.Clear()

        BDQUIT(sqlconn)
    End Sub

    Private Sub BtLogo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtLogo.Click

        'affichage de l'image dans le picturebox
        Dim dlg As New OpenFileDialog
        dlg.Filter = "Documents Images (*.png; *.gif; *.jpg; *.bmp)|*.png;*.gif;*.jpg;*.bmp"
        dlg.FileName = String.Empty
        dlg.ShowDialog()
        If (dlg.FileName.ToString <> "") Then
            Dim fichier As FileStream = New FileStream(dlg.FileName, FileMode.Open)
            Dim fichier1 As String = dlg.FileName
            TxtExt.Text = ExtensionImage(fichier1)
            PbLogoProjet.Image = Image.FromStream(fichier)
            TxtChemin.Text = fichier1

                Dim mon_fichier As FileInfo = New FileInfo(TxtChemin.Text)
                If mon_fichier.Length < 1000000 Then
                Else
                    MsgBox("Image trop volumineuse !!!", MsgBoxStyle.Exclamation)
                    TxtExt.Text = ""
                    TxtChemin.Text = ""
                    PbLogoProjet.Image = Nothing
                End If
            fichier.Close()
        End If

    End Sub

    Private Sub BtPhoto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtPhoto.Click

        'affichage de l'image dans le picturebox
        TxtPhotoAdmin.Text = ""
        Dim dlg As New OpenFileDialog
        dlg.Filter = "Documents Images (*.png; *.gif; *.jpg; *.bmp)|*.png;*.gif;*.jpg;*.bmp"
        dlg.FileName = String.Empty
        dlg.ShowDialog()
        If (dlg.FileName.ToString <> "") Then
            Dim fichier As FileStream = New FileStream(dlg.FileName, FileMode.Open)
            Dim fichier1 As String = dlg.FileName
            TxtExt.Text = ExtensionImage(fichier1)
            PbAdmin.Image = Image.FromStream(fichier)
            TxtPhotoAdmin.Text = fichier1

                Dim mon_fichier As FileInfo = New FileInfo(TxtChemin.Text)
                If mon_fichier.Length < 1000000 Then
                Else
                    MsgBox("Image trop volumineuse !!!", MsgBoxStyle.Exclamation)
                    TxtExt.Text = ""
                    TxtPhotoAdmin.Text = ""
                    PbAdmin.Image = Nothing
                End If
            fichier.Close()
        End If

    End Sub

    Private Sub TxtRepertoire_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtRepertoire.TextChanged

        If (TxtRepertoire.Text <> "") Then
            If (Directory.Exists(TxtRepertoire.Text) = True) Then
                BtChemTrue.Visible = True
                BtChemFalse.Visible = False
            Else
                BtChemTrue.Visible = False
                BtChemFalse.Visible = True
            End If
        Else
            BtChemTrue.Visible = False
            BtChemFalse.Visible = False
        End If

    End Sub

    Private Sub BtRepertoire_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRepertoire.Click

        TxtRepertoire.Text = ""
        Dim dlg As New FolderBrowserDialog
        dlg.ShowDialog()

        If (dlg.SelectedPath.ToString <> "") Then
            TxtRepertoire.Text = dlg.SelectedPath.ToString
        End If

    End Sub

    Private Sub TxtAbrege_Validated(sender As Object, e As System.EventArgs) Handles TxtAbrege.Validated
        database = "bd" & TxtAbrege.Text
        RemplirComboPays()
        RemplirComboSpecialite()
    End Sub

End Class