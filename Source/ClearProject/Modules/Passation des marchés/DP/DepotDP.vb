Imports MySql.Data.MySqlClient

Public Class DepotDP

    Dim dr As DataRow
    Dim dt = New DataTable()
    Dim TpsDepot As String = ""
    Dim CodeConsult As String = ""
    Dim ProcessusEnCours As Boolean = False
    Dim LigneSelect As Integer = 0
    Dim DateReportes As String = ""

    Private Sub DepotDP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RemplirCmbNumDp()

        Timer1.Interval = 1000 'Timer1_Tick sera déclenché toutes les secondes.
        Timer1.Start() 'On démarre le Timer
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
                    InterText = "Clôture des dépôts de propositions dans " & vbNewLine & InterText
                    BtAfficheInfos.Text = InterText.ToString
                End If

            Else
                BtAfficheInfos.ForeColor = Color.Black
                BtAfficheInfos.Text = "Dépôt des propositions clôturé !"
            End If
        Else
            If (CmbNumDp.Text <> "") Then
                BtAfficheInfos.Text = "Délai non défini !"
            End If
        End If

    End Sub

    Private Sub RemplirCmbNumDp()
        CmbNumDp.Properties.Items.Clear()
        CmbNumDp.Text = ""
        query = "select NumeroDp from T_DP where DossValider='Valider' and DateEnvoiDp<='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' and Statut<>'Annulé' and CodeProjet='" & ProjetEnCours & "' ORDER BY DateEdition DESC"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbNumDp.Properties.Items.Add(MettreApost(rw("NumeroDp").ToString))
        Next
    End Sub

    Private Sub CmbNumDp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbNumDp.SelectedIndexChanged

        TpsDepot = ""
        BtAfficheInfos.Text = ""
        InitFormulaire()

        If (CmbNumDp.SelectedIndex <> -1) Then

            query = "select LibelleMiss, MethodeSelection, DateReporter, DateLimitePropo, DateFinOuverture from T_DP where NumeroDp='" & EnleverApost(CmbNumDp.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)

            For Each rw As DataRow In dt0.Rows
                TxtMethode.Text = MettreApost(rw("MethodeSelection").ToString)
                TxtLibMarche.Text = MettreApost(rw("LibelleMiss").ToString)
                ' If (rw("DateLimitePropo").ToString <> "" And rw("DateFinOuverture").ToString = "") Then
                If (rw("DateLimitePropo").ToString <> "") Then
                    TpsDepot = rw("DateLimitePropo").ToString
                End If

                If rw("DateReporter").ToString <> "" Then TpsDepot = rw("DateReporter").ToString
                DateReportes = rw("DateReporter").ToString
            Next

            RemplirGridListeRest()
        End If
    End Sub

    Private Sub RemplirGridListeRest(Optional ByVal TexteRecherche As String = "")
        If (CmbNumDp.SelectedIndex <> -1) Then
            dt.Columns.Clear()
            dt.Columns.Add("N°", Type.GetType("System.String"))
            dt.Columns.Add("Nom", Type.GetType("System.String"))
            dt.Columns.Add("Pays", Type.GetType("System.String"))
            dt.Columns.Add("Contact", Type.GetType("System.String"))
            dt.Columns.Add("Dépot", Type.GetType("System.String"))
            dt.Columns.Add("Propositions", Type.GetType("System.String"))

            Dim nbSoumis As Decimal = 0
            dt.Rows.Clear()

            If TexteRecherche = "" Then
                query = "select * from T_Consultant where NumeroDp='" & EnleverApost(CmbNumDp.Text) & "' order by NomConsult"
            Else
                query = "select * from T_Consultant where ((NomConsult like '%" & TexteRecherche & "%') or (PaysConsult like '%" & TexteRecherche & "%') or (NomDepot like '%" & TexteRecherche & "%')) and  NumeroDp='" & EnleverApost(CmbNumDp.Text) & "' order by NomConsult"
            End If

            Dim dt0 As DataTable = ExcecuteSelectQuery(query)

            For Each rw As DataRow In dt0.Rows
                nbSoumis = nbSoumis + 1
                Dim drS = dt.NewRow()
                drS("N°") = rw("RefConsult").ToString
                drS("Nom") = MettreApost(rw("NomConsult").ToString)
                drS("Pays") = MettreApost(rw("PaysConsult").ToString)
                Dim ContactS As String = ""
                For i As Integer = 4 To 5
                    If (rw(i).ToString <> "") Then
                        If (ContactS <> "") Then ContactS = ContactS & " / "
                        ContactS = ContactS & rw(i).ToString
                    End If
                Next

                drS("Contact") = ContactS
                drS("Dépot") = MettreApost(rw("NomDepot").ToString)
                drS("Propositions") = IIf(rw("ProptionDeposer").ToString <> "", "Déposée", "").ToString

                dt.Rows.Add(drS)

                'Dim propoDemande As String = ""
                '    If (rw("PT").ToString = "OUI") Then
                '        propoDemande = "Technique"
                '    End If
                '    If (rw("PF").ToString = "OUI") Then
                '        If (propoDemande <> "") Then propoDemande = propoDemande & " / "
                '        propoDemande = propoDemande & "Financière"
                '    End If
            Next

            GridListeRestreinte.DataSource = dt
            GridView1.Columns.Item(0).Visible = False
            GridView1.Columns.Item(1).Width = 200
            GridView1.Columns.Item(2).Width = 150
            GridView1.Columns.Item(3).Width = 150
            GridView1.Columns.Item(4).Width = 200
            GridView1.Columns.Item(5).Width = 100

            GridView1.Columns.Item(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            GridView1.Columns.Item(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                'Recherche consultant
                ' If (drS("Nom").ToString.ToLower.Replace(TexteRecherche, "") <> drS("Nom").ToLower Or drS("Pays").ToString.ToLower.Replace(TexteRecherche, "") <> drS("Pays").ToLower Or drS("Contact").ToString.ToLower.Replace(TexteRecherche, "") <> drS("Contact").ToLower Or drS("Dépot").ToString.ToLower.Replace(TexteRecherche, "") <> drS("Dépot").ToLower Or drS("Propositions").ToString.ToLower.Replace(TexteRecherche, "") <> drS("Propositions").ToLower) Then
            End If
    End Sub

    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtSearch.TextChanged
        If (TxtSearch.Text.Trim <> "") Then
            RemplirGridListeRest(EnleverApost(TxtSearch.Text))
        Else
            RemplirGridListeRest()
        End If
    End Sub

    Private Sub GridListeRestreinte_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridListeRestreinte.MouseUp
        If (CmbNumDp.SelectedIndex <> -1 And GridView1.RowCount > 0) Then

            If (ProcessusEnCours = False) Then

                dr = GridView1.GetDataRow(GridView1.FocusedRowHandle)
                LigneSelect = GridView1.FocusedRowHandle

                If (Mid(BtAfficheInfos.Text, 1, 7) = "Clôture") Then
                    If (dr("Dépot").ToString = "") Then
                        CodeConsult = dr("N°").ToString
                        ContextMenuStrip1.Items(0).Text = "Réception des propositions de " & dr("Nom").ToString
                        ContextMenuStrip1.Items(0).Enabled = True
                        ContextMenuStrip1.Items(1).Visible = False
                    Else
                        CodeConsult = ""
                        ContextMenuStrip1.Items(0).Text = "Propositions de " & dr("Nom").ToString & " déjà réçues."
                        ContextMenuStrip1.Items(0).Enabled = False

                        If DateReportes.ToString <> "" Then
                            ContextMenuStrip1.Items(1).Visible = True
                            ContextMenuStrip1.Items(1).Text = "Rétirer la proposition de " & dr("Nom").ToString & "."
                        Else
                            ContextMenuStrip1.Items(1).Visible = False
                        End If
                    End If
                Else
                    CodeConsult = ""
                    ContextMenuStrip1.Items(0).Text = "DEPOT DES PROPOSITIONS FERME"
                    ContextMenuStrip1.Items(0).Enabled = False
                    ContextMenuStrip1.Items(1).Visible = False
                End If
            Else
                ContextMenuStrip1.Items(0).Text = "Terminez l'action en cours !"
                ContextMenuStrip1.Items(0).Enabled = False
                ContextMenuStrip1.Items(1).Visible = False
            End If

        Else
            ContextMenuStrip1.Items(0).Text = "..."
            ContextMenuStrip1.Items(0).Enabled = False
            ContextMenuStrip1.Items(1).Visible = False
        End If
    End Sub

    Private Sub ReceptionDesOffres_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReceptionDesOffres.Click
        If (CodeConsult <> "") Then
            ProcessusEnCours = True
            InitialiserRepresentant()

            ' Recup des infos ***
            query = "select NomConsult,PaysConsult,AdressConsult,TelConsult,FaxConsult,EmailConsult from T_Consultant where NumeroDp='" & EnleverApost(CmbNumDp.Text) & "' and RefConsult='" & CodeConsult & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtNomConsult.Text = MettreApost(rw("NomConsult").ToString)
                TxtPaysConsult.Text = MettreApost(rw("PaysConsult").ToString)
                TxtAdresseConsult.Text = MettreApost(rw("AdressConsult").ToString)
                TxtTelConsult.Text = MettreApost(rw("TelConsult").ToString)
                TxtFaxConsult.Text = MettreApost(rw("FaxConsult").ToString)
                TxtMailConsult.Text = MettreApost(rw("EmailConsult").ToString).ToLower
                GbRepresentant.Enabled = True
            Next

            BtEnrgDepot.Enabled = True
            NewReadOnly(False)
        End If
    End Sub

    Private Sub BtEnrgDepot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgDepot.Click
        If (ProcessusEnCours = True And CodeConsult <> "") Then

            If TxtNomRep.IsRequiredControl("Veuillez saisir le nom du répresentant") Then
                TxtNomRep.Focus()
                Exit Sub
            End If
            If TxtContactRep.IsRequiredControl("Veuillez saisir le contact du répresentant") Then
                TxtContactRep.Focus()
                Exit Sub
            End If

            ExecuteNonQuery("Update T_Consultant set NomDepot='" & EnleverApost(TxtNomRep.Text) & "', ContactDepot='" & EnleverApost(TxtContactRep.Text) & "', TitreDepot='" & EnleverApost(TxtTitreRep.Text) & "', MailDepot='" & EnleverApost(TxtMailRep.Text) & "', DateModif='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', Operateur='" & CodeUtilisateur & "', DateDepot='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', ProptionDeposer='Déposer' where RefConsult='" & CodeConsult & "' and NumeroDp='" & EnleverApost(CmbNumDp.Text) & "'")
            SuccesMsg("Proposition déposée avec succès")

            GridView1.GetDataRow(LigneSelect).Item("Propositions") = "Déposée"
            GridView1.GetDataRow(LigneSelect).Item("Dépot") = TxtNomRep.Text

            ProcessusEnCours = False
            CodeConsult = ""
            NewReadOnly(True)
            BtEnrgDepot.Enabled = False
            'RemplirGridListeRest()
        End If
    End Sub

    Private Sub NewReadOnly(ByVal value As Boolean)
        TxtNomRep.Properties.ReadOnly = value
        TxtTitreRep.Properties.ReadOnly = value
        TxtContactRep.Properties.ReadOnly = value
        TxtMailRep.Properties.ReadOnly = value
    End Sub

    Private Sub InitialiserRepresentant()
        TxtMailRep.Text = ""
        TxtContactRep.Text = ""
        TxtTitreRep.Text = ""
        TxtNomRep.Text = ""
    End Sub

    Private Sub InitFormulaire()
        InitialiserRepresentant()

        TxtMailConsult.Text = ""
        TxtFaxConsult.Text = ""
        TxtTelConsult.Text = ""
        TxtAdresseConsult.Text = ""
        TxtPaysConsult.Text = ""
        TxtNomConsult.Text = ""

        TxtMethode.Text = ""
        TxtLibMarche.Text = ""

        ProcessusEnCours = False
        CodeConsult = ""
        BtEnrgDepot.Enabled = False
        NewReadOnly(True)
        LigneSelect = 0
        ' TxtSearch.Text = ""
    End Sub

    Private Sub DepotDP_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub RétirerLaPropositionDeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RétirerLaPropositionDeToolStripMenuItem.Click
        If GridView1.RowCount > 0 Then
            dr = GridView1.GetDataRow(GridView1.FocusedRowHandle)
            If ConfirmMsg("Voulez-vous vraiment retirer la proposition de " & dr("Nom").ToString) = DialogResult.Yes Then
                ExecuteNonQuery("delete from T_Consultant where RefConsult='" & dr("N°").ToString & "' and NumeroDp='" & EnleverApost(CmbNumDp.Text) & "'")
                SuccesMsg("Retrait effectué avec succès")
                GridView1.GetDataRow(GridView1.FocusedRowHandle).Delete()

                InitialiserRepresentant()

                TxtMailConsult.Text = ""
                TxtFaxConsult.Text = ""
                TxtTelConsult.Text = ""
                TxtAdresseConsult.Text = ""
                TxtPaysConsult.Text = ""
                TxtNomConsult.Text = ""

                ProcessusEnCours = False
                CodeConsult = ""
                BtEnrgDepot.Enabled = False
                NewReadOnly(True)
                LigneSelect = 0
            End If
        End If

    End Sub

End Class