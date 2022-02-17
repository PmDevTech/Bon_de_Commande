<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AjoutMontantSousLot
    Inherits DevExpress.XtraEditors.XtraForm

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.GridSousLot = New DevExpress.XtraEditors.GroupControl()
        Me.BtAjoutMontant = New DevExpress.XtraEditors.SimpleButton()
        Me.GridMontantSL = New DevExpress.XtraGrid.GridControl()
        Me.ViewMontantSL = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LabelControl25 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbSousLot = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtMontantSousLot = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl26 = New DevExpress.XtraEditors.LabelControl()
        Me.txtLibelleSousLot = New DevExpress.XtraEditors.TextEdit()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SupprimerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.GridSousLot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GridSousLot.SuspendLayout()
        CType(Me.GridMontantSL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewMontantSL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSousLot.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMontantSousLot.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLibelleSousLot.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridSousLot
        '
        Me.GridSousLot.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridSousLot.AppearanceCaption.Options.UseFont = True
        Me.GridSousLot.Controls.Add(Me.BtAjoutMontant)
        Me.GridSousLot.Controls.Add(Me.GridMontantSL)
        Me.GridSousLot.Controls.Add(Me.LabelControl25)
        Me.GridSousLot.Controls.Add(Me.cmbSousLot)
        Me.GridSousLot.Controls.Add(Me.txtMontantSousLot)
        Me.GridSousLot.Controls.Add(Me.LabelControl26)
        Me.GridSousLot.Controls.Add(Me.txtLibelleSousLot)
        Me.GridSousLot.Location = New System.Drawing.Point(0, 1)
        Me.GridSousLot.Name = "GridSousLot"
        Me.GridSousLot.Size = New System.Drawing.Size(359, 178)
        Me.GridSousLot.TabIndex = 10
        Me.GridSousLot.Text = "Montant sous lot"
        '
        'BtAjoutMontant
        '
        Me.BtAjoutMontant.Image = Global.ClearProject.My.Resources.Resources.Add_16x16
        Me.BtAjoutMontant.Location = New System.Drawing.Point(331, 51)
        Me.BtAjoutMontant.Name = "BtAjoutMontant"
        Me.BtAjoutMontant.Size = New System.Drawing.Size(23, 21)
        Me.BtAjoutMontant.TabIndex = 77
        Me.BtAjoutMontant.Text = "Enregistrer"
        '
        'GridMontantSL
        '
        Me.GridMontantSL.ContextMenuStrip = Me.ContextMenuStrip2
        Me.GridMontantSL.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridMontantSL.Location = New System.Drawing.Point(60, 78)
        Me.GridMontantSL.MainView = Me.ViewMontantSL
        Me.GridMontantSL.Name = "GridMontantSL"
        Me.GridMontantSL.Size = New System.Drawing.Size(294, 95)
        Me.GridMontantSL.TabIndex = 8
        Me.GridMontantSL.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewMontantSL})
        '
        'ViewMontantSL
        '
        Me.ViewMontantSL.ActiveFilterEnabled = False
        Me.ViewMontantSL.GridControl = Me.GridMontantSL
        Me.ViewMontantSL.Name = "ViewMontantSL"
        Me.ViewMontantSL.OptionsBehavior.Editable = False
        Me.ViewMontantSL.OptionsBehavior.ReadOnly = True
        Me.ViewMontantSL.OptionsCustomization.AllowColumnMoving = False
        Me.ViewMontantSL.OptionsCustomization.AllowFilter = False
        Me.ViewMontantSL.OptionsCustomization.AllowGroup = False
        Me.ViewMontantSL.OptionsFilter.AllowFilterEditor = False
        Me.ViewMontantSL.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewMontantSL.OptionsPrint.PrintHeader = False
        Me.ViewMontantSL.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewMontantSL.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewMontantSL.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewMontantSL.OptionsView.ShowGroupPanel = False
        Me.ViewMontantSL.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewMontantSL.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'LabelControl25
        '
        Me.LabelControl25.Location = New System.Drawing.Point(9, 29)
        Me.LabelControl25.Name = "LabelControl25"
        Me.LabelControl25.Size = New System.Drawing.Size(38, 13)
        Me.LabelControl25.TabIndex = 29
        Me.LabelControl25.Text = "Sous lot"
        '
        'cmbSousLot
        '
        Me.cmbSousLot.Location = New System.Drawing.Point(60, 26)
        Me.cmbSousLot.Name = "cmbSousLot"
        Me.cmbSousLot.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbSousLot.Size = New System.Drawing.Size(53, 20)
        Me.cmbSousLot.TabIndex = 30
        '
        'txtMontantSousLot
        '
        Me.txtMontantSousLot.Location = New System.Drawing.Point(60, 52)
        Me.txtMontantSousLot.Name = "txtMontantSousLot"
        Me.txtMontantSousLot.Properties.Appearance.Options.UseTextOptions = True
        Me.txtMontantSousLot.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtMontantSousLot.Properties.Mask.BeepOnError = True
        Me.txtMontantSousLot.Properties.Mask.EditMask = "n0"
        Me.txtMontantSousLot.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtMontantSousLot.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtMontantSousLot.Properties.MaxLength = 20
        Me.txtMontantSousLot.Size = New System.Drawing.Size(260, 20)
        Me.txtMontantSousLot.TabIndex = 76
        '
        'LabelControl26
        '
        Me.LabelControl26.Location = New System.Drawing.Point(9, 55)
        Me.LabelControl26.Name = "LabelControl26"
        Me.LabelControl26.Size = New System.Drawing.Size(40, 13)
        Me.LabelControl26.TabIndex = 75
        Me.LabelControl26.Text = "Montant"
        '
        'txtLibelleSousLot
        '
        Me.txtLibelleSousLot.Location = New System.Drawing.Point(119, 26)
        Me.txtLibelleSousLot.Name = "txtLibelleSousLot"
        Me.txtLibelleSousLot.Properties.Appearance.Options.UseTextOptions = True
        Me.txtLibelleSousLot.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.txtLibelleSousLot.Properties.ReadOnly = True
        Me.txtLibelleSousLot.Size = New System.Drawing.Size(235, 20)
        Me.txtLibelleSousLot.TabIndex = 31
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SupprimerToolStripMenuItem})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(181, 48)
        '
        'SupprimerToolStripMenuItem
        '
        Me.SupprimerToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.SupprimerToolStripMenuItem.Name = "SupprimerToolStripMenuItem"
        Me.SupprimerToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SupprimerToolStripMenuItem.Text = "Supprimer"
        '
        'AjoutMontantSousLot2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(358, 180)
        Me.Controls.Add(Me.GridSousLot)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AjoutMontantSousLot2"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ajout des montant des sous lots"
        CType(Me.GridSousLot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GridSousLot.ResumeLayout(False)
        Me.GridSousLot.PerformLayout()
        CType(Me.GridMontantSL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewMontantSL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSousLot.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMontantSousLot.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLibelleSousLot.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GridSousLot As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridMontantSL As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewMontantSL As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl25 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbSousLot As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtMontantSousLot As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl26 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtLibelleSousLot As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BtAjoutMontant As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents SupprimerToolStripMenuItem As ToolStripMenuItem
End Class
