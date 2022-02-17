Imports System.IO
'Imports SystemMonitor
Public NotInheritable Class Visionneuse

    'TODO: This form can easily be set as the splash screen for the application by going to the "Application" tab
    '  of the Project Designer ("Properties" under the "Project" menu).


    Private Sub Visionneuse_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
        'gcImpDoc.Width = Me.Width - 20
        'gcImpDoc.Left = 10
        'gcDocument.Width = gcImpDoc.Width
        'gcDocument.Height = Me.Height - (gcImpDoc.Height + 60)
        'gcDocument.Left = 10

        'taille de visioword 
        Visioword.Dock = DockStyle.Fill

        'Visioword.Top = 2
        'Visioword.Left = 2
        'Visioword.Height = gcDocument.Height - 4
        'Visioword.Width = gcDocument.Width - 4

        'taille de visionpdf
        VisionPdf.Dock = DockStyle.Fill

        'VisionPdf.Top = 2
        'VisionPdf.Left = 2
        'VisionPdf.Height = gcDocument.Height - 4
        'VisionPdf.Width = gcDocument.Width - 4
        'position des boutons
        'pbFermerDoc.Left = GRHEmploye.txtSearchEmp.Left + GRHEmploye.txtSearchEmp.Width
        'pbImpDoc.Left = pbFermerDoc.Left - (pbImpDoc.Width + 5)
    End Sub

    Private Sub pbImpDoc_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbImpDoc.MouseDown
        pbImpDoc.BorderStyle = BorderStyle.Fixed3D
    End Sub

    Private Sub pbImpDoc_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbImpDoc.MouseUp
        pbImpDoc.BorderStyle = BorderStyle.None
    End Sub

    Private Sub gcImpDoc_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles gcImpDoc.Paint

    End Sub

    Private Sub pbFermerDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbFermerDoc.Click
        Try
            If File.Exists(line & "\Modèles\PrintMissions.docx") Then
                File.Delete(line & "\Modèles\PrintMissions.docx")
            End If
            If File.Exists(line & "\Modèles\PrintSalaires.docx") Then
                File.Delete(line & "\Modèles\PrintSalaires.docx")
            End If
            If File.Exists(line & "\Modèles\PrintPassages.docx") Then
                File.Delete(line & "\Modèles\PrintPassages.docx")
            End If
            If File.Exists(line & "\Modèles\PrintOrdres.docx") Then
                File.Delete(line & "\Modèles\PrintOrdres.docx")
            End If
            If File.Exists(line & "\Modèles\PrintJustifications.docx") Then
                File.Delete(line & "\Modèles\PrintJustifications.docx")
            End If
            If File.Exists(line & "\Modèles\PrintImpots.docx") Then
                File.Delete(line & "\Modèles\PrintImpots.docx")
            End If
            If File.Exists(line & "\Modèles\PrintRapports.docx") Then
                File.Delete(line & "\Modèles\PrintRapports.docx")
            End If
            If File.Exists(line & "\Modèles\PrintMissions.docx") Then
                File.Delete(line & "\Modèles\PrintMissions.docx")
            End If
            If File.Exists(line & "\Modèles\PrintConges.docx") Then
                File.Delete(line & "\Modèles\PrintConges.docx")
            End If
            If File.Exists(line & "\Modèles\PrintPrevisionConges.docx") Then
                File.Delete(line & "\Modèles\PrintPrevisionConges.docx")
            End If
            If File.Exists(line & "\Modèles\PrintLivrePaye.docx") Then
                File.Delete(line & "\Modèles\PrintLivrePaye.docx")
            End If
            If File.Exists(line & "\Modèles\PrintFinancement.docx") Then
                File.Delete(line & "\Modèles\PrintFinancement.docx")
            End If
            If File.Exists(line & "\Modèles\Printdrf.docx") Then
                File.Delete(line & "\Modèles\Printdrf.docx")
            End If
            If File.Exists(line & "\Modèles\PrintEtatRecapDrf.docx") Then
                File.Delete(line & "\Modèles\PrintEtatRecapDrf.docx")
            End If
            If File.Exists(line & "\Modèles\PrintRapprochBailleur.docx") Then
                File.Delete(line & "\Modèles\PrintRapprochBailleur.docx")
            End If
            If File.Exists(line & "\Modèles\Printdpd.docx") Then
                File.Delete(line & "\Modèles\Printdpd.docx")
            End If
            If File.Exists(line & "\Modèles\PrintEtatRecapDpd.docx") Then
                File.Delete(line & "\Modèles\PrintEtatRecapDpd.docx")
            End If
            If File.Exists(line & "\Modèles\PrintRapActivites.docx") Then
                File.Delete(line & "\Modèles\PrintRapActivites.docx")
            End If
            If File.Exists(line & "\Modèles\PrintFinancement.docx") Then
                File.Delete(line & "\Modèles\PrintFinancement.docx")
            End If

            Me.Close()
            cur_form.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub pbFermerDoc_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbFermerDoc.MouseDown
        pbFermerDoc.BorderStyle = BorderStyle.Fixed3D
    End Sub

    Private Sub pbFermerDoc_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbFermerDoc.MouseUp
        pbFermerDoc.BorderStyle = BorderStyle.None
    End Sub

    Private Sub pbImpDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbImpDoc.Click
        'Visioword.ShowPrintDialog()
    End Sub


End Class
