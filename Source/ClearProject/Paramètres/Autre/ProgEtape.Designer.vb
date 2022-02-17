<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProgEtape
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
        Me.CmbRespoEtpe = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.DureeEtape = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.DateDebutEtape = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.TitreEtape = New System.Windows.Forms.RichTextBox
        Me.LabelNumeroEtape = New System.Windows.Forms.Label
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.TxtRespoCache = New System.Windows.Forms.TextBox
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'CmbRespoEtpe
        '
        Me.CmbRespoEtpe.FormattingEnabled = True
        Me.CmbRespoEtpe.Location = New System.Drawing.Point(92, 95)
        Me.CmbRespoEtpe.Name = "CmbRespoEtpe"
        Me.CmbRespoEtpe.Size = New System.Drawing.Size(324, 21)
        Me.CmbRespoEtpe.TabIndex = 18
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 99)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Responsable"
        '
        'DureeEtape
        '
        Me.DureeEtape.BackColor = System.Drawing.Color.White
        Me.DureeEtape.Location = New System.Drawing.Point(259, 68)
        Me.DureeEtape.Name = "DureeEtape"
        Me.DureeEtape.ReadOnly = True
        Me.DureeEtape.Size = New System.Drawing.Size(158, 21)
        Me.DureeEtape.TabIndex = 16
        Me.DureeEtape.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(220, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Durée"
        '
        'DateDebutEtape
        '
        Me.DateDebutEtape.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateDebutEtape.Location = New System.Drawing.Point(92, 68)
        Me.DateDebutEtape.Name = "DateDebutEtape"
        Me.DateDebutEtape.Size = New System.Drawing.Size(105, 21)
        Me.DateDebutEtape.TabIndex = 14
        Me.DateDebutEtape.Value = New Date(2012, 12, 3, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 71)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Date Début"
        '
        'TitreEtape
        '
        Me.TitreEtape.BackColor = System.Drawing.Color.White
        Me.TitreEtape.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TitreEtape.Location = New System.Drawing.Point(92, 12)
        Me.TitreEtape.Name = "TitreEtape"
        Me.TitreEtape.ReadOnly = True
        Me.TitreEtape.Size = New System.Drawing.Size(325, 47)
        Me.TitreEtape.TabIndex = 12
        Me.TitreEtape.Text = ""
        '
        'LabelNumeroEtape
        '
        Me.LabelNumeroEtape.AutoSize = True
        Me.LabelNumeroEtape.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelNumeroEtape.Location = New System.Drawing.Point(9, 28)
        Me.LabelNumeroEtape.Name = "LabelNumeroEtape"
        Me.LabelNumeroEtape.Size = New System.Drawing.Size(68, 16)
        Me.LabelNumeroEtape.TabIndex = 11
        Me.LabelNumeroEtape.Text = "Etape N°1"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(276, 128)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 10
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.BackColor = System.Drawing.Color.White
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        Me.OK_Button.UseVisualStyleBackColor = False
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.BackColor = System.Drawing.Color.White
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        Me.Cancel_Button.UseVisualStyleBackColor = False
        '
        'TxtRespoCache
        '
        Me.TxtRespoCache.BackColor = System.Drawing.Color.White
        Me.TxtRespoCache.Location = New System.Drawing.Point(100, 95)
        Me.TxtRespoCache.Name = "TxtRespoCache"
        Me.TxtRespoCache.ReadOnly = True
        Me.TxtRespoCache.Size = New System.Drawing.Size(43, 21)
        Me.TxtRespoCache.TabIndex = 19
        Me.TxtRespoCache.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ProgEtape
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(434, 169)
        Me.ControlBox = False
        Me.Controls.Add(Me.CmbRespoEtpe)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DureeEtape)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateDebutEtape)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TitreEtape)
        Me.Controls.Add(Me.LabelNumeroEtape)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.TxtRespoCache)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "ProgEtape"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Attribution plan"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CmbRespoEtpe As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DureeEtape As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DateDebutEtape As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TitreEtape As System.Windows.Forms.RichTextBox
    Friend WithEvents LabelNumeroEtape As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents TxtRespoCache As System.Windows.Forms.TextBox
End Class
