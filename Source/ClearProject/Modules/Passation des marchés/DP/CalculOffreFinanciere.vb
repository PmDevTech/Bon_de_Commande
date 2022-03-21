Imports ClearProject.PassationMarche
Imports System.Math

Public Class CalculOffreFinanciere
    Public StatutOffres As Boolean = False
    Public NumDos As String = ""
    Public MethodeMarches As String = ""
    Dim MontantAjutDevis As Decimal = 0

    Private Sub CalculOffreFinanciere_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ReponseDialog = "" Or MethodeMarches.ToString = "" Then
            Me.Close()
        End If

        If StatutOffres = False Then
            Timer1.Interval = 1000
            Timer1.Start()
        End If

        ChargerDonneCalule
    End Sub

    Private Sub ChargerDonneCalule()
        Try

            Dim rwSoumi As DataRow = ExcecuteSelectQuery("Select TauxEvalOffrsFin, DateSaisieOffreFin, TauxCalculOffrsFin, DateCalculOfrFin, MontantProposeDevise, MontantOffresLocal, MontantAjusterDevise, MontantAjusterLocal, TauxJournalierDevise, TauxJournalierLocal, NbreJrsTravail, Monnaie from t_soumissionconsultant where RefSoumis='" & ReponseDialog & "'").Rows(0)
            Dim MontantFraisRem As String = ""
            MontantFraisRem = ExecuteScallar("select SUM(f.Montants) from t_dp_montantfraisremboursable as f, t_soumissionconsultant as s where s.RefSoumis=f.RefSoumis and s.NumeroDp='" & EnleverApost(NumDos.ToString) & "' and f.RefSoumis='" & ReponseDialog & "'")
            If MontantFraisRem.ToString = "" Then MontantFraisRem = 0

            If StatutOffres = False Then
                Dim TauxDevise1 As String = ExecuteScallar("select TauxDevise from T_Devise where AbregeDevise='" & EnleverApost(rwSoumi("Monnaie").ToString) & "'")
                Dim TauxDevise2 As String = ExecuteScallar("select TauxDevise from T_Devise where AbregeDevise='" & EnleverApost(MonnaieEvalOffre.ToString) & "'")
                If TauxDevise1.ToString <> "" Then TauxDevise1 = CDec(TauxDevise1)
                If TauxDevise2.ToString <> "" Then TauxDevise2 = CDec(TauxDevise2)
                If TauxDevise1.ToString <> "" And TauxDevise2.ToString <> "" Then
                    TauxLocal2.Text = Round(TauxDevise1 / TauxDevise2, 3)
                End If
            Else
                TauxLocal2.Text = AfficherMonnaie(rwSoumi("TauxCalculOffrsFin").ToString)
                TextEdit6.Text = rwSoumi("DateCalculOfrFin").ToString
            End If

            LabelDevis1.Text = rwSoumi("Monnaie").ToString
            LabelDevis2.Text = rwSoumi("Monnaie").ToString
            LabelDevis3.Text = rwSoumi("Monnaie").ToString
            LabelLocal1.Text = MonnaieEvalOffre.ToString
            LabelLocal2.Text = MonnaieEvalOffre.ToString
            LabelLocal3.Text = MonnaieEvalOffre.ToString
            LabelLocal4.Text = MonnaieEvalOffre.ToString
            LabelLocal5.Text = MonnaieEvalOffre.ToString

            MontantProposeDevise.Text = AfficherMonnaie(rwSoumi("MontantProposeDevise").ToString)
            txtMontantpropose.Text = AfficherMonnaie(CDec(rwSoumi("MontantProposeDevise").ToString.Replace(".", ",")) - MontantFraisRem)
            MontantFraisremb.Text = AfficherMonnaie(MontantFraisRem.ToString)
            MontantOffreLocal.Text = AfficherMonnaie(CDec(MontantProposeDevise.Text) * CDec(rwSoumi("TauxEvalOffrsFin").ToString.Replace(".", ",")))
            TauxLocal1.Text = AfficherMonnaie(rwSoumi("TauxEvalOffrsFin").ToString)
            DateHeureLocal.Text = rwSoumi("DateSaisieOffreFin").ToString

            TauxJournaDevise.Text = AfficherMonnaie(rwSoumi("TauxJournalierDevise").ToString)
            TauxJournaConvert.Text = Round(CDec(TauxLocal2.Text) * CDec(rwSoumi("TauxJournalierDevise").ToString.Replace(".", ",")))
            NbrJourTravail.Text = AfficherMonnaie(rwSoumi("NbreJrsTravail").ToString)

            MontantAjutDevis = CDec(rwSoumi("NbreJrsTravail").ToString) * CDec(rwSoumi("TauxJournalierDevise").ToString.Replace(".", ",")) + MontantFraisRem
            Dim MontVerifDevis As Decimal = CDec(rwSoumi("MontantProposeDevise").ToString) - MontantAjutDevis

            'Montant propose trop eleve On diminue le montant propose
            If MontVerifDevis > 0 Then
                AjustementDevise.Text = AfficherMonnaie(MontVerifDevis.ToString)
                txtSigne1.Text = "-"
                'Montant propose trop faible On augmente le montant propose
            ElseIf MontVerifDevis < 0 Then
                AjustementDevise.Text = AfficherMonnaie(MontantAjutDevis - CDec(rwSoumi("MontantProposeDevise").ToString))
                txtSigne1.Text = "+"
            ElseIf MontVerifDevis = 0 Then
                AjustementDevise.Text = 0
                txtSigne1.Text = "+"
            End If

            Dim MontantLocalConv As Decimal = Round(CDec(TauxJournaConvert.Text) * CDec(rwSoumi("NbreJrsTravail").ToString) + CDec(TauxLocal2.Text) * MontantFraisRem)
            Dim DifMontantLocalConv As Decimal = CDec(MontantOffreLocal.Text) - MontantLocalConv

            'Montant propose trop eleve On diminue le montant propose
            If DifMontantLocalConv > 0 Then
                AjustementLocal.Text = AfficherMonnaie(DifMontantLocalConv.ToString)
                txtSigne2.Text = "-"

                'Montant propose trop faible On augmente le montant propose
            ElseIf DifMontantLocalConv < 0 Then
                AjustementLocal.Text = AfficherMonnaie(MontantLocalConv - CDec(MontantOffreLocal.Text))
                txtSigne2.Text = "+"
            ElseIf DifMontantLocalConv = 0 Then
                AjustementLocal.Text = 0
                txtSigne1.Text = "+"
            End If
            TxtPrixTotal.Text = AfficherMonnaie(MontantLocalConv.ToString)
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub BtEnrgCalcul_Click(sender As Object, e As EventArgs) Handles BtEnrgCalcul.Click
        Try
            If TxtPrixTotal.Text = "" Then 'En cas de bug
                Exit Sub
            End If

            If ConfirmMsg("L'enregistrement des ajustements empêchera la modifiaction de l'offre du consultant" & vbNewLine & " Voulez-vous continuer ? ") = DialogResult.Yes Then
                ExecuteNonQuery("UPDATE t_soumissionconsultant set TauxCalculOffrsFin='" & TauxLocal2.Text.Replace(" ", "").Replace(".", ",") & "', DateCalculOfrFin='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "', TauxJournalierLocal='" & Round(CDec(TauxJournaConvert.Text.Replace(" ", ""))) & "', MontantAjusterLocal='" & TxtPrixTotal.Text.Replace(" ", "") & "', MontantAjusterDevise='" & Round(CDec(MontantAjutDevis.ToString.Replace(" ", "")), 2).ToString.Replace(",", ".") & "' where RefSoumis='" & ReponseDialog & "'")
                EvaluationConsultants.ViewSaisiOffreFinance.SetFocusedRowCellValue("Offre financière", AfficherMonnaie(TxtPrixTotal.Text) & " " & MonnaieEvalOffre.ToString)
                EvaluationConsultants.ViewSaisiOffreFinance.SetFocusedRowCellValue("Statut de l'offre", "Calculé")
                If StatutOffres = False Then Timer1.Stop()
                SuccesMsg("Ajustement effectué avec succès")
                BtEnrgCalcul.Enabled = False
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub TxtPrixTotal_TextChanged(sender As Object, e As EventArgs) Handles TxtPrixTotal.TextChanged
        If TxtPrixTotal.Text.Trim <> "" Then
            TxtPrixTotalLettre.Text = MontantLettre(CDec(TxtPrixTotal.Text))
        Else
            TxtPrixTotalLettre.Text = ""
        End If
    End Sub

    Private Sub BtAnnuler_Click(sender As Object, e As EventArgs) Handles BtAnnuler.Click
        Me.Close()
    End Sub

    Private Sub Ajustement_TextChanged(sender As Object, e As EventArgs) Handles AjustementDevise.TextChanged
        If AjustementDevise.Text.Trim <> "" Then
            AjustementLettreDevise.Text = MontantLettre(CDec(AjustementDevise.Text))
        Else
            AjustementLettreDevise.Text = ""
        End If
    End Sub

    Private Sub AjustementConvert_TextChanged(sender As Object, e As EventArgs) Handles AjustementLocal.TextChanged
        If AjustementLocal.Text.Trim <> "" Then
            TxtAjustementsLettreLocal.Text = MontantLettre(CDec(AjustementLocal.Text))
        Else
            TxtAjustementsLettreLocal.Text = ""
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        TextEdit6.Text = Now.ToShortDateString & " " & Now.ToLongTimeString
    End Sub


End Class