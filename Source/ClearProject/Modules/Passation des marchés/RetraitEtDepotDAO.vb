Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports MySql.Data.MySqlClient
Imports ClearProject.PassationMarche
Public Class RetraitEtDepotDAO

    Dim dr As DataRow
    Dim dt = New DataTable()
    Dim TpsDepot As String = ""
    Dim ChargeGridEnCours As Boolean = False
    Dim CodeSoumis As String = ""
    Dim SoumissionEnCours As Boolean = False
    Dim Modification As Boolean = False

    Private Sub RetraitEtDepotDAO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RemplirCmbNumDAO()
        Timer1.Interval = 1000 'Timer1_Tick sera déclenché toutes les secondes.
        Timer1.Start() 'On démarre le Timer
        BtModDepot.Enabled = False
        BtModRetrait.Enabled = False
        ItemCmbPays()

    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If (TpsDepot <> "") Then
            If (DateTime.Compare(CDate(TpsDepot), Now) >= 0) Then
                Dim InterJours As Decimal = CDec(DateDiff(DateInterval.DayOfYear, Now, CDate(TpsDepot)))
                Dim InterHeures As Decimal = CDec(DateDiff(DateInterval.Hour, Now, CDate(TpsDepot)))
                Dim InterMinutes As Decimal = CDec(DateDiff(DateInterval.Minute, Now, CDate(TpsDepot)))
                Dim InterSecondes As Decimal = CDec(DateDiff(DateInterval.Second, Now, CDate(TpsDepot)))

                Dim InterText As String = ""
                If (InterJours > 0) Then
                    InterText = InterJours.ToString & " J"
                End If
                If (InterHeures > 0) Then
                    If (InterText <> "") Then InterText = InterText & "   :   "
                    InterText = InterText & (InterHeures - (24 * InterJours)).ToString & " H"
                End If
                If (InterMinutes > 0) Then
                    If (InterText <> "") Then InterText = InterText & "   :   "
                    InterText = InterText & (InterMinutes - 60 * ((24 * InterJours) + (InterHeures - (24 * InterJours)))).ToString & " M"
                End If
                If (InterSecondes > 0) Then
                    If (InterText <> "") Then InterText = InterText & "   :   "
                    InterText = InterText & (InterSecondes - 60 * InterMinutes).ToString & " S"
                End If

                If (InterSecondes <= 10 And InterMinutes = 0 And InterHeures = 0 And InterJours = 0) Then
                    My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
                    BtAfficheInfos.ForeColor = Color.Red
                Else
                    BtAfficheInfos.ForeColor = Color.Black
                End If

                If (InterText = "") Then
                    My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
                    BtAfficheInfos.Text = "FERMETURE DEPOT EN COURS"
                Else
                    InterText = "Clôture de dépôt des offres dans " & vbNewLine & InterText
                    BtAfficheInfos.Text = InterText.ToString
                End If

            Else
                BtAfficheInfos.ForeColor = Color.Black
                BtAfficheInfos.Text = "Délai expiré!"
                InitFormulaire()
                GbSoumissionnaire.Enabled = False
                'GbRecapSoumis.Enabled = False

            End If
        Else
            If (CmbNumDAO.Text <> "") Then
                BtAfficheInfos.Text = "Dépot des offres clôturé!"
            End If
        End If

    End Sub

    Private Sub RemplirCmbNumDAO()
        query = "select NumeroDAO from T_DAO where CodeProjet='" & ProjetEnCours & "' order by NumeroDAO"
        CmbNumDAO.Properties.Items.Clear()
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbNumDAO.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next
    End Sub

    Private Sub CmbNumDAO_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbNumDAO.SelectedValueChanged
        TpsDepot = ""
        BtAfficheInfos.Text = ""

        If (CmbNumDAO.Text <> "") Then
            query = "select IntituleDAO,MethodePDM,TypeMarche,PrixDAO,DateLimiteRemise,DateFinOuverture, DateReport from T_DAO where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtMethode.Text = rw(1).ToString
                TxtTypeMarche.Text = rw(2).ToString
                TxtLibMarche.Text = MettreApost(rw(0).ToString)
                TxtPrixDAO.Text = AfficherMonnaie(CInt(rw(3).ToString))

                If (rw("DateLimiteRemise").ToString <> "" And rw("DateFinOuverture").ToString = "") Then
                    If rw("DateReport").ToString <> "" Then
                        TpsDepot = rw("DateReport").ToString
                    Else
                        TpsDepot = rw("DateLimiteRemise").ToString
                    End If
                    GbSoumissionnaire.Enabled = True
                    'GbRecapSoumis.Enabled = True
                    TxtNomSoumis.Focus()
                Else
                    GbSoumissionnaire.Enabled = False
                    'GbRecapSoumis.Enabled = False
                End If
            Next
            RemplirGridSoumis()
        End If
    End Sub

    Private Sub TxtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtSearch.KeyPress
        If (SoumissionEnCours = False) Then
            If (TxtSearch.Text = "" Or TxtSearch.EditValue = "") Then
                If (ChargeGridEnCours = False) Then
                    RemplirGridSoumis()
                End If
            End If
        End If
    End Sub

    Private Sub TxtSearch_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtSearch.EditValueChanged
        If (SoumissionEnCours = False) Then
            If (ChargeGridEnCours = False) Then
                RemplirGridSoumis(TxtSearch.Text)
            End If
        End If
    End Sub

    Private Sub RemplirGridSoumis(Optional ByVal TexteRecherche As String = "")
        ChargeGridEnCours = True


        If CmbNumDAO.Text <> "" Then

            If (TxtSearch.Focused = False) Then
                TxtSearch.Text = ""
                TxtSearch.EditValue = ""
            End If

            dt.Columns.Clear()

            dt.Columns.Add("CodeFournis", Type.GetType("System.String"))
            dt.Columns.Add("Nom", Type.GetType("System.String"))
            dt.Columns.Add("Pays", Type.GetType("System.String"))
            dt.Columns.Add("Contact", Type.GetType("System.String"))
            dt.Columns.Add("Nom représentant retrait", Type.GetType("System.String"))
            dt.Columns.Add("Nom représentant dépot", Type.GetType("System.String"))
            dt.Columns.Add("email", Type.GetType("System.String"))

            Dim nbSoumis As Decimal = 0
            query = "select NomFournis,PaysFournis,TelFournis,FaxFournis,CelFournis,NomAch,NomDep,CodeFournis,MailFournis from T_Fournisseur where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet='" & ProjetEnCours & "' order by NomFournis"
            dt.Rows.Clear()
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                nbSoumis = nbSoumis + 1
                Dim drS = dt.NewRow()
                drS(0) = rw(7).ToString
                drS(1) = MettreApost(rw(0).ToString)
                drS(2) = MettreApost(rw(1).ToString)
                Dim ContactS As String = ""
                For i As Integer = 2 To 4
                    If (rw(i).ToString <> "") Then
                        If (ContactS <> "") Then ContactS = ContactS & " / "
                        ContactS = ContactS & rw(i).ToString
                    End If
                Next
                drS(3) = ContactS
                drS(4) = MettreApost(rw(5).ToString)
                drS(5) = MettreApost(rw(6).ToString)
                drS(6) = MettreApost(rw("MailFournis").ToString)
                If (TexteRecherche <> "") Then
                    TexteRecherche = TexteRecherche.ToLower
                    If (drS(1).ToString.ToLower.Replace(TexteRecherche, "") <> drS(1).ToLower Or drS(2).ToString.ToLower.Replace(TexteRecherche, "") <> drS(2).ToLower Or drS(3).ToString.ToLower.Replace(TexteRecherche, "") <> drS(3).ToLower Or drS(4).ToString.ToLower.Replace(TexteRecherche, "") <> drS(4).ToLower Or drS(5).ToString.ToLower.Replace(TexteRecherche, "") <> drS(5).ToLower) Then
                        dt.Rows.Add(drS)
                    End If
                Else
                    dt.Rows.Add(drS)
                End If
            Next

            GridRecapSoumis.DataSource = dt
            GridView1.Columns.Item(0).Visible = False
            GridView1.Columns.Item("email").Visible = False
            GridView1.OptionsView.ColumnAutoWidth = True
            GridView1.OptionsBehavior.AutoExpandAllGroups = True
            GridView1.VertScrollVisibility = True
            GridView1.HorzScrollVisibility = True
            GridView1.BestFitColumns()
            'GridView1.Columns.Item(1).Width = 200
            'GridView1.Columns.Item(2).Width = 150
            'GridView1.Columns.Item(3).Width = 150
            'GridView1.Columns.Item(4).Width = 150
            'GridView1.Columns.Item(5).Width = 150

            'GridView1.Columns.Item(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            GridView1.Columns.Item(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left


            'Dim drInt As DataRow
            'For k As Integer = 0 To GridView1.RowCount - 1
            '    drInt = GridView1.GetDataRow(k)
            '    If (drInt(5).ToString <> "") Then
            '        For j As Integer = 0 To GridView1.Columns.Count - 1

            '            GridView1.Columns(j).AppearanceCell.BackColor = Color.LightGreen
            '        Next

            '    End If
            'Next

        End If

        ChargeGridEnCours = False

    End Sub

    Private Sub TxtNomSoumis_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNomSoumis.EditValueChanged
        If (SoumissionEnCours = False) Then
            If (TxtNomSoumis.Text <> "") Then
                GbRetrait.Enabled = True
            Else
                GbRetrait.Enabled = False
            End If
            TxtSearch.Text = TxtNomSoumis.Text
        End If

    End Sub

    Private Sub ItemCmbPays()
        CmbPaysSoumis.Properties.Items.Clear()
        query = "select LibelleZone from T_ZoneGeo where CodeZoneMere='0'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbPaysSoumis.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next
    End Sub

    Private Sub BtEnrgRetrait_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgRetrait.Click
        Dim erreur As String = ""
        'si nom soumissionaire n'est pas renseigné
        If TxtNomSoumis.Text = "" Then
            erreur += "- Nom soumissionnaire" + ControlChars.CrLf
        End If
        'si le pays du soumissionnaire n'est pas renseigné
        If CmbPaysSoumis.SelectedIndex = -1 Then
            erreur += "- Pays soumissionnaire" + ControlChars.CrLf
        End If
        'si  l'adresse du soumissionnaire n'est pas renseigné
        If TxtAdresseSoumis.Text = "" Then
            erreur += "- Adresse soumissionnaire" + ControlChars.CrLf
        End If
        'si  Téléphone soumissionnaire n'est pas renseigné
        If TxtTelSoumis.Text = "" Then
            erreur += "- Téléphone soumissionnaire" + ControlChars.CrLf
        End If
        'si  portable soumissionnaire n'est pas renseigné
        If TxtCelSoumis.Text = "" Then
            erreur += "- Portable soumissionnaire" + ControlChars.CrLf
        End If
        'si  l'e-mail du soumissionnaire n'est pas renseigné
        If TxtMailSoumis.Text = "" Then
            erreur += "- E-mail soumissionnaire" + ControlChars.CrLf
        End If
        'si nom retrait n'est pas renseigné
        If TxtNomRetrait.Text = "" Then
            erreur += "- Nom representant pour le retrait" + ControlChars.CrLf
        End If
        'si contact retrait n'est pas renseigné
        If TxtContactRetrait.Text = "" Then
            erreur += "- Contact representant pour le retrait" + ControlChars.CrLf
        End If
        If erreur = "" Then
            ' Vérif de l'existance du fournisseur
            query = "select * from T_Fournisseur where NumeroDAO='" & EnleverApost(CmbNumDAO.Text) & "' and CodeProjet='" & ProjetEnCours & "' and NomFournis='" & EnleverApost(TxtNomSoumis.Text) & "' and NomAch='" & EnleverApost(TxtNomRetrait.Text) & "' and AdresseCompleteFournis='" & EnleverApost(TxtAdresseSoumis.Text) & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            If dt0.Rows.Count > 0 Then
                SuccesMsg("Ce fournisseur existe déjà!")
                Exit Sub
            End If

            ' Enregistrement du fournisseur
            Dim DatSet = New DataSet
            query = "select * from T_Fournisseur"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_Fournisseur")
            Dim DatTable = DatSet.Tables("T_Fournisseur")
            Dim DatRow = DatSet.Tables("T_Fournisseur").NewRow()

            DatRow("NomFournis") = EnleverApost(TxtNomSoumis.Text)
            DatRow("PaysFournis") = EnleverApost(CmbPaysSoumis.Text)
            DatRow("AdresseCompleteFournis") = EnleverApost(TxtAdresseSoumis.Text)
            DatRow("TelFournis") = EnleverApost(TxtTelSoumis.Text)
            If (TxtFaxSoumis.Text <> "") Then DatRow("FaxFournis") = EnleverApost(TxtFaxSoumis.Text)
            If (TxtCelSoumis.Text <> "") Then DatRow("CelFournis") = EnleverApost(TxtCelSoumis.Text)
            If (TxtMailSoumis.Text <> "") Then DatRow("MailFournis") = EnleverApost(TxtMailSoumis.Text)
            DatRow("DateSaisie") = Now.ToShortDateString & " " & Now.ToLongTimeString
            DatRow("DateModif") = Now.ToShortDateString & " " & Now.ToLongTimeString
            DatRow("NumeroDAO") = EnleverApost(CmbNumDAO.Text)
            DatRow("NomAch") = EnleverApost(TxtNomRetrait.Text)
            If (TxtTitreRetrait.Text <> "") Then DatRow("TitreAch") = EnleverApost(TxtTitreRetrait.Text)
            DatRow("TelAch") = EnleverApost(TxtContactRetrait.Text)
            If (TxtMailRetrait.Text <> "") Then DatRow("EmailAch") = EnleverApost(TxtMailRetrait.Text)
            DatRow("CodeProjet") = ProjetEnCours

            DatSet.Tables("T_Fournisseur").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_Fournisseur")
            BDQUIT(sqlconn)
            DatSet.Clear()

            InitFormulaire()
            RemplirGridSoumis()
            TxtNomSoumis.Focus()
            SuccesMsg("Enregistrement effectué avec succès")
        Else
            SuccesMsg("Veuillez remplir ces champs : " & ControlChars.CrLf & erreur)
        End If
    End Sub

    Private Sub InitFormulaire()
        TxtMailDepot.Text = ""
        TxtContactDepot.Text = ""
        TxtTitreDepot.Text = ""
        TxtNomDepot.Text = ""
        ChkDepotRetrait.Checked = False
        GbDepot.Enabled = False

        TxtMailRetrait.Text = ""
        TxtContactRetrait.Text = ""
        TxtTitreRetrait.Text = ""
        TxtNomRetrait.Text = ""
        GbRetrait.Enabled = False

        TxtMailSoumis.Text = ""
        TxtCelSoumis.Text = ""
        TxtFaxSoumis.Text = ""
        TxtTelSoumis.Text = ""
        TxtAdresseSoumis.Text = ""
        CmbPaysSoumis.Text = ""
        TxtNomSoumis.Text = ""

        SoumissionEnCours = False
        CodeSoumis = ""
    End Sub

    Private Sub GridRecapSoumis_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridRecapSoumis.MouseUp
        If (CmbNumDAO.Text <> "" And GridView1.RowCount > 0) Then
            If SoumissionEnCours = False And Modification = False Then
                dr = GridView1.GetDataRow(GridView1.FocusedRowHandle)
                ContextMenuStrip1.Items(1).Enabled = True
                ContextMenuStrip1.Items(3).Enabled = True
                ContextMenuStrip1.Items(4).Enabled = True
                If dr("Nom représentant dépot").ToString = "" Then
                    ContextMenuStrip1.Items(2).Enabled = False
                Else
                    ContextMenuStrip1.Items(2).Enabled = True
                End If
                If (Mid(BtAfficheInfos.Text, 1, 7) = "Clôture") Then
                    If (dr(5).ToString = "") Then
                        CodeSoumis = dr(0).ToString
                        ContextMenuStrip1.Items(0).Text = "Réception des offres de " & dr(1).ToString
                        ContextMenuStrip1.Items(0).Enabled = True
                    Else
                        CodeSoumis = ""
                        ContextMenuStrip1.Items(0).Text = "Offres de " & dr(1).ToString & " déjà réçues."
                        ContextMenuStrip1.Items(0).Enabled = False
                    End If
                Else
                    CodeSoumis = ""
                    ContextMenuStrip1.Items(0).Text = "DEPOT DES OFFRES FERME"
                    ContextMenuStrip1.Items(0).Enabled = False
                End If
            Else
                ContextMenuStrip1.Items(0).Text = "Terminez l'action en cours!"
                ContextMenuStrip1.Items(0).Enabled = False
                ContextMenuStrip1.Items(1).Enabled = False
                ContextMenuStrip1.Items(2).Enabled = False
            End If
        Else
            ContextMenuStrip1.Items(0).Text = "..."
            ContextMenuStrip1.Items(0).Enabled = False
            'ContextMenuStrip1.Items(1).Text = "..."
            ContextMenuStrip1.Items(1).Enabled = False
            'ContextMenuStrip1.Items(2).Text = "..."
            ContextMenuStrip1.Items(2).Enabled = False
            'ContextMenuStrip1.Items(3).Text = "..."
            ContextMenuStrip1.Items(3).Enabled = False
            'ContextMenuStrip1.Items(4).Text = "..."
            ContextMenuStrip1.Items(4).Enabled = False

        End If
    End Sub

    Private Sub ReceptionDesOffres_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReceptionDesOffres.Click
        If (CodeSoumis <> "") Then
            SoumissionEnCours = True

            'InitFormulaire()
            BtEnrgDepot.Enabled = True
            BtModDepot.Enabled = False
            GbDepot.Enabled = True

            ' Recup des infos ***
            query = "select NomFournis,PaysFournis,AdresseCompleteFournis,TelFournis,FaxFournis,CelFournis,MailFournis,NomAch,TitreAch,TelAch,EmailAch from T_Fournisseur where NumeroDAO='" & CmbNumDAO.Text & "' and CodeProjet='" & ProjetEnCours & "' and CodeFournis='" & CodeSoumis & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtNomSoumis.Text = MettreApost(rw(0).ToString)
                CmbPaysSoumis.Text = MettreApost(rw(1).ToString)
                TxtAdresseSoumis.Text = MettreApost(rw(2).ToString)
                TxtTelSoumis.Text = MettreApost(rw(3).ToString)
                TxtFaxSoumis.Text = MettreApost(rw(4).ToString)
                TxtCelSoumis.Text = MettreApost(rw(5).ToString)
                TxtMailSoumis.Text = MettreApost(rw(6).ToString.ToLower)

                TxtNomRetrait.Text = MettreApost(rw(7).ToString)
                TxtTitreRetrait.Text = MettreApost(rw(8).ToString)
                TxtContactRetrait.Text = MettreApost(rw(9).ToString)
                TxtMailRetrait.Text = MettreApost(rw(10).ToString.ToLower)
            Next
        End If
    End Sub

    Private Sub ChkDepotRetrait_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkDepotRetrait.CheckedChanged
        If (ChkDepotRetrait.Checked = True) Then
            If BtEnrgDepot.Enabled = True Then
                TxtNomDepot.Text = TxtNomRetrait.Text
                TxtTitreDepot.Text = TxtTitreRetrait.Text
                TxtContactDepot.Text = TxtContactRetrait.Text
                TxtMailDepot.Text = TxtMailRetrait.Text

                TxtNomDepot.Properties.ReadOnly = True
                TxtTitreDepot.Properties.ReadOnly = True
                TxtContactDepot.Properties.ReadOnly = True
                TxtMailDepot.Properties.ReadOnly = True
            Else
                query = "select * from T_Fournisseur where CodeFournis='" & txtCodeFournis.Text & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    txtCodeFournis.Text = rw(0).ToString
                    TxtNomDepot.Text = MettreApost(rw("NomAch").ToString)
                    TxtTitreDepot.Text = MettreApost(rw("TitreAch").ToString)
                    TxtContactDepot.Text = rw("TelAch").ToString
                    TxtMailDepot.Text = MettreApost(rw("EmailAch").ToString)
                Next
            End If
        Else
            TxtNomDepot.Text = ""
            TxtTitreDepot.Text = ""
            TxtContactDepot.Text = ""
            TxtMailDepot.Text = ""

            TxtNomDepot.Properties.ReadOnly = False
            TxtTitreDepot.Properties.ReadOnly = False
            TxtContactDepot.Properties.ReadOnly = False
            TxtMailDepot.Properties.ReadOnly = False
        End If
    End Sub

    Private Sub BtEnrgDepot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgDepot.Click
        If (SoumissionEnCours = True And CodeSoumis <> "") Then
            Dim erreur As String = ""
            'si nom depot n'est pas renseigné
            If TxtNomDepot.Text = "" Then
                erreur += "- Nom representant pour le dépot" + ControlChars.CrLf
            End If
            'si contact depot n'est pas renseigné
            If TxtContactDepot.Text = "" Then
                erreur += "- Contact representant pour le dépot" + ControlChars.CrLf
            End If
            If erreur = "" Then
                ' Enregistrement dépot offres
                Dim DatSet = New DataSet
                query = "select * from T_Fournisseur where CodeFournis='" & CodeSoumis & "'"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Fill(DatSet, "T_Fournisseur")

                DatSet.Tables!T_Fournisseur.Rows(0)!NomDep = EnleverApost(TxtNomDepot.Text)
                DatSet.Tables!T_Fournisseur.Rows(0)!TelDep = TxtContactDepot.Text
                If (TxtTitreDepot.Text <> "") Then DatSet.Tables!T_Fournisseur.Rows(0)!TitreDep = EnleverApost(TxtTitreDepot.Text)
                If (TxtMailDepot.Text <> "") Then DatSet.Tables!T_Fournisseur.Rows(0)!EmailDep = EnleverApost(TxtMailDepot.Text)
                DatSet.Tables!T_Fournisseur.Rows(0)!DateModif = Now.ToShortDateString & " " & Now.ToLongTimeString
                DatSet.Tables!T_Fournisseur.Rows(0)!DateDepotDAO = Now.ToShortDateString & " " & Now.ToLongTimeString
                DatAdapt.Update(DatSet, "T_Fournisseur")

                DatSet.Clear()
                BDQUIT(sqlconn)
                InitFormulaire()
                RemplirGridSoumis()
                TxtNomSoumis.Focus()

                SoumissionEnCours = False
            Else
                SuccesMsg("Veuillez remplir ces champs : " + ControlChars.CrLf + erreur)
            End If

        End If
    End Sub

    Private Sub CmbPaysSoumis_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbPaysSoumis.SelectedValueChanged
        Dim CodeIndic As String = ""
        query = "select IndicZone from T_ZoneGeo where LibelleZone='" & EnleverApost(CmbPaysSoumis.Text) & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CodeIndic = "+" & rw(0).ToString
        Next
    End Sub

    Private Sub RetraitEtDepotDAO_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub BtAnulDepot_Click(sender As Object, e As EventArgs) Handles BtAnulDepot.Click
        InitFormulaire()
        TxtNomSoumis.Focus()
        SoumissionEnCours = False
        GbDepot.Enabled = False
        Modification = False
    End Sub

    Private Sub BtAnulRetrait_Click(sender As Object, e As EventArgs) Handles BtAnulRetrait.Click
        InitFormulaire()
        TxtNomSoumis.Focus()
        BtModRetrait.Enabled = False
        BtEnrgRetrait.Enabled = True
        Modification = False
    End Sub

    Private Sub ModifierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModifierToolStripMenuItem.Click
        Modification = True
        BtModRetrait.Enabled = True
        BtEnrgRetrait.Enabled = False
        drx = GridView1.GetDataRow(GridView1.FocusedRowHandle)
        Dim CodeFournis = drx(0).ToString
        query = "select * from T_Fournisseur where CodeFournis='" & CodeFournis & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            txtCodeFournis.Text = rw(0).ToString
            TxtNomSoumis.Text = MettreApost(rw("NomFournis").ToString)
            CmbPaysSoumis.Text = MettreApost(rw("PaysFournis").ToString)
            TxtAdresseSoumis.Text = MettreApost(rw("AdresseCompleteFournis").ToString)
            TxtTelSoumis.Text = MettreApost(rw("TelFournis").ToString)
            TxtFaxSoumis.Text = MettreApost(rw("FaxFournis").ToString)
            TxtCelSoumis.Text = MettreApost(rw("CelFournis").ToString)
            TxtMailSoumis.Text = MettreApost(rw("MailFournis").ToString)

            TxtNomRetrait.Text = MettreApost(rw("NomAch").ToString)
            TxtTitreRetrait.Text = MettreApost(rw("TitreAch").ToString)
            TxtContactRetrait.Text = MettreApost(rw("TelAch").ToString)
            TxtMailRetrait.Text = MettreApost(rw("EmailAch").ToString)
        Next
    End Sub

    Private Sub BtModRetrait_Click(sender As Object, e As EventArgs) Handles BtModRetrait.Click
        Dim erreur As String = ""
        'si nom soumissionaire n'est pas renseigné
        If TxtNomSoumis.Text = "" Then
            erreur += "- Nom soumissionnaire" + ControlChars.CrLf
        End If
        'si le pays du soumissionnaire n'est pas renseigné
        If CmbPaysSoumis.SelectedIndex = -1 Then
            erreur += "- Pays soumissionnaire" + ControlChars.CrLf
        End If
        'si  l'adresse du soumissionnaire n'est pas renseigné
        If TxtAdresseSoumis.Text = "" Then
            erreur += "- Adresse soumissionnaire" + ControlChars.CrLf
        End If
        'si  Téléphone soumissionnaire n'est pas renseigné
        If TxtTelSoumis.Text = "" Then
            erreur += "- Téléphone soumissionnaire" + ControlChars.CrLf
        End If
        'si  portable soumissionnaire n'est pas renseigné
        If TxtCelSoumis.Text = "" Then
            erreur += "- Portable soumissionnaire" + ControlChars.CrLf
        End If
        'si  l'e-mail du soumissionnaire n'est pas renseigné
        If TxtMailSoumis.Text = "" Then
            erreur += "- E-mail soumissionnaire" + ControlChars.CrLf
        End If
        'si nom retrait n'est pas renseigné
        If TxtNomRetrait.Text = "" Then
            erreur += "- Nom representant pour le retrait" + ControlChars.CrLf
        End If
        'si contact retrait n'est pas renseigné
        If TxtContactRetrait.Text = "" Then
            erreur += "- Contact representant pour le retrait" + ControlChars.CrLf
        End If
        If erreur = "" Then
            ' Modification du fournisseur
            query = "UPDATE T_Fournisseur SET NomFournis='" & EnleverApost(TxtNomSoumis.Text) & "',PaysFournis='" & EnleverApost(CmbPaysSoumis.Text) & "',AdresseCompleteFournis='" & EnleverApost(TxtAdresseSoumis.Text) & "',TelFournis='" & EnleverApost(TxtTelSoumis.Text) & "',FaxFournis='" & TxtFaxSoumis.Text & "',CelFournis='" & EnleverApost(TxtCelSoumis.Text) & "',MailFournis='" & EnleverApost(TxtMailSoumis.Text) & "',NomAch='" & EnleverApost(TxtNomRetrait.Text) & "',TitreAch='" & EnleverApost(TxtTitreRetrait.Text) & "',TelAch='" & EnleverApost(TxtContactRetrait.Text) & "',EmailAch='" & EnleverApost(TxtMailRetrait.Text) & "', DateModif='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' WHERE CodeFournis='" & txtCodeFournis.Text & "'"
            ExecuteNonQuery(query)
            InitFormulaire()
            RemplirGridSoumis()
            TxtNomSoumis.Focus()
            BtEnrgRetrait.Enabled = True
            BtModRetrait.Enabled = False
            SoumissionEnCours = False
            Modification = False
        Else
            SuccesMsg("Veuillez remplir ces champs : " & ControlChars.CrLf & erreur)
        End If
    End Sub

    Private Sub BtModDepot_Click(sender As Object, e As EventArgs) Handles BtModDepot.Click
        Dim erreur As String = ""
        'si nom depot n'est pas renseigné
        If TxtNomDepot.Text = "" Then
            erreur += "- Nom representant pour le dépot" & ControlChars.CrLf
        End If
        'si contact depot n'est pas renseigné
        If TxtContactDepot.Text = "" Then
            erreur += "- Contact representant pour le dépot" & ControlChars.CrLf
        End If
        If erreur = "" Then
            ' Modification du fournisseur
            query = "UPDATE T_Fournisseur SET NomDep='" & EnleverApost(TxtNomDepot.Text) & "',TitreDep='" & EnleverApost(TxtTitreDepot.Text) & "',TelDep='" & TxtContactDepot.Text & "',EmailDep='" & TxtMailDepot.Text & "', DateModif='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' WHERE CodeFournis='" & txtCodeFournis.Text & "'"
            ExecuteNonQuery(query)
            InitFormulaire()
            RemplirGridSoumis()
            TxtNomSoumis.Focus()
            BtEnrgDepot.Enabled = True
            BtModDepot.Enabled = False
            SoumissionEnCours = False
            Modification = False
        Else
            SuccesMsg("Veuillez remplir ces champs : " & ControlChars.CrLf + erreur)
        End If
    End Sub

    Private Sub ModifierReprésentantPourLeDépotToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModifierReprésentantPourLeDépotToolStripMenuItem.Click
        Modification = True
        GbDepot.Enabled = True
        BtModDepot.Enabled = True
        BtEnrgDepot.Enabled = False
        drx = GridView1.GetDataRow(GridView1.FocusedRowHandle)
        Dim CodeFournis = drx(0).ToString
        query = "select * from T_Fournisseur where CodeFournis='" & CodeFournis & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            txtCodeFournis.Text = rw(0).ToString
            TxtNomDepot.Text = MettreApost(rw("NomDep").ToString)
            TxtTitreDepot.Text = MettreApost(rw("TitreDep").ToString)
            TxtContactDepot.Text = rw("TelDep").ToString
            TxtMailDepot.Text = MettreApost(rw("EmailDep").ToString)
        Next
    End Sub

    Private Sub ImprimerReçuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImprimerReçuToolStripMenuItem.Click
        DebutChargement()
        Dim Chemin As String = lineEtat & "\Marches\DAO\Fournitures\RecuPaiement\"
        Dim reportRecu As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table
        Dim CodeFournis = GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "CodeFournis")
        query = "SELECT prixDAO FROM t_dao WHERE numeroDAO='" & EnleverApost(CmbNumDAO.Text) & "'"
        Dim montant = CInt(ExecuteScallar(query))
        reportRecu.Load(Chemin & "RecuPaiementDao.rpt")

        With crConnectionInfo
            .ServerName = ODBCNAME
            .DatabaseName = DB
            .UserID = USERNAME
            .Password = PWD
        End With

        CrTables = reportRecu.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        reportRecu.SetParameterValue("NumeroDao", EnleverApost(CmbNumDAO.Text))
        reportRecu.SetParameterValue("CodeFournisseur", CodeFournis)
        reportRecu.SetParameterValue("MontantLettre", MontantLettre(montant))
        'reportRecu.SetParameterValue("MontantLettre", MontantLettre(TxtPrixDAO.Text.Replace(" ", "")))
        reportRecu.SetParameterValue("CodeProjet", ProjetEnCours)

        FullScreenReport.FullView.ReportSource = reportRecu
        FinChargement()
        FullScreenReport.ShowDialog()
    End Sub

    Private Sub EnvoyerLeReçuParEmailToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnvoyerLeReçuParEmailToolStripMenuItem.Click
        If ConfirmMsg("Voulez-vous envoyer le reçu de retrait de dossier de ce soumissionnaire par e-mail ?") = DialogResult.Yes Then
            DebutChargement()
            Dim Chemin As String = lineEtat & "\Marches\DAO\Fournitures\RecuPaiement\"
            Dim reportRecu As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim CodeFournis = GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "CodeFournis")
            Dim NomFournis = GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "Nom")
            Dim MailFournis = GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "email")

            query = "SELECT prixDAO FROM t_dao WHERE numeroDAO='" & EnleverApost(CmbNumDAO.Text) & "'"
            Dim montant = CInt(ExecuteScallar(query))
            reportRecu.Load(Chemin & "RecuPaiementDao_Mail.rpt")

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = reportRecu.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            reportRecu.SetParameterValue("NumeroDao", EnleverApost(CmbNumDAO.Text))
            reportRecu.SetParameterValue("CodeFournisseur", CodeFournis)
            reportRecu.SetParameterValue("MontantLettre", MontantLettre(montant))
            reportRecu.SetParameterValue("CodeProjet", ProjetEnCours)
            Dim NomRepertoire As String = Environ$("TEMP")
            NomRepertoire = NomRepertoire & "\Reçu\"
            If Not System.IO.Directory.Exists(NomRepertoire) Then
                System.IO.Directory.CreateDirectory(NomRepertoire)
            End If
            FullScreenReport.FullView.ReportSource = reportRecu

            Dim nomRecu = "Reçu DAO N° " & CmbNumDAO.Text.Replace("/", "_") & ".pdf"
            Dim rep = NomRepertoire & nomRecu
            reportRecu.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, rep)

            envoieMail2(NomFournis, CmbNumDAO.Text, MailFournis, rep)
            FinChargement()
            SuccesMsg("e-mail envoyer avec succès")
        End If
    End Sub

    Private Sub TxtMailSoumis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtMailSoumis.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub TxtMailRetrait_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtMailRetrait.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub
End Class