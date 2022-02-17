<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Afficher_doc
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
        Me.GridArchives = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SuppressionDocToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LayoutView1 = New DevExpress.XtraGrid.Views.Layout.LayoutView()
        Me.LayoutViewCard1 = New DevExpress.XtraGrid.Views.Layout.LayoutViewCard()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.GridArchives, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.LayoutView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutViewCard1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridArchives
        '
        Me.GridArchives.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GridArchives.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GridArchives.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridArchives.EmbeddedNavigator.Appearance.Options.UseImage = True
        Me.GridArchives.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.GridArchives.Location = New System.Drawing.Point(0, 0)
        Me.GridArchives.MainView = Me.LayoutView1
        Me.GridArchives.Name = "GridArchives"
        Me.GridArchives.Size = New System.Drawing.Size(495, 419)
        Me.GridArchives.TabIndex = 1
        Me.GridArchives.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.LayoutView1, Me.GridView1})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SuppressionDocToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(181, 48)
        '
        'SuppressionDocToolStripMenuItem
        '
        Me.SuppressionDocToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.SuppressionDocToolStripMenuItem.Name = "SuppressionDocToolStripMenuItem"
        Me.SuppressionDocToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SuppressionDocToolStripMenuItem.Text = "Supprimer"
        '
        'LayoutView1
        '
        Me.LayoutView1.Appearance.HeaderPanel.Options.UseImage = True
        Me.LayoutView1.CardMinSize = New System.Drawing.Size(207, 183)
        Me.LayoutView1.GridControl = Me.GridArchives
        Me.LayoutView1.Name = "LayoutView1"
        Me.LayoutView1.OptionsBehavior.Editable = False
        Me.LayoutView1.OptionsBehavior.ReadOnly = True
        Me.LayoutView1.OptionsCarouselMode.BottomCardScale = 0.4!
        Me.LayoutView1.OptionsCarouselMode.CardCount = 20
        Me.LayoutView1.OptionsCarouselMode.PitchAngle = 1.570796!
        Me.LayoutView1.OptionsCarouselMode.Radius = 200
        Me.LayoutView1.OptionsCarouselMode.RollAngle = 1.570796!
        Me.LayoutView1.OptionsHeaderPanel.EnableCustomizeButton = False
        Me.LayoutView1.OptionsView.ShowCardExpandButton = False
        Me.LayoutView1.OptionsView.ShowCardLines = False
        Me.LayoutView1.OptionsView.ShowHeaderPanel = False
        Me.LayoutView1.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.Carousel
        Me.LayoutView1.TemplateCard = Me.LayoutViewCard1
        '
        'LayoutViewCard1
        '
        Me.LayoutViewCard1.CustomizationFormText = "TemplateCard"
        Me.LayoutViewCard1.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText
        Me.LayoutViewCard1.Name = "LayoutViewCard1"
        Me.LayoutViewCard1.OptionsItemText.TextToControlDistance = 5
        Me.LayoutViewCard1.Text = "TemplateCard"
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.GridArchives
        Me.GridView1.Name = "GridView1"
        '
        'Afficher_doc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(495, 419)
        Me.Controls.Add(Me.GridArchives)
        Me.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Afficher_doc"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Liste des documents"
        CType(Me.GridArchives, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.LayoutView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutViewCard1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridArchives As DevExpress.XtraGrid.GridControl
    Friend WithEvents LayoutViewCard1 As DevExpress.XtraGrid.Views.Layout.LayoutViewCard
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Public WithEvents LayoutView1 As DevExpress.XtraGrid.Views.Layout.LayoutView
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SuppressionDocToolStripMenuItem As ToolStripMenuItem
End Class
