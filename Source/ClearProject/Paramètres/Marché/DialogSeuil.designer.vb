<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DialogSeuil
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SelectMarche = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SelectMethode = New System.Windows.Forms.ComboBox()
        Me.TxtMethode = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.NoPlafondLimite = New System.Windows.Forms.CheckBox()
        Me.TousMontants = New System.Windows.Forms.CheckBox()
        Me.SelectRevue = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PlafondInclus = New System.Windows.Forms.CheckBox()
        Me.MontantPlafond = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PlancheInclus = New System.Windows.Forms.CheckBox()
        Me.MontantPlanche = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CodeMethodeCache = New System.Windows.Forms.TextBox()
        Me.CombBailleur = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(194, 230)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Type de Marché"
        '
        'SelectMarche
        '
        Me.SelectMarche.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SelectMarche.FormattingEnabled = True
        Me.SelectMarche.Items.AddRange(New Object() {"Travaux", "Fournitures", "Consultants"})
        Me.SelectMarche.Location = New System.Drawing.Point(97, 15)
        Me.SelectMarche.Name = "SelectMarche"
        Me.SelectMarche.Size = New System.Drawing.Size(105, 21)
        Me.SelectMarche.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(209, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Méthode"
        '
        'SelectMethode
        '
        Me.SelectMethode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SelectMethode.FormattingEnabled = True
        Me.SelectMethode.Location = New System.Drawing.Point(261, 15)
        Me.SelectMethode.Name = "SelectMethode"
        Me.SelectMethode.Size = New System.Drawing.Size(80, 21)
        Me.SelectMethode.TabIndex = 9
        '
        'TxtMethode
        '
        Me.TxtMethode.Location = New System.Drawing.Point(97, 45)
        Me.TxtMethode.Name = "TxtMethode"
        Me.TxtMethode.ReadOnly = True
        Me.TxtMethode.Size = New System.Drawing.Size(244, 20)
        Me.TxtMethode.TabIndex = 10
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.NoPlafondLimite)
        Me.GroupBox1.Controls.Add(Me.TousMontants)
        Me.GroupBox1.Controls.Add(Me.SelectRevue)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.PlafondInclus)
        Me.GroupBox1.Controls.Add(Me.MontantPlafond)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.PlancheInclus)
        Me.GroupBox1.Controls.Add(Me.MontantPlanche)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 105)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(329, 119)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Intervalle de montants"
        '
        'NoPlafondLimite
        '
        Me.NoPlafondLimite.AutoSize = True
        Me.NoPlafondLimite.Location = New System.Drawing.Point(188, 69)
        Me.NoPlafondLimite.Name = "NoPlafondLimite"
        Me.NoPlafondLimite.Size = New System.Drawing.Size(92, 17)
        Me.NoPlafondLimite.TabIndex = 9
        Me.NoPlafondLimite.Text = "Plafond illimité"
        Me.NoPlafondLimite.UseVisualStyleBackColor = True
        '
        'TousMontants
        '
        Me.TousMontants.AutoSize = True
        Me.TousMontants.Location = New System.Drawing.Point(86, 69)
        Me.TousMontants.Name = "TousMontants"
        Me.TousMontants.Size = New System.Drawing.Size(97, 17)
        Me.TousMontants.TabIndex = 8
        Me.TousMontants.Text = "Tous Montants"
        Me.TousMontants.UseVisualStyleBackColor = True
        '
        'SelectRevue
        '
        Me.SelectRevue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SelectRevue.FormattingEnabled = True
        Me.SelectRevue.Items.AddRange(New Object() {"Revue a Priori", "Revue a Postériori"})
        Me.SelectRevue.Location = New System.Drawing.Point(85, 92)
        Me.SelectRevue.Name = "SelectRevue"
        Me.SelectRevue.Size = New System.Drawing.Size(210, 21)
        Me.SelectRevue.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(43, 96)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Revue"
        '
        'PlafondInclus
        '
        Me.PlafondInclus.AutoSize = True
        Me.PlafondInclus.Location = New System.Drawing.Point(245, 43)
        Me.PlafondInclus.Name = "PlafondInclus"
        Me.PlafondInclus.Size = New System.Drawing.Size(54, 17)
        Me.PlafondInclus.TabIndex = 5
        Me.PlafondInclus.Text = "Inclus"
        Me.PlafondInclus.UseVisualStyleBackColor = True
        '
        'MontantPlafond
        '
        Me.MontantPlafond.Location = New System.Drawing.Point(85, 42)
        Me.MontantPlafond.Name = "MontantPlafond"
        Me.MontantPlafond.Size = New System.Drawing.Size(146, 20)
        Me.MontantPlafond.TabIndex = 4
        Me.MontantPlafond.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(39, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Plafond"
        '
        'PlancheInclus
        '
        Me.PlancheInclus.AutoSize = True
        Me.PlancheInclus.Location = New System.Drawing.Point(245, 17)
        Me.PlancheInclus.Name = "PlancheInclus"
        Me.PlancheInclus.Size = New System.Drawing.Size(54, 17)
        Me.PlancheInclus.TabIndex = 2
        Me.PlancheInclus.Text = "Inclus"
        Me.PlancheInclus.UseVisualStyleBackColor = True
        '
        'MontantPlanche
        '
        Me.MontantPlanche.Location = New System.Drawing.Point(85, 16)
        Me.MontantPlanche.Name = "MontantPlanche"
        Me.MontantPlanche.Size = New System.Drawing.Size(146, 20)
        Me.MontantPlanche.TabIndex = 1
        Me.MontantPlanche.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(35, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Plancher"
        '
        'CodeMethodeCache
        '
        Me.CodeMethodeCache.Location = New System.Drawing.Point(22, 44)
        Me.CodeMethodeCache.Name = "CodeMethodeCache"
        Me.CodeMethodeCache.Size = New System.Drawing.Size(43, 20)
        Me.CodeMethodeCache.TabIndex = 12
        Me.CodeMethodeCache.Visible = False
        '
        'CombBailleur
        '
        Me.CombBailleur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CombBailleur.FormattingEnabled = True
        Me.CombBailleur.Location = New System.Drawing.Point(97, 70)
        Me.CombBailleur.Name = "CombBailleur"
        Me.CombBailleur.Size = New System.Drawing.Size(244, 21)
        Me.CombBailleur.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(53, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Bailleur"
        '
        'DialogSeuilModifier
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(350, 259)
        Me.Controls.Add(Me.CombBailleur)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TxtMethode)
        Me.Controls.Add(Me.SelectMethode)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.SelectMarche)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.CodeMethodeCache)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DialogSeuilModifier"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SEUIL ET REVUE"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    'Friend WithEvents CsprCollapisblePanel1 As csprCollapsiblePanel.csprCollapisblePanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SelectMarche As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SelectMethode As System.Windows.Forms.ComboBox
    Friend WithEvents TxtMethode As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents SelectRevue As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents PlafondInclus As System.Windows.Forms.CheckBox
    Friend WithEvents MontantPlafond As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PlancheInclus As System.Windows.Forms.CheckBox
    Friend WithEvents MontantPlanche As System.Windows.Forms.TextBox
    Friend WithEvents CodeMethodeCache As System.Windows.Forms.TextBox
    Friend WithEvents TousMontants As System.Windows.Forms.CheckBox
    Friend WithEvents NoPlafondLimite As System.Windows.Forms.CheckBox
    Friend WithEvents CombBailleur As ComboBox
    Friend WithEvents Label6 As Label
End Class
