<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DiagDelai
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
        Me.NbreD = New System.Windows.Forms.NumericUpDown
        Me.UniteD = New System.Windows.Forms.ComboBox
        Me.ValideD = New System.Windows.Forms.Button
        Me.SuppD = New System.Windows.Forms.Button
        Me.RdDelaiDAO = New System.Windows.Forms.RadioButton
        Me.RdDelaiNormal = New System.Windows.Forms.RadioButton
        CType(Me.NbreD, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NbreD
        '
        Me.NbreD.Location = New System.Drawing.Point(82, 30)
        Me.NbreD.Name = "NbreD"
        Me.NbreD.Size = New System.Drawing.Size(56, 20)
        Me.NbreD.TabIndex = 1
        Me.NbreD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'UniteD
        '
        Me.UniteD.FormattingEnabled = True
        Me.UniteD.Items.AddRange(New Object() {"Jours", "Semaines", "Mois"})
        Me.UniteD.Location = New System.Drawing.Point(142, 30)
        Me.UniteD.Name = "UniteD"
        Me.UniteD.Size = New System.Drawing.Size(123, 21)
        Me.UniteD.TabIndex = 2
        '
        'ValideD
        '
        Me.ValideD.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ValideD.Location = New System.Drawing.Point(183, 93)
        Me.ValideD.Name = "ValideD"
        Me.ValideD.Size = New System.Drawing.Size(88, 21)
        Me.ValideD.TabIndex = 3
        Me.ValideD.Text = "Valider"
        Me.ValideD.UseVisualStyleBackColor = True
        '
        'SuppD
        '
        Me.SuppD.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SuppD.Location = New System.Drawing.Point(88, 93)
        Me.SuppD.Name = "SuppD"
        Me.SuppD.Size = New System.Drawing.Size(92, 21)
        Me.SuppD.TabIndex = 4
        Me.SuppD.Text = "Supprimer délai"
        Me.SuppD.UseVisualStyleBackColor = True
        Me.SuppD.Visible = False
        '
        'RdDelaiDAO
        '
        Me.RdDelaiDAO.AutoSize = True
        Me.RdDelaiDAO.Location = New System.Drawing.Point(30, 58)
        Me.RdDelaiDAO.Name = "RdDelaiDAO"
        Me.RdDelaiDAO.Size = New System.Drawing.Size(236, 17)
        Me.RdDelaiDAO.TabIndex = 5
        Me.RdDelaiDAO.TabStop = True
        Me.RdDelaiDAO.Text = "Délai d'exécution du Dossier d'Appel d'Offres"
        Me.RdDelaiDAO.UseVisualStyleBackColor = True
        '
        'RdDelaiNormal
        '
        Me.RdDelaiNormal.AutoSize = True
        Me.RdDelaiNormal.Location = New System.Drawing.Point(30, 33)
        Me.RdDelaiNormal.Name = "RdDelaiNormal"
        Me.RdDelaiNormal.Size = New System.Drawing.Size(49, 17)
        Me.RdDelaiNormal.TabIndex = 6
        Me.RdDelaiNormal.TabStop = True
        Me.RdDelaiNormal.Text = "Délai"
        Me.RdDelaiNormal.UseVisualStyleBackColor = True
        '
        'DiagDelai
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(303, 119)
        Me.Controls.Add(Me.RdDelaiNormal)
        Me.Controls.Add(Me.RdDelaiDAO)
        Me.Controls.Add(Me.SuppD)
        Me.Controls.Add(Me.ValideD)
        Me.Controls.Add(Me.UniteD)
        Me.Controls.Add(Me.NbreD)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DiagDelai"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Délai "
        Me.TopMost = True
        CType(Me.NbreD, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents NbreD As System.Windows.Forms.NumericUpDown
    Friend WithEvents UniteD As System.Windows.Forms.ComboBox
    Friend WithEvents ValideD As System.Windows.Forms.Button
    Friend WithEvents SuppD As System.Windows.Forms.Button
    Friend WithEvents RdDelaiDAO As System.Windows.Forms.RadioButton
    Friend WithEvents RdDelaiNormal As System.Windows.Forms.RadioButton
End Class
