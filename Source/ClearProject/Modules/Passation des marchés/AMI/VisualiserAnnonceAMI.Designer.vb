<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AnnonceAMI
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
        Me.GroupDossier = New DevExpress.XtraEditors.GroupControl()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.Bar3 = New DevExpress.XtraBars.Bar()
        CType(Me.GroupDossier, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupDossier.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupDossier
        '
        Me.GroupDossier.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupDossier.Appearance.Options.UseFont = True
        Me.GroupDossier.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupDossier.AppearanceCaption.Options.UseFont = True
        Me.GroupDossier.Controls.Add(Me.WebBrowser1)
        Me.GroupDossier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupDossier.Location = New System.Drawing.Point(0, 0)
        Me.GroupDossier.Name = "GroupDossier"
        Me.GroupDossier.Size = New System.Drawing.Size(394, 201)
        Me.GroupDossier.TabIndex = 13
        Me.GroupDossier.Text = "Annonce"
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser1.Location = New System.Drawing.Point(2, 23)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(390, 176)
        Me.WebBrowser1.TabIndex = 0
        '
        'Bar3
        '
        Me.Bar3.BarName = "Status bar"
        Me.Bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.Bar3.DockCol = 0
        Me.Bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.Bar3.OptionsBar.AllowQuickCustomization = False
        Me.Bar3.OptionsBar.DrawDragBorder = False
        Me.Bar3.OptionsBar.UseWholeRow = True
        Me.Bar3.Text = "Status bar"
        '
        'AnnonceAMI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 201)
        Me.Controls.Add(Me.GroupDossier)
        Me.Name = "AnnonceAMI"
        Me.Text = "Annonce de l'AMI"
        CType(Me.GroupDossier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupDossier.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupDossier As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Bar3 As DevExpress.XtraBars.Bar
    Friend WithEvents WebBrowser1 As WebBrowser
End Class
