'Imports AxMicrosoft
Imports Microsoft
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.Math

Public Class DialogSeuil

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If (SelectMarche.Text <> "" And SelectMethode.Text <> "" And CombBailleur.Text <> "" And ((MontantPlanche.Text <> "" And MontantPlafond.Text <> "") Or (TousMontants.Checked = True) Or (NoPlafondLimite.Checked = True)) And SelectRevue.Text <> "") Then

            ' Verification du plafond et du planché ********************
            Dim Conflit As Boolean = False
            Dim MsgConflit As String = ""
            Dim VerifIntervalle As Boolean = True

            query = "select MethodeMarcheAuto from T_ParamTechProjet where CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                If (rw("MethodeMarcheAuto").ToString = "NON") Then
                    VerifIntervalle = False
                End If
            Next

            If (VerifIntervalle = True) Then

                If (TousMontants.Checked = False And NoPlafondLimite.Checked = False) Then

                    query = "select MontantPlanche,PlancheInclu,MontantPlafond,PlafondInclu from T_Seuil where CodeProcAO='" & CodeMethodeCache.Text & "' and MontantPlanche<>'TM' and MontantPlafond<>'NL' and Bailleur='" & EnleverApost(CombBailleur.Text) & "'"
                    Dim dt6 As DataTable = ExcecuteSelectQuery(query)

                    For Each rw As DataRow In dt6.Rows
                        Dim MontPlanche As Decimal = CDec(rw("MontantPlanche"))
                        Dim MontPlafond As Decimal = CDec(rw("MontantPlafond"))
                        If (rw("PlancheInclu").ToString = "NON") Then MontPlanche = MontPlanche + 1
                        If (rw("PlafondInclu").ToString = "NON") Then MontPlafond = MontPlafond - 1

                        Dim NewMontPlanche As Decimal = Val(MontantPlanche.Text.Replace(" ", ""))
                        Dim NewMontPlafond As Decimal = Val(MontantPlafond.Text.Replace(" ", ""))
                        If (PlancheInclus.Checked = False) Then NewMontPlanche = NewMontPlanche + 1
                        If (PlafondInclus.Checked = False) Then NewMontPlafond = NewMontPlafond - 1

                        If (NewMontPlanche >= MontPlanche And NewMontPlanche <= MontPlafond) Then
                            Conflit = True
                            MsgConflit = "Montant planché inclu dans un intervalle existant."
                        ElseIf (NewMontPlafond >= MontPlanche And NewMontPlafond <= MontPlafond) Then
                            Conflit = True
                            MsgConflit = "Montant plafond inclu dans un intervalle existant."
                        ElseIf (NewMontPlanche >= MontPlanche And NewMontPlafond <= MontPlafond) Then
                            Conflit = True
                            MsgConflit = "Intervalle inclu dans un intervalle existant."
                        ElseIf (NewMontPlanche <= MontPlanche And NewMontPlafond >= MontPlafond) Then
                            Conflit = True
                            MsgConflit = "L'intervalle couvre un intervalle existant."
                        ElseIf (NewMontPlanche = MontPlafond) Then
                            Conflit = True
                            MsgConflit = "Le planché est égal à un plafond existant."
                        ElseIf (NewMontPlafond = MontPlanche) Then
                            Conflit = True
                            MsgConflit = "Le plafond est égal à un planché existant."
                        End If
                    Next

                    ' Notre plafond comparé au planché d'un no limite
                    query = "select MontantPlanche,PlancheInclu from T_Seuil where CodeProcAO='" & CodeMethodeCache.Text & "' and MontantPlanche<>'TM' and MontantPlafond='NL' and Bailleur='" & EnleverApost(CombBailleur.Text) & "'"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt1.Rows
                        Dim MontPlanche As Decimal = CDec(rw1("MontantPlanche"))
                        If (rw1("PlancheInclu").ToString = "NON") Then MontPlanche = MontPlanche + 1

                        Dim NewMontPlafond As Decimal = Val(MontantPlafond.Text.Replace(" ", ""))
                        If (PlafondInclus.Checked = False) Then NewMontPlafond = NewMontPlafond - 1

                        If (MontPlanche = NewMontPlafond) Then
                            Conflit = True
                            MsgConflit = "Le plafond est égal à un planché existant."
                        ElseIf (MontPlanche < NewMontPlafond) Then
                            Conflit = True
                            MsgConflit = "Le plafond est supérieur à un planché existant."
                        End If
                    Next

                    ' (plafond no limite) Notre planché comparé aux plafonds existant
                ElseIf (NoPlafondLimite.Checked = True) Then
                    query = "select MontantPlanche,PlancheInclu,MontantPlafond,PlafondInclu from T_Seuil where CodeProcAO='" & CodeMethodeCache.Text & "' and MontantPlanche<>'TM' and MontantPlafond<>'NL' and Bailleur='" & EnleverApost(CombBailleur.Text) & "'"
                    Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw2 As DataRow In dt2.Rows
                        Dim MontPlafond As Decimal = CDec(rw2("MontantPlafond"))
                        If (rw2("PlafondInclu").ToString = "NON") Then MontPlafond = MontPlafond - 1

                        Dim NewMontPlanche As Decimal = Val(MontantPlanche.Text.Replace(" ", ""))
                        If (PlancheInclus.Checked = False) Then NewMontPlanche = NewMontPlanche + 1

                        If (NewMontPlanche < MontPlafond) Then
                            Conflit = True
                            MsgConflit = "Le planché est inclu dans un intervalle existant."

                        ElseIf (NewMontPlanche = MontPlafond) Then
                            Conflit = True
                            MsgConflit = "Le planché est égal à un plafond existant."
                        End If
                    Next

                    ' (plafond no limite) Recherche d'un plafond illimité
                    query = "select count(*) from T_Seuil where CodeProcAO='" & CodeMethodeCache.Text & "' and MontantPlafond='NL' and Bailleur='" & EnleverApost(CombBailleur.Text) & "'"
                    If Val(ExecuteScallar(query)) > 0 Then
                        Conflit = True
                        MsgConflit = "Impossible de faire cohexister deux intervalles de plafonds illimités."
                    End If

                    ' Chercher s'il existe un quelconque enregistrement
                ElseIf (TousMontants.Checked = True) Then

                    query = "select count(*) from T_Seuil where CodeProcAO='" & CodeMethodeCache.Text & "' and Bailleur='" & EnleverApost(CombBailleur.Text) & "'"
                    If (Val(ExecuteScallar(query)) > 0) Then
                        Conflit = True
                        MsgConflit = "Cet intervalle couvre un autre existant déjà."
                    End If
                End If

                ' Chercher s'il existe un tous les montants
                query = "select count(*) from T_Seuil where CodeProcAO='" & CodeMethodeCache.Text & "' and MontantPlanche='TM' and Bailleur='" & EnleverApost(CombBailleur.Text) & "'"
                If (Val(ExecuteScallar(query)) > 0) Then
                    Conflit = True
                    MsgConflit = "Cet intervalle est inclu dans un autre existant déjà."
                End If

                query = "select count(*) from T_Seuil where CodeProcAO='" & CodeMethodeCache.Text & "' and Bailleur='" & EnleverApost(CombBailleur.Text) & "'"
                If (Val(ExecuteScallar(query)) > 0) Then
                    Conflit = True
                    MsgConflit = "Cette methode est déjà utilisée pour se bailleur."
                End If
            End If

            'MsgBox("Valeur conflit=" & Conflit, MsgBoxStyle.Information)
            Dim RechAuto As String = "OUI"

            'If (Conflit = True) Then
            '    If ConfirmMsg("Il y a chevauchement entre les montants!" & vbNewLine & MsgConflit & vbNewLine & "Voulez-vous continuer malgré tout?" & vbNewLine & "Cette action désactivera la mise à jour automatique" & vbNewLine & "des méthodes de passation des marchés pour celle-ci!") = DialogResult.Yes Then
            '        RechAuto = "NON"
            '        Conflit = False
            '    Else
            '        Exit Sub
            '    End If
            'End If

            If Conflit = True Then
                SuccesMsg("Il y a chevauchement entre les montants!" & vbNewLine & MsgConflit)
                Conflit = False
                Exit Sub
            End If

            Dim DatSet = New DataSet
            query = "select * from T_Seuil"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_Seuil")
            Dim DatTable = DatSet.Tables("T_Seuil")
            Dim DatRow = DatSet.Tables("T_Seuil").NewRow()

            DatRow("CodeProcAO") = CodeMethodeCache.Text
            DatRow("MontantPlanche") = MontantPlanche.Text.Replace(" ", "")
            DatRow("MontantPlafond") = MontantPlafond.Text.Replace(" ", "")
            DatRow("PlancheInclu") = IIf(PlancheInclus.Checked = True, "OUI", "NON").ToString
            DatRow("PlafondInclu") = IIf(PlafondInclus.Checked = True, "OUI", "NON").ToString

            DatRow("TypeExamenAO") = IIf(SelectRevue.Text = "Revue a Priori", "Priori", "Postériori").ToString
            DatRow("ExceptionRevue") = ExceptRevue
            DatRow("Bailleur") = EnleverApost(CombBailleur.Text)
            DatSet.Tables("T_Seuil").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_Seuil")
            DatSet.Clear()
            BDQUIT(sqlconn)

            If (RechAuto = "NON") Then ExecuteNonQuery("update T_ProcAO set RechAuto = '" & RechAuto & "' where CodeProcAO='" & CodeMethodeCache.Text & "' and CodeProjet='" & ProjetEnCours & "'")
            ExceptRevue = ""
            SuccesMsg("Enregistrement terminé avec succès.")
            initialiser()

            If SeuilRevue.ComBailleur.Text.Trim <> "" Then
                SeuilRevue.RemplirSeuil(SeuilRevue.ComBailleur.Text)
            End If
            ' Me.Close()
        Else
            SuccesMsg("Veuillez renseigner tous les champs!")
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        initialiser()
        Me.Close()
    End Sub

    Private Sub DialogSeuil_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RemplirMarche()
        initialiser()
        '  RemplirMethode()
        RemplirRevue()
        RemplirBailleur()
        ExceptRevue = ""
        SelectMethode.Items.Clear()
    End Sub

    Private Sub initialiser()
        TxtMethode.Text = ""
        SelectMethode.Text = ""
        MontantPlanche.Text = ""
        MontantPlafond.Text = ""
        CombBailleur.Text = ""
        ExceptRevue = ""
        SelectRevue.Text = ""

        PlancheInclus.Checked = False
        PlafondInclus.Checked = False
        MontantPlanche.Enabled = True
        MontantPlafond.Enabled = True
        PlancheInclus.Enabled = True
        PlafondInclus.Enabled = True
        TousMontants.Checked = False
        NoPlafondLimite.Checked = False

    End Sub


    Private Sub RemplirBailleur()
        query = "select InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' order by InitialeBailleur"
        CombBailleur.Items.Clear()
        CombBailleur.Text = ""
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CombBailleur.Items.Add(MettreApost(rw("InitialeBailleur").ToString))
        Next
    End Sub

    Private Sub RemplirRevue()
        SelectRevue.Items.Clear()
        SelectRevue.Items.Add("Revue a Priori")
        SelectRevue.Items.Add("Revue a Postériori")
    End Sub

    Private Sub RemplirMarche()
        SelectMarche.Items.Clear()
        SelectMarche.Text = ""
        query = "select TypeMarche from t_typemarche"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            SelectMarche.Items.Add(MettreApost(rw("TypeMarche").ToString))
        Next
    End Sub

    Private Sub RemplirMethode()
        SelectMethode.Items.Clear()
        SelectMethode.Text = ""

        query = "select AbregeAO from T_ProcAO where TypeMarcheAO='" & EnleverApost(SelectMarche.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            SelectMethode.Items.Add(MettreApost(rw("AbregeAO").ToString))
        Next
    End Sub

    Private Sub SelectMarche_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectMarche.SelectedIndexChanged
        RemplirMethode()
    End Sub

    Private Sub SelectMethode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectMethode.SelectedIndexChanged

        query = "select LibelleAO, CodeProcAO from T_ProcAO where TypeMarcheAO='" & EnleverApost(SelectMarche.Text) & "' and AbregeAO='" & EnleverApost(SelectMethode.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            TxtMethode.Text = MettreApost(rw("LibelleAO"))
            CodeMethodeCache.Text = MettreApost(rw("CodeProcAO").ToString)
        Next

    End Sub

    Private Sub TousMontants_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TousMontants.CheckedChanged
        If (TousMontants.Checked = True) Then
            MontantPlanche.Text = "TM"
            MontantPlanche.Enabled = False
            MontantPlafond.Text = "TM"
            MontantPlafond.Enabled = False
            PlancheInclus.Checked = True
            PlancheInclus.Enabled = False
            PlafondInclus.Checked = True
            PlafondInclus.Enabled = False
        Else
            MontantPlanche.Text = ""
            MontantPlafond.Text = ""
            MontantPlanche.Enabled = True
            MontantPlafond.Enabled = True
            PlancheInclus.Enabled = True
            PlafondInclus.Enabled = True
        End If
    End Sub

    Private Sub NoPlafondLimite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoPlafondLimite.CheckedChanged
        If (NoPlafondLimite.Checked = True) Then
            MontantPlafond.Text = "NL"
            MontantPlafond.Enabled = False
            PlafondInclus.Checked = True
            PlafondInclus.Enabled = False
        Else
            MontantPlafond.Text = ""
            MontantPlafond.Enabled = True
            PlafondInclus.Checked = False
            PlafondInclus.Enabled = True
        End If
    End Sub

    Private Sub SelectRevue_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectRevue.SelectedIndexChanged
        If (SelectRevue.Text = "Revue a Postériori") Then
            ExceptRevue = ""
            Dialog_form(ExceptionRevue)
        End If
    End Sub

    Private Sub MontantPlanche_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MontantPlanche.TextChanged
        VerifSaisieMontant(MontantPlanche)
    End Sub

    Private Sub MontantPlafond_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MontantPlafond.TextChanged
        VerifSaisieMontant(MontantPlafond)
    End Sub
End Class
