<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExceptionRevue
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
        Me.RdAucuneExcep = New System.Windows.Forms.RadioButton
        Me.RdMarcheExcep = New System.Windows.Forms.RadioButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtNbMarche = New System.Windows.Forms.TextBox
        Me.GbExcept = New System.Windows.Forms.GroupBox
        Me.GbExecMarche = New System.Windows.Forms.GroupBox
        Me.ChkMarcheAgence = New System.Windows.Forms.CheckBox
        Me.ChkMarcheCC = New System.Windows.Forms.CheckBox
        Me.BtValider = New System.Windows.Forms.Button
        Me.BtAnnuler = New System.Windows.Forms.Button
        Me.GbExcept.SuspendLayout()
        Me.GbExecMarche.SuspendLayout()
        Me.SuspendLayout()
        '
        'RdAucuneExcep
        '
        Me.RdAucuneExcep.AutoSize = True
        Me.RdAucuneExcep.Location = New System.Drawing.Point(16, 30)
        Me.RdAucuneExcep.Name = "RdAucuneExcep"
        Me.RdAucuneExcep.Size = New System.Drawing.Size(134, 17)
        Me.RdAucuneExcep.TabIndex = 6
        Me.RdAucuneExcep.TabStop = True
        Me.RdAucuneExcep.Text = "AUCUNE EXCEPTION"
        Me.RdAucuneExcep.UseVisualStyleBackColor = True
        '
        'RdMarcheExcep
        '
        Me.RdMarcheExcep.AutoSize = True
        Me.RdMarcheExcep.Location = New System.Drawing.Point(16, 57)
        Me.RdMarcheExcep.Name = "RdMarcheExcep"
        Me.RdMarcheExcep.Size = New System.Drawing.Size(45, 17)
        Me.RdMarcheExcep.TabIndex = 7
        Me.RdMarcheExcep.TabStop = True
        Me.RdMarcheExcep.Text = "LES"
        Me.RdMarcheExcep.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(106, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "PREMIERS MARCHES"
        '
        'TxtNbMarche
        '
        Me.TxtNbMarche.Enabled = False
        Me.TxtNbMarche.Location = New System.Drawing.Point(63, 55)
        Me.TxtNbMarche.Name = "TxtNbMarche"
        Me.TxtNbMarche.Size = New System.Drawing.Size(42, 20)
        Me.TxtNbMarche.TabIndex = 10
        Me.TxtNbMarche.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GbExcept
        '
        Me.GbExcept.Controls.Add(Me.RdAucuneExcep)
        Me.GbExcept.Controls.Add(Me.TxtNbMarche)
        Me.GbExcept.Controls.Add(Me.RdMarcheExcep)
        Me.GbExcept.Controls.Add(Me.Label1)
        Me.GbExcept.Location = New System.Drawing.Point(9, 48)
        Me.GbExcept.Name = "GbExcept"
        Me.GbExcept.Size = New System.Drawing.Size(245, 102)
        Me.GbExcept.TabIndex = 11
        Me.GbExcept.TabStop = False
        Me.GbExcept.Text = "Exceptions"
        '
        'GbExecMarche
        '
        Me.GbExecMarche.Controls.Add(Me.ChkMarcheAgence)
        Me.GbExecMarche.Controls.Add(Me.ChkMarcheCC)
        Me.GbExecMarche.Enabled = False
        Me.GbExecMarche.Location = New System.Drawing.Point(267, 48)
        Me.GbExecMarche.Name = "GbExecMarche"
        Me.GbExecMarche.Size = New System.Drawing.Size(240, 101)
        Me.GbExecMarche.TabIndex = 12
        Me.GbExecMarche.TabStop = False
        Me.GbExecMarche.Text = "Marchés concernés"
        '
        'ChkMarcheAgence
        '
        Me.ChkMarcheAgence.AutoSize = True
        Me.ChkMarcheAgence.Location = New System.Drawing.Point(15, 59)
        Me.ChkMarcheAgence.Name = "ChkMarcheAgence"
        Me.ChkMarcheAgence.Size = New System.Drawing.Size(188, 17)
        Me.ChkMarcheAgence.TabIndex = 1
        Me.ChkMarcheAgence.Text = "Marchés des agences d'exécution"
        Me.ChkMarcheAgence.UseVisualStyleBackColor = True
        '
        'ChkMarcheCC
        '
        Me.ChkMarcheCC.AutoSize = True
        Me.ChkMarcheCC.Location = New System.Drawing.Point(15, 30)
        Me.ChkMarcheCC.Name = "ChkMarcheCC"
        Me.ChkMarcheCC.Size = New System.Drawing.Size(194, 17)
        Me.ChkMarcheCC.TabIndex = 0
        Me.ChkMarcheCC.Text = "Marchés du Comité de Coordination"
        Me.ChkMarcheCC.UseVisualStyleBackColor = True
        '
        'BtValider
        '
        Me.BtValider.Location = New System.Drawing.Point(425, 157)
        Me.BtValider.Name = "BtValider"
        Me.BtValider.Size = New System.Drawing.Size(82, 24)
        Me.BtValider.TabIndex = 13
        Me.BtValider.Text = "Valider"
        Me.BtValider.UseVisualStyleBackColor = True
        '
        'BtAnnuler
        '
        Me.BtAnnuler.Location = New System.Drawing.Point(337, 157)
        Me.BtAnnuler.Name = "BtAnnuler"
        Me.BtAnnuler.Size = New System.Drawing.Size(82, 24)
        Me.BtAnnuler.TabIndex = 14
        Me.BtAnnuler.Text = "Annuler"
        Me.BtAnnuler.UseVisualStyleBackColor = True
        '
        'ExceptionRevue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(520, 195)
        Me.Controls.Add(Me.BtAnnuler)
        Me.Controls.Add(Me.BtValider)
        Me.Controls.Add(Me.GbExecMarche)
        Me.Controls.Add(Me.GbExcept)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ExceptionRevue"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EXCEPTION SUR REVUE"
        Me.GbExcept.ResumeLayout(False)
        Me.GbExcept.PerformLayout()
        Me.GbExecMarche.ResumeLayout(False)
        Me.GbExecMarche.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RdAucuneExcep As System.Windows.Forms.RadioButton
    Friend WithEvents RdMarcheExcep As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtNbMarche As System.Windows.Forms.TextBox
    Friend WithEvents GbExcept As System.Windows.Forms.GroupBox
    Friend WithEvents GbExecMarche As System.Windows.Forms.GroupBox
    Friend WithEvents ChkMarcheCC As System.Windows.Forms.CheckBox
    Friend WithEvents ChkMarcheAgence As System.Windows.Forms.CheckBox
    Friend WithEvents BtValider As System.Windows.Forms.Button
    Friend WithEvents BtAnnuler As System.Windows.Forms.Button
End Class
