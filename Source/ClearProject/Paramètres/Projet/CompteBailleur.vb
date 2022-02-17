Imports MySql.Data.MySqlClient

Public Class CompteBailleur
    Dim dtCompte = New DataTable()

    Private Sub CompteBailleur_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        BtAnnuler_Click(Me, e)
    End Sub

    Private Sub CompteBailleur_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ChargerGridCompte()
        ChargerDevise()
        ChargerNumComptable()
        ChargerBanque()
        ChargerBailleur()
        BtAnnuler_Click(Me, e)
    End Sub

    Private Sub ChargerGridCompte()
        dtCompte.Columns.Clear()
        dtCompte.Columns.Add("Code", Type.GetType("System.String"))
        dtCompte.Columns.Add("N° Compte", Type.GetType("System.String"))
        dtCompte.Columns.Add("Libellé", Type.GetType("System.String"))
        dtCompte.Columns.Add("Bailleur", Type.GetType("System.String"))
        dtCompte.Columns.Add("Devise", Type.GetType("System.String"))
        dtCompte.Columns.Add("Solde d'ouvert.", Type.GetType("System.String"))
        dtCompte.Columns.Add("Plafond décais.", Type.GetType("System.String"))
        dtCompte.Columns.Add("Seuil réappro.", Type.GetType("System.String"))
        dtCompte.Columns.Add("N° Comptable", Type.GetType("System.String"))
        dtCompte.Columns.Add("Domiciliation", Type.GetType("System.String"))
        dtCompte.Columns.Add("Compte d'avance", Type.GetType("System.String"))

        dtCompte.Rows.Clear()

        Dim NbTotal As Decimal = 0

        query = "select C.NumeroCompte, C.LibelleCompte, B.InitialeBailleur, B.NomBailleur, D.LibelleDevise, D.AbregeDevise, C.MontantInitial, C.PlafonDecaissCompte, C.SeuilReapproCompte, C.NumeroComptable, E.CodeBanque, E.NomCompletBanque, C.CompteAvance from T_CompteBancaire as C, T_Devise as D, T_Bailleur as B, T_Banque as E where C.CodeDevise=D.CodeDevise and C.CodeBailleur=B.CodeBailleur and C.RefBanque=E.RefBanque and C.CodeProjet='" & ProjetEnCours & "' order by C.LibelleCompte"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            NbTotal += 1
            Dim drS = dtCompte.NewRow()

            drS(0) = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
            drS(1) = rw(0).ToString
            drS(2) = MettreApost(rw(1).ToString)
            drS(3) = "(" & rw(2).ToString & ") " & MettreApost(rw(3).ToString)
            drS(4) = "(" & rw(5).ToString & ") " & MettreApost(rw(4).ToString)
            drS(5) = AfficherMonnaie(rw(6).ToString)
            drS(6) = AfficherMonnaie(rw(7).ToString)
            drS(7) = AfficherMonnaie(rw(8).ToString)
            drS(8) = rw(9).ToString
            drS(9) = "(" & rw(10).ToString & ") " & MettreApost(rw(11).ToString)
            drS(10) = rw(12).ToString

            dtCompte.Rows.Add(drS)
        Next
        GridCompte.DataSource = dtCompte
        ViewCompte.Columns(0).Visible = False
        ViewCompte.Columns(1).Width = 150
        ViewCompte.Columns(2).Width = 150
        ViewCompte.Columns(3).Width = 300
        ViewCompte.Columns(4).Width = 100
        ViewCompte.Columns(5).Width = 120
        ViewCompte.Columns(6).Width = 120
        ViewCompte.Columns(7).Width = 120
        ViewCompte.Columns(8).Width = 80
        ViewCompte.Columns(9).Width = 200
        ViewCompte.Columns(10).Width = 80

        ViewCompte.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewCompte.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewCompte.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewCompte.Columns(7).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewCompte.Columns(8).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewCompte.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        'ViewCompte.Columns(3).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        'ViewCompte.Columns(2).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

        ViewCompte.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewCompte, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub

    Private Sub ChargerDevise()
        query = "select AbregeDevise from T_Devise order by AbregeDevise"
        CmbDevise.Properties.Items.Clear()
        Dim dt0 = ExcecuteSelectQuery(query)
        For Each rw In dt0.Rows
            CmbDevise.Properties.Items.Add(rw(0).ToString)
        Next
    End Sub

    Private Sub ChargerNumComptable()
        query = "select CODE_SC, LIBELLE_SC from T_COMP_SOUS_CLASSE WHERE CODE_SC LIKE '5%' order by CODE_SC"
        CmbNumComptable.Text = ""
        CmbNumComptable.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            CmbNumComptable.Properties.Items.Add(rw("CODE_SC").ToString & " - " & MettreApost(rw("LIBELLE_SC").ToString))
        Next
        ChargerCompteAvances()
    End Sub
    Private Sub ChargerCompteAvances()
        query = "select CODE_SC, LIBELLE_SC from T_COMP_SOUS_CLASSE WHERE CODE_SC LIKE '4582%' OR CODE_SC LIKE '449%' order by CODE_SC"
        cmbCompteAvance.Text = ""
        cmbCompteAvance.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            cmbCompteAvance.Properties.Items.Add(rw("CODE_SC").ToString & " - " & MettreApost(rw("LIBELLE_SC").ToString))
        Next
    End Sub
    Private Sub ChargerBanque()
        query = "select CodeBanque from T_Banque where CodeProjet='" & ProjetEnCours & "' order by CodeBanque"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        TxtBanque.Text = ""
        CmbBanque.Text = ""
        CmbBanque.Properties.Items.Clear()
        For Each rw In dt.Rows
            CmbBanque.Properties.Items.Add(rw("CodeBanque").ToString)
        Next
    End Sub
    Private Sub ChargerBailleur()
        query = "select InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' order by InitialeBailleur"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        TxtBailleur.Text = ""
        CmbBailleur.Text = ""
        CmbBailleur.Properties.Items.Clear()
        For Each rw In dt.Rows
            CmbBailleur.Properties.Items.Add(rw("InitialeBailleur").ToString)
        Next
    End Sub
    Private Sub CmbDevise_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbDevise.SelectedValueChanged
        query = "select LibelleDevise, CodeDevise from T_Devise where AbregeDevise='" & CmbDevise.Text & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        TxtDevise.Text = ""
        TxtCodeDevise.Text = ""
        If dt.Rows.Count > 0 Then
            Dim rw As DataRow = dt.Rows(0)
            TxtDevise.Text = MettreApost(rw("LibelleDevise").ToString)
            TxtCodeDevise.Text = rw("CodeDevise").ToString
        End If
    End Sub
    Private Sub BtPlanComptable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtPlanComptable.Click
        Dialog_form(compte_general)
        Dim oldCompteComptable As String = CmbNumComptable.Text
        Dim oldCompteAvance As String = cmbCompteAvance.Text
        ChargerNumComptable()
        CmbNumComptable.Text = oldCompteComptable
        cmbCompteAvance.Text = oldCompteAvance
    End Sub
    Private Sub BtBanque_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtBanque.Click
        EtsBancaire.Size = New Point(730, 400)
        Dialog_form(EtsBancaire)
        Dim oldCompteBanque As String = CmbBanque.Text
        ChargerBanque()
        CmbBanque.Text = oldCompteBanque
    End Sub
    Private Sub CmbBanque_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbBanque.SelectedValueChanged
        query = "select NomCompletBanque, RefBanque from T_Banque where CodeProjet='" & ProjetEnCours & "' and CodeBanque='" & CmbBanque.Text & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        TxtBanque.Text = ""
        TxtRefBanque.Text = ""
        If dt.Rows.Count > 0 Then
            Dim rw As DataRow = dt.Rows(0)
            TxtBanque.Text = MettreApost(rw("NomCompletBanque").ToString)
            TxtRefBanque.Text = rw("RefBanque").ToString
        End If
    End Sub

    Private Sub CmbBailleur_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbBailleur.SelectedValueChanged
        query = "select NomBailleur, CodeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' and InitialeBailleur='" & CmbBailleur.Text & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        TxtBailleur.Text = ""
        TxtCodeBailleur.Text = ""
        If dt.Rows.Count > 0 Then
            Dim rw As DataRow = dt.Rows(0)
            TxtBailleur.Text = MettreApost(rw("NomBailleur").ToString)
            TxtCodeBailleur.Text = rw("CodeBailleur").ToString
        End If
    End Sub

    Private Sub BtAnnuler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtAnnuler.Click
        For Each Ctrls In GroupControl1.Controls
            If Not (TypeOf (Ctrls) Is DevExpress.XtraEditors.LabelControl) Then
                Ctrls.Text = ""
                TxtNumCompte.Enabled = True
            End If
        Next
        BtEnregistrer.Enabled = True
        btDel.Enabled = True
        btModifier.Enabled = False
    End Sub

    Private Sub BtEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click
        For Each Ctrls In GroupControl1.Controls
            If Not (TypeOf (Ctrls) Is DevExpress.XtraEditors.SimpleButton) Then
                If (Ctrls.Text.Replace(" ", "") = "") Then
                    SuccesMsg("Formulaire incomplet.")
                    Exit Sub
                End If
            End If
        Next

        query = "select * from T_CompteBancaire where (NumeroCompte='" & TxtNumCompte.Text & "' or LibelleCompte='" & EnleverApost(TxtNomCompte.Text) & "' or (NumeroComptable='" & Trim(CmbNumComptable.Text.Split(" "c)(0)) & "' and CodeBailleur<>'" & TxtCodeBailleur.Text & "')) and CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 = ExcecuteSelectQuery(query)
        If dt0.Rows.Count > 0 Then
            SuccesMsg("Veuillez choissir un autre numéro comptable.")
            Exit Sub
        End If

        Dim refbank As Decimal
        Dim codebail As Decimal

        refbank = TxtRefBanque.Text
        codebail = TxtCodeBailleur.Text


        Dim DatSet = New DataSet
        query = "SELECT * FROM T_CompteBancaire"
        Dim queryconn As New MySqlConnection
        BDOPEN(queryconn)
        Dim Cmd As MySqlCommand = New MySqlCommand(query, queryconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd)
        DatAdapt.Fill(DatSet, "T_CompteBancaire")
        Dim DatTable = DatSet.Tables("T_CompteBancaire")
        Dim DatRow = DatSet.Tables("T_CompteBancaire").NewRow()

        DatRow("NumeroCompte") = TxtNumCompte.Text
        DatRow("LibelleCompte") = EnleverApost(TxtNomCompte.Text)
        DatRow("RefBanque") = refbank.ToString
        DatRow("TypeCompte") = CmbTypeCompte.Text
        DatRow("CodeDevise") = TxtCodeDevise.Text
        DatRow("SeuilReapproCompte") = TxtSeuil.EditValue
        DatRow("PlafonDecaissCompte") = TxtPlafond.EditValue
        DatRow("MontantInitial") = TxtSolde.EditValue
        DatRow("SoldeCompte") = TxtSolde.EditValue
        DatRow("NumeroComptable") = Trim(CmbNumComptable.Text.Split(" - ")(0))
        DatRow("CompteAvance") = Trim(cmbCompteAvance.Text.Split(" - ")(0))
        DatRow("CodeBailleur") = codebail.ToString
        DatRow("CodeProjet") = ProjetEnCours
        DatRow("DateModif") = Now.ToShortDateString & " " & Now.ToLongTimeString
        DatRow("DateSaisie") = Now.ToShortDateString & " " & Now.ToLongTimeString
        DatRow("Operateur") = CodeUtilisateur
        DatRow("CodeConvention") = ""

        DatSet.Tables("T_CompteBancaire").Rows.Add(DatRow)
        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)

        DatAdapt.Update(DatSet, "T_CompteBancaire")
        DatSet.Clear()
        BDQUIT(queryconn)
        SuccesMsg("Enregistrement effectué avec succès")
        ChargerGridCompte()
        BtAnnuler_Click(Me, e)

    End Sub

    Private Sub GridCompte_Click(sender As System.Object, e As System.EventArgs) Handles GridCompte.Click
        If (ViewCompte.RowCount > 0) Then
            drx = ViewCompte.GetDataRow(ViewCompte.FocusedRowHandle)
            Dim IDL = drx(1).ToString
            ColorRowGrid(ViewCompte, "[Code]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewCompte, "[N° Compte]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
            BtAnnuler.PerformClick()
        End If
    End Sub

    Private Sub GridCompte_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridCompte.DoubleClick
        TxtNumCompte.Enabled = False
        If ViewCompte.RowCount > 0 Then
            BtEnregistrer.Enabled = False
            btDel.Enabled = False
            btModifier.Enabled = True
            drx = ViewCompte.GetDataRow(ViewCompte.FocusedRowHandle)
            query = "select * from t_comptebancaire where NumeroCompte='" & drx(1).ToString & "'"
            Dim dt0 = ExcecuteSelectQuery(query)
            For Each rw0 In dt0.Rows
                TxtNumCompte.Text = rw0(0).ToString
                TxtNomCompte.Text = MettreApost(rw0(1).ToString)
                TxtSolde.Text = rw0(8).ToString
                TxtPlafond.Text = rw0(6).ToString
                TxtSeuil.Text = rw0(5).ToString
                CmbTypeCompte.Text = rw0(3).ToString

                'bailleur
                query = "select * from t_bailleur where codebailleur='" & rw0("CodeBailleur").ToString & "'"
                Dim dt1 = ExcecuteSelectQuery(query)
                For Each rw1 In dt1.Rows
                    CmbBailleur.Text = rw1(2).ToString
                    TxtBailleur.Text = MettreApost(rw1(1).ToString)
                Next

                'Banque
                query = "select * from t_banque where refbanque='" & rw0(2).ToString & "'"
                dt1 = ExcecuteSelectQuery(query)
                For Each rw1 In dt1.Rows
                    CmbBanque.Text = rw1(1).ToString
                    TxtBanque.Text = MettreApost(rw1(2).ToString)
                Next

                'Devise
                query = "select * from t_devise where codedevise='" & rw0(4).ToString & "'"
                dt1 = ExcecuteSelectQuery(query)
                For Each rw1 In dt1.Rows
                    CmbDevise.Text = rw1(2).ToString
                Next

                'compte comptable
                query = "select * from t_comp_sous_classe where code_sc='" & rw0("NumeroComptable").ToString & "'"
                dt0 = ExcecuteSelectQuery(query)
                For Each rwx In dt0.Rows
                    CmbNumComptable.Text = rwx(0).ToString & " - " & MettreApost(rwx(2).ToString)
                Next

                'compte d'avance
                query = "select * from t_comp_sous_classe where code_sc='" & rw0("CompteAvance").ToString & "'"
                dt0 = ExcecuteSelectQuery(query)
                For Each rwx In dt0.Rows
                    cmbCompteAvance.Text = rwx(0).ToString & " - " & MettreApost(rwx(2).ToString)
                Next

            Next
        End If
    End Sub

    Private Sub ModifierCompteBailleurToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ModifierCompteBailleurToolStripMenuItem.Click
        'Code de modification de l'enregistrement choisi
        If (TxtSolde.Text <> "") And (TxtSeuil.Text <> "") And (TxtPlafond.Text <> "") And (TxtCodeDevise.Text <> "") And (TxtCodeBailleur.Text <> "") Then
            drx = ViewCompte.GetDataRow(ViewCompte.FocusedRowHandle)
            query = "UPDATE t_comptebancaire SET LibelleCompte = '" & EnleverApost(TxtNomCompte.Text) & "', SeuilReapproCompte = '" & TxtSeuil.Text &
               "', PlafonDecaissCompte = '" & TxtPlafond.Text & "', CodeDevise = '" & TxtCodeDevise.Text & "', CodeBailleur = '" & TxtCodeBailleur.Text & "' WHERE NumeroCompte = '" & drx(1).ToString & "'"
            ExecuteNonQuery(query)
            SuccesMsg("Modification effectuée avec succès.")
            ChargerGridCompte()
        Else
            MsgBox("Veuillez selectionner une ligne dans le tableau !", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub SupprimerCompteBailleurToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SupprimerCompteBailleurToolStripMenuItem.Click
        btDel.PerformClick()
    End Sub

    Private Sub btModifier_Click(sender As System.Object, e As System.EventArgs) Handles btModifier.Click
        'Code de modification de l'enregistrement choisi
        If (TxtSolde.Text <> "") And (TxtSeuil.Text <> "") And (TxtPlafond.Text <> "") And (TxtCodeDevise.Text <> "") And (TxtCodeBailleur.Text <> "") And (CmbTypeCompte.Text <> "") And (CmbNumComptable.Text <> "") And (TxtRefBanque.Text <> "") Then
            drx = ViewCompte.GetDataRow(ViewCompte.FocusedRowHandle)
            query = "UPDATE t_comptebancaire SET LibelleCompte = '" & EnleverApost(TxtNomCompte.Text) & "', SeuilReapproCompte = '" & TxtSeuil.Text & "', PlafonDecaissCompte = '" & TxtPlafond.Text & "',MontantInitial='" & TxtSolde.Text & "',SoldeCompte ='" & TxtSolde.Text & "', CodeDevise = '" & TxtCodeDevise.Text & "', CodeBailleur = '" & TxtCodeBailleur.Text & "', TypeCompte = '" & CmbTypeCompte.Text & "', NumeroComptable = '" & CmbNumComptable.Text.Split(" - ")(0) & "', CompteAvance = '" & cmbCompteAvance.Text.Split(" - ")(0) & "', RefBanque = '" & TxtRefBanque.Text & "'   WHERE NumeroCompte = '" & drx(1).ToString & "'"
            Try
                ExecuteNonQuery(query)
                SuccesMsg("Modification effectuée avec succès.")
                ChargerGridCompte()
                EffacerTexBox4(GroupControl1)
                BtEnregistrer.Enabled = False
                btModifier.Enabled = True
            Catch ex As Exception
                FailMsg("Impossible de modifier : " & vbNewLine & ex.ToString())
            End Try
        Else
            MsgBox("Veuillez selectionner une ligne dans le tableau !", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub btDel_Click(sender As System.Object, e As System.EventArgs) Handles btDel.Click
        If ViewCompte.FocusedRowHandle <> -1 And ViewCompte.RowCount > 0 Then
            If ConfirmMsg("Voulez-vous vraiment supprimer?") = DialogResult.Yes Then
                drx = ViewCompte.GetDataRow(ViewCompte.FocusedRowHandle)
                Dim DatSet = New DataSet
                query = "DELETE FROM t_comptebancaire WHERE NumeroCompte = '" & drx(1).ToString & "'"
                ExecuteNonQuery(query)

                SuccesMsg("Suppression effectuée avec succès")
                ChargerGridCompte()
                EffacerTexBox4(GroupControl1)
            End If
        End If
    End Sub
End Class