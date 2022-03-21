<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PMPochette
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PMPochette))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.treeLPochette = New DevExpress.XtraTreeList.TreeList()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.OuvrirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TéléchargerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SupprimerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.navBarGroup1 = New DevExpress.XtraNavBar.NavBarGroup()
        Me.dockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.barAndDockingController1 = New DevExpress.XtraBars.BarAndDockingController(Me.components)
        Me.barManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.bar1 = New DevExpress.XtraBars.Bar()
        Me.BtOpen = New DevExpress.XtraBars.BarLargeButtonItem()
        Me.BtDownload = New DevExpress.XtraBars.BarLargeButtonItem()
        Me.BtDelete = New DevExpress.XtraBars.BarLargeButtonItem()
        Me.barDockControl1 = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControl2 = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControl3 = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControl4 = New DevExpress.XtraBars.BarDockControl()
        Me.siFile = New DevExpress.XtraBars.BarSubItem()
        Me.iOpen = New DevExpress.XtraBars.BarButtonItem()
        Me.iSave = New DevExpress.XtraBars.BarButtonItem()
        Me.iExit = New DevExpress.XtraBars.BarButtonItem()
        Me.siView = New DevExpress.XtraBars.BarSubItem()
        Me.iToolBars = New DevExpress.XtraBars.BarToolbarsListItem()
        Me.siFavorites = New DevExpress.XtraBars.BarSubItem()
        Me.iAdd = New DevExpress.XtraBars.BarButtonItem()
        Me.siHelp = New DevExpress.XtraBars.BarSubItem()
        Me.iAbout = New DevExpress.XtraBars.BarButtonItem()
        Me.iMedia = New DevExpress.XtraBars.BarLargeButtonItem()
        Me.iGo = New DevExpress.XtraBars.BarButtonItem()
        Me.eAddress = New DevExpress.XtraBars.BarEditItem()
        Me.repositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.eProgress = New DevExpress.XtraBars.BarEditItem()
        Me.repositoryItemProgressBar1 = New DevExpress.XtraEditors.Repository.RepositoryItemProgressBar()
        Me.ipsWXP = New DevExpress.XtraBars.BarButtonItem()
        Me.ipsOXP = New DevExpress.XtraBars.BarButtonItem()
        Me.ipsO2K = New DevExpress.XtraBars.BarButtonItem()
        Me.iPaintStyle = New DevExpress.XtraBars.BarSubItem()
        Me.ipsDefault = New DevExpress.XtraBars.BarButtonItem()
        Me.ipsO3 = New DevExpress.XtraBars.BarButtonItem()
        Me.CombBail = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.treeLPochette, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.dockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barAndDockingController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CombBail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'treeLPochette
        '
        Me.treeLPochette.Appearance.FocusedRow.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.treeLPochette.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.treeLPochette.Appearance.FocusedRow.Options.UseBackColor = True
        Me.treeLPochette.Appearance.FocusedRow.Options.UseForeColor = True
        Me.treeLPochette.Dock = System.Windows.Forms.DockStyle.Fill
        Me.treeLPochette.Location = New System.Drawing.Point(0, 31)
        Me.treeLPochette.Name = "treeLPochette"
        Me.treeLPochette.OptionsBehavior.Editable = False
        Me.treeLPochette.OptionsView.ShowColumns = False
        Me.treeLPochette.Size = New System.Drawing.Size(993, 392)
        Me.treeLPochette.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OuvrirToolStripMenuItem, Me.TéléchargerToolStripMenuItem, Me.SupprimerToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(135, 70)
        '
        'OuvrirToolStripMenuItem
        '
        Me.OuvrirToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Ribbon_OPEN_16x16
        Me.OuvrirToolStripMenuItem.Name = "OuvrirToolStripMenuItem"
        Me.OuvrirToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.OuvrirToolStripMenuItem.Text = "Ouvrir"
        '
        'TéléchargerToolStripMenuItem
        '
        Me.TéléchargerToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.versLeBas
        Me.TéléchargerToolStripMenuItem.Name = "TéléchargerToolStripMenuItem"
        Me.TéléchargerToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.TéléchargerToolStripMenuItem.Text = "Télécharger"
        '
        'SupprimerToolStripMenuItem
        '
        Me.SupprimerToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.SupprimerToolStripMenuItem.Name = "SupprimerToolStripMenuItem"
        Me.SupprimerToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.SupprimerToolStripMenuItem.Text = "Supprimer"
        '
        'navBarGroup1
        '
        Me.navBarGroup1.Caption = "Search"
        Me.navBarGroup1.Expanded = True
        Me.navBarGroup1.GroupClientHeight = 127
        Me.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer
        Me.navBarGroup1.LargeImage = CType(resources.GetObject("navBarGroup1.LargeImage"), System.Drawing.Image)
        Me.navBarGroup1.Name = "navBarGroup1"
        '
        'dockManager1
        '
        Me.dockManager1.Controller = Me.barAndDockingController1
        Me.dockManager1.MenuManager = Me.barManager1
        Me.dockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "System.Windows.Forms.StatusBar"})
        '
        'barAndDockingController1
        '
        Me.barAndDockingController1.PaintStyleName = "Skin"
        Me.barAndDockingController1.PropertiesBar.AllowLinkLighting = False
        '
        'barManager1
        '
        Me.barManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.bar1})
        Me.barManager1.Categories.AddRange(New DevExpress.XtraBars.BarManagerCategory() {New DevExpress.XtraBars.BarManagerCategory("Built-in Menus", New System.Guid("4712321c-b9cd-461f-b453-4a7791063abb")), New DevExpress.XtraBars.BarManagerCategory("Standard", New System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b")), New DevExpress.XtraBars.BarManagerCategory("Address", New System.Guid("fb82a187-cdf0-4f39-a566-c00dbaba593d")), New DevExpress.XtraBars.BarManagerCategory("StatusBar", New System.Guid("2ca54f89-3af6-4cbb-93d8-4a4a9387f283")), New DevExpress.XtraBars.BarManagerCategory("Items", New System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13")), New DevExpress.XtraBars.BarManagerCategory("Favorites", New System.Guid("e1ba440c-33dc-4df6-b712-79cdc4dcd983"))})
        Me.barManager1.Controller = Me.barAndDockingController1
        Me.barManager1.DockControls.Add(Me.barDockControl1)
        Me.barManager1.DockControls.Add(Me.barDockControl2)
        Me.barManager1.DockControls.Add(Me.barDockControl3)
        Me.barManager1.DockControls.Add(Me.barDockControl4)
        Me.barManager1.DockManager = Me.dockManager1
        Me.barManager1.Form = Me
        Me.barManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.siFile, Me.siView, Me.siFavorites, Me.siHelp, Me.BtOpen, Me.BtDownload, Me.BtDelete, Me.iMedia, Me.iGo, Me.eAddress, Me.eProgress, Me.iToolBars, Me.iAbout, Me.iExit, Me.iAdd, Me.iOpen, Me.iSave, Me.ipsWXP, Me.ipsOXP, Me.ipsO2K, Me.iPaintStyle, Me.ipsO3, Me.ipsDefault})
        Me.barManager1.MaxItemId = 39
        Me.barManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.repositoryItemComboBox1, Me.repositoryItemProgressBar1})
        '
        'bar1
        '
        Me.bar1.BarName = "Standard Buttons"
        Me.bar1.DockCol = 0
        Me.bar1.DockRow = 0
        Me.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.bar1.FloatLocation = New System.Drawing.Point(48, 104)
        Me.bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BtOpen), New DevExpress.XtraBars.LinkPersistInfo(Me.BtDownload), New DevExpress.XtraBars.LinkPersistInfo(Me.BtDelete, True)})
        Me.bar1.Text = "Standard Buttons"
        '
        'BtOpen
        '
        Me.BtOpen.Caption = "Ouvrir"
        Me.BtOpen.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right
        Me.BtOpen.CategoryGuid = New System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b")
        Me.BtOpen.Glyph = Global.ClearProject.My.Resources.Resources.Ribbon_OPEN_16x16
        Me.BtOpen.Hint = "Ouvrir"
        Me.BtOpen.Id = 10
        Me.BtOpen.LargeImageIndex = 9
        Me.BtOpen.Name = "BtOpen"
        Me.BtOpen.ShowCaptionOnBar = False
        '
        'BtDownload
        '
        Me.BtDownload.Caption = "Télécharger"
        Me.BtDownload.CategoryGuid = New System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b")
        Me.BtDownload.Hint = "Télécharger"
        Me.BtDownload.Id = 11
        Me.BtDownload.LargeGlyphHot = Global.ClearProject.My.Resources.Resources.versLeBas
        Me.BtDownload.LargeImageIndex = 0
        Me.BtDownload.Name = "BtDownload"
        Me.BtDownload.ShowCaptionOnBar = False
        '
        'BtDelete
        '
        Me.BtDelete.Caption = "Supprimer"
        Me.BtDelete.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right
        Me.BtDelete.CategoryGuid = New System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b")
        Me.BtDelete.Glyph = Global.ClearProject.My.Resources.Resources.Delete_16x16
        Me.BtDelete.Hint = "Supprimer"
        Me.BtDelete.Id = 12
        Me.BtDelete.LargeImageIndex = 10
        Me.BtDelete.Name = "BtDelete"
        '
        'barDockControl1
        '
        Me.barDockControl1.CausesValidation = False
        Me.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControl1.Location = New System.Drawing.Point(0, 0)
        Me.barDockControl1.Size = New System.Drawing.Size(993, 31)
        '
        'barDockControl2
        '
        Me.barDockControl2.CausesValidation = False
        Me.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControl2.Location = New System.Drawing.Point(0, 423)
        Me.barDockControl2.Size = New System.Drawing.Size(993, 0)
        '
        'barDockControl3
        '
        Me.barDockControl3.CausesValidation = False
        Me.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControl3.Location = New System.Drawing.Point(0, 31)
        Me.barDockControl3.Size = New System.Drawing.Size(0, 392)
        '
        'barDockControl4
        '
        Me.barDockControl4.CausesValidation = False
        Me.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControl4.Location = New System.Drawing.Point(993, 31)
        Me.barDockControl4.Size = New System.Drawing.Size(0, 392)
        '
        'siFile
        '
        Me.siFile.Caption = "&File"
        Me.siFile.CategoryGuid = New System.Guid("4712321c-b9cd-461f-b453-4a7791063abb")
        Me.siFile.Id = 0
        Me.siFile.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.iOpen), New DevExpress.XtraBars.LinkPersistInfo(Me.iSave), New DevExpress.XtraBars.LinkPersistInfo(Me.iExit, True)})
        Me.siFile.Name = "siFile"
        '
        'iOpen
        '
        Me.iOpen.Caption = "&Open..."
        Me.iOpen.CategoryGuid = New System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13")
        Me.iOpen.Id = 29
        Me.iOpen.ImageIndex = 0
        Me.iOpen.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O))
        Me.iOpen.Name = "iOpen"
        '
        'iSave
        '
        Me.iSave.Caption = "&Save"
        Me.iSave.CategoryGuid = New System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13")
        Me.iSave.Enabled = False
        Me.iSave.Id = 30
        Me.iSave.ImageIndex = 1
        Me.iSave.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
        Me.iSave.Name = "iSave"
        '
        'iExit
        '
        Me.iExit.Caption = "E&xit"
        Me.iExit.CategoryGuid = New System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13")
        Me.iExit.Id = 27
        Me.iExit.Name = "iExit"
        '
        'siView
        '
        Me.siView.Caption = "&View"
        Me.siView.CategoryGuid = New System.Guid("4712321c-b9cd-461f-b453-4a7791063abb")
        Me.siView.Id = 2
        Me.siView.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.iToolBars)})
        Me.siView.Name = "siView"
        '
        'iToolBars
        '
        Me.iToolBars.Caption = "ToolBarsList"
        Me.iToolBars.CategoryGuid = New System.Guid("4712321c-b9cd-461f-b453-4a7791063abb")
        Me.iToolBars.Id = 25
        Me.iToolBars.Name = "iToolBars"
        '
        'siFavorites
        '
        Me.siFavorites.Caption = "F&avorites"
        Me.siFavorites.CategoryGuid = New System.Guid("4712321c-b9cd-461f-b453-4a7791063abb")
        Me.siFavorites.Id = 3
        Me.siFavorites.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.iAdd)})
        Me.siFavorites.Name = "siFavorites"
        '
        'iAdd
        '
        Me.iAdd.Caption = "Add to Favorites..."
        Me.iAdd.CategoryGuid = New System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13")
        Me.iAdd.Id = 28
        Me.iAdd.ItemAppearance.Normal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.iAdd.ItemAppearance.Normal.Options.UseFont = True
        Me.iAdd.Name = "iAdd"
        '
        'siHelp
        '
        Me.siHelp.Caption = "&Help"
        Me.siHelp.CategoryGuid = New System.Guid("4712321c-b9cd-461f-b453-4a7791063abb")
        Me.siHelp.Id = 4
        Me.siHelp.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.iAbout)})
        Me.siHelp.Name = "siHelp"
        '
        'iAbout
        '
        Me.iAbout.Caption = "&About"
        Me.iAbout.CategoryGuid = New System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13")
        Me.iAbout.Id = 26
        Me.iAbout.Name = "iAbout"
        '
        'iMedia
        '
        Me.iMedia.Caption = "Media"
        Me.iMedia.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right
        Me.iMedia.CategoryGuid = New System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b")
        Me.iMedia.Hint = "Media"
        Me.iMedia.Id = 15
        Me.iMedia.LargeImageIndex = 7
        Me.iMedia.Name = "iMedia"
        '
        'iGo
        '
        Me.iGo.Caption = "Go"
        Me.iGo.CategoryGuid = New System.Guid("fb82a187-cdf0-4f39-a566-c00dbaba593d")
        Me.iGo.Glyph = CType(resources.GetObject("iGo.Glyph"), System.Drawing.Image)
        Me.iGo.Hint = "Go to ..."
        Me.iGo.Id = 20
        Me.iGo.Name = "iGo"
        Me.iGo.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'eAddress
        '
        Me.eAddress.AutoFillWidth = True
        Me.eAddress.Caption = "Address"
        Me.eAddress.CategoryGuid = New System.Guid("fb82a187-cdf0-4f39-a566-c00dbaba593d")
        Me.eAddress.Edit = Me.repositoryItemComboBox1
        Me.eAddress.Id = 21
        Me.eAddress.IEBehavior = True
        Me.eAddress.Name = "eAddress"
        Me.eAddress.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.eAddress.Width = 400
        '
        'repositoryItemComboBox1
        '
        Me.repositoryItemComboBox1.AllowFocused = False
        Me.repositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.repositoryItemComboBox1.CycleOnDblClick = False
        Me.repositoryItemComboBox1.Name = "repositoryItemComboBox1"
        '
        'eProgress
        '
        Me.eProgress.CanOpenEdit = False
        Me.eProgress.CategoryGuid = New System.Guid("2ca54f89-3af6-4cbb-93d8-4a4a9387f283")
        Me.eProgress.Edit = Me.repositoryItemProgressBar1
        Me.eProgress.EditHeight = 10
        Me.eProgress.Id = 24
        Me.eProgress.Name = "eProgress"
        Me.eProgress.Width = 70
        '
        'repositoryItemProgressBar1
        '
        Me.repositoryItemProgressBar1.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.repositoryItemProgressBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1"
        '
        'ipsWXP
        '
        Me.ipsWXP.Caption = "Windows XP"
        Me.ipsWXP.CategoryGuid = New System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13")
        Me.ipsWXP.Description = "WindowsXP"
        Me.ipsWXP.Id = 32
        Me.ipsWXP.ImageIndex = 4
        Me.ipsWXP.Name = "ipsWXP"
        '
        'ipsOXP
        '
        Me.ipsOXP.Caption = "Office XP"
        Me.ipsOXP.CategoryGuid = New System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13")
        Me.ipsOXP.Description = "OfficeXP"
        Me.ipsOXP.Id = 33
        Me.ipsOXP.ImageIndex = 2
        Me.ipsOXP.Name = "ipsOXP"
        '
        'ipsO2K
        '
        Me.ipsO2K.Caption = "Office 2000"
        Me.ipsO2K.CategoryGuid = New System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13")
        Me.ipsO2K.Description = "Office2000"
        Me.ipsO2K.Id = 34
        Me.ipsO2K.ImageIndex = 3
        Me.ipsO2K.Name = "ipsO2K"
        '
        'iPaintStyle
        '
        Me.iPaintStyle.Caption = "Paint Style"
        Me.iPaintStyle.CategoryGuid = New System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13")
        Me.iPaintStyle.Id = 35
        Me.iPaintStyle.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.ipsDefault), New DevExpress.XtraBars.LinkPersistInfo(Me.ipsWXP), New DevExpress.XtraBars.LinkPersistInfo(Me.ipsOXP), New DevExpress.XtraBars.LinkPersistInfo(Me.ipsO2K), New DevExpress.XtraBars.LinkPersistInfo(Me.ipsO3)})
        Me.iPaintStyle.Name = "iPaintStyle"
        Me.iPaintStyle.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'ipsDefault
        '
        Me.ipsDefault.Caption = "Default"
        Me.ipsDefault.CategoryGuid = New System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13")
        Me.ipsDefault.Description = "Default"
        Me.ipsDefault.Id = 37
        Me.ipsDefault.Name = "ipsDefault"
        '
        'ipsO3
        '
        Me.ipsO3.Caption = "Office 2003"
        Me.ipsO3.CategoryGuid = New System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13")
        Me.ipsO3.Description = "Office2003"
        Me.ipsO3.Id = 36
        Me.ipsO3.ImageIndex = 5
        Me.ipsO3.Name = "ipsO3"
        '
        'CombBail
        '
        Me.CombBail.Location = New System.Drawing.Point(177, 3)
        Me.CombBail.Margin = New System.Windows.Forms.Padding(2)
        Me.CombBail.MenuManager = Me.barManager1
        Me.CombBail.Name = "CombBail"
        Me.CombBail.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CombBail.Size = New System.Drawing.Size(808, 20)
        Me.CombBail.TabIndex = 5
        '
        'PMPochette
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(993, 423)
        Me.Controls.Add(Me.CombBail)
        Me.Controls.Add(Me.treeLPochette)
        Me.Controls.Add(Me.barDockControl3)
        Me.Controls.Add(Me.barDockControl4)
        Me.Controls.Add(Me.barDockControl2)
        Me.Controls.Add(Me.barDockControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PMPochette"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pochette"
        CType(Me.treeLPochette, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.dockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barAndDockingController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CombBail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents treeLPochette As DevExpress.XtraTreeList.TreeList
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SupprimerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OuvrirToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TéléchargerToolStripMenuItem As ToolStripMenuItem
    Private WithEvents navBarGroup1 As DevExpress.XtraNavBar.NavBarGroup
    Private WithEvents dockManager1 As DevExpress.XtraBars.Docking.DockManager
    Private WithEvents barAndDockingController1 As DevExpress.XtraBars.BarAndDockingController
    Private WithEvents barManager1 As DevExpress.XtraBars.BarManager
    Private WithEvents bar1 As DevExpress.XtraBars.Bar
    Private WithEvents BtOpen As DevExpress.XtraBars.BarLargeButtonItem
    Private WithEvents BtDownload As DevExpress.XtraBars.BarLargeButtonItem
    Private WithEvents BtDelete As DevExpress.XtraBars.BarLargeButtonItem
    Private WithEvents barDockControl1 As DevExpress.XtraBars.BarDockControl
    Private WithEvents barDockControl2 As DevExpress.XtraBars.BarDockControl
    Private WithEvents barDockControl3 As DevExpress.XtraBars.BarDockControl
    Private WithEvents barDockControl4 As DevExpress.XtraBars.BarDockControl
    Private WithEvents siFile As DevExpress.XtraBars.BarSubItem
    Private WithEvents iOpen As DevExpress.XtraBars.BarButtonItem
    Private WithEvents iSave As DevExpress.XtraBars.BarButtonItem
    Private WithEvents iExit As DevExpress.XtraBars.BarButtonItem
    Private WithEvents siView As DevExpress.XtraBars.BarSubItem
    Private WithEvents iToolBars As DevExpress.XtraBars.BarToolbarsListItem
    Private WithEvents siFavorites As DevExpress.XtraBars.BarSubItem
    Private WithEvents iAdd As DevExpress.XtraBars.BarButtonItem
    Private WithEvents siHelp As DevExpress.XtraBars.BarSubItem
    Private WithEvents iAbout As DevExpress.XtraBars.BarButtonItem
    Private WithEvents iMedia As DevExpress.XtraBars.BarLargeButtonItem
    Private WithEvents iGo As DevExpress.XtraBars.BarButtonItem
    Private WithEvents eAddress As DevExpress.XtraBars.BarEditItem
    Private WithEvents repositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Private WithEvents eProgress As DevExpress.XtraBars.BarEditItem
    Private WithEvents repositoryItemProgressBar1 As DevExpress.XtraEditors.Repository.RepositoryItemProgressBar
    Private WithEvents ipsWXP As DevExpress.XtraBars.BarButtonItem
    Private WithEvents ipsOXP As DevExpress.XtraBars.BarButtonItem
    Private WithEvents ipsO2K As DevExpress.XtraBars.BarButtonItem
    Private WithEvents iPaintStyle As DevExpress.XtraBars.BarSubItem
    Private WithEvents ipsDefault As DevExpress.XtraBars.BarButtonItem
    Private WithEvents ipsO3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents CombBail As DevExpress.XtraEditors.ComboBoxEdit
End Class
