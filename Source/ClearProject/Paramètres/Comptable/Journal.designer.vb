<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Journal
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
        Me.gcjournal = New DevExpress.XtraEditors.GroupControl()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.LgListJournaux = New DevExpress.XtraGrid.GridControl()
        Me.ViewJournaux = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btsuppr = New DevExpress.XtraEditors.SimpleButton()
        Me.btnewj = New DevExpress.XtraEditors.SimpleButton()
        Me.btannulerj = New DevExpress.XtraEditors.SimpleButton()
        Me.btenregisterj = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Combjp = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.combtj = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkApplyActivity = New DevExpress.XtraEditors.CheckEdit()
        Me.combsc = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.checkasscpt = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.txtlibj = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.txtcodej = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.gcjournal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcjournal.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.LgListJournaux, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewJournaux, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Combjp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.combtj.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.chkApplyActivity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.combsc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkasscpt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtlibj.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcodej.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gcjournal
        '
        Me.gcjournal.Controls.Add(Me.GroupBox4)
        Me.gcjournal.Controls.Add(Me.GroupBox5)
        Me.gcjournal.Controls.Add(Me.GroupBox1)
        Me.gcjournal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcjournal.Location = New System.Drawing.Point(0, 0)
        Me.gcjournal.Name = "gcjournal"
        Me.gcjournal.Size = New System.Drawing.Size(964, 270)
        Me.gcjournal.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.LgListJournaux)
        Me.GroupBox4.Location = New System.Drawing.Point(438, 22)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(525, 240)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        '
        'LgListJournaux
        '
        Me.LgListJournaux.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.LgListJournaux.AllowDrop = True
        Me.LgListJournaux.AllowRestoreSelectionAndFocusedRow = DevExpress.Utils.DefaultBoolean.[True]
        Me.LgListJournaux.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LgListJournaux.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LgListJournaux.Location = New System.Drawing.Point(3, 17)
        Me.LgListJournaux.MainView = Me.ViewJournaux
        Me.LgListJournaux.Name = "LgListJournaux"
        Me.LgListJournaux.Size = New System.Drawing.Size(519, 220)
        Me.LgListJournaux.TabIndex = 42
        Me.LgListJournaux.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewJournaux, Me.GridView1})
        '
        'ViewJournaux
        '
        Me.ViewJournaux.ActiveFilterEnabled = False
        Me.ViewJournaux.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewJournaux.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewJournaux.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
        Me.ViewJournaux.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.ViewJournaux.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.ViewJournaux.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.ViewJournaux.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.ViewJournaux.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
        Me.ViewJournaux.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.ViewJournaux.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.ViewJournaux.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewJournaux.Appearance.Empty.BackColor2 = System.Drawing.Color.White
        Me.ViewJournaux.Appearance.Empty.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ViewJournaux.Appearance.EvenRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ViewJournaux.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.ViewJournaux.Appearance.EvenRow.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.EvenRow.Options.UseBorderColor = True
        Me.ViewJournaux.Appearance.EvenRow.Options.UseForeColor = True
        Me.ViewJournaux.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewJournaux.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewJournaux.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.ViewJournaux.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.ViewJournaux.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.ViewJournaux.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewJournaux.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewJournaux.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewJournaux.Appearance.FilterPanel.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.FilterPanel.Options.UseForeColor = True
        Me.ViewJournaux.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.ViewJournaux.Appearance.FixedLine.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.ViewJournaux.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.ViewJournaux.Appearance.FocusedCell.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.FocusedCell.Options.UseForeColor = True
        Me.ViewJournaux.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(177, Byte), Integer))
        Me.ViewJournaux.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.ViewJournaux.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewJournaux.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewJournaux.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewJournaux.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewJournaux.Appearance.FooterPanel.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.ViewJournaux.Appearance.FooterPanel.Options.UseForeColor = True
        Me.ViewJournaux.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.ViewJournaux.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.ViewJournaux.Appearance.GroupButton.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.GroupButton.Options.UseBorderColor = True
        Me.ViewJournaux.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.ViewJournaux.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.ViewJournaux.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.ViewJournaux.Appearance.GroupFooter.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.ViewJournaux.Appearance.GroupFooter.Options.UseForeColor = True
        Me.ViewJournaux.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewJournaux.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewJournaux.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewJournaux.Appearance.GroupPanel.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.GroupPanel.Options.UseForeColor = True
        Me.ViewJournaux.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.ViewJournaux.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.ViewJournaux.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
        Me.ViewJournaux.Appearance.GroupRow.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.GroupRow.Options.UseBorderColor = True
        Me.ViewJournaux.Appearance.GroupRow.Options.UseForeColor = True
        Me.ViewJournaux.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.ViewJournaux.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.ViewJournaux.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewJournaux.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.ViewJournaux.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.ViewJournaux.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(186, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.ViewJournaux.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(134, Byte), Integer))
        Me.ViewJournaux.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.ViewJournaux.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.ViewJournaux.Appearance.HorzLine.BorderColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.ViewJournaux.Appearance.HorzLine.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.HorzLine.Options.UseBorderColor = True
        Me.ViewJournaux.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewJournaux.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewJournaux.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.ViewJournaux.Appearance.OddRow.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.OddRow.Options.UseBorderColor = True
        Me.ViewJournaux.Appearance.OddRow.Options.UseForeColor = True
        Me.ViewJournaux.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.ViewJournaux.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
        Me.ViewJournaux.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(134, Byte), Integer))
        Me.ViewJournaux.Appearance.Preview.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.Preview.Options.UseFont = True
        Me.ViewJournaux.Appearance.Preview.Options.UseForeColor = True
        Me.ViewJournaux.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewJournaux.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.ViewJournaux.Appearance.Row.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.Row.Options.UseForeColor = True
        Me.ViewJournaux.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewJournaux.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
        Me.ViewJournaux.Appearance.RowSeparator.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ViewJournaux.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black
        Me.ViewJournaux.Appearance.SelectedRow.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.SelectedRow.Options.UseForeColor = True
        Me.ViewJournaux.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
        Me.ViewJournaux.Appearance.TopNewRow.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.ViewJournaux.Appearance.VertLine.BorderColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.ViewJournaux.Appearance.VertLine.Options.UseBackColor = True
        Me.ViewJournaux.Appearance.VertLine.Options.UseBorderColor = True
        Me.ViewJournaux.GridControl = Me.LgListJournaux
        Me.ViewJournaux.Name = "ViewJournaux"
        Me.ViewJournaux.OptionsBehavior.Editable = False
        Me.ViewJournaux.OptionsBehavior.ReadOnly = True
        Me.ViewJournaux.OptionsBehavior.SmartVertScrollBar = False
        Me.ViewJournaux.OptionsCustomization.AllowColumnMoving = False
        Me.ViewJournaux.OptionsCustomization.AllowFilter = False
        Me.ViewJournaux.OptionsCustomization.AllowGroup = False
        Me.ViewJournaux.OptionsCustomization.AllowSort = False
        Me.ViewJournaux.OptionsFilter.AllowFilterEditor = False
        Me.ViewJournaux.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewJournaux.OptionsPrint.AutoWidth = False
        Me.ViewJournaux.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewJournaux.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewJournaux.OptionsView.ColumnAutoWidth = False
        Me.ViewJournaux.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewJournaux.OptionsView.EnableAppearanceOddRow = True
        Me.ViewJournaux.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewJournaux.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewJournaux.OptionsView.ShowGroupPanel = False
        Me.ViewJournaux.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewJournaux.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.LgListJournaux
        Me.GridView1.Name = "GridView1"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btsuppr)
        Me.GroupBox5.Controls.Add(Me.btnewj)
        Me.GroupBox5.Controls.Add(Me.btannulerj)
        Me.GroupBox5.Controls.Add(Me.btenregisterj)
        Me.GroupBox5.Location = New System.Drawing.Point(5, 22)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(428, 57)
        Me.GroupBox5.TabIndex = 8
        Me.GroupBox5.TabStop = False
        '
        'btsuppr
        '
        Me.btsuppr.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btsuppr.Image = Global.ClearProject.My.Resources.Resources.supprimer
        Me.btsuppr.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btsuppr.Location = New System.Drawing.Point(336, 11)
        Me.btsuppr.Name = "btsuppr"
        Me.btsuppr.Size = New System.Drawing.Size(42, 39)
        Me.btsuppr.TabIndex = 3
        Me.btsuppr.ToolTip = "Supprimer"
        '
        'btnewj
        '
        Me.btnewj.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnewj.Image = Global.ClearProject.My.Resources.Resources.Ribbon_New_32x32
        Me.btnewj.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btnewj.Location = New System.Drawing.Point(248, 11)
        Me.btnewj.Name = "btnewj"
        Me.btnewj.Size = New System.Drawing.Size(42, 39)
        Me.btnewj.TabIndex = 2
        Me.btnewj.ToolTip = "Nouveau"
        '
        'btannulerj
        '
        Me.btannulerj.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btannulerj.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_32
        Me.btannulerj.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btannulerj.Location = New System.Drawing.Point(292, 11)
        Me.btannulerj.Name = "btannulerj"
        Me.btannulerj.Size = New System.Drawing.Size(42, 39)
        Me.btannulerj.TabIndex = 1
        Me.btannulerj.ToolTip = "Initialiser"
        '
        'btenregisterj
        '
        Me.btenregisterj.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btenregisterj.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_32x32
        Me.btenregisterj.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btenregisterj.Location = New System.Drawing.Point(380, 11)
        Me.btenregisterj.Name = "btenregisterj"
        Me.btenregisterj.Size = New System.Drawing.Size(42, 39)
        Me.btenregisterj.TabIndex = 0
        Me.btenregisterj.ToolTip = "Enregistrer"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Combjp)
        Me.GroupBox1.Controls.Add(Me.combtj)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.LabelControl7)
        Me.GroupBox1.Controls.Add(Me.txtlibj)
        Me.GroupBox1.Controls.Add(Me.LabelControl1)
        Me.GroupBox1.Controls.Add(Me.LabelControl2)
        Me.GroupBox1.Controls.Add(Me.txtcodej)
        Me.GroupBox1.Controls.Add(Me.LabelControl3)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 81)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(428, 184)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        '
        'Combjp
        '
        Me.Combjp.Enabled = False
        Me.Combjp.Location = New System.Drawing.Point(141, 88)
        Me.Combjp.Margin = New System.Windows.Forms.Padding(2)
        Me.Combjp.Name = "Combjp"
        Me.Combjp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Combjp.Properties.Items.AddRange(New Object() {"Manuel", "Automatique"})
        Me.Combjp.Size = New System.Drawing.Size(274, 20)
        Me.Combjp.TabIndex = 6
        '
        'combtj
        '
        Me.combtj.Enabled = False
        Me.combtj.Location = New System.Drawing.Point(241, 15)
        Me.combtj.Margin = New System.Windows.Forms.Padding(2)
        Me.combtj.Name = "combtj"
        Me.combtj.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.combtj.Size = New System.Drawing.Size(175, 20)
        Me.combtj.TabIndex = 4
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkApplyActivity)
        Me.GroupBox2.Controls.Add(Me.combsc)
        Me.GroupBox2.Controls.Add(Me.checkasscpt)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(3, 109)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(422, 72)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        '
        'chkApplyActivity
        '
        Me.chkApplyActivity.Location = New System.Drawing.Point(7, 50)
        Me.chkApplyActivity.Name = "chkApplyActivity"
        Me.chkApplyActivity.Properties.Caption = "Appliquer une activité"
        Me.chkApplyActivity.Size = New System.Drawing.Size(146, 19)
        Me.chkApplyActivity.TabIndex = 12
        Me.chkApplyActivity.Visible = False
        '
        'combsc
        '
        Me.combsc.Enabled = False
        Me.combsc.Location = New System.Drawing.Point(152, 16)
        Me.combsc.Margin = New System.Windows.Forms.Padding(2)
        Me.combsc.Name = "combsc"
        Me.combsc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.combsc.Size = New System.Drawing.Size(260, 20)
        Me.combsc.TabIndex = 11
        '
        'checkasscpt
        '
        Me.checkasscpt.Enabled = False
        Me.checkasscpt.Location = New System.Drawing.Point(6, 17)
        Me.checkasscpt.Name = "checkasscpt"
        Me.checkasscpt.Properties.Caption = "Associer Compte Général"
        Me.checkasscpt.Size = New System.Drawing.Size(146, 19)
        Me.checkasscpt.TabIndex = 0
        '
        'LabelControl7
        '
        Me.LabelControl7.Location = New System.Drawing.Point(11, 90)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(118, 13)
        Me.LabelControl7.TabIndex = 10
        Me.LabelControl7.Text = "Numérotation des pièces"
        '
        'txtlibj
        '
        Me.txtlibj.Enabled = False
        Me.txtlibj.Location = New System.Drawing.Point(43, 47)
        Me.txtlibj.Name = "txtlibj"
        Me.txtlibj.Properties.MaxLength = 250
        Me.txtlibj.Size = New System.Drawing.Size(373, 20)
        Me.txtlibj.TabIndex = 5
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(10, 17)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(25, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Code"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(210, 18)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(24, 13)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Type"
        '
        'txtcodej
        '
        Me.txtcodej.Enabled = False
        Me.txtcodej.Location = New System.Drawing.Point(43, 16)
        Me.txtcodej.Name = "txtcodej"
        Me.txtcodej.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcodej.Properties.Mask.EditMask = "[0-9A-Z]+"
        Me.txtcodej.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.txtcodej.Properties.Mask.ShowPlaceHolders = False
        Me.txtcodej.Properties.MaxLength = 7
        Me.txtcodej.Size = New System.Drawing.Size(156, 20)
        Me.txtcodej.TabIndex = 3
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(10, 49)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(29, 13)
        Me.LabelControl3.TabIndex = 2
        Me.LabelControl3.Text = "Libellé"
        '
        'Journal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(964, 270)
        Me.Controls.Add(Me.gcjournal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Journal"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Journal"
        CType(Me.gcjournal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcjournal.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.LgListJournaux, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewJournaux, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.Combjp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.combtj.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.chkApplyActivity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.combsc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkasscpt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtlibj.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcodej.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gcjournal As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtlibj As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtcodej As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents checkasscpt As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents btannulerj As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btenregisterj As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnewj As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsuppr As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LgListJournaux As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewJournaux As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Combjp As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents combtj As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents combsc As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents chkApplyActivity As DevExpress.XtraEditors.CheckEdit
End Class
