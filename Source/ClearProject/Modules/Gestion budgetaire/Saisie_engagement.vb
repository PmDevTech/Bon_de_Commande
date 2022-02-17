Imports System.IO
Imports ClearProject.PassationMarche
Imports ClearProject.GestBudgetaire

Public Class Saisie_engagement

    Dim dtdoc = New DataTable()
    Dim drx As DataRow
    Dim codecat As String = ""
    Dim CodeProcAO As String = ""
    Dim DateDebutExercice As Date = CDate(ExerciceComptable.Rows(0)("datedebut"))
    Dim DateFinExercice As Date = CDate(ExerciceComptable.Rows(0)("datefin"))
    Dim TablBailleur As String()
    Dim TablPartition As String()
    Dim RefMarche As String = ""
    Dim ModePPM As String = ""
    Dim TypeMarches As String = ""

    Private Sub Saisie_engagement_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SimpleButton1_Click(Me, e)
    End Sub

    Private Sub Saisie_engagement_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        DateDebutExercice = CDate(ExerciceComptable.Rows(0)("datedebut"))
        DateFinExercice = CDate(ExerciceComptable.Rows(0)("datefin"))
        LoadMarcherGenere()
        typemarche()
        RemplirBailleur()
        RemplirFRS()

        'Date
        ' DateMarche.Properties.MinValue = ExerciceComptable.Rows(0).Item("datedebut").ToString
        ' DateMarche.Properties.MaxValue = ExerciceComptable.Rows(0).Item("datefin").ToString
        ' DateMarche.Text = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")

        Checkbonc.Checked = True
        Checkbonc_CheckedChanged(sender, e)
        'query = "select datedebut, datefin from T_COMP_EXERCICE where encours='1'"
        'Dim dt As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt.Rows
        'Next

        dtdoc.Columns.Clear()
        dtdoc.Columns.Add("CodePartition", Type.GetType("System.String"))
        dtdoc.Columns.Add("Activité", Type.GetType("System.String"))
        dtdoc.Columns.Add("Libellé de l'activité", Type.GetType("System.String"))
        dtdoc.Columns.Add("Compte comptable", Type.GetType("System.String"))
        dtdoc.Columns.Add("Montant de l'activité", Type.GetType("System.String"))
        txtnbon.Select()
    End Sub

    Private Sub RemplirBailleur()

        CmbBaill.Properties.Items.Clear()
        query = "select InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' order by InitialeBailleur"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbBaill.Properties.Items.Add(MettreApost(rw("InitialeBailleur").ToString))
        Next
    End Sub

    Private Sub RemplirFRS()

        TxtFournisMarche.Properties.Items.Clear()
        query = "select CODE_CPT, NOM_CPT from T_COMP_COMPTE where Code_Projet='" & ProjetEnCours & "' order by code_cpt"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            TxtFournisMarche.Properties.Items.Add(rw("CODE_CPT").ToString & " | " & MettreApost(rw("NOM_CPT").ToString))
        Next

    End Sub

    Private Sub ChargerConvention(ByVal bail As String, Optional TypeGenePPM As String = "")

        If TypeGenePPM.ToString = "" Then
            query = "select CodeConvention from T_Convention where CodeBailleur='" & bail & "' order by CodeConvention"
        Else
            'PPM Importer
            query = "select CodeConvention from t_ppm_repartitionbailleur where RefMarche='" & TypeGenePPM & "' order by CodeConvention"
        End If

        CmbConv.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbConv.Properties.Items.Add(rw("CodeConvention").ToString)
        Next

        If Checkbonc.Checked = True Then
            If (CmbConv.Properties.Items.Count > 0) Then CmbConv.SelectedIndex = 0
        End If
    End Sub

    Private Sub RemplirCategorie(ByVal CodeConvention As String)

        'remplir les sous classe du plan comptable
        CmbCatDep.Properties.Items.Clear()
        query = "select NumCateg, LibelleCateg from t_categoriedepense where CodeConvention='" & EnleverApost(CodeConvention.ToString) & "' ORDER BY CodeCateg"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbCatDep.Properties.Items.Add(rw("NumCateg").ToString & " | " & MettreApost(rw("LibelleCateg").ToString))
        Next

    End Sub

    Private Sub LoadMarcherGenere()

        query = "select NumeroMarche from t_marchesigne where NumMarcheDMP =''"
        CmbLotMarche.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbLotMarche.Properties.Items.Add(rw("NumeroMarche").ToString)
        Next

    End Sub
    Private Sub typemarche()

        query = "select TypeMarche from t_typemarche"
        txttypemarche.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            txttypemarche.Properties.Items.Add(MettreApost(rw("TypeMarche").ToString))
        Next

    End Sub

    Private Sub ChargerActivites(Optional PeriodeMarche As String = "")

        '  query = "select libellecourt, libellepartition from t_partition where { fn LENGTH(LibelleCourt) } >= 5 AND (DateDebutPartition>='" & dateconvert(DateDebutExercice) & "' AND DateFinPartition<='" & dateconvert(DateFinExercice) & "')"
        If PeriodeMarche = "" Then
            query = "select codepartition, libellecourt, libellepartition, DateDebutPartition, DateFinPartition from t_partition where CodeClassePartition=5 ORDER BY LibelleCourt ASC" ' AND DateDebutPartition>='" & dateconvert(DateDebutExercice) & "' AND DateFinPartition<='" & dateconvert(DateFinExercice) & "' ORDER BY LibelleCourt ASC"
        Else
            Dim Periode As String() = PeriodeMarche.ToString.Split("-")
            'query = "select codepartition, libellecourt, libellepartition, DateDebutPartition, DateFinPartition from t_partition where CodeClassePartition=5 AND DateDebutPartition>='" & dateconvert(Periode(0).Trim) & "' AND DateFinPartition<='" & dateconvert(Periode(1).Trim) & "'"
            query = "select codepartition, libellecourt, libellepartition, DateDebutPartition, DateFinPartition, LibelleCourt from t_partition where CodeClassePartition=5 AND DateDebutPartition>='" & dateconvert(Periode(0).Trim) & "' and DateDebutPartition<='" & dateconvert(Periode(1).Trim) & "' ORDER BY LibelleCourt ASC" ' AND DateFinPartition<='" & dateconvert(Periode(1).Trim) & "'"
        End If

        Dim dt As DataTable = ExcecuteSelectQuery(query)
        Combact.Properties.Items.Clear()
        Combact.Text = ""
        ReDim TablPartition(dt.Rows.Count)
        Dim i As Integer = 0
        Dim AnnePartition As String = ""
        For Each rw As DataRow In dt.Rows
            TablPartition(i) = rw("codepartition")
            i += 1
            '  AnnePartition = CDate(rw("DateDebutPartition")).Year & "-" & CDate(rw("DateFinPartition")).Year
            AnnePartition = CDate(rw("DateFinPartition")).Year
            Combact.Properties.Items.Add(rw("libellecourt").ToString & " | " & MettreApost(rw("libellepartition").ToString) & " | " & AnnePartition)
        Next

    End Sub

    Private Sub Combact_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Combact.SelectedIndexChanged
        If Combact.SelectedIndex > -1 Then
            ' Dim CodeAct As String = Combact.Text.Split(" | ")(0)
            'query = "SELECT codepartition FROM t_partition where LibelleCourt='" & CodeAct & "' AND (DateDebutPartition>='" & dateconvert(DateDebutExercice) & "' AND DateFinPartition<='" & dateconvert(DateFinExercice) & "')"
            ' Dim codePartition As String = ExecuteScallar(query)
            loadCpteCollectif(TablPartition(Combact.SelectedIndex))
        Else
            loadCpteCollectif(-1)
        End If
    End Sub
    Private Sub loadCpteCollectif(CodePartition As String)
        query = "SELECT CODE_SC, LIBELLE_SC FROM t_comp_sous_classe WHERE CODE_SC IN (SELECT NumeroComptable FROM t_besoinpartition WHERE CodePartition='" & CodePartition & "')"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        txtcompte.Properties.Items.Clear()
        txtcompte.ResetText()
        For Each rw As DataRow In dt.Rows
            txtcompte.Properties.Items.Add(rw("CODE_SC") & " | " & MettreApost(rw("LIBELLE_SC")))
        Next
    End Sub

    Private Sub NewInitialiser()
        RefMarche = ""
        TypeMarches = ""
        TxtLotMarche.Text = ""
        TxtFournisMarche.Text = ""
        CmbBaill.Text = ""
        CmbConv.Text = ""
        CmbCatDep.Text = ""
        txtmontant.Text = ""
        txttypemarche.Text = ""
        txtmethode.Text = ""
        DateMarche.Text = ""
        txtcompte.Text = ""
        TxtPieceJointe.Text = ""
        cmbRevue.Text = ""
    End Sub

    Private Sub CmbLotMarche_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbLotMarche.SelectedIndexChanged
        'initialisation des variables
        ' EffacerTexBox4(PanelControl6)
        NewInitialiser()

        If CmbLotMarche.SelectedIndex <> -1 Then
            Try
                '  Dim rwMerche As DataTable = ExcecuteSelectQuery("select TypeMarche, MontantHT, DateMarche, NumeroDAO from t_marchesigne where NumeroMarche='" & EnleverApost(CmbLotMarche.Text) & "' and EtatMarche IS NULL and CodeProjet='" & ProjetEnCours & "'")
                Dim rwMerche As DataTable = ExcecuteSelectQuery("select TypeMarche, MontantHT, DateMarche, NumeroDAO from t_marchesigne where NumeroMarche='" & EnleverApost(CmbLotMarche.Text) & "' and CodeProjet='" & ProjetEnCours & "'")

                If rwMerche.Rows(0)("TypeMarche").ToString = "Consultants" Then
                    query = "select m.DescriptionMarche, m.NumeroComptable, m.MontantEstimatif, m.PeriodeMarche, m.RevuePrioPost, m.CodeProcAO, m.TypeMarche, m.Convention_ChefFile, m.RefMarche from t_marche as m, t_dp as d where d.RefMarche=m.RefMarche and d.NumeroDp='" & rwMerche.Rows(0)("NumeroDAO") & "'"
                Else
                    query = "select m.DescriptionMarche, m.NumeroComptable, m.MontantEstimatif, m.PeriodeMarche, m.RevuePrioPost, m.CodeProcAO, m.TypeMarche, m.Convention_ChefFile, m.RefMarche from t_marche as m, t_dao as d where d.RefMarche=m.RefMarche and d.NumeroDAO='" & rwMerche.Rows(0)("NumeroDAO") & "'"
                End If

                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows

                    TxtLotMarche.Text = MettreApost(rw("DescriptionMarche").ToString())
                    '  txtmontant.Text = AfficherMonnaie(rw("MontantEstimatif").ToString())
                    txtmontant.Text = AfficherMonnaie(rwMerche.Rows(0)("MontantHT").ToString)
                    Dim Method As DataRow = ExcecuteSelectQuery("select AbregeAO, LibelleAO from T_ProcAO where CodeProcAO ='" & rw("CodeProcAO") & "'").Rows(0)
                    txtmethode.Text = Method("AbregeAO").ToString & " | " & MettreApost(Method("LibelleAO").ToString)
                    txttypemarche.Text = rw("TypeMarche").ToString()
                    TypeMarches = rw("TypeMarche").ToString()

                    DateMarche.Text = CDate(rwMerche.Rows(0)("DateMarche")).ToShortDateString
                    cmbRevue.Text = rw("RevuePrioPost").ToString()
                    RefMarche = rw("RefMarche").ToString

                    If ModePPM = "Genere" Then
                        CmbBaill.Text = GetInitialbailleur(rw("Convention_ChefFile").ToString) ' ExecuteScallar("select InitialeBailleur from t_bailleur b, t_convention c where c.CodeBailleur=b.CodeBailleur and c.CodeConvention='" & rw("Convention_ChefFile").ToString() & "'")
                        CmbConv.Text = rw("Convention_ChefFile").ToString()
                        'Remplir la categorie de depense du bailleur chef fil sur la convention
                    Else
                        ChargerConvention(rw("Convention_ChefFile").ToString, RefMarche)
                    End If

                    RemplirCategorie(rw("Convention_ChefFile").ToString())

                    ''remplir les sous classe du plan comptable
                    'Modif_engagement.txtcompte.Properties.Items.Clear()
                    'query = "select CODE_SC, LIBELLE_SC from T_COMP_SOUS_CLASSE where code_sc='" & rw("NumeroComptable").ToString & "' ORDER BY code_sc"
                    'Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    'For Each rw1 As DataRow In dt1.Rows
                    '    Modif_engagement.txtcompte.Text = rw1("CODE_SC").ToString() & " | " & MettreApost(rw1("LIBELLE_SC").ToString())
                    'Next

                    'Dim codefrs As String = ""
                    'query = "select f.NomFournis from t_marchesigne ms, T_Fournisseur f where ms.CodeFournis=f.CodeFournis and ms.refmarche='" & rw("refmarche").ToString & "'"
                    'codefrs = ExecuteScallar(query)

                    'query = "select code_cpt, nom_cpt from t_comp_compte where nom_cpt='" & codefrs.ToString & "' and Code_Projet='" & ProjetEnCours & "'"
                    'Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                    'For Each rw2 As DataRow In dt2.Rows
                    '    TxtFournisMarche.Text = rw2("code_cpt").ToString() & " | " & MettreApost(rw2("nom_cpt").ToString())
                    'Next

                    'dtdoc.Columns.Clear()
                    'dtdoc.Columns.Add("Activité", Type.GetType("System.String"))
                    'dtdoc.Columns.Add("Libellé de l'activité", Type.GetType("System.String"))
                    'dtdoc.Columns.Add("Montant de l'activité", Type.GetType("System.String"))

                    If ModePPM = "Genere" Then

                        '  query = "select distinct p.codepartition, p.libellecourt, p.libellepartition from t_marche m, t_repartitionparbailleur r, t_besoinpartition b, t_partition p where m.refmarche=r.refmarche and r.RefBesoinPartition = b.RefBesoinPartition and b.CodePartition = p.CodePartition and m.refmarche='" & rw("refmarche").ToString & "'"
                        ' query = "select distinct p.codepartition, p.libellecourt, p.libellepartition, p.DateFinPartition from t_besoinmarche bm, t_besoinpartition b, t_partition p where bm.RefBesoinPartition=b.RefBesoinPartition and b.CodePartition=p.CodePartition and bm.RefMarche='" & rw("RefMarche") & "'"

                        query = "select p.codepartition, p.libellecourt, p.libellepartition, p.DateFinPartition, b.NumeroComptable, b.RefBesoinPartition from t_besoinmarche bm, t_besoinpartition b, t_partition p where bm.RefBesoinPartition=b.RefBesoinPartition and b.CodePartition=p.CodePartition and bm.RefMarche='" & rw("RefMarche") & "'"
                        Dim dt3 As DataTable = ExcecuteSelectQuery(query)

                        dtdoc.Rows.Clear()
                        For Each rw3 As DataRow In dt3.Rows
                            Dim drs = dtdoc.NewRow()
                            drs("CodePartition") = rw3("codepartition").ToString()
                            drs("Activité") = rw3("libellecourt").ToString()
                            drs("Libellé de l'activité") = MettreApost(rw3("libellepartition").ToString()) & " | " & CDate(rw3("DateFinPartition").ToString).Year
                            drs("Compte comptable") = MettreApost(rw3("NumeroComptable").ToString())

                            drs("Montant de l'activité") = AfficherMonnaie(MontantActivites(rw("RefMarche"), rwMerche.Rows(0)("TypeMarche").ToString, rw3("codepartition"), rw3("RefBesoinPartition")))
                            ' drs("CodeConvention") = "" 'En cas de marche generer

                            'mettre le montant(a revoir)
                            'drs(2) = ""
                            dtdoc.Rows.Add(drs)
                        Next

                        LgListAct.DataSource = dtdoc
                        Viewact.OptionsView.ColumnAutoWidth = True
                        Viewact.OptionsBehavior.AutoExpandAllGroups = True
                        Viewact.VertScrollVisibility = True
                        Viewact.HorzScrollVisibility = True
                        Viewact.BestFitColumns()
                        GetNewEnebaled(False)

                        Viewact.Columns("CodePartition").Visible = False
                        'Viewact.Columns("Activité").Visible = False
                        Viewact.Columns("Activité").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        Viewact.Columns("Compte comptable").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        Viewact.Columns("Montant de l'activité").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                        'ElseIf ModePPM = "PPSD" Then
                    Else
                        ChargerActivites(rw("PeriodeMarche").ToString)
                        GetNewEnebaled(True)
                        ' CmbBaill.Enabled = True
                        CmbConv.Enabled = True
                        TxtMontanteng.Enabled = False
                    End If
                Next
            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        End If
    End Sub

    Private Function MontantActivites(ByVal RefMarche As String, ByVal TypMarche As String, ByVal CodePartitions As String, ByVal RefBesoinPartition As String)
        Dim MontantActivit As Decimal = 0
        Try
            query = "SELECT SUM(R.MontantBailleur) from t_repartitionparbailleur as R, t_besoinmarche as B, t_besoinpartition as P WHERE B.RefBesoinPartition=P.RefBesoinPartition AND P.RefBesoinPartition=R.RefBesoinPartition AND B.RefMarche='" & RefMarche & "' and P.TypeBesoin='" & TypMarche & "' and P.CodePartition='" & CodePartitions & "' and P.RefBesoinPartition='" & RefBesoinPartition & "' and P.CodeProjet='" & ProjetEnCours & "'"
            MontantActivit = ExecuteScallar(query)
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        Return MontantActivit
    End Function

    Private Sub BtEnr_Click(sender As System.Object, e As System.EventArgs) Handles BtEnr.Click

        If Checkmarche.Checked = False And Checkbonc.Checked = False Then
            FailMsg("Veuillez cocher une option.")
            Exit Sub
        End If

        Try
            Dim somact As Double = 0
            Dim codepart As String = ""
            Dim MontantActivit As Decimal = 0

            If Checkmarche.Checked Then 'Cas de marche genere

                If NumDMP.Text.Trim = "" Then
                    SuccesMsg("Renseigner le Numéro du marché à la DMP")
                    NumDMP.Select()
                    Exit Sub
                End If

                If CmbLotMarche.SelectedIndex = -1 Then
                    SuccesMsg("Choississez un numéro de marché")
                    CmbLotMarche.Select()
                    Exit Sub
                End If

                If TxtFournisMarche.SelectedIndex = -1 Then
                    SuccesMsg("Choississez le fournisseur")
                    TxtFournisMarche.Select()
                    Exit Sub
                End If

                If TxtPieceJointe.Text = "" Then
                    SuccesMsg("Veuillez joindre le marche")
                    TxtPieceJointe.Select()
                    Exit Sub
                End If

                If Viewact.RowCount = 0 Then
                    SuccesMsg("Entrer les activités et comptes comptables")
                    Exit Sub
                End If

                If ModePPM <> "Genere" Then
                    For i = 0 To Viewact.RowCount - 1
                        somact = somact + CDec(Viewact.GetRowCellValue(i, "Montant de l'activité").ToString.Replace(" ", ""))
                    Next

                    If CDbl(txtmontant.Text.Replace(Chr(160), "")) <> (CDbl(somact.ToString)) Then
                        FailMsg("La somme des montants des activités doit être égale au montant du marché")
                        txtmontant.Focus()
                        Exit Sub
                    End If
                End If

                If TxtPieceJointe.Text.Length <> 0 Then
                    Dim NomFichier As String = line & "\Marches\"
                    Dim NewPieceName As String = FormatFileName(NumDMP.Text, "_") & "." & TxtPieceJointe.Text.Split(".")(1)
                    NomFichier = NomFichier & "\" & NewPieceName
                    File.Copy(TxtChemin.Text, NomFichier, True)
                End If

                Dim frs() As String = TxtFournisMarche.Text.Split(" | ")

                If ModePPM = "Genere" Then
                    ExecuteNonQuery("update t_marchesigne set NumMarcheDMP='" & EnleverApost(NumDMP.Text) & "', RefMarche='" & RefMarche & "', CodeCateg='" & codecat.ToString & "', Attributaire='" & frs(0).ToString & "' where NumeroMarche='" & EnleverApost(CmbLotMarche.Text) & "'")
                    NewSaveEngagement(False)
                Else
                    ExecuteNonQuery("update t_marchesigne set NumMarcheDMP='" & EnleverApost(NumDMP.Text) & "', RefMarche='" & RefMarche & "', CodeCateg='" & codecat.ToString & "', Attributaire='" & frs(0).ToString & "' where NumeroMarche='" & EnleverApost(CmbLotMarche.Text) & "'")
                    'Enregistrement des engagements
                    NewSaveEngagement(True)
                End If

                LoadMarcherGenere()

            ElseIf Checkbonc.Checked Then 'Cas de bon de commande

                If txtnbon.Text = "" Then
                    FailMsg("Renseigner le numéro du bon de commande")
                    txtnbon.Focus()
                    Exit Sub
                End If

                If TxtLotMarche.Text = "" Then
                    FailMsg("Renseigner le libellé")
                    TxtLotMarche.Focus()
                    Exit Sub
                End If

                If TxtFournisMarche.SelectedIndex = -1 Then
                    FailMsg("Choississez le fournisseur")
                    TxtFournisMarche.Select()
                    Exit Sub
                End If

                If CmbBaill.SelectedIndex = -1 Then
                    FailMsg("Choississez le bailleur")
                    CmbBaill.Select()
                    Exit Sub
                End If

                If CmbConv.SelectedIndex = -1 Then
                    FailMsg("Choississez la convention")
                    CmbConv.Select()
                    Exit Sub
                End If

                If CmbCatDep.SelectedIndex = -1 Then
                    FailMsg("Choississez la Catégorie de dépense")
                    CmbCatDep.Select()
                    Exit Sub
                End If

                If txtmontant.Text.Length = 0 Then
                    FailMsg("Entrer le montant du marché")
                    txtmontant.Focus()
                    Exit Sub
                ElseIf Val(txtmontant.Text) = 0 Then
                    FailMsg("Le montant du marché doit être supérieur à 0")
                    txtmontant.Focus()
                    Exit Sub
                End If

                If txttypemarche.SelectedIndex = -1 Then
                    FailMsg("Choississez le type de marché")
                    txttypemarche.Select()
                    Exit Sub
                End If

                If txtmethode.SelectedIndex = -1 Then
                    FailMsg("Choississez la méthode")
                    txtmethode.Select()
                    Exit Sub
                End If

                If DateMarche.Text = "" Then
                    FailMsg("Renseigner la date du marché")
                    DateMarche.Select()
                    Exit Sub
                End If

                If cmbRevue.SelectedIndex = -1 Then
                    FailMsg("Choississez la revue")
                    cmbRevue.Select()
                    Exit Sub
                End If

                If Viewact.RowCount = 0 Then
                    FailMsg("Entrer les activités et comptes comptables imputables pour le bon de commande")
                    Combact.Select()
                    Exit Sub
                End If

                If TxtPieceJointe.Text.Trim = "" Then
                    FailMsg("Veuillez joindre le bon de commande")
                    TxtPieceJointe.Focus()
                    Exit Sub
                End If

                For i = 0 To Viewact.RowCount - 1
                    somact = somact + CDec(Viewact.GetRowCellValue(i, "Montant de l'activité").ToString.Replace(" ", ""))
                Next

                If CDbl(txtmontant.Text.Replace(Chr(160), "")) <> (CDbl(somact.ToString)) Then
                    FailMsg("La somme des montants des activités doit être égale au montant du marché")
                    txtmontant.Focus()
                    Exit Sub
                End If

                If Val(ExecuteScallar("select count(numeromarche) from t_marche where numeromarche='" & EnleverApost(txtnbon.Text) & "'")) > 0 Then
                    SuccesMsg("Le bon de commande existe déjà.")
                    Exit Sub
                End If

                If TxtPieceJointe.Text <> "" Then
                    Dim NomFichier As String = line & "\Marches\"
                    If Not Directory.Exists(NomFichier) Then
                        Directory.CreateDirectory(NomFichier)
                    End If
                    Dim NewPieceName As String = FormatFileName(txtnbon.Text, "_") & "." & TxtPieceJointe.Text.Split(".")(1)
                    NomFichier = NomFichier & "\" & NewPieceName
                    File.Copy(TxtChemin.Text, NomFichier, True)
                End If

                Dim frs() As String
                frs = TxtFournisMarche.Text.Split(" | ")

                'Dim sc() As String
                'sc = txtcompte.Text.Split(" | ")

                Dim cat() As String
                cat = CmbCatDep.Text.Split(" | ")

                Dim meth() As String
                meth = txtmethode.Text.Split(" | ")

                Dim periode As String
                periode = "01/01/" & Year(DateMarche.Text) & " - 31/12/" & Year(DateMarche.Text)

                ' CodeProcAO meth(0).ToString 
                ExecuteNonQuery("insert into t_marche values (NULL,'" & EnleverApost(txtnbon.Text) & "', '" & ProjetEnCours & "','','" & EnleverApost(txttypemarche.Text) & "','" & EnleverApost(TxtLotMarche.Text) & "','','','','" & CDec(txtmontant.Text.Replace(" ", "")) & "','" & CodeProcAO & "','','" & EnleverApost(cmbRevue.Text) & "','" & periode.ToString & "','" & CmbBaill.Text & "','" & CmbConv.Text & "', '" & CmbConv.Text & "','','" & CodeProcAO & "','', '0', '" & Now.ToShortDateString & " " & Now.ToLongTimeString & "','-1')")

                RefMarche = Val(ExecuteScallar("select max(refmarche) from t_marche"))

                'a revoir
                ExecuteNonQuery("insert into t_marchesigne values('" & EnleverApost(txtnbon.Text) & "','" & EnleverApost(txtnbon.Text) & "','" & DateMarche.Text & "','" & RefMarche.ToString & "', '', '" & EnleverApost(txttypemarche.Text) & "','0','0','0','0','" & CDec(txtmontant.Text.Replace(" ", "")) & "','','','','','" & codecat.ToString & "','Terminé','" & ProjetEnCours & "','" & frs(0).ToString & "','BONCMDE')")

                'Enregistrement des engagements
                NewSaveEngagement(True)
            End If

            SuccesMsg("Enregistrement effectué avec succès.")
            'effacer les activités enregistrées
            dtdoc.Rows.Clear()
            liste_engagement.LoadData()
            'effacer les champs remplis
            EffacerTexBox4(PanelControl6)
            TxtPieceJointe.ResetText()
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub NewSaveEngagement(ByVal TypeSave As Boolean)
        Try
            Dim MontantActivit As Decimal = 0
            Dim codepart As String = ""
            Dim refbesoin As String = ""

            For i = 0 To Viewact.RowCount - 1

                MontantActivit = Viewact.GetDataRow(i)("Montant de l'activité").ToString.Replace(" ", "")
                codepart = Viewact.GetDataRow(i)("CodePartition")

                query = "insert into t_acteng values ('" & Viewact.GetDataRow(i)("Activité").ToString & "', '" & CDec(Viewact.GetDataRow(i)("Compte comptable").ToString()) & "','" & RefMarche & "','" & MontantActivit.ToString & "', '" & codepart & "')"
                ExecuteNonQuery(query)

                If TypeSave = True Then 'Enregitrer dans ces tables en cas de bon de commande ou PPM importé ou saisie
                    refbesoin = ExecuteScallar("select RefBesoinPartition from t_besoinpartition where codepartition='" & codepart.ToString & "' and numerocomptable='" & Viewact.GetDataRow(i)("Compte comptable").ToString & "' and CodeProjet='" & ProjetEnCours & "'")

                    If refbesoin.ToString <> "" Then
                        ExecuteNonQuery("update t_repartitionparbailleur set RefMarche='" & RefMarche.ToString & "' where RefBesoinPartition='" & IIf(refbesoin.ToString = Nothing, 0, refbesoin.ToString) & "'")

                        ExecuteNonQuery("insert into t_besoinmarche values('" & IIf(refbesoin.ToString = Nothing, 0, refbesoin.ToString) & "', '" & RefMarche.ToString & "')")
                    End If
                End If

            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub


    Private Sub Checkmarche_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Checkmarche.CheckedChanged
        If Checkmarche.Checked Then
            Checkbonc.Checked = False
            txtnbon.Visible = False

            CmbLotMarche.Visible = True
            LabelControl13.Text = "N° Marché généré"
            NumDMP.Enabled = True
            GetEnebaled()
            SimpleButton1_Click(Me, e)
            ModePPM = GetModGenerePPM
            CmbLotMarche.Select()
            Combact.Properties.Items.Clear()
            TablPartition = Nothing
        Else
            ModePPM = ""
        End If
    End Sub

    Private Sub Checkbonc_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Checkbonc.CheckedChanged
        If Checkbonc.Checked Then
            Checkmarche.Checked = False
            CmbLotMarche.Visible = False

            txtnbon.Visible = True
            NumDMP.Enabled = False
            LabelControl13.Text = "N° Marché / Bon de Cde"
            GetEnebaled()
            SimpleButton1_Click(Me, e)
            txtnbon.Focus()
            ChargerActivites()
        End If
    End Sub

    Private Sub GetEnebaled()
        TxtLotMarche.Enabled = True
        CmbBaill.Enabled = True
        CmbConv.Enabled = True
        txtmontant.Enabled = True
        txttypemarche.Enabled = True
        txtmethode.Enabled = True
        DateMarche.Enabled = True
        cmbRevue.Enabled = True
        Combact.Enabled = True
        txtcompte.Enabled = True
        TxtMontanteng.Enabled = True
        BtAjout.Enabled = True
    End Sub

    Private Sub GetNewEnebaled(ByVal value As Boolean)
        TxtLotMarche.Enabled = False
        CmbBaill.Enabled = False
        CmbConv.Enabled = False
        txtmontant.Enabled = False
        txttypemarche.Enabled = False
        txtmethode.Enabled = False
        DateMarche.Enabled = False
        cmbRevue.Enabled = False

        Combact.Enabled = value
        txtcompte.Enabled = value
        TxtMontanteng.Enabled = value
        BtAjout.Enabled = value
    End Sub


    Private Sub BtAjout_Click(sender As System.Object, e As System.EventArgs) Handles BtAjout.Click
        If txtmontant.Text.Length = 0 Then
            FailMsg("Entrer le montant du marché")
            txtmontant.Focus()
            Exit Sub
        ElseIf Val(txtmontant.Text) = 0 Then
            FailMsg("Le montant du marché doit être supérieur à 0")
            txtmontant.Focus()
            Exit Sub
        End If

        If Combact.SelectedIndex = -1 Then
            FailMsg("Choississez une activité")
            Combact.Select()
            Exit Sub
        End If

        If txtcompte.SelectedIndex = -1 Then
            FailMsg("Choississez le Numéro comptable")
            txtcompte.Select()
            Exit Sub
        End If

        If Checkmarche.Checked = True And CmbConv.Enabled = True And CmbConv.Text.Trim = "" Then
            SuccesMsg("Veuillez selectionné la convention")
            CmbConv.Select()
            Exit Sub
        End If

        ' Dim MontantEngager = TxtMontanteng.Text.Replace(" ", "")
        ' Dim MontantEngager = TxtMontanteng.Text.Replace(" ", "")

        If TxtMontanteng.Text.Replace(" ", "").Length = 0 Then
            FailMsg("Entrer le montant pour le compte " & txtcompte.Text.Split(" | ")(0) & " sur l'activité " & Combact.Text.Split(" | ")(0))
            TxtMontanteng.Focus()
            Exit Sub
        End If

        Dim somact As Double = 0
        For i = 0 To Viewact.RowCount - 1
            somact = somact + Viewact.GetRowCellValue(i, "Montant de l'activité")
        Next

        If txtmontant.Text.Replace(" ", "").Length > 0 And TxtMontanteng.Text.Replace(" ", "").Length > 0 Then
            If CDbl(txtmontant.Text.Replace(Chr(160), "")) < (CDbl(somact.ToString) + CDbl(TxtMontanteng.Text.Replace(Chr(160), ""))) Then
                FailMsg("La somme des montants des activités ne doit pas excéder le montant du marché")
                TxtMontanteng.Focus()
                Exit Sub
            End If
        End If

        Dim activite() As String = Combact.Text.Split("|"c)
        Dim souscompte() As String = txtcompte.Text.Split(" | ")

        'Recherche du montant de l'activite
        Dim MontantTotalActivite As Decimal = Val(ExecuteScallar("select sum(QteNature*PUNature) from t_besoinpartition where CodePartition='" & TablPartition(Combact.SelectedIndex) & "' and CodeProjet='" & ProjetEnCours & "'"))
        Dim MontantAtiviteEngage As Decimal = 0

        'For i = 0 To Viewact.RowCount - 1
        '    If Viewact.GetDataRow(i)("Activité").ToString = activite(0).ToString() And Viewact.GetDataRow(i)("Compte comptable") = souscompte(0) Then
        '        SuccesMsg("Cette répartition existe déjà")
        '        Combact.Select()
        '        Exit Sub
        '    End If
        'Next

        'Montant activite engage a partir des marches
        MontantAtiviteEngage = Val(ExecuteScallar("SELECT SUM(Montant_libellecourt) from t_acteng where codepartition='" & TablPartition(Combact.SelectedIndex) & "'"))
        MontantAtiviteEngage += Val(ExecuteScallar("SELECT SUM(Montant_act) from t_comp_activite where codepartition='" & TablPartition(Combact.SelectedIndex) & "' and CODE_PROJET='" & ProjetEnCours & "'"))

        For i = 0 To Viewact.RowCount - 1
            If Viewact.GetDataRow(i)("CodePartition").ToString = TablPartition(Combact.SelectedIndex) Then
                MontantAtiviteEngage += CDec(Viewact.GetDataRow(i)("Montant de l'activité"))
            End If
        Next

        'Verifier si le montant de l'activité est deja utiliser
        If MontantTotalActivite < MontantAtiviteEngage + Val(TxtMontanteng.Text.Replace(" ", "")) Then
            ' SuccesMsg("Cette répartition existe déjà")
            SuccesMsg("Le montant de l'activité est déjà consommé")
            Combact.Select()
            Exit Sub
        End If

        ' Array.Clear(activite, 0, 2)
        ' drs(2) = Trim(MettreApost(Strings.Join(souscompte, " ").ToString))

        Dim drs = dtdoc.NewRow()
        drs("CodePartition") = TablPartition(Combact.SelectedIndex)
        drs("Activité") = activite(0).ToString.Trim()
        drs("Libellé de l'activité") = activite(1).ToString.Trim()
        drs("Compte comptable") = souscompte(0)
        drs("Montant de l'activité") = AfficherMonnaie(TxtMontanteng.Text)

        dtdoc.Rows.Add(drs)

        LgListAct.DataSource = dtdoc
        Viewact.OptionsView.ColumnAutoWidth = True
        Viewact.OptionsBehavior.AutoExpandAllGroups = True
        Viewact.VertScrollVisibility = True
        Viewact.HorzScrollVisibility = True
        Viewact.BestFitColumns()

        Viewact.Columns("CodePartition").Visible = False
        ' Viewact.Columns("Activité").Visible = False
        Viewact.Columns("Activité").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Viewact.Columns("Compte comptable").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Viewact.Columns("Montant de l'activité").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        'On vide la ligne de l'imputation
        Combact.SelectedIndex = -1
        TxtMontanteng.ResetText()
        Combact.Select()

        If Checkmarche.Checked = True Then
            If CmbConv.Enabled = True Then CmbConv.Text = ""
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dialog_form(Creer_compte_tier)
        ' Dim OldText As String = TxtFournisMarche.Text
        RemplirFRS()
    End Sub

    Private Sub CmbBaill_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbBaill.SelectedIndexChanged

        query = "Select CodeBailleur, InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' and InitialeBailleur='" & EnleverApost(CmbBaill.Text) & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CodeBailleurCache.Text = rw("CodeBailleur")
        Next

        If Checkbonc.Checked = True Then
            ChargerConvention(CodeBailleurCache.Text)
        End If
    End Sub

    Private Sub LgListAct_Click(sender As System.Object, e As System.EventArgs) Handles LgListAct.Click
        If (Viewact.RowCount > 0) Then
            drx = Viewact.GetDataRow(Viewact.FocusedRowHandle)
            Dim IDL = drx(0).ToString
            ColorRowGrid(Viewact, "[Libellé de l'activité]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(Viewact, "[Activité]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
        End If
    End Sub

    Private Sub SupprimerLactivitéToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SupprimerLactivitéToolStripMenuItem.Click
        Try
            If Viewact.RowCount > 0 Then
                If ConfirmMsg("Voulez-vous supprimer cette imputation ?") = DialogResult.Yes Then
                    Viewact.GetDataRow(Viewact.FocusedRowHandle).Delete()
                End If
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub CmbConv_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbConv.SelectedIndexChanged
        If Checkbonc.Checked = True Then
            RemplirCategorie(CmbConv.Text)
        Else
            If ModePPM <> "Genere" Then
                CmbBaill.Text = GetInitialbailleur(EnleverApost(CmbConv.Text.Trim))
                TxtMontanteng.Text = AfficherMonnaie(ExecuteScallar("SELECT Montant from t_ppm_repartitionbailleur where CodeConvention='" & EnleverApost(CmbConv.Text.Trim) & "' and RefMarche='" & RefMarche & "'"))
            End If
        End If
    End Sub

    Private Sub Combact_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles Combact.KeyPress, txtcompte.KeyPress, TxtMontanteng.KeyPress
        Select Case e.KeyChar
            Case ControlChars.CrLf
                BtAjout_Click(Me, e)
            Case Else
        End Select
    End Sub

    Private Sub txtmontant_TextChanged(sender As Object, e As System.EventArgs) Handles txtmontant.TextChanged
        txtmontant.Text = AfficherMonnaie(txtmontant.Text)
    End Sub

    'Private Sub TxtPieceJointe_Click(sender As Object, e As System.EventArgs) Handles TxtPieceJointed.Click
    '    Dim dlg As New OpenFileDialog
    '    dlg.Filter = "Documents|*.pdf|Images|*.jpg;*.jpeg;*.png;*.gif;*.bmp"
    '    Dim rep = dlg.ShowDialog
    '    If rep = DialogResult.OK Then
    '        If Checkbonc.Checked Then
    '            TxtChemin.Text = dlg.FileName
    '            Dim ext = New System.IO.FileInfo(dlg.FileName).Extension
    '            TxtPieceJointed.Text = txtnbon.Text.Replace("/", "-") & "" & ext.ToString
    '        Else
    '            TxtChemin.Text = dlg.FileName
    '            Dim ext = New System.IO.FileInfo(dlg.FileName).Extension
    '            TxtPieceJointed.Text = txtndmp.Text.Replace("/", "-") & "" & ext.ToString
    '        End If
    '    End If
    'End Sub

    Private Sub txttypemarche_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles txttypemarche.SelectedIndexChanged
        RemplirComboBaileur()
    End Sub

    Private Sub RemplirComboBaileur()
        Try
            query = "select AbregeAO, LibelleAO from T_ProcAO where TypeMarcheAO ='" & EnleverApost(txttypemarche.Text) & "'"
            ' query = "select p.AbregeAO, p.LibelleAO from T_ProcAO as p, t_seuil as s where p.CodeProcAO=s.CodeProcAO and s.Bailleur ='" & EnleverApost(txttypemarche.Text) & "'"
            txtmethode.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            txtmethode.Properties.Items.Add(rw("AbregeAO").ToString & " | " & MettreApost(rw("LibelleAO").ToString))
        Next

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click

        'effacer les champs remplis
        EffacerTexBox4(PanelControl6)
        TxtPieceJointe.ResetText()

        'effacer les activités enregistrées
        dtdoc.Rows.Clear()

    End Sub

    Private Sub CmbCatDep_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbCatDep.SelectedIndexChanged
        Dim cat() As String
        cat = CmbCatDep.Text.Split(" | ")
        query = "select CodeCateg from t_categoriedepense where NumCateg='" & cat(0).ToString & "' and CodeConvention='" & EnleverApost(CmbConv.Text) & "'"
        codecat = ExecuteScallar(query)
    End Sub


    Private Sub PnlPieceJustif_Click(sender As Object, e As EventArgs) Handles PnlPieceJustif.Click
        Dim OpenFile As New OpenFileDialog()
        Try
            OpenFile.Filter = "Document PDF|*.pdf"
            'OpenFile.Filter = "Tout|*.pdf;*.jpg;*.jpeg;*.png;*.gif;*.bmp|Documents|*.pdf|Images|*.jpg;*.jpeg;*.png;*.gif;*.bmp"
            OpenFile.FileName = ""
            If OpenFile.ShowDialog() = DialogResult.OK Then
                'If Checkbonc.Checked Then
                'TxtChemin.Text = OpenFile.FileName
                'TxtPieceJointe.Text = OpenFile.SafeFileName
                '  Else
                TxtChemin.Text = OpenFile.FileName
                TxtPieceJointe.Text = OpenFile.SafeFileName
                ' End If
            End If
        Catch ex As Exception
            'FailMsg(ex.ToString())
        End Try
    End Sub

    Private Sub txtnbon_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtnbon.KeyPress
        If e.KeyChar = "'"c Or e.KeyChar = "\"c Then
            e.Handled = True
        End If
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If Viewact.RowCount = 0 Then e.Cancel = True
    End Sub

    Private Sub txtmethode_SelectedValueChanged(sender As Object, e As EventArgs) Handles txtmethode.SelectedValueChanged
        CodeProcAO = ""
        If txtmethode.SelectedIndex <> -1 Then
            Dim Code() As String = txtmethode.Text.Split(" | ")
            CodeProcAO = ExecuteScallar("SELECT CodeProcAO from t_procao where AbregeAO='" & Code(0).ToString & "' and TypeMarcheAO='" & EnleverApost(txttypemarche.Text) & "' and CodeProjet='" & ProjetEnCours & "'")
        End If
    End Sub
End Class