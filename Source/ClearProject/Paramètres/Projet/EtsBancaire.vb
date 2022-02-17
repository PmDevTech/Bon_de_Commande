Imports MySql.Data.MySqlClient

Public Class EtsBancaire

    Dim dtEts = New DataTable()

    Private Sub EtsBancaire_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        ChargerGridEts()
        ChargerPays()

    End Sub

    Private Sub ChargerPays()

        query = "select LibelleZone from T_ZoneGeo where CodeZoneMere='0' order by LibelleZone"
        CmbPays.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbPays.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next

    End Sub

    Private Sub CmbPays_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbPays.SelectedValueChanged

        Dim CodePays As String = ""
        query = "select CodeZone, IndicZone from T_ZoneGeo where CodeZoneMere='0' and LibelleZone='" & EnleverApost(CmbPays.Text) & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            CodePays = dt.Rows(0).Item(0).ToString
            TxtIndic1.Text = dt.Rows(0).Item(1).ToString
            TxtIndic2.Text = dt.Rows(0).Item(1).ToString
        End If
        ChargerVille(CodePays)

    End Sub

    Private Sub ChargerVille(ByVal CodeP As String)

        query = "select LibelleZone from T_ZoneGeo where CodeZoneMere='" & CodeP & "' order by LibelleZone"
        CmbVille.Text = ""
        CmbVille.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbVille.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next

    End Sub

    Private Sub BtAnnuler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtAnnuler.Click

        For Each Ctrls In GroupControl1.Controls
            If Not (TypeOf (Ctrls) Is DevExpress.XtraEditors.LabelControl) Then
                Ctrls.Text = ""
            End If
        Next

    End Sub

    Private Sub BtEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click

        For Each Ctrls In GroupControl1.Controls
            If Not (TypeOf (Ctrls) Is DevExpress.XtraEditors.SimpleButton) Then
                If (Ctrls.Name <> TxtMail.Name) Then
                    If (Ctrls.Text.Replace(" ", "") = "") Then
                        MsgBox("Formulaire incomplet!", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                End If
            End If
        Next

        query = "select * from T_Banque where CodeBanque<>'" & TxtCodeBanque.Text & "' and SwiftBanque='" & TxtSwift.Text & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            MsgBox("Ce code swift appartient déjà à un établissement!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim DatSet = New DataSet
        query = "SELECT * FROM T_Banque"
        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)
        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd)
        DatAdapt.Fill(DatSet, "T_Banque")
        Dim DatTable = DatSet.Tables("T_Banque")
        Dim DatRow = DatSet.Tables("T_Banque").NewRow()

        DatRow("CodeBanque") = TxtCodeBanque.Text
        DatRow("NomCompletBanque") = EnleverApost(TxtNomBanque.Text)
        DatRow("PaysBanque") = EnleverApost(CmbPays.Text)
        DatRow("VilleBanque") = EnleverApost(CmbVille.Text)
        DatRow("AdresseBanque") = EnleverApost(TxtAdresse.Text)
        DatRow("TelBanque") = TxtTel.Text
        DatRow("FaxBanque") = TxtFax.Text
        DatRow("MailBanque") = TxtMail.Text
        DatRow("SwiftBanque") = TxtSwift.Text
        DatRow("CodeProjet") = ProjetEnCours

        DatSet.Tables("T_Banque").Rows.Add(DatRow)
        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        DatAdapt.Update(DatSet, "T_Banque")
        DatSet.Clear()


        ChargerGridEts()
        EffacerTexBox4(GroupControl1)
    End Sub

    Private Sub ChargerGridEts()

        dtEts.Columns.Clear()
        dtEts.Columns.Add("RefBanque", Type.GetType("System.String"))
        dtEts.Columns.Add("Code", Type.GetType("System.String"))
        dtEts.Columns.Add("Etablissement", Type.GetType("System.String"))
        dtEts.Columns.Add("Code swift", Type.GetType("System.String"))
        dtEts.Columns.Add("Pays", Type.GetType("System.String"))
        dtEts.Columns.Add("Ville", Type.GetType("System.String"))
        dtEts.Columns.Add("Adresse", Type.GetType("System.String"))
        dtEts.Columns.Add("Téléphone", Type.GetType("System.String"))
        dtEts.Columns.Add("Télécopie", Type.GetType("System.String"))
        dtEts.Columns.Add("E-mail", Type.GetType("System.String"))

        dtEts.Rows.Clear()

        Dim NbTotal As Decimal = 0

        query = "select RefBanque,CodeBanque, NomCompletBanque, SwiftBanque, PaysBanque, VilleBanque, AdresseBanque, TelBanque, FaxBanque, MailBanque from T_Banque where CodeProjet='" & ProjetEnCours & "' order by CodeBanque"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            NbTotal += 1
            Dim drS = dtEts.NewRow()

            drS(0) = rw(0).ToString
            drS(1) = MettreApost(rw(1).ToString)
            drS(2) = rw(2).ToString
            drS(3) = MettreApost(rw(3).ToString)
            drS(4) = MettreApost(rw(4).ToString)
            drS(5) = MettreApost(rw(5).ToString)
            drS(6) = rw(6).ToString
            drS(7) = rw(7).ToString
            drS(8) = rw(8).ToString
            drS(9) = rw(9).ToString
            dtEts.Rows.Add(drS)

        Next

        GridEts.DataSource = dtEts
        ViewEts.Columns(0).Visible = False
        ViewEts.Columns(1).Width = 250
        ViewEts.Columns(2).Width = 80
        ViewEts.Columns(3).Width = 150
        ViewEts.Columns(4).Width = 150
        ViewEts.Columns(5).Width = 150
        ViewEts.Columns(6).Width = 100
        ViewEts.Columns(7).Width = 100
        ViewEts.Columns(8).Width = 200
        ViewEts.Columns(9).Width = 200

        ViewEts.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewEts.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewEts.Columns(7).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewEts.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

        ViewEts.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewEts, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub

    Private Sub SimpleButton2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        drx = ViewEts.GetDataRow(ViewEts.FocusedRowHandle)
        Dim result = MessageBox.Show("Voulez-vous Supprimer la Banque", "ClearProject", MessageBoxButtons.YesNo)
        If result = DialogResult.No Then

        ElseIf result = DialogResult.Yes Then

            Dim RefBanque As String = ""
            query = "select NumeroCompte from t_comptebancaire where RefBanque='" & drx(0).ToString & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            If dt.Rows.Count = 0 Then
               query= "delete from t_banque where RefBanque='" & drx(0).ToString & "'"
                ExecuteNonQuery(query)
            Else
                MsgBox("Banque Déjà Utilisé dans le Compte Bancaire !!!")
            End If
            ChargerGridEts()
            EffacerTexBox4(GroupControl1)
        End If
    End Sub

    Private Sub GridEts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEts.Click
        If (ViewEts.RowCount > 0) Then
            drx = ViewEts.GetDataRow(ViewEts.FocusedRowHandle)
            Dim IDL = drx(1).ToString
            ColorRowGrid(ViewEts, "[RefBanque]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewEts, "[Code]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
        End If
    End Sub

    Private Sub GridEts_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEts.DoubleClick
        If ViewEts.RowCount > 0 Then
            drx = ViewEts.GetDataRow(ViewEts.FocusedRowHandle)
            query = "select * from t_banque where RefBanque='" & drx(0).ToString & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                TxtCodeBanque.Text = MettreApost(rw(1).ToString)
                TxtNomBanque.Text = MettreApost(rw(2).ToString)
                TxtSwift.Text = MettreApost(rw(9).ToString)
                TxtAdresse.Text = MettreApost(rw(5).ToString)
                TxtMail.Text = MettreApost(rw(8).ToString)
                TxtTel.Text = MettreApost(rw(6).ToString)
                TxtFax.Text = MettreApost(rw(7).ToString)
                CmbPays.Text = MettreApost(rw(3).ToString)
                CmbVille.Text = MettreApost(rw(4).ToString)
            Next
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        'Code de modification de l'enregistrement choisi
        If (TxtAdresse.Text <> "") And (TxtNomBanque.Text <> "") And (TxtCodeBanque.Text <> "") And (CmbPays.Text <> "") And (CmbVille.Text <> "") Then

            drx = ViewEts.GetDataRow(ViewEts.FocusedRowHandle)
            query = "UPDATE t_banque SET CodeBanque = '" & TxtCodeBanque.Text & "', NomCompletBanque = '" & EnleverApost(TxtNomBanque.Text) & "', PaysBanque = '" & EnleverApost(CmbPays.Text) & "', VilleBanque = '" & EnleverApost(CmbVille.Text) & "', AdresseBanque = '" & EnleverApost(TxtAdresse.Text) & "'   WHERE RefBanque='" & drx(0).ToString & "'"
            ExecuteNonQuery(query)

            MsgBox("MODIFICATION EFFECTUE AVEC SUCCES", MsgBoxStyle.Information)

            ChargerGridEts()
            EffacerTexBox4(GroupControl1)
        Else
            MsgBox("Veuillez selectionner une ligne dans le tableau !", MsgBoxStyle.Exclamation)
        End If
    End Sub
End Class