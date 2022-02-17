Imports Microsoft
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.Math
Public Class DialogMethodeConsult


    Dim CodMarche As Decimal

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If (CmbMethode.Text <> "") Then
            Dim DatSet = New DataSet
            query = "select * from T_Marche where RefMarche='" & CodMarche & "'"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Fill(DatSet, "T_Marche")
            DatSet.Tables!T_Marche.Rows(0)!MethodeMarche = CmbMethode.Text
            DatAdapt.Update(DatSet, "T_Marche")
            DatSet.Clear()

            Dim Montant As Decimal = 0
            query = "select MontantEstimatif from T_Marche where RefMarche='" & CodMarche & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                Montant = CDec(rw(0))
            Next

            Dim Revue As String = ""
            Dim KodSeuil As Decimal = 0
            query = "select MontantPlanche,PlancheInclu,MontantPlafond,PlafondInclu,TypeExamenAO,ExceptionRevue,CodeSeuil from T_Seuil where CodeProcAO='" & CodMethodCache.Text & "'"
            Dim dt2 As DataTable = ExcecuteSelectQuery(query)
            For Each rw2 In dt2.Rows
                If (rw2(1).ToString = "OUI") Then
                    If (rw2(3).ToString = "OUI") Then
                        If (Montant >= CDec(rw2(0)) And Montant <= CDec(rw2(2))) Then
                            Revue = rw2(4).ToString
                            KodSeuil = rw2(6)
                        End If
                    Else
                        If (rw2(2).ToString <> "NL" And rw2(2).ToString <> "TM") Then
                            If (Montant >= CDec(rw2(0)) And Montant < CDec(rw2(2))) Then
                                Revue = rw2(4).ToString
                                KodSeuil = rw2(6)
                            End If
                        ElseIf (rw2(2).ToString = "NL") Then
                            If (Montant >= CDec(rw2(0))) Then
                                Revue = rw2(4).ToString
                                KodSeuil = rw2(6)
                            End If
                        End If
                    End If
                Else

                    If (rw2(3).ToString = "OUI") Then
                        If (Montant > CDec(rw2(0)) And Montant <= CDec(rw2(2))) Then
                            Revue = rw2(4).ToString
                            KodSeuil = rw2(6)
                        End If
                    Else
                        If (rw2(2).ToString <> "NL" And rw2(2).ToString <> "TM") Then
                            If (Montant > CDec(rw2(0)) And Montant < CDec(rw2(2))) Then
                                Revue = rw2(4).ToString
                                KodSeuil = rw2(6)
                            End If
                        ElseIf (rw2(2).ToString = "NL") Then
                            If (Montant >= CDec(rw2(0))) Then
                                Revue = rw2(4).ToString
                                KodSeuil = rw2(6)
                            End If
                        ElseIf (rw2(2).ToString = "TM") Then
                            Revue = rw2(4).ToString
                            KodSeuil = rw2(6)
                        End If
                    End If

                End If
                If (rw2(5).ToString <> "") Then
                    Revue = Revue & "*"
                End If
            Next

            DatSet = New DataSet
            query = "select * from T_Marche where RefMarche='" & CodMarche & "'"
            Cmd = New MySqlCommand(query, sqlconn)
            DatAdapt = New MySqlDataAdapter(Cmd)
            CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Fill(DatSet, "T_Marche")
            DatSet.Tables!T_Marche.Rows(0)!RevuePrioPost = Revue
            DatSet.Tables!T_Marche.Rows(0)!CodeProcAO = CodMethodCache.Text
            DatSet.Tables!T_Marche.Rows(0)!CodeSeuil = KodSeuil
            DatAdapt.Update(DatSet, "T_Marche")
            DatSet.Clear()

            ' On ecrase le plan du marché
            query = "DELETE from T_PlanMarche where RefMarche='" & CodMarche & "'"
            ExecuteNonQuery(query)

            ' On recupère les étapes avec le ccode projet, le type marché en ordre
            Dim LesKodEtapes(50) As Decimal
            Dim NbEtp As Decimal = 0
            query = "select RefEtape from T_EtapeMarche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='" & TypeRessource & "' order by NumeroOrdre"
            Dim dt3 As DataTable = ExcecuteSelectQuery(query)
            For Each rw3 In dt3.Rows
                LesKodEtapes(NbEtp) = rw3(0)
                NbEtp = NbEtp + 1
            Next

            ' on actualise le plan marché avec code marché, code etape, code méthode et numero ordre
            For i As Integer = 0 To NbEtp - 1
                DatSet = New DataSet
                query = "select * from T_PlanMarche"
                Cmd = New MySqlCommand(query, sqlconn)
                DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "T_PlanMarche")
                Dim DatTable = DatSet.Tables("T_PlanMarche")
                Dim DatRow = DatSet.Tables("T_PlanMarche").NewRow()

                DatRow("RefEtape") = LesKodEtapes(i)
                DatRow("RefMarche") = CodMarche
                DatRow("NumeroOrdre") = (i + 1).ToString

                DatSet.Tables("T_PlanMarche").Rows.Add(DatRow)
                CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "T_PlanMarche")
                DatSet.Clear()

            Next
            BDQUIT(sqlconn)

        End If
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.Close()
    End Sub

    Private Sub DialogMethodeConsult_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RempCmbMethod()
        CodMarche = CInt(ReponseDialog)
        ReponseDialog = ""

        Dim Existe As Boolean = False
        query = "select * from T_PlanMarche where RefMarche='" & CodMarche & "' and DebutPrevu<>''"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Existe = True
        Next

        If (Existe = True) Then
            MsgBox("Marché en cours d'utilisation.", MsgBoxStyle.Exclamation)
            Me.Close()
        End If

    End Sub
    Private Sub RempCmbMethod()

        query = "select AbregeAO from T_ProcAO where TypeMarcheAO='" & TypeRessource & "' and CodeProjet='" & ProjetEnCours & "'"
        CmbMethode.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbMethode.Items.Add(rw(0))
        Next

    End Sub

    Private Sub CmbMethode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbMethode.SelectedIndexChanged
        RechercheLabel()

    End Sub
    Private Sub RechercheLabel()

        query = "select LibelleAO,CodeProcAO from T_ProcAO where TypeMarcheAO='" & TypeRessource & "' and AbregeAO='" & CmbMethode.Text & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Dim Libi As String = rw(0)
            TxtMethode.Text = MettreApost(Libi)
            CodMethodCache.Text = rw(1)
        Next

    End Sub
End Class