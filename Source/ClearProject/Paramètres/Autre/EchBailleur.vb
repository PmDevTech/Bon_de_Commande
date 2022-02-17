Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System
'Imports AxMicrosoft
Imports Microsoft
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.Math
Imports System.Text.RegularExpressions
Public Class EchBailleur

    'Private Sub EchBailleur_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
    'Me.Icon = My.Resources.Logo_ClearProject_Valide
    '    Dim NbBail As Decimal = PlanDecaissement.ViewEcheanceBailleur.RowCount
    '    CodeEchCache.Text = PubCodeEch.ToString
    '    MontEchCache.Text = PubMontEch.Replace(" ", "")

    '    TxtMontBail1.Text = "0"
    '    TxtMontBail2.Text = "0"
    '    TxtMontBail3.Text = "0"
    '    TxtMontBail4.Text = "0"

    '    If (NbBail < 4) Then
    '        TxtBailleur4.Visible = False
    '        TxtMontBail4.Visible = False
    '        TxtMontTotBail4.Visible = False
    '    End If
    '    If (NbBail < 3) Then
    '        TxtBailleur3.Visible = False
    '        TxtMontBail3.Visible = False
    '        TxtMontTotBail3.Visible = False
    '    End If

    '    For i As Integer = 0 To NbBail - 1
    '        If (i = 0) Then
    '            CodeBail1Cache.Text = PlanDecaissement.ViewEcheanceBailleur.GetDataRow(i)(1).ToString
    '            TxtBailleur1.Text = PlanDecaissement.ViewEcheanceBailleur.GetDataRow(i)(2).ToString
    '            TxtMontTotBail1.Text = "/ " & AfficherMonnaie(PlanDecaissement.ViewEcheanceBailleur.GetDataRow(i)(4).ToString.Replace(" ", ""))
    '        End If
    '        If (i = 1) Then
    '            CodeBail2Cache.Text = PlanDecaissement.ViewEcheanceBailleur.GetDataRow(i)(1).ToString
    '            TxtBailleur2.Text = PlanDecaissement.ViewEcheanceBailleur.GetDataRow(i)(2).ToString
    '            TxtMontTotBail2.Text = "/ " & AfficherMonnaie(PlanDecaissement.ViewEcheanceBailleur.GetDataRow(i)(4).ToString.Replace(" ", ""))
    '        End If
    '        If (i = 2) Then
    '            CodeBail3Cache.Text = PlanDecaissement.ViewEcheanceBailleur.GetDataRow(i)(1).ToString
    '            TxtBailleur3.Text = PlanDecaissement.ViewEcheanceBailleur.GetDataRow(i)(2).ToString
    '            TxtMontTotBail3.Text = "/ " & AfficherMonnaie(PlanDecaissement.ViewEcheanceBailleur.GetDataRow(i)(4).ToString.Replace(" ", ""))
    '        End If
    '        If (i = 3) Then
    '            CodeBail4Cache.Text = PlanDecaissement.ViewEcheanceBailleur.GetDataRow(i)(1).ToString
    '            TxtBailleur4.Text = PlanDecaissement.ViewEcheanceBailleur.GetDataRow(i)(2).ToString
    '            TxtMontTotBail4.Text = "/ " & AfficherMonnaie(PlanDecaissement.ViewEcheanceBailleur.GetDataRow(i)(4).ToString.Replace(" ", ""))
    '        End If
    '    Next

    '    TxtGdTotEch.Text = "/ " & AfficherMonnaie(MontEchCache.Text.Replace(" ", ""))

    'End Sub

    'Private Sub TxtMontBail1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontBail1.Click
    '    If (TxtMontBail1.Text = "0") Then
    '        TxtMontBail1.Text = ""
    '        TxtMontBail1.Select()
    '    End If

    'End Sub

    'Private Sub TxtMontBail1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtMontBail1.TextChanged
    '    If (Me.Visible = True) Then

    '        VerifSaisieMontant(TxtMontBail1)
    '        CalculDuTotal()

    '    End If

    'End Sub

    'Private Sub TxtMontBail2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontBail2.Click
    '    If (TxtMontBail2.Text = "0") Then
    '        TxtMontBail2.Text = ""
    '        TxtMontBail2.Select()
    '    End If
    'End Sub

    'Private Sub TxtMontBail2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtMontBail2.TextChanged
    '    If (Me.Visible = True) Then
    '        VerifSaisieMontant(TxtMontBail2)
    '        CalculDuTotal()
    '    End If

    'End Sub

    'Private Sub TxtMontBail3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontBail3.Click
    '    If (TxtMontBail3.Text = "0") Then
    '        TxtMontBail3.Text = ""
    '        TxtMontBail3.Select()
    '    End If
    'End Sub

    'Private Sub TxtMontBail3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtMontBail3.TextChanged
    '    If (Me.Visible = True) Then
    '        VerifSaisieMontant(TxtMontBail3)
    '        CalculDuTotal()
    '    End If

    'End Sub

    'Private Sub TxtMontBail4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontBail4.Click
    '    If (TxtMontBail4.Text = "0") Then
    '        TxtMontBail4.Text = ""
    '        TxtMontBail4.Select()
    '    End If
    'End Sub

    'Private Sub TxtMontBail4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtMontBail4.TextChanged
    '    If (Me.Visible = True) Then
    '        VerifSaisieMontant(TxtMontBail4)
    '        CalculDuTotal()
    '    End If

    'End Sub
    'Private Sub CalculDuTotal()

    '    If (Me.Visible = True) Then
    '        BtOk.Enabled = True
    '        Dim TotalCalcule As Decimal = 0
    '        If (TxtMontBail1.Text <> "") Then
    '            TotalCalcule = TotalCalcule + CDec(TxtMontBail1.Text.Replace(" ", ""))
    '        End If
    '        If (TxtMontBail2.Text <> "") Then
    '            TotalCalcule = TotalCalcule + CDec(TxtMontBail2.Text.Replace(" ", ""))
    '        End If
    '        If (TxtMontBail3.Visible = True And TxtMontBail3.Text <> "") Then
    '            TotalCalcule = TotalCalcule + CDec(TxtMontBail3.Text.Replace(" ", ""))
    '        End If
    '        If (TxtMontBail4.Visible = True And TxtMontBail4.Text <> "") Then
    '            TotalCalcule = TotalCalcule + CDec(TxtMontBail4.Text.Replace(" ", ""))
    '        End If

    '        If (TotalCalcule = CDec(MontEchCache.Text.Replace(" ", ""))) Then
    '            PictureTrue.Visible = True
    '            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
    '        Else
    '            PictureTrue.Visible = False
    '        End If

    '        If (TotalCalcule > CDec(MontEchCache.Text.Replace(" ", ""))) Then
    '            TxtTotEchBail.Text = "ERREUR TOTAL"
    '            BtOk.Enabled = False
    '            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Question)
    '        Else
    '            TxtTotEchBail.Text = AfficherMonnaie(TotalCalcule.ToString)
    '        End If
    '    End If

    'End Sub
    'Private Sub EnregEchBailleur(ByVal KodeBail As Decimal, ByVal MonttBail As String)
    '    Dim DatSet = New DataSet

    '    query = "select * from T_EcheanceBailleur"
    '    Dim sqlconn As New MySqlConnection
    '    BDOPEN(sqlconn)
    '    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
    '    Dim DatAdapt = New MySqlDataAdapter(Cmd)
    '    DatAdapt.Fill(DatSet, "T_EcheanceBailleur")
    '    Dim DatTable = DatSet.Tables("T_EcheanceBailleur")
    '    Dim DatRow = DatSet.Tables("    ").NewRow()

    '    DatRow("RefEcheance") = CodeEchCache.Text
    '    DatRow("CodeBailleur") = KodeBail
    '    DatRow("MontantBailleur") = MonttBail

    '    DatSet.Tables("T_EcheanceBailleur").Rows.Add(DatRow) 'ajout d'une nouvelle ligne 
    '    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt) 'execution de l'enregistrement
    '    DatAdapt.Update(DatSet, "T_EcheanceBailleur")
    '    DatSet.Clear()
    '    BDQUIT(sqlconn)
    'End Sub
    'Private Sub BtOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtOk.Click
    '    If (CDec(TxtTotEchBail.Text.Replace(" ", "")) = CDec(MontEchCache.Text.Replace(" ", ""))) Then
    '        If (TxtBailleur1.Visible = True) Then
    '            EnregEchBailleur(CodeBail1Cache.Text, TxtMontBail1.Text.Replace(" ", ""))
    '        End If
    '        If (TxtBailleur2.Visible = True) Then
    '            EnregEchBailleur(CodeBail2Cache.Text, TxtMontBail2.Text.Replace(" ", ""))
    '        End If
    '        If (TxtBailleur3.Visible = True) Then
    '            EnregEchBailleur(CodeBail3Cache.Text, TxtMontBail3.Text.Replace(" ", ""))
    '        End If
    '        If (TxtBailleur4.Visible = True) Then
    '            EnregEchBailleur(CodeBail4Cache.Text, TxtMontBail4.Text.Replace(" ", ""))
    '        End If
    '        Me.Close()
    '    Else
    '        MsgBox("Répartition incorrecte!", MsgBoxStyle.Information)
    '        Exit Sub
    '    End If

    'End Sub

    'Private Sub BtAnnuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAnnuler.Click
    '    Dim Rep1 As MsgBoxResult = MsgBox("Voulez-vous vraiment annuler la répartition de cette activité ?", MsgBoxStyle.YesNo)
    '    If (Rep1 = MsgBoxResult.Yes) Then
    '        query = "DELETE from T_EcheanceActivite where CodePartition='" & PlanDecaissement.ViewEcheanceActivite.GetDataRow(PlanDecaissement.ViewEcheanceActivite.FocusedRowHandle)(1).ToString & "'"
    '        ExecuteNonQuery(query)

    '        Me.Close()
    '    Else
    '        Exit Sub
    '    End If
    'End Sub
End Class