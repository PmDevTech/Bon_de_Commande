Imports KS.Gantt
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports MySql.Data.MySqlClient
Imports DevExpress.Printing

Public Class DiagrammeGantt

    Private Sub DiagrammeGantt_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub DiagrammeGantt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        Dim Nbre As Decimal = 0
        query = "select COUNT(*) from T_Partition where CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Nbre = rw(0)
        Next

        If Nbre = 0 Then
        Else
            ChargerGantt()
            ChangerCouleur()
        End If
    End Sub

    Private Sub ChangerCouleur()

        Gantt1.BackColor = ColorBackGround.Color
        Gantt1.WeekendFormatStyle.BackgroundStyle.Color = ColorWeekEnd.Color
        Gantt1.TodayLineStyle.Color = ColorToday.Color
        Gantt1.Refresh()

    End Sub

    Private Sub ChargerGantt()
        Dim col As GanttDataColumn
        col = Me.GanttDataGrid1.TaskColumns.Add("ColDesc", "Libellé")
        col.EditType = GanttEditTypes.Text
        col.Width = 250

        Dim colCode As GanttDataColumn
        colCode = Me.GanttDataGrid1.TaskColumns.Add("ColCode", "Code")
        colCode.EditType = GanttEditTypes.Text
        colCode.Visible = False

        Gantt1.SuspendItemLayout()

        Dim gProjet As GroupItem
        gProjet = Gantt1.AddGroup(ProjetEnCours)

        'date
        Dim datedeb, datefin As Date
        datedeb = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
        datefin = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")
        'query = "select datedebut, datefin from T_COMP_EXERCICE where Etat<>'2' and encours='1'"
        'Dim dt As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt.Rows
        'Next

        'conversion de la date
        Dim str(3) As String
        str = datedeb.ToString("dd/MM/yyyy").Split("/")
        Dim tempdt As String = String.Empty
        For j As Integer = 2 To 0 Step -1
            tempdt += str(j) & "-"
        Next
        tempdt = tempdt.Substring(0, 10)

        Dim str1(3) As String
        str1 = datefin.ToString("dd/MM/yyyy").Split("/")
        Dim tempdt1 As String = String.Empty
        For j As Integer = 2 To 0 Step -1
            tempdt1 += str1(j) & "-"
        Next
        tempdt1 = tempdt1.Substring(0, 10)

        query = "select CodePartition, LibelleCourt, LibellePartition from T_Partition where LENGTH(LibelleCourt)='1' and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Dim gCompo As GroupItem
            gCompo = Gantt1.AddGroup("Composante " & rw(1).ToString)
            Dim dernDate As Date = CDate("01/01/2000")
            'Dim Reader As MySqlDataReader
            query = "select CodePartition, LibelleCourt, LibellePartition, DateDebutPartition, DateFinPartition, ProgressionPartition, StatutPartition from T_Partition where CodeClassePartition='5' and LibelleCourt like '" & rw(1).ToString & "%' and CodeProjet='" & ProjetEnCours & "' AND dateDebutPartition>='" & tempdt & "' AND dateDebutPartition <='" & tempdt1 & "' order by LibelleCourt"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw0 As DataRow In dt0.Rows

                Dim durree As String
                Dim fev As Decimal = 0
                durree = DateDiff(DateInterval.Day, CDate(rw0(4).ToString), CDate(rw0(3).ToString)) - (2 * DateDiff(DateInterval.Weekday, CDate(rw0(4).ToString), CDate(rw0(3).ToString)))
                durree = durree.ToString
                query = "select date_jf from jour_ferier where date_jf >= '" & tempdt & "' And date_jf <= '" & tempdt1 & "'"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                fev = dt1.Rows.Count
                durree = CInt(durree) - fev

                Dim NewActivite As TaskItem
                NewActivite = Me.Gantt1.AddTask(CDate(rw0(3).ToString).ToShortDateString, CDate(rw0(4).ToString).ToShortDateString, rw0(1).ToString)
                NewActivite.PercentDone = CDec(rw0(5)) / 100
                Ressource(NewActivite, rw0(0).ToString, "Responsable")
                Ressource(NewActivite, durree, "Elaborateur")
                'Ressource(NewActivite, rw0(0).ToString, "Elaborateur")
                NewActivite.Status = IIf(rw0(6).ToString = "En attente", TaskItem.TaskStates.Waiting, IIf(rw0(6).ToString = "En cours", TaskItem.TaskStates.Running, TaskItem.TaskStates.Done))
                NewActivite.Priority = TaskItem.TaskPriorities.Normal
                NewActivite.SetProperty("ColDesc", MettreApost(rw0(2).ToString))
                NewActivite.SetProperty("ColCode", rw0(0).ToString)
                If (Date.Compare(dernDate, CDate(rw0(4).ToString)) < 0) Then
                    dernDate = CDate(rw0(4)).ToShortDateString
                End If

                gCompo.AddChild(NewActivite)
            Next
            '

            If (dernDate.ToShortDateString <> CDate("01/01/2000").ToShortDateString) Then
                    Dim Saut As TaskItem
                    Saut = Me.Gantt1.AddTask(dernDate, dernDate, "Fin Composante " & rw(1).ToString)
                    Saut.IsMilestone = True
                    gCompo.AddChild(Saut)

                    gProjet.AddChild(gCompo)
                Else
                    gCompo.Visible = False
                End If
            Next

            Me.Gantt1.AutoMoveItems()
        Me.Gantt1.ZoomToFit()
        Me.Gantt1.ResumeItemLayout()
        Me.Gantt1.Focus()


    End Sub

    Private Sub Ressource(ByRef Tache As TaskItem, ByVal Activite As String, ByVal Fonction As String)
        
        Dim resource1 As ResourceItem
        query = "select EMP_NOM, EMP_PRENOMS from t_grh_employe E, T_OperateurPartition P where E.EMP_ID=P.EMP_ID and P.CodePartition='" & Activite & "' and TitreOpPart='" & Fonction & "' and E.PROJ_ID='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            resource1 = Me.Gantt1.AddResource(MettreApost(rw("EMP_NOM").ToString & " " & rw("EMP_PRENOMS").ToString), Fonction, 1)
            Tache.AddResource(resource1)
        Next
    End Sub

    Private Sub GanttDataGrid1_ItemDoubleClick(ByVal sender As Object, ByVal e As KS.Gantt.GanttItemEventArgs) Handles GanttDataGrid1.ItemDoubleClick

        If (Gantt1.ViewMode = Gantt.ViewModes.Resources) Then
            MsgBox("Vous n'êtes pas en mode tâches.", MsgBoxStyle.Information)
        Else
            Dim Indx As Decimal = GanttDataGrid1.SelectedRowIndex
            If (Gantt1.ItemByRowIndex(Indx).ToString.Length >= 5 And Gantt1.ItemByRowIndex(Indx).ToString.Length <= 6) Then
                Fiche(Gantt1.ItemByRowIndex(Indx).Properties.Item("ColCode").ToString)
            Else
                'MsgBox("Ce n'est pas une activité!", MsgBoxStyle.Information)
            End If

        End If

    End Sub

    Private Sub Gantt1_ItemDoubleClick(ByVal sender As Object, ByVal e As KS.Gantt.GanttItemMouseEventArgs) Handles Gantt1.ItemDoubleClick

        If (Gantt1.ViewMode = Gantt.ViewModes.Resources) Then
            MsgBox("Vous n'êtes pas en mode tâches.", MsgBoxStyle.Information)
        Else

            Dim Indx As Decimal = GanttDataGrid1.SelectedRowIndex
            If (Gantt1.ItemByRowIndex(Indx).Properties.Item("ColCode").ToString <> "" ) Then
                Fiche(Gantt1.ItemByRowIndex(Indx).Properties.Item("ColCode").ToString)
            Else
                'MsgBox("Ce n'est pas une activité!", MsgBoxStyle.Information)
            End If

        End If

    End Sub

    Private Sub ChargerConvention(ByRef TabConv() As String, ByRef nB As Decimal)
        nB = 0

        query = "select c.CodeConvention from T_Convention c, T_Bailleur b where b.CodeBailleur=c.CodeBailleur and b.CodeProjet='" & ProjetEnCours & "' order by c.CodeConvention"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            TabConv(nB) = rw(0).ToString
            nB += 1
        Next
        query = "DELETE from T_TampConvention"
        ExecuteNonQuery(query)

        Dim DatSet = New DataSet
        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)

        query = "select * from T_TampConvention"
        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd)
        DatAdapt.Fill(DatSet, "T_TampConvention")
        Dim DatTable = DatSet.Tables("T_TampConvention")
        Dim DatRow = DatSet.Tables("T_TampConvention").NewRow()

        For n As Decimal = 0 To nB - 1
            DatRow("conv" & (n + 1).ToString) = TabConv(n)
        Next

        DatSet.Tables("T_TampConvention").Rows.Add(DatRow)
        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        DatAdapt.Update(DatSet, "T_TampConvention")

        DatSet.Clear()
        BDQUIT(sqlconn)

    End Sub

    Private Sub Fiche(ByVal CodeAct As String)
        If Not Access_Btn("BtnPrintFicheActivite") Then
            Exit Sub
        End If

        DebutChargement(True, "Recherche des informations demandées en cours...")
        'Chargement des conventions
        query = "SELECT COUNT(*) FROM t_convention c, t_bailleur b WHERE b.CodeBailleur=c.CodeBailleur AND b.CodeProjet='" & ProjetEnCours & "'"
        Dim ConvCount As Decimal = Val(ExecuteScallar(query))
        query = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS  WHERE table_name = 't_tampconvention' AND table_schema = '" & DB & "'"
        Dim OldConvCount As Decimal = Val(ExecuteScallar(query))
        If ConvCount <> OldConvCount Then
            'Suppression des conventions tamporaires
            If ConvCount > 1 Then
                Dim ConvName As String = "conv"
                If OldConvCount > 1 Then
                    For i = 2 To OldConvCount
                        ConvName = "conv" & i
                        query = "ALTER TABLE `t_tampconvention` DROP `" & ConvName & "`;"
                        ExecuteNonQuery(query)
                    Next
                End If

                For i = 2 To ConvCount
                    ConvName = "conv" & i
                    Dim Convaftername As String = "conv" & (i - 1)
                    query = "ALTER TABLE `t_tampconvention` ADD `" & ConvName & "` VARCHAR(255) NOT NULL AFTER `" & Convaftername & "`"
                    ExecuteNonQuery(query)
                Next
            ElseIf ConvCount = 1 Then
                Dim ConvName As String = "conv"
                If OldConvCount > 1 Then
                    For i = 2 To OldConvCount
                        ConvName = "conv" & i
                        query = "ALTER TABLE `t_tampconvention` DROP `" & ConvName & "`;"
                        ExecuteNonQuery(query)
                    Next
                End If
            End If
        End If
        'Insertion des conventions
        query = "SELECT c.CodeConvention FROM t_convention c, t_bailleur b WHERE b.CodeBailleur=c.CodeBailleur AND b.CodeProjet='" & ProjetEnCours & "' ORDER BY CodeConvention ASC"
        Dim dtConvention As DataTable = ExcecuteSelectQuery(query)
        Dim ConvString As String = String.Empty
        For Each rw As DataRow In dtConvention.Rows
            ConvString &= "'" & rw("CodeConvention") & "',"
        Next
        ConvString = Mid(ConvString, 1, (ConvString.Length - 1)) 'Enlever le dernier ';'

        ExecuteNonQuery("TRUNCATE t_tampconvention") 'Vider la table
        query = "INSERT INTO t_tampconvention VALUES(" & ConvString & ")"
        ExecuteNonQuery(query)

        'Chargement des colonnes de la table tampon
        query = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS  WHERE table_name = 't_tampficheactivite' AND table_schema = '" & DB & "' AND COLUMN_NAME='Conv'"
        If Val(ExecuteScallar(query)) > 0 Then 'Retire la colonne Conv de la table 't_tampficheactivite'
            query = "ALTER TABLE `t_tampficheactivite` DROP `Conv`;"
            ExecuteNonQuery(query)
        End If


        query = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS  WHERE table_name = 't_tampficheactivite' AND table_schema = '" & DB & "'"
        Dim OldTampCount As Decimal = Val(ExecuteScallar(query)) - 9

        If ConvCount <> OldTampCount Then
            'Suppression des conventions tamporaires
            If ConvCount > 1 Then
                Dim ConvName As String = "MontBail"
                If OldTampCount > 1 Then
                    For i = 2 To OldTampCount
                        ConvName = "MontBail" & i
                        query = "ALTER TABLE `t_tampficheactivite` DROP `" & ConvName & "`;"
                        ExecuteNonQuery(query)
                    Next
                End If

                For i = 2 To ConvCount
                    ConvName = "MontBail" & i
                    Dim Convaftername As String = "MontBail" & (i - 1)
                    query = "ALTER TABLE `t_tampficheactivite` ADD `" & ConvName & "` DECIMAL(20.0) NOT NULL DEFAULT 0 AFTER `" & Convaftername & "`"
                    ExecuteNonQuery(query)
                Next
            ElseIf ConvCount = 1 Then
                Dim ConvName As String = "MontBail"
                If OldTampCount > 1 Then
                    For i = 2 To OldTampCount
                        ConvName = "MontBail" & i
                        query = "ALTER TABLE `t_tampficheactivite` DROP `" & ConvName & "`;"
                        ExecuteNonQuery(query)
                    Next
                End If
            End If
        End If

        query = "DELETE FROM T_TampFicheActivite WHERE CodeUtils='" & SessionID & "' AND CodeProjet='" & ProjetEnCours & "'"
        ExecuteNonQuery(query)

        query = "DELETE FROM t_tamppartition WHERE CodeUtils='" & SessionID & "' AND CodeProjet='" & ProjetEnCours & "'"
        ExecuteNonQuery(query)

        'query = "TRUNCATE T_TampFicheActivite"
        'ExecuteNonQuery(query)

        'query = "TRUNCATE t_tamppartition"
        'ExecuteNonQuery(query)

        'Insertion des activités selectionnées dans la table tamppartition
        Dim LibelleCourt As String = ExecuteScallar("SELECT LibelleCourt FROM t_partition WHERE CodePartition='" & CodeAct & "'")
        query = "INSERT INTO t_tamppartition VALUES(NULL,'" & LibelleCourt & "','" & ProjetEnCours & "','" & SessionID & "')"
        ExecuteNonQuery(query)

        query = "select RefBesoinPartition, NumeroComptable, LibelleBesoin, QteNature, UniteBesoin, PUNature from T_BesoinPartition where CodePartition='" & CodeAct & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Dim MontConvString As String = String.Empty
            Dim Tamp As Double = 0
            query = "SELECT c.CodeConvention FROM t_convention c, t_bailleur b WHERE b.CodeBailleur=c.CodeBailleur AND b.CodeProjet='" & ProjetEnCours & "' ORDER BY CodeConvention ASC"
            dtConvention = ExcecuteSelectQuery(query)
            Dim Count As Decimal = 0
            For Each rwConv As DataRow In dtConvention.Rows
                Count += 1
                query = "select MontantBailleur from T_RepartitionParBailleur where CodeConvention='" & rwConv("CodeConvention") & "' and RefBesoinPartition='" & rw("RefBesoinPartition").ToString & "'"
                Dim dtRepartition As DataTable = ExcecuteSelectQuery(query)
                If dtRepartition.Rows.Count > 0 Then
                    MontConvString &= "'" & CDec(dtRepartition.Rows(0)("MontantBailleur")) & "',"
                Else
                    MontConvString &= "'0',"
                End If
            Next
            MontConvString = Mid(MontConvString, 1, (MontConvString.Length - 1)) 'Enlever le dernier ','

            query = "INSERT INTO T_TampFicheActivite VALUES(NULL,'" & rw("RefBesoinPartition") & "','" & rw("NumeroComptable") & "','" & EnleverApost(rw("LibelleBesoin")) & "','" & rw("QteNature") & "','" & rw("UniteBesoin") & "','" & rw("PUNature") & "','" & SessionID & "','" & ProjetEnCours & "'," & MontConvString & ")"
            'query = "INSERT INTO T_TampFicheActivite VALUES(NULL,'" & rw("RefBesoinPartition") & "','" & rw("NumeroComptable") & "','" & EnleverApost(rw("LibelleBesoin")) & "','" & rw("QteNature") & "','" & rw("UniteBesoin") & "','" & rw("PUNature") & "'," & MontConvString & ")"
            ExecuteNonQuery(query)
        Next

        ' Affichage état ***************************
        query = "SELECT COUNT(*) FROM t_convention c, t_bailleur b WHERE b.CodeBailleur=c.CodeBailleur AND b.CodeProjet='" & ProjetEnCours & "'"
        ' Dim ConvCount As Decimal = Val(ExecuteScallar(query))
        Dim EtatToLoad As String = lineEtat & "\FicheActivite\FicheActivite" & ConvCount & ".rpt"
        If IO.File.Exists(EtatToLoad) Then
            Dim reportActivite As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table

            Dim DatSet = New DataSet
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
            reportActivite.SetParameterValue("CodeUtils", SessionID, "FicheActivite" & ConvCount & "_SousRapport")
            reportActivite.SetParameterValue("CodeUtils", SessionID)

            FullScreenReport.FullView.ReportSource = reportActivite

            FinChargement()
            FullScreenReport.ShowDialog()
        Else
            FailMsg("Trop de conventions enregistrées" & vbNewLine & "Veuillez migrer sur une ligne supérieure")
        End If

    End Sub

    Private Sub ChargerBailleur(ByRef TabBail() As String, ByRef nB As Decimal)
        nB = 0
        Dim NomBail(5) As String
        query = "select CodeBailleur, InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' order by InitialeBailleur"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            NomBail(nB) = rw(1).ToString
            TabBail(nB) = rw(0).ToString
            nB += 1
        Next
        query = "DELETE from T_TampBailleur"
        ExecuteNonQuery(query)

        Dim DatSet = New DataSet
        query = "select * from T_TampBailleur"
        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)
        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd)
        DatAdapt.Fill(DatSet, "T_TampBailleur")
        Dim DatTable = DatSet.Tables("T_TampBailleur")
        Dim DatRow = DatSet.Tables("T_TampBailleur").NewRow()

        For n As Decimal = 0 To nB - 1
            DatRow("Bail" & (n + 1).ToString) = NomBail(n)
        Next

        DatSet.Tables("T_TampBailleur").Rows.Add(DatRow)
        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        DatAdapt.Update(DatSet, "T_TampBailleur")
        
        DatSet.Clear()
        BDQUIT(sqlconn)

    End Sub

    Private Sub BtImprimer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtImprimer.Click
        Dim Printer = New GanttPrintDocument
        Printer.PrinterSettings.PrinterName = "T_TampBailleur"
        Gantt1.Printing.PrintPreview()
    End Sub

    Private Sub ColorBackGround_ColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ColorBackGround.ColorChanged
        ChangerCouleur()
    End Sub

    Private Sub ColorWeekEnd_ColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ColorWeekEnd.ColorChanged
        ChangerCouleur()
    End Sub

    Private Sub ColorToday_ColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ColorToday.ColorChanged
        ChangerCouleur()
    End Sub

End Class