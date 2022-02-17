<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Comparaison_Etats_Financier
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.rdComptesNonRattaches = New DevExpress.XtraEditors.CheckEdit()
        Me.rdComptesRattaches = New DevExpress.XtraEditors.CheckEdit()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.cmbRubrique = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btPrint = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.LgListComparaison = New DevExpress.XtraGrid.GridControl()
        Me.ViewComparaison = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.rdComptesNonRattaches.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdComptesRattaches.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.cmbRubrique.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.LgListComparaison, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewComparaison, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.rdComptesNonRattaches)
        Me.PanelControl1.Controls.Add(Me.rdComptesRattaches)
        Me.PanelControl1.Controls.Add(Me.PanelControl3)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(784, 63)
        Me.PanelControl1.TabIndex = 0
        '
        'rdComptesNonRattaches
        '
        Me.rdComptesNonRattaches.Location = New System.Drawing.Point(410, 41)
        Me.rdComptesNonRattaches.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.rdComptesNonRattaches.Name = "rdComptesNonRattaches"
        Me.rdComptesNonRattaches.Properties.Caption = "Visualisation des comptes non rattachés"
        Me.rdComptesNonRattaches.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.rdComptesNonRattaches.Properties.RadioGroupIndex = 1
        Me.rdComptesNonRattaches.Size = New System.Drawing.Size(349, 19)
        Me.rdComptesNonRattaches.TabIndex = 17
        Me.rdComptesNonRattaches.TabStop = False
        '
        'rdComptesRattaches
        '
        Me.rdComptesRattaches.Location = New System.Drawing.Point(102, 41)
        Me.rdComptesRattaches.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.rdComptesRattaches.Name = "rdComptesRattaches"
        Me.rdComptesRattaches.Properties.Caption = "Visualisation des comparaisons"
        Me.rdComptesRattaches.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.rdComptesRattaches.Properties.RadioGroupIndex = 1
        Me.rdComptesRattaches.Size = New System.Drawing.Size(298, 19)
        Me.rdComptesRattaches.TabIndex = 16
        Me.rdComptesRattaches.TabStop = False
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.cmbRubrique)
        Me.PanelControl3.Controls.Add(Me.btPrint)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl3.Location = New System.Drawing.Point(2, 2)
        Me.PanelControl3.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(780, 34)
        Me.PanelControl3.TabIndex = 15
        '
        'cmbRubrique
        '
        Me.cmbRubrique.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbRubrique.Location = New System.Drawing.Point(2, 2)
        Me.cmbRubrique.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.cmbRubrique.Name = "cmbRubrique"
        Me.cmbRubrique.Properties.AutoHeight = False
        Me.cmbRubrique.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbRubrique.Properties.Items.AddRange(New Object() {"Bilan", "Compte de résultat", "Tableau Emplois Ressources"})
        Me.cmbRubrique.Size = New System.Drawing.Size(681, 30)
        Me.cmbRubrique.TabIndex = 0
        '
        'btPrint
        '
        Me.btPrint.Dock = System.Windows.Forms.DockStyle.Right
        Me.btPrint.Image = Global.ClearProject.My.Resources.Resources.Group_Reports
        Me.btPrint.Location = New System.Drawing.Point(683, 2)
        Me.btPrint.Margin = New System.Windows.Forms.Padding(2)
        Me.btPrint.Name = "btPrint"
        Me.btPrint.Size = New System.Drawing.Size(95, 30)
        Me.btPrint.TabIndex = 15
        Me.btPrint.Text = "Imprimer"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.LgListComparaison)
        Me.PanelControl2.Controls.Add(Me.GridControl1)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(0, 63)
        Me.PanelControl2.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(784, 398)
        Me.PanelControl2.TabIndex = 1
        '
        'LgListComparaison
        '
        Me.LgListComparaison.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.LgListComparaison.AllowDrop = True
        Me.LgListComparaison.AllowRestoreSelectionAndFocusedRow = DevExpress.Utils.DefaultBoolean.[True]
        Me.LgListComparaison.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LgListComparaison.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LgListComparaison.Location = New System.Drawing.Point(2, 2)
        Me.LgListComparaison.MainView = Me.ViewComparaison
        Me.LgListComparaison.Name = "LgListComparaison"
        Me.LgListComparaison.Size = New System.Drawing.Size(780, 394)
        Me.LgListComparaison.TabIndex = 46
        Me.LgListComparaison.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewComparaison, Me.GridView4})
        '
        'ViewComparaison
        '
        Me.ViewComparaison.ActiveFilterEnabled = False
        Me.ViewComparaison.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewComparaison.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewComparaison.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
        Me.ViewComparaison.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.ViewComparaison.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.ViewComparaison.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.ViewComparaison.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.ViewComparaison.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
        Me.ViewComparaison.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.ViewComparaison.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.ViewComparaison.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewComparaison.Appearance.Empty.BackColor2 = System.Drawing.Color.White
        Me.ViewComparaison.Appearance.Empty.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ViewComparaison.Appearance.EvenRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ViewComparaison.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.ViewComparaison.Appearance.EvenRow.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.EvenRow.Options.UseBorderColor = True
        Me.ViewComparaison.Appearance.EvenRow.Options.UseForeColor = True
        Me.ViewComparaison.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewComparaison.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewComparaison.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.ViewComparaison.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.ViewComparaison.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.ViewComparaison.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewComparaison.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewComparaison.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewComparaison.Appearance.FilterPanel.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.FilterPanel.Options.UseForeColor = True
        Me.ViewComparaison.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.ViewComparaison.Appearance.FixedLine.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.ViewComparaison.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.ViewComparaison.Appearance.FocusedCell.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.FocusedCell.Options.UseForeColor = True
        Me.ViewComparaison.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(177, Byte), Integer))
        Me.ViewComparaison.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.ViewComparaison.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewComparaison.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewComparaison.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewComparaison.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewComparaison.Appearance.FooterPanel.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.ViewComparaison.Appearance.FooterPanel.Options.UseForeColor = True
        Me.ViewComparaison.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.ViewComparaison.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.ViewComparaison.Appearance.GroupButton.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.GroupButton.Options.UseBorderColor = True
        Me.ViewComparaison.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.ViewComparaison.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.ViewComparaison.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.ViewComparaison.Appearance.GroupFooter.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.ViewComparaison.Appearance.GroupFooter.Options.UseForeColor = True
        Me.ViewComparaison.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewComparaison.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewComparaison.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewComparaison.Appearance.GroupPanel.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.GroupPanel.Options.UseForeColor = True
        Me.ViewComparaison.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.ViewComparaison.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.ViewComparaison.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
        Me.ViewComparaison.Appearance.GroupRow.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.GroupRow.Options.UseBorderColor = True
        Me.ViewComparaison.Appearance.GroupRow.Options.UseForeColor = True
        Me.ViewComparaison.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.ViewComparaison.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.ViewComparaison.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewComparaison.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.ViewComparaison.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.ViewComparaison.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(186, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.ViewComparaison.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(134, Byte), Integer))
        Me.ViewComparaison.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.ViewComparaison.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.ViewComparaison.Appearance.HorzLine.BorderColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.ViewComparaison.Appearance.HorzLine.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.HorzLine.Options.UseBorderColor = True
        Me.ViewComparaison.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewComparaison.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewComparaison.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.ViewComparaison.Appearance.OddRow.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.OddRow.Options.UseBorderColor = True
        Me.ViewComparaison.Appearance.OddRow.Options.UseForeColor = True
        Me.ViewComparaison.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.ViewComparaison.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
        Me.ViewComparaison.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(134, Byte), Integer))
        Me.ViewComparaison.Appearance.Preview.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.Preview.Options.UseFont = True
        Me.ViewComparaison.Appearance.Preview.Options.UseForeColor = True
        Me.ViewComparaison.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewComparaison.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.ViewComparaison.Appearance.Row.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.Row.Options.UseForeColor = True
        Me.ViewComparaison.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewComparaison.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
        Me.ViewComparaison.Appearance.RowSeparator.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ViewComparaison.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black
        Me.ViewComparaison.Appearance.SelectedRow.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.SelectedRow.Options.UseForeColor = True
        Me.ViewComparaison.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
        Me.ViewComparaison.Appearance.TopNewRow.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.ViewComparaison.Appearance.VertLine.BorderColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.ViewComparaison.Appearance.VertLine.Options.UseBackColor = True
        Me.ViewComparaison.Appearance.VertLine.Options.UseBorderColor = True
        Me.ViewComparaison.GridControl = Me.LgListComparaison
        Me.ViewComparaison.Name = "ViewComparaison"
        Me.ViewComparaison.OptionsBehavior.Editable = False
        Me.ViewComparaison.OptionsBehavior.ReadOnly = True
        Me.ViewComparaison.OptionsBehavior.SmartVertScrollBar = False
        Me.ViewComparaison.OptionsCustomization.AllowColumnMoving = False
        Me.ViewComparaison.OptionsCustomization.AllowFilter = False
        Me.ViewComparaison.OptionsCustomization.AllowGroup = False
        Me.ViewComparaison.OptionsCustomization.AllowSort = False
        Me.ViewComparaison.OptionsFilter.AllowFilterEditor = False
        Me.ViewComparaison.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewComparaison.OptionsPrint.AutoWidth = False
        Me.ViewComparaison.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewComparaison.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewComparaison.OptionsView.ColumnAutoWidth = False
        Me.ViewComparaison.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewComparaison.OptionsView.EnableAppearanceOddRow = True
        Me.ViewComparaison.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewComparaison.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewComparaison.OptionsView.ShowGroupPanel = False
        Me.ViewComparaison.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'GridView4
        '
        Me.GridView4.GridControl = Me.LgListComparaison
        Me.GridView4.Name = "GridView4"
        '
        'GridControl1
        '
        Me.GridControl1.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.GridControl1.AllowDrop = True
        Me.GridControl1.AllowRestoreSelectionAndFocusedRow = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControl1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridControl1.Location = New System.Drawing.Point(2, 2)
        Me.GridControl1.MainView = Me.GridView2
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(780, 394)
        Me.GridControl1.TabIndex = 45
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2, Me.GridView1})
        Me.GridControl1.Visible = False
        '
        'GridView2
        '
        Me.GridView2.ActiveFilterEnabled = False
        Me.GridView2.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.GridView2.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.GridView2.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.GridView2.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.GridView2.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.GridView2.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.GridView2.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.GridView2.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.GridView2.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.GridView2.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.GridView2.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.GridView2.Appearance.Empty.BackColor2 = System.Drawing.Color.White
        Me.GridView2.Appearance.Empty.Options.UseBackColor = True
        Me.GridView2.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridView2.Appearance.EvenRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridView2.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.EvenRow.Options.UseBackColor = True
        Me.GridView2.Appearance.EvenRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.EvenRow.Options.UseForeColor = True
        Me.GridView2.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.GridView2.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.GridView2.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.GridView2.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.GridView2.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.GridView2.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.GridView2.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
        Me.GridView2.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.FilterPanel.Options.UseBackColor = True
        Me.GridView2.Appearance.FilterPanel.Options.UseForeColor = True
        Me.GridView2.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.GridView2.Appearance.FixedLine.Options.UseBackColor = True
        Me.GridView2.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.GridView2.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.FocusedCell.Options.UseBackColor = True
        Me.GridView2.Appearance.FocusedCell.Options.UseForeColor = True
        Me.GridView2.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(177, Byte), Integer))
        Me.GridView2.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.GridView2.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView2.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.GridView2.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.GridView2.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.FooterPanel.Options.UseBackColor = True
        Me.GridView2.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.GridView2.Appearance.FooterPanel.Options.UseForeColor = True
        Me.GridView2.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.GridView2.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.GridView2.Appearance.GroupButton.Options.UseBackColor = True
        Me.GridView2.Appearance.GroupButton.Options.UseBorderColor = True
        Me.GridView2.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.GridView2.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.GridView2.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.GroupFooter.Options.UseBackColor = True
        Me.GridView2.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.GridView2.Appearance.GroupFooter.Options.UseForeColor = True
        Me.GridView2.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.GridView2.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.GridView2.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.GroupPanel.Options.UseBackColor = True
        Me.GridView2.Appearance.GroupPanel.Options.UseForeColor = True
        Me.GridView2.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.GridView2.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.GridView2.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.GroupRow.Options.UseBackColor = True
        Me.GridView2.Appearance.GroupRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.GroupRow.Options.UseForeColor = True
        Me.GridView2.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.GridView2.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.GridView2.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.GridView2.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.GridView2.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.GridView2.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(186, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.GridView2.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(134, Byte), Integer))
        Me.GridView2.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.GridView2.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.GridView2.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.GridView2.Appearance.HorzLine.BorderColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.GridView2.Appearance.HorzLine.Options.UseBackColor = True
        Me.GridView2.Appearance.HorzLine.Options.UseBorderColor = True
        Me.GridView2.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.GridView2.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.GridView2.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.OddRow.Options.UseBackColor = True
        Me.GridView2.Appearance.OddRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.OddRow.Options.UseForeColor = True
        Me.GridView2.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.GridView2.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
        Me.GridView2.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(134, Byte), Integer))
        Me.GridView2.Appearance.Preview.Options.UseBackColor = True
        Me.GridView2.Appearance.Preview.Options.UseFont = True
        Me.GridView2.Appearance.Preview.Options.UseForeColor = True
        Me.GridView2.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.GridView2.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.Row.Options.UseBackColor = True
        Me.GridView2.Appearance.Row.Options.UseForeColor = True
        Me.GridView2.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.GridView2.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
        Me.GridView2.Appearance.RowSeparator.Options.UseBackColor = True
        Me.GridView2.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.GridView2.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black
        Me.GridView2.Appearance.SelectedRow.Options.UseBackColor = True
        Me.GridView2.Appearance.SelectedRow.Options.UseForeColor = True
        Me.GridView2.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
        Me.GridView2.Appearance.TopNewRow.Options.UseBackColor = True
        Me.GridView2.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.GridView2.Appearance.VertLine.BorderColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.GridView2.Appearance.VertLine.Options.UseBackColor = True
        Me.GridView2.Appearance.VertLine.Options.UseBorderColor = True
        Me.GridView2.GridControl = Me.GridControl1
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsBehavior.Editable = False
        Me.GridView2.OptionsBehavior.ReadOnly = True
        Me.GridView2.OptionsBehavior.SmartVertScrollBar = False
        Me.GridView2.OptionsCustomization.AllowColumnMoving = False
        Me.GridView2.OptionsCustomization.AllowFilter = False
        Me.GridView2.OptionsCustomization.AllowGroup = False
        Me.GridView2.OptionsCustomization.AllowSort = False
        Me.GridView2.OptionsFilter.AllowFilterEditor = False
        Me.GridView2.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.GridView2.OptionsPrint.AutoWidth = False
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.GridView2.OptionsView.ColumnAutoWidth = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GridView2.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.GridView2.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridView2.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        '
        'Comparaison_Etats_Financier
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 461)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Comparaison_Etats_Financier"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Comparaison des comptes"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.rdComptesNonRattaches.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdComptesRattaches.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        CType(Me.cmbRubrique.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.LgListComparaison, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewComparaison, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents rdComptesNonRattaches As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents rdComptesRattaches As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LgListComparaison As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewComparaison As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbRubrique As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btPrint As DevExpress.XtraEditors.SimpleButton
End Class
