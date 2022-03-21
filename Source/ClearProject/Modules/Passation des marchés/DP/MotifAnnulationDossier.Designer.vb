<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MotifAnnulationDossier
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
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.BtQuitter = New DevExpress.XtraEditors.SimpleButton()
        Me.BtEnreMotifAnnul = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtTextAnnul = New DevExpress.XtraEditors.MemoEdit()
        Me.TxtTextDoss = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.TxtTextAnnul.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTextDoss.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.BtQuitter)
        Me.PanelControl2.Controls.Add(Me.BtEnreMotifAnnul)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl2.Location = New System.Drawing.Point(0, 178)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(517, 40)
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
        Me.BtQuitter.TabIndex = 2
        Me.BtQuitter.Text = "Annuler"
        '
        'BtEnreMotifAnnul
        '
        Me.BtEnreMotifAnnul.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnreMotifAnnul.Appearance.Options.UseFont = True
        Me.BtEnreMotifAnnul.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtEnreMotifAnnul.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_32x32
        Me.BtEnreMotifAnnul.Location = New System.Drawing.Point(387, 2)
        Me.BtEnreMotifAnnul.Name = "BtEnreMotifAnnul"
        Me.BtEnreMotifAnnul.Size = New System.Drawing.Size(128, 36)
        Me.BtEnreMotifAnnul.TabIndex = 1
        Me.BtEnreMotifAnnul.Text = "Enregistrer"
        '
        'TxtTextAnnul
        '
        Me.TxtTextAnnul.Location = New System.Drawing.Point(6, 72)
        Me.TxtTextAnnul.Name = "TxtTextAnnul"
        Me.TxtTextAnnul.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTextAnnul.Properties.Appearance.Options.UseFont = True
        Me.TxtTextAnnul.Size = New System.Drawing.Size(503, 99)
        Me.TxtTextAnnul.TabIndex = 0
        '
        'TxtTextDoss
        '
        Me.TxtTextDoss.Dock = System.Windows.Forms.DockStyle.Top
        Me.TxtTextDoss.EditValue = "Annulation du dossier N°"
        Me.TxtTextDoss.Location = New System.Drawing.Point(0, 0)
        Me.TxtTextDoss.Name = "TxtTextDoss"
        Me.TxtTextDoss.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTextDoss.Properties.Appearance.ForeColor = System.Drawing.Color.Red
        Me.TxtTextDoss.Properties.Appearance.Options.UseFont = True
        Me.TxtTextDoss.Properties.Appearance.Options.UseForeColor = True
        Me.TxtTextDoss.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtTextDoss.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtTextDoss.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.TxtTextDoss.Properties.ReadOnly = True
        Me.TxtTextDoss.Size = New System.Drawing.Size(517, 28)
        Me.TxtTextDoss.TabIndex = 7
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(9, 53)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(33, 14)
        Me.LabelControl1.TabIndex = 8
        Me.LabelControl1.Text = "Motif"
        '
        'MotifAnnulationDossier
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(517, 218)
        Me.ControlBox = False
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.TxtTextDoss)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.TxtTextAnnul)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "MotifAnnulationDossier"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Annulation dossier"
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.TxtTextAnnul.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTextDoss.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtEnreMotifAnnul As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtQuitter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtTextAnnul As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents TxtTextDoss As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
