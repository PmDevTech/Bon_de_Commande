<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Compte_Classe
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btann = New DevExpress.XtraEditors.SimpleButton()
        Me.btenr = New DevExpress.XtraEditors.SimpleButton()
        Me.txtlibcl = New DevExpress.XtraEditors.TextEdit()
        Me.txtcl = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.txtlibcl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btann)
        Me.PanelControl1.Controls.Add(Me.btenr)
        Me.PanelControl1.Controls.Add(Me.txtlibcl)
        Me.PanelControl1.Controls.Add(Me.txtcl)
        Me.PanelControl1.Controls.Add(Me.LabelControl2)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(473, 364)
        Me.PanelControl1.TabIndex = 0
        '
        'btann
        '
        Me.btann.Location = New System.Drawing.Point(245, 267)
        Me.btann.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.btann.Name = "btann"
        Me.btann.Size = New System.Drawing.Size(135, 48)
        Me.btann.TabIndex = 5
        Me.btann.Text = "Annuler"
        '
        'btenr
        '
        Me.btenr.Location = New System.Drawing.Point(87, 267)
        Me.btenr.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.btenr.Name = "btenr"
        Me.btenr.Size = New System.Drawing.Size(125, 48)
        Me.btenr.TabIndex = 4
        Me.btenr.Text = "Enregistrer"
        '
        'txtlibcl
        '
        Me.txtlibcl.Location = New System.Drawing.Point(108, 155)
        Me.txtlibcl.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtlibcl.Name = "txtlibcl"
        Me.txtlibcl.Size = New System.Drawing.Size(323, 30)
        Me.txtlibcl.TabIndex = 3
        '
        'txtcl
        '
        Me.txtcl.Location = New System.Drawing.Point(108, 62)
        Me.txtcl.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtcl.Name = "txtcl"
        Me.txtcl.Size = New System.Drawing.Size(323, 30)
        Me.txtcl.TabIndex = 2
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(37, 75)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(67, 23)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Numero"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(37, 161)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(52, 23)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Libellé"
        '
        'Compte_Classe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(473, 364)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Compte_Classe"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Compte Classe"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.txtlibcl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btann As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btenr As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtlibcl As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtcl As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
