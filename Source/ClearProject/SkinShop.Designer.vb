<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SkinShop
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
        Me.components = New System.ComponentModel.Container()
        Me.ControlNavigator1 = New DevExpress.XtraEditors.ControlNavigator()
        Me.GridSkin = New DevExpress.XtraGrid.GridControl()
        Me.LayoutSkin = New DevExpress.XtraGrid.Views.Layout.LayoutView()
        Me.LayoutViewCard1 = New DevExpress.XtraGrid.Views.Layout.LayoutViewCard()
        Me.item1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.RepositoryItemPictureEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.RepositoryItemButtonEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TOperateurSkinBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataSet1 = New DataSet1()
        Me.T_OperateurSkinTableAdapter = New DataSet1TableAdapters.T_OperateurSkinTableAdapter()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.BtAppercu = New DevExpress.XtraEditors.SimpleButton()
        Me.BtValider = New DevExpress.XtraEditors.SimpleButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.GridSkin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutSkin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutViewCard1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.item1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TOperateurSkinBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ControlNavigator1
        '
        Me.ControlNavigator1.Appearance.Font = New System.Drawing.Font("Segoe Print", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ControlNavigator1.Appearance.Options.UseFont = True
        Me.ControlNavigator1.Buttons.Append.Visible = False
        Me.ControlNavigator1.Buttons.CancelEdit.Visible = False
        Me.ControlNavigator1.Buttons.Edit.Visible = False
        Me.ControlNavigator1.Buttons.EndEdit.Visible = False
        Me.ControlNavigator1.Buttons.First.Visible = False
        Me.ControlNavigator1.Buttons.Last.Visible = False
        Me.ControlNavigator1.Buttons.NextPage.Visible = False
        Me.ControlNavigator1.Buttons.PrevPage.Visible = False
        Me.ControlNavigator1.Buttons.Remove.Visible = False
        Me.ControlNavigator1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ControlNavigator1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ControlNavigator1.Location = New System.Drawing.Point(2, 2)
        Me.ControlNavigator1.Name = "ControlNavigator1"
        Me.ControlNavigator1.NavigatableControl = Me.GridSkin
        Me.ControlNavigator1.Size = New System.Drawing.Size(279, 19)
        Me.ControlNavigator1.TabIndex = 2
        Me.ControlNavigator1.Text = "ControlNavigator1"
        Me.ControlNavigator1.TextStringFormat = "Thème {0} / {1}"
        '
        'GridSkin
        '
        Me.GridSkin.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GridSkin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridSkin.EmbeddedNavigator.Appearance.Options.UseImage = True
        Me.GridSkin.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.GridSkin.Location = New System.Drawing.Point(0, 0)
        Me.GridSkin.MainView = Me.LayoutSkin
        Me.GridSkin.Name = "GridSkin"
        Me.GridSkin.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPictureEdit1, Me.RepositoryItemButtonEdit1})
        Me.GridSkin.Size = New System.Drawing.Size(744, 262)
        Me.GridSkin.TabIndex = 3
        Me.GridSkin.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.LayoutSkin, Me.GridView1})
        '
        'LayoutSkin
        '
        Me.LayoutSkin.Appearance.Card.Font = New System.Drawing.Font("Segoe Print", 12.0!)
        Me.LayoutSkin.Appearance.Card.Options.UseFont = True
        Me.LayoutSkin.Appearance.HeaderPanel.Options.UseImage = True
        Me.LayoutSkin.CardMinSize = New System.Drawing.Size(265, 221)
        Me.LayoutSkin.GridControl = Me.GridSkin
        Me.LayoutSkin.Name = "LayoutSkin"
        Me.LayoutSkin.OptionsBehavior.AutoFocusNewCard = True
        Me.LayoutSkin.OptionsBehavior.ReadOnly = True
        Me.LayoutSkin.OptionsCarouselMode.BottomCardScale = 0.6!
        Me.LayoutSkin.OptionsCarouselMode.CardCount = 20
        Me.LayoutSkin.OptionsCarouselMode.PitchAngle = 1.570796!
        Me.LayoutSkin.OptionsCarouselMode.Radius = 350
        Me.LayoutSkin.OptionsCarouselMode.RollAngle = 3.14!
        Me.LayoutSkin.OptionsHeaderPanel.EnableCustomizeButton = False
        Me.LayoutSkin.OptionsView.AllowHotTrackFields = False
        Me.LayoutSkin.OptionsView.ShowCardCaption = False
        Me.LayoutSkin.OptionsView.ShowCardExpandButton = False
        Me.LayoutSkin.OptionsView.ShowCardFieldBorders = True
        Me.LayoutSkin.OptionsView.ShowCardLines = False
        Me.LayoutSkin.OptionsView.ShowFieldHints = False
        Me.LayoutSkin.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.LayoutSkin.OptionsView.ShowHeaderPanel = False
        Me.LayoutSkin.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.Carousel
        Me.LayoutSkin.TemplateCard = Me.LayoutViewCard1
        '
        'LayoutViewCard1
        '
        Me.LayoutViewCard1.CustomizationFormText = "TemplateCard"
        Me.LayoutViewCard1.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText
        Me.LayoutViewCard1.GroupBordersVisible = False
        Me.LayoutViewCard1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.item1})
        Me.LayoutViewCard1.Name = "layoutViewTemplateCard"
        Me.LayoutViewCard1.OptionsItemText.TextToControlDistance = 5
        Me.LayoutViewCard1.Text = "TemplateCard"
        '
        'item1
        '
        Me.item1.AllowHotTrack = False
        Me.item1.CustomizationFormText = "item1"
        Me.item1.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.item1.Location = New System.Drawing.Point(0, 0)
        Me.item1.Name = "item1"
        Me.item1.Size = New System.Drawing.Size(119, 30)
        Me.item1.Text = "item1"
        Me.item1.TextSize = New System.Drawing.Size(0, 0)
        '
        'RepositoryItemPictureEdit1
        '
        Me.RepositoryItemPictureEdit1.Name = "RepositoryItemPictureEdit1"
        '
        'RepositoryItemButtonEdit1
        '
        Me.RepositoryItemButtonEdit1.AutoHeight = False
        Me.RepositoryItemButtonEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.RepositoryItemButtonEdit1.Name = "RepositoryItemButtonEdit1"
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.GridSkin
        Me.GridView1.Name = "GridView1"
        '
        'TOperateurSkinBindingSource
        '
        Me.TOperateurSkinBindingSource.DataMember = "T_OperateurSkin"
        Me.TOperateurSkinBindingSource.DataSource = Me.DataSet1
        '
        'DataSet1
        '
        Me.DataSet1.DataSetName = "DataSet1"
        Me.DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'T_OperateurSkinTableAdapter
        '
        Me.T_OperateurSkinTableAdapter.ClearBeforeFill = True
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.BtAppercu)
        Me.PanelControl1.Controls.Add(Me.BtValider)
        Me.PanelControl1.Controls.Add(Me.ControlNavigator1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 239)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(744, 23)
        Me.PanelControl1.TabIndex = 4
        '
        'BtAppercu
        '
        Me.BtAppercu.Appearance.Font = New System.Drawing.Font("Segoe Print", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtAppercu.Appearance.Options.UseFont = True
        Me.BtAppercu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtAppercu.Image = Global.ClearProject.My.Resources.Resources.Ribbon_New_16x16
        Me.BtAppercu.Location = New System.Drawing.Point(281, 2)
        Me.BtAppercu.Name = "BtAppercu"
        Me.BtAppercu.Size = New System.Drawing.Size(167, 19)
        Me.BtAppercu.TabIndex = 4
        Me.BtAppercu.Text = "Appliquer"
        '
        'BtValider
        '
        Me.BtValider.Appearance.Font = New System.Drawing.Font("Segoe Print", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtValider.Appearance.Options.UseFont = True
        Me.BtValider.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtValider.Image = Global.ClearProject.My.Resources.Resources.ActiveRents_16x16
        Me.BtValider.Location = New System.Drawing.Point(448, 2)
        Me.BtValider.Name = "BtValider"
        Me.BtValider.Size = New System.Drawing.Size(294, 19)
        Me.BtValider.TabIndex = 3
        Me.BtValider.Text = "Définir comme mon thème personnel"
        '
        'Timer1
        '
        '
        'SkinShop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(744, 262)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.GridSkin)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SkinShop"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ClearSkin"
        CType(Me.GridSkin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutSkin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutViewCard1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.item1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TOperateurSkinBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ControlNavigator1 As DevExpress.XtraEditors.ControlNavigator
    Friend WithEvents GridSkin As DevExpress.XtraGrid.GridControl
    Friend WithEvents LayoutSkin As DevExpress.XtraGrid.Views.Layout.LayoutView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemPictureEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents DataSet1 As DataSet1
    Friend WithEvents TOperateurSkinBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents T_OperateurSkinTableAdapter As DataSet1TableAdapters.T_OperateurSkinTableAdapter
    Friend WithEvents RepositoryItemButtonEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtValider As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtAppercu As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents LayoutViewCard1 As DevExpress.XtraGrid.Views.Layout.LayoutViewCard
    Friend WithEvents item1 As DevExpress.XtraLayout.EmptySpaceItem
End Class
