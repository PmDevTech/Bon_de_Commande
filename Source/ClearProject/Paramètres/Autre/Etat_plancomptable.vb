Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Etat_plancomptable

    Private Sub Etat_plancomptable_Load(sender As System.Object, e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        'remplir les sous classe du plan comptable
        comb1.Properties.Items.Clear()
        comb2.Properties.Items.Clear()
        query = "select * from T_COMP_SOUS_CLASSE ORDER BY code_sc"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            comb1.Properties.Items.Add(rw(0).ToString & "   " & MettreApost(rw(2).ToString))
            comb2.Properties.Items.Add(rw(0).ToString & "   " & MettreApost(rw(2).ToString))
        Next
    End Sub

    Private Sub btimprim_Click(sender As System.Object, e As System.EventArgs) Handles btimprim.Click

        Dim sc1() As String
        sc1 = comb1.Text.Split("   ")

        Dim sc2() As String
        sc2 = comb2.Text.Split("   ")

        If comb1.Text = "" And comb2.Text = "" And CombType.Text = "" Then

            Dim plandetaille As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table

            Dim Chemin As String = lineEtat & "\Parametres\PlanComptable\"

            Dim DatSet = New DataSet
            plandetaille.Load(Chemin & "Plancomptabledetaille.rpt")

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = plandetaille.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            plandetaille.SetDataSource(DatSet)
            plandetaille.SetParameterValue("Codeprojet", ProjetEnCours)

            FullScreenReport.FullView.ReportSource = plandetaille
            FullScreenReport.ShowDialog()


        ElseIf comb1.Text = "" And comb2.Text = "" Then

            If CombType.Text = "Plan Comptable Détaillé" Then

                Dim plandetaille As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table

                Dim Chemin As String = lineEtat & "\Parametres\PlanComptable\"

                Dim DatSet = New DataSet
                plandetaille.Load(Chemin & "Plancomptabledetaille.rpt")

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = plandetaille.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                plandetaille.SetDataSource(DatSet)
                plandetaille.SetParameterValue("Codeprojet", ProjetEnCours)

                FullScreenReport.FullView.ReportSource = plandetaille
                FullScreenReport.ShowDialog()

            ElseIf CombType.Text = "Plan Comptable Simple" Then
                Dim plancompte As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table

                Dim Chemin As String = lineEtat & "\Parametres\PlanComptable\"

                Dim DatSet = New DataSet
                plancompte.Load(Chemin & "Plancomptable.rpt")

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = plancompte.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                plancompte.SetDataSource(DatSet)
                plancompte.SetParameterValue("Codeprojet", ProjetEnCours)

                FullScreenReport.FullView.ReportSource = plancompte
                FullScreenReport.ShowDialog()

            End If

        ElseIf comb1.Text <> "" And comb2.Text <> "" Then

            If CombType.Text = "Plan Comptable Détaillé" Then

                Dim plandetaille As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table

                Dim Chemin As String = lineEtat & "\Parametres\PlanComptable\"

                Dim DatSet = New DataSet
                plandetaille.Load(Chemin & "Plancomptabledetaille_criteres.rpt")

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = plandetaille.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                plandetaille.SetDataSource(DatSet)
                plandetaille.SetParameterValue("Compte1", sc1(0).ToString)
                plandetaille.SetParameterValue("Compte2", sc2(0).ToString)
                plandetaille.SetParameterValue("Codeprojet", ProjetEnCours)

                FullScreenReport.FullView.ReportSource = plandetaille
                FullScreenReport.ShowDialog()


            ElseIf CombType.Text = "Plan Comptable Simple" Then

                Dim plancompte As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table

                Dim Chemin As String = lineEtat & "\Parametres\PlanComptable\"

                Dim DatSet = New DataSet
                plancompte.Load(Chemin & "Plancomptable_criteres.rpt")

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = plancompte.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                plancompte.SetDataSource(DatSet)
                plancompte.SetParameterValue("Compte1", sc1(0).ToString)
                plancompte.SetParameterValue("Compte2", sc2(0).ToString)
                plancompte.SetParameterValue("Codeprojet", ProjetEnCours)

                FullScreenReport.FullView.ReportSource = plancompte
                FullScreenReport.ShowDialog()

            End If

        End If

        comb1.Text = ""
        comb2.Text = ""
    End Sub
End Class