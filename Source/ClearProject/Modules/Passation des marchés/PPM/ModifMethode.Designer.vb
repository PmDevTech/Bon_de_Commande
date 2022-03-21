<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ModifMethode
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
        Me.BtEnregComm = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelChoixValeur = New DevExpress.XtraEditors.PanelControl()
        Me.cmbMethode = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.PanelChoixValeur, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelChoixValeur.SuspendLayout()
        CType(Me.cmbMethode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtEnregComm
        '
        Me.BtEnregComm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtEnregComm.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnregComm.Appearance.Options.UseFont = True
        Me.BtEnregComm.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.BtEnregComm.Location = New System.Drawing.Point(165, 77)
        Me.BtEnregComm.Name = "BtEnregComm"
        Me.BtEnregComm.Size = New System.Drawing.Size(112, 29)
        Me.BtEnregComm.TabIndex = 1
        Me.BtEnregComm.Text = "Enregistrer"
        '
        'PanelChoixValeur
        '
        Me.PanelChoixValeur.Controls.Add(Me.cmbMethode)
        Me.PanelChoixValeur.Controls.Add(Me.LabelControl14)
        Me.PanelChoixValeur.Controls.Add(Me.BtEnregComm)
        Me.PanelChoixValeur.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelChoixValeur.Location = New System.Drawing.Point(0, 0)
        Me.PanelChoixValeur.Name = "PanelChoixValeur"
        Me.PanelChoixValeur.Size = New System.Drawing.Size(380, 127)
        Me.PanelChoixValeur.TabIndex = 2
        '
        'cmbMethode
        '
        Me.cmbMethode.Location = New System.Drawing.Point(165, 41)
        Me.cmbMethode.Name = "cmbMethode"
        Me.cmbMethode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.OK)})
        Me.cmbMethode.Properties.ValidateOnEnterKey = True
        Me.cmbMethode.Size = New System.Drawing.Size(203, 20)
        Me.cmbMethode.TabIndex = 26
        '
        'LabelControl14
        '
        Me.LabelControl14.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl14.LineVisible = True
        Me.LabelControl14.Location = New System.Drawing.Point(7, 44)
        Me.LabelControl14.Name = "LabelControl14"
        Me.LabelControl14.Size = New System.Drawing.Size(152, 13)
        Me.LabelControl14.TabIndex = 25
        Me.LabelControl14.Text = "Choisir la méthode de passation"
        '
        'ModifMethode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(380, 128)
        Me.Controls.Add(Me.PanelChoixValeur)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ModifMethode"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Modifer la méthode de passation de marché"
        CType(Me.PanelChoixValeur, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelChoixValeur.ResumeLayout(False)
        Me.PanelChoixValeur.PerformLayout()
        CType(Me.cmbMethode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtEnregComm As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelChoixValeur As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbMethode As DevExpress.XtraEditors.ComboBoxEdit
End Class
