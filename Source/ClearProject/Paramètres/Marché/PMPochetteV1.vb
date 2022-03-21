Imports MySql.Data.MySqlClient
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Columns
Imports DevExpress.XtraTreeList.Nodes

Public Class PMPochetteV1
    Dim dglistdocument = New DataTable()
    Public EMP_ID As Decimal = 0
    Dim currentNode As TreeListNode
    Dim InitialBailleur As String = ""

    Private Sub CreateColumns(ByVal tl As TreeList)
        ' Create three columns. 
        tl.BeginUpdate()
        Dim col1 As TreeListColumn = tl.Columns.Add()
        col1.Caption = "Pochette"
        col1.VisibleIndex = 0
        Dim col2 As TreeListColumn = tl.Columns.Add()
        col2.Caption = "Data"
        col2.VisibleIndex = -1
        tl.EndUpdate()
    End Sub

    Private Sub ChargerBailleur()
        query = "select InitialeBailleur, NomBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' order by InitialeBailleur"
        CombBail.Properties.Items.Clear()
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CombBail.Properties.Items.Add(rw("InitialeBailleur").ToString & " | " & MettreApost(rw("NomBailleur").ToString))
        Next
    End Sub

    Private Sub CreateNodes(ByVal tl As TreeList)
        tl.BeginUnboundLoad()

        Dim dt As DataTable = ExcecuteSelectQuery("select distinct AbregeAO, LibelleAO from `t_procao` where CodeProjet='" & ProjetEnCours & "'")
        For Each rw As DataRow In dt.Rows

            Dim MethodeNodes As TreeListNode = Nothing

            'Node des Methodes
            Dim rootNode As TreeListNode = tl.AppendNode(New Object() {MettreApost(rw("LibelleAO").ToString()), "Methode:" & rw("AbregeAO")}, MethodeNodes)

            Dim dt1 As DataTable = ExcecuteSelectQuery("select t.CodeTypeMarche, t.TypeMarche from `t_procao` p, t_typemarche t where p.TypeMarcheAO=t.TypeMarche and p.AbregeAO='" & rw("AbregeAO").ToString & "'")
            For Each rw1 As DataRow In dt1.Rows

                'Node types marches
                Dim NodeTypeMarche = tl.AppendNode(New Object() {MettreApost(rw1("TypeMarche").ToString()), "TypeMarche"}, rootNode)

                If rw1("TypeMarche").ToString.ToLower = "consultants" Then ' Cas de consultants
                    'Ajout des AMI
                    Dim LigneAMI As TreeListNode = tl.AppendNode(New Object() {"AMI", "AMI"}, NodeTypeMarche) 'Node Doss AMI

                    Dim dt2 As DataTable = ExcecuteSelectQuery("select NumeroDAMI from `t_ami` where MethodeSelection='" & rw("AbregeAO") & "'")
                    For Each rw2 As DataRow In dt2.Rows
                        'Add doss AMI
                        Dim DossAMI = tl.AppendNode(New Object() {MettreApost(rw2("NumeroDAMI").ToString), "BtAMI"}, LigneAMI)

                        Dim dt3 As DataTable = ExcecuteSelectQuery("select * from `t_pm_pochette_document` where CodeTypeMarche ='" & rw1("CodeTypeMarche").ToString & "' AND TypePochette='AMI'")
                        For Each rw3 As DataRow In dt3.Rows
                            Dim Options1 As TreeListNode = tl.AppendNode(New Object() {MettreApost(rw3("POCHDOC_LIB")), "Doc:" & rw3("POCHDOC_ID")}, DossAMI)

                            Dim dt4 As DataTable = ExcecuteSelectQuery("select * from `t_pm_pochette_bailleur` where CodeBailleur='" & InitialBailleur & "' and POCHDOC_ID='" & rw3("POCHDOC_ID") & "' and AbregeAo='" & rw("AbregeAO").ToString & "'")
                            tl.AppendNode(New Object() {"Ajouter", "BtNew"}, Options1)
                            For Each rw4 As DataRow In dt4.Rows
                                tl.AppendNode(New Object() {MettreApost(rw4("FileName")), "Blob"}, Options1)
                            Next
                        Next
                    Next

                    'Ajout des DP
                    Dim LigneDP As TreeListNode = tl.AppendNode(New Object() {"DP", "DP"}, NodeTypeMarche)
                    dt2 = ExcecuteSelectQuery("select NumeroDp from `t_dp` where MethodeSelection='" & rw("AbregeAO") & "'")
                    For Each rw2 As DataRow In dt2.Rows
                        Dim NodeDossier1 = tl.AppendNode(New Object() {MettreApost(rw2("NumeroDp").ToString), "BtDP"}, LigneDP)

                        Dim dt3 As DataTable = ExcecuteSelectQuery("select * from `t_pm_pochette_document` where CodeTypeMarche ='" & rw1("CodeTypeMarche").ToString & "' and TypePochette='DP'")
                        For Each rw0 As DataRow In dt3.Rows
                            Dim Options2 As TreeListNode = tl.AppendNode(New Object() {MettreApost(rw0("POCHDOC_LIB")), "Doc:" & rw0("POCHDOC_ID")}, NodeDossier1)

                            tl.AppendNode(New Object() {"Ajouter", "BtNew"}, Options2)
                            Dim dt4 As DataTable = ExcecuteSelectQuery("select * from `t_pm_pochette_bailleur` where CodeBailleur='" & InitialBailleur & "' and POCHDOC_ID='" & rw0("POCHDOC_ID") & "' and AbregeAo='" & rw("AbregeAO").ToString & "'")
                            For Each rw4 As DataRow In dt4.Rows
                                tl.AppendNode(New Object() {MettreApost(rw4("FileName")), "Blob"}, Options2)
                            Next
                        Next
                    Next
                Else
                    'Ajout des Travaux et des Fournitures
                    Dim dt3 As DataTable = ExcecuteSelectQuery("select NumeroDAO from `t_dao` where TypeMarche ='" & MettreApost(rw1("TypeMarche").ToString) & "' and MethodePDM='" & rw("AbregeAO") & "'")
                    For Each rw3 As DataRow In dt3.Rows
                        Dim LigneTravauxFour = tl.AppendNode(New Object() {MettreApost(rw3("NumeroDAO").ToString), "BtFrTr"}, NodeTypeMarche)

                        Dim dt0 As DataTable = ExcecuteSelectQuery("select * from `t_pm_pochette_document` where CodeTypeMarche ='" & rw1("CodeTypeMarche").ToString & "'")
                        For Each rw0 As DataRow In dt0.Rows
                            Dim Options3 As TreeListNode = tl.AppendNode(New Object() {MettreApost(rw0("POCHDOC_LIB")), "Doc:" & rw0("POCHDOC_ID")}, LigneTravauxFour)

                            tl.AppendNode(New Object() {"Ajouter", "BtNew"}, Options3)
                            Dim dt2 As DataTable = ExcecuteSelectQuery("select * from `t_pm_pochette_bailleur` where CodeBailleur='" & InitialBailleur & "' and POCHDOC_ID='" & rw0("POCHDOC_ID") & "' and AbregeAo='" & rw("AbregeAO").ToString & "'")
                            For Each rw2 As DataRow In dt2.Rows
                                tl.AppendNode(New Object() {MettreApost(rw2("FileName")), "Blob"}, Options3)
                            Next
                        Next
                    Next
                End If
            Next
        Next

        ' Create a child node for the node1             
        ' Creating more nodes 
        ' ... 
        tl.EndUnboundLoad()
    End Sub


    Private Sub GRHPochette_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        CombBail.Text = ""
        CreateColumns(treeLPochette)
        ChargerBailleur()

    End Sub

    Private Sub GRHPochetteEmploye_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        treeLPochette.Columns.Clear()
        treeLPochette.Nodes.Clear()
    End Sub
    Private Sub OuvrirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OuvrirToolStripMenuItem.Click
        If treeLPochette.FocusedNode.Item(1) = "Blob" Then

        End If
    End Sub

    Private Sub TéléchargerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TéléchargerToolStripMenuItem.Click
        If treeLPochette.FocusedNode.Item(1) = "Blob" Then
            '  treeLPochette.ExpandAll()
            'treeLPochette.ExportToPdf(My.Computer.FileSystem.CurrentDirectory & "\Pochette.pdf")
        End If
    End Sub

    Private Sub SupprimerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupprimerToolStripMenuItem.Click
        If treeLPochette.FocusedNode.Item(1) = "Blob" Then

        End If
    End Sub

    Private Sub treeLPochette_FocusedNodeChanged(sender As Object, e As FocusedNodeChangedEventArgs) Handles treeLPochette.FocusedNodeChanged
        currentNode = e.Node
    End Sub

    Private Sub treeLPochette_DoubleClick(sender As Object, e As EventArgs) Handles treeLPochette.DoubleClick
        If treeLPochette.Nodes.Count > 0 Then

            Dim opfile As New OpenFileDialog
            opfile.Filter = "Documents|*.pdf;*.docx;*.doc;*.rtf;*.xl;*.xls;*.xlsx;*.csv|Images|*.jpg;*.jpeg;*.png;*.gif;*.bmp"
            Dim paths As String() = {""}
            Dim AbreMethode As String = ""

            Try
                Dim pathFile As String = ""
                Dim TypeMarche As String = ""
                Dim NumeroDoss As String = ""
                Dim LibellePochette As String = ""

                If (currentNode.Item(1) = "Blob") Or (currentNode.Item(1) = "BtNew") Then
                    'Ligne de type marche fournitures ou travaux clique
                    If currentNode.ParentNode.ParentNode.ParentNode.Item(1).ToString = "TypeMarche" Then
                        AbreMethode = Mid(currentNode.ParentNode.ParentNode.ParentNode.ParentNode.Item(1).ToString, 9)
                        TypeMarche = currentNode.ParentNode.ParentNode.ParentNode.Item(0).ToString
                    Else
                        AbreMethode = Mid(currentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.Item(1).ToString, 9)
                        TypeMarche = currentNode.ParentNode.ParentNode.ParentNode.ParentNode.Item(0).ToString
                    End If
                    NumeroDoss = currentNode.ParentNode.ParentNode.Item(0).ToString
                    LibellePochette = currentNode.ParentNode.Item(0).ToString
                End If

                If currentNode.Item(1) = "Blob" Then 'ligne d'un document clique
                    pathFile = line & "\PochettesPM\BAIL_" & InitialBailleur & "\" & AbreMethode & "\" & TypeMarche & "\" & NumeroDoss & "\" & LibellePochette & "\" & currentNode.Item(0).ToString

                    If Not File.Exists(pathFile) Then
                        If ConfirmMsg("Le fichier demandé n'existe plus" & vbNewLine & "Voulez-vous le supprimer ?") = DialogResult.Yes Then
                            ExecuteNonQuery("delete from t_pm_pochette_bailleur where POCHDOC_ID='" & Mid(currentNode.ParentNode.Item(1), 5) & "' and CodeBailleur='" & InitialBailleur & "' and FileName='" & EnleverApost(currentNode.Item(0)) & "' and AbregeAO='" & AbreMethode & "'")
                            treeLPochette.Nodes.Remove(currentNode)
                        End If
                    Else
                        Process.Start(pathFile)
                    End If
                ElseIf currentNode.Item(1) = "BtNew" Then
                    Dim rep = opfile.ShowDialog
                    If rep = DialogResult.OK Then
                        paths = opfile.FileName.Split("\"c)
                        Dim verif As String = ExecuteScallar("select FileName from t_pm_pochette_bailleur where FileName='" & EnleverApost(paths(paths.Length - 1)) & "' and CodeBailleur='" & InitialBailleur & "' and POCHDOC_ID='" & Mid(currentNode.ParentNode.Item(1).ToString(), 5) & "' And AbregeAO ='" & AbreMethode & "'")
                        If Len(verif) <> 0 Then
                            SuccesMsg("Le fichier existe déjà.")
                        Else
                            '  Dim path As String = line & "\PochettesPM\" & currentNode.ParentNode.Item(0) & "\BAIL_" & EMP_ID
                            Dim path As String = line & "\PochettesPM\BAIL_" & InitialBailleur & "\" & AbreMethode & "\" & TypeMarche & "\" & NumeroDoss & "\" & LibellePochette

                            If Not Directory.Exists(path) Then
                                Directory.CreateDirectory(path)
                            End If
                            File.Copy(opfile.FileName, path & "\" & paths(paths.Length - 1), True)

                            ExecuteNonQuery("insert into t_pm_pochette_bailleur values(NULL,'" & EnleverApost(paths(paths.Length - 1)) & "','" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "',NULL, '" & Mid(currentNode.ParentNode.Item(1).ToString(), 5) & "','" & InitialBailleur & "', '" & AbreMethode & "')")
                            treeLPochette.AppendNode(New Object() {paths(paths.Length - 1), "Blob"}, currentNode.ParentNode)
                        End If
                    End If
                End If
            Catch ex As Exception
                ExecuteNonQuery("delete from t_pm_pochette_bailleur where POCHDOC_ID='" & Mid(currentNode.ParentNode.Item(1), 5) & "' and CodeBailleur='" & InitialBailleur & "' and FileName='" & EnleverApost(currentNode.Item(0)) & "' And AbregeAO ='" & AbreMethode & "'")
                FailMsg("Erreur : " & vbNewLine & ex.ToString)
            End Try
        End If
    End Sub

    Private Sub CombBail_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CombBail.SelectedIndexChanged
        If CombBail.SelectedIndex <> -1 Then
            Dim int() As String
            int = CombBail.Text.Split(" | ")
            InitialBailleur = int(0).ToString.Trim

            query = "select CodeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' and InitialeBailleur='" & int(0).ToString & "' order by InitialeBailleur"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                EMP_ID = rw("CodeBailleur").ToString
            Next

            treeLPochette.Columns.Clear()
            treeLPochette.Nodes.Clear()
            CreateColumns(treeLPochette)
            CreateNodes(treeLPochette)
        Else
            treeLPochette.Columns.Clear()
            treeLPochette.Nodes.Clear()
            InitialBailleur = ""
        End If

    End Sub


    Private Sub BtDelete_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtDelete.ItemClick
        If treeLPochette.Nodes.Count = 0 Then
            FailMsg("Aucun fichier à supprimer")
            Exit Sub
        End If

        If (currentNode.Item(1) = "Blob") Then
            Dim TypeMarche As String = ""
            Dim NumeroDoss As String = ""
            Dim LibellePochette As String = ""
            Dim AbreMethode As String = ""

            If currentNode.ParentNode.ParentNode.ParentNode.Item(1).ToString = "TypeMarche" Then
                AbreMethode = Mid(currentNode.ParentNode.ParentNode.ParentNode.ParentNode.Item(1).ToString, 9)
                TypeMarche = currentNode.ParentNode.ParentNode.ParentNode.Item(0).ToString
            Else
                AbreMethode = Mid(currentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.Item(1).ToString, 9)
                TypeMarche = currentNode.ParentNode.ParentNode.ParentNode.ParentNode.Item(0).ToString
            End If
            NumeroDoss = currentNode.ParentNode.ParentNode.Item(0).ToString
            LibellePochette = currentNode.ParentNode.Item(0).ToString

            Dim pathFile As String = line & "\PochettesPM\BAIL_" & InitialBailleur & "\" & AbreMethode & "\" & TypeMarche & "\" & NumeroDoss & "\" & LibellePochette & "\" & currentNode.Item(0).ToString

            'Dim POCHDOC_LIB As String = ""
            'If Mid(currentNode.ParentNode.Item(1).ToString(), 1, 6) <> "Filtre" Then
            '    POCHDOC_LIB = currentNode.ParentNode.Item(0).ToString()
            'Else
            '    POCHDOC_LIB = currentNode.ParentNode.ParentNode.Item(0).ToString()
            'End If

            If Not File.Exists(pathFile) Then
                If ConfirmMsg("Le fichier a supprimé n'existe plus" & vbNewLine & "Voulez-vous le supprimer de la liste ?") = DialogResult.Yes Then
                    ExecuteNonQuery("delete from t_pm_pochette_bailleur where POCHDOC_ID='" & Val(Mid(currentNode.ParentNode.Item(1).ToString(), 5)) & "' and CodeBailleur='" & InitialBailleur & "' and FileName='" & EnleverApost(currentNode.Item(0)) & "' and AbregeAO='" & AbreMethode & "'")
                    treeLPochette.Nodes.Remove(currentNode)
                End If
            ElseIf ConfirmMsg("Voulez-vous supprimer ce fichier?") = DialogResult.Yes Then
                Try
                    File.Delete(pathFile)
                    ExecuteNonQuery("delete from t_pm_pochette_bailleur where POCHDOC_ID='" & Val(Mid(currentNode.ParentNode.Item(1).ToString(), 5)) & "' and CodeBailleur='" & InitialBailleur & "' and FileName='" & EnleverApost(currentNode.Item(0)) & "' and AbregeAO='" & AbreMethode & "'")
                    treeLPochette.Nodes.Remove(currentNode)
                Catch ep As IO.IOException
                    SuccesMsg("Le fichier est utilisé par une autre application" & vbNewLine & "Veuillez le fermer svp.")
                Catch ex As Exception
                    FailMsg(ex.ToString())
                End Try
            End If
        End If
    End Sub

    Private Sub BtOpen_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtOpen.ItemClick
        If treeLPochette.Nodes.Count = 0 Then
            FailMsg("Aucun fichier à ouvrir")
            Exit Sub
        End If

        If treeLPochette.FocusedNode.Item(1) = "Blob" Then
            Try
                Dim TypeMarche As String = ""
                Dim NumeroDoss As String = ""
                Dim LibellePochette As String = ""
                Dim AbreMethode As String = ""

                'Ligne de type marche fournitures ou travaux clique
                If currentNode.ParentNode.ParentNode.ParentNode.Item(1).ToString = "TypeMarche" Then
                    AbreMethode = Mid(currentNode.ParentNode.ParentNode.ParentNode.ParentNode.Item(1).ToString, 9)
                    TypeMarche = currentNode.ParentNode.ParentNode.ParentNode.Item(0).ToString
                Else
                    AbreMethode = Mid(currentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.Item(1).ToString, 9)
                    TypeMarche = currentNode.ParentNode.ParentNode.ParentNode.ParentNode.Item(0).ToString
                End If
                NumeroDoss = currentNode.ParentNode.ParentNode.Item(0).ToString
                LibellePochette = currentNode.ParentNode.Item(0).ToString
                Dim pathFile As String = line & "\PochettesPM\BAIL_" & InitialBailleur & "\" & AbreMethode & "\" & TypeMarche & "\" & NumeroDoss & "\" & LibellePochette & "\" & currentNode.Item(0).ToString

                If Not File.Exists(pathFile) Then
                    If ConfirmMsg("Le fichier demandé n'existe plus" & vbNewLine & "Voulez-vous le supprimer ?") = DialogResult.Yes Then
                        ExecuteNonQuery("delete from t_pm_pochette_bailleur where POCHDOC_ID='" & Mid(currentNode.ParentNode.Item(1), 5) & "' and CodeBailleur='" & InitialBailleur & "' and FileName='" & EnleverApost(currentNode.Item(0)) & "' and AbregeAO='" & AbreMethode & "'")
                        treeLPochette.Nodes.Remove(currentNode)
                    End If
                Else
                    Process.Start(pathFile)
                End If
            Catch ex As Exception
                FailMsg(ex.ToString)
            End Try
        Else
            SuccesMsg("Veuillez selectionné un document")
        End If
    End Sub
End Class