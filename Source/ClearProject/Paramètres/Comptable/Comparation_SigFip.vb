Imports System.Math
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Comparation_SigFip
    Private Sub Comparation_SigFip_Load(sender As Object, e As EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        rdNonCorrespondant.Checked = False
        rdCorrespondance.Checked = False
        ClearGrille()
    End Sub
    Private Sub loadCorrespondant()
        DebutChargement()
        Dim dtGrille = New DataTable
        dtGrille.Columns.Clear()
        dtGrille.Columns.Add("Sigfip", Type.GetType("System.String"))
        dtGrille.Columns.Add("Syscohada", Type.GetType("System.String"))
        dtGrille.Columns.Add("Libellé", Type.GetType("System.String"))

        query = "SELECT * FROM t_plansigfip WHERE SIGFCOMPTE IN (SELECT DISTINCT SIGFCOMPTE FROM t_correspondance_sigfip ORDER BY SIGFCOMPTE) ORDER BY SIGFCOMPTE"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Dim drS = dtGrille.NewRow()
            drS(0) = rw("SIGFCOMPTE").ToString
            drS(1) = ""
            drS(2) = MettreApost(rw("SIGFLIBELLE").ToString())
            dtGrille.Rows.Add(drS)
            query = "SELECT * FROM t_comp_sous_classe where CODE_SC IN (SELECT COMPTE FROM t_correspondance_sigfip WHERE SIGFCOMPTE='" & rw("SIGFCOMPTE") & "' ORDER BY COMPTE)"
            Dim dtSYS As DataTable = ExcecuteSelectQuery(query)
            For Each rwSYS As DataRow In dtSYS.Rows
                drS = dtGrille.NewRow
                drS(0) = ""
                drS(1) = rwSYS("CODE_SC")
                drS(2) = MettreApost(rwSYS("LIBELLE_SC").ToString())
                dtGrille.Rows.Add(drS)
            Next
        Next
        ViewCorrespondance.Columns.Clear()
        dgCorrespondance.DataSource = dtGrille
        ViewCorrespondance.OptionsBehavior.Editable = False
        ViewCorrespondance.OptionsSelection.EnableAppearanceFocusedCell = False
        ViewCorrespondance.OptionsView.ColumnAutoWidth = True
        ViewCorrespondance.OptionsBehavior.AutoExpandAllGroups = True
        ViewCorrespondance.VertScrollVisibility = True
        ViewCorrespondance.HorzScrollVisibility = True
        ViewCorrespondance.BestFitColumns()

        ViewCorrespondance.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewCorrespondance.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        ColorRowGridAnal(ViewCorrespondance, "[Syscohada]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
        FinChargement()
        'ColorRowGridAnal(ViewCorrespondance, "[Libellé]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
    End Sub

    Private Sub loadNonCorrespondnat()
        DebutChargement()
        Dim dtGrille = New DataTable
        dtGrille.Columns.Clear()
        'dtGrille.Columns.Add("Sigfip", Type.GetType("System.String"))
        dtGrille.Columns.Add("Syscohada", Type.GetType("System.String"))
        dtGrille.Columns.Add("Libellé", Type.GetType("System.String"))

        query = "SELECT * FROM t_comp_sous_classe where CODE_SC NOT IN (SELECT COMPTE FROM t_correspondance_sigfip ORDER BY COMPTE) ORDER BY CODE_SC"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Dim drS = dtGrille.NewRow()
            drS(0) = rw("CODE_SC").ToString
            drS(1) = MettreApost(rw("LIBELLE_SC").ToString())
            dtGrille.Rows.Add(drS)
        Next

        ViewCorrespondance.Columns.Clear()
        dgCorrespondance.DataSource = dtGrille
        ViewCorrespondance.OptionsBehavior.Editable = False
        ViewCorrespondance.OptionsSelection.EnableAppearanceFocusedCell = False
        ViewCorrespondance.OptionsView.ColumnAutoWidth = True
        ViewCorrespondance.OptionsBehavior.AutoExpandAllGroups = True
        ViewCorrespondance.VertScrollVisibility = True
        ViewCorrespondance.HorzScrollVisibility = True
        ViewCorrespondance.BestFitColumns()

        ViewCorrespondance.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewCorrespondance.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        FinChargement()
        'ColorRowGridAnal(ViewCorrespondance, "[Syscohada]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
    End Sub
    Private Sub ClearGrille()
        Dim dtGrille = New DataTable
        dtGrille.Columns.Clear()
        dtGrille.Columns.Add("Sigfip", Type.GetType("System.String"))
        dtGrille.Columns.Add("Syscohada", Type.GetType("System.String"))
        dtGrille.Columns.Add("Libellé", Type.GetType("System.String"))

        dtGrille.Rows.Clear()
        ViewCorrespondance.Columns.Clear()
        dgCorrespondance.DataSource = dtGrille
        ViewCorrespondance.OptionsBehavior.Editable = False
        ViewCorrespondance.OptionsSelection.EnableAppearanceFocusedCell = False
        ViewCorrespondance.OptionsView.ColumnAutoWidth = True
        ViewCorrespondance.OptionsBehavior.AutoExpandAllGroups = True
        ViewCorrespondance.VertScrollVisibility = True
        ViewCorrespondance.HorzScrollVisibility = True
        ViewCorrespondance.BestFitColumns()

        ViewCorrespondance.Columns(0).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewCorrespondance.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        ColorRowGridAnal(ViewCorrespondance, "[Syscohada]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black)
    End Sub
    Private Sub rdCorrespondance_CheckedChanged(sender As Object, e As EventArgs) Handles rdCorrespondance.CheckedChanged, rdNonCorrespondant.CheckedChanged
        If rdCorrespondance.Checked Then
            loadCorrespondant()
        ElseIf rdNonCorrespondant.Checked Then
            loadNonCorrespondnat()
        Else
            ClearGrille()
        End If
    End Sub

    Private Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click
        If Not Access_Btn("BtnPrintCorrespondanceSIGFIP") Then
            Exit Sub
        End If

        If rdCorrespondance.Checked Then
            If ViewCorrespondance.RowCount = 0 Then
                SuccesMsg("Aucune donnée à imprimer")
                Exit Sub
            End If
            Dim RapprochSigfip As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table

            Dim Chemin As String = lineEtat & "\Comptabilite\"

            Dim DatSet = New DataSet
            RapprochSigfip.Load(Chemin & "CompteSigfip_Correspondance.rpt")

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = RapprochSigfip.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            RapprochSigfip.SetDataSource(DatSet)
            RapprochSigfip.SetParameterValue("CodeProjet", ProjetEnCours)

            FullScreenReport.FullView.ReportSource = RapprochSigfip
            FinChargement()
            FullScreenReport.ShowDialog()
        ElseIf rdNonCorrespondant.Checked Then
            If ViewCorrespondance.RowCount = 0 Then
                SuccesMsg("Aucune donnée à imprimer")
                Exit Sub
            End If
            Dim RapprochSigfip As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table

            Dim Chemin As String = lineEtat & "\Comptabilite\"

            Dim DatSet = New DataSet
            RapprochSigfip.Load(Chemin & "CompteSigfip_NonCorrespondance.rpt")

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = RapprochSigfip.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            RapprochSigfip.SetDataSource(DatSet)
            RapprochSigfip.SetParameterValue("CodeProjet", ProjetEnCours)

            FullScreenReport.FullView.ReportSource = RapprochSigfip
            FinChargement()
            FullScreenReport.ShowDialog()
        Else
            SuccesMsg("Veuillez cocher une option.")
        End If
    End Sub
End Class