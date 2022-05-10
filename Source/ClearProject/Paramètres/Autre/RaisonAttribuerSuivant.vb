Imports MySql.Data.MySqlClient

Public Class RaisonAttribuerSuivant

    Dim dtSoumis = New DataTable()
    Dim DrX As DataRow

    Private Sub RaisonAttribuerSuivant_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        TxtSoumDisq.Text = ReponseDialog
        ReponseDialog = ""
        TxtRaisonChange.Text = ""
        TxtRaisonChoix.Text = ""
        TxtSoumisAttrib.Text = ""
        RefSoumisFavoris.Text = ""
        ChargerSoumis()
    End Sub

    Private Sub ChargerSoumis()
        dtSoumis.Columns.Clear()
        dtSoumis.Columns.Add("Code", Type.GetType("System.String"))
        dtSoumis.Columns.Add("Soumissionnaire", Type.GetType("System.String"))
        dtSoumis.Columns.Add("Prix de l'offre", Type.GetType("System.String"))
        dtSoumis.Columns.Add("Prix en lettre", Type.GetType("System.String"))
        dtSoumis.Columns.Add("Classement", Type.GetType("System.String"))
        dtSoumis.Columns.Add("CodeX", Type.GetType("System.String"))
        dtSoumis.Rows.Clear()

        Dim cpt As Decimal = 0
        'query = "select F.NomFournis,S.RefSoumis,S.PrixCorrigeOffre,S.RangPostQualif from T_Fournisseur as F,T_SoumissionFournisseur as S where F.CodeFournis=S.CodeFournis and S.CodeLot='" & JugementOffres.CmbNumLotAttrib.Text & "' and S.ExamPQValide='OUI' and S.RefSoumis<>'" & ExceptRevue & "' and F.NumeroDAO='" & JugementOffres.CmbNumDoss.Text & "' order by S.RangPostQualif"
        query = "select F.NomFournis,S.CodeFournis,S.PrixCorrigeOffre,S.RangPostQualif from T_Fournisseur as F,t_soumissionfournisseurclassement as S where F.CodeFournis=S.CodeFournis and S.CodeLot='" & JugementOffres.CmbNumLotAttrib.Text & "' and S.ExamPQValide='OUI' and S.CodeFournis<>'" & ExceptRevue & "' and F.NumeroDAO=S.NumeroDAO and F.NumeroDAO='" & EnleverApost(JugementOffres.CmbNumDoss.Text) & "' order by S.RangPostQualif"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cpt += 1

            Dim DrT2 = dtSoumis.NewRow()
            DrT2("Code") = rw("CodeFournis").ToString
            DrT2("Soumissionnaire") = MettreApost(rw("NomFournis").ToString)
            DrT2("Prix de l'offre") = AfficherMonnaie(rw("PrixCorrigeOffre").ToString.Replace(" ", "")) & "  HT"
            DrT2("Prix en lettre") = MontantLettre(rw("PrixCorrigeOffre").ToString.Replace(" ", ""))
            DrT2("Classement") = rw(3).ToString & IIf(rw("RangPostQualif").ToString = "1", "er", "ème").ToString
            DrT2("CodeX") = IIf(cpt Mod 2 = 0, "x", "").ToString
            dtSoumis.Rows.Add(DrT2)
        Next
        GridNvFavoris.DataSource = dtSoumis

        ViewNvFavoris.Columns("Code").Visible = False
        ViewNvFavoris.Columns("CodeX").Visible = False
        ViewNvFavoris.Columns(1).Width = 250
        ViewNvFavoris.Columns(2).Width = 150
        ViewNvFavoris.Columns(3).Width = GridNvFavoris.Width - 518
        ViewNvFavoris.Columns(4).Width = 100

        ViewNvFavoris.Columns(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        ViewNvFavoris.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

        ViewNvFavoris.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far                  'Drawing.StringAlignment.Center
        ViewNvFavoris.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        ColorRowGrid(ViewNvFavoris, "[CodeX]='x'", Color.LightGray, "Tahoma", 8, FontStyle.Bold, Color.Black)

    End Sub

    Private Sub GridNvFavoris_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridNvFavoris.Click
        If (ViewNvFavoris.RowCount > 0) Then

            DrX = ViewNvFavoris.GetDataRow(ViewNvFavoris.FocusedRowHandle)
            RefSoumisFavoris.Text = DrX(0).ToString
            TxtSoumisAttrib.Text = DrX(1).ToString
        Else
            RefSoumisFavoris.Text = ""
            TxtSoumisAttrib.Text = ""
        End If

    End Sub

    Private Sub BtAnnuler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtAnnuler.Click
        ReponseDialog = ""
        Me.Close()
    End Sub

    Private Sub BtValider_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtValider.Click
        If TxtRaisonChange.IsRequiredControl("Veuillez saisir la raison de disqualification de ce fournisseur") Then
            TxtRaisonChange.Select()
            Exit Sub
        End If
        If TxtRaisonChoix.IsRequiredControl("Veuillez saisir la raison de choix de ce fournisseur") Then
            TxtRaisonChoix.Select()
            Exit Sub
        End If

        ExecuteNonQuery("Update t_soumissionfournisseurclassement set Selectionne='NON', MotifSelect='" & EnleverApost(TxtRaisonChange.Text) & "', FournisDisqualifie='OUI' where CodeFournis='" & ExceptRevue & "' and CodeLot='" & JugementOffres.CmbNumLotAttrib.Text & "' and NumeroDAO='" & EnleverApost(JugementOffres.CmbNumDoss.Text) & "'")

        ExecuteNonQuery("Update t_soumissionfournisseurclassement set Selectionne='OUI', MotifSelect='" & EnleverApost(TxtRaisonChoix.Text) & "', FournisDisqualifie='NON' where CodeFournis='" & RefSoumisFavoris.Text & "' and CodeLot='" & JugementOffres.CmbNumLotAttrib.Text & "' and NumeroDAO='" & EnleverApost(JugementOffres.CmbNumDoss.Text) & "'")
        ReponseDialog = TxtSoumisAttrib.Text
        Me.Close()
    End Sub
End Class