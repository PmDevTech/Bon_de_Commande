<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ListeRestreindreAMI
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
        Me.GridLrs = New DevExpress.XtraGrid.GridControl()
        Me.ViewRs = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl3AMI = New DevExpress.XtraEditors.GroupControl()
        Me.BtInfoConnecter = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl6AMI = New DevExpress.XtraEditors.GroupControl()
        Me.GbNotationConsultantsAMI = New DevExpress.XtraEditors.GroupControl()
        Me.PanelControl5 = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl6AMI = New DevExpress.XtraEditors.LabelControl()
        Me.DossierAMI = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.SimpleButton2 = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GridLrs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewRs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3AMI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3AMI.SuspendLayout()
        CType(Me.GroupControl6AMI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6AMI.SuspendLayout()
        CType(Me.GbNotationConsultantsAMI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GbNotationConsultantsAMI.SuspendLayout()
        CType(Me.PanelControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl5.SuspendLayout()
        CType(Me.DossierAMI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridLrs
        '
        Me.GridLrs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridLrs.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLrs.Location = New System.Drawing.Point(2, 26)
        Me.GridLrs.MainView = Me.ViewRs
        Me.GridLrs.Name = "GridLrs"
        Me.GridLrs.Size = New System.Drawing.Size(647, 331)
        Me.GridLrs.TabIndex = 5
        Me.GridLrs.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewRs, Me.GridView1})
        '
        'ViewRs
        '
        Me.ViewRs.ActiveFilterEnabled = False
        Me.ViewRs.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewRs.Appearance.Row.Options.UseFont = True
        Me.ViewRs.GridControl = Me.GridLrs
        Me.ViewRs.Name = "ViewRs"
        Me.ViewRs.OptionsBehavior.Editable = False
        Me.ViewRs.OptionsBehavior.ReadOnly = True
        Me.ViewRs.OptionsCustomization.AllowColumnMoving = False
        Me.ViewRs.OptionsCustomization.AllowFilter = False
        Me.ViewRs.OptionsCustomization.AllowGroup = False
        Me.ViewRs.OptionsCustomization.AllowSort = False
        Me.ViewRs.OptionsFilter.AllowFilterEditor = False
        Me.ViewRs.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewRs.OptionsPrint.AutoWidth = False
        Me.ViewRs.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewRs.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewRs.OptionsView.ColumnAutoWidth = False
        Me.ViewRs.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewRs.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewRs.OptionsView.ShowGroupPanel = False
        Me.ViewRs.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewRs.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.GridLrs
        Me.GridView1.Name = "GridView1"
        '
        'GroupControl3AMI
        '
        Me.GroupControl3AMI.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl3AMI.AppearanceCaption.ForeColor = System.Drawing.Color.Red
        Me.GroupControl3AMI.AppearanceCaption.Options.UseFont = True
        Me.GroupControl3AMI.AppearanceCaption.Options.UseForeColor = True
        Me.GroupControl3AMI.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl3AMI.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GroupControl3AMI.Controls.Add(Me.GridLrs)
        Me.GroupControl3AMI.Controls.Add(Me.BtInfoConnecter)
        Me.GroupControl3AMI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3AMI.Location = New System.Drawing.Point(2, 23)
        Me.GroupControl3AMI.Name = "GroupControl3AMI"
        Me.GroupControl3AMI.Size = New System.Drawing.Size(651, 359)
        Me.GroupControl3AMI.TabIndex = 17
        Me.GroupControl3AMI.Text = "Liste restreinte"
        '
        'BtInfoConnecter
        '
        Me.BtInfoConnecter.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtInfoConnecter.Appearance.ForeColor = System.Drawing.Color.Red
        Me.BtInfoConnecter.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BtInfoConnecter.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.BtInfoConnecter.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.BtInfoConnecter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtInfoConnecter.Location = New System.Drawing.Point(2, 26)
        Me.BtInfoConnecter.Name = "BtInfoConnecter"
        Me.BtInfoConnecter.Size = New System.Drawing.Size(647, 331)
        Me.BtInfoConnecter.TabIndex = 28
        Me.BtInfoConnecter.Text = "...."
        '
        'GroupControl6AMI
        '
        Me.GroupControl6AMI.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl6AMI.AppearanceCaption.Options.UseFont = True
        Me.GroupControl6AMI.CaptionLocation = DevExpress.Utils.Locations.Top
        Me.GroupControl6AMI.Controls.Add(Me.GbNotationConsultantsAMI)
        Me.GroupControl6AMI.Controls.Add(Me.PanelControl5)
        Me.GroupControl6AMI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl6AMI.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl6AMI.Name = "GroupControl6AMI"
        Me.GroupControl6AMI.Size = New System.Drawing.Size(659, 433)
        Me.GroupControl6AMI.TabIndex = 18
        '
        'GbNotationConsultantsAMI
        '
        Me.GbNotationConsultantsAMI.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GbNotationConsultantsAMI.AppearanceCaption.Options.UseFont = True
        Me.GbNotationConsultantsAMI.AppearanceCaption.Options.UseTextOptions = True
        Me.GbNotationConsultantsAMI.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GbNotationConsultantsAMI.CaptionLocation = DevExpress.Utils.Locations.Top
        Me.GbNotationConsultantsAMI.Controls.Add(Me.GroupControl3AMI)
        Me.GbNotationConsultantsAMI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GbNotationConsultantsAMI.FireScrollEventOnMouseWheel = True
        Me.GbNotationConsultantsAMI.Location = New System.Drawing.Point(2, 47)
        Me.GbNotationConsultantsAMI.Name = "GbNotationConsultantsAMI"
        Me.GbNotationConsultantsAMI.Size = New System.Drawing.Size(655, 384)
        Me.GbNotationConsultantsAMI.TabIndex = 12
        '
        'PanelControl5
        '
        Me.PanelControl5.Controls.Add(Me.LabelControl6AMI)
        Me.PanelControl5.Controls.Add(Me.DossierAMI)
        Me.PanelControl5.Controls.Add(Me.SimpleButton2)
        Me.PanelControl5.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl5.Location = New System.Drawing.Point(2, 23)
        Me.PanelControl5.Name = "PanelControl5"
        Me.PanelControl5.Size = New System.Drawing.Size(655, 24)
        Me.PanelControl5.TabIndex = 2
        '
        'LabelControl6AMI
        '
        Me.LabelControl6AMI.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6AMI.Dock = System.Windows.Forms.DockStyle.Left
        Me.LabelControl6AMI.Location = New System.Drawing.Point(2, 2)
        Me.LabelControl6AMI.Name = "LabelControl6AMI"
        Me.LabelControl6AMI.Size = New System.Drawing.Size(52, 19)
        Me.LabelControl6AMI.TabIndex = 25
        Me.LabelControl6AMI.Text = "Dossier"
        '
        'DossierAMI
        '
        Me.DossierAMI.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DossierAMI.Location = New System.Drawing.Point(80, 2)
        Me.DossierAMI.Name = "DossierAMI"
        Me.DossierAMI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DossierAMI.Size = New System.Drawing.Size(574, 20)
        Me.DossierAMI.TabIndex = 24
        '
        'SimpleButton2
        '
        Me.SimpleButton2.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SimpleButton2.Appearance.ForeColor = System.Drawing.Color.Black
        Me.SimpleButton2.Appearance.Options.UseFont = True
        Me.SimpleButton2.Appearance.Options.UseForeColor = True
        Me.SimpleButton2.Appearance.Options.UseTextOptions = True
        Me.SimpleButton2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.SimpleButton2.Enabled = False
        Me.SimpleButton2.Location = New System.Drawing.Point(1, 56)
        Me.SimpleButton2.Name = "SimpleButton2"
        Me.SimpleButton2.Size = New System.Drawing.Size(107, 22)
        Me.SimpleButton2.TabIndex = 8
        Me.SimpleButton2.Text = "Code de présence"
        '
        'ListeRestreindreAMI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(659, 433)
        Me.Controls.Add(Me.GroupControl6AMI)
        Me.Name = "ListeRestreindreAMI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Liste restreinte"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GridLrs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewRs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3AMI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3AMI.ResumeLayout(False)
        CType(Me.GroupControl6AMI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6AMI.ResumeLayout(False)
        CType(Me.GbNotationConsultantsAMI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GbNotationConsultantsAMI.ResumeLayout(False)
        CType(Me.PanelControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl5.ResumeLayout(False)
        Me.PanelControl5.PerformLayout()
        CType(Me.DossierAMI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GridLrs As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewRs As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl3AMI As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl6AMI As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GbNotationConsultantsAMI As DevExpress.XtraEditors.GroupControl
    Friend WithEvents PanelControl5 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl6AMI As DevExpress.XtraEditors.LabelControl
    Friend WithEvents DossierAMI As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents SimpleButton2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtInfoConnecter As DevExpress.XtraEditors.LabelControl
End Class
