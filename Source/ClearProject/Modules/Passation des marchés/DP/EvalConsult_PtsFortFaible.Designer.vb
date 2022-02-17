<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EvalConsult_PtsFortFaible
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
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GridNoteConsult = New DevExpress.XtraGrid.GridControl()
        Me.ViewNoteConsult = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.GridPtsFaibles = New DevExpress.XtraGrid.GridControl()
        Me.ViewPtsFaibles = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TxtPtFaible = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.GridPtsForts = New DevExpress.XtraGrid.GridControl()
        Me.ViewPtsForts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TxtPtFort = New DevExpress.XtraEditors.TextEdit()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.BtFermer = New DevExpress.XtraEditors.SimpleButton()
        Me.CmbConsult = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GridNoteConsult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewNoteConsult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.GridPtsFaibles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewPtsFaibles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPtFaible.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.GridPtsForts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewPtsForts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPtFort.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.CmbConsult.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 43)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.GroupControl1)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.GroupControl3)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.GroupControl2)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(967, 404)
        Me.SplitContainerControl1.SplitterPosition = 400
        Me.SplitContainerControl1.TabIndex = 0
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl1.Controls.Add(Me.GridNoteConsult)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(400, 404)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Notes obtenues"
        '
        'GridNoteConsult
        '
        Me.GridNoteConsult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridNoteConsult.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridNoteConsult.Location = New System.Drawing.Point(2, 23)
        Me.GridNoteConsult.MainView = Me.ViewNoteConsult
        Me.GridNoteConsult.Name = "GridNoteConsult"
        Me.GridNoteConsult.Size = New System.Drawing.Size(396, 379)
        Me.GridNoteConsult.TabIndex = 7
        Me.GridNoteConsult.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewNoteConsult})
        '
        'ViewNoteConsult
        '
        Me.ViewNoteConsult.ActiveFilterEnabled = False
        Me.ViewNoteConsult.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewNoteConsult.Appearance.Row.Options.UseFont = True
        Me.ViewNoteConsult.GridControl = Me.GridNoteConsult
        Me.ViewNoteConsult.Name = "ViewNoteConsult"
        Me.ViewNoteConsult.OptionsBehavior.Editable = False
        Me.ViewNoteConsult.OptionsBehavior.ReadOnly = True
        Me.ViewNoteConsult.OptionsCustomization.AllowColumnMoving = False
        Me.ViewNoteConsult.OptionsCustomization.AllowFilter = False
        Me.ViewNoteConsult.OptionsCustomization.AllowGroup = False
        Me.ViewNoteConsult.OptionsCustomization.AllowSort = False
        Me.ViewNoteConsult.OptionsFilter.AllowFilterEditor = False
        Me.ViewNoteConsult.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewNoteConsult.OptionsPrint.AutoWidth = False
        Me.ViewNoteConsult.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewNoteConsult.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewNoteConsult.OptionsView.ColumnAutoWidth = False
        Me.ViewNoteConsult.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewNoteConsult.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewNoteConsult.OptionsView.ShowGroupPanel = False
        Me.ViewNoteConsult.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewNoteConsult.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'GroupControl3
        '
        Me.GroupControl3.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl3.AppearanceCaption.Options.UseFont = True
        Me.GroupControl3.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl3.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl3.Controls.Add(Me.GridPtsFaibles)
        Me.GroupControl3.Controls.Add(Me.TxtPtFaible)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(0, 197)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(562, 207)
        Me.GroupControl3.TabIndex = 1
        Me.GroupControl3.Text = "Points faibles"
        '
        'GridPtsFaibles
        '
        Me.GridPtsFaibles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridPtsFaibles.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridPtsFaibles.Location = New System.Drawing.Point(2, 47)
        Me.GridPtsFaibles.MainView = Me.ViewPtsFaibles
        Me.GridPtsFaibles.Name = "GridPtsFaibles"
        Me.GridPtsFaibles.Size = New System.Drawing.Size(558, 158)
        Me.GridPtsFaibles.TabIndex = 2
        Me.GridPtsFaibles.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewPtsFaibles})
        '
        'ViewPtsFaibles
        '
        Me.ViewPtsFaibles.ActiveFilterEnabled = False
        Me.ViewPtsFaibles.GridControl = Me.GridPtsFaibles
        Me.ViewPtsFaibles.Name = "ViewPtsFaibles"
        Me.ViewPtsFaibles.OptionsBehavior.Editable = False
        Me.ViewPtsFaibles.OptionsBehavior.ReadOnly = True
        Me.ViewPtsFaibles.OptionsCustomization.AllowColumnMoving = False
        Me.ViewPtsFaibles.OptionsCustomization.AllowFilter = False
        Me.ViewPtsFaibles.OptionsCustomization.AllowGroup = False
        Me.ViewPtsFaibles.OptionsFilter.AllowFilterEditor = False
        Me.ViewPtsFaibles.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewPtsFaibles.OptionsPrint.PrintHeader = False
        Me.ViewPtsFaibles.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewPtsFaibles.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewPtsFaibles.OptionsView.ColumnAutoWidth = False
        Me.ViewPtsFaibles.OptionsView.ShowColumnHeaders = False
        Me.ViewPtsFaibles.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewPtsFaibles.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewPtsFaibles.OptionsView.ShowGroupPanel = False
        Me.ViewPtsFaibles.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewPtsFaibles.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'TxtPtFaible
        '
        Me.TxtPtFaible.Dock = System.Windows.Forms.DockStyle.Top
        Me.TxtPtFaible.Location = New System.Drawing.Point(2, 23)
        Me.TxtPtFaible.Name = "TxtPtFaible"
        Me.TxtPtFaible.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPtFaible.Properties.Appearance.Options.UseFont = True
        Me.TxtPtFaible.Size = New System.Drawing.Size(558, 24)
        Me.TxtPtFaible.TabIndex = 1
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl2.Controls.Add(Me.GridPtsForts)
        Me.GroupControl2.Controls.Add(Me.TxtPtFort)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(562, 197)
        Me.GroupControl2.TabIndex = 0
        Me.GroupControl2.Text = "Points forts"
        '
        'GridPtsForts
        '
        Me.GridPtsForts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridPtsForts.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridPtsForts.Location = New System.Drawing.Point(2, 47)
        Me.GridPtsForts.MainView = Me.ViewPtsForts
        Me.GridPtsForts.Name = "GridPtsForts"
        Me.GridPtsForts.Size = New System.Drawing.Size(558, 148)
        Me.GridPtsForts.TabIndex = 2
        Me.GridPtsForts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewPtsForts})
        '
        'ViewPtsForts
        '
        Me.ViewPtsForts.ActiveFilterEnabled = False
        Me.ViewPtsForts.GridControl = Me.GridPtsForts
        Me.ViewPtsForts.Name = "ViewPtsForts"
        Me.ViewPtsForts.OptionsBehavior.Editable = False
        Me.ViewPtsForts.OptionsBehavior.ReadOnly = True
        Me.ViewPtsForts.OptionsCustomization.AllowColumnMoving = False
        Me.ViewPtsForts.OptionsCustomization.AllowFilter = False
        Me.ViewPtsForts.OptionsCustomization.AllowGroup = False
        Me.ViewPtsForts.OptionsFilter.AllowFilterEditor = False
        Me.ViewPtsForts.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewPtsForts.OptionsPrint.PrintHeader = False
        Me.ViewPtsForts.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewPtsForts.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewPtsForts.OptionsView.ColumnAutoWidth = False
        Me.ViewPtsForts.OptionsView.ShowColumnHeaders = False
        Me.ViewPtsForts.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewPtsForts.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewPtsForts.OptionsView.ShowGroupPanel = False
        Me.ViewPtsForts.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewPtsForts.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'TxtPtFort
        '
        Me.TxtPtFort.Dock = System.Windows.Forms.DockStyle.Top
        Me.TxtPtFort.Location = New System.Drawing.Point(2, 23)
        Me.TxtPtFort.Name = "TxtPtFort"
        Me.TxtPtFort.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPtFort.Properties.Appearance.Options.UseFont = True
        Me.TxtPtFort.Size = New System.Drawing.Size(558, 24)
        Me.TxtPtFort.TabIndex = 0
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.BtFermer)
        Me.PanelControl1.Controls.Add(Me.CmbConsult)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(967, 43)
        Me.PanelControl1.TabIndex = 1
        '
        'BtFermer
        '
        Me.BtFermer.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtFermer.Appearance.Options.UseFont = True
        Me.BtFermer.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtFermer.Image = Global.ClearProject.My.Resources.Resources.Close_32x32
        Me.BtFermer.Location = New System.Drawing.Point(858, 2)
        Me.BtFermer.Name = "BtFermer"
        Me.BtFermer.Size = New System.Drawing.Size(107, 39)
        Me.BtFermer.TabIndex = 3
        Me.BtFermer.Text = "Fermer"
        '
        'CmbConsult
        '
        Me.CmbConsult.Location = New System.Drawing.Point(89, 8)
        Me.CmbConsult.Name = "CmbConsult"
        Me.CmbConsult.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbConsult.Properties.Appearance.Options.UseFont = True
        Me.CmbConsult.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbConsult.Size = New System.Drawing.Size(473, 26)
        Me.CmbConsult.TabIndex = 2
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(12, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(72, 19)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Consultant"
        '
        'EvalConsult_PtsFortFaible
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(967, 447)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EvalConsult_PtsFortFaible"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "POINTS FORTS - POINTS FAIBLES"
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.GridNoteConsult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewNoteConsult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.GridPtsFaibles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewPtsFaibles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPtFaible.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.GridPtsForts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewPtsForts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPtFort.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.CmbConsult.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents CmbConsult As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridNoteConsult As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewNoteConsult As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TxtPtFaible As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPtFort As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GridPtsFaibles As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewPtsFaibles As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridPtsForts As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewPtsForts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BtFermer As DevExpress.XtraEditors.SimpleButton
End Class
