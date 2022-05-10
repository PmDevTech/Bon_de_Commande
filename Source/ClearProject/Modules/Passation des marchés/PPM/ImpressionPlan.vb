Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class ImpressionPlan
    Public IDPlan As Decimal = 0
    Public TypeMarches As String = ""

    Private Sub ModifMethode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        If IDPlan <= 0 Or TypeMarches = "" Then
            Me.Close()
        End If
        CmbTypeplan.ResetText()
        CmbTypeplan.Select()
    End Sub

    Private Sub BtEnregComm_Click(sender As Object, e As EventArgs) Handles BtEnregComm.Click
        Try
            If CmbTypeplan.IsRequiredControl("Veuillez choisir le type de plan à imprimer") Then
                CmbTypeplan.Select()
                Exit Sub
            End If

            DebutChargement(True, "Chargement du plan en cours...")
            Dim PPM As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table

            Dim DatSet = New DataSet

            Dim Chemin As String = lineEtat & "\Marches\PPM\"
            If CmbTypeplan.Text = "Plan résumé" Then
                If EnleverApost(TypeMarches.ToString).ToLower = "Consultants".ToLower Then
                    'PPM.Load(Chemin & "PPM_Resume.rpt")
                    PPM.Load(Chemin & "PPM_Resume _Consultants.rpt")
                Else
                    PPM.Load(Chemin & "PPM_Resume_Autres.rpt")
                End If

            ElseIf CmbTypeplan.Text = "Plan détaillé" Then
                'PPM.Load(Chemin & "PPM_Resume.rpt")
                FinChargement()
                FailMsg("Le plan est en cours de réalisation.")
                Exit Sub
            End If

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = PPM.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            PPM.SetDataSource(DatSet)

            ExecuteNonQuery("TRUNCATE TABLE `t_ppm_tamponetape`")

            'Recuperation des dernières etapes de realisation du PPM
            Dim dtRefMarche As DataTable = ExcecuteSelectQuery("select RefMarche from t_marche where RefPPM='" & IDPlan & "' and TypeMarche='" & EnleverApost(TypeMarches.ToString) & "' and CodeProjet='" & ProjetEnCours & "'")
            Dim TitreDerniereEtapes As String = ""
            For Each rw1 In dtRefMarche.Rows
                Dim RefEtape As Decimal = Val(ExecuteScallar("select RefEtape from t_planmarche where RefMarche='" & rw1("RefMarche") & "' ORDER BY FinEffective DESC LIMIT 1"))
                If RefEtape > 0 Then
                    TitreDerniereEtapes = ExecuteScallar("select TitreEtape from t_etapemarche where RefEtape='" & RefEtape & "'")
                    ExecuteNonQuery("INSERT INTO t_ppm_tamponetape VALUES(NULL,'" & IDPlan & "', '" & rw1("RefMarche") & "', '" & TitreDerniereEtapes.ToString & "', '" & ProjetEnCours & "')")
                Else
                    ExecuteNonQuery("INSERT INTO t_ppm_tamponetape VALUES(NULL,'" & IDPlan & "', '" & rw1("RefMarche") & "', '', '" & ProjetEnCours & "')")
                End If
            Next

            PPM.SetParameterValue("CodeProjet", ProjetEnCours)
            PPM.SetParameterValue("RefPPM", IDPlan)
            PPM.SetParameterValue("TypeMarche", EnleverApost(TypeMarches.ToString))

            Dim ChefFileBailleur As String = ""
            Dim NumeroPlan As String = ""
            Dim dt As DataTable = ExcecuteSelectQuery("select B.InitialeBailleur, P.NumeroPlan from t_ppm_marche as P, t_marche as M, t_bailleur as B, t_convention as C  where M.RefPPM=P.RefPPM and M.Convention_ChefFile=C.CodeConvention and C.CodeBailleur=B.CodeBailleur and P.RefPPM='" & IDPlan & "' and M.CodeProjet='" & ProjetEnCours & "' LIMIT 1")
            For Each rw In dt.Rows
                ChefFileBailleur = rw("InitialeBailleur").ToString
                NumeroPlan = rw("NumeroPlan").ToString
            Next

            PPM.SetParameterValue("ChefFileBailleur", EnleverApost(ChefFileBailleur.ToString))
            FullScreenReport.FullView.ReportSource = PPM
            FullScreenReport.Text = "PLAN N° " & MettreApost(NumeroPlan.ToString)
            FinChargement()
            FullScreenReport.ShowDialog()

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub
End Class