Public Class ModifEcriture

    Private Sub XtraForm1_Load(sender As System.Object, e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        'date
        Dim datd As String = ExerciceComptable.Rows(0).Item("datedebut")
        Dim datf As String = ExerciceComptable.Rows(0).Item("datefin")

        'query = "select datedebut, datefin from T_COMP_EXERCICE where encours='1'"
        'Dim dt As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt.Rows
        '    datd = CDate(rw(0)).ToString("dd/MM/yyyy")
        '    datf = CDate(rw(1)).ToString("dd/MM/yyyy")
        'Next


        'conversion de la date
        Dim str(3) As String
        str = datd.ToString.Split("/")
        Dim tempdt As String = String.Empty
        For j As Integer = 2 To 0 Step -1
            tempdt += str(j) & "-"
        Next
        tempdt = tempdt.Substring(0, 10)

        Dim str1(3) As String
        str1 = datf.ToString.Split("/")
        Dim tempdt1 As String = String.Empty
        For j As Integer = 2 To 0 Step -1
            tempdt1 += str1(j) & "-"
        Next
        tempdt1 = tempdt1.Substring(0, 10)

        'remplir ecriture
        CombE.Properties.Items.Clear()
        query = "select CODE_E from T_COMP_ECRITURE C, T_COMP_EXERCICE E WHERE E.id_exercice=C.id_exercice and E.encours='1' ORDER BY C.CODE_E"
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        For Each rw1 As DataRow In dt1.Rows
            CombE.Properties.Items.Add(rw1(0).ToString)
        Next

        'remplir activite

        CombA.Properties.Items.Clear()
        query = "select LibelleCourt from T_PARTITION where { fn LENGTH(LibelleCourt) } >= 5 and dateDebutPartition >='" & tempdt & "' AND dateFinPartition <='" & tempdt1 & "' ORDER BY LibelleCourt"
        Dim dt2 As DataTable = ExcecuteSelectQuery(query)
        For Each rw2 As DataRow In dt2.Rows
            CombA.Properties.Items.Add(rw2(0).ToString)
        Next

    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        Try

           query= "update t_comp_activite set LibelleCourt='" & CombA.Text & "' WHERE CODE_E='" & CombE.Text & "'"
            ExecuteNonQuery(query)

           query= "update t_comp_activite_payer set LibelleCourt='" & CombA.Text & "' WHERE CODE_E='" & CombE.Text & "'"
            ExecuteNonQuery(query)

            MsgBox("Modification Effectuée avec succès !!!", MsgBoxStyle.Information, "ClearProject")

        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub SimpleButton2_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub
End Class