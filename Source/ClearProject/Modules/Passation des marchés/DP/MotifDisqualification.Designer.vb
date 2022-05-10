<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MotifDisqualification
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.TxtNomConslt = New DevExpress.XtraEditors.TextEdit()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.BtQuitter = New DevExpress.XtraEditors.SimpleButton()
        Me.BtEnregMotif = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtMotif = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.TxtNomConslt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.TxtMotif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.TxtNomConslt)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(685, 32)
        Me.PanelControl1.TabIndex = 0
        '
        'TxtNomConslt
        '
        Me.TxtNomConslt.Dock = System.Windows.Forms.DockStyle.Top
        Me.TxtNomConslt.EditValue = ""
        Me.TxtNomConslt.Location = New System.Drawing.Point(2, 2)
        Me.TxtNomConslt.Name = "TxtNomConslt"
        Me.TxtNomConslt.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNomConslt.Properties.Appearance.ForeColor = System.Drawing.Color.Red
        Me.TxtNomConslt.Properties.Appearance.Options.UseFont = True
        Me.TxtNomConslt.Properties.Appearance.Options.UseForeColor = True
        Me.TxtNomConslt.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtNomConslt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtNomConslt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.TxtNomConslt.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtNomConslt.Properties.ReadOnly = True
        Me.TxtNomConslt.Size = New System.Drawing.Size(681, 28)
        Me.TxtNomConslt.TabIndex = 0
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.BtQuitter)
        Me.PanelControl2.Controls.Add(Me.BtEnregMotif)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl2.Location = New System.Drawing.Point(0, 164)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(685, 40)
        Me.PanelControl2.TabIndex = 1
        '
        'BtQuitter
        '
        Me.BtQuitter.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtQuitter.Appearance.Options.UseFont = True
        Me.BtQuitter.Dock = System.Windows.Forms.DockStyle.Left
        Me.BtQuitter.Image = Global.ClearProject.My.Resources.Resources.Close_32x32
        Me.BtQuitter.Location = New System.Drawing.Point(2, 2)
        Me.BtQuitter.Name = "BtQuitter"
        Me.BtQuitter.Size = New System.Drawing.Size(128, 36)
        Me.BtQuitter.TabIndex = 1
        Me.BtQuitter.Text = "Annuler"
        '
        'BtEnregMotif
        '
        Me.BtEnregMotif.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnregMotif.Appearance.Options.UseFont = True
        Me.BtEnregMotif.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtEnregMotif.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_32x32
        Me.BtEnregMotif.Location = New System.Drawing.Point(555, 2)
        Me.BtEnregMotif.Name = "BtEnregMotif"
        Me.BtEnregMotif.Size = New System.Drawing.Size(128, 36)
        Me.BtEnregMotif.TabIndex = 0
        Me.BtEnregMotif.Text = "Enregistrer"
        '
        'TxtMotif
        '
        Me.TxtMotif.Location = New System.Drawing.Point(6, 86)
        Me.TxtMotif.Name = "TxtMotif"
        Me.TxtMotif.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMotif.Properties.Appearance.Options.UseFont = True
        Me.TxtMotif.Properties.MaxLength = 500
        Me.TxtMotif.Size = New System.Drawing.Size(676, 72)
        Me.TxtMotif.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl1.Location = New System.Drawing.Point(9, 65)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(325, 16)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "Spécifiez la raison de la disqualification du soumissionaire"
        '
        'MotifDisqualification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(685, 204)
        Me.ControlBox = False
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.TxtMotif)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "MotifDisqualification"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Raison de la disqualification"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.TxtNomConslt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.TxtMotif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtEnregMotif As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtQuitter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtMotif As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents TxtNomConslt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
