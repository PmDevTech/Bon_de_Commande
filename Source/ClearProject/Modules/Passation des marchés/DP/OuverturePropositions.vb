Imports MySql.Data.MySqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO

Public Class OuverturePropositions

    Dim dt1 As DataTable = New DataTable()
    Dim dt2 As DataTable = New DataTable()
    Dim HeureDemarrage As String = ""
    Dim DureeSeance As Decimal = 0
    Dim DateOuvertureEffective As String = ""

    ' Dim CodeConsult As String()

    Private Sub OuverturePropositions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RemplirCmbNumDAO()
        ChargerColones()
    End Sub

    Private Sub ChargerColones()
        ' dt1.Columns.Clear()
        dt1.Columns.Add("Proposition technique", Type.GetType("System.String"))
        dt1.Columns.Add("Proposition financière", Type.GetType("System.String"))
        dt1.Columns.Add("Observations", Type.GetType("System.String"))
        GridRecapOffre.DataSource = dt1
        GridView2.Columns.Item(0).Width = 150
        GridView2.Columns.Item(1).Width = 150
        GridView2.Columns.Item(2).Width = 400

        ' dt2.Columns.Clear()
        dt2.Columns.Add("Nom", Type.GetType("System.String"))
        dt2.Columns.Add("Téléphone", Type.GetType("System.String"))
        dt2.Columns.Add("Date et heure de pointage", Type.GetType("System.String"))
        GridCojo.DataSource = dt2
        GridView1.OptionsView.ColumnAutoWidth = True
        GridView1.OptionsBehavior.AutoExpandAllGroups = True
    End Sub

    Private Sub RemplirCmbNumDAO()
        CmbNumDAO.Properties.Items.Clear()
        CmbNumDAO.Text = ""
        'query = "select NumeroDp from T_DP where DossValider='Valider' and DateOuverture<='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' and Statut<>'Annuler' and CodeProjet='" & ProjetEnCours & "' order by NumeroDp"
        query = "select NumeroDp from T_DP where DossValider='Valider' and Statut<>'Annuler' and CodeProjet='" & ProjetEnCours & "' order by NumeroDp"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbNumDAO.Properties.Items.Add(rw("NumeroDp").ToString)
        Next
    End Sub


    Private Sub InitInfosDossier()
        LblRecapOffres.Text = "..."
        TxtDateOuverture.Text = ""
        TxtDateCloture.Text = ""
        TxtLibelleDAO.Text = ""
        TxtMethode.Text = ""
    End Sub

    Private Sub InitiliserInfoConsultant()
        ' CmbNomSoumis.Text = ""
        TxtPaysSoumis.Text = ""
        TxtAdresseSoumis.Text = ""
        TxtTelSoumis.Text = ""
        TxtFaxSoumis.Text = ""
        TxtMailSoumis.Text = ""
        TextRepre.Text = ""
    End Sub

    Private Sub InitiliserOffres()
        ChkPropoTech.Checked = False
        NumNbPropoTech.Value = 0
        ChkPropoFin.Checked = False
        NumNbPropoFin.Value = 0
        TxtObserv.Text = ""
    End Sub

    Private Sub CmbNumDAO_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumDAO.SelectedValueChanged
        InitInfosDossier()
        dt1.Rows.Clear()
        dt2.Rows.Clear()
        InitiliserInfoConsultant()
        CmbNomSoumis.Text = ""
        If (CmbNumDAO.SelectedIndex <> -1) Then
            query = "select LibelleMiss,MethodeSelection,DateLimitePropo,DateOuvertureEffective,DateOuverture,DateFinOuverture,DureeSeance,PropoTech,PropoFin, DateReporter from T_DP where NumeroDp='" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)

            For Each rw As DataRow In dt0.Rows
                TxtMethode.Text = rw("MethodeSelection").ToString
                TxtLibelleDAO.Text = MettreApost(rw("LibelleMiss").ToString)
                DateOuvertureEffective = rw("DateOuvertureEffective").ToString
                If (rw("DateLimitePropo").ToString <> "") Then
                    TxtDateCloture.Text = rw("DateLimitePropo").ToString.Replace(" ", "   à   ")
                    'Remplacer en cas des reporte
                    If rw("DateReporter").ToString <> "" Then TxtDateCloture.Text = rw("DateReporter").ToString.Replace(" ", "   à   ")
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
                    GbOffres.Enabled = False
                    'GbOffres.Enabled = True
                    BtOuvertureOffre.Text = "Etat PV" & vbNewLine & "Ouverture"
                    BtOuvertureOffre.Enabled = True
                    BtDureeSeance.Enabled = True
                    BtDureeSeance.Text = rw("DureeSeance").ToString.Replace(":", "   :   ")
                    CmbNomSoumis.Enabled = True

                    RemplirListePresence()
                Else
                    BtDureeSeance.Text = "00   :   00   :   00"
                    BtDureeSeance.Enabled = False
                    BtOuvertureOffre.Text = "Démarrer" & vbNewLine & "Ouverture"
                    BtOuvertureOffre.Enabled = False
                    GbOffres.Enabled = False
                    CmbNomSoumis.Enabled = False

                    Timer2.Interval = 1000 'Timer1_Tick sera déclenché toutes les secondes.
                    Timer2.Start() 'On démarre le Timer
                    '  BtOuvertureOffre.Text = "Dates à" & vbNewLine & "définir"
                End If
                NumNbPropoTech.Properties.MaxValue = CInt(rw("PropoTech"))
                NumNbPropoFin.Properties.MaxValue = CInt(rw("PropoFin"))
            Next

            ' Offres déposés et soumissionnaires ayant déposés ******
            CmbNomSoumis.Properties.Items.Clear()
            CmbNomSoumis.Text = ""

            query = "select RefConsult, NomConsult from T_Consultant where NumeroDp='" & EnleverApost(CmbNumDAO.Text) & "' and DateDepot <>''"
            dt0 = ExcecuteSelectQuery(query)
            Dim NbDepot As Decimal = 0

            '  ReDim CodeConsult(dt0.Rows.Count)

            For Each rw As DataRow In dt0.Rows
                '  CodeConsult(NbDepot) = rw("RefConsult")
                NbDepot += 1
                CmbNomSoumis.Properties.Items.Add(GetNewCode(rw("RefConsult")) & " | " & MettreApost(rw("NomConsult").ToString))
            Next

            Dim NbRetrait As Decimal = 0
            NbRetrait = Val(ExecuteScallar("select Count(*) from T_Consultant where NumeroDp='" & EnleverApost(CmbNumDAO.Text) & "'"))
            LblRecapOffres.Text = "Nombre de consultants sur la Liste Restreinte : " & NbRetrait.ToString & "                        Propositions reçues : " & NbDepot.ToString
        End If
    End Sub

    Private Sub RemplirListePresence()
        If (CmbNumDAO.SelectedIndex <> -1) Then
            dt2 = GridCojo.DataSource
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

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If CmbNumDAO.SelectedIndex <> -1 Then
            RemplirListePresence()
        End If
    End Sub



    Private Sub BtOuvertureOffre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtOuvertureOffre.Click
        If CmbNumDAO.SelectedIndex <> -1 Then

            Dim Deb As String = Mid(BtOuvertureOffre.Text, 1, 3)
            If (Deb = "Dém") Then
                Timer2.Stop()
                CmbNomSoumis.Enabled = True

                ' GbSoumissionnaire.Enabled = True
                ' GbOffres.Enabled = True
                HeureDemarrage = Now.ToLongTimeString
                DureeSeance = 0
                BtDureeSeance.Enabled = True
                BtOuvertureOffre.Text = "Fin de" & vbNewLine & "Séance"

                Timer1.Interval = 1000
                Timer1.Start()
                CmbNumDAO.Enabled = False

                If DateOuvertureEffective.ToString = "" Then
                    DateOuvertureEffective = dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString
                    ExecuteNonQuery("Update t_dp set DateOuvertureEffective='" & DateOuvertureEffective & "' where NumeroDp='" & EnleverApost(CmbNumDAO.Text) & "'")
                    TxtDateOuverture.Text = DateOuvertureEffective.ToString.Replace(" ", "   à   ")
                End If
            ElseIf (Deb = "Fin") Then

                'verif des soumissions de tous les fournisseurs
                Dim NbreDepot As Decimal = 0
                Dim nbSoumission As Decimal = 0
                query = "select count(*) from T_Consultant where NumeroDp='" & EnleverApost(CmbNumDAO.Text) & "' and DateDepot <>''"
                NbreDepot = Val(ExecuteScallar(query))

                query = "select count(*) from T_SoumissionConsultant where NumeroDp='" & EnleverApost(CmbNumDAO.Text) & "'"
                nbSoumission = Val(ExecuteScallar(query))

                If (NbreDepot > nbSoumission) Then
                    SuccesMsg("Il reste des propositions à ouvrir !")
                    Exit Sub
                End If

                Timer1.Stop()

                'enregistrement de la fin de séance dans la BD
                Dim FinSeance As String = Now.ToLongTimeString

                ExecuteNonQuery("Update T_DP set DateFinOuverture='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', DureeSeance ='" & BtDureeSeance.Text.Replace(" ", "") & "', Operateur ='" & CodeUtilisateur & "', DateModif ='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' where  NumeroDp='" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet='" & ProjetEnCours & "'")

                If ConfirmMsg("Imprimer le PV d'ouverture ?") = DialogResult.Yes Then
                    ImprimerPvOuverture()
                End If

                InitiliserOffres()
                InitiliserInfoConsultant()
                InitInfosDossier()
                BtDureeSeance.Text = "00   :   00   :   00"
                BtOuvertureOffre.Text = ""
                BtOuvertureOffre.Enabled = False
                CmbNomSoumis.Text = ""
                CmbNumDAO.Text = ""
                CmbNumDAO.Focus()
                dt1.Rows.Clear()
                dt2.Rows.Clear()

                BtEnrgOffre.Text = "ENREGISTRER L'OFFRE"

                CmbNomSoumis.Enabled = False
                GbOffres.Enabled = False
                CmbNumDAO.Enabled = True
            ElseIf (Deb = "Eta") Then
                ImprimerPvOuverture()
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        DureeSeance = DureeSeance + 1
        BtDureeSeance.Text = CalculTps(DureeSeance)
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

        BtEnrgOffre.Text = "ENREGISTRER L'OFFRE"

        If (CmbNomSoumis.SelectedIndex <> -1 And CmbNumDAO.SelectedIndex <> -1) Then
            Dim Deb As String = Mid(BtOuvertureOffre.Text, 1, 3)

            ' Recup des infos ***
            query = "select * from T_Consultant where NumeroDp='" & EnleverApost(CmbNumDAO.Text) & "' and ProptionDeposer <>''"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)

            For Each rw As DataRow In dt0.Rows
                TxtPaysSoumis.Text = MettreApost(rw("PaysConsult").ToString)
                TxtAdresseSoumis.Text = MettreApost(rw("AdressConsult").ToString)
                TxtTelSoumis.Text = rw("TelConsult").ToString
                TxtFaxSoumis.Text = rw("FaxConsult").ToString
                TxtMailSoumis.Text = rw("EmailConsult").ToString.ToLower
                If rw("TitreDepot").ToString <> "" Then
                    TextRepre.Text = MettreApost(rw("NomDepot").ToString & " (" & rw("TitreDepot").ToString & ")")
                Else
                    TextRepre.Text = MettreApost(rw("NomDepot").ToString)
                End If
            Next

            If Deb = "Fin" Then
                GbOffres.Enabled = True
            Else
                GbOffres.Enabled = False
            End If

            RempliGridRecapOffre()
        Else
            GbOffres.Enabled = False
            InitiliserOffres()
            InitiliserInfoConsultant()
            dt1.Rows.Clear()
        End If
    End Sub

    Private Sub RempliGridRecapOffre()

        If CmbNomSoumis.SelectedIndex <> -1 Then
            dt1 = GridRecapOffre.DataSource
            dt1.Rows.Clear()

            query = "select Observations, PropTech, PropFin from T_SoumissionConsultant where RefConsult='" & CInt(CmbNomSoumis.Text.Split("|")(0).Trim) & "' and NumeroDp='" & EnleverApost(CmbNumDAO.Text) & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)

            If dt0.Rows.Count > 0 Then
                For Each rw As DataRow In dt0.Rows
                    Dim drS = dt1.NewRow()
                    drS("Observations") = MettreApost(rw("Observations").ToString)
                    drS("Proposition technique") = Val(rw("PropTech")) & IIf(CInt(rw("PropTech")) > 1, " Copies", " Copie")
                    drS("Proposition financière") = Val(rw("PropFin")) & IIf(CInt(rw("PropFin")) > 1, " Copies", " Copie")

                    'ChkPropoTech.Checked = IIf(CInt(rw("PropTech")) > 0, True, False).ToString
                    'ChkPropoFin.Checked = IIf(CInt(rw("PropFin")) > 0, True, False).ToString
                    'NumNbPropoTech.Value = Val(rw("PropTech").ToString)
                    'NumNbPropoFin.Value = Val(rw("PropFin").ToString)
                    dt1.Rows.Add(drS)
                Next
                'Else
                '    Dim drS = dtt.NewRow()
                '    drS("Observations") = "R.A.S"
                '    dtt.Rows.Add(drS)
            End If
        End If
    End Sub

    Private Sub BtEnrgOffre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgOffre.Click
        If CmbNomSoumis.SelectedIndex <> -1 Then
            If (ChkPropoTech.Checked = False And ChkPropoFin.Checked = False) Then
                SuccesMsg("Informations incomplètes !")
                Exit Sub
            End If

            query = "select * from T_SoumissionConsultant where RefConsult ='" & CInt(CmbNomSoumis.Text.Split("|")(0).Trim) & "' and NumeroDp='" & EnleverApost(CmbNumDAO.Text) & "'"

            If (Mid(BtEnrgOffre.Text, 1, 3) = "ENR") Then

                Dim dts As DataTable = ExcecuteSelectQuery(query)
                If dts.Rows.Count > 0 Then
                    SuccesMsg("L'offre du consultant a été saisie")
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

                DatRow("RefConsult") = CInt(CmbNomSoumis.Text.Split("|")(0).Trim)
                DatRow("PropTech") = IIf(ChkPropoTech.Checked = True, NumNbPropoTech.Value, 0)
                DatRow("PropFin") = IIf(ChkPropoFin.Checked = True, NumNbPropoFin.Value, 0)
                DatRow("Observations") = EnleverApost(TxtObserv.Text)
                DatRow("DateSaisie") = Now.ToShortDateString & " " & Now.ToLongTimeString
                DatRow("DateModif") = Now.ToShortDateString & " " & Now.ToLongTimeString
                DatRow("Operateur") = CodeUtilisateur
                DatRow("NumeroDp") = EnleverApost(CmbNumDAO.Text)

                DatSet.Tables("T_SoumissionConsultant").Rows.Add(DatRow)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "T_SoumissionConsultant")
                DatSet.Clear()
            Else
                query = "update T_SoumissionConsultant set Observations='" & EnleverApost(TxtObserv.Text) & "',  PropTech='" & IIf(ChkPropoTech.Checked = True, NumNbPropoTech.Value, 0) & "', PropFin='" & IIf(ChkPropoFin.Checked = True, NumNbPropoFin.Value, 0) & "', Observations='" & EnleverApost(TxtObserv.Text) & "', DateModif='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', Operateur='" & CodeUtilisateur & "' where RefConsult ='" & CInt(CmbNomSoumis.Text.Split("|")(0).Trim) & "' and NumeroDp='" & EnleverApost(CmbNumDAO.Text) & "'"
                ExecuteNonQuery(query)
            End If

            ' Maj Propo ***********
            query = "update T_Consultant set PT='" & IIf(ChkPropoTech.Checked = True, NumNbPropoTech.Value, 0) & "', PF='" & IIf(ChkPropoFin.Checked = True, NumNbPropoFin.Value, 0) & "' where RefConsult='" & CInt(CmbNomSoumis.Text.Split("|")(0).Trim) & "'"
            ExecuteNonQuery(query)

            RempliGridRecapOffre()

            BtEnrgOffre.Text = "ENREGISTRER L'OFFRE"
            InitiliserOffres()
        Else
            SuccesMsg("Veuillez selectionné un consultant dans la liste")
            CmbNomSoumis.Select()
        End If

    End Sub

    Private Sub ChkPropoTech_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPropoTech.CheckedChanged
        If (ChkPropoTech.Checked = True) Then
            NumNbPropoTech.Enabled = True
            NumNbPropoTech.Value = 1
            NumNbPropoTech.Focus()
        Else
            NumNbPropoTech.Enabled = False
            NumNbPropoTech.Value = 0
        End If
    End Sub

    Private Sub ChkPropoFin_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPropoFin.CheckedChanged
        If (ChkPropoFin.Checked = True) Then
            NumNbPropoFin.Enabled = True
            NumNbPropoFin.Value = 1
            NumNbPropoFin.Focus()
        Else
            NumNbPropoFin.Enabled = False
            NumNbPropoFin.Value = 0
        End If
    End Sub

    Private Sub TxtObserv_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtObserv.GotFocus
        If (NumNbPropoTech.Value <= 0) Then ChkPropoTech.Checked = False
        If (NumNbPropoFin.Value <= 0) Then ChkPropoFin.Checked = False
    End Sub
    Private Sub OuverturePropositions_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub GridRecapOffre_DoubleClick(sender As Object, e As EventArgs) Handles GridRecapOffre.DoubleClick
        If GridView2.RowCount > 0 Then
            Dim Index As Integer = GridView2.FocusedRowHandle
            TxtObserv.Text = GridView2.GetDataRow(Index).Item("Observations").ToString
            Dim PropoT As String() = GridView2.GetDataRow(Index).Item("Proposition technique").ToString.Split(" ")
            Dim PropoF As String() = GridView2.GetDataRow(Index).Item("Proposition financière").ToString.Split(" ")

            ChkPropoTech.Checked = IIf(CInt(PropoT(0)) > 0, True, False).ToString
            ChkPropoFin.Checked = IIf(CInt(PropoF(0)) > 0, True, False).ToString
            NumNbPropoTech.Value = CInt(PropoT(0))
            NumNbPropoFin.Value = CInt(PropoF(0))
            BtEnrgOffre.Text = "MODIFIER L'OFFRE"
        End If
    End Sub

    Private Sub ImprimerPvOuverture()
        Try
            DebutChargement(True, "Chargement du Pv d'ouverture en cours...")
            Dim RapportPV As New ReportDocument
            RapportPV.Load(lineEtat & "\Marches\DP\PV Ouverture\" & "PvOuverture.rpt")

            Dim rwNumDp As DataRow = ExcecuteSelectQuery("select d.LibelleMiss, d.DureeSeance,d.DateReporter, d.RefMarche, d.DateLimitePropo, d.DateFinOuverture,d.DateOuverture,d.DateOuvertureEffective, m.DescriptionMarche, m.Convention_ChefFile, p.MinistereTutelle, p.NomProjet from T_DP as d, T_Marche as m, T_Projet as p where d.RefMarche=m.RefMarche and d.CodeProjet=p.CodeProjet and d.NumeroDp='" & EnleverApost(CmbNumDAO.Text) & "' and d.CodeProjet='" & ProjetEnCours & "'").Rows(0)
            RapportPV.SetParameterValue("NomProjet", MettreApost(rwNumDp("NomProjet").ToString))
            RapportPV.SetParameterValue("NumeroDp", EnleverApost(CmbNumDAO.Text))

            ' RapportPV.SetParameterValue("MinistereTutel", MettreApost(NomProjet("MinistereTutelle").ToString), "PvOuverturePageGarde.rpt")
            ' RapportPV.SetParameterValue("NomProjets", MettreApost(NomProjet("NomProjet").ToString), "PvOuverturePageGarde.rpt")

            'RapportPV.SetParameterValue("TypeConv", MettreApost(ExecuteScallar("SELECT TypeConvention from t_convention where CodeConvention='" & DtMarche("CodeConvention").ToString & "'")), "PvOuverturePageGarde.rpt")
            ' RapportPV.SetParameterValue("Bailleur", DtMarche("InitialeBailleur").ToString, "PvOuverturePageGarde.rpt")
            ' RapportPV.SetParameterValue("NumConv", DtMarche("CodeConvention").ToString, "PvOuverturePageGarde.rpt")
            'RapportPV.SetParameterValue("DateEdition", CDate(Now.ToShortDateString).ToString("MMMM").ToUpper & "  " & CDate(Now.ToShortDateString).ToString("yyyy"), "PvOuverturePageGarde.rpt")

            'RapportPV.SetParameterValue("NumDao", EnleverApost(CmbNumDAO.Text), "PvOuverturePageGarde.rpt")
            'RapportPV.SetParameterValue("DateFormatLong", CDate(rwNumDp("DateOuverture").ToString.Split(" ")(0)).ToLongDateString, "PvOuverturePageGarde.rpt")

            'Liste des cojos
            ' RapportPV.SetParameterValue("NumDpCojo", EnleverApost(CmbNumDAO.Text), "CojoPvOuvertureDP.rpt")
            'Liste cojos signataire
            ' RapportPV.SetParameterValue("NumDpCojoSigne", EnleverApost(CmbNumDAO.Text), "CojoPvSignatureDP.rpt")
            'Liste Offres depose
            'RapportPV.SetParameterValue("NumDpConsulDepot", EnleverApost(CmbNumDAO.Text), "OffresDeposesDP.rpt")

            'Pv ouverture pricipale
            RapportPV.SetParameterValue("AnneeEnLettre", MontantLettre(CDate(Now.ToShortDateString).ToString("yyyy")))
            Dim DateOuver As String() = rwNumDp("DateOuvertureEffective").ToString.Split(" ")
            Dim finSean As String() = DateOuver(1).ToString.Split(":")
            Dim DateDebutSeance = finSean(0) & " Heures " & finSean(1) & " Minutes " & finSean(2) & " Secondes"
            RapportPV.SetParameterValue("DateOuverture", CDate(DateOuver(0)).ToLongDateString & " à " & DateDebutSeance.ToString)
            RapportPV.SetParameterValue("CodeProjet", ProjetEnCours)
            RapportPV.SetParameterValue("DateFormatLong", Now.ToLongDateString)

            If rwNumDp("DateReporter").ToString <> "" Then
                RapportPV.SetParameterValue("DateDepot", CDate(rwNumDp("DateReporter").ToString).ToLongDateString)
            Else
                RapportPV.SetParameterValue("DateDepot", CDate(rwNumDp("DateLimitePropo").ToString).ToLongDateString)
            End If

            finSean = rwNumDp("DateFinOuverture").ToString.Split(" ")(1).ToString.Split(":")
            DateDebutSeance = finSean(0) & " Heures " & finSean(1) & " Minutes " & finSean(2) & " Secondes"
            RapportPV.SetParameterValue("FinSeance", DateDebutSeance)
            RapportPV.SetParameterValue("LibelleMarche", MettreApost(rwNumDp("DescriptionMarche").ToString))

            'Données du marché *********************
            'Données de l'activité (Compo Souscompo) **************
            'Dim CodActiv1 As String = ""
            'query = "Select P.LibelleCourt from T_BesoinPartition As B, T_Partition as P, t_besoinmarche as M where B.CodePartition=P.CodePartition And B.RefBesoinPartition=M.RefBesoinPartition AND B.CodeProjet='" & ProjetEnCours & "' and M.RefMarche='" & DtMarche("RefMarche").ToString & "'"
            'Dim dt0 = ExcecuteSelectQuery(query)
            'For Each rw As DataRow In dt0.Rows
            '    CodActiv1 = rw("LibelleCourt").ToString
            'Next

            'Composante   *****
            'Dim CodComp As String = Mid(CodActiv1, 1, 1)
            'RapportPV.SetParameterValue("CodeCompo", CodComp)
            'query = "select LibellePartition from T_Partition where LibelleCourt='" & CodComp & "' and CodeProjet='" & ProjetEnCours & "'"
            'dt0 = ExcecuteSelectQuery(query)
            'For Each rw As DataRow In dt0.Rows
            '    RapportPV.SetParameterValue("LibelleCompo", MettreApost(rw("LibellePartition").ToString))
            'Next

            'Sous Composante   *****
            'Dim CodSouComp As String = Mid(CodActiv1, 1, 2)
            'RapportPV.SetParameterValue("CodeSouCompo", CodSouComp)
            'query = "select LibellePartition from T_Partition where LibelleCourt='" & CodSouComp & "' and CodeProjet='" & ProjetEnCours & "'"
            'dt0 = ExcecuteSelectQuery(query)
            'For Each rw As DataRow In dt0.Rows
            '    RapportPV.SetParameterValue("LibelleSouCompo", MettreApost(rw("LibellePartition").ToString))
            'Next

            query = "select Count(*) from T_Consultant where NumeroDp='" & EnleverApost(CmbNumDAO.Text) & "'"
            Dim NbDaoRetires As Decimal = Val(ExecuteScallar(query))

            RapportPV.SetParameterValue("NbDossierRetires", NbDaoRetires.ToString)
            RapportPV.SetParameterValue("NbDossierRetiresLettre", MontantLettre(NbDaoRetires.ToString))

            query = "select Count(*) from T_Consultant where DateDepot<>'' and NumeroDp='" & EnleverApost(CmbNumDAO.Text) & "'"
            Dim NbOffresRecues As Decimal = Val(ExecuteScallar(query))
            RapportPV.SetParameterValue("NbOffresDeposes", NbOffresRecues.ToString)
            RapportPV.SetParameterValue("NbOffresDeposesLettre", MontantLettre(NbOffresRecues.ToString))

            FinChargement()

            With FullScreenReport
                .FullView.ReportSource = RapportPV
                '.FullView.ReportSource = RapportPV
                .Text = "PV D'OUVERTURE DU DOSSIER N°" & EnleverApost(CmbNumDAO.Text)
                .ShowDialog()
            End With
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub
End Class