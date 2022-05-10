Imports ClearProject.PassationMarche

Public Class CalculDetaille

    Dim Fiche() As String = {"0", "11", "01", "00"}
    Dim numPage As Decimal = 1
    Dim SomProvis As Decimal = 0

    Private Sub CalculDetaille_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        numPage = 1
        SomProvis = 0
        Presenter(Fiche(numPage))
        Dim lot As String = JugementOffres.CmbNumLot.Text

        If (ReponseDialog <> "") Then
            TxtNomSoumis.Text = ExceptRevue 'Soumissionnaire
            ChargerLesInfos()
        End If
    End Sub

    Private Sub ChargerLesInfos()
        If (ReponseDialog <> "") Then
            SomProvis = 0

            query = "select Monnaie,MontantPropose,MontantAvecMonnaie,ErreurCalcul,SomProvision,PrctRabais,MontantRabais,AjoutOmission,Ajustements,VariationMineure,PrixCorrigeOffre,RangExamDetaille,SigneErreur from T_SoumissionFournisseur where RefSoumis='" & ReponseDialog & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows

                TxtMontLu.Text = IIf(IsNumeric(rw("MontantPropose")) <> "0", AfficherMonnaie(rw("MontantPropose").ToString), "0").ToString
                LblSigneErreur.Text = rw("SigneErreur").ToString
                TxtErrCalcul.Text = IIf(Not IsDBNull(rw("ErreurCalcul")), AfficherMonnaie(rw("ErreurCalcul").ToString), AfficherMonnaie(CalculerErreur(TxtMontLu.Text.Replace(" ", "")).ToString)).ToString
                TxtErrCalcul.Text = IIf(TxtErrCalcul.Text = "", 0, TxtErrCalcul.Text)
                TxtSomProvis.Text = IIf(rw("SomProvision").ToString = "", 0, AfficherMonnaie(rw("SomProvision").ToString))
                TxtMontRabais.Text = IIf(rw("MontantRabais").ToString <> "", AfficherMonnaie(rw("MontantRabais").ToString), "0")
                TxtDeviseOffre.Text = rw("Monnaie").ToString
                TxtTaux.Text = AfficherMonnaie(Taux(rw("Monnaie").ToString))
                TxtAjouts.Text = AfficherMonnaie(rw("AjoutOmission").ToString)
                TxtAjouts.Text = IIf(TxtAjouts.Text = "", 0, TxtAjouts.Text)
                TxtAjustements.Text = AfficherMonnaie(rw("Ajustements").ToString)
                TxtAjustements.Text = IIf(TxtAjustements.Text = "", 0, TxtAjustements.Text)
                TxtVariations.Text = AfficherMonnaie(rw("VariationMineure").ToString)
                TxtVariations.Text = IIf(TxtVariations.Text = "", 0, TxtVariations.Text)

                TxtRang.Text = IIf(Val(rw("RangExamDetaille").ToString) > 0, rw("RangExamDetaille").ToString & IIf(rw("RangExamDetaille").ToString = "1", "er", "ème").ToString, "")
            Next
        End If
    End Sub

    Private Function Taux(ByVal Monnaie As String) As String

        Dim RepTaux As String = "1"
        query = "select TauxDevise from T_Devise where AbregeDevise='" & Monnaie & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            RepTaux = rw("TauxDevise").ToString
        Next
        Return RepTaux

    End Function

    Private Function CalculerErreur(ByVal montant As String) As Decimal

        Dim montReel As Decimal = 0
        If (JugementOffres.TxtTypeMarche.Text = "Fournitures") Then
            query = "select A.PrixUnitaire, B.QteFournit from T_SoumisPrixFourniture as A, T_SpecTechFourniture as B where A.RefSpecFournit=B.RefSpecFournit and RefSoumis='" & ReponseDialog & "'"
        Else
            query = "select MontantItem,Commentaire from T_SoumisPrixItemDQE where RefSoumis='" & ReponseDialog & "'"
        End If

        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows

            If (rw(0).ToString <> "") Then
                If (IsNumeric(rw(0).ToString.Replace(" ", "")) = True) Then
                    If (JugementOffres.TxtTypeMarche.Text = "Fournitures" And IsNumeric(rw(1).ToString.Replace(" ", "")) = True) Then
                        montReel = montReel + (CDec(rw("PrixUnitaire").ToString.Replace(" ", "")) * CDec(rw("QteFournit").ToString.Replace(" ", "")))
                    ElseIf (JugementOffres.TxtTypeMarche.Text = "Fournitures") Then
                        montReel = montReel + (CDec(rw("PrixUnitaire").ToString.Replace(" ", "")))
                    End If

                    If (JugementOffres.TxtTypeMarche.Text = "Travaux") Then
                        If (rw(1).ToString = "Somme provisionnelle") Then
                            SomProvis = SomProvis + CDec(rw(0).ToString.Replace(" ", ""))
                        End If
                    End If
                End If
            End If
        Next

        Dim montLu As Decimal = 0
        If (montant.Replace(" ", "") <> "") Then
            If (IsNumeric(montant.Replace(" ", "")) = True) Then
                montLu = CDec(montant.Replace(" ", ""))
            End If
        End If

        'MsgBox("Montt reel avec provis= " & montReel.ToString, MsgBoxStyle.Information)
        montReel = montReel - SomProvis
        'MsgBox("Montt reel sans provis= " & montReel.ToString, MsgBoxStyle.Information)
        Dim ErrMont As Decimal = 0
        If (montLu > montReel) Then
            LblSigneErreur.Text = "-"
            ErrMont = montLu - montReel
        Else
            LblSigneErreur.Text = "+"
            ErrMont = montReel - montLu
        End If
        Return ErrMont

    End Function

    Private Sub Presenter(ByVal Page As String)

        If (Len(Page) = 2) Then

            SplitPrincipal.Collapsed = CInt(Val(Page(0)))
            SplitSecondaire.Collapsed = CInt(Val(Page(1)))

            If (Page = "11") Then TxtTitreCalcul.Text = "CORRECTIONS ET RABAIS INCONDITIONNELS"
            If (Page = "01") Then TxtTitreCalcul.Text = "CONVERSION MONETAIRE"
            If (Page = "00") Then TxtTitreCalcul.Text = "OMISSIONS, AJUSTEMENTS ET VARIATIONS"

            If (numPage < 2) Then
                BtPrec.Visible = False
                BtSuiv.Visible = True

                BtPrec2.Enabled = False
                BtSuiv2.Enabled = True
            ElseIf (numPage > 2) Then
                BtSuiv.Visible = False
                BtPrec.Visible = True

                BtSuiv2.Enabled = False
                BtPrec2.Enabled = True
            Else
                BtPrec.Visible = True
                BtSuiv.Visible = True

                BtPrec2.Enabled = True
                BtSuiv2.Enabled = True
            End If
            BtNumPage.Text = "PAGE " & numPage.ToString & " / 3"
        End If
    End Sub

    Private Sub BtPrec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtPrec.Click
        numPage = numPage - 1
        Presenter(Fiche(numPage))
    End Sub

    Private Sub BtSuiv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtSuiv.Click
        numPage = numPage + 1
        Presenter(Fiche(numPage))
    End Sub

    Private Sub SplitPrincipal_SplitGroupPanelCollapsed(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.SplitGroupPanelCollapsedEventArgs) Handles SplitPrincipal.SplitGroupPanelCollapsed
        Presenter(Fiche(numPage))
    End Sub

    Private Sub SplitSecondaire_SplitGroupPanelCollapsed(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.SplitGroupPanelCollapsedEventArgs) Handles SplitSecondaire.SplitGroupPanelCollapsed
        Presenter(Fiche(numPage))
    End Sub

    Private Sub BtPrec2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtPrec2.Click
        numPage = numPage - 1
        Presenter(Fiche(numPage))
    End Sub

    Private Sub BtSuiv2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSuiv2.Click
        numPage = numPage + 1
        Presenter(Fiche(numPage))
    End Sub

    Private Sub TxtMontLu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontLu.TextChanged
        If (TxtMontLu.Text.Replace(" ", "") <> "") Then
            TxtMontLuLettre.Text = MontantLettre(TxtMontLu.Text.Replace(" ", ""))
            If (TxtErrCalcul.Text.Replace(" ", "") <> "" And TxtSomProvis.Text.Replace(" ", "") <> "") Then
                Dim montTT As Decimal = 0
                montTT = CDec(TxtMontLu.Text.Replace(" ", "")) + CDec(TxtErrCalcul.Text.Replace(" ", "")) + CDec(TxtSomProvis.Text.Replace(" ", ""))
                TxtPrixCorrige.Text = AfficherMonnaie(montTT.ToString)
            End If
        Else
            TxtMontLuLettre.Text = ""
        End If
    End Sub

    Private Sub TxtErrCalcul_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtErrCalcul.TextChanged
        If (TxtErrCalcul.Text.Replace(" ", "") <> "") Then
            TxtErrCalculLettre.Text = MontantLettre(TxtErrCalcul.Text.Replace(" ", ""))
            If (TxtMontLu.Text.Replace(" ", "") <> "" And TxtSomProvis.Text.Replace(" ", "") <> "") Then
                Dim montTT As Decimal = 0
                If (LblSigneErreur.Text = "-") Then
                    montTT = CDec(TxtMontLu.Text.Replace(" ", "")) - CDec(TxtErrCalcul.Text.Replace(" ", "")) + CDec(TxtSomProvis.Text.Replace(" ", ""))
                Else
                    montTT = CDec(TxtMontLu.Text.Replace(" ", "")) + CDec(TxtErrCalcul.Text.Replace(" ", "")) + CDec(TxtSomProvis.Text.Replace(" ", ""))
                End If
                TxtPrixCorrige.Text = AfficherMonnaie(montTT.ToString)
            End If
        Else
            TxtErrCalculLettre.Text = ""
        End If
    End Sub

    Private Sub TxtSomProvis_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtSomProvis.TextChanged
        If (TxtSomProvis.Text.Replace(" ", "") <> "") Then
            TxtSomProvisLettre.Text = MontantLettre(TxtSomProvis.Text.Replace(" ", ""))
            If (TxtMontLu.Text.Replace(" ", "") <> "" And TxtErrCalcul.Text.Replace(" ", "") <> "") Then
                Dim montTT As Decimal = 0
                If (LblSigneErreur.Text = "-") Then
                    montTT = CDec(TxtMontLu.Text.Replace(" ", "")) - CDec(TxtErrCalcul.Text.Replace(" ", "")) + CDec(TxtSomProvis.Text.Replace(" ", ""))
                Else
                    montTT = CDec(TxtMontLu.Text.Replace(" ", "")) + CDec(TxtErrCalcul.Text.Replace(" ", "")) + CDec(TxtSomProvis.Text.Replace(" ", ""))
                End If
                TxtPrixCorrige.Text = AfficherMonnaie(montTT.ToString)
            End If
        Else
            TxtSomProvisLettre.Text = ""
        End If
    End Sub

    Private Sub TxtPrixCorrige_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPrixCorrige.TextChanged
        If (TxtPrixCorrige.Text.Replace(" ", "") <> "0") Then
            TxtPrixCorrigeLettre.Text = MontantLettre(TxtPrixCorrige.Text.Replace(" ", ""))
            Dim prixRab As Decimal = 0
            If (TxtMontRabais.Text.Replace(" ", "") <> "") Then
                prixRab = CDec(TxtPrixCorrige.Text.Replace(" ", "")) - CDec(TxtMontRabais.Text.Replace(" ", ""))
                TxtPrixCorRabais.Text = AfficherMonnaie(prixRab.ToString)
            End If
        Else
            TxtPrixCorrigeLettre.Text = ""
        End If
    End Sub

    Private Sub TxtMontRabais_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontRabais.TextChanged
        If (TxtMontRabais.Text.Replace(" ", "") <> "") Then
            TxtRabaisLettre.Text = MontantLettre(TxtMontRabais.Text.Replace(" ", ""))
            Dim prixRab As Decimal = 0
            Dim prcrab As Decimal = 0
            If TxtPrixCorrige.Text.Replace(" ", "") <> "" Then
                If CDec(TxtMontRabais.Text.Replace(" ", "")) <= CDec(TxtPrixCorrige.Text.Replace(" ", "")) Then
                    BtEnrgCalcul.Enabled = True
                    TxtMontRabais.ForeColor = Color.Black
                    prixRab = CDec(TxtPrixCorrige.Text.Replace(" ", "")) - CDec(TxtMontRabais.Text.Replace(" ", ""))
                    TxtPrixCorRabais.Text = AfficherMonnaie(prixRab.ToString)
                    If CDec(TxtPrixCorrige.Text.Replace(" ", "")) <> 0 Then     'TxtMontRabais.Focused = True And 
                        prcrab = CDec(TxtMontRabais.Text.Replace(" ", "")) * 100 / CDec(TxtPrixCorrige.Text.Replace(" ", ""))
                        TxtPrctRabais.Text = Math.Round(prcrab, 2).ToString.Replace(".", ",")
                    End If
                Else
                    TxtMontRabais.ForeColor = Color.Red
                    BtEnrgCalcul.Enabled = False
                End If
            End If
        Else
            TxtRabaisLettre.Text = ""
            TxtPrctRabais.Text = "0"
        End If
    End Sub

    Private Sub TxtPrixCorRabais_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPrixCorRabais.TextChanged
        If (TxtPrixCorRabais.Text.Replace(" ", "") <> "") Then
            TxtPrixCorRabais2.Text = TxtPrixCorRabais.Text
            'TxtPrixCorRabais3.Text = TxtPrixCorRabais.Text
            TxtPrixCorRabaisLettre.Text = MontantLettre(TxtPrixCorRabais.Text.Replace(" ", ""))
            TxtPrixCorRabaisLettre2.Text = MontantLettre(TxtPrixCorRabais.Text.Replace(" ", ""))
            'TxtPrixCorRabaisLettre3.Text = MontantLettre(TxtPrixCorRabais.Text.Replace(" ", ""))
        Else
            TxtPrixCorRabais2.Text = ""
            TxtPrixCorRabais3.Text = ""
            TxtPrixCorRabaisLettre.Text = ""
            TxtPrixCorRabaisLettre2.Text = ""
            'TxtPrixCorRabaisLettre3.Text = ""
        End If
    End Sub

    Private Sub TxtPrctRabais_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPrctRabais.TextChanged
        If (TxtPrctRabais.Text.Replace(" ", "") <> "") Then
            If (IsNumeric(TxtPrctRabais.Text.Replace(" ", "")) = True) Then
                TxtPrctRabais.ForeColor = Color.Black
                BtEnrgCalcul.Enabled = True
                If (CDec(TxtPrctRabais.Text.Replace(" ", "")) <= 100) Then
                    TxtPrctRabais.ForeColor = Color.Black
                    BtEnrgCalcul.Enabled = True

                    Dim prct As Decimal = 0
                    If (TxtPrixCorrige.Text.Replace(" ", "") <> "") Then
                        prct = (CDec(TxtPrixCorrige.Text.Replace(" ", "")) * CDec(TxtPrctRabais.Text.Replace(" ", ""))) / 100
                        TxtMontRabais.Text = AfficherMonnaie(Math.Round(prct, 0).ToString)
                    End If
                Else
                    TxtPrctRabais.ForeColor = Color.Red
                    BtEnrgCalcul.Enabled = False
                End If
            Else
                TxtPrctRabais.ForeColor = Color.Red
                BtEnrgCalcul.Enabled = False
            End If
        Else
            TxtMontRabais.Text = "0"
        End If
    End Sub

    Private Sub TxtTaux_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtTaux.TextChanged
        If (TxtTaux.Text.Replace(" ", "") <> "") Then
            TxtTauxLettre.Text = MontantLettre(TxtTaux.Text.Replace(" ", ""))

            Dim produit As Decimal = 0
            If (TxtPrixCorRabais2.Text.Replace(" ", "") <> "") Then
                produit = CDec(TxtPrixCorRabais2.Text.Replace(" ", "")) * CDec(TxtTaux.Text.Replace(" ", ""))
            End If
            TxtPrixEnDevise.Text = AfficherMonnaie(Math.Round(produit, 0).ToString)
        Else
            TxtTauxLettre.Text = ""
            TxtPrixEnDevise.Text = TxtPrixCorRabais2.Text
        End If
    End Sub

    Private Sub TxtPrixEnDevise_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPrixEnDevise.TextChanged
        If (TxtPrixEnDevise.Text.Replace(" ", "") <> "") Then
            TxtPrixEnDeviseLettre.Text = MontantLettre(TxtPrixEnDevise.Text.Replace(" ", ""))
            TxtPrixCorRabais3.Text = TxtPrixEnDevise.Text
            TxtPrixCorRabaisLettre3.Text = MontantLettre(TxtPrixEnDevise.Text.Replace(" ", ""))
        Else
            TxtPrixEnDeviseLettre.Text = ""
            TxtPrixCorRabaisLettre3.Text = ""
        End If
    End Sub

    Private Sub PrixTotal()
        If (TxtAjouts.Text.Replace(" ", "") <> "" And TxtAjustements.Text.Replace(" ", "") <> "" And TxtVariations.Text.Replace(" ", "") <> "" And TxtPrixCorRabais3.Text.Replace(" ", "") <> "") Then
            Dim pTot As Decimal = CDec(TxtAjouts.Text.Replace(" ", "")) + CDec(TxtAjustements.Text.Replace(" ", "")) + CDec(TxtVariations.Text.Replace(" ", "")) + CDec(TxtPrixCorRabais3.Text.Replace(" ", ""))
            TxtPrixTotal.Text = AfficherMonnaie(pTot.ToString)
            BtEnrgCalcul.Enabled = True
        Else
            TxtPrixTotal.Text = ""
            BtEnrgCalcul.Enabled = False
        End If
    End Sub

    Private Sub TxtAjouts_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtAjouts.TextChanged
        If (TxtAjouts.Text.Replace(" ", "") <> "") Then
            TxtAjoutsLettre.Text = MontantLettre(TxtAjouts.Text.Replace(" ", ""))
        Else
            TxtAjoutsLettre.Text = ""
        End If
        PrixTotal()
    End Sub

    Private Sub TxtAjustements_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtAjustements.TextChanged
        If (TxtAjustements.Text.Replace(" ", "") <> "") Then
            TxtAjustementsLettre.Text = MontantLettre(TxtAjustements.Text.Replace(" ", ""))
        Else
            TxtAjustementsLettre.Text = ""
        End If
        PrixTotal()
    End Sub

    Private Sub TxtVariations_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtVariations.TextChanged
        If (TxtVariations.Text.Replace(" ", "") <> "") Then
            TxtVariationsLettre.Text = MontantLettre(TxtVariations.Text.Replace(" ", ""))
        Else
            TxtVariationsLettre.Text = ""
        End If
        PrixTotal()
    End Sub

    Private Sub TxtPrixTotal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPrixTotal.TextChanged
        If (TxtPrixTotal.Text.Replace(" ", "") <> "") Then
            TxtPrixTotalLettre.Text = MontantLettre(TxtPrixTotal.Text.Replace(" ", ""))
            BtEnrgCalcul.Enabled = True
        Else
            TxtPrixTotalLettre.Text = ""
            BtEnrgCalcul.Enabled = False
        End If
    End Sub

    Private Sub BtEnrgCalcul_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgCalcul.Click
        If (TxtPrixTotal.Text.Replace(" ", "") <> "") Then
            DebutChargement(True, "Traitement en cours...")

            Dim Verif As String = ""
            Dim Resultat As Object() = GetSousLot(JugementOffres.CmbNumLot.Text, JugementOffres.CmbNumDoss.Text)
            Dim nbsouslot As Integer = Val(Resultat(0))
            If nbsouslot > 0 Then

                If JugementOffres.AttributionMarche = "Lot" Then
                    query = "update t_soumissionfournisseur set VariationMineure='" & IIf(TxtVariations.Text.Replace(" ", "") <> "", CDbl(TxtVariations.Text.Replace(" ", "")), "0") & "', Ajustements='" & IIf(TxtAjustements.Text.Replace(" ", "") <> "", CDbl(TxtAjustements.Text.Replace(" ", "")), "0").ToString & "', AjoutOmission='" & IIf(TxtAjouts.Text.Replace(" ", "") <> "", CDbl(TxtAjouts.Text.Replace(" ", "")), "0").ToString & "', MontantRabais='" & IIf(TxtMontRabais.Text.Replace(" ", "") <> "", CDbl(TxtMontRabais.Text.Replace(" ", "")), "0").ToString & "', PrctRabais='" & IIf(TxtPrctRabais.Text.Replace(" ", "").Replace(",", ".") <> "", TxtPrctRabais.Text.Replace(" ", "").Replace(",", "."), "0").ToString & "', SomProvision='" & IIf(TxtSomProvis.Text.Replace(" ", "") <> "", CDbl(TxtSomProvis.Text.Replace(" ", "")), "0").ToString & "', SigneErreur='" & LblSigneErreur.Text & "', MontantAvecMonnaie='" & IIf(TxtPrixEnDevise.Text.Replace(" ", "") <> "", CDbl(TxtPrixEnDevise.Text.Replace(" ", "")), "0").ToString & "', PrixCorrigeOffre='" & IIf(TxtPrixTotal.Text.Replace(" ", "") <> "", CDbl(TxtPrixTotal.Text.Replace(" ", "")), "0").ToString & "', ErreurCalcul='" & IIf(TxtErrCalcul.Text.Replace(" ", "") <> "", CDbl(TxtErrCalcul.Text.Replace(" ", "")), "0").ToString & "' where RefSoumis='" & ReponseDialog & "'" 'RangExamDetaille
                    ExecuteNonQuery(query)
                Else
                    query = "update t_soumissionfournisseur set VariationMineure='" & IIf(TxtVariations.Text.Replace(" ", "") <> "", CDbl(TxtVariations.Text.Replace(" ", "")), "0") & "', Ajustements='" & IIf(TxtAjustements.Text.Replace(" ", "") <> "", CDbl(TxtAjustements.Text.Replace(" ", "")), "0").ToString & "', AjoutOmission='" & IIf(TxtAjouts.Text.Replace(" ", "") <> "", CDbl(TxtAjouts.Text.Replace(" ", "")), "0").ToString & "', MontantRabais='" & IIf(TxtMontRabais.Text.Replace(" ", "") <> "", CDbl(TxtMontRabais.Text.Replace(" ", "")), "0").ToString & "', PrctRabais='" & IIf(TxtPrctRabais.Text.Replace(" ", "").Replace(",", ".") <> "", TxtPrctRabais.Text.Replace(" ", "").Replace(",", "."), "0").ToString & "', SomProvision='" & IIf(TxtSomProvis.Text.Replace(" ", "") <> "", CDbl(TxtSomProvis.Text.Replace(" ", "")), "0").ToString & "', SigneErreur='" & LblSigneErreur.Text & "', MontantAvecMonnaie='" & IIf(TxtPrixEnDevise.Text.Replace(" ", "") <> "", CDbl(TxtPrixEnDevise.Text.Replace(" ", "")), "0").ToString & "', PrixCorrigeOffre='" & IIf(TxtPrixTotal.Text.Replace(" ", "") <> "", CDbl(TxtPrixTotal.Text.Replace(" ", "")), "0").ToString & "', ErreurCalcul='" & IIf(TxtErrCalcul.Text.Replace(" ", "") <> "", CDbl(TxtErrCalcul.Text.Replace(" ", "")), "0").ToString & "' where RefSoumis='" & ReponseDialog & "'"
                    ExecuteNonQuery(query)
                End If
            Else
                query = "update t_soumissionfournisseur set VariationMineure='" & IIf(TxtVariations.Text.Replace(" ", "") <> "", CDbl(TxtVariations.Text.Replace(" ", "")), "0") & "', Ajustements='" & IIf(TxtAjustements.Text.Replace(" ", "") <> "", CDbl(TxtAjustements.Text.Replace(" ", "")), "0").ToString & "', AjoutOmission='" & IIf(TxtAjouts.Text.Replace(" ", "") <> "", CDbl(TxtAjouts.Text.Replace(" ", "")), "0").ToString & "', MontantRabais='" & IIf(TxtMontRabais.Text.Replace(" ", "") <> "", CDbl(TxtMontRabais.Text.Replace(" ", "")), "0").ToString & "', PrctRabais='" & IIf(TxtPrctRabais.Text.Replace(" ", "").Replace(",", ".") <> "", TxtPrctRabais.Text.Replace(" ", "").Replace(",", "."), "0").ToString & "', SomProvision='" & IIf(TxtSomProvis.Text.Replace(" ", "") <> "", CDbl(TxtSomProvis.Text.Replace(" ", "")), "0").ToString & "', SigneErreur='" & LblSigneErreur.Text & "', MontantAvecMonnaie='" & IIf(TxtPrixEnDevise.Text.Replace(" ", "") <> "", CDbl(TxtPrixEnDevise.Text.Replace(" ", "")), "0").ToString & "', PrixCorrigeOffre='" & IIf(TxtPrixTotal.Text.Replace(" ", "") <> "", CDbl(TxtPrixTotal.Text.Replace(" ", "")), "0").ToString & "', ErreurCalcul='" & IIf(TxtErrCalcul.Text.Replace(" ", "") <> "", CDbl(TxtErrCalcul.Text.Replace(" ", "")), "0").ToString & "' where RefSoumis='" & ReponseDialog & "'"
                ExecuteNonQuery(query)
            End If
            JugementOffres.ChargerExamDetaille()
            JugementOffres.OffresTraitees()
            FinChargement()
            SuccesMsg("Traitement effectué avec succès.")
            ReponseDialog = ""
            Me.Close()
        End If
    End Sub

    Private Sub TxtPrixCorRabais2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPrixCorRabais2.TextChanged
        If (TxtPrixCorRabais2.Text.Replace(" ", "") <> "") Then

            Dim produit As Decimal = 0
            If (TxtTaux.Text.Replace(" ", "") <> "") Then
                produit = CDec(TxtPrixCorRabais2.Text.Replace(" ", "")) * CDec(TxtTaux.Text.Replace(" ", ""))
            End If
            TxtPrixEnDevise.Text = AfficherMonnaie(Math.Round(produit, 0).ToString)

        End If
    End Sub

    Private Sub TxtPrixCorRabais3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPrixCorRabais3.TextChanged
        PrixTotal()
    End Sub
End Class