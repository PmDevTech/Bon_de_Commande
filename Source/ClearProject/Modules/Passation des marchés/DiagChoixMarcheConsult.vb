Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.IO
Public Class DiagChoixMarcheConsult

    Private Sub DiagChoixMarcheConsult_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        MajMarcheChoix()
    End Sub

    Private Sub BtAjoutMarche_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjoutMarche.Click

        Dim CodMarc(20) As Decimal
        Dim NbreMarche As Decimal = 0

        For i As Integer = 0 To GridChoixMarche.RowCount - 1
            GridChoixMarche.Rows.Item(i).Cells(0).Selected = True
            GridChoixMarche.Rows.Item(i).Cells(1).Selected = True

            If (GridChoixMarche.Rows.Item(i).Cells(0).Value = True) Then

                CodMarc(NbreMarche) = GridChoixMarche.Rows.Item(i).Cells(1).Value
                NbreMarche = NbreMarche + 1

                Dim DatSet = New DataSet
                query = "select * from T_Marche where RefMarche='" & GridChoixMarche.Rows.Item(i).Cells(1).Value & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Fill(DatSet, "T_Marche")

                DatSet.Tables!T_Marche.Rows(0)!NumeroDAO = ReponseDialog

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
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    MontTT = MontTT + CDec(rw(1))
                    If (MontPlus < CDec(rw(1))) Then
                        MontPlus = CDec(rw(1))
                        MethMarc = rw(0)
                        BaillPlus = rw(3).ToString
                        Qualif = rw(4).ToString
                    End If
                    If (Descript <> "") Then
                        Descript = MettreApost(Descript) & " et "
                    End If
                    Descript = Descript & MettreApost(rw(2).ToString)

                Next

            Next


            'Recherche de la convention **************
            Dim Conv1 As String = ""
            Dim PaysBenef As String = ""
            query = "select C.CodeConvention,C.TypeConvention,C.Beneficiaire from T_Convention as C, T_Bailleur as B where B.CodeBailleur=C.CodeBailleur and B.InitialeBailleur='" & BaillPlus & "' and B.CodeProjet='" & ProjetEnCours & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw1 As DataRow In dt1.Rows
                Conv1 = rw1(0).ToString
                PaysBenef = "Etat de " & rw1(2).ToString
            Next

            'If (NewDp.ChkLibDpAuto.Checked = False) Then
            '    Descript = NewDp.TxtLibDp.Text
            'End If
            If (NewDp.TxtLibDp.Text <> "") Then
                If (Mid(NewDp.TxtLibDp.Text, 1, 4) <> "****") Then
                    Descript = NewDp.TxtLibDp.Text & "; et " & Descript
                End If
            End If

            ExceptRevue = MettreApost(Descript)

            Dim mont As Double = 0
            mont = MontTT

            If Mid(ReponseDialog, 1, 3) = "AMI" Then

               query= "update T_AMI set MethodeSelection='" & MethMarc & "', MontantMarche='" & mont.ToString & "', LibelleMiss='" & EnleverApost(Descript) & "', CodeConvention='" & Conv1 & "', NomEmprunteur='" & PaysBenef & "', PreQualif='" & Qualif & "' where NumeroDAMI='" & ReponseDialog & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

            Else

               query= "update T_DP set MethodeSelection='" & MethMarc & "', MontantMarche='" & mont.ToString & "', LibelleMiss='" & EnleverApost(Descript) & "', CodeConvention='" & Conv1 & "', NomEmprunteur='" & PaysBenef & "', PreQualif='" & Qualif & "' where NumeroDP='" & ReponseDialog & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

            End If

            Dim NomDossier As String = line & "\DP\" & CbTypeMarche.Text & "\" & MethMarc & "\" & ReponseDialog.Replace("/", "_")
            If (Directory.Exists(NomDossier) = False) Then
                Directory.CreateDirectory(NomDossier)
            End If

            NewDp.TxtMethodeSelect.Text = MethMarc

        End If

        ReponseDialog = CbTypeMarche.Text
        Me.Close()
    End Sub

    Private Sub BtQuitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        ReponseDialog = ""
        Me.Close()
    End Sub

    Private Sub MajMarcheChoix()

        query = "select RefMarche,DescriptionMarche from T_Marche where NumeroDAO='' and MethodeMarche in ('SFQC','SFQ','SCBD','SMC','QC','3CV','ED') and TypeMarche='" & CbTypeMarche.Text & "' and CodeProjet='" & ProjetEnCours & "'"
        GridChoixMarche.Rows.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Dim n As Decimal = GridChoixMarche.Rows.Add()
            GridChoixMarche.Rows.Item(n).Cells(1).Value = rw(0)
            GridChoixMarche.Rows.Item(n).Cells(2).Value = MettreApost(rw(1).ToString)
        Next

    End Sub

End Class