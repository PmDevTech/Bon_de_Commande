Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class jour_ferie

    Dim dtjourf = New DataTable()
    Dim DrX As DataRow

    Private Sub InitFormulaire()
        DateEdit1.Text = ""
        TextEdit1.Text = ""
        BtEnregistrer.Enabled = True
        BtModifier.Enabled = False
        Chargerdatagrid()
    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click

        Dim erreur As String = ""

        If DateEdit1.Text = "" Then
            erreur += "- Renseigner la Date" & ControlChars.CrLf
        End If

        If TextEdit1.Text = "" Then
            erreur += "- Renseigner le libellé" & ControlChars.CrLf
        End If

        If erreur = "" Then

            query = "select * from jour_ferier where date_jf='" & dateconvert(DateEdit1.Text) & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            If dt0.Rows.Count = 0 Then

               query= "INSERT INTO jour_ferier values (NULL,'" & dateconvert(DateEdit1.Text) & "','" & EnleverApost(TextEdit1.Text) & "')"
                ExecuteNonQuery(query)
                SuccesMsg("Jour ferié ajouté.")
                DateEdit1.Text = ""
                TextEdit1.Text = ""

            Else
                SuccesMsg("Ce jour existe déjà.")
            End If

        Else
            SuccesMsg("Veuillez : " & ControlChars.CrLf + erreur)
        End If

        Chargerdatagrid()
    End Sub

    Private Sub Chargerdatagrid()

        dtjourf.Columns.Clear()
        dtjourf.Columns.Add("Code", Type.GetType("System.String"))
        dtjourf.Columns.Add("Date", Type.GetType("System.String"))
        dtjourf.Columns.Add("Libellé", Type.GetType("System.String"))


        query = "select * from jour_ferier order by date_jf"
        dtjourf.Rows.Clear()
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows

            Dim drS = dtjourf.NewRow()
            drS(0) = rw(0).ToString
            drS(1) = CDate(rw(1)).ToString("dd/MM/yyyy")
            drS(2) = MettreApost(rw(2).ToString)
            dtjourf.Rows.Add(drS)

        Next

        GridControl1.DataSource = dtjourf
        GridView1.Columns(0).Visible = False
        GridView1.Columns(1).Width = 100
        GridView1.Columns(2).Width = 500
        GridView1.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(GridView1, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub

    Private Sub XtraForm1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        Chargerdatagrid()
        InitFormulaire()
    End Sub

    Private Sub SimpleButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtModifier.Click
        'convertion de la date en date anglaise
        Try

            DrX = GridView1.GetDataRow(GridView1.FocusedRowHandle)
           query= "Update jour_ferier set  date_jf='" & dateconvert(DateEdit1.Text) & "', libelle='" & EnleverApost(TextEdit1.Text) & "' where id='" & DrX(0).ToString & "'"
            ExecuteNonQuery(query)
            InitFormulaire()

        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub BtSupprimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSupprimer.Click
        If GridView1.RowCount > 0 And GridView1.FocusedRowHandle <> -1 Then
            If ConfirmMsg("Voulez-vous vraiment supprimer le jour sélectionné?") = DialogResult.Yes Then

                DrX = GridView1.GetDataRow(GridView1.FocusedRowHandle)
                query = "delete from jour_ferier where id='" & DrX(0).ToString & "'"
                ExecuteNonQuery(query)
                InitFormulaire()

            End If

        End If
    End Sub

    Private Sub GridControl1_Click(sender As System.Object, e As System.EventArgs) Handles GridControl1.Click
        If (GridView1.RowCount > 0) Then
            DrX = GridView1.GetDataRow(GridView1.FocusedRowHandle)
            BtEnregistrer.Enabled = False
            BtModifier.Enabled = True

            Dim IDL = DrX(1).ToString
            ColorRowGrid(GridView1, "[Code]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(GridView1, "[Date]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)

            DateEdit1.Text = CDate(DrX(1)).ToString("dd/MM/yyyy")
            TextEdit1.Text = MettreApost(DrX(2).ToString)
        End If
    End Sub

    Private Sub SimpleButton1_Click_1(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        DateEdit1.Text = ""
        TextEdit1.Text = ""
        InitFormulaire()
    End Sub

    Private Sub TextEdit2_TextChanged(sender As Object, e As System.EventArgs) Handles TextEdit2.TextChanged
        Try

            dtjourf.Columns.Clear()
            dtjourf.Columns.Add("Code", Type.GetType("System.String"))
            dtjourf.Columns.Add("Date", Type.GetType("System.String"))
            dtjourf.Columns.Add("Libellé", Type.GetType("System.String"))

            Dim cptr As Decimal = 0

           query= "select * from jour_ferier where date_jf like '%" & TextEdit2.Text & "%' or libelle like '%" & TextEdit2.Text & "%'"
            dtjourf.Rows.Clear()
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                Dim drS = dtjourf.NewRow()
                drS(0) = rw(0).ToString
                drS(1) = CDate(rw(1)).ToString("yyyy-MM-dd")
                drS(2) = MettreApost(rw(2).ToString)
                dtjourf.Rows.Add(drS)
            Next

            GridControl1.DataSource = dtjourf
            GridView1.Columns(0).Visible = False
            GridView1.Columns(1).Width = 100
            GridView1.Columns(2).Width = 500
            GridView1.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
            ColorRowGrid(GridView1, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

        Catch ex As Exception
            FailMsg("Information non disponible" & vbNewLine & ex.ToString)
        End Try
    End Sub

End Class