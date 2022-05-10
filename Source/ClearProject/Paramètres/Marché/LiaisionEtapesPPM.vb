Imports MySql.Data.MySqlClient
Imports DevExpress.XtraEditors.Repository


Public Class LiaisionEtapesPPM
    Private Sub SaisieMethodes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ChargerTypeMarche()
        ListeEtapes.Columns.Clear()
        ListeEtapes.Rows.Clear()
    End Sub

    Private Sub ChargerTypeMarche()
        CmbTypeMarche.ResetText()
        CmbTypeMarche.Properties.Items.Clear()
        query = "select TypeMarche from T_TypeMarche order by TypeMarche"
        CmbTypeMarche.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbTypeMarche.Properties.Items.Add(MettreApost(rw("TypeMarche").ToString))
        Next
    End Sub

    Private Sub RemplirListe(ByVal TypesMarches As String)
        Try
            ListeEtapes.Columns.Clear()
            ListeEtapes.Rows.Clear()

            Dim ColonneNum As New DataGridViewTextBoxColumn
            With ColonneNum
                .HeaderText = "RefEtape"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Name = "RefEtape"
                .Width = 50
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Frozen = True
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Visible = False
            End With
            ListeEtapes.Columns.Insert(0, ColonneNum)

            Dim ColonneDesc As New DataGridViewTextBoxColumn
            With ColonneDesc
                .HeaderText = "N°"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Name = "Num"
                .Width = 50
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Frozen = True
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            ListeEtapes.Columns.Insert(1, ColonneDesc)

            ColonneDesc = New DataGridViewTextBoxColumn
            With ColonneDesc
                .HeaderText = "Intitulé"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Name = "LiblleEtape"
                .Width = 250
                .ReadOnly = True
                .Frozen = True
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            ListeEtapes.Columns.Insert(2, ColonneDesc)

            query = "Select CodeProcAO, AbregeAO from T_ProcAO where TypeMarcheAO='" & EnleverApost(TypesMarches.ToString) & "' and CodeProjet='" & ProjetEnCours & "' order by AbregeAO ASC"
            Dim dtt As DataTable = ExcecuteSelectQuery(query)

            Dim Position As Integer = 0
            If dtt.Rows.Count > 0 Then
                'Ajout des AbregeAO des des methodes ***************************
                For Each rwMethod As DataRow In dtt.Rows
                    Dim type As New DataGridViewCheckBoxColumn
                    With type
                        .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                        .Name = rwMethod("CodeProcAO").ToString & "_" & MettreApost(rwMethod("AbregeAO").ToString)
                        .HeaderText = EnleverApost(rwMethod("AbregeAO").ToString)
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        .Width = 70
                        .ReadOnly = False
                    End With
                    Position += 1
                    ListeEtapes.Columns.Insert(Position + 2, type)
                Next
            End If

            query = "SELECT RefEtape,TitreEtape FROM `t_etapemarche` where TypeMarche='" & EnleverApost(TypesMarches.ToString) & "' and CodeProjet='" & ProjetEnCours & "' ORDER BY `NumeroOrdre` ASC"
            Dim dt2 = ExcecuteSelectQuery(query)

            Dim Cpte As Integer = 0
            For Each rw1 As DataRow In dt2.Rows
                Cpte += 1
                Dim m As Decimal = ListeEtapes.Rows.Add
                ListeEtapes.Rows.Item(m).Cells("RefEtape").Value = rw1("RefEtape").ToString
                ListeEtapes.Rows.Item(m).Cells("Num").Value = Cpte.ToString
                ListeEtapes.Rows.Item(m).Cells("LiblleEtape").Value = MettreApost(rw1("TitreEtape").ToString)

                Dim dtLiaisonEtape As DataTable = ExcecuteSelectQuery("select CodeProcAO from t_liaisonetape where RefEtape='" & rw1("RefEtape") & "' and CodeProjet='" & ProjetEnCours & "'")
                If ListeEtapes.Columns.Count > 2 Then
                    For j = 3 To ListeEtapes.Columns.Count - 1
                        ListeEtapes.Rows.Item(m).Cells(j).Value = GetValeurMethode(ListeEtapes.Columns(j).Name.Split("_")(0), dtLiaisonEtape)
                    Next
                End If
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Function GetValeurMethode(ByVal CodeProcAO As Decimal, ByVal dt As DataTable) As Boolean
        Try
            For Each rw In dt.Rows
                If CodeProcAO = rw("CodeProcAO") Then
                    Return True
                End If
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        Return False
    End Function

    'Dim colcount As Decimal = ListeEtapes.Columns.GetColumnCount(DataGridViewElementStates.Visible) - 1  

    Private Sub CmbTypeMarche_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbTypeMarche.SelectedIndexChanged
        ListeEtapes.Columns.Clear()
        ListeEtapes.Rows.Clear()
        Try
            If CmbTypeMarche.SelectedIndex <> -1 Then
                RemplirListe(CmbTypeMarche.Text)
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub BtEnregistrer_Click(sender As Object, e As EventArgs) Handles BtEnregistrer.Click
        Try
            If CmbTypeMarche.SelectedIndex = -1 Then
                SuccesMsg("Veuillez selectionner le type de marché.")
                CmbTypeMarche.Select()
                Exit Sub
            End If

            If ListeEtapes.RowCount = 0 Or ListeEtapes.Columns.Count <= 2 Then
                SuccesMsg("Aucune donnée à enregistrer.")
                Exit Sub
            End If

            If ListeEtapes.RowCount > 0 And ListeEtapes.Columns.Count > 2 Then

                Dim Mjrs As Boolean = False
                DebutChargement(True, "Enregistrement en cours...")
                Dim ColName As String = ""
                Dim DejaSave As Decimal = 0

                For i = 0 To ListeEtapes.RowCount - 1
                    'Parcourrir les colonnes des Methodes
                    For j = 3 To ListeEtapes.Columns.Count - 1
                        ColName = Convert.ToString(ListeEtapes.Columns.Item(j).Name)

                        DejaSave = Val(ExecuteScallar("select COUNT(*) from t_liaisonetape where CodeProcAO='" & ColName.ToString.Split("_")(0) & "' and CodeProjet='" & ProjetEnCours & "' and RefEtape='" & ListeEtapes.Rows.Item(i).Cells("RefEtape").Value & "'"))
                        If ListeEtapes.Rows.Item(i).Cells(j).Value = True Then
                            If DejaSave = 0 Then
                                ExecuteNonQuery("insert into t_liaisonetape values('" & ListeEtapes.Rows.Item(i).Cells("RefEtape").Value & "', '" & ColName.ToString.Split("_")(0) & "', '" & ProjetEnCours & "')")
                                Mjrs = True
                            End If
                        Else
                            ExecuteNonQuery("delete from t_liaisonetape where RefEtape='" & ListeEtapes.Rows.Item(i).Cells("RefEtape").Value & "' and  CodeProcAO='" & ColName.ToString.Split("_")(0) & "' and  CodeProjet='" & ProjetEnCours & "'")
                            Mjrs = True
                        End If
                    Next
                Next
                FinChargement()

                If Mjrs = True Then
                    SuccesMsg("Enregistrement effectué avec succès.")
                    RemplirListe(CmbTypeMarche.Text)
                End If

            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    ' GridPlanMarche.Rows(e.RowIndex).Cells(e.ColumnIndex)

    Private Sub ListeEtapes_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles ListeEtapes.CellBeginEdit
        Try
            If ListeEtapes.RowCount > 0 Then
                Dim ColName As String = Convert.ToString(ListeEtapes.Columns.Item(e.ColumnIndex).Name)
                Dim ValLigne As Boolean = ListeEtapes.Rows(ListeEtapes.CurrentRow.Index).Cells(e.ColumnIndex).Value
                Dim RefEtape As String = ListeEtapes.Rows(ListeEtapes.CurrentRow.Index).Cells(0).Value
                If ValLigne = True Then
                    If Val(ExecuteScallar("select COUNT(m.CodeProcAO) from t_marche as m, t_liaisonetape as l where m.CodeProcAO=l.CodeProcAO and l.CodeProjet='" & ProjetEnCours & "' and l.RefEtape='" & RefEtape & "' and l.CodeProcAO='" & ColName.ToString.Split("_")(0) & "'")) > 0 Then
                        e.Cancel = True
                    End If
                    'Verifier si les prevision ont été déjà fait
                    If Val(ExecuteScallar("select COUNT(p.RefEtape) from T_PlanMarche as p, t_liaisonetape as l where p.RefEtape=l.RefEtape and l.CodeProjet='" & ProjetEnCours & "' and l.RefEtape='" & RefEtape & "' and l.CodeProcAO='" & ColName.ToString.Split("_")(0) & "'")) > 0 Then
                        e.Cancel = True
                    End If

                End If
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub
End Class