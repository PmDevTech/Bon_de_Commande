Imports DevExpress.XtraEditors

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ParametreTauxAppli
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
        Me.Label4 = New System.Windows.Forms.Label()
        Me.gcBailleur = New DevExpress.XtraEditors.GroupControl()
        Me.txtCode = New DevExpress.XtraEditors.TextEdit()
        Me.cmbConvention = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ComboBailleur = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbCategorie = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTaux = New DevExpress.XtraEditors.SpinEdit()
        Me.btEnregTaux = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.btnSupprimer = New DevExpress.XtraEditors.SimpleButton()
        Me.btnModifier = New DevExpress.XtraEditors.SimpleButton()
        Me.GridCategorie = New DevExpress.XtraGrid.GridControl()
        Me.ViewCategorie = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.gcBailleur, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcBailleur.SuspendLayout()
        CType(Me.txtCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbConvention.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ComboBailleur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCategorie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTaux.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GridCategorie, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewCategorie, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Bailleur"
        '
        'gcBailleur
        '
        Me.gcBailleur.Controls.Add(Me.txtCode)
        Me.gcBailleur.Controls.Add(Me.cmbConvention)
        Me.gcBailleur.Controls.Add(Me.Label6)
        Me.gcBailleur.Controls.Add(Me.ComboBailleur)
        Me.gcBailleur.Controls.Add(Me.Label5)
        Me.gcBailleur.Dock = System.Windows.Forms.DockStyle.Top
        Me.gcBailleur.Location = New System.Drawing.Point(0, 0)
        Me.gcBailleur.Name = "gcBailleur"
        Me.gcBailleur.Size = New System.Drawing.Size(536, 77)
        Me.gcBailleur.TabIndex = 33
        Me.gcBailleur.Text = "Bailleur / Convention"
        '
        'txtCode
        '
        Me.txtCode.Enabled = False
        Me.txtCode.Location = New System.Drawing.Point(427, 2)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(42, 20)
        Me.txtCode.TabIndex = 81
        Me.txtCode.Visible = False
        '
        'cmbConvention
        '
        Me.cmbConvention.Location = New System.Drawing.Point(275, 43)
        Me.cmbConvention.Name = "cmbConvention"
        Me.cmbConvention.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbConvention.Size = New System.Drawing.Size(243, 20)
        Me.cmbConvention.TabIndex = 42
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(272, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 13)
        Me.Label6.TabIndex = 43
        Me.Label6.Text = "Convention"
        '
        'ComboBailleur
        '
        Me.ComboBailleur.Location = New System.Drawing.Point(15, 43)
        Me.ComboBailleur.Name = "ComboBailleur"
        Me.ComboBailleur.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ComboBailleur.Size = New System.Drawing.Size(243, 20)
        Me.ComboBailleur.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 27)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 13)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "Bailleur"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(272, 31)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 13)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "Taux"
        '
        'cmbCategorie
        '
        Me.cmbCategorie.Location = New System.Drawing.Point(15, 47)
        Me.cmbCategorie.Name = "cmbCategorie"
        Me.cmbCategorie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCategorie.Size = New System.Drawing.Size(243, 20)
        Me.cmbCategorie.TabIndex = 44
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 13)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "Catégorie de dépense"
        '
        'txtTaux
        '
        Me.txtTaux.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtTaux.Location = New System.Drawing.Point(275, 47)
        Me.txtTaux.Name = "txtTaux"
        Me.txtTaux.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtTaux.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtTaux.Properties.Mask.EditMask = "P2"
        Me.txtTaux.Properties.MaxValue = New Decimal(New Integer() {100, 0, 0, 0})
        Me.txtTaux.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtTaux.Size = New System.Drawing.Size(243, 20)
        Me.txtTaux.TabIndex = 77
        '
        'btEnregTaux
        '
        Me.btEnregTaux.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.btEnregTaux.Location = New System.Drawing.Point(64, 88)
        Me.btEnregTaux.Name = "btEnregTaux"
        Me.btEnregTaux.Size = New System.Drawing.Size(123, 32)
        Me.btEnregTaux.TabIndex = 34
        Me.btEnregTaux.Text = "Enregistrer"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.btnSupprimer)
        Me.GroupControl1.Controls.Add(Me.btnModifier)
        Me.GroupControl1.Controls.Add(Me.cmbCategorie)
        Me.GroupControl1.Controls.Add(Me.btEnregTaux)
        Me.GroupControl1.Controls.Add(Me.Label3)
        Me.GroupControl1.Controls.Add(Me.txtTaux)
        Me.GroupControl1.Controls.Add(Me.Label2)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 77)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(536, 126)
        Me.GroupControl1.TabIndex = 78
        Me.GroupControl1.Text = "Categorie de dépense / taux applicable"
        '
        'btnSupprimer
        '
        Me.btnSupprimer.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.btnSupprimer.Location = New System.Drawing.Point(360, 88)
        Me.btnSupprimer.Name = "btnSupprimer"
        Me.btnSupprimer.Size = New System.Drawing.Size(123, 32)
        Me.btnSupprimer.TabIndex = 81
        Me.btnSupprimer.Text = "Supprimer"
        '
        'btnModifier
        '
        Me.btnModifier.Image = Global.ClearProject.My.Resources.Resources.Edit_16x16
        Me.btnModifier.Location = New System.Drawing.Point(211, 88)
        Me.btnModifier.Name = "btnModifier"
        Me.btnModifier.Size = New System.Drawing.Size(123, 32)
        Me.btnModifier.TabIndex = 80
        Me.btnModifier.Text = "Modifier"
        '
        'GridCategorie
        '
        Me.GridCategorie.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridCategorie.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridCategorie.Location = New System.Drawing.Point(0, 203)
        Me.GridCategorie.MainView = Me.ViewCategorie
        Me.GridCategorie.Name = "GridCategorie"
        Me.GridCategorie.Size = New System.Drawing.Size(536, 157)
        Me.GridCategorie.TabIndex = 79
        Me.GridCategorie.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewCategorie})
        '
        'ViewCategorie
        '
        Me.ViewCategorie.ActiveFilterEnabled = False
        Me.ViewCategorie.GridControl = Me.GridCategorie
        Me.ViewCategorie.Name = "ViewCategorie"
        Me.ViewCategorie.OptionsBehavior.Editable = False
        Me.ViewCategorie.OptionsBehavior.ReadOnly = True
        Me.ViewCategorie.OptionsCustomization.AllowColumnMoving = False
        Me.ViewCategorie.OptionsCustomization.AllowFilter = False
        Me.ViewCategorie.OptionsCustomization.AllowGroup = False
        Me.ViewCategorie.OptionsCustomization.AllowSort = False
        Me.ViewCategorie.OptionsFilter.AllowFilterEditor = False
        Me.ViewCategorie.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewCategorie.OptionsPrint.AutoWidth = False
        Me.ViewCategorie.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewCategorie.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewCategorie.OptionsView.ColumnAutoWidth = False
        Me.ViewCategorie.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewCategorie.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewCategorie.OptionsView.ShowGroupPanel = False
        Me.ViewCategorie.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewCategorie.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'ParametreTauxAppli
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(536, 360)
        Me.Controls.Add(Me.GridCategorie)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.gcBailleur)
        Me.Controls.Add(Me.Label4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ParametreTauxAppli"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Taux applicable"
        CType(Me.gcBailleur, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcBailleur.ResumeLayout(False)
        Me.gcBailleur.PerformLayout()
        CType(Me.txtCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbConvention.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ComboBailleur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCategorie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTaux.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.GridCategorie, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewCategorie, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label4 As Label
    Friend WithEvents gcBailleur As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ComboBailleur As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents Label5 As Label
    Friend WithEvents cmbCategorie As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btEnregTaux As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtTaux As SpinEdit
    Friend WithEvents cmbConvention As ComboBoxEdit
    Friend WithEvents Label6 As Label
    Friend WithEvents GroupControl1 As GroupControl
    Friend WithEvents GridCategorie As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewCategorie As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btnModifier As SimpleButton
    Friend WithEvents txtCode As TextEdit
    Friend WithEvents btnSupprimer As SimpleButton
End Class
