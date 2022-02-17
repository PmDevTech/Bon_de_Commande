<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Nouvel_exercice
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
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.datedb = New DevExpress.XtraEditors.DateEdit()
        Me.datefin = New DevExpress.XtraEditors.DateEdit()
        Me.BtEnrg = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.txtLibelle = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.BtAnnul = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbAnnee = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.datedb.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.datedb.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.datefin.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.datefin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtLibelle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbAnnee.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(51, 100)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(54, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Date début"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(67, 130)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(38, 13)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Date fin"
        '
        'datedb
        '
        Me.datedb.EditValue = Nothing
        Me.datedb.Enabled = False
        Me.datedb.Location = New System.Drawing.Point(110, 98)
        Me.datedb.Name = "datedb"
        Me.datedb.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.datedb.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.datedb.Size = New System.Drawing.Size(205, 20)
        Me.datedb.TabIndex = 4
        '
        'datefin
        '
        Me.datefin.EditValue = Nothing
        Me.datefin.Enabled = False
        Me.datefin.Location = New System.Drawing.Point(110, 128)
        Me.datefin.Name = "datefin"
        Me.datefin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.datefin.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.datefin.Size = New System.Drawing.Size(205, 20)
        Me.datefin.TabIndex = 5
        '
        'BtEnrg
        '
        Me.BtEnrg.Image = Global.ClearProject.My.Resources.Resources.disque_editer_fichier_enregistrez_icone_4226_16
        Me.BtEnrg.Location = New System.Drawing.Point(214, 161)
        Me.BtEnrg.Name = "BtEnrg"
        Me.BtEnrg.Size = New System.Drawing.Size(84, 23)
        Me.BtEnrg.TabIndex = 9
        Me.BtEnrg.Text = "Enregistrer"
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Trebuchet MS", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.cmbAnnee)
        Me.GroupControl1.Controls.Add(Me.BtAnnul)
        Me.GroupControl1.Controls.Add(Me.BtEnrg)
        Me.GroupControl1.Controls.Add(Me.txtLibelle)
        Me.GroupControl1.Controls.Add(Me.datefin)
        Me.GroupControl1.Controls.Add(Me.LabelControl4)
        Me.GroupControl1.Controls.Add(Me.LabelControl3)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.datedb)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(349, 196)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "Créer un nouvel exercice"
        '
        'txtLibelle
        '
        Me.txtLibelle.Enabled = False
        Me.txtLibelle.Location = New System.Drawing.Point(110, 66)
        Me.txtLibelle.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.txtLibelle.Name = "txtLibelle"
        Me.txtLibelle.Properties.Mask.EditMask = "n0"
        Me.txtLibelle.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtLibelle.Size = New System.Drawing.Size(205, 20)
        Me.txtLibelle.TabIndex = 6
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(76, 68)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(29, 13)
        Me.LabelControl3.TabIndex = 0
        Me.LabelControl3.Text = "Libellé"
        '
        'BtAnnul
        '
        Me.BtAnnul.Image = Global.ClearProject.My.Resources.Resources.Delete_16x16
        Me.BtAnnul.Location = New System.Drawing.Point(68, 161)
        Me.BtAnnul.Name = "BtAnnul"
        Me.BtAnnul.Size = New System.Drawing.Size(84, 23)
        Me.BtAnnul.TabIndex = 10
        Me.BtAnnul.Text = "Annuler"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(20, 37)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(85, 13)
        Me.LabelControl4.TabIndex = 0
        Me.LabelControl4.Text = "Année Comptable"
        '
        'cmbAnnee
        '
        Me.cmbAnnee.Location = New System.Drawing.Point(110, 34)
        Me.cmbAnnee.Name = "cmbAnnee"
        Me.cmbAnnee.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbAnnee.Size = New System.Drawing.Size(205, 20)
        Me.cmbAnnee.TabIndex = 11
        '
        'Nouvel_exercice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(349, 196)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Nouvel_exercice"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Exercice comptable"
        CType(Me.datedb.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.datedb.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.datefin.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.datefin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtLibelle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbAnnee.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents datedb As DevExpress.XtraEditors.DateEdit
    Friend WithEvents datefin As DevExpress.XtraEditors.DateEdit
    Friend WithEvents BtEnrg As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents BtAnnul As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtLibelle As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbAnnee As DevExpress.XtraEditors.ComboBoxEdit
End Class
