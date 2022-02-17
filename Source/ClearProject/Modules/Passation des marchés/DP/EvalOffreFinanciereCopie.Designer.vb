<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EvalOffreFinanciereCopie
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.TxtNom = New DevExpress.XtraEditors.TextEdit()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.BtQuitter = New DevExpress.XtraEditors.SimpleButton()
        Me.BtEnregOffre = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.CmbDevise = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtMontPropose = New DevExpress.XtraEditors.TextEdit()
        Me.TxtMontLettre = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelMonnaie = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtTaux = New DevExpress.XtraEditors.TextEdit()
        Me.TxtMontOffre = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.TxtNom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.CmbDevise.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtMontPropose.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtMontLettre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTaux.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtMontOffre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.TxtNom)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(685, 34)
        Me.PanelControl1.TabIndex = 0
        '
        'TxtNom
        '
        Me.TxtNom.Dock = System.Windows.Forms.DockStyle.Top
        Me.TxtNom.Location = New System.Drawing.Point(2, 2)
        Me.TxtNom.Name = "TxtNom"
        Me.TxtNom.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNom.Properties.Appearance.Options.UseFont = True
        Me.TxtNom.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtNom.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtNom.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.TxtNom.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtNom.Properties.ReadOnly = True
        Me.TxtNom.Size = New System.Drawing.Size(681, 30)
        Me.TxtNom.TabIndex = 0
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.BtQuitter)
        Me.PanelControl2.Controls.Add(Me.BtEnregOffre)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl2.Location = New System.Drawing.Point(0, 164)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(685, 40)
        Me.PanelControl2.TabIndex = 1
        '
        'BtQuitter
        '
        Me.BtQuitter.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtQuitter.Appearance.Options.UseFont = True
        Me.BtQuitter.Dock = System.Windows.Forms.DockStyle.Left
        Me.BtQuitter.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_32
        Me.BtQuitter.Location = New System.Drawing.Point(2, 2)
        Me.BtQuitter.Name = "BtQuitter"
        Me.BtQuitter.Size = New System.Drawing.Size(128, 36)
        Me.BtQuitter.TabIndex = 1
        Me.BtQuitter.Text = "Quitter"
        '
        'BtEnregOffre
        '
        Me.BtEnregOffre.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnregOffre.Appearance.Options.UseFont = True
        Me.BtEnregOffre.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtEnregOffre.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_32x32
        Me.BtEnregOffre.Location = New System.Drawing.Point(555, 2)
        Me.BtEnregOffre.Name = "BtEnregOffre"
        Me.BtEnregOffre.Size = New System.Drawing.Size(128, 36)
        Me.BtEnregOffre.TabIndex = 0
        Me.BtEnregOffre.Text = "Enregistrer"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(14, 40)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(59, 19)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "Monnaie"
        '
        'CmbDevise
        '
        Me.CmbDevise.Location = New System.Drawing.Point(12, 58)
        Me.CmbDevise.Name = "CmbDevise"
        Me.CmbDevise.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbDevise.Properties.Appearance.Options.UseFont = True
        Me.CmbDevise.Properties.Appearance.Options.UseTextOptions = True
        Me.CmbDevise.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CmbDevise.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbDevise.Size = New System.Drawing.Size(102, 22)
        Me.CmbDevise.TabIndex = 3
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(132, 40)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(113, 19)
        Me.LabelControl2.TabIndex = 4
        Me.LabelControl2.Text = "Montant proposé"
        '
        'TxtMontPropose
        '
        Me.TxtMontPropose.Enabled = False
        Me.TxtMontPropose.Location = New System.Drawing.Point(130, 58)
        Me.TxtMontPropose.Name = "TxtMontPropose"
        Me.TxtMontPropose.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMontPropose.Properties.Appearance.Options.UseFont = True
        Me.TxtMontPropose.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtMontPropose.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.TxtMontPropose.Size = New System.Drawing.Size(159, 22)
        Me.TxtMontPropose.TabIndex = 5
        '
        'TxtMontLettre
        '
        Me.TxtMontLettre.Location = New System.Drawing.Point(12, 86)
        Me.TxtMontLettre.Name = "TxtMontLettre"
        Me.TxtMontLettre.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMontLettre.Properties.Appearance.Options.UseFont = True
        Me.TxtMontLettre.Properties.ReadOnly = True
        Me.TxtMontLettre.Size = New System.Drawing.Size(661, 61)
        Me.TxtMontLettre.TabIndex = 6
        '
        'LabelMonnaie
        '
        Me.LabelMonnaie.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelMonnaie.Location = New System.Drawing.Point(392, 58)
        Me.LabelMonnaie.Name = "LabelMonnaie"
        Me.LabelMonnaie.Size = New System.Drawing.Size(44, 19)
        Me.LabelMonnaie.TabIndex = 7
        Me.LabelMonnaie.Text = "FCFA"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Location = New System.Drawing.Point(312, 41)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(36, 19)
        Me.LabelControl4.TabIndex = 8
        Me.LabelControl4.Text = "Taux"
        '
        'TxtTaux
        '
        Me.TxtTaux.Location = New System.Drawing.Point(312, 58)
        Me.TxtTaux.Name = "TxtTaux"
        Me.TxtTaux.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTaux.Properties.Appearance.Options.UseFont = True
        Me.TxtTaux.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtTaux.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.TxtTaux.Properties.ReadOnly = True
        Me.TxtTaux.Size = New System.Drawing.Size(74, 22)
        Me.TxtTaux.TabIndex = 9
        '
        'TxtMontOffre
        '
        Me.TxtMontOffre.Location = New System.Drawing.Point(500, 58)
        Me.TxtMontOffre.Name = "TxtMontOffre"
        Me.TxtMontOffre.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMontOffre.Properties.Appearance.Options.UseFont = True
        Me.TxtMontOffre.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtMontOffre.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.TxtMontOffre.Properties.ReadOnly = True
        Me.TxtMontOffre.Size = New System.Drawing.Size(173, 22)
        Me.TxtMontOffre.TabIndex = 11
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Location = New System.Drawing.Point(502, 41)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(120, 19)
        Me.LabelControl5.TabIndex = 10
        Me.LabelControl5.Text = "Montant de l'offre"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6.Location = New System.Drawing.Point(295, 59)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(12, 18)
        Me.LabelControl6.TabIndex = 12
        Me.LabelControl6.Text = "X"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl7.Location = New System.Drawing.Point(453, 58)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(17, 19)
        Me.LabelControl7.TabIndex = 13
        Me.LabelControl7.Text = "  ="
        '
        'EvalOffreFinanciereCopie
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(685, 204)
        Me.ControlBox = False
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.TxtMontOffre)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.TxtTaux)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelMonnaie)
        Me.Controls.Add(Me.TxtMontPropose)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.CmbDevise)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.TxtMontLettre)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "EvalOffreFinanciereCopie"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Offre Financière"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.TxtNom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.CmbDevise.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtMontPropose.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtMontLettre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTaux.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtMontOffre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents TxtNom As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtEnregOffre As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtQuitter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CmbDevise As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtMontPropose As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtMontLettre As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelMonnaie As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtTaux As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtMontOffre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
End Class
