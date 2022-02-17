<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Notification_EnvoiMail
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.BtEnvoyer = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.GridDestinataire = New DevExpress.XtraGrid.GridControl()
        Me.ViewDestinataire = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.BtAjoutCc = New DevExpress.XtraEditors.SimpleButton()
        Me.CmbListeCarbone = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl4 = New DevExpress.XtraEditors.PanelControl()
        Me.GridCarbone = New DevExpress.XtraGrid.GridControl()
        Me.ViewCarbone = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.GridDestinataire, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewDestinataire, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.CmbListeCarbone.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl4.SuspendLayout()
        CType(Me.GridCarbone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewCarbone, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.BtEnvoyer)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(813, 31)
        Me.PanelControl1.TabIndex = 0
        '
        'BtEnvoyer
        '
        Me.BtEnvoyer.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnvoyer.Appearance.Options.UseFont = True
        Me.BtEnvoyer.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtEnvoyer.Image = Global.ClearProject.My.Resources.Resources.Mail_16x16
        Me.BtEnvoyer.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft
        Me.BtEnvoyer.Location = New System.Drawing.Point(620, 2)
        Me.BtEnvoyer.Name = "BtEnvoyer"
        Me.BtEnvoyer.Size = New System.Drawing.Size(191, 27)
        Me.BtEnvoyer.TabIndex = 3
        Me.BtEnvoyer.Text = "ENVOYER"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.LabelControl1.Location = New System.Drawing.Point(2, 2)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(167, 27)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Envoyer à :"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.GridDestinataire)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl2.Location = New System.Drawing.Point(0, 31)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(813, 168)
        Me.PanelControl2.TabIndex = 1
        '
        'GridDestinataire
        '
        Me.GridDestinataire.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridDestinataire.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridDestinataire.Location = New System.Drawing.Point(2, 2)
        Me.GridDestinataire.MainView = Me.ViewDestinataire
        Me.GridDestinataire.Name = "GridDestinataire"
        Me.GridDestinataire.Size = New System.Drawing.Size(809, 164)
        Me.GridDestinataire.TabIndex = 6
        Me.GridDestinataire.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewDestinataire})
        '
        'ViewDestinataire
        '
        Me.ViewDestinataire.ActiveFilterEnabled = False
        Me.ViewDestinataire.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewDestinataire.Appearance.Row.Options.UseFont = True
        Me.ViewDestinataire.GridControl = Me.GridDestinataire
        Me.ViewDestinataire.Name = "ViewDestinataire"
        Me.ViewDestinataire.OptionsBehavior.Editable = False
        Me.ViewDestinataire.OptionsBehavior.ReadOnly = True
        Me.ViewDestinataire.OptionsCustomization.AllowColumnMoving = False
        Me.ViewDestinataire.OptionsCustomization.AllowFilter = False
        Me.ViewDestinataire.OptionsCustomization.AllowGroup = False
        Me.ViewDestinataire.OptionsCustomization.AllowSort = False
        Me.ViewDestinataire.OptionsFilter.AllowFilterEditor = False
        Me.ViewDestinataire.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewDestinataire.OptionsPrint.AutoWidth = False
        Me.ViewDestinataire.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewDestinataire.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewDestinataire.OptionsView.ColumnAutoWidth = False
        Me.ViewDestinataire.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewDestinataire.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewDestinataire.OptionsView.ShowGroupPanel = False
        Me.ViewDestinataire.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.[True]
        Me.ViewDestinataire.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewDestinataire.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.BtAjoutCc)
        Me.PanelControl3.Controls.Add(Me.CmbListeCarbone)
        Me.PanelControl3.Controls.Add(Me.LabelControl2)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl3.Location = New System.Drawing.Point(0, 199)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(813, 31)
        Me.PanelControl3.TabIndex = 2
        '
        'BtAjoutCc
        '
        Me.BtAjoutCc.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtAjoutCc.Appearance.Options.UseFont = True
        Me.BtAjoutCc.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtAjoutCc.Image = Global.ClearProject.My.Resources.Resources.ActiveRents_16x16
        Me.BtAjoutCc.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtAjoutCc.Location = New System.Drawing.Point(784, 2)
        Me.BtAjoutCc.Name = "BtAjoutCc"
        Me.BtAjoutCc.Size = New System.Drawing.Size(27, 27)
        Me.BtAjoutCc.TabIndex = 2
        Me.BtAjoutCc.ToolTip = "Ajouter"
        '
        'CmbListeCarbone
        '
        Me.CmbListeCarbone.Location = New System.Drawing.Point(163, 1)
        Me.CmbListeCarbone.Name = "CmbListeCarbone"
        Me.CmbListeCarbone.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbListeCarbone.Properties.Appearance.Options.UseFont = True
        Me.CmbListeCarbone.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbListeCarbone.Size = New System.Drawing.Size(615, 28)
        Me.CmbListeCarbone.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Left
        Me.LabelControl2.Location = New System.Drawing.Point(2, 2)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(155, 27)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "Copie Carbone : "
        '
        'PanelControl4
        '
        Me.PanelControl4.Controls.Add(Me.GridCarbone)
        Me.PanelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl4.Location = New System.Drawing.Point(0, 230)
        Me.PanelControl4.Name = "PanelControl4"
        Me.PanelControl4.Size = New System.Drawing.Size(813, 150)
        Me.PanelControl4.TabIndex = 3
        '
        'GridCarbone
        '
        Me.GridCarbone.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridCarbone.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridCarbone.Location = New System.Drawing.Point(2, 2)
        Me.GridCarbone.MainView = Me.ViewCarbone
        Me.GridCarbone.Name = "GridCarbone"
        Me.GridCarbone.Size = New System.Drawing.Size(809, 146)
        Me.GridCarbone.TabIndex = 7
        Me.GridCarbone.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewCarbone})
        '
        'ViewCarbone
        '
        Me.ViewCarbone.ActiveFilterEnabled = False
        Me.ViewCarbone.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewCarbone.Appearance.Row.Options.UseFont = True
        Me.ViewCarbone.GridControl = Me.GridCarbone
        Me.ViewCarbone.Name = "ViewCarbone"
        Me.ViewCarbone.OptionsBehavior.Editable = False
        Me.ViewCarbone.OptionsBehavior.ReadOnly = True
        Me.ViewCarbone.OptionsCustomization.AllowColumnMoving = False
        Me.ViewCarbone.OptionsCustomization.AllowFilter = False
        Me.ViewCarbone.OptionsCustomization.AllowGroup = False
        Me.ViewCarbone.OptionsCustomization.AllowSort = False
        Me.ViewCarbone.OptionsFilter.AllowFilterEditor = False
        Me.ViewCarbone.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewCarbone.OptionsPrint.AutoWidth = False
        Me.ViewCarbone.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewCarbone.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewCarbone.OptionsView.ColumnAutoWidth = False
        Me.ViewCarbone.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewCarbone.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewCarbone.OptionsView.ShowGroupPanel = False
        Me.ViewCarbone.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.[True]
        Me.ViewCarbone.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewCarbone.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'Notification_EnvoiMail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(813, 380)
        Me.Controls.Add(Me.PanelControl4)
        Me.Controls.Add(Me.PanelControl3)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Notification_EnvoiMail"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Envoi des notifications"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.GridDestinataire, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewDestinataire, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        CType(Me.CmbListeCarbone.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl4.ResumeLayout(False)
        CType(Me.GridCarbone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewCarbone, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtEnvoyer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridDestinataire As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewDestinataire As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtAjoutCc As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CmbListeCarbone As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl4 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GridCarbone As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewCarbone As DevExpress.XtraGrid.Views.Grid.GridView
End Class
