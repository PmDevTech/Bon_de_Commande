Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.IO

Public Class ProjetClear

    Private Sub ProjetClear_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RemplirComboPays()
    End Sub

    Private Sub RemplirComboPays()
        query = "select LibelleZone from T_ZoneGeo WHERE CodeZoneMere='0'"

        ComboPays.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            ComboPays.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next

    End Sub

    Private Sub DonnerIndicatif()

        query = "Select IndicZone From T_ZoneGeo Where LibelleZone='" & EnleverApost(ComboPays.Text) & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            TxtIndic1.Text = rw(0).ToString
            TxtIndic2.Text = rw(0).ToString
        Next
    End Sub

    Private Sub BtEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click

        Dim action As String = ""
        Dim madate = Now
        Dim dd = madate.ToString("H:mm:ss")

        If (TxtAbrege.Text <> "" And TxtIntitule.Text <> "" _
           And TxtDateDebutMO.Text <> "" And TxtDateFinMO.Text <> "" And TxtDateDebutMV.Text <> "" _
           And TxtDateFinMV.Text <> "" And ComboPays.Text <> "" And TxtAdresse.Text <> "" And TxtBp.Text <> "") Then

            
           query= "Update t_projet set NomProjet='" & EnleverApost(TxtIntitule.Text) & "' ,AdresseProjet='" & EnleverApost(TxtAdresse.Text) & "', TelProjet='" & TxtTelCoordo.Text & "', FaxProjet='" & TxtFaxCoordo.Text & "', MailProjet='" & TxtMailCoordo.Text & "', SiteWebProjet='" & EnleverApost(TxtSiteWeb.Text) & "', DateDebutProjetMO='" & TxtDateDebutMO.Text & "', DateFinProjetMO='" & TxtDateFinMO.Text & "', DateDebutProjetMV='" & TxtDateDebutMV.Text & "', DateFinProjetMV='" & TxtDateFinMV.Text & "', PaysProjet='" & EnleverApost(ComboPays.Text) & "', MinistereTutelle='" & EnleverApost(TxtMinistere.Text) & "', BoitePostaleProjet='" & EnleverApost(TxtBp.Text) & "' where codeprojet='" & TxtAbrege.Text & "'"
            ExecuteNonQuery(query)

            ''historique
            'action = "Mise à jour des informations du projet"
            'sql = "insert into t_historique values (NULL,'" & ProjetEnCours & "','" & NomUtilisateur & "','" & EnleverApost(action) & "','" & madate & "','" & dd & "')"
            'ExecuteNonQuery(query)

            'modification du logo
            If TxtChemin.Text <> "" Then
                Dim DatSet = New DataSet
                query = "select * from t_projet where CodeProjet='" & ProjetEnCours & "'"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Fill(DatSet, "t_projet")
                DatSet.Tables!t_projet.Rows(0)!LogoImage = File.ReadAllBytes(TxtChemin.Text)
                DatAdapt.Update(DatSet, "t_projet")
                DatSet.Clear()
                BDQUIT(sqlconn)
            End If

            SuccesMsg("Modification terminée avec succès.")
        End If

    End Sub

    Private Sub BtLogo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtLogo.Click

        Dim dlg As New OpenFileDialog
        dlg.Filter = "Documents Images (*.png; *.gif; *.jpg; *.bmp)|*.png;*.gif;*.jpg;*.bmp"
        dlg.ShowDialog()
        If (dlg.FileName.ToString <> "") Then

            Dim fichier As FileStream = New FileStream(dlg.FileName, FileMode.Open)
            Dim fichier1 As String = dlg.FileName
            TxtExt.Text = ExtensionImage(fichier1)
            PbLogoProjet.Image = Image.FromStream(fichier)
            TxtChemin.Text = fichier1
            fichier.Close()

        End If

    End Sub

   
End Class