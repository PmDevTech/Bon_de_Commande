Imports MySql.Data.MySqlClient
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Columns
Imports DevExpress.XtraTreeList.Nodes

Public Class PMPochette
    Dim dglistdocument = New DataTable()
    Public EMP_ID As Decimal = 0
    Dim currentNode As TreeListNode

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

        query = "select distinct AbregeAO, LibelleAO from `t_procao` where CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows

            Dim MethodeNodes As TreeListNode = Nothing
            Dim rootNode As TreeListNode = tl.AppendNode(New Object() {MettreApost(rw("LibelleAO").ToString()), "Methode:" & rw("AbregeAO")}, MethodeNodes)

            query = "select t.CodeTypeMarche, t.TypeMarche from `t_procao` p, t_typemarche t where p.TypeMarcheAO=t.TypeMarche and p.AbregeAO='" & rw("AbregeAO").ToString & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw1 As DataRow In dt1.Rows

                Dim NodeTypeMarche = tl.AppendNode(New Object() {MettreApost(rw1("TypeMarche").ToString()), "TypeMarche"}, rootNode)

                query = "select * from `t_pm_pochette_document` where CodeTypeMarche ='" & rw1("CodeTypeMarche").ToString & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw0 As DataRow In dt0.Rows

                    Dim Options As TreeListNode = tl.AppendNode(New Object() {MettreApost(rw0("POCHDOC_LIB")), "Doc:" & rw0("POCHDOC_ID")}, NodeTypeMarche)

                    query = "select * from `t_pm_pochette_bailleur` where CodeBailleur=" & EMP_ID & " and POCHDOC_ID='" & rw0("POCHDOC_ID") & "' and AbregeAo='" & rw("AbregeAO").ToString & "'"
                    tl.AppendNode(New Object() {"Ajouter", "BtNew"}, Options)
                    Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw2 As DataRow In dt2.Rows
                        tl.AppendNode(New Object() {MettreApost(rw2("FileName")), "Blob"}, Options)
                    Next

                Next
            Next
        Next
        ' Create a child node for the node1             
        ' Creating more nodes 
        ' ... 
        tl.EndUnboundLoad()
    End Sub

    Private Sub GRHPochette_Load(sender As Object, e As EventArgs)  Handles MyBase.Load
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
            treeLPochette.ExpandAll()
            treeLPochette.ExportToPdf(My.Computer.FileSystem.CurrentDirectory & "\Pochette.pdf")
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
        Dim opfile As New OpenFileDialog
        opfile.Filter = "Documents|*.pdf;*.docx;*.doc;*.rtf;*.xl;*.xls;*.xlsx;*.csv|Images|*.jpg;*.jpeg;*.png;*.gif;*.bmp"
        Dim paths As String() = {""}
        Try
            If currentNode.Item(1) = "Blob" Then
                Dim pathFile As String = line & "\PochettesPM\" & currentNode.ParentNode.Item(0) & "\BAIL_" & EMP_ID & "\" & currentNode.Item(0)
                If Not File.Exists(pathFile) Then
                    If MessageBox.Show("Le fichier demandé n'existe plus" & vbNewLine & "Voulez-vous le supprimer ?", "ClearProject", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                        ExecuteNonQuery("delete from t_pm_pochette_bailleur where POCHDOC_ID=" & Mid(currentNode.ParentNode.Item(1), 5) & " and CodeBailleur=" & EMP_ID & " and FileName='" & EnleverApost(currentNode.Item(0)) & "'")
                        treeLPochette.Nodes.Remove(currentNode)
                    End If
                Else
                    Process.Start(pathFile)
                End If
            ElseIf currentNode.Item(1) = "BtNew" Then
                Dim rep = opfile.ShowDialog
                If rep = DialogResult.OK Then
                    paths = opfile.FileName.Split("\"c)
                    Dim verif As String = ExecuteScallar("select FileName from t_pm_pochette_bailleur where FileName='" & EnleverApost(paths(paths.Length - 1)) & "' and CodeBailleur=" & EMP_ID & " and POCHDOC_ID=" & Mid(currentNode.ParentNode.Item(1).ToString(), 5))
                    If Len(verif) <> 0 Then
                        SuccesMsg("Le fichier existe déjà.")
                    Else
                        Dim path As String = line & "\PochettesPM\" & currentNode.ParentNode.Item(0) & "\BAIL_" & EMP_ID
                        If Not Directory.Exists(path) Then
                            Directory.CreateDirectory(path)
                        End If
                        File.Copy(opfile.FileName, path & "\" & paths(paths.Length - 1), True)
                        query = "insert into t_pm_pochette_bailleur values(null,'" & EnleverApost(paths(paths.Length - 1)) & "','" & dateconvert(Now.ToShortDateString()) & "',null," & Mid(currentNode.ParentNode.Item(1).ToString(), 5) & "," & EMP_ID & ", '" & Mid(currentNode.ParentNode.ParentNode.ParentNode.Item(1).ToString(), 9) & "')"
                        ExecuteNonQuery(query)
                        treeLPochette.AppendNode(New Object() {paths(paths.Length - 1), "Blob"}, currentNode.ParentNode)
                    End If
                End If
            End If
        Catch ex As Exception
            ExecuteNonQuery("delete from t_pm_pochette_bailleur where POCHDOC_ID=" & Mid(currentNode.ParentNode.Item(1), 5) & " and CodeBailleur=" & EMP_ID & " and FileName='" & EnleverApost(currentNode.Item(0)) & "'")
            FailMsg("Erreur : " & vbNewLine & ex.ToString)
        End Try
    End Sub

    Private Sub CombBail_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CombBail.SelectedIndexChanged
        Dim int() As String
        int = CombBail.Text.Split(" | ")

        query = "select CodeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' and InitialeBailleur='" & int(0).ToString & "' order by InitialeBailleur"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            EMP_ID = rw("CodeBailleur").ToString
        Next

        treeLPochette.Columns.Clear()
        treeLPochette.Nodes.Clear()
        CreateColumns(treeLPochette)
        CreateNodes(treeLPochette)
    End Sub

    Private Sub BtDelete_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtDelete.ItemClick
        Dim POCHDOC_LIB As String = ""
        If Mid(currentNode.ParentNode.Item(1).ToString(), 1, 6) <> "Filtre" Then
            POCHDOC_LIB = currentNode.ParentNode.Item(0).ToString()
        Else
            POCHDOC_LIB = currentNode.ParentNode.ParentNode.Item(0).ToString()
        End If
        Dim pathFile As String = line & "\PochettesPM\" & POCHDOC_LIB & "\BAIL_" & EMP_ID & "\" & currentNode.Item(0)
        If Not File.Exists(pathFile) Then
            If ConfirmMsg("Le fichier a supprimé n'existe plus" & vbNewLine & "Voulez-vous le supprimer de la liste ?") = DialogResult.Yes Then
                Dim POCHDOC_ID As Decimal = -1
                If Mid(currentNode.ParentNode.Item(1).ToString(), 1, 6) <> "Filtre" Then
                    POCHDOC_ID = Val(Mid(currentNode.ParentNode.Item(1).ToString(), 5))
                Else
                    POCHDOC_ID = Val(Mid(currentNode.ParentNode.ParentNode.Item(1).ToString(), 5))
                End If
                ExecuteNonQuery("delete from t_pm_pochette_bailleur where POCHDOC_ID=" & POCHDOC_ID & " and CodeBailleur=" & EMP_ID & " and FileName='" & EnleverApost(currentNode.Item(0)) & "'")
                treeLPochette.Nodes.Remove(currentNode)
            End If
        ElseIf ConfirmMsg("Voulez-vous supprimer ce fichier?") = DialogResult.Yes Then
            Try
                File.Delete(pathFile)
                Dim POCHDOC_ID As Decimal = -1
                If Mid(currentNode.ParentNode.Item(1).ToString(), 1, 6) <> "Filtre" Then
                    POCHDOC_ID = Val(Mid(currentNode.ParentNode.Item(1).ToString(), 5))
                Else
                    POCHDOC_ID = Val(Mid(currentNode.ParentNode.ParentNode.Item(1).ToString(), 5))
                End If
                query = "delete from t_pm_pochette_bailleur where POCHDOC_ID=" & POCHDOC_ID & " and CodeBailleur=" & EMP_ID & " and FileName='" & EnleverApost(currentNode.Item(0)) & "'"
                ExecuteNonQuery(query)
                treeLPochette.Nodes.Remove(currentNode)
            Catch ep As IO.IOException
                MessageBox.Show("Le fichier est utilisé par une autre application" & vbNewLine & "Veuillez le fermer svp.", "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                FailMsg(ex.ToString())
            End Try
        End If
    End Sub

End Class