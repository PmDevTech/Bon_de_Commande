<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectConsultant
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
        Me.CmbConsult = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtAdresseConsult = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.BtOk = New DevExpress.XtraEditors.SimpleButton()
        Me.BtQuitter = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.CmbConsult.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAdresseConsult.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmbConsult
        '
        Me.CmbConsult.Location = New System.Drawing.Point(124, 38)
        Me.CmbConsult.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.CmbConsult.Name = "CmbConsult"
        Me.CmbConsult.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbConsult.Properties.Appearance.Options.UseFont = True
        Me.CmbConsult.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbConsult.Size = New System.Drawing.Size(996, 36)
        Me.CmbConsult.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(14, 44)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(99, 25)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Consultant"
        '
        'TxtAdresseConsult
        '
        Me.TxtAdresseConsult.Location = New System.Drawing.Point(124, 94)
        Me.TxtAdresseConsult.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.TxtAdresseConsult.Name = "TxtAdresseConsult"
        Me.TxtAdresseConsult.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdresseConsult.Properties.Appearance.Options.UseFont = True
        Me.TxtAdresseConsult.Size = New System.Drawing.Size(996, 169)
        Me.TxtAdresseConsult.TabIndex = 2
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(40, 100)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(73, 25)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Adresse"
        '
        'BtOk
        '
        Me.BtOk.Location = New System.Drawing.Point(828, 275)
        Me.BtOk.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.BtOk.Name = "BtOk"
        Me.BtOk.Size = New System.Drawing.Size(140, 52)
        Me.BtOk.TabIndex = 4
        Me.BtOk.Text = "OK"
        '
        'BtQuitter
        '
        Me.BtQuitter.Location = New System.Drawing.Point(980, 275)
        Me.BtQuitter.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.BtQuitter.Name = "BtQuitter"
        Me.BtQuitter.Size = New System.Drawing.Size(140, 52)
        Me.BtQuitter.TabIndex = 5
        Me.BtQuitter.Text = "Quitter"
        '
        'SelectConsultant
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1144, 340)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtQuitter)
        Me.Controls.Add(Me.BtOk)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.TxtAdresseConsult)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.CmbConsult)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.Name = "SelectConsultant"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Selection du consultant"
        CType(Me.CmbConsult.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAdresseConsult.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CmbConsult As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtAdresseConsult As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtQuitter As DevExpress.XtraEditors.SimpleButton
End Class
