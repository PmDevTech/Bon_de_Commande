Imports MySql.Data.MySqlClient

Public Class AjoutCritereConsult

    Private Sub AjoutCritereConsult_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        TxtCritere.Text = ""
        TxtNote.Text = ""
        PersonnelCle.Checked = False
        ChkEtiquette.Checked = True

        If (ReponseDialog = "") Then
            Me.Close()
        End If
    End Sub


    Private Sub BtQuitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        Me.Close()
    End Sub

    Private Sub BtAjoutCritere_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjoutCritere.Click
        If (TxtCritere.Text.Trim <> "" And ReponseDialog <> "") Then

            Dim typeCritere As String = "Etiquette"
            Dim noteCritere As String = ""
            Dim NoteAuto As String = "NON"

            If (ChkEtiquette.Checked = False) Then typeCritere = "Note"
            If (typeCritere = "Note" And TxtNote.Text = "") Then
                SuccesMsg("La note est obligatoire !")
                TxtNote.Focus()
                Exit Sub
            ElseIf (typeCritere = "Etiquette" And TxtNote.Text = "") Then
                If ConfirmMsg("La note de ce critère sera la somme des notes de ses sous critères. Voulez-vous continuer?") = DialogResult.No Then
                    TxtNote.Focus()
                    Exit Sub
                End If
                NoteAuto = "OUI"
            End If

            If (TxtNote.Text <> "") Then
                If (IsNumeric(TxtNote.Text.Replace(".", ","))) Then
                    noteCritere = CDec(TxtNote.Text.Replace(".", ",")).ToString
                Else
                    SuccesMsg("Veuillez saisir une valeur valide")
                    TxtNote.Focus()
                    Exit Sub
                End If
            End If

            query = "select * from T_DP_CritereEval where CodeProjet='" & ProjetEnCours & "' and NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='0' and IntituleCritere='" & EnleverApost(TxtCritere.Text) & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                SuccesMsg("Ce critère existe déjà !")
                TxtCritere.Focus()
                Exit Sub
            End If

            Dim PointCritere As Decimal = 0
            Dim SomPoints As String = ""
            SomPoints = ExecuteScallar("select SUM(PointCritere) from T_DP_CritereEval where CodeProjet='" & ProjetEnCours & "' and NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='0' and PointCritere<>''").ToString.Replace(".", ",")
            If SomPoints <> "" Then PointCritere = CDec(SomPoints)
            If noteCritere <> "" Then PointCritere += noteCritere
            If PointCritere > 100 Then
                SuccesMsg("Le total des points des critères d'évaluation ne doit pas excéder 100 points")
                Exit Sub
            End If

            Dim CodeCritere() As String = {"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X"}
                Dim NbCritere As Decimal = 0
                query = "select Count(*) from T_DP_CritereEval where CodeProjet='" & ProjetEnCours & "' and NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='0'"
                NbCritere = Val(ExecuteScallar(query))

                Dim DatSet = New DataSet
                query = "select * from T_DP_CritereEval where CodeProjet='" & ProjetEnCours & "'"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "T_DP_CritereEval")
                Dim DatTable = DatSet.Tables("T_DP_CritereEval")
                Dim DatRow = DatSet.Tables("T_DP_CritereEval").NewRow()

                DatRow("NumeroDp") = EnleverApost(ReponseDialog)
                DatRow("CodeCritere") = CodeCritere(NbCritere)
                DatRow("IntituleCritere") = EnleverApost(TxtCritere.Text)
                DatRow("TypeCritere") = typeCritere
                DatRow("CritereParent") = 0
                If noteCritere <> "" Then DatRow("PointCritere") = noteCritere.Replace(".", ",")
                DatRow("PointAuto") = NoteAuto
                DatRow("CodeProjet") = ProjetEnCours
                DatRow("Niveau") = "1"

                If (PersonnelCle.Checked = True) Then DatRow("CriterePersonnelCle") = "OUI"

                DatSet.Tables("T_DP_CritereEval").Rows.Add(DatRow)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "T_DP_CritereEval")
                DatSet.Clear()
                BDQUIT(sqlconn)

                NewDp.MajGridEvaluation()
                TxtCritere.Text = ""
                ChkEtiquette.Checked = True
                TxtNote.Text = ""
                TxtCritere.Focus()
                PersonnelCle.Checked = False
            End If
    End Sub

    Private Sub ChkEtiquette_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkEtiquette.CheckedChanged
        If (ChkEtiquette.Checked = False) Then
            LblObs.Text = "* La note est obligatoire !"
            LblObs.ForeColor = Color.Red
            TxtNote.Enabled = True
        Else
            LblObs.Text = "* La note n'est pas obligatoire"
            LblObs.ForeColor = Color.Black
            TxtNote.Enabled = False
        End If
    End Sub
End Class