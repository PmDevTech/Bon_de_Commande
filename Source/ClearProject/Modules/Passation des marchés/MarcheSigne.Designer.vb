<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MarcheSigne
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
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.CmbEtat = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ChEtat = New DevExpress.XtraEditors.CheckEdit()
        Me.TxtNumMarcheSearch = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.CmbFournis = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ChkFournis = New DevExpress.XtraEditors.CheckEdit()
        Me.CmbDAO = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ChkDAO = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.GridMarche = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ConsulterLeDossierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ImprimerLeDossierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewMarche = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.CmbEtat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChEtat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtNumMarcheSearch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbFournis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkFournis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbDAO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkDAO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridMarche, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.ViewMarche, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.PanelControl2)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1003, 70)
        Me.PanelControl1.TabIndex = 0
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.CmbEtat)
        Me.PanelControl2.Controls.Add(Me.ChEtat)
        Me.PanelControl2.Controls.Add(Me.TxtNumMarcheSearch)
        Me.PanelControl2.Controls.Add(Me.LabelControl3)
        Me.PanelControl2.Controls.Add(Me.CmbFournis)
        Me.PanelControl2.Controls.Add(Me.ChkFournis)
        Me.PanelControl2.Controls.Add(Me.CmbDAO)
        Me.PanelControl2.Controls.Add(Me.ChkDAO)
        Me.PanelControl2.Controls.Add(Me.LabelControl2)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl2.Location = New System.Drawing.Point(2, 36)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(999, 32)
        Me.PanelControl2.TabIndex = 1
        '
        'CmbEtat
        '
        Me.CmbEtat.EditValue = ""
        Me.CmbEtat.Enabled = False
        Me.CmbEtat.Location = New System.Drawing.Point(589, 5)
        Me.CmbEtat.Name = "CmbEtat"
        Me.CmbEtat.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbEtat.Properties.Appearance.Options.UseFont = True
        Me.CmbEtat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbEtat.Properties.Items.AddRange(New Object() {"En cours", "Annulé", "Terminé"})
        Me.CmbEtat.Size = New System.Drawing.Size(87, 22)
        Me.CmbEtat.TabIndex = 8
        '
        'ChEtat
        '
        Me.ChEtat.Location = New System.Drawing.Point(541, 7)
        Me.ChEtat.Name = "ChEtat"
        Me.ChEtat.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChEtat.Properties.Appearance.Options.UseFont = True
        Me.ChEtat.Properties.Caption = "Etat"
        Me.ChEtat.Size = New System.Drawing.Size(46, 20)
        Me.ChEtat.TabIndex = 7
        '
        'TxtNumMarcheSearch
        '
        Me.TxtNumMarcheSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtNumMarcheSearch.Location = New System.Drawing.Point(819, 6)
        Me.TxtNumMarcheSearch.Name = "TxtNumMarcheSearch"
        Me.TxtNumMarcheSearch.Size = New System.Drawing.Size(175, 20)
        Me.TxtNumMarcheSearch.TabIndex = 6
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.LineLocation = DevExpress.XtraEditors.LineLocation.Left
        Me.LabelControl3.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Vertical
        Me.LabelControl3.LineVisible = True
        Me.LabelControl3.Location = New System.Drawing.Point(690, 9)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(127, 15)
        Me.LabelControl3.TabIndex = 5
        Me.LabelControl3.Text = "  RECHERCHER :    N°"
        '
        'CmbFournis
        '
        Me.CmbFournis.Enabled = False
        Me.CmbFournis.Location = New System.Drawing.Point(362, 5)
        Me.CmbFournis.Name = "CmbFournis"
        Me.CmbFournis.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbFournis.Properties.Appearance.Options.UseFont = True
        Me.CmbFournis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbFournis.Size = New System.Drawing.Size(170, 22)
        Me.CmbFournis.TabIndex = 4
        '
        'ChkFournis
        '
        Me.ChkFournis.Location = New System.Drawing.Point(273, 7)
        Me.ChkFournis.Name = "ChkFournis"
        Me.ChkFournis.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkFournis.Properties.Appearance.Options.UseFont = True
        Me.ChkFournis.Properties.Caption = "Fournisseur"
        Me.ChkFournis.Size = New System.Drawing.Size(89, 20)
        Me.ChkFournis.TabIndex = 3
        '
        'CmbDAO
        '
        Me.CmbDAO.EditValue = ""
        Me.CmbDAO.Enabled = False
        Me.CmbDAO.Location = New System.Drawing.Point(126, 5)
        Me.CmbDAO.Name = "CmbDAO"
        Me.CmbDAO.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbDAO.Properties.Appearance.Options.UseFont = True
        Me.CmbDAO.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbDAO.Size = New System.Drawing.Size(141, 22)
        Me.CmbDAO.TabIndex = 2
        '
        'ChkDAO
        '
        Me.ChkDAO.Location = New System.Drawing.Point(75, 7)
        Me.ChkDAO.Name = "ChkDAO"
        Me.ChkDAO.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkDAO.Properties.Appearance.Options.UseFont = True
        Me.ChkDAO.Properties.Caption = "DAO"
        Me.ChkDAO.Size = New System.Drawing.Size(51, 20)
        Me.ChkDAO.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(5, 9)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(68, 15)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "AFFICHER :"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(436, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(190, 24)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "MARCHES PASSES"
        '
        'GridMarche
        '
        Me.GridMarche.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GridMarche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridMarche.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridMarche.Location = New System.Drawing.Point(0, 70)
        Me.GridMarche.MainView = Me.ViewMarche
        Me.GridMarche.Name = "GridMarche"
        Me.GridMarche.Size = New System.Drawing.Size(1003, 400)
        Me.GridMarche.TabIndex = 8
        Me.GridMarche.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewMarche})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConsulterLeDossierToolStripMenuItem, Me.ToolStripSeparator1, Me.ImprimerLeDossierToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(178, 54)
        '
        'ConsulterLeDossierToolStripMenuItem
        '
        Me.ConsulterLeDossierToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Ribbon_OPEN_16x16
        Me.ConsulterLeDossierToolStripMenuItem.Name = "ConsulterLeDossierToolStripMenuItem"
        Me.ConsulterLeDossierToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ConsulterLeDossierToolStripMenuItem.Text = "Consulter le dossier"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(174, 6)
        '
        'ImprimerLeDossierToolStripMenuItem
        '
        Me.ImprimerLeDossierToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Group_Reports
        Me.ImprimerLeDossierToolStripMenuItem.Name = "ImprimerLeDossierToolStripMenuItem"
        Me.ImprimerLeDossierToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ImprimerLeDossierToolStripMenuItem.Text = "Imprimer le dossier"
        '
        'ViewMarche
        '
        Me.ViewMarche.ActiveFilterEnabled = False
        Me.ViewMarche.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewMarche.Appearance.Row.Options.UseFont = True
        Me.ViewMarche.GridControl = Me.GridMarche
        Me.ViewMarche.Name = "ViewMarche"
        Me.ViewMarche.OptionsBehavior.Editable = False
        Me.ViewMarche.OptionsBehavior.ReadOnly = True
        Me.ViewMarche.OptionsCustomization.AllowColumnMoving = False
        Me.ViewMarche.OptionsCustomization.AllowFilter = False
        Me.ViewMarche.OptionsCustomization.AllowGroup = False
        Me.ViewMarche.OptionsCustomization.AllowSort = False
        Me.ViewMarche.OptionsFilter.AllowFilterEditor = False
        Me.ViewMarche.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewMarche.OptionsPrint.AutoWidth = False
        Me.ViewMarche.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewMarche.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewMarche.OptionsView.ColumnAutoWidth = False
        Me.ViewMarche.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewMarche.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewMarche.OptionsView.ShowGroupPanel = False
        Me.ViewMarche.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewMarche.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'MarcheSigne
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1003, 470)
        Me.Controls.Add(Me.GridMarche)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MarcheSigne"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Marché signé"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.PanelControl2.PerformLayout()
        CType(Me.CmbEtat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChEtat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtNumMarcheSearch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbFournis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkFournis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbDAO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkDAO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridMarche, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.ViewMarche, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents CmbFournis As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ChkFournis As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents CmbDAO As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ChkDAO As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtNumMarcheSearch As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GridMarche As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewMarche As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CmbEtat As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ChEtat As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ConsulterLeDossierToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ImprimerLeDossierToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
