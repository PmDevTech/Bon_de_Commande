Imports MySql.Data.MySqlClient

Public Class FichesActivitesEtape3
    'initialisation des variables
    Dim dtActivite = New DataTable()
    Dim DrX As DataRow

    Private Sub FichesActivitesEtape3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        'date
        Datedebub.Text = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
        DateFin.Text = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")

        Datedebub.Properties.MinValue = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
        Datedebub.Properties.MaxValue = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")

        DateFin.Properties.MinValue = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
        DateFin.Properties.MaxValue = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")

        'appel des fonctions au chargement de la page
        Dim Nbre As Decimal = 0
        query = "select COUNT(*) from T_Partition where CodeProjet='" & ProjetEnCours & "'"
        Nbre = Val(ExecuteScallar(query))

        If Nbre = 0 Then
        Else
            RemplirActivite()
            RemplirCompo()
            ChargerIndicateur()
            ChargerMoyenVerif()
            ChargerRespo()
            ChargerZone()
        End If

    End Sub

    Private Sub RemplirCompo()
        'remplissage
        query = "select LibelleCourt, LibellePartition from T_Partition where LENGTH(LibelleCourt)=1 and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
        CmbCompo.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            CmbCompo.Properties.Items.Add(rw(0).ToString & " : " & MettreApost(rw(1).ToString))
        Next
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

    Private Sub ChargerZone()
        'remplissage de la zone 
        CmbLocalisation.Properties.Items.Clear()
        query = "select AbregeZone,LibelleZone,CodeZone from T_ZoneGeo where CodeZoneMere='0' order by LibelleZone"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            CmbLocalisation.Properties.Items.Add(MettreApost(rw(1).ToString))

            query = "select AbregeZone,LibelleZone, CodeZone from T_ZoneGeo where CodeZoneMere='" & rw(2).ToString & "' and NiveauStr=2 order by LibelleZone"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw0 In dt0.Rows
                CmbLocalisation.Properties.Items.Add(MettreApost(rw0(1).ToString))

                query = "select AbregeZone,LibelleZone, CodeZone from T_ZoneGeo where CodeZoneMere='" & rw0(2).ToString & "' and NiveauStr=3 order by LibelleZone"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw1 In dt1.Rows
                    CmbLocalisation.Properties.Items.Add(MettreApost(rw1(1).ToString))

                    query = "select AbregeZone,LibelleZone from T_ZoneGeo where CodeZoneMere='" & rw1(2).ToString & "' and NiveauStr=4 order by LibelleZone"
                    Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw2 In dt2.Rows
                        CmbLocalisation.Properties.Items.Add(MettreApost(rw2(1).ToString))
                    Next
                Next
            Next
        Next
    End Sub

    Private Sub CmbCompo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCompo.SelectedValueChanged
        RemplirSousCompo()
        RemplirActivite()
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

        RemplirActivite()
    End Sub

    Private Sub RemplirActivite()
        Try
            'remplir le tableau des partitions
            dtActivite.Columns.Clear()

            dtActivite.Columns.Add("CodeX", Type.GetType("System.String"))
            dtActivite.Columns.Add("Partition", Type.GetType("System.String"))
            dtActivite.Columns.Add("Code", Type.GetType("System.String"))
            dtActivite.Columns.Add("Libellé Activité", Type.GetType("System.String"))
            dtActivite.Columns.Add("Indicateur de performance", Type.GetType("System.String"))
            dtActivite.Columns.Add("Fréquence de collecte", Type.GetType("System.String"))
            dtActivite.Columns.Add("Moyen de vérification", Type.GetType("System.String"))
            dtActivite.Columns.Add("Résultats attendus", Type.GetType("System.String"))
            dtActivite.Columns.Add("Responsable Activité", Type.GetType("System.String"))
            dtActivite.Columns.Add("Elaborateur de la Fiche", Type.GetType("System.String"))
            dtActivite.Columns.Add("Localisation Activité", Type.GetType("System.String"))
            dtActivite.Columns.Add("Alertes", Type.GetType("System.Boolean"))
            dtActivite.Rows.Clear()


            Dim codeAct() As String
            codeAct = CmbSousCompo.Text.Split(" : ")

            Dim clause As String = ""

            'conversion de la date
            Dim str(3) As String
            str = Datedebub.Text.Split("/")
            Dim tempdt As String = String.Empty
            For j As Integer = 2 To 0 Step -1
                tempdt += str(j) & "-"
            Next
            tempdt = tempdt.Substring(0, 10)

            Dim str1(3) As String
            str1 = DateFin.Text.Split("/")
            Dim tempdt1 As String = String.Empty
            For j As Integer = 2 To 0 Step -1
                tempdt1 += str1(j) & "-"
            Next
            tempdt1 = tempdt1.Substring(0, 10)

            'Requete Date
            If DateTime.Compare(tempdt1, tempdt) >= 0 Then
                clause = " AND dateDebutPartition >='" & tempdt & "' AND dateFinPartition <='" & tempdt1 & "' order by LibelleCourt"
            Else
                SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
            End If

            Dim NbTotal As Decimal = 0

            query = "select CodePartition, LibelleCourt, LibellePartition, ResAttendu, CodeZone from T_Partition where CodeClassePartition='5'" & IIf(CmbSousCompo.Text <> "", " and CodePartitionMere='" & TxtCodeMere.Text & "'", IIf(CmbCompo.Text <> "", " and LibelleCourt like '" & Mid(CmbCompo.Text, 1, 1) & "%'", "").ToString).ToString & " and CodeProjet='" & ProjetEnCours & "'" & clause
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                NbTotal += 1
                Dim InfosTamp As String() = Infos_Indic(rw(0).ToString)
                Dim drS = dtActivite.NewRow()

                drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                drS(1) = rw(0).ToString
                drS(2) = rw(1).ToString
                drS(3) = MettreApost(rw(2).ToString)
                drS(4) = InfosTamp(0)
                drS(5) = InfosTamp(1)
                drS(6) = InfosTamp(2)
                drS(7) = MettreApost(rw(3).ToString)
                drS(8) = NomDu("Responsable", rw(0).ToString)
                drS(9) = NomDu("Elaborateur", rw(0).ToString)
                drS(10) = LocalisationDe(rw(4).ToString).Replace(" - ", "")
                drS(11) = IIf(InfosTamp(3).ToUpper = "OUI", True, False)

                dtActivite.Rows.Add(drS)
            Next
            GridActivite.DataSource = dtActivite
            ViewActivite.Columns(0).Visible = False
            ViewActivite.Columns(1).Visible = False
            ViewActivite.Columns(2).Width = 60
            ViewActivite.Columns(3).Width = 350
            ViewActivite.Columns(4).Width = 200
            ViewActivite.Columns(5).Width = 200
            ViewActivite.Columns(6).Width = 200
            ViewActivite.Columns(7).Width = 200
            ViewActivite.Columns(8).Width = 200
            ViewActivite.Columns(9).Width = 200
            ViewActivite.Columns(10).Width = 200
            ViewActivite.Columns(11).Width = 50
            ViewActivite.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewActivite.Columns(2).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewActivite.Columns(3).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewActivite.Columns(11).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
            ViewActivite.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
            ColorRowGrid(ViewActivite, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            NbTotActivite = NbTotal
            LblNbActivite.Text = "     " & NbTotal & " Activité" & IIf(NbTotal > 1, "s", "").ToString

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub ChargerIndicateur()

        CmbIndic.Properties.Items.Clear()
        query = "select LibelleIndicateur, CodeIndicateur from T_Indicateur order by libelleIndicateur"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            Dim Indic As String = rw(1).ToString
            While Len(Indic) < 4
                Indic = "0" & Indic
            End While

            CmbIndic.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next

    End Sub

    Private Sub ChargerMoyenVerif()
        'remplir les moyens de vérification
        CmbMoyenVerif.Properties.Items.Clear()
        query = "select LibelleMoyenVerif from T_MoyenVerif order by LibelleMoyenVerif"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            CmbMoyenVerif.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next
    End Sub

    Private Sub ChargerRespo()

        'rechercher les informations de l'opérateur connecté
        CmbRespo.Properties.Items.Clear()
        CmbElabo.Properties.Items.Clear()
        query = "select EMP_ID, EMP_NOM, EMP_PRENOMS from t_grh_employe where PROJ_ID='" & ProjetEnCours & "' ORDER BY EMP_NOM, EMP_PRENOMS ASC"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            Dim codep As String = rw(0).ToString
            While (Len(codep) < 4)
                codep = "0" & codep
            End While
            CmbRespo.Properties.Items.Add(MettreApost(rw("EMP_NOM").ToString & " " & rw("EMP_PRENOMS").ToString) & " | " & codep)
            CmbElabo.Properties.Items.Add(MettreApost(rw("EMP_NOM").ToString & " " & rw("EMP_PRENOMS").ToString) & " | " & codep)
        Next

    End Sub

    Private Function NomDu(ByVal Titre As String, ByVal Kod As String) As String
        'rechercher les informations de l'opérateur connecté
        Dim NomOp As String = ""
        query = "select E.EMP_ID, EMP_NOM, EMP_PRENOMS from T_OperateurPartition as P, t_grh_employe as E where P.EMP_ID=E.EMP_ID and P.CodePartition='" & Kod & "' and P.TitreOpPart='" & Titre & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            Dim codep As String = rw(0).ToString
            While (Len(codep) < 4)
                codep = "0" & codep
            End While
            NomOp = MettreApost(rw(1).ToString & " " & rw(2).ToString) & " | " & codep
        Next
        Return NomOp

    End Function

    Private Function Infos_Indic(ByVal Partit As String) As String()
        'recherche des indicateurs de performance
        Dim Indic As String = ""
        Dim FreqC As String = ""
        Dim MoyVerif As String = ""
        Dim Alertes As String = ""
        query = "select I.LibelleIndicateur, P.FrequenceCollecte, P.AlertesCollecte, P.MoyenVerifCollecte, I.CodeIndicateur from T_Indicateur as I, T_IndicateurPartition as P where P.CodeIndicateur=I.CodeIndicateur and P.CodePartition='" & Partit & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            Indic = MettreApost(rw(0).ToString)
            FreqC = MettreApost(rw(1).ToString)
            MoyVerif = MettreApost(rw(3).ToString)
            Alertes = rw(2).ToString
        Next
        Return {Indic, FreqC, MoyVerif, Alertes}

    End Function

    Private Function LocalisationDe(ByVal Zone As String) As String
        'recherche de la localité ou se trouve le projet
        Dim LocPart As String = ""
        query = "select LibelleZone, CodeZoneMere from T_ZoneGeo where CodeZone='" & Zone & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            LocPart = IIf(rw(1).ToString <> "0", "", "").ToString & MettreApost(rw(0).ToString)
        Next
        Return LocPart

    End Function

    Private Function IndComplet(ByVal Libelle As String) As String
        'indicateur de performance
        Dim Indc As String = ""
        query = "select LibelleIndicateur, CodeIndicateur from T_Indicateur where LibelleIndicateur='" & EnleverApost(Libelle) & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            Indc = rw(1).ToString
            While Len(Indc) < 4
                Indc = "0" & Indc
            End While
            Indc = "(" & Indc & ") " & MettreApost(rw(0).ToString)
        Next

        Return Indc

    End Function

    Private Function CodeIndic(ByVal Libelle As String) As String

        'indicateur de performance
        Dim Indc As String = ""
        query = "select LibelleIndicateur, CodeIndicateur from T_Indicateur where LibelleIndicateur='" & EnleverApost(Libelle) & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            Indc = rw(1).ToString
        Next

        Return Indc

    End Function


    Private Sub GridActivite_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridActivite.DoubleClick

        'affectation des éléments de la partition au double-clik d'une valeur dans le tableau
        If (ViewActivite.RowCount > 0) Then
            GbProprietes.Visible = True
            DrX = ViewActivite.GetDataRow(ViewActivite.FocusedRowHandle)

            ColorRowGrid(ViewActivite, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewActivite, "[Partition]='" & DrX(1).ToString & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)

            TxtCodePartition.Text = DrX(1).ToString
            TxtCode.Text = DrX(2).ToString
            TxtLibelleActiv.Text = DrX(3).ToString
            CmbIndic.Text = DrX(4).ToString
            TxtFreqCollecte.Text = DrX(5).ToString
            CmbMoyenVerif.Text = DrX(6).ToString
            TxtResAttendu.Text = DrX(7).ToString
            CmbRespo.Text = DrX(8).ToString
            CmbElabo.Text = DrX(9).ToString

            Dim Czone As String = ""

            query = "select CodeZone from T_Partition where CodePartition='" & DrX(1).ToString & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                Czone = rw(0).ToString
            Next

            TxtCodeZone.Text = Czone
            CmbLocalisation.Text = LocalisationDe(Czone)
            ChkAlertes.Checked = DrX(11)

        End If

    End Sub

    Private Sub BtQuitter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        GbProprietes.Visible = False
    End Sub

    Private Sub FichesActivitesEtape3_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub BtAjoutIndic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjoutIndic.Click
        IndicateursEtUnites.XtraTabControl1.SelectedTabPage = IndicateursEtUnites.TabPageIndicateur
        Dialog_form(IndicateursEtUnites)

        ChargerIndicateur()
    End Sub

    Private Sub BtAjoutMoyVerif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjoutMoyVerif.Click
        IndicateursEtUnites.XtraTabControl1.SelectedTabPage = IndicateursEtUnites.TabPageMoyVerif
        Dialog_form(IndicateursEtUnites)

        ChargerMoyenVerif()
    End Sub

    Private Sub BtFreqCollecte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtFreqCollecte.Click

        Dim RepFreq As String = ChoixFrequenceCollecte.ShowDialog()
        If (RepFreq = DialogResult.OK) Then
            TxtFreqCollecte.Text = FrequenceDialogResult
        End If

    End Sub


    Private Sub BtAjoutZone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjoutZone.Click
        Dialog_form(Zonegeo)

        ChargerZone()
    End Sub

    Private Sub GbProprietes_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GbProprietes.VisibleChanged

        If (GbProprietes.Visible = True) Then
            PnlCompo.Enabled = False
        Else
            PnlCompo.Enabled = True
        End If

    End Sub

    Private Sub BtEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click
        If CmbIndic.SelectedIndex = -1 Then
            SuccesMsg("Veuillez sélectionner un indicateur dans liste.")
            CmbIndic.Select()
            Exit Sub
        End If

        If CmbMoyenVerif.SelectedIndex = -1 Then
            SuccesMsg("Veuillez choisir un moyen de vérification dans liste.")
            CmbMoyenVerif.Select()
            Exit Sub
        End If

        If CmbRespo.SelectedIndex = -1 Then
            SuccesMsg("Veuillez choisir un responsable dans la liste.")
            CmbRespo.Select()
            Exit Sub
        End If

        If CmbLocalisation.SelectedIndex = -1 Then
            SuccesMsg("Veuillez choisir une localisation dans liste.")
            CmbLocalisation.Select()
            Exit Sub
        End If

        If CmbElabo.SelectedIndex = -1 Then
            SuccesMsg("Veuillez choisir l'élaborateur de la fiche dans liste.")
            CmbElabo.Select()
            Exit Sub
        End If
        'Mise à jour IndicateurPartition **********************************
        query = "DELETE from T_IndicateurPartition where CodePartition='" & TxtCodePartition.Text & "'"
        ExecuteNonQuery(query)


        If (CmbIndic.Text <> "") Then
            Dim DatSet = New DataSet
            query = "select * from T_IndicateurPartition"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_IndicateurPartition")
            Dim DatTable = DatSet.Tables("T_IndicateurPartition")
            Dim DatRow = DatSet.Tables("T_IndicateurPartition").NewRow()
            DatRow("CodePartition") = TxtCodePartition.Text
            DatRow("CodeIndicateur") = CodeIndic(CmbIndic.Text)
            DatRow("FrequenceCollecte") = EnleverApost(TxtFreqCollecte.Text)
            DatRow("AlertesCollecte") = If(ChkAlertes.Checked = True, "OUI", "NON")
            DatRow("MoyenVerifCollecte") = EnleverApost(CmbMoyenVerif.Text)

            DatSet.Tables("T_IndicateurPartition").Rows.Add(DatRow) 'ajout d'une nouvelle ligne 
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt) 'execution de l'enregistrement
            DatAdapt.Update(DatSet, "T_IndicateurPartition")

            DatSet.Clear()
            BDQUIT(sqlconn)
        End If

        '******************************************************************

        'Mise à jour Partition ********************************************

        query = "update T_Partition set ResAttendu='" & EnleverApost(TxtResAttendu.Text) & "',CodeZone='" & IIf(TxtCodeZone.Text <> "", TxtCodeZone.Text, "0").ToString & "',DateModif='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "',Operateur='" & CodeUtilisateur & "' where CodePartition='" & TxtCodePartition.Text & "' and CodeProjet='" & ProjetEnCours & "'"
        ExecuteNonQuery(query)

        '******************************************************************

        'Mise à jour OperateurPartition ***********************************
        Dim Respo As String = ""
        Dim Elabo As String = ""

        Dim cr() As String
        cr = CmbRespo.Text.Split("|")

        Dim ce() As String
        ce = CmbElabo.Text.Split("|")

        If (CmbRespo.Text <> "") Then Respo = Mid(cr(1), 3)
        If (CmbElabo.Text <> "") Then Elabo = Mid(ce(1), 3)

        MiseAJourRespoElaborat(Respo, "Responsable")
        MiseAJourRespoElaborat(Elabo, "Elaborateur")
        '******************************************************************

        RemplirActivite()
        EffacerTexBox(GbProprietes)

    End Sub

    Private Sub MiseAJourRespoElaborat(ByVal Opr As String, ByVal Titre As String)

        Try
            query = "DELETE from T_OperateurPartition where CodePartition='" & TxtCodePartition.Text & "' and TitreOpPart='" & Titre & "'"
            ExecuteNonQuery(query)

            If (Opr <> "") Then

                Dim DatSet = New DataSet
                query = "select * from T_OperateurPartition"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "T_OperateurPartition")
                Dim DatTable = DatSet.Tables("T_OperateurPartition")
                Dim DatRow = DatSet.Tables("T_OperateurPartition").NewRow()
                DatRow("CodePartition") = TxtCodePartition.Text
                DatRow("EMP_ID") = Val(Opr)
                DatRow("TitreOpPart") = Titre
                DatRow("DateSaisie") = Now.ToShortDateString & " " & Now.ToLongTimeString
                DatRow("DateModif") = Now.ToShortDateString & " " & Now.ToLongTimeString
                DatRow("Operateur") = CodeUtilisateur
                DatSet.Tables("T_OperateurPartition").Rows.Add(DatRow)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "T_OperateurPartition")
                
                DatSet.Clear()
                BDQUIT(sqlconn)

            End If

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub CmbLocalisation_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbLocalisation.SelectedValueChanged
        TxtCodeZone.Text = ""
        query = "select CodeZone from T_ZoneGeo where LibelleZone='" & EnleverApost(CmbLocalisation.Text.Replace(" - ", "")) & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            TxtCodeZone.Text = rw(0).ToString
        Next
    End Sub

    Private Sub BtActualiser_Click(sender As System.Object, e As System.EventArgs) Handles BtActualiser.Click
        RemplirActivite()
    End Sub

    Private Sub Datedebub_EditValueChanged(sender As Object, e As System.EventArgs) Handles Datedebub.EditValueChanged
        If Datedebub.Text <> "" And DateFin.Text <> "" Then
            RemplirActivite()
        End If
    End Sub

    Private Sub DateFin_EditValueChanged(sender As Object, e As System.EventArgs) Handles DateFin.EditValueChanged
        If Datedebub.Text <> "" And DateFin.Text <> "" Then
            RemplirActivite()
        End If
    End Sub

End Class