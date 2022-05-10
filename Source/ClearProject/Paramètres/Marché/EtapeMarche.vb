Imports MySql.Data.MySqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class EtapeMarche
    Dim CodeMethode As String()
    Dim DrX As DataRow
    Dim ChangerDirection As Boolean = False
    Dim PourModif As Decimal = 0
    Dim LigneSelect As Decimal = 0

    Private Sub EtapeMarche_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        LoadTypeMarche()
        GridEtape.DataSource = Nothing
    End Sub

    Private Sub LoadTypeMarche()
        query = "select TypeMarche from T_TypeMarche order by TypeMarche"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        CmbTypeMarche.Properties.Items.Clear()
        CmbTypeMarche.Text = ""
        For Each rw As DataRow In dt.Rows
            CmbTypeMarche.Properties.Items.Add(MettreApost(rw("TypeMarche").ToString))
        Next
    End Sub

    Private Sub LoadMethode(TypeMarche As String)
        query = "SELECT AbregeAO,CodeProcAO FROM t_procao WHERE TypeMarcheAO='" & EnleverApost(TypeMarche) & "' AND CodeProjet='" & ProjetEnCours & "' ORDER BY AbregeAO"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        ReDim CodeMethode(dt.Rows.Count)
        Dim i As Integer = 0
        cmbMethode.ResetText()
        cmbMethode.Properties.Items.Clear()
        For Each rw As DataRow In dt.Rows
            cmbMethode.Properties.Items.Add(MettreApost(rw("AbregeAO").ToString))
            CodeMethode(i) = rw("CodeProcAO")
            i += 1
        Next
    End Sub
    Private Sub InitForm()
        txtIntitule.Text = ""
        RdPriori.Checked = False
        RdPosteriori.Checked = False
        ChangerDirection = False
        PourModif = 0
        cmbDelaiMesure.SelectedIndex = 0
        txtDelaiValue.Value = 1
        LigneSelect = 0
    End Sub

    Private Sub ChargerEtape(Optional IdMethode As Decimal = 0) 'IdMethode

        Dim dtEtape = New DataTable()
        dtEtape.Columns.Clear()
        dtEtape.Columns.Add("Code", Type.GetType("System.String"))
        dtEtape.Columns.Add("Ref", Type.GetType("System.String"))
        dtEtape.Columns.Add("N°", Type.GetType("System.Int32"))
        dtEtape.Columns.Add("Intitulé", Type.GetType("System.String"))
        dtEtape.Columns.Add("Délai", Type.GetType("System.String"))
        dtEtape.Columns.Add("Priori", Type.GetType("System.Boolean"))
        dtEtape.Columns.Add("Posteriori", Type.GetType("System.Boolean"))
        dtEtape.Rows.Clear()

        ' Dim Keys(0) As DataColumn
        ' Keys(0) = dtEtape.Columns("Ref")
        ' dtEtape.PrimaryKey = Keys 'Definir une cle primaire pour le datatable pour utiliser LoadDataRow()
        ' dtEtape.DefaultView.Sort = "N° ASC"
        ' GridEtape.DataSource = dtEtape
        ' If ViewEtape.Columns("Code").Visible = True Then
        ' End If
        ' Dim dtEtape As DataTable = GridEtape.DataSource

        If IdMethode = 0 Then
            query = "select * from T_EtapeMarche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='" & EnleverApost(CmbTypeMarche.Text) & "' ORDER BY NumeroOrdre ASC"
        Else
            query = "select E.* from T_EtapeMarche as E, t_liaisonetape as L where L.RefEtape=E.RefEtape and E.CodeProjet='" & ProjetEnCours & "' and E.TypeMarche='" & EnleverApost(CmbTypeMarche.Text) & "' AND L.CodeProcAO='" & IdMethode & "' order by E.NumeroOrdre ASC"
        End If

        Dim dt As DataTable = ExcecuteSelectQuery(query)

        'Chargement des numéros d'ordre des étapes
        'cmbNumOrdre.Properties.Items.Clear()
        'cmbNumOrdre.ResetText()

        'For i = 0 To dt.Rows.Count
        '    cmbNumOrdre.Properties.Items.Add((i + 1).ToString())
        'Next
        'dtEtape.BeginLoadData()

        Dim cpt As Decimal = 0
        For Each rw As DataRow In dt.Rows
            cpt += 1
            ' Dim drS() As Object = {"", "", "", "", "", "", ""}
            Dim drS = dtEtape.NewRow()

            drS("Code") = IIf(cpt Mod 2, "x", "").ToString
            drS("Ref") = rw("RefEtape")
            drS("N°") = rw("NumeroOrdre")
            drS("Intitulé") = MettreApost(rw("TitreEtape").ToString)
            drS("Délai") = rw("DelaiEtape")

            If rw("Priori").ToString() = "OUI" Then
                drS("Priori") = True
            Else
                drS("Priori") = False
            End If

            If rw("Posteriori").ToString() = "OUI" Then
                drS("Posteriori") = True
            Else
                drS("Posteriori") = False
            End If
            ' dtEtape.LoadDataRow(drS, True)
            dtEtape.Rows.Add(drS)
        Next

        ' dtEtape.EndLoadData()
        GridEtape.DataSource = dtEtape
        ViewEtape.Columns("Ref").Visible = False
        ViewEtape.Columns("Code").Visible = False
        ViewEtape.Columns("N°").MaxWidth = 30
        ViewEtape.Columns("Délai").MaxWidth = 50
        ViewEtape.Columns("Priori").MaxWidth = 50
        ViewEtape.Columns("Posteriori").MaxWidth = 50
        ViewEtape.OptionsView.ColumnAutoWidth = True
        ViewEtape.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ViewEtape.Columns("Posteriori").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        ViewEtape.Columns("Priori").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        ViewEtape.OptionsView.ColumnAutoWidth = True
        ViewEtape.Columns("N°").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ColorRowGrid(ViewEtape, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
    End Sub

    Private Sub BtNewEtape_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtNewEtape.Click
        'If (cmbMethode.SelectedIndex > -1 And CmbTypeMarche.SelectedIndex <> -1) Then
        If (CmbTypeMarche.SelectedIndex <> -1) Then
            InitForm()
            GbNewEtape.Visible = True
            ' cmbNumOrdre.SelectedIndex = cmbNumOrdre.Properties.Items.Count - 1
            'txtIntitule.Select()
        Else
            SuccesMsg("Veuillez sélectionner un type de marché.") 'et une méthode.")
        End If
    End Sub
    Private Sub BtRetour_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtRetour.Click
        InitForm()
        GbNewEtape.Visible = False
    End Sub

    Private Sub GridEtape_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEtape.Click

        If (ViewEtape.RowCount > 0) Then
            DrX = ViewEtape.GetDataRow(ViewEtape.FocusedRowHandle)
            Dim CodEtap As String = DrX("Ref").ToString
            ColorRowGrid(ViewEtape, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewEtape, "[Ref]='" & CodEtap & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)
        End If

    End Sub

    Private Sub GridEtape_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEtape.DoubleClick
        On Error Resume Next
        If (ViewEtape.RowCount > 0) Then
            If ChangerDirection = True Then
                SuccesMsg("Veuillez enregistrer les modifications en cours")
                Exit Sub
            End If

            DrX = ViewEtape.GetDataRow(ViewEtape.FocusedRowHandle)
            LigneSelect = ViewEtape.FocusedRowHandle
            Dim CodEtap As String = DrX("Ref").ToString
            ColorRowGrid(ViewEtape, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewEtape, "[Ref]='" & CodEtap & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)

            GbNewEtape.Visible = True
            PourModif = DrX("Ref").ToString
            txtIntitule.Text = DrX("Intitulé").ToString
            ' cmbNumOrdre.Text = DrX("N°").ToString()
            If DrX("Priori") = True Then
                RdPriori.Checked = True
            Else
                RdPriori.Checked = False
            End If
            If DrX("Posteriori") = True Then
                RdPosteriori.Checked = True
            Else
                RdPosteriori.Checked = False
            End If

            If DrX("Délai").ToString() = "DAO" Then
                rdDelaiDAO.Checked = True
            Else
                rdDelai.Checked = True
                txtDelaiValue.Value = Val(Split(DrX("Délai").ToString(), " ")(0))
                cmbDelaiMesure.Text = Split(DrX("Délai").ToString(), " ")(1)
            End If
        End If

    End Sub

    Private Sub UpdateNumeroOrdre(TypeMarche As String, CodeMethode As String, Numéro As Integer, Optional OldNuméro As Integer = -1, Optional Signe As String = "+")
        If Signe = "+" Then
            If OldNuméro = -1 Then
                query = "UPDATE t_etapemarche SET NumeroOrdre=NumeroOrdre" & Signe & "1 WHERE CodeProjet='" & ProjetEnCours & "' AND TypeMarche='" & EnleverApost(TypeMarche) & "' AND CodeProcAO='" & CodeMethode & "' AND NumeroOrdre>='" & Numéro & "'"
            Else
                If Numéro < OldNuméro Then
                    query = "UPDATE t_etapemarche SET NumeroOrdre=NumeroOrdre" & Signe & "1 WHERE CodeProjet='" & ProjetEnCours & "' AND TypeMarche='" & EnleverApost(TypeMarche) & "' AND CodeProcAO='" & CodeMethode & "' AND NumeroOrdre<'" & OldNuméro & "' AND NumeroOrdre>='" & Numéro & "'"
                ElseIf Numéro >= OldNuméro Then
                    query = "UPDATE t_etapemarche SET NumeroOrdre=NumeroOrdre-1 WHERE CodeProjet='" & ProjetEnCours & "' AND TypeMarche='" & EnleverApost(TypeMarche) & "' AND CodeProcAO='" & CodeMethode & "' AND NumeroOrdre<='" & Numéro & "' AND NumeroOrdre>'" & OldNuméro & "'"
                End If
            End If
        Else
            query = "UPDATE t_etapemarche SET NumeroOrdre=NumeroOrdre" & Signe & "1 WHERE CodeProjet='" & ProjetEnCours & "' AND TypeMarche='" & EnleverApost(TypeMarche) & "' AND CodeProcAO='" & CodeMethode & "' AND NumeroOrdre>'" & OldNuméro & "'"
        End If
        ExecuteNonQuery(query)
    End Sub

    Private Sub BtEnreg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnreg.Click

        'Vérification des données
        If ChangerDirection = False Then

            If (txtIntitule.Text.Trim().Length = 0) Then
                SuccesMsg("Veuillez saisir l'intitulé.")
                txtIntitule.Select()
                Exit Sub
            End If
            'If cmbNumOrdre.SelectedIndex = -1 Then
            '    SuccesMsg("Veuillez choisir un numéro d'ordre.")
            '    cmbNumOrdre.Select()
            '    Exit Sub
            'End If

            If (RdPriori.Checked = False And RdPosteriori.Checked = False) Then
                SuccesMsg("Veuillez définir à quelle revue l'étape appartient.")
                Exit Sub
            End If

            If (rdDelai.Checked = False And rdDelaiDAO.Checked = False) Then
                SuccesMsg("Veuillez définir la durée de l'étape.")
                Exit Sub
            End If

            If rdDelai.Checked Then
                If txtDelaiValue.Value = 0 Then
                    SuccesMsg("Veuillez définir la durée de l'étape.")
                    txtDelaiValue.Select()
                    Exit Sub
                End If
            End If
        End If

        Dim NumOrdre As Integer = 0

        If (PourModif = 0) Then   'Nouvel enregistrement

            'Verifier si le libelle de l'etape n'existe pas  ******************************************
            If Val(ExecuteScallar("select count(*) from T_EtapeMarche where CodeProjet='" & ProjetEnCours & "' and TitreEtape='" & EnleverApost(txtIntitule.Text) & "' and TypeMarche='" & EnleverApost(CmbTypeMarche.Text) & "'")) > 0 Then 'AND CodeProcAO='" & CodeMethode(cmbMethode.SelectedIndex) & "'")
                SuccesMsg("Cette étape existe déjà.")
                Exit Sub
            End If

            'numero d'ordre
            NumOrdre = Val(ExecuteScallar("select count(*) from T_EtapeMarche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='" & EnleverApost(CmbTypeMarche.Text) & "'")) ' AND CodeProcAO='" & CodeMethode(cmbMethode.SelectedIndex) & "'

            'Mise à jour de l'ordre des étapes avant d'ajouter la nouvelle
            ' UpdateNumeroOrdre(CmbTypeMarche.Text, CodeMethode(cmbMethode.SelectedIndex), Val(cmbNumOrdre.Text))

            ExecuteNonQuery("INSERT INTO T_EtapeMarche VALUES(NULL,'" & ProjetEnCours & "','" & NumOrdre + 1 & "','" & EnleverApost(CmbTypeMarche.Text) & "','" & EnleverApost(txtIntitule.Text) & "','" & IIf(RdPriori.Checked, "OUI", "NON") & "','" & IIf(RdPosteriori.Checked, "OUI", "NON") & "','NON','NON','NON','NON','NON','NON','NON',NULL,'" & IIf(rdDelai.Checked, txtDelaiValue.Value & " " & cmbDelaiMesure.Text, "DAO") & "')") ''" & CodeMethode(cmbMethode.SelectedIndex) & "'
            SuccesMsg("Enregistrement effectué avec succès")

        ElseIf PourModif > 0 Then ' Modification d'un enregistrement
            'Mise à jour de l'ordre des étapes avant de passer à la modification
            'DrX = ViewEtape.GetDataRow(ViewEtape.FocusedRowHandle)
            ' UpdateNumeroOrdre(CmbTypeMarche.Text, CodeMethode(cmbMethode.SelectedIndex), Val(cmbNumOrdre.Text), DrX("N°"))
            DebutChargement(True, "Modification en cours...")
            If GetVerifierUtiliser(PourModif) = True Then
                ExecuteNonQuery("UPDATE t_etapemarche SET TitreEtape='" & EnleverApost(txtIntitule.Text) & "' WHERE RefEtape='" & PourModif & "'")
                FinChargement()
                SuccesMsg("Cette étape est déjà en cours d'utilisation." & vbNewLine & "Seul l'intitulé a été modifié.")
            Else
                ' ExecuteNonQuery("UPDATE t_etapemarche SET NumeroOrdre='" & cmbNumOrdre.Text & "', TitreEtape='" & EnleverApost(txtIntitule.Text.Trim()) & "', Priori='" & IIf(RdPriori.Checked, "OUI", "NON") & "', Posteriori='" & IIf(RdPosteriori.Checked, "OUI", "NON") & "', DelaiEtape='" & IIf(rdDelai.Checked, txtDelaiValue.Value & " " & cmbDelaiMesure.Text, "DAO") & "' WHERE RefEtape='" & PourModif & "'")
                ExecuteNonQuery("UPDATE t_etapemarche SET TitreEtape='" & EnleverApost(txtIntitule.Text) & "', Priori='" & IIf(RdPriori.Checked, "OUI", "NON") & "', Posteriori='" & IIf(RdPosteriori.Checked, "OUI", "NON") & "', DelaiEtape='" & IIf(rdDelai.Checked, txtDelaiValue.Value & " " & cmbDelaiMesure.Text, "DAO") & "' WHERE RefEtape='" & PourModif & "'")
                FinChargement()
                SuccesMsg("Modification effectuée avec succès.")
            End If
            GridEtape_Click(GridEtape, New EventArgs)

        ElseIf ChangerDirection = True Then
            If ViewEtape.RowCount > 0 Then
                DebutChargement(True, "Enregistrement en cours...")
                '  ExecuteNonQuery("DELETE FROM T_EtapeMarche WHERE CodeProjet='" & ProjetEnCours & "' and TypeMarche='" & EnleverApost(CmbTypeMarche.Text) & "' and CodeProcAO='" & CodeMethode(cmbMethode.SelectedIndex) & "'")
                For i = 0 To ViewEtape.RowCount - 1
                    'ExecuteNonQuery("INSERT INTO T_EtapeMarche VALUES(NULL,'" & ProjetEnCours & "','" & ViewEtape.GetRowCellValue(i, "N°") & "','" & EnleverApost(CmbTypeMarche.Text) & "','" & EnleverApost(ViewEtape.GetRowCellValue(i, "Intitulé").ToString) & "','" & IIf(ViewEtape.GetRowCellValue(i, "Priori") = True, "OUI", "NON") & "','" & IIf(ViewEtape.GetRowCellValue(i, "Posteriori") = True, "OUI", "NON") & "','NON','NON','NON','NON','NON','NON','NON','" & CodeMethode(cmbMethode.SelectedIndex) & "','" & EnleverApost(ViewEtape.GetRowCellValue(i, "Délai").ToString) & "')")
                    ExecuteNonQuery("UPDATE T_EtapeMarche SET NumeroOrdre='" & ViewEtape.GetRowCellValue(i, "N°") & "' WHERE RefEtape='" & ViewEtape.GetRowCellValue(i, "Ref") & "' and CodeProjet='" & ProjetEnCours & "'")
                Next
                FinChargement()
                SuccesMsg("Enregistrement effectué avec succès.")
            End If
        End If
        If cmbMethode.SelectedIndex <> -1 Then
            ChargerEtape(CodeMethode(cmbMethode.SelectedIndex))
        Else
            ChargerEtape()
        End If
        InitForm()

    End Sub

    Private Function GetVerifierUtiliser(ByVal RefEtape As Decimal) As Boolean
        Try
            Dim CodeProcAO As String = ExecuteScallar("select CodeProcAO from t_etapemarche where RefEtape='" & RefEtape & "'")

            If (Val(ExecuteScallar("select count(*) from t_marche where CodeProcAO='" & CodeProcAO & "'")) > 0) Then
                Return True
            End If

            If (Val(ExecuteScallar("select count(*) from T_PlanMarche where RefEtape='" & RefEtape & "'")) > 0) Then
                Return True
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        Return False
    End Function

    Private Sub SupprimerEtape_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SupprimerEtape.Click

        If (ViewEtape.RowCount > 0) Then
            If PourModif > 0 Then
                SuccesMsg("Veuillez enregistrer les modifications en cours.")
                Exit Sub
            End If

            DrX = ViewEtape.GetDataRow(ViewEtape.FocusedRowHandle)
            Dim CodEtap As String = DrX("Ref").ToString
            ColorRowGrid(ViewEtape, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewEtape, "[Ref]='" & CodEtap & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)

            If GetVerifierUtiliser(CodEtap) = True Then
                FailMsg("Impossible de supprimer une étape déjà en cours d'utilisation.")
                Exit Sub
            End If

            If ConfirmMsg("Voulez-vous supprimer " & vbNewLine & "<<" & DrX("Intitulé").ToString & ">> ?") = DialogResult.Yes Then

                'Mise à jour de l'ordre des étapes avant de passer à la suppression
                ' UpdateNumeroOrdre(CmbTypeMarche.Text, CodeMethode(cmbMethode.SelectedIndex), DrX("N°"), DrX("N°"), "-")
                ExecuteNonQuery("UPDATE t_etapemarche SET NumeroOrdre=NumeroOrdre-1 WHERE CodeProjet='" & ProjetEnCours & "' AND TypeMarche='" & EnleverApost(CmbTypeMarche.Text) & "' AND NumeroOrdre>'" & CInt(DrX("N°")) & "'") 'CodeProcAO='" & CodeMethode(cmbMethode.SelectedIndex) & "'
                ExecuteNonQuery("DELETE from T_EtapeMarche where RefEtape='" & CodEtap & "'")
                ExecuteNonQuery("DELETE from t_liaisonetape where RefEtape='" & CodEtap & "' and CodeProjet='" & ProjetEnCours & "'")
                SuccesMsg("Suppression effectuée avec succès")

                ViewEtape.GetDataRow(ViewEtape.FocusedRowHandle).Delete()
                If cmbMethode.SelectedIndex <> -1 Then
                    ChargerEtape(CodeMethode(cmbMethode.SelectedIndex))
                Else
                    ChargerEtape()
                End If
            End If
        End If
    End Sub

    'Private Sub BtMonter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtMonter.Click

    '    If (ViewEtape.RowCount > 0) Then
    '        If (ViewEtape.FocusedRowHandle > 0) Then
    '            DeplacerEtape()
    '        Else
    '            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
    '        End If
    '    End If

    'End Sub

    'Private Sub BtDescendre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtDescendre.Click

    '    If (ViewEtape.RowCount > 0) Then
    '        If (ViewEtape.FocusedRowHandle < ViewEtape.RowCount - 1) Then
    '            DeplacerEtape("Descendre")
    '        Else
    '            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
    '        End If
    '    End If

    'End Sub

    'Private Sub DeplacerEtape(Optional ByVal Sens As String = "Monter")

    '    If (ViewEtape.RowCount > 0) Then

    '        Dim PosActuelle As Decimal = ViewEtape.FocusedRowHandle + 1
    '        DrX = ViewEtape.GetDataRow(ViewEtape.FocusedRowHandle)
    '        Dim CodEtap As String = DrX(1).ToString

    '        query = "select * from T_PlanMarche as P,T_Marche as M where P.RefMarche=M.RefMarche and M.TypeMarche='" & CmbTypeMarche.Text & "' and M.CodeProjet='" & ProjetEnCours & "'"
    '        Dim dt As DataTable = ExcecuteSelectQuery(query)
    '        If dt.Rows.Count > 0 Then
    '            MsgBox("Enregistrement en cours d'utilisation!", MsgBoxStyle.Exclamation)
    '            Exit Sub
    '        End If

    '        Dim LeKod As String = ViewEtape.GetRow(ViewEtape.FocusedRowHandle)(1).ToString
    '        query = "select * from T_PlanMarche where RefEtape='" & LeKod & "'"
    '        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
    '        If dt1.Rows.Count > 0 Then
    '            MsgBox("Enregistrement en cours d'utilisation!", MsgBoxStyle.Exclamation)
    '            Exit Sub
    '        End If

    '        Dim newPos As Decimal = ViewEtape.FocusedRowHandle
    '        Dim tampPos As Decimal = 0
    '        If (Sens = "Monter") Then

    '            query = "update T_EtapeMarche set NumeroOrdre='" & tampPos.ToString & "' where TypeMarche='" & CmbTypeMarche.Text & "' and NumeroOrdre='" & PosActuelle.ToString & "' and CodeProjet='" & ProjetEnCours & "'"
    '            ExecuteNonQuery(query)

    '            query = "update T_EtapeMarche set NumeroOrdre='" & PosActuelle.ToString & "' where TypeMarche='" & CmbTypeMarche.Text & "' and NumeroOrdre='" & (PosActuelle - 1).ToString & "' and CodeProjet='" & ProjetEnCours & "'"
    '            ExecuteNonQuery(query)

    '            query = "update T_EtapeMarche set NumeroOrdre='" & (PosActuelle - 1).ToString & "' where TypeMarche='" & CmbTypeMarche.Text & "' and NumeroOrdre='" & tampPos.ToString & "' and CodeProjet='" & ProjetEnCours & "'"
    '            ExecuteNonQuery(query)

    '            newPos = ViewEtape.FocusedRowHandle - 1

    '        Else

    '            query = "update T_EtapeMarche set NumeroOrdre='" & tampPos.ToString & "' where TypeMarche='" & CmbTypeMarche.Text & "' and NumeroOrdre='" & PosActuelle.ToString & "' and CodeProjet='" & ProjetEnCours & "'"
    '            ExecuteNonQuery(query)

    '            query = "update T_EtapeMarche set NumeroOrdre='" & PosActuelle.ToString & "' where TypeMarche='" & CmbTypeMarche.Text & "' and NumeroOrdre='" & (PosActuelle + 1).ToString & "' and CodeProjet='" & ProjetEnCours & "'"
    '            ExecuteNonQuery(query)

    '            query = "update T_EtapeMarche set NumeroOrdre='" & (PosActuelle + 1).ToString & "' where TypeMarche='" & CmbTypeMarche.Text & "' and NumeroOrdre='" & tampPos.ToString & "' and CodeProjet='" & ProjetEnCours & "'"
    '            ExecuteNonQuery(query)

    '            newPos = ViewEtape.FocusedRowHandle + 1

    '        End If

    '        ChargerEtape()
    '        ColorRowGrid(ViewEtape, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
    '        ColorRowGridAnal(ViewEtape, "[Ref]='" & CodEtap & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)
    '        ViewEtape.FocusedRowHandle = newPos
    '    End If

    'End Sub

    Private Sub BtImpEtape_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtImpEtape.Click
        If Not Access_Btn("BtnPrintLstTEtapesMarche") Then
            Exit Sub
        End If

        If CmbTypeMarche.IsRequiredControl("Veuillez sélectionner le type de marché à imprimer.") Then
            CmbTypeMarche.Select()
            Exit Sub
        End If

        If cmbMethode.IsRequiredControl("Veuillez sélectionner une méthode.") Then
            cmbMethode.Select()
            Exit Sub
        End If

        'If (CmbTypeMarche.Text <> "") Then

        '    Dim Cmd As MySqlCommand
        '    query = "Truncate T_TampEtapeListe"
        '    ExecuteNonQuery(query)

        '    query = "Truncate T_TampEtapeMethode"
        '    ExecuteNonQuery(query)

        '    Dim LesCodeMet(10) As String
        '    Dim LesMethod(10) As String
        '    Dim LesDelais(10) As Decimal
        '    Dim nbMethod As Decimal = 0

        '    query = "select AbregeAO, CodeProcAO from T_ProcAO where TypeMarcheAO='" & CmbTypeMarche.Text & "' and CodeProjet='" & ProjetEnCours & "' order by AbregeAO"
        '    Dim dt As DataTable = ExcecuteSelectQuery(query)
        '    For Each rw0 As DataRow In dt.Rows
        '        LesMethod(nbMethod) = rw0(0).ToString
        '        LesCodeMet(nbMethod) = rw0(1).ToString
        '        nbMethod += 1
        '    Next

        '    Dim LigneEtape(50, 12) As String
        '    For i As Integer = 0 To 49
        '        For j As Integer = 0 To 11
        '            LigneEtape(i, j) = ""
        '        Next
        '    Next

        '    Dim nbLigNe As Integer = 0
        '    query = "select RefEtape, NumeroOrdre, TitreEtape from T_EtapeMarche where TypeMarche='" & CmbTypeMarche.Text & "' and CodeProjet='" & ProjetEnCours & "' order by NumeroOrdre"
        '    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        '    For Each rw0 As DataRow In dt1.Rows

        '        LigneEtape(nbLigNe, 0) = rw0(1).ToString
        '        LigneEtape(nbLigNe, 1) = MettreApost(rw0(2).ToString)

        '        For n As Decimal = 0 To nbMethod - 1
        '            query = "select DelaiEtape from T_DelaiEtape where RefEtape='" & rw0(0).ToString & "' and CodeProcAO='" & LesCodeMet(n) & "'"
        '            Dim dt2 As DataTable = ExcecuteSelectQuery(query)
        '            If dt2.Rows.Count > 0 Then
        '                For Each rw1 As DataRow In dt2.Rows

        '                    LigneEtape(nbLigNe, n + 2) = rw1(0).ToString
        '                    Dim partDelai() As String = rw1(0).ToString.Split(" "c)
        '                    Dim jrsDelai As Decimal = CInt(partDelai(0))
        '                    If (partDelai(1) = "Semaines") Then
        '                        jrsDelai = jrsDelai * 7
        '                    ElseIf (partDelai(1) = "Mois") Then
        '                        jrsDelai = jrsDelai * 31
        '                    ElseIf (partDelai(1) = "Ans") Then
        '                        jrsDelai = jrsDelai * 365
        '                    End If
        '                    LesDelais(n) += jrsDelai

        '                Next
        '            End If

        '        Next

        '        nbLigNe += 1

        '    Next

        '    '*************************************************************
        '    ' Enregistrement des Methodes ********************************
        Dim DatSet = New DataSet
        '    query = "select * from T_TampEtapeMethode"
        '    Dim sqlconn As New MySqlConnection
        '    BDOPEN(sqlconn)

        '    Cmd = New MySqlCommand(query, sqlconn)
        '    Dim DatAdapt = New MySqlDataAdapter(Cmd)
        '    DatAdapt.Fill(DatSet, "T_TampEtapeMethode")
        '    Dim DatTable = DatSet.Tables("T_TampEtapeMethode")
        '    Dim DatRow = DatSet.Tables("T_TampEtapeMethode").NewRow()

        '    For k As Integer = 0 To (nbMethod - 1)
        '        DatRow("Method" & (k + 1).ToString) = LesMethod(k)
        '        DatRow("Delai" & (k + 1).ToString) = LesDelais(k)
        '    Next

        '    DatSet.Tables("T_TampEtapeMethode").Rows.Add(DatRow) 'ajout d'une nouvelle ligne 
        '    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt) 'execution de l'enregistrement
        '    DatAdapt.Update(DatSet, "T_TampEtapeMethode")
        '    DatSet.Clear()
        '    '**************************************************************

        '    '*************************************************************
        '    ' Enregistrement des etapes***********************************

        '    DatSet = New DataSet
        '    query = "select * from T_TampEtapeListe"
        '    Cmd = New MySqlCommand(query, sqlconn)
        '    DatAdapt = New MySqlDataAdapter(Cmd)
        '    DatAdapt.Fill(DatSet, "T_TampEtapeListe")
        '    DatTable = DatSet.Tables("T_TampEtapeListe")

        '    For i As Integer = 0 To nbLigNe - 1
        '        DatRow = DatSet.Tables("T_TampEtapeListe").NewRow()

        '        DatRow("NumEtape") = LigneEtape(i, 0)
        '        DatRow("LibelleEtape") = LigneEtape(i, 1)

        '        For j As Integer = 0 To nbMethod - 1
        '            DatRow("DelaiM" & (j + 1).ToString) = LigneEtape(i, j + 2)
        '        Next

        '        DatSet.Tables("T_TampEtapeListe").Rows.Add(DatRow) 'ajout d'une nouvelle ligne 
        '    Next

        '    CmdBuilder = New MySqlCommandBuilder(DatAdapt) 'execution de l'enregistrement
        '    DatAdapt.Update(DatSet, "T_TampEtapeListe")
        '    DatSet.Clear()
        '    BDQUIT(sqlconn)
        '    '*************************************************************

        ' Affichage état ***************************
        DebutChargement(True, "Chargement des etapes en cours...")
        Dim reportEtape As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
        Dim Chemin As String = lineEtat & "\Marches\PPM\"

        DatSet = New DataSet
        reportEtape.Load(Chemin & "Etapes_PPM.rpt")

        With crConnectionInfo
            .ServerName = ODBCNAME
            .DatabaseName = DB
            .UserID = USERNAME
            .Password = PWD
        End With

        CrTables = reportEtape.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next
        reportEtape.SetDataSource(DatSet)
        reportEtape.SetParameterValue("CodeProjet", ProjetEnCours)
        reportEtape.SetParameterValue("TypeMarche", EnleverApost(CmbTypeMarche.Text))
        reportEtape.SetParameterValue("Methode", CodeMethode(cmbMethode.SelectedIndex))

        FullScreenReport.FullView.ReportSource = reportEtape
        FinChargement()
        FullScreenReport.ShowDialog()

        'End If
    End Sub


    Private Sub CmbTypeMarche_SelectedValueChanged(sender As Object, e As EventArgs) Handles CmbTypeMarche.SelectedValueChanged
        On Error Resume Next
        Dim dtEtape As DataTable = GridEtape.DataSource
        dtEtape.Rows.Clear()
        InitForm()
        If CmbTypeMarche.SelectedIndex <> -1 Then
            LoadMethode(CmbTypeMarche.Text)
            ChargerEtape()
        Else
            cmbMethode.ResetText()
            cmbMethode.Properties.Items.Clear()
        End If
    End Sub

    Private Sub cmbMethode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMethode.SelectedIndexChanged
        Try
            'On Error Resume Next
            If cmbMethode.SelectedIndex <> -1 Then
                Dim dtEtape As DataTable = GridEtape.DataSource
                dtEtape.Rows.Clear()
                InitForm()
                ChargerEtape(CodeMethode(cmbMethode.SelectedIndex))
            ElseIf CmbTypeMarche.SelectedIndex <> -1 Then
                ChargerEtape()
            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub rdDelaiDAO_CheckedChanged(sender As Object, e As EventArgs) Handles rdDelaiDAO.CheckedChanged
        If CType(sender, DevExpress.XtraEditors.CheckEdit).Checked Then
            txtDelaiValue.Enabled = False
            cmbDelaiMesure.Enabled = False
        End If
    End Sub

    Private Sub rdDelai_CheckedChanged(sender As Object, e As EventArgs) Handles rdDelai.CheckedChanged
        If CType(sender, DevExpress.XtraEditors.CheckEdit).Checked Then
            txtDelaiValue.Enabled = True
            cmbDelaiMesure.Enabled = True
        End If
    End Sub

    Private Sub EtapeMarche_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        CmbTypeMarche.ResetText()
        cmbMethode.ResetText()
        BtRetour.PerformClick()
        CmbTypeMarche.Select()
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If ViewEtape.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub BtMonter_Click(sender As Object, e As EventArgs) Handles BtMonter.Click
        If ViewEtape.RowCount > 0 Then
            If PourModif > 0 Then
                SuccesMsg("Veuillez enregistrer les modifications en cours.")
                Exit Sub
            End If

            If GbNewEtape.Visible = False Then GbNewEtape.Visible = True
            Dim TablRemplcer(4) As String
            Dim NumHaut As Integer
            Dim lefocus As Integer

            DrX = ViewEtape.GetDataRow(ViewEtape.FocusedRowHandle)
            If GetVerifierUtiliser(DrX("Ref")) = True Then
                FailMsg("Impossible de changer l'ordre d'une étape déjà en cours d'utilisation.")
                Exit Sub
            End If

            NumHaut = CInt(DrX("N°").ToString)
            NumHaut = NumHaut - 2
            lefocus = CInt(DrX("N°").ToString) - 1
            If lefocus > 0 Then
                ChangerDirection = True
                PourModif = -1

                TablRemplcer(0) = DrX("Ref")
                TablRemplcer(1) = DrX("Intitulé").ToString
                TablRemplcer(2) = DrX("Délai").ToString
                TablRemplcer(3) = DrX("Priori")
                TablRemplcer(4) = DrX("Posteriori")

                DrX("Ref") = ViewEtape.GetRowCellValue(NumHaut, "Ref")
                DrX("Intitulé") = ViewEtape.GetRowCellValue(NumHaut, "Intitulé").ToString
                DrX("Délai") = ViewEtape.GetRowCellValue(NumHaut, "Délai").ToString
                DrX("Priori") = ViewEtape.GetRowCellValue(NumHaut, "Priori")
                DrX("Posteriori") = ViewEtape.GetRowCellValue(NumHaut, "Posteriori")

                ViewEtape.GetDataRow(NumHaut).Item("Ref") = TablRemplcer(0)
                ViewEtape.GetDataRow(NumHaut).Item("Intitulé") = TablRemplcer(1)
                ViewEtape.GetDataRow(NumHaut).Item("Délai") = TablRemplcer(2)
                ViewEtape.GetDataRow(NumHaut).Item("Priori") = TablRemplcer(3)
                ViewEtape.GetDataRow(NumHaut).Item("Posteriori") = TablRemplcer(4)

                ViewEtape.FocusedRowHandle = NumHaut

                ColorRowGrid(ViewEtape, "[Code]='x'", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.Black)
                ColorRowGridAnal(ViewEtape, "[N°]='" & lefocus.ToString & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
            End If
        End If
    End Sub

    Private Sub BtDescendre_Click(sender As Object, e As EventArgs) Handles BtDescendre.Click
        If ViewEtape.RowCount > 0 Then
            If PourModif > 0 Then
                SuccesMsg("Veuillez enregistrer les modifications en cours.")
                Exit Sub
            End If
            If GbNewEtape.Visible = False Then GbNewEtape.Visible = True

            DrX = ViewEtape.GetDataRow(ViewEtape.FocusedRowHandle)

            'If Val(ExecuteScallar("select count(*) from T_PlanMarche where RefEtape='" & DrX("Ref") & "'")) > 0 Then
            '    FailMsg("Impossible de changer l'ordre d'une étape déjà en cours d'utilisation.")
            '    Exit Sub
            'End If

            If GetVerifierUtiliser(DrX("Ref")) = True Then
                FailMsg("Impossible de changer l'ordre d'une étape déjà en cours d'utilisation.")
                Exit Sub
            End If

            Dim TablRemplcer(4) As String
            Dim NumBas As Integer = 0
            Dim lefocus As Integer = 0

            NumBas = CInt(DrX("N°").ToString)
            lefocus = CInt(DrX("N°").ToString) + 1

            If lefocus <= ViewEtape.RowCount Then
                ChangerDirection = True
                PourModif = -1

                TablRemplcer(0) = DrX("Ref")
                TablRemplcer(1) = DrX("Intitulé").ToString
                TablRemplcer(2) = DrX("Délai").ToString
                TablRemplcer(3) = DrX("Priori")
                TablRemplcer(4) = DrX("Posteriori")

                DrX("Ref") = ViewEtape.GetRowCellValue(NumBas, "Ref")
                DrX("Intitulé") = ViewEtape.GetRowCellValue(NumBas, "Intitulé").ToString
                DrX("Délai") = ViewEtape.GetRowCellValue(NumBas, "Délai").ToString
                DrX("Priori") = ViewEtape.GetRowCellValue(NumBas, "Priori")
                DrX("Posteriori") = ViewEtape.GetRowCellValue(NumBas, "Posteriori")

                ViewEtape.GetDataRow(NumBas).Item("Ref") = TablRemplcer(0)
                ViewEtape.GetDataRow(NumBas).Item("Intitulé") = TablRemplcer(1)
                ViewEtape.GetDataRow(NumBas).Item("Délai") = TablRemplcer(2)
                ViewEtape.GetDataRow(NumBas).Item("Priori") = TablRemplcer(3)
                ViewEtape.GetDataRow(NumBas).Item("Posteriori") = TablRemplcer(4)

                ViewEtape.FocusedRowHandle = NumBas

                ColorRowGrid(ViewEtape, "[Code]='x'", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.Black)
                ColorRowGridAnal(ViewEtape, "[N°]='" & lefocus.ToString & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
            End If
        End If
    End Sub
End Class