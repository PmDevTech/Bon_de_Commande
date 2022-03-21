<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PlanMarcheSuite
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
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.NavBarControlTypeMarche = New DevExpress.XtraNavBar.NavBarControl()
        Me.NavBarTravaux = New DevExpress.XtraNavBar.NavBarGroup()
        Me.NavBarConsultants = New DevExpress.XtraNavBar.NavBarGroup()
        Me.NavBarFournitures = New DevExpress.XtraNavBar.NavBarGroup()
        Me.NavBarAutresServices = New DevExpress.XtraNavBar.NavBarGroup()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.cmbConvention = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbBailleur = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.LgEtape = New DevExpress.XtraGrid.GridControl()
        Me.ViewEtape = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.btSave = New DevExpress.XtraEditors.SimpleButton()
        Me.cmbRevue = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtStatut = New DevExpress.XtraEditors.TextEdit()
        Me.txtMethode = New DevExpress.XtraEditors.TextEdit()
        Me.txtMontant = New DevExpress.XtraEditors.TextEdit()
        Me.txtMarche = New DevExpress.XtraEditors.TextEdit()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.NavBarControlTypeMarche, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.cmbConvention.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBailleur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.LgEtape, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewEtape, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.cmbRevue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStatut.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMethode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMontant.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMarche.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.PanelControl2)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.PanelControl1)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.GroupControl1)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.GroupControl4)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1019, 419)
        Me.SplitContainerControl1.SplitterPosition = 268
        Me.SplitContainerControl1.TabIndex = 0
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.NavBarControlTypeMarche)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(0, 32)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(268, 387)
        Me.PanelControl2.TabIndex = 1
        '
        'NavBarControlTypeMarche
        '
        Me.NavBarControlTypeMarche.ActiveGroup = Me.NavBarTravaux
        Me.NavBarControlTypeMarche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NavBarControlTypeMarche.Groups.AddRange(New DevExpress.XtraNavBar.NavBarGroup() {Me.NavBarConsultants, Me.NavBarFournitures, Me.NavBarAutresServices, Me.NavBarTravaux})
        Me.NavBarControlTypeMarche.Location = New System.Drawing.Point(2, 2)
        Me.NavBarControlTypeMarche.Name = "NavBarControlTypeMarche"
        Me.NavBarControlTypeMarche.OptionsNavPane.ExpandedWidth = 264
        Me.NavBarControlTypeMarche.Size = New System.Drawing.Size(264, 383)
        Me.NavBarControlTypeMarche.TabIndex = 2
        Me.NavBarControlTypeMarche.Text = "NavBarControl1"
        '
        'NavBarTravaux
        '
        Me.NavBarTravaux.Caption = "Travaux"
        Me.NavBarTravaux.Expanded = True
        Me.NavBarTravaux.Name = "NavBarTravaux"
        '
        'NavBarConsultants
        '
        Me.NavBarConsultants.Caption = "Consultants"
        Me.NavBarConsultants.Expanded = True
        Me.NavBarConsultants.Name = "NavBarConsultants"
        '
        'NavBarFournitures
        '
        Me.NavBarFournitures.Caption = "Fournitures"
        Me.NavBarFournitures.Expanded = True
        Me.NavBarFournitures.Name = "NavBarFournitures"
        '
        'NavBarAutresServices
        '
        Me.NavBarAutresServices.Caption = "Services autres que les services de consultants"
        Me.NavBarAutresServices.Expanded = True
        Me.NavBarAutresServices.Name = "NavBarAutresServices"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.cmbConvention)
        Me.PanelControl1.Controls.Add(Me.cmbBailleur)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(268, 32)
        Me.PanelControl1.TabIndex = 0
        '
        'cmbConvention
        '
        Me.cmbConvention.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbConvention.Enabled = False
        Me.cmbConvention.Location = New System.Drawing.Point(104, 5)
        Me.cmbConvention.Name = "cmbConvention"
        Me.cmbConvention.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.OK)})
        Me.cmbConvention.Size = New System.Drawing.Size(160, 20)
        Me.cmbConvention.TabIndex = 35
        '
        'cmbBailleur
        '
        Me.cmbBailleur.Enabled = False
        Me.cmbBailleur.Location = New System.Drawing.Point(3, 5)
        Me.cmbBailleur.Name = "cmbBailleur"
        Me.cmbBailleur.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.OK)})
        Me.cmbBailleur.Size = New System.Drawing.Size(99, 20)
        Me.cmbBailleur.TabIndex = 34
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.LgEtape)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 87)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(746, 332)
        Me.GroupControl1.TabIndex = 27
        Me.GroupControl1.Text = "Etapes"
        '
        'LgEtape
        '
        Me.LgEtape.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LgEtape.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LgEtape.Location = New System.Drawing.Point(2, 21)
        Me.LgEtape.MainView = Me.ViewEtape
        Me.LgEtape.Name = "LgEtape"
        Me.LgEtape.Size = New System.Drawing.Size(742, 309)
        Me.LgEtape.TabIndex = 51
        Me.LgEtape.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewEtape, Me.GridView1})
        '
        'ViewEtape
        '
        Me.ViewEtape.ActiveFilterEnabled = False
        Me.ViewEtape.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewEtape.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewEtape.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
        Me.ViewEtape.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.ViewEtape.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.ViewEtape.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.ViewEtape.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewEtape.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewEtape.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
        Me.ViewEtape.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.ViewEtape.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.ViewEtape.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.ViewEtape.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.ViewEtape.Appearance.Empty.BackColor2 = System.Drawing.Color.White
        Me.ViewEtape.Appearance.Empty.Options.UseBackColor = True
        Me.ViewEtape.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewEtape.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.ViewEtape.Appearance.EvenRow.Options.UseBackColor = True
        Me.ViewEtape.Appearance.EvenRow.Options.UseForeColor = True
        Me.ViewEtape.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewEtape.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewEtape.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.ViewEtape.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.ViewEtape.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.ViewEtape.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.ViewEtape.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.ViewEtape.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewEtape.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewEtape.Appearance.FilterPanel.Options.UseBackColor = True
        Me.ViewEtape.Appearance.FilterPanel.Options.UseForeColor = True
        Me.ViewEtape.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(218, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.ViewEtape.Appearance.FixedLine.Options.UseBackColor = True
        Me.ViewEtape.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.ViewEtape.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.ViewEtape.Appearance.FocusedCell.Options.UseBackColor = True
        Me.ViewEtape.Appearance.FocusedCell.Options.UseForeColor = True
        Me.ViewEtape.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(91, Byte), Integer))
        Me.ViewEtape.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.ViewEtape.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewEtape.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewEtape.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewEtape.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewEtape.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewEtape.Appearance.FooterPanel.Options.UseBackColor = True
        Me.ViewEtape.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.ViewEtape.Appearance.FooterPanel.Options.UseForeColor = True
        Me.ViewEtape.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewEtape.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewEtape.Appearance.GroupButton.Options.UseBackColor = True
        Me.ViewEtape.Appearance.GroupButton.Options.UseBorderColor = True
        Me.ViewEtape.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewEtape.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewEtape.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.ViewEtape.Appearance.GroupFooter.Options.UseBackColor = True
        Me.ViewEtape.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.ViewEtape.Appearance.GroupFooter.Options.UseForeColor = True
        Me.ViewEtape.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.ViewEtape.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewEtape.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewEtape.Appearance.GroupPanel.Options.UseBackColor = True
        Me.ViewEtape.Appearance.GroupPanel.Options.UseForeColor = True
        Me.ViewEtape.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewEtape.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewEtape.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
        Me.ViewEtape.Appearance.GroupRow.Options.UseBackColor = True
        Me.ViewEtape.Appearance.GroupRow.Options.UseBorderColor = True
        Me.ViewEtape.Appearance.GroupRow.Options.UseForeColor = True
        Me.ViewEtape.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewEtape.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewEtape.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewEtape.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.ViewEtape.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.ViewEtape.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.ViewEtape.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(183, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ViewEtape.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.ViewEtape.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.ViewEtape.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.ViewEtape.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewEtape.Appearance.HorzLine.Options.UseBackColor = True
        Me.ViewEtape.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewEtape.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.ViewEtape.Appearance.OddRow.Options.UseBackColor = True
        Me.ViewEtape.Appearance.OddRow.Options.UseForeColor = True
        Me.ViewEtape.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.ViewEtape.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
        Me.ViewEtape.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(186, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.ViewEtape.Appearance.Preview.Options.UseBackColor = True
        Me.ViewEtape.Appearance.Preview.Options.UseFont = True
        Me.ViewEtape.Appearance.Preview.Options.UseForeColor = True
        Me.ViewEtape.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewEtape.Appearance.Row.BorderColor = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewEtape.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.ViewEtape.Appearance.Row.Options.UseBackColor = True
        Me.ViewEtape.Appearance.Row.Options.UseBorderColor = True
        Me.ViewEtape.Appearance.Row.Options.UseForeColor = True
        Me.ViewEtape.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.ViewEtape.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
        Me.ViewEtape.Appearance.RowSeparator.Options.UseBackColor = True
        Me.ViewEtape.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.ViewEtape.Appearance.SelectedRow.Options.UseBackColor = True
        Me.ViewEtape.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
        Me.ViewEtape.Appearance.TopNewRow.Options.UseBackColor = True
        Me.ViewEtape.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewEtape.Appearance.VertLine.Options.UseBackColor = True
        Me.ViewEtape.GridControl = Me.LgEtape
        Me.ViewEtape.Name = "ViewEtape"
        Me.ViewEtape.OptionsBehavior.Editable = False
        Me.ViewEtape.OptionsBehavior.ReadOnly = True
        Me.ViewEtape.OptionsCustomization.AllowColumnMoving = False
        Me.ViewEtape.OptionsCustomization.AllowFilter = False
        Me.ViewEtape.OptionsCustomization.AllowGroup = False
        Me.ViewEtape.OptionsCustomization.AllowSort = False
        Me.ViewEtape.OptionsFilter.AllowFilterEditor = False
        Me.ViewEtape.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewEtape.OptionsPrint.AutoWidth = False
        Me.ViewEtape.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewEtape.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewEtape.OptionsView.ColumnAutoWidth = False
        Me.ViewEtape.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewEtape.OptionsView.EnableAppearanceOddRow = True
        Me.ViewEtape.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewEtape.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewEtape.OptionsView.ShowGroupPanel = False
        Me.ViewEtape.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewEtape.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.LgEtape
        Me.GridView1.Name = "GridView1"
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.btSave)
        Me.GroupControl4.Controls.Add(Me.cmbRevue)
        Me.GroupControl4.Controls.Add(Me.txtStatut)
        Me.GroupControl4.Controls.Add(Me.txtMethode)
        Me.GroupControl4.Controls.Add(Me.txtMontant)
        Me.GroupControl4.Controls.Add(Me.txtMarche)
        Me.GroupControl4.Controls.Add(Me.Label5)
        Me.GroupControl4.Controls.Add(Me.Label2)
        Me.GroupControl4.Controls.Add(Me.Label3)
        Me.GroupControl4.Controls.Add(Me.Label1)
        Me.GroupControl4.Controls.Add(Me.Label4)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl4.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(746, 87)
        Me.GroupControl4.TabIndex = 26
        Me.GroupControl4.Text = "Informations"
        '
        'btSave
        '
        Me.btSave.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btSave.Appearance.Options.UseFont = True
        Me.btSave.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.btSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btSave.Location = New System.Drawing.Point(687, 30)
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(53, 50)
        Me.btSave.TabIndex = 36
        '
        'cmbRevue
        '
        Me.cmbRevue.Location = New System.Drawing.Point(383, 57)
        Me.cmbRevue.Name = "cmbRevue"
        Me.cmbRevue.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.OK)})
        Me.cmbRevue.Properties.Items.AddRange(New Object() {"Postériori", "Priori"})
        Me.cmbRevue.Size = New System.Drawing.Size(128, 20)
        Me.cmbRevue.TabIndex = 34
        '
        'txtStatut
        '
        Me.txtStatut.Enabled = False
        Me.txtStatut.Location = New System.Drawing.Point(559, 58)
        Me.txtStatut.Name = "txtStatut"
        Me.txtStatut.Properties.Appearance.Options.UseTextOptions = True
        Me.txtStatut.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtStatut.Size = New System.Drawing.Size(122, 20)
        Me.txtStatut.TabIndex = 19
        '
        'txtMethode
        '
        Me.txtMethode.Enabled = False
        Me.txtMethode.Location = New System.Drawing.Point(254, 57)
        Me.txtMethode.Name = "txtMethode"
        Me.txtMethode.Properties.Appearance.Options.UseTextOptions = True
        Me.txtMethode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtMethode.Size = New System.Drawing.Size(83, 20)
        Me.txtMethode.TabIndex = 19
        '
        'txtMontant
        '
        Me.txtMontant.Enabled = False
        Me.txtMontant.Location = New System.Drawing.Point(53, 56)
        Me.txtMontant.Name = "txtMontant"
        Me.txtMontant.Properties.Appearance.Options.UseTextOptions = True
        Me.txtMontant.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtMontant.Size = New System.Drawing.Size(146, 20)
        Me.txtMontant.TabIndex = 19
        '
        'txtMarche
        '
        Me.txtMarche.Location = New System.Drawing.Point(53, 30)
        Me.txtMarche.Name = "txtMarche"
        Me.txtMarche.Size = New System.Drawing.Size(628, 20)
        Me.txtMarche.TabIndex = 19
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(517, 60)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Statut"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(206, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Methode"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(343, 61)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Revue"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Montant"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 13)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Marché"
        '
        'PlanMarcheSuite
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1019, 419)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PlanMarcheSuite"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Plan de passation de marchés"
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.NavBarControlTypeMarche, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.cmbConvention.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBailleur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.LgEtape, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewEtape, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.cmbRevue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStatut.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMethode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMontant.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMarche.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtMarche As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtMethode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtMontant As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtStatut As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label5 As Label
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents NavBarControlTypeMarche As DevExpress.XtraNavBar.NavBarControl
    Friend WithEvents NavBarTravaux As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents NavBarConsultants As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents NavBarFournitures As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents NavBarAutresServices As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents cmbBailleur As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbConvention As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbRevue As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LgEtape As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewEtape As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btSave As DevExpress.XtraEditors.SimpleButton
End Class
