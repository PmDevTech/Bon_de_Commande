Imports MySql.Data.MySqlClient
Imports System.IO
Imports Microsoft.Office.Interop
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions
Imports System.Math
Imports DevExpress.XtraRichEdit
Imports CrystalDecisions.Shared
Imports ClearProject.PassationMarche
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports System.Security.Cryptography
Imports System.Text

Public Class AnnonceAMI
    Dim CheminRapportEvaluationDOC As String = String.Empty
    Dim CheminRapportEvaluationPDF As String = String.Empty
    Dim ValidationsRapports As String = String.Empty
    Dim RapportModif As Boolean = False

    Private Sub RapportEvaluationMI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

    End Sub
End Class