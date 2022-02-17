<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TypeRepartitionBudget
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
        Me.CmbTypeRepart = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.BtEnrg = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.CmbTypeRepart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(14, 24)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(151, 15)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Répartir le financement par"
        '
        'CmbTypeRepart
        '
        Me.CmbTypeRepart.Location = New System.Drawing.Point(14, 45)
        Me.CmbTypeRepart.Name = "CmbTypeRepart"
        Me.CmbTypeRepart.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbTypeRepart.Properties.Appearance.Options.UseFont = True
        Me.CmbTypeRepart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbTypeRepart.Properties.Items.AddRange(New Object() {"Composante", "Sous composante", "Activité"})
        Me.CmbTypeRepart.Size = New System.Drawing.Size(266, 22)
        Me.CmbTypeRepart.TabIndex = 1
        '
        'BtEnrg
        '
        Me.BtEnrg.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnrg.Appearance.Options.UseFont = True
        Me.BtEnrg.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.BtEnrg.Location = New System.Drawing.Point(176, 79)
        Me.BtEnrg.Name = "BtEnrg"
        Me.BtEnrg.Size = New System.Drawing.Size(104, 26)
        Me.BtEnrg.TabIndex = 2
        Me.BtEnrg.Text = "Enregistrer"
        '
        'TypeRepartitionBudget
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 115)
        Me.Controls.Add(Me.BtEnrg)
        Me.Controls.Add(Me.CmbTypeRepart)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TypeRepartitionBudget"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Type de répartition"
        CType(Me.CmbTypeRepart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CmbTypeRepart As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents BtEnrg As DevExpress.XtraEditors.SimpleButton
End Class
