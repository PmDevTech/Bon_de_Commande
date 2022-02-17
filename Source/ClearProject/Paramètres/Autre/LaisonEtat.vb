Imports MySql.Data.MySqlClient

Public Class LaisonEtat

    Private Sub LaisonEtat_Load(sender As System.Object, e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        'remplir bailleur
        CombBail.Properties.Items.Clear()
        query = "select CodeBailleur, NomBailleur from t_bailleur where (CodeProjet='" & ProjetEnCours.ToString & "' and CodeBailleur in (select CodeBailleur from t_convention where CodeProjet='" & ProjetEnCours.ToString & "')) order by CodeBailleur"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CombBail.Properties.Items.Add(rw(0).ToString & " | " & MettreApost(rw(1).ToString))
        Next

        'remplir etat
        CombEtat.Properties.Items.Clear()
        query = "select * from t_etatbailleur where etat=0"
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        For Each rw1 As DataRow In dt1.Rows
            CombEtat.Properties.Items.Add(rw1(0).ToString & " | " & MettreApost(rw1(1).ToString))
        Next
    End Sub

    Private Sub btEnreg_Click(sender As System.Object, e As System.EventArgs) Handles btEnreg.Click
        Dim erreur As String = ""

        If Txtmodule.Text = "" Then
            erreur += "- Renseigner le Module" & ControlChars.CrLf
        End If

        If CombBail.SelectedIndex = -1 Then
            erreur += "- Choississer le bailleur" & ControlChars.CrLf
        End If

        If CombEtat.SelectedIndex = -1 Then
            erreur += "- Choississer un etat" & ControlChars.CrLf
        End If

        If erreur = "" Then

            Dim cb() As String
            cb = CombBail.Text.Split(" | ")

            Dim ce() As String
            ce = CombEtat.Text.Split(" | ")

            Dim DatSet = New DataSet
            query = "select * from  t_liaisonetat"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "t_liaisonetat")
            Dim DatTable = DatSet.Tables("t_liaisonetat")
            Dim DatRow = DatSet.Tables("t_liaisonetat").NewRow()

            DatRow("CodeBailleur") = cb(0).ToString
            DatRow("idetat") = ce(0).ToString
            DatRow("Module") = EnleverApost(Txtmodule.Text)

            DatSet.Tables("t_liaisonetat").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "t_liaisonetat")
            DatSet.Clear()
            BDQUIT(sqlconn)
            CombBail.Text = ""
            CombEtat.Text = ""
            Txtmodule.Text = ""

        Else
            MsgBox("Veuillez : " & ControlChars.CrLf + erreur, MsgBoxStyle.Exclamation)
        End If
    End Sub
End Class