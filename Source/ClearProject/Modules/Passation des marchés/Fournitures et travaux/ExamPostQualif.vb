Imports MySql.Data.MySqlClient
Imports ClearProject.PassationMarche

Public Class ExamPostQualif

    Public RefSoumis As Integer = 0
    Dim pageActu As Decimal = 0
    Dim SoumElimine As Boolean = False
    Dim nbCritNon As Decimal = 0
    Dim NbreCritDisqualifier As Decimal = 0
    Dim verdictDefinitif As Boolean = False
    Dim TableDonne As New DataTable

    Private Sub ExamPostQualif_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        pageActu = 0
        PnlDecision.Visible = False

        If (ReponseDialog <> "") Then
            TxtNomSoumis.Text = ExceptRevue
            AfficherCritere(pageActu)
            SoumElimine = False
            nbCritNon = 0
            GetCreatColum()
        End If
    End Sub

    Private Sub GetCreatColum()
        TableDonne.Columns.Clear()
        TableDonne.Rows.Clear()
        TableDonne.Columns.Add("RefCritere")
        TableDonne.Columns.Add("Verdict")
        TableDonne.Columns.Add("Commentaire")
    End Sub

    Private Sub AfficherCritere(ByVal Numero As Decimal)

        If (Numero <= JugementOffres.NombreCritere - 1) Then
            BtContinuer.Text = "CONTINUER"

            LblCritereNum.Text = "CRITERE N°" & (Numero + 1).ToString
            If (JugementOffres.CritereElimine(Numero) = "OUI") Then
                LblElimine.Visible = True
            Else
                LblElimine.Visible = False
            End If

            TxtCritere.Text = JugementOffres.TableCritere(Numero)
            TxtGroupeCritere.Text = JugementOffres.GroupeCritere(Numero)

            query = "select Verdict,Commentaire from T_SoumisFournisPostQualif where CodeFournis='" & ReponseDialog & "' and RefCritere='" & JugementOffres.CodeCritere(Numero) & "' AND CodeLot='" & JugementOffres.CmbNumLot.Text & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                If dt.Rows.Count > 0 Then
                    If (rw("Verdict").ToString = "Satisfait") Then
                        RdConforme.Checked = True

                    ElseIf (rw("Verdict").ToString = "Ne satisfait pas") Then
                        RdNonConforme.Checked = True
                    Else
                        RdConforme.Checked = False
                        RdNonConforme.Checked = False
                    End If
                    TxtCommentaire.Text = IIf(rw("Commentaire").ToString <> "", MettreApost(rw("Commentaire").ToString), "")
                End If
            Next
        End If
    End Sub

    Private Sub RdConforme_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdConforme.CheckedChanged, RdNonConforme.CheckedChanged
        VerifDecision()
    End Sub

    Private Sub VerifDecision()
        If (RdConforme.Checked = True Or RdNonConforme.Checked = True) Then
            BtContinuer.Enabled = True
        Else
            BtContinuer.Enabled = False
        End If
    End Sub

    Private Sub BtContinuer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtContinuer.Click
        Try

            If (pageActu <= JugementOffres.NombreCritere - 1) Then
                If RdConforme.Checked = False And RdNonConforme.Checked = False Then
                    SuccesMsg("Veuillez choisir une option :" & vbNewLine & " - [Satisfait au critère]" & vbNewLine & " - [Ne satisfait pas au critère]")
                    Exit Sub
                End If

                Dim DonneAjouter As Boolean = False
                If TableDonne.Rows.Count > 0 Then
                    For Each rw In TableDonne.Rows
                        If rw("RefCritere") = JugementOffres.CodeCritere(pageActu) Then
                            rw("RefCritere") = JugementOffres.CodeCritere(pageActu)
                            rw("Verdict") = IIf(RdConforme.Checked = True, "Satisfait", "Ne satisfait pas").ToString
                            rw("Commentaire") = TxtCommentaire.Text
                            DonneAjouter = True
                            Exit For
                        End If
                    Next
                End If

                If DonneAjouter = False Then
                    Dim dt As DataTable = TableDonne
                    Dim Drx = dt.NewRow
                    Drx("RefCritere") = JugementOffres.CodeCritere(pageActu)
                    Drx("Verdict") = IIf(RdConforme.Checked = True, "Satisfait", "Ne satisfait pas").ToString
                    Drx("Commentaire") = TxtCommentaire.Text
                    TableDonne.Rows.Add(Drx)
                End If

                ' ExecuteNonQuery("update T_SoumisFournisPostQualif set Verdict='" & IIf(RdConforme.Checked = True, "Satisfait", "Ne satisfait pas").ToString & "', Commentaire='" & EnleverApost(TxtCommentaire.Text) & "' where CodeFournis='" & ReponseDialog & "' and RefCritere='" & JugementOffres.CodeCritere(pageActu) & "' AND CodeLot='" & JugementOffres.CmbNumLot.Text & "'")

                If (RdNonConforme.Checked = True And LblElimine.Visible = True) Then
                    SoumElimine = True
                    NbreCritDisqualifier += 1
                    nbCritNon += 1
                ElseIf (RdNonConforme.Checked = True) Then
                    nbCritNon += 1
                End If

                pageActu += 1
                BTPrecedent.Enabled = True

                If (pageActu < JugementOffres.NombreCritere) Then
                    AfficherCritere(pageActu)
                ElseIf (PnlDecision.Visible = False) Then
                    BtContinuer.Text = "VALIDER"
                    PnlDecision.Visible = True
                    GbExplications.Visible = False

                    If (nbCritNon = 0) Then
                        TxtComVerdict.Text = "Le soumissionnaire satisfait à tous les critères de post qualification."
                        BtOui.Visible = True
                        BtNon.Visible = False
                        PnlModif.Visible = False
                        verdictDefinitif = True
                    ElseIf (nbCritNon = 1) Then
                        If (SoumElimine = True) Then
                            TxtComVerdict.Text = "Le soumissionnaire ne satisfait pas à un (1) critère éliminatoire, il ne peut donc être post qualifié."
                            BtOui.Visible = False
                            BtNon.Visible = True
                            PnlModif.Visible = False
                            verdictDefinitif = True
                        Else
                            TxtComVerdict.Text = "Le soumissionnaire ne satisfait pas à un (1) critère, il est donc 'provisoirement disqualifié'. Vous avez la possibilité de reconsidérer le verdict."
                            BtOui.Visible = False
                            BtNon.Visible = True
                            PnlModif.Visible = True
                            verdictDefinitif = False
                        End If
                    ElseIf (nbCritNon > 1) Then
                        If (SoumElimine = True) Then
                            TxtComVerdict.Text = "Le soumissionnaire ne satisfait pas à " & MontantLettre(nbCritNon.ToString) & " (" & nbCritNon.ToString & ") critères dont au moins un (1) critère éliminatoire, il ne peut donc être post qualifié."
                            BtOui.Visible = False
                            BtNon.Visible = True
                            PnlModif.Visible = False
                            verdictDefinitif = True
                        Else
                            TxtComVerdict.Text = "Le soumissionnaire ne satisfait pas à " & MontantLettre(nbCritNon.ToString) & " (" & nbCritNon.ToString & ") critères, il est donc 'provisoirement disqualifié'. Vous avez la possibilité de reconsidérer le verdict."
                            BtOui.Visible = False
                            BtNon.Visible = True
                            PnlModif.Visible = True
                            verdictDefinitif = False
                        End If
                    End If
                End If

            ElseIf (PnlDecision.Visible = True) Then

                If ((GbExplications.Visible = True And TxtExplications.Text <> "") Or GbExplications.Visible = False) Then
                    DebutChargement(True, "Traitement en cours...")

                    If TableDonne.Rows.Count > 0 Then
                        For Each rw In TableDonne.Rows
                            ExecuteNonQuery("update T_SoumisFournisPostQualif set Verdict='" & rw("Verdict").ToString & "', Commentaire='" & EnleverApost(rw("Commentaire").ToString) & "' where CodeFournis='" & ReponseDialog & "' and RefCritere='" & rw("RefCritere") & "' AND CodeLot='" & JugementOffres.CmbNumLot.Text & "'")
                        Next
                    End If

                    Dim JustifPQValide As String = IIf(GbExplications.Visible = True, EnleverApost(TxtExplications.Text), IIf(BtOui.Visible = True, "Satisfait à tous les critères", "Ne satisfait pas à " & MontantLettre(nbCritNon.ToString) & " (" & nbCritNon.ToString & ") " & IIf(nbCritNon > 1, "critères.", "critère.").ToString).ToString).ToString

                    ExecuteNonQuery("update t_soumissionfournisseurclassement set ExamPQValide = '" & IIf(BtOui.Visible = True, "OUI", "NON").ToString & "', JustifPQValide='" & JustifPQValide.ToString & "' where CodeFournis='" & ReponseDialog & "' AND CodeLot='" & JugementOffres.CmbNumLot.Text & "' AND NumeroDAO='" & EnleverApost(JugementOffres.CmbNumDoss.Text) & "'")
                    JugementOffres.ChargerExamPostQualif()
                    TxtExplications.Text = ""
                    ReponseDialog = ""
                    FinChargement()
                    SuccesMsg("Traitement effectué avec succès.")
                    Me.Close()
                Else
                    SuccesMsg("Le changement de verdict necessite des explications!")
                End If
            End If

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub CaseOui_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CaseOui.Click
        If (verdictDefinitif = False) Then
            If (nbCritNon > 0) Then
                GbExplications.Visible = True
            Else
                GbExplications.Visible = False
            End If
            BtOui.Visible = True
            BtNon.Visible = False

        End If
    End Sub

    Private Sub CaseNon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CaseNon.Click
        If (verdictDefinitif = False) Then
            If (nbCritNon <= 0) Then
                GbExplications.Visible = True
            Else
                GbExplications.Visible = False
            End If
            BtOui.Visible = False
            BtNon.Visible = True

        End If
    End Sub

    Private Sub ExamPostQualif_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ReponseDialog <> "" Then
            If pageActu > 0 And TableDonne.Rows.Count > 0 Then
                If ConfirmMsg("La fermeture du formulaire annulera toutes les modifications apportées." & vbNewLine & "Etes-vous sûrs de vouloir continuer ?") = DialogResult.No Then
                    e.Cancel = True
                End If
            End If
            ReponseDialog = ""
        End If
    End Sub

    Private Sub BTPrecedent_Click(sender As Object, e As EventArgs) Handles BTPrecedent.Click
        Try
            If pageActu > 0 And TableDonne.Rows.Count > 0 Then

                pageActu -= 1

                If pageActu = 0 Then BTPrecedent.Enabled = False

                LblCritereNum.Text = "CRITERE N°" & (pageActu + 1).ToString
                If (JugementOffres.CritereElimine(pageActu) = "OUI") Then
                    LblElimine.Visible = True
                Else
                    LblElimine.Visible = False
                End If

                TxtCritere.Text = JugementOffres.TableCritere(pageActu)
                TxtGroupeCritere.Text = JugementOffres.GroupeCritere(pageActu)

                RdConforme.Checked = IIf(TableDonne.Rows.Item(pageActu)("Verdict") = "Satisfait", True, False).ToString
                RdNonConforme.Checked = IIf(TableDonne.Rows.Item(pageActu)("Verdict") = "Ne satisfait pas", True, False).ToString
                TxtCommentaire.Text = TableDonne.Rows.Item(pageActu)("Commentaire").ToString

                If (RdNonConforme.Checked = True And LblElimine.Visible = True) Then
                    If NbreCritDisqualifier > 0 Then
                        NbreCritDisqualifier -= 1
                        If NbreCritDisqualifier = 0 Then SoumElimine = False
                    End If
                    If nbCritNon > 0 Then nbCritNon -= 1

                ElseIf (RdNonConforme.Checked = True) Then
                    If nbCritNon > 0 Then nbCritNon -= 1
                End If

                If PnlDecision.Visible = True Then PnlDecision.Visible = False
                If PnlModif.Visible = True Then PnlModif.Visible = False
                If BtContinuer.Text = "VALIDER" Then BtContinuer.Text = "CONTINUER"
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub
End Class