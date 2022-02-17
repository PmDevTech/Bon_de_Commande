<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Zonegeo
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.TxtCodeZone = New System.Windows.Forms.TextBox()
        Me.TxtNiveauStrCache = New System.Windows.Forms.TextBox()
        Me.TxtCodeZoneMereCache = New System.Windows.Forms.TextBox()
        Me.TxtCodeDeviseCache = New System.Windows.Forms.TextBox()
        Me.BtRetour = New DevExpress.XtraEditors.SimpleButton()
        Me.BtEnregistrer = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.CmbPays_de = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.CmbIssu_de = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ActualiserDevise = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.CmbDevise = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TxtTva = New DevExpress.XtraEditors.TextEdit()
        Me.TxtIndicatifZone = New DevExpress.XtraEditors.TextEdit()
        Me.TxtAbrege = New DevExpress.XtraEditors.TextEdit()
        Me.TxtNomZone = New DevExpress.XtraEditors.TextEdit()
        Me.CmbTypZone = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.TreeList1 = New DevExpress.XtraTreeList.TreeList()
        Me.Code = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.Indicatif = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.NomZone = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.Devises = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.Tva = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.Type = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.CodeZone = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.CodeZoneMere = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.Loaded = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ModifierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SupprimerLaLigneSelectionnerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.CmbPays_de.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbIssu_de.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbDevise.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtIndicatifZone.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAbrege.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtNomZone.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbTypZone.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.TreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.TxtCodeZone)
        Me.PanelControl1.Controls.Add(Me.TxtNiveauStrCache)
        Me.PanelControl1.Controls.Add(Me.TxtCodeZoneMereCache)
        Me.PanelControl1.Controls.Add(Me.TxtCodeDeviseCache)
        Me.PanelControl1.Controls.Add(Me.BtRetour)
        Me.PanelControl1.Controls.Add(Me.BtEnregistrer)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(966, 44)
        Me.PanelControl1.TabIndex = 1
        '
        'TxtCodeZone
        '
        Me.TxtCodeZone.Enabled = False
        Me.TxtCodeZone.Location = New System.Drawing.Point(587, 13)
        Me.TxtCodeZone.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TxtCodeZone.MaxLength = 4
        Me.TxtCodeZone.Name = "TxtCodeZone"
        Me.TxtCodeZone.Size = New System.Drawing.Size(87, 21)
        Me.TxtCodeZone.TabIndex = 23
        Me.TxtCodeZone.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtCodeZone.Visible = False
        '
        'TxtNiveauStrCache
        '
        Me.TxtNiveauStrCache.Enabled = False
        Me.TxtNiveauStrCache.Location = New System.Drawing.Point(310, 13)
        Me.TxtNiveauStrCache.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TxtNiveauStrCache.MaxLength = 4
        Me.TxtNiveauStrCache.Name = "TxtNiveauStrCache"
        Me.TxtNiveauStrCache.Size = New System.Drawing.Size(87, 21)
        Me.TxtNiveauStrCache.TabIndex = 22
        Me.TxtNiveauStrCache.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtNiveauStrCache.Visible = False
        '
        'TxtCodeZoneMereCache
        '
        Me.TxtCodeZoneMereCache.Enabled = False
        Me.TxtCodeZoneMereCache.Location = New System.Drawing.Point(495, 13)
        Me.TxtCodeZoneMereCache.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TxtCodeZoneMereCache.MaxLength = 4
        Me.TxtCodeZoneMereCache.Name = "TxtCodeZoneMereCache"
        Me.TxtCodeZoneMereCache.Size = New System.Drawing.Size(87, 21)
        Me.TxtCodeZoneMereCache.TabIndex = 21
        Me.TxtCodeZoneMereCache.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtCodeZoneMereCache.Visible = False
        '
        'TxtCodeDeviseCache
        '
        Me.TxtCodeDeviseCache.Enabled = False
        Me.TxtCodeDeviseCache.Location = New System.Drawing.Point(403, 13)
        Me.TxtCodeDeviseCache.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TxtCodeDeviseCache.MaxLength = 4
        Me.TxtCodeDeviseCache.Name = "TxtCodeDeviseCache"
        Me.TxtCodeDeviseCache.Size = New System.Drawing.Size(87, 21)
        Me.TxtCodeDeviseCache.TabIndex = 20
        Me.TxtCodeDeviseCache.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtCodeDeviseCache.Visible = False
        '
        'BtRetour
        '
        Me.BtRetour.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_32
        Me.BtRetour.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtRetour.Location = New System.Drawing.Point(878, 3)
        Me.BtRetour.Name = "BtRetour"
        Me.BtRetour.Size = New System.Drawing.Size(39, 39)
        Me.BtRetour.TabIndex = 8
        Me.BtRetour.ToolTip = "Retour"
        '
        'BtEnregistrer
        '
        Me.BtEnregistrer.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_32x32
        Me.BtEnregistrer.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtEnregistrer.Location = New System.Drawing.Point(920, 3)
        Me.BtEnregistrer.Name = "BtEnregistrer"
        Me.BtEnregistrer.Size = New System.Drawing.Size(39, 39)
        Me.BtEnregistrer.TabIndex = 7
        Me.BtEnregistrer.ToolTip = "Enregistrer"
        '
        'GroupControl1
        '
        Me.GroupControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.857143!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.Appearance.Options.UseFont = True
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.857143!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.CmbPays_de)
        Me.GroupControl1.Controls.Add(Me.CmbIssu_de)
        Me.GroupControl1.Controls.Add(Me.ActualiserDevise)
        Me.GroupControl1.Controls.Add(Me.LabelControl7)
        Me.GroupControl1.Controls.Add(Me.LabelControl6)
        Me.GroupControl1.Controls.Add(Me.LabelControl5)
        Me.GroupControl1.Controls.Add(Me.LabelControl4)
        Me.GroupControl1.Controls.Add(Me.LabelControl3)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.CmbDevise)
        Me.GroupControl1.Controls.Add(Me.TxtTva)
        Me.GroupControl1.Controls.Add(Me.TxtIndicatifZone)
        Me.GroupControl1.Controls.Add(Me.TxtAbrege)
        Me.GroupControl1.Controls.Add(Me.TxtNomZone)
        Me.GroupControl1.Controls.Add(Me.CmbTypZone)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 44)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(966, 170)
        Me.GroupControl1.TabIndex = 2
        Me.GroupControl1.Text = "Localisation"
        '
        'CmbPays_de
        '
        Me.CmbPays_de.Location = New System.Drawing.Point(535, 44)
        Me.CmbPays_de.Margin = New System.Windows.Forms.Padding(2)
        Me.CmbPays_de.Name = "CmbPays_de"
        Me.CmbPays_de.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbPays_de.Properties.Appearance.Options.UseFont = True
        Me.CmbPays_de.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbPays_de.Size = New System.Drawing.Size(214, 26)
        Me.CmbPays_de.TabIndex = 15
        '
        'CmbIssu_de
        '
        Me.CmbIssu_de.Location = New System.Drawing.Point(753, 44)
        Me.CmbIssu_de.Margin = New System.Windows.Forms.Padding(2)
        Me.CmbIssu_de.Name = "CmbIssu_de"
        Me.CmbIssu_de.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbIssu_de.Properties.Appearance.Options.UseFont = True
        Me.CmbIssu_de.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbIssu_de.Size = New System.Drawing.Size(202, 26)
        Me.CmbIssu_de.TabIndex = 1
        '
        'ActualiserDevise
        '
        Me.ActualiserDevise.Image = Global.ClearProject.My.Resources.Resources.Add_16x16
        Me.ActualiserDevise.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.ActualiserDevise.Location = New System.Drawing.Point(431, 46)
        Me.ActualiserDevise.Margin = New System.Windows.Forms.Padding(2)
        Me.ActualiserDevise.Name = "ActualiserDevise"
        Me.ActualiserDevise.Size = New System.Drawing.Size(25, 26)
        Me.ActualiserDevise.TabIndex = 14
        '
        'LabelControl7
        '
        Me.LabelControl7.Location = New System.Drawing.Point(488, 125)
        Me.LabelControl7.Margin = New System.Windows.Forms.Padding(2)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(19, 13)
        Me.LabelControl7.TabIndex = 13
        Me.LabelControl7.Text = "TVA"
        '
        'LabelControl6
        '
        Me.LabelControl6.Location = New System.Drawing.Point(488, 90)
        Me.LabelControl6.Margin = New System.Windows.Forms.Padding(2)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(35, 13)
        Me.LabelControl6.TabIndex = 12
        Me.LabelControl6.Text = "Abrégé"
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(488, 51)
        Me.LabelControl5.Margin = New System.Windows.Forms.Padding(2)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(35, 13)
        Me.LabelControl5.TabIndex = 11
        Me.LabelControl5.Text = "Issu de"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(311, 51)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(2)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(32, 13)
        Me.LabelControl4.TabIndex = 10
        Me.LabelControl4.Text = "Devise"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(14, 129)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(2)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(39, 13)
        Me.LabelControl3.TabIndex = 9
        Me.LabelControl3.Text = "Indicatif"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(14, 90)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(2)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(48, 13)
        Me.LabelControl2.TabIndex = 8
        Me.LabelControl2.Text = "Nom Zone"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(14, 51)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(51, 13)
        Me.LabelControl1.TabIndex = 7
        Me.LabelControl1.Text = "Type Zone"
        '
        'CmbDevise
        '
        Me.CmbDevise.Location = New System.Drawing.Point(347, 46)
        Me.CmbDevise.Margin = New System.Windows.Forms.Padding(2)
        Me.CmbDevise.Name = "CmbDevise"
        Me.CmbDevise.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbDevise.Properties.Appearance.Options.UseFont = True
        Me.CmbDevise.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbDevise.Size = New System.Drawing.Size(80, 26)
        Me.CmbDevise.TabIndex = 6
        '
        'TxtTva
        '
        Me.TxtTva.Location = New System.Drawing.Point(535, 120)
        Me.TxtTva.Margin = New System.Windows.Forms.Padding(2)
        Me.TxtTva.Name = "TxtTva"
        Me.TxtTva.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTva.Properties.Appearance.Options.UseFont = True
        Me.TxtTva.Properties.Mask.EditMask = "P0"
        Me.TxtTva.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TxtTva.Size = New System.Drawing.Size(420, 26)
        Me.TxtTva.TabIndex = 5
        '
        'TxtIndicatifZone
        '
        Me.TxtIndicatifZone.Location = New System.Drawing.Point(92, 120)
        Me.TxtIndicatifZone.Margin = New System.Windows.Forms.Padding(2)
        Me.TxtIndicatifZone.Name = "TxtIndicatifZone"
        Me.TxtIndicatifZone.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIndicatifZone.Properties.Appearance.Options.UseFont = True
        Me.TxtIndicatifZone.Properties.Mask.EditMask = "\d\d\d"
        Me.TxtIndicatifZone.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Regular
        Me.TxtIndicatifZone.Size = New System.Drawing.Size(363, 26)
        Me.TxtIndicatifZone.TabIndex = 4
        '
        'TxtAbrege
        '
        Me.TxtAbrege.Location = New System.Drawing.Point(535, 85)
        Me.TxtAbrege.Margin = New System.Windows.Forms.Padding(2)
        Me.TxtAbrege.Name = "TxtAbrege"
        Me.TxtAbrege.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAbrege.Properties.Appearance.Options.UseFont = True
        Me.TxtAbrege.Properties.MaxLength = 5
        Me.TxtAbrege.Size = New System.Drawing.Size(420, 26)
        Me.TxtAbrege.TabIndex = 3
        '
        'TxtNomZone
        '
        Me.TxtNomZone.Location = New System.Drawing.Point(92, 85)
        Me.TxtNomZone.Margin = New System.Windows.Forms.Padding(2)
        Me.TxtNomZone.Name = "TxtNomZone"
        Me.TxtNomZone.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNomZone.Properties.Appearance.Options.UseFont = True
        Me.TxtNomZone.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtNomZone.Size = New System.Drawing.Size(363, 26)
        Me.TxtNomZone.TabIndex = 2
        '
        'CmbTypZone
        '
        Me.CmbTypZone.Location = New System.Drawing.Point(93, 46)
        Me.CmbTypZone.Margin = New System.Windows.Forms.Padding(2)
        Me.CmbTypZone.Name = "CmbTypZone"
        Me.CmbTypZone.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbTypZone.Properties.Appearance.Options.UseFont = True
        Me.CmbTypZone.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbTypZone.Size = New System.Drawing.Size(177, 26)
        Me.CmbTypZone.TabIndex = 0
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.TreeList1)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(0, 214)
        Me.PanelControl2.Margin = New System.Windows.Forms.Padding(2)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(966, 341)
        Me.PanelControl2.TabIndex = 3
        '
        'TreeList1
        '
        Me.TreeList1.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.Code, Me.Indicatif, Me.NomZone, Me.Devises, Me.Tva, Me.Type, Me.CodeZone, Me.CodeZoneMere, Me.Loaded})
        Me.TreeList1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.TreeList1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeList1.Location = New System.Drawing.Point(2, 2)
        Me.TreeList1.Name = "TreeList1"
        Me.TreeList1.OptionsBehavior.Editable = False
        Me.TreeList1.Size = New System.Drawing.Size(962, 337)
        Me.TreeList1.TabIndex = 43
        '
        'Code
        '
        Me.Code.Caption = "Code"
        Me.Code.FieldName = "Code"
        Me.Code.Name = "Code"
        Me.Code.Visible = True
        Me.Code.VisibleIndex = 0
        Me.Code.Width = 77
        '
        'Indicatif
        '
        Me.Indicatif.Caption = "Indicatif"
        Me.Indicatif.FieldName = "Indicatif"
        Me.Indicatif.Name = "Indicatif"
        Me.Indicatif.Visible = True
        Me.Indicatif.VisibleIndex = 1
        Me.Indicatif.Width = 72
        '
        'NomZone
        '
        Me.NomZone.Caption = "Libellé"
        Me.NomZone.FieldName = "NomZone"
        Me.NomZone.Name = "NomZone"
        Me.NomZone.Visible = True
        Me.NomZone.VisibleIndex = 2
        Me.NomZone.Width = 245
        '
        'Devises
        '
        Me.Devises.Caption = "Devises"
        Me.Devises.FieldName = "Devises"
        Me.Devises.Name = "Devises"
        Me.Devises.Visible = True
        Me.Devises.VisibleIndex = 3
        Me.Devises.Width = 129
        '
        'Tva
        '
        Me.Tva.Caption = "Tva"
        Me.Tva.FieldName = "Tva"
        Me.Tva.Name = "Tva"
        Me.Tva.Visible = True
        Me.Tva.VisibleIndex = 4
        Me.Tva.Width = 128
        '
        'Type
        '
        Me.Type.Caption = "Type"
        Me.Type.FieldName = "Type"
        Me.Type.Name = "Type"
        Me.Type.Visible = True
        Me.Type.VisibleIndex = 5
        Me.Type.Width = 212
        '
        'CodeZone
        '
        Me.CodeZone.Caption = "CodeZone"
        Me.CodeZone.FieldName = "CodeZone"
        Me.CodeZone.Name = "CodeZone"
        Me.CodeZone.Visible = True
        Me.CodeZone.VisibleIndex = 8
        '
        'CodeZoneMere
        '
        Me.CodeZoneMere.Caption = "CodeZoneMere"
        Me.CodeZoneMere.FieldName = "CodeZoneMere"
        Me.CodeZoneMere.Name = "CodeZoneMere"
        Me.CodeZoneMere.Visible = True
        Me.CodeZoneMere.VisibleIndex = 7
        '
        'Loaded
        '
        Me.Loaded.Caption = "Loaded"
        Me.Loaded.FieldName = "Loaded"
        Me.Loaded.Name = "Loaded"
        Me.Loaded.Visible = True
        Me.Loaded.VisibleIndex = 6
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ModifierToolStripMenuItem, Me.SupprimerLaLigneSelectionnerToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(130, 48)
        '
        'ModifierToolStripMenuItem
        '
        Me.ModifierToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Edit_16x16
        Me.ModifierToolStripMenuItem.Name = "ModifierToolStripMenuItem"
        Me.ModifierToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.ModifierToolStripMenuItem.Text = "Modifier"
        '
        'SupprimerLaLigneSelectionnerToolStripMenuItem
        '
        Me.SupprimerLaLigneSelectionnerToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.SupprimerLaLigneSelectionnerToolStripMenuItem.Name = "SupprimerLaLigneSelectionnerToolStripMenuItem"
        Me.SupprimerLaLigneSelectionnerToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.SupprimerLaLigneSelectionnerToolStripMenuItem.Text = "Supprimer"
        '
        'Zonegeo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(966, 555)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IsMdiContainer = True
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Zonegeo"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Zone Géographique "
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.CmbPays_de.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbIssu_de.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbDevise.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtIndicatifZone.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAbrege.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtNomZone.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbTypZone.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.TreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtRetour As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtEnregistrer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CmbIssu_de As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents CmbTypZone As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents CmbDevise As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents TxtTva As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtIndicatifZone As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtAbrege As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtNomZone As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtCodeDeviseCache As System.Windows.Forms.TextBox
    Friend WithEvents TxtCodeZoneMereCache As System.Windows.Forms.TextBox
    Friend WithEvents TxtNiveauStrCache As System.Windows.Forms.TextBox
    Friend WithEvents ActualiserDevise As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SupprimerLaLigneSelectionnerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TxtCodeZone As System.Windows.Forms.TextBox
    Friend WithEvents TreeList1 As DevExpress.XtraTreeList.TreeList
    Friend WithEvents Code As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents Indicatif As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents NomZone As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents Type As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents Devises As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents Tva As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents CodeZone As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents CodeZoneMere As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents Loaded As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents CmbPays_de As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ModifierToolStripMenuItem As ToolStripMenuItem
End Class
