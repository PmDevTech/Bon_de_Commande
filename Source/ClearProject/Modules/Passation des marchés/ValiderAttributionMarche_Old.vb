Imports MySql.Data.MySqlClient

Public Class ValiderAttributionMarche_Old

    'Dim Contrl() As DevExpress.XtraEditors.CheckEdit = {Eval1, Eval2, Eval3, Eval4}
    'Dim laRefLot As String = ""
    Dim ListeValide(10) As String
    Dim NomListe(10) As String
    Dim OkValide(10) As String
    Dim NbreListe As Decimal = 0

    Private Sub ValiderAttributionMarche_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        'NbreListe = 0
        'For k As Integer = 0 To 9
        '    ListeValide(k) = ""
        '    NomListe(k) = ""
        '    OkValide(k) = ""
        'Next

        'Eval3.Checked = False
        'Eval1.Checked = False
        'Eval2.Checked = False
        'Eval4.Checked = False
        'Eval5.Checked = False

        'Attente.Visible = False
        'Avertissement.Visible = False
        'BtAttribuer.Enabled = False
        'TxtCode.Enabled = True
        'TxtCode.Properties.ReadOnly = False

        ChargerListe()
        VerifierListe()
        ListeOk()
        'TxtCode.Focus()

    End Sub

    Private Sub ListeOk()

        Eval3.Checked = False
        Eval1.Checked = False
        Eval2.Checked = False
        Eval4.Checked = False
        Eval5.Checked = False

        Dim ValRet As Boolean = True
        For k As Integer = 0 To NbreListe - 1
            If (OkValide(k) <> "OK") Then
                ValRet = False
            End If
        Next

        'If (OkValide(0) = "OK") Then
        '    Coordo.Checked = True
        'Else
        '    Coordo.Checked = False
        'End If

        If (OkValide(0) = "OK" And NbreListe >= 1) Then Eval1.Checked = True
        If (OkValide(1) = "OK" And NbreListe >= 2) Then Eval2.Checked = True
        If (OkValide(2) = "OK" And NbreListe >= 3) Then Eval3.Checked = True
        If (OkValide(3) = "OK" And NbreListe >= 4) Then Eval4.Checked = True
        If (OkValide(4) = "OK" And NbreListe >= 5) Then Eval5.Checked = True

        If (ValRet = True) Then
            BtAttribuer.Enabled = True
            TxtCode.Text = ""
            TxtCode.Properties.ReadOnly = True
            BtAttribuer.Focus()
        Else
            BtAttribuer.Enabled = False
            TxtCode.Text = ""
            TxtCode.Properties.ReadOnly = False
            TxtCode.Focus()
        End If

        If (ValRet = True) Then
            Attente.Visible = True
            Avertissement.Visible = True
        Else
            Attente.Visible = False
            Avertissement.Visible = False
        End If


    End Sub

    Private Sub VerifierListe()
        'query = "select NomValidation from T_LotValidationMarche where NumeroDAO='" & JugementOffres.CmbNumDoss.Text & "' and CodeLot='" & JugementOffres.CmbNumLotAttrib.Text & "'"
        query = "select S.CodeMem,S.NomMem from T_Commission as S , t_lotvalidationrapportcojo as F where F.Id_COJO=S.CodeMem and S.NumeroDAO='" & JugementOffres.CmbNumDoss.Text & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            For k As Integer = 0 To NbreListe - 1
                If (NomListe(k) = MettreApost(rw(0).ToString)) Then
                    OkValide(k) = "OK"
                End If
            Next
        Next
    End Sub

    Private Sub ChargerListe()

        Dim cpt As Decimal = 0
        query = "select CodeMem,NomMem from T_Commission where NumeroDAO='" & JugementOffres.CmbNumDoss.Text & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            NomListe(cpt) = MettreApost(rw(0).ToString & " " & rw(1).ToString)
            ListeValide(cpt) = rw(2).ToString
            cpt += 1
        Next
        NbreListe = cpt
    End Sub

    Private Sub TxtCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtCode.KeyDown

        If (e.KeyCode = Keys.Enter) Then

            For k As Integer = 0 To NbreListe - 1
                If (ListeValide(k) = TxtCode.Text) Then
                    If (OkValide(k) = "OK") Then
                        SuccesMsg("Code déjà saisie!")
                    Else
                        Dim DatSet = New DataSet
                        query = "select * from T_LotValidationMarche"
                        Dim sqlconn As New MySqlConnection
                        BDOPEN(sqlconn)
                        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                        Dim DatAdapt = New MySqlDataAdapter(Cmd)
                        DatAdapt.Fill(DatSet, "T_LotValidationMarche")
                        Dim DatTable = DatSet.Tables("T_LotValidationMarche")
                        Dim DatRow = DatSet.Tables("T_LotValidationMarche").NewRow()

                        DatRow("NumeroDAO") = JugementOffres.CmbNumDoss.Text
                        DatRow("CodeLot") = JugementOffres.CmbNumLotAttrib.Text
                        DatRow("NomValidation") = EnleverApost(NomListe(k))
                        DatRow("DateValidation") = Now.ToShortDateString & " " & Now.ToShortTimeString

                        DatSet.Tables("T_LotValidationMarche").Rows.Add(DatRow)
                        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                        DatAdapt.Update(DatSet, "T_LotValidationMarche")
                        DatSet.Clear()
                        BDQUIT(sqlconn)
                        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
                    End If
                End If
            Next

            VerifierListe()
            ListeOk()

            'TxtCode.Text = ""
            'TxtCode.Focus()

        End If

    End Sub

    Private Sub BtAnnuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAnnuler.Click
        ReponseDialog = ""
        Me.Close()
    End Sub

    Private Sub BtAttribuer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAttribuer.Click
        ReponseDialog = "OK"
        Me.Close()
    End Sub

    Private Sub Avertissement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Avertissement.Click
        BtAttribuer.Focus()
    End Sub
End Class