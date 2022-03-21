Imports MySql.Data.MySqlClient
Imports System.Math

Public Class EvalOffreFinanciereFinal
    Public NumDossDp As String = ""
    Dim MontantFrais As Decimal = 0

    Private Sub EvalOffreFinanciere_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        InitialiserDonnée()
        GetEnebled(False)

        TxtNom.Text = ReponseDialog
        LabelMonnaie1.Text = MonnaieEvalOffre.ToString.ToUpper
        LabelMonnaie2.Text = "(en " & MonnaieEvalOffre.ToString & ")"
        LabelDevise1.Text = ""
        LabelDevise2.Text = ""

        'ItemDevise()
        RemplirCombo2(CmbDevise, "T_Devise", "AbregeDevise")
        'Unité
        RemplirCombo2(CmbUnite, "t_unite", "LibelleUnite")

        'Liste frais remboursable
        GetFraisRemboursable()
    End Sub

    Private Sub GetFraisRemboursable()
        Dim dt As DataTable = ExcecuteSelectQuery("SELECT * from t_dp_listerembours where NumeroDp='" & EnleverApost(NumDossDp) & "'")
        If dt.Rows.Count > 0 Then
            'Frais remboursable appliquer
            Me.Size = New Point(780, 433)

            GroupControlfr.Visible = True
            TxtMontLettre.Visible = False

            Dim dtFrais = New DataTable
            dtFrais.Columns.Add("CodeX", Type.GetType("System.String"))
            dtFrais.Columns.Add("Ref", Type.GetType("System.String"))
            dtFrais.Columns.Add("N°", Type.GetType("System.String"))
            dtFrais.Columns.Add("Libelle frais remboursable", Type.GetType("System.String"))
            dtFrais.Columns.Add("Montant", Type.GetType("System.String"))
            dtFrais.Rows.Clear()

            Dim Nbre As Integer = 1
            For Each rw As DataRow In dt.Rows
                Dim drS = dtFrais.NewRow()
                drS("CodeX") = IIf(Nbre Mod 2 = 0, "x", "").ToString
                drS("N°") = Nbre.ToString
                drS("Ref") = rw("RefListe").ToString
                drS("Libelle frais remboursable") = MettreApost(rw("Description").ToString)
                drS("Montant") = "" ' MontantFraisRemboursable(ExceptRevue, rw("RefListe").ToString)
                dtFrais.Rows.Add(drS)
                Nbre += 1
            Next
            GridViewFrais.DataSource = dtFrais
            ViewFrais.OptionsView.ColumnAutoWidth = True

            Dim txtNumero As New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
            AddHandler txtNumero.EditValueChanged, AddressOf txtNumero_EditValueChanged

            ViewFrais.Columns(4).ColumnEdit = txtNumero
            ViewFrais.Columns("CodeX").Visible = False
            ViewFrais.Columns("Ref").Visible = False

            ViewFrais.Columns("CodeX").OptionsColumn.AllowEdit = False
            ViewFrais.Columns("N°").OptionsColumn.AllowEdit = False
            ViewFrais.Columns("Ref").OptionsColumn.AllowEdit = False
            ViewFrais.Columns(3).OptionsColumn.AllowEdit = False
            ViewFrais.Columns(4).OptionsColumn.AllowEdit = False
            ViewFrais.Columns("N°").Width = 20
            ViewFrais.Columns(4).Width = 150
            ViewFrais.Columns("N°").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewFrais.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

            ColorRowGrid(ViewFrais, "[CodeX]='x'", Color.LightGray, "Tahoma", 10, FontStyle.Regular, Color.Black)
        Else
            'Frais remboursable non appliquer
            TxtMontLettre.Visible = True
            GroupControlfr.Visible = False
            Me.Size = New Point(780, 317)
        End If
    End Sub

    Private Sub txtNumero_EditValueChanged(ByVal sender As Object, e As EventArgs)
        If ViewFrais.RowCount > 0 And GroupControlfr.Visible = True Then
            Dim SommMontants As Decimal = 0
            If IsNumeric(sender.text) Then
                SommMontants += CDec((sender.text))
                Dim Index As Decimal = ViewFrais.FocusedRowHandle

                For i = 0 To ViewFrais.RowCount - 1
                    If i <> Index Then
                        If IsNumeric(ViewFrais.GetRow(i)(4).ToString) Then
                            SommMontants += CDec(ViewFrais.GetRow(i)(4).ToString)
                        End If
                    End If
                Next
                If TxtTaux.Text <> "" Then MontantFrais = CDec(SommMontants) * CDec(TxtTaux.Text.Replace(".", ","))
            End If
            CalculerOffre()
        End If
    End Sub

    Private Function MontantFraisRemboursable(ByVal RefSoumis As String, ByVal RefFrais As String) As String
        Dim MontantFrai As String = ""
        Try
            MontantFrai = ExecuteScallar("select f.MontantFrais from t_dp_montantfraisremboursable as f, t_soumissionconsultant as s where f.RefSoumis=s.RefSoumis and s.RefSoumis='" & RefSoumis & "' and s.NumeroDp='" & EnleverApost(NumDossDp) & "' and f.Ref='" & RefFrais & "'")
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        Return MontantFrai
    End Function

    Private Sub InitialiserDonnée()
        CmbUnite.Text = ""
        TxtMontPropose.Text = ""
        CmbDevise.Text = ""
        TxtTauxJrs.Text = ""
        TxtMontOffre.Text = ""
        TxtMontLettre.Text = ""
        NbJoursTravail.Text = ""
        TxtTaux.Text = ""
        MontantFrais = 0
    End Sub


    Private Sub ItemDevise()

        query = "select AbregeDevise from T_Devise"
        CmbDevise.Text = ""
        CmbDevise.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbDevise.Properties.Items.Add(rw("AbregeDevise").ToString)
        Next
    End Sub

    Private Sub GetEnebled(value As Boolean)
        TxtMontPropose.Enabled = value
        CmbUnite.Enabled = value
        NbJoursTravail.Enabled = value
        TxtTauxJrs.Enabled = value
    End Sub

    Private Sub CmbDevise_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbDevise.SelectedValueChanged
        If CmbDevise.SelectedIndex <> -1 Then
            GetEnebled(True)
            LabelDevise1.Text = "(en " & CmbDevise.Text & ")"
            LabelDevise2.Text = "(en " & CmbDevise.Text & ")"

            If GroupControlfr.Visible = True Then
                ViewFrais.Columns(4).OptionsColumn.AllowEdit = True
                ViewFrais.Columns(4).Caption = "Montant en " & CmbDevise.Text
            End If

            'Taux de la devise selectionné
            Dim TauxDevise1 As String = ExecuteScallar("select TauxDevise from T_Devise where AbregeDevise='" & EnleverApost(CmbDevise.Text) & "'")
            'Taux de la devise de la monnaie d'évaluation
            Dim TauxDevise2 As String = ExecuteScallar("select TauxDevise from T_Devise where AbregeDevise='" & EnleverApost(MonnaieEvalOffre) & "'")
            If TauxDevise1.ToString <> "" Then TauxDevise1 = CDec(TauxDevise1)
            If TauxDevise2.ToString <> "" Then TauxDevise2 = CDec(TauxDevise2)

            If TauxDevise2.ToString <> "" And TauxDevise2.ToString <> "" Then
                TxtTaux.Text = Round(TauxDevise1 / TauxDevise2, 3)
            Else
                TxtTaux.Text = ""
            End If

            TxtMontPropose.Select()
            CalculerOffre()
        Else
            GetEnebled(False)
            LabelDevise1.Text = ""
            LabelDevise2.Text = ""
            TxtTaux.Text = ""
            If GroupControlfr.Visible = True Then ViewFrais.Columns(4).OptionsColumn.AllowEdit = False
            If GroupControlfr.Visible = True Then ViewFrais.Columns(4).Caption = "Montant"
        End If

    End Sub

    Private Sub CalculerOffre()

        If (TxtMontPropose.Text <> "" And TxtTaux.Text <> "" And (TxtMontPropose.Text <> "0" Or TxtMontPropose.Text <> "0,000")) Then
            TxtMontOffre.Text = Round(CDec(TxtMontPropose.Text.Replace(".", ",")) * CDec(TxtTaux.Text.Replace(".", ",")) + MontantFrais)
        ElseIf MontantFrais > 0 And TxtTaux.Text <> "" Then
            TxtMontOffre.Text = Round(CDec(MontantFrais.ToString))
        Else
            TxtMontOffre.Text = ""
        End If
    End Sub

    Private Sub TxtMontPropose_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontPropose.TextChanged, TxtTaux.TextChanged
        CalculerOffre()
    End Sub

    Private Sub TxtMontOffre_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontOffre.TextChanged
        If TxtMontOffre.Text.Trim <> "" Then
            TxtMontLettre.Text = MontantLettre(TxtMontOffre.Text.Replace(".", ","))
        Else
            TxtMontLettre.Text = ""
        End If
    End Sub

    Private Sub BtQuitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        Me.Close()
    End Sub

    ' MontantFrais

    Private Sub BtEnregOffre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnregOffre.Click
        If (TxtMontPropose.Text = "0" Or TxtMontPropose.Text = "") Then
            SuccesMsg("Aucun montant proposé")
            TxtMontPropose.Focus()
            Exit Sub
        End If

        If (TxtMontOffre.Text = "0" Or TxtMontOffre.Text = "") Then
            SuccesMsg("Veuillez saisir le montant de l'offre")
            TxtMontOffre.Focus()
            Exit Sub
        End If

        If CmbDevise.SelectedIndex = -1 Then
            SuccesMsg("Veuillez selectionné la devise dans laquelle le consultant a fait sa proposition")
            CmbDevise.Focus()
            Exit Sub
        End If

        If (TxtTauxJrs.Text = "0" Or TxtTauxJrs.Text = "") Then
            SuccesMsg("Veuillez saisir le taux jounalier du consultant")
            TxtTauxJrs.Focus()
            Exit Sub
        End If

        If Val(NbJoursTravail.Text) = "0" Or NbJoursTravail.Text = "" Then
            SuccesMsg("Veuillez saisir le nombre de jour de travail du consultant")
            NbJoursTravail.Focus()
            Exit Sub
        End If

        'Frais rembousable appliquer
        Dim MontantFraisRembours As Decimal = 0
        If GroupControlfr.Visible = True Then
            For i = 0 To ViewFrais.RowCount - 1
                If IsNumeric(ViewFrais.GetRow(i)(4).ToString) Then
                    MontantFraisRembours += CDec(ViewFrais.GetRow(i)(4).ToString)
                Else
                    SuccesMsg("Veuillez saisir correctement tous les montants des frais remboursables")
                    Exit Sub
                End If
            Next
        End If

        DebutChargement(True, "Traitement des offres en cours...")
        If GroupControlfr.Visible = True Then
            ExecuteScallar("delete from t_dp_montantfraisremboursable where RefSoumis='" & ExceptRevue & "'")

            For i = 0 To ViewFrais.RowCount - 1
                ExecuteNonQuery("INSERT INTO t_dp_montantfraisremboursable VALUES(NULL, '" & ExceptRevue & "', '" & ViewFrais.GetRowCellValue(i, "Ref").ToString & "', '" & ViewFrais.GetRow(i)(4).ToString & "')")
            Next
        End If

        'TauxJournalierLocal
        'MontantAjusterDevise
        ExecuteNonQuery("Update T_SoumissionConsultant set Monnaie='" & EnleverApost(CmbDevise.Text) & "', HtHdTtc='HT', MontantProposeDevise='" & Round(MontantFraisRembours + CDec(TxtMontPropose.Text.Replace(".", ",").Replace(" ", "")), 2).ToString.Replace(",", ".") & "', TauxJournalierDevise='" & TxtTauxJrs.Text.Replace(" ", "").Replace(",", ".") & "', MontantOffresLocal='" & TxtMontOffre.Text.Replace(" ", "").Replace(",", ".") & "', TauxJournalierLocal='" & Round(CDec(TxtTaux.Text.Replace(".", ",").Replace(" ", "")) * CDec(TxtTauxJrs.Text.Replace(".", ",").Replace(" ", ""))) & "', NbreJrsTravail='" & NbJoursTravail.Text.Replace(" ", "") & "', Unite='" & EnleverApost(CmbUnite.Text) & "', TauxEvalOffrsFin='" & TxtTaux.Text.Replace(".", ",").Replace(" ", "") & "', DateSaisieOffreFin='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' where RefSoumis='" & ExceptRevue & "'")

        FinChargement()
        SuccesMsg("Offre enregistrée avec succès")
        EvaluationConsultants.ViewSaisiOffreFinance.SetFocusedRowCellValue("Offre financière", AfficherMonnaie(TxtMontOffre.Text.Replace(".", ",")) & " " & MonnaieEvalOffre.ToString)
        EvaluationConsultants.ViewSaisiOffreFinance.SetFocusedRowCellValue("Statut de l'offre", "A calculer")

        Me.Close()
    End Sub
End Class