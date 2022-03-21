<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ModifLignePPM
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
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.txtMontant = New DevExpress.XtraEditors.TextEdit()
        Me.cmbMethode = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbTypeExamen = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtDesc = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.PanelControl7 = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtMontRestant = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtMontAffecte = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtMontTotal = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtPrct = New DevExpress.XtraEditors.TextEdit()
        Me.CmbBailleur = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.CmbConv = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TxtMontBailleur = New DevExpress.XtraEditors.TextEdit()
        Me.GridRepartBailleur = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SupprimerLaLigne = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewRepartBailleur = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BtEnrgPPSD = New DevExpress.XtraEditors.SimpleButton()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.btAnnuler = New DevExpress.XtraEditors.SimpleButton()
        Me.CodeBailleurCache = New System.Windows.Forms.TextBox()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.txtMontant.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbMethode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTypeExamen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.PanelControl7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl7.SuspendLayout()
        CType(Me.TxtMontRestant.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtMontAffecte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtMontTotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPrct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbBailleur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbConv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtMontBailleur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridRepartBailleur, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.ViewRepartBailleur, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.txtMontant)
        Me.GroupControl2.Controls.Add(Me.cmbMethode)
        Me.GroupControl2.Controls.Add(Me.Label7)
        Me.GroupControl2.Controls.Add(Me.cmbTypeExamen)
        Me.GroupControl2.Controls.Add(Me.Label6)
        Me.GroupControl2.Controls.Add(Me.Label5)
        Me.GroupControl2.Controls.Add(Me.TxtDesc)
        Me.GroupControl2.Controls.Add(Me.Label4)
        Me.GroupControl2.Location = New System.Drawing.Point(3, 4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(699, 89)
        Me.GroupControl2.TabIndex = 1
        Me.GroupControl2.Text = "Informations sur la ligne "
        '
        'txtMontant
        '
        Me.txtMontant.Location = New System.Drawing.Point(75, 56)
        Me.txtMontant.Name = "txtMontant"
        Me.txtMontant.Properties.Appearance.Options.UseTextOptions = True
        Me.txtMontant.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtMontant.Properties.Mask.EditMask = "n0"
        Me.txtMontant.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtMontant.Size = New System.Drawing.Size(233, 20)
        Me.txtMontant.TabIndex = 5
        '
        'cmbMethode
        '
        Me.cmbMethode.Location = New System.Drawing.Point(433, 60)
        Me.cmbMethode.Name = "cmbMethode"
        Me.cmbMethode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.OK)})
        Me.cmbMethode.Properties.ValidateOnEnterKey = True
        Me.cmbMethode.Size = New System.Drawing.Size(247, 20)
        Me.cmbMethode.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(314, 63)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(113, 13)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Methode de passation"
        '
        'cmbTypeExamen
        '
        Me.cmbTypeExamen.Location = New System.Drawing.Point(433, 30)
        Me.cmbTypeExamen.Name = "cmbTypeExamen"
        Me.cmbTypeExamen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.OK)})
        Me.cmbTypeExamen.Properties.Items.AddRange(New Object() {"Postériori", "Priori"})
        Me.cmbTypeExamen.Properties.ValidateOnEnterKey = True
        Me.cmbTypeExamen.Size = New System.Drawing.Size(247, 20)
        Me.cmbTypeExamen.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(314, 33)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Type examen"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Montant"
        '
        'TxtDesc
        '
        Me.TxtDesc.Location = New System.Drawing.Point(75, 30)
        Me.TxtDesc.Multiline = True
        Me.TxtDesc.Name = "TxtDesc"
        Me.TxtDesc.Size = New System.Drawing.Size(233, 20)
        Me.TxtDesc.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Description"
        '
        'GroupControl4
        '
        Me.GroupControl4.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl4.AppearanceCaption.Options.UseFont = True
        Me.GroupControl4.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl4.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl4.Controls.Add(Me.PanelControl7)
        Me.GroupControl4.Controls.Add(Me.TxtPrct)
        Me.GroupControl4.Controls.Add(Me.CmbBailleur)
        Me.GroupControl4.Controls.Add(Me.CmbConv)
        Me.GroupControl4.Controls.Add(Me.TxtMontBailleur)
        Me.GroupControl4.Controls.Add(Me.GridRepartBailleur)
        Me.GroupControl4.Location = New System.Drawing.Point(3, 99)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(699, 218)
        Me.GroupControl4.TabIndex = 21
        Me.GroupControl4.Text = "Repartion du montant par convention"
        '
        'PanelControl7
        '
        Me.PanelControl7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelControl7.Controls.Add(Me.LabelControl12)
        Me.PanelControl7.Controls.Add(Me.TxtMontRestant)
        Me.PanelControl7.Controls.Add(Me.LabelControl10)
        Me.PanelControl7.Controls.Add(Me.TxtMontAffecte)
        Me.PanelControl7.Controls.Add(Me.LabelControl9)
        Me.PanelControl7.Controls.Add(Me.TxtMontTotal)
        Me.PanelControl7.Controls.Add(Me.LabelControl8)
        Me.PanelControl7.Location = New System.Drawing.Point(0, 20)
        Me.PanelControl7.Name = "PanelControl7"
        Me.PanelControl7.Size = New System.Drawing.Size(191, 140)
        Me.PanelControl7.TabIndex = 50
        '
        'LabelControl12
        '
        Me.LabelControl12.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl12.LineLocation = DevExpress.XtraEditors.LineLocation.Top
        Me.LabelControl12.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal
        Me.LabelControl12.Location = New System.Drawing.Point(28, 0)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Size = New System.Drawing.Size(114, 15)
        Me.LabelControl12.TabIndex = 39
        Me.LabelControl12.Text = "Aperçu des montants"
        '
        'TxtMontRestant
        '
        Me.TxtMontRestant.EditValue = "0"
        Me.TxtMontRestant.Location = New System.Drawing.Point(3, 104)
        Me.TxtMontRestant.Name = "TxtMontRestant"
        Me.TxtMontRestant.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMontRestant.Properties.Appearance.Options.UseFont = True
        Me.TxtMontRestant.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtMontRestant.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.TxtMontRestant.Properties.DisplayFormat.FormatString = "n0"
        Me.TxtMontRestant.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.TxtMontRestant.Properties.EditFormat.FormatString = "n0"
        Me.TxtMontRestant.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.TxtMontRestant.Properties.Mask.BeepOnError = True
        Me.TxtMontRestant.Properties.Mask.EditMask = "n0"
        Me.TxtMontRestant.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TxtMontRestant.Properties.ReadOnly = True
        Me.TxtMontRestant.Size = New System.Drawing.Size(183, 22)
        Me.TxtMontRestant.TabIndex = 38
        '
        'LabelControl10
        '
        Me.LabelControl10.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl10.Location = New System.Drawing.Point(6, 89)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(92, 15)
        Me.LabelControl10.TabIndex = 37
        Me.LabelControl10.Text = "Montant Restant"
        '
        'TxtMontAffecte
        '
        Me.TxtMontAffecte.EditValue = "0"
        Me.TxtMontAffecte.Location = New System.Drawing.Point(3, 66)
        Me.TxtMontAffecte.Name = "TxtMontAffecte"
        Me.TxtMontAffecte.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMontAffecte.Properties.Appearance.Options.UseFont = True
        Me.TxtMontAffecte.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtMontAffecte.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.TxtMontAffecte.Properties.DisplayFormat.FormatString = "n0"
        Me.TxtMontAffecte.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.TxtMontAffecte.Properties.EditFormat.FormatString = "n0"
        Me.TxtMontAffecte.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.TxtMontAffecte.Properties.Mask.BeepOnError = True
        Me.TxtMontAffecte.Properties.Mask.EditMask = "n0"
        Me.TxtMontAffecte.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TxtMontAffecte.Properties.ReadOnly = True
        Me.TxtMontAffecte.Size = New System.Drawing.Size(183, 22)
        Me.TxtMontAffecte.TabIndex = 36
        '
        'LabelControl9
        '
        Me.LabelControl9.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl9.Location = New System.Drawing.Point(6, 51)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(86, 15)
        Me.LabelControl9.TabIndex = 35
        Me.LabelControl9.Text = "Montant Affecté"
        '
        'TxtMontTotal
        '
        Me.TxtMontTotal.EditValue = "0"
        Me.TxtMontTotal.Location = New System.Drawing.Point(3, 29)
        Me.TxtMontTotal.Name = "TxtMontTotal"
        Me.TxtMontTotal.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMontTotal.Properties.Appearance.Options.UseFont = True
        Me.TxtMontTotal.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtMontTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.TxtMontTotal.Properties.DisplayFormat.FormatString = "n0"
        Me.TxtMontTotal.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.TxtMontTotal.Properties.EditFormat.FormatString = "n0"
        Me.TxtMontTotal.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.TxtMontTotal.Properties.Mask.BeepOnError = True
        Me.TxtMontTotal.Properties.Mask.EditMask = "n0"
        Me.TxtMontTotal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TxtMontTotal.Properties.ReadOnly = True
        Me.TxtMontTotal.Size = New System.Drawing.Size(183, 22)
        Me.TxtMontTotal.TabIndex = 34
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl8.Location = New System.Drawing.Point(6, 14)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(80, 15)
        Me.LabelControl8.TabIndex = 2
        Me.LabelControl8.Text = "Montant Total"
        '
        'TxtPrct
        '
        Me.TxtPrct.EditValue = "0"
        Me.TxtPrct.Location = New System.Drawing.Point(649, 26)
        Me.TxtPrct.Name = "TxtPrct"
        Me.TxtPrct.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPrct.Properties.Appearance.Options.UseFont = True
        Me.TxtPrct.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtPrct.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtPrct.Properties.DisplayFormat.FormatString = "n"
        Me.TxtPrct.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.TxtPrct.Properties.EditFormat.FormatString = "n"
        Me.TxtPrct.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.TxtPrct.Properties.Mask.BeepOnError = True
        Me.TxtPrct.Properties.Mask.EditMask = "n"
        Me.TxtPrct.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TxtPrct.Properties.ReadOnly = True
        Me.TxtPrct.Size = New System.Drawing.Size(47, 22)
        Me.TxtPrct.TabIndex = 49
        '
        'CmbBailleur
        '
        Me.CmbBailleur.Location = New System.Drawing.Point(209, 26)
        Me.CmbBailleur.Name = "CmbBailleur"
        Me.CmbBailleur.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbBailleur.Properties.Appearance.Options.UseFont = True
        Me.CmbBailleur.Properties.Appearance.Options.UseTextOptions = True
        Me.CmbBailleur.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CmbBailleur.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbBailleur.Size = New System.Drawing.Size(104, 22)
        Me.CmbBailleur.TabIndex = 7
        '
        'CmbConv
        '
        Me.CmbConv.Location = New System.Drawing.Point(319, 26)
        Me.CmbConv.Name = "CmbConv"
        Me.CmbConv.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbConv.Properties.Appearance.Options.UseFont = True
        Me.CmbConv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbConv.Size = New System.Drawing.Size(173, 22)
        Me.CmbConv.TabIndex = 8
        '
        'TxtMontBailleur
        '
        Me.TxtMontBailleur.EditValue = "0"
        Me.TxtMontBailleur.Location = New System.Drawing.Point(498, 26)
        Me.TxtMontBailleur.Name = "TxtMontBailleur"
        Me.TxtMontBailleur.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMontBailleur.Properties.Appearance.Options.UseFont = True
        Me.TxtMontBailleur.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtMontBailleur.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.TxtMontBailleur.Properties.DisplayFormat.FormatString = "n0"
        Me.TxtMontBailleur.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.TxtMontBailleur.Properties.EditFormat.FormatString = "n0"
        Me.TxtMontBailleur.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.TxtMontBailleur.Properties.Mask.BeepOnError = True
        Me.TxtMontBailleur.Properties.Mask.EditMask = "n0"
        Me.TxtMontBailleur.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TxtMontBailleur.Properties.Mask.PlaceHolder = Global.Microsoft.VisualBasic.ChrW(48)
        Me.TxtMontBailleur.Properties.MaxLength = 15
        Me.TxtMontBailleur.Properties.NullText = "0"
        Me.TxtMontBailleur.Size = New System.Drawing.Size(145, 22)
        Me.TxtMontBailleur.TabIndex = 9
        '
        'GridRepartBailleur
        '
        Me.GridRepartBailleur.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GridRepartBailleur.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridRepartBailleur.Location = New System.Drawing.Point(209, 54)
        Me.GridRepartBailleur.MainView = Me.ViewRepartBailleur
        Me.GridRepartBailleur.Name = "GridRepartBailleur"
        Me.GridRepartBailleur.Size = New System.Drawing.Size(490, 159)
        Me.GridRepartBailleur.TabIndex = 46
        Me.GridRepartBailleur.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewRepartBailleur})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SupprimerLaLigne})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(171, 26)
        '
        'SupprimerLaLigne
        '
        Me.SupprimerLaLigne.Image = Global.ClearProject.My.Resources.Resources.Delete_16x16
        Me.SupprimerLaLigne.Name = "SupprimerLaLigne"
        Me.SupprimerLaLigne.Size = New System.Drawing.Size(170, 22)
        Me.SupprimerLaLigne.Text = "Supprimer la ligne"
        '
        'ViewRepartBailleur
        '
        Me.ViewRepartBailleur.ActiveFilterEnabled = False
        Me.ViewRepartBailleur.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewRepartBailleur.Appearance.Row.Options.UseFont = True
        Me.ViewRepartBailleur.GridControl = Me.GridRepartBailleur
        Me.ViewRepartBailleur.Name = "ViewRepartBailleur"
        Me.ViewRepartBailleur.OptionsBehavior.Editable = False
        Me.ViewRepartBailleur.OptionsBehavior.ReadOnly = True
        Me.ViewRepartBailleur.OptionsCustomization.AllowColumnMoving = False
        Me.ViewRepartBailleur.OptionsCustomization.AllowFilter = False
        Me.ViewRepartBailleur.OptionsCustomization.AllowGroup = False
        Me.ViewRepartBailleur.OptionsCustomization.AllowSort = False
        Me.ViewRepartBailleur.OptionsFilter.AllowFilterEditor = False
        Me.ViewRepartBailleur.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewRepartBailleur.OptionsPrint.AutoWidth = False
        Me.ViewRepartBailleur.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewRepartBailleur.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewRepartBailleur.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewRepartBailleur.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewRepartBailleur.OptionsView.ShowGroupPanel = False
        Me.ViewRepartBailleur.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewRepartBailleur.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'BtEnrgPPSD
        '
        Me.BtEnrgPPSD.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnrgPPSD.Appearance.Options.UseFont = True
        Me.BtEnrgPPSD.Image = Global.ClearProject.My.Resources.Resources.ActiveRents_16x16
        Me.BtEnrgPPSD.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft
        Me.BtEnrgPPSD.Location = New System.Drawing.Point(369, 323)
        Me.BtEnrgPPSD.Name = "BtEnrgPPSD"
        Me.BtEnrgPPSD.Size = New System.Drawing.Size(158, 33)
        Me.BtEnrgPPSD.TabIndex = 11
        Me.BtEnrgPPSD.Text = "Modifier"
        '
        'GridView2
        '
        Me.GridView2.Name = "GridView2"
        '
        'btAnnuler
        '
        Me.btAnnuler.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btAnnuler.Appearance.Options.UseFont = True
        Me.btAnnuler.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_161
        Me.btAnnuler.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft
        Me.btAnnuler.Location = New System.Drawing.Point(177, 323)
        Me.btAnnuler.Name = "btAnnuler"
        Me.btAnnuler.Size = New System.Drawing.Size(158, 33)
        Me.btAnnuler.TabIndex = 12
        Me.btAnnuler.Text = "Annuler"
        '
        'CodeBailleurCache
        '
        Me.CodeBailleurCache.Location = New System.Drawing.Point(53, 335)
        Me.CodeBailleurCache.Name = "CodeBailleurCache"
        Me.CodeBailleurCache.Size = New System.Drawing.Size(48, 21)
        Me.CodeBailleurCache.TabIndex = 105
        Me.CodeBailleurCache.Visible = False
        '
        'ModifLignePPM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(705, 370)
        Me.Controls.Add(Me.CodeBailleurCache)
        Me.Controls.Add(Me.GroupControl4)
        Me.Controls.Add(Me.btAnnuler)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.BtEnrgPPSD)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ModifLignePPM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Modifier ligne PPM"
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.txtMontant.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbMethode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTypeExamen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        CType(Me.PanelControl7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl7.ResumeLayout(False)
        Me.PanelControl7.PerformLayout()
        CType(Me.TxtMontRestant.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtMontAffecte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtMontTotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPrct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbBailleur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbConv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtMontBailleur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridRepartBailleur, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.ViewRepartBailleur, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtDesc As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cmbTypeExamen As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbMethode As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents Label7 As Label
    Friend WithEvents txtMontant As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BtEnrgPPSD As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TxtPrct As DevExpress.XtraEditors.TextEdit
    Friend WithEvents CmbConv As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents TxtMontBailleur As DevExpress.XtraEditors.TextEdit
    Friend WithEvents CmbBailleur As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents GridRepartBailleur As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewRepartBailleur As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btAnnuler As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelControl7 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtMontRestant As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtMontAffecte As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtMontTotal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SupprimerLaLigne As ToolStripMenuItem
    Friend WithEvents CodeBailleurCache As TextBox
End Class
