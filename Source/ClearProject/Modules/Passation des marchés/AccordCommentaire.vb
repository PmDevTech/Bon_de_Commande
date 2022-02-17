Public Class AccordCommentaire 

    Private Sub AccordCommentaire_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        If (JugementOffres.TxtTypeMarche.Text = "Fournitures") Then

            If (ReponseDialog = "") Then
                Me.Text = "Analyse"

                ChkCasSimilaire.Checked = False
                ChkRubrique.Checked = False
                If (JugementOffres.ValeurActuelle <> "") Then
                    PanelChoixValeur.Enabled = False
                    TxtCommentaire.Enabled = True
                Else
                    PanelChoixValeur.Enabled = True
                    TxtCommentaire.Enabled = False
                End If

                ReponseDialog = ""
                If (JugementOffres.ValeurActuelle = "...") Then
                    ChkCasSimilaire.Enabled = True
                    ChkRubrique.Enabled = False

                ElseIf (JugementOffres.ValeurActuelle = "") Then
                    ChkCasSimilaire.Enabled = True
                    ChkRubrique.Enabled = False

                Else
                    ChkCasSimilaire.Enabled = False
                    ChkRubrique.Enabled = True
                End If


            End If
            TxtCommentaire.Text = ""

        Else

            If (ReponseDialog = "") Then
                Me.Text = "Analyse"

                ChkCasSimilaire.Checked = False
                ChkRubrique.Checked = False
                If (JugementOffres.ValeurActuelle <> "...") Then
                    PanelChoixValeur.Enabled = False
                    TxtCommentaire.Enabled = True
                Else
                    PanelChoixValeur.Enabled = True
                    TxtCommentaire.Enabled = False
                End If

                ReponseDialog = ""
                If (JugementOffres.ValeurActuelle = "...") Then
                    ChkCasSimilaire.Enabled = True
                    ChkRubrique.Enabled = False

                    'ElseIf (JugementOffres.ValeurActuelle = "") Then
                    '    ChkCasSimilaire.Enabled = True
                    '    ChkRubrique.Enabled = False

                Else
                    ChkCasSimilaire.Enabled = False
                    ChkRubrique.Enabled = True
                End If


            End If
            TxtCommentaire.Text = ""

        End If



    End Sub

    Private Sub BtEnregComm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnregComm.Click
        If (Me.Text = "Analyse") Then

            'If (JugementOffres.TxtTypeMarche.Text = "Fournitures") Then

            If (TxtCommentaire.Text <> "") Then
                ReponseDialog = TxtCommentaire.Text
                If (ChkCasSimilaire.Checked = True) Then ExceptRevue = "OUI"
                If (ChkRubrique.Checked = True) Then ExceptRevue2 = "OUI"
                Me.Close()

            ElseIf (PanelChoixValeur.Enabled = True) Then
                If (RdValMin.Checked = True) Then
                    ReponseDialog = TxtValMin.Text.Replace(" ", "")
                    ExceptRevue = "Min"
                ElseIf (RdValMax.Checked = True) Then
                    ReponseDialog = TxtValMax.Text.Replace(" ", "")
                    ExceptRevue = "Max"
                ElseIf (RdValMoy.Checked = True) Then
                    ReponseDialog = TxtValMoy.Text.Replace(" ", "")
                    ExceptRevue = "Moy"
                Else
                    MsgBox("Sélectionnez une valeur!", MsgBoxStyle.Information)
                    Exit Sub
                End If

                'ExceptRevue = TxtCommentaire.Text
                If (ChkCasSimilaire.Checked = True) Then ExceptRevue2 = "OUI"
                Me.Close()

            End If

        End If



        'End If
    End Sub
End Class