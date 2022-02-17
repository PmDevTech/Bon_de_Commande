Imports System.IO
Imports MySql.Data.MySqlClient

Public Class LicenceUtilisateur
    Dim loc As Point
    Dim ErrorMsg As String
    Private Sub CodeAcces_MouseMove(sender As Object, e As MouseEventArgs) Handles LblFinGlob.MouseMove, MyBase.MouseMove, Label2.MouseMove
        If e.Button = MouseButtons.Left Then
            Dim newLoc As Point
            newLoc.X = Location.X + (e.X - loc.X)
            newLoc.Y = Location.Y + (e.Y - loc.Y)
            Location = newLoc
        End If
    End Sub
    Private Sub CodeAcces_MouseDown(sender As Object, e As MouseEventArgs) Handles LblFinGlob.MouseDown, MyBase.MouseDown, Label2.MouseDown
        loc = New Point(e.X, e.Y)
    End Sub

    Private Sub btNext_Click(sender As Object, e As EventArgs) Handles btNext.Click
        CloseError()
        If txtKey.Text.Length <> 23 Then
            MakeError("La clé saisie n'a pas le bon format.")
            txtKey.Select()
            Exit Sub
        Else
            DebutChargement(True, "Vérification de la licence")
            Dim obj As Object = VerifLicence(txtKey.Text)
            If CBool(obj(0)) = False Then
                FinChargement()
                MakeError(obj(1))
                Exit Sub
            End If
        End If
        Me.DialogResult = DialogResult.OK
        FinChargement()
        Me.Close()

    End Sub
    Private Sub MakeError(ByVal ErrMessage As String)
        If Not TxtError.Visible Then
            TxtError.Visible = True
        End If
        TxtError.Text = ErrMessage
    End Sub
    Private Sub CloseError()
        If TxtError.Visible Then
            TxtError.Visible = False
        End If
        TxtError.ResetText()
    End Sub

    Private Function VerifLicence(Key As String) As Object
        Dim ConnectServer As New MySqlConnection
        If ConnecteServer(ConnectServer) = False Then
            Return {False, "Impossible de se connecter au serveur de licence."}
        End If
        Dim TypeProduit As String = String.Empty
        If DB.Length >= 2 Then
            Try
                If Mid(DB, 1, 2).ToLower() = "bd" Then
                    TypeProduit = "ClearProject"
                ElseIf Mid(DB, 1, 2).ToLower() = "cc" Then
                    TypeProduit = "ClearCompta"
                ElseIf Mid(DB, 1, 3).ToLower() = "gst" Then
                    TypeProduit = "ClearProject"
                ElseIf Mid(DB, 1, 3).ToLower() = "crh" Then
                    TypeProduit = "ClearProject"
                ElseIf Mid(DB, 1, 2).ToLower() = "gspro" Then
                    TypeProduit = "ClearProjectPro"
                ElseIf Mid(DB, 1, 2).ToLower() = "cimm" Then
                    TypeProduit = "ClearProject"
                Else
                    Return {False, "Votre produit est inconnu"}
                End If
            Catch ex As Exception
                Return {False, "Votre produit est inconnu"}
            End Try
        Else
            Return {False, "Votre produit est inconnu"}
        End If
        Dim query = "SELECT NbreUser,ResteUser FROM t_licences_users WHERE licence_key='" & Key & "' AND CodeProjet='" & ProjetEnCours & "' AND TypeProduit='" & TypeProduit & "'"
        Dim dt As New DataTable
        Try
            dt = ExecuteSelectQueryServer(query)
        Catch ex As Exception
            Return {False, "Impossible de se connecter au serveur de licence."}
        End Try

        If dt.Rows.Count <= 0 Then
            Return {False, "La clé saisie ne fonctionne pas."}
        ElseIf dt.Rows.Count > 1 Then
            Return {False, "La clé saisie est corrompue."}
        Else
            Dim row As DataRow = dt.Rows(0)
            query = "SELECT COUNT(*) as Counted FROM t_operateur WHERE licence_key='" & Key & "'"
            Dim dtUsers As DataTable = ExcecuteSelectQuery(query)
            Dim rw As DataRow = dtUsers.Rows(0)
            Dim NbUsersLocal As Decimal = Val(rw("Counted"))
            Dim ResteUser As Decimal = Val(row("NbreUser")) - NbUsersLocal
            If ResteUser <> Val(row("ResteUser")) Then
                query = "UPDATE t_licences_users SET ResteUser='" & ResteUser & "' WHERE licence_key='" & Key & "' AND CodeProjet='" & ProjetEnCours & "' AND TypeProduit='" & TypeProduit & "'"
                Try
                    ExecuteNonQueryServer(query)
                Catch ex As Exception
                    Return {False, "Impossible de se connecter au serveur de licence."}
                End Try
            End If
            If ResteUser <= 0 Then
                Return {False, "Cette licence d'utilisateur est complète."}
            End If

            Return {True, "La clé saisie est correct."}
        End If
    End Function
    Public Function ExecuteSelectQueryServer(ByVal query As String) As DataTable
        Dim Connection As New MySqlConnection
        ConnecteServer(Connection)
        Try
            Dim cmd As MySqlCommand = New MySqlCommand(query, Connection)
            Dim dt As New DataTable
            Dim Adapt As New MySqlDataAdapter(cmd)
            Adapt.Fill(dt)
            Connection.Close()
            Return dt
        Catch ex As Exception
            ErrorMsg = ex.ToString
            Throw ex
        End Try
    End Function
    Public Function ExecuteScalarServer(ByVal query As String) As String
        Dim Connection As New MySqlConnection
        ConnecteServer(Connection)
        Try
            Dim cmd As MySqlCommand = New MySqlCommand(query, Connection)
            Dim res As String = cmd.ExecuteScalar()
            Connection.Close()
            Return res
        Catch null As InvalidCastException
            Return String.Empty
        Catch ex As Exception
            ErrorMsg = ex.ToString
            Throw ex
        End Try
    End Function
    Public Function ExecuteNonQueryServer(ByVal query As String) As Integer
        Dim Connection As New MySqlConnection
        ConnecteServer(Connection)
        Try
            Dim cmd As MySqlCommand = New MySqlCommand(query, Connection)
            Dim res As Integer = cmd.ExecuteNonQuery()
            Connection.Close()
            Return res
        Catch ex As Exception
            ErrorMsg = ex.ToString
            Throw ex
        End Try
    End Function

    Private Sub btClose_Click(sender As Object, e As EventArgs) Handles btClose.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub LicenceUtilisateur_Load(sender As Object, e As EventArgs) Handles Me.Load
        ErrorMsg = String.Empty
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If TxtError.Text.Trim.Length = 0 Then
            e.Cancel = True
        ElseIf TxtError.BackColor = Color.Red Then
            If TxtError.Text.Length >= 10 Then
                If Mid(TxtError.Text, 1, 10) = "Impossible" Then
                    CopierLeMessageDerreurToolStripMenuItem.Visible = True
                Else
                    e.Cancel = True
                End If
            Else
                e.Cancel = True
            End If
        End If
    End Sub
End Class
