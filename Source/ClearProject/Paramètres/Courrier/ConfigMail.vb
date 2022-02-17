Imports AxEmail
Imports MySql.Data.MySqlClient

Public Class ConfigMail

    Dim objPop3Server As Pop3 = New Pop3()
    Dim objConstants As AxEmail.Constants = New AxEmail.Constants()

    Private Sub ConfigMail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        ChargerHote()
        HoteProjet()

    End Sub

    Private Sub ChargerHote()

        query = "select Serv_Nom from T_ParamMailServeur order by Serv_Nom"
        CmbNom.Properties.Items.Clear()
        Dim dt = ExcecuteSelectQuery(query)
        For Each rwx As DataRow In dt.Rows
            CmbNom.Properties.Items.Add(rwx(0).ToString)
        Next

    End Sub

    Private Sub HoteProjet()

        query = "select Serv_Nom, Mail_Account, Mail_PassWord from T_ParamTechProjet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt = ExcecuteSelectQuery(query)
        If dt.rows.count > 0 Then
            For Each rwx As DataRow In dt.Rows
                CmbNom.Text = rwx(0).ToString
                TxtCompte.Text = rwx(1).ToString
                TxtPasse.Text = rwx(2).ToString
            Next

        Else
            CmbNom.Text = ""
            TxtCompte.Text = ""
            TxtPasse.Text = ""
        End If

    End Sub

    Private Sub CmbNom_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNom.SelectedValueChanged

        'Dim Reader As MySqlDataReader
        query = "select Serv_Hote, Serv_Port, Serv_Secur, Serv_Authent, Serv_PortPop, Serv_Pop3 from T_ParamMailServeur where Serv_Nom='" & CmbNom.Text & "'"
        Dim dt = ExcecuteSelectQuery(query)
        If dt.rows.count > 0 Then

            For Each rwx As DataRow In dt.Rows

                TxtHote.Text = rwx(0).ToString
                TxtPort.Text = rwx(1).ToString
                TxtPortPop3.Text = rwx(4).ToString
                TxtHotePop3.Text = rwx(5).ToString
                ChkSecure.Checked = IIf(rwx(2).ToString = "O", True, False)
                ChkAuthent.Checked = IIf(rwx(3).ToString = "O", True, False)
            Next

        Else

            TxtHote.Text = ""
            TxtPort.Text = ""
            TxtPortPop3.Text = ""
            ChkSecure.Checked = False
            ChkAuthent.Checked = False
            TxtCompte.Text = ""
            TxtPasse.Text = ""

        End If

    End Sub

    Private Sub ChkAuthent_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAuthent.CheckedChanged

        If (ChkAuthent.Checked = True) Then
            PnlCompte.Enabled = True
        Else
            PnlCompte.Enabled = False
        End If

    End Sub

    Private Sub BtTestConn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtTestConn.Click

        objPop3Server.Authentication = objConstants.POP3_AUTH_AUTO

        ' Set Secure if secure communications is required
        If (ChkSecure.Checked) Then
            objPop3Server.SetSecure(Int32.Parse(TxtPortPop3.Text))
        Else
            objPop3Server.HostPort = Int32.Parse(TxtPortPop3.Text)
        End If

        If (GetResult() = 0) Then
            ' Connects to the POP3 server
            objPop3Server.Connect(TxtHotePop3.Text, TxtCompte.Text, TxtPasse.Text)
        End If

        If (BtEnreg.Enabled = False) Then
            If (GetResult() = 0) Then
                objPop3Server.Disconnect()
                MsgBox("Connexion réussie!", MsgBoxStyle.Information)
                BtEnreg.Enabled = True
                BtTestConn.Enabled = False
            Else
                MsgBox("Connexion échouée!", MsgBoxStyle.Critical)
                BtEnreg.Enabled = False
                BtTestConn.Enabled = True
            End If
        End If

    End Sub

    Function GetResult()

        Dim lResult As Decimal = 0

        lResult = objPop3Server.LastError

        Dim Resultat As String = lResult.ToString() + ": " + objPop3Server.GetErrorDescription(objPop3Server.LastError)
        Dim Reponse As String = objPop3Server.LastPop3Response

        If (lResult <> 0) Then
            MsgBox(Resultat & vbNewLine & Reponse, MsgBoxStyle.Critical)
        End If

        Return lResult
    End Function

    Private Sub BtEnreg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnreg.Click

        BtTestConn_Click(Me, e)
        If (GetResult() = 0) Then
            objPop3Server.Disconnect()
        Else
            MsgBox("Paramètres incorrects!", MsgBoxStyle.Critical)
            BtEnreg.Enabled = False
            BtTestConn.Enabled = True
            Exit Sub
        End If

        'Maintenant on passe à l'enregistrement **************
        query = "update T_ParamTechProjet set Serv_Nom='" + CmbNom.Text + "',Mail_Account='" + TxtCompte.Text + "',Mail_PassWord='" + TxtPasse.Text + "' where CodeProjet='" & ProjetEnCours & "'"
        ExecuteNonQuery(query)

        query = "update T_ParamMailServeur set Serv_Port='" + TxtPort.Text + "',Serv_PortPop='" + TxtPortPop3.Text + "',Serv_Secur='" + IIf(ChkSecure.Checked = True, "O", "N").ToString + "',Serv_Authent='" + IIf(ChkAuthent.Checked = True, "O", "N").ToString + "' where Serv_Nom='" & CmbNom.Text & "'"
        ExecuteNonQuery(query)

        MsgBox("Votre configuration e-mail a bien été enregistrée." & vbNewLine & vbNewLine & "Les nouveaux paramètres seront pris en compte au prochain demarrage de ClearProject", MsgBoxStyle.Information)
        Me.Close()

    End Sub
End Class