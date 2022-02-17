Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Etat_parametres

    Private Sub btimprim_Click(sender As System.Object, e As System.EventArgs) Handles btimprim.Click

        If comb1.Text = "" And comb2.Text = "" Then

            Dim journaux As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim Chemin As String = lineEtat & "\Parametres\Journal\"

            Dim DatSet = New DataSet
            journaux.Load(Chemin & "Journaux.rpt")

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = journaux.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            journaux.SetDataSource(DatSet)
            journaux.SetParameterValue("Codeprojet", ProjetEnCours)

            FullScreenReport.FullView.ReportSource = journaux
            FullScreenReport.ShowDialog()

        ElseIf comb1.Text <> "" And comb2.Text <> "" Then

            Dim j1() As String
            j1 = comb1.Text.Split("   ")

            Dim j2() As String
            j2 = comb2.Text.Split("   ")

            Dim journaux As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim Chemin As String = lineEtat & "\Parametres\Journal\"

            Dim DatSet = New DataSet
            journaux.Load(Chemin & "Journaux_criteres.rpt")

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = journaux.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            journaux.SetDataSource(DatSet)
            journaux.SetParameterValue("Compte1", j1(0).ToString)
            journaux.SetParameterValue("Compte2", j2(0).ToString)
            journaux.SetParameterValue("Codeprojet", ProjetEnCours)

            FullScreenReport.FullView.ReportSource = journaux
            FullScreenReport.ShowDialog()

        End If

        comb1.Text = ""
        comb2.Text = ""
    End Sub

    Private Sub Etat_parametres_Load(sender As System.Object, e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        'remplir le tpe journal
        comb1.Properties.Items.Clear()
        comb2.Properties.Items.Clear()
        query = "select * from T_COMP_JOURNAL ORDER BY code_j"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            comb1.Properties.Items.Add(rw(0).ToString & "   " & MettreApost(rw(3).ToString))
            comb2.Properties.Items.Add(rw(0).ToString & "   " & MettreApost(rw(3).ToString))
        Next

    End Sub

End Class