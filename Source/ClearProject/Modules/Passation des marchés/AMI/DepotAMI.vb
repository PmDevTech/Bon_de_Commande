Imports MySql.Data.MySqlClient

Public Class DepotAMI

    Dim DrX As DataRow
    Dim TpsDepot As String = ""
    Dim ChargeGridEnCours As Boolean = False
    Dim CodeConsult As String = ""
    Dim ProcessusEnCours As Boolean = False
    Dim Action As Boolean = False
    Dim IndexModif As Integer = -1
    Dim DateReporters As String = ""
    Private Sub DepotAMI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RemplirCmbNumDp()

        Timer1.Interval = 1000 'Timer1_Tick sera déclenché toutes les secondes.
        Timer1.Start() 'On démarre le Timer

        Dim dt = New DataTable()
        'dt.Columns.Clear()
        dt.Columns.Add("N°", Type.GetType("System.String"))
        dt.Columns.Add("Nom", Type.GetType("System.String"))
        dt.Columns.Add("Pays", Type.GetType("System.String"))
        dt.Columns.Add("Contact", Type.GetType("System.String"))
        dt.Columns.Add("Email", Type.GetType("System.String"))
        dt.Columns.Add("Adresse", Type.GetType("System.String"))
        dt.Columns.Add("Dépot", Type.GetType("System.String"))
        dt.Columns.Add("Propositions", Type.GetType("System.String"))

        GridListeRestreinte.DataSource = dt
        GridView1.Columns.Item(0).Visible = False
        GridView1.Columns.Item("Dépot").Visible = False
        GridView1.Columns.Item("Propositions").Visible = False
        GridView1.Columns.Item("Nom").Width = 200
        GridView1.Columns.Item("Pays").Width = 150
        GridView1.Columns.Item("Contact").Width = 150
        GridView1.Columns.Item("Email").Width = 150
        GridView1.Columns.Item("Adresse").Width = 150
        GridView1.Columns.Item(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        GridView1.Columns.Item(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
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
                    InterText = "Clôture des propositions dans " & vbNewLine & InterText
                    BtAfficheInfos.Text = InterText.ToString
                    'GbConsultant.Enabled = True
                End If

            Else
                BtAfficheInfos.ForeColor = Color.Black
                BtAfficheInfos.Text = "Délai expiré !"
                GbConsultant.Enabled = False
            End If
        Else
            If (CmbNumDp.Text <> "") Then
                BtAfficheInfos.Text = "Délai non défini!"
                ' GbConsultant.Enabled = False
            End If
        End If
    End Sub

    Private Sub RemplirCmbNumDp()
        CmbNumDp.Properties.Items.Clear()
        'Dossier valider et date de publication arrivée
        query = "select NumeroDAMI from T_AMI where ValiderEditionAmi='Valider' and DatePub<='" & dateconvert(Now.ToShortDateString) & "' and CodeProjet='" & ProjetEnCours & "' order by NumeroDAMI"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbNumDp.Properties.Items.Add(MettreApost(rw("NumeroDAMI").ToString))
        Next
    End Sub

    Private Sub CmbNumDp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbNumDp.SelectedIndexChanged
        TpsDepot = ""
        BtAfficheInfos.Text = ""
        InitFormulaire()

        If (CmbNumDp.Text <> "") Then
            query = "select LibelleMiss, MethodeSelection, DateLimitePropo, DateFinOuverture, DateReporte from T_AMI where NumeroDAMI='" & EnleverApost(CmbNumDp.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)

            For Each rw As DataRow In dt0.Rows
                TxtMethode.Text = rw("MethodeSelection").ToString
                TxtLibMarche.Text = MettreApost(rw("LibelleMiss").ToString)

                'heure de demarrage du timer
                TpsDepot = rw("DateLimitePropo").ToString
                'En  cas de date de reporte 
                If rw("DateReporte").ToString <> "" Then TpsDepot = rw("DateReporte").ToString
                DateReporters = rw("DateReporte").ToString

                If (rw("DateLimitePropo").ToString <> "" And rw("DateFinOuverture").ToString = "") Then
                    GbConsultant.Enabled = True
                    TxtNomConsult.Focus()
                Else
                    GbConsultant.Enabled = False
                End If
            Next

            BtEnrgConsult.Text = "Ajouter à la liste"
            ChargerGridConsult()
            TxtNomConsult.Properties.ReadOnly = False
        End If

    End Sub

    Private Sub ChargerGridConsult(Optional ByVal TexteRecherche As String = "")
        ChargeGridEnCours = True

        If (CmbNumDp.Text <> "") Then

            Dim newami As DataTable = GridListeRestreinte.DataSource
            newami.Rows.Clear()

            If TexteRecherche = "" Then
                query = "select NomConsult,PaysConsult,TelConsult,FaxConsult,NomDepot,PT,PF,RefConsult, EmailConsult, AdressConsult from T_Consultant where NumeroDp='" & EnleverApost(CmbNumDp.Text) & "' order by NomConsult"
            Else
                query = "select NomConsult,PaysConsult,TelConsult,FaxConsult,NomDepot,PT,PF,RefConsult, EmailConsult, AdressConsult from T_Consultant where ((NomConsult like '%" & TexteRecherche & "%') or (PaysConsult like '%" & TexteRecherche & "%') or (TelConsult like '%" & TexteRecherche & "%')) AND  NumeroDp='" & EnleverApost(CmbNumDp.Text) & "' order by NomConsult ASC"
            End If

            Dim cmpte As Decimal = 0
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows

                cmpte += 1

                Dim drS = newami.NewRow()
                drS("N°") = rw("RefConsult").ToString
                drS("Nom") = MettreApost(rw("NomConsult").ToString)
                drS("Pays") = MettreApost(rw("PaysConsult").ToString)
                drS("Email") = MettreApost(rw("EmailConsult").ToString)
                drS("Adresse") = MettreApost(rw("AdressConsult").ToString)

                Dim ContactS As String = ""
                For i As Integer = 2 To 3
                    If (rw(i).ToString <> "") Then
                        If (ContactS <> "") Then ContactS = ContactS & " / "
                        ContactS = ContactS & rw(i).ToString
                    End If
                Next
                drS("Contact") = MettreApost(ContactS)
                drS("Dépot") = MettreApost(rw("NomDepot").ToString)
                Dim propoDemande As String = ""
                If (rw("PT").ToString = "OUI") Then
                    propoDemande = "Technique"
                End If
                If (rw("PF").ToString = "OUI") Then
                    If (propoDemande <> "") Then propoDemande = propoDemande & " / "
                    propoDemande = propoDemande & "Financière"
                End If
                drS("Propositions") = propoDemande

                newami.Rows.Add(drS)
            Next

            If Action = True Then
                ExecuteNonQuery("UPDATE t_ami set ListeRestreinte='" & cmpte & "' where NumeroDAMI='" & EnleverApost(CmbNumDp.Text) & "' and CodeProjet='" & ProjetEnCours & "'")
                Action = False
            End If
        End If

        ChargeGridEnCours = False
    End Sub

    Private Sub TxtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtSearch.TextChanged
        If TxtSearch.Text <> "" Then
            ChargerGridConsult(EnleverApost(TxtSearch.Text))
        Else
            ChargerGridConsult()
        End If
    End Sub

    Private Sub GridListeRestreinte_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridListeRestreinte.MouseUp
        If (CmbNumDp.SelectedIndex <> -1 And GridView1.RowCount > 0 And DateReporters.ToString <> "" And Mid(BtAfficheInfos.Text, 1, 7) = "Clôture") Then
            DrX = GridView1.GetDataRow(GridView1.FocusedRowHandle)
            CodeConsult = DrX("N°").ToString
            ContextMenuStrip1.Items(0).Text = "Rétirer la proposition de " & DrX("Nom").ToString
            ContextMenuStrip1.Items(0).Visible = True
        Else
            ContextMenuStrip1.Items(0).Visible = False
            ContextMenuStrip1.Items(0).Text = "..."
        End If

        'If (DrX("Dépot").ToString = "") Then
        '        CodeConsult = DrX("N°").ToString
        '        ContextMenuStrip1.Items(0).Text = "Réception des propositions de " & DrX("Nom").ToString
        '        ContextMenuStrip1.Items(0).Enabled = True
        '    End If

        '    If (ProcessusEnCours = False) Then
        '            DrX = GridView1.GetDataRow(GridView1.FocusedRowHandle)
        '            If (Mid(BtAfficheInfos.Text, 1, 7) = "Clôture") Then
        '                If (DrX("Dépot").ToString = "") Then
        '                    CodeConsult = DrX("N°").ToString
        '                    ContextMenuStrip1.Items(0).Text = "Réception des propositions de " & DrX("Nom").ToString
        '                    ContextMenuStrip1.Items(0).Enabled = True
        '                Else
        '                    CodeConsult = ""
        '                    ContextMenuStrip1.Items(0).Text = "Propositions de " & DrX("Nom").ToString & " déjà réçues."
        '                    ContextMenuStrip1.Items(0).Enabled = False
        '                End If
        '            Else
        '                CodeConsult = ""
        '                ContextMenuStrip1.Items(0).Text = "DEPOT DES PROPOSITIONS FERME"
        '                ContextMenuStrip1.Items(0).Enabled = False
        '            End If

        '        Else
        '            ContextMenuStrip1.Items(0).Text = "Terminez l'action en cours!"
        '            ContextMenuStrip1.Items(0).Enabled = False
        '        End If

        '    Else
        '        ContextMenuStrip1.Items(0).Text = "..."
        '    ContextMenuStrip1.Items(0).Enabled = False
        'End If
    End Sub

    Private Sub ReceptionDesOffres_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReceptionDesOffres.Click
        If (CodeConsult <> "") Then

            If ConfirmMsg("Voulez-vous vraiment retirer la proposition de " & GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Nom").ToString) = DialogResult.Yes Then
                ExecuteNonQuery("DELETE FROM t_consultant where RefConsult='" & CodeConsult & "' and NumeroDp='" & EnleverApost(CmbNumDp.Text) & "'")
                SuccesMsg("Retrait effectué avec succès")
                GridView1.GetDataRow(GridView1.FocusedRowHandle).Delete()
                InitFormulaire()
            End If

            'ProcessusEnCours = True
            'TxtNomConsult.Properties.ReadOnly = True

            '' Recup des infos ***
            'query = "select NomConsult,PaysConsult,AdressConsult,TelConsult,FaxConsult,EmailConsult from T_Consultant where NumeroDp='" & CmbNumDp.Text & "' and RefConsult='" & CodeConsult & "'"
            'Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            'For Each rw As DataRow In dt0.Rows
            '    TxtNomConsult.Text = MettreApost(rw("NomConsult").ToString)
            '    TxtPaysConsult.Text = MettreApost(rw("PaysConsult").ToString)
            '    TxtAdresseConsult.Text = MettreApost(rw("AdressConsult").ToString)
            '    TxtTelConsult.Text = rw("TelConsult").ToString
            '    TxtFaxConsult.Text = rw("FaxConsult").ToString
            '    TxtMailConsult.Text = rw("EmailConsult").ToString.ToLower
            'Next
        End If
    End Sub

    Private Sub InitFormulaire()
        TxtNomConsult.Text = ""
        TxtPaysConsult.Text = ""
        TxtAdresseConsult.Text = ""
        TxtTelConsult.Text = ""
        TxtFaxConsult.Text = ""
        TxtMailConsult.Text = ""
        TxtNomConsult.Focus()
        ' ProcessusEnCours = False
        CodeConsult = ""
        IndexModif = -1
    End Sub

    Private Sub DepotDP_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub BtEnrgConsult_Click(sender As System.Object, e As System.EventArgs) Handles BtEnrgConsult.Click
        If CmbNumDp.SelectedIndex <> -1 Then

            If BtEnrgConsult.Text = "Ajouter à la liste" Then
                If TxtNomConsult.IsRequiredControl("Veuillez saisir le nom du consulatant") Then
                    Exit Sub
                End If
                If TxtPaysConsult.IsRequiredControl("Veuillez saisir le pays du consulatant") Then
                    Exit Sub
                End If
                If TxtTelConsult.IsRequiredControl("Veuillez saisir le numero du consulatant") Then
                    Exit Sub
                End If
                If TxtAdresseConsult.IsRequiredControl("Veuillez saisir l'adresse du consulatant") Then
                    Exit Sub
                End If

                query = "select count(*) from T_Consultant where NumeroDp='" & EnleverApost(CmbNumDp.Text) & "'and NomConsult='" & EnleverApost(TxtNomConsult.Text) & "' and PaysConsult='" & EnleverApost(TxtPaysConsult.Text) & "'"
                If Val(ExecuteScallar(query)) > 0 Then
                    SuccesMsg("Le consultant existe déjà")
                    Exit Sub
                End If

                ' Dim LongAbreg As Decimal = 5
                'If (Len((TxtNomConsult.Text.Replace(" ", "")).Replace("'", "")) < 5) Then LongAbreg = Len((TxtNomConsult.Text.Replace(" ", "")).Replace("'", ""))

                Dim DatSet = New DataSet
                query = "select * from T_Consultant"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "T_Consultant")
                Dim DatTable = DatSet.Tables("T_Consultant")
                Dim DatRow = DatSet.Tables("T_Consultant").NewRow()

                DatRow("CodeConsult") = GenererCode(8) ' Mid((TxtNomConsult.Text.Replace(" ", "")).Replace("'", ""), 1, LongAbreg).ToUpper
                DatRow("NomConsult") = EnleverApost(TxtNomConsult.Text)
                DatRow("PaysConsult") = EnleverApost(TxtPaysConsult.Text)
                DatRow("TelConsult") = EnleverApost(TxtTelConsult.Text)
                DatRow("AdressConsult") = EnleverApost(TxtAdresseConsult.Text)
                DatRow("FaxConsult") = IIf(TxtFaxConsult.Text <> "", TxtFaxConsult.Text, "").ToString
                DatRow("EmailConsult") = IIf(TxtMailConsult.Text <> "", TxtMailConsult.Text, "").ToString
                DatRow("NumeroDp") = EnleverApost(CmbNumDp.Text)
                DatRow("DateDepot") = Now.ToShortDateString & " " & Now.ToLongTimeString
                DatRow("DateSaisie") = Now.ToShortDateString & " " & Now.ToLongTimeString
                DatRow("DateModif") = Now.ToShortDateString & " " & Now.ToLongTimeString
                DatRow("Operateur") = CodeUtilisateur

                DatSet.Tables("T_Consultant").Rows.Add(DatRow)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "T_Consultant")
                DatSet.Clear()
                BDQUIT(sqlconn)

                Action = True
            Else
                query = "update T_Consultant set NomConsult = '" & EnleverApost(TxtNomConsult.Text) & "', PaysConsult = '" & EnleverApost(TxtPaysConsult.Text) & "', TelConsult = '" & EnleverApost(TxtTelConsult.Text) & "', AdressConsult = '" & EnleverApost(TxtAdresseConsult.Text) & "', EmailConsult = '" & TxtMailConsult.Text & "', DateModif='" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "' where RefConsult='" & CodeConsult & "'"
                ExecuteNonQuery(query)
            End If

            ChargerGridConsult()
            InitFormulaire()
            BtEnrgConsult.Text = "Ajouter à la liste"
        End If
    End Sub

    'Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
    '    e.Cancel = True
    'End Sub

    Private Sub GridListeRestreinte_DoubleClick(sender As Object, e As EventArgs) Handles GridListeRestreinte.DoubleClick

        If GridView1.RowCount > 0 Then
            DrX = GridView1.GetDataRow(GridView1.FocusedRowHandle)
            CodeConsult = DrX("N°").ToString
            IndexModif = GridView1.FocusedRowHandle
            '  TxtNomConsult.Properties.ReadOnly = True
            TxtNomConsult.Text = DrX("Nom").ToString
            TxtPaysConsult.Text = DrX("Pays").ToString
            TxtAdresseConsult.Text = DrX("Adresse").ToString
            TxtTelConsult.Text = DrX("Contact").ToString.Split("/")(0)
            If DrX("Contact").ToString.IndexOf("/") >= 0 Then TxtFaxConsult.Text = DrX("Contact").ToString.Split("/")(1)
            TxtMailConsult.Text = DrX("Email").ToString
            BtEnrgConsult.Text = "Modifier"
        End If
    End Sub

End Class