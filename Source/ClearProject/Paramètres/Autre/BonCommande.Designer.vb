<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BonCommande
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
        Me.BtAnnuler = New DevExpress.XtraEditors.SimpleButton()
        Me.BtEnregistrer = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.TxtPu = New DevExpress.XtraEditors.TextEdit()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CmbNumMarche = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TxtQte = New DevExpress.XtraEditors.TextEdit()
        Me.TxtNewMont = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtMontLettre = New System.Windows.Forms.TextBox()
        Me.Dateboncmde = New DevExpress.XtraEditors.DateEdit()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Cmbctfour = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.Txtboncmde = New DevExpress.XtraEditors.TextEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.ListBonCmde = New DevExpress.XtraGrid.GridControl()
        Me.ViewLstCmde = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Checkbonc = New DevExpress.XtraEditors.CheckEdit()
        Me.Checkmarche = New DevExpress.XtraEditors.CheckEdit()
        Me.TxtBonC = New DevExpress.XtraEditors.TextEdit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.TxtPu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbNumMarche.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtQte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Dateboncmde.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Dateboncmde.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Cmbctfour.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txtboncmde.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.ListBonCmde, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewLstCmde, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Checkbonc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Checkmarche.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBonC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtAnnuler
        '
        Me.BtAnnuler.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_32
        Me.BtAnnuler.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtAnnuler.Location = New System.Drawing.Point(61, 453)
        Me.BtAnnuler.Name = "BtAnnuler"
        Me.BtAnnuler.Size = New System.Drawing.Size(125, 39)
        Me.BtAnnuler.TabIndex = 11
        Me.BtAnnuler.ToolTip = "Retour"
        '
        'BtEnregistrer
        '
        Me.BtEnregistrer.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_32x32
        Me.BtEnregistrer.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtEnregistrer.Location = New System.Drawing.Point(207, 453)
        Me.BtEnregistrer.Name = "BtEnregistrer"
        Me.BtEnregistrer.Size = New System.Drawing.Size(135, 39)
        Me.BtEnregistrer.TabIndex = 10
        Me.BtEnregistrer.ToolTip = "Enregistrer"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.TxtBonC)
        Me.GroupControl1.Controls.Add(Me.Checkmarche)
        Me.GroupControl1.Controls.Add(Me.Checkbonc)
        Me.GroupControl1.Controls.Add(Me.Label2)
        Me.GroupControl1.Controls.Add(Me.BtAnnuler)
        Me.GroupControl1.Controls.Add(Me.BtEnregistrer)
        Me.GroupControl1.Controls.Add(Me.TxtPu)
        Me.GroupControl1.Controls.Add(Me.Label9)
        Me.GroupControl1.Controls.Add(Me.CmbNumMarche)
        Me.GroupControl1.Controls.Add(Me.TxtQte)
        Me.GroupControl1.Controls.Add(Me.TxtNewMont)
        Me.GroupControl1.Controls.Add(Me.Label6)
        Me.GroupControl1.Controls.Add(Me.TxtMontLettre)
        Me.GroupControl1.Controls.Add(Me.Dateboncmde)
        Me.GroupControl1.Controls.Add(Me.Label12)
        Me.GroupControl1.Controls.Add(Me.Label11)
        Me.GroupControl1.Controls.Add(Me.Cmbctfour)
        Me.GroupControl1.Controls.Add(Me.Txtboncmde)
        Me.GroupControl1.Controls.Add(Me.Label1)
        Me.GroupControl1.Controls.Add(Me.Label5)
        Me.GroupControl1.Controls.Add(Me.Label4)
        Me.GroupControl1.Location = New System.Drawing.Point(8, 8)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(685, 516)
        Me.GroupControl1.TabIndex = 15
        Me.GroupControl1.Text = "Bon de commande"
        '
        'TxtPu
        '
        Me.TxtPu.Location = New System.Drawing.Point(153, 248)
        Me.TxtPu.Name = "TxtPu"
        Me.TxtPu.Properties.Mask.EditMask = "d"
        Me.TxtPu.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TxtPu.Properties.MaxLength = 12
        Me.TxtPu.Size = New System.Drawing.Size(387, 20)
        Me.TxtPu.TabIndex = 24
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Italic)
        Me.Label9.Location = New System.Drawing.Point(7, 330)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(90, 14)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "Montant en lettres"
        '
        'CmbNumMarche
        '
        Me.CmbNumMarche.Location = New System.Drawing.Point(152, 67)
        Me.CmbNumMarche.Margin = New System.Windows.Forms.Padding(2)
        Me.CmbNumMarche.Name = "CmbNumMarche"
        Me.CmbNumMarche.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbNumMarche.Size = New System.Drawing.Size(387, 20)
        Me.CmbNumMarche.TabIndex = 15
        '
        'TxtQte
        '
        Me.TxtQte.Location = New System.Drawing.Point(151, 217)
        Me.TxtQte.Name = "TxtQte"
        Me.TxtQte.Properties.Mask.EditMask = "d"
        Me.TxtQte.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TxtQte.Properties.MaxLength = 12
        Me.TxtQte.Size = New System.Drawing.Size(389, 20)
        Me.TxtQte.TabIndex = 23
        '
        'TxtNewMont
        '
        Me.TxtNewMont.Font = New System.Drawing.Font("Tahoma", 8.142858!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNewMont.Location = New System.Drawing.Point(151, 282)
        Me.TxtNewMont.Name = "TxtNewMont"
        Me.TxtNewMont.ReadOnly = True
        Me.TxtNewMont.Size = New System.Drawing.Size(388, 21)
        Me.TxtNewMont.TabIndex = 9
        Me.TxtNewMont.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(7, 285)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(123, 15)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "MONTANT A PAYER"
        '
        'TxtMontLettre
        '
        Me.TxtMontLettre.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.TxtMontLettre.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMontLettre.Location = New System.Drawing.Point(151, 326)
        Me.TxtMontLettre.Multiline = True
        Me.TxtMontLettre.Name = "TxtMontLettre"
        Me.TxtMontLettre.ReadOnly = True
        Me.TxtMontLettre.Size = New System.Drawing.Size(388, 23)
        Me.TxtMontLettre.TabIndex = 10
        '
        'Dateboncmde
        '
        Me.Dateboncmde.EditValue = Nothing
        Me.Dateboncmde.Location = New System.Drawing.Point(152, 120)
        Me.Dateboncmde.Margin = New System.Windows.Forms.Padding(2)
        Me.Dateboncmde.Name = "Dateboncmde"
        Me.Dateboncmde.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Dateboncmde.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.Dateboncmde.Size = New System.Drawing.Size(387, 20)
        Me.Dateboncmde.TabIndex = 12
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(9, 123)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(30, 13)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "Date"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(9, 182)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(62, 13)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Attributaire"
        '
        'Cmbctfour
        '
        Me.Cmbctfour.Location = New System.Drawing.Point(153, 179)
        Me.Cmbctfour.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmbctfour.Name = "Cmbctfour"
        Me.Cmbctfour.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Cmbctfour.Size = New System.Drawing.Size(387, 20)
        Me.Cmbctfour.TabIndex = 8
        '
        'Txtboncmde
        '
        Me.Txtboncmde.Location = New System.Drawing.Point(152, 149)
        Me.Txtboncmde.Margin = New System.Windows.Forms.Padding(2)
        Me.Txtboncmde.Name = "Txtboncmde"
        Me.Txtboncmde.Size = New System.Drawing.Size(388, 20)
        Me.Txtboncmde.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 151)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "N° Bon de commande"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Italic)
        Me.Label5.Location = New System.Drawing.Point(9, 223)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 14)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Quantité"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Italic)
        Me.Label4.Location = New System.Drawing.Point(9, 254)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 14)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Prix unitaire"
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.ListBonCmde)
        Me.GroupControl3.Location = New System.Drawing.Point(699, 8)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(368, 418)
        Me.GroupControl3.TabIndex = 23
        Me.GroupControl3.Text = "Borderau de prix"
        '
        'ListBonCmde
        '
        Me.ListBonCmde.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBonCmde.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBonCmde.Location = New System.Drawing.Point(2, 21)
        Me.ListBonCmde.MainView = Me.ViewLstCmde
        Me.ListBonCmde.Name = "ListBonCmde"
        Me.ListBonCmde.Size = New System.Drawing.Size(364, 395)
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 13)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Numéro du Marché"
        '
        'Checkbonc
        '
        Me.Checkbonc.EditValue = True
        Me.Checkbonc.Location = New System.Drawing.Point(61, 33)
        Me.Checkbonc.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.Checkbonc.Name = "Checkbonc"
        Me.Checkbonc.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.125!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Checkbonc.Properties.Appearance.Options.UseFont = True
        Me.Checkbonc.Properties.Caption = "Enregistrement Marché / Bon de commande"
        Me.Checkbonc.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.Checkbonc.Size = New System.Drawing.Size(281, 21)
        Me.Checkbonc.TabIndex = 24
        '
        'Checkmarche
        '
        Me.Checkmarche.Location = New System.Drawing.Point(358, 33)
        Me.Checkmarche.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.Checkmarche.Name = "Checkmarche"
        Me.Checkmarche.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.125!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Checkmarche.Properties.Appearance.Options.UseFont = True
        Me.Checkmarche.Properties.Caption = "Enregistrement Marché Généré"
        Me.Checkmarche.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.Checkmarche.Size = New System.Drawing.Size(252, 21)
        Me.Checkmarche.TabIndex = 26
        '
        'TxtBonC
        '
        Me.TxtBonC.Location = New System.Drawing.Point(151, 92)
        Me.TxtBonC.Margin = New System.Windows.Forms.Padding(2)
        Me.TxtBonC.Name = "TxtBonC"
        Me.TxtBonC.Size = New System.Drawing.Size(388, 20)
        Me.TxtBonC.TabIndex = 27
        '
        'BonCommande
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1106, 536)
        Me.Controls.Add(Me.GroupControl3)
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
        CType(Me.TxtPu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbNumMarche.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtQte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Dateboncmde.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Dateboncmde.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Cmbctfour.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txtboncmde.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.ListBonCmde, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewLstCmde, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Checkbonc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Checkmarche.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBonC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtAnnuler As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtEnregistrer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TxtMontLettre As System.Windows.Forms.TextBox
    Friend WithEvents TxtNewMont As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Dateboncmde As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Cmbctfour As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents Txtboncmde As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ListBonCmde As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewLstCmde As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TxtQte As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPu As DevExpress.XtraEditors.TextEdit
    Friend WithEvents CmbNumMarche As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents Checkbonc As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Checkmarche As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents TxtBonC As DevExpress.XtraEditors.TextEdit
End Class
