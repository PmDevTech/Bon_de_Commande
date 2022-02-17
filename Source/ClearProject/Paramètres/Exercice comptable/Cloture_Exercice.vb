Imports ClearProject.GestComptable
Public Class Cloture_Exercice

    Private Sub BtEnrg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrg.Click
        Try
            If combexer.SelectedIndex <> -1 Then

                DebutChargement()

                Dim DateDebut As String = dateconvert(CDate(txtDateDebut.Text))

                Dim DateFin As String = dateconvert(CDate(txtDateFin.Text))

                Dim DateN1 As String = dateconvert(CDate(txtDateFin.Text).AddDays(1))


                'Clean tables reports
                CleanReport(txtDateFin.DateTime.Year)

                'MAJ des montants dans les tables de report
                UpdateReport(txtDateFin.DateTime.Year)


                'Actualisation du resultat
                Try
                    UpdateResultat(CDate(txtDateDebut.Text).Date, CDate(txtDateFin.Text).Date)
                Catch ex As Exception

                End Try

                'Vérification du désequilibre
                Dim Resultat As Decimal = GetResultat(txtDateFin.DateTime.Year)

                If Resultat <> 0 Then
                    Dim Equilibre As New Equilibrer_Cloture
                    Equilibre.lblMontant.Text = AfficherMonnaie(Resultat)
                    FinChargement()
                    Equilibre.ShowDialog()
                    If Equilibre.DialogResult = DialogResult.OK Then
                        Dim Compte As String = Equilibre.Compte
                        query = "SELECT COUNT(*) FROM report_sc WHERE code_sc='" & Compte & "' AND DATE_LE='" & DateN1 & "'"
                        If Val(ExecuteScallar(query)) > 0 Then
                            If Resultat > 0 Then
                                query = "UPDATE report_sc SET credit_le=" & Resultat & " WHERE code_sc='" & Compte & "' AND DATE_LE='" & DateN1 & "'"
                                ExecuteNonQuery(query)
                            Else
                                query = "UPDATE report_sc SET debit_le=" & Math.Abs(Resultat) & " WHERE code_sc='" & Compte & "' AND DATE_LE='" & DateN1 & "'"
                                ExecuteNonQuery(query)
                            End If
                        Else
                            If Resultat > 0 Then
                                query = "INSERT INTO report_sc VALUES(NULL,'" & Compte & "','0','" & Resultat & "','0','0','" & DateN1 & "')"
                                ExecuteNonQuery(query)
                            Else
                                query = "INSERT INTO report_sc VALUES(NULL,'" & Compte & "','" & Math.Abs(Resultat) & "','0','0','0','" & DateN1 & "')"
                                ExecuteNonQuery(query)
                            End If
                        End If

                        query = "SELECT COUNT(*) FROM T_COMP_LIGNE_ECRITURE WHERE code_sc='" & Compte & "' AND DATE_LE<='" & Year(txtDateFin.DateTime.AddDays(1)) & "-12-31' AND DATE_LE>='" & Year(txtDateFin.DateTime.AddDays(1)) & "-01-01'"
                        If Val(ExecuteScallar(query)) = 0 Then
                            query = "insert into T_COMP_LIGNE_ECRITURE values (NULL,'','" & Compte & "','', '','" & DateN1 & "','0','0','','','" & ProjetEnCours & "','non','non','" & CodeUtilisateur & "','" & DateDuJour & "', '" & DateDuJour & "','0','non','')"
                            ExecuteNonQuery(query)
                        End If
                    End If
                End If

                'MAJ des statuts de l'exercice
                If rdProvisoire.Checked Then
                    query = "update T_COMP_EXERCICE set ETAT='1' where libelle='" & combexer.Text & "'"
                    ExecuteNonQuery(query)

                    query = "update T_COMP_LIGNE_ECRITURE set Etat='1' where DATE_lE >='" & DateDebut & "' and DATE_LE <='" & DateFin & "'"
                    ExecuteNonQuery(query)
                Else
                    query = "update T_COMP_EXERCICE set ETAT='2' where libelle='" & combexer.Text & "'"
                    ExecuteNonQuery(query)

                    query = "update T_COMP_LIGNE_ECRITURE set Etat='2' where DATE_lE >='" & DateDebut & "' and DATE_LE <='" & DateFin & "'"
                    ExecuteNonQuery(query)
                End If

                FinChargement()

                combexer.Text = ""
                txtDateDebut.Text = ""
                txtDateFin.Text = ""

                If rdProvisoire.Checked = True Then
                    SuccesMsg("Clôture provisoire effectuée avec succès.")
                ElseIf rdDefinitif.Checked = True Then
                    loadExercice()
                    SuccesMsg("Clôture definitive effectuée avec succès.")
                End If
            Else
                SuccesMsg("Veuillez choisir un exercice comptable")
            End If

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub combexer_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles combexer.SelectedIndexChanged
        Try
            If combexer.SelectedIndex <> -1 Then
                query = "select * FROM T_COMP_EXERCICE where libelle='" & combexer.Text & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    txtDateDebut.Text = rw("datedebut")
                    txtDateFin.Text = rw("datefin")
                Next
            Else
                txtDateDebut.Text = ""
                txtDateFin.Text = ""
            End If

        Catch ex As Exception
            FailMsg(ex.ToString())
        End Try
    End Sub

    Private Sub loadExercice()
        Try

            combexer.Properties.Items.Clear()
            query = "select * from T_COMP_EXERCICE where etat <='1' ORDER BY libelle"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                combexer.Properties.Items.Add(rw("libelle").ToString)
            Next

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & vbNewLine & ex.ToString())
        End Try
    End Sub

    Private Sub BtAnnul_Click(sender As System.Object, e As System.EventArgs) Handles BtAnnul.Click
        combexer.Text = ""
        txtDateDebut.Text = ""
        txtDateFin.Text = ""
        Me.Close()
    End Sub

    Private Sub Cloture_Exercice_Load(sender As Object, e As EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        loadExercice()
    End Sub
End Class