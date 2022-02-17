Imports DevExpress.XtraBars.Helpers
Imports DevExpress.LookAndFeel
Imports DevExpress.Utils.Drawing
Imports DevExpress.UserSkins
Imports DevExpress.XtraBars
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraNavBar

Public Class ClearMdi

    Dim nbCourrier As Decimal = 0
    Dim nomChat As String = ""

    Private Declare Function InternetGetConnectedState Lib "wininet.dll" (ByRef lpdwFlags As Long, ByVal dwReserved As Long) As Long

    Private Sub InitSkins()
        BonusSkins.Register()
        SkinHelper.InitSkinGallery(skinGalleryBarItem, True)
        UserLookAndFeel.Default.SetSkinStyle(SkinActu)

    End Sub

    Public Function GetSkinImage(ByVal userLF As UserLookAndFeel, ByVal sz As Size, ByVal indent As Decimal) As Bitmap
        Dim image As New Bitmap(sz.Width, sz.Height)
        Using g As Graphics = Graphics.FromImage(image)
            Dim info As New StyleObjectInfoArgs(New GraphicsCache(g))
            info.Bounds = New Rectangle(Point.Empty, sz)
            userLF.Painter.Button.DrawObject(info)
            info.Bounds = New Rectangle(indent, indent, sz.Width - indent * 2, sz.Height - indent * 2)
        End Using
        Return image
    End Function

    Private Sub SkinPicture_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SkinPicture.Click

        SkinShop.ShowDialog()
        FinChargement()

    End Sub

    Private Sub ClearMdi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        'On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        CodeAcces.Close()
        InitSkins()
        NavBarControl1.OptionsNavPane.NavPaneState = DevExpress.XtraNavBar.NavPaneState.Collapsed
        RibbonControl1.Pages(0).Groups(0).Visible = False
        LaDevise = "F CFA"
        BarListItem1.ShowChecks = False
        BarListItem2.ShowChecks = False
        BarListItem3.ShowChecks = False

        exercice()

        Me.Text = Application.ProductName
        Me.BarListItem2.Strings.Clear()
        Me.BarListItem2.Strings.AddRange(New Object() {"Zone géographique", "Devise", "Jours fériés", "Bailleurs de fonds", "Catégorie de dépense", "Composantes", "Sous composantes", "Directions", "Services", "Fonctions", "Plan comptable", "Journaux", "Compte SIGFIP", "Seuil et Revue", "Méthode de Passation", "Types de courrier", "Conventions"})

        documents()

        Demarrage.MdiParent = Me
        Demarrage.Show()

        Me.Cursor = Cursors.Default
        ' BackChercheCourrier.RunWorkerAsync()
        Miseajourcourrier()
        'Actualisation()

        Timer1.Interval = 10000
        Timer1.Start()

        If ProjetEnCours = "PDIC" Then
            'Demarrage.BtSigfip.Visible = False
            'Demarrage.BtLOP.Visible = False
            'Demarrage.BtDRF.Visible = False
            'NavSuiviSigfip.Visible = False
        End If
    End Sub

    Public Sub exercice()
        query = "select * from t_comp_exercice ORDER BY datedebut DESC"
        BarListItem1.Strings.Clear()
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        Dim cpte As Decimal = 0
        For Each rw As DataRow In dt0.Rows
            BarListItem1.Strings.Add(rw(1).ToString)
            If rw("Encours") = 1 Then
                BarListItem1.DataIndex = cpte
            End If
            cpte += 1
        Next

        ExceptRevue = "..."
        myData = "01"
        Dim Niveau As String = "Niveau 0"
        Dim foncOp As String = "..."

        query = "select PhotoOperateur, CodeOperateur, NomOperateur, PrenOperateur, AccesOperateur, FonctionOperateur from T_Operateur where UtilOperateur='" & CodeUtilisateur & "' and CodeProjet='" & ProjetEnCours & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            myData = rw(0).ToString
            ExceptRevue = MettreApost(rw(2).ToString & " " & rw(3).ToString)
            Niveau = rw(4).ToString
            foncOp = MettreApost(rw(5).ToString)
        Next

        Dim monImage As String = line & "\Photos\" & myData
        If File.Exists(monImage) Then
            Dim fichier As FileStream = New FileStream(monImage, FileMode.Open)
            PictureEdit1.Image = Image.FromStream(fichier)
            fichier.Close()
        Else
            PictureEdit1.Image = Image.FromFile(line & "\employe.png")
        End If
        PictureEdit1.ToolTip = ExceptRevue

        'On remplir l'exercice comptable par défaut

        query = "select * from t_comp_exercice where encours='1'"
        ExerciceComptable = ExcecuteSelectQuery(query)
        BarProjet.Caption = "PROJET EN COURS : " & ProjetEnCours.ToUpper & "    |    Niveau d'Accès : " & Niveau.ToUpper & " (" & foncOp & ")  |  " & ExerciceComptable.Rows(0).Item("libelle")

    End Sub

    Private Sub documents()

        query = "select * from t_typedoc"
        BarListItem3.Strings.Clear()
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            BarListItem3.Strings.Add(MettreApost(rw(1).ToString))
        Next

    End Sub

    'Private Sub ClearMdi_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    '    'rendre l'operateur actif
    '    query = "update t_operateur set opinterne='0' where codeoperateur='" & CodeOperateurEnCours.ToString & "'"
    '    ExecuteNonQuery(query)
    '    While BackChercheCourrier.IsBusy = True
    '    End While
    '    Application.Exit()
    'End Sub

    Private Sub Actualisation()

        If InternetGetConnectedState(0&, 0&) Then
            LblInt.Text = "Accès Internet"
            Internet100.Visible = True
            Internet75.Visible = False
            Internet50.Visible = False
            Internet25.Visible = False
        Else
            LblInt.Text = "Pas d'Internet"
            Internet25.Visible = True
            Internet50.Visible = False
            Internet75.Visible = False
            Internet100.Visible = False
        End If

        If (nbCourrier > 0) Then
            BtAlerteCourrier.Enabled = True
            LblAlerteCourrier.Text = nbCourrier.ToString & " Courrier" & IIf(nbCourrier > 1, "s", "").ToString & " reçu" & IIf(nbCourrier > 1, "s", "").ToString
        Else
            BtAlerteCourrier.Enabled = False
            LblAlerteCourrier.Text = "..............."
        End If

        If (nomChat <> "") Then
            BtAlertChat.Enabled = True
            LblAlertChat.Text = NomDe(nomChat).Split(" "c)(0) & " a écrit"
        Else
            BtAlertChat.Enabled = False
            LblAlertChat.Text = "..............."
        End If

        If (BackChercheCourrier.IsBusy = False) Then
            BackChercheCourrier.RunWorkerAsync()
        End If

    End Sub

    Private Sub Miseajourcourrier()

        nbCourrier = 0
        nomChat = ""


        query = "select Count(*) from T_Courrier as C, T_SuiviRetourCourrier as S where C.CodeCourrier=S.CodeCourrier and S.DateRetour<>'' and S.DateFinTraitement='' and C.CodeProjet='" & ProjetEnCours & "' and S.CodeOperateur='" & CodeOperateurEnCours & "' and C.DateLecture=''"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        Dim rw As DataRow = dt0.Rows(0)
        nbCourrier = CInt(rw(0))

        query = "select Count(*) from T_Courrier as C, T_SuiviTraitementCourrier as S where C.CodeCourrier=S.CodeCourrier and S.DateDebut<>'' and S.DateFin='' and C.CodeMarche='0' and C.CodeProjet='" & ProjetEnCours & "' and S.CodeOperateur='" & CodeOperateurEnCours & "' and C.CodeCourrier not in (select CodeCourrier from T_SuiviRetourCourrier where DateFinTraitement='') and C.DateLecture=''"
        dt0 = ExcecuteSelectQuery(query)
        rw = dt0.Rows(0)
        nbCourrier = nbCourrier + CInt(rw(0))

        query = "select Count(*) from T_Courrier as C, T_PlanMarche as P where C.CodeMarche=P.RefMarche and P.DebutEffectif<>'' and P.FinEffective='' and C.CodeProjet='" & ProjetEnCours & "' and P.CodeOperateur='" & CodeOperateurEnCours & "' and C.CodeCourrier not in (select CodeCourrier from T_SuiviRetourCourrier where DateFinTraitement='') and C.DateLecture=''"
        dt0 = ExcecuteSelectQuery(query)
        rw = dt0.Rows(0)
        nbCourrier = nbCourrier + CInt(rw(0))

        query = "select Count(*) from T_Courrier where CodeProjet='" & ProjetEnCours & "' and Destinataire='" & CodeOperateurEnCours & "' and Rayon='' and DateEnvoi<>'' and CodeMarche='0' and DateLecture=''"
        dt0 = ExcecuteSelectQuery(query)
        rw = dt0.Rows(0)
        nbCourrier = nbCourrier + CInt(rw(0))

        query = "select Exped from T_Chat_Msge where Destin='" & CodeOperateurEnCours & "' and Vu='N' order by DateChat"
        dt0 = ExcecuteSelectQuery(query)
        If dt0.Rows.Count > 0 Then
            For Each rw0 As DataRow In dt0.Rows
                nomChat = rw0(0).ToString
            Next
        End If
    End Sub

    Private Sub BtBailleur_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtBailleur.ItemClick
        Dialog_form(Bailleur)
    End Sub

    Private Sub BtConvention_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtConvention.ItemClick
        Dialog_form(SourceFinancement)
    End Sub

    Private Sub BtDevise_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtDevise.ItemClick
        Dialog_form(Devise)
    End Sub

    Private Sub NavPrevActiv_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavPrevActiv.LinkClicked
        Disposer_form(FichesActivitesEtape1)
        FinChargement()
    End Sub

    Private Sub NavGantt_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavGantt.LinkClicked
        Disposer_form(DiagrammeGantt)
        FinChargement()
    End Sub

    Private Sub NavRess_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavRess.LinkClicked
        Disposer_form(FichesActivitesEtape2)
        FinChargement()
    End Sub

    Private Sub NavPropActiv_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavPropActiv.LinkClicked
        Disposer_form(FichesActivitesEtape3)
        FinChargement()
    End Sub

    Private Sub NavPPM_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavPPM.LinkClicked
        Disposer_form(PlanMarche)
        FinChargement()
    End Sub

    Private Sub NavDAO_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavDAO.LinkClicked
        Disposer_form(NewDao)
        FinChargement()
    End Sub

    Private Sub BtFournis_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtFournis.ItemClick
        Disposer_form(RattrapFournisseur)
        FinChargement()
    End Sub

    Private Sub BtRattrapRglt_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtRattrapRglt.ItemClick
        Disposer_form(RattrapReglement)
        FinChargement()
    End Sub

    Private Sub NavDp_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavDp.LinkClicked
        Disposer_form(NewDp)
        FinChargement()
    End Sub

    Private Sub NavRetraitDepotDAO_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavRetraitDepotDAO.LinkClicked
        Disposer_form(RetraitEtDepotDAO)
        FinChargement()
    End Sub

    Private Sub NavDepotPropo_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavDepotPropo.LinkClicked
        Disposer_form(DepotDP)
        FinChargement()
    End Sub

    Private Sub NavOuverture_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavOuverture.LinkClicked
        Disposer_form(OuvertureOffres)
        FinChargement()
    End Sub

    Private Sub NavOuvertPropo_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavOuvertPropo.LinkClicked
        Disposer_form(OuverturePropositions)
        FinChargement()
    End Sub

    Private Sub NavJugementOffre_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavJugementOffre.LinkClicked
        Disposer_form(JugementOffres)
        FinChargement()
    End Sub

    Private Sub RibbonControl1_SelectedPageChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RibbonControl1.SelectedPageChanged
        If (RibbonControl1.SelectedPage Is RibbonControl1.Pages(0)) Then
            PnlMenuPrincipal.Visible = True
        Else
            PnlMenuPrincipal.Visible = False
        End If
    End Sub

    Private Sub BarCompo_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarCompo.ItemClick
        Dialog_form(Composante)
    End Sub

    Private Sub BarSousCompo_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarSousCompo.ItemClick
        Dialog_form(SousComposante)
    End Sub

    Private Sub BtPlanComptable_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtPlanComptable.ItemClick
        Dialog_form(compte_general)
    End Sub

    Private Sub BtJournal_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtJournal.ItemClick
        Dialog_form(Journal)
    End Sub

    Private Sub BtParamEtatCompte_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtParamEtatCompte.ItemClick
        Dialog_form(Parametrage)
    End Sub

    Private Sub BtGeo_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtGeo.ItemClick
        Dialog_form(Zonegeo)
    End Sub

    Private Sub BtIndicateur_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtIndicateur.ItemClick
        Dialog_form(IndicateursEtUnites)
    End Sub

    Private Sub BtMethode_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtMethode.ItemClick
        Dialog_form(SaisieMethodes)
    End Sub

    Private Sub BtEtape_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtEtape.ItemClick
        Dialog_form(EtapeMarche)
    End Sub

    Private Sub BtSeuil_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtSeuil.ItemClick
        Dialog_form(SeuilRevue)
    End Sub

    Private Sub BtService_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtService.ItemClick
        Dialog_form(Service)
    End Sub

    Private Sub BtFonction_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtFonction.ItemClick
        Dialog_form(Fonction)
    End Sub

    Private Sub NavEditionBudget_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavEditionBudget.LinkClicked
        Disposer_form(EditionBudgetaire)
        FinChargement()
    End Sub

    Private Sub BtTypeRepartition_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtTypeRepartition.ItemClick
        Dialog_form(TypeRepartitionBudget)
    End Sub

    Private Sub BtDecoupAdmin_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtDecoupAdmin.ItemClick
        Dialog_form(DivisionAdministrative)
    End Sub

    Private Sub BtCompteMarche_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtCompteMarche.ItemClick
        Dialog_form(CompteA_MarcheEtTypeMarche)
    End Sub

    Private Sub BtGestionAcces_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtGestionAcces.ItemClick
        Dialog_form(GroupUtils)
    End Sub

    Private Sub BtAcces_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtAcces.ItemClick
        Dialog_form(AccesEtOperateur)
    End Sub

    Private Sub BtChangeMdp_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtChangeMdp.ItemClick
        Dialog_form(MonMotDePasse)
    End Sub

    Private Sub BarConfigMail_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarConfigMail.ItemClick
        Dialog_form(ConfigMail)
    End Sub

    Private Sub BarConfigSms_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarConfigSms.ItemClick
        Dialog_form(ConfigSms)
    End Sub

    Private Sub NavRepartActiv_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavRepartActiv.LinkClicked
        Disposer_form(PlanDecaissement)
        FinChargement()
    End Sub

    Private Sub NavEtatBudget_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavEtatBudget.LinkClicked
        DebutChargement()
        Disposer_form(FicheActivite)
        FinChargement()
    End Sub

    Private Sub SkinPicture_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles SkinPicture.MouseDown
        SkinPicture.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
    End Sub

    Private Sub SkinPicture_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles SkinPicture.MouseUp
        SkinPicture.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    End Sub

    Private Sub NavEvalCons_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavEvalCons.LinkClicked
        Disposer_form(EvaluationConsultants)
        FinChargement()
    End Sub

    Private Sub BtCompteTiers_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtCompteTiers.ItemClick
        Dialog_form(Plan_tiers)
    End Sub

    Private Sub NavSuiviNature_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavSuiviNature.LinkClicked
        Disposer_form(SuiviBudgetCompte)
        FinChargement()
    End Sub

    Private Sub BtCompteBancaire_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtCompteBancaire.ItemClick
        Dialog_form(CompteBailleur)
    End Sub

    Private Sub BarPS_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarPS.ItemClick
        Dialog_form(Parametre_Sigfip)
    End Sub

    Private Sub NavFAA_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavFAA.LinkClicked
        Disposer_form(Fiche_annuelle)
        FinChargement()
    End Sub

    Private Sub NavSuiviSigfip_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavSuiviSigfip.LinkClicked
        Disposer_form(Suivibugetaire_Sigfip)
        FinChargement()
    End Sub

    Private Sub BtNexercice_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtNexercice.ItemClick
        Dialog_form(Nouvel_exercice)
    End Sub

    Private Sub BtCjournaux_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtCjournaux.ItemClick
        Dialog_form(Cloture_journaux)
    End Sub

    Private Sub BtCexercice_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtCexercice.ItemClick
        Dialog_form(Cloture_Exercice)
    End Sub

    Private Sub BarCateg_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarCateg.ItemClick
        Dialog_form(CategorieDepense)
    End Sub

    Private Sub BarButtonItem6_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem6.ItemClick
        Dialog_form(jour_ferie)
    End Sub

    Private Sub BarButtonItem8_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem8.ItemClick
        Dialog_form(HeureTravail)
    End Sub

    Private Sub BarListItem1_ListItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ListItemClickEventArgs) Handles BarListItem1.ListItemClick
        If Not Access_Btn("BtnLstExercices") Then
            BarListItem1.ShowChecks = False
            Exit Sub
        End If

        Try
            If BarListItem1.ShowMenuCaption = True Then
                Dim indexExercice As Integer = BarListItem1.DataIndex

                Try
                    If BarListItem1.Strings(BarListItem1.DataIndex) = ExerciceComptable.Rows(0).Item("libelle") Then
                        Exit Sub
                    End If
                Catch ex As Exception

                End Try
                'query = "select count(*) from t_operateur where opinterne=1"
                'Dim nbre As Decimal = Val(ExecuteScallar(query))

                'If nbre > 1 Then
                '    MsgBox("Plusieurs Utilisateurs sont connectés, ils doivent se déconnecter avant de pouvoir changer d'exercice")
                'Else
                Dim repon As Boolean = False
                Dim ExerciceLibelle As String = BarListItem1.Strings(BarListItem1.DataIndex)
                If Me.MdiChildren.Length > 1 Then
                    If MessageBox.Show("Voulez-vous basculer sur " & ExerciceLibelle & "?" & vbNewLine & "Tous les onglets ouverts seront fermés.", "ClearProject", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        repon = True
                    End If
                Else
                    repon = True
                End If
                If repon Then
                    For Each child As Object In Me.MdiChildren
                        If child.Name <> "Demarrage" Then
                            child.Close()
                        End If
                    Next
                    query = "select * from t_comp_exercice where libelle='" & ExerciceLibelle & "'"
                    ExerciceComptable = ExcecuteSelectQuery(query)

                    Datedebut.Text = ExerciceComptable.Rows(0).Item("datedebut")
                    DateFin.Text = ExerciceComptable.Rows(0).Item("datefin")

                    Dim str2(3) As String
                    str2 = Datedebut.Text.Split("/")
                    Dim tempdt2 As String = String.Empty
                    For j As Integer = 2 To 0 Step -1
                        tempdt2 += str2(j) & "-"
                    Next
                    tempdt2 = tempdt2.Substring(0, 10)

                    Dim str3(3) As String
                    str3 = DateFin.Text.Split("/")
                    Dim tempdt3 As String = String.Empty
                    For j As Integer = 2 To 0 Step -1
                        tempdt3 += str3(j) & "-"
                    Next
                    tempdt3 = tempdt3.Substring(0, 10)

                    query = "update T_COMP_LIGNE_ECRITURE set Etat='0' where DATE_lE >='" & tempdt2 & "' and DATE_LE <='" & tempdt3 & "'"
                    ExecuteNonQuery(query)


                    'titre du projet

                    Dim libelle_check As String = ExerciceComptable.Rows(0).Item("libelle")

                    ExceptRevue = "..."
                    myData = "01"
                    Dim Niveau As String = "Niveau 0"
                    Dim foncOp As String = "..."

                    query = "select PhotoOperateur, CodeOperateur, NomOperateur, PrenOperateur, AccesOperateur, FonctionOperateur from T_Operateur where UtilOperateur='" & CodeUtilisateur & "' and CodeProjet='" & ProjetEnCours & "'"
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        myData = rw(0).ToString
                        ExceptRevue = MettreApost(rw(2).ToString & " " & rw(3).ToString)
                        Niveau = rw(4).ToString
                        foncOp = MettreApost(rw(5).ToString)
                    Next


                    BarProjet.Caption = "PROJET EN COURS : " & ProjetEnCours.ToUpper & "    |    Niveau d'Accès : " & Niveau.ToUpper & " (" & foncOp & ")  |  " & libelle_check

                Else

                End If
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub BarButtonItem9_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem9.ItemClick
        Dialog_form(Comparaison_Etats_Financier)
    End Sub

    Private Sub BtProjet_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtProjet.ItemClick
        query = "Select * from t_projet where codeprojet='" & ProjetEnCours & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            ProjetClear.TxtIdentifiant.Text = rw(15).ToString
            ProjetClear.TxtAbrege.Text = rw(0).ToString
            ProjetClear.TxtIntitule.Text = MettreApost(rw(1).ToString)
            ProjetClear.TxtDateDebutMO.Text = rw(7).ToString
            ProjetClear.TxtDateFinMO.Text = rw(8).ToString
            ProjetClear.TxtDateDebutMV.Text = rw(9).ToString
            ProjetClear.TxtDateFinMV.Text = rw(10).ToString
            ProjetClear.ComboPays.Text = MettreApost(rw(18).ToString)
            ProjetClear.TxtAdresse.Text = MettreApost(rw(2).ToString)
            ProjetClear.TxtBp.Text = MettreApost(rw(19).ToString)
            ProjetClear.TxtTelCoordo.Text = rw(3).ToString
            ProjetClear.TxtFaxCoordo.Text = rw(4).ToString
            ProjetClear.TxtMailCoordo.Text = rw(5).ToString
            ProjetClear.TxtSiteWeb.Text = rw(6).ToString
            ProjetClear.TxtMinistere.Text = MettreApost(rw(20).ToString)
            ProjetClear.PbLogoProjet.Image = Bitmap.FromStream(New MemoryStream(CType(rw(22), Byte())))
        Next

        'insertion des indicateurs pour les pays
        query = "Select * from t_zonegeo where libellezone='" & EnleverApost(ProjetClear.ComboPays.Text) & "'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            ProjetClear.TxtIndic2.Text = rw(4).ToString
            ProjetClear.TxtIndic1.Text = rw(4).ToString
        Next
        Dialog_form(ProjetClear)

    End Sub

    Private Sub BtSupl_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtSupl.ItemClick
        Dialog_form(SuiteProjet)
    End Sub

    Private Sub BarListItem2_ListItemClick(sender As System.Object, e As DevExpress.XtraBars.ListItemClickEventArgs) Handles BarListItem2.ListItemClick
        If Not Access_Btn("BtnLstEtats") Then
            Exit Sub
        End If

        Try
            Dim inde As Integer = BarListItem2.DataIndex
            If BarListItem2.Strings(inde) = "Zone géographique" Then
                Dim devise As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table
                Dim Chemin As String = lineEtat & "\Parametres\"

                Dim DatSet = New DataSet
                devise.Load(Chemin & "zonegeo.rpt")

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = devise.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                devise.SetDataSource(DatSet)
                devise.SetParameterValue("CodeProjet", ProjetEnCours)
                FullScreenReport.FullView.ReportSource = devise
                FinChargement()
                FullScreenReport.ShowDialog()
            ElseIf BarListItem2.Strings(inde) = "Devise" Then
                Dim devise As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table
                Dim Chemin As String = lineEtat & "\Parametres\"

                Dim DatSet = New DataSet
                devise.Load(Chemin & "Etat_Devise.rpt")

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = devise.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                devise.SetDataSource(DatSet)
                devise.SetParameterValue("CodeProjet", ProjetEnCours)
                FullScreenReport.FullView.ReportSource = devise
                FinChargement()
                FullScreenReport.ShowDialog()

            ElseIf BarListItem2.Strings(inde) = "Jours fériés" Then
                Imprim_ferier.ShowDialog()
            ElseIf BarListItem2.Strings(inde) = "Bailleurs de fonds" Then
                Dim bailleur As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table
                Dim Chemin As String = lineEtat & "\Parametres\"

                Dim DatSet = New DataSet
                bailleur.Load(Chemin & "Bailleur_Fonds.rpt")

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = bailleur.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                bailleur.SetDataSource(DatSet)
                bailleur.SetParameterValue("CodeProjet", ProjetEnCours)
                FullScreenReport.FullView.ReportSource = bailleur
                FinChargement()
                FullScreenReport.ShowDialog()
            ElseIf BarListItem2.Strings(inde) = "Catégorie de dépense" Then
                Dim categoriedepense As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table
                Dim Chemin As String = lineEtat & "\Parametres\"

                Dim DatSet = New DataSet
                categoriedepense.Load(Chemin & "CategorieDepense.rpt")

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = categoriedepense.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                categoriedepense.SetDataSource(DatSet)
                categoriedepense.SetParameterValue("CodeProjet", ProjetEnCours)
                FullScreenReport.FullView.ReportSource = categoriedepense
                FinChargement()
                FullScreenReport.ShowDialog()

            ElseIf BarListItem2.Strings(inde) = "Composantes" Then
                Dim reportCompo As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table

                'DebutChargement(True, "Le traitement de votre demande est en cours...")
                Dim Chemin As String = lineEtat & "\CompoActivites\"

                Dim DatSet = New DataSet
                reportCompo.Load(Chemin & "EtatCompo.rpt")

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = reportCompo.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                reportCompo.SetDataSource(DatSet)
                reportCompo.SetParameterValue("CodeProjet", ProjetEnCours)

                FullScreenReport.FullView.ReportSource = reportCompo
                FullScreenReport.ShowDialog()

            ElseIf BarListItem2.Strings(inde) = "Sous composantes" Then

                ' Affichage état ***************************
                Dim reportSCompo As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table

                Dim Chemin As String = lineEtat & "\CompoActivites\"

                Dim DatSet = New DataSet
                reportSCompo.Load(Chemin & "Etat_Sous_Compo.rpt")

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = reportSCompo.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                reportSCompo.SetDataSource(DatSet)
                reportSCompo.SetParameterValue("CodeProjet", ProjetEnCours)

                FullScreenReport.FullView.ReportSource = reportSCompo
                FullScreenReport.ShowDialog()

            ElseIf BarListItem2.Strings(inde) = "Directions" Then

                Dim reportConvention As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table

                DebutChargement(True, "Le traitement de votre demande est en cours...")
                Dim Chemin As String = lineEtat & "\Parametres\ListeDivisionAdministrative.rpt"

                Dim DatSet = New DataSet
                reportConvention.Load(Chemin)

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = reportConvention.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                reportConvention.SetDataSource(DatSet)
                reportConvention.SetParameterValue("CodeProjet", ProjetEnCours)
                FullScreenReport.FullView.ReportSource = reportConvention
                FinChargement()
                FullScreenReport.ShowDialog()

            ElseIf BarListItem2.Strings(inde) = "Services" Then

                Dim reportConvention As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table

                DebutChargement(True, "Le traitement de votre demande est en cours...")
                Dim Chemin As String = lineEtat & "\Parametres\ListeServices.rpt"

                Dim DatSet = New DataSet
                reportConvention.Load(Chemin)

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = reportConvention.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                reportConvention.SetDataSource(DatSet)
                reportConvention.SetParameterValue("CodeProjet", ProjetEnCours)
                FullScreenReport.FullView.ReportSource = reportConvention
                FinChargement()
                FullScreenReport.ShowDialog()

            ElseIf BarListItem2.Strings(inde) = "Fonctions" Then

                Dim reportConvention As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table
                DebutChargement(True, "Le traitement de votre demande est en cours...")
                Dim Chemin As String = lineEtat & "\Parametres\ListeFonctions.rpt"

                Dim DatSet = New DataSet
                reportConvention.Load(Chemin)

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = reportConvention.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                reportConvention.SetDataSource(DatSet)
                reportConvention.SetParameterValue("CodeProjet", ProjetEnCours)
                FullScreenReport.FullView.ReportSource = reportConvention
                FinChargement()
                FullScreenReport.ShowDialog()
            ElseIf BarListItem2.Strings(inde) = "Plan comptable" Then
                Etat_plancomptable.ShowDialog()

            ElseIf BarListItem2.Strings(inde) = "Journaux" Then
                Etat_parametres.ShowDialog()

            ElseIf BarListItem2.Strings(inde) = "Compte SIGFIP" Then
                Etat_sigfip.ShowDialog()

            ElseIf BarListItem2.Strings(inde) = "Seuil et Revue" Then
                Dim reportSeuil As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table
                Dim chemin As String = lineEtat & "\Marches\"

                Dim DatSet = New DataSet
                reportSeuil.Load(chemin & "SeuilMarches.rpt")

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = reportSeuil.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                reportSeuil.SetDataSource(DatSet)
                reportSeuil.SetParameterValue("CodeProjet", ProjetEnCours)
                FullScreenReport.FullView.ReportSource = reportSeuil
                FullScreenReport.ShowDialog()

            ElseIf BarListItem2.Strings(inde) = "Méthode de Passation" Then
                Dim reportMethod As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table
                Dim chemin As String = lineEtat & "\Marches\"
                Dim DatSet = New DataSet
                reportMethod.Load(chemin & "ListeMethodes.rpt")

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = reportMethod.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                reportMethod.SetDataSource(DatSet)
                reportMethod.SetParameterValue("CodeProjet", ProjetEnCours)
                FullScreenReport.FullView.ReportSource = reportMethod
                FullScreenReport.ShowDialog()

            ElseIf BarListItem2.Strings(inde) = "Conventions" Then
                Dim reportMethod As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table
                Dim chemin As String = lineEtat & "\Parametres\"
                Dim DatSet = New DataSet
                reportMethod.Load(chemin & "Convention.rpt")

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = reportMethod.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                'date
                'Dim dateconv As Date
                'query = "select datedebut, datefin from T_COMP_EXERCICE where encours='1'"
                'Dim dt As DataTable = ExcecuteSelectQuery(query)
                'For Each rw As DataRow In dt.Rows
                '    dateconv = CDate(rw(0)).ToString("dd/MM/yyyy")
                'Next

                reportMethod.SetDataSource(DatSet)
                reportMethod.SetParameterValue("CodeProjet", ProjetEnCours)
                'reportMethod.SetParameterValue("DateConv", dateconv.ToString)
                FullScreenReport.FullView.ReportSource = reportMethod
                FullScreenReport.ShowDialog()

            ElseIf BarListItem2.Strings(inde) = "Types de courrier" Then
                Dim reportMethod As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table
                Dim chemin As String = lineEtat & "\Parametres\"
                Dim DatSet = New DataSet
                reportMethod.Load(chemin & "TypeCourriers.rpt")

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = reportMethod.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                reportMethod.SetDataSource(DatSet)
                reportMethod.SetParameterValue("CodeProjet", ProjetEnCours)
                FullScreenReport.FullView.ReportSource = reportMethod
                FullScreenReport.ShowDialog()
            End If
        Catch ex As Exception
            FailMsg("Une erreur empêche l'impression")
        End Try

    End Sub

    Private Sub BarButtonItem10_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem10.ItemClick
        Dialog_form(Manuels)
    End Sub

    Private Sub BarListItem3_ListItemClick(sender As System.Object, e As DevExpress.XtraBars.ListItemClickEventArgs) Handles BarListItem3.ListItemClick
        Afficher_doc.ShowDialog()
    End Sub

    Private Sub BarButtonItem12_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem12.ItemClick
        If File.Exists(line & "\Manuel\Manuel.pdf") Then
            Process.Start(line & "\Manuel\Manuel.pdf")
        End If
    End Sub

    Private Sub NavEng_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavEng.LinkClicked
        Disposer_form(liste_engagement)
        FinChargement()
    End Sub

    Private Sub NavSituationDep_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavSituationDep.LinkClicked
        Dialog_form(Etat_engFac)
    End Sub

    Private Sub NavSituationPaie_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavSituationPaie.LinkClicked
        Dialog_form(Etat_engPai)
    End Sub

    Private Sub NavElabTDR_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavElabTDR.LinkClicked
        Disposer_form(NewAmi)
        FinChargement()
    End Sub

    Private Sub NavDepotAMI_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavDepotAMI.LinkClicked
        Disposer_form(DepotAMI)
        FinChargement()
    End Sub

    Private Sub NavOuvAMI_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavOuvAMI.LinkClicked
        Disposer_form(OuvertureAmi)
        FinChargement()
    End Sub

    Private Sub NavEvaTDR_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavEvaTDR.LinkClicked
        Disposer_form(EvaluationTDR)
        FinChargement()
    End Sub

    Private Sub NavSaisieOffre_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavSaisieOffre.LinkClicked
        Disposer_form(SaisieOffres)
        FinChargement()
    End Sub

    Private Sub BarTypeImmo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarTypeImmo.ItemClick
        parametre = "Type Immobilisation"
        paramètre.GroupControl1.Text = parametre
        Dialog_form(paramètre)
    End Sub

    Private Sub BarButtonItem16_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem16.ItemClick
        Dialog_form(PMPochetteDocument)
    End Sub

    Private Sub BarButtonItem18_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem18.ItemClick
        Dialog_form(LaisonEtat)
    End Sub

    Private Sub NavFinancement_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavFinancement.LinkClicked
        Dialog_form(Etat_EngBail)
    End Sub

    Private Sub BtPret_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtPret.ItemClick
        Dialog_form(TypeTiers)
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Actualisation()
    End Sub

    Private Sub NavArchivage_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavArchivage.LinkClicked
        Dialog_form(PMPochette)
    End Sub

    Private Sub ClearMdi_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Application.ExitThread()
        Application.Exit()
    End Sub

    Private Sub btCorrespSigFip_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btCorrespSigFip.ItemClick
        Dialog_form(Comparation_SigFip)
    End Sub

    Private Sub BtTiers_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BtTiers.ItemClick
        Dialog_form(TypeTiers)
    End Sub

    Private Sub BtRessourcesRSf_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BtRessourcesRSf.ItemClick
        Dialog_form(RessourcesRSF)
    End Sub

    Private Sub BtExerciceParDefaut_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BtExerciceParDefaut.ItemClick
        Dialog_form(Exercice_Par_Defaut)
    End Sub

    Private Sub BarTauxAppli_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarTauxAppli.ItemClick
        Dialog_form(ParametreTauxAppli)
    End Sub

    Private Sub BtMonaiDevisVersion1_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtMonaiDevisVersion1.ItemClick
        DebutChargement()
        Dialog_form(DeviseVersion1)
        FinChargement()
    End Sub

    Private Sub ElaboPPM_ItemClick(sender As Object, e As ItemClickEventArgs) Handles ElaboPPM.ItemClick
        Dialog_form(ModePlanMarche)
    End Sub
    Private Sub ResponsableEtape_ItemClick(sender As Object, e As ItemClickEventArgs) Handles RespEtape.ItemClick
        Dialog_form(ResponsableEtape)
    End Sub

    Private Sub BtSignataire_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BtSignataire.ItemClick
        DebutChargement(True, "Chargement des signataires en cours...")
        Dialog_form(ListesSignataires)
    End Sub

    Private Sub BtResposblePM_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BtResposblePM.ItemClick
        Dialog_form(ResponsablePM)
    End Sub
End Class