<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RessourcesRSF
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
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.CmbBailleur = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbCondition = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.CmbNumComptable = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtLibelle = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.GridCompte = New DevExpress.XtraGrid.GridControl()
        Me.ViewCompte = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BtEnregistrer = New DevExpress.XtraEditors.SimpleButton()
        Me.btModifier = New DevExpress.XtraEditors.SimpleButton()
        Me.btDel = New DevExpress.XtraEditors.SimpleButton()
        Me.btRetour = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.CmbBailleur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCondition.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbNumComptable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLibelle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.GridCompte, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewCompte, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.CmbBailleur)
        Me.GroupControl1.Controls.Add(Me.LabelControl10)
        Me.GroupControl1.Controls.Add(Me.cmbCondition)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.LabelControl7)
        Me.GroupControl1.Controls.Add(Me.CmbNumComptable)
        Me.GroupControl1.Controls.Add(Me.txtLibelle)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(424, 128)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Nouveau Compte"
        '
        'CmbBailleur
        '
        Me.CmbBailleur.Location = New System.Drawing.Point(64, 26)
        Me.CmbBailleur.Name = "CmbBailleur"
        Me.CmbBailleur.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbBailleur.Properties.Appearance.Options.UseFont = True
        Me.CmbBailleur.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbBailleur.Size = New System.Drawing.Size(350, 22)
        Me.CmbBailleur.TabIndex = 5
        '
        'LabelControl10
        '
        Me.LabelControl10.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl10.Location = New System.Drawing.Point(12, 29)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(46, 15)
        Me.LabelControl10.TabIndex = 21
        Me.LabelControl10.Text = "Bailleur"
        '
        'cmbCondition
        '
        Me.cmbCondition.Location = New System.Drawing.Point(310, 58)
        Me.cmbCondition.Name = "cmbCondition"
        Me.cmbCondition.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCondition.Properties.Appearance.Options.UseFont = True
        Me.cmbCondition.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCondition.Properties.Items.AddRange(New Object() {"Débit", "Crédit"})
        Me.cmbCondition.Size = New System.Drawing.Size(104, 22)
        Me.cmbCondition.TabIndex = 15
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(16, 95)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(38, 15)
        Me.LabelControl1.TabIndex = 13
        Me.LabelControl1.Text = "Libellé"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl7.Location = New System.Drawing.Point(15, 62)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(42, 15)
        Me.LabelControl7.TabIndex = 13
        Me.LabelControl7.Text = "Compte"
        '
        'CmbNumComptable
        '
        Me.CmbNumComptable.Location = New System.Drawing.Point(64, 58)
        Me.CmbNumComptable.Name = "CmbNumComptable"
        Me.CmbNumComptable.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbNumComptable.Properties.Appearance.Options.UseFont = True
        Me.CmbNumComptable.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbNumComptable.Size = New System.Drawing.Size(240, 22)
        Me.CmbNumComptable.TabIndex = 10
        '
        'txtLibelle
        '
        Me.txtLibelle.Location = New System.Drawing.Point(64, 92)
        Me.txtLibelle.Name = "txtLibelle"
        Me.txtLibelle.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLibelle.Properties.Appearance.Options.UseFont = True
        Me.txtLibelle.Size = New System.Drawing.Size(350, 22)
        Me.txtLibelle.TabIndex = 20
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.GridCompte)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl2.Location = New System.Drawing.Point(0, 168)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(424, 255)
        Me.GroupControl2.TabIndex = 2
        Me.GroupControl2.Text = "Comptes enregistrés"
        '
        'GridCompte
        '
        Me.GridCompte.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridCompte.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridCompte.Location = New System.Drawing.Point(2, 23)
        Me.GridCompte.MainView = Me.ViewCompte
        Me.GridCompte.Name = "GridCompte"
        Me.GridCompte.Size = New System.Drawing.Size(420, 230)
        Me.GridCompte.TabIndex = 7
        Me.GridCompte.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewCompte})
        '
        'ViewCompte
        '
        Me.ViewCompte.ActiveFilterEnabled = False
        Me.ViewCompte.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewCompte.Appearance.Row.Options.UseFont = True
        Me.ViewCompte.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
        Me.ViewCompte.GridControl = Me.GridCompte
        Me.ViewCompte.Name = "ViewCompte"
        Me.ViewCompte.OptionsBehavior.Editable = False
        Me.ViewCompte.OptionsBehavior.ReadOnly = True
        Me.ViewCompte.OptionsCustomization.AllowColumnMoving = False
        Me.ViewCompte.OptionsCustomization.AllowFilter = False
        Me.ViewCompte.OptionsCustomization.AllowGroup = False
        Me.ViewCompte.OptionsCustomization.AllowSort = False
        Me.ViewCompte.OptionsFilter.AllowFilterEditor = False
        Me.ViewCompte.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewCompte.OptionsPrint.AutoWidth = False
        Me.ViewCompte.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewCompte.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewCompte.OptionsView.ColumnAutoWidth = False
        Me.ViewCompte.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewCompte.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewCompte.OptionsView.ShowGroupPanel = False
        Me.ViewCompte.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.[True]
        Me.ViewCompte.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewCompte.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'BtEnregistrer
        '
        Me.BtEnregistrer.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnregistrer.Appearance.Options.UseFont = True
        Me.BtEnregistrer.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.BtEnregistrer.Location = New System.Drawing.Point(112, 134)
        Me.BtEnregistrer.Name = "BtEnregistrer"
        Me.BtEnregistrer.Size = New System.Drawing.Size(99, 28)
        Me.BtEnregistrer.TabIndex = 30
        Me.BtEnregistrer.Text = "Enregistrer"
        '
        'btModifier
        '
        Me.btModifier.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btModifier.Appearance.Options.UseFont = True
        Me.btModifier.Image = Global.ClearProject.My.Resources.Resources.Edit_16x16
        Me.btModifier.Location = New System.Drawing.Point(215, 134)
        Me.btModifier.Name = "btModifier"
        Me.btModifier.Size = New System.Drawing.Size(91, 28)
        Me.btModifier.TabIndex = 35
        Me.btModifier.Text = "Modifier"
        '
        'btDel
        '
        Me.btDel.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btDel.Appearance.Options.UseFont = True
        Me.btDel.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.btDel.Location = New System.Drawing.Point(310, 134)
        Me.btDel.Name = "btDel"
        Me.btDel.Size = New System.Drawing.Size(91, 28)
        Me.btDel.TabIndex = 40
        Me.btDel.Text = "Supprimer"
        '
        'btRetour
        '
        Me.btRetour.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btRetour.Appearance.Options.UseFont = True
        Me.btRetour.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_161
        Me.btRetour.Location = New System.Drawing.Point(15, 134)
        Me.btRetour.Name = "btRetour"
        Me.btRetour.Size = New System.Drawing.Size(91, 28)
        Me.btRetour.TabIndex = 25
        Me.btRetour.Text = "Annuler"
        '
        'RessourcesRSF
        '
        Me.AcceptButton = Me.BtEnregistrer
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(424, 423)
        Me.Controls.Add(Me.btRetour)
        Me.Controls.Add(Me.btDel)
        Me.Controls.Add(Me.btModifier)
        Me.Controls.Add(Me.BtEnregistrer)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RessourcesRSF"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Les ressources du RSF"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.CmbBailleur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCondition.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbNumComptable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLibelle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.GridCompte, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewCompte, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents CmbBailleur As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CmbNumComptable As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtLibelle As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents BtEnregistrer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridCompte As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewCompte As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btModifier As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btDel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmbCondition As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btRetour As DevExpress.XtraEditors.SimpleButton
End Class
