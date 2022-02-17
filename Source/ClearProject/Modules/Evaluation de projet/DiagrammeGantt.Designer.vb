<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DiagrammeGantt
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
        Dim ResourceItemComparerByName1 As KS.Gantt.ResourceItemComparerByName = New KS.Gantt.ResourceItemComparerByName()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DiagrammeGantt))
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.GanttDataGrid1 = New KS.Gantt.GanttDataGrid()
        Me.Gantt1 = New KS.Gantt.Gantt()
        Me.XtraScrollableControl1 = New DevExpress.XtraEditors.XtraScrollableControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.ColorToday = New DevExpress.XtraEditors.ColorEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.ColorWeekEnd = New DevExpress.XtraEditors.ColorEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.ColorBackGround = New DevExpress.XtraEditors.ColorEdit()
        Me.BtImprimer = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.GanttDataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gantt1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraScrollableControl1.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.ColorToday.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ColorWeekEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ColorBackGround.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 53)
        Me.SplitContainerControl1.Margin = New System.Windows.Forms.Padding(5)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.GanttDataGrid1)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.XtraScrollableControl1)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1728, 789)
        Me.SplitContainerControl1.SplitterPosition = 300
        Me.SplitContainerControl1.TabIndex = 0
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'GanttDataGrid1
        '
        Me.GanttDataGrid1.AllowEdit = False
        Me.GanttDataGrid1.AllowRowReorder = False
        Me.GanttDataGrid1.BackColor = System.Drawing.SystemColors.Window
        Me.GanttDataGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GanttDataGrid1.GanttControl = Me.Gantt1
        Me.GanttDataGrid1.Location = New System.Drawing.Point(0, 0)
        Me.GanttDataGrid1.Margin = New System.Windows.Forms.Padding(5)
        Me.GanttDataGrid1.Name = "GanttDataGrid1"
        Me.GanttDataGrid1.SelectionMode = KS.Gantt.GanttDataGrid.GanttDataGridSelectionModes.Row
        Me.GanttDataGrid1.Size = New System.Drawing.Size(300, 789)
        Me.GanttDataGrid1.TabIndex = 1
        Me.GanttDataGrid1.TaskColumns = CType(resources.GetObject("GanttDataGrid1.TaskColumns"), KS.Gantt.GanttDataColumnCollection)
        Me.GanttDataGrid1.Text = "GanttDataGrid1"
        '
        'Gantt1
        '
        Me.Gantt1.AllowEdit = False
        Me.Gantt1.AllowItemAddNew = False
        Me.Gantt1.AllowItemDelete = False
        Me.Gantt1.AllowItemMove = False
        Me.Gantt1.AllowItemResize = False
        Me.Gantt1.AutoArrangeRowsOnUpdate = False
        Me.Gantt1.AutoMoveItemsOnUpdate = False
        Me.Gantt1.BackColor = System.Drawing.SystemColors.Window
        Me.Gantt1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Gantt1.CausesValidation = False
        Me.Gantt1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gantt1.Location = New System.Drawing.Point(0, 0)
        Me.Gantt1.Margin = New System.Windows.Forms.Padding(5)
        Me.Gantt1.Name = "Gantt1"
        Me.Gantt1.PercentBarDeflate = New System.Drawing.Size(0, 8)
        Me.Gantt1.ResourceViewItemComparer = ResourceItemComparerByName1
        Me.Gantt1.Size = New System.Drawing.Size(1423, 789)
        Me.Gantt1.TabIndex = 0
        Me.Gantt1.Text = "Gantt1"
        Me.Gantt1.TodayLineStyle = CType(resources.GetObject("Gantt1.TodayLineStyle"), KS.Gantt.LineStyle)
        Me.Gantt1.WeekPrefix = "S "
        '
        'XtraScrollableControl1
        '
        Me.XtraScrollableControl1.Controls.Add(Me.Gantt1)
        Me.XtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraScrollableControl1.Location = New System.Drawing.Point(0, 0)
        Me.XtraScrollableControl1.Margin = New System.Windows.Forms.Padding(5)
        Me.XtraScrollableControl1.Name = "XtraScrollableControl1"
        Me.XtraScrollableControl1.Size = New System.Drawing.Size(1423, 789)
        Me.XtraScrollableControl1.TabIndex = 1
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.ColorToday)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.ColorWeekEnd)
        Me.PanelControl1.Controls.Add(Me.LabelControl2)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Controls.Add(Me.ColorBackGround)
        Me.PanelControl1.Controls.Add(Me.BtImprimer)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(5)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1728, 53)
        Me.PanelControl1.TabIndex = 1
        '
        'ColorToday
        '
        Me.ColorToday.EditValue = System.Drawing.Color.Red
        Me.ColorToday.Location = New System.Drawing.Point(860, 9)
        Me.ColorToday.Margin = New System.Windows.Forms.Padding(5)
        Me.ColorToday.Name = "ColorToday"
        Me.ColorToday.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ColorToday.Properties.Appearance.Options.UseBackColor = True
        Me.ColorToday.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ColorToday.Size = New System.Drawing.Size(200, 30)
        Me.ColorToday.TabIndex = 6
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(740, 12)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(5)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(121, 25)
        Me.LabelControl3.TabIndex = 5
        Me.LabelControl3.Text = "Aujourd'hui"
        '
        'ColorWeekEnd
        '
        Me.ColorWeekEnd.EditValue = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ColorWeekEnd.Location = New System.Drawing.Point(507, 9)
        Me.ColorWeekEnd.Margin = New System.Windows.Forms.Padding(5)
        Me.ColorWeekEnd.Name = "ColorWeekEnd"
        Me.ColorWeekEnd.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ColorWeekEnd.Properties.Appearance.Options.UseBackColor = True
        Me.ColorWeekEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ColorWeekEnd.Size = New System.Drawing.Size(200, 30)
        Me.ColorWeekEnd.TabIndex = 4
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(363, 12)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(5)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(149, 25)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Fin de semaine"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(8, 12)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(5)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(137, 25)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "Fond de page"
        '
        'ColorBackGround
        '
        Me.ColorBackGround.EditValue = System.Drawing.SystemColors.Window
        Me.ColorBackGround.Location = New System.Drawing.Point(133, 9)
        Me.ColorBackGround.Margin = New System.Windows.Forms.Padding(5)
        Me.ColorBackGround.Name = "ColorBackGround"
        Me.ColorBackGround.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ColorBackGround.Properties.Appearance.Options.UseBackColor = True
        Me.ColorBackGround.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ColorBackGround.Size = New System.Drawing.Size(200, 30)
        Me.ColorBackGround.TabIndex = 1
        '
        'BtImprimer
        '
        Me.BtImprimer.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtImprimer.Appearance.Options.UseFont = True
        Me.BtImprimer.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtImprimer.Image = Global.ClearProject.My.Resources.Resources.Group_Reports
        Me.BtImprimer.Location = New System.Drawing.Point(1529, 2)
        Me.BtImprimer.Margin = New System.Windows.Forms.Padding(5)
        Me.BtImprimer.Name = "BtImprimer"
        Me.BtImprimer.Size = New System.Drawing.Size(197, 49)
        Me.BtImprimer.TabIndex = 0
        Me.BtImprimer.Text = "IMPRIMER"
        '
        'DiagrammeGantt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1728, 842)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "DiagrammeGantt"
        Me.Text = "Diagramme de Gantt"
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.GanttDataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gantt1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraScrollableControl1.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.ColorToday.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ColorWeekEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ColorBackGround.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtImprimer As DevExpress.XtraEditors.SimpleButton
    'Friend WithEvents Gantt_DataSet As ClearProject.Gantt_DataSet
    'Friend WithEvents T_Gantt_ActiviteTableAdapter As ClearProject.Gantt_DataSetTableAdapters.T_Gantt_ActiviteTableAdapter
    Friend WithEvents XtraScrollableControl1 As DevExpress.XtraEditors.XtraScrollableControl
    Friend WithEvents GanttDataGrid1 As KS.Gantt.GanttDataGrid
    Friend WithEvents Gantt1 As KS.Gantt.Gantt
    Friend WithEvents ColorWeekEnd As DevExpress.XtraEditors.ColorEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ColorBackGround As DevExpress.XtraEditors.ColorEdit
    Friend WithEvents ColorToday As DevExpress.XtraEditors.ColorEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    'Friend WithEvents DataSet2 As ClearProject.DataSet2
    'Friend WithEvents T_Gantt_ActiviteTableAdapter As ClearProject.DataSet2TableAdapters.T_Gantt_ActiviteTableAdapter
    'Friend WithEvents DataSet2 As ClearProject.DataSet2
    'Friend WithEvents TPartitionBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents T_PartitionTableAdapter As ClearProject.DataSet2TableAdapters.T_PartitionTableAdapter
End Class
