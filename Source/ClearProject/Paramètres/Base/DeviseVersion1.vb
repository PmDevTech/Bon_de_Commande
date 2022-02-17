Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Data.DataSet
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraTreeList

Public Class DeviseVersion1

    Dim Nouvo As Boolean = False
    Dim PourAjout As Boolean = False
    Dim PourModif As Boolean = False
    Dim TypeModif As Char = ""
    Dim CodeDevise As String = ""
    Dim RefTauxDevise As String = ""
    Dim TabDevise As String()

    Dim DrX As DataRow

    Private Sub DeviseVersion1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        GetVisibleCode("Code")
        Initiliser()
        ' ChargerDevise()
        LoadDevise()
    End Sub

    Private Sub GetVisibleCode(ByVal Champ As String)
        If Champ.ToString = "Code" Then
            Code.Visible = True
            CmbDevise.Visible = False
        Else
            Code.Visible = False
            CmbDevise.Visible = True
        End If
    End Sub

    Private Sub RemplirComboDevise()
        Try
            Dim dt As DataTable = ExcecuteSelectQuery("select CodeDevise, AbregeDevise from t_devisev1 where CodeProjet='" & ProjetEnCours & "'")
            ReDim TabDevise(dt.Rows.Count)
            Dim i As Integer = 0
            CmbDevise.Properties.Items.Clear()
            For Each rw In dt.Rows
                TabDevise(i) = rw("CodeDevise")
                i += 1
                CmbDevise.Properties.Items.Add(MettreApost(rw("AbregeDevise").ToString))
            Next
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub
    Private Sub Initiliser()
        CmbDevise.Text = ""
        Code.Text = ""
        Nom.Text = ""
        Taux.Text = ""
        TxtDate.Text = ""
        TypeModif = ""
    End Sub

    Private Sub NewRealOnly(ByVal value As Boolean)
        CmbDevise.Properties.ReadOnly = value
        Code.Properties.ReadOnly = value
        Nom.Properties.ReadOnly = value
        Taux.Properties.ReadOnly = value
        TxtDate.Properties.ReadOnly = value
    End Sub
    Private Sub ChargerDevise(Optional CodeDevise As String = "")
        Dim dtDevise As New DataTable()
        dtDevise.Columns.Clear()
        'dtDevise.Columns.Add("CodeX", Type.GetType("System.String"))
        'dtDevise.Columns.Add("CodeDevise", Type.GetType("System.String"))
        'dtDevise.Columns.Add("RefTauxDevise", Type.GetType("System.String"))
        'dtDevise.Columns.Add("TauxUtiliser", Type.GetType("System.String"))
        'dtDevise.Columns.Add("Code", Type.GetType("System.String"))
        'dtDevise.Columns.Add("Nom", Type.GetType("System.String"))
        'dtDevise.Columns.Add("Taux", Type.GetType("System.String"))
        'dtDevise.Columns.Add("Date", Type.GetType("System.String"))
        dtDevise.Rows.Clear()

        Try
            If CodeDevise.ToString = "" Then
                query = "select d.CodeDevise, d.LibelleDevise, d.AbregeDevise, t.RefTauxDevise, t.TauxDevise, t.DateTaux, t.TauxUtiliser from t_devisev1 As d, t_tauxdevise as t where t.CodeDevise=d.CodeDevise And d.CodeProjet='" & ProjetEnCours & "' ORDER BY t.DateTaux DESC"
            Else
                query = "select d.CodeDevise, d.LibelleDevise, d.AbregeDevise, t.RefTauxDevise, t.TauxDevise, t.DateTaux, t.TauxUtiliser from t_devisev1 as d, t_tauxdevise as t where t.CodeDevise=d.CodeDevise and d.CodeDevise='" & CodeDevise & "' and d.CodeProjet='" & ProjetEnCours & "' ORDER BY t.DateTaux DESC"
            End If
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            Dim Nbre As Integer = 0
            For Each rw In dt.Rows
                Dim drs = dtDevise.NewRow()
                Nbre += 1
                drs("CodeX") = If(Nbre Mod 2 = 0, "x", "").ToString
                drs("CodeDevise") = rw("CodeDevise")
                drs("RefTauxDevise") = rw("RefTauxDevise")
                drs("TauxUtiliser") = rw("TauxUtiliser").ToString
                drs("Code") = MettreApost(rw("AbregeDevise").ToString)
                drs("Taux") = AfficherMonnaie(rw("TauxDevise").ToString)
                drs("Nom") = MettreApost(rw("LibelleDevise").ToString)
                If CodeDevise <> "" Then ReponseDialog = MettreApost(rw("LibelleDevise").ToString)
                drs("Date") = CDate(rw("DateTaux")).ToShortDateString
                dtDevise.Rows.Add(drs)
            Next

            GridDevise.DataSource = dtDevise
            ViewDevise.Columns("CodeX").Visible = False
            ViewDevise.Columns("CodeDevise").Visible = False
            ViewDevise.Columns("RefTauxDevise").Visible = False
            ViewDevise.Columns("TauxUtiliser").Visible = False
            ViewDevise.Columns("Code").Width = 100
            ViewDevise.Columns("Taux").Width = 100
            ViewDevise.Columns("Date").Width = 105
            ViewDevise.Columns("Nom").Width = 300
            ViewDevise.Columns("Taux").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ViewDevise.Columns("Date").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ColorRowGrid(ViewDevise, "[CodeX]='x'", Color.LightGray, "Tahoma", 10, FontStyle.Regular, Color.Black)
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub LoadDevise(Optional CodeDevise As String = "", Optional parent As TreeListNode = Nothing)
        TreeListDevise.BeginUnboundLoad()
        If Not IsNothing(parent) Then
            parent.Nodes.Clear()
        Else
            TreeListDevise.Nodes.Clear()
        End If

        If CodeDevise = "" And parent Is Nothing Then
            query = "select CodeDevise, LibelleDevise, AbregeDevise from t_devisev1 where CodeProjet='" & ProjetEnCours & "' ORDER BY AbregeDevise ASC"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                Dim rootNode As TreeListNode = TreeListDevise.AppendNode(New Object() {rw("CodeDevise"), "D", "", MettreApost(rw("AbregeDevise").ToString), MettreApost(rw("LibelleDevise").ToString), "", "", "0"}, parent)
                TreeListDevise.AppendNode(New Object() {"", "", "", "", "", "", "", ""}, rootNode)
            Next
        ElseIf CodeDevise <> "" Then
            query = "select d.CodeDevise, d.LibelleDevise, d.AbregeDevise, t.RefTauxDevise, t.TauxDevise, t.DateTaux, t.TauxUtiliser from t_devisev1 as d, t_tauxdevise as t where t.CodeDevise=d.CodeDevise and d.CodeDevise='" & CodeDevise & "' and d.CodeProjet='" & ProjetEnCours & "' ORDER BY t.DateTaux DESC"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rWT In dt1.Rows
                ReponseDialog = MettreApost(rWT("LibelleDevise").ToString)
                Dim rootNode As TreeListNode = TreeListDevise.AppendNode(New Object() {rWT("RefTauxDevise"), "T", rWT("TauxUtiliser").ToString, "", "", AfficherMonnaie(rWT("TauxDevise").ToString), rWT("DateTaux").ToString, "0"}, parent)
            Next
        End If
        TreeListDevise.EndUnboundLoad()
    End Sub

    Private Sub Devise_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub BtNouveau_Click(sender As Object, e As EventArgs) Handles BtNouveau.Click
        GetVisibleCode("Code")
        Initiliser()
        NewRealOnly(False)
        PourModif = False
        Nouvo = True
        PourAjout = False
    End Sub

    Private Sub BtAjout_Click(sender As Object, e As EventArgs) Handles BtAjout.Click
        GetVisibleCode("Combo")
        Initiliser()
        NewRealOnly(False)
        RemplirComboDevise()
        CmbDevise.Text = ""
        PourModif = False
        Nouvo = False
        PourAjout = True
    End Sub

    Private Sub BtRetour_Click(sender As Object, e As EventArgs) Handles BtRetour.Click
        Initiliser()
        NewRealOnly(True)
        PourModif = False
        Nouvo = False
        PourAjout = False
    End Sub

    Private Sub BtnEnregistrer_Click(sender As Object, e As EventArgs) Handles BtnEnregistrer.Click
        If Nom.IsRequiredControl("Veuillez saisir le libelle de la devise") Then
            Nom.Focus()
            Exit Sub
        End If

        If Nouvo = True Or PourAjout = True Then
            If Taux.IsRequiredControl("Veuillez saisir le taux") Then
                Taux.Focus()
                Exit Sub
            End If
            If TxtDate.IsRequiredControl("Veuillez saisir la date") Then
                TxtDate.Focus()
                Exit Sub
            End If
        End If

        If Nouvo = True Then
            If Code.IsRequiredControl("Veuillez saisir le code") Then
                Code.Focus()
                Exit Sub
            End If
            If Val(ExecuteScallar("select count(*) from t_devisev1 where AbregeDevise='" & EnleverApost(Code.Text) & "' And CodeProjet ='" & ProjetEnCours & "'")) > 0 Then
                SuccesMsg("Ce code existe déjà")
                Code.Focus()
                Exit Sub
            End If

            ExecuteNonQuery("insert into t_devisev1 values(NULL, '" & EnleverApost(Nom.Text) & "','" & EnleverApost(Code.Text) & "', '" & ProjetEnCours & "')")
            Dim MaxCode = ExecuteScallar("select MAX(CodeDevise) from t_devisev1")
            ExecuteNonQuery("insert into t_tauxdevise values(NULL, '" & MaxCode & "','" & Taux.Text.Replace(".", ",").Replace(" ", "") & "', '" & TxtDate.Text & "', 'N')")
        ElseIf PourAjout = True Then
            If CmbDevise.SelectedIndex = -1 Then
                SuccesMsg("Veuilez selectionné un code")
                CmbDevise.Focus()
                Exit Sub
            End If

            If Val(ExecuteScallar("select count(*) from t_tauxdevise where DateTaux='" & TxtDate.Text & "' And TauxDevise ='" & Taux.Text.Replace(".", ",") & "' and CodeDevise='" & TabDevise(CmbDevise.SelectedIndex) & "'")) > 0 Then
                SuccesMsg("La date et le taux existe déjà")
                Taux.Focus()
                Exit Sub
            End If
            ExecuteNonQuery("insert into t_tauxdevise values(NULL, '" & TabDevise(CmbDevise.SelectedIndex) & "','" & Taux.Text.Replace(".", ",").Replace(" ", "") & "', '" & TxtDate.Text & "', 'N')")
        ElseIf PourModif = True Then
            'If Code.IsRequiredControl("Veuillez saisir le code") Then
            '    Code.Focus()
            '    Exit Sub
            'End If
            If TypeModif = "D" Then ExecuteNonQuery("UPDATE t_devisev1 set LibelleDevise='" & EnleverApost(Nom.Text) & "' where CodeDevise='" & CodeDevise & "'")
            If TypeModif = "T" Then
                If Val(ExecuteScallar("select count(*) from t_tauxdevise where DateTaux='" & TxtDate.Text & "' And TauxDevise ='" & Taux.Text.Replace(".", ",") & "' and CodeDevise='" & CodeDevise & "'")) > 0 Then
                    SuccesMsg("La date et le taux existe déjà")
                    Taux.Focus()
                    Exit Sub
                End If
                ExecuteNonQuery("UPDATE t_tauxdevise set TauxDevise='" & Taux.Text.Replace(".", ",").Replace(" ", "") & "', DateTaux='" & TxtDate.Text & "' where RefTauxDevise='" & RefTauxDevise & "'")
            End If
        End If

        If Nouvo = True Or PourModif = True Then
            Code.Text = ""
            Nom.Text = ""
            Taux.Text = ""
            TxtDate.Text = ""
            CodeDevise = ""
            RefTauxDevise = ""
        ElseIf PourAjout = True Then
            Taux.Text = ""
            TxtDate.Text = ""
        End If
        TypeModif = ""
        SuccesMsg("Enregistrement effectué avec succès")
        'If CmbDevise.SelectedIndex <> -1 Then
        '    ' ChargerDevise(TabDevise(CmbDevise.SelectedIndex))
        '    LoadDevise(TabDevise(CmbDevise.SelectedIndex))
        'Else
        '    ' ChargerDevise()
        'End If
        LoadDevise()
    End Sub

    Private Sub SupprimerToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If ViewDevise.RowCount > 0 Then
            DrX = ViewDevise.GetDataRow(ViewDevise.FocusedRowHandle)
            If DrX("TauxUtiliser").ToString = "O" Then
                SuccesMsg("Impossible de supprimé la devise")
                Exit Sub
            End If
            If ConfirmMsg("Voulez-vous vraiment supprimer ?") = DialogResult.Yes Then
                If Val(ExecuteScallar("select count(*) from t_tauxdevise where CodeDevise='" & DrX("CodeDevise").ToString & "'")) = 1 Then
                    ExecuteNonQuery("delete from t_devisev1 where CodeDevise='" & DrX("CodeDevise").ToString & "'")
                End If

                ExecuteNonQuery("delete from t_tauxdevise where RefTauxDevise='" & DrX("RefTauxDevise").ToString & "'")
                SuccesMsg("Suppression effectuée avec succès")
                ViewDevise.DeleteRow(ViewDevise.FocusedRowHandle)
                Initiliser()
            End If
        End If
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs)
        If ViewDevise.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub GridDevise_DoubleClick(sender As Object, e As EventArgs) Handles GridDevise.DoubleClick
        If ViewDevise.RowCount > 0 Then
            DrX = ViewDevise.GetDataRow(ViewDevise.FocusedRowHandle)
            If DrX("TauxUtiliser").ToString = "O" Then
                SuccesMsg("Impossible de modifier la devise")
                Exit Sub
            End If
            GetVisibleCode("Code")
            Code.Text = DrX("Code").ToString
            Nom.Text = DrX("Nom").ToString
            Taux.Text = DrX("Taux").ToString
            TxtDate.Text = DrX("Date").ToString
            CodeDevise = DrX("CodeDevise").ToString
            RefTauxDevise = DrX("RefTauxDevise").ToString
            PourModif = True
            Nouvo = False
            PourAjout = False
            NewRealOnly(False)
            Code.Properties.ReadOnly = True
        End If
    End Sub

    Private Sub CmbDevise_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbDevise.SelectedIndexChanged
        If CmbDevise.SelectedIndex <> -1 Then
            ' ChargerDevise(TabDevise(CmbDevise.SelectedIndex))
            '  LoadDevise(TabDevise(CmbDevise.SelectedIndex))
            Nom.Text = MettreApost(ExecuteScallar("SELECT LibelleDevise FROM t_devisev1 WHERE CodeDevise='" & TabDevise(CmbDevise.SelectedIndex) & "'"))
            Nom.Properties.ReadOnly = True
            Taux.Text = ""
            TxtDate.Text = ""
        End If
    End Sub

    Private Sub TreeListDevise_NodeChanged(sender As Object, e As DevExpress.XtraTreeList.NodeChangedEventArgs) Handles TreeListDevise.NodeChanged
        If e.ChangeType = NodeChangeTypeEnum.Expanded Then
            Dim Loaded As Integer = Val(e.Node.Item("loaded"))
            If Loaded = 0 Then
                LoadDevise(e.Node.Item("CodeDevise"), e.Node)
                e.Node.Item("loaded") = "1"
            End If
        End If
    End Sub

    Private Sub ModifierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModifierToolStripMenuItem.Click
        If TreeListDevise.Nodes.Count > 0 Then
            Dim Lign = TreeListDevise.FocusedNode
            If Lign.GetValue("TauxUtilise").ToString = "O" Then
                SuccesMsg("Impossible de modifier la devise")
                Exit Sub
            End If

            NewRealOnly(False)
            GetVisibleCode("Code")

            If Lign.GetValue("Type").ToString = "D" Then
                Code.Text = Lign.GetValue("Code").ToString
                Nom.Text = Lign.GetValue("Nom").ToString
                TxtDate.Text = ""
                Taux.Text = ""
                CodeDevise = Lign.GetValue("CodeDevise")
                TypeModif = "D"
                RefTauxDevise = ""
                TxtDate.Properties.ReadOnly = True
                Taux.Properties.ReadOnly = True
            Else
                Code.Text = Lign.ParentNode.GetValue("Code").ToString
                Nom.Text = Lign.ParentNode.GetValue("Nom").ToString
                TxtDate.Text = CDate(Lign.GetValue("Date")).ToShortDateString
                Taux.Text = Lign.GetValue("Taux").ToString
                TypeModif = "T"
                RefTauxDevise = Lign.GetValue("CodeDevise").ToString
                CodeDevise = Lign.ParentNode.GetValue("CodeDevise").ToString
                Code.Properties.ReadOnly = True
                Nom.Properties.ReadOnly = True
            End If

            'Taux.Text = DrX("Taux").ToString
            'TxtDate.Text = DrX("Date").ToString
            '    CodeDevise = DrX("CodeDevise").ToString
            '    RefTauxDevise = DrX("RefTauxDevise").ToString
            PourModif = True
            Nouvo = False
            PourAjout = False

            ' Code.Properties.ReadOnly = True
        End If
    End Sub

    Private Sub SupprimerToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles SupprimerToolStripMenuItem.Click
        If TreeListDevise.Nodes.Count > 0 Then
            Dim Lign = TreeListDevise.FocusedNode
            If Lign.GetValue("TauxUtilise").ToString = "O" Then
                SuccesMsg("Impossible de supprimé la devise")
                Exit Sub
            End If

            If ConfirmMsg("Voulez-vous vraiment supprimer ?") = DialogResult.Yes Then
                If Lign.GetValue("Type").ToString = "D" Then
                    If Val(ExecuteScallar("select count(*) from t_tauxdevise where CodeDevise='" & Lign.GetValue("CodeDevise").ToString & "' and TauxUtiliser='O'")) > 0 Then
                        SuccesMsg("Impossible de supprimé la devise")
                        Exit Sub
                    End If
                    ExecuteNonQuery("delete from t_devisev1 where CodeDevise='" & Lign.GetValue("CodeDevise") & "'")
                    ExecuteNonQuery("delete from t_tauxdevise where CodeDevise='" & Lign.GetValue("CodeDevise") & "'")
                Else
                    If Lign.GetValue("TauxUtilise") = "O" Then
                        SuccesMsg("Impossible de supprimé la devise")
                        Exit Sub
                    End If
                    If Val(ExecuteScallar("select count(*) from t_tauxdevise where CodeDevise='" & Lign.ParentNode.GetValue("CodeDevise").ToString & "'")) = 1 Then
                        ExecuteNonQuery("delete from t_devisev1 where CodeDevise='" & Lign.ParentNode.GetValue("CodeDevise") & "'")
                    End If
                    ExecuteNonQuery("delete from t_tauxdevise where RefTauxDevise='" & Lign.GetValue("CodeDevise").ToString & "'")
                End If

                SuccesMsg("Suppression effectuée avec succès")
                LoadDevise()
                Initiliser()
            End If
        End If
    End Sub

    Private Sub ContextMenuStrip1_Opening_1(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If TreeListDevise.Nodes.Count = 0 Then
            e.Cancel = True
        End If
    End Sub
End Class