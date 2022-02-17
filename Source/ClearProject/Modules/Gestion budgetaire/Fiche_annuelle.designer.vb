<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Fiche_annuelle
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
        Me.GridActivites = New DevExpress.XtraGrid.GridControl()
        Me.ViewActivites = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.CmbMont = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.CmbSousCompo = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.CmbCompo = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.CmbRespo = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LblNombre = New DevExpress.XtraEditors.LabelControl()
        Me.dtFin = New DevExpress.XtraEditors.DateEdit()
        Me.dtdebut = New DevExpress.XtraEditors.DateEdit()
        Me.BtAppercu = New DevExpress.XtraEditors.SimpleButton()
        Me.ChkTous = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.GridActivites, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewActivites, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.CmbMont.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbSousCompo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbCompo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbRespo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFin.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtdebut.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtdebut.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkTous.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridActivites
        '
        Me.GridActivites.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridActivites.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5)
        Me.GridActivites.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridActivites.Location = New System.Drawing.Point(0, 57)
        Me.GridActivites.MainView = Me.ViewActivites
        Me.GridActivites.Margin = New System.Windows.Forms.Padding(5)
        Me.GridActivites.Name = "GridActivites"
        Me.GridActivites.Size = New System.Drawing.Size(1895, 844)
        Me.GridActivites.TabIndex = 11
        Me.GridActivites.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewActivites})
        '
        'ViewActivites
        '
        Me.ViewActivites.ActiveFilterEnabled = False
        Me.ViewActivites.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewActivites.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewActivites.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
        Me.ViewActivites.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.ViewActivites.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.ViewActivites.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.ViewActivites.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewActivites.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewActivites.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
        Me.ViewActivites.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.ViewActivites.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.ViewActivites.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.ViewActivites.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.ViewActivites.Appearance.Empty.BackColor2 = System.Drawing.Color.White
        Me.ViewActivites.Appearance.Empty.Options.UseBackColor = True
        Me.ViewActivites.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewActivites.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.ViewActivites.Appearance.EvenRow.Options.UseBackColor = True
        Me.ViewActivites.Appearance.EvenRow.Options.UseForeColor = True
        Me.ViewActivites.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewActivites.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewActivites.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.ViewActivites.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.ViewActivites.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.ViewActivites.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.ViewActivites.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.ViewActivites.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewActivites.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewActivites.Appearance.FilterPanel.Options.UseBackColor = True
        Me.ViewActivites.Appearance.FilterPanel.Options.UseForeColor = True
        Me.ViewActivites.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(218, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.ViewActivites.Appearance.FixedLine.Options.UseBackColor = True
        Me.ViewActivites.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.ViewActivites.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.ViewActivites.Appearance.FocusedCell.Options.UseBackColor = True
        Me.ViewActivites.Appearance.FocusedCell.Options.UseForeColor = True
        Me.ViewActivites.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(91, Byte), Integer))
        Me.ViewActivites.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.ViewActivites.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewActivites.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewActivites.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewActivites.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewActivites.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewActivites.Appearance.FooterPanel.Options.UseBackColor = True
        Me.ViewActivites.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.ViewActivites.Appearance.FooterPanel.Options.UseForeColor = True
        Me.ViewActivites.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewActivites.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewActivites.Appearance.GroupButton.Options.UseBackColor = True
        Me.ViewActivites.Appearance.GroupButton.Options.UseBorderColor = True
        Me.ViewActivites.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewActivites.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewActivites.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.ViewActivites.Appearance.GroupFooter.Options.UseBackColor = True
        Me.ViewActivites.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.ViewActivites.Appearance.GroupFooter.Options.UseForeColor = True
        Me.ViewActivites.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.ViewActivites.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewActivites.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewActivites.Appearance.GroupPanel.Options.UseBackColor = True
        Me.ViewActivites.Appearance.GroupPanel.Options.UseForeColor = True
        Me.ViewActivites.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewActivites.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewActivites.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
        Me.ViewActivites.Appearance.GroupRow.Options.UseBackColor = True
        Me.ViewActivites.Appearance.GroupRow.Options.UseBorderColor = True
        Me.ViewActivites.Appearance.GroupRow.Options.UseForeColor = True
        Me.ViewActivites.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewActivites.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewActivites.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewActivites.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.ViewActivites.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.ViewActivites.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.ViewActivites.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(183, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ViewActivites.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.ViewActivites.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.ViewActivites.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.ViewActivites.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewActivites.Appearance.HorzLine.Options.UseBackColor = True
        Me.ViewActivites.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewActivites.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.ViewActivites.Appearance.OddRow.Options.UseBackColor = True
        Me.ViewActivites.Appearance.OddRow.Options.UseForeColor = True
        Me.ViewActivites.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.ViewActivites.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
        Me.ViewActivites.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(186, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.ViewActivites.Appearance.Preview.Options.UseBackColor = True
        Me.ViewActivites.Appearance.Preview.Options.UseFont = True
        Me.ViewActivites.Appearance.Preview.Options.UseForeColor = True
        Me.ViewActivites.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewActivites.Appearance.Row.BorderColor = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewActivites.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.ViewActivites.Appearance.Row.Options.UseBackColor = True
        Me.ViewActivites.Appearance.Row.Options.UseBorderColor = True
        Me.ViewActivites.Appearance.Row.Options.UseForeColor = True
        Me.ViewActivites.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.ViewActivites.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
        Me.ViewActivites.Appearance.RowSeparator.Options.UseBackColor = True
        Me.ViewActivites.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.ViewActivites.Appearance.SelectedRow.Options.UseBackColor = True
        Me.ViewActivites.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
        Me.ViewActivites.Appearance.TopNewRow.Options.UseBackColor = True
        Me.ViewActivites.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewActivites.Appearance.VertLine.Options.UseBackColor = True
        Me.ViewActivites.GridControl = Me.GridActivites
        Me.ViewActivites.Name = "ViewActivites"
        Me.ViewActivites.OptionsBehavior.Editable = False
        Me.ViewActivites.OptionsBehavior.ReadOnly = True
        Me.ViewActivites.OptionsCustomization.AllowColumnMoving = False
        Me.ViewActivites.OptionsCustomization.AllowFilter = False
        Me.ViewActivites.OptionsCustomization.AllowGroup = False
        Me.ViewActivites.OptionsCustomization.AllowSort = False
        Me.ViewActivites.OptionsFilter.AllowFilterEditor = False
        Me.ViewActivites.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewActivites.OptionsPrint.AutoWidth = False
        Me.ViewActivites.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewActivites.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewActivites.OptionsView.ColumnAutoWidth = False
        Me.ViewActivites.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewActivites.OptionsView.EnableAppearanceOddRow = True
        Me.ViewActivites.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewActivites.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewActivites.OptionsView.ShowGroupPanel = False
        Me.ViewActivites.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewActivites.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.CmbMont)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.CmbSousCompo)
        Me.PanelControl1.Controls.Add(Me.LabelControl5)
        Me.PanelControl1.Controls.Add(Me.CmbCompo)
        Me.PanelControl1.Controls.Add(Me.LabelControl6)
        Me.PanelControl1.Controls.Add(Me.CmbRespo)
        Me.PanelControl1.Controls.Add(Me.LabelControl4)
        Me.PanelControl1.Controls.Add(Me.LblNombre)
        Me.PanelControl1.Controls.Add(Me.dtFin)
        Me.PanelControl1.Controls.Add(Me.dtdebut)
        Me.PanelControl1.Controls.Add(Me.BtAppercu)
        Me.PanelControl1.Controls.Add(Me.ChkTous)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(5)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1895, 57)
        Me.PanelControl1.TabIndex = 10
        '
        'CmbMont
        '
        Me.CmbMont.EditValue = ""
        Me.CmbMont.Location = New System.Drawing.Point(1159, 14)
        Me.CmbMont.Margin = New System.Windows.Forms.Padding(5)
        Me.CmbMont.Name = "CmbMont"
        Me.CmbMont.Properties.Appearance.Options.UseTextOptions = True
        Me.CmbMont.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CmbMont.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbMont.Properties.Items.AddRange(New Object() {"Avec", "Sans"})
        Me.CmbMont.Size = New System.Drawing.Size(110, 30)
        Me.CmbMont.TabIndex = 23
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(1058, 18)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(5)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(103, 25)
        Me.LabelControl3.TabIndex = 22
        Me.LabelControl3.Text = "Ressource"
        '
        'CmbSousCompo
        '
        Me.CmbSousCompo.Location = New System.Drawing.Point(830, 16)
        Me.CmbSousCompo.Margin = New System.Windows.Forms.Padding(5)
        Me.CmbSousCompo.Name = "CmbSousCompo"
        Me.CmbSousCompo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbSousCompo.Size = New System.Drawing.Size(216, 30)
        Me.CmbSousCompo.TabIndex = 21
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Location = New System.Drawing.Point(665, 17)
        Me.LabelControl5.Margin = New System.Windows.Forms.Padding(5)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(175, 25)
        Me.LabelControl5.TabIndex = 20
        Me.LabelControl5.Text = "Sous-composante"
        '
        'CmbCompo
        '
        Me.CmbCompo.Location = New System.Drawing.Point(456, 13)
        Me.CmbCompo.Margin = New System.Windows.Forms.Padding(5)
        Me.CmbCompo.Name = "CmbCompo"
        Me.CmbCompo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbCompo.Size = New System.Drawing.Size(197, 30)
        Me.CmbCompo.TabIndex = 19
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6.Location = New System.Drawing.Point(337, 17)
        Me.LabelControl6.Margin = New System.Windows.Forms.Padding(5)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(126, 25)
        Me.LabelControl6.TabIndex = 18
        Me.LabelControl6.Text = "Composante"
        '
        'CmbRespo
        '
        Me.CmbRespo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmbRespo.EditValue = ""
        Me.CmbRespo.Location = New System.Drawing.Point(1402, 14)
        Me.CmbRespo.Margin = New System.Windows.Forms.Padding(5)
        Me.CmbRespo.Name = "CmbRespo"
        Me.CmbRespo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbRespo.Size = New System.Drawing.Size(17, 30)
        Me.CmbRespo.TabIndex = 17
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Location = New System.Drawing.Point(1285, 17)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(5)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(125, 25)
        Me.LabelControl4.TabIndex = 16
        Me.LabelControl4.Text = "Responsable"
        '
        'LblNombre
        '
        Me.LblNombre.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblNombre.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNombre.Location = New System.Drawing.Point(1429, 17)
        Me.LblNombre.Margin = New System.Windows.Forms.Padding(5)
        Me.LblNombre.Name = "LblNombre"
        Me.LblNombre.Size = New System.Drawing.Size(18, 25)
        Me.LblNombre.TabIndex = 15
        Me.LblNombre.Text = "..."
        '
        'dtFin
        '
        Me.dtFin.EditValue = Nothing
        Me.dtFin.Location = New System.Drawing.Point(171, 13)
        Me.dtFin.Margin = New System.Windows.Forms.Padding(5)
        Me.dtFin.Name = "dtFin"
        Me.dtFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFin.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.dtFin.Size = New System.Drawing.Size(155, 30)
        Me.dtFin.TabIndex = 12
        '
        'dtdebut
        '
        Me.dtdebut.EditValue = Nothing
        Me.dtdebut.Location = New System.Drawing.Point(6, 13)
        Me.dtdebut.Margin = New System.Windows.Forms.Padding(5)
        Me.dtdebut.Name = "dtdebut"
        Me.dtdebut.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtdebut.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.dtdebut.Size = New System.Drawing.Size(155, 30)
        Me.dtdebut.TabIndex = 11
        '
        'BtAppercu
        '
        Me.BtAppercu.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtAppercu.Appearance.Options.UseFont = True
        Me.BtAppercu.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtAppercu.Image = Global.ClearProject.My.Resources.Resources.Ribbon_New_16x16
        Me.BtAppercu.Location = New System.Drawing.Point(1756, 2)
        Me.BtAppercu.Margin = New System.Windows.Forms.Padding(5)
        Me.BtAppercu.Name = "BtAppercu"
        Me.BtAppercu.Size = New System.Drawing.Size(137, 53)
        Me.BtAppercu.TabIndex = 4
        Me.BtAppercu.Text = "Imprimer"
        '
        'ChkTous
        '
        Me.ChkTous.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChkTous.Enabled = False
        Me.ChkTous.Location = New System.Drawing.Point(1666, 14)
        Me.ChkTous.Margin = New System.Windows.Forms.Padding(5)
        Me.ChkTous.Name = "ChkTous"
        Me.ChkTous.Properties.Caption = "Tous"
        Me.ChkTous.Size = New System.Drawing.Size(80, 28)
        Me.ChkTous.TabIndex = 10
        '
        'Fiche_annuelle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1895, 901)
        Me.Controls.Add(Me.GridActivites)
        Me.Controls.Add(Me.PanelControl1)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "Fiche_annuelle"
        Me.Text = "Edition des Fiches d'activité Annuelles"
        CType(Me.GridActivites, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewActivites, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.CmbMont.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbSousCompo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbCompo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbRespo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFin.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtdebut.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtdebut.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkTous.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridActivites As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewActivites As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtAppercu As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ChkTous As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents dtFin As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtdebut As DevExpress.XtraEditors.DateEdit
    Friend WithEvents CmbRespo As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblNombre As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CmbMont As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CmbSousCompo As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CmbCompo As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
End Class
