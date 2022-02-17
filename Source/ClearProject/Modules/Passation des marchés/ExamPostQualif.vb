Imports MySql.Data.MySqlClient
Imports ClearProject.PassationMarche

Public Class ExamPostQualif
    Public RefSoumis As Integer = 0
    Dim pageActu As Decimal = 0
    Dim SoumElimine As Boolean = False
    Dim nbCritNon As Decimal = 0
    Dim verdictDefinitif As Boolean = False

    Private Sub ExamPostQualif_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        pageActu = 0
        PnlDecision.Visible = False

        If (ReponseDialog <> "") Then
            TxtNomSoumis.Text = ExceptRevue
            AfficherCritere(pageActu)
            SoumElimine = False
            nbCritNon = 0



        End If
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
                    If (rw(0).ToString = "Satisfait") Then
                        RdConforme.Checked = True
                    ElseIf (rw(0).ToString = "Ne satisfait pas") Then
                        RdNonConforme.Checked = True
                    Else
                        RdConforme.Checked = False
                        RdNonConforme.Checked = False
                    End If

                    TxtCommentaire.Text = IIf(rw(1).ToString <> "", MettreApost(rw(1).ToString), "")

                End If
            Next

        End If

    End Sub

    Private Sub RdConforme_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdConforme.CheckedChanged
        VerifDecision()
    End Sub

    Private Sub VerifDecision()
        If (RdConforme.Checked = True Or RdNonConforme.Checked = True) Then
            BtContinuer.Enabled = True
        Else
            BtContinuer.Enabled = False
        End If
    End Sub

    Private Sub RdNonConforme_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdNonConforme.CheckedChanged
        VerifDecision()
    End Sub

    Private Sub BtContinuer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtContinuer.Click



        If (pageActu <= JugementOffres.NombreCritere - 1) Then


            query = "update T_SoumisFournisPostQualif set Verdict='" & IIf(RdConforme.Checked = True, "Satisfait", "Ne satisfait pas").ToString & "', Commentaire='" & EnleverApost(TxtCommentaire.Text) & "' where CodeFournis='" & ReponseDialog & "' and RefCritere='" & JugementOffres.CodeCritere(pageActu) & "' AND CodeLot='" & JugementOffres.CmbNumLot.Text & "'"
            ExecuteNonQuery(query)


            If (RdNonConforme.Checked = True And LblElimine.Visible = True) Then
                SoumElimine = True
                nbCritNon += 1
            ElseIf (RdNonConforme.Checked = True) Then
                nbCritNon += 1
            End If

            pageActu += 1
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
                    'GbExplications.Visible = False
                ElseIf (nbCritNon = 1) Then
                    If (SoumElimine = True) Then
                        TxtComVerdict.Text = "Le soumissionnaire ne satisfait pas à un (1) critère éliminatoire, il ne peut donc être post qualifié."
                        BtOui.Visible = False
                        BtNon.Visible = True
                        PnlModif.Visible = False
                        verdictDefinitif = True
                        'GbExplications.Visible = False
                    Else
                        TxtComVerdict.Text = "Le soumissionnaire ne satisfait pas à un (1) critère, il est donc 'provisoirement disqualifié'. Vous avez la possibilité de reconsidérer le verdict."
                        BtOui.Visible = False
                        BtNon.Visible = True
                        PnlModif.Visible = True
                        verdictDefinitif = False
                        'GbExplications.Visible = True
                    End If
                ElseIf (nbCritNon > 1) Then
                    If (SoumElimine = True) Then
                        TxtComVerdict.Text = "Le soumissionnaire ne satisfait pas à " & MontantLettre(nbCritNon.ToString) & " (" & nbCritNon.ToString & ") critères dont au moins un (1) critère éliminatoire, il ne peut donc être post qualifié."
                        BtOui.Visible = False
                        BtNon.Visible = True
                        PnlModif.Visible = False
                        verdictDefinitif = True
                        'GbExplications.Visible = False
                    Else
                        TxtComVerdict.Text = "Le soumissionnaire ne satisfait pas à " & MontantLettre(nbCritNon.ToString) & " (" & nbCritNon.ToString & ") critères, il est donc 'provisoirement disqualifié'. Vous avez la possibilité de reconsidérer le verdict."
                        BtOui.Visible = False
                        BtNon.Visible = True
                        PnlModif.Visible = True
                        verdictDefinitif = False
                        'GbExplications.Visible = True
                    End If

                End If

            End If


        ElseIf (PnlDecision.Visible = True) Then

            If ((GbExplications.Visible = True And TxtExplications.Text <> "") Or GbExplications.Visible = False) Then

                Dim DatSet = New DataSet
                'query = "select * from T_Fournisseur where CodeFournis='" & ReponseDialog & "'"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                'DatAdapt.Fill(DatSet, "T_Fournisseur")

                'DatSet.Tables!T_Fournisseur.Rows(0)!PostQualifie = IIf(BtOui.Visible = True, "OUI", "NON").ToString
                'DatSet.Tables!T_Fournisseur.Rows(0)!JustifPostQualif = EnleverApost(TxtExplications.Text)

                'DatAdapt.Update(DatSet, "T_Fournisseur")
                'DatSet.Clear()

                'Post Qualif T_Soumission
                DatSet = New DataSet
                'query = "select * from T_SoumissionFournisseur where CodeFournis='" & ReponseDialog & "'"
                query = "select * from t_soumissionfournisseurclassement  where CodeFournis='" & ReponseDialog & "' AND CodeLot='" & JugementOffres.CmbNumLot.Text & "' AND CodeSousLot='" & JugementOffres.cmbSousLot.Text & "' AND NumeroDAO='" & JugementOffres.CmbNumDoss.Text & "'"

                'Dim Resultat As Object() = GetSousLot(JugementOffres.CmbNumLot.Text, JugementOffres.CmbNumDoss.Text)
                'Dim nbsouslot As Integer = Val(Resultat(0))
                'If nbsouslot > 0 Then
                '    query = "select * from t_soumissionfournisseurexamdetail where RefSoumis='" & RefSoumis & "'"
                'Else
                '    query = "select * from T_SoumissionFournisseur where RefSoumis='" & RefSoumis & "'"
                'End If
                Cmd = New MySqlCommand(query, sqlconn)
                DatAdapt = New MySqlDataAdapter(Cmd)
                CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Fill(DatSet, "t_soumissionfournisseurclassement")

                DatSet.Tables!t_soumissionfournisseurclassement.Rows(0)!ExamPQValide = IIf(BtOui.Visible = True, "OUI", "NON").ToString
                DatSet.Tables!t_soumissionfournisseurclassement.Rows(0)!JustifPQValide = IIf(GbExplications.Visible = True, EnleverApost(TxtExplications.Text), IIf(BtOui.Visible = True, "Satisfait à tous les critères", "Ne satisfait pas à " & MontantLettre(nbCritNon.ToString) & " (" & nbCritNon.ToString & ") " & IIf(nbCritNon > 1, "critères.", "critère.").ToString).ToString).ToString

                DatAdapt.Update(DatSet, "t_soumissionfournisseurclassement")
                DatSet.Clear()
                BDQUIT(sqlconn)

                TxtExplications.Text = ""
                SuccesMsg("Traitement effectué avec succès!")
                ReponseDialog = ""
                Me.Close()
            Else
                SuccesMsg("Le changement de verdict necessite des explications!")
            End If

        End If



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

End Class