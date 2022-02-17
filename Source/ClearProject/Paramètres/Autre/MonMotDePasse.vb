Imports System.IO
Imports MySql.Data.MySqlClient

Public Class MonMotDePasse

    Dim passUtil As String = ""
    Dim peutChanger As Boolean = True

    Private Sub MonMotDePasse_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        passUtil = ""
        ChargerInfos()
    End Sub

    Private Sub ChargerInfos()

        query = "select NomOperateur, PrenOperateur, PhotoOperateur, MdpOperateur, ChangeMDP from T_Operateur where UtilOperateur='" & CodeUtilisateur & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            '************
            NomOper.Text = "  " & (MettreApost(rw("NomOperateur").ToString & " " & rw("PrenOperateur").ToString)).Trim()
            TxtLogin.Text = CodeUtilisateur
            TxtPhoto.Text = rw("PhotoOperateur").ToString
            If File.Exists(line & "\Photos\" & TxtPhoto.Text) Then
                Dim OldImage As FileStream = New FileStream(line & "\Photos\" & TxtPhoto.Text, FileMode.Open)
                PhotoOperateur.Image = Image.FromStream(OldImage)
                OldImage.Close()
            Else
                PhotoOperateur.Image = Image.FromFile(line & "\employe.png")
            End If
            passUtil = rw("MdpOperateur").ToString
            peutChanger = IIf(rw(4).ToString = "O", True, False)
            '************
        Next


    End Sub

    Private Sub PhotoOperateur_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles PhotoOperateur.MouseHover
        BtPhoto.Visible = True
    End Sub

    Private Sub BtPhoto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtPhoto.Click

        'affichage de l'image dans le picturebox
        Dim dlg As New OpenFileDialog
        dlg.Filter = "Images|*.jpg;*.jpeg;*.png;*.gif;*.bmp"
        dlg.FileName = String.Empty
        If (dlg.ShowDialog() = DialogResult.OK) Then
            TxtChemin.Text = dlg.FileName
            Dim fichier As FileStream = New FileStream(dlg.FileName, FileMode.Open)
            PhotoOperateur.Image = Image.FromStream(fichier)
            fichier.Close()
        End If

    End Sub

    Private Sub BtEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click
        If (TxtLogin.Text.Trim.Length = 0) Then
            SuccesMsg("Utilisateur inconnu.")
            Exit Sub
        End If

        If TxtOldPass.Text.Trim.Length = 0 And TxtNewPass.Text.Trim.Length = 0 And TxtNewPass2.Text.Trim.Length = 0 Then
            If ConfirmMsg("Il semble que les mots de passes n'ont pas été saisies." & vbNewLine & "Voulez-vous uniquement modifier votre photo de profil?") = DialogResult.Yes Then
                If (TxtChemin.Text <> "") Then
                    Dim ext As String = New FileInfo(TxtChemin.Text).Extension
                    File.Copy(TxtChemin.Text, line & "\Photos\" & TxtPhoto.Text, True)
                    If File.Exists(line & "\Photos\" & TxtPhoto.Text) Then
                        Dim NewImage As FileStream = New FileStream(line & "\Photos\" & TxtPhoto.Text, FileMode.Open)
                        ClearMdi.PictureEdit1.Image = Image.FromStream(NewImage)
                        NewImage.Close()
                        SuccesMsg("Votre compte a été modifié avec succès.")
                        Me.Close()
                        Exit Sub
                    End If
                Else
                    SuccesMsg("Veuillez modifier la photo actuelle bien avant.")
                    Exit Sub
                End If
            End If
        End If

        If (TxtOldPass.Text.Trim.Length = 0) Then
            SuccesMsg("Entrer votre ancien mot de passe.")
            Exit Sub
        ElseIf (TxtNewPass.Text.Trim.Length = 0) Then
            SuccesMsg("Entrer le nouveau mot de passe.")
            Exit Sub
        ElseIf (TxtNewPass2.Text.Trim.Length = 0) Then
            SuccesMsg("Confirmer le nouveau mot de passe.")
            Exit Sub
        ElseIf (TxtNewPass.Text <> TxtNewPass2.Text) Then
            SuccesMsg("Les mots de passe ne concordent pas.")
            TxtNewPass.Focus()
            Exit Sub
        ElseIf (TxtOldPass.Text <> passUtil) Then
            SuccesMsg("L'ancien mot de passe est incorrect.")
            TxtOldPass.Focus()
            Exit Sub
        End If

        If (TxtChemin.Text <> "") Then
            Dim ext As String = New FileInfo(TxtChemin.Text).Extension
            File.Copy(TxtChemin.Text, line & "\Photos\" & TxtPhoto.Text, True)
            If File.Exists(line & "\Photos\" & TxtPhoto.Text) Then
                Dim NewImage As FileStream = New FileStream(line & "\Photos\" & TxtPhoto.Text, FileMode.Open)
                ClearMdi.PictureEdit1.Image = Image.FromStream(NewImage)
                NewImage.Close()
            End If
        End If

        Dim DatSet = New DataSet
        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)
        query = "select * from T_Operateur where UtilOperateur='" & CodeUtilisateur & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd)
        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        DatAdapt.Fill(DatSet, "T_Operateur")

        DatSet.Tables!T_Operateur.Rows(0)!MdpOperateur = TxtNewPass.Text
        DatSet.Tables!T_Operateur.Rows(0)!DateModif = Now.ToShortDateString & " " & Now.ToLongTimeString
        DatSet.Tables!T_Operateur.Rows(0)!Operateur = CodeUtilisateur

        DatAdapt.Update(DatSet, "T_Operateur")
        DatSet.Clear()
        BDQUIT(sqlconn)

        SuccesMsg("Votre compte a été modifié avec succès.")
        Me.Close()

    End Sub

End Class