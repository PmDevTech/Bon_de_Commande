Public Class Nouvel_exercice 
  
    Private Sub BtEnrg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrg.Click
        Try
            'vérification des champ text
            Dim erreur As String = ""

            If datedb.Text = "" Then
                erreur += "- Date Début" & ControlChars.CrLf
            End If

            If erreur = "" Then

                'convertion de la date en date anglaise
                Dim str(3) As String
                str = datedb.Text.Split("/")
                Dim tempdt As String = String.Empty
                For j As Integer = 2 To 0 Step -1
                    tempdt += str(j) & "-"
                Next
                tempdt = tempdt.Substring(0, 10)

                Dim str1(3) As String
                str1 = datefin.Text.Split("/")
                Dim tempdt1 As String = String.Empty
                For j As Integer = 2 To 0 Step -1
                    tempdt1 += str1(j) & "-"
                Next
                tempdt1 = tempdt1.Substring(0, 10)

                query = "select * from T_COMP_EXERCICE where (datedebut='" & dateconvert(datedb.Text) & "' and datefin='" & dateconvert(datefin.Text) & "')"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                If dt0.Rows.Count = 0 Then
                    DebutChargement(True, "Traitement en cours...")
                    Dim exer As String = txtLibelle.Text

                    'On verifie si l'exercice de l'annee en cours a ete deja cree
                    query = "select * from T_COMP_EXERCICE where (datedebut='" & Now.Year & "-01-01')"
                    Dim verif As DataTable = ExcecuteSelectQuery(query)
                    If verif.Rows.Count > 0 Then
                        query = "insert into T_COMP_EXERCICE values (NULL,'" & exer & "','" & tempdt & "','" & tempdt1 & "', '0','0')"
                        ExecuteNonQuery(query)
                    Else
                        'On deselectionne l'exercice en cours
                        ExecuteNonQuery("update T_COMP_EXERCICE set Etat='0', Encours='0'")
                        query = "insert into T_COMP_EXERCICE values (NULL,'" & exer & "','" & tempdt & "','" & tempdt1 & "', '0','1')"
                        ExecuteNonQuery(query)
                    End If

                    query = "select code_sc from T_COMP_SOUS_CLASSE"
                    Dim dt As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                       query= "insert into report_sc values (NULL, '" & rw(0).ToString & "','0', '0','0','0','" & tempdt & "')"
                        ExecuteNonQuery(query)
                    Next

                    query = "select distinct code_cl from T_COMP_SOUS_CLASSE"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt1.Rows
                       query= "insert into report_cl values (NULL, '" & rw1(0).ToString & "','0', '0', '" & tempdt & "')"
                        ExecuteNonQuery(query)
                    Next

                    query = "select code_cl0 from T_COMP_CLASSE0"
                    Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw2 As DataRow In dt2.Rows
                       query= "insert into report_cl0 values (NULL, '" & rw2(0).ToString & "','0', '0', '" & tempdt & "')"
                        ExecuteNonQuery(query)
                    Next

                    query = "select code_cpt from T_COMP_COMPTE"
                    Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw3 As DataRow In dt3.Rows
                       query= "insert into report_cpt values (NULL, '" & rw3(0).ToString & "','0', '0','0', '0', '" & tempdt & "')"
                        ExecuteNonQuery(query)
                    Next

                    FinChargement()
                    ClearMdi.exercice()
                    SuccesMsg("Enregistrement effectué.")
                    EffacerTexBox4(GroupControl1)
                    Me.Close()
                Else
                    SuccesMsg(txtLibelle.Text & " existe déjà.")
                    cmbAnnee.Select()
                End If

                Else
                SuccesMsg("Veuillez remplir ces champs : " & ControlChars.CrLf + erreur)
            End If

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub BtAnnul_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAnnul.Click
        EffacerTexBox4(GroupControl1)
    End Sub

    Private Sub Nouvel_exercice_Load(sender As Object, e As EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        cmbAnnee.Properties.Items.Clear()
        For i = (Now.Year - 5) To (Now.Year + 1)
            cmbAnnee.Properties.Items.Add(i)
        Next
        cmbAnnee.SelectedIndex = (cmbAnnee.Properties.Items.Count - 2)
    End Sub

    Private Sub cmbAnnee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAnnee.SelectedIndexChanged
        If cmbAnnee.SelectedIndex <> -1 Then
            txtLibelle.Text = "Exercice " & cmbAnnee.Text
            datedb.Text = "01/01/" & cmbAnnee.Text
            datefin.Text = "31/12/" & cmbAnnee.Text
        Else
            txtLibelle.ResetText()
            datedb.ResetText()
            datefin.ResetText()
        End If
    End Sub
End Class