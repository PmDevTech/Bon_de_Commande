<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Suivibugetaire_Sigfip
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
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblNbLign = New DevExpress.XtraEditors.LabelControl()
        Me.dpFin = New DevExpress.XtraEditors.DateEdit()
        Me.dpDebut = New DevExpress.XtraEditors.DateEdit()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LgListBudgetCompte = New DevExpress.XtraGrid.GridControl()
        Me.ViewBudgetCompte = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.CmbRech = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.CmbCritere = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbBudget = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.LblNombre = New DevExpress.XtraEditors.LabelControl()
        Me.BtAppercu = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        CType(Me.dpFin.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dpFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dpDebut.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dpDebut.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LgListBudgetCompte, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewBudgetCompte, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbRech.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbCritere.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBudget.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListBox2
        '
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.Location = New System.Drawing.Point(533, 125)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(50, 17)
        Me.ListBox2.TabIndex = 43
        Me.ListBox2.Visible = False
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(463, 124)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(50, 17)
        Me.ListBox1.TabIndex = 42
        Me.ListBox1.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(180, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "Période"
        '
        'lblNbLign
        '
        Me.lblNbLign.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNbLign.Location = New System.Drawing.Point(25, 556)
        Me.lblNbLign.Name = "lblNbLign"
        Me.lblNbLign.Size = New System.Drawing.Size(0, 15)
        Me.lblNbLign.TabIndex = 45
        '
        'dpFin
        '
        Me.dpFin.EditValue = Nothing
        Me.dpFin.Location = New System.Drawing.Point(153, 24)
        Me.dpFin.Name = "dpFin"
        Me.dpFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dpFin.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.dpFin.Size = New System.Drawing.Size(107, 20)
        Me.dpFin.TabIndex = 48
        '
        'dpDebut
        '
        Me.dpDebut.EditValue = Nothing
        Me.dpDebut.Location = New System.Drawing.Point(12, 24)
        Me.dpDebut.Name = "dpDebut"
        Me.dpDebut.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dpDebut.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.dpDebut.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.dpDebut.Size = New System.Drawing.Size(107, 20)
        Me.dpDebut.TabIndex = 47
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(125, 27)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(22, 13)
        Me.Label5.TabIndex = 46
        Me.Label5.Text = "Au"
        '
        'LgListBudgetCompte
        '
        Me.LgListBudgetCompte.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LgListBudgetCompte.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LgListBudgetCompte.Location = New System.Drawing.Point(2, 2)
        Me.LgListBudgetCompte.MainView = Me.ViewBudgetCompte
        Me.LgListBudgetCompte.Name = "LgListBudgetCompte"
        Me.LgListBudgetCompte.Size = New System.Drawing.Size(901, 366)
        Me.LgListBudgetCompte.TabIndex = 59
        Me.LgListBudgetCompte.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewBudgetCompte})
        '
        'ViewBudgetCompte
        '
        Me.ViewBudgetCompte.ActiveFilterEnabled = False
        Me.ViewBudgetCompte.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
        Me.ViewBudgetCompte.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.ViewBudgetCompte.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.ViewBudgetCompte.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
        Me.ViewBudgetCompte.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.ViewBudgetCompte.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.ViewBudgetCompte.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.Empty.BackColor2 = System.Drawing.Color.White
        Me.ViewBudgetCompte.Appearance.Empty.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.ViewBudgetCompte.Appearance.EvenRow.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.EvenRow.Options.UseForeColor = True
        Me.ViewBudgetCompte.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.ViewBudgetCompte.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.ViewBudgetCompte.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.ViewBudgetCompte.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewBudgetCompte.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewBudgetCompte.Appearance.FilterPanel.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.FilterPanel.Options.UseForeColor = True
        Me.ViewBudgetCompte.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(218, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.FixedLine.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.ViewBudgetCompte.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.ViewBudgetCompte.Appearance.FocusedCell.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.FocusedCell.Options.UseForeColor = True
        Me.ViewBudgetCompte.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(91, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.ViewBudgetCompte.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewBudgetCompte.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewBudgetCompte.Appearance.FooterPanel.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.ViewBudgetCompte.Appearance.FooterPanel.Options.UseForeColor = True
        Me.ViewBudgetCompte.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.GroupButton.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.GroupButton.Options.UseBorderColor = True
        Me.ViewBudgetCompte.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.ViewBudgetCompte.Appearance.GroupFooter.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.ViewBudgetCompte.Appearance.GroupFooter.Options.UseForeColor = True
        Me.ViewBudgetCompte.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewBudgetCompte.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewBudgetCompte.Appearance.GroupPanel.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.GroupPanel.Options.UseForeColor = True
        Me.ViewBudgetCompte.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
        Me.ViewBudgetCompte.Appearance.GroupRow.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.GroupRow.Options.UseBorderColor = True
        Me.ViewBudgetCompte.Appearance.GroupRow.Options.UseForeColor = True
        Me.ViewBudgetCompte.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewBudgetCompte.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.ViewBudgetCompte.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.ViewBudgetCompte.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(183, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.ViewBudgetCompte.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.HorzLine.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.ViewBudgetCompte.Appearance.OddRow.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.OddRow.Options.UseForeColor = True
        Me.ViewBudgetCompte.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
        Me.ViewBudgetCompte.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(186, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.Preview.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.Preview.Options.UseFont = True
        Me.ViewBudgetCompte.Appearance.Preview.Options.UseForeColor = True
        Me.ViewBudgetCompte.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.Row.BorderColor = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.ViewBudgetCompte.Appearance.Row.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.Row.Options.UseBorderColor = True
        Me.ViewBudgetCompte.Appearance.Row.Options.UseForeColor = True
        Me.ViewBudgetCompte.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
        Me.ViewBudgetCompte.Appearance.RowSeparator.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.SelectedRow.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
        Me.ViewBudgetCompte.Appearance.TopNewRow.Options.UseBackColor = True
        Me.ViewBudgetCompte.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ViewBudgetCompte.Appearance.VertLine.Options.UseBackColor = True
        Me.ViewBudgetCompte.GridControl = Me.LgListBudgetCompte
        Me.ViewBudgetCompte.Name = "ViewBudgetCompte"
        Me.ViewBudgetCompte.OptionsBehavior.Editable = False
        Me.ViewBudgetCompte.OptionsBehavior.ReadOnly = True
        Me.ViewBudgetCompte.OptionsCustomization.AllowColumnMoving = False
        Me.ViewBudgetCompte.OptionsCustomization.AllowFilter = False
        Me.ViewBudgetCompte.OptionsCustomization.AllowGroup = False
        Me.ViewBudgetCompte.OptionsCustomization.AllowSort = False
        Me.ViewBudgetCompte.OptionsFilter.AllowFilterEditor = False
        Me.ViewBudgetCompte.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewBudgetCompte.OptionsPrint.AutoWidth = False
        Me.ViewBudgetCompte.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewBudgetCompte.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewBudgetCompte.OptionsView.ColumnAutoWidth = False
        Me.ViewBudgetCompte.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewBudgetCompte.OptionsView.EnableAppearanceOddRow = True
        Me.ViewBudgetCompte.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewBudgetCompte.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewBudgetCompte.OptionsView.ShowGroupPanel = False
        Me.ViewBudgetCompte.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewBudgetCompte.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(29, 327)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(0, 15)
        Me.LabelControl1.TabIndex = 58
        '
        'CmbRech
        '
        Me.CmbRech.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmbRech.Location = New System.Drawing.Point(762, 24)
        Me.CmbRech.Name = "CmbRech"
        Me.CmbRech.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbRech.Size = New System.Drawing.Size(33, 20)
        Me.CmbRech.TabIndex = 57
        '
        'CmbCritere
        '
        Me.CmbCritere.Location = New System.Drawing.Point(519, 24)
        Me.CmbCritere.Name = "CmbCritere"
        Me.CmbCritere.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbCritere.Properties.Items.AddRange(New Object() {"Par Projet", "Par Composante", "Par Sous Composante", "Par Activité", "Par Bailleur", "Par Convention"})
        Me.CmbCritere.Size = New System.Drawing.Size(237, 20)
        Me.CmbCritere.TabIndex = 56
        '
        'cmbBudget
        '
        Me.cmbBudget.Location = New System.Drawing.Point(276, 24)
        Me.cmbBudget.Name = "cmbBudget"
        Me.cmbBudget.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBudget.Properties.Items.AddRange(New Object() {"Engagements", "Dépenses"})
        Me.cmbBudget.Size = New System.Drawing.Size(237, 20)
        Me.cmbBudget.TabIndex = 55
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(26, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 15)
        Me.Label3.TabIndex = 53
        Me.Label3.Text = "Sélectionner"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(388, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(145, 15)
        Me.Label4.TabIndex = 52
        Me.Label4.Text = "Critères de recherche"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(258, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "Sélectionner"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.LblNombre)
        Me.GroupControl1.Controls.Add(Me.BtAppercu)
        Me.GroupControl1.Controls.Add(Me.dpDebut)
        Me.GroupControl1.Controls.Add(Me.Label5)
        Me.GroupControl1.Controls.Add(Me.Label3)
        Me.GroupControl1.Controls.Add(Me.dpFin)
        Me.GroupControl1.Controls.Add(Me.Label4)
        Me.GroupControl1.Controls.Add(Me.Label2)
        Me.GroupControl1.Controls.Add(Me.CmbRech)
        Me.GroupControl1.Controls.Add(Me.Label1)
        Me.GroupControl1.Controls.Add(Me.cmbBudget)
        Me.GroupControl1.Controls.Add(Me.CmbCritere)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(905, 53)
        Me.GroupControl1.TabIndex = 60
        Me.GroupControl1.Text = "Critères de recherche"
        '
        'LblNombre
        '
        Me.LblNombre.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblNombre.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNombre.Location = New System.Drawing.Point(781, 25)
        Me.LblNombre.Name = "LblNombre"
        Me.LblNombre.Size = New System.Drawing.Size(9, 15)
        Me.LblNombre.TabIndex = 59
        Me.LblNombre.Text = "..."
        Me.LblNombre.Visible = False
        '
        'BtAppercu
        '
        Me.BtAppercu.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtAppercu.Appearance.Options.UseFont = True
        Me.BtAppercu.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtAppercu.Image = Global.ClearProject.My.Resources.Resources.Ribbon_New_16x16
        Me.BtAppercu.Location = New System.Drawing.Point(806, 21)
        Me.BtAppercu.Name = "BtAppercu"
        Me.BtAppercu.Size = New System.Drawing.Size(97, 30)
        Me.BtAppercu.TabIndex = 58
        Me.BtAppercu.Text = "Imprimer"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.LgListBudgetCompte)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 53)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(905, 370)
        Me.PanelControl1.TabIndex = 61
        '
        'Suivibugetaire_Sigfip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(905, 423)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.lblNbLign)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.ListBox1)
        Me.Name = "Suivibugetaire_Sigfip"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Suivi Bugetaire Par Sigfip"
        CType(Me.dpFin.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dpFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dpDebut.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dpDebut.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LgListBudgetCompte, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewBudgetCompte, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbRech.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbCritere.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBudget.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblNbLign As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dpFin As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dpDebut As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents LgListBudgetCompte As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewBudgetCompte As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CmbRech As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents CmbCritere As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbBudget As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LblNombre As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtAppercu As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
End Class
