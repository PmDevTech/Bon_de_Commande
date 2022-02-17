<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DiagChoixMarche
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.CbTypeMarche = New System.Windows.Forms.ComboBox()
        Me.BtQuitter = New System.Windows.Forms.Button()
        Me.BtAjoutMarche = New System.Windows.Forms.Button()
        Me.GridChoixMarche = New System.Windows.Forms.DataGridView()
        Me.Column3 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.GridChoixMarche, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CbTypeMarche
        '
        Me.CbTypeMarche.FormattingEnabled = True
        Me.CbTypeMarche.Items.AddRange(New Object() {"Travaux", "Fournitures"})
        Me.CbTypeMarche.Location = New System.Drawing.Point(180, 11)
        Me.CbTypeMarche.Margin = New System.Windows.Forms.Padding(5)
        Me.CbTypeMarche.Name = "CbTypeMarche"
        Me.CbTypeMarche.Size = New System.Drawing.Size(467, 31)
        Me.CbTypeMarche.TabIndex = 9
        '
        'BtQuitter
        '
        Me.BtQuitter.Location = New System.Drawing.Point(972, 4)
        Me.BtQuitter.Margin = New System.Windows.Forms.Padding(5)
        Me.BtQuitter.Name = "BtQuitter"
        Me.BtQuitter.Size = New System.Drawing.Size(113, 42)
        Me.BtQuitter.TabIndex = 7
        Me.BtQuitter.Text = "Quitter"
        Me.BtQuitter.UseVisualStyleBackColor = True
        '
        'BtAjoutMarche
        '
        Me.BtAjoutMarche.Location = New System.Drawing.Point(848, 4)
        Me.BtAjoutMarche.Margin = New System.Windows.Forms.Padding(5)
        Me.BtAjoutMarche.Name = "BtAjoutMarche"
        Me.BtAjoutMarche.Size = New System.Drawing.Size(113, 42)
        Me.BtAjoutMarche.TabIndex = 6
        Me.BtAjoutMarche.Text = "Ajouter"
        Me.BtAjoutMarche.UseVisualStyleBackColor = True
        '
        'GridChoixMarche
        '
        Me.GridChoixMarche.AllowUserToAddRows = False
        Me.GridChoixMarche.AllowUserToDeleteRows = False
        Me.GridChoixMarche.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.GridChoixMarche.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GridChoixMarche.BackgroundColor = System.Drawing.Color.White
        Me.GridChoixMarche.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GridChoixMarche.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.GridChoixMarche.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridChoixMarche.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column3, Me.Column1, Me.Column2})
        Me.GridChoixMarche.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GridChoixMarche.Location = New System.Drawing.Point(0, 56)
        Me.GridChoixMarche.Margin = New System.Windows.Forms.Padding(5)
        Me.GridChoixMarche.MultiSelect = False
        Me.GridChoixMarche.Name = "GridChoixMarche"
        Me.GridChoixMarche.RowHeadersVisible = False
        Me.GridChoixMarche.Size = New System.Drawing.Size(1178, 462)
        Me.GridChoixMarche.TabIndex = 5
        '
        'Column3
        '
        Me.Column3.Frozen = True
        Me.Column3.HeaderText = "*"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 30
        '
        'Column1
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column1.Frozen = True
        Me.Column1.HeaderText = "Numero"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 60
        '
        'Column2
        '
        Me.Column2.HeaderText = "Description"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 700
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 14)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(163, 24)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Type de marchés"
        '
        'DiagChoixMarche
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1178, 518)
        Me.Controls.Add(Me.CbTypeMarche)
        Me.Controls.Add(Me.BtQuitter)
        Me.Controls.Add(Me.BtAjoutMarche)
        Me.Controls.Add(Me.GridChoixMarche)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DiagChoixMarche"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DiagChoixMarche"
        CType(Me.GridChoixMarche, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CbTypeMarche As System.Windows.Forms.ComboBox
    Friend WithEvents BtQuitter As System.Windows.Forms.Button
    Friend WithEvents BtAjoutMarche As System.Windows.Forms.Button
    Friend WithEvents GridChoixMarche As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
