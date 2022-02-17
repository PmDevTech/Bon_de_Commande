<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AjoutSousCritereConsult
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
        Me.TxtSousCritere = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ChkBareme = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtNote = New DevExpress.XtraEditors.TextEdit()
        Me.ChkEtiquette = New DevExpress.XtraEditors.CheckEdit()
        Me.ChkNote = New DevExpress.XtraEditors.CheckEdit()
        Me.CmbCritere = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LblInfo = New DevExpress.XtraEditors.LabelControl()
        Me.BtQuitter = New DevExpress.XtraEditors.SimpleButton()
        Me.BtAjoutCritere = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.TxtSousCritere.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkBareme.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtNote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkEtiquette.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkNote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbCritere.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtSousCritere
        '
        Me.TxtSousCritere.Location = New System.Drawing.Point(12, 83)
        Me.TxtSousCritere.Name = "TxtSousCritere"
        Me.TxtSousCritere.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSousCritere.Properties.Appearance.Options.UseFont = True
        Me.TxtSousCritere.Size = New System.Drawing.Size(511, 77)
        Me.TxtSousCritere.TabIndex = 1
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(13, 24)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(33, 13)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "Critère"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(14, 67)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(57, 13)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Sous critère"
        '
        'ChkBareme
        '
        Me.ChkBareme.Enabled = False
        Me.ChkBareme.Location = New System.Drawing.Point(148, 167)
        Me.ChkBareme.Name = "ChkBareme"
        Me.ChkBareme.Properties.Caption = "Barème"
        Me.ChkBareme.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.ChkBareme.Properties.RadioGroupIndex = 1
        Me.ChkBareme.Size = New System.Drawing.Size(67, 19)
        Me.ChkBareme.TabIndex = 8
        Me.ChkBareme.TabStop = False
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(385, 168)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(23, 13)
        Me.LabelControl3.TabIndex = 9
        Me.LabelControl3.Text = "Note"
        '
        'TxtNote
        '
        Me.TxtNote.Location = New System.Drawing.Point(411, 164)
        Me.TxtNote.Name = "TxtNote"
        Me.TxtNote.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNote.Properties.Appearance.Options.UseFont = True
        Me.TxtNote.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.TxtNote.Properties.Mask.EditMask = "n"
        Me.TxtNote.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TxtNote.Size = New System.Drawing.Size(112, 22)
        Me.TxtNote.TabIndex = 10
        '
        'ChkEtiquette
        '
        Me.ChkEtiquette.Enabled = False
        Me.ChkEtiquette.Location = New System.Drawing.Point(71, 167)
        Me.ChkEtiquette.Name = "ChkEtiquette"
        Me.ChkEtiquette.Properties.Caption = "Etiquette"
        Me.ChkEtiquette.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.ChkEtiquette.Properties.RadioGroupIndex = 1
        Me.ChkEtiquette.Size = New System.Drawing.Size(71, 19)
        Me.ChkEtiquette.TabIndex = 11
        Me.ChkEtiquette.TabStop = False
        '
        'ChkNote
        '
        Me.ChkNote.EditValue = True
        Me.ChkNote.Enabled = False
        Me.ChkNote.Location = New System.Drawing.Point(9, 167)
        Me.ChkNote.Name = "ChkNote"
        Me.ChkNote.Properties.Caption = "Note"
        Me.ChkNote.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.ChkNote.Properties.RadioGroupIndex = 1
        Me.ChkNote.Size = New System.Drawing.Size(52, 19)
        Me.ChkNote.TabIndex = 12
        '
        'CmbCritere
        '
        Me.CmbCritere.Location = New System.Drawing.Point(11, 40)
        Me.CmbCritere.Name = "CmbCritere"
        Me.CmbCritere.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbCritere.Properties.Appearance.Options.UseFont = True
        Me.CmbCritere.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbCritere.Size = New System.Drawing.Size(512, 24)
        Me.CmbCritere.TabIndex = 13
        '
        'LblInfo
        '
        Me.LblInfo.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInfo.Appearance.ForeColor = System.Drawing.Color.Red
        Me.LblInfo.Location = New System.Drawing.Point(14, 193)
        Me.LblInfo.Name = "LblInfo"
        Me.LblInfo.Size = New System.Drawing.Size(122, 15)
        Me.LblInfo.TabIndex = 14
        Me.LblInfo.Text = "Sous critère d'évaluation"
        '
        'BtQuitter
        '
        Me.BtQuitter.Image = Global.ClearProject.My.Resources.Resources.Close_32x32
        Me.BtQuitter.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtQuitter.Location = New System.Drawing.Point(491, 3)
        Me.BtQuitter.Name = "BtQuitter"
        Me.BtQuitter.Size = New System.Drawing.Size(32, 32)
        Me.BtQuitter.TabIndex = 7
        Me.BtQuitter.Text = " "
        '
        'BtAjoutCritere
        '
        Me.BtAjoutCritere.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_32x32
        Me.BtAjoutCritere.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtAjoutCritere.Location = New System.Drawing.Point(454, 3)
        Me.BtAjoutCritere.Name = "BtAjoutCritere"
        Me.BtAjoutCritere.Size = New System.Drawing.Size(32, 32)
        Me.BtAjoutCritere.TabIndex = 6
        Me.BtAjoutCritere.Text = " "
        '
        'AjoutSousCritereConsult
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(538, 212)
        Me.ControlBox = False
        Me.Controls.Add(Me.LblInfo)
        Me.Controls.Add(Me.CmbCritere)
        Me.Controls.Add(Me.ChkNote)
        Me.Controls.Add(Me.ChkEtiquette)
        Me.Controls.Add(Me.TxtNote)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.ChkBareme)
        Me.Controls.Add(Me.BtQuitter)
        Me.Controls.Add(Me.BtAjoutCritere)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.TxtSousCritere)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "AjoutSousCritereConsult"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Nouveau sous critère"
        CType(Me.TxtSousCritere.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkBareme.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtNote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkEtiquette.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkNote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbCritere.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TxtSousCritere As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtQuitter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtAjoutCritere As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ChkBareme As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtNote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ChkEtiquette As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ChkNote As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents CmbCritere As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LblInfo As DevExpress.XtraEditors.LabelControl
End Class
