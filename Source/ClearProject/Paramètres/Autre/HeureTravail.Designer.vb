<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HeureTravail
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
        Me.BtEnregistrer = New DevExpress.XtraEditors.SimpleButton()
        Me.BtSupprimer = New DevExpress.XtraEditors.SimpleButton()
        Me.Heure_fin = New DevExpress.XtraEditors.LabelControl()
        Me.H_deb = New DevExpress.XtraEditors.LabelControl()
        Me.H_trav = New DevExpress.XtraEditors.LabelControl()
        Me.H_pause = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.TotalHPause = New System.Windows.Forms.TextBox()
        Me.TotalHTrav = New System.Windows.Forms.TextBox()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.debHTrav = New DevExpress.XtraEditors.TextEdit()
        Me.finHPause = New DevExpress.XtraEditors.TextEdit()
        Me.debHPause = New DevExpress.XtraEditors.TextEdit()
        Me.finHTrav = New DevExpress.XtraEditors.TextEdit()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.debHTrav.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.finHPause.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.debHPause.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.finHTrav.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtEnregistrer
        '
        Me.BtEnregistrer.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnregistrer.Appearance.Options.UseFont = True
        Me.BtEnregistrer.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.BtEnregistrer.Location = New System.Drawing.Point(206, 262)
        Me.BtEnregistrer.Margin = New System.Windows.Forms.Padding(7)
        Me.BtEnregistrer.Name = "BtEnregistrer"
        Me.BtEnregistrer.Size = New System.Drawing.Size(212, 46)
        Me.BtEnregistrer.TabIndex = 81
        Me.BtEnregistrer.Text = "Enregistrer"
        '
        'BtSupprimer
        '
        Me.BtSupprimer.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtSupprimer.Appearance.Options.UseFont = True
        Me.BtSupprimer.Image = Global.ClearProject.My.Resources.Resources.Delete_16x16
        Me.BtSupprimer.Location = New System.Drawing.Point(514, 262)
        Me.BtSupprimer.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.BtSupprimer.Name = "BtSupprimer"
        Me.BtSupprimer.Size = New System.Drawing.Size(200, 46)
        Me.BtSupprimer.TabIndex = 83
        Me.BtSupprimer.Text = "Supprimer"
        '
        'Heure_fin
        '
        Me.Heure_fin.Location = New System.Drawing.Point(496, 69)
        Me.Heure_fin.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Heure_fin.Name = "Heure_fin"
        Me.Heure_fin.Size = New System.Drawing.Size(105, 23)
        Me.Heure_fin.TabIndex = 55
        Me.Heure_fin.Text = "Heure de fin"
        '
        'H_deb
        '
        Me.H_deb.Location = New System.Drawing.Point(64, 66)
        Me.H_deb.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.H_deb.Name = "H_deb"
        Me.H_deb.Size = New System.Drawing.Size(133, 23)
        Me.H_deb.TabIndex = 54
        Me.H_deb.Text = "Heure de début"
        '
        'H_trav
        '
        Me.H_trav.Location = New System.Drawing.Point(464, 176)
        Me.H_trav.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.H_trav.Name = "H_trav"
        Me.H_trav.Size = New System.Drawing.Size(139, 23)
        Me.H_trav.TabIndex = 56
        Me.H_trav.Text = "Heure de Travail"
        '
        'H_pause
        '
        Me.H_pause.Location = New System.Drawing.Point(63, 173)
        Me.H_pause.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.H_pause.Name = "H_pause"
        Me.H_pause.Size = New System.Drawing.Size(133, 23)
        Me.H_pause.TabIndex = 58
        Me.H_pause.Text = "Heure de Pause"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(478, 122)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(124, 23)
        Me.LabelControl1.TabIndex = 62
        Me.LabelControl1.Text = "fin de la pause"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(44, 116)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(154, 23)
        Me.LabelControl2.TabIndex = 61
        Me.LabelControl2.Text = "Début de la pause"
        '
        'TotalHPause
        '
        Me.TotalHPause.Location = New System.Drawing.Point(204, 166)
        Me.TotalHPause.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.TotalHPause.Name = "TotalHPause"
        Me.TotalHPause.ReadOnly = True
        Me.TotalHPause.Size = New System.Drawing.Size(244, 30)
        Me.TotalHPause.TabIndex = 85
        '
        'TotalHTrav
        '
        Me.TotalHTrav.Location = New System.Drawing.Point(606, 170)
        Me.TotalHTrav.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.TotalHTrav.Name = "TotalHTrav"
        Me.TotalHTrav.ReadOnly = True
        Me.TotalHTrav.Size = New System.Drawing.Size(253, 30)
        Me.TotalHTrav.TabIndex = 86
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.debHTrav)
        Me.GroupControl1.Controls.Add(Me.finHPause)
        Me.GroupControl1.Controls.Add(Me.debHPause)
        Me.GroupControl1.Controls.Add(Me.finHTrav)
        Me.GroupControl1.Controls.Add(Me.TotalHTrav)
        Me.GroupControl1.Controls.Add(Me.BtSupprimer)
        Me.GroupControl1.Controls.Add(Me.TotalHPause)
        Me.GroupControl1.Controls.Add(Me.BtEnregistrer)
        Me.GroupControl1.Controls.Add(Me.H_deb)
        Me.GroupControl1.Controls.Add(Me.Heure_fin)
        Me.GroupControl1.Controls.Add(Me.H_trav)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.H_pause)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(927, 328)
        Me.GroupControl1.TabIndex = 74
        Me.GroupControl1.Text = "Heure de Travail"
        '
        'debHTrav
        '
        Me.debHTrav.EditValue = "00:00"
        Me.debHTrav.Location = New System.Drawing.Point(204, 66)
        Me.debHTrav.Name = "debHTrav"
        Me.debHTrav.Properties.Mask.EditMask = "(0?\d|1\d|2[0-3])\:[0-5]\d"
        Me.debHTrav.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.debHTrav.Properties.Mask.PlaceHolder = Global.Microsoft.VisualBasic.ChrW(48)
        Me.debHTrav.Size = New System.Drawing.Size(244, 30)
        Me.debHTrav.TabIndex = 78
        '
        'finHPause
        '
        Me.finHPause.EditValue = "00:00"
        Me.finHPause.Location = New System.Drawing.Point(607, 119)
        Me.finHPause.Name = "finHPause"
        Me.finHPause.Properties.Mask.EditMask = "(0?\d|1\d|2[0-3])\:[0-5]\d"
        Me.finHPause.Properties.Mask.IgnoreMaskBlank = False
        Me.finHPause.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.finHPause.Properties.Mask.PlaceHolder = Global.Microsoft.VisualBasic.ChrW(48)
        Me.finHPause.Size = New System.Drawing.Size(251, 30)
        Me.finHPause.TabIndex = 81
        '
        'debHPause
        '
        Me.debHPause.EditValue = "00:00"
        Me.debHPause.Location = New System.Drawing.Point(204, 114)
        Me.debHPause.Name = "debHPause"
        Me.debHPause.Properties.Mask.EditMask = "(0?\d|1\d|2[0-3])\:[0-5]\d"
        Me.debHPause.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.debHPause.Properties.Mask.PlaceHolder = Global.Microsoft.VisualBasic.ChrW(48)
        Me.debHPause.Size = New System.Drawing.Size(246, 30)
        Me.debHPause.TabIndex = 80
        '
        'finHTrav
        '
        Me.finHTrav.EditValue = "00:00"
        Me.finHTrav.Location = New System.Drawing.Point(606, 66)
        Me.finHTrav.Name = "finHTrav"
        Me.finHTrav.Properties.Mask.EditMask = "(0?\d|1\d|2[0-3]):[0-5]\d"
        Me.finHTrav.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.finHTrav.Properties.Mask.PlaceHolder = Global.Microsoft.VisualBasic.ChrW(48)
        Me.finHTrav.Size = New System.Drawing.Size(251, 30)
        Me.finHTrav.TabIndex = 79
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.GridControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 328)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(927, 123)
        Me.PanelControl1.TabIndex = 75
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControl1.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GridControl1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridControl1.Location = New System.Drawing.Point(2, 2)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(923, 119)
        Me.GridControl1.TabIndex = 29
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.ActiveFilterEnabled = False
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsCustomization.AllowColumnMoving = False
        Me.GridView1.OptionsCustomization.AllowFilter = False
        Me.GridView1.OptionsCustomization.AllowGroup = False
        Me.GridView1.OptionsCustomization.AllowSort = False
        Me.GridView1.OptionsFilter.AllowFilterEditor = False
        Me.GridView1.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.GridView1.OptionsPrint.AutoWidth = False
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GridView1.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'HeureTravail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(927, 451)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "HeureTravail"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Heure de Travail"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.debHTrav.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.finHPause.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.debHPause.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.finHTrav.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtEnregistrer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtSupprimer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Heure_fin As DevExpress.XtraEditors.LabelControl
    Friend WithEvents H_deb As DevExpress.XtraEditors.LabelControl
    Friend WithEvents H_trav As DevExpress.XtraEditors.LabelControl
    Friend WithEvents H_pause As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TotalHPause As System.Windows.Forms.TextBox
    Friend WithEvents TotalHTrav As System.Windows.Forms.TextBox
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents finHPause As DevExpress.XtraEditors.TextEdit
    Friend WithEvents debHPause As DevExpress.XtraEditors.TextEdit
    Friend WithEvents finHTrav As DevExpress.XtraEditors.TextEdit
    Friend WithEvents debHTrav As DevExpress.XtraEditors.TextEdit
End Class
