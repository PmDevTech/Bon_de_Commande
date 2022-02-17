<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Devise
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
        Me.BtAjout = New DevExpress.XtraEditors.SimpleButton()
        Me.BtModif = New DevExpress.XtraEditors.SimpleButton()
        Me.BtSupprimer = New DevExpress.XtraEditors.SimpleButton()
        Me.BtRetour = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnEnregistrer = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtTaux = New System.Windows.Forms.TextBox()
        Me.Txtlibelle = New System.Windows.Forms.TextBox()
        Me.TxtAbrege = New System.Windows.Forms.TextBox()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtAjout
        '
        Me.BtAjout.Image = Global.ClearProject.My.Resources.Resources.ajouter
        Me.BtAjout.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtAjout.Location = New System.Drawing.Point(625, 4)
        Me.BtAjout.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.BtAjout.Name = "BtAjout"
        Me.BtAjout.Size = New System.Drawing.Size(65, 69)
        Me.BtAjout.TabIndex = 9
        Me.BtAjout.ToolTip = "Nouveau"
        '
        'BtModif
        '
        Me.BtModif.Image = Global.ClearProject.My.Resources.Resources.Edit_32x32
        Me.BtModif.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtModif.Location = New System.Drawing.Point(695, 4)
        Me.BtModif.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.BtModif.Name = "BtModif"
        Me.BtModif.Size = New System.Drawing.Size(65, 69)
        Me.BtModif.TabIndex = 8
        Me.BtModif.ToolTip = "Modifier"
        '
        'BtSupprimer
        '
        Me.BtSupprimer.Image = Global.ClearProject.My.Resources.Resources.supprimer
        Me.BtSupprimer.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtSupprimer.Location = New System.Drawing.Point(765, 4)
        Me.BtSupprimer.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.BtSupprimer.Name = "BtSupprimer"
        Me.BtSupprimer.Size = New System.Drawing.Size(65, 69)
        Me.BtSupprimer.TabIndex = 7
        Me.BtSupprimer.ToolTip = "Supprimer"
        '
        'BtRetour
        '
        Me.BtRetour.Enabled = False
        Me.BtRetour.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_32
        Me.BtRetour.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtRetour.Location = New System.Drawing.Point(835, 4)
        Me.BtRetour.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.BtRetour.Name = "BtRetour"
        Me.BtRetour.Size = New System.Drawing.Size(65, 69)
        Me.BtRetour.TabIndex = 6
        Me.BtRetour.ToolTip = "Retour"
        '
        'BtnEnregistrer
        '
        Me.BtnEnregistrer.Enabled = False
        Me.BtnEnregistrer.Image = Global.ClearProject.My.Resources.Resources.enregistrer1
        Me.BtnEnregistrer.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtnEnregistrer.Location = New System.Drawing.Point(905, 4)
        Me.BtnEnregistrer.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.BtnEnregistrer.Name = "BtnEnregistrer"
        Me.BtnEnregistrer.Size = New System.Drawing.Size(65, 69)
        Me.BtnEnregistrer.TabIndex = 5
        Me.BtnEnregistrer.ToolTip = "Enregistrer"
        '
        'TxtTaux
        '
        Me.TxtTaux.Enabled = False
        Me.TxtTaux.Location = New System.Drawing.Point(754, 39)
        Me.TxtTaux.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.TxtTaux.MaxLength = 9
        Me.TxtTaux.Name = "TxtTaux"
        Me.TxtTaux.Size = New System.Drawing.Size(212, 30)
        Me.TxtTaux.TabIndex = 12
        Me.TxtTaux.Text = "Taux"
        Me.TxtTaux.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Txtlibelle
        '
        Me.Txtlibelle.Enabled = False
        Me.Txtlibelle.Location = New System.Drawing.Point(185, 39)
        Me.Txtlibelle.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Txtlibelle.MaxLength = 50
        Me.Txtlibelle.Name = "Txtlibelle"
        Me.Txtlibelle.Size = New System.Drawing.Size(566, 30)
        Me.Txtlibelle.TabIndex = 11
        Me.Txtlibelle.Text = "Nom"
        '
        'TxtAbrege
        '
        Me.TxtAbrege.Enabled = False
        Me.TxtAbrege.Location = New System.Drawing.Point(7, 39)
        Me.TxtAbrege.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.TxtAbrege.MaxLength = 10
        Me.TxtAbrege.Name = "TxtAbrege"
        Me.TxtAbrege.Size = New System.Drawing.Size(164, 30)
        Me.TxtAbrege.TabIndex = 10
        Me.TxtAbrege.Text = "Code"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.TxtAbrege)
        Me.GroupControl1.Controls.Add(Me.TxtTaux)
        Me.GroupControl1.Controls.Add(Me.Txtlibelle)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl1.Location = New System.Drawing.Point(0, 76)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(973, 85)
        Me.GroupControl1.TabIndex = 13
        Me.GroupControl1.Text = "Nouvelle devise"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.ListView1)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl2.Location = New System.Drawing.Point(0, 161)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(973, 334)
        Me.GroupControl2.TabIndex = 14
        Me.GroupControl2.Text = "Devises enregistrées"
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(2, 31)
        Me.ListView1.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(969, 301)
        Me.ListView1.TabIndex = 1
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Code"
        Me.ColumnHeader1.Width = 102
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Nom"
        Me.ColumnHeader2.Width = 348
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Taux"
        Me.ColumnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader3.Width = 102
        '
        'Devise
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(973, 495)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.BtAjout)
        Me.Controls.Add(Me.BtModif)
        Me.Controls.Add(Me.BtSupprimer)
        Me.Controls.Add(Me.BtRetour)
        Me.Controls.Add(Me.BtnEnregistrer)
        Me.Controls.Add(Me.GroupControl2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Devise"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Devise"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtAjout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtModif As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtSupprimer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtRetour As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnEnregistrer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtTaux As System.Windows.Forms.TextBox
    Friend WithEvents Txtlibelle As System.Windows.Forms.TextBox
    Friend WithEvents TxtAbrege As System.Windows.Forms.TextBox
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
End Class
