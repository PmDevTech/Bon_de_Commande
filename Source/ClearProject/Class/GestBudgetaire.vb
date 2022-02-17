Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Math
Imports DevExpress.XtraEditors.Repository

Public Class GestBudgetaire
#Region "Suivi budgetaire par SIGFIP"

#End Region


#Region "Enagements"
    Public Shared Sub LoadListeEngagement(ByVal requete As String, ByVal mondg As DevExpress.XtraGrid.GridControl, ByVal nbre As DevExpress.XtraEditors.LabelControl, ByVal grid As DevExpress.XtraGrid.Views.Grid.GridView)
        Try
            Dim dtEngagement As New DataTable()
            dtEngagement.Columns.Clear()
            dtEngagement.Columns.Add("Code", Type.GetType("System.Boolean"))
            dtEngagement.Columns.Add("Type", Type.GetType("System.String"))
            dtEngagement.Columns.Add("Numéro", Type.GetType("System.String"))
            dtEngagement.Columns.Add("Description", Type.GetType("System.String"))
            dtEngagement.Columns.Add("Montant", Type.GetType("System.String"))
            dtEngagement.Columns.Add("Date", Type.GetType("System.String"))
            dtEngagement.Columns.Add("Attributaire", Type.GetType("System.String"))
            dtEngagement.Columns.Add("Etat", Type.GetType("System.String"))
            dtEngagement.Columns.Add("RefMarche", Type.GetType("System.String"))
            dtEngagement.Columns.Add("TypeEngegement", Type.GetType("System.String"))
            dtEngagement.Rows.Clear()

            Dim cptr As Decimal = 0
            Dim dt As DataTable = ExcecuteSelectQuery(requete)
            For Each rw As DataRow In dt.Rows
                cptr += 1
                Dim drS = dtEngagement.NewRow()
                drS("Code") = TabTrue(cptr - 1)
                drS("Type") = rw("TypeMarche").ToString
                drS("Numéro") = rw("NumeroMarche").ToString
                drS("Description") = MettreApost(rw("DescriptionMarche").ToString)
                drS("Montant") = AfficherMonnaie(Round(CDbl(rw("MontantHT").ToString)))
                drS("Date") = rw("DateMarche").ToString
                drS("Attributaire") = MettreApost(rw("NOM_CPT").ToString)
                drS("Etat") = rw("EtatMarche").ToString
                'Specifiant si c'est un bon de commande ou un marche venant de Passation des marches
                drS("RefMarche") = rw("RefMarche").ToString
                drS("TypeEngegement") = IIf(rw("NumeroDAO").ToString <> "", "PPM", "BCMDE").ToString
                dtEngagement.Rows.Add(drS)
            Next

            mondg.DataSource = dtEngagement
            nbre.Text = cptr.ToString & " Enregistrements"
            Dim edit As RepositoryItemCheckEdit = New RepositoryItemCheckEdit()
            edit.ValueChecked = True
            edit.ValueUnchecked = False
            grid.Columns("Code").ColumnEdit = edit
            mondg.RepositoryItems.Add(edit)

            If grid.Columns("Type").OptionsColumn.AllowEdit = True Then
                grid.OptionsBehavior.Editable = True

                grid.Columns("Type").OptionsColumn.AllowEdit = False
                grid.Columns("Numéro").OptionsColumn.AllowEdit = False
                grid.Columns("Description").OptionsColumn.AllowEdit = False
                grid.Columns("Montant").OptionsColumn.AllowEdit = False
                grid.Columns("Date").OptionsColumn.AllowEdit = False
                grid.Columns("TypeEngegement").OptionsColumn.AllowEdit = False
                grid.Columns("Attributaire").OptionsColumn.AllowEdit = False
                grid.Columns("Etat").OptionsColumn.AllowEdit = False
                grid.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
                grid.Columns("Code").Caption = "  ..."
                grid.Columns("Code").MaxWidth = 50
                grid.Columns("Etat").Width = 60
                grid.Columns("Type").Width = 70
                grid.Columns("Date").Width = 70
                grid.Columns("Description").MaxWidth = 650
                grid.Columns("Montant").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            End If

            grid.Columns("TypeEngegement").Visible = False
            grid.Columns("RefMarche").Visible = False
            grid.OptionsView.ColumnAutoWidth = True
            grid.OptionsBehavior.AutoExpandAllGroups = True
            grid.VertScrollVisibility = True
            grid.HorzScrollVisibility = True
            grid.BestFitColumns()

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub

    Public Shared Function GetModGenerePPM() As String
        Dim ModePPm As String = ""
        Try
            ModePPm = ExecuteScallar("SELECT ModePlanMarche from t_paramtechprojet where CodeProjet='" & ProjetEnCours & "'")
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        Return ModePPm
    End Function
#End Region

End Class
