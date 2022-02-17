<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EvaluateurPresent
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
        Me.TxtCode = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LblRefuse = New DevExpress.XtraEditors.LabelControl()
        CType(Me.TxtCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtCode
        '
        Me.TxtCode.EditValue = ""
        Me.TxtCode.Location = New System.Drawing.Point(121, 73)
        Me.TxtCode.Name = "TxtCode"
        Me.TxtCode.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCode.Properties.Appearance.Options.UseFont = True
        Me.TxtCode.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtCode.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtCode.Properties.UseSystemPasswordChar = True
        Me.TxtCode.Size = New System.Drawing.Size(195, 26)
        Me.TxtCode.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(122, 54)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(144, 19)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Entrez votre code svp"
        '
        'LblRefuse
        '
        Me.LblRefuse.Appearance.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRefuse.Appearance.ForeColor = System.Drawing.Color.Red
        Me.LblRefuse.Location = New System.Drawing.Point(84, 121)
        Me.LblRefuse.Name = "LblRefuse"
        Me.LblRefuse.Size = New System.Drawing.Size(266, 22)
        Me.LblRefuse.TabIndex = 2
        Me.LblRefuse.Text = ">>>>> ACCES REFUSE <<<<<"
        Me.LblRefuse.Visible = False
        '
        'EvaluateurPresent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(454, 173)
        Me.Controls.Add(Me.LblRefuse)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.TxtCode)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EvaluateurPresent"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mot de passe"
        CType(Me.TxtCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TxtCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblRefuse As DevExpress.XtraEditors.LabelControl
End Class
