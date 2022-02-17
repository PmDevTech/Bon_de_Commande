<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SplashScreen2
    Inherits DevExpress.XtraSplashScreen.SplashScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pictureEdit2 = New DevExpress.XtraEditors.PictureEdit()
        Me.labelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LblChargement = New DevExpress.XtraEditors.LabelControl()
        CType(Me.pictureEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pictureEdit2
        '
        Me.pictureEdit2.Cursor = System.Windows.Forms.Cursors.Default
        Me.pictureEdit2.EditValue = Global.ClearProject.My.Resources.Resources.espace_globe_87
        Me.pictureEdit2.Location = New System.Drawing.Point(-5, -4)
        Me.pictureEdit2.Name = "pictureEdit2"
        Me.pictureEdit2.Properties.AllowFocused = False
        Me.pictureEdit2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.pictureEdit2.Properties.Appearance.Options.UseBackColor = True
        Me.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pictureEdit2.Properties.ShowMenu = False
        Me.pictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.pictureEdit2.Size = New System.Drawing.Size(111, 94)
        Me.pictureEdit2.TabIndex = 14
        '
        'labelControl2
        '
        Me.labelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelControl2.Location = New System.Drawing.Point(102, 28)
        Me.labelControl2.Name = "labelControl2"
        Me.labelControl2.Size = New System.Drawing.Size(163, 23)
        Me.labelControl2.TabIndex = 12
        Me.labelControl2.Text = "Veuillez patienter..."
        '
        'LblChargement
        '
        Me.LblChargement.Appearance.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblChargement.Location = New System.Drawing.Point(105, 51)
        Me.LblChargement.Name = "LblChargement"
        Me.LblChargement.Size = New System.Drawing.Size(3, 14)
        Me.LblChargement.TabIndex = 15
        Me.LblChargement.Text = " "
        '
        'SplashScreen2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(367, 88)
        Me.Controls.Add(Me.LblChargement)
        Me.Controls.Add(Me.labelControl2)
        Me.Controls.Add(Me.pictureEdit2)
        Me.Name = "SplashScreen2"
        Me.SplashImage = Global.ClearProject.My.Resources.Resources.espace_globe_87
        Me.Text = "Form1"
        CType(Me.pictureEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents pictureEdit2 As DevExpress.XtraEditors.PictureEdit
    Private WithEvents labelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblChargement As DevExpress.XtraEditors.LabelControl
End Class
