<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ValiderAttributionMarche
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
        Me.Eval1 = New DevExpress.XtraEditors.CheckEdit()
        Me.Eval2 = New DevExpress.XtraEditors.CheckEdit()
        Me.Eval3 = New DevExpress.XtraEditors.CheckEdit()
        Me.Eval4 = New DevExpress.XtraEditors.CheckEdit()
        Me.Eval5 = New DevExpress.XtraEditors.CheckEdit()
        Me.BtAttribuer = New DevExpress.XtraEditors.SimpleButton()
        Me.BtAnnuler = New DevExpress.XtraEditors.SimpleButton()
        Me.Attente = New System.Windows.Forms.ProgressBar()
        Me.Avertissement = New DevExpress.XtraEditors.MemoEdit()
        CType(Me.Eval1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Eval2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Eval3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Eval4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Eval5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Avertissement.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Eval1
        '
        Me.Eval1.Location = New System.Drawing.Point(78, 103)
        Me.Eval1.Name = "Eval1"
        Me.Eval1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
        Me.Eval1.Properties.Caption = ""
        Me.Eval1.Properties.ReadOnly = True
        Me.Eval1.Size = New System.Drawing.Size(23, 23)
        Me.Eval1.TabIndex = 1
        Me.Eval1.Visible = False
        '
        'Eval2
        '
        Me.Eval2.Location = New System.Drawing.Point(128, 103)
        Me.Eval2.Name = "Eval2"
        Me.Eval2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
        Me.Eval2.Properties.Caption = ""
        Me.Eval2.Properties.ReadOnly = True
        Me.Eval2.Size = New System.Drawing.Size(23, 23)
        Me.Eval2.TabIndex = 2
        Me.Eval2.Visible = False
        '
        'Eval3
        '
        Me.Eval3.Location = New System.Drawing.Point(178, 103)
        Me.Eval3.Name = "Eval3"
        Me.Eval3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
        Me.Eval3.Properties.Caption = ""
        Me.Eval3.Properties.ReadOnly = True
        Me.Eval3.Size = New System.Drawing.Size(23, 23)
        Me.Eval3.TabIndex = 3
        Me.Eval3.Visible = False
        '
        'Eval4
        '
        Me.Eval4.Location = New System.Drawing.Point(228, 103)
        Me.Eval4.Name = "Eval4"
        Me.Eval4.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
        Me.Eval4.Properties.Caption = ""
        Me.Eval4.Properties.ReadOnly = True
        Me.Eval4.Size = New System.Drawing.Size(23, 23)
        Me.Eval4.TabIndex = 4
        Me.Eval4.Visible = False
        '
        'Eval5
        '
        Me.Eval5.Location = New System.Drawing.Point(278, 103)
        Me.Eval5.Name = "Eval5"
        Me.Eval5.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
        Me.Eval5.Properties.Caption = ""
        Me.Eval5.Properties.ReadOnly = True
        Me.Eval5.Size = New System.Drawing.Size(23, 23)
        Me.Eval5.TabIndex = 5
        Me.Eval5.Visible = False
        '
        'BtAttribuer
        '
        Me.BtAttribuer.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtAttribuer.Appearance.Options.UseFont = True
        Me.BtAttribuer.Enabled = False
        Me.BtAttribuer.Image = Global.ClearProject.My.Resources.Resources.Ribbon_SaveAs_32x32
        Me.BtAttribuer.Location = New System.Drawing.Point(114, 139)
        Me.BtAttribuer.Name = "BtAttribuer"
        Me.BtAttribuer.Size = New System.Drawing.Size(153, 35)
        Me.BtAttribuer.TabIndex = 6
        Me.BtAttribuer.Text = "ATTRIBUER"
        '
        'BtAnnuler
        '
        Me.BtAnnuler.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtAnnuler.Appearance.Options.UseFont = True
        Me.BtAnnuler.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_32
        Me.BtAnnuler.Location = New System.Drawing.Point(11, 139)
        Me.BtAnnuler.Name = "BtAnnuler"
        Me.BtAnnuler.Size = New System.Drawing.Size(37, 35)
        Me.BtAnnuler.TabIndex = 7
        '
        'Attente
        '
        Me.Attente.Location = New System.Drawing.Point(11, 83)
        Me.Attente.Name = "Attente"
        Me.Attente.Size = New System.Drawing.Size(360, 14)
        Me.Attente.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.Attente.TabIndex = 8
        Me.Attente.Visible = False
        '
        'Avertissement
        '
        Me.Avertissement.EditValue = ""
        Me.Avertissement.Location = New System.Drawing.Point(11, 2)
        Me.Avertissement.Name = "Avertissement"
        Me.Avertissement.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Avertissement.Properties.Appearance.ForeColor = System.Drawing.Color.Red
        Me.Avertissement.Properties.Appearance.Options.UseFont = True
        Me.Avertissement.Properties.Appearance.Options.UseForeColor = True
        Me.Avertissement.Properties.Appearance.Options.UseTextOptions = True
        Me.Avertissement.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.Avertissement.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.Avertissement.Properties.ReadOnly = True
        Me.Avertissement.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.Avertissement.Size = New System.Drawing.Size(360, 84)
        Me.Avertissement.TabIndex = 9
        '
        'ValiderAttributionMarche
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 186)
        Me.ControlBox = False
        Me.Controls.Add(Me.Attente)
        Me.Controls.Add(Me.Avertissement)
        Me.Controls.Add(Me.BtAnnuler)
        Me.Controls.Add(Me.BtAttribuer)
        Me.Controls.Add(Me.Eval5)
        Me.Controls.Add(Me.Eval4)
        Me.Controls.Add(Me.Eval3)
        Me.Controls.Add(Me.Eval2)
        Me.Controls.Add(Me.Eval1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ValiderAttributionMarche"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Accord d'attribution"
        CType(Me.Eval1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Eval2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Eval3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Eval4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Eval5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Avertissement.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Eval1 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Eval2 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Eval3 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Eval4 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Eval5 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents BtAttribuer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtAnnuler As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Attente As System.Windows.Forms.ProgressBar
    Friend WithEvents Avertissement As DevExpress.XtraEditors.MemoEdit
End Class
