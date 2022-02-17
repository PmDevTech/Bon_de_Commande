<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DivisionAdministrative
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
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.BtAjoutDecoup = New DevExpress.XtraEditors.SimpleButton()
        Me.LblNomPlan = New DevExpress.XtraEditors.LabelControl()
        Me.BtDeselect = New DevExpress.XtraEditors.SimpleButton()
        Me.BtSelect = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.GridPlan = New DevExpress.XtraGrid.GridControl()
        Me.ViewPlan = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GridGeo = New DevExpress.XtraGrid.GridControl()
        Me.ViewGeo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.GridDecoup = New DevExpress.XtraGrid.GridControl()
        Me.ViewDecoup = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GbAjoutDecoup = New DevExpress.XtraEditors.GroupControl()
        Me.cmbZone = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.CmbDecoupSup = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtLibDecoup = New DevExpress.XtraEditors.TextEdit()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.BtQuitter = New DevExpress.XtraEditors.SimpleButton()
        Me.BtEnrgDecoup = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.GridPlan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewPlan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GridGeo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewGeo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.GridDecoup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewDecoup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GbAjoutDecoup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GbAjoutDecoup.SuspendLayout()
        CType(Me.cmbZone.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbDecoupSup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtLibDecoup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.PanelControl2)
        Me.PanelControl1.Controls.Add(Me.BtDeselect)
        Me.PanelControl1.Controls.Add(Me.BtSelect)
        Me.PanelControl1.Controls.Add(Me.GroupControl2)
        Me.PanelControl1.Controls.Add(Me.GroupControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(654, 215)
        Me.PanelControl1.TabIndex = 0
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.BtAjoutDecoup)
        Me.PanelControl2.Controls.Add(Me.LblNomPlan)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl2.Location = New System.Drawing.Point(2, 182)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(650, 31)
        Me.PanelControl2.TabIndex = 4
        '
        'BtAjoutDecoup
        '
        Me.BtAjoutDecoup.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtAjoutDecoup.Appearance.Options.UseFont = True
        Me.BtAjoutDecoup.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtAjoutDecoup.Image = Global.ClearProject.My.Resources.Resources.Add_16x16
        Me.BtAjoutDecoup.Location = New System.Drawing.Point(502, 2)
        Me.BtAjoutDecoup.Name = "BtAjoutDecoup"
        Me.BtAjoutDecoup.Size = New System.Drawing.Size(146, 27)
        Me.BtAjoutDecoup.TabIndex = 0
        Me.BtAjoutDecoup.Text = "Ajouter Découpage"
        '
        'LblNomPlan
        '
        Me.LblNomPlan.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNomPlan.Location = New System.Drawing.Point(5, 7)
        Me.LblNomPlan.Name = "LblNomPlan"
        Me.LblNomPlan.Size = New System.Drawing.Size(9, 15)
        Me.LblNomPlan.TabIndex = 1
        Me.LblNomPlan.Text = "..."
        '
        'BtDeselect
        '
        Me.BtDeselect.Location = New System.Drawing.Point(299, 115)
        Me.BtDeselect.Name = "BtDeselect"
        Me.BtDeselect.Size = New System.Drawing.Size(57, 22)
        Me.BtDeselect.TabIndex = 3
        Me.BtDeselect.Text = "<<"
        '
        'BtSelect
        '
        Me.BtSelect.Location = New System.Drawing.Point(299, 81)
        Me.BtSelect.Name = "BtSelect"
        Me.BtSelect.Size = New System.Drawing.Size(57, 22)
        Me.BtSelect.TabIndex = 2
        Me.BtSelect.Text = ">>"
        '
        'GroupControl2
        '
        Me.GroupControl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl2.Controls.Add(Me.GridPlan)
        Me.GroupControl2.Location = New System.Drawing.Point(364, 5)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(285, 174)
        Me.GroupControl2.TabIndex = 1
        Me.GroupControl2.Text = "Découpage Analytique"
        '
        'GridPlan
        '
        Me.GridPlan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridPlan.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridPlan.Location = New System.Drawing.Point(2, 23)
        Me.GridPlan.MainView = Me.ViewPlan
        Me.GridPlan.Name = "GridPlan"
        Me.GridPlan.Size = New System.Drawing.Size(281, 149)
        Me.GridPlan.TabIndex = 11
        Me.GridPlan.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewPlan})
        '
        'ViewPlan
        '
        Me.ViewPlan.ActiveFilterEnabled = False
        Me.ViewPlan.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewPlan.Appearance.Row.Options.UseFont = True
        Me.ViewPlan.GridControl = Me.GridPlan
        Me.ViewPlan.Name = "ViewPlan"
        Me.ViewPlan.OptionsBehavior.Editable = False
        Me.ViewPlan.OptionsBehavior.ReadOnly = True
        Me.ViewPlan.OptionsCustomization.AllowColumnMoving = False
        Me.ViewPlan.OptionsCustomization.AllowFilter = False
        Me.ViewPlan.OptionsCustomization.AllowGroup = False
        Me.ViewPlan.OptionsCustomization.AllowSort = False
        Me.ViewPlan.OptionsFilter.AllowFilterEditor = False
        Me.ViewPlan.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewPlan.OptionsPrint.AutoWidth = False
        Me.ViewPlan.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewPlan.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewPlan.OptionsView.ColumnAutoWidth = False
        Me.ViewPlan.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewPlan.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewPlan.OptionsView.ShowGroupPanel = False
        Me.ViewPlan.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewPlan.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl1.Controls.Add(Me.GridGeo)
        Me.GroupControl1.Location = New System.Drawing.Point(5, 5)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(285, 174)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Découpage Géographique"
        '
        'GridGeo
        '
        Me.GridGeo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridGeo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridGeo.Location = New System.Drawing.Point(2, 23)
        Me.GridGeo.MainView = Me.ViewGeo
        Me.GridGeo.Name = "GridGeo"
        Me.GridGeo.Size = New System.Drawing.Size(281, 149)
        Me.GridGeo.TabIndex = 11
        Me.GridGeo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewGeo})
        '
        'ViewGeo
        '
        Me.ViewGeo.ActiveFilterEnabled = False
        Me.ViewGeo.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewGeo.Appearance.Row.Options.UseFont = True
        Me.ViewGeo.GridControl = Me.GridGeo
        Me.ViewGeo.Name = "ViewGeo"
        Me.ViewGeo.OptionsBehavior.Editable = False
        Me.ViewGeo.OptionsBehavior.ReadOnly = True
        Me.ViewGeo.OptionsCustomization.AllowColumnMoving = False
        Me.ViewGeo.OptionsCustomization.AllowFilter = False
        Me.ViewGeo.OptionsCustomization.AllowGroup = False
        Me.ViewGeo.OptionsCustomization.AllowSort = False
        Me.ViewGeo.OptionsFilter.AllowFilterEditor = False
        Me.ViewGeo.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewGeo.OptionsPrint.AutoWidth = False
        Me.ViewGeo.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewGeo.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewGeo.OptionsView.ColumnAutoWidth = False
        Me.ViewGeo.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewGeo.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewGeo.OptionsView.ShowGroupPanel = False
        Me.ViewGeo.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewGeo.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'GroupControl3
        '
        Me.GroupControl3.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl3.AppearanceCaption.Options.UseFont = True
        Me.GroupControl3.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl3.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl3.Controls.Add(Me.GridDecoup)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl3.Location = New System.Drawing.Point(0, 330)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(654, 176)
        Me.GroupControl3.TabIndex = 2
        Me.GroupControl3.Text = "DECOUPAGE ADMINISTRATIF"
        '
        'GridDecoup
        '
        Me.GridDecoup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridDecoup.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridDecoup.Location = New System.Drawing.Point(2, 23)
        Me.GridDecoup.MainView = Me.ViewDecoup
        Me.GridDecoup.Name = "GridDecoup"
        Me.GridDecoup.Size = New System.Drawing.Size(650, 151)
        Me.GridDecoup.TabIndex = 11
        Me.GridDecoup.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewDecoup})
        '
        'ViewDecoup
        '
        Me.ViewDecoup.ActiveFilterEnabled = False
        Me.ViewDecoup.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewDecoup.Appearance.Row.Options.UseFont = True
        Me.ViewDecoup.GridControl = Me.GridDecoup
        Me.ViewDecoup.Name = "ViewDecoup"
        Me.ViewDecoup.OptionsBehavior.Editable = False
        Me.ViewDecoup.OptionsBehavior.ReadOnly = True
        Me.ViewDecoup.OptionsCustomization.AllowColumnMoving = False
        Me.ViewDecoup.OptionsCustomization.AllowFilter = False
        Me.ViewDecoup.OptionsCustomization.AllowGroup = False
        Me.ViewDecoup.OptionsCustomization.AllowSort = False
        Me.ViewDecoup.OptionsFilter.AllowFilterEditor = False
        Me.ViewDecoup.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewDecoup.OptionsPrint.AutoWidth = False
        Me.ViewDecoup.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewDecoup.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewDecoup.OptionsView.ColumnAutoWidth = False
        Me.ViewDecoup.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewDecoup.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewDecoup.OptionsView.ShowGroupPanel = False
        Me.ViewDecoup.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewDecoup.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'GbAjoutDecoup
        '
        Me.GbAjoutDecoup.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GbAjoutDecoup.AppearanceCaption.Options.UseFont = True
        Me.GbAjoutDecoup.AppearanceCaption.Options.UseTextOptions = True
        Me.GbAjoutDecoup.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GbAjoutDecoup.Controls.Add(Me.cmbZone)
        Me.GbAjoutDecoup.Controls.Add(Me.LabelControl1)
        Me.GbAjoutDecoup.Controls.Add(Me.CmbDecoupSup)
        Me.GbAjoutDecoup.Controls.Add(Me.LabelControl3)
        Me.GbAjoutDecoup.Controls.Add(Me.LabelControl2)
        Me.GbAjoutDecoup.Controls.Add(Me.TxtLibDecoup)
        Me.GbAjoutDecoup.Controls.Add(Me.PanelControl3)
        Me.GbAjoutDecoup.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GbAjoutDecoup.Location = New System.Drawing.Point(0, 215)
        Me.GbAjoutDecoup.Name = "GbAjoutDecoup"
        Me.GbAjoutDecoup.Size = New System.Drawing.Size(654, 115)
        Me.GbAjoutDecoup.TabIndex = 3
        Me.GbAjoutDecoup.Text = "Nouveau Découpage"
        Me.GbAjoutDecoup.Visible = False
        '
        'cmbZone
        '
        Me.cmbZone.Location = New System.Drawing.Point(110, 30)
        Me.cmbZone.Name = "cmbZone"
        Me.cmbZone.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbZone.Properties.Appearance.Options.UseFont = True
        Me.cmbZone.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbZone.Size = New System.Drawing.Size(412, 22)
        Me.cmbZone.TabIndex = 3
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(81, 33)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(27, 15)
        Me.LabelControl1.TabIndex = 5
        Me.LabelControl1.Text = "Zone"
        '
        'CmbDecoupSup
        '
        Me.CmbDecoupSup.Location = New System.Drawing.Point(110, 87)
        Me.CmbDecoupSup.Name = "CmbDecoupSup"
        Me.CmbDecoupSup.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbDecoupSup.Properties.Appearance.Options.UseFont = True
        Me.CmbDecoupSup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbDecoupSup.Size = New System.Drawing.Size(412, 22)
        Me.CmbDecoupSup.TabIndex = 5
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(50, 90)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(58, 15)
        Me.LabelControl3.TabIndex = 5
        Me.LabelControl3.Text = "Dépend de"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(5, 60)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(103, 15)
        Me.LabelControl2.TabIndex = 4
        Me.LabelControl2.Text = "Libellé Découpage"
        '
        'TxtLibDecoup
        '
        Me.TxtLibDecoup.Location = New System.Drawing.Point(110, 57)
        Me.TxtLibDecoup.Name = "TxtLibDecoup"
        Me.TxtLibDecoup.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLibDecoup.Properties.Appearance.Options.UseFont = True
        Me.TxtLibDecoup.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtLibDecoup.Size = New System.Drawing.Size(412, 22)
        Me.TxtLibDecoup.TabIndex = 4
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.BtQuitter)
        Me.PanelControl3.Controls.Add(Me.BtEnrgDecoup)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelControl3.Location = New System.Drawing.Point(539, 23)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(113, 90)
        Me.PanelControl3.TabIndex = 6
        '
        'BtQuitter
        '
        Me.BtQuitter.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtQuitter.Appearance.Options.UseFont = True
        Me.BtQuitter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BtQuitter.Image = Global.ClearProject.My.Resources.Resources.Delete_16x16
        Me.BtQuitter.Location = New System.Drawing.Point(2, 61)
        Me.BtQuitter.Name = "BtQuitter"
        Me.BtQuitter.Size = New System.Drawing.Size(109, 27)
        Me.BtQuitter.TabIndex = 7
        Me.BtQuitter.Text = "Quitter"
        '
        'BtEnrgDecoup
        '
        Me.BtEnrgDecoup.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnrgDecoup.Appearance.Options.UseFont = True
        Me.BtEnrgDecoup.Dock = System.Windows.Forms.DockStyle.Top
        Me.BtEnrgDecoup.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.BtEnrgDecoup.Location = New System.Drawing.Point(2, 2)
        Me.BtEnrgDecoup.Name = "BtEnrgDecoup"
        Me.BtEnrgDecoup.Size = New System.Drawing.Size(109, 27)
        Me.BtEnrgDecoup.TabIndex = 6
        Me.BtEnrgDecoup.Text = "Enregistrer"
        '
        'DivisionAdministrative
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(654, 506)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.GbAjoutDecoup)
        Me.Controls.Add(Me.GroupControl3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DivisionAdministrative"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Division Administrative"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.PanelControl2.PerformLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.GridPlan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewPlan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.GridGeo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewGeo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.GridDecoup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewDecoup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GbAjoutDecoup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GbAjoutDecoup.ResumeLayout(False)
        Me.GbAjoutDecoup.PerformLayout()
        CType(Me.cmbZone.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbDecoupSup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtLibDecoup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtDeselect As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtSelect As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LblNomPlan As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtAjoutDecoup As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridPlan As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewPlan As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridGeo As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewGeo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridDecoup As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewDecoup As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GbAjoutDecoup As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtLibDecoup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtQuitter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtEnrgDecoup As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CmbDecoupSup As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbZone As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
