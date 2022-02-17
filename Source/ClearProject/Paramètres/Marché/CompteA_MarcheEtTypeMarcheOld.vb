Imports MySql.Data.MySqlClient

Public Class CompteA_MarcheEtTypeMarcheOld

    Dim dtGeneral = New DataTable()
    Dim dtType = New DataTable()
    Dim dtMarche = New DataTable()
    Dim DrX As DataRow

    Dim GeneralSelect As String = ""
    Dim TypeSelect As String = ""
    Dim MarcheSelect As String = ""
    Private Sub CompteA_MarcheEtTypeMarche_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        RemplirGeneral()
        RemplirType()
        RemplirMarche()
    End Sub

    Private Sub RemplirGeneral()
        GeneralSelect = ""

        dtGeneral.Columns.Clear()

        dtGeneral.Columns.Add("Code", Type.GetType("System.String"))
        dtGeneral.Columns.Add("Compte", Type.GetType("System.String"))
        dtGeneral.Columns.Add("Libellé", Type.GetType("System.String"))

        Dim cptr As Decimal = 0
        query = "select CODE_SC, LIBELLE_SC from T_COMP_SOUS_CLASSE where TypeCompte='' and CompteMarche='N' order by CODE_SC"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        dtGeneral.Rows.Clear()
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtGeneral.NewRow()

            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = rw(0).ToString
            drS(2) = MettreApost(rw(1).ToString)

            dtGeneral.Rows.Add(drS)
        Next


        GridGeneral.DataSource = dtGeneral

        ViewGeneral.Columns(0).Visible = False
        ViewGeneral.Columns(1).Width = 80
        ViewGeneral.Columns(2).Width = 350

        ViewGeneral.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewGeneral.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        ViewGeneral.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

        ColorRowGrid(ViewGeneral, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub

    Private Sub RemplirType()
        TypeSelect = ""

        dtType.Columns.Clear()

        dtType.Columns.Add("Code", Type.GetType("System.String"))
        dtType.Columns.Add("Compte", Type.GetType("System.String"))
        dtType.Columns.Add("Type", Type.GetType("System.String"))
        dtType.Columns.Add("Libellé", Type.GetType("System.String"))

        Dim cptr As Decimal = 0

        'Dim Reader As MySqlDataReader

        query = "select CODE_SC, LIBELLE_SC, TypeCompte from T_COMP_SOUS_CLASSE where TypeCompte<>'' and CompteMarche='N' order by CODE_SC"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        dtType.Rows.Clear()
        For Each rw As DataRow In dt.Rows

            cptr += 1
            Dim drS = dtType.NewRow()

            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = rw(0).ToString
            drS(2) = rw(2).ToString
            drS(3) = MettreApost(rw(1).ToString)

            dtType.Rows.Add(drS)

        Next


        GridType.DataSource = dtType

        ViewType.Columns(0).Visible = False
        ViewType.Columns(1).Width = 80
        ViewType.Columns(2).Width = 50
        ViewType.Columns(3).Width = 300

        ViewType.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewType.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewType.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        ViewType.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

        ColorRowGrid(ViewType, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub

    Private Sub RemplirMarche()
        MarcheSelect = ""

        dtMarche.Columns.Clear()

        dtMarche.Columns.Add("Code", Type.GetType("System.String"))
        dtMarche.Columns.Add("Compte", Type.GetType("System.String"))
        dtMarche.Columns.Add("Libellé", Type.GetType("System.String"))

        Dim cptr As Decimal = 0

        'Dim Reader As MySqlDataReader

        query = "select CODE_SC, LIBELLE_SC from T_COMP_SOUS_CLASSE where TypeCompte<>'' and CompteMarche<>'N' order by CODE_SC"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        dtMarche.Rows.Clear()
        For Each rw As DataRow In dt.Rows

            cptr += 1
            Dim drS = dtMarche.NewRow()

            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = rw(0).ToString
            drS(2) = MettreApost(rw(1).ToString)

            dtMarche.Rows.Add(drS)

        Next


        GridMarche.DataSource = dtMarche

        ViewMarche.Columns(0).Visible = False
        ViewMarche.Columns(1).Width = 80
        ViewMarche.Columns(2).Width = 350

        ViewMarche.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewMarche.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        ViewMarche.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

        ColorRowGrid(ViewMarche, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub

    Private Sub GridGeneral_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridGeneral.Click

        GeneralSelect = ""
        If (ViewGeneral.RowCount > 0) Then

            DrX = ViewGeneral.GetDataRow(ViewGeneral.FocusedRowHandle)
            GeneralSelect = DrX(1).ToString
            ColorRowGrid(ViewGeneral, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewGeneral, "[Compte]='" & GeneralSelect & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)

        End If

    End Sub

    Private Sub GridType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridType.Click

        TypeSelect = ""
        If (ViewType.RowCount > 0) Then

            DrX = ViewType.GetDataRow(ViewType.FocusedRowHandle)
            TypeSelect = DrX(1).ToString
            ColorRowGrid(ViewType, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewType, "[Compte]='" & TypeSelect & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)

        End If

    End Sub

    Private Sub GridMarche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridMarche.Click

        MarcheSelect = ""
        If (ViewMarche.RowCount > 0) Then

            DrX = ViewMarche.GetDataRow(ViewMarche.FocusedRowHandle)
            MarcheSelect = DrX(1).ToString
            ColorRowGrid(ViewMarche, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewMarche, "[Compte]='" & MarcheSelect & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)

        End If

    End Sub

    Private Sub BtConsultants_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtConsultants.Click

        If (GeneralSelect <> "") Then
            ModifierType(GeneralSelect, "CS")
            ModifierTypepartition(GeneralSelect, "Consultants")
            RemplirGeneral()
            RemplirType()
        End If

    End Sub

    Private Sub ModifierType(ByVal Code As String, ByVal Type As String)

        query = "update T_COMP_SOUS_CLASSE set TypeCompte='" & Type & "' WHERE CODE_SC='" & Code & "'"
        ExecuteNonQuery(query)

    End Sub

    Private Sub ModifierTypepartition(ByVal Code As String, ByVal Type As String)

        query = "update t_besoinpartition set TypeBesoin='" & Type & "' WHERE NumeroComptable='" & Code & "'"
        ExecuteNonQuery(query)

    End Sub

    Private Sub ModifierMarche(ByVal Code As String, ByVal Decis As String)

        query = "update T_COMP_SOUS_CLASSE set CompteMarche='" & Decis & "' WHERE CODE_SC='" & Code & "'"
        ExecuteNonQuery(query)

    End Sub

    Private Sub BtFournitures_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtFournitures.Click

        If (GeneralSelect <> "") Then
            ModifierType(GeneralSelect, "FR")
            ModifierTypepartition(GeneralSelect, "Fournitures")
            RemplirGeneral()
            RemplirType()
        End If

    End Sub

    Private Sub BtTravaux_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtTravaux.Click

        If (GeneralSelect <> "") Then
            ModifierType(GeneralSelect, "TX")
            ModifierTypepartition(GeneralSelect, "Travaux")
            RemplirGeneral()
            RemplirType()
        End If

    End Sub

    Private Sub BtSvceAssimile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtSvceAssimile.Click

        If (GeneralSelect <> "") Then
            ModifierType(GeneralSelect, "SA")
            ModifierTypepartition(GeneralSelect, "Services autres que les services de consultants")
            RemplirGeneral()
            RemplirType()
        End If

    End Sub

    Private Sub BtSuppType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtSuppType.Click

        If (TypeSelect <> "") Then

            Dim NbType As Decimal = 0

            'Dim Reader10 As MySqlDataReader

            query = "select Count(*) from T_Marche where NumeroComptable='" & TypeSelect & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            Dim rw As DataRow = dt.Rows(0)

            NbType = CInt(rw(0))

            If (NbType <= 0) Then
                ModifierType(TypeSelect, "")
                RemplirGeneral()
                RemplirType()
            Else
                MsgBox("Compte en cours d'utilisation!", MsgBoxStyle.Exclamation)
            End If

        End If

    End Sub

    Private Sub BtMarche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtMarche.Click

        If (TypeSelect <> "") Then
            ModifierMarche(TypeSelect, "O")
            RemplirType()
            RemplirMarche()
        End If

    End Sub

    Private Sub BtSuppMarche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtSuppMarche.Click

        If (MarcheSelect <> "") Then

            Dim NbMar As Decimal = 0

            query = "select Count(*) from T_Marche where NumeroComptable='" & MarcheSelect & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            Dim rw As DataRow = dt.Rows(0)
            NbMar = CInt(rw(0))

            If (NbMar <= 0) Then
                ModifierMarche(MarcheSelect, "N")
                RemplirType()
                RemplirMarche()
            Else
                MsgBox("Compte en cours d'utilisation!", MsgBoxStyle.Exclamation)
            End If

        End If

    End Sub

    Private Sub BtToutMarche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtToutMarche.Click

        For k As Integer = 0 To ViewType.RowCount - 1
            ModifierMarche(ViewType.GetDataRow(k)(1).ToString, "O")
        Next
        RemplirType()
        RemplirMarche()

    End Sub

    Private Sub BtSuppToutMarche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtSuppToutMarche.Click

        Dim Message As String = ""
        'Dim Reader10 As MySqlDataReader
        For k As Integer = 0 To ViewMarche.RowCount - 1

            Dim NbMar As Decimal = 0

            query = "select Count(*) from T_Marche where NumeroComptable='" & ViewMarche.GetDataRow(k)(1).ToString & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            Dim rw As DataRow = dt.Rows(0)
            NbMar = CInt(rw(0))

            If (NbMar <= 0) Then
                ModifierMarche(ViewMarche.GetDataRow(k)(1).ToString, "N")
            Else
                If (Message <> "") Then Message = Message & ", "
                Message = Message & ViewMarche.GetDataRow(k)(1).ToString
            End If

        Next
        RemplirType()
        RemplirMarche()
        If (Message <> "") Then
            MsgBox("Les comptes " & Message & " sont en cours d'utilisation!", MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Sub BtSuppToutType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtSuppToutType.Click

        Dim Message As String = ""
        'Dim Reader10 As MySqlDataReader
        For k As Integer = 0 To ViewType.RowCount - 1

            Dim NbType As Decimal = 0

            query = "select Count(*) from T_Marche where NumeroComptable='" & ViewType.GetDataRow(k)(1).ToString & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            Dim rw As DataRow = dt.Rows(0)
            NbType = CInt(rw(0))

            If (NbType <= 0) Then
                ModifierType(ViewType.GetDataRow(k)(1).ToString, "")
            Else
                If (Message <> "") Then Message = Message & ", "
                Message = Message & ViewType.GetDataRow(k)(1).ToString
            End If

        Next
        RemplirGeneral()
        RemplirType()
        If (Message <> "") Then
            MsgBox("Les comptes " & Message & " sont en cours d'utilisation!", MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Sub BtMajRessources_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtMajRessources.Click

        Dim Message As String = ""
        Dim nbTotLigneAffectee As Decimal = 0
        For k As Integer = 0 To ViewType.RowCount - 1

            Dim nbCompte As Decimal = 0

            'Dim Reader10 As MySqlDataReader

            query = "select Count(*) from T_BesoinPartition where NumeroComptable='" & ViewType.GetDataRow(k)(1).ToString & "' and CodeProjet='" & ProjetEnCours & "' and TypeBesoin<>'" & IIf(ViewType.GetDataRow(k)(2).ToString = "CS", "Consultants", IIf(ViewType.GetDataRow(k)(2).ToString = "FR", "Fournitures", IIf(ViewType.GetDataRow(k)(2).ToString = "TX", "Travaux", "Services autres que les services de consultants").ToString).ToString).ToString & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            Dim rw As DataRow = dt.Rows(0)
            nbCompte = CInt(rw(0))

            If (nbCompte > 0) Then
                nbTotLigneAffectee += nbCompte
                If (Message <> "") Then Message = Message & ", "
                Message = Message & ViewType.GetDataRow(k)(1).ToString

                For n As Decimal = 0 To nbCompte - 1
                    query = "update T_BesoinPartition set TypeBesoin='" & IIf(ViewType.GetDataRow(k)(2).ToString = "CS", "Consultants", IIf(ViewType.GetDataRow(k)(2).ToString = "FR", "Fournitures", IIf(ViewType.GetDataRow(k)(2).ToString = "TX", "Travaux", "Services autres que les services de consultants").ToString).ToString).ToString & "' where NumeroComptable='" & ViewType.GetDataRow(k)(1).ToString & "' and CodeProjet='" & ProjetEnCours & "'"
                    ExecuteNonQuery(query)
                Next

            End If

        Next

        If (Message <> "") Then
            MsgBox("Les comptes " & Message & " ont été mis à jour avec succès." & vbNewLine & MontantLettre(nbTotLigneAffectee) & " (" & nbTotLigneAffectee & ") lignes affectées.", MsgBoxStyle.Information)
        Else
            MsgBox("0 lignes affectées!", MsgBoxStyle.Information)
        End If

    End Sub
End Class