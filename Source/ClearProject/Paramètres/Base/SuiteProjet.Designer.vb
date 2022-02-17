<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SuiteProjet
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
        Me.txtEmp_ID = New DevExpress.XtraEditors.TextEdit()
        Me.BtEnregistrer = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.txtActivites = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl19 = New DevExpress.XtraEditors.LabelControl()
        Me.txtNumEmployeur = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl18 = New DevExpress.XtraEditors.LabelControl()
        Me.txtCodeEtablissement = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.txtCodeActivite = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.txtCapital = New DevExpress.XtraEditors.TextEdit()
        Me.cmbFormeJuridique = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.txtdateRegistreCommerce = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.txtCentreImpot = New DevExpress.XtraEditors.TextEdit()
        Me.txtRegistreCommerce = New DevExpress.XtraEditors.TextEdit()
        Me.txtCompteContribuable = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbNomCodornateur = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.txtEmp_ID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtActivites.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNumEmployeur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodeEtablissement.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodeActivite.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCapital.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbFormeJuridique.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdateRegistreCommerce.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdateRegistreCommerce.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCentreImpot.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRegistreCommerce.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCompteContribuable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.cmbNomCodornateur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.txtEmp_ID)
        Me.PanelControl1.Controls.Add(Me.BtEnregistrer)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 314)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(684, 40)
        Me.PanelControl1.TabIndex = 8
        '
        'txtEmp_ID
        '
        Me.txtEmp_ID.Location = New System.Drawing.Point(5, 6)
        Me.txtEmp_ID.Name = "txtEmp_ID"
        Me.txtEmp_ID.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmp_ID.Properties.Appearance.Options.UseFont = True
        Me.txtEmp_ID.Properties.Appearance.Options.UseTextOptions = True
        Me.txtEmp_ID.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtEmp_ID.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtEmp_ID.Properties.Mask.EditMask = "[A-Z][A-Z]-[A-Z][A-Z][A-Z]-[0-9][0-9][0-9][0-9]-[A-Z]-[0-9][0-9][0-9][0-9]"
        Me.txtEmp_ID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.txtEmp_ID.Properties.MaxLength = 20
        Me.txtEmp_ID.Size = New System.Drawing.Size(46, 22)
        Me.txtEmp_ID.TabIndex = 7
        Me.txtEmp_ID.Visible = False
        '
        'BtEnregistrer
        '
        Me.BtEnregistrer.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnregistrer.Appearance.Options.UseFont = True
        Me.BtEnregistrer.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.BtEnregistrer.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft
        Me.BtEnregistrer.Location = New System.Drawing.Point(215, 2)
        Me.BtEnregistrer.Name = "BtEnregistrer"
        Me.BtEnregistrer.Size = New System.Drawing.Size(232, 36)
        Me.BtEnregistrer.TabIndex = 6
        Me.BtEnregistrer.Text = "ENREGISTREMENT"
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.txtActivites)
        Me.GroupControl1.Controls.Add(Me.LabelControl19)
        Me.GroupControl1.Controls.Add(Me.txtNumEmployeur)
        Me.GroupControl1.Controls.Add(Me.LabelControl5)
        Me.GroupControl1.Controls.Add(Me.LabelControl18)
        Me.GroupControl1.Controls.Add(Me.txtCodeEtablissement)
        Me.GroupControl1.Controls.Add(Me.LabelControl10)
        Me.GroupControl1.Controls.Add(Me.txtCodeActivite)
        Me.GroupControl1.Controls.Add(Me.LabelControl9)
        Me.GroupControl1.Controls.Add(Me.txtCapital)
        Me.GroupControl1.Controls.Add(Me.cmbFormeJuridique)
        Me.GroupControl1.Controls.Add(Me.LabelControl8)
        Me.GroupControl1.Controls.Add(Me.txtdateRegistreCommerce)
        Me.GroupControl1.Controls.Add(Me.LabelControl4)
        Me.GroupControl1.Controls.Add(Me.LabelControl3)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.txtCentreImpot)
        Me.GroupControl1.Controls.Add(Me.txtRegistreCommerce)
        Me.GroupControl1.Controls.Add(Me.txtCompteContribuable)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(684, 237)
        Me.GroupControl1.TabIndex = 7
        Me.GroupControl1.Text = "Informations du Projet"
        '
        'txtActivites
        '
        Me.txtActivites.Location = New System.Drawing.Point(8, 180)
        Me.txtActivites.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.txtActivites.Name = "txtActivites"
        Me.txtActivites.Size = New System.Drawing.Size(658, 50)
        Me.txtActivites.TabIndex = 33
        '
        'LabelControl19
        '
        Me.LabelControl19.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl19.Location = New System.Drawing.Point(167, 114)
        Me.LabelControl19.Name = "LabelControl19"
        Me.LabelControl19.Size = New System.Drawing.Size(104, 15)
        Me.LabelControl19.TabIndex = 32
        Me.LabelControl19.Text = "Numéro employeur"
        '
        'txtNumEmployeur
        '
        Me.txtNumEmployeur.Location = New System.Drawing.Point(167, 130)
        Me.txtNumEmployeur.Name = "txtNumEmployeur"
        Me.txtNumEmployeur.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumEmployeur.Properties.Appearance.Options.UseFont = True
        Me.txtNumEmployeur.Properties.Mask.EditMask = "[0-9][0-9][0-9][0-9][0-9][0-9]"
        Me.txtNumEmployeur.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.txtNumEmployeur.Size = New System.Drawing.Size(194, 22)
        Me.txtNumEmployeur.TabIndex = 17
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Location = New System.Drawing.Point(8, 160)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(132, 15)
        Me.LabelControl5.TabIndex = 30
        Me.LabelControl5.Text = "Activité(s) principale(s)"
        '
        'LabelControl18
        '
        Me.LabelControl18.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl18.Location = New System.Drawing.Point(535, 114)
        Me.LabelControl18.Name = "LabelControl18"
        Me.LabelControl18.Size = New System.Drawing.Size(107, 15)
        Me.LabelControl18.TabIndex = 30
        Me.LabelControl18.Text = "Code etablissement"
        '
        'txtCodeEtablissement
        '
        Me.txtCodeEtablissement.Location = New System.Drawing.Point(535, 130)
        Me.txtCodeEtablissement.Name = "txtCodeEtablissement"
        Me.txtCodeEtablissement.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodeEtablissement.Properties.Appearance.Options.UseFont = True
        Me.txtCodeEtablissement.Size = New System.Drawing.Size(135, 22)
        Me.txtCodeEtablissement.TabIndex = 29
        '
        'LabelControl10
        '
        Me.LabelControl10.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl10.Location = New System.Drawing.Point(367, 114)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(74, 15)
        Me.LabelControl10.TabIndex = 19
        Me.LabelControl10.Text = "Code Activité"
        '
        'txtCodeActivite
        '
        Me.txtCodeActivite.EditValue = ""
        Me.txtCodeActivite.Location = New System.Drawing.Point(367, 130)
        Me.txtCodeActivite.Name = "txtCodeActivite"
        Me.txtCodeActivite.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodeActivite.Properties.Appearance.Options.UseFont = True
        Me.txtCodeActivite.Properties.Mask.BeepOnError = True
        Me.txtCodeActivite.Properties.Mask.EditMask = "[0-9][0-9]"
        Me.txtCodeActivite.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.txtCodeActivite.Properties.Mask.ShowPlaceHolders = False
        Me.txtCodeActivite.Size = New System.Drawing.Size(161, 22)
        Me.txtCodeActivite.TabIndex = 18
        '
        'LabelControl9
        '
        Me.LabelControl9.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl9.Location = New System.Drawing.Point(8, 114)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(43, 15)
        Me.LabelControl9.TabIndex = 17
        Me.LabelControl9.Text = "Capital"
        '
        'txtCapital
        '
        Me.txtCapital.Location = New System.Drawing.Point(8, 130)
        Me.txtCapital.Name = "txtCapital"
        Me.txtCapital.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCapital.Properties.Appearance.Options.UseFont = True
        Me.txtCapital.Properties.Mask.EditMask = "n0"
        Me.txtCapital.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtCapital.Properties.MaxLength = 50
        Me.txtCapital.Size = New System.Drawing.Size(146, 22)
        Me.txtCapital.TabIndex = 16
        '
        'cmbFormeJuridique
        '
        Me.cmbFormeJuridique.Location = New System.Drawing.Point(167, 86)
        Me.cmbFormeJuridique.Name = "cmbFormeJuridique"
        Me.cmbFormeJuridique.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFormeJuridique.Properties.Appearance.Options.UseFont = True
        Me.cmbFormeJuridique.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbFormeJuridique.Properties.Items.AddRange(New Object() {"SA", "SARL", "SARL Uni-Personnel", "PME"})
        Me.cmbFormeJuridique.Size = New System.Drawing.Size(502, 22)
        Me.cmbFormeJuridique.TabIndex = 15
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl8.Location = New System.Drawing.Point(167, 68)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(91, 15)
        Me.LabelControl8.TabIndex = 14
        Me.LabelControl8.Text = "Forme Juridique"
        '
        'txtdateRegistreCommerce
        '
        Me.txtdateRegistreCommerce.EditValue = Nothing
        Me.txtdateRegistreCommerce.Location = New System.Drawing.Point(7, 86)
        Me.txtdateRegistreCommerce.Name = "txtdateRegistreCommerce"
        Me.txtdateRegistreCommerce.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdateRegistreCommerce.Properties.Appearance.Options.UseFont = True
        Me.txtdateRegistreCommerce.Properties.Appearance.Options.UseTextOptions = True
        Me.txtdateRegistreCommerce.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtdateRegistreCommerce.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtdateRegistreCommerce.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtdateRegistreCommerce.Size = New System.Drawing.Size(148, 22)
        Me.txtdateRegistreCommerce.TabIndex = 7
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Location = New System.Drawing.Point(9, 72)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(47, 15)
        Me.LabelControl4.TabIndex = 6
        Me.LabelControl4.Text = "Date RC"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(358, 27)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(97, 15)
        Me.LabelControl3.TabIndex = 5
        Me.LabelControl3.Text = "Centre des impôts"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(175, 27)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(121, 15)
        Me.LabelControl2.TabIndex = 4
        Me.LabelControl2.Text = "Registre de Commerce"
        '
        'txtCentreImpot
        '
        Me.txtCentreImpot.Location = New System.Drawing.Point(358, 43)
        Me.txtCentreImpot.Name = "txtCentreImpot"
        Me.txtCentreImpot.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCentreImpot.Properties.Appearance.Options.UseFont = True
        Me.txtCentreImpot.Properties.MaxLength = 500
        Me.txtCentreImpot.Size = New System.Drawing.Size(310, 22)
        Me.txtCentreImpot.TabIndex = 3
        '
        'txtRegistreCommerce
        '
        Me.txtRegistreCommerce.Location = New System.Drawing.Point(166, 43)
        Me.txtRegistreCommerce.Name = "txtRegistreCommerce"
        Me.txtRegistreCommerce.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegistreCommerce.Properties.Appearance.Options.UseFont = True
        Me.txtRegistreCommerce.Properties.Appearance.Options.UseTextOptions = True
        Me.txtRegistreCommerce.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtRegistreCommerce.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRegistreCommerce.Properties.Mask.EditMask = "[A-Z][A-Z]-[A-Z][A-Z][A-Z]-[0-9][0-9][0-9][0-9]-[A-Z]-[0-9][0-9][0-9][0-9]"
        Me.txtRegistreCommerce.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.txtRegistreCommerce.Properties.MaxLength = 20
        Me.txtRegistreCommerce.Size = New System.Drawing.Size(182, 22)
        Me.txtRegistreCommerce.TabIndex = 2
        '
        'txtCompteContribuable
        '
        Me.txtCompteContribuable.Location = New System.Drawing.Point(7, 43)
        Me.txtCompteContribuable.Name = "txtCompteContribuable"
        Me.txtCompteContribuable.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompteContribuable.Properties.Appearance.Options.UseFont = True
        Me.txtCompteContribuable.Properties.Appearance.Options.UseTextOptions = True
        Me.txtCompteContribuable.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtCompteContribuable.Properties.Mask.BeepOnError = True
        Me.txtCompteContribuable.Properties.Mask.EditMask = "[0-9][0-9][0-9][0-9][0-9][0-9][0-9][A-Z]"
        Me.txtCompteContribuable.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.txtCompteContribuable.Properties.Mask.ShowPlaceHolders = False
        Me.txtCompteContribuable.Properties.MaxLength = 20
        Me.txtCompteContribuable.Size = New System.Drawing.Size(148, 22)
        Me.txtCompteContribuable.TabIndex = 1
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(9, 27)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(119, 15)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Compte Contribuable"
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.LabelControl6)
        Me.GroupControl2.Controls.Add(Me.cmbNomCodornateur)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 237)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(684, 77)
        Me.GroupControl2.TabIndex = 9
        Me.GroupControl2.Text = "Directeur ou Cordonnateur du Projet"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6.Location = New System.Drawing.Point(7, 40)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(87, 15)
        Me.LabelControl6.TabIndex = 33
        Me.LabelControl6.Text = "Nom et prénoms"
        '
        'cmbNomCodornateur
        '
        Me.cmbNomCodornateur.Location = New System.Drawing.Point(120, 37)
        Me.cmbNomCodornateur.Name = "cmbNomCodornateur"
        Me.cmbNomCodornateur.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNomCodornateur.Properties.Appearance.Options.UseFont = True
        Me.cmbNomCodornateur.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbNomCodornateur.Size = New System.Drawing.Size(546, 22)
        Me.cmbNomCodornateur.TabIndex = 34
        '
        'SuiteProjet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 354)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SuiteProjet"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Informations Supplémentaires"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.txtEmp_ID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtActivites.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNumEmployeur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodeEtablissement.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodeActivite.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCapital.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbFormeJuridique.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdateRegistreCommerce.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdateRegistreCommerce.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCentreImpot.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRegistreCommerce.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCompteContribuable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.cmbNomCodornateur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtEnregistrer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl19 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNumEmployeur As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl18 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCodeEtablissement As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCodeActivite As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCapital As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbFormeJuridique As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtdateRegistreCommerce As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCentreImpot As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtRegistreCommerce As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCompteContribuable As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtActivites As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbNomCodornateur As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtEmp_ID As DevExpress.XtraEditors.TextEdit
End Class
