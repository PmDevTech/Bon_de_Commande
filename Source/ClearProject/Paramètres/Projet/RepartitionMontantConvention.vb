Imports MySql.Data.MySqlClient

Public Class RepartitionMontantConvention

    Dim Repart As String = ""
    Dim dtConv = New DataTable()
    Dim dtRepart = New DataTable()
    Dim DrX As DataRow

    Private Sub RepartitionMontantConvention_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        VerifTypeRepart()
        LblTypeRepart.Text = "REPARTITION PAR " & Repart.ToUpper

        ChargerGridRepart()
        ChargerConvention()

    End Sub

    Private Sub VerifTypeRepart()

        query = "select UniteRepartitionBudget from T_ParamTechProjet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        If dt0.Rows.Count > 0 Then

            Repart = dt0.Rows(0).Item(0).ToString

        End If


    End Sub

    Private Sub ChargerConvention()

        dtConv.Columns.Clear()

        dtConv.Columns.Add("Code", Type.GetType("System.String"))
        dtConv.Columns.Add("Numéro", Type.GetType("System.String"))
        dtConv.Columns.Add("Montant total", Type.GetType("System.String"))
        dtConv.Columns.Add("Montant alloué", Type.GetType("System.String"))
        dtConv.Columns.Add("Montant restant", Type.GetType("System.String"))

        Dim cptr As Decimal = 0
        dtConv.Rows.Clear()
        CmbConvention.Properties.Items.Clear()
        query = "select C.CodeConvention, C.MontantConvention from T_Bailleur as B, T_Convention as C where B.CodeBailleur=C.CodeBailleur and B.CodeProjet='" & ProjetEnCours & "' order by C.CodeConvention"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            CmbConvention.Properties.Items.Add(MettreApost(rw(0).ToString))

            cptr += 1
            Dim drS = dtConv.NewRow()

            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = MettreApost(rw(0).ToString)
            drS(2) = AfficherMonnaie(rw(1).ToString)
            drS(3) = AlloueRestant(rw(1).ToString, rw(0).ToString)(0)
            drS(4) = AlloueRestant(rw(1).ToString, rw(0).ToString)(1)

            dtConv.Rows.Add(drS)
        Next
        GridConvention.DataSource = dtConv

        ViewConvention.Columns(0).Visible = False
        ViewConvention.Columns(1).Width = 150
        ViewConvention.Columns(2).Width = 150
        ViewConvention.Columns(3).Width = 150
        ViewConvention.Columns(4).Width = 150

        ViewConvention.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        ViewConvention.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewConvention.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewConvention.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

        ViewConvention.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

        ColorRowGrid(ViewConvention, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
        ColorRowGridAnal(ViewConvention, "[Montant restant]='0'", Color.LightGray, "Times New Roman", 11, FontStyle.Bold, Color.Black, False)

    End Sub

    Private Function AlloueRestant(ByVal MontTot As String, ByVal Conv As String, Optional ByVal Partition As String = "") As String()

        Dim montAlloue As Decimal = 0
        Dim montRestant As Decimal = CDec(MontTot.Replace(" ", ""))
        query = "select B.MontantAlloue from T_Partition as P, T_Partition_Budget as B where B.CodePartition=P.CodePartition and P.CodeProjet='" & ProjetEnCours & "' and B.CodeConvention='" & Conv & "'" & IIf(Partition <> "", " and B.CodePartition='" & Partition & "'", "").ToString
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            montAlloue += CDec(rw(0))
        Next

        montRestant = CDec(MontTot.Replace(" ", "")) - montAlloue
        Return {AfficherMonnaie(montAlloue.ToString), AfficherMonnaie(montRestant.ToString)}

    End Function

    Private Sub ChargerGridRepart()

        dtRepart.Columns.Clear()

        dtRepart.Columns.Add("CodeX", Type.GetType("System.String"))
        dtRepart.Columns.Add("Ref", Type.GetType("System.String"))
        dtRepart.Columns.Add("Code", Type.GetType("System.String"))
        dtRepart.Columns.Add("Libellé", Type.GetType("System.String"))

        Dim nbConv As Integer = 0
        Dim LaConv(10) As String
        
        query = "select C.CodeConvention from T_Bailleur as B, T_Convention as C where B.CodeBailleur=C.CodeBailleur and B.CodeProjet='" & ProjetEnCours & "' order by C.CodeConvention"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            LaConv(nbConv) = rw(0).ToString
            nbConv += 1
            dtRepart.Columns.Add(rw(0).ToString, Type.GetType("System.String"))
        Next

        dtRepart.Columns.Add("Montant total", Type.GetType("System.String"))

        Dim cptr As Decimal = 0
        Dim Reqt As String = ""
        If (Repart = "Composante") Then
            Reqt = "select CodePartition, LibelleCourt, LibellePartition from T_Partition where LENGTH(LibelleCourt)='1' and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
        ElseIf (Repart = "Sous composante") Then
            Reqt = "select CodePartition, LibelleCourt, LibellePartition from T_Partition where CodeClassePartition=2 and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
        ElseIf (Repart = "Activité") Then
            Reqt = "select CodePartition, LibelleCourt, LibellePartition from T_Partition where CodeClassePartition='5' and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
        End If
        query = Reqt
        dtRepart.Rows.Clear()
        dt0 = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows

            cptr += 1
            Dim drS = dtRepart.NewRow()

            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = rw(0).ToString
            drS(2) = Repart & " " & rw(1).ToString
            drS(3) = MettreApost(rw(2).ToString)

            Dim TotPart As Decimal = 0
            For k As Integer = 0 To nbConv - 1
                drS(k + 4) = AlloueRestant("0", LaConv(k), rw(0).ToString)(0)
                TotPart += CDec(AlloueRestant("0", LaConv(k), rw(0).ToString)(0).Replace(" ", ""))
            Next

            drS(nbConv + 4) = AfficherMonnaie(TotPart.ToString)

            dtRepart.Rows.Add(drS)

        Next

        GridReparti.DataSource = dtRepart

        ViewReparti.Columns(0).Visible = False
        ViewReparti.Columns(1).Visible = False
        ViewReparti.Columns(2).Width = 150
        ViewReparti.Columns(3).Width = 300
        For k As Integer = 0 To nbConv - 1
            ViewReparti.Columns(k + 4).Width = 120
        Next
        ViewReparti.Columns(nbConv + 4).Width = 150

        ViewReparti.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewReparti.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        For k As Integer = 0 To nbConv - 1
            ViewReparti.Columns(k + 4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Next
        ViewReparti.Columns(nbConv + 4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

        ViewReparti.Appearance.Row.Font = New Font("Times New Roman", 12, FontStyle.Regular)

        ColorRowGrid(ViewReparti, "[CodeX]='x'", Color.LightGray, "Times New Roman", 12, FontStyle.Regular, Color.Black)

        CmbConvention.Enabled = False
        TxtMontant.Enabled = False
        TxtPrct.Enabled = False
        BtOk.Enabled = False

    End Sub

    Private Sub GridReparti_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridReparti.DoubleClick

        If (ViewReparti.RowCount > 0) Then
            DrX = ViewReparti.GetDataRow(ViewReparti.FocusedRowHandle)

            TxtRef.Text = DrX(1).ToString
            TxtCode.Text = DrX(2).ToString.Replace(Repart & " ", "")
            TxtLibelle.Text = DrX(3).ToString

            CmbConvention.Enabled = True
            TxtMontant.Enabled = True
            TxtPrct.Enabled = True
            BtOk.Enabled = True

        End If

    End Sub

    Private Sub CmbConvention_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbConvention.SelectedValueChanged

        If (CmbConvention.Text <> "") Then

            Dim montConTot As String = "0"

            query = "select MontantConvention from T_Convention where CodeConvention='" & CmbConvention.Text & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                montConTot = rw(0).ToString
            Next

            TxtMontRestConv.Text = AlloueRestant(montConTot, CmbConvention.Text)(1).Replace(" ", "")
            TxtMontConv.Text = montConTot
            If (TxtRef.Text <> "") Then
                Dim montPart As String = "0"
                query = "select MontantAlloue from T_Partition_Budget where CodeConvention='" & CmbConvention.Text & "' and CodePartition='" & TxtRef.Text & "'"
                dt0 = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    montPart = rw(0).ToString
                Next
                TxtMontant.Text = montPart
            End If
        End If
    End Sub

    Private Sub TxtMontant_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMontant.TextChanged

        If (IsNumeric(TxtMontant.EditValue.ToString) = True And CmbConvention.Text <> "") Then

            If (CDec(TxtMontant.EditValue.ToString) <= CDec(TxtMontRestConv.Text)) Then

                TxtMontLettre.Text = MontantLettre(TxtMontant.EditValue.ToString)
                TxtPrct.Text = Math.Round((CDec(TxtMontant.EditValue.ToString) * 100) / CDec(TxtMontConv.Text), 3).ToString
                BtOk.Enabled = True

            Else
                TxtMontLettre.Text = "Ce montant est supérieur au montant restant de la convention (" & AfficherMonnaie(TxtMontRestConv.Text) & ") !"
                TxtPrct.Text = "-!-"
                BtOk.Enabled = False
            End If

        Else
            TxtMontLettre.Text = "Veuillez entrer un montant et une convention corrects!"
            TxtPrct.Text = "-!-"
            BtOk.Enabled = False
        End If

    End Sub

    Private Sub BtOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtOk.Click

        If (TxtRef.Text <> "" And CmbConvention.Text <> "" And TxtPrct.Text <> "-!-") Then

            Dim montTotAct As Decimal = 0
            query = "select B.QteNature, B.PUNature from T_Partition as P, T_BesoinPartition as B where B.CodePartition=P.CodePartition and P.CodeProjet='" & ProjetEnCours & "' and LENGTH(P.LibelleCourt)='5' and LibelleCourt like '" & TxtCode.Text & "%'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                montTotAct += (CDec(rw(0)) * CDec(rw(1)))
            Next

            Dim Allocation As String = TxtMontant.EditValue.ToString
            If (montTotAct > CDec(TxtMontant.Text)) Then
                Dim RepAlloc As MsgBoxResult = MsgBox("Le montant estimatif actuel de cette " & Repart.ToLower & " est de " & MontantLettre(montTotAct.ToString) & " (" & AfficherMonnaie(montTotAct.ToString) & ") ." & vbNewLine & "Cliquez [OUI] pour lui allouer ce montant ou [NON] pour continuer avec votre montant.", MsgBoxStyle.YesNoCancel)
                If (RepAlloc = MsgBoxResult.Cancel) Then
                    TxtMontant.Focus()
                    Exit Sub
                ElseIf (RepAlloc = MsgBoxResult.Yes) Then

                    If (montTotAct > CDec(TxtMontRestConv.Text)) Then
                        MsgBox("Le montant restant sur cette convention n'est pas suffisant!" & vbNewLine & "Le montant que vous avez défini sera donc conservé comme allocation.", MsgBoxStyle.Information)
                    Else
                        Allocation = montTotAct.ToString
                    End If

                End If

            End If

            query = "DELETE from T_Partition_Budget where CodePartition='" & TxtRef.Text & "' and CodeConvention='" & CmbConvention.Text & "'"
            ExecuteNonQuery(query)

            Dim DatSet = New DataSet
            query = "select * from T_Partition_Budget"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_Partition_Budget")
            Dim DatTable = DatSet.Tables("T_Partition_Budget")
            Dim DatRow = DatSet.Tables("T_Partition_Budget").NewRow()

            DatRow("CodePartition") = TxtRef.Text
            DatRow("CodeConvention") = CmbConvention.Text
            DatRow("MontantAlloue") = Allocation

            DatSet.Tables("T_Partition_Budget").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_Partition_Budget")
            DatSet.Clear()
            BDQUIT(sqlconn)

            TxtMontConv.Text = ""
            TxtMontRestConv.Text = ""
            CmbConvention.Text = ""
            TxtMontant.Text = ""
            TxtPrct.Text = ""
            TxtMontLettre.Text = ""

            ChargerConvention()
            ChargerGridRepart()
        End If

    End Sub

End Class