<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ModifEcriture
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
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.CombE = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.CombA = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton2 = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.CombE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CombA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(52, 91)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(113, 23)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Code Ecriture"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(52, 185)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(107, 23)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "Code Activité"
        '
        'CombE
        '
        Me.CombE.Location = New System.Drawing.Point(223, 88)
        Me.CombE.Margin = New System.Windows.Forms.Padding(4)
        Me.CombE.Name = "CombE"
        Me.CombE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CombE.Size = New System.Drawing.Size(540, 30)
        Me.CombE.TabIndex = 1
        '
        'CombA
        '
        Me.CombA.Location = New System.Drawing.Point(223, 182)
        Me.CombA.Margin = New System.Windows.Forms.Padding(4)
        Me.CombA.Name = "CombA"
        Me.CombA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CombA.Size = New System.Drawing.Size(540, 30)
        Me.CombA.TabIndex = 1
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Location = New System.Drawing.Point(64, 303)
        Me.SimpleButton1.Margin = New System.Windows.Forms.Padding(4)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(302, 65)
        Me.SimpleButton1.TabIndex = 2
        Me.SimpleButton1.Text = "Modification"
        '
        'SimpleButton2
        '
        Me.SimpleButton2.Location = New System.Drawing.Point(436, 303)
        Me.SimpleButton2.Margin = New System.Windows.Forms.Padding(4)
        Me.SimpleButton2.Name = "SimpleButton2"
        Me.SimpleButton2.Size = New System.Drawing.Size(302, 65)
        Me.SimpleButton2.TabIndex = 2
        Me.SimpleButton2.Text = "Fermerture"
        '
        'ModifEcriture
        '
        Me.Appearance.Font = New System.Drawing.Font("Tahoma", 9.857143!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(13.0!, 28.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(832, 458)
        Me.Controls.Add(Me.SimpleButton2)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.CombA)
        Me.Controls.Add(Me.CombE)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ModifEcriture"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Modification Activités"
        CType(Me.CombE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CombA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CombE As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents CombA As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton2 As DevExpress.XtraEditors.SimpleButton
End Class
