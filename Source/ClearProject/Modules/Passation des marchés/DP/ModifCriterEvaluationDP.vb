Imports MySql.Data.MySqlClient

Public Class ModifCriterEvaluationDP
    Public RefCriterAModif As Decimal = 0
    Dim CodeCritere As String = ""
    Dim TypeCritere As String = ""
    Dim CritereParent As Decimal = 0
    Dim PointCritere As String = ""

    Private Sub ModifCriterEvaluationDP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        If (RefCriterAModif.ToString = "0" Or RefCriterAModif.ToString = "") Then
            Me.Close()
        End If

        ChargerInfoDonneCritereamodifier()
    End Sub

    Private Sub BtQuitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        Me.Close()
    End Sub

    Private Sub ChargerInfoDonneCritereamodifier()
        TxtCritere.Text = ""
        TxtNote.Text = ""
        query = "Select * from t_dp_critereeval where RefCritere='" & RefCriterAModif & "' and NumeroDp='" & EnleverApost(ReponseDialog) & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            CodeCritere = rw("CodeCritere") 'CodeCritere
            TypeCritere = rw("TypeCritere").ToString 'TypeCritere
            CritereParent = rw("CritereParent") 'CritereParent
            PointCritere = rw("PointCritere").ToString.Replace(".", ",") 'PointCritere
            TxtCritere.Text = MettreApost(rw("IntituleCritere").ToString)
            TxtNote.Text = rw("PointCritere").ToString.Replace(".", ",")
        Next

        If (TypeCritere.ToString = "Bareme") Or (TypeCritere.ToString = "Note") Then
            TxtNote.Enabled = True
        ElseIf TypeCritere.ToString = "Etiquette" Then
            TxtNote.Enabled = False
        End If

    End Sub

    Private Sub BtAjoutCritere_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjoutCritere.Click
        If TxtCritere.IsRequiredControl("Veuillez saisir le libelle du critère") Then
            TxtCritere.Focus()
            Exit Sub
        End If

        If ((TypeCritere.ToString = "Bareme") Or (TypeCritere.ToString = "Note")) And TxtNote.Text.Trim = "" Then
            SuccesMsg("Veuillez saisir la note !")
            TxtNote.Select()
            Exit Sub
        End If

        If (TypeCritere.ToString = "Bareme") Then
            'Verifier dans le cas d'une valeur Monnetaire
            If IsNumeric(TxtNote.Text.Replace(".", ",")) Then
                Dim PointCriteres = ExecuteScallar("select PointCritere from t_dp_critereeval where NumeroDp='" & EnleverApost(ReponseDialog) & "' and RefCritere='" & CritereParent & "'").ToString.Replace(".", ",")
                If PointCriteres <> "" Then
                    If Val(PointCriteres) < CDec(TxtNote.Text.Replace(".", ",")) Then
                        SuccesMsg("Nombre de points trop élevé !")
                        TxtNote.Select()
                        Exit Sub
                    End If
                End If
            End If

            ExecuteNonQuery("update T_DP_CritereEval set IntituleCritere='" & EnleverApost(TxtCritere.Text) & "', PointCritere='" & EnleverApost(TxtNote.Text.Replace(",", ".")) & "' where RefCritere='" & RefCriterAModif & "' and NumeroDp='" & EnleverApost(ReponseDialog) & "'")
        ElseIf TypeCritere.ToString = "Etiquette" Then
            ExecuteNonQuery("update T_DP_CritereEval set IntituleCritere='" & EnleverApost(TxtCritere.Text) & "' where RefCritere='" & RefCriterAModif & "' and NumeroDp='" & EnleverApost(ReponseDialog) & "'")
        ElseIf TypeCritere.ToString = "Note" Then
            Dim NvelleNoteSaisie As Decimal = 0

            If (IsNumeric(TxtNote.Text.Replace(".", ","))) Then
                NvelleNoteSaisie = CDec(TxtNote.Text.Replace(".", ","))
            Else
                SuccesMsg("Saisie incorrect !")
                TxtNote.Select()
                Exit Sub
            End If

            'verification du depassement de 100 points
            Dim SommePoints As Decimal = ExecuteScallar("select SUM(PointCritere) from T_DP_CritereEval where CodeProjet='" & ProjetEnCours & "' and NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='0' and PointCritere<>''").ToString.Replace(".", ",")
            SommePoints = SommePoints - CDec(PointCritere) 'Retrancher l'ancienne note dans le total
            SommePoints = SommePoints + NvelleNoteSaisie 'Ajouter la nvlle note saisie
            If SommePoints > 100 Then
                SuccesMsg("Le total des points des critères d'évaluation ne doit pas excéder 100 points")
                Exit Sub
            End If

            ExecuteNonQuery("update T_DP_CritereEval set IntituleCritere='" & EnleverApost(TxtCritere.Text) & "', PointCritere='" & EnleverApost(NvelleNoteSaisie.ToString.Replace(",", ".")) & "' where RefCritere='" & RefCriterAModif & "' and NumeroDp='" & EnleverApost(ReponseDialog) & "'")

            If CritereParent.ToString <> "0" Then
                Dim MJPtsCrteresParent1 As String = ""
                Dim MJPtsCrteresParent2 As String = ""
                'MJ point sous critère
                MJPtsCrteresParent2 = ExecuteScallar("select SUM(PointCritere) from T_DP_CritereEval where NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='" & CritereParent & "' and PointCritere<>'' and CodeProjet='" & ProjetEnCours & "'")
                ExecuteNonQuery("update T_DP_CritereEval set PointCritere='" & MJPtsCrteresParent2.ToString.Replace(",", ".") & "' where NumeroDp='" & EnleverApost(ReponseDialog) & "' and RefCritere='" & CritereParent & "' and CodeProjet='" & ProjetEnCours & "'")

                Dim CodParant As String = ExecuteScallar("SELECT CritereParent FROM T_DP_CritereEval where NumeroDp='" & EnleverApost(ReponseDialog) & "' and RefCritere='" & CritereParent & "' and CodeProjet='" & ProjetEnCours & "'")
                'MJ point critères parents
                If CodParant.ToString <> "0" Then
                    MJPtsCrteresParent1 = ExecuteScallar("Select SUM(PointCritere) from T_DP_CritereEval where NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='" & CodParant & "' and PointCritere<>'' and CodeProjet='" & ProjetEnCours & "'")
                    ExecuteNonQuery("update T_DP_CritereEval set PointCritere='" & MJPtsCrteresParent1.ToString.Replace(",", ".") & "' where NumeroDp='" & EnleverApost(ReponseDialog) & "' and RefCritere='" & CodParant & "' and CodeProjet='" & ProjetEnCours & "'")
                End If
            End If
        End If
        SuccesMsg("Modification effectuée avec succès")
        TxtCritere.Text = ""
        TxtNote.Text = ""
        NewDp.MajGridEvaluation()
        Me.Close()
    End Sub
End Class