<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LiaisionEtapesPPM
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ListeEtapes = New System.Windows.Forms.DataGridView()
        Me.GetValeurMethode51 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Num = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Libelle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PnlNewEtape = New DevExpress.XtraEditors.PanelControl()
        Me.CmbTypeMarche = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.BtEnregistrer = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ListeEtapes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PnlNewEtape, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlNewEtape.SuspendLayout()
        CType(Me.CmbTypeMarche.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ListeEtapes
        '
        Me.ListeEtapes.AllowUserToAddRows = False
        Me.ListeEtapes.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ListeEtapes.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.ListeEtapes.BackgroundColor = System.Drawing.Color.White
        Me.ListeEtapes.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListeEtapes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.ListeEtapes.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GetValeurMethode51, Me.Num, Me.Libelle})
        Me.ListeEtapes.GridColor = System.Drawing.Color.White
        Me.ListeEtapes.Location = New System.Drawing.Point(0, 32)
        Me.ListeEtapes.MultiSelect = False
        Me.ListeEtapes.Name = "ListeEtapes"
        Me.ListeEtapes.RowHeadersWidth = 4
        Me.ListeEtapes.Size = New System.Drawing.Size(736, 357)
        Me.ListeEtapes.TabIndex = 5
        '
        'GetValeurMethode51
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.GetValeurMethode51.DefaultCellStyle = DataGridViewCellStyle2
        Me.GetValeurMethode51.Frozen = True
        Me.GetValeurMethode51.HeaderText = "RefEtape"
        Me.GetValeurMethode51.Name = "GetValeurMethode51"
        Me.GetValeurMethode51.ReadOnly = True
        Me.GetValeurMethode51.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GetValeurMethode51.Width = 77
        '
        'Num
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Num.DefaultCellStyle = DataGridViewCellStyle3
        Me.Num.HeaderText = "N°"
        Me.Num.Name = "Num"
        Me.Num.ReadOnly = True
        Me.Num.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Num.Width = 25
        '
        'Libelle
        '
        Me.Libelle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Libelle.HeaderText = "Libellé"
        Me.Libelle.Name = "Libelle"
        Me.Libelle.ReadOnly = True
        '
        'PnlNewEtape
        '
        Me.PnlNewEtape.Controls.Add(Me.CmbTypeMarche)
        Me.PnlNewEtape.Controls.Add(Me.LabelControl1)
        Me.PnlNewEtape.Controls.Add(Me.BtEnregistrer)
        Me.PnlNewEtape.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlNewEtape.Location = New System.Drawing.Point(0, 0)
        Me.PnlNewEtape.Name = "PnlNewEtape"
        Me.PnlNewEtape.Size = New System.Drawing.Size(736, 28)
        Me.PnlNewEtape.TabIndex = 6
        '
        'CmbTypeMarche
        '
        Me.CmbTypeMarche.Location = New System.Drawing.Point(103, 3)
        Me.CmbTypeMarche.Name = "CmbTypeMarche"
        Me.CmbTypeMarche.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbTypeMarche.Properties.Appearance.Options.UseFont = True
        Me.CmbTypeMarche.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbTypeMarche.Size = New System.Drawing.Size(178, 22)
        Me.CmbTypeMarche.TabIndex = 2
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.LabelControl1.Location = New System.Drawing.Point(2, 2)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(93, 24)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Type de marché"
        '
        'BtEnregistrer
        '
        Me.BtEnregistrer.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnregistrer.Appearance.Options.UseFont = True
        Me.BtEnregistrer.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtEnregistrer.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.BtEnregistrer.Location = New System.Drawing.Point(625, 2)
        Me.BtEnregistrer.Name = "BtEnregistrer"
        Me.BtEnregistrer.Size = New System.Drawing.Size(109, 24)
        Me.BtEnregistrer.TabIndex = 12
        Me.BtEnregistrer.Text = "Enregistrer"
        Me.BtEnregistrer.ToolTip = "Enregistrer"
        '
        'LiaisionEtapesPPM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(736, 390)
        Me.Controls.Add(Me.ListeEtapes)
        Me.Controls.Add(Me.PnlNewEtape)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LiaisionEtapesPPM"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Liaison des étapes de passation des marchés"
        CType(Me.ListeEtapes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PnlNewEtape, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlNewEtape.ResumeLayout(False)
        CType(Me.CmbTypeMarche.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ListeEtapes As System.Windows.Forms.DataGridView
    Friend WithEvents PnlNewEtape As DevExpress.XtraEditors.PanelControl
    Friend WithEvents CmbTypeMarche As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtEnregistrer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GetValeurMethode51 As DataGridViewTextBoxColumn
    Friend WithEvents Num As DataGridViewTextBoxColumn
    Friend WithEvents Libelle As DataGridViewTextBoxColumn
End Class
