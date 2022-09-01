Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class EtatListeBonCommande
    Private Sub EtatListeBonCommande_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RechargerCombo()
    End Sub

    Private Sub RechargerCombo()
        DateDebut.Text = ""
        DateDebut.Focus()
        DateFin.Text = ""
        CmbStatut.ResetText()
        CmbStatut.Properties.Items.Clear()

        Dim dt As DataTable = New DataTable()
        query = "SELECT Min(DateCommande) as DateDebut, Max(DateCommande) as DateFin FROM t_boncommande WHERE CodeProjet = '" & ProjetEnCours & "'"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            DateDebut.Text = CDate(rw("DateDebut")).ToString("dd/MM/yyyy")
            DateFin.Text = CDate(rw("DateFin")).ToString("dd/MM/yyyy")
        Next

        query = "SELECT DISTINCT Statut FROM t_boncommande WHERE CodeProjet = '" & ProjetEnCours & "' ORDER by Statut ASC"
        dt = ExcecuteSelectQuery(query)
        CmbStatut.Properties.Items.Add("Tous")
        For Each rw As DataRow In dt.Rows
            CmbStatut.Properties.Items.Add(rw("Statut").ToString)
        Next
    End Sub

    Private Sub BtnImprimer_Click(sender As Object, e As EventArgs) Handles BtnImprimer.Click
        Dim verifdate As Integer = Date.Compare(CDate(DateDebut.Text), CDate(DateFin.Text))

        If verifdate = 1 Then
            SuccesMsg("La date de fin ne peut être antérieure à la date de début.")
            DateFin.Text = ""
            DateFin.Focus()
            Exit Sub
        End If

        If DateDebut.Text = "" Then
            SuccesMsg("Veuillez saisir la date de début")
            DateDebut.Focus()
            Exit Sub
        End If

        If DateFin.Text = "" Then
            SuccesMsg("Veuillez saisir la date de fin")
            DateFin.Focus()
            Exit Sub
        End If

        If CmbStatut.SelectedIndex = -1 Then
            SuccesMsg("Veuillez sélectionner le statut")
            CmbStatut.Focus()
            Exit Sub
        End If

        'convertion de la date en date anglaise
        Dim str As String = DateDebut.Text
        Dim tempdt = dateconvert(str)
        Dim str2 As String = DateFin.Text
        Dim tempdt1 = dateconvert(str2)
        Dim ReceiveDate As Double = 0

        Dim reportEtatBonCommande As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table

        'vérification de la plage de date saisie
        query = "SELECT count(RefBonCommande) as VerifDate FROM t_boncommande WHERE DateCommande BETWEEN '" & tempdt & "' AND '" & tempdt1 & "'"
        ReceiveDate = Val(ExecuteScallar(query))
        If ReceiveDate = 0 Then
            SuccesMsg("Aucune données sur la période sélectionnée")
            Exit Sub
        End If

        DebutChargement(True, "Le traitement de votre demande est en cours...")

        Dim DatSet = New DataSet

        If CmbStatut.Text = "Tous" Then
            Dim Chemin As String = lineEtat & "\Bon_Commande\ListeBonCommande_Tous.rpt"
            reportEtatBonCommande.Load(Chemin)
        ElseIf CmbStatut.Text = "Annulé" Then
            Dim Chemin As String = lineEtat & "\Bon_Commande\ListeBonCommande_Annuler.rpt"
            reportEtatBonCommande.Load(Chemin)
        ElseIf CmbStatut.Text = "En cours" Then
            Dim Chemin As String = lineEtat & "\Bon_Commande\ListeBonCommande_EnCours.rpt"
            reportEtatBonCommande.Load(Chemin)
        ElseIf CmbStatut.Text = "Rejeté" Then
            Dim Chemin As String = lineEtat & "\Bon_Commande\ListeBonCommande_Rejeter.rpt"
            reportEtatBonCommande.Load(Chemin)
        ElseIf CmbStatut.Text = "Signé" Then
            Dim Chemin As String = lineEtat & "\Bon_Commande\ListeBonCommande_Signer.rpt"
            reportEtatBonCommande.Load(Chemin)
        End If

        With crConnectionInfo
            .ServerName = ODBCNAME
            .DatabaseName = DB
            .UserID = USERNAME
            .Password = PWD
        End With

        CrTables = reportEtatBonCommande.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        reportEtatBonCommande.SetDataSource(DatSet)
        reportEtatBonCommande.SetParameterValue("CodeProjet", ProjetEnCours)
        reportEtatBonCommande.SetParameterValue("DateDeb", tempdt)
        reportEtatBonCommande.SetParameterValue("DateFin", tempdt1)

        FullScreenReport.FullView.ReportSource = reportEtatBonCommande
        FullScreenReport.Text = "Etat récapitulatif des bons de commande"
        FinChargement()
        FullScreenReport.ShowDialog()
    End Sub
End Class