<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LicenceUtilisateur
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LicenceUtilisateur))
        Me.TxtError = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopierLeMessageDerreurToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btClose = New DevExpress.XtraEditors.SimpleButton()
        Me.btNext = New DevExpress.XtraEditors.SimpleButton()
        Me.txtKey = New DevExpress.XtraEditors.TextEdit()
        Me.Souligne = New System.Windows.Forms.Panel()
        Me.LblFinGlob = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.txtKey.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtError
        '
        Me.TxtError.BackColor = System.Drawing.Color.Red
        Me.TxtError.ContextMenuStrip = Me.ContextMenuStrip1
        Me.TxtError.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtError.ForeColor = System.Drawing.Color.White
        Me.TxtError.Location = New System.Drawing.Point(4, 61)
        Me.TxtError.Name = "TxtError"
        Me.TxtError.Size = New System.Drawing.Size(405, 25)
        Me.TxtError.TabIndex = 42
        Me.TxtError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.TxtError.Visible = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopierLeMessageDerreurToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(215, 26)
        '
        'CopierLeMessageDerreurToolStripMenuItem
        '
        Me.CopierLeMessageDerreurToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Close_16x16
        Me.CopierLeMessageDerreurToolStripMenuItem.Name = "CopierLeMessageDerreurToolStripMenuItem"
        Me.CopierLeMessageDerreurToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.CopierLeMessageDerreurToolStripMenuItem.Text = "Copier le message d'erreur"
        '
        'btClose
        '
        Me.btClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btClose.Location = New System.Drawing.Point(95, 92)
        Me.btClose.Name = "btClose"
        Me.btClose.Size = New System.Drawing.Size(101, 26)
        Me.btClose.TabIndex = 40
        Me.btClose.Text = "&Annuler"
        '
        'btNext
        '
        Me.btNext.Location = New System.Drawing.Point(216, 92)
        Me.btNext.Name = "btNext"
        Me.btNext.Size = New System.Drawing.Size(101, 26)
        Me.btNext.TabIndex = 41
        Me.btNext.Text = "&Vérifier"
        '
        'txtKey
        '
        Me.txtKey.Location = New System.Drawing.Point(129, 33)
        Me.txtKey.Name = "txtKey"
        Me.txtKey.Properties.AutoHeight = False
        Me.txtKey.Properties.Mask.EditMask = "\p{Lu}\p{Lu}\p{Lu}\p{Lu}\p{Lu}-\p{Lu}\p{Lu}\p{Lu}\p{Lu}\p{Lu}-\p{Lu}\p{Lu}\p{Lu}\" &
    "p{Lu}\p{Lu}-\p{Lu}\p{Lu}\p{Lu}\p{Lu}\p{Lu}"
        Me.txtKey.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.txtKey.Properties.MaxLength = 60
        Me.txtKey.Size = New System.Drawing.Size(262, 20)
        Me.txtKey.TabIndex = 34
        '
        'Souligne
        '
        Me.Souligne.BackColor = System.Drawing.Color.DarkBlue
        Me.Souligne.Location = New System.Drawing.Point(1, 23)
        Me.Souligne.Name = "Souligne"
        Me.Souligne.Size = New System.Drawing.Size(412, 1)
        Me.Souligne.TabIndex = 39
        Me.Souligne.Visible = False
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
        Me.LblFinGlob.Size = New System.Drawing.Size(138, 14)
        Me.LblFinGlob.TabIndex = 38
        Me.LblFinGlob.Text = "Vérificateur de licences"
        Me.LblFinGlob.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(39, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 15)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "Clé de licence"
        '
        'LicenceUtilisateur
        '
        Me.AcceptButton = Me.btNext
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch
        Me.BackgroundImageStore = Global.ClearProject.My.Resources.Resources.NvFond
        Me.CancelButton = Me.btClose
        Me.ClientSize = New System.Drawing.Size(414, 128)
        Me.ControlBox = False
        Me.Controls.Add(Me.TxtError)
        Me.Controls.Add(Me.btClose)
        Me.Controls.Add(Me.btNext)
        Me.Controls.Add(Me.txtKey)
        Me.Controls.Add(Me.Souligne)
        Me.Controls.Add(Me.LblFinGlob)
        Me.Controls.Add(Me.Label2)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LicenceUtilisateur"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Information"
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.txtKey.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtError As Label
    Friend WithEvents btClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btNext As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtKey As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Souligne As Panel
    Friend WithEvents LblFinGlob As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents CopierLeMessageDerreurToolStripMenuItem As ToolStripMenuItem
End Class
