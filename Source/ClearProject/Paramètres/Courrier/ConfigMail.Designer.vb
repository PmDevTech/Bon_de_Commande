<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConfigMail
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
        Me.CmbNom = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TxtHote = New DevExpress.XtraEditors.TextEdit()
        Me.ChkSecure = New DevExpress.XtraEditors.CheckEdit()
        Me.ChkAuthent = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtCompte = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtPortPop3 = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPort = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.PnlCompte = New DevExpress.XtraEditors.PanelControl()
        Me.BtTestConn = New DevExpress.XtraEditors.SimpleButton()
        Me.BtEnreg = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.TxtHotePop3 = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPasse = New System.Windows.Forms.TextBox()
        CType(Me.CmbNom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtHote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkSecure.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkAuthent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCompte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.TxtPortPop3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPort.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PnlCompte, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlCompte.SuspendLayout()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.TxtHotePop3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(30, 25)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(5)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(46, 26)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Hôte"
        '
        'CmbNom
        '
        Me.CmbNom.Location = New System.Drawing.Point(87, 19)
        Me.CmbNom.Margin = New System.Windows.Forms.Padding(5)
        Me.CmbNom.Name = "CmbNom"
        Me.CmbNom.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbNom.Properties.Appearance.Options.UseFont = True
        Me.CmbNom.Properties.Appearance.Options.UseTextOptions = True
        Me.CmbNom.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CmbNom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbNom.Size = New System.Drawing.Size(177, 32)
        Me.CmbNom.TabIndex = 1
        '
        'TxtHote
        '
        Me.TxtHote.EditValue = ""
        Me.TxtHote.Location = New System.Drawing.Point(270, 19)
        Me.TxtHote.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtHote.Name = "TxtHote"
        Me.TxtHote.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHote.Properties.Appearance.Options.UseFont = True
        Me.TxtHote.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtHote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtHote.Properties.ReadOnly = True
        Me.TxtHote.Size = New System.Drawing.Size(232, 32)
        Me.TxtHote.TabIndex = 2
        Me.TxtHote.ToolTip = "SMTP"
        '
        'ChkSecure
        '
        Me.ChkSecure.Location = New System.Drawing.Point(7, 71)
        Me.ChkSecure.Margin = New System.Windows.Forms.Padding(5)
        Me.ChkSecure.Name = "ChkSecure"
        Me.ChkSecure.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkSecure.Properties.Appearance.Options.UseFont = True
        Me.ChkSecure.Properties.Caption = "Serveur de messagerie sécurisé"
        Me.ChkSecure.Size = New System.Drawing.Size(343, 31)
        Me.ChkSecure.TabIndex = 3
        '
        'ChkAuthent
        '
        Me.ChkAuthent.Location = New System.Drawing.Point(7, 172)
        Me.ChkAuthent.Margin = New System.Windows.Forms.Padding(5)
        Me.ChkAuthent.Name = "ChkAuthent"
        Me.ChkAuthent.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAuthent.Properties.Appearance.Options.UseFont = True
        Me.ChkAuthent.Properties.Caption = "Authentification requise"
        Me.ChkAuthent.Size = New System.Drawing.Size(297, 31)
        Me.ChkAuthent.TabIndex = 4
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(3, 14)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(5)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(73, 26)
        Me.LabelControl2.TabIndex = 5
        Me.LabelControl2.Text = "Compte"
        '
        'TxtCompte
        '
        Me.TxtCompte.Location = New System.Drawing.Point(77, 9)
        Me.TxtCompte.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtCompte.Name = "TxtCompte"
        Me.TxtCompte.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCompte.Properties.Appearance.Options.UseFont = True
        Me.TxtCompte.Properties.Mask.BeepOnError = True
        Me.TxtCompte.Properties.Mask.EditMask = "[a-z,0-9,.,-,_]{4,50}[@]{1}[a-z,-,_]{3,6}[.]{1}[a-z]{2,5}"
        Me.TxtCompte.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.TxtCompte.Properties.Mask.ShowPlaceHolders = False
        Me.TxtCompte.Size = New System.Drawing.Size(272, 32)
        Me.TxtCompte.TabIndex = 6
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(363, 14)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(5)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(124, 26)
        Me.LabelControl3.TabIndex = 7
        Me.LabelControl3.Text = "Mot de passe"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.LabelControl5)
        Me.PanelControl1.Controls.Add(Me.TxtPortPop3)
        Me.PanelControl1.Controls.Add(Me.TxtPort)
        Me.PanelControl1.Controls.Add(Me.LabelControl4)
        Me.PanelControl1.Location = New System.Drawing.Point(10, 103)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(5)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(732, 58)
        Me.PanelControl1.TabIndex = 4
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Location = New System.Drawing.Point(392, 16)
        Me.LabelControl5.Margin = New System.Windows.Forms.Padding(5)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(60, 26)
        Me.LabelControl5.TabIndex = 12
        Me.LabelControl5.Text = "Port 2"
        '
        'TxtPortPop3
        '
        Me.TxtPortPop3.Location = New System.Drawing.Point(452, 11)
        Me.TxtPortPop3.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtPortPop3.Name = "TxtPortPop3"
        Me.TxtPortPop3.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPortPop3.Properties.Appearance.Options.UseFont = True
        Me.TxtPortPop3.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtPortPop3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtPortPop3.Properties.Mask.BeepOnError = True
        Me.TxtPortPop3.Properties.Mask.EditMask = "n0"
        Me.TxtPortPop3.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TxtPortPop3.Size = New System.Drawing.Size(272, 32)
        Me.TxtPortPop3.TabIndex = 11
        Me.TxtPortPop3.ToolTip = "POP3"
        '
        'TxtPort
        '
        Me.TxtPort.Location = New System.Drawing.Point(77, 11)
        Me.TxtPort.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtPort.Name = "TxtPort"
        Me.TxtPort.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPort.Properties.Appearance.Options.UseFont = True
        Me.TxtPort.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtPort.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtPort.Properties.Mask.BeepOnError = True
        Me.TxtPort.Properties.Mask.EditMask = "n0"
        Me.TxtPort.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TxtPort.Size = New System.Drawing.Size(272, 32)
        Me.TxtPort.TabIndex = 10
        Me.TxtPort.ToolTip = "SMTP"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Location = New System.Drawing.Point(17, 16)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(5)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(60, 26)
        Me.LabelControl4.TabIndex = 10
        Me.LabelControl4.Text = "Port 1"
        '
        'PnlCompte
        '
        Me.PnlCompte.Controls.Add(Me.TxtPasse)
        Me.PnlCompte.Controls.Add(Me.TxtCompte)
        Me.PnlCompte.Controls.Add(Me.LabelControl2)
        Me.PnlCompte.Controls.Add(Me.LabelControl3)
        Me.PnlCompte.Enabled = False
        Me.PnlCompte.Location = New System.Drawing.Point(10, 203)
        Me.PnlCompte.Margin = New System.Windows.Forms.Padding(5)
        Me.PnlCompte.Name = "PnlCompte"
        Me.PnlCompte.Size = New System.Drawing.Size(732, 57)
        Me.PnlCompte.TabIndex = 10
        '
        'BtTestConn
        '
        Me.BtTestConn.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtTestConn.Appearance.Options.UseFont = True
        Me.BtTestConn.Location = New System.Drawing.Point(10, 12)
        Me.BtTestConn.Margin = New System.Windows.Forms.Padding(5)
        Me.BtTestConn.Name = "BtTestConn"
        Me.BtTestConn.Size = New System.Drawing.Size(473, 42)
        Me.BtTestConn.TabIndex = 11
        Me.BtTestConn.Text = "Tester la connexion au serveur"
        '
        'BtEnreg
        '
        Me.BtEnreg.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnreg.Appearance.Options.UseFont = True
        Me.BtEnreg.Enabled = False
        Me.BtEnreg.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.BtEnreg.Location = New System.Drawing.Point(493, 12)
        Me.BtEnreg.Margin = New System.Windows.Forms.Padding(5)
        Me.BtEnreg.Name = "BtEnreg"
        Me.BtEnreg.Size = New System.Drawing.Size(248, 42)
        Me.BtEnreg.TabIndex = 13
        Me.BtEnreg.Text = "Enregistrer"
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.BtTestConn)
        Me.PanelControl3.Controls.Add(Me.BtEnreg)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl3.Location = New System.Drawing.Point(0, 286)
        Me.PanelControl3.Margin = New System.Windows.Forms.Padding(5)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(752, 64)
        Me.PanelControl3.TabIndex = 14
        '
        'TxtHotePop3
        '
        Me.TxtHotePop3.Location = New System.Drawing.Point(508, 19)
        Me.TxtHotePop3.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtHotePop3.Name = "TxtHotePop3"
        Me.TxtHotePop3.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHotePop3.Properties.Appearance.Options.UseFont = True
        Me.TxtHotePop3.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtHotePop3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtHotePop3.Properties.ReadOnly = True
        Me.TxtHotePop3.Size = New System.Drawing.Size(225, 32)
        Me.TxtHotePop3.TabIndex = 15
        Me.TxtHotePop3.ToolTip = "POP3"
        '
        'TxtPasse
        '
        Me.TxtPasse.Location = New System.Drawing.Point(489, 10)
        Me.TxtPasse.Name = "TxtPasse"
        Me.TxtPasse.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPasse.Size = New System.Drawing.Size(238, 30)
        Me.TxtPasse.TabIndex = 16
        '
        'ConfigMail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(752, 350)
        Me.Controls.Add(Me.TxtHotePop3)
        Me.Controls.Add(Me.PanelControl3)
        Me.Controls.Add(Me.PnlCompte)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.ChkAuthent)
        Me.Controls.Add(Me.ChkSecure)
        Me.Controls.Add(Me.TxtHote)
        Me.Controls.Add(Me.CmbNom)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ConfigMail"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Compte e-mail"
        CType(Me.CmbNom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtHote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkSecure.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkAuthent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCompte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.TxtPortPop3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPort.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PnlCompte, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlCompte.ResumeLayout(False)
        Me.PnlCompte.PerformLayout()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        CType(Me.TxtHotePop3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CmbNom As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents TxtHote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ChkSecure As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ChkAuthent As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtCompte As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents TxtPort As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PnlCompte As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtTestConn As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtEnreg As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtPortPop3 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtHotePop3 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPasse As System.Windows.Forms.TextBox
End Class
