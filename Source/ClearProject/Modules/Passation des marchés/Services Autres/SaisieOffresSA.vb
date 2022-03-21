Imports MySql.Data.MySqlClient
Imports ClearProject.PassationMarche
Imports DevExpress.XtraEditors.Controls

Public Class SaisieOffresSA
    Dim dtCojo = New DataTable
    Dim dt = New DataTable()
    Dim dt2 = New DataTable()
    Dim Modif = False
    Private Sub SaisieOffres_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ItemsCmbDao()
    End Sub

    Private Sub ItemsCmbDao()
        query = "select NumeroDAO from T_DAO where CodeProjet='" & ProjetEnCours & "' and DateFinouverture<>'' order by NumeroDAO"
        CmbNumDAO.Properties.Items.Clear()
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbNumDAO.Properties.Items.Add(rw(0).ToString)
        Next
    End Sub

    Private Sub CmbNumDAO_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumDAO.SelectedValueChanged
        InitGbLot()
        RemplirTabSpecDemande(String.Empty)

        If (CmbNumDAO.Text <> "") Then
            query = "select DateDebutOuverture,IntituleDAO,TypeMarche,NbreLotDAO from T_DAO where CodeProjet='" & ProjetEnCours & "' and NumeroDAO='" & CmbNumDAO.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                Dim partOuv() As String = rw(0).ToString.Split(" "c)
                TxtDateOuverture.Text = partOuv(0)
                TxtLibelleDAO.Text = MettreApost(rw(1).ToString)
                TxtTypeMarche.Text = rw(2).ToString
                TxtNbLot.Text = rw(3).ToString

                CmbNumLot.Properties.Items.Clear()
                For k As Integer = 1 To CInt(rw(3))
                    CmbNumLot.Properties.Items.Add(k.ToString)
                Next
            Next

            query = "select Count(*) from T_Fournisseur where CodeProjet='" & ProjetEnCours & "' and NumeroDAO='" & CmbNumDAO.Text & "' and DateDepotDAO<>''"
            Dim NbreSoumis = Val(ExecuteScallar(query))
            TxtNbSoumis.Text = NbreSoumis.ToString
            If (TxtTypeMarche.Text = "Travaux") Then
                GbArticles.Enabled = False
                GbSpecDemandes.Text = "Sommes à valoir"
                GbSpecOffertes.Text = "Dévis quatitatif estimé"
                GbPrixArticle.Visible = False
                GbSection.Visible = True
            Else
                GbArticles.Enabled = True
                GbSpecDemandes.Text = "Spécifications demandées"
                GbSpecOffertes.Text = "Spécifications offertes"
                GbPrixArticle.Visible = True
                GbSection.Visible = False
            End If
        End If

    End Sub

    Private Sub CmbSousLot_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSousLot.SelectedValueChanged
        'ZoneAffichage.Controls.Clear()
        CmbCodeSection.Text = ""
        TxtLibelleSection.Text = ""
        CmbNomSoumis.Text = ""

        If (CmbSousLot.Text <> "") Then
            query = "select LibelleSousLot from T_LotDAO_SousLot where CodeSousLot='" & CmbSousLot.Text & "' and NumeroDAO='" & CmbNumDAO.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtSousLot.Text = MettreApost(rw(0).ToString)
            Next
            query = "select distinct l.* from T_SpecTechFourniture l, t_lotdao_souslot s where l.NumeroDAO='" & CmbNumDAO.Text & "' and l.CodeLot='" & CmbNumLot.Text & "' and s.CodeSousLot='" & CmbSousLot.Text & "' and s.CodeSousLot=l.CodeSousLot"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtNbSectionArticle.Text = CInt(dt0.Rows.Count)
            Next
            InitGbArticle()
            CmbCodeArticle.Properties.Items.Clear()
            query = "select distinct CodeFournit from T_SpecTechFourniture where NumeroDAO='" & CmbNumDAO.Text & "' and CodeLot='" & CmbNumLot.Text & "' and CodeSousLot='" & CmbSousLot.Text & "' order by CodeFournit"
            dt0 = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                CmbCodeArticle.Properties.Items.Add(rw(0).ToString)
            Next
            ItemsNomSoumis()
            'Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDAO.Text)
            'Dim nbsouslot As Integer = Val(Resultat(0))
            'If nbsouslot > 0 Then
            '    CmbSousLot.Properties.Items.Clear()
            '    Dim dt As DataTable = CType(Resultat(1), DataTable)
            '    For Each rw As DataRow In dt.Rows
            '        CmbSousLot.Properties.Items.Add(rw(0).ToString)
            '    Next
            'End If
            'CmbCodeSection.Enabled = True
            'CmbCodeSection.Properties.Items.Clear()
            'query = "select NumeroSection from T_DQESection where NumeroDAO='" & CmbNumDAO.Text & "' and CodeLot='" & CmbNumLot.Text & "' and CodeSousLot='" & CmbSousLot.Text & "' order by NumeroSection"
            'dt = ExcecuteSelectQuery(query)
            'For Each rw As DataRow In dt0.Rows
            '    CmbCodeSection.Properties.Items.Add(rw(0).ToString)
            'Next
        End If
    End Sub

    Private Sub CmbNumLot_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumLot.SelectedValueChanged
        'ZoneAffichage.Controls.Clear()
        InitGbSoumis()

        If (CmbNumLot.Text <> "") Then

            Dim RefDuLot As String = ""
            query = "select LibelleLot,RefLot from T_LotDAO where NumeroDAO='" & CmbNumDAO.Text & "' and CodeLot='" & CmbNumLot.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtLibelleLot.Text = MettreApost(rw(0).ToString)
                RefDuLot = rw(1).ToString
            Next

            If (TxtTypeMarche.Text = "Travaux") Then
                LabelControl18.Text = "Nombre de sections"
                Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDAO.Text)
                Dim nbsouslot As Integer = Val(Resultat(0))
                If nbsouslot > 0 Then
                    CmbSousLot.Enabled = True
                    CmbSousLot.Text = ""
                    TxtSousLot.Text = ""
                    CmbSousLot.Properties.Items.Clear()
                    Dim dt As DataTable = CType(Resultat(1), DataTable)
                    For Each rw As DataRow In dt.Rows
                        CmbSousLot.Properties.Items.Add(rw("CodeSousLot").ToString)
                    Next
                Else
                    CmbSousLot.Text = ""
                    TxtSousLot.Text = ""
                    CmbSousLot.Enabled = False
                    query = "select Count(*) from T_DQESection where NumeroDAO='" & CmbNumDAO.Text & "' and CodeLot='" & CmbNumLot.Text & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        TxtNbSectionArticle.Text = CInt(rw(0))
                    Next
                End If
            Else
                LabelControl18.Text = "Nombre d'articles"
                Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDAO.Text)
                Dim nbsouslot As Integer = Val(Resultat(0))
                If nbsouslot > 0 Then
                    CmbSousLot.Enabled = True
                    CmbSousLot.Text = ""
                    TxtSousLot.Text = ""
                    CmbSousLot.Properties.Items.Clear()
                    Dim dt As DataTable = CType(Resultat(1), DataTable)
                    For Each rw As DataRow In dt.Rows
                        CmbSousLot.Properties.Items.Add(rw("CodeSousLot").ToString)
                    Next
                Else
                    CmbSousLot.Text = ""
                    TxtSousLot.Text = ""
                    CmbSousLot.Enabled = False
                    query = "select Count(*) from T_SpecTechFourniture where NumeroDAO='" & CmbNumDAO.Text & "' and CodeLot='" & CmbNumLot.Text & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        TxtNbSectionArticle.Text = CInt(rw(0))
                    Next
                End If
            End If

            ItemsNomSoumis()

            'Code de remplissage combo articles
            If (TxtTypeMarche.Text = "Fournitures") Then

                Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDAO.Text)
                Dim nbsouslot As Integer = Val(Resultat(0))
                If nbsouslot = 0 Then
                    CmbCodeArticle.Properties.Items.Clear()
                    query = "select distinct CodeFournit from T_SpecTechFourniture where NumeroDAO='" & CmbNumDAO.Text & "' and CodeLot='" & CmbNumLot.Text & "' order by CodeFournit"
                    dt0 = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        CmbCodeArticle.Properties.Items.Add(rw(0).ToString)
                    Next
                Else
                    CmbCodeArticle.Properties.Items.Clear()
                End If
            Else               ' Travaux ***************

                CmbSousLot.Text = ""
                TxtSousLot.Text = ""
                Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDAO.Text)
                Dim nbsouslot As Integer = Val(Resultat(0))
                If nbsouslot = 0 Then
                    CmbCodeSection.Properties.Items.Clear()
                    query = "select NumeroSection from T_DQESection where NumeroDAO='" & CmbNumDAO.Text & "' and CodeLot='" & CmbNumLot.Text & "' order by NumeroSection"
                    dt0 = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        CmbCodeSection.Properties.Items.Add(rw(0).ToString)
                    Next
                Else
                    CmbCodeSection.Properties.Items.Clear()
                End If
                'Dim nbSLot As Decimal = 0
                'query = "select Count(*) from T_LotDAO_SousLot where RefLot='" & RefDuLot & "'"
                'dt0 = ExcecuteSelectQuery(query)
                'If dt0.Rows.Count > 0 Then
                '    nbSLot = CInt(dt.Rows(0).Item(0))
                'End If


                'If (nbSLot > 0) Then
                '    CmbSousLot.Enabled = True
                '    CmbCodeSection.Enabled = False
                '    query = "select CodeSousLot from T_LotDAO_SousLot where RefLot='" & RefDuLot & "'"

                '    CmbSousLot.Properties.Items.Clear()
                '    dt0 = ExcecuteSelectQuery(query)
                '    For Each rw As DataRow In dt0.Rows
                '        CmbSousLot.Properties.Items.Add(rw(0).ToString)
                '    Next
                'Else
                '    CmbSousLot.Enabled = False
                '    CmbCodeSection.Enabled = True

                '    CmbCodeSection.Properties.Items.Clear()
                '    query = "select NumeroSection from T_DQESection where NumeroDAO='" & CmbNumDAO.Text & "' and CodeLot='" & CmbNumLot.Text & "' order by NumeroSection"
                '    dt0 = ExcecuteSelectQuery(query)
                '    For Each rw As DataRow In dt0.Rows
                '        CmbCodeSection.Properties.Items.Add(rw(0).ToString)
                '    Next


                'End If

            End If


            'Sommes à valoir
            If (TxtTypeMarche.Text = "Travaux") Then
                Dim SomAvaloir As Decimal = 0
                Dim nbSyst As Decimal = 0

                dt.Columns.Clear()

                dt.Columns.Add("N°", Type.GetType("System.String"))
                dt.Columns.Add("Spécification", Type.GetType("System.String"))
                dt.Columns.Add("Quantité", Type.GetType("System.String"))
                dt.Columns.Add("Prix U. HTVA", Type.GetType("System.String"))
                dt.Columns.Add("Montant HTVA", Type.GetType("System.String"))
                dt.Columns.Add("Prix U. Lettre", Type.GetType("System.String"))
                dt.Columns.Add("Syst", Type.GetType("System.String"))

                dt.Rows.Clear()
                query = "select S.NumeroSection,I.NumeroItem,I.Designation,I.QteItem,I.UniteItem,I.PuHtva,I.MontHtva,I.PuHtvaLettre from T_DQESection as S, T_DQEItem as I where S.RefSection=I.RefSection and S.NumeroDAO='" & CmbNumDAO.Text & "' and S.CodeLot='" & CmbNumLot.Text & "' and I.PuHtva='0'"
                dt0 = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    'SomAvaloir = SomAvaloir + CDec(rw(6))
                    nbSyst = nbSyst + 1

                    'Grid
                    Dim dr = dt.NewRow()

                    dr(0) = rw(0).ToString & "." & rw(1).ToString
                    dr(1) = MettreApost(rw(2).ToString)
                    dr(2) = rw(3).ToString & " " & rw(4).ToString
                    dr(3) = rw(5).ToString
                    dr(4) = rw(6).ToString
                    dr(5) = MettreApost(rw(7).ToString)
                    If (CDec(nbSyst) / 2 = CDec(nbSyst) \ 2) Then dr(6) = "x"

                    dt.Rows.Add(dr)
                Next


                'GridSpecDemandes.DataSource = dt
                'GridViewSpec.Columns(0).Width = 40
                'GridViewSpec.Columns(1).Width = 200
                'GridViewSpec.Columns(2).Width = 60
                'GridViewSpec.Columns(3).Width = 150
                'GridViewSpec.Columns(4).Width = 150
                'GridViewSpec.Columns(5).Width = 500
                'GridViewSpec.Columns(6).Visible = False

                ColorRowGrid(GridViewSpec, "[Syst]='x'", Color.Silver, "Tahoma", 8, FontStyle.Regular, Color.Black)

                GbSpecDemandes.Text = "Sommes à valoir (" & AfficherMonnaie(SomAvaloir.ToString) & ")"

            End If

        End If

    End Sub

    Private Sub ItemsNomSoumis()
        Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDAO.Text)
        Dim nbsouslot As Integer = Val(Resultat(0))
        If nbsouslot > 0 Then
            query = "select distinct F.NomFournis from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & CmbNumDAO.Text & "' and S.CodeLot='" & CmbNumLot.Text & "' AND S.CodeSousLot='" & CmbSousLot.Text & "'"
        Else
            query = "select distinct F.NomFournis from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & CmbNumDAO.Text & "' and S.CodeLot='" & CmbNumLot.Text & "'"
        End If
        CmbNomSoumis.Properties.Items.Clear()
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbNomSoumis.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next
    End Sub

    Private Sub CmbNomSoumis_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNomSoumis.SelectedValueChanged
        RefSoumisCache.Text = ""
        ZoneAffichage.Controls.Clear()
        If (TxtTypeMarche.Text = "Fournitures") Then
            InitGbArticle()
        Else
            CmbCodeSection.Text = ""
            TxtLibelleSection.Text = ""
            ZoneAffichage.Controls.Clear()
        End If

        If (CmbNomSoumis.Text <> "") Then
            Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDAO.Text)
            Dim nbsouslot As Integer = Val(Resultat(0))
            If nbsouslot > 0 Then
                query = "select F.PaysFournis,F.AdresseCompleteFournis,F.TelFournis,F.FaxFournis,F.CelFournis,F.MailFournis,S.Monnaie,S.HtHdTtc,S.MontantPropose,S.RefSoumis from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NomFournis='" & EnleverApost(CmbNomSoumis.Text) & "' and F.NumeroDAO='" & CmbNumDAO.Text & "' and S.CodeLot='" & CmbNumLot.Text & "'AND S.CodeSousLot='" & CmbSousLot.Text & "'"
            Else
                query = "select F.PaysFournis,F.AdresseCompleteFournis,F.TelFournis,F.FaxFournis,F.CelFournis,F.MailFournis,S.Monnaie,S.HtHdTtc,S.MontantPropose,S.RefSoumis from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NomFournis='" & EnleverApost(CmbNomSoumis.Text) & "' and F.NumeroDAO='" & CmbNumDAO.Text & "' and S.CodeLot='" & CmbNumLot.Text & "'"
            End If
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtPaysSoumis.Text = MettreApost(rw(0).ToString)
                Dim AdrSoumis As String = ""
                If (rw(1).ToString <> "") Then AdrSoumis = MettreApost(rw(1).ToString)
                If (rw(2).ToString <> "") Then AdrSoumis = AdrSoumis & vbNewLine & "Tél : " & rw(2).ToString
                If (rw(3).ToString <> "") Then AdrSoumis = AdrSoumis & vbNewLine & "Fax : " & rw(3).ToString
                If (rw(4).ToString <> "") Then AdrSoumis = AdrSoumis & vbNewLine & "Cel : " & rw(4).ToString
                If (rw(5).ToString <> "") Then AdrSoumis = AdrSoumis & vbNewLine & "E-mail : " & rw(5).ToString
                TxtAdresseSoumis.Text = AdrSoumis
                TxtMonnaie.Text = rw(6).ToString
                TxtTaxes.Text = rw(7).ToString
                TxtMontantLot.Text = AfficherMonnaie(rw(8).ToString)
                RefSoumisCache.Text = rw(9).ToString
            Next
        End If
    End Sub

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
        If (TxtTypeMarche.Text = "Fournitures") Then
            InitGbArticle()
        Else
            CmbCodeSection.Text = ""
            TxtLibelleSection.Text = ""
            'ZoneAffichage.Controls.Clear()
        End If
    End Sub

    Private Sub InitGbArticle()
        CmbCodeArticle.Text = ""
        TxtNomArticle.Text = ""
        TxtQteArticle.Text = ""
        TxtCategorieArticle.Text = ""
        TxtSousCategorieArticle.Text = ""
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

    Private Sub CmbCodeArticle_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCodeArticle.SelectedValueChanged
        InitGbPrixArticle()
        InitTabSaisieOffre()

        If (CmbNomSoumis.Text <> "" And CmbCodeArticle.Text <> "") Then
            Dim Categor As String() = {"", ""}

            Dim CodeFournit As String = ""
            Dim Resultat As Object() = GetSousLot(CmbNumLot.Text, CmbNumDAO.Text)
            Dim nbsouslot As Integer = Val(Resultat(0))
            If nbsouslot > 0 Then
                query = "select DescripFournit,QteFournit,UniteFournit,LieuLivraison,CodeCategorie,RefSpecFournit from T_SpecTechFourniture where NumeroDAO='" & CmbNumDAO.Text & "' and CodeLot='" & CmbNumLot.Text & "' and CodeFournit='" & CmbCodeArticle.Text & "' and CodeSousLot='" & CmbSousLot.Text & "'"
            Else
                query = "select DescripFournit,QteFournit,UniteFournit,LieuLivraison,CodeCategorie,RefSpecFournit from T_SpecTechFourniture where NumeroDAO='" & CmbNumDAO.Text & "' and CodeLot='" & CmbNumLot.Text & "' and CodeFournit='" & CmbCodeArticle.Text & "'"
            End If
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw0 As DataRow In dt0.Rows
                TxtNomArticle.Text = MettreApost(rw0(0).ToString)
                TxtQteArticle.Text = rw0(1).ToString & " " & rw0(2).ToString
                QteFournitCache.Text = rw0(1).ToString
                TxtLieuLivraison.Text = MettreApost(rw0(3).ToString)
                Categor = rw0(4).ToString.Split("-")
                CodeFournit = rw0(5).ToString
                CodeFournitCache.Text = CodeFournit
            Next
            If Categor(1).ToString = "Cat" Then
                query = "select * from T_PredFournitures_Groupe where IdCat='" & Categor(0).ToString & "'"
                dt = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    TxtCategorieArticle.Text = MettreApost(rw("LibelleCat").ToString)
                Next
            Else
                query = "select c.* from T_PredFournitures_Groupe c, t_predfournitures_sous_groupe s where s.IdSousCat='" & Categor(0).ToString & "' and c.idcat=s.idcat "
                dt = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    TxtCategorieArticle.Text = MettreApost(rw("LibelleCat").ToString)
                Next
                query = "select * from t_predfournitures_sous_groupe where IdSousCat='" & Categor(0).ToString & "'"
                dt = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    TxtSousCategorieArticle.Text = MettreApost(rw("LibelleSousCat").ToString)
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
            query = "SELECT COUNT(*) FROM t_spectechcaract a , t_soumiscaractfournit b WHERE a.RefSpecCaract=b.RefSpecCaract AND b.RefSoumis='" & RefSoumisCache.Text & "' AND a.RefSpecFournit='" & CodeFournit & "'"
            Dim valeur = Val(ExecuteScallar(query))
            If valeur > 0 Then
                AfficherCaracteristiques(CodeFournit)
                BtModifier.Enabled = True
                BtValiderOffre.Enabled = False
            Else
                AjouterCaracteristiques(CodeFournit)
                BtValiderOffre.Enabled = True
                BtModifier.Enabled = False
            End If
            RemplirTabSpecDemande(CodeFournit)

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
    Private Sub RemplirTabSpecDemande(ByVal leCode As String)
        dt.Columns.Clear()

        dt.Columns.Add("N°", Type.GetType("System.String"))
        dt.Columns.Add("Spécification", Type.GetType("System.String"))
        dt.Columns.Add("Valeur", Type.GetType("System.String"))
        dt.Columns.Add("Syst", Type.GetType("System.String"))

        dt.Rows.Clear()
        Dim nbElt As Decimal = 0
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
        Next

        GridSpecDemandes.DataSource = dt
        GridViewSpec.OptionsView.ColumnAutoWidth = True

        'GridViewSpec.Columns.Item(0).Width = 40
        'GridViewSpec.Columns.Item(1).Width = 300
        'GridViewSpec.Columns.Item(2).Width = 500
        GridViewSpec.Columns.Item(3).Visible = False
        ColorRowGrid(GridViewSpec, "[Syst]='x'", Color.Silver, "Times New Roman", 10, FontStyle.Regular, Color.Black)
    End Sub
    Private Sub AfficherCaracteristiques(ByVal leCode As String)
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
        query = "select LibelleCaract,RefSpecCaract from T_SpecTechCaract  where RefSpecFournit='" & leCode & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            query = "select distinct ValeurOfferte from T_SoumisCaractFournit where RefSpecCaract='" & rw("RefSpecCaract") & "' and RefSoumis='" & RefSoumisCache.Text & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw0 In dt1.Rows
                Dim dr1 = dt.NewRow()
                nbElt2 = nbElt2 + 1
                dr1(0) = nbElt2
                dr1(1) = rw("RefSpecCaract").ToString
                dr1(2) = MettreApost(rw("LibelleCaract").ToString)
                dr1(3) = MettreApost(rw0("ValeurOfferte").ToString)
                dr1(4) = "Required"
                dt.Rows.Add(dr1)
            Next
        Next
        query = "select distinct a.LibelleCaract,a.RefSpecCaractPro, b.ValeurOfferte from t_spectechcaractpropose a, t_soumiscaractfournitsupl b where a.RefSpecFournit='" & leCode & "' And a.RefSpecCaractPro=b.RefSpecCaract and b.RefSoumis='" & RefSoumisCache.Text & "'"
        Dim dt3 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt3.Rows
            Dim dr1 = dt.NewRow()
            nbElt2 = nbElt2 + 1
            dr1(0) = nbElt2
            dr1(1) = rw(1).ToString
            dr1(2) = MettreApost(rw(0).ToString)
            dr1(3) = MettreApost(rw(2).ToString)
            dr1(4) = ""
            dt.Rows.Add(dr1)
        Next
        query = "select PrixUnitaire from T_SoumisPrixFourniture where RefSpecFournit='" & CodeFournitCache.Text & "' and RefSoumis='" & RefSoumisCache.Text & "'"
        Dim dt4 = ExcecuteSelectQuery(query)
        For Each rw0 As DataRow In dt4.Rows
            TxtPuArticle.Text = rw0(0).ToString
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
    Private Sub AjouterCaracteristiques(ByVal leCode As String)
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
        query = "select LibelleCaract,RefSpecCaract from T_SpecTechCaract  where RefSpecFournit='" & leCode & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            Dim dr1 = dt.NewRow()
            nbElt2 = nbElt2 + 1
            dr1(0) = nbElt2
            dr1(1) = rw("RefSpecCaract").ToString
            dr1(2) = MettreApost(rw("LibelleCaract").ToString)
            dr1(3) = ""
            dr1(4) = "Required"
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

    Private Sub CmbCodeSection_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCodeSection.SelectedValueChanged
        'ZoneAffichage.Controls.Clear()
        CodeSectionCache.Text = ""
        CmbSousSect.Properties.Items.Clear()
        CmbSousSect.Enabled = False
        CmbSousSect.Text = ""
        TxtSousSect.Text = ""

        If (CmbCodeSection.Text <> "") Then
            query = "select Designation,RefSection from T_DQESection where NumeroDAO='" & CmbNumDAO.Text & "' and CodeLot='" & CmbNumLot.Text & "' and NumeroSection='" & CmbCodeSection.Text & "' and CodeSousLot='" & IIf(CmbSousLot.Enabled = True, CmbSousLot.Text, "").ToString & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtLibelleSection.Text = MettreApost(rw(0).ToString)
                CodeSectionCache.Text = rw(1).ToString
            Next


            ' Sous section *************************
            query = "select NumeroSousSection,RefSousSection from T_DQESection_SousSection where NumeroDAO='" & CmbNumDAO.Text & "' and RefSection='" & CodeSectionCache.Text & "' order by RefSousSection"
            dt = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                CmbSousSect.Enabled = True
            End If
            For Each rw As DataRow In dt0.Rows
                CmbSousSect.Properties.Items.Add(rw(0).ToString)
            Next
            If (CmbSousSect.Enabled = False) Then
                AjouterItemDQE(CmbSousLot.Text, "")
            End If

        End If
    End Sub

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

    Private Sub TxtPuArticle_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPuArticle.TextChanged
        If (TxtPuArticle.Text <> "" And QteFournitCache.Text <> "") Then
            TxtPtArticle.Text = AfficherMonnaie((CDec(TxtPuArticle.Text) * CDec(QteFournitCache.EditValue)).ToString)
            TxtPuArticleLettre.Text = MontantLettre(TxtPuArticle.EditValue.ToString.Replace(" ", ""))
        Else
            TxtPtArticle.Text = "0"
            TxtPuArticleLettre.Text = ""
        End If
    End Sub

    Private Sub BtValiderOffre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtValiderOffre.Click
        If Modif = False Then
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            If (TxtTypeMarche.Text = "Fournitures") Then

                Dim EvalEffectuee As Boolean = False
                If Val(TxtPuArticle.EditValue) <= 0 Then
                    SuccesMsg("Veuillez saisir le prix unitaire de l'article")
                    Exit Sub
                End If
                query = "SELECT * FROM t_commission WHERE NumeroDAO='" & CmbNumDAO.Text & "'"
                dtCojo = ExcecuteSelectQuery(query)
                'query = "select RefSpecCaract from T_SpecTechCaract where RefSpecFournit='" & CodeFournitCache.Text & "'"
                'Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                'For Each rw As DataRow In dt0.Rows
                '    For Each Paneau In ZoneAffichage.Controls
                '        For Each TxtZone As DevExpress.XtraEditors.BaseControl In Paneau.controls
                '            If (TxtZone.Name = "Txt" & rw(0).ToString And EvalEffectuee = False) Then
                '                query = "select * from T_SoumisCaractFournit where RefSpecCaract='" & rw(0).ToString & "' and RefSoumis='" & RefSoumisCache.Text & "' and MentionValeur<>''"
                '                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                '                If dt1.Rows.Count > 0 Then
                '                    EvalEffectuee = True
                '                End If
                '                If (EvalEffectuee = False) Then
                '                    query = "DELETE from T_SoumisCaractFournit where RefSoumis='" & RefSoumisCache.Text & "' and RefSpecCaract='" & rw(0).ToString & "'"
                '                    ExecuteNonQuery(query)


                '                    'If (TxtZone.Text.Replace(" ", "") <> "") Then
                '                    Dim DatSet = New DataSet
                '                    query = "select * from T_SoumisCaractFournit"

                '                    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                '                    Dim DatAdapt = New MySqlDataAdapter(Cmd)
                '                    DatAdapt.Fill(DatSet, "T_SoumisCaractFournit")
                '                    Dim DatTable = DatSet.Tables("T_SoumisCaractFournit")
                '                    Dim DatRow = DatSet.Tables("T_SoumisCaractFournit").NewRow()

                '                    DatRow("RefSoumis") = RefSoumisCache.Text
                '                    DatRow("RefSpecCaract") = rw(0).ToString
                '                    DatRow("ValeurOfferte") = EnleverApost(TxtZone.Text)

                '                    DatSet.Tables("T_SoumisCaractFournit").Rows.Add(DatRow)
                '                    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                '                    DatAdapt.Update(DatSet, "T_SoumisCaractFournit")
                '                    DatSet.Clear()

                '                    'End If

                '                End If

                '                'End If
                '            End If
                '        Next
                '    Next
                'Next
                query = "select RefSpecCaract from T_SpecTechCaract where RefSpecFournit='" & CodeFournitCache.Text & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    If (ViewSaisieSpecTech.RowCount > 0 And EvalEffectuee = False) Then
                        query = "select * from T_SoumisCaractFournit where RefSpecCaract='" & rw(0).ToString & "' and RefSoumis='" & RefSoumisCache.Text & "' and MentionValeur<>''"
                        'query = "select S.* from T_SoumisCaractFournit S , t_jugementcojodao J where S.RefSpecCaract='" & rw(0).ToString & "' and S.RefSoumis='" & RefSoumisCache.Text & "' And S.Id_Caractfournit=J.Id_Caractfournit "
                        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                        If dt1.Rows.Count > 0 Then
                            EvalEffectuee = True
                        End If

                        If (EvalEffectuee = False) Then
                            For i = 0 To ViewSaisieSpecTech.RowCount - 1
                                If Not IsDBNull(ViewSaisieSpecTech.GetRowCellValue(i, "Syst")) Then
                                    If ViewSaisieSpecTech.GetRowCellValue(i, "Syst") = "Required" Then
                                        query = "DELETE from T_SoumisCaractFournit where RefSoumis='" & RefSoumisCache.Text & "' and RefSpecCaract='" & ViewSaisieSpecTech.GetRowCellValue(i, "RefSpecCaract") & "'"
                                        ExecuteNonQuery(query)
                                        For Each rw1 In dtCojo.Rows
                                            If ViewSaisieSpecTech.GetRowCellValue(i, "Valeur") = "" Then
                                                Dim valeurOfferte = "Aucune information donnée"
                                                query = "INSERT INTO T_SoumisCaractFournit(RefSpecCaract,RefSoumis, ValeurOfferte,ID_COJO) Values('" & ViewSaisieSpecTech.GetRowCellValue(i, "RefSpecCaract") & "','" & RefSoumisCache.Text & "','" & valeurOfferte & "','" & rw1("CodeMem").ToString & "')"
                                            Else
                                                query = "INSERT INTO T_SoumisCaractFournit(RefSpecCaract,RefSoumis, ValeurOfferte,ID_COJO) Values('" & ViewSaisieSpecTech.GetRowCellValue(i, "RefSpecCaract") & "','" & RefSoumisCache.Text & "','" & ViewSaisieSpecTech.GetRowCellValue(i, "Valeur") & "','" & rw1("CodeMem").ToString & "')"
                                            End If
                                            ExecuteNonQuery(query)
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
                                query = "INSERT INTO t_spectechcaractpropose (RefSpecFournit,LibelleCaract) Values('" & CodeFournitCache.Text & "','" & ViewSaisieSpecTech.GetRowCellValue(i, "Spécification techniques") & "')"
                                ExecuteNonQuery(query)
                                query = "SELECT MAX(RefSpecCaractPro) FROM t_spectechcaractpropose"
                                Dim RefSpeCaract = Val(ExecuteScallar(query))
                                For Each rw1 In dtCojo.Rows
                                    query = "INSERT INTO t_soumiscaractfournitsupl(RefSpecCaract,RefSoumis, ValeurOfferte,ID_COJO) Values('" & RefSpeCaract & "','" & RefSoumisCache.Text & "','" & ViewSaisieSpecTech.GetRowCellValue(i, "Valeur") & "','" & rw1("CodeMem").ToString & "')"
                                    ExecuteNonQuery(query)
                                Next
                            End If
                        End If
                    Next
                End If
                If (EvalEffectuee = False) Then

                    'query = "DELETE from T_SoumisPrixFourniture where RefSoumis='" & RefSoumisCache.Text & "' and RefSpecFournit='" & CodeFournitCache.Text & "'"
                    'ExecuteNonQuery(query)
                    query = "INSERT INTO T_SoumisPrixFourniture (RefSoumis,RefSpecFournit,PrixUnitaire) Values('" & RefSoumisCache.Text & "','" & CodeFournitCache.Text & "','" & TxtPuArticle.EditValue.ToString.Replace(" ", "") & "')"
                    ExecuteNonQuery(query)
                    'Dim DatSet = New DataSet
                    'query = "select * from T_SoumisPrixFourniture"

                    'Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                    'Dim DatAdapt = New MySqlDataAdapter(Cmd)
                    'DatAdapt.Fill(DatSet, "T_SoumisPrixFourniture")
                    'Dim DatTable = DatSet.Tables("T_SoumisPrixFourniture")
                    'Dim DatRow = DatSet.Tables("T_SoumisPrixFourniture").NewRow()

                    'DatRow("RefSoumis") = RefSoumisCache.Text
                    'DatRow("RefSpecFournit") = CodeFournitCache.Text
                    'DatRow("PrixUnitaire") = TxtPuArticle.EditValue.ToString.Replace(" ", "")

                    'DatSet.Tables("T_SoumisPrixFourniture").Rows.Add(DatRow)
                    'Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                    'DatAdapt.Update(DatSet, "T_SoumisPrixFourniture")
                    'DatSet.Clear()
                End If
                'AjouterCaracteristiques(CodeFournitCache.Text)

                If (EvalEffectuee = True) Then
                    SuccesMsg("Traitement impossible! Offre déjà analysée.")
                Else
                    SuccesMsg("Enregistrement effectué avec succès.")
                    AfficherCaracteristiques(CodeFournitCache.Text)
                    BtModifier.Enabled = True
                    BtValiderOffre.Enabled = False
                End If

            Else     ' Pour les travaux *********

                Dim MontSection As Decimal = 0
                Dim EvalEffectuee As Boolean = False
                query = "select RefItem from T_DQEItem where RefSection='" & CodeSectionCache.Text & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows

                    For Each Paneau In ZoneAffichage.Controls
                        For Each TxtZone As DevExpress.XtraEditors.BaseControl In Paneau.Controls

                            If (TxtZone.Name = "Txt" & rw(0).ToString And EvalEffectuee = False) Then
                                query = "select * from T_SoumisPrixItemDQE where RefItem='" & rw(0).ToString & "' and RefSoumis='" & RefSoumisCache.Text & "' and Mention<>''"
                                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                                If dt1.Rows.Count > 0 Then
                                    EvalEffectuee = True
                                End If

                                If (EvalEffectuee = False) Then
                                    query = "DELETE from T_SoumisPrixItemDQE where RefSoumis='" & RefSoumisCache.Text & "' and RefItem='" & rw(0).ToString & "'"
                                    ExecuteNonQuery(query)

                                    Dim DatSet = New DataSet
                                    query = "select * from T_SoumisPrixItemDQE"

                                    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                                    Dim DatAdapt = New MySqlDataAdapter(Cmd)
                                    DatAdapt.Fill(DatSet, "T_SoumisPrixItemDQE")
                                    Dim DatTable = DatSet.Tables("T_SoumisPrixItemDQE")
                                    Dim DatRow = DatSet.Tables("T_SoumisPrixItemDQE").NewRow()

                                    DatRow("RefSoumis") = IIf(RefSoumisCache.Text = "", 0, RefSoumisCache.Text)
                                    DatRow("RefItem") = rw(0).ToString
                                    DatRow("MontantItem") = AfficherMonnaie(CDec(IIf(TxtZone.Text = "", 0, TxtZone.Text)).ToString.Replace(" ", ""))
                                    If (TxtZone.Text.Replace(" ", "") <> "") Then
                                        MontSection = MontSection + CDec(CDec(TxtZone.Text).ToString.Replace(" ", ""))
                                    End If

                                    DatSet.Tables("T_SoumisPrixItemDQE").Rows.Add(DatRow)
                                    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                                    DatAdapt.Update(DatSet, "T_SoumisPrixItemDQE")
                                    DatSet.Clear()

                                End If
                            End If

                        Next
                    Next
                Next

                If (EvalEffectuee = False) Then                        'TxtPuArticle.Text <> "" And 

                    query = "DELETE from T_SoumisPrixSectionDQE where RefSoumis='" & RefSoumisCache.Text & "' and RefSection='" & CodeSectionCache.Text & "'"
                    ExecuteNonQuery(query)


                    Dim DatSet = New DataSet
                    query = "select * from T_SoumisPrixSectionDQE"

                    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                    Dim DatAdapt = New MySqlDataAdapter(Cmd)
                    DatAdapt.Fill(DatSet, "T_SoumisPrixSectionDQE")
                    Dim DatTable = DatSet.Tables("T_SoumisPrixSectionDQE")
                    Dim DatRow = DatSet.Tables("T_SoumisPrixSectionDQE").NewRow()

                    DatRow("RefSoumis") = IIf(RefSoumisCache.Text = "", 0, RefSoumisCache.Text)
                    DatRow("RefSection") = CodeSectionCache.Text
                    DatRow("MontantSection") = MontSection.ToString

                    DatSet.Tables("T_SoumisPrixSectionDQE").Rows.Add(DatRow)
                    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                    DatAdapt.Update(DatSet, "T_SoumisPrixSectionDQE")
                    DatSet.Clear()


                End If

                AjouterItemDQE(CmbSousLot.Text, CmbSousSect.Text)

                If (EvalEffectuee = True) Then
                    SuccesMsg("Traitement impossible! Offre déjà analysée.")
                End If

            End If
            BDQUIT(sqlconn)
        Else
            Dim EvalEffectuee As Boolean = False
            query = "SELECT * FROM t_commission WHERE NumeroDAO='" & CmbNumDAO.Text & "'"
            dtCojo = ExcecuteSelectQuery(query)
            query = "select RefSpecCaract from T_SpecTechCaract where RefSpecFournit='" & CodeFournitCache.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                If (ViewSaisieSpecTech.RowCount > 0 And EvalEffectuee = False) Then
                    query = "select * from T_SoumisCaractFournit where RefSpecCaract='" & rw(0).ToString & "' and RefSoumis='" & RefSoumisCache.Text & "' and MentionValeur<>''"
                    'query = "select S.* from T_SoumisCaractFournit S , t_jugementcojodao J where S.RefSpecCaract='" & rw(0).ToString & "' and S.RefSoumis='" & RefSoumisCache.Text & "' And S.Id_Caractfournit=J.Id_Caractfournit "
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    If dt1.Rows.Count > 0 Then
                        EvalEffectuee = True
                    End If

                    If (EvalEffectuee = False) Then
                        For i = 0 To ViewSaisieSpecTech.RowCount - 1
                            If Not IsDBNull(ViewSaisieSpecTech.GetRowCellValue(i, "Syst")) Then
                                If ViewSaisieSpecTech.GetRowCellValue(i, "Syst") = "Required" Then
                                    If ViewSaisieSpecTech.GetRowCellValue(i, "Valeur") = "" Then
                                        Dim valeurOfferte = "Aucune information donnée"
                                        query = "UPDATE T_SoumisCaractFournit SET ValeurOfferte ='" & valeurOfferte & "' where RefSoumis='" & RefSoumisCache.Text & "' and RefSpecCaract='" & ViewSaisieSpecTech.GetRowCellValue(i, "RefSpecCaract") & "'"
                                    Else
                                        query = "UPDATE T_SoumisCaractFournit SET ValeurOfferte ='" & ViewSaisieSpecTech.GetRowCellValue(i, "Valeur") & "' where RefSoumis='" & RefSoumisCache.Text & "' and RefSpecCaract='" & ViewSaisieSpecTech.GetRowCellValue(i, "RefSpecCaract") & "'"
                                    End If
                                    ExecuteNonQuery(query)
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
                                query = "INSERT INTO t_spectechcaractpropose (RefSpecFournit,LibelleCaract) Values('" & CodeFournitCache.Text & "','" & ViewSaisieSpecTech.GetRowCellValue(i, "Spécification techniques") & "')"
                                ExecuteNonQuery(query)
                                query = "SELECT MAX(RefSpecCaractPro) FROM t_spectechcaractpropose"
                                Dim RefSpeCaract = Val(ExecuteScallar(query))
                                For Each rw1 In dtCojo.Rows
                                    query = "INSERT INTO t_soumiscaractfournitsupl(RefSpecCaract,RefSoumis, ValeurOfferte,ID_COJO) Values('" & RefSpeCaract & "','" & RefSoumisCache.Text & "','" & ViewSaisieSpecTech.GetRowCellValue(i, "Valeur") & "','" & rw1("CodeMem").ToString & "')"
                                    ExecuteNonQuery(query)
                                Next
                            End If
                        End If
                    End If
                    If IsDBNull(ViewSaisieSpecTech.GetRowCellValue(i, "Syst")) Then
                        If Not IsDBNull(ViewSaisieSpecTech.GetRowCellValue(i, "Spécification techniques")) And Not IsDBNull(ViewSaisieSpecTech.GetRowCellValue(i, "Valeur")) Then
                            query = "INSERT INTO t_spectechcaractpropose (RefSpecFournit,LibelleCaract) Values('" & CodeFournitCache.Text & "','" & ViewSaisieSpecTech.GetRowCellValue(i, "Spécification techniques") & "')"
                            ExecuteNonQuery(query)
                            query = "SELECT MAX(RefSpecCaractPro) FROM t_spectechcaractpropose"
                            Dim RefSpeCaract = Val(ExecuteScallar(query))
                            For Each rw1 In dtCojo.Rows
                                query = "INSERT INTO t_soumiscaractfournitsupl(RefSpecCaract,RefSoumis, ValeurOfferte,ID_COJO) Values('" & RefSpeCaract & "','" & RefSoumisCache.Text & "','" & ViewSaisieSpecTech.GetRowCellValue(i, "Valeur") & "','" & rw1("CodeMem").ToString & "')"
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
            If (EvalEffectuee = True) Then
                SuccesMsg("Traitement impossible! Offre déjà analysée.")
            Else
                SuccesMsg("Modification effectué avec succès.")
                AfficherCaracteristiques(CodeFournitCache.Text)
                Modif = False
                BtModifier.Enabled = True
                BtValiderOffre.Enabled = False
            End If
        End If

    End Sub

    Private Sub SaisieOffres_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub BtModifier_Click(sender As System.Object, e As System.EventArgs) Handles BtModifier.Click
        If (CmbNomSoumis.Text <> "" And CmbCodeArticle.Text <> "") Then
            'Dim Categor() As String
            'Dim CodeFournit As String = ""
            'query = "select DescripFournit,QteFournit,UniteFournit,LieuLivraison,CodeCategorie,RefSpecFournit from T_SpecTechFourniture where NumeroDAO='" & CmbNumDAO.Text & "' and CodeLot='" & CmbNumLot.Text & "' and CodeFournit='" & CmbCodeArticle.Text & "'"
            'Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            'For Each rw As DataRow In dt0.Rows
            '    TxtNomArticle.Text = MettreApost(rw(0).ToString)
            '    TxtQteArticle.Text = rw(1).ToString & " " & rw(2).ToString
            '    QteFournitCache.Text = rw(1).ToString
            '    TxtLieuLivraison.Text = MettreApost(rw(3).ToString)
            '    Categor = rw(4).ToString.Split("&"c)
            '    CodeFournit = rw(5).ToString
            '    CodeFournitCache.Text = CodeFournit
            'Next
            Modif = True
            TxtPuArticle.Properties.ReadOnly = False
            GridSaisieSpecTech.EmbeddedNavigator.Buttons.Append.Enabled = True
            ViewSaisieSpecTech.OptionsBehavior.Editable = True
            BtValiderOffre.Enabled = True
            BtModifier.Enabled = False
            'AjouterCaracteristiques5(CodeFournit)
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

    Private Sub GridSaisieSpecTech_EmbeddedNavigator_ButtonClick(sender As Object, e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridSaisieSpecTech.EmbeddedNavigator.ButtonClick
        'Dim NumDernier As Integer
        'NumDernier = ViewSaisieSpecTech.GetRowCellValue(ViewSaisieSpecTech.RowCount - 1, "N°")
    End Sub
    Private Sub BtAnnuler_Click(sender As Object, e As EventArgs) Handles BtAnnuler.Click
        If Modif = True Then
            BtValiderOffre.Enabled = False
            BtModifier.Enabled = True
            Modif = False
            AfficherCaracteristiques(CodeFournitCache.Text)
        End If
    End Sub

    Private Sub SaisieOffres_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Modif = False
    End Sub
End Class