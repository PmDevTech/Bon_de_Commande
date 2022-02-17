<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AjoutCritereConsult
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
        Me.TxtCritere = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtNote = New DevExpress.XtraEditors.TextEdit()
        Me.ChkEtiquette = New DevExpress.XtraEditors.CheckEdit()
        Me.LblObs = New DevExpress.XtraEditors.LabelControl()
        Me.BtQuitter = New DevExpress.XtraEditors.SimpleButton()
        Me.BtAjoutCritere = New DevExpress.XtraEditors.SimpleButton()
        Me.PersonnelCle = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.TxtCritere.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtNote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkEtiquette.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PersonnelCle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtCritere
        '
        Me.TxtCritere.Location = New System.Drawing.Point(14, 42)
        Me.TxtCritere.Name = "TxtCritere"
        Me.TxtCritere.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCritere.Properties.Appearance.Options.UseFont = True
        Me.TxtCritere.Size = New System.Drawing.Size(512, 63)
        Me.TxtCritere.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(16, 26)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(33, 13)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Critère"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(382, 113)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(29, 13)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Note*"
        '
        'TxtNote
        '
        Me.TxtNote.Enabled = False
        Me.TxtNote.Location = New System.Drawing.Point(414, 110)
        Me.TxtNote.Name = "TxtNote"
        Me.TxtNote.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNote.Properties.Appearance.Options.UseFont = True
        Me.TxtNote.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.TxtNote.Properties.Mask.EditMask = "n"
        Me.TxtNote.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TxtNote.Size = New System.Drawing.Size(112, 22)
        Me.TxtNote.TabIndex = 3
        '
        'ChkEtiquette
        '
        Me.ChkEtiquette.EditValue = True
        Me.ChkEtiquette.Location = New System.Drawing.Point(11, 108)
        Me.ChkEtiquette.Name = "ChkEtiquette"
        Me.ChkEtiquette.Properties.Caption = "Critère étiquette"
        Me.ChkEtiquette.Size = New System.Drawing.Size(145, 19)
        Me.ChkEtiquette.TabIndex = 6
        '
        'LblObs
        '
        Me.LblObs.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblObs.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LblObs.Location = New System.Drawing.Point(13, 134)
        Me.LblObs.Name = "LblObs"
        Me.LblObs.Size = New System.Drawing.Size(145, 15)
        Me.LblObs.TabIndex = 7
        Me.LblObs.Text = "* La note n'est pas obligatoire"
        '
        'BtQuitter
        '
        Me.BtQuitter.Image = Global.ClearProject.My.Resources.Resources.Close_32x32
        Me.BtQuitter.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtQuitter.Location = New System.Drawing.Point(494, 3)
        Me.BtQuitter.Name = "BtQuitter"
        Me.BtQuitter.Size = New System.Drawing.Size(32, 32)
        Me.BtQuitter.TabIndex = 5
        Me.BtQuitter.Text = " "
        '
        'BtAjoutCritere
        '
        Me.BtAjoutCritere.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_32x32
        Me.BtAjoutCritere.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtAjoutCritere.Location = New System.Drawing.Point(457, 3)
        Me.BtAjoutCritere.Name = "BtAjoutCritere"
        Me.BtAjoutCritere.Size = New System.Drawing.Size(32, 32)
        Me.BtAjoutCritere.TabIndex = 4
        Me.BtAjoutCritere.Text = " "
        '
        'PersonnelCle
        '
        Me.PersonnelCle.Location = New System.Drawing.Point(153, 110)
        Me.PersonnelCle.Name = "PersonnelCle"
        Me.PersonnelCle.Properties.Caption = "Critère de type personnel clé"
        Me.PersonnelCle.Size = New System.Drawing.Size(166, 19)
        Me.PersonnelCle.TabIndex = 8
        '
        'AjoutCritereConsult
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(538, 155)
        Me.ControlBox = False
        Me.Controls.Add(Me.PersonnelCle)
        Me.Controls.Add(Me.LblObs)
        Me.Controls.Add(Me.ChkEtiquette)
        Me.Controls.Add(Me.BtQuitter)
        Me.Controls.Add(Me.BtAjoutCritere)
        Me.Controls.Add(Me.TxtNote)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.TxtCritere)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "AjoutCritereConsult"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Nouveau critère"
        Me.TopMost = True
        CType(Me.TxtCritere.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtNote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkEtiquette.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PersonnelCle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TxtCritere As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtNote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BtAjoutCritere As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtQuitter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ChkEtiquette As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LblObs As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PersonnelCle As DevExpress.XtraEditors.CheckEdit
End Class
