<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ModifLigneDQE
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.MontantTotal = New DevExpress.XtraEditors.TextEdit()
        Me.PrixUnitaire = New DevExpress.XtraEditors.TextEdit()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Designation = New DevExpress.XtraEditors.TextEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btOK = New DevExpress.XtraEditors.SimpleButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Unites = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.NumQteBien = New DevExpress.XtraEditors.SpinEdit()
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.MontantTotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PrixUnitaire.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Designation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Unites.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumQteBien.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MontantTotal
        '
        Me.MontantTotal.Location = New System.Drawing.Point(95, 31)
        Me.MontantTotal.Name = "MontantTotal"
        Me.MontantTotal.Properties.Appearance.Options.UseTextOptions = True
        Me.MontantTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.MontantTotal.Properties.MaxLength = 160
        Me.MontantTotal.Properties.ReadOnly = True
        Me.MontantTotal.Size = New System.Drawing.Size(257, 20)
        Me.MontantTotal.TabIndex = 15
        '
        'PrixUnitaire
        '
        Me.PrixUnitaire.Location = New System.Drawing.Point(95, 5)
        Me.PrixUnitaire.Name = "PrixUnitaire"
        Me.PrixUnitaire.Properties.Appearance.Options.UseTextOptions = True
        Me.PrixUnitaire.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.PrixUnitaire.Properties.Mask.EditMask = "n0"
        Me.PrixUnitaire.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.PrixUnitaire.Properties.MaxLength = 160
        Me.PrixUnitaire.Size = New System.Drawing.Size(257, 20)
        Me.PrixUnitaire.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 33)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Montant total"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Prix Unitaires  *"
        '
        'Designation
        '
        Me.Designation.Location = New System.Drawing.Point(94, 14)
        Me.Designation.Name = "Designation"
        Me.Designation.Properties.MaxLength = 50
        Me.Designation.Size = New System.Drawing.Size(257, 20)
        Me.Designation.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Quantité *"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(195, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Unité *"
        '
        'btOK
        '
        Me.btOK.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btOK.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btOK.Appearance.Options.UseFont = True
        Me.btOK.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.btOK.Location = New System.Drawing.Point(140, 74)
        Me.btOK.Name = "btOK"
        Me.btOK.Size = New System.Drawing.Size(117, 31)
        Me.btOK.TabIndex = 20
        Me.btOK.Text = "Enregistrer"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Désignation *"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(355, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(36, 13)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = " FCFA"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(355, 34)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(36, 13)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = " FCFA"
        '
        'Unites
        '
        Me.Unites.Location = New System.Drawing.Point(242, 40)
        Me.Unites.Name = "Unites"
        Me.Unites.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Unites.Properties.MaxLength = 255
        Me.Unites.Size = New System.Drawing.Size(109, 20)
        Me.Unites.TabIndex = 5
        '
        'NumQteBien
        '
        Me.NumQteBien.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumQteBien.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumQteBien.Location = New System.Drawing.Point(94, 40)
        Me.NumQteBien.Name = "NumQteBien"
        Me.NumQteBien.Properties.Appearance.Options.UseTextOptions = True
        Me.NumQteBien.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.NumQteBien.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.NumQteBien.Properties.Mask.EditMask = "f0"
        Me.NumQteBien.Properties.MaxValue = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.NumQteBien.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumQteBien.Size = New System.Drawing.Size(95, 20)
        Me.NumQteBien.TabIndex = 23
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PrixUnitaire)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.MontantTotal)
        Me.Panel1.Location = New System.Drawing.Point(2, 65)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(394, 56)
        Me.Panel1.TabIndex = 24
        Me.Panel1.Visible = False
        '
        'ModifLigneDQE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(397, 112)
        Me.Controls.Add(Me.btOK)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.NumQteBien)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Designation)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Unites)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ModifLigneDQE"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Modification DQE"
        CType(Me.MontantTotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PrixUnitaire.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Designation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Unites.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumQteBien.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label4 As Label
    Friend WithEvents Designation As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents MontantTotal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PrixUnitaire As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Unites As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents NumQteBien As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents Panel1 As Panel
End Class
