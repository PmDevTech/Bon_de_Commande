Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Etat_engPai

    Private Sub btimprim_Click(sender As System.Object, e As System.EventArgs) Handles btimprim.Click

        If dtd.Text <> "" And dtf.Text <> "" Then

           query= "select count(*) from t_comp_activite a, t_comp_activite_payer p, t_marchesigne m where a.code_e=p.code_e and a.numeromarche=m.numeromarche and STR_TO_DATE(m.DateMarche,'%d/%m/%Y')>='" & dateconvert(dtd.Text) & "' and STR_TO_DATE(m.DateMarche,'%d/%m/%Y')<='" & dateconvert(dtf.Text) & "'"
            Dim nbre = ExecuteScallar(query)
            If nbre = 0 Then
                MsgBox("Aucun paiement enregistré", MsgBoxStyle.Information, "ClearProject")
            Else

                Dim parfrs As New ReportDocument
                Dim crtableLogoninfos As New TableLogOnInfos
                Dim crtableLogoninfo As New TableLogOnInfo
                Dim crConnectionInfo As New ConnectionInfo
                Dim CrTables As Tables
                Dim CrTable As Table

                Dim Chemin As String = lineEtat & "\Engagements\"

                Dim DatSet = New DataSet
                parfrs.Load(Chemin & "Engagement3.rpt")

                With crConnectionInfo
                    .ServerName = ODBCNAME
                    .DatabaseName = DB
                    .UserID = USERNAME
                    .Password = PWD
                End With

                CrTables = parfrs.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

                parfrs.SetDataSource(DatSet)
                parfrs.SetParameterValue("Date1", dtd.Text)
                parfrs.SetParameterValue("Date2", dtf.Text)
                parfrs.SetParameterValue("CodeProjet", ProjetEnCours)

                FullScreenReport.FullView.ReportSource = parfrs
                FullScreenReport.ShowDialog()

            End If

        Else
            MsgBox("Veuillez entrer une période valide", MsgBoxStyle.Information)
        End If

    End Sub

    Private Sub Etat_engPai_Load(sender As System.Object, e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        'date
        dtd.Text = CDate(ExerciceComptable.Rows(0).Item("datedebut")).ToString("dd/MM/yyyy")
        dtf.Text = CDate(ExerciceComptable.Rows(0).Item("datefin")).ToString("dd/MM/yyyy")
        dtd.Properties.MinValue = ExerciceComptable.Rows(0).Item("datedebut").ToString
        dtf.Properties.MinValue = ExerciceComptable.Rows(0).Item("datedebut").ToString
        dtd.Properties.MaxValue = ExerciceComptable.Rows(0).Item("datefin").ToString
        dtf.Properties.MaxValue = ExerciceComptable.Rows(0).Item("datefin").ToString
        'query = "select datedebut, datefin from T_COMP_EXERCICE where encours='1'"
        'Dim dt As DataTable = ExcecuteSelectQuery(query)
        'For Each rw In dt.Rows
        'Next
    End Sub

    Private Sub dtd_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles dtd.EditValueChanged
        'If dtd.Text <> "" And dtf.Text <> "" Then
        '    If DateTime.Compare(CDate(dtf.Text), CDate(dtd.Text)) >= 0 Then
        '    Else
        '        dtd.Text = ""
        '        SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
        '    End If
        'End If
    End Sub

    Private Sub dtf_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles dtf.EditValueChanged
        'If dtd.Text <> "" And dtf.Text <> "" Then
        '    If DateTime.Compare(CDate(dtf.Text), CDate(dtd.Text)) >= 0 Then
        '    Else
        '        dtf.Text = ""
        '        SuccesMsg("La date de fin doit être supérieure ou égale à la date de début.")
        '    End If
        'End If
    End Sub
End Class