<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BonCommande
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
        Me.BtAnnuler = New DevExpress.XtraEditors.SimpleButton()
        Me.BtEnregistrer = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.Dateboncmde = New DevExpress.XtraEditors.DateEdit()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Cmbctfour = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.CmbService = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.Txtboncmde = New DevExpress.XtraEditors.TextEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtMarche = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TxtPu = New DevExpress.XtraEditors.TextEdit()
        Me.TxtQte = New DevExpress.XtraEditors.TextEdit()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CmbActivite = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TxtMontLettre = New System.Windows.Forms.TextBox()
        Me.TxtNewMont = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtDesignation = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.ListBonCmde = New DevExpress.XtraGrid.GridControl()
        Me.ViewLstCmde = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.Dateboncmde.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Dateboncmde.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Cmbctfour.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbService.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txtboncmde.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.TxtPu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtQte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbActivite.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.ListBonCmde, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewLstCmde, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtAnnuler
        '
        Me.BtAnnuler.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_32
        Me.BtAnnuler.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtAnnuler.Location = New System.Drawing.Point(14, 369)
        Me.BtAnnuler.Name = "BtAnnuler"
        Me.BtAnnuler.Size = New System.Drawing.Size(125, 39)
        Me.BtAnnuler.TabIndex = 11
        Me.BtAnnuler.ToolTip = "Retour"
        '
        'BtEnregistrer
        '
        Me.BtEnregistrer.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_32x32
        Me.BtEnregistrer.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtEnregistrer.Location = New System.Drawing.Point(160, 369)
        Me.BtEnregistrer.Name = "BtEnregistrer"
        Me.BtEnregistrer.Size = New System.Drawing.Size(135, 39)
        Me.BtEnregistrer.TabIndex = 10
        Me.BtEnregistrer.ToolTip = "Enregistrer"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.Dateboncmde)
        Me.GroupControl1.Controls.Add(Me.Label12)
        Me.GroupControl1.Controls.Add(Me.Label11)
        Me.GroupControl1.Controls.Add(Me.Label10)
        Me.GroupControl1.Controls.Add(Me.Cmbctfour)
        Me.GroupControl1.Controls.Add(Me.CmbService)
        Me.GroupControl1.Controls.Add(Me.Txtboncmde)
        Me.GroupControl1.Controls.Add(Me.Label1)
        Me.GroupControl1.Controls.Add(Me.TxtMarche)
        Me.GroupControl1.Location = New System.Drawing.Point(8, 8)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(363, 418)
        Me.GroupControl1.TabIndex = 15
        Me.GroupControl1.Text = "Bon de commande"
        '
        'Dateboncmde
        '
        Me.Dateboncmde.EditValue = Nothing
        Me.Dateboncmde.Location = New System.Drawing.Point(10, 50)
        Me.Dateboncmde.Margin = New System.Windows.Forms.Padding(2)
        Me.Dateboncmde.Name = "Dateboncmde"
        Me.Dateboncmde.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Dateboncmde.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.Dateboncmde.Size = New System.Drawing.Size(348, 20)
        Me.Dateboncmde.TabIndex = 12
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(8, 28)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(30, 13)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "Date"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(8, 351)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(63, 13)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Fournisseur"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 303)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(62, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Demandeur"
        '
        'Cmbctfour
        '
        Me.Cmbctfour.Location = New System.Drawing.Point(7, 374)
        Me.Cmbctfour.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmbctfour.Name = "Cmbctfour"
        Me.Cmbctfour.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Cmbctfour.Size = New System.Drawing.Size(352, 20)
        Me.Cmbctfour.TabIndex = 8
        '
        'CmbService
        '
        Me.CmbService.Location = New System.Drawing.Point(7, 322)
        Me.CmbService.Margin = New System.Windows.Forms.Padding(2)
        Me.CmbService.Name = "CmbService"
        Me.CmbService.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbService.Size = New System.Drawing.Size(352, 20)
        Me.CmbService.TabIndex = 7
        '
        'Txtboncmde
        '
        Me.Txtboncmde.Location = New System.Drawing.Point(130, 89)
        Me.Txtboncmde.Margin = New System.Windows.Forms.Padding(2)
        Me.Txtboncmde.Name = "Txtboncmde"
        Me.Txtboncmde.Size = New System.Drawing.Size(229, 20)
        Me.Txtboncmde.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 91)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "N° Bon de commande"
        '
        'TxtMarche
        '
        Me.TxtMarche.Location = New System.Drawing.Point(7, 114)
        Me.TxtMarche.Multiline = True
        Me.TxtMarche.Name = "TxtMarche"
        Me.TxtMarche.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtMarche.Size = New System.Drawing.Size(353, 186)
        Me.TxtMarche.TabIndex = 5
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.TxtPu)
        Me.Panel1.Controls.Add(Me.TxtQte)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.CmbActivite)
        Me.Panel1.Controls.Add(Me.BtAnnuler)
        Me.Panel1.Controls.Add(Me.BtEnregistrer)
        Me.Panel1.Controls.Add(Me.TxtMontLettre)
        Me.Panel1.Controls.Add(Me.TxtNewMont)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.TxtDesignation)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(377, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(302, 419)
        Me.Panel1.TabIndex = 22
        '
        'TxtPu
        '
        Me.TxtPu.Location = New System.Drawing.Point(151, 130)
        Me.TxtPu.Name = "TxtPu"
        Me.TxtPu.Properties.Mask.EditMask = "d"
        Me.TxtPu.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TxtPu.Properties.MaxLength = 12
        Me.TxtPu.Size = New System.Drawing.Size(134, 20)
        Me.TxtPu.TabIndex = 24
        '
        'TxtQte
        '
        Me.TxtQte.Location = New System.Drawing.Point(8, 130)
        Me.TxtQte.Name = "TxtQte"
        Me.TxtQte.Properties.Mask.EditMask = "d"
        Me.TxtQte.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TxtQte.Properties.MaxLength = 12
        Me.TxtQte.Size = New System.Drawing.Size(134, 20)
        Me.TxtQte.TabIndex = 23
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Times New Roman", 8.142858!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 14)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Activité"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Italic)
        Me.Label9.Location = New System.Drawing.Point(10, 229)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(90, 14)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "Montant en lettres"
        '
        'CmbActivite
        '
        Me.CmbActivite.Location = New System.Drawing.Point(10, 24)
        Me.CmbActivite.Margin = New System.Windows.Forms.Padding(2)
        Me.CmbActivite.Name = "CmbActivite"
        Me.CmbActivite.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbActivite.Size = New System.Drawing.Size(283, 20)
        Me.CmbActivite.TabIndex = 13
        '
        'TxtMontLettre
        '
        Me.TxtMontLettre.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.TxtMontLettre.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMontLettre.Location = New System.Drawing.Point(12, 245)
        Me.TxtMontLettre.Multiline = True
        Me.TxtMontLettre.Name = "TxtMontLettre"
        Me.TxtMontLettre.ReadOnly = True
        Me.TxtMontLettre.Size = New System.Drawing.Size(284, 117)
        Me.TxtMontLettre.TabIndex = 10
        '
        'TxtNewMont
        '
        Me.TxtNewMont.Font = New System.Drawing.Font("Tahoma", 8.142858!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNewMont.Location = New System.Drawing.Point(10, 184)
        Me.TxtNewMont.Name = "TxtNewMont"
        Me.TxtNewMont.ReadOnly = True
        Me.TxtNewMont.Size = New System.Drawing.Size(286, 21)
        Me.TxtNewMont.TabIndex = 9
        Me.TxtNewMont.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(96, 166)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(123, 15)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "MONTANT A PAYER"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Italic)
        Me.Label5.Location = New System.Drawing.Point(12, 109)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 14)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Quantité"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Italic)
        Me.Label4.Location = New System.Drawing.Point(153, 109)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 14)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Prix unitaire"
        '
        'TxtDesignation
        '
        Me.TxtDesignation.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.TxtDesignation.Location = New System.Drawing.Point(12, 73)
        Me.TxtDesignation.Name = "TxtDesignation"
        Me.TxtDesignation.Size = New System.Drawing.Size(285, 21)
        Me.TxtDesignation.TabIndex = 1
        Me.TxtDesignation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Italic)
        Me.Label2.Location = New System.Drawing.Point(12, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 14)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Désignation"
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.ListBonCmde)
        Me.GroupControl3.Location = New System.Drawing.Point(683, 8)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(307, 418)
        Me.GroupControl3.TabIndex = 23
        Me.GroupControl3.Text = "Historique des besoins"
        '
        'ListBonCmde
        '
        Me.ListBonCmde.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBonCmde.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBonCmde.Location = New System.Drawing.Point(2, 21)
        Me.ListBonCmde.MainView = Me.ViewLstCmde
        Me.ListBonCmde.Name = "ListBonCmde"
        Me.ListBonCmde.Size = New System.Drawing.Size(303, 395)
        Me.ListBonCmde.TabIndex = 43
        Me.ListBonCmde.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewLstCmde})
        '
        'ViewLstCmde
        '
        Me.ViewLstCmde.ActiveFilterEnabled = False
        Me.ViewLstCmde.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewLstCmde.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewLstCmde.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
        Me.ViewLstCmde.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.ViewLstCmde.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.ViewLstCmde.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.ViewLstCmde.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.ViewLstCmde.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
        Me.ViewLstCmde.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.ViewLstCmde.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.ViewLstCmde.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewLstCmde.Appearance.Empty.BackColor2 = System.Drawing.Color.White
        Me.ViewLstCmde.Appearance.Empty.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ViewLstCmde.Appearance.EvenRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ViewLstCmde.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.ViewLstCmde.Appearance.EvenRow.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.EvenRow.Options.UseBorderColor = True
        Me.ViewLstCmde.Appearance.EvenRow.Options.UseForeColor = True
        Me.ViewLstCmde.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewLstCmde.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewLstCmde.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.ViewLstCmde.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.ViewLstCmde.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.ViewLstCmde.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewLstCmde.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewLstCmde.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewLstCmde.Appearance.FilterPanel.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.FilterPanel.Options.UseForeColor = True
        Me.ViewLstCmde.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.ViewLstCmde.Appearance.FixedLine.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.ViewLstCmde.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.ViewLstCmde.Appearance.FocusedCell.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.FocusedCell.Options.UseForeColor = True
        Me.ViewLstCmde.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(177, Byte), Integer))
        Me.ViewLstCmde.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.ViewLstCmde.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewLstCmde.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewLstCmde.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewLstCmde.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewLstCmde.Appearance.FooterPanel.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.ViewLstCmde.Appearance.FooterPanel.Options.UseForeColor = True
        Me.ViewLstCmde.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.ViewLstCmde.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.ViewLstCmde.Appearance.GroupButton.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.GroupButton.Options.UseBorderColor = True
        Me.ViewLstCmde.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.ViewLstCmde.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.ViewLstCmde.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.ViewLstCmde.Appearance.GroupFooter.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.ViewLstCmde.Appearance.GroupFooter.Options.UseForeColor = True
        Me.ViewLstCmde.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewLstCmde.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewLstCmde.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewLstCmde.Appearance.GroupPanel.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.GroupPanel.Options.UseForeColor = True
        Me.ViewLstCmde.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.ViewLstCmde.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.ViewLstCmde.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
        Me.ViewLstCmde.Appearance.GroupRow.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.GroupRow.Options.UseBorderColor = True
        Me.ViewLstCmde.Appearance.GroupRow.Options.UseForeColor = True
        Me.ViewLstCmde.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.ViewLstCmde.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.ViewLstCmde.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewLstCmde.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.ViewLstCmde.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.ViewLstCmde.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(186, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.ViewLstCmde.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(134, Byte), Integer))
        Me.ViewLstCmde.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.ViewLstCmde.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.ViewLstCmde.Appearance.HorzLine.BorderColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.ViewLstCmde.Appearance.HorzLine.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.HorzLine.Options.UseBorderColor = True
        Me.ViewLstCmde.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewLstCmde.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewLstCmde.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.ViewLstCmde.Appearance.OddRow.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.OddRow.Options.UseBorderColor = True
        Me.ViewLstCmde.Appearance.OddRow.Options.UseForeColor = True
        Me.ViewLstCmde.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.ViewLstCmde.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
        Me.ViewLstCmde.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(134, Byte), Integer))
        Me.ViewLstCmde.Appearance.Preview.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.Preview.Options.UseFont = True
        Me.ViewLstCmde.Appearance.Preview.Options.UseForeColor = True
        Me.ViewLstCmde.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewLstCmde.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.ViewLstCmde.Appearance.Row.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.Row.Options.UseForeColor = True
        Me.ViewLstCmde.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewLstCmde.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
        Me.ViewLstCmde.Appearance.RowSeparator.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ViewLstCmde.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black
        Me.ViewLstCmde.Appearance.SelectedRow.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.SelectedRow.Options.UseForeColor = True
        Me.ViewLstCmde.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
        Me.ViewLstCmde.Appearance.TopNewRow.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.ViewLstCmde.Appearance.VertLine.BorderColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.ViewLstCmde.Appearance.VertLine.Options.UseBackColor = True
        Me.ViewLstCmde.Appearance.VertLine.Options.UseBorderColor = True
        Me.ViewLstCmde.GridControl = Me.ListBonCmde
        Me.ViewLstCmde.Name = "ViewLstCmde"
        Me.ViewLstCmde.OptionsBehavior.Editable = False
        Me.ViewLstCmde.OptionsBehavior.ReadOnly = True
        Me.ViewLstCmde.OptionsCustomization.AllowColumnMoving = False
        Me.ViewLstCmde.OptionsCustomization.AllowFilter = False
        Me.ViewLstCmde.OptionsCustomization.AllowGroup = False
        Me.ViewLstCmde.OptionsCustomization.AllowSort = False
        Me.ViewLstCmde.OptionsFilter.AllowFilterEditor = False
        Me.ViewLstCmde.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewLstCmde.OptionsPrint.AutoWidth = False
        Me.ViewLstCmde.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewLstCmde.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewLstCmde.OptionsView.ColumnAutoWidth = False
        Me.ViewLstCmde.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewLstCmde.OptionsView.EnableAppearanceOddRow = True
        Me.ViewLstCmde.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewLstCmde.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewLstCmde.OptionsView.ShowGroupPanel = False
        Me.ViewLstCmde.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewLstCmde.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'BonCommande
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(993, 430)
        Me.Controls.Add(Me.GroupControl3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "BonCommande"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bon de commande"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.Dateboncmde.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Dateboncmde.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Cmbctfour.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbService.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txtboncmde.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.TxtPu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtQte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbActivite.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.ListBonCmde, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewLstCmde, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtAnnuler As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtEnregistrer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TxtMarche As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TxtMontLettre As System.Windows.Forms.TextBox
    Friend WithEvents TxtNewMont As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtDesignation As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Dateboncmde As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Cmbctfour As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents CmbService As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents Txtboncmde As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CmbActivite As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ListBonCmde As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewLstCmde As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TxtQte As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPu As DevExpress.XtraEditors.TextEdit
End Class
