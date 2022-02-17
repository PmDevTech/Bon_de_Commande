Public Class Compte_Classe 

    Private Sub btenr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btenr.Click
        Try
            'vérification des champ text
            Dim erreur As String = ""

            If txtcl.Text = "" Then
                erreur += "- Compte Classe " & ControlChars.CrLf
            End If
            If txtlibcl.Text = "" Then
                erreur += "- Libellé Classe" & ControlChars.CrLf
            End If


            If erreur = "" Then
               
                'insertion de la sous classe

               query= "insert into T_COMP_CLASSE values('" & txtcl.Text & "','" & EnleverApost(txtlibcl.Text) & "')"
                ExecuteNonQuery(query)



                SuccesMsg("Enregistrement effectué avec succès.")

                EffacerTexBox6(PanelControl1)
               
            Else
                MsgBox("Veuillez remplir ces champs : " & ControlChars.CrLf + erreur, MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation, "ClearProject")
        End Try
    End Sub

    Private Sub btann_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btann.Click
        EffacerTexBox6(PanelControl1)
    End Sub

End Class