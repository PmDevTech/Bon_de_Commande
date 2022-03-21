Public Class ModifMethode
    Public IDPlan As Decimal = 0
    Public RefMarcheMod As Decimal = 0
    Dim CodeProcAO() As Decimal
    Dim CodeAncinneMethode As String = ""
    Dim typeMarche As String = ""
    Dim InitialeBailleur As String = ""

    Private Sub ModifMethode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        query = "SELECT m.Convention_ChefFile, m.TypeMarche, m.CodeProcAO, B.InitialeBailleur FROM t_marche as m, t_bailleur as B, t_convention as C WHERE m.Convention_ChefFile=C.CodeConvention and C.CodeBailleur=B.CodeBailleur AND m.RefMarche='" & RefMarcheMod & "' AND m.RefPPM ='" & IDPlan & "' AND m.CodeProjet='" & ProjetEnCours & "'"
        Dim dt = ExcecuteSelectQuery(query)

        cmbMethode.ResetText()
        cmbMethode.Properties.Items.Clear()
        For Each rw In dt.Rows
            typeMarche = rw("TypeMarche").ToString
            CodeAncinneMethode = rw("CodeProcAO").ToString
            InitialeBailleur = rw("InitialeBailleur").ToString

            query = "select P.CodeProcAO, P.AbregeAO from T_ProcAO as P, T_Seuil as S where P.CodeProcAO=S.CodeProcAO and P.TypeMarcheAO='" & rw("TypeMarche").ToString & "' and P.CodeProjet='" & ProjetEnCours & "' and S.Bailleur='" & rw("InitialeBailleur").ToString & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            ReDim CodeProcAO(dt1.Rows.Count)
            Dim i As Integer = 0
            For Each rw1 As DataRow In dt1.Rows
                If rw1("CodeProcAO").ToString = CodeAncinneMethode Then
                    cmbMethode.Text = rw1("AbregeAO").ToString
                End If
                cmbMethode.Properties.Items.Add(MettreApost(rw1("AbregeAO").ToString))
                CodeProcAO(i) = rw1("CodeProcAO").ToString
                i += 1
            Next
        Next
    End Sub

    Private Sub BtEnregComm_Click(sender As Object, e As EventArgs) Handles BtEnregComm.Click
        If cmbMethode.SelectedIndex <> -1 Then
            If CodeAncinneMethode <> CodeProcAO(cmbMethode.SelectedIndex) Then

                'Verifier s'il existe des dates de prevision ou de réalisation des étapes
                If Val(ExecuteScallar("SELECT COUNT(*) from t_planmarche where RefMarche='" & RefMarcheMod & "'")) > 0 Then
                    If ConfirmMsg("La modification de la méthode supprimera toutes les dates de previsions et de réalisations des étapes de ce marché." & vbNewLine & "Êtes-vous sûrs de vouloir modifier la méthode?") = DialogResult.No Then
                        Exit Sub
                    End If
                    ExecuteNonQuery("delete from t_planmarche WHERE RefMarche='" & RefMarcheMod & "'")
                End If
            End If

            Dim CodeSeuil As Decimal = 0
            Dim TypeExamenAO As String = ""
            query = "select S.CodeSeuil, S.TypeExamenAO from T_ProcAO as P,T_Seuil as S where P.CodeProcAO=S.CodeProcAO and P.TypeMarcheAO='" & EnleverApost(typeMarche.ToString) & "' and P.CodeProjet='" & ProjetEnCours & "' and S.Bailleur='" & EnleverApost(InitialeBailleur) & "' AND P.RechAuto='OUI' and P.CodeProcAO='" & CodeProcAO(cmbMethode.SelectedIndex) & "'"
            Dim dtt As DataTable = ExcecuteSelectQuery(query)
            For Each rws In dtt.Rows
                CodeSeuil = rws("CodeSeuil")
                TypeExamenAO = rws("TypeExamenAO").ToString
            Next

            ExecuteNonQuery("UPDATE T_Marche SET CodeProcAO='" & CodeProcAO(cmbMethode.SelectedIndex) & "', MethodeMarche ='" & CodeProcAO(cmbMethode.SelectedIndex) & "',RevuePrioPost ='" & TypeExamenAO & "', CodeSeuil ='" & CodeSeuil & "', DerniereMaj ='" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "' WHERE RefPPM='" & IDPlan & "' AND RefMarche='" & RefMarcheMod & "'")
            SuccesMsg("Méthode modifiée avec succès.")
            Me.Close()
            Me.DialogResult = DialogResult.Yes
        Else
            SuccesMsg("Veuillez choisir une méthode de passation de marché.")
        End If
    End Sub
End Class