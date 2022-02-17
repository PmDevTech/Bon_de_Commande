Imports MySql.Data.MySqlClient

Public Class EvalConsult_PtsFortFaibleAMI

    Dim dtNoteCons = New DataTable()
    Dim dtForts = New DataTable()
    Dim dtFaibles = New DataTable()
    Dim RefSoum As String = ""

    Private Sub EvalConsult_PtsFortFaibleAMI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        ChargerConsult()

    End Sub

    Private Sub ChargerConsult()

        query = "select C.NomConsult from T_Consultant as C,T_SoumissionConsultant as S where S.RefConsult=C.RefConsult and C.NumeroDp='" & EvaluationTDR.CmbNumDoss.Text & "' order by C.NomConsult"
        CmbConsult.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbConsult.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next

    End Sub

    Private Sub CmbConsult_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbConsult.SelectedValueChanged
        Dim Code As String = ""
        query = "select RefConsult from T_Consultant where NumeroDp='" & EvaluationTDR.CmbNumDoss.Text & "' and NomConsult='" & EnleverApost(CmbConsult.Text) & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Code = rw(0).ToString
        Next

        query = "select S.Refsoumis from T_Consultant as C,T_SoumissionConsultant as S where C.RefConsult=S.RefConsult and C.RefConsult='" & Code & "' and C.NumeroDp='" & EvaluationTDR.CmbNumDoss.Text & "'"
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        For Each rw1 As DataRow In dt1.Rows
            RefSoum = rw1(0).ToString
        Next

        ChargerNoteConsult()
        ChargerForts()
        ChargerFaibles()
    End Sub

    Private Sub ChargerNoteConsult()

        dtNoteCons.Columns.Clear()
        dtNoteCons.Columns.Add("Code", Type.GetType("System.String"))
        dtNoteCons.Columns.Add("Critères / sous-critères", Type.GetType("System.String"))
        dtNoteCons.Columns.Add("Points obtenus", Type.GetType("System.String"))
        dtNoteCons.Rows.Clear()

        query = "select RefCritere,CodeCritere,IntituleCritere,TypeCritere,PointCritere from T_DP_CritereEval where NumeroDp='" & EvaluationTDR.CmbNumDoss.Text & "' and CritereParent='0' order by RefCritere"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Dim drS = dtNoteCons.NewRow()

            drS(0) = "x"
            drS(1) = MettreApost(rw(1).ToString & "/ " & rw(2).ToString)
            drS(2) = IIf(rw(3).ToString = "Note", Note(rw(0).ToString).ToString & " / " & rw(4).ToString, "").ToString

            dtNoteCons.Rows.Add(drS)

            If (rw(3).ToString <> "Note") Then

                query = "select RefCritere,CodeCritere,IntituleCritere,TypeCritere,PointCritere from T_DP_CritereEval where NumeroDp='" & EvaluationTDR.CmbNumDoss.Text & "' and CritereParent='" & rw(0).ToString & "' order by RefCritere"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows

                    Dim drS1 = dtNoteCons.NewRow()
                    drS1(0) = ""
                    drS1(1) = MettreApost(rw1(1).ToString & "/ " & rw1(2).ToString)
                    drS1(2) = IIf(rw1(3).ToString = "Note", Note(rw1(0).ToString).ToString & " / " & rw1(4).ToString, "").ToString

                    dtNoteCons.Rows.Add(drS1)

                    If (rw1(3).ToString <> "Note") Then


                        'Dim Reader10 As MySqlDataReader
                        query = "select RefCritere,CodeCritere,IntituleCritere,TypeCritere,PointCritere from T_DP_CritereEval where NumeroDp='" & EvaluationTDR.CmbNumDoss.Text & "' and CritereParent='" & rw1(0).ToString & "' order by RefCritere"
                        Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw2 As DataRow In dt2.Rows

                            Dim drS10 = dtNoteCons.NewRow()
                            drS10(0) = ""
                            drS10(1) = MettreApost(rw2(1).ToString & "/ " & rw2(2).ToString)
                            drS10(2) = IIf(rw2(3).ToString = "Note", Note(rw2(0).ToString).ToString & " / " & rw2(4).ToString, "").ToString
                            dtNoteCons.Rows.Add(drS10)

                        Next
                    End If
                Next
            End If
        Next

        GridNoteConsult.DataSource = dtNoteCons
        ViewNoteConsult.Columns(0).Visible = False
        ViewNoteConsult.Columns(1).Width = GridNoteConsult.Width - 118
        ViewNoteConsult.Columns(2).Width = 100
        ViewNoteConsult.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ViewNoteConsult.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ColorRowGrid(ViewNoteConsult, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Bold, Color.Black)

    End Sub

    Private Function Note(ByVal Critere As String) As Decimal

        Dim Tamp As Decimal = 0
        query = "select NoteConsult from T_SoumisNoteConsult where RefSoumis='" & RefSoum & "' and RefCritere='" & Critere & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Tamp += CDec(rw(0))
        Next

        Dim nbEval As Integer = 0
        query = "select Count(*) from T_Commission where NumeroDAO='" & EvaluationTDR.CmbNumDoss.Text & "' and TypeComm='EVAC'"
        nbEval = Val(ExecuteScallar(query))

        If (nbEval <> 0) Then
            Return Math.Round(Tamp / nbEval, 2)
        Else
            Return 0
        End If

    End Function

    Private Sub ChargerForts()

        dtForts.Columns.Clear()
        dtForts.Columns.Add("Code", Type.GetType("System.String"))
        dtForts.Columns.Add("Points forts", Type.GetType("System.String"))
        dtForts.Rows.Clear()

        Dim cptr As Decimal = 0
        query = "select DescripPtFort from T_SoumissionConsultant_PtFort where RefSoumis='" & RefSoum & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtForts.NewRow()
            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = MettreApost(rw(0).ToString)
            dtForts.Rows.Add(drS)
        Next

        GridPtsForts.DataSource = dtForts
        ViewPtsForts.Columns(0).Visible = False
        ViewPtsForts.Columns(1).Width = GridPtsForts.Width - 18
        ViewPtsForts.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewPtsForts, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub

    Private Sub ChargerFaibles()

        dtFaibles.Columns.Clear()
        dtFaibles.Columns.Add("Code", Type.GetType("System.String"))
        dtFaibles.Columns.Add("Points forts", Type.GetType("System.String"))
        dtFaibles.Rows.Clear()

        Dim cptr As Decimal = 0
        query = "select DescripPtFaible from T_SoumissionConsultant_PtFaible where RefSoumis='" & RefSoum & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtFaibles.NewRow()
            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = MettreApost(rw(0).ToString)
            dtFaibles.Rows.Add(drS)
        Next

        GridPtsFaibles.DataSource = dtFaibles
        ViewPtsFaibles.Columns(0).Visible = False
        ViewPtsFaibles.Columns(1).Width = GridPtsFaibles.Width - 18
        ViewPtsFaibles.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewPtsFaibles, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub

    Private Sub TxtPtFort_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPtFort.KeyDown

        If (e.KeyCode = Keys.Enter And RefSoum <> "") Then

            Dim DatSet = New DataSet
            query = "select * from T_SoumissionConsultant_PtFort"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_SoumissionConsultant_PtFort")
            Dim DatTable = DatSet.Tables("T_SoumissionConsultant_PtFort")
            Dim DatRow = DatSet.Tables("T_SoumissionConsultant_PtFort").NewRow()

            DatRow("RefSoumis") = RefSoum
            DatRow("DescripPtFort") = EnleverApost(TxtPtFort.Text)

            DatSet.Tables("T_SoumissionConsultant_PtFort").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_SoumissionConsultant_PtFort")
            DatSet.Clear()
            BDQUIT(sqlconn)

            ChargerForts()
            TxtPtFort.Text = ""
            TxtPtFort.Focus()
        End If

    End Sub

    Private Sub TxtPtFaible_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPtFaible.KeyDown

        If (e.KeyCode = Keys.Enter And RefSoum <> "") Then

            Dim DatSet = New DataSet
            query = "select * from T_SoumissionConsultant_PtFaible"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_SoumissionConsultant_PtFaible")
            Dim DatTable = DatSet.Tables("T_SoumissionConsultant_PtFaible")
            Dim DatRow = DatSet.Tables("T_SoumissionConsultant_PtFaible").NewRow()

            DatRow("RefSoumis") = RefSoum
            DatRow("DescripPtFaible") = EnleverApost(TxtPtFaible.Text)

            DatSet.Tables("T_SoumissionConsultant_PtFaible").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_SoumissionConsultant_PtFaible")
            DatSet.Clear()
            BDQUIT(sqlconn)

            ChargerFaibles()
            TxtPtFaible.Text = ""
            TxtPtFaible.Focus()
        End If

    End Sub

    Private Sub BtFermer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtFermer.Click
        Me.Close()
    End Sub
End Class