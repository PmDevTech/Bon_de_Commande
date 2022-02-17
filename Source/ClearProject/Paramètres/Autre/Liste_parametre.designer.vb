<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Liste_parametre
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
        Me.dglistparam = New System.Windows.Forms.DataGridView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AjouterUneRubriqueToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SuppressionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.dglistparam, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.dglistparam)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(412, 210)
        Me.PanelControl1.TabIndex = 0
        '
        'dglistparam
        '
        Me.dglistparam.AllowUserToAddRows = False
        Me.dglistparam.AllowUserToDeleteRows = False
        Me.dglistparam.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dglistparam.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dglistparam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dglistparam.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2})
        Me.dglistparam.ContextMenuStrip = Me.ContextMenuStrip1
        Me.dglistparam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dglistparam.Location = New System.Drawing.Point(2, 2)
        Me.dglistparam.Name = "dglistparam"
        Me.dglistparam.ReadOnly = True
        Me.dglistparam.RowHeadersWidth = 5
        Me.dglistparam.Size = New System.Drawing.Size(408, 206)
        Me.dglistparam.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AjouterUneRubriqueToolStripMenuItem, Me.SuppressionToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(188, 48)
        '
        'AjouterUneRubriqueToolStripMenuItem
        '
        Me.AjouterUneRubriqueToolStripMenuItem.Name = "AjouterUneRubriqueToolStripMenuItem"
        Me.AjouterUneRubriqueToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.AjouterUneRubriqueToolStripMenuItem.Text = "Ajouter une Rubrique"
        '
        'SuppressionToolStripMenuItem
        '
        Me.SuppressionToolStripMenuItem.Name = "SuppressionToolStripMenuItem"
        Me.SuppressionToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.SuppressionToolStripMenuItem.Text = "Suppression"
        '
        'Column1
        '
        Me.Column1.HeaderText = "Type"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "Libellé Paramètre"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 303
        '
        'Liste_parametre
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(412, 210)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Liste_parametre"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Paramètrage"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.dglistparam, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents dglistparam As System.Windows.Forms.DataGridView
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AjouterUneRubriqueToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SuppressionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
