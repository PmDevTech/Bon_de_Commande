<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Etat_sigfip
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.comb2 = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.comb1 = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btimprim = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.comb2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comb1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 207)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(465, 24)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "-----------------------------------------------------------------"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(36, 132)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(78, 23)
        Me.LabelControl2.TabIndex = 16
        Me.LabelControl2.Text = "à compte"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(36, 63)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(90, 23)
        Me.LabelControl1.TabIndex = 15
        Me.LabelControl1.Text = "du compte"
        '
        'comb2
        '
        Me.comb2.Location = New System.Drawing.Point(181, 130)
        Me.comb2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.comb2.Name = "comb2"
        Me.comb2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.comb2.Size = New System.Drawing.Size(274, 30)
        Me.comb2.TabIndex = 14
        '
        'comb1
        '
        Me.comb1.Location = New System.Drawing.Point(181, 60)
        Me.comb1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.comb1.Name = "comb1"
        Me.comb1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.comb1.Size = New System.Drawing.Size(274, 30)
        Me.comb1.TabIndex = 13
        '
        'btimprim
        '
        Me.btimprim.Location = New System.Drawing.Point(167, 275)
        Me.btimprim.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.btimprim.Name = "btimprim"
        Me.btimprim.Size = New System.Drawing.Size(165, 40)
        Me.btimprim.TabIndex = 12
        Me.btimprim.Text = "Imprimer"
        '
        'Etat_sigfip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(502, 337)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.comb2)
        Me.Controls.Add(Me.comb1)
        Me.Controls.Add(Me.btimprim)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Etat_sigfip"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Plan Sigfip"
        CType(Me.comb2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comb1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents comb2 As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents comb1 As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btimprim As DevExpress.XtraEditors.SimpleButton
End Class
