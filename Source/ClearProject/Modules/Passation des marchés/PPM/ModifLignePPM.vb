Public Class ModifLignePPM
    Dim dtBailleur As New DataTable()
    Public IDPlan As Decimal = 0
    Public RefMarcheMod As Decimal = 0
    Public ConventionChefFils As String = ""
    Dim CodeProcAO() As Decimal
    Dim CodeAncienneMethode As String = ""
    Dim AncienneRevu As String = ""

    Private Sub SaisiePPSD_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RemplirBailleur()
        dtBailleur.Columns.Clear()
        dtBailleur.Columns.Add("CodeX", Type.GetType("System.String"))
        dtBailleur.Columns.Add("Ref", Type.GetType("System.String"))
        dtBailleur.Columns.Add("Bailleur", Type.GetType("System.String"))
        dtBailleur.Columns.Add("Convention", Type.GetType("System.String"))
        dtBailleur.Columns.Add("Montant", Type.GetType("System.String"))
        dtBailleur.Columns.Add("%", Type.GetType("System.String"))

        query = "SELECT * FROM t_marche WHERE RefMarche='" & RefMarcheMod & "' AND RefPPM ='" & IDPlan & "' AND CodeProjet='" & ProjetEnCours & "'"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            TxtDesc.Text = MettreApost(rw("DescriptionMarche").ToString)
            txtMontant.Text = rw("MontantEstimatif")
            cmbTypeExamen.Text = rw("RevuePrioPost").ToString
            CodeAncienneMethode = rw("CodeProcAO").ToString
            AncienneRevu = rw("RevuePrioPost").ToString
            'RemplirMehtodeMarche(rw("TypeMarche").ToString)

            query = "select CodeProcAO,AbregeAO from t_procao WHERE TypeMarcheAO='" & rw("TypeMarche").ToString & "'"
            cmbMethode.Properties.Items.Clear()
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            ReDim CodeProcAO(dt1.Rows.Count)
            Dim i As Integer = 0
            For Each rw1 As DataRow In dt1.Rows
                cmbMethode.Properties.Items.Add(MettreApost(rw1("AbregeAO").ToString))
                If rw1("CodeProcAO").ToString = rw("CodeProcAO").ToString Then
                    cmbMethode.Text = rw1("AbregeAO").ToString
                End If
                CodeProcAO(i) = rw1("CodeProcAO").ToString
                i += 1
            Next
        Next

        query = "SELECT * FROM t_ppm_repartitionbailleur WHERE RefMarche='" & RefMarcheMod & "' AND RefPPM ='" & IDPlan & "'"
        Dim dt2 = ExcecuteSelectQuery(query)
        For Each rw2 In dt2.Rows
            Dim bailleur = ExecuteScallar("SELECT B.InitialeBailleur FROM t_bailleur as B, t_convention as C WHERE C.CodeBailleur=B.CodeBailleur AND C.CodeConvention='" & rw2("CodeConvention").ToString.Trim & "'")
            Dim Pourcentage = Math.Round(CDec(rw2("Montant").ToString) * 100 / CDec(txtMontant.Text.ToString.Trim), 2)
            RemplirRepart(bailleur, rw2("CodeConvention").ToString, rw2("Montant").ToString, Pourcentage, txtMontant.Text)
        Next

        If dt2.Rows.Count = 0 Then
            query = "SELECT InitialeBailleur, CodeConvention, MontantEstimatif FROM t_marche WHERE RefMarche='" & RefMarcheMod & "' AND RefPPM ='" & IDPlan & "' AND CodeProjet='" & ProjetEnCours & "'"
            Dim dt3 = ExcecuteSelectQuery(query)
            For Each rw3 In dt3.Rows
                Dim Pourcentage = Math.Round(CDec(rw3("MontantEstimatif").ToString) * 100 / CDec(txtMontant.Text.ToString.Trim), 2)
                RemplirRepart(rw3("InitialeBailleur").ToString, rw3("CodeConvention").ToString, rw3("MontantEstimatif").ToString, Pourcentage, rw3("MontantEstimatif").ToString)
            Next
        End If
    End Sub

    Private Sub RemplirBailleur()
        query = "select InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' order by InitialeBailleur"
        CmbBailleur.Properties.Items.Clear()
        CmbBailleur.Text = ""
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbBailleur.Properties.Items.Add(rw("InitialeBailleur"))
        Next
    End Sub

    Private Sub ChargerConvention(ByVal bail As String)
        CmbConv.Text = ""
        query = "select CodeConvention from T_Convention where CodeBailleur='" & bail & "' order by CodeConvention"
        CmbConv.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbConv.Properties.Items.Add(rw("CodeConvention").ToString)
        Next
        'CmbConv.SelectedIndex = 0
    End Sub

    Private Sub CmbBailleur_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbBailleur.SelectedIndexChanged
        query = "select CodeBailleur, InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' and InitialeBailleur='" & CmbBailleur.Text & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                CodeBailleurCache.Text = rw(0)
            Next
            ChargerConvention(CodeBailleurCache.Text)
            TxtMontBailleur.Text = 0
        Else
            CmbConv.Text = ""
            CmbConv.Properties.Items.Clear()
        End If
    End Sub

    Private Sub TxtMontBailleur_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtMontBailleur.KeyDown
        If (e.KeyCode = Keys.Enter And TxtMontBailleur.ForeColor <> Color.Red And CmbConv.Text <> "" And CmbBailleur.Text <> "" And CodeBailleurCache.Text <> "") Then
            For i = 0 To ViewRepartBailleur.RowCount - 1
                If ViewRepartBailleur.GetRowCellValue(i, "Bailleur").ToString = CmbBailleur.Text Then
                    SuccesMsg("Ce bailleur est déjà enregistré.")
                    Exit Sub
                End If
            Next

            RemplirRepart(CmbBailleur.Text, CmbConv.Text, TxtMontBailleur.Text.Replace(" ", ""), TxtPrct.Text, AfficherMonnaie(CDec(TxtMontAffecte.Text) + CDec(TxtMontBailleur.Text.Replace(" ", ""))))
            CmbBailleur.Focus()
        ElseIf (e.KeyCode = Keys.Enter And (TxtMontBailleur.ForeColor = Color.Red Or CmbConv.Text = "" Or CmbBailleur.Text = "")) Then
            FailMsg("Données incorrectes.")
        End If
    End Sub
    Private Sub RemplirRepart(ByVal Bailleur As String, ByVal Convention As String, ByVal MontBailleur As String, ByVal Pourcentage As String, ByVal MontAffecte As String)
        Dim drS = dtBailleur.NewRow()
        Dim cpt As Decimal = 0
        drS("CodeX") = cpt
        drS("Ref") = "0"
        drS("Bailleur") = Bailleur
        drS("Convention") = Convention
        drS("Montant") = MontBailleur
        drS("%") = Pourcentage

        TxtMontAffecte.Text = AfficherMonnaie(MontAffecte)
        dtBailleur.Rows.Add(drS)
        GridRepartBailleur.DataSource = dtBailleur
        cpt = cpt + 1

        ViewRepartBailleur.Columns(0).Visible = False
        ViewRepartBailleur.Columns(1).Visible = False
        ViewRepartBailleur.Columns(2).Width = 84
        ViewRepartBailleur.Columns(3).Width = 172
        ViewRepartBailleur.Columns(4).Width = 145
        ViewRepartBailleur.Columns(5).Width = 48
        ViewRepartBailleur.OptionsView.ColumnAutoWidth = True
        ViewRepartBailleur.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewRepartBailleur.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewRepartBailleur.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        ViewRepartBailleur.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewRepartBailleur, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub
    Private Sub TxtMontBailleur_EditValueChanged(sender As Object, e As EventArgs) Handles TxtMontBailleur.EditValueChanged
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
            Prct = Math.Round(CDec((TxtMontBailleur.EditValue) * 100) / CDec(TxtMontTotal.EditValue), 2)
        End If
        Return Prct
    End Function

    Private Sub CalculAffectRest()
        If (TxtMontAffecte.Text <> "" And TxtMontTotal.Text.Replace(" ", "") <> "") Then
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
    Private Sub TxtMontAffecte_EditValueChanged(sender As Object, e As EventArgs) Handles TxtMontAffecte.EditValueChanged
        CalculAffectRest()
    End Sub
    Private Sub TxtMontTotal_EditValueChanged(sender As Object, e As EventArgs) Handles TxtMontTotal.EditValueChanged
        CalculAffectRest()
    End Sub

    Private Sub txtMontant_EditValueChanged(sender As Object, e As EventArgs) Handles txtMontant.EditValueChanged
        TxtMontTotal.Text = AfficherMonnaie(IIf(txtMontant.Text <> "", txtMontant.Text, 0))
    End Sub
    Private Sub SupprimerLaLigne_Click(sender As Object, e As EventArgs) Handles SupprimerLaLigne.Click
        If ViewRepartBailleur.RowCount > 0 Then
            TxtMontAffecte.Text = AfficherMonnaie(CDec(TxtMontAffecte.Text) - CDec(ViewRepartBailleur.GetRowCellValue(ViewRepartBailleur.FocusedRowHandle, "Montant").ToString.Replace(" ", "")))
            ViewRepartBailleur.DeleteRow(ViewRepartBailleur.FocusedRowHandle)
        End If
    End Sub

    Private Sub BtEnrgPPSD_Click(sender As Object, e As EventArgs) Handles BtEnrgPPSD.Click
        Dim erreur As String = ""
        'si la description de la ligne n'est pas renseigné
        If TxtDesc.IsRequiredControl("Veuillez renseigner la description.") Then
            TxtDesc.Select()
            Exit Sub
        End If
        'si le type d'examen n'est pas renseigné      
        If cmbTypeExamen.IsRequiredControl("Veuillez choisir le type d'examen dans la liste.") Then
            cmbTypeExamen.Select()
            Exit Sub
        End If
        'si le montant de la ligne n'est pas renseigné            
        If txtMontant.IsRequiredControl("Veuillez renseigner le montant.") Then
            txtMontant.Select()
            Exit Sub
        End If
        'si le montant de la ligne n'est pas renseigné            
        If cmbMethode.IsRequiredControl("Veuillez choisir la méthode de passation des marchés dans la liste.") Then
            cmbMethode.Select()
            Exit Sub
        End If
        'si  la repartition du montant n'est pas renseigné
        If CDec(IIf(txtMontant.Text = "", 0, txtMontant.Text)) <> IIf(TxtMontAffecte.Text = "", 0, CDec(TxtMontAffecte.Text.ToString.Replace(" ", ""))) Then
            erreur = "La repartition du montant n'est pas correct" + ControlChars.CrLf
        End If
        Dim Pourcentage As Decimal = 0
        For i = 0 To ViewRepartBailleur.RowCount - 1
            Pourcentage += CDec(ViewRepartBailleur.GetRowCellValue(i, "%"))
        Next
        If Pourcentage < 100 Or Pourcentage > 100 Then
            erreur = "La repartition du montant n'est pas correct" + ControlChars.CrLf
        End If

        If erreur = "" Then
            'Savoir si la revue ou la méthode de passation du marche en cours de modification a changé
            If (CodeAncienneMethode <> CodeProcAO(cmbMethode.SelectedIndex)) Or (cmbTypeExamen.Text <> AncienneRevu) Then

                'Verifier s'il existe des date de prevision ou de realisation des etapes
                If Val(ExecuteScallar("SELECT COUNT(*) from t_planmarche where RefMarche='" & RefMarcheMod & "'")) > 0 Then
                    'Personnalisation du message
                    Dim Message As String = ""
                    If (CodeAncienneMethode <> CodeProcAO(cmbMethode.SelectedIndex)) And (cmbTypeExamen.Text <> AncienneRevu) Then
                        Message = "Vous avez changer la revue et la méthode du marché. Si vous continuer toutes les dates de prévisions et de réalisations des étapes de ce marché seront supprimer." & vbNewLine & "Êtes-vous sûrs de vouloir continuer?"
                    ElseIf (CodeAncienneMethode <> CodeProcAO(cmbMethode.SelectedIndex)) Then
                        Message = "Vous avez changer la méthode du marché. Si vous continuer toutes les dates de prévisions et de réalisations des étapes de ce marché seront supprimer." & vbNewLine & "Êtes-vous sûrs de vouloir continuer?"
                    Else
                        Message = "Vous avez changer la revue du marché. Si vous continuer toutes les dates de prévisions et de réalisations des étapes de ce marché seront supprimer." & vbNewLine & "Êtes-vous sûrs de vouloir continuer?"
                    End If

                    If ConfirmMsg(Message) = DialogResult.No Then
                        Exit Sub
                    End If
                    ExecuteNonQuery("delete from t_planmarche WHERE RefMarche='" & RefMarcheMod & "'")
                End If
            End If
            DebutChargement(True, "Traitement des modifications en cours...")

            ExecuteNonQuery("DELETE FROM t_ppm_repartitionbailleur WHERE RefPPM='" & IDPlan & "' AND RefMarche='" & RefMarcheMod & "'")

            Dim lesBailleurs As String = ViewRepartBailleur.GetRowCellValue(0, "Bailleur").ToString
            Dim lesConventions As String = ViewRepartBailleur.GetRowCellValue(0, "Convention").ToString
            ' Dim montantSup As Decimal = ViewRepartBailleur.GetRowCellValue(0, "Montant").ToString.Replace(" ", "")
            Dim ChefFile As String = ViewRepartBailleur.GetRowCellValue(0, "Convention").ToString
            ExecuteNonQuery("insert into t_ppm_repartitionbailleur values(NULL,'" & IDPlan & "','" & RefMarcheMod & "','" & ViewRepartBailleur.GetRowCellValue(0, "Convention").ToString & "','" & ViewRepartBailleur.GetRowCellValue(0, "Montant").ToString.Replace(" ", "") & "')")

            If ViewRepartBailleur.RowCount > 1 Then
                For i = 1 To ViewRepartBailleur.RowCount - 1
                    ExecuteNonQuery("insert into t_ppm_repartitionbailleur values(NULL,'" & IDPlan & "','" & RefMarcheMod & "','" & ViewRepartBailleur.GetRowCellValue(i, "Convention").ToString & "','" & ViewRepartBailleur.GetRowCellValue(i, "Montant").ToString.Replace(" ", "") & "')")
                    lesBailleurs = lesBailleurs & " | " & ViewRepartBailleur.GetRowCellValue(i, "Bailleur").ToString
                    lesConventions = lesConventions & " | " & ViewRepartBailleur.GetRowCellValue(i, "Convention").ToString

                    'If ViewRepartBailleur.GetRowCellValue(i, "Montant").ToString > montantSup Then
                    '    montantSup = ViewRepartBailleur.GetRowCellValue(i, "Montant").ToString.Replace(" ", "")
                    '    ChefFile = ViewRepartBailleur.GetRowCellValue(i, "Convention").ToString
                    'End If
                Next
            End If

            ExecuteNonQuery("UPDATE t_marche SET DescriptionMarche='" & EnleverApost(TxtDesc.Text) & "', MontantEstimatif='" & txtMontant.Text.Replace(" ", "") & "', RevuePrioPost='" & EnleverApost(cmbTypeExamen.Text) & "', InitialeBailleur='" & lesBailleurs & "', CodeConvention='" & lesConventions & "', Convention_ChefFile='" & ConventionChefFils & "', CodeProcAO='" & CodeProcAO(cmbMethode.SelectedIndex) & "', DerniereMaj='" & Now.ToString & "' WHERE RefPPM='" & IDPlan & "' AND RefMarche='" & RefMarcheMod & "'")
            FinChargement()
            SuccesMsg("Ligne modifiée avec succès.")

            'PlanMarche.cmbDevise.Text = "US$"
            'PlanMarche.RemplirMarcheAConsulter()
            'query = "SELECT max(RefPPM), LibellePPM FROM t_ppm_marche WHERE CodeProjet='" & ProjetEnCours & "'"
            'Dim dtPPM As DataTable = ExcecuteSelectQuery(query)
            'For Each rwPPM As DataRow In dtPPM.Rows
            '    PlanMarche.MarcheAConsulter.Text = (MettreApost(rwPPM("LibellePPM")))
            'Next
            Me.Close()
            PlanMarche.BtActualiserPlan.PerformClick()
        Else
            SuccesMsg(erreur)
        End If
    End Sub
    Private Sub btAnnuler_Click(sender As Object, e As EventArgs) Handles btAnnuler.Click
        Me.Close()
    End Sub
End Class