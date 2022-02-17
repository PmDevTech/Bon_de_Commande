<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Exercice_Par_Defaut
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
        Me.btnDefinir = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbExercice = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.cmbExercice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnDefinir
        '
        Me.btnDefinir.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDefinir.Appearance.Options.UseFont = True
        Me.btnDefinir.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.btnDefinir.Location = New System.Drawing.Point(82, 34)
        Me.btnDefinir.Name = "btnDefinir"
        Me.btnDefinir.Size = New System.Drawing.Size(118, 31)
        Me.btnDefinir.TabIndex = 19
        Me.btnDefinir.Text = "Définir"
        Me.btnDefinir.ToolTip = "Cliquer pour Frais Non Justifiés"
        Me.btnDefinir.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(10, 9)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(47, 16)
        Me.LabelControl1.TabIndex = 21
        Me.LabelControl1.Text = "Exercice"
        '
        'cmbExercice
        '
        Me.cmbExercice.Location = New System.Drawing.Point(64, 8)
        Me.cmbExercice.Name = "cmbExercice"
        Me.cmbExercice.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbExercice.Size = New System.Drawing.Size(208, 20)
        Me.cmbExercice.TabIndex = 20
        '
        'Exercice_Par_Defaut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(279, 71)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.cmbExercice)
        Me.Controls.Add(Me.btnDefinir)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Exercice_Par_Defaut"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Définir l'exercice par défaut"
        CType(Me.cmbExercice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnDefinir As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbExercice As DevExpress.XtraEditors.ComboBoxEdit
End Class
