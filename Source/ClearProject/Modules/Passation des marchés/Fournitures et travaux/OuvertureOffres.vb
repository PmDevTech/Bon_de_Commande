Imports MySql.Data.MySqlClient
Imports System.IO
Imports ClearProject.PassationMarche
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraTreeList.Nodes

Public Class OuvertureOffres
    Dim dt = New DataTable()
    Dim dt2 = New DataTable()
    Dim HeureDemarrage As String = ""
    Dim DureeSeance As String = ""
    Dim OuvertureTermine As Boolean = False
    Dim leCode As String
    Dim AttestationCNPS As String = ""
    Dim AttestationReguFiscal As String = ""


    Private Sub OuvertureOffres_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        BtModOffre.Enabled = False
        RemplirCmbNumDAO()
        ItemDevise()
        MajCmbCompte()
    End Sub

    Private Sub RemplirCmbNumDAO()
        CmbNumDAO.Text = ""
        CmbNumDAO.Properties.Items.Clear()
        query = "select NumeroDAO from T_DAO where DossValider=true and statut_DAO<>'Annulé' and CodeProjet='" & ProjetEnCours & "' ORDER BY DateEdition DESC"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbNumDAO.Properties.Items.Add(EnleverApost(rw("NumeroDAO").ToString))
        Next
    End Sub

    Private Sub CmbNumDAO_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumDAO.SelectedValueChanged

        TxtMethode.Text = ""
        TxtTypeMarche.Text = ""
        TxtLibelleDAO.Text = ""
        TxtDateCloture.Text = ""
        TxtDateOuverture.Text = ""
        AttestationCNPS = ""
        AttestationReguFiscal = ""
        TxtNbLot.Text = ""
        InitInfosSoumis()
        InitSaisieInfos()
        ViderGridRecap()
        ListeRecap.Nodes.Clear()
        GbOffres.Enabled = False

        If (CmbNumDAO.SelectedIndex <> -1) Then
            Dim DateOuv As String = "01/01/2999"
            query = "select * from T_DAO where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtMethode.Text = rw("MethodePDM").ToString
                TxtTypeMarche.Text = rw("TypeMarche").ToString
                TxtLibelleDAO.Text = MettreApost(rw("IntituleDAO").ToString)
                AttestationCNPS = rw("AttestationCNPS").ToString
                AttestationReguFiscal = rw("AttestationReguFiscal").ToString

                If (rw("DateLimiteRemise").ToString <> "") Then
                    TxtDateCloture.Text = IIf(rw("DateReport").ToString <> "", rw("DateReport").ToString.Replace(" ", "   à   "), rw("DateLimiteRemise").ToString.Replace(" ", "   à   ")).ToString

                    'DateOuv = rw("DateOuverture").ToString
                    'L'ouverture commence à la date de fin de dépôt des offres
                    DateOuv = IIf(rw("DateReport").ToString <> "", rw("DateReport").ToString, rw("DateLimiteRemise").ToString).ToString
                Else
                    TxtDateCloture.Text = "Non définie"
                End If

                If (rw("DateOuverture").ToString <> "") Then
                    TxtDateOuverture.Text = IIf(rw("DateDebutOuverture").ToString <> "", rw("DateDebutOuverture").ToString.Replace(" ", "   à   "), rw("DateOuverture").ToString.Replace(" ", "   à   ")).ToString
                Else
                    TxtDateOuverture.Text = "Non définie"
                End If
                TxtNbLot.Text = rw("NbreLotDAO").ToString

                CmbNumLot.Text = ""
                CmbNumLot.Properties.Items.Clear()
                For k As Integer = 1 To CInt(rw("NbreLotDAO"))
                    CmbNumLot.Properties.Items.Add(k.ToString)
                Next

                If (rw("DateFinOuverture").ToString <> "") Then
                    'TxtCodePresence.Enabled = False
                    GbSoumissionnaire.Enabled = True
                    GbOffres.Enabled = True
                    BtOuvertureOffre.Text = "Etat PV" & vbNewLine & "d'ouverture"
                    BtOuvertureOffre.Enabled = True
                    BtDureeSeance.Enabled = True
                    OuvertureTermine = True
                    BtDureeSeance.Text = rw("DureeSeance").ToString.Replace(":", "   :   ")
                    'RemplirCojo()
                    ' RemplirListePresence(CmbNumDAO.Text)
                Else
                    BtDureeSeance.Text = "00   :   00   :   00"
                    BtDureeSeance.Enabled = False
                    BtOuvertureOffre.Text = "Démarrer" & vbNewLine & "Ouverture"
                    BtOuvertureOffre.Enabled = False
                    OuvertureTermine = False
                    GbSoumissionnaire.Enabled = False
                    If (rw("DateLimiteRemise").ToString <> "" And rw("DateOuverture").ToString <> "") Then
                        'TxtCodePresence.Enabled = True
                        'TxtCodePresence.Focus()
                        Timer2.Interval = 1000 'Timer1_Tick sera déclenché toutes les secondes.
                        Timer2.Start() 'On démarre le Timer
                    Else
                        'TxtCodePresence.Enabled = False
                        BtOuvertureOffre.Text = "Dates à" & vbNewLine & "définir"
                    End If

                End If
            Next

            ' Offres déposés et soumissionnaires ayant déposés ******
            Dim NbDepot As Decimal = 0
            CmbNomSoumis.Text = ""
            CmbNomSoumis.Properties.Items.Clear()
            query = "select CodeFournis, NomFournis from T_Fournisseur where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet='" & ProjetEnCours & "' and DateDepotDAO<>''"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                NbDepot = NbDepot + 1
                CmbNomSoumis.Properties.Items.Add(GetNewCode(rw("CodeFournis")) & " | " & MettreApost(rw("NomFournis").ToString))
            Next

            Dim NbRetrait As Decimal = 0
            NbRetrait = Val(ExecuteScallar("select Count(*) from T_Fournisseur where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet='" & ProjetEnCours & "'"))

            LblRecapOffres.Text = "Dossiers retirés : " & NbRetrait.ToString & "                              Offres reçues : " & NbDepot.ToString

            If (TxtNbLot.Text = "0" Or DateTime.Compare(CDate(DateOuv), Now) > 0) Then
                GbSoumissionnaire.Enabled = False
                'TxtCodePresence.Enabled = False
                BtOuvertureOffre.Enabled = False
            End If
            If OuvertureTermine = True Then
                GbSaisieInfos.Enabled = False
            End If
            'RemplirCojo()
            RemplirListePresence(CmbNumDAO.Text)
        End If
    End Sub

#Region "Code non utiliser"

    Private Sub RemplirCojo()
        If (CmbNumDAO.Text <> "") Then

            dt.Columns.Clear()
            dt.Columns.Add("Commission", Type.GetType("System.String"))

            'Dim Reader As MySqlDataReader
            dt.Rows.Clear()
            query = "select NomMem,TitreMem from T_Commission where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and Pointage<>''"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                Dim drS = dt.NewRow()

                drS("Commission") = MettreApost(rw("NomMem").ToString) & " (" & rw("TitreMem").ToString & ")"

                dt.Rows.Add(drS)
            Next

            GridCojo.DataSource = dt

            If (GridView1.RowCount > 0) Then
                BtOuvertureOffre.Enabled = True
            Else
                BtOuvertureOffre.Enabled = False
            End If
        End If
    End Sub
#End Region

    Private Sub BtOuvertureOffre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtOuvertureOffre.Click
        Try
            If CmbNumDAO.SelectedIndex <> -1 Then

                Dim Deb As String = Mid(BtOuvertureOffre.Text, 1, 3)
                If (Deb = "Dém") Then
                    Timer2.Stop()
                    GbSoumissionnaire.Enabled = True
                    GbOffres.Enabled = True
                    HeureDemarrage = Now.ToLongTimeString
                    DureeSeance = 0
                    BtDureeSeance.Enabled = True
                    BtOuvertureOffre.Text = "Fin de" & vbNewLine & "Séance"
                    TxtDateOuverture.Text = dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString
                    ' ExecuteNonQuery("Update T_DAO set DateDebutOuverture='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet='" & ProjetEnCours & "'")
                    ExecuteNonQuery("Update T_DAO set DateDebutOuverture='" & TxtDateOuverture.Text & "' where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet='" & ProjetEnCours & "'")

                    Timer1.Interval = 1000
                    Timer1.Start()
                    CmbNumDAO.Enabled = False

                ElseIf (Deb = "Fin" Or Deb = "Eta") Then

                    'verif des soumissions de tous les fournisseurs
                    Dim LesFournis(500) As String
                    Dim nbFournis As Decimal = 0
                    query = "select CodeFournis from T_Fournisseur where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet='" & ProjetEnCours & "' and DateDepotDAO<>''"
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        LesFournis(nbFournis) = rw("CodeFournis").ToString
                        nbFournis = nbFournis + 1
                    Next

                    Dim nbSoumission As Decimal = 0
                    For k As Integer = 0 To nbFournis - 1
                        query = "select * from T_SoumissionFournisseur where CodeFournis='" & LesFournis(k) & "'"
                        dt0 = ExcecuteSelectQuery(query)
                        For Each rw As DataRow In dt0.Rows
                            nbSoumission = nbSoumission + 1
                            Exit For
                        Next
                    Next

                    If (nbFournis > nbSoumission And Deb <> "Eta") Then
                        SuccesMsg("Il reste des offres à ouvrir!")
                        Exit Sub
                    End If

                    If (Deb <> "Eta") Then
                        Timer1.Stop()
                        InitInfosSoumis()
                        InitSaisieInfos()
                        dt2.Columns.Clear()
                        dt2.Rows.Clear()
                        'GbSaisieInfos.Enabled = True
                        GbOffres.Enabled = False
                        CmbNumDAO.Enabled = True

                        'enregistrement de la fin de séance dans la BD
                        ExecuteNonQuery("Update T_DAO set DateFinOuverture='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', DureeSeance='" & BtDureeSeance.Text.Replace(" ", "") & "', DateModif='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', Operateur='" & CodeOperateurEnCours & "' where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet='" & ProjetEnCours & "'")
                    End If

                    DebutChargement(True, "Chargement du PV d'ouverture des offres...")
                    Dim reportPVO As New ReportDocument
                    Dim crtableLogoninfos As New TableLogOnInfos
                    Dim crtableLogoninfo As New TableLogOnInfo
                    Dim crConnectionInfo As New ConnectionInfo
                    Dim CrTables As Tables
                    Dim CrTable As Table
                    Dim Chemin As String = lineEtat & "\Marches\DAO\Fournitures\"
                    Dim DatSet = New DataSet
                    reportPVO.Load(Chemin & "PVO.rpt")

                    With crConnectionInfo
                        .ServerName = ODBCNAME
                        .DatabaseName = DB
                        .UserID = USERNAME
                        .Password = PWD
                    End With

                    CrTables = reportPVO.Database.Tables
                    For Each CrTable In CrTables
                        crtableLogoninfo = CrTable.LogOnInfo
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo
                        CrTable.ApplyLogOnInfo(crtableLogoninfo)
                    Next

                    'reportPVO.SetParameterValue("NumDAO", CmbNumDAO.Text, "CojoPvOuvertureDAO.rpt")
                    'reportPVO.SetParameterValue("NumDAO2", CmbNumDAO.Text, "DossiersRetiresDAO.rpt")
                    'reportPVO.SetParameterValue("NumDAO3", CmbNumDAO.Text, "OffresDeposesDAO.rpt")
                    'reportPVO.SetParameterValue("NumDAO4", CmbNumDAO.Text, "OffresLuesDAO.rpt")
                    'reportPVO.SetParameterValue("NumDAO5", CmbNumDAO.Text, "CojoPvSignatureDAO.rpt")
                    'reportPVO.SetParameterValue("NumDao", CmbNumDAO.Text, "PvOuverturePageGarde.rpt")

                    reportPVO.SetParameterValue("NumDao", EnleverApost(CmbNumDAO.Text))
                    reportPVO.SetParameterValue("CodeProjet", ProjetEnCours)
                    'reportPVO.SetParameterValue("CodeProjet", ProjetEnCours, "PvOuverturePageGarde.rpt")
                    reportPVO.SetParameterValue("LibelleMarche", TxtLibelleDAO.Text)

                    query = "select MinistereTutelle,NomProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        reportPVO.SetParameterValue("Ministere", MettreApost(rw("MinistereTutelle").ToString), "PvOuverturePageGarde.rpt")
                        reportPVO.SetParameterValue("NomProjet", MettreApost(rw("NomProjet").ToString), "PvOuverturePageGarde.rpt")
                        reportPVO.SetParameterValue("NomProjet", MettreApost(rw("NomProjet").ToString))
                    Next

                    Dim RefMarche As String = ""
                    query = "select MethodePDM,RefMarche, NbreLotDAO,DateLimiteRemise, DateDebutOuverture, DateFinOuverture, DateReport from T_DAO where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        RefMarche = rw("RefMarche")

                        reportPVO.SetParameterValue("AnneeEnLettre", MontantLettre(CDate(rw("DateDebutOuverture")).ToString("yyyy")))
                        reportPVO.SetParameterValue("DateEnLettre", CDate(rw("DateDebutOuverture")).ToLongDateString)
                        reportPVO.SetParameterValue("Annee", CDate(rw("DateDebutOuverture")).ToString("yyyy"))
                        reportPVO.SetParameterValue("DebutSeance", Mid(CDate(rw("DateDebutOuverture")).ToLongTimeString, 1, 5).Replace(":", " heures ") & " mn")

                        reportPVO.SetParameterValue("FinSeance", Mid(CDate(rw("DateFinOuverture")).ToLongTimeString, 1, 5).Replace(":", " heures ") & " mn")
                        reportPVO.SetParameterValue("DateEdition", CDate(rw("DateFinOuverture")).ToString("MMMM").ToUpper & "  " & CDate(rw("DateFinOuverture")).ToString("yyyy"), "PvOuverturePageGarde.rpt")
                        reportPVO.SetParameterValue("DateFormatLong", CDate(rw("DateFinOuverture")).ToLongDateString)
                        reportPVO.SetParameterValue("DateFormatLong", CDate(rw("DateFinOuverture")).ToLongDateString, "PvOuverturePageGarde.rpt")

                        If (rw("MethodePDM").ToString <> "") Then
                            reportPVO.SetParameterValue("MethodePdm", MettreApost(rw("MethodePDM").ToString), "PvOuverturePageGarde.rpt")
                        Else
                            reportPVO.SetParameterValue("MethodePdm", "AO", "PvOuverturePageGarde.rpt")
                        End If

                        reportPVO.SetParameterValue("NbLots", rw("NbreLotDAO").ToString & IIf(CInt(rw("NbreLotDAO")) > 1, "LOTS", "LOT"), "PvOuverturePageGarde.rpt")

                        Dim DatCoup As String = ""
                        If rw("DateReport").ToString <> "" Then
                            DatCoup = rw("DateReport").ToString.Split(" "c)(0)
                        Else
                            DatCoup = rw("DateLimiteRemise").ToString.Split(" "c)(0)
                        End If

                        reportPVO.SetParameterValue("DateDepot", CDate(DatCoup).ToLongDateString)
                    Next

                    'Données du marché *********************
                    Dim CodeMarche As Decimal = 0
                    Dim LeBaill As String = ""
                    Dim LeConvention As String = ""
                    Dim ConventionChefFil As String = ""
                    Dim LibMarc As String = ""
                    query = "select RefMarche,DescriptionMarche,InitialeBailleur, CodeConvention,Convention_ChefFile from T_Marche where RefMarche='" & RefMarche & "' and CodeProjet='" & ProjetEnCours & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        CodeMarche = rw("RefMarche")
                        LeBaill = rw("InitialeBailleur")
                        ConventionChefFil = rw("Convention_ChefFile").ToString
                        LeConvention = rw("CodeConvention").ToString

                        If (LibMarc <> "") Then
                            LibMarc = LibMarc & vbNewLine & " et " & vbNewLine
                        End If
                        LibMarc = LibMarc & rw("DescriptionMarche").ToString
                    Next

                    reportPVO.SetParameterValue("LibelleMarche", MettreApost(LibMarc))
                    reportPVO.SetParameterValue("LibelleMarche", MettreApost(LibMarc), "PvOuverturePageGarde.rpt")

                    ' La convention ****************************
                    'query = "select C.CodeConvention,C.TypeConvention from T_Convention as C, T_Bailleur as B where B.CodeBailleur=C.CodeBailleur and B.InitialeBailleur='" & LeBaill & "' and B.CodeProjet='" & ProjetEnCours & "'"
                    Dim TypeConventionBailleuChefFil As String = ExecuteScallar("select TypeConvention from T_Convention  where CodeConvention='" & ConventionChefFil & "'")
                    'For Each rw As DataRow In dt0.Rows
                    reportPVO.SetParameterValue("TypeConv", TypeConventionBailleuChefFil.ToString.ToUpper, "PvOuverturePageGarde.rpt")
                    reportPVO.SetParameterValue("NumConv", LeConvention.ToString, "PvOuverturePageGarde.rpt")
                    reportPVO.SetParameterValue("Bailleur", LeBaill, "PvOuverturePageGarde.rpt")
                    'Next

                    ''Données de l'activité (Compo Souscompo) **************
                    'Dim CodActiv1 As String = ""
                    'query = "select P.LibelleCourt from T_BesoinPartition as B, T_BesoinMarche as BM,T_Partition as P where B.CodePartition=P.CodePartition and BM.RefBesoinPartition=B.RefBesoinPartition and B.CodeProjet='" & ProjetEnCours & "' and BM.RefMarche='" & CodeMarche & "'"
                    'dt0 = ExcecuteSelectQuery(query)
                    'For Each rw As DataRow In dt0.Rows
                    '    CodActiv1 = rw(0).ToString
                    'Next
                    ''       Composante   *****
                    'Dim CodComp As String = Mid(CodActiv1, 1, 1)
                    'reportPVO.SetParameterValue("CodeCompo", CodComp, "PvOuverturePageGarde.rpt")
                    'reportPVO.SetParameterValue("CodeCompo", CodComp)
                    'query = "select LibellePartition from T_Partition where LibelleCourt='" & CodComp & "' and CodeProjet='" & ProjetEnCours & "'"
                    'dt0 = ExcecuteSelectQuery(query)
                    'For Each rw As DataRow In dt0.Rows
                    '    reportPVO.SetParameterValue("LibCompo", MettreApost(rw(0).ToString).ToUpper, "PvOuverturePageGarde.rpt")
                    '    reportPVO.SetParameterValue("LibelleCompo", MettreApost(rw(0).ToString))
                    'Next

                    ''       Sous Composante   *****
                    'Dim CodSouComp As String = Mid(CodActiv1, 1, 2)
                    'reportPVO.SetParameterValue("CodeSouCompo", CodSouComp, "PvOuverturePageGarde.rpt")
                    'reportPVO.SetParameterValue("CodeSouCompo", CodSouComp)
                    'query = "select LibellePartition from T_Partition where LibelleCourt='" & CodSouComp & "' and CodeProjet='" & ProjetEnCours & "'"
                    'dt0 = ExcecuteSelectQuery(query)
                    'For Each rw As DataRow In dt0.Rows
                    '    reportPVO.SetParameterValue("LibSouCompo", MettreApost(rw(0).ToString).ToUpper, "PvOuverturePageGarde.rpt")
                    '    reportPVO.SetParameterValue("LibelleSouCompo", MettreApost(rw(0).ToString))
                    'Next

                    Dim NbDaoRetires As Decimal = Val(ExecuteScallar("select Count(*) from T_Fournisseur where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet='" & ProjetEnCours & "'"))
                    reportPVO.SetParameterValue("NbDossierRetires", NbDaoRetires.ToString)
                    reportPVO.SetParameterValue("NbDossierRetiresLettre", MontantLettre(NbDaoRetires.ToString))

                    Dim NbOffresRecues As Decimal = Val(ExecuteScallar("select Count(*) from T_Fournisseur where DateDepotDAO<>'' and NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet='" & ProjetEnCours & "'"))
                    reportPVO.SetParameterValue("NbOffresDeposes", NbOffresRecues.ToString)
                    reportPVO.SetParameterValue("NbOffresDeposesLettre", MontantLettre(NbOffresRecues.ToString))

                    If (Deb = "Eta") Then
                        reportPVO.SetParameterValue("Copie", "COPIE")
                        reportPVO.SetParameterValue("Copie", "COPIE", "PvOuverturePageGarde.rpt")
                    Else
                        reportPVO.SetParameterValue("Copie", "")
                        reportPVO.SetParameterValue("Copie", "", "PvOuverturePageGarde.rpt")
                    End If

                    If (Deb <> "Eta") Then
                        Dim DossierPV As String = line & "\DAO\" & TxtTypeMarche.Text & "\" & TxtMethode.Text & "\" & Format(CmbNumDAO.Text, "_") & "\" & "PvOuverture"
                        If (Directory.Exists(DossierPV) = False) Then
                            Directory.CreateDirectory(DossierPV)
                        End If
                        reportPVO.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, DossierPV & "\PVO.pdf")
                    End If

                    FullScreenReport.FullView.ReportSource = reportPVO
                    FullScreenReport.Text = "PV D'OUVERTURE DU DOSSIER N°" & CmbNumDAO.Text
                    FinChargement()
                    FullScreenReport.ShowDialog()

                    If (Deb <> "Eta") Then
                        InitInfosDossier()
                        BtDureeSeance.Text = "00   :   00   :   00"
                        BtOuvertureOffre.Text = ""
                        BtOuvertureOffre.Enabled = False
                        dt.Columns.Clear()
                        dt.Rows.Clear()
                        'TxtCodePresence.Enabled = False
                    End If
                End If
            Else
                FailMsg("Veuillez sélectionner un dossier.")
                CmbNumDAO.Select()
            End If
        Catch ex As Exception
            FinChargement()
            FailMsg("Information non disponible : " & vbNewLine & ex.ToString)
        End Try
    End Sub

    Private Sub InitInfosDossier()
        LblRecapOffres.Text = "..."
        TxtNbLot.Text = ""
        TxtDateOuverture.Text = ""
        TxtDateCloture.Text = ""
        TxtLibelleDAO.Text = ""
        TxtTypeMarche.Text = ""
        TxtMethode.Text = ""
        CmbNumDAO.Text = ""
    End Sub

    Private Sub InitInfosSoumis()
        CmbNomSoumis.Text = ""
        TxtPaysSoumis.Text = ""
        TxtAdresseSoumis.Text = ""
        TxtTelSoumis.Text = ""
        TxtCelSoumis.Text = ""
        TxtFaxSoumis.Text = ""
        TxtMailSoumis.Text = ""
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        DureeSeance = DureeSeance + 1
        BtDureeSeance.Text = CalculTps(DureeSeance)
    End Sub
    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If CmbNumDAO.Text <> "" And BtOuvertureOffre.Enabled = False Then
            RemplirListePresence(CmbNumDAO.Text)
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
            leCode = ""
            ' query = "select PaysFournis,AdresseCompleteFournis,TelFournis,FaxFournis,CelFournis,MailFournis,CodeFournis from T_Fournisseur where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet='" & ProjetEnCours & "' and NomFournis='" & EnleverApost(CmbNomSoumis.Text) & "'"

            query = "select PaysFournis,AdresseCompleteFournis,TelFournis,FaxFournis,CelFournis,MailFournis,CodeFournis from T_Fournisseur where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet='" & ProjetEnCours & "' and CodeFournis='" & CInt(CmbNomSoumis.Text.Split(" "c)(0)) & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows

                leCode = rw("CodeFournis").ToString
                TxtPaysSoumis.Text = MettreApost(rw("PaysFournis").ToString)
                TxtAdresseSoumis.Text = MettreApost(rw("AdresseCompleteFournis").ToString)
                TxtTelSoumis.Text = MettreApost(rw("TelFournis").ToString)
                TxtFaxSoumis.Text = MettreApost(rw("FaxFournis").ToString)
                TxtCelSoumis.Text = MettreApost(rw("CelFournis").ToString)
                TxtMailSoumis.Text = MettreApost(rw("MailFournis").ToString).ToLower
                TxtCodeSoumis.Text = rw("CodeFournis").ToString
                If OuvertureTermine = False Then
                    GbSaisieInfos.Enabled = True
                End If
            Next

            'If leCode <> "" Then

            '    Dim lesCrit(100) As String
            '    Dim nbCr As Decimal = 0
            '    query = "select RefCritere from T_DAO_PostQualif where NumeroDAO='" & CmbNumDAO.Text & "' and RefCritereMere<>'0'"
            '    dt0 = ExcecuteSelectQuery(query)
            '    For Each rw As DataRow In dt0.Rows
            '        lesCrit(nbCr) = rw(0).ToString
            '        nbCr += 1
            '    Next

            '    query = "DELETE from T_SoumisFournisPostQualif where CodeFournis='" & leCode & "'"
            '    ExecuteNonQuery(query)

            '    Dim sqlconn As New MySqlConnection
            '    BDOPEN(sqlconn)
            '    For k As Integer = 0 To nbCr - 1
            '        Dim DatSet = New DataSet

            '        query = "select * from T_SoumisFournisPostQualif"
            '        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            '        Dim DatAdapt = New MySqlDataAdapter(Cmd)
            '        DatAdapt.Fill(DatSet, "T_SoumisFournisPostQualif")
            '        Dim DatTable = DatSet.Tables("T_SoumisFournisPostQualif")
            '        Dim DatRow = DatSet.Tables("T_SoumisFournisPostQualif").NewRow()

            '        DatRow("CodeFournis") = leCode
            '        DatRow("RefCritere") = lesCrit(k)

            '        DatSet.Tables("T_SoumisFournisPostQualif").Rows.Add(DatRow)
            '        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            '        DatAdapt.Update(DatSet, "T_SoumisFournisPostQualif")
            '        DatSet.Clear()
            '    Next
            '    BDQUIT(sqlconn)

            'End If
            If OuvertureTermine = True Then
                GbSaisieInfos.Enabled = False
            End If
            MajGridRecap()
            MajTreeListRecap()
            InitSaisieInfos()
            btnAjoutSousLot.Enabled = False
            GridMontantDesSousLot()
        End If

    End Sub

    Private Sub ItemDevise()
        CmbDevise.Text = ""
        CmbDevise.Properties.Items.Clear()
        query = "select AbregeDevise from T_Devise"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbDevise.Properties.Items.Add(MettreApost(rw("AbregeDevise").ToString))
        Next
    End Sub

    Private Sub CmbNumLot_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumLot.SelectedValueChanged
        query = "select LibelleLot,RefLot from T_LotDAO where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeLot='" & CmbNumLot.Text & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            TxtLibelleLot.Text = MettreApost(rw("LibelleLot").ToString)
            TxtRefLot.Text = rw("RefLot").ToString
        Next

        If CmbNumLot.Text <> "" Then
            Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDAO.Text)
            Dim nbsouslot As Integer = Val(Resultat(0))
            If nbsouslot > 0 Then
                btnAjoutSousLot.Enabled = True
                TxtMontantOffre.Text = ""
                TxtMontantOffre.Enabled = False
            Else
                TxtMontantOffre.Text = ""
                TxtMontantOffre.Enabled = True
                btnAjoutSousLot.Enabled = False
            End If
        End If
    End Sub

    Private Sub CmbDevise_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbDevise.SelectedValueChanged
        query = "select LibelleDevise,AbregeDevise from T_Devise where AbregeDevise='" & EnleverApost(CmbDevise.Text) & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            TxtDevise.Text = rw("LibelleDevise").ToString
            TxtDeviseGarantie.Text = rw("AbregeDevise").ToString
        Next
    End Sub

    Private Sub MajCmbCompte()
        CmbEtsBancaire.Text = ""
        CmbEtsBancaire.Properties.Items.Clear()
        query = "select CodeBanque from T_Banque order by CodeBanque"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbEtsBancaire.Properties.Items.Add(MettreApost(rw("CodeBanque").ToString))
        Next
    End Sub

    Private Sub BtEnrgOffre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgOffre.Click
        If CmbNumDAO.SelectedIndex = -1 Then
            FailMsg("Veuillez sélectionner un dossier.")
            CmbNumDAO.Select()
            Exit Sub
        End If
        If CmbNomSoumis.SelectedIndex = -1 Then
            FailMsg("Veuillez sélectionner un soumissoinnaire.")
            CmbNomSoumis.Select()
            Exit Sub
        End If
        If CmbNumLot.SelectedIndex = -1 Then
            FailMsg("Veuillez sélectionner un lot.")
            CmbNumLot.Select()
            Exit Sub
        End If

        Dim erreur As String = ""
        'Si les montant sous lot ne sont pas renseigné
        If btnAjoutSousLot.Enabled = True Then
            Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDAO.Text)
            Dim nbsouslot As Integer = Val(Resultat(0))
            If ViewMontantDesSL.RowCount <> nbsouslot Then
                erreur += "- le montant des sous-lots" + ControlChars.CrLf
            End If
        End If

        'si la monnaie n'est pas renseigné
        If CmbDevise.SelectedIndex = -1 Then
            erreur += "- Monnaie" + ControlChars.CrLf
        End If
        'si le montant de l'offre n'est pas renseigné
        If TxtMontantOffre.Text = "" Then
            erreur += "- Montant offre" + ControlChars.CrLf
        End If

        'si  la taxes n'est pas renseigné
        'If CmbTaxes.SelectedIndex = -1 Then
        '    erreur += "- Taxes" + ControlChars.CrLf
        'End If
        'si garantie offre n'est pas renseigné
        'If TxtGarantieOffre.Text = "" Then
        '    erreur += "- Garantie offre" + ControlChars.CrLf
        'End If
        'si  la livraison n'est pas renseigné

        If NumDelaiLivraison.Value <= 0 Then
            erreur += "- Livraison" + ControlChars.CrLf
        End If
        'si delai livraison n'est pas renseigné
        If CmbDelaiLivraison.SelectedIndex = -1 Then
            erreur += "- Type de délai de livraison" + ControlChars.CrLf
        End If

        If NumValidOffre.Value <= 0 Then
            erreur += "- Validité de l'offre" + ControlChars.CrLf
        End If
        'si delai livraison n'est pas renseigné
        If CmbValidOffre.SelectedIndex = -1 Then
            erreur += "- Type de validité de l'offre" + ControlChars.CrLf
        End If

        'si banque garant n'est pas renseigné
        'If CmbEtsBancaire.SelectedIndex = -1 Then
        '    erreur += "- La banque garant de l'offre" + ControlChars.CrLf
        'End If
        If TxtGarantieOffre.Text <> "" Then
            If Val(TxtGarantieOffre.Text) > 0 Then
                If CmbEtsBancaire.IsRequiredControl("Veuillez selectionner un element dans la liste.") Then
                    CmbEtsBancaire.Select()
                    Exit Sub
                End If
            End If
        End If
        If AttestationReguFiscal.ToString = "OUI" Then
            If DateRegFiscale.IsRequiredControl("Veuillez sélectionner la date de l'attestation de régularité fiscale.") Then
                DateRegFiscale.Focus()
                Exit Sub
            End If
        End If
        If AttestationCNPS.ToString = "OUI" Then
            If DateAttestationsqlconnPS.IsRequiredControl("Veuillez sélectionner la date de l'attestation sociale.") Then
                DateAttestationsqlconnPS.Focus()
                Exit Sub
            End If
        End If

        If erreur = "" Then
            ' Vérif de l'existance d'un lot

            If Val(ExecuteScallar("select count(*) from T_SoumissionFournisseur where CodeLot='" & CmbNumLot.Text & "' and CodeFournis='" & TxtCodeSoumis.Text & "'")) > 0 Then
                SuccesMsg("Cette  offre existe déjà.")
                Exit Sub
            End If

            'query = "select count(*) from T_SoumissionFournisseur where CodeLot='" & CmbNumLot.Text & "' and CodeFournis='" & TxtCodeSoumis.Text & "'"
            'Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            'For Each rw In dt0.Rows
            '    If rw("CodeSousLot").ToString <> "" Then
            '        'query = "select * from T_SoumissionFournisseur where CodeLot='" & CmbNumLot.Text & "' and CodeFournis='" & TxtCodeSoumis.Text & "' And CodeSousLot='" & cmbSousLot.Text & "'"
            '        query = "select * from T_SoumissionFournisseur where CodeLot='" & CmbNumLot.Text & "' and CodeFournis='" & TxtCodeSoumis.Text & "'"
            '        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            '        If dt1.Rows.Count > 0 Then
            '            SuccesMsg("Cette  offre existe déjà!")
            '            Exit Sub
            '        End If
            '    Else
            '        If dt0.Rows.Count > 0 Then
            '            SuccesMsg("Cette  offre existe déjà!")
            '            Exit Sub
            '        End If
            '    End If
            'Next

            'Enregistrement des critères par lot
            If leCode <> "" Then

                Dim lesCrit(100) As String
                Dim nbCr As Decimal = 0
                query = "select RefCritere from T_DAO_PostQualif where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and RefCritereMere<>'0'"
                dt = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    lesCrit(nbCr) = rw("RefCritere").ToString
                    nbCr += 1
                Next

                ExecuteNonQuery("DELETE from T_SoumisFournisPostQualif where CodeFournis='" & leCode & "' AND CodeLot='" & CmbNumLot.Text & "'")
                If nbCr > 0 Then
                    For k As Integer = 0 To nbCr - 1
                        ExecuteNonQuery("insert into T_SoumisFournisPostQualif values('" & leCode & "', '" & CmbNumLot.Text & "', '" & lesCrit(k) & "', NULL, NULL)")
                    Next
                End If
            End If

            'Enregistrement du fournisseur
            Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDAO.Text)
            Dim nbsouslot As Integer = Val(Resultat(0))

            If nbsouslot > 0 Then 'Lot contenant des sous lots
                For i = 0 To ViewMontantDesSL.RowCount - 1
                    Dim DatSet = New DataSet
                    query = "select * from T_SoumissionFournisseur"
                    Dim sqlconn As New MySqlConnection
                    BDOPEN(sqlconn)
                    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                    Dim DatAdapt = New MySqlDataAdapter(Cmd)
                    DatAdapt.Fill(DatSet, "T_SoumissionFournisseur")
                    Dim DatTable = DatSet.Tables("T_SoumissionFournisseur")
                    Dim DatRow = DatSet.Tables("T_SoumissionFournisseur").NewRow()
                    DatRow("CodeFournis") = TxtCodeSoumis.Text
                    DatRow("RefLot") = TxtRefLot.Text
                    DatRow("CodeLot") = CmbNumLot.Text
                    DatRow("CodeSousLot") = ViewMontantDesSL.GetRowCellValue(i, "Code Sous lot")
                    DatRow("Monnaie") = CmbDevise.Text
                    'DatRow("HtHdTtc") = CmbTaxes.Text
                    DatRow("HtHdTtc") = ""
                    DatRow("MontantPropose") = CDbl(ViewMontantDesSL.GetRowCellValue(i, "Montant soumission").ToString.Replace(" ", ""))
                    DatRow("AttRegFiscale") = DateRegFiscale.Text
                    DatRow("AttCNPS") = DateAttestationsqlconnPS.Text
                    DatRow("CautionBancaire") = CDbl(TxtGarantieOffre.Text.ToString.Replace(" ", ""))
                    DatRow("BanqueCaution") = EnleverApost(CmbEtsBancaire.Text)
                    DatRow("ValiditeOffre") = NumValidOffre.Value.ToString & " " & CmbValidOffre.Text
                    DatRow("DelaiLivraison") = NumDelaiLivraison.Value.ToString & " " & CmbDelaiLivraison.Text
                    DatRow("Observations") = EnleverApost(TxtObserv.Text)
                    DatRow("DateSaisie") = Now.ToShortDateString & " " & Now.ToLongTimeString
                    DatRow("DateModif") = Now.ToShortDateString & " " & Now.ToLongTimeString
                    DatRow("Operateur") = CodeUtilisateur
                    DatSet.Tables("T_SoumissionFournisseur").Rows.Add(DatRow)
                    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                    DatAdapt.Update(DatSet, "T_SoumissionFournisseur")
                    DatSet.Clear()
                    BDQUIT(sqlconn)
                Next
            Else
                Dim DatSet = New DataSet
                query = "select * from T_SoumissionFournisseur"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "T_SoumissionFournisseur")
                Dim DatTable = DatSet.Tables("T_SoumissionFournisseur")
                Dim DatRow = DatSet.Tables("T_SoumissionFournisseur").NewRow()
                DatRow("CodeFournis") = TxtCodeSoumis.Text
                DatRow("RefLot") = TxtRefLot.Text
                DatRow("CodeLot") = CmbNumLot.Text
                DatRow("CodeSousLot") = ""
                DatRow("Monnaie") = CmbDevise.Text
                'DatRow("HtHdTtc") = CmbTaxes.Text
                DatRow("HtHdTtc") = ""
                DatRow("MontantPropose") = CDbl(TxtMontantOffre.Text.ToString.Replace(" ", ""))
                DatRow("AttRegFiscale") = DateRegFiscale.Text
                DatRow("AttCNPS") = DateAttestationsqlconnPS.Text
                DatRow("CautionBancaire") = CDbl(TxtGarantieOffre.Text.ToString.Replace(" ", ""))
                DatRow("BanqueCaution") = EnleverApost(CmbEtsBancaire.Text)
                DatRow("ValiditeOffre") = NumValidOffre.Value.ToString & " " & CmbValidOffre.Text
                DatRow("DelaiLivraison") = NumDelaiLivraison.Value.ToString & " " & CmbDelaiLivraison.Text
                DatRow("Observations") = EnleverApost(TxtObserv.Text)
                DatRow("DateSaisie") = Now.ToShortDateString & " " & Now.ToLongTimeString
                DatRow("DateModif") = Now.ToShortDateString & " " & Now.ToLongTimeString
                DatRow("Operateur") = CodeUtilisateur
                DatSet.Tables("T_SoumissionFournisseur").Rows.Add(DatRow)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "T_SoumissionFournisseur")
                DatSet.Clear()
                BDQUIT(sqlconn)
            End If

            SuccesMsg("L'offre a été enregistrée avec succés.")
            MajGridRecap()
            MajTreeListRecap()
            InitSaisieInfos()
            GridMontantDesSousLot()
        Else
            SuccesMsg("Veuillez remplir ces champs : " + ControlChars.CrLf + erreur)
        End If
    End Sub
    Private Sub ViderGridRecap()
        dt2.Columns.Clear()
        dt2.Rows.Clear()
    End Sub

    Private Sub InitSaisieInfos()
        'cmbSousLot.Text = ""
        'cmbSousLot.Properties.Items.Clear()
        'txtLibelleSousLot.Text = ""
        btnAjoutSousLot.Text = "Ajout montant sous lot"
        CmbNumLot.Text = ""
        TxtLibelleLot.Text = ""
        CmbDevise.Text = ""
        TxtDevise.Text = ""
        TxtMontantOffre.Text = ""
        'CmbTaxes.Text = ""
        TxtGarantieOffre.Text = ""
        TxtDeviseGarantie.Text = ""
        NumDelaiLivraison.Value = 0
        CmbDelaiLivraison.Text = ""
        CmbEtsBancaire.Text = ""
        'DateRegFiscale.DateTime = "01/01/" & Now.Year
        DateRegFiscale.EditValue = Nothing
        ' DateAttestationsqlconnPS.DateTime = "01/01/" & Now.Year
        DateAttestationsqlconnPS.EditValue = Nothing
        NumValidOffre.Value = 0
        CmbValidOffre.Text = ""
        TxtObserv.Text = ""
    End Sub

    Private Sub OuvertureOffres_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub CmbNumLot_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbNumLot.SelectedIndexChanged
        ' DateRegFiscale.DateTime = Now.ToShortDateString
        ' DateAttestationsqlconnPS.DateTime = Now.ToShortDateString
    End Sub

    Private Sub ModifierLoffreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModifierLoffreToolStripMenuItem.Click
        If ListeRecap.Nodes.Count > 0 Then
            btnAjoutSousLot.Text = "Modifier montant sous lots"
            Dim node As TreeListNode = ListeRecap.FocusedNode
            Dim node1 As TreeListNode = ListeRecap.FocusedNode.ParentNode

            If node.ParentNode Is Nothing Then 'Lot ne contenant pas de sous lot
                If Not node.HasChildren Then
                    'GridSousLot.Visible = False
                    txtRefSoumis.Text = node.GetValue("RefSoumis").ToString
                    query = "select * from t_soumissionfournisseur where RefSoumis='" & txtRefSoumis.Text & "'"
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        Dim DelaiLiv = rw("DelaiLivraison").ToString.Split(" ")
                        Dim ValideOffre = rw("ValiditeOffre").ToString.Split(" ")
                        CmbNumLot.Text = rw("CodeLot").ToString
                        CmbDevise.Text = rw("Monnaie").ToString
                        TxtMontantOffre.Text = rw("MontantPropose").ToString
                        'CmbTaxes.Text = rw("HtHdTtc").ToString
                        TxtGarantieOffre.Text = rw("CautionBancaire").ToString
                        CmbEtsBancaire.Text = MettreApost(rw("BanqueCaution").ToString)
                        NumDelaiLivraison.Text = DelaiLiv(0).ToString
                        CmbDelaiLivraison.Text = DelaiLiv(1).ToString
                        DateRegFiscale.Text = rw("AttRegFiscale").ToString
                        DateAttestationsqlconnPS.Text = rw("AttCNPS").ToString
                        NumValidOffre.Text = ValideOffre(0).ToString
                        CmbValidOffre.Text = MettreApost(ValideOffre(1).ToString)
                        TxtObserv.Text = MettreApost(rw("Observations").ToString)
                    Next
                Else
                    'GridSousLot.Visible = True
                    txtRefSoumis.Text = node.GetValue("RefSoumis").ToString
                    query = "select * from t_soumissionfournisseur where RefSoumis='" & txtRefSoumis.Text & "'"
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        Dim DelaiLiv = rw("DelaiLivraison").ToString.Split(" ")
                        Dim ValideOffre = rw("ValiditeOffre").ToString.Split(" ")
                        CmbNumLot.Text = rw("CodeLot").ToString
                        CmbDevise.Text = rw("Monnaie").ToString
                        'CmbTaxes.Text = rw("HtHdTtc").ToString
                        TxtGarantieOffre.Text = rw("CautionBancaire").ToString
                        CmbEtsBancaire.Text = MettreApost(rw("BanqueCaution").ToString)
                        NumDelaiLivraison.Text = DelaiLiv(0).ToString
                        CmbDelaiLivraison.Text = DelaiLiv(1).ToString
                        DateRegFiscale.Text = rw("AttRegFiscale").ToString
                        DateAttestationsqlconnPS.Text = rw("AttCNPS").ToString
                        NumValidOffre.Text = ValideOffre(0).ToString
                        CmbValidOffre.Text = MettreApost(ValideOffre(1).ToString)
                        TxtObserv.Text = MettreApost(rw("Observations").ToString)
                    Next
                    query = "select CodeLot,MontantPropose,RefSoumis,CodeSousLot from T_SoumissionFournisseur where CodeFournis='" & TxtCodeSoumis.Text & "' and CodeLot='" & CmbNumLot.Text & "'"
                    Dim dtx = ExcecuteSelectQuery(query)
                    For Each rw In dtx.Rows
                        Dim dt As DataTable = GridMontantDesSL.DataSource
                        Dim drS As DataRow = dt.NewRow
                        drS("Code Sous lot") = rw("CodeSousLot").ToString
                        drS("Montant soumission") = AfficherMonnaie(rw("MontantPropose").ToString)
                        drS("RefSoumis") = rw("RefSoumis").ToString
                        dt.Rows.Add(drS)
                    Next
                    TxtMontantOffre.Text = AjoutMontantSousLot.UpdateMontantLot()
                End If
            Else
                'GridSousLot.Visible = True
                txtRefSoumis.Text = node.GetValue("RefSoumis").ToString
                query = "select * from t_soumissionfournisseur where RefSoumis='" & txtRefSoumis.Text & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    Dim DelaiLiv = rw("DelaiLivraison").ToString.Split(" ")
                    Dim ValideOffre = rw("ValiditeOffre").ToString.Split(" ")
                    CmbNumLot.Text = rw("CodeLot").ToString
                    CmbDevise.Text = rw("Monnaie").ToString
                    'CmbTaxes.Text = rw("HtHdTtc").ToString
                    TxtGarantieOffre.Text = rw("CautionBancaire").ToString
                    CmbEtsBancaire.Text = MettreApost(rw("BanqueCaution").ToString)
                    NumDelaiLivraison.Text = DelaiLiv(0).ToString
                    CmbDelaiLivraison.Text = DelaiLiv(1).ToString
                    DateRegFiscale.Text = rw("AttRegFiscale").ToString
                    DateAttestationsqlconnPS.Text = rw("AttCNPS").ToString
                    NumValidOffre.Text = ValideOffre(0).ToString
                    CmbValidOffre.Text = MettreApost(ValideOffre(1).ToString)
                    TxtObserv.Text = MettreApost(rw("Observations").ToString)
                Next
                query = "select CodeLot,MontantPropose,RefSoumis,CodeSousLot from T_SoumissionFournisseur where CodeFournis='" & TxtCodeSoumis.Text & "' and CodeLot='" & CmbNumLot.Text & "'"
                Dim dtx = ExcecuteSelectQuery(query)
                For Each rw In dtx.Rows
                    Dim dt As DataTable = GridMontantDesSL.DataSource
                    Dim drS As DataRow = dt.NewRow
                    drS("Code Sous lot") = rw("CodeSousLot").ToString
                    drS("Montant soumission") = AfficherMonnaie(rw("MontantPropose").ToString)
                    drS("RefSoumis") = rw("RefSoumis").ToString
                    dt.Rows.Add(drS)
                Next
                TxtMontantOffre.Text = AjoutMontantSousLot.UpdateMontantLot()
            End If
            BtModOffre.Enabled = True
            BtEnrgOffre.Enabled = False
            CmbNumLot.Enabled = False
        End If
    End Sub

    Private Sub RemplirListePresence(ByVal NumeroDAO As String)
        If (CmbNumDAO.Text <> "") Then
            Dim NbreCOJOPointe As Boolean = True

            dt.Columns.Clear()
            dt.Columns.Add("Nom", Type.GetType("System.String"))
            dt.Columns.Add("Téléphone", Type.GetType("System.String"))
            dt.Columns.Add("Date et heure de pointage", Type.GetType("System.String"))
            dt.Rows.Clear()
            query = "select * from T_Commission where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "'" ' AND TypeComm='COJO'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                Dim drS = dt.NewRow()

                drS("Nom") = MettreApost(rw("NomMem").ToString) & " (" & rw("TitreMem").ToString & ")"
                drS("Téléphone") = MettreApost(rw("TelMem").ToString)

                If rw("Pointage").ToString = "" Then
                    drS("Date et heure de pointage") = "En attente"
                    NbreCOJOPointe = False
                Else
                    drS("Date et heure de pointage") = MettreApost(rw("Pointage").ToString)
                End If
                dt.Rows.Add(drS)
            Next

            GridCojo.DataSource = dt
            GridView1.OptionsView.ColumnAutoWidth = True
            GridView1.OptionsBehavior.AutoExpandAllGroups = True

            If NbreCOJOPointe = False Then
                BtOuvertureOffre.Enabled = False
            Else
                BtOuvertureOffre.Enabled = True
            End If
        End If
    End Sub

    Private Sub BtModOffre_Click(sender As Object, e As EventArgs) Handles BtModOffre.Click
        If CmbNumDAO.SelectedIndex = -1 Then
            FailMsg("Veuillez sélectionner un dossier.")
            CmbNumDAO.Select()
            Exit Sub
        End If
        If CmbNomSoumis.SelectedIndex = -1 Then
            FailMsg("Veuillez sélectionner un soumissoinnaire.")
            CmbNomSoumis.Select()
            Exit Sub
        End If
        If CmbNumLot.SelectedIndex = -1 Then
            FailMsg("Veuillez sélectionner un lot.")
            CmbNumLot.Select()
            Exit Sub
        End If
        Dim erreur As String = ""
        'si la monnaie n'est pas renseigné
        If CmbDevise.SelectedIndex = -1 Then
            erreur += "- Monnaie" + ControlChars.CrLf
        End If
        'si le montant de l'offre n'est pas renseigné
        If TxtMontantOffre.Text = "" Then
            erreur += "- Montant offre" + ControlChars.CrLf
        End If
        'si  la taxes n'est pas renseigné
        'If CmbTaxes.SelectedIndex = -1 Then
        '    erreur += "- Taxes" + ControlChars.CrLf
        'End If
        'Si les montant sous lot ne sont pas renseigné
        'If cmbSousLot.Enabled = True Then
        '    Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDAO.Text)
        '    Dim nbsouslot As Integer = Val(Resultat(0))
        '    If ViewMontantSL.RowCount <> nbsouslot Then
        '        erreur += "- les montants des sous lot" + ControlChars.CrLf
        '    End If
        'End If
        'si garantie offre n'est pas renseigné
        'If TxtGarantieOffre.EditValue< Then
        '    erreur += "- Garantie offre" + ControlChars.CrLf
        'End If
        'si  le livraison n'est pas renseigné
        If NumDelaiLivraison.Value <= 0 Then
            erreur += "- Livraison" + ControlChars.CrLf
        End If
        'si delai livraison n'est pas renseigné
        If CmbDelaiLivraison.SelectedIndex = -1 Then
            erreur += "- Type de délai de livraison" + ControlChars.CrLf
        End If
        'si banque garant n'est pas renseigné
        'If CmbEtsBancaire.SelectedIndex = -1 Then
        '    erreur += "- La banque garant de l'offre" + ControlChars.CrLf
        'End If
        If NumValidOffre.Value <= 0 Then
            erreur += "- Validité de l'offre" + ControlChars.CrLf
        End If
        'si delai livraison n'est pas renseigné
        If CmbValidOffre.SelectedIndex = -1 Then
            erreur += "- Type de validité de l'offre" + ControlChars.CrLf
        End If
        If AttestationReguFiscal.ToString = "OUI" Then
            If DateRegFiscale.IsRequiredControl("Veuillez sélectionner la date de l'attestation de régularité fiscale.") Then
                DateRegFiscale.Focus()
                Exit Sub
            End If
        End If
        If AttestationCNPS.ToString = "OUI" Then
            If DateAttestationsqlconnPS.IsRequiredControl("Veuillez sélectionner la date de l'attestation sociale.") Then
                DateAttestationsqlconnPS.Focus()
                Exit Sub
            End If
        End If
        If TxtGarantieOffre.Text <> "" Then
            If Val(TxtGarantieOffre.Text) > 0 Then
                If CmbEtsBancaire.IsRequiredControl("Veuillez selectionner un element dans la liste.") Then
                    CmbEtsBancaire.Select()
                    Exit Sub
                End If
            End If
        End If

        If erreur = "" Then
            If ConfirmMsg("Voulez-vous modifier cette offre ?") = DialogResult.Yes Then
                ' Modification de l'offre
                Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDAO.Text)
                Dim nbsouslot As Integer = Val(Resultat(0))
                If nbsouslot > 0 Then
                    For i = 0 To ViewMontantDesSL.RowCount - 1
                        query = "UPDATE T_SoumissionFournisseur SET CodeLot='" & EnleverApost(CmbNumLot.Text) & "', Monnaie='" & EnleverApost(CmbDevise.Text) & "', MontantPropose='" & CDbl(ViewMontantDesSL.GetRowCellValue(i, "Montant soumission").ToString) & "',HtHdTtc='" & EnleverApost(CmbTaxes.Text) & "', CautionBancaire='" & CDbl(EnleverApost(TxtGarantieOffre.Text)) & "'"
                        query &= ",BanqueCaution='" & EnleverApost(CmbEtsBancaire.Text) & "',DelaiLivraison='" & NumDelaiLivraison.Value.ToString & " " & EnleverApost(CmbDelaiLivraison.Text) & "',AttRegFiscale='" & DateRegFiscale.Text & "', AttCNPS='" & DateAttestationsqlconnPS.Text & "'"
                        query &= ", CodeSousLot='" & EnleverApost(ViewMontantDesSL.GetRowCellValue(i, "Code Sous lot").ToString) & "', ValiditeOffre='" & NumValidOffre.Value.ToString & " " & EnleverApost(CmbValidOffre.Text) & "',Observations='" & EnleverApost(TxtObserv.Text) & "',DateModif='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' WHERE RefSoumis='" & ViewMontantDesSL.GetRowCellValue(i, "RefSoumis").ToString & "'"
                        ExecuteNonQuery(query)
                    Next
                Else
                    query = "UPDATE T_SoumissionFournisseur SET CodeLot='" & EnleverApost(CmbNumLot.Text) & "',Monnaie='" & EnleverApost(CmbDevise.Text) & "',MontantPropose='" & CDbl(TxtMontantOffre.Text) & "',HtHdTtc='" & EnleverApost(CmbTaxes.Text) & "', CautionBancaire='" & CDbl(EnleverApost(TxtGarantieOffre.Text)) & "'"
                    query &= ",BanqueCaution='" & EnleverApost(CmbEtsBancaire.Text) & "',DelaiLivraison='" & NumDelaiLivraison.Value.ToString & " " & EnleverApost(CmbDelaiLivraison.Text) & "',AttRegFiscale='" & DateRegFiscale.Text & "',AttCNPS='" & DateAttestationsqlconnPS.Text & "'"
                    query &= ", ValiditeOffre='" & NumValidOffre.Value.ToString & " " & EnleverApost(CmbValidOffre.Text) & "',Observations='" & EnleverApost(TxtObserv.Text) & "', DateModif='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' WHERE RefSoumis='" & txtRefSoumis.Text & "'"
                    ExecuteNonQuery(query)
                End If
                SuccesMsg("L'offre a été modifiée avec succés.")
                BtEnrgOffre.Enabled = True
                BtModOffre.Enabled = False
                MajTreeListRecap()
                InitSaisieInfos()
                GridMontantDesSousLot()
                btnAjoutSousLot.Enabled = False
                CmbNumLot.Enabled = True
            End If
        Else
            SuccesMsg("Veuillez remplir ces champs : " + ControlChars.CrLf + erreur)
        End If
    End Sub
    Private Sub BtAnulOffre_Click(sender As Object, e As EventArgs) Handles BtAnulOffre.Click
        BtEnrgOffre.Enabled = True
        BtModOffre.Enabled = False
        InitSaisieInfos()
        GridMontantDesSousLot()
        btnAjoutSousLot.Enabled = False
        CmbNumLot.Enabled = True
    End Sub

    Private Sub MajTreeListRecap()
        ListeRecap.Nodes.Clear()
        query = "select CodeLot,Monnaie,HtHdTtc,MontantPropose,AttRegFiscale,BanqueCaution,CautionBancaire,AttCNPS,DelaiLivraison,RefSoumis,CodeSousLot from T_SoumissionFournisseur where CodeFournis='" & TxtCodeSoumis.Text & "' GROUP by CodeLot order by CodeLot"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        ListeRecap.BeginUnboundLoad()
        Dim parentForRootNodes As TreeListNode = Nothing
        For Each rw As DataRow In dt0.Rows
            Dim Resultat As Object() = GetSousLot(rw("CodeLot").ToString, CmbNumDAO.Text)
            Dim nbsouslot As Integer = Val(Resultat(0))
            If nbsouslot > 0 Then
                query = "select DISTINCT CodeLot,Monnaie,HtHdTtc,SUM(MontantPropose),AttRegFiscale,BanqueCaution,CautionBancaire,AttCNPS,DelaiLivraison,RefSoumis,CodeSousLot from T_SoumissionFournisseur where CodeFournis='" & TxtCodeSoumis.Text & "' and CodeLot='" & rw("CodeLot").ToString & "' GROUP by CodeLot"
                Dim dt1 = ExcecuteSelectQuery(query)
                For Each rw1 In dt1.Rows
                    Dim rootNode As TreeListNode = ListeRecap.AppendNode(New Object() {"N°" & rw1("CodeLot").ToString, rw1("CodeSousLot").ToString, rw1("Monnaie").ToString, AfficherMonnaie(rw1("SUM(MontantPropose)").ToString) & " " & rw1("HtHdTtc").ToString, AfficherMonnaie(rw1("CautionBancaire").ToString) & IIf(rw1("BanqueCaution").ToString <> "", " (" & MettreApost(rw1("BanqueCaution").ToString) & ")", "").ToString, rw1("AttRegFiscale").ToString, MettreApost(rw1("AttCNPS").ToString), rw1("DelaiLivraison").ToString, rw1("RefSoumis").ToString}, parentForRootNodes)
                    query = "select CodeLot,Monnaie,HtHdTtc,MontantPropose,AttRegFiscale,BanqueCaution,CautionBancaire,AttCNPS,DelaiLivraison,RefSoumis,CodeSousLot from T_SoumissionFournisseur where CodeFournis='" & TxtCodeSoumis.Text & "' and CodeLot='" & rw1("CodeLot").ToString & "'"
                    Dim dt2 = ExcecuteSelectQuery(query)
                    For Each rw2 In dt2.Rows
                        ListeRecap.AppendNode(New Object() {"N°" & rw2("CodeSousLot").ToString, "", "", AfficherMonnaie(rw2("MontantPropose").ToString) & " " & rw2("HtHdTtc").ToString, "", "", "", "", rw2("RefSoumis").ToString}, rootNode)
                    Next
                Next
            Else
                ListeRecap.AppendNode(New Object() {"N°" & rw("CodeLot").ToString, rw("CodeSousLot").ToString, rw("Monnaie").ToString, AfficherMonnaie(rw("MontantPropose").ToString) & " " & rw("HtHdTtc").ToString, AfficherMonnaie(rw("CautionBancaire").ToString) & IIf(rw("BanqueCaution").ToString <> "", " (" & MettreApost(rw("BanqueCaution").ToString) & ")", "").ToString, rw("AttRegFiscale").ToString, MettreApost(rw("AttCNPS").ToString), rw("DelaiLivraison").ToString, rw("RefSoumis").ToString}, parentForRootNodes)
            End If
        Next
        ListeRecap.EndUnboundLoad()
    End Sub

    Private Sub MajGridRecap()
        If (TxtCodeSoumis.Text <> "" And CmbNumDAO.Text <> "") Then
            dt2.Columns.Clear()
            dt2.Columns.Add("Lot", Type.GetType("System.String"))
            dt2.Columns.Add("Sous lot", Type.GetType("System.String"))
            dt2.Columns.Add("Monnaie", Type.GetType("System.String"))
            dt2.Columns.Add("Montant soumission", Type.GetType("System.String"))
            dt2.Columns.Add("Garantie de l'offre", Type.GetType("System.String"))
            dt2.Columns.Add("Attestation de régularité fiscale", Type.GetType("System.String"))
            dt2.Columns.Add("Attestation sociale", Type.GetType("System.String"))
            dt2.Columns.Add("Délai de livraison", Type.GetType("System.String"))
            dt2.Columns.Add("RefSoumis", Type.GetType("System.String"))
            dt2.Rows.Clear()

            query = "select CodeLot,Monnaie,HtHdTtc,MontantPropose,AttRegFiscale,BanqueCaution,CautionBancaire,AttCNPS,DelaiLivraison,RefSoumis,CodeSousLot from T_SoumissionFournisseur where CodeFournis='" & TxtCodeSoumis.Text & "' order by CodeLot"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                Dim drS = dt2.NewRow()

                drS(0) = "N°" & rw("CodeLot").ToString
                drS(1) = rw("CodeSousLot").ToString
                drS(2) = rw("Monnaie").ToString
                drS(3) = AfficherMonnaie(rw("MontantPropose").ToString) & " " & rw("HtHdTtc").ToString
                drS(4) = AfficherMonnaie(rw("CautionBancaire").ToString) & " (" & MettreApost(rw("BanqueCaution").ToString) & ")"
                drS(5) = rw("AttRegFiscale").ToString
                drS(6) = MettreApost(rw("AttCNPS").ToString)
                drS(7) = rw("DelaiLivraison").ToString
                drS(8) = rw("RefSoumis").ToString
                dt2.Rows.Add(drS)
            Next

            GridRecapOffre.DataSource = dt2
            GridView2.Columns.Item(8).Visible = False
            GridView2.OptionsView.ColumnAutoWidth = True
            GridView2.OptionsBehavior.AutoExpandAllGroups = True
            GridView2.VertScrollVisibility = True
            GridView2.HorzScrollVisibility = True
            GridView2.BestFitColumns()
            GridView2.Columns("Montant soumission").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            GridView2.Columns("Garantie de l'offre").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            GridView2.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridView2.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridView2.Appearance.Row.Font = New Font("Times New Roman", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub GridMontantDesSousLot()
        Dim dt2 As New DataTable
        dt2.Columns.Clear()
        dt2.Columns.Add("Code Sous lot", Type.GetType("System.String"))
        dt2.Columns.Add("Montant soumission", Type.GetType("System.String"))
        dt2.Columns.Add("RefSoumis", Type.GetType("System.String"))
        dt2.Rows.Clear()
        GridMontantDesSL.DataSource = dt2
        ViewMontantDesSL.Columns("RefSoumis").Visible = False
        ViewMontantDesSL.OptionsView.ColumnAutoWidth = True
        'GridView2.OptionsBehavior.AutoExpandAllGroups = True
        ViewMontantDesSL.VertScrollVisibility = True
        ViewMontantDesSL.HorzScrollVisibility = True
        ViewMontantDesSL.BestFitColumns()
        ViewMontantDesSL.Columns("Montant soumission").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewMontantDesSL.Appearance.Row.Font = New Font("Times New Roman", 9, FontStyle.Regular)
    End Sub

    Private Sub bntAjoutSousLot_Click(sender As Object, e As EventArgs) Handles btnAjoutSousLot.Click
        If CmbNumDAO.SelectedIndex = -1 Then
            FailMsg("Veuillez sélectionner un dossier.")
            CmbNumDAO.Select()
            Exit Sub
        End If
        If CmbNomSoumis.SelectedIndex = -1 Then
            FailMsg("Veuillez sélectionner un soumissoinnaire.")
            CmbNomSoumis.Select()
            Exit Sub
        End If
        If CmbNumLot.SelectedIndex = -1 Then
            FailMsg("Veuillez sélectionner un lot.")
            CmbNumLot.Select()
            Exit Sub
        End If

        AjoutMontantSousLot.ShowDialog()
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If OuvertureTermine = True Or ListeRecap.Nodes.Count = 0 Then
            e.Cancel = True
        End If
    End Sub
End Class