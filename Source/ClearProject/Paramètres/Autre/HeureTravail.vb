Public Class HeureTravail

    Dim Hdetravail = New DataTable()
    Dim DrX As DataRow

    Private Sub InitFormulaire()
        finHTrav.Text = "00:00"
        debHTrav.Text = "00:00"
        finHPause.Text = "00:00"
        debHPause.Text = "00:00"
        TotalHPause.Text = ""
        TotalHTrav.Text = ""
        Chargerdatagrid()
    End Sub

    Private Sub Chargerdatagrid()

        Hdetravail.Columns.Clear()
        Hdetravail.Columns.Add("id_HTrav", Type.GetType("System.String"))
        Hdetravail.Columns.Add("Heure début de travail", Type.GetType("System.String"))
        Hdetravail.Columns.Add("Heure d'arrêt de travail", Type.GetType("System.String"))
        Hdetravail.Columns.Add("Début de pause", Type.GetType("System.String"))
        Hdetravail.Columns.Add("Arrêt de pause", Type.GetType("System.String"))
        Hdetravail.Columns.Add("T_HPause", Type.GetType("System.String"))
        Hdetravail.Columns.Add("T_HTravail", Type.GetType("System.String"))
        Hdetravail.Columns.Add("Choix", Type.GetType("System.String"))
        Hdetravail.Rows.Clear()

        query = "select * from h_travail"
        Dim cptr As Decimal = 0
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows

            cptr += 1
            Dim drS = Hdetravail.NewRow()
            drS(0) = rw(0).ToString
            drS(1) = rw(1).ToString
            drS(2) = rw(2).ToString
            drS(3) = rw(3).ToString
            drS(4) = rw(4).ToString
          
            Dim tp As TimeSpan
            Dim tp1 As DateTime = Convert.ToDateTime(rw(2).ToString.Replace(" ", ""))
            Dim tp2 As DateTime = Convert.ToDateTime(rw(3).ToString.Replace(" ", ""))
            Dim hp As Date
            If DateTime.Compare(tp1, tp2) > 0 Then
            Else
                tp = tp2.Subtract(tp1)
                hp = tp.Hours & ":" & tp.Minutes
                drS(5) = hp
            End If

            Dim trv As TimeSpan
            Dim trv1 As DateTime = Convert.ToDateTime(rw(1).ToString.Replace(" ", ""))
            Dim trv2 As DateTime = Convert.ToDateTime(rw(2).ToString.Replace(" ", ""))
            Dim htrv As Date

            If DateTime.Compare(trv1, trv2) > 0 Then
            Else
                tp = tp2.Subtract(tp1)
                trv = trv2.Subtract(trv1)
                trv = trv.Subtract(tp)
                htrv = trv.Hours & ":" & trv.Minutes
                drS(6) = htrv
            End If

            drS(7) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString()
            Hdetravail.Rows.Add(drS)
        Next

        GridControl1.DataSource = Hdetravail
        GridView1.Columns(0).Visible = False
        GridView1.Columns(1).Width = 150
        GridView1.Columns(2).Width = 150
        GridView1.Columns(3).Width = 150
        GridView1.Columns(4).Width = 150
        GridView1.Columns(5).Visible = False
        GridView1.Columns(6).Visible = False
        GridView1.Columns(7).Visible = False
        GridView1.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(GridView1, "[Choix]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
        ColorRowGridAnal(GridView1, "[id_HTrav]=true", Color.LightGray, "Times New Roman", 11, FontStyle.Bold, Color.Black, False)

    End Sub

    Private Sub BtEnregistrer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click

        Dim erreur As String = ""

        If debHTrav.Text = "00:00" Then
            erreur += "- Renseigner Début Heure de Travail " & ControlChars.CrLf
        End If

        If finHTrav.Text = "00:00" Then
            erreur += "- Renseigner Fin Heure de Travail" & ControlChars.CrLf
        End If

        If debHPause.Text = "00:00" Then
            erreur += "- Renseigner Début Heure de Pause" & ControlChars.CrLf
        End If

        If finHPause.Text = "00:00" Then
            erreur += "- Renseigner Fin Heure de Pause" & ControlChars.CrLf
        End If


        If erreur = "" Then

            Dim nb As Decimal = 0
           query= "select count(*) as nbre from h_travail"
            nb = ExecuteScallar(query)

            If nb = 0 Then

               query= "INSERT INTO h_travail values (NULL,'" & debHTrav.Text & "','" & finHTrav.Text & "','" & debHPause.Text & "','" & finHPause.Text & "')"
                ExecuteNonQuery(query)
                MsgBox("Tâche employé ajoutée dans votre table")
                InitFormulaire()
                debHTrav.Focus()

            Else

                DrX = GridView1.GetDataRow(GridView1.FocusedRowHandle)
               query= "Update h_travail set debHTrav='" & debHTrav.Text & "', finHTrav='" & finHTrav.Text & "', debHPause='" & debHPause.Text & "', finHPause='" & finHPause.Text & "'"
                ExecuteNonQuery(query)
                InitFormulaire()
                debHTrav.Focus()

            End If
        Else
            MsgBox("Veuillez : " & ControlChars.CrLf + erreur, MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub BtSupprimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSupprimer.Click
        If GridView1.RowCount > 0 And GridView1.FocusedRowHandle <> -1 Then

            If ConfirmMsg("Voulez-vous vraiment supprimer?") = DialogResult.Yes Then

                DrX = GridView1.GetDataRow(GridView1.FocusedRowHandle)
                query = "delete from h_travail where id_HTrav='" & DrX(0).ToString & "'"
                ExecuteNonQuery(query)
                InitFormulaire()
                debHTrav.Focus()

            End If

        End If
    End Sub

    Private Sub HeureTravail_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        InitFormulaire()
    End Sub

    Private Sub HeureTravail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        debHTrav.Focus()
        Chargerdatagrid()
    End Sub

    Private Sub GridControl1_Click(sender As System.Object, e As System.EventArgs) Handles GridControl1.Click
        If (GridView1.RowCount > 0) Then
            DrX = GridView1.GetDataRow(GridView1.FocusedRowHandle)
            debHTrav.Text = DrX(1).ToString
            finHTrav.Text = DrX(2).ToString
            debHPause.Text = DrX(3).ToString
            finHPause.Text = DrX(4).ToString
          
            Dim IDL = DrX(0).ToString
            ColorRowGrid(GridView1, "[Choix]='x'", Color.White, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(GridView1, "[id_HTrav]='" & IDL & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)
        End If
    End Sub

    Private Sub finHTrav_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles finHTrav.EditValueChanged

        If debHTrav.Text <> "" Then
            Dim trv As TimeSpan
            Dim trv1 As DateTime = Convert.ToDateTime(debHTrav.Text.Replace(" ", ""))
            Dim trv2 As DateTime = Convert.ToDateTime(finHTrav.Text.Replace(" ", ""))
            Dim tp1 As DateTime = Convert.ToDateTime(debHPause.Text.Replace(" ", ""))
            Dim tp2 As DateTime = Convert.ToDateTime(finHPause.Text.Replace(" ", ""))
            Dim htrv As Date
            Dim tp As TimeSpan
            If DateTime.Compare(trv1, trv2) > 0 Then
            Else
                tp = tp2.Subtract(tp1)
                trv = trv2.Subtract(trv1)
                trv = trv.Subtract(tp)
                htrv = trv.Hours & ":" & trv.Minutes
                TotalHTrav.Text = htrv
            End If
        End If

    End Sub

    Private Sub finHPause_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles finHPause.EditValueChanged
        If debHPause.Text <> "" Then
            Dim tp As TimeSpan
            Dim tp1 As DateTime = Convert.ToDateTime(debHPause.Text.Replace(" ", ""))
            Dim tp2 As DateTime = Convert.ToDateTime(finHPause.Text.Replace(" ", ""))
            Dim hp As Date
            If DateTime.Compare(tp1, tp2) > 0 Then
            Else
                tp = tp2.Subtract(tp1)
                hp = tp.Hours & ":" & tp.Minutes
                TotalHPause.Text = hp
            End If
        End If

        If debHTrav.Text <> "" And debHPause.Text <> "" Then
            Dim trv As TimeSpan
            Dim trv1 As DateTime = Convert.ToDateTime(debHTrav.Text.Replace(" ", ""))
            Dim trv2 As DateTime = Convert.ToDateTime(finHTrav.Text.Replace(" ", ""))
            Dim tp1 As DateTime = Convert.ToDateTime(debHPause.Text.Replace(" ", ""))
            Dim tp2 As DateTime = Convert.ToDateTime(finHPause.Text.Replace(" ", ""))
            Dim htrv As Date
            Dim tp As TimeSpan
            If DateTime.Compare(trv1, trv2) > 0 Then
            Else
                Try
                    tp = tp2.Subtract(tp1)
                    trv = trv2.Subtract(trv1)
                    trv = trv.Subtract(tp)
                    htrv = trv.Hours & ":" & trv.Minutes
                    TotalHTrav.Text = htrv
                Catch ex As Exception
                    FailMsg("Veuillez définir correctement les heures.")
                End Try
            End If
        End If
    End Sub

End Class