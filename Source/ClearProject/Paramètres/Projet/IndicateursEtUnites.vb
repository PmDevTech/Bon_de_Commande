Imports MySql.Data.MySqlClient

Public Class IndicateursEtUnites
    Dim dtIndic = New DataTable()
    Dim dtUnite = New DataTable()
    Dim dtMoyen = New DataTable()

    Private Sub IndicateursEtUnites_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        TxtIndic.Text = ""
        TxtCodeUnite.Text = ""
        TxtMoyen.Text = ""
        TxtUnite.Text = ""
        ChargerIndic()
        ChargerUnite()
        ChargerMoyen()

        BtEnrgIndic.Enabled = True
        BtnModifIndic.Enabled = False
        BtEnrgUnite.Enabled = True
        BtnModifUnite.Enabled = False
        BtEnrgMoyen.Enabled = True
        BtnModifMoyen.Enabled = False
    End Sub

    Private Sub ChargerIndic()
        dtIndic.Columns.Clear()
        dtIndic.Columns.Add("Code", Type.GetType("System.String"))
        dtIndic.Columns.Add("Libellé", Type.GetType("System.String"))
        dtIndic.Columns.Add("Choix", Type.GetType("System.String"))
        Dim cptr As Decimal = 0
        'Dim Reader As MySqlDataReader
        query = "select * from T_Indicateur order by CodeIndicateur"
        dtIndic.Rows.Clear()
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtIndic.NewRow()

            drS(0) = rw(0).ToString
            drS(1) = MettreApost(rw(1).ToString)
            drS(2) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString

            dtIndic.Rows.Add(drS)
        Next

        GridIndic.DataSource = dtIndic

        ViewIndic.Columns(0).Visible = False
        ViewIndic.Columns(1).Width = GridIndic.Width - 18
        ViewIndic.Columns(2).Visible = False

        ViewIndic.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewIndic, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub

    Private Sub BtEnrgIndic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnrgIndic.Click

        If (TxtIndic.Text <> "") Then


            'Dim Reader As MySqlDataReader

            query = "select * from T_Indicateur where LibelleIndicateur='" & EnleverApost(TxtIndic.Text) & "'"
            Dim dt = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                MsgBox("Cet indicateur existe déjà!", MsgBoxStyle.Information)
                TxtIndic.Text = ""
            End If

            Dim DatSet = New DataSet
            query = "select * from T_Indicateur"

            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_Indicateur")
            Dim DatTable = DatSet.Tables("T_Indicateur")
            Dim DatRow = DatSet.Tables("T_Indicateur").NewRow()

            DatRow("LibelleIndicateur") = EnleverApost(TxtIndic.Text)

            DatSet.Tables("T_Indicateur").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_Indicateur")

            DatSet.Clear()
            BDQUIT(sqlconn)

            TxtIndic.Text = ""
            ChargerIndic()
            TxtIndic.Focus()
        End If

    End Sub

    Private Sub GridIndic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridIndic.Click
        If ViewIndic.RowCount > 0 Then
            drx = ViewIndic.GetDataRow(ViewIndic.FocusedRowHandle)

            BtEnrgIndic.Enabled = False
            BtnModifIndic.Enabled = True

            query = "select * from T_Indicateur where CodeIndicateur='" & drx(0).ToString & "'"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                TxtIndic.Text = MettreApost(rw(1).ToString)
            Next

            Dim IDL = drx(0).ToString
            ColorRowGrid(ViewIndic, "[Choix]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewIndic, "[Code]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)

        End If
    End Sub

    Private Sub BtnModifIndic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnModifIndic.Click
        If (TxtIndic.Text <> "") Then
            drx = ViewIndic.GetDataRow(ViewIndic.FocusedRowHandle)

            query = "UPDATE t_indicateur SET LibelleIndicateur = '" & EnleverApost(TxtIndic.Text) & "'  WHERE CodeIndicateur = '" & drx(0).ToString & "'"
            ExecuteNonQuery(query)

            MsgBox("MODIFICATION EFFECTUEE AVEC SUCCES", MsgBoxStyle.Information)
            TxtIndic.Text = ""
            BtEnrgIndic.Enabled = True
            BtnModifIndic.Enabled = False
            ChargerIndic()

        Else
            MsgBox("Veuillez selectionner une ligne dans le tableau !", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub SupprimerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupprimerToolStripMenuItem.Click
        If MsgBox("Voulez-vous vraiment supprimer?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
            drx = ViewIndic.GetDataRow(ViewIndic.FocusedRowHandle)


            Dim DatSet = New DataSet
            query = "DELETE FROM t_indicateur WHERE CodeIndicateur = '" & drx(0).ToString & "'"
            ExecuteNonQuery(query)

            MsgBox("SUPPRESSION EFFECTUE AVEC SUCCES", MsgBoxStyle.Exclamation)
            TxtIndic.Text = ""
            BtEnrgIndic.Enabled = True
            BtnModifIndic.Enabled = False
            ChargerIndic()
        End If
    End Sub

    Private Sub ChargerUnite()

        dtUnite.Columns.Clear()

        dtUnite.Columns.Add("Code Unite", Type.GetType("System.String"))
        dtUnite.Columns.Add("Symbole", Type.GetType("System.String"))
        dtUnite.Columns.Add("Libellé", Type.GetType("System.String"))

        query = "select * from T_Unite order by CodeUnite"
        dtUnite.Rows.Clear()
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Dim drS = dtUnite.NewRow()
            drS(0) = rw(0).ToString
            drS(1) = rw(1).ToString
            drS(2) = MettreApost(rw(2).ToString)

            dtUnite.Rows.Add(drS)
        Next

        GridUnite.DataSource = dtUnite
        ViewUnite.Columns(0).Visible = False
        ViewUnite.Columns(1).Width = 60
        ViewUnite.Columns(2).Width = GridUnite.Width - 78

        ViewUnite.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewUnite, "[Code Unite]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub

    Private Sub BtEnrgUnite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnrgUnite.Click

        If (TxtCodeUnite.Text <> "" And TxtUnite.Text <> "") Then

            query = "select * from T_Unite where LibelleUnite='" & EnleverApost(TxtUnite.Text) & "'"
            Dim dt = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                MsgBox("Cette unité existe déjà!", MsgBoxStyle.Information)
                Exit Sub
            End If

            Dim DatSet = New DataSet
            query = "select * from T_Unite"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_Unite")
            Dim DatTable = DatSet.Tables("T_Unite")
            Dim DatRow = DatSet.Tables("T_Unite").NewRow()

            DatRow("LibelleCourtUnite") = TxtCodeUnite.Text
            DatRow("LibelleUnite") = EnleverApost(TxtUnite.Text)

            DatSet.Tables("T_Unite").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_Unite")
            DatSet.Clear()
            BDQUIT(sqlconn)

            TxtCodeUnite.Text = ""
            TxtUnite.Text = ""
            ChargerUnite()
            TxtCodeUnite.Focus()
        End If

    End Sub

    Private Sub GridUnite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridUnite.Click
        If ViewUnite.RowCount > 0 Then
            drx = ViewUnite.GetDataRow(ViewUnite.FocusedRowHandle)
            BtEnrgUnite.Enabled = False
            BtnModifUnite.Enabled = True

            query = "select * from T_Unite where CodeUnite='" & drx(0).ToString & "'"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                TxtCodeUnite.Text = rw(1).ToString
                TxtUnite.Text = MettreApost(rw(2).ToString)
            Next

            Dim IDL = drx(1).ToString
            ColorRowGrid(ViewUnite, "[Code Unite]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewUnite, "[Symbole]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
        End If
    End Sub

    Private Sub BtnModifUnite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnModifUnite.Click
        If (TxtUnite.Text <> "") And (TxtCodeUnite.Text <> "") Then
            drx = ViewUnite.GetDataRow(ViewUnite.FocusedRowHandle)
            query = "UPDATE T_Unite SET LibelleCourtUnite = '" & EnleverApost(TxtCodeUnite.Text) & "',LibelleUnite = '" & EnleverApost(TxtUnite.Text) & "'  WHERE CodeUnite = '" & drx(0).ToString & "'"
            ExecuteNonQuery(query)

            MsgBox("MODIFICATION EFFECTUEE AVEC SUCCES", MsgBoxStyle.Information)
            TxtCodeUnite.Text = ""
            TxtUnite.Text = ""
            BtEnrgUnite.Enabled = True
            BtnModifUnite.Enabled = False
            ChargerUnite()

        Else
            MsgBox("Veuillez selectionner une ligne dans le tableau !", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub SupprimerToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupprimerToolStripMenuItem1.Click
        If MsgBox("Voulez-vous vraiment supprimer?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
            drx = ViewUnite.GetDataRow(ViewUnite.FocusedRowHandle)
            Dim DatSet = New DataSet
            query = "DELETE FROM T_Unite WHERE CodeUnite = '" & drx(0).ToString & "'"
            '
            'Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            'Dim DatAdapt = New MySqlDataAdapter(Cmd)
            'Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            'DatAdapt.Fill(DatSet, "T_Unite")
            '
            ExecuteNonQuery(query)
            MsgBox("SUPPRESSION EFFECTUE AVEC SUCCES", MsgBoxStyle.Exclamation)

            TxtCodeUnite.Text = ""
            TxtUnite.Text = ""
            BtEnrgUnite.Enabled = True
            BtnModifUnite.Enabled = False
            ChargerUnite()
        End If
    End Sub

    Private Sub ChargerMoyen()

        dtMoyen.Columns.Clear()

        dtMoyen.Columns.Add("Code", Type.GetType("System.String"))
        dtMoyen.Columns.Add("Libellé", Type.GetType("System.String"))
        dtMoyen.Columns.Add("Choix", Type.GetType("System.String"))

        Dim cptr As Decimal = 0

        'Dim Reader As MySqlDataReader

        query = "select * from T_MoyenVerif order by CodeMoyenVerif"
        dtMoyen.Rows.Clear()
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtMoyen.NewRow()

            drS(0) = rw(0).ToString
            drS(1) = MettreApost(rw(1).ToString)
            drS(2) = cptr

            dtMoyen.Rows.Add(drS)
        Next

        GridMoyen.DataSource = dtMoyen

        ViewMoyen.Columns(0).Visible = False
        ViewMoyen.Columns(1).Width = GridIndic.Width - 18
        ViewMoyen.Columns(2).Visible = False

        ViewMoyen.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewMoyen, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub

    Private Sub BtEnrgMoyen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnrgMoyen.Click

        If (TxtMoyen.Text <> "") Then


            'Dim Reader As MySqlDataReader

            query = "select * from T_MoyenVerif where LibelleMoyenVerif='" & EnleverApost(TxtMoyen.Text) & "'"
            Dim dt = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                MsgBox("Ce moyen de vérification existe déjà!", MsgBoxStyle.Information)
                Exit Sub
            End If

            Dim DatSet = New DataSet
            query = "select * from T_MoyenVerif"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_MoyenVerif")
            Dim DatTable = DatSet.Tables("T_MoyenVerif")
            Dim DatRow = DatSet.Tables("T_MoyenVerif").NewRow()

            DatRow("LibelleMoyenVerif") = EnleverApost(TxtMoyen.Text)

            DatSet.Tables("T_MoyenVerif").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_MoyenVerif")

            DatSet.Clear()
            BDQUIT(sqlconn)

            TxtMoyen.Text = ""
            ChargerMoyen()
            TxtMoyen.Focus()
        End If

    End Sub

    Private Sub GridMoyen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridMoyen.Click
        If ViewMoyen.RowCount > 0 Then
            drx = ViewMoyen.GetDataRow(ViewMoyen.FocusedRowHandle)
            BtEnrgMoyen.Enabled = False
            BtnModifMoyen.Enabled = True

            query = "select * from t_moyenverif where CodeMoyenVerif='" & drx(0).ToString & "'"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                TxtMoyen.Text = MettreApost(rw(1).ToString)
            Next

            Dim IDL = drx(0).ToString
            ColorRowGrid(ViewMoyen, "[Choix]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewMoyen, "[Code]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
        End If
    End Sub

    Private Sub BtnModifMoyen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnModifMoyen.Click
        If (TxtMoyen.Text <> "") Then
            drx = ViewMoyen.GetDataRow(ViewMoyen.FocusedRowHandle)
            query = "UPDATE t_moyenverif SET LibelleMoyenVerif = '" & EnleverApost(TxtMoyen.Text) & "'  WHERE CodeMoyenVerif = '" & drx(0).ToString & "'"
            ExecuteNonQuery(query)
            MsgBox("MODIFICATION EFFECTUEE AVEC SUCCES", MsgBoxStyle.Information)
            TxtMoyen.Text = ""
            BtEnrgMoyen.Enabled = True
            BtnModifMoyen.Enabled = False
            ChargerMoyen()
        Else
            MsgBox("Veuillez selectionner une ligne dans le tableau !", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub SupprimerToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupprimerToolStripMenuItem2.Click
        If MsgBox("Voulez-vous vraiment supprimer?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
            drx = ViewMoyen.GetDataRow(ViewMoyen.FocusedRowHandle)
            Dim DatSet = New DataSet
            query = "DELETE FROM t_moyenverif WHERE CodeMoyenVerif = '" & drx(0).ToString & "'"
            ExecuteNonQuery(query)
            MsgBox("SUPPRESSION EFFECTUE AVEC SUCCES", MsgBoxStyle.Exclamation)
            TxtMoyen.Text = ""
            BtEnrgMoyen.Enabled = True
            BtnModifMoyen.Enabled = False
            ChargerMoyen()
        End If
    End Sub
End Class