Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Data.DataSet
Public Class Devise

    Dim PourAjout As Boolean = False
    Dim PourModif As Boolean = False
    Dim PourSupp As Boolean = False

    Public Sub ChargerListview()
        ListView1.Items.Clear()
        Try
            query = "SELECT * FROM T_Devise"

            'Charger la listview
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                Dim lva As New ListViewItem
                lva.Text = rw(2)
                lva.SubItems.Add(MettreApost(rw(1)))
                lva.SubItems.Add(rw(3))
                ListView1.Items.Add(lva)
            Next

        Catch ex As Exception
            FailMsg(ex.ToString())
        End Try
    End Sub

    Private Sub TxtTaux_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TxtTaux.KeyDown
        If e.KeyCode = Keys.Enter Then
            BtnEnregistrer_Click(Me, e)
        End If
    End Sub

    Private Sub TxtTaux_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtTaux.KeyPress
        If InStr("1234567890.", e.KeyChar) = 0 Or InStr(TxtTaux.Text, ".") <> 0 And e.KeyChar = "." Then
            e.KeyChar = "" : Beep()
        End If
    End Sub

    Private Sub Devise_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        EffacerText()
    End Sub

    Private Sub Devise_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ChargerListview()
    End Sub

    Private Sub BtnEnregistrer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEnregistrer.Click

        If (TxtAbrege.Text <> "" And TxtAbrege.Text <> "Code") And (Txtlibelle.Text <> "" And Txtlibelle.Text <> "Nom") And (TxtTaux.Text <> "" And TxtTaux.Text <> "Taux") Then

            'verification si l'abregé existe avant d'enregistrer
            Dim nbreligneAbrege As Byte
            Dim action As String = ""
            Dim madate = Now
            Dim dd = madate.ToString("H:mm:ss")

            query = "SELECT COUNT(*) FROM T_Devise WHERE AbregeDevise = '" & TxtAbrege.Text & "'"
            nbreligneAbrege = Val(ExecuteScallar(query))

            If nbreligneAbrege = 0 Then
                ' On enregistre
                Dim Element As New ListViewItem
                Element.Text = TxtAbrege.Text
                Element.SubItems.Add(MettreApost(Txtlibelle.Text))
                Element.SubItems.Add(TxtTaux.Text)

                ListView1.Items.Add(Element)

                Try


                    Dim DatSet = New DataSet
                    query = "SELECT * from T_Devise"
                    Dim sqlconn As New MySqlConnection
                    BDOPEN(sqlconn)
                    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                    Dim DatAdapt = New MySqlDataAdapter(Cmd)
                    DatAdapt.Fill(DatSet, "T_Devise")
                    Dim DatTable = DatSet.Tables("T_Devise")
                    Dim DatRow = DatSet.Tables("T_Devise").NewRow()

                    'enregistrement des TextBox dans les champs de la BD
                    TxtTaux.Text = TxtTaux.Text.Replace(".", ",")

                    DatRow("LibelleDevise") = EnleverApost(Txtlibelle.Text)
                    DatRow("AbregeDevise") = TxtAbrege.Text
                    DatRow("TauxDevise") = TxtTaux.Text
                    DatSet.Tables("T_Devise").Rows.Add(DatRow)
                    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)

                    DatAdapt.Update(DatSet, "T_Devise")
                    DatSet.Clear()
                    BDQUIT(sqlconn)

                    action = "Ajout de la devise: " & EnleverApost(Txtlibelle.Text) & ""

                    ''historique
                    'sql = "insert into t_historique values (NULL,'" & ProjetEnCours & "','" & NomUtilisateur & "','" & EnleverApost(action) & "','" & madate & "','" & dd & "')"
                    'ExecuteNonQuery(query)

                    SuccesMsg("Enregistrement effectué avec succès")
                    ChargerListview()
                    EffacerText()
                Catch ex As Exception
                    FailMsg(ex.ToString())
                End Try

            Else
                SuccesMsg("Ce code ou symbole existe déjà.")
            End If

        Else
            SuccesMsg("Veuillez remplir correctement tous les champs.")
        End If


    End Sub
    Private Sub TxtAbrege_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtAbrege.Click

        If TxtAbrege.Text = "Code" Then
            TxtAbrege.Text = ""
        End If

    End Sub
    Private Sub TxtAbrege_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtAbrege.TextChanged

        If TxtAbrege.Text <> "Code" And TxtAbrege.Text <> "" Then
            Txtlibelle.Enabled = True
        Else
            Txtlibelle.Enabled = False

        End If

    End Sub
    Private Sub Txtlibelle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txtlibelle.Click

        If Txtlibelle.Text = "Nom" Then
            Txtlibelle.Text = ""
        End If

    End Sub
    Private Sub Txtlibelle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txtlibelle.TextChanged

        If Txtlibelle.Text <> "Nom" And Txtlibelle.Text <> "" Then
            TxtTaux.Enabled = True
        Else
            TxtTaux.Enabled = False
        End If

    End Sub
    Private Sub TxtTaux_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtTaux.Click
        If TxtTaux.Text = "Taux" Then
            TxtTaux.Text = ""
        End If
    End Sub
    Private Sub TxtTaux_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtTaux.TextChanged
        If TxtTaux.Text <> "Taux" And TxtTaux.Text <> "" Then
            BtnEnregistrer.Enabled = True
        Else
            BtnEnregistrer.Enabled = False
        End If
    End Sub

    Private Sub BtAjout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjout.Click

        PourAjout = True
        PourModif = False
        PourSupp = False
        TxtAbrege.Enabled = True

        TxtAbrege.Text = "Code"
        TxtAbrege.Focus()
        Txtlibelle.Text = "Nom"
        TxtTaux.Text = "Taux"

        BtModif.Enabled = False
        BtSupprimer.Enabled = False
        BtRetour.Enabled = True

    End Sub

    Private Sub BtModif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtModif.Click

        'Code de modification de l'enregistrement choisi
        If (TxtAbrege.Text <> "" And TxtAbrege.Text <> "Code") And (Txtlibelle.Text <> "" And Txtlibelle.Text <> "Nom") And (TxtTaux.Text <> "" And TxtTaux.Text <> "Taux") Then

            Dim Abrege As String = ""
            Dim action As String = ""
            Dim madate = Now
            Dim dd = madate.ToString("H:mm:ss")

            With ListView1
                Abrege = .Items(.SelectedIndices(0)).SubItems(0).Text
            End With


            query = "UPDATE T_Devise SET LibelleDevise = '" & EnleverApost(Txtlibelle.Text) & "', AbregeDevise = '" & TxtAbrege.Text &
               "', TauxDevise = '" & TxtTaux.Text & "' WHERE AbregeDevise = '" & Abrege & "'"
            ExecuteNonQuery(query)

            action = "Modification de la devise: " & EnleverApost(Txtlibelle.Text) & ""

            ''historique
            'sql = "insert into t_historique values (NULL,'" & ProjetEnCours & "','" & NomUtilisateur & "','" & EnleverApost(action) & "','" & madate & "','" & dd & "')"
            'ExecuteNonQuery(query)


            SuccesMsg("Modification effectuée avec succès.")
            ChargerListview()
            EffacerText()

        Else
            SuccesMsg("Veuillez selectionner une ligne dans le tableau.")
        End If

    End Sub
    Private Sub ActiverTout()
        TxtAbrege.Enabled = True
        Txtlibelle.Enabled = True
        TxtTaux.Enabled = True
        BtnEnregistrer.Enabled = True
    End Sub

    Private Sub BtSupprimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSupprimer.Click

        'Si l'enregistrement n'est pas en cours d'utilisation alors supprimer
        If (TxtAbrege.Text <> "" And TxtAbrege.Text <> "Code") And (Txtlibelle.Text <> "" And Txtlibelle.Text <> "Nom") And (TxtTaux.Text <> "" And TxtTaux.Text <> "Taux") Then

            Dim Verif, NbreLigne1, NbreLigne2 As Decimal

            Dim Abrege As String = ""
            Dim action As String = ""
            Dim madate = Now
            Dim dd = madate.ToString("H:mm:ss")

            With ListView1
                Abrege = .Items(.SelectedIndices(0)).SubItems(0).Text
            End With

            'verifier que l'enregistrement n'est pas en cours d'utilisation
            Try
                query = "SELECT CodeDevise FROM T_Devise WHERE AbregeDevise ='" & Abrege & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw In dt.Rows
                    Verif = rw(0)
                Next

                query = "SELECT Count(*) FROM T_ZoneGeo WHERE CodeDevise ='" & Verif & "'"
                NbreLigne1 = Val(ExecuteScallar(query))

                query = "SELECT Count(*) FROM T_CompteBancaire WHERE CodeDevise ='" & Verif & "'"
                NbreLigne2 = Val(ExecuteScallar(query))

            Catch ex As Exception
                MessageBox.Show(ex.ToString(), "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try


            If NbreLigne1 < 1 And NbreLigne2 < 1 Then

                If MsgBox("Voulez-vous vraiment supprimer?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                    ListView1.Items.Remove(ListView1.SelectedItems(0))

                    query = "DELETE FROM T_Devise WHERE CodeDevise = '" & Verif & "'"
                    ExecuteNonQuery(query)

                    action = "Suppression de la devise: " & EnleverApost(Txtlibelle.Text) & ""

                    ''historique
                    'sql = "insert into t_historique values (NULL,'" & ProjetEnCours & "','" & NomUtilisateur & "','" & EnleverApost(action) & "','" & madate & "','" & dd & "')"
                    'ExecuteNonQuery(query)

                    SuccesMsg("Suppression effectuée avec succès")
                    ChargerListview()
                    EffacerText()
                End If
            Else
                MsgBox("Impossible de supprimer car cette devise est utilisée", MsgBoxStyle.Exclamation)
            End If

        Else
            MsgBox("Veuillez selectionner une ligne dans le tableau !", MsgBoxStyle.Exclamation)
        End If


    End Sub

    Private Sub BtRetour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRetour.Click
        EffacerText()
    End Sub

    Public Sub EffacerText()
        TxtAbrege.Enabled = False
        TxtAbrege.Text = ""
        Txtlibelle.Enabled = False
        Txtlibelle.Text = ""
        TxtTaux.Enabled = False
        TxtTaux.Text = ""
        BtnEnregistrer.Enabled = False
        BtRetour.Enabled = False
        BtAjout.Enabled = True
        BtModif.Enabled = True
        BtSupprimer.Enabled = True
        PourAjout = False
        PourModif = False
        PourSupp = False
    End Sub

    Private Sub ListView1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.Click

            ActiverTout()

            With ListView1
            If .SelectedIndices.Count > 0 Then
                .Items(.SelectedIndices(0)).Selected = True
                TxtAbrege.Text = .Items(.SelectedIndices(0)).SubItems(0).Text
                Txtlibelle.Text = .Items(.SelectedIndices(0)).SubItems(1).Text
                TxtTaux.Text = .Items(.SelectedIndices(0)).SubItems(2).Text
                BtnEnregistrer.Enabled = False
                BtRetour.Enabled = True
            End If
            End With


    End Sub

    Private Sub Devise_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub
End Class