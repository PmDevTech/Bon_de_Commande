Imports System.Globalization
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports MySql.Data.MySqlClient

Public Class ListeRestreindreAMI

    Dim NbreCojoPointeAMI As Decimal = 0
    Dim CountNbreCojoAMI As Decimal = 0

    Private Sub ListeRestreindreAMI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ChargerListeDossier()
    End Sub


    Private Sub ChargerListeDossier()
        DossierAMI.Text = ""
        DossierAMI.Properties.Items.Clear()
        Try
            query = "select NumeroDAMI from t_ami where StatutDoss <>'Annulé' and EvalTechnique IS NOT NULL ORDER BY DateEdition DESC"
            dt = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                For Each rw In dt.Rows
                    DossierAMI.Properties.Items.Add(MettreApost(rw("NumeroDAMI").ToString))
                Next
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub


    Public Sub ChargerListeRestreindre(ByVal NumDossier As String)
        Dim dtt = New DataTable()
        dtt.Columns.Clear()
        dtt.Columns.Add("Code", Type.GetType("System.String"))
        dtt.Columns.Add("CodeX", Type.GetType("System.String"))
        dtt.Columns.Add("Consultant", Type.GetType("System.String"))
        dtt.Columns.Add("Moyenne", Type.GetType("System.String"))
        dtt.Columns.Add("Rang", Type.GetType("System.String"))
        dtt.Columns.Add("Décision", Type.GetType("System.String"))
        dtt.Rows.Clear()
        Try
            Dim Methode As String = ExecuteScallar("select MethodeSelection from t_ami where NumeroDAMI='" & EnleverApost(NumDossier.ToString) & "'")
            If Methode.ToString.ToUpper = "3CV" Then
                query = "select c.RefConsult, c.NomConsult, c.TelConsult, c.PaysConsult, s.Observations, s.RefSoumis, s.NoteConsult, s.ReferenceNote, s.RangConsult, s.EvalTechOk from t_consultant as c, t_soumissionconsultant as s where c.RefConsult=s.RefConsult And s.NumeroDp='" & EnleverApost(NumDossier) & "' and s.EvalTechOk='OUI' and s.RangConsult IS NOT NULL order by s.RangConsult ASC LIMIT 3"
            Else
                query = "select c.RefConsult, c.NomConsult, c.TelConsult, c.PaysConsult, s.Observations, s.RefSoumis, s.NoteConsult, s.ReferenceNote, s.RangConsult, s.EvalTechOk from t_consultant as c, t_soumissionconsultant as s where c.RefConsult=s.RefConsult And s.NumeroDp='" & EnleverApost(NumDossier) & "' and s.EvalTechOk='OUI' and s.RangConsult IS NOT NULL order by s.RangConsult ASC LIMIT 6"
            End If
            Dim dt As DataTable = ExcecuteSelectQuery(query)

            If dt.Rows.Count > 0 Then
                Dim cpte As Integer = 0
                For Each rw As DataRow In dt.Rows
                    cpte += 1

                    Dim drS = dtt.NewRow()

                    drS("Code") = rw("RefSoumis").ToString
                    drS("CodeX") = IIf(CDec(cpte / 2) = CDec(cpte \ 2), "x", "")

                    drS("Consultant") = MettreApost(rw("NomConsult").ToString)
                    drS("Moyenne") = rw("NoteConsult").ToString.ToString.Replace(".", ",") & " / " & rw("ReferenceNote").ToString
                    drS("Rang") = IIf(rw("RangConsult").ToString <> "0", rw("RangConsult").ToString & IIf(rw("RangConsult").ToString = "1", "er", "ème").ToString, "-").ToString
                    drS("Décision") = IIf(rw("EvalTechOk").ToString <> "", IIf(rw("EvalTechOk").ToString = "OUI", "ACCEPTE", "REFUSE").ToString, "-").ToString
                    dtt.Rows.Add(drS)
                Next

                GridLrs.DataSource = dtt
                ViewRs.OptionsView.ColumnAutoWidth = True

                GroupControl3AMI.Text = "Liste restreinte  (" & cpte.ToString & " Consultant(s) retenu(s))"
                ViewRs.Columns("Code").Visible = False
                ViewRs.Columns("CodeX").Visible = False

                ViewRs.Columns("Rang").Width = 100
                ViewRs.Columns("Moyenne").Width = 100
                ViewRs.Columns("Décision").Width = 100

                ViewRs.Columns("Moyenne").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ViewRs.Columns("Rang").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                ViewRs.Columns("Décision").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                ColorRowGrid(ViewRs, "[CodeX]='x'", Color.LightGray, "Tahoma", 10, FontStyle.Regular, Color.LightSkyBlue)

                BtInfoConnecter.Visible = False
                GridLrs.Visible = True
            Else
                GroupControl3AMI.Text = "Liste restreinte"
                BtInfoConnecter.Visible = True
                BtInfoConnecter.Text = "Aucun consultant retenu sur la liste restreinte"
                GridLrs.Visible = False
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub DossierAMI_SelectedValueChanged(sender As Object, e As EventArgs) Handles DossierAMI.SelectedValueChanged
        If DossierAMI.SelectedIndex <> -1 Then
            GroupControl3AMI.Text = "Liste restreinte"
            ChargerListeRestreindre(DossierAMI.Text)
        End If
    End Sub
End Class