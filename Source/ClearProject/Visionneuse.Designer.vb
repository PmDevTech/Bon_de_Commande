<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Visionneuse
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.gcImpDoc = New DevExpress.XtraEditors.GroupControl()
        Me.pbFermerDoc = New System.Windows.Forms.PictureBox()
        Me.pbImpDoc = New System.Windows.Forms.PictureBox()
        Me.gcDocument = New DevExpress.XtraEditors.GroupControl()
        Me.VisionPdf = New System.Windows.Forms.WebBrowser()
        Me.Visioword = New DevExpress.XtraRichEdit.RichEditControl()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        CType(Me.gcImpDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcImpDoc.SuspendLayout()
        CType(Me.pbFermerDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbImpDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcDocument.SuspendLayout()
        Me.SuspendLayout()
        '
        'gcImpDoc
        '
        Me.gcImpDoc.Controls.Add(Me.pbFermerDoc)
        Me.gcImpDoc.Controls.Add(Me.pbImpDoc)
        Me.gcImpDoc.Dock = System.Windows.Forms.DockStyle.Top
        Me.gcImpDoc.Location = New System.Drawing.Point(0, 0)
        Me.gcImpDoc.Name = "gcImpDoc"
        Me.gcImpDoc.ShowCaption = False
        Me.gcImpDoc.Size = New System.Drawing.Size(934, 40)
        Me.gcImpDoc.TabIndex = 9
        '
        'pbFermerDoc
        '
        Me.pbFermerDoc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbFermerDoc.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbFermerDoc.Image = Global.ClearProject.My.Resources.Resources.CloseDetails_32x32
        Me.pbFermerDoc.Location = New System.Drawing.Point(898, 3)
        Me.pbFermerDoc.Name = "pbFermerDoc"
        Me.pbFermerDoc.Size = New System.Drawing.Size(34, 34)
        Me.pbFermerDoc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbFermerDoc.TabIndex = 4
        Me.pbFermerDoc.TabStop = False
        '
        'pbImpDoc
        '
        Me.pbImpDoc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbImpDoc.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbImpDoc.Image = Global.ClearProject.My.Resources.Resources.imprimer
        Me.pbImpDoc.Location = New System.Drawing.Point(835, 3)
        Me.pbImpDoc.Name = "pbImpDoc"
        Me.pbImpDoc.Size = New System.Drawing.Size(34, 34)
        Me.pbImpDoc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbImpDoc.TabIndex = 3
        Me.pbImpDoc.TabStop = False
        '
        'gcDocument
        '
        Me.gcDocument.Controls.Add(Me.VisionPdf)
        Me.gcDocument.Controls.Add(Me.Visioword)
        Me.gcDocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcDocument.Location = New System.Drawing.Point(0, 40)
        Me.gcDocument.Name = "gcDocument"
        Me.gcDocument.ShowCaption = False
        Me.gcDocument.Size = New System.Drawing.Size(934, 322)
        Me.gcDocument.TabIndex = 10
        Me.gcDocument.Text = "Document"
        '
        'VisionPdf
        '
        Me.VisionPdf.AllowNavigation = False
        Me.VisionPdf.AllowWebBrowserDrop = False
        Me.VisionPdf.IsWebBrowserContextMenuEnabled = False
        Me.VisionPdf.Location = New System.Drawing.Point(435, 54)
        Me.VisionPdf.MinimumSize = New System.Drawing.Size(20, 20)
        Me.VisionPdf.Name = "VisionPdf"
        Me.VisionPdf.Size = New System.Drawing.Size(226, 173)
        Me.VisionPdf.TabIndex = 1
        Me.VisionPdf.Visible = False
        Me.VisionPdf.WebBrowserShortcutsEnabled = False
        '
        'Visioword
        '
        Me.Visioword.Location = New System.Drawing.Point(2, 2)
        Me.Visioword.Name = "Visioword"
        Me.Visioword.ReadOnly = True
        Me.Visioword.Size = New System.Drawing.Size(932, 320)
        Me.Visioword.TabIndex = 0
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Visionneuse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(934, 362)
        Me.Controls.Add(Me.gcDocument)
        Me.Controls.Add(Me.gcImpDoc)
        Me.Name = "Visionneuse"
        Me.Text = "Visionneuse"
        CType(Me.gcImpDoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcImpDoc.ResumeLayout(False)
        CType(Me.pbFermerDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbImpDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcDocument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcDocument.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pbFermerDoc As System.Windows.Forms.PictureBox
    Friend WithEvents gcImpDoc As DevExpress.XtraEditors.GroupControl
    Friend WithEvents pbImpDoc As System.Windows.Forms.PictureBox
    Friend WithEvents gcDocument As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Visioword As DevExpress.XtraRichEdit.RichEditControl
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents VisionPdf As System.Windows.Forms.WebBrowser
End Class
