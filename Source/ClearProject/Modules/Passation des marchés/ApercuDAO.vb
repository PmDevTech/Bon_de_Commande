Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions
Imports System.Math
Imports DevExpress.XtraRichEdit
Imports CrystalDecisions.Shared
Imports Microsoft.Office.Interop

Public Class ApercuDAO
    Public NumDoss As String = ""
    Dim TypeMarche As String = ""
    Dim MethodMarche As String = ""

#Region "Appercu DAO"

    Private Sub DaoComplet(Optional ByVal Impression As Boolean = False)

        DebutChargement(True, "Chargement du dossier en cours...")
        Dim Chemin As String = lineEtat & "\Marches\DAO\Fournitures\"
        Dim report, reportplc, reportDocAnnexe, reportFormMarche, reportSpecTech As New ReportDocument
        Dim report2, reportInst, reportDPAO, reportCriteres, reportSoumis, reportPaysEligibles, reportFraude, reportCCAG, reportCCAP, reportCF1, reportInstCF, reportBDQCF, reportDCF, reportCriteresCF, reportSoumisCF, reportCF, reportFormMarcheCF As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table

        Dim DatSet = New DataSet
        Dim typem As String = ""
        query = "select MethodePDM,TypeMarche from T_DAO where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dtDAO = ExcecuteSelectQuery(query)
        For Each rw In dtDAO.Rows
            typem = rw("MethodePDM").ToString
            TypeMarche = rw("TypeMarche").ToString
            MethodMarche = rw("MethodePDM").ToString
        Next
        If typem = "AOI" Then
            report.Load(Chemin & "Prelude.rpt")
            report2.Load(Chemin & "PageGarde.rpt")
            reportInst.Load(Chemin & "Instructions.rpt")
            reportDPAO.Load(Chemin & "DPAO.rpt")
            reportCriteres.Load(Chemin & "Criteres.rpt")
            If TypeMarche = "Fournitures" Then
                reportSoumis.Load(Chemin & "Soumission_FR.rpt")
            ElseIf TypeMarche = "Travaux" Then
                reportSoumis.Load(Chemin & "Soumission_TX.rpt")
            End If
            reportPaysEligibles.Load(Chemin & "PaysEligibles.rpt")
            reportFraude.Load(Chemin & "FraudeEtCorruption.rpt")
            reportCCAG.Load(Chemin & "CCAG.rpt")
            reportCCAP.Load(Chemin & "CCAP.rpt")
            reportDocAnnexe.Load(Chemin & "AnnexesDoc.rpt")
            reportFormMarche.Load(Chemin & "FormulairesMarche.rpt")
            reportSpecTech.Load(Chemin & "SpecTechFournit.rpt")
        ElseIf typem = "AON" Then
            report.Load(Chemin & "Prelude.rpt")
            report2.Load(Chemin & "PageGarde.rpt")
            reportInst.Load(Chemin & "Instructions.rpt")
            reportDPAO.Load(Chemin & "DPAO.rpt")
            reportCriteres.Load(Chemin & "Criteres.rpt")
            If TypeMarche = "Fournitures" Then
                reportSoumis.Load(Chemin & "Soumission_FR.rpt")
            ElseIf TypeMarche = "Travaux" Then
                reportSoumis.Load(Chemin & "Soumission_TX.rpt")
            End If
            reportPaysEligibles.Load(Chemin & "PaysEligibles.rpt")
            reportFraude.Load(Chemin & "FraudeEtCorruption.rpt")
            reportCCAG.Load(Chemin & "CCAG.rpt")
            reportCCAP.Load(Chemin & "CCAP.rpt")
            reportDocAnnexe.Load(Chemin & "AnnexesDoc.rpt")
            reportFormMarche.Load(Chemin & "FormulairesMarche.rpt")
            reportSpecTech.Load(Chemin & "SpecTechFournit.rpt")
        ElseIf typem = "PSC" Then
            reportplc.Load(Chemin & "PLC.rpt")
        ElseIf typem = "PSL" Or typem = "PSO" Then
            report.Load(Chemin & "Prelude.rpt")
            report2.Load(Chemin & "PageGarde.rpt")
            reportInstCF.Load(Chemin & "Instructions_CF.rpt")
            reportDCF.Load(Chemin & "DCF.rpt")
            reportCriteresCF.Load(Chemin & "Criteres_CF.rpt")
            reportSoumisCF.Load(Chemin & "Soumission_CF.rpt")
            reportCF.Load(Chemin & "TCC_CF.rpt")
            reportBDQCF.Load(Chemin & "BDQ_CF.rpt")
            reportFormMarcheCF.Load(Chemin & "FormulairesMarche_CF.rpt")
        End If

        If typem = "PSC" Then
            reportplc.SetParameterValue("NumDao", NumDoss)
            reportplc.SetParameterValue("NumDao", NumDoss, "T1.1.rpt")
            reportplc.SetParameterValue("NumDao", NumDoss, "T1.2.rpt")
            reportplc.SetParameterValue("NumDao", NumDoss, "T1.3.rpt")
            reportplc.SetParameterValue("NumDao", NumDoss, "T1.4.rpt")
        ElseIf typem = "PSL" Or typem = "PSO" Then
            report.SetParameterValue("NumDao", NumDoss)
            report2.SetParameterValue("NumDao", NumDoss)
            reportDCF.SetParameterValue("NumDao", NumDoss)
            reportDCF.SetParameterValue("CodeProjet", ProjetEnCours)
            reportCriteresCF.SetParameterValue("NumDao", NumDoss)
            reportSoumisCF.SetParameterValue("NumDao", NumDoss)
            reportCF.SetParameterValue("NumDao", NumDoss)
            reportBDQCF.SetParameterValue("NumDao", NumDoss)
            reportFormMarcheCF.SetParameterValue("NumDao", NumDoss)
        Else

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = reportSpecTech.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            CrTables = reportplc.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next
            reportplc.SetDataSource(DatSet)
            reportSpecTech.SetDataSource(DatSet)
            ' ****************************** Paramètres page de présentations ***********************************
            report.SetParameterValue("NumDao", NumDoss)
            report2.SetParameterValue("NumDao", NumDoss)
            reportDPAO.SetParameterValue("NumDao", NumDoss)
            reportSoumis.SetParameterValue("NumDao1", NumDoss)
            If TypeMarche = "Fournitures" Then
            ElseIf TypeMarche = "Travaux" Then
                reportSoumis.SetParameterValue("NumDao2", NumDoss)
                reportSoumis.SetParameterValue("NumDao3", NumDoss)
                reportSoumis.SetParameterValue("NumDao4", NumDoss)
                reportSoumis.SetParameterValue("NumDao5", NumDoss)
            End If
            reportSpecTech.SetParameterValue("NumDaoSpec", NumDoss)
        End If
        query = "select PaysProjet,MinistereTutelle,NomProjet,LogoProjet,AdresseProjet,VilleProjet,BoitePostaleProjet,TelProjet,FaxProjet,MailProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows

            If typem = "PSC" Then
                reportplc.SetParameterValue("CodeProjet", ProjetEnCours)
            ElseIf typem = "PSL" Or typem = "PSO" Then
                report.SetParameterValue("CodeProjet", ProjetEnCours)
                report2.SetParameterValue("CodeProjet", ProjetEnCours)
                report.SetParameterValue("PaysProjet", MettreApost(rw(0).ToString))
                report.SetParameterValue("Ministere", MettreApost(rw(1).ToString))
                report2.SetParameterValue("Ministere", MettreApost(rw(1).ToString).ToString().ToUpper)
                report.SetParameterValue("NomProjet", MettreApost(rw(2).ToString))
                report2.SetParameterValue("NomProjet", MettreApost(rw(2).ToString).ToString().ToUpper)
                report.SetParameterValue("AdresseProjet", MettreApost(rw(4).ToString))
                report.SetParameterValue("VilleProjet", MettreApost(rw(5).ToString))
                report.SetParameterValue("BpProjet", MettreApost(rw(6).ToString.ToUpper))
                report.SetParameterValue("TelProjet", MettreApost(rw(7).ToString))
                report.SetParameterValue("FaxProjet", MettreApost(rw(8).ToString))
                report.SetParameterValue("MailProjet", MettreApost(rw(9).ToString).ToString().ToLower())
            Else
                report.SetParameterValue("CodeProjet", ProjetEnCours)
                report2.SetParameterValue("CodeProjet", ProjetEnCours)
                report.SetParameterValue("PaysProjet", MettreApost(rw(0).ToString))
                report.SetParameterValue("Ministere", MettreApost(rw(1).ToString))
                report2.SetParameterValue("Ministere", MettreApost(rw(1).ToString).ToString().ToUpper)
                report.SetParameterValue("NomProjet", MettreApost(rw(2).ToString))
                report2.SetParameterValue("NomProjet", MettreApost(rw(2).ToString).ToString().ToUpper)
                report.SetParameterValue("AdresseProjet", MettreApost(rw(4).ToString))
                report.SetParameterValue("VilleProjet", MettreApost(rw(5).ToString))
                report.SetParameterValue("BpProjet", MettreApost(rw(6).ToString.ToUpper))
                report.SetParameterValue("TelProjet", MettreApost(rw(7).ToString))
                report.SetParameterValue("FaxProjet", MettreApost(rw(8).ToString))
                reportDPAO.SetParameterValue("CodeProjet", ProjetEnCours)
                reportCCAP.SetParameterValue("CodeProjet", ProjetEnCours)
                reportDPAO.SetParameterValue("PaysProjet", MettreApost(rw(0).ToString))
                reportDPAO.SetParameterValue("Ministere", MettreApost(rw(1).ToString))
                reportCCAP.SetParameterValue("Ministere", MettreApost(rw(1).ToString).ToString().ToUpper)
                reportDPAO.SetParameterValue("NomProjet", MettreApost(rw(2).ToString))
                reportDPAO.SetParameterValue("AdresseProjet", MettreApost(rw(4).ToString))
                reportDPAO.SetParameterValue("VilleProjet", MettreApost(rw(5).ToString))
                reportDPAO.SetParameterValue("BpProjet", MettreApost(rw(6).ToString.ToUpper))
                reportDPAO.SetParameterValue("TelProjet", MettreApost(rw(7).ToString))
                reportDPAO.SetParameterValue("FaxProjet", MettreApost(rw(8).ToString))
                reportDPAO.SetParameterValue("MailProjet", MettreApost(rw(9).ToString).ToString().ToLower)
                report.SetParameterValue("MailProjet", MettreApost(rw(9).ToString).ToString().ToLower)
            End If
        Next

        'Données du DAO ***********************
        Dim NroCompte As String = ""
        Dim PrctGarantie As String = ""
        Dim methodePDM As String = ""
        query = "select MethodePDM,TypeMarche,NbreLotDAO,DelaiExecution,PrixDAO,CompteAchat,DateLimiteRemise,PourcGarantie,DatePublication,NumPublication,JournalPublication,DateEdition,NbreMembreGroup,PreQualif,DateReunionPrepa,LangueSoumission,ValiditeOffre,ValiditeCaution,NbCopieSoumission,NomConciliateur,MontConciliateur,DateOuverture,DesignConciliateur,DesignAdresse from T_DAO where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            methodePDM = rw(0).ToString
            If (rw(0).ToString <> "") Then

                If (rw(0).ToString = "AON") Then
                    report.SetParameterValue("LibelleMethodePdm", "Appel d'Offres National")
                    report2.SetParameterValue("LibelleMethodePdm", "APPEL D'OFFRES NATIONAL")
                    reportInst.SetParameterValue("LibelleMethodePdm", "Appel d'Offres National")
                    reportDPAO.SetParameterValue("LibelleMethodePdm", "Appel d'Offres National")
                    report.SetParameterValue("MethodePdm", rw(0).ToString)
                    reportInst.SetParameterValue("MethodePdm", rw(0).ToString)
                    reportDPAO.SetParameterValue("MethodePdm", rw(0).ToString)
                ElseIf (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("LibelleMethodePdm", "Demande de Cotation")
                    report2.SetParameterValue("LibelleMethodePdm", "DEMANDE DE COTATION")
                    report.SetParameterValue("MethodePdm", rw(0).ToString)
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("LibelleMethodePdm", "Appel d'Offres International")
                    report2.SetParameterValue("LibelleMethodePdm", "APPEL D'OFFRES INTERNATIONAL")
                    reportInst.SetParameterValue("LibelleMethodePdm", "Appel d'Offres International")
                    reportDPAO.SetParameterValue("LibelleMethodePdm", "Appel d'Offres International")
                    report.SetParameterValue("MethodePdm", rw(0).ToString)
                    reportInst.SetParameterValue("MethodePdm", rw(0).ToString)
                    reportDPAO.SetParameterValue("MethodePdm", rw(0).ToString)
                End If
            Else
                report.SetParameterValue("MethodePdm", "<ND>")
                reportInst.SetParameterValue("MethodePdm", "<ND>")
                report.SetParameterValue("LibelleMethodePdm", "<ND>")
                report2.SetParameterValue("LibelleMethodePdm", "<ND>")
                reportInst.SetParameterValue("LibelleMethodePdm", "<ND>")
                reportDPAO.SetParameterValue("MethodePdm", "<ND>")
            End If

            If (rw(1).ToString <> "") Then
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("TypeMarche", rw(1).ToString)
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("TypeMarche", rw(1).ToString)
                    reportDPAO.SetParameterValue("TypeMarche", rw(1).ToString)
                End If

            Else
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("TypeMarche", "<ND>")
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("TypeMarche", "<ND>")
                    reportDPAO.SetParameterValue("TypeMarche", "<ND>")
                End If

            End If

            If (rw(2).ToString <> "") Then
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("NbreLot", MontantLettre(rw(2).ToString) & " (" & rw(2).ToString & ")")
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("NbreLot", MontantLettre(rw(2).ToString) & " (" & rw(2).ToString & ")")
                    reportDPAO.SetParameterValue("NbreLot", MontantLettre(rw(2).ToString) & " (" & rw(2).ToString & ")")
                End If
            Else
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("NbreLot", "<ND>")
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("NbreLot", "<ND>")
                    reportDPAO.SetParameterValue("NbreLot", "<ND>")
                End If

            End If

            If (rw(3).ToString <> "") Then
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("DelaiExecut", rw(3).ToString)
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("DelaiExecut", rw(3).ToString)
                    reportDPAO.SetParameterValue("DelaiExecut", rw(3).ToString)
                End If

            Else
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("DelaiExecut", "<ND>")
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("DelaiExecut", "<ND>")
                    reportDPAO.SetParameterValue("DelaiExecut", "<ND>")
                End If

            End If

            If (rw(0).ToString = "PSC") Then
            Else
                If (rw(4).ToString <> "") Then
                    report.SetParameterValue("PrixDAO", AfficherMonnaie(rw(4).ToString) & " Fcfa")
                Else
                    report.SetParameterValue("PrixDAO", "<ND>")
                End If

                If (rw(5).ToString <> "") Then
                    Dim nomCompte = ExecuteScallar("SELECT LibelleCompte FROM t_comptebancaire WHERE NumeroCompte='" & rw(5).ToString & "'")
                    report.SetParameterValue("NomCompte", nomCompte)
                    NroCompte = rw(5).ToString
                Else
                    report.SetParameterValue("NomCompte", "<ND>")
                End If
            End If


            If (rw(6).ToString <> "") Then
                Dim DatCoup() As String = rw(6).ToString.Split(" "c)
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("DateFinDepot", CDate(DatCoup(0)).ToLongDateString)
                    report.SetParameterValue("HeureFinDepot", CDate(DatCoup(1)).ToLongTimeString)
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("DateFinDepot", CDate(DatCoup(0)).ToLongDateString)
                    report.SetParameterValue("HeureFinDepot", CDate(DatCoup(1)).ToLongTimeString)
                    reportDPAO.SetParameterValue("DateFinDepot", CDate(DatCoup(0)).ToLongDateString)
                    reportDPAO.SetParameterValue("HeureFinDepot", CDate(DatCoup(1)).ToLongTimeString)
                End If

            Else
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("DateFinDepot", "<ND>")
                    report.SetParameterValue("HeureFinDepot", "<ND>")
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("DateFinDepot", "<ND>")
                    report.SetParameterValue("HeureFinDepot", "<ND>")
                    reportDPAO.SetParameterValue("DateFinDepot", "<ND>")
                    reportDPAO.SetParameterValue("HeureFinDepot", "<ND>")
                End If

            End If

            If (rw(21).ToString <> "") Then
                Dim DatCoup() As String = rw(21).ToString.Split(" "c)
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("DateOuverture", CDate(DatCoup(0)).ToLongDateString)
                    report.SetParameterValue("HeureOuverture", CDate(DatCoup(1)).ToLongTimeString)
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("DateOuverture", CDate(DatCoup(0)).ToLongDateString)
                    report.SetParameterValue("HeureOuverture", CDate(DatCoup(1)).ToLongTimeString)
                    reportDPAO.SetParameterValue("DateOuverture", CDate(DatCoup(0)).ToLongDateString)
                    reportDPAO.SetParameterValue("HeureOuverture", CDate(DatCoup(1)).ToLongTimeString)
                End If

            Else
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("DateOuverture", "<ND>")
                    report.SetParameterValue("HeureOuverture", "<ND>")
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("DateOuverture", "<ND>")
                    report.SetParameterValue("HeureOuverture", "<ND>")
                    reportDPAO.SetParameterValue("DateOuverture", "<ND>")
                    reportDPAO.SetParameterValue("HeureOuverture", "<ND>")
                End If

            End If

            If (rw(0).ToString = "PSC") Then
            Else
                If (rw(7).ToString <> "") Then
                    PrctGarantie = rw(7).ToString.Replace("%", "")
                Else
                    PrctGarantie = "0"
                End If

                If (rw(8).ToString <> "") Then
                    'RemplacerTexte("[DatePub]", rw(8).ToString, Doc)
                End If

                If (rw(9).ToString <> "") Then
                    'RemplacerTexte("[NumPub]", rw(9).ToString, Doc)
                End If

                If (rw(10).ToString <> "") Then
                    'RemplacerTexte("[JournalPub]", rw(10).ToString, Doc)
                End If

                If (rw(11).ToString <> "") Then
                    report2.SetParameterValue("DateEdition", CDate(rw(11)).ToString("MMMM").ToUpper & " " & CDate(rw(11)).ToString("yyyy"))
                Else
                    report2.SetParameterValue("DateEdition", "<ND>")
                End If
            End If

            If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                Dim DelaiValid As String = ""
                If (rw(16).ToString <> "") Then
                    DelaiValid = rw(16).ToString
                End If

                Dim DelaiValidC As String = ""
                If (rw(17).ToString <> "") Then
                    DelaiValidC = rw(17).ToString
                End If
            ElseIf (rw(0).ToString = "PSC") Then

            Else
                If (rw(12).ToString <> "") Then
                    reportDPAO.SetParameterValue("NbreMembreGroupe", MontantLettre(rw(12).ToString) & " (" & rw(12).ToString & ")")
                Else
                    reportDPAO.SetParameterValue("NbreMembreGroupe", "<ND>")
                End If

                If (rw(13).ToString <> "OUI") Then
                    reportDPAO.SetParameterValue("PreQualif", "Le présent appel d'offres n'est pas précédé d'une préqualification.")
                Else
                    reportDPAO.SetParameterValue("PreQualif", "Le présent appel d'offres est précédé d'une préqualification.")
                End If

                If (rw(14).ToString <> "") Then
                    Dim PartDate() As String = rw(14).ToString.Split(" "c)
                    reportDPAO.SetParameterValue("DateReunionPrepa", CDate(PartDate(0)).ToLongDateString & " à " & PartDate(1))
                Else
                    reportDPAO.SetParameterValue("DateReunionPrepa", "<ND>")
                End If

                If (rw(15).ToString <> "") Then
                    reportDPAO.SetParameterValue("LangueOffre", rw(15).ToString)
                    reportCCAP.SetParameterValue("LangueOffre", rw(15).ToString)
                Else
                    reportDPAO.SetParameterValue("LangueOffre", "<ND>")
                    reportCCAP.SetParameterValue("LangueOffre", "<ND>")
                End If

                Dim DelaiValid As String = ""
                If (rw(16).ToString <> "") Then
                    reportDPAO.SetParameterValue("ValiditeOffre", rw(16).ToString)
                    DelaiValid = rw(16).ToString
                Else
                    reportDPAO.SetParameterValue("ValiditeOffre", "<ND>")
                End If

                Dim DelaiValidC As String = ""
                If (rw(17).ToString <> "") Then
                    reportDPAO.SetParameterValue("ValiditeCaution", rw(17).ToString)
                    DelaiValidC = rw(17).ToString
                Else
                    reportDPAO.SetParameterValue("ValiditeCaution", "<ND>")
                End If

                If (DelaiValid <> "" And DelaiValidC <> "") Then
                    Dim PartOffre() As String = DelaiValid.Split(" "c)
                    Dim NumOffre As Decimal = CInt(PartOffre(0))
                    If (PartOffre(1) = "Semaines") Then
                        NumOffre = NumOffre * 7
                    ElseIf (PartOffre(1) = "Mois") Then
                        NumOffre = NumOffre * 30
                    End If

                    Dim PartCaut() As String = DelaiValidC.Split(" "c)
                    Dim NumCaut As Decimal = CInt(PartCaut(0))
                    If (PartCaut(1) = "Semaines") Then
                        NumCaut = NumCaut * 7
                    ElseIf (PartCaut(1) = "Mois") Then
                        NumCaut = NumCaut * 30
                    End If
                    reportDPAO.SetParameterValue("ExcedentValiditeCaution", Abs(NumCaut - NumOffre).ToString & " Jours")
                Else
                    reportDPAO.SetParameterValue("ExcedentValiditeCaution", "<ND>")
                End If

                If (rw(18).ToString <> "") Then
                    reportDPAO.SetParameterValue("NbreCopies", MontantLettre(rw(18).ToString) & " (" & rw(18).ToString & ")")
                Else
                    reportDPAO.SetParameterValue("NbreCopies", "<ND>")
                End If

                If (rw(19).ToString <> "") Then
                    reportDPAO.SetParameterValue("NomConciliateur", MettreApost(rw(19).ToString))
                Else
                    reportDPAO.SetParameterValue("NomConciliateur", "<ND>")
                End If

                If (rw(20).ToString <> "") Then
                    reportDPAO.SetParameterValue("MontConciliateur", AfficherMonnaie(rw(20).ToString))
                    reportCCAP.SetParameterValue("MontConciliateur", AfficherMonnaie(rw(20).ToString))
                Else
                    reportDPAO.SetParameterValue("MontConciliateur", "<ND>")
                    reportCCAP.SetParameterValue("MontConciliateur", "<ND>")
                End If

                If (rw(22).ToString <> "") Then
                    reportDPAO.SetParameterValue("DesignConcil", MettreApost(rw(22).ToString))
                    reportCCAP.SetParameterValue("DesignConcil", MettreApost(rw(22).ToString))
                Else
                    reportDPAO.SetParameterValue("DesignConcil", "<ND>")
                    reportCCAP.SetParameterValue("DesignConcil", "<ND>")
                End If

                If (rw(23).ToString <> "") Then
                    reportDPAO.SetParameterValue("AdresseDesign", MettreApost(rw(23).ToString))
                    reportCCAP.SetParameterValue("AdresseDesign", MettreApost(rw(23).ToString))
                Else
                    reportDPAO.SetParameterValue("AdresseDesign", "<ND>")
                    reportCCAP.SetParameterValue("AdresseDesign", "<ND>")
                End If
            End If

        Next

        'Membres du COJO **********************
        Dim NbreMem As Decimal = 0
        Dim ListMem As String = ""
        query = "select NomMem from T_Commission where NumeroDAO='" & NumDoss & "' order by NomMem"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            If (NbreMem > 0) Then
                ListMem = ListMem & vbNewLine
            End If
            ListMem = ListMem & (NbreMem + 1).ToString & "./    " & MettreApost(rw(0).ToString)
            NbreMem = NbreMem + 1
        Next

        If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
        ElseIf (methodePDM.ToString = "PSC") Then
        Else
            If (NbreMem > 0) Then
                reportDPAO.SetParameterValue("MembresCojo", ListMem)
            Else
                reportDPAO.SetParameterValue("MembresCojo", "<ND>")
            End If
        End If

        'Données du marché *********************
        Dim CodeMarche As Decimal = 0
        Dim LeBaill As String = ""
        Dim LibMarc As String = ""
        query = "select RefMarche,DescriptionMarche,MontantEstimatif,InitialeBailleur from T_Marche where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CodeMarche = rw(0)
            LeBaill = rw(3)
            If (LibMarc <> "") Then
                LibMarc = LibMarc & vbNewLine & " et " & vbNewLine
            End If
            LibMarc = LibMarc & rw(1).ToString
            If (PrctGarantie <> "") Then
                Dim MontGarant As Decimal = (CDec(PrctGarantie) / 100) * CDec(rw(2))
                MontGarant = Ceiling(MontGarant)
                If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
                    report.SetParameterValue("MontantGarantie", MontantLettre(MontGarant.ToString).Replace(" zero", "") & " (" & AfficherMonnaie(MontGarant.ToString) & ")" & " de Fcfa")
                ElseIf (methodePDM.ToString = "PSC") Then
                Else
                    report.SetParameterValue("MontantGarantie", MontantLettre(MontGarant.ToString).Replace(" zero", "") & " (" & AfficherMonnaie(MontGarant.ToString) & ")" & " de Fcfa")
                    reportDPAO.SetParameterValue("MontantGarantie", MontantLettre(MontGarant.ToString).Replace(" zero", "") & " (" & AfficherMonnaie(MontGarant.ToString) & ")" & " de Fcfa")
                End If

            End If
        Next

        If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
            report.SetParameterValue("LibelleMarche", MettreApost(LibMarc))
            report2.SetParameterValue("LibelleMarche", MettreApost(LibMarc).ToString().ToUpper)
        ElseIf (methodePDM.ToString = "PSC") Then
        Else
            report.SetParameterValue("LibelleMarche", MettreApost(LibMarc))
            report2.SetParameterValue("LibelleMarche", MettreApost(LibMarc).ToString().ToUpper)
            reportDPAO.SetParameterValue("LibelleMarche", MettreApost(LibMarc))
        End If

        ' La convention ****************************
        query = "select C.CodeConvention,C.TypeConvention,C.MontantConvention from T_Convention as C, T_Bailleur as B where B.CodeBailleur=C.CodeBailleur and B.InitialeBailleur='" & LeBaill & "' and B.CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows

            If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
                report.SetParameterValue("TypeConv", rw(1).ToString)
                report2.SetParameterValue("TypeConv", rw(1).ToString.ToUpper)
                report.SetParameterValue("NumConv", rw(0).ToString)
                report2.SetParameterValue("NumConv", rw(0).ToString)
                report.SetParameterValue("Bailleur", LeBaill)
                report2.SetParameterValue("Bailleur", LeBaill)
            ElseIf (methodePDM.ToString = "PSC") Then
            Else
                report.SetParameterValue("TypeConv", rw(1).ToString)
                report2.SetParameterValue("TypeConv", rw(1).ToString.ToUpper)
                report.SetParameterValue("NumConv", rw(0).ToString)
                report2.SetParameterValue("NumConv", rw(0).ToString)
                report.SetParameterValue("Bailleur", LeBaill)
                report2.SetParameterValue("Bailleur", LeBaill)
                reportDPAO.SetParameterValue("TypeConv", rw(1).ToString)
                reportDPAO.SetParameterValue("NumConv", rw(0).ToString)
                reportDPAO.SetParameterValue("Bailleur", LeBaill)
            End If
        Next

        'Montant total de la convention **************
        Dim TotConv As Decimal = 0
        query = "select C.MontantConvention from T_Convention as C,T_Bailleur as B where C.CodeBailleur=B.CodeBailleur and B.CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            TotConv = TotConv + CDec(rw(0))
        Next

        If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
        ElseIf (methodePDM.ToString = "PSC") Then
        Else
            reportDPAO.SetParameterValue("MontTotConv", AfficherMonnaie(TotConv.ToString) & " de francs CFA")
        End If

        'Données du compte d'achat DAO **********************
        query = "select C.NumeroCompte,B.CodeBanque,B.PaysBanque,B.AdresseBanque from T_CompteBancaire as C,T_Banque as B where C.RefBanque=B.RefBanque and C.NumeroCompte='" & NroCompte & "' and C.CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
                report.SetParameterValue("NumCompte", rw(0).ToString)
                report.SetParameterValue("CodeBanque", rw(1).ToString)
                report.SetParameterValue("PaysBanque", MettreApost(rw(2).ToString).ToString().ToUpper)
                report.SetParameterValue("AdresseBanque", MettreApost(rw(3).ToString))
            ElseIf (methodePDM.ToString = "PSC") Then
            Else
                report.SetParameterValue("NumCompte", rw(0).ToString)
                report.SetParameterValue("CodeBanque", rw(1).ToString)
                report.SetParameterValue("PaysBanque", MettreApost(rw(2).ToString).ToString().ToUpper)
                report.SetParameterValue("AdresseBanque", MettreApost(rw(3).ToString))
            End If
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
        If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
            report2.SetParameterValue("CodeCompo", CodComp)
        ElseIf (methodePDM.ToString = "PSC") Then
        Else
            report2.SetParameterValue("CodeCompo", CodComp)
            reportDPAO.SetParameterValue("CodeCompo", CodComp)
        End If
        query = "select LibellePartition from T_Partition where LibelleCourt='" & CodComp & "' and CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows

            If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
                report2.SetParameterValue("LibCompo", MettreApost(rw(0).ToString).ToString().ToUpper)
            ElseIf (methodePDM.ToString = "PSC") Then
            Else
                report2.SetParameterValue("LibCompo", MettreApost(rw(0).ToString).ToString().ToUpper)
                reportDPAO.SetParameterValue("LibCompo", MettreApost(rw(0).ToString))
            End If
        Next

        '       Sous Composante   *****
        Dim CodSouComp As String = IIf(Len(CodActiv1) = 6, Mid(CodActiv1, 1, 3), Mid(CodActiv1, 1, 2))

        If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
            report2.SetParameterValue("CodeSouCompo", CodSouComp)
        ElseIf (methodePDM.ToString = "PSC") Then
        Else
            report2.SetParameterValue("CodeSouCompo", CodSouComp)
            reportDPAO.SetParameterValue("CodeSouCompo", CodSouComp)
        End If
        query = "select LibellePartition from T_Partition where LibelleCourt='" & CodSouComp & "' and CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows

            If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
                report2.SetParameterValue("LibSouCompo", MettreApost(rw(0).ToString).ToString().ToUpper)
            ElseIf (methodePDM.ToString = "PSC") Then
            Else
                report2.SetParameterValue("LibSouCompo", MettreApost(rw(0).ToString).ToString().ToUpper)
                reportDPAO.SetParameterValue("LibSouCompo", MettreApost(rw(0).ToString))
            End If
        Next

        ' Avis général de passation des marchés **************
        If (methodePDM.ToString = "PSC") Then
        Else
            report.SetParameterValue("NumPubAG", "<ND>")
            report.SetParameterValue("DatePubAG", "<ND>")
            report.SetParameterValue("MediaPubAG", "<ND>")
        End If

        ' Annexes du dossier **********
        ' CV du Conciliateur **************************
        Dim NomDossier As String = FormatFileName(line & "\DAO\" & TypeMarche & "\" & MethodMarche & "\" & NumDoss, "")
        If File.Exists(NomDossier & "\CV_CONCILIATEUR.pdf") Or File.Exists(NomDossier & "\CV_CONCILIATEUR.doc") Or File.Exists(NomDossier & "\CV_CONCILIATEUR.docx") Then
            TabAnnexes.TabPages.Add("CV Conciliateur")

            If (File.Exists(NomDossier & "\CV_CONCILIATEUR.pdf") = True) Then
                Dim Web1 As New WebBrowser
                Web1.Dock = DockStyle.Fill
                Web1.Navigate(NomDossier & "\CV_CONCILIATEUR.pdf")
                TabAnnexes.TabPages.Item(1).Controls.Add(Web1)

            ElseIf (File.Exists(NomDossier & "\CV_CONCILIATEUR.doc") = True) Then
                Dim NewWord As New RichEditControl
                NewWord.LoadDocument(NomDossier & "\CV_CONCILIATEUR.doc", DevExpress.XtraRichEdit.DocumentFormat.Doc)
                NewWord.Dock = DockStyle.Fill
                TabAnnexes.TabPages.Item(1).Controls.Add(NewWord)

            ElseIf (File.Exists(NomDossier & "\CV_CONCILIATEUR.docx") = True) Then
                Dim NewWord As New RichEditControl
                NewWord.LoadDocument(NomDossier & "\CV_CONCILIATEUR.docx", DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
                NewWord.Dock = DockStyle.Fill
                TabAnnexes.TabPages.Item(1).Controls.Add(NewWord)

            Else
                Dim NewWord As New RichEditControl
                NewWord.Dock = DockStyle.Fill
                TabAnnexes.TabPages.Item(1).Controls.Add(NewWord)
            End If
        End If

        If (Impression = True) Then
            report2.PrintToPrinter(1, True, 0, 0)
        Else
            If MethodMarche = "PSC" Then
                ViewDPAO.ReportSource = reportplc
                XtraTabControl2.TabPages.Item(3).Text = "Demande de cotation"
                XtraTabControl2.TabPages.Item(0).PageVisible = False
                XtraTabControl2.TabPages.Item(1).PageVisible = False
                XtraTabControl2.TabPages.Item(2).PageVisible = False
                XtraTabControl2.TabPages.Item(4).PageVisible = False
                XtraTabControl2.TabPages.Item(5).PageVisible = False
                XtraTabControl2.TabPages.Item(6).PageVisible = False
                XtraTabControl2.TabPages.Item(7).PageVisible = False
                XtraTabControl2.TabPages.Item(8).PageVisible = False
                XtraTabControl2.TabPages.Item(9).PageVisible = False
                XtraTabControl2.TabPages.Item(10).PageVisible = False
                XtraTabControl2.TabPages.Item(11).PageVisible = False
                XtraTabControl2.TabPages.Item(12).PageVisible = False

            ElseIf MethodMarche = "PSL" Or MethodMarche = "PSO" Then
                ViewPrelude.ReportSource = report
                ViewCouverture.ReportSource = report2
                ViewInstructions.ReportSource = reportInstCF
                ViewDPAO.ReportSource = reportDCF
                ViewCriteres.ReportSource = reportCriteresCF
                ViewFormSoumis.ReportSource = reportSoumisCF
                ViewPaysEligibles.ReportSource = reportCF
                ViewFraude.ReportSource = reportBDQCF
                ViewFormMarche.ReportSource = reportFormMarcheCF

                XtraTabControl2.TabPages.Item(9).PageVisible = False
                XtraTabControl2.TabPages.Item(10).PageVisible = False
                XtraTabControl2.TabPages.Item(12).PageVisible = False
                XtraTabControl2.TabPages.Item(8).PageVisible = False
                XtraTabControl2.TabPages.Item(3).Text = "Demande cotation"
                XtraTabControl2.TabPages.Item(6).Text = "Comparatif"
                XtraTabControl2.TabPages.Item(7).Text = "Bordereau"
            Else
                ViewPrelude.ReportSource = report
                ViewCouverture.ReportSource = report2
                ViewInstructions.ReportSource = reportInst
                ViewDPAO.ReportSource = reportDPAO
                ViewCriteres.ReportSource = reportCriteres
                ViewFormSoumis.ReportSource = reportSoumis
                ViewPaysEligibles.ReportSource = reportPaysEligibles
                ViewFraude.ReportSource = reportFraude
                If (TypeMarche = "Travaux") Then
                    EditSpecTravaux.Visible = True
                    ViewSpecTech.Visible = False
                    EditSpecTravaux.Dock = DockStyle.Fill
                    ChargerRtf(Chemin & "SpecTravaux.rtf", EditSpecTravaux)
                Else
                    EditSpecTravaux.Visible = False
                    ViewSpecTech.Visible = True
                    ViewSpecTech.Dock = DockStyle.Fill
                    ViewSpecTech.ReportSource = reportSpecTech
                End If

                ViewCAG.ReportSource = reportCCAG
                ViewCAP.ReportSource = reportCCAP
                ViewFormMarche.ReportSource = reportFormMarche

                'Dim LesAnnexes As String = ""
                'PageAjout()
                'For i As Integer = 1 To TabAnnexes.TabPages.Count - 2
                '    LesAnnexes = LesAnnexes & TabAnnexes.TabPages.Item(i).Text & vbNewLine
                'Next
                'reportDocAnnexe.SetParameterValue("Annexes", "BONJOUR") 'LesAnnexes)
                'ViewDocAnnexe.ReportSource = reportDocAnnexe

                XtraTabControl2.TabPages.Item(9).PageVisible = True
                XtraTabControl2.TabPages.Item(10).PageVisible = True
                XtraTabControl2.TabPages.Item(12).PageVisible = True
                XtraTabControl2.TabPages.Item(8).PageVisible = True
                XtraTabControl2.TabPages.Item(3).Text = "DPAO"
                XtraTabControl2.TabPages.Item(6).Text = "Pays éligibles"
                XtraTabControl2.TabPages.Item(7).Text = "Fraude"
            End If

            ' Enregistrement automatique *************************
            NomDossier = FormatFileName(line & "\DAO\" & TypeMarche & "\" & MethodMarche & "\" & NumDoss, "")
            If (Directory.Exists(NomDossier) = True) Then
                Directory.CreateDirectory(NomDossier & "\Dossier")

                Try
                    If MethodMarche = "PSC" Then
                        With crConnectionInfo
                            .ServerName = ODBCNAME
                            .DatabaseName = DB
                            .UserID = USERNAME
                            .Password = PWD
                        End With

                        CrTables = reportplc.Database.Tables
                        For Each CrTable In CrTables
                            crtableLogoninfo = CrTable.LogOnInfo
                            crtableLogoninfo.ConnectionInfo = crConnectionInfo
                            CrTable.ApplyLogOnInfo(crtableLogoninfo)
                        Next
                        reportplc.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\PLC.pdf")
                    ElseIf MethodMarche = "PSL" Or MethodMarche = "PSO" Then
                        With crConnectionInfo
                            .ServerName = ODBCNAME
                            .DatabaseName = DB
                            .UserID = USERNAME
                            .Password = PWD
                        End With

                        CrTables = reportInstCF.Database.Tables
                        For Each CrTable In CrTables
                            crtableLogoninfo = CrTable.LogOnInfo
                            crtableLogoninfo.ConnectionInfo = crConnectionInfo
                            CrTable.ApplyLogOnInfo(crtableLogoninfo)
                        Next

                        CrTables = reportDCF.Database.Tables
                        For Each CrTable In CrTables
                            crtableLogoninfo = CrTable.LogOnInfo
                            crtableLogoninfo.ConnectionInfo = crConnectionInfo
                            CrTable.ApplyLogOnInfo(crtableLogoninfo)
                        Next

                        CrTables = reportCriteresCF.Database.Tables
                        For Each CrTable In CrTables
                            crtableLogoninfo = CrTable.LogOnInfo
                            crtableLogoninfo.ConnectionInfo = crConnectionInfo
                            CrTable.ApplyLogOnInfo(crtableLogoninfo)
                        Next

                        CrTables = reportSoumisCF.Database.Tables
                        For Each CrTable In CrTables
                            crtableLogoninfo = CrTable.LogOnInfo
                            crtableLogoninfo.ConnectionInfo = crConnectionInfo
                            CrTable.ApplyLogOnInfo(crtableLogoninfo)
                        Next

                        CrTables = reportCF.Database.Tables
                        For Each CrTable In CrTables
                            crtableLogoninfo = CrTable.LogOnInfo
                            crtableLogoninfo.ConnectionInfo = crConnectionInfo
                            CrTable.ApplyLogOnInfo(crtableLogoninfo)
                        Next

                        CrTables = reportBDQCF.Database.Tables
                        For Each CrTable In CrTables
                            crtableLogoninfo = CrTable.LogOnInfo
                            crtableLogoninfo.ConnectionInfo = crConnectionInfo
                            CrTable.ApplyLogOnInfo(crtableLogoninfo)
                        Next

                        CrTables = reportFormMarcheCF.Database.Tables
                        For Each CrTable In CrTables
                            crtableLogoninfo = CrTable.LogOnInfo
                            crtableLogoninfo.ConnectionInfo = crConnectionInfo
                            CrTable.ApplyLogOnInfo(crtableLogoninfo)
                        Next

                        'reportInstCF.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\Instructions_CF.pdf")
                        'reportDCF.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\DCF.pdf")
                        'reportCriteresCF.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\Criteres_CF.pdf")
                        'reportSoumisCF.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\Soumission_CF.pdf")
                        'reportCF.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\TCC_CF.pdf")
                        'reportBDQCF.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\BDQ_CF.pdf")
                        'reportFormMarcheCF.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\FormulairesMarche_CF.pdf")
                    Else
                        'reportDocAnnexe.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\ListeAnnexes.pdf")
                        'reportFormMarche.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\FormulairesMarche.pdf")
                        'reportCCAP.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\CCAP.pdf")
                        'reportCCAG.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\CCAG.pdf")
                        'If (TypeMarche = "Travaux") Then
                        '    EditSpecTravaux.SaveDocument(NomDossier & "\Dossier\SpecificationTravaux.rtf", DevExpress.XtraRichEdit.DocumentFormat.Rtf)
                        'Else
                        '    reportSpecTech.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\SpecificationsTechniques.pdf")
                        'End If
                        'reportFraude.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\FraudeEtCorruption.pdf")
                        'reportPaysEligibles.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\PaysEligibles.pdf")
                        'reportSoumis.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\FormulairesSoumission.pdf")
                        'reportCriteres.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\CriteresEvaluation.pdf")
                        'reportDPAO.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\DonneesParticulieres.pdf")
                        'reportInst.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\InstructionsSoumission.pdf")
                        'report.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\Prelude.pdf")
                        'report2.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\Couverture.pdf")
                    End If

                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
                End Try
            End If
        End If
        FinChargement()
    End Sub
    Private Sub DaoComplet1(Optional ByVal Impression As Boolean = False)

        DebutChargement(True, "Chargement du dossier en cours...")
        Dim Chemin As String = lineEtat & "\Marches\DAO\Fournitures\"
        Dim report2 As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        'Dim CrTables As Tables
        'Dim CrTable As Table

        Dim DatSet = New DataSet

        query = "select MethodePDM from T_DAO where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim typem = ExecuteScallar(query)
        MsgBox(typem)
        If typem = "AOI" Then
            report2.Load(Chemin & "PageGardeDAO.rpt")
            If TypeMarche = "Fournitures" Then
            ElseIf TypeMarche = "Travaux" Then
            End If
        ElseIf typem = "AON" Then
            report2.Load(Chemin & "PageGardeDAO.rpt")
        ElseIf typem = "PSC" Then
            report2.Load(Chemin & "PageGardeDAO.rpt")
        ElseIf typem = "PSL" Or typem = "PSO" Then
            report2.Load(Chemin & "PageGardeDAO.rpt")
        End If

        If typem = "PSC" Then
            report2.SetParameterValue("NumDao", NumDoss)
        ElseIf typem = "PSL" Or typem = "PSO" Then
            report2.SetParameterValue("NumDao", NumDoss)
        Else

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With





            ' ****************************** Paramètres page de présentations ***********************************
            report2.SetParameterValue("NumDao", NumDoss)
            If TypeMarche = "Fournitures" Then
            ElseIf TypeMarche = "Travaux" Then

            End If
        End If
        query = "select PaysProjet,MinistereTutelle,NomProjet,LogoProjet,AdresseProjet,VilleProjet,BoitePostaleProjet,TelProjet,FaxProjet,MailProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows

            If typem = "PSC" Then
                report2.SetParameterValue("CodeProjet", ProjetEnCours)
                report2.SetParameterValue("Ministere", MettreApost(rw(1).ToString).ToString().ToUpper)
                report2.SetParameterValue("NomProjet", MettreApost(rw(2).ToString).ToString().ToUpper)
            ElseIf typem = "PSL" Or typem = "PSO" Then
                report2.SetParameterValue("CodeProjet", ProjetEnCours)
                report2.SetParameterValue("Ministere", MettreApost(rw(1).ToString().ToUpper))
                report2.SetParameterValue("NomProjet", MettreApost(rw(2).ToString().ToUpper))
            Else
                report2.SetParameterValue("CodeProjet", ProjetEnCours)
                report2.SetParameterValue("Ministere", MettreApost(rw(1).ToString).ToString().ToUpper)
                report2.SetParameterValue("NomProjet", MettreApost(rw(2).ToString).ToString().ToUpper)
            End If
        Next

        'Données du DAO ***********************
        Dim NroCompte As String = ""
        Dim PrctGarantie As String = ""
        Dim methodePDM As String = ""
        query = "select MethodePDM,TypeMarche,NbreLotDAO,DelaiExecution,PrixDAO,CompteAchat,DateLimiteRemise,PourcGarantie,DatePublication,NumPublication,JournalPublication,DateEdition,NbreMembreGroup,PreQualif,DateReunionPrepa,LangueSoumission,ValiditeOffre,ValiditeCaution,NbCopieSoumission,NomConciliateur,MontConciliateur,DateOuverture,DesignConciliateur,DesignAdresse from T_DAO where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            methodePDM = rw(0).ToString
            If (rw(0).ToString <> "") Then

                If (rw(0).ToString = "AON") Then
                    report2.SetParameterValue("LibelleMethodePdm", "APPEL D'OFFRES NATIONAL")
                ElseIf (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report2.SetParameterValue("LibelleMethodePdm", "DEMANDE DE COTATION")
                ElseIf (rw(0).ToString = "PSC") Then
                    report2.SetParameterValue("LibelleMethodePdm", "PSC")
                Else
                    report2.SetParameterValue("LibelleMethodePdm", "APPEL D'OFFRES INTERNATIONAL")
                End If
            Else

                report2.SetParameterValue("LibelleMethodePdm", "<ND>")
            End If

            If (rw(1).ToString <> "") Then
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                ElseIf (rw(0).ToString = "PSC") Then
                Else

                End If

            Else
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                ElseIf (rw(0).ToString = "PSC") Then
                Else

                End If

            End If

            If (rw(2).ToString <> "") Then
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                ElseIf (rw(0).ToString = "PSC") Then
                Else

                End If
            Else
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                ElseIf (rw(0).ToString = "PSC") Then
                Else

                End If

            End If

            If (rw(3).ToString <> "") Then
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                ElseIf (rw(0).ToString = "PSC") Then
                Else

                End If

            Else
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                End If

            End If

            If (rw(0).ToString = "PSC") Then
            Else
                If (rw(4).ToString <> "") Then
                Else
                End If

                If (rw(5).ToString <> "") Then
                    NroCompte = rw(5).ToString
                Else
                End If
            End If


            If (rw(6).ToString <> "") Then
                Dim DatCoup() As String = rw(6).ToString.Split(" "c)
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then

                ElseIf (rw(0).ToString = "PSC") Then
                Else

                End If

            Else
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then

                ElseIf (rw(0).ToString = "PSC") Then
                Else

                End If

            End If

            If (rw(21).ToString <> "") Then
                Dim DatCoup() As String = rw(21).ToString.Split(" "c)
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then

                ElseIf (rw(0).ToString = "PSC") Then
                Else

                End If

            Else
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then

                ElseIf (rw(0).ToString = "PSC") Then
                Else

                End If

            End If

            If (rw(0).ToString = "PSC") Then
            Else
                If (rw(7).ToString <> "") Then
                    PrctGarantie = rw(7).ToString.Replace("%", "")
                Else
                    PrctGarantie = "0"
                End If

                If (rw(8).ToString <> "") Then
                    'RemplacerTexte("[DatePub]", rw(8).ToString, Doc)
                End If

                If (rw(9).ToString <> "") Then
                    'RemplacerTexte("[NumPub]", rw(9).ToString, Doc)
                End If

                If (rw(10).ToString <> "") Then
                    'RemplacerTexte("[JournalPub]", rw(10).ToString, Doc)
                End If

                If (rw(11).ToString <> "") Then
                    report2.SetParameterValue("DateEdition", CDate(rw(11)).ToString("MMMM").ToUpper & " " & CDate(rw(11)).ToString("yyyy"))
                Else
                    report2.SetParameterValue("DateEdition", "<ND>")
                End If
            End If

            If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                Dim DelaiValid As String = ""
                If (rw(16).ToString <> "") Then
                    DelaiValid = rw(16).ToString
                End If

                Dim DelaiValidC As String = ""
                If (rw(17).ToString <> "") Then
                    DelaiValidC = rw(17).ToString
                End If
            ElseIf (rw(0).ToString = "PSC") Then

            Else

            End If

        Next

        'Membres du COJO **********************
        Dim NbreMem As Decimal = 0
        Dim ListMem As String = ""
        query = "select NomMem from T_Commission where NumeroDAO='" & NumDoss & "' order by NomMem"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            If (NbreMem > 0) Then
                ListMem = ListMem & vbNewLine
            End If
            ListMem = ListMem & (NbreMem + 1).ToString & "./    " & MettreApost(rw(0).ToString)
            NbreMem = NbreMem + 1
        Next

        If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
        ElseIf (methodePDM.ToString = "PSC") Then
        Else

        End If

        'Données du marché *********************
        Dim CodeMarche As Decimal = 0
        Dim LeBaill As String = ""
        Dim LibMarc As String = ""
        query = "select RefMarche,DescriptionMarche,MontantEstimatif,InitialeBailleur from T_Marche where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CodeMarche = rw(0)
            LeBaill = rw(3)
            If (LibMarc <> "") Then
                LibMarc = LibMarc & vbNewLine & " et " & vbNewLine
            End If
            LibMarc = LibMarc & rw(1).ToString
            If (PrctGarantie <> "") Then
                Dim MontGarant As Decimal = (CDec(PrctGarantie) / 100) * CDec(rw(2))
                MontGarant = Ceiling(MontGarant)


            End If
        Next

        If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
            report2.SetParameterValue("LibelleMarche", MettreApost(LibMarc).ToString().ToUpper)
        ElseIf (methodePDM.ToString = "PSC") Then
        Else
            report2.SetParameterValue("LibelleMarche", MettreApost(LibMarc).ToString().ToUpper)
        End If

        ' La convention ****************************
        query = "select C.CodeConvention,C.TypeConvention,C.MontantConvention from T_Convention as C, T_Bailleur as B where B.CodeBailleur=C.CodeBailleur and B.InitialeBailleur='" & LeBaill & "' and B.CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows

            If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
                report2.SetParameterValue("TypeConv", rw(1).ToString.ToUpper)
                report2.SetParameterValue("NumConv", rw(0).ToString)
                report2.SetParameterValue("Bailleur", LeBaill)
            ElseIf (methodePDM.ToString = "PSC") Then
                report2.SetParameterValue("TypeConv", rw(1).ToString.ToUpper)
                report2.SetParameterValue("NumConv", rw(0).ToString)
                report2.SetParameterValue("Bailleur", LeBaill)
            Else

                report2.SetParameterValue("TypeConv", rw(1).ToString.ToUpper)
                report2.SetParameterValue("NumConv", rw(0).ToString)
                report2.SetParameterValue("Bailleur", LeBaill)
            End If
        Next

        'Montant total de la convention **************
        Dim TotConv As Decimal = 0
        query = "select C.MontantConvention from T_Convention as C,T_Bailleur as B where C.CodeBailleur=B.CodeBailleur and B.CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            TotConv = TotConv + CDec(rw(0))
        Next

        If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
        ElseIf (methodePDM.ToString = "PSC") Then
        Else
        End If

        'Données de l'activité (Compo Souscompo) **************
        Dim CodActiv1 As String = ""
        query = "select P.LibelleCourt from T_BesoinPartition as B, T_BesoinMarche as BM,T_Partition as P where B.CodePartition=P.CodePartition and BM.RefBesoinPartition=B.RefBesoinPartition and B.CodeProjet='" & ProjetEnCours & "' and BM.RefMarche='" & CodeMarche & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CodActiv1 = rw(0).ToString
        Next

        '       Composante   *****
        Dim CodComp As String = Mid(CodActiv1, 1, 1)
        If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
        ElseIf (methodePDM.ToString = "PSC") Then
            report2.SetParameterValue("CodeCompo", CodComp)
        Else
            report2.SetParameterValue("CodeCompo", CodComp)
        End If
        query = "select LibellePartition from T_Partition where LibelleCourt='" & CodComp & "' and CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows

            If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
                report2.SetParameterValue("LibCompo", MettreApost(rw(0).ToString).ToString().ToUpper)
            ElseIf (methodePDM.ToString = "PSC") Then
                report2.SetParameterValue("LibCompo", MettreApost(rw(0).ToString).ToString().ToUpper)
            Else
                report2.SetParameterValue("LibCompo", MettreApost(rw(0).ToString).ToString().ToUpper)
            End If
        Next

        '       Sous Composante   *****
        Dim CodSouComp As String = IIf(Len(CodActiv1) = 6, Mid(CodActiv1, 1, 3), Mid(CodActiv1, 1, 2))

        If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
            report2.SetParameterValue("CodeSouCompo", CodSouComp)
        ElseIf (methodePDM.ToString = "PSC") Then
            report2.SetParameterValue("CodeSouCompo", CodSouComp)

        Else
            report2.SetParameterValue("CodeSouCompo", CodSouComp)
        End If
        query = "select LibellePartition from T_Partition where LibelleCourt='" & CodSouComp & "' and CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows

            If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
                report2.SetParameterValue("LibSouCompo", MettreApost(rw(0).ToString).ToString().ToUpper)
            ElseIf (methodePDM.ToString = "PSC") Then
            Else
                report2.SetParameterValue("LibSouCompo", MettreApost(rw(0).ToString).ToString().ToUpper)
            End If
        Next

        ' Avis général de passation des marchés **************
        If (methodePDM.ToString = "PSC") Then
        Else

        End If

        ' Annexes du dossier **********
        ' CV du Conciliateur **************************
        Dim NomDossier As String = FormatFileName(line & "\DAO\" & TypeMarche & "\" & MethodMarche & "\" & NumDoss, "")
        If File.Exists(NomDossier & "\CV_CONCILIATEUR.pdf") Or File.Exists(NomDossier & "\CV_CONCILIATEUR.doc") Or File.Exists(NomDossier & "\CV_CONCILIATEUR.docx") Then
            TabAnnexes.TabPages.Add("CV Conciliateur")

            If (File.Exists(NomDossier & "\CV_CONCILIATEUR.pdf") = True) Then
                Dim Web1 As New WebBrowser
                Web1.Dock = DockStyle.Fill
                Web1.Navigate(NomDossier & "\CV_CONCILIATEUR.pdf")
                TabAnnexes.TabPages.Item(1).Controls.Add(Web1)

            ElseIf (File.Exists(NomDossier & "\CV_CONCILIATEUR.doc") = True) Then
                Dim NewWord As New RichEditControl
                NewWord.LoadDocument(NomDossier & "\CV_CONCILIATEUR.doc", DevExpress.XtraRichEdit.DocumentFormat.Doc)
                NewWord.Dock = DockStyle.Fill
                TabAnnexes.TabPages.Item(1).Controls.Add(NewWord)

            ElseIf (File.Exists(NomDossier & "\CV_CONCILIATEUR.docx") = True) Then
                Dim NewWord As New RichEditControl
                NewWord.LoadDocument(NomDossier & "\CV_CONCILIATEUR.docx", DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
                NewWord.Dock = DockStyle.Fill
                TabAnnexes.TabPages.Item(1).Controls.Add(NewWord)

            Else
                Dim NewWord As New RichEditControl
                NewWord.Dock = DockStyle.Fill
                TabAnnexes.TabPages.Item(1).Controls.Add(NewWord)
            End If
        End If

        If (Impression = True) Then
            report2.PrintToPrinter(1, True, 0, 0)
        Else
            If MethodMarche = "PSC" Then
                XtraTabControl2.TabPages.Item(3).Text = "Demande de cotation"
                XtraTabControl2.TabPages.Item(0).PageVisible = False
                XtraTabControl2.TabPages.Item(1).PageVisible = False
                XtraTabControl2.TabPages.Item(2).PageVisible = False
                XtraTabControl2.TabPages.Item(4).PageVisible = False
                XtraTabControl2.TabPages.Item(5).PageVisible = False
                XtraTabControl2.TabPages.Item(6).PageVisible = False
                XtraTabControl2.TabPages.Item(7).PageVisible = False
                XtraTabControl2.TabPages.Item(8).PageVisible = False
                XtraTabControl2.TabPages.Item(9).PageVisible = False
                XtraTabControl2.TabPages.Item(10).PageVisible = False
                XtraTabControl2.TabPages.Item(11).PageVisible = False
                XtraTabControl2.TabPages.Item(12).PageVisible = False

            ElseIf MethodMarche = "PSL" Or MethodMarche = "PSO" Then

                XtraTabControl2.TabPages.Item(9).PageVisible = False
                XtraTabControl2.TabPages.Item(10).PageVisible = False
                XtraTabControl2.TabPages.Item(12).PageVisible = False
                XtraTabControl2.TabPages.Item(8).PageVisible = False
                XtraTabControl2.TabPages.Item(3).Text = "Demande cotation"
                XtraTabControl2.TabPages.Item(6).Text = "Comparatif"
                XtraTabControl2.TabPages.Item(7).Text = "Bordereau"
            Else
                ViewCouverture.ReportSource = report2

                If (TypeMarche = "Travaux") Then
                    EditSpecTravaux.Visible = True
                    ViewSpecTech.Visible = False
                    EditSpecTravaux.Dock = DockStyle.Fill
                    ChargerRtf(Chemin & "SpecTravaux.rtf", EditSpecTravaux)
                Else
                    EditSpecTravaux.Visible = False
                    ViewSpecTech.Visible = True
                    ViewSpecTech.Dock = DockStyle.Fill
                End If

                'Dim LesAnnexes As String = ""
                'PageAjout()
                'For i As Integer = 1 To TabAnnexes.TabPages.Count - 2
                '    LesAnnexes = LesAnnexes & TabAnnexes.TabPages.Item(i).Text & vbNewLine
                'Next
                'reportDocAnnexe.SetParameterValue("Annexes", "BONJOUR") 'LesAnnexes)
                'ViewDocAnnexe.ReportSource = reportDocAnnexe

                XtraTabControl2.TabPages.Item(9).PageVisible = True
                XtraTabControl2.TabPages.Item(10).PageVisible = True
                XtraTabControl2.TabPages.Item(12).PageVisible = True
                XtraTabControl2.TabPages.Item(8).PageVisible = True
                XtraTabControl2.TabPages.Item(3).Text = "DPAO"
                XtraTabControl2.TabPages.Item(6).Text = "Pays éligibles"
                XtraTabControl2.TabPages.Item(7).Text = "Fraude"
            End If

            ' Enregistrement automatique *************************
            NomDossier = FormatFileName(line & "\DAO\" & TypeMarche & "\" & MethodMarche & "\" & NumDoss, "")
            If (Directory.Exists(NomDossier) = True) Then
                Directory.CreateDirectory(NomDossier & "\Dossier")

                Try
                    If MethodMarche = "PSC" Then
                        With crConnectionInfo
                            .ServerName = ODBCNAME
                            .DatabaseName = DB
                            .UserID = USERNAME
                            .Password = PWD
                        End With


                    ElseIf MethodMarche = "PSL" Or MethodMarche = "PSO" Then
                        With crConnectionInfo
                            .ServerName = ODBCNAME
                            .DatabaseName = DB
                            .UserID = USERNAME
                            .Password = PWD
                        End With





                    Else

                        If (TypeMarche = "Travaux") Then
                            EditSpecTravaux.SaveDocument(NomDossier & "\Dossier\SpecificationTravaux.rtf", DevExpress.XtraRichEdit.DocumentFormat.Rtf)
                        Else
                        End If
                        report2.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, NomDossier & "\Dossier\Couverture.pdf")
                    End If

                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
                End Try
            End If
        End If
        FinChargement()
    End Sub

    Private Sub PageAjout()
        TabAnnexes.TabPages.Add("+")
        TabAnnexes.TabPages.Item(TabAnnexes.TabPages.Count - 1).ShowCloseButton = DevExpress.Utils.DefaultBoolean.False
    End Sub

    Private Sub TabAnnexes_SelectedPageChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabAnnexes.SelectedPageChanged
        If (TabAnnexes.Visible = True) Then
            If (TabAnnexes.SelectedTabPageIndex = TabAnnexes.TabPages.Count - 1) Then
                Try
                    Dim NouvFenetre As String = (TabAnnexes.TabPages.Count - 1).ToString

                    Dim dlg As New OpenFileDialog
                    dlg.FileName = String.Empty
                    dlg.ShowDialog()

                    If (dlg.FileName.ToString <> "") Then
                        TabAnnexes.SelectedTabPage.Text = dlg.FileName
                        Dim partDlg() As String = dlg.FileName.Split("."c)

                        If (partDlg(1) = "doc" Or partDlg(1) = "docx") Then
                            Dim NewWord As New RichEditControl
                            If (partDlg(1) = "docx") Then
                                NewWord.LoadDocument(dlg.FileName, DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
                            ElseIf (partDlg(1) = "doc") Then
                                NewWord.LoadDocument(dlg.FileName, DevExpress.XtraRichEdit.DocumentFormat.Doc)
                            End If
                            NewWord.Dock = DockStyle.Fill
                            TabAnnexes.SelectedTabPage.Controls.Add(NewWord)
                            TabAnnexes.SelectedTabPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True
                            EnregistrerPJ(dlg.FileName)
                            PageAjout()

                        ElseIf (partDlg(1) = "pdf") Then

                            Dim Web1 As New WebBrowser
                            Web1.Dock = DockStyle.Fill
                            Web1.Navigate(dlg.FileName)
                            TabAnnexes.SelectedTabPage.Controls.Add(Web1)
                            TabAnnexes.SelectedTabPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True
                            EnregistrerPJ(dlg.FileName)
                            PageAjout()

                        ElseIf (partDlg(1) = "png" Or partDlg(1) = "jpg" Or partDlg(1) = "bmp" Or partDlg(1) = "gif") Then

                            Dim NewImg As New PictureBox
                            NewImg.Load(dlg.FileName)
                            NewImg.Dock = DockStyle.Fill
                            NewImg.SizeMode = PictureBoxSizeMode.Zoom
                            TabAnnexes.SelectedTabPage.Controls.Add(NewImg)
                            TabAnnexes.SelectedTabPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True
                            EnregistrerPJ(dlg.FileName)
                            PageAjout()

                        Else
                            MsgBox("Format non pris en charge!", MsgBoxStyle.Information)
                            TabAnnexes.SelectedTabPage.Text = "+"
                        End If


                    End If
                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
                End Try

            End If
        End If

    End Sub

    Private Sub EnregistrerPJ(ByVal NomPJ As String)
        Dim NomDossier As String = FormatFileName(line & "\DAO\" & TypeMarche & "\" & MethodMarche & "\" & NumDoss, "")
        If (Directory.Exists(NomDossier) = True) Then
            Dim partNomPj() As String = NomPJ.Split("\"c)
            Dim NomCourtPJ As String = ""
            For Each part As String In partNomPj
                NomCourtPJ = part
            Next
            File.Copy(NomPJ, NomDossier & "\" & NomCourtPJ, True)
        End If
    End Sub

    Private Sub ChargerRtf(ByVal NomFichier As String, ByRef DocumtCible As RichEditControl)
        DocumtCible.Document.RtfText.Remove(0)
        DocumtCible.LoadDocument(NomFichier, DocumentFormat.Rtf)
    End Sub

    Private Sub BtPleinEcran_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EtatPleinEcran(ViewPrelude, "DAO N° " & NumDoss)
    End Sub

#End Region
    Private Sub NewDao_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EtatPleinEcran(ViewInstructions, "DAO N° " & NumDoss)
    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EtatPleinEcran(ViewDPAO, "DAO N° " & NumDoss)
    End Sub

    Private Sub SimpleButton11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EtatPleinEcran(ViewCouverture, "DAO N° " & NumDoss)
    End Sub

    Private Sub SimpleButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EtatPleinEcran(ViewCriteres, "DAO N° " & NumDoss)
    End Sub

    Private Sub SimpleButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EtatPleinEcran(ViewFormSoumis, "DAO N° " & NumDoss)
    End Sub

    Private Sub SimpleButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EtatPleinEcran(ViewPaysEligibles, "DAO N° " & NumDoss)
    End Sub

    Private Sub SimpleButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EtatPleinEcran(ViewFraude, "DAO N° " & NumDoss)
    End Sub

    Private Sub SimpleButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EtatPleinEcran(ViewSpecTech, "DAO N° " & NumDoss)
    End Sub

    Private Sub SimpleButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EtatPleinEcran(ViewCAG, "DAO N° " & NumDoss)
    End Sub

    Private Sub SimpleButton9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EtatPleinEcran(ViewCAP, "DAO N° " & NumDoss)
    End Sub

    Private Sub SimpleButton10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EtatPleinEcran(ViewFormMarche, "DAO N° " & NumDoss)
    End Sub

    Private Sub ApercuDAO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text &= " (" & NumDoss & ")"
        DaoComplet()
    End Sub

    Private Sub SimpleButton13_Click(sender As Object, e As EventArgs)
        Dim Chemin As String = lineEtat & "\Marches\DAO\Fournitures\"
        Dim reportInstCF As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table
        reportInstCF.Load(Chemin & "Instructions_CF.rpt")
        With crConnectionInfo
            .ServerName = ODBCNAME
            .DatabaseName = DB
            .UserID = USERNAME
            .Password = PWD
        End With
        CrTables = reportInstCF.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next
        ViewInstructions.ReportSource = reportInstCF

    End Sub
    Public Sub ImpressionDAO(ByVal NumDoss As String)

        DebutChargement(True, "Impression du dossier en cours...")
        Dim Chemin As String = lineEtat & "\Marches\DAO\Fournitures\"
        Dim CheminSauvGarde As String = ""
        Dim report, reportplc, reportDocAnnexe, reportFormMarche, reportSpecTech As New ReportDocument
        Dim report2, reportInst, reportDPAO, reportCriteres, reportSoumis, reportPaysEligibles, reportFraude, reportCCAG, reportCCAP, reportCF1, reportInstCF, reportBDQCF, reportDCF, reportCriteresCF, reportSoumisCF, reportCF, reportFormMarcheCF As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table

        Dim DatSet = New DataSet
        Dim typem As String = ""
        query = "select MethodePDM,TypeMarche from T_DAO where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dtDAO = ExcecuteSelectQuery(query)
        For Each rw In dtDAO.Rows
            typem = rw("MethodePDM").ToString
            TypeMarche = rw("TypeMarche").ToString
            MethodMarche = rw("MethodePDM").ToString
        Next
        If typem = "AOI" Then
            report.Load(Chemin & "Prelude.rpt")
            report2.Load(Chemin & "PageGarde.rpt")
            reportInst.Load(Chemin & "Instructions.rpt")
            reportDPAO.Load(Chemin & "DPAO.rpt")
            reportCriteres.Load(Chemin & "Criteres.rpt")
            If TypeMarche = "Fournitures" Then
                reportSoumis.Load(Chemin & "Soumission_FR.rpt")
            ElseIf TypeMarche = "Travaux" Then
                reportSoumis.Load(Chemin & "Soumission_TX.rpt")
            End If
            reportPaysEligibles.Load(Chemin & "PaysEligibles.rpt")
            reportFraude.Load(Chemin & "FraudeEtCorruption.rpt")
            reportCCAG.Load(Chemin & "CCAG.rpt")
            reportCCAP.Load(Chemin & "CCAP.rpt")
            reportDocAnnexe.Load(Chemin & "AnnexesDoc.rpt")
            reportFormMarche.Load(Chemin & "FormulairesMarche.rpt")
            reportSpecTech.Load(Chemin & "SpecTechFournit.rpt")
        ElseIf typem = "AON" Then
            report.Load(Chemin & "Prelude.rpt")
            report2.Load(Chemin & "PageGarde.rpt")
            reportInst.Load(Chemin & "Instructions.rpt")
            reportDPAO.Load(Chemin & "DPAO.rpt")
            reportCriteres.Load(Chemin & "Criteres.rpt")
            If TypeMarche = "Fournitures" Then
                reportSoumis.Load(Chemin & "Soumission_FR.rpt")
            ElseIf TypeMarche = "Travaux" Then
                reportSoumis.Load(Chemin & "Soumission_TX.rpt")
            End If
            reportPaysEligibles.Load(Chemin & "PaysEligibles.rpt")
            reportFraude.Load(Chemin & "FraudeEtCorruption.rpt")
            reportCCAG.Load(Chemin & "CCAG.rpt")
            reportCCAP.Load(Chemin & "CCAP.rpt")
            reportDocAnnexe.Load(Chemin & "AnnexesDoc.rpt")
            reportFormMarche.Load(Chemin & "FormulairesMarche.rpt")
            reportSpecTech.Load(Chemin & "SpecTechFournit.rpt")
        ElseIf typem = "PSC" Then
            reportplc.Load(Chemin & "PLC.rpt")
        ElseIf typem = "PSL" Or typem = "PSO" Then
            report.Load(Chemin & "Prelude.rpt")
            report2.Load(Chemin & "PageGarde.rpt")
            reportInstCF.Load(Chemin & "Instructions_CF.rpt")
            reportDCF.Load(Chemin & "DCF.rpt")
            reportCriteresCF.Load(Chemin & "Criteres_CF.rpt")
            reportSoumisCF.Load(Chemin & "Soumission_CF.rpt")
            reportCF.Load(Chemin & "TCC_CF.rpt")
            reportBDQCF.Load(Chemin & "BDQ_CF.rpt")
            reportFormMarcheCF.Load(Chemin & "FormulairesMarche_CF.rpt")
        End If

        If typem = "PSC" Then
            reportplc.SetParameterValue("NumDao", NumDoss)
            reportplc.SetParameterValue("NumDao", NumDoss, "T1.1.rpt")
            reportplc.SetParameterValue("NumDao", NumDoss, "T1.2.rpt")
            reportplc.SetParameterValue("NumDao", NumDoss, "T1.3.rpt")
            reportplc.SetParameterValue("NumDao", NumDoss, "T1.4.rpt")
        ElseIf typem = "PSL" Or typem = "PSO" Then
            report.SetParameterValue("NumDao", NumDoss)
            report2.SetParameterValue("NumDao", NumDoss)
            reportDCF.SetParameterValue("NumDao", NumDoss)
            reportDCF.SetParameterValue("CodeProjet", ProjetEnCours)
            reportCriteresCF.SetParameterValue("NumDao", NumDoss)
            reportSoumisCF.SetParameterValue("NumDao", NumDoss)
            reportCF.SetParameterValue("NumDao", NumDoss)
            reportBDQCF.SetParameterValue("NumDao", NumDoss)
            reportFormMarcheCF.SetParameterValue("NumDao", NumDoss)
        Else

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = reportSpecTech.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            CrTables = reportplc.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next
            reportplc.SetDataSource(DatSet)
            reportSpecTech.SetDataSource(DatSet)
            ' ****************************** Paramètres page de présentations ***********************************
            report.SetParameterValue("NumDao", NumDoss)
            report2.SetParameterValue("NumDao", NumDoss)
            reportDPAO.SetParameterValue("NumDao", NumDoss)
            reportSoumis.SetParameterValue("NumDao1", NumDoss)
            If TypeMarche = "Fournitures" Then
            ElseIf TypeMarche = "Travaux" Then
                reportSoumis.SetParameterValue("NumDao2", NumDoss)
                reportSoumis.SetParameterValue("NumDao3", NumDoss)
                reportSoumis.SetParameterValue("NumDao4", NumDoss)
                reportSoumis.SetParameterValue("NumDao5", NumDoss)
            End If
            reportSpecTech.SetParameterValue("NumDaoSpec", NumDoss)
        End If
        query = "select PaysProjet,MinistereTutelle,NomProjet,LogoProjet,AdresseProjet,VilleProjet,BoitePostaleProjet,TelProjet,FaxProjet,MailProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows

            If typem = "PSC" Then
                reportplc.SetParameterValue("CodeProjet", ProjetEnCours)
            ElseIf typem = "PSL" Or typem = "PSO" Then
                report.SetParameterValue("CodeProjet", ProjetEnCours)
                report2.SetParameterValue("CodeProjet", ProjetEnCours)
                report.SetParameterValue("PaysProjet", MettreApost(rw(0).ToString))
                report.SetParameterValue("Ministere", MettreApost(rw(1).ToString))
                report2.SetParameterValue("Ministere", MettreApost(rw(1).ToString).ToString().ToUpper)
                report.SetParameterValue("NomProjet", MettreApost(rw(2).ToString))
                report2.SetParameterValue("NomProjet", MettreApost(rw(2).ToString).ToString().ToUpper)
                report.SetParameterValue("AdresseProjet", MettreApost(rw(4).ToString))
                report.SetParameterValue("VilleProjet", MettreApost(rw(5).ToString))
                report.SetParameterValue("BpProjet", MettreApost(rw(6).ToString.ToUpper))
                report.SetParameterValue("TelProjet", MettreApost(rw(7).ToString))
                report.SetParameterValue("FaxProjet", MettreApost(rw(8).ToString))
                report.SetParameterValue("MailProjet", MettreApost(rw(9).ToString).ToString().ToLower())
            Else
                report.SetParameterValue("CodeProjet", ProjetEnCours)
                report2.SetParameterValue("CodeProjet", ProjetEnCours)
                report.SetParameterValue("PaysProjet", MettreApost(rw(0).ToString))
                report.SetParameterValue("Ministere", MettreApost(rw(1).ToString))
                report2.SetParameterValue("Ministere", MettreApost(rw(1).ToString).ToString().ToUpper)
                report.SetParameterValue("NomProjet", MettreApost(rw(2).ToString))
                report2.SetParameterValue("NomProjet", MettreApost(rw(2).ToString).ToString().ToUpper)
                report.SetParameterValue("AdresseProjet", MettreApost(rw(4).ToString))
                report.SetParameterValue("VilleProjet", MettreApost(rw(5).ToString))
                report.SetParameterValue("BpProjet", MettreApost(rw(6).ToString.ToUpper))
                report.SetParameterValue("TelProjet", MettreApost(rw(7).ToString))
                report.SetParameterValue("FaxProjet", MettreApost(rw(8).ToString))
                reportDPAO.SetParameterValue("CodeProjet", ProjetEnCours)
                reportCCAP.SetParameterValue("CodeProjet", ProjetEnCours)
                reportDPAO.SetParameterValue("PaysProjet", MettreApost(rw(0).ToString))
                reportDPAO.SetParameterValue("Ministere", MettreApost(rw(1).ToString))
                reportCCAP.SetParameterValue("Ministere", MettreApost(rw(1).ToString).ToString().ToUpper)
                reportDPAO.SetParameterValue("NomProjet", MettreApost(rw(2).ToString))
                reportDPAO.SetParameterValue("AdresseProjet", MettreApost(rw(4).ToString))
                reportDPAO.SetParameterValue("VilleProjet", MettreApost(rw(5).ToString))
                reportDPAO.SetParameterValue("BpProjet", MettreApost(rw(6).ToString.ToUpper))
                reportDPAO.SetParameterValue("TelProjet", MettreApost(rw(7).ToString))
                reportDPAO.SetParameterValue("FaxProjet", MettreApost(rw(8).ToString))
                reportDPAO.SetParameterValue("MailProjet", MettreApost(rw(9).ToString).ToString().ToLower)
                report.SetParameterValue("MailProjet", MettreApost(rw(9).ToString).ToString().ToLower)
            End If
        Next

        'Données du DAO ***********************
        Dim NroCompte As String = ""
        Dim PrctGarantie As String = ""
        Dim methodePDM As String = ""
        query = "select MethodePDM,TypeMarche,NbreLotDAO,DelaiExecution,PrixDAO,CompteAchat,DateLimiteRemise,PourcGarantie,DatePublication,NumPublication,JournalPublication,DateEdition,NbreMembreGroup,PreQualif,DateReunionPrepa,LangueSoumission,ValiditeOffre,ValiditeCaution,NbCopieSoumission,NomConciliateur,MontConciliateur,DateOuverture,DesignConciliateur,DesignAdresse from T_DAO where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            methodePDM = rw(0).ToString
            If (rw(0).ToString <> "") Then

                If (rw(0).ToString = "AON") Then
                    report.SetParameterValue("LibelleMethodePdm", "Appel d'Offres National")
                    report2.SetParameterValue("LibelleMethodePdm", "APPEL D'OFFRES NATIONAL")
                    reportInst.SetParameterValue("LibelleMethodePdm", "Appel d'Offres National")
                    reportDPAO.SetParameterValue("LibelleMethodePdm", "Appel d'Offres National")
                    report.SetParameterValue("MethodePdm", rw(0).ToString)
                    reportInst.SetParameterValue("MethodePdm", rw(0).ToString)
                    reportDPAO.SetParameterValue("MethodePdm", rw(0).ToString)
                ElseIf (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("LibelleMethodePdm", "Demande de Cotation")
                    report2.SetParameterValue("LibelleMethodePdm", "DEMANDE DE COTATION")
                    report.SetParameterValue("MethodePdm", rw(0).ToString)
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("LibelleMethodePdm", "Appel d'Offres International")
                    report2.SetParameterValue("LibelleMethodePdm", "APPEL D'OFFRES INTERNATIONAL")
                    reportInst.SetParameterValue("LibelleMethodePdm", "Appel d'Offres International")
                    reportDPAO.SetParameterValue("LibelleMethodePdm", "Appel d'Offres International")
                    report.SetParameterValue("MethodePdm", rw(0).ToString)
                    reportInst.SetParameterValue("MethodePdm", rw(0).ToString)
                    reportDPAO.SetParameterValue("MethodePdm", rw(0).ToString)
                End If
            Else
                report.SetParameterValue("MethodePdm", "<ND>")
                reportInst.SetParameterValue("MethodePdm", "<ND>")
                report.SetParameterValue("LibelleMethodePdm", "<ND>")
                report2.SetParameterValue("LibelleMethodePdm", "<ND>")
                reportInst.SetParameterValue("LibelleMethodePdm", "<ND>")
                reportDPAO.SetParameterValue("MethodePdm", "<ND>")
            End If

            If (rw(1).ToString <> "") Then
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("TypeMarche", rw(1).ToString)
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("TypeMarche", rw(1).ToString)
                    reportDPAO.SetParameterValue("TypeMarche", rw(1).ToString)
                End If

            Else
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("TypeMarche", "<ND>")
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("TypeMarche", "<ND>")
                    reportDPAO.SetParameterValue("TypeMarche", "<ND>")
                End If

            End If

            If (rw(2).ToString <> "") Then
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("NbreLot", MontantLettre(rw(2).ToString) & " (" & rw(2).ToString & ")")
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("NbreLot", MontantLettre(rw(2).ToString) & " (" & rw(2).ToString & ")")
                    reportDPAO.SetParameterValue("NbreLot", MontantLettre(rw(2).ToString) & " (" & rw(2).ToString & ")")
                End If
            Else
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("NbreLot", "<ND>")
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("NbreLot", "<ND>")
                    reportDPAO.SetParameterValue("NbreLot", "<ND>")
                End If

            End If

            If (rw(3).ToString <> "") Then
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("DelaiExecut", rw(3).ToString)
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("DelaiExecut", rw(3).ToString)
                    reportDPAO.SetParameterValue("DelaiExecut", rw(3).ToString)
                End If

            Else
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("DelaiExecut", "<ND>")
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("DelaiExecut", "<ND>")
                    reportDPAO.SetParameterValue("DelaiExecut", "<ND>")
                End If

            End If

            If (rw(0).ToString = "PSC") Then
            Else
                If (rw(4).ToString <> "") Then
                    report.SetParameterValue("PrixDAO", AfficherMonnaie(rw(4).ToString) & " Fcfa")
                Else
                    report.SetParameterValue("PrixDAO", "<ND>")
                End If

                If (rw(5).ToString <> "") Then
                    Dim nomCompte = ExecuteScallar("SELECT LibelleCompte FROM t_comptebancaire WHERE NumeroCompte='" & rw(5).ToString & "'")
                    report.SetParameterValue("NomCompte", nomCompte)
                    NroCompte = rw(5).ToString
                Else
                    report.SetParameterValue("NomCompte", "<ND>")
                End If
            End If


            If (rw(6).ToString <> "") Then
                Dim DatCoup() As String = rw(6).ToString.Split(" "c)
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("DateFinDepot", CDate(DatCoup(0)).ToLongDateString)
                    report.SetParameterValue("HeureFinDepot", CDate(DatCoup(1)).ToLongTimeString)
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("DateFinDepot", CDate(DatCoup(0)).ToLongDateString)
                    report.SetParameterValue("HeureFinDepot", CDate(DatCoup(1)).ToLongTimeString)
                    reportDPAO.SetParameterValue("DateFinDepot", CDate(DatCoup(0)).ToLongDateString)
                    reportDPAO.SetParameterValue("HeureFinDepot", CDate(DatCoup(1)).ToLongTimeString)
                End If

            Else
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("DateFinDepot", "<ND>")
                    report.SetParameterValue("HeureFinDepot", "<ND>")
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("DateFinDepot", "<ND>")
                    report.SetParameterValue("HeureFinDepot", "<ND>")
                    reportDPAO.SetParameterValue("DateFinDepot", "<ND>")
                    reportDPAO.SetParameterValue("HeureFinDepot", "<ND>")
                End If

            End If

            If (rw(21).ToString <> "") Then
                Dim DatCoup() As String = rw(21).ToString.Split(" "c)
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("DateOuverture", CDate(DatCoup(0)).ToLongDateString)
                    report.SetParameterValue("HeureOuverture", CDate(DatCoup(1)).ToLongTimeString)
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("DateOuverture", CDate(DatCoup(0)).ToLongDateString)
                    report.SetParameterValue("HeureOuverture", CDate(DatCoup(1)).ToLongTimeString)
                    reportDPAO.SetParameterValue("DateOuverture", CDate(DatCoup(0)).ToLongDateString)
                    reportDPAO.SetParameterValue("HeureOuverture", CDate(DatCoup(1)).ToLongTimeString)
                End If

            Else
                If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                    report.SetParameterValue("DateOuverture", "<ND>")
                    report.SetParameterValue("HeureOuverture", "<ND>")
                ElseIf (rw(0).ToString = "PSC") Then
                Else
                    report.SetParameterValue("DateOuverture", "<ND>")
                    report.SetParameterValue("HeureOuverture", "<ND>")
                    reportDPAO.SetParameterValue("DateOuverture", "<ND>")
                    reportDPAO.SetParameterValue("HeureOuverture", "<ND>")
                End If

            End If

            If (rw(0).ToString = "PSC") Then
            Else
                If (rw(7).ToString <> "") Then
                    PrctGarantie = rw(7).ToString.Replace("%", "")
                Else
                    PrctGarantie = "0"
                End If

                If (rw(8).ToString <> "") Then
                    'RemplacerTexte("[DatePub]", rw(8).ToString, Doc)
                End If

                If (rw(9).ToString <> "") Then
                    'RemplacerTexte("[NumPub]", rw(9).ToString, Doc)
                End If

                If (rw(10).ToString <> "") Then
                    'RemplacerTexte("[JournalPub]", rw(10).ToString, Doc)
                End If

                If (rw(11).ToString <> "") Then
                    report2.SetParameterValue("DateEdition", CDate(rw(11)).ToString("MMMM").ToUpper & " " & CDate(rw(11)).ToString("yyyy"))
                Else
                    report2.SetParameterValue("DateEdition", "<ND>")
                End If
            End If

            If (rw(0).ToString = "PSL") Or (rw(0).ToString = "PSO") Then
                Dim DelaiValid As String = ""
                If (rw(16).ToString <> "") Then
                    DelaiValid = rw(16).ToString
                End If

                Dim DelaiValidC As String = ""
                If (rw(17).ToString <> "") Then
                    DelaiValidC = rw(17).ToString
                End If
            ElseIf (rw(0).ToString = "PSC") Then

            Else
                If (rw(12).ToString <> "") Then
                    reportDPAO.SetParameterValue("NbreMembreGroupe", MontantLettre(rw(12).ToString) & " (" & rw(12).ToString & ")")
                Else
                    reportDPAO.SetParameterValue("NbreMembreGroupe", "<ND>")
                End If

                If (rw(13).ToString <> "OUI") Then
                    reportDPAO.SetParameterValue("PreQualif", "Le présent appel d'offres n'est pas précédé d'une préqualification.")
                Else
                    reportDPAO.SetParameterValue("PreQualif", "Le présent appel d'offres est précédé d'une préqualification.")
                End If

                If (rw(14).ToString <> "") Then
                    Dim PartDate() As String = rw(14).ToString.Split(" "c)
                    reportDPAO.SetParameterValue("DateReunionPrepa", CDate(PartDate(0)).ToLongDateString & " à " & PartDate(1))
                Else
                    reportDPAO.SetParameterValue("DateReunionPrepa", "<ND>")
                End If

                If (rw(15).ToString <> "") Then
                    reportDPAO.SetParameterValue("LangueOffre", rw(15).ToString)
                    reportCCAP.SetParameterValue("LangueOffre", rw(15).ToString)
                Else
                    reportDPAO.SetParameterValue("LangueOffre", "<ND>")
                    reportCCAP.SetParameterValue("LangueOffre", "<ND>")
                End If

                Dim DelaiValid As String = ""
                If (rw(16).ToString <> "") Then
                    reportDPAO.SetParameterValue("ValiditeOffre", rw(16).ToString)
                    DelaiValid = rw(16).ToString
                Else
                    reportDPAO.SetParameterValue("ValiditeOffre", "<ND>")
                End If

                Dim DelaiValidC As String = ""
                If (rw(17).ToString <> "") Then
                    reportDPAO.SetParameterValue("ValiditeCaution", rw(17).ToString)
                    DelaiValidC = rw(17).ToString
                Else
                    reportDPAO.SetParameterValue("ValiditeCaution", "<ND>")
                End If

                If (DelaiValid <> "" And DelaiValidC <> "") Then
                    Dim PartOffre() As String = DelaiValid.Split(" "c)
                    Dim NumOffre As Decimal = CInt(PartOffre(0))
                    If (PartOffre(1) = "Semaines") Then
                        NumOffre = NumOffre * 7
                    ElseIf (PartOffre(1) = "Mois") Then
                        NumOffre = NumOffre * 30
                    End If

                    Dim PartCaut() As String = DelaiValidC.Split(" "c)
                    Dim NumCaut As Decimal = CInt(PartCaut(0))
                    If (PartCaut(1) = "Semaines") Then
                        NumCaut = NumCaut * 7
                    ElseIf (PartCaut(1) = "Mois") Then
                        NumCaut = NumCaut * 30
                    End If
                    reportDPAO.SetParameterValue("ExcedentValiditeCaution", Abs(NumCaut - NumOffre).ToString & " Jours")
                Else
                    reportDPAO.SetParameterValue("ExcedentValiditeCaution", "<ND>")
                End If

                If (rw(18).ToString <> "") Then
                    reportDPAO.SetParameterValue("NbreCopies", MontantLettre(rw(18).ToString) & " (" & rw(18).ToString & ")")
                Else
                    reportDPAO.SetParameterValue("NbreCopies", "<ND>")
                End If

                If (rw(19).ToString <> "") Then
                    reportDPAO.SetParameterValue("NomConciliateur", MettreApost(rw(19).ToString))
                Else
                    reportDPAO.SetParameterValue("NomConciliateur", "<ND>")
                End If

                If (rw(20).ToString <> "") Then
                    reportDPAO.SetParameterValue("MontConciliateur", AfficherMonnaie(rw(20).ToString))
                    reportCCAP.SetParameterValue("MontConciliateur", AfficherMonnaie(rw(20).ToString))
                Else
                    reportDPAO.SetParameterValue("MontConciliateur", "<ND>")
                    reportCCAP.SetParameterValue("MontConciliateur", "<ND>")
                End If

                If (rw(22).ToString <> "") Then
                    reportDPAO.SetParameterValue("DesignConcil", MettreApost(rw(22).ToString))
                    reportCCAP.SetParameterValue("DesignConcil", MettreApost(rw(22).ToString))
                Else
                    reportDPAO.SetParameterValue("DesignConcil", "<ND>")
                    reportCCAP.SetParameterValue("DesignConcil", "<ND>")
                End If

                If (rw(23).ToString <> "") Then
                    reportDPAO.SetParameterValue("AdresseDesign", MettreApost(rw(23).ToString))
                    reportCCAP.SetParameterValue("AdresseDesign", MettreApost(rw(23).ToString))
                Else
                    reportDPAO.SetParameterValue("AdresseDesign", "<ND>")
                    reportCCAP.SetParameterValue("AdresseDesign", "<ND>")
                End If
            End If

        Next

        'Membres du COJO **********************
        Dim NbreMem As Decimal = 0
        Dim ListMem As String = ""
        query = "select NomMem from T_Commission where NumeroDAO='" & NumDoss & "' order by NomMem"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            If (NbreMem > 0) Then
                ListMem = ListMem & vbNewLine
            End If
            ListMem = ListMem & (NbreMem + 1).ToString & "./    " & MettreApost(rw(0).ToString)
            NbreMem = NbreMem + 1
        Next

        If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
        ElseIf (methodePDM.ToString = "PSC") Then
        Else
            If (NbreMem > 0) Then
                reportDPAO.SetParameterValue("MembresCojo", ListMem)
            Else
                reportDPAO.SetParameterValue("MembresCojo", "<ND>")
            End If
        End If

        'Données du marché *********************
        Dim CodeMarche As Decimal = 0
        Dim LeBaill As String = ""
        Dim LibMarc As String = ""
        query = "select RefMarche,DescriptionMarche,MontantEstimatif,InitialeBailleur from T_Marche where NumeroDAO='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CodeMarche = rw(0)
            LeBaill = rw(3)
            If (LibMarc <> "") Then
                LibMarc = LibMarc & vbNewLine & " et " & vbNewLine
            End If
            LibMarc = LibMarc & rw(1).ToString
            If (PrctGarantie <> "") Then
                Dim MontGarant As Decimal = (CDec(PrctGarantie) / 100) * CDec(rw(2))
                MontGarant = Ceiling(MontGarant)
                If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
                    report.SetParameterValue("MontantGarantie", MontantLettre(MontGarant.ToString).Replace(" zero", "") & " (" & AfficherMonnaie(MontGarant.ToString) & ")" & " de Fcfa")
                ElseIf (methodePDM.ToString = "PSC") Then
                Else
                    report.SetParameterValue("MontantGarantie", MontantLettre(MontGarant.ToString).Replace(" zero", "") & " (" & AfficherMonnaie(MontGarant.ToString) & ")" & " de Fcfa")
                    reportDPAO.SetParameterValue("MontantGarantie", MontantLettre(MontGarant.ToString).Replace(" zero", "") & " (" & AfficherMonnaie(MontGarant.ToString) & ")" & " de Fcfa")
                End If

            End If
        Next

        If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
            report.SetParameterValue("LibelleMarche", MettreApost(LibMarc))
            report2.SetParameterValue("LibelleMarche", MettreApost(LibMarc).ToString().ToUpper)
        ElseIf (methodePDM.ToString = "PSC") Then
        Else
            report.SetParameterValue("LibelleMarche", MettreApost(LibMarc))
            report2.SetParameterValue("LibelleMarche", MettreApost(LibMarc).ToString().ToUpper)
            reportDPAO.SetParameterValue("LibelleMarche", MettreApost(LibMarc))
        End If

        ' La convention ****************************
        query = "select C.CodeConvention,C.TypeConvention,C.MontantConvention from T_Convention as C, T_Bailleur as B where B.CodeBailleur=C.CodeBailleur and B.InitialeBailleur='" & LeBaill & "' and B.CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows

            If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
                report.SetParameterValue("TypeConv", rw(1).ToString)
                report2.SetParameterValue("TypeConv", rw(1).ToString.ToUpper)
                report.SetParameterValue("NumConv", rw(0).ToString)
                report2.SetParameterValue("NumConv", rw(0).ToString)
                report.SetParameterValue("Bailleur", LeBaill)
                report2.SetParameterValue("Bailleur", LeBaill)
            ElseIf (methodePDM.ToString = "PSC") Then
            Else
                report.SetParameterValue("TypeConv", rw(1).ToString)
                report2.SetParameterValue("TypeConv", rw(1).ToString.ToUpper)
                report.SetParameterValue("NumConv", rw(0).ToString)
                report2.SetParameterValue("NumConv", rw(0).ToString)
                report.SetParameterValue("Bailleur", LeBaill)
                report2.SetParameterValue("Bailleur", LeBaill)
                reportDPAO.SetParameterValue("TypeConv", rw(1).ToString)
                reportDPAO.SetParameterValue("NumConv", rw(0).ToString)
                reportDPAO.SetParameterValue("Bailleur", LeBaill)
            End If
        Next

        'Montant total de la convention **************
        Dim TotConv As Decimal = 0
        query = "select C.MontantConvention from T_Convention as C,T_Bailleur as B where C.CodeBailleur=B.CodeBailleur and B.CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            TotConv = TotConv + CDec(rw(0))
        Next

        If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
        ElseIf (methodePDM.ToString = "PSC") Then
        Else
            reportDPAO.SetParameterValue("MontTotConv", AfficherMonnaie(TotConv.ToString) & " de francs CFA")
        End If

        'Données du compte d'achat DAO **********************
        query = "select C.NumeroCompte,B.CodeBanque,B.PaysBanque,B.AdresseBanque from T_CompteBancaire as C,T_Banque as B where C.RefBanque=B.RefBanque and C.NumeroCompte='" & NroCompte & "' and C.CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
                report.SetParameterValue("NumCompte", rw(0).ToString)
                report.SetParameterValue("CodeBanque", rw(1).ToString)
                report.SetParameterValue("PaysBanque", MettreApost(rw(2).ToString).ToString().ToUpper)
                report.SetParameterValue("AdresseBanque", MettreApost(rw(3).ToString))
            ElseIf (methodePDM.ToString = "PSC") Then
            Else
                report.SetParameterValue("NumCompte", rw(0).ToString)
                report.SetParameterValue("CodeBanque", rw(1).ToString)
                report.SetParameterValue("PaysBanque", MettreApost(rw(2).ToString).ToString().ToUpper)
                report.SetParameterValue("AdresseBanque", MettreApost(rw(3).ToString))
            End If
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
        If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
            report2.SetParameterValue("CodeCompo", CodComp)
        ElseIf (methodePDM.ToString = "PSC") Then
        Else
            report2.SetParameterValue("CodeCompo", CodComp)
            reportDPAO.SetParameterValue("CodeCompo", CodComp)
        End If
        query = "select LibellePartition from T_Partition where LibelleCourt='" & CodComp & "' and CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows

            If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
                report2.SetParameterValue("LibCompo", MettreApost(rw(0).ToString).ToString().ToUpper)
            ElseIf (methodePDM.ToString = "PSC") Then
            Else
                report2.SetParameterValue("LibCompo", MettreApost(rw(0).ToString).ToString().ToUpper)
                reportDPAO.SetParameterValue("LibCompo", MettreApost(rw(0).ToString))
            End If
        Next

        '       Sous Composante   *****
        Dim CodSouComp As String = IIf(Len(CodActiv1) = 6, Mid(CodActiv1, 1, 3), Mid(CodActiv1, 1, 2))

        If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
            report2.SetParameterValue("CodeSouCompo", CodSouComp)
        ElseIf (methodePDM.ToString = "PSC") Then
        Else
            report2.SetParameterValue("CodeSouCompo", CodSouComp)
            reportDPAO.SetParameterValue("CodeSouCompo", CodSouComp)
        End If
        query = "select LibellePartition from T_Partition where LibelleCourt='" & CodSouComp & "' and CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows

            If (methodePDM.ToString = "PSL") Or (methodePDM.ToString = "PSO") Then
                report2.SetParameterValue("LibSouCompo", MettreApost(rw(0).ToString).ToString().ToUpper)
            ElseIf (methodePDM.ToString = "PSC") Then
            Else
                report2.SetParameterValue("LibSouCompo", MettreApost(rw(0).ToString).ToString().ToUpper)
                reportDPAO.SetParameterValue("LibSouCompo", MettreApost(rw(0).ToString))
            End If
        Next

        ' Avis général de passation des marchés **************
        If (methodePDM.ToString = "PSC") Then
        Else
            report.SetParameterValue("NumPubAG", "<ND>")
            report.SetParameterValue("DatePubAG", "<ND>")
            report.SetParameterValue("MediaPubAG", "<ND>")
        End If

        ' Annexes du dossier **********
        ' CV du Conciliateur **************************
        Dim NomDossier As String = FormatFileName(line & "\DAO\" & TypeMarche & "\" & MethodMarche & "\" & NumDoss, "")
        If File.Exists(NomDossier & "\CV_CONCILIATEUR.pdf") Or File.Exists(NomDossier & "\CV_CONCILIATEUR.doc") Or File.Exists(NomDossier & "\CV_CONCILIATEUR.docx") Then
            TabAnnexes.TabPages.Add("CV Conciliateur")

            If (File.Exists(NomDossier & "\CV_CONCILIATEUR.pdf") = True) Then
                Dim Web1 As New WebBrowser
                Web1.Dock = DockStyle.Fill
                Web1.Navigate(NomDossier & "\CV_CONCILIATEUR.pdf")
                TabAnnexes.TabPages.Item(1).Controls.Add(Web1)

            ElseIf (File.Exists(NomDossier & "\CV_CONCILIATEUR.doc") = True) Then
                Dim NewWord As New RichEditControl
                NewWord.LoadDocument(NomDossier & "\CV_CONCILIATEUR.doc", DevExpress.XtraRichEdit.DocumentFormat.Doc)
                NewWord.Dock = DockStyle.Fill
                TabAnnexes.TabPages.Item(1).Controls.Add(NewWord)

            ElseIf (File.Exists(NomDossier & "\CV_CONCILIATEUR.docx") = True) Then
                Dim NewWord As New RichEditControl
                NewWord.LoadDocument(NomDossier & "\CV_CONCILIATEUR.docx", DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
                NewWord.Dock = DockStyle.Fill
                TabAnnexes.TabPages.Item(1).Controls.Add(NewWord)

            Else
                Dim NewWord As New RichEditControl
                NewWord.Dock = DockStyle.Fill
                TabAnnexes.TabPages.Item(1).Controls.Add(NewWord)
            End If
        End If
        ' Enregistrement automatique *************************
        NomDossier = Environ$("TEMP") & "\DAO\" & TypeMarche & "\" & MethodMarche & "\" & NumDoss.Replace("/", "_")
        CheminSauvGarde = line & "\DAO\" & TypeMarche & "\" & MethodMarche
        Dim NomFichier As String = "DAO N°_" & NumDoss.Replace("/", "_") & ".pdf"
        If (Directory.Exists(NomDossier) = False) Then
            Directory.CreateDirectory(NomDossier)
        End If
        If (Directory.Exists(CheminSauvGarde) = False) Then
            Directory.CreateDirectory(CheminSauvGarde)
        End If
        Try
            If MethodMarche = "PSC" Then
                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = reportplc.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next
                reportplc.ExportToDisk([Shared].ExportFormatType.PortableDocFormat, CheminSauvGarde & "\" & NomFichier)
            ElseIf MethodMarche = "PSL" Or MethodMarche = "PSO" Then
                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = reportInstCF.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                CrTables = reportDCF.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                CrTables = reportCriteresCF.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                CrTables = reportSoumisCF.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                CrTables = reportCF.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                CrTables = reportBDQCF.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                CrTables = reportFormMarcheCF.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                reportInstCF.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\Instructions_CF.doc")
                reportDCF.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\DCF.doc")
                reportCriteresCF.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\Criteres_CF.doc")
                'reportSoumisCF.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\Soumission_CF.doc")
                reportCF.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\TCC_CF.doc")
                reportBDQCF.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\BDQ_CF.doc")
                reportFormMarcheCF.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\FormulairesMarche_CF.doc")
            Else
                reportDocAnnexe.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\ListeAnnexes.doc")
                reportFormMarche.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\FormulairesMarche.doc")
                reportCCAP.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\CCAP.doc")
                reportCCAG.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\CCAG.doc")
                If (TypeMarche = "Travaux") Then
                    EditSpecTravaux.SaveDocument(NomDossier & "\SpecificationTravaux.rtf", DevExpress.XtraRichEdit.DocumentFormat.Rtf)
                Else
                    reportSpecTech.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\SpecificationsTechniques.doc")
                End If
                reportFraude.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\FraudeEtCorruption.doc")
                reportPaysEligibles.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\PaysEligibles.doc")
                reportSoumis.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\FormulairesSoumission.doc")
                reportCriteres.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\CriteresEvaluation.doc")
                reportDPAO.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\DonneesParticulieres.doc")
                reportInst.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\InstructionsSoumission.doc")
                report.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\Prelude.doc")
                report2.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\Couverture.doc")
            End If
            If methodePDM = "PSC" Then
                Try
                    Dim printer As New Process
                    printer.StartInfo.Verb = "Print"
                    printer.StartInfo.FileName = CheminSauvGarde & "\" & NomFichier
                    printer.StartInfo.CreateNoWindow = True
                    printer.Start()
                Catch ex As Exception
                    FailMsg(ex.ToString)
                End Try
            ElseIf MethodMarche = "PSL" Or MethodMarche = "PSO" Then
                Dim oWord As New Word.Application
                Dim nomRapport As String = "DAO N°_" & NumDoss.Replace("/", "_") & ".pdf"
                CheminSauvGarde = CheminSauvGarde & "\" & nomRapport
                Try
                    Dim currentDoc As Word.Document
                    Dim Instructions_CF As String = NomDossier & "\Instructions_CF.doc"
                    Dim DCF As String = NomDossier & "\DCF.doc"
                    Dim Criteres_CF As String = NomDossier & "\Criteres_CF.doc"
                    Dim Soumission_CF As String = NomDossier & "\Soumission_CF.doc"
                    Dim TCC_CF As String = NomDossier & "\TCC_CF.doc"
                    Dim BDQ_CF As String = NomDossier & "\BDQ_CF.doc"
                    Dim FormulairesMarche_CF As String = NomDossier & "\FormulairesMarche_CF.doc"

                    'Ajout de la page Instructions_CF
                    currentDoc = oWord.Documents.Add(Instructions_CF)
                    Dim myRange As Word.Range = currentDoc.Bookmarks.Item("\endofdoc").Range
                    Dim mySection1 As Word.Section = AjouterNouvelleSectionDocument(currentDoc, myRange)
                    'Ajout de la page DCF
                    myRange.InsertFile(DCF)
                    'Ajout de la page Criteres_CF
                    mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                    'mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                    myRange.InsertFile(Criteres_CF)
                    'Ajout de la page Soumission_CF
                    mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                    myRange.InsertFile(Soumission_CF)
                    'Ajout de la page TCC_CF
                    mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                    myRange.InsertFile(TCC_CF)
                    'Ajout de page BDQ_CF
                    mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                    mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                    myRange.InsertFile(BDQ_CF)
                    'Ajout de la page FormulairesMarche_CF
                    mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                    mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                    myRange.InsertFile(FormulairesMarche_CF)
                    Try
                        currentDoc.SaveAs2(FileName:=CheminSauvGarde, FileFormat:=Word.WdSaveFormat.wdFormatPDF)
                    Catch ex As Exception
                        FailMsg("le document est ouvert par un utlisateur")
                    End Try
                    oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                Catch ex As Exception
                    FailMsg("erreur de traitement" & ex.ToString)
                    oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                End Try
                Try
                    Dim printer As New Process
                    printer.StartInfo.Verb = "Print"
                    printer.StartInfo.FileName = CheminSauvGarde & "\" & NomFichier
                    printer.StartInfo.CreateNoWindow = True
                    printer.Start()
                Catch ex As Exception
                    FailMsg(ex.ToString)
                End Try
            Else

            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        FinChargement()
    End Sub
End Class
