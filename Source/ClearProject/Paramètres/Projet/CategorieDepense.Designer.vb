<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CategorieDepense
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
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.BtActualiser = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtCodeBailleur = New DevExpress.XtraEditors.TextEdit()
        Me.TxtMontConv = New DevExpress.XtraEditors.TextEdit()
        Me.CmbBailleur = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.CmbConvention = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.TxtCodeCategorie = New DevExpress.XtraEditors.TextEdit()
        Me.GridCategorie = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SupprimerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewCategorie = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.TxtCategorie = New DevExpress.XtraEditors.TextEdit()
        Me.TxtMontCateg = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPourcent = New DevExpress.XtraEditors.TextEdit()
        Me.TxtNumCat = New DevExpress.XtraEditors.TextEdit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.TxtCodeBailleur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtMontConv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbBailleur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbConvention.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.TxtCodeCategorie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridCategorie, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.ViewCategorie, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.TxtCategorie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtMontCateg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPourcent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtNumCat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.BtActualiser)
        Me.GroupControl1.Controls.Add(Me.TxtCodeBailleur)
        Me.GroupControl1.Controls.Add(Me.TxtMontConv)
        Me.GroupControl1.Controls.Add(Me.CmbBailleur)
        Me.GroupControl1.Controls.Add(Me.CmbConvention)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(708, 52)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Bailleur/Convention"
        '
        'BtActualiser
        '
        Me.BtActualiser.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_161
        Me.BtActualiser.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtActualiser.Location = New System.Drawing.Point(663, 0)
        Me.BtActualiser.Name = "BtActualiser"
        Me.BtActualiser.Size = New System.Drawing.Size(45, 23)
        Me.BtActualiser.TabIndex = 5
        Me.BtActualiser.Text = "SimpleButton1"
        '
        'TxtCodeBailleur
        '
        Me.TxtCodeBailleur.EditValue = ""
        Me.TxtCodeBailleur.Location = New System.Drawing.Point(201, -3)
        Me.TxtCodeBailleur.Name = "TxtCodeBailleur"
        Me.TxtCodeBailleur.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCodeBailleur.Properties.Appearance.Options.UseFont = True
        Me.TxtCodeBailleur.Size = New System.Drawing.Size(56, 26)
        Me.TxtCodeBailleur.TabIndex = 4
        Me.TxtCodeBailleur.ToolTip = "[Entrer] pour valider"
        Me.TxtCodeBailleur.Visible = False
        '
        'TxtMontConv
        '
        Me.TxtMontConv.EditValue = ""
        Me.TxtMontConv.Location = New System.Drawing.Point(335, 23)
        Me.TxtMontConv.Name = "TxtMontConv"
        Me.TxtMontConv.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMontConv.Properties.Appearance.Options.UseFont = True
        Me.TxtMontConv.Properties.Mask.BeepOnError = True
        Me.TxtMontConv.Properties.Mask.EditMask = "f0"
        Me.TxtMontConv.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TxtMontConv.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.TxtMontConv.Size = New System.Drawing.Size(56, 26)
        Me.TxtMontConv.TabIndex = 3
        Me.TxtMontConv.ToolTip = "[Entrer] pour valider"
        Me.TxtMontConv.Visible = False
        '
        'CmbBailleur
        '
        Me.CmbBailleur.Dock = System.Windows.Forms.DockStyle.Left
        Me.CmbBailleur.Location = New System.Drawing.Point(2, 23)
        Me.CmbBailleur.Name = "CmbBailleur"
        Me.CmbBailleur.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbBailleur.Properties.Appearance.Options.UseFont = True
        Me.CmbBailleur.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbBailleur.Size = New System.Drawing.Size(193, 26)
        Me.CmbBailleur.TabIndex = 1
        '
        'CmbConvention
        '
        Me.CmbConvention.Dock = System.Windows.Forms.DockStyle.Right
        Me.CmbConvention.Location = New System.Drawing.Point(201, 23)
        Me.CmbConvention.Name = "CmbConvention"
        Me.CmbConvention.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbConvention.Properties.Appearance.Options.UseFont = True
        Me.CmbConvention.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbConvention.Size = New System.Drawing.Size(505, 26)
        Me.CmbConvention.TabIndex = 4
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.TxtCodeCategorie)
        Me.GroupControl2.Controls.Add(Me.GridCategorie)
        Me.GroupControl2.Controls.Add(Me.PanelControl1)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 52)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(708, 210)
        Me.GroupControl2.TabIndex = 1
        Me.GroupControl2.Text = "Catégories de dépense"
        '
        'TxtCodeCategorie
        '
        Me.TxtCodeCategorie.Location = New System.Drawing.Point(229, 2)
        Me.TxtCodeCategorie.Name = "TxtCodeCategorie"
        Me.TxtCodeCategorie.Size = New System.Drawing.Size(28, 20)
        Me.TxtCodeCategorie.TabIndex = 4
        Me.TxtCodeCategorie.Visible = False
        '
        'GridCategorie
        '
        Me.GridCategorie.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GridCategorie.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridCategorie.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridCategorie.Location = New System.Drawing.Point(2, 54)
        Me.GridCategorie.MainView = Me.ViewCategorie
        Me.GridCategorie.Name = "GridCategorie"
        Me.GridCategorie.Size = New System.Drawing.Size(704, 154)
        Me.GridCategorie.TabIndex = 3
        Me.GridCategorie.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewCategorie})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SupprimerToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(130, 26)
        '
        'SupprimerToolStripMenuItem
        '
        Me.SupprimerToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.SupprimerToolStripMenuItem.Name = "SupprimerToolStripMenuItem"
        Me.SupprimerToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.SupprimerToolStripMenuItem.Text = "Supprimer"
        '
        'ViewCategorie
        '
        Me.ViewCategorie.ActiveFilterEnabled = False
        Me.ViewCategorie.GridControl = Me.GridCategorie
        Me.ViewCategorie.Name = "ViewCategorie"
        Me.ViewCategorie.OptionsBehavior.Editable = False
        Me.ViewCategorie.OptionsBehavior.ReadOnly = True
        Me.ViewCategorie.OptionsCustomization.AllowColumnMoving = False
        Me.ViewCategorie.OptionsCustomization.AllowFilter = False
        Me.ViewCategorie.OptionsCustomization.AllowGroup = False
        Me.ViewCategorie.OptionsCustomization.AllowSort = False
        Me.ViewCategorie.OptionsFilter.AllowFilterEditor = False
        Me.ViewCategorie.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewCategorie.OptionsPrint.AutoWidth = False
        Me.ViewCategorie.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewCategorie.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewCategorie.OptionsView.ColumnAutoWidth = False
        Me.ViewCategorie.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewCategorie.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewCategorie.OptionsView.ShowGroupPanel = False
        Me.ViewCategorie.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewCategorie.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.TxtCategorie)
        Me.PanelControl1.Controls.Add(Me.TxtMontCateg)
        Me.PanelControl1.Controls.Add(Me.TxtPourcent)
        Me.PanelControl1.Controls.Add(Me.TxtNumCat)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(2, 23)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(704, 31)
        Me.PanelControl1.TabIndex = 0
        '
        'TxtCategorie
        '
        Me.TxtCategorie.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtCategorie.Location = New System.Drawing.Point(52, 2)
        Me.TxtCategorie.Name = "TxtCategorie"
        Me.TxtCategorie.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCategorie.Properties.Appearance.Options.UseFont = True
        Me.TxtCategorie.Properties.MaxLength = 150
        Me.TxtCategorie.Size = New System.Drawing.Size(450, 26)
        Me.TxtCategorie.TabIndex = 6
        Me.TxtCategorie.ToolTip = "[Entrer] pour valider"
        '
        'TxtMontCateg
        '
        Me.TxtMontCateg.Dock = System.Windows.Forms.DockStyle.Right
        Me.TxtMontCateg.EditValue = ""
        Me.TxtMontCateg.Location = New System.Drawing.Point(502, 2)
        Me.TxtMontCateg.Name = "TxtMontCateg"
        Me.TxtMontCateg.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMontCateg.Properties.Appearance.Options.UseFont = True
        Me.TxtMontCateg.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtMontCateg.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.TxtMontCateg.Properties.Mask.BeepOnError = True
        Me.TxtMontCateg.Properties.Mask.EditMask = "f0"
        Me.TxtMontCateg.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TxtMontCateg.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.TxtMontCateg.Size = New System.Drawing.Size(144, 26)
        Me.TxtMontCateg.TabIndex = 7
        Me.TxtMontCateg.ToolTip = "[Entrer] pour valider"
        '
        'TxtPourcent
        '
        Me.TxtPourcent.Dock = System.Windows.Forms.DockStyle.Right
        Me.TxtPourcent.EditValue = ""
        Me.TxtPourcent.Location = New System.Drawing.Point(646, 2)
        Me.TxtPourcent.Name = "TxtPourcent"
        Me.TxtPourcent.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPourcent.Properties.Appearance.Options.UseFont = True
        Me.TxtPourcent.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtPourcent.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtPourcent.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value
        Me.TxtPourcent.Properties.Mask.BeepOnError = True
        Me.TxtPourcent.Properties.Mask.EditMask = "f"
        Me.TxtPourcent.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TxtPourcent.Properties.Mask.ShowPlaceHolders = False
        Me.TxtPourcent.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.TxtPourcent.Properties.ReadOnly = True
        Me.TxtPourcent.Size = New System.Drawing.Size(56, 26)
        Me.TxtPourcent.TabIndex = 2
        Me.TxtPourcent.ToolTip = "[Entrer] pour valider"
        '
        'TxtNumCat
        '
        Me.TxtNumCat.Dock = System.Windows.Forms.DockStyle.Left
        Me.TxtNumCat.EditValue = ""
        Me.TxtNumCat.Location = New System.Drawing.Point(2, 2)
        Me.TxtNumCat.Name = "TxtNumCat"
        Me.TxtNumCat.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNumCat.Properties.Appearance.Options.UseFont = True
        Me.TxtNumCat.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtNumCat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtNumCat.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtNumCat.Properties.MaxLength = 1
        Me.TxtNumCat.Size = New System.Drawing.Size(50, 26)
        Me.TxtNumCat.TabIndex = 5
        Me.TxtNumCat.ToolTip = "[Entrer] pour valider"
        '
        'CategorieDepense
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(708, 262)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CategorieDepense"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Catégories de dépense"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.TxtCodeBailleur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtMontConv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbBailleur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbConvention.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.TxtCodeCategorie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridCategorie, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.ViewCategorie, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.TxtCategorie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtMontCateg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPourcent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtNumCat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents CmbBailleur As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents CmbConvention As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents TxtCategorie As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtMontCateg As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GridCategorie As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewCategorie As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TxtPourcent As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtMontConv As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtCodeBailleur As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtNumCat As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SupprimerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TxtCodeCategorie As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BtActualiser As DevExpress.XtraEditors.SimpleButton
End Class
