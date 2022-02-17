<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PlanDecaissement
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
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.GridEcheanceActivite = New DevExpress.XtraGrid.GridControl()
        Me.ViewEcheanceActivite = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.txtGAP = New DevExpress.XtraEditors.LabelControl()
        Me.TxtMontantBailleur = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtCreerPlan = New DevExpress.XtraEditors.SimpleButton()
        Me.CmbMois = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TxtMois = New DevExpress.XtraEditors.LabelControl()
        Me.cmbBailleur = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.CmbJour = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TxtJour = New DevExpress.XtraEditors.LabelControl()
        Me.RdAutre = New DevExpress.XtraEditors.CheckEdit()
        Me.RdAns = New DevExpress.XtraEditors.CheckEdit()
        Me.RdMois = New DevExpress.XtraEditors.CheckEdit()
        Me.TxtTitre = New DevExpress.XtraEditors.LabelControl()
        Me.GridEcheanceMontant = New DevExpress.XtraGrid.GridControl()
        Me.MenuStripRepartition = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SupprimerPJ = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewEcheanceMontant = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl6 = New DevExpress.XtraEditors.PanelControl()
        Me.TxtResteEcheance = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.TxtMontantEcheance = New DevExpress.XtraEditors.TextEdit()
        Me.DTDateEcheance = New DevExpress.XtraEditors.DateEdit()
        Me.BtAjouterEcheance = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.GridEcheanceActivite, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewEcheanceActivite, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.CmbMois.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBailleur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbJour.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RdAutre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RdAns.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RdMois.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridEcheanceMontant, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripRepartition.SuspendLayout()
        CType(Me.ViewEcheanceMontant, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl6.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.TxtMontantEcheance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DTDateEcheance.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DTDateEcheance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.GridEcheanceActivite)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.PanelControl3)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.PanelControl1)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.GridEcheanceMontant)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.PanelControl6)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.PanelControl2)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1370, 423)
        Me.SplitContainerControl1.SplitterPosition = 250
        Me.SplitContainerControl1.TabIndex = 0
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'GridEcheanceActivite
        '
        Me.GridEcheanceActivite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEcheanceActivite.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEcheanceActivite.Location = New System.Drawing.Point(0, 41)
        Me.GridEcheanceActivite.MainView = Me.ViewEcheanceActivite
        Me.GridEcheanceActivite.Name = "GridEcheanceActivite"
        Me.GridEcheanceActivite.Size = New System.Drawing.Size(1115, 353)
        Me.GridEcheanceActivite.TabIndex = 13
        Me.GridEcheanceActivite.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewEcheanceActivite})
        '
        'ViewEcheanceActivite
        '
        Me.ViewEcheanceActivite.ActiveFilterEnabled = False
        Me.ViewEcheanceActivite.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewEcheanceActivite.Appearance.Row.Options.UseFont = True
        Me.ViewEcheanceActivite.GridControl = Me.GridEcheanceActivite
        Me.ViewEcheanceActivite.Name = "ViewEcheanceActivite"
        Me.ViewEcheanceActivite.OptionsBehavior.Editable = False
        Me.ViewEcheanceActivite.OptionsBehavior.ReadOnly = True
        Me.ViewEcheanceActivite.OptionsCustomization.AllowColumnMoving = False
        Me.ViewEcheanceActivite.OptionsCustomization.AllowFilter = False
        Me.ViewEcheanceActivite.OptionsCustomization.AllowGroup = False
        Me.ViewEcheanceActivite.OptionsCustomization.AllowSort = False
        Me.ViewEcheanceActivite.OptionsFilter.AllowFilterEditor = False
        Me.ViewEcheanceActivite.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewEcheanceActivite.OptionsPrint.AutoWidth = False
        Me.ViewEcheanceActivite.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewEcheanceActivite.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewEcheanceActivite.OptionsView.ColumnAutoWidth = False
        Me.ViewEcheanceActivite.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewEcheanceActivite.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewEcheanceActivite.OptionsView.ShowGroupPanel = False
        Me.ViewEcheanceActivite.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewEcheanceActivite.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.txtGAP)
        Me.PanelControl3.Controls.Add(Me.TxtMontantBailleur)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl3.Location = New System.Drawing.Point(0, 394)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(1115, 29)
        Me.PanelControl3.TabIndex = 1
        '
        'txtGAP
        '
        Me.txtGAP.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGAP.Dock = System.Windows.Forms.DockStyle.Right
        Me.txtGAP.Location = New System.Drawing.Point(1101, 2)
        Me.txtGAP.Name = "txtGAP"
        Me.txtGAP.Size = New System.Drawing.Size(12, 17)
        Me.txtGAP.TabIndex = 1
        Me.txtGAP.Text = "..."
        '
        'TxtMontantBailleur
        '
        Me.TxtMontantBailleur.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMontantBailleur.Dock = System.Windows.Forms.DockStyle.Left
        Me.TxtMontantBailleur.Location = New System.Drawing.Point(2, 2)
        Me.TxtMontantBailleur.Name = "TxtMontantBailleur"
        Me.TxtMontantBailleur.Size = New System.Drawing.Size(0, 17)
        Me.TxtMontantBailleur.TabIndex = 1
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.Label1)
        Me.PanelControl1.Controls.Add(Me.BtCreerPlan)
        Me.PanelControl1.Controls.Add(Me.CmbMois)
        Me.PanelControl1.Controls.Add(Me.TxtMois)
        Me.PanelControl1.Controls.Add(Me.cmbBailleur)
        Me.PanelControl1.Controls.Add(Me.CmbJour)
        Me.PanelControl1.Controls.Add(Me.TxtJour)
        Me.PanelControl1.Controls.Add(Me.RdAutre)
        Me.PanelControl1.Controls.Add(Me.RdAns)
        Me.PanelControl1.Controls.Add(Me.RdMois)
        Me.PanelControl1.Controls.Add(Me.TxtTitre)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1115, 41)
        Me.PanelControl1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Bailleur"
        '
        'BtCreerPlan
        '
        Me.BtCreerPlan.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtCreerPlan.Location = New System.Drawing.Point(1031, 2)
        Me.BtCreerPlan.Name = "BtCreerPlan"
        Me.BtCreerPlan.Size = New System.Drawing.Size(82, 37)
        Me.BtCreerPlan.TabIndex = 8
        Me.BtCreerPlan.Text = "Créer Plan"
        '
        'CmbMois
        '
        Me.CmbMois.Location = New System.Drawing.Point(849, 13)
        Me.CmbMois.Name = "CmbMois"
        Me.CmbMois.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbMois.Properties.Appearance.Options.UseFont = True
        Me.CmbMois.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbMois.Properties.Items.AddRange(New Object() {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"})
        Me.CmbMois.Size = New System.Drawing.Size(46, 22)
        Me.CmbMois.TabIndex = 7
        '
        'TxtMois
        '
        Me.TxtMois.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMois.Location = New System.Drawing.Point(812, 14)
        Me.TxtMois.Name = "TxtMois"
        Me.TxtMois.Size = New System.Drawing.Size(34, 17)
        Me.TxtMois.TabIndex = 6
        Me.TxtMois.Text = "Mois"
        '
        'cmbBailleur
        '
        Me.cmbBailleur.Location = New System.Drawing.Point(49, 10)
        Me.cmbBailleur.Name = "cmbBailleur"
        Me.cmbBailleur.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBailleur.Properties.Appearance.Options.UseFont = True
        Me.cmbBailleur.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBailleur.Properties.Items.AddRange(New Object() {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})
        Me.cmbBailleur.Size = New System.Drawing.Size(294, 22)
        Me.cmbBailleur.TabIndex = 5
        '
        'CmbJour
        '
        Me.CmbJour.Location = New System.Drawing.Point(751, 13)
        Me.CmbJour.Name = "CmbJour"
        Me.CmbJour.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbJour.Properties.Appearance.Options.UseFont = True
        Me.CmbJour.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbJour.Properties.Items.AddRange(New Object() {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})
        Me.CmbJour.Size = New System.Drawing.Size(46, 22)
        Me.CmbJour.TabIndex = 5
        '
        'TxtJour
        '
        Me.TxtJour.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtJour.Location = New System.Drawing.Point(717, 14)
        Me.TxtJour.Name = "TxtJour"
        Me.TxtJour.Size = New System.Drawing.Size(30, 17)
        Me.TxtJour.TabIndex = 4
        Me.TxtJour.Text = "Jour"
        '
        'RdAutre
        '
        Me.RdAutre.Location = New System.Drawing.Point(594, 11)
        Me.RdAutre.Name = "RdAutre"
        Me.RdAutre.Properties.AllowFocused = False
        Me.RdAutre.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RdAutre.Properties.Appearance.Options.UseFont = True
        Me.RdAutre.Properties.Caption = "Personnaliser"
        Me.RdAutre.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.RdAutre.Properties.RadioGroupIndex = 1
        Me.RdAutre.Size = New System.Drawing.Size(118, 22)
        Me.RdAutre.TabIndex = 3
        Me.RdAutre.TabStop = False
        '
        'RdAns
        '
        Me.RdAns.Location = New System.Drawing.Point(525, 11)
        Me.RdAns.Name = "RdAns"
        Me.RdAns.Properties.AllowFocused = False
        Me.RdAns.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RdAns.Properties.Appearance.Options.UseFont = True
        Me.RdAns.Properties.Caption = "Année"
        Me.RdAns.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.RdAns.Properties.RadioGroupIndex = 1
        Me.RdAns.Size = New System.Drawing.Size(70, 22)
        Me.RdAns.TabIndex = 2
        Me.RdAns.TabStop = False
        '
        'RdMois
        '
        Me.RdMois.Location = New System.Drawing.Point(466, 11)
        Me.RdMois.Name = "RdMois"
        Me.RdMois.Properties.AllowFocused = False
        Me.RdMois.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RdMois.Properties.Appearance.Options.UseFont = True
        Me.RdMois.Properties.Caption = "Mois"
        Me.RdMois.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.RdMois.Properties.RadioGroupIndex = 1
        Me.RdMois.Size = New System.Drawing.Size(62, 22)
        Me.RdMois.TabIndex = 1
        Me.RdMois.TabStop = False
        '
        'TxtTitre
        '
        Me.TxtTitre.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTitre.Location = New System.Drawing.Point(352, 11)
        Me.TxtTitre.Name = "TxtTitre"
        Me.TxtTitre.Size = New System.Drawing.Size(108, 17)
        Me.TxtTitre.TabIndex = 0
        Me.TxtTitre.Text = "Répartition par :"
        '
        'GridEcheanceMontant
        '
        Me.GridEcheanceMontant.ContextMenuStrip = Me.MenuStripRepartition
        Me.GridEcheanceMontant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEcheanceMontant.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEcheanceMontant.Location = New System.Drawing.Point(0, 41)
        Me.GridEcheanceMontant.MainView = Me.ViewEcheanceMontant
        Me.GridEcheanceMontant.Name = "GridEcheanceMontant"
        Me.GridEcheanceMontant.Size = New System.Drawing.Size(250, 357)
        Me.GridEcheanceMontant.TabIndex = 13
        Me.GridEcheanceMontant.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewEcheanceMontant})
        '
        'MenuStripRepartition
        '
        Me.MenuStripRepartition.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SupprimerPJ})
        Me.MenuStripRepartition.Name = "ContextMenuStrip2"
        Me.MenuStripRepartition.Size = New System.Drawing.Size(130, 26)
        '
        'SupprimerPJ
        '
        Me.SupprimerPJ.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.SupprimerPJ.Name = "SupprimerPJ"
        Me.SupprimerPJ.Size = New System.Drawing.Size(129, 22)
        Me.SupprimerPJ.Text = "Supprimer"
        '
        'ViewEcheanceMontant
        '
        Me.ViewEcheanceMontant.ActiveFilterEnabled = False
        Me.ViewEcheanceMontant.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewEcheanceMontant.Appearance.Row.Options.UseFont = True
        Me.ViewEcheanceMontant.GridControl = Me.GridEcheanceMontant
        Me.ViewEcheanceMontant.Name = "ViewEcheanceMontant"
        Me.ViewEcheanceMontant.OptionsBehavior.Editable = False
        Me.ViewEcheanceMontant.OptionsBehavior.ReadOnly = True
        Me.ViewEcheanceMontant.OptionsCustomization.AllowColumnMoving = False
        Me.ViewEcheanceMontant.OptionsCustomization.AllowGroup = False
        Me.ViewEcheanceMontant.OptionsCustomization.AllowSort = False
        Me.ViewEcheanceMontant.OptionsPrint.AutoWidth = False
        Me.ViewEcheanceMontant.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewEcheanceMontant.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewEcheanceMontant.OptionsView.ColumnAutoWidth = False
        Me.ViewEcheanceMontant.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewEcheanceMontant.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewEcheanceMontant.OptionsView.ShowGroupPanel = False
        Me.ViewEcheanceMontant.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewEcheanceMontant.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'PanelControl6
        '
        Me.PanelControl6.Controls.Add(Me.TxtResteEcheance)
        Me.PanelControl6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl6.Location = New System.Drawing.Point(0, 398)
        Me.PanelControl6.Name = "PanelControl6"
        Me.PanelControl6.Size = New System.Drawing.Size(250, 25)
        Me.PanelControl6.TabIndex = 3
        '
        'TxtResteEcheance
        '
        Me.TxtResteEcheance.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtResteEcheance.Location = New System.Drawing.Point(5, 3)
        Me.TxtResteEcheance.Name = "TxtResteEcheance"
        Me.TxtResteEcheance.Size = New System.Drawing.Size(113, 17)
        Me.TxtResteEcheance.TabIndex = 2
        Me.TxtResteEcheance.Text = "Reste à répartir :"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.TxtMontantEcheance)
        Me.PanelControl2.Controls.Add(Me.DTDateEcheance)
        Me.PanelControl2.Controls.Add(Me.BtAjouterEcheance)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(250, 41)
        Me.PanelControl2.TabIndex = 0
        '
        'TxtMontantEcheance
        '
        Me.TxtMontantEcheance.Enabled = False
        Me.TxtMontantEcheance.Location = New System.Drawing.Point(130, 12)
        Me.TxtMontantEcheance.Name = "TxtMontantEcheance"
        Me.TxtMontantEcheance.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMontantEcheance.Properties.Appearance.Options.UseFont = True
        Me.TxtMontantEcheance.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtMontantEcheance.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.TxtMontantEcheance.Properties.Mask.EditMask = "d"
        Me.TxtMontantEcheance.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TxtMontantEcheance.Properties.MaxLength = 15
        Me.TxtMontantEcheance.Size = New System.Drawing.Size(114, 22)
        Me.TxtMontantEcheance.TabIndex = 11
        '
        'DTDateEcheance
        '
        Me.DTDateEcheance.EditValue = Nothing
        Me.DTDateEcheance.Enabled = False
        Me.DTDateEcheance.Location = New System.Drawing.Point(37, 12)
        Me.DTDateEcheance.Name = "DTDateEcheance"
        Me.DTDateEcheance.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTDateEcheance.Properties.Appearance.Options.UseFont = True
        Me.DTDateEcheance.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DTDateEcheance.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.DTDateEcheance.Size = New System.Drawing.Size(87, 20)
        Me.DTDateEcheance.TabIndex = 10
        '
        'BtAjouterEcheance
        '
        Me.BtAjouterEcheance.Image = Global.ClearProject.My.Resources.Resources.Add_16x16
        Me.BtAjouterEcheance.Location = New System.Drawing.Point(5, 12)
        Me.BtAjouterEcheance.Name = "BtAjouterEcheance"
        Me.BtAjouterEcheance.Size = New System.Drawing.Size(26, 22)
        Me.BtAjouterEcheance.TabIndex = 9
        '
        'PlanDecaissement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1370, 423)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Name = "PlanDecaissement"
        Me.Text = "Répartition périodique des activités"
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.GridEcheanceActivite, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewEcheanceActivite, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        Me.PanelControl3.PerformLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.CmbMois.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBailleur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbJour.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RdAutre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RdAns.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RdMois.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridEcheanceMontant, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripRepartition.ResumeLayout(False)
        CType(Me.ViewEcheanceMontant, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl6.ResumeLayout(False)
        Me.PanelControl6.PerformLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.TxtMontantEcheance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DTDateEcheance.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DTDateEcheance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl6 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GridEcheanceActivite As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewEcheanceActivite As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CmbJour As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents TxtJour As DevExpress.XtraEditors.LabelControl
    Friend WithEvents RdAutre As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents RdAns As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents RdMois As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents TxtTitre As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GridEcheanceMontant As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewEcheanceMontant As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BtCreerPlan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CmbMois As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents TxtMois As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtMontantEcheance As DevExpress.XtraEditors.TextEdit
    Friend WithEvents DTDateEcheance As DevExpress.XtraEditors.DateEdit
    Friend WithEvents BtAjouterEcheance As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtMontantBailleur As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtResteEcheance As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbBailleur As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtGAP As DevExpress.XtraEditors.LabelControl
    Friend WithEvents MenuStripRepartition As ContextMenuStrip
    Friend WithEvents SupprimerPJ As ToolStripMenuItem
End Class
