Imports System.Globalization
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Layout
Imports DevExpress.XtraGrid.Views.Card
Imports MySql.Data.MySqlClient
Imports System.IO
Imports Microsoft.Office.Interop
Imports DevExpress.XtraSplashScreen
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions
Imports System.Math
Imports DevExpress.XtraRichEdit
Imports ClearProject.PassationMarche
Imports CrystalDecisions.Shared
Imports System.Text.RegularExpressions

Public Class NewDp

    Dim dr As DataRow
    Dim dt = New DataTable()

    Dim PourAjout As Boolean = False
    Dim PourModif As Boolean = False
    Dim DejaDansLaBD As Boolean = False
    Dim AffichDossDp As Boolean = False

    Dim ModifTous As Boolean = False
    Dim TypeModif As String = ""
    Dim CheminDocTDR As String = ""
    Dim CodeConvention As String = ""

    'Variable verifiant s'il s'agit d'une modifiaction ou un afficharge de dossier
    'Dim BoutonCliker As Boolean = False

    Dim montMarc As Decimal = 0
    Dim TraitementEnCours As Char
    Dim nomEtAdrConsImpression As String = ""
    Dim nomConsImprim As String = ""
    Dim NamePageSelectionner As String = ""
    Dim NumDoss As String = ""
    Dim typeMarc As String = ""
    Dim methodMarc As String = ""
    Dim RefMarche As String()
    Dim CodeMarcheEnCours As String = ""
    Dim TabAMettreAJour As Boolean() = {False, False, False, False, False}
    Dim LstTabName As New List(Of String) From {"PageDonneesBase", "PageDonneesPartic", "PageEvaluation", "PageTDR", "PageAppercuDp"}

    Private Sub NewDp_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub NewDp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RibbonDP.Minimized = True

        ArchivesDP()
        ItemsPays()
        ItemDevise()
        ChargerDossierAMI()
        ChargerMarcher()
        VisibleOtherTabs(False)
        CmbDossAMI.ResetText()
        CombMarche.ResetText()
    End Sub

    Private Sub NewDp_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

#Region "Bouton"
    Private Sub VisibleOtherTabs(ByVal Value As Boolean)
        'On désactive les autres tabs pour amener l'user à enregister les données de base.
        PageDonneesBase.PageEnabled = Value
        PageDonneesPartic.PageEnabled = Value
        PageEvaluation.PageEnabled = Value
        PageTDR.PageEnabled = Value
        PageAppercuDp.PageEnabled = Value
    End Sub

    Private Sub BtNouveau_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtNouveau.ItemClick
        If (PourAjout = False And PourModif = False And DejaDansLaBD = False) Then
            PourAjout = True
            PageDonneesBase.PageEnabled = True
            NewReadOnlyDP(False)
            BtEnregistrer.Enabled = True
            BtRetour.Enabled = True
            If Not TxtNumDp.Enabled Then TxtNumDp.Enabled = True
        ElseIf (PourAjout = True) Then
            SuccesMsg("Veuillez enregistrer le dossier en cours.")
        ElseIf (PourModif = True Or DejaDansLaBD = True) Then
            SuccesMsg("Veuillez fermer le dossier en cours.")
        End If
    End Sub

    Private Sub BtRetour_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtRetour.ItemClick
        DebutChargement(True, "Fermeture du dossier " & NumDoss & " en cours...")

        NumDoss = ""
        PourAjout = False
        PourModif = False
        DejaDansLaBD = False
        ModifTous = False
        TypeModif = ""

        'Initialisation des données de base
        InitialiserDP()

        RaseSaisieConsult()
        InitialiserDonneesPartic()
        'initialiser critere evaluation
        RaseCritereEval()
        NewReadOnlyDP(False)
        GridListeRestreinte.Rows.Clear()
        VisibleOtherTabs(False)
        TabAMettreAJour = {False, False, False, False, False}

        DocTDR.ResetText()
        CheminDocTDR = ""
        AjoutManuelConult(True)
        BtEnregistrer.Enabled = False
        BtRetour.Enabled = False
        AffichDossDp = False

        CmbDossAMI.Properties.ReadOnly = False
        CombMarche.Properties.ReadOnly = False
        CmbDossAMI.ResetText()
        CombMarche.ResetText()
        DateDepot.ResetText()
        FinChargement()
    End Sub

    Private Sub BtAppercu_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtAppercu.ItemClick
        If (TxtNumDp.Text <> "" And NumDoss <> "") Then
            Dim nbEnrg As Decimal = 0
            query = "select Count(*) from T_DP where NumeroDp='" & TxtNumDp.Text & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                nbEnrg = CInt(rw(0))
            Next

            If (nbEnrg <= 0) Then
                SuccesMsg("Dossier inexistant !")
                Exit Sub
            End If

            ReponseDialog = NumDoss
            SelectConsultant.ShowDialog()
            If (ReponseDialog = "") Then
                Exit Sub
            Else
                nomEtAdrConsImpression = ReponseDialog
                nomConsImprim = ExceptRevue
                ExceptRevue = ""
                ReponseDialog = ""
            End If

            ' UpdateInfoDossierDP()
            PageAppercuDp.PageVisible = True
            XtraTabDP.SelectedTabPage = PageAppercuDp
            BtEnregistrer.Enabled = True
            BtImprimer.Enabled = True
        Else
            SuccesMsg("Aucun enregistrement !")
        End If
    End Sub

    Private Sub GetDateAutomatique()
        Dim datefindepot As Date = CDate(DateEnvoie.Text)

        If JoursDelaiPub.Text.ToLower = "jours" Then
            datefindepot = datefindepot.AddDays(CInt(NbreDelaiPub.Text))
        ElseIf JoursDelaiPub.Text.ToLower = "semaines" Then
            datefindepot = datefindepot.AddDays(CInt(NbreDelaiPub.Text) * 7)
        ElseIf JoursDelaiPub.Text.ToLower = "mois" Then
            datefindepot = datefindepot.AddMonths(CInt(NbreDelaiPub.Text))
        End If

        DateDepot.Text = datefindepot
        If DateReporte.Text = "" And HeureReporte.Text = "" Then DateOuverture.Text = datefindepot
        If HeureDepot.Text.Trim <> "" And DateReporte.Text = "" And HeureReporte.Text = "" Then
            Dim HeurOuvertur As DateTime = datefindepot & " " & HeureDepot.Text
            HeurOuvertur = HeurOuvertur.AddMinutes(30)
            DateOuverture.Text = CDate(HeurOuvertur).ToShortDateString
            HeureOuverture.EditValue = CDate(HeurOuvertur).ToLongTimeString
        End If
    End Sub

    Private Sub DateEnvoie_EditValueChanged(sender As Object, e As EventArgs) Handles NbreDelaiPub.EditValueChanged, JoursDelaiPub.EditValueChanged, HeureDepot.EditValueChanged, DateEnvoie.EditValueChanged
        If DateEnvoie.Text.Trim <> "" And NbreDelaiPub.Text.Trim <> "" And JoursDelaiPub.Text.Trim <> "" Then
            GetDateAutomatique()
        End If
    End Sub

    Private Sub DateReporte_EditValueChanged(sender As Object, e As EventArgs) Handles DateReporte.EditValueChanged, HeureReporte.EditValueChanged
        If DateReporte.Text.Trim <> "" Then
            DateOuverture.Text = CDate(DateReporte.Text)
            If HeureReporte.Text.Trim <> "" Then
                Dim HeurOuvertur As DateTime = CDate(DateReporte.Text) & " " & HeureReporte.Text
                HeurOuvertur = HeurOuvertur.AddMinutes(30)
                DateOuverture.Text = CDate(HeurOuvertur).ToShortDateString
                HeureOuverture.EditValue = CDate(HeurOuvertur).ToLongTimeString
            End If
        End If
    End Sub

    Private Sub BtEnregistrer_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtEnregistrer.ItemClick

        If TxtNumDp.Text.Trim <> "" Then
            NumDoss = EnleverApost(TxtNumDp.Text.Replace(":", "")) 'A cause des 2 (:) du cle d'authentification
        End If
        If VerifierTraiterMethode(TxtMethodeSelect.Text) = False Then
            FailMsg("Aucun traitement prévu pour la méthode [" & TxtMethodeSelect.Text & "]")
            Exit Sub
        End If

        If Not DejaDansLaBD Then

            If TxtNumDp.IsRequiredControl("Veuillez saisir le numéro de la DP") Then
                TxtNumDp.Focus()
                Exit Sub
            End If
            If TxtLibDp.IsRequiredControl("Veuillez saisir la description de la DP") Then
                TxtLibDp.Focus()
                Exit Sub
            End If

            If DateEnvoie.Text.Trim = "" Or HeureEnvoi.Text.Trim = "" Then
                SuccesMsg("Veuillez selectionné la date d'envoi des propositions")
                DateEnvoie.Select()
                Exit Sub
            End If

            If NbreDelaiPub.Text.Trim = "" Then
                SuccesMsg("Veuillez saisir le délai de publication")
                NbreDelaiPub.Focus()
                Exit Sub
            End If

            If JoursDelaiPub.Text.Trim = "" Then
                SuccesMsg("Veuillez saisir le délai de publication")
                JoursDelaiPub.Focus()
                Exit Sub
            End If

            '  If DateDepot.Text = "" Or HeureDepot.Text = "" Then
            If HeureDepot.Text.Trim = "" Then
                SuccesMsg("Veuillez saisir l'heure de depôt")
                HeureDepot.Focus()
                Exit Sub
            End If

            'If DateReporte.Text.Trim <> "" And HeureReporte.Text.Trim <> "" Then
            '    Dim DateEnvoi = DateEnvoie.Text & " " & HeureEnvoi.Text
            '    Dim DateReportes = DateReporte.Text & " " & HeureReporte.Text
            '    If DateTime.Compare(CDate(DateEnvoi), CDate(DateReportes)) > 0 Then
            '        SuccesMsg("La date de reporte des dossiers doit être" & vbNewLine & " supperieure à la date de dépot des dossiers")
            '        DateReporte.Select()
            '        Exit Sub
            '    End If
            'End If

            If DateOuverture.Text = "" Or HeureOuverture.Text = "" Then
                SuccesMsg("Veuillez definir la date et l'heure d'ouverture")
                DateOuverture.Focus()
                Exit Sub
            End If

            If LibellePublication.Text.Trim = "" Then
                SuccesMsg("Veuillez saisir l'organe de publication")
                LibellePublication.Focus()
                Exit Sub
            End If

            If CmbDossAMI.Text.Trim = "" And CombMarche.Text.Trim = "" Then
                SuccesMsg("Veuillez selectionné un marche ou un dossier d'ami")
                CmbDossAMI.Focus()
                Exit Sub
            End If

            If NumDelai.Text = "" Or Val(NumDelai.Text) = 0 Or Val(NumDelai.Value) < 0 Then
                SuccesMsg("Veuillez indiqué le nombre de jour de travail")
                NumDelai.Select()
                Exit Sub
            End If

            If CmbDelai.SelectedIndex = -1 Then
                SuccesMsg("Veuillez indiqué le nombre de jour de travail")
                CmbDelai.Select()
                Exit Sub
            End If

            If CmbTypeRemune.IsRequiredControl("Veuillez selectionné le type de rénumération") Then
                CmbTypeRemune.Focus()
                Exit Sub
            End If

            'If DateEnvoie.Text = "" Or HeureEnvoi.Text = "" Then
            '    SuccesMsg("Veuillez selectionné la date d'envoi des propositions")
            '    DateEnvoie.Focus()
            '    Exit Sub
            'End If

            If (DateReporte.Text = "" And HeureReporte.Text <> "") Or (DateReporte.Text <> "" And HeureReporte.Text = "") Then
                SuccesMsg("Veuillez selectionné la date et l'heure de reporte")
                DateReporte.Focus()
                Exit Sub
            End If

            'If DateDepot.Text.Trim <> "" And HeureDepot.Text.Trim <> "" And DateReporte.Text.Trim <> "" And HeureReporte.Text.Trim <> "" Then
            '    If DateTime.Compare(CDate(DateDepot.Text), CDate(DateReporte.Text)) > 0 Then
            '        SuccesMsg("La date de limite de dépot doit être inférieur à la date de reporte")
            '        Exit Sub
            '    End If
            'End If

            If LieuRemiseProposition.IsRequiredControl("Veuillez saisir le lieu de remise des propositions") Then
                LieuRemiseProposition.Select()
                Exit Sub
            End If

            If GridListeRestreinte.RowCount = 0 Then
                SuccesMsg("Veuillez ajouter un consultant")
                Exit Sub
            End If

            'Dans la table DP
            query = "select count(NumeroDp) from t_dp where NumeroDp='" & EnleverApost(TxtNumDp.Text) & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                SuccesMsg("Le numero de la DP existe déjà")
                TxtNumDp.Focus()
                Exit Sub
            End If

            'Dans la table DP
            query = "select count(NumeroDAO) from t_dao where NumeroDAO='" & EnleverApost(TxtNumDp.Text) & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                SuccesMsg("Le numero de la DP existe déjà")
                TxtNumDp.Focus()
                Exit Sub
            End If

            'Dans la table DP
            query = "select count(NumeroDAMI) from T_AMI where NumeroDAMI='" & EnleverApost(TxtNumDp.Text) & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                SuccesMsg("Le numero de la DP existe déjà")
                TxtNumDp.Focus()
                Exit Sub
            End If

            Try
                DebutChargement(True, "Enregistrement des données de base en cours...")

                CreerDP()
                EnregistrerConsultant()
                'Insertion line donnée particulières
                ExecuteNonQuery("INSERT INTO t_dp_donneparticuliere(RefDP,NumeroDp,CodeProjet) values(NULL,'" & EnleverApost(TxtNumDp.Text) & "', '" & ProjetEnCours & "')")
                SuccesMsg("Dossier enregistrer avec succès")

                'OuvrirGroupPartic() ' Données particulières
                BtImportTDR.Enabled = True  ' TDR
                BtModifTDR.Enabled = True   ' TDR

                TxtNumDp.Enabled = False
                ArchivesDP()
                ChargerMarcher()
                ChargerDossierAMI()

                'XtraTabDP.SelectedTabPageIndex = 1
                'TabAMettreAJour(1) = True
                DejaDansLaBD = True
                VisibleOtherTabs(True)
                CmbDossAMI.Properties.ReadOnly = True
                CombMarche.Properties.ReadOnly = True
                FinChargement()

            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        Else

            DebutChargement(True, "Enregistrement des données en cours...")
            Dim Trouver As Boolean = False

            Dim LstTabName As New List(Of String) From {"PageDonneesBase", "PageDonneesPartic", "PageEvaluation", "PageTDR", "PageAppercuDp"}
            For i = 0 To XtraTabDP.TabPages.Count - 1
                If TabAMettreAJour(i) Then 'On doit mettre à jour les données de cette tab
                    Dim CurrentTab As DevExpress.XtraTab.XtraTabPage = XtraTabDP.TabPages(i)

                    Select Case CurrentTab.Name
                        Case "PageDonneesBase"
                            If SavePageDonneeBase(NumDoss) Then
                                Trouver = True
                                Exit Select
                            Else
                                FinChargement()
                                Exit Sub
                            End If
                        Case "PageDonneesPartic"
                            If SavePageDonnePartic(NumDoss) Then
                                Trouver = True
                                Exit Select
                            Else
                                FinChargement()
                                Exit Sub
                            End If
                        Case "PageEvaluation"
                            If SavePageEvaluation(NumDoss) Then
                                Trouver = True
                                Exit Select
                            Else
                                FinChargement()
                                Exit Sub
                            End If
                        'Case "PageTDR"
                        '    If SavePageTDR(NumDoss) Then
                        '        Trouver = True
                        '        Exit Select
                        '    Else
                        '        Exit Sub
                        '    End If
                        Case "PageAppercuDp"
                            If SavePageAppercuDp(NumDoss) Then
                                Trouver = True
                                Exit Select
                            Else
                                FinChargement()
                                Exit Sub
                            End If
                        Case Else
                            Exit Select
                    End Select
                End If
            Next
            FinChargement()

            If Trouver = True Then
                SuccesMsg("Enregistrement effectué avec succès")
            End If

        End If
    End Sub

    Private Sub ContextMenuStrip2_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip2.Opening
        If LayoutView1.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub
    Private Sub AfficherDossier_Click(sender As Object, e As EventArgs) Handles AfficherDossier.Click
        If (PourAjout = False And PourModif = False And DejaDansLaBD = False) Then
            AffichDossDp = True
            ModifierDossier_Click(Me, e)
            BtEnregistrer.Enabled = False
            NewReadOnlyDP(True)
        Else
            SuccesMsg("Veuillez fermer le dossier en cours")
        End If
    End Sub

    Private Sub ImprimerDossier_Click(sender As Object, e As EventArgs) Handles ImprimerDossier.Click
        If LayoutView1.RowCount > 0 Then
            Try
                ' If (PourAjout = False And PourModif = False And DejaDansLaBD = False) Then
                dr = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
                NumDoss = dr("N°").ToString
                Dim NomRepCheminSauve As String = line & "\DP\" & FormatFileName(NumDoss.ToString, "_") & "\ElaborationDp.pdf"
                Dim DossierGenerer As Boolean = False

                'Dossier valider par la bailleur de fond et fichier existant on actualise plus
                If DateTime.Compare(CDate(dr("DateLimitePropo").ToString), Now) < 0 And dr("DossValider").ToString = "Valider" And File.Exists(NomRepCheminSauve) = True Then
                    DossierGenerer = True
                End If

                If DossierGenerer = False Then
                    If UpdateInfoDossierDP() = False Then
                        Exit Sub
                    End If
                End If

                If File.Exists(NomRepCheminSauve) = True Then
                    Process.Start(NomRepCheminSauve)
                Else
                    SuccesMsg("Le fichier spécifié n'existe pas ou a été supprimer")
                End If

            Catch ex As Exception
                FinChargement()
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub SupprimerDossier_Click(sender As Object, e As EventArgs) Handles SupprimerDossier.Click
        If LayoutView1.RowCount > 0 Then

            If PourModif = True Or PourAjout = True Or DejaDansLaBD = True Then
                SuccesMsg("Veuillez fermer le dossier en cours")
                Exit Sub
            End If

            dr = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
            NumDoss = EnleverApost(dr("N°").ToString)

            'If dr("DossValider").ToString = "Valider" Then
            '    SuccesMsg("Impossible de supprimer ce dossier")
            '    Exit Sub
            'End If

            Dim rwDoss = ExcecuteSelectQuery("select DateLimitePropo from t_dp where NumeroDp='" & NumDoss & "'").Rows(0)
            If DateTime.Compare(CDate(rwDoss("DateLimitePropo").ToString), Now) < 0 And dr("DossValider").ToString = "Valider" Then
                SuccesMsg("Impossible de supprimer ce dossier")
                Exit Sub
            End If

            If ConfirmMsg("Voulez-vous supprimer ce dossier ?") = DialogResult.Yes Then
                DebutChargement()
                Dim rwDp As DataRow = ExcecuteSelectQuery("Select NumeroAMI, RefMarche from t_dp Where NumeroDp='" & NumDoss & "'").Rows(0)

                query = "delete from t_dp where NumeroDp='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

                query = "delete from t_commission where NumeroDAO='" & NumDoss & "'"
                ExecuteNonQuery(query)

                query = "delete from t_dp_critereeval where NumeroDp='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

                query = "delete from t_dp_listerembours where NumeroDp='" & NumDoss & "'"
                ExecuteNonQuery(query)

                query = "delete from t_dp_listeeltsfournis where NumeroDp='" & NumDoss & "'"
                ExecuteNonQuery(query)

                query = "delete from t_dp_section where NumeroDp='" & NumDoss & "'"
                ExecuteNonQuery(query)

                ExecuteNonQuery("delete from t_consultant where NumeroDp='" & NumDoss & "'")
                ExecuteNonQuery("delete from t_dp_donneparticuliere where NumeroDp='" & NumDoss & "'")

                If rwDp("NumeroAMI").ToString <> "" Then
                    ExecuteNonQuery("Update t_ami set DossUtiliser=NULL where NumeroDAMI='" & rwDp("NumeroAMI") & "'")
                Else
                    ExecuteNonQuery("Update t_marche set NumeroDAO=NULL where RefMarche='" & rwDp("RefMarche") & "'")
                End If

                FinChargement()
                SuccesMsg("Dossier supprimé avec succès")
                ChargerDossierAMI()
                ChargerMarcher()
                ArchivesDP()
            End If
        End If

    End Sub

    Private Sub ValiderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValiderToolStripMenuItem.Click
        If LayoutView1.RowCount > 0 Then

            dr = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
            NumDoss = EnleverApost(dr("N°").ToString)

            If dr("Statut").ToString = "Annuler" Then
                FailMsg("Ce marché a été annuler")
                Exit Sub
            End If

            If dr("Statut").ToString = "Terminer" Then
                FailMsg("Ce marché a été executer")
                Exit Sub
            End If
            If Val(ExecuteScallar("select count(*) from t_dp_critereeval where NumeroDp='" & EnleverApost(dr("N°").ToString) & "' and PointCritere IS NULL and CodeProjet='" & ProjetEnCours & "'")) > 0 Then
                SuccesMsg("Un critère d'évaluation n'a pas de note" & vbNewLine & "Veuillez donc la saisir")
                Exit Sub
            End If

            'Dim ConfirmBalleur As String = ExecuteScallar("SELECT RaportEvalTechBailleur FROM t_dp WHERE NumeroDp='" & EnleverApost(dr("N°").ToString) & "' and CodeProjet='" & ProjetEnCours & "'").ToString
            Dim ConfirmBalleur As String = ExecuteScallar("SELECT DateOuvertureEffective FROM t_dp WHERE NumeroDp='" & EnleverApost(dr("N°").ToString) & "' and CodeProjet='" & ProjetEnCours & "'").ToString
            If ConfirmBalleur.ToString <> "" Then
                SuccesMsg("Ce dossier a été valider")
                Exit Sub
            End If

            If ConfirmMsg("Confirmez-vous la validation du dossier ?") = DialogResult.Yes Then

                DebutChargement(True, "Traitement de la validation du dossier en cours...")

                ExecuteNonQuery("UPDATE t_dp set DossValider='Valider' where NumeroDp='" & EnleverApost(dr("N°").ToString) & "' and CodeProjet='" & ProjetEnCours & "'")

                query = "SELECT CodeMem, Civil, NomMem, EmailMem FROM T_Commission WHERE NumeroDAO='" & EnleverApost(dr("N°").ToString) & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)

                Dim CodeCrypter As String = String.Empty
                For Each rw0 In dt0.Rows
                    CodeCrypter = GenererToken(dr("N°").ToString, rw0("CodeMem"), "DP", DB)
                    ExecuteNonQuery("Update T_Commission set AuthKey='" & CodeCrypter.ToString.Split(":")(0) & "' where CodeMem='" & rw0("CodeMem") & "' and NumeroDAO='" & EnleverApost(dr("N°").ToString) & "'")
                    envoieMail(rw0("Civil").ToString & " " & MettreApost(rw0("NomMem").ToString), MettreApost(rw0("EmailMem").ToString), CodeCrypter)
                Next
                FinChargement()

                SuccesMsg("Dossier valider avec succès")
                ' ArchivesDP()
                'Update validation dossier
                LayoutView1.SetFocusedRowCellValue("DossValider", "Valider")

            End If
        End If
    End Sub


    Private Sub ModifierDossier_Click(sender As Object, e As EventArgs) Handles ModifierDossier.Click
        If LayoutView1.RowCount > 0 Then
            If (PourAjout = False And PourModif = False And DejaDansLaBD = False) Then

                dr = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
                NumDoss = EnleverApost(dr("N°").ToString)
                ' Dim rwDoss As DataRow

                'If AffichDossDp = False Then
                '    If Val(ExecuteScallar("SELECT COUNT(*) FROM t_commission WHERE NumeroDAO='" & EnleverApost(NumDoss) & "' AND Pointage<>''")) > 0 Then
                '        SuccesMsg("Impossible de modifier ce dossier")
                '        Exit Sub
                '    End If
                'End If

                If AffichDossDp = False Then
                    '  rwDoss = ExcecuteSelectQuery("select DateLimitePropo from t_dp where NumeroDp='" & NumDoss & "'").Rows(0)
                    If DateTime.Compare(CDate(dr("DateLimitePropo").ToString), Now) < 0 And dr("DossValider").ToString = "Valider" Then
                        SuccesMsg("Impossible de modifier ce dossier")
                        Exit Sub
                    End If
                End If

                DebutChargement()
                TxtNumDp.Text = NumDoss
                TxtNumDp.Enabled = False
                BtRetour.Enabled = True
                BtEnregistrer.Enabled = True

                PourModif = True
                DejaDansLaBD = True

                VisibleOtherTabs(True)
                'If AffichDossDp = False Then
                '    If dr("DossValider").ToString = "Valider" Then
                '        'SuccesMsg("Impossible de modifier ce dossier")
                '        'Exit Sub
                '        'desactive tous les champs seul la date reporte est modifiable
                '        ' NewReadOnlyDP(True)
                '        'Seul la date reporte est modifiable
                '        ' DateReporte.Properties.ReadOnly = False
                '        ' HeureReporte.Properties.ReadOnly = False
                '        'DatePub1.Properties.ReadOnly = False
                '        ' DatePub2.Properties.ReadOnly = False
                '        'LibellePublication.Properties.ReadOnly = False
                '        'LieuRemiseProposition.Properties.ReadOnly = False
                '        'VisibleOtherTabs(False)
                '        ' PageDonneesBase.PageEnabled = True
                '    End If
                'End If

                XtraTabDP.SelectedTabPageIndex = 0

                CmbDossAMI.Properties.ReadOnly = True
                CombMarche.Properties.ReadOnly = True
                FinChargement()
            Else
                SuccesMsg("Veuillez enregistrer ou fermer le dossier en cours")
            End If
        End If
    End Sub
#End Region

#Region "Methodes"
    Private Sub ItemDevise()
        CmbDevise.Properties.Items.Clear()
        query = "select AbregeDevise from T_Devise"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbDevise.Properties.Items.Add(rw("AbregeDevise").ToString)
        Next
    End Sub

    Private Sub ChargerDossierAMI()
        ' CmbDossAMI.Text = ""
        CmbDossAMI.Properties.Items.Clear()
        Try
            query = "SELECT NumeroDAMI from t_ami WHERE ValidationsRapports= 'Valider' and CodeProjet='" & ProjetEnCours & "' and DossUtiliser IS NULL"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                CmbDossAMI.Properties.Items.Add(MettreApost(rw("NumeroDAMI").ToString))
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub ChargerMarcher()

        'query = "Select RefMarche, DescriptionMarche, MontantEstimatif, Convention_ChefFile from T_Marche where CodeProjet='" & ProjetEnCours & "' AND TypeMarche LIKE 'Consultants%' AND NumeroDAO IS NULL order by TypeMarche ASC"

        query = "Select RefMarche, DescriptionMarche, MontantEstimatif, Convention_ChefFile from T_Marche where CodeProjet='" & ProjetEnCours & "' AND TypeMarche LIKE 'Consultants%' and NumeroMarche IS NULL"
        Dim dt As DataTable = ExcecuteSelectQuery(query)

        Dim Taille As Integer = 0
        CombMarche.Properties.Items.Clear()
        ' CombMarche.ResetText()

        Dim MontantMarcheRestant As Decimal = 0
        For Each rw As DataRow In dt.Rows
            'Montant marche restant (à utiliser)
            MontantMarcheRestant = CDec(rw("MontantEstimatif").ToString.Replace(" ", "")) - NewVerifierMontMarche(rw("RefMarche")) 'Montant consomé
            If MontantMarcheRestant > 0 Then
                ReDim Preserve RefMarche(Taille)
                RefMarche(Taille) = rw("RefMarche")
                Taille += 1
                CombMarche.Properties.Items.Add(MettreApost(rw("DescriptionMarche")) & " | " & MontantMarcheRestant & " | " & GetInitialbailleur(rw("Convention_ChefFile").ToString) & "(" & rw("Convention_ChefFile").ToString & ")")
            End If
        Next
    End Sub

    Private Sub ItemsPays()
        CmbPays.Properties.Items.Clear()
        query = "select LibelleZone from T_ZoneGeo where CodeZoneMere='0'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbPays.Properties.Items.Add(MettreApost(rw("LibelleZone").ToString))
        Next
    End Sub

    Private Sub XtraTabDP_SelectedPageChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles XtraTabDP.SelectedPageChanged
        If XtraTabDP.SelectedTabPageIndex <> -1 Then
            If DejaDansLaBD Then
                If Not TabAMettreAJour(LstTabName.IndexOf(e.Page.Name)) Then
                    TabAMettreAJour(LstTabName.IndexOf(e.Page.Name)) = True
                    If PourModif Or PourAjout Then
                        LoadPage(e.Page.Name, NumDoss)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub LoadPage(ByVal PageName As String, ByVal NumDossier As String)

        If PageName = "PageDonneesBase" Then
            LoadPageDonneBase(NumDossier)
        End If
        If PageName = "PageDonneesPartic" Then
            LoadPageDonnePartic(NumDossier)
        End If

        If PageName = "PageEvaluation" Then
            LoadPageCritereEvaluation(NumDossier)
        End If

        If PageName = "PageTDR" Then
            'LoadPageSpecTech(NumDossier)
            LoadPageSpecTech()
        End If

        'If PageName = "PageAppercuDp" Then
        '    LoadPageConformTechnique(NumDossier)
        'End If

    End Sub
#End Region

#Region "Données de base"

    Private Function SavePageDonneeBase(ByVal NumDoss As String) As Boolean

        If PourModif = True Then
            'Mise a jour DP
            Try
                'If DatePub1.Text.Trim = "" Then
                '    SuccesMsg("Veuillez selectionné la date de publication")
                '    DatePub1.Focus()
                '    Return False
                'End If

                If NbreDelaiPub.Text.Trim = "" Then
                    SuccesMsg("Veuillez saisir le délai de publication")
                    NbreDelaiPub.Focus()
                    Return False
                End If

                If JoursDelaiPub.Text.Trim = "" Then
                    SuccesMsg("Veuillez saisir le délai de publication")
                    JoursDelaiPub.Focus()
                    Return False
                End If

                '  If DateDepot.Text = "" Or HeureDepot.Text = "" Then
                If HeureDepot.Text.Trim = "" Then
                    SuccesMsg("Veuillez saisir l'heure de depôt des propositions")
                    HeureDepot.Focus()
                    Return False
                End If

                If DateOuverture.Text = "" Or HeureOuverture.Text = "" Then
                    SuccesMsg("Veuillez définir la date et l'heure d'ouverture")
                    DateOuverture.Focus()
                    Return False
                End If

                If LibellePublication.Text.Trim = "" Then
                    SuccesMsg("Veuillez saisir l'organe de publication")
                    LibellePublication.Focus()
                    Return False
                End If

                If NumDelai.Text = "" Or Val(NumDelai.Text) <= 0 Then
                    SuccesMsg("Veuillez indiqué le nombre de jour de travail")
                    NumDelai.Focus()
                    Return False
                End If

                'If CmbDossAMI.Text.Trim = "" And CombMarche.Text.Trim = "" Then
                '    SuccesMsg("Veuillez selectionné un marche ou un dossier d'ami")
                '    CmbDossAMI.Focus()
                '    Return False
                'End If

                'If NumDelai.Value = 0 Or NumDelai.Value < 0 Then
                '    SuccesMsg("Veuillez indiqué le nombre de jour de travail")
                '    NumDelai.Select()
                '    Return False
                'End If

                If CmbDelai.SelectedIndex = -1 Then
                    SuccesMsg("Veuillez indiqué le nombre de jour de travail")
                    CmbDelai.Select()
                    Return False
                End If

                If CmbTypeRemune.IsRequiredControl("Veuillez selectionné le type de rénumération") Then
                    CmbTypeRemune.Focus()
                    Return False
                End If

                If DateEnvoie.Text = "" Or HeureEnvoi.Text = "" Then
                    SuccesMsg("Veuillez selectionné la date d'envoi des propositions")
                    DateEnvoie.Focus()
                    Return False
                End If

                If (DateReporte.Text = "" And HeureReporte.Text <> "") Or (DateReporte.Text <> "" And HeureReporte.Text = "") Then
                    SuccesMsg("Veuillez selectionné la date l'heure reporte")
                    DateReporte.Focus()
                    Return False
                End If

                'If DateEnvoie.Text.Trim <> "" And HeureEnvoi.Text.Trim <> "" And DateReporte.Text.Trim <> "" And HeureReporte.Text.Trim <> "" Then
                '    Dim dateenvoi = DateEnvoie.Text & " " & HeureEnvoi.Text
                '    Dim datereport = DateReporte.Text & " " & HeureReporte.Text
                '    If DateTime.Compare(CDate(dateenvoi), CDate(datereport)) > 0 Then
                '        SuccesMsg("La date de reporte des dossiers doit être" & vbNewLine & " supperieure à la date de dépot des dossiers")
                '        DateReporte.Select()
                '        Return False
                '    End If
                'End If

                If LieuRemiseProposition.IsRequiredControl("Veuillez saisir le lieu de remise des propositions") Then
                    LieuRemiseProposition.Select()
                    Return False
                End If

                'If GridListeRestreinte.RowCount = 0 Then
                '    SuccesMsg("Veuillez ajouter un consultant")
                '    Exit Sub
                'End If

                'If DateDepot.Text.Trim <> "" And HeureDepot.Text.Trim <> "" And DateReporte.Text.Trim <> "" And HeureReporte.Text.Trim <> "" Then
                '    If DateTime.Compare(CDate(DateDepot.Text), CDate(DateReporte.Text)) > 0 Then
                '        SuccesMsg("La date de limite de dépot doit être inférieur à la date de reporte")
                '        DateReporte.Select()
                '        Return False
                '    End If
                'End If

                Dim DateReporter As String = ""
                If DateReporte.Text <> "" And HeureReporte.Text <> "" Then DateReporter = CDate(DateReporte.DateTime).ToShortDateString & " " & CDate(HeureReporte.Time).ToLongTimeString

                query = "Update t_dp set LibelleMiss='" & EnleverApost(TxtLibDp.Text) & "', TypeRemune='" & EnleverApost(CmbTypeRemune.Text) & "', ListeRestreinte = '" & GridListeRestreinte.RowCount & "', NumeroAMI = '" & EnleverApost(CmbDossAMI.Text) & "', "
                query &= "DateLimitePropo='" & dateconvert(DateDepot.Text) & " " & CDate(HeureDepot.Time).ToLongTimeString & "', DateOuverture = '" & dateconvert(DateOuverture.Text) & " " & CDate(HeureOuverture.Time).ToLongTimeString & "', DateEnvoiDp='" & dateconvert(DateEnvoie.Text) & " " & CDate(HeureEnvoi.Time).ToLongTimeString & "', "
                ' query &= "DatePub1='" & IIf(DatePub1.Text <> "", DatePub1.Text, "").ToString & "', DatePub2= '" & IIf(DatePub2.Text <> "", DatePub2.Text, "").ToString & "', MoyenPub='" & EnleverApost(LibellePublication.Text) & "', NbreJourTravail= '" & NumDelai.Text & " " & CmbDelai.Text & "', DelaiPublication= '" & NbreDelaiPub.Text & " " & JoursDelaiPub.Text & "', "
                query &= " MoyenPub='" & EnleverApost(LibellePublication.Text) & "', NbreJourTravail= '" & NumDelai.Text & " " & CmbDelai.Text & "', DelaiPublication= '" & NbreDelaiPub.Text & " " & JoursDelaiPub.Text & "', "
                query &= " DateModif = '" & dateconvert(Now.ToShortDateString) & " " & Now.ToShortTimeString & "', CodeConvention='" & CodeConvention.ToString & "', DateReporter='" & DateReporter.ToString & "', LieuRemisePropo='" & EnleverApost(LieuRemiseProposition.Text) & "' where NumeroDp='" & NumDoss & "'"

                ExecuteNonQuery(query)

                ExecuteNonQuery("Update t_marche Set Forfait_TpsPasse='" & EnleverApost(CmbTypeRemune.Text) & "' Where RefMarche='" & CodeMarcheEnCours & "' and CodeProjet='" & ProjetEnCours & "'")

                EnregistrerConsultant()
            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        End If
        Return True
    End Function

    Private Sub LoadPageDonneBase(ByVal NumDossier As String)
        If PourModif = True Then
            Try
                query = "Select d.*, m.DescriptionMarche, m.MontantEstimatif,m.Convention_ChefFile from T_DP as d, T_Marche as m where d.RefMarche=m.RefMarche and d.NumeroDp='" & NumDossier & "' and  d.CodeProjet='" & ProjetEnCours & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw In dt0.Rows

                    TxtNumDp.Text = NumDoss
                    TxtLibDp.Text = MettreApost(rw("LibelleMiss").ToString)
                    CmbTypeRemune.Text = MettreApost(rw("TypeRemune").ToString)

                    TxtMethodeSelect.Text = MettreApost(rw("MethodeSelection").ToString)
                    MontantMarche.Text = AfficherMonnaie(rw("MontantMarche").ToString.Replace(" ", "").Replace("?", ""))
                    CmbDossAMI.Text = MettreApost(rw("NumeroAMI").ToString)
                    CodeMarcheEnCours = rw("RefMarche").ToString

                    If rw("DelaiPublication").ToString <> "" Then
                        NbreDelaiPub.Text = CInt(rw("DelaiPublication").ToString.Split(" "c)(0))
                        JoursDelaiPub.Text = rw("DelaiPublication").ToString.Split(" "c)(1)
                    End If

                    If rw("DateLimitePropo").ToString <> "" Then
                        DateDepot.DateTime = CDate(rw("DateLimitePropo")).ToShortDateString
                        HeureDepot.Time = CDate(rw("DateLimitePropo")).ToLongTimeString
                    End If
                    If rw("DateOuverture").ToString <> "" Then
                        DateOuverture.DateTime = CDate(rw("DateOuverture")).ToShortDateString
                        HeureOuverture.Time = CDate(rw("DateOuverture")).ToLongTimeString
                    End If

                    If rw("DateReporter").ToString <> "" Then
                        DateReporte.DateTime = CDate(rw("DateReporter")).ToShortDateString
                        HeureReporte.Time = CDate(rw("DateReporter")).ToLongTimeString
                    End If

                    LieuRemiseProposition.Text = MettreApost(rw("LieuRemisePropo").ToString)

                    '  DatePub1.Text = ""
                    ' DatePub2.Text = ""

                    ' If rw("DatePub1").ToString <> "" Then DatePub1.DateTime = CDate(rw("DatePub1")).ToShortDateString
                    ' If rw("DatePub2").ToString <> "" Then DatePub1.DateTime = CDate(rw("DatePub2")).ToShortDateString
                    'If Val(rw("NbreJourTravail").ToString) > 0 Then NbreJourTravail.EditValue = Val(rw("NbreJourTravail").ToString)

                    If rw("NbreJourTravail").ToString <> "" Then
                        NumDelai.Value = Val(rw("NbreJourTravail").ToString.Split(" "c)(0))
                        CmbDelai.Text = rw("NbreJourTravail").ToString.Split(" "c)(1)
                    End If

                    LibellePublication.Text = MettreApost(rw("MoyenPub").ToString)

                    If rw("DateEnvoiDp").ToString <> "" Then
                        DateEnvoie.DateTime = CDate(rw("DateEnvoiDp")).ToShortDateString
                        HeureEnvoi.Time = CDate(rw("DateEnvoiDp")).ToLongTimeString
                    End If

                    TabAMettreAJour(0) = True
                    '  CodeConvention = rw("CodeConvention").ToString
                    CodeConvention = rw("Convention_ChefFile").ToString

                    CheminDocTDR = rw("CheminDocTDR").ToString

                    If rw("NumeroAMI").ToString = "" Then
                        ' query = "Select DescriptionMarche, MontantEstimatif, InitialeBailleur, Convention_ChefFile from T_Marche where CodeProjet='" & ProjetEnCours & "' AND RefMarche='" & CodeMarcheEnCours & "'"
                        ' Dim dts As DataTable = ExcecuteSelectQuery(query)
                        ' For Each rw In dts.Rows
                        ' CodeConvention = rw("Convention_ChefFile").ToString
                        CombMarche.Text = MettreApost(rw("DescriptionMarche").ToString) & " | " & AfficherMonnaie(rw("MontantMarche").ToString.Replace(" ", "")) & " | " & GetInitialbailleur(rw("Convention_ChefFile").ToString) & "(" & rw("Convention_ChefFile").ToString & ")"
                        ' Next
                    End If
                Next

                ChargerGridConsult(NumDossier)
            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub
    Private Sub ArchivesDP()
        dt.Columns.Clear()

        dt.Columns.Add("N°", Type.GetType("System.String"))
        dt.Columns.Add("Edité le", Type.GetType("System.String"))
        dt.Columns.Add("Rémun.", Type.GetType("System.String"))
        dt.Columns.Add("Méthode", Type.GetType("System.String"))
        dt.Columns.Add("Liste", Type.GetType("System.String"))
        dt.Columns.Add("Ouverture", Type.GetType("System.String"))
        dt.Columns.Add("Date", Type.GetType("System.String"))
        dt.Columns.Add("Mission", Type.GetType("System.String"))
        dt.Columns.Add("DateOuvertures", Type.GetType("System.String"))
        dt.Columns.Add("DossValider", Type.GetType("System.String"))
        dt.Columns.Add("CheminDocTDR", Type.GetType("System.String"))
        dt.Columns.Add("Statut", Type.GetType("System.String"))
        dt.Columns.Add("DateLimitePropo", Type.GetType("System.String"))

        dt.Rows.Clear()
        query = "select NumeroDP,DateEdition,TypeRemune,DossValider,CheminDocTDR,MethodeSelection,ListeRestreinte,DateFinOuverture,Statut,DateOuverture,LibelleMiss, DateLimitePropo from T_DP where CodeProjet='" & ProjetEnCours & "' order by NumeroDP"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            Dim dr1 = dt.NewRow()
            dr1("N°") = rw("NumeroDP").ToString
            dr1("Edité le") = rw("DateEdition").ToString
            dr1("Rémun.") = rw("TypeRemune").ToString
            dr1("DateOuvertures") = rw("DateOuverture").ToString
            dr1("Méthode") = rw("MethodeSelection").ToString
            dr1("Liste") = rw("ListeRestreinte").ToString & " Consultants"
            dr1("DossValider") = rw("DossValider").ToString
            dr1("CheminDocTDR") = rw("CheminDocTDR").ToString
            dr1("DateLimitePropo") = rw("DateLimitePropo").ToString

            If (rw("DateFinOuverture").ToString <> "") Then
                dr1("Ouverture") = "Effectuée"
                dr1("Date") = Mid(rw("DateFinOuverture").ToString, 1, 10) & " à " & Mid(rw("DateFinOuverture").ToString, 12, 5).Replace(":", " h ") & " mn"
            Else
                If (rw("DateOuverture").ToString <> "") Then
                    dr1("Ouverture") = "Non effectuée"
                    dr1("Date") = Mid(rw("DateOuverture").ToString, 1, 10) & " à " & Mid(rw("DateOuverture").ToString, 12, 5).Replace(":", " h ") & " mn"

                Else
                    dr1("Ouverture") = "Non Prévue"
                    dr1("Date") = "__/__/____"
                End If
            End If
            dr1("Mission") = MettreApost(rw("LibelleMiss").ToString)
            dr1("Statut") = MettreApost(rw("Statut").ToString)
            dt.Rows.Add(dr1)
        Next

        GridArchives.DataSource = dt
        LayoutView1.Columns("DateOuvertures").Visible = False
        LayoutView1.Columns("DossValider").Visible = False
        LayoutView1.Columns("CheminDocTDR").Visible = False
        LayoutView1.Columns("DateLimitePropo").Visible = False
    End Sub

    Private Sub TxtNumDp_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNumDp.TextChanged
        If (TxtNumDp.Text.Trim <> "") Then
            TxtLibDp.Enabled = True
        Else
            TxtLibDp.Enabled = False
        End If
    End Sub

    Private Sub TxtLibDp_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtLibDp.TextChanged
        If (TxtLibDp.Text.Trim <> "") Then
            GroupCaracteristique.Enabled = True
            GroupMarche.Enabled = True
            GbListeConsult.Enabled = True
            GroupControlPub.Enabled = True
        Else
            GroupCaracteristique.Enabled = False
            GbListeConsult.Enabled = False
            GroupMarche.Enabled = False
            GroupControlPub.Enabled = False
        End If
    End Sub

    'Private Sub GridMarcheDp_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
    '    NumDoss = TxtNumDp.Text.Replace(" ", "")
    '    Dim nLg As Decimal = GridMarcheDp.CurrentRow.Index
    '    Dim nCol As Decimal = GridMarcheDp.CurrentCell.ColumnIndex
    '    If (nLg = GridMarcheDp.RowCount - 1 And nCol = 0) Then
    '        CreerDP()
    '        ReponseDialog = NumDoss
    '        DiagChoixMarcheConsult.ShowDialog()
    '        typeMarc = ReponseDialog
    '        methodMarc = TxtMethodeSelect.Text
    '        ReponseDialog = ""
    '        If (ExceptRevue <> "" And ChkLibDpAuto.Checked = True) Then
    '            TxtLibDp.Text = ExceptRevue
    '        End If
    '        MajGridMarche()
    '        CmbTypeRemune.Enabled = True
    '        DateDepot.Enabled = True
    '        HeureDepot.Enabled = True
    '        DateOuverture.Enabled = True
    '        HeureOuverture.Enabled = True
    '        GbListeConsult.Enabled = True

    '        '  OuvrirGroupPartic() ' Données particulières
    '        BtImportTDR.Enabled = True  ' TDR
    '        BtModifTDR.Enabled = True   ' TDR

    '        BtEnregistrer.Enabled = True
    '        BtRetour.Enabled = True
    '    End If
    'End Sub

    Private Sub CreerDP()
        Dim DatSet = New DataSet
        query = "select * from T_DP"
        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)
        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd)
        DatAdapt.Fill(DatSet, "T_DP")
        Dim DatTable = DatSet.Tables("T_DP")
        Dim DatRow = DatSet.Tables("T_DP").NewRow()

        DatRow("NumeroDP") = NumDoss
        DatRow("LibelleMiss") = EnleverApost(TxtLibDp.Text)
        DatRow("MethodeSelection") = TxtMethodeSelect.Text
        DatRow("TypeRemune") = EnleverApost(CmbTypeRemune.Text)
        DatRow("DateEdition") = dateconvert(Now.ToShortDateString)
        DatRow("MontantMarche") = MontantMarche.Text

        DatRow("DateLimitePropo") = CDate(DateDepot.DateTime).ToShortDateString & " " & CDate(HeureDepot.Time).ToLongTimeString
        DatRow("DateOuverture") = CDate(DateOuverture.DateTime).ToShortDateString & " " & CDate(HeureOuverture.Time).ToLongTimeString
        DatRow("DateEnvoiDp") = CDate(DateEnvoie.DateTime).ToShortDateString & " " & CDate(HeureEnvoi.Time).ToLongTimeString
        If DateReporte.Text <> "" Then DatRow("DateReporter") = CDate(DateReporte.DateTime).ToShortDateString & " " & CDate(HeureReporte.Time).ToLongTimeString
        DatRow("LieuRemisePropo") = EnleverApost(LieuRemiseProposition.Text)

        DatRow("ListeRestreinte") = GridListeRestreinte.RowCount
        DatRow("NumeroAMI") = EnleverApost(CmbDossAMI.Text)
        DatRow("RefMarche") = CodeMarcheEnCours
        DatRow("CodeConvention") = CodeConvention.ToString
        DatRow("MoyenPub") = EnleverApost(LibellePublication.Text)
        DatRow("DelaiPublication") = CInt(NbreDelaiPub.Text) & " " & JoursDelaiPub.Text
        If NumDelai.Value > 0 And CmbDelai.Text <> "" Then DatRow("NbreJourTravail") = NumDelai.Value & " " & CmbDelai.Text

        DatRow("DateModif") = dateconvert(Now.ToShortDateString) & " " & Now.ToShortTimeString
        DatRow("DateSaisie") = dateconvert(Now.ToShortDateString) & " " & Now.ToShortTimeString
        DatRow("Operateur") = CodeUtilisateur
        DatRow("CodeProjet") = ProjetEnCours
        DatRow("Statut") = "En cours"

        DatSet.Tables("T_DP").Rows.Add(DatRow)
        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        DatAdapt.Update(DatSet, "T_DP")
        DatSet.Clear()
        BDQUIT(sqlconn)

        ExecuteNonQuery("Update t_marche Set Forfait_TpsPasse='" & EnleverApost(CmbTypeRemune.Text) & "' Where RefMarche='" & CodeMarcheEnCours & "' and CodeProjet='" & ProjetEnCours & "'")
        If CmbDossAMI.SelectedIndex <> -1 Then
            ExecuteNonQuery("Update t_ami set DossUtiliser='" & NumDoss & "' where NumeroDAMI='" & EnleverApost(CmbDossAMI.Text) & "'")
        End If
    End Sub

    ' Private Sub EnregistrerConsultant(ByVal Actions As String)
    Private Sub EnregistrerConsultant()

        If GridListeRestreinte.RowCount > 0 Then
            Dim actuliser As Boolean = False

            For i = 0 To GridListeRestreinte.RowCount - 1

                If GridListeRestreinte.Rows.Item(i).Cells("RefConsult").Value.ToString = "" Then

                    query = "SELECT COUNT(*) FROM t_consultant where NomConsult = '" & EnleverApost(GridListeRestreinte.Rows.Item(i).Cells("Nom").Value) & "' and  EmailConsult = '" & EnleverApost(GridListeRestreinte.Rows.Item(i).Cells("Email").Value) & "' and TelConsult ='" & GridListeRestreinte.Rows.Item(i).Cells("Telephone").Value & "' and NumeroDp='" & NumDoss.ToString & "'"
                    If Val(ExecuteScallar(query) = 0) Then
                        actuliser = True
                        Dim DatSet = New DataSet
                        query = "select * from T_Consultant"
                        Dim sqlconn As New MySqlConnection
                        BDOPEN(sqlconn)
                        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                        Dim DatAdapt = New MySqlDataAdapter(Cmd)
                        DatAdapt.Fill(DatSet, "T_Consultant")
                        Dim DatTable = DatSet.Tables("T_Consultant")
                        Dim DatRow = DatSet.Tables("T_Consultant").NewRow()

                        DatRow("CodeConsult") = GenererCode(8)
                        DatRow("NomConsult") = EnleverApost(GridListeRestreinte.Rows.Item(i).Cells("Nom").Value)
                        DatRow("PaysConsult") = EnleverApost(GridListeRestreinte.Rows.Item(i).Cells("Pays").Value)
                        DatRow("TelConsult") = GridListeRestreinte.Rows.Item(i).Cells("Telephone").Value
                        DatRow("AdressConsult") = GridListeRestreinte.Rows.Item(i).Cells("Adresse").Value
                        DatRow("FaxConsult") = GridListeRestreinte.Rows.Item(i).Cells("Fax").Value
                        DatRow("EmailConsult") = EnleverApost(GridListeRestreinte.Rows.Item(i).Cells("Email").Value)
                        DatRow("NumeroDp") = TxtNumDp.Text
                        DatRow("DateSaisie") = dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString
                        DatRow("DateModif") = dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString
                        DatRow("Operateur") = CodeUtilisateur
                        DatSet.Tables("T_Consultant").Rows.Add(DatRow)
                        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                        DatAdapt.Update(DatSet, "T_Consultant")
                        DatSet.Clear()
                        BDQUIT(sqlconn)
                    End If

                ElseIf GridListeRestreinte.Rows.Item(i).Cells("Action").Value.ToString = "Modifier" Then
                    query = "Update T_Consultant set NomConsult= '" & EnleverApost(GridListeRestreinte.Rows.Item(i).Cells("Nom").Value) & "', PaysConsult='" & EnleverApost(GridListeRestreinte.Rows.Item(i).Cells("Pays").Value) & "', TelConsult='" & GridListeRestreinte.Rows.Item(i).Cells("Telephone").Value & "', FaxConsult='" & GridListeRestreinte.Rows.Item(i).Cells("Fax").Value & "', AdressConsult='" & GridListeRestreinte.Rows.Item(i).Cells("Adresse").Value & "', EmailConsult='" & EnleverApost(GridListeRestreinte.Rows.Item(i).Cells("Email").Value) & "', DateModif='" & dateconvert(Now.ToShortDateString & " " & Now.ToLongTimeString) & "' where NumeroDp='" & NumDoss & "' and RefConsult ='" & GridListeRestreinte.Rows.Item(i).Cells("RefConsult").Value & "'"
                    ExecuteNonQuery(query)
                End If
            Next

            If actuliser = True Then
                ChargerGridConsult(NumDoss)
            End If
        End If
    End Sub

    Private Sub InitialMonths()
        TxtMethodeSelect.Text = ""
        MontantMarche.Text = ""
    End Sub

    Private Sub CombMarche_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CombMarche.SelectedIndexChanged
        InitialMonths()

        If CombMarche.SelectedIndex <> -1 Then
            CmbDossAMI.Text = ""
            GridListeRestreinte.Rows.Clear()
            AjoutManuelConult(True)

            query = "SELECT CodeProcAO FROM t_marche WHERE RefMarche='" & RefMarche(CombMarche.SelectedIndex) & "'"
            Dim CodeProcAO As String = ExecuteScallar(query)
            If CodeProcAO <> "" Then
                TxtMethodeSelect.Text = GetMethode(CodeProcAO)
            Else
                TxtMethodeSelect.ResetText()
            End If

            CodeMarcheEnCours = RefMarche(CombMarche.SelectedIndex)
            ' MontantMarche.Text = AfficherMonnaie(CombMarche.Text.Split("|")(1))
            MontantMarche.Text = CombMarche.Text.Split("|")(1).ToString.Replace(" ", "")
            Dim CodConven As String = CombMarche.Text.Split("|")(2)
            CodeConvention = CodConven.ToString.Split("(")(1).Replace(")", "")
        End If
    End Sub

    Private Function VerifExistenceConsult(ByVal dtConsult As DataTable) As Boolean
        Try
            Dim reponse As Boolean = False
            If GridListeRestreinte.Rows.Count > 0 Then

                For n = 0 To GridListeRestreinte.Rows.Count - 1
                    For Each rw In dtConsult.Rows
                        If GridListeRestreinte.Rows.Item(n).Cells(2).Value = MettreApost(rw("NomConsult").ToString) And GridListeRestreinte.Rows.Item(n).Cells(5).Value = MettreApost(rw("EmailConsult").ToString) And GridListeRestreinte.Rows.Item(n).Cells(6).Value = MettreApost(rw("TelConsult").ToString) Then
                            Return True
                        End If
                    Next
                Next
            End If
            Return reponse

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Function

    Private Sub CmbDossAMI_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbDossAMI.SelectedIndexChanged
        InitialMonths()


        If CmbDossAMI.SelectedIndex <> -1 And TxtNumDp.Text.Trim <> "" Then
            CombMarche.Text = ""
            AjoutManuelConult(False)

            query = "select MethodeSelection, MontantMarche, CodeConvention, RefMarche from t_ami where NumeroDAMI='" & EnleverApost(CmbDossAMI.Text) & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                TxtMethodeSelect.Text = MettreApost(rw("MethodeSelection").ToString)
                MontantMarche.Text = rw("MontantMarche").ToString.Replace(" ", "")
                CodeMarcheEnCours = rw("RefMarche").ToString
                CodeConvention = rw("CodeConvention").ToString
            Next

            Dim Cpt As Integer = 0

            query = "select c.RefConsult, c.NumeroDp, c.NomConsult, c.PaysConsult, c.TelConsult, c.FaxConsult, c.AdressConsult, c.EmailConsult, s.RefConsult, a.NumeroDAMI from t_ami a, t_consultant c, t_soumissionconsultant s where a.NumeroDAMI=c.NumeroDp and c.RefConsult=s.RefConsult and a.NumeroDAMI='" & EnleverApost(CmbDossAMI.Text) & "' and s.RangConsult is not null and s.EvalTechOk='OUI' order by s.RangConsult ASC LIMIT 6"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)

            GridListeRestreinte.Rows.Clear()

            For Each rw As DataRow In dt0.Rows
                Dim n As Decimal = GridListeRestreinte.Rows.Add()
                ' GridListeRestreinte.Rows.Item(n).Cells(0).Value = rw("RefConsult")
                GridListeRestreinte.Rows.Item(n).Cells("RefConsult").Value = ""
                Cpt = Cpt + 1
                GridListeRestreinte.Rows.Item(n).Cells(1).Value = Cpt
                GridListeRestreinte.Rows.Item(n).Cells(2).Value = MettreApost(rw("NomConsult").ToString)
                GridListeRestreinte.Rows.Item(n).Cells(3).Value = MettreApost(rw("PaysConsult").ToString)
                GridListeRestreinte.Rows.Item(n).Cells(4).Value = MettreApost(rw("AdressConsult").ToString)
                GridListeRestreinte.Rows.Item(n).Cells(5).Value = MettreApost(rw("EmailConsult").ToString)
                GridListeRestreinte.Rows.Item(n).Cells(6).Value = MettreApost(rw("TelConsult").ToString)
                GridListeRestreinte.Rows.Item(n).Cells(7).Value = MettreApost(rw("FaxConsult").ToString)
                GridListeRestreinte.Rows.Item(n).Cells(8).Value = "ConsultantAMI"
            Next
        End If
    End Sub

    Private Sub DateDepot_DateTimeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateDepot.DateTimeChanged
        HeureDepot.Focus()
    End Sub
    Private Sub DateOuverture_DateTimeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateOuverture.DateTimeChanged
        HeureOuverture.Focus()
    End Sub

    Private Sub TxtNomConsult_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtNomConsult.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            CmbPays.Focus()
        End If
    End Sub

    Private Sub CmbPays_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbPays.SelectedValueChanged
        If (CmbPays.Text <> "") Then
            TxtAdresse.Focus()
        End If
    End Sub

    Private Sub TxtAdresse_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtAdresse.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            TxtTel.Focus()
        End If
    End Sub

    Private Sub AjoutManuelConult(ByVal value As Boolean)
        BtEnrgConsult.Enabled = value
        BtSuppConsult.Enabled = value
    End Sub

    Private Sub BtEnrgConsult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgConsult.Click
        If (TxtNumDp.Text.Trim <> "") Then
            If TxtNomConsult.IsRequiredControl("Veuillez saisir un consultant") Then
                TxtNomConsult.Focus()
                Exit Sub
            End If
            If CmbPays.IsRequiredControl("Veuillez selectionner le pays du consultant") Then
                CmbPays.Focus()
                Exit Sub
            End If
            If TxtTel.IsRequiredControl("Veuillez saisir le numéro du consultant") Then
                TxtTel.Focus()
                Exit Sub
            End If
            If TxtMail.IsRequiredControl("Veuillez saisir l'email du consultant") Then
                TxtMail.Focus()
                Exit Sub
            End If

            Dim n As Integer
            Dim Nbre As Integer
            If ModifTous = True And TypeModif = "Consultant" Then
                n = GridListeRestreinte.CurrentRow.Index
            Else
                Nbre = GridListeRestreinte.RowCount
                n = GridListeRestreinte.Rows.Add()

                GridListeRestreinte.Rows.Item(n).Cells(0).Value = ""
                GridListeRestreinte.Rows.Item(n).Cells(1).Value = Nbre + 1
            End If

            GridListeRestreinte.Rows.Item(n).Cells(2).Value = TxtNomConsult.Text
            GridListeRestreinte.Rows.Item(n).Cells(3).Value = CmbPays.Text
            GridListeRestreinte.Rows.Item(n).Cells(4).Value = TxtAdresse.Text
            GridListeRestreinte.Rows.Item(n).Cells(5).Value = TxtMail.Text
            GridListeRestreinte.Rows.Item(n).Cells(6).Value = TxtTel.Text
            GridListeRestreinte.Rows.Item(n).Cells(7).Value = TxtFax.Text
            GridListeRestreinte.Rows.Item(n).Cells(8).Value = IIf(ModifTous = True And TypeModif = "Consultant", "Modifier", "Ajouter").ToString
            RaseSaisieConsult()
            ModifTous = False
            TypeModif = ""
            TxtNomConsult.Focus()
        End If
    End Sub

    Private Sub NewReadOnlyDP(ByVal value As Boolean)
        'donnée de base
        TxtLibDp.Properties.ReadOnly = value
        CmbTypeRemune.Properties.ReadOnly = value
        CmbDossAMI.Properties.ReadOnly = value
        CombMarche.Properties.ReadOnly = value
        ' DateDepot.Properties.ReadOnly = value
        HeureDepot.Properties.ReadOnly = value
        ' DateOuverture.Properties.ReadOnly = value
        ' HeureOuverture.Properties.ReadOnly = value
        NbreDelaiPub.Properties.ReadOnly = value
        JoursDelaiPub.Properties.ReadOnly = value

        '  DatePub1.Properties.ReadOnly = value
        ' DatePub2.Properties.ReadOnly = value
        DateReporte.Properties.ReadOnly = value
        HeureReporte.Properties.ReadOnly = value
        DateEnvoie.Properties.ReadOnly = value
        HeureEnvoi.Properties.ReadOnly = value
        ' NbreJourTravail.Properties.ReadOnly = value
        NumDelai.Properties.ReadOnly = value
        CmbDelai.Properties.ReadOnly = value
        LibellePublication.Properties.ReadOnly = value
        LieuRemiseProposition.Properties.ReadOnly = value

        'Critere evaluation
        NumPoidsTech.Properties.ReadOnly = value
        TxtScoreMinimum.Properties.ReadOnly = value
        ' GridListeRestreinte.ReadOnly = value
        BtEnrgConsult.Enabled = Not value
        BtSuppConsult.Enabled = Not value
        'donnée particuliere
        GbProposition.Enabled = Not value
        GbMission.Enabled = Not value
        GbListeRembours.Enabled = Not value
        GbMeOuvrageDelegue.Enabled = Not value
        GbEltsFournis.Enabled = Not value
        GbCojo.Enabled = Not value
        GroupProposition.Enabled = Not value
        GroupSection.Enabled = Not value
        'Bouton TDR
        BtImportTDR.Enabled = Not value
        BtModifTDR.Enabled = Not value
        'Bouton ajout critères
        BtAjoutCritere.Enabled = Not value
        BtAjoutSousCritere.Enabled = Not value
        BtModifierCritere.Enabled = Not value

        'GroupControl nouveau ajouter
        GCDisposition.Enabled = Not value
        GCPrepaPropo.Enabled = Not value
        GCnegociation.Enabled = Not value
        GCDepotOuverture.Enabled = Not value
    End Sub

    Private Sub ChargerGridConsult(ByVal NumDoss As String)
        Dim Cpt As Decimal = 1
        GridListeRestreinte.Rows.Clear()

        query = "select * from T_Consultant where  NumeroDp='" & NumDoss & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            Dim n As Decimal = GridListeRestreinte.Rows.Add()
            GridListeRestreinte.Rows.Item(n).Cells(0).Value = rw("RefConsult")
            GridListeRestreinte.Rows.Item(n).Cells(1).Value = Cpt
            GridListeRestreinte.Rows.Item(n).Cells(2).Value = MettreApost(rw("NomConsult").ToString)
            GridListeRestreinte.Rows.Item(n).Cells(3).Value = MettreApost(rw("PaysConsult").ToString)
            GridListeRestreinte.Rows.Item(n).Cells(4).Value = rw("AdressConsult").ToString
            GridListeRestreinte.Rows.Item(n).Cells(5).Value = MettreApost(rw("EmailConsult").ToString)
            GridListeRestreinte.Rows.Item(n).Cells(6).Value = rw("TelConsult").ToString
            GridListeRestreinte.Rows.Item(n).Cells(7).Value = rw("FaxConsult").ToString
            GridListeRestreinte.Rows.Item(n).Cells(8).Value = "ConsultantEnregist"
            Cpt = Cpt + 1
        Next
    End Sub

    Private Sub InitialiserDP()
        TxtNumDp.Text = ""
        TxtLibDp.Text = ""
        TxtMethodeSelect.Text = ""
        MontantMarche.Text = ""

        CmbTypeRemune.Text = ""
        CodeMarcheEnCours = ""
        CodeConvention = ""
        CmbDossAMI.Text = ""
        CombMarche.Text = ""
        LieuRemiseProposition.Text = ""
        DateDepot.EditValue = Nothing
        HeureDepot.EditValue = Nothing
        DateOuverture.EditValue = Nothing
        HeureOuverture.EditValue = Nothing
        DateEnvoie.EditValue = Nothing
        HeureEnvoi.EditValue = Nothing
        DateReporte.EditValue = Nothing
        HeureReporte.EditValue = Nothing
        DateDepot.Text = ""
        '  DatePub1.Text = ""
        ' DatePub2.Text = ""
        NbreDelaiPub.Text = ""
        JoursDelaiPub.Text = ""
        'NbreJourTravail.EditValue = Nothing
        NumDelai.Value = 0
        CmbDelai.Text = ""
        LibellePublication.Text = ""

    End Sub

    Private Sub RaseSaisieConsult()
        TxtNomConsult.Text = ""
        CmbPays.Text = ""
        TxtAdresse.Text = ""
        TxtTel.Text = ""
        TxtFax.Text = ""
        TxtMail.Text = ""
        'TxtNomConsult.Focus()
    End Sub

    Private Sub GridListeRestreinte_DoubleClick(sender As Object, e As EventArgs) Handles GridListeRestreinte.DoubleClick
        If GridListeRestreinte.RowCount > 0 Then
            Dim n = GridListeRestreinte.CurrentRow.Index
            ModifTous = True
            TypeModif = "Consultant"

            TxtNomConsult.Text = GridListeRestreinte.Rows.Item(n).Cells("Nom").Value
            CmbPays.Text = GridListeRestreinte.Rows.Item(n).Cells("Pays").Value
            TxtAdresse.Text = GridListeRestreinte.Rows.Item(n).Cells("Adresse").Value
            TxtTel.Text = GridListeRestreinte.Rows.Item(n).Cells("Telephone").Value
            TxtFax.Text = GridListeRestreinte.Rows.Item(n).Cells("Fax").Value
            TxtMail.Text = GridListeRestreinte.Rows.Item(n).Cells("Email").Value

            '  GridListeRestreinte.Rows.Item(n).Cells(8).Value = "ConsultantAMI"
        Else
            SuccesMsg("Veuillez ajouter un consultant")
        End If
    End Sub

    Private Sub BtSuppConsult_Click(sender As Object, e As EventArgs) Handles BtSuppConsult.Click
        If GridListeRestreinte.RowCount > 0 Then

            Dim Index As Integer = GridListeRestreinte.CurrentRow.Index
            If ConfirmMsg("Voulez-vous vraiment supprimer ce consultant ?") = DialogResult.Yes Then
                Dim RefConsuls = GridListeRestreinte.Rows.Item(Index).Cells("RefConsult").Value
                GridListeRestreinte.Rows.RemoveAt(Index)
                For i = 0 To GridListeRestreinte.RowCount - 1
                    GridListeRestreinte.Rows.Item(i).Cells("Numero").Value = i + 1
                Next

                If RefConsuls.ToString <> "" Then
                    query = "delete from t_consultant where RefConsult='" & RefConsuls & "' and NumeroDp='" & NumDoss & "'"
                    ExecuteNonQuery(query)
                End If
            End If
        End If
    End Sub

    Private Sub PDFToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PDFToolStripMenuItem.Click
        If LayoutView1.RecordCount > 0 Then
            Try
                ' If (PourAjout = False And PourModif = False And DejaDansLaBD = False) Then
                dr = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
                NumDoss = dr("N°").ToString
                Dim NomRepCheminSauve As String = line & "\DP\" & FormatFileName(NumDoss.ToString, "_") & "\ElaborationDp.pdf"
                If File.Exists(NomRepCheminSauve) = True Then
                    Process.Start(NomRepCheminSauve)
                Else
                    SuccesMsg("Le fichier spécifié n'existe pas ou a été supprimer")
                End If
                'end if
            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub FormatWordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FormatWordToolStripMenuItem.Click
        If LayoutView1.RecordCount > 0 Then
            Try
                ' If (PourAjout = False And PourModif = False And DejaDansLaBD = False) Then
                dr = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
                NumDoss = dr("N°").ToString
                Dim NomRepCheminSauve As String = line & "\DP\" & FormatFileName(NumDoss.ToString, "_") & "\ElaborationDp.doc"
                If File.Exists(NomRepCheminSauve) = True Then
                    Process.Start(NomRepCheminSauve)
                Else
                    SuccesMsg("Le fichier spécifié n'existe pas ou a été supprimer")
                End If
                'end if
            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub AnnulerLaDPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnnulerLaDPToolStripMenuItem.Click
        If LayoutView1.RecordCount > 0 Then
            Try
                dr = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
                If dr("Statut").ToString = "Annuler" Then
                    FailMsg("Ce marché a été annuler")
                    Exit Sub
                End If

                'Verifier si le marche a ete engager
                If dr("Statut").ToString = "Terminer" Or dr("Statut").ToString = "Engager" Then
                    FailMsg("Le marché a été engager donc impossible de l'annuler")
                    Exit Sub
                End If

                'Verifier si le marche a ete engager
                'query = "select count(*) from t_marchesigne as t, t_dp as d where d.RefMarche=t.RefMarche and t.NumeroDAO='" & EnleverApost(dr("N°").ToString) & "' and t.CodeProjet='" & ProjetEnCours & "'"
                'If Val(ExecuteScallar(query)) > 0 Then
                '    FailMsg("Le marché a été engager donc impossible de l'annuler")
                '    Exit Sub
                'End If

                If ConfirmMsg("Voulez-vous vraiment annuler ce marché ?") = DialogResult.Yes Then
                    DebutChargement(True, "Annulation du marché en cours...")
                    ExecuteNonQuery("Update t_dp set Statut='Annuler' where NumeroDp='" & EnleverApost(dr("N°").ToString) & "'")
                    FinChargement()
                    SuccesMsg("Marché annuler avec succès")
                    LayoutView1.SetFocusedRowCellValue("Statut", "Annuler")

                    'Fermeture des formulaires
                    FermerForm()
                End If
            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub FermerForm()
        Try
            'Arret du processus et fermetures des formulairs ouverts
            'For Each child As Object In Me.MdiChildren
            For Each child As Object In ClearMdi.MdiChildren
                If (child.Name = "DepotDP") Or (child.Name = "OuverturePropositions") Or (child.Name = "EvaluationConsultants") Then
                    child.Close()
                End If
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Public Function VerifierFormOuvert(ByVal NomForm As String) As Boolean
        Try
            For Each FormOuver In Application.OpenForms
                If FormOuver.Name = NomForm.ToString Then

                    Return True
                End If
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        Return False
    End Function

    Private Sub AuBailleurToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AuBailleurToolStripMenuItem.Click
        If LayoutView1.RecordCount > 0 Then
            Try
                dr = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
                If dr("Statut").ToString = "Annuler" Then
                    FailMsg("Ce marché a été annuler")
                    Exit Sub
                End If

                If dr("Statut").ToString = "Terminer" Then
                    FailMsg("Ce marché a été executer")
                    Exit Sub
                End If

                'Info de l'envoi de l'email
                If ChargerLesDonneEmail_AMI_DP_SERVICEAUTRES(dr("N°").ToString, "DP") = False Then
                    Exit Sub
                End If

                Dim MessageText As String = ""
                If dr("DossValider").ToString = "Valider" Then
                    MessageText = "Le bailleur de fond a déjà valider le dossier" & vbNewLine & "Voulez-vous l'envoye à nouveau ?"
                Else
                    MessageText = "Confirmez-vous l'envoi de la demande" & vbNewLine & "de proposition au bailleur [ " & MettreApost(rwDossDPAMISA.Rows(0)("InitialeBailleur").ToString) & " ] ?"
                End If

                If ConfirmMsg(MessageText) = DialogResult.Yes Then
                    Dim CheminFile As String = line & "\DP\" & FormatFileName(dr("N°").ToString, "_") & "\ElaborationDp.doc"

                    Try
                        If File.Exists(CheminFile) = True Then
                            DebutChargement(True, "Envoi de la demande de proposition au bailleur...")
                            'Envoi dla DP au bailleur 
                            EnvoiMailRapport(NomBailleurRetenu, dr("N°").ToString, EmailDestinatauer, CheminFile, EmailCoordinateurProjet, EmailResponsablePM, "DossierDP")
                            FinChargement()
                            SuccesMsg("Dossier envoyé avec succès")
                        Else
                            FailMsg("Le dossier a envoyer n'existe pas ou a été supprimer")
                        End If
                    Catch ep As IO.IOException
                        SuccesMsg("Le fichier est utilisé par une autre application" & vbNewLine & "Veuillez le fermer svp.")
                        FinChargement()
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

    Private Sub AuxConsultantsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AuxConsultantsToolStripMenuItem.Click
        If LayoutView1.RecordCount > 0 Then
            Try
                dr = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
                If dr("Statut").ToString = "Annuler" Then
                    FailMsg("Ce marché a été annuler")
                    Exit Sub
                End If
                If dr("Statut").ToString = "Terminer" Then
                    FailMsg("Ce marché a été executer")
                    Exit Sub
                End If

                'verifer si le bailleur de fonds a valider en tenant compte de la revu
                Dim Revu As String = ExecuteScallar("select RevuePrioPost from t_marche as m, t_dp as d where d.RefMarche=m.RefMarche and d.NumeroDp='" & EnleverApost(dr("N°").ToString) & "'")
                If Revu.ToString = "Priori" And dr("DossValider").ToString <> "Valider" Then
                    FailMsg("Le bailleur de fonds doit valider le dossier avant d'envoyé aux consultants")
                    Exit Sub
                End If

                If Revu.ToString <> "Priori" And dr("DossValider").ToString <> "Valider" Then
                    FailMsg("Vous devez valider le dossier avant d'envoyé aux consultants")
                    Exit Sub
                End If

                Dim rWConsult As DataTable = ExcecuteSelectQuery("select NomConsult, EmailConsult from t_consultant where NumeroDp='" & EnleverApost(dr("N°").ToString) & "'")
                If rWConsult.Rows.Count = 0 Then
                    FailMsg("Veuillez ajouter un consultant pour ce dossier")
                    Exit Sub
                End If

                'Envoi du dossier au consultants
                If ConfirmMsg("Confirmez-vous l'envoi de la demande de proposition aux consultants ?") = DialogResult.Yes Then
                    Try
                        Dim CheminFile As String = line & "\DP\" & FormatFileName(dr("N°").ToString, "_") & "\ElaborationDp.doc"
                        If File.Exists(CheminFile) = True Then

                            DebutChargement(True, "Envoie de la DP aux consultants...")
                            For Each rw In rWConsult.Rows
                                EnvoiMailRapport(MettreApost(rw("NomConsult").ToString), dr("N°").ToString, MettreApost(rw("EmailConsult").ToString), CheminFile, "", "", "ConsultantsDP")
                            Next
                            FinChargement()
                            SuccesMsg("Dossier envoyé avec succès")
                        Else
                            FailMsg("Le dossier a envoyer n'existe pas ou a été supprimer")
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
#End Region

#Region "Données Particulières"

    Private Sub LoadPageDonnePartic(ByVal NumDoss As String)
        If PourModif = True Then

            Try
                query = "select LibelleMiss,TypeRemune,TypAssociation,DelaiEclaircissement,DebutMiss,DureeMiss,RessPersonnel,FormationIntrinsq,ImpotRembourse,PropoTech,PropoFin,PoidsTech,PoidsFin,ScoreTechMin,LangueDp,MonnaieEval,ValiditePropo,ModalitePropo,DateLimitePropo,AssoListeRest,MeOuvrageDelegue,DateOuverture from T_DP where NumeroDp='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt0 = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows

                    If (rw("LangueDp").ToString <> "") Then CmbLangue.Text = rw("LangueDp").ToString
                    If (rw("MonnaieEval").ToString <> "") Then CmbDevise.Text = rw("MonnaieEval").ToString
                    If (rw("ValiditePropo").ToString <> "") Then
                        Dim partValid() As String = rw("ValiditePropo").ToString.Split(" "c)
                        NumValidite.Value = CInt(partValid(0))
                        CmbValidite.Text = partValid(1)
                    End If

                    If (rw("ModalitePropo").ToString <> "") Then CmbModalite.Text = rw("ModalitePropo").ToString
                    If (rw("AssoListeRest").ToString <> "") Then
                        If (rw("AssoListeRest").ToString = "OUI") Then CmbAssociation.Text = "OUI (groupement)"
                        If (rw("AssoListeRest").ToString = "NON") Then CmbAssociation.Text = "NON (individuel)"
                    End If
                    TypAssociation.Text = MettreApost(rw("TypAssociation").ToString)

                    'If rw("ConfPrea").ToString <> "" Then
                    '    ChkConference.Checked = True
                    '    Dim partConf() As String = rw("ConfPrea").ToString.Split(" "c)
                    '    DateConference.DateTime = CDate(partConf(0)).ToShortDateString
                    '    HeureConference.Time = CDate(partConf(1)).ToLongTimeString
                    'Else
                    '    ChkConference.Checked = False
                    'End If

                    If rw("DelaiEclaircissement").ToString <> "" Then
                        Dim partEclair() As String = rw("DelaiEclaircissement").ToString.Split(" ")
                        NumEclaircissement.Value = CInt(partEclair(0))
                        CmbEclaircissement.Text = partEclair(1).ToString
                    Else
                        NumEclaircissement.Value = 0
                        CmbEclaircissement.Text = ""
                    End If

                    ' If (rw("PropoTech").ToString <> "0") Then
                    If Val(rw("PropoTech").ToString) > 0 Then
                        ChkPropoTechnique.Checked = True
                        NumNbreCopieTechnq.Value = CInt(rw("PropoTech"))
                    End If

                    ' If (rw("PropoFin").ToString <> "0") Then
                    If Val(rw("PropoFin").ToString) > 0 Then
                        ChkPropoFinanciere.Checked = True
                        NumNbreCopieFinance.Value = CInt(rw("PropoFin"))
                    End If

                    If (rw("DureeMiss").ToString <> "") Then
                        Dim partDuree() As String = rw("DureeMiss").ToString.Split(" "c)
                        NumDureeMission.Value = CInt(partDuree(0))
                        CmbDureeMission.Text = partDuree(1)
                    End If

                    If (rw("RessPersonnel").ToString <> "") Then
                        Dim partPers() As String = rw("RessPersonnel").ToString.Split(" "c)
                        NumNbrePersonnel.Value = CInt(partPers(0))
                        CmbFreqPersonnel.Text = partPers(1)
                    End If

                    If (rw("DebutMiss").ToString <> "") Then
                        Dim partDeb() As String = rw("DebutMiss").ToString.Split(" "c)
                        NumDebutMission.Value = CInt(partDeb(0))
                        CmbDebutMission.Text = partDeb(1).ToString
                    End If

                    If (rw("FormationIntrinsq").ToString = "OUI") Then
                        ChkFormationIntrinseque.Checked = True
                    End If
                    If (rw("ImpotRembourse").ToString = "OUI") Then
                        ChkImpotRedevable.Checked = True
                    End If
                    If (rw("MeOuvrageDelegue").ToString <> "") Then
                        ChkOuvrageDelegueOui.Checked = True
                        TxtMaitreOuvrageDelegue.Text = MettreApost(rw("MeOuvrageDelegue").ToString)
                    End If

                    ' If rw("PoidsTech").ToString <> "" Then NumPoidsTech.Value = CDec(rw("PoidsTech"))
                    ' If rw("PoidsFin").ToString <> "" Then NumPoidsTech.Value = CDec(rw("PoidsFin"))
                    ' If rw("ScoreTechMin").ToString <> "" Then TxtScoreMinimum.Text = rw("ScoreTechMin").ToString

                Next

                UpdatePlusDonneeParticulier(NumDoss, "Load")
                MajGridRembours(NumDoss)
                MajGridCojo(NumDoss)
                MajGridEltsFournis(NumDoss)
                ChargerLesSection(NumDoss)

            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub

    Private Function SavePageDonnePartic(ByVal NumDoss As String) As Boolean
        'update dans la table DP
        'Vérification des champs
        If CmbAssociation.Text = "OUI  (groupement)" And TypAssociation.Text = "" Then
            SuccesMsg("Veuillez selectionnez le type d'association")
            TypAssociation.Select()
            Return False
        End If
        If CmbLangue.IsRequiredControl("Veuillez choisir une langue dans la liste.") Then
            CmbLangue.Focus()
            Return False
        End If
        If CmbDevise.IsRequiredControl("Veuillez choisir une devise dans la liste.") Then
            CmbDevise.Focus()
            Return False
        End If

        If NumValidite.Value <> 0 Then
            If CmbValidite.IsRequiredControl("Veuillez bien définir la validité.") Then
                NumValidite.Focus()
                Return False
            End If
        ElseIf Val(NumValidite.Value) <= 0 Then
            SuccesMsg("Veuillez entrer la validité")
            NumValidite.Focus()
            Return False
        End If

        'If (DateConference.DateTime.ToShortDateString = CDate("01/01/2000").ToShortDateString And HeureConference.Time.ToLongTimeString = CDate("00:00:00").ToLongTimeString And ChkConference.Checked = True) Then
        '    SuccesMsg("Veuillez bien définir la date et l'heure de la conference.")
        '    DateConference.Focus()
        '    Return False
        'End If

        If NumEclaircissement.Value.ToString <> "0" And CmbEclaircissement.Text.Trim = "" Then
            SuccesMsg("Veuillez bien définir l'éclaircissement.")
            CmbEclaircissement.Focus()
            Return False
        End If

        If ChkPropoTechnique.Checked = True And Val(NumNbreCopieTechnq.Value) <= 0 Then
            SuccesMsg("Veuillez entrer le nombre de copies.")
            NumNbreCopieTechnq.Focus()
            Return False
        End If

        If ChkPropoFinanciere.Checked = True And Val(NumNbreCopieFinance.Value) <= 0 Then
            SuccesMsg("Veuillez entrer le nombre de copies.")
            NumNbreCopieFinance.Focus()
            Return False
        End If

        If NumDureeMission.Value.ToString <> "0" And CmbDureeMission.Text.Trim = "" Then
            SuccesMsg("Veuillez définir la durée de la mission.")
            CmbDureeMission.Focus()
            Return False
        End If

        If NumDebutMission.Value.ToString <> "0" And CmbDebutMission.Text.Trim = "" Then
            SuccesMsg("Veuillez définir la durée de la mission.")
            CmbDebutMission.Focus()
            Return False
        End If

        If ChkOuvrageDelegueOui.Checked = True Then
            If (TxtMaitreOuvrageDelegue.IsRequiredControl("Veuillez saisir le maître d'ouvrage délégué")) Then
                TxtMaitreOuvrageDelegue.Focus()
                Return False
            End If
        End If

        'proposition financière cocher et propo tech non cocher
        If ChkPropoFinanciere.Checked = True And ChkPropoTechnique.Checked = False Then
            SuccesMsg("Veuillez cocher la proposition technique.")
            ChkPropoTechnique.Select()
            Return False
        End If

        'New Info **********************************************
        If OuverturePropTechLine.Checked = True And ProcedurProTechLine.Text = "" Then
            SuccesMsg("Veuillez saisir la procédure de l'ouverture de la proposition technique en ligne.")
            ProcedurProTechLine.Select()
            Return False
        End If

        If OuvrProFinLine.Checked = True And ProcedurOvrPropoFinLine.Text = "" Then
            SuccesMsg("Veuillez saisir la procédure de l'ouverture de la proposition financière en ligne.")
            ProcedurProTechLine.Select()
            Return False
        End If

        If Impot.Checked = True And MontantImpot.Text = "" Then
            SuccesMsg("Veuillez saisir le montant de l'impôt.")
            MontantImpot.Select()
            Return False
        End If

        If ChkConference.Checked = True Then
            If DateConference.IsRequiredControl("Veuillez saisir la date de la conférence") Then
                DateConference.Select()
                Return False
            End If
            If HeureConference.IsRequiredControl("Veuillez saisir l'heure de la conférence") Then
                HeureConference.Select()
                Return False
            End If
            If AdresseConfere.IsRequiredControl("Veuillez saisir l'adresse de la conférence") Then
                AdresseConfere.Select()
                Return False
            End If
            If NomConferenc.IsRequiredControl("Veuillez saisir le nom du coordonateur de la conférence") Then
                NomConferenc.Select()
                Return False
            End If
            If TitreConference.IsRequiredControl("Veuillez saisir le titre du coordonateur de la conférence") Then
                TitreConference.Select()
                Return False
            End If
        End If

        ' DebutChargement(True, "Enregistrement des données particulières en cours...")

        Dim FormationIntrinsq = IIf(ChkFormationIntrinseque.Checked = True, "OUI", "NON").ToString
        Dim ImpotRembourse = IIf(ChkImpotRedevable.Checked = True, "OUI", "NON").ToString
        Dim MeOuvrageDelegue = IIf(ChkOuvrageDelegueNon.Checked = True, "", EnleverApost(TxtMaitreOuvrageDelegue.Text)).ToString
        'Dim DatConferenc As String = IIf(ChkConference.Checked = True, dateconvert(DateConference.DateTime.ToShortDateString) & " " & HeureConference.Time.ToLongTimeString, "").ToString

        query = "UPDATE T_DP set LangueDp='" & EnleverApost(CmbLangue.Text) & "', MonnaieEval='" & CmbDevise.Text & "', ValiditePropo='" & NumValidite.Value.ToString & " " & CmbValidite.Text & "', "
        query &= "ModalitePropo='" & EnleverApost(CmbModalite.Text) & "', AssoListeRest='" & EnleverApost(Mid(CmbAssociation.Text, 1, 3)) & "', TypAssociation='" & EnleverApost(TypAssociation.Text) & "',"
        query &= "DelaiEclaircissement='" & NumEclaircissement.Value.ToString & " " & CmbEclaircissement.Text & "', PropoTech='" & CInt(NumNbreCopieTechnq.Value) & "', PropoFin ='" & CInt(NumNbreCopieFinance.Value) & "', DureeMiss = '" & NumDureeMission.Value.ToString & " " & CmbDureeMission.Text & "',"
        query &= "RessPersonnel = '" & NumNbrePersonnel.Value.ToString & " " & CmbFreqPersonnel.Text & "', DebutMiss = '" & NumDebutMission.Value.ToString & " " & CmbDebutMission.Text & "', FormationIntrinsq='" & FormationIntrinsq & "', ImpotRembourse='" & ImpotRembourse & "',"
        query &= "MeOuvrageDelegue='" & EnleverApost(MeOuvrageDelegue) & "' where NumeroDp='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"

        ExecuteNonQuery(query)

        UpdatePlusDonneeParticulier(NumDoss)
        Dim Actuliser As Boolean = False

        For i = 0 To GridDepenseRembourse.RowCount - 1
            'nouvelle ligne ajouter l'ors de la modification
            If GridDepenseRembourse.Rows.Item(i).Cells("Num1").Value.ToString = "" Then
                query = "insert into T_DP_ListeRembours values(NULL,'" & NumDoss & "','" & EnleverApost(GridDepenseRembourse.Rows.Item(i).Cells("Objet1").Value) & "')"
                ExecuteNonQuery(query)
                Actuliser = True
                'ligne modifier
            ElseIf GridDepenseRembourse.Rows.Item(i).Cells("Action1").Value.ToString = "Modifier" Then
                query = "update T_DP_ListeRembours set Description='" & EnleverApost(GridDepenseRembourse.Rows.Item(i).Cells("Objet1").Value) & "' where RefListe='" & GridDepenseRembourse.Rows.Item(i).Cells("Num1").Value & "' and NumeroDp='" & NumDoss & "'"
                ExecuteNonQuery(query)
            End If
        Next

        If Actuliser = True Then
            MajGridRembours(NumDoss)
        End If

        Actuliser = False

        For i = 0 To GridEltsFournis.RowCount - 1
            'nouvelle ligne ajouter l'ors de la modification
            If GridEltsFournis.Rows.Item(i).Cells("Num2").Value.ToString = "" Then
                query = "insert into T_DP_ListeEltsFournis values(NULL, '" & NumDoss & "', '" & EnleverApost(GridEltsFournis.Rows.Item(i).Cells("Objet2").Value) & "')"
                ExecuteNonQuery(query)
                Actuliser = True
                'ligne modifier
            ElseIf GridEltsFournis.Rows.Item(i).Cells("Action2").Value.ToString = "Modifier" Then
                query = "update T_DP_ListeEltsFournis set Description='" & EnleverApost(GridEltsFournis.Rows.Item(i).Cells("Objet2").Value) & "' where RefListe='" & GridEltsFournis.Rows.Item(i).Cells("Num2").Value & "' and NumeroDp='" & NumDoss & "'"
                ExecuteNonQuery(query)
            End If
        Next

        If Actuliser = True Then
            MajGridEltsFournis(NumDoss)
        End If
        Actuliser = False

        For k = 0 To GridCojo.RowCount - 1
            'nouveau cojo ajouter
            If GridCojo.Rows.Item(k).Cells("Refcojo").Value.ToString = "" Then

                query = "insert into T_Commission values(NULL,'" & EnleverApost(GridCojo.Rows.Item(k).Cells("Nomcojo").Value) & "', '" & GridCojo.Rows.Item(k).Cells("Telephonecojo").Value & "', '" & EnleverApost(GridCojo.Rows.Item(k).Cells("Emailcojo").Value) & "', '" & EnleverApost(GridCojo.Rows.Item(k).Cells("Organismecojo").Value) & "', '',  '" & EnleverApost(GridCojo.Rows.Item(k).Cells("Fonctioncojo").Value) & "', '" & NumDoss & "', '" & GridCojo.Rows.Item(k).Cells("Typecojo").Value & "', '" & GridCojo.Rows.Item(k).Cells("Civilitecojo").Value & "', '" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', '" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', '" & CodeUtilisateur & "', '', '','', '', '','')"
                ExecuteNonQuery(query)

                Actuliser = True
                'cojo modifier
            ElseIf (GridCojo.Rows.Item(k).Cells("Actioncojo").Value.ToString = "Modifier") Then
                query = "update T_Commission set NomMem='" & EnleverApost(GridCojo.Rows.Item(k).Cells("Nomcojo").Value) & "', TelMem='" & GridCojo.Rows.Item(k).Cells("Telephonecojo").Value & "', EmailMem='" & EnleverApost(GridCojo.Rows.Item(k).Cells("Emailcojo").Value) & "', FoncMem='" & EnleverApost(GridCojo.Rows.Item(k).Cells("Organismecojo").Value) & "', TitreMem='" & EnleverApost(GridCojo.Rows.Item(k).Cells("Fonctioncojo").Value) & "', TypeComm='" & GridCojo.Rows.Item(k).Cells("Typecojo").Value & "', Civil='" & GridCojo.Rows.Item(k).Cells("Civilitecojo").Value & "', DateModif='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', Operateur='" & CodeUtilisateur & "' where CodeMem='" & GridCojo.Rows.Item(k).Cells("Refcojo").Value & "' and NumeroDAO='" & NumDoss & "'"
                ExecuteNonQuery(query)
            End If
        Next

        If Actuliser = True Then
            MajGridCojo(NumDoss)
        End If

        Actuliser = False
        For k = 0 To GridSection.RowCount - 1
            'nouveau cojo ajouter
            If GridSection.Rows.Item(k).Cells("RefSection").Value.ToString = "" Then

                query = "insert into T_DP_Section values(NULL,'" & NumDoss & "', '" & GridSection.Rows.Item(k).Cells("CodeSection").Value & "', '" & EnleverApost(GridSection.Rows.Item(k).Cells("Description").Value) & "', '" & ProjetEnCours & "')"
                ExecuteNonQuery(query)

                Actuliser = True
                'cojo modifier
            ElseIf (GridSection.Rows.Item(k).Cells("LigneModif").Value.ToString = "Modifier") Then
                query = "update T_DP_Section set Description='" & EnleverApost(GridSection.Rows.Item(k).Cells("Description").Value) & "' where RefSection='" & GridSection.Rows.Item(k).Cells("RefSection").Value & "'"
                ExecuteNonQuery(query)
            End If
        Next

        If Actuliser = True Then
            ChargerLesSection(NumDoss)
        End If
        Return True
    End Function

    Private Sub ChkOuvrageDelegueNon_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkOuvrageDelegueNon.CheckedChanged
        If ChkOuvrageDelegueNon.Checked = True Then
            TxtMaitreOuvrageDelegue.Text = ""
            TxtMaitreOuvrageDelegue.Enabled = False
        ElseIf (ChkOuvrageDelegueNon.Checked = False) Then
            TxtMaitreOuvrageDelegue.Enabled = True
        End If
    End Sub

    Private Sub CmbDevise_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbDevise.SelectedValueChanged
        If (CmbDevise.Text <> "") Then
            query = "select LibelleDevise from T_Devise where AbregeDevise='" & CmbDevise.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtDevise.Text = rw("LibelleDevise").ToString
            Next
        End If
    End Sub

    Private Sub ChkPropoTechnique_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkPropoTechnique.CheckedChanged
        If (ChkPropoTechnique.Checked = True) Then
            NumNbreCopieTechnq.Enabled = True
        Else
            NumNbreCopieTechnq.Enabled = False
            NumNbreCopieTechnq.Value = 0
        End If
    End Sub

    Private Sub ChkPropoFinanciere_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkPropoFinanciere.CheckedChanged
        If (ChkPropoFinanciere.Checked = True) Then
            NumNbreCopieFinance.Enabled = True
        Else
            NumNbreCopieFinance.Enabled = False
            NumNbreCopieFinance.Value = 0
        End If
    End Sub

    Private Sub NumDureeMission_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumDureeMission.ValueChanged
        If (NumDureeMission.Value <> 0 And CmbDureeMission.Text <> "") Then
            NumNbrePersonnel.Enabled = True
            CmbFreqPersonnel.Enabled = True
        Else
            NumNbrePersonnel.Enabled = False
            CmbFreqPersonnel.Enabled = False
        End If
    End Sub

    Private Sub CmbDureeMission_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbDureeMission.SelectedValueChanged
        If (NumDureeMission.Value <> 0 And CmbDureeMission.Text <> "") Then
            NumNbrePersonnel.Enabled = True
            CmbFreqPersonnel.Enabled = True
        Else
            NumNbrePersonnel.Enabled = False
            CmbFreqPersonnel.Enabled = False
        End If
    End Sub

    Private Sub TxtMaitreOuvrageDelegue_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtMaitreOuvrageDelegue.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            If (NumDoss <> "") Then
                TxtEltsFournis.Focus()
            End If
        End If
    End Sub
    Private Sub TxtDepenseRembourse_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtDepenseRembourse.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            If (TxtDepenseRembourse.Text.Trim <> "") Then
                Dim n As Decimal

                If ModifTous = True And TypeModif = "Depense" Then
                    n = GridDepenseRembourse.CurrentRow.Index
                    GridDepenseRembourse.Rows.Item(n).Cells("Action1").Value = "Modifier"
                Else
                    n = GridDepenseRembourse.Rows.Add()
                    GridDepenseRembourse.Rows.Item(n).Cells("Num1").Value = ""
                    GridDepenseRembourse.Rows.Item(n).Cells("Action1").Value = "Ajouter"
                End If
                GridDepenseRembourse.Rows.Item(n).Cells("Objet1").Value = TxtDepenseRembourse.Text
                TxtDepenseRembourse.Text = ""
                ModifTous = False
                TypeModif = ""
                TxtDepenseRembourse.Focus()
            End If
        End If
    End Sub

    Private Sub MajGridRembours(ByVal NumDoss As String)
        GridDepenseRembourse.Rows.Clear()
        query = "select RefListe,Description from T_DP_ListeRembours where NumeroDp='" & NumDoss & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            Dim n As Decimal = GridDepenseRembourse.Rows.Add()
            GridDepenseRembourse.Rows.Item(n).Cells("Num1").Value = rw("RefListe").ToString
            GridDepenseRembourse.Rows.Item(n).Cells("Objet1").Value = MettreApost(rw("Description").ToString)
            GridDepenseRembourse.Rows.Item(n).Cells("Action1").Value = "Enregistrer"
        Next
    End Sub
    Private Sub TxtEltsFournis_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtEltsFournis.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            If (TxtEltsFournis.Text <> "") Then
                Dim n As Decimal
                If ModifTous = True And TypeModif = "Fournisseur" Then
                    n = GridEltsFournis.CurrentRow.Index
                    GridEltsFournis.Rows.Item(n).Cells("Action2").Value = "Modifier"
                Else
                    n = GridEltsFournis.Rows.Add()
                    GridEltsFournis.Rows.Item(n).Cells("Num2").Value = ""
                    GridEltsFournis.Rows.Item(n).Cells("Action2").Value = "Ajouter"
                End If
                GridEltsFournis.Rows.Item(n).Cells("Objet2").Value = TxtEltsFournis.Text
                TxtEltsFournis.Text = ""
                TxtEltsFournis.Focus()
            End If
        End If
    End Sub

    Private Sub MajGridEltsFournis(ByVal NumDoss As String)
        GridEltsFournis.Rows.Clear()
        query = "Select RefListe, Description from T_DP_ListeEltsFournis where NumeroDp='" & NumDoss & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            Dim n As Decimal = GridEltsFournis.Rows.Add()
            GridEltsFournis.Rows.Item(n).Cells("Num2").Value = rw("RefListe").ToString
            GridEltsFournis.Rows.Item(n).Cells("Objet2").Value = MettreApost(rw("Description").ToString)
            GridEltsFournis.Rows.Item(n).Cells("Action2").Value = "Enregistrer"
        Next
    End Sub

    Private Sub UpdatePlusDonneeParticulier(ByVal NumeroDps As String, Optional TypeRequette As String = "")
        Try
            If TypeRequette = "" Then
                query = "UPDATE t_dp_donneparticuliere set CheckRemisePropo='" & IIf(CheckRemisePropo.Checked = True, "OUI", "NON").ToString & "', DateReglement='" & DateReglement.Text & "', ChkConference='" & IIf(ChkConference.Checked = True, "OUI", "NON").ToString & "', ConsulRetenu='" & IIf(ConsulRetenu.Checked = True, "OUI", "NON").ToString & "',"
                query &= "DateConference='" & DateConference.Text & " " & HeureConference.Text & "', AdresseConfere='" & EnleverApost(AdresseConfere.Text) & "', NomConferenc='" & EnleverApost(NomConferenc.Text) & "', TitreConference='" & EnleverApost(TitreConference.Text) & "', Inflation='" & IIf(Inflation.Checked = True, "OUI", "NON").ToString() & "',"
                query &= "TelConferen='" & EnleverApost(TelConferen.Text) & "', CourrielConferen='" & EnleverApost(CourrielConferen.Text) & "', MontantImpot ='" & IIf(Impot.Checked = True, EnleverApost(MontantImpot.Text), "").ToString & "', RespectLoi='" & IIf(RespectLoi.Checked = True, "OUI", "NON").ToString() & "', Soustraitant='" & IIf(Soustraitant.Checked = True, "OUI", "NON").ToString() & "', RevisionPrix = '" & IIf(RevisionPrix.Checked = True, "OUI", "NON").ToString() & "',"
                query &= "ProcedurProTechLine = '" & EnleverApost(ProcedurProTechLine.Text) & "', ProcedurOvrPropoFinLine='" & EnleverApost(ProcedurOvrPropoFinLine.Text) & "', AdresslieuOuvr='" & EnleverApost(AdresslieuOuvr.Text) & "', VillelieuOuvr='" & EnleverApost(VillelieuOuvr.Text) & "', BuroOuver='" & EnleverApost(BuroOuver.Text) & "', PaysOuvertur='" & EnleverApost(PaysOuvertur.Text) & "',"
                query &= "DateNego = '" & EnleverApost(DateNego.Text) & "', AdresseNego='" & EnleverApost(AdresseNego.Text) & "', DateService='" & DateService.Text & "', LieuService='" & EnleverApost(LieuService.Text) & "', NomReclama='" & EnleverApost(NomReclama.Text) & "', TitreReclam='" & EnleverApost(TitreReclam.Text) & "', AdresseReclam='" & EnleverApost(AdresseReclam.Text) & "', Agence='" & EnleverApost(Agence.Text) & "',"
                query &= " TelecopociRecla='" & EnleverApost(TelecopociRecla.Text) & "' where NumeroDp='" & NumeroDps & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            Else
                query = "SELECT * FROM t_dp_donneparticuliere WHERE NumeroDp='" & NumeroDps & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw In dt.Rows
                    CheckRemisePropo.Checked = IIf(rw("CheckRemisePropo").ToString = "OUI", True, False).ToString
                    DateReglement.Text = rw("DateReglement").ToString
                    ChkConference.Checked = IIf(rw("ChkConference").ToString = "OUI", True, False).ToString

                    RespectLoi.Checked = IIf(rw("RespectLoi").ToString = "OUI", True, False).ToString
                    Soustraitant.Checked = IIf(rw("Soustraitant").ToString = "OUI", True, False).ToString
                    RevisionPrix.Checked = IIf(rw("RevisionPrix").ToString = "OUI", True, False).ToString
                    Inflation.Checked = IIf(rw("Inflation").ToString = "OUI", True, False).ToString

                    If rw("ChkConference").ToString = "OUI" Then
                        DateConference.Text = CDate(rw("DateConference").ToString).ToShortDateString
                        HeureConference.Text = CDate(rw("DateConference").ToString).ToLongTimeString
                    End If
                    AdresseConfere.Text = MettreApost(rw("AdresseConfere").ToString)
                    NomConferenc.Text = MettreApost(rw("NomConferenc").ToString)
                    TitreConference.Text = MettreApost(rw("TitreConference").ToString)
                    TelConferen.Text = MettreApost(rw("TelConferen").ToString)
                    CourrielConferen.Text = MettreApost(rw("CourrielConferen").ToString)

                    If rw("MontantImpot").ToString <> "" Then Impot.Checked = True
                    MontantImpot.Text = MettreApost(rw("MontantImpot").ToString)

                    If rw("ProcedurProTechLine").ToString <> "" Then OuverturePropTechLine.Checked = True
                    If rw("ProcedurOvrPropoFinLine").ToString <> "" Then OuvrProFinLine.Checked = True
                    ProcedurProTechLine.Text = MettreApost(rw("ProcedurProTechLine").ToString)
                    ProcedurOvrPropoFinLine.Text = MettreApost(rw("ProcedurOvrPropoFinLine").ToString)

                    AdresslieuOuvr.Text = MettreApost(rw("AdresslieuOuvr").ToString)
                    VillelieuOuvr.Text = MettreApost(rw("VillelieuOuvr").ToString)
                    BuroOuver.Text = MettreApost(rw("BuroOuver").ToString)
                    PaysOuvertur.Text = MettreApost(rw("PaysOuvertur").ToString)

                    DateNego.Text = MettreApost(rw("DateNego").ToString)
                    AdresseNego.Text = MettreApost(rw("AdresseNego").ToString)
                    DateService.Text = MettreApost(rw("DateService").ToString)
                    LieuService.Text = MettreApost(rw("LieuService").ToString)
                    NomReclama.Text = MettreApost(rw("NomReclama").ToString)
                    TitreReclam.Text = MettreApost(rw("TitreReclam").ToString)
                    AdresseReclam.Text = MettreApost(rw("AdresseReclam").ToString)
                    Agence.Text = MettreApost(rw("Agence").ToString)
                    TelecopociRecla.Text = MettreApost(rw("TelecopociRecla").ToString)
                    ConsulRetenu.Checked = IIf(rw("ConsulRetenu").ToString = "OUI", True, False).ToString
                Next
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub BtAjoutCojo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjoutCojo.Click
        If CmbCivCojo.Text = "" Then
            SuccesMsg("Veuillez selectionné une civilité")
            CmbCivCojo.Focus()
            Exit Sub
        End If
        If TxtCojo.IsRequiredControl("Veuillez saisir le nom du mêmbre de la commission") Then
            TxtCojo.Focus()
            Exit Sub
        End If
        If CmbTitreCojo.Text = "" Then
            SuccesMsg("Veuillez selectionnée le titre du mêmbre de la commission")
            CmbTitreCojo.Focus()
            Exit Sub
        End If

        If TxtMailCojo.IsRequiredControl("Veuillez saisir l'emeil du mêmbre de la commission") Then
            TxtMailCojo.Focus()
            Exit Sub
        End If

        Dim n As Integer
        If ModifTous = True And TypeModif = "Cojo" Then
            n = GridCojo.CurrentRow.Index
            GridCojo.Rows.Item(n).Cells("Actioncojo").Value = "Modifier"
        Else
            n = GridCojo.Rows.Add()
            GridCojo.Rows.Item(n).Cells("Refcojo").Value = ""
            GridCojo.Rows.Item(n).Cells("Actioncojo").Value = "Ajouter"
        End If

        GridCojo.Rows.Item(n).Cells("Nomcojo").Value = TxtCojo.Text
        GridCojo.Rows.Item(n).Cells("Fonctioncojo").Value = CmbTitreCojo.Text
        GridCojo.Rows.Item(n).Cells("Telephonecojo").Value = TxtContactCojo.Text
        GridCojo.Rows.Item(n).Cells("Emailcojo").Value = TxtMailCojo.Text
        GridCojo.Rows.Item(n).Cells("Organismecojo").Value = TxtFonctionCojo.Text
        GridCojo.Rows.Item(n).Cells("Typecojo").Value = IIf(ChkEvaluateur.Checked = True, "EVAC", "COJO").ToString
        GridCojo.Rows.Item(n).Cells("Civilitecojo").Value = CmbCivCojo.Text
        InitCojo()
        ModifTous = False
        TypeModif = ""
    End Sub

    Private Sub BtAjoutSection_Click(sender As Object, e As EventArgs) Handles BtAjoutSection.Click

        If TxtSection.IsRequiredControl("Veuillez saisie la description de la section") Then
            TxtSection.Focus()
            Exit Sub
        End If

        Dim n As Integer
        If ModifTous = True And TypeModif = "Section" Then
            n = GridSection.CurrentRow.Index
            GridSection.Rows.Item(n).Cells("LigneModif").Value = "Modifier"
        Else
            If CombSection.SelectedIndex = -1 Then
                SuccesMsg("Veuillez selectionné la section à saisir")
                CombSection.Focus()
                Exit Sub
            End If

            n = GridSection.Rows.Add()
            GridSection.Rows.Item(n).Cells("RefSection").Value = ""
            GridSection.Rows.Item(n).Cells("LigneModif").Value = "Ajouter"
        End If

        GridSection.Rows.Item(n).Cells("Description").Value = TxtSection.Text
        GridSection.Rows.Item(n).Cells("CodeSection").Value = CombSection.Text.Split(" ")(1).ToString

        If ModifTous = True And TypeModif = "Section" Then
            CombSection.Enabled = True
            CombSection.Text = ""
        End If

        ' GridSection.Rows.Item(n).Cells("Code section").Value = CombSection.Text
        TxtSection.Text = ""
        ModifTous = False
        TypeModif = ""
    End Sub

    Private Sub CombSection_TextChanged(sender As Object, e As EventArgs) Handles CombSection.TextChanged
        If CombSection.Text.Trim <> "" Then
            TxtSection.Properties.ReadOnly = False
        Else
            TxtSection.Properties.ReadOnly = True
        End If
    End Sub
    Private Sub CombSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CombSection.SelectedIndexChanged
        If CombSection.SelectedIndex <> -1 And CombSection.Text = "Section 3.3" Then
            GroupBoxSection.Text = "Description du personnel"
        Else
            GroupBoxSection.Text = "Saisie la description de la section"
        End If
    End Sub


    Private Sub GridSection_DoubleClick(sender As Object, e As EventArgs) Handles GridSection.DoubleClick
        If GridSection.RowCount > 0 Then
            Dim n = GridSection.CurrentRow.Index
            ModifTous = True
            TypeModif = "Section"

            TxtSection.Text = GridSection.Rows.Item(n).Cells("Description").Value
            CombSection.Text = "Section " & GridSection.Rows.Item(n).Cells("CodeSection").Value
            CombSection.Enabled = False
        End If
    End Sub

    Private Sub ChargerLesSection(ByVal NumDoss As String)
        CombSection.Text = ""
        CombSection.Enabled = True
        TxtSection.Text = ""

        GridSection.Rows.Clear()
        query = "Select RefSection, CodeSection, Description from T_DP_Section where NumeroDp='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            Dim n As Decimal = GridSection.Rows.Add()
            GridSection.Rows.Item(n).Cells("RefSection").Value = rw("RefSection").ToString
            GridSection.Rows.Item(n).Cells("Description").Value = MettreApost(rw("Description").ToString)
            GridSection.Rows.Item(n).Cells("CodeSection").Value = MettreApost(rw("CodeSection").ToString)
            GridSection.Rows.Item(n).Cells("LigneModif").Value = "Enregistrer"
        Next
    End Sub

    Private Sub VerifRapporteur()
        If (TxtNumDp.Text <> "") Then
            Dim RapporteurExist As Boolean = False
            query = "select * from T_Commission where NumeroDAO='" & TxtNumDp.Text & "' and TitreMem='Rapporteur'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            If dt0.Rows.Count > 0 Then
                RapporteurExist = True
            Else
                RapporteurExist = False
            End If

            If (RapporteurExist = False) Then
                LblControl.Text = "* Rapporteur pas encore enregistré !"
            Else
                LblControl.Text = ""
            End If
        End If
    End Sub

    Private Sub MajGridCojo(ByVal NumDoss As String)
        GridCojo.Rows.Clear()
        query = "select * from T_Commission where NumeroDAO='" & NumDoss & "' order by CodeMem"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            Dim n As Decimal = GridCojo.Rows.Add()
            GridCojo.Rows.Item(n).Cells("Refcojo").Value = rw("CodeMem")
            GridCojo.Rows.Item(n).Cells("Nomcojo").Value = MettreApost(rw("NomMem").ToString)
            GridCojo.Rows.Item(n).Cells("Civilitecojo").Value = rw("Civil").ToString
            GridCojo.Rows.Item(n).Cells("Emailcojo").Value = rw("EmailMem").ToString
            GridCojo.Rows.Item(n).Cells("Telephonecojo").Value = rw("TelMem").ToString
            GridCojo.Rows.Item(n).Cells("Actioncojo").Value = "Enregistrer"
            GridCojo.Rows.Item(n).Cells("Typecojo").Value = rw("TypeComm").ToString
            GridCojo.Rows.Item(n).Cells("Fonctioncojo").Value = MettreApost(rw("TitreMem").ToString)
            GridCojo.Rows.Item(n).Cells("Organismecojo").Value = MettreApost(rw("FoncMem").ToString)
        Next
    End Sub

    Private Sub InitCojo()
        CmbCivCojo.Text = ""
        TxtCojo.Text = ""
        TxtFonctionCojo.Text = ""
        TxtContactCojo.Text = ""
        CmbTitreCojo.Text = ""
        TxtMailCojo.Text = ""
        CmbCivCojo.Focus()
    End Sub

    Private Sub TxtCojo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtCojo.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            If (TxtCojo.Text <> "") Then
                TxtFonctionCojo.Focus()
            End If
        End If
    End Sub

    Private Sub TxtFonctionCojo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtFonctionCojo.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            TxtContactCojo.Focus()
        End If
    End Sub

    Private Sub TxtContactCojo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtContactCojo.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            TxtMailCojo.Focus()
        End If
    End Sub

    Private Sub TxtMailCojo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtMailCojo.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            BtAjoutCojo_Click(Me, e)
        End If
    End Sub

    Private Sub OuvrirGroupPartic(ByVal value As Boolean)

        GbProposition.Enabled = value
        GbMission.Enabled = value
        GbListeRembours.Enabled = value
        GbMeOuvrageDelegue.Enabled = value
        GbEltsFournis.Enabled = value
        GbCojo.Enabled = value

    End Sub

    Private Sub InitialiserDonneesPartic()
        CmbLangue.Text = ""
        CmbDevise.Text = ""
        TxtDevise.Text = ""
        NumValidite.Value = 0
        CmbValidite.Text = ""
        CmbModalite.Text = ""
        CmbAssociation.Text = ""
        NumEclaircissement.Value = 0
        CmbEclaircissement.Text = ""
        ChkPropoTechnique.Checked = False
        NumNbreCopieTechnq.Value = 0
        NumNbreCopieTechnq.Enabled = False
        ChkPropoFinanciere.Checked = False
        NumNbreCopieFinance.Value = 0
        NumNbreCopieFinance.Enabled = False
        NumDureeMission.Value = 0
        CmbDureeMission.Text = ""
        NumNbrePersonnel.Value = 0
        NumNbrePersonnel.Enabled = False
        CmbFreqPersonnel.Text = ""
        CmbFreqPersonnel.Enabled = False
        NumDebutMission.Value = 0
        CmbDebutMission.Text = ""
        ChkFormationIntrinseque.Checked = False
        ChkImpotRedevable.Checked = False
        TxtDepenseRembourse.Text = ""
        GridDepenseRembourse.Rows.Clear()
        TxtMaitreOuvrageDelegue.Text = ""
        ChkOuvrageDelegueNon.Checked = True
        TxtEltsFournis.Text = ""
        GridEltsFournis.Rows.Clear()
        GridCojo.Rows.Clear()
        TypAssociation.Text = ""
        CombSection.Text = ""
        CombSection.Enabled = True
        TxtSection.Text = ""
        'FermetureDonneParticuliere()

        'Init nouvau bouton
        CheckRemisePropo.Checked = False
        DateReglement.EditValue = Nothing
        ChkConference.Checked = False
        DateConference.EditValue = Nothing
        HeureConference.EditValue = Nothing
        AdresseConfere.ResetText()
        NomConferenc.ResetText()
        TitreConference.ResetText()
        TelConferen.ResetText()
        CourrielConferen.ResetText()
        Impot.Checked = False
        MontantImpot.ResetText()
        RespectLoi.Checked = False
        Soustraitant.Checked = False
        RevisionPrix.Checked = False
        Inflation.Checked = False
        OuverturePropTechLine.Checked = False
        ProcedurProTechLine.ResetText()
        OuvrProFinLine.Checked = False
        ProcedurOvrPropoFinLine.ResetText()
        AdresslieuOuvr.ResetText()
        VillelieuOuvr.ResetText()
        BuroOuver.ResetText()
        PaysOuvertur.ResetText()
        DateNego.EditValue = Nothing
        AdresseNego.ResetText()
        DateService.EditValue = Nothing
        LieuService.ResetText()
        NomReclama.ResetText()
        TitreReclam.ResetText()
        AdresseReclam.ResetText()
        Agence.ResetText()
        TelecopociRecla.ResetText()
        ConsulRetenu.Checked = False
    End Sub

    Private Sub RetirerRem_Click(sender As Object, e As EventArgs) Handles RetirerRem.Click
        If GridDepenseRembourse.RowCount > 0 Then
            If ConfirmMsg("Voulez-vous vraiment supprimer ?") = DialogResult.Yes Then
                Dim Index = GridDepenseRembourse.CurrentRow.Index
                Dim Code = GridDepenseRembourse.Rows.Item(Index).Cells("Num1").Value
                GridDepenseRembourse.Rows.RemoveAt(Index)

                If Code.ToString <> "" Then
                    query = "delete from t_dp_listerembours where RefListe='" & Code & "'"
                    ExecuteNonQuery(query)
                End If
            End If
        Else
            SuccesMsg("Veuillez ajouter une dépense")
        End If
    End Sub

    Private Sub RetirerFour_Click(sender As Object, e As EventArgs) Handles RetirerFour.Click
        If GridEltsFournis.RowCount > 0 Then
            If ConfirmMsg("Voulez-vous vraiment supprimer ?") = DialogResult.Yes Then
                Dim index = GridEltsFournis.CurrentRow.Index
                Dim Code = GridEltsFournis.Rows.Item(index).Cells("Num2").Value
                GridEltsFournis.Rows.RemoveAt(index)
                If Code.ToString <> "" Then
                    query = "delete from t_dp_listeeltsfournis where RefListe='" & Code & "'"
                    ExecuteNonQuery(query)
                End If
            End If
        End If
    End Sub

    Private Sub RetirerCojo_Click(sender As Object, e As EventArgs) Handles RetirerCojo.Click
        If GridCojo.RowCount > 0 Then
            If ConfirmMsg("Voulez-vous vraiment supprimer ?") = DialogResult.Yes Then
                Dim index = GridCojo.CurrentRow.Index
                Dim RefCojo = GridCojo.Rows.Item(index).Cells("Refcojo").Value
                GridCojo.Rows.RemoveAt(index)
                If RefCojo.ToString <> "" Then
                    query = "delete from t_commission where CodeMem='" & RefCojo & "'"
                    ExecuteNonQuery(query)
                End If
            End If
        Else
            SuccesMsg("Veuillez ajouter un mêmbre de la commission")
        End If
    End Sub

    Private Sub GridDepenseRembourse_DoubleClick(sender As Object, e As EventArgs) Handles GridDepenseRembourse.DoubleClick
        If GridDepenseRembourse.Rows.Count > 0 Then
            ModifTous = True
            TypeModif = "Depense"
            Dim Ligne = GridDepenseRembourse.CurrentRow
            TxtDepenseRembourse.Text = Ligne.Cells("Objet1").Value.ToString
        End If
    End Sub

    Private Sub GridEltsFournis_DoubleClick(sender As Object, e As EventArgs) Handles GridEltsFournis.DoubleClick
        If GridEltsFournis.Rows.Count > 0 Then
            ModifTous = True
            TypeModif = "Fournisseur"
            Dim Ligne = GridEltsFournis.CurrentRow.Index
            TxtEltsFournis.Text = GridEltsFournis.Rows.Item(Ligne).Cells("Objet2").Value.ToString
        End If
    End Sub


    Private Sub GridCojo_DoubleClick(sender As Object, e As EventArgs) Handles GridCojo.DoubleClick
        If GridCojo.Rows.Count > 0 Then
            ModifTous = True
            TypeModif = "Cojo"

            Dim Ligne = GridCojo.CurrentRow.Index

            CmbCivCojo.Text = GridCojo.Rows.Item(Ligne).Cells("Civilitecojo").Value.ToString
            TxtCojo.Text = GridCojo.Rows.Item(Ligne).Cells("Nomcojo").Value.ToString
            TxtFonctionCojo.Text = GridCojo.Rows.Item(Ligne).Cells("Organismecojo").Value.ToString
            TxtContactCojo.Text = GridCojo.Rows.Item(Ligne).Cells("Telephonecojo").Value.ToString
            TxtMailCojo.Text = GridCojo.Rows.Item(Ligne).Cells("Emailcojo").Value.ToString
            CmbTitreCojo.Text = GridCojo.Rows.Item(Ligne).Cells("Fonctioncojo").Value.ToString
            CmbCivCojo.Focus()
        End If
    End Sub

    Private Sub ChkConference_CheckedChanged(sender As Object, e As EventArgs) Handles ChkConference.CheckedChanged
        If ChkConference.Checked = True Then
            DateConference.Enabled = True
            HeureConference.Enabled = True
            AdresseConfere.Enabled = True
            NomConferenc.Enabled = True
            TitreConference.Enabled = True
            TelConferen.Enabled = True
            CourrielConferen.Enabled = True
        Else
            DateConference.Enabled = False
            HeureConference.Enabled = False
            AdresseConfere.Enabled = False
            NomConferenc.Enabled = False
            TitreConference.Enabled = False
            TelConferen.Enabled = False
            CourrielConferen.Enabled = False
        End If
    End Sub

    Private Sub Impot_CheckedChanged(sender As Object, e As EventArgs) Handles Impot.CheckedChanged
        If Impot.Checked = True Then
            MontantImpot.Enabled = True
        Else
            MontantImpot.Enabled = False
        End If
    End Sub

    Private Sub OuverturePropTechLine_CheckedChanged(sender As Object, e As EventArgs) Handles OuverturePropTechLine.CheckedChanged
        If OuverturePropTechLine.Checked = True Then
            ProcedurProTechLine.Enabled = True
        Else
            ProcedurProTechLine.Enabled = False
        End If
    End Sub

    Private Sub OuvrProFinLine_CheckedChanged(sender As Object, e As EventArgs) Handles OuvrProFinLine.CheckedChanged
        If OuvrProFinLine.Checked = True Then
            ProcedurOvrPropoFinLine.Enabled = True
        Else
            ProcedurOvrPropoFinLine.Enabled = False
        End If
    End Sub

#End Region

#Region "Critères d'évaluation"
    'sauvegarder les critere d'evaluation
    Private Function SavePageEvaluation(NumDoss) As Boolean
        If GridEvaluation.RowCount > 0 Then
            If NumPoidsTech.Value.ToString = "0" And NumPoidsFin.Value.ToString = "0" Then
                SuccesMsg("Veuillez saisir le poids technique")
                NumPoidsTech.Focus()
                Return False
            End If

            If TxtScoreMinimum.Text = "0" And Val(TxtTotPts.Text.Replace(".", ",")) > 0 Then
                SuccesMsg("Veuillez saisir le score technique minimum requis pour être admis")
                TxtScoreMinimum.Select()
                Return False
            ElseIf Val(TxtScoreMinimum.Text.Replace(".", ",")) > 0 Then

                If CDec(TxtScoreMinimum.Text.Replace(".", ",")) > CDec(TxtTotPts.Text.Replace(".", ",")) Then
                    SuccesMsg("Le score technique minimum requis est suppérieur à la note totale !")
                    TxtScoreMinimum.ForeColor = Color.Red
                    TxtScoreMinimum.Focus()
                    Return False
                End If

                query = "Update T_DP SET PoidsTech='" & NumPoidsTech.Value.ToString.Replace(".", ",") & "', PoidsFin='" & NumPoidsFin.Value.ToString.Replace(".", ",") & "', ScoreTechMin='" & CDec(TxtScoreMinimum.Text.Replace(".", ",")) & "' where NumeroDp='" & EnleverApost(NumDoss) & "' and CodeProjet='" & ProjetEnCours & "'"
                ' query = "Update T_DP SET ScoreTechMin='" & CDec(TxtScoreMinimum.Text) & "' where NumeroDp='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
                TxtScoreMinimum.ForeColor = Color.Black
                Return True
            Else
                SuccesMsg("Saisie incorrecte !")
                TxtScoreMinimum.ForeColor = Color.Red
                TxtScoreMinimum.Focus()
                Return False
            End If
        Else
            Return False
        End If
    End Function

    'Charger les criteres d'evaluations
    Private Sub LoadPageCritereEvaluation(ByVal NumDossier As String)
        MajGridEvaluation()
    End Sub
    Private Sub GridEvaluation_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEvaluation.SizeChanged
        GridEvaluation.Columns.Item(1).Width = GridEvaluation.Width - 52
    End Sub

    Public Sub MajGridEvaluation()
        Dim TotPoint As Decimal = 0

        ' 1er niveau  ****************************
        GridEvaluation.Rows.Clear()
        query = "select RefCritere, IntituleCritere, PointCritere, CodeCritere from T_DP_CritereEval where NumeroDp='" & EnleverApost(NumDoss) & "' and CritereParent='0' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)

        For Each rw As DataRow In dt0.Rows
            'If IsNumeric(rw("PointCritere").ToString.Replace(".", ",")) Then TotPoint = TotPoint + CDec(rw("PointCritere").ToString.Replace(".", ","))
            If rw("PointCritere").ToString <> "" Then TotPoint = TotPoint + CDec(rw("PointCritere").ToString.Replace(".", ","))
            Dim n As Decimal = GridEvaluation.Rows.Add()
            GridEvaluation.Rows.Item(n).DefaultCellStyle.Font = New Font("Times New Roman", 11, FontStyle.Bold)
            GridEvaluation.Rows.Item(n).DefaultCellStyle.BackColor = Color.LightBlue 'LightGray
            GridEvaluation.Rows.Item(n).Cells("Ref").Value = rw("RefCritere").ToString
            GridEvaluation.Rows.Item(n).Cells("Details").Value = rw("CodeCritere").ToString & "/  " & MettreApost(rw("IntituleCritere").ToString) & "   ............................................................................................................................................................................................................."
            GridEvaluation.Rows.Item(n).Cells("Points").Value = rw("PointCritere").ToString.Replace(".", ",")

            ' 2eme niveau ********************************

            'Dim Reader As MySqlDataReader
            query = "Select RefCritere, IntituleCritere, PointCritere, TypeCritere, CodeCritere from T_DP_CritereEval where NumeroDp='" & EnleverApost(NumDoss) & "' and CritereParent='" & rw("RefCritere").ToString & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)

            For Each rw1 As DataRow In dt1.Rows
                Dim m As Decimal = GridEvaluation.Rows.Add()
                GridEvaluation.Rows.Item(m).DefaultCellStyle.Font = New Font("Times New Roman", 11, FontStyle.Regular)
                If (rw1("TypeCritere").ToString = "Bareme") Then
                    GridEvaluation.Rows.Item(m).DefaultCellStyle.Font = New Font("Times New Roman", 10, FontStyle.Regular)
                    GridEvaluation.Rows.Item(m).DefaultCellStyle.ForeColor = Color.Gray
                End If
                GridEvaluation.Rows.Item(m).Cells("Ref").Value = rw1("RefCritere").ToString
                GridEvaluation.Rows.Item(m).Cells("Details").Value = "   " & rw1("CodeCritere").ToString & "/  " & MettreApost(rw1("IntituleCritere").ToString) & "   .................................................................................................................................................................................................."
                GridEvaluation.Rows.Item(m).Cells("Points").Value = rw1("PointCritere").ToString.Replace(".", ",")

                ' 3eme niveau **************************************

                query = "select RefCritere,IntituleCritere,PointCritere,TypeCritere,CodeCritere from T_DP_CritereEval where NumeroDp='" & EnleverApost(NumDoss) & "' and CritereParent='" & rw1("RefCritere").ToString & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                For Each rw2 As DataRow In dt2.Rows
                    Dim x As Decimal = GridEvaluation.Rows.Add()
                    GridEvaluation.Rows.Item(x).DefaultCellStyle.Font = New Font("Times New Roman", 11, FontStyle.Regular)
                    If (rw2("TypeCritere").ToString = "Bareme") Then
                        GridEvaluation.Rows.Item(x).DefaultCellStyle.Font = New Font("Times New Roman", 10, FontStyle.Regular)
                        GridEvaluation.Rows.Item(x).DefaultCellStyle.ForeColor = Color.Gray
                    End If
                    GridEvaluation.Rows.Item(x).Cells("Ref").Value = rw2("RefCritere").ToString
                    GridEvaluation.Rows.Item(x).Cells("Details").Value = "      " & rw2("CodeCritere").ToString & "/  " & MettreApost(rw2("IntituleCritere").ToString) & "   ........................................................................................................................................................................................."
                    GridEvaluation.Rows.Item(x).Cells("Points").Value = rw2("PointCritere").ToString.Replace(".", ",")

                    ' 4eme niveau **************************************

                    query = "select RefCritere,IntituleCritere,PointCritere,TypeCritere,CodeCritere from T_DP_CritereEval where NumeroDp='" & EnleverApost(NumDoss) & "' and CritereParent='" & rw2("RefCritere").ToString & "'  and CodeProjet='" & ProjetEnCours & "'"
                    Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw3 As DataRow In dt3.Rows

                        Dim y As Decimal = GridEvaluation.Rows.Add()
                        GridEvaluation.Rows.Item(y).DefaultCellStyle.Font = New Font("Times New Roman", 11, FontStyle.Regular)
                        If (rw3("TypeCritere").ToString = "Bareme") Then
                            GridEvaluation.Rows.Item(y).DefaultCellStyle.Font = New Font("Times New Roman", 10, FontStyle.Regular)
                            GridEvaluation.Rows.Item(y).DefaultCellStyle.ForeColor = Color.Gray
                        End If
                        GridEvaluation.Rows.Item(y).Cells("Ref").Value = rw3("RefCritere").ToString
                        GridEvaluation.Rows.Item(y).Cells("Details").Value = "         " & rw3("CodeCritere").ToString & "/  " & MettreApost(rw3("IntituleCritere").ToString) & "   ...................................................................................................................................................................................."
                        GridEvaluation.Rows.Item(y).Cells("Points").Value = rw3("PointCritere").ToString.Replace(".", ",")
                    Next
                Next
            Next
        Next

        query = "select PoidsTech, PoidsFin, ScoreTechMin from T_DP where NumeroDp='" & EnleverApost(NumDoss) & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim rwdts As DataRow = ExcecuteSelectQuery(query).Rows(0)

        'NumPoidsTech.Value = 0
        'NumPoidsFin.Value = 0
        'TxtScoreMinimum.Value = 0

        If rwdts("PoidsTech").ToString <> "" Then NumPoidsTech.Value = CDec(rwdts("PoidsTech"))
        ' If rwdts("PoidsFin").ToString <> "" Then NumPoidsFin.Value = CDec(rwdts("PoidsFin"))
        If rwdts("ScoreTechMin").ToString <> "" Then TxtScoreMinimum.Text = CDec(rwdts("ScoreTechMin").ToString.Replace(".", ","))

        If (GridEvaluation.RowCount > 0) Then
            Dim q As Decimal = GridEvaluation.Rows.Add()
            Dim k As Decimal = GridEvaluation.Rows.Add()
            GridEvaluation.Rows.Item(k).Height = 2
            GridEvaluation.Rows.Item(k).DefaultCellStyle.BackColor = Color.Black
            Dim z As Decimal = GridEvaluation.Rows.Add()
            GridEvaluation.Rows.Item(z).DefaultCellStyle.Font = New Font("Times New Roman", 12, FontStyle.Bold)
            GridEvaluation.Rows.Item(z).Cells(1).Value = "TOTAL DES POINTS DE L'EVALUATION   ........................................................................................................................................................................................................................................................................"
            GridEvaluation.Rows.Item(z).Cells(2).Value = TotPoint.ToString

            TxtTotPts.Text = TotPoint.ToString
            NumPoidsTech.Enabled = True
            TxtScoreMinimum.Enabled = True
            BtModifierCritere.Enabled = True
        Else
            TxtTotPts.Text = "0"
            NumPoidsTech.Enabled = False
            TxtScoreMinimum.Enabled = False
            BtModifierCritere.Enabled = False
        End If
    End Sub

    Private Sub BtAjoutCritere_Click(sender As Object, e As EventArgs) Handles BtAjoutCritere.Click
        ReponseDialog = NumDoss
        AjoutCritereConsult.ShowDialog()
        ReponseDialog = ""
    End Sub

    Private Sub BtAjoutSousCritere_Click(sender As Object, e As EventArgs) Handles BtAjoutSousCritere.Click
        ReponseDialog = NumDoss
        AjoutSousCritereConsult.ShowDialog()
        ReponseDialog = ""
    End Sub

    Private Sub BtModifierCritere_Click(sender As Object, e As EventArgs) Handles BtModifierCritere.Click
        If GridEvaluation.RowCount > 0 And NumDoss <> "" Then
            ReponseDialog = NumDoss
            Dim Index = GridEvaluation.CurrentRow.Index
            ModifCriterEvaluationDP.RefCriterAModif = GridEvaluation.Rows.Item(Index).Cells("Ref").Value.ToString
            ModifCriterEvaluationDP.ShowDialog()
            ReponseDialog = ""
        End If
    End Sub

    'Private Sub CritèreToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CritèreToolStripMenuItem.Click
    '    ReponseDialog = NumDoss
    '    AjoutCritereConsult.ShowDialog()
    '    ReponseDialog = ""
    'End Sub

    'Private Sub SousCritèreToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SousCritèreToolStripMenuItem.Click
    '    ReponseDialog = NumDoss
    '    AjoutSousCritereConsult.ShowDialog()
    '    ReponseDialog = ""
    'End Sub

    'Private Sub ModifieCritererItem_Click(sender As Object, e As EventArgs) Handles ModifieCritererItem.Click
    '    If GridEvaluation.RowCount > 0 And NumDoss <> "" Then
    '        ReponseDialog = NumDoss
    '        Dim Index = GridEvaluation.CurrentRow.Index
    '        ModifCriterEvaluationDP.RefCriterAModif = GridEvaluation.Rows.Item(Index).Cells("Ref").Value.ToString
    '        ModifCriterEvaluationDP.ShowDialog()
    '        ReponseDialog = ""
    '    End If
    'End Sub

    Private Sub SupprimerCritereItem_Click(sender As Object, e As EventArgs) Handles SupprimerCritereItem.Click
        If GridEvaluation.RowCount > 0 And NumDoss <> "" Then
            If ConfirmMsg("Voulez-vous vraiment supprimer ?") = DialogResult.Yes Then
                Try
                    DebutChargement(True, "Suppression du critère en cours...")

                    Dim TableCriterSelec(2) As String
                    Dim Index = GridEvaluation.CurrentRow.Index
                    Dim CodeCriterSelec As String = GridEvaluation.Rows.Item(Index).Cells("Ref").Value.ToString

                    query = "Select CodeCritere, TypeCritere, CritereParent from t_dp_critereeval where RefCritere='" & CodeCriterSelec & "' and CodeProjet='" & ProjetEnCours & "'"
                    Dim dt As DataTable = ExcecuteSelectQuery(query)
                    For Each rw In dt.Rows
                        TableCriterSelec(0) = rw("CodeCritere").ToString 'CodeCritere
                        TableCriterSelec(1) = rw("TypeCritere").ToString 'TypeCritere
                        TableCriterSelec(2) = rw("CritereParent") 'CritereParent
                    Next

                    'suppression 
                    ExecuteNonQuery("delete from t_dp_critereeval where RefCritere='" & CodeCriterSelec & "' and CodeProjet='" & ProjetEnCours & "'")

                    'Recuperation du code critere parent du critere a supprimer
                    Dim NotationCriter As String = ""
                    Dim NbrEnfant As Integer = 0
                    Dim CodeCritere() As String = {"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X"}

                    query = "select CodeCritere from t_dp_critereeval where RefCritere='" & TableCriterSelec(2) & "' and NumeroDp='" & EnleverApost(NumDoss) & "' And CodeProjet ='" & ProjetEnCours & "'"
                    NotationCriter = ExecuteScallar(query)

                    If TableCriterSelec(1) = "Bareme" Then

                        query = "select RefCritere from t_dp_critereeval where CritereParent='" & TableCriterSelec(2) & "' and NumeroDp='" & EnleverApost(NumDoss) & "' And CodeProjet ='" & ProjetEnCours & "'"
                        Dim dt1 As DataTable = ExcecuteSelectQuery(query)

                        Dim n As Integer = 1
                        For Each rw In dt1.Rows
                            query = "Update t_dp_critereeval set CodeCritere='" & NotationCriter.ToString & "." & n & "'  where RefCritere='" & rw("RefCritere") & "' And CodeProjet ='" & ProjetEnCours & "'"
                            ExecuteNonQuery(query)
                            n += 1
                        Next
                    ElseIf TableCriterSelec(2) = 0 Then

                        '1er niveau **********************************
                        query = "select RefCritere, CritereParent from t_dp_critereeval where CritereParent='" & CodeCriterSelec & "' and NumeroDp='" & EnleverApost(NumDoss) & "' and CodeProjet='" & ProjetEnCours & "'"
                        Dim dt1 As DataTable = ExcecuteSelectQuery(query)

                        For Each rw1 In dt1.Rows

                            query = "delete from t_dp_critereeval where RefCritere='" & rw1("RefCritere") & "' and CodeProjet='" & ProjetEnCours & "'"
                            ExecuteNonQuery(query)

                            query = "select RefCritere, CritereParent from t_dp_critereeval where CritereParent='" & rw1("RefCritere") & "' and NumeroDp='" & EnleverApost(NumDoss) & "' and CodeProjet='" & ProjetEnCours & "'"
                            Dim dt2 As DataTable = ExcecuteSelectQuery(query)

                            For Each rw2 In dt2.Rows
                                ExecuteNonQuery("delete from t_dp_critereeval where RefCritere='" & rw2("RefCritere") & "' and CodeProjet='" & ProjetEnCours & "'")
                                ExecuteNonQuery("delete from t_dp_critereeval where CritereParent='" & rw2("RefCritere") & "' and CodeProjet='" & ProjetEnCours & "'")
                            Next
                        Next

                        'Mise a jours notation
                        Dim NbCritere As Decimal = 0

                        '1er niveau   **********************
                        query = "Select RefCritere, CritereParent from t_dp_critereeval where NumeroDp='" & EnleverApost(NumDoss) & "' and CritereParent='0' and CodeProjet='" & ProjetEnCours & "'"
                        dt1 = ExcecuteSelectQuery(query)

                        Dim i As Integer = 0
                        For Each rw In dt1.Rows
                            query = "Update t_dp_critereeval set CodeCritere ='" & CodeCritere(i) & "' where RefCritere='" & rw("RefCritere") & "' and CodeProjet='" & ProjetEnCours & "'"
                            ExecuteNonQuery(query)

                            '2eme niveau   **********************
                            query = "Select RefCritere, CritereParent from t_dp_critereeval where CritereParent='" & rw("RefCritere") & "' and NumeroDp='" & EnleverApost(NumDoss) & "' and CodeProjet='" & ProjetEnCours & "'"
                            Dim dt2 As DataTable = ExcecuteSelectQuery(query)

                            Dim j As Integer = 1
                            For Each rw2 In dt2.Rows
                                query = "Update t_dp_critereeval set CodeCritere ='" & CodeCritere(i) & "." & j & "' where RefCritere='" & rw2("RefCritere") & "' and CodeProjet='" & ProjetEnCours & "'"
                                ExecuteNonQuery(query)

                                '3eme niveau   **********************
                                query = "Select RefCritere, CritereParent from t_dp_critereeval where CritereParent='" & rw2("RefCritere") & "' and NumeroDp='" & EnleverApost(NumDoss) & "' and CodeProjet='" & ProjetEnCours & "'"
                                Dim dt3 As DataTable = ExcecuteSelectQuery(query)

                                Dim k As Integer = 1
                                For Each rw3 In dt3.Rows
                                    query = "Update t_dp_critereeval set CodeCritere ='" & CodeCritere(i) & "." & j & "." & k & "' where RefCritere='" & rw3("RefCritere") & "' and CodeProjet='" & ProjetEnCours & "'"
                                    ExecuteNonQuery(query)

                                    '4eme niveau   **********************
                                    query = "Select RefCritere, CritereParent from t_dp_critereeval where CritereParent='" & rw3("RefCritere") & "' and NumeroDp='" & EnleverApost(NumDoss) & "' and CodeProjet='" & ProjetEnCours & "'"
                                    Dim dt4 As DataTable = ExcecuteSelectQuery(query)

                                    Dim m As Integer = 1
                                    For Each rw4 In dt4.Rows
                                        ExecuteNonQuery("Update t_dp_critereeval set CodeCritere ='" & CodeCritere(i) & "." & j & "." & k & "." & m & "' where RefCritere='" & rw4("RefCritere") & "' and CodeProjet='" & ProjetEnCours & "'")
                                        m += 1
                                    Next
                                    k += 1
                                Next
                                j += 1
                            Next
                            i += 1
                        Next
                    Else
                        '1 eme niveau ********************************
                        query = "select RefCritere, CritereParent from t_dp_critereeval where CritereParent='" & CodeCriterSelec & "' and NumeroDp='" & EnleverApost(NumDoss) & "' and CodeProjet='" & ProjetEnCours & "'"
                        dt = ExcecuteSelectQuery(query)

                        For Each rw In dt.Rows
                            ExecuteNonQuery("delete from t_dp_critereeval where RefCritere='" & rw("RefCritere") & "' and CodeProjet='" & ProjetEnCours & "'")
                            ExecuteNonQuery("delete from t_dp_critereeval where CritereParent='" & rw("RefCritere") & "' and CodeProjet='" & ProjetEnCours & "'")
                        Next

                        'Mise a jour notation et points
                        query = "select RefCritere, CritereParent from t_dp_critereeval where CritereParent='" & TableCriterSelec(2) & "' and NumeroDp='" & EnleverApost(NumDoss) & "' and CodeProjet='" & ProjetEnCours & "'"
                        dt = ExcecuteSelectQuery(query)

                        Dim i As Integer = 1
                        For Each rw In dt.Rows
                            ExecuteNonQuery("Update t_dp_critereeval set CodeCritere ='" & NotationCriter.ToString & "." & i & "' where RefCritere='" & rw("RefCritere") & "' and CodeProjet='" & ProjetEnCours & "'")

                            query = "select RefCritere from t_dp_critereeval where CritereParent='" & rw("RefCritere") & "' and NumeroDp='" & EnleverApost(NumDoss) & "' and CodeProjet='" & ProjetEnCours & "'"
                            Dim dt1 As DataTable = ExcecuteSelectQuery(query)

                            Dim j As Integer = 1
                            For Each rw1 In dt1.Rows
                                ExecuteNonQuery("Update t_dp_critereeval set CodeCritere ='" & NotationCriter.ToString & "." & i & "." & j & "' where RefCritere='" & rw1("RefCritere") & "' and CodeProjet='" & ProjetEnCours & "'")

                                query = "select RefCritere from t_dp_critereeval where CritereParent='" & rw1("RefCritere") & "' and NumeroDp='" & EnleverApost(NumDoss) & "' and CodeProjet='" & ProjetEnCours & "'"
                                Dim dt2 As DataTable = ExcecuteSelectQuery(query)

                                Dim k As Integer = 1
                                For Each rw2 In dt2.Rows
                                    ExecuteNonQuery("Update t_dp_critereeval set CodeCritere ='" & NotationCriter.ToString & "." & i & "." & j & "." & k & "' where RefCritere='" & rw2("RefCritere") & "' and CodeProjet='" & ProjetEnCours & "'")
                                    k += 1
                                Next
                                j += 1
                            Next
                            i += 1
                        Next

                        'Mise a jour note
                        Dim CodePere As Decimal = Val(ExecuteScallar("select CritereParent from t_dp_critereeval where RefCritere='" & TableCriterSelec(2) & "' and CodeProjet='" & ProjetEnCours & "'"))
                        Dim Somme1 As String = ExecuteScallar("select SUM(PointCritere) from t_dp_critereeval where CritereParent='" & TableCriterSelec(2) & "' and PointCritere<>'' and NumeroDp='" & EnleverApost(NumDoss) & "' and CodeProjet='" & ProjetEnCours & "'")

                        If Somme1.ToString <> "" Then
                            ExecuteNonQuery("Update t_dp_critereeval set PointCritere='" & Somme1.Replace(",", ".") & "' where RefCritere='" & TableCriterSelec(2) & "' and CodeProjet='" & ProjetEnCours & "'")
                        Else
                            ExecuteNonQuery("Update t_dp_critereeval set PointCritere=NULL where RefCritere='" & TableCriterSelec(2) & "' and CodeProjet='" & ProjetEnCours & "'")
                        End If

                        If CodePere > 0 Then
                            Dim Somme2 As String = ExecuteScallar("select SUM(PointCritere) from t_dp_critereeval where CritereParent='" & CodePere & "' and PointCritere<>'' and NumeroDp='" & EnleverApost(NumDoss) & "' and CodeProjet='" & ProjetEnCours & "'")
                            If Somme2.ToString <> "" Then  ExecuteNonQuery("Update t_dp_critereeval set PointCritere ='" & Somme2.Replace(",", ".") & "' where RefCritere='" & CodePere & "' and CodeProjet='" & ProjetEnCours & "'")
                            If Somme2.ToString = "" Then ExecuteNonQuery("Update t_dp_critereeval set PointCritere =NULL where RefCritere='" & CodePere & "' and CodeProjet='" & ProjetEnCours & "'")
                        End If
                        End If
                    FinChargement()
                    SuccesMsg("Suppression effectué avec succès")
                    MajGridEvaluation()
                Catch ex As Exception
                    FailMsg(ex.ToString)
                    FinChargement()
                End Try
            End If
        End If
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If AffichDossDp = True Or GridEvaluation.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub NumPoidsTech_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumPoidsTech.ValueChanged
        If NumPoidsTech.Value <> 0 Then
            NumPoidsFin.Value = 1 - NumPoidsTech.Value
        Else
            NumPoidsFin.Value = 0
        End If
    End Sub

    Private Sub RaseCritereEval()

        TxtTotPts.Text = "0"
        TxtScoreMinimum.Text = "0"
        TxtScoreMinimum.Enabled = False

        ' NumPoidsFin.Value = 0
        NumPoidsTech.Value = 0
        NumPoidsTech.Enabled = False

        GridEvaluation.Rows.Clear()
        GridEvaluation.ReadOnly = True
    End Sub


#End Region

#Region "Termes de References"
    Private Sub LoadPageSpecTech()
        If CheminDocTDR.ToString <> "" Then
            DebutChargement(True, "Chargement des termes de références en cours...")
            DocTDR.LoadDocument(CheminDocTDR.ToString, DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
            FinChargement()
        End If
    End Sub

    Private Sub BtModifTDR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtModifTDR.Click
        If (NumDoss <> "") Then

            If DocTDR.Text.Trim = "" Then
                SuccesMsg("Veuillez importez un fichier")
                Exit Sub
            End If
            DebutChargement()
            ReponseDialog = CheminDocTDR
            ExceptRevue = TxtNumDp.Text
            ExceptRevue2 = "DP"
            SaisieTexte.ShowDialog()
            FinChargement()
            DocTDR.LoadDocument(CheminDocTDR, DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
            ReponseDialog = ""
            ExceptRevue = ""
            ExceptRevue2 = ""
        End If
    End Sub

    Private Sub BtImportTDR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtImportTDR.Click
        Try
            If (NumDoss <> "") Then
                Dim dlg As New OpenFileDialog
                dlg.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                'dlg.Filter = "Documents Word (*.doc; *.docx)|*.doc;*.docx"
                dlg.Filter = "Documents Word (*.docx)|*.docx"

                If dlg.ShowDialog() = DialogResult.OK Then
                    If (dlg.FileName.ToString = "") Then
                        Exit Sub
                    End If

                    DebutChargement(True, "Importation des termes de références en cours...")

                    If CheminDocTDR.ToString = "" Then
                        CheminDocTDR = line & "\DP\" & FormatFileName(NumDoss.ToString, "_")
                        ExecuteNonQuery("update t_dp set CheminDocTDR='" & CheminDocTDR.ToString.Replace("\", "\\") & "\\TDR.docx" & "' where NumeroDp ='" & EnleverApost(NumDoss) & "' and CodeProjet='" & ProjetEnCours & "'")
                    End If

                    CheminDocTDR = line & "\DP\" & FormatFileName(NumDoss.ToString, "_")
                    If (Directory.Exists(CheminDocTDR) = False) Then
                        Directory.CreateDirectory(CheminDocTDR)
                    End If

                    CheminDocTDR = CheminDocTDR & "\TDR.docx"
                    Dim Chemin1 As String = line & "\DP\" & FormatFileName(NumDoss.ToString, "_") & "\TDR1.Rtf"
                    Dim fStream As FileStream
                    fStream = New FileStream(dlg.FileName, FileMode.Open)
                    DocTDR.LoadDocument(fStream, DevExpress.XtraRichEdit.DocumentFormat.OpenXml)

                    'Dim fStream2 As FileStream
                    'fStream2 = New FileStream(NomDossier, FileMode.Open)
                    DocTDR.SaveDocument(Chemin1, DevExpress.XtraRichEdit.DocumentFormat.Rtf)
                    DocTDR.SaveDocument(CheminDocTDR, DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
                    FinChargement()
                End If
            End If
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

#End Region

#Region "Apperçu du dossier"

    Private Function SavePageAppercuDp(NumDoss) As Boolean
        Try

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Function

    Private Function UpdateInfoDossierDP(Optional ByVal Impression As Boolean = False) As Boolean
        If LayoutView1.RowCount = 0 Then Return False
        Try
            dr = LayoutView1.GetDataRow(LayoutView1.FocusedRowHandle)
            NumDoss = dr("N°").ToString

            'verification des TDR
            If dr("CheminDocTDR").ToString = "" Then
                FailMsg("Veuillez ajouter les termes de references")
                Return False
            End If

            ' Dim CheminTDR1 As String = line & "\DP\" & FormatFileName(NumDoss.ToString, "_") & "\TDR1.Rtf"
            Dim CheminTDR As String = line & "\DP\" & FormatFileName(NumDoss.ToString, "_") & "\TDR1.Rtf"

            If Not File.Exists(CheminTDR) Then
                FailMsg("Le fichier de TDR n'existe pas ou a été supprimer")
                Return False
            End If

            DebutChargement(True, "Chargement du dossier de la demande de proposition en cours...")

            Dim Chemin As String = lineEtat & "\Marches\DP\ElaborationDp\"

            Dim reports1, reports2, reports3, reports4, reports5, reports6, reports7, reports8, reports9, reports10, reports12, reports13, reports14 As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim DatSet = New DataSet

            reports1.Load(Chemin & "1_DP_principal_1.rpt")
            reports2.Load(Chemin & "2_FormulaireTech_5.1.rpt")
            reports3.Load(Chemin & "3_DemandeProposition.rpt")
            reports4.Load(Chemin & "4_Tech7.rpt")
            reports5.Load(Chemin & "5_DemandeProposition.rpt")
            reports6.Load(Chemin & "6_ConsultForm1.rpt")
            reports7.Load(Chemin & "7_DemandeProposition.rpt")
            reports8.Load(Chemin & "8_ConsultForm2.rpt")
            reports9.Load(Chemin & "9_DemandeProposition.rpt")
            reports10.Load(Chemin & "10_ConsultForm3.rpt")
            reports12.Load(Chemin & "12_Consultant.rpt")
            reports13.Load(Chemin & "13_Consult_Form4.rpt")
            reports14.Load(Chemin & "14_Consultant.rpt")

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = reports1.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            'CrTables = reports2.Database.Tables
            'For Each CrTable In CrTables
            '    crtableLogoninfo = CrTable.LogOnInfo
            '    crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '    CrTable.ApplyLogOnInfo(crtableLogoninfo)
            'Next

            'CrTables = reports3.Database.Tables
            'For Each CrTable In CrTables
            '    crtableLogoninfo = CrTable.LogOnInfo
            '    crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '    CrTable.ApplyLogOnInfo(crtableLogoninfo)
            'Next

            'CrTables = reports4.Database.Tables
            'For Each CrTable In CrTables
            '    crtableLogoninfo = CrTable.LogOnInfo
            '    crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '    CrTable.ApplyLogOnInfo(crtableLogoninfo)
            'Next

            'CrTables = reports5.Database.Tables
            'For Each CrTable In CrTables
            '    crtableLogoninfo = CrTable.LogOnInfo
            '    crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '    CrTable.ApplyLogOnInfo(crtableLogoninfo)
            'Next

            'CrTables = reports6.Database.Tables
            'For Each CrTable In CrTables
            '    crtableLogoninfo = CrTable.LogOnInfo
            '    crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '    CrTable.ApplyLogOnInfo(crtableLogoninfo)
            'Next

            'CrTables = reports7.Database.Tables
            'For Each CrTable In CrTables
            '    crtableLogoninfo = CrTable.LogOnInfo
            '    crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '    CrTable.ApplyLogOnInfo(crtableLogoninfo)
            'Next

            'CrTables = reports8.Database.Tables
            'For Each CrTable In CrTables
            '    crtableLogoninfo = CrTable.LogOnInfo
            '    crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '    CrTable.ApplyLogOnInfo(crtableLogoninfo)
            'Next

            'CrTables = reports9.Database.Tables
            'For Each CrTable In CrTables
            '    crtableLogoninfo = CrTable.LogOnInfo
            '    crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '    CrTable.ApplyLogOnInfo(crtableLogoninfo)
            'Next

            'CrTables = reports10.Database.Tables
            'For Each CrTable In CrTables
            '    crtableLogoninfo = CrTable.LogOnInfo
            '    crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '    CrTable.ApplyLogOnInfo(crtableLogoninfo)
            'Next

            'CrTables = reports11.Database.Tables
            'For Each CrTable In CrTables
            '    crtableLogoninfo = CrTable.LogOnInfo
            '    crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '    CrTable.ApplyLogOnInfo(crtableLogoninfo)
            'Next

            CrTables = reports12.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            'CrTables = reports13.Database.Tables
            'For Each CrTable In CrTables
            '    crtableLogoninfo = CrTable.LogOnInfo
            '    crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '    CrTable.ApplyLogOnInfo(crtableLogoninfo)
            'Next
            'CrTables = reports14.Database.Tables
            'For Each CrTable In CrTables
            '    crtableLogoninfo = CrTable.LogOnInfo
            '    crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '    CrTable.ApplyLogOnInfo(crtableLogoninfo)
            'Next
            'CrTables = reports15.Database.Tables
            'For Each CrTable In CrTables
            '    crtableLogoninfo = CrTable.LogOnInfo
            '    crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '    CrTable.ApplyLogOnInfo(crtableLogoninfo)
            'Next

            reports1.SetDataSource(DatSet)
            'reports2.SetDataSource(DatSet)
            'reports3.SetDataSource(DatSet)
            ' reports4.SetDataSource(DatSet)
            ' reports5.SetDataSource(DatSet)
            ' reports7.SetDataSource(DatSet)
            ' reports6.SetDataSource(DatSet)
            'reports8.SetDataSource(DatSet)
            ' reports9.SetDataSource(DatSet)
            ' reports10.SetDataSource(DatSet)
            ' reports11.SetDataSource(DatSet)
            reports12.SetDataSource(DatSet)
            'reports13.SetDataSource(DatSet)
            'reports14.SetDataSource(DatSet)

            'Les paramettres
            reports1.SetParameterValue("CodeProjet", ProjetEnCours)
            reports1.SetParameterValue("NumeroDp", EnleverApost(NumDoss.ToString))

            'reports3.SetParameterValue("NumDp", EnleverApost(NumDoss.ToString))
            'reports5.SetParameterValue("NumDp", EnleverApost(NumDoss.ToString))
            'reports7.SetParameterValue("NumDp", EnleverApost(NumDoss.ToString))
            'reports9.SetParameterValue("NumDp", EnleverApost(NumDoss.ToString))

            reports12.SetParameterValue("CodeProjet", ProjetEnCours)
            reports12.SetParameterValue("NumDp", EnleverApost(NumDoss.ToString))

            Dim Chemin1 As String = Path.GetTempFileName & ".doc"
            Dim Chemin2 As String = Path.GetTempFileName & ".doc"
            Dim Chemin3 As String = Path.GetTempFileName & ".doc"
            Dim Chemin4 As String = Path.GetTempFileName & ".doc"
            Dim Chemin5 As String = Path.GetTempFileName & ".doc"
            Dim Chemin6 As String = Path.GetTempFileName & ".doc"
            Dim Chemin7 As String = Path.GetTempFileName & ".doc"
            Dim Chemin8 As String = Path.GetTempFileName & ".doc"
            Dim Chemin9 As String = Path.GetTempFileName & ".doc"
            Dim Chemin10 As String = Path.GetTempFileName & ".doc"
            Dim Chemin12 As String = Path.GetTempFileName & ".doc"
            Dim Chemin13 As String = Path.GetTempFileName & ".doc"
            Dim Chemin14 As String = Path.GetTempFileName & ".doc"

            reports1.ExportToDisk(ExportFormatType.WordForWindows, Chemin1)
            reports2.ExportToDisk(ExportFormatType.WordForWindows, Chemin2)
            reports3.ExportToDisk(ExportFormatType.WordForWindows, Chemin3)
            reports4.ExportToDisk(ExportFormatType.WordForWindows, Chemin4)
            reports5.ExportToDisk(ExportFormatType.WordForWindows, Chemin5)
            reports6.ExportToDisk(ExportFormatType.WordForWindows, Chemin6)
            reports7.ExportToDisk(ExportFormatType.WordForWindows, Chemin7)
            reports8.ExportToDisk(ExportFormatType.WordForWindows, Chemin8)
            reports9.ExportToDisk(ExportFormatType.WordForWindows, Chemin9)
            reports10.ExportToDisk(ExportFormatType.WordForWindows, Chemin10)
            reports12.ExportToDisk(ExportFormatType.WordForWindows, Chemin12)
            reports13.ExportToDisk(ExportFormatType.WordForWindows, Chemin13)
            reports14.ExportToDisk(ExportFormatType.WordForWindows, Chemin14)
            ' reports15.ExportToDisk(ExportFormatType.WordForWindows, Chemin15)

            Dim NomRepCheminSauve As String = line & "\DP\" & FormatFileName(NumDoss.ToString, "_")

            If Not Directory.Exists(NomRepCheminSauve) Then
                Directory.CreateDirectory(NomRepCheminSauve)
            End If
            NomRepCheminSauve = NomRepCheminSauve & "\ElaborationDp.pdf"
            Dim CheminSauveDoc As String = NomRepCheminSauve & "\ElaborationDp.doc"

            'Ajout de la page de garde
            Dim oWord As New Word.Application
            Dim currentDoc As Word.Document

            Try
                currentDoc = oWord.Documents.Add(Chemin1)
                Dim myRange As Word.Range = currentDoc.Bookmarks.Item("\endofdoc").Range
                Dim mySection1 As Word.Section = AjouterNouvelleSectionDocument(currentDoc, myRange)
                mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                myRange.InsertFile(Chemin2)
                mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                myRange.InsertFile(Chemin3)
                mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                myRange.InsertFile(Chemin4)
                mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                myRange.InsertFile(Chemin5)
                mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                myRange.InsertFile(Chemin6)
                mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                myRange.InsertFile(Chemin7)
                mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                myRange.InsertFile(Chemin8)
                mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                myRange.InsertFile(Chemin9)

                mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                myRange.InsertFile(Chemin10)

                'Insertion des TDR
                Try
                    If File.Exists(CheminTDR) = True Then
                        mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                        mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                        myRange.InsertFile(CheminTDR)
                    End If
                Catch exs As IO.IOException
                    FinChargement()
                    FailMsg("Le fichier [TDR] est utilisé par une autre application" & vbNewLine & "Veuillez le fermer svp.")
                    oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                    Return False
                Catch ex As Exception
                    FinChargement()
                    FailMsg(ex.ToString)
                    oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                    Return False
                End Try

                mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                myRange.InsertFile(Chemin12)

                mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                myRange.InsertFile(Chemin13)
                mySection1 = AjouterNouvelleSectionDocument(currentDoc, myRange)
                mySection1.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                myRange.InsertFile(Chemin14)

                currentDoc.SaveAs2(FileName:=NomRepCheminSauve, FileFormat:=Word.WdSaveFormat.wdFormatPDF)
                currentDoc.SaveAs2(FileName:=CheminSauveDoc, FileFormat:=Word.WdSaveFormat.wdFormatDocument)

                oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                FinChargement()

            Catch ex As Exception
                FinChargement()
                FailMsg(ex.ToString)
                oWord.Quit(SaveChanges:=Word.WdSaveOptions.wdDoNotSaveChanges)
                Return False
            End Try

        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
            Return False
        End Try
        Return True
    End Function

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
                        Dim fStream As FileStream
                        fStream = New FileStream(dlg.FileName, FileMode.Open)

                        TabAnnexes.SelectedTabPage.Text = dlg.FileName
                        Dim partDlg() As String = dlg.FileName.Split("."c)

                        If (partDlg(1) = "doc" Or partDlg(1) = "docx") Then
                            Try
                                Dim NewWord As New RichEditControl
                                If (partDlg(1) = "docx") Then
                                    NewWord.LoadDocument(fStream, DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
                                ElseIf (partDlg(1) = "doc") Then
                                    NewWord.LoadDocument(fStream, DevExpress.XtraRichEdit.DocumentFormat.Doc)
                                End If
                                NewWord.Dock = DockStyle.Fill
                                TabAnnexes.SelectedTabPage.Controls.Add(NewWord)
                                TabAnnexes.SelectedTabPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True
                                EnregistrerPJ(dlg.FileName)
                                PageAjout()
                            Catch ex1 As Exception
                                SuccesMsg(ex1.ToString)
                            End Try

                        ElseIf (partDlg(1) = "pdf") Then
                            Try
                                Dim Web1 As New WebBrowser
                                Web1.Dock = DockStyle.Fill
                                Web1.Navigate(dlg.FileName)
                                TabAnnexes.SelectedTabPage.Controls.Add(Web1)
                                TabAnnexes.SelectedTabPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True
                                EnregistrerPJ(dlg.FileName)
                                PageAjout()
                            Catch ex1 As Exception
                                SuccesMsg(ex1.ToString)
                            End Try

                        ElseIf (partDlg(1) = "png" Or partDlg(1) = "jpg" Or partDlg(1) = "bmp" Or partDlg(1) = "gif") Then

                            Try
                                Dim NewImg As New PictureBox
                                NewImg.Image = Image.FromStream(fStream)
                                NewImg.Dock = DockStyle.Fill
                                NewImg.SizeMode = PictureBoxSizeMode.Zoom
                                TabAnnexes.SelectedTabPage.Controls.Add(NewImg)
                                TabAnnexes.SelectedTabPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True
                                EnregistrerPJ(dlg.FileName)
                                PageAjout()
                            Catch ex1 As Exception
                                SuccesMsg(ex1.ToString)
                            End Try

                        Else
                            SuccesMsg("Format non pris en charge!")
                            TabAnnexes.SelectedTabPage.Text = "+"
                        End If

                        fStream.Close()

                    End If
                Catch ex As Exception
                    SuccesMsg(ex.ToString)
                End Try
            End If
        End If

    End Sub

    Private Sub EnregistrerPJ(ByVal NomPJ As String)
        Dim NomDossier As String = line & "\DP\" & typeMarc & "\" & methodMarc & "\" & NumDoss.Replace("/", "_")
        If (Directory.Exists(NomDossier) = True) Then
            Dim partNomPj() As String = NomPJ.Split("\"c)
            Dim NomCourtPJ As String = ""
            For Each part As String In partNomPj
                NomCourtPJ = part
            Next
            File.Copy(NomPJ, NomDossier & "\" & NomCourtPJ, True)
        End If
    End Sub

#End Region

#Region "Menu contextuel"

    Private Sub AjoutInfoDp()
        If (NumDoss <> "") Then
            If (PourModif = True) Then
                BtRetour.Enabled = True
                GridArchives.Enabled = False
                'ChkNumDpAuto.Checked = False
                ' ChkNumDpAuto.Enabled = False
                TxtLibDp.Enabled = False
                'ChkLibDpAuto.Enabled = False
                'GridMarcheDp.Enabled = True
                CmbTypeRemune.Enabled = True
                DateDepot.Enabled = True
                HeureDepot.Enabled = True
                DateOuverture.Enabled = True
                HeureOuverture.Enabled = True
                GbListeConsult.Enabled = True
                ' OuvrirGroupPartic()
                NumPoidsTech.Enabled = True
                TxtScoreMinimum.Enabled = True
                BtModifTDR.Enabled = True

            End If

            ItemsPays()
            'MajGridMarche()
            ' ChargerGridConsult()
            ItemDevise()
            'MajGridRembours()
            'MajGridEltsFournis()
            'MajGridCojo()
            MajGridEvaluation()

            query = "Select LibelleMiss,TypeRemune,ConfPrea,DelaiEclaircissement,DebutMiss,DureeMiss,RessPersonnel,FormationIntrinsq,ImpotRembourse,PropoTech,PropoFin,PoidsTech,PoidsFin,ScoreTechMin,LangueDp,MonnaieEval,ValiditePropo,ModalitePropo,DateLimitePropo,AssoListeRest,MeOuvrageDelegue,DateOuverture from T_DP where NumeroDp='" & NumDoss & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows

                If (rw("LibelleMiss").ToString <> "") Then TxtLibDp.Text = MettreApost(rw("LibelleMiss").ToString)
                If (rw("TypeRemune").ToString <> "") Then CmbTypeRemune.SelectedText = rw("TypeRemune").ToString
                If (rw("DateLimitePropo").ToString <> "") Then
                    Dim partDepot() As String = rw("DateLimitePropo").ToString.Split(" "c)
                    DateDepot.DateTime = CDate(partDepot(0)).ToShortDateString
                    HeureDepot.Time = CDate(partDepot(1)).ToLongTimeString
                Else
                    DateDepot.DateTime = "01/01/2000"
                    HeureDepot.Time = "00:00:00"
                End If
                If (rw("DateOuverture").ToString <> "") Then
                    Dim partOuvert() As String = rw("DateOuverture").ToString.Split(" "c)
                    DateOuverture.DateTime = CDate(partOuvert(0)).ToShortDateString
                    HeureOuverture.Time = CDate(partOuvert(1)).ToLongTimeString
                Else
                    DateOuverture.DateTime = "01/01/2000"
                    HeureOuverture.Time = "00:00:00"
                End If
                If (rw("LangueDp").ToString <> "") Then CmbLangue.Text = rw("LangueDp").ToString
                If (rw("MonnaieEval").ToString <> "") Then CmbDevise.Text = rw("MonnaieEval").ToString
                If (rw("ValiditePropo").ToString <> "") Then
                    Dim partValid() As String = rw("ValiditePropo").ToString.Split(" "c)
                    NumValidite.Value = CInt(partValid(0))
                    CmbValidite.Text = partValid(1)
                End If
                If (rw("ModalitePropo").ToString <> "") Then CmbModalite.Text = rw("ModalitePropo").ToString
                If (rw("AssoListeRest").ToString <> "") Then
                    If (rw("AssoListeRest").ToString = "OUI") Then CmbAssociation.Text = "OUI (groupement)"
                    If (rw("AssoListeRest").ToString = "NON") Then CmbAssociation.Text = "NON (individuel)"
                End If
                'If (rw("ConfPrea").ToString <> "") Then
                '    ChkConference.Checked = True
                '    Dim partConf() As String = rw("ConfPrea").ToString.Split(" "c)
                '    DateConference.DateTime = CDate(partConf(0)).ToShortDateString
                '    HeureConference.Time = CDate(partConf(1)).ToLongTimeString
                'End If
                If (rw("DelaiEclaircissement").ToString <> "") Then
                    Dim partEclair() As String = rw("DelaiEclaircissement").ToString.Split(" "c)
                    NumEclaircissement.Value = CInt(partEclair(0))
                    CmbEclaircissement.Text = partEclair(1)
                End If
                If (rw("PropoTech").ToString <> "0") Then
                    ChkPropoTechnique.Checked = True
                    NumNbreCopieTechnq.Value = CInt(rw("PropoTech"))
                End If
                If (rw("PropoFin").ToString <> "0") Then
                    ChkPropoFinanciere.Checked = True
                    NumNbreCopieFinance.Value = CInt(rw("PropoFin"))
                End If
                If (rw("DureeMiss").ToString <> "") Then
                    Dim partDuree() As String = rw("DureeMiss").ToString.Split(" "c)
                    NumDureeMission.Value = CInt(partDuree(0))
                    CmbDureeMission.Text = partDuree(1)
                End If
                If (rw("RessPersonnel").ToString <> "") Then
                    Dim partPers() As String = rw("RessPersonnel").ToString.Split(" "c)
                    NumNbrePersonnel.Value = CInt(partPers(0))
                    CmbFreqPersonnel.Text = partPers(1)
                End If
                If (rw("DebutMiss").ToString <> "") Then
                    Dim partDeb() As String = rw("DebutMiss").ToString.Split(" "c)
                    NumDebutMission.Value = CInt(partDeb(0))
                    CmbDebutMission.Text = partDeb(1)
                End If
                If (rw("FormationIntrinsq").ToString = "OUI") Then
                    ChkFormationIntrinseque.Checked = True
                End If
                If (rw("ImpotRembourse").ToString = "OUI") Then
                    ChkImpotRedevable.Checked = True
                End If
                If (rw("MeOuvrageDelegue").ToString <> "") Then
                    ChkOuvrageDelegueOui.Checked = True
                    TxtMaitreOuvrageDelegue.Text = MettreApost(rw("MeOuvrageDelegue").ToString)
                End If
                'If (rw("PoidsTech").ToString <> "") Then
                '    NumPoidsTech.Value = CDec(rw("PoidsTech"))
                'End If
                'If (rw("ScoreTechMin").ToString <> "") Then
                '    TxtScoreMinimum.Text = rw("ScoreTechMin").ToString
                'End If
            Next


            Dim NomDossier As String = line & "\DP\" & typeMarc & "\" & methodMarc & "\" & NumDoss.Replace("/", "_") & "\TDR.docx"
            'MsgBox("Type:" & typeMarc & " Methode:" & methodMarc, MsgBoxStyle.Information)
            If (File.Exists(NomDossier) = True) Then
                'MsgBox("Le TDR existe!", MsgBoxStyle.Information)   'Vérif du TDR
                Dim nfStream As FileStream
                nfStream = New FileStream(NomDossier, FileMode.Open)
                DocTDR.LoadDocument(nfStream, DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
                DocTDR.ReadOnly = True
            End If

        End If
    End Sub

#End Region

    Private Sub CmbDossAMI_TextChanged(sender As Object, e As EventArgs) Handles CmbDossAMI.TextChanged
        If CmbDossAMI.Text.Trim <> "" Then
            BtEnrgConsult.Enabled = False
            BtSuppConsult.Enabled = False
        Else
            BtEnrgConsult.Enabled = True
            BtSuppConsult.Enabled = True
        End If
    End Sub

End Class