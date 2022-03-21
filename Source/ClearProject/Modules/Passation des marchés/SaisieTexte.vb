Imports System.IO

Public Class SaisieTexte

    Dim CheminDocTDR As String = ""

    Private Sub SaisieTexte_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        If (ReponseDialog = "") Then
            Me.Close()
        Else
            CheminDocTDR = ReponseDialog
        End If
        If File.Exists(CheminDocTDR.ToString) = True Then
            ClearOfficeDocument.LoadDocument(CheminDocTDR, DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
        Else
            Me.Close()
        End If

    End Sub

    Private Sub BtEnregistrer_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtEnregistrer.ItemClick
        'Cas de DP
        Try
            If ExceptRevue2.ToString = "DP" Then
                Dim Chemin1 As String = line & "\DP\" & FormatFileName(ExceptRevue.ToString, "_") & "\TDR1.Rtf"
                ClearOfficeDocument.SaveDocument(Chemin1, DevExpress.XtraRichEdit.DocumentFormat.Rtf)
            ElseIf ExceptRevue2.ToString = "AMI" Then
                Dim CheminPdf As String = line & "\AMI\" & FormatFileName(ExceptRevue.ToString, "_") & " \PublicationAMI.pdf"
                ClearOfficeDocument.ExportToPdf(CheminPdf)
            End If

            ClearOfficeDocument.SaveDocument(CheminDocTDR, DevExpress.XtraRichEdit.DocumentFormat.OpenXml)

        Catch sex As IOException
            SuccesMsg("Un exemplaire du fichier à sauvegardé est ouvert dans une autre application. Veuillez le ferermé svp.")
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub ClearOfficeDocument_ModifiedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClearOfficeDocument.ModifiedChanged
        If (ClearOfficeDocument.Modified = True) Then
            BtEnregistrer.Enabled = True
        Else
            BtEnregistrer.Enabled = False
        End If
    End Sub

    Private Sub BtQuitter_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtQuitter.ItemClick
        If (ClearOfficeDocument.Modified = True) Then
            Dim RepMsg As MsgBoxResult = MsgBox("Voulez-vous enregistrer les modifications apportées?", MsgBoxStyle.YesNoCancel)
            If (RepMsg = MsgBoxResult.Cancel) Then
                Exit Sub
            ElseIf (RepMsg = MsgBoxResult.Yes) Then
                BtEnregistrer_ItemClick(Me, e)
            End If
        End If
        Me.Close()
    End Sub

    Private Sub SaisieTexte_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        FinChargement()
    End Sub
End Class