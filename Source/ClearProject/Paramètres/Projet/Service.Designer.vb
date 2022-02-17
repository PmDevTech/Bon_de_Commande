<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Service
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.BtSupp = New DevExpress.XtraEditors.SimpleButton()
        Me.BtModif = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.CmbDivAdmin = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.BtAjoutZone = New DevExpress.XtraEditors.SimpleButton()
        Me.BtAnnuler = New DevExpress.XtraEditors.SimpleButton()
        Me.BtEnrg = New DevExpress.XtraEditors.SimpleButton()
        Me.CmbLocalisation = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.CmbServ = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtCodeServ = New DevExpress.XtraEditors.TextEdit()
        Me.TxtService = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GridService = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ModifierService = New System.Windows.Forms.ToolStripMenuItem()
        Me.SupprimerServiceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewService = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.CmbDivAdmin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbLocalisation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbServ.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCodeServ.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtService.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GridService, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.ViewService, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.BtSupp)
        Me.PanelControl1.Controls.Add(Me.BtModif)
        Me.PanelControl1.Controls.Add(Me.LabelControl5)
        Me.PanelControl1.Controls.Add(Me.CmbDivAdmin)
        Me.PanelControl1.Controls.Add(Me.BtAjoutZone)
        Me.PanelControl1.Controls.Add(Me.BtAnnuler)
        Me.PanelControl1.Controls.Add(Me.BtEnrg)
        Me.PanelControl1.Controls.Add(Me.CmbLocalisation)
        Me.PanelControl1.Controls.Add(Me.LabelControl4)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.CmbServ)
        Me.PanelControl1.Controls.Add(Me.LabelControl2)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Controls.Add(Me.TxtCodeServ)
        Me.PanelControl1.Controls.Add(Me.TxtService)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(583, 247)
        Me.PanelControl1.TabIndex = 0
        '
        'BtSupp
        '
        Me.BtSupp.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtSupp.Appearance.Options.UseFont = True
        Me.BtSupp.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.BtSupp.Location = New System.Drawing.Point(384, 194)
        Me.BtSupp.Name = "BtSupp"
        Me.BtSupp.Size = New System.Drawing.Size(129, 27)
        Me.BtSupp.TabIndex = 9
        Me.BtSupp.Text = "Supprimer"
        '
        'BtModif
        '
        Me.BtModif.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtModif.Appearance.Options.UseFont = True
        Me.BtModif.Image = Global.ClearProject.My.Resources.Resources.Edit_16x16
        Me.BtModif.Location = New System.Drawing.Point(240, 194)
        Me.BtModif.Name = "BtModif"
        Me.BtModif.Size = New System.Drawing.Size(129, 27)
        Me.BtModif.TabIndex = 8
        Me.BtModif.Text = "Modifier"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Location = New System.Drawing.Point(10, 6)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(129, 15)
        Me.LabelControl5.TabIndex = 13
        Me.LabelControl5.Text = "Division Administrative"
        '
        'CmbDivAdmin
        '
        Me.CmbDivAdmin.Location = New System.Drawing.Point(8, 23)
        Me.CmbDivAdmin.Name = "CmbDivAdmin"
        Me.CmbDivAdmin.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbDivAdmin.Properties.Appearance.Options.UseFont = True
        Me.CmbDivAdmin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbDivAdmin.Size = New System.Drawing.Size(568, 22)
        Me.CmbDivAdmin.TabIndex = 0
        '
        'BtAjoutZone
        '
        Me.BtAjoutZone.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtAjoutZone.Appearance.Options.UseFont = True
        Me.BtAjoutZone.Image = Global.ClearProject.My.Resources.Resources.Add_16x16
        Me.BtAjoutZone.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtAjoutZone.Location = New System.Drawing.Point(543, 158)
        Me.BtAjoutZone.Name = "BtAjoutZone"
        Me.BtAjoutZone.Size = New System.Drawing.Size(28, 22)
        Me.BtAjoutZone.TabIndex = 5
        Me.BtAjoutZone.ToolTip = "Ajouter une ville"
        '
        'BtAnnuler
        '
        Me.BtAnnuler.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtAnnuler.Appearance.Options.UseFont = True
        Me.BtAnnuler.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_32
        Me.BtAnnuler.Location = New System.Drawing.Point(10, 193)
        Me.BtAnnuler.Name = "BtAnnuler"
        Me.BtAnnuler.Size = New System.Drawing.Size(40, 28)
        Me.BtAnnuler.TabIndex = 6
        '
        'BtEnrg
        '
        Me.BtEnrg.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnrg.Appearance.Options.UseFont = True
        Me.BtEnrg.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.BtEnrg.Location = New System.Drawing.Point(99, 194)
        Me.BtEnrg.Name = "BtEnrg"
        Me.BtEnrg.Size = New System.Drawing.Size(126, 27)
        Me.BtEnrg.TabIndex = 7
        Me.BtEnrg.Text = "Enregistrer"
        '
        'CmbLocalisation
        '
        Me.CmbLocalisation.Location = New System.Drawing.Point(10, 158)
        Me.CmbLocalisation.Name = "CmbLocalisation"
        Me.CmbLocalisation.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbLocalisation.Properties.Appearance.Options.UseFont = True
        Me.CmbLocalisation.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbLocalisation.Size = New System.Drawing.Size(527, 22)
        Me.CmbLocalisation.TabIndex = 4
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Location = New System.Drawing.Point(10, 141)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(69, 15)
        Me.LabelControl4.TabIndex = 6
        Me.LabelControl4.Text = "Localisation"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(10, 96)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(58, 15)
        Me.LabelControl3.TabIndex = 5
        Me.LabelControl3.Text = "Dépend de"
        '
        'CmbServ
        '
        Me.CmbServ.Location = New System.Drawing.Point(8, 113)
        Me.CmbServ.Name = "CmbServ"
        Me.CmbServ.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbServ.Properties.Appearance.Options.UseFont = True
        Me.CmbServ.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbServ.Size = New System.Drawing.Size(568, 22)
        Me.CmbServ.TabIndex = 3
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(460, 51)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(87, 15)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Code du service"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(10, 51)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(83, 15)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "Nom du service"
        '
        'TxtCodeServ
        '
        Me.TxtCodeServ.Location = New System.Drawing.Point(458, 68)
        Me.TxtCodeServ.Name = "TxtCodeServ"
        Me.TxtCodeServ.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCodeServ.Properties.Appearance.Options.UseFont = True
        Me.TxtCodeServ.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtCodeServ.Properties.Mask.EditMask = "\p{L}+"
        Me.TxtCodeServ.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.TxtCodeServ.Size = New System.Drawing.Size(118, 22)
        Me.TxtCodeServ.TabIndex = 2
        '
        'TxtService
        '
        Me.TxtService.Location = New System.Drawing.Point(10, 68)
        Me.TxtService.Name = "TxtService"
        Me.TxtService.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtService.Properties.Appearance.Options.UseFont = True
        Me.TxtService.Properties.MaxLength = 160
        Me.TxtService.Size = New System.Drawing.Size(442, 22)
        Me.TxtService.TabIndex = 1
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl1.Controls.Add(Me.GridService)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 247)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(583, 199)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "Services"
        '
        'GridService
        '
        Me.GridService.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GridService.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridService.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridService.Location = New System.Drawing.Point(2, 23)
        Me.GridService.MainView = Me.ViewService
        Me.GridService.Name = "GridService"
        Me.GridService.Size = New System.Drawing.Size(579, 174)
        Me.GridService.TabIndex = 10
        Me.GridService.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewService})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ModifierService, Me.SupprimerServiceToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(130, 48)
        '
        'ModifierService
        '
        Me.ModifierService.Image = Global.ClearProject.My.Resources.Resources.Edit_16x16
        Me.ModifierService.Name = "ModifierService"
        Me.ModifierService.Size = New System.Drawing.Size(129, 22)
        Me.ModifierService.Text = "Modifier"
        Me.ModifierService.Visible = False
        '
        'SupprimerServiceToolStripMenuItem
        '
        Me.SupprimerServiceToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.SupprimerServiceToolStripMenuItem.Name = "SupprimerServiceToolStripMenuItem"
        Me.SupprimerServiceToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.SupprimerServiceToolStripMenuItem.Text = "Supprimer"
        '
        'ViewService
        '
        Me.ViewService.ActiveFilterEnabled = False
        Me.ViewService.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewService.Appearance.Row.Options.UseFont = True
        Me.ViewService.GridControl = Me.GridService
        Me.ViewService.Name = "ViewService"
        Me.ViewService.OptionsBehavior.Editable = False
        Me.ViewService.OptionsBehavior.ReadOnly = True
        Me.ViewService.OptionsCustomization.AllowColumnMoving = False
        Me.ViewService.OptionsCustomization.AllowFilter = False
        Me.ViewService.OptionsCustomization.AllowGroup = False
        Me.ViewService.OptionsCustomization.AllowSort = False
        Me.ViewService.OptionsFilter.AllowFilterEditor = False
        Me.ViewService.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewService.OptionsPrint.AutoWidth = False
        Me.ViewService.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewService.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewService.OptionsView.ColumnAutoWidth = False
        Me.ViewService.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewService.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewService.OptionsView.ShowGroupPanel = False
        Me.ViewService.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewService.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'Service
        '
        Me.AcceptButton = Me.BtEnrg
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(583, 446)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Service"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Service"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.CmbDivAdmin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbLocalisation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbServ.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCodeServ.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtService.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.GridService, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.ViewService, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtEnrg As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CmbLocalisation As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CmbServ As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtCodeServ As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtService As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridService As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewService As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BtAnnuler As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ModifierService As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BtAjoutZone As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CmbDivAdmin As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents SupprimerServiceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BtSupp As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtModif As DevExpress.XtraEditors.SimpleButton
End Class
