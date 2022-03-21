Imports System.Math
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class AjoutSignataire
    Dim LigneModif As Integer = -1

    Private Sub AjoutSignataire_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        If ReponseDialog <> "" Then
            GroupControl1.Visible = False
            Me.Size = New Point(564, 239)
            BtEnregistrer.Text = "Modifier"
        Else
            GroupControl1.Visible = True
            Me.Size = New Point(564, 469)
            BtEnregistrer.Text = "Enregistrer"
            BtAnnuler.PerformClick()
        End If
    End Sub

    Private Sub AjoutSignataire_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        FinChargement()
    End Sub

    Private Sub AjoutSignataire_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ReponseDialog = ""
    End Sub

    Private Sub EmailSign_KeyDown(sender As Object, e As KeyEventArgs) Handles EmailSigndp.KeyDown, ContactSigndp.KeyDown, AdresseSigndp.KeyDown, NomSignatairedp.KeyDown, TypeSignatairedp.KeyDown, Txtfonctiondp.KeyDown

        If e.KeyCode = Keys.Enter And GroupControl1.Visible = True Then
            If NomSignatairedp.IsRequiredControl("Veuillez saisir le nom du signataire") Then
                NomSignatairedp.Focus()
                Exit Sub
            End If

            If ContactSigndp.IsRequiredControl("Veuillez saisir le contact du signataire") Then
                ContactSigndp.Focus()
                Exit Sub
            End If

            If AdresseSigndp.IsRequiredControl("Veuillez saisir l'adresse du signataire") Then
                AdresseSigndp.Focus()
                Exit Sub
            End If

            If EmailSigndp.IsRequiredControl("Veuillez saisir l'email du signataire") Then
                EmailSigndp.Focus()
                Exit Sub
            End If

            If TypeSignatairedp.IsRequiredControl("Veuillez selectionné le type") Then
                Exit Sub
            End If

            Dim n As Integer = 0

            'Nouvo signataire
            If LigneModif = -1 Then
                n = GridSignataire.Rows.Add()
            Else
                'Index du tabaleau de la ligne du signataire a modifier
                n = LigneModif
            End If

            GridSignataire.Rows.Item(n).Cells("Nom").Value = NomSignatairedp.Text
            GridSignataire.Rows.Item(n).Cells("Contact").Value = ContactSigndp.Text
            GridSignataire.Rows.Item(n).Cells("Adresse").Value = AdresseSigndp.Text
            GridSignataire.Rows.Item(n).Cells("Email").Value = EmailSigndp.Text
            GridSignataire.Rows.Item(n).Cells("TypeSignatair").Value = TypeSignatairedp.Text
            GridSignataire.Rows.Item(n).Cells("Fonction").Value = Txtfonctiondp.Text

            EffacerTexBox4(PanelControl4)
            LigneModif = -1
        End If

    End Sub

    Private Sub GridSignataire_DoubleClick(sender As Object, e As EventArgs) Handles GridSignataire.DoubleClick
        If GridSignataire.RowCount > 0 Then
            Dim n As Integer = GridSignataire.CurrentRow.Index
            LigneModif = GridSignataire.CurrentRow.Index
            NomSignatairedp.Text = GridSignataire.Rows.Item(n).Cells("Nom").Value.ToString
            ContactSigndp.Text = GridSignataire.Rows.Item(n).Cells("Contact").Value.ToString
            AdresseSigndp.Text = GridSignataire.Rows.Item(n).Cells("Adresse").Value.ToString
            EmailSigndp.Text = GridSignataire.Rows.Item(n).Cells("Email").Value.ToString
            Txtfonctiondp.Text = GridSignataire.Rows.Item(n).Cells("Fonction").Value.ToString
            TypeSignatairedp.Text = GridSignataire.Rows.Item(n).Cells("TypeSignatair").Value.ToString
        End If
    End Sub


    Private Sub BtAnnuler_Click(sender As Object, e As EventArgs) Handles BtAnnuler.Click
        EffacerTexBox4(PanelControl4)
        EffacerTexBox2(GroupControl1)
    End Sub

    Private Sub BtEnregistrer_Click(sender As Object, e As EventArgs) Handles BtEnregistrer.Click
        Try

            If BtEnregistrer.Text = "Enregistrer" Then
                If GridSignataire.RowCount = 0 Then
                    SuccesMsg("Veuillez ajouter un signataire")
                    Exit Sub
                End If

                DebutChargement(True, "Enregistrement en cours...")
                For n = 0 To GridSignataire.RowCount - 1
                    query = "Insert into t_signataire values(NULL,'" & EnleverApost(GridSignataire.Rows.Item(n).Cells("Nom").Value.ToString) & "','" & GridSignataire.Rows.Item(n).Cells("Contact").Value.ToString & "', '" & EnleverApost(GridSignataire.Rows.Item(n).Cells("Adresse").Value.ToString) & "', '" & EnleverApost(GridSignataire.Rows.Item(n).Cells("Email").Value.ToString) & "', '" & EnleverApost(GridSignataire.Rows.Item(n).Cells("Fonction").Value.ToString) & "', '" & EnleverApost(GridSignataire.Rows.Item(n).Cells("TypeSignatair").Value.ToString) & "','" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', '" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', '" & CodeUtilisateur & "', '" & ProjetEnCours & "')"
                    ExecuteNonQuery(query)
                Next
                FinChargement()
                SuccesMsg("Enregistrement effecuté avec succès")
                GridSignataire.Rows.Clear()
                BtAnnuler.PerformClick()
                ListesSignataires.ChargerSignataire()
            Else
                If ReponseDialog = "" Then
                    Exit Sub
                End If

                If NomSignatairedp.IsRequiredControl("Veuillez saisir le nom du signataire") Then
                    NomSignatairedp.Focus()
                    Exit Sub
                End If

                If ContactSigndp.IsRequiredControl("Veuillez saisir le contact du signataire") Then
                    ContactSigndp.Focus()
                    Exit Sub
                End If

                If AdresseSigndp.IsRequiredControl("Veuillez saisir l'adresse du signataire") Then
                    AdresseSigndp.Focus()
                    Exit Sub
                End If

                If EmailSigndp.IsRequiredControl("Veuillez saisir l'email du signataire") Then
                    EmailSigndp.Focus()
                    Exit Sub
                End If

                If TypeSignatairedp.Text.Trim = "" Then
                    SuccesMsg("Veuillez selectionné le type")
                    TypeSignatairedp.Select()
                    Exit Sub
                End If

                query = "Update t_signataire set NomPren='" & EnleverApost(NomSignatairedp.Text) & "', Contact='" & EnleverApost(ContactSigndp.Text) & "', Adresse='" & EnleverApost(AdresseSigndp.Text) & "', Email='" & EnleverApost(EmailSigndp.Text) & "', Fonction='" & EnleverApost(Txtfonctiondp.Text) & "', TypeSignataire='" & EnleverApost(TypeSignatairedp.Text) & "', DateModif='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', CodeUtils='" & CodeUtilisateur & "' where RefSignataire='" & ReponseDialog & "'"
                ExecuteNonQuery(query)

                SuccesMsg("Modification effecutée avec succès")
                BtAnnuler.PerformClick()
                Me.Close()
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub SuppressionCompteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SuppressionCompteToolStripMenuItem.Click
        If GridSignataire.RowCount > 0 Then
            If ConfirmMsg("Voulez-vous vraiment supprimer ce signataire ?") = DialogResult.Yes Then
                GridSignataire.Rows.RemoveAt(GridSignataire.CurrentRow.Index)
            End If
        End If
    End Sub
End Class