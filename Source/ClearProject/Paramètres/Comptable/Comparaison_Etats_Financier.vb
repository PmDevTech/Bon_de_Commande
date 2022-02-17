Imports System.Math
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Comparaison_Etats_Financier

    Private Sub LgListComparaison_Click(sender As System.Object, e As System.EventArgs)
        If (ViewComparaison.RowCount > 0) Then
            Dim DrX = ViewComparaison.GetDataRow(ViewComparaison.FocusedRowHandle)
            Dim IDL = DrX(1).ToString
            ColorRowGrid(ViewComparaison, "[CodeX]='x'", Color.White, "Times New Roman", 9, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewComparaison, "[Rubriques]='" & IDL & "'", Color.Navy, "Times New Roman", 9, FontStyle.Bold, Color.White, True)
        End If
    End Sub

    Private Sub rdComptesRattaches_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdComptesRattaches.CheckedChanged
        DebutChargement()
        ViderGrille()
        Dim dtcomparaison = New DataTable
        dtcomparaison.Columns.Clear()
        dtcomparaison.Columns.Add("CodeX", Type.GetType("System.String"))
        dtcomparaison.Columns.Add("Rubriques", Type.GetType("System.String"))
        dtcomparaison.Columns.Add("Comptes rattachés", Type.GetType("System.String"))
        dtcomparaison.Columns.Add("Comptes utilisés", Type.GetType("System.String"))
        dtcomparaison.Rows.Clear()
        If cmbRubrique.SelectedIndex <> -1 Then
            If rdComptesRattaches.Checked Then

                query = "DELETE FROM t_competat WHERE etat='" & cmbRubrique.Text & "'"
                ExecuteNonQuery(query)

                'récupération de la date de l'exercice en cours
                Dim DateDebutExercice = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
                Dim DateFinExercice = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")


                Dim NbTotal As Decimal = 0
                Dim compte As String = ""
                query = "select r.code_rub, GROUP_CONCAT(`code_sc` ORDER BY t.code_sc SEPARATOR ', ') as Comptes from t_comp_type_rubrique t, t_comp_rubrique r where r.code_rub=t.code_rub and r.etat_rub='" & cmbRubrique.Text & "' Group By r.code_rub"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows

                    NbTotal += 1
                    Dim drS = dtcomparaison.NewRow()
                    drS(0) = If(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                    drS(1) = rw("code_rub").ToString
                    drS(2) = rw("Comptes").ToString

                    Dim tab() As String
                    tab = Split(rw("Comptes"), ", ")
                    Dim ComptesUtilises As String = String.Empty
                    For i As Integer = 0 To tab.Length - 1

                        query = "select distinct(`code_sc`) as code_sc from t_comp_ligne_ecriture where (Date_le>= '" & dateconvert(DateDebutExercice) & "' and Date_le<='" & dateconvert(DateFinExercice) & "') and code_sc like '" & tab(i) & "%' AND (DEBIT_LE<>0 OR CREDIT_LE<>0)"
                        Dim dtCompteUtilise As DataTable = ExcecuteSelectQuery(query)
                        For Each rwUtilise As DataRow In dtCompteUtilise.Rows
                            ComptesUtilises += rwUtilise("code_sc").ToString & ", "
                        Next
                    Next

                    'On enleve le caractère éventuel ", " qui peut etre à la fin des comptes utilisés
                    If ComptesUtilises.Length > 0 Then
                        If Mid(ComptesUtilises, ComptesUtilises.Length - 1) = ", " Then
                            ComptesUtilises = Mid(ComptesUtilises, 1, ComptesUtilises.Length - 2)
                        End If
                    End If

                    drS(3) = ComptesUtilises

                    'enregistrement dans la table
                    query = "INSERT INTO t_competat VALUES(NULL,'" & rw("code_rub").ToString & "','" & rw("Comptes").ToString & "','" & ComptesUtilises & "','" & cmbRubrique.Text & "')"
                    ExecuteNonQuery(query)
                    dtcomparaison.Rows.Add(drS)
                Next
            End If
        End If
        LgListComparaison.DataSource = dtcomparaison
        ViewComparaison.Columns(0).Visible = False
        ViewComparaison.Columns(1).MaxWidth = 90
        ViewComparaison.OptionsView.ColumnAutoWidth = True
        ViewComparaison.OptionsBehavior.AutoExpandAllGroups = True
        ViewComparaison.VertScrollVisibility = True
        ViewComparaison.HorzScrollVisibility = True
        ViewComparaison.BestFitColumns()
        FinChargement()
    End Sub
    Private Sub rdComptesNonRattaches_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdComptesNonRattaches.CheckedChanged
        DebutChargement()
        ViderGrille()

        Dim dtcomparaison = New DataTable
        dtcomparaison.Columns.Clear()
        dtcomparaison.Columns.Add("CodeX", Type.GetType("System.String"))
        dtcomparaison.Columns.Add("Comptes non rattachés", Type.GetType("System.String"))
        dtcomparaison.Columns.Add("Compte utilisé", Type.GetType("System.String"))
        dtcomparaison.Rows.Clear()
        If cmbRubrique.SelectedIndex <> -1 Then
            If rdComptesNonRattaches.Checked Then

                query = "DELETE FROM t_competat WHERE etat='" & cmbRubrique.Text & "'"
                ExecuteNonQuery(query)

                'récupération de la date de l'exercice en cours
                Dim DateDebutExercice = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
                Dim DateFinExercice = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")
                Dim NbTotal As Decimal = 0

                If cmbRubrique.Text = "Bilan" Then

                    'query = "SELECT * FROM t_comp_sous_classe WHERE (CODE_SC LIKE '1%' OR CODE_SC LIKE '2%' OR CODE_SC LIKE '3%' OR CODE_SC LIKE '4%' OR CODE_SC LIKE '5%')"
                    'Dim dt As DataTable = ExcecuteSelectQuery(query)
                    'For Each rw As DataRow In dt.Rows
                    '    If VerifCompte(rw("CODE_SC"), cmbRubrique.Text) Then
                    '        Continue For
                    '    Else
                    '        NbTotal += 1
                    '        Dim drS = dtcomparaison.NewRow()
                    '        drS(0) = If(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                    '        drS(1) = rw("CODE_SC").ToString
                    '        query = "select distinct(`code_sc`) as code_sc from t_comp_ligne_ecriture where (Date_le>= '" & dateconvert(DateDebutExercice) & "' and Date_le<='" & dateconvert(DateFinExercice) & "') and code_sc like '" & rw("CODE_SC").ToString & "%' AND (DEBIT_LE<>0 OR CREDIT_LE<>0)"
                    '        Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
                    '        If dtVerif.Rows.Count > 0 Then
                    '            drS(2) = "Oui"
                    '        Else
                    '            drS(2) = "Non"
                    '        End If
                    '        dtcomparaison.Rows.Add(drS)
                    '    End If
                    'Next

                    query = "select distinct(`code_sc`) as code_sc from t_comp_ligne_ecriture where (Date_le>= '" & dateconvert(DateDebutExercice) & "' and Date_le<='" & dateconvert(DateFinExercice) & "') AND (DEBIT_LE<>0 OR CREDIT_LE<>0) AND (CODE_SC LIKE '1%' OR CODE_SC LIKE '2%' OR CODE_SC LIKE '3%' OR CODE_SC LIKE '4%' OR CODE_SC LIKE '5%')"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt1.Rows
                        If VerifCompte(rw("CODE_SC"), cmbRubrique.Text) Then
                            Continue For
                        Else
                            NbTotal += 1
                            Dim drS = dtcomparaison.NewRow()
                            drS(0) = If(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                            drS(1) = rw("CODE_SC").ToString
                            drS(2) = "Oui"
                            dtcomparaison.Rows.Add(drS)
                        End If
                    Next

                    query = "SELECT CODE_SC FROM t_comp_sous_classe WHERE (CODE_SC LIKE '1%' OR CODE_SC LIKE '2%' OR CODE_SC LIKE '3%' OR CODE_SC LIKE '4%' OR CODE_SC LIKE '5%') AND CODE_SC NOT IN (select distinct(`code_sc`) as code_sc from t_comp_ligne_ecriture where (Date_le>= '" & dateconvert(DateDebutExercice) & "' and Date_le<='" & dateconvert(DateFinExercice) & "') AND (DEBIT_LE<>0 OR CREDIT_LE<>0))"
                    dt1 = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt1.Rows
                        If VerifCompte(rw("CODE_SC"), cmbRubrique.Text) Then
                            Continue For
                        Else
                            NbTotal += 1
                            Dim drS = dtcomparaison.NewRow()
                            drS(0) = If(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                            drS(1) = rw("CODE_SC").ToString
                            drS(2) = "Non"
                            dtcomparaison.Rows.Add(drS)
                        End If
                    Next

                ElseIf cmbRubrique.Text = "Compte de résultat" Then
                    query = "select distinct(`code_sc`) as code_sc from t_comp_ligne_ecriture where (Date_le>= '" & dateconvert(DateDebutExercice) & "' and Date_le<='" & dateconvert(DateFinExercice) & "') AND (DEBIT_LE<>0 OR CREDIT_LE<>0) AND MID(CODE_SC,1,1)>5"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt1.Rows
                        If VerifCompte(rw("CODE_SC"), cmbRubrique.Text) Then
                            Continue For
                        Else
                            NbTotal += 1
                            Dim drS = dtcomparaison.NewRow()
                            drS(0) = If(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                            drS(1) = rw("CODE_SC").ToString
                            drS(2) = "Oui"
                            dtcomparaison.Rows.Add(drS)
                        End If
                    Next

                    query = "SELECT CODE_SC FROM t_comp_sous_classe WHERE MID(CODE_SC,1,1)>5 AND CODE_SC NOT IN (select distinct(`code_sc`) as code_sc from t_comp_ligne_ecriture where (Date_le>= '" & dateconvert(DateDebutExercice) & "' and Date_le<='" & dateconvert(DateFinExercice) & "') AND (DEBIT_LE<>0 OR CREDIT_LE<>0))"
                    dt1 = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt1.Rows
                        If VerifCompte(rw("CODE_SC"), cmbRubrique.Text) Then
                            Continue For
                        Else
                            NbTotal += 1
                            Dim drS = dtcomparaison.NewRow()
                            drS(0) = If(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                            drS(1) = rw("CODE_SC").ToString
                            drS(2) = "Non"
                            dtcomparaison.Rows.Add(drS)
                        End If
                    Next

                ElseIf cmbRubrique.Text = "Tableau Emplois Ressources" Then
                    'query = "SELECT * FROM t_comp_sous_classe"
                    'Dim dt As DataTable = ExcecuteSelectQuery(query)
                    'For Each rw As DataRow In dt.Rows
                    '    If VerifCompte(rw("CODE_SC"), cmbRubrique.Text) Then
                    '        Continue For
                    '    Else
                    '        NbTotal += 1
                    '        Dim drS = dtcomparaison.NewRow()
                    '        drS(0) = If(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                    '        drS(1) = rw("CODE_SC").ToString
                    '        query = "select distinct(`code_sc`) as code_sc from t_comp_ligne_ecriture where (Date_le>= '" & dateconvert(DateDebutExercice) & "' and Date_le<='" & dateconvert(DateFinExercice) & "') and code_sc like '" & rw("CODE_SC").ToString & "%' AND (DEBIT_LE<>0 OR CREDIT_LE<>0)"
                    '        Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
                    '        If dtVerif.Rows.Count > 0 Then
                    '            drS(2) = "Oui"
                    '        Else
                    '            drS(2) = "Non"
                    '        End If
                    '        dtcomparaison.Rows.Add(drS)
                    '    End If
                    'Next

                    query = "select distinct(`code_sc`) as code_sc from t_comp_ligne_ecriture where (Date_le>= '" & dateconvert(DateDebutExercice) & "' and Date_le<='" & dateconvert(DateFinExercice) & "') AND (DEBIT_LE<>0 OR CREDIT_LE<>0)"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt1.Rows
                        If VerifCompte(rw("CODE_SC"), cmbRubrique.Text) Then
                            Continue For
                        Else
                            NbTotal += 1
                            Dim drS = dtcomparaison.NewRow()
                            drS(0) = If(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                            drS(1) = rw("CODE_SC").ToString
                            drS(2) = "Oui"
                            dtcomparaison.Rows.Add(drS)
                        End If
                    Next

                    query = "SELECT CODE_SC FROM t_comp_sous_classe WHERE CODE_SC NOT IN (select distinct(`code_sc`) as code_sc from t_comp_ligne_ecriture where (Date_le>= '" & dateconvert(DateDebutExercice) & "' and Date_le<='" & dateconvert(DateFinExercice) & "') AND (DEBIT_LE<>0 OR CREDIT_LE<>0))"
                    dt1 = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt1.Rows
                        If VerifCompte(rw("CODE_SC"), cmbRubrique.Text) Then
                            Continue For
                        Else
                            NbTotal += 1
                            Dim drS = dtcomparaison.NewRow()
                            drS(0) = If(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
                            drS(1) = rw("CODE_SC").ToString
                            drS(2) = "Non"
                            dtcomparaison.Rows.Add(drS)
                        End If
                    Next
                End If
            End If
        End If
        LgListComparaison.DataSource = dtcomparaison
        ViewComparaison.Columns(0).Visible = False
        ViewComparaison.OptionsView.ColumnAutoWidth = True
        ViewComparaison.OptionsBehavior.AutoExpandAllGroups = True
        ViewComparaison.VertScrollVisibility = True
        ViewComparaison.HorzScrollVisibility = True
        ViewComparaison.BestFitColumns()
        FinChargement()
    End Sub
    Private Function VerifCompte(ByVal Compte As String, Etat As String) As Boolean
        query = "SELECT * FROM t_comp_type_rubrique WHERE CODE_SC LIKE '" & Mid(Compte, 1, 1) & "%' AND ETAT_RUB='" & Etat & "'"
        Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
        If dtVerif.Rows.Count > 0 Then
            Return True
        End If
        query = "SELECT * FROM t_comp_type_rubrique WHERE CODE_SC LIKE '" & Mid(Compte, 1, 2) & "%' AND ETAT_RUB='" & Etat & "'"
        dtVerif = ExcecuteSelectQuery(query)
        If dtVerif.Rows.Count > 0 Then
            Return True
        End If
        query = "SELECT * FROM t_comp_type_rubrique WHERE CODE_SC LIKE '" & Mid(Compte, 1, 3) & "%' AND ETAT_RUB='" & Etat & "'"
        dtVerif = ExcecuteSelectQuery(query)
        If dtVerif.Rows.Count > 0 Then
            Return True
        End If
        query = "SELECT * FROM t_comp_type_rubrique WHERE CODE_SC LIKE '" & Mid(Compte, 1, 4) & "%' AND ETAT_RUB='" & Etat & "'"
        dtVerif = ExcecuteSelectQuery(query)
        If dtVerif.Rows.Count > 0 Then
            Return True
        End If
        query = "SELECT * FROM t_comp_type_rubrique WHERE CODE_SC LIKE '" & Compte & "%' AND ETAT_RUB='" & Etat & "'"
        dtVerif = ExcecuteSelectQuery(query)
        If dtVerif.Rows.Count > 0 Then
            Return True
        End If
        Return False
    End Function
    Private Sub Comparaison_Etats_Financier_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        cmbRubrique.SelectedIndex = -1
        rdComptesNonRattaches.Checked = False
        rdComptesRattaches.Checked = False
    End Sub
    Private Sub ViderGrille()
        ViewComparaison.Columns.Clear()
        LgListComparaison.DataSource = New Object
    End Sub
    Private Sub cmbRubrique_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbRubrique.SelectedIndexChanged
        Try
            If cmbRubrique.SelectedIndex <> -1 Then
                If rdComptesNonRattaches.Checked Then
                    rdComptesNonRattaches_CheckedChanged(sender, e)
                ElseIf rdComptesRattaches.Checked Then
                    rdComptesRattaches_CheckedChanged(sender, e)
                Else
                    ViderGrille()
                End If
            Else
                ViderGrille()
            End If

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & vbNewLine & ex.ToString())
        End Try
    End Sub

    Private Sub btPrint_Click(sender As System.Object, e As System.EventArgs) Handles btPrint.Click
        If Not Access_Btn("BtnPrintComparaisonCompte") Then
            Exit Sub
        End If

        If rdComptesRattaches.Checked = True Then
            Dim Bilancompte As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table

            Dim Chemin As String = lineEtat & "\Comptabilite\"

            Dim DatSet = New DataSet
            Bilancompte.Load(Chemin & "ComparaisonCompte.rpt")

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = Bilancompte.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            Bilancompte.SetDataSource(DatSet)
            Bilancompte.SetParameterValue("CodeProjet", ProjetEnCours)
            Bilancompte.SetParameterValue("Etat", cmbRubrique.Text)

            FullScreenReport.FullView.ReportSource = Bilancompte
            FinChargement()
            FullScreenReport.ShowDialog()
        End If
    End Sub
End Class