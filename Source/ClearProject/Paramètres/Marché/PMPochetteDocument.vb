Imports MySql.Data.MySqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class PMPochetteDocument
    Dim dtDocument = New DataTable()
    Dim DrX As DataRow

    Dim tabPochette As String()
    Private Sub Service_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        InitFormulaire()
        ChargerPochette()
        'CmbPochette_SelectedIndexChanged(sender, e)
        ViewPochetteDocument.OptionsView.ColumnAutoWidth = True
        ViewPochetteDocument.OptionsBehavior.AutoExpandAllGroups = True
        ViewPochetteDocument.VertScrollVisibility = True
        ViewPochetteDocument.HorzScrollVisibility = True
        ViewPochetteDocument.BestFitColumns()
        NewTypePochette(False)
    End Sub

    Private Sub NewTypePochette(value As Boolean)
        If value = False Then
            CmbPochette.Size = New Point(394, 22)
            LabelTextType.Visible = value
            CmbTypepochette.Visible = value
        Else
            CmbPochette.Size = New Point(261, 22)
            LabelTextType.Visible = value
            CmbTypepochette.Visible = value
        End If
    End Sub

    Private Sub ChargerPochette()
        CmbPochette.ResetText()
        query = "select * from `t_typemarche` ORDER BY `CodeTypeMarche` ASC"
        CmbPochette.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        ReDim tabPochette(dt.Rows.Count)
        Dim cpte As Decimal = 0
        For Each rw As DataRow In dt.Rows
            CmbPochette.Properties.Items.Add(MettreApost(rw("TypeMarche").ToString))
            tabPochette(cpte) = rw("CodeTypeMarche")
            cpte += 1
        Next
    End Sub

    Private Sub RemplirDocument(ByVal PochetteID As String)

        dtDocument.Columns.Clear()
        dtDocument.Columns.Add("N°", Type.GetType("System.String"))
        dtDocument.Columns.Add("Ref", Type.GetType("System.String"))
        dtDocument.Columns.Add("Document", Type.GetType("System.String"))

        Dim TypePochette As String = PochetteID
        If CmbTypepochette.Visible = True And CmbTypepochette.SelectedIndex <> -1 Then TypePochette = CmbTypepochette.Text

        Dim cptr As Decimal = 0
        query = "SELECT * FROM `t_pm_pochette_document` where CodeTypeMarche='" & PochetteID & "' and TypePochette='" & TypePochette & "' order by POCHDOC_LIB ASC"
        dtDocument.Rows.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtDocument.NewRow()
            drS("N°") = cptr
            drS("Ref") = rw("POCHDOC_ID").ToString
            drS("Document") = MettreApost(rw("POCHDOC_LIB").ToString)
            dtDocument.Rows.Add(drS)
        Next
        dtPochetteDocument.DataSource = dtDocument

        ViewPochetteDocument.Columns("N°").Visible = True
        ViewPochetteDocument.Columns("Ref").Visible = False
        ViewPochetteDocument.Columns("N°").Width = 30
        ViewPochetteDocument.Columns("Document").Width = 250

        ViewPochetteDocument.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewPochetteDocument, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub
    Private Sub InitFormulaire()
        TxtLibDoc.Text = ""
        CmbTypepochette.Text = ""
        TxtLibDoc.Enabled = True
        BtEnrg.Enabled = True
        BtModif.Enabled = False
        CmbPochette.Enabled = True
        Me.AcceptButton = BtEnrg
    End Sub

    Private Sub CmbPochette_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbPochette.SelectedIndexChanged
        If CmbPochette.SelectedIndex > -1 Then
            If Mid(CmbPochette.Text.Trim.ToLower, 1, 10) = "consultant" Then
                NewTypePochette(True)
                If CmbTypepochette.SelectedIndex <> -1 Then RemplirDocument(tabPochette(CmbPochette.SelectedIndex))
            Else
                NewTypePochette(False)
                RemplirDocument(tabPochette(CmbPochette.SelectedIndex))
            End If
        Else
            RemplirDocument(-1)
            NewTypePochette(False)
        End If
    End Sub

    Private Sub CmbTypepochette_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbTypepochette.SelectedIndexChanged
        If CmbTypepochette.Visible = True And CmbTypepochette.SelectedIndex <> -1 And CmbPochette.SelectedIndex > -1 Then
            RemplirDocument(tabPochette(CmbPochette.SelectedIndex))
        End If
    End Sub

    Private Sub dtPochetteDocument_Click(sender As System.Object, e As System.EventArgs) Handles dtPochetteDocument.Click

        If (ViewPochetteDocument.RowCount > 0) Then
            DrX = ViewPochetteDocument.GetDataRow(ViewPochetteDocument.FocusedRowHandle)
            Dim IDL = DrX("Ref").ToString
            ColorRowGrid(ViewPochetteDocument, "[N°]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewPochetteDocument, "[Ref]='" & IDL & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)

            ' BtModif.Enabled = True
            ' BtEnrg.Enabled = False
            ' CmbPochette.Enabled = False
            ' TxtLibDoc.Text = MettreApost(DrX("Document").ToString)
        End If
    End Sub
    Private Sub BtEnrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnrg.Click
        If CmbPochette.SelectedIndex = -1 Then
            SuccesMsg("Veuillez choisir une pochette svp.")
            CmbPochette.Select()
            Exit Sub
        End If
        If CmbTypepochette.Visible = True And CmbTypepochette.SelectedIndex = -1 Then
            SuccesMsg("Veuillez choisir le type de la pochette.")
            CmbTypepochette.Select()
            Exit Sub
        End If
        If Trim(TxtLibDoc.Text) = "" Or TxtLibDoc.SelectedIndex = -1 Then
            SuccesMsg("Veuillez choisir le nom du document dans la liste")
            TxtLibDoc.Focus()
            Exit Sub
        End If

        Dim TypePochette As String = tabPochette(CmbPochette.SelectedIndex)
        If CmbTypepochette.Visible = True And CmbTypepochette.SelectedIndex <> -1 Then TypePochette = CmbTypepochette.Text

        If Val(ExecuteScallar("SELECT COUNT(*) from t_pm_pochette_document where POCHDOC_LIB='" & EnleverApost(Trim(TxtLibDoc.Text)) & "' AND CodeTypeMarche='" & tabPochette(CmbPochette.SelectedIndex) & "' and TypePochette='" & TypePochette & "'")) > 0 Then
            FailMsg("Ce document existe déjà")
            Exit Sub
        End If

        Try
            ExecuteNonQuery("INSERT INTO t_pm_pochette_document VALUES(NULL,'" & EnleverApost(Trim(TxtLibDoc.Text)) & "','" & tabPochette(CmbPochette.SelectedIndex) & "', '" & TypePochette & "')")
            SuccesMsg("Document enregistré avec succès.")
            ' InitFormulaire()
            TxtLibDoc.Text = ""
            CmbPochette_SelectedIndexChanged(sender, e)
        Catch my As MySqlException
            SuccesMsg("Erreur : Imformation non disponible." & vbNewLine & my.ToString())
        Catch ex As Exception
            SuccesMsg("L'enregistrement à échoué!")
        End Try

    End Sub

    Private Sub BtModif_Click(sender As System.Object, e As System.EventArgs) Handles BtModif.Click, ModifierService.Click
        If Trim(TxtLibDoc.Text) = "" Then
            SuccesMsg("Veuillez entrer le nom du document svp.")
            Exit Sub
        End If
        Try
            DrX = ViewPochetteDocument.GetDataRow(ViewPochetteDocument.FocusedRowHandle)
            query = "Update t_pm_pochette_document set POCHDOC_LIB='" & EnleverApost(Trim(TxtLibDoc.Text)) & "' where POCHDOC_ID=" & DrX(1)
            ExecuteNonQuery(query)
            InitFormulaire()
            CmbPochette_SelectedIndexChanged(sender, e)

        Catch my As MySqlException
            FailMsg("Erreur : Information non disponible : " & my.Message)
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub

    Private Sub SupprimerServiceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupprimerServiceToolStripMenuItem.Click
        If ViewPochetteDocument.RowCount > 0 Then
            DrX = ViewPochetteDocument.GetDataRow(ViewPochetteDocument.FocusedRowHandle)
            Dim IDL = DrX("Ref").ToString
            ColorRowGrid(ViewPochetteDocument, "[N°]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewPochetteDocument, "[Ref]='" & IDL & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)

            'Verifier s'il exit un document sur cette pochette
            If Val(ExecuteScallar("SELECT COUNT(*) from t_pm_pochette_bailleur where POCHDOC_ID='" & DrX("Ref") & "'")) > 0 Then
                FailMsg("Impossible de supprimer ce document")
                Exit Sub
            End If

            If ConfirmMsg("Voulez-vous continuer supprimer [" & DrX("Document").ToString & "] ?") = DialogResult.Yes Then
                ExecuteNonQuery("delete from t_pm_pochette_document where POCHDOC_ID='" & DrX("Ref") & "'")
                ' InitFormulaire()
                'RemplirDocument(tabPochette(CmbPochette.SelectedIndex))
                'CmbPochette_SelectedIndexChanged(sender, e)
                SuccesMsg("Document supprimé avec succès.")
                ViewPochetteDocument.GetDataRow(ViewPochetteDocument.FocusedRowHandle).Delete()
                If ViewPochetteDocument.RowCount > 0 Then
                    For i = 0 To ViewPochetteDocument.RowCount - 1
                        ViewPochetteDocument.SetRowCellValue(i, "N°", i + 1)
                    Next
                End If
            End If

        Else
            SuccesMsg("Suppression Impossible !")
        End If
    End Sub

    Private Sub Service_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        InitFormulaire()
    End Sub
    Private Sub Service_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        CmbPochette.Focus()
    End Sub

    Private Sub btRetour_Click(sender As Object, e As EventArgs) Handles btRetour.Click
        InitFormulaire()
        ' NewTypePochette(False)
        CmbPochette.Text = ""
        CmbPochette.SelectedIndex = -1
        CmbPochette_SelectedIndexChanged(sender, e)
    End Sub

End Class