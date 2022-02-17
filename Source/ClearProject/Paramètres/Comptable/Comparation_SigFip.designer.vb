<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Comparation_SigFip
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
        Me.rdNonCorrespondant = New DevExpress.XtraEditors.CheckEdit()
        Me.rdCorrespondance = New DevExpress.XtraEditors.CheckEdit()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.dgCorrespondance = New DevExpress.XtraGrid.GridControl()
        Me.ViewCorrespondance = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.btPrint = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.rdNonCorrespondant.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdCorrespondance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.dgCorrespondance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewCorrespondance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btPrint)
        Me.PanelControl1.Controls.Add(Me.rdNonCorrespondant)
        Me.PanelControl1.Controls.Add(Me.rdCorrespondance)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(822, 31)
        Me.PanelControl1.TabIndex = 0
        '
        'rdNonCorrespondant
        '
        Me.rdNonCorrespondant.Location = New System.Drawing.Point(452, 5)
        Me.rdNonCorrespondant.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.rdNonCorrespondant.Name = "rdNonCorrespondant"
        Me.rdNonCorrespondant.Properties.Caption = "Visualiser les comptes non correspondants"
        Me.rdNonCorrespondant.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.rdNonCorrespondant.Properties.RadioGroupIndex = 0
        Me.rdNonCorrespondant.Size = New System.Drawing.Size(253, 19)
        Me.rdNonCorrespondant.TabIndex = 3
        Me.rdNonCorrespondant.TabStop = False
        '
        'rdCorrespondance
        '
        Me.rdCorrespondance.Location = New System.Drawing.Point(92, 6)
        Me.rdCorrespondance.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.rdCorrespondance.Name = "rdCorrespondance"
        Me.rdCorrespondance.Properties.Caption = "Visualiser la correspondances des comptes"
        Me.rdCorrespondance.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.rdCorrespondance.Properties.RadioGroupIndex = 0
        Me.rdCorrespondance.Size = New System.Drawing.Size(298, 19)
        Me.rdCorrespondance.TabIndex = 0
        Me.rdCorrespondance.TabStop = False
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.dgCorrespondance)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(0, 31)
        Me.PanelControl2.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(822, 392)
        Me.PanelControl2.TabIndex = 1
        '
        'dgCorrespondance
        '
        Me.dgCorrespondance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgCorrespondance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgCorrespondance.Location = New System.Drawing.Point(2, 2)
        Me.dgCorrespondance.MainView = Me.ViewCorrespondance
        Me.dgCorrespondance.Name = "dgCorrespondance"
        Me.dgCorrespondance.Size = New System.Drawing.Size(818, 388)
        Me.dgCorrespondance.TabIndex = 69
        Me.dgCorrespondance.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewCorrespondance, Me.GridView1})
        '
        'ViewCorrespondance
        '
        Me.ViewCorrespondance.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ViewCorrespondance.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ViewCorrespondance.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.White
        Me.ViewCorrespondance.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.ViewCorrespondance.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.ViewCorrespondance.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(98, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.ViewCorrespondance.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(98, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.ViewCorrespondance.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.White
        Me.ViewCorrespondance.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.ViewCorrespondance.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.ViewCorrespondance.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(177, Byte), Integer))
        Me.ViewCorrespondance.Appearance.Empty.BackColor2 = System.Drawing.Color.White
        Me.ViewCorrespondance.Appearance.Empty.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.EvenRow.BackColor = System.Drawing.Color.White
        Me.ViewCorrespondance.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.ViewCorrespondance.Appearance.EvenRow.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.EvenRow.Options.UseForeColor = True
        Me.ViewCorrespondance.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ViewCorrespondance.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ViewCorrespondance.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.White
        Me.ViewCorrespondance.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.ViewCorrespondance.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.ViewCorrespondance.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(177, Byte), Integer))
        Me.ViewCorrespondance.Appearance.FilterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(177, Byte), Integer))
        Me.ViewCorrespondance.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewCorrespondance.Appearance.FilterPanel.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.FilterPanel.Options.UseBorderColor = True
        Me.ViewCorrespondance.Appearance.FilterPanel.Options.UseForeColor = True
        Me.ViewCorrespondance.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(98, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.ViewCorrespondance.Appearance.FixedLine.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.ViewCorrespondance.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.ViewCorrespondance.Appearance.FocusedCell.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.FocusedCell.Options.UseForeColor = True
        Me.ViewCorrespondance.Appearance.FocusedRow.BackColor = System.Drawing.Color.Navy
        Me.ViewCorrespondance.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.Navy
        Me.ViewCorrespondance.Appearance.FocusedRow.BorderColor = System.Drawing.Color.Navy
        Me.ViewCorrespondance.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.ViewCorrespondance.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.ViewCorrespondance.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewCorrespondance.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ViewCorrespondance.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ViewCorrespondance.Appearance.FooterPanel.ForeColor = System.Drawing.Color.White
        Me.ViewCorrespondance.Appearance.FooterPanel.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.ViewCorrespondance.Appearance.FooterPanel.Options.UseForeColor = True
        Me.ViewCorrespondance.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ViewCorrespondance.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ViewCorrespondance.Appearance.GroupButton.ForeColor = System.Drawing.Color.White
        Me.ViewCorrespondance.Appearance.GroupButton.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.GroupButton.Options.UseBorderColor = True
        Me.ViewCorrespondance.Appearance.GroupButton.Options.UseForeColor = True
        Me.ViewCorrespondance.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.ViewCorrespondance.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.ViewCorrespondance.Appearance.GroupFooter.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.ViewCorrespondance.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(177, Byte), Integer))
        Me.ViewCorrespondance.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewCorrespondance.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewCorrespondance.Appearance.GroupPanel.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.GroupPanel.Options.UseForeColor = True
        Me.ViewCorrespondance.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.ViewCorrespondance.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.ViewCorrespondance.Appearance.GroupRow.ForeColor = System.Drawing.Color.White
        Me.ViewCorrespondance.Appearance.GroupRow.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.GroupRow.Options.UseBorderColor = True
        Me.ViewCorrespondance.Appearance.GroupRow.Options.UseForeColor = True
        Me.ViewCorrespondance.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(98, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.ViewCorrespondance.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(98, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.ViewCorrespondance.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewCorrespondance.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.ViewCorrespondance.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.ViewCorrespondance.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(93, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ViewCorrespondance.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White
        Me.ViewCorrespondance.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.ViewCorrespondance.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(98, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.ViewCorrespondance.Appearance.HorzLine.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.ViewCorrespondance.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.ViewCorrespondance.Appearance.OddRow.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.OddRow.Options.UseForeColor = True
        Me.ViewCorrespondance.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(253, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.ViewCorrespondance.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
        Me.ViewCorrespondance.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.ViewCorrespondance.Appearance.Preview.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.Preview.Options.UseFont = True
        Me.ViewCorrespondance.Appearance.Preview.Options.UseForeColor = True
        Me.ViewCorrespondance.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.ViewCorrespondance.Appearance.Row.BackColor2 = System.Drawing.Color.White
        Me.ViewCorrespondance.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.ViewCorrespondance.Appearance.Row.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.Row.Options.UseForeColor = True
        Me.ViewCorrespondance.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
        Me.ViewCorrespondance.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
        Me.ViewCorrespondance.Appearance.RowSeparator.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(41, Byte), Integer))
        Me.ViewCorrespondance.Appearance.SelectedRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(41, Byte), Integer))
        Me.ViewCorrespondance.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
        Me.ViewCorrespondance.Appearance.SelectedRow.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.SelectedRow.Options.UseBorderColor = True
        Me.ViewCorrespondance.Appearance.SelectedRow.Options.UseForeColor = True
        Me.ViewCorrespondance.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
        Me.ViewCorrespondance.Appearance.TopNewRow.Options.UseBackColor = True
        Me.ViewCorrespondance.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(98, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.ViewCorrespondance.Appearance.VertLine.Options.UseBackColor = True
        Me.ViewCorrespondance.GridControl = Me.dgCorrespondance
        Me.ViewCorrespondance.Name = "ViewCorrespondance"
        Me.ViewCorrespondance.OptionsSelection.EnableAppearanceHideSelection = False
        Me.ViewCorrespondance.OptionsSelection.MultiSelect = True
        Me.ViewCorrespondance.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewCorrespondance.OptionsView.ShowGroupPanel = False
        Me.ViewCorrespondance.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewCorrespondance.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.dgCorrespondance
        Me.GridView1.Name = "GridView1"
        '
        'btPrint
        '
        Me.btPrint.Dock = System.Windows.Forms.DockStyle.Right
        Me.btPrint.Image = Global.ClearProject.My.Resources.Resources.Group_Reports
        Me.btPrint.Location = New System.Drawing.Point(725, 2)
        Me.btPrint.Margin = New System.Windows.Forms.Padding(2)
        Me.btPrint.Name = "btPrint"
        Me.btPrint.Size = New System.Drawing.Size(95, 27)
        Me.btPrint.TabIndex = 16
        Me.btPrint.Text = "Imprimer"
        '
        'Comparation_SigFip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(822, 423)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Comparation_SigFip"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Correspondance des comptes SYSCOHADA au comptes SIGFIP"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.rdNonCorrespondant.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdCorrespondance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.dgCorrespondance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewCorrespondance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents rdNonCorrespondant As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents rdCorrespondance As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents dgCorrespondance As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewCorrespondance As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btPrint As DevExpress.XtraEditors.SimpleButton
End Class
