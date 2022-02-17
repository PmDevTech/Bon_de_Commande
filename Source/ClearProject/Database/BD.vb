Imports MySql.Data.MySqlClient
Module BD
    Public database As String
    Public SRV, NbCompo As String 'variable devant contenir le nom  du serveur 
    Public DB As String 'variable devant contenir le nom de la base de donnees
    Public PORT As String 'variable devant contenir le port
    Public USERNAME As String 'variable devant contenir le nom d'utilisateur
    Public PWD As String 'variable devant contenir le mot de passe de la Base de donnée
    Public ODBCNAME As String 'variable devant contenir le nom de l'ODBC

    Public SRV1 As String 'variable devant contenir le nom  du serveur enregistré
    Public DB1 As String 'variable devant contenir le nom de la base de donnees enregistré
    Public PORT1 As String 'variable devant contenir le port enregistré
    Public USERNAME1 As String 'variable devant contenir le nom d'utilisateur enregistré
    Public PWD1 As String 'variable devant contenir le mot de passe de la Base de donnée enregistré
    Public ODBCNAME1 As String 'variable devant contenir le nom de l'ODBC enregistré
    Public Sub ParaCon()
        Dim Line As String
        SRV = ""
        DB = ""
        PORT = ""
        USERNAME = ""
        PWD = ""
        ODBCNAME = ""

        FileOpen(1, Application.StartupPath & "\Cnx", OpenMode.Input)
        While Not EOF(1)
            Line = LineInput(1)

            If SRV = "" Then
                SRV = Line
            ElseIf DB = "" Then
                DB = Line
            ElseIf PORT = "" Then
                PORT = Line
            ElseIf USERNAME = "" Then
                USERNAME = Line
            ElseIf PWD = "" Then
                PWD = Line
            ElseIf ODBCNAME = "" Then
                ODBCNAME = Line
                Exit While
            End If
        End While

        FileClose(1)
        SRV = Mid(SRV, 8)
        DB = Mid(DB, 4)
        PORT = Mid(PORT, 6)
        USERNAME = Mid(USERNAME, 10)
        ODBCNAME = Mid(ODBCNAME, 10)
        PWD = Mid(PWD, 5)

        'Sauvegarder des variables
        SRV1 = SRV
        DB1 = DB
        PORT1 = PORT
        USERNAME1 = USERNAME
        ODBCNAME1 = ODBCNAME
        PWD1 = PWD
    End Sub

    Public Function BDOPEN(ByRef ConnectionVariable As MySqlConnection, Optional BDName As String = "") As Boolean
        If SRV.Length <> 0 Then
            Dim strConx As String
            If BDName = "" Then
                If DB.Length > 4 Then
                    If Mid(DB, 1, 4).ToUpper() = "LIKE" Then
                        strConx = "server=" & SRV & ";Port=" & PORT & ";User Id=" & USERNAME & ";Pwd=" & PWD & "; Pooling=False;"
                    Else
                        strConx = "server=" & SRV & ";Database=" & DB & ";Port=" & PORT & ";User Id=" & USERNAME & ";Pwd=" & PWD & "; Pooling=False;"
                    End If
                Else
                    strConx = "server=" & SRV & ";Database=" & DB & ";Port=" & PORT & ";User Id=" & USERNAME & ";Pwd=" & PWD & "; Pooling=False;"
                End If
            Else
                strConx = "server=" & SRV & ";Database=" & BDName & ";Port=" & PORT & ";User Id=" & USERNAME & ";Pwd=" & PWD & "; Pooling=False;"
            End If
            ConnectionVariable = New MySqlConnection(strConx)
            Try
                ConnectionVariable.Open()
                Return True
            Catch ex As Exception
                Return False
                FailMsg("Impossible de se connecter au serveur" & vbNewLine & vbNewLine & ex.ToString)
            End Try
        Else
            Return False
            FailMsg("Le serveur de base de données n'a pas été configuré")
        End If
    End Function
    Public Sub BDOPEN1(SRV As String, BD As String, PORT As String, UserID As String, Pwd As String)
        Dim strConx As String = "server=" & SRV & ";Database=" & BD & ";Port=" & PORT & ";User Id=" & UserID & ";Pwd=" & Pwd & ""
        Dim sqlconn = New MySqlConnection(strConx)
        sqlconn.ConnectionString = strConx
        Try
            sqlconn.Open()
            SRV = SRV
            SRV1 = SRV
            DB = BD
            DB1 = BD
            PORT = PORT
            PORT1 = PORT
            USERNAME = UserID
            USERNAME1 = UserID
            Pwd = Pwd
            PWD1 = Pwd
        Catch ex As Exception
            FailMsg("Impossible de se connecter au serveur")
        End Try
    End Sub

    Public Sub BDQUIT(ByRef ConnectionVariable As MySqlConnection)
        Try
            ConnectionVariable.Close()
        Catch ex As Exception
        End Try
    End Sub

    Public Function ExcecuteSelectQuery(ByVal sqlquery As String, Optional BDName As String = "") As DataTable
        Dim Connection As New MySqlConnection
        Try
            Dim ConResult As Boolean = BDOPEN(Connection, BDName)
            'While Not ConResult
            '    ConResult = BDOPEN(Connection)
            'End While
            Dim cmd As MySqlCommand = New MySqlCommand(sqlquery, Connection)
            Dim dt As New DataTable
            Dim Adapt As New MySqlDataAdapter(cmd)
            Adapt.Fill(dt)
            Connection.Close()
            Return dt

        Catch ex As MySqlException
            Throw ex
        End Try
    End Function

    Public Function ExecuteNonQuery(ByVal sqlquery As String) As Decimal
        Dim Connection As New MySqlConnection
        Try
            Dim ConResult As Boolean = BDOPEN(Connection)
            Dim cmd As MySqlCommand = New MySqlCommand(sqlquery, Connection)
            Dim res As Decimal = -1
            Try
                res = cmd.ExecuteNonQuery()
            Catch ex As Exception
                If ex.Message = "Connection must be valid and open." Then
                    Threading.Thread.Sleep(2500)
                    Connection = New MySqlConnection
                    ConResult = BDOPEN(Connection)
                    If ConResult = False Then
                        FailMsg("La communication avec le serveur de base de données a été interrompue.")
                    Else
                        cmd = New MySqlCommand(sqlquery, Connection)
                        res = -1
                        Try
                            res = cmd.ExecuteNonQuery()
                        Catch exp As Exception
                            'FailMsg("La communication avec le serveur de base de données a été interrompue.")
                        End Try
                    End If
                Else
                    Throw ex
                End If
            End Try
            Connection.Close()
            Return res
        Catch ex As MySqlException
            Throw ex
        End Try
    End Function

    Public Function ExecuteScallar(ByVal sqlquery As String) As String
        Dim Connection As New MySqlConnection
        Try
            Dim ConResult As Boolean = BDOPEN(Connection)
            'While Not ConResult
            '    ConResult = BDOPEN(Connection)
            'End While
            Dim cmd As MySqlCommand = New MySqlCommand(sqlquery, Connection)
            Dim res As String = String.Empty
            Try
                res = cmd.ExecuteScalar().ToString
            Catch null As NullReferenceException

            Catch ex As Exception
                If ex.Message = "Connection must be valid and open." Then
                    Threading.Thread.Sleep(2500)
                    Connection = New MySqlConnection
                    ConResult = BDOPEN(Connection)
                    If ConResult = False Then
                        FailMsg("La communication avec le serveur de base de données a été interrompue.")
                    Else
                        cmd = New MySqlCommand(sqlquery, Connection)
                        res = String.Empty
                        Try
                            res = cmd.ExecuteScalar().ToString
                        Catch nu As NullReferenceException

                        Catch exp As Exception
                            'FailMsg("La communication avec le serveur de base de données a été interrompue.")
                        End Try
                    End If
                Else
                    'InputBox(0, 1, sqlquery)
                    Throw ex
                    'FailMsg(ex.ToString)
                End If
            End Try
            Connection.Close()
            Return res
        Catch ex As MySqlException
            FailMsg(ex.ToString)
        End Try
        Return ""
    End Function

    Sub SearchTable2(ByVal matable As String, ByVal ChampMatable As String, ByVal mot As DevExpress.XtraEditors.TextEdit, ByVal dtgrid As DataGridView)

        dtgrid.Rows.Clear()
        Dim id_table As String = ""
        Dim lib_table As String = ""
        query = "select * from " & matable & " WHERE " & ChampMatable & " like '%" & EnleverApost(mot.Text) & "%' "
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            id_table = MettreApost(rw(0).ToString)
            lib_table = MettreApost(rw(1).ToString)
            dtgrid.Rows.Add(id_table, lib_table)
        Next

    End Sub

    Public Function MySqlDataAdapterr(ByVal query As String, ByVal matable As String) As Decimal
        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)
        Dim mcmd = New MySql.Data.MySqlClient.MySqlCommand(query, sqlconn)
        sd = New MySql.Data.MySqlClient.MySqlDataAdapter(mcmd)
        sd.Fill(dts, matable)
        dt = dts.Tables(matable)
        BDQUIT(sqlconn)
    End Function

    Function SearchTable(ByVal ColonneMatable As String, ByVal matable As String, ByVal ChampMatable As String, ByVal mot As String)

        Dim id_table As String = ""
       query= "select " & ColonneMatable & " from " & matable & " WHERE " & ChampMatable & " like '%" & mot & "%' "
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            id_table = rw(0).ToString
        Next
        Return id_table

    End Function

    Function SearchTable1(ByVal ColonneMatable As String, ByVal matable As String, ByVal ChampMatable As String, ByVal mot As String)

        Dim id_table As String = ""
       query= "select " & ColonneMatable & " from " & matable & " WHERE " & ChampMatable & " like '%" & mot & "%' "
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            id_table = rw(0).ToString
        Next
        Return id_table

    End Function

    Function SearchTable2(ByVal ColonneMatable As String, ByVal matable As String, ByVal ChampMatable As String, ByVal mot As String)

        Dim id_table As String = ""
        query = "select " & ColonneMatable & " from " & matable & " WHERE " & ChampMatable & " = '" & mot & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            id_table = rw(0).ToString
        Next
        Return id_table

    End Function
    Function SearchTable4(ByVal ColonneMatable As String, ByVal matable As String, ByVal ChampMatable As String, ByVal mot As String)

        query = "select " & ColonneMatable & " from " & matable & " WHERE " & ChampMatable & " like '%" & mot & "%' "
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            id_table2 = rw(0).ToString
        Next
        Return id_table2

    End Function
    Public Function ExecuteNonQueryServer(ByVal query As String, DB As String, Optional PublicUser As Boolean = True) As Integer
        Dim Connection As New MySqlConnection
        If DB <> "" Then
            ConnecteServer(Connection, DB, PublicUser)
        Else
            ConnecteServer(Connection)
        End If
        Try
            Dim cmd As MySqlCommand = New MySqlCommand(query, Connection)
            Return cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ExecuteNonQueryHereBlob(query As String, DB As String, Parametre As String, Value As Byte()) As Integer
        Dim Connection As New MySqlConnection
        If DB <> "" Then
            ConnecteServer(Connection, DB, False)
        Else
            ConnecteServer(Connection)
        End If
        Try
            Dim cmd As MySqlCommand = New MySqlCommand(query, Connection)
            cmd.Parameters.AddWithValue(Parametre, Value)
            Dim res As Integer = cmd.ExecuteNonQuery()
            Connection.Close()
            Return res
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ConnecteServer(ByRef ConnectionVar As MySqlConnection, Optional PublicUser As Boolean = True, Optional DB As String = "projectmanagment") As Boolean
        Dim ConnectionString As String
        ConnectionString = "server=162.214.71.165;PORT=3306;Database=" & DB & ";User Id=pmAdmin;Pwd=Zx#35a9q;Pooling=False"
        'If PublicUser Then
        '    If DB = "projectmanagment" Then
        '        ConnectionString = "server=ClearProject.online;PORT=3306;Database=" & DB & ";User Id=pmAdmin;Pwd=Zx#35a9q;Pooling=False"
        '    Else
        '    End If
        'Else
        '    ConnectionString = "server=ClearProject.online;PORT=3306;Database=projectmanagment;User Id=pmAdmin;Pwd=Zx#35a9q;Pooling=False"
        'End If
        ConnectionVar = New MySqlConnection(ConnectionString)
        Try
            ConnectionVar.Open()
            Return True
        Catch ex As Exception
            Return False
            'FailMsg(ex.ToString)
        End Try
    End Function
End Module
