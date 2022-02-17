Imports MySql.Data.MySqlClient
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine

Public Class Notification_EnvoiMail

    Private Sub Notification_EnvoiMail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        CmbListeCarbone.Text = ""
    End Sub

    Private Sub BtAjoutCc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtAjoutCc.Click

        If (CmbListeCarbone.Text <> "") Then
            Dim DatSet = New DataSet
            query = "select * from T_NotifCarbone"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)

            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_NotifCarbone")
            Dim DatTable = DatSet.Tables("T_NotifCarbone")
            Dim DatRow = DatSet.Tables("T_NotifCarbone").NewRow()

            DatRow("RefMarche") = NotificationAttributionRejet.TxtCodeMarche.Text
            DatRow("TypeNotif") = IIf(NotificationAttributionRejet.XtraTabControl1.SelectedTabPage Is NotificationAttributionRejet.PageAttribution, "Attribution", "Rejet").ToString
            DatRow("CodeOperateur") = CInt(Mid(CmbListeCarbone.Text, 2, 4))
            DatRow("CarboneEnvoye") = ""

            DatSet.Tables("T_NotifCarbone").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_NotifCarbone")
            DatSet.Clear()

            BDQUIT(sqlconn)
            CmbListeCarbone.Text = ""

            NotificationAttributionRejet.ChargerGridCarbonne(IIf(NotificationAttributionRejet.XtraTabControl1.SelectedTabPage Is NotificationAttributionRejet.PageAttribution, "Attribution", "Rejet").ToString)

        Else
            MsgBox("....?")
        End If

    End Sub

    Private Sub BtEnvoyer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnvoyer.Click

        DebutChargement(True, "Envoi du mail en cours...")

        Dim AmpliCc As String = ""

        Dim CopieCarb As String = ""
        Dim CodeCarb(20) As String
        Dim nbCarb As Decimal = 0
        For k As Integer = 0 To ViewCarbone.RowCount - 1
            If (ViewCarbone.GetRow(k)(3).ToString <> "") Then
                CodeCarb(nbCarb) = ViewCarbone.GetRow(k)(1).ToString
                If (CopieCarb <> "") Then CopieCarb = CopieCarb & "|"
                CopieCarb = CopieCarb & ViewCarbone.GetRow(k)(3).ToString
                nbCarb += 1

                If (AmpliCc <> "") Then AmpliCc = AmpliCc & vbNewLine
                AmpliCc = " -" & ViewCarbone.GetRow(k)(2).ToString
            End If
        Next

        Dim DateEnvoi As String = ""
        Dim laNotif As String = IIf(NotificationAttributionRejet.XtraTabControl1.SelectedTabPage Is NotificationAttributionRejet.PageAttribution, "Attribution", "Rejet").ToString

        For k As Integer = 0 To ViewDestinataire.RowCount - 1
            If (ViewDestinataire.GetRow(k)(4).ToString <> "" And (ViewDestinataire.GetRow(k)(5).ToString = "-" Or ViewDestinataire.GetRow(k)(5).ToString = "")) Then

                query = "select S.CodeLot from T_Fournisseur as F, T_SoumissionFournisseur as S, T_DAO as D, T_Marche as M where F.CodeFournis=S.CodeFournis and S.CodeFournis='" & ViewDestinataire.GetRow(k)(1).ToString & "' and F.NumeroDAO=D.NumeroDAO and D.NumeroDAO=M.NumeroDAO and S.Attribue" & IIf(laNotif = "Attribution", "='OUI'", "<>'OUI'").ToString & " and M.RefMarche='" & NotificationAttributionRejet.TxtCodeMarche.Text & "' and M.CodeProjet='" & ProjetEnCours & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows

                    SortieFichierMail(laNotif, CInt(ViewDestinataire.GetRow(k)(1).ToString), rw(0).ToString, AmpliCc)

                    Dim leFichier As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\" & IIf(laNotif = "Attribution", "Notification d'Attribution.pdf", "Notification de rejet.pdf").ToString

                    If (File.Exists(leFichier) = True) Then

                       query= "DELETE from T_NotifAttrib where RefMarche='" & NotificationAttributionRejet.TxtCodeMarche.Text & "' and CodeLot='" & rw(0).ToString & "' and CodeFournis='" & ViewDestinataire.GetRow(k)(1).ToString & "' and RefConsult='0'"
                        ExecuteScallar(query)

                        Dim DatSet = New DataSet
                        query = "select * from T_NotifAttrib"
                        Dim sqlconn As New MySqlConnection
                        BDOPEN(sqlconn)

                        Dim Cmd = New MySqlCommand(query, sqlconn)
                        Dim DatAdapt = New MySqlDataAdapter(Cmd)
                        DatAdapt.Fill(DatSet, "T_NotifAttrib")
                        Dim DatTable = DatSet.Tables("T_NotifAttrib")
                        Dim DatRow = DatSet.Tables("T_NotifAttrib").NewRow()

                        DatRow("RefMarche") = NotificationAttributionRejet.TxtCodeMarche.Text
                        DatRow("CodeLot") = rw(0).ToString
                        DatRow("CodeFournis") = ViewDestinataire.GetRow(k)(1).ToString
                        DatRow("RefConsult") = 0
                        DateEnvoi = EnvoiMailDirect(ViewDestinataire.GetRow(k)(4).ToString, CopieCarb, "Notification " & ProjetEnCours & ".", leFichier)
                        DatRow("NotifAttribEnvoye") = DateEnvoi

                        DatSet.Tables("T_NotifAttrib").Rows.Add(DatRow)
                        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                        DatAdapt.Update(DatSet, "T_NotifAttrib")
                        DatSet.Clear()

                        BDQUIT(sqlconn)

                        File.Delete(leFichier)

                    End If

                Next


                NotificationAttributionRejet.RemplirDestinataires(laNotif)
            End If
        Next


        For k As Integer = 0 To nbCarb - 1

            Dim DatSet = New DataSet
            query = "select * from T_NotifCarbone where RefMarche='" & NotificationAttributionRejet.TxtCodeMarche.Text & "' and TypeNotif='" & laNotif & "' and CodeOperateur='" & CodeCarb(k) & "'"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)

            Dim Cmd = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Fill(DatSet, "T_NotifCarbone")

            DatSet.Tables!T_NotifCarbone.Rows(0)!CarboneEnvoye = DateEnvoi

            DatAdapt.Update(DatSet, "T_NotifCarbone")
            DatSet.Clear()
            BDQUIT(sqlconn)

            NotificationAttributionRejet.ChargerGridCarbonne(laNotif)
        Next


        FinChargement()

    End Sub

    Private Sub SortieFichierMail(ByVal typeNotif As String, ByVal CodeFournis As Decimal, ByVal codeLot As String, ByVal Ampli As String)

        Dim reportAttrib, reportRejet As New ReportDocument
        Dim Chemin As String = lineEtat & "\Marches\"

        Dim DatSet = New DataSet
        reportAttrib.Load(Chemin & IIf(typeNotif = "Attribution", "NotificationAttribution_aEnvoyer.rpt", IIf(typeNotif = "Rejet", "NotificationRejetOffre_aEnvoyer.rpt", "OrdreDeService_aEnvoyer.rpt").ToString).ToString)
        reportAttrib.SetDataSource(DatSet)
        reportAttrib.SetParameterValue("CodeProjet", ProjetEnCours)
        reportAttrib.SetParameterValue("CodeMarche", NotificationAttributionRejet.TxtCodeMarche.Text)
        If (typeNotif <> "Demarr") Then
            reportAttrib.SetParameterValue("Ampliation", Ampli)
            reportAttrib.SetParameterValue("CompteBanqueProjet", "Banque du Projet")
        End If
        reportAttrib.SetParameterValue("Fournis", CodeFournis)
        reportAttrib.SetParameterValue("LotN", codeLot)

        Dim CheminExport As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\" & IIf(typeNotif = "Attribution", "Notification d'Attribution.pdf", IIf(typeNotif = "Rejet", "Notification de rejet.pdf", "Ordre de Service.pdf").ToString).ToString

        If (CheminExport <> "") Then
            reportAttrib.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, CheminExport)
        End If

    End Sub
End Class