<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Fonction
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
        Me.chkChef = New System.Windows.Forms.CheckBox()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.CmbDivAdmin = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.BtAvantage = New DevExpress.XtraEditors.SimpleButton()
        Me.BtSupp = New DevExpress.XtraEditors.SimpleButton()
        Me.BtModif = New DevExpress.XtraEditors.SimpleButton()
        Me.LblTypeFonction = New DevExpress.XtraEditors.LabelControl()
        Me.CmbService = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.BtAjoutService = New DevExpress.XtraEditors.SimpleButton()
        Me.BtAnnuler = New DevExpress.XtraEditors.SimpleButton()
        Me.BtEnrg = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.CmbSup = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtCodeFonction = New DevExpress.XtraEditors.TextEdit()
        Me.TxtFonction = New DevExpress.XtraEditors.TextEdit()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip()
        Me.ModifierFonction = New System.Windows.Forms.ToolStripMenuItem()
        Me.SupprimerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GridFonction = New DevExpress.XtraGrid.GridControl()
        Me.ViewFonction = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.CmbDivAdmin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbService.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbSup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCodeFonction.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFonction.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.GridFonction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewFonction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.chkChef)
        Me.PanelControl1.Controls.Add(Me.LabelControl5)
        Me.PanelControl1.Controls.Add(Me.CmbDivAdmin)
        Me.PanelControl1.Controls.Add(Me.BtAvantage)
        Me.PanelControl1.Controls.Add(Me.BtSupp)
        Me.PanelControl1.Controls.Add(Me.BtModif)
        Me.PanelControl1.Controls.Add(Me.LblTypeFonction)
        Me.PanelControl1.Controls.Add(Me.CmbService)
        Me.PanelControl1.Controls.Add(Me.BtAjoutService)
        Me.PanelControl1.Controls.Add(Me.BtAnnuler)
        Me.PanelControl1.Controls.Add(Me.BtEnrg)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.CmbSup)
        Me.PanelControl1.Controls.Add(Me.LabelControl2)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Controls.Add(Me.TxtCodeFonction)
        Me.PanelControl1.Controls.Add(Me.TxtFonction)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(583, 210)
        Me.PanelControl1.TabIndex = 2
        '
        'chkChef
        '
        Me.chkChef.AutoSize = True
        Me.chkChef.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkChef.Location = New System.Drawing.Point(9, 136)
        Me.chkChef.Name = "chkChef"
        Me.chkChef.Size = New System.Drawing.Size(245, 20)
        Me.chkChef.TabIndex = 16
        Me.chkChef.Text = "Définir comme responsable du service"
        Me.chkChef.UseVisualStyleBackColor = True
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Location = New System.Drawing.Point(9, 4)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(129, 15)
        Me.LabelControl5.TabIndex = 19
        Me.LabelControl5.Text = "Division Administrative"
        '
        'CmbDivAdmin
        '
        Me.CmbDivAdmin.Location = New System.Drawing.Point(7, 21)
        Me.CmbDivAdmin.Name = "CmbDivAdmin"
        Me.CmbDivAdmin.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbDivAdmin.Properties.Appearance.Options.UseFont = True
        Me.CmbDivAdmin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbDivAdmin.Size = New System.Drawing.Size(568, 22)
        Me.CmbDivAdmin.TabIndex = 0
        '
        'BtAvantage
        '
        Me.BtAvantage.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtAvantage.Appearance.Options.UseFont = True
        Me.BtAvantage.Image = Global.ClearProject.My.Resources.Resources.sac_tresorerie_argent_icone_5930_16
        Me.BtAvantage.Location = New System.Drawing.Point(397, 174)
        Me.BtAvantage.Name = "BtAvantage"
        Me.BtAvantage.Size = New System.Drawing.Size(96, 24)
        Me.BtAvantage.TabIndex = 36
        Me.BtAvantage.Text = "Avantage"
        '
        'BtSupp
        '
        Me.BtSupp.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtSupp.Appearance.Options.UseFont = True
        Me.BtSupp.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.BtSupp.Location = New System.Drawing.Point(294, 174)
        Me.BtSupp.Name = "BtSupp"
        Me.BtSupp.Size = New System.Drawing.Size(96, 24)
        Me.BtSupp.TabIndex = 32
        Me.BtSupp.Text = "Supprimer"
        '
        'BtModif
        '
        Me.BtModif.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtModif.Appearance.Options.UseFont = True
        Me.BtModif.Image = Global.ClearProject.My.Resources.Resources.Edit_16x16
        Me.BtModif.Location = New System.Drawing.Point(191, 174)
        Me.BtModif.Name = "BtModif"
        Me.BtModif.Size = New System.Drawing.Size(96, 24)
        Me.BtModif.TabIndex = 28
        Me.BtModif.Text = "Modifier"
        '
        'LblTypeFonction
        '
        Me.LblTypeFonction.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTypeFonction.Location = New System.Drawing.Point(5, 45)
        Me.LblTypeFonction.Name = "LblTypeFonction"
        Me.LblTypeFonction.Size = New System.Drawing.Size(40, 15)
        Me.LblTypeFonction.TabIndex = 13
        Me.LblTypeFonction.Text = "Service"
        '
        'CmbService
        '
        Me.CmbService.Location = New System.Drawing.Point(5, 62)
        Me.CmbService.Name = "CmbService"
        Me.CmbService.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbService.Properties.Appearance.Options.UseFont = True
        Me.CmbService.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbService.Size = New System.Drawing.Size(534, 22)
        Me.CmbService.TabIndex = 4
        '
        'BtAjoutService
        '
        Me.BtAjoutService.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtAjoutService.Appearance.Options.UseFont = True
        Me.BtAjoutService.Image = Global.ClearProject.My.Resources.Resources.Add_16x16
        Me.BtAjoutService.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtAjoutService.Location = New System.Drawing.Point(545, 62)
        Me.BtAjoutService.Name = "BtAjoutService"
        Me.BtAjoutService.Size = New System.Drawing.Size(28, 22)
        Me.BtAjoutService.TabIndex = 100
        Me.BtAjoutService.ToolTip = "Ajouter un service"
        '
        'BtAnnuler
        '
        Me.BtAnnuler.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtAnnuler.Appearance.Options.UseFont = True
        Me.BtAnnuler.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_32
        Me.BtAnnuler.Location = New System.Drawing.Point(5, 172)
        Me.BtAnnuler.Name = "BtAnnuler"
        Me.BtAnnuler.Size = New System.Drawing.Size(40, 30)
        Me.BtAnnuler.TabIndex = 20
        '
        'BtEnrg
        '
        Me.BtEnrg.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnrg.Appearance.Options.UseFont = True
        Me.BtEnrg.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.BtEnrg.Location = New System.Drawing.Point(91, 174)
        Me.BtEnrg.Name = "BtEnrg"
        Me.BtEnrg.Size = New System.Drawing.Size(93, 24)
        Me.BtEnrg.TabIndex = 24
        Me.BtEnrg.Text = "Enregistrer"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(7, 163)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(128, 15)
        Me.LabelControl3.TabIndex = 5
        Me.LabelControl3.Text = "Supérieur hiérarchique"
        Me.LabelControl3.Visible = False
        '
        'CmbSup
        '
        Me.CmbSup.Location = New System.Drawing.Point(5, 180)
        Me.CmbSup.Name = "CmbSup"
        Me.CmbSup.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbSup.Properties.Appearance.Options.UseFont = True
        Me.CmbSup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbSup.Size = New System.Drawing.Size(568, 22)
        Me.CmbSup.TabIndex = 5
        Me.CmbSup.Visible = False
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(457, 90)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(107, 15)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Code de la fonction"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(7, 90)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(116, 15)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "Libellé de la fonction"
        '
        'TxtCodeFonction
        '
        Me.TxtCodeFonction.Location = New System.Drawing.Point(455, 107)
        Me.TxtCodeFonction.Name = "TxtCodeFonction"
        Me.TxtCodeFonction.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCodeFonction.Properties.Appearance.Options.UseFont = True
        Me.TxtCodeFonction.Size = New System.Drawing.Size(118, 22)
        Me.TxtCodeFonction.TabIndex = 12
        '
        'TxtFonction
        '
        Me.TxtFonction.Location = New System.Drawing.Point(5, 107)
        Me.TxtFonction.Name = "TxtFonction"
        Me.TxtFonction.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFonction.Properties.Appearance.Options.UseFont = True
        Me.TxtFonction.Properties.MaxLength = 200
        Me.TxtFonction.Size = New System.Drawing.Size(444, 22)
        Me.TxtFonction.TabIndex = 8
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ModifierFonction, Me.SupprimerToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(130, 48)
        '
        'ModifierFonction
        '
        Me.ModifierFonction.Image = Global.ClearProject.My.Resources.Resources.Edit_16x16
        Me.ModifierFonction.Name = "ModifierFonction"
        Me.ModifierFonction.Size = New System.Drawing.Size(129, 22)
        Me.ModifierFonction.Text = "Modifier"
        Me.ModifierFonction.Visible = False
        '
        'SupprimerToolStripMenuItem
        '
        Me.SupprimerToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.SupprimerToolStripMenuItem.Name = "SupprimerToolStripMenuItem"
        Me.SupprimerToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.SupprimerToolStripMenuItem.Text = "Supprimer"
        '
        'GridFonction
        '
        Me.GridFonction.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GridFonction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridFonction.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridFonction.Location = New System.Drawing.Point(2, 23)
        Me.GridFonction.MainView = Me.ViewFonction
        Me.GridFonction.Name = "GridFonction"
        Me.GridFonction.Size = New System.Drawing.Size(579, 237)
        Me.GridFonction.TabIndex = 0
        Me.GridFonction.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewFonction})
        '
        'ViewFonction
        '
        Me.ViewFonction.ActiveFilterEnabled = False
        Me.ViewFonction.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewFonction.Appearance.Row.Options.UseFont = True
        Me.ViewFonction.GridControl = Me.GridFonction
        Me.ViewFonction.Name = "ViewFonction"
        Me.ViewFonction.OptionsBehavior.Editable = False
        Me.ViewFonction.OptionsBehavior.ReadOnly = True
        Me.ViewFonction.OptionsCustomization.AllowColumnMoving = False
        Me.ViewFonction.OptionsCustomization.AllowFilter = False
        Me.ViewFonction.OptionsCustomization.AllowGroup = False
        Me.ViewFonction.OptionsCustomization.AllowSort = False
        Me.ViewFonction.OptionsFilter.AllowFilterEditor = False
        Me.ViewFonction.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewFonction.OptionsPrint.AutoWidth = False
        Me.ViewFonction.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewFonction.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewFonction.OptionsView.ColumnAutoWidth = False
        Me.ViewFonction.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewFonction.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewFonction.OptionsView.ShowGroupPanel = False
        Me.ViewFonction.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewFonction.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl1.Controls.Add(Me.GridFonction)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 210)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(583, 262)
        Me.GroupControl1.TabIndex = 40
        Me.GroupControl1.Text = "Fonctions"
        '
        'Fonction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(583, 472)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Fonction"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Fonction"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.CmbDivAdmin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbService.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbSup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCodeFonction.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFonction.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.GridFonction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewFonction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LblTypeFonction As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CmbService As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents BtAjoutService As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtAnnuler As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtEnrg As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CmbSup As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtCodeFonction As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtFonction As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ModifierFonction As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GridFonction As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewFonction As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents BtSupp As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtModif As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SupprimerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CmbDivAdmin As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents BtAvantage As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents chkChef As CheckBox
End Class
