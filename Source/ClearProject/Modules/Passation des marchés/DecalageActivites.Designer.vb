<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DecalageActivites
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
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.LblCpteActiv = New DevExpress.XtraEditors.LabelControl()
        Me.TxtDateNotif = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GridDecalage = New DevExpress.XtraGrid.GridControl()
        Me.ViewDecalage = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.BtQuitter = New DevExpress.XtraEditors.SimpleButton()
        Me.BtValider = New DevExpress.XtraEditors.SimpleButton()
        Me.RdDebutNotif = New DevExpress.XtraEditors.CheckEdit()
        Me.PanelControl4 = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.CmbUnitDelai = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.NumDelai = New DevExpress.XtraEditors.SpinEdit()
        Me.RdDebutApresNotif = New DevExpress.XtraEditors.CheckEdit()
        Me.GbMarche = New DevExpress.XtraEditors.GroupControl()
        Me.LblMarche = New DevExpress.XtraEditors.LabelControl()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.TxtDateNotif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GridDecalage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewDecalage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.RdDebutNotif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl4.SuspendLayout()
        CType(Me.CmbUnitDelai.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumDelai.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RdDebutApresNotif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GbMarche, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GbMarche.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.LblCpteActiv)
        Me.PanelControl2.Controls.Add(Me.TxtDateNotif)
        Me.PanelControl2.Controls.Add(Me.LabelControl1)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl2.Location = New System.Drawing.Point(0, 62)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(884, 32)
        Me.PanelControl2.TabIndex = 1
        '
        'LblCpteActiv
        '
        Me.LblCpteActiv.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCpteActiv.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LblCpteActiv.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblCpteActiv.LineLocation = DevExpress.XtraEditors.LineLocation.Right
        Me.LblCpteActiv.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Vertical
        Me.LblCpteActiv.LineVisible = True
        Me.LblCpteActiv.Location = New System.Drawing.Point(669, 7)
        Me.LblCpteActiv.Name = "LblCpteActiv"
        Me.LblCpteActiv.Size = New System.Drawing.Size(210, 19)
        Me.LblCpteActiv.TabIndex = 2
        Me.LblCpteActiv.Text = "Activités sélectionnées : 0 / 0"
        '
        'TxtDateNotif
        '
        Me.TxtDateNotif.Location = New System.Drawing.Point(279, 3)
        Me.TxtDateNotif.Name = "TxtDateNotif"
        Me.TxtDateNotif.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateNotif.Properties.Appearance.Options.UseFont = True
        Me.TxtDateNotif.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtDateNotif.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtDateNotif.Properties.ReadOnly = True
        Me.TxtDateNotif.Size = New System.Drawing.Size(338, 26)
        Me.TxtDateNotif.TabIndex = 1
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(7, 6)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(266, 19)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Notification de Démarrage des Services"
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.GridDecalage)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 94)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(884, 171)
        Me.GroupControl1.TabIndex = 2
        Me.GroupControl1.Text = "Activités concernées"
        '
        'GridDecalage
        '
        Me.GridDecalage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridDecalage.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridDecalage.Location = New System.Drawing.Point(2, 23)
        Me.GridDecalage.MainView = Me.ViewDecalage
        Me.GridDecalage.Name = "GridDecalage"
        Me.GridDecalage.Size = New System.Drawing.Size(880, 146)
        Me.GridDecalage.TabIndex = 9
        Me.GridDecalage.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewDecalage})
        '
        'ViewDecalage
        '
        Me.ViewDecalage.ActiveFilterEnabled = False
        Me.ViewDecalage.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewDecalage.Appearance.Row.Options.UseFont = True
        Me.ViewDecalage.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
        Me.ViewDecalage.GridControl = Me.GridDecalage
        Me.ViewDecalage.Name = "ViewDecalage"
        Me.ViewDecalage.OptionsBehavior.Editable = False
        Me.ViewDecalage.OptionsBehavior.ReadOnly = True
        Me.ViewDecalage.OptionsCustomization.AllowColumnMoving = False
        Me.ViewDecalage.OptionsCustomization.AllowFilter = False
        Me.ViewDecalage.OptionsCustomization.AllowGroup = False
        Me.ViewDecalage.OptionsCustomization.AllowSort = False
        Me.ViewDecalage.OptionsFilter.AllowFilterEditor = False
        Me.ViewDecalage.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewDecalage.OptionsPrint.AutoWidth = False
        Me.ViewDecalage.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewDecalage.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewDecalage.OptionsView.ColumnAutoWidth = False
        Me.ViewDecalage.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewDecalage.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewDecalage.OptionsView.ShowGroupPanel = False
        Me.ViewDecalage.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.[True]
        Me.ViewDecalage.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewDecalage.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.BtQuitter)
        Me.PanelControl3.Controls.Add(Me.BtValider)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl3.Location = New System.Drawing.Point(0, 301)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(884, 40)
        Me.PanelControl3.TabIndex = 3
        '
        'BtQuitter
        '
        Me.BtQuitter.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtQuitter.Appearance.Options.UseFont = True
        Me.BtQuitter.Dock = System.Windows.Forms.DockStyle.Left
        Me.BtQuitter.Image = Global.ClearProject.My.Resources.Resources.Delete32
        Me.BtQuitter.Location = New System.Drawing.Point(2, 2)
        Me.BtQuitter.Name = "BtQuitter"
        Me.BtQuitter.Size = New System.Drawing.Size(169, 36)
        Me.BtQuitter.TabIndex = 1
        Me.BtQuitter.Text = "Quitter"
        '
        'BtValider
        '
        Me.BtValider.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtValider.Appearance.Options.UseFont = True
        Me.BtValider.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtValider.Image = Global.ClearProject.My.Resources.Resources.Period_32x32
        Me.BtValider.Location = New System.Drawing.Point(713, 2)
        Me.BtValider.Name = "BtValider"
        Me.BtValider.Size = New System.Drawing.Size(169, 36)
        Me.BtValider.TabIndex = 0
        Me.BtValider.Text = "VALIDER"
        '
        'RdDebutNotif
        '
        Me.RdDebutNotif.Location = New System.Drawing.Point(5, 6)
        Me.RdDebutNotif.Name = "RdDebutNotif"
        Me.RdDebutNotif.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RdDebutNotif.Properties.Appearance.Options.UseFont = True
        Me.RdDebutNotif.Properties.Caption = "Débuter la sélection dès la Notification"
        Me.RdDebutNotif.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.RdDebutNotif.Properties.RadioGroupIndex = 1
        Me.RdDebutNotif.Size = New System.Drawing.Size(293, 24)
        Me.RdDebutNotif.TabIndex = 2
        '
        'PanelControl4
        '
        Me.PanelControl4.Controls.Add(Me.LabelControl2)
        Me.PanelControl4.Controls.Add(Me.CmbUnitDelai)
        Me.PanelControl4.Controls.Add(Me.NumDelai)
        Me.PanelControl4.Controls.Add(Me.RdDebutApresNotif)
        Me.PanelControl4.Controls.Add(Me.RdDebutNotif)
        Me.PanelControl4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl4.Location = New System.Drawing.Point(0, 265)
        Me.PanelControl4.Name = "PanelControl4"
        Me.PanelControl4.Size = New System.Drawing.Size(884, 36)
        Me.PanelControl4.TabIndex = 4
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(740, 9)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(138, 19)
        Me.LabelControl2.TabIndex = 6
        Me.LabelControl2.Text = "Après la Notification"
        '
        'CmbUnitDelai
        '
        Me.CmbUnitDelai.Enabled = False
        Me.CmbUnitDelai.Location = New System.Drawing.Point(629, 5)
        Me.CmbUnitDelai.Name = "CmbUnitDelai"
        Me.CmbUnitDelai.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbUnitDelai.Properties.Appearance.Options.UseFont = True
        Me.CmbUnitDelai.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbUnitDelai.Properties.Items.AddRange(New Object() {"Jours", "Semaines", "Mois", "Ans"})
        Me.CmbUnitDelai.Size = New System.Drawing.Size(105, 26)
        Me.CmbUnitDelai.TabIndex = 5
        '
        'NumDelai
        '
        Me.NumDelai.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumDelai.Enabled = False
        Me.NumDelai.Location = New System.Drawing.Point(579, 5)
        Me.NumDelai.Name = "NumDelai"
        Me.NumDelai.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumDelai.Properties.Appearance.Options.UseFont = True
        Me.NumDelai.Properties.Appearance.Options.UseTextOptions = True
        Me.NumDelai.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.NumDelai.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.NumDelai.Properties.IsFloatValue = False
        Me.NumDelai.Properties.Mask.EditMask = "N00"
        Me.NumDelai.Size = New System.Drawing.Size(44, 26)
        Me.NumDelai.TabIndex = 4
        '
        'RdDebutApresNotif
        '
        Me.RdDebutApresNotif.Location = New System.Drawing.Point(416, 6)
        Me.RdDebutApresNotif.Name = "RdDebutApresNotif"
        Me.RdDebutApresNotif.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RdDebutApresNotif.Properties.Appearance.Options.UseFont = True
        Me.RdDebutApresNotif.Properties.Caption = "Débuter la sélection"
        Me.RdDebutApresNotif.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.RdDebutApresNotif.Properties.RadioGroupIndex = 1
        Me.RdDebutApresNotif.Size = New System.Drawing.Size(157, 24)
        Me.RdDebutApresNotif.TabIndex = 3
        Me.RdDebutApresNotif.TabStop = False
        '
        'GbMarche
        '
        Me.GbMarche.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GbMarche.AppearanceCaption.Options.UseFont = True
        Me.GbMarche.Controls.Add(Me.LblMarche)
        Me.GbMarche.Dock = System.Windows.Forms.DockStyle.Top
        Me.GbMarche.Location = New System.Drawing.Point(0, 0)
        Me.GbMarche.Name = "GbMarche"
        Me.GbMarche.Size = New System.Drawing.Size(884, 62)
        Me.GbMarche.TabIndex = 5
        Me.GbMarche.Text = "Plan de Passation de Marché de Fournitures"
        '
        'LblMarche
        '
        Me.LblMarche.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMarche.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LblMarche.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblMarche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMarche.LineLocation = DevExpress.XtraEditors.LineLocation.Left
        Me.LblMarche.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Vertical
        Me.LblMarche.LineVisible = True
        Me.LblMarche.Location = New System.Drawing.Point(2, 23)
        Me.LblMarche.Name = "LblMarche"
        Me.LblMarche.Size = New System.Drawing.Size(880, 37)
        Me.LblMarche.TabIndex = 3
        '
        'DecalageActivites
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 341)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.PanelControl4)
        Me.Controls.Add(Me.PanelControl3)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.GbMarche)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DecalageActivites"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Décalage des activités"
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.PanelControl2.PerformLayout()
        CType(Me.TxtDateNotif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.GridDecalage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewDecalage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        CType(Me.RdDebutNotif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl4.ResumeLayout(False)
        Me.PanelControl4.PerformLayout()
        CType(Me.CmbUnitDelai.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumDelai.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RdDebutApresNotif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GbMarche, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GbMarche.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents TxtDateNotif As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents RdDebutNotif As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents PanelControl4 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CmbUnitDelai As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents NumDelai As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents RdDebutApresNotif As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LblCpteActiv As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GridDecalage As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewDecalage As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BtQuitter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtValider As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GbMarche As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LblMarche As DevExpress.XtraEditors.LabelControl
End Class
