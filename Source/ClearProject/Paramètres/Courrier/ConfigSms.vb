Imports AxSms
Imports System.IO
Imports MySql.Data.MySqlClient

Public Class ConfigSms

    Dim objGsm As AxSms.Gsm = New AxSms.Gsm
    Dim objSmsConstants As AxSms.Constants = New AxSms.Constants

    Public Sub New()
        InitializeComponent()

        objGsm = New AxSms.Gsm()
        objSmsConstants = New AxSms.Constants()

    End Sub

    Private Sub ConfigSms_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        ChargerCmb()
        ChkAffichePin.Checked = False
        PnlConfig.Enabled = True
        BtEnreg.Enabled = False
        BtTestTerminal.Enabled = True
        TxtNumTest.Text = ""
        infosProjet()

    End Sub

    Private Sub infosProjet()

        query = "select Sms_Terminal, Sms_Isdn, Sms_Imei, Sms_Modele, Sms_Vitesse, Sms_Pin, Sms_Encodage from T_ParamTechProjet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt = ExcecuteSelectQuery(query)
        If dt.rows.count > 0 Then

            For Each rwx As DataRow In dt.Rows

                If (rwx(0).ToString <> "") Then CmbTerminal.Text = rwx(0).ToString
                If (rwx(1).ToString <> "") Then TxtNumTerminal.Text = rwx(1).ToString
                If (rwx(3).ToString <> "") Then TxtNomTerminal.Text = rwx(3).ToString
                If (rwx(2).ToString <> "") Then TxtNumSerie.Text = rwx(2).ToString
                If (rwx(4).ToString <> "") Then CmbVitesse.Text = rwx(4).ToString
                If (rwx(5).ToString <> "") Then TxtCodePin.Text = rwx(5).ToString

                If (rwx(6).ToString <> "") Then
                    If (CInt(rwx(6).ToString) = objSmsConstants.DATACODING_DEFAULT) Then CmbEncodage.Text = "Text"
                    If (CInt(rwx(6).ToString) = objSmsConstants.DATACODING_UNICODE) Then CmbEncodage.Text = "Unicode"
                    If (CInt(rwx(6).ToString) = objSmsConstants.DATACODING_8BIT_DATA) Then CmbEncodage.Text = "Data"
                    If (CInt(rwx(6).ToString) = objSmsConstants.DATACODING_FLASH) Then CmbEncodage.Text = "Flash"
                End If

            Next
        End If
       
    End Sub

    Private Sub ChargerCmb()
        ' Fill the devices combobox by autodetecting them. Adding COM ports
        ' and TAPI devices to the same list.
        CmbTerminal.Properties.Items.Clear()
        Dim strDevice As [String] = objGsm.FindFirstPort()
        While objGsm.LastError = 0
            CmbTerminal.Properties.Items.Add(strDevice)
            strDevice = objGsm.FindNextPort()
        End While
        'objSmsConstants.GSM_STATUS_MESSAGE_DELIVERED_SUCCESSFULLY
        strDevice = objGsm.FindFirstDevice()
        While objGsm.LastError = 0
            CmbTerminal.Properties.Items.Add(strDevice)
            strDevice = objGsm.FindNextDevice()
        End While
        'CmbTerminal.SelectedIndex = 0

        CmbVitesse.Properties.Items.Clear()
        CmbVitesse.Properties.Items.Add("Défaut")
        CmbVitesse.Properties.Items.Add(Convert.ToString(objSmsConstants.GSM_BAUDRATE_110))
        CmbVitesse.Properties.Items.Add(Convert.ToString(objSmsConstants.GSM_BAUDRATE_300))
        CmbVitesse.Properties.Items.Add(Convert.ToString(objSmsConstants.GSM_BAUDRATE_600))
        CmbVitesse.Properties.Items.Add(Convert.ToString(objSmsConstants.GSM_BAUDRATE_1200))
        CmbVitesse.Properties.Items.Add(Convert.ToString(objSmsConstants.GSM_BAUDRATE_2400))
        CmbVitesse.Properties.Items.Add(Convert.ToString(objSmsConstants.GSM_BAUDRATE_4800))
        CmbVitesse.Properties.Items.Add(Convert.ToString(objSmsConstants.GSM_BAUDRATE_9600))
        CmbVitesse.Properties.Items.Add(Convert.ToString(objSmsConstants.GSM_BAUDRATE_14400))
        CmbVitesse.Properties.Items.Add(Convert.ToString(objSmsConstants.GSM_BAUDRATE_19200))
        CmbVitesse.Properties.Items.Add(Convert.ToString(objSmsConstants.GSM_BAUDRATE_38400))
        CmbVitesse.Properties.Items.Add(Convert.ToString(objSmsConstants.GSM_BAUDRATE_56000))
        CmbVitesse.Properties.Items.Add(Convert.ToString(objSmsConstants.GSM_BAUDRATE_57600))
        CmbVitesse.Properties.Items.Add(Convert.ToString(objSmsConstants.GSM_BAUDRATE_64000))
        CmbVitesse.Properties.Items.Add(Convert.ToString(objSmsConstants.GSM_BAUDRATE_115200))
        CmbVitesse.Properties.Items.Add(Convert.ToString(objSmsConstants.GSM_BAUDRATE_128000))
        CmbVitesse.Properties.Items.Add(Convert.ToString(objSmsConstants.GSM_BAUDRATE_230400))
        CmbVitesse.Properties.Items.Add(Convert.ToString(objSmsConstants.GSM_BAUDRATE_256000))
        CmbVitesse.Properties.Items.Add(Convert.ToString(objSmsConstants.GSM_BAUDRATE_460800))
        CmbVitesse.Properties.Items.Add(Convert.ToString(objSmsConstants.GSM_BAUDRATE_921600))
        CmbVitesse.SelectedIndex = 0

        CmbEncodage.Properties.Items.Clear()
        CmbEncodage.Properties.Items.Add("Text")
        CmbEncodage.Properties.Items.Add("Unicode")
        CmbEncodage.Properties.Items.Add("Data")
        CmbEncodage.Properties.Items.Add("Flash")
        CmbEncodage.SelectedIndex = 0

    End Sub

    Private Function Encodage(ByRef Objet As AxSms.Constants, ByVal Code As String) As Decimal

        If (Code = "Text") Then Return Objet.DATACODING_DEFAULT
        If (Code = "Unicode") Then Return Objet.DATACODING_UNICODE
        If (Code = "Data") Then Return Objet.DATACODING_8BIT_DATA
        If (Code = "Flash") Then Return Objet.DATACODING_FLASH
        Return Objet.DATACODING_DEFAULT

    End Function

    Private Sub ChkAffichePin_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAffichePin.CheckedChanged

        If (ChkAffichePin.Checked = True) Then
            TxtCodePin.Properties.UseSystemPasswordChar = False
        Else
            TxtCodePin.Properties.UseSystemPasswordChar = True
        End If

    End Sub

    Private Sub BtTestTerminal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtTestTerminal.Click

        'DebutChargement(True, "Recherche ISDN du terminal et envoi sms test en cours...")
        Cursor = Cursors.WaitCursor

        Dim strResponse As String = String.Empty
        Dim strCommand As String = String.Empty
        Dim strFields As String()

        Dim vitesse As Decimal = objSmsConstants.GSM_BAUDRATE_DEFAULT
        If (CmbVitesse.Text <> "Défaut" And CmbVitesse.Text <> "") Then vitesse = CInt(CmbVitesse.Text)

        strCommand = String.Format("AT+CUSD=1," & Chr(34) & "{0}" & Chr(34) & ",15", "#99#")

        objGsm.Open(CmbTerminal.SelectedItem.ToString(), TxtCodePin.Text, vitesse)

        ' If there was a problem BDOPENing the GSM device there's no use in trying to continue.
        If objGsm.LastError <> 0 Then
            Cursor = Cursors.Default
            MsgBox("Connexion au terminal a échouée!", MsgBoxStyle.Critical)
            Return
        End If

        ' Sends the USSD Command though the selected GSM Modem
        objGsm.SendCommand(strCommand)

        ' Reads the response from the GSM Modem
        If (objGsm.LastError = 0) Then
            strResponse = objGsm.ReadResponse(10000)
        End If

        If (objGsm.LastError = 0) Then
            If (strResponse.Contains("OK")) Then ' Response should be OK
                objGsm.SendCommand(String.Empty)
                strResponse = objGsm.ReadResponse(10000)

                If (objGsm.LastError <> 0) Then
                    Cursor = Cursors.Default
                    MsgBox(String.Format("{0}: {1}", objGsm.LastError, objGsm.GetErrorDescription(objGsm.LastError)), MsgBoxStyle.Exclamation)
                    Return
                End If

                If (strResponse.Contains("+CUSD:")) Then

                    strFields = strResponse.Split(Char.Parse(Chr(34)))

                    If (strFields.Length > 1) Then
                        strResponse = strFields(1)
                    Else
                        strResponse = strFields(0)
                    End If
                End If
            End If
        End If


        TxtNumTerminal.Text = Mid(strResponse, 19, Len(strResponse) - 23)
        TxtNomTerminal.Text = Mid(objGsm.Manufacturer.ToString(), 8).ToUpper & " " & Mid(objGsm.Model.ToString, 8)
        TxtNumSerie.Text = objGsm.SerialNr.ToString
        'DebutChargement(True, "Envoi du message test en cours...")


        ' Create a new SMS message and configure it for sending.
        Dim objSms As New AxSms.Message()
        objSms.ToAddress = TxtNumTest.Text
        objSms.DataCoding = Encodage(objSmsConstants, CmbEncodage.Text)
        objSms.Body = "Bonjour," & vbNewLine & "Ce message vous est envoyé automatiquement par le " & ProjetEnCours & " pour un test de connexion sms à partir notre système de gestion." & vbNewLine & vbNewLine & "Bien à vous." & vbNewLine & "ClearProject."

        ' Set the SMS properties from the advanced dialog            
        objSms.BodyFormat = objSmsConstants.BODYFORMAT_TEXT

        objSms.ToAddressTON = objSmsConstants.TON_UNKNOWN
        objSms.ToAddressNPI = objSmsConstants.NPI_UNKNOWN

        objSms.RequestDeliveryReport = True
        objSms.HasUdh = False

        Dim strReference As [String] = objGsm.SendSms(objSms, objSmsConstants.MULTIPART_ACCEPT, 0)

        ' There was a problem sending the SMS message return early and don't add the 
        ' message to the listbox.
        If objGsm.LastError <> 0 Then
            ' Close the GSM object.
            objGsm.Close()
            PnlConfig.Enabled = True
            BtEnreg.Enabled = False
            BtTestTerminal.Enabled = True
            Cursor = Cursors.Default
            MsgBox("Echec envoi message!" & vbNewLine & "Veuillez vérifier la configuration du terminal.", MsgBoxStyle.Critical)
            Return
        End If

        objGsm.Close()
        Cursor = Cursors.Default
        MsgBox("Connexion au terminal réussie!", MsgBoxStyle.Information)
        PnlConfig.Enabled = False
        BtEnreg.Enabled = True
        BtTestTerminal.Enabled = False

    End Sub

    Private Sub CmbTerminal_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbTerminal.SelectedValueChanged

        DebutChargement()
        TxtNomTerminal.Text = ""
        TxtNumSerie.Text = ""
        If (CmbTerminal.Text <> "") Then
            objGsm.Open(CmbTerminal.Text)
            If objGsm.LastError = 0 Then
                TxtNomTerminal.Text = Mid(objGsm.Manufacturer.ToString(), 8).ToUpper & " " & Mid(objGsm.Model.ToString, 8)
                TxtNumSerie.Text = objGsm.SerialNr.ToString
                objGsm.Close()
                FinChargement()
            Else
                FinChargement()
                MsgBox("Appareil introuvable ou Accès réfusé!", MsgBoxStyle.Information, "Info GSM")
                CmbTerminal.Text = ""
            End If
        Else
            FinChargement()
        End If
        'objGsm.Sleep(1000)


    End Sub

    Private Sub TxtNumTerminal_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNumTerminal.GotFocus
        BtTestTerminal.Focus()
    End Sub

    Private Sub BtTestTerminal_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtTestTerminal.EnabledChanged
        TxtNumTest.Enabled = BtTestTerminal.Enabled
    End Sub

    Private Sub BtEnreg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnreg.Click

        'Maintenant on passe à l'enregistrement **************

        query = "update T_ParamTechProjet set Sms_Terminal='" + CmbTerminal.Text + "', Sms_Isdn='" + TxtNumTerminal.Text + "', Sms_Imei='" + TxtNumSerie.Text + "', Sms_Modele='" + TxtNomTerminal.Text + "', Sms_Vitesse='" + CmbVitesse.Text + "', Sms_Pin='" + TxtCodePin.Text + "', Sms_Encodage='" + Encodage(objSmsConstants, CmbEncodage.Text) + "' where CodeProjet='" & ProjetEnCours & "'"
        ExecuteNonQuery(query)

        MsgBox("Terminal configuré avec succès!" & vbNewLine & vbNewLine & "Les nouveaux paramètres seront pris en compte au prochain demarrage de ClearProject", MsgBoxStyle.Information)
        Me.Close()

    End Sub
End Class