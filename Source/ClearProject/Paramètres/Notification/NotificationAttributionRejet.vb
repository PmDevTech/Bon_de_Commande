Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient

Public Class NotificationAttributionRejet

    Private Sub NotificationAttributionRejet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide



    End Sub

    Private Sub SortieFichier(ByVal typeNotif As String, Optional ByVal Direct As Boolean = False)

        Dim reportAttrib, reportRejet As New ReportDocument
        Dim Chemin As String = lineEtat & "\Marches\"

        Dim DatSet = New DataSet
        reportAttrib.Load(Chemin & IIf(typeNotif = "Attribution", "NotificationAttribution.rpt", IIf(typeNotif = "Rejet", "NotificationRejetOffre.rpt", "OrdreDeService.rpt").ToString).ToString)
        reportAttrib.SetDataSource(DatSet)
        reportAttrib.SetParameterValue("CodeProjet", ProjetEnCours)
        reportAttrib.SetParameterValue("CodeMarche", Me.TxtCodeMarche.Text)
        If (typeNotif <> "Demarr") Then
            reportAttrib.SetParameterValue("Ampliation", "")
            reportAttrib.SetParameterValue("CompteBanqueProjet", "Banque du Projet")
        End If

        Dim CheminExport As String = ""
        'If (Direct = False) Then
        Dim dlg As New SaveFileDialog
        dlg.DefaultExt = ".pdf"
        dlg.Filter = "Fichiers Pdf (*.Pdf)|*.pdf|*.Pdf|*.PDF"
        dlg.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        dlg.FileName = IIf(typeNotif = "Attribution", "Notification d'Attribution", IIf(typeNotif = "Rejet", "Notification de rejet", "Ordre de Service").ToString).ToString
        dlg.ShowDialog()
        If (dlg.FileName <> "") Then
            CheminExport = dlg.FileName
        End If
        'Else
        'CheminExport = My.Computer.FileSystem.SpecialDirectories.Temp & "\" & IIf(typeNotif = "Attribution", "Notification d'Attribution.pdf", "Notification de rejet.pdf").ToString
        'End If


        If (CheminExport <> "") Then
            reportAttrib.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, CheminExport)
        End If

    End Sub

    Private Sub BtExportAttrib_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtExportAttrib.Click
        SortieFichier("Attribution")
    End Sub

    Private Sub BtExportOrdre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtExportOrdre.Click
        SortieFichier("Demarr")
    End Sub

    Private Sub BtExportRejet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtExportRejet.Click
        SortieFichier("Rejet")
    End Sub

    Private Sub BtImprimAttrib_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtImprimAttrib.Click
        ViewAttrib.PrintReport()
    End Sub

    Private Sub BtImprimRejet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtImprimRejet.Click
        ViewRejet.PrintReport()
    End Sub

    Private Sub BtImprimOrdre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtImprimOrdre.Click
        ViewOrdreService.PrintReport()
    End Sub

    Dim dtDestin = New DataTable()
    Dim dtCarbone = New DataTable()
    Dim DrX As DataRow

    Private Sub BtEnvoiAttrib_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnvoiAttrib.Click
        ChargerCmbCarbone()
        RemplirDestinataires("Attribution")
        ChargerGridCarbonne("Attribution")
        Dialog_form(Notification_EnvoiMail)
    End Sub

    Private Sub BtEnvoiRejet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnvoiRejet.Click
        ChargerCmbCarbone()
        RemplirDestinataires("Rejet")
        ChargerGridCarbonne("Rejet")
        Dialog_form(Notification_EnvoiMail)
    End Sub

    Private Sub BtEnvoiOrdre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnvoiOrdre.Click
        ChargerCmbCarbone()
        RemplirDestinataires("Demarr")
        ChargerGridCarbonne("Demarr")
        Dialog_form(Notification_EnvoiMail)
    End Sub

    Public Sub ChargerGridCarbonne(ByVal TypeNotif As String)

        dtCarbone.Columns.Clear()

        dtCarbone.Columns.Add("Code", Type.GetType("System.String"))
        dtCarbone.Columns.Add("CodeOp", Type.GetType("System.String"))
        dtCarbone.Columns.Add("Destinataire", Type.GetType("System.String"))
        dtCarbone.Columns.Add("E-mail", Type.GetType("System.String"))
        dtCarbone.Columns.Add("Date d'envoi", Type.GetType("System.String"))
        dtCarbone.Columns.Add("N° Cel", Type.GetType("System.String"))

        dtCarbone.Rows.Clear()

        Dim Cpt As Decimal = 0
        query = "select N.CodeOperateur, N.CarboneEnvoye, O.FonctionOperateur, O.CiviliteOperateur, O.NomOperateur, O.PrenOperateur, O.TelOperateur, O.MailOperateur from T_NotifCarbone as N, T_Operateur as O where N.RefMarche='" & TxtCodeMarche.Text & "' and TypeNotif='" & TypeNotif & "' and N.CodeOperateur=O.CodeOperateur and O.CodeProjet='" & ProjetEnCours & "' order by O.FonctionOperateur"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows

            Cpt += 1
            Dim drS = dtCarbone.NewRow()

            Dim codeCC As String = rw(0).ToString
            While Len(codeCC) < 4
                codeCC = "0" & codeCC
            End While
            For k As Integer = 0 To Notification_EnvoiMail.CmbListeCarbone.Properties.Items.Count - 1
                If (Mid(Notification_EnvoiMail.CmbListeCarbone.Properties.Items(k).ToString, 2, 4) = codeCC) Then
                    Notification_EnvoiMail.CmbListeCarbone.Properties.Items.RemoveAt(k)
                End If
            Next

            Dim conj As String = ""
            If (Mid(rw(2).ToString, 1, 1).ToLower = "a" Or Mid(rw(2).ToString, 1, 1).ToLower = "e" Or Mid(rw(2).ToString, 1, 1).ToLower = "i" Or Mid(rw(2).ToString, 1, 1).ToLower = "u" Or Mid(rw(2).ToString, 1, 1).ToLower = "o") Then
                conj = "L'"
            ElseIf (rw(3).ToString = "M.") Then
                conj = "Le"
            Else
                conj = "La"
            End If

            Dim DateEnv As String = "-"
            If (rw(1).ToString <> "") Then
                DateEnv = Mid(rw(1).ToString, 1, 10)
            End If

            drS(0) = IIf(CDec(Cpt / 2) <> CDec(Cpt \ 2), "x", "").ToString
            drS(1) = rw(0).ToString
            drS(2) = MettreApost(rw(3).ToString & " " & conj & " " & rw(2).ToString & " (" & rw(4).ToString & " " & rw(5).ToString & ")")
            drS(3) = rw(7).ToString
            drS(4) = DateEnv
            drS(5) = rw(6).ToString

            dtCarbone.Rows.Add(drS)
        Next

        Notification_EnvoiMail.GridCarbone.DataSource = dtCarbone

        Notification_EnvoiMail.ViewCarbone.Columns(0).Visible = False
        Notification_EnvoiMail.ViewCarbone.Columns(1).Visible = False
        Notification_EnvoiMail.ViewCarbone.Columns(2).Width = 350
        Notification_EnvoiMail.ViewCarbone.Columns(3).Width = 350
        Notification_EnvoiMail.ViewCarbone.Columns(4).Width = 100
        Notification_EnvoiMail.ViewCarbone.Columns(5).Visible = False

        Notification_EnvoiMail.ViewCarbone.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        Notification_EnvoiMail.ViewCarbone.Appearance.Row.Font = New Font("Times New Roman", 12, FontStyle.Regular)

        ColorRowGrid(Notification_EnvoiMail.ViewCarbone, "[Code]='x'", Color.LightGray, "Times New Roman", 12, FontStyle.Regular, Color.Black)
        ColorRowGridAnal(Notification_EnvoiMail.ViewCarbone, "[E-mail]=''", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.DarkGray, False)

    End Sub

    Public Sub RemplirDestinataires(ByVal TypeNotif As String)
        dtDestin.Columns.Clear()
        dtDestin.Columns.Add("Code", Type.GetType("System.String"))
        dtDestin.Columns.Add("CodeFournis", Type.GetType("System.String"))
        dtDestin.Columns.Add("Lot", Type.GetType("System.String"))
        dtDestin.Columns.Add("Destinataire", Type.GetType("System.String"))
        dtDestin.Columns.Add("E-mail", Type.GetType("System.String"))
        dtDestin.Columns.Add("Date d'envoi", Type.GetType("System.String"))
        dtDestin.Columns.Add("N° Cel", Type.GetType("System.String"))

        dtDestin.Rows.Clear()
        Dim Cpt As Decimal = 0
        query = "select F.CodeFournis, S.CodeLot, F.NomFournis, F.MailFournis, F.CelFournis, F.PaysFournis from T_Fournisseur as F, T_SoumissionFournisseur as S, T_DAO as D, T_Marche as M where F.CodeFournis=S.CodeFournis and F.NumeroDAO=D.NumeroDAO and D.NumeroDAO=M.NumeroDAO and S.Attribue" & IIf(TypeNotif = "Rejet", "<>'OUI'", "='OUI'").ToString & " and M.RefMarche='" & TxtCodeMarche.Text & "' and M.CodeProjet='" & ProjetEnCours & "' order by S.CodeLot, F.NomFournis"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows()

            Cpt += 1
            Dim drS = dtDestin.NewRow()

            drS(0) = IIf(CDec(Cpt / 2) <> CDec(Cpt \ 2), "x", "").ToString
            drS(1) = rw(0).ToString
            drS(2) = rw(1).ToString
            drS(3) = MettreApost(rw(2).ToString & " (" & rw(5).ToString & ")")
            drS(4) = rw(3).ToString
            drS(5) = DateEnvoi(TypeNotif, TxtCodeMarche.Text, rw(1).ToString, rw(0).ToString)
            drS(6) = rw(4).ToString

            dtDestin.Rows.Add(drS)

        Next


        Notification_EnvoiMail.GridDestinataire.DataSource = dtDestin

        Notification_EnvoiMail.ViewDestinataire.Columns(0).Visible = False
        Notification_EnvoiMail.ViewDestinataire.Columns(1).Visible = False
        Notification_EnvoiMail.ViewDestinataire.Columns(2).Width = 30
        Notification_EnvoiMail.ViewDestinataire.Columns(3).Width = 320
        Notification_EnvoiMail.ViewDestinataire.Columns(4).Width = 350
        Notification_EnvoiMail.ViewDestinataire.Columns(5).Width = 100
        Notification_EnvoiMail.ViewDestinataire.Columns(6).Visible = False

        Notification_EnvoiMail.ViewDestinataire.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        Notification_EnvoiMail.ViewDestinataire.Appearance.Row.Font = New Font("Times New Roman", 12, FontStyle.Regular)

        ColorRowGrid(Notification_EnvoiMail.ViewDestinataire, "[Code]='x'", Color.LightGray, "Times New Roman", 12, FontStyle.Regular, Color.Black)
        ColorRowGridAnal(Notification_EnvoiMail.ViewDestinataire, "[Date d'envoi]='-'", Color.LightGray, "Times New Roman", 10, FontStyle.Bold, Color.Black, False)

    End Sub

    Private Function DateEnvoi(ByVal Notif As String, ByVal Marche As String, ByVal Lot As String, ByVal Fournis As String) As String

        Dim laDate As String = "-"

        'Dim Reader As MySqlDataReader
        Dim laReq As String = ""
        If (Notif = "Attribution") Then
            laReq = "select NotifAttribEnvoye from T_NotifAttrib where RefMarche='" & Marche & "' and CodeLot='" & Lot & "' and CodeFournis='" & Fournis & "'"
        ElseIf (Notif = "Rejet") Then
            laReq = "select NotifRejetEnvoye from T_NotifRejet where RefMarche='" & Marche & "' and CodeLot='" & Lot & "' and CodeFournis='" & Fournis & "'"
        Else
            laReq = "select NotifDemarrEnvoye from T_NotifDemarr where RefMarche='" & Marche & "' and CodeLot='" & Lot & "' and CodeFournis='" & Fournis & "'"
        End If
        query = laReq

        Dim dt As DataTable = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            laDate = Mid(dt.Rows(0).Item(0).ToString, 1, 10)
        End If
        Return laDate
    End Function

    Private Sub ChargerCmbCarbone()
        query = "select CodeOperateur, CiviliteOperateur, FonctionOperateur, TelOperateur from T_Operateur where EMP_ID='0' and CodeProjet='" & ProjetEnCours & "' order by FonctionOperateur"
        Notification_EnvoiMail.CmbListeCarbone.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Dim code As String = rw(0).ToString
            While Len(code) < 4
                code = "0" & code
            End While
            Notification_EnvoiMail.CmbListeCarbone.Properties.Items.Add("|" & code & "|  " & MettreApost(rw(2).ToString))
        Next
    End Sub

End Class