Imports Microsoft
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.Math
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class SeuilRevue

    Private Sub SeuilRevue_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        ' RemplirSeuil()
        BtAjouter.Enabled = True
        BtSupprimer.Enabled = False
        SeuilGrid.Rows.Clear()
        RemplirBailleur()
    End Sub

    Private Sub RemplirBailleur()
        query = "select InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' order by InitialeBailleur"
        ComBailleur.Properties.Items.Clear()
        ComBailleur.Text = ""
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            ComBailleur.Properties.Items.Add(MettreApost(rw("InitialeBailleur").ToString))
        Next
    End Sub

    Public Sub RemplirSeuil(ByVal Bailleur As String)
        SeuilGrid.Rows.Clear()

        'Marchés de Travaux ***********************************************************************************************
        Dim m As Decimal = SeuilGrid.Rows.Add
        SeuilGrid.Rows.Item(m).DefaultCellStyle.BackColor = Color.Black
        SeuilGrid.Rows.Item(m).DefaultCellStyle.ForeColor = Color.White
        SeuilGrid.Rows.Item(m).DefaultCellStyle.Font = New Font("Times New Roman", 9, FontStyle.Bold)
        SeuilGrid.Rows.Item(m).Cells(0).Value = "Marchés de Travaux"
        SeuilGrid.Rows.Item(m).Cells(3).Value = 0

        query = "select * from T_ProcAO where TypeMarcheAO='Travaux' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Dim CodeAO As Decimal = rw("CodeProcAO")
            Dim Abrege As String = rw("AbregeAO")

            query = "select * from T_Seuil where CodeProcAO='" & CodeAO & "' and Bailleur='" & EnleverApost(ComBailleur.Text) & "' order by MontantPlanche"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw1 As DataRow In dt1.Rows
                Dim n As Decimal = SeuilGrid.Rows.Add
                SeuilGrid.Rows.Item(n).DefaultCellStyle.Font = New Font("Times New Roman", 9, FontStyle.Regular)

                SeuilGrid.Rows.Item(n).Cells(3).Value = rw1("CodeSeuil")
                SeuilGrid.Rows.Item(n).Cells(3).Style.ForeColor = Color.White

                If (rw1("TypeExamenAO").ToString = "Priori") Then
                    SeuilGrid.Rows.Item(n).Cells(2).Value = "Tous les marchés"
                ElseIf (rw1("TypeExamenAO").ToString = "Postériori" And rw1("ExceptionRevue").ToString <> "") Then
                    Dim TextRevu As String = "Les "
                    Dim Part() As String = rw1("ExceptionRevue").ToString.Split(" "c)
                    TextRevu = TextRevu & Part(0) & " premiers marchés"
                    If (Part(1) = "AE") Then
                        TextRevu = TextRevu & " par Agence d'Exécution"
                    End If
                    SeuilGrid.Rows.Item(n).Cells(2).Value = TextRevu
                End If

                SeuilGrid.Rows.Item(n).Cells(1).Value = Abrege
                If (rw1("MontantPlanche") = "TM") Then
                    Dim TexteSeuil As String = "                    "
                    TexteSeuil = TexteSeuil & "Quelque soit le Montant du Marché"
                    SeuilGrid.Rows.Item(n).Cells(0).Value = TexteSeuil

                ElseIf (rw1("MontantPlafond").ToString = "NL") Then
                    Dim TexteSeuil As String = "                    "
                    TexteSeuil = TexteSeuil & "Tous Marchés d'un Montant   >"
                    If (rw1("PlancheInclu").ToString = "OUI") Then TexteSeuil = TexteSeuil & "="
                    TexteSeuil = TexteSeuil & "   " & AfficherMonnaie(rw1(2).ToString)
                    SeuilGrid.Rows.Item(n).Cells(0).Value = TexteSeuil

                ElseIf (rw1(2).ToString = "0") Then
                    Dim TexteSeuil As String = "                    "
                    TexteSeuil = TexteSeuil & "Tous Marchés d'un Montant   <"
                    If (rw1(5).ToString = "OUI") Then TexteSeuil = TexteSeuil & "="
                    TexteSeuil = TexteSeuil & "   " & AfficherMonnaie(rw1(4).ToString)
                    SeuilGrid.Rows.Item(n).Cells(0).Value = TexteSeuil

                Else
                    Dim TexteSeuil As String = "                    "
                    TexteSeuil = TexteSeuil & AfficherMonnaie(rw1(2).ToString) & "   <"
                    If (rw1(3).ToString = "OUI") Then TexteSeuil = TexteSeuil & "="
                    TexteSeuil = TexteSeuil & "   Montant   <"
                    If (rw1(5).ToString = "OUI") Then TexteSeuil = TexteSeuil & "="
                    TexteSeuil = TexteSeuil & "   " & AfficherMonnaie(rw1(4).ToString)
                    SeuilGrid.Rows.Item(n).Cells(0).Value = TexteSeuil
                End If

            Next
        Next


        Dim d As Decimal = SeuilGrid.Rows.Add
        SeuilGrid.Rows.Item(d).Height = 1
        SeuilGrid.Rows.Item(d).DefaultCellStyle.BackColor = Color.White

        'Marchés de Fournitures ********************************************************************************************
        Dim p As Decimal = SeuilGrid.Rows.Add
        SeuilGrid.Rows.Item(p).DefaultCellStyle.BackColor = Color.Black
        SeuilGrid.Rows.Item(p).DefaultCellStyle.ForeColor = Color.White
        SeuilGrid.Rows.Item(p).DefaultCellStyle.Font = New Font("Times New Roman", 9, FontStyle.Bold)
        SeuilGrid.Rows.Item(p).Cells(0).Value = "Marchés de Fournitures"
        SeuilGrid.Rows.Item(p).Cells(3).Value = 0

        query = "select * from T_ProcAO where TypeMarcheAO='Fournitures' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt2 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt2.Rows
            Dim CodeAO As Decimal = rw(0)
            Dim Abrege As String = rw(2)

            query = "select * from T_Seuil where CodeProcAO='" & CodeAO & "' and Bailleur='" & EnleverApost(ComBailleur.Text) & "' order by MontantPlanche"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw1 As DataRow In dt1.Rows
                Dim q As Decimal = SeuilGrid.Rows.Add
                SeuilGrid.Rows.Item(q).DefaultCellStyle.Font = New Font("Times New Roman", 9, FontStyle.Regular)

                SeuilGrid.Rows.Item(q).Cells(3).Value = rw1(0)
                SeuilGrid.Rows.Item(q).Cells(3).Style.ForeColor = Color.White

                If (rw1(6).ToString = "Priori") Then
                    SeuilGrid.Rows.Item(q).Cells(2).Value = "Tous les marchés"
                ElseIf (rw1(6).ToString = "Postériori" And rw1(7).ToString <> "") Then
                    Dim TextRevu As String = "Les "
                    Dim Part() As String = rw1(7).ToString.Split(" "c)
                    TextRevu = TextRevu & Part(0) & " premiers marchés"
                    If (Part(1) = "AE") Then
                        TextRevu = TextRevu & " par Agence d'Exécution"
                    End If
                    SeuilGrid.Rows.Item(q).Cells(2).Value = TextRevu
                End If

                SeuilGrid.Rows.Item(q).Cells(1).Value = Abrege
                If (rw1(2) = "TM") Then
                    Dim TexteSeuil As String = "                    "
                    TexteSeuil = TexteSeuil & "Quelque soit le Montant du Marché"
                    SeuilGrid.Rows.Item(q).Cells(0).Value = TexteSeuil

                ElseIf (rw1(4).ToString = "NL") Then
                    Dim TexteSeuil As String = "                    "
                    TexteSeuil = TexteSeuil & "Tous Marchés d'un Montant   >"
                    If (rw1(3).ToString = "OUI") Then TexteSeuil = TexteSeuil & "="
                    TexteSeuil = TexteSeuil & "   " & AfficherMonnaie(rw1(2).ToString)
                    SeuilGrid.Rows.Item(q).Cells(0).Value = TexteSeuil

                ElseIf (rw1(2).ToString = "0") Then
                    Dim TexteSeuil As String = "                    "
                    TexteSeuil = TexteSeuil & "Tous Marchés d'un Montant   <"
                    If (rw1(5).ToString = "OUI") Then TexteSeuil = TexteSeuil & "="
                    TexteSeuil = TexteSeuil & "   " & AfficherMonnaie(rw1(4).ToString)
                    SeuilGrid.Rows.Item(q).Cells(0).Value = TexteSeuil

                Else
                    Dim TexteSeuil As String = "                    "
                    TexteSeuil = TexteSeuil & AfficherMonnaie(rw1(2).ToString) & "   <"
                    If (rw1(3).ToString = "OUI") Then TexteSeuil = TexteSeuil & "="
                    TexteSeuil = TexteSeuil & "   Montant   <"
                    If (rw1(5).ToString = "OUI") Then TexteSeuil = TexteSeuil & "="
                    TexteSeuil = TexteSeuil & "   " & AfficherMonnaie(rw1(4).ToString)
                    SeuilGrid.Rows.Item(q).Cells(0).Value = TexteSeuil
                End If
            Next
        Next

        Dim f As Decimal = SeuilGrid.Rows.Add
        SeuilGrid.Rows.Item(f).Height = 1
        SeuilGrid.Rows.Item(f).DefaultCellStyle.BackColor = Color.White


        'Marchés d'autre service ***********************************************************************************************
        Dim sac As Decimal = SeuilGrid.Rows.Add
        SeuilGrid.Rows.Item(sac).DefaultCellStyle.BackColor = Color.Black
        SeuilGrid.Rows.Item(sac).DefaultCellStyle.ForeColor = Color.White
        SeuilGrid.Rows.Item(sac).DefaultCellStyle.Font = New Font("Times New Roman", 9, FontStyle.Bold)
        SeuilGrid.Rows.Item(sac).Cells(0).Value = "Marchés de Service autre que service de consultants"
        SeuilGrid.Rows.Item(sac).Cells(3).Value = 0

        'Dim CmdSAC As MySqlCommand = sqlconn.CreateCommand
        query = "select * from T_ProcAO where TypeMarcheAO='Services autres que les services de consultants' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt5 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt5.Rows
            Dim CodeAO As Decimal = rw(0)
            Dim Abrege As String = rw(2)

            ' Dim CmdQ As MySqlCommand = sqlconn.CreateCommand
            query = "select * from T_Seuil where CodeProcAO='" & CodeAO & "' and Bailleur='" & EnleverApost(ComBailleur.Text) & "' order by MontantPlanche"
            Dim dt6 As DataTable = ExcecuteSelectQuery(query)
            For Each rw1 As DataRow In dt6.Rows
                Dim n As Decimal = SeuilGrid.Rows.Add
                SeuilGrid.Rows.Item(n).DefaultCellStyle.Font = New Font("Times New Roman", 9, FontStyle.Regular)

                SeuilGrid.Rows.Item(n).Cells(3).Value = rw1(0)
                SeuilGrid.Rows.Item(n).Cells(3).Style.ForeColor = Color.White

                If (rw1(6).ToString = "Priori") Then
                    SeuilGrid.Rows.Item(n).Cells(2).Value = "Tous les marchés"
                ElseIf (rw1(6).ToString = "Postériori" And rw1(7).ToString <> "") Then
                    Dim TextRevu As String = "Les "
                    Dim Part() As String = rw1(7).ToString.Split(" "c)
                    TextRevu = TextRevu & Part(0) & " premiers marchés"
                    If (Part(1) = "AE") Then
                        TextRevu = TextRevu & " par Agence d'Exécution"
                    End If
                    SeuilGrid.Rows.Item(n).Cells(2).Value = TextRevu
                End If

                SeuilGrid.Rows.Item(n).Cells(1).Value = Abrege
                If (rw1(2) = "TM") Then
                    Dim TexteSeuil As String = "                    "
                    TexteSeuil = TexteSeuil & "Quelque soit le Montant du Marché"
                    SeuilGrid.Rows.Item(n).Cells(0).Value = TexteSeuil

                ElseIf (rw1(4).ToString = "NL") Then
                    Dim TexteSeuil As String = "                    "
                    TexteSeuil = TexteSeuil & "Tous Marchés d'un Montant   >"
                    If (rw1(3).ToString = "OUI") Then TexteSeuil = TexteSeuil & "="
                    TexteSeuil = TexteSeuil & "   " & AfficherMonnaie(rw1(2).ToString)
                    SeuilGrid.Rows.Item(n).Cells(0).Value = TexteSeuil

                ElseIf (rw1(2).ToString = "0") Then
                    Dim TexteSeuil As String = "                    "
                    TexteSeuil = TexteSeuil & "Tous Marchés d'un Montant   <"
                    If (rw1(5).ToString = "OUI") Then TexteSeuil = TexteSeuil & "="
                    TexteSeuil = TexteSeuil & "   " & AfficherMonnaie(rw1(4).ToString)
                    SeuilGrid.Rows.Item(n).Cells(0).Value = TexteSeuil

                Else
                    Dim TexteSeuil As String = "                    "
                    TexteSeuil = TexteSeuil & AfficherMonnaie(rw1(2).ToString) & "   <"
                    If (rw1(3).ToString = "OUI") Then TexteSeuil = TexteSeuil & "="
                    TexteSeuil = TexteSeuil & "   Montant   <"
                    If (rw1(5).ToString = "OUI") Then TexteSeuil = TexteSeuil & "="
                    TexteSeuil = TexteSeuil & "   " & AfficherMonnaie(rw1(4).ToString)
                    SeuilGrid.Rows.Item(n).Cells(0).Value = TexteSeuil
                End If

            Next
        Next


        Dim sac1 As Decimal = SeuilGrid.Rows.Add
        SeuilGrid.Rows.Item(sac1).Height = 1
        SeuilGrid.Rows.Item(sac1).DefaultCellStyle.BackColor = Color.White

        'Sélection de consultants ********************************************************************************************
        Dim g As Decimal = SeuilGrid.Rows.Add
        SeuilGrid.Rows.Item(g).DefaultCellStyle.BackColor = Color.Black
        SeuilGrid.Rows.Item(g).DefaultCellStyle.ForeColor = Color.White
        SeuilGrid.Rows.Item(g).DefaultCellStyle.Font = New Font("Times New Roman", 9, FontStyle.Bold)
        SeuilGrid.Rows.Item(g).Cells(0).Value = "Sélection de Consultants"
        SeuilGrid.Rows.Item(g).Cells(3).Value = 0

        query = "select * from T_ProcAO where TypeMarcheAO='Consultants' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt3 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt3.Rows
            Dim CodeAO As Decimal = rw(0)
            Dim Abrege As String = rw(2)

            query = "select * from T_Seuil where CodeProcAO='" & CodeAO & "' and Bailleur='" & EnleverApost(ComBailleur.Text) & "' order by MontantPlanche"
            Dim dt4 As DataTable = ExcecuteSelectQuery(query)
            For Each rw4 As DataRow In dt4.Rows
                Dim h As Decimal = SeuilGrid.Rows.Add
                SeuilGrid.Rows.Item(h).DefaultCellStyle.Font = New Font("Times New Roman", 9, FontStyle.Regular)

                SeuilGrid.Rows.Item(h).Cells(3).Value = rw4(0)
                SeuilGrid.Rows.Item(h).Cells(3).Style.ForeColor = Color.White

                If (rw4(6).ToString = "Priori") Then
                    SeuilGrid.Rows.Item(h).Cells(2).Value = "Tous les contrats"
                ElseIf (rw4(6).ToString = "Postériori" And rw4(7).ToString <> "") Then
                    Dim TextRevu As String = "Les "
                    Dim Part() As String = rw4(7).ToString.Split(" "c)
                    TextRevu = TextRevu & Part(0) & " premiers contrats"
                    If (Part(1) = "AE") Then
                        TextRevu = TextRevu & " par Agence d'Exécution"
                    End If
                    SeuilGrid.Rows.Item(h).Cells(2).Value = TextRevu
                End If

                SeuilGrid.Rows.Item(h).Cells(1).Value = Abrege
                If (rw4(2).ToString = "TM") Then
                    Dim TexteSeuil As String = "                    "
                    TexteSeuil = TexteSeuil & "Quelque soit le Montant du Contrat"
                    SeuilGrid.Rows.Item(h).Cells(0).Value = TexteSeuil

                ElseIf (rw4(4).ToString = "NL") Then
                    Dim TexteSeuil As String = "                    "
                    TexteSeuil = TexteSeuil & "Tous Contrats d'un Montant   >"
                    If (rw4(3).ToString = "OUI") Then TexteSeuil = TexteSeuil & "="
                    TexteSeuil = TexteSeuil & "   " & AfficherMonnaie(rw4(2).ToString)
                    SeuilGrid.Rows.Item(h).Cells(0).Value = TexteSeuil

                ElseIf (rw4(2).ToString = "0") Then
                    Dim TexteSeuil As String = "                    "
                    TexteSeuil = TexteSeuil & "Tous Contrats d'un Montant   <"
                    If (rw4(5).ToString = "OUI") Then TexteSeuil = TexteSeuil & "="
                    TexteSeuil = TexteSeuil & "   " & AfficherMonnaie(rw4(4).ToString)
                    SeuilGrid.Rows.Item(h).Cells(0).Value = TexteSeuil

                Else
                    Dim TexteSeuil As String = "                    "
                    TexteSeuil = TexteSeuil & AfficherMonnaie(rw4(2).ToString) & "   <"
                    If (rw4(3).ToString = "OUI") Then TexteSeuil = TexteSeuil & "="
                    TexteSeuil = TexteSeuil & "   Montant   <"
                    If (rw4(5).ToString = "OUI") Then TexteSeuil = TexteSeuil & "="
                    TexteSeuil = TexteSeuil & "   " & AfficherMonnaie(rw4(4).ToString)
                    SeuilGrid.Rows.Item(h).Cells(0).Value = TexteSeuil
                End If
            Next
        Next

        Dim k As Decimal = SeuilGrid.Rows.Add
        SeuilGrid.Rows.Item(k).Height = 1
        SeuilGrid.Rows.Item(k).DefaultCellStyle.BackColor = Color.White

    End Sub

    Private Sub BtAjouter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjouter.Click
        ExceptRevue = ""
        Dialog_form(DialogSeuil)
        ' Dialog_form(DialogSeuilModifier)
        ' If ComBailleur.Text.Trim <> "" Then
        ' RemplirSeuil(ComBailleur.Text)
        ' End If

    End Sub

    Private Sub BtSupprimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSupprimer.Click
        Dim LigneSupp As Decimal = SeuilGrid.CurrentRow.Index
        Dim CodeSeuil As Decimal = SeuilGrid.Rows.Item(LigneSupp).Cells(3).Value

        '******** ligne a ne pas supprimer  (CodeSeuil = 0)
        If (CodeSeuil = 0) Then
            FailMsg("Cette ligne ne peut être supprimée!")
            Exit Sub
        End If
        '*******  verifre si on a déjà definis des etapes de marche
        Dim CodeProcAO = ExecuteScallar("select CodeProcAO from T_Seuil where CodeSeuil='" & CodeSeuil & "'")
        If Val(ExecuteScallar("select count(*) from t_etapemarche where CodeProcAO='" & CodeProcAO & "'")) > 0 Then
            FailMsg("Cette ligne ne peut être supprimée !")
            Exit Sub
        End If

        If ConfirmMsg("Voulez-vous supprimer définitivement la ligne" & vbNewLine & Mid(SeuilGrid.Rows.Item(LigneSupp).Cells(0).Value.ToString, 20) & " " & SeuilGrid.Rows.Item(LigneSupp).Cells(1).Value & " " & SeuilGrid.Rows.Item(LigneSupp).Cells(2).Value & " ?") = DialogResult.Yes Then
            ExecuteNonQuery("DELETE from T_Seuil where CodeSeuil='" & CodeSeuil & "'")
            SuccesMsg("Suppression effectuée avec succès")
            If ComBailleur.Text.Trim <> "" Then RemplirSeuil(ComBailleur.Text)
        End If
    End Sub

    Private Sub SeuilGrid_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles SeuilGrid.CellClick
        BtSupprimer.Enabled = False
    End Sub
    Private Sub SeuilGrid_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles SeuilGrid.CellDoubleClick
        BtSupprimer.Enabled = True
    End Sub

    Private Sub SeuilGrid_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles SeuilGrid.Leave
        BtSupprimer.Enabled = False
    End Sub

    Private Sub ComBailleur_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComBailleur.SelectedValueChanged
        If ComBailleur.SelectedIndex <> -1 Then
            DebutChargement()
            RemplirSeuil(ComBailleur.Text)
            FinChargement()
        Else
            SeuilGrid.Rows.Clear()
        End If
    End Sub
End Class