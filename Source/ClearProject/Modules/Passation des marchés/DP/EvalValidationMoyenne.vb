Imports MySql.Data.MySqlClient

Public Class EvalValidationMoyenne

    Dim ListeValide(6) As String
    Dim NomListe(6) As String
    Dim OkValide(6) As String
    Dim NbreListe As Decimal = 0

    Private Sub EvalValidationMoyenne_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        ChargerListe()


    End Sub

    Private Sub VerifList()

        Dim ToutOk As Boolean = True
        For k As Integer = 0 To NbreListe - 1
            If (OkValide(k) = "OK") Then
                If (k = 0) Then
                    ChkAppro1.Checked = True
                    TxtPasse1.Enabled = False
                End If

                If (k = 1) Then
                    ChkAppro2.Checked = True
                    TxtPasse2.Enabled = False
                End If

                If (k = 2) Then
                    ChkAppro3.Checked = True
                    TxtPasse3.Enabled = False
                End If

                If (k = 3) Then
                    ChkAppro4.Checked = True
                    TxtPasse4.Enabled = False
                End If

                If (k = 4) Then
                    ChkAppro5.Checked = True
                    TxtPasse5.Enabled = False
                End If

                If (k = 5) Then
                    ChkAppro6.Checked = True
                    TxtPasse6.Enabled = False
                End If
            Else
                ToutOk = False
            End If
        Next

        If (ToutOk = True) Then
            BtValider.Enabled = True
        Else
            BtValider.Enabled = False
        End If

    End Sub

    Private Sub ChargerListe()

        query = "select CodeMem,NomMem,PasseMem,TitreMem from T_Commission where NumeroDAO='" & EvaluationConsultants.CmbNumDoss.Text & "' and TypeComm='EVAC'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            NomListe(NbreListe) = MettreApost(rw(1).ToString) & " (" & rw(3).ToString & ")"
            ListeValide(NbreListe) = rw(2).ToString
            OkValide(NbreListe) = ""
            NbreListe += 1

            If (NbreListe = 1) Then
                LblEval1.Visible = True
                LblEval1.Text = MettreApost(rw(1).ToString) & " (" & rw(3).ToString & ")"
                TxtPasse1.Visible = True
                ChkAppro1.Visible = True
            End If

            If (NbreListe = 2) Then
                LblEval2.Visible = True
                LblEval2.Text = MettreApost(rw(1).ToString) & " (" & rw(3).ToString & ")"
                TxtPasse2.Visible = True
                ChkAppro2.Visible = True
            End If

            If (NbreListe = 3) Then
                LblEval3.Visible = True
                LblEval3.Text = MettreApost(rw(1).ToString) & " (" & rw(3).ToString & ")"
                TxtPasse3.Visible = True
                ChkAppro3.Visible = True
            End If

            If (NbreListe = 4) Then
                LblEval4.Visible = True
                LblEval4.Text = MettreApost(rw(1).ToString) & " (" & rw(3).ToString & ")"
                TxtPasse4.Visible = True
                ChkAppro4.Visible = True
            End If

            If (NbreListe = 5) Then
                LblEval5.Visible = True
                LblEval5.Text = MettreApost(rw(1).ToString) & " (" & rw(3).ToString & ")"
                TxtPasse5.Visible = True
                ChkAppro5.Visible = True
            End If

            If (NbreListe = 6) Then
                LblEval6.Visible = True
                LblEval6.Text = MettreApost(rw(1).ToString) & " (" & rw(3).ToString & ")"
                TxtPasse6.Visible = True
                ChkAppro6.Visible = True
            End If

        Next


    End Sub

    Private Sub TxtPasse1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPasse1.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            If (ListeValide(0) = TxtPasse1.Text) Then
                OkValide(0) = "OK"
                TxtPasse1.Text = ""
                VerifList()
            Else
                MsgBox("Accès réfusé!", MsgBoxStyle.Exclamation)
                TxtPasse1.Text = ""
                TxtPasse1.Select()
            End If

        End If
    End Sub

    Private Sub TxtPasse2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPasse2.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            If (ListeValide(1) = TxtPasse2.Text) Then
                OkValide(1) = "OK"
                TxtPasse2.Text = ""
                VerifList()
            Else
                MsgBox("Accès réfusé!", MsgBoxStyle.Exclamation)
                TxtPasse2.Text = ""
                TxtPasse2.Select()
            End If

        End If
    End Sub

    Private Sub TxtPasse3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPasse3.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            If (ListeValide(2) = TxtPasse3.Text) Then
                OkValide(2) = "OK"
                TxtPasse3.Text = ""
                VerifList()
            Else
                MsgBox("Accès réfusé!", MsgBoxStyle.Exclamation)
                TxtPasse3.Text = ""
                TxtPasse3.Select()
            End If

        End If
    End Sub

    Private Sub TxtPasse4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPasse4.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            If (ListeValide(3) = TxtPasse4.Text) Then
                OkValide(3) = "OK"
                TxtPasse4.Text = ""
                VerifList()
            Else
                MsgBox("Accès réfusé!", MsgBoxStyle.Exclamation)
                TxtPasse4.Text = ""
                TxtPasse4.Select()
            End If

        End If
    End Sub

    Private Sub TxtPasse5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPasse5.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            If (ListeValide(4) = TxtPasse5.Text) Then
                OkValide(4) = "OK"
                TxtPasse5.Text = ""
                VerifList()
            Else
                MsgBox("Accès réfusé!", MsgBoxStyle.Exclamation)
                TxtPasse5.Text = ""
                TxtPasse5.Select()
            End If

        End If
    End Sub

    Private Sub TxtPasse6_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPasse6.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            If (ListeValide(5) = TxtPasse6.Text) Then
                OkValide(5) = "OK"
                TxtPasse6.Text = ""
                VerifList()
            Else
                MsgBox("Accès réfusé!", MsgBoxStyle.Exclamation)
                TxtPasse6.Text = ""
                TxtPasse6.Select()
            End If

        End If
    End Sub

    Private Sub BtValider_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtValider.Click

        Dim DatSet = New DataSet
        query = "select * from T_DP where NumeroDp='" & EvaluationConsultants.CmbNumDoss.Text & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)
        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd)
        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        DatAdapt.Fill(DatSet, "T_DP")

        DatSet.Tables!T_DP.Rows(0)!EvalTechnique = Now.ToShortDateString & " " & Now.ToLongTimeString

        DatAdapt.Update(DatSet, "T_DP")
        DatSet.Clear()
        BDQUIT(sqlconn)

        Me.Close()
    End Sub

    Private Sub BtQuitter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        Me.Close()
    End Sub
End Class