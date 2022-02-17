Imports MySql.Data.MySqlClient
Imports DevExpress.XtraEditors.Repository


Public Class SaisieMethodes
    Dim PourAjout As Boolean = False
    Dim LigneAjout As Decimal = -1
    Dim LigneModif As Decimal = -1
    Dim TailleCombMetho As Decimal = 23

    'Verifiant l'existance du code de la methode
    Dim AbreMethode As New List(Of String) From {"AOI", "AON", "SFQC", "SFQ", "SCBD", "SMC", "SD", "SQC", "3CV", "ED", "CF", "QC", "REGIE", "PLC", "PSC", "PSL", "PSO"}


    Private Sub SaisieMethodes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        BtAjouter.Enabled = True
        BtEnregistrer.Enabled = True
        Initialiser()
        RemplirListe()
    End Sub

    Private Sub Initialiser()
        PourAjout = False
        LigneAjout = -1
        LigneModif = -1
    End Sub

    Private Sub init()
        If BtAjouter.Enabled = False Then BtAjouter.Enabled = True

        If PourAjout = True Then ListeMethode.Rows.Remove(ListeMethode.Rows(ListeMethode.Rows.Count - 1))
        'If PourAjout = True Then
        '    PourAjout = False
        '    LigneAjout = -1
        '    ListeMethode.Rows.Remove(ListeMethode.Rows(ListeMethode.Rows.Count - 1))
        'ElseIf LigneModif <> -1 Then
        '    ListeMethode.Rows.Item(LigneModif).Cells(1).ReadOnly = True
        '    ListeMethode.Rows.Item(LigneModif).Cells(1).Style.BackColor = Color.Empty
        '    'Tim Dev ;;;; Obtenir dynamiquement le nombre de checkbox
        '    Dim colcount As Decimal = ListeMethode.Columns.GetColumnCount(DataGridViewElementStates.Visible) - 1
        '    For i As Integer = 2 To colcount
        '        ListeMethode.Rows.Item(LigneModif).Cells(i).ReadOnly = True
        '        ListeMethode.Rows.Item(LigneModif).Cells(i).Style.BackColor = Color.Empty
        '    Next
        '    Dim CurrentCell As Integer = Val(Mid(ListeMethode.CurrentCellAddress.ToString(), 4, 1))
        '    RemplirListe()
        '    ListeMethode.CurrentCell = ListeMethode.Rows(LigneModif).Cells(CurrentCell)
        '    LigneModif = -1
        'End If
        Initialiser()
        RemplirListe()
    End Sub

    Private Sub RemplirListe()
        ListeMethode.Columns.Clear()
        ListeMethode.Rows.Clear()
        query = "SELECT TypeMarche FROM t_typemarche"
        Dim dtt As DataTable = ExcecuteSelectQuery(query)
        If dtt.Rows.Count > 0 Then
            Dim col As New DataGridViewComboBoxColumn
            col.Name = "Code"
            col.HeaderText = "Code"
            col.Width = 80
            col.MaxDropDownItems = 100
            col.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
            col.FlatStyle = FlatStyle.Popup
            col.DataSource = AbreMethode
            col.ReadOnly = True
            ListeMethode.Columns.Add(col)

            Dim col1 = New DataGridViewTextBoxColumn
            col1.Name = "Libelle"
            col1.HeaderText = "Libellé"
            col1.Width = 250
            ListeMethode.Columns.Add(col1)

            'Ajout des types des marches ***************************
            Dim cpte As Decimal = 0
            For Each rwx As DataRow In dtt.Rows
                Dim type As New DataGridViewCheckBoxColumn
                type.Name = MettreApost(rwx("TypeMarche").ToString.ToLower.Replace(" ", "_"))
                type.HeaderText = MettreApost(rwx("TypeMarche").ToString)
                ListeMethode.Columns.Add(type)
                cpte += 1
            Next


            query = "Select distinct AbregeAO, LibelleAO from T_ProcAO where CodeProjet='" & ProjetEnCours & "' order by AbregeAO"
            Dim dt2 = ExcecuteSelectQuery(query)

            'Initialiser l'emplacement du ComboMethode
            TailleCombMetho = 23
            Dim Taille As Decimal = 0

            For Each rw As DataRow In dt2.Rows
                Dim m As Decimal = ListeMethode.Rows.Add
                ListeMethode.Rows.Item(m).Cells("Code").Value = rw("AbregeAO").ToString
                ListeMethode.Rows.Item(m).Cells("Code").ReadOnly = True
                ListeMethode.Rows.Item(m).Cells("Libelle").Value = MettreApost(rw("LibelleAO").ToString)
                ListeMethode.Rows.Item(m).Cells("Libelle").ReadOnly = True

                For i As Integer = 2 To cpte + 1
                    ListeMethode.Rows.Item(m).Cells(i).ReadOnly = True
                Next

                'Cocher les checkbox
                query = "select TypeMarcheAO from T_ProcAO where AbregeAO='" & rw("AbregeAO").ToString & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows
                    If VeriferExisteColumName(dtt, rw1("TypeMarcheAO").ToString) = True Then ListeMethode.Rows.Item(m).Cells(MettreApost(rw1("TypeMarcheAO").ToString.ToLower.Replace(" ", "_"))).Value = True
                Next
            Next

            TailleCombMetho += Taille
            If BtAjouter.Enabled = False Then BtAjouter.Enabled = True

        End If
    End Sub

    Private Function VeriferExisteColumName(ByVal Tabl As DataTable, ByVal TypeMarche As String) As Boolean
        Try
            For Each rw In Tabl.Rows
                If MettreApost(rw("TypeMarche").ToString.ToLower.Replace(" ", "_")) = MettreApost(TypeMarche.ToString.ToLower.Replace(" ", "_")) Then
                    Return True
                End If
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        Return False
    End Function

    Private Sub BtAjouter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjouter.Click

        PourAjout = True
        Dim n As Decimal = ListeMethode.Rows.Add
        LigneAjout = n

        For i As Integer = 0 To 4
            ListeMethode.Rows.Item(n).Cells(i).ReadOnly = False
            If (i = 0 Or i = 1) Then
                ListeMethode.Rows.Item(n).Cells(i).Value = ""
            Else
                ListeMethode.Rows.Item(n).Cells(i).Value = False
            End If
        Next
        ListeMethode.CurrentCell = ListeMethode.Rows(n).Cells(0)
        BtAjouter.Enabled = False
        LigneModif = -1
    End Sub

    Private Sub BtEnregistrer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click
        If ListeMethode.RowCount > 0 Then
            'Tim---->Dev ::::::::: Validation des champs 
            Dim BienRenseigne As Boolean = True
            Dim CodeMethode As String = ""
            Dim ExisteCodeMethode As Boolean = False
            Dim BienRenseigne2 As Decimal = 0
            Dim TypeMarche As String = ""
            Dim LibelleAO As String = ""
            Dim AbregeAO As String = ""
            Dim DernCode As Decimal = 0

            'Tim---->Dev ::::::::: Obtenir dynamiquement le nombre de checkbox
            Dim colcount As Decimal = ListeMethode.Columns.GetColumnCount(DataGridViewElementStates.Visible) - 1

            If (PourAjout And LigneAjout <> -1) Then
                For k As Integer = 0 To 1
                    'Recuperer le nouveau code de la methode saisie
                    If k = 0 Then CodeMethode = ListeMethode.Rows(LigneAjout).Cells(k).Value.ToString

                    If ((ListeMethode.Rows(LigneAjout).Cells(k).Value).ToString = "") Then
                        BienRenseigne = False
                    End If
                Next

                Dim CurrentCell As Integer = Val(Mid(ListeMethode.CurrentCellAddress.ToString(), 4, 1))
                If CurrentCell > 0 Then
                    ListeMethode.CurrentCell = ListeMethode.Rows(LigneAjout).Cells(CurrentCell - 1)
                Else
                    ListeMethode.CurrentCell = ListeMethode.Rows(LigneAjout).Cells(CurrentCell + 1)
                End If

                For p As Integer = 2 To colcount
                    If (ListeMethode.Rows(LigneAjout).Cells(p).Value = False Or Len(ListeMethode.Rows(LigneAjout).Cells(p).Value) = 0) Then
                        BienRenseigne2 += 1
                    End If
                Next

                If (BienRenseigne = False) Then
                    SuccesMsg("Veuillez renseigner tous les champs svp.")
                    Exit Sub
                End If

                'Verification de l'existence du code de la methode
                'For i = 0 To AbreMethode.Count - 1
                '    If AbreMethode(i).ToString = CodeMethode.ToString.ToUpper.Trim Then
                '        ExisteCodeMethode = True
                '        Exit For
                '    End If
                'Next

                'If ExisteCodeMethode = False Then
                '    SuccesMsg("Le code de la méthode saisie n'existe pas")
                '    Exit Sub
                'End If

                If (BienRenseigne2 = (colcount - 1)) Then
                    SuccesMsg("Veuillez selectionner au moins un type de marché.")
                    Exit Sub
                End If

                query = "select count(*) from T_ProcAO where AbregeAO='" & MettreApost(ListeMethode.Rows.Item(LigneAjout).Cells(0).Value.ToString) & "' and CodeProjet='" & ProjetEnCours & "'"
                If Val(ExecuteScallar(query)) > 0 Then
                    SuccesMsg("Cette méthode existe déjà !")
                    Exit Sub
                End If

                'Tim Dev --- Obtenir dynamiquement le nombre de checkbox : c'est colcount

                For i As Integer = 2 To colcount
                    If (ListeMethode.Rows.Item(LigneAjout).Cells(i).Value = True) Then
                        TypeMarche = ListeMethode.Columns(ListeMethode.Rows.Item(LigneAjout).Cells(i).ColumnIndex).HeaderText
                        LibelleAO = EnleverApost(ListeMethode.Rows.Item(LigneAjout).Cells(1).Value.ToString)
                        AbregeAO = EnleverApost(ListeMethode.Rows.Item(LigneAjout).Cells(0).Value.ToString)

                        ExecuteNonQuery("INSERT INTO T_ProcAO VALUES(NULL, '" & LibelleAO & "', '" & AbregeAO & "', '" & TypeMarche & "', '" & ProjetEnCours & "', 'OUI')")
                        ' DernCode = Val(ExecuteScallar("select MAX(CodeProcAO) from T_ProcAO where AbregeAO='" & AbregeAO & "' and CodeProjet='" & ProjetEnCours & "' and TypeMarcheAO='" & TypeMarche & "'"))
                        DernCode = Val(ExecuteScallar("select MAX(CodeProcAO) from T_ProcAO "))
                        ExecuteNonQuery("INSERT INTO T_NombreMarche VALUES('" & ProjetEnCours & "', '" & TypeMarche & "', '" & DernCode & "', '0')")
                    End If
                Next

                SuccesMsg("Méthode " & ListeMethode.Rows.Item(LigneAjout).Cells(0).Value.ToString & " enregistrée avec succès.")
                Initialiser()
                RemplirListe()
            ElseIf (LigneModif <> -1) Then 'Action de modification
                For k As Integer = 0 To 1
                    If ((ListeMethode.Rows(LigneModif).Cells(k).Value).ToString = "") Then
                        BienRenseigne = False
                    End If
                Next

                'Dim CurrentCell As Integer = Val(Mid(ListeMethode.CurrentCellAddress.ToString(), 4, 1))
                ListeMethode.CurrentCell = ListeMethode.Rows(LigneModif).Cells(0)

                For p As Integer = 2 To colcount
                    If (ListeMethode.Rows(LigneModif).Cells(p).Value = False Or Len(ListeMethode.Rows(LigneModif).Cells(p).Value) = 0) Then
                        BienRenseigne2 += 1
                    End If
                Next

                If (BienRenseigne = False) Then
                    SuccesMsg("Veuillez renseigner tous les champs svp.")
                    Exit Sub
                End If

                If (BienRenseigne2 = (colcount - 1)) Then
                    SuccesMsg("Veuillez selectionner au moins un type de marché.")
                    Exit Sub
                End If

                ' Recherche du code de la methode *****
                Dim Abreg As String = ListeMethode.Rows.Item(LigneModif).Cells(0).Value.ToString

                Dim TabCodeProcAO As DataTable = ExcecuteSelectQuery("select CodeProcAO from T_ProcAO WHERE AbregeAO='" & EnleverApost(Abreg) & "' and CodeProjet='" & ProjetEnCours & "'")
                For Each rw1 In TabCodeProcAO.Rows
                    ExecuteNonQuery("DELETE FROM T_NombreMarche WHERE CodeProcAO='" & rw1("CodeProcAO") & "' and CodeProjet='" & ProjetEnCours & "'")
                Next
                ExecuteNonQuery("DELETE FROM T_ProcAO WHERE AbregeAO='" & Abreg & "' and CodeProjet='" & ProjetEnCours & "'")

                For i As Integer = 2 To colcount
                    If (ListeMethode.Rows.Item(LigneModif).Cells(i).Value = True) Then
                        TypeMarche = ListeMethode.Columns(ListeMethode.Rows.Item(LigneModif).Cells(i).ColumnIndex).HeaderText
                        LibelleAO = EnleverApost(ListeMethode.Rows.Item(LigneModif).Cells(1).Value.ToString)
                        AbregeAO = EnleverApost(ListeMethode.Rows.Item(LigneModif).Cells(0).Value.ToString)

                        ExecuteNonQuery("INSERT INTO T_ProcAO VALUES(NULL, '" & LibelleAO & "', '" & AbregeAO & "', '" & TypeMarche & "', '" & ProjetEnCours & "', 'OUI')")
                        'DernCode = Val(ExecuteScallar("select CodeProcAO from T_ProcAO where AbregeAO='" & AbregeAO & "' and CodeProjet='" & ProjetEnCours & "' and TypeMarcheAO='" & TypeMarche & "'"))
                        DernCode = Val(ExecuteScallar("select CodeProcAO from T_ProcAO "))
                        ExecuteNonQuery("INSERT INTO T_NombreMarche VALUES('" & ProjetEnCours & "', '" & TypeMarche & "', '" & DernCode & "', '0')")
                    End If
                Next

                SuccesMsg("Méthode " & ListeMethode.Rows.Item(LigneModif).Cells(0).Value.ToString & " modifiée avec succès.")
                Dim CurrentCell As Integer = Val(Mid(ListeMethode.CurrentCellAddress.ToString(), 4, 1))
                RemplirListe()
                ListeMethode.CurrentCell = ListeMethode.Rows(LigneModif).Cells(CurrentCell)
                Initialiser()
            End If
        End If
    End Sub

    Private Function VeriferMethode() As Boolean
        Try
            If ListeMethode.RowCount > 0 Then
                Dim colcount As Decimal = ListeMethode.Columns.GetColumnCount(DataGridViewElementStates.Visible) - 1
                Dim Abreg As String = ListeMethode.Rows.Item(LigneModif).Cells(0).Value.ToString

                For i As Integer = 2 To colcount
                    If (ListeMethode.Rows.Item(LigneModif).Cells(i).Value = True) Then
                        Dim TypeMarche As String = ListeMethode.Columns(ListeMethode.Rows.Item(LigneModif).Cells(i).ColumnIndex).HeaderText
                        query = "select CodeProcAO from T_ProcAO where AbregeAO='" & EnleverApost(Abreg) & "' and TypeMarcheAO='" & EnleverApost(TypeMarche) & "' and CodeProjet='" & ProjetEnCours & "'"
                        Dim dt As DataTable = ExcecuteSelectQuery(query)
                        For Each rw1 As DataRow In dt.Rows
                            If Val(ExecuteScallar("select count(*) from T_DelaiEtape where CodeProcAO='" & CInt(rw1("CodeProcAO")) & "'")) > 0 Or Val(ExecuteScallar("select count(*) from t_marche where MethodeMarche='" & CInt(rw1("CodeProcAO")) & "'")) > 0 Then
                                Return True
                            End If
                        Next
                    End If
                Next
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        Return False
    End Function

    Private Sub ListeMethode_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ListeMethode.CellDoubleClick
        If ListeMethode.RowCount > 0 Then
            If (LigneModif = -1 And PourAjout = False And LigneAjout = -1) Then
                LigneModif = ListeMethode.CurrentCell.RowIndex

                If VeriferMethode() = True Then
                    '  SuccesMsg("Cette méthode est en cours d'utilisation." & vbNewLine & "Vous ne pouvez pas la retirer du type de marché.")
                    SuccesMsg("Cette méthode est en cours d'utilisation." & vbNewLine & "Vous ne pouvez pas la modifier.")
                    LigneModif = -1
                    Exit Sub
                End If

                If ConfirmMsg("Voulez-vous modifier les marchés de la méthode " & ListeMethode.Rows.Item(LigneModif).Cells(0).Value.ToString) = DialogResult.Yes Then
                    'Tim Dev ;;;; Obtenir dynamiquement le nombre de checkbox
                    Dim colcount As Decimal = ListeMethode.Columns.GetColumnCount(DataGridViewElementStates.Visible) - 1
                    ListeMethode.Rows.Item(LigneModif).Cells(1).ReadOnly = False
                    ListeMethode.Rows.Item(LigneModif).Cells(1).Style.BackColor = Color.Yellow
                    For i As Integer = 2 To colcount
                        ListeMethode.Rows.Item(LigneModif).Cells(i).ReadOnly = False
                        ListeMethode.Rows.Item(LigneModif).Cells(i).Style.BackColor = Color.Yellow
                    Next
                    BtAjouter.Enabled = False
                    PourAjout = False
                    LigneAjout = -1
                Else
                    Initialiser()
                End If
            Else
                SuccesMsg("Veuillez d'abord enregistrer les modifications en cours!")
            End If
        End If
    End Sub

    Private Sub BtReload_Click(sender As Object, e As EventArgs) Handles BtReload.Click
        init()
    End Sub

    Private Sub BtSupprimer_Click(sender As Object, e As EventArgs) Handles BtSupprimer.Click
        If ListeMethode.Rows.Count > 0 Then
            If PourAjout = False And LigneModif = -1 Then
                ' Recherche du code de la methode *****
                LigneModif = ListeMethode.CurrentCell.RowIndex
                If VeriferMethode() = True Then
                    SuccesMsg("Cette méthode est en cours d'utilisation." & vbNewLine & "Vous ne pouvez pas la supprimer.")
                    LigneModif = -1
                    Exit Sub
                End If
                Dim Abreg As String = ListeMethode.CurrentRow.Cells(0).Value.ToString

                If ConfirmMsg("Êtes-vous sûr de vouloir supprimer " & Abreg & "?") = DialogResult.Yes Then
                    Dim TabCodeProcAO As DataTable = ExcecuteSelectQuery("select CodeProcAO from T_ProcAO WHERE AbregeAO='" & EnleverApost(Abreg) & "' and CodeProjet='" & ProjetEnCours & "'")
                    ExecuteNonQuery("DELETE FROM T_ProcAO WHERE AbregeAO='" & Abreg & "' and CodeProjet='" & ProjetEnCours & "'")
                    For Each rw1 In TabCodeProcAO.Rows
                        ExecuteNonQuery("DELETE FROM T_NombreMarche WHERE CodeProcAO='" & rw1("CodeProcAO") & "' and CodeProjet='" & ProjetEnCours & "'")
                    Next

                    SuccesMsg("Suppression effectuée avec succès")
                    ListeMethode.Rows.Remove(ListeMethode.Rows(ListeMethode.CurrentCell.RowIndex))
                    Dim inde As Decimal = ListeMethode.CurrentRow.Index
                    ' RemplirListe()
                    'If inde > 0 Then ListeMethode.CurrentCell = ListeMethode.Rows(inde - 1).Cells(0)
                End If
                Initialiser()
            Else
                SuccesMsg("Veuillez d'abord enregistrer les modifications en cours!")
            End If
        Else
            SuccesMsg("Aucune methode à supprimer !")
        End If
    End Sub

End Class