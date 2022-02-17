<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SaisieMethodes
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SaisieMethodes))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtAjouter = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.BtEnregistrer = New System.Windows.Forms.ToolStripButton()
        Me.BtSupprimer = New System.Windows.Forms.ToolStripButton()
        Me.BtReload = New System.Windows.Forms.ToolStripButton()
        Me.ListeMethode = New System.Windows.Forms.DataGridView()
        Me.Code = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Libelle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
        CType(Me.ListeMethode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtAjouter
        '
        Me.BtAjouter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BtAjouter.Image = CType(resources.GetObject("BtAjouter.Image"), System.Drawing.Image)
        Me.BtAjouter.Name = "BtAjouter"
        Me.BtAjouter.RightToLeftAutoMirrorImage = True
        Me.BtAjouter.Size = New System.Drawing.Size(23, 22)
        Me.BtAjouter.Text = "Ajouter"
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.AddNewItem = Me.BtAjouter
        Me.BindingNavigator1.CountItem = Nothing
        Me.BindingNavigator1.DeleteItem = Nothing
        Me.BindingNavigator1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtAjouter, Me.BindingNavigatorSeparator2, Me.BtEnregistrer, Me.BtSupprimer, Me.BtReload})
        Me.BindingNavigator1.Location = New System.Drawing.Point(0, 0)
        Me.BindingNavigator1.MoveFirstItem = Nothing
        Me.BindingNavigator1.MoveLastItem = Nothing
        Me.BindingNavigator1.MoveNextItem = Nothing
        Me.BindingNavigator1.MovePreviousItem = Nothing
        Me.BindingNavigator1.Name = "BindingNavigator1"
        Me.BindingNavigator1.PositionItem = Nothing
        Me.BindingNavigator1.Size = New System.Drawing.Size(565, 25)
        Me.BindingNavigator1.TabIndex = 4
        Me.BindingNavigator1.Text = "BindingNavigator1"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'BtEnregistrer
        '
        Me.BtEnregistrer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BtEnregistrer.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.BtEnregistrer.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtEnregistrer.Name = "BtEnregistrer"
        Me.BtEnregistrer.Size = New System.Drawing.Size(23, 22)
        Me.BtEnregistrer.Text = "Enregistrer"
        '
        'BtSupprimer
        '
        Me.BtSupprimer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BtSupprimer.Image = Global.ClearProject.My.Resources.Resources.Delete_16x16
        Me.BtSupprimer.ImageTransparentColor = System.Drawing.Color.Linen
        Me.BtSupprimer.Name = "BtSupprimer"
        Me.BtSupprimer.Size = New System.Drawing.Size(23, 22)
        Me.BtSupprimer.Text = "Supprimer"
        '
        'BtReload
        '
        Me.BtReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BtReload.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_161
        Me.BtReload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtReload.Name = "BtReload"
        Me.BtReload.Size = New System.Drawing.Size(23, 22)
        Me.BtReload.Text = "Revenir"
        '
        'ListeMethode
        '
        Me.ListeMethode.AllowUserToAddRows = False
        Me.ListeMethode.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ListeMethode.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.ListeMethode.BackgroundColor = System.Drawing.Color.White
        Me.ListeMethode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListeMethode.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.ListeMethode.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Code, Me.Libelle})
        Me.ListeMethode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListeMethode.GridColor = System.Drawing.Color.White
        Me.ListeMethode.Location = New System.Drawing.Point(0, 25)
        Me.ListeMethode.MultiSelect = False
        Me.ListeMethode.Name = "ListeMethode"
        Me.ListeMethode.RowHeadersWidth = 4
        Me.ListeMethode.Size = New System.Drawing.Size(565, 293)
        Me.ListeMethode.TabIndex = 5
        '
        'Code
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Code.DefaultCellStyle = DataGridViewCellStyle2
        Me.Code.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.Code.DropDownWidth = 100
        Me.Code.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Code.Frozen = True
        Me.Code.HeaderText = "Code"
        Me.Code.Items.AddRange(New Object() {"AON", "AOI", "SFQC", "SFQ", "SD", "SQC", "SCBD", "SMC", "3CV", "ED", "CF", "QC", "PLC", "PSL", "PSO"})
        Me.Code.MaxDropDownItems = 99
        Me.Code.Name = "Code"
        Me.Code.ReadOnly = True
        Me.Code.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Code.Width = 80
        '
        'Libelle
        '
        Me.Libelle.HeaderText = "Libellé"
        Me.Libelle.Name = "Libelle"
        Me.Libelle.ReadOnly = True
        Me.Libelle.Width = 250
        '
        'SaisieMethodes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(565, 318)
        Me.Controls.Add(Me.ListeMethode)
        Me.Controls.Add(Me.BindingNavigator1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SaisieMethodes"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Saisie Methodes PDM"
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()
        CType(Me.ListeMethode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    'Friend WithEvents CsprCollapisblePanel1 As csprCollapsiblePanel.csprCollapisblePanel
    Friend WithEvents BindingNavigator1 As System.Windows.Forms.BindingNavigator
    Friend WithEvents BtAjouter As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BtEnregistrer As System.Windows.Forms.ToolStripButton
    Friend WithEvents ListeMethode As System.Windows.Forms.DataGridView
    Friend WithEvents BtSupprimer As ToolStripButton
    Friend WithEvents BtReload As ToolStripButton
    Friend WithEvents Code As DataGridViewComboBoxColumn
    Friend WithEvents Libelle As DataGridViewTextBoxColumn
End Class
