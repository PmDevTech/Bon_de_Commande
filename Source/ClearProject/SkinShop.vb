Imports DevExpress.LookAndFeel
Imports MySql.Data.MySqlClient

Public Class SkinShop

    Dim dtSkin = New DataTable()
    Dim DrX As DataRow

    Dim DrX0 As DataRow
    Dim NewLoad As Boolean = False

    Private Sub SkinShop_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        'TODO: cette ligne de code charge les données dans la table 'DataSet1.T_OperateurSkin'. Vous pouvez la déplacer ou la supprimer selon vos besoins.
        'Me.T_OperateurSkinTableAdapter.Fill(Me.DataSet1.T_OperateurSkin)
        ChargerSkin()
        NewLoad = True

    End Sub

    Private Sub ChargerSkin()

        dtSkin.Columns.Clear()
        dtSkin.Columns.Add("*", Type.GetType("System.String"))
        dtSkin.Columns.Add("B", Type.GetType("System.String"))
        dtSkin.Columns.Add(">", Type.GetType("System.String"))
        dtSkin.Rows.Clear()

        query = "select CodeSkin, LibelleSkin, DescriptionSkin from T_OperateurSkin order by CodeSkin"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows

            Dim drS = dtSkin.NewRow()
            drS(0) = rw(0).ToString
            drS(1) = rw(1).ToString
            drS(2) = rw(2).ToString
            dtSkin.Rows.Add(drS)

        Next


        GridSkin.DataSource = dtSkin

        LayoutSkin.Columns(1).Visible = False
        LayoutSkin.Columns(2).AppearanceCell.Font = New Font("Segoe Print", 12, FontStyle.Bold)
        LayoutSkin.Columns(0).LayoutViewField.TextVisible = False
        LayoutSkin.Columns(2).LayoutViewField.TextVisible = False


    End Sub

    Private Sub SkinShop_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

        If (NewLoad = True) Then

            Dim posSkin As Decimal = 0
            query = "select CodeSkin from T_Operateur where UtilOperateur='" & CodeUtilisateur & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                If dt.Rows.Count > 0 Then
                    posSkin = CInt(Mid(rw(0).ToString, 2, 2))
                    posSkin = posSkin - 1
                End If
            Next

            LayoutSkin.FocusedRowHandle = posSkin
            DrX0 = LayoutSkin.GetDataRow(LayoutSkin.FocusedRowHandle)
            Me.LookAndFeel.SkinName = DrX0(1).ToString

        End If

    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        DrX0 = LayoutSkin.GetDataRow(LayoutSkin.FocusedRowHandle)
        Me.LookAndFeel.SkinName = DrX0(1).ToString
        Timer1.Stop()

    End Sub

    Private Sub ControlNavigator1_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles ControlNavigator1.ButtonClick

        NewLoad = False
        Timer1.Interval = 370 'Timer1_Tick sera déclenché toutes les secondes.
        Timer1.Start() 'On démarre le Timer

    End Sub

    Private Sub BtAppercu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtAppercu.Click

        DebutChargement(True, "Chargement de l'apperçu en cours...")
        If (LayoutSkin.RowCount > 0) Then
            DrX0 = LayoutSkin.GetDataRow(LayoutSkin.FocusedRowHandle)
            UserLookAndFeel.Default.SetSkinStyle(DrX0(1).ToString)
        End If
        FinChargement()

    End Sub

    Private Sub BtValider_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtValider.Click

        DebutChargement(True, "Application du thème permanent en cours...")
        If (LayoutSkin.RowCount > 0) Then
            DrX0 = LayoutSkin.GetDataRow(LayoutSkin.FocusedRowHandle)
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim DatSet = New DataSet
            query = "select * from T_Operateur where UtilOperateur='" & CodeUtilisateur & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Fill(DatSet, "T_Operateur")
            DatSet.Tables!T_Operateur.Rows(0)!CodeSkin = DrX0(0).ToString
            DatAdapt.Update(DatSet, "T_Operateur")
            DatSet.Clear()
            BDQUIT(sqlconn)
            SkinActu = DrX0(1).ToString
            UserLookAndFeel.Default.SetSkinStyle(SkinActu)

        End If

        Me.Close()
        'FinChargement()

    End Sub


End Class