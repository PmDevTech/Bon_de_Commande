<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CompteA_MarcheEtTypeMarche
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
        Me.BtMajRessources = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.BtSuppToutType = New DevExpress.XtraEditors.SimpleButton()
        Me.BtSuppType = New DevExpress.XtraEditors.SimpleButton()
        Me.BtSvceAssimile = New DevExpress.XtraEditors.SimpleButton()
        Me.BtTravaux = New DevExpress.XtraEditors.SimpleButton()
        Me.BtFournitures = New DevExpress.XtraEditors.SimpleButton()
        Me.BtConsultants = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.GridType = New DevExpress.XtraGrid.GridControl()
        Me.ViewType = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GridGeneral = New DevExpress.XtraGrid.GridControl()
        Me.ViewGeneral = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.GridType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GridGeneral, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewGeneral, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.BtMajRessources)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1072, 46)
        Me.PanelControl1.TabIndex = 0
        Me.PanelControl1.Visible = False
        '
        'BtMajRessources
        '
        Me.BtMajRessources.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtMajRessources.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtMajRessources.Appearance.Options.UseFont = True
        Me.BtMajRessources.Image = Global.ClearProject.My.Resources.Resources.actualiser
        Me.BtMajRessources.Location = New System.Drawing.Point(852, 5)
        Me.BtMajRessources.Name = "BtMajRessources"
        Me.BtMajRessources.Size = New System.Drawing.Size(215, 37)
        Me.BtMajRessources.TabIndex = 4
        Me.BtMajRessources.Text = "Mettre les ressources à jour"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.BtSuppToutType)
        Me.PanelControl2.Controls.Add(Me.BtSuppType)
        Me.PanelControl2.Controls.Add(Me.BtSvceAssimile)
        Me.PanelControl2.Controls.Add(Me.BtTravaux)
        Me.PanelControl2.Controls.Add(Me.BtFournitures)
        Me.PanelControl2.Controls.Add(Me.BtConsultants)
        Me.PanelControl2.Controls.Add(Me.GroupControl3)
        Me.PanelControl2.Controls.Add(Me.GroupControl1)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(0, 46)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(1072, 452)
        Me.PanelControl2.TabIndex = 1
        '
        'BtSuppToutType
        '
        Me.BtSuppToutType.Appearance.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtSuppToutType.Appearance.Options.UseFont = True
        Me.BtSuppToutType.Location = New System.Drawing.Point(548, 303)
        Me.BtSuppToutType.Name = "BtSuppToutType"
        Me.BtSuppToutType.Size = New System.Drawing.Size(90, 24)
        Me.BtSuppToutType.TabIndex = 10
        Me.BtSuppToutType.Text = "<<"
        '
        'BtSuppType
        '
        Me.BtSuppType.Appearance.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtSuppType.Appearance.Options.UseFont = True
        Me.BtSuppType.Location = New System.Drawing.Point(548, 273)
        Me.BtSuppType.Name = "BtSuppType"
        Me.BtSuppType.Size = New System.Drawing.Size(90, 24)
        Me.BtSuppType.TabIndex = 7
        Me.BtSuppType.Text = "<"
        '
        'BtSvceAssimile
        '
        Me.BtSvceAssimile.Appearance.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtSvceAssimile.Appearance.Options.UseFont = True
        Me.BtSvceAssimile.Location = New System.Drawing.Point(548, 196)
        Me.BtSvceAssimile.Name = "BtSvceAssimile"
        Me.BtSvceAssimile.Size = New System.Drawing.Size(90, 24)
        Me.BtSvceAssimile.TabIndex = 6
        Me.BtSvceAssimile.Text = "Autre Service >"
        '
        'BtTravaux
        '
        Me.BtTravaux.Appearance.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtTravaux.Appearance.Options.UseFont = True
        Me.BtTravaux.Location = New System.Drawing.Point(548, 166)
        Me.BtTravaux.Name = "BtTravaux"
        Me.BtTravaux.Size = New System.Drawing.Size(90, 24)
        Me.BtTravaux.TabIndex = 5
        Me.BtTravaux.Text = "Travaux >"
        '
        'BtFournitures
        '
        Me.BtFournitures.Appearance.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtFournitures.Appearance.Options.UseFont = True
        Me.BtFournitures.Location = New System.Drawing.Point(548, 136)
        Me.BtFournitures.Name = "BtFournitures"
        Me.BtFournitures.Size = New System.Drawing.Size(90, 24)
        Me.BtFournitures.TabIndex = 4
        Me.BtFournitures.Text = "Fournitures >"
        '
        'BtConsultants
        '
        Me.BtConsultants.Appearance.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtConsultants.Appearance.Options.UseFont = True
        Me.BtConsultants.Location = New System.Drawing.Point(548, 106)
        Me.BtConsultants.Name = "BtConsultants"
        Me.BtConsultants.Size = New System.Drawing.Size(90, 24)
        Me.BtConsultants.TabIndex = 3
        Me.BtConsultants.Text = "Consultants >"
        '
        'GroupControl3
        '
        Me.GroupControl3.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl3.AppearanceCaption.Options.UseFont = True
        Me.GroupControl3.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl3.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl3.Controls.Add(Me.GridType)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupControl3.Location = New System.Drawing.Point(646, 2)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(424, 448)
        Me.GroupControl3.TabIndex = 2
        Me.GroupControl3.Text = "Comptes à Marchés"
        '
        'GridType
        '
        Me.GridType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridType.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridType.Location = New System.Drawing.Point(2, 23)
        Me.GridType.MainView = Me.ViewType
        Me.GridType.Name = "GridType"
        Me.GridType.Size = New System.Drawing.Size(420, 423)
        Me.GridType.TabIndex = 10
        Me.GridType.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewType})
        '
        'ViewType
        '
        Me.ViewType.ActiveFilterEnabled = False
        Me.ViewType.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewType.Appearance.Row.Options.UseFont = True
        Me.ViewType.GridControl = Me.GridType
        Me.ViewType.Name = "ViewType"
        Me.ViewType.OptionsBehavior.Editable = False
        Me.ViewType.OptionsBehavior.ReadOnly = True
        Me.ViewType.OptionsCustomization.AllowColumnMoving = False
        Me.ViewType.OptionsCustomization.AllowFilter = False
        Me.ViewType.OptionsCustomization.AllowGroup = False
        Me.ViewType.OptionsCustomization.AllowSort = False
        Me.ViewType.OptionsFilter.AllowFilterEditor = False
        Me.ViewType.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewType.OptionsPrint.AutoWidth = False
        Me.ViewType.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewType.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewType.OptionsView.ColumnAutoWidth = False
        Me.ViewType.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewType.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewType.OptionsView.ShowGroupPanel = False
        Me.ViewType.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewType.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl1.Controls.Add(Me.GridGeneral)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupControl1.Location = New System.Drawing.Point(2, 2)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(537, 448)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Comptes Généraux"
        '
        'GridGeneral
        '
        Me.GridGeneral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridGeneral.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridGeneral.Location = New System.Drawing.Point(2, 23)
        Me.GridGeneral.MainView = Me.ViewGeneral
        Me.GridGeneral.Name = "GridGeneral"
        Me.GridGeneral.Size = New System.Drawing.Size(533, 423)
        Me.GridGeneral.TabIndex = 10
        Me.GridGeneral.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewGeneral})
        '
        'ViewGeneral
        '
        Me.ViewGeneral.ActiveFilterEnabled = False
        Me.ViewGeneral.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewGeneral.Appearance.Row.Options.UseFont = True
        Me.ViewGeneral.GridControl = Me.GridGeneral
        Me.ViewGeneral.Name = "ViewGeneral"
        Me.ViewGeneral.OptionsBehavior.Editable = False
        Me.ViewGeneral.OptionsBehavior.ReadOnly = True
        Me.ViewGeneral.OptionsCustomization.AllowColumnMoving = False
        Me.ViewGeneral.OptionsCustomization.AllowFilter = False
        Me.ViewGeneral.OptionsCustomization.AllowGroup = False
        Me.ViewGeneral.OptionsCustomization.AllowSort = False
        Me.ViewGeneral.OptionsFilter.AllowFilterEditor = False
        Me.ViewGeneral.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewGeneral.OptionsPrint.AutoWidth = False
        Me.ViewGeneral.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewGeneral.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewGeneral.OptionsView.ColumnAutoWidth = False
        Me.ViewGeneral.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewGeneral.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewGeneral.OptionsView.ShowGroupPanel = False
        Me.ViewGeneral.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewGeneral.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'CompteA_MarcheEtTypeMarche
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1072, 498)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CompteA_MarcheEtTypeMarche"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Comptes à Marchés"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.GridType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.GridGeneral, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewGeneral, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridType As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewType As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridGeneral As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewGeneral As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BtConsultants As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtSuppType As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtSvceAssimile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtTravaux As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtFournitures As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtSuppToutType As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtMajRessources As DevExpress.XtraEditors.SimpleButton
End Class
