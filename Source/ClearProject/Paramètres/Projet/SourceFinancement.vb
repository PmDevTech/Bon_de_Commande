Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Data.DataSet
Imports System.IO
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.Math

Public Class SourceFinancement
    Dim dtListConvention = New DataTable
    Dim drx As DataRow

    Public PourAjout, PourSupprim, PourModif As Boolean
    Public OkModifS As Boolean

    Private Sub SourceFinancement_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        BtRetour_Click_1(Me, e)
    End Sub

    Private Sub SourceFinancement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        OkModifS = False
        PourAjout = False
        PourModif = False
        PourSupprim = False
        ChargerSource()

        DTDateSignature.Text = Now.ToShortDateString
        DTEntreeVigueur.Text = Now.ToShortDateString
        DTCloture.Text = Now.ToShortDateString



        dtListConvention.columns.clear()
        dtListConvention.columns.add("Bailleur", Type.GetType("System.String"))
        dtListConvention.columns.add("Type", Type.GetType("System.String"))
        dtListConvention.columns.add("Numéro", Type.GetType("System.String"))
        dtListConvention.columns.add("Montant", Type.GetType("System.String"))
        dtListConvention.columns.add("Signature", Type.GetType("System.String"))
        dtListConvention.columns.add("Ouverture", Type.GetType("System.String"))
        dtListConvention.columns.add("Clôture", Type.GetType("System.String"))
        dtListConvention.columns.add("Etat actuel", Type.GetType("System.String"))

        'ChargerGridConv()
        ChargerConvention()
        ViderZoneTexte()


    End Sub

    Private Sub ViderZoneTexte()
        BtAjouter.Enabled = True
        BtModifier.Enabled = False
        BtSupprimer.Enabled = False
        BtRetour.Enabled = False
        BtEnregistrer.Enabled = False
        CmbSource.Enabled = False
        CmbSource.Text = ""
        TxtSource.Enabled = False
        TxtSource.Text = ""
        CmbType.Enabled = False
        CmbType.Text = ""
        TxtNumConvention.Enabled = False
        TxtNumConvention.Text = ""
        TxtPieceJointe.Enabled = False
        TxtPieceJointe.Text = ""
        BtRecherche.Enabled = False
        TxtCFA.Enabled = False
        'TxtCFA.Text = ""
        TxtEuro.Enabled = False
        'TxtEuro.Text = ""
        TxtUS.Enabled = False
        'TxtUS.Text = ""
        TxtLIVRE.Enabled = False
        'TxtLIVRE.Text = ""
        DTDateSignature.Enabled = False
        DTEntreeVigueur.Enabled = False
        DTCloture.Enabled = False
    End Sub


    Private Sub ChargerConvention()
        Dim DateDeb, DateFin, DateJr As Date
        Dim cptr As Integer = 0
        query = "select c.TypeConvention, c.CodeConvention, c.MontantConvention, c.DateSignature, c.EntreeEnVigueur, c.DateCloture, b.InitialeBailleur from T_Convention c, T_Bailleur b where c.CodeBailleur=b.CodeBailleur and b.CodeProjet='" & ProjetEnCours & "'"

        Dim dt = ExcecuteSelectQuery(query)
        dtListConvention.Rows.Clear()
        For Each rw As DataRow In dt.Rows
            DateDeb = CDate(rw("EntreeEnVigueur"))
            DateFin = CDate(rw("DateCloture"))
            DateJr = Now.ToShortDateString
            Dim EtatActu As String = "En exécution"
            cptr += 1

            Dim drS = dtListConvention.NewRow()

            drS("Bailleur") = rw("InitialeBailleur").ToString
            drS("Type") = rw("TypeConvention").ToString
            drS("Numéro") = MettreApost(rw("CodeConvention").ToString)
            drS("Montant") = AfficherMonnaie(rw("MontantConvention").ToString)
            drS("Signature") = CDate(rw("DateSignature")).ToString("yyyy/MM/dd")
            drS("Ouverture") = CDate(rw("EntreeEnVigueur")).ToString("yyyy/MM/dd")
            drS("Clôture") = CDate(rw("DateCloture")).ToString("yyyy/MM/dd")

            If (DateTime.Compare(DateJr, DateDeb) < 0) Then
                EtatActu = "En attente"
            ElseIf (DateTime.Compare(DateFin, DateJr) < 0) Then
                EtatActu = "Délai passé"
            End If

            drS("Etat actuel") = EtatActu


            dtListConvention.Rows.Add(drS)
        Next

        GridConvention1.DataSource = dtListConvention

        TextEdit1.Enabled = False
        TextEdit2.Enabled = False
        TextEdit3.Enabled = False
        TextEdit4.Enabled = False

        ViewConvention1.Columns("Bailleur").Width = 100
        ViewConvention1.Columns("Type").Width = 100
        ViewConvention1.Columns("Numéro").Width = 100
        ViewConvention1.Columns("Montant").Width = 125
        ViewConvention1.Columns("Signature").Width = 75
        ViewConvention1.Columns("Ouverture").Width = 75
        ViewConvention1.Columns("Clôture").Width = 75
        ViewConvention1.Columns("Etat actuel").Width = 100


        'ViewDecoup.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        ViewConvention1.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

        'ColorRowGrid(ViewConvention1, "[Bailleur]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub
    Private Sub ChargerSource()
        query = "select InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 = ExcecuteSelectQuery(query)
        CmbSource.properties.Items.Clear()
        For Each rw In dt0.Rows
            CmbSource.Properties.Items.Add(rw("InitialeBailleur"))
        Next
    End Sub
    'Private Sub BtAjouter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    CmbSource.Enabled = True
    '    BtRetour.Enabled = True
    'End Sub
    'Private Sub BtModifier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim sqlconn As New MySqlConnection
    '        BDOPEN(sqlconn)
    '        Dim DatSet = New DataSet
    '        query = "select * from T_Convention where CodeConvention='" & TxtNumConvention.Text & "'"

    '        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
    '        Dim DatAdapt = New MySqlDataAdapter(Cmd)
    '        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
    '        DatAdapt.Fill(DatSet, "T_Convention")

    '        DatSet.Tables!T_Convention.Rows(0)!TypeConvention = CmbType.Text
    '        DatSet.Tables!T_Convention.Rows(0)!MontantConvention = TxtCFA.Text.Replace(" ", "")
    '        DatSet.Tables!T_Convention.Rows(0)!DateSignature = DTDateSignature.Text.ToString
    '        DatSet.Tables!T_Convention.Rows(0)!EntreeEnVigueur = DTEntreeVigueur.Text.ToString
    '        DatSet.Tables!T_Convention.Rows(0)!DateCloture = DTCloture.Text.ToString

    '        If (TxtPieceJointe.Text <> "") Then
    '            Dim NomFichier As String = line & "\Conventions\" & CmbSource.Text & "_" & CmbType.Text & TxtNumConvention.Text.Replace("/", "&") & "\"
    '            If (Directory.Exists(NomFichier) = False) Then
    '                Directory.CreateDirectory(NomFichier)
    '                File.Copy(TxtChemin.Text, NomFichier, True)
    '            Else
    '            End If
    '            NomFichier = NomFichier & "\" & TxtPieceJointe.Text
    '            DatSet.Tables!T_Convention.Rows(0)!PieceConvention = EnleverApost(NomFichier)
    '            DatSet.Tables!T_Convention.Rows(0)!NomPiece = EnleverApost(TxtPieceJointe.Text)
    '        End If

    '        DatAdapt.Update(DatSet, "T_Convention")
    '        DatSet.Clear()
    '        'ChargerGridConv()
    '        ChargerConvention()

    '        MsgBox("Modification terminée avec succès.", MsgBoxStyle.Information)
    '        BtRetour_Click(Me, e)
    '        BDQUIT(sqlconn)

    '    Catch ex As Exception
    '        MsgBox(ex.ToString, MsgBoxStyle.Information)
    '    End Try

    'End Sub
    'Private Sub BtSupprimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        query = "DELETE from T_Convention where CodeConvention='" & TxtNumConvention.Text & "'"
    '        ExecuteScallar(query)

    '        Dim NomFichier As String = line & "\Conventions\" & CmbSource.Text & "_" & CmbType.Text & TxtNumConvention.Text.Replace("/", "&")
    '        If (Directory.Exists(NomFichier) = True) Then
    '            Directory.Delete(NomFichier)
    '        End If

    '        'ChargerGridConv()
    '        query = "CALL `DeleteTampColConvention`();"
    '        'ExecuteNonQuery(query)
    '        query = "CALL `CreateTampColConvention`();"
    '        'ExecuteNonQuery(query)

    '        ChargerConvention()

    '        BtRetour_Click(Me, e)
    '        MsgBox("Suppression terminée avec succès.", MsgBoxStyle.Information)
    '    Catch ex As Exception
    '        MsgBox(ex.ToString, MsgBoxStyle.Information)
    '    End Try
    'End Sub
    'Private Sub BtRetour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    BtAjouter.Enabled = True
    '    BtModifier.Enabled = True
    '    BtSupprimer.Enabled = True

    '    BtEnregistrer.Enabled = False
    '    BtRetour.Enabled = False

    '    PourAjout = False
    '    PourModif = False
    '    PourSupprim = False

    '    CmbSource.Text = ""
    '    CmbSource.Enabled = False
    '    TxtSource.Text = ""
    '    CmbType.Text = ""
    '    CmbType.Enabled = False
    '    TxtNumConvention.Text = ""
    '    TxtNumConvention.Enabled = False
    '    BtRecherche.Enabled = False
    '    TxtCFA.Text = ""
    '    TxtCFA.Enabled = False
    '    TxtEuro.Text = ""
    '    TxtEuro.Enabled = False
    '    TxtUS.Text = ""
    '    TxtUS.Enabled = False
    '    TxtLIVRE.Text = ""
    '    TxtLIVRE.Enabled = False
    '    DTDateSignature.Text = Now.ToShortDateString
    '    DTDateSignature.Enabled = False
    '    DTEntreeVigueur.Text = Now.ToShortDateString
    '    DTEntreeVigueur.Enabled = False
    '    DTCloture.Text = Now.ToShortDateString
    '    DTCloture.Enabled = False
    '    TxtPieceJointe.Text = ""

    '    'GridConvention.Rows.Clear()
    '    ChargerSource()
    '    'ChargerGridConv()

    'End Sub
    'Private Sub CmbSource_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If (CmbSource.Text <> "") Then
    '            query = "select NomBailleur,CodeBailleur from T_Bailleur where InitialeBailleur='" & CmbSource.Text & "' and CodeProjet='" + ProjetEnCours + "'"
    '            Dim dt0 = ExcecuteSelectQuery(query)
    '            For Each rw In dt0.Rows
    '                TxtSource.Text = MettreApost(rw(0))
    '                TxtCodeSource.Text = rw(1)
    '            Next
    '            CmbType.Enabled = True
    '            If Not TxtNumConvention.Enabled And CmbType.SelectedIndex >= 0 Then
    '                TxtNumConvention.Enabled = True
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub CmbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If (CmbType.Text <> "") Then
    '        TxtNumConvention.Enabled = True
    '    End If
    'End Sub
    'Private Sub TxtNumConvention_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If (TxtNumConvention.Text <> "") Then
    '        BtRecherche.Enabled = True
    '        TxtCFA.Enabled = True
    '        TxtEuro.Enabled = True
    '        TxtUS.Enabled = True
    '        TxtLIVRE.Enabled = True
    '    End If
    'End Sub
    'Private Sub TxtCFA_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    TesterChampsMont()
    '    VerifSaisieMontant(TxtCFA)

    '    If (TxtCFA.Focused And TxtCFA.Text <> "") Then
    '        ConversionMont()
    '    ElseIf (TxtCFA.Focused And TxtCFA.Text = "") Then
    '        TxtEuro.Text = ""
    '        TxtUS.Text = ""
    '        TxtLIVRE.Text = ""
    '    End If
    'End Sub
    Private Sub TesterChampsMont()
        If (TxtCFA.Text <> "" Or TxtEuro.Text <> "" Or TxtUS.Text <> "" Or TxtLIVRE.Text <> "") Then
            DTDateSignature.Enabled = True
        End If
    End Sub

    Private Sub ConversionMont()
        Dim TauxCfa As Double = 1
        Dim TauxEuro As Double = 1
        Dim TauxDoll As Double = 1
        Dim TauxLivre As Double = 1
        query = "select TauxDevise from T_Devise where AbregeDevise='FCFA'"
        Dim dt0 = ExcecuteSelectQuery(query)
        For Each rw In dt0.Rows
            TauxCfa = CDbl(rw("TauxDevise"))
        Next
        query = "select TauxDevise from T_Devise where AbregeDevise='€'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw In dt0.Rows
            TauxEuro = CDbl(rw("TauxDevise"))
        Next
        query = "select TauxDevise from T_Devise where AbregeDevise='US$'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw In dt0.Rows
            TauxDoll = CDbl(rw("TauxDevise"))
        Next
        query = "select TauxDevise from T_Devise where AbregeDevise='£'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw In dt0.Rows
            TauxLivre = CDbl(rw("TauxDevise"))
        Next


        If (ActiveControl.Parent.Name = "TxtCFA") Then
            TxtEuro.Text = AfficherMonnaie(Math.Round((TauxCfa / TauxEuro) * CDbl(TxtCFA.Text.Replace(" ", "")), 2).ToString)
            TxtUS.Text = AfficherMonnaie(Math.Round((TauxCfa / TauxDoll) * CDbl(TxtCFA.Text.Replace(" ", "")), 2).ToString)
            TxtLIVRE.Text = AfficherMonnaie(Math.Round((TauxCfa / TauxLivre) * CDbl(TxtCFA.Text.Replace(" ", "")), 2).ToString)
        ElseIf (ActiveControl.Parent.Name = "TxtEuro") Then
            TxtCFA.Text = AfficherMonnaie(Math.Round((TauxEuro / TauxCfa) * CDbl(TxtEuro.Text.Replace(" ", "")), 0).ToString)
            TxtUS.Text = AfficherMonnaie(Math.Round((TauxEuro / TauxDoll) * CDbl(TxtEuro.Text.Replace(" ", "")), 2).ToString)
            TxtLIVRE.Text = AfficherMonnaie(Math.Round((TauxEuro / TauxLivre) * CDbl(TxtEuro.Text.Replace(" ", "")), 2).ToString)
        ElseIf (ActiveControl.Parent.Name = "TxtUS") Then
            TxtCFA.Text = AfficherMonnaie(Math.Round((TauxDoll / TauxCfa) * CDbl(TxtUS.Text.Replace(" ", "")), 0).ToString)
            TxtEuro.Text = AfficherMonnaie(Math.Round((TauxDoll / TauxEuro) * CDbl(TxtUS.Text.Replace(" ", "")), 2).ToString)
            TxtLIVRE.Text = AfficherMonnaie(Math.Round((TauxDoll / TauxLivre) * CDbl(TxtUS.Text.Replace(" ", "")), 2).ToString)
        ElseIf (ActiveControl.Parent.Name = "TxtLIVRE") Then
            TxtCFA.Text = AfficherMonnaie(Math.Round((TauxLivre / TauxCfa) * CDbl(TxtLIVRE.Text.Replace(" ", "")), 0).ToString)
            TxtEuro.Text = AfficherMonnaie(Math.Round((TauxLivre / TauxEuro) * CDbl(TxtLIVRE.Text.Replace(" ", "")), 2).ToString)
            TxtUS.Text = AfficherMonnaie(Math.Round((TauxLivre / TauxDoll) * CDbl(TxtLIVRE.Text.Replace(" ", "")), 2).ToString)
        End If

    End Sub

    'Private Sub DTDateSignature_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs)
    '    DTEntreeVigueur.Enabled = True
    'End Sub
    'Private Sub DTEntreeVigueur_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs)
    '    DTCloture.Enabled = True
    'End Sub
    'Private Sub DTCloture_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs)
    '    BtEnregistrer.Enabled = True
    'End Sub
    'Private Sub ChargerGridConv()
    '    Dim DateDeb, DateFin, DateJr As Date

    '    query = "select c.TypeConvention, c.CodeConvention, c.MontantConvention, c.DateSignature, c.EntreeEnVigueur, c.DateCloture, b.InitialeBailleur from T_Convention c, T_Bailleur b where c.CodeBailleur=b.CodeBailleur and b.CodeProjet='" & ProjetEnCours & "'"
    '    GridConvention.Rows.Clear()
    '    Dim dt0 = ExcecuteSelectQuery(query)
    '    For Each rw In dt0.Rows
    '        DateDeb = CDate(rw(4))
    '        DateFin = CDate(rw(5))
    '        DateJr = Now.ToShortDateString
    '        Dim EtatActu As String = "En exécution"

    '        Dim n As Integer = GridConvention.Rows.Add()
    '        GridConvention.Rows.Item(n).Cells(0).Value = MettreApost(rw(6))
    '        GridConvention.Rows.Item(n).Cells(1).Value = MettreApost(rw(0))
    '        GridConvention.Rows.Item(n).Cells(2).Value = MettreApost(rw(1))
    '        GridConvention.Rows.Item(n).Cells(3).Value = AfficherMonnaie(rw(2).ToString)
    '        GridConvention.Rows.Item(n).Cells(4).Value = rw(3)
    '        GridConvention.Rows.Item(n).Cells(5).Value = rw(4)
    '        GridConvention.Rows.Item(n).Cells(6).Value = rw(5)

    '        If (DateTime.Compare(DateJr, DateDeb) < 0) Then
    '            EtatActu = "En attente"
    '        ElseIf (DateTime.Compare(DateFin, DateJr) < 0) Then
    '            EtatActu = "Délai passé"
    '        End If
    '        GridConvention.Rows.Item(n).Cells(7).Value = EtatActu
    '    Next


    'End Sub

    'Private Sub BtRecherche_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim dlg As New OpenFileDialog
    '    dlg.FileName = String.Empty
    '    If dlg.ShowDialog() = DialogResult.OK Then
    '        Dim fichier As String = dlg.FileName
    '        Dim NomComp As String() = fichier.Split("\"c)
    '        Dim Nbr As Integer = 0
    '        For Each Elt In NomComp
    '            Nbr = Nbr + 1
    '        Next
    '        TxtPieceJointe.Text = NomComp(Nbr - 1)
    '        TxtChemin.Text = fichier

    '    End If

    'End Sub

    'Private Sub GridConvention_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    '    Dim IndexLg As Integer = GridConvention.CurrentRow.Index
    '    CmbType.Text = GridConvention.Rows.Item(IndexLg).Cells(1).Value
    '    TxtNumConvention.Text = MettreApost(GridConvention.Rows.Item(IndexLg).Cells(2).Value)
    '    TxtCFA.Focus()
    '    TxtCFA.Text = GridConvention.Rows.Item(IndexLg).Cells(3).Value.ToString.Replace(" ", "")
    '    DTDateSignature.Text = CDate(GridConvention.Rows.Item(IndexLg).Cells(4).Value)
    '    DTEntreeVigueur.Text = CDate(GridConvention.Rows.Item(IndexLg).Cells(5).Value)
    '    DTCloture.Text = CDate(GridConvention.Rows.Item(IndexLg).Cells(6).Value)
    '    Dim EtatAct As String = GridConvention.Rows.Item(IndexLg).Cells(7).Value.ToString
    '    If (EtatAct = "En attente") Then
    '        OkModifS = True
    '    Else
    '        OkModifS = False
    '    End If

    '    Dim source As String = ""
    '    Dim textsource As String = ""

    '    query = "select C.PieceConvention, C.NomPiece, B.InitialeBailleur, B.NomBailleur from T_Convention C, T_Bailleur B where C.CodeConvention='" & TxtNumConvention.Text & "' and B.CodeBailleur=C.CodeBailleur"
    '    Dim dt0 = ExcecuteSelectQuery(query)
    '    For Each rw In dt0.Rows
    '        TxtPieceJointe.Text = MettreApost(rw(1))
    '        TxtChemin.Text = MettreApost(rw(0))
    '        source = MettreApost(rw(2))
    '        textsource = MettreApost(rw(3))
    '    Next

    '    CmbSource.Text = source.ToString
    '    TxtSource.Text = textsource.ToString
    '    DTEntreeVigueur.Enabled = True
    '    DTCloture.Enabled = True
    '    TxtNumConvention.Enabled = False
    '    CmbSource.Enabled = False
    '    BtEnregistrer.Enabled = False
    'End Sub

    'Private Sub BtEnregistrer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim erreur As String = ""

    '        If CmbSource.Text = "" Then
    '            erreur += "- Selectionner le Bailleur" + ControlChars.CrLf
    '        End If

    '        If CmbType.Text = "" Then
    '            erreur += "- Selectionner le type" + ControlChars.CrLf
    '        End If

    '        If TxtNumConvention.Text = "" Then
    '            erreur += "- Renseigner le Code de la convention" + ControlChars.CrLf
    '        End If

    '        If TxtCFA.Text = "" Then
    '            erreur += "- Renseigner le Montant de la convention" + ControlChars.CrLf
    '        End If

    '        If erreur = "" Then

    '            Dim Pays As String = ""
    '            query = "select PaysProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
    '            Dim dt0 = ExcecuteSelectQuery(query)
    '            For Each rw In dt0.Rows
    '                Pays = MettreApost(rw(0))
    '            Next

    '            Dim sqlconn As New MySqlConnection
    '            BDOPEN(sqlconn)
    '            Dim DatSet = New DataSet
    '            query = "select * from T_Convention"
    '            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
    '            Dim DatAdapt = New MySqlDataAdapter(Cmd)
    '            DatAdapt.Fill(DatSet, "T_Convention")
    '            Dim DatTable = DatSet.Tables("T_Convention")
    '            Dim DatRow = DatSet.Tables("T_Convention").NewRow()

    '            DatRow("CodeConvention") = EnleverApost(TxtNumConvention.Text)
    '            DatRow("TypeConvention") = CmbType.Text
    '            DatRow("TitreConvention") = ""
    '            DatRow("Beneficiaire") = EnleverApost(Pays)
    '            DatRow("MontantConvention") = TxtCFA.Text.Replace(" ", "")
    '            DatRow("DateSignature") = DTDateSignature.Text.ToString
    '            DatRow("EntreeEnVigueur") = DTEntreeVigueur.Text.ToString
    '            DatRow("DateCloture") = DTCloture.Text.ToString

    '            If (TxtPieceJointe.Text <> "") Then
    '                Dim NomFichier As String = line & "\Conventions\" & CmbSource.Text & "_" & CmbType.Text & TxtNumConvention.Text.Replace("/", "&")
    '                If (Directory.Exists(NomFichier) = False) Then
    '                    Directory.CreateDirectory(NomFichier)
    '                End If
    '                NomFichier = NomFichier & "\" & TxtPieceJointe.Text
    '                File.Copy(TxtChemin.Text, NomFichier, True)
    '                DatRow("PieceConvention") = EnleverApost(NomFichier)
    '                DatRow("NomPiece") = EnleverApost(TxtPieceJointe.Text)
    '            Else
    '                DatRow("PieceConvention") = ""
    '                DatRow("NomPiece") = ""
    '            End If

    '            DatRow("CodeBailleur") = TxtCodeSource.Text
    '            DatSet.Tables("T_Convention").Rows.Add(DatRow)
    '            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
    '            DatAdapt.Update(DatSet, "T_Convention")
    '            DatSet.Clear()
    '            BDQUIT(sqlconn)

    '            query = "CALL `CreateTampColConvention`();"
    '            ExecuteNonQuery(query)
    '            'ChargerGridConv()
    '            MsgBox("Enregistrement terminée avec succès.", MsgBoxStyle.Information)
    '            BtRetour_Click(Me, e)
    '        Else
    '            MsgBox("Veuillez : " + ControlChars.CrLf + erreur, MsgBoxStyle.Exclamation)
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.ToString, MsgBoxStyle.Information)
    '    End Try

    'End Sub
    Private Sub SourceFinancement_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub



    Private Sub GridConvention1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridConvention1.Click

        ''Dim IndexLg As Integer = GridConvention.CurrentRow.Index

        'If ViewConvention1.RowCount > 0 Then

        '    drx = ViewConvention1.GetDataRow(ViewConvention1.FocusedRowHandle)
        '    Dim IDl = drx("Numéro").ToString

        '    ColorRowGrid(ViewConvention1, "[Numéro]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
        '    ColorRowGridAnal(ViewConvention1, "[Numéro]='" & IDl & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)

        '    CmbSource.Text = drx("Bailleur").ToString
        '    CmbType.Text = drx("Type").ToString
        '    TxtNumConvention.Text = MettreApost(drx("Numéro").ToString)
        '    TxtCFA.Focus()
        '    TxtCFA.Text = drx("Montant").ToString.Replace(" ", "")
        '    DTDateSignature.Text = CDate(drx("Signature")).ToShortDateString
        '    DTEntreeVigueur.Text = CDate(drx("Ouverture")).ToShortDateString
        '    DTCloture.Text = CDate(drx("Clôture")).ToShortDateString

        '    Dim EtatAct As String = drx("Etat actuel").ToString
        '    If (EtatAct = "En attente") Then
        '        OkModifS = True
        '    Else
        '        OkModifS = False
        '    End If

        '    'ChargerGridConv()
        '    'ChargerConvention()

        '    Dim source As String = ""
        '    Dim textsource As String = ""

        '    query = "select C.PieceConvention, C.NomPiece, B.InitialeBailleur, B.NomBailleur from T_Convention C, T_Bailleur B where C.CodeConvention='" & TxtNumConvention.Text & "' and B.CodeBailleur=C.CodeBailleur"
        '    Dim dt0 = ExcecuteSelectQuery(query)
        '    For Each rw In dt0.Rows
        '        TxtPieceJointe.Text = MettreApost(rw("NomPiece").ToString)
        '        TxtChemin.Text = MettreApost(rw("PieceConvention").ToString)
        '        source = MettreApost(rw("InitialeBailleur").ToString)
        '        textsource = MettreApost(rw("NomBailleur").ToString)
        '    Next
        '    BtRetour.Enabled = True
        '    BtEnregistrer.Enabled = False
        '    BtModifier.Enabled = True
        '    BtSupprimer.Enabled = True
        '    BtAjouter.Enabled = False
        '    CmbSource.Text = source.ToString
        '    TxtSource.Text = textsource.ToString
        '    TxtSource.Enabled = False
        '    DTEntreeVigueur.Enabled = True
        '    DTCloture.Enabled = True
        '    CmbSource.Enabled = False
        '    TxtNumConvention.Enabled = False


        'End If
    End Sub

    Private Sub CmbSource_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbSource.SelectedIndexChanged
        Try
            If (CmbSource.Text <> "") Then
                query = "select NomBailleur,CodeBailleur from T_Bailleur where InitialeBailleur='" & CmbSource.Text & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt0 = ExcecuteSelectQuery(query)
                For Each rw In dt0.Rows
                    TxtSource.Text = MettreApost(rw("NomBailleur"))
                    TxtCodeSource.Text = rw("CodeBailleur")
                Next
                CmbType.Enabled = True
                If Not TxtNumConvention.Enabled And CmbType.SelectedIndex >= 0 Then
                    TxtNumConvention.Enabled = True
                    TxtSource.Enabled = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CmbType_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbType.SelectedIndexChanged
        If (CmbType.Text <> "") Then
            TxtNumConvention.Enabled = True
        End If
    End Sub

    Private Sub TxtNumConvention_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNumConvention.EditValueChanged
        If (TxtNumConvention.Text <> "") Then
            BtRecherche.Enabled = True
            TxtCFA.Enabled = True
            TxtEuro.Enabled = True
            TxtUS.Enabled = True
            TxtLIVRE.Enabled = True
        End If
    End Sub

    Private Sub TxtCFA_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCFA.EditValueChanged
        On Error Resume Next
        TesterChampsMont()
        'VerifSaisieMontant(TxtCFA)
        'MsgBox(ActiveControl.Parent.Name)
        If (ActiveControl.Parent.Name = "TxtCFA" And TxtCFA.Text <> "") Then
            ConversionMont()
        ElseIf (ActiveControl.Parent.Name = "TxtCFA" And TxtCFA.Text = "") Then
            TxtCFA.Text = ""
            TxtEuro.Text = ""
            TxtUS.Text = ""
            TxtLIVRE.Text = ""
        End If
    End Sub

    Private Sub TxtEuro_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtEuro.EditValueChanged
        TesterChampsMont()
        'VerifSaisieMontant(TxtEuro)

        If (ActiveControl.Parent.Name = "TxtEuro" And TxtEuro.Text <> "") Then
            ConversionMont()
        ElseIf (ActiveControl.Parent.Name = "TxtEuro" And TxtEuro.Text = "") Then
            TxtCFA.Text = ""
            TxtUS.Text = ""
            TxtLIVRE.Text = ""
        End If
    End Sub

    Private Sub TxtUS_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtUS.EditValueChanged
        TesterChampsMont()
        'VerifSaisieMontant(TxtUS)

        If (ActiveControl.Parent.Name = "TxtUS" And TxtUS.Text <> "") Then
            ConversionMont()
        ElseIf (ActiveControl.Parent.Name = "TxtUS" And TxtUS.Text = "") Then
            TxtCFA.Text = ""
            TxtEuro.Text = ""
            TxtLIVRE.Text = ""
        End If
    End Sub

    Private Sub TxtLIVRE_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtLIVRE.EditValueChanged
        TesterChampsMont()
        'VerifSaisieMontant(TxtLIVRE)

        If (ActiveControl.Parent.Name = "TxtLIVRE" And TxtLIVRE.Text <> "") Then
            ConversionMont()
        ElseIf (ActiveControl.Parent.Name = "TxtLIVRE" And TxtLIVRE.Text = "") Then
            TxtCFA.Text = ""
            TxtEuro.Text = ""
            TxtUS.Text = ""
        End If
    End Sub

    Private Sub DTDateSignature_CloseUp(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles DTDateSignature.CloseUp
        DTEntreeVigueur.Enabled = True
    End Sub

    Private Sub DTEntreeVigueur_CloseUp(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles DTEntreeVigueur.CloseUp
        DTCloture.Enabled = True
    End Sub

    'Private Sub DTCloture_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTCloture.CloseUp
    '    BtEnregistrer.Enabled = False
    'End Sub

    Private Sub BtModifier_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtModifier.Click
        Try
            If CmbSource.Text <> "" Then

                If ConfirmMsg("Voulez vous modifier cette convention ?") = DialogResult.Yes Then
                    Dim sqlconn As New MySqlConnection
                    BDOPEN(sqlconn)
                    Dim DatSet = New DataSet
                    query = "select * from T_Convention where CodeConvention='" & TxtNumConvention.Text & "'"

                    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                    Dim DatAdapt = New MySqlDataAdapter(Cmd)
                    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                    DatAdapt.Fill(DatSet, "T_Convention")

                    'DatSet.Tables!T_Convention.Rows(0)!CodeConvention = TxtNumConvention.Text
                    DatSet.Tables!T_Convention.Rows(0)!TypeConvention = CmbType.Text
                    DatSet.Tables!T_Convention.Rows(0)!NomPiece = TxtPieceJointe.Text
                    DatSet.Tables!T_Convention.Rows(0)!MontantConvention = TxtCFA.Text.Replace(" ", "")
                    DatSet.Tables!T_Convention.Rows(0)!DateSignature = DTDateSignature.Text.ToString
                    DatSet.Tables!T_Convention.Rows(0)!EntreeEnVigueur = DTEntreeVigueur.Text.ToString
                    DatSet.Tables!T_Convention.Rows(0)!DateCloture = DTCloture.Text.ToString



                    'Mise à jour de la table T_Categorie
                    query = "select MontantCateg from t_categoriedepense where CodeConvention='" & TxtNumConvention.Text & "'"
                    Dim Verif = Val(ExecuteScallar(query))
                    If Verif <> 0 Then
                        query = "select * from t_categoriedepense where CodeConvention='" & TxtNumConvention.Text & "'"
                        Dim dt As DataTable = ExcecuteSelectQuery(query)

                        For Each rw In dt.Rows
                            Dim PourcCateg As Double = 0
                            'rw("MontantCateg").ToString / TxtCFA.Text

                            PourcCateg = Math.Round((rw("MontantCateg").ToString * 100) / TxtCFA.EditValue, 2).ToString
                            query = "update t_categoriedepense set PrctCateg='" & PourcCateg & "' where CodeCateg='" & rw("CodeCateg").ToString & "' "
                            ExecuteNonQuery(query)
                        Next
                    End If



                    'If (TxtPieceJointe.Text <> "") Then
                    '    Dim NomFichier As String = line & "\Conventions\" & CmbSource.Text & "_" & CmbType.Text & TxtNumConvention.Text.Replace("/", "&")

                    '    'If (Directory.Exists(NomFichier) = False) Then
                    '    '    Directory.CreateDirectory(NomFichier)
                    '    NomFichier = NomFichier & TxtPieceJointe.Text
                    '    System.IO.File.Copy(TxtChemin.Text, NomFichier, True)
                    '    'Else
                    '    'End If
                    '    'NomFichier = NomFichier & TxtPieceJointe.Text
                    '    DatSet.Tables!T_Convention.Rows(0)!PieceConvention = EnleverApost(NomFichier)
                    '    DatSet.Tables!T_Convention.Rows(0)!NomPiece = EnleverApost(TxtPieceJointe.Text)
                    'End If

                    DatAdapt.Update(DatSet, "T_Convention")
                    DatSet.Clear()
                    ChargerConvention()
                    SuccesMsg("Modification terminée avec succès.")
                    BtRetour_Click_1(Me, e)
                    BDQUIT(sqlconn)
                    ViderZoneTexte()
                End If
            Else
                SuccesMsg("Veuillez selectionner une ligne dans le tableau !")
            End If

        Catch ex As Exception
            FailMsg(" Informations non disponible " & vbNewLine & ex.ToString)
        End Try
    End Sub

    Private Sub BtSupprimer_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSupprimer.Click
        Try




            If (ViewConvention1.RowCount > 0) Then

                'Dim CodeBail_List As String = ""
                Dim cpte As Decimal = 0
                Dim errordel As Decimal = 0
                Dim str As String = String.Empty
                drx = ViewConvention1.GetDataRow(ViewConvention1.FocusedRowHandle)
                'Dim CodeConv = drx("Numéro").ToString
                Dim LibConv = drx("Type").ToString
                'For i = 0 To ViewBailleur.RowCount - 1
                'If CBool(ViewBailleur.GetRowCellValue(i, "Choix")) = True Then
                'CodeBail_List &= CodeBail & ";"
                '---------requete de verif
                query = "select Distinct CodeConvention from t_categoriedepense where CodeConvention ='" & TxtNumConvention.Text & "'"
                Dim dt = ExcecuteSelectQuery(query)
                If dt.Rows.Count > 0 Then
                    SuccesMsg("impossible de supprimer cette convention !")

                    Exit Sub
                End If
                If ConfirmMsg("Voulez vous supprimer cette convention?") = DialogResult.Yes Then

                    Dim NomFichier As String = line & "\Conventions\" & CmbSource.Text & "_" & CmbType.Text & TxtNumConvention.Text.Replace("/", "&")
                    If (Directory.Exists(NomFichier) = True) Then
                        Directory.Delete(NomFichier)
                    End If
                    'NomFichier = NomFichier & TxtPieceJointe.Text
                    'System.IO.File.Delete(TxtChemin.Text)

                    query = "delete from t_convention where CodeConvention='" & TxtNumConvention.Text & "'"
                    ExecuteNonQuery(query)

                    query = "CALL `DeleteTampColConvention`();"
                    'ExecuteNonQuery(query)
                    query = "CALL `CreateTampColConvention`();"
                    'ExecuteNonQuery(query)
                    BtRetour_Click_1(Me, e)
                    ViderZoneTexte()
                    ChargerConvention()
                    SuccesMsg("Suppression effectuée avec succès")

                End If
                'End If
                'Next

            End If

            'Next
            'End If




            '    If CmbSource.Text <> "" Then
            '        If MsgBox("Voulez vous vraiment supprimer?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then

            '            query = "DELETE from T_Convention where CodeConvention='" & TxtNumConvention.Text & "'"
            '            ExecuteScallar(query)

            '            Dim NomFichier As String = line & "\Conventions\" & CmbSource.Text & "_" & CmbType.Text & TxtNumConvention.Text.Replace("/", "&")
            '            If (Directory.Exists(NomFichier) = True) Then
            '                Directory.Delete(NomFichier)
            '            End If


            '            query = "CALL `DeleteTampColConvention`();"
            '            'ExecuteNonQuery(query)
            '            query = "CALL `CreateTampColConvention`();"
            '            'ExecuteNonQuery(query)
            '            'BtRetour_Click_1(Me, e)
            '            SuccesMsg("Suppression terminée avec succès.")
            '            ChargerConvention()
            '        End If
            '    Else
            '        FailMsg("Veuillez selectionner une ligne dans le tableau !")
            '    End If
        Catch ex As Exception
            FailMsg(" Informations non disponible " & vbNewLine & ex.ToString)
        End Try
    End Sub

    Private Sub BtEnregistrer_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click
        Try


            Dim erreur As String = ""

            If CmbSource.Text = "" Then
                erreur += "- Selectionner le Bailleur" + ControlChars.CrLf
            End If

            If CmbType.Text = "" Then
                erreur += "- Selectionner le type" + ControlChars.CrLf
            End If

            If TxtNumConvention.Text = "" Then
                erreur += "- Renseigner le Code de la convention" + ControlChars.CrLf
            End If

            If TxtCFA.Text = "" Then
                erreur += "- Renseigner le Montant de la convention" + ControlChars.CrLf
            End If

            If erreur = "" Then

                Dim Pays As String = ""
                query = "select PaysProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
                Dim dt0 = ExcecuteSelectQuery(query)
                For Each rw In dt0.Rows
                    Pays = MettreApost(rw(0))
                Next

                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim DatSet = New DataSet
                query = "select * from T_Convention"
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "T_Convention")
                Dim DatTable = DatSet.Tables("T_Convention")
                Dim DatRow = DatSet.Tables("T_Convention").NewRow()

                DatRow("CodeConvention") = EnleverApost(TxtNumConvention.Text)
                DatRow("TypeConvention") = CmbType.Text
                DatRow("TitreConvention") = ""
                DatRow("Beneficiaire") = EnleverApost(Pays)
                DatRow("MontantConvention") = TxtCFA.Text.Replace(" ", "")
                DatRow("DateSignature") = DTDateSignature.Text.ToString
                DatRow("EntreeEnVigueur") = DTEntreeVigueur.Text.ToString
                DatRow("DateCloture") = DTCloture.Text.ToString

                If (TxtPieceJointe.Text <> "") Then
                    Dim NomFichier As String = line & "\Conventions\" & CmbSource.Text & "_" & CmbType.Text & TxtNumConvention.Text.Replace("/", "&")
                    'If (Directory.Exists(NomFichier) = False) Then
                    '    Directory.CreateDirectory(NomFichier)
                    NomFichier = NomFichier & TxtPieceJointe.Text
                    System.IO.File.Copy(TxtChemin.Text, NomFichier, True)
                    'End If
                    'NomFichier = NomFichier & "\" & TxtPieceJointe.Text
                    'File.Copy(TxtChemin.Text, NomFichier, True)
                    DatRow("PieceConvention") = EnleverApost(NomFichier)
                    DatRow("NomPiece") = EnleverApost(TxtPieceJointe.Text)
                Else
                    DatRow("PieceConvention") = ""
                    DatRow("NomPiece") = ""
                End If

                DatRow("CodeBailleur") = TxtCodeSource.Text
                DatSet.Tables("T_Convention").Rows.Add(DatRow)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "T_Convention")
                DatSet.Clear()
                BDQUIT(sqlconn)

                query = "CALL `CreateTampColConvention`();"
                'ExecuteNonQuery(query)

                SuccesMsg("Enregistrement terminé avec succès.")
                ChargerConvention()
                BtRetour_Click_1(Me, e)

            Else
                SuccesMsg("Veuillez : " + ControlChars.CrLf + erreur)
            End If
        Catch ex As Exception
            FailMsg(" Informations non disponible " & vbNewLine & ex.ToString)
        End Try
    End Sub

    Private Sub BtAjouter_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjouter.Click
        CmbSource.Enabled = True
        BtRetour.Enabled = True
        BtEnregistrer.Enabled = True
        BtModifier.Enabled = False
        BtSupprimer.Enabled = False
    End Sub

    Private Sub BtRetour_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRetour.Click
        BtAjouter.Enabled = True
        BtModifier.Enabled = False
        BtSupprimer.Enabled = False

        BtEnregistrer.Enabled = False
        BtRetour.Enabled = False

        PourAjout = False
        PourModif = False
        PourSupprim = False

        CmbSource.Text = ""
        CmbSource.Enabled = False
        TxtSource.Text = ""
        TxtSource.Enabled = False
        CmbType.Text = ""
        CmbType.Enabled = False
        TxtNumConvention.Text = ""
        TxtNumConvention.Enabled = False
        BtRecherche.Enabled = False
        TxtCFA.Text = ""
        TxtCFA.Enabled = False
        TxtEuro.Text = ""
        TxtEuro.Enabled = False
        TxtUS.Text = ""
        TxtUS.Enabled = False
        TxtLIVRE.Text = ""
        TxtLIVRE.Enabled = False
        DTDateSignature.Text = Now.ToShortDateString
        DTDateSignature.Enabled = False
        DTEntreeVigueur.Text = Now.ToShortDateString
        DTEntreeVigueur.Enabled = False
        DTCloture.Text = Now.ToShortDateString
        DTCloture.Enabled = False
        TxtPieceJointe.Text = ""

        ChargerSource()
        ChargerConvention()
    End Sub

    Private Sub GridConvention1_DoubleClick(sender As Object, e As EventArgs) Handles GridConvention1.DoubleClick
        'Dim IndexLg As Integer = GridConvention.CurrentRow.Index

        If ViewConvention1.RowCount > 0 Then

            drx = ViewConvention1.GetDataRow(ViewConvention1.FocusedRowHandle)
            Dim IDl = drx("Numéro").ToString

            ColorRowGrid(ViewConvention1, "[Numéro]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewConvention1, "[Numéro]='" & IDl & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)

            CmbSource.Text = drx("Bailleur").ToString
            CmbType.Text = drx("Type").ToString
            TxtNumConvention.Text = MettreApost(drx("Numéro").ToString)
            TxtCFA.Focus()
            TxtCFA.Text = drx("Montant").ToString.Replace(" ", "")
            DTDateSignature.Text = CDate(drx("Signature")).ToShortDateString
            DTEntreeVigueur.Text = CDate(drx("Ouverture")).ToShortDateString
            DTCloture.Text = CDate(drx("Clôture")).ToShortDateString

            Dim EtatAct As String = drx("Etat actuel").ToString
            If (EtatAct = "En attente") Then
                OkModifS = True
            Else
                OkModifS = False
            End If

            Dim source As String = ""
            Dim textsource As String = ""

            query = "select C.PieceConvention, C.NomPiece, B.InitialeBailleur, B.NomBailleur from T_Convention C, T_Bailleur B where C.CodeConvention='" & TxtNumConvention.Text & "' and B.CodeBailleur=C.CodeBailleur"
            Dim dt0 = ExcecuteSelectQuery(query)
            For Each rw In dt0.Rows
                TxtPieceJointe.Text = MettreApost(rw("NomPiece").ToString)
                TxtChemin.Text = MettreApost(rw("PieceConvention").ToString)
                source = MettreApost(rw("InitialeBailleur").ToString)
                textsource = MettreApost(rw("NomBailleur").ToString)
            Next
            BtRetour.Enabled = True
            BtEnregistrer.Enabled = False
            BtModifier.Enabled = True
            BtSupprimer.Enabled = True
            BtAjouter.Enabled = False
            CmbSource.Text = source.ToString
            TxtSource.Text = textsource.ToString
            TxtSource.Enabled = False
            DTEntreeVigueur.Enabled = True
            DTCloture.Enabled = True
            CmbSource.Enabled = False
            TxtNumConvention.Enabled = False


        End If
    End Sub

    Private Sub BtRecherche_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRecherche.Click
        Dim dlg As New OpenFileDialog
        dlg.FileName = String.Empty
        If dlg.ShowDialog() = DialogResult.OK Then
            Dim fichier As String = dlg.FileName
            Dim NomComp As String() = fichier.Split("\"c)
            Dim Nbr As Integer = 0
            For Each Elt In NomComp
                Nbr = Nbr + 1
            Next
            TxtPieceJointe.Text = NomComp(Nbr - 1)
            TxtChemin.Text = fichier

        End If
    End Sub

    Private Sub ConsulterLaConventionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConsulterLaConventionToolStripMenuItem.Click
        Try
            If (ViewConvention1.RowCount > 0) Then


                drx = ViewConvention1.GetDataRow(ViewConvention1.FocusedRowHandle)
                query = "SELECT NomPiece FROM t_convention WHERE CodeConvention='" & drx("Numéro").ToString & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw In dt.Rows
                    Dim Piece_convention = line + "\Conventions\" & CmbSource.Text & "_" & CmbType.Text & TxtNumConvention.Text.Replace("/", "&") + rw(0).ToString
                    If File.Exists(Piece_convention) Then
                        Process.Start(Piece_convention)
                    Else
                        FailMsg("Le fichier n'exite pas.")
                    End If
                Next
            End If
        Catch ex As Exception
            FailMsg("" & vbNewLine)
        End Try
    End Sub
End Class