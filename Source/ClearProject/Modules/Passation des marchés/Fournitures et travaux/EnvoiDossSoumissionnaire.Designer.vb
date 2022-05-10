<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EnvoiDossSoumissionnaire
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
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.BtEnregComm = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelChoixValeur = New DevExpress.XtraEditors.PanelControl()
        Me.LgListapproskt = New DevExpress.XtraGrid.GridControl()
        Me.ViewArtiappro = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemPictureEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.RepositoryItemPictureEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        CType(Me.PanelChoixValeur, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelChoixValeur.SuspendLayout()
        CType(Me.LgListapproskt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewArtiappro, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtEnregComm
        '
        Me.BtEnregComm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtEnregComm.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnregComm.Appearance.Options.UseFont = True
        Me.BtEnregComm.Image = Global.ClearProject.My.Resources.Resources.Mail_16x16
        Me.BtEnregComm.Location = New System.Drawing.Point(135, 4)
        Me.BtEnregComm.Name = "BtEnregComm"
        Me.BtEnregComm.Size = New System.Drawing.Size(100, 25)
        Me.BtEnregComm.TabIndex = 1
        Me.BtEnregComm.Text = "Envoyer"
        Me.BtEnregComm.ToolTip = "Envoyer"
        '
        'PanelChoixValeur
        '
        Me.PanelChoixValeur.Controls.Add(Me.BtEnregComm)
        Me.PanelChoixValeur.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelChoixValeur.Location = New System.Drawing.Point(0, 142)
        Me.PanelChoixValeur.Name = "PanelChoixValeur"
        Me.PanelChoixValeur.Size = New System.Drawing.Size(380, 32)
        Me.PanelChoixValeur.TabIndex = 2
        '
        'LgListapproskt
        '
        Me.LgListapproskt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LgListapproskt.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        GridLevelNode1.RelationName = "Level1"
        Me.LgListapproskt.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.LgListapproskt.Location = New System.Drawing.Point(0, 0)
        Me.LgListapproskt.MainView = Me.ViewArtiappro
        Me.LgListapproskt.Name = "LgListapproskt"
        Me.LgListapproskt.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemPictureEdit1, Me.RepositoryItemPictureEdit2})
        Me.LgListapproskt.Size = New System.Drawing.Size(380, 142)
        Me.LgListapproskt.TabIndex = 16
        Me.LgListapproskt.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewArtiappro})
        '
        'ViewArtiappro
        '
        Me.ViewArtiappro.ActiveFilterEnabled = False
        Me.ViewArtiappro.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.Silver
        Me.ViewArtiappro.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewArtiappro.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.Silver
        Me.ViewArtiappro.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
        Me.ViewArtiappro.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.ViewArtiappro.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.ViewArtiappro.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewArtiappro.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.ViewArtiappro.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewArtiappro.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
        Me.ViewArtiappro.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.ViewArtiappro.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.ViewArtiappro.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewArtiappro.Appearance.Empty.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.EvenRow.BackColor = System.Drawing.Color.Silver
        Me.ViewArtiappro.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite
        Me.ViewArtiappro.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.ViewArtiappro.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewArtiappro.Appearance.EvenRow.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.EvenRow.Options.UseForeColor = True
        Me.ViewArtiappro.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewArtiappro.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(154, Byte), Integer))
        Me.ViewArtiappro.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewArtiappro.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.ViewArtiappro.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewArtiappro.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.ViewArtiappro.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.ViewArtiappro.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ViewArtiappro.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewArtiappro.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
        Me.ViewArtiappro.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewArtiappro.Appearance.FilterPanel.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.FilterPanel.Options.UseForeColor = True
        Me.ViewArtiappro.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.ViewArtiappro.Appearance.FixedLine.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.FocusedRow.BackColor = System.Drawing.Color.Teal
        Me.ViewArtiappro.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(178, Byte), Integer))
        Me.ViewArtiappro.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.ViewArtiappro.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewArtiappro.Appearance.FooterPanel.BackColor = System.Drawing.Color.Silver
        Me.ViewArtiappro.Appearance.FooterPanel.BorderColor = System.Drawing.Color.Silver
        Me.ViewArtiappro.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewArtiappro.Appearance.FooterPanel.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.ViewArtiappro.Appearance.FooterPanel.Options.UseForeColor = True
        Me.ViewArtiappro.Appearance.GroupButton.BackColor = System.Drawing.Color.Silver
        Me.ViewArtiappro.Appearance.GroupButton.BorderColor = System.Drawing.Color.Silver
        Me.ViewArtiappro.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
        Me.ViewArtiappro.Appearance.GroupButton.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.GroupButton.Options.UseBorderColor = True
        Me.ViewArtiappro.Appearance.GroupButton.Options.UseForeColor = True
        Me.ViewArtiappro.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ViewArtiappro.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ViewArtiappro.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.ViewArtiappro.Appearance.GroupFooter.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.ViewArtiappro.Appearance.GroupFooter.Options.UseForeColor = True
        Me.ViewArtiappro.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ViewArtiappro.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewArtiappro.Appearance.GroupPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ViewArtiappro.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
        Me.ViewArtiappro.Appearance.GroupPanel.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.GroupPanel.Options.UseFont = True
        Me.ViewArtiappro.Appearance.GroupPanel.Options.UseForeColor = True
        Me.ViewArtiappro.Appearance.GroupRow.BackColor = System.Drawing.Color.Gray
        Me.ViewArtiappro.Appearance.GroupRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.ViewArtiappro.Appearance.GroupRow.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.GroupRow.Options.UseForeColor = True
        Me.ViewArtiappro.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Silver
        Me.ViewArtiappro.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Silver
        Me.ViewArtiappro.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ViewArtiappro.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewArtiappro.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.ViewArtiappro.Appearance.HeaderPanel.Options.UseFont = True
        Me.ViewArtiappro.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.ViewArtiappro.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
        Me.ViewArtiappro.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewArtiappro.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.ViewArtiappro.Appearance.HorzLine.BackColor = System.Drawing.Color.Silver
        Me.ViewArtiappro.Appearance.HorzLine.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.OddRow.BackColor = System.Drawing.Color.White
        Me.ViewArtiappro.Appearance.OddRow.BackColor2 = System.Drawing.Color.White
        Me.ViewArtiappro.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.ViewArtiappro.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
        Me.ViewArtiappro.Appearance.OddRow.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.OddRow.Options.UseForeColor = True
        Me.ViewArtiappro.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ViewArtiappro.Appearance.Preview.BackColor2 = System.Drawing.Color.White
        Me.ViewArtiappro.Appearance.Preview.ForeColor = System.Drawing.Color.Teal
        Me.ViewArtiappro.Appearance.Preview.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.Preview.Options.UseForeColor = True
        Me.ViewArtiappro.Appearance.Row.BackColor = System.Drawing.Color.White
        Me.ViewArtiappro.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.ViewArtiappro.Appearance.Row.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.Row.Options.UseForeColor = True
        Me.ViewArtiappro.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
        Me.ViewArtiappro.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewArtiappro.Appearance.RowSeparator.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.ViewArtiappro.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
        Me.ViewArtiappro.Appearance.SelectedRow.Options.UseBackColor = True
        Me.ViewArtiappro.Appearance.SelectedRow.Options.UseForeColor = True
        Me.ViewArtiappro.Appearance.VertLine.BackColor = System.Drawing.Color.White
        Me.ViewArtiappro.Appearance.VertLine.Options.UseBackColor = True
        Me.ViewArtiappro.GridControl = Me.LgListapproskt
        Me.ViewArtiappro.Name = "ViewArtiappro"
        Me.ViewArtiappro.OptionsCustomization.AllowFilter = False
        Me.ViewArtiappro.OptionsSelection.EnableAppearanceHideSelection = False
        Me.ViewArtiappro.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewArtiappro.OptionsView.EnableAppearanceOddRow = True
        Me.ViewArtiappro.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewArtiappro.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewArtiappro.OptionsView.ShowGroupPanel = False
        Me.ViewArtiappro.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewArtiappro.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'RepositoryItemPictureEdit1
        '
        Me.RepositoryItemPictureEdit1.Name = "RepositoryItemPictureEdit1"
        '
        'RepositoryItemPictureEdit2
        '
        Me.RepositoryItemPictureEdit2.Name = "RepositoryItemPictureEdit2"
        '
        'EnvoiDossSoumissionnaire
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(380, 174)
        Me.Controls.Add(Me.LgListapproskt)
        Me.Controls.Add(Me.PanelChoixValeur)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EnvoiDossSoumissionnaire"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Envoi du DAO  aux soumissionnaires"
        CType(Me.PanelChoixValeur, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelChoixValeur.ResumeLayout(False)
        CType(Me.LgListapproskt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewArtiappro, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtEnregComm As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelChoixValeur As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LgListapproskt As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewArtiappro As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemPictureEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents RepositoryItemPictureEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
End Class
