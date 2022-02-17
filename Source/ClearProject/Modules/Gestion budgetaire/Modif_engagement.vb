Imports System.IO
Imports ClearProject.PassationMarche
Imports ClearProject.GestBudgetaire


Public Class Modif_engagement
    Dim DateDebutPartition As Date = CDate(ExerciceComptable.Rows(0)("datedebut"))
    Dim DateFinPartition As Date = CDate(ExerciceComptable.Rows(0)("datefin"))
    Dim drx As DataRow
    Dim dtdoc = New DataTable()
    Dim codecat As String = ""
    Dim OldActivites(,) As String
    Dim CpteOldActivites As Decimal = 0

    Public NumMarche As String = ""
    Public PeriodsMarches As String = ""
    Public RefsMarches As String = ""

    Dim TablPartition As String()
    Dim ModePPM As String = ""
    Dim CodeProcAO As String = ""

    Private Sub Modif_engagement_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        DateDebutPartition = CDate(ExerciceComptable.Rows(0)("datedebut"))
        DateFinPartition = CDate(ExerciceComptable.Rows(0)("datefin"))
        LoadActivite(PeriodsMarches)
        LoadTypeMarche()
        RemplirFRS()
        RemplirBailleur()

        ModePPM = GetModGenerePPM() 'Mode generation du PPM

        'date
        ' DateMarche.Text = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
        'DateMarche.Properties.MinValue = ExerciceComptable.Rows(0).Item("datedebut").ToString
        'DateMarche.Properties.MaxValue = ExerciceComptable.Rows(0).Item("datefin").ToString
        'query = "select datedebut, datefin from T_COMP_EXERCICE where encours='1'"
        'Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        'For Each rw1 As DataRow In dt1.Rows
        'Next

        'query = "select * from t_marche where NumeroMarche='" & EnleverApost(NumMarche) & "'"

        query = "select * from t_marche where RefMarche='" & RefsMarches & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)

        For Each rw As DataRow In dt.Rows

            'On remplit les champs
            txtnbon.Text = NumMarche
            TxtLotMarche.Text = MettreApost(rw("DescriptionMarche").ToString)
            CodeProcAO = rw("CodeProcAO").ToString
            cmbRevue.Text = MettreApost(rw("RevuePrioPost"))
            txttypemarche.Text = rw("TypeMarche").ToString
            CmbBaill.Text = GetInitialbailleur(rw("Convention_ChefFile").ToString)
            CmbConv.Text = rw("Convention_ChefFile").ToString
            TxtPieceJointe.Text = NumMarche & ".pdf"

            query = "select AbregeAO, LibelleAO from T_ProcAO where CodeProcAO ='" & rw("CodeProcAO").ToString & "'"
            Dim dt5 As DataTable = ExcecuteSelectQuery(query)
            For Each rw5 As DataRow In dt5.Rows
                txtmethode.Text = rw5("AbregeAO").ToString & " | " & MettreApost(rw5("LibelleAO").ToString)
            Next

            Dim rWmarcheSigne As DataRow = ExcecuteSelectQuery("select DateMarche,CodeCateg, MontantHT, NumMarcheDMP, CodeCateg, Attributaire from t_marchesigne where NumeroMarche='" & EnleverApost(NumMarche) & "'").Rows(0)
            DateMarche.Text = CDate(rWmarcheSigne("DateMarche")).ToShortDateString
            txtmontant.Text = AfficherMonnaie(rWmarcheSigne("MontantHT").ToString)
            codecat = rWmarcheSigne("CodeCateg").ToString

            'bon de commande
            If PeriodsMarches = "" Then
                NewEnabled(True)
                NumDMP.Text = ""
            ElseIf ModePPM = "Genere" Then
                NewEnabled(False)
                CmbConv.Enabled = False
                Combact.Enabled = False
                txtcompte.Enabled = False
                ContextMenuStrip1.Items(0).Visible = False
                NumDMP.Text = MettreApost(rWmarcheSigne("NumMarcheDMP").ToString)
            Else
                NewEnabled(False)
                CmbConv.Enabled = True
                NumDMP.Text = MettreApost(rWmarcheSigne("NumMarcheDMP").ToString)
            End If

            ''remplir les sous classe du plan comptable
            'txtcompte.Properties.Items.Clear()
            'query = "select * from T_COMP_SOUS_CLASSE where code_sc='" & rw(3).ToString & "' ORDER BY code_sc"
            'Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            'For Each rw1 As DataRow In dt1.Rows
            '    txtcompte.Text = rw1(0).ToString & " | " & MettreApost(rw1(2).ToString)
            'Next

            ' query = "select c.NumCateg, c.LibelleCateg from t_marchesigne m, t_CategorieDepense c where m.CodeCateg=c.CodeCateg and m.NumeroMarche ='" & EnleverApost(NumMarche) & "'"

            query = "select NumCateg, LibelleCateg from t_CategorieDepense  where CodeCateg='" & rWmarcheSigne("CodeCateg").ToString & "'" 'c.CodeCateg and m.NumeroMarche ='" & EnleverApost(NumMarche) & "'"
            Dim dt2 As DataTable = ExcecuteSelectQuery(query)
            For Each rw2 As DataRow In dt2.Rows
                CmbCatDep.Text = rw2("NumCateg").ToString & " | " & MettreApost(rw2("LibelleCateg").ToString)
            Next

            ' Dim codefrs As String = ""
            ' query = "select Attributaire from t_marchesigne where NumeroMarche ='" & NumMarche & "'"
            ' codefrs = ExecuteScallar(query)

            query = "select CODE_CPT, NOM_CPT from T_COMP_COMPTE where code_cpt='" & rWmarcheSigne("Attributaire").ToString & "' and Code_Projet='" & ProjetEnCours & "' order by code_cpt"
            Dim dt4 As DataTable = ExcecuteSelectQuery(query)
            For Each rw4 As DataRow In dt4.Rows
                TxtFournisMarche.Text = rw4("CODE_CPT").ToString & " | " & MettreApost(rw4("NOM_CPT").ToString)
            Next

            'On remplit les activites
            dtdoc.Columns.Clear()
            dtdoc.Columns.Add("CodePartition", Type.GetType("System.String"))
            dtdoc.Columns.Add("Activité", Type.GetType("System.String"))
            dtdoc.Columns.Add("Libellé de l'activité", Type.GetType("System.String"))
            dtdoc.Columns.Add("Compte comptable", Type.GetType("System.String"))
            dtdoc.Columns.Add("Montant de l'activité", Type.GetType("System.String"))
            dtdoc.Rows.Clear()

            ' query = "select DISTINCT a.LibelleCourt, p.libellepartition, a.Montant_libellecourt, a.NumeroComptable from t_acteng a, t_partition p where a.LibelleCourt = p.LibelleCourt and a.RefMarche ='" & rw(0).ToString & "' AND p.DateDebutPartition>='" & dateconvert(DateDebutPartition) & "' AND p.DateFinPartition<='" & dateconvert(DateFinPartition) & "'"

            query = "select a.CodePartition, a.LibelleCourt, p.libellepartition, a.Montant_libellecourt, a.NumeroComptable from t_acteng a, t_partition p where a.CodePartition = p.CodePartition and a.RefMarche ='" & rw("RefMarche").ToString & "' ORDER BY LibelleCourt ASC" 'AND p.DateDebutPartition>='" & dateconvert(DateDebutPartition) & "' AND p.DateFinPartition<='" & dateconvert(DateFinPartition) & "'"
            Dim dt3 As DataTable = ExcecuteSelectQuery(query)
            ReDim OldActivites(dt3.Rows.Count, 3)
            Dim i As Decimal = 0
            CpteOldActivites = dt3.Rows.Count

            For Each rw3 As DataRow In dt3.Rows
                Dim drs = dtdoc.NewRow()
                drs("CodePartition") = rw3("CodePartition").ToString
                drs("Activité") = rw3("LibelleCourt").ToString
                drs("Libellé de l'activité") = MettreApost(rw3("libellepartition").ToString)
                drs("Compte comptable") = rw3("NumeroComptable").ToString
                drs("Montant de l'activité") = AfficherMonnaie(rw3("Montant_libellecourt").ToString)
                dtdoc.Rows.Add(drs)

                'On stock les anciennes activités dans notre tableau
                OldActivites(i, 0) = rw3("CodePartition").ToString
                OldActivites(i, 1) = rw3("LibelleCourt").ToString
                OldActivites(i, 2) = rw3("NumeroComptable").ToString
                OldActivites(i, 3) = rw3("Montant_libellecourt").ToString
                i += 1
            Next

            LgListAct.DataSource = dtdoc
            Viewact.OptionsView.ColumnAutoWidth = True
            '  Viewact.Columns("CodePartition").Visible=false
            Viewact.OptionsBehavior.AutoExpandAllGroups = True
            Viewact.VertScrollVisibility = True
            Viewact.HorzScrollVisibility = True
            Viewact.BestFitColumns()
            Viewact.Columns("CodePartition").Visible = False
            Viewact.Columns("Activité").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            Viewact.Columns("Compte comptable").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            Viewact.Columns("Montant de l'activité").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

        Next

        'For i = 0 To liste_engagement.ViewEngag.RowCount - 1

        '    If CBool(liste_engagement.ViewEngag.GetRowCellValue(i, "Code")) = True Then
        '    End If
        'Next
    End Sub

    Private Sub NewEnabled(value As Boolean)
        txttypemarche.Enabled = value
        txtmethode.Enabled = value
        txtmontant.Enabled = value
        NumDMP.Enabled = value
        DateMarche.Enabled = value
        BtAjout.Enabled = value
        CmbBaill.Enabled = value
        cmbRevue.Enabled = value
        CmbCatDep.Enabled = value
        TxtLotMarche.Enabled = value
        TxtMontanteng.Enabled = value
        NumDMP.Enabled = Not value
    End Sub

    Private Sub RemplirBailleur()

        query = "select InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' order by InitialeBailleur"
        CmbBaill.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbBaill.Properties.Items.Add(rw(0))
        Next

    End Sub

    Private Sub RemplirFRS()

        query = "select * from T_COMP_COMPTE where Code_Projet='" & ProjetEnCours & "' order by code_cpt"
        TxtFournisMarche.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            TxtFournisMarche.Properties.Items.Add(rw(0).ToString & " | " & MettreApost(rw(4).ToString))
        Next

    End Sub

    Private Sub LoadTypeMarche()

        query = "select TypeMarche from t_typemarche"
        txttypemarche.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            txttypemarche.Properties.Items.Add(MettreApost(rw("TypeMarche").ToString))
        Next

    End Sub

    Private Sub LoadActivite(ByVal PeriodeMarche As String)

        'query = "select libellecourt, libellepartition from t_partition where { fn LENGTH(LibelleCourt) } >= 5 AND (DateDebutPartition>='" & dateconvert(DateDebutPartition) & "' AND DateFinPartition<='" & dateconvert(DateFinPartition) & "')"
        'Combact.Properties.Items.Clear()
        'Dim dt As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt.Rows
        '    Combact.Properties.Items.Add(rw(0).ToString & " | " & MettreApost(rw(1).ToString))
        'Next

        'BON DE COMMANDE
        If PeriodeMarche = "" Then
            query = "select codepartition, libellecourt, libellepartition, DateDebutPartition, DateFinPartition from t_partition where CodeClassePartition=5 ORDER BY LibelleCourt ASC"
        Else
            'MARCHE GENERER
            Dim Periode As String() = PeriodeMarche.ToString.Split("-")
            query = "select codepartition, libellecourt, libellepartition, DateDebutPartition, DateFinPartition, LibelleCourt from t_partition where CodeClassePartition=5 AND DateDebutPartition>='" & dateconvert(Periode(0).Trim) & "' and DateDebutPartition<='" & dateconvert(Periode(1).Trim) & "' ORDER BY LibelleCourt ASC"
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
            Combact.Properties.Items.Add(rw("libellecourt").ToString & " | " & MettreApost(rw("libellepartition").ToString) & " | " & CDate(rw("DateFinPartition")).Year)
        Next
    End Sub

    Private Sub Combact_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles Combact.KeyPress, txtcompte.KeyPress, TxtMontanteng.KeyPress
        Select Case e.KeyChar
            Case ControlChars.CrLf
                BtAjout_Click(Me, e)
            Case Else
        End Select
    End Sub
    Private Sub BtEnr_Click(sender As System.Object, e As System.EventArgs) Handles BtEnr.Click
        Dim erreur As String = ""

        If txtnbon.Text = "" Then
            erreur += "Renseigner le numéro du bon de commande"
            FailMsg(erreur)
            txtnbon.Focus()
            Exit Sub
        End If

        If TxtLotMarche.Text = "" Then
            erreur += "Renseigner le libellé"
            FailMsg(erreur)
            TxtLotMarche.Focus()
            Exit Sub
        End If

        If TxtFournisMarche.SelectedIndex = -1 Then
            erreur += "Choississez le fournisseur"
            FailMsg(erreur)
            TxtFournisMarche.Select()
            Exit Sub
        End If

        If CmbBaill.Text.Trim = "" Then
            erreur += "Choississez le bailleur"
            FailMsg(erreur)
            CmbBaill.Select()
            Exit Sub
        End If

        If CmbConv.Text.Trim = "" Then
            erreur += "Choississez la convention"
            FailMsg(erreur)
            CmbConv.Select()
            Exit Sub
        End If

        If CmbCatDep.Text.Trim = "" Then
            erreur += "Choississez la Catégorie de dépense"
            FailMsg(erreur)
            CmbCatDep.Select()
            Exit Sub
        End If

        If txtmontant.Text.Length = 0 Then
            erreur += "Entrer le montant du marché"
            FailMsg(erreur)
            txtmontant.Focus()
            Exit Sub
        ElseIf Val(txtmontant.Text) = 0 Then
            FailMsg("Le montant du marché doit être supérieur à 0")
            txtmontant.Focus()
            Exit Sub
        End If

        If txttypemarche.Text.Trim = "" Then
            erreur += "Choississez le type de marché"
            FailMsg(erreur)
            txttypemarche.Select()
            Exit Sub
        End If

        If txtmethode.Text.Trim = "" Then
            erreur += "Choississez la méthode"
            FailMsg(erreur)
            txtmethode.Select()
            Exit Sub
        End If

        If DateMarche.Text.Trim = "" Then
            erreur += "Renseigner la date du marché"
            FailMsg(erreur)
            DateMarche.Select()
            Exit Sub
        End If

        If Viewact.RowCount = 0 Then
            erreur += "Entrer les activités et comptes comptables imputables pour le bon de commande"
            FailMsg(erreur)
            Combact.Select()
            Exit Sub
        End If

        If TxtPieceJointe.Text.Trim = "" Then
            erreur += "Veuillez joindre le bon de commande"
            FailMsg(erreur)
            TxtPieceJointe.Focus()
            Exit Sub
        End If

        Dim somact As Double = 0
        For i = 0 To Viewact.RowCount - 1
            somact = somact + CDec(Viewact.GetRowCellValue(i, "Montant de l'activité").ToString.Replace(" ", ""))
        Next

        If CDbl(txtmontant.Text.Replace(Chr(160), "")) <> (CDbl(somact.ToString)) Then
            erreur += "La somme des montants des activités doit être égale au montant du marché"
            FailMsg(erreur)
            txtmontant.Focus()
            Exit Sub
        End If

        'On verifie la liste des activites a celle contenue dans notre tableau, si il y'a eu un changement on supprime l'ancien pour garder la modification
        'query = "select refmarche from t_marche where NumeroMarche='" & NumMarche & "'"
        'Dim RefMarche As String = ExecuteScallar(query)

        'If CpteOldActivites > 0 Then 'On vérifie si le bc ou le marché n'a pas encore d'activités liées
        '    For i = 0 To (CpteOldActivites - 1)
        '        Dim trouv As Boolean = False
        '        For l = 0 To (Viewact.RowCount - 1)
        '            Dim rw As DataRow = Viewact.GetDataRow(l)
        '            If OldActivites(i, 0) = rw("CodePartition") And OldActivites(i, 1) = rw("Activité") And OldActivites(i, 2) = rw("Compte comptable") And OldActivites(i, 3) = CDec(rw("Montant de l'activité").ToString().Replace(Chr(160), "")) Then
        '                trouv = True
        '                Exit For
        '            End If
        '        Next

        '        If Not trouv Then ' Si notre imputation n'existe plus, on la supprime de la bd
        '            query = "delete from t_acteng where Refmarche='" & RefMarche & "' and LibelleCourt='" & OldActivites(i, 0) & "' and NumeroComptable='" & OldActivites(i, 1) & "' and Montant_libellecourt='" & OldActivites(i, 2) & "'"
        '            ExecuteNonQuery(query)
        '        End If
        '    Next
        'End If

        Dim frs() As String
        frs = TxtFournisMarche.Text.Split(" | ")

        Dim cat() As String
        cat = CmbCatDep.Text.Split(" | ")

        Dim meth() As String
        meth = txtmethode.Text.Split(" | ")

        Dim periode As String
        periode = "01/01/" & Year(DateMarche.Text) & " - 31/12/" & Year(DateMarche.Text)

        If PeriodsMarches.ToString = "" Then 'Modification bon de commande

            query = "update t_marche SET TypeMarche='" & EnleverApost(txttypemarche.Text) & "', DescriptionMarche='" & EnleverApost(TxtLotMarche.Text) & "', InitialeBailleur='" & EnleverApost(CmbBaill.Text) & "', CodeConvention='" & EnleverApost(CmbConv.Text) & "', Convention_ChefFile='" & EnleverApost(CmbConv.Text) & "',  MontantEstimatif='" & CDec(txtmontant.Text) & "', MethodeMarche='" & CodeProcAO.ToString & "', CodeProcAO='" & CodeProcAO.ToString & "', RevuePrioPost='" & EnleverApost(cmbRevue.Text) & "', PeriodeMarche='" & periode.ToString & "' where RefMarche='" & RefsMarches & "'"
            ExecuteNonQuery(query)
            query = "update t_marchesigne set DateMarche='" & DateMarche.Text & "', TypeMarche='" & EnleverApost(txttypemarche.Text) & "', MontantHT='" & CDec(txtmontant.Text) & "', CodeCateg='" & codecat.ToString & "', Attributaire='" & frs(0).ToString & "' where NumeroMarche='" & EnleverApost(txtnbon.Text) & "'"
            ExecuteNonQuery(query)

            'Save engagement
            SaveEngagement()

        ElseIf ModePPM = "Genere" Then 'Update PPM generer a travers les fiches d'activités
            ExecuteNonQuery("update t_marchesigne set NumMarcheDMP='" & EnleverApost(NumDMP.Text) & "', Attributaire='" & frs(0).ToString & "' where NumeroMarche='" & EnleverApost(NumMarche) & "'")
        Else 'Update PPM saisie ou importer
            ExecuteNonQuery("update t_marchesigne set NumMarcheDMP='" & EnleverApost(NumDMP.Text) & "', Attributaire='" & frs(0).ToString & "' where NumeroMarche='" & EnleverApost(NumMarche) & "'")
            SaveEngagement()
        End If

        If TxtChemin.Text.Length > 0 Then
            Try
                Dim NomFichier As String = line & "\Marches\"
                If Not Directory.Exists(NomFichier) Then
                    Directory.CreateDirectory(NomFichier)
                End If
                Dim NewPieceName As String = FormatFileName(txtnbon.Text, "_") & "." & TxtPieceJointe.Text.Split(".")(1)
                NomFichier = NomFichier & "\" & NewPieceName
                File.Copy(TxtChemin.Text, NomFichier, True)
            Catch ex As Exception
            End Try
        End If

        'effacer les activités enregistrées
        dtdoc.Rows.Clear()

        'effacer les champs remplis
        EffacerTexBox4(PanelControl6)

        SuccesMsg("Modification effectuée avec succès.")
        liste_engagement.MustRefresh = True
        Me.Close()

    End Sub

    Private Sub SaveEngagement()
        Try
            Dim refbesoin As String = ""
            Dim mont As Double = 0

            For i = 0 To Viewact.RowCount - 1

                ' query = "select count(*) from t_acteng where LibelleCourt='" & Viewact.GetDataRow(i)(0).ToString & "' and RefMarche='" & RefMarche.ToString & "' and NumeroComptable='" & Viewact.GetDataRow(i)(2) & "' and Montant_libellecourt='" & CDec(Viewact.GetDataRow(i)(3)) & "'"
                query = "select count(*) from t_acteng where codePartition='" & Viewact.GetDataRow(i)("CodePartition").ToString & "' and LibelleCourt='" & Viewact.GetDataRow(i)("Activité").ToString & "' and RefMarche='" & RefsMarches.ToString & "' and NumeroComptable='" & Viewact.GetDataRow(i)("Compte comptable") & "' and Montant_libellecourt='" & CDec(Viewact.GetDataRow(i)("Montant de l'activité")) & "'"
                Dim nbre As Decimal = Val(ExecuteScallar(query))

                If nbre = 0 Then
                    mont = CDec(Viewact.GetDataRow(i)("Montant de l'activité").ToString)
                    Try
                        query = "insert into t_acteng values ('" & Viewact.GetDataRow(i)("Activité").ToString & "', '" & Viewact.GetDataRow(i)("Compte comptable").ToString() & "','" & RefsMarches & "','" & mont.ToString & "', '" & Viewact.GetDataRow(i)("CodePartition").ToString & "')"
                        ExecuteNonQuery(query)
                    Catch ex As Exception
                        'On insere toutes les activites dans la bd, une exception de type duplicate Key sera leve si la repartion existe deja, on ne gere pas cette erreur vu que la repartion existe deja
                    End Try

                    ' Dim codepart As String = ""
                    ' query = "select codepartition from t_partition where libellecourt='" & Viewact.GetDataRow(i)(0).ToString & "'"
                    ' codepart = ExecuteScallar(quer

                    refbesoin = ExecuteScallar("select RefBesoinPartition from t_besoinpartition where codepartition='" & Viewact.GetDataRow(i)("CodePartition").ToString & "' and numerocomptable='" & Viewact.GetDataRow(i)("Compte comptable").ToString & "'")
                    If refbesoin <> "" Then
                        ExecuteNonQuery("update t_repartitionparbailleur set RefMarche='" & RefsMarches & "' where RefBesoinPartition='" & IIf(refbesoin.ToString = Nothing, 0, refbesoin.ToString) & "'")
                        ExecuteNonQuery("insert into t_besoinmarche values('" & IIf(refbesoin.ToString = Nothing, 0, refbesoin.ToString) & "', '" & RefsMarches.ToString & "')")
                    End If

                End If
            Next

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub SupprimerLactivitéToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SupprimerLactivitéToolStripMenuItem.Click

        If Viewact.RowCount > 0 Then
            Try
                If MsgBox("Voulez-vous vraiment supprimer?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                    drx = Viewact.GetDataRow(Viewact.FocusedRowHandle)

                    If ModePPM <> "Genere" Or PeriodsMarches.ToString = "" Then 'bon de commande et marche importé ou saisie
                        Dim RefBesoinPartition As String = ExecuteScallar("select RefBesoinPartition from t_besoinpartition where codepartition='" & drx("CodePartition").ToString & "' and numerocomptable='" & drx("Compte comptable").ToString & "' and CodeProjet='" & ProjetEnCours & "'")
                        ExecuteNonQuery("delete from t_besoinmarche where RefBesoinPartition='" & RefBesoinPartition & "' RefMarche='" & RefsMarches.ToString & "'")
                        ExecuteNonQuery("update t_repartitionparbailleur set RefMarche='0' where RefBesoinPartition='" & IIf(RefBesoinPartition.ToString = Nothing, 0, RefBesoinPartition.ToString) & "'")
                    End If

                    ExecuteNonQuery("delete from t_acteng where RefMarche='" & RefsMarches.ToString & "' and LibelleCourt='" & drx("Activité").ToString & "' and NumeroComptable='" & drx("Compte comptable").ToString & "' and Montant_libellecourt='" & drx("Montant de l'activité").ToString.Replace(" ", "") & "' and codepartition='" & drx("CodePartition").ToString & "'")
                    Viewact.GetDataRow(Viewact.FocusedRowHandle).Delete()
                End If
            Catch ex As Exception
                FailMsg("Erreur : Information non disponible : " & ex.ToString())
            End Try
        End If


        'Try
        '    If Viewact.RowCount > 0 Then
        '        If ConfirmMsg("Voulez-vous supprimer cette activité ?") = DialogResult.Yes Then
        '            Viewact.GetDataRow(Viewact.FocusedRowHandle).Delete()
        '        End If
        '    End If
        'Catch ex As Exception
        '    FailMsg("Erreur : Information non disponible : " & ex.ToString())
        'End Try

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

        If PeriodsMarches.ToString <> "" And ModePPM <> "Genere" And CmbConv.Enabled = True And CmbConv.Text.Trim = "" Then
            SuccesMsg("Veuillez selectionné la convention")
            CmbConv.Select()
            Exit Sub
        End If

        If TxtMontanteng.Text.Length = 0 Or Val(TxtMontanteng.Text) = 0 Then
            FailMsg("Entrer le montant pour le compte " & txtcompte.Text.Split(" | ")(0) & " sur l'activité " & Combact.Text.Split(" | ")(0))
            TxtMontanteng.Focus()
            Exit Sub
        End If

        Dim somact As Double = 0
        For i = 0 To Viewact.RowCount - 1
            somact = somact + CDec(Viewact.GetRowCellValue(i, "Montant de l'activité").ToString.Replace(" ", ""))
        Next

        If txtmontant.Text.Length > 0 And TxtMontanteng.Text.Length > 0 Then
            If CDbl(txtmontant.Text.Replace(Chr(160), "")) < (CDbl(somact.ToString) + CDbl(TxtMontanteng.Text.Replace(Chr(160), ""))) Then
                FailMsg("La somme des montants des activités ne doit pas excéder le montant du marché")
                TxtMontanteng.Focus()
                Exit Sub
            End If
        End If

        Dim Activite() As String
        Activite = Combact.Text.Split(" | ")

        Dim souscompte() As String
        souscompte = txtcompte.Text.Split(" | ")

        Dim MontantTotalActivite As Decimal = Val(ExecuteScallar("select sum(QteNature*PUNature) from t_besoinpartition where CodePartition='" & TablPartition(Combact.SelectedIndex) & "' and CodeProjet='" & ProjetEnCours & "'"))
        Dim MontantAtiviteEngage As Decimal = 0

        'Montant activite engage a partir des marches
        MontantAtiviteEngage = Val(ExecuteScallar("SELECT SUM(Montant_libellecourt) from t_acteng where codepartition='" & TablPartition(Combact.SelectedIndex) & "'"))
        MontantAtiviteEngage += Val(ExecuteScallar("SELECT SUM(Montant_act) from t_comp_activite where codepartition='" & TablPartition(Combact.SelectedIndex) & "' and CODE_PROJET='" & ProjetEnCours & "'"))

        'For i = 0 To Viewact.RowCount - 1
        '    If Viewact.GetDataRow(i)("CodePartition").ToString = TablPartition(Combact.SelectedIndex) Then
        '        MontantAtiviteEngage += CDec(Viewact.GetDataRow(i)("Montant de l'activité"))
        '    End If
        'Next

        'Verifier si le montant de l'activité est deja utiliser
        If MontantTotalActivite < MontantAtiviteEngage + Val(TxtMontanteng.Text.Replace(" ", "")) Then
            SuccesMsg("Le montant de l'activité est déjà consommé")
            Combact.Select()
            Exit Sub
        End If

        'For i = 0 To Viewact.RowCount - 1
        '    If Viewact.GetDataRow(i)(0).ToString = Activite(0).ToString() And Viewact.GetDataRow(i)(2) = souscompte(0) Then
        '        SuccesMsg("Cette répartition existe déjà")
        '        Combact.Select()
        '        Exit Sub
        '    End If
        'Next

        Dim drs = dtdoc.NewRow()

        drs("CodePartition") = TablPartition(Combact.SelectedIndex)
        drs("Activité") = Activite(0).ToString.Trim()
        drs("Libellé de l'activité") = Activite(1).ToString.Trim()
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
        Viewact.Columns("Activité").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Viewact.Columns("Compte comptable").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Viewact.Columns("Montant de l'activité").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        'On vide la ligne de l'imputation

        Combact.SelectedIndex = -1
        TxtMontanteng.ResetText()
        Combact.Select()
        If PeriodsMarches.ToString <> "" And ModePPM <> "Genere" And CmbConv.Enabled = True Then
            CmbConv.Text = ""
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dialog_form(Creer_compte_tier)
    End Sub

    Private Sub LgListAct_Click(sender As System.Object, e As System.EventArgs) Handles LgListAct.Click
        If (Viewact.RowCount > 0) Then
            drx = Viewact.GetDataRow(Viewact.FocusedRowHandle)
            Dim IDL = drx(0).ToString
            ColorRowGrid(Viewact, "[Libellé de l'activité]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(Viewact, "[Activité]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
        End If
    End Sub


    Private Sub txtmontant_TextChanged(sender As Object, e As System.EventArgs) Handles txtmontant.TextChanged
        txtmontant.Text = AfficherMonnaie(txtmontant.Text)
    End Sub

    Private Sub CmbConv_TextChanged(sender As Object, e As System.EventArgs) Handles CmbConv.TextChanged
        If PeriodsMarches = "" Then
            remplirCategorie()
        End If
    End Sub
    Private Sub CmbConv_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbConv.SelectedIndexChanged

        If PeriodsMarches = "" Then 'remplir la categorie de depense en cas de bon de commande
            remplirCategorie()
        Else 'Concerseve la categorie de depense de la convention chef fill
            If ModePPM <> "Genere" Then
                CmbBaill.Text = GetInitialbailleur(EnleverApost(CmbConv.Text.Trim))
                TxtMontanteng.Text = AfficherMonnaie(ExecuteScallar("SELECT Montant from t_ppm_repartitionbailleur where CodeConvention='" & EnleverApost(CmbConv.Text.Trim) & "' and RefMarche='" & RefsMarches & "'"))
            End If
        End If
    End Sub

    Private Sub CmbConv_Validated(sender As Object, e As System.EventArgs) Handles CmbConv.Validated
        If PeriodsMarches = "" Then
            remplirCategorie()
        End If
    End Sub

    Private Sub CmbBaill_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbBaill.SelectedIndexChanged

        query = "select CodeBailleur, InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' and InitialeBailleur='" & EnleverApost(CmbBaill.Text) & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CodeBailleurCache.Text = rw("CodeBailleur")
        Next
        ChargerConvention(CodeBailleurCache.Text)
    End Sub

    Private Sub remplirCategorie()

        'remplir les sous classe du plan comptable
        CmbCatDep.Properties.Items.Clear()
        query = "select CodeCateg, LibelleCateg from t_categoriedepense where CodeConvention='" & EnleverApost(CmbConv.Text) & "' ORDER BY NumCateg"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbCatDep.Properties.Items.Add(rw("CodeCateg").ToString & " | " & MettreApost(rw("LibelleCateg").ToString))
        Next

    End Sub

    Private Sub ChargerConvention(ByVal bail As String)

        'query = "select CodeConvention from T_Convention where CodeBailleur='" & bail & "' order by CodeConvention"
        'CmbConv.Properties.Items.Clear()
        'Dim dt As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt.Rows
        '    CmbConv.Properties.Items.Add(rw(0).ToString)
        'Next

        'If (CmbConv.Properties.Items.Count > 0) Then
        '    CmbConv.SelectedIndex = 0
        'End If

        If PeriodsMarches.ToString = "" Then
            query = "select CodeConvention from T_Convention where CodeBailleur='" & bail & "' order by CodeConvention"
        ElseIf ModePPM <> "Genere" Then
            'PPM Importer
            query = "select CodeConvention from t_ppm_repartitionbailleur where RefMarche='" & RefsMarches & "' order by CodeConvention"
        End If

        CmbConv.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbConv.Properties.Items.Add(rw("CodeConvention").ToString)
        Next

        If PeriodsMarches.ToString = "" Then
            If (CmbConv.Properties.Items.Count > 0) Then CmbConv.SelectedIndex = 0
        End If
    End Sub


    'Private Sub TxtPieceJointe_Click(sender As Object, e As System.EventArgs) Handles TxtPieceJointed.Click
    '    Dim dlg As New OpenFileDialog
    '    dlg.Filter = "Documents|*.pdf|Images|*.jpg;*.jpeg;*.png;*.gif;*.bmp"
    '    Dim rep = dlg.ShowDialog
    '    If rep = DialogResult.OK Then
    '        TxtChemin.Text = dlg.FileName
    '        Dim ext = New System.IO.FileInfo(dlg.FileName).Extension
    '        TxtPieceJointed.Text = txtnbon.Text.Replace("/", "-") & "" & ext.ToString
    '    End If
    'End Sub

    Private Sub txttypemarche_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles txttypemarche.SelectedIndexChanged

        query = "select AbregeAO, LibelleAO from T_ProcAO where TypeMarcheAO ='" & EnleverApost(txttypemarche.Text) & "'"
        txtmethode.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            txtmethode.Properties.Items.Add(rw("AbregeAO").ToString & " | " & MettreApost(rw("LibelleAO").ToString))
        Next

    End Sub

    Private Sub CmbCatDep_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbCatDep.SelectedIndexChanged
        Dim cat() As String
        cat = CmbCatDep.Text.Split(" | ")
        query = "select CodeCateg from t_categoriedepense where NumCateg='" & cat(0).ToString & "' and CodeConvention='" & EnleverApost(CmbConv.Text) & "'"
        codecat = Val(ExecuteScallar(query))
    End Sub

    Private Sub CmbBaill_TextChanged(sender As Object, e As System.EventArgs) Handles CmbBaill.TextChanged
        query = "select CodeBailleur, InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' and InitialeBailleur='" & EnleverApost(CmbBaill.Text) & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)

        For Each rw As DataRow In dt.Rows
            CodeBailleurCache.Text = rw("CodeBailleur")
        Next

        ChargerConvention(CodeBailleurCache.Text)
    End Sub

    Private Sub txttypemarche_TextChanged(sender As Object, e As System.EventArgs) Handles txttypemarche.TextChanged
        query = "select AbregeAO, LibelleAO from T_ProcAO where TypeMarcheAO ='" & EnleverApost(txttypemarche.Text) & "'"
        txtmethode.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            txtmethode.Properties.Items.Add(rw(0).ToString & " | " & MettreApost(rw(1).ToString))
        Next
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        Try

            Dim filepath As String = line & "\Marches\" & FormatFileName(TxtPieceJointe.Text, "_")
            If File.Exists(filepath) Then
                Process.Start(filepath)
                'If New FileInfo(filepath).Exists.ToString().ToLower() = ".pdf" Then
                'ElseIf New FileInfo(filepath).Exists.ToString().ToLower() = ".pdf" Or
                'End If
            Else
                If File.Exists(TxtChemin.Text) Then
                    Process.Start(TxtChemin.Text)
                End If
            End If

        Catch ex As Exception
            FailMsg("Erreur")
        End Try
    End Sub

    Private Sub Combact_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Combact.SelectedIndexChanged
        If Combact.SelectedIndex > -1 Then
            ' Dim CodeAct As String = Combact.Text.Split(" | ")(0)
            ' query = "SELECT codepartition FROM t_partition where LibelleCourt='" & CodeAct & "' AND (DateDebutPartition>='" & dateconvert(DateDebutPartition) & "' AND DateFinPartition<='" & dateconvert(DateFinPartition) & "')"
            ' Dim codePartition As String = ExecuteScallar(query)
            'loadCpteCollectif(codePartition)
            loadCpteCollectif(TablPartition(Combact.SelectedIndex))
        Else
            loadCpteCollectif(-1)
        End If
    End Sub

    Private Sub loadCpteCollectif(CodePartition As String)
        query = "SELECT CODE_SC,LIBELLE_SC FROM t_comp_sous_classe WHERE CODE_SC IN (SELECT NumeroComptable FROM t_besoinpartition WHERE CodePartition='" & CodePartition & "')"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        txtcompte.Properties.Items.Clear()
        txtcompte.ResetText()
        For Each rw As DataRow In dt.Rows
            txtcompte.Properties.Items.Add(rw("CODE_SC") & " | " & MettreApost(rw("LIBELLE_SC")))
        Next
    End Sub

    Private Sub PnlPieceJustif_Click(sender As Object, e As EventArgs) Handles PnlPieceJustif.Click
        Dim OpenFile As New OpenFileDialog()
        Try
            OpenFile.Filter = "Document PDF|*.pdf"
            'OpenFile.Filter = "Tout|*.pdf;*.jpg;*.jpeg;*.png;*.gif;*.bmp|Documents|*.pdf|Images|*.jpg;*.jpeg;*.png;*.gif;*.bmp"
            OpenFile.FileName = ""
            If OpenFile.ShowDialog() = DialogResult.OK Then
                TxtChemin.Text = OpenFile.FileName
                TxtPieceJointe.Text = OpenFile.SafeFileName
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

    Private Sub LgListAct_MouseUp(sender As Object, e As MouseEventArgs) Handles LgListAct.MouseUp
        If Viewact.RowCount > 0 Then
            If ModePPM = "Genere" And PeriodsMarches.ToString <> "" Then
                ContextMenuStrip1.Items(0).Visible = False
            Else
                ContextMenuStrip1.Items(0).Visible = True
            End If
        End If
    End Sub

    Private Sub txtmethode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtmethode.SelectedIndexChanged
        CodeProcAO = ""
        If txtmethode.SelectedIndex <> -1 Then
            Dim Code() As String = txtmethode.Text.Split(" | ")
            CodeProcAO = ExecuteScallar("SELECT CodeProcAO from t_procao where AbregeAO='" & Code(0).ToString & "' and TypeMarcheAO='" & EnleverApost(txttypemarche.Text) & "' and CodeProjet='" & ProjetEnCours & "'")
        End If
    End Sub
End Class