Imports DevExpress.XtraEditors.Repository
Imports System.Math
Imports DevExpress.XtraEditors
Imports System.IO
Imports DevExpress.XtraReports.UI
Imports Microsoft.Office.Interop
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports ClearProject.PassationMarche

Public Class EnvoiDossSoumissionnaire
    Public NumeroDAO As String = ""
    Public ExtensionExport As String = ""
    Public TypesMarches As String = ""

    Private Sub ReportDate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If NumeroDAO.ToString = "" Then
            Me.Close()
        End If
        ChargerFourinsseur()
    End Sub

    Private Sub ChargerFourinsseur()

        Dim dtFour = New DataTable()
        dtFour.Columns.Clear()
        dtFour.Columns.Add("Choix", Type.GetType("System.Boolean"))
        dtFour.Columns.Add("Code", Type.GetType("System.String"))
        dtFour.Columns.Add("CodeFournis", Type.GetType("System.String"))
        dtFour.Columns.Add("Soumissoinnaire", Type.GetType("System.String"))
        dtFour.Rows.Clear()

        query = "select CodeFournis, NomFournis from T_Fournisseur where NumeroDAO='" & EnleverApost(NumeroDAO.ToString) & "' and CodeProjet='" & ProjetEnCours & "'" ' and DateDepotDAO<>''"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        Dim cmpte As Integer = 0
        For Each rw In dt.Rows
            cmpte += 1
            Dim NewLigne = dtFour.NewRow
            NewLigne("Code") = IIf(cmpte Mod 2 = 0, "x", "").ToString
            NewLigne("Choix") = False
            NewLigne("CodeFournis") = rw("CodeFournis").ToString
            NewLigne("Soumissoinnaire") = MettreApost(rw("NomFournis").ToString)
            dtFour.Rows.Add(NewLigne)
        Next

        LgListapproskt.DataSource = dtFour
        Dim edit As RepositoryItemCheckEdit = New RepositoryItemCheckEdit()
        edit.ValueChecked = True
        edit.ValueUnchecked = False
        ViewArtiappro.Columns("Choix").ColumnEdit = edit
        LgListapproskt.RepositoryItems.Add(edit)
        ViewArtiappro.OptionsBehavior.Editable = True

        ViewArtiappro.Columns("Code").OptionsColumn.AllowEdit = False
        ViewArtiappro.Columns("CodeFournis").OptionsColumn.AllowEdit = False
        ViewArtiappro.Columns("Soumissoinnaire").OptionsColumn.AllowEdit = False
        ViewArtiappro.Columns("Code").Visible = False
        ViewArtiappro.Columns("CodeFournis").Visible = False

        ViewArtiappro.OptionsView.ColumnAutoWidth = True
        ViewArtiappro.OptionsBehavior.AutoExpandAllGroups = True
        ViewArtiappro.VertScrollVisibility = True
        ViewArtiappro.HorzScrollVisibility = True
        ViewArtiappro.BestFitColumns()
        ViewArtiappro.Columns("Choix").MaxWidth = 50
    End Sub

    Private Sub BtEnregComm_Click(sender As Object, e As EventArgs) Handles BtEnregComm.Click
        If ViewArtiappro.RowCount > 0 Then
            Dim CodFournisseur As New List(Of String)

            For j = 0 To ViewArtiappro.RowCount - 1
                If ViewArtiappro.GetRowCellValue(j, "Choix") = True Then
                    CodFournisseur.Add(ViewArtiappro.GetRowCellValue(j, "CodeFournis"))
                End If
            Next

            If BtEnregComm.Text = "Envoyer" Then
                If CodFournisseur.Count = 0 Then
                    FailMsg("Veuillez sélectionner les soumissoinnaires à qui envoyés le DAO.")
                    Exit Sub
                End If


            ElseIf BtEnregComm.Text = "Exporter" Then 'Exportation

                If CodFournisseur.Count = 0 Then
                    FailMsg("Veuillez sélectionner un soumissoinnaire.")
                    Exit Sub
                End If

                Dim NomRepCheminSauve As String = line & "\DAO\" & TypesMarches.ToString & "\PSL\" & FormatFileName(NumeroDAO.ToString, "")
                Dim NomFichier As String = ""

                For i As Integer = 0 To CodFournisseur.Count - 1
                    NomFichier = "\DAO N°_" & CodFournisseur(i) & FormatFileName(NumeroDAO.ToString, "") & ExtensionExport.ToString
                    If File.Exists(NomRepCheminSauve & NomFichier.ToString) = True Then
                        If ExtensionExport = ".pdf" Then
                            If ExporterPDF(NomRepCheminSauve.ToString & NomFichier.ToString, "DossierAppelOffre.pdf") = False Then
                                Exit Sub
                            End If
                        Else
                            If ExporterWORDfOrmatDocx(NomRepCheminSauve.ToString & NomFichier.ToString, "Dossier_Appel_Offre.docx") = False Then
                                Exit Sub
                            End If
                        End If
                    Else
                        FailMsg("Le fichier à exporter n'existe pas ou a été supprimé.")
                        Exit Sub
                    End If
                Next
            End If
        End If

    End Sub
End Class