Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.IO
Public Class Bailleur

    Inherits DevExpress.XtraEditors.XtraForm
    Dim CodePays As Integer
    Dim IniBailleur As String
    Dim Sig As String
    Dim PourAjout As Boolean = False
    Dim PourModif As Boolean = False
    Dim PourSupp As Boolean = False
    Dim som As Integer
    Dim NomVille As String
    Dim CodeZoneMere As String
    Dim IndicZone As String
    Dim Siege As String
    Dim dtListBailleur = New DataTable
    Dim drx As DataRow
    Dim TabCodePays As String()
    'Verifiant l'existence de l'initiale du bailleur
    Dim TablInitialBailleur As New List(Of String) From {"IDA", "BAD", "RCI", "CI", "BIRD"}


    'Private Sub TxtSigle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    If TxtSigle.Text <> "" And TxtSigle.Text <> "SIGLE" Then
    '        TxtNomBailleur.Enabled = True
    '    End If

    '    If TxtSigle.Text = "" Then
    '        TxtNomBailleur.Enabled = False
    '    End If

    'End Sub

    'Private Sub TxtSigle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    If TxtSigle.Text = "SIGLE" Then
    '        TxtSigle.Text = ""
    '    End If

    'End Sub

    'Private Sub TxtNomBailleur_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    If TxtNomBailleur.Text <> "" Then
    '        ComboPays.Enabled = True
    '    End If

    '    If TxtNomBailleur.Text = "" Then
    '        ComboPays.Enabled = False
    '    End If


    'End Sub

    'Private Sub ComboPays_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Dim Pays As String = ComboPays.Text
    '    Dim Codevilleindic As String = ""
    '    CorrectionChaine(Pays)

    '    query = "select CodeZone,IndicZone from T_ZoneGeo where LibelleZone='" & Pays & "'"
    '    On Error Resume Next
    '    Dim dt = ExcecuteSelectQuery(query)
    '    For Each rw In dt.Rows
    '        Indicatif.Text = rw(1)
    '        TxtIndic1.Text = rw(1)
    '        TxtIndic2.Text = rw(1)
    '        CodePays = rw(0)
    '        Codevilleindic = rw(1)
    '    Next
    '    RemplirChampVille(Codevilleindic)

    '    If ComboPays.Text <> "" Then
    '        TxtAdresse.Enabled = True

    '    End If
    '    If ComboPays.Text = "" Then
    '        TxtAdresse.Enabled = False

    '    End If

    'End Sub

    Private Sub ComboVille_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboVille.SelectedIndexChanged
        If ComboVille.Text <> "" Then
            TxtAdresse.Enabled = True

        End If
        If ComboVille.Text = "" Then
            TxtAdresse.Enabled = False

        End If
    End Sub

    'Private Sub TxtAdresse_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If TxtAdresse.Text <> "" Then
    '        TxtLogo.Enabled = True
    '        BtLogo.Enabled = True
    '        ComboPays.Enabled = True
    '        TxtSiteWebBailleur.Enabled = True
    '    End If
    '    If TxtAdresse.Text = "" Then
    '        TxtLogo.Enabled = False
    '        BtLogo.Enabled = False
    '        ComboPays.Enabled = False
    '        TxtSiteWebBailleur.Enabled = False
    '    End If
    'End Sub

    'Private Sub ComboTitreTtl_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    If ComboPays.Text <> "" Then
    '        TxtAdresse.Enabled = True

    '    End If
    '    If ComboPays.Text = "" Then
    '        TxtAdresse.Enabled = False

    '    End If

    'End Sub

    'Private Sub TxtNomTtl_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If TxtNomTtl.Text <> "" Then
    '        TxtPrenomTtl.Enabled = True

    '    End If
    '    If TxtNomTtl.Text = "" Then
    '        TxtPrenomTtl.Enabled = False

    '    End If
    'End Sub

    'Private Sub TxtPrenomTtl_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If TxtPrenomTtl.Text <> "" Then
    '        TxtFonctionTtl.Enabled = True

    '    End If
    '    If TxtPrenomTtl.Text = "" Then
    '        TxtFonctionTtl.Enabled = False

    '    End If
    'End Sub

    'Private Sub TxtFonctionTtl_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If TxtFonctionTtl.Text <> "" Then
    '        TxtTelTtl.Enabled = True
    '        TxtFaxTtl.Enabled = True
    '        TxtAdresse.Enabled = True
    '    End If
    '    If TxtFonctionTtl.Text = "" Then
    '        TxtTelTtl.Enabled = False
    '        TxtFaxTtl.Enabled = False
    '        TxtAdresse.Enabled = False
    '    End If
    'End Sub

    'Private Sub TxtTelTtl_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If (TxtTelTtl.Text <> "" Or TxtFaxTtl.Text <> "") Then

    '    End If
    '    If (TxtTelTtl.Text = "" And TxtFaxTtl.Text = "") Then

    '    End If
    'End Sub

    'Private Sub BtLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    On Error Resume Next
    '    'affichage de l'image dans le picturebox
    '    Dim dlg As New OpenFileDialog
    '    dlg.Filter = "Documents Images (*.png; *.gif; *.jpg; *.bmp)|*.png;*.gif;*.jpg;*.bmp"
    '    If dlg.ShowDialog() = DialogResult.OK Then
    '        Dim fichier As FileStream = New FileStream(dlg.FileName, FileMode.Open)
    '        Dim fichier1 As String = dlg.FileName
    '        TxtExt.Text = ExtensionImage(fichier1)
    '        If (TxtExt.Text.ToLower = "gif" Or TxtExt.Text.ToLower = "png" Or TxtExt.Text.ToLower = "bmp" Or TxtExt.Text.ToLower = "jpg") Then
    '            LogoBailleur.Image = Image.FromStream(fichier)
    '            TxtLogo.Text = fichier1

    '            Dim mon_fichier As FileInfo = New FileInfo(TxtLogo.Text)
    '            If mon_fichier.Length < 1000000 Then
    '            Else
    '                MsgBox("Image trop volumineuse !!!", MsgBoxStyle.Exclamation)
    '                TxtExt.Text = ""
    '                TxtLogo.Text = ""
    '                LogoBailleur.Image = Nothing
    '            End If
    '        Else
    '            TxtExt.Text = ""
    '            MsgBox("Ce fichier n'est pas une image!", MsgBoxStyle.Exclamation)
    '        End If
    '        fichier.Close()
    '    End If
    'End Sub

    'Private Sub TxtFaxTtl_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If (TxtTelTtl.Text <> "" Or TxtFaxTtl.Text <> "") Then
    '    End If
    '    If (TxtTelTtl.Text = "" And TxtFaxTtl.Text = "") Then
    '    End If
    'End Sub

    Private Sub Bailleur_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        BtRetour1_Click(Me, e)
    End Sub

    Private Sub ViderZoneTexte()
        BtAjouter1.Enabled = True
        BtModifier1.Enabled = False
        BtSupprimer1.Enabled = False
        BtRetour1.Enabled = False
        BtEnregistrer1.Enabled = False
        TxtSigle.Enabled = False
        TxtNomBailleur.Enabled = False
        ComboPays.Enabled = False
        TxtAdresse.Enabled = False
        TxtSiteWebBailleur.Enabled = False
        ComboTitreTtl.Enabled = False
        TxtNomTtl.Enabled = False
        TxtPrenomTtl.Enabled = False
        TxtFonctionTtl.Enabled = False
        MailTTL.Enabled = False
        TxtTelTtl.Enabled = False
        TxtFaxTtl.Enabled = False
    End Sub


    Private Sub Bailleur_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        RemplirChampPays()

        LogoBailleur.Image = Nothing

        dtListBailleur.columns.clear()
        dtListBailleur.columns.add("Code", Type.GetType("System.String"))
        dtListBailleur.columns.add("Sigle", Type.GetType("System.String"))
        dtListBailleur.columns.add("Intitulé", Type.GetType("System.String"))
        dtListBailleur.columns.add("Siège", Type.GetType("System.String"))

        ViderZoneTexte()
        chargerBailleur()
        BtAjouter1.Focus()
    End Sub
    Private Sub RemplirChampPays()
        query = "select CodeZone, LibelleZone from T_ZoneGeo where NiveauStr='1'"
        ComboPays.Properties.Items.Clear()
        Dim dt = ExcecuteSelectQuery(query)
        Dim CodPays As Integer = 0
        ReDim TabCodePays(dt.Rows.Count)
        For Each rw In dt.Rows
            TabCodePays(CodPays) = rw("CodeZone").ToString
            CodPays += 1
            ComboPays.Properties.Items.Add(MettreApost(rw("LibelleZone").ToString))
        Next
    End Sub
    Private Sub chargerBailleur()
        Dim cptr As Integer = 0
        Dim codezone As Integer
        codezone = PrendreCodeVille(EnleverApost(ComboPays.Text))
        'Dim Reader As MySqlDataReader
        'dtListBailleur.Rows.Clear()
        'query = "select B.CodeBailleur,B.InitialeBailleur,B.NomBailleur,Z.LibelleZone from T_Bailleur B,T_ZoneGeo Z where B.CodeZone=Z.CodeZone and B.CodeProjet='" & ProjetEnCours & "'"
        'query = "select B.CodeBailleur,B.InitialeBailleur,B.NomBailleur,Z.LibelleZone from T_Bailleur B,T_ZoneGeo Z where B.CodeZone=Z.CodeZone "
        query = "select CodeBailleur,InitialeBailleur,NomBailleur,CodeZone from T_Bailleur"


        Dim dt = ExcecuteSelectQuery(query)
        dtListBailleur.Rows.Clear()
        For Each rw As DataRow In dt.Rows
            query = "select LibelleZone from T_ZoneGeo where CodeZone='" & rw("CodeZone").ToString & "'"
            Dim LibelleZone = ExecuteScallar(query)
            cptr += 1
            Dim drS = dtListBailleur.NewRow()

            drS("Code") = rw("CodeBailleur").ToString
            drS("Sigle") = rw("InitialeBailleur").ToString
            drS("Intitulé") = MettreApost(rw("NomBailleur").ToString)
            drS("Siège") = MettreApost(LibelleZone)


            dtListBailleur.Rows.Add(drS)
        Next

        GridBailleur.DataSource = dtListBailleur

        BtAjouter1.Focus()

        ViewBailleur.Columns("Code").Visible = False
        ViewBailleur.Columns("Sigle").MaxWidth = 150
        ViewBailleur.Columns("Intitulé").Width = 150
        ViewBailleur.OptionsView.ColumnAutoWidth = True


        ViewBailleur.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        ViewBailleur.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

        'ColorRowGrid(ViewBailleur, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
    End Sub

    Private Sub RemplirChampVille(ByVal Indicteur As String)
        'on efface les lignes du comboville 
        query = "select LibelleZone from T_ZoneGeo where IndicZone='" & Indicteur & "'"
        ComboVille.Items.Clear()
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            ComboVille.Items.Add(MettreApost(rw(0)))
        Next
    End Sub



    Private Sub EnregistrerNouveauBailleur()
        Try

            If (TxtSigle.Text.Trim <> "" And TxtNomBailleur.Text <> "" And ComboPays.Text <> "" And TxtAdresse.Text <> "" And TxtSiteWebBailleur.Text <> "" And ComboTitreTtl.Text <> "" And TxtNomTtl.Text <> "" And TxtPrenomTtl.Text <> "" And TxtFonctionTtl.Text <> "" And MailTTL.Text <> "" And TxtTelTtl.Text <> "") Then
                'on instancie l'objet DataSet avant de l'utiliser
                Dim logo As String = ""
                'vérification de l'extension et copie du chemin de l'image dans le textBox txtchemin
                If (TxtLogo.Text <> "" And TxtExt.Text <> "") Then
                    LogoBailleur.Image.Dispose()
                    LogoBailleur.Image = Nothing
                    Dim NomFichier As String = line & "\LogoProjet\" & TxtSigle.Text & ProjetEnCours & TxtExt.Text
                    NomFichier = NomFichier & LogoBailleur.Text
                    System.IO.File.Copy(TxtLogo.Text, NomFichier, True)
                    logo = TxtSigle.Text & ProjetEnCours & TxtExt.Text
                Else
                    logo = "blanc.GIF"
                End If

                Dim codebail As Integer
                Dim codezone As Integer
                codebail = TxtCodeBailleur.Text
                codezone = PrendreCodeVille(EnleverApost(ComboPays.Text))

                If Val(ExecuteScallar("SELECT COUNT(*) FROM t_bailleur WHERE InitialeBailleur='" & EnleverApost(TxtSigle.Text) & "' and CodeProjet='" & ProjetEnCours & "'")) > 0 Then
                    SuccesMsg("Ce sigle est déjà utilisé")
                    Exit Sub
                    TxtSigle.Focus()
                End If

                'If ViewBailleur.RowCount = 0 Then
                query = "insert into T_Bailleur values(NULL,'" & EnleverApost(TxtNomBailleur.Text) & "','" & EnleverApost(TxtSigle.Text) & "','" & EnleverApost(TxtAdresse.Text) & "','','','" & EnleverApost(TxtSiteWebBailleur.Text) & "','" & logo.ToString & "','" & EnleverApost(MailTTL.Text) & "','" & EnleverApost(ComboTitreTtl.Text) & "','" & EnleverApost(TxtNomTtl.Text) & "','" & EnleverApost(TxtPrenomTtl.Text) & "','" & EnleverApost(TxtFonctionTtl.Text) & "','" & TxtTelTtl.Text & "','" & TxtFaxTtl.Text & "','','" & codezone.ToString & "','" & ProjetEnCours & "')"
                ExecuteNonQuery(query)
                IniBailleur = TxtSigle.Text
                query = "CALL `CreateTampColBailleur`();"
                'ExecuteNonQuery(query)
                'MsgBox("Enregistrement terminée avec succès.", MsgBoxStyle.Information)

                SuccesMsg("Enregistrement terminé avec succès.")
                Effacer()
                'End If
            Else
                SuccesMsg("Veuillez remplir les champs ")
                BtEnregistrer1.Enabled = True
            End If
        Catch ex As Exception
            FailMsg(" Informations non disponible" & vbNewLine & ex.ToString)
        End Try

    End Sub
    'Procédure permettant de créer une ligne après validation du boutton enregistrer
    Private Sub AjouterNvelLIgne()
        Dim MaLigne As ListViewItem
        MaLigne = New ListViewItem(New String() {TxtCodeBailleur.Text, TxtSigle.Text, TxtNomBailleur.Text, ComboPays.Text})
        dtListBailleur.Rows.Add(MaLigne)
    End Sub

    Private Sub CreationCodeBailleur(ByVal id As Integer)
        id = id + 1
        If PourAjout Then
            TxtCodeBailleur.Text = "0" & id
        End If
    End Sub

    Private Function PrendreCodeVille(ByVal Vil As String)
        Dim ValRetour2 As Integer = 0
        CorrectionChaine(Vil)
        query = "select CodeZone from T_ZoneGeo where LibelleZone='" & Vil & "'"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            ValRetour2 = rw(0)
        Next
        Return ValRetour2
    End Function

    Private Sub Effacer()
        TxtNomBailleur.Text = ""
        TxtNomBailleur.Enabled = False
        TxtSigle.Text = ""
        TxtSigle.Enabled = False
        TxtAdresse.Text = ""
        TxtAdresse.Enabled = False
        MailTTL.Text = ""
        MailTTL.Enabled = False
        ComboTitreTtl.Text = ""
        ComboTitreTtl.Enabled = False
        TxtNomTtl.Text = ""
        TxtNomTtl.Enabled = False
        TxtPrenomTtl.Text = ""
        TxtPrenomTtl.Enabled = False
        TxtFonctionTtl.Text = ""
        TxtFonctionTtl.Enabled = False
        TxtTelTtl.Text = ""
        TxtTelTtl.Enabled = False
        TxtFaxTtl.Text = ""
        TxtFaxTtl.Enabled = False
        TxtCodeBailleur.Text = ""
        ComboPays.Text = ""
        ComboPays.Enabled = False
        ComboVille.Text = ""
        ComboVille.Enabled = False
        TxtLogo.Text = ""
        TxtLogo.Enabled = False
        BtLogo.Enabled = False
        Indicatif.Text = ""
        TxtIndic1.Text = ""
        TxtIndic2.Text = ""
        TxtSiteWebBailleur.Text = ""
        LogoBailleur.Image = Nothing
        If BtEnregistrer1.Enabled = False Then BtEnregistrer1.Enabled = True
    End Sub
    Private Sub BtAjouter1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        TxtSigle.Enabled = True
        PourAjout = True
        PourModif = False
        PourSupp = False

        BtModifier1.Enabled = False
        BtSupprimer1.Enabled = False
        BtRetour1.Enabled = True
        BtEnregistrer1.Enabled = True

        If PourAjout Then
            GenererCodeBailler()
        End If

    End Sub

    Private Sub BtModifier1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If TxtAdresse.Text <> "" Then

            'Dim j As Integer = ListViewBailleur.SelectedIndices(0).ToString
            ModificationBailleur()
            chargerBailleur()

            'on met les nouvelles données dans les dif zones du listview
            'ListViewBailleur.Items(j).SubItems(0).Text = TxtCodeBailleur.Text
            ' ListViewBailleur.Items(j).SubItems(1).Text = TxtSigle.Text
            ' ListViewBailleur.Items(j).SubItems(2).Text = TxtNomBailleur.Text
            ' ListViewBailleur.Items(j).SubItems(3).Text = ComboPays.Text
            '******************************************************
            SuccesMsg("Modification terminée avec succès.")
            Effacer() 'effacer les données des zones de saisies


        Else
            SuccesMsg("Veuillez selectionner une ligne dans le tableau !")
        End If

    End Sub

    Private Sub BtSupprimer1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If TxtAdresse.Text <> "" Then

                If ConfirmMsg("Voulez vous vraiment supprimer ?") = MsgBoxResult.Yes Then

                    'on supprime le bailleur 

                    Dim DatSet = New DataSet
                    query = "DELETE FROM T_Bailleur WHERE CodeBailleur = '" & TxtAdresse.Text & "'"
                    ExecuteNonQuery(query)

                    query = "CALL `DeleteTampColBailleur`();"
                    'ExecuteNonQuery(query)
                    query = "CALL `CreateTampColBailleur`();"
                    'ExecuteNonQuery(query)

                    Dim ChemImg As String = TxtSigle.Text & ProjetEnCours & "." & TxtExt.Text
                    LogoBailleur.Image.Dispose()
                    LogoBailleur.Image = Nothing

                    If (File.Exists(line & "\LogoProjet\" & ChemImg) = True) Then
                        File.Delete(line & "\LogoProjet\" & ChemImg)
                    End If

                    chargerBailleur()

                    Effacer() 'pour effacer les zones de text,combo et autre
                    SuccesMsg("Suppression terminée avec succès.")
                End If

            Else
                SuccesMsg("Veuillez selectionner une ligne dans le tableau !")
            End If
        Catch ex As Exception
            FailMsg(" Informations non disponible" & vbNewLine & ex.ToString)
        End Try
    End Sub

    Private Sub BtRetour1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Effacer()

        PourAjout = False
        PourModif = False
        PourSupp = False

        BtModifier1.Enabled = True
        BtAjouter1.Enabled = True
        BtSupprimer1.Enabled = True
        BtRetour1.Enabled = False
        BtEnregistrer1.Enabled = False

        chargerBailleur()


    End Sub
    'Private Sub BtEnregistrer1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    '**********************  Pour ajouter ******************************

    '    Sig = TxtSigle.Text
    '    EnregistrerNouveauBailleur()
    '    'ParaCon() 'procedure permettant de prendre le nom du serveur 

    '    'vérification de l'extension et copie du chemin de l'image dans la texbox txtchemin

    '    If (TxtExt.Text <> "") Then
    '        File.Copy(TxtLogo.Text, line & "\LogoProjet\" & TxtSigle.Text & ProjetEnCours & "." & TxtExt.Text, True)
    '    End If

    '    'AjouterNvelLIgne()
    '    'CreationCodeBailleur(TxtAdresse.Text)
    '    Effacer()
    '    chargerBailleur()


    'End Sub

    'Private Sub ListViewBailleur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Dim i As Integer = dtListBailleur.SelectedIndices(0).ToString
    '    ToutChampActiver()

    '    With ListViewBailleur
    '        If .SelectedIndices.Count > 0 Then
    '            .Items(.SelectedIndices(0)).Selected = True
    '            TxtAdresse.Text = .Items(.SelectedIndices(0)).SubItems(0).Text
    '            TxtSigle.Text = .Items(.SelectedIndices(0)).SubItems(1).Text
    '            TxtNomBailleur.Text = .Items(.SelectedIndices(0)).SubItems(2).Text
    '            AfficherInfoTableBailleur(ListViewBailleur.Items(i).SubItems(0).Text)
    '            GroupInstitution.Enabled = True
    '            GroupTTL.Enabled = True
    '            TxtExt.Text = ""
    '            BtEnregistrer1.Enabled = False
    '        End If
    '    End With
    'End Sub

    Private Sub ToutChampActiver()

        TxtSigle.Enabled = True
        TxtNomBailleur.Enabled = True
        ComboPays.Enabled = True
        ComboVille.Enabled = True
        TxtAdresse.Enabled = True
        TxtLogo.Enabled = True
        BtLogo.Enabled = True
        ComboTitreTtl.Enabled = True
        TxtNomTtl.Enabled = True
        TxtPrenomTtl.Enabled = True
        TxtFonctionTtl.Enabled = True
        MailTTL.Enabled = True
        TxtTelTtl.Enabled = True
        TxtFaxTtl.Enabled = True
        GridBailleur.Enabled = True
        'ListViewBailleur.Enabled = True

    End Sub
    'Private Sub ListViewBailleur_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim i As Integer = ListViewBailleur.SelectedIndices(0).ToString

    '    If PourSupp = True Then
    '        'ToutChampActiver()
    '        If PourSupp = True Then
    '            With ListViewBailleur
    '                If .SelectedIndices.Count > 0 Then
    '                    .Enabled = True
    '                    .Items(.SelectedIndices(0)).Selected = True
    '                    TxtAdresse.Text = .Items(.SelectedIndices(0)).SubItems(0).Text
    '                    TxtSigle.Text = .Items(.SelectedIndices(0)).SubItems(1).Text
    '                    TxtNomBailleur.Text = .Items(.SelectedIndices(0)).SubItems(2).Text
    '                    AfficherInfoTableBailleur(ListViewBailleur.Items(i).SubItems(0).Text)
    '                    GroupInstitution.Enabled = False
    '                    GroupTTL.Enabled = False
    '                End If
    '            End With
    '        End If
    '    End If
    'End Sub

    Private Sub AfficherInfoTableBailleur(ByVal CodeBail As String)

        LogoBailleur.Image = Nothing

        Dim IdZone As String = ""

        query = "select CodeBailleur,NomBailleur,InitialeBailleur,AdresseCompleteBailleur,TitreTTL,NomTTL,PrenomTTL,FonctionTTL,TelTTL,MailTTL,FaxTTL,CodeZone,LogoBailleur,SiteWeb from T_Bailleur where CodeBailleur='" & CodeBail & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            '*************  Institution *************************
            IdZone = rw("CodeZone").ToString
            TxtCodeBailleur.Text = rw("CodeBailleur").ToString
            TxtSigle.Text = rw("InitialeBailleur").ToString
            TxtNomBailleur.Text = MettreApost(rw("NomBailleur").ToString)
            TxtAdresse.Text = MettreApost(rw("AdresseCompleteBailleur").ToString)
            '********* TTL *******************
            ComboTitreTtl.Text = MettreApost(rw("TitreTTL").ToString)
            TxtNomTtl.Text = MettreApost(rw("NomTTL").ToString)
            TxtFonctionTtl.Text = MettreApost(rw("FonctionTTL").ToString)
            TxtTelTtl.Text = rw("TelTTL").ToString
            TxtPrenomTtl.Text = MettreApost(rw("PrenomTTL").ToString)
            MailTTL.Text = MettreApost(rw("MailTTL").ToString)
            TxtFaxTtl.Text = rw("FaxTTL").ToString
            TxtSiteWebBailleur.Text = MettreApost(rw("SiteWeb").ToString)
            Dim img() As String
            img = rw("LogoBailleur").ToString.Split(".")



            If File.Exists(line & "\LogoProjet\" & rw(12).ToString) Then
                LogoBailleur.Image = Image.FromFile(line & "\LogoProjet\" & rw(12).ToString)
                Dim fichier1 As String = line & "\LogoProjet\" & rw(12).ToString
                TxtExt.Text = ExtensionImage(fichier1)
                TxtLogo.Text = fichier1
            End If
            '*********************************************************
        Next

        PrendreLibVille(IdZone)
        ComboPays.Text = NomVille
        TxtIndic1.Text = IndicZone
        TxtIndic2.Text = IndicZone
        Indicatif.Text = IndicZone

    End Sub

    Private Sub PrendreLibVille(ByVal CodeVille As String)

        query = "select LibelleZone,CodeZoneMere,IndicZone from T_ZoneGeo where CodeZone='" & CodeVille & "'"
        Dim dt = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            Dim rw As DataRow = dt.Rows(0)
            CodeZoneMere = rw("CodeZoneMere")
            NomVille = MettreApost(rw("LibelleZone"))
            IndicZone = rw("IndicZone")
        End If
    End Sub
    Private Sub PrendreLibDuPays(ByVal IdPays As String)

        query = "select LibelleZone,IndicZone from T_ZoneGeo where CodeZone='" & IdPays & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            Dim rw As DataRow = dt.Rows(0)
            CodeZoneMere = MettreApost(rw("LibelleZone"))
            IndicZone = rw("IndicZone")
        End If
    End Sub
    Private Sub EffacerZoneDeSaises()
        TxtCodeBailleur.Text = ""
        TxtSigle.Text = ""
        TxtNomBailleur.Text = ""
        Indicatif.Text = ""
        ComboPays.Text = ""
        ComboVille.Text = ""
        TxtAdresse.Text = ""
        TxtLogo.Text = ""
        ComboTitreTtl.Text = ""
        TxtNomTtl.Text = ""
        TxtFonctionTtl.Text = ""
        TxtIndic1.Text = ""
        TxtIndic2.Text = ""
        TxtTelTtl.Text = ""
        TxtPrenomTtl.Text = ""
        MailTTL.Text = ""
        TxtFaxTtl.Text = ""
        LogoBailleur.Image = Image.FromFile(line & "\LogoProjet\blanc.GIF")
    End Sub

    Private Sub ModificationBailleur()

        Try
            'on instancie l'objet DataSet avec de l'utiliser

            If TxtExt.Text = "" Then

            Else
                Dim logo As String = ""


                query = "select LogoBailleur from T_Bailleur where CodeBailleur='" & TxtCodeBailleur.Text & "'"
                logo = ExecuteScallar(query)


                Dim ChemImg As String = logo.ToString
                'LogoBailleur.Image.Dispose()
                'LogoBailleur.Image = Nothing
                If Mid(logo, 1, 3) = "Tmp" Then
                    Dim cpt As Integer = Val(Mid(logo, 4))
                    File.Copy(TxtLogo.Text, line & "\LogoProjet\Tmp" & cpt + 1 & TxtSigle.Text & ProjetEnCours & TxtExt.Text, True)
                    query = "UPDATE T_Bailleur SET LogoBailleur='Tmp" & cpt + 1 & TxtSigle.Text & ProjetEnCours & TxtExt.Text & "' where CodeBailleur='" & TxtCodeBailleur.Text & "'"
                    ExecuteNonQuery(query)
                Else
                    File.Copy(TxtLogo.Text, line & "\LogoProjet\Tmp" & TxtSigle.Text & ProjetEnCours & TxtExt.Text, True)
                    query = "UPDATE T_Bailleur SET LogoBailleur='Tmp" & TxtSigle.Text & ProjetEnCours & TxtExt.Text & "' where CodeBailleur='" & TxtCodeBailleur.Text & "'"
                    ExecuteNonQuery(query)
                End If
            End If


            query = "update T_Bailleur set CodeZone='" & TabCodePays(ComboPays.SelectedIndex) & "'  ,AdresseCompleteBailleur='" & EnleverApost(TxtAdresse.Text) & "',InitialeBailleur='" & EnleverApost(TxtSigle.Text) & "' , NomBailleur='" & EnleverApost(TxtNomBailleur.Text) & "', Siteweb='" & EnleverApost(TxtSiteWebBailleur.Text) & "', MailTTl='" & EnleverApost(MailTTL.Text) & "', NomTTl='" & EnleverApost(TxtNomTtl.Text) & "', PrenomTTL='" & EnleverApost(TxtPrenomTtl.Text) & "', FonctionTTL='" & EnleverApost(TxtFonctionTtl.Text) & "', TelTTL='" & TxtTelTtl.Text & "', FaxTTL='" & TxtFaxTtl.Text & "' where CodeBailleur='" & TxtCodeBailleur.Text & "'"
            ExecuteNonQuery(query)


        Catch ex As Exception
            FailMsg(" Informations non disponible " & vbNewLine & ex.ToString)
        End Try

    End Sub
    Private Sub GenererCodeBailler()
        'on  remplit le dataTable du  Dataset 
        Dim DatSet = New DataSet
        query = "select * from T_Bailleur"

        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)

        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd)
        DatAdapt.Fill(DatSet, "T_Bailleur")
        Dim DatTable = DatSet.Tables("T_Bailleur")
        'on teste ici pour voir s'il y a un enregistrement non
        If DatSet.HasErrors = False Then

            'requete pour compter le nb d'enregistrement

            query = "select COUNT(*) from T_Bailleur"
            som = ExecuteScallar(query)

            'appel de la procédure pour déterminer le Code de la Catégorie suivante à ajouter 
            CreationCodeBailleur(som)
        End If

    End Sub

    Private Sub Bailleur_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    'Private Sub ActualiserDevise_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Zonegeo.ShowDialog()
    'End Sub


    Private Sub SupprimerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If TxtAdresse.Text <> "" Then

                If ConfirmMsg("Voulez vous vraiment supprimer ?") = MsgBoxResult.Yes Then

                    'on supprime le bailleur 

                    Dim DatSet = New DataSet
                    query = "DELETE FROM T_Bailleur WHERE CodeBailleur = '" & TxtAdresse.Text & "'"
                    ExecuteNonQuery(query)

                    query = "CALL `DeleteTampColBailleur`();"
                    'ExecuteNonQuery(query)
                    query = "CALL `CreateTampColBailleur`();"
                    'ExecuteNonQuery(query)

                    Dim ChemImg As String = TxtSigle.Text & ProjetEnCours & "." & TxtExt.Text
                    LogoBailleur.Image.Dispose()
                    LogoBailleur.Image = Nothing

                    If (File.Exists(line & "\LogoProjet\" & ChemImg) = True) Then
                        File.Delete(line & "\LogoProjet\" & ChemImg)
                    End If



                    Effacer() 'pour effacer les zones de text,combo et autre
                    SuccesMsg("Suppression terminée avec succès.")

                    chargerBailleur()

                End If

            Else
                SuccesMsg("Veuillez selectionner une ligne dans le tableau !")
            End If
        Catch ex As Exception
            FailMsg(" Informations non disponible " & vbNewLine & ex.ToString)
        End Try
    End Sub

    
    Private Sub GridBailleur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridBailleur.Click
        'If ViewBailleur.RowCount > 0 Then

        '    drx = ViewBailleur.GetDataRow(ViewBailleur.FocusedRowHandle)
        '    Dim IDl = drx("Code").ToString

        '    ColorRowGrid(ViewBailleur, "[N°]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
        '    ColorRowGridAnal(ViewBailleur, "[Code]='" & IDl & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)

        '    AfficherInfoTableBailleur(drx("Code").ToString)

        '    BtRetour1.Enabled = True
        '    BtModifier1.Enabled = True
        '    BtSupprimer1.Enabled = True
        '    BtAjouter1.Enabled = False
        '    BtEnregistrer1.Enabled = False
        '    Indicatif.Enabled = False
        '    TxtIndic1.Enabled = False
        '    TxtIndic2.Enabled = False
        'End If
    End Sub

    Private Sub BtRetour1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRetour1.Click
        Effacer()

        PourAjout = False
        PourModif = False
        PourSupp = False

        BtModifier1.Enabled = False
        BtAjouter1.Enabled = True
        BtAjouter1.Focus()
        BtSupprimer1.Enabled = False
        BtRetour1.Enabled = False
        BtEnregistrer1.Enabled = False

        chargerBailleur()
    End Sub






    Private Sub BtSupprimer1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSupprimer1.Click
        Try

            If (ViewBailleur.RowCount > 0) Then

                'Dim CodeBail_List As String = ""
                Dim cpte As Decimal = 0
                Dim errordel As Decimal = 0
                Dim str As String = String.Empty
                drx = ViewBailleur.GetDataRow(ViewBailleur.FocusedRowHandle)
                Dim CodeBail = drx("Code").ToString
                Dim LibBail = drx("Intitulé").ToString
                'For i = 0 To ViewBailleur.RowCount - 1
                'If CBool(ViewBailleur.GetRowCellValue(i, "Choix")) = True Then
                'CodeBail_List &= CodeBail & ";"
                '---------requete de verif
                query = "select Distinct CodeBailleur from T_Convention where CodeBailleur ='" & CodeBail & "'"
                Dim dt = ExcecuteSelectQuery(query)
                If dt.Rows.Count > 0 Then
                    SuccesMsg("impossible de supprimer ce bailleur !")
                    chargerBailleur()
                    Effacer()
                    Exit Sub
                End If
                If ConfirmMsg("Voulez vous supprimer ce bailleur?") = DialogResult.Yes Then
                    query = "delete from T_Bailleur where CodeBailleur='" & TxtCodeBailleur.Text & "'"
                    ExecuteNonQuery(query)
                    SuccesMsg("Suppression effectuée avec succès")
                    chargerBailleur()
                    Effacer()
                End If
            Else
                SuccesMsg("Veuillez selectionner une ligne dans le tableau !")

            End If
            'Next

            'End If

            'Next
            'End If







            '    If TxtCodeBailleur.Text <> "" Then

            '        If MsgBox("Voulez vous vraiment supprimer?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then

            '            'on supprime le bailleur 

            '            Dim DatSet = New DataSet
            '            query = "DELETE FROM T_Bailleur WHERE CodeBailleur = '" & TxtCodeBailleur.Text & "'"
            '            ExecuteNonQuery(query)

            '            query = "CALL `DeleteTampColBailleur`();"
            '            'ExecuteNonQuery(query)
            '            query = "CALL `CreateTampColBailleur`();"
            '            'ExecuteNonQuery(query)

            '            Dim ChemImg As String = TxtSigle.Text & ProjetEnCours & "." & TxtExt.Text
            '            LogoBailleur.Image.Dispose()
            '            LogoBailleur.Image = Nothing

            '            If (File.Exists(line & "\LogoProjet\" & ChemImg) = True) Then
            '                File.Delete(line & "\LogoProjet\" & ChemImg)
            '            End If



            '            Effacer() 'pour effacer les zones de text,combo et autre
            '            SuccesMsg("Suppression terminée avec succès.")

            '            chargerBailleur()

            '        End If

            '    Else
            '        FailMsg("Veuillez selectionner une ligne dans le tableau !")
            '    End If
        Catch ex As Exception
            FailMsg(" Informations non disponible " & vbNewLine & ex.ToString)
        End Try
    End Sub


    'Verification de l'existence du code de la methode
    Private Function GetVerifierInitialBailleur(ByVal Initialbailleur As String) As Boolean
        Try
            For i = 0 To TablInitialBailleur.Count - 1
                If TablInitialBailleur(i).ToString = Initialbailleur.ToString.ToUpper.Trim Then
                    Return True
                End If
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        Return False
    End Function

    Private Sub BtModifier1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtModifier1.Click
        If TxtCodeBailleur.Text <> "" Then
            If GetVerifierInitialBailleur(TxtSigle.Text) = False Then
                SuccesMsg("Le sigle saisie n'existe pas")
                TxtSigle.Select()
                Exit Sub
            End If

            If ConfirmMsg("Voulez vous modifier ce bailleur ?") = DialogResult.Yes Then
                'Dim j As Integer = ListViewBailleur.SelectedIndices(0).ToString
                ModificationBailleur()
                chargerBailleur()

                'on met les nouvelles données dans les dif zones du listview
                'ListViewBailleur.Items(j).SubItems(0).Text = TxtCodeBailleur.Text
                ' ListViewBailleur.Items(j).SubItems(1).Text = TxtSigle.Text
                ' ListViewBailleur.Items(j).SubItems(2).Text = TxtNomBailleur.Text
                ' ListViewBailleur.Items(j).SubItems(3).Text = ComboPays.Text
                '******************************************************
                SuccesMsg("Modification terminée avec succès.")
                Effacer() 'effacer les données des zones de saisies
                BtSupprimer1.Enabled = False
            End If

        Else
            SuccesMsg("Veuillez selectionner une ligne dans le tableau !")
        End If

    End Sub

    Private Sub BtAjouter1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjouter1.Click
        TxtSigle.Enabled = True
        TxtSigle.Focus()
        PourAjout = True
        PourModif = False
        PourSupp = False

        BtModifier1.Enabled = False
        BtSupprimer1.Enabled = False
        BtRetour1.Enabled = True
        BtEnregistrer1.Enabled = True

        If PourAjout Then
            GenererCodeBailler()
        End If
    End Sub

    Private Sub BtEnregistrer1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnregistrer1.Click
       
        '**********************  Pour ajouter ******************************

        Sig = TxtSigle.Text
        If GetVerifierInitialBailleur(TxtSigle.Text) = False Then
            SuccesMsg("Le sigle saisie n'existe pas")
            TxtSigle.Select()
            Exit Sub
        End If

        'ParaCon() 'procedure permettant de prendre le nom du serveur 
        EnregistrerNouveauBailleur()
        AjouterNvelLIgne()
        'CreationCodeBailleur(TxtCodeBailleur.Text)
        chargerBailleur()
        BtEnregistrer1.Enabled = True
    End Sub

    Private Sub ComboPays_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboPays.SelectedIndexChanged
        Dim Pays As String = ComboPays.Text
        Dim Codevilleindic As String = ""
        CorrectionChaine(Pays)

        query = "select CodeZone,IndicZone from T_ZoneGeo where LibelleZone='" & Pays & "'"
        On Error Resume Next
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            Indicatif.Text = rw("IndicZone")
            TxtIndic1.Text = rw("IndicZone")
            TxtIndic2.Text = rw("IndicZone")
            CodePays = rw("CodeZone")
            Codevilleindic = rw("IndicZone")
        Next
        RemplirChampVille(Codevilleindic)

        If ComboPays.Text <> "" Then
            TxtAdresse.Enabled = True
            TxtIndic1.Enabled = False
            TxtIndic2.Enabled = False
            Indicatif.Enabled = False

        End If
        If ComboPays.Text = "" Then
            TxtAdresse.Enabled = False

        End If

    End Sub

    Private Sub TxtSigle_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSigle.EditValueChanged

        If TxtSigle.Text <> "" And TxtSigle.Text <> "SIGLE" Then
            TxtNomBailleur.Enabled = True
            'BtEnregistrer1.Enabled = True
        End If

        If TxtSigle.Text = "" Then
            TxtNomBailleur.Enabled = False
        End If
    End Sub

    Private Sub TxtSigle_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSigle.Click
        If TxtSigle.Text = "SIGLE" Then
            TxtSigle.Text = ""
        End If
    End Sub

    Private Sub TxtNomBailleur_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNomBailleur.EditValueChanged
        If TxtNomBailleur.Text <> "" Then
            ComboPays.Enabled = True
            'BtEnregistrer1.Enabled = True
        End If

        If TxtNomBailleur.Text = "" Then
            ComboPays.Enabled = False
        End If
    End Sub

    Private Sub TxtAdresse_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtAdresse.EditValueChanged
        If TxtAdresse.Text <> "" Then
            TxtLogo.Enabled = True
            BtLogo.Enabled = True
            ComboTitreTtl.Enabled = True
            TxtSiteWebBailleur.Enabled = True
            'BtEnregistrer1.Enabled = True
        End If
        If TxtAdresse.Text = "" Then
            TxtLogo.Enabled = False
            BtLogo.Enabled = False
            ComboTitreTtl.Enabled = False
            TxtSiteWebBailleur.Enabled = False
        End If
    End Sub

    Private Sub ComboTitreTtl_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboTitreTtl.SelectedIndexChanged
        If ComboTitreTtl.Text <> "" Then
            TxtNomTtl.Enabled = True
            'BtEnregistrer1.Enabled = True

        End If
        If ComboTitreTtl.Text = "" Then
            TxtNomTtl.Enabled = False

        End If
    End Sub

    Private Sub TxtNomTtl_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNomTtl.EditValueChanged
        If TxtNomTtl.Text <> "" Then
            TxtPrenomTtl.Enabled = True
            'BtEnregistrer1.Enabled = True
        End If
        If TxtNomTtl.Text = "" Then
            TxtPrenomTtl.Enabled = False

        End If
    End Sub

    Private Sub TxtPrenomTtl_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtPrenomTtl.EditValueChanged
        If TxtPrenomTtl.Text <> "" Then
            TxtFonctionTtl.Enabled = True
            'BtEnregistrer1.Enabled = True
        End If
        If TxtPrenomTtl.Text = "" Then
            TxtFonctionTtl.Enabled = False

        End If
    End Sub

    Private Sub TxtFonctionTtl_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtFonctionTtl.EditValueChanged
        If TxtFonctionTtl.Text <> "" Then
            MailTTL.Enabled = True
            TxtTelTtl.Enabled = True
            TxtFaxTtl.Enabled = True
            TxtAdresse.Enabled = True
            'BtEnregistrer1.Enabled = True
        End If
        If TxtFonctionTtl.Text = "" Then
            TxtTelTtl.Enabled = False
            TxtFaxTtl.Enabled = False
            TxtAdresse.Enabled = False
        End If
    End Sub

    Private Sub TxtTelTtl_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtTelTtl.EditValueChanged
        If (TxtTelTtl.Text <> "" Or TxtFaxTtl.Text <> "") Then
            'BtEnregistrer1.Enabled = True
        End If
        If (TxtTelTtl.Text = "" And TxtFaxTtl.Text = "") Then

        End If
    End Sub

    Private Sub TxtFaxTtl_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtFaxTtl.EditValueChanged
        If (TxtTelTtl.Text <> "" Or TxtFaxTtl.Text <> "") Then
            'BtEnregistrer1.Enabled = True
        End If
        If (TxtTelTtl.Text = "" And TxtFaxTtl.Text = "") Then
        End If
    End Sub

    Private Sub BtLogo_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtLogo.Click
        On Error Resume Next
        'affichage de l'image dans le picturebox
        Dim dlg As New OpenFileDialog
        dlg.Filter = "Documents Images (*.png; *.gif; *.jpg; *.bmp)|*.png;*.gif;*.jpg;*.bmp"
        If dlg.ShowDialog() = DialogResult.OK Then
            Dim fichier As FileStream = New FileStream(dlg.FileName, FileMode.Open)
            Dim fichier1 As String = dlg.FileName
            TxtExt.Text = ExtensionImage(fichier1)
            If (TxtExt.Text.ToLower = ".gif" Or TxtExt.Text.ToLower = ".png" Or TxtExt.Text.ToLower = ".bmp" Or TxtExt.Text.ToLower = ".jpg") Then
                LogoBailleur.Image = Image.FromStream(fichier)
                TxtLogo.Text = fichier1

                Dim mon_fichier As FileInfo = New FileInfo(TxtLogo.Text)
                If mon_fichier.Length < 1000000 Then
                Else
                    SuccesMsg("Image trop volumineuse !!!")
                    TxtExt.Text = ""
                    TxtLogo.Text = ""
                    LogoBailleur.Image = Nothing
                End If
            Else
                TxtExt.Text = ""
                SuccesMsg("Ce fichier n'est pas une image!")
            End If
            fichier.Close()
        End If
    End Sub

    Private Sub ActualiserDevise_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActualiserDevise.Click
        Zonegeo.ShowDialog()
    End Sub

    Private Sub TxtSiteWebBailleur_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSiteWebBailleur.EditValueChanged
        If TxtSiteWebBailleur.Text <> "" Then
            BtLogo.Enabled = True
        End If

        If TxtSiteWebBailleur.Text = "" Then
            BtLogo.Enabled = False
        End If
    End Sub

    Private Sub GridBailleur_DoubleClick(sender As Object, e As EventArgs) Handles GridBailleur.DoubleClick
        If ViewBailleur.RowCount > 0 Then

            drx = ViewBailleur.GetDataRow(ViewBailleur.FocusedRowHandle)
            Dim IDl = drx("Code").ToString

            ColorRowGrid(ViewBailleur, "[N°]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewBailleur, "[Code]='" & IDl & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)

            AfficherInfoTableBailleur(drx("Code").ToString)

            BtRetour1.Enabled = True
            BtModifier1.Enabled = True
            BtSupprimer1.Enabled = True
            BtAjouter1.Enabled = False
            BtEnregistrer1.Enabled = False
            Indicatif.Enabled = False
            TxtIndic1.Enabled = False
            TxtIndic2.Enabled = False
            TxtSigle.Enabled = False
        End If
    End Sub

End Class

