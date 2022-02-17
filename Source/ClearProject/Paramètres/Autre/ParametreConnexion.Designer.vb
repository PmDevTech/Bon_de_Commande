<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ParametreConnexion
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
        Me.LblConnFalse = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.BtQuitter = New DevExpress.XtraEditors.SimpleButton()
        Me.BtEnregistrer = New DevExpress.XtraEditors.SimpleButton()
        Me.BtTester = New DevExpress.XtraEditors.SimpleButton()
        Me.LblConnOk = New DevExpress.XtraEditors.LabelControl()
        Me.TxtMdp = New DevExpress.XtraEditors.TextEdit()
        Me.TxtUtil = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtBd = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.CmbServeur = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtPort = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.TxtMdp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtUtil.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbServeur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPort.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblConnFalse
        '
        Me.LblConnFalse.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblConnFalse.Appearance.ForeColor = System.Drawing.Color.Red
        Me.LblConnFalse.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LblConnFalse.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblConnFalse.Location = New System.Drawing.Point(162, 149)
        Me.LblConnFalse.Name = "LblConnFalse"
        Me.LblConnFalse.Size = New System.Drawing.Size(171, 25)
        Me.LblConnFalse.TabIndex = 22
        Me.LblConnFalse.Text = "CONNEXION ECHOUEE"
        Me.LblConnFalse.Visible = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.BtQuitter)
        Me.PanelControl1.Controls.Add(Me.BtEnregistrer)
        Me.PanelControl1.Controls.Add(Me.BtTester)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 187)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(406, 38)
        Me.PanelControl1.TabIndex = 20
        '
        'BtQuitter
        '
        Me.BtQuitter.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtQuitter.Appearance.Options.UseFont = True
        Me.BtQuitter.Image = Global.ClearProject.My.Resources.Resources.Delete_16x16
        Me.BtQuitter.Location = New System.Drawing.Point(5, 6)
        Me.BtQuitter.Name = "BtQuitter"
        Me.BtQuitter.Size = New System.Drawing.Size(105, 27)
        Me.BtQuitter.TabIndex = 10
        Me.BtQuitter.Text = "Annuler"
        '
        'BtEnregistrer
        '
        Me.BtEnregistrer.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnregistrer.Appearance.Options.UseFont = True
        Me.BtEnregistrer.Enabled = False
        Me.BtEnregistrer.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.BtEnregistrer.Location = New System.Drawing.Point(296, 6)
        Me.BtEnregistrer.Name = "BtEnregistrer"
        Me.BtEnregistrer.Size = New System.Drawing.Size(105, 27)
        Me.BtEnregistrer.TabIndex = 9
        Me.BtEnregistrer.Text = "Enregistrer"
        '
        'BtTester
        '
        Me.BtTester.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtTester.Appearance.Options.UseFont = True
        Me.BtTester.Image = Global.ClearProject.My.Resources.Resources.ActiveRents_16x16
        Me.BtTester.Location = New System.Drawing.Point(179, 6)
        Me.BtTester.Name = "BtTester"
        Me.BtTester.Size = New System.Drawing.Size(105, 27)
        Me.BtTester.TabIndex = 8
        Me.BtTester.Text = "Tester"
        '
        'LblConnOk
        '
        Me.LblConnOk.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblConnOk.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblConnOk.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LblConnOk.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblConnOk.Location = New System.Drawing.Point(162, 149)
        Me.LblConnOk.Name = "LblConnOk"
        Me.LblConnOk.Size = New System.Drawing.Size(171, 25)
        Me.LblConnOk.TabIndex = 21
        Me.LblConnOk.Text = "CONNEXION REUSSIE"
        Me.LblConnOk.Visible = False
        '
        'TxtMdp
        '
        Me.TxtMdp.Location = New System.Drawing.Point(117, 121)
        Me.TxtMdp.Name = "TxtMdp"
        Me.TxtMdp.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMdp.Properties.Appearance.Options.UseFont = True
        Me.TxtMdp.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtMdp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtMdp.Properties.UseSystemPasswordChar = True
        Me.TxtMdp.Size = New System.Drawing.Size(273, 22)
        Me.TxtMdp.TabIndex = 19
        '
        'TxtUtil
        '
        Me.TxtUtil.Location = New System.Drawing.Point(117, 93)
        Me.TxtUtil.Name = "TxtUtil"
        Me.TxtUtil.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUtil.Properties.Appearance.Options.UseFont = True
        Me.TxtUtil.Size = New System.Drawing.Size(273, 22)
        Me.TxtUtil.TabIndex = 18
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Location = New System.Drawing.Point(46, 124)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(69, 15)
        Me.LabelControl4.TabIndex = 17
        Me.LabelControl4.Text = "Mot de passe"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(56, 96)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(59, 15)
        Me.LabelControl3.TabIndex = 16
        Me.LabelControl3.Text = "Utilisateur"
        '
        'TxtBd
        '
        Me.TxtBd.Location = New System.Drawing.Point(117, 37)
        Me.TxtBd.Name = "TxtBd"
        Me.TxtBd.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBd.Properties.Appearance.Options.UseFont = True
        Me.TxtBd.Size = New System.Drawing.Size(273, 22)
        Me.TxtBd.TabIndex = 15
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(23, 40)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(92, 15)
        Me.LabelControl2.TabIndex = 14
        Me.LabelControl2.Text = "Base de Données"
        '
        'CmbServeur
        '
        Me.CmbServeur.Location = New System.Drawing.Point(117, 9)
        Me.CmbServeur.Name = "CmbServeur"
        Me.CmbServeur.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbServeur.Properties.Appearance.Options.UseFont = True
        Me.CmbServeur.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbServeur.Size = New System.Drawing.Size(273, 22)
        Me.CmbServeur.TabIndex = 13
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(71, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(44, 15)
        Me.LabelControl1.TabIndex = 12
        Me.LabelControl1.Text = "Serveur"
        '
        'TxtPort
        '
        Me.TxtPort.Location = New System.Drawing.Point(117, 64)
        Me.TxtPort.Name = "TxtPort"
        Me.TxtPort.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPort.Properties.Appearance.Options.UseFont = True
        Me.TxtPort.Size = New System.Drawing.Size(273, 22)
        Me.TxtPort.TabIndex = 16
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Location = New System.Drawing.Point(91, 67)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(24, 15)
        Me.LabelControl5.TabIndex = 23
        Me.LabelControl5.Text = "Port"
        '
        'ParametreConnexion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(406, 225)
        Me.ControlBox = False
        Me.Controls.Add(Me.TxtPort)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.LblConnFalse)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.LblConnOk)
        Me.Controls.Add(Me.TxtMdp)
        Me.Controls.Add(Me.TxtUtil)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.TxtBd)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.CmbServeur)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ParametreConnexion"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Paramètres de Connexion"
        Me.TopMost = True
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.TxtMdp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtUtil.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbServeur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPort.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblConnFalse As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtQuitter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtEnregistrer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtTester As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LblConnOk As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtMdp As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtUtil As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtBd As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CmbServeur As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtPort As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
End Class
