<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Cloture_Exercice
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
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.txtDateDebut = New DevExpress.XtraEditors.DateEdit()
        Me.txtDateFin = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.combexer = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.rdDefinitif = New System.Windows.Forms.RadioButton()
        Me.rdProvisoire = New System.Windows.Forms.RadioButton()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.BtEnrg = New DevExpress.XtraEditors.SimpleButton()
        Me.BtAnnul = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtDateDebut.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDateDebut.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDateFin.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDateFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.combexer.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Trebuchet MS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.txtDateDebut)
        Me.GroupControl1.Controls.Add(Me.txtDateFin)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.combexer)
        Me.GroupControl1.Controls.Add(Me.rdDefinitif)
        Me.GroupControl1.Controls.Add(Me.rdProvisoire)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(431, 144)
        Me.GroupControl1.TabIndex = 15
        Me.GroupControl1.Text = "Informations"
        '
        'txtDateDebut
        '
        Me.txtDateDebut.EditValue = Nothing
        Me.txtDateDebut.Enabled = False
        Me.txtDateDebut.Location = New System.Drawing.Point(26, 106)
        Me.txtDateDebut.Name = "txtDateDebut"
        Me.txtDateDebut.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtDateDebut.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtDateDebut.Size = New System.Drawing.Size(174, 20)
        Me.txtDateDebut.TabIndex = 17
        '
        'txtDateFin
        '
        Me.txtDateFin.EditValue = Nothing
        Me.txtDateFin.Enabled = False
        Me.txtDateFin.Location = New System.Drawing.Point(235, 106)
        Me.txtDateFin.Name = "txtDateFin"
        Me.txtDateFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtDateFin.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtDateFin.Size = New System.Drawing.Size(174, 20)
        Me.txtDateFin.TabIndex = 17
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(25, 67)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(47, 16)
        Me.LabelControl1.TabIndex = 3
        Me.LabelControl1.Text = "Exercice"
        '
        'combexer
        '
        Me.combexer.Location = New System.Drawing.Point(86, 63)
        Me.combexer.Name = "combexer"
        Me.combexer.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.combexer.Size = New System.Drawing.Size(322, 20)
        Me.combexer.TabIndex = 2
        '
        'rdDefinitif
        '
        Me.rdDefinitif.AutoSize = True
        Me.rdDefinitif.BackColor = System.Drawing.Color.Transparent
        Me.rdDefinitif.Font = New System.Drawing.Font("Tempus Sans ITC", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdDefinitif.Location = New System.Drawing.Point(242, 29)
        Me.rdDefinitif.Name = "rdDefinitif"
        Me.rdDefinitif.Size = New System.Drawing.Size(138, 21)
        Me.rdDefinitif.TabIndex = 1
        Me.rdDefinitif.Text = "Clôture définitive"
        Me.rdDefinitif.UseVisualStyleBackColor = False
        '
        'rdProvisoire
        '
        Me.rdProvisoire.AutoSize = True
        Me.rdProvisoire.BackColor = System.Drawing.Color.Transparent
        Me.rdProvisoire.Checked = True
        Me.rdProvisoire.Font = New System.Drawing.Font("Tempus Sans ITC", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdProvisoire.Location = New System.Drawing.Point(25, 29)
        Me.rdProvisoire.Name = "rdProvisoire"
        Me.rdProvisoire.Size = New System.Drawing.Size(141, 21)
        Me.rdProvisoire.TabIndex = 0
        Me.rdProvisoire.TabStop = True
        Me.rdProvisoire.Text = "Clôture provisoire"
        Me.rdProvisoire.UseVisualStyleBackColor = False
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.BtEnrg)
        Me.PanelControl2.Controls.Add(Me.BtAnnul)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl2.Location = New System.Drawing.Point(0, 143)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(431, 36)
        Me.PanelControl2.TabIndex = 16
        '
        'BtEnrg
        '
        Me.BtEnrg.Appearance.Font = New System.Drawing.Font("Trebuchet MS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnrg.Appearance.Options.UseFont = True
        Me.BtEnrg.Image = Global.ClearProject.My.Resources.Resources.disque_editer_fichier_enregistrez_icone_4226_16
        Me.BtEnrg.Location = New System.Drawing.Point(216, 7)
        Me.BtEnrg.Name = "BtEnrg"
        Me.BtEnrg.Size = New System.Drawing.Size(153, 23)
        Me.BtEnrg.TabIndex = 10
        Me.BtEnrg.Text = "Enregistrer"
        '
        'BtAnnul
        '
        Me.BtAnnul.Appearance.Font = New System.Drawing.Font("Trebuchet MS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtAnnul.Appearance.Options.UseFont = True
        Me.BtAnnul.Image = Global.ClearProject.My.Resources.Resources.Delete_16x16
        Me.BtAnnul.Location = New System.Drawing.Point(67, 7)
        Me.BtAnnul.Name = "BtAnnul"
        Me.BtAnnul.Size = New System.Drawing.Size(143, 23)
        Me.BtAnnul.TabIndex = 11
        Me.BtAnnul.Text = "Fermer"
        '
        'Cloture_Exercice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(431, 179)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Cloture_Exercice"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Clôture des exercices"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtDateDebut.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDateDebut.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDateFin.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDateFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.combexer.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents rdDefinitif As System.Windows.Forms.RadioButton
    Friend WithEvents rdProvisoire As System.Windows.Forms.RadioButton
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtEnrg As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtAnnul As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents combexer As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtDateDebut As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtDateFin As DevExpress.XtraEditors.DateEdit
End Class
