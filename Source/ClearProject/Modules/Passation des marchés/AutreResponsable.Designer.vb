<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AutreResponsable
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
        Me.gcTitre = New DevExpress.XtraEditors.GroupControl()
        Me.txtStructure = New DevExpress.XtraEditors.TextEdit()
        Me.txtFonction = New DevExpress.XtraEditors.TextEdit()
        Me.txtContact = New DevExpress.XtraEditors.TextEdit()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPrenom = New DevExpress.XtraEditors.TextEdit()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtNom = New DevExpress.XtraEditors.TextEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btOK = New DevExpress.XtraEditors.SimpleButton()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.gcTitre, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcTitre.SuspendLayout()
        CType(Me.txtStructure.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFonction.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtContact.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrenom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gcTitre
        '
        Me.gcTitre.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gcTitre.AppearanceCaption.Options.UseFont = True
        Me.gcTitre.Controls.Add(Me.txtStructure)
        Me.gcTitre.Controls.Add(Me.txtFonction)
        Me.gcTitre.Controls.Add(Me.txtContact)
        Me.gcTitre.Controls.Add(Me.Label5)
        Me.gcTitre.Controls.Add(Me.txtPrenom)
        Me.gcTitre.Controls.Add(Me.Label3)
        Me.gcTitre.Controls.Add(Me.txtNom)
        Me.gcTitre.Controls.Add(Me.Label2)
        Me.gcTitre.Controls.Add(Me.btCancel)
        Me.gcTitre.Controls.Add(Me.Label1)
        Me.gcTitre.Controls.Add(Me.btOK)
        Me.gcTitre.Controls.Add(Me.Label4)
        Me.gcTitre.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcTitre.Location = New System.Drawing.Point(0, 0)
        Me.gcTitre.Name = "gcTitre"
        Me.gcTitre.Size = New System.Drawing.Size(345, 215)
        Me.gcTitre.TabIndex = 5
        Me.gcTitre.Text = "Définir un responsable"
        '
        'txtStructure
        '
        Me.txtStructure.Location = New System.Drawing.Point(70, 142)
        Me.txtStructure.Name = "txtStructure"
        Me.txtStructure.Properties.MaxLength = 160
        Me.txtStructure.Size = New System.Drawing.Size(257, 20)
        Me.txtStructure.TabIndex = 15
        '
        'txtFonction
        '
        Me.txtFonction.Location = New System.Drawing.Point(70, 116)
        Me.txtFonction.Name = "txtFonction"
        Me.txtFonction.Properties.MaxLength = 160
        Me.txtFonction.Size = New System.Drawing.Size(257, 20)
        Me.txtFonction.TabIndex = 11
        '
        'txtContact
        '
        Me.txtContact.Location = New System.Drawing.Point(70, 87)
        Me.txtContact.Name = "txtContact"
        Me.txtContact.Properties.MaxLength = 160
        Me.txtContact.Size = New System.Drawing.Size(257, 20)
        Me.txtContact.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 145)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Structure"
        '
        'txtPrenom
        '
        Me.txtPrenom.Location = New System.Drawing.Point(70, 58)
        Me.txtPrenom.Name = "txtPrenom"
        Me.txtPrenom.Properties.MaxLength = 255
        Me.txtPrenom.Size = New System.Drawing.Size(257, 20)
        Me.txtPrenom.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 119)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Fonction *"
        '
        'txtNom
        '
        Me.txtNom.Location = New System.Drawing.Point(70, 32)
        Me.txtNom.Name = "txtNom"
        Me.txtNom.Properties.MaxLength = 50
        Me.txtNom.Size = New System.Drawing.Size(257, 20)
        Me.txtNom.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 90)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Contact *"
        '
        'btCancel
        '
        Me.btCancel.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btCancel.Appearance.Options.UseFont = True
        Me.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btCancel.Location = New System.Drawing.Point(179, 179)
        Me.btCancel.Name = "btCancel"
        Me.btCancel.Size = New System.Drawing.Size(66, 24)
        Me.btCancel.TabIndex = 25
        Me.btCancel.Text = "Annuler"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Prénoms *"
        '
        'btOK
        '
        Me.btOK.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btOK.Appearance.Options.UseFont = True
        Me.btOK.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btOK.Location = New System.Drawing.Point(90, 179)
        Me.btOK.Name = "btOK"
        Me.btOK.Size = New System.Drawing.Size(66, 24)
        Me.btOK.TabIndex = 20
        Me.btOK.Text = "OK"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 35)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Nom *"
        '
        'AutreResponsable
        '
        Me.AcceptButton = Me.btOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btCancel
        Me.ClientSize = New System.Drawing.Size(345, 215)
        Me.Controls.Add(Me.gcTitre)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "AutreResponsable"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Autre Responsable"
        CType(Me.gcTitre, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcTitre.ResumeLayout(False)
        Me.gcTitre.PerformLayout()
        CType(Me.txtStructure.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFonction.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtContact.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrenom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gcTitre As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label4 As Label
    Friend WithEvents txtNom As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtContact As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPrenom As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtStructure As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtFonction As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
End Class
