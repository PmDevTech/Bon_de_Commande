<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ModePlanMarche
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
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.BtEnrg = New DevExpress.XtraEditors.SimpleButton()
        Me.rdPPSD = New DevExpress.XtraEditors.CheckEdit()
        Me.rdGenere = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbModePlan = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.rdPPSD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdGenere.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbModePlan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Trebuchet MS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.PanelControl2)
        Me.GroupControl1.Controls.Add(Me.rdPPSD)
        Me.GroupControl1.Controls.Add(Me.rdGenere)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.cmbModePlan)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(431, 131)
        Me.GroupControl1.TabIndex = 15
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.BtEnrg)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl2.Location = New System.Drawing.Point(2, 93)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(427, 36)
        Me.PanelControl2.TabIndex = 16
        '
        'BtEnrg
        '
        Me.BtEnrg.Appearance.Font = New System.Drawing.Font("Trebuchet MS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnrg.Appearance.Options.UseFont = True
        Me.BtEnrg.Image = Global.ClearProject.My.Resources.Resources.disque_editer_fichier_enregistrez_icone_4226_16
        Me.BtEnrg.Location = New System.Drawing.Point(122, 5)
        Me.BtEnrg.Name = "BtEnrg"
        Me.BtEnrg.Size = New System.Drawing.Size(153, 26)
        Me.BtEnrg.TabIndex = 10
        Me.BtEnrg.Text = "Enregistrer"
        '
        'rdPPSD
        '
        Me.rdPPSD.Location = New System.Drawing.Point(223, 33)
        Me.rdPPSD.Name = "rdPPSD"
        Me.rdPPSD.Properties.Caption = "Saisir le plan"
        Me.rdPPSD.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.rdPPSD.Properties.RadioGroupIndex = 0
        Me.rdPPSD.Size = New System.Drawing.Size(149, 19)
        Me.rdPPSD.TabIndex = 12
        Me.rdPPSD.TabStop = False
        '
        'rdGenere
        '
        Me.rdGenere.EditValue = True
        Me.rdGenere.Location = New System.Drawing.Point(93, 33)
        Me.rdGenere.Name = "rdGenere"
        Me.rdGenere.Properties.Caption = "Générer le plan"
        Me.rdGenere.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.rdGenere.Properties.RadioGroupIndex = 0
        Me.rdGenere.Size = New System.Drawing.Size(124, 19)
        Me.rdGenere.TabIndex = 11
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(12, 64)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(68, 16)
        Me.LabelControl1.TabIndex = 3
        Me.LabelControl1.Text = "Généré  par"
        '
        'cmbModePlan
        '
        Me.cmbModePlan.Location = New System.Drawing.Point(95, 63)
        Me.cmbModePlan.Name = "cmbModePlan"
        Me.cmbModePlan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbModePlan.Properties.Items.AddRange(New Object() {"Tous les bailleurs", "Bailleur"})
        Me.cmbModePlan.Size = New System.Drawing.Size(313, 20)
        Me.cmbModePlan.TabIndex = 2
        '
        'ModePlanMarche
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(431, 131)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ModePlanMarche"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mode d'élaboration du PPM"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.rdPPSD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdGenere.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbModePlan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtEnrg As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbModePlan As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents rdPPSD As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents rdGenere As DevExpress.XtraEditors.CheckEdit
End Class
