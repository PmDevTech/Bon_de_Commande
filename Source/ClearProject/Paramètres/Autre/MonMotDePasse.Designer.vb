<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MonMotDePasse
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
        Me.NomOper = New DevExpress.XtraEditors.SimpleButton()
        Me.PhotoOperateur = New DevExpress.XtraEditors.PictureEdit()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.TxtPhoto = New DevExpress.XtraEditors.TextEdit()
        Me.TxtChemin = New DevExpress.XtraEditors.TextEdit()
        Me.BtEnregistrer = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtLogin = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.BtPhoto = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtOldPass = New System.Windows.Forms.TextBox()
        Me.TxtNewPass = New System.Windows.Forms.TextBox()
        Me.TxtNewPass2 = New System.Windows.Forms.TextBox()
        CType(Me.PhotoOperateur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.TxtPhoto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtChemin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtLogin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NomOper
        '
        Me.NomOper.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NomOper.Appearance.Options.UseFont = True
        Me.NomOper.Appearance.Options.UseTextOptions = True
        Me.NomOper.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.NomOper.Dock = System.Windows.Forms.DockStyle.Top
        Me.NomOper.Image = Global.ClearProject.My.Resources.Resources.Employee_16x16
        Me.NomOper.Location = New System.Drawing.Point(0, 0)
        Me.NomOper.Name = "NomOper"
        Me.NomOper.Size = New System.Drawing.Size(520, 30)
        Me.NomOper.TabIndex = 0
        '
        'PhotoOperateur
        '
        Me.PhotoOperateur.Dock = System.Windows.Forms.DockStyle.Left
        Me.PhotoOperateur.Location = New System.Drawing.Point(0, 30)
        Me.PhotoOperateur.Name = "PhotoOperateur"
        Me.PhotoOperateur.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PhotoOperateur.Properties.Appearance.Options.UseBackColor = True
        Me.PhotoOperateur.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.PhotoOperateur.Size = New System.Drawing.Size(188, 206)
        Me.PhotoOperateur.TabIndex = 29
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.TxtPhoto)
        Me.PanelControl1.Controls.Add(Me.TxtChemin)
        Me.PanelControl1.Controls.Add(Me.BtEnregistrer)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 236)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(520, 36)
        Me.PanelControl1.TabIndex = 30
        '
        'TxtPhoto
        '
        Me.TxtPhoto.Location = New System.Drawing.Point(211, 8)
        Me.TxtPhoto.Name = "TxtPhoto"
        Me.TxtPhoto.Size = New System.Drawing.Size(100, 20)
        Me.TxtPhoto.TabIndex = 2
        Me.TxtPhoto.Visible = False
        '
        'TxtChemin
        '
        Me.TxtChemin.Location = New System.Drawing.Point(12, 8)
        Me.TxtChemin.Name = "TxtChemin"
        Me.TxtChemin.Size = New System.Drawing.Size(187, 20)
        Me.TxtChemin.TabIndex = 1
        Me.TxtChemin.Visible = False
        '
        'BtEnregistrer
        '
        Me.BtEnregistrer.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnregistrer.Appearance.Options.UseFont = True
        Me.BtEnregistrer.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtEnregistrer.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.BtEnregistrer.Location = New System.Drawing.Point(402, 2)
        Me.BtEnregistrer.Name = "BtEnregistrer"
        Me.BtEnregistrer.Size = New System.Drawing.Size(116, 32)
        Me.BtEnregistrer.TabIndex = 0
        Me.BtEnregistrer.Text = "Modifier"
        '
        'TxtLogin
        '
        Me.TxtLogin.Location = New System.Drawing.Point(199, 56)
        Me.TxtLogin.Name = "TxtLogin"
        Me.TxtLogin.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLogin.Properties.Appearance.Options.UseFont = True
        Me.TxtLogin.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtLogin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtLogin.Properties.Mask.EditMask = "[A-Z,0-9]{5,10}"
        Me.TxtLogin.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.TxtLogin.Properties.Mask.ShowPlaceHolders = False
        Me.TxtLogin.Properties.ReadOnly = True
        Me.TxtLogin.Size = New System.Drawing.Size(313, 26)
        Me.TxtLogin.TabIndex = 31
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(202, 38)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(106, 17)
        Me.LabelControl1.TabIndex = 35
        Me.LabelControl1.Text = "Nom d'utilisateur"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(202, 87)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(126, 17)
        Me.LabelControl2.TabIndex = 36
        Me.LabelControl2.Text = "Mot de passe actuel"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(202, 135)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(140, 17)
        Me.LabelControl3.TabIndex = 37
        Me.LabelControl3.Text = "Nouveau mot de passe"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Location = New System.Drawing.Point(202, 184)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(206, 17)
        Me.LabelControl4.TabIndex = 38
        Me.LabelControl4.Text = "Confirmer nouveau mot de passe"
        '
        'BtPhoto
        '
        Me.BtPhoto.Image = Global.ClearProject.My.Resources.Resources.Edit_16x16
        Me.BtPhoto.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtPhoto.Location = New System.Drawing.Point(168, 30)
        Me.BtPhoto.Name = "BtPhoto"
        Me.BtPhoto.Size = New System.Drawing.Size(20, 20)
        Me.BtPhoto.TabIndex = 41
        Me.BtPhoto.ToolTip = "Modifier la photo"
        Me.BtPhoto.Visible = False
        '
        'TxtOldPass
        '
        Me.TxtOldPass.Location = New System.Drawing.Point(199, 110)
        Me.TxtOldPass.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TxtOldPass.MaxLength = 50
        Me.TxtOldPass.Name = "TxtOldPass"
        Me.TxtOldPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtOldPass.Size = New System.Drawing.Size(315, 21)
        Me.TxtOldPass.TabIndex = 42
        Me.TxtOldPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtOldPass.UseSystemPasswordChar = True
        '
        'TxtNewPass
        '
        Me.TxtNewPass.Location = New System.Drawing.Point(199, 158)
        Me.TxtNewPass.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TxtNewPass.MaxLength = 50
        Me.TxtNewPass.Name = "TxtNewPass"
        Me.TxtNewPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtNewPass.Size = New System.Drawing.Size(315, 21)
        Me.TxtNewPass.TabIndex = 43
        Me.TxtNewPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtNewPass.UseSystemPasswordChar = True
        '
        'TxtNewPass2
        '
        Me.TxtNewPass2.Location = New System.Drawing.Point(199, 203)
        Me.TxtNewPass2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TxtNewPass2.MaxLength = 50
        Me.TxtNewPass2.Name = "TxtNewPass2"
        Me.TxtNewPass2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtNewPass2.Size = New System.Drawing.Size(315, 21)
        Me.TxtNewPass2.TabIndex = 44
        Me.TxtNewPass2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtNewPass2.UseSystemPasswordChar = True
        '
        'MonMotDePasse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(520, 272)
        Me.Controls.Add(Me.TxtNewPass2)
        Me.Controls.Add(Me.TxtNewPass)
        Me.Controls.Add(Me.TxtOldPass)
        Me.Controls.Add(Me.BtPhoto)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.TxtLogin)
        Me.Controls.Add(Me.PhotoOperateur)
        Me.Controls.Add(Me.NomOper)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MonMotDePasse"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mon compte"
        CType(Me.PhotoOperateur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.TxtPhoto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtChemin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtLogin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents NomOper As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PhotoOperateur As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtEnregistrer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtLogin As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtPhoto As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtChemin As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPhoto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtOldPass As System.Windows.Forms.TextBox
    Friend WithEvents TxtNewPass As System.Windows.Forms.TextBox
    Friend WithEvents TxtNewPass2 As System.Windows.Forms.TextBox
End Class
