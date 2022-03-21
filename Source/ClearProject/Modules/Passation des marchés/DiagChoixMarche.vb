Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.IO

Public Class DiagChoixMarche

    Private Sub DiagChoixMarche_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        CbTypeMarche_SelectedIndexChanged(Me, e)
    End Sub

    Private Sub BtAjoutMarche_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjoutMarche.Click

        Dim CodMarc(20) As Decimal
        Dim NbreMarche As Decimal = 0

        For i As Integer = 0 To GridChoixMarche.RowCount - 1
            GridChoixMarche.Rows.Item(i).Cells(0).Selected = True
            GridChoixMarche.Rows.Item(i).Cells(1).Selected = True

            Dim checkCell As DataGridViewCheckBoxCell = CType(GridChoixMarche.Rows(i).Cells("Column3"), DataGridViewCheckBoxCell)
            If (checkCell.Value = True) Then

                CodMarc(NbreMarche) = GridChoixMarche.Rows.Item(i).Cells(1).Value
                NbreMarche = NbreMarche + 1


               query= "select MethodeMarche from T_Marche where RefMarche='" & GridChoixMarche.Rows.Item(i).Cells(1).Value & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim methodMarc As String = ExecuteScallar(query)


                If (methodMarc <> "") Then

                    If methodMarc.ToString = "AOI" Or methodMarc.ToString = "AON" Then

                        Dim nDao As Decimal = 0
                        query = "select Count(*) from T_DAO where CodeProjet='" & ProjetEnCours & "'"
                        nDao = ExecuteNonQuery(query)
                        nDao = CInt(nDao) + 1
                        
                        Dim NdaoStr As String = ""

                        If (nDao < 10) Then
                            NdaoStr = "00" & nDao.ToString
                        ElseIf (nDao < 100) Then
                            NdaoStr = "0" & nDao.ToString
                        Else
                            NdaoStr = nDao.ToString
                        End If

                        NdaoStr = "DAO" & NdaoStr & "/" & ProjetEnCours & "/" & Mid(Now.ToShortDateString, 4, 2) & "/" & Mid(Now.ToShortDateString, 9, 2)
                        NewDao.TxtNumDao.Text = NdaoStr
                    Else

                        Dim nDao As Decimal = 0
                        query = "select Count(*) from T_DAO where CodeProjet='" & ProjetEnCours & "' and MethodePDM='" & methodMarc.ToString & "'"
                        nDao = Val(ExecuteScallar(query))
                        nDao = CInt(nDao) + 1

                        Dim NdaoStr As String = ""

                        If (nDao < 10) Then
                            NdaoStr = "00" & nDao.ToString
                        ElseIf (nDao < 100) Then
                            NdaoStr = "0" & nDao.ToString
                        Else
                            NdaoStr = nDao.ToString
                        End If

                        NdaoStr = methodMarc.ToString & NdaoStr & "/" & ProjetEnCours & "/" & Mid(Now.ToShortDateString, 4, 2) & "/" & Mid(Now.ToShortDateString, 9, 2)
                        NewDao.TxtNumDao.Text = NdaoStr
                    End If

                End If

                Dim DatSet = New DataSet
                query = "select * from T_Marche where RefMarche='" & GridChoixMarche.Rows.Item(i).Cells(1).Value & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Fill(DatSet, "T_Marche")

                DatSet.Tables!T_Marche.Rows(0)!NumeroDAO = NewDao.TxtNumDao.Text

                DatAdapt.Update(DatSet, "T_Marche")
                DatSet.Clear()
                BDQUIT(sqlconn)
            End If
        Next


        If (NbreMarche >= 1) Then
            Dim MethMarc As String = ""
            Dim MontPlus As Decimal = 0
            Dim BaillPlus As String = ""
            Dim MontTT As Decimal = 0
            Dim Descript As String = ""
            Dim Qualif As String = ""

            For k As Integer = 0 To NbreMarche - 1
                query = "select MethodeMarche,MontantEstimatif,DescriptionMarche,InitialeBailleur,QualifPrePost from T_Marche where RefMarche='" & CodMarc(k) & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt5 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt5.Rows
                    MontTT = MontTT + CDec(rw(1))
                    If (MontPlus < CDec(rw(1))) Then
                        MontPlus = CDec(rw(1))
                        MethMarc = rw(0)
                        BaillPlus = rw(3).ToString
                        Qualif = rw(4).ToString
                    End If
                    If (Descript <> "") Then
                        Descript = Descript & " et "
                    End If
                    Descript = Descript & rw(2).ToString

                Next

            Next

            'Recherche de la convention **************
            Dim Conv1 As String = ""
            Dim PaysBenef As String = ""
            query = "select C.CodeConvention,C.TypeConvention,C.Beneficiaire from T_Convention as C, T_Bailleur as B where B.CodeBailleur=C.CodeBailleur and B.InitialeBailleur='" & BaillPlus & "' and B.CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                Conv1 = rw(0).ToString
                PaysBenef = "Etat de " & rw(2).ToString
            Next

            'If (NewDao.ChkLibDaoAuto.Checked = False) Then
            '    Descript = NewDao.TxtLibelleDao.Text
            'End If

            ExceptRevue = MettreApost(Descript)
            CreerDAO()

           query= "Update T_DAO set TypeMarche='" & CbTypeMarche.Text & "', MethodePDM='" & MethMarc & "', MontantMarche='" & MontTT & "', IntituleDAO='" & EnleverApost(Descript) & "', CodeConvention='" & Conv1 & "', NomEmprunteur='" & PaysBenef & "', PreQualif='" & Qualif & "' where NumeroDAO='" & NewDao.TxtNumDao.Text & "' and CodeProjet='" & ProjetEnCours & "'"
            ExecuteNonQuery(query)

            Dim NomDossier As String = line & "\DAO\" & CbTypeMarche.Text & "\" & MethMarc & "\" & NewDao.TxtNumDao.Text.Replace("/", "_")
            If (Directory.Exists(NomDossier) = False) Then
                Directory.CreateDirectory(NomDossier)
            End If

            NewDao.cmbTypeMarche.Text = CbTypeMarche.Text
            NewDao.TxtMethodeMarche.Text = MethMarc

        End If

        ReponseDialog = CbTypeMarche.Text
        Me.Close()
    End Sub

    Private Sub CreerDAO()

        query = "select * from T_DAO where NumeroDAO='" & NewDao.TxtNumDao.Text & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Exit Sub
        Next

        Dim DatSet = New DataSet
        query = "select * from T_DAO"
        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)
        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd)
        DatAdapt.Fill(DatSet, "T_DAO")
        Dim DatTable = DatSet.Tables("T_DAO")
        Dim DatRow = DatSet.Tables("T_DAO").NewRow()

        DatRow("NumeroDAO") = NewDao.TxtNumDao.Text
        DatRow("DateEdition") = Now.ToShortDateString
        DatRow("DateModif") = Now.ToShortDateString & " " & Now.ToShortTimeString
        DatRow("DateSaisie") = Now.ToShortDateString & " " & Now.ToShortTimeString
        DatRow("Operateur") = CodeUtilisateur
        DatRow("CodeProjet") = ProjetEnCours

        DatSet.Tables("T_DAO").Rows.Add(DatRow)
        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        DatAdapt.Update(DatSet, "T_DAO")
        DatSet.Clear()
        BDQUIT(sqlconn)
    End Sub

    Private Sub BtQuitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        ReponseDialog = ""
        Me.Close()
    End Sub

    Private Sub CbTypeMarche_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CbTypeMarche.SelectedIndexChanged
        If (CbTypeMarche.Text <> "") Then

            query = "select RefMarche,DescriptionMarche from T_Marche where NumeroDAO is null and TypeMarche='" & CbTypeMarche.Text & "' and CodeProjet='" & ProjetEnCours & "'"
            GridChoixMarche.Rows.Clear()
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                Dim n As Decimal = GridChoixMarche.Rows.Add()
                GridChoixMarche.Rows.Item(n).Cells(1).Value = rw(0)
                GridChoixMarche.Rows.Item(n).Cells(2).Value = MettreApost(rw(1).ToString)
            Next

        End If
    End Sub
End Class