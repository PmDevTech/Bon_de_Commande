Imports MySql.Data.MySqlClient

Public Class RattrapFournisseur

    Private Sub RattrapFournisseur_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        CmbPaysItems()
        CmbDAOItems()
        GridResumeRows()
        DateSignature.Value = Now.ToShortDateString
        DateFinExecution.Value = Now.ToShortDateString

    End Sub
    Private Sub CmbPaysItems()

        query = "select LibelleZone from T_ZoneGeo where CodeZoneMere='0' order by LibelleZone"
        CmbPays.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbPays.Items.Add(MettreApost(rw(0)))
        Next

    End Sub

    Private Sub CmbDAOItems()

        query = "select NumeroDAO from T_DAO where CodeProjet='" & ProjetEnCours & "' order by NumeroDAO"
        CmbDAO.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbDAO.Items.Add(rw(0))
        Next

    End Sub

    Private Sub CmbPays_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbPays.SelectedIndexChanged

        query = "select IndicZone from T_ZoneGeo where LibelleZone='" & EnleverApost(CmbPays.Text) & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            TxtIndic1.Text = rw(0)
            TxtIndic2.Text = rw(0)
        Next

    End Sub

    Private Sub CmbDAO_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbDAO.SelectedIndexChanged

        query = "select TypeMarche,MethodePDM,DelaiExecution from T_DAO where CodeProjet='" & ProjetEnCours & "' and NumeroDAO='" & CmbDAO.Text & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            TxtType.Text = rw(0)
            TxtMethode.Text = rw(1)
            TxtDuree.Text = rw(2)
        Next

        Dim LesCodeLot(10) As String
        Dim NbCodeLot As Decimal = 0
        query = "select CodeLot from T_LotDAO where NumeroDAO='" & CmbDAO.Text & "' and CodeLot<>'' order by CodeLot"
        Dim dt2 As DataTable = ExcecuteSelectQuery(query)
        For Each rw2 As DataRow In dt2.Rows
            LesCodeLot(NbCodeLot) = rw2(0)
            NbCodeLot = NbCodeLot + 1
        Next

        'Retrait des codes non attribués et déjà utilisés
        Dim NewCpt As Decimal = 0
        query = "select L.CodeLot from T_LotDAO as L,T_BonCommande as B where L.NumeroDAO='" & CmbDAO.Text & "' and L.RefLot=B.RefLot and B.CodeProjet='" & ProjetEnCours & "' order by CodeLot"
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        For Each rw1 As DataRow In dt1.Rows
            For i As Integer = 0 To NbCodeLot - 1
                If (rw1(0).ToString = LesCodeLot(i)) Then
                    NewCpt = NewCpt + 1
                    For k As Integer = i To NbCodeLot - 2
                        LesCodeLot(k) = LesCodeLot(k + 1)
                    Next

                End If
            Next
        Next

        'On rempli le cmbo lot
        CmbLot.Items.Clear()
        For j As Integer = 0 To (NbCodeLot - NewCpt) - 1
            CmbLot.Items.Add(LesCodeLot(j))
        Next

    End Sub

    Private Sub CmbLot_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbLot.SelectedIndexChanged

        query = "select RefLot from T_LotDAO where NumeroDAO='" & CmbDAO.Text & "' and CodeLot='" & CmbLot.Text & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            RefLotCache.Text = rw(0)
        Next

        If (CmbLot.Text <> "") Then
            GbContrat.Enabled = True
        Else
            GbContrat.Enabled = False
        End If
    End Sub

    Private Sub TxtMontant_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtMontant.TextChanged
        VerifSaisieMontant(TxtMontant)
    End Sub

    Private Sub BtEnregistrer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click

        If (TxtCode.Text <> "" And TxtNom.Text <> "" And CmbLot.Text <> "" And TxtMontant.Text <> "") Then

            'Enregistrement dans T_Fournisseur
            Dim DatSet = New DataSet
            query = "select * from T_Fournisseur"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_Fournisseur")
            Dim DatTable = DatSet.Tables("T_Fournisseur")
            Dim DatRow = DatSet.Tables("T_Fournisseur").NewRow()

            DatRow("NomFournis") = EnleverApost(TxtNom.Text)
            DatRow("AbregeNomFournis") = EnleverApost(TxtCode.Text)
            DatRow("AdresseCompleteFournis") = EnleverApost(TxtAdresse.Text)
            DatRow("TelFournis") = TxtTel.Text
            DatRow("FaxFournis") = TxtFax.Text
            DatRow("MailFournis") = TxtMail.Text
            DatRow("DateSaisie") = Now.ToShortDateString & " " & Now.ToLongTimeString
            DatRow("DateModif") = Now.ToShortDateString & " " & Now.ToLongTimeString
            DatRow("NumeroDAO") = CmbDAO.Text
            DatRow("DateDepotDAO") = Now.ToShortDateString & " " & Now.ToLongTimeString
            DatRow("CodeProjet") = ProjetEnCours
            DatRow("PaysFournis") = EnleverApost(CmbPays.Text)

            DatSet.Tables("T_Fournisseur").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_Fournisseur")
            DatSet.Clear()

            'Dernier index fournisseur
            Dim DernIndex As Decimal = 0

            query = "select CodeFournis from T_Fournisseur where CodeProjet='" & ProjetEnCours & "' and NumeroDAO='" & CmbDAO.Text & "' and NomFournis='" & EnleverApost(TxtNom.Text) & "' and AbregeNomFournis='" & EnleverApost(TxtCode.Text) & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                DernIndex = CInt(rw(0))
            Next

            'RefMarche 
            Dim codeMarche As Decimal = 0
            query = "select RefMarche from T_Marche where CodeProjet='" & ProjetEnCours & "' and NumeroDAO='" & CmbDAO.Text & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw1 As DataRow In dt1.Rows
                codeMarche = CInt(rw1(0))
            Next

            'Enregistrement dans T_bonCommande
            DatSet = New DataSet
            query = "select * from T_BonCommande"
            Cmd = New MySqlCommand(query, sqlconn)
            DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_BonCommande")
            DatTable = DatSet.Tables("T_BonCommande")
            DatRow = DatSet.Tables("T_BonCommande").NewRow()

            DatRow("RefMarche") = codeMarche
            DatRow("RefLot") = RefLotCache.Text
            DatRow("DateCommande") = DateSignature.Value.ToShortDateString
            DatRow("DateLivraison") = DateFinExecution.Value.ToShortDateString
            DatRow("CodeFournis") = DernIndex
            DatRow("MontantContrat") = TxtMontant.Text.Replace(" ", "")
            DatRow("CodeProjet") = ProjetEnCours

            DatSet.Tables("T_BonCommande").Rows.Add(DatRow)
            CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_BonCommande")
            DatSet.Clear()

            'dernier bon
            Dim DernBon As Decimal = 0
            query = "select RefBon from T_BonCommande where RefMarche='" & codeMarche & "' and RefLot='" & RefLotCache.Text & "' and CodeFournis='" & DernIndex & "'"
            Dim dt2 As DataTable = ExcecuteSelectQuery(query)
            For Each rw2 As DataRow In dt2.Rows
                DernBon = CInt(rw2(0))
            Next

            'Nbre facture dans la periode
            Dim Num1 As Decimal = 0
            Dim Mois3 As String = Mid(Now.ToString("MMMM"), 1, 3).ToUpper
            If (Mid(Now.ToString("MMMM"), 1).ToUpper = "JUILLET") Then Mois3 = "JUT"
            Dim CodeFin As String = "_" & codeMarche & "/" & Mois3 & "_" & Now.ToString("yyyy")
            query = "select * from T_Facture where IdentFacture like '%" & CodeFin & "'"
            Dim dt3 As DataTable = ExcecuteSelectQuery(query)
            For Each rw3 As DataRow In dt3.Rows
                Num1 = Num1 + 1
            Next

            If (Num1 < 10) Then
                CodeFin = "0" & (Num1 + 1).ToString & CodeFin
            Else
                CodeFin = (Num1 + 1).ToString & CodeFin
            End If


            'Enregistrement facture
            DatSet = New DataSet
            query = "select * from T_Facture"
            Cmd = New MySqlCommand(query, sqlconn)
            DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_Facture")
            DatTable = DatSet.Tables("T_Facture")
            DatRow = DatSet.Tables("T_Facture").NewRow()

            DatRow("IdentFacture") = CodeFin
            DatRow("NumFacture") = "XXXXXX"
            DatRow("DateFacture") = Now.ToShortDateString
            DatRow("LibelleFacture") = "Rattrapage facture"
            DatRow("MontantFacture") = TxtMontant.Text.Replace(" ", "")
            DatRow("CodeProjet") = ProjetEnCours
            DatRow("RefBon") = DernBon

            DatSet.Tables("T_Facture").Rows.Add(DatRow)
            CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_Facture")
            DatSet.Clear()

            BDQUIT(sqlconn)


            GridResumeRows()
            RaseNet()
        End If

    End Sub

    Private Sub GridResumeRows()

        GridResume.Rows.Clear()
        query = "select RefMarche,RefLot,DateCommande,DateLivraison,CodeFournis,MontantContrat from T_BonCommande where CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows

            'Recherche du CodeLot
            Dim codeLot As String = ""
            Dim numDao As String = ""

            query = "select CodeLot,NumeroDAO from T_LotDAO where RefLot='" & rw(1) & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw1 As DataRow In dt1.Rows
                codeLot = rw1(0)
                numDao = rw1(1)
            Next

            'recherche du nom fournisseur
            Dim nomFourniss As String = ""
            query = "select NomFournis,AbregeNomFournis from T_Fournisseur where CodeFournis='" & rw(4) & "'"
            Dim dt2 As DataTable = ExcecuteSelectQuery(query)
            For Each rw2 As DataRow In dt2.Rows
                nomFourniss = MettreApost("(" & rw2(1).ToString & ") " & rw2(0).ToString)
            Next

            Dim n As Decimal = GridResume.Rows.Add()
            GridResume.Rows.Item(n).Cells(0).Value = numDao
            GridResume.Rows.Item(n).Cells(1).Value = codeLot
            GridResume.Rows.Item(n).Cells(2).Value = nomFourniss
            GridResume.Rows.Item(n).Cells(3).Value = AfficherMonnaie(rw(5).ToString)
            GridResume.Rows.Item(n).Cells(4).Value = rw(2)
            GridResume.Rows.Item(n).Cells(5).Value = rw(3)

        Next

    End Sub

    Private Sub RaseNet()

        TxtCode.Text = ""
        TxtNom.Text = ""
        TxtAdresse.Text = ""
        CmbPays.Text = ""
        TxtIndic1.Text = ""
        TxtTel.Text = ""
        TxtIndic2.Text = ""
        TxtFax.Text = ""
        TxtMail.Text = ""
        CmbDAO.Text = ""
        TxtMethode.Text = ""
        TxtType.Text = ""
        CmbLot.Text = ""
        TxtDuree.Text = ""
        TxtMontant.Text = ""

        TxtCode.Focus()
    End Sub

    Private Sub DateSignature_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateSignature.ValueChanged

        If (GbContrat.Enabled = True) Then
            Dim DatDeb As Date = DateSignature.Value.ToShortDateString
            Dim PartDuree() As String = TxtDuree.Text.Split(" "c)
            If (PartDuree.Length > 1) Then
                Dim nDuree As Decimal = CInt(PartDuree(0))

                If (PartDuree(1) = "Jours") Then DateFinExecution.Value = DatDeb.AddDays(nDuree).ToShortDateString
                If (PartDuree(1) = "Semaines") Then DateFinExecution.Value = DatDeb.AddDays(nDuree * 7).ToShortDateString
                If (PartDuree(1) = "Mois") Then DateFinExecution.Value = DatDeb.AddMonths(nDuree).ToShortDateString


            End If


        End If


    End Sub

    Private Sub RattrapFournisseur_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub BtRetour1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRetour1.Click
        RaseNet()
    End Sub
End Class