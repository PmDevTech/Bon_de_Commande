Imports System.Math
Imports MySql.Data.MySqlClient

Public Class RattrapReglement

    Dim numFacture As String = ""
    Dim tauxDollar As Decimal = 1
    Dim CfaGere As Boolean = True

    Private Sub RattrapReglement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        CmbMarcheItems()
        DollarTx()
        GridHistoriqueRows()

    End Sub

    Private Sub GridHistoriqueRows()

        query = "select DateRglt,IdentFacture,Montant,ModeReglement from T_Reglement where CodeProjet='" & ProjetEnCours & "'"
        GridHistorique.Rows.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Dim n As Decimal = GridHistorique.Rows.Add()
            GridHistorique.Rows.Item(n).Cells(0).Value = rw(0).ToString
            GridHistorique.Rows.Item(n).Cells(1).Value = rw(1).ToString
            GridHistorique.Rows.Item(n).Cells(2).Value = AfficherMonnaie(rw(2).ToString)
            GridHistorique.Rows.Item(n).Cells(3).Value = rw(3).ToString
        Next

    End Sub

    Private Sub DollarTx()

        query = "select TauxDevise from T_Devise where AbregeDevise='US$'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            tauxDollar = CDec(rw(0))
        Next

    End Sub

    Private Sub CmbMarcheItems()

        query = "select RefMarche from T_BonCommande where CodeProjet='" & ProjetEnCours & "' group by RefMarche"
        CmbMarche.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbMarche.Items.Add(rw(0))
        Next

    End Sub

    Private Sub CmbMarche_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbMarche.SelectedIndexChanged

        'Libelle marché
        query = "select DescriptionMarche from T_Marche where RefMarche='" & CmbMarche.Text & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            TxtMarche.Text = MettreApost(rw(0).ToString)
        Next

        'Les factures du marché
        query = "select B.RefLot,B.CodeFournis,F.IdentFacture,F.MontantFacture from T_BonCommande as B,T_Facture as F where B.RefBon=F.RefBon and B.RefMarche='" & CmbMarche.Text & "' and B.CodeProjet='" & ProjetEnCours & "'"
        GridFactures.Rows.Clear()
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        For Each rw1 As DataRow In dt1.Rows

            Dim codeLot As String = ""

            query = "select CodeLot from T_LotDAO where RefLot='" & rw1(0) & "'"
            Dim dt2 As DataTable = ExcecuteSelectQuery(query)
            For Each rw2 As DataRow In dt2.Rows
                codeLot = rw2(0).ToString
            Next

            Dim Fourniss As String = ""
            query = "select AbregeNomFournis,NomFournis from T_Fournisseur where CodeFournis='" & rw1(1) & "'"
            Dim dt3 As DataTable = ExcecuteSelectQuery(query)
            For Each rw3 As DataRow In dt3.Rows
                Fourniss = "(" & MettreApost(rw3(0).ToString) & ") " & MettreApost(rw3(1).ToString)
            Next

            Dim n As Decimal = GridFactures.Rows.Add()
            GridFactures.Rows.Item(n).Cells(0).Value = codeLot
            GridFactures.Rows.Item(n).Cells(1).Value = rw1(2)
            GridFactures.Rows.Item(n).Cells(2).Value = Fourniss
            GridFactures.Rows.Item(n).Cells(3).Value = AfficherMonnaie(rw1(3).ToString)

        Next


    End Sub

    Private Sub GridFactures_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridFactures.CellClick

    End Sub

    Private Sub GridFactures_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridFactures.CellDoubleClick

        Dim nLigNe As Integer = GridFactures.CurrentRow.Index
        numFacture = GridFactures.Rows.Item(nLigne).Cells(1).Value.ToString
        TxtTotFacture.Text = GridFactures.Rows.Item(nLigne).Cells(3).Value.ToString
        Dim partFournis() As String = GridFactures.Rows.Item(nLigne).Cells(2).Value.ToString.Split("("c)
        If (partFournis.Length > 1) Then
            Dim partF2() As String = partFournis(1).Split(")"c)
            If (partF2(0) <> "") Then
                TxtFournis.Text = partF2(0)
            Else
                TxtFournis.Text = Mid(GridFactures.Rows.Item(nLigne).Cells(2).Value.ToString, 1, 4)
            End If
        End If

        'Recap règlement de la facture
        Dim montPaye As Decimal = 0
        Dim dernMont As Decimal = 0
        Dim dernDate As String = "__/__/____"

        query = "select DateRglt,Montant from T_Reglement where IdentFacture='" & numFacture & "' and CodeProjet='" & ProjetEnCours & "' order by NumRglt"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            dernMont = CDec(rw(1))
            dernDate = rw(0)
            montPaye = montPaye + CDec(rw(1))
        Next

        TxtMontPaye.Text = AfficherMonnaie(montPaye.ToString)
        TxtMontReste.Text = AfficherMonnaie(CDec(TxtTotFacture.Text.Replace(" ", "")) - montPaye)
        TxtDernDate.Text = dernDate
        TxtDernMont.Text = AfficherMonnaie(dernMont.ToString)

        Panel1.Enabled = True
        TxtNewMont.Focus()


    End Sub

    Private Sub TxtNewMont_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtNewMont.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            BtEnregistrer_Click(Me, e)
        End If
    End Sub

    Private Sub TxtNewMont_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtNewMont.KeyPress

        CfaGere = True

    End Sub

    Private Sub TxtNewMont_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNewMont.TextChanged



        If (CfaGere = True) Then
            If (TxtNewMont.Text <> "") Then
                VerifSaisieMontant(TxtNewMont)
                Dim montConvert As Decimal = Math.Round(CDec(TxtNewMont.Text.Replace(" ", "")) / tauxDollar, 2)
                ''If (Truncate(montConvert) = montConvert) Then
                ''    montConvert = Truncate(montConvert)
                ''End If
                TxtNewMontDollar.Text = AfficherMonnaie(montConvert.ToString)

                Dim DeviseLettre As String = " francs"
                TxtMontLettre.Text = MontantLettre(TxtNewMont.Text.Replace(" ", "")).Replace(" zero", "") & DeviseLettre

            Else
                TxtNewMontDollar.Text = ""
                TxtMontLettre.Text = ""
            End If
        End If

        If (TxtNewMont.Text <> "") Then
            If (CDec(TxtNewMont.Text.Replace(" ", "")) > CDec(TxtMontReste.Text.Replace(" ", ""))) Then
                TxtNewMont.BackColor = Color.Red
                TxtNewMontDollar.BackColor = Color.Red
                TxtMontLettre.Text = "Le montant saisie est supérieur au montant restant à payer!"
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
            Else
                TxtNewMont.BackColor = Color.White
                TxtNewMontDollar.BackColor = Color.White
            End If
        End If
    End Sub

    Private Sub TxtMontLettre_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtMontLettre.TextChanged
        If (TxtMontLettre.Text = "Million francs" Or TxtMontLettre.Text = "Milliard francs") Then
            TxtMontLettre.Text = "un " & TxtMontLettre.Text.Replace(" francs", "") & " de francs"
        End If
        If (TxtMontLettre.Text <> "") Then
            TxtMontLettre.Text = Mid(TxtMontLettre.Text, 1, 1).ToUpper & Mid(TxtMontLettre.Text, 2)
        End If
    End Sub

    Private Sub TxtNewMontDollar_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtNewMontDollar.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            BtEnregistrer_Click(Me, e)
        End If
    End Sub

    Private Sub TxtNewMontDollar_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtNewMontDollar.KeyPress
        CfaGere = False

    End Sub

    Private Sub TxtNewMontDollar_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNewMontDollar.TextChanged

        If (CfaGere = False) Then
            If (TxtNewMontDollar.Text <> "") Then

                'TxtNewMontDollar.Text = Math.Round(CDec(TxtNewMontDollar.Text.Replace(" ", "")), 2)

                VerifSaisieMontant(TxtNewMontDollar)

                Dim montPourConvert As String = TxtNewMontDollar.Text.Replace(" ", "")
                Dim PartMont() As String = TxtNewMontDollar.Text.Replace(" ", "").Split(","c)
                If (PartMont.Length > 1) Then
                    If (PartMont(1) = "") Then
                        montPourConvert = Mid(montPourConvert, 1, Len(montPourConvert) - 1)
                    ElseIf (PartMont(1).Length > 2) Then
                        montPourConvert = PartMont(0) & "," & Mid(PartMont(1), 1, 2)
                        TxtNewMontDollar.Text = montPourConvert
                    End If
                End If

                Dim montConvert As Decimal = Math.Round(CDec(montPourConvert) * tauxDollar, 0)
                'montConvert = Ceiling(montConvert)

                TxtNewMont.Text = AfficherMonnaie(montConvert.ToString)

                Dim DeviseLettre As String = " dollars"
                Dim PartDollar() As String = TxtNewMontDollar.Text.Split(","c)
                If (PartDollar.Length = 1 And PartDollar(0) <> "") Then
                    TxtMontLettre.Text = MontantLettre(TxtNewMontDollar.Text.Replace(" ", "")).Replace(" zero", "") & DeviseLettre
                ElseIf (PartDollar.Length > 1 And PartDollar(1) <> "") Then
                    TxtMontLettre.Text = MontantLettre(PartDollar(0).Replace(" ", "")).Replace(" zero", "") & DeviseLettre & " et " & MontantLettre(PartDollar(1).Replace(" ", "")).Replace(" zero", "") & " centimes"
                ElseIf (PartDollar.Length > 1 And PartDollar(1) = "") Then
                    TxtMontLettre.Text = MontantLettre(PartDollar(0).Replace(" ", "")).Replace(" zero", "") & DeviseLettre
                End If
                'End If

            Else
                TxtNewMont.Text = ""
                TxtMontLettre.Text = ""
            End If
        End If


    End Sub

    Private Sub BtAnnuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAnnuler.Click
        RaserTT()
    End Sub
    Private Sub RaserTT()
        TxtFournis.Text = ""
        TxtTotFacture.Text = ""
        TxtMontPaye.Text = ""
        TxtMontReste.Text = ""
        TxtDernDate.Text = ""
        TxtDernMont.Text = ""
        TxtNewMont.Text = ""
        TxtNewMontDollar.Text = ""
        TxtMontLettre.Text = ""
        Panel1.Enabled = False
    End Sub

    Private Sub BtEnregistrer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click

        If (TxtNewMont.Text <> "") Then
            If (TxtNewMont.BackColor <> Color.Red) Then
                Dim DatSet = New DataSet
                query = "select * from T_Reglement"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "T_Reglement")
                Dim DatTable = DatSet.Tables("T_Reglement")
                Dim DatRow = DatSet.Tables("T_Reglement").NewRow()

                DatRow("IdentFacture") = numFacture
                DatRow("DateRglt") = Now.ToShortDateString
                DatRow("Montant") = TxtNewMont.Text.Replace(" ", "")
                DatRow("ModeReglement") = "ESPECE"
                DatRow("CodeProjet") = ProjetEnCours
                DatRow("DateSaisie") = Now.ToShortDateString
                DatRow("Operateur") = CodeUtilisateur

                DatSet.Tables("T_Reglement").Rows.Add(DatRow)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "T_Reglement")
                DatSet.Clear()
                BDQUIT(sqlconn)

                RaserTT()
                GridHistoriqueRows()
            Else
                MsgBox("Saisie incorrecte!", MsgBoxStyle.Exclamation)
            End If

        Else
            MsgBox("Saisie obligatoire!", MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Sub RattrapReglement_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

End Class