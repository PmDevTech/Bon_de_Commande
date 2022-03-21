Imports MySql.Data.MySqlClient

Public Class SelectConsultant

    Dim DossNuméro As String = ""

    Private Sub SelectConsultant_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        If (ReponseDialog = "") Then
            Me.Close()
        Else
            DossNuméro = ReponseDialog
            ReponseDialog = ""
        End If

        ChargerConsult()
        ExceptRevue = ""

    End Sub

    Private Sub ChargerConsult()

        query = "select NomConsult,PaysConsult from T_Consultant where NumeroDp='" & DossNuméro & "' and DateDepot='' order by PaysConsult"
        CmbConsult.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbConsult.Properties.Items.Add("[" & MettreApost(rw(1).ToString) & "] " & MettreApost(rw(0).ToString))
        Next
        CmbConsult.Properties.Items.Add("Afficher le dossier type")

    End Sub

    Private Sub CmbConsult_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbConsult.SelectedValueChanged
        If (CmbConsult.SelectedText <> "") Then
            If (CmbConsult.SelectedText = "Afficher le dossier type") Then
                TxtAdresseConsult.Text = "Le dossier qui sera chargé ne peut être imprimé!"
                ReponseDialog = "[Nom et adresse du consultant]"
            Else
                Dim LeNom() As String = CmbConsult.SelectedText.Split("]"c)
                query = "select AdressConsult,TelConsult,FaxConsult,EmailConsult from T_Consultant where NumeroDp='" & DossNuméro & "' and NomConsult='" & Mid(LeNom(1), 2) & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    If (rw(0).ToString <> "") Then TxtAdresseConsult.Text = MettreApost(rw(0).ToString)
                    If (rw(1).ToString <> "") Then TxtAdresseConsult.Text = TxtAdresseConsult.Text & vbNewLine & "Tel: " & rw(1).ToString
                    If (rw(2).ToString <> "") Then TxtAdresseConsult.Text = TxtAdresseConsult.Text & vbNewLine & "Fax: " & rw(2).ToString
                    If (rw(3).ToString <> "") Then TxtAdresseConsult.Text = TxtAdresseConsult.Text & vbNewLine & "E.mail: " & rw(3).ToString

                    ReponseDialog = Mid(LeNom(1), 2) & ", " & MettreApost(rw(0).ToString)
                    ExceptRevue = (((Mid(LeNom(1), 2).Replace("/", "_")).Replace("\", "_")).Replace(".", "_")).Replace("&", "_")
                Next

            End If
        End If
    End Sub

    Private Sub BtOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtOk.Click
        Me.Close()
    End Sub

    Private Sub BtQuitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        ReponseDialog = ""
        Me.Close()
    End Sub

End Class