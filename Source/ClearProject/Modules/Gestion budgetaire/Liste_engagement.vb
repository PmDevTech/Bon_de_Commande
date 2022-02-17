Imports System.IO
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports Microsoft.Office.Interop
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports ClearProject.GestBudgetaire

Public Class liste_engagement
    Dim drx As DataRow
    Dim dtdoc = New DataTable()
    Dim DateDebutExercice As Date = CDate(ExerciceComptable.Rows(0)("datedebut"))
    Dim DateFinExercice As Date = CDate(ExerciceComptable.Rows(0)("datefin"))
    Public MustRefresh As Boolean = False

    Private Sub liste_engagement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        DateDebutExercice = CDate(ExerciceComptable.Rows(0)("datedebut"))
        DateFinExercice = CDate(ExerciceComptable.Rows(0)("datefin"))

        Try
            LoadData()
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub liste_engagement_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Public Sub LoadData()
        'Liste des engemlents de l'exercice comptable
        query = "select s.TypeMarche, s.NumeroMarche, s.NumeroDAO, s.RefMarche, m.DescriptionMarche, s.MontantHT, s.DateMarche, c.NOM_CPT, s.EtatMarche from t_marchesigne s, t_marche m, t_comp_compte c where s.refmarche=m.refmarche and s.attributaire=c.CODE_CPT and s.codeprojet='" & ProjetEnCours & "' AND STR_TO_DATE(s.DateMarche,'%d/%m/%Y')>='" & dateconvert(DateDebutExercice) & "' AND STR_TO_DATE(s.DateMarche,'%d/%m/%Y')<='" & dateconvert(DateFinExercice) & "' ORDER BY length(s.NumeroMarche), s.NumeroMarche"
        LoadListeEngagement(query, LgListEngag, LblNombre, ViewEngag)
    End Sub

    Private Sub BtImprimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtImprimer.Click
        Dialog_form(Etat_eng)
    End Sub

    Private Sub BtSupprimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSupprimer.Click
        If ViewEngag.RowCount > 0 Then
            Try
                Dim str As String = String.Empty
                Dim cpte As Decimal = 0
                Dim nummarche As String = ""
                Dim RefMarche As String = ""

                For i = 0 To ViewEngag.RowCount - 1
                    If CBool(ViewEngag.GetRowCellValue(i, "Code")) = True Then
                        cpte += 1
                        nummarche = ViewEngag.GetRowCellValue(i, "Numéro").ToString
                        query = "select count(*) from t_comp_activite where NumeroMarche='" & nummarche.ToString & "'"
                        If Val(ExecuteScallar(query)) > 0 Then
                            str &= nummarche & vbNewLine
                        Else
                            'query = "select count(*) from t_gf_demandepd where NumeroMarche='" & nummarche.ToString & "'"
                            'Dim nbre As Integer = Val(ExecuteScallar(query))
                            'If nbre > 0 Then
                            '    str &= nummarche & vbNewLine
                            'End If
                        End If
                    End If
                Next

                If str <> String.Empty Then
                    FailMsg("Impossible de supprimer le(s) marché(s) suivant(s) : " & vbNewLine & str)
                    Exit Sub
                End If

                Dim Reponse As New DialogResult
                If cpte = 0 Then
                    SuccesMsg("Veuillez cocher au moins un marché.")
                ElseIf cpte = 1 Then
                    Reponse = ConfirmMsg("Voulez-vous supprimer le marché coché?")
                ElseIf cpte > 1 Then
                    Reponse = ConfirmMsg("Voulez-vous vraiment supprimer les marchés cochés?")
                End If

                If Reponse = DialogResult.Yes Then
                    'Suppression des données 
                    DebutChargement(True, "Suppression en cours...")

                    For i = 0 To ViewEngag.RowCount - 1
                        If CBool(ViewEngag.GetRowCellValue(i, "Code")) = True Then
                            nummarche = ViewEngag.GetRowCellValue(i, "Numéro").ToString
                            RefMarche = ViewEngag.GetRowCellValue(i, "RefMarche").ToString

                            ExecuteNonQuery("DELETE FROM t_acteng WHERE Refmarche='" & RefMarche & "'")
                            Dim dt As DataTable = ExcecuteSelectQuery("select RefBesoinPartition FROM t_besoinmarche WHERE RefMarche='" & RefMarche & "'")
                            ExecuteNonQuery("DELETE FROM t_besoinmarche WHERE RefMarche='" & RefMarche & "'")
                            For Each rw In dt.Rows
                                ExecuteNonQuery("update t_repartitionparbailleur set RefMarche='0' where RefBesoinPartition='" & rw("RefBesoinPartition") & "'")
                            Next

                            If ViewEngag.GetRowCellValue(i, "TypeEngegement").ToString = "PPM" Then
                                ExecuteNonQuery("update t_marchesigne set NumMarcheDMP='', RefMarche='0', CodeCateg='0', Attributaire='' where NumeroMarche='" & EnleverApost(nummarche) & "'")
                            ElseIf ViewEngag.GetRowCellValue(i, "TypeEngegement").ToString = "BCMDE" Then
                                ExecuteNonQuery("DELETE FROM t_marchesigne WHERE NumeroMarche='" & EnleverApost(nummarche) & "'")
                            End If

                            'query = "select refmarche from t_marche where NumeroMarche='" & nummarche.ToString & "'"
                            'Dim RefMarche As String = ExecuteScallar(query)
                            'Try
                            '    If Val(RefMarche) <> 0 Then
                            '        ExecuteNonQuery("DELETE FROM t_acteng WHERE Refmarche='" & RefMarche & "'")
                            '        ExecuteNonQuery("DELETE FROM t_marchesigne WHERE Refmarche='" & RefMarche & "'")
                            '        Dim refbesoin As String = ""
                            '        ExecuteNonQuery("DELETE FROM t_besoinmarche WHERE RefMarche='" & RefMarche & "'")
                            '    End If
                            '    DeleteRecords2("t_marche", "NumeroMarche", nummarche)
                            '    DeleteRecords2("t_marchesigne", "NumeroMarche", nummarche)

                            Dim filepath As String = line & "\Marches\" & FormatFileName(nummarche, "_")
                            Try
                                If File.Exists(filepath & ".pdf") Then
                                    File.Delete(filepath & ".pdf")
                                End If
                            Catch ex As Exception
                                FinChargement()
                            End Try
                        End If
                    Next
                    FinChargement()

                    LoadData()
                    SuccesMsg("Suppression effectuée avec succès")
                End If
            Catch ex As Exception
                FinChargement()
                FailMsg(ex.ToString)
            End Try
        End If

    End Sub

    Private Sub BtModifier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtModifier.Click
        Try
            Dim bool As Boolean = False
            MustRefresh = False

            For i = 0 To ViewEngag.RowCount - 1

                If CBool(ViewEngag.GetRowCellValue(i, "Code")) = True Then

                    Dim NewModif As New Modif_engagement
                    NewModif.NumMarche = ViewEngag.GetRowCellValue(i, "Numéro").ToString
                    NewModif.RefsMarches = ViewEngag.GetRowCellValue(i, "RefMarche").ToString

                    'marche generer
                    If ViewEngag.GetRowCellValue(i, "TypeEngegement").ToString = "PPM" Then
                        NewModif.PeriodsMarches = ExecuteScallar("select PeriodeMarche from t_marche where RefMarche='" & ViewEngag.GetRowCellValue(i, "RefMarche") & "'")
                    Else 'Bon de commande
                        NewModif.PeriodsMarches = ""
                    End If

                    NewModif.Size = New Point(959, 584)

                    query = "SELECT COUNT(*) FROM t_comp_activite WHERE NumeroMarche='" & EnleverApost(ViewEngag.GetRowCellValue(i, "Numéro").ToString) & "'"
                    Dim cpte As Integer = Val(ExecuteScallar(query))
                    'query = "SELECT COUNT(*) from t_gf_demandepd WHERE NumeroMarche='" & ViewEngag.GetRowCellValue(i, "Numéro").ToString & "'"
                    Dim nbre As Integer = 0 'Val(ExecuteScallar(query))

                    If (cpte > 0) Or (nbre > 0) Then
                        NewModif.LgListAct.ContextMenu = New ContextMenu
                        NewModif.CmbBaill.Enabled = False
                        NewModif.CmbConv.Enabled = False
                        NewModif.CmbCatDep.Enabled = False
                        NewModif.Combact.Enabled = False
                        NewModif.txtcompte.Enabled = False
                        NewModif.txtmontant.Enabled = False
                        NewModif.BtAjout.Enabled = False
                        NewModif.BtEnr.Enabled = False
                    End If

                    Dialog_form(NewModif)
                    bool = True
                    ' MustRefresh = True
                End If
            Next

            If MustRefresh Then
                Try
                    LoadData()
                Catch ex As Exception
                    FailMsg(ex.ToString)
                End Try
            End If

            If bool = False Then
                SuccesMsg("Veuillez cocher un marché/bon de commande")
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try

    End Sub

    Private Sub BtAjouter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjouter.Click
        ' Saisie_engagement.Size = New Point(950, 575)
        Dialog_form(Saisie_engagement)
    End Sub

    Private Sub BtActualiser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtActualiser.Click
        Try
            LoadData()
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Checktous_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Checktous.CheckedChanged
        Try
            If (ViewEngag.RowCount > 0 And Checktous.Enabled = True) Then
                For k As Integer = 0 To ViewEngag.RowCount - 1
                    TabTrue(k) = Checktous.Checked
                Next

                If (Checktous.Checked = True) Then
                    nbTab = ViewEngag.RowCount
                Else
                    nbTab = 0
                End If

                LoadData()

            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        Dim visual As Boolean = False

        'suppression des données 
        For i = 0 To ViewEngag.RowCount - 1

            If CBool(ViewEngag.GetRowCellValue(i, "Code")) = True Then
                Dim FileName = line & "\Marches\" & FormatFileName(ViewEngag.GetRowCellValue(i, "Numéro").ToString & ".pdf", "_")
                If File.Exists(FileName) Then
                    Process.Start(FileName)
                End If
                visual = True
            End If
        Next

        If visual = False Then
            SuccesMsg("Veuillez cocher un marché/bon de commande")
        End If

    End Sub
End Class