<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ResponsableEtape
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
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.LgListRespoEtape = New DevExpress.XtraGrid.GridControl()
        Me.ViewRespoEtape = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl4 = New DevExpress.XtraEditors.PanelControl()
        Me.Checktous = New DevExpress.XtraEditors.CheckEdit()
        Me.BtImprimer = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtRechecher = New DevExpress.XtraEditors.TextEdit()
        Me.BtSupprimer = New DevExpress.XtraEditors.SimpleButton()
        Me.BtActualiser = New DevExpress.XtraEditors.SimpleButton()
        Me.BtModifier = New DevExpress.XtraEditors.SimpleButton()
        Me.BtAjouter = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.TxtPage = New DevExpress.XtraEditors.LabelControl()
        Me.BtLast = New DevExpress.XtraEditors.SimpleButton()
        Me.BtNext = New DevExpress.XtraEditors.SimpleButton()
        Me.BtPrev = New DevExpress.XtraEditors.SimpleButton()
        Me.BtFrist = New DevExpress.XtraEditors.SimpleButton()
        Me.CmbPageSize = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AjoutPlanDeTiersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModificationPlanDeTiersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SuppressionCompteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImprimerCompteDeTiersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.LgListRespoEtape, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewRespoEtape, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl4.SuspendLayout()
        CType(Me.Checktous.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtRechecher.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.CmbPageSize.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.PanelControl3)
        Me.PanelControl1.Controls.Add(Me.PanelControl4)
        Me.PanelControl1.Controls.Add(Me.PanelControl2)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(784, 561)
        Me.PanelControl1.TabIndex = 0
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.LgListRespoEtape)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl3.Location = New System.Drawing.Point(2, 36)
        Me.PanelControl3.Margin = New System.Windows.Forms.Padding(2)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(780, 491)
        Me.PanelControl3.TabIndex = 46
        '
        'LgListRespoEtape
        '
        Me.LgListRespoEtape.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.LgListRespoEtape.AllowDrop = True
        Me.LgListRespoEtape.AllowRestoreSelectionAndFocusedRow = DevExpress.Utils.DefaultBoolean.[True]
        Me.LgListRespoEtape.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LgListRespoEtape.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LgListRespoEtape.Location = New System.Drawing.Point(2, 2)
        Me.LgListRespoEtape.MainView = Me.ViewRespoEtape
        Me.LgListRespoEtape.Name = "LgListRespoEtape"
        Me.LgListRespoEtape.Size = New System.Drawing.Size(776, 487)
        Me.LgListRespoEtape.TabIndex = 43
        Me.LgListRespoEtape.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewRespoEtape, Me.GridView1})
        '
        'ViewRespoEtape
        '
        Me.ViewRespoEtape.ActiveFilterEnabled = False
        Me.ViewRespoEtape.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.Silver
        Me.ViewRespoEtape.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewRespoEtape.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.Silver
        Me.ViewRespoEtape.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
        Me.ViewRespoEtape.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.ViewRespoEtape.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.ViewRespoEtape.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewRespoEtape.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.ViewRespoEtape.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewRespoEtape.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
        Me.ViewRespoEtape.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.ViewRespoEtape.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.ViewRespoEtape.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewRespoEtape.Appearance.Empty.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.EvenRow.BackColor = System.Drawing.Color.Silver
        Me.ViewRespoEtape.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite
        Me.ViewRespoEtape.Appearance.EvenRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewRespoEtape.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.ViewRespoEtape.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewRespoEtape.Appearance.EvenRow.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.EvenRow.Options.UseFont = True
        Me.ViewRespoEtape.Appearance.EvenRow.Options.UseForeColor = True
        Me.ViewRespoEtape.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewRespoEtape.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.ViewRespoEtape.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewRespoEtape.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.ViewRespoEtape.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewRespoEtape.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.ViewRespoEtape.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.ViewRespoEtape.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.ViewRespoEtape.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewRespoEtape.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
        Me.ViewRespoEtape.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewRespoEtape.Appearance.FilterPanel.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.FilterPanel.Options.UseForeColor = True
        Me.ViewRespoEtape.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.ViewRespoEtape.Appearance.FixedLine.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.ViewRespoEtape.Appearance.FocusedCell.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewRespoEtape.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Navy
        Me.ViewRespoEtape.Appearance.FocusedCell.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.FocusedCell.Options.UseFont = True
        Me.ViewRespoEtape.Appearance.FocusedCell.Options.UseForeColor = True
        Me.ViewRespoEtape.Appearance.FocusedRow.BackColor = System.Drawing.Color.Navy
        Me.ViewRespoEtape.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.Navy
        Me.ViewRespoEtape.Appearance.FocusedRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewRespoEtape.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.ViewRespoEtape.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.FocusedRow.Options.UseFont = True
        Me.ViewRespoEtape.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewRespoEtape.Appearance.FooterPanel.BackColor = System.Drawing.Color.Silver
        Me.ViewRespoEtape.Appearance.FooterPanel.BorderColor = System.Drawing.Color.Silver
        Me.ViewRespoEtape.Appearance.FooterPanel.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewRespoEtape.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewRespoEtape.Appearance.FooterPanel.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.ViewRespoEtape.Appearance.FooterPanel.Options.UseFont = True
        Me.ViewRespoEtape.Appearance.FooterPanel.Options.UseForeColor = True
        Me.ViewRespoEtape.Appearance.GroupButton.BackColor = System.Drawing.Color.Silver
        Me.ViewRespoEtape.Appearance.GroupButton.BorderColor = System.Drawing.Color.Silver
        Me.ViewRespoEtape.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
        Me.ViewRespoEtape.Appearance.GroupButton.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.GroupButton.Options.UseBorderColor = True
        Me.ViewRespoEtape.Appearance.GroupButton.Options.UseForeColor = True
        Me.ViewRespoEtape.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ViewRespoEtape.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ViewRespoEtape.Appearance.GroupFooter.Font = New System.Drawing.Font("Times New Roman", 8.0!)
        Me.ViewRespoEtape.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.ViewRespoEtape.Appearance.GroupFooter.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.ViewRespoEtape.Appearance.GroupFooter.Options.UseFont = True
        Me.ViewRespoEtape.Appearance.GroupFooter.Options.UseForeColor = True
        Me.ViewRespoEtape.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.ViewRespoEtape.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewRespoEtape.Appearance.GroupPanel.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ViewRespoEtape.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
        Me.ViewRespoEtape.Appearance.GroupPanel.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.GroupPanel.Options.UseFont = True
        Me.ViewRespoEtape.Appearance.GroupPanel.Options.UseForeColor = True
        Me.ViewRespoEtape.Appearance.GroupRow.BackColor = System.Drawing.Color.Gray
        Me.ViewRespoEtape.Appearance.GroupRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewRespoEtape.Appearance.GroupRow.ForeColor = System.Drawing.Color.Silver
        Me.ViewRespoEtape.Appearance.GroupRow.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.GroupRow.Options.UseFont = True
        Me.ViewRespoEtape.Appearance.GroupRow.Options.UseForeColor = True
        Me.ViewRespoEtape.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Silver
        Me.ViewRespoEtape.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Silver
        Me.ViewRespoEtape.Appearance.HeaderPanel.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ViewRespoEtape.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewRespoEtape.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.ViewRespoEtape.Appearance.HeaderPanel.Options.UseFont = True
        Me.ViewRespoEtape.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.ViewRespoEtape.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
        Me.ViewRespoEtape.Appearance.HideSelectionRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewRespoEtape.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewRespoEtape.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.HideSelectionRow.Options.UseFont = True
        Me.ViewRespoEtape.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.ViewRespoEtape.Appearance.HorzLine.BackColor = System.Drawing.Color.Silver
        Me.ViewRespoEtape.Appearance.HorzLine.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.OddRow.BackColor = System.Drawing.Color.White
        Me.ViewRespoEtape.Appearance.OddRow.BackColor2 = System.Drawing.Color.White
        Me.ViewRespoEtape.Appearance.OddRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewRespoEtape.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.ViewRespoEtape.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
        Me.ViewRespoEtape.Appearance.OddRow.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.OddRow.Options.UseFont = True
        Me.ViewRespoEtape.Appearance.OddRow.Options.UseForeColor = True
        Me.ViewRespoEtape.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ViewRespoEtape.Appearance.Preview.BackColor2 = System.Drawing.Color.White
        Me.ViewRespoEtape.Appearance.Preview.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewRespoEtape.Appearance.Preview.ForeColor = System.Drawing.Color.Maroon
        Me.ViewRespoEtape.Appearance.Preview.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.Preview.Options.UseFont = True
        Me.ViewRespoEtape.Appearance.Preview.Options.UseForeColor = True
        Me.ViewRespoEtape.Appearance.Row.BackColor = System.Drawing.Color.White
        Me.ViewRespoEtape.Appearance.Row.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewRespoEtape.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.ViewRespoEtape.Appearance.Row.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.Row.Options.UseFont = True
        Me.ViewRespoEtape.Appearance.Row.Options.UseForeColor = True
        Me.ViewRespoEtape.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
        Me.ViewRespoEtape.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewRespoEtape.Appearance.RowSeparator.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.ViewRespoEtape.Appearance.SelectedRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewRespoEtape.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
        Me.ViewRespoEtape.Appearance.SelectedRow.Options.UseBackColor = True
        Me.ViewRespoEtape.Appearance.SelectedRow.Options.UseFont = True
        Me.ViewRespoEtape.Appearance.SelectedRow.Options.UseForeColor = True
        Me.ViewRespoEtape.Appearance.TopNewRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewRespoEtape.Appearance.TopNewRow.Options.UseFont = True
        Me.ViewRespoEtape.Appearance.VertLine.BackColor = System.Drawing.Color.Silver
        Me.ViewRespoEtape.Appearance.VertLine.Options.UseBackColor = True
        Me.ViewRespoEtape.GridControl = Me.LgListRespoEtape
        Me.ViewRespoEtape.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.ViewRespoEtape.Name = "ViewRespoEtape"
        Me.ViewRespoEtape.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown
        Me.ViewRespoEtape.OptionsPrint.AutoWidth = False
        Me.ViewRespoEtape.OptionsSelection.MultiSelect = True
        Me.ViewRespoEtape.OptionsView.ColumnAutoWidth = False
        Me.ViewRespoEtape.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewRespoEtape.OptionsView.EnableAppearanceOddRow = True
        Me.ViewRespoEtape.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewRespoEtape.OptionsView.ShowGroupPanel = False
        Me.ViewRespoEtape.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None
        Me.ViewRespoEtape.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.LgListRespoEtape
        Me.GridView1.Name = "GridView1"
        '
        'PanelControl4
        '
        Me.PanelControl4.Controls.Add(Me.Checktous)
        Me.PanelControl4.Controls.Add(Me.BtImprimer)
        Me.PanelControl4.Controls.Add(Me.LabelControl2)
        Me.PanelControl4.Controls.Add(Me.LabelControl3)
        Me.PanelControl4.Controls.Add(Me.TxtRechecher)
        Me.PanelControl4.Controls.Add(Me.BtSupprimer)
        Me.PanelControl4.Controls.Add(Me.BtActualiser)
        Me.PanelControl4.Controls.Add(Me.BtModifier)
        Me.PanelControl4.Controls.Add(Me.BtAjouter)
        Me.PanelControl4.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl4.Location = New System.Drawing.Point(2, 2)
        Me.PanelControl4.Margin = New System.Windows.Forms.Padding(2)
        Me.PanelControl4.Name = "PanelControl4"
        Me.PanelControl4.Size = New System.Drawing.Size(780, 34)
        Me.PanelControl4.TabIndex = 45
        '
        'Checktous
        '
        Me.Checktous.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Checktous.Location = New System.Drawing.Point(657, 8)
        Me.Checktous.Name = "Checktous"
        Me.Checktous.Properties.Caption = "Tout sélectionner"
        Me.Checktous.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Checktous.Size = New System.Drawing.Size(116, 19)
        Me.Checktous.TabIndex = 26
        '
        'BtImprimer
        '
        Me.BtImprimer.Image = Global.ClearProject.My.Resources.Resources.Group_Reports
        Me.BtImprimer.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtImprimer.Location = New System.Drawing.Point(479, 5)
        Me.BtImprimer.Name = "BtImprimer"
        Me.BtImprimer.Size = New System.Drawing.Size(28, 23)
        Me.BtImprimer.TabIndex = 25
        Me.BtImprimer.ToolTip = "Impression"
        Me.BtImprimer.Visible = False
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(461, 6)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(6, 19)
        Me.LabelControl2.TabIndex = 23
        Me.LabelControl2.Text = "|"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(253, 5)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(6, 19)
        Me.LabelControl3.TabIndex = 22
        Me.LabelControl3.Text = "|"
        '
        'TxtRechecher
        '
        Me.TxtRechecher.EditValue = "Rechercher"
        Me.TxtRechecher.Location = New System.Drawing.Point(265, 8)
        Me.TxtRechecher.Name = "TxtRechecher"
        Me.TxtRechecher.Size = New System.Drawing.Size(190, 20)
        Me.TxtRechecher.TabIndex = 24
        Me.TxtRechecher.Tag = "Rechercher"
        Me.TxtRechecher.ToolTip = "Rechercher"
        Me.TxtRechecher.ToolTipTitle = "Rechercher"
        '
        'BtSupprimer
        '
        Me.BtSupprimer.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.BtSupprimer.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtSupprimer.Location = New System.Drawing.Point(218, 5)
        Me.BtSupprimer.Name = "BtSupprimer"
        Me.BtSupprimer.Size = New System.Drawing.Size(28, 23)
        Me.BtSupprimer.TabIndex = 21
        Me.BtSupprimer.ToolTip = "Supprimer"
        '
        'BtActualiser
        '
        Me.BtActualiser.Image = Global.ClearProject.My.Resources.Resources.vieux_rafraichir_vue_icone_4185_16
        Me.BtActualiser.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtActualiser.Location = New System.Drawing.Point(2, 5)
        Me.BtActualiser.Name = "BtActualiser"
        Me.BtActualiser.Size = New System.Drawing.Size(28, 23)
        Me.BtActualiser.TabIndex = 18
        Me.BtActualiser.ToolTipTitle = "Actualiser"
        '
        'BtModifier
        '
        Me.BtModifier.Image = Global.ClearProject.My.Resources.Resources.Edit_16x16
        Me.BtModifier.Location = New System.Drawing.Point(127, 5)
        Me.BtModifier.Name = "BtModifier"
        Me.BtModifier.Size = New System.Drawing.Size(86, 23)
        Me.BtModifier.TabIndex = 20
        Me.BtModifier.Text = "Modifier"
        Me.BtModifier.ToolTip = "Modifier"
        '
        'BtAjouter
        '
        Me.BtAjouter.Image = Global.ClearProject.My.Resources.Resources.Add_16x16
        Me.BtAjouter.Location = New System.Drawing.Point(37, 5)
        Me.BtAjouter.Name = "BtAjouter"
        Me.BtAjouter.Size = New System.Drawing.Size(86, 23)
        Me.BtAjouter.TabIndex = 19
        Me.BtAjouter.Text = "Ajouter"
        Me.BtAjouter.ToolTip = "Ajouter"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.TxtPage)
        Me.PanelControl2.Controls.Add(Me.BtLast)
        Me.PanelControl2.Controls.Add(Me.BtNext)
        Me.PanelControl2.Controls.Add(Me.BtPrev)
        Me.PanelControl2.Controls.Add(Me.BtFrist)
        Me.PanelControl2.Controls.Add(Me.CmbPageSize)
        Me.PanelControl2.Controls.Add(Me.LabelControl1)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl2.Location = New System.Drawing.Point(2, 527)
        Me.PanelControl2.Margin = New System.Windows.Forms.Padding(2)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(780, 32)
        Me.PanelControl2.TabIndex = 43
        '
        'TxtPage
        '
        Me.TxtPage.Location = New System.Drawing.Point(353, 10)
        Me.TxtPage.Margin = New System.Windows.Forms.Padding(2)
        Me.TxtPage.Name = "TxtPage"
        Me.TxtPage.Size = New System.Drawing.Size(46, 13)
        Me.TxtPage.TabIndex = 6
        Me.TxtPage.Text = "Page Size"
        '
        'BtLast
        '
        Me.BtLast.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.BtLast.Location = New System.Drawing.Point(695, 7)
        Me.BtLast.Margin = New System.Windows.Forms.Padding(2)
        Me.BtLast.Name = "BtLast"
        Me.BtLast.Size = New System.Drawing.Size(79, 19)
        Me.BtLast.TabIndex = 5
        Me.BtLast.Text = "Dernier"
        '
        'BtNext
        '
        Me.BtNext.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.BtNext.Location = New System.Drawing.Point(613, 7)
        Me.BtNext.Margin = New System.Windows.Forms.Padding(2)
        Me.BtNext.Name = "BtNext"
        Me.BtNext.Size = New System.Drawing.Size(79, 19)
        Me.BtNext.TabIndex = 4
        Me.BtNext.Text = "Suivant"
        '
        'BtPrev
        '
        Me.BtPrev.Location = New System.Drawing.Point(265, 7)
        Me.BtPrev.Margin = New System.Windows.Forms.Padding(2)
        Me.BtPrev.Name = "BtPrev"
        Me.BtPrev.Size = New System.Drawing.Size(79, 19)
        Me.BtPrev.TabIndex = 3
        Me.BtPrev.Text = "Précédent"
        '
        'BtFrist
        '
        Me.BtFrist.Location = New System.Drawing.Point(183, 7)
        Me.BtFrist.Margin = New System.Windows.Forms.Padding(2)
        Me.BtFrist.Name = "BtFrist"
        Me.BtFrist.Size = New System.Drawing.Size(79, 19)
        Me.BtFrist.TabIndex = 2
        Me.BtFrist.Text = "Premier"
        '
        'CmbPageSize
        '
        Me.CmbPageSize.Location = New System.Drawing.Point(96, 7)
        Me.CmbPageSize.Margin = New System.Windows.Forms.Padding(2)
        Me.CmbPageSize.Name = "CmbPageSize"
        Me.CmbPageSize.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbPageSize.Properties.Items.AddRange(New Object() {"25", "50", "100", "250", "500"})
        Me.CmbPageSize.Size = New System.Drawing.Size(79, 20)
        Me.CmbPageSize.TabIndex = 1
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(5, 10)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(82, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Nombre de lignes"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AjoutPlanDeTiersToolStripMenuItem, Me.ModificationPlanDeTiersToolStripMenuItem, Me.SuppressionCompteToolStripMenuItem, Me.ImprimerCompteDeTiersToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(130, 92)
        '
        'AjoutPlanDeTiersToolStripMenuItem
        '
        Me.AjoutPlanDeTiersToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Add_16x16
        Me.AjoutPlanDeTiersToolStripMenuItem.Name = "AjoutPlanDeTiersToolStripMenuItem"
        Me.AjoutPlanDeTiersToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.AjoutPlanDeTiersToolStripMenuItem.Text = "Ajouter"
        '
        'ModificationPlanDeTiersToolStripMenuItem
        '
        Me.ModificationPlanDeTiersToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Edit_16x16
        Me.ModificationPlanDeTiersToolStripMenuItem.Name = "ModificationPlanDeTiersToolStripMenuItem"
        Me.ModificationPlanDeTiersToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.ModificationPlanDeTiersToolStripMenuItem.Text = "Modifier"
        '
        'SuppressionCompteToolStripMenuItem
        '
        Me.SuppressionCompteToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.SuppressionCompteToolStripMenuItem.Name = "SuppressionCompteToolStripMenuItem"
        Me.SuppressionCompteToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.SuppressionCompteToolStripMenuItem.Text = "Supprimer"
        '
        'ImprimerCompteDeTiersToolStripMenuItem
        '
        Me.ImprimerCompteDeTiersToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Group_Reports
        Me.ImprimerCompteDeTiersToolStripMenuItem.Name = "ImprimerCompteDeTiersToolStripMenuItem"
        Me.ImprimerCompteDeTiersToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.ImprimerCompteDeTiersToolStripMenuItem.Text = "Imprimer"
        '
        'ResponsableEtape
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ResponsableEtape"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Responsables extérieurs étapes"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        CType(Me.LgListRespoEtape, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewRespoEtape, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl4.ResumeLayout(False)
        Me.PanelControl4.PerformLayout()
        CType(Me.Checktous.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtRechecher.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.PanelControl2.PerformLayout()
        CType(Me.CmbPageSize.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AjoutPlanDeTiersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ModificationPlanDeTiersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SuppressionCompteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImprimerCompteDeTiersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtLast As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtNext As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtPrev As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtFrist As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CmbPageSize As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtPage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl4 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtImprimer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtRechecher As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BtSupprimer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtActualiser As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtModifier As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtAjouter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Checktous As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LgListRespoEtape As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewRespoEtape As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
End Class
