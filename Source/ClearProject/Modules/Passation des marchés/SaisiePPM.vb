Imports MySql.Data.MySqlClient
Imports System.IO
Imports ClearProject.PassationMarche
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraTreeList.Nodes

Public Class SaisiePPM
    Dim dtBailleur As New DataTable()
    Dim dtLignesPPM As New DataTable()
    Dim Nbre As Integer
    Dim AjoutLigne As Boolean = True
    Dim indexLigne As Integer
    Public IDPlan As Integer = -1

    Private Sub SaisiePPSD_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        If IDPlan = -1 Then
            InitForm()
            RemplirTypeMarche()
        Else
            query = "SELECT * FROM t_ppm_marche WHERE RefPPM='" & IDPlan & "' AND CodeProjet='" & ProjetEnCours & "'"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                cmbTypeMarche.Text = rw("TypeMarche").ToString
                DateDebutMarche.Text = CDate(rw("PeriodePlan").ToString.Split("-")(0).Trim)
                DateFinMarche.Text = CDate(rw("PeriodePlan").ToString.Split("-")(1).Trim)
                txtNumeroPlan.Text = rw("NumeroPlan").ToString
            Next
            cmbTypeMarche.Enabled = False
            DateDebutMarche.Enabled = False
            DateFinMarche.Enabled = False
            RemplirMehtodeMarche(cmbTypeMarche.Text)
        End If
        RemplirBailleur()
        Nbre = 1
        dtBailleur.Columns.Clear()
        dtBailleur.Columns.Add("CodeX", Type.GetType("System.String"))
        dtBailleur.Columns.Add("Ref", Type.GetType("System.String"))
        dtBailleur.Columns.Add("Bailleur", Type.GetType("System.String"))
        dtBailleur.Columns.Add("Convention", Type.GetType("System.String"))
        dtBailleur.Columns.Add("Montant", Type.GetType("System.String"))
        dtBailleur.Columns.Add("%", Type.GetType("System.String"))

        dtLignesPPM.Columns.Clear()
        dtLignesPPM.Columns.Add("N°", Type.GetType("System.String"))
        dtLignesPPM.Columns.Add("Description", Type.GetType("System.String"))
        dtLignesPPM.Columns.Add("Montant estimatif", Type.GetType("System.String"))
        dtLignesPPM.Columns.Add("Type examen", Type.GetType("System.String"))
        dtLignesPPM.Columns.Add("Méthodes de passation des marchés", Type.GetType("System.String"))
        dtLignesPPM.Columns.Add("Bailleur", Type.GetType("System.String"))
        dtLignesPPM.Columns.Add("Conventions", Type.GetType("System.String"))
        dtLignesPPM.Columns.Add("ChefFile", Type.GetType("System.String"))
        dtLignesPPM.Columns.Add("Montant bailleur", Type.GetType("System.String"))

    End Sub
    Private Sub RemplirTypeMarche()
        query = "select TypeMarche from T_TypeMarche order by TypeMarche"
        cmbTypeMarche.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cmbTypeMarche.Properties.Items.Add(MettreApost(rw("TypeMarche").ToString))
        Next
    End Sub
    Private Sub RemplirMehtodeMarche(ByVal TypeMarche As String)
        query = "select AbregeAO from t_procao WHERE TypeMarcheAO='" & TypeMarche & "'"
        cmbMethode.Text = ""
        cmbMethode.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cmbMethode.Properties.Items.Add(MettreApost(rw("AbregeAO").ToString))
        Next
    End Sub
    Private Sub cmbTypeMarche_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTypeMarche.SelectedIndexChanged
        InitLigneAjouter()
        dtLignesPPM.Rows.Clear()
        Nbre = 1
        If cmbTypeMarche.SelectedIndex <> -1 Then
            RemplirMehtodeMarche(EnleverApost(cmbTypeMarche.Text))
        Else
            cmbMethode.Properties.Items.Clear()
            cmbMethode.Text = ""
        End If
    End Sub
    Private Sub RemplirBailleur()
        query = "select InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' order by InitialeBailleur"
        CmbBailleur.Properties.Items.Clear()
        CmbBailleur.Text = ""
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbBailleur.Properties.Items.Add(rw(0))
        Next
    End Sub
    Private Sub ChargerConvention(ByVal bail As String)
        CmbConv.Text = ""
        query = "select CodeConvention from T_Convention where CodeBailleur='" & bail & "' order by CodeConvention"
        CmbConv.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbConv.Properties.Items.Add(rw(0).ToString)
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

            'If boolmodif = True Then
            'Else
            '    If (VerifAllocationEtConvention() = False) Then
            '        Exit Sub
            '    End If
            'End If
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
        'drS(0) = cpt
        'drS(1) = "0"
        'drS(2) = CmbBailleur.Text
        'drS(3) = CmbConv.Text
        'drS(4) = TxtMontBailleur.Text.Replace(" ", "")
        'drS(5) = TxtPrct.Text
        'TxtMontAffecte.Text = AfficherMonnaie(CDec(TxtMontAffecte.Text) + CDec(TxtMontBailleur.Text.Replace(" ", "")))
        drS(0) = cpt
        drS(1) = "0"
        drS(2) = Bailleur
        drS(3) = Convention
        drS(4) = MontBailleur
        drS(5) = Pourcentage
        TxtMontAffecte.Text = MontAffecte
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
    Private Sub TxtMontAffecte_EditValueChanged(sender As Object, e As EventArgs) Handles TxtMontAffecte.EditValueChanged
        CalculAffectRest()
    End Sub
    Private Sub TxtMontTotal_EditValueChanged(sender As Object, e As EventArgs) Handles TxtMontTotal.EditValueChanged
        CalculAffectRest()
    End Sub

    Private Sub txtMontant_EditValueChanged(sender As Object, e As EventArgs) Handles txtMontant.EditValueChanged
        TxtMontTotal.Text = txtMontant.Text
    End Sub
    Private Sub InitForm()
        AjoutLigne = True
        Nbre = 1
        txtMontant.Text = ""
        txtNumeroPlan.Text = ""
        TxtMontAffecte.Text = 0
        TxtMontRestant.Text = 0
        TxtMontBailleur.Text = ""
        TxtMontTotal.Text = 0
        dtBailleur.Rows.Clear()
        TxtPrct.Text = "0,00"
        TxtDesc.Text = ""
        cmbTypeExamen.Text = ""
        cmbTypeMarche.Text = ""
        CmbBailleur.Text = ""
        CmbConv.Properties.Items.Clear()
        CmbConv.Text = ""
        DateDebutMarche.Text = ""
        DateFinMarche.Text = ""
        dtBailleur.Columns.Clear()
        dtBailleur.Columns.Add("CodeX", Type.GetType("System.String"))
        dtBailleur.Columns.Add("Ref", Type.GetType("System.String"))
        dtBailleur.Columns.Add("Bailleur", Type.GetType("System.String"))
        dtBailleur.Columns.Add("Convention", Type.GetType("System.String"))
        dtBailleur.Columns.Add("Montant", Type.GetType("System.String"))
        dtBailleur.Columns.Add("%", Type.GetType("System.String"))
        dtLignesPPM.Rows.Clear()
        dtBailleur.Rows.Clear()

    End Sub
    Private Sub SupprimerLaLigne_Click(sender As Object, e As EventArgs) Handles SupprimerLaLigne.Click
        If ViewRepartBailleur.RowCount > 0 Then
            TxtMontAffecte.Text = AfficherMonnaie(CDec(TxtMontAffecte.Text) - CDec(ViewRepartBailleur.GetRowCellValue(ViewRepartBailleur.FocusedRowHandle, "Montant").ToString.Replace(" ", "")))
            ViewRepartBailleur.DeleteRow(ViewRepartBailleur.FocusedRowHandle)
        End If
    End Sub
    Private Sub btAjoutLigne_Click(sender As Object, e As EventArgs) Handles btAjoutLigne.Click
        If AjoutLigne = True Then
            Dim erreur As String = ""
            Dim erreur1 As String = ""

            'si la description de la ligne n'est pas renseigné
            If TxtDesc.Text = "" Then
                erreur += "- Description " + ControlChars.CrLf
            End If
            'si le type d'examen n'est pas renseigné
            If cmbTypeExamen.SelectedIndex = -1 Then
                erreur += "- Type examen" + ControlChars.CrLf
            End If
            'si le montant de la ligne n'est pas renseigné
            If txtMontant.Text = "" Then
                erreur += "- Montant " + ControlChars.CrLf
            End If
            'si  la methode de passation n'est pas renseigné
            If cmbMethode.SelectedIndex = -1 Then
                erreur += "- Methode de passation" + ControlChars.CrLf
            End If
            'si  la repartition du montant n'est pas renseigné
            If CDec(IIf(txtMontant.Text = "", 0, txtMontant.Text)) <> IIf(TxtMontAffecte.Text = "", 0, CDec(TxtMontAffecte.Text.ToString.Replace(" ", ""))) Then
                erreur1 = "La repartition du montant n'est pas correct" + ControlChars.CrLf
            End If
            Dim Pourcentage As Decimal = 0
            For i = 0 To ViewRepartBailleur.RowCount - 1
                Pourcentage += CDec(ViewRepartBailleur.GetRowCellValue(i, "%"))
            Next
            If Pourcentage.ToString <> "100,00" Then
                erreur1 = "La repartition du montant n'est pas correct" + ControlChars.CrLf
            End If
            If erreur = "" And erreur1 = "" Then
                RemplirPPM()
                InitLigneAjouter()
            Else
                If erreur <> "" Then
                    SuccesMsg("Veuillez remplir ces champs : " + ControlChars.CrLf + erreur)
                    Exit Sub
                End If
                If erreur1 <> "" Then
                    SuccesMsg(erreur1)
                End If
            End If
        Else
            Dim erreur As String = ""
            Dim erreur1 As String = ""

            'si la description de la ligne n'est pas renseigné
            If TxtDesc.Text = "" Then
                erreur += "- Description " + ControlChars.CrLf
            End If
            'si le type d'examen n'est pas renseigné
            If cmbTypeExamen.SelectedIndex = -1 Then
                erreur += "- Type examen" + ControlChars.CrLf
            End If
            'si le montant de la ligne n'est pas renseigné
            If txtMontant.Text = "" Then
                erreur += "- Montant " + ControlChars.CrLf
            End If
            'si  la methode de passation n'est pas renseigné
            If cmbMethode.SelectedIndex = -1 Then
                erreur += "- Methode de passation" + ControlChars.CrLf
            End If
            'si  la repartition du montant n'est pas renseigné
            If CDec(IIf(txtMontant.Text = "", 0, txtMontant.Text)) <> IIf(TxtMontAffecte.Text = "", 0, CDec(TxtMontAffecte.Text.ToString.Replace(" ", ""))) Then
                erreur1 = "La repartition du montant n'est pas correct" + ControlChars.CrLf
            End If
            Dim Pourcentage As Decimal = 0
            For i = 0 To ViewRepartBailleur.RowCount - 1
                Pourcentage += CDec(ViewRepartBailleur.GetRowCellValue(i, "%"))
            Next
            If Pourcentage < 100 Or Pourcentage > 100 Then
                erreur1 = "La repartition du montant n'est pas correct" + ControlChars.CrLf
            End If
            If erreur = "" And erreur1 = "" Then
                Dim lesBailleurs As String = ""
                Dim lesConventions As String = ""
                Dim lesMontantConv As String = ""
                Dim montantSup As Integer = 0
                Dim ChefFile As String = ""
                For i = 0 To ViewRepartBailleur.RowCount - 1
                    If ViewRepartBailleur.RowCount <= 1 Then
                        lesBailleurs = ViewRepartBailleur.GetRowCellValue(i, "Bailleur").ToString
                        lesConventions = ViewRepartBailleur.GetRowCellValue(i, "Convention").ToString
                        lesMontantConv = ViewRepartBailleur.GetRowCellValue(i, "Montant").ToString
                        ChefFile = ViewRepartBailleur.GetRowCellValue(i, "Convention").ToString
                    Else
                        lesBailleurs += ViewRepartBailleur.GetRowCellValue(i, "Bailleur").ToString & " | "
                        lesConventions += ViewRepartBailleur.GetRowCellValue(i, "Convention").ToString & " | "
                        lesMontantConv += ViewRepartBailleur.GetRowCellValue(i, "Convention").ToString & "&" & ViewRepartBailleur.GetRowCellValue(i, "Montant").ToString & " | "
                        If ViewRepartBailleur.GetRowCellValue(i, "Montant").ToString > montantSup Then
                            montantSup = ViewRepartBailleur.GetRowCellValue(i, "Montant").ToString
                            ChefFile = ViewRepartBailleur.GetRowCellValue(i, "Convention").ToString
                        End If
                    End If
                Next
                ViewPPM.SetRowCellValue(indexLigne, "Description", TxtDesc.Text)
                ViewPPM.SetRowCellValue(indexLigne, "Montant estimatif", txtMontant.Text)
                ViewPPM.SetRowCellValue(indexLigne, "Type examen", cmbTypeExamen.Text)
                ViewPPM.SetRowCellValue(indexLigne, "Méthodes de passation des marchés", cmbMethode.Text)
                ViewPPM.SetRowCellValue(indexLigne, "Bailleur", lesBailleurs)
                ViewPPM.SetRowCellValue(indexLigne, "Conventions", lesConventions)
                ViewPPM.SetRowCellValue(indexLigne, "ChefFile", ChefFile)
                ViewPPM.SetRowCellValue(indexLigne, "Montant bailleur", lesMontantConv)
                InitLigneAjouter()
            Else
                If erreur <> "" Then
                    SuccesMsg("Veuillez remplir ces champs : " + ControlChars.CrLf + erreur)
                    Exit Sub
                End If
                If erreur1 <> "" Then
                    SuccesMsg(erreur1)
                End If
            End If
        End If
    End Sub
    Private Sub InitLigneAjouter()
        AjoutLigne = True
        TxtDesc.Text = ""
        txtMontant.Text = 0
        cmbMethode.Text = ""
        cmbTypeExamen.Text = ""
        TxtMontAffecte.Text = 0
        TxtMontBailleur.Text = 0
        TxtMontRestant.Text = 0
        CmbBailleur.Text = ""
        CmbConv.Text = ""
        TxtPrct.Text = "0,00"
        dtBailleur.Rows.Clear()
    End Sub
    Private Sub RemplirPPM()
        Dim lesBailleurs As String = ""
        Dim lesConventions As String = ""
        Dim lesMontantConv As String = ""
        Dim montantSup As Double = 0
        Dim ChefFile As String = ""
        For i = 0 To ViewRepartBailleur.RowCount - 1
            If ViewRepartBailleur.RowCount <= 1 Then
                lesBailleurs = ViewRepartBailleur.GetRowCellValue(i, "Bailleur").ToString
                lesConventions = ViewRepartBailleur.GetRowCellValue(i, "Convention").ToString
                lesMontantConv = ViewRepartBailleur.GetRowCellValue(i, "Montant").ToString
                ChefFile = ViewRepartBailleur.GetRowCellValue(i, "Convention").ToString
            Else
                lesBailleurs += ViewRepartBailleur.GetRowCellValue(i, "Bailleur").ToString & " | "
                lesConventions += ViewRepartBailleur.GetRowCellValue(i, "Convention").ToString & " | "
                lesMontantConv += ViewRepartBailleur.GetRowCellValue(i, "Convention").ToString & "&" & ViewRepartBailleur.GetRowCellValue(i, "Montant").ToString & " | "
                If ViewRepartBailleur.GetRowCellValue(i, "Montant").ToString > montantSup Then
                    montantSup = ViewRepartBailleur.GetRowCellValue(i, "Montant").ToString
                    ChefFile = ViewRepartBailleur.GetRowCellValue(i, "Convention").ToString
                End If
            End If
        Next
        Dim drS = dtLignesPPM.NewRow()
        'Dim cpt As Decimal = 0
        drS(0) = Nbre
        drS(1) = TxtDesc.Text
        drS(2) = AfficherMonnaie(txtMontant.Text)
        drS(3) = cmbTypeExamen.Text
        drS(4) = cmbMethode.Text
        drS(5) = lesBailleurs
        drS(6) = lesConventions
        drS(7) = ChefFile
        drS(8) = lesMontantConv
        dtLignesPPM.Rows.Add(drS)
        GridPPM.DataSource = dtLignesPPM
        Nbre = Nbre + 1
        ViewPPM.Columns(0).Width = 15
        ViewPPM.Columns("Bailleur").Visible = False
        ViewPPM.Columns("ChefFile").Visible = False
        ViewPPM.Columns("Montant bailleur").Visible = False
        'ViewPPM.Columns(6).Visible = False
        'ViewPPM.Columns(1).Visible = False
        'ViewPPM.Columns(2).Width = 84
        'ViewPPM.Columns(3).Width = 172
        'ViewPPM.Columns(4).Width = 145
        'ViewPPM.Columns(5).Width = 48

        ViewPPM.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewPPM.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        'ViewPPM.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        ViewPPM.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewPPM, "[CodeX]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub

    Private Sub BtEnrgPPSD_Click(sender As Object, e As EventArgs) Handles BtEnrgPPSD.Click
        If IDPlan = -1 Then
            Dim erreur As String = ""
            'si le type de marché n'est pas renseigné
            If txtNumeroPlan.IsRequiredControl("Veuillez renseigner le numero du plan.") Then
                Exit Sub
            End If
            'si le type de marché n'est pas renseigné
            If cmbTypeMarche.IsRequiredControl("Veuillez choisir un type de marché.") Then
                Exit Sub
            End If
            'si la date de début de période n'est pas renseigné
            If DateDebutMarche.IsRequiredControl("Veuillez renseigner la date de début de période.") Then
                Exit Sub
            End If
            'si la date de fin de période n'est pas renseigné
            If DateFinMarche.IsRequiredControl("Veuillez renseigner la date de fin de période.") Then
                Exit Sub
            End If
            If Date.Compare(CDate(DateDebutMarche.Text), CDate(DateFinMarche.Text)) > 0 Then
                erreur += "- La période saisie n'est pas correcte" + ControlChars.CrLf
            End If
            'si le PPM ne contient pas de ligne
            If dtLignesPPM.Rows.Count = 0 Then
                erreur += "- Veuillez ajouter des lignes au PPM" + ControlChars.CrLf
            End If
            If erreur = "" Then
                'si le numéro du plan existe déjà
                query = "SELECT COUNT(NumeroPlan) from t_ppm_marche WHERE NumeroPlan='" & EnleverApost(txtNumeroPlan.Text) & "'"
                Dim dtResult = ExecuteScallar(query)
                If dtResult > 0 Then
                    FailMsg("Le numero du plan saisie existe déjà.")
                    Exit Sub
                End If
                DebutChargement(True, "Enregistrement du plan en cours")
                query = "select PeriodeMarche,DescriptionMarche from T_Marche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='" & cmbTypeMarche.Text & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    Dim GroupDate() As String = (rw("PeriodeMarche").ToString).Split(" "c)
                    Dim PeriodeDebut As Date = CDate(GroupDate(0))
                    Dim PeriodeFin As Date = CDate(GroupDate(2))
                    If ((Date.Compare(CDate(DateDebutMarche.Text), PeriodeDebut) >= 0 And Date.Compare(CDate(DateDebutMarche.Text), PeriodeFin) <= 0) Or (Date.Compare(CDate(DateFinMarche.Text), PeriodeDebut) >= 0 And Date.Compare(CDate(DateFinMarche.Text), PeriodeFin) <= 0)) Then
                        FailMsg("Impossible d'enregistrer ce plan." & vbNewLine & "Soit des marchés existent déjà ou la période chevauche une déjà existante.")
                        Exit Sub
                    End If
                Next
                Dim CodeNewPlan As String = String.Empty
                Dim periode = CDate(DateDebutMarche.Text) & " - " & CDate(DateFinMarche.Text)
                query = "insert into t_ppm_marche values (NULL,'" & EnleverApost(cmbTypeMarche.Text) & "_" & EnleverApost(periode) & "','" & EnleverApost(cmbTypeMarche.Text) & "','" & EnleverApost(periode) & "','Tous',NULL,'PPSD','" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "','" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "','" & ProjetEnCours & "','" & CodeUtilisateur & "')"
                ExecuteNonQuery(query)
                CodeNewPlan = ExecuteScallar("SELECT MAX(RefPPM) FROM t_ppm_marche")
                'Enregistrements des marchés
                For i = 0 To ViewPPM.RowCount - 1
                    Dim IdMethodePPM As Integer = Val(ExecuteScallar("SELECT CodeProcAO FROM t_procao WHERE AbregeAO='" & EnleverApost(ViewPPM.GetRowCellValue(i, "Méthodes de passation des marchés").ToString) & "' AND TypeMarcheAO='" & cmbTypeMarche.Text & "'"))
                    query = "insert into t_marche(CodeProjet,TypeMarche,NumeroComptable,DescriptionMarche,MontantEstimatif,RevuePrioPost,PeriodeMarche,InitialeBailleur,CodeConvention,CodeProcAO,RefPPM,DerniereMaj,Convention_ChefFile) values('" & ProjetEnCours & "','" & EnleverApost(cmbTypeMarche.Text) & "',NULL,'" & EnleverApost(ViewPPM.GetRowCellValue(i, "Description").ToString) & "','" & CInt(ViewPPM.GetRowCellValue(i, "Montant estimatif").ToString) & "','" & EnleverApost(ViewPPM.GetRowCellValue(i, "Type examen").ToString) & "','" & periode & "','" & ViewPPM.GetRowCellValue(i, "Bailleur").ToString & "','" & ViewPPM.GetRowCellValue(i, "Conventions").ToString & "','" & IdMethodePPM & "','" & CodeNewPlan & "','" & Now.ToShortDateString & " " & Now.ToLongTimeString & "','" & ViewPPM.GetRowCellValue(i, "ChefFile").ToString & "')"
                    ExecuteNonQuery(query)
                    Dim LastRefMarche As String = ExecuteScallar("SELECT MAX(RefMarche) FROM t_marche")
                    If ViewPPM.GetRowCellValue(i, "Montant bailleur").ToString.Contains("|") Then
                        Dim lesBailleurs = ViewPPM.GetRowCellValue(i, "Montant bailleur").ToString.Split("|")
                        For j = 0 To lesBailleurs.Length - 1
                            Dim Conv As String
                            Dim Montant As Double
                            If lesBailleurs(j).ToString <> String.Empty And lesBailleurs(j).ToString <> "|" Then
                                If lesBailleurs(j).ToString.Contains("&") = True Then
                                    Conv = lesBailleurs(j).ToString.Split("&")(0)
                                    Montant = lesBailleurs(j).ToString.Split("&")(1)
                                    query = "insert into t_ppm_repartitionbailleur values(NULL,'" & CodeNewPlan & "','" & LastRefMarche & "','" & Conv & "','" & Montant & "')"
                                    ExecuteNonQuery(query)
                                End If
                            End If
                        Next
                    End If
                Next
                FinChargement()
                SuccesMsg("Le plan a été enregistré avec succès")
                PlanMarche.cmbDevise.Text = "US$"
                PlanMarche.RemplirMarcheAConsulter()
                query = "SELECT max(RefPPM), LibellePPM FROM t_ppm_marche WHERE CodeProjet='" & ProjetEnCours & "'"
                Dim dtPPM As DataTable = ExcecuteSelectQuery(query)
                For Each rwPPM As DataRow In dtPPM.Rows
                    PlanMarche.MarcheAConsulter.Text = (MettreApost(rwPPM("LibellePPM")))
                Next
                Me.Close()
            Else
                SuccesMsg(erreur)
            End If
        Else
            Dim erreur As String = ""
            'si le PPM ne contient pas de ligne
            If dtLignesPPM.Rows.Count = 0 Then
                erreur += "- Veuillez ajouter des lignes au PPM" + ControlChars.CrLf
            End If
            If erreur = "" Then
                DebutChargement(True, "Enregistrement des nouvelles lignes en cours")

                Dim periode = CDate(DateDebutMarche.Text) & " - " & CDate(DateFinMarche.Text)
                'Enregistrements des marchés
                For i = 0 To ViewPPM.RowCount - 1
                    Dim IdMethodePPM As Integer = Val(ExecuteScallar("SELECT CodeProcAO FROM t_procao WHERE AbregeAO='" & EnleverApost(ViewPPM.GetRowCellValue(i, "Méthodes de passation des marchés").ToString) & "' AND TypeMarcheAO='" & cmbTypeMarche.Text & "'"))
                    query = "insert into t_marche(CodeProjet,TypeMarche,NumeroComptable,DescriptionMarche,MontantEstimatif,RevuePrioPost,PeriodeMarche,InitialeBailleur,CodeConvention,CodeProcAO,RefPPM,DerniereMaj,Convention_ChefFile) values('" & ProjetEnCours & "','" & EnleverApost(cmbTypeMarche.Text) & "',NULL,'" & EnleverApost(ViewPPM.GetRowCellValue(i, "Description").ToString) & "','" & CInt(ViewPPM.GetRowCellValue(i, "Montant estimatif").ToString) & "','" & EnleverApost(ViewPPM.GetRowCellValue(i, "Type examen").ToString) & "','" & periode & "','" & ViewPPM.GetRowCellValue(i, "Bailleur").ToString & "','" & ViewPPM.GetRowCellValue(i, "Conventions").ToString & "','" & IdMethodePPM & "','" & IDPlan & "','" & Now.ToShortDateString & " " & Now.ToLongTimeString & "','" & ViewPPM.GetRowCellValue(i, "ChefFile").ToString & "')"
                    ExecuteNonQuery(query)
                    Dim LastRefMarche As String = ExecuteScallar("SELECT MAX(RefMarche) FROM t_marche")
                    If ViewPPM.GetRowCellValue(i, "Montant bailleur").ToString.Contains("|") Then
                        Dim lesBailleurs = ViewPPM.GetRowCellValue(i, "Montant bailleur").ToString.Split("|")
                        For j = 0 To lesBailleurs.Length - 1
                            Dim Conv As String
                            Dim Montant As Double
                            If lesBailleurs(j).ToString <> String.Empty And lesBailleurs(j).ToString <> "|" Then
                                If lesBailleurs(j).ToString.Contains("&") = True Then
                                    Conv = lesBailleurs(j).ToString.Split("&")(0)
                                    Montant = lesBailleurs(j).ToString.Split("&")(1)
                                    query = "insert into t_ppm_repartitionbailleur values(NULL,'" & IDPlan & "','" & LastRefMarche & "','" & Conv & "','" & Montant & "')"
                                    ExecuteNonQuery(query)
                                End If
                            End If
                        Next
                    End If
                Next
                FinChargement()
                SuccesMsg("Les lignes ont été ajouté avec succès")
                PlanMarche.cmbDevise.Text = "US$"
                PlanMarche.BtActualiserPlan.PerformClick()
                Me.Close()
            Else
                SuccesMsg(erreur)
            End If
        End If


    End Sub
    Private Sub SupprimerlignePPM_Click(sender As Object, e As EventArgs) Handles SupprimerlignePPM.Click
        If ViewPPM.RowCount > 0 Then
            ViewPPM.DeleteRow(ViewPPM.FocusedRowHandle)
            Dim NewNbre As Integer = 1
            For i = 0 To ViewPPM.RowCount
                'If CInt(ViewPPM.GetRowCellValue(i, "N°")) > 1 Then
                '    NewNbre = CInt(ViewPPM.GetRowCellValue(i, "N°")) - 1
                'End If
                ViewPPM.SetRowCellValue(i, "N°", NewNbre)
                NewNbre += 1
            Next
            Nbre -= 1
        End If
    End Sub

    Private Sub ModifierLignePPM_Click(sender As Object, e As EventArgs) Handles ModifierLignePPM.Click
        If ViewPPM.RowCount > 0 Then
            AjoutLigne = False
            indexLigne = ViewPPM.FocusedRowHandle
            TxtDesc.Text = ViewPPM.GetRowCellValue(ViewPPM.FocusedRowHandle, "Description")
            txtMontant.Text = ViewPPM.GetRowCellValue(ViewPPM.FocusedRowHandle, "Montant estimatif")
            cmbTypeExamen.Text = ViewPPM.GetRowCellValue(ViewPPM.FocusedRowHandle, "Type examen")
            cmbMethode.Text = ViewPPM.GetRowCellValue(ViewPPM.FocusedRowHandle, "Méthodes de passation des marchés")
            If ViewPPM.GetRowCellValue(ViewPPM.FocusedRowHandle, "Montant bailleur").ToString.Contains("|") Then
                Dim lesBailleurs = ViewPPM.GetRowCellValue(ViewPPM.FocusedRowHandle, "Montant bailleur").ToString.Split("|")
                For i = 0 To lesBailleurs.Length - 1
                    Dim Conv As String
                    Dim Montant As Integer
                    If lesBailleurs(i).ToString <> String.Empty And lesBailleurs(i).ToString <> "|" Then
                        Dim Bailleur As String
                        Dim Pourcentage As String
                        If lesBailleurs(i).ToString.Contains("&") = True Then
                            Conv = lesBailleurs(i).ToString.Split("&")(0)
                            Montant = lesBailleurs(i).ToString.Split("&")(1)
                            Pourcentage = Math.Round(CDec(Montant.ToString.Trim) * 100 / CDec(ViewPPM.GetRowCellValue(ViewPPM.FocusedRowHandle, "Montant estimatif").ToString.Trim), 2)
                            Bailleur = ExecuteScallar("SELECT B.InitialeBailleur FROM t_bailleur as B, t_convention as C WHERE C.CodeBailleur=B.CodeBailleur AND C.CodeConvention='" & Conv.ToString.Trim & "'")
                            Dim drS = dtBailleur.NewRow()
                            RemplirRepart(Bailleur, Conv, Montant, Pourcentage, AfficherMonnaie(ViewPPM.GetRowCellValue(ViewPPM.FocusedRowHandle, "Montant estimatif").Replace(" ", "")))
                        End If

                    End If
                Next
            Else
                RemplirRepart(ViewPPM.GetRowCellValue(ViewPPM.FocusedRowHandle, "Bailleur"), ViewPPM.GetRowCellValue(ViewPPM.FocusedRowHandle, "Conventions"), ViewPPM.GetRowCellValue(ViewPPM.FocusedRowHandle, "Montant bailleur"), "100,00", AfficherMonnaie(ViewPPM.GetRowCellValue(ViewPPM.FocusedRowHandle, "Montant bailleur").Replace(" ", "")))
            End If
        End If
    End Sub

    Private Sub btAnnuler_Click(sender As Object, e As EventArgs) Handles btAnnuler.Click
        If IDPlan = -1 Then
            InitForm()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub btnVider_Click(sender As Object, e As EventArgs) Handles btnVider.Click
        InitLigneAjouter()
    End Sub

    Private Sub SaisiePPM_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If IDPlan <> -1 Then
            InitForm()
        End If
    End Sub
End Class