Imports MySql.Data.MySqlClient
Imports System.IO
Imports ClearProject.PassationMarche
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class OuvertureOffresOld
    Dim dt2 = New DataTable()
    Dim HeureDemarrage As String = ""
    Dim DureeSeance As String = ""
    Dim OuvertureTermine As Boolean = False

    Private Sub OuvertureOffres_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        BtModOffre.Enabled = False
        RemplirCmbNumDAO()
    End Sub

    Private Sub RemplirCmbNumDAO()
        query = "select NumeroDAO from T_DAO where CodeProjet='" & ProjetEnCours & "' order by NumeroDAO"
        CmbNumDAO.Properties.Items.Clear()
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbNumDAO.Properties.Items.Add(rw(0).ToString)
        Next
    End Sub

    Private Sub CmbNumDAO_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumDAO.SelectedValueChanged

        TxtMethode.Text = ""
        TxtTypeMarche.Text = ""
        TxtLibelleDAO.Text = ""
        TxtDateCloture.Text = ""
        TxtDateOuverture.Text = ""
        TxtNbLot.Text = ""
        InitInfosSoumis()
        InitSaisieInfos()
        ViderGridRecap()
        GbOffres.Enabled = False

        If (CmbNumDAO.Text <> "") Then
            Dim DateOuv As String = "01/01/2999"
            query = "select IntituleDAO,MethodePDM,TypeMarche,DateLimiteRemise,DateDebutOuverture,NbreLotDAO,DateFinOuverture,DureeSeance from T_DAO where NumeroDAO='" & CmbNumDAO.Text & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtMethode.Text = rw(1).ToString
                TxtTypeMarche.Text = rw(2).ToString
                TxtLibelleDAO.Text = MettreApost(rw(0).ToString)
                If (rw(3).ToString <> "") Then
                    TxtDateCloture.Text = rw(3).ToString.Replace(" ", "   à   ")
                    DateOuv = rw(3).ToString
                Else
                    TxtDateCloture.Text = "Non définie"
                End If
                If (rw(4).ToString <> "") Then
                    TxtDateOuverture.Text = rw(4).ToString.Replace(" ", "   à   ")
                Else
                    TxtDateOuverture.Text = "Non définie"
                End If
                TxtNbLot.Text = rw(5).ToString

                CmbNumLot.Properties.Items.Clear()
                For k As Integer = 1 To CInt(rw(5))
                    CmbNumLot.Properties.Items.Add(k.ToString)
                Next

                If (rw(6).ToString <> "") Then
                    TxtCodePresence.Enabled = False
                    GbSoumissionnaire.Enabled = True
                    GbOffres.Enabled = True
                    BtOuvertureOffre.Text = "Etat PV" & vbNewLine & "d'ouverture"
                    BtOuvertureOffre.Enabled = True
                    BtDureeSeance.Enabled = True
                    OuvertureTermine = True
                    BtDureeSeance.Text = rw(7).ToString.Replace(":", "   :   ")
                    RemplirCojo()
                Else
                    BtDureeSeance.Text = "00   :   00   :   00"
                    BtDureeSeance.Enabled = False
                    BtOuvertureOffre.Text = "Démarrer" & vbNewLine & "Ouverture"
                    BtOuvertureOffre.Enabled = False
                    OuvertureTermine = False
                    GbSoumissionnaire.Enabled = False
                    If (rw(3).ToString <> "" And rw(4).ToString <> "") Then
                        TxtCodePresence.Enabled = True
                        TxtCodePresence.Focus()
                    Else
                        TxtCodePresence.Enabled = False
                        BtOuvertureOffre.Text = "Dates à" & vbNewLine & "définir"
                    End If

                End If
            Next


            ' Offres déposés et soumissionnaires ayant déposés ******
            Dim NbDepot As Decimal = 0
            CmbNomSoumis.Properties.Items.Clear()
            query = "select NomFournis from T_Fournisseur where NumeroDAO='" & CmbNumDAO.Text & "' and CodeProjet='" & ProjetEnCours & "' and DateDepotDAO<>''"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                NbDepot = NbDepot + 1
                CmbNomSoumis.Properties.Items.Add(MettreApost(rw(0).ToString))
            Next

            Dim NbRetrait As Decimal = 0
            query = "select Count(*) from T_Fournisseur where NumeroDAO='" & CmbNumDAO.Text & "' and CodeProjet='" & ProjetEnCours & "'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                NbRetrait = CInt(rw(0))
            Next


            LblRecapOffres.Text = "Dossiers retirés : " & NbRetrait.ToString & "                              Offres reçues : " & NbDepot.ToString

            If (TxtNbLot.Text = "0" Or DateTime.Compare(CDate(DateOuv), Now) > 0) Then
                GbSoumissionnaire.Enabled = False
                TxtCodePresence.Enabled = False
                BtOuvertureOffre.Enabled = False
            End If
            If OuvertureTermine = True Then
                GbSaisieInfos.Enabled = False
            End If
            RemplirCojo()
        End If
    End Sub

    Private Sub RemplirCojo()
        If (CmbNumDAO.Text <> "") Then

            dt.Columns.Clear()
            dt.Columns.Add("Commission", Type.GetType("System.String"))


            'Dim Reader As MySqlDataReader
            dt.Rows.Clear()
            query = "select NomMem,TitreMem from T_Commission where NumeroDAO='" & CmbNumDAO.Text & "' and Pointage<>''"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                Dim drS = dt.NewRow()

                drS(0) = MettreApost(rw(0).ToString) & " (" & rw(1).ToString & ")"

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

    Private Sub BtOuvertureOffre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtOuvertureOffre.Click
        Dim Deb As String = Mid(BtOuvertureOffre.Text, 1, 3)
        If (Deb = "Dém") Then

            GbSoumissionnaire.Enabled = True
            GbOffres.Enabled = True
            HeureDemarrage = Now.ToLongTimeString
            DureeSeance = 0
            BtDureeSeance.Enabled = True
            BtOuvertureOffre.Text = "Fin de" & vbNewLine & "Séance"
            query = "Update T_DAO set DateDebutOuverture='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' where NumeroDAO='" & CmbNumDAO.Text & "' and CodeProjet='" & ProjetEnCours & "'"
            ExecuteNonQuery(query)
            Timer1.Interval = 1000
            Timer1.Start()
            CmbNumDAO.Enabled = False

        ElseIf (Deb = "Fin" Or Deb = "Eta") Then

            'verif des soumissions de tous les fournisseurs
            Dim LesFournis(500) As String
            Dim nbFournis As Decimal = 0
            query = "select CodeFournis from T_Fournisseur where NumeroDAO='" & CmbNumDAO.Text & "' and CodeProjet='" & ProjetEnCours & "' and DateDepotDAO<>''"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                LesFournis(nbFournis) = rw(0).ToString
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
                GbSaisieInfos.Enabled = False
                GbOffres.Enabled = False
                CmbNumDAO.Enabled = True

                'enregistrement de la fin de séance dans la BD
                Dim FinSeance As String = Now.ToLongTimeString

                query = "Update T_DAO set DateFinOuverture='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', DureeSeance='" & BtDureeSeance.Text.Replace(" ", "") & "', DateModif='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', Operateur='" & CodeUtilisateur & "' where NumeroDAO='" & CmbNumDAO.Text & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

            End If

            DebutChargement()
            Dim reportPVO As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim Chemin As String = lineEtat & "\DAO\"
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

            reportPVO.SetParameterValue("NumDAO", CmbNumDAO.Text, "CojoPvOuvertureDAO.rpt")
            reportPVO.SetParameterValue("NumDAO2", CmbNumDAO.Text, "DossiersRetiresDAO.rpt")
            reportPVO.SetParameterValue("NumDAO3", CmbNumDAO.Text, "OffresDeposesDAO.rpt")
            reportPVO.SetParameterValue("NumDAO4", CmbNumDAO.Text, "OffresLuesDAO.rpt")
            reportPVO.SetParameterValue("NumDAO5", CmbNumDAO.Text, "CojoPvSignatureDAO.rpt")
            reportPVO.SetParameterValue("NumDao", CmbNumDAO.Text, "PvOuverturePageGarde.rpt")
            reportPVO.SetParameterValue("CodeProjet", ProjetEnCours)
            reportPVO.SetParameterValue("CodeProjet", ProjetEnCours, "PvOuverturePageGarde.rpt")
            reportPVO.SetParameterValue("LibelleMarche", TxtLibelleDAO.Text)

            query = "select MinistereTutelle,NomProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                reportPVO.SetParameterValue("Ministere", MettreApost(rw(0).ToString), "PvOuverturePageGarde.rpt")
                reportPVO.SetParameterValue("NomProjet", MettreApost(rw(1).ToString), "PvOuverturePageGarde.rpt")
                reportPVO.SetParameterValue("NomProjet", MettreApost(rw(1).ToString))
            Next

            query = "select MethodePDM,NbreLotDAO,DateLimiteRemise, DateDebutOuverture, DateFinOuverture from T_DAO where NumeroDAO='" & CmbNumDAO.Text & "' and CodeProjet='" & ProjetEnCours & "'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                reportPVO.SetParameterValue("AnneeEnLettre", MontantLettre(CDate(rw(3)).ToString("yyyy")))
                reportPVO.SetParameterValue("DateEnLettre", CDate(rw(3)).ToLongDateString)
                reportPVO.SetParameterValue("Annee", CDate(rw(3)).ToString("yyyy"))
                reportPVO.SetParameterValue("DebutSeance", Mid(CDate(rw(3)).ToLongTimeString, 1, 5).Replace(":", " heures ") & " mn")

                reportPVO.SetParameterValue("FinSeance", Mid(CDate(rw(4)).ToLongTimeString, 1, 5).Replace(":", " heures ") & " mn")
                reportPVO.SetParameterValue("DateEdition", CDate(rw(4)).ToString("MMMM").ToUpper & "  " & CDate(rw(4)).ToString("yyyy"), "PvOuverturePageGarde.rpt")
                reportPVO.SetParameterValue("DateFormatLong", CDate(rw(4)).ToLongDateString)
                reportPVO.SetParameterValue("DateFormatLong", CDate(rw(4)).ToLongDateString, "PvOuverturePageGarde.rpt")

                If (rw(0).ToString <> "") Then
                    reportPVO.SetParameterValue("MethodePdm", MettreApost(rw(0).ToString), "PvOuverturePageGarde.rpt")
                Else
                    reportPVO.SetParameterValue("MethodePdm", "AO", "PvOuverturePageGarde.rpt")
                End If

                reportPVO.SetParameterValue("NbLots", rw(1).ToString & IIf(CInt(rw(1)) > 1, "LOTS", "LOT"), "PvOuverturePageGarde.rpt")

                Dim DatCoup() As String = rw(2).ToString.Split(" "c)
                reportPVO.SetParameterValue("DateDepot", CDate(DatCoup(0)).ToLongDateString)
            Next

            'Données du marché *********************
            Dim CodeMarche As Decimal = 0
            Dim LeBaill As String = ""
            Dim LibMarc As String = ""
            query = "select RefMarche,DescriptionMarche,InitialeBailleur from T_Marche where NumeroDAO='" & CmbNumDAO.Text & "' and CodeProjet='" & ProjetEnCours & "'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                CodeMarche = rw(0)
                LeBaill = rw(2)
                If (LibMarc <> "") Then
                    LibMarc = LibMarc & vbNewLine & " et " & vbNewLine
                End If
                LibMarc = LibMarc & rw(1).ToString
            Next

            reportPVO.SetParameterValue("LibelleMarche", MettreApost(LibMarc))
            reportPVO.SetParameterValue("LibelleMarche", MettreApost(LibMarc), "PvOuverturePageGarde.rpt")

            ' La convention ****************************
            query = "select C.CodeConvention,C.TypeConvention from T_Convention as C, T_Bailleur as B where B.CodeBailleur=C.CodeBailleur and B.InitialeBailleur='" & LeBaill & "' and B.CodeProjet='" & ProjetEnCours & "'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                reportPVO.SetParameterValue("TypeConv", rw(1).ToString.ToUpper, "PvOuverturePageGarde.rpt")
                reportPVO.SetParameterValue("NumConv", rw(0).ToString, "PvOuverturePageGarde.rpt")
                reportPVO.SetParameterValue("Bailleur", LeBaill, "PvOuverturePageGarde.rpt")
            Next

            'Données de l'activité (Compo Souscompo) **************
            Dim CodActiv1 As String = ""
            query = "select P.LibelleCourt from T_BesoinPartition as B, T_BesoinMarche as BM,T_Partition as P where B.CodePartition=P.CodePartition and BM.RefBesoinPartition=B.RefBesoinPartition and B.CodeProjet='" & ProjetEnCours & "' and BM.RefMarche='" & CodeMarche & "'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                CodActiv1 = rw(0).ToString
            Next
            '       Composante   *****
            Dim CodComp As String = Mid(CodActiv1, 1, 1)
            reportPVO.SetParameterValue("CodeCompo", CodComp, "PvOuverturePageGarde.rpt")
            reportPVO.SetParameterValue("CodeCompo", CodComp)
            query = "select LibellePartition from T_Partition where LibelleCourt='" & CodComp & "' and CodeProjet='" & ProjetEnCours & "'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                reportPVO.SetParameterValue("LibCompo", MettreApost(rw(0).ToString).ToUpper, "PvOuverturePageGarde.rpt")
                reportPVO.SetParameterValue("LibelleCompo", MettreApost(rw(0).ToString))
            Next

            '       Sous Composante   *****
            Dim CodSouComp As String = Mid(CodActiv1, 1, 2)
            reportPVO.SetParameterValue("CodeSouCompo", CodSouComp, "PvOuverturePageGarde.rpt")
            reportPVO.SetParameterValue("CodeSouCompo", CodSouComp)
            query = "select LibellePartition from T_Partition where LibelleCourt='" & CodSouComp & "' and CodeProjet='" & ProjetEnCours & "'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                reportPVO.SetParameterValue("LibSouCompo", MettreApost(rw(0).ToString).ToUpper, "PvOuverturePageGarde.rpt")
                reportPVO.SetParameterValue("LibelleSouCompo", MettreApost(rw(0).ToString))
            Next

            Dim NbDaoRetires As Decimal = 0
            query = "select Count(*) from T_Fournisseur where NumeroDAO='" & CmbNumDAO.Text & "' and CodeProjet='" & ProjetEnCours & "'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                NbDaoRetires = CInt(rw(0))
            Next

            reportPVO.SetParameterValue("NbDossierRetires", NbDaoRetires.ToString)
            reportPVO.SetParameterValue("NbDossierRetiresLettre", MontantLettre(NbDaoRetires.ToString))

            Dim NbOffresRecues As Decimal = 0
            query = "select Count(*) from T_Fournisseur where DateDepotDAO<>'' and NumeroDAO='" & CmbNumDAO.Text & "' and CodeProjet='" & ProjetEnCours & "'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                NbOffresRecues = CInt(rw(0))
            Next

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
                Dim DossierPV As String = line & "\PvOuverture"
                If (Directory.Exists(DossierPV) = False) Then
                    Directory.CreateDirectory(DossierPV)
                End If

                DossierPV = DossierPV & "\" & ProjetEnCours
                If (Directory.Exists(DossierPV) = False) Then
                    Directory.CreateDirectory(DossierPV)
                End If

                DossierPV = DossierPV & "\" & CmbNumDAO.Text.Replace("/", "_").Replace("-", "_")
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
                TxtCodePresence.Enabled = False
            End If

        End If
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
        If (CmbNomSoumis.Text <> "" And CmbNumDAO.Text <> "") Then
            ' Recup des infos ***
            Dim leCode As String = ""
            query = "select PaysFournis,AdresseCompleteFournis,TelFournis,FaxFournis,CelFournis,MailFournis,CodeFournis from T_Fournisseur where NumeroDAO='" & CmbNumDAO.Text & "' and CodeProjet='" & ProjetEnCours & "' and NomFournis='" & EnleverApost(CmbNomSoumis.Text) & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows

                leCode = rw(6).ToString
                TxtPaysSoumis.Text = MettreApost(rw(0).ToString)
                TxtAdresseSoumis.Text = MettreApost(rw(1).ToString)
                TxtTelSoumis.Text = rw(2).ToString
                TxtFaxSoumis.Text = rw(3).ToString
                TxtCelSoumis.Text = rw(4).ToString
                TxtMailSoumis.Text = rw(5).ToString.ToLower
                TxtCodeSoumis.Text = rw(6).ToString
                If OuvertureTermine = False Then
                    GbSaisieInfos.Enabled = True
                End If
            Next

            If (leCode <> "") Then

                Dim lesCrit(100) As String
                Dim nbCr As Decimal = 0
                query = "select RefCritere from T_DAO_PostQualif where NumeroDAO='" & CmbNumDAO.Text & "' and RefCritereMere<>'0'"
                dt0 = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    lesCrit(nbCr) = rw(0).ToString
                    nbCr += 1
                Next

                query = "DELETE from T_SoumisFournisPostQualif where CodeFournis='" & leCode & "'"
                ExecuteNonQuery(query)


                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                For k As Integer = 0 To nbCr - 1
                    Dim DatSet = New DataSet

                    query = "select * from T_SoumisFournisPostQualif"
                    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                    Dim DatAdapt = New MySqlDataAdapter(Cmd)
                    DatAdapt.Fill(DatSet, "T_SoumisFournisPostQualif")
                    Dim DatTable = DatSet.Tables("T_SoumisFournisPostQualif")
                    Dim DatRow = DatSet.Tables("T_SoumisFournisPostQualif").NewRow()

                    DatRow("CodeFournis") = leCode
                    DatRow("RefCritere") = lesCrit(k)

                    DatSet.Tables("T_SoumisFournisPostQualif").Rows.Add(DatRow)
                    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                    DatAdapt.Update(DatSet, "T_SoumisFournisPostQualif")
                    DatSet.Clear()
                Next
                BDQUIT(sqlconn)

            End If
            If OuvertureTermine = True Then
                GbSaisieInfos.Enabled = False
            End If
            ItemDevise()
            MajCmbCompte()
            MajGridRecap()
            InitSaisieInfos()

        End If

    End Sub

    Private Sub ItemDevise()
        CmbDevise.Properties.Items.Clear()
        query = "select AbregeDevise from T_Devise"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbDevise.Properties.Items.Add(rw(0).ToString)
        Next
    End Sub

    Private Sub CmbNumLot_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumLot.SelectedValueChanged
        query = "select LibelleLot,RefLot from T_LotDAO where NumeroDAO='" & CmbNumDAO.Text & "' and CodeLot='" & CmbNumLot.Text & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            TxtLibelleLot.Text = MettreApost(rw(0).ToString)
            TxtRefLot.Text = rw(1).ToString
        Next
        If CmbNumLot.Text <> "" Then
            Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDAO.Text)
            Dim nbsouslot As Integer = Val(Resultat(0))
            If nbsouslot > 0 Then
                cmbSousLot.Enabled = True
                cmbSousLot.Properties.Items.Clear()
                cmbSousLot.Text = ""
                txtLibelleSousLot.Text = ""
                Dim dt As DataTable = CType(Resultat(1), DataTable)
                For Each rw As DataRow In dt.Rows
                    cmbSousLot.Properties.Items.Add(rw("CodeSousLot").ToString)
                Next
            Else
                cmbSousLot.Text = ""
                txtLibelleSousLot.Text = ""
                cmbSousLot.Enabled = False
            End If
        End If
    End Sub

    Private Sub CmbDevise_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbDevise.SelectedValueChanged
        query = "select LibelleDevise,AbregeDevise from T_Devise where AbregeDevise='" & CmbDevise.Text & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            TxtDevise.Text = rw(0).ToString
            TxtDeviseGarantie.Text = rw(1).ToString
        Next
    End Sub

    Private Sub MajCmbCompte()
        CmbEtsBancaire.Properties.Items.Clear()
        query = "select CodeBanque from T_Banque order by CodeBanque"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbEtsBancaire.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next
    End Sub

    Private Sub BtEnrgOffre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgOffre.Click
        Dim erreur As String = ""
        'si le lot n'est pas renseigné
        If CmbNumLot.SelectedIndex = -1 Then
            erreur += "- Lot" + ControlChars.CrLf
        End If
        'si le sous lot n'est pas renseigné
        If cmbSousLot.Enabled = True And cmbSousLot.SelectedIndex = -1 Then
            erreur += "- Sous lot" + ControlChars.CrLf
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
        If CmbTaxes.SelectedIndex = -1 Then
            erreur += "- Taxes" + ControlChars.CrLf
        End If
        'si garantie offre n'est pas renseigné
        'If TxtGarantieOffre.Text = "" Then
        '    erreur += "- Garantie offre" + ControlChars.CrLf
        'End If
        'si  le livraison n'est pas renseigné
        If NumDelaiLivraison.Value < 0 Then
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
        If erreur = "" Then
            ' Vérif de l'existance d'un lot
            query = "select * from T_SoumissionFournisseur where CodeLot='" & CmbNumLot.Text & "' and CodeFournis='" & TxtCodeSoumis.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt0.Rows
                If rw("CodeSousLot").ToString <> "" Then
                    query = "select * from T_SoumissionFournisseur where CodeLot='" & CmbNumLot.Text & "' and CodeFournis='" & TxtCodeSoumis.Text & "' And CodeSousLot='" & cmbSousLot.Text & "'"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    If dt1.Rows.Count > 0 Then
                        SuccesMsg("Cette  offre existe déjà!")
                        Exit Sub
                    End If
                Else
                    If dt0.Rows.Count > 0 Then
                        SuccesMsg("Cette  offre existe déjà!")
                        Exit Sub
                    End If
                End If
            Next


            ' Enregistrement du fournisseur
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
            DatRow("CodeSousLot") = cmbSousLot.Text
            DatRow("Monnaie") = CmbDevise.Text
            DatRow("HtHdTtc") = CmbTaxes.Text
            DatRow("MontantPropose") = CDbl(TxtMontantOffre.Text.ToString.Replace(" ", ""))
            DatRow("AttRegFiscale") = DateRegFiscale.DateTime.ToShortDateString
            DatRow("AttCNPS") = DateAttestationsqlconnPS.DateTime.ToShortDateString
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
            SuccesMsg("L'offre a été enregistrée avec succés")
            MajGridRecap()
            InitSaisieInfos()
        Else
            SuccesMsg("Veuillez remplir ces champs : " + ControlChars.CrLf + erreur)
        End If
    End Sub
    Private Sub ViderGridRecap()
        dt2.Columns.Clear()
        dt2.Rows.Clear()
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
            dt2.Columns.Add("Attestation CNPS", Type.GetType("System.String"))
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
                drS(5) = CDate(rw("AttRegFiscale").ToString).ToShortDateString
                drS(6) = CDate(MettreApost(rw("AttCNPS").ToString)).ToShortDateString
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
            'GridView2.Columns.Item(0).Width = 40
            'GridView2.Columns.Item(1).Width = 80
            'GridView2.Columns.Item(2).Width = 150
            'GridView2.Columns.Item(3).Width = 250
            'GridView2.Columns.Item(4).Width = 150
            'GridView2.Columns.Item(5).Width = 200
            'GridView2.Columns.Item(6).Width = 150
        End If

    End Sub

    Private Sub InitSaisieInfos()
        cmbSousLot.Text = ""
        cmbSousLot.Properties.Items.Clear()
        txtLibelleSousLot.Text = ""
        CmbNumLot.Text = ""
        TxtLibelleLot.Text = ""
        CmbDevise.Text = ""
        TxtDevise.Text = ""
        TxtMontantOffre.Text = ""
        CmbTaxes.Text = ""
        TxtGarantieOffre.Text = ""
        TxtDeviseGarantie.Text = ""
        NumDelaiLivraison.Value = 0
        CmbDelaiLivraison.Text = ""
        CmbEtsBancaire.Text = ""
        DateRegFiscale.DateTime = "01/01/" & Now.Year
        DateAttestationsqlconnPS.DateTime = "01/01/" & Now.Year
        NumValidOffre.Value = 0
        CmbValidOffre.Text = ""
        TxtObserv.Text = ""
    End Sub

    Private Sub OuvertureOffres_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub CmbNumLot_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbNumLot.SelectedIndexChanged
        DateRegFiscale.DateTime = Now.ToShortDateString
        DateAttestationsqlconnPS.DateTime = Now.ToShortDateString
    End Sub

    Private Sub TxtCodePresence_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtCodePresence.KeyDown
        If (e.KeyCode = Keys.Enter And CmbNumDAO.Text <> "") Then
            If (TxtCodePresence.Text <> "") Then
                Dim CodMembre As String = ""
                query = "select CodeMem,Pointage from T_Commission where NumeroDAO='" & CmbNumDAO.Text & "' and PasseMem='" & TxtCodePresence.Text & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    If (rw(1).ToString <> "") Then
                        SuccesMsg("Code déjà entré!")
                        TxtCodePresence.Text = ""
                        Exit Sub
                    End If
                    CodMembre = rw(0).ToString
                Next

                If (CodMembre <> "") Then
                    query = "Update T_Commission set Pointage='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' where CodeMem='" & CodMembre & "'"
                    ExecuteNonQuery(query)
                    TxtCodePresence.Text = ""
                    RemplirCojo()
                Else
                    SuccesMsg("Accès réfusé!")
                End If

            End If

        End If
    End Sub

    Private Sub ModifierLoffreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModifierLoffreToolStripMenuItem.Click
        If GridView2.RowCount > 0 Then
            drx1 = GridView2.GetDataRow(GridView2.FocusedRowHandle)
            txtRefSoumis.Text = drx1("RefSoumis").ToString
            query = "select * from t_soumissionfournisseur where RefSoumis='" & txtRefSoumis.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                Dim DelaiLiv = rw("DelaiLivraison").ToString.Split(" ")
                Dim ValideOffre = rw("ValiditeOffre").ToString.Split(" ")
                CmbNumLot.Text = rw("CodeLot").ToString
                cmbSousLot.Text = rw("CodeSousLot").ToString
                CmbDevise.Text = rw("Monnaie").ToString
                TxtMontantOffre.Text = rw("MontantPropose").ToString
                CmbTaxes.Text = rw("HtHdTtc").ToString
                TxtGarantieOffre.Text = rw("CautionBancaire").ToString
                CmbEtsBancaire.Text = MettreApost(rw("BanqueCaution").ToString)
                NumDelaiLivraison.Text = DelaiLiv(0).ToString
                CmbDelaiLivraison.Text = DelaiLiv(1).ToString
                DateRegFiscale.Text = CDate(rw("AttRegFiscale").ToString).ToShortDateString
                DateAttestationsqlconnPS.Text = CDate(rw("AttCNPS").ToString).ToShortDateString
                NumValidOffre.Text = ValideOffre(0).ToString
                CmbValidOffre.Text = MettreApost(ValideOffre(1).ToString)
                TxtObserv.Text = MettreApost(rw("Observations").ToString)
            Next
            BtModOffre.Enabled = True
            BtEnrgOffre.Enabled = False
        End If
    End Sub

    Private Sub BtModOffre_Click(sender As Object, e As EventArgs) Handles BtModOffre.Click
        Dim erreur As String = ""
        'si le lot n'est pas renseigné
        If CmbNumLot.SelectedIndex = -1 Then
            erreur += "- Lot" + ControlChars.CrLf
        End If
        'si le sous lot n'est pas renseigné
        If cmbSousLot.Enabled = True And cmbSousLot.SelectedIndex = -1 Then
            erreur += "- Sous lot" + ControlChars.CrLf
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
        If CmbTaxes.SelectedIndex = -1 Then
            erreur += "- Taxes" + ControlChars.CrLf
        End If
        'si garantie offre n'est pas renseigné
        'If TxtGarantieOffre.EditValue< Then
        '    erreur += "- Garantie offre" + ControlChars.CrLf
        'End If
        'si  le livraison n'est pas renseigné
        If NumDelaiLivraison.Value < 0 Then
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
        If erreur = "" Then
            If ConfirmMsg("Voulez-vous modifier cette offre ?") = DialogResult.Yes Then
                ' Modification de l'offre
                query = "UPDATE T_SoumissionFournisseur SET CodeLot='" & EnleverApost(CmbNumLot.Text) & "',Monnaie='" & EnleverApost(CmbDevise.Text) & "',MontantPropose='" & CDbl(TxtMontantOffre.Text) & "',HtHdTtc='" & EnleverApost(CmbTaxes.Text) & "', CautionBancaire='" & CDbl(EnleverApost(TxtGarantieOffre.Text)) & "'"
                query &= ",BanqueCaution='" & EnleverApost(CmbEtsBancaire.Text) & "',DelaiLivraison='" & NumDelaiLivraison.Value.ToString & " " & EnleverApost(CmbDelaiLivraison.Text) & "',AttRegFiscale='" & dateconvert(DateRegFiscale.Text) & "',AttCNPS='" & dateconvert(DateAttestationsqlconnPS.Text) & "'"
                query &= ",CodeSousLot='" & EnleverApost(cmbSousLot.Text) & "', ValiditeOffre='" & NumValidOffre.Value.ToString & " " & EnleverApost(CmbValidOffre.Text) & "',Observations='" & EnleverApost(TxtObserv.Text) & "',DateModif='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' WHERE RefSoumis='" & txtRefSoumis.Text & "'"
                ExecuteNonQuery(query)
                SuccesMsg("L'offre a été modifiée avec succés")
                BtEnrgOffre.Enabled = True
                BtModOffre.Enabled = False
                MajGridRecap()
                InitSaisieInfos()
            End If
        Else
            SuccesMsg("Veuillez remplir ces champs : " + ControlChars.CrLf + erreur)
        End If
    End Sub
    Private Sub BtAnulOffre_Click(sender As Object, e As EventArgs) Handles BtAnulOffre.Click
        BtEnrgOffre.Enabled = True
        BtModOffre.Enabled = False
        InitSaisieInfos()

    End Sub

    Private Sub cmbSousLot_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbSousLot.SelectedValueChanged
        query = "select LibelleSousLot,CodeSousLot from t_lotdao_souslot where NumeroDAO='" & CmbNumDAO.Text & "' and RefLot='" & TxtRefLot.Text & "' And CodeSousLot='" & cmbSousLot.Text & "'"
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        txtLibelleSousLot.Text = ""
        For Each rw As DataRow In dt1.Rows
            txtLibelleSousLot.Text = MettreApost(rw("LibelleSousLot").ToString)
        Next
    End Sub

    Private Sub GridRecapOffre_MouseUp(sender As Object, e As MouseEventArgs) Handles GridRecapOffre.MouseUp
        'If OuvertureTermine +
    End Sub
End Class