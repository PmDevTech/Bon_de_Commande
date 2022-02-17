<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NotificationAttributionRejet
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
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.PageAttribution = New DevExpress.XtraTab.XtraTabPage()
        Me.ViewAttrib = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.TxtCodeMarche = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.BtImprimAttrib = New DevExpress.XtraEditors.SimpleButton()
        Me.BtEnvoiAttrib = New DevExpress.XtraEditors.SimpleButton()
        Me.BtExportAttrib = New DevExpress.XtraEditors.SimpleButton()
        Me.PageRejet = New DevExpress.XtraTab.XtraTabPage()
        Me.ViewRejet = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.BtImprimRejet = New DevExpress.XtraEditors.SimpleButton()
        Me.BtEnvoiRejet = New DevExpress.XtraEditors.SimpleButton()
        Me.BtExportRejet = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.PageOrdreService = New DevExpress.XtraTab.XtraTabPage()
        Me.ViewOrdreService = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.BtImprimOrdre = New DevExpress.XtraEditors.SimpleButton()
        Me.BtEnvoiOrdre = New DevExpress.XtraEditors.SimpleButton()
        Me.BtExportOrdre = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.reportDocument1 = New CrystalDecisions.CrystalReports.Engine.ReportDocument()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.PageAttribution.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.TxtCodeMarche.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PageRejet.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        Me.PageOrdreService.SuspendLayout()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        Me.SuspendLayout()
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Right
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.PageAttribution
        Me.XtraTabControl1.Size = New System.Drawing.Size(1016, 514)
        Me.XtraTabControl1.TabIndex = 0
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.PageAttribution, Me.PageRejet, Me.PageOrdreService})
        '
        'PageAttribution
        '
        Me.PageAttribution.Controls.Add(Me.ViewAttrib)
        Me.PageAttribution.Controls.Add(Me.PanelControl1)
        Me.PageAttribution.Name = "PageAttribution"
        Me.PageAttribution.Size = New System.Drawing.Size(988, 508)
        Me.PageAttribution.Text = "ATTRIBUTION DE MARCHE"
        '
        'ViewAttrib
        '
        Me.ViewAttrib.ActiveViewIndex = -1
        Me.ViewAttrib.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ViewAttrib.DisplayGroupTree = False
        Me.ViewAttrib.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ViewAttrib.Location = New System.Drawing.Point(0, 28)
        Me.ViewAttrib.Name = "ViewAttrib"
        Me.ViewAttrib.SelectionFormula = ""
        Me.ViewAttrib.ShowCloseButton = False
        Me.ViewAttrib.ShowExportButton = False
        Me.ViewAttrib.ShowGotoPageButton = False
        Me.ViewAttrib.ShowGroupTreeButton = False
        Me.ViewAttrib.ShowPrintButton = False
        Me.ViewAttrib.ShowRefreshButton = False
        Me.ViewAttrib.ShowTextSearchButton = False
        Me.ViewAttrib.Size = New System.Drawing.Size(988, 480)
        Me.ViewAttrib.TabIndex = 1
        Me.ViewAttrib.ViewTimeSelectionFormula = ""
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.TxtCodeMarche)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Controls.Add(Me.BtImprimAttrib)
        Me.PanelControl1.Controls.Add(Me.BtEnvoiAttrib)
        Me.PanelControl1.Controls.Add(Me.BtExportAttrib)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(988, 28)
        Me.PanelControl1.TabIndex = 0
        '
        'TxtCodeMarche
        '
        Me.TxtCodeMarche.Location = New System.Drawing.Point(836, 4)
        Me.TxtCodeMarche.Name = "TxtCodeMarche"
        Me.TxtCodeMarche.Size = New System.Drawing.Size(50, 20)
        Me.TxtCodeMarche.TabIndex = 4
        Me.TxtCodeMarche.Visible = False
        '
        'LabelControl1
        '
        Me.LabelControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(312, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(362, 21)
        Me.LabelControl1.TabIndex = 3
        Me.LabelControl1.Text = "NOTIFICATION D'ATTRIBUTION DE MARCHE"
        '
        'BtImprimAttrib
        '
        Me.BtImprimAttrib.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtImprimAttrib.Appearance.Options.UseFont = True
        Me.BtImprimAttrib.Dock = System.Windows.Forms.DockStyle.Left
        Me.BtImprimAttrib.Image = Global.ClearProject.My.Resources.Resources.Group_Reports
        Me.BtImprimAttrib.Location = New System.Drawing.Point(92, 2)
        Me.BtImprimAttrib.Name = "BtImprimAttrib"
        Me.BtImprimAttrib.Size = New System.Drawing.Size(90, 24)
        Me.BtImprimAttrib.TabIndex = 2
        Me.BtImprimAttrib.Text = "Imprimer"
        Me.BtImprimAttrib.ToolTip = "Imprimer"
        '
        'BtEnvoiAttrib
        '
        Me.BtEnvoiAttrib.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnvoiAttrib.Appearance.Options.UseFont = True
        Me.BtEnvoiAttrib.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtEnvoiAttrib.Image = Global.ClearProject.My.Resources.Resources.Outbox_16x16
        Me.BtEnvoiAttrib.Location = New System.Drawing.Point(896, 2)
        Me.BtEnvoiAttrib.Name = "BtEnvoiAttrib"
        Me.BtEnvoiAttrib.Size = New System.Drawing.Size(90, 24)
        Me.BtEnvoiAttrib.TabIndex = 1
        Me.BtEnvoiAttrib.Text = "Envoyer"
        Me.BtEnvoiAttrib.ToolTip = "Envoyer"
        '
        'BtExportAttrib
        '
        Me.BtExportAttrib.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtExportAttrib.Appearance.Options.UseFont = True
        Me.BtExportAttrib.Dock = System.Windows.Forms.DockStyle.Left
        Me.BtExportAttrib.Image = Global.ClearProject.My.Resources.Resources.ExportToPDF_16x16
        Me.BtExportAttrib.Location = New System.Drawing.Point(2, 2)
        Me.BtExportAttrib.Name = "BtExportAttrib"
        Me.BtExportAttrib.Size = New System.Drawing.Size(90, 24)
        Me.BtExportAttrib.TabIndex = 0
        Me.BtExportAttrib.Text = "Exporter"
        Me.BtExportAttrib.ToolTip = "Exporter"
        '
        'PageRejet
        '
        Me.PageRejet.Controls.Add(Me.ViewRejet)
        Me.PageRejet.Controls.Add(Me.PanelControl2)
        Me.PageRejet.Name = "PageRejet"
        Me.PageRejet.Size = New System.Drawing.Size(988, 508)
        Me.PageRejet.Text = "REJET D'OFFRE"
        '
        'ViewRejet
        '
        Me.ViewRejet.ActiveViewIndex = -1
        Me.ViewRejet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ViewRejet.DisplayGroupTree = False
        Me.ViewRejet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ViewRejet.Location = New System.Drawing.Point(0, 28)
        Me.ViewRejet.Name = "ViewRejet"
        Me.ViewRejet.SelectionFormula = ""
        Me.ViewRejet.ShowCloseButton = False
        Me.ViewRejet.ShowExportButton = False
        Me.ViewRejet.ShowGotoPageButton = False
        Me.ViewRejet.ShowGroupTreeButton = False
        Me.ViewRejet.ShowPrintButton = False
        Me.ViewRejet.ShowRefreshButton = False
        Me.ViewRejet.ShowTextSearchButton = False
        Me.ViewRejet.Size = New System.Drawing.Size(988, 480)
        Me.ViewRejet.TabIndex = 3
        Me.ViewRejet.ViewTimeSelectionFormula = ""
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.BtImprimRejet)
        Me.PanelControl2.Controls.Add(Me.BtEnvoiRejet)
        Me.PanelControl2.Controls.Add(Me.BtExportRejet)
        Me.PanelControl2.Controls.Add(Me.LabelControl2)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(988, 28)
        Me.PanelControl2.TabIndex = 2
        '
        'BtImprimRejet
        '
        Me.BtImprimRejet.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtImprimRejet.Appearance.Options.UseFont = True
        Me.BtImprimRejet.Dock = System.Windows.Forms.DockStyle.Left
        Me.BtImprimRejet.Image = Global.ClearProject.My.Resources.Resources.Group_Reports
        Me.BtImprimRejet.Location = New System.Drawing.Point(92, 2)
        Me.BtImprimRejet.Name = "BtImprimRejet"
        Me.BtImprimRejet.Size = New System.Drawing.Size(90, 24)
        Me.BtImprimRejet.TabIndex = 7
        Me.BtImprimRejet.Text = "Imprimer"
        Me.BtImprimRejet.ToolTip = "Imprimer"
        '
        'BtEnvoiRejet
        '
        Me.BtEnvoiRejet.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnvoiRejet.Appearance.Options.UseFont = True
        Me.BtEnvoiRejet.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtEnvoiRejet.Image = Global.ClearProject.My.Resources.Resources.Outbox_16x16
        Me.BtEnvoiRejet.Location = New System.Drawing.Point(896, 2)
        Me.BtEnvoiRejet.Name = "BtEnvoiRejet"
        Me.BtEnvoiRejet.Size = New System.Drawing.Size(90, 24)
        Me.BtEnvoiRejet.TabIndex = 6
        Me.BtEnvoiRejet.Text = "Envoyer"
        Me.BtEnvoiRejet.ToolTip = "Envoyer"
        '
        'BtExportRejet
        '
        Me.BtExportRejet.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtExportRejet.Appearance.Options.UseFont = True
        Me.BtExportRejet.Dock = System.Windows.Forms.DockStyle.Left
        Me.BtExportRejet.Image = Global.ClearProject.My.Resources.Resources.ExportToPDF_16x16
        Me.BtExportRejet.Location = New System.Drawing.Point(2, 2)
        Me.BtExportRejet.Name = "BtExportRejet"
        Me.BtExportRejet.Size = New System.Drawing.Size(90, 24)
        Me.BtExportRejet.TabIndex = 5
        Me.BtExportRejet.Text = "Exporter"
        Me.BtExportRejet.ToolTip = "Exporter"
        '
        'LabelControl2
        '
        Me.LabelControl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Location = New System.Drawing.Point(313, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(362, 21)
        Me.LabelControl2.TabIndex = 4
        Me.LabelControl2.Text = "NOTIFICATION DE REJET DE L'OFFRE"
        '
        'PageOrdreService
        '
        Me.PageOrdreService.Controls.Add(Me.ViewOrdreService)
        Me.PageOrdreService.Controls.Add(Me.PanelControl3)
        Me.PageOrdreService.Name = "PageOrdreService"
        Me.PageOrdreService.Size = New System.Drawing.Size(988, 508)
        Me.PageOrdreService.Text = "ORDRE DE SERVICE"
        '
        'ViewOrdreService
        '
        Me.ViewOrdreService.ActiveViewIndex = -1
        Me.ViewOrdreService.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ViewOrdreService.DisplayGroupTree = False
        Me.ViewOrdreService.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ViewOrdreService.Location = New System.Drawing.Point(0, 28)
        Me.ViewOrdreService.Name = "ViewOrdreService"
        Me.ViewOrdreService.ShowCloseButton = False
        Me.ViewOrdreService.ShowExportButton = False
        Me.ViewOrdreService.ShowGotoPageButton = False
        Me.ViewOrdreService.ShowGroupTreeButton = False
        Me.ViewOrdreService.ShowPrintButton = False
        Me.ViewOrdreService.ShowRefreshButton = False
        Me.ViewOrdreService.ShowTextSearchButton = False
        Me.ViewOrdreService.Size = New System.Drawing.Size(988, 480)
        Me.ViewOrdreService.TabIndex = 5
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.BtImprimOrdre)
        Me.PanelControl3.Controls.Add(Me.BtEnvoiOrdre)
        Me.PanelControl3.Controls.Add(Me.BtExportOrdre)
        Me.PanelControl3.Controls.Add(Me.LabelControl3)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl3.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(988, 28)
        Me.PanelControl3.TabIndex = 4
        '
        'BtImprimOrdre
        '
        Me.BtImprimOrdre.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtImprimOrdre.Appearance.Options.UseFont = True
        Me.BtImprimOrdre.Dock = System.Windows.Forms.DockStyle.Left
        Me.BtImprimOrdre.Image = Global.ClearProject.My.Resources.Resources.Group_Reports
        Me.BtImprimOrdre.Location = New System.Drawing.Point(92, 2)
        Me.BtImprimOrdre.Name = "BtImprimOrdre"
        Me.BtImprimOrdre.Size = New System.Drawing.Size(90, 24)
        Me.BtImprimOrdre.TabIndex = 7
        Me.BtImprimOrdre.Text = "Imprimer"
        Me.BtImprimOrdre.ToolTip = "Imprimer"
        '
        'BtEnvoiOrdre
        '
        Me.BtEnvoiOrdre.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnvoiOrdre.Appearance.Options.UseFont = True
        Me.BtEnvoiOrdre.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtEnvoiOrdre.Image = Global.ClearProject.My.Resources.Resources.Outbox_16x16
        Me.BtEnvoiOrdre.Location = New System.Drawing.Point(896, 2)
        Me.BtEnvoiOrdre.Name = "BtEnvoiOrdre"
        Me.BtEnvoiOrdre.Size = New System.Drawing.Size(90, 24)
        Me.BtEnvoiOrdre.TabIndex = 6
        Me.BtEnvoiOrdre.Text = "Envoyer"
        Me.BtEnvoiOrdre.ToolTip = "Envoyer"
        '
        'BtExportOrdre
        '
        Me.BtExportOrdre.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtExportOrdre.Appearance.Options.UseFont = True
        Me.BtExportOrdre.Dock = System.Windows.Forms.DockStyle.Left
        Me.BtExportOrdre.Image = Global.ClearProject.My.Resources.Resources.ExportToPDF_16x16
        Me.BtExportOrdre.Location = New System.Drawing.Point(2, 2)
        Me.BtExportOrdre.Name = "BtExportOrdre"
        Me.BtExportOrdre.Size = New System.Drawing.Size(90, 24)
        Me.BtExportOrdre.TabIndex = 5
        Me.BtExportOrdre.Text = "Exporter"
        Me.BtExportOrdre.ToolTip = "Exporter"
        '
        'LabelControl3
        '
        Me.LabelControl3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl3.Location = New System.Drawing.Point(313, 4)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(362, 21)
        Me.LabelControl3.TabIndex = 4
        Me.LabelControl3.Text = "ORDRE DE SERVICE"
        '
        'reportDocument1
        '
        Me.reportDocument1.FileName = "rassdk://C:\Users\FRANCK\Documents\Visual Studio 2010\Projects\ClearProject\DxC" & _
            "learProject\bin\Debug\Etats\Marches\OrdreDeService.rpt"
        '
        'NotificationAttributionRejet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 514)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "NotificationAttributionRejet"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Notifications d'Attribution de marché et de Rejet d'Offres"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.PageAttribution.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.TxtCodeMarche.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PageRejet.ResumeLayout(False)
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.PageOrdreService.ResumeLayout(False)
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents PageAttribution As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PageRejet As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtImprimAttrib As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtEnvoiAttrib As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtExportAttrib As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ViewAttrib As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ViewRejet As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtImprimRejet As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtEnvoiRejet As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtExportRejet As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtCodeMarche As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PageOrdreService As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtImprimOrdre As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtEnvoiOrdre As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtExportOrdre As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ViewOrdreService As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents reportDocument1 As CrystalDecisions.CrystalReports.Engine.ReportDocument
End Class
