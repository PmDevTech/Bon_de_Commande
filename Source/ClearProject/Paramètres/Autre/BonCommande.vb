Imports System.Math
Imports MySql.Data.MySqlClient

Public Class BonCommande

    Dim tauxDollar As Decimal = 1
    Dim CfaGere As Boolean = True
    Dim dtboncommande = New DataTable

    Private Sub ChargerService()
        CmbService.Properties.Items.Clear()
        CmbService.ResetText()
        query = "select NomService from T_Service where CodeProjet='" & ProjetEnCours & "' order by NomService"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbService.Properties.Items.Add(MettreApost(rw(0).ToString))
        Next
    End Sub

    Private Sub Chargertiers()
        Cmbctfour.Properties.Items.Clear()
        Cmbctfour.ResetText()
        query = "select r.CPT_TIER, r.CODE_CPT, r.CODE_TCPT,c.NOM_CPT from T_COMP_RATTACH_TIERS r, T_COMP_COMPTE c where c.CODE_CPT=r.CODE_CPT AND r.code_sc like '401%'"
        Dim dt2 As DataTable = ExcecuteSelectQuery(query)
        For Each rw2 As DataRow In dt2.Rows
            Cmbctfour.Properties.Items.Add(rw2(1).ToString & "   " & MettreApost(rw2(3).ToString))
        Next
    End Sub

    Private Sub RemplirListeActivites()
        CmbActivite.Properties.Items.Clear()
        CmbActivite.ResetText()
        query = "select LibelleCourt,LibellePartition from T_Partition where CodeClassePartition='5' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            CmbActivite.Properties.Items.Add(rw(0).ToString & "-" & MettreApost(rw(1).ToString))
        Next
    End Sub

    Private Sub BonCommande_Load(sender As System.Object, e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ChargerService()
        Chargertiers()
        RemplirListeActivites()
        'codeauto(TextEdit1)
        Initialiser()
    End Sub

    Private Sub Initialiser()
        CmbActivite.ResetText()
        Cmbctfour.ResetText()
        CmbService.ResetText()
        Dateboncmde.Text = ""
        Txtboncmde.Text = ""
        TxtMarche.Text = ""
        TxtDesignation.Text = ""
        TxtQte.Text = ""
        TxtPu.Text = ""
        TxtNewMont.Text = ""
        TxtMontLettre.Text = ""

        'Dim dtlstboncmde As DataTable = ListBonCmde.DataSource
        'dtlstboncmde.Rows.Clear()
    End Sub

    Private Sub TxtNewMont_TextChanged(sender As System.Object, e As System.EventArgs) Handles TxtNewMont.TextChanged
        If (CfaGere = True) Then
            If (TxtNewMont.Text <> "") Then
                VerifSaisieMontant(TxtNewMont)
                Dim montConvert As Decimal = Math.Round(CDec(TxtNewMont.Text.Replace(" ", "")) / tauxDollar, 2)
                Dim DeviseLettre As String = " francs"
                TxtMontLettre.Text = (TxtNewMont.Text.Replace(" ", "")).Replace(" zero", "") & DeviseLettre
            End If
        End If
    End Sub

    Sub codeauto(ByVal montext As DevExpress.XtraEditors.TextEdit)
        Try
            Dim nbre As Decimal = 0
            query = "select count(CodeBon) from t_boncommande where CodeProjet='" & ProjetEnCours & "'"
            nbre = Val(ExecuteScallar(query))

            If nbre = 0 Then
                montext.Text = "1"
            Else
                query = "select count(CodeBon) from t_boncommande where CodeProjet='" & ProjetEnCours & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw In dt.Rows

                    Dim strNombre As String
                    Dim SpaceIndex As Decimal
                    Dim caractere As String
                    Dim intNombre, nbr As Decimal

                    strNombre = ""
                    For SpaceIndex = 1 To Len(rw(0).ToString)
                        caractere = Mid$(rw(0).ToString, SpaceIndex, 1)
                        If caractere >= "0" And caractere <= "9" Then
                            strNombre = strNombre + caractere
                        End If
                    Next
                    intNombre = CInt(strNombre)
                    nbr = intNombre + Int(1)
                    montext.Text = nbr.ToString
                Next

            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub

    Private Sub GridBonCommande()

        dtboncommande.Columns.Clear()
        dtboncommande.Columns.Add("Choix", Type.GetType("System.Boolean"))
        dtboncommande.Columns.Add("Designation", Type.GetType("System.String"))
        dtboncommande.Columns.Add("Quantité", Type.GetType("System.String"))
        dtboncommande.Columns.Add("Prix Unitaire", Type.GetType("System.String"))
        dtboncommande.Columns.Add("Prix Total", Type.GetType("System.String"))
        Dim cpt As Decimal = 0

        Dim drS = dtboncommande.NewRow()
        cpt = cpt + 1
        drS(0) = TabTrue(cpt - 1)
        drS(1) = EnleverApost(TxtDesignation.Text)
        drS(2) = AfficherMonnaie(CDbl(TxtQte.Text))
        drS(3) = AfficherMonnaie(CDbl(TxtPu.Text))
        drS(4) = AfficherMonnaie(CDbl(TxtNewMont.Text))
        dtboncommande.Rows.Add(drS)
        ListBonCmde.DataSource = dtboncommande
    End Sub

    Private Sub TxtQte_TextChanged(sender As Object, e As EventArgs) Handles TxtQte.TextChanged, TxtPu.TextChanged
        If TxtQte.Text <> "" And TxtPu.Text <> "" Then
            Dim qte As Double = 0
            Dim pu As Double = 0
            qte = IIf(TxtQte.Text = "", 0, TxtQte.Text)
            pu = IIf(TxtPu.Text = "", 0, TxtPu.Text)
            TxtNewMont.Text = CDbl(qte.ToString) * CDbl(pu.ToString)
        End If
    End Sub

    Private Sub TxtPu_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPu.KeyPress
        Select Case e.KeyChar
            Case ControlChars.CrLf
                GridBonCommande()
            Case Else
        End Select
    End Sub

    Private Sub BtAnnuler_Click(sender As Object, e As EventArgs) Handles BtAnnuler.Click
        Initialiser()
    End Sub
End Class