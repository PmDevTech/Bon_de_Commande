<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Etat_parametres
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
        Me.btimprim = New DevExpress.XtraEditors.SimpleButton()
        Me.comb1 = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.comb2 = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.comb1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comb2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btimprim
        '
        Me.btimprim.Location = New System.Drawing.Point(174, 269)
        Me.btimprim.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.btimprim.Name = "btimprim"
        Me.btimprim.Size = New System.Drawing.Size(165, 40)
        Me.btimprim.TabIndex = 5
        Me.btimprim.Text = "Imprimer"
        '
        'comb1
        '
        Me.comb1.Location = New System.Drawing.Point(174, 53)
        Me.comb1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.comb1.Name = "comb1"
        Me.comb1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.comb1.Size = New System.Drawing.Size(274, 30)
        Me.comb1.TabIndex = 7
        '
        'comb2
        '
        Me.comb2.Location = New System.Drawing.Point(174, 123)
        Me.comb2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.comb2.Name = "comb2"
        Me.comb2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.comb2.Size = New System.Drawing.Size(274, 30)
        Me.comb2.TabIndex = 8
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(29, 56)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(86, 23)
        Me.LabelControl1.TabIndex = 9
        Me.LabelControl1.Text = "du journal"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(29, 126)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(85, 23)
        Me.LabelControl2.TabIndex = 9
        Me.LabelControl2.Text = "au journal"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 204)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(472, 24)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "------------------------------------------------------------------"
        '
        'Etat_parametres
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(514, 338)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.comb2)
        Me.Controls.Add(Me.comb1)
        Me.Controls.Add(Me.btimprim)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Etat_parametres"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Liste des journaux"
        CType(Me.comb1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comb2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btimprim As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents comb1 As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents comb2 As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
