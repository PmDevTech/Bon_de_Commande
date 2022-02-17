<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ListesSignataires
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
        Me.ListeSignataite = New DevExpress.XtraGrid.GridControl()
        Me.ViewSignataire = New DevExpress.XtraGrid.Views.Grid.GridView()
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
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AjoutPlanDeTiersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModificationPlanDeTiersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SuppressionCompteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImprimerCompteDeTiersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.ListeSignataite, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewSignataire, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl4.SuspendLayout()
        CType(Me.Checktous.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtRechecher.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.PanelControl3)
        Me.PanelControl1.Controls.Add(Me.PanelControl4)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(784, 419)
        Me.PanelControl1.TabIndex = 0
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.ListeSignataite)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl3.Location = New System.Drawing.Point(2, 36)
        Me.PanelControl3.Margin = New System.Windows.Forms.Padding(2)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(780, 381)
        Me.PanelControl3.TabIndex = 46
        '
        'ListeSignataite
        '
        Me.ListeSignataite.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.ListeSignataite.AllowDrop = True
        Me.ListeSignataite.AllowRestoreSelectionAndFocusedRow = DevExpress.Utils.DefaultBoolean.[True]
        Me.ListeSignataite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListeSignataite.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListeSignataite.Location = New System.Drawing.Point(2, 2)
        Me.ListeSignataite.MainView = Me.ViewSignataire
        Me.ListeSignataite.Name = "ListeSignataite"
        Me.ListeSignataite.Size = New System.Drawing.Size(776, 377)
        Me.ListeSignataite.TabIndex = 43
        Me.ListeSignataite.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewSignataire, Me.GridView1})
        '
        'ViewSignataire
        '
        Me.ViewSignataire.ActiveFilterEnabled = False
        Me.ViewSignataire.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.Silver
        Me.ViewSignataire.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewSignataire.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.Silver
        Me.ViewSignataire.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
        Me.ViewSignataire.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.ViewSignataire.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.ViewSignataire.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewSignataire.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.ViewSignataire.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewSignataire.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
        Me.ViewSignataire.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.ViewSignataire.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.ViewSignataire.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewSignataire.Appearance.Empty.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.EvenRow.BackColor = System.Drawing.Color.Silver
        Me.ViewSignataire.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite
        Me.ViewSignataire.Appearance.EvenRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSignataire.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.ViewSignataire.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewSignataire.Appearance.EvenRow.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.EvenRow.Options.UseFont = True
        Me.ViewSignataire.Appearance.EvenRow.Options.UseForeColor = True
        Me.ViewSignataire.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewSignataire.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.ViewSignataire.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewSignataire.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.ViewSignataire.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewSignataire.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.ViewSignataire.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.ViewSignataire.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.ViewSignataire.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewSignataire.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
        Me.ViewSignataire.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewSignataire.Appearance.FilterPanel.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.FilterPanel.Options.UseForeColor = True
        Me.ViewSignataire.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.ViewSignataire.Appearance.FixedLine.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.ViewSignataire.Appearance.FocusedCell.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSignataire.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Navy
        Me.ViewSignataire.Appearance.FocusedCell.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.FocusedCell.Options.UseFont = True
        Me.ViewSignataire.Appearance.FocusedCell.Options.UseForeColor = True
        Me.ViewSignataire.Appearance.FocusedRow.BackColor = System.Drawing.Color.Navy
        Me.ViewSignataire.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.Navy
        Me.ViewSignataire.Appearance.FocusedRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSignataire.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.ViewSignataire.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.FocusedRow.Options.UseFont = True
        Me.ViewSignataire.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewSignataire.Appearance.FooterPanel.BackColor = System.Drawing.Color.Silver
        Me.ViewSignataire.Appearance.FooterPanel.BorderColor = System.Drawing.Color.Silver
        Me.ViewSignataire.Appearance.FooterPanel.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSignataire.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewSignataire.Appearance.FooterPanel.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.ViewSignataire.Appearance.FooterPanel.Options.UseFont = True
        Me.ViewSignataire.Appearance.FooterPanel.Options.UseForeColor = True
        Me.ViewSignataire.Appearance.GroupButton.BackColor = System.Drawing.Color.Silver
        Me.ViewSignataire.Appearance.GroupButton.BorderColor = System.Drawing.Color.Silver
        Me.ViewSignataire.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
        Me.ViewSignataire.Appearance.GroupButton.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.GroupButton.Options.UseBorderColor = True
        Me.ViewSignataire.Appearance.GroupButton.Options.UseForeColor = True
        Me.ViewSignataire.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ViewSignataire.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ViewSignataire.Appearance.GroupFooter.Font = New System.Drawing.Font("Times New Roman", 8.0!)
        Me.ViewSignataire.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.ViewSignataire.Appearance.GroupFooter.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.ViewSignataire.Appearance.GroupFooter.Options.UseFont = True
        Me.ViewSignataire.Appearance.GroupFooter.Options.UseForeColor = True
        Me.ViewSignataire.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.ViewSignataire.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewSignataire.Appearance.GroupPanel.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ViewSignataire.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
        Me.ViewSignataire.Appearance.GroupPanel.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.GroupPanel.Options.UseFont = True
        Me.ViewSignataire.Appearance.GroupPanel.Options.UseForeColor = True
        Me.ViewSignataire.Appearance.GroupRow.BackColor = System.Drawing.Color.Gray
        Me.ViewSignataire.Appearance.GroupRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSignataire.Appearance.GroupRow.ForeColor = System.Drawing.Color.Silver
        Me.ViewSignataire.Appearance.GroupRow.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.GroupRow.Options.UseFont = True
        Me.ViewSignataire.Appearance.GroupRow.Options.UseForeColor = True
        Me.ViewSignataire.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Silver
        Me.ViewSignataire.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Silver
        Me.ViewSignataire.Appearance.HeaderPanel.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ViewSignataire.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewSignataire.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.ViewSignataire.Appearance.HeaderPanel.Options.UseFont = True
        Me.ViewSignataire.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.ViewSignataire.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
        Me.ViewSignataire.Appearance.HideSelectionRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSignataire.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewSignataire.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.HideSelectionRow.Options.UseFont = True
        Me.ViewSignataire.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.ViewSignataire.Appearance.HorzLine.BackColor = System.Drawing.Color.Silver
        Me.ViewSignataire.Appearance.HorzLine.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.OddRow.BackColor = System.Drawing.Color.White
        Me.ViewSignataire.Appearance.OddRow.BackColor2 = System.Drawing.Color.White
        Me.ViewSignataire.Appearance.OddRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSignataire.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.ViewSignataire.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
        Me.ViewSignataire.Appearance.OddRow.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.OddRow.Options.UseFont = True
        Me.ViewSignataire.Appearance.OddRow.Options.UseForeColor = True
        Me.ViewSignataire.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ViewSignataire.Appearance.Preview.BackColor2 = System.Drawing.Color.White
        Me.ViewSignataire.Appearance.Preview.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSignataire.Appearance.Preview.ForeColor = System.Drawing.Color.Maroon
        Me.ViewSignataire.Appearance.Preview.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.Preview.Options.UseFont = True
        Me.ViewSignataire.Appearance.Preview.Options.UseForeColor = True
        Me.ViewSignataire.Appearance.Row.BackColor = System.Drawing.Color.White
        Me.ViewSignataire.Appearance.Row.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSignataire.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.ViewSignataire.Appearance.Row.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.Row.Options.UseFont = True
        Me.ViewSignataire.Appearance.Row.Options.UseForeColor = True
        Me.ViewSignataire.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
        Me.ViewSignataire.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewSignataire.Appearance.RowSeparator.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.ViewSignataire.Appearance.SelectedRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSignataire.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
        Me.ViewSignataire.Appearance.SelectedRow.Options.UseBackColor = True
        Me.ViewSignataire.Appearance.SelectedRow.Options.UseFont = True
        Me.ViewSignataire.Appearance.SelectedRow.Options.UseForeColor = True
        Me.ViewSignataire.Appearance.TopNewRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSignataire.Appearance.TopNewRow.Options.UseFont = True
        Me.ViewSignataire.Appearance.VertLine.BackColor = System.Drawing.Color.Silver
        Me.ViewSignataire.Appearance.VertLine.Options.UseBackColor = True
        Me.ViewSignataire.GridControl = Me.ListeSignataite
        Me.ViewSignataire.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.ViewSignataire.Name = "ViewSignataire"
        Me.ViewSignataire.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown
        Me.ViewSignataire.OptionsPrint.AutoWidth = False
        Me.ViewSignataire.OptionsSelection.MultiSelect = True
        Me.ViewSignataire.OptionsView.ColumnAutoWidth = False
        Me.ViewSignataire.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewSignataire.OptionsView.EnableAppearanceOddRow = True
        Me.ViewSignataire.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewSignataire.OptionsView.ShowGroupPanel = False
        Me.ViewSignataire.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None
        Me.ViewSignataire.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.ListeSignataite
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
        'ListesSignataires
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 419)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ListesSignataires"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Listes des signataires"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        CType(Me.ListeSignataite, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewSignataire, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl4.ResumeLayout(False)
        Me.PanelControl4.PerformLayout()
        CType(Me.Checktous.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtRechecher.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AjoutPlanDeTiersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ModificationPlanDeTiersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SuppressionCompteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImprimerCompteDeTiersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
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
    Friend WithEvents ListeSignataite As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewSignataire As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
End Class
