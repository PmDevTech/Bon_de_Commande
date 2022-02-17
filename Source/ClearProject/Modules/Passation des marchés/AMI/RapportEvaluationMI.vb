Imports MySql.Data.MySqlClient
Imports System.IO
Imports Microsoft.Office.Interop
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions
Imports System.Math
Imports DevExpress.XtraRichEdit
Imports CrystalDecisions.Shared
Imports ClearProject.PassationMarche
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports System.Security.Cryptography
Imports System.Text
Imports Microsoft.Reporting

Public Class RapportEvaluationMI

    Dim CheminRapportEvaluationDOC As String = String.Empty
    Dim CheminRapportEvaluationPDF As String = String.Empty
    Dim ValidationsRapports As String = String.Empty
    Dim RapportModif As Boolean = False

    Private Sub RapportEvaluationMI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ChargerDossierAMI()
    End Sub

    Private Sub ChargerDossierAMI()
        CmbDossier.Text = ""
        CmbDossier.Properties.Items.Clear()
        Try
            query = "select NumeroDAMI from t_ami where EvalTechnique IS NOT NULL and CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                CmbDossier.Properties.Items.Add(MettreApost(rw("NumeroDAMI").ToString))
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub InitialiserBouton(ByVal value As Boolean)
        BtGenerers.Enabled = value
        BtModifiers.Enabled = value
        BtActualisers.Enabled = value
        Btpdf.Enabled = value
        BtWord.Enabled = value
    End Sub

    Private Sub ValRejetEnvoi(value As Boolean)
        BtEnvoieBailleurs.Enabled = value
        BtValiderRaports.Enabled = value
        BtRejetterRapports.Enabled = value
        BtGenerers.Enabled = value
        BtModifiers.Enabled = value
        BtActualisers.Enabled = value
    End Sub

    Private Sub InitialiserBoutons1(ByVal value As Boolean)
        BtGenerers.Enabled = value
        BtModifiers.Enabled = value
        BtActualisers.Enabled = value
        BtEnvoieBailleurs.Enabled = value
        BtValiderRaports.Enabled = value
        BtRejetterRapports.Enabled = value
        Btpdf.Enabled = value
        BtWord.Enabled = value
    End Sub


    Private Sub CmbDossier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbDossier.SelectedIndexChanged
        'initialiser WebBrowser1
        WebBrowser1.Navigate("")
        If CmbDossier.SelectedIndex <> -1 Then
            RapportModif = False

            Try
                query = "select ValidationsRapports, CheminRapportEvaluation from t_ami where NumeroDAMI='" & EnleverApost(CmbDossier.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)

                For Each rw In dt1.Rows
                    If rw("CheminRapportEvaluation").ToString <> "" Then
                        CheminRapportEvaluationPDF = line & "\AMI\" & FormatFileName(CmbDossier.Text, "_") & "\Rapport_Evaluation\" & rw("CheminRapportEvaluation").ToString
                        CheminRapportEvaluationDOC = line & "\AMI\" & FormatFileName(CmbDossier.Text, "_") & "\Rapport_Evaluation\Rapport_Evaluation_Technique.doc"
                    Else
                        CheminRapportEvaluationPDF = ""
                        CheminRapportEvaluationDOC = ""
                    End If

                    ValidationsRapports = rw("ValidationsRapports").ToString
                Next

                'activations des boutons
                InitialiserBoutons1(True)

                If CheminRapportEvaluationPDF.ToString = "" Then 'Aucun rapport generer
                    InitialiserBoutons1(False)
                    BtGenerers.Enabled = True 'Seul le bouton generer est activer
                Else
                    If File.Exists(CheminRapportEvaluationPDF) = True Then
                        DebutChargement(True, "Chargement du rapport d'évaluation technique en cours...")
                        WebBrowser1.Navigate(CheminRapportEvaluationPDF.ToString)
                        Threading.Thread.Sleep(5000)
                    Else
                        SuccesMsg("Le rapport en cours de chargements n'existe pas")
                        InitialiserBoutons1(False)
                        BtGenerers.Enabled = True
                        FinChargement()
                        Exit Sub
                    End If

                    If ValidationsRapports.ToString = "Valider" Then
                        ValRejetEnvoi(False)
                    End If

                    FinChargement()
                End If
            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        Else
            InitialiserBouton(False)
            ValRejetEnvoi(False)
        End If
    End Sub

    Private Sub BtGenerers_Click(sender As Object, e As EventArgs) Handles BtGenerers.Click
        If CmbDossier.SelectedIndex <> -1 Then
            Try
                Dim NumDoss = EnleverApost(CmbDossier.Text)

                DebutChargement(True, "Génération du rapport d'évaluation en cours...")

                Dim Chemin As String = lineEtat & "\Marches\AMI\Rapport_Evaluation\"

                Dim RapportAMI As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table

                Dim DatSet = New DataSet
                Try
                    RapportAMI.Load(Chemin & "AMI_Rapport.rpt")
                    With crConnectionInfo
                        .ServerName = ODBCNAME
                        .DatabaseName = DB
                        .UserID = USERNAME
                        .Password = PWD
                    End With

                    CrTables = RapportAMI.Database.Tables
                    For Each CrTable In CrTables
                        crtableLogoninfo = CrTable.LogOnInfo
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo
                        CrTable.ApplyLogOnInfo(crtableLogoninfo)
                    Next

                    RapportAMI.SetDataSource(DatSet)
                    RapportAMI.SetParameterValue("CodeProjet", ProjetEnCours)
                    RapportAMI.SetParameterValue("NumDAO", NumDoss)

                    'recherche nombre de consultant
                    query = "select count(RefConsult) from t_soumissionconsultant  where NumeroDp='" & NumDoss & "'"
                    Dim NbreConsultant As Decimal = Val(ExecuteScallar(query))

                    RapportAMI.SetParameterValue("NbOffresDeposesLettre", MontantBrut(NbreConsultant.ToString))
                    RapportAMI.SetParameterValue("NbOffresDeposes", NbreConsultant.ToString)

                    'date de soumission
                    Dim dt As DataTable = ExcecuteSelectQuery("select DateOuverture from t_ami where NumeroDAMI='" & NumDoss & "'")
                    For Each rw In dt.Rows
                        RapportAMI.SetParameterValue("DateFormatLong", CDate(rw("DateOuverture").ToString).ToLongDateString)
                    Next

                    'Paramettre experience general a determiner
                    RapportAMI.SetParameterValue("Param_CritereExpGenerale", "[Vide]")
                Catch ex As Exception
                    FailMsg(ex.ToString)
                End Try

                CheminRapportEvaluationDOC = line & "\AMI\" & FormatFileName(CmbDossier.Text, "_") & "\Rapport_Evaluation"
                If (Directory.Exists(CheminRapportEvaluationDOC) = False) Then
                    Directory.CreateDirectory(CheminRapportEvaluationDOC)
                End If

                '  Dim TmpFileRapportAMI = Path.GetTempFileName & ".doc"

                Dim NewCheminpdf As String = "Rapport_" & FormatFileName(Now.ToString.Replace(" ", "_"), "_") & ".pdf"
                CheminRapportEvaluationPDF = CheminRapportEvaluationDOC & "\" & NewCheminpdf
                RapportAMI.ExportToDisk(ExportFormatType.WordForWindows, CheminRapportEvaluationDOC & "\Rapport_Evaluation_Technique.doc")
                RapportAMI.ExportToDisk(ExportFormatType.PortableDocFormat, CheminRapportEvaluationPDF)

                'Dim WdApp As New Word.Application
                'Try
                '    Dim WdDoc As Word.Document = WdApp.Documents.Add(TmpFileRapportAMI)
                '    Dim CurrentRange As Word.Range = WdDoc.Bookmarks.Item("\endofdoc").Range
                '    Dim CurrentSection As Word.Section '= AjouterNouvelleSectionDocument(WdDoc, CurrentRange)
                '    ' CurrentRange.InsertFile(TmpFileRapportAMI)

                '    'Insertion des annexes
                '    Dim rWInfoAMI As DataRow = ExcecuteSelectQuery("Select CodeConvention, MethodeSelection from t_ami where NumeroDAMI='" & EnleverApost(CmbDossier.Text) & "'").Rows(0)
                '    Dim InitialBailleurs As String = GetInitialbailleur(rWInfoAMI("CodeConvention").ToString)

                '    Dim pathFile As String = line & "\PochettesPM\BAIL_" & InitialBailleurs & "\" & rWInfoAMI("MethodeSelection").ToString & "\Consultants\" & CmbDossier.Text
                '    Dim CheminDoc As String = ""

                '    'Ajout pub
                '    Dim dt1 As DataTable = ExcecuteSelectQuery("select f.FileName from t_pm_pochette_bailleur f, t_pm_pochette_document d where f.POCHDOC_ID=d.POCHDOC_ID and d.TypePochette='AMI' and d.POCHDOC_LIB='Publications' and f.CodeBailleur='" & InitialBailleurs & "' and f.AbregeAO='" & rWInfoAMI("MethodeSelection").ToString & "'")
                '    For Each rw As DataRow In dt1.Rows
                '        CheminDoc = pathFile & "\Publications\" & MettreApost(rw("FileName").ToString)
                '        If File.Exists(CheminDoc) Then
                '            CurrentSection = AjouterNouvelleSectionDocument(WdDoc, CurrentRange)
                '            CurrentRange.InsertFile(CheminDoc)
                '        End If
                '    Next

                ''Ajout PV ouverture
                'dt1 = ExcecuteSelectQuery("select f.FileName from t_pm_pochette_bailleur f, t_pm_pochette_document d where f.POCHDOC_ID=d.POCHDOC_ID and d.TypePochette='AMI' and d.POCHDOC_LIB='Procès Verbal d&apost;ouverture des offres' and f.CodeBailleur='" & InitialBailleurs & "' and f.AbregeAO='" & rWInfoAMI("MethodeSelection").ToString & "'")
                'For Each rw2 As DataRow In dt1.Rows
                '    CheminDoc = pathFile & "\Procès Verbal d'ouverture des offres\" & MettreApost(rw2("FileName").ToString)
                '    If File.Exists(CheminDoc) Then
                '        CurrentSection = AjouterNouvelleSectionDocument(WdDoc, CurrentRange)
                '        CurrentRange.InsertFile(CheminDoc)
                '    End If
                'Next

                ''Liste signataires
                'dt1 = ExcecuteSelectQuery("select f.FileName from t_pm_pochette_bailleur f, t_pm_pochette_document d where f.POCHDOC_ID=d.POCHDOC_ID and d.TypePochette='AMI' and d.POCHDOC_LIB='Liste signataires' and f.CodeBailleur='" & InitialBailleurs & "' and f.AbregeAO='" & rWInfoAMI("MethodeSelection").ToString & "'")
                'For Each rw3 As DataRow In dt1.Rows
                '    CheminDoc = pathFile & "\Liste signataires\" & MettreApost(rw3("FileName").ToString)
                '    If File.Exists(CheminDoc) Then
                '        CurrentSection = AjouterNouvelleSectionDocument(WdDoc, CurrentRange)
                '        CurrentRange.InsertFile(CheminDoc)
                '    End If
                'Next

                'Dim NewCheminpdf As String = "Rapport_" & FormatFileName(Now.ToString.Replace(" ", "_"), "_") & ".pdf"
                'CheminRapportEvaluationPDF = CheminRapportEvaluationDOC & "\" & NewCheminpdf
                'WdDoc.SaveAs2(FileName:=CheminRapportEvaluationDOC & "\Rapport_Evaluation_Technique.doc", FileFormat:=Word.WdSaveFormat.wdFormatDocument)
                'WdDoc.SaveAs2(FileName:=CheminRapportEvaluationPDF, FileFormat:=Word.WdSaveFormat.wdFormatPDF)
                'WdDoc.Close(True)
                'WdApp.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)

                ExecuteNonQuery("Update t_ami Set CheminRapportEvaluation= '" & NewCheminpdf & "' where NumeroDAMI='" & NumDoss & "'")
                WebBrowser1.Navigate(CheminRapportEvaluationPDF)
                Threading.Thread.Sleep(5000)
                'Catch ex As Exception
                'FinChargement()
                'FailMsg(ex.ToString)
                '   WdApp.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                'End Try
                InitialiserBoutons1(True) 'Tous les boutons sont activés
                FinChargement()
            Catch ep As IO.IOException
                FinChargement()
                SuccesMsg("Un exemplaire du rapport d'évaluation technique est ouvert dans une autre application" & vbNewLine & "Veuillez le fermer svp.")
            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub BtModifiers_Click(sender As Object, e As EventArgs) Handles BtModifiers.Click
        Try
            If CmbDossier.SelectedIndex <> -1 Then
                CheminRapportEvaluationDOC = line & "\AMI\" & FormatFileName(CmbDossier.Text, "_") & "\Rapport_Evaluation\Rapport_Evaluation_Technique.doc"
                If File.Exists(CheminRapportEvaluationDOC.ToString) = True Then
                    DebutChargement(True, "Chargement du rapport d'évaluation en cours...")
                    Process.Start(CheminRapportEvaluationDOC)
                    FinChargement()
                    RapportModif = True
                Else
                    SuccesMsg("Le chemin d'accès spécifier n'existe pas")
                End If
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub BtActualisers_Click(sender As Object, e As EventArgs) Handles BtActualisers.Click
        Try
            If CmbDossier.SelectedIndex <> -1 And RapportModif = True Then
                CheminRapportEvaluationDOC = line & "\AMI\" & FormatFileName(CmbDossier.Text, "_") & "\Rapport_Evaluation\Rapport_Evaluation_Technique.doc"
                If File.Exists(CheminRapportEvaluationDOC) = True Then

                    DebutChargement(True, "Actualisation du rapport d'évaluation en cours...")
                    Dim NewCheminpdf As String = "Rapport_" & FormatFileName(Now.ToString.Replace(" ", "_"), "_") & ".pdf"
                    CheminRapportEvaluationPDF = line & "\AMI\" & FormatFileName(CmbDossier.Text, "_") & "\Rapport_Evaluation\" & NewCheminpdf

                    Dim WdApp As New Word.Application
                    Dim WdDoc As Word.Document = WdApp.Documents.Add(CheminRapportEvaluationDOC)
                    Try
                        WdDoc.SaveAs2(FileName:=CheminRapportEvaluationPDF, FileFormat:=Word.WdSaveFormat.wdFormatPDF)
                        WdDoc.Close(True)
                        WdApp.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                    Catch ep As IO.IOException
                        FinChargement()
                        SuccesMsg("Un exemplaire du rapport d'évaluation technique est ouvert dans une autre application" & vbNewLine & "Veuillez le fermer svp.")
                        WdDoc.Close(True)
                        WdApp.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                        Exit Sub
                    Catch ex As Exception
                        FailMsg(ex.ToString)
                        WdDoc.Close(True)
                        WdApp.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                        Exit Sub
                    End Try

                    ExecuteNonQuery("Update t_ami Set CheminRapportEvaluation= '" & NewCheminpdf & "' where NumeroDAMI='" & EnleverApost(CmbDossier.Text) & "'")
                    WebBrowser1.Navigate(CheminRapportEvaluationPDF.ToString)
                    Threading.Thread.Sleep(5000)
                    FinChargement()
                    RapportModif = False
                Else
                    SuccesMsg("Le chemin spécifier n'existe pas")
                End If
            ElseIf RapportModif = False Then
                SuccesMsg("Veuillez modifier le rapport avant d'actualiser")
            End If
        Catch ep As IO.IOException
            FinChargement()
            SuccesMsg("Un exemplaire du rapport d'évaluation technique est ouvert dans une autre application" & vbNewLine & "Veuillez le fermer svp.")
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub BtEnvoieBailleurs_Click(sender As Object, e As EventArgs) Handles BtEnvoieBailleurs.Click
        If CmbDossier.SelectedIndex > -1 Then
            Try

                'Info de l'envoi de l'email
                If ChargerLesDonneEmail_AMI_DP_SERVICEAUTRES(CmbDossier.Text, "AMI") = False Then
                    Exit Sub
                End If

                If ConfirmMsg("Confirmez-vous l'envoi du rapport d'évaluation" & vbNewLine & "technique au bailleur [ " & MettreApost(rwDossDPAMISA.Rows(0)("InitialeBailleur").ToString) & " ]") = DialogResult.Yes Then
                    Try
                        Dim CheminDoc As String = line & "\AMI\" & FormatFileName(CmbDossier.Text, "_") & "\Rapport_Evaluation\Rapport_Evaluation_Technique.doc"
                        If File.Exists(CheminDoc) = True Then
                            DebutChargement(True, "Envoi du rapport d'évaluation technique au bailleur...")

                            'Envoi du rapport au bailleur
                            EnvoiMailRapport(NomBailleurRetenu, CmbDossier.Text, EmailDestinatauer, CheminDoc, EmailCoordinateurProjet, EmailResponsablePM, "RapportEvalTechAMI")

                            SuccesMsg("Le rapport d'évaluation a été avec succès")
                            FinChargement()
                        Else
                            SuccesMsg("Le rapport à envoyer n'existe pas ou a été supprimer")
                        End If
                    Catch ep As IO.IOException
                        SuccesMsg("Le fichier est utilisé par une autre application" & vbNewLine & "Veuillez le fermer svp.")
                        FinChargement()
                    Catch ex As Exception
                        FailMsg(ex.ToString())
                        FinChargement()
                    End Try
                End If
            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        End If

    End Sub

    Private Sub BtValiderRaports_Click(sender As Object, e As EventArgs) Handles BtValiderRaports.Click
        If CmbDossier.SelectedIndex <> -1 Then
            Try
                If ConfirmMsg("Confirmez-vous la validation du rapport ?") = DialogResult.Yes Then
                    ExecuteNonQuery("Update t_ami set ValidationsRapports= 'Valider' where NumeroDAMI='" & EnleverApost(CmbDossier.Text) & "'")
                    SuccesMsg("Rapport d'évaluation valider avec succès")
                    ValidationsRapports = "Valider"
                    ValRejetEnvoi(False)
                End If
            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub BtImprimer_Click(sender As Object, e As EventArgs)

        WebBrowser1.Print()
        WebBrowser1.ShowPrintDialog()
        WebBrowser1.ShowPrintPreviewDialog()
    End Sub

    Private Sub BtRejetterRapports_Click(sender As Object, e As EventArgs) Handles BtRejetterRapports.Click
        If CmbDossier.SelectedIndex <> -1 Then
            Try
                If ConfirmMsg("Voulez-vous vraiment rejetter ce rapport ?") = DialogResult.Yes Then
                    ExecuteNonQuery("Update t_ami set EvalTechnique=NULL, ValidationsRapports='Rejeter' where NumeroDAMI='" & EnleverApost(CmbDossier.Text) & "'")
                    ExecuteNonQuery("Update t_noteconsultantparcriteres set ValidationNote='' where NumeroDp='" & EnleverApost(CmbDossier.Text) & "'")
                    ExecuteNonQuery("Update t_soumissionconsultant set NoteConsult=NULL, ReferenceNote=NULL, RangConsult=NULL, EvalTechOk=NULL where NumeroDp='" & EnleverApost(CmbDossier.Text) & "'")
                    SuccesMsg("Rapport d'évaluation rejeté")
                    InitialiserBoutons1(False)
                    ChargerDossierAMI()
                End If
            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub Btpdf_Click(sender As Object, e As EventArgs) Handles Btpdf.Click

        Try
            If CmbDossier.SelectedIndex <> -1 Then
                Dim Cheminpdf As String = ExecuteScallar(" select CheminRapportEvaluation from t_ami where NumeroDAMI='" & EnleverApost(CmbDossier.Text) & "'")
                CheminRapportEvaluationPDF = line & "\AMI\" & FormatFileName(CmbDossier.Text, "_") & "\Rapport_Evaluation\" & Cheminpdf
                If File.Exists(CheminRapportEvaluationPDF.ToString) = True Then
                    DebutChargement(True, "Exportation du rapport d'évaluation en cours...")
                    Process.Start(CheminRapportEvaluationPDF)
                    FinChargement()
                Else
                    SuccesMsg("Le chemin d'accès spécifier n'existe pas")
                End If
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub BtWord_Click(sender As Object, e As EventArgs) Handles BtWord.Click
        Try
            If CmbDossier.SelectedIndex <> -1 Then
                CheminRapportEvaluationDOC = line & "\AMI\" & FormatFileName(CmbDossier.Text, "_") & "\Rapport_Evaluation\Rapport_Evaluation_Technique.doc"
                If File.Exists(CheminRapportEvaluationDOC.ToString) = True Then
                    DebutChargement(True, "Exportation du rapport d'évaluation en cours...")
                    Process.Start(CheminRapportEvaluationDOC)
                    FinChargement()
                Else
                    SuccesMsg("Le chemin d'accès spécifier n'existe pas")
                End If
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub
End Class