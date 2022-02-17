Imports MySql.Data.MySqlClient

Public Class ModifCriterEvaluationDP
    Public RefCriterAModif As Decimal = 0
    Dim TableDonneCriterAModif(5) As String

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
            TableDonneCriterAModif(0) = rw("RefCritere") 'RefCritere 
            TableDonneCriterAModif(1) = rw("CodeCritere") 'CodeCritere
            TableDonneCriterAModif(2) = rw("TypeCritere").ToString 'TypeCritere
            TableDonneCriterAModif(3) = rw("CritereParent") 'CritereParent
            TableDonneCriterAModif(4) = rw("PointCritere").ToString.Replace(".", ",") 'PointCritere
            TableDonneCriterAModif(5) = MettreApost(rw("IntituleCritere").ToString) 'IntituleCritere
            TxtCritere.Text = TableDonneCriterAModif(5).ToString
            TxtNote.Text = TableDonneCriterAModif(4).ToString
        Next
    End Sub

    Private Sub BtAjoutCritere_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjoutCritere.Click
        If TxtCritere.IsRequiredControl("Veuillez saisir le libelle du critère") Then
            TxtCritere.Focus()
            Exit Sub
        End If

        If TxtCritere.Text = TableDonneCriterAModif(5) And TxtNote.Text.Replace(".", ",") = TableDonneCriterAModif(4) Then
            Me.Close()
            Exit Sub
        End If

        Dim NvelleNote As String = ""

        If TableDonneCriterAModif(4) <> TxtNote.Text.Replace(".", ",") Then
            If IsNumeric(TxtNote.Text.Replace(".", ",")) = True Then
                NvelleNote = CDec(TxtNote.Text.Replace(".", ","))
            Else
                SuccesMsg("Saisie incorrect !")
                TxtNote.Focus()
                Exit Sub
            End If
        End If

        'verification du depassement de 100 points
        If TableDonneCriterAModif(2) <> "Bareme" Then
            Dim PointCriteres As Decimal = 0
            Dim SomPoints As String = ""
            SomPoints = ExecuteScallar("select SUM(PointCritere) from T_DP_CritereEval where CodeProjet='" & ProjetEnCours & "' and NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='0' and PointCritere<>''").ToString.Replace(".", ",")
            If SomPoints <> "" Then PointCriteres = CDec(SomPoints)
            If NvelleNote <> "" Then PointCriteres += NvelleNote
            If PointCriteres > 100 Then
                SuccesMsg("Le total des points des critères d'évaluation ne doit pas excéder 100 points")
                Exit Sub
            End If
        End If

        Dim NoteParent As String = ""

        'point du perent du critere selectionné
        query = "Select PointCritere from t_dp_critereeval where RefCritere ='" & TableDonneCriterAModif(3) & "' and CodeProjet='" & ProjetEnCours & "'"
        NoteParent = ExecuteScallar(query).ToString.Replace(".", ",")

        'modification d'un parent
        If NoteParent.ToString = "" Then
            'Verification s'il contient des enfants
            Dim NbrEnfant As Integer = 0
            Dim PtEnfants As Decimal = 0
            query = "Select Count(*) from t_dp_critereeval where CritereParent ='" & TableDonneCriterAModif(0) & "' and NumeroDp='" & EnleverApost(ReponseDialog) & "' and CodeProjet='" & ProjetEnCours & "'"
            NbrEnfant = Val(ExecuteScallar(query))

            If NbrEnfant > 0 Then
                query = "Select SUM(PointCritere) from t_dp_critereeval where CritereParent ='" & TableDonneCriterAModif(0) & "' and PointCritere<>'0' and NumeroDp='" & EnleverApost(ReponseDialog) & "' and CodeProjet='" & ProjetEnCours & "'"
                PtEnfants = ExecuteScallar(query).ToString.Replace(".", ",")
                If PtEnfants.ToString <> "" Then PtEnfants = CDec(PtEnfants)
            End If

            If PtEnfants > NvelleNote Then
                SuccesMsg("Impossible d'appliqué les modifications")
                TxtNote.Focus()
                Exit Sub
            Else
                query = "Update t_dp_critereeval set IntituleCritere='" & EnleverApost(TxtCritere.Text) & "', PointCritere='" & NvelleNote.Replace(",", ".") & "' where RefCritere ='" & TableDonneCriterAModif(0) & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If

        ElseIf TableDonneCriterAModif(2) = "Bareme" Then
            If NvelleNote > CDec(NoteParent) Then
                SuccesMsg("La note saisie est trop élevé !")
                TxtNote.Focus()
                Exit Sub
            End If

            query = "Update t_dp_critereeval set IntituleCritere='" & EnleverApost(TxtCritere.Text) & "', PointCritere='" & NvelleNote.Replace(",", ".") & "' where RefCritere ='" & TableDonneCriterAModif(0) & "' and CodeProjet='" & ProjetEnCours & "'"
            ExecuteNonQuery(query)
        Else

            Dim SommePoints As Decimal = 0
            Dim SomPoints1 As Decimal = 0
            Dim Notesdefo As String = ""
            query = "select SUM(PointCritere) from t_dp_critereeval where CritereParent= '" & TableDonneCriterAModif(3) & "' and PointCritere<>'' and NumeroDp='" & ReponseDialog & "' and CodeProjet='" & ProjetEnCours & "'"
            Notesdefo = ExecuteScallar(query).ToString.Replace(".", ",")
            If Notesdefo.ToString <> "" Then SommePoints = CDec(Notesdefo)
            SomPoints1 = SommePoints
            SomPoints1 = SomPoints1 - CDec(TableDonneCriterAModif(4))
            SomPoints1 = SomPoints1 + NvelleNote

            If SomPoints1 > CDec(NoteParent) Then
                If ConfirmMsg("Dépassement du nombre de point total !" & vbNewLine & "Si vous voulez continuer l'enregistrement," & vbNewLine & "le nombre total de points sera recalculé." & vbNewLine & "Voulez-vous poursuivre l'enregistrement ?") = DialogResult.No Then
                    Exit Sub
                End If
            End If

            'Mise a jour critère modifier
            query = "Update t_dp_critereeval set IntituleCritere='" & EnleverApost(TxtCritere.Text) & "', PointCritere='" & NvelleNote.Replace(",", ".") & "' where RefCritere ='" & TableDonneCriterAModif(0) & "' and CodeProjet='" & ProjetEnCours & "'"
            ExecuteNonQuery(query)

            'Mise a jour de son premier parents
            query = "Update t_dp_critereeval set PointCritere='" & SommePoints.ToString.Replace(",", ".") & "' where RefCritere ='" & TableDonneCriterAModif(3) & "' and CodeProjet='" & ProjetEnCours & "'"
            ExecuteNonQuery(query)

            'Mise a jours de son parent principal
            SommePoints = 0
            Dim CodeParent As Decimal = 0
            Dim SumPoint As String = ""
            query = "select CritereParent from t_dp_critereeval where RefCritere= '" & TableDonneCriterAModif(3) & "' and CritereParent<>'0' and CodeProjet='" & ProjetEnCours & "'"
            CodeParent = Val(ExecuteScallar(query))
            If CodeParent > 0 Then
                query = "select SUM(PointCritere) from t_dp_critereeval where CritereParent= '" & CodeParent & "' and PointCritere<>'' and CodeProjet='" & ProjetEnCours & "' and NumeroDp='" & ReponseDialog & "'"
                SumPoint = ExecuteScallar(query)

                If SumPoint.ToString <> "" Then query = "Update t_dp_critereeval set PointCritere='" & SumPoint.Replace(",", ".") & "' where RefCritere ='" & CodeParent & "' and CodeProjet='" & ProjetEnCours & "'"
                If SumPoint.ToString = "" Then query = "Update t_dp_critereeval set PointCritere=NULL where RefCritere ='" & CodeParent & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If
        End If

        SuccesMsg("Modification effectué avec succès")
        NewDp.MajGridEvaluation()
        TxtCritere.Text = ""
        TxtNote.Text = ""
        Me.Close()
    End Sub
End Class