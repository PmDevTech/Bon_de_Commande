Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.Text
Imports System.Runtime.InteropServices
Imports MySql.Data
Imports DevExpress.XtraSplashScreen
Imports System.IO

Public Class ParametreConnexion

    '<DllImport("odbc32.dll")> _
    'Private Shared Function SQLAllocHandle(ByVal hType As Short, ByVal inputHandle As IntPtr, ByRef outputHandle As IntPtr) As Short
    'End Function
    '
    '<DllImport("odbc32.dll")> _
    'Private Shared Function SQLSetEnvAttr(ByVal henv As IntPtr, ByVal attribute As Decimal, ByVal valuePtr As IntPtr, ByVal strLength As Decimal) As Short
    'End Function
    '
    '<DllImport("odbc32.dll")> _
    'Private Shared Function SQLFreeHandle(ByVal hType As Short, ByVal handle As IntPtr) As Short
    'End Function
    '
    '<DllImport("odbc32.dll")> _
    'Private Shared Function SQLBrowseConnect(ByVal hconn As IntPtr, ByVal inString As StringBuilder, ByVal inStringLength As Short, ByVal outString As StringBuilder, ByVal outStringLength As Short, ByRef outLengthNeeded As Short) As Short
    'End Function
    'Private Const SQL_HANDLE_ENV As Short = 1
    'Private Const SQL_HANDLE_DBC As Short = 2
    'Private Const SQL_ATTR_ODBC_VERSION As Decimal = 200
    'Private Const SQL_OV_ODBC3 As Decimal = 3
    'Private Const SQL_SUCCESS As Short = 0
    'Private Const SQL_NEED_DATA As Short = 99
    'Private Const DEFAULT_RESULT_SIZE As Short = 1024
    'Private Const SQL_DRIVER_STR As String = "DRIVER=SQL SERVER"

    Private Sub ParametreConnexion_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim liste_serveur As String() = {}
        'Dim txt As String = String.Empty
        'Dim henv As IntPtr = IntPtr.Zero
        'Dim hconn As IntPtr = IntPtr.Zero
        'Dim inString As StringBuilder = New StringBuilder(SQL_DRIVER_STR)
        'Dim outString As StringBuilder = New StringBuilder(DEFAULT_RESULT_SIZE)
        'Dim inStringLength As Short = CType(inString.Length, Short)
        'Dim lenNeeded As Short = 0
        'Try
        '    If SQL_SUCCESS = SQLAllocHandle(SQL_HANDLE_ENV, henv, henv) Then
        '        If SQL_SUCCESS = SQLSetEnvAttr(henv, SQL_ATTR_ODBC_VERSION, New IntPtr(SQL_OV_ODBC3), 0) Then
        '            If SQL_SUCCESS = SQLAllocHandle(SQL_HANDLE_DBC, henv, hconn) Then
        '                If SQL_NEED_DATA = SQLBrowseConnect(hconn, inString, inStringLength, outString, DEFAULT_RESULT_SIZE, lenNeeded) Then
        '                    If DEFAULT_RESULT_SIZE < lenNeeded Then
        '                        outString.Capacity = lenNeeded
        '                        If Not (SQL_NEED_DATA = SQLBrowseConnect(hconn, inString, inStringLength, outString, lenNeeded, lenNeeded)) Then
        '                            Throw New ApplicationException("Unabled to aquirequeryServers from ODBC driver.")
        '                        End If
        '                    End If
        '                    txt = outString.ToString
        '                    Dim start As Decimal = txt.IndexOf("{") + 1
        '                    Dim len As Decimal = txt.IndexOf("}") - start
        '                    If (start > 0) AndAlso (len > 0) Then
        '                        txt = txt.Substring(start, len)
        '                    Else
        '                        txt = String.Empty
        '                    End If
        '                End If
        '            End If
        '        End If
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString())
        '    txt = String.Empty
        'Finally
        '    If Not (hconn.Equals(IntPtr.Zero)) Then
        '        SQLFreeHandle(SQL_HANDLE_DBC, hconn)
        '    End If
        '    If Not (henv.Equals(IntPtr.Zero)) Then
        '        SQLFreeHandle(SQL_HANDLE_ENV, hconn)
        '    End If
        'End Try
        'If txt.Length > 0 Then
        '    liste_serveur = txt.Split(",".ToCharArray)
        'End If
        'Dim serveur As String
        'For Each serveur In liste_serveur
        '    'MessageBox.Show(serveur)
        '    'Dim ItemServer As ListViewItem = New ListViewItem(serveur)
        '    CmbServeur.Properties.Items.Add(serveur.ToString)
        '    'ItemServer = Nothing
        'Next
    End Sub

    Private Sub BtQuitter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        'ReponseDialog = "xxx"
        If MessageBox.Show("Voulez-vous quitter ClearProject?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub BtTester_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtTester.Click
        Me.Cursor = Cursors.WaitCursor
        If (CmbServeur.Text = "") Then
            MsgBox("Serveur : saisie obligatoire!", MsgBoxStyle.Information, "Serveur")
            Exit Sub
        End If
        If (TxtBd.Text = "") Then
            MsgBox("Base de Données : saisie obligatoire!", MsgBoxStyle.Information, "BD")
            Exit Sub
        End If
        If (TxtUtil.Text = "") Then
            MsgBox("Utilisateur : saisie obligatoire!", MsgBoxStyle.Information, "Compte")
            Exit Sub
        End If
        If (TxtMdp.Text = "") Then
            MsgBox("Mot de passe : saisie obligatoire!", MsgBoxStyle.Information, "Compte")
            Exit Sub
        End If
        Try
            ParaCon()
            BDOPEN1(CmbServeur.Text, TxtBd.Text, TxtPort.Text, TxtUtil.Text, TxtMdp.Text)
        Catch ex As Exception
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
            LblConnFalse.Visible = True
            LblConnOk.Visible = False
            BtEnregistrer.Enabled = False
            Me.Cursor = Cursors.Default
            Exit Sub
        End Try
        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
        LblConnFalse.Visible = False
        LblConnOk.Visible = True
        BtEnregistrer.Enabled = True
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub BtEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click
        Dim SW As StreamWriter = File.CreateText(Application.StartupPath & "\Cnx") ' crée ou si existe écrase
        SW.WriteLine("server=" & CmbServeur.Text)
        SW.WriteLine("DB=" & TxtBd.Text)
        SW.WriteLine("PORT=" & TxtPort.Text)
        SW.WriteLine("USERNAME=" & TxtUtil.Text)
        SW.WriteLine("PWD=" & TxtMdp.Text)
        SW.WriteLine("ODBCNAME=ClearODBC")
        SW.Close()
        ReponseDialog = ""
        
        Me.Close()
    End Sub
End Class
'End Namespace