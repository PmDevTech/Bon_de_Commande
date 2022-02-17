<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TypeTiers
    Inherits System.Windows.Forms.Form

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
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.dgPrets = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SupprimerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcnewGrade = New DevExpress.XtraEditors.GroupControl()
        Me.cmbCompte = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btRetour = New DevExpress.XtraEditors.SimpleButton()
        Me.btModifier = New DevExpress.XtraEditors.SimpleButton()
        Me.btEnregistrer = New DevExpress.XtraEditors.SimpleButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtLibelle = New DevExpress.XtraEditors.TextEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.dgPrets, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcnewGrade, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcnewGrade.SuspendLayout()
        CType(Me.cmbCompte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLibelle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.dgPrets)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(0, 135)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(344, 231)
        Me.GroupControl3.TabIndex = 80
        '
        'dgPrets
        '
        Me.dgPrets.ContextMenuStrip = Me.ContextMenuStrip1
        Me.dgPrets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgPrets.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgPrets.Location = New System.Drawing.Point(2, 21)
        Me.dgPrets.MainView = Me.GridView1
        Me.dgPrets.Name = "dgPrets"
        Me.dgPrets.Size = New System.Drawing.Size(340, 208)
        Me.dgPrets.TabIndex = 14
        Me.dgPrets.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SupprimerToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(130, 26)
        '
        'SupprimerToolStripMenuItem
        '
        Me.SupprimerToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.SupprimerToolStripMenuItem.Name = "SupprimerToolStripMenuItem"
        Me.SupprimerToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.SupprimerToolStripMenuItem.Text = "Supprimer"
        '
        'GridView1
        '
        Me.GridView1.ActiveFilterEnabled = False
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.GridControl = Me.dgPrets
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsCustomization.AllowColumnMoving = False
        Me.GridView1.OptionsCustomization.AllowFilter = False
        Me.GridView1.OptionsCustomization.AllowGroup = False
        Me.GridView1.OptionsCustomization.AllowSort = False
        Me.GridView1.OptionsFilter.AllowFilterEditor = False
        Me.GridView1.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.GridView1.OptionsPrint.AutoWidth = False
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GridView1.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'gcnewGrade
        '
        Me.gcnewGrade.Controls.Add(Me.cmbCompte)
        Me.gcnewGrade.Controls.Add(Me.btRetour)
        Me.gcnewGrade.Controls.Add(Me.btModifier)
        Me.gcnewGrade.Controls.Add(Me.btEnregistrer)
        Me.gcnewGrade.Controls.Add(Me.Label1)
        Me.gcnewGrade.Controls.Add(Me.txtLibelle)
        Me.gcnewGrade.Controls.Add(Me.Label2)
        Me.gcnewGrade.Dock = System.Windows.Forms.DockStyle.Top
        Me.gcnewGrade.Location = New System.Drawing.Point(0, 0)
        Me.gcnewGrade.Name = "gcnewGrade"
        Me.gcnewGrade.Size = New System.Drawing.Size(344, 135)
        Me.gcnewGrade.TabIndex = 79
        '
        'cmbCompte
        '
        Me.cmbCompte.Location = New System.Drawing.Point(52, 57)
        Me.cmbCompte.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.cmbCompte.Name = "cmbCompte"
        Me.cmbCompte.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCompte.Size = New System.Drawing.Size(284, 20)
        Me.cmbCompte.TabIndex = 2
        '
        'btRetour
        '
        Me.btRetour.Image = Global.ClearProject.My.Resources.Resources.Return_16x16
        Me.btRetour.Location = New System.Drawing.Point(234, 88)
        Me.btRetour.Margin = New System.Windows.Forms.Padding(2)
        Me.btRetour.Name = "btRetour"
        Me.btRetour.Size = New System.Drawing.Size(86, 28)
        Me.btRetour.TabIndex = 6
        Me.btRetour.Text = "Annuler"
        '
        'btModifier
        '
        Me.btModifier.Image = Global.ClearProject.My.Resources.Resources.Edit_16x16
        Me.btModifier.Location = New System.Drawing.Point(135, 88)
        Me.btModifier.Margin = New System.Windows.Forms.Padding(2)
        Me.btModifier.Name = "btModifier"
        Me.btModifier.Size = New System.Drawing.Size(86, 28)
        Me.btModifier.TabIndex = 5
        Me.btModifier.Text = "Modifier"
        '
        'btEnregistrer
        '
        Me.btEnregistrer.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.btEnregistrer.Location = New System.Drawing.Point(36, 88)
        Me.btEnregistrer.Margin = New System.Windows.Forms.Padding(2)
        Me.btEnregistrer.Name = "btEnregistrer"
        Me.btEnregistrer.Size = New System.Drawing.Size(86, 28)
        Me.btEnregistrer.TabIndex = 4
        Me.btEnregistrer.Text = "Enregistrer"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 99
        Me.Label1.Text = "Compte"
        '
        'txtLibelle
        '
        Me.txtLibelle.Location = New System.Drawing.Point(52, 27)
        Me.txtLibelle.Margin = New System.Windows.Forms.Padding(2)
        Me.txtLibelle.Name = "txtLibelle"
        Me.txtLibelle.Properties.Mask.EditMask = "n0"
        Me.txtLibelle.Properties.MaxLength = 35
        Me.txtLibelle.Size = New System.Drawing.Size(284, 20)
        Me.txtLibelle.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 99
        Me.Label2.Text = "Libellé"
        '
        'TypeTiers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(344, 366)
        Me.Controls.Add(Me.GroupControl3)
        Me.Controls.Add(Me.gcnewGrade)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TypeTiers"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Type des tiers"
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.dgPrets, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcnewGrade, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcnewGrade.ResumeLayout(False)
        Me.gcnewGrade.PerformLayout()
        CType(Me.cmbCompte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLibelle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dgPrets As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcnewGrade As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btModifier As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btEnregistrer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtLibelle As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents btRetour As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SupprimerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbCompte As DevExpress.XtraEditors.ComboBoxEdit
End Class
