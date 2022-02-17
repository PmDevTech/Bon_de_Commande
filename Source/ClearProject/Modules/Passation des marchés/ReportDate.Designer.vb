<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ReportDate
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
        Me.HeureOuverture = New DevExpress.XtraEditors.TimeEdit()
        Me.NomJournal = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.DatePublication = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.DateOuverture = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.PanelChoixValeur, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelChoixValeur.SuspendLayout()
        CType(Me.HeureOuverture.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NomJournal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DatePublication.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DatePublication.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateOuverture.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateOuverture.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtEnregComm
        '
        Me.BtEnregComm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtEnregComm.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnregComm.Appearance.Options.UseFont = True
        Me.BtEnregComm.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.BtEnregComm.Location = New System.Drawing.Point(168, 113)
        Me.BtEnregComm.Name = "BtEnregComm"
        Me.BtEnregComm.Size = New System.Drawing.Size(100, 29)
        Me.BtEnregComm.TabIndex = 1
        Me.BtEnregComm.Text = "Enregistrer"
        '
        'PanelChoixValeur
        '
        Me.PanelChoixValeur.Controls.Add(Me.HeureOuverture)
        Me.PanelChoixValeur.Controls.Add(Me.NomJournal)
        Me.PanelChoixValeur.Controls.Add(Me.LabelControl14)
        Me.PanelChoixValeur.Controls.Add(Me.DatePublication)
        Me.PanelChoixValeur.Controls.Add(Me.LabelControl1)
        Me.PanelChoixValeur.Controls.Add(Me.DateOuverture)
        Me.PanelChoixValeur.Controls.Add(Me.LabelControl4)
        Me.PanelChoixValeur.Controls.Add(Me.BtEnregComm)
        Me.PanelChoixValeur.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelChoixValeur.Location = New System.Drawing.Point(0, 0)
        Me.PanelChoixValeur.Name = "PanelChoixValeur"
        Me.PanelChoixValeur.Size = New System.Drawing.Size(380, 147)
        Me.PanelChoixValeur.TabIndex = 2
        '
        'HeureOuverture
        '
        Me.HeureOuverture.EditValue = Nothing
        Me.HeureOuverture.Location = New System.Drawing.Point(303, 5)
        Me.HeureOuverture.Name = "HeureOuverture"
        Me.HeureOuverture.Properties.Appearance.Options.UseTextOptions = True
        Me.HeureOuverture.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.HeureOuverture.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.HeureOuverture.Properties.DisplayFormat.FormatString = "HH:mm:ss"
        Me.HeureOuverture.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.HeureOuverture.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText
        Me.HeureOuverture.Size = New System.Drawing.Size(65, 20)
        Me.HeureOuverture.TabIndex = 15
        '
        'NomJournal
        '
        Me.NomJournal.Location = New System.Drawing.Point(168, 78)
        Me.NomJournal.Name = "NomJournal"
        Me.NomJournal.Properties.Appearance.Options.UseTextOptions = True
        Me.NomJournal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.NomJournal.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.NomJournal.Size = New System.Drawing.Size(200, 20)
        Me.NomJournal.TabIndex = 26
        '
        'LabelControl14
        '
        Me.LabelControl14.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl14.LineVisible = True
        Me.LabelControl14.Location = New System.Drawing.Point(7, 81)
        Me.LabelControl14.Name = "LabelControl14"
        Me.LabelControl14.Size = New System.Drawing.Size(141, 13)
        Me.LabelControl14.TabIndex = 25
        Me.LabelControl14.Text = "Nom du journal de publication"
        '
        'DatePublication
        '
        Me.DatePublication.EditValue = Nothing
        Me.DatePublication.Location = New System.Drawing.Point(168, 41)
        Me.DatePublication.Name = "DatePublication"
        Me.DatePublication.Properties.Appearance.Options.UseTextOptions = True
        Me.DatePublication.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DatePublication.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DatePublication.Properties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None)
        Me.DatePublication.Properties.DisplayFormat.FormatString = "dd/MM/yy"
        Me.DatePublication.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.DatePublication.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.DatePublication.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.DatePublication.Size = New System.Drawing.Size(200, 20)
        Me.DatePublication.TabIndex = 24
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.LineVisible = True
        Me.LabelControl1.Location = New System.Drawing.Point(7, 44)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(92, 13)
        Me.LabelControl1.TabIndex = 23
        Me.LabelControl1.Text = "Date de publication"
        '
        'DateOuverture
        '
        Me.DateOuverture.EditValue = Nothing
        Me.DateOuverture.Location = New System.Drawing.Point(168, 5)
        Me.DateOuverture.Name = "DateOuverture"
        Me.DateOuverture.Properties.Appearance.Options.UseTextOptions = True
        Me.DateOuverture.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DateOuverture.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateOuverture.Properties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None)
        Me.DateOuverture.Properties.DisplayFormat.FormatString = "dd/MM/yy"
        Me.DateOuverture.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.DateOuverture.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.DateOuverture.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.DateOuverture.Size = New System.Drawing.Size(129, 20)
        Me.DateOuverture.TabIndex = 22
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.LineVisible = True
        Me.LabelControl4.Location = New System.Drawing.Point(7, 8)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(125, 13)
        Me.LabelControl4.TabIndex = 21
        Me.LabelControl4.Text = "Nouvelle date d'ouverture"
        '
        'ReportDate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(380, 146)
        Me.Controls.Add(Me.PanelChoixValeur)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ReportDate"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report de la date d'ouverture"
        CType(Me.PanelChoixValeur, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelChoixValeur.ResumeLayout(False)
        Me.PanelChoixValeur.PerformLayout()
        CType(Me.HeureOuverture.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NomJournal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DatePublication.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DatePublication.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateOuverture.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateOuverture.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtEnregComm As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelChoixValeur As DevExpress.XtraEditors.PanelControl
    Friend WithEvents DatePublication As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents DateOuverture As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents NomJournal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents HeureOuverture As DevExpress.XtraEditors.TimeEdit
End Class
