<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Etat_engPai
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
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.btimprim = New DevExpress.XtraEditors.SimpleButton()
        Me.dtf = New DevExpress.XtraEditors.DateEdit()
        Me.dtd = New DevExpress.XtraEditors.DateEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.dtf.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtd.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(22, 75)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(38, 13)
        Me.LabelControl2.TabIndex = 16
        Me.LabelControl2.Text = "Date fin"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(22, 36)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(54, 13)
        Me.LabelControl1.TabIndex = 15
        Me.LabelControl1.Text = "Date début"
        '
        'btimprim
        '
        Me.btimprim.Location = New System.Drawing.Point(105, 155)
        Me.btimprim.Name = "btimprim"
        Me.btimprim.Size = New System.Drawing.Size(99, 23)
        Me.btimprim.TabIndex = 12
        Me.btimprim.Text = "Imprimer"
        '
        'dtf
        '
        Me.dtf.EditValue = Nothing
        Me.dtf.Location = New System.Drawing.Point(94, 71)
        Me.dtf.Name = "dtf"
        Me.dtf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtf.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.dtf.Size = New System.Drawing.Size(169, 20)
        Me.dtf.TabIndex = 23
        '
        'dtd
        '
        Me.dtd.EditValue = Nothing
        Me.dtd.Location = New System.Drawing.Point(94, 34)
        Me.dtd.Name = "dtd"
        Me.dtd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtd.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.dtd.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.dtd.Size = New System.Drawing.Size(169, 20)
        Me.dtd.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 117)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(279, 13)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "--------------------------------------------------------------------"
        '
        'Etat_engPai
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(301, 193)
        Me.Controls.Add(Me.dtf)
        Me.Controls.Add(Me.dtd)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.btimprim)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(526, 405)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(508, 380)
        Me.Name = "Etat_engPai"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Situation des factures payées par marché"
        CType(Me.dtf.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtd.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btimprim As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents dtf As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
