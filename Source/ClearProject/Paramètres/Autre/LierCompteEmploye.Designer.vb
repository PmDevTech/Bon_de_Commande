<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LierCompteEmploye
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btOK = New DevExpress.XtraEditors.SimpleButton()
        Me.cmbEmploye = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.cmbEmploye.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btOK
        '
        Me.btOK.Location = New System.Drawing.Point(111, 38)
        Me.btOK.Name = "btOK"
        Me.btOK.Size = New System.Drawing.Size(99, 23)
        Me.btOK.TabIndex = 10
        Me.btOK.Text = "OK"
        '
        'cmbEmploye
        '
        Me.cmbEmploye.Location = New System.Drawing.Point(61, 10)
        Me.cmbEmploye.Name = "cmbEmploye"
        Me.cmbEmploye.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbEmploye.Properties.Appearance.Options.UseFont = True
        Me.cmbEmploye.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEmploye.Properties.Items.AddRange(New Object() {"Non défini"})
        Me.cmbEmploye.Size = New System.Drawing.Size(269, 22)
        Me.cmbEmploye.TabIndex = 5
        Me.cmbEmploye.ToolTip = "Civilité"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(4, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(48, 16)
        Me.LabelControl1.TabIndex = 14
        Me.LabelControl1.Text = "Employé"
        '
        'LierCompteEmploye
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(336, 67)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.cmbEmploye)
        Me.Controls.Add(Me.btOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LierCompteEmploye"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Liaison de compte d'accès"
        CType(Me.cmbEmploye.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmbEmploye As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
