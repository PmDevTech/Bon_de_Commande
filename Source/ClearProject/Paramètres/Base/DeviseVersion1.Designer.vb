<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DeviseVersion1
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
        Me.BtAjout = New DevExpress.XtraEditors.SimpleButton()
        Me.BtRetour = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnEnregistrer = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.TreeListDevise = New DevExpress.XtraTreeList.TreeList()
        Me.CodeDevis = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.Type = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TauxUtilis = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.Codes = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.Noms = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.Tauxs = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.Dates = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.loaded = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.GridDevise = New DevExpress.XtraGrid.GridControl()
        Me.ViewDevise = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Code = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.Taux = New DevExpress.XtraEditors.TextEdit()
        Me.Nom = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtDate = New DevExpress.XtraEditors.DateEdit()
        Me.BtNouveau = New DevExpress.XtraEditors.SimpleButton()
        Me.CmbDevise = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ModifierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SupprimerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.TreeListDevise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridDevise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewDevise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Taux.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Nom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDate.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbDevise.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtAjout
        '
        Me.BtAjout.Image = Global.ClearProject.My.Resources.Resources.Add_32x32
        Me.BtAjout.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtAjout.Location = New System.Drawing.Point(507, 2)
        Me.BtAjout.Name = "BtAjout"
        Me.BtAjout.Size = New System.Drawing.Size(39, 39)
        Me.BtAjout.TabIndex = 9
        Me.BtAjout.ToolTip = "Ajouter"
        '
        'BtRetour
        '
        Me.BtRetour.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_32
        Me.BtRetour.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtRetour.Location = New System.Drawing.Point(550, 2)
        Me.BtRetour.Name = "BtRetour"
        Me.BtRetour.Size = New System.Drawing.Size(39, 39)
        Me.BtRetour.TabIndex = 6
        Me.BtRetour.ToolTip = "Retour"
        '
        'BtnEnregistrer
        '
        Me.BtnEnregistrer.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_32x32
        Me.BtnEnregistrer.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtnEnregistrer.Location = New System.Drawing.Point(592, 2)
        Me.BtnEnregistrer.Name = "BtnEnregistrer"
        Me.BtnEnregistrer.Size = New System.Drawing.Size(39, 39)
        Me.BtnEnregistrer.TabIndex = 5
        Me.BtnEnregistrer.ToolTip = "Enregistrer"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.TreeListDevise)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl1.Location = New System.Drawing.Point(0, 123)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(634, 155)
        Me.GroupControl1.TabIndex = 13
        Me.GroupControl1.Text = "Devise enregistré"
        '
        'TreeListDevise
        '
        Me.TreeListDevise.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.CodeDevis, Me.Type, Me.TauxUtilis, Me.Codes, Me.Noms, Me.Tauxs, Me.Dates, Me.loaded})
        Me.TreeListDevise.ContextMenuStrip = Me.ContextMenuStrip1
        Me.TreeListDevise.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeListDevise.Location = New System.Drawing.Point(2, 21)
        Me.TreeListDevise.Name = "TreeListDevise"
        Me.TreeListDevise.OptionsBehavior.Editable = False
        Me.TreeListDevise.Size = New System.Drawing.Size(630, 132)
        Me.TreeListDevise.TabIndex = 44
        '
        'CodeDevis
        '
        Me.CodeDevis.Caption = "CodeDevise"
        Me.CodeDevis.FieldName = "CodeDevise"
        Me.CodeDevis.Name = "CodeDevis"
        Me.CodeDevis.Width = 20
        '
        'Type
        '
        Me.Type.Caption = "Type"
        Me.Type.FieldName = "Type"
        Me.Type.Name = "Type"
        '
        'TauxUtilis
        '
        Me.TauxUtilis.Caption = "TauxUtilise"
        Me.TauxUtilis.FieldName = "TauxUtilise"
        Me.TauxUtilis.Name = "TauxUtilis"
        Me.TauxUtilis.Width = 50
        '
        'Codes
        '
        Me.Codes.Caption = "Code"
        Me.Codes.FieldName = "Code"
        Me.Codes.Name = "Codes"
        Me.Codes.Visible = True
        Me.Codes.VisibleIndex = 0
        Me.Codes.Width = 100
        '
        'Noms
        '
        Me.Noms.Caption = "Nom"
        Me.Noms.FieldName = "Nom"
        Me.Noms.Name = "Noms"
        Me.Noms.Visible = True
        Me.Noms.VisibleIndex = 1
        Me.Noms.Width = 200
        '
        'Tauxs
        '
        Me.Tauxs.AppearanceCell.Options.UseTextOptions = True
        Me.Tauxs.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.Tauxs.Caption = "Taux"
        Me.Tauxs.FieldName = "Taux"
        Me.Tauxs.Name = "Tauxs"
        Me.Tauxs.Visible = True
        Me.Tauxs.VisibleIndex = 2
        Me.Tauxs.Width = 100
        '
        'Dates
        '
        Me.Dates.AppearanceCell.Options.UseTextOptions = True
        Me.Dates.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.Dates.Caption = "Date"
        Me.Dates.FieldName = "Date"
        Me.Dates.Name = "Dates"
        Me.Dates.Visible = True
        Me.Dates.VisibleIndex = 3
        Me.Dates.Width = 100
        '
        'loaded
        '
        Me.loaded.Caption = "loaded"
        Me.loaded.FieldName = "loaded"
        Me.loaded.Name = "loaded"
        '
        'GridDevise
        '
        Me.GridDevise.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridDevise.Location = New System.Drawing.Point(182, 3)
        Me.GridDevise.MainView = Me.ViewDevise
        Me.GridDevise.Name = "GridDevise"
        Me.GridDevise.Size = New System.Drawing.Size(216, 35)
        Me.GridDevise.TabIndex = 96
        Me.GridDevise.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewDevise})
        Me.GridDevise.Visible = False
        '
        'ViewDevise
        '
        Me.ViewDevise.ActiveFilterEnabled = False
        Me.ViewDevise.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewDevise.Appearance.Row.Options.UseFont = True
        Me.ViewDevise.GridControl = Me.GridDevise
        Me.ViewDevise.Name = "ViewDevise"
        Me.ViewDevise.OptionsBehavior.Editable = False
        Me.ViewDevise.OptionsBehavior.ReadOnly = True
        Me.ViewDevise.OptionsCustomization.AllowColumnMoving = False
        Me.ViewDevise.OptionsCustomization.AllowFilter = False
        Me.ViewDevise.OptionsCustomization.AllowGroup = False
        Me.ViewDevise.OptionsCustomization.AllowSort = False
        Me.ViewDevise.OptionsFilter.AllowFilterEditor = False
        Me.ViewDevise.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewDevise.OptionsPrint.AutoWidth = False
        Me.ViewDevise.OptionsView.ColumnAutoWidth = False
        Me.ViewDevise.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewDevise.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewDevise.OptionsView.ShowGroupPanel = False
        Me.ViewDevise.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewDevise.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'Code
        '
        Me.Code.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Code.Location = New System.Drawing.Point(3, 46)
        Me.Code.Name = "Code"
        Me.Code.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Code.Properties.Appearance.Options.UseFont = True
        Me.Code.Properties.MaxLength = 50
        Me.Code.Properties.ReadOnly = True
        Me.Code.Size = New System.Drawing.Size(106, 24)
        Me.Code.TabIndex = 94
        Me.Code.Visible = False
        '
        'LabelControl14
        '
        Me.LabelControl14.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl14.Location = New System.Drawing.Point(113, 26)
        Me.LabelControl14.Name = "LabelControl14"
        Me.LabelControl14.Size = New System.Drawing.Size(31, 19)
        Me.LabelControl14.TabIndex = 95
        Me.LabelControl14.Text = "Nom"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(5, 26)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(34, 19)
        Me.LabelControl1.TabIndex = 98
        Me.LabelControl1.Text = "Code"
        '
        'Taux
        '
        Me.Taux.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Taux.Location = New System.Drawing.Point(409, 46)
        Me.Taux.Name = "Taux"
        Me.Taux.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Taux.Properties.Appearance.Options.UseFont = True
        Me.Taux.Properties.Appearance.Options.UseTextOptions = True
        Me.Taux.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.Taux.Properties.Mask.EditMask = "n"
        Me.Taux.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.Taux.Properties.MaxLength = 20
        Me.Taux.Properties.ReadOnly = True
        Me.Taux.Size = New System.Drawing.Size(88, 24)
        Me.Taux.TabIndex = 102
        '
        'Nom
        '
        Me.Nom.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Nom.Location = New System.Drawing.Point(114, 46)
        Me.Nom.Name = "Nom"
        Me.Nom.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Nom.Properties.Appearance.Options.UseFont = True
        Me.Nom.Properties.MaxLength = 50
        Me.Nom.Properties.ReadOnly = True
        Me.Nom.Size = New System.Drawing.Size(293, 24)
        Me.Nom.TabIndex = 101
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(502, 26)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(29, 19)
        Me.LabelControl3.TabIndex = 100
        Me.LabelControl3.Text = "Date"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(408, 26)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(30, 19)
        Me.LabelControl2.TabIndex = 99
        Me.LabelControl2.Text = "Taux"
        '
        'TxtDate
        '
        Me.TxtDate.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtDate.EditValue = Nothing
        Me.TxtDate.Location = New System.Drawing.Point(502, 46)
        Me.TxtDate.Name = "TxtDate"
        Me.TxtDate.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDate.Properties.Appearance.Options.UseFont = True
        Me.TxtDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TxtDate.Properties.Mask.EditMask = ""
        Me.TxtDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.TxtDate.Properties.MaxLength = 50
        Me.TxtDate.Properties.ReadOnly = True
        Me.TxtDate.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.TxtDate.Size = New System.Drawing.Size(124, 24)
        Me.TxtDate.TabIndex = 103
        '
        'BtNouveau
        '
        Me.BtNouveau.Image = Global.ClearProject.My.Resources.Resources.ajouter
        Me.BtNouveau.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtNouveau.Location = New System.Drawing.Point(462, 3)
        Me.BtNouveau.Name = "BtNouveau"
        Me.BtNouveau.Size = New System.Drawing.Size(39, 39)
        Me.BtNouveau.TabIndex = 99
        Me.BtNouveau.ToolTip = "Nouveau"
        '
        'CmbDevise
        '
        Me.CmbDevise.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmbDevise.Location = New System.Drawing.Point(3, 46)
        Me.CmbDevise.Name = "CmbDevise"
        Me.CmbDevise.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbDevise.Properties.Appearance.Options.UseFont = True
        Me.CmbDevise.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbDevise.Properties.MaxLength = 50
        Me.CmbDevise.Properties.ReadOnly = True
        Me.CmbDevise.Size = New System.Drawing.Size(106, 24)
        Me.CmbDevise.TabIndex = 104
        Me.CmbDevise.Visible = False
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.CmbDevise)
        Me.GroupControl2.Controls.Add(Me.Taux)
        Me.GroupControl2.Controls.Add(Me.Nom)
        Me.GroupControl2.Controls.Add(Me.TxtDate)
        Me.GroupControl2.Controls.Add(Me.LabelControl3)
        Me.GroupControl2.Controls.Add(Me.LabelControl14)
        Me.GroupControl2.Controls.Add(Me.LabelControl2)
        Me.GroupControl2.Controls.Add(Me.Code)
        Me.GroupControl2.Controls.Add(Me.LabelControl1)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl2.Location = New System.Drawing.Point(0, 44)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(634, 79)
        Me.GroupControl2.TabIndex = 97
        Me.GroupControl2.Text = "Nouvelle devise"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ModifierToolStripMenuItem, Me.SupprimerToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(181, 70)
        '
        'ModifierToolStripMenuItem
        '
        Me.ModifierToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Edit_16x16
        Me.ModifierToolStripMenuItem.Name = "ModifierToolStripMenuItem"
        Me.ModifierToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.ModifierToolStripMenuItem.Text = "Modifier"
        '
        'SupprimerToolStripMenuItem
        '
        Me.SupprimerToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Delete_16x16
        Me.SupprimerToolStripMenuItem.Name = "SupprimerToolStripMenuItem"
        Me.SupprimerToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.SupprimerToolStripMenuItem.Text = "Supprimer"
        '
        'DeviseVersion1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(634, 278)
        Me.Controls.Add(Me.GridDevise)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.BtNouveau)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.BtAjout)
        Me.Controls.Add(Me.BtRetour)
        Me.Controls.Add(Me.BtnEnregistrer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DeviseVersion1"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Devise"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.TreeListDevise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridDevise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewDevise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Taux.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Nom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDate.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbDevise.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtAjout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtRetour As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnEnregistrer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Code As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GridDevise As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewDevise As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Taux As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Nom As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents BtNouveau As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CmbDevise As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TreeListDevise As DevExpress.XtraTreeList.TreeList
    Friend WithEvents CodeDevis As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TauxUtilis As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents Codes As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents Noms As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents Tauxs As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents Dates As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents loaded As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents Type As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents SupprimerToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ModifierToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SupprimerToolStripMenuItem As ToolStripMenuItem
End Class
