Imports MySql.Data.MySqlClient
Imports System.IO


Public Class Manuels

    Private Sub SimpleButton4_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton4.Click
        Dim dlg As New OpenFileDialog
        dlg.ShowDialog()
        Dim fichier As String = dlg.FileName
        Dim NomComp As String() = fichier.Split("\"c)
        Dim Nbr As Decimal = 0
        For Each Elt In NomComp
            Nbr = Nbr + 1
        Next
        TxtPieceJointe.Text = NomComp(Nbr - 1)
        TxtChemin.Text = fichier
    End Sub

    Private Sub Manuels_Load(sender As System.Object, e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ChargerTypeManuel()
    End Sub

    Private Sub ChargerTypeManuel()

        CombType.Properties.Items.Clear()
        query = "select libelle_tm from t_typedoc"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CombType.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next

    End Sub

    Private Sub CombType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CombType.SelectedIndexChanged

       query= "select id_tm from t_typedoc where libelle_tm ='" & EnleverApost(CombType.Text) & "'"
        txtcodetm.Text = ExecuteScallar(query)

    End Sub

    Private Sub BtEnregistrer_Click(sender As System.Object, e As System.EventArgs) Handles BtEnregistrer.Click

        Dim NomFichier As String = line & "\documentsProjet\" & CombType.Text

        If (TxtPieceJointe.Text <> "" And CombType.SelectedIndex <> -1) Then
            NomFichier = NomFichier & "\" & TxtPieceJointe.Text
            File.Copy(TxtChemin.Text, NomFichier, True)

            query = "insert into t_manuel values (NULL,'" & txtcodetm.Text & "','" & EnleverApost(TxtPieceJointe.Text) & "','" & EnleverApost(NomFichier) & "')"
            ExecuteNonQuery(query)

            CombType.Text = ""
            TxtPieceJointe.Text = ""

            SuccesMsg("Enregistement effectué avec succès.")
        End If
    End Sub

    Private Sub BtRetour_Click(sender As System.Object, e As System.EventArgs) Handles BtRetour.Click
        CombType.Text = ""
        TxtPieceJointe.Text = ""
    End Sub

    Private Sub SimpleButton3_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton3.Click
        paramètre.GroupControl1.Text = "type documents"
        paramètre.Size = New Point(485, 450)
        Dialog_form(paramètre)
    End Sub
End Class