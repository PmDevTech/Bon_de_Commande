Imports MySql.Data.MySqlClient

Public Class MarcheSigne

    Dim dtMarche = New DataTable()
    Dim DrX As DataRow

    Private Sub MarcheSigne_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        ChargerDAO()
        ChargerFournis()
        ChargerMarche()



    End Sub

    Private Sub ChargerDAO()

        query = "select distinct(L.NumeroDAO) from T_LotDAO as L,T_MarcheSigne as M,T_DAO as D where M.RefLot=L.RefLot and L.NumeroDAO=D.NumeroDAO and D.CodeProjet='" & ProjetEnCours & "'"
        CmbDAO.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbDAO.Properties.Items.Add(rw(0).ToString)
        Next

    End Sub

    Private Sub ChargerFournis()

        query = "select distinct(F.NomFournis) from T_MarcheSigne as M,T_Fournisseur as F where M.CodeFournis=F.CodeFournis and F.CodeProjet='" & ProjetEnCours & "'"
        CmbFournis.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbFournis.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next

    End Sub

    Private Sub ChargerMarche()

        If CmbDAO.Text <> "" Or CmbFournis.Text <> "" Or TxtNumMarcheSearch.Text <> "" Or CmbEtat.Text <> "" Then
            dtMarche.Columns.Clear()

            dtMarche.Columns.Add("Code", Type.GetType("System.String"))
            dtMarche.Columns.Add("N° Marche", Type.GetType("System.String"))
            dtMarche.Columns.Add("Type", Type.GetType("System.String"))
            dtMarche.Columns.Add("Libelle / Description", Type.GetType("System.String"))
            dtMarche.Columns.Add("Financement", Type.GetType("System.String"))
            dtMarche.Columns.Add("Montant", Type.GetType("System.String"))
            dtMarche.Columns.Add("Fournisseur", Type.GetType("System.String"))
            dtMarche.Columns.Add("Date signature", Type.GetType("System.String"))
            dtMarche.Columns.Add("Durée", Type.GetType("System.String"))
            dtMarche.Columns.Add("Date de fin", Type.GetType("System.String"))
            dtMarche.Columns.Add("Etat", Type.GetType("System.String"))

            Dim cptr As Decimal = 0

            'query = "select M.NumeroMarche,D.TypeMarche,D.IntituleDAO,L.LibelleLot,N.InitialeBailleur,S.PrixCorrigeOffre,F.NomFournis,M.DateMarche,S.DelaiLivraison,M.EtatMarche from T_MarcheSigne as M, T_Fournisseur as F, T_SoumissionFournisseur as S, T_LotDAO as L, T_DAO as D, T_Marche as N where M.CodeFournis=F.CodeFournis and S.RefSoumis=M.RefSoumis and M.RefLot=L.RefLot and L.NumeroDAO=D.NumeroDAO and D.NumeroDAO=N.NumeroDAO and L.NumeroDAO like '" & CmbDAO.Text & "%' and D.CodeProjet='" & ProjetEnCours & "' and F.NomFournis like '" & EnleverApost(CmbFournis.Text) & "%' and M.EtatMarche like '" & CmbEtat.Text & "%' and M.NumeroMarche like '%" & TxtNumMarcheSearch.Text & "%'"
            'query = "select M.NumeroMarche,D.TypeMarche,D.IntituleDAO,L.LibelleLot,N.InitialeBailleur,P.PrixCorrigeOffre,F.NomFournis,M.DateMarche,S.DelaiLivraison,M.EtatMarche from T_MarcheSigne as M, T_Fournisseur as F, T_SoumissionFournisseur as S, t_soumissionfournisseurclassement as P, T_LotDAO as L, T_DAO as D, T_Marche as N where M.CodeFournis=F.CodeFournis AND S.CodeFournis=P.CodeFournis and S.CodeFournis=F.CodeFournis and S.CodeFournis=M.CodeFournis AND P.CodeFournis=M.CodeFournis and M.RefLot=L.RefLot and M.RefLot=S.RefLot and L.NumeroDAO=D.NumeroDAO AND S.CodeLot=P.CodeLot and D.NumeroDAO=N.NumeroDAO and P.NumeroDAO=D.NumeroDAO and D.CodeProjet='" & ProjetEnCours & "' and L.NumeroDAO like'" & CmbDAO.Text & "%' and F.NomFournis like '" & EnleverApost(CmbFournis.Text) & "%' and M.EtatMarche like '" & CmbEtat.Text & "%' and M.NumeroMarche like '%" & TxtNumMarcheSearch.Text & "%'"
            query = "select M.NumeroMarche,D.TypeMarche,D.IntituleDAO,L.LibelleLot,N.InitialeBailleur,P.PrixCorrigeOffre,F.NomFournis,M.DateMarche,S.DelaiLivraison,M.EtatMarche from T_MarcheSigne as M, T_Fournisseur as F, T_SoumissionFournisseur as S, t_soumissionfournisseurclassement as P, T_LotDAO as L, T_DAO as D, T_Marche as N where M.CodeFournis=F.CodeFournis AND S.CodeFournis=P.CodeFournis and S.CodeFournis=F.CodeFournis and S.CodeFournis=M.CodeFournis AND P.CodeFournis=M.CodeFournis and M.RefLot=L.RefLot and M.RefLot=S.RefLot and L.NumeroDAO=D.NumeroDAO AND S.CodeLot=P.CodeLot and D.NumeroDAO=N.NumeroDAO and P.NumeroDAO=D.NumeroDAO and D.CodeProjet='" & ProjetEnCours & "' and L.NumeroDAO like'" & CmbDAO.Text & "%' and F.NomFournis like '" & EnleverApost(CmbFournis.Text) & "%' and M.NumeroMarche like '%" & TxtNumMarcheSearch.Text & "%'  GROUP by F.NomFournis,D.IntituleDAO"
            dtMarche.Rows.Clear()
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cptr += 1
                Dim drS = dtMarche.NewRow()

                drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
                drS(1) = rw(0).ToString
                drS(2) = rw(1).ToString
                drS(3) = IIf(Mid(rw(3).ToString, 1, 6) = "Lot N°", MettreApost(rw(2).ToString) & " (" & rw(3).ToString & ")", MettreApost(rw(3).ToString)).ToString
                drS(4) = rw(4).ToString
                drS(5) = AfficherMonnaie(rw(5).ToString.Replace(" ", ""))
                drS(6) = MettreApost(rw(6).ToString)
                drS(7) = rw(7).ToString
                drS(8) = rw(8).ToString
                Dim partDel() As String = rw(8).ToString.Split(" "c)
                Dim durr As Decimal = (CInt(partDel(0)) * CInt(IIf(partDel(1) = "Mois", 30, CInt(IIf(partDel(1) = "Semaines", 7, 1)))))
                drS(9) = (CDate(rw(7)).AddDays(durr)).ToShortDateString
                drS(10) = rw(9).ToString

                dtMarche.Rows.Add(drS)
            Next

            GridMarche.DataSource = dtMarche

            ViewMarche.Columns(0).Visible = False
            ViewMarche.Columns(1).Width = 120
            ViewMarche.Columns(2).Width = 100
            ViewMarche.Columns(3).Width = 300
            ViewMarche.Columns(4).Width = 80
            ViewMarche.Columns(5).Width = 120
            ViewMarche.Columns(6).Width = 120
            ViewMarche.Columns(7).Width = 100
            ViewMarche.Columns(8).Width = 80
            ViewMarche.Columns(9).Width = 100
            ViewMarche.Columns(10).Width = 100

            ViewMarche.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewMarche.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewMarche.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewMarche.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewMarche.Columns(7).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewMarche.Columns(8).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewMarche.Columns(9).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            ViewMarche.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

            ColorRowGrid(ViewMarche, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
        End If


    End Sub

    Private Sub ChkDAO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkDAO.CheckedChanged

        If (ChkDAO.Checked = True) Then
            CmbDAO.Enabled = True
        Else
            CmbDAO.Text = ""
            CmbDAO.Enabled = False
        End If

    End Sub

    Private Sub CmbDAO_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbDAO.SelectedValueChanged
        ChargerMarche()
    End Sub

    Private Sub ChkFournis_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkFournis.CheckedChanged

        If (ChkFournis.Checked = True) Then
            CmbFournis.Enabled = True
        Else
            CmbFournis.Text = ""
            CmbFournis.Enabled = False
        End If

    End Sub

    Private Sub CmbFournis_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbFournis.SelectedValueChanged
        ChargerMarche()
    End Sub

    Private Sub ChEtat_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChEtat.CheckedChanged

        If (ChEtat.Checked = True) Then
            CmbEtat.Enabled = True
        Else
            CmbEtat.Text = ""
            CmbEtat.Enabled = False
        End If

    End Sub

    Private Sub TxtNumMarcheSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNumMarcheSearch.TextChanged
        ChargerMarche()
    End Sub

    Private Sub CmbEtat_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbEtat.SelectedValueChanged
        ChargerMarche()
    End Sub

    Private Sub ConsulterLeDossierToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ConsulterLeDossierToolStripMenuItem.Click

        If (ViewMarche.RowCount > 0) Then
            DrX = ViewMarche.GetDataRow(ViewMarche.FocusedRowHandle)
            JugementOffres.EditerMarche(DrX(1).ToString, "Afficher")
        End If

    End Sub

    Private Sub ImprimerLeDossierToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImprimerLeDossierToolStripMenuItem.Click

        If (ViewMarche.RowCount > 0) Then
            DrX = ViewMarche.GetDataRow(ViewMarche.FocusedRowHandle)
            JugementOffres.EditerMarche(DrX(1).ToString, "Imprimer")
        End If

    End Sub
End Class