<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FullScreenReport
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
        Me.FullView = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.BtImpressionPV = New DevExpress.XtraEditors.SimpleButton()
        Me.SuspendLayout()
        '
        'FullView
        '
        Me.FullView.ActiveViewIndex = -1
        Me.FullView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FullView.Cursor = System.Windows.Forms.Cursors.Default
        Me.FullView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FullView.Location = New System.Drawing.Point(0, 0)
        Me.FullView.Name = "FullView"
        Me.FullView.SelectionFormula = ""
        Me.FullView.ShowCloseButton = False
        Me.FullView.ShowGotoPageButton = False
        Me.FullView.ShowGroupTreeButton = False
        Me.FullView.ShowRefreshButton = False
        Me.FullView.Size = New System.Drawing.Size(822, 423)
        Me.FullView.TabIndex = 0
        Me.FullView.ViewTimeSelectionFormula = ""
        '
        'BtImpressionPV
        '
        Me.BtImpressionPV.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtImpressionPV.Location = New System.Drawing.Point(883, 2)
        Me.BtImpressionPV.Name = "BtImpressionPV"
        Me.BtImpressionPV.Size = New System.Drawing.Size(131, 21)
        Me.BtImpressionPV.TabIndex = 1
        Me.BtImpressionPV.Text = "Imprimer"
        Me.BtImpressionPV.Visible = False
        '
        'FullScreenReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(822, 423)
        Me.Controls.Add(Me.BtImpressionPV)
        Me.Controls.Add(Me.FullView)
        Me.Name = "FullScreenReport"
        Me.Text = "Plein écran"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtImpressionPV As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FullView As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
