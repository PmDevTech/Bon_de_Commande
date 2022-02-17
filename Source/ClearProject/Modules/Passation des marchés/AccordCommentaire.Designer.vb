<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AccordCommentaire
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.TxtCommentaire = New DevExpress.XtraEditors.MemoEdit()
        Me.ChkCasSimilaire = New DevExpress.XtraEditors.CheckEdit()
        Me.BtEnregComm = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelChoixValeur = New DevExpress.XtraEditors.PanelControl()
        Me.RdValMin = New DevExpress.XtraEditors.CheckEdit()
        Me.RdValMax = New DevExpress.XtraEditors.CheckEdit()
        Me.TxtValMax = New DevExpress.XtraEditors.TextEdit()
        Me.TxtValMin = New DevExpress.XtraEditors.TextEdit()
        Me.TxtValMoy = New DevExpress.XtraEditors.TextEdit()
        Me.RdValMoy = New DevExpress.XtraEditors.CheckEdit()
        Me.ChkRubrique = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.TxtCommentaire.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkCasSimilaire.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelChoixValeur, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelChoixValeur.SuspendLayout()
        CType(Me.RdValMin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RdValMax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtValMax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtValMin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtValMoy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RdValMoy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkRubrique.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.ChkRubrique)
        Me.PanelControl1.Controls.Add(Me.BtEnregComm)
        Me.PanelControl1.Controls.Add(Me.ChkCasSimilaire)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 107)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(721, 39)
        Me.PanelControl1.TabIndex = 0
        '
        'TxtCommentaire
        '
        Me.TxtCommentaire.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtCommentaire.EditValue = ""
        Me.TxtCommentaire.Location = New System.Drawing.Point(0, 35)
        Me.TxtCommentaire.Name = "TxtCommentaire"
        Me.TxtCommentaire.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCommentaire.Properties.Appearance.Options.UseFont = True
        Me.TxtCommentaire.Size = New System.Drawing.Size(721, 72)
        Me.TxtCommentaire.TabIndex = 1
        '
        'ChkCasSimilaire
        '
        Me.ChkCasSimilaire.Location = New System.Drawing.Point(5, 8)
        Me.ChkCasSimilaire.Name = "ChkCasSimilaire"
        Me.ChkCasSimilaire.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkCasSimilaire.Properties.Appearance.Options.UseFont = True
        Me.ChkCasSimilaire.Properties.Caption = "Appliquer à tous les cas similaires"
        Me.ChkCasSimilaire.Size = New System.Drawing.Size(217, 21)
        Me.ChkCasSimilaire.TabIndex = 0
        '
        'BtEnregComm
        '
        Me.BtEnregComm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtEnregComm.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnregComm.Appearance.Options.UseFont = True
        Me.BtEnregComm.Location = New System.Drawing.Point(612, 7)
        Me.BtEnregComm.Name = "BtEnregComm"
        Me.BtEnregComm.Size = New System.Drawing.Size(100, 24)
        Me.BtEnregComm.TabIndex = 1
        Me.BtEnregComm.Text = "Enregistrer"
        '
        'PanelChoixValeur
        '
        Me.PanelChoixValeur.Controls.Add(Me.TxtValMoy)
        Me.PanelChoixValeur.Controls.Add(Me.RdValMoy)
        Me.PanelChoixValeur.Controls.Add(Me.TxtValMin)
        Me.PanelChoixValeur.Controls.Add(Me.TxtValMax)
        Me.PanelChoixValeur.Controls.Add(Me.RdValMin)
        Me.PanelChoixValeur.Controls.Add(Me.RdValMax)
        Me.PanelChoixValeur.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelChoixValeur.Location = New System.Drawing.Point(0, 0)
        Me.PanelChoixValeur.Name = "PanelChoixValeur"
        Me.PanelChoixValeur.Size = New System.Drawing.Size(721, 35)
        Me.PanelChoixValeur.TabIndex = 2
        '
        'RdValMin
        '
        Me.RdValMin.Location = New System.Drawing.Point(5, 8)
        Me.RdValMin.Name = "RdValMin"
        Me.RdValMin.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RdValMin.Properties.Appearance.Options.UseFont = True
        Me.RdValMin.Properties.Caption = "Valeur Min."
        Me.RdValMin.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.RdValMin.Properties.RadioGroupIndex = 1
        Me.RdValMin.Size = New System.Drawing.Size(97, 21)
        Me.RdValMin.TabIndex = 1
        '
        'RdValMax
        '
        Me.RdValMax.Location = New System.Drawing.Point(253, 8)
        Me.RdValMax.Name = "RdValMax"
        Me.RdValMax.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RdValMax.Properties.Appearance.Options.UseFont = True
        Me.RdValMax.Properties.Caption = "Valeur Max."
        Me.RdValMax.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.RdValMax.Properties.RadioGroupIndex = 1
        Me.RdValMax.Size = New System.Drawing.Size(97, 21)
        Me.RdValMax.TabIndex = 2
        Me.RdValMax.TabStop = False
        '
        'TxtValMax
        '
        Me.TxtValMax.Location = New System.Drawing.Point(347, 7)
        Me.TxtValMax.Name = "TxtValMax"
        Me.TxtValMax.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtValMax.Properties.Appearance.Options.UseFont = True
        Me.TxtValMax.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtValMax.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.TxtValMax.Properties.ReadOnly = True
        Me.TxtValMax.Size = New System.Drawing.Size(120, 22)
        Me.TxtValMax.TabIndex = 3
        '
        'TxtValMin
        '
        Me.TxtValMin.EditValue = ""
        Me.TxtValMin.Location = New System.Drawing.Point(96, 7)
        Me.TxtValMin.Name = "TxtValMin"
        Me.TxtValMin.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtValMin.Properties.Appearance.Options.UseFont = True
        Me.TxtValMin.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtValMin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.TxtValMin.Properties.ReadOnly = True
        Me.TxtValMin.Size = New System.Drawing.Size(120, 22)
        Me.TxtValMin.TabIndex = 4
        '
        'TxtValMoy
        '
        Me.TxtValMoy.Location = New System.Drawing.Point(597, 7)
        Me.TxtValMoy.Name = "TxtValMoy"
        Me.TxtValMoy.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtValMoy.Properties.Appearance.Options.UseFont = True
        Me.TxtValMoy.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtValMoy.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.TxtValMoy.Properties.ReadOnly = True
        Me.TxtValMoy.Size = New System.Drawing.Size(120, 22)
        Me.TxtValMoy.TabIndex = 6
        '
        'RdValMoy
        '
        Me.RdValMoy.Location = New System.Drawing.Point(503, 8)
        Me.RdValMoy.Name = "RdValMoy"
        Me.RdValMoy.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RdValMoy.Properties.Appearance.Options.UseFont = True
        Me.RdValMoy.Properties.Caption = "Valeur Moy."
        Me.RdValMoy.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.RdValMoy.Properties.RadioGroupIndex = 1
        Me.RdValMoy.Size = New System.Drawing.Size(97, 21)
        Me.RdValMoy.TabIndex = 5
        Me.RdValMoy.TabStop = False
        '
        'ChkRubrique
        '
        Me.ChkRubrique.Location = New System.Drawing.Point(241, 8)
        Me.ChkRubrique.Name = "ChkRubrique"
        Me.ChkRubrique.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkRubrique.Properties.Appearance.Options.UseFont = True
        Me.ChkRubrique.Properties.Caption = "Appliquer à toute la rubrique"
        Me.ChkRubrique.Size = New System.Drawing.Size(197, 21)
        Me.ChkRubrique.TabIndex = 2
        '
        'AccordCommentaire
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(721, 146)
        Me.Controls.Add(Me.TxtCommentaire)
        Me.Controls.Add(Me.PanelChoixValeur)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AccordCommentaire"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Commentaire"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.TxtCommentaire.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkCasSimilaire.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelChoixValeur, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelChoixValeur.ResumeLayout(False)
        CType(Me.RdValMin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RdValMax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtValMax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtValMin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtValMoy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RdValMoy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkRubrique.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents TxtCommentaire As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents ChkCasSimilaire As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents BtEnregComm As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelChoixValeur As DevExpress.XtraEditors.PanelControl
    Friend WithEvents TxtValMin As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtValMax As DevExpress.XtraEditors.TextEdit
    Friend WithEvents RdValMax As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents RdValMin As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents TxtValMoy As DevExpress.XtraEditors.TextEdit
    Friend WithEvents RdValMoy As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ChkRubrique As DevExpress.XtraEditors.CheckEdit
End Class
