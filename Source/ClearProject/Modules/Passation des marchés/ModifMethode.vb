Public Class ModifMethode
    Public IDPlan As Integer = 0
    Public RefMarcheMod As Integer = 0
    Dim CodeProcAO() As Integer

    Private Sub ModifMethode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        query = "SELECT Convention_ChefFile,TypeMarche, CodeProcAO FROM t_marche WHERE RefMarche='" & RefMarcheMod & "' AND RefPPM ='" & IDPlan & "' AND CodeProjet='" & ProjetEnCours & "'"
        Dim dt = ExcecuteSelectQuery(query)
        Dim InitialeBailleur As String = ""
        Dim typeMarche As String = ""
        Dim CodeMethode As String = ""
        For Each rw In dt.Rows
            query = "SELECT B.InitialeBailleur FROM t_bailleur as B, t_convention as C WHERE C.CodeBailleur=B.CodeBailleur AND C.CodeConvention='" & rw("Convention_ChefFile").ToString & "'"
            InitialeBailleur = ExecuteScallar(query)
            typeMarche = rw("TypeMarche").ToString
            CodeMethode = rw("CodeProcAO").ToString
        Next
        cmbMethode.Properties.Items.Clear()
        query = "select P.CodeProcAO,P.AbregeAO from T_ProcAO as P,T_Seuil as S where P.CodeProcAO=S.CodeProcAO and P.TypeMarcheAO='" & typeMarche & "' and P.CodeProjet='" & ProjetEnCours & "' and S.Bailleur='" & InitialeBailleur & "'"
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        ReDim CodeProcAO(dt1.Rows.Count)
        Dim i As Integer = 0
        For Each rw1 As DataRow In dt1.Rows
            If rw1("CodeProcAO").ToString = CodeMethode Then
                cmbMethode.Text = rw1("AbregeAO").ToString
            End If
            cmbMethode.Properties.Items.Add(MettreApost(rw1("AbregeAO").ToString))
            CodeProcAO(i) = rw1("CodeProcAO").ToString
            i += 1
        Next
    End Sub

    Private Sub BtEnregComm_Click(sender As Object, e As EventArgs) Handles BtEnregComm.Click
        If cmbMethode.SelectedIndex = -1 Then
            SuccesMsg("Veuillez choisir une méthode de passation de marché.")
        End If
        If cmbMethode.SelectedIndex <> -1 Then
            query = "UPDATE t_marche SET CodeProcAO='" & CodeProcAO(cmbMethode.SelectedIndex) & "', DerniereMaj='" & Now.ToString & "' WHERE RefPPM='" & IDPlan & "' AND RefMarche='" & RefMarcheMod & "'"
            ExecuteNonQuery(query)
            SuccesMsg("Methode modifiée avec succès.")
            Me.Close()
            Me.DialogResult = DialogResult.Yes
        End If
    End Sub
End Class