<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DiagChoixTypeFourniture
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btClose = New DevExpress.XtraEditors.SimpleButton()
        Me.BtSelectItem = New DevExpress.XtraEditors.SimpleButton()
        Me.btAnnuler = New DevExpress.XtraEditors.SimpleButton()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.btSelectionnerItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btModifierItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btSupprimerItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PanelControl4 = New DevExpress.XtraEditors.PanelControl()
        Me.PanelControl5 = New DevExpress.XtraEditors.PanelControl()
        Me.GridCategorie = New DevExpress.XtraGrid.GridControl()
        Me.GridViewCategorie = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.TxtSearch = New DevExpress.XtraEditors.TextEdit()
        Me.BtTypeRecherche = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtSousCategorie = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl53 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbCategorie = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl4.SuspendLayout()
        CType(Me.PanelControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl5.SuspendLayout()
        CType(Me.GridCategorie, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewCategorie, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.TxtSearch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.txtSousCategorie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCategorie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btClose)
        Me.PanelControl1.Controls.Add(Me.BtSelectItem)
        Me.PanelControl1.Controls.Add(Me.btAnnuler)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 375)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(643, 31)
        Me.PanelControl1.TabIndex = 0
        '
        'btClose
        '
        Me.btClose.Dock = System.Windows.Forms.DockStyle.Left
        Me.btClose.Image = Global.ClearProject.My.Resources.Resources.Close_16x16
        Me.btClose.Location = New System.Drawing.Point(94, 2)
        Me.btClose.Name = "btClose"
        Me.btClose.Size = New System.Drawing.Size(92, 27)
        Me.btClose.TabIndex = 22
        Me.btClose.Text = "Fermer"
        '
        'BtSelectItem
        '
        Me.BtSelectItem.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtSelectItem.Image = Global.ClearProject.My.Resources.Resources.ActiveRents_16x16
        Me.BtSelectItem.Location = New System.Drawing.Point(549, 2)
        Me.BtSelectItem.Name = "BtSelectItem"
        Me.BtSelectItem.Size = New System.Drawing.Size(92, 27)
        Me.BtSelectItem.TabIndex = 22
        Me.BtSelectItem.Text = "Sélectionner"
        '
        'btAnnuler
        '
        Me.btAnnuler.Dock = System.Windows.Forms.DockStyle.Left
        Me.btAnnuler.Image = Global.ClearProject.My.Resources.Resources.Return_16x16
        Me.btAnnuler.Location = New System.Drawing.Point(2, 2)
        Me.btAnnuler.Name = "btAnnuler"
        Me.btAnnuler.Size = New System.Drawing.Size(92, 27)
        Me.btAnnuler.TabIndex = 23
        Me.btAnnuler.Text = "Annuler"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btSelectionnerItem, Me.btModifierItem, Me.btSupprimerItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(140, 70)
        '
        'btSelectionnerItem
        '
        Me.btSelectionnerItem.Image = Global.ClearProject.My.Resources.Resources.ActiveRents_16x16
        Me.btSelectionnerItem.Name = "btSelectionnerItem"
        Me.btSelectionnerItem.Size = New System.Drawing.Size(139, 22)
        Me.btSelectionnerItem.Text = "Sélectionner"
        '
        'btModifierItem
        '
        Me.btModifierItem.Image = Global.ClearProject.My.Resources.Resources.Edit_16x16
        Me.btModifierItem.Name = "btModifierItem"
        Me.btModifierItem.Size = New System.Drawing.Size(139, 22)
        Me.btModifierItem.Text = "Modifier"
        '
        'btSupprimerItem
        '
        Me.btSupprimerItem.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.btSupprimerItem.Name = "btSupprimerItem"
        Me.btSupprimerItem.Size = New System.Drawing.Size(139, 22)
        Me.btSupprimerItem.Text = "Supprimer"
        '
        'PanelControl4
        '
        Me.PanelControl4.Controls.Add(Me.PanelControl5)
        Me.PanelControl4.Controls.Add(Me.PanelControl3)
        Me.PanelControl4.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl4.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl4.Name = "PanelControl4"
        Me.PanelControl4.Size = New System.Drawing.Size(643, 375)
        Me.PanelControl4.TabIndex = 5
        '
        'PanelControl5
        '
        Me.PanelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl5.Controls.Add(Me.GridCategorie)
        Me.PanelControl5.Controls.Add(Me.PanelControl2)
        Me.PanelControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl5.Location = New System.Drawing.Point(2, 72)
        Me.PanelControl5.Name = "PanelControl5"
        Me.PanelControl5.Size = New System.Drawing.Size(639, 301)
        Me.PanelControl5.TabIndex = 4
        '
        'GridCategorie
        '
        Me.GridCategorie.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GridCategorie.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridCategorie.Location = New System.Drawing.Point(0, 26)
        Me.GridCategorie.MainView = Me.GridViewCategorie
        Me.GridCategorie.Name = "GridCategorie"
        Me.GridCategorie.Size = New System.Drawing.Size(639, 275)
        Me.GridCategorie.TabIndex = 3
        Me.GridCategorie.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewCategorie})
        '
        'GridViewCategorie
        '
        Me.GridViewCategorie.ActiveFilterEnabled = False
        Me.GridViewCategorie.GridControl = Me.GridCategorie
        Me.GridViewCategorie.Name = "GridViewCategorie"
        Me.GridViewCategorie.OptionsBehavior.Editable = False
        Me.GridViewCategorie.OptionsBehavior.ReadOnly = True
        Me.GridViewCategorie.OptionsCustomization.AllowColumnMoving = False
        Me.GridViewCategorie.OptionsCustomization.AllowFilter = False
        Me.GridViewCategorie.OptionsCustomization.AllowGroup = False
        Me.GridViewCategorie.OptionsFilter.AllowFilterEditor = False
        Me.GridViewCategorie.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.GridViewCategorie.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridViewCategorie.OptionsView.ColumnAutoWidth = False
        Me.GridViewCategorie.OptionsView.ShowColumnHeaders = False
        Me.GridViewCategorie.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GridViewCategorie.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.GridViewCategorie.OptionsView.ShowGroupPanel = False
        Me.GridViewCategorie.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridViewCategorie.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.TxtSearch)
        Me.PanelControl2.Controls.Add(Me.BtTypeRecherche)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(639, 26)
        Me.PanelControl2.TabIndex = 4
        '
        'TxtSearch
        '
        Me.TxtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtSearch.Location = New System.Drawing.Point(28, 1)
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSearch.Properties.Appearance.Options.UseFont = True
        Me.TxtSearch.Size = New System.Drawing.Size(611, 22)
        Me.TxtSearch.TabIndex = 15
        '
        'BtTypeRecherche
        '
        Me.BtTypeRecherche.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtTypeRecherche.Appearance.ForeColor = System.Drawing.Color.Black
        Me.BtTypeRecherche.Appearance.Options.UseFont = True
        Me.BtTypeRecherche.Appearance.Options.UseForeColor = True
        Me.BtTypeRecherche.Appearance.Options.UseTextOptions = True
        Me.BtTypeRecherche.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BtTypeRecherche.Image = Global.ClearProject.My.Resources.Resources.Preview_16x16
        Me.BtTypeRecherche.Location = New System.Drawing.Point(0, 1)
        Me.BtTypeRecherche.Name = "BtTypeRecherche"
        Me.BtTypeRecherche.Size = New System.Drawing.Size(26, 22)
        Me.BtTypeRecherche.TabIndex = 14
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.LabelControl1)
        Me.PanelControl3.Controls.Add(Me.txtSousCategorie)
        Me.PanelControl3.Controls.Add(Me.LabelControl53)
        Me.PanelControl3.Controls.Add(Me.cmbCategorie)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl3.Location = New System.Drawing.Point(2, 2)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(639, 70)
        Me.PanelControl3.TabIndex = 4
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(40, 39)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(84, 15)
        Me.LabelControl1.TabIndex = 20
        Me.LabelControl1.Text = "Libellé sous lot"
        '
        'txtSousCategorie
        '
        Me.txtSousCategorie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSousCategorie.Location = New System.Drawing.Point(131, 40)
        Me.txtSousCategorie.Name = "txtSousCategorie"
        Me.txtSousCategorie.Size = New System.Drawing.Size(493, 20)
        Me.txtSousCategorie.TabIndex = 21
        '
        'LabelControl53
        '
        Me.LabelControl53.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl53.Location = New System.Drawing.Point(40, 12)
        Me.LabelControl53.Name = "LabelControl53"
        Me.LabelControl53.Size = New System.Drawing.Size(57, 15)
        Me.LabelControl53.TabIndex = 19
        Me.LabelControl53.Text = "Libellé lot"
        '
        'cmbCategorie
        '
        Me.cmbCategorie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbCategorie.Location = New System.Drawing.Point(131, 11)
        Me.cmbCategorie.Name = "cmbCategorie"
        Me.cmbCategorie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCategorie.Size = New System.Drawing.Size(493, 20)
        Me.cmbCategorie.TabIndex = 5
        '
        'DiagChoixTypeFourniture
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(643, 406)
        Me.Controls.Add(Me.PanelControl4)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DiagChoixTypeFourniture"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Catégories de Biens, Fournitures et Matériels"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl4.ResumeLayout(False)
        CType(Me.PanelControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl5.ResumeLayout(False)
        CType(Me.GridCategorie, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewCategorie, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.TxtSearch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        Me.PanelControl3.PerformLayout()
        CType(Me.txtSousCategorie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCategorie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtSelectItem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents btSelectionnerItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btModifierItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btSupprimerItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelControl4 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl5 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GridCategorie As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewCategorie As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents TxtSearch As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BtTypeRecherche As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSousCategorie As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl53 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btAnnuler As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmbCategorie As DevExpress.XtraEditors.ComboBoxEdit
End Class
