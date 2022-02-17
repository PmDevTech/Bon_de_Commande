Public Class ReportDate
    Private Sub ReportDate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateOuverture.Text = ""
        DatePublication.Text = ""
        HeureOuverture.Text = ""
        NomJournal.Text = ""
    End Sub
    Private Sub BtEnregComm_Click(sender As Object, e As EventArgs) Handles BtEnregComm.Click
        If NewDao.LayoutView1.RowCount > 0 Then
            If DateOuverture.IsRequiredControl("Veuillez indiquer la nouvelle date d'ouverture") Then
                Exit Sub
            End If
            If HeureOuverture.IsRequiredControl("Veuillez indiquer l'heure d'ouverture") Then
                Exit Sub
            End If
            If DatePublication.IsRequiredControl("Veuillez indiquer la date de publication") Then
                Exit Sub
            End If
            If NomJournal.IsRequiredControl("Veuillez indiquer le journal de publication") Then
                Exit Sub
            End If
            Dim drx = NewDao.LayoutView1.GetDataRow(NewDao.LayoutView1.FocusedRowHandle)
            Dim NumDoss = drx("N°")
            query = "SELECT DateDebutOuverture FROM t_dao WHERE NumeroDAO='" & NumDoss & "'"
            Dim result = ExecuteScallar(query)
            If result = "" Then
                If ConfirmMsg("Voulez-vraiment reporter la date d'ouverture de ce dossier ?") = DialogResult.Yes Then
                    query = "UPDATE SET DateReport='" & dateconvert(DateOuverture.DateTime.ToShortDateString()) & " " & HeureOuverture.Text & "', DatePublicationReport='" & dateconvert(DatePublication.DateTime.ToShortDateString()) & "', JournalPublicationReport='" & NomJournal.Text & "', DateSaisiReport='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' WHERE NumeroDAO='" & NumDoss & "' AND CodeProjet='" & ProjetEnCours & "'"
                    ExecuteNonQuery(query)
                    SuccesMsg("Report effectué avec succès.")
                End If
            Else
                SuccesMsg("Impossible de reporter l'ouverture de ce dossier car elle a déjà été effectuée.")
            End If
        End If
    End Sub
End Class