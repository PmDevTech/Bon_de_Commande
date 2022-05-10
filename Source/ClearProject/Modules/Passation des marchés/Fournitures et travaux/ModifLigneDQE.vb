Imports System.Math
Public Class ModifLigneDQE
    Public RefSection As Decimal = 0
    Public TypeModification As String = ""

    Private Sub ModifLigneDQE_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Designation.Focus()
    End Sub

    Private Sub btOK_Click(sender As Object, e As EventArgs) Handles btOK.Click
        Try
            If Designation.Text.Trim().Length = 0 Then
                FailMsg("Veuillez saisir la désignation.")
                Designation.Select()
                Exit Sub
            End If

            DebutChargement(True, "Modification en cours...")

            If TypeModification = "Section" Then
                ExecuteNonQuery("UPDATE t_dqesection set Designation='" & EnleverApost(Designation.Text) & "' where RefSection='" & RefSection & "'")
            Else
                If Val(NumQteBien.Text) <= 0 Then
                    FinChargement()
                    FailMsg("Veuillez saisir la quantité.")
                    NumQteBien.Focus()
                    Exit Sub
                End If
                If Unites.Text.Trim = "" Then
                    FinChargement()
                    FailMsg("Veuillez choisir l'unité.")
                    Unites.Select()
                    Exit Sub
                End If

                'If Val(PrixUnitaire.Text) <= 0 Then
                '    FinChargement()
                '    FailMsg("Veuillez saisir le prix unitaire.")
                '    PrixUnitaire.Select()
                '    Exit Sub
                'End If
                'ExecuteNonQuery("UPDATE t_dqeitem set Designation='" & EnleverApost(Designation.Text) & "', UniteItem='" & EnleverApost(Unites.Text) & "', QteItem='" & CDbl(NumQteBien.Text) & "', PuHtva='" & CDbl(PrixUnitaire.Text) & "', MontHtva='" & CDbl(MontantTotal.Text.Replace(" ", "")) & "', PuHtvaLettre='" & MontantLettre(MontantTotal.Text.Replace(".", ",").Replace(" ", "")) & "' where RefItem='" & RefSection & "'")

                ExecuteNonQuery("UPDATE t_dqeitem set Designation='" & EnleverApost(Designation.Text) & "', UniteItem='" & EnleverApost(Unites.Text) & "', QteItem='" & CDbl(NumQteBien.Text) & "' where RefItem='" & RefSection & "'")
            End If

            NewDao.MajGridDQE()
            FinChargement()
            SuccesMsg("Modification effectuée avec succès.")
            Me.Close()
        Catch ex As Exception
            FinChargement()
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub ModifLigneDQE_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        If RefSection = 0 Then
            Me.Close()
        End If
        MajCmbUnite()
    End Sub

    Private Sub MajCmbUnite()
        Unites.Properties.Items.Clear()
        query = "select LibelleCourtUnite from T_Unite"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            Unites.Properties.Items.Add(rw("LibelleCourtUnite").ToString)
        Next
    End Sub

    Private Sub PrixUnitaire_TextChanged(sender As Object, e As EventArgs) Handles PrixUnitaire.TextChanged, NumQteBien.TextChanged
        If NumQteBien.Text.Trim <> "" And PrixUnitaire.Text.Trim <> "" Then
            MontantTotal.Text = AfficherMonnaie(Round(NumQteBien.Text * PrixUnitaire.Text))
        Else
            MontantTotal.Text = ""
        End If
    End Sub
End Class