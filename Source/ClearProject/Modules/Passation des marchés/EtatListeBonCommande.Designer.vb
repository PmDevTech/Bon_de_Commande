<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EtatListeBonCommande
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
        Me.DateDebut = New DevExpress.XtraEditors.DateEdit()
        Me.DateFin = New DevExpress.XtraEditors.DateEdit()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CmbStatut = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.BtnImprimer = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        CType(Me.DateDebut.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateDebut.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateFin.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbStatut.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateDebut
        '
        Me.DateDebut.EditValue = Nothing
        Me.DateDebut.Location = New System.Drawing.Point(95, 9)
        Me.DateDebut.Name = "DateDebut"
        Me.DateDebut.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateDebut.Properties.Mask.EditMask = ""
        Me.DateDebut.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.DateDebut.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.DateDebut.Size = New System.Drawing.Size(207, 20)
        Me.DateDebut.TabIndex = 1
        '
        'DateFin
        '
        Me.DateFin.EditValue = Nothing
        Me.DateFin.Location = New System.Drawing.Point(95, 45)
        Me.DateFin.Name = "DateFin"
        Me.DateFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateFin.Properties.Mask.EditMask = ""
        Me.DateFin.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.DateFin.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.DateFin.Size = New System.Drawing.Size(207, 20)
        Me.DateFin.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(21, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 117
        Me.Label3.Text = "Date début"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(37, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 118
        Me.Label1.Text = "Date fin"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(45, 85)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 119
        Me.Label2.Text = "Statut"
        '
        'CmbStatut
        '
        Me.CmbStatut.Location = New System.Drawing.Point(95, 82)
        Me.CmbStatut.Name = "CmbStatut"
        Me.CmbStatut.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbStatut.Size = New System.Drawing.Size(207, 20)
        Me.CmbStatut.TabIndex = 3
        '
        'BtnImprimer
        '
        Me.BtnImprimer.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnImprimer.Appearance.Options.UseFont = True
        Me.BtnImprimer.Image = Global.ClearProject.My.Resources.Resources.Group_Reports
        Me.BtnImprimer.Location = New System.Drawing.Point(98, 10)
        Me.BtnImprimer.Name = "BtnImprimer"
        Me.BtnImprimer.Size = New System.Drawing.Size(132, 35)
        Me.BtnImprimer.TabIndex = 4
        Me.BtnImprimer.Text = "Imprimer"
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.Label3)
        Me.PanelControl3.Controls.Add(Me.DateDebut)
        Me.PanelControl3.Controls.Add(Me.CmbStatut)
        Me.PanelControl3.Controls.Add(Me.Label2)
        Me.PanelControl3.Controls.Add(Me.Label1)
        Me.PanelControl3.Controls.Add(Me.DateFin)
        Me.PanelControl3.Location = New System.Drawing.Point(8, 7)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(333, 113)
        Me.PanelControl3.TabIndex = 1
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.BtnImprimer)
        Me.PanelControl1.Location = New System.Drawing.Point(8, 127)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(333, 55)
        Me.PanelControl1.TabIndex = 2
        '
        'EtatListeBonCommande
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(349, 190)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.PanelControl3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EtatListeBonCommande"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Récapitulatif des Bons de Commande"
        CType(Me.DateDebut.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateDebut.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateFin.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbStatut.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        Me.PanelControl3.PerformLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DateDebut As DevExpress.XtraEditors.DateEdit
    Friend WithEvents DateFin As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents CmbStatut As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents BtnImprimer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
End Class
