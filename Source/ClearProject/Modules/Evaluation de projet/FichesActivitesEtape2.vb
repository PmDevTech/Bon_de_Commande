Imports MySql.Data.MySqlClient

Public Class FichesActivitesEtape2
    'initialisation des variables
    Dim dtBesoin = New DataTable()
    Dim dtBailleur = New DataTable()
    Dim DrX As DataRow
    Dim nbTp As Decimal = 0
    Dim boolmodif As Boolean = False

    Private Sub ChargerArticle()
        'fonction permettant de remplir le combobox
        query = "select ART_ID, ART_LIB from RP_ARTICLE order by ART_ID"
        Cmbarticle.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            Cmbarticle.Properties.Items.Add(rw(0).ToString & "   " & MettreApost(rw(1).ToString))
        Next
    End Sub

    Private Sub FichesActivitesEtape2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        'au chargement de la page, exécution des fonctions et condition
        Try
            'date
            Datedebub.Text = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
            DateFin.Text = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")

            Datedebub.Properties.MinValue = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
            Datedebub.Properties.MaxValue = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")

            DateFin.Properties.MinValue = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
            DateFin.Properties.MaxValue = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")

            Dim Nbre As Decimal = 0
            dtBailleur.Columns.Clear()
            dtBailleur.Columns.Add("CodeX", Type.GetType("System.String"))
            dtBailleur.Columns.Add("Ref", Type.GetType("System.String"))
            dtBailleur.Columns.Add("Bailleur", Type.GetType("System.String"))
            dtBailleur.Columns.Add("Convention", Type.GetType("System.String"))
            dtBailleur.Columns.Add("Montant", Type.GetType("System.String"))
            dtBailleur.Columns.Add("%", Type.GetType("System.String"))

            query = "select COUNT(*) from T_Partition where CodeProjet='" & ProjetEnCours & "'"
            dt = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                Nbre = dt.Rows(0).Item(0)
            End If

            If Nbre = 0 Then
            Else
                RemplirCompo()
                RemplirListeActivites()
                RemplirCompte()
                RemplirUnite()
                RemplirBailleur()
            End If

            CmbCompo.Focus()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub RemplirCompo()
        'remplissage de la liste de données
        query = "select LibelleCourt, LibellePartition from T_Partition where LENGTH(LibelleCourt)=1 and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
        CmbCompo.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            CmbCompo.Properties.Items.Add(rw(0).ToString & " : " & MettreApost(rw(1).ToString))
        Next
    End Sub

    Private Sub CmbCompo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCompo.SelectedValueChanged
        'appel des fonctions lorsde la selection d'un élément dans un combobox
        RemplirSousCompo()
        RemplirListeActivites()
    End Sub

    Private Sub RemplirSousCompo()
        'remplissage
        CmbSousCompo.Text = ""
        CmbSousCompo.Properties.Items.Clear()
        If (CmbCompo.Text <> "") Then
            query = "select LibelleCourt, LibellePartition from T_Partition where CodeClassePartition=2 and LibelleCourt like '" & Mid(CmbCompo.Text, 1, 1) & "%' and CodeProjet='" & ProjetEnCours & "' order by length(libelleCourt),libelleCourt"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                CmbSousCompo.Properties.Items.Add(rw(0).ToString & " : " & MettreApost(rw(1).ToString))
            Next
        End If
    End Sub

    Private Sub CmbSousCompo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSousCompo.SelectedValueChanged
        TxtCodeMere.Text = ""
        If (CmbSousCompo.Text <> "") Then
            Dim codeAct() As String
            codeAct = CmbSousCompo.Text.Split(" : ")
            query = "select CodePartition from T_Partition where CodeClassePartition=2 and LibelleCourt='" & codeAct(0).ToString & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                TxtCodeMere.Text = rw(0).ToString
            Next

        End If

        RemplirListeActivites()
    End Sub

    Private Sub RemplirListeActivites()
        'remplissage
        Try
            If Not IsDate(Datedebub.Text) And Not IsDate(DateFin.Text) Then
                Exit Sub
            End If

            Dim codeAct() As String
            codeAct = CmbSousCompo.Text.Split(" : ")

            Dim clause As String = ""


            'Requete Date
            If DateTime.Compare(CDate(DateFin.Text), CDate(DateFin.Text)) >= 0 Then
                clause = " AND dateDebutPartition >='" & dateconvert(Datedebub.Text) & "' AND dateFinPartition <='" & dateconvert(DateFin.Text) & "' order by LibelleCourt"
            Else
                SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
            End If

            TxtCodeActiv.Text = ""

            CmbActivite.Text = ""
            CmbActivite.Properties.Items.Clear()
            query = "select CodePartition,LibelleCourt,LibellePartition from T_Partition where CodeClassePartition='5' and CodePartitionMere='" & TxtCodeMere.Text & "' and CodeProjet='" & ProjetEnCours & "'" & clause
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                CmbActivite.Properties.Items.Add(rw("LibelleCourt").ToString & " - " & MettreApost(rw("LibellePartition").ToString))
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try


    End Sub

    Private Sub CmbActivite_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbActivite.SelectedValueChanged
        'lors de la selection d'une valeur, affectation du codepartition dans un textbox
        If (CmbActivite.Text <> "") Then
            BtAjoutBesoin.Enabled = True

            Dim LibelleCourt As String = Split(CmbActivite.Text, " - ")(0)
            query = "SELECT CodePartition from T_Partition where LibelleCourt='" & LibelleCourt & "' AND DateDebutPartition>='" & dateconvert(Datedebub.Text) & "' AND DateFinPartition<='" & dateconvert(DateFin.Text) & "' AND CodeProjet='" & ProjetEnCours & "'"
            TxtCodeActiv.Text = ExecuteScallar(query)
            'query = "select CodePartition from T_Partition where LibelleCourt='" & PartLib(0) & "' and CodeProjet='" & ProjetEnCours & "'"
            'Dim dt As DataTable = ExcecuteSelectQuery(query)
            'For Each rw In dt.Rows
            '    TxtCodeActiv.Text = rw(0).ToString
            'Next
        Else
            BtAjoutBesoin.Enabled = False
            TxtCodeActiv.Text = ""
        End If

        RemplirBesoin()

    End Sub

    Private Sub RemplirBesoin()
        'remplir le tableau des besoins
        Try

            dtBesoin.Columns.Clear()

            dtBesoin.Columns.Add("CodeX", Type.GetType("System.String"))
            dtBesoin.Columns.Add("Ref", Type.GetType("System.String"))
            dtBesoin.Columns.Add("Marche", Type.GetType("System.String"))
            dtBesoin.Columns.Add("Compte", Type.GetType("System.String"))
            dtBesoin.Columns.Add("Type", Type.GetType("System.String"))
            dtBesoin.Columns.Add("Libellé", Type.GetType("System.String"))
            dtBesoin.Columns.Add("Qté", Type.GetType("System.String"))
            dtBesoin.Columns.Add("Unité", Type.GetType("System.String"))
            dtBesoin.Columns.Add("Prix Unitaire", Type.GetType("System.String"))
            dtBesoin.Columns.Add("Prix Total", Type.GetType("System.String"))

            Dim Bailleur As String()
            Dim nbBaill As Integer = 0

            query = "select InitialeBailleur, CodeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' order by InitialeBailleur"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            ReDim Bailleur(dt.Rows.Count)
            For Each rw In dt.Rows
                Bailleur(nbBaill) = rw(1).ToString
                nbBaill += 1

                dtBesoin.Columns.Add("Montant " & rw(0).ToString, Type.GetType("System.String"))
                dtBesoin.Columns.Add("Convention " & rw(0).ToString, Type.GetType("System.String"))
                dtBesoin.Columns.Add("% " & rw(0).ToString, Type.GetType("System.String"))
            Next


            dtBesoin.Columns.Add("Montant GAP", Type.GetType("System.String"))
            dtBesoin.Columns.Add("% GAP", Type.GetType("System.String"))

            dtBesoin.Rows.Clear()

            Dim PrixTot As Decimal = 0
            Dim TotBail(nbBaill) As Decimal
            For k As Integer = 0 To (nbBaill - 1)
                TotBail(k) = 0
            Next

            Dim TotGap As Decimal = 0
            Dim NbTotal As Decimal = 0
            query = "select RefBesoinPartition, RefMarche, NumeroComptable, TypeBesoin, LibelleBesoin, QteNature, UniteBesoin, PUNature from T_BesoinPartition where CodePartition='" & TxtCodeActiv.Text & "' order by NumeroComptable"
            dt = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                NbTotal += 1
                Dim TotLigNe As Decimal = 0

                Dim drS = dtBesoin.NewRow()

                drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                drS(1) = rw(0).ToString
                drS(2) = rw(1).ToString
                drS(3) = rw(2).ToString
                drS(4) = rw(3).ToString
                drS(5) = MettreApost(rw(4).ToString)
                drS(6) = AfficherMonnaie(rw(5).ToString)
                drS(7) = rw(6).ToString
                drS(8) = AfficherMonnaie(rw(7).ToString)
                TotLigne = Math.Ceiling(CDec(rw(5)) * CDec(rw(7)))
                PrixTot += TotLigne
                drS(9) = AfficherMonnaie(TotLigne)

                Dim totPartielbail As Decimal = 0
                For k As Integer = 0 To (nbBaill - 1)
                    Dim DocBaill() As String = InfosBailleur(rw(0).ToString, Bailleur(k))
                    TotBail(k) += CDec(DocBaill(0))
                    totPartielbail += CDec(DocBaill(0))

                    drS(3 * k + 10) = AfficherMonnaie(DocBaill(0))
                    drS(3 * k + 11) = DocBaill(1)
                    drS(3 * k + 12) = IIf(TotLigNe <> 0, Math.Round((CDec(DocBaill(0)) * 100) / TotLigNe, 2).ToString & " %", "").ToString
                Next
                drS(3 * nbBaill + 10) = AfficherMonnaie((TotLigne - totPartielbail).ToString)
                drS(3 * nbBaill + 11) = IIf(TotLigne <> 0, Math.Round(((TotLigne - totPartielbail) * 100) / TotLigne, 2).ToString & " %", "").ToString
                'TotGap = PrixTot - totPartielbail
                dtBesoin.Rows.Add(drS)
            Next
            'Totaux ********************************************************************
            Dim drS0 = dtBesoin.NewRow()
            drS0(0) = "Tot"
            drS0(1) = ""
            drS0(2) = ""
            drS0(3) = ""
            drS0(4) = ""
            drS0(5) = "TOTAUX"
            drS0(6) = ""
            drS0(7) = ""
            drS0(8) = ""
            drS0(9) = AfficherMonnaie(PrixTot.ToString)

            Dim mtToBailLg As Decimal = 0
            For k As Integer = 0 To (nbBaill - 1)
                mtToBailLg += TotBail(k)
                drS0(3 * k + 10) = AfficherMonnaie(TotBail(k).ToString)
                drS0(3 * k + 11) = ""
                If (PrixTot <> 0) Then
                    drS0(3 * k + 12) = Math.Round((TotBail(k) * 100) / PrixTot, 2).ToString & " %"
                End If
            Next
            TotGap = PrixTot - mtToBailLg
            drS0(3 * nbBaill + 10) = AfficherMonnaie(TotGap.ToString)
            If (PrixTot <> 0) Then
                drS0(3 * nbBaill + 11) = Math.Round((TotGap * 100) / PrixTot, 2).ToString & " %"
            End If

            dtBesoin.Rows.Add(drS0)
            '***************************************************************************
            LblMontActiv.Text = " Montant Total Activité = " & AfficherMonnaie(PrixTot.ToString) & " "
            If (TotGap <> 0) Then
                LblGap.Visible = True
                LblGap.Text = " Montant Couvert = " & AfficherMonnaie(mtToBailLg.ToString) & "; GAP = " & AfficherMonnaie(TotGap.ToString) & " "
            Else
                LblGap.Visible = False
                LblGap.Text = ""
            End If


            GridBesoin.DataSource = dtBesoin

            ViewBesoin.Columns(0).Visible = False
            ViewBesoin.Columns(1).Visible = False
            ViewBesoin.Columns(2).Visible = False
            ViewBesoin.Columns(3).Width = 80
            ViewBesoin.Columns(4).Width = 80
            ViewBesoin.Columns(5).Width = 300
            ViewBesoin.Columns(6).Width = 50
            ViewBesoin.Columns(7).Width = 80
            ViewBesoin.Columns(8).Width = 120
            ViewBesoin.Columns(9).Width = 120
            For k As Integer = 0 To (nbBaill - 1)
                ViewBesoin.Columns(3 * k + 10).Width = 120
                ViewBesoin.Columns(3 * k + 11).Width = 140
                ViewBesoin.Columns(3 * k + 12).Width = 60

                ViewBesoin.Columns(3 * k + 10).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewBesoin.Columns(3 * k + 11).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                ViewBesoin.Columns(3 * k + 12).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            Next
            ViewBesoin.Columns(3 * nbBaill + 10).Width = 120
            ViewBesoin.Columns(3 * nbBaill + 11).Width = 80

            ViewBesoin.Columns(3 * nbBaill + 10).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewBesoin.Columns(3 * nbBaill + 11).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

            ViewBesoin.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewBesoin.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewBesoin.Columns(8).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewBesoin.Columns(9).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

            ViewBesoin.Columns(3).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewBesoin.Columns(2).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewBesoin.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewBesoin.Columns(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

            ViewBesoin.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

            ColorRowGrid(ViewBesoin, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewBesoin, "[CodeX]='Tot'", Color.DarkGray, "Times New Roman", 11, FontStyle.Bold, Color.White, True)

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try

    End Sub

    Private Function InfosBailleur(ByVal Besoin As String, ByVal Bailleur As String) As String()
        'récupération du montant du bailleur et de l'identifiant de la convention
        Dim Convention As String = "-"
        Dim MontBailleur As Decimal = 0
        query = "select MontantBailleur,CodeConvention from T_RepartitionParBailleur where RefBesoinPartition='" & Besoin & "' and CodeBailleur='" & Bailleur & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            Convention = rw(1).ToString
            MontBailleur = CDec(rw(0))
        Next
        Return {MontBailleur.ToString, Convention}

    End Function

    Private Sub FichesActivitesEtape2_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub BtAjoutBesoin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtAjoutBesoin.Click
        InitGbBesoin()
        GbNewBesoin.Visible = True
        If Not CmbCompte.Enabled Then CmbCompte.Enabled = True
    End Sub

    Private Sub RemplirCompte()
        'remplir la sous classe
        query = "select CODE_SC, LIBELLE_SC from T_COMP_SOUS_CLASSE order by CODE_SC"
        CmbCompte.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            CmbCompte.Properties.Items.Add(rw(0).ToString & " | " & MettreApost(rw(1).ToString))
        Next
    End Sub

    Private Sub RemplirUnite()
        'liste des unité de valeur

        query = "select LibelleCourtUnite,LibelleUnite from T_Unite order by LibelleUnite"
        CmbUnite.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            CmbUnite.Properties.Items.Add(MettreApost(rw(1).ToString) & " [" & rw(0).ToString & "]")
        Next
    End Sub

    Private Sub RemplirBailleur()
        'liste des bailleurs
        query = "select InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' order by InitialeBailleur"
        CmbBailleur.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            CmbBailleur.Properties.Items.Add(rw(0).ToString)
        Next
    End Sub

    Private Sub RemplirConvention()
        'liste des conventions
        Dim nbCv As Decimal = 0
        query = "select C.CodeConvention, B.CodeBailleur from T_Convention as C, T_Bailleur as B where B.CodeProjet='" & ProjetEnCours & "' and B.CodeBailleur=C.CodeBailleur and B.InitialeBailleur='" & CmbBailleur.Text & "' order by C.CodeConvention"
        CmbConv.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            TxtCodeBailleur.Text = rw(1).ToString
            CmbConv.Properties.Items.Add(rw(0).ToString)
            nbCv += 1
        Next

        If (nbCv = 1) Then
            CmbConv.Text = CmbConv.Properties.Items(0).ToString
        End If

    End Sub

    Private Sub GbNewBesoin_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GbNewBesoin.VisibleChanged

        If (GbNewBesoin.Visible = True) Then
            PnlNewBesoin.Enabled = False
            ContextMenuStrip1.Enabled = False
        Else
            PnlNewBesoin.Enabled = True
            ContextMenuStrip1.Enabled = True
        End If

    End Sub

    Private Sub CmbCompte_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCompte.SelectedValueChanged
        'paramétrage des types de compte
        query = "select TypeCompte from T_COMP_SOUS_CLASSE where CODE_SC='" & Trim(CmbCompte.Text.Split(" "c)(0)) & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            Dim typp As String = rw(0).ToString
            If (typp = "FR") Then
                TxtTypeCompte.Text = "Fournitures"
            ElseIf (typp = "TX") Then
                TxtTypeCompte.Text = "Travaux"
            ElseIf (typp = "CS") Then
                TxtTypeCompte.Text = "Consultants"
            ElseIf (typp = "SA") Then
                TxtTypeCompte.Text = "Services autres que les services de consultants"
            End If
        Next

    End Sub

    Private Sub TxtCodePanier_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCodePanier.TextChanged

        If (TxtCodePanier.Text <> "") Then
            TxtPrixUnit.Properties.ReadOnly = True
        Else
            TxtPrixUnit.Properties.ReadOnly = False
        End If

    End Sub

    Private Sub TxtTypeCompte_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtTypeCompte.TextChanged
        If (TxtTypeCompte.Text = "Fournitures" And ChkPrixRef.Checked = True And (RdPrixMin.Checked = False And RdPrixRef.Checked = False And RdPrixMax.Checked = False)) Then
            SuccesMsg("Sélectionner une valeur Min, Réf ou Max.")
        End If

    End Sub

    Private Sub TxtQte_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtQte.EditValueChanged
        CalculPrixTot()
    End Sub

    Private Sub CalculPrixTot()

        If (TxtQte.Text <> "" And TxtPrixUnit.Text <> "") Then
            TxtPrixTotal.EditValue = Math.Ceiling(CDec(TxtQte.EditValue.ToString.Replace(".", ",")) * CDec(TxtPrixUnit.EditValue.ToString.Replace(".", ",")))
        Else
            TxtPrixTotal.EditValue = 0
        End If

    End Sub

    Private Sub TxtPrixUnit_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPrixUnit.EditValueChanged
        CalculPrixTot()
    End Sub

    Private Sub TxtPrixTotal_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPrixTotal.EditValueChanged

        If (TxtPrixTotal.Text = "") Then
            TxtPrixTotal.EditValue = 0
            TxtMontTotal.EditValue = 0
        End If

        TxtMontTotal.EditValue = TxtPrixTotal.EditValue
        TxtPrixLettre.Text = MontantLettre(TxtPrixTotal.Text)

    End Sub

    Private Sub TxtMontTotal_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontTotal.EditValueChanged
        CalculAffectRest()
    End Sub

    Private Sub CalculAffectRest()

        If (TxtMontAffecte.Text <> "" And TxtMontTotal.Text <> "") Then
            TxtMontRestant.EditValue = CDec(TxtMontTotal.EditValue) - CDec(TxtMontAffecte.EditValue)
        ElseIf (TxtMontAffecte.Text = "") Then
            TxtMontRestant.EditValue = TxtMontTotal.EditValue
        End If

        If (TxtMontRestant.Text = "0" Or TxtMontRestant.Text = "") Then
            CmbBailleur.Enabled = False
            CmbConv.Enabled = False
            TxtMontBailleur.Enabled = False
        Else
            CmbBailleur.Enabled = True
            CmbConv.Enabled = True
            TxtMontBailleur.Enabled = True
        End If

    End Sub

    Private Sub TxtMontAffecte_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontAffecte.EditValueChanged
        CalculAffectRest()
    End Sub

    Private Sub CmbBailleur_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbBailleur.SelectedValueChanged

        CmbConv.Text = ""
        CmbConv.Properties.Items.Clear()
        RemplirConvention()

    End Sub

    Private Sub TxtMontBailleur_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontBailleur.EditValueChanged
        If TxtMontBailleur.Text <> "" Then
            If (TxtMontBailleur.EditValue > TxtMontRestant.EditValue) Then
                TxtMontBailleur.ForeColor = Color.Red
            Else
                TxtMontBailleur.ForeColor = CmbConv.ForeColor
                TxtPrct.EditValue = CalculPrct()
            End If
        End If
    End Sub

    Private Function CalculPrct() As Decimal

        Dim Prct As Decimal = 0
        If (TxtMontTotal.Text <> "" And TxtMontTotal.EditValue <> 0 And TxtMontBailleur.Text <> "") Then
            Prct = Math.Round((CDec(TxtMontBailleur.EditValue) * 100) / CDec(TxtMontTotal.EditValue), 2)
        End If
        Return Prct

    End Function

    Private Sub TxtMontBailleur_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtMontBailleur.KeyDown

        If (e.KeyCode = Keys.Enter And TxtMontBailleur.ForeColor <> Color.Red And CmbConv.Text <> "" And CmbBailleur.Text <> "" And TxtCodeBailleur.Text <> "") Then

            If boolmodif = True Then
            Else
                If (VerifAllocationEtConvention() = False) Then
                    Exit Sub
                End If
            End If

            RemplirRepart()
            EffacerTexBox4(PanelControl6)
            CmbBailleur.Focus()
        ElseIf (e.KeyCode = Keys.Enter And (TxtMontBailleur.ForeColor = Color.Red Or CmbConv.Text = "" Or CmbBailleur.Text = "")) Then
            FailMsg("Données incorrectes.")
        End If

    End Sub

    Private Function VerifAllocationEtConvention() As Boolean

        Dim VerifOk As Boolean = True

        Dim Convent As String = CmbConv.Text
        Dim MontConvent As Decimal = 0
        Dim MontRepartBudget As Decimal = 0
        Dim MontBudgetActu As Decimal = 0

        'Vérif Allocation ****************************************************
        Dim UnitBudget As String = ""
        query = "select UniteRepartitionBudget from T_ParamTechProjet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            UnitBudget = rw(0).ToString
        Next


        Dim Req0 As String = ""
        If (UnitBudget = "Composante") Then
            Req0 = "select B.MontantAlloue from T_Partition as P, T_Partition_Budget as B where B.CodePartition=P.CodePartition and LENGTH(P.LibelleCourt)='1' and P.LibelleCourt like '" & Mid(CmbActivite.Text, 1, 1) & "%' and P.CodeProjet='" & ProjetEnCours & "' and B.CodeConvention='" & Convent & "'"
        ElseIf (UnitBudget = "Sous composante") Then
            Req0 = "select B.MontantAlloue from T_Partition as P, T_Partition_Budget as B where B.CodePartition=P.CodePartition and LENGTH(P.LibelleCourt)='2' and P.LibelleCourt like '" & Mid(CmbActivite.Text, 1, 2) & "%' and P.CodeProjet='" & ProjetEnCours & "' and B.CodeConvention='" & Convent & "'"
        ElseIf (UnitBudget = "Activité") Then
            Req0 = "select B.MontantAlloue from T_Partition as P, T_Partition_Budget as B where B.CodePartition=P.CodePartition and LENGTH(P.LibelleCourt)='5' and P.LibelleCourt like '" & Mid(CmbActivite.Text, 1, 5) & "%' and P.CodeProjet='" & ProjetEnCours & "' and B.CodeConvention='" & Convent & "'"
        End If

        If (Req0 <> "") Then
            query = Req0
            dt = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                MontRepartBudget = CDec(rw(0))
            Next

        End If

        '**********************************************************************
        Dim ReqAttribue As String = ""
        If (UnitBudget = "Composante") Then
            ReqAttribue = "select R.MontantBailleur from T_Partition as P, T_BesoinPartition as B, T_RepartitionParBailleur as R where P.CodePartition=B.CodePartition and B.RefBesoinPartition=R.RefBesoinPartition and P.LibelleCourt like '" & Mid(CmbActivite.Text, 1, 1) & "%' and P.CodeProjet='" & ProjetEnCours & "' and R.CodeConvention='" & Convent & "'"
        ElseIf (UnitBudget = "Sous composante") Then
            ReqAttribue = "select R.MontantBailleur from T_Partition as P, T_BesoinPartition as B, T_RepartitionParBailleur as R where P.CodePartition=B.CodePartition and B.RefBesoinPartition=R.RefBesoinPartition and P.LibelleCourt like '" & Mid(CmbActivite.Text, 1, 2) & "%' and P.CodeProjet='" & ProjetEnCours & "' and R.CodeConvention='" & Convent & "'"
        ElseIf (UnitBudget = "Activité") Then
            ReqAttribue = "select R.MontantBailleur from T_Partition as P, T_BesoinPartition as B, T_RepartitionParBailleur as R where P.CodePartition=B.CodePartition and B.RefBesoinPartition=R.RefBesoinPartition and P.LibelleCourt like '" & Mid(CmbActivite.Text, 1, 5) & "%' and P.CodeProjet='" & ProjetEnCours & "' and R.CodeConvention='" & Convent & "'"
        Else
            ReqAttribue = "select MontantBailleur from T_RepartitionParBailleur where CodeConvention='" & Convent & "'"
        End If

        query = ReqAttribue
        dt = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            MontConvent = MontConvent + CDec(rw(0))
        Next

        MontConvent = MontConvent + CDec(TxtMontBailleur.EditValue)
        MontBudgetActu = MontConvent

        Dim MontOrigineConvent As Decimal = 0
        query = "select MontantConvention from T_Convention where CodeConvention='" & Convent & "'"
        dt = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            MontOrigineConvent = CDec(rw(0))
        Next

        'MsgBox("Montant Alloué=" & MontConvent.ToString & " Mont Total=" & MontRepartBudget.ToString, MsgBoxStyle.Information)

        If (MontConvent <> 0 And MontRepartBudget <> 0 And MontConvent > MontRepartBudget) Then

            Dim RepConv As MsgBoxResult = MsgBox("Dépassement du montant alloué à la " & UnitBudget & " par la convention [" & Convent & "]" & " d'un montant estimé à " & AfficherMonnaie((MontConvent - MontRepartBudget).ToString) & " FCFA" & vbNewLine & "Voulez-vous continuer l'enregistrement?", MsgBoxStyle.YesNo)
            If (RepConv = MsgBoxResult.No) Then
                TxtMontBailleur.Focus()
                VerifOk = False
            End If
        End If

        If (MontConvent <> 0 And MontOrigineConvent <> 0 And MontConvent > MontOrigineConvent) Then
            Dim RepConv As MsgBoxResult = MsgBox("Dépassement du montant de la convention [" & Convent & "]" & vbNewLine & "d'un montant estimé à " & AfficherMonnaie((MontConvent - MontOrigineConvent).ToString) & " FCFA" & vbNewLine & "Voulez-vous continuer l'enregistrement?", MsgBoxStyle.YesNo)
            If (RepConv = MsgBoxResult.No) Then
                TxtMontBailleur.Focus()
                VerifOk = False
            End If
        End If

        Return VerifOk

    End Function

    Private Function CodBailleur(ByVal initial As String) As String

        Dim Initiale As String = ""
        query = "select CodeBailleur InitialeBailleur from T_Bailleur where InitialeBailleur='" & initial & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            Initiale = rw(0).ToString
        Next
        Return Initiale

    End Function

    Private Sub RemplirRepart()

        Dim drS = dtBailleur.NewRow()
        Dim cpt As Decimal = 0
        drS(0) = cpt
        drS(1) = "0"
        drS(2) = CmbBailleur.Text
        drS(3) = CmbConv.Text
        drS(4) = TxtMontBailleur.Text.Replace(" ", "")
        drS(5) = TxtPrct.Text
        TxtMontAffecte.Text = AfficherMonnaie(CDec(TxtMontAffecte.Text) + CDec(TxtMontBailleur.Text.Replace(" ", "")))
        dtBailleur.Rows.Add(drS)
        GridRepartBailleur.DataSource = dtBailleur
        cpt = cpt + 1

        ViewRepartBailleur.Columns(0).Visible = False
        ViewRepartBailleur.Columns(1).Visible = False
        ViewRepartBailleur.Columns(2).Width = 84
        ViewRepartBailleur.Columns(3).Width = 172
        ViewRepartBailleur.Columns(4).Width = 145
        ViewRepartBailleur.Columns(5).Width = 48

        ViewRepartBailleur.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewRepartBailleur.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewRepartBailleur.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        ViewRepartBailleur.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewRepartBailleur, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub

    Private Sub BtEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click

        Dim MsgErr As String = ""
        If (CmbCompte.Text = "") And (CmbCompte.Visible = True) Then MsgErr = " - Numéro de compte"
        If (Cmbarticle.Text = "") And (Cmbarticle.Visible = True) Then MsgErr = " - Numéro de l'article"
        If (TxtLibelleBesoin.Text.Replace(" ", "") = "") Then MsgErr = MsgErr & vbNewLine & " - Libellé de la dépense"
        If (TxtQte.Text = "0" Or TxtQte.Text = "") Then MsgErr = MsgErr & vbNewLine & " - Quantité de la ressource"
        If (CmbUnite.Text = "") Then MsgErr = MsgErr & vbNewLine & " - Unité de la ressource"
        If (TxtPrixUnit.Text = "0" Or TxtPrixUnit.Text = "") Then MsgErr = MsgErr & vbNewLine & " - Prix Unitaire"

        If (MsgErr <> "") Then
            MsgBox("Un ou plusieurs champ(s) incorrect(s) :" & vbNewLine & MsgErr, MsgBoxStyle.Critical)
            Exit Sub
        Else

            If (Modification.Checked = True) Then
                ModifBesoin()
            Else
                EnregistrementBesoin()
            End If
            RepartitionBailleur()
            RemplirBesoin()
            If (Modification.Checked = True) Then
                GbNewBesoin.Visible = False
            End If

            EffacerTexBox4(GbNewBesoin)
            dtBailleur.Rows.Clear()
            TxtMontTotal.Text = "0"
            TxtMontAffecte.Text = "0"
            TxtMontRestant.Text = "0"
        End If


    End Sub

    Private Sub RepartitionBailleur()
        query = "DELETE from T_RepartitionParBailleur where RefBesoinPartition='" & TxtRefBesoin.Text & "'"
        ExecuteNonQuery(query)

        For i As Integer = 0 To ViewRepartBailleur.RowCount - 1
            Dim montbail As Double = CDbl(dtBailleur.Rows(i).Item(4).ToString)
            Dim DatSet = New DataSet
            query = "select * from T_RepartitionParBailleur"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_RepartitionParBailleur")
            Dim DatTable = DatSet.Tables("T_RepartitionParBailleur")
            Dim DatRow = DatSet.Tables("T_RepartitionParBailleur").NewRow()

            DatRow("RefBesoinPartition") = TxtRefBesoin.EditValue
            DatRow("CodeBailleur") = CodBailleur(dtBailleur.Rows(i).Item(2).ToString)
            DatRow("MontantBailleur") = montbail.ToString
            DatRow("CodeConvention") = dtBailleur.Rows(i).Item(3).ToString
            DatRow("RefMarche") = "0"

            DatSet.Tables("T_RepartitionParBailleur").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_RepartitionParBailleur")
            DatSet.Clear()
            BDQUIT(sqlconn)
        Next

    End Sub

    Private Sub EnregistrementBesoin()
        Dim numeroc As String = ""
        If ChkPrixRef.Checked Then
            numeroc = Trim(Cmbarticle.Text.Split("|"c)(0))
            numeroc = Mid(numeroc, 2, 6)
        Else
            Dim ca() As String
            ca = CmbCompte.Text.Split("   ")
            numeroc = ca(0).ToString
        End If

        Dim DatSet = New DataSet
        query = "select * from T_BesoinPartition"
        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)
        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd)
        DatAdapt.Fill(DatSet, "T_BesoinPartition")
        Dim DatTable = DatSet.Tables("T_BesoinPartition")
        Dim DatRow = DatSet.Tables("T_BesoinPartition").NewRow()

        DatRow("CodeRefPrix") = TxtCodePanier.Text
        DatRow("LibelleBesoin") = EnleverApost(TxtLibelleBesoin.Text)
        DatRow("CodePartition") = TxtCodeActiv.Text
        DatRow("NumeroComptable") = numeroc.ToString
        DatRow("QteNature") = TxtQte.EditValue.ToString.Replace(".", ",")
        DatRow("PUNature") = TxtPrixUnit.EditValue.ToString
        DatRow("CodeProjet") = ProjetEnCours
        DatRow("UniteBesoin") = EnleverApost(Trim(CmbUnite.Text.Split("["c)(0)))
        DatRow("TypeBesoin") = TxtTypeCompte.Text
        DatRow("RefMarche") = "0"
        DatRow("CodeNature") = "0"

        DatSet.Tables("T_BesoinPartition").Rows.Add(DatRow)
        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        DatAdapt.Update(DatSet, "T_BesoinPartition")

        DatSet.Clear()
        BDQUIT(sqlconn)
        query = "select Max(RefBesoinPartition) from T_BesoinPartition where CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            TxtRefBesoin.Text = rw(0).ToString
        Next
    End Sub

    Private Sub ModifBesoin()

        Dim numeroc As String = ""
        If ChkPrixRef.Checked Then
            numeroc = Trim(Cmbarticle.Text.Split("|"c)(0))
            numeroc = Mid(numeroc, 2, 6)
        Else
            Dim ca() As String
            ca = CmbCompte.Text.Split("   ")
            numeroc = ca(0).ToString
        End If

        Dim montbail As Double = CDbl(TxtPrixTotal.Text.Replace(" ", ""))

        query = "update T_BesoinPartition set NumeroComptable='" & numeroc.ToString & "',LibelleBesoin='" & EnleverApost(TxtLibelleBesoin.Text) & "',QteNature='" & TxtQte.EditValue.ToString.Replace(".", ",") & "',PUNature='" & TxtPrixUnit.EditValue.ToString & "',UniteBesoin='" & EnleverApost(Trim(CmbUnite.Text.Split("["c)(0))) & "',TypeBesoin='" & TxtTypeCompte.Text & "'  where RefBesoinPartition='" & TxtRefBesoin.Text & "' and CodeProjet='" & ProjetEnCours & "'"
        ExecuteNonQuery(query)

        query = "update t_repartitionparbailleur set MontantBailleur='" & montbail.ToString & "' where RefBesoinPartition='" & TxtRefBesoin.Text & "'"
        ExecuteNonQuery(query)
    End Sub

    Private Sub GridBesoin_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridBesoin.MouseUp

        If (ViewBesoin.RowCount > 0 And ViewBesoin.FocusedRowHandle <> ViewBesoin.RowCount - 1) Then
            If (GbNewBesoin.Visible = False) Then
                ContextMenuStrip1.Enabled = True
            Else
                ContextMenuStrip1.Enabled = False
            End If

            DrX = ViewBesoin.GetDataRow(ViewBesoin.FocusedRowHandle)
            Dim RefBes As String = DrX(1).ToString
            ColorRowGrid(ViewBesoin, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewBesoin, "[CodeX]='Tot'", Color.DarkGray, "Times New Roman", 11, FontStyle.Bold, Color.White, True)
            ColorRowGridAnal(ViewBesoin, "[Ref]='" & RefBes & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)
        Else
            ContextMenuStrip1.Enabled = False
        End If

    End Sub

    Private Sub ModifierLaLigne_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ModifierLaLigne.Click

        If (ViewBesoin.RowCount > 0) Then
            DrX = ViewBesoin.GetDataRow(ViewBesoin.FocusedRowHandle)
            Dim RefBes As String = DrX(1).ToString
            OuvrirModif(RefBes)
            boolmodif = True

        End If

    End Sub

    Private Sub OuvrirModif(ByVal Besoin As String)

        If (GbNewBesoin.Visible = True) Then
            SuccesMsg("Merci de fermer le formulaire d'enregistrement pour initialisation.")
        Else
            InitGbBesoin()
            GbNewBesoin.Visible = True
            Modification.Checked = True

            Dim mTotal As Decimal = 0

            query = "select B.NumeroComptable, C.Libelle_sc, B.CodeRefPrix, B.LibelleBesoin, B.QteNature, B.UniteBesoin, U.LibelleCourtUnite, B.PUNature from T_BesoinPartition as B, T_COMP_SOUS_CLASSE as C, T_Unite as U where B.NumeroComptable=C.CODE_SC and B.UniteBesoin=U.LibelleUnite and RefBesoinPartition='" & Besoin & "' and B.CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                Dim rw = dt.Rows(0)
                TxtRefBesoin.Text = Besoin
                CmbCompte.Text = rw(0).ToString & " | " & MettreApost(rw(1).ToString)
                TxtCodePanier.Text = rw(2).ToString
                TxtLibelleBesoin.Text = MettreApost(rw(3).ToString)
                TxtQte.EditValue = CDec(rw(4))
                CmbUnite.Text = MettreApost(rw(5).ToString) & " [" & rw(6).ToString & "]"
                TxtPrixUnit.EditValue = CDbl(rw(7))

                mTotal = Math.Ceiling(CDec(rw(4)) * CDec(rw(7)))

                Dim LockCompte As Boolean = False
                query = "SELECT COUNT(*) FROM t_comp_activite WHERE CODE_SC='" & rw(0).ToString & "' AND CodePartition='" & TxtCodeActiv.Text & "' AND Date_act BETWEEN '" & dateconvert(CDate(ExerciceComptable.Rows(0)("datedebut"))) & "' AND '" & dateconvert(CDate(ExerciceComptable.Rows(0)("datefin"))) & "'"
                Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
                If Val(dtVerif.Rows(0)(0)) > 0 Then
                    LockCompte = True
                End If

                query = "SELECT COUNT(*) FROM t_acteng A, t_marchesigne M WHERE M.RefMarche=A.RefMarche AND (STR_TO_DATE(DateMarche,'%d/%m/%Y') BETWEEN '" & dateconvert(CDate(ExerciceComptable.Rows(0)("datedebut"))) & "' AND '" & dateconvert(CDate(ExerciceComptable.Rows(0)("datefin"))) & "') AND A.LibelleCourt='" & Split(CmbActivite.Text, " - ")(0) & "' AND NumeroComptable='" & rw(0).ToString & "'"
                dtVerif = ExcecuteSelectQuery(query)
                If Val(dtVerif.Rows(0)(0)) > 0 Then
                    LockCompte = True
                End If

                If LockCompte Then
                    CmbCompte.Enabled = False
                Else
                    CmbCompte.Enabled = True
                End If
            End If


            'déclaration des columns
            dtBailleur.Columns.Clear()
            dtBailleur.Columns.Add("CodeX", Type.GetType("System.String"))
            dtBailleur.Columns.Add("Ref", Type.GetType("System.String"))
            dtBailleur.Columns.Add("Bailleur", Type.GetType("System.String"))
            dtBailleur.Columns.Add("Convention", Type.GetType("System.String"))
            dtBailleur.Columns.Add("Montant", Type.GetType("System.String"))
            dtBailleur.Columns.Add("%", Type.GetType("System.String"))
            dtBailleur.Rows.Clear()

            nbTp = 0
            query = "select CodeBailleur, MontantBailleur, CodeConvention from T_RepartitionParBailleur where RefBesoinPartition='" & Besoin & "'"
            dt = ExcecuteSelectQuery(query)
            Dim montAffecte As Decimal = 0
            For Each rw In dt.Rows
                Dim drS = dtBailleur.NewRow()
                drS(0) = nbTp
                drS(1) = "0"
                drS(2) = SearchTable2("InitialeBailleur", "T_Bailleur", "CodeBailleur", rw("CodeBailleur").ToString)
                drS(3) = rw("CodeConvention").ToString
                drS(4) = AfficherMonnaie(rw("MontantBailleur").ToString)
                If (CDec(mTotal) <> 0) Then
                    drS(5) = Math.Round((CDec(rw("MontantBailleur")) * 100) / mTotal, 2)
                End If
                montAffecte += CDec(rw("MontantBailleur").ToString)
                dtBailleur.Rows.Add(drS)
                nbTp += 1
                GridRepartBailleur.DataSource = dtBailleur

                ViewRepartBailleur.Columns(0).Visible = False
                ViewRepartBailleur.Columns(1).Visible = False
                ViewRepartBailleur.Columns(2).Width = 84
                ViewRepartBailleur.Columns(3).Width = 172
                ViewRepartBailleur.Columns(4).Width = 145
                ViewRepartBailleur.Columns(5).Width = 48

                ViewRepartBailleur.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                ViewRepartBailleur.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewRepartBailleur.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

                ViewRepartBailleur.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
                ColorRowGrid(ViewRepartBailleur, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            Next
            TxtMontAffecte.Text = AfficherMonnaie(montAffecte.ToString())
        End If

    End Sub

    Private Sub InitGbBesoin()

        CmbCompte.Text = ""
        TxtTypeCompte.Text = ""
        TxtCodePanier.Text = ""
        TxtLibelleBesoin.Text = ""
        TxtQte.EditValue = 0
        CmbUnite.Text = ""
        TxtPrixUnit.EditValue = 0
        TxtPrixTotal.EditValue = 0
        CmbBailleur.Text = ""
        TxtMontBailleur.EditValue = 0
        GridRepartBailleur.DataSource = Nothing
        GridRepartBailleur.Refresh()
        TxtRefBesoin.Text = ""
        TxtCodeBailleur.Text = ""
        Modification.Checked = False

    End Sub

    Private Sub Modification_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Modification.CheckedChanged
        If (Modification.Checked = True) Then
            PnlRefPrix.Enabled = False
        Else
            PnlRefPrix.Enabled = True
        End If
    End Sub

    Private Sub BtQuitter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        GbNewBesoin.Visible = False
        dtBailleur.Rows.Clear()
        EffacerTexBox4(GbNewBesoin)
        TxtMontTotal.Text = "0"
        TxtMontAffecte.Text = "0"
        TxtMontRestant.Text = "0"
    End Sub

    Private Sub GridRepartBailleur_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridRepartBailleur.DoubleClick

        If (ViewRepartBailleur.RowCount > 0) Then
            DrX = ViewRepartBailleur.GetDataRow(ViewRepartBailleur.FocusedRowHandle)
            Dim RepSuppBail As DialogResult = ConfirmMsg("Confirmez-vous la suppression de la part " & DrX(2).ToString & ".")
            If (RepSuppBail = DialogResult.Yes) Then

                TxtMontAffecte.Text = AfficherMonnaie(CDbl(TxtMontAffecte.Text) - CDbl(DrX(4).ToString))
                ViewRepartBailleur.GetDataRow(ViewRepartBailleur.FocusedRowHandle).Delete()

            End If

        End If

    End Sub

    Private Sub SupprimerLaLigne_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SupprimerLaLigne.Click

        If (ViewBesoin.RowCount > 0) Then
            DrX = ViewBesoin.GetDataRow(ViewBesoin.FocusedRowHandle)
            Dim RefBes As String = DrX(1).ToString
            query = "SELECT COUNT(*) FROM t_comp_activite WHERE CODE_SC='" & DrX("Compte") & "' AND CodePartition='" & TxtCodeActiv.Text & "' AND Date_act BETWEEN '" & dateconvert(CDate(ExerciceComptable.Rows(0)("datedebut"))) & "' AND '" & dateconvert(CDate(ExerciceComptable.Rows(0)("datefin"))) & "'"
            Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
            If Val(dtVerif.Rows(0)(0)) > 0 Then
                FailMsg("Impossible de supprimer ce compte car il est utilisé à la comptabilité.")
                Exit Sub
            End If

            query = "SELECT COUNT(*) FROM t_acteng A, t_marchesigne M WHERE M.RefMarche=A.RefMarche AND (STR_TO_DATE(DateMarche,'%d/%m/%Y') BETWEEN '" & dateconvert(CDate(ExerciceComptable.Rows(0)("datedebut"))) & "' AND '" & dateconvert(CDate(ExerciceComptable.Rows(0)("datefin"))) & "') AND A.LibelleCourt='" & Split(CmbActivite.Text, " - ")(0) & "' AND NumeroComptable='" & DrX("Compte") & "'"
            dtVerif = ExcecuteSelectQuery(query)
            If Val(dtVerif.Rows(0)(0)) > 0 Then
                FailMsg("Impossible de supprimer ce compte car il est utilisé dans les engagements.")
                Exit Sub
            End If

            Dim Rep0 As DialogResult = ConfirmMsg("Confirmez-vous la suppression de " & DrX(5).ToString & vbNewLine & "Montant : " & DrX(9).ToString & ".")
            If (Rep0 = DialogResult.Yes) Then

                query = "delete from T_BesoinPartition where RefBesoinPartition='" & RefBes & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

                query = "delete from t_repartitionparbailleur where RefBesoinPartition='" & RefBes & "'"
                ExecuteNonQuery(query)

                dtBailleur.Rows.Clear()
                RemplirBesoin()
                TxtMontTotal.Text = "0"
                TxtMontAffecte.Text = "0"
                TxtMontRestant.Text = "0"

            End If

        End If

    End Sub

    Private Sub ChkPrixRef_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkPrixRef.CheckedChanged

        If (ChkPrixRef.Checked = True) Then
            CmbCompte.Visible = False
            Cmbarticle.Visible = True
            Cmbarticle.Text = ""
            Cmbarticle.Focus()
            ChargerArticle()
            LabelControl11.Text = "Article"
        Else
            PnlChoixPrix.Enabled = False
            CmbCompte.Visible = True
            CmbCompte.Focus()
            Cmbarticle.Visible = False
            RemplirCompte()
            LabelControl11.Text = "Compte"
        End If

    End Sub

    Private Sub RdPrixMin_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdPrixMin.CheckedChanged
        VerifRadio()

        Dim ca() As String
        ca = Cmbarticle.Text.Split("   ")
        query = "select STAND_PLANCHER from RP_PRIX_STANDARD where ART_ID='" & ca(0).ToString & "'"
        TxtPrixUnit.Text = ""
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            TxtPrixUnit.Text = rw(0).ToString
        Next
    End Sub

    Private Sub RdPrixMax_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdPrixMax.CheckedChanged
        VerifRadio()

        Dim ca() As String
        ca = Cmbarticle.Text.Split("   ")
        query = "select STAND_PLAFOND from RP_PRIX_STANDARD where ART_ID='" & ca(0).ToString & "'"
        TxtPrixUnit.Text = ""
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            TxtPrixUnit.Text = rw(0).ToString
        Next
    End Sub

    Private Sub RdPrixRef_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdPrixRef.CheckedChanged
        VerifRadio()

        Dim ca() As String
        ca = Cmbarticle.Text.Split("   ")
        query = "select STAND_REFERENCE from RP_PRIX_STANDARD where ART_ID='" & ca(0).ToString & "'"
        TxtPrixUnit.Text = ""
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            TxtPrixUnit.Text = rw(0).ToString
        Next
    End Sub

    Private Sub VerifRadio()

        If (RdPrixMax.Checked = True Or RdPrixMin.Checked = True Or RdPrixRef.Checked = True) Then
            CmbCompte.Enabled = True
        ElseIf (ChkPrixRef.Checked = True) Then
            CmbCompte.Enabled = False
        End If

    End Sub

    Private Sub Cmbarticle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmbarticle.SelectedIndexChanged
        Dim ca() As String
        ca = Cmbarticle.Text.Split("   ")

        PnlChoixPrix.Enabled = True

        'paramétrage des types de compte
        query = "select TypeCompte from T_COMP_SOUS_CLASSE where CODE_SC='" & Mid(ca(0).ToString, 2, 6) & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            Dim typp As String = rw(0).ToString
            If (typp = "FR") Then
                TxtTypeCompte.Text = "Fournitures"
            ElseIf (typp = "TX") Then
                TxtTypeCompte.Text = "Travaux"
            ElseIf (typp = "CS") Then
                TxtTypeCompte.Text = "Consultants"
            ElseIf (typp = "SA") Then
                TxtTypeCompte.Text = "Services autres que les services de consultants"
            End If
        Next
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If ViewBesoin.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub
End Class