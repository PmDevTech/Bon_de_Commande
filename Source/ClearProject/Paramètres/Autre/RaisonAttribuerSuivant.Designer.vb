<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RaisonAttribuerSuivant
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
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtRaisonChange = New DevExpress.XtraEditors.MemoEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.RefSoumisFavoris = New DevExpress.XtraEditors.TextEdit()
        Me.TxtSoumisAttrib = New DevExpress.XtraEditors.TextEdit()
        Me.GridNvFavoris = New DevExpress.XtraGrid.GridControl()
        Me.ViewNvFavoris = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TxtRaisonChoix = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.BtValider = New DevExpress.XtraEditors.SimpleButton()
        Me.BtAnnuler = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtSoumDisq = New DevExpress.XtraEditors.TextEdit()
        CType(Me.TxtRaisonChange.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.RefSoumisFavoris.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSoumisAttrib.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridNvFavoris, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewNvFavoris, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtRaisonChoix.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSoumDisq.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(12, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(368, 18)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Spécifiez la raison de la disqualification du soumissionaire"
        '
        'TxtRaisonChange
        '
        Me.TxtRaisonChange.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtRaisonChange.EditValue = ""
        Me.TxtRaisonChange.Location = New System.Drawing.Point(12, 33)
        Me.TxtRaisonChange.Name = "TxtRaisonChange"
        Me.TxtRaisonChange.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRaisonChange.Properties.Appearance.Options.UseFont = True
        Me.TxtRaisonChange.Properties.MaxLength = 500
        Me.TxtRaisonChange.Size = New System.Drawing.Size(702, 82)
        Me.TxtRaisonChange.TabIndex = 1
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GroupControl1.Controls.Add(Me.RefSoumisFavoris)
        Me.GroupControl1.Controls.Add(Me.TxtSoumisAttrib)
        Me.GroupControl1.Controls.Add(Me.GridNvFavoris)
        Me.GroupControl1.Location = New System.Drawing.Point(12, 124)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(702, 108)
        Me.GroupControl1.TabIndex = 2
        Me.GroupControl1.Text = "Sélectionnez le nouveau favoris"
        '
        'RefSoumisFavoris
        '
        Me.RefSoumisFavoris.Location = New System.Drawing.Point(36, 3)
        Me.RefSoumisFavoris.Name = "RefSoumisFavoris"
        Me.RefSoumisFavoris.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RefSoumisFavoris.Properties.Appearance.Options.UseFont = True
        Me.RefSoumisFavoris.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RefSoumisFavoris.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.RefSoumisFavoris.Properties.ReadOnly = True
        Me.RefSoumisFavoris.Properties.UseParentBackground = True
        Me.RefSoumisFavoris.Size = New System.Drawing.Size(26, 20)
        Me.RefSoumisFavoris.TabIndex = 18
        Me.RefSoumisFavoris.Visible = False
        '
        'TxtSoumisAttrib
        '
        Me.TxtSoumisAttrib.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtSoumisAttrib.Location = New System.Drawing.Point(242, 3)
        Me.TxtSoumisAttrib.Name = "TxtSoumisAttrib"
        Me.TxtSoumisAttrib.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSoumisAttrib.Properties.Appearance.Options.UseFont = True
        Me.TxtSoumisAttrib.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.TxtSoumisAttrib.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtSoumisAttrib.Properties.ReadOnly = True
        Me.TxtSoumisAttrib.Properties.UseParentBackground = True
        Me.TxtSoumisAttrib.Size = New System.Drawing.Size(457, 20)
        Me.TxtSoumisAttrib.TabIndex = 13
        '
        'GridNvFavoris
        '
        Me.GridNvFavoris.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridNvFavoris.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridNvFavoris.Location = New System.Drawing.Point(2, 25)
        Me.GridNvFavoris.MainView = Me.ViewNvFavoris
        Me.GridNvFavoris.Name = "GridNvFavoris"
        Me.GridNvFavoris.Size = New System.Drawing.Size(698, 81)
        Me.GridNvFavoris.TabIndex = 3
        Me.GridNvFavoris.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewNvFavoris})
        '
        'ViewNvFavoris
        '
        Me.ViewNvFavoris.ActiveFilterEnabled = False
        Me.ViewNvFavoris.GridControl = Me.GridNvFavoris
        Me.ViewNvFavoris.Name = "ViewNvFavoris"
        Me.ViewNvFavoris.OptionsBehavior.Editable = False
        Me.ViewNvFavoris.OptionsBehavior.ReadOnly = True
        Me.ViewNvFavoris.OptionsCustomization.AllowColumnMoving = False
        Me.ViewNvFavoris.OptionsCustomization.AllowFilter = False
        Me.ViewNvFavoris.OptionsCustomization.AllowGroup = False
        Me.ViewNvFavoris.OptionsCustomization.AllowSort = False
        Me.ViewNvFavoris.OptionsFilter.AllowFilterEditor = False
        Me.ViewNvFavoris.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewNvFavoris.OptionsPrint.AutoWidth = False
        Me.ViewNvFavoris.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewNvFavoris.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewNvFavoris.OptionsView.ColumnAutoWidth = False
        Me.ViewNvFavoris.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewNvFavoris.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewNvFavoris.OptionsView.ShowGroupPanel = False
        Me.ViewNvFavoris.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewNvFavoris.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'TxtRaisonChoix
        '
        Me.TxtRaisonChoix.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtRaisonChoix.EditValue = ""
        Me.TxtRaisonChoix.Location = New System.Drawing.Point(12, 260)
        Me.TxtRaisonChoix.Name = "TxtRaisonChoix"
        Me.TxtRaisonChoix.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRaisonChoix.Properties.Appearance.Options.UseFont = True
        Me.TxtRaisonChoix.Properties.MaxLength = 500
        Me.TxtRaisonChoix.Size = New System.Drawing.Size(702, 82)
        Me.TxtRaisonChoix.TabIndex = 4
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(12, 239)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(207, 18)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Spécifiez la raison de votre choix"
        '
        'BtValider
        '
        Me.BtValider.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtValider.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtValider.Appearance.Options.UseFont = True
        Me.BtValider.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_32x32
        Me.BtValider.Location = New System.Drawing.Point(503, 354)
        Me.BtValider.Name = "BtValider"
        Me.BtValider.Size = New System.Drawing.Size(211, 37)
        Me.BtValider.TabIndex = 5
        Me.BtValider.Text = "VALIDER LE CHOIX"
        '
        'BtAnnuler
        '
        Me.BtAnnuler.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtAnnuler.Appearance.Options.UseFont = True
        Me.BtAnnuler.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_32
        Me.BtAnnuler.Location = New System.Drawing.Point(12, 354)
        Me.BtAnnuler.Name = "BtAnnuler"
        Me.BtAnnuler.Size = New System.Drawing.Size(37, 37)
        Me.BtAnnuler.TabIndex = 8
        '
        'TxtSoumDisq
        '
        Me.TxtSoumDisq.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtSoumDisq.Location = New System.Drawing.Point(394, 10)
        Me.TxtSoumDisq.Name = "TxtSoumDisq"
        Me.TxtSoumDisq.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSoumDisq.Properties.Appearance.Options.UseFont = True
        Me.TxtSoumDisq.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.TxtSoumDisq.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtSoumDisq.Properties.ReadOnly = True
        Me.TxtSoumDisq.Properties.UseParentBackground = True
        Me.TxtSoumDisq.Size = New System.Drawing.Size(320, 20)
        Me.TxtSoumDisq.TabIndex = 14
        '
        'RaisonAttribuerSuivant
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(726, 401)
        Me.ControlBox = False
        Me.Controls.Add(Me.TxtSoumDisq)
        Me.Controls.Add(Me.BtAnnuler)
        Me.Controls.Add(Me.BtValider)
        Me.Controls.Add(Me.TxtRaisonChoix)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.TxtRaisonChange)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RaisonAttribuerSuivant"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Raison de la disqualification"
        CType(Me.TxtRaisonChange.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.RefSoumisFavoris.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSoumisAttrib.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridNvFavoris, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewNvFavoris, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtRaisonChoix.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSoumDisq.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtRaisonChange As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridNvFavoris As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewNvFavoris As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TxtRaisonChoix As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtValider As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtAnnuler As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtSoumisAttrib As DevExpress.XtraEditors.TextEdit
    Friend WithEvents RefSoumisFavoris As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtSoumDisq As DevExpress.XtraEditors.TextEdit
End Class
