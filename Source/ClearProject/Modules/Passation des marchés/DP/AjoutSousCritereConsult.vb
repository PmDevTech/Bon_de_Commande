Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient

Public Class AjoutSousCritereConsult

    Private Sub AjoutSousCritereConsult_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        MajListeCritere()
        Initialiser()
        CmbCritere.Focus()
    End Sub

    Private Sub Initialiser()
        TxtSousCritere.Text = ""
        TxtNote.Text = ""
        CmbCritere.Text = ""
    End Sub

    Private Sub MajListeCritere()

        ' 1er niveau ****************************
        CmbCritere.Properties.Items.Clear()
        query = "select RefCritere, IntituleCritere, PointCritere, CodeCritere from T_DP_CritereEval where NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='0' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbCritere.Properties.Items.Add(rw("CodeCritere").ToString & "/  " & MettreApost(rw("IntituleCritere").ToString))

            ' 2eme niveau ********************************
            query = "select RefCritere,IntituleCritere,PointCritere,TypeCritere,CodeCritere from T_DP_CritereEval where NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='" & rw("RefCritere").ToString & "' and TypeCritere<>'Bareme' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw1 As DataRow In dt1.Rows
                CmbCritere.Properties.Items.Add(rw1("CodeCritere").ToString & "/  " & MettreApost(rw1("IntituleCritere").ToString))

                ' 3eme niveau **************************************
                query = "select RefCritere,IntituleCritere,PointCritere,TypeCritere,CodeCritere from T_DP_CritereEval where NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='" & rw1("RefCritere").ToString & "' and TypeCritere<>'Bareme' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                For Each rw2 As DataRow In dt2.Rows
                    CmbCritere.Properties.Items.Add(rw2("CodeCritere").ToString & "/  " & MettreApost(rw2("IntituleCritere").ToString))
                Next
            Next
        Next
        CmbCritere.Properties.Items.Add("Ajouter nouveau critère")
    End Sub


    Private Sub CmbCritere_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCritere.SelectedValueChanged
        If (CmbCritere.SelectedText = "Ajouter nouveau critère") Then
            Dialog_form(AjoutCritereConsult)
            MajListeCritere()
        ElseIf (CmbCritere.SelectedText <> "") Then

            Dim codeTitre() As String = CmbCritere.SelectedText.Split("/"c)

            Dim lgNiv() As String = codeTitre(0).Split("."c)

            query = "select PointCritere, TypeCritere, CritereParent from T_DP_CritereEval where NumeroDp='" & EnleverApost(ReponseDialog) & "' and CodeCritere='" & codeTitre(0) & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)

            For Each rw As DataRow In dt.Rows
                If (rw("TypeCritere").ToString = "Note") Then
                    ChkNote.Checked = False
                    ChkNote.Enabled = False
                    ChkEtiquette.Checked = False
                    ChkEtiquette.Enabled = False

                    ChkBareme.Checked = True
                    ChkBareme.Enabled = True
                    TxtSousCritere.Text = ""
                ElseIf rw("CritereParent").ToString = "0" And rw("TypeCritere").ToString = "Etiquette" Then
                    ChkBareme.Checked = False
                    ChkBareme.Enabled = False

                    ChkNote.Checked = False
                    ChkNote.Enabled = True
                    ChkEtiquette.Checked = True
                    ChkEtiquette.Enabled = True
                    TxtSousCritere.Text = ""
                Else
                    ChkBareme.Checked = False
                    ChkBareme.Enabled = False
                    ChkEtiquette.Checked = False
                    ChkEtiquette.Enabled = False

                    ChkNote.Checked = True
                    ChkNote.Enabled = True
                    TxtSousCritere.Text = ""
                End If
            Next
        Else
            ChkBareme.Checked = False
            ChkBareme.Enabled = False
            ChkEtiquette.Checked = False
            ChkEtiquette.Enabled = False

            ChkNote.Checked = True
            ChkNote.Enabled = True
            TxtSousCritere.Text = ""
        End If
    End Sub

    Private Sub ChkEtiquette_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkEtiquette.CheckedChanged
        If (ChkEtiquette.Checked = True) Then
            LblInfo.Text = "Sous critère regroupant des sous critères d'évaluation"
            TxtNote.Enabled = False
            TxtNote.ResetText()
        End If
    End Sub

    Private Sub ChkBareme_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkBareme.CheckedChanged
        If (ChkBareme.Checked = True) Then
            LblInfo.Text = "Sous critère détaillant la note à attribuer en fonction des acquis du consultant"
            TxtNote.Enabled = True
        End If
    End Sub

    Private Sub ChkNote_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkNote.CheckedChanged
        If (ChkNote.Checked = True) Then
            LblInfo.Text = "Sous critère d'évaluation"
            TxtNote.Enabled = True
            If Not IsNumeric(TxtNote.Text) Then TxtNote.ResetText()
        End If
    End Sub

    Private Sub BtQuitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtQuitter.Click
        Me.Close()
    End Sub

    Private Sub BtAjoutCritere_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjoutCritere.Click
        If (CmbCritere.SelectedIndex <> -1 And ReponseDialog <> "") Then

            Dim typeCritere As String = "Note"
            Dim NvelleNoteSaisie As String = ""
            Dim Niveau As Integer = 0
            Dim SomPointsMessage As Decimal = 0

            If (ChkEtiquette.Checked = True) Then
                typeCritere = "Etiquette"
            ElseIf (ChkBareme.Checked = True) Then
                typeCritere = "Bareme"
            End If

            If TxtSousCritere.Text = "" Then
                If typeCritere = "Note" Or typeCritere = "Etiquette" Then
                    SuccesMsg("Veuillez saisir le libelle du sous critère")
                Else
                    SuccesMsg("Veuillez saisir le libelle du barème")
                End If
                TxtSousCritere.Focus()
                Exit Sub
            End If

            If ((ChkBareme.Checked = True) Or (ChkNote.Checked = True)) And TxtNote.Text.Trim = "" Then
                SuccesMsg("Veuillez saisir la note !")
                TxtNote.Select()
                Exit Sub
            End If

            If ChkNote.Checked = True And TxtNote.Text.Trim <> "" Then
                If (IsNumeric(TxtNote.Text.Replace(".", ","))) Then
                    NvelleNoteSaisie = CDec(TxtNote.Text.Replace(".", ","))
                Else
                    SuccesMsg("Saisie incorrect !")
                    TxtNote.Select()
                    Exit Sub
                End If
            End If

            Dim CodeCriterSelect() As String = CmbCritere.SelectedText.Split("/"c)
            Dim RefCriterSelect As Decimal = 0
            Dim NoteAuto As String = ""
            Dim NoteCriterSelect As Decimal = 0
            Dim CodeParentCrtSelect As Decimal = 0

            Dim TableNivo As Array = CodeCriterSelect(0).ToString.Split(".")
            Niveau = TableNivo.Length + 1

            'Info du critère selectionné
            query = "select RefCritere, PointAuto, PointCritere, CritereParent from T_DP_CritereEval where NumeroDp='" & EnleverApost(ReponseDialog) & "' and CodeCritere='" & CodeCriterSelect(0) & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)

            For Each rw As DataRow In dt.Rows
                RefCriterSelect = CInt(rw("RefCritere"))
                NoteAuto = rw("PointAuto").ToString
                If (rw("PointCritere").ToString <> "") Then
                    NoteCriterSelect = CDec(rw("PointCritere").ToString.Replace(".", ","))
                End If
                CodeParentCrtSelect = CInt(rw("CritereParent"))
            Next

            'Verifier si le libellé du critère existe dans le critère selectionné
            query = "select count(*) from T_DP_CritereEval where NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='" & RefCriterSelect.ToString & "' and IntituleCritere='" & EnleverApost(TxtSousCritere.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                SuccesMsg("Ce sous critère existe déjà !")
                TxtSousCritere.Focus()
                Exit Sub
            End If

            'Compté le nombre d'enfant du critère selection ****** pour la numerotation
            Dim nbSousCrit As Decimal = 0
            query = "select Count(*) from T_DP_CritereEval where NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='" & RefCriterSelect.ToString & "' and CodeProjet='" & ProjetEnCours & "'"
            nbSousCrit = Val(ExecuteScallar(query))
            nbSousCrit = nbSousCrit + 1
            Dim CodeCritere As String = CodeCriterSelect(0) & "." & nbSousCrit.ToString

            If ChkBareme.Checked = True Then 'Save baremes
                'Verifier dans le cas d'une valeur Monnetaire
                If IsNumeric(TxtNote.Text.Replace(".", ",")) Then
                    Dim PointCriteres = ExecuteScallar("select PointCritere from t_dp_critereeval where NumeroDp='" & EnleverApost(ReponseDialog) & "' and RefCritere='" & RefCriterSelect & "'").ToString.Replace(".", ",")
                    If PointCriteres <> "" Then
                        If Val(PointCriteres) < CDec(TxtNote.Text.Replace(".", ",")) Then
                            SuccesMsg("Nombre de points trop élevé !")
                            TxtNote.Select()
                            Exit Sub
                        End If
                    End If
                End If

                ExecuteNonQuery("Insert into T_DP_CritereEval values(NULL,'" & EnleverApost(ReponseDialog) & "','" & CodeCritere & "','" & EnleverApost(TxtSousCritere.Text) & "','" & typeCritere & "','" & RefCriterSelect & "','" & EnleverApost(TxtNote.Text.Replace(",", ".")) & "','" & NoteAuto & "', '" & ProjetEnCours & "','" & Niveau.ToString & "', NULL)")

                ElseIf ChkEtiquette.Checked = True Then 'Save Sous critère etiquettes
                If ConfirmMsg("La note de ce critère sera la somme des notes de ses sous critères. Voulez-vous continuer ?") = DialogResult.No Then
                    Exit Sub
                End If
                ExecuteNonQuery("Insert into T_DP_CritereEval values(NULL,'" & EnleverApost(ReponseDialog) & "','" & CodeCritere & "','" & EnleverApost(TxtSousCritere.Text) & "','" & typeCritere & "','" & RefCriterSelect & "','','" & NoteAuto & "', '" & ProjetEnCours & "','" & Niveau.ToString & "', NULL)")

            ElseIf ChkNote.Checked = True Then 'Save critere note
                'verification du depassement de 100 points
                Dim PointsCriteres As String = ""
                PointsCriteres = ExecuteScallar("select SUM(PointCritere) from T_DP_CritereEval where CodeProjet='" & ProjetEnCours & "' and NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='0' and PointCritere<>''").ToString.Replace(".", ",")
                If PointsCriteres <> "" Then SomPointsMessage = CDec(PointsCriteres)
                If NvelleNoteSaisie <> "" Then SomPointsMessage += CDec(NvelleNoteSaisie)

                If SomPointsMessage > 100 Then
                    SuccesMsg("Le total des points des critères d'évaluation ne doit pas excéder 100 points")
                    Exit Sub
                End If

                'insertion du nouveau critère saisi
                ExecuteNonQuery("Insert into T_DP_CritereEval values(NULL,'" & EnleverApost(ReponseDialog) & "','" & CodeCritere & "','" & EnleverApost(TxtSousCritere.Text) & "','" & typeCritere & "','" & RefCriterSelect & "','" & NvelleNoteSaisie.Replace(",", ".") & "','OUI', '" & ProjetEnCours & "','" & Niveau.ToString & "', NULL)")

                Dim MJPtsCrteresParent1 As String = ""
                Dim MJPtsCrteresParent2 As String = ""
                'MJ point sous critère
                MJPtsCrteresParent2 = ExecuteScallar("select SUM(PointCritere) from T_DP_CritereEval where NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='" & RefCriterSelect & "' and PointCritere<>'' and CodeProjet='" & ProjetEnCours & "'")
                ExecuteNonQuery("update T_DP_CritereEval set PointCritere='" & MJPtsCrteresParent2.ToString.Replace(",", ".") & "' where NumeroDp='" & EnleverApost(ReponseDialog) & "' and RefCritere='" & RefCriterSelect & "' and CodeProjet='" & ProjetEnCours & "'")
                'MJ point critères parents
                If CodeParentCrtSelect.ToString <> "0" Then
                    MJPtsCrteresParent1 = ExecuteScallar("select SUM(PointCritere) from T_DP_CritereEval where NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='" & CodeParentCrtSelect & "' and PointCritere<>'' and CodeProjet='" & ProjetEnCours & "'")
                    ExecuteNonQuery("update T_DP_CritereEval set PointCritere='" & MJPtsCrteresParent1.ToString.Replace(",", ".") & "' where NumeroDp='" & EnleverApost(ReponseDialog) & "' and RefCritere='" & CodeParentCrtSelect & "' and CodeProjet='" & ProjetEnCours & "'")
                End If
            End If

            NewDp.MajGridEvaluation()
            MajListeCritere()
            TxtSousCritere.Text = ""
            TxtNote.Text = ""
            TxtSousCritere.Focus()
        Else
            SuccesMsg("Veuillez selectionné un element dans la liste")
            CmbCritere.Select()
        End If
    End Sub

    'Avant
#Region "Fonctionnement d'avant"

    'Private Sub BtAjoutCritere_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjoutCritere.Click
    '    If (CmbCritere.SelectedIndex <> -1 And ReponseDialog <> "") Then

    '        Dim typeCritere As String = "Note"
    '        Dim NvelleNoteSaisie As String = ""
    '        Dim Niveau As Integer = 0
    '        Dim SomPointsMessage As Decimal = 0

    '        If (ChkEtiquette.Checked = True) Then
    '            typeCritere = "Etiquette"
    '        ElseIf (ChkBareme.Checked = True) Then
    '            typeCritere = "Bareme"
    '        End If

    '        If TxtSousCritere.Text = "" Then
    '            If typeCritere = "Note" Or typeCritere = "Etiquette" Then
    '                SuccesMsg("Veuillez saisir le libelle du sous critère")
    '            Else
    '                SuccesMsg("Veuillez saisir le libelle du barème")
    '            End If
    '            TxtSousCritere.Focus()
    '            Exit Sub
    '        End If

    '        If ((ChkBareme.Checked = True) Or (ChkNote.Checked = True)) And TxtNote.Text.Trim = "" Then
    '            SuccesMsg("Veuillez saisir la note !")
    '            TxtNote.Select()
    '            Exit Sub
    '        End If

    '        If ChkNote.Checked = True And TxtNote.Text.Trim <> "" Then
    '            If (IsNumeric(TxtNote.Text.Replace(".", ","))) Then
    '                NvelleNoteSaisie = CDec(TxtNote.Text.Replace(".", ","))
    '            Else
    '                SuccesMsg("Saisie incorrect !")
    '                TxtNote.Select()
    '                Exit Sub
    '            End If
    '        End If

    '        Dim CodeCriterSelect() As String = CmbCritere.SelectedText.Split("/"c)
    '        Dim RefCriterSelect As Decimal = 0
    '        Dim NoteAuto As String = ""
    '        Dim NoteCriterSelect As Decimal = 0
    '        Dim CodeParentCrtSelect As Decimal = 0

    '        Dim TableNivo As Array = CodeCriterSelect(0).ToString.Split(".")
    '        Niveau = TableNivo.Length + 1

    '        'Info du critère selectionné
    '        query = "select RefCritere, PointAuto, PointCritere, CritereParent from T_DP_CritereEval where NumeroDp='" & EnleverApost(ReponseDialog) & "' and CodeCritere='" & CodeCriterSelect(0) & "' and CodeProjet='" & ProjetEnCours & "'"
    '        Dim dt As DataTable = ExcecuteSelectQuery(query)

    '        For Each rw As DataRow In dt.Rows
    '            RefCriterSelect = CInt(rw("RefCritere"))
    '            NoteAuto = rw("PointAuto").ToString
    '            If (rw("PointCritere").ToString <> "") Then
    '                NoteCriterSelect = CDec(rw("PointCritere").ToString.Replace(".", ","))
    '            End If
    '            CodeParentCrtSelect = CInt(rw("CritereParent"))
    '        Next

    '        'Verifier si le libellé du critère existe dans le critère selectionné
    '        query = "select count(*) from T_DP_CritereEval where NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='" & RefCriterSelect.ToString & "' and IntituleCritere='" & EnleverApost(TxtSousCritere.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
    '        If Val(ExecuteScallar(query)) > 0 Then
    '            SuccesMsg("Ce sous critère existe déjà !")
    '            TxtSousCritere.Focus()
    '            Exit Sub
    '        End If

    '        'Compté le nombre d'enfant du critère selection ****** pour la numerotation
    '        Dim nbSousCrit As Decimal = 0
    '        query = "select Count(*) from T_DP_CritereEval where NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='" & RefCriterSelect.ToString & "' and CodeProjet='" & ProjetEnCours & "'"
    '        nbSousCrit = Val(ExecuteScallar(query))

    '        'verification du depassement de 100 points
    '        If (ChkNote.Checked = True) Then 'Or (ChkEtiquette.Checked = True) Then
    '            Dim PointsCriteres As String = ""
    '            PointsCriteres = ExecuteScallar("select SUM(PointCritere) from T_DP_CritereEval where CodeProjet='" & ProjetEnCours & "' and NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='0' and PointCritere<>''").ToString.Replace(".", ",")
    '            If PointsCriteres <> "" Then SomPointsMessage = CDec(PointsCriteres)
    '            If NvelleNoteSaisie <> "" Then SomPointsMessage += CDec(NvelleNoteSaisie)
    '        End If


    '        'vérification des points ************************* somme des points des enfants du critere selectionné
    '        Dim SumPtEfantCrtSelect As Decimal = 0
    '        Dim premierCrit As String = ""
    '        query = "select SUM(PointCritere) from T_DP_CritereEval where NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='" & RefCriterSelect.ToString & "' and PointCritere<>'' and CodeProjet='" & ProjetEnCours & "'"
    '        premierCrit = ExecuteScallar(query).ToString.Replace(".", ",")
    '        If premierCrit <> "" Then SumPtEfantCrtSelect = CDec(premierCrit)

    '        Dim MjrPtCrtSelect As Boolean = False

    '        SumPtEfantCrtSelect = SumPtEfantCrtSelect + CDec(NvelleNoteSaisie) 'nouvelle note saise

    '        If NoteCriterSelect.ToString = "0" Then
    '            MjrPtCrtSelect = True
    '        Else
    '            If (SumPtEfantCrtSelect > NoteCriterSelect And typeCritere <> "Bareme") Then
    '                If ConfirmMsg("Dépassement du nombre de point total !" & vbNewLine & "Si vous voulez continuer l'enregistrement," & vbNewLine & "le nombre total de points sera recalculé." & vbNewLine & "Voulez-vous poursuivre l'enregistrement ?") = DialogResult.Yes Then
    '                    'Verification depassement des 100 ponts
    '                    If SomPointsMessage > 100 Then
    '                        SuccesMsg("Le total des points des critères d'évaluation ne doit pas excéder 100 points")
    '                        Exit Sub
    '                    End If

    '                    MjrPtCrtSelect = True
    '                    NoteAuto = "OUI"
    '                Else
    '                    TxtNote.Focus()
    '                    Exit Sub
    '                End If
    '            ElseIf (typeCritere = "Bareme" And NvelleNoteSaisie > NoteCriterSelect) Then
    '                SuccesMsg("Nombre de points trop élevé !")
    '                TxtNote.Focus()
    '                Exit Sub
    '            End If
    '        End If

    '        nbSousCrit = nbSousCrit + 1
    '        Dim CodeCritere As String = CodeCriterSelect(0) & "." & nbSousCrit.ToString

    '        'insertion du nouveau critère saisi
    '        query = "Insert into T_DP_CritereEval values(NULL,'" & EnleverApost(ReponseDialog) & "','" & CodeCritere & "','" & EnleverApost(TxtSousCritere.Text) & "','" & typeCritere & "','" & RefCriterSelect & "','" & NvelleNoteSaisie.Replace(",", ".") & "','" & NoteAuto & "', '" & ProjetEnCours & "','" & Niveau.ToString & "', NULL)"
    '        ExecuteNonQuery(query)

    '        'Mise a jours des points du critère selectionné
    '        If MjrPtCrtSelect = True Then
    '            query = "update T_DP_CritereEval set PointCritere='" & SumPtEfantCrtSelect.ToString.Replace(",", ".") & "' where NumeroDp='" & EnleverApost(ReponseDialog) & "' and RefCritere='" & RefCriterSelect.ToString & "' and CodeProjet='" & ProjetEnCours & "'"
    '            ExecuteNonQuery(query)
    '        End If

    '        'Mise a jours des points du parents du critère selectionné
    '        If CodeParentCrtSelect.ToString <> "0" Then
    '            If MjrPtCrtSelect = True Then
    '                'Somme des points des enfants du parent du critères selectionné
    '                Dim SommePointas As String = ""
    '                query = "select SUM(PointCritere) from T_DP_CritereEval where NumeroDp='" & EnleverApost(ReponseDialog) & "' and CritereParent='" & CodeParentCrtSelect.ToString & "' and PointCritere<>'' and CodeProjet='" & ProjetEnCours & "'"
    '                SommePointas = ExecuteScallar(query)

    '                query = "update T_DP_CritereEval set PointCritere='" & SommePointas.ToString.Replace(",", ".") & "' where NumeroDp='" & EnleverApost(ReponseDialog) & "' and RefCritere='" & CodeParentCrtSelect.ToString & "' and CodeProjet='" & ProjetEnCours & "'"
    '                ExecuteNonQuery(query)
    '            End If
    '        End If

    '        NewDp.MajGridEvaluation()
    '        MajListeCritere()
    '        TxtSousCritere.Text = ""
    '        TxtNote.Text = ""
    '        TxtSousCritere.Focus()
    '    Else
    '        SuccesMsg("Veuillez selectionné un element dans la liste")
    '        CmbCritere.Select()
    '    End If
    'End Sub
#End Region

End Class