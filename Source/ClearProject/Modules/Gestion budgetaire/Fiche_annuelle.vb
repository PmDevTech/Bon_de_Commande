Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports MySql.Data.MySqlClient

Public Class Fiche_annuelle
    Dim dtActivite = New DataTable()
    Dim DrX As DataRow

    Private Sub Fiche_annuelle_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        Dim Nbre As Decimal = 0
        query = "select COUNT(*) from T_Partition where CodeProjet='" & ProjetEnCours & "'"
        Nbre = Val(ExecuteScallar(query))


        If Nbre = 0 Then
        Else
            InitTabTrue()
            ChargerCompo()
            ChargerRespo()

            'date
            dtdebut.Text = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
            dtFin.Text = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")
            dtdebut.Properties.MinValue = ExerciceComptable.Rows(0).Item("datedebut").ToString
            dtFin.Properties.MinValue = ExerciceComptable.Rows(0).Item("datedebut").ToString
            dtdebut.Properties.MaxValue = ExerciceComptable.Rows(0).Item("datefin").ToString
            dtFin.Properties.MaxValue = ExerciceComptable.Rows(0).Item("datefin").ToString
            'query = "select datedebut, datefin from T_COMP_EXERCICE where Etat<>'2' and encours='1'"
            'Dim dt As DataTable = ExcecuteSelectQuery(query)
            'For Each rw As DataRow In dt.Rows
            'Next

        End If

    End Sub

    Private Sub ChargerCompo()

        query = "select LibelleCourt, LibellePartition from T_Partition where LENGTH(LibelleCourt)=1 and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
        CmbCompo.Properties.Items.Clear()
        CmbCompo.Properties.Items.Add("Toutes")
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbCompo.Properties.Items.Add(rw(0).ToString & " : " & MettreApost(rw(1).ToString))
        Next

    End Sub
    Private Sub ChargerRespo()

        query = "select E.EMP_ID, EMP_NOM, EMP_PRENOMS from t_grh_employe as E, T_OperateurPartition as P where E.EMP_ID=P.EMP_ID and P.TitreOpPart='Responsable' and E.PROJ_ID='" & ProjetEnCours & "' ORDER BY EMP_NOM,EMP_PRENOMS ASC"
        CmbRespo.Properties.Items.Clear()
        CmbRespo.Properties.Items.Add("Sans")
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbRespo.Properties.Items.Add(MettreApost(rw(1).ToString & " " & rw(2).ToString) & " : " & IIf(Len(rw(0).ToString) = 1, "00", IIf(Len(rw(0).ToString) = 2, "0", "").ToString).ToString & rw(0).ToString)
        Next

    End Sub

    Private Sub ChargerActivites()

        Dim TousVrai As Boolean = True
        ChkTous.Enabled = False
        Dim clause As String = ""
        Dim clause1 As String = ""
        'Dim max_exer As String = ""

        'query = "select max(id_exercice) from T_COMP_EXERCICE"
        'max_exer = ExecuteScallar(query)

        dtActivite.Columns.Clear()

        Dim str(3) As String
        str = dtdebut.Text.Split("/")
        Dim tempdt As String = String.Empty
        For j As Integer = 2 To 0 Step -1
            tempdt += str(j) & "-"
        Next
        tempdt = tempdt.Substring(0, 10)

        Dim str1(3) As String
        str1 = dtFin.Text.Split("/")
        Dim tempdt1 As String = String.Empty
        For j As Integer = 2 To 0 Step -1
            tempdt1 += str1(j) & "-"
        Next
        tempdt1 = tempdt1.Substring(0, 10)

        dtActivite.Columns.Add("Code", Type.GetType("System.String"))
        dtActivite.Columns.Add("Code Partition", Type.GetType("System.String"))
        dtActivite.Columns.Add("Choix", Type.GetType("System.Boolean"))
        dtActivite.Columns.Add("Référence", Type.GetType("System.String"))
        dtActivite.Columns.Add("Libellé", Type.GetType("System.String"))
        dtActivite.Columns.Add("Date début", Type.GetType("System.String"))
        dtActivite.Columns.Add("Date fin", Type.GetType("System.String"))
        dtActivite.Columns.Add("Besoin", Type.GetType("System.String"))

        'Requete Date
        If DateTime.Compare(tempdt1, tempdt) >= 0 Then
            clause = "AND dateDebutPartition>='" & tempdt & "' AND dateDebutPartition <='" & tempdt1 & "'"
        Else
            SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
        End If

        If DateTime.Compare(tempdt1, tempdt) >= 0 Then
            clause1 = "AND P.dateDebutPartition>='" & tempdt & "' AND P.dateDebutPartition <='" & tempdt1 & "'"
        Else
            SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
        End If

        Dim ReqActiv As String = ""
        If (CmbRespo.Text = "") Then
            query = "select CodePartition, LibelleCourt, LibellePartition, DateDebutPartition, DureePartitionPrevue, DateFinPartition from T_Partition where LENGTH(LibelleCourt)>=5 and LibelleCourt like '" & IIf(CmbSousCompo.Text = "Toutes", IIf(CmbCompo.Text = "Toutes", "", Mid(CmbCompo.Text, 1, 1)).ToString, Mid(CmbSousCompo.Text, 1, 2)).ToString & "%' and CodeProjet='" & ProjetEnCours & "'" & clause
        ElseIf (CmbRespo.Text = "Sans") Then
            query = "select CodePartition, LibelleCourt, LibellePartition, DateDebutPartition, DureePartitionPrevue, DateFinPartition from T_Partition where LENGTH(LibelleCourt)>=5 and LibelleCourt like '" & IIf(CmbSousCompo.Text = "Toutes", IIf(CmbCompo.Text = "Toutes", "", Mid(CmbCompo.Text, 1, 1)).ToString, Mid(CmbSousCompo.Text, 1, 2)).ToString & "%' and CodeProjet='" & ProjetEnCours & "' and CodePartition Not In (select CodePartition from T_OperateurPartition where TitreOpPart='Responsable') " & clause
        Else
            If CmbRespo.Text.Contains(":") Then
                Dim partResp() As String = CmbRespo.Text.Split(":")
                Dim CodOp As Decimal = CInt(Trim(partResp(1)))
                query = "select P.CodePartition, P.LibelleCourt, P.LibellePartition, P.DateDebutPartition, P.DureePartitionPrevue, DateFinPartition from T_Partition as P, T_OperateurPartition as O where P.CodePartition=O.CodePartition and O.EMP_ID='" & CodOp.ToString & "' and O.TitreOpPart='Responsable' and LENGTH(P.LibelleCourt)>=5 and P.LibelleCourt like '" & IIf(CmbSousCompo.Text = "Toutes", IIf(CmbCompo.Text = "Toutes", "", Mid(CmbCompo.Text, 1, 1)).ToString, Mid(CmbSousCompo.Text, 1, 2)).ToString & "%' and P.CodeProjet='" & ProjetEnCours & "'" & clause1
            Else
                Dim CodOp As Decimal = 0
                query = "select P.CodePartition, P.LibelleCourt, P.LibellePartition, P.DateDebutPartition, P.DureePartitionPrevue, DateFinPartition from T_Partition as P, T_OperateurPartition as O where P.CodePartition=O.CodePartition and O.EMP_ID='" & CodOp.ToString & "' and O.TitreOpPart='Responsable' and LENGTH(P.LibelleCourt)>=5 and P.LibelleCourt like '" & IIf(CmbSousCompo.Text = "Toutes", IIf(CmbCompo.Text = "Toutes", "", Mid(CmbCompo.Text, 1, 1)).ToString, Mid(CmbSousCompo.Text, 1, 2)).ToString & "%' and P.CodeProjet='" & ProjetEnCours & "'" & clause1
            End If
        End If

        Dim cptr As Decimal = 0
        dtActivite.Rows.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows

            Dim nbBes As Decimal = 0
            query = "select Count(*) from T_BesoinPartition where CodePartition='" & rw(0).ToString & "'"
            nbBes = Val(ExecuteScallar(query))

            If ((nbBes = 0 And CmbMont.Text <> "Avec") Or (nbBes > 0 And CmbMont.Text <> "Sans")) Then

                cptr += 1
                Dim drS = dtActivite.NewRow()

                drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
                drS(1) = rw(0).ToString
                drS(2) = TabTrue(cptr - 1)
                drS(3) = rw(1).ToString
                drS(4) = MettreApost(rw(2).ToString)
                drS(5) = CDate(rw(3)).ToString("dd/MM/yyyy")
                Dim partdurr() As String = rw(4).ToString.Split(" "c)
                drS(6) = CDate(rw(5)).ToString("dd/MM/yyyy")
                'Existance de besoin ****************
                drS(7) = IIf(nbBes > 0, "x", "").ToString

                If (TabTrue(cptr - 1) = False) Then
                    TousVrai = False
                End If
                dtActivite.Rows.Add(drS)

            End If

        Next

        LblNombre.Text = cptr.ToString & " Enregistrements"
        GridActivites.DataSource = dtActivite
        ViewActivites.Columns(0).Visible = False
        ViewActivites.Columns(1).Visible = False
        ViewActivites.Columns(2).Width = 50
        ViewActivites.Columns(3).Width = 100
        ViewActivites.Columns(4).Width = GridActivites.Width - 408
        ViewActivites.Columns(5).Width = 120
        ViewActivites.Columns(6).Width = 120
        ViewActivites.Columns(7).Visible = False
        ViewActivites.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewActivites.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewActivites.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewActivites.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewActivites.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

        ColorRowGrid(ViewActivites, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
        ColorRowGridAnal(ViewActivites, "[Choix]=true", Color.LightGray, "Times New Roman", 11, FontStyle.Bold, Color.Black, False)
        ColorRowGridAnal(ViewActivites, "[Besoin]<>'x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Gray, False)

        If (ViewActivites.RowCount > 0) Then
            If (TousVrai = True) Then
                ChkTous.Checked = True
            Else
                ChkTous.Checked = False
            End If
            ChkTous.Enabled = True
        Else
            ChkTous.Checked = False
        End If

    End Sub

    Private Sub GridActivites_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridActivites.Click
        If (ViewActivites.RowCount > 0) Then
            DrX = ViewActivites.GetDataRow(ViewActivites.FocusedRowHandle)
            If (DrX(7).ToString <> "x") Then
                If TabTrue(ViewActivites.FocusedRowHandle) = False Then
                    Dim RepImp As MsgBoxResult = MsgBox("Aucun coût enregistré sur cette activité!" & vbNewLine & "Voulez-vous l'imprimer?", MsgBoxStyle.YesNo)
                    If (RepImp = MsgBoxResult.Yes) Then
                        TabTrue(ViewActivites.FocusedRowHandle) = Not (TabTrue(ViewActivites.FocusedRowHandle))
                        ChargerActivites()
                        nbTab = ViewActivites.RowCount
                    End If
                    Exit Sub
                End If
            End If
            TabTrue(ViewActivites.FocusedRowHandle) = Not (TabTrue(ViewActivites.FocusedRowHandle))
            ChargerActivites()
            nbTab = ViewActivites.RowCount
        End If
    End Sub

    Private Sub ChkTous_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkTous.CheckedChanged

        If (ViewActivites.RowCount > 0 And ChkTous.Enabled = True) Then
            For k As Integer = 0 To ViewActivites.RowCount - 1
                TabTrue(k) = ChkTous.Checked
            Next

            If (ChkTous.Checked = True) Then
                nbTab = ViewActivites.RowCount
            Else
                nbTab = 0
            End If
            ChargerActivites()
        End If

    End Sub

    Private Sub CmbMont_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbMont.SelectedValueChanged
        ChkTous.Checked = False
        ChargerActivites()
    End Sub

    Private Sub CmbRespo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbRespo.SelectedValueChanged
        ChkTous.Checked = False
        ChargerActivites()
    End Sub

    Private Sub ChargerSousCompo(ByVal Compo As String)

        query = "select LibelleCourt, LibellePartition from T_Partition where LENGTH(LibelleCourt)=2 and LibelleCourt like '" & Compo & "%' and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
        CmbSousCompo.Properties.Items.Clear()
        CmbSousCompo.Properties.Items.Add("Toutes")
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbSousCompo.Properties.Items.Add(rw(0).ToString & " : " & MettreApost(rw(1).ToString))
        Next

    End Sub

    Private Sub CmbSousCompo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSousCompo.SelectedValueChanged

        GridActivites.DataSource = Nothing
        GridActivites.Refresh()

        InitTabTrue()

        If (CmbSousCompo.Text <> "") Then
            If (CmbSousCompo.Text = "Toutes") Then
                ChkTous.Checked = False
                ChargerActivites()
            Else
                ChkTous.Checked = False
                ChargerActivites()
            End If
        End If

    End Sub

    Private Sub CmbCompo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCompo.SelectedValueChanged

        GridActivites.DataSource = Nothing
        GridActivites.Refresh()

        InitTabTrue()

        CmbSousCompo.Properties.Items.Clear()
        CmbSousCompo.Text = ""

        If (CmbCompo.Text <> "") Then
            If (CmbCompo.Text = "Toutes") Then
                ChargerSousCompo("")
            Else
                ChargerSousCompo(Mid(CmbCompo.Text, 1, 1))
            End If
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

    Private Sub BtAppercu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAppercu.Click
        If Not Access_Btn("BtnPrintFicheActivite") Then
            Exit Sub
        End If
        If (nbTab > 0 And ViewActivites.RowCount > 0) Then
            DebutChargement(True, "Recherche des informations demandées en cours...")

            Dim TabInfo(20) As String
            Dim Bailleur(5) As String
            Dim nbBail As Decimal = 0
            ChargerBailleur(Bailleur, nbBail)

            query = "DELETE from T_TampFicheActivite"
            ExecuteNonQuery(query)
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)

            For k As Integer = 0 To nbTab - 1

                If (TabTrue(k) = True) Then

                    query = "select RefBesoinPartition, NumeroComptable, LibelleBesoin, QteNature, UniteBesoin, PUNature from T_BesoinPartition where CodePartition='" & dtActivite.Rows(k).Item(1).ToString & "'"
                    Dim dt As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows

                        TabInfo(0) = rw(0).ToString
                        TabInfo(1) = EnleverApost(rw(2).ToString)
                        TabInfo(2) = rw(1).ToString
                        TabInfo(3) = rw(3).ToString
                        TabInfo(4) = rw(4).ToString
                        TabInfo(5) = rw(5).ToString

                        For m As Decimal = 0 To nbBail - 1

                            Dim Tamp As Double = 0
                            query = "select MontantBailleur from T_RepartitionParBailleur where CodeBailleur='" & Bailleur(m) & "' and RefBesoinPartition='" & rw(0).ToString & "'"
                            dt = ExcecuteSelectQuery(query)
                            For Each rw0 As DataRow In dt.Rows
                                Tamp += CDbl(rw0(0))
                            Next
                            TabInfo(6 + m) = Tamp.ToString

                        Next

                        Dim DatSet = New DataSet
                        query = "select * from T_TampFicheActivite"
                        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                        Dim DatAdapt = New MySqlDataAdapter(Cmd)
                        DatAdapt.Fill(DatSet, "T_TampFicheActivite")
                        Dim DatTable = DatSet.Tables("T_TampFicheActivite")
                        Dim DatRow = DatSet.Tables("T_TampFicheActivite").NewRow()

                        DatRow("RefBesoinPartition") = TabInfo(0)
                        DatRow("NumeroCompte") = TabInfo(2)
                        DatRow("Libelle") = TabInfo(1)
                        DatRow("Qte") = TabInfo(3)
                        DatRow("Unite") = TabInfo(4)
                        DatRow("PUn") = TabInfo(5)
                        For x As Decimal = 0 To nbBail - 1
                            DatRow("MontBail" & (x + 1).ToString) = TabInfo(6 + x)
                        Next

                        DatSet.Tables("T_TampFicheActivite").Rows.Add(DatRow)
                        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                        DatAdapt.Update(DatSet, "T_TampFicheActivite")
                        DatSet.Clear()

                    Next

                End If

            Next
            BDQUIT(sqlconn)

            ' Affichage état ***************************
            Dim MontBail1 As Double = 0
            Dim MontBail2 As Double = 0
            Dim MontBail3 As Double = 0
            Dim MontBail4 As Double = 0
            Dim MontBail5 As Double = 0
            Dim MontBail6 As Double = 0

           query= "select sum(MontBail1) from T_TampFicheActivite"
            Try
                MontBail1 = ExecuteScallar(query)
            Catch ex As Exception
                MontBail1 = 0
            End Try

           query= "select sum(MontBail2) from T_TampFicheActivite"
            Try
                MontBail2 = ExecuteScallar(query)
            Catch ex As Exception
                MontBail2 = 0
            End Try

           query= "select sum(MontBail3) from T_TampFicheActivite"
            Try
                MontBail3 = ExecuteScallar(query)
            Catch ex As Exception
                MontBail3 = 0
            End Try

           query= "select sum(MontBail4) from T_TampFicheActivite"
            Try
                MontBail4 = ExecuteScallar(query)
            Catch ex As Exception
                MontBail4 = 0
            End Try

           query= "select sum(MontBail5) from T_TampFicheActivite"
            Try
                MontBail5 = ExecuteScallar(query)
            Catch ex As Exception
                MontBail5 = 0
            End Try

           query= "select sum(MontBail6) from T_TampFicheActivite"
            Try
                MontBail6 = ExecuteScallar(query)
            Catch ex As Exception
                MontBail6 = 0
            End Try

            If MontBail1 <> 0 Or MontBail2 <> 0 Or MontBail3 <> 0 Or MontBail4 <> 0 Or MontBail5 <> 0 Or MontBail6 <> 0 Then
                Dim reportActivite As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table
                Dim Chemin As String = lineEtat & "\FicheActivite\"

                Dim DatSet = New DataSet
                reportActivite.Load(Chemin & "FicheActivite1.rpt")

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
                reportActivite.SetParameterValue("NombreBailleur", nbBail)
                FullScreenReport.FullView.ReportSource = reportActivite
                FinChargement()
                FullScreenReport.ShowDialog()

            ElseIf MontBail1 = 0 And MontBail2 <> 0 And MontBail3 = 0 Then

                Dim reportActivite As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table
                Dim Chemin As String = lineEtat & "\FicheActivite\"

                Dim DatSet = New DataSet
                reportActivite.Load(Chemin & "FicheActivite1.rpt")

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
                reportActivite.SetParameterValue("NombreBailleur", nbBail)

                FullScreenReport.FullView.ReportSource = reportActivite

                FinChargement()
                FullScreenReport.ShowDialog()

            ElseIf MontBail1 = 0 And MontBail2 = 0 And MontBail3 <> 0 Then

                Dim reportActivite As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table
                Dim Chemin As String = lineEtat & "\FicheActivite\"

                Dim DatSet = New DataSet
                reportActivite.Load(Chemin & "FicheActivite1.rpt")

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
                reportActivite.SetParameterValue("NombreBailleur", nbBail)

                FullScreenReport.FullView.ReportSource = reportActivite

                FinChargement()
                FullScreenReport.ShowDialog()

            ElseIf MontBail1 <> 0 And MontBail2 <> 0 Then

                Dim reportActivite As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table
                Dim Chemin As String = lineEtat & "\FicheActivite\"

                Dim DatSet = New DataSet
                reportActivite.Load(Chemin & "FicheActivite2.rpt")

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
                reportActivite.SetParameterValue("NombreBailleur", nbBail)

                FullScreenReport.FullView.ReportSource = reportActivite

                FinChargement()
                FullScreenReport.ShowDialog()

            ElseIf MontBail2 <> 0 And MontBail3 <> 0 Then

                Dim reportActivite As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table
                Dim Chemin As String = lineEtat & "\FicheActivite\"

                Dim DatSet = New DataSet
                reportActivite.Load(Chemin & "FicheActivite23.rpt")

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
                reportActivite.SetParameterValue("NombreBailleur", nbBail)

                FullScreenReport.FullView.ReportSource = reportActivite

                FinChargement()
                FullScreenReport.ShowDialog()

            ElseIf MontBail1 <> 0 And MontBail3 <> 0 Then

                Dim reportActivite As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table
                Dim Chemin As String = lineEtat & "\FicheActivite\"

                Dim DatSet = New DataSet
                reportActivite.Load(Chemin & "FicheActivite22.rpt")

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
                reportActivite.SetParameterValue("NombreBailleur", nbBail)

                FullScreenReport.FullView.ReportSource = reportActivite

                FinChargement()
                FullScreenReport.ShowDialog()

            Else
                Dim reportActivite As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table
                Dim Chemin As String = lineEtat & "\FicheActivite\"

                Dim DatSet = New DataSet
                reportActivite.Load(Chemin & "FicheActivite.rpt")

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
                reportActivite.SetParameterValue("NombreBailleur", nbBail)

                FullScreenReport.FullView.ReportSource = reportActivite

                FinChargement()
                FullScreenReport.ShowDialog()

            End If

        Else
            MsgBox("Aucune fiche d'activité selectionnée", MsgBoxStyle.Information, "ClearProject")
        End If

    End Sub

End Class