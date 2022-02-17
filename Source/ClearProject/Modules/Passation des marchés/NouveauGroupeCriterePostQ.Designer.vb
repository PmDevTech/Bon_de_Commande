<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NouveauGroupeCriterePostQ
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
        Me.TxtGroupe = New DevExpress.XtraEditors.MemoEdit()
        Me.BtEnrgGroupe = New DevExpress.XtraEditors.SimpleButton()
        Me.BtQuitter = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.TxtGroupe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtGroupe
        '
        Me.TxtGroupe.Dock = System.Windows.Forms.DockStyle.Top
        Me.TxtGroupe.Location = New System.Drawing.Point(0, 0)
        Me.TxtGroupe.Name = "TxtGroupe"
        Me.TxtGroupe.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGroupe.Properties.Appearance.Options.UseFont = True
        Me.TxtGroupe.Properties.MaxLength = 500
        Me.TxtGroupe.Size = New System.Drawing.Size(517, 77)
        Me.TxtGroupe.TabIndex = 0
        '
        'BtEnrgGroupe
        '
        Me.BtEnrgGroupe.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnrgGroupe.Appearance.Options.UseFont = True
        Me.BtEnrgGroupe.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_32x32
        Me.BtEnrgGroupe.Location = New System.Drawing.Point(395, 78)
        Me.BtEnrgGroupe.Name = "BtEnrgGroupe"
        Me.BtEnrgGroupe.Size = New System.Drawing.Size(122, 34)
        Me.BtEnrgGroupe.TabIndex = 1
        Me.BtEnrgGroupe.Text = "Enregistrer"
        '
        'BtQuitter
        '
        Me.BtQuitter.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtQuitter.Appearance.Options.UseFont = True
        Me.BtQuitter.Image = Global.ClearProject.My.Resources.Resources.Close_32x32
        Me.BtQuitter.Location = New System.Drawing.Point(0, 78)
        Me.BtQuitter.Name = "BtQuitter"
        Me.BtQuitter.Size = New System.Drawing.Size(122, 34)
        Me.BtQuitter.TabIndex = 2
        Me.BtQuitter.Text = "Quitter"
        '
        'NouveauGroupeCriterePostQ
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(517, 113)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtQuitter)
        Me.Controls.Add(Me.BtEnrgGroupe)
        Me.Controls.Add(Me.TxtGroupe)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "NouveauGroupeCriterePostQ"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Nouveau Groupe"
        CType(Me.TxtGroupe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TxtGroupe As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents BtEnrgGroupe As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtQuitter As DevExpress.XtraEditors.SimpleButton
End Class
