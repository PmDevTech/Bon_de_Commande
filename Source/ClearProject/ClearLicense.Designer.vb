<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ClearLicense
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
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtCleLic = New DevExpress.XtraEditors.TextEdit()
        Me.BtValider = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.TxtCleLic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(48, 34)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(389, 42)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Entrer la clé de license ..."
        '
        'TxtCleLic
        '
        Me.TxtCleLic.Location = New System.Drawing.Point(45, 81)
        Me.TxtCleLic.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.TxtCleLic.Name = "TxtCleLic"
        Me.TxtCleLic.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCleLic.Properties.Appearance.Options.UseFont = True
        Me.TxtCleLic.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtCleLic.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtCleLic.Properties.Mask.BeepOnError = True
        Me.TxtCleLic.Properties.Mask.EditMask = "[0-9,A-Z]{5} - [0-9,A-Z]{5} - [0-9,A-Z]{5} - [0-9,A-Z]{5}"
        Me.TxtCleLic.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.TxtCleLic.Properties.Mask.ShowPlaceHolders = False
        Me.TxtCleLic.Size = New System.Drawing.Size(580, 48)
        Me.TxtCleLic.TabIndex = 1
        '
        'BtValider
        '
        Me.BtValider.Appearance.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtValider.Appearance.Options.UseFont = True
        Me.BtValider.Location = New System.Drawing.Point(45, 159)
        Me.BtValider.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.BtValider.Name = "BtValider"
        Me.BtValider.Size = New System.Drawing.Size(580, 53)
        Me.BtValider.TabIndex = 2
        Me.BtValider.Text = "Activer la création de Projet"
        '
        'ClearLicense
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(672, 241)
        Me.Controls.Add(Me.BtValider)
        Me.Controls.Add(Me.TxtCleLic)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ClearLicense"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Clé de License"
        Me.TopMost = True
        CType(Me.TxtCleLic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtCleLic As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BtValider As DevExpress.XtraEditors.SimpleButton
End Class
