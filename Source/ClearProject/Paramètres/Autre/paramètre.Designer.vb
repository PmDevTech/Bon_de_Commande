<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class paramètre
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
        Me.SimpleButton4 = New DevExpress.XtraEditors.SimpleButton()
        Me.TextEdit3 = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.SimpleButton5 = New DevExpress.XtraEditors.SimpleButton()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TextEdit2 = New DevExpress.XtraEditors.TextEdit()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.SimpleButton3 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton2 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.TextEdit1 = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.TextEdit3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SimpleButton4
        '
        Me.SimpleButton4.Image = Global.ClearProject.My.Resources.Resources.Edit_16x16
        Me.SimpleButton4.Location = New System.Drawing.Point(225, 175)
        Me.SimpleButton4.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.SimpleButton4.Name = "SimpleButton4"
        Me.SimpleButton4.Size = New System.Drawing.Size(143, 52)
        Me.SimpleButton4.TabIndex = 9
        Me.SimpleButton4.Text = "Modifier"
        '
        'TextEdit3
        '
        Me.TextEdit3.Location = New System.Drawing.Point(138, 287)
        Me.TextEdit3.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.TextEdit3.Name = "TextEdit3"
        Me.TextEdit3.Size = New System.Drawing.Size(253, 30)
        Me.TextEdit3.TabIndex = 8
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.GroupControl2)
        Me.GroupControl1.Controls.Add(Me.PanelControl1)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(782, 765)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "GroupControl1"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.SimpleButton5)
        Me.GroupControl2.Controls.Add(Me.DataGridView1)
        Me.GroupControl2.Controls.Add(Me.TextEdit2)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(2, 288)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(778, 475)
        Me.GroupControl2.TabIndex = 1
        Me.GroupControl2.Text = "Liste"
        '
        'SimpleButton5
        '
        Me.SimpleButton5.Image = Global.ClearProject.My.Resources.Resources.Preview_16x16
        Me.SimpleButton5.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.SimpleButton5.Location = New System.Drawing.Point(13, 76)
        Me.SimpleButton5.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.SimpleButton5.Name = "SimpleButton5"
        Me.SimpleButton5.Size = New System.Drawing.Size(35, 37)
        Me.SimpleButton5.TabIndex = 26
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridView1.ColumnHeadersHeight = 30
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column2, Me.Column1})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 134)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersWidth = 5
        Me.DataGridView1.Size = New System.Drawing.Size(755, 328)
        Me.DataGridView1.TabIndex = 6
        '
        'Column2
        '
        Me.Column2.HeaderText = "id"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Visible = False
        '
        'Column1
        '
        Me.Column1.HeaderText = "Libellé"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 440
        '
        'TextEdit2
        '
        Me.TextEdit2.Location = New System.Drawing.Point(47, 76)
        Me.TextEdit2.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.TextEdit2.Name = "TextEdit2"
        Me.TextEdit2.Size = New System.Drawing.Size(718, 30)
        Me.TextEdit2.TabIndex = 5
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.SimpleButton4)
        Me.PanelControl1.Controls.Add(Me.TextEdit3)
        Me.PanelControl1.Controls.Add(Me.SimpleButton3)
        Me.PanelControl1.Controls.Add(Me.SimpleButton2)
        Me.PanelControl1.Controls.Add(Me.SimpleButton1)
        Me.PanelControl1.Controls.Add(Me.TextEdit1)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(2, 31)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(778, 257)
        Me.PanelControl1.TabIndex = 0
        '
        'SimpleButton3
        '
        Me.SimpleButton3.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_161
        Me.SimpleButton3.Location = New System.Drawing.Point(538, 175)
        Me.SimpleButton3.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.SimpleButton3.Name = "SimpleButton3"
        Me.SimpleButton3.Size = New System.Drawing.Size(143, 52)
        Me.SimpleButton3.TabIndex = 4
        Me.SimpleButton3.Text = "Annuler"
        '
        'SimpleButton2
        '
        Me.SimpleButton2.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.SimpleButton2.Location = New System.Drawing.Point(382, 175)
        Me.SimpleButton2.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.SimpleButton2.Name = "SimpleButton2"
        Me.SimpleButton2.Size = New System.Drawing.Size(143, 52)
        Me.SimpleButton2.TabIndex = 3
        Me.SimpleButton2.Text = "Supprimer"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.SimpleButton1.Location = New System.Drawing.Point(68, 175)
        Me.SimpleButton1.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(143, 52)
        Me.SimpleButton1.TabIndex = 2
        Me.SimpleButton1.Text = "Enregistrer"
        '
        'TextEdit1
        '
        Me.TextEdit1.Location = New System.Drawing.Point(35, 96)
        Me.TextEdit1.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.TextEdit1.Name = "TextEdit1"
        Me.TextEdit1.Size = New System.Drawing.Size(688, 30)
        Me.TextEdit1.TabIndex = 1
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 10.25!)
        Me.LabelControl1.Location = New System.Drawing.Point(40, 52)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(67, 27)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Libellé"
        '
        'paramètre
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(782, 765)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "paramètre"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "paramètre"
        CType(Me.TextEdit3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SimpleButton4 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TextEdit3 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TextEdit2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents SimpleButton3 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TextEdit1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SimpleButton5 As DevExpress.XtraEditors.SimpleButton
End Class
