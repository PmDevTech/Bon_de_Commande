Imports MySql.Data.MySqlClient
Imports Microsoft.Office.Interop
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class FichesActivitesEtape1
    Dim dtActivite = New DataTable()
    Dim DrX As DataRow
    Dim Operation As String = "Normal"
    'Dim LibelleActiviteDupliq As String = ""
    Private Sub FichesActivitesEtape1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        'date
        Datedebub.Text = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
        DateFin.Text = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")
        Datedebub.Properties.MinValue = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
        Datedebub.Properties.MaxValue = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")

        DateFin.Properties.MinValue = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
        DateFin.Properties.MaxValue = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")
        'query = "select datedebut, datefin from T_COMP_EXERCICE where encours='1'"
        'Dim dt As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt.Rows
        'Next

        Dim Nbre As Decimal = 0

        query = "select COUNT(*) from T_Partition where CodeProjet='" & ProjetEnCours & "'"
        dt = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            Nbre = dt.Rows(0).Item(0)
        End If

        If Nbre = 0 Then
        Else
            RemplirActivite()
            RemplirCompo()
        End If

    End Sub

    Private Sub RemplirActivite()
        Try
            dtActivite.Columns.Clear()
            dtActivite.Columns.Add("CodeX", Type.GetType("System.String"))
            dtActivite.Columns.Add("Partition", Type.GetType("System.String"))
            dtActivite.Columns.Add("Code", Type.GetType("System.String"))
            dtActivite.Columns.Add("Libellé", Type.GetType("System.String"))
            dtActivite.Columns.Add("Date début", Type.GetType("System.String"))
            dtActivite.Columns.Add("Date fin", Type.GetType("System.String"))
            dtActivite.Columns.Add("Délai", Type.GetType("System.String"))
            dtActivite.Columns.Add("Statut", Type.GetType("System.String"))
            dtActivite.Columns.Add("Progression", Type.GetType("System.String"))
            dtActivite.Columns.Add("Description", Type.GetType("System.String"))
            dtActivite.Columns.Add("Justification", Type.GetType("System.String"))
            dtActivite.Rows.Clear()

            If Not IsDate(Datedebub.Text) And Not IsDate(DateFin.Text) Then
                Exit Sub
            End If

            Dim clause As String = ""

            'conversion de la date
            Dim str(3) As String
            str = Datedebub.Text.Split("/")
            Dim tempdt As String = String.Empty
            For j As Integer = 2 To 0 Step -1
                tempdt += str(j) & "-"
            Next
            tempdt = tempdt.Substring(0, 10)

            Dim str1(3) As String
            str1 = DateFin.Text.Split("/")
            Dim tempdt1 As String = String.Empty
            For j As Integer = 2 To 0 Step -1
                tempdt1 += str1(j) & "-"
            Next
            tempdt1 = tempdt1.Substring(0, 10)

            'Requete Date
            If DateTime.Compare(tempdt1, tempdt) >= 0 Then
                clause = "AND dateDebutPartition >='" & tempdt & "' AND dateFinPartition <='" & tempdt1 & "' order by LibelleCourt"
            Else
                SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
                Exit Sub
            End If

            Dim codeAct() As String
            codeAct = CmbSousCompo.Text.Split(" : ")

            Dim NbTotal As Decimal = 0

            query = "select CodePartition, LibelleCourt, LibellePartition, DateDebutPartition, DateFinPartition, DureePartitionPrevue, StatutPartition, ProgressionPartition, DescPartition, JustifPartition from T_Partition where CodeClassePartition='5'" & IIf(CmbSousCompo.Text <> "", " and CodePartitionMere='" & TxtCodeMere.Text & "'", IIf(CmbCompo.Text <> "", " and LibelleCourt like '" & Mid(CmbCompo.Text, 1, 1) & "%'", "").ToString).ToString & " and CodeProjet='" & ProjetEnCours & "'" & clause
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                NbTotal += 1
                Dim drS = dtActivite.NewRow()
                drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                drS(1) = rw("CodePartition").ToString
                drS(2) = rw("LibelleCourt").ToString
                drS(3) = MettreApost(rw("LibellePartition").ToString)
                drS(4) = CDate(rw("DateDebutPartition")).ToString("dd/MM/yyyy")
                drS(5) = CDate(rw("DateFinPartition")).ToString("dd/MM/yyyy")
                drS(6) = rw("DureePartitionPrevue").ToString & "  "
                drS(7) = rw("StatutPartition").ToString
                drS(8) = rw("ProgressionPartition").ToString & " %"
                drS(9) = MettreApost(rw("DescPartition").ToString)
                drS(10) = MettreApost(rw("JustifPartition").ToString)

                dtActivite.Rows.Add(drS)
            Next

            GridActivite.DataSource = dtActivite

            ViewActivite.Columns(0).Visible = False
            ViewActivite.Columns(1).Visible = False
            ViewActivite.Columns(2).Width = 70
            ViewActivite.Columns(3).Width = 340
            ViewActivite.Columns(4).Width = 100
            ViewActivite.Columns(5).Width = 100
            ViewActivite.Columns(6).Width = 100
            ViewActivite.Columns(7).Width = 100
            ViewActivite.Columns(8).Width = 60
            ViewActivite.Columns(9).Width = 250
            ViewActivite.Columns(10).Width = 250

            ViewActivite.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewActivite.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewActivite.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewActivite.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewActivite.Columns(8).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewActivite.Columns(2).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewActivite.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewActivite.Columns(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

            ViewActivite.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

            ColorRowGrid(ViewActivite, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)


            NbTotActivite = NbTotal
            LblNbActivite.Text = "     " & NbTotal & " Activité" & IIf(NbTotal > 1, "s", "").ToString

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try

    End Sub

    Private Sub BtAjoutActivite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtAjoutActivite.Click
        GbNewActivite.Visible = True
        Modif.Checked = False

        Dim codeAct() As String
        codeAct = CmbSousCompo.Text.Split(" : ")
        TxtCode.Text = CodeNouvelleActivite(codeAct(0).ToString)
    End Sub

    Private Sub GbNewActivite_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GbNewActivite.VisibleChanged

        If (GbNewActivite.Visible = True) Then
            PnlNewActivite.Enabled = False
        Else
            PnlNewActivite.Enabled = True
        End If

    End Sub

    Private Sub BtQuitter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        GbNewActivite.Visible = False
        If Not TxtLibelle.Enabled Then TxtLibelle.Enabled = True

        Modif.Checked = False
        TxtCode.Text = ""
        TxtLibelle.Text = ""
        TxtDescription.Text = ""
        TxtJustification.Text = ""
        DtDateDeb.DateTime = Nothing
        DtDateDeb.Enabled = True
        DtDateFin.DateTime = Nothing
        TxtDelai.Text = ""
        TxtStatut.Text = ""
    End Sub

    Private Sub FichesActivitesEtape1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub CmbSousCompo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSousCompo.SelectedValueChanged

        TxtCodeMere.Text = ""
        If (CmbSousCompo.Text <> "") Then
            BtAjoutActivite.Enabled = True

            Dim codeAct() As String
            codeAct = CmbSousCompo.Text.Split(" : ")


            query = "select CodePartition from T_Partition where CodeClassePartition=2 and LibelleCourt='" & codeAct(0).ToString & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                TxtCodeMere.Text = rw(0).ToString
            Next

        Else
            BtAjoutActivite.Enabled = False
        End If
        RemplirActivite()

    End Sub

    Private Sub RemplirCompo()

        query = "select LibelleCourt, LibellePartition from T_Partition where LENGTH(LibelleCourt)=1 and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
        CmbCompo.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbCompo.Properties.Items.Add(rw(0).ToString & " : " & MettreApost(rw(1).ToString))
        Next
    End Sub

    Private Sub RemplirSousCompo()

        TxtCodeMere.Text = ""
        CmbSousCompo.Text = ""
        CmbSousCompo.Properties.Items.Clear()
        If (CmbCompo.Text <> "") Then
            query = "select LibelleCourt, LibellePartition from T_Partition where CodeClassePartition=2 and LibelleCourt like '" & Mid(CmbCompo.Text, 1, 1) & "%' and CodeProjet='" & ProjetEnCours & "' order by length(libelleCourt),libelleCourt"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                CmbSousCompo.Properties.Items.Add(rw(0).ToString & " : " & MettreApost(rw(1).ToString))
            Next
        End If

    End Sub

    Private Sub CmbCompo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCompo.SelectedValueChanged
        RemplirSousCompo()
        RemplirActivite()
    End Sub

    Private Sub ChangerDemplacement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChangerDemplacement.Click

        If (ViewActivite.RowCount > 0) Then
            DrX = ViewActivite.GetDataRow(ViewActivite.FocusedRowHandle)
            ReponseDialog = DrX(2).ToString
            Dialog_form(ChangerEmplacement)
            RemplirActivite()
        End If

    End Sub

    Private Sub DtDateDeb_DateTimeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DtDateDeb.DateTimeChanged
        On Error Resume Next
        DtDateFin.Text = ""
        DtDateFin.Enabled = True
        DtDateFin.Properties.MinValue = DtDateDeb.DateTime.AddDays(1)
        'If Operation = "Dupliquer" Then
        '    Try
        '        TxtLibelle.Text = LibelleActiviteDupliq & " (" & DtDateFin.DateTime.Year & ")"
        '    Catch ex As Exception
        '    End Try
        'End If
    End Sub

    Private Sub DtDateFin_DateTimeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DtDateFin.DateTimeChanged
        On Error Resume Next
        If DtDateFin.Text <> "" Then

            'convertion de la date en date anglaise
            Dim str(3) As String
            str = DtDateDeb.Text.Split("/")
            Dim tempdt As String = String.Empty
            For j As Integer = 2 To 0 Step -1
                tempdt += str(j) & "-"
            Next
            tempdt = tempdt.Substring(0, 10)

            'convertion de la date en date anglaise
            Dim str1(3) As String
            str1 = DtDateFin.Text.Split("/")
            Dim tempdt1 As String = String.Empty
            For j As Integer = 2 To 0 Step -1
                tempdt1 += str1(j) & "-"
            Next
            tempdt1 = tempdt1.Substring(0, 10)


            Dim delai As String
            'Dim date_jf As Date
            Dim fev As Decimal = 0
            ' Dim Rest_Delai As String'
            delai = DateDiff(DateInterval.Day, DtDateDeb.DateTime.Date, DtDateFin.DateTime.Date) - (2 * DateDiff(DateInterval.Weekday, DtDateDeb.DateTime.Date, DtDateFin.DateTime.Date))
            delai = delai.ToString

            query = "select date_jf from jour_ferier where date_jf >= '" & tempdt & "' And date_jf <= '" & tempdt1 & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            fev = dt.Rows.Count
            delai = CInt(delai) - fev

            TxtDelai.Text = delai.ToString & " Jours"
            If (Date.Compare(Now.ToShortDateString, DtDateDeb.DateTime.Date) >= 0 And Date.Compare(Now.ToShortDateString, DtDateFin.DateTime.Date) <= 0) Then
                TxtStatut.Text = "En cours"
            ElseIf (Date.Compare(Now.ToShortDateString, DtDateDeb.DateTime.Date) < 0) Then
                TxtStatut.Text = "En attente"
            Else
                TxtStatut.Text = "Terminée"
            End If

            'If Operation = "Dupliquer" Then
            '    Try
            '        TxtLibelle.Text = LibelleActiviteDupliq & " (" & DtDateFin.DateTime.Year & ")"
            '    Catch ex As Exception
            '    End Try
            'End If
        End If

    End Sub

    Private Sub BtEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click
        If Operation = "Dupliquer" Then
            If (TxtLibelle.Text <> "" And TxtDescription.Text <> "" And TxtJustification.Text <> "" And TxtDelai.Text <> "" And TxtStatut.Text <> "") Then

                'query = "SELECT * FROM t_partition WHERE (DateDebutPartition>='" & dateconvert(DtDateDeb.Text) & "' AND DateDebutPartition<='" & dateconvert(DtDateFin.Text) & "') OR (DateFinPartition>='" & dateconvert(DtDateDeb.Text) & "' AND DateFinPartition<='" & dateconvert(DtDateFin.Text) & "')"
                query = "SELECT * FROM t_partition WHERE LibelleCourt='" & TxtCode.Text & "' AND ('" & CDate(DtDateDeb.Text).Year & "' BETWEEN YEAR(`DateDebutPartition`) AND YEAR(`DateFinPartition`))"
                'query = "SELECT * FROM t_partition WHERE LibelleCourt='" & TxtCode.Text & "' AND (('" & dateconvert(DtDateDeb.Text) & "' BETWEEN DateDebutPartition AND `DateFinPartition`) OR ('" & dateconvert(DtDateFin.Text) & "' BETWEEN `DateDebutPartition` AND `DateFinPartition`))"
                Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
                If dtVerif.Rows.Count > 0 Then
                    SuccesMsg("L'activité " & TxtCode.Text & " existe déjà sur l'année " & CDate(DtDateDeb.Text).Year & ".")
                    'SuccesMsg("L'activité " & TxtCode.Text & " existe déjà sur la période saisie.")
                    Exit Sub
                End If
                Dim CodeMere As Decimal = Val(ExecuteScallar("SELECT CodePartitionMere FROM t_partition WHERE LibelleCourt='" & TxtCode.Text & "'"))
                query = "insert into T_Partition values (NULL,'" & EnleverApost(TxtLibelle.Text) & "','" & EnleverApost(TxtDescription.Text) & "','" & EnleverApost(TxtJustification.Text) & "','','','" & dateconvert(DtDateDeb.Text) & "','" & TxtDelai.Text & "','Tous les jours','" & dateconvert(DtDateFin.Text) & "','5','" & ProjetEnCours & "','" & CodeMere & "','0','" & TxtCode.Text & "/" & Now() & "','" & TxtCode.Text & "','0','Normale','" & TxtStatut.Text & "','','" & Now.ToShortDateString & " " & Now.ToLongTimeString & "','" & Now.ToShortDateString & " " & Now.ToLongTimeString & "','" & CodeUtilisateur & "')"
                ExecuteNonQuery(query)
                RemplirActivite()
                BtQuitter.PerformClick()
                Operation = "Normal"
                TxtCode.Text = ""
                SuccesMsg("Activité dupliquée avec succès.")
            Else
                SuccesMsg("Formulaire incomplet.")
                Exit Sub
            End If
        ElseIf Operation = "DupliquerCout" Then
            If (TxtLibelle.Text <> "" And TxtDescription.Text <> "" And TxtJustification.Text <> "" And TxtDelai.Text <> "" And TxtStatut.Text <> "") Then

                'query = "SELECT * FROM t_partition WHERE (DateDebutPartition>='" & dateconvert(DtDateDeb.Text) & "' AND DateDebutPartition<='" & dateconvert(DtDateFin.Text) & "') OR (DateFinPartition>='" & dateconvert(DtDateDeb.Text) & "' AND DateFinPartition<='" & dateconvert(DtDateFin.Text) & "')"
                query = "SELECT * FROM t_partition WHERE LibelleCourt='" & TxtCode.Text & "' AND ('" & CDate(DtDateDeb.Text).Year & "' BETWEEN YEAR(`DateDebutPartition`) AND YEAR(`DateFinPartition`))"
                Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
                If dtVerif.Rows.Count > 0 Then
                    SuccesMsg("L'activité " & TxtCode.Text & " existe déjà sur l'année " & CDate(DtDateDeb.Text).Year & ".")
                    Exit Sub
                End If
                Dim CodeMere As Decimal = Val(ExecuteScallar("SELECT CodePartitionMere FROM t_partition WHERE LibelleCourt='" & TxtCode.Text & "'"))
                query = "insert into T_Partition values (NULL,'" & EnleverApost(TxtLibelle.Text) & "','" & EnleverApost(TxtDescription.Text) & "','" & EnleverApost(TxtJustification.Text) & "','','','" & dateconvert(DtDateDeb.Text) & "','" & TxtDelai.Text & "','Tous les jours','" & dateconvert(DtDateFin.Text) & "','5','" & ProjetEnCours & "','" & CodeMere & "','0','" & TxtCode.Text & "/" & Now() & "','" & TxtCode.Text & "','0','Normale','" & TxtStatut.Text & "','','" & Now.ToShortDateString & " " & Now.ToLongTimeString & "','" & Now.ToShortDateString & " " & Now.ToLongTimeString & "','" & CodeUtilisateur & "')"
                ExecuteNonQuery(query)
                Dim GetLastActiviteID As Integer = Val(ExecuteScallar("SELECT MAX(codepartition) FROM t_partition"))
                Dim CodePartition As String = DrX(1).ToString
                query = "SELECT * FROM `t_besoinpartition` WHERE CodePartition='" & CodePartition & "'"
                Dim dtCopieAlloc As DataTable = ExcecuteSelectQuery(query)
                For Each rwCopieAlloc As DataRow In dtCopieAlloc.Rows
                    query = "INSERT INTO t_besoinpartition VALUES(NULL,'" & rwCopieAlloc("CodeRefPrix") & "','" & rwCopieAlloc("CodeNature") & "','" & rwCopieAlloc("LibelleBesoin") & "','" & GetLastActiviteID & "','" & rwCopieAlloc("NumeroComptable") & "','" & rwCopieAlloc("QteNature") & "','" & rwCopieAlloc("PUNature") & "','" & rwCopieAlloc("CodeProjet") & "','" & rwCopieAlloc("UniteBesoin") & "','" & rwCopieAlloc("TypeBesoin") & "','" & rwCopieAlloc("RefMarche") & "')"
                    ExecuteNonQuery(query)
                    Dim GetLastAlloc As Integer = Val(ExecuteScallar("SELECT MAX(RefBesoinPartition) FROM t_besoinpartition WHERE CodeProjet='" & rwCopieAlloc("CodeProjet") & "'"))

                    query = "SELECT * FROM t_repartitionparbailleur WHERE RefBesoinPartition='" & rwCopieAlloc("RefBesoinPartition") & "'"
                    Dim dtRepartition As DataTable = ExcecuteSelectQuery(query)
                    For Each rwRepartition As DataRow In dtRepartition.Rows
                        query = "INSERT INTO t_repartitionparbailleur VALUES(NULL,'" & GetLastAlloc & "','" & rwRepartition("CodeBailleur") & "','" & rwRepartition("MontantBailleur") & "','" & rwRepartition("CodeConvention") & "','" & rwRepartition("RefMarche") & "')"
                        ExecuteNonQuery(query)
                    Next
                Next

                RemplirActivite()
                BtQuitter.PerformClick()
                Operation = "Normal"
                TxtCode.Text = ""
                SuccesMsg("Activité dupliquée avec succès.")
            Else
                SuccesMsg("Formulaire incomplet.")
                Exit Sub
            End If
        Else
            If (Modif.Checked = False) Then

                If (TxtLibelle.Text <> "" And TxtDescription.Text <> "" And TxtJustification.Text <> "" And TxtDelai.Text <> "" And TxtStatut.Text <> "") Then

                    'convertion de la date en date anglaise
                    Dim str(3) As String
                    str = DtDateDeb.Text.Split("/")
                    Dim tempdt As String = String.Empty
                    For j As Integer = 2 To 0 Step -1
                        tempdt += str(j) & "-"
                    Next
                    tempdt = tempdt.Substring(0, 10)

                    'convertion de la date en date anglaise
                    Dim str1(3) As String
                    str1 = DtDateFin.Text.Split("/")
                    Dim tempdt1 As String = String.Empty
                    For j As Integer = 2 To 0 Step -1
                        tempdt1 += str1(j) & "-"
                    Next
                    tempdt1 = tempdt1.Substring(0, 10)


                   query= "insert into T_Partition values (NULL,'" & EnleverApost(TxtLibelle.Text) & "','" & EnleverApost(TxtDescription.Text) & "','" & EnleverApost(TxtJustification.Text) & "','','','" & tempdt & "','" & TxtDelai.Text & "','Tous les jours','" & tempdt1 & "','5','" & ProjetEnCours & "','" & TxtCodeMere.Text & "','0','" & TxtCode.Text & "/" & Now() & "','" & TxtCode.Text & "','0','Normale','" & TxtStatut.Text & "','','" & Now.ToShortDateString & " " & Now.ToLongTimeString & "','" & Now.ToShortDateString & " " & Now.ToLongTimeString & "','" & CodeUtilisateur & "')"
                    ExecuteNonQuery(query)


                    RemplirActivite()

                Else
                    SuccesMsg("Formulaire incomplet.")
                    Exit Sub
                End If

                Dim sc() As String
                sc = CmbSousCompo.Text.Split(" : ")

                TxtCode.Text = CodeNouvelleActivite(sc(0).ToString)

            Else

                'convertion de la date en date anglaise
                Dim str(3) As String
                str = DtDateDeb.Text.Split("/")
                Dim tempdt As String = String.Empty
                For j As Integer = 2 To 0 Step -1
                    tempdt += str(j) & "-"
                Next
                tempdt = tempdt.Substring(0, 10)

                'convertion de la date en date anglaise
                Dim str1(3) As String
                str1 = DtDateFin.Text.Split("/")
                Dim tempdt1 As String = String.Empty
                For j As Integer = 2 To 0 Step -1
                    tempdt1 += str1(j) & "-"
                Next
                tempdt1 = tempdt1.Substring(0, 10)

                Dim CodePartition As String = DrX(1).ToString

                'query = "SELECT * FROM t_partition WHERE LibelleCourt='" & TxtCode.Text & "' AND (('" & dateconvert(DtDateDeb.Text) & "' BETWEEN DateDebutPartition AND `DateFinPartition`) OR ('" & dateconvert(DtDateFin.Text) & "' BETWEEN `DateDebutPartition` AND `DateFinPartition`))"
                'Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
                'If dtVerif.Rows.Count > 0 Then
                '    SuccesMsg("L'activité " & TxtCode.Text & " existe déjà sur la période saisie.")
                '    Exit Sub
                'End If

                query = "Update T_Partition set LibellePartition='" & EnleverApost(TxtLibelle.Text) & "', DescPartition='" & EnleverApost(TxtDescription.Text) & "', JustifPartition='" & EnleverApost(TxtJustification.Text) & "', DateDebutPartition='" & tempdt & "', DateFinPartition='" & tempdt1 & "', DureePartitionPrevue='" & TxtDelai.Text & "', StatutPartition='" & TxtStatut.Text & "', DateModif='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "', Operateur='" & CodeUtilisateur & "' where LibelleCourt='" & TxtCode.Text & "' and CodeProjet='" & ProjetEnCours & "' AND CodePartition='" & CodePartition & "'"
                ExecuteNonQuery(query)

                'On met a jour le libelle des activites dupliquees
                query = "Update T_Partition set LibellePartition='" & EnleverApost(TxtLibelle.Text) & "' where LibelleCourt='" & TxtCode.Text & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

                RemplirActivite()
                GbNewActivite.Visible = False
                TxtCode.Text = ""

            End If
        End If

        Modif.Checked = False
        TxtLibelle.Text = ""
        TxtDescription.Text = ""
        TxtJustification.Text = ""
        DtDateDeb.DateTime = Nothing
        DtDateDeb.Enabled = True
        DtDateFin.DateTime = Nothing
        TxtDelai.Text = ""
        TxtStatut.Text = ""

    End Sub

    Private Sub GridActivite_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridActivite.DoubleClick

        If (ViewActivite.RowCount > 0) Then
            'LibelleActiviteDupliq = String.Empty
            Operation = "Normal"
            DrX = ViewActivite.GetDataRow(ViewActivite.FocusedRowHandle)
            Dim CodePartition As String = DrX(1).ToString
            ColorRowGrid(ViewActivite, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewActivite, "[Partition]='" & CodePartition & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)

            If TxtLibelle.Enabled = False Then TxtLibelle.Enabled = True
            TxtLibelle.Focus()

            GbNewActivite.Visible = True
            Modif.Checked = True
            TxtCode.Text = DrX(2).ToString
            TxtLibelle.Text = DrX(3).ToString
            TxtDescription.Text = DrX(9).ToString
            TxtJustification.Text = DrX(10).ToString
            DtDateDeb.DateTime = CDate(DrX(4).ToString).ToShortDateString
            DtDateFin.DateTime = CDate(DrX(5).ToString).ToShortDateString
            DtDateFin.Enabled = False
        End If

    End Sub

    Private Sub GridActivite_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridActivite.MouseUp

        If (ViewActivite.RowCount > 0) Then
            DrX = ViewActivite.GetDataRow(ViewActivite.FocusedRowHandle)
            Dim CodePartition As String = DrX(1).ToString
            ColorRowGrid(ViewActivite, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewActivite, "[Partition]='" & CodePartition & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)
        End If

    End Sub

    Private Sub SupprimerActivite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SupprimerActivite.Click
        If (ViewActivite.RowCount > 0) Then
            If ViewActivite.FocusedRowHandle > -1 Then
                DrX = ViewActivite.GetDataRow(ViewActivite.FocusedRowHandle)
                Dim CodParttition As String = DrX(1).ToString
                Dim LibelleCourt As String = DrX("Code").ToString
                query = "SELECT COUNT(codepartition) FROM t_comp_activite WHERE codepartition='" & CodParttition & "'"
                If Val(ExecuteScallar(query)) > 0 Then
                    FailMsg("Vous ne pouvez pas supprimer cetet activité car elle est utilisée en Comptabilité.")
                    Exit Sub
                End If

                query = "SELECT COUNT(LibelleCourt) FROM t_acteng a, t_marchesigne s WHERE s.RefMarche=a.RefMarche AND (YEAR(STR_TO_DATE(DateMarche,'%d/%m/%Y')) BETWEEN '" & CDate(DrX("Date début")).Year & "' AND '" & CDate(DrX("Date fin")).Year & "') AND a.LibelleCourt='" & LibelleCourt & "'"
                If Val(ExecuteScallar(query)) > 0 Then
                    FailMsg("Vous ne pouvez pas supprimer cette activité car elle est utilisée dans la saisie des engagements.")
                    Exit Sub
                End If

                Dim RepSupp As DialogResult = ConfirmMsg("Confirmez-vous la suppression de l'activité " & DrX(2).ToString & "?")
                If (RepSupp = DialogResult.Yes) Then

                    query = "delete from t_operateurpartition where CodePartition='" & DrX(1).ToString & "'"
                    ExecuteNonQuery(query)

                    query = "delete from t_echeanceactivite where CodePartition='" & DrX(1).ToString & "'"
                    ExecuteNonQuery(query)

                    query = "delete from t_indicateurpartition where CodePartition='" & DrX(1).ToString & "'"
                    ExecuteNonQuery(query)

                    query = "delete from t_besoinpartition where CodePartition='" & DrX(1).ToString & "'"
                    ExecuteNonQuery(query)

                    query = "delete from T_Partition where CodePartition='" & DrX(1).ToString & "' and CodeProjet='" & ProjetEnCours & "'"
                    ExecuteNonQuery(query)

                    RemplirActivite()
                End If
            End If
        End If
    End Sub

    Private Sub BtImportFiche_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtImportFiche.Click
        Try
            Dim OpFile As New OpenFileDialog
            OpFile.Filter = "Excel|*.xlsx;*.xls"
            If OpFile.ShowDialog() = DialogResult.OK Then
                DebutChargement(True, "Vérification des données du fichier Excel en cours...")
                Textimpfiche.Text = OpFile.FileName
                Dim app As New Excel.Application
                app.Workbooks.Open(OpFile.FileName)
                Dim AucuneImputation As Boolean = False 'Va nous permettre de savoir si aucune ligne d'imputation n'a étét défini
                Dim LstActivite As New List(Of String) 'Va nous permettre de savoir si aucun coût direct n'a été défini
                Dim Contents As New List(Of Object) 'On va sauvegarder toutes les coûts directs pendant le processus de verification afin de ne pas reparcourir le fichier pour l'importation
                Dim Headers As New List(Of Object) 'On va sauvegarder toutes les infos relatives aux fiches durant le processus de verification afin de ne pas reparcourir le fichier pour l'importation
                Dim Conventions As New List(Of Object) 'On va sauvegarder toutes les infos relatives aux fiches durant le processus de verification afin de ne pas reparcourir le fichier pour l'importation
                For i As Integer = 1 To app.Workbooks(1).Worksheets.Count()
                    Dim Feuille = app.Workbooks(1).Worksheets(i)
                    Dim Titre As String = Feuille.Range("A1").Value
                    If Titre <> "FICHE D'ACTIVITÉ" Then
                        FinChargement()
                        FailMsg("La feuille de calcul " & Feuille.Name & " n'a pas le bon format d'importation")
                        app.Quit()
                        Exit Sub
                    End If
                    Dim NomProjet As String = ""
                    Dim CodeProjet As String = ""
                    Dim DateDebutActivite As String = ""
                    Dim DateFinActivite As String = ""
                    Dim LibelleActivite As String = ""
                    Dim LieuActivite As String = ""
                    Dim ResponsableActivite As String = ""
                    Dim Composante As String = ""
                    Dim SousComposante As String = ""
                    Dim Description As String = ""
                    Dim Justification As String = ""
                    Dim ResultatActivite As String = ""
                    Dim DateElaboration As String = ""
                    Dim IndicateurPerformance As String = ""
                    Dim Editeur As String = ""
                    Dim RowCount = Feuille.Cells(Feuille.Rows.Count, 1).End(Excel.XlDirection.xlUp).Row
                    Dim ColCount = Feuille.Cells.Find("*", , , , Excel.XlSearchOrder.xlByColumns, Excel.XlSearchDirection.xlPrevious).Column
                    Dim nbConvention As Decimal = ColCount - 7
                    Dim GetToatl As Decimal = RowCount - 3
                    Try
                        NomProjet = Feuille.Range("B2").Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        CodeProjet = Feuille.Range("B3").Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        DateDebutActivite = Feuille.Range("B4").Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        DateFinActivite = Feuille.Range("B5").Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        LibelleActivite = Feuille.Range("C8").Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        LieuActivite = Feuille.Range("B6").Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        ResponsableActivite = Feuille.Range("B7").Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        Composante = Feuille.Range("C9").Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        SousComposante = Feuille.Range("C10").Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        Description = Feuille.Range("C11").Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        Justification = Feuille.Range("C12").Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        ResultatActivite = Feuille.Range("C" & (RowCount - 1)).Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        DateElaboration = Feuille.Range("E" & RowCount).Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        IndicateurPerformance = Feuille.Range("C" & (RowCount - 2)).Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        Editeur = Feuille.Range("B" & RowCount).Value.ToString()
                    Catch ex As Exception
                    End Try

                    If NomProjet.ToString().Length = 0 Then
                        FinChargement()
                        FailMsg("Entrer le nom du projet sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    If DateDebutActivite.ToString().Length = 0 Or Not IsDate(DateDebutActivite.ToString()) Then
                        FinChargement()
                        FailMsg("Entrer la date de début de l'activité sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    If DateFinActivite.ToString().Length = 0 Or Not IsDate(DateFinActivite.ToString()) Then
                        FinChargement()
                        FailMsg("Entrer la date de fin de l'activité sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    If CDate(DateDebutActivite) > CDate(DateFinActivite) Then
                        FinChargement()
                        FailMsg("La date de début de l'activité ne peut pas supérieur à la date de fin sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    ElseIf CDate(DateDebutActivite) = CDate(DateFinActivite) Then
                        FinChargement()
                        FailMsg("La date de début de l'activité ne peut pas être égale à la date de fin sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    If LieuActivite.ToString().Length = 0 Then
                        FinChargement()
                        FailMsg("Entrer le lieu de l'activité sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    If ResponsableActivite.ToString().Length = 0 Then
                        FinChargement()
                        FailMsg("Entrer le nom et prénoms du responsable de l'activité sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    If LibelleActivite.ToString().Length = 0 Then
                        FinChargement()
                        FailMsg("Entrer le libellé de l'activité sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    If Composante.ToString().Length = 0 Then
                        FinChargement()
                        FailMsg("Entrer la composante de l'activité sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    If SousComposante.ToString().Length = 0 Then
                        FinChargement()
                        FailMsg("Entrer la sous composante de l'activité sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    If Description.ToString().Length = 0 Then
                        FinChargement()
                        FailMsg("Entrer la description de l'activité sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    If Justification.ToString().Length = 0 Then
                        FinChargement()
                        FailMsg("Entrer la justification de l'activité sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    If IndicateurPerformance.ToString().Length = 0 Then
                        FinChargement()
                        FailMsg("Entrer l'indicateur de performance sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    If ResultatActivite.ToString().Length = 0 Then
                        FinChargement()
                        FailMsg("Entrer le résultat attendu de l'activité sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    If Editeur.ToString().Length = 0 Then
                        FinChargement()
                        FailMsg("Entrer le nom et prénoms de celui qui a établi l'activité sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    If DateElaboration.ToString().Length = 0 Or Not IsDate(DateElaboration.ToString()) Then
                        FinChargement()
                        FailMsg("Entrer la date d'élaboration sur la " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If

                    query = "SELECT CodeProjet from t_projet WHERE CodeProjet='" & CodeProjet & "'"
                    Dim dtResult As DataTable = ExcecuteSelectQuery(query)
                    If dtResult.Rows.Count = 0 Then
                        FinChargement()
                        FailMsg("Le code du projet de la feuille " & Feuille.Name & " n'existe pas.")
                        app.Quit()
                        Exit Sub
                    End If

                    query = "SELECT EMP_ID from t_grh_employe WHERE TRIM(CONCAT(EMP_NOM,' ',EMP_PRENOMS))='" & EnleverApost(ResponsableActivite) & "'"
                    dtResult = ExcecuteSelectQuery(query)
                    If dtResult.Rows.Count = 0 Then
                        FinChargement()
                        FailMsg("Le responsable de la feuille " & Feuille.Name & " n'existe pas.")
                        app.Quit()
                        Exit Sub
                    Else
                        '    query = "SELECT * from t_operateur WHERE TRIM(CONCAT(NomOperateur,' ',PrenOperateur))='" & EnleverApost(ResponsableActivite) & "'"
                        '    dtResult = ExcecuteSelectQuery(query)
                        '    If dtResult.Rows.Count = 0 Then
                        '        FinChargement()
                        '        FailMsg("Le responsable de la feuille " & Feuille.Name & " n'a pas de compte dans d'accès.")
                        '        app.Quit()
                        '        Exit Sub

                        '        'Dim EMP_ID As Decimal = dtResult.Rows(0).Item("EMP_ID")
                        '        'query = "SELECT * FROM t_grh_employe WHERE EMP_ID=" & EMP_ID
                        '        'dtResult = ExcecuteSelectQuery(query)
                        '        'Dim rwResult As DataRow = dtResult.Rows(0)
                        '        'Dim Civilite As String = ""
                        '        'If rwResult("EMP_CIV") = "Madame" Then
                        '        '    Civilite = "Mme"
                        '        'ElseIf rwResult("EMP_CIV") = "Mademoiselle" Then
                        '        '    Civilite = "Mlle"
                        '        'Else
                        '        '    Civilite = "M."
                        '        'End If
                        '        'Dim Login = GetNewLogin(rwResult("EMP_NOM"), rwResult("EMP_PRENOMS"))
                        '        'query = "INSERT INTO t_operateur VALUES(NULL, " & rwResult("EMP_ID") & ",'" & rwResult("EMP_MAT") & "','" & Civilite & "','" & rwResult("EMP_NOM") & "','" & rwResult("EMP_PRENOMS") & "','" & rwResult("EMP_DATENAIS") & "','" & rwResult("EMP_LIEUNAIS") & "','" & rwResult("EMP_NATION") & "','" & rwResult("EMP_CONTACT") & "','" & rwResult("EMP_ADRESSE") & "','" & rwResult("EMP_EMAIL") & "','" & rwResult("EMP_DATA") & "','','','','','O','01/01/1900','31/01/1900','" & rwResult("EMP_ADRESSE") & "','','" & Login & "','P@ss','Niveau0','" & ProjetEnCours & "','" & Now & "','" & Now & "','" & CodeUtilisateur & "','0','S25')"
                        '        'Try
                        '        '    ExecuteNonQuery(query)
                        '        'Catch ex As Exception
                        '        '    FinChargement()
                        '        '    FailMsg("Nous n'arrivons pas a créé l'accès du responsable la feuille " & Feuille.Name & " n'existe pas.")
                        '        '    app.Quit()
                        '        '    Exit Sub
                        '        'End Try
                        '    End If
                    End If

                    query = "SELECT CodeZone from t_zonegeo WHERE LibelleZone='" & EnleverApost(LieuActivite) & "'"
                    dtResult = ExcecuteSelectQuery(query)
                    If dtResult.Rows.Count = 0 Then
                        FinChargement()
                        FailMsg("Le lieu défini pour l'activité sur la feuille " & Feuille.Name & " n'existe pas.")
                        app.Quit()
                        Exit Sub
                    End If

                    query = "SELECT codepartition from t_partition WHERE LibellePartition='" & EnleverApost(Composante) & "' and CodeClassePartition='1'"
                    dtResult = ExcecuteSelectQuery(query)
                    If dtResult.Rows.Count = 0 Then
                        FinChargement()
                        FailMsg("La composante définie pour l'activité sur la feuille " & Feuille.Name & " n'existe pas.")
                        app.Quit()
                        Exit Sub
                    End If

                    query = "SELECT codepartition from t_partition WHERE CodePartitionMere=" & dtResult.Rows(0).Item("codepartition") & " and LibellePartition='" & EnleverApost(SousComposante) & "' and CodeClassePartition='2'"
                    dtResult = ExcecuteSelectQuery(query)
                    If dtResult.Rows.Count = 0 Then
                        FinChargement()
                        FailMsg("La sous composante " & SousComposante & " n'existe pas sur la composante " & Composante & " sur la feuille " & Feuille.Name & ".")
                        app.Quit()
                        Exit Sub
                    End If

                    query = "SELECT EMP_ID from t_grh_employe WHERE TRIM(CONCAT(EMP_NOM,' ',EMP_PRENOMS))='" & EnleverApost(Editeur) & "'"
                    dtResult = ExcecuteSelectQuery(query)
                    If dtResult.Rows.Count = 0 Then
                        FinChargement()
                        FailMsg("L'employé qui a préparé la fiche d'activité sur la feuille " & Feuille.Name & " n'existe pas.")
                        app.Quit()
                        Exit Sub
                    End If

                    Dim Titreconvention As String = Feuille.Range("G14").Value
                    If Titreconvention <> "Répartition du montant par convention" Then
                        FinChargement()
                        FailMsg("La feuille de calcul " & Feuille.Name & " n'a pas le bon format d'importation")
                        app.Quit()
                        Exit Sub
                    End If
                    Dim TitreTotal As String = Feuille.Range("C" & GetToatl).Value
                    If TitreTotal <> "TOTAL" Then
                        FinChargement()
                        FailMsg("La feuille de calcul " & Feuille.Name & " n'a pas le bon format d'importation")
                        app.Quit()
                        Exit Sub
                    End If

                    If nbConvention <= 0 Then
                        FinChargement()
                        FailMsg("La feuille de calcul " & Feuille.Name & " n'a pas le bon format d'importation")
                        app.Quit()
                        Exit Sub
                    End If
                    Dim dtConvention As New DataTable
                    dtConvention.Columns.Add("CodeConvention")
                    dtConvention.Rows.Clear()
                    For l = 8 To ColCount
                        Dim Conv As String = ""
                        Try
                            Conv = Feuille.Cells(15, l).Value.ToString()
                        Catch ex As Exception
                        End Try
                        If Conv.Length = 0 Then
                            FinChargement()
                            FailMsg("Veuillez entrer toutes les conventions sur la feuille " & Feuille.Name & ".")
                            app.Quit()
                            Exit Sub
                        Else
                            query = "SELECT CodeConvention from t_convention WHERE CodeConvention='" & EnleverApost(Conv) & "'"
                            dtResult = ExcecuteSelectQuery(query)
                            If dtResult.Rows.Count = 0 Then
                                FinChargement()
                                FailMsg("La convention " & Conv & " de la feuille " & Feuille.Name & " n'existe pas.")
                                app.Quit()
                                Exit Sub
                            Else
                                dtConvention.Rows.Add(Conv)
                            End If
                        End If
                    Next

                    'Enregistrement des conventions dans notre liste
                    Conventions.Add(dtConvention)

                    Dim doublonConv As Boolean = False
                    Dim ConvDouble As String = ""
                    For k = 0 To (dtConvention.Rows.Count - 2)
                        If dtConvention.Rows(k).Item("CodeConvention") = dtConvention.Rows(k + 1).Item("CodeConvention") Then
                            doublonConv = True
                            ConvDouble = dtConvention.Rows(k).Item("CodeConvention")
                        End If
                    Next
                    If doublonConv Then
                        FinChargement()
                        FailMsg("La convention " & ConvDouble & " est repétée sur la feuille " & Feuille.Name & ".")
                        app.Quit()
                        Exit Sub
                    End If

                    'On va sauvegarder notre entête dans notre liste d'objet
                    Dim dtHeaders As New DataTable
                    dtHeaders.Columns.Add("CodeProjet")
                    dtHeaders.Columns.Add("DebutActivite")
                    dtHeaders.Columns.Add("FinActivite")
                    dtHeaders.Columns.Add("Lieu")
                    dtHeaders.Columns.Add("Responsable")
                    dtHeaders.Columns.Add("TitreActivite")
                    dtHeaders.Columns.Add("Composante")
                    dtHeaders.Columns.Add("SousComposante")
                    dtHeaders.Columns.Add("Description")
                    dtHeaders.Columns.Add("Justification")
                    dtHeaders.Columns.Add("IndicateurPerformance")
                    dtHeaders.Columns.Add("ResultatAttendu")
                    dtHeaders.Columns.Add("Editeur")
                    dtHeaders.Columns.Add("DateElaboration")
                    dtHeaders.Rows.Clear()
                    dtHeaders.Rows.Add(CodeProjet, DateDebutActivite, DateFinActivite, LieuActivite, ResponsableActivite, LibelleActivite, Composante, SousComposante, Description, Justification, IndicateurPerformance, ResultatActivite, Editeur, DateElaboration)
                    Headers.Add(dtHeaders)

                    Dim dtLigne As New DataTable
                    dtLigne.Columns.Add("Compte")
                    dtLigne.Columns.Add("LibBesoin")
                    dtLigne.Columns.Add("Qte")
                    dtLigne.Columns.Add("Unite")
                    dtLigne.Columns.Add("CU")
                    dtLigne.Columns.Add("Montant")
                    For k = 0 To (dtConvention.Rows.Count - 1)
                        dtLigne.Columns.Add("MontantConv" & k)
                    Next
                    dtLigne.Rows.Clear()
                    'On parcoure les lignes des imputations
                    For l = 16 To (GetToatl - 1) 'On va de la ligne 14 jusqu'a la ligne qui précède le TOTAL
                        Dim Compte As String = ""
                        Dim LibBesoin As String = ""
                        Dim Qte As String = ""
                        Dim Unite As String = ""
                        Dim CU As String = ""
                        Dim Montant As String = ""
                        Try
                            Compte = Feuille.Range("A" & l).Value.ToString()
                        Catch ex As Exception
                        End Try
                        Try
                            LibBesoin = Feuille.Range("B" & l).Value.ToString()
                        Catch ex As Exception
                        End Try
                        Try
                            Qte = Feuille.Range("C" & l).Value.ToString()
                        Catch ex As Exception
                        End Try
                        Try
                            Unite = Feuille.Range("D" & l).Value.ToString()
                        Catch ex As Exception
                        End Try
                        Try
                            CU = Feuille.Range("E" & l).Value.ToString()
                        Catch ex As Exception
                        End Try
                        Try
                            Montant = Feuille.Range("F" & l).Value.ToString()
                        Catch ex As Exception
                        End Try

                        If Val(Compte) = 0 And LibBesoin.Length = 0 And Val(Qte) = 0 And Unite.Length = 0 And Val(CU) = 0 And Val(Montant) = 0 Then
                            If Not LstActivite.Contains(Feuille.Name) Then
                                LstActivite.Add(Feuille.Name)
                            End If
                            Continue For
                        End If

                        If LibBesoin.Length = 0 Then
                            FinChargement()
                            FailMsg("Veuillez entrer le libellé de la dépense à la ligne " & l & " de la feuille " & Feuille.Name & ".")
                            app.Quit()
                            Exit Sub
                        End If

                        If Val(Qte) = 0 Then
                            FinChargement()
                            FailMsg("Veuillez entrer la quantité à la ligne " & l & " de la feuille " & Feuille.Name & ".")
                            app.Quit()
                            Exit Sub
                        End If

                        If Unite.Length = 0 Then
                            FinChargement()
                            FailMsg("Veuillez entrer l'unité de la dépense à la ligne " & l & " de la feuille " & Feuille.Name & ".")
                            app.Quit()
                            Exit Sub
                        Else
                            query = "SELECT LibelleUnite from t_unite WHERE LibelleUnite='" & EnleverApost(Unite) & "'"
                            dtResult = ExcecuteSelectQuery(query)
                            If dtResult.Rows.Count = 0 Then
                                FinChargement()
                                FailMsg("L'unité " & Unite & " à la ligne " & l & " de la feuille " & Feuille.Name & " n'existe pas.")
                                app.Quit()
                                Exit Sub
                            End If
                        End If

                        If Val(CU) = 0 Then
                            FinChargement()
                            FailMsg("Veuillez entrer le coût unitaire à la ligne " & l & " de la feuille " & Feuille.Name & ".")
                            app.Quit()
                            Exit Sub
                        End If

                        If Val(Montant) = 0 Then
                            FinChargement()
                            FailMsg("Veuillez entrer le montant total de la dépense à la ligne " & l & " de la feuille " & Feuille.Name & ".")
                            app.Quit()
                            Exit Sub
                        Else
                            If Val(Montant) <> Val(CU) * Val(Qte) Then
                                FinChargement()
                                FailMsg("Le montant total défini à la ligne " & l & " doit être " & Val(CU) * Val(Qte) & " de la feuille " & Feuille.Name & ".")
                                app.Quit()
                                Exit Sub
                            End If
                        End If
                        Dim MTotalConvention As Decimal = 0
                        For k = 8 To ColCount
                            Dim MontantConv As String = ""
                            Try
                                If Val(Feuille.Cells(l, k).Value.ToString()) = 0 Then
                                    Dim Conv = Feuille.Cells(15, k).Value.ToString()
                                    FinChargement()
                                    FailMsg("Entrer correctement le montant de la convention " & Conv & " à la ligne " & l & " de la feuille " & Feuille.Name & ".")
                                    app.Quit()
                                    Exit Sub
                                End If
                                MontantConv = Feuille.Cells(l, k).Value.ToString()
                                MTotalConvention += Val(MontantConv)
                            Catch ex As Exception
                            End Try
                        Next

                        If MTotalConvention <> Val(Montant) Then
                            FinChargement()
                            FailMsg("Veuillez répartir correctement le montant total de la dépense sur les conventions à la ligne " & l & " sur la feuille " & Feuille.Name & ".")
                            app.Quit()
                            Exit Sub
                        End If
                        Dim NewRow As DataRow = dtLigne.NewRow
                        NewRow("Compte") = Compte
                        NewRow("LibBesoin") = LibBesoin
                        NewRow("Qte") = Qte
                        NewRow("Unite") = Unite
                        NewRow("CU") = CU
                        NewRow("Montant") = Montant
                        For k = 8 To ColCount
                            Dim MontantConv As String = ""
                            Try
                                If Val(Feuille.Cells(l, k).Value.ToString()) = 0 Then
                                    Dim Conv = Feuille.Cells(15, k).Value.ToString()
                                    FinChargement()
                                    FailMsg("Entrer correctement le montant de la convention " & Conv & " à la ligne " & l & " de la feuille " & Feuille.Name & ".")
                                    app.Quit()
                                    Exit Sub
                                End If
                                MontantConv = Feuille.Cells(l, k).Value.ToString()
                                MTotalConvention += Val(MontantConv)
                                NewRow("MontantConv" & (k - 8)) = Val(MontantConv)
                            Catch ex As Exception
                                NewRow("MontantConv" & (k - 8)) = 0
                            End Try
                        Next
                        dtLigne.Rows.Add(NewRow)
                        AucuneImputation = True
                    Next
                    Contents.Add(dtLigne)
                Next

                If Not AucuneImputation Then
                    Dim str As String = String.Empty
                    For i = 0 To (LstActivite.Count - 1)
                        str += "=> " & LstActivite.Item(i)
                    Next
                    FinChargement()
                    If MessageBox.Show("Nous avons détecté des fiches sans coûts directs :" & vbNewLine & str & vbNewLine & "Voulez-vous continuer l'importation?", "ClearProject", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                        app.Quit()
                        Exit Sub
                    End If
                Else
                    If MessageBox.Show("Vérification du fichier terminée avec succès." & "Voulez-vous commencer l'importation?", "ClearProject", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                        app.Quit()
                        Exit Sub
                    End If
                End If


                DebutChargement(True, "Importation des données Excel en cours...")
                app.Quit()
                Threading.Thread.Sleep(2000)
                'FinChargement()
                Dim countError As Decimal = 0
                Dim Errors As String = String.Empty
                Dim nbFiche As Decimal = Headers.Count
                For i = 0 To (nbFiche - 1)
                    Dim dtHeader As DataTable = CType(Headers(i), DataTable)
                    Dim dtContent As DataTable = CType(Contents(i), DataTable)
                    Dim dtConvention As DataTable = CType(Conventions(i), DataTable)
                    For k = 0 To (dtHeader.Rows.Count - 1)
                        Dim rwHeader As DataRow = dtHeader.Rows(k)
                        'On enregistre les activités
                        'On collecte les données pour inserer la fiche d'activité
                        'Declaration des variables
                        Dim DateDeb As Date = CDate(rwHeader("DebutActivite"))
                        Dim Datefin As Date = CDate(rwHeader("FinActivite"))
                        Dim CodeComposante As Decimal = 0
                        Dim IdComposante As String = String.Empty
                        Dim CodeSousComposante As Decimal = 0
                        Dim IdSousComposante As String = String.Empty
                        Dim Durree As String = DateDiff(DateInterval.Day, DateDeb, Datefin)
                        Dim CodeZoNe As Integer = -1
                        Dim StatutActivite As String = String.Empty
                        Dim IndicateurPerformance As String = rwHeader("IndicateurPerformance")

                        query = "select date_jf from jour_ferier where date_jf >= '" & dateconvert(DateDeb) & "' And date_jf <= '" & dateconvert(Datefin) & "'"
                        Dim dtFerier = ExcecuteSelectQuery(query)
                        Durree = CInt(Durree) - dtFerier.Rows.Count  'On retranche dans le nombre de jour total de l'activite le nombre total de jour ferie

                        query = "select codepartition,LibelleCourt from T_Partition where LENGTH(LibelleCourt)=1 and  libellepartition='" & EnleverApost(rwHeader("Composante")) & "'"
                        Dim dt As DataTable = ExcecuteSelectQuery(query)
                        For Each rw As DataRow In dt.Rows
                            CodeComposante = rw("codepartition")
                            IdComposante = rw("LibelleCourt")
                        Next
                        query = "select codepartition,LibelleCourt from T_Partition where LENGTH(LibelleCourt)=2 and CodePartitionMere=" & CodeComposante & " and libellepartition='" & EnleverApost(rwHeader("SousComposante")) & "'"
                        dt = ExcecuteSelectQuery(query)
                        For Each rw As DataRow In dt.Rows
                            CodeSousComposante = rw("codepartition")
                            IdSousComposante = rw("LibelleCourt")
                        Next

                        Dim codeact As String = ""
                        Dim codemere As String = ""

                        'Code automatique de la sous composante
                        codeact = CodeNouvelleActivite1(EnleverApost(rwHeader("SousComposante")))

                        If Date.Compare(Now.ToShortDateString, CDate(DateDeb)) >= 0 And Date.Compare(Now.ToShortDateString, CDate(Datefin)) < 0 Then
                            StatutActivite = "En cours"
                        ElseIf (Date.Compare(Now.ToShortDateString, CDate(DateDeb)) < 0) Then
                            StatutActivite = "En attente"
                        Else
                            StatutActivite = "Terminée"
                        End If

                        'Recuperation du code du lieu
                        query = "select CodeZone from T_ZoneGeo where LibelleZone='" & EnleverApost(rwHeader("Lieu")) & "'"
                        dt = ExcecuteSelectQuery(query)
                        If dt.Rows.Count > 0 Then
                            CodeZone = Val(dt.Rows(0).Item("CodeZone").ToString)
                        End If

                        'Insertion de l'activité
                        Dim CodeNewActivite As String = String.Empty
                        Try
                            query = "insert into T_Partition values (NULL,'" & Mid(EnleverApost(rwHeader("TitreActivite")), 1, 1000) & "','" & Mid(EnleverApost(rwHeader("Description")), 1, 1500) & "','" & Mid(EnleverApost(rwHeader("Justification")), 1, 1500) & "','" & Mid(EnleverApost(rwHeader("ResultatAttendu")), 1, 500) & "','','" & dateconvert(DateDeb) & "','" & Durree.ToString & " Jours" & "','Tous les jours','" & dateconvert(Datefin) & "','5','" & ProjetEnCours & "','" & CodeSousComposante & "','" & CodeZone & "','" & codeact.ToString & "/" & Now() & "','" & codeact.ToString & "','0','Normale','" & StatutActivite & "','','" & Now.ToShortDateString & " " & Now.ToLongTimeString & "','" & Now.ToShortDateString & " " & Now.ToLongTimeString & "','" & CodeUtilisateur & "')"
                            ExecuteNonQuery(query)
                            CodeNewActivite = ExecuteScallar("SELECT MAX(codepartition) FROM T_Partition")
                        Catch ex As Exception
                            FinChargement()
                            Dim rep As DialogResult = MessageBox.Show("Nous n'avons pas pu importer l'activité " & rwHeader("TitreActivite") & vbNewLine & "Voulez-vous continuer l'importation?", "ClearProject", MessageBoxButtons.YesNo, MessageBoxIcon.Error)
                            If rep = DialogResult.No Then
                                Exit Sub
                            Else
                                DebutChargement(True, "Importation des données Excel en cours...")
                                countError += 1
                                Errors += rwHeader("TitreActivite") & vbNewLine
                            End If
                        End Try


                        'Enregistrements des besoins de l'activite
                        For l = 0 To (dtContent.Rows.Count - 1)
                            'Declaration des variables
                            Dim rwCoutDirect = dtContent.Rows(l)
                            Dim TypeCompte As String
                            Dim Compte As String = rwCoutDirect("Compte")
                            Dim LibBesoin As String = rwCoutDirect("LibBesoin")
                            Dim Qte As String = rwCoutDirect("Qte")
                            Dim Unite As String = rwCoutDirect("Unite")
                            Dim CoutUnitaire As String = rwCoutDirect("CU")
                            Dim Montant As String = rwCoutDirect("Montant")
                            Dim CodeBailleur As String = rwCoutDirect("Montant")
                            'dtLigne.Columns.Add("Compte")
                            'dtLigne.Columns.Add("LibBesoin")
                            'dtLigne.Columns.Add("Qte")
                            'dtLigne.Columns.Add("Unite")
                            'dtLigne.Columns.Add("CU")
                            'dtLigne.Columns.Add("Montant")
                            'For k = 0 To (dtConvention.Rows.Count - 1)
                            '    dtLigne.Columns.Add("MontantConv" & k)
                            'Next
                            query = "select TypeCompte from T_COMP_SOUS_CLASSE where Code_sc='" & Compte & "'"
                            TypeCompte = ExecuteScallar(query)
                            If TypeCompte.ToString = "FR" Then
                                TypeCompte = "Fournitures"
                            ElseIf TypeCompte.ToString = "TX" Then
                                TypeCompte = "Travaux"
                            ElseIf TypeCompte.ToString = "SA" Then
                                TypeCompte = "Services autres que les services de consultants"
                            ElseIf TypeCompte.ToString = "CS" Then
                                TypeCompte = "Consultants"
                            End If

                            Try
                                query = "insert into T_BesoinPartition values(NULL,'',0,'" & EnleverApost(LibBesoin) & "'," & CodeNewActivite & ",'" & Compte & "','" & Qte & "','" & CoutUnitaire & "','" & ProjetEnCours & "','" & Unite & "','" & TypeCompte & "','0')"
                                ExecuteNonQuery(query)
                            Catch ex As Exception
                                query = "DELETE FROM T_BesoinPartition WHERE CodePartition='" & CodeNewActivite & "'"
                                ExecuteNonQuery(query)
                                query = "DELETE FROM t_partition WHERE codepartition='" & CodeNewActivite & "'"
                                ExecuteNonQuery(query)
                                countError += 1
                                Errors += rwHeader("TitreActivite") & vbNewLine
                            End Try
                            Dim LastBesoinPartition As String = ExecuteScallar("SELECT MAX(RefBesoinPartition) FROM T_BesoinPartition")

                            For c = 6 To (dtContent.Columns.Count - 1)
                                Dim TConv As String = dtContent.Columns(c).ColumnName
                                Dim MontConv As Decimal = CDec(rwCoutDirect(TConv))
                                If MontConv > 0 Then
                                    Dim Conv As String = dtConvention.Rows((c - 6)).Item("CodeConvention")
                                    Dim Bailleur As String = ExecuteScallar("select CodeBailleur from t_convention where CodeConvention='" & Conv & "'")
                                    Try
                                        query = "insert into T_RepartitionParBailleur values(NULL,'" & LastBesoinPartition & "','" & Bailleur & "','" & MontConv & "','" & Conv & "','0')"
                                        ExecuteNonQuery(query)
                                    Catch ex As Exception
                                        query = "DELETE FROM t_repartitionparbailleur WHERE RefBesoinPartition='" & LastBesoinPartition & "'"
                                        ExecuteNonQuery(query)

                                        query = "DELETE FROM t_besoinpartition WHERE RefBesoinPartition='" & LastBesoinPartition & "'"
                                        ExecuteNonQuery(query)

                                        query = "DELETE FROM t_partition WHERE codepartition='" & CodeNewActivite & "'"
                                        ExecuteNonQuery(query)
                                        countError += 1
                                        Errors += rwHeader("TitreActivite") & vbNewLine

                                    End Try
                                End If
                            Next
                        Next

                        'On verifie le ID de l'indicateur
                        Dim IndicateurID As Decimal = -1
                        query = "select CodeIndicateur from T_Indicateur where LibelleIndicateur='" & EnleverApost(IndicateurPerformance) & "'"
                        dt = ExcecuteSelectQuery(query)
                        If dt.Rows.Count = 0 Then 'L'indicateur n'existe pas, on le créé
                            query = "insert into T_Indicateur values(NULL,'" & EnleverApost(IndicateurPerformance) & "')"
                            ExecuteNonQuery(query)
                            IndicateurID = Val(ExecuteScallar("SELECT MAX(CodeIndicateur) FROM t_indicateur"))
                        Else 'L'indicateur existe on selectionne son ID
                            IndicateurID = dt.Rows(0).Item("CodeIndicateur")
                        End If
                        'Repartition des indicateurs
                        query = "INSERT INTO t_indicateurpartition VALUES(NULL," & CodeNewActivite & "," & IndicateurID & ",'Chaque 5 de mois','OUI','" & EnleverApost("Rapport d'activité") & "')"
                        ExecuteNonQuery(query)

                        'Enregistrement du responsable et de l'editeur de la fiche
                        query = "select EMP_ID from t_grh_employe where TRIM(CONCAT(EMP_NOM,' ',EMP_PRENOMS))='" & EnleverApost(rwHeader("Responsable")) & "'"
                        Dim CodeResponsable = Val(ExecuteScallar(query))

                        query = "select EMP_ID from t_grh_employe where TRIM(CONCAT(EMP_NOM,' ',EMP_PRENOMS))='" & EnleverApost(rwHeader("Editeur")) & "'"
                        Dim CodeEditeur = Val(ExecuteScallar(query))

                        query = "INSERT INTO t_operateurpartition VALUES (NULL," & CodeNewActivite & "," & CodeResponsable & ",'Responsable','" & Now & "','" & Now & "','" & CodeUtilisateur & "')"
                        Try
                            ExecuteNonQuery(query)
                        Catch ex As Exception
                            query = "DELETE FROM t_repartitionparbailleur WHERE RefBesoinPartition IN (SELECT RefBesoinPartition FROM T_BesoinPartition WHERE CodePartition='" & CodeNewActivite & "')" '"
                            ExecuteNonQuery(query)

                            query = "DELETE FROM T_BesoinPartition WHERE CodePartition='" & CodeNewActivite & "'"
                            ExecuteNonQuery(query)

                            query = "DELETE FROM t_partition WHERE codepartition='" & CodeNewActivite & "'"
                            ExecuteNonQuery(query)
                            countError += 1
                            Errors += rwHeader("TitreActivite") & vbNewLine
                        End Try

                        query = "INSERT INTO t_operateurpartition VALUES (NULL," & CodeNewActivite & "," & CodeEditeur & ",'Elaborateur','" & Now & "','" & Now & "','" & CodeUtilisateur & "')"
                        Try
                            ExecuteNonQuery(query)
                        Catch ex As Exception
                            query = "DELETE FROM t_repartitionparbailleur WHERE RefBesoinPartition IN (SELECT RefBesoinPartition FROM T_BesoinPartition WHERE CodePartition='" & CodeNewActivite & "')" '"
                            ExecuteNonQuery(query)

                            query = "DELETE FROM T_BesoinPartition WHERE CodePartition='" & CodeNewActivite & "'"
                            ExecuteNonQuery(query)

                            query = "DELETE FROM t_partition WHERE codepartition='" & CodeNewActivite & "'"
                            ExecuteNonQuery(query)
                            countError += 1
                            Errors += rwHeader("TitreActivite") & vbNewLine
                        End Try

                    Next
                Next
                FinChargement()
                If countError = 0 Then
                    SuccesMsg("Importation terminée avec succès")
                Else
                    SuccesMsg("Les activités suivantes n'ont pas pu être importées :" & vbNewLine & Errors)
                End If

                RemplirCompo()
                RemplirActivite()
                Exit Sub

            End If

        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
            FinChargement()
        End Try
    End Sub

    Private Sub BtImp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtImp.Click
        If Not Access_Btn("BtnPrintListeActivite") Then
            Exit Sub
        End If

        Dim clause As String = ""
        'conversion de la date
        Dim str(3) As String
        str = Datedebub.Text.Split("/")
        Dim tempdt As String = String.Empty
        For j As Integer = 2 To 0 Step -1
            tempdt += str(j) & "-"
        Next
        tempdt = tempdt.Substring(0, 10)

        Dim str1(3) As String
        str1 = DateFin.Text.Split("/")
        Dim tempdt1 As String = String.Empty
        For j As Integer = 2 To 0 Step -1
            tempdt1 += str1(j) & "-"
        Next
        tempdt1 = tempdt1.Substring(0, 10)

        'Requete Date
        If DateTime.Compare(tempdt1, tempdt) >= 0 Then
            clause = " AND dateDebutPartition >='" & tempdt & "' AND dateFinPartition <='" & tempdt1 & "' order by LibelleCourt"
        Else
            SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
        End If
       query= "select count(*) from t_partition where CodeClassePartition=5" & clause
        Dim nbre = ExecuteScallar(query)
        If nbre = 0 Then
            SuccesMsg("Aucune fiche d'activité créée")
        Else
            ' Affichage état ***************************
            Dim reportActiv As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim Chemin As String = lineEtat & "\CompoActivites\"

            Dim DatSet = New DataSet
            reportActiv.Load(Chemin & "EtatActivites.rpt")

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = reportActiv.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            reportActiv.SetDataSource(DatSet)
            reportActiv.SetParameterValue("CodeProjet", ProjetEnCours)
            reportActiv.SetParameterValue("DateDebut", Datedebub.Text)
            reportActiv.SetParameterValue("DateFin", DateFin.Text)

            FullScreenReport.FullView.ReportSource = reportActiv
            FullScreenReport.ShowDialog()
        End If
    End Sub

    Private Sub Datedebub_EditValueChanged(sender As Object, e As System.EventArgs) Handles Datedebub.EditValueChanged
        If Datedebub.Text <> "" And DateFin.Text <> "" Then
            RemplirActivite()
        End If
    End Sub

    Private Sub DateFin_EditValueChanged(sender As Object, e As System.EventArgs) Handles DateFin.EditValueChanged
        If Datedebub.Text <> "" And DateFin.Text <> "" Then
            RemplirActivite()
        End If
    End Sub

    Private Sub BtActualiser_Click(sender As System.Object, e As System.EventArgs) Handles BtActualiser.Click
        If Datedebub.Text <> "" And DateFin.Text <> "" Then
            RemplirActivite()
        End If
    End Sub

    Private Sub DupliquerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DupliquerToolStripMenuItem.Click
        Dim Reponse As DialogResult = ConfirmCancelMsg("Voulez-vous dupliquer cette activité et copier ses coûts directs par la même occasion?")
        If Reponse = DialogResult.Yes Then
            GbNewActivite.Visible = True
            Modif.Checked = True
            TxtCode.Text = DrX(2).ToString
            TxtLibelle.Text = DrX(3).ToString
            TxtDescription.Text = DrX(9).ToString
            TxtJustification.Text = DrX(10).ToString
            DtDateDeb.ResetText()
            DtDateFin.ResetText()
            'LibelleActiviteDupliq = DrX(3).ToString
            Operation = "DupliquerCout"
            TxtLibelle.Enabled = False
        ElseIf Reponse = DialogResult.No Then
            GbNewActivite.Visible = True
            Modif.Checked = True
            TxtCode.Text = DrX(2).ToString
            TxtLibelle.Text = DrX(3).ToString
            TxtDescription.Text = DrX(9).ToString
            TxtJustification.Text = DrX(10).ToString
            DtDateDeb.ResetText()
            DtDateFin.ResetText()
            'LibelleActiviteDupliq = DrX(3).ToString
            Operation = "Dupliquer"
            TxtLibelle.Enabled = False
        Else
            Operation = "Normal"
            'LibelleActiviteDupliq = String.Empty
        End If
    End Sub
End Class