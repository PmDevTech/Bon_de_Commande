<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GroupUtils
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
        Me.components = New System.ComponentModel.Container()
        Dim StyleFormatCondition1 As DevExpress.XtraTreeList.StyleFormatConditions.StyleFormatCondition = New DevExpress.XtraTreeList.StyleFormatConditions.StyleFormatCondition()
        Me.ColNomObjet = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtGroup = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ListeDossier = New DevExpress.XtraTreeList.TreeList()
        Me.ColItems = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.PanelControl4 = New DevExpress.XtraEditors.PanelControl()
        Me.CheckEdit3 = New DevExpress.XtraEditors.CheckEdit()
        Me.CheckEdit2 = New DevExpress.XtraEditors.CheckEdit()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.BtRetour = New DevExpress.XtraEditors.SimpleButton()
        Me.BtEnregistrer = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.GridGroup = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SupprimerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewGroup = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl5 = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.TxtGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ListeDossier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl4.SuspendLayout()
        CType(Me.CheckEdit3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CheckEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.GridGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.ViewGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl5.SuspendLayout()
        Me.SuspendLayout()
        '
        'ColNomObjet
        '
        Me.ColNomObjet.Caption = "Objets"
        Me.ColNomObjet.FieldName = "Lecture"
        Me.ColNomObjet.Name = "ColNomObjet"
        Me.ColNomObjet.OptionsColumn.AllowMove = False
        Me.ColNomObjet.OptionsColumn.AllowMoveToCustomizationForm = False
        Me.ColNomObjet.OptionsColumn.AllowSize = False
        Me.ColNomObjet.OptionsColumn.AllowSort = False
        Me.ColNomObjet.OptionsColumn.FixedWidth = True
        Me.ColNomObjet.OptionsColumn.ShowInCustomizationForm = False
        Me.ColNomObjet.OptionsFilter.AllowAutoFilter = False
        Me.ColNomObjet.OptionsFilter.AllowFilter = False
        Me.ColNomObjet.Width = 100
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.LabelControl4)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.TxtGroup)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(784, 61)
        Me.PanelControl1.TabIndex = 0
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Location = New System.Drawing.Point(628, 32)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(35, 15)
        Me.LabelControl4.TabIndex = 3
        Me.LabelControl4.Text = "<<<<<"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(240, 31)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(35, 15)
        Me.LabelControl3.TabIndex = 2
        Me.LabelControl3.Text = ">>>>>"
        '
        'TxtGroup
        '
        Me.TxtGroup.EditValue = ""
        Me.TxtGroup.Location = New System.Drawing.Point(281, 26)
        Me.TxtGroup.Name = "TxtGroup"
        Me.TxtGroup.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGroup.Properties.Appearance.Options.UseFont = True
        Me.TxtGroup.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtGroup.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtGroup.Properties.Mask.EditMask = "\p{L}+"
        Me.TxtGroup.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.TxtGroup.Size = New System.Drawing.Size(341, 26)
        Me.TxtGroup.TabIndex = 1
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(390, 9)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(118, 15)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Groupe d'Utilisateurs"
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl1.Controls.Add(Me.ListeDossier)
        Me.GroupControl1.Controls.Add(Me.PanelControl4)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(230, 2)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(552, 354)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "AUTORISATIONS"
        '
        'ListeDossier
        '
        Me.ListeDossier.Appearance.CustomizationFormHint.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListeDossier.Appearance.CustomizationFormHint.Options.UseFont = True
        Me.ListeDossier.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.ColItems, Me.ColNomObjet})
        Me.ListeDossier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListeDossier.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        StyleFormatCondition1.Appearance.BackColor = System.Drawing.Color.Transparent
        StyleFormatCondition1.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        StyleFormatCondition1.Appearance.Options.UseBackColor = True
        StyleFormatCondition1.Appearance.Options.UseFont = True
        StyleFormatCondition1.ApplyToRow = True
        StyleFormatCondition1.Column = Me.ColNomObjet
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition1.Value1 = ""
        StyleFormatCondition1.Value2 = ""
        Me.ListeDossier.FormatConditions.AddRange(New DevExpress.XtraTreeList.StyleFormatConditions.StyleFormatCondition() {StyleFormatCondition1})
        Me.ListeDossier.Location = New System.Drawing.Point(2, 23)
        Me.ListeDossier.Name = "ListeDossier"
        Me.ListeDossier.OptionsBehavior.AllowRecursiveNodeChecking = True
        Me.ListeDossier.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ListeDossier.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ListeDossier.OptionsView.ShowCheckBoxes = True
        Me.ListeDossier.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2})
        Me.ListeDossier.Size = New System.Drawing.Size(548, 305)
        Me.ListeDossier.TabIndex = 0
        '
        'ColItems
        '
        Me.ColItems.Caption = "Dossiers"
        Me.ColItems.FieldName = "Postes de travail"
        Me.ColItems.MinWidth = 32
        Me.ColItems.Name = "ColItems"
        Me.ColItems.OptionsColumn.AllowEdit = False
        Me.ColItems.OptionsColumn.AllowMove = False
        Me.ColItems.OptionsColumn.AllowMoveToCustomizationForm = False
        Me.ColItems.OptionsColumn.AllowSize = False
        Me.ColItems.OptionsColumn.AllowSort = False
        Me.ColItems.OptionsColumn.ReadOnly = True
        Me.ColItems.OptionsColumn.ShowInCustomizationForm = False
        Me.ColItems.OptionsFilter.AllowAutoFilter = False
        Me.ColItems.OptionsFilter.AllowFilter = False
        Me.ColItems.Visible = True
        Me.ColItems.VisibleIndex = 0
        Me.ColItems.Width = 414
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'PanelControl4
        '
        Me.PanelControl4.Controls.Add(Me.CheckEdit3)
        Me.PanelControl4.Controls.Add(Me.CheckEdit2)
        Me.PanelControl4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl4.Location = New System.Drawing.Point(2, 328)
        Me.PanelControl4.Name = "PanelControl4"
        Me.PanelControl4.Size = New System.Drawing.Size(548, 24)
        Me.PanelControl4.TabIndex = 1
        '
        'CheckEdit3
        '
        Me.CheckEdit3.Location = New System.Drawing.Point(2, 2)
        Me.CheckEdit3.Name = "CheckEdit3"
        Me.CheckEdit3.Properties.AllowGrayed = True
        Me.CheckEdit3.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckEdit3.Properties.Appearance.Options.UseFont = True
        Me.CheckEdit3.Properties.Caption = "Aucun accès"
        Me.CheckEdit3.Properties.ReadOnly = True
        Me.CheckEdit3.Size = New System.Drawing.Size(88, 20)
        Me.CheckEdit3.TabIndex = 4
        Me.CheckEdit3.Visible = False
        '
        'CheckEdit2
        '
        Me.CheckEdit2.EditValue = True
        Me.CheckEdit2.Location = New System.Drawing.Point(97, 2)
        Me.CheckEdit2.Name = "CheckEdit2"
        Me.CheckEdit2.Properties.AllowGrayed = True
        Me.CheckEdit2.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckEdit2.Properties.Appearance.Options.UseFont = True
        Me.CheckEdit2.Properties.Caption = "Accès total"
        Me.CheckEdit2.Properties.ReadOnly = True
        Me.CheckEdit2.Size = New System.Drawing.Size(120, 20)
        Me.CheckEdit2.TabIndex = 3
        Me.CheckEdit2.Visible = False
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.BtRetour)
        Me.PanelControl2.Controls.Add(Me.BtEnregistrer)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl2.Location = New System.Drawing.Point(0, 419)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(784, 42)
        Me.PanelControl2.TabIndex = 2
        '
        'BtRetour
        '
        Me.BtRetour.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtRetour.Appearance.Options.UseFont = True
        Me.BtRetour.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_161
        Me.BtRetour.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtRetour.Location = New System.Drawing.Point(3, 3)
        Me.BtRetour.Name = "BtRetour"
        Me.BtRetour.Size = New System.Drawing.Size(42, 36)
        Me.BtRetour.TabIndex = 1
        Me.BtRetour.Visible = False
        '
        'BtEnregistrer
        '
        Me.BtEnregistrer.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnregistrer.Appearance.Options.UseFont = True
        Me.BtEnregistrer.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_32x32
        Me.BtEnregistrer.Location = New System.Drawing.Point(393, 3)
        Me.BtEnregistrer.Name = "BtEnregistrer"
        Me.BtEnregistrer.Size = New System.Drawing.Size(165, 36)
        Me.BtEnregistrer.TabIndex = 0
        Me.BtEnregistrer.Text = "Enregistrer"
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.GroupControl1)
        Me.PanelControl3.Controls.Add(Me.GroupControl2)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl3.Location = New System.Drawing.Point(0, 61)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(784, 358)
        Me.PanelControl3.TabIndex = 3
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl2.Controls.Add(Me.GridGroup)
        Me.GroupControl2.Controls.Add(Me.PanelControl5)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupControl2.Location = New System.Drawing.Point(2, 2)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(228, 354)
        Me.GroupControl2.TabIndex = 2
        Me.GroupControl2.Text = "GROUPES"
        '
        'GridGroup
        '
        Me.GridGroup.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GridGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridGroup.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridGroup.Location = New System.Drawing.Point(2, 23)
        Me.GridGroup.MainView = Me.ViewGroup
        Me.GridGroup.Name = "GridGroup"
        Me.GridGroup.Size = New System.Drawing.Size(224, 305)
        Me.GridGroup.TabIndex = 12
        Me.GridGroup.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewGroup})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SupprimerToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(172, 26)
        '
        'SupprimerToolStripMenuItem
        '
        Me.SupprimerToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.SupprimerToolStripMenuItem.Name = "SupprimerToolStripMenuItem"
        Me.SupprimerToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.SupprimerToolStripMenuItem.Text = "Supprimer Groupe"
        '
        'ViewGroup
        '
        Me.ViewGroup.ActiveFilterEnabled = False
        Me.ViewGroup.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewGroup.Appearance.Row.Options.UseFont = True
        Me.ViewGroup.GridControl = Me.GridGroup
        Me.ViewGroup.Name = "ViewGroup"
        Me.ViewGroup.OptionsBehavior.Editable = False
        Me.ViewGroup.OptionsBehavior.ReadOnly = True
        Me.ViewGroup.OptionsCustomization.AllowColumnMoving = False
        Me.ViewGroup.OptionsCustomization.AllowFilter = False
        Me.ViewGroup.OptionsCustomization.AllowGroup = False
        Me.ViewGroup.OptionsCustomization.AllowSort = False
        Me.ViewGroup.OptionsFilter.AllowFilterEditor = False
        Me.ViewGroup.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewGroup.OptionsPrint.AutoWidth = False
        Me.ViewGroup.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewGroup.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewGroup.OptionsView.ColumnAutoWidth = False
        Me.ViewGroup.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewGroup.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewGroup.OptionsView.ShowGroupPanel = False
        Me.ViewGroup.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewGroup.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'PanelControl5
        '
        Me.PanelControl5.Controls.Add(Me.LabelControl2)
        Me.PanelControl5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl5.Location = New System.Drawing.Point(2, 328)
        Me.PanelControl5.Name = "PanelControl5"
        Me.PanelControl5.Size = New System.Drawing.Size(224, 24)
        Me.PanelControl5.TabIndex = 2
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(6, 5)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(130, 15)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Double-click pour modifier"
        '
        'GroupUtils
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 461)
        Me.Controls.Add(Me.PanelControl3)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "GroupUtils"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Groupes d'Utilisateurs"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.TxtGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ListeDossier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl4.ResumeLayout(False)
        CType(Me.CheckEdit3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CheckEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.GridGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.ViewGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl5.ResumeLayout(False)
        Me.PanelControl5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ListeDossier As DevExpress.XtraTreeList.TreeList
    Friend WithEvents ColItems As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents ColNomObjet As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridGroup As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewGroup As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TxtGroup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtEnregistrer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtRetour As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelControl4 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents CheckEdit3 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents CheckEdit2 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents PanelControl5 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SupprimerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
