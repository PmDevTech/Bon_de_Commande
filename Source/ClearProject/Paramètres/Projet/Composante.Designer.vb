<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Composante
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
        Me.GridCompo = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SupprimerComposanteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewCompo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TxtCompo = New DevExpress.XtraEditors.TextEdit()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.Rafraichir = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GridCompo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.ViewCompo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCompo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridCompo
        '
        Me.GridCompo.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GridCompo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridCompo.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridCompo.Location = New System.Drawing.Point(0, 79)
        Me.GridCompo.MainView = Me.ViewCompo
        Me.GridCompo.Name = "GridCompo"
        Me.GridCompo.Size = New System.Drawing.Size(759, 284)
        Me.GridCompo.TabIndex = 12
        Me.GridCompo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewCompo})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SupprimerComposanteToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(181, 48)
        '
        'SupprimerComposanteToolStripMenuItem
        '
        Me.SupprimerComposanteToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.SupprimerComposanteToolStripMenuItem.Name = "SupprimerComposanteToolStripMenuItem"
        Me.SupprimerComposanteToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SupprimerComposanteToolStripMenuItem.Text = "Supprimer"
        '
        'ViewCompo
        '
        Me.ViewCompo.ActiveFilterEnabled = False
        Me.ViewCompo.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewCompo.Appearance.Row.Options.UseFont = True
        Me.ViewCompo.GridControl = Me.GridCompo
        Me.ViewCompo.Name = "ViewCompo"
        Me.ViewCompo.OptionsBehavior.Editable = False
        Me.ViewCompo.OptionsBehavior.ReadOnly = True
        Me.ViewCompo.OptionsCustomization.AllowColumnMoving = False
        Me.ViewCompo.OptionsCustomization.AllowFilter = False
        Me.ViewCompo.OptionsCustomization.AllowGroup = False
        Me.ViewCompo.OptionsCustomization.AllowSort = False
        Me.ViewCompo.OptionsFilter.AllowFilterEditor = False
        Me.ViewCompo.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewCompo.OptionsPrint.AutoWidth = False
        Me.ViewCompo.OptionsView.ColumnAutoWidth = False
        Me.ViewCompo.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewCompo.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewCompo.OptionsView.ShowGroupPanel = False
        Me.ViewCompo.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewCompo.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'TxtCompo
        '
        Me.TxtCompo.Dock = System.Windows.Forms.DockStyle.Top
        Me.TxtCompo.Location = New System.Drawing.Point(0, 27)
        Me.TxtCompo.Name = "TxtCompo"
        Me.TxtCompo.Properties.AutoHeight = False
        Me.TxtCompo.Size = New System.Drawing.Size(759, 52)
        Me.TxtCompo.TabIndex = 2
        Me.TxtCompo.ToolTip = "Ajout de la Composante"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.Rafraichir)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(759, 27)
        Me.PanelControl1.TabIndex = 13
        '
        'Rafraichir
        '
        Me.Rafraichir.Dock = System.Windows.Forms.DockStyle.Right
        Me.Rafraichir.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_161
        Me.Rafraichir.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.Rafraichir.Location = New System.Drawing.Point(728, 2)
        Me.Rafraichir.Name = "Rafraichir"
        Me.Rafraichir.Size = New System.Drawing.Size(29, 23)
        Me.Rafraichir.TabIndex = 1
        '
        'Composante
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(759, 363)
        Me.Controls.Add(Me.GridCompo)
        Me.Controls.Add(Me.TxtCompo)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Composante"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Composantes"
        CType(Me.GridCompo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.ViewCompo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCompo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridCompo As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewCompo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TxtCompo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SupprimerComposanteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents Rafraichir As DevExpress.XtraEditors.SimpleButton
End Class
