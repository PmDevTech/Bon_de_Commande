<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Imprim_ferier
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
        Me.dtf = New DevExpress.XtraEditors.DateEdit()
        Me.btimprim = New DevExpress.XtraEditors.SimpleButton()
        Me.df = New DevExpress.XtraEditors.LabelControl()
        Me.db0 = New DevExpress.XtraEditors.LabelControl()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CmbAnneeFeriee = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.dtf.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbAnneeFeriee.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtf
        '
        Me.dtf.EditValue = Nothing
        Me.dtf.Location = New System.Drawing.Point(55, 91)
        Me.dtf.Name = "dtf"
        Me.dtf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtf.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.dtf.Size = New System.Drawing.Size(169, 20)
        Me.dtf.TabIndex = 26
        Me.dtf.Visible = False
        '
        'btimprim
        '
        Me.btimprim.Image = Global.ClearProject.My.Resources.Resources.Group_Reports
        Me.btimprim.Location = New System.Drawing.Point(76, 49)
        Me.btimprim.Name = "btimprim"
        Me.btimprim.Size = New System.Drawing.Size(99, 23)
        Me.btimprim.TabIndex = 24
        Me.btimprim.Text = "Imprimer"
        '
        'df
        '
        Me.df.Location = New System.Drawing.Point(9, 94)
        Me.df.Name = "df"
        Me.df.Size = New System.Drawing.Size(40, 13)
        Me.df.TabIndex = 23
        Me.df.Text = "Date Fin"
        Me.df.Visible = False
        '
        'db0
        '
        Me.db0.Location = New System.Drawing.Point(16, 14)
        Me.db0.Name = "db0"
        Me.db0.Size = New System.Drawing.Size(31, 13)
        Me.db0.TabIndex = 22
        Me.db0.Text = "Année"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 114)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(279, 13)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "--------------------------------------------------------------------"
        Me.Label1.Visible = False
        '
        'CmbAnneeFeriee
        '
        Me.CmbAnneeFeriee.Location = New System.Drawing.Point(55, 12)
        Me.CmbAnneeFeriee.Name = "CmbAnneeFeriee"
        Me.CmbAnneeFeriee.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbAnneeFeriee.Size = New System.Drawing.Size(181, 20)
        Me.CmbAnneeFeriee.TabIndex = 28
        '
        'Imprim_ferier
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(255, 86)
        Me.Controls.Add(Me.CmbAnneeFeriee)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtf)
        Me.Controls.Add(Me.btimprim)
        Me.Controls.Add(Me.df)
        Me.Controls.Add(Me.db0)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Imprim_ferier"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Impression"
        CType(Me.dtf.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbAnneeFeriee.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtf As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btimprim As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents df As DevExpress.XtraEditors.LabelControl
    Friend WithEvents db0 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CmbAnneeFeriee As DevExpress.XtraEditors.ComboBoxEdit
End Class
