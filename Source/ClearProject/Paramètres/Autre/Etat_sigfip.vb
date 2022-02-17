Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Etat_sigfip

    Private Sub Etat_sigfip_Load(sender As System.Object, e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        'remplir les sous classe du plan comptable
        comb1.Properties.Items.Clear()
        comb2.Properties.Items.Clear()
       query= "select * from t_plansigfip ORDER BY SIGFCOMPTE"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            comb1.Properties.Items.Add(rw(0).ToString & "   " & MettreApost(rw(1).ToString))
            comb2.Properties.Items.Add(rw(0).ToString & "   " & MettreApost(rw(1).ToString))
        Next
    End Sub

    Private Sub btimprim_Click(sender As System.Object, e As System.EventArgs) Handles btimprim.Click

        Dim sc1() As String
        sc1 = comb1.Text.Split("   ")

        Dim sc2() As String
        sc2 = comb2.Text.Split("   ")

        If comb1.Text = "" And comb2.Text = "" Then

            Dim plansigfip As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table

            Dim Chemin As String = lineEtat & "\Parametres\PlanSigfip\"

            Dim DatSet = New DataSet
            plansigfip.Load(Chemin & "PlanSigfip.rpt")

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = plansigfip.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            plansigfip.SetDataSource(DatSet)
            plansigfip.SetParameterValue("Codeprojet", ProjetEnCours)

            FullScreenReport.FullView.ReportSource = plansigfip
            FullScreenReport.ShowDialog()

        ElseIf comb1.Text <> "" And comb2.Text <> "" Then

            Dim plansigfip As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table

            Dim Chemin As String = lineEtat & "\Parametres\PlanSigfip\"

            Dim DatSet = New DataSet
            plansigfip.Load(Chemin & "PlanSigfip_criteres.rpt")

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = plansigfip.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            plansigfip.SetDataSource(DatSet)
            plansigfip.SetParameterValue("Compte1", sc1(0).ToString)
            plansigfip.SetParameterValue("Compte2", sc2(0).ToString)
            plansigfip.SetParameterValue("Codeprojet", ProjetEnCours)

            FullScreenReport.FullView.ReportSource = plansigfip
            FullScreenReport.ShowDialog()

        End If

        comb1.Text = ""
        comb2.Text = ""
    End Sub
End Class