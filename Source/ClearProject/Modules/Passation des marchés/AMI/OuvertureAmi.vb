Imports MySql.Data.MySqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared


Public Class OuvertureAmi
    Dim dt1 As DataTable = New DataTable()
    Dim dt2 As DataTable = New DataTable()
    Dim HeureDemarrage As String = ""
    Dim DureeSeance As Decimal = 0
    Dim ModifOffre As Boolean = False
    Dim DateOuvertureEffective As String = ""

    Private Sub OuvertureAmi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RemplirCmbNumAMI()

        dt1.Columns.Clear()
        dt1.Columns.Add("Observations", Type.GetType("System.String"))
        GridRecapOffre.DataSource = dt1
        GridView2.Columns("Observations").Width = 400

        dt2.Columns.Clear()
        dt2.Columns.Add("Nom", Type.GetType("System.String"))
        dt2.Columns.Add("Téléphone", Type.GetType("System.String"))
        dt2.Columns.Add("Date et heure de pointage", Type.GetType("System.String"))
        GridCojo.DataSource = dt2
        GridView1.OptionsView.ColumnAutoWidth = True
        GridView1.OptionsBehavior.AutoExpandAllGroups = True
    End Sub

    Private Sub RemplirCmbNumAMI()
        CmbNumDAO.Properties.Items.Clear()
        CmbNumDAO.ResetText()
        'dossier valider et date d'ouverture arrivé
        'query = "select NumeroDAMI from T_AMI where ValiderEditionAmi='Valider' and DateOuverture<='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' AND CodeProjet='" & ProjetEnCours & "' order by NumeroDAMI"
        query = "select NumeroDAMI from T_AMI where ValiderEditionAmi='Valider' and DatePub<='" & dateconvert(Now.ToShortDateString) & "' AND CodeProjet='" & ProjetEnCours & "' order by NumeroDAMI"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbNumDAO.Properties.Items.Add(MettreApost(rw("NumeroDAMI").ToString))
        Next
    End Sub

    Private Sub CmbNumDAO_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumDAO.SelectedValueChanged
        InitInfosSoumis()
        dt1.Rows.Clear()
        TxtMethode.Text = ""
        TxtLibelleDAO.Text = ""
        TxtDateCloture.Text = ""
        TxtDateOuverture.Text = ""
        TxtObserv.Text = ""

        If (CmbNumDAO.SelectedIndex <> -1) Then
            query = "select LibelleMiss, MethodeSelection, DateLimitePropo, DateOuvertureEffective, DateOuverture, DateFinOuverture, DureeSeance, PropoTech, PropoFin, DateReporte from T_AMI where NumeroDAMI='" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)

            For Each rw As DataRow In dt0.Rows
                TxtMethode.Text = rw("MethodeSelection").ToString
                TxtLibelleDAO.Text = MettreApost(rw("LibelleMiss").ToString)
                DateOuvertureEffective = rw("DateOuvertureEffective").ToString

                If (rw("DateLimitePropo").ToString <> "") Then
                    TxtDateCloture.Text = rw("DateLimitePropo").ToString.Replace(" ", "   à   ")
                    If rw("DateReporte").ToString <> "" Then TxtDateCloture.Text = rw("DateReporte").ToString.Replace(" ", "   à   ")
                Else
                    TxtDateCloture.Text = "Non définie"
                End If

                If (rw("DateOuverture").ToString <> "") Then
                    TxtDateOuverture.Text = rw("DateOuverture").ToString.Replace(" ", "   à   ")
                    If rw("DateOuvertureEffective").ToString <> "" Then TxtDateOuverture.Text = rw("DateOuvertureEffective").ToString.Replace(" ", "   à   ")
                Else
                    TxtDateOuverture.Text = "Non définie"
                End If

                If (rw("DateFinOuverture").ToString <> "") Then
                    BtEnrgOffre.Enabled = False
                    CmbNomSoumis.Enabled = True
                    GbOffres.Enabled = True
                    BtOuvertureOffre.Text = "Etat PV" & vbNewLine & "Ouverture"
                    BtOuvertureOffre.Enabled = True
                    BtDureeSeance.Enabled = True
                    BtDureeSeance.Text = rw("DureeSeance").ToString.Replace(":", "   :   ")
                    RemplirListePresence()
                Else
                    BtEnrgOffre.Enabled = True
                    BtDureeSeance.Text = "00   :   00   :   00"
                    BtDureeSeance.Enabled = False
                    BtOuvertureOffre.Text = "Démarrer" & vbNewLine & "Ouverture"
                    BtOuvertureOffre.Enabled = False
                    CmbNomSoumis.Enabled = False
                    GbOffres.Enabled = False

                    If (rw("DateLimitePropo").ToString <> "" And rw("DateOuverture").ToString <> "") Then
                        Timer2.Interval = 1000 'Timer1_Tick sera déclenché toutes les secondes.
                        Timer2.Start() 'On démarre le Timer
                    Else
                        BtOuvertureOffre.Text = "Dates à" & vbNewLine & "définir"
                    End If
                End If
            Next

            'listent des consutants
            Dim NbDepot As Integer = 0
            CmbNomSoumis.Properties.Items.Clear()
            query = "select RefConsult, NomConsult from T_Consultant where NumeroDp='" & EnleverApost(CmbNumDAO.Text) & "'"
            dt0 = ExcecuteSelectQuery(query)

            For Each rw As DataRow In dt0.Rows
                NbDepot += 1
                CmbNomSoumis.Properties.Items.Add(GetNewCode(rw("RefConsult")) & " | " & MettreApost(rw("NomConsult").ToString))
            Next

            LblRecapOffres.Text = "Nombre de consultants : " & NbDepot.ToString
        End If
    End Sub

    Private Sub RemplirListePresence()
        If (CmbNumDAO.Text <> "") Then
            dt2.Rows.Clear()

            'Teste de verification du nombre de pointage
            Dim NbreCOJOPointe As Boolean = False

            query = "select * from T_Commission where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                Dim drS = dt2.NewRow()

                drS("Nom") = MettreApost(rw("NomMem").ToString) & " (" & rw("TitreMem").ToString & ")"
                drS("Téléphone") = MettreApost(rw("TelMem").ToString)

                If rw("Pointage").ToString = "" Then
                    drS("Date et heure de pointage") = "En attente"
                    NbreCOJOPointe = True
                Else
                    drS("Date et heure de pointage") = MettreApost(rw("Pointage").ToString)
                End If

                dt2.Rows.Add(drS)
            Next

            If (GridView1.RowCount > 0) Then
                If NbreCOJOPointe = True Then
                    BtOuvertureOffre.Enabled = False
                Else
                    BtOuvertureOffre.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub BtOuvertureOffre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtOuvertureOffre.Click
        If CmbNumDAO.SelectedIndex <> -1 Then

            Dim Deb As String = Mid(BtOuvertureOffre.Text, 1, 3)

            If (Deb = "Dém") Then
                Timer2.Stop()

                CmbNomSoumis.Enabled = True
                GbOffres.Enabled = True
                HeureDemarrage = Now.ToLongTimeString
                DureeSeance = 0
                BtDureeSeance.Enabled = True
                BtOuvertureOffre.Text = "Fin de" & vbNewLine & "Séance"

                Timer1.Interval = 1000
                Timer1.Start()
                CmbNumDAO.Enabled = False

                'Date effective d'ouverture

                If DateOuvertureEffective.ToString = "" Then
                    DateOuvertureEffective = dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString
                    ExecuteNonQuery("Update t_ami set DateOuvertureEffective='" & DateOuvertureEffective & "' where NumeroDAMI='" & EnleverApost(CmbNumDAO.Text) & "'")
                    TxtDateOuverture.Text = DateOuvertureEffective.ToString.Replace(" ", "   à   ")
                End If
            ElseIf Deb = "Eta" Then
                ImprimerPvOuverture()
            Else

                'verif des soumissions de tous les fournisseurs
                Dim NbrConlt As Decimal = 0
                Dim nbSoumission As Decimal = 0

                query = "select count(*) from T_Consultant where NumeroDp='" & EnleverApost(CmbNumDAO.Text) & "'"
                NbrConlt = Val(ExecuteScallar(query))

                query = "select  count(*) from T_SoumissionConsultant where NumeroDp='" & EnleverApost(CmbNumDAO.Text) & "'"
                nbSoumission = Val(ExecuteScallar(query))

                If (NbrConlt > nbSoumission) Then
                    SuccesMsg("Il reste des MI à ouvrir !")
                    Exit Sub
                End If

                Timer1.Stop()

                'enregistrement de la fin de séance dans la BD
                Dim FinSeance As String = Now.ToLongTimeString

                query = "Update T_AMI set DateFinOuverture='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', DureeSeance='" & BtDureeSeance.Text.Replace(" ", "") & "', Operateur='" & CodeUtilisateur & "', DateModif='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' where NumeroDAMI='" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

                If ConfirmMsg("Imprimer le PV d'ouverture ?") = DialogResult.Yes Then
                    ImprimerPvOuverture()
                End If

                InitInfosDossier()
                BtDureeSeance.Text = "00   :   00   :   00"
                BtOuvertureOffre.Text = ""
                BtOuvertureOffre.Enabled = False
                'dt.Columns.Clear()
                'dt.Rows.Clear()
                InitInfosSoumis()
                InitSaisieInfos()
                dt1.Rows.Clear()
                dt2.Rows.Clear()
                GbSaisieInfos.Enabled = False
                GbOffres.Enabled = False
                CmbNumDAO.Enabled = True
                CmbNumDAO.Focus()
            End If
        End If
    End Sub

    Private Sub ImprimerPvOuverture()

        Try
            DebutChargement(True, "Chargement du pv d'ouverture en cours...")

            Dim Chemin As String = lineEtat & "\Marches\AMI\"

            Dim RapportPVouverture As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables

            Dim DatSet = New DataSet

            RapportPVouverture.Load(Chemin & "AMI_PV_Ouverture.rpt")

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = RapportPVouverture.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            RapportPVouverture.SetDataSource(DatSet)

            RapportPVouverture.SetParameterValue("CodeProjet", ProjetEnCours)
            RapportPVouverture.SetParameterValue("NumDp", EnleverApost(CmbNumDAO.Text))

            'query = "select MoyenPublication, DatePub from t_ami where NumeroDAMI='" & EnleverApost(CmbNumDAO.Text) & "' AND  CodeProjet='" & ProjetEnCours & "'"
            'Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            'For Each rw As DataRow In dt0.Rows
            '    RapportPVouverture.SetParameterValue("LibellePublication", MettreApost(rw("MoyenPublication").ToString))
            '    RapportPVouverture.SetParameterValue("DatePublication", rw("DatePub").ToString)
            'Next

            With FullScreenReport
                .FullView.ReportSource = RapportPVouverture
                '.FullView.ReportSource = reportPV
                .Text = "PV D'OUVERTURE DU DOSSIER N°" & EnleverApost(CmbNumDAO.Text)
                .ShowDialog()
            End With

            FinChargement()
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub InitInfosDossier()
        LblRecapOffres.Text = "..."
        TxtDateOuverture.Text = ""
        TxtDateCloture.Text = ""
        TxtLibelleDAO.Text = ""
        TxtMethode.Text = ""
        CmbNumDAO.Text = ""
    End Sub

    Private Sub InitInfosSoumis()
        CmbNomSoumis.Text = ""
        TxtPaysSoumis.Text = ""
        TxtAdresseSoumis.Text = ""
        TxtTelSoumis.Text = ""
        TxtFaxSoumis.Text = ""
        TxtMailSoumis.Text = ""
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        DureeSeance = DureeSeance + 1
        BtDureeSeance.Text = CalculTps(DureeSeance)
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If CmbNumDAO.SelectedIndex <> -1 Then
            RemplirListePresence()
        End If
    End Sub

    Private Function CalculTps(ByVal Tps As Decimal) As String
        Dim Hre As String = "0"
        Dim Min As String = "0"
        Dim Sec As String = "0"

        If (Tps <> 0) Then
            Hre = (Tps \ 3600).ToString
            Min = ((Tps - (Hre * 3600)) \ 60).ToString
            Sec = (Tps - (Hre * 3600) - (Min * 60)).ToString
        End If

        If (Len(Hre) < 2) Then Hre = "0" & Hre
        If (Len(Min) < 2) Then Min = "0" & Min
        If (Len(Sec) < 2) Then Sec = "0" & Sec

        Return Hre & "   :   " & Min & "   :   " & Sec
    End Function

    Private Sub CmbNomSoumis_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbNomSoumis.SelectedIndexChanged
        If (CmbNomSoumis.SelectedIndex <> -1 And CmbNumDAO.SelectedIndex <> -1) Then
            ' Recup des infos ***
            query = "select PaysConsult,AdressConsult,TelConsult,FaxConsult,EmailConsult,RefConsult from T_Consultant where RefConsult='" & CInt(Trim(CmbNomSoumis.Text.Split("|")(0))) & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtPaysSoumis.Text = MettreApost(rw("PaysConsult").ToString)
                TxtAdresseSoumis.Text = MettreApost(rw("AdressConsult").ToString)
                TxtTelSoumis.Text = MettreApost(rw("TelConsult").ToString)
                TxtFaxSoumis.Text = MettreApost(rw("FaxConsult").ToString)
                TxtMailSoumis.Text = MettreApost(rw("EmailConsult").ToString.ToLower)
                GbSaisieInfos.Enabled = True
            Next
            MajGridRecap()
            InitSaisieInfos()
        End If

    End Sub

    Private Sub BtEnrgOffre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgOffre.Click
        If CmbNumDAO.SelectedIndex = -1 Then
            SuccesMsg("Veuillez selectionner un dossier")
            CmbNumDAO.Focus()
            Exit Sub
        End If

        If CmbNomSoumis.SelectedIndex = -1 Then
            SuccesMsg("Veuillez selectionner un consultant")
            CmbNomSoumis.Focus()
            Exit Sub
        End If

        If TxtObserv.IsRequiredControl("Veuillez saisir votre observation") Then
            TxtObserv.Focus()
            Exit Sub
        End If

        If ModifOffre = False Then
            Dim Observation As String = ""
            query = "select Observations from T_SoumissionConsultant where RefConsult='" & CInt(Trim(CmbNomSoumis.Text.Split("|")(0))) & "'"
            Observation = ExecuteScallar(query)

            If Observation.ToString <> "" Then
                SuccesMsg("Le dossier du consultant en cours à été saisie")
                TxtObserv.Focus()
                Exit Sub
            End If

            ' Enregistrement du fournisseur
            Dim DatSet = New DataSet
            query = "select * from T_SoumissionConsultant"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_SoumissionConsultant")
            Dim DatTable = DatSet.Tables("T_SoumissionConsultant")
            Dim DatRow = DatSet.Tables("T_SoumissionConsultant").NewRow()

            DatRow("RefConsult") = CInt(Trim(CmbNomSoumis.Text.Split("|")(0)))
            DatRow("PropTech") = "0"
            DatRow("PropFin") = "0"
            DatRow("Observations") = EnleverApost(TxtObserv.Text)
            DatRow("DateSaisie") = Now.ToShortDateString & " " & Now.ToLongTimeString
            DatRow("DateModif") = Now.ToShortDateString & " " & Now.ToLongTimeString
            DatRow("Operateur") = CodeUtilisateur
            DatRow("NumeroDp") = EnleverApost(CmbNumDAO.Text)

            DatSet.Tables("T_SoumissionConsultant").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_SoumissionConsultant")
            DatSet.Clear()
            BDQUIT(sqlconn)
        Else
            query = "update T_SoumissionConsultant set Observations = '" & EnleverApost(TxtObserv.Text) & "', DateModif='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', Operateur = '" & CodeUtilisateur & "' where RefConsult = '" & CInt(Trim(CmbNomSoumis.Text.Split("|")(0))) & "'"
            ExecuteNonQuery(query)
        End If

        ' Maj Propo ***********
        MajGridRecap()
        InitSaisieInfos()
        ModifOffre = False
    End Sub

    Private Sub MajGridRecap()
        If (CmbNumDAO.Text <> "") Then
            dt1.Rows.Clear()

            query = "select Observations from T_SoumissionConsultant where RefConsult='" & CInt(Trim(CmbNomSoumis.Text.Split("|")(0))) & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)

            If dt0.Rows.Count > 0 Then
                For Each rw As DataRow In dt0.Rows
                    Dim drS = dt1.NewRow()
                    drS("Observations") = MettreApost(rw("Observations").ToString)
                    dt1.Rows.Add(drS)
                Next
                'Else
                '    Dim drS = dtt.NewRow()
                '    drS("Observations") = "R.A.S"
                '    dtt.Rows.Add(drS)
            End If

            'MsgBox(TxtCodeSoumis.Text, MsgBoxStyle.Information)
            'dt2.Columns.Add("Proposition technique", Type.GetType("System.String"))
            'dt2.Columns.Add("Proposition financière", Type.GetType("System.String"))
            ''Dim Reader As MySqlDataReader
            'query = "select PT,PF from T_Consultant where RefConsult='" & TxtCodeSoumis.Text & "'"
            '
            'Dim Reader=ExecuteQuerry(query)()
            'dt2.Rows.Clear()
            'While Reader.Read()
            '    drS(0) = Reader.GetValue(0).ToString
            '    drS(1) = Reader.GetValue(1).ToString
            'End While
            'Reader.Close()

            'drS(0) = drS(0).ToString & IIf(CInt(drS(0)) > 1, " Copies", " Copie")
            'drS(1) = drS(1).ToString & IIf(CInt(drS(1)) > 1, " Copies", " Copie")
            'If (drS("Observations").ToString = "") Then drS("Observations") = "R.A.S"
            'dt2.Rows.Add(drS)
            'GridView2.Columns.Item(0).Width = 150
            'GridView2.Columns.Item(1).Width = 150
        End If
    End Sub

    Private Sub InitSaisieInfos()
        TxtObserv.Text = ""
    End Sub

    Private Sub OuverturePropositions_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub GridRecapOffre_Click(sender As Object, e As EventArgs) Handles GridRecapOffre.Click
        If CmbNomSoumis.SelectedIndex <> -1 Then
            Dim DrX As DataRow = GridView2.GetDataRow(GridView2.FocusedRowHandle)
            TxtObserv.Text = DrX("Observations").ToString
            ModifOffre = True
        End If
    End Sub
End Class