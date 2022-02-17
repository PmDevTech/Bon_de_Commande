Imports MySql.Data.MySqlClient

Public Class CompteA_MarcheEtTypeMarche

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

        If ViewGeneral.Columns(0).Visible = True Then
            ViewGeneral.Columns(0).Visible = False
            ViewGeneral.Columns(1).MaxWidth = 80
            ViewGeneral.OptionsView.ColumnAutoWidth = True
            ViewGeneral.OptionsBehavior.AutoExpandAllGroups = True
            ViewGeneral.VertScrollVisibility = True
            ViewGeneral.HorzScrollVisibility = True
            ViewGeneral.BestFitColumns()
            ViewGeneral.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewGeneral.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewGeneral.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        End If
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

        query = "select CODE_SC, LIBELLE_SC, TypeCompte from T_COMP_SOUS_CLASSE where TypeCompte<>'' and CompteMarche='O' order by CODE_SC"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        dtType.Rows.Clear()
        For Each rw As DataRow In dt.Rows

            cptr += 1
            Dim drS = dtType.NewRow()

            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = rw("CODE_SC").ToString
            drS(2) = rw("TypeCompte").ToString
            drS(3) = MettreApost(rw("LIBELLE_SC").ToString)

            dtType.Rows.Add(drS)

        Next


        GridType.DataSource = dtType

        If ViewType.Columns(0).Visible = True Then
            ViewType.Columns(0).Visible = False
            ViewType.Columns(1).MaxWidth = 80
            ViewType.Columns(2).MaxWidth = 70
            ViewType.VertScrollVisibility = True
            ViewType.HorzScrollVisibility = True
            ViewType.OptionsView.ColumnAutoWidth = True
            'ViewType.OptionsBehavior.AutoExpandAllGroups = True
            'ViewType.BestFitColumns()
            ViewType.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewType.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewType.Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            ViewType.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        End If
        ColorRowGrid(ViewType, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

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

    Private Sub BtConsultants_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtConsultants.Click

        If (GeneralSelect <> "") Then
            ModifierMarche(GeneralSelect, "CS", "O")
            ModifierTypepartition(GeneralSelect, "Consultants")
            RemplirGeneral()
            RemplirType()
        End If

    End Sub

    Private Sub ModifierTypepartition(ByVal Code As String, ByVal Type As String)

        query = "update t_besoinpartition set TypeBesoin='" & Type & "' WHERE NumeroComptable='" & Code & "'"
        ExecuteNonQuery(query)

    End Sub

    Private Sub ModifierMarche(ByVal Code As String, ByVal Type As String, ByVal Decis As String)

        query = "update T_COMP_SOUS_CLASSE set CompteMarche='" & Decis & "',TypeCompte='" & Type & "' WHERE CODE_SC='" & Code & "'"
        ExecuteNonQuery(query)

    End Sub

    Private Sub BtFournitures_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtFournitures.Click

        If (GeneralSelect <> "") Then
            ModifierMarche(GeneralSelect, "FR", "O")
            ModifierTypepartition(GeneralSelect, "Fournitures")
            RemplirGeneral()
            RemplirType()
        End If

    End Sub

    Private Sub BtTravaux_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtTravaux.Click

        If (GeneralSelect <> "") Then
            ModifierMarche(GeneralSelect, "TX", "O")
            ModifierTypepartition(GeneralSelect, "Travaux")
            RemplirGeneral()
            RemplirType()
        End If

    End Sub

    Private Sub BtSvceAssimile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtSvceAssimile.Click

        If (GeneralSelect <> "") Then
            ModifierMarche(GeneralSelect, "SA", "O")
            ModifierTypepartition(GeneralSelect, "Services autres que les services de consultants")
            RemplirGeneral()
            RemplirType()
        End If

    End Sub

    Private Sub BtSuppType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtSuppType.Click

        If (TypeSelect <> "") Then

            Dim NbType As Decimal = 0

            query = "select Count(*) from T_Marche where NumeroComptable='" & TypeSelect & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            Dim rw As DataRow = dt.Rows(0)
           
            NbType = CInt(rw(0))
            
            If (NbType <= 0) Then
                ModifierMarche(TypeSelect, "", "N")
                ModifierTypepartition(TypeSelect, "")
                RemplirGeneral()
                RemplirType()
            Else
                SuccesMsg("Ce compte est en cours d'utilisation.")
            End If

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
                ModifierMarche(ViewType.GetDataRow(k)(1).ToString, "", "N")
                ModifierTypepartition(ViewType.GetDataRow(k)(1).ToString, "")
            Else
                If (Message <> "") Then Message = Message & ", "
                Message = Message & ViewType.GetDataRow(k)(1).ToString
            End If

        Next
        RemplirGeneral()
        RemplirType()
        If (Message <> "") Then
            SuccesMsg("Les comptes " & Message & " sont en cours d'utilisation.")
        End If

    End Sub

    Private Sub BtMajRessources_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtMajRessources.Click

        Dim Message As String = ""
        Dim nbTotLigneAffectee As Decimal = 0
        For k As Integer = 0 To ViewType.RowCount - 1

            Dim nbCompte As Decimal = 0

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
            SuccesMsg("Les comptes " & Message & " ont été mis à jour avec succès." & vbNewLine & MontantLettre(nbTotLigneAffectee) & " (" & nbTotLigneAffectee & ") lignes affectées.")
        Else
            SuccesMsg("0 lignes affectées.")
        End If

    End Sub
End Class