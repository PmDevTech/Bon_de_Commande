<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RsmBailleurDevise
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
        Me.BtAnnuler = New System.Windows.Forms.Button()
        Me.BtOk = New System.Windows.Forms.Button()
        Me.TxtDevise = New System.Windows.Forms.TextBox()
        Me.CmbDevise = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtBailleur = New System.Windows.Forms.TextBox()
        Me.CmbBailleur = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'BtAnnuler
        '
        Me.BtAnnuler.Location = New System.Drawing.Point(278, 76)
        Me.BtAnnuler.Name = "BtAnnuler"
        Me.BtAnnuler.Size = New System.Drawing.Size(75, 24)
        Me.BtAnnuler.TabIndex = 15
        Me.BtAnnuler.Text = "Annuler"
        Me.BtAnnuler.UseVisualStyleBackColor = True
        '
        'BtOk
        '
        Me.BtOk.Location = New System.Drawing.Point(200, 76)
        Me.BtOk.Name = "BtOk"
        Me.BtOk.Size = New System.Drawing.Size(75, 24)
        Me.BtOk.TabIndex = 14
        Me.BtOk.Text = "OK"
        Me.BtOk.UseVisualStyleBackColor = True
        '
        'TxtDevise
        '
        Me.TxtDevise.Location = New System.Drawing.Point(135, 39)
        Me.TxtDevise.Name = "TxtDevise"
        Me.TxtDevise.Size = New System.Drawing.Size(218, 21)
        Me.TxtDevise.TabIndex = 13
        Me.TxtDevise.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'CmbDevise
        '
        Me.CmbDevise.FormattingEnabled = True
        Me.CmbDevise.Location = New System.Drawing.Point(60, 39)
        Me.CmbDevise.Name = "CmbDevise"
        Me.CmbDevise.Size = New System.Drawing.Size(69, 21)
        Me.CmbDevise.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Monnaie"
        '
        'TxtBailleur
        '
        Me.TxtBailleur.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtBailleur.Location = New System.Drawing.Point(135, 12)
        Me.TxtBailleur.Name = "TxtBailleur"
        Me.TxtBailleur.Size = New System.Drawing.Size(218, 21)
        Me.TxtBailleur.TabIndex = 10
        Me.TxtBailleur.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'CmbBailleur
        '
        Me.CmbBailleur.FormattingEnabled = True
        Me.CmbBailleur.Location = New System.Drawing.Point(60, 12)
        Me.CmbBailleur.Name = "CmbBailleur"
        Me.CmbBailleur.Size = New System.Drawing.Size(69, 21)
        Me.CmbBailleur.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Bailleur"
        '
        'RsmBailleurDevise
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(367, 110)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtAnnuler)
        Me.Controls.Add(Me.BtOk)
        Me.Controls.Add(Me.TxtDevise)
        Me.Controls.Add(Me.CmbDevise)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtBailleur)
        Me.Controls.Add(Me.CmbBailleur)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "RsmBailleurDevise"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BAILLEUR ET DEVISE"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtAnnuler As System.Windows.Forms.Button
    Friend WithEvents BtOk As System.Windows.Forms.Button
    Friend WithEvents TxtDevise As System.Windows.Forms.TextBox
    Friend WithEvents CmbDevise As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtBailleur As System.Windows.Forms.TextBox
    Friend WithEvents CmbBailleur As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
