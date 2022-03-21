Public Class AjoutRespoEtape

    Public Modif As Boolean = False

    Private Sub AjoutRespoEtape_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        If Modif = False Then
            InitForm()
        End If

        query = "SELECT Service,Fonction FROM `t_ppm_responsableetape` GROUP BY Service,Fonction"
        Dim dt0 = ExcecuteSelectQuery(query)
        cmbStructure.Properties.Items.Clear()
        cmbFonction.Properties.Items.Clear()
        For Each rw0 In dt0.Rows
            cmbStructure.Properties.Items.Add(MettreApost(rw0("Service").ToString))
            cmbFonction.Properties.Items.Add(MettreApost(rw0("Fonction").ToString))
        Next
    End Sub
    Private Sub InitForm()
        txtNom.Text = ""
        txtPrenom.Text = ""
        txtTelephone.Text = ""
        txtPortable.Text = ""
        txtFax.Text = ""
        txtMail.Text = ""
        cmbFonction.Text = ""
        cmbStructure.Text = ""
    End Sub

    Private Sub BtEnrg_Click(sender As Object, e As EventArgs) Handles BtEnrg.Click
        If txtNom.IsRequiredControl("Veuillez entrer le nom") Then
            Exit Sub
        End If
        If txtPrenom.IsRequiredControl("Veuillez entrer le prenom") Then
            Exit Sub
        End If
        If cmbStructure.Text = "" Then
            SuccesMsg("Veuillez entrer la structure")
            Exit Sub
        End If
        If cmbFonction.Text = "" Then
            SuccesMsg("Veuillez entrer la fonction")
            Exit Sub
        End If
        If txtMail.IsRequiredControl("Veuillez entrer le mail") Then
            Exit Sub
        End If

        If Modif = False Then
            query = "INSERT INTO t_ppm_responsableetape (Nom,Prenoms,Service,Fonction,Telephone,Portable,Fax,Email,CodeProjet) VALUES ('" & EnleverApost(txtNom.Text) & "','" & EnleverApost(txtPrenom.Text) & "','" & EnleverApost(cmbStructure.Text) & "','" & EnleverApost(cmbFonction.Text) & "','" & EnleverApost(txtTelephone.Text) & "','" & EnleverApost(txtPortable.Text) & "','" & EnleverApost(txtFax.Text) & "','" & EnleverApost(txtMail.Text) & "','" & ProjetEnCours & "')"
            ExecuteNonQuery(query)
            SuccesMsg("Enregistrement effectué avec succès.")
            InitForm()
            ResponsableEtape.BtActualiser.PerformClick()
        Else
            query = "UPDATE t_ppm_responsableetape SET Nom='" & EnleverApost(txtNom.Text) & "', Prenoms='" & EnleverApost(txtPrenom.Text) & "', Service='" & EnleverApost(cmbStructure.Text) & "', Fonction='" & EnleverApost(cmbFonction.Text) & "', Telephone='" & EnleverApost(txtTelephone.Text) & "', Portable='" & EnleverApost(txtPortable.Text) & "', Fax='" & EnleverApost(txtFax.Text) & "', Email='" & EnleverApost(txtMail.Text) & "' WHERE CodeProjet='" & ProjetEnCours & "' AND ID='" & IDCahe.Text & "'"
            ExecuteNonQuery(query)
            Modif = False
            SuccesMsg("Modification effectuée avec succès.")
            Me.Close()
        End If
    End Sub

    Private Sub BtAnulOffre_Click(sender As Object, e As EventArgs) Handles BtAnulOffre.Click
        InitForm()
    End Sub
End Class