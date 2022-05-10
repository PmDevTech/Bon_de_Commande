Imports MySql.Data.MySqlClient
Imports ClearProject.PassationMarche
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository

Public Class SaisieOffres
    Dim dtCojo = New DataTable
    Dim dt = New DataTable()
    Dim dt2 = New DataTable()
    Dim Modif = False
    Dim DrX As DataRow

    Private Sub SaisieOffres_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ItemsCmbDao()
    End Sub

    Private Sub ItemsCmbDao()
        CmbNumDAO.Text = ""
        CmbNumDAO.Properties.Items.Clear()
        query = "select NumeroDAO from T_DAO where DossValider=true and statut_DAO<>'Annulé' and DateFinouverture<>'' and CodeProjet='" & ProjetEnCours & "' ORDER BY DateEdition DESC"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbNumDAO.Properties.Items.Add(MettreApost(rw("NumeroDAO").ToString))
        Next
    End Sub

#Region "Select DAO"
    Private Sub CmbNumDAO_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumDAO.SelectedValueChanged
        InitGbLot()
        RemplirTabSpecDemande(String.Empty)
        GetVisibleGridView("Fournitures")
        BtValiderOffre.Enabled = False
        BtModifier.Enabled = False

        If (CmbNumDAO.SelectedIndex <> -1) Then
            query = "select DateDebutOuverture,IntituleDAO,TypeMarche,NbreLotDAO from T_DAO where CodeProjet='" & ProjetEnCours & "' and NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtDateOuverture.Text = CDate(rw("DateDebutOuverture").ToString).ToShortDateString
                TxtLibelleDAO.Text = MettreApost(rw("IntituleDAO").ToString)
                TxtTypeMarche.Text = rw("TypeMarche").ToString
                TxtNbLot.Text = rw("NbreLotDAO").ToString

                CmbNumLot.ResetText()
                CmbNumLot.Properties.Items.Clear()
                For k As Integer = 1 To CInt(rw("NbreLotDAO"))
                    CmbNumLot.Properties.Items.Add(k.ToString)
                Next
            Next

            Dim NbreSoumis = Val(ExecuteScallar("select Count(*) from T_Fournisseur where CodeProjet='" & ProjetEnCours & "' and NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and DateDepotDAO<>''"))
            TxtNbSoumis.Text = NbreSoumis.ToString

            GetVisibleGridView(TxtTypeMarche.Text)
        End If

    End Sub

    Private Sub GetVisibleGridView(TypeMarche As String)
        GridDQE.Visible = False
        GridSaisieDQE.Visible = False
        GridSpecDemandes.Visible = False
        GridSaisieSpecTech.Visible = False
        GridDQE.DataSource = Nothing
        GridSaisieDQE.DataSource = Nothing
        GridSpecDemandes.DataSource = Nothing
        GridSaisieSpecTech.DataSource = Nothing

        If TypeMarche.ToLower = "Fournitures".ToLower Then
            GridSpecDemandes.Visible = True
            GridSaisieSpecTech.Visible = True
            GbSpecDemandes.Text = "Spécifications demandées"
            GbSpecOffertes.Text = "Spécifications offertes"
            GbArticles.Enabled = True
            GbPrixArticle.Visible = True
            GbSection.Visible = False
        Else
            GridDQE.Visible = True
            GridSaisieDQE.Visible = True
            GbSpecDemandes.Text = "Sommes à valoir"
            GbSpecOffertes.Text = "Dévis quantitatif estimé"
            GbArticles.Enabled = False
            GbPrixArticle.Visible = False
            GbSection.Visible = True
        End If
    End Sub

#End Region

#Region "Select Lot, sous lot et soumisoinnaire"

    Private Sub CmbNumLot_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumLot.SelectedValueChanged
        If CmbNumDAO.SelectedIndex <> -1 Then
            'ZoneAffichage.Controls.Clear()
            InitGbSoumis()

            If (CmbNumLot.Text <> "") Then

                Dim RefDuLot As String = ""
                query = "select LibelleLot,RefLot from T_LotDAO where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeLot='" & CmbNumLot.Text & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    TxtLibelleLot.Text = MettreApost(rw("LibelleLot").ToString)
                    RefDuLot = rw("RefLot").ToString
                Next

                Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDAO.Text)
                Dim nbsouslot As Integer = Val(Resultat(0))
                CmbSousLot.Text = ""
                TxtSousLot.Text = ""

                If (TxtTypeMarche.Text = "Travaux") Then
                    LabelControl18.Text = "Nombre de sections"
                    If nbsouslot > 0 Then
                        CmbSousLot.Enabled = True
                        CmbSousLot.Properties.Items.Clear()
                        Dim dt As DataTable = CType(Resultat(1), DataTable)
                        For Each rw As DataRow In dt.Rows
                            CmbSousLot.Properties.Items.Add(rw("CodeSousLot").ToString)
                        Next
                    Else
                        CmbSousLot.Enabled = False
                        TxtNbSectionArticle.Text = Val(ExecuteScallar("select Count(*) from T_DQESection where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeLot='" & CmbNumLot.Text & "'"))
                    End If
                Else
                    LabelControl18.Text = "Nombre d'articles"
                    If nbsouslot > 0 Then
                        CmbSousLot.Enabled = True
                        CmbSousLot.Properties.Items.Clear()
                        Dim dt As DataTable = CType(Resultat(1), DataTable)
                        For Each rw As DataRow In dt.Rows
                            CmbSousLot.Properties.Items.Add(rw("CodeSousLot").ToString)
                        Next
                    Else
                        CmbSousLot.Enabled = False
                        TxtNbSectionArticle.Text = Val(ExecuteScallar("select Count(*) from T_SpecTechFourniture where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeLot='" & CmbNumLot.Text & "'"))
                    End If
                End If

                'Chargement des soumissionnaires qui ont soumissionné pour ce lot selectionné.
                ItemsNomSoumis()

                'Code de remplissage combo articles
                If (TxtTypeMarche.Text = "Fournitures") Then
                    CmbCodeArticle.ResetText()
                    CmbCodeArticle.Properties.Items.Clear()
                    If nbsouslot = 0 Then
                        CmbCodeArticle.Properties.Items.Clear()
                        query = "select distinct CodeFournit from T_SpecTechFourniture where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeLot='" & CmbNumLot.Text & "' order by CodeFournit"
                        dt0 = ExcecuteSelectQuery(query)
                        For Each rw As DataRow In dt0.Rows
                            CmbCodeArticle.Properties.Items.Add(MettreApost(rw("CodeFournit").ToString))
                        Next
                    End If

                Else ' Travaux ***************

                    CmbCodeSection.ResetText()
                    CmbCodeSection.Properties.Items.Clear()
                    CmbCodeSection.Enabled = False

                    If nbsouslot = 0 Then
                        query = "select NumeroSection from T_DQESection where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeLot='" & CmbNumLot.Text & "' order by NumeroSection"
                        dt0 = ExcecuteSelectQuery(query)
                        For Each rw As DataRow In dt0.Rows
                            CmbCodeSection.Properties.Items.Add(MettreApost(rw("NumeroSection").ToString))
                        Next
                    End If
                End If

                'Sommes à valoir
                'Mi en commentaire par Fodj
                'If (TxtTypeMarche.Text = "Travaux") Then
                '    Dim SomAvaloir As Decimal = 0
                '    Dim nbSyst As Decimal = 0

                '    dt.Columns.Clear()
                '    dt.Columns.Add("N°", Type.GetType("System.String"))
                '    dt.Columns.Add("Spécification", Type.GetType("System.String"))
                '    dt.Columns.Add("Quantité", Type.GetType("System.String"))
                '    dt.Columns.Add("Prix U. HTVA", Type.GetType("System.String"))
                '    dt.Columns.Add("Montant HTVA", Type.GetType("System.String"))
                '    dt.Columns.Add("Prix U. Lettre", Type.GetType("System.String"))
                '    dt.Columns.Add("Syst", Type.GetType("System.String"))
                '    dt.Rows.Clear()

                '    query = "select S.NumeroSection,I.NumeroItem,I.Designation,I.QteItem,I.UniteItem,I.PuHtva,I.MontHtva,I.PuHtvaLettre from T_DQESection as S, T_DQEItem as I where S.RefSection=I.RefSection and S.NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and S.CodeLot='" & CmbNumLot.Text & "' and I.PuHtva='0'"
                '    dt0 = ExcecuteSelectQuery(query)
                '    For Each rw As DataRow In dt0.Rows
                '        'SomAvaloir = SomAvaloir + CDec(rw(6))
                '        nbSyst = nbSyst + 1

                '        'Grid
                '        Dim dr = dt.NewRow()

                '        dr("N°") = rw("NumeroSection").ToString & "." & rw("NumeroItem").ToString
                '        dr("Spécification") = MettreApost(rw("Designation").ToString)
                '        dr("Quantité") = rw("QteItem").ToString & " " & rw("UniteItem").ToString
                '        dr("Prix U. HTVA") = rw("PuHtva").ToString
                '        dr("Montant HTVA") = rw("MontHtva").ToString
                '        dr("Prix U. Lettre") = MettreApost(rw("PuHtvaLettre").ToString)
                '        If (CDec(nbSyst) / 2 = CDec(nbSyst) \ 2) Then dr("Syst") = "x"

                '        dt.Rows.Add(dr)
                '    Next

                '    'GridSpecDemandes.DataSource = dt
                '    'GridViewSpec.Columns(0).Width = 40
                '    'GridViewSpec.Columns(1).Width = 200
                '    'GridViewSpec.Columns(2).Width = 60
                '    'GridViewSpec.Columns(3).Width = 150
                '    'GridViewSpec.Columns(4).Width = 150
                '    'GridViewSpec.Columns(5).Width = 500
                '    'GridViewSpec.Columns(6).Visible = False

                '    ColorRowGrid(GridViewSpec, "[Syst]='x'", Color.Silver, "Tahoma", 8, FontStyle.Regular, Color.Black)
                '    GbSpecDemandes.Text = "Sommes à valoir (" & AfficherMonnaie(SomAvaloir.ToString) & ")"
                'End If

            End If
        End If
    End Sub

    Private Sub CmbSousLot_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSousLot.SelectedValueChanged
        Try
            'ZoneAffichage.Controls.Clear()
            CmbCodeSection.Text = ""
            TxtLibelleSection.Text = ""
            CmbNomSoumis.Text = ""
            TxtNbSectionArticle.Text = ""

            If (CmbSousLot.Text <> "" And CmbNumLot.Text <> "" And CmbNumDAO.SelectedIndex <> -1) Then

                query = "select LibelleSousLot from T_LotDAO_SousLot where CodeSousLot='" & CmbSousLot.Text & "' and NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    TxtSousLot.Text = MettreApost(rw("LibelleSousLot").ToString)
                Next

                If (TxtTypeMarche.Text.ToLower = "Fournitures".ToLower) Then

                    query = "select distinct l.* from T_SpecTechFourniture l, t_lotdao_souslot s where l.NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and l.CodeLot='" & CmbNumLot.Text & "' and s.CodeSousLot='" & CmbSousLot.Text & "' and s.CodeSousLot=l.CodeSousLot"
                    dt0 = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        TxtNbSectionArticle.Text = CInt(dt0.Rows.Count)
                        Exit For
                    Next

                    InitGbArticle()
                    CmbCodeArticle.ResetText()
                    CmbCodeArticle.Properties.Items.Clear()
                    query = "select distinct CodeFournit from T_SpecTechFourniture where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeLot='" & CmbNumLot.Text & "' and CodeSousLot='" & CmbSousLot.Text & "' order by CodeFournit"
                    dt0 = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        CmbCodeArticle.Properties.Items.Add(MettreApost(rw("CodeFournit").ToString))
                    Next

                Else 'Travaux

                    TxtNbSectionArticle.Text = Val(ExecuteScallar("select Count(*) from T_DQESection where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeLot='" & CmbNumLot.Text & "' and CodeSousLot='" & CmbSousLot.Text & "'"))
                    CmbCodeSection.ResetText()
                    CmbCodeSection.Properties.Items.Clear()
                    CmbCodeSection.Enabled = False

                    query = "select NumeroSection from T_DQESection where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeLot='" & CmbNumLot.Text & "' and CodeSousLot='" & CmbSousLot.Text & "' order by NumeroSection"
                    dt0 = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        CmbCodeSection.Properties.Items.Add(MettreApost(rw("NumeroSection").ToString))
                    Next
                End If

                'Charger les soumissionnaires ayant soumissionné pour ce lot et sous lot.
                ItemsNomSoumis()
            End If

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub ItemsNomSoumis()
        Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDAO.Text)
        Dim nbsouslot As Integer = Val(Resultat(0))
        If nbsouslot > 0 Then
            'query = "select distinct F.NomFournis from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and S.CodeLot='" & CmbNumLot.Text & "' AND S.CodeSousLot='" & CmbSousLot.Text & "'"
            query = "select F.CodeFournis, F.NomFournis from T_Fournisseur as F, T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and S.CodeLot='" & CmbNumLot.Text & "' AND S.CodeSousLot='" & CmbSousLot.Text & "' GROUP BY CodeFournis"
        Else
            ' query = "select distinct F.NomFournis from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and S.CodeLot='" & CmbNumLot.Text & "'"
            query = "select F.CodeFournis, F.NomFournis from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and S.CodeLot='" & CmbNumLot.Text & "'  GROUP BY CodeFournis"
        End If

        CmbNomSoumis.ResetText()
        CmbNomSoumis.Properties.Items.Clear()
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            'CmbNomSoumis.Properties.Items.Add(MettreApost(rw("NomFournis").ToString))
            CmbNomSoumis.Properties.Items.Add(GetNewCode(rw("CodeFournis")) & " | " & MettreApost(rw("NomFournis").ToString))
        Next
    End Sub

    Private Sub CmbNomSoumis_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNomSoumis.SelectedValueChanged
        If CmbNumLot.Text <> "" And CmbNumDAO.Text <> "" Then
            RefSoumisCache.Text = ""
            ZoneAffichage.Controls.Clear()
            If (TxtTypeMarche.Text = "Fournitures") Then
                InitGbArticle()
            Else
                CmbCodeSection.Text = ""
                TxtLibelleSection.Text = ""
                GridDQE.DataSource = Nothing
                GridSaisieDQE.DataSource = Nothing
                ZoneAffichage.Controls.Clear()
                CmbCodeSection.Enabled = False
                BtValiderOffre.Enabled = False
                BtModifier.Enabled = False
            End If

            If (CmbNomSoumis.SelectedIndex <> -1) Then
                Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDAO.Text)
                Dim nbsouslot As Integer = Val(Resultat(0))
                If nbsouslot > 0 Then
                    ' query = "select F.PaysFournis,F.AdresseCompleteFournis,F.TelFournis,F.FaxFournis,F.CelFournis,F.MailFournis,S.Monnaie,S.HtHdTtc,S.MontantPropose,S.RefSoumis from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NomFournis='" & EnleverApost(CmbNomSoumis.Text) & "' and F.NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and S.CodeLot='" & CmbNumLot.Text & "'AND S.CodeSousLot='" & CmbSousLot.Text & "'"
                    query = "select F.PaysFournis,F.AdresseCompleteFournis,F.TelFournis,F.FaxFournis,F.CelFournis,F.MailFournis,S.Monnaie,S.HtHdTtc,S.MontantPropose,S.RefSoumis from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.CodeFournis='" & CInt(CmbNomSoumis.Text.Split(" "c)(0)) & "' and F.NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and S.CodeLot='" & CmbNumLot.Text & "'AND S.CodeSousLot='" & CmbSousLot.Text & "'"
                Else
                    ' query = "select F.PaysFournis,F.AdresseCompleteFournis,F.TelFournis,F.FaxFournis,F.CelFournis,F.MailFournis,S.Monnaie,S.HtHdTtc,S.MontantPropose,S.RefSoumis from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NomFournis='" & EnleverApost(CmbNomSoumis.Text) & "' and F.NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and S.CodeLot='" & CmbNumLot.Text & "'"
                    query = "select F.PaysFournis,F.AdresseCompleteFournis,F.TelFournis,F.FaxFournis,F.CelFournis,F.MailFournis,S.Monnaie,S.HtHdTtc,S.MontantPropose,S.RefSoumis from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.CodeFournis='" & CInt(CmbNomSoumis.Text.Split(" "c)(0)) & "' and F.NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and S.CodeLot='" & CmbNumLot.Text & "'"
                End If

                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    TxtPaysSoumis.Text = MettreApost(rw("PaysFournis").ToString)
                    Dim AdrSoumis As String = ""
                    If (rw(1).ToString <> "") Then AdrSoumis = MettreApost(rw(1).ToString)
                    If (rw(2).ToString <> "") Then AdrSoumis = AdrSoumis & vbNewLine & "Tél : " & rw(2).ToString
                    If (rw(3).ToString <> "") Then AdrSoumis = AdrSoumis & vbNewLine & "Fax : " & rw(3).ToString
                    If (rw(4).ToString <> "") Then AdrSoumis = AdrSoumis & vbNewLine & "Cel : " & rw(4).ToString
                    If (rw(5).ToString <> "") Then AdrSoumis = AdrSoumis & vbNewLine & "E-mail : " & rw(5).ToString
                    TxtAdresseSoumis.Text = AdrSoumis
                    TxtMonnaie.Text = rw("Monnaie").ToString
                    TxtTaxes.Text = rw("HtHdTtc").ToString
                    TxtMontantLot.Text = AfficherMonnaie(rw("MontantPropose").ToString)
                    RefSoumisCache.Text = rw("RefSoumis").ToString
                Next

                If (TxtTypeMarche.Text.ToLower = "travaux") Then CmbCodeSection.Enabled = True
            End If
        End If
    End Sub

#End Region

#Region "Initialisation"
    Private Sub InitGbLot()
        CmbNumLot.Text = ""
        CmbNumLot.Properties.Items.Clear()
        TxtNbSectionArticle.Text = ""
        TxtLibelleLot.Text = ""
        CmbSousLot.Text = ""
        TxtSousLot.Text = ""
        InitGbSoumis()
    End Sub

    Private Sub InitGbSoumis()
        CmbNomSoumis.Text = ""
        CmbNomSoumis.Properties.Items.Clear()
        TxtPaysSoumis.Text = ""
        TxtAdresseSoumis.Text = ""
        TxtMonnaie.Text = ""
        TxtTaxes.Text = ""
        TxtMontantLot.Text = ""
        TxtNbSectionArticle.Text = ""
        If (TxtTypeMarche.Text = "Fournitures") Then
            InitGbArticle()
        Else
            CmbCodeSection.Text = ""
            TxtLibelleSection.Text = ""
            CmbCodeSection.Enabled = False

            'ZoneAffichage.Controls.Clear()
        End If
    End Sub

    Private Sub InitGbArticle()
        CmbCodeArticle.Text = ""
        TxtNomArticle.Text = ""
        TxtQteArticle.Text = ""
        TxtCategorieArticle.Text = ""
        TxtLieuLivraison.Text = ""
        InitGbPrixArticle()
        'ZoneAffichage.Controls.Clear()
    End Sub

    Private Sub InitGbPrixArticle()
        TxtPuArticle.Properties.ReadOnly = False
        TxtPuArticle.Text = ""
        TxtPuArticleLettre.Text = ""
        TxtPtArticle.Text = ""
    End Sub

#End Region

#Region "Code non utiliser"

    Private Sub AjouterCaracteristiques5(ByVal leCode As String)

        dt.Columns.Clear()

        dt.Columns.Add("N°", Type.GetType("System.String"))
        dt.Columns.Add("Spécification", Type.GetType("System.String"))
        dt.Columns.Add("Valeur", Type.GetType("System.String"))
        dt.Columns.Add("Syst", Type.GetType("System.String"))

        GbSpecOffertes.Enabled = True
        ZoneAffichage.Enabled = True

        Dim nbElt As Decimal = 0

        dt.Rows.Clear()
        ZoneAffichage.Controls.Clear()
        query = "select LibelleCaract,ValeurCaract,RefSpecCaract from T_SpecTechCaract where RefSpecFournit='" & leCode & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            nbElt = nbElt + 1

            'Grid
            Dim dr = dt.NewRow()

            dr(0) = nbElt.ToString
            dr(1) = MettreApost(rw(0).ToString)
            dr(2) = MettreApost(rw(1).ToString)
            If (CDec(nbElt) / 2 = CDec(nbElt) \ 2) Then dr(3) = "x"

            dt.Rows.Add(dr)

            'Zone
            Dim nvPanel As New DevExpress.XtraEditors.PanelControl
            nvPanel.Name = "Pnl" & rw(2).ToString
            nvPanel.Dock = DockStyle.Top
            nvPanel.Size = New System.Drawing.Size(100, 25)
            ZoneAffichage.Controls.Add(nvPanel)
            ZoneAffichage.Enabled = True
            nvPanel.BringToFront()

            Dim nvChek As New DevExpress.XtraEditors.CheckEdit
            nvChek.Name = "Chk" & rw(2).ToString
            nvChek.Text = " " & nbElt.ToString & " : " & MettreApost(rw(0).ToString) & " ........................................................................................................................................................................................."
            nvChek.Size = New System.Drawing.Size(ZoneAffichage.Width, 19)
            nvChek.Properties.ReadOnly = False
            nvPanel.Controls.Add(nvChek)
            nvChek.Location = New System.Drawing.Point(3, 3)

            Dim nvText As New DevExpress.XtraEditors.TextEdit
            nvText.Name = "Txt" & rw(2).ToString
            nvText.Size = New System.Drawing.Size(300, 20)
            nvPanel.Controls.Add(nvText)
            nvText.Dock = DockStyle.Right
            nvText.BringToFront()


            query = "select ValeurOfferte from T_SoumisCaractFournit where RefSpecCaract='" & rw(2) & "' and RefSoumis='" & RefSoumisCache.Text & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw0 As DataRow In dt1.Rows
                nvChek.Checked = False
                nvText.Text = MettreApost(rw0(0).ToString)
                nvText.BackColor = Color.LightBlue
                nvChek.BackColor = Color.LightBlue
            Next
            query = "select PrixUnitaire from T_SoumisPrixFourniture where RefSpecFournit='" & CodeFournitCache.Text & "' and RefSoumis='" & RefSoumisCache.Text & "'"
            dt1 = ExcecuteSelectQuery(query)
            For Each rw0 As DataRow In dt1.Rows
                TxtPuArticle.Text = rw0(0).ToString
                TxtPuArticle.Properties.ReadOnly = False
            Next
        Next

        GridSpecDemandes.DataSource = dt

        GridViewSpec.Columns.Item(0).Width = 40
        GridViewSpec.Columns.Item(1).Width = 300
        GridViewSpec.Columns.Item(2).Width = 500
        GridViewSpec.Columns.Item(3).Visible = False

        ColorRowGrid(GridViewSpec, "[Syst]='x'", Color.Silver, "Times New Roman", 10, FontStyle.Regular, Color.Black)

    End Sub

    Private Sub AjouterCaracteristiques_old(ByVal leCode As String)

        dt.Columns.Clear()

        dt.Columns.Add("N°", Type.GetType("System.String"))
        dt.Columns.Add("Spécification", Type.GetType("System.String"))
        dt.Columns.Add("Valeur", Type.GetType("System.String"))
        dt.Columns.Add("Syst", Type.GetType("System.String"))

        Dim nbElt As Decimal = 0
        dt.Rows.Clear()
        ZoneAffichage.Controls.Clear()
        query = "select LibelleCaract,ValeurCaract,RefSpecCaract from T_SpecTechCaract where RefSpecFournit='" & leCode & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            nbElt = nbElt + 1

            'Grid
            Dim dr = dt.NewRow()

            dr(0) = nbElt.ToString
            dr(1) = MettreApost(rw(0).ToString)
            dr(2) = MettreApost(rw(1).ToString)
            If (CDec(nbElt) / 2 = CDec(nbElt) \ 2) Then dr(3) = "x"

            dt.Rows.Add(dr)

            'Zone
            Dim nvPanel As New DevExpress.XtraEditors.PanelControl
            nvPanel.Name = "Pnl" & rw(2).ToString
            nvPanel.Dock = DockStyle.Top
            nvPanel.Size = New System.Drawing.Size(100, 25)
            ZoneAffichage.Controls.Add(nvPanel)
            nvPanel.BringToFront()

            Dim nvChek As New DevExpress.XtraEditors.CheckEdit
            nvChek.Name = "Chk" & rw(2).ToString
            nvChek.Text = " " & nbElt.ToString & " : " & MettreApost(rw(0).ToString) & " ........................................................................................................................................................................................."
            nvChek.Size = New System.Drawing.Size(ZoneAffichage.Width, 19)
            nvChek.Properties.ReadOnly = True
            nvPanel.Controls.Add(nvChek)
            nvChek.Location = New System.Drawing.Point(3, 3)

            Dim nvText As New DevExpress.XtraEditors.TextEdit
            nvText.Name = "Txt" & rw(2).ToString
            nvText.Size = New System.Drawing.Size(300, 20)
            nvPanel.Controls.Add(nvText)
            nvText.Dock = DockStyle.Right
            nvText.BringToFront()

            query = "select ValeurOfferte from T_SoumisCaractFournit where RefSpecCaract='" & rw(2) & "' and RefSoumis='" & RefSoumisCache.Text & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw0 As DataRow In dt1.Rows
                nvChek.Checked = True
                nvText.Text = MettreApost(rw0(0).ToString)
                nvText.BackColor = Color.LightBlue
                nvChek.BackColor = Color.LightBlue
            Next
            query = "select PrixUnitaire from T_SoumisPrixFourniture where RefSpecFournit='" & CodeFournitCache.Text & "' and RefSoumis='" & RefSoumisCache.Text & "'"
            dt1 = ExcecuteSelectQuery(query)
            For Each rw0 As DataRow In dt1.Rows
                TxtPuArticle.Text = rw0(0).ToString
                TxtPuArticle.Properties.ReadOnly = True
            Next
        Next

        GridSpecDemandes.DataSource = dt

        GridViewSpec.Columns.Item(0).Width = 40
        GridViewSpec.Columns.Item(1).Width = 300
        GridViewSpec.Columns.Item(2).Width = 500
        GridViewSpec.Columns.Item(3).Visible = False

        ColorRowGrid(GridViewSpec, "[Syst]='x'", Color.Silver, "Times New Roman", 10, FontStyle.Regular, Color.Black)

    End Sub
#End Region

#Region "Traitement Travaux"
    Private Sub CmbCodeSection_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCodeSection.SelectedValueChanged
        Try
            'ZoneAffichage.Controls.Clear()
            CodeSectionCache.Text = ""
            CmbSousSect.Properties.Items.Clear()
            CmbSousSect.Enabled = False
            CmbSousSect.Text = ""
            TxtSousSect.Text = ""
            Modif = False

            If CmbNumDAO.SelectedIndex <> -1 And CmbNomSoumis.SelectedIndex <> -1 And CmbNumLot.Text <> "" And CmbCodeSection.SelectedIndex <> -1 Then
                DebutChargement(True, "Chargement des données en cours...")
                ChargerDQE_Demande()
                ChargerDonneDQE_Saisie()
                FinChargement()
            End If

            ' Sous section *************************
            'query = "select NumeroSousSection,RefSousSection from T_DQESection_SousSection where NumeroDAO='" & CmbNumDAO.Text & "' and RefSection='" & CodeSectionCache.Text & "' order by RefSousSection"
            'dt = ExcecuteSelectQuery(query)
            'If dt.Rows.Count > 0 Then
            '    CmbSousSect.Enabled = True
            'End If
            'For Each rw As DataRow In dt0.Rows
            '    CmbSousSect.Properties.Items.Add(rw(0).ToString)
            'Next
            'If (CmbSousSect.Enabled = False) Then
            '    AjouterItemDQE(CmbSousLot.Text, "")
            'End If

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub ChargerDQE_Demande()
        Try
            query = "select Designation, RefSection from T_DQESection where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeLot='" & CmbNumLot.Text & "' and NumeroSection='" & EnleverApost(CmbCodeSection.Text) & "' and CodeSousLot='" & IIf(CmbSousLot.Enabled = True, CmbSousLot.Text, "").ToString & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtLibelleSection.Text = MettreApost(rw("Designation").ToString)
                CodeSectionCache.Text = rw("RefSection").ToString
            Next

            Dim SomAvaloir As Decimal = 0
            Dim nbSyst As Decimal = 0

            Dim Newdt As New DataTable
            Newdt.Columns.Clear()
            Newdt.Columns.Add("N°", Type.GetType("System.String"))
            Newdt.Columns.Add("Désignation", Type.GetType("System.String"))
            Newdt.Columns.Add("Unités", Type.GetType("System.String"))
            Newdt.Columns.Add("Quantité", Type.GetType("System.String"))
            Newdt.Columns.Add("Prix U. HTVA", Type.GetType("System.String"))
            Newdt.Columns.Add("Montant HTVA", Type.GetType("System.String"))
            Newdt.Columns.Add("Montant en Lettre", Type.GetType("System.String"))
            Newdt.Columns.Add("Syst", Type.GetType("System.String"))
            Newdt.Rows.Clear()

            'query = "select S.NumeroSection, I.NumeroItem, I.Designation,I.QteItem,I.UniteItem,I.PuHtva,I.MontHtva,I.PuHtvaLettre from T_DQESection as S, T_DQEItem as I where S.RefSection=I.RefSection and S.NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and S.CodeLot='" & CmbNumLot.Text & "' and I.PuHtva='0'"

            query = "select S.NumeroSection, I.NumeroItem, I.Designation,I.QteItem,I.UniteItem,I.PuHtva,I.MontHtva,I.PuHtvaLettre from T_DQESection as S, T_DQEItem as I where S.RefSection=I.RefSection and S.NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and S.CodeLot='" & CmbNumLot.Text & "' and S.RefSection='" & CodeSectionCache.Text & "'"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                SomAvaloir = SomAvaloir + CDec(rw("MontHtva"))
                nbSyst = nbSyst + 1
                Dim dr = Newdt.NewRow()

                ' dr("N°") = rw("NumeroSection").ToString & "." & rw("NumeroItem").ToString
                dr("N°") = MettreApost(rw("NumeroItem").ToString)
                dr("Désignation") = MettreApost(rw("Designation").ToString)
                dr("Unités") = MettreApost(rw("UniteItem").ToString)
                dr("Quantité") = AfficherMonnaie(rw("QteItem").ToString)
                dr("Prix U. HTVA") = AfficherMonnaie(rw("PuHtva").ToString)
                dr("Montant HTVA") = AfficherMonnaie(rw("MontHtva").ToString)
                dr("Montant en Lettre") = MettreApost(rw("PuHtvaLettre").ToString)
                If (CDec(nbSyst) / 2 = CDec(nbSyst) \ 2) Then dr("Syst") = "x"
                Newdt.Rows.Add(dr)
            Next

            GridDQE.DataSource = Newdt
            GridViewDQE.OptionsView.ColumnAutoWidth = True
            GridViewDQE.Columns("N°").MaxWidth = 50
            GridViewDQE.Columns("Désignation").Width = 200
            GridViewDQE.Columns("Unités").MaxWidth = 70
            GridViewDQE.Columns("Quantité").MaxWidth = 70
            GridViewDQE.Columns("Prix U. HTVA").Width = 150
            GridViewDQE.Columns("Montant HTVA").Width = 150
            GridViewDQE.Columns("Montant en Lettre").Width = 500
            GridViewDQE.Columns("Syst").Visible = False
            GridViewDQE.Columns("Prix U. HTVA").Visible = False
            GridViewDQE.Columns("Montant HTVA").Visible = False
            GridViewDQE.Columns("Montant en Lettre").Visible = False

            ColorRowGrid(GridViewDQE, "[Syst]='x'", Color.Silver, "Tahoma", 8, FontStyle.Regular, Color.Black)
            '  GbSpecDemandes.Text = "Sommes à valoir (" & AfficherMonnaie(SomAvaloir.ToString) & ")"

            GridViewDQE.Columns("Unités").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridViewDQE.Columns("Quantité").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            GridViewDQE.Columns("Prix U. HTVA").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            GridViewDQE.Columns("Montant HTVA").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub ChargerDonneDQE_Saisie()
        Try
            Dim NewdtSaisiDQE As New DataTable
            NewdtSaisiDQE.Columns.Clear()
            NewdtSaisiDQE.Columns.Add("RefItem", Type.GetType("System.String"))
            NewdtSaisiDQE.Columns.Add("Désignation", Type.GetType("System.String"))
            NewdtSaisiDQE.Columns.Add("Unités", Type.GetType("System.String"))
            NewdtSaisiDQE.Columns.Add("Quantité", Type.GetType("System.String"))
            NewdtSaisiDQE.Columns.Add("Prix U. HTVA en FCFA", Type.GetType("System.String"))
            NewdtSaisiDQE.Columns.Add("Montant HTVA en FCFA", Type.GetType("System.String"))
            NewdtSaisiDQE.Columns.Add("Montant en Lettre", Type.GetType("System.String"))
            NewdtSaisiDQE.Columns.Add("CodeX", Type.GetType("System.String"))
            NewdtSaisiDQE.Columns.Add("Syst", Type.GetType("System.String"))
            NewdtSaisiDQE.Rows.Clear()
            GridSaisieDQE.DataSource = Nothing

            query = "select S.NumeroSection, I.NumeroItem, I.* from T_DQESection as S, T_DQEItem as I where S.RefSection=I.RefSection and S.NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and S.CodeLot='" & CmbNumLot.Text & "' and S.RefSection='" & CodeSectionCache.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            Dim nbSyst As Integer = 0
            Dim PrixUnitaireSoumis As String = ""
            Dim MontantSoumis As String = ""
            Dim MontantTotalSection As Decimal = 0

            For Each rw As DataRow In dt0.Rows
                Dim dr = NewdtSaisiDQE.NewRow()
                nbSyst += 1
                dr("CodeX") = IIf(nbSyst Mod 2 = 0, "x", "").ToString
                dr("Syst") = "Required"
                dr("RefItem") = rw("RefItem")
                dr("Désignation") = MettreApost(rw("Designation").ToString)
                dr("Unités") = MettreApost(rw("UniteItem").ToString)
                dr("Quantité") = AfficherMonnaie(rw("QteItem").ToString)

                '************* Recherche du prix unitaire du soumissoinnaire
                PrixUnitaireSoumis = GetSelectPrixUnitaireSoumis(rw("RefItem"))

                If (PrixUnitaireSoumis.ToString <> "") Then
                    MontantSoumis = CDbl(PrixUnitaireSoumis) * CDbl(rw("QteItem"))
                    MontantTotalSection += CDbl(MontantSoumis)
                Else
                    MontantSoumis = ""
                End If

                dr("Prix U. HTVA en FCFA") = IIf(PrixUnitaireSoumis.ToString = "", "", AfficherMonnaie(PrixUnitaireSoumis.ToString)).ToString
                dr("Montant HTVA en FCFA") = IIf(PrixUnitaireSoumis.ToString = "", "", AfficherMonnaie(MontantSoumis.ToString)).ToString
                dr("Montant en Lettre") = IIf(MontantSoumis.ToString <> "", MontantLettre(MontantSoumis.ToString), "").ToString
                NewdtSaisiDQE.Rows.Add(dr)
            Next

            ' **** Ajout des elements ajouter par le soumissionnaire

            query = "select F.*, P.* from t_fournisseur_dqeitem_propose as F, t_soumis_dqeitem_propose as P, t_dqesection as S where P.RefDqeitemPro=F.RefDqeitemPro and F.RefSection=S.RefSection and S.NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and S.CodeLot='" & CmbNumLot.Text & "' and S.RefSection='" & CodeSectionCache.Text & "' and P.RefSoumis='" & RefSoumisCache.Text & "' GROUP BY P.RefDqeitemPro"
            dt0 = ExcecuteSelectQuery(query)
            PrixUnitaireSoumis = ""
            MontantSoumis = ""

            For Each rw As DataRow In dt0.Rows
                Dim dr = NewdtSaisiDQE.NewRow()
                nbSyst += 1
                dr("CodeX") = IIf(nbSyst Mod 2 = 0, "x", "").ToString
                dr("Syst") = ""
                dr("RefItem") = rw("RefDqeitemPro")
                dr("Désignation") = MettreApost(rw("Designation").ToString)
                dr("Unités") = MettreApost(rw("Unite").ToString)
                dr("Quantité") = AfficherMonnaie(rw("Qte").ToString)

                '************* Recherche du prix unitaire du soumissoinnaire
                'PrixUnitaireSoumis = GetSelectPrixUnitaireSoumis(rw("RefDqeitemPro"), "DQE PROPOSE")
                PrixUnitaireSoumis = rw("PrixUnitePropo").ToString
                If (PrixUnitaireSoumis.ToString <> "") Then
                    MontantSoumis = CDbl(PrixUnitaireSoumis) * CDbl(rw("Qte"))
                    MontantTotalSection += CDbl(MontantSoumis)
                Else
                    MontantSoumis = ""
                End If

                dr("Prix U. HTVA en FCFA") = IIf(PrixUnitaireSoumis.ToString = "", "", AfficherMonnaie(PrixUnitaireSoumis.ToString)).ToString
                dr("Montant HTVA en FCFA") = IIf(PrixUnitaireSoumis.ToString = "", "", AfficherMonnaie(MontantSoumis.ToString)).ToString
                dr("Montant en Lettre") = IIf(MontantSoumis.ToString <> "", MontantLettre(MontantSoumis.ToString), "").ToString
                NewdtSaisiDQE.Rows.Add(dr)
            Next

            GridSaisieDQE.DataSource = NewdtSaisiDQE
            ViewSaisieDQE.Columns("Syst").Visible = False
            ViewSaisieDQE.Columns("CodeX").Visible = False
            ViewSaisieDQE.Columns("RefItem").Visible = False
            ' ViewSaisieDQE.Columns("RefItem").Width = 50
            ViewSaisieDQE.Columns("Désignation").Width = 250
            ViewSaisieDQE.Columns("Unités").Width = 100
            ViewSaisieDQE.Columns("Quantité").Width = 60
            ViewSaisieDQE.Columns("Prix U. HTVA en FCFA").Width = 150
            ViewSaisieDQE.Columns("Montant HTVA en FCFA").Width = 150
            ViewSaisieDQE.Columns("Montant en Lettre").Width = 500
            ColorRowGrid(ViewSaisieDQE, "[CodeX]='x'", Color.Silver, "Tahoma", 8, FontStyle.Regular, Color.Black)

            ViewSaisieDQE.Columns("Unités").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewSaisieDQE.Columns("Quantité").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewSaisieDQE.Columns("Prix U. HTVA en FCFA").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewSaisieDQE.Columns("Montant HTVA en FCFA").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

            ViewSaisieDQE.OptionsBehavior.Editable = True
            GridSaisieDQE.EmbeddedNavigator.Buttons.Append.Enabled = True

            'ViewSaisieDQE.OptionsView.ColumnAutoWidth = True
            'ViewSaisieDQE.Columns("N°").MaxWidth = 30

            ViewSaisieDQE.Columns("Syst").OptionsColumn.AllowEdit = False
            ViewSaisieDQE.Columns("CodeX").OptionsColumn.AllowEdit = False
            ' ViewSaisieDQE.Columns("Spécification").OptionsColumn.AllowEdit = False
            ViewSaisieDQE.Columns("Montant HTVA en FCFA").OptionsColumn.AllowEdit = False
            ViewSaisieDQE.Columns("Montant en Lettre").OptionsColumn.AllowEdit = False

            Dim cmbUnites As RepositoryItemComboBox = New RepositoryItemComboBox()
            Dim txtEditQte As New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
            Dim txtEditPU As New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
            Dim txtEditDesing As New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit

            cmbUnites.TextEditStyle = TextEditStyles.DisableTextEditor
            GetCmbUnites(cmbUnites)
            AddHandler txtEditDesing.EditValueChanging, AddressOf txtEditDesing_EditValueChanging
            AddHandler cmbUnites.EditValueChanging, AddressOf CmbUniteCancelEdit

            AddHandler txtEditQte.EditValueChanged, AddressOf txtEditQte_EditValueChanged
            AddHandler txtEditQte.EditValueChanging, AddressOf txtEditQte_EditValueChanging
            AddHandler txtEditPU.EditValueChanged, AddressOf txtEditPU_EditValueChanged
            AddHandler txtEditPU.EditValueChanging, AddressOf txtEditPU_EditValueChanging
            ViewSaisieDQE.Columns("Unités").ColumnEdit = cmbUnites
            ViewSaisieDQE.Columns("Quantité").ColumnEdit = txtEditQte
            ViewSaisieDQE.Columns("Prix U. HTVA en FCFA").ColumnEdit = txtEditPU
            ViewSaisieDQE.Columns("Désignation").ColumnEdit = txtEditDesing

            ' ViewSaisieDQE.Columns("Quantité").GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            GbSpecDemandes.Text = IIf(MontantTotalSection > 0, "Sommes à valoir (" & AfficherMonnaie(MontantTotalSection.ToString) & ")", "Sommes à valoir").ToString

            query = "SELECT COUNT(*) FROM t_dqeitem I, t_soumisprixitemdqe S WHERE S.RefItem=I.RefItem AND S.RefSoumis='" & RefSoumisCache.Text & "' AND I.RefSection='" & CodeSectionCache.Text & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                'Verifier si les membres de la commission ont commencé l'analyse de offres pour empeché la modification
                query = "SELECT COUNT(*) FROM t_dao_evalcojo as F , t_dao as S WHERE F.NumeroDAO=S.NumeroDAO AND S.NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and S.CodeProjet='" & ProjetEnCours & "' AND F.AnalyseOffres IS NOT NULL" 'ExamPrelimOffres
                'Si tous les membres de la commission ont finis d'évaluer
                If Val(ExecuteScallar(query)) = 0 Then
                    BtModifier.Enabled = True
                    BtValiderOffre.Enabled = False
                    ViewSaisieDQE.OptionsBehavior.Editable = False
                    GridSaisieDQE.EmbeddedNavigator.Buttons.Append.Enabled = False
                Else
                    BtModifier.Enabled = False
                    BtValiderOffre.Enabled = False
                    ViewSaisieDQE.OptionsBehavior.Editable = False
                    GridSaisieDQE.EmbeddedNavigator.Buttons.Append.Enabled = False
                End If
            Else
                BtValiderOffre.Enabled = True
                BtModifier.Enabled = False
            End If

            ' ViewSaisieDQE.OptionsBehavior.AutoExpandAllGroups = False
            ViewSaisieDQE.VertScrollVisibility = True
            ViewSaisieDQE.HorzScrollVisibility = True
            ' ViewSaisieDQE.BestFitColumns()
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub txtEditDesing_EditValueChanging(ByVal sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        If ViewSaisieDQE.RowCount > 0 Then
            DrX = ViewSaisieDQE.GetDataRow(ViewSaisieDQE.FocusedRowHandle)
            If DrX("Syst").ToString = "Required" Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub CmbUniteCancelEdit(ByVal sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        If ViewSaisieDQE.RowCount > 0 Then
            DrX = ViewSaisieDQE.GetDataRow(ViewSaisieDQE.FocusedRowHandle)
            If DrX("Syst").ToString = "Required" Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub GetCmbUnites(ByRef OutPut As RepositoryItemComboBox)
        Dim dt1 As DataTable = ExcecuteSelectQuery("SELECT * FROM t_unite ORDER BY LibelleUnite ASC")
        OutPut.Items.Clear()
        For Each rw As DataRow In dt1.Rows
            OutPut.Items.Add(MettreApost(rw("LibelleUnite").ToString))
        Next
    End Sub

    Private Sub txtEditQte_EditValueChanging(ByVal sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        If ViewSaisieDQE.RowCount > 0 Then
            DrX = ViewSaisieDQE.GetDataRow(ViewSaisieDQE.FocusedRowHandle)
            If DrX("Syst").ToString = "Required" Then
                e.Cancel = True
            End If

            If sender.text <> "" Then
                If Not IsNumeric(sender.text) Then
                    e.Cancel = True
                End If
            Else
                ViewSaisieDQE.SetFocusedRowCellValue("Montant HTVA en FCFA", "")
                ViewSaisieDQE.SetFocusedRowCellValue("Montant en Lettre", "")
            End If
        End If
    End Sub

    Private Sub txtEditQte_EditValueChanged(ByVal sender As Object, e As EventArgs)
        If ViewSaisieDQE.RowCount > 0 Then
            DrX = ViewSaisieDQE.GetDataRow(ViewSaisieDQE.FocusedRowHandle)
            If sender.text <> "" Then
                If IsNumeric(sender.text) Then
                    If DrX("Prix U. HTVA en FCFA").ToString <> "" Then
                        If IsNumeric(DrX("Prix U. HTVA en FCFA").ToString) Then
                            Dim Montant As Decimal = CDbl(sender.text) * CDbl(DrX("Prix U. HTVA en FCFA").ToString)

                            ViewSaisieDQE.SetFocusedRowCellValue("Montant HTVA en FCFA", AfficherMonnaie(Montant.ToString))
                            ViewSaisieDQE.SetFocusedRowCellValue("Montant en Lettre", MontantLettre(Montant.ToString))
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txtEditPU_EditValueChanging(ByVal sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        If sender.text <> "" Then
            If Not IsNumeric(sender.text) Then
                e.Cancel = True
            End If
        Else
            ViewSaisieDQE.SetFocusedRowCellValue("Montant HTVA en FCFA", "")
            ViewSaisieDQE.SetFocusedRowCellValue("Montant en Lettre", "")
        End If
    End Sub

    Private Sub txtEditPU_EditValueChanged(ByVal sender As Object, e As EventArgs)
        If ViewSaisieDQE.RowCount > 0 Then
            DrX = ViewSaisieDQE.GetDataRow(ViewSaisieDQE.FocusedRowHandle)
            If sender.text <> "" Then
                If IsNumeric(sender.text) Then
                    If DrX("Quantité").ToString <> "" Then
                        If IsNumeric(DrX("Quantité").ToString) Then
                            Dim Montant As Decimal = CDbl(sender.text) * CDbl(DrX("Quantité").ToString)

                            ViewSaisieDQE.SetFocusedRowCellValue("Montant HTVA en FCFA", AfficherMonnaie(Montant.ToString))
                            ViewSaisieDQE.SetFocusedRowCellValue("Montant en Lettre", MontantLettre(Montant.ToString))
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Function GetSelectPrixUnitaireSoumis(ByVal RefItem As String, Optional TypeRequette As String = "") As String
        Dim PrixUnitaire As String = ""
        Try
            If TypeRequette = "" Then
                PrixUnitaire = ExecuteScallar("select MontantItem from t_soumisprixitemdqe where RefItem='" & RefItem & "' and RefSoumis='" & RefSoumisCache.Text & "' LIMIT 1")
            Else
                PrixUnitaire = ExecuteScallar("select S.PrixUnitePropo from t_soumis_dqeitem_propose as S, t_fournisseur_dqeitem_propose as F where S.RefDqeitemPro=F.RefDqeitemPro AND F.RefSection='" & CodeSectionCache.Text & "' and S.RefSoumis='" & RefSoumisCache.Text & "' LIMIT 1")
            End If
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
            Return PrixUnitaire.ToString
        End Try
        Return PrixUnitaire
    End Function
#End Region

#Region "Traitement Fournitures"

    Private Sub CmbCodeArticle_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCodeArticle.SelectedValueChanged
        InitGbPrixArticle()
        InitTabSaisieOffre()

        If (CmbNomSoumis.Text <> "" And CmbCodeArticle.Text <> "") Then
            Dim Categor As String() = {"", ""}

            Dim RefSpecFournit As String = ""
            Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDAO.Text)
            Dim nbsouslot As Integer = Val(Resultat(0))

            If nbsouslot > 0 Then
                query = "select DescripFournit,QteFournit,UniteFournit,LieuLivraison,CodeCategorie,RefSpecFournit from T_SpecTechFourniture where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeLot='" & CmbNumLot.Text & "' and CodeFournit='" & CmbCodeArticle.Text & "' and CodeSousLot='" & CmbSousLot.Text & "'"
            Else
                query = "select DescripFournit,QteFournit,UniteFournit,LieuLivraison,CodeCategorie,RefSpecFournit from T_SpecTechFourniture where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeLot='" & CmbNumLot.Text & "' and CodeFournit='" & CmbCodeArticle.Text & "'"
            End If
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw0 As DataRow In dt0.Rows
                TxtNomArticle.Text = MettreApost(rw0("DescripFournit").ToString)
                TxtQteArticle.Text = rw0("QteFournit").ToString & " " & rw0("UniteFournit").ToString
                QteFournitCache.Text = rw0("QteFournit").ToString
                TxtLieuLivraison.Text = MettreApost(rw0("LieuLivraison").ToString)
                Categor = rw0("CodeCategorie").ToString.Split("-")
                RefSpecFournit = rw0("RefSpecFournit").ToString
                CodeFournitCache.Text = RefSpecFournit
            Next

            If Categor(1).ToString = "Cat" Then
                query = "select * from T_PredFournitures_Groupe where IdCat='" & Categor(0).ToString & "'"
                dt = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    TxtCategorieArticle.Text = MettreApost(rw("LibelleCat").ToString)
                Next
            Else
                'query = "select c.* from T_PredFournitures_Groupe c, t_predfournitures_sous_groupe s where s.IdSousCat='" & Categor(0).ToString & "' and c.idcat=s.idcat "
                'dt = ExcecuteSelectQuery(query)
                'For Each rw As DataRow In dt.Rows
                '    TxtCategorieArticle.Text = MettreApost(rw("LibelleCat").ToString)
                'Next
                query = "select * from t_predfournitures_sous_groupe where IdSousCat='" & Categor(0).ToString & "'"
                dt = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    TxtCategorieArticle.Text = MettreApost(rw("LibelleSousCat").ToString)
                Next
            End If

            'Dim TableCategorie As String = ""
            'query = "select LibellePredFGroupe,NomT_PredFItem from T_PredFournitures_Groupe where RefPredFGroupe='" & Categor(1) & "'"
            'dt = ExcecuteSelectQuery(query)
            'For Each rw As DataRow In dt.Rows
            '    TxtCategorieArticle.Text = MettreApost(rw(0).ToString)
            '    TableCategorie = rw(1).ToString
            'Next

            'If (TableCategorie <> "") Then
            '    query = "select LibellePredFItem from " & TableCategorie & " where RefPredFItem='" & Categor(0) & "'"
            '    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            '    For Each rw1 As DataRow In dt1.Rows
            '        TxtSousCategorieArticle.Text = MettreApost(rw1(0).ToString)
            '    Next
            '    End If

            query = "SELECT COUNT(*) FROM t_spectechcaract a , t_soumiscaractfournit b WHERE a.RefSpecCaract=b.RefSpecCaract AND b.RefSoumis='" & RefSoumisCache.Text & "' AND a.RefSpecFournit='" & RefSpecFournit & "'"
            Dim valeur = Val(ExecuteScallar(query))
            If valeur > 0 Then
                AfficherCaracteristiques(RefSpecFournit)

                'Verifier si les membres de la commission ont commencé l'analyse de offres pour empeché la modification
                query = "SELECT COUNT(*) FROM t_dao_evalcojo as F , t_dao as S WHERE F.NumeroDAO=S.NumeroDAO AND S.NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and S.CodeProjet='" & ProjetEnCours & "' AND F.AnalyseOffres IS NOT NULL" 'ExamPrelimOffres
                Dim NbreCOJOEval As Integer = Val(ExecuteScallar(query))
                'Si tous les membres de la commission ont finis d'évaluer
                If NbreCOJOEval = 0 Then
                    BtModifier.Enabled = True
                    BtValiderOffre.Enabled = False
                Else
                    BtModifier.Enabled = False
                    BtValiderOffre.Enabled = False
                End If
            Else
                AjouterCaracteristiques(RefSpecFournit)
                BtValiderOffre.Enabled = True
                BtModifier.Enabled = False
            End If
            RemplirTabSpecDemande(RefSpecFournit)

        End If
    End Sub

    Private Sub InitTabSaisieOffre()
        dt2.Columns.Clear()
        dt2.Columns.Add("N°", Type.GetType("System.String"))
        dt2.Columns.Add("RefSpecCaract", Type.GetType("System.String"))
        dt2.Columns.Add("Spécification techniques", Type.GetType("System.String"))
        dt2.Columns.Add("Valeur", Type.GetType("System.String"))
        dt2.Columns.Add("Syst", Type.GetType("System.String"))
        dt2.Rows.Clear()
        GridSaisieSpecTech.DataSource = dt2
        ViewSaisieSpecTech.OptionsBehavior.Editable = False
        ViewSaisieSpecTech.OptionsView.ColumnAutoWidth = True
        ViewSaisieSpecTech.OptionsBehavior.AutoExpandAllGroups = True
        ViewSaisieSpecTech.VertScrollVisibility = True
        ViewSaisieSpecTech.HorzScrollVisibility = True
        ViewSaisieSpecTech.BestFitColumns()
        ViewSaisieSpecTech.Columns("N°").Visible = False
        ViewSaisieSpecTech.Columns("RefSpecCaract").Visible = False
        ViewSaisieSpecTech.Columns("Syst").Visible = False
        ColorRowGrid(ViewSaisieSpecTech, "[Syst]='x'", Color.Silver, "Times New Roman", 12, FontStyle.Regular, Color.Black)
    End Sub

    Private Sub RemplirTabSpecDemande(ByVal RefSpecFournit As String)
        dt.Columns.Clear()
        dt.Columns.Add("N°", Type.GetType("System.String"))
        dt.Columns.Add("Spécification", Type.GetType("System.String"))
        dt.Columns.Add("Valeur", Type.GetType("System.String"))
        dt.Columns.Add("Syst", Type.GetType("System.String"))
        dt.Rows.Clear()

        Dim nbElt As Decimal = 0
        query = "select LibelleCaract,ValeurCaract,RefSpecCaract from T_SpecTechCaract where RefSpecFournit='" & RefSpecFournit & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            nbElt = nbElt + 1
            Dim dr = dt.NewRow()
            dr("N°") = nbElt.ToString
            dr("Spécification") = MettreApost(rw("LibelleCaract").ToString)
            dr("Valeur") = MettreApost(rw("ValeurCaract").ToString)
            If (CDec(nbElt) / 2 = CDec(nbElt) \ 2) Then dr("Syst") = "x"
            dt.Rows.Add(dr)
        Next

        GridSpecDemandes.DataSource = dt
        GridViewSpec.OptionsView.ColumnAutoWidth = True
        GridViewSpec.Columns.Item(3).Visible = False
        ColorRowGrid(GridViewSpec, "[Syst]='x'", Color.Silver, "Times New Roman", 10, FontStyle.Regular, Color.Black)
    End Sub

    Private Sub AfficherCaracteristiques(ByVal RefSpecFournit As String)
        dt2.Columns.Clear()
        dt2.Columns.Add("N°", Type.GetType("System.String"))
        dt2.Columns.Add("RefSpecCaract", Type.GetType("System.String"))
        dt2.Columns.Add("Spécification techniques", Type.GetType("System.String"))
        dt2.Columns.Add("Valeur", Type.GetType("System.String"))
        dt2.Columns.Add("Syst", Type.GetType("System.String"))
        dt2.Rows.Clear()
        GridSaisieSpecTech.DataSource = dt2
        Dim dt As DataTable = GridSaisieSpecTech.DataSource
        Dim txtNumero As New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
        Dim txtSpec As New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
        ViewSaisieSpecTech.Columns("N°").ColumnEdit = txtNumero
        ViewSaisieSpecTech.Columns("Spécification techniques").ColumnEdit = txtSpec
        AddHandler txtNumero.EditValueChanging, AddressOf txtNumero_EditValueChanging
        AddHandler txtSpec.EditValueChanging, AddressOf txtNumero_EditValueChanging
        Dim nbElt2 As Integer = 0

        query = "select LibelleCaract,RefSpecCaract from T_SpecTechCaract  where RefSpecFournit='" & RefSpecFournit & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)

        For Each rw As DataRow In dt0.Rows
            query = "select distinct ValeurOfferte from T_SoumisCaractFournit where RefSpecCaract='" & rw("RefSpecCaract") & "' and RefSoumis='" & RefSoumisCache.Text & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw0 In dt1.Rows
                Dim dr1 = dt.NewRow()
                nbElt2 = nbElt2 + 1
                dr1("N°") = nbElt2
                dr1("RefSpecCaract") = rw("RefSpecCaract").ToString
                dr1("Spécification techniques") = MettreApost(rw("LibelleCaract").ToString)
                dr1("Valeur") = MettreApost(rw0("ValeurOfferte").ToString)
                dr1("Syst") = "Required"
                dt.Rows.Add(dr1)
            Next
        Next

        query = "select distinct a.LibelleCaract,a.RefSpecCaractPro, b.ValeurOfferte from t_spectechcaractpropose a, t_soumiscaractfournitsupl b where a.RefSpecFournit='" & RefSpecFournit & "' And a.RefSpecCaractPro=b.RefSpecCaract and b.RefSoumis='" & RefSoumisCache.Text & "'"
        Dim dt3 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt3.Rows
            Dim dr1 = dt.NewRow()
            nbElt2 = nbElt2 + 1
            dr1("N°") = nbElt2
            dr1("RefSpecCaract") = rw("RefSpecCaractPro").ToString
            dr1("Spécification techniques") = MettreApost(rw("LibelleCaract").ToString)
            dr1("Valeur") = MettreApost(rw("ValeurOfferte").ToString)
            dr1("Syst") = ""
            dt.Rows.Add(dr1)
        Next

        query = "select PrixUnitaire from T_SoumisPrixFourniture where RefSpecFournit='" & CodeFournitCache.Text & "' and RefSoumis='" & RefSoumisCache.Text & "'"
        Dim dt4 = ExcecuteSelectQuery(query)
        For Each rw0 As DataRow In dt4.Rows
            TxtPuArticle.Text = rw0("PrixUnitaire").ToString
            TxtPuArticle.Properties.ReadOnly = True
        Next
        ViewSaisieSpecTech.OptionsBehavior.Editable = False
        ViewSaisieSpecTech.OptionsView.ColumnAutoWidth = True
        ViewSaisieSpecTech.OptionsBehavior.AutoExpandAllGroups = True
        ViewSaisieSpecTech.VertScrollVisibility = True
        ViewSaisieSpecTech.HorzScrollVisibility = True
        ViewSaisieSpecTech.BestFitColumns()
        ViewSaisieSpecTech.Columns("N°").Visible = False
        ViewSaisieSpecTech.Columns("RefSpecCaract").Visible = False
        ViewSaisieSpecTech.Columns("Syst").Visible = False
        GridSaisieSpecTech.EmbeddedNavigator.Buttons.Append.Enabled = False
        ColorRowGrid(ViewSaisieSpecTech, "[Syst]='x'", Color.Silver, "Times New Roman", 12, FontStyle.Regular, Color.Black)

    End Sub

    Private Sub AjouterCaracteristiques(ByVal RefSpecFournit As String)
        dt2.Columns.Clear()
        dt2.Columns.Add("N°", Type.GetType("System.String"))
        dt2.Columns.Add("RefSpecCaract", Type.GetType("System.String"))
        dt2.Columns.Add("Spécification techniques", Type.GetType("System.String"))
        dt2.Columns.Add("Valeur", Type.GetType("System.String"))
        dt2.Columns.Add("Syst", Type.GetType("System.String"))
        dt2.Rows.Clear()
        GridSaisieSpecTech.DataSource = dt2
        Dim dt As DataTable = GridSaisieSpecTech.DataSource
        Dim txtNumero As New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
        Dim txtSpec As New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
        ViewSaisieSpecTech.Columns("N°").ColumnEdit = txtNumero
        ViewSaisieSpecTech.Columns("Spécification techniques").ColumnEdit = txtSpec
        AddHandler txtNumero.EditValueChanging, AddressOf txtNumero_EditValueChanging
        AddHandler txtSpec.EditValueChanging, AddressOf txtNumero_EditValueChanging
        Dim nbElt2 As Integer = 0
        query = "select LibelleCaract,RefSpecCaract from T_SpecTechCaract  where RefSpecFournit='" & RefSpecFournit & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            Dim dr1 = dt.NewRow()
            nbElt2 = nbElt2 + 1
            dr1("N°") = nbElt2
            dr1("RefSpecCaract") = rw("RefSpecCaract").ToString
            dr1("Spécification techniques") = MettreApost(rw("LibelleCaract").ToString)
            dr1("Valeur") = ""
            dr1("Syst") = "Required"
            dt.Rows.Add(dr1)
        Next
        ViewSaisieSpecTech.OptionsBehavior.Editable = True
        ViewSaisieSpecTech.OptionsView.ColumnAutoWidth = True
        ViewSaisieSpecTech.OptionsBehavior.AutoExpandAllGroups = True
        ViewSaisieSpecTech.VertScrollVisibility = True
        ViewSaisieSpecTech.HorzScrollVisibility = True
        ViewSaisieSpecTech.BestFitColumns()
        ViewSaisieSpecTech.Columns("N°").Visible = False
        ViewSaisieSpecTech.Columns("RefSpecCaract").Visible = False
        ViewSaisieSpecTech.Columns("Syst").Visible = False
        GridSaisieSpecTech.EmbeddedNavigator.Buttons.Append.Enabled = True
        'ViewSaisieSpecTech.Columns.Item(0).Width = 40
        'ViewSaisieSpecTech.Columns.Item(1).Width = 300
        'ViewSaisieSpecTech.Columns.Item(2).Width = 500
        'ViewSaisieSpecTech.Columns.Item(3).Visible = False

        ColorRowGrid(ViewSaisieSpecTech, "[Syst]='x'", Color.Silver, "Times New Roman", 12, FontStyle.Regular, Color.Black)
    End Sub

    Private Sub txtNumero_EditValueChanging(ByVal sender As Object, e As ChangingEventArgs)
        If ViewSaisieSpecTech.GetRowCellValue(ViewSaisieSpecTech.FocusedRowHandle, "Syst").ToString() = "Required" Then
            e.Cancel = True
        End If
    End Sub

#End Region

#Region "Code Travaux non utiliser"
    Private Sub CmbSousSect_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSousSect.SelectedValueChanged

        If (CmbSousSect.Text <> "") Then
            query = "select LibelleSousSection from T_DQESection_SousSection where NumeroDAO='" & CmbNumDAO.Text & "' and RefSection='" & CodeSectionCache.Text & "' and NumeroSousSection='" & CmbSousSect.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtSousSect.Text = MettreApost(rw(0).ToString)
            Next
            AjouterItemDQE(CmbSousLot.Text, CmbSousSect.Text)
        End If

    End Sub

    Private Sub AjouterItemDQE(Optional ByVal SousLot As String = "", Optional ByVal SousSect As String = "")
        'MsgBox("CodeLot=" & CmbNumLot.Text & "   SousLot=" & SousLot & "  SousSect=" & SousSect & "  CodeSection=" & CodeSectionCache.Text, MsgBoxStyle.Information)

        If (CmbCodeSection.Text <> "") Then
            ZoneAffichage.Controls.Clear()
            query = "select S.NumeroSection,I.NumeroItem,I.Designation,I.QteItem,I.UniteItem,S.Designation,I.PuHtva,S.RefSection,I.RefItem from T_DQESection as S, T_DQEItem as I where S.RefSection=I.RefSection and S.NumeroDAO='" & CmbNumDAO.Text & "' and S.CodeLot='" & CmbNumLot.Text & "' and S.NumeroSection='" & CmbCodeSection.Text & "' and S.RefSection='" & CodeSectionCache.Text & "' and S.CodeSousLot='" & SousLot & "' and I.NumeroSousSection='" & SousSect & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows

                'Zone
                Dim nvPanel As New DevExpress.XtraEditors.PanelControl
                nvPanel.Name = "Pnl" & rw(8).ToString
                nvPanel.Dock = DockStyle.Top
                nvPanel.Size = New System.Drawing.Size(100, 25)
                ZoneAffichage.Controls.Add(nvPanel)
                nvPanel.BringToFront()

                Dim nvChek As New DevExpress.XtraEditors.CheckEdit
                nvChek.Name = "Chk" & rw(8).ToString
                nvChek.Text = " " & rw(0).ToString & "." & rw(1).ToString & " : " & MettreApost(rw(2).ToString) & " ........................................................................................................................................................................................."
                nvChek.Size = New System.Drawing.Size(ZoneAffichage.Width, 19)
                nvChek.Properties.ReadOnly = True
                nvPanel.Controls.Add(nvChek)
                nvChek.Location = New System.Drawing.Point(3, 3)

                Dim nvText As New DevExpress.XtraEditors.TextEdit
                nvText.Name = "Txt" & rw(8).ToString
                nvText.Size = New System.Drawing.Size(200, 20)
                nvText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                nvText.Properties.Mask.EditMask = "n0"
                nvText.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
                nvText.Properties.Mask.UseMaskAsDisplayFormat = True
                nvText.Properties.MaxLength = 20
                If (rw(6).ToString <> "") Then
                    nvText.Text = rw(6).ToString
                    nvText.Properties.ReadOnly = True
                End If
                nvPanel.Controls.Add(nvText)
                nvText.Dock = DockStyle.Right
                nvText.BringToFront()

                Dim nvText2 As New DevExpress.XtraEditors.TextEdit
                nvText2.Name = "Qte" & rw(8).ToString
                nvText2.Size = New System.Drawing.Size(100, 20)
                nvText2.Text = rw(3).ToString & " " & rw(4).ToString
                nvText2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                nvText2.Properties.ReadOnly = True
                nvPanel.Controls.Add(nvText2)
                nvText2.Dock = DockStyle.Right
                nvText2.BringToFront()

                query = "select MontantItem from T_SoumisPrixItemDQE where RefItem='" & rw(8) & "' and RefSoumis='" & RefSoumisCache.Text & "'"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw0 As DataRow In dt1.Rows
                    If (rw0(0).ToString <> "") Then
                        nvChek.Checked = True
                        nvText.Text = AfficherMonnaie(rw0(0).ToString.Replace(" ", ""))
                        nvText.BackColor = Color.LightBlue
                        nvText2.BackColor = Color.LightBlue
                        nvChek.BackColor = Color.LightBlue
                    End If
                Next

                BtValiderOffre.Enabled = True
            Next
        End If

    End Sub

#End Region

#Region "Traitement du bouton validation"

    Private Sub BtValiderOffre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtValiderOffre.Click
        Try
            If CmbNumDAO.IsRequiredControl("Veuillez sélectionner un dossier.") Then
                CmbNumDAO.Select()
                Exit Sub
            End If
            If CmbNumLot.IsRequiredControl("Veuillez sélectionner un lot.") Then
                CmbNumLot.Select()
                Exit Sub
            End If
            If CmbSousLot.Enabled Then
                If CmbSousLot.IsRequiredControl("Veuillez sélectionner un sous lot.") Then
                    CmbSousLot.Select()
                    Exit Sub
                End If
            End If
            If CmbNomSoumis.IsRequiredControl("Veuillez sélectionner un soumissionnaire.") Then
                CmbNomSoumis.Select()
                Exit Sub
            End If

            If (TxtTypeMarche.Text.ToLower = "Fournitures".ToLower) Then
                If CmbCodeArticle.IsRequiredControl("Veuillez sélectionner un article.") Then
                    CmbCodeArticle.Select()
                    Exit Sub
                End If
                If ViewSaisieSpecTech.RowCount = 0 Then
                    SuccesMsg("Aucune spécification offerte.")
                    Exit Sub
                End If

                If Val(TxtPuArticle.EditValue) <= 0 Then
                    SuccesMsg("Veuillez saisir le prix unitaire de l'article.")
                    TxtPuArticle.Select()
                    Exit Sub
                End If

            Else ' **** Travaux
                If CmbCodeSection.IsRequiredControl("Veuillez sélectionner une section.") Then
                    CmbCodeSection.Select()
                    Exit Sub
                End If
                If ViewSaisieDQE.RowCount = 0 Then
                    FailMsg("Aucun element define dans le tableau" & vbNewLine & "de dévis quantitatif estimé.")
                    Exit Sub
                End If

                'Verification
                Dim MessageEurreur As String = "Veuillez remplir correctement le tableau de dévis quantitatif estimé."
                For j = 0 To ViewSaisieDQE.RowCount - 1
                    If ViewSaisieDQE.GetRowCellValue(j, "Syst").ToString = "Required" Then
                        If ViewSaisieDQE.GetRowCellValue(j, "Prix U. HTVA en FCFA").ToString = "" Then
                            FailMsg(MessageEurreur.ToString)
                            Exit Sub
                        End If
                    Else
                        If IsDBNull(ViewSaisieDQE.GetRowCellValue(j, "Désignation")) Or IsDBNull(ViewSaisieDQE.GetRowCellValue(j, "Unités")) Or IsDBNull(ViewSaisieDQE.GetRowCellValue(j, "Quantité")) Or IsDBNull(ViewSaisieDQE.GetRowCellValue(j, "Prix U. HTVA en FCFA")) Then
                            FailMsg(MessageEurreur.ToString)
                            Exit Sub
                        End If
                        If ViewSaisieDQE.GetRowCellValue(j, "Désignation").ToString = "" Or ViewSaisieDQE.GetRowCellValue(j, "Unités").ToString = "" Or ViewSaisieDQE.GetRowCellValue(j, "Quantité").ToString = "" Or ViewSaisieDQE.GetRowCellValue(j, "Prix U. HTVA en FCFA").ToString = "" Then
                            FailMsg(MessageEurreur.ToString)
                            Exit Sub
                        End If
                    End If
                Next

                If Modif = False Then 'Pas en cours de modification
                    query = "select A.* from T_SoumisPrixItemDQE AS A, T_DQEItem AS B where A.RefItem=B.RefItem and B.RefSection='" & CodeSectionCache.Text & "' and A.RefSoumis='" & RefSoumisCache.Text & "' and A.Mention<>''"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    If dt1.Rows.Count > 0 Then
                        FailMsg("Traitement impossible, car l'offre a été analysée.")
                        Exit Sub
                    End If
                End If

            End If

            Dim EvalEffectuee As Boolean = False
            Dim MontTotalSection As Decimal = 0

            If Modif = False Then 'Nouvau enregistrement

                query = "SELECT * FROM t_commission WHERE NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "'"
                dtCojo = ExcecuteSelectQuery(query)

                If (TxtTypeMarche.Text.ToLower = "Fournitures".ToLower) Then

                    DebutChargement(True, "Enregistrement des spécifications offertes...")

                    query = "select RefSpecCaract from T_SpecTechCaract where RefSpecFournit='" & CodeFournitCache.Text & "'"
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)

                    Dim valeurOfferte As String = ""

                    For Each rw As DataRow In dt0.Rows
                        If (ViewSaisieSpecTech.RowCount > 0 And EvalEffectuee = False) Then
                            query = "select * from T_SoumisCaractFournit where RefSpecCaract='" & rw(0).ToString & "' and RefSoumis='" & RefSoumisCache.Text & "' and MentionValeur<>''"
                            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                            If dt1.Rows.Count > 0 Then
                                EvalEffectuee = True
                                Exit For 'Sortir de la boucle
                            End If

                            If (EvalEffectuee = False) Then
                                For i = 0 To ViewSaisieSpecTech.RowCount - 1
                                    If Not IsDBNull(ViewSaisieSpecTech.GetRowCellValue(i, "Syst")) Then
                                        If ViewSaisieSpecTech.GetRowCellValue(i, "Syst") = "Required" Then
                                            ExecuteNonQuery("DELETE from T_SoumisCaractFournit where RefSoumis='" & RefSoumisCache.Text & "' and RefSpecCaract='" & ViewSaisieSpecTech.GetRowCellValue(i, "RefSpecCaract") & "'")
                                            For Each rw1 In dtCojo.Rows

                                                If ViewSaisieSpecTech.GetRowCellValue(i, "Valeur") = "" Then
                                                    valeurOfferte = "Aucune information donnée"
                                                Else
                                                    valeurOfferte = ViewSaisieSpecTech.GetRowCellValue(i, "Valeur").ToString
                                                End If
                                                ExecuteNonQuery("INSERT INTO T_SoumisCaractFournit(RefSpecCaract,RefSoumis, ValeurOfferte,ID_COJO) Values('" & ViewSaisieSpecTech.GetRowCellValue(i, "RefSpecCaract") & "','" & RefSoumisCache.Text & "','" & EnleverApost(valeurOfferte.ToString) & "','" & rw1("CodeMem").ToString & "')")
                                            Next
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    Next

                    If (ViewSaisieSpecTech.RowCount > 0 And EvalEffectuee = False) Then
                        For i = 0 To ViewSaisieSpecTech.RowCount - 1
                            If IsDBNull(ViewSaisieSpecTech.GetRowCellValue(i, "Syst")) Then
                                If Not IsDBNull(ViewSaisieSpecTech.GetRowCellValue(i, "Spécification techniques")) And Not IsDBNull(ViewSaisieSpecTech.GetRowCellValue(i, "Valeur")) Then
                                    query = "INSERT INTO t_spectechcaractpropose (RefSpecFournit,LibelleCaract) Values('" & CodeFournitCache.Text & "','" & EnleverApost(ViewSaisieSpecTech.GetRowCellValue(i, "Spécification techniques").ToString) & "')"
                                    ExecuteNonQuery(query)
                                    query = "SELECT MAX(RefSpecCaractPro) FROM t_spectechcaractpropose"
                                    Dim RefSpeCaract = Val(ExecuteScallar(query))
                                    For Each rw1 In dtCojo.Rows
                                        query = "INSERT INTO t_soumiscaractfournitsupl(RefSpecCaract,RefSoumis, ValeurOfferte,ID_COJO) Values('" & RefSpeCaract & "','" & RefSoumisCache.Text & "','" & EnleverApost(ViewSaisieSpecTech.GetRowCellValue(i, "Valeur").ToString) & "','" & rw1("CodeMem").ToString & "')"
                                        ExecuteNonQuery(query)
                                    Next
                                End If
                            End If
                        Next
                    End If

                    If (EvalEffectuee = False) Then
                        query = "INSERT INTO T_SoumisPrixFourniture (RefSoumis,RefSpecFournit,PrixUnitaire) Values('" & RefSoumisCache.Text & "','" & CodeFournitCache.Text & "','" & TxtPuArticle.EditValue.ToString.Replace(" ", "") & "')"
                        ExecuteNonQuery(query)
                    End If

                    If (EvalEffectuee = True) Then
                        FinChargement()
                        FailMsg("Traitement impossible! Offre déjà analysée.")
                    Else
                        FinChargement()
                        SuccesMsg("Enregistrement effectué avec succès.")
                        AfficherCaracteristiques(CodeFournitCache.Text)
                        BtModifier.Enabled = True
                        BtValiderOffre.Enabled = False
                    End If

                Else     ' Pour les travaux *********

                    DebutChargement(True, "Enregistrement en cours...")
                    Dim RefDqeitemPro As Decimal = 0

                    For j = 0 To ViewSaisieDQE.RowCount - 1
                        'Sum montant de la section
                        MontTotalSection += CDbl(ViewSaisieDQE.GetRowCellValue(j, "Montant HTVA en FCFA"))

                        If ViewSaisieDQE.GetRowCellValue(j, "Syst").ToString = "Required" Then
                            ExecuteNonQuery("DELETE from T_SoumisPrixItemDQE where RefSoumis='" & RefSoumisCache.Text & "' and RefItem='" & ViewSaisieDQE.GetRowCellValue(j, "RefItem") & "'")
                            For Each rw1 In dtCojo.Rows
                                ExecuteNonQuery("INSERT INTO T_SoumisPrixItemDQE(RefItem,RefSoumis, MontantItem,ID_COJO) Values('" & ViewSaisieDQE.GetRowCellValue(j, "RefItem") & "','" & RefSoumisCache.Text & "','" & CDbl(ViewSaisieDQE.GetRowCellValue(j, "Prix U. HTVA en FCFA")) & "','" & rw1("CodeMem").ToString & "')")
                            Next
                        Else
                            query = "INSERT INTO t_fournisseur_dqeitem_propose Values(NULL, '" & CodeSectionCache.Text & "','" & EnleverApost(ViewSaisieDQE.GetRowCellValue(j, "Désignation").ToString) & "', '" & EnleverApost(ViewSaisieDQE.GetRowCellValue(j, "Unités").ToString) & "', '" & CDbl(ViewSaisieDQE.GetRowCellValue(j, "Quantité").ToString) & "')"
                            ExecuteNonQuery(query)

                            RefDqeitemPro = ExecuteScallar("SELECT MAX(RefDqeitemPro) FROM t_fournisseur_dqeitem_propose")
                            For Each rw1 In dtCojo.Rows
                                ExecuteNonQuery("INSERT INTO t_soumis_dqeitem_propose(RefDqeitemPro,RefSoumis, PrixUnitePropo,ID_COJO) Values('" & RefDqeitemPro & "','" & RefSoumisCache.Text & "','" & CDbl(ViewSaisieDQE.GetRowCellValue(j, "Prix U. HTVA en FCFA").ToString) & "','" & rw1("CodeMem").ToString & "')")
                            Next
                        End If
                    Next

                    'Save du montant de la section
                    ExecuteNonQuery("DELETE from T_SoumisPrixSectionDQE where RefSoumis='" & RefSoumisCache.Text & "' and RefSection='" & CodeSectionCache.Text & "'")
                    ExecuteNonQuery("INSERT INTO T_SoumisPrixSectionDQE Values('" & RefSoumisCache.Text & "','" & CodeSectionCache.Text & "','" & CDbl(MontTotalSection) & "')")

                    FinChargement()
                    SuccesMsg("Enregistrement effectué avec succès.")
                    'Chargement des données en cours
                    ' GridSaisieDQE.DataSource = Nothing
                    ChargerDonneDQE_Saisie()
                End If

            Else 'Modification en cours

                DebutChargement(True, "Enregistrement des modifications en cours...")

                query = "SELECT * FROM t_commission WHERE NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "'"
                dtCojo = ExcecuteSelectQuery(query)

                If (TxtTypeMarche.Text.ToLower = "Fournitures".ToLower) Then

                    query = "select RefSpecCaract from T_SpecTechCaract where RefSpecFournit='" & CodeFournitCache.Text & "'"
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    Dim valeurOfferte As String = ""

                    For Each rw As DataRow In dt0.Rows
                        If (ViewSaisieSpecTech.RowCount > 0 And EvalEffectuee = False) Then
                            query = "select * from T_SoumisCaractFournit where RefSpecCaract='" & rw("RefSpecCaract").ToString & "' and RefSoumis='" & RefSoumisCache.Text & "' and MentionValeur<>''"
                            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                            If dt1.Rows.Count > 0 Then
                                EvalEffectuee = True
                                Exit For
                            End If

                            If (EvalEffectuee = False) Then
                                For i = 0 To ViewSaisieSpecTech.RowCount - 1
                                    If Not IsDBNull(ViewSaisieSpecTech.GetRowCellValue(i, "Syst")) Then
                                        If ViewSaisieSpecTech.GetRowCellValue(i, "Syst") = "Required" Then
                                            If ViewSaisieSpecTech.GetRowCellValue(i, "Valeur") = "" Then
                                                valeurOfferte = "Aucune information donnée"
                                            Else
                                                valeurOfferte = ViewSaisieSpecTech.GetRowCellValue(i, "Valeur").ToString
                                            End If
                                            ExecuteNonQuery("UPDATE T_SoumisCaractFournit SET ValeurOfferte ='" & EnleverApost(valeurOfferte.ToString) & "' where RefSoumis='" & RefSoumisCache.Text & "' and RefSpecCaract='" & ViewSaisieSpecTech.GetRowCellValue(i, "RefSpecCaract") & "'")
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    Next

                    If (ViewSaisieSpecTech.RowCount > 0 And EvalEffectuee = False) Then
                        For i = 0 To ViewSaisieSpecTech.RowCount - 1
                            If Not IsDBNull(ViewSaisieSpecTech.GetRowCellValue(i, "Syst")) Then
                                If ViewSaisieSpecTech.GetRowCellValue(i, "Syst") = "" Then
                                    query = "DELETE from t_soumiscaractfournitsupl where RefSpecCaract='" & ViewSaisieSpecTech.GetRowCellValue(i, "RefSpecCaract") & "' AND RefSoumis='" & RefSoumisCache.Text & "'"
                                    ExecuteNonQuery(query)
                                    query = "DELETE from t_spectechcaractpropose where RefSpecFournit='" & CodeFournitCache.Text & "' AND RefSpecCaractPro='" & ViewSaisieSpecTech.GetRowCellValue(i, "RefSpecCaract") & "'"
                                    ExecuteNonQuery(query)
                                    If ViewSaisieSpecTech.GetRowCellValue(i, "Spécification techniques") <> "" And ViewSaisieSpecTech.GetRowCellValue(i, "Valeur") <> "" Then
                                        query = "INSERT INTO t_spectechcaractpropose (RefSpecFournit,LibelleCaract) Values('" & CodeFournitCache.Text & "','" & EnleverApost(ViewSaisieSpecTech.GetRowCellValue(i, "Spécification techniques").ToString) & "')"
                                        ExecuteNonQuery(query)
                                        query = "SELECT MAX(RefSpecCaractPro) FROM t_spectechcaractpropose"
                                        Dim RefSpeCaract = Val(ExecuteScallar(query))
                                        For Each rw1 In dtCojo.Rows
                                            query = "INSERT INTO t_soumiscaractfournitsupl(RefSpecCaract,RefSoumis, ValeurOfferte,ID_COJO) Values('" & RefSpeCaract & "','" & RefSoumisCache.Text & "','" & EnleverApost(ViewSaisieSpecTech.GetRowCellValue(i, "Valeur").ToString) & "','" & rw1("CodeMem").ToString & "')"
                                            ExecuteNonQuery(query)
                                        Next
                                    End If
                                End If
                            End If

                            If IsDBNull(ViewSaisieSpecTech.GetRowCellValue(i, "Syst")) Then
                                If Not IsDBNull(ViewSaisieSpecTech.GetRowCellValue(i, "Spécification techniques")) And Not IsDBNull(ViewSaisieSpecTech.GetRowCellValue(i, "Valeur")) Then
                                    query = "INSERT INTO t_spectechcaractpropose (RefSpecFournit,LibelleCaract) Values('" & CodeFournitCache.Text & "','" & EnleverApost(ViewSaisieSpecTech.GetRowCellValue(i, "Spécification techniques").ToString) & "')"
                                    ExecuteNonQuery(query)
                                    query = "SELECT MAX(RefSpecCaractPro) FROM t_spectechcaractpropose"
                                    Dim RefSpeCaract = Val(ExecuteScallar(query))
                                    For Each rw1 In dtCojo.Rows
                                        query = "INSERT INTO t_soumiscaractfournitsupl(RefSpecCaract,RefSoumis, ValeurOfferte,ID_COJO) Values('" & RefSpeCaract & "','" & RefSoumisCache.Text & "','" & EnleverApost(ViewSaisieSpecTech.GetRowCellValue(i, "Valeur").ToString) & "','" & rw1("CodeMem").ToString & "')"
                                        ExecuteNonQuery(query)
                                    Next
                                End If
                            End If
                        Next
                    End If

                    If (EvalEffectuee = False) Then
                        query = "UPDATE T_SoumisPrixFourniture SET PrixUnitaire ='" & TxtPuArticle.EditValue.ToString.Replace(" ", "") & "' WHERE RefSpecFournit='" & CodeFournitCache.Text & "' AND RefSoumis='" & RefSoumisCache.Text & "'"
                        ExecuteNonQuery(query)
                    End If

                    FinChargement()
                    If (EvalEffectuee = True) Then
                        FailMsg("Traitement impossible! Offre déjà analysée.")
                    Else
                        SuccesMsg("Modification effectuée avec succès.")
                        AfficherCaracteristiques(CodeFournitCache.Text)
                        Modif = False
                        BtModifier.Enabled = True
                        BtValiderOffre.Enabled = False
                    End If

                Else '************ Travaux

                    MontTotalSection = 0
                    Dim RefDqeitemPro As Decimal = 0

                    For j = 0 To ViewSaisieDQE.RowCount - 1
                        'Sum montant de la section
                        MontTotalSection += CDbl(ViewSaisieDQE.GetRowCellValue(j, "Montant HTVA en FCFA"))

                        If ViewSaisieDQE.GetRowCellValue(j, "Syst").ToString = "Required" Then
                            ExecuteNonQuery("UPDATE T_SoumisPrixItemDQE SET MontantItem='" & CDbl(ViewSaisieDQE.GetRowCellValue(j, "Prix U. HTVA en FCFA")) & "' where RefSoumis='" & RefSoumisCache.Text & "' and RefItem='" & ViewSaisieDQE.GetRowCellValue(j, "RefItem") & "'")
                        Else
                            ExecuteNonQuery("delete from t_fournisseur_dqeitem_propose where RefDqeitemPro='" & ViewSaisieDQE.GetRowCellValue(j, "RefItem") & "' and RefSection='" & CodeSectionCache.Text & "'")
                            ExecuteNonQuery("delete from t_soumis_dqeitem_propose where RefDqeitemPro='" & ViewSaisieDQE.GetRowCellValue(j, "RefItem") & "' and RefSoumis='" & RefSoumisCache.Text & "'")

                            query = "INSERT INTO t_fournisseur_dqeitem_propose Values(NULL, '" & CodeSectionCache.Text & "','" & EnleverApost(ViewSaisieDQE.GetRowCellValue(j, "Désignation").ToString) & "', '" & EnleverApost(ViewSaisieDQE.GetRowCellValue(j, "Unités").ToString) & "', '" & CDbl(ViewSaisieDQE.GetRowCellValue(j, "Quantité").ToString) & "')"
                            ExecuteNonQuery(query)

                            RefDqeitemPro = ExecuteScallar("SELECT MAX(RefDqeitemPro) FROM t_fournisseur_dqeitem_propose")
                            For Each rw1 In dtCojo.Rows
                                ExecuteNonQuery("INSERT INTO t_soumis_dqeitem_propose(RefDqeitemPro,RefSoumis, PrixUnitePropo,ID_COJO) Values('" & RefDqeitemPro & "','" & RefSoumisCache.Text & "','" & CDbl(ViewSaisieDQE.GetRowCellValue(j, "Prix U. HTVA en FCFA")) & "','" & rw1("CodeMem").ToString & "')")
                            Next
                        End If
                    Next

                    'Update du montant de la section
                    ExecuteNonQuery("UPDATE T_SoumisPrixSectionDQE SET MontantSection= '" & CDbl(MontTotalSection) & "' where RefSoumis='" & RefSoumisCache.Text & "' and RefSection='" & CodeSectionCache.Text & "'")

                    SuccesMsg("Modification effectuée avec succès.")
                    Modif = False
                    ChargerDonneDQE_Saisie()
                End If
            End If

        Catch ex As Exception
            FinChargement()
            FailMsg("Information indisponible : " & vbNewLine & ex.ToString)
        End Try
    End Sub

#End Region


    Private Sub TxtPuArticle_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPuArticle.TextChanged
        If (TxtPuArticle.Text <> "" And QteFournitCache.Text <> "") Then
            TxtPtArticle.Text = AfficherMonnaie((CDec(TxtPuArticle.Text) * CDec(QteFournitCache.EditValue)).ToString)
            TxtPuArticleLettre.Text = MontantLettre(TxtPuArticle.EditValue.ToString.Replace(" ", ""))
        Else
            TxtPtArticle.Text = "0"
            TxtPuArticleLettre.Text = ""
        End If
    End Sub

    Private Sub SaisieOffres_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub BtModifier_Click(sender As System.Object, e As System.EventArgs) Handles BtModifier.Click
        If TxtTypeMarche.Text.ToLower = "Fournitures".ToLower Then
            If (CmbNomSoumis.Text <> "" And CmbCodeArticle.Text <> "") Then
                Modif = True
                TxtPuArticle.Properties.ReadOnly = False
                GridSaisieSpecTech.EmbeddedNavigator.Buttons.Append.Enabled = True
                ViewSaisieSpecTech.OptionsBehavior.Editable = True
                BtValiderOffre.Enabled = True
                BtModifier.Enabled = False
            End If

        ElseIf TxtTypeMarche.Text.ToLower = "Travaux".ToLower Then
            If CmbNumDAO.SelectedIndex <> -1 And CmbNumLot.SelectedIndex <> -1 And CmbNomSoumis.SelectedIndex <> -1 And CmbCodeSection.SelectedIndex <> -1 Then
                query = "select A.* from T_SoumisPrixItemDQE AS A, T_DQEItem AS B where A.RefItem=B.RefItem and B.RefSection='" & CodeSectionCache.Text & "' and A.RefSoumis='" & RefSoumisCache.Text & "' and A.Mention<>''"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                If dt1.Rows.Count > 0 Then
                    FailMsg("Modification impossible, car l'offre a été analysée.")
                    Exit Sub
                End If

                Modif = True
                GridSaisieDQE.EmbeddedNavigator.Buttons.Append.Enabled = True
                ViewSaisieDQE.OptionsBehavior.Editable = True
                BtValiderOffre.Enabled = True
                BtModifier.Enabled = False
            End If
        End If

    End Sub

    Private Sub ContextMenuStripSaisieSpec_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStripSaisieSpec.Opening
        If ViewSaisieSpecTech.RowCount = 0 Then
            e.Cancel = True
        End If
        If ViewSaisieSpecTech.GetRowCellValue(ViewSaisieSpecTech.FocusedRowHandle, "Syst").ToString() = "Required" Then
            e.Cancel = True
        End If
        If Modif = False Then
            If ViewSaisieSpecTech.GetRowCellValue(ViewSaisieSpecTech.FocusedRowHandle, "RefSpecCaract").ToString() <> "" Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub DeleteSousLot_Click(sender As Object, e As EventArgs) Handles DeleteSousLot.Click
        If Modif = True Then
            If ViewSaisieSpecTech.RowCount > 0 Then
                query = "DELETE from t_soumiscaractfournitsupl where RefSpecCaract='" & ViewSaisieSpecTech.GetRowCellValue(ViewSaisieSpecTech.FocusedRowHandle, "RefSpecCaract") & "' AND RefSoumis='" & RefSoumisCache.Text & "'"
                ExecuteNonQuery(query)
                query = "DELETE from t_spectechcaractpropose where RefSpecFournit='" & CodeFournitCache.Text & "' AND RefSpecCaractPro='" & ViewSaisieSpecTech.GetRowCellValue(ViewSaisieSpecTech.FocusedRowHandle, "RefSpecCaract") & "'"
                ExecuteNonQuery(query)
                ViewSaisieSpecTech.GetDataRow(ViewSaisieSpecTech.FocusedRowHandle).Delete()
            End If
        Else
            If ViewSaisieSpecTech.RowCount > 0 Then
                ViewSaisieSpecTech.GetDataRow(ViewSaisieSpecTech.FocusedRowHandle).Delete()
            End If
        End If
    End Sub

    Private Sub GridSaisieSpecTech_EmbeddedNavigator_ButtonClick(sender As Object, e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridSaisieSpecTech.EmbeddedNavigator.ButtonClick, GridSaisieDQE.EmbeddedNavigator.ButtonClick
        'Dim NumDernier As Integer
        'NumDernier = ViewSaisieSpecTech.GetRowCellValue(ViewSaisieSpecTech.RowCount - 1, "N°")
    End Sub
    Private Sub BtAnnuler_Click(sender As Object, e As EventArgs) Handles BtAnnuler.Click
        If TxtTypeMarche.Text.ToLower = "Fournitures".ToLower Then
            If Modif = True Then
                BtValiderOffre.Enabled = False
                BtModifier.Enabled = True
                Modif = False
                AfficherCaracteristiques(CodeFournitCache.Text)
            End If

        ElseIf TxtTypeMarche.Text.ToLower = "Travaux".ToLower Then
            If CmbNumDAO.SelectedIndex <> -1 And CmbNumLot.SelectedIndex <> -1 And CmbNomSoumis.SelectedIndex <> -1 And CmbCodeSection.SelectedIndex <> -1 Then
                ChargerDonneDQE_Saisie()
                Modif = False
            End If
        End If
    End Sub

    Private Sub SaisieOffres_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Modif = False
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Try
            If ViewSaisieDQE.RowCount > 0 Then
                DrX = ViewSaisieDQE.GetDataRow(ViewSaisieDQE.FocusedRowHandle)
                If DrX("Syst").ToString = "Required" Then
                    FailMsg("Impossible de supprimer la ligne sélectionnée.")
                    Exit Sub
                End If

                If DrX("RefItem").ToString <> "" Then 'Ligne déjà enregistrée.
                    ExecuteNonQuery("DELETE from t_fournisseur_dqeitem_propose where RefDqeitemPro='" & DrX("RefItem").ToString & "'")
                    ExecuteNonQuery("DELETE from t_soumis_dqeitem_propose where RefDqeitemPro='" & DrX("RefItem").ToString & "'")
                End If

                ViewSaisieDQE.GetDataRow(ViewSaisieDQE.FocusedRowHandle).Delete()
            End If
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub ContextMenuStrip1DQE_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1DQE.Opening
        If ViewSaisieDQE.RowCount = 0 Then
            e.Cancel = True
        End If
        If ViewSaisieDQE.GetRowCellValue(ViewSaisieDQE.FocusedRowHandle, "Syst").ToString() = "Required" Then
            e.Cancel = True
        End If

        If Modif = False Then
            If ViewSaisieDQE.GetRowCellValue(ViewSaisieDQE.FocusedRowHandle, "RefItem").ToString() <> "" Then
                e.Cancel = True
            End If
        End If
    End Sub
End Class