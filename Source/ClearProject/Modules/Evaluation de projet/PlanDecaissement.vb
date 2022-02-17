Imports MySql.Data.MySqlClient

Public Class PlanDecaissement

    Dim dtEcheanceBaill = New DataTable()
    Dim DrX As DataRow

    Dim CodPartit As String = ""
    Dim CodeBailleur As String = "-1"
    Dim MontAct As Decimal = 0
    Dim dDebut As Date
    Dim dFin As Date
    Dim DateDebut As Date = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
    Dim DateFin As Date = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")
    Dim tabCodeBailleurs As String()
    Private Sub PlanDecaissement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        InitBoutEcheance()
        LoadCompteBailleur()
        cmbBailleur_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub LoadActivite(Bailleur As String)
        'On vide la repatition des montants
        RemplirEchMontant(-1, -1, -1)

        Dim CodeBailleur As Decimal = 0
        Dim dtEcheanceAct = New DataTable()
        dtEcheanceAct.Columns.Clear()
        dtEcheanceAct.Columns.Add("CodeX", Type.GetType("System.String"))
        dtEcheanceAct.Columns.Add("Ref", Type.GetType("System.String"))
        dtEcheanceAct.Columns.Add("Code", Type.GetType("System.String"))
        dtEcheanceAct.Columns.Add("Description", Type.GetType("System.String"))
        dtEcheanceAct.Columns.Add("Montant total (FCFA)", Type.GetType("System.String"))
        dtEcheanceAct.Columns.Add("Date de début", Type.GetType("System.String"))
        dtEcheanceAct.Columns.Add("Date de fin", Type.GetType("System.String"))
        dtEcheanceAct.Columns.Add("*", Type.GetType("System.String"))

        Dim cptr As Decimal = 0
        Dim TotalDotation As Decimal = 0
        Dim TotalMontantReparti As Decimal = 0
        Dim InitialeBailleur As String = String.Empty
        If Bailleur = "Not" Then
            query = "SELECT CodePartition,LibelleCourt,LibellePartition,DateDebutPartition,DateFinPartition FROM t_partition WHERE DateDebutPartition>='" & dateconvert(DateDebut) & "' AND DateFinPartition<='" & dateconvert(DateFin) & "' AND CodePartition NOT IN(SELECT DISTINCT CodePartition FROM t_repartitionparbailleur r, t_besoinpartition b WHERE b.RefBesoinPartition=r.RefBesoinPartition AND CodeProjet='" & ProjetEnCours & "') ORDER BY LibelleCourt ASC"
        Else
            CodeBailleur = Val(Bailleur)
            query = "SELECT CodePartition,LibelleCourt,LibellePartition,DateDebutPartition,DateFinPartition FROM t_partition WHERE DateDebutPartition>='" & dateconvert(DateDebut) & "' AND DateFinPartition<='" & dateconvert(DateFin) & "' AND CodePartition IN(SELECT DISTINCT CodePartition FROM t_repartitionparbailleur r, t_besoinpartition b WHERE b.RefBesoinPartition=r.RefBesoinPartition AND CodeBailleur='" & CodeBailleur & "' AND CodeProjet='" & ProjetEnCours & "') ORDER BY LibelleCourt ASC"
            InitialeBailleur = ExecuteScallar("SELECT InitialeBailleur FROM t_bailleur WHERE CodeBailleur='" & CodeBailleur & "'")
        End If

        Dim dtActivites As DataTable = ExcecuteSelectQuery(query)
        For Each rwActivite As DataRow In dtActivites.Rows
            cptr += 1
            Dim drS = dtEcheanceAct.NewRow()
            If Bailleur = "Not" Then
                query = "SELECT SUM(QteNature*PUNature) FROM t_besoinpartition WHERE CodePartition='" & rwActivite("CodePartition") & "' AND CodeProjet='" & ProjetEnCours & "'"
            Else
                query = "SELECT SUM(MontantBailleur) FROM t_repartitionparbailleur r, t_besoinpartition b WHERE b.RefBesoinPartition=r.RefBesoinPartition AND CodePartition='" & rwActivite("CodePartition") & "' AND CodeBailleur='" & CodeBailleur & "' AND CodeProjet='" & ProjetEnCours & "'"
            End If

            Dim MontTotal As Decimal = Val(ExecuteScallar(query))
            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = rwActivite("CodePartition").ToString
            drS(2) = rwActivite("LibelleCourt").ToString
            drS(3) = MettreApost(rwActivite("LibellePartition").ToString)
            drS(4) = AfficherMonnaie(MontTotal.ToString)
            drS(5) = CDate(rwActivite("DateDebutPartition")).ToString("dd/MM/yyyy")
            drS(6) = CDate(rwActivite("DateFinPartition")).ToString("dd/MM/yyyy")
            drS(7) = ""
            TotalDotation += MontTotal
            dtEcheanceAct.Rows.Add(drS)

        Next

        TotalMontantReparti = GetTotalMontantReparti(Bailleur)

        If dtActivites.Rows.Count = 0 Then
            TxtMontantBailleur.ResetText()
        Else
            TxtMontantBailleur.Text = ("Total dotation " & InitialeBailleur).Trim() & " : " & AfficherMonnaie(TotalDotation) & "  |  Montant total reparti : " & AfficherMonnaie(TotalMontantReparti)
        End If

        GridEcheanceActivite.DataSource = dtEcheanceAct

        'If ViewEcheanceActivite.Columns("CodeX").Visible Then
        ViewEcheanceActivite.Columns(0).Visible = False
        ViewEcheanceActivite.Columns(1).Visible = False
        ViewEcheanceActivite.Columns(2).Width = 60
        ViewEcheanceActivite.Columns(3).Width = Me.Width
        ViewEcheanceActivite.Columns(4).Width = 120
        ViewEcheanceActivite.Columns(5).Width = 100
        ViewEcheanceActivite.Columns(6).Width = 100
        ViewEcheanceActivite.Columns(7).Visible = False

        ViewEcheanceActivite.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewEcheanceActivite.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewEcheanceActivite.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewEcheanceActivite.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        ViewEcheanceActivite.Columns(2).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        ViewEcheanceActivite.Columns(6).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        ViewEcheanceActivite.Columns(5).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        ViewEcheanceActivite.Columns(4).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        ViewEcheanceActivite.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewEcheanceActivite, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
        'GridEcheanceActivite.Refresh()
        'End If

    End Sub
    Private Sub ReloadGAP(CodePartition As Decimal)
        Dim TotalDotationActivite As Decimal = Val(ExecuteScallar("SELECT SUM(QteNature*PUNature) FROM t_besoinpartition WHERE CodePartition='" & CodePartition & "' AND CodeProjet='" & ProjetEnCours & "'"))
        Dim TotalMontantRepartiActivite As Decimal = Val(ExecuteScallar("SELECT SUM(MontantBailleur) FROM t_repartitionparbailleur r, t_besoinpartition b WHERE b.RefBesoinPartition=r.RefBesoinPartition AND CodePartition='" & CodePartition & "' AND CodeProjet='" & ProjetEnCours & "'"))
        If TotalDotationActivite <> TotalMontantRepartiActivite Then
            Dim GAP As Decimal = TotalDotationActivite - TotalMontantRepartiActivite
            txtGAP.Text = "GAP : " & AfficherMonnaie(GAP)
        Else
            txtGAP.ResetText()
        End If
    End Sub
    Private Function GetTotalMontantReparti(Bailleur As String) As Decimal
        If Bailleur = "Not" Then
            Return 0
        Else
            query = "SELECT SUM(MontantEcheance) FROM t_echeanceactivite WHERE CodeBailleur='" & Val(Bailleur) & "'"
        End If
        Return Val(ExecuteScallar(query))
    End Function
    Private Sub PlanDecaissement_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub
    Private Sub LoadCompteBailleur()
        cmbBailleur.Properties.Items.Clear()
        cmbBailleur.ResetText()
        query = "SELECT * FROM t_bailleur WHERE CodeProjet='" & ProjetEnCours & "' ORDER BY InitialeBailleur ASC"
        Dim dt As Data.DataTable = ExcecuteSelectQuery(query)
        ReDim tabCodeBailleurs(dt.Rows.Count)
        cmbBailleur.Properties.Items.Add("Activités sans bailleur")
        tabCodeBailleurs(0) = "Not"
        Dim cpte As Decimal = 1
        For Each rw As DataRow In dt.Rows
            cmbBailleur.Properties.Items.Add(rw("InitialeBailleur") & " - " & MettreApost(rw("NomBailleur")))
            tabCodeBailleurs(cpte) = rw("CodeBailleur")
            cpte += 1
        Next
    End Sub
    Private Sub GridEcheanceActivite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEcheanceActivite.Click
        If (ViewEcheanceActivite.RowCount > 0) Then

            TxtTitre.Enabled = True
            RdMois.Enabled = True
            RdMois.Checked = False
            RdAns.Enabled = True
            RdAns.Checked = False
            RdAutre.Enabled = True
            RdAutre.Checked = False
            DrX = ViewEcheanceActivite.GetDataRow(ViewEcheanceActivite.FocusedRowHandle)
            CodPartit = DrX(1).ToString
            MontAct = CDec(DrX(4).ToString.Replace(" ", ""))
            dDebut = CDate(DrX(5).ToString).ToShortDateString
            dFin = CDate(DrX(6).ToString).ToShortDateString

            DTDateEcheance.Properties.MinValue = dDebut
            DTDateEcheance.Properties.MaxValue = dFin
            DTDateEcheance.DateTime = dDebut

            ColorRowGrid(ViewEcheanceActivite, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewEcheanceActivite, "[Ref]='" & CodPartit & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)

            If cmbBailleur.SelectedIndex >= 0 Then
                CodeBailleur = tabCodeBailleurs(cmbBailleur.SelectedIndex)
            End If
            RemplirEchMontant(CodPartit, MontAct, CodeBailleur)

            ReloadGAP(CodPartit)
        End If

        TxtMontantEcheance.ResetText()

    End Sub

    Private Sub RemplirEchMontant(ByVal Partition As String, ByVal montTotal As String, Bailleur As String)

        Dim dtEcheanceMont = New DataTable()
        dtEcheanceMont.Columns.Clear()

        dtEcheanceMont.Columns.Add("CodeX", Type.GetType("System.String"))
        dtEcheanceMont.Columns.Add("Ref", Type.GetType("System.String"))
        dtEcheanceMont.Columns.Add("Date", Type.GetType("System.String"))
        dtEcheanceMont.Columns.Add("Montant", Type.GetType("System.String"))
        dtEcheanceMont.Columns.Add("*", Type.GetType("System.String"))

        Dim cptr As Decimal = 0
        Dim TotalMontantReparti As Decimal = 0
        Dim ResteARepartir As Decimal = CDec(montTotal.Replace(" ", ""))

        If Bailleur <> "Not" And Bailleur <> "-1" Then
            query = "select RefEcheance,DateEcheance,MontantEcheance from T_EcheanceActivite where CodePartition='" & Partition & "' AND CodeBailleur='" & Bailleur & "' Order by STR_TO_DATE(DateEcheance,'%d/%m/%Y') ASC"
            dtEcheanceMont.Rows.Clear()
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count = 0 Then
                ActiverActionRepartition()
                TxtResteEcheance.ResetText()
            Else
                For Each rw In dt.Rows
                    cptr += 1
                    Dim drS = dtEcheanceMont.NewRow()

                    drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
                    drS(1) = rw("RefEcheance").ToString
                    drS(2) = rw("DateEcheance").ToString
                    drS(3) = AfficherMonnaie(rw("MontantEcheance").ToString)
                    drS(4) = ""
                    TotalMontantReparti += CDec(rw("MontantEcheance"))
                    dtEcheanceMont.Rows.Add(drS)
                Next
                ResteARepartir -= TotalMontantReparti
                TxtResteEcheance.Text = "Reste à répartir : " & AfficherMonnaie(ResteARepartir)
            End If
        Else
            InitBoutEcheance()
        End If

        GridEcheanceMontant.DataSource = dtEcheanceMont

        'If ViewEcheanceMontant.Columns("CodeX").Visible = True Then
        ViewEcheanceMontant.Columns("CodeX").Visible = False
        ViewEcheanceMontant.Columns(1).Visible = False
        ViewEcheanceMontant.Columns(2).Width = 100
        ViewEcheanceMontant.Columns(3).Width = 130
        ViewEcheanceMontant.Columns(4).Visible = False

        ViewEcheanceMontant.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewEcheanceMontant.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewEcheanceMontant.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewEcheanceMontant, "[CodeX]='x'", Color.LightBlue, "Times New Roman", 11, FontStyle.Regular, Color.Black)
        'End If

        Dim MontTT As Decimal = CDec(montTotal.Replace(" ", ""))
        If MontTT > 0 Then
            If (ResteARepartir = 0) Then
                RdMois.Enabled = False
                RdAns.Enabled = False
                RdAutre.Enabled = False
                If BtCreerPlan.Visible Then
                    BtCreerPlan.Visible = False
                End If
            ElseIf ResteARepartir > 0 Then
                If ViewEcheanceMontant.RowCount > 0 Then
                    ActiverActionRepartition()
                    RdMois.Enabled = False
                    RdAns.Enabled = False
                    RdAutre.Checked = True
                Else
                    ActiverActionRepartition()
                End If
            End If
        End If

        'Actualisation du state
        Dim OldState As String = TxtMontantBailleur.Text
        TxtMontantBailleur.Text = OldState.Split("Montant total reparti : ")(0) & "Montant total reparti : " & AfficherMonnaie(GetTotalMontantReparti(Bailleur))

    End Sub

    Private Sub BtCreerPlan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtCreerPlan.Click
        If cmbBailleur.SelectedIndex = -1 Or cmbBailleur.SelectedIndex = 0 Then
            FailMsg("Impossible d'établir un plan." & vbNewLine & "Aucun bailleur trouvé pour cette activité.")
            Exit Sub
        End If

        If (txtGAP.Text.Length > 0) Then
            FailMsg("Impossible d'établir un plan." & vbNewLine & "Le montant de l'activité n'est pas totalement pris en charge.")
            Exit Sub
        End If

        If (RdMois.Checked = True) Then

            If (CmbJour.Text <> "") Then ' Repartition par mois *********
                Dim DateDeb As Date = dDebut
                Dim DateFin As Date = dFin
                Dim JrValid As String = CmbJour.Text
                Dim DateDeb2 As Date = DateDeb
                Dim LesDates(100) As String
                Dim LesMonts(100) As String
                Dim Cptr As Decimal = 0
                Dim MontActiv As Decimal = MontAct

                While (DateTime.Compare(DateDeb2, DateFin) <= 0)
                    Dim Jr As String = Mid(DateDeb2.ToShortDateString, 1, 2)
                    If (CInt(Jr) = CInt(JrValid)) Then
                        LesDates(Cptr) = DateDeb2.ToString
                        Cptr = Cptr + 1
                        DateDeb2 = DateDeb2.AddMonths(1)
                    Else
                        DateDeb2 = DateDeb2.AddDays(1)
                    End If

                End While

                If (Cptr <> 0) Then
                    Dim PartEch As Decimal = MontActiv / Cptr
                    Dim MontEch As Decimal = Math.Round(PartEch, 0)
                    Dim RestEch As Decimal = MontActiv - (Cptr * MontEch)

                    For i As Integer = 0 To Cptr - 1
                        If (i = 0) Then
                            EnregistrerEcheance(CDate(LesDates(i)), (MontEch + RestEch).ToString, CodeBailleur, True)
                        Else
                            EnregistrerEcheance(CDate(LesDates(i)), (MontEch).ToString, CodeBailleur, True)
                        End If
                    Next

                Else
                    SuccesMsg("Répartition impossible.")
                    Exit Sub
                End If

            Else
                SuccesMsg("Veuillez définir le jour de validation de la prévision.")
                Exit Sub
            End If

        ElseIf (RdAns.Checked = True) Then ' Repartition par an *********

            If (CmbJour.Text <> "" And CmbMois.Text <> "") Then

                Dim DateDeb As Date = dDebut
                Dim DateFin As Date = dFin
                Dim JrValid As String = CmbJour.Text
                Dim MsValid As String = CmbMois.Text
                Dim DateDeb2 As Date = DateDeb
                Dim LesDates(100) As String
                Dim LesMonts(100) As String
                Dim Cptr As Decimal = 0
                Dim MontActiv As Decimal = MontAct

                While (DateTime.Compare(DateDeb2, DateFin) <= 0)
                    Dim Jr As String = Mid(DateDeb2.ToString, 1, 2)
                    Dim Ms As String = Mid(DateDeb2.ToString, 4, 2)
                    If (Jr = JrValid And Ms = MsValid) Then
                        LesDates(Cptr) = DateDeb2.ToString
                        Cptr = Cptr + 1
                        DateDeb2 = DateDeb2.AddYears(1)
                    Else
                        DateDeb2 = DateDeb2.AddDays(1)
                    End If

                End While

                If (Cptr <> 0) Then
                    Dim PartEch As Decimal = MontActiv / Cptr
                    Dim MontEch As Decimal = Math.Round(PartEch, 0)
                    Dim RestEch As Decimal = MontActiv - (Cptr * MontEch)

                    For i As Integer = 0 To Cptr - 1
                        If (i = 0) Then
                            EnregistrerEcheance(CDate(LesDates(i)), (MontEch + RestEch).ToString, CodeBailleur, True)
                        Else
                            EnregistrerEcheance(CDate(LesDates(i)), (MontEch).ToString, CodeBailleur, True)
                        End If
                    Next

                Else
                    SuccesMsg("Répartition impossible.")
                    Exit Sub
                End If

            Else
                SuccesMsg("Veuillez définir le jour et le mois de validation de la prévision.")
                Exit Sub
            End If

        End If

    End Sub

    Private Sub EnregistrerEcheance(ByVal DateEch As Date, ByVal MontRep As String, CodeBailleur As String, Optional CreerPlan As Boolean = False)

        Try
            If Not CreerPlan Then
                query = "select SUM(MontantEcheance) from T_EcheanceActivite where CodePartition='" & CodPartit & "' AND CodeBailleur='" & CodeBailleur & "'"
                Dim MontantTotalReparti As Decimal = Val(ExecuteScallar(query))
                Dim drx = ViewEcheanceActivite.GetFocusedRow
                Dim MontantTotalActivite As Decimal = CDec(drx("Montant total (FCFA)").ToString().Replace(" ", "").Replace(Chr(160), ""))
                Dim ResteARepartir As Decimal = MontantTotalActivite - MontantTotalReparti
                Dim NewMontant As Decimal = CDec(TxtMontantEcheance.Text.Replace(" ", "").Replace(Chr(160), ""))
                If NewMontant > ResteARepartir Then
                    FailMsg("Le montant restant est de " & AfficherMonnaie(ResteARepartir))
                    TxtMontantEcheance.Select()
                    Exit Sub
                End If
                query = "SELECT COUNT(*) FROM t_echeanceactivite WHERE CodePartition='" & CodPartit & "' AND CodeBailleur='" & CodeBailleur & "' AND STR_TO_DATE(DateEcheance,'%d/%m/%Y')='" & dateconvert(CDate(DTDateEcheance.DateTime).ToShortDateString) & "'"
                If Val(ExecuteScallar(query)) > 0 Then
                    SuccesMsg("Une répartition à la date " & CDate(DTDateEcheance.DateTime).ToShortDateString & " existe déjà.")
                    Exit Sub
                End If
            End If

            query = "INSERT INTO T_EcheanceActivite VALUES(NULL,'" & CodPartit & "','" & DateEch.ToShortDateString & "','" & MontRep & "','" & CodeBailleur & "')"
            ExecuteNonQuery(query)

            RemplirEchMontant(CodPartit, MontAct.ToString, CodeBailleur)

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try

    End Sub
    Private Sub ActiverActionRepartition()
        If Not RdMois.Enabled Then RdMois.Enabled = True
        If Not RdAns.Enabled Then RdAns.Enabled = True
        If Not RdAutre.Enabled Then RdAutre.Enabled = True
        If Not RdMois.Checked And Not RdAns.Checked And Not RdAutre.Checked Then
            If BtCreerPlan.Visible Then BtCreerPlan.Visible = False
        Else
            If Not BtCreerPlan.Visible Then BtCreerPlan.Visible = True
        End If
    End Sub
    Private Sub InitBoutEcheance()
        TxtTitre.Enabled = False
        RdMois.Enabled = False
        RdAns.Enabled = False
        RdAutre.Enabled = False
        BtAjouterEcheance.Enabled = False

        TxtJour.Visible = False
        CmbJour.Visible = False
        TxtMois.Visible = False
        CmbMois.Visible = False
        BtCreerPlan.Visible = False

        txtGAP.ResetText()
        TxtResteEcheance.ResetText()

        TxtResteEcheance.Text = ""

        DTDateEcheance.Enabled = False
        TxtMontantEcheance.ResetText()
        'If SplitContainerControl1.Panel2.Enabled Then SplitContainerControl1.Panel2.Enabled = False
    End Sub

    Private Sub RdMois_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdMois.CheckedChanged
        If (RdMois.Checked = True) Then
            TxtJour.Visible = True
            CmbJour.Visible = True
            BtCreerPlan.Visible = True
        Else
            TxtJour.Visible = False
            CmbJour.Visible = False
            BtCreerPlan.Visible = False
        End If
    End Sub

    Private Sub RdAns_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdAns.CheckedChanged
        If (RdAns.Checked = True) Then
            TxtJour.Visible = True
            CmbJour.Visible = True
            TxtMois.Visible = True
            CmbMois.Visible = True
            BtCreerPlan.Visible = True
        Else
            TxtJour.Visible = False
            CmbJour.Visible = False
            TxtMois.Visible = False
            CmbMois.Visible = False
            BtCreerPlan.Visible = False
        End If
    End Sub

    Private Sub RdAutre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdAutre.CheckedChanged
        If (RdAutre.Checked = True) Then
            If cmbBailleur.SelectedIndex > 0 Then
                BtAjouterEcheance.Enabled = True
            End If
        Else
            BtAjouterEcheance.Enabled = False
            DTDateEcheance.Enabled = False
            TxtMontantEcheance.Enabled = False
        End If
    End Sub

    Private Sub BtAjouterEcheance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjouterEcheance.Click
        DTDateEcheance.Enabled = True
        DTDateEcheance.Focus()
        If (RdAutre.Checked = True) Then
            TxtMontantEcheance.Enabled = True
            TxtMontantEcheance.ResetText()
        End If
    End Sub


    Private Sub TxtMontantEcheance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontantEcheance.Click
        If (TxtMontantEcheance.Text = "0") Then
            TxtMontantEcheance.Text = ""
            TxtMontantEcheance.Select()
        End If
    End Sub

    Private Sub TxtMontantEcheance_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtMontantEcheance.KeyUp
        If CodeBailleur = "-1" Or CodeBailleur = "Not" Then
            Exit Sub
        End If
        If Val(TxtMontantEcheance.Text.Trim()) = 0 Then
            Exit Sub
        End If
        If (TxtMontantEcheance.Text <> "") Then

            If (e.KeyCode = Keys.Enter) Then
                Dim RepM As DialogResult = ConfirmMsg("Voulez-vous enregistrer?")
                If (RepM = DialogResult.Yes) Then

                    Dim NewMontant As Decimal = CDec(TxtMontantEcheance.Text.Replace(" ", "").Replace(Chr(160), ""))
                    EnregistrerEcheance(CDate(DTDateEcheance.DateTime).ToShortDateString, NewMontant, CodeBailleur)

                    TxtMontantEcheance.Text = ""
                    DTDateEcheance.Focus()
                End If
            End If

        End If

    End Sub

    Private Sub cmbBailleur_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBailleur.SelectedIndexChanged
        If cmbBailleur.SelectedIndex >= 0 Then
            CodeBailleur = tabCodeBailleurs(cmbBailleur.SelectedIndex)
        Else
            CodeBailleur = "-1"
            InitBoutEcheance()
        End If
        LoadActivite(CodeBailleur)

    End Sub

    Private Sub MenuStripRepartition_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MenuStripRepartition.Opening
        If ViewEcheanceMontant.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub SupprimerPJ_Click(sender As Object, e As EventArgs) Handles SupprimerPJ.Click
        If ConfirmMsg("Voulez-vous supprimer la répartition sélectionnée ?") = DialogResult.Yes Then
            Dim drx = ViewEcheanceMontant.GetFocusedRow
            query = "DELETE FROM t_echeanceactivite WHERE RefEcheance='" & drx("Ref") & "'"
            ExecuteNonQuery(query)
            RemplirEchMontant(CodPartit, MontAct, CodeBailleur)
        End If
    End Sub

    Private Sub GridEcheanceMontant_Click(sender As Object, e As EventArgs) Handles GridEcheanceMontant.Click
        If (ViewEcheanceMontant.RowCount > 0) Then
            Dim Ligne = ViewEcheanceMontant.GetDataRow(ViewEcheanceMontant.FocusedRowHandle)
            Dim RefRepartition As Decimal = Ligne("Ref")
            ColorRowGrid(ViewEcheanceMontant, "[CodeX]='x'", Color.LightBlue, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewEcheanceMontant, "[Ref]='" & RefRepartition & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)

        End If
    End Sub

End Class