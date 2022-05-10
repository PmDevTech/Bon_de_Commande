Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports MySql.Data.MySqlClient
Imports System.IO
Imports Microsoft.Office.Interop
Imports ClearProject.PassationMarche


Public Class MarcheSigne
    Dim dtMarche = New DataTable()
    Dim DrX As DataRow
    Public NumeroDAO As String = ""

    Private Sub MarcheSigne_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ChargerDAO()
        ChargerFournis()
        ChargerMarche()
    End Sub

    Private Sub ChargerDAO()
        query = "select distinct(L.NumeroDAO) from T_LotDAO as L,T_MarcheSigne as M, T_DAO as D where M.RefLot=L.RefLot and L.NumeroDAO=D.NumeroDAO and D.statut_DAO<>'Annulé' and  D.CodeProjet='" & ProjetEnCours & "' ORDER BY D.DateEdition DESC"
        CmbDAO.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbDAO.Properties.Items.Add(MettreApost(rw("NumeroDAO").ToString))
        Next
    End Sub

    Private Sub ChargerFournis()
        query = "select distinct(F.NomFournis) from T_MarcheSigne as M, T_Fournisseur as F where M.CodeFournis=F.CodeFournis and F.CodeProjet='" & ProjetEnCours & "'"
        CmbFournis.Text = ""
        CmbFournis.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbFournis.Properties.Items.Add(MettreApost(rw("NomFournis").ToString))
        Next
    End Sub

    Private Sub ChargerMarche()

        If CmbDAO.Text <> "" Or CmbFournis.Text <> "" Or TxtNumMarcheSearch.Text <> "" Or CmbEtat.Text <> "" Then
            dtMarche.Columns.Clear()
            dtMarche.Columns.Add("Code", Type.GetType("System.String"))
            dtMarche.Columns.Add("N° Marche", Type.GetType("System.String"))
            dtMarche.Columns.Add("Type", Type.GetType("System.String"))
            dtMarche.Columns.Add("Libelle / Description", Type.GetType("System.String"))
            dtMarche.Columns.Add("Financement", Type.GetType("System.String"))
            dtMarche.Columns.Add("Montant", Type.GetType("System.String"))
            dtMarche.Columns.Add("Fournisseur", Type.GetType("System.String"))
            dtMarche.Columns.Add("Date signature", Type.GetType("System.String"))
            dtMarche.Columns.Add("Durée", Type.GetType("System.String"))
            dtMarche.Columns.Add("Date de fin", Type.GetType("System.String"))
            dtMarche.Columns.Add("Etat", Type.GetType("System.String"))
            dtMarche.Columns.Add("MethodePDM", Type.GetType("System.String"))
            dtMarche.Rows.Clear()

            Dim cptr As Decimal = 0

            'query = "select M.NumeroMarche,D.TypeMarche,D.IntituleDAO,L.LibelleLot,N.InitialeBailleur,P.PrixCorrigeOffre,F.NomFournis,M.DateMarche,S.DelaiLivraison,M.EtatMarche from T_MarcheSigne as M, T_Fournisseur as F, T_SoumissionFournisseur as S, t_soumissionfournisseurclassement as P, T_LotDAO as L, T_DAO as D, T_Marche as N where M.CodeFournis=F.CodeFournis AND S.CodeFournis=P.CodeFournis and S.CodeFournis=F.CodeFournis and S.CodeFournis=M.CodeFournis AND P.CodeFournis=M.CodeFournis and M.RefLot=L.RefLot and M.RefLot=S.RefLot and L.NumeroDAO=D.NumeroDAO AND S.CodeLot=P.CodeLot and D.NumeroDAO=N.NumeroDAO and P.NumeroDAO=D.NumeroDAO and D.CodeProjet='" & ProjetEnCours & "' and L.NumeroDAO like'" & CmbDAO.Text & "%' and F.NomFournis like '" & EnleverApost(CmbFournis.Text) & "%' and M.NumeroMarche like '%" & TxtNumMarcheSearch.Text & "%'  GROUP by F.NomFournis,D.IntituleDAO"
            ' query = "select M.NumeroMarche, M.DateMarche, M.EtatMarche, M.MontantHT, D.TypeMarche, D.IntituleDAO, L.LibelleLot, N.InitialeBailleur, F.NomFournis, S.DelaiLivraison from T_MarcheSigne as M, T_Fournisseur as F, T_SoumissionFournisseur as S, T_LotDAO as L, T_DAO as D, T_Marche as N where M.CodeFournis=F.CodeFournis AND S.CodeFournis=F.CodeFournis and S.CodeFournis=M.CodeFournis AND M.RefLot=L.RefLot and M.RefLot=S.RefLot and L.NumeroDAO=D.NumeroDAO AND S.CodeLot=L.CodeLot and D.RefMarche=N.RefMarche and M.NumeroDAO=D.NumeroDAO and D.CodeProjet='" & ProjetEnCours & "' and L.NumeroDAO like'" & CmbDAO.Text & "%' and F.NomFournis like '" & EnleverApost(CmbFournis.Text) & "%' and M.NumeroMarche like '%" & TxtNumMarcheSearch.Text & "%'  GROUP by M.NumeroMarche" 'F.NomFournis, D.IntituleDAO

            query = "select M.NumeroMarche, M.DateMarche, M.EtatMarche, M.MontantHT, D.TypeMarche, D.IntituleDAO, D.MethodePDM, L.LibelleLot, N.InitialeBailleur, F.NomFournis, S.DelaiLivraison from T_MarcheSigne as M, T_Fournisseur as F, T_SoumissionFournisseur as S, T_LotDAO as L, T_DAO as D, T_Marche as N where M.CodeFournis=F.CodeFournis AND S.CodeFournis=F.CodeFournis and S.CodeFournis=M.CodeFournis AND M.RefLot=L.RefLot and M.RefLot=S.RefLot and L.NumeroDAO=D.NumeroDAO AND S.CodeLot=L.CodeLot and D.RefMarche=N.RefMarche and M.NumeroDAO=D.NumeroDAO and D.CodeProjet='" & ProjetEnCours & "' and M.NumeroDAO='" & EnleverApost(CmbDAO.Text) & "' and F.NomFournis like '" & EnleverApost(CmbFournis.Text) & "%' and M.EtatMarche LIKE '" & EnleverApost(CmbEtat.Text) & "%' and M.NumeroMarche like '%" & TxtNumMarcheSearch.Text & "%' GROUP by M.NumeroMarche" 'F.NomFournis, D.IntituleDAO
            'InputBox("fod", "fodj", query)
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cptr += 1
                Dim drS = dtMarche.NewRow()
                drS("Code") = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
                drS("N° Marche") = MettreApost(rw("NumeroMarche").ToString)
                drS("Type") = MettreApost(rw("TypeMarche").ToString)
                drS("Libelle / Description") = IIf(Mid(rw("LibelleLot").ToString, 1, 6) = "Lot N°", MettreApost(rw("IntituleDAO").ToString) & " (" & rw("LibelleLot").ToString & ")", MettreApost(rw("LibelleLot").ToString)).ToString
                drS("Financement") = MettreApost(rw("InitialeBailleur").ToString)
                drS("Montant") = AfficherMonnaie(rw("MontantHT").ToString.Replace(" ", ""))
                drS("Fournisseur") = MettreApost(rw("NomFournis").ToString)
                drS("Date signature") = rw("DateMarche").ToString
                drS("Durée") = rw("DelaiLivraison").ToString
                Dim partDel() As String = rw("DelaiLivraison").ToString.Split(" "c)
                Dim durr As Decimal = (CInt(partDel(0)) * CInt(IIf(partDel(1) = "Mois", 30, CInt(IIf(partDel(1) = "Semaines", 7, 1)))))
                drS("Date de fin") = (CDate(rw("DateMarche")).AddDays(durr)).ToShortDateString
                drS("Etat") = rw("EtatMarche").ToString
                drS("MethodePDM") = rw("MethodePDM").ToString

                dtMarche.Rows.Add(drS)
            Next

            GridMarche.DataSource = dtMarche

            ViewMarche.Columns("Code").Visible = False
            ViewMarche.Columns("MethodePDM").Visible = False
            ViewMarche.Columns(1).Width = 120
            ViewMarche.Columns(2).Width = 100
            ViewMarche.Columns(3).Width = 300
            ViewMarche.Columns(4).Width = 80
            ViewMarche.Columns(5).Width = 120
            ViewMarche.Columns(6).Width = 120
            ViewMarche.Columns(7).Width = 100
            ViewMarche.Columns(8).Width = 80
            ViewMarche.Columns(9).Width = 100
            ViewMarche.Columns(10).Width = 100

            ViewMarche.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewMarche.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewMarche.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewMarche.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewMarche.Columns(7).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewMarche.Columns(8).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewMarche.Columns(9).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            ViewMarche.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

            ColorRowGrid(ViewMarche, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
        End If
    End Sub

    Private Sub ChkDAO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkDAO.CheckedChanged
        If (ChkDAO.Checked = True) Then
            CmbDAO.Enabled = True
        Else
            CmbDAO.Text = ""
            CmbDAO.Enabled = False
        End If
    End Sub

    Private Sub CmbDAO_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbDAO.SelectedValueChanged, CmbFournis.SelectedValueChanged, CmbEtat.SelectedValueChanged
        ChargerMarche()
    End Sub

    Private Sub ChkFournis_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkFournis.CheckedChanged
        If (ChkFournis.Checked = True) Then
            CmbFournis.Enabled = True
        Else
            CmbFournis.Text = ""
            CmbFournis.Enabled = False
        End If
    End Sub

    Private Sub ChEtat_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChEtat.CheckedChanged
        If (ChEtat.Checked = True) Then
            CmbEtat.Enabled = True
        Else
            CmbEtat.Text = ""
            CmbEtat.Enabled = False
        End If
    End Sub

    Private Sub TxtNumMarcheSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNumMarcheSearch.TextChanged
        ChargerMarche()
    End Sub

#Region "Consulter dossier"
    Private Sub ConsulterLeDossierToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ConsulterLeDossierToolStripMenuItem.Click
        Try
            If (ViewMarche.RowCount > 0) Then
                DrX = ViewMarche.GetDataRow(ViewMarche.FocusedRowHandle)

                query = "SELECT M.*, D.MethodePDM FROM t_marchesigne as M, t_dao as D WHERE M.NumeroDAO=D.NumeroDAO and M.NumeroMarche='" & EnleverApost(DrX("N° Marche").ToString) & "' AND M.CodeProjet='" & ProjetEnCours & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                If dt.Rows.Count = 0 Then
                    FailMsg("Aucune informations trouvés.")
                    Exit Sub
                End If
                Dim dtDoss As DataRow = dt.Rows(0)

                'Verifier si le fichier existe déjà
                Dim CheminFile As String = line & "\DAO\" & MettreApost(dtDoss("TypeMarche").ToString) & "\" & MettreApost(dtDoss("MethodePDM").ToString) & "\" & FormatFileName(MettreApost(dtDoss("NumeroDAO").ToString), "") & "\Marche"
                Dim NomFichierpdf As String = "\Marche N°_" & FormatFileName(DrX("N° Marche").ToString, "") & ".pdf"

                If File.Exists(CheminFile & NomFichierpdf) = True Then
                    If ConfirmMsg("Voulez-vous actualiser les données du marchés ?") = DialogResult.Yes Then
                        If GetConsoliderMarcher(dtDoss, "Actualisation") = False Then
                            Exit Sub
                        End If
                    End If
                ElseIf Not File.Exists(CheminFile & NomFichierpdf) = True Then
                    If GetConsoliderMarcher(dtDoss, "Consolidation") = False Then
                        Exit Sub
                    End If
                End If

                If File.Exists(CheminFile & NomFichierpdf) = True Then
                    DebutChargement(True, "Chargement de l'etat du marché en cours...")
                    Process.Start(CheminFile & NomFichierpdf)
                    FinChargement()
                End If
            End If
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

#End Region

#Region "Imprimer dossier"
    Private Sub ImprimerLeDossierToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImprimerLeDossierToolStripMenuItem.Click
        Try
            If (ViewMarche.RowCount > 0) Then
                DrX = ViewMarche.GetDataRow(ViewMarche.FocusedRowHandle)
                query = "SELECT M.*, D.MethodePDM FROM t_marchesigne as M, t_dao as D WHERE M.NumeroDAO=D.NumeroDAO and M.NumeroMarche='" & EnleverApost(DrX("N° Marche").ToString) & "' AND M.CodeProjet='" & ProjetEnCours & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                If dt.Rows.Count = 0 Then
                    Exit Sub
                End If
                Dim dtDoss As DataRow = dt.Rows(0)

                'Verifier si le fichier existe déjà
                Dim CheminFile As String = line & "\DAO\" & MettreApost(dtDoss("TypeMarche").ToString) & "\" & MettreApost(dtDoss("MethodePDM").ToString) & "\" & FormatFileName(MettreApost(dtDoss("NumeroDAO").ToString), "") & "\Marche"
                Dim NomFichierpdf As String = "\Marche N°_" & FormatFileName(DrX("N° Marche").ToString, "") & ".pdf"

                If File.Exists(CheminFile & NomFichierpdf) = True Then
                    If ConfirmMsg("Voulez-vous actualiser les données du marchés ?") = DialogResult.Yes Then
                        If GetConsoliderMarcher(dtDoss, "Actualisation") = False Then
                            Exit Sub
                        End If
                    End If
                ElseIf Not File.Exists(CheminFile & NomFichierpdf) = True Then
                    If GetConsoliderMarcher(dtDoss, "Consolidation") = False Then
                        Exit Sub
                    End If
                End If
                FinChargement()

                If File.Exists(CheminFile & NomFichierpdf) = True Then
                    Try
                        DebutChargement(True, "Chargement du fichier à imprimer en cours...")
                        Dim printer As New Process
                        printer.StartInfo.Verb = "Print"
                        printer.StartInfo.FileName = CheminFile & NomFichierpdf
                        printer.StartInfo.CreateNoWindow = True
                        FinChargement()
                        printer.Start()
                    Catch ex As Exception
                        FailMsg(ex.ToString)
                    End Try
                End If
            End If

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

#End Region

#Region "GetConsoliderMarcher"

    Private Function GetConsoliderMarcher(ByVal dtDoss As DataRow, Optional TextMessagas As String = "") As Boolean
        Try
            Dim report1, report2, report3, report4 As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim DatSet = New DataSet

            DebutChargement(True, TextMessagas.ToString & " de l'etat du marché en cours...")
            If GetSaveArticle(dtDoss("NumeroMarche"), dtDoss("TypeMarche"), dtDoss("MethodePDM")) = False Then
                FinChargement()
                Return False
            End If

            Dim Chemin As String = lineEtat & "\Marches\DAO\Fournitures\DTAO\"

            If dtDoss("TypeMarche").ToString.ToLower = "Travaux".ToLower Then
                Chemin = lineEtat & "\Marches\DAO\Travaux\" 'Chemin Travaux
            End If

            If dtDoss("TypeMarche").ToString.ToLower = "Fournitures".ToLower Then
                If dtDoss("MethodePDM").ToString = "PSL" Then
                    report1.Load(Chemin & "PSL\DAO_Fourniture_Contrat_PSL.rpt")
                    report2.Load(Chemin & "PSL\ClauseContrat.rpt")
                ElseIf dtDoss("MethodePDM").ToString = "PSO" Then
                    report1.Load(Chemin & "PSO\DAO_Fourniture_Contrat_PSO.rpt")
                    report2.Load(Chemin & "PSO\ClauseContrat.rpt")
                ElseIf dtDoss("MethodePDM").ToString = "PSC" Then
                    report1.Load(Chemin & "PSC\DAO_Fourniture_Contrat_PSC.rpt")
                    report2.Load(Chemin & "PSC\ClauseContrat.rpt")
                ElseIf dtDoss("MethodePDM").ToString = "AOI" Or dtDoss("MethodePDM").ToString = "AON" Then
                    report1.Load(Chemin & "AOI_AON\Marche\Contrat_AOI_AON_1_IDA.rpt")
                    report2.Load(Chemin & "AOI_AON\Marche\Contrat_AOI_AON_2_IDA.rpt")
                    report3.Load(Chemin & "AOI_AON\Marche\Contrat_AOI_AON_3_IDA.rpt")
                    report4.Load(Chemin & "AOI_AON\Marche\Contrat_AOI_AON_4_IDA.rpt")
                Else
                    FinChargement()
                    FailMsg("Etat non prévu.")
                    Return False
                End If

            ElseIf dtDoss("TypeMarche").ToString.ToLower = "Travaux".ToLower Then
                FinChargement()
                FailMsg("Etat en cours de réalisation.")
                Return False
            End If

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = report1.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next
            report1.SetDataSource(DatSet)
            CrTables = report2.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next
            report2.SetDataSource(DatSet)

            Dim CodeFournis As String = String.Empty
            Dim RefLot As String = String.Empty
            Dim NumCompteBanque = String.Empty
            Dim DelaiLivraison = String.Empty
            Dim BanqueCaution = String.Empty
            Dim PrixCorrigeOffre = String.Empty
            Dim refSoumis = String.Empty
            Dim NomCordonnateur = String.Empty
            Dim CodeLot = String.Empty

            query = "SELECT * FROM t_soumissionfournisseur WHERE CodeFournis='" & dtDoss("CodeFournis").ToString & "' AND RefLot='" & dtDoss("RefLot").ToString & "' GROUP by CodeLot"
            Dim dt1 = ExcecuteSelectQuery(query)
            For Each rw1 In dt1.Rows
                NumCompteBanque = rw1("NumCompteBanque").ToString
                DelaiLivraison = rw1("DelaiLivraison").ToString
                BanqueCaution = rw1("BanqueCaution").ToString
                refSoumis = rw1("RefSoumis").ToString
                CodeLot = rw1("CodeLot").ToString
            Next
            PrixCorrigeOffre = ExecuteScallar("SELECT PrixCorrigeOffre FROM t_soumissionfournisseurclassement WHERE CodeFournis='" & dtDoss("CodeFournis").ToString & "' AND CodeLot='" & CodeLot & "' AND NumeroDAO='" & dtDoss("NumeroDAO") & "'")

            If dtDoss("TypeMarche").ToString.ToLower = "Fournitures".ToLower Then
                If dtDoss("MethodePDM").ToString.ToUpper = "PSL" Or dtDoss("MethodePDM").ToString.ToUpper = "PSO" Or dtDoss("MethodePDM").ToString.ToUpper = "PSC" Then
                    report1.SetParameterValue("NumDAO", dtDoss("NumeroDAO").ToString)
                    report1.SetParameterValue("CodeProjet", ProjetEnCours)
                    report1.SetParameterValue("DelaiLivraison", DelaiLivraison)
                    report1.SetParameterValue("NumMarche", dtDoss("NumeroMarche"))
                    report2.SetParameterValue("NumDAO", dtDoss("NumeroDAO").ToString)
                    report2.SetParameterValue("CodeProjet", ProjetEnCours)
                    report2.SetParameterValue("DelaiLivraison", DelaiLivraison)
                    report2.SetParameterValue("NumMarche", dtDoss("NumeroMarche"))

                ElseIf dtDoss("MethodePDM").ToString.ToUpper = "AON" Or dtDoss("MethodePDM").ToString.ToUpper = "AOI" Then
                    CrTables = report3.Database.Tables
                    For Each CrTable In CrTables
                        crtableLogoninfo = CrTable.LogOnInfo
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo
                        CrTable.ApplyLogOnInfo(crtableLogoninfo)
                    Next
                    report3.SetDataSource(DatSet)
                    CrTables = report4.Database.Tables
                    For Each CrTable In CrTables
                        crtableLogoninfo = CrTable.LogOnInfo
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo
                        CrTable.ApplyLogOnInfo(crtableLogoninfo)
                    Next
                    report4.SetDataSource(DatSet)

                    ' ************** Paramettre ************************
                    report1.SetParameterValue("NumDAO", dtDoss("NumeroDAO").ToString)
                    report1.SetParameterValue("CodeProjet", ProjetEnCours)

                    report2.SetParameterValue("NumDAO", dtDoss("NumeroDAO").ToString)
                    report2.SetParameterValue("CodeProjet", ProjetEnCours)
                    report2.SetParameterValue("NumMarche", dtDoss("NumeroMarche").ToString)

                    report3.SetParameterValue("NumDAO", dtDoss("NumeroDAO").ToString)
                    report3.SetParameterValue("NumMarche", dtDoss("NumeroMarche").ToString)
                    report3.SetParameterValue("CodeProjet", ProjetEnCours)
                    report3.SetParameterValue("IdFournisseur", dtDoss("CodeFournis"))

                    report4.SetParameterValue("NumDAO", dtDoss("NumeroDAO").ToString)
                    report4.SetParameterValue("NumMarche", dtDoss("NumeroMarche").ToString)
                    report4.SetParameterValue("CodeProjet", ProjetEnCours)
                End If

            ElseIf dtDoss("TypeMarche").ToString.ToLower = "Travaux".ToLower Then

            Else

            End If

            'Enregistrement automatique *************************
            Dim CheminSauvGarde As String = ""
            Dim NomDossier As String = ""

            NomDossier = Environ$("TEMP") & "\DAO\" & MettreApost(dtDoss("TypeMarche").ToString) & "\" & MettreApost(dtDoss("MethodePDM").ToString) & "\" & FormatFileName(MettreApost(dtDoss("NumeroMarche").ToString), "")
            CheminSauvGarde = line & "\DAO\" & MettreApost(dtDoss("TypeMarche").ToString) & "\" & MettreApost(dtDoss("MethodePDM").ToString) & "\" & FormatFileName(MettreApost(dtDoss("NumeroDAO").ToString), "") & "\Marche"

            If (Directory.Exists(NomDossier) = False) Then
                Directory.CreateDirectory(NomDossier)
            End If
            If (Directory.Exists(CheminSauvGarde) = False) Then
                Directory.CreateDirectory(CheminSauvGarde)
            End If

            Dim page1 = NomDossier & "\" & "DonneeMarche1.doc"
            Dim page2 = NomDossier & "\" & "DonneeMarche2.doc"
            Dim page3 = NomDossier & "\" & "DonneeMarche3.doc"
            Dim page4 = NomDossier & "\" & "DonneeMarche4.doc"

            If dtDoss("TypeMarche").ToString.ToLower = "Fournitures".ToLower Then
                If dtDoss("MethodePDM").ToString.ToUpper = "PSL" Or dtDoss("MethodePDM").ToString.ToUpper = "PSO" Or dtDoss("MethodePDM").ToString.ToUpper = "PSC" Then
                    report1.ExportToDisk(ExportFormatType.WordForWindows, page1)
                    report2.ExportToDisk(ExportFormatType.WordForWindows, page2)

                ElseIf dtDoss("MethodePDM").ToString.ToUpper = "AOI" Or dtDoss("MethodePDM").ToString.ToUpper = "AON" Then
                    report1.ExportToDisk(ExportFormatType.WordForWindows, page1)
                    report2.ExportToDisk(ExportFormatType.WordForWindows, page2)
                    report3.ExportToDisk(ExportFormatType.WordForWindows, page3)
                    report4.ExportToDisk(ExportFormatType.WordForWindows, page4)
                End If

            ElseIf dtDoss("TypeMarche").ToString.ToLower = "Travaux".ToLower Then

            Else

            End If

            Dim oWord As New Word.Application
            Dim currentDoc As New Word.Document

            Dim NomFichierpdf As String = "Marche N°_" & FormatFileName(MettreApost(dtDoss("NumeroMarche").ToString), "") & ".pdf"
            Dim NomFichierWord As String = "Marche N°_" & FormatFileName(MettreApost(dtDoss("NumeroMarche").ToString), "") & ".docx"

            Try
                'Ajout de la premiere page
                currentDoc = oWord.Documents.Add(page1)

                If dtDoss("TypeMarche").ToString.ToLower = "Fournitures".ToLower Then
                    If dtDoss("MethodePDM").ToString.ToUpper = "PSL" Or dtDoss("MethodePDM").ToString.ToUpper = "PSO" Or dtDoss("MethodePDM").ToString.ToUpper = "PSC" Then
                        Dim myRange As Word.Range = currentDoc.Bookmarks.Item("\endofdoc").Range
                        Dim mySection1 As Word.Section = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        ' mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                        myRange.InsertFile(page2)

                    ElseIf dtDoss("MethodePDM").ToString.ToUpper = "AOI" Or dtDoss("MethodePDM").ToString.ToUpper = "AON" Then
                        Dim myRange As Word.Range = currentDoc.Bookmarks.Item("\endofdoc").Range
                        Dim mySection1 As Word.Section = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        myRange.InsertFile(page2)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        myRange.InsertFile(page3)
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        myRange.InsertFile(page4)
                    End If

                ElseIf dtDoss("TypeMarche").ToString.ToLower = "Travaux".ToLower Then

                Else

                End If

                Try
                    currentDoc.SaveAs2(FileName:=CheminSauvGarde & "\" & NomFichierWord.ToString, FileFormat:=Word.WdSaveFormat.wdFormatDocumentDefault)
                    currentDoc.SaveAs2(FileName:=CheminSauvGarde & "\" & NomFichierpdf.ToString, FileFormat:=Word.WdSaveFormat.wdFormatPDF)
                    currentDoc.Close(True)
                    oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)

                Catch exp As IOException
                    FinChargement()
                    FailMsg("Un exemplaire du dossier est ouvert par une auttre applicattion. Veuillez le fermer svp.")
                    currentDoc.Close(True)
                    oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                    Return False
                Catch ex As Exception
                    FinChargement()
                    FailMsg("Erreur de traitement" & ex.ToString)
                    currentDoc.Close(True)
                    oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                    Return False
                End Try

            Catch ex As Exception
                FinChargement()
                FailMsg("Erreur de traitement " & ex.ToString)
                currentDoc.Close(True)
                oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                Return False
            End Try

            Return True
        Catch ex As Exception
            FinChargement()
            FailMsg("Erreur de traitement " & ex.ToString)
            Return False
        End Try
    End Function

    Private Function GetSaveArticle(ByVal NumeroMarche As String, ByVal TypeMarche As String, ByVal Methode As String) As Boolean
        Try
            ExecuteNonQuery("delete from t_dao_article_tampon where NumeroMarche='" & NumeroMarche & "' and CodeProjet='" & ProjetEnCours & "' and CodeUtils='" & CodeOperateurEnCours & "'")
            Dim CodeArticle As Array
            Dim OK As Boolean = False
            If TypeMarche.ToLower = "fournitures" Then
                If Methode.ToUpper = "AOI" Or Methode.ToUpper = "AON" Then
                    CodeArticle = {"CCAG 4.2(b)", "CCAG 13.1", "CCAG 15.1", "CCAG 16.5(a)", "CCAG 16.5(b)", "CCAG 18.1(a)", "CCAG 18.1(b)", "CCAG 18.3(a)", "CCAG 18.3(b)", "CCAG 18.4", "CCAG 23.2", "CCAG 24.1", "CCAG 25.1", "CCAG 26.1", "CCAG 26.2", "CCAG 27.1(a)", "CCAG 27.1(b)", "CCAG 28.5, CCAG 28.6", "CCAG 33.4"}
                    OK = True
                ElseIf Methode.ToUpper = "PSL" Or Methode.ToUpper = "PSO" Or Methode.ToUpper = "PSC" Then
                    CodeArticle = {"6", "8", "13", "14", "15"}
                    OK = True
                End If
            ElseIf TypeMarche.ToLower = "Travaux".ToLower Then 'Travaux
                If Methode.ToUpper = "AOI" Or Methode.ToUpper = "AON" Then

                End If
            End If

            Dim dt0 As New DataTable
            If OK = True Then
                For i = 0 To CodeArticle.Length - 1
                    dt0 = ExcecuteSelectQuery("SELECT Description from t_dao_article_tampon where CodeArticle='" & CodeArticle(i) & "' and NumeroMarche='" & NumeroMarche.ToString & "' and CodeProjet ='" & ProjetEnCours & "'")

                    If dt0.Rows.Count > 0 Then
                        For Each rw0 In dt0.Rows
                            ExecuteNonQuery("Insert into t_dao_article_tampon values(NULL, '" & NumeroMarche.ToString & "', '" & CodeArticle(i) & "', '" & EnleverApost(rw0("Description").ToString) & "', '" & CodeOperateurEnCours & "', '" & ProjetEnCours & "')")
                        Next
                    Else
                        ExecuteNonQuery("Insert into t_dao_article_tampon values(NULL, '" & NumeroMarche.ToString & "', '" & CodeArticle(i) & "', '" & GetValDefautArticle(CodeArticle(i), TypeMarche, Methode) & "', '" & CodeOperateurEnCours & "',  '" & ProjetEnCours & "')")
                    End If
                Next
            End If
        Catch ex As Exception
            FinChargement()
            FailMsg("Erreur dans l'enregistrement des articles." & ex.ToString)
            Return False
        End Try
        Return True
    End Function

    Private Function GetValDefautArticle(ByVal CodeArtcile As String, TypeMarche As String, Methode As String) As String

        Dim DescriptionArticle As String = ""
        If TypeMarche.ToLower = "fournitures" Then
            If Methode = "AOI" Or Methode = "AON" Then
                If CodeArtcile = "CCAG 15.1" Then
                    DescriptionArticle = "ne seront pas"
                ElseIf CodeArtcile = "CCAG 18.1(a)" Then
                    DescriptionArticle = "ne sera pas"
                Else
                    DescriptionArticle = "Sans Objet"
                End If
            Else
                DescriptionArticle = "Sans Objet"
            End If

        ElseIf TypeMarche.ToLower = "travaux" Then
            If Methode.ToUpper = "AOI" Or Methode.ToUpper = "AON" Then

            End If
        End If
        Return DescriptionArticle
    End Function
#End Region

#Region "Envoi dossier au bailleur"

    Private Sub EnvoyerLeDossierAuBailleurToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnvoyerLeDossierAuBailleurToolStripMenuItem.Click
        If ViewMarche.RowCount > 0 Then
            Try
                DrX = ViewMarche.GetDataRow(ViewMarche.FocusedRowHandle)
                query = "SELECT M.*, D.MethodePDM FROM t_marchesigne as M, t_dao as D WHERE M.NumeroDAO=D.NumeroDAO and M.NumeroMarche='" & EnleverApost(DrX("N° Marche").ToString) & "' AND M.CodeProjet='" & ProjetEnCours & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                If dt.Rows.Count = 0 Then
                    Exit Sub
                End If
                Dim dtDoss As DataRow = dt.Rows(0)

                'Info de l'envoi de l'email
                If GetVerifDonneEmailBailleur(dtDoss("NumeroDAO").ToString, False) = False Then
                    Exit Sub
                End If

                'Verifier si le fichier existe déjà
                Dim CheminFile As String = line & "\DAO\" & MettreApost(dtDoss("TypeMarche").ToString) & "\" & MettreApost(dtDoss("MethodePDM").ToString) & "\" & FormatFileName(MettreApost(dtDoss("NumeroDAO").ToString), "") & "\Marche"
                Dim NomFichierWord As String = "\Marche N°_" & FormatFileName(DrX("N° Marche").ToString, "") & ".docx"

                If File.Exists(CheminFile & NomFichierWord) = False Then
                    FinChargement()
                    FailMsg("Le dossier à envoyer au bailleur de fonds n'existe pas ou a été supprimé.")
                    Exit Sub
                End If
                Dim MessageText = "Confirmez-vous l'envoi du marché au bailleur [ " & MettreApost(rwDossDAO.Rows(0)("InitialeBailleur").ToString) & " ] ?"

                If ConfirmMsg(MessageText) = DialogResult.Yes Then
                    Try
                        DebutChargement(True, "Envoi du marche au bailleur [ " & MettreApost(rwDossDAO.Rows(0)("InitialeBailleur").ToString) & " ] en cours ...")
                        'Envoi du dossier
                        If EnvoiMailRapport(NomBailleurRetenuDAO, DrX("N° Marche").ToString, EmailDestinatauerDAO, CheminFile & NomFichierWord, EmailCoordinateurProjetDAO, EmailResponsablePMDAO, "Marché", "DAO") = False Then
                            FinChargement()
                            Exit Sub
                        End If
                        FinChargement()
                        SuccesMsg("Dossier envoyé avec succès.")
                    Catch ep As IOException
                        FinChargement()
                        SuccesMsg("Le fichier est utilisé par une autre application" & vbNewLine & "Veuillez le fermer svp.")
                    Catch ex As Exception
                        FinChargement()
                        FailMsg(ex.ToString)
                    End Try
                End If
            Catch exs As Exception
                FailMsg(exs.ToString)
            End Try
        End If
    End Sub

#End Region

#Region "Exportation"
    Private Sub FormatPDFToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FormatPDFToolStripMenuItem.Click
        Try
            If (ViewMarche.RowCount > 0) Then
                DrX = ViewMarche.GetDataRow(ViewMarche.FocusedRowHandle)
                query = "SELECT M.*, D.MethodePDM FROM t_marchesigne as M, t_dao as D WHERE M.NumeroDAO=D.NumeroDAO and M.NumeroMarche='" & EnleverApost(DrX("N° Marche").ToString) & "' AND M.CodeProjet='" & ProjetEnCours & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                If dt.Rows.Count = 0 Then
                    Exit Sub
                End If
                Dim dtDoss As DataRow = dt.Rows(0)

                'Verifier si le fichier existe déjà
                Dim CheminFile As String = line & "\DAO\" & MettreApost(dtDoss("TypeMarche").ToString) & "\" & MettreApost(dtDoss("MethodePDM").ToString) & "\" & FormatFileName(MettreApost(dtDoss("NumeroDAO").ToString), "") & "\Marche"
                Dim NomFichierpdf As String = "\Marche N°_" & FormatFileName(DrX("N° Marche").ToString, "") & ".pdf"

                If File.Exists(CheminFile & NomFichierpdf) = True Then
                    If ExporterPDF(CheminFile & NomFichierpdf, FormatFileName("Marche N° " & DrX("N° Marche").ToString, "") & ".pdf") = False Then
                        Exit Sub
                    End If
                Else
                    FailMsg("Le fichier à exporter n'existe pas ou a été supprimé.")
                End If
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub FormatWordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FormatWordToolStripMenuItem.Click
        Try
            If (ViewMarche.RowCount > 0) Then
                DrX = ViewMarche.GetDataRow(ViewMarche.FocusedRowHandle)
                query = "SELECT M.*, D.MethodePDM FROM t_marchesigne as M, t_dao as D WHERE M.NumeroDAO=D.NumeroDAO and M.NumeroMarche='" & EnleverApost(DrX("N° Marche").ToString) & "' AND M.CodeProjet='" & ProjetEnCours & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                If dt.Rows.Count = 0 Then
                    Exit Sub
                End If
                Dim dtDoss As DataRow = dt.Rows(0)

                'Verifier si le fichier existe déjà
                Dim CheminFile As String = line & "\DAO\" & MettreApost(dtDoss("TypeMarche").ToString) & "\" & MettreApost(dtDoss("MethodePDM").ToString) & "\" & FormatFileName(MettreApost(dtDoss("NumeroDAO").ToString), "") & "\Marche"
                Dim NomFichierpdf As String = "\Marche N°_" & FormatFileName(DrX("N° Marche").ToString, "") & ".docx"

                If File.Exists(CheminFile & NomFichierpdf) = True Then
                    If ExporterWORDfOrmatDocx(CheminFile & NomFichierpdf, FormatFileName("Marche N° " & DrX("N° Marche").ToString, "") & ".docx") = False Then
                        Exit Sub
                    End If
                Else
                    FailMsg("Le fichier à exporter n'existe pas ou a été supprimé.")
                End If
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub
#End Region

#Region "Envoi notification attribution du marché"
    Private Sub ExporterLaNotificationDintentionDattributionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExporterLaNotificationDintentionDattributionToolStripMenuItem.Click
        If ViewMarche.RowCount > 0 Then
            Try
                DrX = ViewMarche.GetDataRow(ViewMarche.FocusedRowHandle)
                query = "select * from T_MarcheSigne where NumeroMarche ='" & EnleverApost(DrX("N° Marche").ToString) & "' and CodeProjet='" & ProjetEnCours & "' and TypeMarche='" & EnleverApost(DrX("Type").ToString) & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                If dt.Rows.Count = 0 Then
                    FailMsg("Aucune information trouvée.")
                    Exit Sub
                End If

                DebutChargement(True, "Chargement de la notification d'attribution du marché...")
                Dim Chemin As String = lineEtat & "\Marches\DAO\Fournitures\DTAO\AOI_AON\Marche\"
                Dim ReportNotification As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table
                ReportNotification.Load(Chemin & "Contrat_NotificationAttribution.rpt")
                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = ReportNotification.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                ReportNotification.SetParameterValue("NumMarche", EnleverApost(dt.Rows(0)("NumeroMarche").ToString))
                ReportNotification.SetParameterValue("NumDAO", EnleverApost(dt.Rows(0)("NumeroDAO").ToString))
                ReportNotification.SetParameterValue("CodeProjet", ProjetEnCours)

                FullScreenReport.FullView.ReportSource = ReportNotification
                FinChargement()
                FullScreenReport.ShowDialog()
            Catch ex As Exception
                FinChargement()
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub EnvoyerLaNotificationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnvoyerLaNotificationToolStripMenuItem.Click
        If ViewMarche.RowCount > 0 Then
            Try
                DrX = ViewMarche.GetDataRow(ViewMarche.FocusedRowHandle)
                query = "select M.NumeroDAO, F.* from T_MarcheSigne AS M, t_fournisseur AS F where M.CodeFournis=F.CodeFournis AND F.DateDepotDAO<>'' AND M.NumeroMarche ='" & EnleverApost(DrX("N° Marche").ToString) & "' and M.CodeProjet='" & ProjetEnCours & "' and M.TypeMarche='" & EnleverApost(DrX("Type").ToString) & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                If dt.Rows.Count = 0 Then
                    FailMsg("Aucune information trouvée.")
                    Exit Sub
                End If
                If dt.Rows(0)("MailFournis").ToString = "" Then
                    FailMsg("L'email du fournissuer est vide.")
                    Exit Sub
                End If

                DebutChargement(True, "Envoi de la notification d'attribution du marché...")
                Dim Chemin As String = lineEtat & "\Marches\DAO\Fournitures\DTAO\AOI_AON\Marche\"
                Dim ReportNotification As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table
                ReportNotification.Load(Chemin & "Contrat_NotificationAttribution.rpt")
                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = ReportNotification.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                ReportNotification.SetParameterValue("NumMarche", EnleverApost(DrX("N° Marche").ToString))
                ReportNotification.SetParameterValue("NumDAO", EnleverApost(dt.Rows(0)("NumeroDAO").ToString))
                ReportNotification.SetParameterValue("CodeProjet", ProjetEnCours)

                Dim NomRepertoire As String = Environ$("TEMP") & "\Notification\"
                If Not System.IO.Directory.Exists(NomRepertoire) Then
                    System.IO.Directory.CreateDirectory(NomRepertoire)
                End If
                FullScreenReport.FullView.ReportSource = ReportNotification

                Dim nomRecu = "NotificationAttributionMarche N° " & FormatFileName(DrX("N° Marche").ToString, "") & ".pdf"
                Dim rep = NomRepertoire & nomRecu
                ReportNotification.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, rep)

                If envoieMail2(EnleverApost(dt.Rows(0)("NomFournis").ToString), EnleverApost(dt.Rows(0)("NumeroDAO").ToString), EnleverApost(dt.Rows(0)("MailFournis").ToString), rep, "NotificationAttributrionMarche") = False Then
                    FinChargement()
                    Exit Sub
                End If
                FinChargement()
                SuccesMsg("La notification d'attibution du marché a été envoyé au fournisseur " & EnleverApost(dt.Rows(0)("NomFournis").ToString) & " avec succès.")
            Catch ex As Exception
                FinChargement()
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub
#End Region

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If ViewMarche.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub GridMarche_MouseUp(sender As Object, e As MouseEventArgs) Handles GridMarche.MouseUp
        If ViewMarche.RowCount > 0 Then
            Try
                DrX = ViewMarche.GetDataRow(ViewMarche.FocusedRowHandle)
                If DrX("Type").ToString.ToLower = "fournitures" Then
                    If DrX("MethodePDM").ToString.ToUpper = "AOI" Or DrX("MethodePDM").ToString.ToUpper = "AON" Then
                        ContextMenuStrip1.Items(4).Visible = True
                        ContextMenuStrip1.Items(5).Visible = True
                    Else
                        ContextMenuStrip1.Items(4).Visible = False
                        ContextMenuStrip1.Items(5).Visible = False
                    End If
                Else
                    ContextMenuStrip1.Items(4).Visible = False
                    ContextMenuStrip1.Items(5).Visible = False
                End If
            Catch ex As Exception
                FinChargement()
                FailMsg(ex.ToString)
            End Try

        End If
    End Sub
End Class