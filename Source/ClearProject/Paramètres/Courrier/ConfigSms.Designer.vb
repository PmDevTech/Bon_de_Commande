<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConfigSms
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
        Me.CmbTerminal = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.CmbVitesse = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtCodePin = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.CmbEncodage = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ChkAffichePin = New DevExpress.XtraEditors.CheckEdit()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.BtEnreg = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtNumTest = New DevExpress.XtraEditors.TextEdit()
        Me.BtTestTerminal = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtNumTerminal = New DevExpress.XtraEditors.TextEdit()
        Me.PnlConfig = New DevExpress.XtraEditors.PanelControl()
        Me.TxtNomTerminal = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtNumSerie = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.CmbTerminal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbVitesse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCodePin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbEncodage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkAffichePin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.TxtNumTest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtNumTerminal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PnlConfig, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlConfig.SuspendLayout()
        CType(Me.TxtNomTerminal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtNumSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(12, 6)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(50, 15)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Terminal"
        '
        'CmbTerminal
        '
        Me.CmbTerminal.Location = New System.Drawing.Point(12, 22)
        Me.CmbTerminal.Name = "CmbTerminal"
        Me.CmbTerminal.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbTerminal.Properties.Appearance.Options.UseFont = True
        Me.CmbTerminal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbTerminal.Size = New System.Drawing.Size(141, 22)
        Me.CmbTerminal.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(12, 50)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(38, 15)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Vitesse"
        '
        'CmbVitesse
        '
        Me.CmbVitesse.Location = New System.Drawing.Point(12, 66)
        Me.CmbVitesse.Name = "CmbVitesse"
        Me.CmbVitesse.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbVitesse.Properties.Appearance.Options.UseFont = True
        Me.CmbVitesse.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbVitesse.Size = New System.Drawing.Size(141, 22)
        Me.CmbVitesse.TabIndex = 3
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(172, 50)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(55, 15)
        Me.LabelControl3.TabIndex = 4
        Me.LabelControl3.Text = "Code PIN"
        '
        'TxtCodePin
        '
        Me.TxtCodePin.EditValue = ""
        Me.TxtCodePin.Location = New System.Drawing.Point(172, 66)
        Me.TxtCodePin.Name = "TxtCodePin"
        Me.TxtCodePin.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCodePin.Properties.Appearance.Options.UseFont = True
        Me.TxtCodePin.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtCodePin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtCodePin.Properties.Mask.BeepOnError = True
        Me.TxtCodePin.Properties.Mask.EditMask = "\d{4,8}"
        Me.TxtCodePin.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.TxtCodePin.Properties.Mask.ShowPlaceHolders = False
        Me.TxtCodePin.Size = New System.Drawing.Size(119, 22)
        Me.TxtCodePin.TabIndex = 5
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Location = New System.Drawing.Point(378, 50)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(55, 15)
        Me.LabelControl4.TabIndex = 6
        Me.LabelControl4.Text = "Encodage"
        '
        'CmbEncodage
        '
        Me.CmbEncodage.Location = New System.Drawing.Point(377, 66)
        Me.CmbEncodage.Name = "CmbEncodage"
        Me.CmbEncodage.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbEncodage.Properties.Appearance.Options.UseFont = True
        Me.CmbEncodage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbEncodage.Size = New System.Drawing.Size(124, 22)
        Me.CmbEncodage.TabIndex = 7
        '
        'ChkAffichePin
        '
        Me.ChkAffichePin.EditValue = True
        Me.ChkAffichePin.Location = New System.Drawing.Point(292, 67)
        Me.ChkAffichePin.Name = "ChkAffichePin"
        Me.ChkAffichePin.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAffichePin.Properties.Appearance.Options.UseFont = True
        Me.ChkAffichePin.Properties.Caption = "Afficher"
        Me.ChkAffichePin.Size = New System.Drawing.Size(73, 20)
        Me.ChkAffichePin.TabIndex = 8
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.BtEnreg)
        Me.PanelControl1.Controls.Add(Me.TxtNumTest)
        Me.PanelControl1.Controls.Add(Me.BtTestTerminal)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 98)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(513, 36)
        Me.PanelControl1.TabIndex = 9
        '
        'BtEnreg
        '
        Me.BtEnreg.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnreg.Appearance.Options.UseFont = True
        Me.BtEnreg.Enabled = False
        Me.BtEnreg.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.BtEnreg.Location = New System.Drawing.Point(377, 5)
        Me.BtEnreg.Name = "BtEnreg"
        Me.BtEnreg.Size = New System.Drawing.Size(124, 26)
        Me.BtEnreg.TabIndex = 11
        Me.BtEnreg.Text = "Enregistrer"
        '
        'TxtNumTest
        '
        Me.TxtNumTest.Location = New System.Drawing.Point(12, 7)
        Me.TxtNumTest.Name = "TxtNumTest"
        Me.TxtNumTest.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNumTest.Properties.Appearance.Options.UseFont = True
        Me.TxtNumTest.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtNumTest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtNumTest.Properties.Mask.BeepOnError = True
        Me.TxtNumTest.Properties.Mask.EditMask = "([+]{1}\d{1,3}) \d\d \d\d \d\d \d\d"
        Me.TxtNumTest.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.TxtNumTest.Size = New System.Drawing.Size(141, 22)
        Me.TxtNumTest.TabIndex = 10
        Me.TxtNumTest.ToolTip = "N° Tél."
        '
        'BtTestTerminal
        '
        Me.BtTestTerminal.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtTestTerminal.Appearance.Options.UseFont = True
        Me.BtTestTerminal.Image = Global.ClearProject.My.Resources.Resources.ActiveRents_16x16
        Me.BtTestTerminal.Location = New System.Drawing.Point(172, 5)
        Me.BtTestTerminal.Name = "BtTestTerminal"
        Me.BtTestTerminal.Size = New System.Drawing.Size(193, 26)
        Me.BtTestTerminal.TabIndex = 0
        Me.BtTestTerminal.Text = "Tester terminal"
        '
        'TxtNumTerminal
        '
        Me.TxtNumTerminal.Location = New System.Drawing.Point(172, 22)
        Me.TxtNumTerminal.Name = "TxtNumTerminal"
        Me.TxtNumTerminal.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNumTerminal.Properties.Appearance.Options.UseFont = True
        Me.TxtNumTerminal.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtNumTerminal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtNumTerminal.Properties.Mask.BeepOnError = True
        Me.TxtNumTerminal.Properties.Mask.EditMask = "([+]{1}\d{1,3}) \d\d \d\d \d\d \d\d"
        Me.TxtNumTerminal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.TxtNumTerminal.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.TxtNumTerminal.Properties.ReadOnly = True
        Me.TxtNumTerminal.Size = New System.Drawing.Size(119, 22)
        Me.TxtNumTerminal.TabIndex = 11
        '
        'PnlConfig
        '
        Me.PnlConfig.Controls.Add(Me.LabelControl6)
        Me.PnlConfig.Controls.Add(Me.TxtNumSerie)
        Me.PnlConfig.Controls.Add(Me.TxtNomTerminal)
        Me.PnlConfig.Controls.Add(Me.LabelControl5)
        Me.PnlConfig.Controls.Add(Me.CmbTerminal)
        Me.PnlConfig.Controls.Add(Me.TxtNumTerminal)
        Me.PnlConfig.Controls.Add(Me.LabelControl1)
        Me.PnlConfig.Controls.Add(Me.LabelControl2)
        Me.PnlConfig.Controls.Add(Me.ChkAffichePin)
        Me.PnlConfig.Controls.Add(Me.CmbVitesse)
        Me.PnlConfig.Controls.Add(Me.CmbEncodage)
        Me.PnlConfig.Controls.Add(Me.LabelControl3)
        Me.PnlConfig.Controls.Add(Me.LabelControl4)
        Me.PnlConfig.Controls.Add(Me.TxtCodePin)
        Me.PnlConfig.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlConfig.Location = New System.Drawing.Point(0, 0)
        Me.PnlConfig.Name = "PnlConfig"
        Me.PnlConfig.Size = New System.Drawing.Size(513, 99)
        Me.PnlConfig.TabIndex = 12
        '
        'TxtNomTerminal
        '
        Me.TxtNomTerminal.Location = New System.Drawing.Point(294, 22)
        Me.TxtNomTerminal.Name = "TxtNomTerminal"
        Me.TxtNomTerminal.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNomTerminal.Properties.Appearance.Options.UseFont = True
        Me.TxtNomTerminal.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtNomTerminal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtNomTerminal.Properties.ReadOnly = True
        Me.TxtNomTerminal.Size = New System.Drawing.Size(207, 22)
        Me.TxtNomTerminal.TabIndex = 13
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Location = New System.Drawing.Point(172, 6)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(49, 15)
        Me.LabelControl5.TabIndex = 12
        Me.LabelControl5.Text = "MSISDN"
        '
        'TxtNumSerie
        '
        Me.TxtNumSerie.Location = New System.Drawing.Point(310, 47)
        Me.TxtNumSerie.Name = "TxtNumSerie"
        Me.TxtNumSerie.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNumSerie.Properties.Appearance.Options.UseFont = True
        Me.TxtNumSerie.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtNumSerie.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtNumSerie.Properties.ReadOnly = True
        Me.TxtNumSerie.Size = New System.Drawing.Size(55, 22)
        Me.TxtNumSerie.TabIndex = 14
        Me.TxtNumSerie.Visible = False
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6.Location = New System.Drawing.Point(295, 6)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(41, 15)
        Me.LabelControl6.TabIndex = 15
        Me.LabelControl6.Text = "Modèle"
        '
        'ConfigSms
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(513, 134)
        Me.Controls.Add(Me.PnlConfig)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ConfigSms"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Compte Sms"
        CType(Me.CmbTerminal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbVitesse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCodePin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbEncodage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkAffichePin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.TxtNumTest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtNumTerminal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PnlConfig, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlConfig.ResumeLayout(False)
        Me.PnlConfig.PerformLayout()
        CType(Me.TxtNomTerminal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtNumSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CmbTerminal As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CmbVitesse As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtCodePin As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CmbEncodage As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ChkAffichePin As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtEnreg As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtNumTest As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BtTestTerminal As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtNumTerminal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PnlConfig As DevExpress.XtraEditors.PanelControl
    Friend WithEvents TxtNomTerminal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtNumSerie As DevExpress.XtraEditors.TextEdit
End Class
