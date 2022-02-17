<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<Global.System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726")> _
Partial Class CodeAcces
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Souligne = New System.Windows.Forms.Panel()
        Me.LblBailleurs = New System.Windows.Forms.Label()
        Me.LblFinGlob = New System.Windows.Forms.Label()
        Me.TxtNomLogo = New System.Windows.Forms.TextBox()
        Me.TxtNomComplet = New System.Windows.Forms.TextBox()
        Me.LibelleProjet = New System.Windows.Forms.RichTextBox()
        Me.ComboProjet = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.OK = New System.Windows.Forms.Button()
        Me.PasswordTextBox = New System.Windows.Forms.TextBox()
        Me.UsernameTextBox = New System.Windows.Forms.TextBox()
        Me.PbLogoProjet = New System.Windows.Forms.PictureBox()
        Me.TxtInactif = New System.Windows.Forms.TextBox()
        Me.chkPassword = New System.Windows.Forms.CheckBox()
        CType(Me.PbLogoProjet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Souligne
        '
        Me.Souligne.BackColor = System.Drawing.Color.DarkBlue
        Me.Souligne.Location = New System.Drawing.Point(0, 16)
        Me.Souligne.Name = "Souligne"
        Me.Souligne.Size = New System.Drawing.Size(412, 1)
        Me.Souligne.TabIndex = 27
        Me.Souligne.Visible = False
        '
        'LblBailleurs
        '
        Me.LblBailleurs.AutoSize = True
        Me.LblBailleurs.BackColor = System.Drawing.Color.Transparent
        Me.LblBailleurs.Dock = System.Windows.Forms.DockStyle.Right
        Me.LblBailleurs.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBailleurs.ForeColor = System.Drawing.Color.DarkBlue
        Me.LblBailleurs.Location = New System.Drawing.Point(305, 0)
        Me.LblBailleurs.Name = "LblBailleurs"
        Me.LblBailleurs.Size = New System.Drawing.Size(107, 14)
        Me.LblBailleurs.TabIndex = 26
        Me.LblBailleurs.Text = "Bailleurs de fonds"
        Me.LblBailleurs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblBailleurs.Visible = False
        '
        'LblFinGlob
        '
        Me.LblFinGlob.AutoSize = True
        Me.LblFinGlob.BackColor = System.Drawing.Color.Transparent
        Me.LblFinGlob.Dock = System.Windows.Forms.DockStyle.Left
        Me.LblFinGlob.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFinGlob.ForeColor = System.Drawing.Color.DarkBlue
        Me.LblFinGlob.Location = New System.Drawing.Point(0, 0)
        Me.LblFinGlob.Name = "LblFinGlob"
        Me.LblFinGlob.Size = New System.Drawing.Size(114, 14)
        Me.LblFinGlob.TabIndex = 25
        Me.LblFinGlob.Text = "Financement global"
        Me.LblFinGlob.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblFinGlob.Visible = False
        '
        'TxtNomLogo
        '
        Me.TxtNomLogo.BackColor = System.Drawing.Color.AliceBlue
        Me.TxtNomLogo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtNomLogo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtNomLogo.Enabled = False
        Me.TxtNomLogo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNomLogo.Location = New System.Drawing.Point(302, 150)
        Me.TxtNomLogo.MaxLength = 20
        Me.TxtNomLogo.Name = "TxtNomLogo"
        Me.TxtNomLogo.ReadOnly = True
        Me.TxtNomLogo.Size = New System.Drawing.Size(100, 15)
        Me.TxtNomLogo.TabIndex = 24
        Me.TxtNomLogo.Text = "AAAA"
        Me.TxtNomLogo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtNomLogo.Visible = False
        '
        'TxtNomComplet
        '
        Me.TxtNomComplet.BackColor = System.Drawing.Color.White
        Me.TxtNomComplet.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtNomComplet.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNomComplet.Location = New System.Drawing.Point(19, 111)
        Me.TxtNomComplet.Multiline = True
        Me.TxtNomComplet.Name = "TxtNomComplet"
        Me.TxtNomComplet.ReadOnly = True
        Me.TxtNomComplet.Size = New System.Drawing.Size(275, 33)
        Me.TxtNomComplet.TabIndex = 22
        Me.TxtNomComplet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtNomComplet.Visible = False
        '
        'LibelleProjet
        '
        Me.LibelleProjet.BackColor = System.Drawing.Color.White
        Me.LibelleProjet.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LibelleProjet.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.LibelleProjet.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LibelleProjet.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LibelleProjet.Location = New System.Drawing.Point(19, 55)
        Me.LibelleProjet.MaxLength = 2500
        Me.LibelleProjet.Name = "LibelleProjet"
        Me.LibelleProjet.ReadOnly = True
        Me.LibelleProjet.Size = New System.Drawing.Size(275, 31)
        Me.LibelleProjet.TabIndex = 16
        Me.LibelleProjet.Text = ""
        Me.LibelleProjet.Visible = False
        '
        'ComboProjet
        '
        Me.ComboProjet.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ComboProjet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboProjet.FormattingEnabled = True
        Me.ComboProjet.Location = New System.Drawing.Point(66, 33)
        Me.ComboProjet.Name = "ComboProjet"
        Me.ComboProjet.Size = New System.Drawing.Size(228, 21)
        Me.ComboProjet.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(22, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 15)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Projet"
        '
        'Cancel
        '
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(213, 193)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(82, 23)
        Me.Cancel.TabIndex = 20
        Me.Cancel.Text = "&Annuler"
        '
        'OK
        '
        Me.OK.Enabled = False
        Me.OK.Location = New System.Drawing.Point(118, 193)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(91, 23)
        Me.OK.TabIndex = 19
        Me.OK.Text = "&OK"
        '
        'PasswordTextBox
        '
        Me.PasswordTextBox.BackColor = System.Drawing.Color.White
        Me.PasswordTextBox.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PasswordTextBox.ForeColor = System.Drawing.Color.Blue
        Me.PasswordTextBox.Location = New System.Drawing.Point(19, 145)
        Me.PasswordTextBox.Name = "PasswordTextBox"
        Me.PasswordTextBox.ReadOnly = True
        Me.PasswordTextBox.Size = New System.Drawing.Size(275, 22)
        Me.PasswordTextBox.TabIndex = 18
        Me.PasswordTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.PasswordTextBox.Visible = False
        '
        'UsernameTextBox
        '
        Me.UsernameTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.UsernameTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsernameTextBox.ForeColor = System.Drawing.Color.Blue
        Me.UsernameTextBox.Location = New System.Drawing.Point(19, 88)
        Me.UsernameTextBox.Name = "UsernameTextBox"
        Me.UsernameTextBox.Size = New System.Drawing.Size(275, 20)
        Me.UsernameTextBox.TabIndex = 17
        Me.UsernameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.UsernameTextBox.Visible = False
        '
        'PbLogoProjet
        '
        Me.PbLogoProjet.BackColor = System.Drawing.Color.Transparent
        Me.PbLogoProjet.Location = New System.Drawing.Point(302, 33)
        Me.PbLogoProjet.Name = "PbLogoProjet"
        Me.PbLogoProjet.Size = New System.Drawing.Size(100, 115)
        Me.PbLogoProjet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PbLogoProjet.TabIndex = 23
        Me.PbLogoProjet.TabStop = False
        '
        'TxtInactif
        '
        Me.TxtInactif.BackColor = System.Drawing.Color.Red
        Me.TxtInactif.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtInactif.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtInactif.ForeColor = System.Drawing.Color.Black
        Me.TxtInactif.Location = New System.Drawing.Point(19, 146)
        Me.TxtInactif.Name = "TxtInactif"
        Me.TxtInactif.ReadOnly = True
        Me.TxtInactif.Size = New System.Drawing.Size(275, 20)
        Me.TxtInactif.TabIndex = 28
        Me.TxtInactif.Text = "COMPTE NON ACTIF"
        Me.TxtInactif.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtInactif.Visible = False
        '
        'chkPassword
        '
        Me.chkPassword.AutoSize = True
        Me.chkPassword.BackColor = System.Drawing.Color.Transparent
        Me.chkPassword.Location = New System.Drawing.Point(19, 169)
        Me.chkPassword.Name = "chkPassword"
        Me.chkPassword.Size = New System.Drawing.Size(139, 17)
        Me.chkPassword.TabIndex = 29
        Me.chkPassword.Text = "Afficher le mot de passe"
        Me.chkPassword.UseVisualStyleBackColor = False
        Me.chkPassword.Visible = False
        '
        'CodeAcces
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.ClearProject.My.Resources.Resources.NvFond
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(412, 226)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkPassword)
        Me.Controls.Add(Me.PasswordTextBox)
        Me.Controls.Add(Me.TxtInactif)
        Me.Controls.Add(Me.Souligne)
        Me.Controls.Add(Me.LblBailleurs)
        Me.Controls.Add(Me.LblFinGlob)
        Me.Controls.Add(Me.TxtNomLogo)
        Me.Controls.Add(Me.PbLogoProjet)
        Me.Controls.Add(Me.TxtNomComplet)
        Me.Controls.Add(Me.LibelleProjet)
        Me.Controls.Add(Me.ComboProjet)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.UsernameTextBox)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CodeAcces"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        CType(Me.PbLogoProjet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Souligne As System.Windows.Forms.Panel
    Friend WithEvents LblBailleurs As System.Windows.Forms.Label
    Friend WithEvents LblFinGlob As System.Windows.Forms.Label
    Friend WithEvents TxtNomLogo As System.Windows.Forms.TextBox
    Friend WithEvents PbLogoProjet As System.Windows.Forms.PictureBox
    Friend WithEvents TxtNomComplet As System.Windows.Forms.TextBox
    Friend WithEvents LibelleProjet As System.Windows.Forms.RichTextBox
    Friend WithEvents ComboProjet As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents PasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents UsernameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TxtInactif As System.Windows.Forms.TextBox
    Friend WithEvents chkPassword As CheckBox
End Class
