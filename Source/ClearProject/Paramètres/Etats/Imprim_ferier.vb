Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Imprim_ferier

    Private Sub btimprim_Click(sender As System.Object, e As System.EventArgs) Handles btimprim.Click

        If CmbAnneeFeriee.SelectedIndex = -1 Then
            FailMsg("Veuillez choisir une année dans la liste svp.")
        Else
            Dim reportferie As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table

            DebutChargement(True, "Le traitement de votre demande est en cours...")
            Dim Chemin As String = lineEtat & "\Parametres\Jour_ferie.rpt"

            Dim DatSet = New DataSet
            reportferie.Load(Chemin)

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = reportferie.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            reportferie.SetDataSource(DatSet)
            reportferie.SetParameterValue("CodeProjet", ProjetEnCours)
            reportferie.SetParameterValue("Annee", "01/01/" & CmbAnneeFeriee.Text)
            FullScreenReport.FullView.ReportSource = reportferie
            FinChargement()
            FullScreenReport.ShowDialog()
        End If
    End Sub

    Private Sub Imprim_ferier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        CmbAnneeFeriee.Properties.Items.Clear()
        For i = (Now.Year - 2) To (Now.Year + 2)
            CmbAnneeFeriee.Properties.Items.Add(i)
        Next
        CmbAnneeFeriee.SelectedIndex = 2
    End Sub
End Class