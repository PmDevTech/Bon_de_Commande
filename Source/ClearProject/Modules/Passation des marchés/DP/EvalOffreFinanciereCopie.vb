Imports MySql.Data.MySqlClient

Public Class EvalOffreFinanciereCopie
    Private Sub EvalOffreFinanciere_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        TxtNom.Text = ReponseDialog
        LabelMonnaie.Text = MonnaieEvalOffre.ToUpper
        ItemDevise()

        TxtMontPropose.Text = ""
        TxtMontPropose.Enabled = False
        CmbDevise.Text = ""
        TxtTaux.Text = ""
        TxtMontOffre.Text = ""
        TxtMontLettre.Text = ""

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

    Private Sub CmbDevise_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbDevise.SelectedValueChanged
        If (CmbDevise.SelectedIndex <> -1) Then
            query = "select TauxDevise from T_Devise where AbregeDevise='" & CmbDevise.Text & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows

                TxtTaux.Text = rw("TauxDevise").ToString
            Next

            CalculerOffre()
            TxtMontPropose.Enabled = True
            TxtMontPropose.Select()
        End If
    End Sub

    Private Sub CalculerOffre()

        If (TxtMontPropose.Text <> "" And TxtTaux.Text <> "") Then
            If (IsNumeric(TxtMontPropose.Text) = False) Then
                SuccesMsg("Format incorrect !")
                TxtMontPropose.Select(0, TxtMontPropose.Text.Length)
                Exit Sub
            End If

            TxtMontOffre.Text = AfficherMonnaie(Math.Round(CDec(TxtMontPropose.Text) * CDec(TxtTaux.Text), 0).ToString)

        Else
            TxtMontOffre.Text = ""
        End If

    End Sub

    Private Sub TxtMontPropose_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontPropose.TextChanged
        CalculerOffre()
    End Sub

    Private Sub TxtMontOffre_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontOffre.TextChanged
        If (TxtMontOffre.Text <> "") Then
            TxtMontLettre.Text = MontantLettre(TxtMontOffre.Text)
        Else
            TxtMontLettre.Text = ""
        End If
    End Sub

    Private Sub BtQuitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        Me.Close()
    End Sub

    Private Sub BtEnregOffre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnregOffre.Click

        If (TxtMontOffre.Text.Trim <> "") Then
            DebutChargement(True, "Traitement des offres en cours...")

            ' query = "Update T_SoumissionConsultant set Monnaie='FCFA', HtHdTtc='HT', MontantPropose='" & TxtMontOffre.Text.Replace(" ", "") & "' where RefSoumis='" & ExceptRevue & "'"

            query = "Update T_SoumissionConsultant set Monnaie='" & LabelMonnaie.Text & "', HtHdTtc='HT', MontantPropose='" & TxtMontOffre.Text.Replace(" ", "") & "' where RefSoumis='" & ExceptRevue & "'"
            ExecuteNonQuery(query)

            'Calculer le scrore financier
            EvaluationConsultants.CalculerScoreFinancier()
            SuccesMsg("Offre enregistre avec succès")
            Me.Close()
            FinChargement()
        Else
            SuccesMsg("Aucun montant saisi !")
            TxtMontPropose.Focus()
        End If

    End Sub
End Class