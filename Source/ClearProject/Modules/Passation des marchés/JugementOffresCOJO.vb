Imports MySql.Data.MySqlClient
Imports ClearProject.PassationMarche
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO


Public Class JugementOffresCOJO

    Dim dt = New DataTable()
    Dim dtExam = New DataTable()
    Dim dtTraite = New DataTable()
    Public EtapeActuelle As String = ""
    Dim CodeActuel As String = ""
    Dim SpecDemande As String = ""
    Public ValeurActuelle As String = ""
    Dim Accord As String = ""
    Dim DrX As DataRow

    Public CodeCritere(100) As String
    Public TableCritere(100) As String
    Public CritereElimine(100) As String
    Public GroupeCritere(100) As String
    Public NombreCritere As Decimal = 0

    Private Sub JugementOffres_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ChargerDossier()
        TxtTypeExamen.Text = ""


        ' Me.RepCouverture.RefreshReport()
    End Sub

    Private Sub ChargerDossier()
        query = "select NumeroDAO from T_DAO where DateFinOuverture<>'' and CodeProjet='" & ProjetEnCours & "' order by NumeroDAO"
        CmbNumDoss.Properties.Items.Clear()
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbNumDoss.Properties.Items.Add(rw(0).ToString)
        Next
    End Sub

    Private Sub JugementOffres_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub CmbNumDoss_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbNumDoss.SelectedIndexChanged

        PnlTermine.Visible = False
        'Initialisation
        EtapeActuelle = ""
        EtapeExamPrelim.ImageIndex = 1
        EtapeExamDetail.ImageIndex = 1
        EtapeExamPost.ImageIndex = 1
        EtapeAnalyse.ImageIndex = 1
        dtExam.Columns.Clear()
        dtExam.Rows.Clear()
        GridTravail.Refresh()
        BtExecuter.Enabled = False
        BtExecuter.Text = "DEBUT"
        PanelLots.Enabled = False
        CmbNumLot.Text = ""
        cmbSousLot.Text = ""
        TxtLibelleSousLot.Text = ""
        TxtLibelleLot.Text = ""
        CmbSoumis.Text = ""
        TxtAdresseSoumis.Text = ""
        PanelVerdict.Enabled = False
        TxtTypeExamen.Text = "VERIFICATION DES OFFRES"
        PanelVerdict.Visible = True
        CmbLotMarche.Text = ""
        PnlEditionMarche.Visible = False

        GbCojo.Enabled = False

        query = "select IntituleDAO,MethodePDM,TypeMarche,DateFinOuverture,NbreLotDAO,AnalyseOffres,ExamPrelimOffres,ExamDetailOffres,ExamPostQualifOffres from T_DAO where NumeroDAO='" & CmbNumDoss.Text & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            TxtNbLot.Text = rw(4).ToString
            TxtLibelleDoss.Text = MettreApost(rw(0).ToString)
            TxtDateOuvert.Text = Mid(rw(3).ToString, 1, 10)
            TxtMethode.Text = rw(1).ToString
            TxtTypeMarche.Text = rw(2).ToString
            GbCojo.Enabled = True

            CmbNumLot.Properties.Items.Clear()
            For i As Integer = 1 To CInt(rw(4).ToString)
                CmbNumLot.Properties.Items.Add(i.ToString)
                CmbNumLotAttrib.Properties.Items.Add(i.ToString)
            Next

            EtapeActuelle = ""
            If (rw(5).ToString <> "") Then
                EtapeAnalyse.ImageIndex = 0
                EtapeActuelle = "Analyse"
                EtapeAnalyse.ForeColor = Color.Black
                TxtTypeExamen.Text = "EXAMEN PRELIMINAIRE"
                PanelVerdict.Visible = False

                If (rw(6).ToString <> "") Then
                    EtapeExamPrelim.ImageIndex = 0
                    EtapeActuelle = "ExamPrelim"
                    EtapeExamPrelim.ForeColor = Color.Black
                    TxtTypeExamen.Text = "EXAMEN DETAILLE"
                    PanelVerdict.Visible = False

                    If (rw(7).ToString <> "") Then
                        EtapeExamDetail.ImageIndex = 0
                        EtapeActuelle = "ExamDetail"
                        EtapeExamDetail.ForeColor = Color.Black
                        TxtTypeExamen.Text = "EXAMEN POST QUALIFICATION"
                        PanelVerdict.Visible = False

                        If (rw(8).ToString <> "") Then
                            EtapeExamPost.ImageIndex = 0
                            EtapeActuelle = "ExamPost"
                            EtapeExamPost.ForeColor = Color.Black
                            TxtTypeExamen.Text = "BILAN DU JUGEMENT DES OFFRES DU DAO N°" & CmbNumDoss.Text
                            PanelVerdict.Visible = False
                        Else
                            EtapeExamPost.ImageIndex = 2
                            EtapeExamPost.ForeColor = Color.Black
                        End If
                    Else
                        EtapeExamDetail.ImageIndex = 2
                        EtapeExamDetail.ForeColor = Color.Black
                    End If
                Else
                    EtapeExamPrelim.ImageIndex = 2
                    EtapeExamPrelim.ForeColor = Color.Black
                End If
            Else
                EtapeAnalyse.ImageIndex = 2
                EtapeAnalyse.ForeColor = Color.Black
            End If
        Next

        RemplirCojo()
        'If (GridViewComJugmt.RowCount > 0) Then
        '    BtExecuter.Enabled = False
        '    PanelLots.Enabled = True
        'End If
        BtRapportEval.Enabled = False
        BtImpMarche.Enabled = False

        OffresTraitees()

        If (EtapeActuelle = "ExamDetail") Then
            BtExecuter.Text = "FIN"
            PanelLots.Enabled = False
            'PanelVerdict.Visible = False
            ChargerGridExam(EtapeActuelle)

        ElseIf (EtapeActuelle = "ExamPost") Then
            BtExecuter.Text = "RAPPORT"
            PanelLots.Enabled = False
            'PanelVerdict.Visible = False
            BtRapportEval.Enabled = True
            BtImpMarche.Enabled = True
            BilanExamOffres()
        End If


    End Sub

    Private Sub TxtCodePresence_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtCodePresence.KeyDown
        If (e.KeyCode = Keys.Enter And CmbNumDoss.Text <> "") Then
            If (TxtCodePresence.Text <> "") Then
                Dim CodMembre As String = ""
                query = "select CodeMem,Evaluation from T_Commission where NumeroDAO='" & CmbNumDoss.Text & "' and PasseMem='" & TxtCodePresence.Text & "'"
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


                    query = "update T_Commission set Evaluation='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' where CodeMem='" & CodMembre & "'"
                    ExecuteNonQuery(query)


                    TxtCodePresence.Text = ""
                    RemplirCojo()
                Else
                    SuccesMsg("Accès réfusé!")
                End If

            End If

        End If
    End Sub

    Private Sub RemplirCojo()
        If (CmbNumDoss.Text <> "") Then

            dt.Columns.Clear()

            dt.Columns.Add("Commission", Type.GetType("System.String"))


            'Dim Reader As MySqlDataReader
            dt.Rows.Clear()
            query = "select NomMem,TitreMem from T_Commission where NumeroDAO='" & CmbNumDoss.Text & "' and Evaluation<>''"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                Dim drS = dt.NewRow()

                drS(0) = MettreApost(rw(0).ToString) & " (" & rw(1).ToString & ")"

                dt.Rows.Add(drS)
            Next

            GridCojo.DataSource = dt

            If (GridViewComJugmt.RowCount > 0) Then
                BtExecuter.Text = "DEBUT"
                BtExecuter.Enabled = True
            Else
                BtExecuter.Enabled = False
            End If
        End If
    End Sub

    Private Sub ContratDeMarche(ByVal NumMarche As String, Optional ByVal Traitement As String = "Afficher")

        Dim reportMarche As New ReportDocument
        Dim Chemin As String = lineEtat & "\Marches\"

        Dim DatSet = New DataSet
        reportMarche.Load(Chemin & "EtatMarche.rpt")
        reportMarche.SetDataSource(DatSet)
        reportMarche.SetParameterValue("CodeProjet", ProjetEnCours)
        reportMarche.SetParameterValue("NumeroMarche", NumMarche)

        If (Traitement = "Enregistrer") Then

            If (Directory.Exists(line & "\Marchés") = False) Then
                Directory.CreateDirectory(line & "\Marchés")
            End If

            If (Directory.Exists(line & "\Marchés\" & NumMarche.Replace("/", "_")) = False) Then
                Directory.CreateDirectory(line & "\Marchés\" & NumMarche.Replace("/", "_"))
            End If

            Try

                reportMarche.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, line & "\Marchés\" & NumMarche.Replace("/", "_") & "\Marche.doc")

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information)
            End Try

        ElseIf (Traitement = "Imprimer") Then

            reportMarche.PrintToPrinter(1, True, 0, 0)

        Else

            FullScreenReport.FullView.ReportSource = reportMarche
            FullScreenReport.ShowDialog()

        End If



    End Sub

    Private Sub RapportEvaluation(Optional ByVal Traitement As String = "Afficher")

        AfficherGrid("Rapport")
        TabRapportEval.Visible = True

        TxtTypeExamen.Text = "RAPPORT D'EVALUATION ET PROPOSITION DE MARCHE DU DAO N° " & CmbNumDoss.Text

        Dim Chemin As String = lineEtat & "\Evaluation\"
        Dim reportCouv, reportTab1a3, reportTab4, reportTab5, reportTab6, reportTab8A, reportTab9, reportRang1, reportPost, reportRang2, reportProposition, reportFavoris As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table
        Dim DatSet = New DataSet

        reportCouv.Load(Chemin & "RapportEvaluationPageGarde.rpt")
        reportTab1a3.Load(Chemin & "RapportEvaluationTableau1a3.rpt")
        reportTab4.Load(Chemin & "RapportEvaluationTableau4.rpt")
        reportTab5.Load(Chemin & "RapportEvaluationTableau5.rpt")
        reportTab6.Load(Chemin & "RapportEvaluationTableau6.rpt")
        reportTab8A.Load(Chemin & "RapportEvaluationTableau8A.rpt")
        reportTab9.Load(Chemin & "RapportEvaluationTableau9.rpt")
        reportRang1.Load(Chemin & "RapportEvaluationClassement1.rpt")
        reportPost.Load(Chemin & "RapportEvaluationPostQualif.rpt")
        reportRang2.Load(Chemin & "RapportEvaluationClassement2.rpt")
        reportProposition.Load(Chemin & "RapportEvaluationProposition.rpt")
        reportFavoris.Load(Chemin & "RapportEvaluationFavoris.rpt")

        With crConnectionInfo
            .ServerName = ODBCNAME
            .DatabaseName = DB
            .UserID = USERNAME
            .Password = PWD
        End With

        CrTables = reportCouv.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        CrTables = reportTab1a3.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        CrTables = reportTab4.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        CrTables = reportTab5.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        CrTables = reportTab6.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        CrTables = reportTab8A.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        CrTables = reportTab9.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        CrTables = reportRang1.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        CrTables = reportPost.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        CrTables = reportRang2.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        CrTables = reportProposition.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        CrTables = reportFavoris.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next


        'reportCouv.SetDataSource(DatSet)
        'reportTab1a3.SetDataSource(DatSet)
        reportTab4.SetDataSource(DatSet)
        reportTab5.SetDataSource(DatSet)
        reportTab6.SetDataSource(DatSet)
        reportTab8A.SetDataSource(DatSet)
        reportTab9.SetDataSource(DatSet)
        reportRang1.SetDataSource(DatSet)
        reportPost.SetDataSource(DatSet)
        reportRang2.SetDataSource(DatSet)
        reportProposition.SetDataSource(DatSet)
        reportFavoris.SetDataSource(DatSet)

        'NomProjet et ministere garde
        query = "select MinistereTutelle,NomProjet,AdresseProjet,BoitePostaleProjet,TelProjet,FaxProjet,MailProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            reportCouv.SetParameterValue("Ministere", MettreApost(rw(0).ToString))
            reportCouv.SetParameterValue("NomProjet", MettreApost(rw(1).ToString).ToUpper)
            reportCouv.SetParameterValue("CodeProjet", ProjetEnCours)

            reportTab1a3.SetParameterValue("NomProjet", MettreApost(rw(1).ToString))
            reportTab1a3.SetParameterValue("CodeProjet", ProjetEnCours)
            reportTab1a3.SetParameterValue("AdresseProjet", MettreApost(rw(2).ToString))
            reportTab1a3.SetParameterValue("BpProjet", rw(3).ToString)
            reportTab1a3.SetParameterValue("TelProjet", rw(4).ToString)
            reportTab1a3.SetParameterValue("FaxProjet", rw(5).ToString)
            reportTab1a3.SetParameterValue("MailProjet", rw(6).ToString)
        Next

        '*****************
        query = "select MethodePDM,NbreLotDAO,DatePublication,NumPublication,JournalPublication,LangueSoumission,DateFinOuverture,DureeSeance,NomEmprunteur,ValiditeOffre,PreQualif,DatePublication,NumPublication,JournalPublication,DateLimiteRemise,MontantMarche,ExamPostQualifOffres from T_DAO where NumeroDAO='" & CmbNumDoss.Text & "' and CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            reportCouv.SetParameterValue("MethodePdm", rw(0).ToString)
            reportCouv.SetParameterValue("NbLots", rw(1).ToString & IIf(CDec(rw(1)) > 1, " lots", " lot").ToString)
            reportCouv.SetParameterValue("DateOuverture", CDate(Mid(rw(6).ToString, 1, 10)).ToLongDateString)

            reportTab1a3.SetParameterValue("Emprunteur", MettreApost(rw(8).ToString))
            Dim partValid() As String = IIf(rw(9).ToString <> "", rw(9).ToString.Split(" "c), {"", ""})
            reportTab1a3.SetParameterValue("ValiditeOffres", IIf(partValid(1) = "Semaines", (CDec(partValid(0)) * 7).ToString & " Jours", IIf(partValid(1) = "Mois", (CDec(partValid(0)) * 30).ToString & " Jours", rw(9).ToString).ToString).ToString)
            reportTab1a3.SetParameterValue("ExamPrealOUI", IIf(rw(10).ToString = "OUI", "X", "").ToString)
            reportTab1a3.SetParameterValue("ExamPrealNON", IIf(rw(10).ToString = "NON", "X", "").ToString)
            reportTab1a3.SetParameterValue("DatePub", rw(11).ToString)
            reportTab1a3.SetParameterValue("JournalPub", MettreApost(rw(13).ToString))
            reportTab1a3.SetParameterValue("DateHeureDepot", rw(14).ToString.Replace(" ", " à "))
            reportTab1a3.SetParameterValue("CoutEstime", AfficherMonnaie(rw(15).ToString))
            reportTab1a3.SetParameterValue("AON_X", IIf(rw(0).ToString = "AON", "X", "").ToString)
            reportTab1a3.SetParameterValue("AOI_X", IIf(rw(0).ToString = "AOI", "X", "").ToString)
            reportTab1a3.SetParameterValue("Autres_X", IIf(rw(0).ToString <> "AON" And rw(0).ToString <> "AOI", "X", "").ToString)

            Dim partDate() As String = rw(6).ToString.Split(" "c)
            Dim duree As String = rw(7).ToString
            Dim heureOuv As DateTime = CDate(partDate(1)).AddHours(-CInt(Mid(duree, 1, 2))).AddMinutes(-CInt(Mid(duree, 4, 2))).AddSeconds(-CInt(Mid(duree, 7, 2)))

            reportTab1a3.SetParameterValue("DateHeureOuverture", partDate(0) & " à " & heureOuv.ToLongTimeString)
            reportCouv.SetParameterValue("DateFormatLong", IIf(rw(16).ToString <> "", CDate(Mid(rw(16).ToString, 1, 10)).ToLongDateString.ToUpper, "-").ToString)
        Next

        'Données du marché *********************
        Dim CodeMarche As Decimal = 0
        Dim LeBaill As String = ""
        Dim LibMarc As String = ""
        query = "select RefMarche,DescriptionMarche,InitialeBailleur from T_Marche where NumeroDAO='" & CmbNumDoss.Text & "' and CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CodeMarche = rw(0)
            LeBaill = rw(2)
            If (LibMarc <> "") Then
                LibMarc = LibMarc & vbNewLine & " et " & vbNewLine
            End If
            LibMarc = LibMarc & rw(1).ToString
        Next

        reportCouv.SetParameterValue("LibelleMarche", MettreApost(LibMarc).ToUpper)
        reportTab1a3.SetParameterValue("LibelleMarche", MettreApost(LibMarc))

        ' La convention ****************************
        query = "select C.CodeConvention,C.TypeConvention,C.MontantConvention,C.EntreeEnVigueur,C.DateCloture from T_Convention as C, T_Bailleur as B where B.CodeBailleur=C.CodeBailleur and B.InitialeBailleur='" & LeBaill & "' and B.CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            reportCouv.SetParameterValue("TypeConv", rw(1).ToString.ToUpper)
            reportCouv.SetParameterValue("NumConv", rw(0).ToString)
            reportCouv.SetParameterValue("Bailleur", LeBaill)

            reportTab1a3.SetParameterValue("NumConv", rw(0).ToString)
            reportTab1a3.SetParameterValue("DateVigueurConv", rw(3).ToString)
            reportTab1a3.SetParameterValue("DateClotureConv", rw(4).ToString)
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
        reportCouv.SetParameterValue("CodeCompo", CodComp)
        query = "select LibellePartition from T_Partition where LibelleCourt='" & CodComp & "' and CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            reportCouv.SetParameterValue("LibCompo", MettreApost(rw(0).ToString).ToUpper)
        Next

        '       Sous Composante   *****
        Dim CodSouComp As String = Mid(CodActiv1, 1, 2)
        reportCouv.SetParameterValue("CodeSouCompo", CodSouComp)
        query = "select LibellePartition from T_Partition where LibelleCourt='" & CodSouComp & "' and CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            reportCouv.SetParameterValue("LibSouCompo", MettreApost(rw(0).ToString).ToUpper)
        Next

        'Dossiers retirés
        query = "select Count(*) from T_Fournisseur where NumeroDAO='" & CmbNumDoss.Text & "' and CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            reportTab1a3.SetParameterValue("NbreDossierRetires", rw(0).ToString)
        Next
        'Offres recues
        query = "select Count(*) from T_Fournisseur where NumeroDAO='" & CmbNumDoss.Text & "' and CodeProjet='" & ProjetEnCours & "' and DateDepotDAO<>''"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            reportTab1a3.SetParameterValue("NbreOffresRecues", rw(0).ToString)
        Next


        reportCouv.SetParameterValue("NumDao", CmbNumDoss.Text)

        reportTab1a3.SetParameterValue("NumDao", CmbNumDoss.Text)
        reportTab1a3.SetParameterValue("FaveurPaysOUI", "")
        reportTab1a3.SetParameterValue("FaveurPaysNON", "")
        reportTab1a3.SetParameterValue("MarcheForfaitOUI", IIf(TxtTypeMarche.Text = "Fournitures", "X", ""))
        reportTab1a3.SetParameterValue("MarcheForfaitNON", IIf(TxtTypeMarche.Text = "Travaux", "X", ""))
        reportTab1a3.SetParameterValue("AvisGlePDM", "")

        reportTab4.SetParameterValue("NumDaoTab4", CmbNumDoss.Text)
        reportTab5.SetParameterValue("NumDaoTab5", CmbNumDoss.Text)
        reportTab6.SetParameterValue("NumDaoTab6", CmbNumDoss.Text)
        reportTab8A.SetParameterValue("NumDaoTab8A", CmbNumDoss.Text)
        reportTab9.SetParameterValue("NumDaoTab9", CmbNumDoss.Text)
        reportRang1.SetParameterValue("NumDaoClass1", CmbNumDoss.Text)
        reportPost.SetParameterValue("NumDaoPost", CmbNumDoss.Text)
        reportRang2.SetParameterValue("NumDaoClass2", CmbNumDoss.Text)
        reportProposition.SetParameterValue("NumDaoPropo", CmbNumDoss.Text)
        reportFavoris.SetParameterValue("NumDaoFavoris", CmbNumDoss.Text)

        'Traiter le cas de changement de favoris de marché!!!!!!!!!!!!!!!!!!!
        Dim AutreChoix As Boolean = False
        query = "select * from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & CmbNumDoss.Text & "' and S.RangPostQualif='1' and S.Selectionne<>'OUI'"
        dt0 = ExcecuteSelectQuery(query)
        If dt0.Rows.Count > 0 Then
            AutreChoix = True
        End If


        If (AutreChoix = True) Then
            ChangerFavoris.PageVisible = True
        Else
            ChangerFavoris.PageVisible = False
        End If



        If (Traitement = "Enregistrer") Then

            If (Directory.Exists(line & "\RapoortEvaluation") = False) Then
                Directory.CreateDirectory(line & "\RapoortEvaluation")
            End If

            If (Directory.Exists(line & "\RapoortEvaluation\" & CmbNumDoss.Text.Replace("/", "_")) = False) Then
                Directory.CreateDirectory(line & "\RapoortEvaluation\" & CmbNumDoss.Text.Replace("/", "_"))
            End If

            Try
                reportCouv.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, line & "\RapoortEvaluation\" & CmbNumDoss.Text.Replace("/", "_") & "\Couverture.docx")
                reportTab1a3.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, line & "\RapoortEvaluation\" & CmbNumDoss.Text.Replace("/", "_") & "\Tableau1a3.docx")
                reportTab4.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, line & "\RapoortEvaluation\" & CmbNumDoss.Text.Replace("/", "_") & "\Tableau4.docx")
                reportTab5.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, line & "\RapoortEvaluation\" & CmbNumDoss.Text.Replace("/", "_") & "\Tableau5.docx")
                reportTab6.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, line & "\RapoortEvaluation\" & CmbNumDoss.Text.Replace("/", "_") & "\Tableau6.docx")
                reportTab8A.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, line & "\RapoortEvaluation\" & CmbNumDoss.Text.Replace("/", "_") & "\Tableau8A.docx")
                reportTab9.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, line & "\RapoortEvaluation\" & CmbNumDoss.Text.Replace("/", "_") & "\Tableau9.docx")
                reportRang1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, line & "\RapoortEvaluation\" & CmbNumDoss.Text.Replace("/", "_") & "\Classement1.docx")
                reportPost.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, line & "\RapoortEvaluation\" & CmbNumDoss.Text.Replace("/", "_") & "\PostQualif.docx")
                reportRang2.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, line & "\RapoortEvaluation\" & CmbNumDoss.Text.Replace("/", "_") & "\Classement2.docx")
                reportProposition.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, line & "\RapoortEvaluation\" & CmbNumDoss.Text.Replace("/", "_") & "\Proposition.docx")
                If (AutreChoix = True) Then
                    reportFavoris.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, line & "\RapoortEvaluation\" & CmbNumDoss.Text.Replace("/", "_") & "\AutreChoix.docx")
                End If
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information)
            End Try

        ElseIf (Traitement = "Imprimer") Then

            If (AutreChoix = True) Then
                reportFavoris.PrintToPrinter(1, True, 0, 0)
            End If
            reportProposition.PrintToPrinter(1, True, 0, 0)
            reportRang2.PrintToPrinter(1, True, 0, 0)
            reportPost.PrintToPrinter(1, True, 0, 0)
            reportRang1.PrintToPrinter(1, True, 0, 0)
            reportTab9.PrintToPrinter(1, True, 0, 0)
            reportTab8A.PrintToPrinter(1, True, 0, 0)
            reportTab6.PrintToPrinter(1, True, 0, 0)
            reportTab5.PrintToPrinter(1, True, 0, 0)
            reportTab4.PrintToPrinter(1, True, 0, 0)
            reportTab1a3.PrintToPrinter(1, True, 0, 0)
            reportCouv.PrintToPrinter(1, True, 0, 0)

        Else
            RepCouverture.ReportSource = reportCouv
            RepTab1a3.ReportSource = reportTab1a3
            RepTab4.ReportSource = reportTab4
            RepTab5.ReportSource = reportTab5
            RepTab6.ReportSource = reportTab6
            RepTab8A.ReportSource = reportTab8A
            RepTab9.ReportSource = reportTab9
            RepRang1.ReportSource = reportRang1
            RepPost.ReportSource = reportPost
            RepRang2.ReportSource = reportRang2
            RepProposition.ReportSource = reportProposition
            If (AutreChoix = True) Then
                RepFavoris.ReportSource = reportFavoris
            End If
        End If

    End Sub

    Private Sub BtExecuter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtExecuter.Click
        If (BtExecuter.Text = "DEBUT") Then
            'MsgBox(EtapeActuelle, MsgBoxStyle.Information)

            PanelLots.Enabled = True
            BtExecuter.Text = "FIN"

            If (EtapeActuelle = "") Then
                LabelSoumis.Enabled = True
                CmbSoumis.Enabled = True
                TxtAdresseSoumis.Enabled = True
                CmbNumLot.Enabled = True
                TxtLibelleLot.Enabled = True
            ElseIf (EtapeActuelle = "Analyse" Or EtapeActuelle = "ExamPrelim") Then
                LabelSoumis.Enabled = False
                CmbSoumis.Enabled = False
                TxtAdresseSoumis.Enabled = False
                CmbNumLot.Enabled = True
                TxtLibelleLot.Enabled = True
            Else
                LabelSoumis.Enabled = False
                CmbSoumis.Enabled = False
                TxtAdresseSoumis.Enabled = False
                CmbNumLot.Enabled = False
                TxtLibelleLot.Enabled = False

            End If
            GbTraitement.Enabled = True

        ElseIf (BtExecuter.Text = "RAPPORT") Then

            DebutChargement(True, "Chargement du rapport d'évaluation en cours...")

            RapportEvaluation()
            RapportEvaluation("Enregistrer")
            BtExecuter.Text = "IMPRIMER"

            FinChargement()

        ElseIf (BtExecuter.Text = "IMPRIMER") Then

            DebutChargement(True, "Impression du rapport d'évaluation en cours...")

            RapportEvaluation("Imprimer")

            FinChargement()

        Else

            If (EtapeActuelle = "") Then
                'Code pour la fin de la verification *****************

                Dim AnalTerminee As Boolean = True
                query = "select * from T_Fournisseur as F,T_SoumissionFournisseur as S where S.CodeFournis=F.CodeFournis and F.NumeroDAO='" & CmbNumDoss.Text & "' and S.ConformiteTechnique=''"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    AnalTerminee = False
                    Exit For
                Next


                If (AnalTerminee = True) Then


                    query = "update T_DAO set AnalyseOffres='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' where NumeroDAO='" & CmbNumDoss.Text & "' and CodeProjet='" & ProjetEnCours & "'"
                    ExecuteNonQuery(query)


                    CmbNumDoss_SelectedIndexChanged(Me, e)
                    BtExecuter.Text = "DEBUT"

                    'Initialiser le formulaire ********
                    'PanelLots.Enabled = False
                    PanelVerdict.Enabled = False
                Else
                    MsgBox("Vérification inachevée!", MsgBoxStyle.Information)
                End If

            ElseIf (EtapeActuelle = "Analyse") Then

                Dim ExamComplet As Boolean = True
                'For k As Integer = 0 To ViewTravail.RowCount - 1
                '    If (dtExam.Rows(k).Item(8).ToString.Replace(" ", "") = "-") Then
                '        ExamComplet = False
                '        Exit For
                '    End If
                'Next
                'Dim AnalTerminee As Boolean = True
                query = "select * from T_Fournisseur as F,T_SoumissionFournisseur as S where S.CodeFournis=F.CodeFournis and F.NumeroDAO='" & CmbNumDoss.Text & "' and S.ConformiteTechnique='OUI' and S.AcceptationExamDetaille=''"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    ExamComplet = False
                    Exit For
                Next


                If (ExamComplet = True) Then

                    Dim RepPrelim As MsgBoxResult = MsgBox("Voulez-vous valider l'examen préliminaire?", MsgBoxStyle.YesNo)
                    If (RepPrelim = MsgBoxResult.Yes) Then


                        query = "update T_DAO set ExamPrelimOffres='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' where NumeroDAO='" & CmbNumDoss.Text & "' and CodeProjet='" & ProjetEnCours & "'"
                        ExecuteNonQuery(query)


                        MsgBox("Traitement effectué avec succès!", MsgBoxStyle.Information)

                        dtExam.Columns.Clear()
                        dtExam.Rows.Clear()
                        GridTravail.Refresh()
                        TxtLibelleLot.Text = ""
                        CmbNumLot.Text = ""
                        'TxtLibelleLot.Enabled = False
                        'CmbNumLot.Enabled = False
                        CmbNumDoss_SelectedIndexChanged(Me, e)
                        BtExecuter.Text = "DEBUT"


                    End If

                Else
                    MsgBox("Tous les soumissionnaires n'ont pas été examinés!", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If

            ElseIf (EtapeActuelle = "ExamPrelim") Then

                Dim AnalTerminee As Boolean = True
                query = "select * from T_Fournisseur as F,T_SoumissionFournisseur as S where S.CodeFournis=F.CodeFournis and F.NumeroDAO='" & CmbNumDoss.Text & "' and S.AcceptationExamDetaille='OUI' and S.RangExamDetaille='0'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    AnalTerminee = False
                    Exit For
                Next

                If (AnalTerminee = True) Then

                    Dim RepPrelim As MsgBoxResult = MsgBox("Voulez-vous terminer l'examen détaillée?", MsgBoxStyle.YesNo)
                    If (RepPrelim = MsgBoxResult.Yes) Then


                        query = "update T_DAO set ExamDetailOffres='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' where NumeroDAO='" & CmbNumDoss.Text & "' and CodeProjet='" & ProjetEnCours & "'"
                        ExecuteNonQuery(query)


                        MsgBox("Traitement effectué avec succès!", MsgBoxStyle.Information)

                        dtExam.Columns.Clear()
                        dtExam.Rows.Clear()
                        GridTravail.Refresh()
                        TxtLibelleLot.Text = ""
                        CmbNumLot.Text = ""
                        'TxtLibelleLot.Enabled = False
                        'CmbNumLot.Enabled = False
                        CmbNumDoss_SelectedIndexChanged(Me, e)
                        BtExecuter.Text = "DEBUT"
                        PnlTermine.Visible = False

                    End If

                Else
                    MsgBox("Tous les soumissionnaires n'ont pas été examinés!", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If

            ElseIf (EtapeActuelle = "ExamDetail") Then

                'cloture de l'examen post qualif
                Dim AnalTerminee As Boolean = True
                query = "select * from T_Fournisseur as F,T_SoumissionFournisseur as S where S.CodeFournis=F.CodeFournis and F.NumeroDAO='" & CmbNumDoss.Text & "' and S.RangExamDetaille<>'0' and S.ExamPQValide=''"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    AnalTerminee = False
                    Exit For
                Next

                If (AnalTerminee = True) Then

                    Dim RepPrelim As MsgBoxResult = MsgBox("Voulez-vous terminer l'examen post qualification?", MsgBoxStyle.YesNo)
                    If (RepPrelim = MsgBoxResult.Yes) Then


                        query = "update T_DAO set ExamPostQualifOffres='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' where NumeroDAO='" & CmbNumDoss.Text & "' and CodeProjet='" & ProjetEnCours & "'"
                        ExecuteNonQuery(query)


                        MsgBox("Traitement effectué avec succès!", MsgBoxStyle.Information)
                        ClassementPostQualif()

                        dtExam.Columns.Clear()
                        dtExam.Rows.Clear()
                        GridTravail.Refresh()
                        TxtLibelleLot.Text = ""
                        CmbNumLot.Text = ""
                        'TxtLibelleLot.Enabled = False
                        'CmbNumLot.Enabled = False
                        CmbNumDoss_SelectedIndexChanged(Me, e)
                        'BtExecuter.Text = "DEBUT"
                        PnlTermine.Visible = False

                    End If

                Else
                    MsgBox("Tous les soumissionnaires n'ont pas été examinés!", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If



            End If

            GbTraitement.Enabled = False
        End If



    End Sub

    Private Sub ChargerExamPostQualif()
        AfficherGrid(EtapeActuelle)

        PnlTermine.Visible = False

        If (CmbNumDoss.Text <> "") Then

            Dim ExamTerminee As Boolean = True

            NombreCritere = 0
            Dim ColCritere(100) As String

            dtExam.Columns.Clear()
            dtExam.Columns.Add("Code", Type.GetType("System.String"))
            dtExam.Columns.Add("CodeRef", Type.GetType("System.String"))
            dtExam.Columns.Add("Soumissionnaire", Type.GetType("System.String"))
            Dim nbCrit As Integer = 0

            'Dim Reader As MySqlDataReader
            query = "select LibelleCritere,CritereElimine,RefCritere,RefCritereMere from T_DAO_PostQualif where NumeroDAO='" & CmbNumDoss.Text & "' and RefCritereMere<>'0' order by RefCritere"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                nbCrit += 1
                Dim Mark As String = IIf(rw(1).ToString = "OUI", "*", "").ToString
                ColCritere(NombreCritere) = "[CRITERE N°" & nbCrit.ToString & "]" & Mark

                CodeCritere(NombreCritere) = rw(2).ToString
                TableCritere(NombreCritere) = MettreApost(rw(0).ToString)
                CritereElimine(NombreCritere) = rw(1).ToString
                GroupeCritere(NombreCritere) = rw(3).ToString
                NombreCritere += 1
            Next

            For i As Integer = 0 To NombreCritere - 1
                dtExam.Columns.Add(ColCritere(i), Type.GetType("System.String"))

                query = "select LibelleCritere from T_DAO_PostQualif where NumeroDAO='" & CmbNumDoss.Text & "' and RefCritere='" & GroupeCritere(i) & "'"
                dt0 = ExcecuteSelectQuery(query)
                If dt0.Rows.Count > 0 Then
                    GroupeCritere(i) = MettreApost(dt0.Rows(0).Item(0).ToString)
                End If

            Next
            dtExam.Columns.Add("Conclusion", Type.GetType("System.String"))

            Dim cpt As Decimal = 0
            dtExam.Rows.Clear()
            query = "select F.CodeFournis,F.NomFournis,F.PostQualifie from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & CmbNumDoss.Text & "' and S.RangExamDetaille<>'0' group by F.CodeFournis,F.NomFournis,F.PostQualifie order by F.NomFournis"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                Dim DrE = dtExam.NewRow()
                cpt += 1
                DrE(0) = IIf(CDec(cpt / 2) = CDec(cpt \ 2), "x", "")
                DrE(1) = rw(0).ToString
                DrE(2) = MettreApost(rw(1).ToString)

                Dim nbCrit2 As Decimal = 2
                query = "select Verdict,Commentaire from T_SoumisFournisPostQualif where CodeFournis='" & rw(0).ToString & "' order by RefCritere"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows
                    nbCrit2 = nbCrit2 + 1
                    DrE(nbCrit2) = IIf(rw1(0).ToString.Replace(" ", "") <> "", rw1(0).ToString, "-")
                Next

                DrE(nbCrit2 + 1) = IIf(rw(2).ToString.Replace(" ", "") <> "", IIf(rw(2).ToString = "OUI", "QUALIFIE", "DISQUALIFIE").ToString, "-").ToString

                If (rw(2).ToString = "") Then ExamTerminee = False

                dtExam.Rows.Add(DrE)
            Next

            GridPost.DataSource = dtExam

            ViewPost.Columns(0).Visible = False
            ViewPost.Columns(1).Visible = False
            ViewPost.Columns(2).Width = 250
            For k As Integer = 1 To nbCrit
                ViewPost.Columns(2 + k).Width = CInt(IIf(GridPost.Width > 400 + (100 * nbCrit), CInt((GridPost.Width - 400) / nbCrit), 100).ToString)
                ViewPost.Columns(2 + k).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                ViewPost.Columns(2 + k).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            Next
            ViewPost.Columns(2 + nbCrit + 1).Width = 150
            ViewPost.Columns(2 + nbCrit + 1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewPost.Columns(2 + nbCrit + 1).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            ViewPost.Columns(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewPost.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewPost.Columns(2).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewPost.Columns(2 + nbCrit + 1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right

            ColorRowGrid(ViewPost, "[Code]='x'", Color.LightGray, "Tahoma", 8, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewPost, "[Conclusion]='DISQUALIFIE'", Color.White, "Tahoma", 8, FontStyle.Regular, Color.Red, False)

            If (ExamTerminee = True) Then
                PnlTermine.Visible = True
            Else
                PnlTermine.Visible = False
            End If

        End If

    End Sub

    Private Sub CmbNumLot_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumLot.SelectedValueChanged
        If CmbNumLot.Text <> "" Then
            Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDoss.Text)
            Dim nbsouslot As Integer = Val(Resultat(0))
            If nbsouslot > 0 Then
                cmbSousLot.Enabled = True
                cmbSousLot.Text = ""
                cmbSousLot.Properties.Items.Clear()
                Dim dt As DataTable = CType(Resultat(1), DataTable)
                For Each rw As DataRow In dt.Rows
                    cmbSousLot.Properties.Items.Add(rw("CodeSousLot").ToString)
                Next
                query = "select LibelleLot,RefLot from T_LotDAO where NumeroDAO='" & CmbNumDoss.Text & "' and CodeLot='" & CmbNumLot.Text & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    TxtLibelleLot.Text = MettreApost(rw(0).ToString)
                    TxtRefLot.Text = rw(1).ToString
                    'ChargerSoumis(EtapeActuelle)
                Next
            Else
                cmbSousLot.Enabled = False
                CmbSoumis.Text = ""
                TxtAdresseSoumis.Text = ""
                query = "select LibelleLot,RefLot from T_LotDAO where NumeroDAO='" & CmbNumDoss.Text & "' and CodeLot='" & CmbNumLot.Text & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    TxtLibelleLot.Text = MettreApost(rw(0).ToString)
                    TxtRefLot.Text = rw(1).ToString

                    ChargerSoumis(EtapeActuelle)
                Next
            End If
            If (EtapeActuelle <> "") Then ChargerGridExam(EtapeActuelle)
        End If
    End Sub

    Private Sub ChargerSoumis(ByVal ActuEtape As String)

        If (ActuEtape = "") Then
            Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDoss.Text)
            Dim nbsouslot As Integer = Val(Resultat(0))
            If nbsouslot > 0 Then
                query = "select F.NomFournis from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & CmbNumDoss.Text & "' and S.CodeLot='" & CmbNumLot.Text & "' and F.CodeProjet='" & ProjetEnCours & "' and F.DateDepotDAO<>'' AND S.CodeSousLot='" & cmbSousLot.Text & "'"
            Else
                query = "select F.NomFournis from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & CmbNumDoss.Text & "' and S.CodeLot='" & CmbNumLot.Text & "' and F.CodeProjet='" & ProjetEnCours & "' and F.DateDepotDAO<>''"
            End If
            CmbSoumis.Properties.Items.Clear()
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                CmbSoumis.Properties.Items.Add(MettreApost(rw(0).ToString))
            Next

            For k As Integer = 0 To ViewTraité.RowCount - 1
                If (dtTraite.Rows(k).Item(0).ToString <> "x") Then
                    If (CmbSoumis.Properties.Items.Contains(dtTraite.Rows(k).Item(1).ToString.Replace("  - ", ""))) Then
                        Dim OkLot As Boolean = False
                        Dim posLot As Decimal = k

                        While (OkLot = False And posLot > 0)
                            posLot = posLot - 1
                            If (dtTraite.Rows(posLot).Item(0).ToString = "x") Then
                                If (Mid(dtTraite.Rows(posLot).Item(1).ToString, 7) = CmbNumLot.Text) Then
                                    OkLot = True
                                Else
                                    Exit While
                                End If
                            End If
                        End While

                        If (OkLot = True) Then CmbSoumis.Properties.Items.Remove(dtTraite.Rows(k).Item(1).ToString.Replace("  - ", ""))

                    End If
                End If
            Next

        ElseIf (ActuEtape = "Analyse") Then
            'ChargerGridExam(EtapeActuelle)

        ElseIf (ActuEtape = "ExamPrelim") Then

        ElseIf (ActuEtape = "ExamDetail") Then

        ElseIf (ActuEtape = "ExamPost") Then

        End If


    End Sub

    Private Sub CmbSoumis_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSoumis.SelectedValueChanged
        query = "select F.CodeFournis,F.PaysFournis,S.RefSoumis from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and S.CodeLot='" & CmbNumLot.Text & "' and F.NomFournis='" & EnleverApost(CmbSoumis.Text) & "' and F.NumeroDAO='" & CmbNumDoss.Text & "' and F.CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            TxtCodeFournis.Text = rw(0).ToString
            TxtRefSoumis.Text = rw(2).ToString
            TxtAdresseSoumis.Text = MettreApost(rw(1).ToString)
        Next
        ChargerGridExam(EtapeActuelle)
    End Sub

    Private Sub ChargerGridExam(ByVal TypeExam As String)
        If (TypeExam = "") Then
            ChargerAnalyse()

        ElseIf (TypeExam = "Analyse") Then
            ChargerExamPrelim()

        ElseIf (TypeExam = "ExamPrelim") Then
            ChargerExamDetaille()

        ElseIf (TypeExam = "ExamDetail") Then
            ChargerExamPostQualif()

        ElseIf (TypeExam = "ExamPost") Then


        End If
    End Sub

    Private Sub ChargerExamDetaille()
        AfficherGrid(EtapeActuelle)

        PnlTermine.Visible = False

        If (CmbNumLot.Text <> "") Then

            dtExam.Columns.Clear()

            dtExam.Columns.Add("Code", Type.GetType("System.String"))
            dtExam.Columns.Add("CodeRef", Type.GetType("System.String"))
            dtExam.Columns.Add("Soumissionnaire", Type.GetType("System.String"))
            dtExam.Columns.Add("Monnaie", Type.GetType("System.String"))
            dtExam.Columns.Add("Taux de change", Type.GetType("System.String"))
            dtExam.Columns.Add("Montant de l'offre", Type.GetType("System.String"))
            dtExam.Columns.Add("Montant corrigé en monnaie d'évaluation", Type.GetType("System.String"))
            dtExam.Columns.Add("Erreurs de calcul", Type.GetType("System.String"))
            dtExam.Columns.Add("Sommes provisionnelles", Type.GetType("System.String"))
            dtExam.Columns.Add("Montant rabais", Type.GetType("System.String"))
            dtExam.Columns.Add("Ajouts pour omission", Type.GetType("System.String"))
            dtExam.Columns.Add("Ajustements", Type.GetType("System.String"))
            dtExam.Columns.Add("Variations mineures", Type.GetType("System.String"))
            dtExam.Columns.Add("Prix Total de l'Offre", Type.GetType("System.String"))
            dtExam.Columns.Add("Rang", Type.GetType("System.String"))

            Dim cpt2 As Decimal = 0
            dtExam.Rows.Clear()
            query = "select F.NomFournis,S.RefSoumis,S.Monnaie,S.MontantPropose,S.MontantAvecMonnaie,S.ErreurCalcul,S.SomProvision,S.PrctRabais,S.MontantRabais,S.AjoutOmission,S.Ajustements,S.VariationMineure,S.PrixCorrigeOffre,S.RangExamDetaille,S.SigneErreur from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & CmbNumDoss.Text & "' and F.CodeProjet='" & ProjetEnCours & "' and S.CodeLot='" & CmbNumLot.Text & "' and AcceptationExamDetaille='OUI' order by S.RangExamDetaille,F.Nomfournis"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                Dim DrE = dtExam.NewRow()
                cpt2 += 1
                DrE(0) = IIf(CDec(cpt2 / 2) = CDec(cpt2 \ 2), "x", "").ToString
                DrE(1) = rw(1).ToString
                DrE(2) = MettreApost(rw(0).ToString)
                DrE(3) = rw(2).ToString

                Dim leTaux As String = ""
                query = "select TauxDevise from T_Devise where AbregeDevise='" & rw(2).ToString & "'"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows
                    leTaux = rw1(0).ToString
                Next


                DrE(4) = AfficherMonnaie(leTaux)
                DrE(5) = AfficherMonnaie(rw(3).ToString)
                DrE(6) = AfficherMonnaie(rw(4).ToString)
                DrE(7) = IIf(rw(14).ToString = "-", rw(14).ToString & " ", "").ToString & AfficherMonnaie(rw(5).ToString)
                DrE(8) = AfficherMonnaie(rw(6).ToString)
                DrE(9) = AfficherMonnaie(rw(8).ToString)
                DrE(10) = AfficherMonnaie(rw(9).ToString)
                DrE(11) = AfficherMonnaie(rw(10).ToString)
                DrE(12) = AfficherMonnaie(rw(11).ToString)
                DrE(13) = AfficherMonnaie(rw(12).ToString)
                DrE(14) = IIf(rw(13).ToString <> "0", rw(13).ToString & IIf(rw(13).ToString = "1", "er", "ème").ToString, "-")

                dtExam.Rows.Add(DrE)
            Next

            GridDetail.DataSource = dtExam

            ViewDetail.Columns(0).Visible = False
            ViewDetail.Columns(1).Visible = False
            ViewDetail.Columns(2).Width = 250
            ViewDetail.Columns(3).Width = 80
            ViewDetail.Columns(4).Width = 60
            ViewDetail.Columns(5).Width = 100
            ViewDetail.Columns(6).Width = 100
            ViewDetail.Columns(7).Width = 100
            ViewDetail.Columns(8).Width = 100
            ViewDetail.Columns(9).Width = 100
            ViewDetail.Columns(10).Width = 100
            ViewDetail.Columns(11).Width = 100
            ViewDetail.Columns(12).Width = 100
            ViewDetail.Columns(13).Width = 150
            ViewDetail.Columns(14).Width = 60

            ViewDetail.Columns(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewDetail.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewDetail.Columns(2).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewDetail.Columns(14).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right

            ViewDetail.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            For k As Integer = 4 To 14
                ViewDetail.Columns(k).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            Next

            ColorRowGrid(ViewDetail, "[Code]='x'", Color.LightGray, "Tahoma", 8, FontStyle.Regular, Color.Black)
            'ColorRowGridAnal(ViewTravail, "[Acceptation pour examen détaillé]='NON'", Color.White, "Tahoma", 8, FontStyle.Regular, Color.Red, False)


            'Dim ToutTraite As Boolean = True
            'For k As Integer = 0 To ViewTravail.RowCount - 1
            '    If (dtExam.Rows(k).Item(14).ToString.Replace(" ", "").Replace("-", "") = "") Then ToutTraite = False
            '    Exit For
            'Next

            'If (ToutTraite = True) Then
            '    PnlTermine.Visible = True
            'End If

        End If


    End Sub

    Private Sub ChargerExamPrelim()
        If (CmbNumLot.Text <> "") Then
            AfficherGrid(EtapeActuelle)

            dtExam.Columns.Clear()

            dtExam.Columns.Add("CodeRef", Type.GetType("System.String"))
            dtExam.Columns.Add("Soumissionnaire", Type.GetType("System.String"))
            dtExam.Columns.Add("Vérification", Type.GetType("System.String"))
            dtExam.Columns.Add("Critères de provenance", Type.GetType("System.String"))
            dtExam.Columns.Add("Conformité aux spécifications techniques", Type.GetType("System.String"))
            dtExam.Columns.Add("Garantie de l'offre", Type.GetType("System.String"))
            dtExam.Columns.Add("Exhaustivité de l'offre", Type.GetType("System.String"))
            dtExam.Columns.Add("Conformité pour l'essentiel", Type.GetType("System.String"))
            dtExam.Columns.Add("Acceptation pour examen détaillé", Type.GetType("System.String"))
            dtExam.Columns.Add("Code", Type.GetType("System.String"))

            Dim cpt2 As Decimal = 0
            dtExam.Rows.Clear()
            query = "select F.NomFournis,F.PaysFournis,S.RefSoumis,S.ConformiteTechnique,S.ConformiteGarantie,S.CautionBancaire,S.ExhaustiviteOffre,S.ConformiteEssentiel,S.AcceptationExamDetaille,S.Verification,S.ConformiteProvenance from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & CmbNumDoss.Text & "' and F.CodeProjet='" & ProjetEnCours & "' and S.CodeLot='" & CmbNumLot.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                cpt2 += 1
                Dim DrE = dtExam.NewRow()
                DrE(9) = IIf(CDec(cpt2 / 2) = CDec(cpt2 \ 2), "x", "").ToString
                DrE(1) = MettreApost(rw(0).ToString)
                DrE(2) = IIf(rw(9).ToString <> "", rw(9).ToString, "-")    'IIf(rw(9).ToString <> "", rw(9).ToString, "-").ToString

                Dim ProvOk As Boolean = True
                query = "select DateDebutSanction,DateFinSanction from T_SanctionPays where PaysSanction='" & rw(1).ToString & "'"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows
                    If (DateTime.Compare(Now.ToShortDateString, CDate(rw(0).ToString)) >= 0 And DateTime.Compare(Now.ToShortDateString, CDate(rw(1).ToString)) <= 0) Then
                        ProvOk = False
                    End If
                Next

                If (rw(1).ToString = "") Then ProvOk = False

                DrE(3) = IIf(rw(10).ToString <> "", rw(10).ToString, IIf(ProvOk = True, "OUI", "NON").ToString).ToString
                DrE(4) = IIf(rw(3).ToString <> "", rw(3).ToString, "-")

                Dim GarantiOk As Boolean = False
                query = "select MontantGarantie from T_LotDAO where NumeroDAO='" & CmbNumDoss.Text & "' and CodeLot='" & CmbNumLot.Text & "'"
                dt1 = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows
                    Dim montGar As Decimal = 0
                    If (rw1(0).ToString <> "") Then
                        If (IsNumeric(rw1(0).ToString) = True) Then
                            montGar = CDec(rw1(0).ToString)
                        Else
                            montGar = 0
                        End If
                    Else
                        montGar = 0
                    End If

                    Dim montGarOffre As Decimal = 0
                    If (rw1(0).ToString <> "") Then
                        If rw(5).ToString <> "" Then
                            montGarOffre = IIf(rw(5).ToString = "", 0, IIf(IsNumeric(rw(5).ToString) = True, CDec(rw(5).ToString), 0))
                        End If
                    Else

                    End If

                    If (montGarOffre >= montGar) Then
                        GarantiOk = True
                    End If
                Next

                DrE(5) = IIf(GarantiOk = True, "OUI", "NON").ToString
                DrE(6) = IIf(rw(6).ToString <> "", rw(6).ToString, "-")
                DrE(7) = IIf(rw(7).ToString <> "", rw(7).ToString, "-")
                DrE(8) = IIf(rw(8).ToString <> "", rw(8).ToString, "-")
                DrE(0) = rw(2).ToString

                dtExam.Rows.Add(DrE)
            Next

            GridPrelim.DataSource = dtExam

            ViewPrelim.Columns(0).Visible = False
            ViewPrelim.OptionsView.ColumnAutoWidth = True
            'ViewPrelim.Columns(1).Width = 300
            'ViewPrelim.Columns(2).Width = 100
            'ViewPrelim.Columns(3).Width = 100
            'ViewPrelim.Columns(4).Width = 100
            'ViewPrelim.Columns(5).Width = 100
            'ViewPrelim.Columns(6).Width = 100
            'ViewPrelim.Columns(7).Width = 100
            'ViewPrelim.Columns(8).Width = 100
            ViewPrelim.Columns(9).Visible = False

            'ViewPrelim.Columns(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            'ViewPrelim.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

            For k As Integer = 2 To 8
                ViewPrelim.Columns(k).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            Next

            ColorRowGrid(ViewPrelim, "[Code]='x'", Color.LightGray, "Tahoma", 8, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewPrelim, "[Acceptation pour examen détaillé]='NON'", Color.White, "Tahoma", 8, FontStyle.Regular, Color.Red, False)

        End If
    End Sub

    Private Sub AfficherGrid(ByVal Examen As String)
        TabRapportEval.Visible = False
        GbTraites.Visible = True
        FullCouverture.Visible = False
        PnlEditionMarche.Visible = False

        If (Examen = "") Then
            GridTravail.Visible = True
            GridPrelim.Visible = False
            GridDetail.Visible = False
            GridPost.Visible = False
            GridBilan.Visible = False
        ElseIf (Examen = "Analyse") Then
            GridTravail.Visible = False
            GridPrelim.Visible = True
            GridDetail.Visible = False
            GridPost.Visible = False
            GridBilan.Visible = False
        ElseIf (Examen = "ExamPrelim") Then
            GridTravail.Visible = False
            GridPrelim.Visible = False
            GridDetail.Visible = True
            GridPost.Visible = False
            GridBilan.Visible = False
        ElseIf (Examen = "ExamDetail") Then
            GridTravail.Visible = False
            GridPrelim.Visible = False
            GridDetail.Visible = False
            GridPost.Visible = True
            GridBilan.Visible = False
        ElseIf (Examen = "ExamPost") Then
            GridTravail.Visible = False
            GridPrelim.Visible = False
            GridDetail.Visible = False
            GridPost.Visible = False
            GridBilan.Visible = True
        Else
            GridTravail.Visible = False
            GridPrelim.Visible = False
            GridDetail.Visible = False
            GridPost.Visible = False
            GridBilan.Visible = False
            TabRapportEval.Visible = True
            GbTraites.Visible = False
            FullCouverture.Visible = True
        End If
    End Sub

    Private Sub ChargerAnalyse()
        If CmbSoumis.Text = "" Then
            dtExam.Rows.Clear()
        End If
        If (CmbSoumis.Text <> "") Then
            AfficherGrid(EtapeActuelle)

            If (TxtTypeMarche.Text = "Fournitures") Then

                dtExam.Columns.Clear()

                dtExam.Columns.Add("Code", Type.GetType("System.String"))
                dtExam.Columns.Add("Spécifications techniques", Type.GetType("System.String"))
                dtExam.Columns.Add("Valeurs demandées", Type.GetType("System.String"))
                dtExam.Columns.Add("Valeurs offertes", Type.GetType("System.String"))
                dtExam.Columns.Add("Conformité / Commentaire", Type.GetType("System.String"))
                dtExam.Columns.Add("NC", Type.GetType("System.String"))
                dtExam.Columns.Add("SpecDemandé", Type.GetType("System.String"))


                dtExam.Rows.Clear()
                Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDoss.Text)
                Dim nbsouslot As Integer = Val(Resultat(0))
                If nbsouslot > 0 Then
                    query = "select RefSpecFournit,CodeFournit,DescripFournit from T_SpecTechFourniture where NumeroDAO='" & CmbNumDoss.Text & "' and CodeLot='" & CmbNumLot.Text & "' and CodeSousLot='" & cmbSousLot.Text & "'"
                Else
                    query = "select RefSpecFournit,CodeFournit,DescripFournit from T_SpecTechFourniture where NumeroDAO='" & CmbNumDoss.Text & "' and CodeLot='" & CmbNumLot.Text & "'"
                End If
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    Dim FournitExist As Boolean = True
                    query = "select RefSpecCaract,LibelleCaract,ValeurCaract from T_SpecTechCaract where RefSpecFournit='" & rw(0).ToString & "'"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt1.Rows
                        If (FournitExist = True) Then
                            Dim drS = dtExam.NewRow()
                            drS(0) = "x"
                            drS(1) = rw(1).ToString.ToUpper & " : " & MettreApost(rw(2).ToString).ToUpper
                            drS(2) = ""
                            drS(3) = ""

                            query = "select PrixUnitaire from T_SoumisPrixFourniture where RefSpecFournit='" & rw(0).ToString & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                            Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                            If dt2.Rows.Count > 0 Then
                                If (dt2.Rows(0).Item(0).ToString <> "") Then
                                    drS(4) = "Prix unitaire Htva : " & AfficherMonnaie(dt2.Rows(0).Item(0).ToString)
                                Else
                                    drS(4) = ""
                                End If
                            Else
                                drS(4) = ""
                            End If
                            dtExam.Rows.Add(drS)
                            FournitExist = False
                        End If

                        Dim drC = dtExam.NewRow()
                        drC(0) = rw1(0).ToString
                        drC(1) = MettreApost(rw1(1).ToString)
                        drC(2) = MettreApost(rw1(2).ToString)
                        query = "select ValeurOfferte,MentionValeur,Commentaire from T_SoumisCaractFournit where RefSpecCaract='" & rw1(0).ToString & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                        Dim dtx As DataTable = ExcecuteSelectQuery(query)
                        If dtx.Rows.Count > 0 Then
                            If (dtx.Rows(0).Item(0).ToString.Replace(" ", "") <> "") Then
                                drC(3) = MettreApost(dtx.Rows(0).Item(0).ToString)
                            Else
                                drC(3) = "..."
                            End If
                            drC(4) = IIf(dtx.Rows(0).Item(1).ToString <> "", dtx.Rows(0).Item(1).ToString, "").ToString & IIf(dtx.Rows(0).Item(2).ToString <> "", " : " & MettreApost(dtx.Rows(0).Item(2).ToString), "")
                            drC(5) = IIf(dtx.Rows(0).Item(1).ToString = "Non Conforme", "x", "").ToString
                        Else
                            drC(3) = "..."
                            drC(4) = "..."
                        End If
                        drC(6) = "oui"
                        dtExam.Rows.Add(drC)

                    Next

                    query = "select a.LibelleCaract,a.RefSpecCaractPro, b.ValeurOfferte, b.MentionValeur, b.Commentaire from t_spectechcaractpropose a, t_soumiscaractfournitsupl b where a.RefSpecFournit='" & rw(0).ToString & "' And a.RefSpecCaractPro=b.RefSpecCaract and b.RefSoumis='" & TxtRefSoumis.Text & "'"
                    Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw3 As DataRow In dt3.Rows
                        Dim drS = dtExam.NewRow()
                        drS(0) = rw3("RefSpecCaractPro").ToString
                        drS(1) = MettreApost(rw3("LibelleCaract").ToString)
                        drS(2) = "-"
                        drS(3) = MettreApost(rw3("ValeurOfferte").ToString)
                        drS(4) = IIf(rw3("MentionValeur").ToString <> "", rw3("MentionValeur").ToString, "").ToString & IIf(rw3("Commentaire").ToString <> "", " : " & MettreApost(rw3("Commentaire").ToString), "")
                        drS(5) = IIf(rw3("MentionValeur").ToString = "Non Conforme", "x", "").ToString
                        drS(6) = "non"
                        dtExam.Rows.Add(drS)
                    Next

                Next
                GridTravail.DataSource = dtExam

                ViewTravail.Columns(0).Visible = False
                ViewTravail.OptionsView.ColumnAutoWidth = True
                'ViewTravail.Columns(1).Width = 300
                'ViewTravail.Columns(2).Width = 200
                'ViewTravail.Columns(3).Width = 200
                'ViewTravail.Columns(4).Width = 350
                ViewTravail.Columns(5).Visible = False
                ViewTravail.Columns(6).Visible = False

                'ViewTravail.Columns(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
                'ViewTravail.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                ColorRowGrid(ViewTravail, "[Code]='x'", Color.LightGray, "Tahoma", 8, FontStyle.Bold, Color.Black)
                ColorRowGridAnal(ViewTravail, "[Valeurs offertes]='...'", Color.Yellow, "Tahoma", 8, FontStyle.Regular, Color.Black)
                ColorRowGridAnal(ViewTravail, "[NC]='x'", Color.White, "Tahoma", 8, FontStyle.Bold, Color.Red)

            Else            'Travaux **************

                Dim TampVerdict As Boolean = True
                Dim ToutAnalyser As Boolean = True
                Dim VerdictVerouille As Boolean = False

                dtExam.Columns.Clear()

                dtExam.Columns.Add("Code", Type.GetType("System.String"))
                dtExam.Columns.Add("CodeX", Type.GetType("System.String"))
                dtExam.Columns.Add("Critère de conformité", Type.GetType("System.String"))
                dtExam.Columns.Add("Conforme", Type.GetType("System.String"))
                dtExam.Columns.Add("Commentaire", Type.GetType("System.String"))

                'Dim CptLg As Decimal = -1
                'Dim PosLg As Decimal = -1
                dtExam.Rows.Clear()
                query = "select LibelleConformTech,RefConformTech from T_DAO_ConformTech where NumeroDAO='" & CmbNumDoss.Text & "' and CodeLot in ('" & CmbNumLot.Text & "','x') and RefConformMere='0'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    Dim cptr1 As Decimal = 0
                    Dim drS = dtExam.NewRow()

                    drS(0) = "x"
                    drS(1) = rw(1).ToString
                    drS(2) = MettreApost(rw(0).ToString)
                    drS(3) = ""
                    drS(4) = ""

                    dtExam.Rows.Add(drS)

                    query = "select LibelleConformTech,Eliminatoire,RefConformTech from T_DAO_ConformTech where NumeroDAO='" & CmbNumDoss.Text & "' and RefConformMere='" & rw(1).ToString & "'"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt1.Rows
                        cptr1 += 1
                        Dim drS2 = dtExam.NewRow()

                        drS2(0) = rw1(2).ToString
                        drS2(1) = IIf(CDec(cptr1 / 2) = CDec(cptr1 \ 2), "x", "").ToString
                        drS2(2) = "     " & MettreApost(rw1(0).ToString)
                        drS2(3) = InfosConform(TxtRefSoumis.Text, rw1(2).ToString)(0)
                        drS2(4) = InfosConform(TxtRefSoumis.Text, rw1(2).ToString)(1)

                        dtExam.Rows.Add(drS2)

                        If (InfosConform(TxtRefSoumis.Text, rw1(2).ToString)(0) <> "Conforme") Then
                            TampVerdict = False
                            If (rw1(1).ToString = "OUI") Then
                                VerdictVerouille = True
                            End If
                        End If

                        If (InfosConform(TxtRefSoumis.Text, rw1(2).ToString)(0).Replace(" ", "").Replace("-", "") = "") Then
                            ToutAnalyser = False
                        End If
                    Next
                Next

                GridTravail.DataSource = dtExam

                ViewTravail.Columns(0).Visible = False
                ViewTravail.Columns(1).Visible = False
                ViewTravail.Columns(2).Width = GridTravail.Width - 318
                ViewTravail.Columns(3).Width = 100
                ViewTravail.Columns(4).Width = 200

                'ViewTravail.Columns(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
                'ViewTravail.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
                'ViewTravail.Columns(2).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                ViewTravail.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

                ColorRowGrid(ViewTravail, "[CodeX]='x'", Color.LightGray, "Tahoma", 8, FontStyle.Regular, Color.Black)
                ColorRowGridAnal(ViewTravail, "[Code]='x'", Color.Navy, "Tahoma", 9, FontStyle.Bold, Color.White)
                ColorRowGridAnal(ViewTravail, "[Conforme]='Non Conforme'", Color.White, "Tahoma", 8, FontStyle.Regular, Color.Red, False)


                If (ToutAnalyser = True) Then

                    If (TampVerdict = True) Then
                        ChkConforme.Checked = True

                        ChkConforme.Properties.ReadOnly = True
                        ChkNonConforme.Properties.ReadOnly = True
                    Else
                        ChkNonConforme.Checked = True

                        If (VerdictVerouille = True) Then
                            ChkConforme.Properties.ReadOnly = True
                            ChkNonConforme.Properties.ReadOnly = True
                        Else
                            ChkConforme.Properties.ReadOnly = False
                            ChkNonConforme.Properties.ReadOnly = False
                        End If
                    End If

                End If

            End If

        End If

        If (ViewTravail.RowCount > 1) Then
            PanelVerdict.Enabled = True
        Else
            PanelVerdict.Enabled = False
        End If


    End Sub

    Private Function InfosConform(ByVal Soumis As String, ByVal Critere As String) As String()

        Dim Mention As String = "-"
        Dim Comment As String = "-"
        Dim ValRet As String = ""
        query = "select Mention,Commentaire from T_SoumisFournisConformTech where RefConformTech='" & Critere & "' and RefSoumis='" & Soumis & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            Mention = rw(0).ToString
            Comment = MettreApost(rw(1).ToString)
        Next
        ValRet = Mention & "#" & Comment
        Return ValRet.Split("#"c)

    End Function

    Private Sub GridBilan_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridBilan.MouseUp
        If (ViewBilan.RowCount > 0) Then
            ChoixContext()
        Else
            ContextMenuStrip1.Items(0).Enabled = False
            ContextMenuStrip1.Items(1).Enabled = False
            ContextMenuStrip1.Items(3).Enabled = False
            ContextMenuStrip1.Items(5).Enabled = False
            ContextMenuStrip1.Items(7).Enabled = False
            ContextMenuStrip1.Items(9).Enabled = False
        End If
    End Sub

    Private Sub GridPost_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridPost.MouseUp
        If (ViewPost.RowCount > 0) Then
            ChoixContext()
        Else
            ContextMenuStrip1.Items(0).Enabled = False
            ContextMenuStrip1.Items(1).Enabled = False
            ContextMenuStrip1.Items(3).Enabled = False
            ContextMenuStrip1.Items(5).Enabled = False
            ContextMenuStrip1.Items(7).Enabled = False
            ContextMenuStrip1.Items(9).Enabled = False
        End If
    End Sub

    Private Sub GridDetail_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridDetail.MouseUp
        If (ViewDetail.RowCount > 0) Then
            ChoixContext()
        Else
            ContextMenuStrip1.Items(0).Enabled = False
            ContextMenuStrip1.Items(1).Enabled = False
            ContextMenuStrip1.Items(3).Enabled = False
            ContextMenuStrip1.Items(5).Enabled = False
            ContextMenuStrip1.Items(7).Enabled = False
            ContextMenuStrip1.Items(9).Enabled = False
        End If
    End Sub

    Private Sub GridPrelim_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridPrelim.MouseUp
        If (ViewPrelim.RowCount > 0) Then
            ChoixContext()
        Else
            ContextMenuStrip1.Items(0).Enabled = False
            ContextMenuStrip1.Items(1).Enabled = False
            ContextMenuStrip1.Items(3).Enabled = False
            ContextMenuStrip1.Items(5).Enabled = False
            ContextMenuStrip1.Items(7).Enabled = False
            ContextMenuStrip1.Items(9).Enabled = False
        End If
    End Sub

    Private Sub ChoixContext()

        If (EtapeActuelle = "Analyse" Or EtapeActuelle = "ExamDetail") Then

            ContextMenuStrip1.Items(0).Enabled = False
            ContextMenuStrip1.Items(1).Enabled = False
            ContextMenuStrip1.Items(3).Enabled = False
            ContextMenuStrip1.Items(5).Enabled = False
            ContextMenuStrip1.Items(7).Enabled = True
            ContextMenuStrip1.Items(9).Enabled = False

        ElseIf (EtapeActuelle = "ExamPrelim") Then

            ContextMenuStrip1.Items(0).Enabled = False
            ContextMenuStrip1.Items(1).Enabled = False
            ContextMenuStrip1.Items(3).Enabled = False
            ContextMenuStrip1.Items(5).Enabled = False
            ContextMenuStrip1.Items(7).Enabled = False
            ContextMenuStrip1.Items(9).Enabled = True

        Else

            ContextMenuStrip1.Items(0).Enabled = False
            ContextMenuStrip1.Items(1).Enabled = False
            ContextMenuStrip1.Items(3).Enabled = False
            ContextMenuStrip1.Items(5).Enabled = False
            ContextMenuStrip1.Items(7).Enabled = False
            ContextMenuStrip1.Items(9).Enabled = False

        End If

    End Sub

    Private Sub GridTravail_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridTravail.MouseUp

        If (ViewTravail.RowCount > 0) Then

            DrX = ViewTravail.GetDataRow(ViewTravail.FocusedRowHandle)

            If (EtapeActuelle = "") Then
                If (TxtTypeMarche.Text = "Fournitures") Then
                    If (DrX(0).ToString <> "x") Then
                        CodeActuel = DrX(0).ToString
                        SpecDemande = DrX(6).ToString
                        ValeurActuelle = DrX(3).ToString
                        ContextMenuStrip1.Items(0).Enabled = True
                        ContextMenuStrip1.Items(1).Enabled = True
                        ContextMenuStrip1.Items(3).Enabled = False
                        ContextMenuStrip1.Items(5).Enabled = False
                    Else
                        ValeurActuelle = ""
                        ContextMenuStrip1.Items(0).Enabled = False
                        ContextMenuStrip1.Items(1).Enabled = False
                        If (Mid(DrX(4).ToString, 1, 4).ToLower = "prix") Then
                            ContextMenuStrip1.Items(3).Enabled = False
                        Else
                            ContextMenuStrip1.Items(3).Enabled = True
                        End If
                        ContextMenuStrip1.Items(5).Enabled = False
                    End If

                Else
                    ContextMenuStrip1.Items(0).Enabled = False
                    ContextMenuStrip1.Items(1).Enabled = False
                    ContextMenuStrip1.Items(3).Enabled = False
                    ContextMenuStrip1.Items(5).Enabled = False

                    If (DrX(0).ToString <> "x") Then
                        CodeActuel = DrX(0).ToString
                        ValeurActuelle = DrX(3).ToString
                        If (ValeurActuelle = "Conforme") Then
                            ContextMenuStrip1.Items(1).Enabled = True

                        ElseIf (ValeurActuelle = "Non Conforme") Then
                            ContextMenuStrip1.Items(0).Enabled = True

                        Else
                            ContextMenuStrip1.Items(1).Enabled = True
                            ContextMenuStrip1.Items(0).Enabled = True
                        End If
                        ContextMenuStrip1.Items(5).Enabled = True
                        'Else
                        '    ContextMenuStrip1.Items(0).Enabled = False
                        '    ContextMenuStrip1.Items(1).Enabled = False
                        '    ContextMenuStrip1.Items(3).Enabled = False
                        '    ContextMenuStrip1.Items(5).Enabled = False

                    End If

                End If

                ContextMenuStrip1.Items(7).Enabled = False
                ContextMenuStrip1.Items(9).Enabled = False

            End If


        End If

    End Sub

    Private Sub AccepterAvecCommentaire_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AccepterAvecCommentaire.Click
        If (TxtTypeMarche.Text = "Fournitures") Then

            Accord = "Conforme"
            ReponseDialog = EtapeActuelle
            ExceptRevue = ""
            ExceptRevue2 = ""
            AccordCommentaire.ShowDialog()

            If (ReponseDialog <> "") Then
                If SpecDemande = "oui" Then
                    VerifCaract(CodeActuel, TxtRefSoumis.Text)
                ElseIf SpecDemande = "non" Then
                    VerifCaractPropose(CodeActuel, TxtRefSoumis.Text)
                End If
                If (ExceptRevue <> "OUI" And ExceptRevue2 <> "OUI") Then

                    If SpecDemande = "oui" Then
                        query = "update T_SoumisCaractFournit set MentionValeur='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefSpecCaract='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                        ExecuteNonQuery(query)
                    ElseIf SpecDemande = "non" Then
                        query = "update t_soumiscaractfournitsupl set MentionValeur='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefSpecCaract='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                        ExecuteNonQuery(query)
                    End If
                Else

                    If (ExceptRevue = "OUI") Then        'Cas similaires

                        For k As Integer = 0 To ViewTravail.RowCount - 1
                            If (dtExam.Rows(k).Item(3).ToString = "...") Then
                                Dim laRef As String = dtExam.Rows(k).Item(0).ToString

                                VerifCaract(laRef, TxtRefSoumis.Text)
                                query = "update T_SoumisCaractFournit set MentionValeur='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefSpecCaract='" & laRef & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                                ExecuteNonQuery(query)
                            Else
                            End If
                        Next
                        ExceptRevue = ""

                    End If

                    If (ExceptRevue2 = "OUI") Then       'toute la rubrique

                        Dim CodeRubrique As String = ""
                        Dim CodeTrouve As Boolean = False
                        Dim PosRow As Decimal = ViewTravail.GetDataSourceRowIndex(ViewTravail.FocusedRowHandle)

                        While CodeTrouve = False
                            PosRow = PosRow - 1
                            If (dtExam.Rows(PosRow).Item(0).ToString = "x") Then
                                CodeTrouve = True
                                Dim CodePart() As String = dtExam.Rows(PosRow).Item(1).ToString.Split(":"c)
                                CodeRubrique = CodePart(0).Replace(" ", "")
                            End If
                        End While

                        Dim RefRub(100) As String
                        Dim cpt1 As Decimal = 0
                        query = "select C.RefSpecCaract from T_SpecTechFourniture as F,T_SpecTechCaract as C where F.RefSpecFournit=C.RefSpecFournit and F.NumeroDAO='" & CmbNumDoss.Text & "' and F.CodeFournit='" & CodeRubrique & "' and F.CodeLot='" & CmbNumLot.Text & "'"
                        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw As DataRow In dt0.Rows
                            RefRub(cpt1) = rw(0).ToString
                            cpt1 += 1
                        Next

                        For k As Integer = 0 To cpt1 - 1

                            VerifCaract(RefRub(k), TxtRefSoumis.Text)



                            query = "update T_SoumisCaractFournit set MentionValeur='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefSpecCaract='" & RefRub(k) & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                            ExecuteNonQuery(query)


                        Next

                        ExceptRevue2 = ""
                    End If

                End If


                ReponseDialog = ""
                ExceptRevue = ""
                ExceptRevue2 = ""
            End If

            Accord = ""
            ChargerGridExam(EtapeActuelle)

        Else           'Travaux *************************

            Accord = "Conforme"
            ReponseDialog = EtapeActuelle
            ExceptRevue = ""
            ExceptRevue2 = ""
            AccordCommentaire.ShowDialog()

            VerifItem(CodeActuel, TxtRefSoumis.Text)

            If (ReponseDialog <> "") Then

                If (ExceptRevue <> "OUI" And ExceptRevue2 <> "OUI") Then


                    query = "update T_SoumisFournisConformTech set Mention='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "'  where RefConformTech='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                    ExecuteNonQuery(query)


                Else

                    If (ExceptRevue2 = "OUI") Then       'toute la rubrique

                        Dim CodeRubrique As String = ""
                        Dim CodeTrouve As Boolean = False
                        Dim PosRow As Decimal = ViewTravail.GetDataSourceRowIndex(ViewTravail.FocusedRowHandle)

                        While CodeTrouve = False
                            PosRow = PosRow - 1
                            If (dtExam.Rows(PosRow).Item(0).ToString = "x") Then
                                CodeTrouve = True
                                CodeRubrique = dtExam.Rows(PosRow).Item(1).ToString
                            End If
                        End While

                        Dim RefRub(100) As String
                        Dim cpt1 As Decimal = 0
                        query = "select RefConformTech from T_DAO_ConformTech where RefConformMere='" & CodeRubrique & "' and NumeroDAO='" & CmbNumDoss.Text & "'"
                        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw As DataRow In dt0.Rows
                            RefRub(cpt1) = rw(0).ToString
                            cpt1 += 1
                        Next

                        For k As Integer = 0 To cpt1 - 1

                            VerifItem(RefRub(k), TxtRefSoumis.Text)



                            query = "update T_SoumisFournisConformTech set Mention='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefConformTech='" & RefRub(k) & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                            ExecuteNonQuery(query)


                        Next

                        ExceptRevue2 = ""
                    End If

                End If


                ReponseDialog = ""
                ExceptRevue = ""
                ExceptRevue2 = ""
            End If

            Accord = ""
            ChargerGridExam(EtapeActuelle)


        End If


    End Sub

    Private Sub AccepterSansCommentaire_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AccepterSansCommentaire.Click
        If (TxtTypeMarche.Text = "Fournitures") Then
            Accord = "Conforme"
            If SpecDemande = "oui" Then
                VerifCaract(CodeActuel, TxtRefSoumis.Text)
                query = "update T_SoumisCaractFournit set MentionValeur='" & Accord & "', Commentaire='' where RefSpecCaract='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                ExecuteNonQuery(query)
            ElseIf SpecDemande = "non" Then
                VerifCaractPropose(CodeActuel, TxtRefSoumis.Text)
                query = "update t_soumiscaractfournitsupl set MentionValeur='" & Accord & "', Commentaire='' where RefSpecCaract='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                ExecuteNonQuery(query)
            End If
            Accord = ""
            ChargerGridExam(EtapeActuelle)

        Else                    'Travaux ************************

            Accord = "Conforme"

            VerifItem(CodeActuel, TxtRefSoumis.Text)


            query = "update T_SoumisFournisConformTech set Mention='" & Accord & "', Commentaire='' where RefConformTech='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
            ExecuteNonQuery(query)


            Accord = ""
            ChargerGridExam(EtapeActuelle)

        End If

    End Sub

    Private Sub RejeterAvecCommentaire_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RejeterAvecCommentaire.Click

        If (TxtTypeMarche.Text = "Fournitures") Then

            Accord = "Non Conforme"
            ReponseDialog = EtapeActuelle
            ExceptRevue = ""
            ExceptRevue2 = ""
            AccordCommentaire.ShowDialog()

            If (ReponseDialog <> "") Then

                If SpecDemande = "oui" Then
                    VerifCaract(CodeActuel, TxtRefSoumis.Text)
                ElseIf SpecDemande = "non" Then
                    VerifCaractPropose(CodeActuel, TxtRefSoumis.Text)
                End If
                If (ExceptRevue <> "OUI" And ExceptRevue2 <> "OUI") Then
                    If SpecDemande = "oui" Then
                        query = "update T_SoumisCaractFournit set MentionValeur='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefSpecCaract='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                        ExecuteNonQuery(query)
                    ElseIf SpecDemande = "non" Then
                        query = "update t_soumiscaractfournitsupl set MentionValeur='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefSpecCaract='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                        ExecuteNonQuery(query)
                    End If
                Else

                    If (ExceptRevue = "OUI") Then        'Cas similaires

                        For k As Integer = 0 To ViewTravail.RowCount - 1
                            If (dtExam.Rows(k).Item(3).ToString = "...") Then
                                Dim laRef As String = dtExam.Rows(k).Item(0).ToString

                                VerifCaract(laRef, TxtRefSoumis.Text)


                                query = "update T_SoumisCaractFournit set MentionValeur='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefSpecCaract='" & laRef & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                                ExecuteNonQuery(query)


                            Else


                            End If
                        Next
                        ExceptRevue = ""

                    End If

                    If (ExceptRevue2 = "OUI") Then       'toute la rubrique

                        Dim CodeRubrique As String = ""
                        Dim CodeTrouve As Boolean = False
                        Dim PosRow As Decimal = ViewTravail.GetDataSourceRowIndex(ViewTravail.FocusedRowHandle)

                        While CodeTrouve = False
                            PosRow = PosRow - 1
                            If (dtExam.Rows(PosRow).Item(0).ToString = "x") Then
                                CodeTrouve = True
                                Dim CodePart() As String = dtExam.Rows(PosRow).Item(1).ToString.Split(":"c)
                                CodeRubrique = CodePart(0).Replace(" ", "")
                            End If
                        End While

                        Dim RefRub(100) As String
                        Dim cpt1 As Decimal = 0
                        query = "select C.RefSpecCaract from T_SpecTechFourniture as F,T_SpecTechCaract as C where F.RefSpecFournit=C.RefSpecFournit and F.NumeroDAO='" & CmbNumDoss.Text & "' and F.CodeFournit='" & CodeRubrique & "'"
                        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw As DataRow In dt0.Rows
                            RefRub(cpt1) = rw(0).ToString
                            cpt1 += 1
                        Next

                        For k As Integer = 0 To cpt1 - 1

                            VerifCaract(RefRub(k), TxtRefSoumis.Text)


                            query = "update T_SoumisCaractFournit set MentionValeur='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefSpecCaract='" & RefRub(k) & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                            ExecuteNonQuery(query)


                        Next

                        ExceptRevue2 = ""
                    End If

                End If


                ReponseDialog = ""
                ExceptRevue = ""
                ExceptRevue2 = ""
            End If

            Accord = ""
            ChargerGridExam(EtapeActuelle)

        Else               'Travaux *********************

            Accord = "Non Conforme"
            ReponseDialog = EtapeActuelle
            ExceptRevue = ""
            ExceptRevue2 = ""
            AccordCommentaire.ShowDialog()

            VerifItem(CodeActuel, TxtRefSoumis.Text)

            If (ReponseDialog <> "") Then

                If (ExceptRevue <> "OUI" And ExceptRevue2 <> "OUI") Then


                    query = "update T_SoumisFournisConformTech set Mention='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefConformTech='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                    ExecuteNonQuery(query)


                Else

                    If (ExceptRevue2 = "OUI") Then       'toute la rubrique

                        Dim CodeRubrique As String = ""
                        Dim CodeTrouve As Boolean = False
                        Dim PosRow As Decimal = ViewTravail.GetDataSourceRowIndex(ViewTravail.FocusedRowHandle)

                        While CodeTrouve = False
                            PosRow = PosRow - 1
                            If (dtExam.Rows(PosRow).Item(0).ToString = "x") Then
                                CodeTrouve = True
                                CodeRubrique = dtExam.Rows(PosRow).Item(1).ToString
                            End If
                        End While

                        Dim RefRub(100) As String
                        Dim cpt1 As Decimal = 0
                        query = "select RefConformTech from T_DAO_ConformTech where RefConformMere='" & CodeRubrique & "' and NumeroDAO='" & CmbNumDoss.Text & "'"
                        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw As DataRow In dt0.Rows
                            RefRub(cpt1) = rw(0).ToString
                            cpt1 += 1
                        Next

                        For k As Integer = 0 To cpt1 - 1

                            VerifItem(RefRub(k), TxtRefSoumis.Text)


                            query = "update T_SoumisFournisConformTech set Mention='" & Accord & "', Commentaire='" & EnleverApost(ReponseDialog) & "' where RefConformTech='" & RefRub(k) & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                            ExecuteNonQuery(query)


                        Next

                        ExceptRevue2 = ""
                    End If

                End If


                ReponseDialog = ""
                ExceptRevue = ""
                ExceptRevue2 = ""
            End If

            Accord = ""
            ChargerGridExam(EtapeActuelle)


        End If

    End Sub

    Private Sub VerifCaract(ByVal Caract As String, ByVal Soumis As String)
        Dim ligneExist As Boolean = False
        query = "select * from T_SoumisCaractFournit where RefSpecCaract='" & Caract & "' and RefSoumis='" & Soumis & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            ligneExist = True
        Next
        If (ligneExist = False) Then

            Dim DatSet = New DataSet
            query = "select * from T_SoumisCaractFournit"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_SoumisCaractFournit")
            Dim DatTable = DatSet.Tables("T_SoumisCaractFournit")
            Dim DatRow = DatSet.Tables("T_SoumisCaractFournit").NewRow()

            DatRow("RefSpecCaract") = Caract
            DatRow("RefSoumis") = Soumis

            DatSet.Tables("T_SoumisCaractFournit").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_SoumisCaractFournit")
            DatSet.Clear()
            BDQUIT(sqlconn)
        End If
    End Sub
    Private Sub VerifCaractPropose(ByVal Caract As String, ByVal Soumis As String)
        Dim ligneExist As Boolean = False
        query = "select * from t_soumiscaractfournitsupl where RefSpecCaract='" & Caract & "' and RefSoumis='" & Soumis & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            ligneExist = True
        Next
        If (ligneExist = False) Then

            Dim DatSet = New DataSet
            query = "select * from t_soumiscaractfournitsupl"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "t_soumiscaractfournitsupl")
            Dim DatTable = DatSet.Tables("t_soumiscaractfournitsupl")
            Dim DatRow = DatSet.Tables("t_soumiscaractfournitsupl").NewRow()

            DatRow("RefSpecCaract") = Caract
            DatRow("RefSoumis") = Soumis

            DatSet.Tables("t_soumiscaractfournitsupl").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "t_soumiscaractfournitsupl")
            DatSet.Clear()
            BDQUIT(sqlconn)
        End If
    End Sub

    Private Sub VerifItem(ByVal ItemCible As String, ByVal Soumis As String)

        Dim ligneExist As Boolean = False
        query = "select * from T_SoumisFournisConformTech where RefConformTech='" & ItemCible & "' and RefSoumis='" & Soumis & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            ligneExist = True
            Exit For
        Next

        If (ligneExist = False) Then

            Dim DatSet = New DataSet
            query = "select * from T_SoumisFournisConformTech"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_SoumisFournisConformTech")
            Dim DatTable = DatSet.Tables("T_SoumisFournisConformTech")
            Dim DatRow = DatSet.Tables("T_SoumisFournisConformTech").NewRow()

            DatRow("RefConformTech") = ItemCible
            DatRow("RefSoumis") = Soumis

            DatSet.Tables("T_SoumisFournisConformTech").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_SoumisFournisConformTech")
            DatSet.Clear()
            BDQUIT(sqlconn)
        End If

    End Sub

    Private Sub RejeterSansCommentaire_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RejeterSansCommentaire.Click

        If (TxtTypeMarche.Text = "Fournitures") Then

            Accord = "Non Conforme"
            If SpecDemande = "oui" Then
                VerifCaract(CodeActuel, TxtRefSoumis.Text)
                query = "update T_SoumisCaractFournit set MentionValeur='" & Accord & "', Commentaire='' where RefSpecCaract='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                ExecuteNonQuery(query)
            ElseIf SpecDemande = "non" Then
                VerifCaractPropose(CodeActuel, TxtRefSoumis.Text)
                query = "update t_soumiscaractfournitsupl set MentionValeur='" & Accord & "', Commentaire='' where RefSpecCaract='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                ExecuteNonQuery(query)
            End If


            Accord = ""
            ChargerGridExam(EtapeActuelle)

        Else         'Travaux **********************

            Accord = "Non Conforme"

            VerifItem(CodeActuel, TxtRefSoumis.Text)


            query = "update T_SoumisFournisConformTech set Mention='" & Accord & "', Commentaire='' where RefConformTech='" & CodeActuel & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
            ExecuteNonQuery(query)


            Accord = ""
            ChargerGridExam(EtapeActuelle)

        End If

    End Sub

    Private Sub DéciderToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DéciderToolStripMenuItem.Click
        If (TxtTypeMarche.Text = "Fournitures") Then
            Dim vMin As Decimal = 10000000000000
            Dim vMax As Decimal = 0
            Dim vMoy As Decimal = 0
            Dim coef As Decimal = 0

            Dim PosRow As Decimal = ViewTravail.GetDataSourceRowIndex(ViewTravail.FocusedRowHandle)
            Dim ChapRub() As String = dtExam.Rows(PosRow).Item(1).ToString.Split(":"c)
            Dim CodeRub As String = ChapRub(0).Replace(" ", "")
            Dim RefRubrq As String = ""

            query = "select P.PrixUnitaire,T.RefSpecFournit from T_SpecTechFourniture as T,T_SoumisPrixFourniture as P where P.RefSpecFournit=T.RefSpecFournit and T.NumeroDAO='" & CmbNumDoss.Text & "' and T.CodeFournit='" & CodeRub & "' and P.PrixUnitaire<>'' and P.RefSoumis<>'" & TxtRefSoumis.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                RefRubrq = rw(1).ToString

                Dim valCompare As Decimal
                If (IsNumeric(rw(0).ToString) = True) Then
                    valCompare = CDec(rw(0).ToString)
                Else
                    valCompare = 0
                End If

                If (vMin > valCompare) Then vMin = valCompare
                If (vMax < valCompare) Then vMax = valCompare
                vMoy += valCompare
                coef += 1
            Next


            vMoy = Math.Round(vMoy / coef)

            AccordCommentaire.TxtValMin.Text = AfficherMonnaie(vMin.ToString)
            AccordCommentaire.TxtValMax.Text = AfficherMonnaie(vMax.ToString)
            AccordCommentaire.TxtValMoy.Text = AfficherMonnaie(vMoy.ToString)

            ReponseDialog = EtapeActuelle
            ExceptRevue = ""
            ExceptRevue2 = ""
            AccordCommentaire.ShowDialog()

            If (ReponseDialog <> "") Then
                Dim Validation As String = ""
                For k As Integer = 0 To GridViewComJugmt.RowCount - 1
                    Dim partValid() As String = dt.rows(k).item(0).ToString.Split("("c)
                    If (k > 0) Then Validation = Validation & ";"
                    Validation = Validation & partValid(0)
                Next

                If (ExceptRevue2 = "OUI") Then


                    query = "update T_SoumisPrixFourniture set PrixUnitaire='" & AfficherMonnaie(ReponseDialog.Replace(" ", "")) & "', Decision='" & EnleverApost(Validation) & "' where RefSpecFournit='" & RefRubrq & "' and PrixUnitaire=''"
                    ExecuteNonQuery(query)


                Else


                    query = "update T_SoumisPrixFourniture set PrixUnitaire='" & AfficherMonnaie(ReponseDialog.Replace(" ", "")) & "', Decision='" & EnleverApost(Validation) & "' where RefSpecFournit='" & RefRubrq & "' and RefSoumis='" & TxtRefSoumis.Text & "'"
                    ExecuteNonQuery(query)


                End If

            End If

            ReponseDialog = ""
            ExceptRevue = ""
            ExceptRevue2 = ""
            ChargerGridExam(EtapeActuelle)

        End If

    End Sub

    Private Sub BtEnrgVerdict_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgVerdict.Click


        If (ChkConforme.Checked = True Or ChkNonConforme.Checked = True) Then

            If (TxtTypeMarche.Text = "Fournitures") Then

                Dim AnalyseOk As Boolean = True
                query = "select * from T_SoumisCaractFournit where RefSoumis='" & TxtRefSoumis.Text & "' and MentionValeur=''"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    AnalyseOk = False
                    Exit For
                Next


                If (AnalyseOk = True) Then

                    If (ViewTravail.RowCount > 0) Then

                        Dim RepVerdict As MsgBoxResult = MsgBox("Confirmation du verdict.", MsgBoxStyle.OkCancel)

                        If (RepVerdict = MsgBoxResult.Ok) Then


                            query = "update T_SoumissionFournisseur set ConformiteTechnique='" & IIf(ChkConforme.Checked = True, "OUI", "NON").ToString & "' where RefSoumis='" & TxtRefSoumis.Text & "'"
                            ExecuteNonQuery(query)


                            MsgBox("Verdict enregistré avec succès!", MsgBoxStyle.Information)
                            ChkConforme.Checked = False
                            ChkNonConforme.Checked = False
                            PanelVerdict.Enabled = False
                            dtExam.Columns.Clear()
                            dtExam.Rows.Clear()
                            GridTravail.Refresh()
                            CmbSoumis.Properties.Items.Remove(CmbSoumis.Text)

                            TxtAdresseSoumis.Text = ""
                            CmbSoumis.Text = ""
                            'TxtLibelleLot.Text = ""
                            'CmbNumLot.Text = ""

                        End If

                    Else
                        MsgBox("Aucune information dans la grille!", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If

                Else
                    MsgBox("Analyse incomplète!", MsgBoxStyle.Information)
                    Exit Sub
                End If

                OffresTraitees()

            Else                 'Travaux **************************

                Dim AnalyseOk As Boolean = True
                'Dim CmdAO As MySqlCommand = sqlconn.CreateCommand
                'Dim ReaderAO As MySqlDataReader
                'CmdAO.CommandText = "select Commentaire from T_SoumisPrixItemDQE where RefSoumis='" & TxtRefSoumis.Text & "' and Mention=''"
                '
                'ReaderAO = CmdAO.ExecuteReader()
                'If ReaderAO.Read() Then
                '    If (ReaderAO.GetValue(0).ToString <> "Somme provisionnelle") Then
                '        AnalyseOk = False
                '    End If
                'End If
                'ReaderAO.Close()
                '

                For k As Integer = 0 To ViewTravail.RowCount - 1
                    If (dtExam.Rows(k).Item(0).ToString <> "x") Then
                        If (dtExam.Rows(k).Item(3).ToString <> "Conforme" And dtExam.Rows(k).Item(3).ToString <> "Non Conforme") Then
                            AnalyseOk = False
                            Exit For
                        End If
                    End If
                Next

                If (AnalyseOk = True) Then

                    If (ViewTravail.RowCount > 0) Then

                        Dim RepVerdict As MsgBoxResult = MsgBox("Confirmation du verdict.", MsgBoxStyle.OkCancel)

                        If (RepVerdict = MsgBoxResult.Ok) Then


                            query = "update T_SoumissionFournisseur set ConformiteTechnique='" & IIf(ChkConforme.Checked = True, "OUI", "NON").ToString & "' where RefSoumis='" & TxtRefSoumis.Text & "'"
                            ExecuteNonQuery(query)


                            MsgBox("Verdict enregistré avec succès!", MsgBoxStyle.Information)
                            ChkConforme.Checked = False
                            'ChkExhaustif.Checked = False
                            ChkNonConforme.Checked = False
                            'ChkNonExhaustif.Checked = False
                            PanelVerdict.Enabled = False
                            dtExam.Columns.Clear()
                            dtExam.Rows.Clear()
                            GridTravail.Refresh()
                            CmbSoumis.Properties.Items.Remove(CmbSoumis.Text)

                            TxtAdresseSoumis.Text = ""
                            CmbSoumis.Text = ""
                            'TxtLibelleLot.Text = ""
                            'CmbNumLot.Text = ""

                        End If

                    Else
                        MsgBox("Aucune information dans la grille!", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If

                Else
                    MsgBox("Analyse incomplète!", MsgBoxStyle.Information)
                    Exit Sub
                End If

                OffresTraitees()

            End If

        End If

    End Sub

    Private Sub OffresTraitees()

        If (CmbNumDoss.Text <> "") Then
            dtTraite.Columns.Clear()

            dtTraite.Columns.Add("Code", Type.GetType("System.String"))
            dtTraite.Columns.Add("Fournisseur", Type.GetType("System.String"))
            dtTraite.Columns.Add("Accepté pour Examen détaillé", Type.GetType("System.String"))
            'dtTraite.Columns.Add("Conformité administrative", Type.GetType("System.String"))
            dtTraite.Columns.Add("Montant lu", Type.GetType("System.String"))
            dtTraite.Columns.Add("Montant corrigé", Type.GetType("System.String"))
            dtTraite.Columns.Add("Classement", Type.GetType("System.String"))
            dtTraite.Columns.Add("Post qualifié", Type.GetType("System.String"))

            dtTraite.Rows.Clear()
            query = "select RefLot,CodeLot from T_LotDAO where NumeroDAO='" & CmbNumDoss.Text & "' order by CodeLot"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                Dim Resultat As Object() = GetSousLot(rw(1).ToString, CmbNumDoss.Text)
                Dim nbsouslot As Integer = Val(Resultat(0))
                Dim DrT = dtTraite.NewRow()
                If nbsouslot > 0 Then
                    Dim dt As DataTable = CType(Resultat(1), DataTable)
                    For Each rw2 As DataRow In dt.Rows
                        cmbSousLot.Properties.Items.Add(rw2("CodeSousLot").ToString)
                    Next
                Else
                    DrT(0) = "x"
                    DrT(1) = "Lot N°" & rw(1).ToString
                End If

                dtTraite.Rows.Add(DrT)

                query = "select F.NomFournis,S.RefSoumis,S.AcceptationExamDetaille,S.ConformiteTechnique,S.MontantPropose,S.PrixCorrigeOffre,S.RangExamDetaille,S.ExamPQValide from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and S.RefLot='" & rw(0).ToString & "' order by F.NomFournis"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows
                    If (rw1(3).ToString <> "") Then

                        Dim DrT2 = dtTraite.NewRow()
                        DrT2(0) = rw1(1).ToString
                        DrT2(1) = "  - " & MettreApost(rw1(0).ToString)
                        DrT2(2) = IIf(rw1(2).ToString <> "", IIf(rw1(2).ToString = "OUI", "OUI", "NON").ToString, "-").ToString
                        'DrT2(3) = IIf(rw1(3).ToString <> "", IIf(rw1(3).ToString = "OUI", "Conforme", "Non Conforme").ToString, "-").ToString
                        If (rw1(2).ToString = "OUI") Then
                            DrT2(3) = AfficherMonnaie(rw1(4).ToString)
                            DrT2(4) = AfficherMonnaie(rw1(5).ToString)
                            DrT2(5) = IIf(rw1(6).ToString <> "0", rw1(6).ToString & IIf(rw1(6).ToString = "1", "er", "ème").ToString, "-").ToString
                            DrT2(6) = rw1(7).ToString
                        Else
                            DrT2(3) = "-"
                            DrT2(4) = "-"
                            DrT2(5) = "-"
                            DrT2(6) = "-"
                        End If

                        dtTraite.Rows.Add(DrT2)
                    End If
                Next
            Next

            GridTraité.DataSource = dtTraite

            ViewTraité.Columns(0).Visible = False
            ViewTraité.Columns(1).Width = 250
            ViewTraité.Columns(2).Width = 150
            ViewTraité.Columns(3).Width = 150
            ViewTraité.Columns(4).Width = 150
            ViewTraité.Columns(5).Width = 150
            ViewTraité.Columns(6).Width = 150

            ViewTraité.Columns(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewTraité.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

            ViewTraité.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center                  'Drawing.StringAlignment.Center
            ViewTraité.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewTraité.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewTraité.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewTraité.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            ColorRowGrid(ViewTraité, "[Code]='x'", Color.LightGray, "Tahoma", 8, FontStyle.Bold, Color.Black)
            ColorRowGridAnal(ViewTraité, "[Post qualifié]='NON'", Color.White, "Tahoma", 8, FontStyle.Strikeout, Color.Black, False)
            ColorRowGridAnal(ViewTraité, "[Accepté pour Examen détaillé]='NON'", Color.White, "Tahoma", 8, FontStyle.Strikeout, Color.Black, False)
        End If

    End Sub

    Private Sub BtOuvFerm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtOuvFerm.Click
        If (BtOuvFerm.Text = "<<") Then
            BtOuvFerm.Text = ">>"
            GbTraites.Width = 188 + GbTraitement.Width - 20
        Else
            BtOuvFerm.Text = "<<"
            GbTraites.Width = 188
        End If
    End Sub

    Private Sub ChkConforme_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkConforme.CheckedChanged
        If (ChkConforme.Checked = True Or ChkNonConforme.Checked = True) Then
            BtEnrgVerdict.Enabled = True
        Else
            BtEnrgVerdict.Enabled = False
        End If
    End Sub

    Private Sub ChkNonConforme_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkNonConforme.CheckedChanged
        If (ChkConforme.Checked = True Or ChkNonConforme.Checked = True) Then
            BtEnrgVerdict.Enabled = True
        Else
            BtEnrgVerdict.Enabled = False
        End If
    End Sub

    Private Sub ExaminerToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExaminerToolStripMenuItem.Click

        If (EtapeActuelle = "Analyse") Then

            If (ViewPrelim.RowCount > 0) Then
                DrX = ViewPrelim.GetDataRow(ViewPrelim.FocusedRowHandle)

                CodeActuel = DrX(0).ToString
                ReponseDialog = CodeActuel
                SpecDemande = DrX(6).ToString
                ExceptRevue = DrX(1).ToString
                ExamenDetaille.ShowDialog()
                If (ReponseDialog = "") Then
                    ChargerGridExam(EtapeActuelle)
                    OffresTraitees()
                End If

            End If

        ElseIf (EtapeActuelle = "ExamDetail") Then

            If (ViewPost.RowCount > 0) Then
                DrX = ViewPost.GetDataRow(ViewPost.FocusedRowHandle)

                CodeActuel = DrX(1).ToString
                ReponseDialog = CodeActuel
                ExceptRevue = DrX(2).ToString
                ExamPostQualif.ShowDialog()
                If (ReponseDialog = "") Then
                    ChargerExamPostQualif()
                    OffresTraitees()
                End If

            End If

        End If

    End Sub

    Private Sub CalculerToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CalculerToolStripMenuItem.Click

        If (ViewDetail.RowCount > 0) Then
            DrX = ViewDetail.GetDataRow(ViewDetail.FocusedRowHandle)

            If (EtapeActuelle = "ExamPrelim") Then

                CodeActuel = DrX(1).ToString
                ReponseDialog = CodeActuel
                ExceptRevue = DrX(2).ToString
                CalculDetaille.ShowDialog()

                If (ReponseDialog = "") Then
                    ChargerGridExam(EtapeActuelle)

                    Dim ToutTraite As Boolean = True
                    For k As Integer = 0 To ViewDetail.RowCount - 1
                        If ((dtExam.Rows(k).Item(13).ToString.Replace(" ", "")).Replace("-", "") = "0") Then
                            ToutTraite = False
                            Exit For
                        End If
                    Next

                    If (ToutTraite = True) Then
                        Classement()
                        ChargerGridExam(EtapeActuelle)
                        OffresTraitees()
                    End If


                End If

            End If

        End If

    End Sub

    Private Sub Classement()

        Dim lesRef(500) As String
        Dim lesPrix(500) As String
        Dim Tamp As String = ""
        Dim nbSoum As Decimal = 0

        query = "select S.RefSoumis,S.PrixCorrigeOffre from T_SoumissionFournisseur as S,T_Fournisseur as F where S.CodeFournis=F.CodeFournis and F.NumeroDAO='" & CmbNumDoss.Text & "' and S.CodeLot='" & CmbNumLot.Text & "' and S.AcceptationExamDetaille='OUI' and S.PrixCorrigeOffre<>'0'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows

            lesRef(nbSoum) = rw(0).ToString
            lesPrix(nbSoum) = rw(1).ToString.Replace(" ", "")
            nbSoum += 1
        Next

        'For i As Integer = 0 To nbSoum - 2
        '    For j As Integer = i + 1 To nbSoum - 1
        '        If (IIf(CDec(lesPrix(j)) = "", 0, CDec(lesPrix(j))) < CDec(lesPrix(i))) Then
        '            Tamp = lesPrix(i)
        '            lesPrix(i) = lesPrix(j)
        '            lesPrix(j) = Tamp

        '            Tamp = lesRef(i)
        '            lesRef(i) = lesRef(j)
        '            lesRef(j) = Tamp
        '        End If
        '    Next
        'Next

        For k As Integer = 0 To nbSoum - 1

            Dim rang As Decimal = 0
            rang = k + 1


            query = "update T_SoumissionFournisseur set RangExamDetaille='" & rang.ToString & "' where RefSoumis='" & lesRef(k) & "'"
            ExecuteNonQuery(query)

        Next

        MsgBox("Classement terminé avec succès!", MsgBoxStyle.Information)


    End Sub

    Private Sub ClassementPostQualif()

        For y As Decimal = 1 To CInt(TxtNbLot.Text.Replace(" ", ""))

            Dim lesRef(500) As String
            Dim lesPrix(500) As String
            Dim Tamp As String = ""
            Dim nbSoum As Decimal = 0

            query = "select S.RefSoumis,S.PrixCorrigeOffre from T_SoumissionFournisseur as S,T_Fournisseur as F where S.CodeFournis=F.CodeFournis and F.NumeroDAO='" & CmbNumDoss.Text & "' and S.CodeLot='" & y.ToString & "' and S.RangExamDetaille<>'0' and S.ExamPQValide='OUI'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                lesRef(nbSoum) = rw(0).ToString
                lesPrix(nbSoum) = rw(1).ToString.Replace(" ", "")
                nbSoum += 1
            Next

            For i As Integer = 0 To nbSoum - 2
                For j As Integer = i + 1 To nbSoum - 1
                    If (CDec(lesPrix(j)) < CDec(lesPrix(i))) Then
                        Tamp = lesPrix(i)
                        lesPrix(i) = lesPrix(j)
                        lesPrix(j) = Tamp

                        Tamp = lesRef(i)
                        lesRef(i) = lesRef(j)
                        lesRef(j) = Tamp
                    End If
                Next
            Next

            Dim Choix As String = ""
            Dim RaisonChoix As String = ""
            For k As Integer = 0 To nbSoum - 1

                Dim rang As Decimal = 0
                rang = k + 1


                query = "update T_SoumissionFournisseur set RangPostQualif='" & rang.ToString & "', Selectionne='" & Choix & "',MotifSelect='" & RaisonChoix & "'  where RefSoumis='" & lesRef(k) & "'"
                ExecuteNonQuery(query)
                If (k = 0) Then
                    Choix = "OUI"
                    RaisonChoix = "Classement évaluateurs"
                Else
                    Choix = "NON"
                    RaisonChoix = "Classement évaluateurs"
                End If


            Next

        Next

        MsgBox("Classement post qualification terminé avec succès!", MsgBoxStyle.Information)

    End Sub

    Private Sub BilanExamOffres()

        AfficherGrid(EtapeActuelle)

        If (CmbNumDoss.Text <> "") Then
            dtExam.Columns.Clear()

            dtExam.Columns.Add("Code", Type.GetType("System.String"))
            dtExam.Columns.Add("Soumissionnaire", Type.GetType("System.String"))
            dtExam.Columns.Add("Prix de l'offre", Type.GetType("System.String"))
            dtExam.Columns.Add("Prix en lettre", Type.GetType("System.String"))
            dtExam.Columns.Add("Classement", Type.GetType("System.String"))

            dtExam.Rows.Clear()
            query = "select RefLot,CodeLot from T_LotDAO where NumeroDAO='" & CmbNumDoss.Text & "' order by CodeLot"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                Dim DrT = dtExam.NewRow()

                DrT(0) = "x"
                DrT(1) = "Lot N°" & rw(1).ToString

                dtExam.Rows.Add(DrT)
                query = "select F.NomFournis,S.RefSoumis,S.PrixCorrigeOffre,S.RangPostQualif from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and S.RefLot='" & rw(0).ToString & "' and S.ExamPQValide='OUI' order by S.RangPostQualif"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows

                    Dim DrT2 = dtExam.NewRow()
                    DrT2(0) = rw1(1).ToString
                    DrT2(1) = "  - " & MettreApost(rw1(0).ToString)
                    DrT2(2) = AfficherMonnaie(rw1(2).ToString.Replace(" ", "")) & "  HT"
                    DrT2(3) = MontantLettre(rw1(2).ToString.Replace(" ", ""))
                    DrT2(4) = rw1(3).ToString & IIf(rw1(3).ToString = "1", "er", "ème").ToString

                    dtExam.Rows.Add(DrT2)
                Next
            Next
            GridBilan.DataSource = dtExam

            ViewBilan.Columns(0).Visible = False
            ViewBilan.Columns(1).Width = 250
            ViewBilan.Columns(2).Width = 150
            ViewBilan.Columns(3).Width = GridBilan.Width - 518
            ViewBilan.Columns(4).Width = 100

            ViewBilan.Columns(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewBilan.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

            ViewBilan.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far                  'Drawing.StringAlignment.Center
            ViewBilan.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            ColorRowGrid(ViewBilan, "[Code]='x'", Color.LightGray, "Tahoma", 8, FontStyle.Bold, Color.Black)
            ColorRowGridAnal(ViewBilan, "[Classement]='1er'", Color.White, "Tahoma", 8, FontStyle.Bold, Color.Navy, False)
            'ColorRowGridAnal(ViewBilan, "[Classement]<>'1er'", Color.White, "Tahoma", 8, FontStyle.Strikeout, Color.Black, False)
        End If



    End Sub

    Private Sub CmbNumLotAttrib_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumLotAttrib.SelectedValueChanged
        query = "select F.NomFournis,S.RefSoumis,S.Attribue from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and S.CodeLot='" & CmbNumLotAttrib.Text & "' and S.Selectionne='OUI' and F.NumeroDAO='" & CmbNumDoss.Text & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        If dt0.Rows.Count > 0 Then
            Dim rw As DataRow = dt0.Rows(0)
            BtOuiAttrib.Enabled = True
            BtSuivantAttrib.Enabled = True

            TxtSoumisAttrib.Text = MettreApost(rw(0).ToString)
            RefSoumisFavoris.Text = rw(1).ToString

            If (rw(2).ToString = "OUI") Then
                BtOuiAttrib.Enabled = False
                BtSuivantAttrib.Enabled = False
            Else
                BtOuiAttrib.Enabled = True
                BtSuivantAttrib.Enabled = True
            End If

        Else

            TxtSoumisAttrib.Text = "Aucun enregistrement trouvé!"
            BtOuiAttrib.Enabled = False
            BtSuivantAttrib.Enabled = False

        End If

    End Sub

    Private Sub BtOuiAttrib_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtOuiAttrib.Click

        ReponseDialog = ""
        ValiderAttributionMarche_Old.ShowDialog()
        If (ReponseDialog = "OK") Then


            query = "update T_SoumissionFournisseur set Attribue='OUI' where RefSoumis='" & RefSoumisFavoris.Text & "'"
            ExecuteNonQuery(query)


            MsgBox("Le marché a été attribuer avec succès!" & vbNewLine & "Vous avez la possibilité de consulter le marché.", MsgBoxStyle.Information)
            BtOuiAttrib.Enabled = False
            BtSuivantAttrib.Enabled = False
        End If

    End Sub

    Private Sub BtSuivantAttrib_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtSuivantAttrib.Click

        ReponseDialog = TxtSoumisAttrib.Text
        ExceptRevue = RefSoumisFavoris.Text
        RaisonAttribuerSuivant.ShowDialog()
        If (ReponseDialog <> "") Then
            ExceptRevue = CmbNumLotAttrib.Text

            DebutChargement(True, "Chargement du rapport d'évaluation en cours...")
            CmbNumLotAttrib.Text = ""
            TxtSoumisAttrib.Text = ""
            RefSoumisFavoris.Text = ""

            RapportEvaluation()
            RapportEvaluation("Enregistrer")

            FinChargement()

            CmbNumLotAttrib.Text = ExceptRevue
            If (TxtSoumisAttrib.Text = ReponseDialog) Then
                MsgBox("Changement du soumissionnaire favoris effectué avec succès!", MsgBoxStyle.Information)
                ReponseDialog = ""
                ExceptRevue = ""
            End If
        End If

    End Sub

    Private Sub FullFavoris_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FullCouverture.Click

        For Each Rapport In TabRapportEval.SelectedTabPage.Controls
            If (TypeOf (Rapport) Is CrystalDecisions.Windows.Forms.CrystalReportViewer) Then
                FullScreenReport.FullView.ReportSource = Rapport.ReportSource
                FullScreenReport.Text = TabRapportEval.SelectedTabPage.Text
            End If
        Next
        'As CrystalDecisions.Windows.Forms.CrystalReportViewer
        FullScreenReport.ShowDialog()


    End Sub

    Private Sub BtRapportEval_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtRapportEval.Click

        Dim Rep As MsgBoxResult = MsgBox("Voulez-vous imprimer le Rapport d'Evaluation?", MsgBoxStyle.YesNo)
        If (Rep = MsgBoxResult.Yes) Then
            RapportEvaluation("Imprimer")
        Else
            RapportEvaluation()
        End If

    End Sub

    Private Sub BtImpMarche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtImpMarche.Click

        TabRapportEval.Visible = False
        PnlEditionMarche.Visible = True
        TxtTypeExamen.Text = "EDITION DES MARCHES DU DOSSIER N° " & CmbNumDoss.Text
        FullCouverture.Visible = False

        CmbLotMarche.Properties.Items.Clear()
        For k As Integer = 1 To CInt(TxtNbLot.Text)
            CmbLotMarche.Properties.Items.Add(k.ToString)
        Next

    End Sub

    Private Sub InitFormMarche()

        'CmbLotMarche.Text = ""
        TxtLotMarche.Text = ""
        TxtRefLotMarche.Text = ""
        TxtCodeFournisMarche.Text = ""
        TxtRefSoumisMarche.Text = ""
        TxtNumeroMarche.Text = ""
        TxtFournisMarche.Text = ""
        TxtAdresseFournisMarche.Text = ""
        TxtContactFournisMarche.Text = ""
        TxtContribuable.Text = ""
        TxtRegCommerce.Text = ""
        TxtNomBanqueFournis.Text = ""
        TxtNumCompteFournis.Text = ""
        TxtMontantMarche.Text = ""
        TxtMontMarcheLettre.Text = ""
        TxtExecutionMarche.Text = ""
        TxtPrctCautionDef.Text = ""
        TxtMontCautionDef.Text = ""
        TxtPrctAvance.Text = ""
        TxtMontAvance.Text = ""
        TxtBailleurMarche.Text = ""
        TxtConventionMarche.Text = ""
        TxtImputBudgetaire.Text = ""

        LblAnPrec.Text = "(....)"
        LblAnCour.Text = "(....)"
        LblAnSuiv.Text = "(....)"

        For Each Ctrl In GroupControl2.Controls
            If (TypeOf (Ctrl) Is DevExpress.XtraEditors.TextEdit) Then
                Ctrl.Text = ""
            ElseIf (TypeOf (Ctrl) Is DevExpress.XtraEditors.CheckEdit) Then
                Ctrl.Checked = False
            End If
        Next

    End Sub

    Private Sub CmbLotMarche_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbLotMarche.SelectedValueChanged
        InitFormMarche()

        If (CmbLotMarche.Text <> "") Then
            query = "select LibelleLot,RefLot from T_LotDAO where NumeroDAO='" & CmbNumDoss.Text & "' and CodeLot='" & CmbLotMarche.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            If dt0.Rows.Count > 0 Then
                Dim rw As DataRow = dt0.Rows(0)
                TxtLotMarche.Text = IIf(rw(0).ToString <> "Lot N°" & CmbLotMarche.Text, MettreApost(rw(0).ToString), TxtLibelleDoss.Text & " (" & rw(0).ToString & ")")
                TxtRefLotMarche.Text = rw(1).ToString

                query = "select BanqueCaution,DelaiLivraison,PrixCorrigeOffre,CodeFournis,RefSoumis,NumCompteBanque from T_SoumissionFournisseur where RefLot='" & rw(1).ToString & "' and Attribue='OUI'"
                dt0 = ExcecuteSelectQuery(query)
                If dt0.Rows.Count > 0 Then
                    Dim rw1 As DataRow = dt0.Rows(0)
                    TxtRefSoumisMarche.Text = rw1(4).ToString
                    Dim NomBank As String = ""
                    query = "select NomCompletBanque from T_Banque where CodeProjet='" & ProjetEnCours & "' and CodeBanque='" & rw1(0).ToString & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    If dt0.Rows.Count > 0 Then
                        NomBank = MettreApost(dt0.Rows(0).Item(0).ToString)
                    End If

                    TxtNomBanqueFournis.Text = MettreApost(rw1(0).ToString) & " (" & NomBank & ")"
                    TxtNumCompteFournis.Text = rw1(5).ToString
                    TxtExecutionMarche.Text = rw1(1).ToString
                    TxtMontantMarche.Text = AfficherMonnaie(rw1(2).ToString.Replace(" ", ""))
                    TxtCodeFournisMarche.Text = rw1(3).ToString

                    TxtTotalReparti.Text = AfficherMonnaie(rw1(2).ToString.Replace(" ", ""))

                    ' infos fournis *****************
                    query = "select NomFournis,PaysFournis,AdresseCompleteFournis,TelFournis,FaxFournis,MailFournis,CompteContribuableFournis,RegistreCommerceFournis,NomRep,AdresseRep,TelRep from T_Fournisseur where CodeFournis='" & rw1(3).ToString & "' and CodeProjet='" & ProjetEnCours & "' and NumeroDAO='" & CmbNumDoss.Text & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    If dt0.Rows.Count > 0 Then
                        Dim rw2 As DataRow = dt0.Rows(0)
                        TxtFournisMarche.Text = MettreApost(rw2(0).ToString & " (" & rw2(1).ToString & ")")
                        TxtAdresseFournisMarche.Text = MettreApost(rw2(2).ToString)
                        TxtContactFournisMarche.Text = IIf(rw2(3).ToString <> "", "Tel : " & rw2(3).ToString & "  ", "") & IIf(rw2(4).ToString <> "", "Fax : " & rw2(4).ToString & "  ", "") & IIf(rw2(5).ToString <> "", "E-mail : " & rw2(5).ToString, "")
                        TxtContribuable.Text = rw2(6).ToString
                        TxtRegCommerce.Text = rw2(7).ToString
                        TxtNomRepLegal.Text = MettreApost(rw2(8).ToString)
                        TxtBpRepLegal.Text = MettreApost(rw2(9).ToString)
                        TxtContactRepLegal.Text = rw2(10).ToString
                    End If
                End If
            End If
            query = "select B.InitialeBailleur,B.NomBailleur,B.CodeBailleur from T_Bailleur as B,T_Marche as M where M.NumeroDAO='" & CmbNumDoss.Text & "' and M.CodeProjet='" & ProjetEnCours & "' and B.InitialeBailleur=M.InitialeBailleur and B.CodeProjet=M.CodeProjet"
            dt0 = ExcecuteSelectQuery(query)
            If dt0.Rows.Count > 0 Then
                Dim rw1 As DataRow = dt0.Rows(0)
                TxtBailleurMarche.Text = MettreApost(rw1(0).ToString & " (" & rw1(1).ToString & ")")
                If (rw1(0).ToString = "ETAT") Then
                    RdTresorAnPrec.Enabled = True
                    RdTresorAnCour.Enabled = True
                    RdTresorAnSuiv.Enabled = True
                Else
                    RdTresorAnPrec.Enabled = False
                    RdTresorAnCour.Enabled = False
                    RdTresorAnSuiv.Enabled = False
                End If

                query = "select C.TypeConvention,C.CodeConvention from T_Convention as C, T_Marche as M where M.NumeroDAO='" & CmbNumDoss.Text & "' and M.CodeConvention=C.CodeConvention and M.CodeProjet='" & ProjetEnCours & "' and C.CodeBailleur='" & rw1(2).ToString & "'"
                dt0 = ExcecuteSelectQuery(query)
                If dt0.Rows.Count > 0 Then
                    rw1 = dt0.Rows(0)
                    TxtConventionMarche.Text = rw1(0).ToString & " " & rw1(1).ToString
                    If (rw1(0).ToString.ToLower = "don") Then
                        RdDonAnPrec.Enabled = True
                        RdDonAnCour.Enabled = True
                        RdDonAnSuiv.Enabled = True

                        RdEmpruntAnPrec.Enabled = False
                        RdEmpruntAnCour.Enabled = False
                        RdEmpruntAnSuiv.Enabled = False
                    Else
                        RdEmpruntAnPrec.Enabled = True
                        RdEmpruntAnCour.Enabled = True
                        RdEmpruntAnSuiv.Enabled = True

                        RdDonAnPrec.Enabled = False
                        RdDonAnCour.Enabled = False
                        RdDonAnSuiv.Enabled = False
                    End If
                End If
            End If

            query = "select NumeroMarche,PrctCautionDef,PrctAvance,ImputBudgetaire from T_MarcheSigne where CodeFournis='" & TxtCodeFournisMarche.Text & "' and RefLot='" & TxtRefLotMarche.Text & "' and RefSoumis='" & TxtRefSoumisMarche.Text & "'"
            dt0 = ExcecuteSelectQuery(query)
            If dt0.Rows.Count > 0 Then
                Dim rw1 As DataRow = dt0.Rows(0)
                TxtPrctCautionDef.Text = IIf(IsNumeric(rw1(1).ToString) = True, rw1(1).ToString, "0").ToString
                TxtPrctAvance.Text = IIf(IsNumeric(rw1(2).ToString) = True, rw1(2).ToString, "0").ToString
                TxtImputBudgetaire.Text = rw1(3).ToString
                TxtNumeroMarche.Text = rw1(0).ToString

                Dim n As Decimal = 0
                query = "select AnneeRepart,MontantRepart,SujetImputation from T_Marche_Repartition where NumeroMarche='" & rw1(0).ToString & "' order by AnneeRepart"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt1.Rows
                    n += 1
                    If (n = 1) Then
                        LblAnPrec.Text = "(" & rw(0).ToString & ")"
                        TxtMontAnPrec.Text = rw(1).ToString
                        Select Case rw(2).ToString
                            Case "Trésor"
                                RdTresorAnPrec.Checked = True
                            Case "Don"
                                RdDonAnPrec.Checked = True
                            Case "Emprunt"
                                RdEmpruntAnPrec.Checked = True
                        End Select

                    ElseIf (n = 2) Then

                        LblAnCour.Text = "(" & rw(0).ToString & ")"
                        TxtMontAnCour.Text = rw(1).ToString
                        Select Case rw(2).ToString
                            Case "Trésor"
                                RdTresorAnCour.Checked = True
                            Case "Don"
                                RdDonAnCour.Checked = True
                            Case "Emprunt"
                                RdEmpruntAnCour.Checked = True
                        End Select

                    ElseIf (n = 3) Then

                        LblAnSuiv.Text = "(" & rw(0).ToString & ")"
                        TxtMontAnSuiv.Text = rw(1).ToString
                        Select Case rw(2).ToString
                            Case "Trésor"
                                RdTresorAnSuiv.Checked = True
                            Case "Don"
                                RdDonAnSuiv.Checked = True
                            Case "Emprunt"
                                RdEmpruntAnSuiv.Checked = True
                        End Select

                    Else

                        MsgBox("Y a problème!", MsgBoxStyle.Exclamation)

                    End If
                Next

            End If

            Dim Annee As Decimal = Now.Year
            LblAnPrec.Text = "(" & (Annee - 1).ToString & ")"
            LblAnCour.Text = "(" & Annee.ToString & ")"
            LblAnSuiv.Text = "(" & (Annee + 1).ToString & ")"

        End If
    End Sub

    Private Sub TxtMontantMarche_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontantMarche.TextChanged

        If (TxtMontantMarche.Text <> "") Then
            TxtMontMarcheLettre.Text = MontantLettre(TxtMontantMarche.Text.Replace(" ", ""))
        Else
            TxtMontMarcheLettre.Text = ""
        End If

    End Sub

    Private Sub TxtPrctCautionDef_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPrctCautionDef.TextChanged

        If (TxtPrctCautionDef.Text <> "") Then

            If (IsNumeric(TxtPrctCautionDef.Text) = True) Then

                If (CDec(TxtPrctCautionDef.Text) <= 100) Then

                    If (TxtMontantMarche.Text <> "") Then
                        TxtMontCautionDef.Text = AfficherMonnaie(Math.Round((CDec(TxtMontantMarche.Text) * CDec(TxtPrctCautionDef.Text)) / 100, 0).ToString)
                    End If
                Else
                    TxtMontCautionDef.Text = "Erreur!"
                End If

            Else
                TxtMontCautionDef.Text = "Non numérique!"
            End If

        Else
            TxtMontCautionDef.Text = ""
        End If

    End Sub

    Private Sub TxtPrctAvance_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPrctAvance.TextChanged

        If (TxtPrctAvance.Text <> "") Then

            If (IsNumeric(TxtPrctAvance.Text) = True) Then

                If (CDec(TxtPrctAvance.Text) <= 100) Then

                    If (TxtMontantMarche.Text <> "") Then
                        TxtMontAvance.Text = AfficherMonnaie(Math.Round((CDec(TxtMontantMarche.Text) * CDec(TxtPrctAvance.Text)) / 100, 0).ToString)
                    End If
                Else
                    TxtMontAvance.Text = "Erreur!"
                End If

            Else
                TxtMontAvance.Text = "Non numérique!"
            End If

        Else
            TxtMontAvance.Text = ""
        End If

    End Sub

    Private Sub TxtMontAnPrec_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontAnPrec.GotFocus

        If (TxtMontAnPrec.Text = "") Then
            If (IsNumeric(TxtMontAnCour.Text.Replace(" ", "")) = True And IsNumeric(TxtMontAnSuiv.Text.Replace(" ", "")) = True) Then
                TxtMontAnPrec.Text = AfficherMonnaie(CDec(TxtMontantMarche.Text.Replace(" ", "")) - (CDec(TxtMontAnCour.Text.Replace(" ", "")) + CDec(TxtMontAnSuiv.Text.Replace(" ", ""))))
            End If
        End If

    End Sub

    Private Sub TxtMontAnPrec_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontAnPrec.TextChanged

        TxtMontAnPrec.Text = AfficherMonnaie(TxtMontAnPrec.Text.Replace(" ", ""))
        CalculTotalReparti()

    End Sub

    Private Sub CalculTotalReparti()

        TxtTotalReparti.Text = AfficherMonnaie(CDec(IIf(IsNumeric(TxtMontAnPrec.Text) = True, TxtMontAnPrec.Text.Replace(" ", ""), "0").ToString) + CDec(IIf(IsNumeric(TxtMontAnCour.Text) = True, TxtMontAnCour.Text.Replace(" ", ""), "0").ToString) + CDec(IIf(IsNumeric(TxtMontAnSuiv.Text) = True, TxtMontAnSuiv.Text.Replace(" ", ""), "0").ToString))
        If (TxtTotalReparti.Text <> "" And TxtMontantMarche.Text <> "") Then

            If (IsNumeric(TxtTotalReparti.Text.Replace(" ", "")) = True And IsNumeric(TxtMontantMarche.Text.Replace(" ", "")) = True) Then
                If (CDec(TxtTotalReparti.Text.Replace(" ", "")) > CDec(TxtMontantMarche.Text.Replace(" ", ""))) Then
                    TxtTotalReparti.Text = "Répartition incorrecte!"
                End If
            End If

        End If

    End Sub

    Private Sub TxtMontAnCour_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontAnCour.GotFocus

        If (TxtMontAnCour.Text = "") Then
            If (IsNumeric(TxtMontAnPrec.Text.Replace(" ", "")) = True And IsNumeric(TxtMontAnSuiv.Text.Replace(" ", "")) = True) Then
                TxtMontAnCour.Text = AfficherMonnaie(CDec(TxtMontantMarche.Text.Replace(" ", "")) - (CDec(TxtMontAnPrec.Text.Replace(" ", "")) + CDec(TxtMontAnSuiv.Text.Replace(" ", ""))))
            End If
        End If

    End Sub

    Private Sub TxtMontAnCour_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontAnCour.TextChanged

        TxtMontAnCour.Text = AfficherMonnaie(TxtMontAnCour.Text.Replace(" ", ""))
        CalculTotalReparti()

    End Sub

    Private Sub TxtMontAnSuiv_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontAnSuiv.GotFocus

        If (TxtMontAnSuiv.Text = "") Then
            If (IsNumeric(TxtMontAnCour.Text.Replace(" ", "")) = True And IsNumeric(TxtMontAnPrec.Text.Replace(" ", "")) = True) Then
                TxtMontAnSuiv.Text = AfficherMonnaie(CDec(TxtMontantMarche.Text.Replace(" ", "")) - (CDec(TxtMontAnCour.Text.Replace(" ", "")) + CDec(TxtMontAnPrec.Text.Replace(" ", ""))))
            End If
        End If

    End Sub

    Private Sub TxtMontAnSuiv_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontAnSuiv.TextChanged

        TxtMontAnSuiv.Text = AfficherMonnaie(TxtMontAnSuiv.Text.Replace(" ", ""))
        CalculTotalReparti()

    End Sub

    Private Sub BtEnregistrerMarche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnregistrerMarche.Click

        If (CmbLotMarche.Text <> "" And TxtContribuable.Text <> "" And TxtRegCommerce.Text <> "" And TxtNumCompteFournis.Text <> "" And TxtPrctCautionDef.Text <> "" _
            And TxtPrctAvance.Text <> "" And TxtImputBudgetaire.Text <> "" And TxtTotalReparti.Text = TxtMontantMarche.Text And TxtRefLotMarche.Text <> "" _
            And TxtCodeFournisMarche.Text <> "" And TxtRefSoumisMarche.Text <> "" And TxtNomRepLegal.Text <> "" And TxtBpRepLegal.Text <> "" And TxtContactRepLegal.Text <> "" And
            (RdTresorAnPrec.Checked = True Or RdDonAnPrec.Checked = True Or RdEmpruntAnPrec.Checked = True Or
            RdTresorAnCour.Checked = True Or RdDonAnCour.Checked = True Or RdEmpruntAnCour.Checked = True Or
            RdTresorAnSuiv.Checked = True Or RdDonAnSuiv.Checked = True Or RdEmpruntAnSuiv.Checked = True)) Then


            ' Maj Fournisseur *********************


            query = "update T_Fournisseur set CompteContribuableFournis='" & TxtContribuable.Text & "', RegistreCommerceFournis='" & TxtRegCommerce.Text & "', NomRep='" & EnleverApost(TxtNomRepLegal.Text) & "', AdresseRep='" & EnleverApost(TxtBpRepLegal.Text) & "', TelRep='" & TxtContactRepLegal.Text & "' where CodeFournis='" & TxtCodeFournisMarche.Text & "'"
            ExecuteNonQuery(query)



            ' Maj Soumission *********************


            query = "update T_SoumissionFournisseur set NumCompteBanque='" & TxtNumCompteFournis.Text & "' where RefSoumis='" & TxtRefSoumisMarche.Text & "'"
            ExecuteNonQuery(query)


            ' Existance marche ********************
            Dim MarcheExiste As Boolean = False
            query = "select * from T_MarcheSigne where CodeFournis='" & TxtCodeFournisMarche.Text & "' and RefLot='" & TxtRefLotMarche.Text & "' and RefSoumis='" & TxtRefSoumisMarche.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            If dt0.Rows.Count > 0 Then
                MarcheExiste = True
            End If
            '          Marche existe ************
            Dim DatSet As New DataSet
            Dim DatAdapt As MySqlDataAdapter
            Dim DatTable As DataTable
            Dim DatRow As DataRow
            Dim CmdBuilder As MySqlCommandBuilder
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            If (MarcheExiste = True) Then
                query = "update T_MarcheSigne set PrctCautionDef='" & TxtPrctCautionDef.Text.Replace(".", ",") & "', PrctAvance='" & +"', ImputBudgetaire='" & TxtImputBudgetaire.Text & "' where CodeFournis='" & TxtCodeFournisMarche.Text & "' and RefLot='" & TxtRefLotMarche.Text & "' and RefSoumis='" & TxtRefSoumisMarche.Text & "'"
                ExecuteNonQuery(query)
            Else
                Dim MarcheRef As String = "0"
                query = "select RefMarche from T_Marche where NumeroDAO='" & CmbNumDoss.Text & "' and TypeMarche='" & TxtTypeMarche.Text & "' and CodeProjet='" & ProjetEnCours & "'"
                dt0 = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    MarcheRef = rw(0).ToString
                Next

                DatSet = New DataSet
                query = "select * from T_MarcheSigne"
                Dim Cmd = New MySqlCommand(query, sqlconn)
                DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "T_MarcheSigne")
                DatTable = DatSet.Tables("T_MarcheSigne")
                DatRow = DatSet.Tables("T_MarcheSigne").NewRow()

                DatRow("NumeroMarche") = ProjetEnCours & TxtCodeFournisMarche.Text & "." & TxtRefLotMarche.Text & "." & TxtRefSoumisMarche.Text & "/" & Now.ToShortDateString.Replace("/", ".")
                DatRow("NumMarcheDMP") = ""
                DatRow("DateMarche") = Now.ToShortDateString
                DatRow("RefMarche") = MarcheRef
                DatRow("TypeMarche") = TxtTypeMarche.Text
                DatRow("MontantHT") = TxtMontantMarche.EditValue.ToString.Replace(" ", "")
                DatRow("CodeFournis") = TxtCodeFournisMarche.Text
                DatRow("RefLot") = TxtRefLotMarche.Text
                DatRow("RefSoumis") = TxtRefSoumisMarche.Text
                DatRow("PrctCautionDef") = TxtPrctCautionDef.Text.Replace(".", ",")
                DatRow("PrctAvance") = TxtPrctAvance.Text.Replace(".", ",")
                DatRow("ImputBudgetaire") = TxtImputBudgetaire.Text
                DatRow("CodeProjet") = ProjetEnCours

                DatSet.Tables("T_MarcheSigne").Rows.Add(DatRow)
                CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "T_MarcheSigne")
                DatSet.Clear()

            End If


            ' Repartition du budget *****************************
            query = "DELETE from T_Marche_Repartition where NumeroMarche like '" & ProjetEnCours & TxtCodeFournisMarche.Text & "." & TxtRefLotMarche.Text & "." & TxtRefSoumisMarche.Text & "/%'"
            ExecuteNonQuery(query)


            'Dim LesTxtMont() As DevExpress.XtraEditors.TextEdit = {TxtMontAnPrec, TxtMontAnCour, TxtMontAnSuiv}

            DatSet = New DataSet
            query = "select * from T_Marche_Repartition"
            Dim Cmd1 = New MySqlCommand(query, sqlconn)
            DatAdapt = New MySqlDataAdapter(Cmd1)
            DatAdapt.Fill(DatSet, "T_Marche_Repartition")
            DatTable = DatSet.Tables("T_Marche_Repartition")

            If (RdTresorAnPrec.Checked = True Or RdDonAnPrec.Checked = True Or RdEmpruntAnPrec.Checked = True) Then
                DatRow = DatSet.Tables("T_Marche_Repartition").NewRow()
                DatRow("NumeroMarche") = IIf(TxtNumeroMarche.Text <> "", TxtNumeroMarche.Text, ProjetEnCours & TxtCodeFournisMarche.Text & "." & TxtRefLotMarche.Text & "." & TxtRefSoumisMarche.Text & "/" & Now.ToShortDateString.Replace("/", ".")).ToString
                DatRow("AnneeRepart") = (Now.Year - 1).ToString
                DatRow("MontantRepart") = IIf(TxtMontAnPrec.Text.Replace(" ", "") <> "", TxtMontAnPrec.Text.Replace(" ", ""), "0").ToString
                DatRow("SujetImputation") = IIf(RdTresorAnPrec.Checked = True, "Trésor", IIf(RdDonAnPrec.Checked = True, "Don", "Emprunt").ToString).ToString
                DatSet.Tables("T_Marche_Repartition").Rows.Add(DatRow)
            End If

            If (RdTresorAnCour.Checked = True Or RdDonAnCour.Checked = True Or RdEmpruntAnCour.Checked = True) Then
                DatRow = DatSet.Tables("T_Marche_Repartition").NewRow()
                DatRow("NumeroMarche") = IIf(TxtNumeroMarche.Text <> "", TxtNumeroMarche.Text, ProjetEnCours & TxtCodeFournisMarche.Text & "." & TxtRefLotMarche.Text & "." & TxtRefSoumisMarche.Text & "/" & Now.ToShortDateString.Replace("/", ".")).ToString
                DatRow("AnneeRepart") = (Now.Year).ToString
                DatRow("MontantRepart") = IIf(TxtMontAnCour.Text.Replace(" ", "") <> "", TxtMontAnCour.Text.Replace(" ", ""), "0").ToString
                DatRow("SujetImputation") = IIf(RdTresorAnCour.Checked = True, "Trésor", IIf(RdDonAnCour.Checked = True, "Don", "Emprunt").ToString).ToString
                DatSet.Tables("T_Marche_Repartition").Rows.Add(DatRow)
            End If

            If (RdTresorAnSuiv.Checked = True Or RdDonAnSuiv.Checked = True Or RdEmpruntAnSuiv.Checked = True) Then
                DatRow = DatSet.Tables("T_Marche_Repartition").NewRow()
                DatRow("NumeroMarche") = IIf(TxtNumeroMarche.Text <> "", TxtNumeroMarche.Text, ProjetEnCours & TxtCodeFournisMarche.Text & "." & TxtRefLotMarche.Text & "." & TxtRefSoumisMarche.Text & "/" & Now.ToShortDateString.Replace("/", ".")).ToString
                DatRow("AnneeRepart") = (Now.Year + 1).ToString
                DatRow("MontantRepart") = IIf(TxtMontAnSuiv.Text.Replace(" ", "") <> "", TxtMontAnSuiv.Text.Replace(" ", ""), "0").ToString
                DatRow("SujetImputation") = IIf(RdTresorAnSuiv.Checked = True, "Trésor", IIf(RdDonAnSuiv.Checked = True, "Don", "Emprunt").ToString).ToString
                DatSet.Tables("T_Marche_Repartition").Rows.Add(DatRow)
            End If

            CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_Marche_Repartition")
            DatSet.Clear()
            BDQUIT(sqlconn)

            MsgBox("Marché enregistré avec succès!", MsgBoxStyle.Information)
            PnlEditionMarche.Visible = False

        Else
            MsgBox("Formulaire incomplet!", MsgBoxStyle.Exclamation)

        End If

    End Sub

    Public Sub EditerMarche(ByVal Marche As String, ByVal Traitemt As String)
        ContratDeMarche(Marche, Traitemt)
    End Sub

    Private Sub BtEtatMarche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEtatMarche.Click

        'If (PnlEditionMarche.Visible = True And TxtNumeroMarche.Text <> "") Then

        '    ContratDeMarche()

        'End If

        MarcheSigne.ShowDialog()
        MarcheSigne.ChkDAO.Checked = True
        MarcheSigne.CmbDAO.Text = CmbNumDoss.Text

    End Sub

    Private Sub CmbLotMarche_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbLotMarche.SelectedIndexChanged
        InitFormMarche()

        If (CmbLotMarche.Text <> "") Then
            query = "select LibelleLot, Reflot from T_LotDAO where NumeroDAO='" & CmbNumDoss.Text & "' and CodeLot='" & CmbLotMarche.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            If dt0.Rows.Count > 0 Then
                Dim rw As DataRow = dt0.Rows(0)
                TxtLotMarche.Text = IIf(rw(0).ToString <> "Lot N°" & CmbLotMarche.Text, MettreApost(rw(0).ToString), TxtLibelleDoss.Text & " (" & rw(0).ToString & ")")
                TxtRefLotMarche.Text = rw(1).ToString

                query = "select BanqueCaution,DelaiLivraison,PrixCorrigeOffre,CodeFournis,RefSoumis,NumCompteBanque from T_SoumissionFournisseur where RefLot='" & rw(1).ToString & "' and Attribue='OUI'"
                dt0 = ExcecuteSelectQuery(query)
                If dt0.Rows.Count > 0 Then
                    Dim rw1 As DataRow = dt0.Rows(0)
                    TxtRefSoumisMarche.Text = rw1(4).ToString
                    Dim NomBank As String = ""

                    query = "select NomCompletBanque from T_Banque where CodeProjet='" & ProjetEnCours & "' and CodeBanque='" & rw1(0).ToString & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    If dt0.Rows.Count > 0 Then
                        NomBank = MettreApost(dt0.Rows(0).Item(0).ToString)
                    End If

                    TxtNomBanqueFournis.Text = MettreApost(rw1(0).ToString) & " (" & NomBank & ")"
                    TxtNumCompteFournis.Text = rw1(5).ToString
                    TxtExecutionMarche.Text = rw1(1).ToString
                    TxtMontantMarche.Text = AfficherMonnaie(rw1(2).ToString.Replace(" ", ""))
                    TxtCodeFournisMarche.Text = rw1(3).ToString

                    TxtTotalReparti.Text = AfficherMonnaie(rw1(2).ToString.Replace(" ", ""))

                    ' infos fournis *****************
                    query = "select NomFournis,PaysFournis,AdresseCompleteFournis,TelFournis,FaxFournis,MailFournis,CompteContribuableFournis,RegistreCommerceFournis,NomRep,AdresseRep,TelRep from T_Fournisseur where CodeFournis='" & rw1(3).ToString & "' and CodeProjet='" & ProjetEnCours & "' and NumeroDAO='" & CmbNumDoss.Text & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    If dt0.Rows.Count > 0 Then
                        rw1 = dt0.Rows(0)
                        TxtFournisMarche.Text = MettreApost(rw1(0).ToString & " (" & rw1(1).ToString & ")")
                        TxtAdresseFournisMarche.Text = MettreApost(rw1(2).ToString)
                        TxtContactFournisMarche.Text = IIf(rw1(3).ToString <> "", "Tel : " & rw1(3).ToString & "  ", "") & IIf(rw1(4).ToString <> "", "Fax : " & rw1(4).ToString & "  ", "") & IIf(rw1(5).ToString <> "", "E-mail : " & rw1(5).ToString, "")
                        TxtContribuable.Text = rw1(6).ToString
                        TxtRegCommerce.Text = rw1(7).ToString
                        TxtNomRepLegal.Text = MettreApost(rw1(8).ToString)
                        TxtBpRepLegal.Text = MettreApost(rw1(9).ToString)
                        TxtContactRepLegal.Text = rw1(10).ToString

                    End If
                End If
            End If

            query = "select B.InitialeBailleur,B.NomBailleur,B.CodeBailleur from T_Bailleur as B,T_Marche as M where M.NumeroDAO='" & CmbNumDoss.Text & "' and M.CodeProjet='" & ProjetEnCours & "' and B.InitialeBailleur=M.InitialeBailleur and B.CodeProjet=M.CodeProjet"
            dt0 = ExcecuteSelectQuery(query)
            If dt0.Rows.Count > 0 Then
                Dim rw As DataRow = dt0.Rows(0)
                TxtBailleurMarche.Text = MettreApost(rw(0).ToString & " (" & rw(1).ToString & ")")
                If (rw(0).ToString = "ETAT") Then
                    RdTresorAnPrec.Enabled = True
                    RdTresorAnCour.Enabled = True
                    RdTresorAnSuiv.Enabled = True
                Else
                    RdTresorAnPrec.Enabled = False
                    RdTresorAnCour.Enabled = False
                    RdTresorAnSuiv.Enabled = False
                End If

                query = "select C.TypeConvention,C.CodeConvention from T_Convention as C, T_Marche as M where M.NumeroDAO='" & CmbNumDoss.Text & "' and M.CodeConvention=C.CodeConvention and M.CodeProjet='" & ProjetEnCours & "' and C.CodeBailleur='" & rw(2).ToString & "'"
                dt0 = ExcecuteSelectQuery(query)
                If dt0.Rows.Count > 0 Then
                    Dim rw1 As DataRow = dt0.Rows(0)
                    TxtConventionMarche.Text = rw1(0).ToString & " " & rw1(1).ToString
                    If (rw1(0).ToString.ToLower = "don") Then
                        RdDonAnPrec.Enabled = True
                        RdDonAnCour.Enabled = True
                        RdDonAnSuiv.Enabled = True

                        RdEmpruntAnPrec.Enabled = False
                        RdEmpruntAnCour.Enabled = False
                        RdEmpruntAnSuiv.Enabled = False
                    Else
                        RdEmpruntAnPrec.Enabled = True
                        RdEmpruntAnCour.Enabled = True
                        RdEmpruntAnSuiv.Enabled = True

                        RdDonAnPrec.Enabled = False
                        RdDonAnCour.Enabled = False
                        RdDonAnSuiv.Enabled = False
                    End If
                End If
            End If

            query = "select NumeroMarche,PrctCautionDef,PrctAvance,ImputBudgetaire from T_MarcheSigne where CodeFournis='" & TxtCodeFournisMarche.Text & "' and RefLot='" & TxtRefLotMarche.Text & "' and RefSoumis='" & TxtRefSoumisMarche.Text & "'"
            dt0 = ExcecuteSelectQuery(query)
            If dt0.Rows.Count > 0 Then
                Dim rw As DataRow = dt0.Rows(0)
                TxtPrctCautionDef.Text = IIf(IsNumeric(rw(1).ToString) = True, rw(1).ToString, "0").ToString
                TxtPrctAvance.Text = IIf(IsNumeric(rw(2).ToString) = True, rw(2).ToString, "0").ToString
                TxtImputBudgetaire.Text = rw(3).ToString
                TxtNumeroMarche.Text = rw(0).ToString

                Dim n As Decimal = 0
                query = "select AnneeRepart,MontantRepart,SujetImputation from T_Marche_Repartition where NumeroMarche='" & rw(0).ToString & "' order by AnneeRepart"
                dt0 = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt0.Rows
                    n += 1
                    If (n = 1) Then
                        LblAnPrec.Text = "(" & rw1(0).ToString & ")"
                        TxtMontAnPrec.Text = rw1(1).ToString
                        Select Case rw1(2).ToString
                            Case "Trésor"
                                RdTresorAnPrec.Checked = True
                            Case "Don"
                                RdDonAnPrec.Checked = True
                            Case "Emprunt"
                                RdEmpruntAnPrec.Checked = True
                        End Select

                    ElseIf (n = 2) Then

                        LblAnCour.Text = "(" & rw1(0).ToString & ")"
                        TxtMontAnCour.Text = rw1(1).ToString
                        Select Case rw1(2).ToString
                            Case "Trésor"
                                RdTresorAnCour.Checked = True
                            Case "Don"
                                RdDonAnCour.Checked = True
                            Case "Emprunt"
                                RdEmpruntAnCour.Checked = True
                        End Select

                    ElseIf (n = 3) Then

                        LblAnSuiv.Text = "(" & rw1(0).ToString & ")"
                        TxtMontAnSuiv.Text = rw1(1).ToString
                        Select Case rw1(2).ToString
                            Case "Trésor"
                                RdTresorAnSuiv.Checked = True
                            Case "Don"
                                RdDonAnSuiv.Checked = True
                            Case "Emprunt"
                                RdEmpruntAnSuiv.Checked = True
                        End Select

                    Else

                        MsgBox("Y a problème!", MsgBoxStyle.Exclamation)

                    End If
                Next
            End If

            Dim Annee As Decimal = Now.Year
            LblAnPrec.Text = "(" & (Annee - 1).ToString & ")"
            LblAnCour.Text = "(" & Annee.ToString & ")"
            LblAnSuiv.Text = "(" & (Annee + 1).ToString & ")"

        End If
    End Sub

    Private Sub cmbSousLot_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbSousLot.SelectedValueChanged
        CmbSoumis.Text = ""
        TxtAdresseSoumis.Text = ""
        query = "select * from t_lotdao_souslot where NumeroDAO='" & CmbNumDoss.Text & "' and RefLot='" & TxtRefLot.Text & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            TxtLibelleSousLot.Text = MettreApost(rw("LibelleSousLot").ToString)
            TxtRefSousLot.Text = rw("CodeSousLot").ToString
            ChargerSoumis(EtapeActuelle)
        Next
    End Sub

End Class