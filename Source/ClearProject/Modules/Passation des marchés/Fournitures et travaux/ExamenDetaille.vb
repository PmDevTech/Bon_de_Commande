Imports MySql.Data.MySqlClient

Public Class ExamenDetaille

    Private Sub ExamenDetaille_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        TxtNomSoumis.Text = ExceptRevue
        PrendreInfos(ReponseDialog)
        InfosProvenance(ReponseDialog)

        If (CommVerification.Text = "Commentaire") Then CommVerification.ForeColor = Color.Silver
        If (CommExhaustif.Text = "Commentaire") Then CommExhaustif.ForeColor = Color.Silver
        If (CommEssentiel.Text = "Commentaire") Then CommEssentiel.ForeColor = Color.Silver

    End Sub

    Private Sub PrendreInfos(ByVal leSoumis As String)

        query = "select Verification,JustifVerification,ConformiteTechnique,ConformiteGarantie,CautionBancaire,ExhaustiviteOffre,JustifExhaustivite,ConformiteEssentiel,JustifConformEss,AcceptationExamDetaille from T_SoumissionFournisseur where RefSoumis='" & leSoumis & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows

            If (rw(0).ToString = "OUI") Then
                VerifOUI.Checked = True
                CommVerification.Text = MettreApost(rw(1).ToString)
            ElseIf (rw(0).ToString = "NON") Then
                VerifNON.Checked = True
                CommVerification.Text = MettreApost(rw(1).ToString)
            Else
                VerifOUI.Checked = False
                VerifNON.Checked = False
                CommVerification.Text = "Commentaire"
            End If

            If (rw(2).ToString = "OUI") Then
                SpecOUI.Checked = True
                CommSpecTech.Text = MettreApost(JustifSpecTech(leSoumis, "OUI"))
            ElseIf (rw(2).ToString = "NON") Then
                SpesqlconnON.Checked = True
                CommSpecTech.Text = MettreApost(JustifSpecTech(leSoumis, "NON"))
            End If

            If (rw(3).ToString = "OUI") Then
                GaranOUI.Checked = True
            ElseIf (rw(3).ToString = "NON") Then
                GaranNON.Checked = True
            End If
            CommGarantie.Text = MettreApost(JustifGarant(leSoumis, rw(4).ToString))

            If (rw(5).ToString = "OUI") Then
                ExhOUI.Checked = True
                CommExhaustif.Text = MettreApost(rw(6).ToString)
            ElseIf (rw(5).ToString = "NON") Then
                ExhNON.Checked = True
                CommExhaustif.Text = MettreApost(rw(6).ToString)
            Else
                ExhOUI.Checked = False
                ExhNON.Checked = False
                CommExhaustif.Text = "Commentaire"
            End If

            If (rw(7).ToString = "OUI") Then
                EssOUI.Checked = True
                CommEssentiel.Text = MettreApost(rw(8).ToString)
            ElseIf (rw(7).ToString = "NON") Then
                EssNON.Checked = True
                CommEssentiel.Text = MettreApost(rw(8).ToString)
            Else
                EssOUI.Checked = False
                EssNON.Checked = False
                CommEssentiel.Text = "Commentaire"
            End If

        Next


    End Sub

    Private Sub InfosProvenance(ByVal Soumis As String)

        Dim lePays As String = ""

        query = "select F.PaysFournis from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and F.NumeroDAO='" & JugementOffres.CmbNumDoss.Text & "' and F.CodeProjet='" & ProjetEnCours & "' and S.RefSoumis='" & Soumis & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            lePays = rw(0).ToString
        Next

        If (lePays = "") Then
            ProvNON.Checked = True
            CommProvenace.Text = "Provenance inconnue!"

        Else
            Dim SanctionVrai As Boolean = False
            query = "select DateDebutSanction,DateFinSanction from T_SanctionPays where PaysSanction='" & lePays & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw1 As DataRow In dt1.Rows
                If (DateTime.Compare(Now.ToShortDateString, CDate(rw1(0).ToString)) >= 0 And DateTime.Compare(Now.ToShortDateString, CDate(rw1(1).ToString)) <= 0) Then
                    SanctionVrai = True
                End If
            Next

            If (SanctionVrai = True) Then
                ProvNON.Checked = True
                CommProvenace.Text = "Le soumissionnaire est issu d'un pays sanctionné!"
            Else
                ProvOUI.Checked = True
                CommProvenace.Text = MettreApost(lePays) & "  est un pays élligible."
            End If

        End If





    End Sub

    Private Function JustifGarant(ByVal Soumis As String, ByVal montGarant As String) As String

        Dim RepGarant As String = ""
        query = "select MontantGarantie from T_LotDAO where NumeroDAO='" & JugementOffres.CmbNumDoss.Text & "' and CodeLot='" & JugementOffres.CmbNumLot.Text & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Dim montGarantLot As Decimal = 0
            If (rw(0).ToString <> "") Then
                If (IsNumeric(rw(0).ToString) = True) Then
                    montGarantLot = CDec(rw(0))
                End If
            End If

            Dim montSoumis As Decimal = 0
            If (montGarant <> "") Then
                If (IsNumeric(montGarant) = True) Then
                    montSoumis = CDec(montGarant)
                End If
            End If

            If (montSoumis >= montGarantLot) Then
                If (GaranOUI.Checked = False And GaranNON.Checked = False) Then
                    GaranOUI.Checked = True
                End If
                RepGarant = "La garantie de l'offre (" & AfficherMonnaie(montSoumis.ToString) & ") est conforme à celle du lot (" & AfficherMonnaie(montGarantLot.ToString) & ")."
            Else

                If (GaranOUI.Checked = False And GaranNON.Checked = False) Then
                    GaranNON.Checked = True
                End If
                RepGarant = "La garantie de l'offre (" & AfficherMonnaie(montSoumis.ToString) & ") n'est pas conforme à celle du lot (" & AfficherMonnaie(montGarantLot.ToString) & ")."

            End If

        Next

        Return RepGarant
    End Function

    Private Function JustifSpecTech(ByVal Soumis As String, ByVal Rep As String) As String

        Dim NonConformite As String = ""
        If (JugementOffres.TxtTypeMarche.Text = "Fournitures") Then
            query = "select T.LibelleCaract,T.ValeurCaract,F.ValeurOfferte,F.Commentaire from T_SpecTechCaract as T,T_SoumisCaractFournit as F where T.RefSpecCaract=F.RefSpecCaract and F.RefSoumis='" & Soumis & "' and F.MentionValeur='Non Conforme' and T.RefSpecFournit in (select RefSpecFournit from T_SpecTechFourniture where NumeroDAO='" & JugementOffres.CmbNumDoss.Text & "' and CodeLot='" & JugementOffres.CmbNumLot.Text & "')"
        Else
            query = "select P.Commentaire,I.NumeroItem from T_SoumisPrixItemDQE as P,T_DQEItem as I where P.RefItem=I.RefItem and P.RefSoumis='" & Soumis & "' and P.Mention='Non Conforme' and I.RefSection in (select RefSection from T_DQESection where NumeroDAO='" & JugementOffres.CmbNumDoss.Text & "' and CodeLot='" & JugementOffres.CmbNumLot.Text & "')"
        End If

        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows

            If (JugementOffres.TxtTypeMarche.Text = "Fournitures") Then
                NonConformite &= " - " & MettreApost(rw(0).ToString) & " : " & MettreApost(rw(1).ToString) & " ; votre offre : " & MettreApost(rw(2).ToString) & " (" & MettreApost(rw(3).ToString) & ")" & vbNewLine
            Else
                NonConformite &= " - Prix " & rw(1).ToString & " : " & MettreApost(rw(0).ToString)
            End If

        Next

        If (Rep = "OUI") Then
            If (NonConformite <> "") Then
                NonConformite = "Conforme pour la quasi-totalité des spécifications à l'exception de :" & vbNewLine & NonConformite
            Else
                NonConformite = "Conforme pour la totalité des spécifications offertes."
            End If
        Else
            If (NonConformite <> "") Then
                NonConformite = "Non conforme pour les spécifications suivantes :" & vbNewLine & NonConformite
            Else
                NonConformite = "Non conformité des spécifications offertes."
            End If
        End If

        Return NonConformite

    End Function

    Private Sub Verdict()
        If (VerifNON.Checked = True Or ProvNON.Checked = True Or SpesqlconnON.Checked = True Or GaranNON.Checked = True Or ExhNON.Checked = True Or EssNON.Checked = True) Then
            LabelVerdict.Text = "NON"
            LabelVerdict.ForeColor = Color.Red
        ElseIf (VerifOUI.Checked = True And ProvOUI.Checked = True And SpecOUI.Checked = True And GaranOUI.Checked = True And ExhOUI.Checked = True And EssOUI.Checked = True) Then
            LabelVerdict.Text = "OUI"
            LabelVerdict.ForeColor = Color.Black
        Else
            LabelVerdict.Text = "..."
            LabelVerdict.ForeColor = Color.Black
        End If
    End Sub

    Private Sub VerifNON_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VerifNON.CheckedChanged
        If (VerifNON.Checked = True) Then
            VerifNON.ForeColor = Color.Red
        Else
            VerifNON.ForeColor = Color.Black
        End If
        Verdict()
    End Sub

    Private Sub ProvNON_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProvNON.CheckedChanged
        If (ProvNON.Checked = True) Then
            ProvNON.ForeColor = Color.Red
        Else
            ProvNON.ForeColor = Color.Black
        End If
        Verdict()
    End Sub

    Private Sub SpesqlconnON_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpesqlconnON.CheckedChanged
        If (SpesqlconnON.Checked = True) Then
            SpesqlconnON.ForeColor = Color.Red
        Else
            SpesqlconnON.ForeColor = Color.Black
        End If
        Verdict()
    End Sub

    Private Sub GaranNON_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GaranNON.CheckedChanged
        If (GaranNON.Checked = True) Then
            GaranNON.ForeColor = Color.Red
        Else
            GaranNON.ForeColor = Color.Black
        End If
        Verdict()
    End Sub

    Private Sub ExhNON_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExhNON.CheckedChanged
        If (ExhNON.Checked = True) Then
            ExhNON.ForeColor = Color.Red
        Else
            ExhNON.ForeColor = Color.Black
        End If
        Verdict()
    End Sub

    Private Sub EssNON_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EssNON.CheckedChanged
        If (EssNON.Checked = True) Then
            EssNON.ForeColor = Color.Red
        Else
            EssNON.ForeColor = Color.Black
        End If
        Verdict()
    End Sub

    Private Sub CommVerification_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CommVerification.GotFocus
        If (CommVerification.Text = "Commentaire") Then
            CommVerification.Text = ""
            CommVerification.ForeColor = Color.Black
        Else
            CommVerification.ForeColor = Color.Black
        End If
    End Sub

    Private Sub CommProvenace_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CommProvenace.GotFocus
        If (CommProvenace.Text = "Commentaire") Then
            CommProvenace.Text = ""
            CommProvenace.ForeColor = Color.Black
        End If
    End Sub

    Private Sub CommSpecTech_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CommSpecTech.GotFocus
        If (CommSpecTech.Text = "Commentaire") Then
            CommSpecTech.Text = ""
            CommSpecTech.ForeColor = Color.Black
        End If
    End Sub

    Private Sub CommGarantie_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CommGarantie.GotFocus
        If (CommGarantie.Text = "Commentaire") Then
            CommGarantie.Text = ""
            CommGarantie.ForeColor = Color.Black
        End If
    End Sub

    Private Sub CommExhaustif_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CommExhaustif.GotFocus
        If (CommExhaustif.Text = "Commentaire") Then
            CommExhaustif.Text = ""
            CommExhaustif.ForeColor = Color.Black
        Else
            CommExhaustif.ForeColor = Color.Black
        End If
    End Sub

    Private Sub CommEssentiel_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CommEssentiel.GotFocus
        If (CommEssentiel.Text = "Commentaire") Then
            CommEssentiel.Text = ""
            CommEssentiel.ForeColor = Color.Black
        Else
            CommEssentiel.ForeColor = Color.Black
        End If
    End Sub

    Private Sub CommVerification_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CommVerification.LostFocus
        If (CommVerification.Text = "") Then
            CommVerification.Text = "Commentaire"
            CommVerification.ForeColor = Color.Silver
        ElseIf (CommVerification.Text = "Commentaire") Then
            CommVerification.ForeColor = Color.Silver
        End If
    End Sub

    Private Sub CommExhaustif_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CommExhaustif.LostFocus
        If (CommExhaustif.Text = "") Then
            CommExhaustif.Text = "Commentaire"
            CommExhaustif.ForeColor = Color.Silver
        ElseIf (CommExhaustif.Text = "Commentaire") Then
            CommExhaustif.ForeColor = Color.Silver
        End If
    End Sub

    Private Sub CommEssentiel_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CommEssentiel.LostFocus
        If (CommEssentiel.Text = "") Then
            CommEssentiel.Text = "Commentaire"
            CommEssentiel.ForeColor = Color.Silver
        ElseIf (CommEssentiel.Text = "Commentaire") Then
            CommEssentiel.ForeColor = Color.Silver
        End If
    End Sub

    Private Sub VerifOUI_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles VerifOUI.CheckedChanged
        Verdict()
    End Sub

    Private Sub ExhOUI_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExhOUI.CheckedChanged
        Verdict()
    End Sub

    Private Sub EssOUI_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles EssOUI.CheckedChanged
        Verdict()
    End Sub

    Private Sub LabelVerdict_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LabelVerdict.TextChanged
        If (LabelVerdict.Text <> "...") Then
            BtEnrgExamDetail.Enabled = True
        Else
            BtEnrgExamDetail.Enabled = False
        End If
    End Sub

    Private Sub BtEnrgExamDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgExamDetail.Click
        If (LabelVerdict.Text <> "...") Then

            Dim DatSet = New DataSet
            query = "select * from T_SoumissionFournisseur where RefSoumis='" & ReponseDialog & "'"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Fill(DatSet, "T_SoumissionFournisseur")
            DatSet.Tables!T_SoumissionFournisseur.Rows(0)!Verification = IIf(VerifOUI.Checked = True, "OUI", "NON").ToString
            DatSet.Tables!T_SoumissionFournisseur.Rows(0)!JustifVerification = IIf(CommVerification.Text.Replace("Commentaire", "") <> "", EnleverApost(CommVerification.Text), "").ToString
            DatSet.Tables!T_SoumissionFournisseur.Rows(0)!ConformiteProvenance = IIf(ProvOUI.Checked = True, "OUI", "NON").ToString
            DatSet.Tables!T_SoumissionFournisseur.Rows(0)!ConformiteGarantie = IIf(GaranOUI.Checked = True, "OUI", "NON").ToString
            DatSet.Tables!T_SoumissionFournisseur.Rows(0)!ExhaustiviteOffre = IIf(ExhOUI.Checked = True, "OUI", "NON").ToString
            DatSet.Tables!T_SoumissionFournisseur.Rows(0)!JustifExhaustivite = IIf(CommExhaustif.Text.Replace("Commentaire", "") <> "", EnleverApost(CommExhaustif.Text), "").ToString
            DatSet.Tables!T_SoumissionFournisseur.Rows(0)!ConformiteEssentiel = IIf(EssOUI.Checked = True, "OUI", "NON").ToString
            DatSet.Tables!T_SoumissionFournisseur.Rows(0)!JustifConformEss = IIf(CommEssentiel.Text.Replace("Commentaire", "") <> "", EnleverApost(CommEssentiel.Text), "").ToString
            DatSet.Tables!T_SoumissionFournisseur.Rows(0)!AcceptationExamDetaille = IIf(LabelVerdict.Text = "OUI", "OUI", "NON").ToString
            DatAdapt.Update(DatSet, "T_SoumissionFournisseur")
            DatSet.Clear()
            BDQUIT(sqlconn)
            MsgBox("Traitement effectué avec succès!", MsgBoxStyle.Information)
            ReponseDialog = ""
            Me.Close()
        End If
    End Sub

End Class