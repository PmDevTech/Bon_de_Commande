<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LaisonEtat
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
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.btEnreg = New DevExpress.XtraEditors.SimpleButton()
        Me.CombBail = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.CombEtat = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.Txtmodule = New DevExpress.XtraEditors.TextEdit()
        CType(Me.CombBail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CombEtat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txtmodule.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 195)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(486, 24)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "--------------------------------------------------------------------"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(39, 256)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(61, 23)
        Me.LabelControl3.TabIndex = 28
        Me.LabelControl3.Text = "Module"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(39, 120)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(33, 23)
        Me.LabelControl2.TabIndex = 27
        Me.LabelControl2.Text = "Etat"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(39, 51)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(61, 23)
        Me.LabelControl1.TabIndex = 26
        Me.LabelControl1.Text = "Bailleur"
        '
        'btEnreg
        '
        Me.btEnreg.Image = Global.ClearProject.My.Resources.Resources.Group_Reports
        Me.btEnreg.Location = New System.Drawing.Point(138, 342)
        Me.btEnreg.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.btEnreg.Name = "btEnreg"
        Me.btEnreg.Size = New System.Drawing.Size(231, 40)
        Me.btEnreg.TabIndex = 24
        Me.btEnreg.Text = "Enregistrer"
        '
        'CombBail
        '
        Me.CombBail.Location = New System.Drawing.Point(143, 44)
        Me.CombBail.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.CombBail.Name = "CombBail"
        Me.CombBail.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CombBail.Properties.Items.AddRange(New Object() {"Par Famille", "Par Fournisseur"})
        Me.CombBail.Size = New System.Drawing.Size(351, 30)
        Me.CombBail.TabIndex = 30
        '
        'CombEtat
        '
        Me.CombEtat.Location = New System.Drawing.Point(143, 113)
        Me.CombEtat.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.CombEtat.Name = "CombEtat"
        Me.CombEtat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CombEtat.Size = New System.Drawing.Size(351, 30)
        Me.CombEtat.TabIndex = 30
        '
        'Txtmodule
        '
        Me.Txtmodule.Location = New System.Drawing.Point(143, 249)
        Me.Txtmodule.Name = "Txtmodule"
        Me.Txtmodule.Size = New System.Drawing.Size(351, 30)
        Me.Txtmodule.TabIndex = 31
        '
        'LaisonEtat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(525, 414)
        Me.Controls.Add(Me.Txtmodule)
        Me.Controls.Add(Me.CombEtat)
        Me.Controls.Add(Me.CombBail)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.btEnreg)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(549, 478)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(531, 460)
        Me.Name = "LaisonEtat"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LaisonEtat"
        CType(Me.CombBail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CombEtat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txtmodule.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btEnreg As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CombBail As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents CombEtat As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents Txtmodule As DevExpress.XtraEditors.TextEdit
End Class
