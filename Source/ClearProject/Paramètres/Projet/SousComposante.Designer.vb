<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SousComposante
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.CmbCompo = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtSousCompo = New DevExpress.XtraEditors.TextEdit()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SupprimerSousComposanteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RafraichirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.codeclass = New System.Windows.Forms.TextBox()
        Me.impfiche = New System.Windows.Forms.OpenFileDialog()
        Me.GridSousCompo = New DevExpress.XtraGrid.GridControl()
        Me.ViewSousCompo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.CmbCompo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSousCompo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.GridSousCompo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewSousCompo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.SimpleButton1)
        Me.PanelControl1.Controls.Add(Me.CmbCompo)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Controls.Add(Me.TxtSousCompo)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(581, 105)
        Me.PanelControl1.TabIndex = 0
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Dock = System.Windows.Forms.DockStyle.Right
        Me.SimpleButton1.Image = Global.ClearProject.My.Resources.Resources.Ribbon_OPEN_16x16
        Me.SimpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.SimpleButton1.Location = New System.Drawing.Point(544, 2)
        Me.SimpleButton1.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(35, 32)
        Me.SimpleButton1.TabIndex = 5
        Me.SimpleButton1.ToolTip = "Importer documents"
        Me.SimpleButton1.Visible = False
        '
        'CmbCompo
        '
        Me.CmbCompo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmbCompo.Location = New System.Drawing.Point(86, 4)
        Me.CmbCompo.Name = "CmbCompo"
        Me.CmbCompo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbCompo.Size = New System.Drawing.Size(301, 20)
        Me.CmbCompo.TabIndex = 1
        Me.CmbCompo.ToolTip = "Choix de la Composante"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.LineLocation = DevExpress.XtraEditors.LineLocation.Right
        Me.LabelControl1.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Vertical
        Me.LabelControl1.LineVisible = True
        Me.LabelControl1.Location = New System.Drawing.Point(11, 7)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(70, 15)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Composante"
        '
        'TxtSousCompo
        '
        Me.TxtSousCompo.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TxtSousCompo.Location = New System.Drawing.Point(2, 34)
        Me.TxtSousCompo.Name = "TxtSousCompo"
        Me.TxtSousCompo.Properties.AutoHeight = False
        Me.TxtSousCompo.Size = New System.Drawing.Size(577, 69)
        Me.TxtSousCompo.TabIndex = 2
        Me.TxtSousCompo.ToolTip = "Ajout de la Sous Composante"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SupprimerSousComposanteToolStripMenuItem, Me.RafraichirToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(130, 48)
        '
        'SupprimerSousComposanteToolStripMenuItem
        '
        Me.SupprimerSousComposanteToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.SupprimerSousComposanteToolStripMenuItem.Name = "SupprimerSousComposanteToolStripMenuItem"
        Me.SupprimerSousComposanteToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SupprimerSousComposanteToolStripMenuItem.Text = "Supprimer"
        '
        'RafraichirToolStripMenuItem
        '
        Me.RafraichirToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_161
        Me.RafraichirToolStripMenuItem.Name = "RafraichirToolStripMenuItem"
        Me.RafraichirToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.RafraichirToolStripMenuItem.Text = "Actualiser"
        '
        'codeclass
        '
        Me.codeclass.Location = New System.Drawing.Point(376, 173)
        Me.codeclass.Name = "codeclass"
        Me.codeclass.Size = New System.Drawing.Size(100, 21)
        Me.codeclass.TabIndex = 15
        Me.codeclass.Text = "2"
        Me.codeclass.Visible = False
        '
        'impfiche
        '
        Me.impfiche.FileName = "impfiche"
        '
        'GridSousCompo
        '
        Me.GridSousCompo.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GridSousCompo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridSousCompo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridSousCompo.Location = New System.Drawing.Point(0, 105)
        Me.GridSousCompo.MainView = Me.ViewSousCompo
        Me.GridSousCompo.Name = "GridSousCompo"
        Me.GridSousCompo.Size = New System.Drawing.Size(581, 262)
        Me.GridSousCompo.TabIndex = 51
        Me.GridSousCompo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewSousCompo, Me.GridView1})
        '
        'ViewSousCompo
        '
        Me.ViewSousCompo.ActiveFilterEnabled = False
        Me.ViewSousCompo.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.Silver
        Me.ViewSousCompo.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewSousCompo.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.Silver
        Me.ViewSousCompo.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
        Me.ViewSousCompo.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.ViewSousCompo.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.ViewSousCompo.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewSousCompo.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.ViewSousCompo.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewSousCompo.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
        Me.ViewSousCompo.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.ViewSousCompo.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.ViewSousCompo.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewSousCompo.Appearance.Empty.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.EvenRow.BackColor = System.Drawing.Color.Silver
        Me.ViewSousCompo.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite
        Me.ViewSousCompo.Appearance.EvenRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSousCompo.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.ViewSousCompo.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewSousCompo.Appearance.EvenRow.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.EvenRow.Options.UseFont = True
        Me.ViewSousCompo.Appearance.EvenRow.Options.UseForeColor = True
        Me.ViewSousCompo.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewSousCompo.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.ViewSousCompo.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewSousCompo.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.ViewSousCompo.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewSousCompo.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.ViewSousCompo.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.ViewSousCompo.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.ViewSousCompo.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewSousCompo.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
        Me.ViewSousCompo.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewSousCompo.Appearance.FilterPanel.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.FilterPanel.Options.UseForeColor = True
        Me.ViewSousCompo.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.ViewSousCompo.Appearance.FixedLine.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.ViewSousCompo.Appearance.FocusedCell.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSousCompo.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Navy
        Me.ViewSousCompo.Appearance.FocusedCell.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.FocusedCell.Options.UseFont = True
        Me.ViewSousCompo.Appearance.FocusedCell.Options.UseForeColor = True
        Me.ViewSousCompo.Appearance.FocusedRow.BackColor = System.Drawing.Color.Navy
        Me.ViewSousCompo.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.Navy
        Me.ViewSousCompo.Appearance.FocusedRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSousCompo.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.ViewSousCompo.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.FocusedRow.Options.UseFont = True
        Me.ViewSousCompo.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewSousCompo.Appearance.FooterPanel.BackColor = System.Drawing.Color.Silver
        Me.ViewSousCompo.Appearance.FooterPanel.BorderColor = System.Drawing.Color.Silver
        Me.ViewSousCompo.Appearance.FooterPanel.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSousCompo.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewSousCompo.Appearance.FooterPanel.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.ViewSousCompo.Appearance.FooterPanel.Options.UseFont = True
        Me.ViewSousCompo.Appearance.FooterPanel.Options.UseForeColor = True
        Me.ViewSousCompo.Appearance.GroupButton.BackColor = System.Drawing.Color.Silver
        Me.ViewSousCompo.Appearance.GroupButton.BorderColor = System.Drawing.Color.Silver
        Me.ViewSousCompo.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
        Me.ViewSousCompo.Appearance.GroupButton.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.GroupButton.Options.UseBorderColor = True
        Me.ViewSousCompo.Appearance.GroupButton.Options.UseForeColor = True
        Me.ViewSousCompo.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ViewSousCompo.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ViewSousCompo.Appearance.GroupFooter.Font = New System.Drawing.Font("Times New Roman", 8.0!)
        Me.ViewSousCompo.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.ViewSousCompo.Appearance.GroupFooter.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.ViewSousCompo.Appearance.GroupFooter.Options.UseFont = True
        Me.ViewSousCompo.Appearance.GroupFooter.Options.UseForeColor = True
        Me.ViewSousCompo.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.ViewSousCompo.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewSousCompo.Appearance.GroupPanel.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ViewSousCompo.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
        Me.ViewSousCompo.Appearance.GroupPanel.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.GroupPanel.Options.UseFont = True
        Me.ViewSousCompo.Appearance.GroupPanel.Options.UseForeColor = True
        Me.ViewSousCompo.Appearance.GroupRow.BackColor = System.Drawing.Color.Gray
        Me.ViewSousCompo.Appearance.GroupRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSousCompo.Appearance.GroupRow.ForeColor = System.Drawing.Color.Silver
        Me.ViewSousCompo.Appearance.GroupRow.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.GroupRow.Options.UseFont = True
        Me.ViewSousCompo.Appearance.GroupRow.Options.UseForeColor = True
        Me.ViewSousCompo.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Silver
        Me.ViewSousCompo.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Silver
        Me.ViewSousCompo.Appearance.HeaderPanel.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ViewSousCompo.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewSousCompo.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.ViewSousCompo.Appearance.HeaderPanel.Options.UseFont = True
        Me.ViewSousCompo.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.ViewSousCompo.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
        Me.ViewSousCompo.Appearance.HideSelectionRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSousCompo.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewSousCompo.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.HideSelectionRow.Options.UseFont = True
        Me.ViewSousCompo.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.ViewSousCompo.Appearance.HorzLine.BackColor = System.Drawing.Color.Silver
        Me.ViewSousCompo.Appearance.HorzLine.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.OddRow.BackColor = System.Drawing.Color.White
        Me.ViewSousCompo.Appearance.OddRow.BackColor2 = System.Drawing.Color.White
        Me.ViewSousCompo.Appearance.OddRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSousCompo.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.ViewSousCompo.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
        Me.ViewSousCompo.Appearance.OddRow.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.OddRow.Options.UseFont = True
        Me.ViewSousCompo.Appearance.OddRow.Options.UseForeColor = True
        Me.ViewSousCompo.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ViewSousCompo.Appearance.Preview.BackColor2 = System.Drawing.Color.White
        Me.ViewSousCompo.Appearance.Preview.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSousCompo.Appearance.Preview.ForeColor = System.Drawing.Color.Maroon
        Me.ViewSousCompo.Appearance.Preview.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.Preview.Options.UseFont = True
        Me.ViewSousCompo.Appearance.Preview.Options.UseForeColor = True
        Me.ViewSousCompo.Appearance.Row.BackColor = System.Drawing.Color.White
        Me.ViewSousCompo.Appearance.Row.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSousCompo.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.ViewSousCompo.Appearance.Row.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.Row.Options.UseFont = True
        Me.ViewSousCompo.Appearance.Row.Options.UseForeColor = True
        Me.ViewSousCompo.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
        Me.ViewSousCompo.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewSousCompo.Appearance.RowSeparator.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.ViewSousCompo.Appearance.SelectedRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSousCompo.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
        Me.ViewSousCompo.Appearance.SelectedRow.Options.UseBackColor = True
        Me.ViewSousCompo.Appearance.SelectedRow.Options.UseFont = True
        Me.ViewSousCompo.Appearance.SelectedRow.Options.UseForeColor = True
        Me.ViewSousCompo.Appearance.TopNewRow.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ViewSousCompo.Appearance.TopNewRow.Options.UseFont = True
        Me.ViewSousCompo.Appearance.VertLine.BackColor = System.Drawing.Color.Silver
        Me.ViewSousCompo.Appearance.VertLine.Options.UseBackColor = True
        Me.ViewSousCompo.GridControl = Me.GridSousCompo
        Me.ViewSousCompo.Name = "ViewSousCompo"
        Me.ViewSousCompo.OptionsBehavior.Editable = False
        Me.ViewSousCompo.OptionsBehavior.ReadOnly = True
        Me.ViewSousCompo.OptionsPrint.AutoWidth = False
        Me.ViewSousCompo.OptionsView.ColumnAutoWidth = False
        Me.ViewSousCompo.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewSousCompo.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewSousCompo.OptionsView.ShowGroupPanel = False
        Me.ViewSousCompo.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.GridSousCompo
        Me.GridView1.Name = "GridView1"
        '
        'SousComposante
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(581, 367)
        Me.Controls.Add(Me.GridSousCompo)
        Me.Controls.Add(Me.codeclass)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SousComposante"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sous Composantes"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.CmbCompo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSousCompo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.GridSousCompo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewSousCompo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents CmbCompo As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtSousCompo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents codeclass As System.Windows.Forms.TextBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SupprimerSousComposanteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents impfiche As System.Windows.Forms.OpenFileDialog
    Friend WithEvents GridSousCompo As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewSousCompo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RafraichirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
