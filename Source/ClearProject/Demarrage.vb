Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Text
Imports System.Windows.Forms
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Threading

Public Class Demarrage

    Private Sub Demarrage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        SplitEvaluation.Visible = False
        SplitBudget.Visible = False
        SplitMarches.Visible = False

        Dim taille As Decimal = Math.Round((hauteur - 340) / 3, 0) 'Pour 3 elements par ligne
        'Dim taille As Decimal = Math.Round((hauteur - 273) / 3, 0) 'Pour 2 elements par ligne
        TuileDemarrage.ItemSize = taille

        TuileDemarrage.Visible = True
        TuileDemarrage.Dock = DockStyle.Fill

    End Sub

    Private Sub OuvrirNavPane()
        If (ClearMdi.NavBarControl1.OptionsNavPane.NavPaneState = DevExpress.XtraNavBar.NavPaneState.Collapsed) Then ClearMdi.NavBarControl1.OptionsNavPane.NavPaneState = DevExpress.XtraNavBar.NavPaneState.Expanded
    End Sub

    Private Sub TileBudget_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileBudget.ItemClick
        ClearMdi.NavBarControl1.ActiveGroup = ClearMdi.GroupBudget
        SplitBudget.Visible = True
        SplitBudget.Dock = DockStyle.Fill
        TuileDemarrage.Visible = False
        Me.Text = "GESTION BUDGETAIRE"
    End Sub

    Private Sub TileMarches_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileMarches.ItemClick

        ClearMdi.NavBarControl1.ActiveGroup = ClearMdi.GroupMarches
        SplitMarches.Visible = True
        SplitMarches.Dock = DockStyle.Fill
        TuileDemarrage.Visible = False
        Me.Text = "PASSATION DES MARCHES"

    End Sub

    Private Sub BtAccueilBudget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAccueilBudget.Click
        TuileDemarrage.Visible = True
        TuileDemarrage.Dock = DockStyle.Fill
        SplitEvaluation.Visible = False
        Me.Text = "ACCUEIL"
    End Sub

    Private Sub BtAccueilMarches_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAccueilMarches.Click
        TuileDemarrage.Visible = True
        TuileDemarrage.Dock = DockStyle.Fill
        SplitMarches.Visible = False
        Me.Text = "ACCUEIL"
    End Sub

    Private Sub BtPrevisionActivite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtPrevisionActivite.Click
        Disposer_form(FichesActivitesEtape1)
        FinChargement()
    End Sub

    Private Sub BtDiagrammeGantt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtDiagrammeGantt.Click
        Disposer_form(DiagrammeGantt)
        FinChargement()
    End Sub

    Private Sub BtRessourcesActivite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRessourcesActivite.Click
        DebutChargement()
        Disposer_form(FichesActivitesEtape2)
        FinChargement()
    End Sub

    Private Sub BtProprieteActivite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtProprieteActivite.Click
        Disposer_form(FichesActivitesEtape3)
        FinChargement()
    End Sub

    Private Sub btPPM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btPPM.Click
        query = "SELECT ModePlanMarche FROM t_paramtechprojet WHERE CodeProjet='" & ProjetEnCours & "'"
        Dim ModePPM As String = ExecuteScallar(query)
        If ModePPM = "" Then
            SuccesMsg("Veuillez choisir le mode d'élaboration du plan dans les paramètres.")
            Exit Sub
        End If
        DebutChargement()
        Disposer_form(PlanMarche)
        FinChargement()
    End Sub

    Private Sub BtNewDAO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtNewDAO.Click
        DebutChargement()
        Disposer_form(NewDao)
        FinChargement()
    End Sub

    Private Sub BtRetraitDepotDAO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRetraitDepotDAO.Click
        DebutChargement()
        Disposer_form(RetraitEtDepotDAO)
        FinChargement()
    End Sub

    Private Sub BtOuvertureOffres_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtOuvertureOffres.Click
        Disposer_form(OuvertureOffres)
        FinChargement()
    End Sub

    Private Sub BtNewDP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtNewDP.Click
        DebutChargement()
        Disposer_form(NewDp)
        FinChargement()
    End Sub

    Private Sub BtDepotDP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtDepotDP.Click
        DebutChargement()
        Disposer_form(DepotDP)
        FinChargement()
    End Sub

    Private Sub BtOuvertureProposition_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtOuvertureProposition.Click
        DebutChargement()
        Disposer_form(OuverturePropositionsDp)
        FinChargement()
    End Sub

    Private Sub BtSaisieOffres_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSaisieOffres.Click
        Disposer_form(SaisieOffres)
        FinChargement()
    End Sub

    Private Sub BtAnalyseOffres_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAnalyseOffres.Click
        Disposer_form(JugementOffres)
        FinChargement()
    End Sub

    Private Sub BtEvalConsult_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEvalConsult.Click
        DebutChargement()
        Disposer_form(EvaluationConsultants)
        FinChargement()
    End Sub

    Private Sub BtEditionBudgetaire_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEditionBudgetaire.Click
        DebutChargement()
        Disposer_form(FicheActivite)
        FinChargement()
    End Sub

    Private Sub BtFicheBudgetaire_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtFicheBudgetaire.Click
        Disposer_form(EditionBudgetaire)
        FinChargement()
    End Sub

    Private Sub BtAllocationBudget_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtAllocationBudget.Click
        Dialog_form(RepartitionMontantConvention)
    End Sub

    Private Sub BtPlanDecaiss_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtPlanDecaiss.Click
        Disposer_form(PlanDecaissement)
        FinChargement()
    End Sub

    Private Sub TileEvaluation_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileEvaluation.ItemClick

        ClearMdi.NavBarControl1.ActiveGroup = ClearMdi.GroupEvaluation
        SplitEvaluation.Visible = True
        SplitEvaluation.Dock = DockStyle.Fill
        TuileDemarrage.Visible = False
        Me.Text = "EVALUATION DU PROJET"
    End Sub

    Private Sub BtSuiviNature_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSuiviNature.Click
        DebutChargement()
        Disposer_form(SuiviBudgetCompte)
        FinChargement()
    End Sub

    Private Sub BtFB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtFB.Click
        Dialog_form(Etat_EngBail)
    End Sub

    Private Sub BtAccueilBudget2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAccueilBudget2.Click
        TuileDemarrage.Visible = True
        TuileDemarrage.Dock = DockStyle.Fill
        SplitBudget.Visible = False
        Me.Text = "ACCUEIL"
    End Sub

    Private Sub BtSigfip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSigfip.Click
        DebutChargement()
        Disposer_form(Suivibugetaire_Sigfip)
        FinChargement()
    End Sub

    Private Sub BtRFA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRFA.Click
        DebutChargement()
        Disposer_form(Fiche_annuelle)
        FinChargement()
    End Sub

    Private Sub BtEng_Click(sender As Object, e As System.EventArgs) Handles BtEng.Click
        DebutChargement()
        Disposer_form(liste_engagement)
        FinChargement()
    End Sub

    Private Sub SimpleButton18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton18.Click
        Dialog_form(Etat_engFac)
    End Sub

    Private Sub SimpleButton19_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton19.Click
        Dialog_form(Etat_engPai)
    End Sub

    Private Sub SimpleButton26_Click(sender As System.Object, e As System.EventArgs) Handles btNewAMI.Click
        DebutChargement()
        Disposer_form(NewAmi)
        FinChargement()
    End Sub

    Private Sub SimpleButton20_Click(sender As System.Object, e As System.EventArgs) Handles btOuvrirAMI.Click
        DebutChargement()
        Disposer_form(OuvertureAmi)
        FinChargement()
    End Sub

    Private Sub SimpleButton21_Click(sender As System.Object, e As System.EventArgs) Handles btEvalMI.Click
        DebutChargement()
        ' Disposer_form(EvaluationTDR)
        Disposer_form(RapportEvaluationMI)
        FinChargement()
    End Sub

    Private Sub NewVisibleBouton(ByVal TypeAfficharge As String)
        'Initialiser Mettre tous les boutons a fale

        'Boutons Fourniture/Travaux
        BtNewDAO.Visible = False
        BtRetraitDepotDAO.Visible = False
        BtOuvertureOffres.Visible = False
        BtSaisieOffres.Visible = False
        BtAnalyseOffres.Visible = False

        'Bouton principal
        btConsultant.Visible = False
        BtServiceAutres.Visible = False
        btFournitureTravaux.Visible = False

        'Boutons Consultants
        BtEvalConsult.Visible = False
        BtOuvertureProposition.Visible = False
        BtDepotDP.Visible = False
        BtNewDP.Visible = False
        btEvalMI.Visible = False
        ListeRestrindre.Visible = False
        btOuvrirAMI.Visible = False
        btDepotAMI.Visible = False
        btNewAMI.Visible = False

        'Boutons services autres
        BtSerElaDAO.Visible = False
        BtSerAutRetraiDOA.Visible = False
        BtSerAutrOuvertureOffre.Visible = False
        BtsaSaiOffres.Visible = False
        BtsaAnalyJugemet.Visible = False

        If TypeAfficharge = "Consultants" Then
            btFournitureTravaux.Visible = True
            BtServiceAutres.Visible = True

            BtEvalConsult.Visible = True
            BtOuvertureProposition.Visible = True
            BtDepotDP.Visible = True
            BtNewDP.Visible = True
            btEvalMI.Visible = True
            ListeRestrindre.Visible = True
            btOuvrirAMI.Visible = True
            btDepotAMI.Visible = True
            btNewAMI.Visible = True
        ElseIf TypeAfficharge = "Service Autres" Then
            btConsultant.Visible = True
            btFournitureTravaux.Visible = True

            BtsaAnalyJugemet.Visible = True
            BtsaSaiOffres.Visible = True
            BtSerAutrOuvertureOffre.Visible = True
            BtSerAutRetraiDOA.Visible = True
            BtSerElaDAO.Visible = True
        ElseIf TypeAfficharge = "Travaux/Fournitures" Then
            BtAnalyseOffres.Visible = True
            BtSaisieOffres.Visible = True
            BtOuvertureOffres.Visible = True
            BtRetraitDepotDAO.Visible = True
            BtNewDAO.Visible = True

            btConsultant.Visible = True
            BtServiceAutres.Visible = True
        End If
    End Sub

    Private Sub SimpleButton28_Click(sender As System.Object, e As System.EventArgs) Handles btFournitureTravaux.Click
        NewVisibleBouton("Travaux/Fournitures")
    End Sub

    Private Sub SimpleButton29_Click(sender As System.Object, e As System.EventArgs) Handles btConsultant.Click
        NewVisibleBouton("Consultants")
    End Sub

    Private Sub BtServiceAutres_Click(sender As Object, e As EventArgs) Handles BtServiceAutres.Click
        NewVisibleBouton("Service Autres")
    End Sub

    Private Sub SimpleButton22_Click(sender As System.Object, e As System.EventArgs) Handles btDepotAMI.Click
        DebutChargement()
        Disposer_form(DepotAMI)
        FinChargement()
    End Sub

    Private Sub TilePortefeuille_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs) Handles TilePortefeuille.ItemClick
        Dim URL As String = "http://clearproject.online"
        If DB.Length > 2 Then
            If Mid(DB, 1, 2) = "bd" Then
                URL = "http://" & Mid(DB, 3) & ".clearproject.online"
            End If
        End If
        System.Diagnostics.Process.Start(URL)
    End Sub

    Private Sub TileClearWeb_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs)
        System.Diagnostics.Process.Start("http://pm-projects.net")
    End Sub

    Private Sub btArchivageDocPM_Click(sender As System.Object, e As System.EventArgs) Handles btArchivageDocPM.Click
        PMPochette.Size = New Point(1000, 500)
        ' Dialog_form(PMPochette)
        Dim NewPMPochetteV1 As New PMPochetteV1
        Dialog_form(NewPMPochetteV1)
    End Sub

    Private Sub BtBonCommande_Click(sender As System.Object, e As System.EventArgs) Handles BtBonCommande.Click
        DebutChargement()
        Disposer_form(Liste_boncommande)
        FinChargement()
    End Sub

    Private Sub ListeRestrindre_Click(sender As Object, e As EventArgs) Handles ListeRestrindre.Click
        DebutChargement()
        Disposer_form(ListeRestreindreAMI)
        FinChargement()
    End Sub

    Private Sub BtSerElaDAO_Click(sender As Object, e As EventArgs) Handles BtSerElaDAO.Click
        DebutChargement()
        Disposer_form(NewSA)
        FinChargement()
    End Sub

    Private Sub BtSerAutRetraiDOA_Click(sender As Object, e As EventArgs) Handles BtSerAutRetraiDOA.Click
        DebutChargement()
        Disposer_form(RetraitEtDepotSA)
        FinChargement()
    End Sub

    Private Sub BtSerAutrOuvertureOffre_Click(sender As Object, e As EventArgs) Handles BtSerAutrOuvertureOffre.Click
        DebutChargement()
        Disposer_form(OuvertureOffresSA)
        FinChargement()
    End Sub

    Private Sub BtsaSaiOffres_Click(sender As Object, e As EventArgs) Handles BtsaSaiOffres.Click
        DebutChargement()
        Disposer_form(SaisieOffresSA)
        FinChargement()
    End Sub

    Private Sub BtsaAnalyJugemet_Click(sender As Object, e As EventArgs) Handles BtsaAnalyJugemet.Click
        DebutChargement()
        Disposer_form(JugementOffresSA)
        FinChargement()
    End Sub
End Class