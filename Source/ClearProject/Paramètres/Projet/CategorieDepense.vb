Imports MySql.Data.MySqlClient

Public Class CategorieDepense
    Dim Modif = False
    Private Sub CategorieDepense_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        CmbBailleur.Text = ""
        CmbConvention.Text = ""
        TxtNumCat.Text = ""
        TxtCategorie.Text = ""
        TxtMontCateg.Text = "0"
        TxtPourcent.Text = "0"
        ChargerBailleur()
        EffacerZones1()
        BtActualiser.Enabled = False
    End Sub

    Private Sub ChargerBailleur()
        query = "select InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' order by InitialeBailleur"
        CmbBailleur.Properties.Items.Clear()
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbBailleur.Properties.Items.Add(rw(0).ToString)
        Next
    End Sub

    Private Sub CmbBailleur_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbBailleur.SelectedValueChanged

        TxtNumCat.Text = ""
        TxtCategorie.Text = ""
        TxtMontCateg.Text = "0"
        TxtPourcent.Text = "0"

        TxtCodeBailleur.Text = ""

        BtActualiser.Enabled = True
        TxtNumCat.Enabled = True

        query = "select CodeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' and InitialeBailleur='" & CmbBailleur.Text & "'"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            TxtCodeBailleur.Text = rw(0).ToString
        Next
        ChargerConvention()
    End Sub

    Private Sub ChargerConvention()

        CmbConvention.Text = ""
        If (TxtCodeBailleur.Text <> "") Then
            query = "select CodeConvention from T_Convention where CodeBailleur='" & TxtCodeBailleur.Text & "' order by CodeConvention"
            CmbConvention.Properties.Items.Clear()
            Dim dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                CmbConvention.Properties.Items.Add(rw(0).ToString)
            Next

            If (CmbConvention.Properties.Items.Count = 1) Then
                CmbConvention.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub CmbConvention_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbConvention.SelectedValueChanged

        TxtNumCat.Text = ""
        TxtCategorie.Text = ""
        TxtMontCateg.Text = "0"
        TxtPourcent.Text = "0"

        GridCategorie.DataSource = Nothing
        GridCategorie.Refresh()
        TxtMontConv.EditValue = 0

        If (CmbConvention.Text <> "") Then
            query = "select MontantConvention from T_Convention where CodeConvention='" & CmbConvention.Text & "' and CodeBailleur='" & TxtCodeBailleur.Text & "'"
            Dim dt = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                Dim rw As DataRow = dt.Rows(0)
                TxtMontConv.EditValue = CDec(rw(0).ToString)
            End If

            ChargerCategorie()
        End If
    End Sub

    Dim dtCat = New DataTable()
    Dim DrX As DataRow

    Private Sub ChargerCategorie()

        dtCat.Columns.Clear()

        dtCat.Columns.Add("Code", Type.GetType("System.String"))
        dtCat.Columns.Add("CodeRef", Type.GetType("System.String"))
        dtCat.Columns.Add("Ref.", Type.GetType("System.String"))
        dtCat.Columns.Add("Catégorie de dépenses", Type.GetType("System.String"))
        dtCat.Columns.Add("Montant", Type.GetType("System.String"))
        dtCat.Columns.Add("%", Type.GetType("System.String"))

        Dim mtTotal As Decimal = 0

        Dim cpt2 As Decimal = 0

        query = "select CodeCateg, LibelleCateg, MontantCateg, PrctCateg, NumCateg from T_CategorieDepense where CodeConvention='" & CmbConvention.Text & "'"
        dtCat.Rows.Clear()
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Dim DrE = dtCat.NewRow()
            cpt2 += 1
            DrE(0) = IIf(CDec(cpt2 / 2) = CDec(cpt2 \ 2), "x", "").ToString
            DrE(1) = rw(0).ToString
            DrE(2) = rw(4).ToString
            DrE(3) = MettreApost(rw(1).ToString)
            DrE(4) = AfficherMonnaie(rw(2).ToString)
            DrE(5) = IIf(rw(3).ToString.Contains(","), rw(3).ToString, rw(3).ToString & ",00").ToString

            mtTotal += CDec(rw(2))

            dtCat.Rows.Add(DrE)
        Next

        '****************************************************
        Dim DrE0 = dtCat.NewRow()
        DrE0(0) = "Z"
        DrE0(1) = ""
        DrE0(2) = ""
        DrE0(3) = "TOTAL CATEGORIES DE DEPENSES"
        DrE0(4) = AfficherMonnaie(mtTotal.ToString)
        If (TxtMontConv.EditValue <> 0) Then
            DrE0(5) = AfficherMonnaie((Math.Round((mtTotal * 100) / TxtMontConv.EditValue, 2)).ToString)
        Else
            DrE0(5) = "0,00"
        End If
        dtCat.Rows.Add(DrE0)
        '****************************************************

        GridCategorie.DataSource = dtCat

        ViewCategorie.Columns(0).Visible = False
        ViewCategorie.Columns(1).Visible = False
        ViewCategorie.Columns(2).Width = 36
        ViewCategorie.Columns(3).Width = 450
        ViewCategorie.Columns(4).Width = 143
        ViewCategorie.Columns(5).Width = 56

        ViewCategorie.Appearance.Row.Font = New Font("Times New Roman", 12, FontStyle.Regular)

        ViewCategorie.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewCategorie.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        ColorRowGrid(ViewCategorie, "[Code]='x'", Color.LightGray, "Times New Roman", 12, FontStyle.Regular, Color.Black)
        ColorRowGridAnal(ViewCategorie, "[Code]='Z'", Color.Navy, "Times New Roman", 12, FontStyle.Bold, Color.White, True)

    End Sub

    Private Sub TxtCategorie_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtCategorie.KeyDown

        If (e.KeyCode = Keys.Enter And TxtCategorie.Text <> "") Then
            TxtMontCateg.Focus()
        End If

    End Sub

    Private Sub TxtMontCateg_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtMontCateg.KeyDown

        If (e.KeyCode = Keys.Enter And TxtCategorie.Text <> "" And TxtMontCateg.EditValue <> 0) Then

            If Modif = False Then

                Dim prt As Double = 0
                Dim nbre As Double = 0
                query = "select count(*) from t_categoriedepense where CodeConvention='" & CmbConvention.Text & "'"
                nbre = Val(ExecuteScallar(query))
                If nbre <> 0 Then
                    'query = "select sum(PrctCateg) from t_categoriedepense where CodeConvention='" & CmbConvention.Text & "'"
                    'prt = Val(ExecuteScallar(query))

                    query = "select sum(MontantCateg) from t_categoriedepense where CodeConvention='" & CmbConvention.Text & "'"
                    prt = Val(ExecuteScallar(query))

                    'If CDbl(prt) = 100 Then
                    '    SuccesMsg("Le montant de la convention est atteint.")
                    'ElseIf (CDbl(prt) + CDbl(TxtPourcent.Text)) <= 100 Then

                    If (prt) = (TxtMontConv.Text) Then
                        SuccesMsg("Le montant de la convention est atteint.")

                    ElseIf ((prt) + (TxtMontCateg.Text)) <= (TxtMontConv.Text) Then
                        EnregistrerCategorie()
                        SuccesMsg(" Enregistrement effectué avec succès ")
                        'ChargerCategorie()

                    Else
                        SuccesMsg("le montant de la convention sera dépassé." & vbNewLine & "Veuillez saisir un autre montant.")
                    End If

                Else
                    EnregistrerCategorie()
                    SuccesMsg(" Enregistrement effectué avec succès ")
                End If

            Else
                If (ConfirmMsg("Voulez-vous modifier la catégorie de dépense" & vbNewLine & DrX(2).ToString & " - " & DrX(3).ToString & " ?") = MsgBoxResult.Yes) Then
                    Dim prt As Double = 0
                    Dim nbre As Double = 0
                    query = "select count(*) from t_categoriedepense where CodeConvention='" & CmbConvention.Text & "'"
                    nbre = Val(ExecuteScallar(query))
                    If nbre <> 0 Then
                        'query = "select sum(PrctCateg) from t_categoriedepense where CodeConvention='" & CmbConvention.Text & "' and CodeCateg <> '" & TxtCodeCategorie.Text & "'"
                        'prt = Val(ExecuteScallar(query))

                        query = "select sum(MontantCateg) from t_categoriedepense where CodeConvention='" & CmbConvention.Text & "' and CodeCateg <> '" & TxtCodeCategorie.Text & "'"
                        prt = Val(ExecuteScallar(query))

                        'If CDbl(prt) = 100 Then
                        '    SuccesMsg("Le montant de la convention est atteint.")
                        'ElseIf (CDbl(prt) + CDbl(TxtPourcent.Text)) <= 100 Then

                        If (prt) = (TxtMontConv.Text) Then
                            SuccesMsg("Le montant de la convention est atteint.")

                        ElseIf ((prt) + (TxtMontCateg.Text)) <= (TxtMontConv.Text) Then

                            Dim erreur As String = ""

                            If TxtNumCat.Text = "" Then
                                erreur += "- Renseigner le code" + ControlChars.CrLf
                            End If

                            If TxtCategorie.Text = "" Then
                                erreur += "- Renseigner le libellé" + ControlChars.CrLf
                            End If

                            If TxtMontCateg.Text = "" Then
                                erreur += "- Renseigner le montant" + ControlChars.CrLf
                            End If

                            If erreur = "" Then


                                query = "update t_categoriedepense set LibelleCateg='" & EnleverApost(TxtCategorie.Text) & "',MontantCateg='" & EnleverApost(TxtMontCateg.Text) & "' , PrctCateg='" & EnleverApost(TxtPourcent.Text) & "' where CodeCateg='" & TxtCodeCategorie.Text & "'"
                                ExecuteNonQuery(query)

                                SuccesMsg("Modification effectuée avec succès ")
                                'Modif = False
                                EffacerZones()
                                TxtNumCat.Text = ""
                                TxtCategorie.Text = ""
                                TxtMontCateg.Text = "0"
                                ChargerCategorie()
                                Modif = False
                            End If

                        Else
                            SuccesMsg("le montant de la convention sera dépassé." & vbNewLine & "Veuillez saisir un autre montant.")
                        End If


                    Else
                        EnregistrerCategorie()
                    End If
                End If

            End If
        End If

    End Sub

    Private Sub TxtPourcent_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPourcent.KeyDown

        If (e.KeyCode = Keys.Enter And TxtCategorie.Text <> "" And TxtMontCateg.EditValue <> 0 And TxtPourcent.Text <> "0") Then
            EnregistrerCategorie()
        ElseIf (e.KeyCode = Keys.Enter And TxtCategorie.Text <> "" And TxtMontCateg.EditValue = 0 And TxtPourcent.Text <> "0") Then
            'Calcul du montant
            TxtMontCateg.EditValue = Math.Round((TxtMontConv.EditValue * CDec(TxtPourcent.Text)) / 100, 0)

            EnregistrerCategorie()
        End If

    End Sub

    Private Sub EnregistrerCategorie()

        Dim erreur As String = ""

        If TxtNumCat.Text = "" Then
            erreur += "- Renseigner le code" + ControlChars.CrLf
        End If

        If TxtCategorie.Text = "" Then
            erreur += "- Renseigner le libellé" + ControlChars.CrLf
        End If

        If TxtMontCateg.Text = "" Then
            erreur += "- Renseigner le montant" + ControlChars.CrLf
        End If

        If erreur = "" Then
            Dim prt As Double = 0
            Dim nbre As Decimal = 0
            query = "select count(*) from T_CategorieDepense where NumCateg='" & TxtNumCat.Text & "' and CodeConvention='" & CmbConvention.Text & "'"
            nbre = ExecuteScallar(query)

            If nbre > 0 Then
                SuccesMsg("Le code de la catégorie existe déjà.")
            Else
                If CDec(TxtPourcent.Text) > 100 Then
                    FailMsg("Le montant dépasse celle de la convention")
                    Exit Sub
                End If

                Dim DatSet = New DataSet
                query = "select * from T_CategorieDepense"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)

                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "T_CategorieDepense")
                Dim DatTable = DatSet.Tables("T_CategorieDepense")
                Dim DatRow = DatSet.Tables("T_CategorieDepense").NewRow()

                DatRow("NumCateg") = TxtNumCat.Text
                DatRow("CodeConvention") = CmbConvention.Text
                DatRow("LibelleCateg") = EnleverApost(TxtCategorie.Text)
                DatRow("MontantCateg") = TxtMontCateg.EditValue.ToString
                DatRow("PrctCateg") = TxtPourcent.Text

                DatSet.Tables("T_CategorieDepense").Rows.Add(DatRow)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "T_CategorieDepense")
                DatSet.Clear()
                BDQUIT(sqlconn)

                EffacerZones()
                ChargerCategorie()
            End If

        Else
            SuccesMsg("Veuillez : " + ControlChars.CrLf + erreur)
        End If
    End Sub

    Private Sub TxtNumCat_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtNumCat.KeyDown

        If (e.KeyCode = Keys.Enter And TxtNumCat.Text <> "") Then
            TxtCategorie.Focus()
        End If

    End Sub

    'Private Sub GridCategorie_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridCategorie.DoubleClick

    '    If (ViewCategorie.RowCount > 0) Then
    '        DrX = ViewCategorie.GetDataRow(ViewCategorie.FocusedRowHandle)
    '        Dim CodeCat As String = DrX(1).ToString

    '        Dim nOccur As Boolean = False
    '        Dim lesRefs As String = ""

    '        query = "select NumeroMarche from T_MarcheSigne where CodeCateg='" & CodeCat & "' and CodeProjet='" & ProjetEnCours & "'"
    '        Dim dt = ExcecuteSelectQuery(query)
    '        For Each rw As DataRow In dt.Rows
    '            If (lesRefs <> "") Then lesRefs = lesRefs & ", "
    '            lesRefs = lesRefs & rw(0).ToString
    '            nOccur = True
    '        Next

    '        If (nOccur = True) Then
    '            MsgBox("Enregistrement en cours d'utilisation!" & vbNewLine & "Ref.: " & lesRefs, MsgBoxStyle.Exclamation)
    '        Else
    '            If (MsgBox("Voulez-vous supprimer la catégorie de dépense" & vbNewLine & DrX(2).ToString & " - " & DrX(3).ToString & " ?", MsgBoxStyle.YesNo, "Suppression de catégorie") = MsgBoxResult.Yes) Then


    '               query= "DELETE from T_CategorieDepense where CodeCateg='" & CodeCat & "' and CodeConvention='" & CmbConvention.Text & "'"
    '                ExecuteNonQuery(query)


    '                MsgBox("Suppression terminée avec succès!", MsgBoxStyle.Information)
    '                ChargerCategorie()
    '            End If
    '        End If

    '    End If



    'End Sub

    Private Sub TxtMontCateg_TextChanged(sender As Object, e As System.EventArgs) Handles TxtMontCateg.TextChanged

        'Calcul du pourcentage
        If TxtMontConv.Text <> "" Then
            If TxtMontCateg.Text <> "" Then
                If (TxtMontConv.Text = 0) Then
                    TxtPourcent.Text = 0
                Else
                    TxtPourcent.Text = Math.Round((TxtMontCateg.EditValue * 100) / TxtMontConv.EditValue, 2).ToString
                End If
            End If
        End If
    End Sub

    Private Sub SupprimerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupprimerToolStripMenuItem.Click
        If (ViewCategorie.RowCount > 0) Then
            DrX = ViewCategorie.GetDataRow(ViewCategorie.FocusedRowHandle)
            Dim CodeCat As String = DrX(1).ToString

            Dim nOccur As Boolean = False
            Dim lesRefs As String = ""

            query = "select NumeroMarche from T_MarcheSigne where CodeCateg='" & CodeCat & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                If (lesRefs <> "") Then lesRefs = lesRefs & ", "
                lesRefs = lesRefs & rw(0).ToString
                nOccur = True
            Next

            If (nOccur = True) Then
                SuccesMsg("Enregistrement en cours d'utilisation!" & vbNewLine & "Ref.: " & lesRefs)
            Else
                If (ConfirmMsg("Voulez-vous supprimer la catégorie de dépense" & vbNewLine & DrX(2).ToString & " - " & DrX(3).ToString & " ?") = MsgBoxResult.Yes) Then


                    query = "DELETE from T_CategorieDepense where CodeCateg='" & CodeCat & "' and CodeConvention='" & CmbConvention.Text & "'"
                    ExecuteNonQuery(query)

                    SuccesMsg("Suppression terminée avec succès!")
                    EffacerZones()
                    ChargerCategorie()
                End If
            End If

        End If
    End Sub

    Private Sub EffacerZones()
        TxtNumCat.Text = ""
        TxtCategorie.Text = ""
        TxtMontCateg.Text = "0"
        TxtPourcent.Text = "0"
    End Sub

    Private Sub EffacerZones1()
        TxtNumCat.Enabled = False
        TxtCategorie.Enabled = False
        TxtMontCateg.Enabled = False
        'TxtPourcent.Enabled = False
    End Sub

    Private Sub GridCategorie_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridCategorie.DoubleClick
        Modif = True
        If ViewCategorie.RowCount > 0 Then
            'Modif = True
            DrX = ViewCategorie.GetDataRow(ViewCategorie.FocusedRowHandle)
            Dim IDl = DrX("Ref.").ToString

            ColorRowGrid(ViewCategorie, "[Ref.]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewCategorie, "[Ref.]='" & IDl & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)


            TxtNumCat.Text = DrX("Ref.").ToString
            TxtCodeCategorie.Text = DrX("CodeRef").ToString
            TxtCategorie.Text = DrX("Catégorie de dépenses").ToString
            TxtMontCateg.Text = DrX("Montant").ToString
            TxtPourcent.Text = DrX("%").ToString

            'ChargerCategorie()
            TxtNumCat.Enabled = False
            TxtPourcent.Enabled = True

        End If

    End Sub

    Private Sub TxtNumCat_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNumCat.EditValueChanged
        If TxtNumCat.EditValue <> "" Then
            TxtCategorie.Enabled = True

        End If

        If TxtNumCat.EditValue = "" Then
            TxtCategorie.Enabled = False
        End If
    End Sub

    Private Sub TxtCategorie_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCategorie.EditValueChanged
        If TxtCategorie.EditValue <> "" Then
            TxtMontCateg.Enabled = True

        End If

        If TxtCategorie.EditValue = "" Then
            TxtMontCateg.Enabled = False
        End If
    End Sub

    Private Sub BtActualiser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtActualiser.Click
        Modif = False
        TxtNumCat.Enabled = True
        EffacerZones()
        TxtNumCat.Focus()
        ChargerConvention()
    End Sub
End Class