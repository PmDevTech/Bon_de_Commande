Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports MySql.Data.MySqlClient

Public Class Etat_EngBail

    Private Sub btimprim_Click(sender As System.Object, e As System.EventArgs) Handles btimprim.Click
        If CheckedComboBoxEdit1.SelectedIndex = -1 Then
            Exit Sub
        End If

        If CheckedComboBoxEdit1.Text = "Par Bailleur" Then
            'Bailersion de la date
            'Dim str(3) As String
            'str = dtd.Text.Split("/")
            'Dim tempdt As String = String.Empty
            'For j As Integer = 2 To 0 Step -1
            '    tempdt += str(j) & "-"
            'Next
            'tempdt = tempdt.Substring(0, 10)

            'Dim str1(3) As String
            'str1 = dtf.Text.Split("/")
            'Dim tempdt1 As String = String.Empty
            'For j As Integer = 2 To 0 Step -1
            '    tempdt1 += str1(j) & "-"
            'Next
            'tempdt1 = tempdt1.Substring(0, 10)

            'Dim clause As String = ""

            ''Requete Date
            'If DateTime.Compare(tempdt1, tempdt) >= 0 Then
            '    clause = "AND p.dateDebutPartition >='" & tempdt & "' AND p.dateFinPartition <='" & tempdt1 & "'"
            'Else
            '    SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
            '    Exit Sub
            'End If

            DebutChargement(True, "Recherche des informations demandées en cours...")

            'Chargement des Bailleurs
            query = "SELECT COUNT(*) FROM t_bailleur b WHERE CodeProjet='" & ProjetEnCours & "'"
            Dim BailCount As Decimal = Val(ExecuteScallar(query))
            query = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS  WHERE table_name = 't_tampbailleur' AND table_schema = '" & DB & "'"
            Dim OldBailCount As Decimal = Val(ExecuteScallar(query))
            If BailCount <> OldBailCount Then
                'Suppression des Bailleurs tamporaires
                If BailCount > 1 Then
                    Dim BailName As String = "Bail"
                    If OldBailCount > 1 Then
                        For i = 2 To OldBailCount
                            BailName = "Bail" & i
                            query = "ALTER TABLE `t_tampbailleur` DROP `" & BailName & "`;"
                            ExecuteNonQuery(query)
                        Next
                    End If

                    For i = 2 To BailCount
                        BailName = "Bail" & i
                        Dim Bailaftername As String = "Bail" & (i - 1)
                        query = "ALTER TABLE `t_tampbailleur` ADD `" & BailName & "` VARCHAR(255) NOT NULL AFTER `" & Bailaftername & "`"
                        ExecuteNonQuery(query)
                    Next
                ElseIf BailCount = 1 Then
                    Dim BailName As String = "Bail"
                    If OldBailCount > 1 Then
                        For i = 2 To OldBailCount
                            BailName = "Bail" & i
                            query = "ALTER TABLE `t_tampbailleur` DROP `" & BailName & "`;"
                            ExecuteNonQuery(query)
                        Next
                    End If
                End If
            End If
            'Insertion des Bailleurs
            query = "SELECT InitialeBailleur FROM t_bailleur WHERE CodeProjet='" & ProjetEnCours & "' ORDER BY InitialeBailleur ASC"
            Dim dtBailention As DataTable = ExcecuteSelectQuery(query)
            Dim BailString As String = String.Empty
            For Each rw As DataRow In dtBailention.Rows
                BailString &= "'" & rw("InitialeBailleur") & "',"
            Next
            BailString = Mid(BailString, 1, (BailString.Length - 1)) 'Enlever le dernier ';'

            ExecuteNonQuery("TRUNCATE t_tampbailleur") 'Vider la table
            query = "INSERT INTO t_tampbailleur VALUES(" & BailString & ")"
            ExecuteNonQuery(query)

            'Chargement des colonnes de la table tampon
            query = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS  WHERE table_name='t_tampficheactivitebailleur' AND table_schema = '" & DB & "' AND COLUMN_NAME='Bail'"
            If Val(ExecuteScallar(query)) > 0 Then 'Retire la colonne Bail de la table 't_tampficheactivite'
                query = "ALTER TABLE `t_tampficheactivitebailleur` DROP `Bail`;"
                ExecuteNonQuery(query)
            End If

            query = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS  WHERE table_name = 't_tampficheactivitebailleur' AND table_schema = '" & DB & "'"
            Dim OldTampCount As Decimal = Val(ExecuteScallar(query)) - 9

            If BailCount <> OldTampCount Then
                'Suppression des Bailleurs tamporaires
                If BailCount > 1 Then
                    Dim BailName As String = "MontBail"
                    If OldTampCount > 1 Then
                        For i = 2 To OldTampCount
                            BailName = "MontBail" & i
                            query = "ALTER TABLE `t_tampficheactivitebailleur` DROP `" & BailName & "`;"
                            ExecuteNonQuery(query)
                        Next
                    End If

                    For i = 2 To BailCount
                        BailName = "MontBail" & i
                        Dim Bailaftername As String = "MontBail" & (i - 1)
                        query = "ALTER TABLE `t_tampficheactivitebailleur` ADD `" & BailName & "` DECIMAL(20.0) NOT NULL DEFAULT 0 AFTER `" & Bailaftername & "`"
                        ExecuteNonQuery(query)
                    Next
                ElseIf BailCount = 1 Then
                    Dim BailName As String = "MontBail"
                    If OldTampCount > 1 Then
                        For i = 2 To OldTampCount
                            BailName = "MontBail" & i
                            query = "ALTER TABLE `t_tampficheactivitebailleur` DROP `" & BailName & "`;"
                            ExecuteNonQuery(query)
                        Next
                    End If
                End If
            End If

            ' Affichage état ***************************
            query = "SELECT COUNT(*) FROM t_convention c, t_bailleur b WHERE b.CodeBailleur=c.CodeBailleur AND b.CodeProjet='" & ProjetEnCours & "'"
            ' Dim ConvCount As Decimal = Val(ExecuteScallar(query))
            Dim EtatToLoad As String = lineEtat & "\EditionBudgetaire\FinancementBailleur" & BailCount & ".rpt"
            If IO.File.Exists(EtatToLoad) Then
                Dim DatSet = New DataSet
                query = "DELETE from T_TampFicheActiviteBailleur WHERE CodeUtils='" & SessionID & "' AND CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

                query = "Select b.RefBesoinPartition, b.NumeroComptable, b.LibelleBesoin, b.QteNature, b.UniteBesoin, b.PUNature from T_BesoinPartition b, T_Partition p where b.CodePartition=p.CodePartition " ' + clause
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    Dim LibelleCompte As String = ExecuteScallar("Select LIBELLE_SC FROM t_comp_sous_classe WHERE CODE_SC='" & rw("NumeroComptable") & "'")
                    Dim MontBailString As String = String.Empty
                    Dim Tamp As Double = 0
                    query = "SELECT CodeBailleur FROM t_bailleur WHERE CodeProjet='" & ProjetEnCours & "' ORDER BY InitialeBailleur ASC"
                    Dim dtBailleur As DataTable = ExcecuteSelectQuery(query)
                    Dim Count As Decimal = 0
                    For Each rwBail As DataRow In dtBailleur.Rows
                        Count += 1
                        query = "select MontantBailleur from T_RepartitionParBailleur where CodeBailleur='" & rwBail("CodeBailleur") & "' and RefBesoinPartition='" & rw("RefBesoinPartition").ToString & "'"
                        Dim dtRepartition As DataTable = ExcecuteSelectQuery(query)
                        If dtRepartition.Rows.Count > 0 Then
                            MontBailString &= "'" & CDec(dtRepartition.Rows(0)("MontantBailleur")) & "',"
                        Else
                            MontBailString &= "'0',"
                        End If
                    Next
                    MontBailString = Mid(MontBailString, 1, (MontBailString.Length - 1)) 'Enlever le dernier ','

                    query = "INSERT INTO T_TampFicheActiviteBailleur VALUES(NULL,'" & rw("RefBesoinPartition") & "','" & rw("NumeroComptable") & "','" & LibelleCompte & "','" & rw("QteNature") & "','" & rw("UniteBesoin") & "','" & rw("PUNature") & "','" & SessionID & "','" & ProjetEnCours & "'," & MontBailString & ")"
                    ExecuteNonQuery(query)
                Next


                Dim reportActivite As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table

                DatSet = New DataSet
                reportActivite.Load(EtatToLoad)

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = reportActivite.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                reportActivite.SetDataSource(DatSet)
                reportActivite.SetParameterValue("CodeProjet", ProjetEnCours)
                reportActivite.SetParameterValue("CodeUtils", SessionID)
                reportActivite.SetParameterValue("DateDeb", ExerciceComptable.Rows(0).Item("datedebut"))
                reportActivite.SetParameterValue("DateFin", ExerciceComptable.Rows(0).Item("datefin"))

                FullScreenReport.FullView.ReportSource = reportActivite

                FinChargement()
                FullScreenReport.ShowDialog()

            Else
                FailMsg("Trop de bailleurs enregistrés" & vbNewLine & "Veuillez migrer sur une ligne supérieure")
            End If




            ''Dim Reader As MySqlDataReader
            'query = "select b.RefBesoinPartition, b.NumeroComptable, b.LibelleBesoin, b.QteNature, b.UniteBesoin, b.PUNature from T_BesoinPartition b, T_Partition p where b.CodePartition=p.CodePartition " & clause
            'Dim dt As DataTable = ExcecuteSelectQuery(query)
            'Dim sqlconn As New MySqlConnection
            'BDOPEN(sqlconn)
            'For Each rw In dt.Rows

            '    TabInfo(0) = rw(0).ToString
            '    TabInfo(1) = EnleverApost(rw(2).ToString)
            '    TabInfo(2) = rw(1).ToString
            '    TabInfo(3) = rw(3).ToString
            '    TabInfo(4) = rw(4).ToString
            '    TabInfo(5) = rw(5).ToString

            '    For m As Decimal = 0 To nbBail - 1

            '        Dim Tamp As Decimal = 0


            '        query = "select MontantBailleur from T_RepartitionParBailleur where CodeBailleur='" & Bailleur(m) & "' and RefBesoinPartition='" & rw(0).ToString & "'"
            '        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            '        For Each rw0 In dt0.Rows
            '            Tamp += CDec(rw0(0))
            '        Next
            '        TabInfo(6 + m) = Tamp.ToString

            '    Next

            '    DatSet = New DataSet
            '    query = "select * from T_TampFicheActiviteBailleur"

            '    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            '    Dim DatAdapt = New MySqlDataAdapter(Cmd)
            '    DatAdapt.Fill(DatSet, "T_TampFicheActiviteBailleur")
            '    Dim DatTable = DatSet.Tables("T_TampFicheActiviteBailleur")
            '    Dim DatRow = DatSet.Tables("T_TampFicheActiviteBailleur").NewRow()

            '    DatRow("RefBesoinPartition") = TabInfo(0)
            '    DatRow("NumeroCompte") = TabInfo(2)
            '    DatRow("Libelle") = TabInfo(1)
            '    DatRow("Qte") = TabInfo(3)
            '    DatRow("Unite") = TabInfo(4)
            '    DatRow("PUn") = TabInfo(5)
            '    For x As Decimal = 0 To nbBail - 1
            '        DatRow("MontBail" & (x + 1).ToString) = TabInfo(6 + x)
            '    Next

            '    DatSet.Tables("T_TampFicheActiviteBailleur").Rows.Add(DatRow)
            '    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            '    DatAdapt.Update(DatSet, "T_TampFicheActiviteBailleur")

            '    DatSet.Clear()
            'Next
            'BDQUIT(sqlconn)

            ' Affichage état ***************************
            '    Dim SyntheseB As New ReportDocument
            '        Dim crtableLogoninfos As New TableLogOnInfos
            '        Dim crtableLogoninfo As New TableLogOnInfo
            '        Dim crConnectionInfo As New ConnectionInfo
            '        Dim CrTables As Tables
            '        Dim CrTable As Table
            '        Dim Chemin As String = lineEtat & "\EditionBudgetaire\"

            '        DatSet = New DataSet
            '        SyntheseB.Load(Chemin & "FinancementBailleur.rpt")
            '        With crConnectionInfo
            '            .ServerName = ODBCNAME
            '            .DatabaseName = DB
            '            .UserID = USERNAME
            '            .Password = PWD
            '        End With

            '        CrTables = SyntheseB.Database.Tables
            '        For Each CrTable In CrTables
            '            crtableLogoninfo = CrTable.LogOnInfo
            '            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '            CrTable.ApplyLogOnInfo(crtableLogoninfo)
            '        Next
            '        SyntheseB.SetDataSource(DatSet)
            '        SyntheseB.SetParameterValue("CodeProjet", ProjetEnCours)
            '        SyntheseB.SetParameterValue("DateDeb", dtd.Text)
            '        SyntheseB.SetParameterValue("DateFin", dtf.Text)
            '        FullScreenReport.FullView.ReportSource = SyntheseB
            '        FinChargement()
            '        FullScreenReport.ShowDialog()
            '    ElseIf CheckedComboBoxEdit1.Text = "Par Activite" Then

            '        'Bailersion de la date
            '        Dim str(3) As String
            '        str = dtd.Text.Split("/")
            '        Dim tempdt As String = String.Empty
            '        For j As Integer = 2 To 0 Step -1
            '            tempdt += str(j) & "-"
            '        Next
            '        tempdt = tempdt.Substring(0, 10)

            '        Dim str1(3) As String
            '        str1 = dtf.Text.Split("/")
            '        Dim tempdt1 As String = String.Empty
            '        For j As Integer = 2 To 0 Step -1
            '            tempdt1 += str1(j) & "-"
            '        Next
            '        tempdt1 = tempdt1.Substring(0, 10)

            '        Dim clause As String = ""

            '        'Requete Date
            '        If DateTime.Compare(tempdt1, tempdt) >= 0 Then
            '            clause = "AND p.dateDebutPartition >='" & tempdt & "' AND p.dateFinPartition <='" & tempdt1 & "'"
            '        Else
            '            SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
            '        End If

            '        DebutChargement(True, "Recherche des informations demandées en cours...")

            '        Dim DatSet = New DataSet
            '        Dim TabInfo(20) As String
            '        Dim Bailleur(5) As String
            '        Dim nbBail As Decimal = 0
            '        ChargerBailleur(Bailleur, nbBail)

            '        query = "DELETE from T_TampFicheActiviteBailleur"
            '        ExecuteNonQuery(query)

            '        'Dim Reader As MySqlDataReader
            '        query = "select b.RefBesoinPartition, b.NumeroComptable, b.LibelleBesoin, b.QteNature, b.UniteBesoin, b.PUNature from T_BesoinPartition b, T_Partition p where b.CodePartition=p.CodePartition " & clause
            '        Dim dt As DataTable = ExcecuteSelectQuery(query)
            '        Dim sqlconn As New MySqlConnection
            '        BDOPEN(sqlconn)
            '        For Each rw In dt.Rows

            '            TabInfo(0) = rw(0).ToString
            '            TabInfo(1) = EnleverApost(rw(2).ToString)
            '            TabInfo(2) = rw(1).ToString
            '            TabInfo(3) = rw(3).ToString
            '            TabInfo(4) = rw(4).ToString
            '            TabInfo(5) = rw(5).ToString

            '            For m As Decimal = 0 To nbBail - 1

            '                Dim Tamp As Decimal = 0


            '                query = "select MontantBailleur from T_RepartitionParBailleur where CodeBailleur='" & Bailleur(m) & "' and RefBesoinPartition='" & rw(0).ToString & "'"
            '                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            '                For Each rw0 In dt0.Rows
            '                    Tamp += CDec(rw0(0))
            '                Next


            '                TabInfo(6 + m) = Tamp.ToString

            '            Next

            '            DatSet = New DataSet
            '            query = "select * from T_TampFicheActiviteBailleur"

            '            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            '            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            '            DatAdapt.Fill(DatSet1, "T_TampFicheActiviteBailleur")
            '            Dim DatTable = DatSet1.Tables("T_TampFicheActiviteBailleur")
            '            Dim DatRow = DatSet.Tables("T_TampFicheActiviteBailleur").NewRow()

            '            DatRow("RefBesoinPartition") = TabInfo(0)
            '            DatRow("NumeroCompte") = TabInfo(2)
            '            DatRow("Libelle") = TabInfo(1)
            '            DatRow("Qte") = TabInfo(3)
            '            DatRow("Unite") = TabInfo(4)
            '            DatRow("PUn") = TabInfo(5)
            '            For x As Decimal = 0 To nbBail - 1
            '                DatRow("MontBail" & (x + 1).ToString) = TabInfo(6 + x)
            '            Next

            '            DatSet.Tables("T_TampFicheActiviteBailleur").Rows.Add(DatRow)
            '            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            '            DatAdapt.Update(DatSet, "T_TampFicheActiviteBailleur")

            '            DatSet.Clear()
            '        Next
            '        BDQUIT(sqlconn)

            '        ' Affichage état ***************************
            '        Dim SyntheseB As New ReportDocument
            '        Dim crtableLogoninfos As New TableLogOnInfos
            '        Dim crtableLogoninfo As New TableLogOnInfo
            '        Dim crConnectionInfo As New ConnectionInfo
            '        Dim CrTables As Tables
            '        Dim CrTable As Table
            '        Dim Chemin As String = lineEtat & "\EditionBudgetaire\"

            '        DatSet = New DataSet
            '        SyntheseB.Load(Chemin & "FinancementActivite.rpt")

            '        With crConnectionInfo
            '            .ServerName = ODBCNAME
            '            .DatabaseName = DB
            '            .UserID = USERNAME
            '            .Password = PWD
            '        End With

            '        CrTables = SyntheseB.Database.Tables
            '        For Each CrTable In CrTables
            '            crtableLogoninfo = CrTable.LogOnInfo
            '            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '            CrTable.ApplyLogOnInfo(crtableLogoninfo)
            '        Next

            '        SyntheseB.SetDataSource(DatSet)
            '        SyntheseB.SetParameterValue("CodeProjet", ProjetEnCours)
            '        SyntheseB.SetParameterValue("DateDeb", dtd.Text)
            '        SyntheseB.SetParameterValue("DateFin", dtf.Text)

            '        FullScreenReport.FullView.ReportSource = SyntheseB

            '        FinChargement()
            '        FullScreenReport.ShowDialog()

            '    ElseIf CheckedComboBoxEdit1.Text = "Par Responsable" Then

            '        'Bailersion de la date
            '        Dim str(3) As String
            '    str = dtd.Text.Split("/")
            '    Dim tempdt As String = String.Empty
            '    For j As Integer = 2 To 0 Step -1
            '        tempdt += str(j) & "-"
            '    Next
            '    tempdt = tempdt.Substring(0, 10)

            '    Dim str1(3) As String
            '    str1 = dtf.Text.Split("/")
            '    Dim tempdt1 As String = String.Empty
            '    For j As Integer = 2 To 0 Step -1
            '        tempdt1 += str1(j) & "-"
            '    Next
            '    tempdt1 = tempdt1.Substring(0, 10)

            '    Dim clause As String = ""

            '    'Requete Date
            '    If DateTime.Compare(tempdt1, tempdt) >= 0 Then
            '        clause = "AND p.dateDebutPartition >='" & tempdt & "' AND p.dateFinPartition <='" & tempdt1 & "'"
            '    Else
            '        SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
            '    End If

            '    DebutChargement(True, "Recherche des informations demandées en cours...")

            '    Dim DatSet = New DataSet
            '    Dim TabInfo(20) As String
            '    Dim Bailleur(5) As String
            '    Dim nbBail As Decimal = 0
            '    ChargerBailleur(Bailleur, nbBail)
            '    query = "DELETE from T_TampFicheActiviteBailleur"
            '    ExecuteNonQuery(query)

            '    'Dim Reader As MySqlDataReader
            '    query = "select b.RefBesoinPartition, b.NumeroComptable, b.LibelleBesoin, b.QteNature, b.UniteBesoin, b.PUNature from T_BesoinPartition b, T_Partition p where b.CodePartition=p.CodePartition " & clause
            '    Dim dt As DataTable = ExcecuteSelectQuery(query)
            '    Dim sqlconn As New MySqlConnection
            '    BDOPEN(sqlconn)
            '    For Each rw In dt.Rows

            '        TabInfo(0) = rw(0).ToString
            '        TabInfo(1) = EnleverApost(rw(2).ToString)
            '        TabInfo(2) = rw(1).ToString
            '        TabInfo(3) = rw(3).ToString
            '        TabInfo(4) = rw(4).ToString
            '        TabInfo(5) = rw(5).ToString
            '        For m As Decimal = 0 To nbBail - 1
            '            Dim Tamp As Decimal = 0
            '            query = "select MontantBailleur from T_RepartitionParBailleur where CodeBailleur='" & Bailleur(m) & "' and RefBesoinPartition='" & rw(0).ToString & "'"
            '            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            '            For Each rw0 In dt0.Rows
            '                Tamp += CDec(rw0(0))
            '            Next
            '            TabInfo(6 + m) = Tamp.ToString
            '        Next

            '        DatSet = New DataSet
            '        query = "select * from T_TampFicheActiviteBailleur"

            '        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            '        Dim DatAdapt = New MySqlDataAdapter(Cmd)
            '        DatAdapt.Fill(DatSet, "T_TampFicheActiviteBailleur")
            '        Dim DatTable = DatSet.Tables("T_TampFicheActiviteBailleur")
            '        Dim DatRow = DatSet.Tables("T_TampFicheActiviteBailleur").NewRow()

            '        DatRow("RefBesoinPartition") = TabInfo(0)
            '        DatRow("NumeroCompte") = TabInfo(2)
            '        DatRow("Libelle") = TabInfo(1)
            '        DatRow("Qte") = TabInfo(3)
            '        DatRow("Unite") = TabInfo(4)
            '        DatRow("PUn") = TabInfo(5)
            '        For x As Decimal = 0 To nbBail - 1
            '            DatRow("MontBail" & (x + 1).ToString) = TabInfo(6 + x)
            '        Next

            '        DatSet.Tables("T_TampFicheActiviteBailleur").Rows.Add(DatRow)
            '        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            '        DatAdapt.Update(DatSet, "T_TampFicheActiviteBailleur")

            '        DatSet.Clear()
            '    Next
            '    BDQUIT(sqlconn)

            '    ' Affichage état ***************************
            '    Dim SyntheseB As New ReportDocument
            '    Dim crtableLogoninfos As New TableLogOnInfos
            '    Dim crtableLogoninfo As New TableLogOnInfo
            '    Dim crConnectionInfo As New ConnectionInfo
            '    Dim CrTables As Tables
            '    Dim CrTable As Table
            '    Dim Chemin As String = lineEtat & "\EditionBudgetaire\"

            '    DatSet = New DataSet
            '    SyntheseB.Load(Chemin & "FinancementResponsable.rpt")

            '    With crConnectionInfo
            '        .ServerName = ODBCNAME
            '        .DatabaseName = DB
            '        .UserID = USERNAME
            '        .Password = PWD
            '    End With

            '    CrTables = SyntheseB.Database.Tables
            '    For Each CrTable In CrTables
            '        crtableLogoninfo = CrTable.LogOnInfo
            '        crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '        CrTable.ApplyLogOnInfo(crtableLogoninfo)
            '    Next

            '    SyntheseB.SetDataSource(DatSet)
            '    SyntheseB.SetParameterValue("CodeProjet", ProjetEnCours)
            '    SyntheseB.SetParameterValue("DateDeb", dtd.Text)
            '    SyntheseB.SetParameterValue("DateFin", dtf.Text)

            '    FullScreenReport.FullView.ReportSource = SyntheseB

            '    FinChargement()
            '    FullScreenReport.ShowDialog()
        End If


    End Sub

    Private Sub Etat_engFac_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        'date
        dtd.Text = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
        dtf.Text = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")
        'query = "select datedebut, datefin from T_COMP_EXERCICE where encours='1'"
        'Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt0.Rows
        'Next
        CheckedComboBoxEdit1.Properties.Items.Clear()
        CheckedComboBoxEdit1.Properties.Items.AddRange({"Par Bailleur"}) ', "Par Responsable", "Par Activite"
    End Sub

    Private Sub dtd_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles dtd.EditValueChanged
        If dtd.Text <> "" And dtf.Text <> "" Then
            If DateTime.Compare(CDate(dtf.Text), CDate(dtd.Text)) >= 0 Then
            Else
                dtd.Text = ""
                SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
            End If
        End If
    End Sub

    Private Sub dtf_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles dtf.EditValueChanged
        If dtd.Text <> "" And dtf.Text <> "" Then
            If DateTime.Compare(CDate(dtf.Text), CDate(dtd.Text)) >= 0 Then
            Else
                dtf.Text = ""
                SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
            End If
        End If
    End Sub
End Class