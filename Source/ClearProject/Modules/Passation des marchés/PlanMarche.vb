Imports Microsoft
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.Math
Imports System.Drawing.Printing
Imports System.IO
Imports ClearProject.PassationMarche
Imports Microsoft.Office.Interop

Public Class PlanMarche

    Dim NewPlan As Boolean = False
    Dim VoirPlan As Boolean = False
    Dim RefPPM As String()
    Dim CurrentRefPPM As Integer = -1
    Public ModePPM As String = ""
    Public ElaboPPM As String = ""
    Dim RefMarche As String
    Dim ligne As Integer
    Dim NumeroDAO As String
    Dim ChefFile As String = ""


    Private Sub PlanMarche_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RemplirMarcheAConsulter()
        RemplirDevise()
        query = "SELECT ModePlanMarche FROM t_paramtechprojet WHERE CodeProjet='" & ProjetEnCours & "'"
        ModePPM = ExecuteScallar(query)
        query = "SELECT B.InitialeBailleur, SUM(C.MontantConvention) as Montant, C.CodeConvention FROM t_convention as C, t_bailleur as B WHERE C.CodeBailleur=B.CodeBailleur GROUP by C.CodeBailleur ORDER by Montant DESC"
        Dim dtChefFile = ExcecuteSelectQuery(query)
        For Each rw In dtChefFile.Rows
            ChefFile = rw("CodeConvention").ToString
            Exit For
        Next
        If ModePPM = "Genere" Then
            gcNewPPSD.Visible = False
            'btAjout.Visible = False
            DateDebutMarche.Text = My.Computer.Clock.GmtTime.Date
            DateFinMarche.Text = My.Computer.Clock.GmtTime.Date
            query = "SELECT ElaboPPM FROM t_paramtechprojet WHERE CodeProjet='" & ProjetEnCours & "'"
            ElaboPPM = ExecuteScallar(query)
            If ElaboPPM = "Tous les bailleurs" Then
                CmbConvention.Visible = False
                BailleurConcerne.Visible = False
                Label13.Visible = False
                Label5.Visible = False
            Else
                Label13.Visible = True
                Label5.Visible = True
                CmbConvention.Visible = True
                BailleurConcerne.Visible = True
                RemplirBailleur()
            End If
            RemplirTypeMarche()
        ElseIf ModePPM = "PPSD" Then
            'btAjout.Visible = True
            gcNewPPSD.Visible = True
        End If


        CouleurTexteTot.Color = Color.White
        CouleurTotaux.Color = Color.Black
        CouleurTexte.Color = Color.Black
        CouleurTexte.Color = Color.Black
        CouleurSeparateur.Color = Color.White
        CouleurRealise.Color = Color.LightBlue
        CouleurPlan.Color = Color.LightBlue
    End Sub

    Private Sub ColDescription()

        GridPlanMarche.Rows.Clear()
        Dim NbCol As Decimal = GridPlanMarche.ColumnCount
        If (NbCol > 0) Then
            For i As Integer = 1 To NbCol - 1
                GridPlanMarche.Columns.Remove("A")
            Next
        End If
        'Dim ColonneRefMarche As New DataGridViewTextBoxColumn
        'With ColonneRefMarche
        '    .HeaderText = "RefMarche"
        '    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        '    .Name = "A"
        '    .Width = 50
        '    .ReadOnly = True
        '    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        '    .Frozen = True
        'End With
        'GridPlanMarche.Columns.Insert(1, ColonneRefMarche)

        Dim ColonneNum As New DataGridViewTextBoxColumn
        With ColonneNum
            .HeaderText = "N°"
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Name = "A"
            .Width = 50
            .ReadOnly = True
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Frozen = True
        End With
        GridPlanMarche.Columns.Insert(1, ColonneNum)

        Dim ColonneDesc As New DataGridViewTextBoxColumn
        With ColonneDesc
            .HeaderText = "Description"
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Name = "A"
            .Width = 250
            .ReadOnly = True
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Frozen = True
        End With
        GridPlanMarche.Columns.Insert(2, ColonneDesc)

    End Sub

    Private Sub DonneesDeBase(ByVal Num As Decimal, Optional AfficherCols As String = "")
        Dim Position As Decimal = 2
        If AfficherCols = "Bailleur" Then
            Position = Position + 1
            Dim ColonneBailleurs As New DataGridViewTextBoxColumn
            With ColonneBailleurs
                .HeaderText = "Bailleurs"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Name = "A"
                .Width = 120
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Frozen = False
                .ToolTipText = "Données de base"
            End With
            GridPlanMarche.Columns.Insert(Position, ColonneBailleurs)
            Position = Position + 1
            Dim ColonneConvetions As New DataGridViewTextBoxColumn
            With ColonneConvetions
                .HeaderText = "Conventions"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Name = "A"
                .Width = 120
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Frozen = False
                .ToolTipText = "Données de base"
            End With
            GridPlanMarche.Columns.Insert(Position, ColonneConvetions)
        End If
        If AfficherCols = "Convention" Then
            Position = Position + 1
            Dim ColonneConvetions As New DataGridViewTextBoxColumn
            With ColonneConvetions
                .HeaderText = "Conventions"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Name = "A"
                .Width = 120
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Frozen = False
                .ToolTipText = "Données de base"
            End With
            GridPlanMarche.Columns.Insert(Position, ColonneConvetions)
        End If
        Position = Position + 1
        Dim ColonneNumDao As New DataGridViewTextBoxColumn
        With ColonneNumDao
            If (Num = 2 Or Num = 1) Then
                .HeaderText = "Numéro de l'Appel d'Offre"
            ElseIf (Num = 3) Then
                .HeaderText = "Forfait ou Temps Passé"
            End If
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Name = "A"
            .Width = 120
            .ReadOnly = True
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Frozen = False
            .ToolTipText = "Données de base"
        End With
        GridPlanMarche.Columns.Insert(Position, ColonneNumDao)

        If (Num = 2 Or Num = 1) Then
            Position = Position + 1
            Dim ColonneNumLot As New DataGridViewTextBoxColumn
            With ColonneNumLot
                .HeaderText = "Numéro du Lot"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Name = "A"
                .Width = 120
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Frozen = False
                .ToolTipText = "Données de base"
            End With
            GridPlanMarche.Columns.Insert(Position, ColonneNumLot)
        End If

        Position = Position + 1
        Dim ColonneMontantUSD As New DataGridViewTextBoxColumn
        With ColonneMontantUSD
            .HeaderText = "Montant Estimatif en " & cmbDevise.Text
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Name = "A"
            .Width = 120
            .ReadOnly = True
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Frozen = False
            .ToolTipText = "Données de base"
        End With
        GridPlanMarche.Columns.Insert(Position, ColonneMontantUSD)

        Position = Position + 1
        Dim ColonneMontantCFA As New DataGridViewTextBoxColumn
        With ColonneMontantCFA
            .HeaderText = "Montant Estimatif en CFA"
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Name = "A"
            .Width = 120
            .ReadOnly = True
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Frozen = False
            .ToolTipText = "Données de base"
        End With
        GridPlanMarche.Columns.Insert(Position, ColonneMontantCFA)

        Position = Position + 1
        Dim ColonneMethode As New DataGridViewTextBoxColumn
        With ColonneMethode
            If (Num = 2 Or Num = 1) Then
                .HeaderText = "Méthode de Passation de Marche"
            ElseIf (Num = 3) Then
                .HeaderText = "Méthode de Sélection"
            End If
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Name = "A"
            .Width = 120
            .ReadOnly = True
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Frozen = False
            .ToolTipText = "Données de base"
        End With
        GridPlanMarche.Columns.Insert(Position, ColonneMethode)

        If (Num = 2 Or Num = 1) Then
            Position = Position + 1
            Dim ColonneQualification As New DataGridViewTextBoxColumn
            With ColonneQualification
                .HeaderText = "Pré ou Post Qualification"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Name = "A"
                .Width = 120
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Frozen = False
                .ToolTipText = "Données de base"
            End With
            GridPlanMarche.Columns.Insert(Position, ColonneQualification)
        End If

        Position = Position + 1
        Dim ColonneRevue As New DataGridViewTextBoxColumn
        With ColonneRevue
            .HeaderText = "Revue à Priori ou à Postériori"
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Name = "A"
            .Width = 120
            .ReadOnly = True
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Frozen = False
            .ToolTipText = "Données de base"
        End With
        GridPlanMarche.Columns.Insert(Position, ColonneRevue)

        Position = Position + 1
        Dim ColonnePlanRealise As New DataGridViewTextBoxColumn
        With ColonnePlanRealise
            .HeaderText = "Prévu" & vbNewLine & "/" & vbNewLine & "Réalisé"
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Name = "A"
            .Width = 120
            .ReadOnly = True
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Frozen = False
            .ToolTipText = "Données de base"
        End With
        GridPlanMarche.Columns.Insert(Position, ColonnePlanRealise)
    End Sub

    Private Sub AfficherLesAutresColonnes()


        Dim NumColo As Decimal = GridPlanMarche.ColumnCount - 1
        Dim ColonneGrid As New DataGridViewTextBoxColumn
        'With ColonneGrid
        '    Dim Titre As String = "Etape"
        '    .HeaderText = MettreApost(Titre)
        '    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        '    .Name = "A"
        '    .Width = 120
        '    .ReadOnly = True
        '    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        '    .Frozen = False
        'End With
        'GridPlanMarche.Columns.Add(ColonneGrid)

        Dim GroupInfo() As String = (MarcheAConsulter.Text).Split("_"c)
        Dim LeType As String = GroupInfo(0)
        NbreColoEtape = 0

        query = "select DISTINCT  TitreEtape from T_EtapeMarche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='" & LeType & "' order by NumeroOrdre"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            NbreColoEtape = NbreColoEtape + 1
            NumColo = NumColo + 1
            ColonneGrid = New DataGridViewTextBoxColumn
            With ColonneGrid
                Dim Titre As String = rw(0)
                .HeaderText = MettreApost(Titre)
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Name = "A"
                .Width = 120
                .ReadOnly = True
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Frozen = False
            End With
            GridPlanMarche.Columns.Insert(NumColo, ColonneGrid)
        Next
    End Sub
    Private Sub RemplirDevise()
        query = "select AbregeDevise from T_Devise "
        Dim dt0 = ExcecuteSelectQuery(query)
        cmbDevise.Text = ""
        cmbDevise.Properties.Items.Clear()
        For Each rw0 As DataRow In dt0.Rows
            If rw0("AbregeDevise") <> "FCFA" Then
                cmbDevise.Properties.Items.Add(rw0("AbregeDevise").ToString)
            End If
        Next
    End Sub

    Private Sub btGenererPlan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGenererPlan.Click

        'Ajout d'un nouveau plan ****************************************************************************************
        If NewPlan Then
            Dim erreur As Integer = 0
            If txtNumPlan.Text.Trim = "" Then
                erreur += 1
            End If
            If DateDebutMarche.Text = "" Then
                erreur += 1
            End If
            If DateFinMarche.Text="" Then
                erreur += 1
            End If
            If DateDebutMarche.Text.Trim().Length < 0 And DateFinMarche.Text.Trim().Length < 0 Then
                erreur += 1
            End If
            If cmbTypeMarche.SelectedIndex = -1 Then
                erreur += 1
            End If
            If ElaboPPM = "Bailleur" Then
                If BailleurConcerne.SelectedIndex = -1 Then
                    erreur += 1
                End If
                If BailleurConcerne.Text <> "" And CmbConvention.SelectedIndex = -1 Then
                    erreur += 1
                End If
            End If
            If erreur = 0 Then
                'si le numéro du plan existe déjà
                query = "SELECT COUNT(NumeroPlan) from t_ppm_marche WHERE NumeroPlan='" & EnleverApost(txtNumPlan.Text) & "'"
                Dim dtResult = ExecuteScallar(query)
                If dtResult > 0 Then
                    FailMsg("Le numero du plan saisie existe déjà.")
                    Exit Sub
                End If
                'Vérification de l'existance du plan en cours de création **************************************************
                Dim DejaFait As Boolean = False

                'query = "select * from T_Marche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='" & ChoixTypeMarche.Text & "' and PeriodeMarche='" & DateDebutMarche.Text & " - " & DateFinMarche.Text & "' and InitialeBailleur='" & BailleurConcerne.Text & "' and CodeConvention='" & CmbConvention.Text & "'"
                'Dim dt As DataTable = ExcecuteSelectQuery(query)
                'For Each rw As DataRow In dt.Rows
                '    DejaFait = True
                '    Exit For
                'Next

                ''Vérification du chevauchement des dates ********************************************************************
                Dim Chevauche As Boolean = False

                If ElaboPPM = "Tous les bailleurs" Then
                    query = "select PeriodeMarche,DescriptionMarche from T_Marche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='" & cmbTypeMarche.Text & "' AND ModePPM<>'PPSD'"
                    Dim dt As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        Dim GroupDate() As String = (rw("PeriodeMarche").ToString).Split(" "c)
                        Dim PeriodeDebut As Date = CDate(GroupDate(0))
                        Dim PeriodeFin As Date = CDate(GroupDate(2))
                        If ((Date.Compare(CDate(DateDebutMarche.Text), PeriodeDebut) >= 0 And Date.Compare(CDate(DateDebutMarche.Text), PeriodeFin) <= 0) Or (Date.Compare(CDate(DateFinMarche.Text), PeriodeDebut) >= 0 And Date.Compare(CDate(DateFinMarche.Text), PeriodeFin) <= 0)) Then
                            Chevauche = True
                            Exit For
                        End If
                    Next
                Else
                    query = "select PeriodeMarche,DescriptionMarche from T_Marche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='" & cmbTypeMarche.Text & "' and InitialeBailleur='" & BailleurConcerne.Text & "' and CodeConvention='" & CmbConvention.Text & "' AND ModePPM<>'PPSD'"
                    Dim dt As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        Dim GroupDate() As String = (rw("PeriodeMarche").ToString).Split(" "c)
                        Dim PeriodeDebut As Date = CDate(GroupDate(0))
                        Dim PeriodeFin As Date = CDate(GroupDate(2))
                        If ((Date.Compare(CDate(DateDebutMarche.Text), PeriodeDebut) >= 0 And Date.Compare(CDate(DateDebutMarche.Text), PeriodeFin) <= 0) Or (Date.Compare(CDate(DateFinMarche.Text), PeriodeDebut) >= 0 And Date.Compare(CDate(DateFinMarche.Text), PeriodeFin) <= 0)) Then
                            Chevauche = True
                            Exit For
                        End If
                    Next
                End If

                If (DejaFait = False And Chevauche = False) Then
                    If ElaboPPM = "Tous les bailleurs" Then
                        ColDescription()
                        If (cmbTypeMarche.Text = "Fournitures") Then
                            DonneesDeBase(2, "Bailleur")
                        ElseIf (cmbTypeMarche.Text = "Travaux") Then
                            DonneesDeBase(2, "Bailleur")
                        ElseIf (cmbTypeMarche.Text = "Consultants") Then
                            DonneesDeBase(3, "Bailleur")
                        Else
                            DonneesDeBase(2, "Bailleur")
                        End If
                    Else
                        ColDescription()
                        If (cmbTypeMarche.Text = "Fournitures") Then
                            DonneesDeBase(2)
                        ElseIf (cmbTypeMarche.Text = "Travaux") Then
                            DonneesDeBase(2)
                        ElseIf (cmbTypeMarche.Text = "Consultants") Then
                            DonneesDeBase(3)
                        Else
                            DonneesDeBase(2)
                        End If
                    End If


                    'AfficherLesAutresColonnes()

                    Dim result As Integer = -3
                    If ElaboPPM = "Tous les bailleurs" Then
                        query = "INSERT INTO t_ppm_marche VALUES(NULL,'" & EnleverApost(cmbTypeMarche.Text & "_" & DateDebutMarche.Text & " - " & DateFinMarche.Text & "_Tous_Bailleurs") & "','" & EnleverApost(cmbTypeMarche.Text) & "','" & DateDebutMarche.Text & " - " & DateFinMarche.Text & "','" & BailleurConcerne.Text & "','" & CmbConvention.Text & "','" & "Genere" & "','" & dateconvert(Now) & "','" & dateconvert(Now) & "','" & ProjetEnCours & "','" & CodeUtilisateur & "','" & txtNumPlan.Text & "','Tous les bailleurs')"
                    Else
                        query = "INSERT INTO t_ppm_marche VALUES(NULL,'" & EnleverApost(cmbTypeMarche.Text & "_" & DateDebutMarche.Text & " - " & DateFinMarche.Text & "_" & BailleurConcerne.Text & "_" & CmbConvention.Text) & "','" & EnleverApost(cmbTypeMarche.Text) & "','" & DateDebutMarche.Text & " - " & DateFinMarche.Text & "','" & BailleurConcerne.EnleverApostrophe & "','" & CmbConvention.EnleverApostrophe & "','" & "Genere" & "','" & dateconvert(Now) & "','" & dateconvert(Now) & "','" & ProjetEnCours & "','" & CodeUtilisateur & "','" & txtNumPlan.Text & "','Bailleur')"
                    End If
                    ExecuteNonQuery(query)

                    Dim LastIDPlan As Integer = Val(ExecuteScallar("SELECT MAX(RefPPM) FROM t_ppm_marche WHERE CodeProjet='" & ProjetEnCours & "' AND CodeUtils='" & CodeUtilisateur & "'"))
                    result = RechercherLesInfos(LastIDPlan)

                    If result = 0 Then
                        cmbDevise.Text = "US$"
                        RemplirMarcheAConsulter()
                        If ElaboPPM = "Tous les bailleurs" Then
                            MarcheAConsulter.Text = cmbTypeMarche.Text & "_" & DateDebutMarche.Text & " - " & DateFinMarche.Text & "_Tous_Bailleurs"
                        Else
                            MarcheAConsulter.Text = cmbTypeMarche.Text & "_" & DateDebutMarche.Text & " - " & DateFinMarche.Text & "_" & BailleurConcerne.Text & "_" & CmbConvention.Text
                        End If
                    Else
                        query = "DELETE FROM t_ppm_marche WHERE RefPPM='" & LastIDPlan & "'"
                        ExecuteNonQuery(query)
                        GridPlanMarche.Rows.Clear()
                        Dim NbCol As Decimal = GridPlanMarche.ColumnCount
                        If (NbCol > 0) Then
                            For i As Integer = 1 To NbCol - 1
                                GridPlanMarche.Columns.Remove("A")
                            Next
                        End If
                        Me.Cursor = Cursors.Default
                        If result <> 0 And result <> -1 Then
                            Me.Cursor = Cursors.Default
                            SuccesMsg("Aucune ressource trouvée dans la période du " & DateDebutMarche.Text & " au " & DateFinMarche.Text & ".")
                            Exit Sub
                        ElseIf result = -1 Then
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        End If
                    End If

                    'NewPlan = False
                    'VoirPlan = True
                    'btGenererPlan.Enabled = False
                    'BtNouveauPlan.Enabled = True
                    'BtConsulterPlan.Enabled = True
                    'BtSupprimerPlan.Enabled = True

                    'Label4.Enabled = True
                    'MarcheAConsulter.Enabled = True
                    'Label1.Enabled = False
                    'Label2.Enabled = False
                    'Label3.Enabled = False
                    'ChoixTypeMarche.Enabled = False
                    'DateDebutMarche.Enabled = False
                    'DateFinMarche.Enabled = False
                    'Label13.Enabled = False
                    'BailleurConcerne.Enabled = False
                    'CmbConvention.Enabled = False
                Else
                    FailMsg("Impossible de poursuivre la création de ce plan de marché." & vbNewLine & "Soit des marchés existent déjà ou la période chevauche une déjà existante.")
                End If

            Else
                SuccesMsg("Veuillez remplir correctement tous les champs.")
            End If
        End If

    End Sub

    Private Function RechercherLesInfos(RefPlan As Integer) As Integer

        Me.Cursor = Cursors.WaitCursor
        Dim JoursCompte As String = ""
        If LunDi.Checked Then JoursCompte = "Lun"
        If MarDi.Checked Then JoursCompte = JoursCompte & ";Mar"
        If MercreDi.Checked Then JoursCompte = JoursCompte & ";Mer"
        If JeuDi.Checked Then JoursCompte = JoursCompte & ";Jeu"
        If VendreDi.Checked <> True Then Else JoursCompte = JoursCompte & ";Ven"
        If SameDi.Checked Then JoursCompte = JoursCompte & ";Sam"
        If DimanChe.Checked Then JoursCompte = JoursCompte & ";Dim"

        Dim CodeTypeMarche As String = String.Empty
        If cmbTypeMarche.Text.ToLower() = "Consultants".ToLower() Then
            CodeTypeMarche = "CS"
        ElseIf cmbTypeMarche.Text.ToLower() = "Fournitures".ToLower() Then
            CodeTypeMarche = "FR"
        ElseIf cmbTypeMarche.Text.ToLower() = "Services autres que les services de consultants".ToLower() Then
            CodeTypeMarche = "SA"
        ElseIf cmbTypeMarche.Text.ToLower() = "Travaux".ToLower() Then
            CodeTypeMarche = "TX"
        End If
        Dim Periode As String = DateDebutMarche.Text & " - " & DateFinMarche.Text

        'Recuperation des comptes à marché qui sont sur les activités de la période
        Dim dtAllocation As New DataTable
        If ElaboPPM = "Tous les bailleurs" Then
            query = "select DISTINCT P.NumeroComptable, B.InitialeBailleur, SUM(R.MontantBailleur) as MontantBailleurs from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebutMarche.Text) & "' AND DateDebutPartition<='" & dateconvert(DateFinMarche.Text) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & cmbTypeMarche.Text & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and R.MontantBailleur<>'0' GROUP BY P.NumeroComptable"
            dtAllocation = ExcecuteSelectQuery(query)
        Else
            'Recuperation des comptes à marché qui sont sur les activités de la période, du bailleur et de la convention indiqué
            query = "select DISTINCT P.NumeroComptable,B.InitialeBailleur from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebutMarche.Text) & "' AND DateDebutPartition<='" & dateconvert(DateFinMarche.Text) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & cmbTypeMarche.Text & "' and B.CodeBailleur=R.CodeBailleur  and R.RefBesoinPartition=P.RefBesoinPartition and B.InitialeBailleur='" & BailleurConcerne.Text & "' and R.MontantBailleur<>'0' and R.CodeConvention='" & CmbConvention.Text & "'"
            dtAllocation = ExcecuteSelectQuery(query)
        End If

        If dtAllocation.Rows.Count = 0 Then
            GridPlanMarche.Rows.Clear()
            Dim NbCol As Decimal = GridPlanMarche.ColumnCount
            If (NbCol > 0) Then
                For i As Integer = 1 To NbCol - 1
                    GridPlanMarche.Columns.Remove("A")
                Next
            End If
            'On verifie si il y'a des activites sur la periode pour personnaliser le message de retour
            query = "SELECT COUNT(*) FROM T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebutMarche.Text) & "' AND DateDebutPartition<='" & dateconvert(DateFinMarche.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                SuccesMsg("Aucun compte à marché trouvé.")
                Return -1
            End If
            Return -2
        End If

        For Each rwAlloc As DataRow In dtAllocation.Rows
            Dim bailleurs As New ArrayList
            Dim MontantConv As New ArrayList
            Dim Conventions As New ArrayList
            If ElaboPPM = "Tous les bailleurs" Then
                query = "SELECT S.*, T.InitialeBailleur FROM t_repartitionparbailleur as S, t_besoinpartition as B, t_bailleur as T WHERE S.RefBesoinPartition=B.RefBesoinPartition and T.CodeBailleur=S.CodeBailleur and B.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND B.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebutMarche.Text) & "' AND DateDebutPartition<='" & dateconvert(DateFinMarche.Text) & "' and CodeProjet='" & ProjetEnCours & "')"
                Dim dtbail = ExcecuteSelectQuery(query)
                For Each rwb In dtbail.Rows
                    'If dtbail.Rows.Count > 1 Then
                    If Not bailleurs.Contains(rwb("InitialeBailleur").ToString) Then
                        bailleurs.Add(rwb("InitialeBailleur").ToString)
                        'Conventions.Add(rwb("CodeConvention").ToString)
                    End If
                    If Not Conventions.Contains(rwb("CodeConvention").ToString) Then
                        Conventions.Add(rwb("CodeConvention").ToString)
                    End If
                    'Else
                    '        bailleurs = rwb("InitialeBailleur").ToString
                    '    Conventions = rwb("CodeConvention").ToString
                    'End If
                Next
                'For i = 0 To bailleurs.Count - 1
                '    MsgBox(bailleurs.Item(i))
                'Next
                'For i = 0 To Conventions.Count - 1
                '    MsgBox(Conventions.Item(i))
                'Next
            End If
            'If CmbConvention.Text = "Toutes" Then
            '    query = "SELECT S.*, T.InitialeBailleur FROM t_repartitionparbailleur as S, t_besoinpartition as B, t_bailleur as T WHERE S.RefBesoinPartition=B.RefBesoinPartition and T.CodeBailleur=S.CodeBailleur and B.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND B.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND T.InitialeBailleur='" & BailleurConcerne.Text & "' AND DateDebutPartition>='" & dateconvert(DateDebutMarche.Text) & "' AND DateFinPartition<='" & dateconvert(DateFinMarche.Text) & "' and CodeProjet='" & ProjetEnCours & "')"
            '    Dim dtconv = ExcecuteSelectQuery(query)
            '    For Each rwc In dtconv.Rows
            '        If Not Conventions.Contains(rwc("CodeConvention").ToString) Then
            '            Conventions.Add(rwc("CodeConvention").ToString)
            '        End If
            '    Next
            'End If
            'query = "select * from T_Marche where CodeProjet='" & ProjetEnCours & "' and NumeroComptable='" & rwAlloc("NumeroComptable") & "' and PeriodeMarche='" & Periode & "' and TypeMarche='" & cmbTypeMarche.Text & "' and InitialeBailleur='" & BailleurConcerne.Text & "' and CodeConvention='" & CmbConvention.Text & "'"
            'Dim dt As DataTable = ExcecuteSelectQuery(query)
            'For Each rw As DataRow In dt.Rows
            '    Exit For 'On ignore le marche déjà enregistré sur le compte à marché de la période et de la convention
            'Next

            Dim LibelleCompte As String = ""
            query = "select LIBELLE_SC from T_COMP_SOUS_CLASSE where CODE_SC='" & rwAlloc("NumeroComptable") & "'"
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                LibelleCompte = MettreApost(rw("LIBELLE_SC").ToString)
            Next
            Dim lesBailleurs As String = ""
            Dim lesConventions As String = ""
            If ElaboPPM = "Tous les bailleurs" Then

                If bailleurs.Count > 1 Then
                    For i = 0 To bailleurs.Count - 1
                        lesBailleurs += bailleurs.Item(i) & " | "
                    Next
                Else
                    For i = 0 To bailleurs.Count - 1
                        lesBailleurs += bailleurs.Item(i)
                    Next
                End If
                If Conventions.Count > 1 Then
                    For i = 0 To Conventions.Count - 1
                        lesConventions += Conventions.Item(i) & " | "
                    Next
                Else
                    For i = 0 To Conventions.Count - 1
                        lesConventions = Conventions.Item(i)
                    Next
                End If

                'Insertion du marché
                query = "INSERT INTO T_Marche(CodeProjet,NumeroComptable,TypeMarche,DescriptionMarche,PeriodeMarche,InitialeBailleur,CodeConvention,Convention_ChefFile,JoursCompte,RefPPM, ModePPM) "
                query &= "VALUES('" & ProjetEnCours & "','" & rwAlloc("NumeroComptable") & "','" & cmbTypeMarche.Text & "','" & EnleverApost(LibelleCompte) & "','" & Trim(Periode) & "','" & lesBailleurs & "','" & lesConventions & "','" & ChefFile & "','" & JoursCompte & "','" & RefPlan & "','Tous_Bailleurs')"
                ExecuteNonQuery(query)
            Else
                'Insertion du marché
                query = "INSERT INTO T_Marche(CodeProjet,NumeroComptable,TypeMarche,DescriptionMarche,PeriodeMarche,InitialeBailleur,CodeConvention,Convention_ChefFile,JoursCompte,RefPPM,ModePPM) "
                query &= "VALUES('" & ProjetEnCours & "','" & rwAlloc("NumeroComptable") & "','" & cmbTypeMarche.Text & "','" & EnleverApost(LibelleCompte) & "','" & Trim(Periode) & "','" & BailleurConcerne.Text & "','" & CmbConvention.Text & "','" & CmbConvention.Text & "','" & JoursCompte & "','" & RefPlan & "','Bailleur')"
                ExecuteNonQuery(query)
            End If
            Dim DernierIndex As Decimal = 0

            If ElaboPPM = "Tous les bailleurs" Then
                'Liaison du marché avec les étapes de son type de marché **************************************************
                query = "select MAX(RefMarche) from T_Marche where CodeProjet='" & ProjetEnCours & "' and NumeroComptable='" & rwAlloc("NumeroComptable") & "' and PeriodeMarche='" & Periode & "' and TypeMarche='" & cmbTypeMarche.Text & "' and CodeConvention='" & lesConventions & "'"
                dt = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    DernierIndex = rw(0)
                Next
            Else
                'Liaison du marché avec les étapes de son type de marché **************************************************
                query = "select MAX(RefMarche) from T_Marche where CodeProjet='" & ProjetEnCours & "' and NumeroComptable='" & rwAlloc("NumeroComptable") & "' and PeriodeMarche='" & Periode & "' and TypeMarche='" & cmbTypeMarche.Text & "' and CodeConvention='" & CmbConvention.Text & "'"
                dt = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    DernierIndex = rw(0)
                Next
            End If

            'Recuperation du montant estimatif du marche
            Dim MontantEstim As Decimal = 0
            Dim dtRepartition As DataTable
            If ElaboPPM = "Tous les bailleurs" Then
                query = "select P.RefBesoinPartition,R.MontantBailleur from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebutMarche.Text) & "' AND DateDebutPartition<='" & dateconvert(DateFinMarche.Text) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & cmbTypeMarche.Text & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and R.MontantBailleur<>'0'"
                dtRepartition = ExcecuteSelectQuery(query)
                For Each rwRepartition As DataRow In dtRepartition.Rows
                    query = "UPDATE T_RepartitionParBailleur SET RefMarche='" & DernierIndex & "' where RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "' AND MontantBailleur<>'0'"   'RefBesoinPartition='" & ListeRefBesoin(w) & "' and 
                    ExecuteNonQuery(query)
                    'query = "SELECT COUNT(RefBesoinPartition) FROM T_BesoinMarche WHERE RefMarche='" & DernierIndex & "' AND RefBesoinPartition NOT IN (SELECT RefBesoinPartition FROM T_BesoinMarche WHERE RefMarche='" & DernierIndex & "')"
                    'Dim Result = ExecuteScallar(query)
                    'If Result = 0 Then
                    query = "DELETE from T_BesoinMarche where RefMarche='" & DernierIndex & "' AND RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "'"
                    ExecuteNonQuery(query)
                    query = "INSERT INTO T_BesoinMarche(RefBesoinPartition,RefMarche) VALUES('" & rwRepartition("RefBesoinPartition") & "','" & DernierIndex & "')"
                    ExecuteNonQuery(query)
                    '    MsgBox(query)
                    'End If
                    MontantEstim += rwRepartition("MontantBailleur")
                    'MsgBox(MontantEstim)
                Next
            Else
                query = "select P.RefBesoinPartition,R.MontantBailleur from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebutMarche.Text) & "' AND DateDebutPartition<='" & dateconvert(DateFinMarche.Text) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & cmbTypeMarche.Text & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and B.InitialeBailleur='" & BailleurConcerne.Text & "' and R.MontantBailleur<>'0' and R.CodeConvention='" & CmbConvention.Text & "'"
                dtRepartition = ExcecuteSelectQuery(query)

                For Each rwRepartition As DataRow In dtRepartition.Rows
                    query = "UPDATE T_RepartitionParBailleur SET RefMarche='" & DernierIndex & "' where RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "' and CodeConvention='" & CmbConvention.Text & "' AND MontantBailleur<>'0'"   'RefBesoinPartition='" & ListeRefBesoin(w) & "' and 
                    ExecuteNonQuery(query)
                    query = "DELETE from T_BesoinMarche where RefMarche='" & DernierIndex & "'AND RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "'"
                    ExecuteNonQuery(query)
                    query = "INSERT INTO T_BesoinMarche(RefBesoinPartition,RefMarche) VALUES('" & rwRepartition("RefBesoinPartition") & "','" & DernierIndex & "')"
                    ExecuteNonQuery(query)
                    MontantEstim += rwRepartition("MontantBailleur")
                Next
            End If


            'Verification de methode auto
            Dim MethodeAuto As Boolean = True
            query = "select MethodeMarcheAuto from T_ParamTechProjet where CodeProjet='" & ProjetEnCours & "' and MethodeMarcheAuto='NON'"
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                MethodeAuto = False
                Exit For
            Next
            Dim LaMethode As String = ""
            Dim LaRevue As String = ""
            Dim ExceptMethode As String = ""
            Dim CodeMethode As Decimal = 0
            Dim KodSeuil As Decimal = 0
            If (MethodeAuto = True) Then
                If ElaboPPM = "Tous les bailleurs" Then
                    query = "SELECT B.InitialeBailleur FROM t_bailleur as B, t_convention as C WHERE C.CodeBailleur=B.CodeBailleur AND C.CodeConvention='" & ChefFile & "'"
                    Dim InitialeBailleur = ExecuteScallar(query)
                    query = "select P.CodeProcAO,P.AbregeAO,P.TypeMarcheAO,S.CodeProcAO,S.MontantPlanche,S.PlancheInclu,S.MontantPlafond,S.PlafondInclu,S.TypeExamenAO,S.ExceptionRevue,S.CodeSeuil from T_ProcAO as P,T_Seuil as S where P.CodeProcAO=S.CodeProcAO and P.TypeMarcheAO='" & cmbTypeMarche.Text & "' and P.CodeProjet='" & ProjetEnCours & "' and S.Bailleur='" & InitialeBailleur & "' AND P.RechAuto='OUI' order by S.MontantPlanche"
                Else
                    query = "select P.CodeProcAO,P.AbregeAO,P.TypeMarcheAO,S.CodeProcAO,S.MontantPlanche,S.PlancheInclu,S.MontantPlafond,S.PlafondInclu,S.TypeExamenAO,S.ExceptionRevue,S.CodeSeuil from T_ProcAO as P,T_Seuil as S where P.CodeProcAO=S.CodeProcAO and P.TypeMarcheAO='" & cmbTypeMarche.Text & "' and P.CodeProjet='" & ProjetEnCours & "' and S.Bailleur='" & BailleurConcerne.Text & "' and P.RechAuto='OUI' order by S.MontantPlanche"
                End If
                dt = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    If (rw("PlancheInclu").ToString = "OUI") Then
                        If (rw("PlafondInclu").ToString = "OUI") Then
                            If CDec(rw("MontantPlanche")) <= MontantEstim Then
                                LaMethode = rw("CodeProcAO")
                                LaRevue = rw("TypeExamenAO")
                                ExceptMethode = rw("ExceptionRevue")
                                CodeMethode = rw("CodeProcAO")
                                KodSeuil = rw("CodeSeuil")

                            End If
                        ElseIf (rw("MontantPlafond").ToString = "NL") Then
                            If CDec(rw("MontantPlanche")) <= MontantEstim Then
                                LaMethode = rw("CodeProcAO")
                                LaRevue = rw("TypeExamenAO")
                                ExceptMethode = rw("ExceptionRevue")
                                CodeMethode = rw("CodeProcAO")
                                KodSeuil = rw("CodeSeuil")

                            End If
                        Else
                            If CDec(rw("MontantPlanche")) <= MontantEstim And CDec(rw("MontantPlafond")) > MontantEstim Then
                                LaMethode = rw("CodeProcAO")
                                LaRevue = rw("TypeExamenAO")
                                ExceptMethode = rw("ExceptionRevue")
                                CodeMethode = rw("CodeProcAO")
                                KodSeuil = rw("CodeSeuil")

                            End If
                        End If
                    Else
                        If (rw("PlafondInclu").ToString = "OUI") Then
                            If CDec(rw("MontantPlanche")) < MontantEstim Then
                                LaMethode = rw("CodeProcAO")
                                LaRevue = rw("TypeExamenAO")
                                ExceptMethode = rw("ExceptionRevue")
                                CodeMethode = rw("CodeProcAO")
                                KodSeuil = rw("CodeSeuil")

                            End If
                        Else
                            If (rw("MontantPlafond").ToString <> "TM" And rw("MontantPlafond").ToString <> "NL") Then
                                If CDec(rw("MontantPlanche")) < MontantEstim Then
                                    LaMethode = rw("CodeProcAO")
                                    LaRevue = rw("TypeExamenAO")
                                    ExceptMethode = rw("ExceptionRevue")
                                    CodeMethode = rw("CodeProcAO")
                                    KodSeuil = rw("CodeSeuil")

                                End If
                            ElseIf (rw("MontantPlanche") <> "TM") Then
                                If (rw("MontantPlafond").ToString = "NL") Then
                                    If (CDec(rw("MontantPlanche")) < MontantEstim) Then
                                        LaMethode = rw("CodeProcAO")
                                        LaRevue = rw("TypeExamenAO")
                                        ExceptMethode = rw("ExceptionRevue")
                                        CodeMethode = rw("CodeProcAO")
                                        KodSeuil = rw("CodeSeuil")

                                    End If

                                ElseIf (rw("MontantPlanche") = "TM") Then
                                    LaMethode = rw("CodeProcAO")
                                    LaRevue = rw("TypeExamenAO")
                                    ExceptMethode = rw("ExceptionRevue")
                                    CodeMethode = rw("CodeProcAO")
                                    KodSeuil = rw("CodeSeuil")
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            If (ExceptMethode <> "") Then LaRevue = LaRevue & "*"

            'Mise à jour des montants estimatifs, Méthodes et Revues dans la table marché *************************************************************
            query = "UPDATE T_Marche SET MontantEstimatif='" & MontantEstim & "',MethodeMarche ='" & LaMethode & "',RevuePrioPost ='" & LaRevue & "', CodeProcAO ='" & CodeMethode & "', CodeSeuil ='" & KodSeuil & "', DerniereMaj ='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' WHERE RefMarche='" & DernierIndex & "'"
            ExecuteNonQuery(query)
        Next
        Me.Cursor = Cursors.Default
        Return 0
    End Function
    Private Function RevisionPPM(RefPlan As Integer) As Integer
        Dim modif = False
        Dim NiveauActu As Integer
        query = "SELECT * FROM t_marche WHERE RefPPM='" & RefPlan & "'"
        Dim dtMarche = ExcecuteSelectQuery(query)
        For Each rw In dtMarche.Rows
            If rw("NiveauActu").ToString <> "" Then
                NiveauActu = CInt(rw("NiveauActu").ToString)
            Else
                NiveauActu = 0
            End If
            query = "INSERT INTO t_ppm_historiquemarche(CodeProjet,RefMarche,NumeroComptable,TypeMarche,DescriptionMarche,PeriodeMarche,InitialeBailleur,CodeConvention,Convention_ChefFile,JoursCompte,RefPPM, ModePPM,NiveauActu) "
            query &= "VALUES('" & ProjetEnCours & "','" & rw("RefMarche").ToString & "','" & rw("NumeroComptable").ToString & "','" & rw("TypeMarche").ToString & "','" & rw("DescriptionMarche").ToString & "','" & rw("PeriodeMarche").ToString & "','" & rw("InitialeBailleur").ToString & "','" & rw("CodeConvention").ToString & "','" & rw("Convention_ChefFile").ToString & "','" & rw("JoursCompte").ToString & "','" & RefPlan & "','" & rw("ModePPM").ToString & "','" & NiveauActu & "')"
            ExecuteNonQuery(query)
        Next
        Dim TypeMarche As String = String.Empty
        Dim CodeTypeMarche As String = String.Empty
        Dim Bailleur As String = String.Empty
        Dim Convention As String = String.Empty
        Dim Periode As String = String.Empty
        Dim JoursCompte As String
        query = "SELECT * FROM t_ppm_marche WHERE RefPPM='" & RefPlan & "'"
        Dim InfoPPM = ExcecuteSelectQuery(query)
        For Each rwInfoPPM In InfoPPM.Rows
            TypeMarche = rwInfoPPM("TypeMarche").ToString
            Periode = rwInfoPPM("PeriodePlan").ToString
            Bailleur = rwInfoPPM("InitialeBailleur").ToString
            Convention = rwInfoPPM("CodeConvention").ToString
        Next
        If TypeMarche.ToLower() = "Consultants".ToLower() Then
            CodeTypeMarche = "CS"
        ElseIf TypeMarche.ToLower() = "Fournitures".ToLower() Then
            CodeTypeMarche = "FR"
        ElseIf TypeMarche.ToLower() = "Services autres que les services de consultants".ToLower() Then
            CodeTypeMarche = "SA"
        ElseIf TypeMarche.ToLower() = "Travaux".ToLower() Then
            CodeTypeMarche = "TX"
        End If
        Dim DateDebut = CDate(Periode.Split(" - ")(0).Trim)
        Dim DateFin = CDate(Periode.Split(" - ")(2).Trim)
        'Recuperation des comptes à marché qui sont sur les activités de la période
        Dim dtAllocation As New DataTable
        If ElaboPPM = "Tous les bailleurs" Then
            query = "select DISTINCT P.NumeroComptable, B.InitialeBailleur, SUM(R.MontantBailleur) as MontantBailleurs from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebutMarche.Text) & "' AND DateDebutPartition<='" & dateconvert(DateFinMarche.Text) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & cmbTypeMarche.Text & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and R.MontantBailleur<>'0' GROUP BY P.NumeroComptable"
            dtAllocation = ExcecuteSelectQuery(query)
        Else
            query = "select DISTINCT P.NumeroComptable,B.InitialeBailleur from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebut) & "' AND DateDebutPartition<='" & dateconvert(DateFin) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & TypeMarche & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and B.InitialeBailleur='" & Bailleur & "' and R.MontantBailleur<>'0' and R.CodeConvention='" & Convention & "'"
            dtAllocation = ExcecuteSelectQuery(query)
        End If
        For Each rwAlloc As DataRow In dtAllocation.Rows
            Dim bailleurs As New ArrayList
            Dim Conventions As New ArrayList
            If ElaboPPM = "Tous les bailleurs" Then
                query = "SELECT S.*, T.InitialeBailleur FROM t_repartitionparbailleur as S, t_besoinpartition as B, t_bailleur as T WHERE S.RefBesoinPartition=B.RefBesoinPartition and T.CodeBailleur=S.CodeBailleur and B.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND B.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebutMarche.Text) & "' AND DateDebutPartition<='" & dateconvert(DateFinMarche.Text) & "' and CodeProjet='" & ProjetEnCours & "')"
                Dim dtbail = ExcecuteSelectQuery(query)
                For Each rwb In dtbail.Rows
                    If Not bailleurs.Contains(rwb("InitialeBailleur").ToString) Then
                        bailleurs.Add(rwb("InitialeBailleur").ToString)
                    End If
                    If Not Conventions.Contains(rwb("CodeConvention").ToString) Then
                        Conventions.Add(rwb("CodeConvention").ToString)
                    End If
                Next
            End If
            'On verifie si le numero du compte est dejà enregistré
            If ElaboPPM = "Tous les bailleurs" Then
                query = "select count(*) from T_Marche where CodeProjet='" & ProjetEnCours & "' and NumeroComptable='" & rwAlloc("NumeroComptable") & "' and RefPPM='" & RefPlan & "'"
            Else
                query = "select count(*) from T_Marche where CodeProjet='" & ProjetEnCours & "' and NumeroComptable='" & rwAlloc("NumeroComptable") & "' and InitialeBailleur='" & Bailleur & "' and RefPPM='" & RefPlan & "'"
            End If
            Dim dtResult0 = ExecuteScallar(query)
            'Si ce n'est pas enregistré
            If dtResult0 = 0 Then
                Dim LibelleCompte0 As String = ""
                query = "select LIBELLE_SC from T_COMP_SOUS_CLASSE where CODE_SC='" & rwAlloc("NumeroComptable") & "'"
                Dim dt0 = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    LibelleCompte0 = MettreApost(rw("LIBELLE_SC").ToString)
                Next
                Dim lesBailleurs0 As String = ""
                Dim lesConventions0 As String = ""
                If ElaboPPM = "Tous les bailleurs" Then
                    If bailleurs.Count > 1 Then
                        For i = 0 To bailleurs.Count - 1
                            lesBailleurs0 += bailleurs.Item(i) & " | "
                        Next
                    Else
                        For i = 0 To bailleurs.Count - 1
                            lesBailleurs0 += bailleurs.Item(i)
                        Next
                    End If
                    If Conventions.Count > 1 Then
                        For i = 0 To Conventions.Count - 1
                            lesConventions0 += Conventions.Item(i) & " | "
                        Next
                    Else
                        For i = 0 To Conventions.Count - 1
                            lesConventions0 += Conventions.Item(i)
                        Next
                    End If
                    'Insertion du marché
                    query = "INSERT INTO T_Marche(CodeProjet,NumeroComptable,TypeMarche,DescriptionMarche,PeriodeMarche,InitialeBailleur,CodeConvention,Convention_ChefFile,JoursCompte,RefPPM, ModePPM) "
                    query &= "VALUES('" & ProjetEnCours & "','" & rwAlloc("NumeroComptable") & "','" & cmbTypeMarche.Text & "','" & EnleverApost(LibelleCompte0) & "','" & Trim(Periode) & "','" & lesBailleurs0 & "','" & lesConventions0 & "','" & ChefFile & "','" & JoursCompte & "','" & RefPlan & "','Tous_Bailleurs')"
                    ExecuteNonQuery(query)
                Else
                    'Insertion du marché
                    query = "INSERT INTO T_Marche(CodeProjet,NumeroComptable,TypeMarche,DescriptionMarche,PeriodeMarche,InitialeBailleur,CodeConvention,Convention_ChefFile,ModePPM,JoursCompte,RefPPM) "
                    query &= "VALUES('" & ProjetEnCours & "','" & rwAlloc("NumeroComptable") & "','" & TypeMarche & "','" & EnleverApost(LibelleCompte0) & "','" & Trim(Periode) & "','" & Bailleur & "','" & Convention & "','" & Convention & "','Bailleur','" & JoursCompte & "','" & RefPlan & "')"
                    ExecuteNonQuery(query)
                End If
                Dim DernierIndex0 As Decimal = 0

                If ElaboPPM = "Tous les bailleurs" Then
                    'Liaison du marché avec les étapes de son type de marché **************************************************
                    query = "select MAX(RefMarche) from T_Marche where CodeProjet='" & ProjetEnCours & "' and NumeroComptable='" & rwAlloc("NumeroComptable") & "' and PeriodeMarche='" & Periode & "' and TypeMarche='" & TypeMarche & "' and CodeConvention='" & lesConventions0 & "'"
                    Dim dt1 = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt1.Rows
                        DernierIndex0 = rw(0)
                    Next
                Else
                    'Liaison du marché avec les étapes de son type de marché **************************************************
                    query = "select MAX(RefMarche) from T_Marche where CodeProjet='" & ProjetEnCours & "' and NumeroComptable='" & rwAlloc("NumeroComptable") & "' and PeriodeMarche='" & Periode & "' and TypeMarche='" & TypeMarche & "' and CodeConvention='" & Convention & "'"
                    Dim dt2 = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt2.Rows
                        DernierIndex0 = rw(0)
                    Next
                End If
                'Recuperation du montant estimatif du marche
                Dim MontantEstim0 As Decimal = 0
                Dim dtRepartition0 As DataTable
                If ElaboPPM = "Tous les bailleurs" Then
                    query = "select P.RefBesoinPartition,R.MontantBailleur from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebutMarche.Text) & "' AND DateDebutPartition<='" & dateconvert(DateFinMarche.Text) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & cmbTypeMarche.Text & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and R.MontantBailleur<>'0'"
                    dtRepartition0 = ExcecuteSelectQuery(query)
                    For Each rwRepartition As DataRow In dtRepartition0.Rows
                        query = "UPDATE T_RepartitionParBailleur SET RefMarche='" & DernierIndex0 & "' where RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "' AND MontantBailleur<>'0'"   'RefBesoinPartition='" & ListeRefBesoin(w) & "' and 
                        ExecuteNonQuery(query)
                        query = "DELETE from T_BesoinMarche where RefMarche='" & DernierIndex0 & "' AND RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "'"
                        ExecuteNonQuery(query)
                        query = "INSERT INTO T_BesoinMarche(RefBesoinPartition,RefMarche) VALUES('" & rwRepartition("RefBesoinPartition") & "','" & DernierIndex0 & "')"
                        ExecuteNonQuery(query)
                        MontantEstim0 += rwRepartition("MontantBailleur")
                    Next
                Else
                    query = "select P.RefBesoinPartition,R.MontantBailleur from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebut) & "' AND DateFinPartition<='" & dateconvert(DateFin) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & TypeMarche & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and B.InitialeBailleur='" & Bailleur & "' and R.MontantBailleur<>'0' and R.CodeConvention='" & Convention & "'"
                    dtRepartition0 = ExcecuteSelectQuery(query)
                    For Each rwRepartition As DataRow In dtRepartition0.Rows
                        query = "UPDATE T_RepartitionParBailleur SET RefMarche='" & DernierIndex0 & "' where RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "' and CodeConvention='" & Convention & "' AND MontantBailleur<>'0'"   'RefBesoinPartition='" & ListeRefBesoin(w) & "' and 
                        ExecuteNonQuery(query)
                        query = "DELETE from T_BesoinMarche where RefMarche='" & DernierIndex0 & "' AND RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "'"
                        ExecuteNonQuery(query)
                        query = "INSERT INTO T_BesoinMarche(RefBesoinPartition,RefMarche) VALUES('" & rwRepartition("RefBesoinPartition") & "','" & DernierIndex0 & "')"
                        ExecuteNonQuery(query)
                        MontantEstim0 += rwRepartition("MontantBailleur")
                    Next
                End If
                'Verification de methode auto
                Dim MethodeAuto0 As Boolean = True
                query = "select MethodeMarcheAuto from T_ParamTechProjet where CodeProjet='" & ProjetEnCours & "' and MethodeMarcheAuto='NON'"
                Dim dt3 = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt3.Rows
                    MethodeAuto0 = False
                    Exit For
                Next
                Dim LaMethode0 As String = ""
                Dim LaRevue0 As String = ""
                Dim ExceptMethode0 As String = ""
                Dim CodeMethode0 As Decimal = 0
                If (MethodeAuto0 = True) Then

                    If ElaboPPM = "Tous les bailleurs" Then
                        query = "SELECT B.InitialeBailleur FROM t_bailleur as B, t_convention as C WHERE C.CodeBailleur=B.CodeBailleur AND C.CodeConvention='" & ChefFile & "'"
                        Dim InitialeBailleur = ExecuteScallar(query)
                        query = "select P.CodeProcAO,P.AbregeAO,P.TypeMarcheAO,S.CodeProcAO,S.MontantPlanche,S.PlancheInclu,S.MontantPlafond,S.PlafondInclu,S.TypeExamenAO,S.ExceptionRevue,S.CodeSeuil from T_ProcAO as P,T_Seuil as S where P.CodeProcAO=S.CodeProcAO and P.TypeMarcheAO='" & cmbTypeMarche.Text & "' and P.CodeProjet='" & ProjetEnCours & "' and S.Bailleur'=" & InitialeBailleur & "' P.RechAuto='OUI' order by S.MontantPlanche"
                    Else
                        query = "select P.CodeProcAO,P.AbregeAO,P.TypeMarcheAO,S.CodeProcAO,S.MontantPlanche,S.PlancheInclu,S.MontantPlafond,S.PlafondInclu,S.TypeExamenAO,S.ExceptionRevue,S.CodeSeuil from T_ProcAO as P,T_Seuil as S where P.CodeProcAO=S.CodeProcAO and P.TypeMarcheAO='" & cmbTypeMarche.Text & "' and P.CodeProjet='" & ProjetEnCours & "' and S.Bailleur='" & BailleurConcerne.Text & "' and P.RechAuto='OUI' order by S.MontantPlanche"
                    End If
                    Dim dt4 = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt4.Rows
                        If (rw("PlancheInclu").ToString = "OUI") Then
                            If (rw("PlafondInclu").ToString = "OUI") Then
                                If CDec(rw("MontantPlanche")) <= MontantEstim0 Then
                                    LaMethode0 = rw("CodeProcAO")
                                    LaRevue0 = rw("TypeExamenAO")
                                    ExceptMethode0 = rw("ExceptionRevue")
                                    CodeMethode0 = rw("CodeProcAO")

                                End If
                            ElseIf (rw("MontantPlafond").ToString = "NL") Then
                                If CDec(rw("MontantPlanche")) <= MontantEstim0 Then
                                    LaMethode0 = rw("CodeProcAO")
                                    LaRevue0 = rw("TypeExamenAO")
                                    ExceptMethode0 = rw("ExceptionRevue")
                                    CodeMethode0 = rw("CodeProcAO")

                                End If
                            Else
                                If CDec(rw("MontantPlanche")) <= MontantEstim0 And CDec(rw("MontantPlafond")) > MontantEstim0 Then
                                    LaMethode0 = rw("CodeProcAO")
                                    LaRevue0 = rw("TypeExamenAO")
                                    ExceptMethode0 = rw("ExceptionRevue")
                                    CodeMethode0 = rw("CodeProcAO")

                                End If
                            End If
                        Else
                            If (rw("PlafondInclu").ToString = "OUI") Then
                                If CDec(rw("MontantPlanche")) < MontantEstim0 Then
                                    LaMethode0 = rw("CodeProcAO")
                                    LaRevue0 = rw("TypeExamenAO")
                                    ExceptMethode0 = rw("ExceptionRevue")
                                    CodeMethode0 = rw("CodeProcAO")

                                End If
                            Else
                                If (rw("MontantPlafond").ToString <> "TM" And rw("MontantPlafond").ToString <> "NL") Then
                                    If CDec(rw("MontantPlanche")) < MontantEstim0 Then
                                        LaMethode0 = rw("CodeProcAO")
                                        LaRevue0 = rw("TypeExamenAO")
                                        ExceptMethode0 = rw("ExceptionRevue")
                                        CodeMethode0 = rw("CodeProcAO")

                                    End If
                                ElseIf (rw("MontantPlanche") <> "TM") Then
                                    If (rw("MontantPlafond").ToString = "NL") Then
                                        If (CDec(rw("MontantPlanche")) < MontantEstim0) Then
                                            LaMethode0 = rw("CodeProcAO")
                                            LaRevue0 = rw("TypeExamenAO")
                                            ExceptMethode0 = rw("ExceptionRevue")
                                            CodeMethode0 = rw("CodeProcAO")

                                        End If

                                    ElseIf (rw("MontantPlanche") = "TM") Then
                                        LaMethode0 = rw("CodeProcAO")
                                        LaRevue0 = rw("TypeExamenAO")
                                        ExceptMethode0 = rw("ExceptionRevue")
                                        CodeMethode0 = rw("CodeProcAO")
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If
                If (ExceptMethode0 <> "") Then LaRevue0 = LaRevue0 & "*"
                'Mise à jour des montants estimatifs, Méthodes et Revues dans la table marché *************************************************************
                query = "UPDATE T_Marche SET MontantEstimatif='" & MontantEstim0 & "',MethodeMarche ='" & LaMethode0 & "',RevuePrioPost ='" & LaRevue0 & "', CodeProcAO ='" & CodeMethode0 & "', DerniereMaj ='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' WHERE RefMarche='" & DernierIndex0 & "'"
                ExecuteNonQuery(query)
                'Si le numéro comptable est déjà enregistré
            Else
                If ElaboPPM = "Tous les bailleurs" Then
                    query = "select * from T_Marche where CodeProjet='" & ProjetEnCours & "' and NumeroComptable='" & rwAlloc("NumeroComptable") & "' and RefPPM='" & RefPlan & "'"
                Else
                    query = "select * from T_Marche where CodeProjet='" & ProjetEnCours & "' and NumeroComptable='" & rwAlloc("NumeroComptable") & "' and InitialeBailleur='" & Bailleur & "' and CodeConvention='" & Convention & "' and RefPPM='" & RefPlan & "'"
                End If
                Dim dtResult1 = ExcecuteSelectQuery(query)
                For Each rw In dtResult1.Rows
                    Dim dtResult2 As Integer = 0
                    If rw("TypeMarche").ToString = "Consultants" Then
                        query = "SELECT COUNT(D.NumeroDAMI) FROM t_marche as M, t_ami as D WHERE M.RefMarche=D.RefMarche AND M.RefMarche='" & rw("RefMarche").ToString & "'"
                        Dim test As Integer = ExecuteScallar(query)
                        query = "SELECT COUNT(D.NumeroDp) FROM t_marche as M, t_dp as D WHERE M.RefMarche=D.RefMarche AND D.Statut<>'Annuler' AND M.RefMarche='" & rw("RefMarche").ToString & "'"
                        Dim test1 As Integer = ExecuteScallar(query)
                        dtResult2 = test + test1
                    Else
                        query = "SELECT COUNT(D.NumeroDAO) FROM t_marche as M, t_dao as D WHERE D.statut_DAO<>'Annuler' AND M.RefMarche=D.RefMarche AND M.RefMarche='" & rw("RefMarche").ToString & "'"
                        dtResult2 = ExecuteScallar(query)
                    End If
                    If dtResult2 = 0 Then
                        Dim lesBailleurs0 As String = ""
                        Dim lesConventions0 As String = ""
                        If bailleurs.Count > 1 Then
                            For i = 0 To bailleurs.Count - 1
                                lesBailleurs0 += bailleurs.Item(i) & " | "
                            Next
                        Else
                            For i = 0 To bailleurs.Count - 1
                                lesBailleurs0 += bailleurs.Item(i)
                            Next
                        End If
                        If Conventions.Count > 1 Then
                            For i = 0 To Conventions.Count - 1
                                lesConventions0 += Conventions.Item(i) & " | "
                            Next
                        Else
                            For i = 0 To Conventions.Count - 1
                                lesConventions0 += Conventions.Item(i)
                            Next
                        End If
                        'Recuperation du montant estimatif du marche
                        Dim MontantEstim0 As Decimal = 0
                        Dim dtRepartition0 As DataTable
                        If ElaboPPM = "Tous les bailleurs" Then
                            query = "select P.RefBesoinPartition,R.MontantBailleur from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebut) & "' AND DateDebutPartition<='" & dateconvert(DateFin) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & TypeMarche & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and R.MontantBailleur<>'0'"
                            dtRepartition0 = ExcecuteSelectQuery(query)
                            query = "DELETE from T_BesoinMarche where RefMarche='" & rw("RefMarche").ToString & "'"
                            ExecuteNonQuery(query)
                            For Each rwRepartition As DataRow In dtRepartition0.Rows
                                query = "UPDATE T_RepartitionParBailleur SET RefMarche='" & rw("RefMarche").ToString & "' where RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "' AND MontantBailleur<>'0'"
                                ExecuteNonQuery(query)
                                query = "DELETE from T_BesoinMarche where RefMarche='" & rw("RefMarche").ToString & "' AND RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "'"
                                ExecuteNonQuery(query)
                                query = "INSERT INTO T_BesoinMarche(RefBesoinPartition,RefMarche) VALUES('" & rwRepartition("RefBesoinPartition") & "','" & rw("RefMarche").ToString & "')"
                                ExecuteNonQuery(query)
                                MontantEstim0 += rwRepartition("MontantBailleur")
                            Next
                        Else
                            query = "select P.RefBesoinPartition,R.MontantBailleur from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebut) & "' AND DateFinPartition<='" & dateconvert(DateFin) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & TypeMarche & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and B.InitialeBailleur='" & Bailleur & "' and R.MontantBailleur<>'0' and R.CodeConvention='" & Convention & "' AND (R.RefMarche='" & rw("RefMarche").ToString & "' or R.RefMarche=0)"
                            dtRepartition0 = ExcecuteSelectQuery(query)
                            query = "DELETE from T_BesoinMarche where RefMarche='" & rw("RefMarche").ToString & "'"
                            ExecuteNonQuery(query)
                            For Each rwRepartition As DataRow In dtRepartition0.Rows
                                query = "UPDATE T_RepartitionParBailleur SET RefMarche='" & rw("RefMarche").ToString & "' where RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "' and CodeConvention='" & Convention & "' AND MontantBailleur<>'0'"   'RefBesoinPartition='" & ListeRefBesoin(w) & "' and 
                                ExecuteNonQuery(query)
                                query = "DELETE from T_BesoinMarche where RefMarche='" & rw("RefMarche").ToString & "' AND RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "'"
                                ExecuteNonQuery(query)
                                query = "INSERT INTO T_BesoinMarche(RefBesoinPartition,RefMarche) VALUES('" & rwRepartition("RefBesoinPartition") & "','" & rw("RefMarche").ToString & "')"
                                ExecuteNonQuery(query)
                                MontantEstim0 += rwRepartition("MontantBailleur")
                                'End If
                            Next
                        End If
                        'Verification de methode auto
                        Dim MethodeAuto0 As Boolean = True
                        query = "select MethodeMarcheAuto from T_ParamTechProjet where CodeProjet='" & ProjetEnCours & "' and MethodeMarcheAuto='NON'"
                        Dim dt3 = ExcecuteSelectQuery(query)
                        For Each rw0 As DataRow In dt3.Rows
                            MethodeAuto0 = False
                            Exit For
                        Next
                        Dim LaMethode0 As String = ""
                        Dim LaRevue0 As String = ""
                        Dim ExceptMethode0 As String = ""
                        Dim CodeMethode0 As Decimal = 0
                        If (MethodeAuto0 = True) Then

                            If ElaboPPM = "Tous les bailleurs" Then
                                'Dim ChefFile As String = ""
                                'query = "SELECT S.CodeConvention, max(S.MontantBailleur) FROM t_repartitionparbailleur as S, t_besoinpartition as B, t_bailleur as T WHERE S.RefBesoinPartition=B.RefBesoinPartition and T.CodeBailleur=S.CodeBailleur and B.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND B.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebutMarche.Text) & "' AND DateDebutPartition<='" & dateconvert(DateFinMarche.Text) & "' and CodeProjet='" & ProjetEnCours & "')"
                                'Dim dtChefFile = ExcecuteSelectQuery(query)
                                'For Each rwChefFile In dtChefFile.Rows
                                '    ChefFile = rwChefFile("CodeConvention").ToString
                                'Next
                                query = "SELECT B.InitialeBailleur FROM t_bailleur as B, t_convention as C WHERE C.CodeBailleur=B.CodeBailleur AND C.CodeConvention='" & ChefFile & "'"
                                Dim InitialeBailleur = ExecuteScallar(query)
                                query = "select P.CodeProcAO,P.AbregeAO,P.TypeMarcheAO,S.CodeProcAO,S.MontantPlanche,S.PlancheInclu,S.MontantPlafond,S.PlafondInclu,S.TypeExamenAO,S.ExceptionRevue,S.CodeSeuil from T_ProcAO as P,T_Seuil as S where P.CodeProcAO=S.CodeProcAO and P.TypeMarcheAO='" & cmbTypeMarche.Text & "' and P.CodeProjet='" & ProjetEnCours & "' and S.Bailleur='" & InitialeBailleur & "' AND P.RechAuto='OUI' order by S.MontantPlanche"
                            Else
                                query = "select P.CodeProcAO,P.AbregeAO,P.TypeMarcheAO,S.CodeProcAO,S.MontantPlanche,S.PlancheInclu,S.MontantPlafond,S.PlafondInclu,S.TypeExamenAO,S.ExceptionRevue,S.CodeSeuil from T_ProcAO as P,T_Seuil as S where P.CodeProcAO=S.CodeProcAO and P.TypeMarcheAO='" & cmbTypeMarche.Text & "' and P.CodeProjet='" & ProjetEnCours & "' and S.Bailleur='" & BailleurConcerne.Text & "' and P.RechAuto='OUI' order by S.MontantPlanche"
                            End If
                            Dim dt4 = ExcecuteSelectQuery(query)
                            For Each rw0 As DataRow In dt4.Rows
                                If (rw0("PlancheInclu").ToString = "OUI") Then
                                    If (rw0("PlafondInclu").ToString = "OUI") Then
                                        If CDec(rw0("MontantPlanche")) <= MontantEstim0 Then
                                            LaMethode0 = rw0("CodeProcAO")
                                            LaRevue0 = rw0("TypeExamenAO")
                                            ExceptMethode0 = rw0("ExceptionRevue")
                                            CodeMethode0 = rw("CodeProcAO")

                                        End If
                                    ElseIf (rw0("MontantPlafond").ToString = "NL") Then
                                        If CDec(rw0("MontantPlanche")) <= MontantEstim0 Then
                                            LaMethode0 = rw("CodeProcAO")
                                            LaRevue0 = rw0("TypeExamenAO")
                                            ExceptMethode0 = rw0("ExceptionRevue")
                                            CodeMethode0 = rw0("CodeProcAO")

                                        End If
                                    Else
                                        If CDec(rw0("MontantPlanche")) <= MontantEstim0 And CDec(rw0("MontantPlafond")) > MontantEstim0 Then
                                            LaMethode0 = rw0("CodeProcAO")
                                            LaRevue0 = rw0("TypeExamenAO")
                                            ExceptMethode0 = rw0("ExceptionRevue")
                                            CodeMethode0 = rw0("CodeProcAO")

                                        End If
                                    End If
                                Else
                                    If (rw0("PlafondInclu").ToString = "OUI") Then
                                        If CDec(rw0("MontantPlanche")) < MontantEstim0 Then
                                            LaMethode0 = rw0("CodeProcAO")
                                            LaRevue0 = rw0("TypeExamenAO")
                                            ExceptMethode0 = rw0("ExceptionRevue")
                                            CodeMethode0 = rw0("CodeProcAO")

                                        End If
                                    Else
                                        If (rw0("MontantPlafond").ToString <> "TM" And rw0("MontantPlafond").ToString <> "NL") Then
                                            If CDec(rw0("MontantPlanche")) < MontantEstim0 Then
                                                LaMethode0 = rw0("CodeProcAO")
                                                LaRevue0 = rw0("TypeExamenAO")
                                                ExceptMethode0 = rw0("ExceptionRevue")
                                                CodeMethode0 = rw0("CodeProcAO")

                                            End If
                                        ElseIf (rw0("MontantPlanche") <> "TM") Then
                                            If (rw0("MontantPlafond").ToString = "NL") Then
                                                If (CDec(rw0("MontantPlanche")) < MontantEstim0) Then
                                                    LaMethode0 = rw0("CodeProcAO")
                                                    LaRevue0 = rw0("TypeExamenAO")
                                                    ExceptMethode0 = rw0("ExceptionRevue")
                                                    CodeMethode0 = rw0("CodeProcAO")

                                                End If

                                            ElseIf (rw("MontantPlanche") = "TM") Then
                                                LaMethode0 = rw0("CodeProcAO")
                                                LaRevue0 = rw0("TypeExamenAO")
                                                ExceptMethode0 = rw0("ExceptionRevue")
                                                CodeMethode0 = rw0("CodeProcAO")
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                        End If
                        If (ExceptMethode0 <> "") Then LaRevue0 = LaRevue0 & "*"
                        'Mise à jour des montants estimatifs, Méthodes et Revues dans la table marché *************************************************************
                        If ElaboPPM = "Tous les bailleurs" Then
                            query = "UPDATE T_Marche SET MontantEstimatif='" & MontantEstim0 & "',MethodeMarche ='" & LaMethode0 & "',RevuePrioPost ='" & LaRevue0 & "', CodeProcAO ='" & CodeMethode0 & "', InitialeBailleur='" & lesBailleurs0 & "', CodeConvention='" & lesConventions0 & "', Convention_ChefFile='" & ChefFile & "', DerniereMaj ='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' WHERE RefMarche='" & rw("RefMarche").ToString & "'"
                        Else
                            query = "UPDATE T_Marche SET MontantEstimatif='" & MontantEstim0 & "',MethodeMarche ='" & LaMethode0 & "',RevuePrioPost ='" & LaRevue0 & "', CodeProcAO ='" & CodeMethode0 & "', DerniereMaj ='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' WHERE RefMarche='" & rw("RefMarche").ToString & "'"
                        End If
                        ExecuteNonQuery(query)
                    Else
                        Dim dtResult3 As Integer = 0
                        If rw("TypeMarche").ToString = "Consultants" Then
                            query = "SELECT COUNT(*) FROM t_marche WHERE NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND RefPPM='" & RefPlan & "' AND RefMarche NOT IN(SELECT M.RefMarche FROM t_marche as M, t_ami as D WHERE M.RefMarche=D.RefMarche AND M.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND RefPPM='" & RefPlan & "' UNION SELECT M.RefMarche FROM t_marche as M, t_dp as D WHERE M.RefMarche=D.RefMarche AND D.Statut<>'Annuler' AND M.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND RefPPM='" & RefPlan & "')"
                            Dim test As Integer = ExecuteScallar(query)
                            query = "SELECT COUNT(*) FROM t_marche WHERE NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND RefPPM='" & RefPlan & "' AND RefMarche NOT IN(SELECT M.RefMarche FROM t_marche as M, t_dp as D WHERE M.RefMarche=D.RefMarche AND D.Statut<>'Annuler' AND M.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND RefPPM='" & RefPlan & "' UNION SELECT M.RefMarche FROM t_marche as M, t_ami as D WHERE M.RefMarche=D.RefMarche AND M.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND RefPPM='" & RefPlan & "')"
                            Dim test1 As Integer = ExecuteScallar(query)
                            dtResult3 = test + test1
                        Else
                            query = "SELECT COUNT(*) FROM t_marche WHERE NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND RefPPM='" & RefPlan & "' AND RefMarche NOT IN(SELECT M.RefMarche FROM t_marche as M, t_dao as D WHERE D.statut_DAO<>'Annuler' AND M.RefMarche=D.RefMarche AND M.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND RefPPM='" & RefPlan & "')"
                            dtResult3 = ExecuteScallar(query)
                        End If
                        If dtResult3 > 0 Then
                            Dim dtResult4 As Integer
                            If rw("TypeMarche").ToString = "Consultants" Then
                                query = "SELECT COUNT(D.NumeroDAMI) FROM t_marche as M, t_ami as D WHERE M.RefMarche=D.RefMarche AND M.RefMarche='" & rw("RefMarche").ToString & "'"
                                Dim test As Integer = ExecuteScallar(query)
                                query = "SELECT COUNT(D.NumeroDp) FROM t_marche as M, t_dp as D WHERE M.RefMarche=D.RefMarche AND D.Statut<>'Annuler' AND M.RefMarche='" & rw("RefMarche").ToString & "'"
                                Dim test1 As Integer = ExecuteScallar(query)
                                dtResult4 = test + test1
                            Else
                                query = "SELECT COUNT(D.NumeroDAO) FROM t_marche as M, t_dao as D WHERE D.statut_DAO<>'Annuler' AND M.RefMarche=D.RefMarche AND M.RefMarche='" & rw("RefMarche").ToString & "'"
                                dtResult4 = ExecuteScallar(query)
                            End If
                            If dtResult4 = 0 Then
                                Dim lesBailleurs0 As String = ""
                                Dim lesConventions0 As String = ""
                                If bailleurs.Count > 1 Then
                                    For i = 0 To bailleurs.Count - 1
                                        lesBailleurs0 += bailleurs.Item(i) & " | "
                                    Next
                                Else
                                    For i = 0 To bailleurs.Count - 1
                                        lesBailleurs0 += bailleurs.Item(i)
                                    Next
                                End If
                                If Conventions.Count > 1 Then
                                    For i = 0 To Conventions.Count - 1
                                        lesConventions0 += Conventions.Item(i) & " | "
                                    Next
                                Else
                                    For i = 0 To Conventions.Count - 1
                                        lesConventions0 += Conventions.Item(i)
                                    Next
                                End If
                                'Recuperation du montant estimatif du marche
                                Dim MontantEstim0 As Decimal = 0
                                Dim dtRepartition0 As DataTable
                                If ElaboPPM = "Tous les bailleurs" Then
                                    query = "select P.RefBesoinPartition,R.MontantBailleur from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebut) & "' AND DateFinPartition<='" & dateconvert(DateFin) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & TypeMarche & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and R.MontantBailleur<>'0'"
                                    dtRepartition0 = ExcecuteSelectQuery(query)
                                    For Each rwRepartition As DataRow In dtRepartition0.Rows
                                        query = "SELECT COUNT(*) T_BesoinMarche WHERE RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "' AND RefMarche='" & rw("RefMarche").ToString & "'"
                                        query = "UPDATE T_RepartitionParBailleur SET RefMarche='" & rw("RefMarche").ToString & "' where RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "' and CodeConvention='" & Convention & "' AND MontantBailleur<>'0'"   'RefBesoinPartition='" & ListeRefBesoin(w) & "' and 
                                        ExecuteNonQuery(query)

                                        query = "DELETE from T_BesoinMarche where RefMarche='" & rw("RefMarche").ToString & "'"
                                        ExecuteNonQuery(query)

                                        MontantEstim0 += rwRepartition("MontantBailleur")
                                    Next
                                Else
                                    query = "select P.RefBesoinPartition,R.MontantBailleur from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebut) & "' AND DateFinPartition<='" & dateconvert(DateFin) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & TypeMarche & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and B.InitialeBailleur='" & Bailleur & "' and R.MontantBailleur<>'0' and R.CodeConvention='" & Convention & "' AND (R.RefMarche='" & rw("RefMarche").ToString & "' or R.RefMarche=0)"
                                    dtRepartition0 = ExcecuteSelectQuery(query)
                                    query = "DELETE from T_BesoinMarche where RefMarche='" & rw("RefMarche").ToString & "'"
                                    ExecuteNonQuery(query)
                                    For Each rwRepartition As DataRow In dtRepartition0.Rows
                                        query = "UPDATE T_RepartitionParBailleur SET RefMarche='" & rw("RefMarche").ToString & "' where RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "' and CodeConvention='" & Convention & "' AND MontantBailleur<>'0'"   'RefBesoinPartition='" & ListeRefBesoin(w) & "' and 
                                        ExecuteNonQuery(query)
                                        query = "INSERT INTO T_BesoinMarche(RefBesoinPartition,RefMarche) VALUES('" & rwRepartition("RefBesoinPartition") & "','" & rw("RefMarche").ToString & "')"
                                        ExecuteNonQuery(query)
                                        MontantEstim0 += rwRepartition("MontantBailleur")
                                        'End If
                                    Next
                                End If
                                'Verification de methode auto
                                Dim MethodeAuto0 As Boolean = True
                                query = "select MethodeMarcheAuto from T_ParamTechProjet where CodeProjet='" & ProjetEnCours & "' and MethodeMarcheAuto='NON'"
                                Dim dt3 = ExcecuteSelectQuery(query)
                                For Each rw0 As DataRow In dt3.Rows
                                    MethodeAuto0 = False
                                    Exit For
                                Next
                                Dim LaMethode0 As String = ""
                                Dim LaRevue0 As String = ""
                                Dim ExceptMethode0 As String = ""
                                Dim CodeMethode0 As Decimal = 0
                                If (MethodeAuto0 = True) Then

                                    If ElaboPPM = "Tous les bailleurs" Then
                                        query = "select P.CodeProcAO,P.AbregeAO,P.TypeMarcheAO,S.CodeProcAO,S.MontantPlanche,S.PlancheInclu,S.MontantPlafond,S.PlafondInclu,S.TypeExamenAO,S.ExceptionRevue,S.CodeSeuil from T_ProcAO as P,T_Seuil as S where P.CodeProcAO=S.CodeProcAO and P.TypeMarcheAO='" & cmbTypeMarche.Text & "' and P.CodeProjet='" & ProjetEnCours & "' and P.RechAuto='OUI' order by S.MontantPlanche"
                                    Else
                                        query = "select P.CodeProcAO,P.AbregeAO,P.TypeMarcheAO,S.CodeProcAO,S.MontantPlanche,S.PlancheInclu,S.MontantPlafond,S.PlafondInclu,S.TypeExamenAO,S.ExceptionRevue,S.CodeSeuil from T_ProcAO as P,T_Seuil as S where P.CodeProcAO=S.CodeProcAO and P.TypeMarcheAO='" & cmbTypeMarche.Text & "' and P.CodeProjet='" & ProjetEnCours & "' and S.Bailleur='" & BailleurConcerne.Text & "' and P.RechAuto='OUI' order by S.MontantPlanche"
                                    End If
                                    Dim dt4 = ExcecuteSelectQuery(query)
                                    For Each rw0 As DataRow In dt4.Rows
                                        If (rw0("PlancheInclu").ToString = "OUI") Then
                                            If (rw0("PlafondInclu").ToString = "OUI") Then
                                                If CDec(rw0("MontantPlanche")) <= MontantEstim0 Then
                                                    LaMethode0 = rw0("CodeProcAO")
                                                    LaRevue0 = rw0("TypeExamenAO")
                                                    ExceptMethode0 = rw0("ExceptionRevue")
                                                    CodeMethode0 = rw("CodeProcAO")

                                                End If
                                            ElseIf (rw0("MontantPlafond").ToString = "NL") Then
                                                If CDec(rw0("MontantPlanche")) <= MontantEstim0 Then
                                                    LaMethode0 = rw("CodeProcAO")
                                                    LaRevue0 = rw0("TypeExamenAO")
                                                    ExceptMethode0 = rw0("ExceptionRevue")
                                                    CodeMethode0 = rw0("CodeProcAO")

                                                End If
                                            Else
                                                If CDec(rw0("MontantPlanche")) <= MontantEstim0 And CDec(rw0("MontantPlafond")) > MontantEstim0 Then
                                                    LaMethode0 = rw0("CodeProcAO")
                                                    LaRevue0 = rw0("TypeExamenAO")
                                                    ExceptMethode0 = rw0("ExceptionRevue")
                                                    CodeMethode0 = rw0("CodeProcAO")

                                                End If
                                            End If
                                        Else
                                            If (rw0("PlafondInclu").ToString = "OUI") Then
                                                If CDec(rw0("MontantPlanche")) < MontantEstim0 Then
                                                    LaMethode0 = rw0("CodeProcAO")
                                                    LaRevue0 = rw0("TypeExamenAO")
                                                    ExceptMethode0 = rw0("ExceptionRevue")
                                                    CodeMethode0 = rw0("CodeProcAO")

                                                End If
                                            Else
                                                If (rw0("MontantPlafond").ToString <> "TM" And rw0("MontantPlafond").ToString <> "NL") Then
                                                    If CDec(rw0("MontantPlanche")) < MontantEstim0 Then
                                                        LaMethode0 = rw0("CodeProcAO")
                                                        LaRevue0 = rw0("TypeExamenAO")
                                                        ExceptMethode0 = rw0("ExceptionRevue")
                                                        CodeMethode0 = rw0("CodeProcAO")

                                                    End If
                                                ElseIf (rw0("MontantPlanche") <> "TM") Then
                                                    If (rw0("MontantPlafond").ToString = "NL") Then
                                                        If (CDec(rw0("MontantPlanche")) < MontantEstim0) Then
                                                            LaMethode0 = rw0("CodeProcAO")
                                                            LaRevue0 = rw0("TypeExamenAO")
                                                            ExceptMethode0 = rw0("ExceptionRevue")
                                                            CodeMethode0 = rw0("CodeProcAO")

                                                        End If

                                                    ElseIf (rw("MontantPlanche") = "TM") Then
                                                        LaMethode0 = rw0("CodeProcAO")
                                                        LaRevue0 = rw0("TypeExamenAO")
                                                        ExceptMethode0 = rw0("ExceptionRevue")
                                                        CodeMethode0 = rw0("CodeProcAO")
                                                    End If
                                                End If
                                            End If
                                        End If
                                    Next
                                End If
                                If (ExceptMethode0 <> "") Then LaRevue0 = LaRevue0 & "*"
                                'Mise à jour des montants estimatifs, Méthodes et Revues dans la table marché *************************************************************
                                If ElaboPPM = "Tous les bailleurs" Then
                                    query = "UPDATE T_Marche SET MontantEstimatif='" & MontantEstim0 & "',MethodeMarche ='" & LaMethode0 & "',RevuePrioPost ='" & LaRevue0 & "', CodeProcAO ='" & CodeMethode0 & "', InitialeBailleur='" & lesBailleurs0 & "', CodeConvention='" & lesConventions0 & "', Convention_ChefFile='" & ChefFile & "', DerniereMaj ='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' WHERE RefMarche='" & rw("RefMarche").ToString & "'"
                                Else
                                    query = "UPDATE T_Marche SET MontantEstimatif='" & MontantEstim0 & "',MethodeMarche ='" & LaMethode0 & "',RevuePrioPost ='" & LaRevue0 & "', CodeProcAO ='" & CodeMethode0 & "', DerniereMaj ='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' WHERE RefMarche='" & rw("RefMarche").ToString & "'"
                                End If
                                ExecuteNonQuery(query)
                            End If
                        ElseIf dtResult3 = 0 Then
                            Dim dtNewAllocation As New DataTable
                            If ElaboPPM = "Tous les bailleurs" Then
                                query = "select DISTINCT P.NumeroComptable, B.InitialeBailleur, SUM(R.MontantBailleur) as MontantBailleurs from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebutMarche.Text) & "' AND DateFinPartition<='" & dateconvert(DateFinMarche.Text) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & cmbTypeMarche.Text & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and R.MontantBailleur<>'0' GROUP BY P.NumeroComptable"
                                dtNewAllocation = ExcecuteSelectQuery(query)
                            Else
                                'query = "select DISTINCT P.NumeroComptable,B.InitialeBailleur from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebut) & "' AND DateDebutPartition<='" & dateconvert(DateFin) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & TypeMarche & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and B.InitialeBailleur='" & Bailleur & "' and R.MontantBailleur<>'0' and R.CodeConvention='" & Convention & "' AND P.RefBesoinPartition NOT IN(SELECT RefBesoinPartition FROM t_besoinmarche WHERE RefMarche='" & rw("RefMarche").ToString & "') AND P.NumeroComptable='" & rwAlloc("NumeroComptable") & "'"
                                query = "select DISTINCT P.NumeroComptable,B.InitialeBailleur from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebut) & "' AND DateDebutPartition<='" & dateconvert(DateFin) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & TypeMarche & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and B.InitialeBailleur='" & Bailleur & "' and R.MontantBailleur<>'0' and R.CodeConvention='" & Convention & "' AND P.RefBesoinPartition NOT IN(SELECT RefBesoinPartition FROM t_besoinmarche) AND P.NumeroComptable='" & rwAlloc("NumeroComptable") & "'"
                                dtNewAllocation = ExcecuteSelectQuery(query)
                            End If
                            If dtNewAllocation.Rows.Count > 0 Then
                                Dim LibelleCompte0 As String = ""
                                query = "select LIBELLE_SC from T_COMP_SOUS_CLASSE where CODE_SC='" & rwAlloc("NumeroComptable") & "'"
                                Dim dt0 = ExcecuteSelectQuery(query)
                                For Each rw3 As DataRow In dt0.Rows
                                    LibelleCompte0 = MettreApost(rw3("LIBELLE_SC").ToString)
                                Next
                                Dim lesBailleurs0 As String = ""
                                Dim lesConventions0 As String = ""
                                If ElaboPPM = "Tous les bailleurs" Then
                                    If bailleurs.Count > 1 Then
                                        For i = 0 To bailleurs.Count - 1
                                            lesBailleurs0 += bailleurs.Item(i) & " | "
                                        Next
                                    Else
                                        For i = 0 To bailleurs.Count - 1
                                            lesBailleurs0 += bailleurs.Item(i)
                                        Next
                                    End If
                                    If Conventions.Count > 1 Then
                                        For i = 0 To Conventions.Count - 1
                                            lesConventions0 += Conventions.Item(i) & " | "
                                        Next
                                    Else
                                        For i = 0 To Conventions.Count - 1
                                            lesConventions0 += Conventions.Item(i)
                                        Next
                                    End If
                                    'Insertion du marché
                                    query = "INSERT INTO T_Marche(CodeProjet,NumeroComptable,TypeMarche,DescriptionMarche,PeriodeMarche,InitialeBailleur,CodeConvention,Convention_ChefFile,JoursCompte,RefPPM, ModePPM) "
                                    query &= "VALUES('" & ProjetEnCours & "','" & rwAlloc("NumeroComptable") & "','" & cmbTypeMarche.Text & "','" & EnleverApost(LibelleCompte0) & "','" & Trim(Periode) & "','" & lesBailleurs0 & "','" & lesConventions0 & "','" & ChefFile & "','" & JoursCompte & "','" & RefPlan & "','Tous_Bailleurs')"
                                    ExecuteNonQuery(query)
                                Else
                                    'Insertion du marché
                                    query = "INSERT INTO T_Marche(CodeProjet,NumeroComptable,TypeMarche,DescriptionMarche,PeriodeMarche,InitialeBailleur,CodeConvention,Convention_ChefFile,ModePPM,JoursCompte,RefPPM) "
                                    query &= "VALUES('" & ProjetEnCours & "','" & rwAlloc("NumeroComptable") & "','" & TypeMarche & "','" & EnleverApost(LibelleCompte0) & "','" & Trim(Periode) & "','" & Bailleur & "','" & Convention & "','" & Convention & "','Bailleur','" & JoursCompte & "','" & RefPlan & "')"
                                    ExecuteNonQuery(query)
                                End If
                                Dim DernierIndex0 As Decimal = 0

                                If ElaboPPM = "Tous les bailleurs" Then
                                    'Liaison du marché avec les étapes de son type de marché **************************************************
                                    query = "select RefMarche from T_Marche where CodeProjet='" & ProjetEnCours & "' and NumeroComptable='" & rwAlloc("NumeroComptable") & "' and PeriodeMarche='" & Periode & "' and TypeMarche='" & TypeMarche & "' and CodeConvention='" & lesConventions0 & "'"
                                    Dim dt1 = ExcecuteSelectQuery(query)
                                    For Each rw3 As DataRow In dt1.Rows
                                        DernierIndex0 = rw(0)
                                    Next
                                Else
                                    'Liaison du marché avec les étapes de son type de marché **************************************************
                                    query = "select MAX(RefMarche) from T_Marche where CodeProjet='" & ProjetEnCours & "' and NumeroComptable='" & rwAlloc("NumeroComptable") & "' and PeriodeMarche='" & Periode & "' and TypeMarche='" & TypeMarche & "' and CodeConvention='" & Convention & "'"
                                    Dim dt2 = ExcecuteSelectQuery(query)
                                    For Each rw3 As DataRow In dt2.Rows
                                        DernierIndex0 = rw3(0)
                                    Next
                                End If
                                'Recuperation du montant estimatif du marche
                                Dim MontantEstim0 As Decimal = 0
                                Dim dtRepartition0 As DataTable
                                If ElaboPPM = "Tous les bailleurs" Then
                                    query = "select P.RefBesoinPartition,R.MontantBailleur from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebut) & "' AND DateFinPartition<='" & dateconvert(DateFin) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & TypeMarche & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and R.MontantBailleur<>'0'"
                                    dtRepartition0 = ExcecuteSelectQuery(query)
                                    query = "DELETE from T_BesoinMarche where RefMarche='" & DernierIndex0 & "'"
                                    ExecuteNonQuery(query)
                                    For Each rwRepartition As DataRow In dtRepartition0.Rows
                                        query = "UPDATE T_RepartitionParBailleur SET RefMarche='" & DernierIndex0 & "' where RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "' and CodeConvention='" & Convention & "' AND MontantBailleur<>'0'"   'RefBesoinPartition='" & ListeRefBesoin(w) & "' and 
                                        ExecuteNonQuery(query)
                                        query = "INSERT INTO T_BesoinMarche(RefBesoinPartition,RefMarche) VALUES('" & rwRepartition("RefBesoinPartition") & "','" & DernierIndex0 & "')"
                                        ExecuteNonQuery(query)
                                        MontantEstim0 += rwRepartition("MontantBailleur")
                                    Next
                                Else
                                    query = "select P.RefBesoinPartition,R.MontantBailleur from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebut) & "' AND DateFinPartition<='" & dateconvert(DateFin) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & TypeMarche & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and B.InitialeBailleur='" & Bailleur & "' and R.MontantBailleur<>'0' and R.CodeConvention='" & Convention & "' AND P.RefBesoinPartition NOT IN(SELECT RefBesoinPartition FROM t_besoinmarche ) AND P.NumeroComptable='" & rwAlloc("NumeroComptable") & "'"
                                    dtRepartition0 = ExcecuteSelectQuery(query)
                                    query = "DELETE from T_BesoinMarche where RefMarche='" & DernierIndex0 & "'"
                                    ExecuteNonQuery(query)
                                    For Each rwRepartition As DataRow In dtRepartition0.Rows
                                        query = "UPDATE T_RepartitionParBailleur SET RefMarche='" & DernierIndex0 & "' where RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "' and CodeConvention='" & Convention & "' AND MontantBailleur<>'0'"   'RefBesoinPartition='" & ListeRefBesoin(w) & "' and 
                                        ExecuteNonQuery(query)
                                        query = "INSERT INTO T_BesoinMarche(RefBesoinPartition,RefMarche) VALUES('" & rwRepartition("RefBesoinPartition") & "','" & DernierIndex0 & "')"
                                        ExecuteNonQuery(query)
                                        MontantEstim0 += rwRepartition("MontantBailleur")
                                    Next
                                End If
                                'Verification de methode auto
                                Dim MethodeAuto0 As Boolean = True
                                query = "select MethodeMarcheAuto from T_ParamTechProjet where CodeProjet='" & ProjetEnCours & "' and MethodeMarcheAuto='NON'"
                                Dim dt3 = ExcecuteSelectQuery(query)
                                For Each rw3 As DataRow In dt3.Rows
                                    MethodeAuto0 = False
                                    Exit For
                                Next
                                Dim LaMethode0 As String = ""
                                Dim LaRevue0 As String = ""
                                Dim ExceptMethode0 As String = ""
                                Dim CodeMethode0 As Decimal = 0
                                If (MethodeAuto0 = True) Then

                                    If ElaboPPM = "Tous les bailleurs" Then
                                        query = "select P.CodeProcAO,P.AbregeAO,P.TypeMarcheAO,S.CodeProcAO,S.MontantPlanche,S.PlancheInclu,S.MontantPlafond,S.PlafondInclu,S.TypeExamenAO,S.ExceptionRevue,S.CodeSeuil from T_ProcAO as P,T_Seuil as S where P.CodeProcAO=S.CodeProcAO and P.TypeMarcheAO='" & cmbTypeMarche.Text & "' and P.CodeProjet='" & ProjetEnCours & "' and P.RechAuto='OUI' order by S.MontantPlanche"
                                    Else
                                        query = "select P.CodeProcAO,P.AbregeAO,P.TypeMarcheAO,S.CodeProcAO,S.MontantPlanche,S.PlancheInclu,S.MontantPlafond,S.PlafondInclu,S.TypeExamenAO,S.ExceptionRevue,S.CodeSeuil from T_ProcAO as P,T_Seuil as S where P.CodeProcAO=S.CodeProcAO and P.TypeMarcheAO='" & cmbTypeMarche.Text & "' and P.CodeProjet='" & ProjetEnCours & "' and S.Bailleur='" & BailleurConcerne.Text & "' and P.RechAuto='OUI' order by S.MontantPlanche"
                                    End If
                                    Dim dt4 = ExcecuteSelectQuery(query)
                                    For Each rw4 As DataRow In dt4.Rows
                                        If (rw4("PlancheInclu").ToString = "OUI") Then
                                            If (rw4("PlafondInclu").ToString = "OUI") Then
                                                If CDec(rw4("MontantPlanche")) <= MontantEstim0 Then
                                                    LaMethode0 = rw4("CodeProcAO")
                                                    LaRevue0 = rw4("TypeExamenAO")
                                                    ExceptMethode0 = rw4("ExceptionRevue")
                                                    CodeMethode0 = rw4("CodeProcAO")


                                                End If
                                            ElseIf (rw4("MontantPlafond").ToString = "NL") Then
                                                If CDec(rw4("MontantPlanche")) <= MontantEstim0 Then
                                                    LaMethode0 = rw4("CodeProcAO")
                                                    LaRevue0 = rw4("TypeExamenAO")
                                                    ExceptMethode0 = rw4("ExceptionRevue")
                                                    CodeMethode0 = rw4("CodeProcAO")


                                                End If
                                            Else
                                                If CDec(rw4("MontantPlanche")) <= MontantEstim0 And CDec(rw4("MontantPlafond")) > MontantEstim0 Then
                                                    LaMethode0 = rw4("CodeProcAO")
                                                    LaRevue0 = rw4("TypeExamenAO")
                                                    ExceptMethode0 = rw4("ExceptionRevue")
                                                    CodeMethode0 = rw4("CodeProcAO")

                                                End If
                                            End If
                                        Else
                                            If (rw4("PlafondInclu").ToString = "OUI") Then
                                                If CDec(rw4("MontantPlanche")) < MontantEstim0 Then
                                                    LaMethode0 = rw4("CodeProcAO")
                                                    LaRevue0 = rw4("TypeExamenAO")
                                                    ExceptMethode0 = rw4("ExceptionRevue")
                                                    CodeMethode0 = rw4("CodeProcAO")

                                                End If
                                            Else
                                                If (rw4("MontantPlafond").ToString <> "TM" And rw4("MontantPlafond").ToString <> "NL") Then
                                                    If CDec(rw4("MontantPlanche")) < MontantEstim0 Then
                                                        LaMethode0 = rw4("CodeProcAO")
                                                        LaRevue0 = rw4("TypeExamenAO")
                                                        ExceptMethode0 = rw4("ExceptionRevue")
                                                        CodeMethode0 = rw4("CodeProcAO")


                                                    End If
                                                ElseIf (rw4("MontantPlanche") <> "TM") Then
                                                    If (rw4("MontantPlafond").ToString = "NL") Then
                                                        If (CDec(rw4("MontantPlanche")) < MontantEstim0) Then
                                                            LaMethode0 = rw4("CodeProcAO")
                                                            LaRevue0 = rw4("TypeExamenAO")
                                                            ExceptMethode0 = rw4("ExceptionRevue")
                                                            CodeMethode0 = rw4("CodeProcAO")


                                                        End If

                                                    ElseIf (rw4("MontantPlanche") = "TM") Then
                                                        LaMethode0 = rw4("CodeProcAO")
                                                        LaRevue0 = rw4("TypeExamenAO")
                                                        ExceptMethode0 = rw4("ExceptionRevue")
                                                        CodeMethode0 = rw4("CodeProcAO")

                                                    End If
                                                End If
                                            End If
                                        End If
                                    Next
                                End If
                                If (ExceptMethode0 <> "") Then LaRevue0 = LaRevue0 & "*"
                                'Mise à jour des montants estimatifs, Méthodes et Revues dans la table marché *************************************************************
                                query = "UPDATE T_Marche SET MontantEstimatif='" & MontantEstim0 & "',MethodeMarche ='" & LaMethode0 & "',RevuePrioPost ='" & LaRevue0 & "', CodeProcAO ='" & CodeMethode0 & "', DerniereMaj ='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' WHERE RefMarche='" & DernierIndex0 & "'"
                                ExecuteNonQuery(query)
                            End If
                        End If
                    End If
                Next
            End If
        Next
        query = "SELECT * FROM t_marche WHERE RefPPM='" & RefPlan & "'"
        Dim NewdtMarche = ExcecuteSelectQuery(query)
        If dtMarche.Rows.Count <> NewdtMarche.Rows.Count Then
            modif = True
            NiveauActu += 1
        End If

        For Each rw In dtMarche.Rows
            If rw("NiveauActu").ToString <> "" Then
                NiveauActu = CInt(rw("NiveauActu").ToString)
            Else
                NiveauActu = 0
            End If
            query = "INSERT INTO t_ppm_historiquemarche(CodeProjet,RefMarche,NumeroComptable,TypeMarche,DescriptionMarche,PeriodeMarche,InitialeBailleur,CodeConvention,Convention_ChefFile,JoursCompte,RefPPM, ModePPM,NiveauActu) "
            query &= "VALUES('" & ProjetEnCours & "','" & rw("RefMarche").ToString & "','" & rw("NumeroComptable").ToString & "','" & rw("TypeMarche").ToString & "','" & rw("DescriptionMarche").ToString & "','" & rw("PeriodeMarche").ToString & "','" & rw("InitialeBailleur").ToString & "','" & rw("CodeConvention").ToString & "','" & rw("Convention_ChefFile").ToString & "','" & rw("JoursCompte").ToString & "','" & RefPlan & "','" & rw("ModePPM").ToString & "','" & NiveauActu & "')"
            ExecuteNonQuery(query)
        Next
    End Function
    Private Function RechercherLesInfos_old(RefPlan As Integer) As Integer

        Me.Cursor = Cursors.WaitCursor
        Dim JoursCompte As String = ""
        If LunDi.Checked Then JoursCompte = "Lun"
        If MarDi.Checked Then JoursCompte = JoursCompte & ";Mar"
        If MercreDi.Checked Then JoursCompte = JoursCompte & ";Mer"
        If JeuDi.Checked Then JoursCompte = JoursCompte & ";Jeu"
        If VendreDi.Checked <> True Then Else JoursCompte = JoursCompte & ";Ven"
        If SameDi.Checked Then JoursCompte = JoursCompte & ";Sam"
        If DimanChe.Checked Then JoursCompte = JoursCompte & ";Dim"

        Dim CodeTypeMarche As String = String.Empty
        If cmbTypeMarche.Text.ToLower() = "Consultants".ToLower() Then
            CodeTypeMarche = "CS"
        ElseIf cmbTypeMarche.Text.ToLower() = "Fournitures".ToLower() Then
            CodeTypeMarche = "FR"
        ElseIf cmbTypeMarche.Text.ToLower() = "Services autres que les services de consultants".ToLower() Then
            CodeTypeMarche = "SA"
        ElseIf cmbTypeMarche.Text.ToLower() = "Travaux".ToLower() Then
            CodeTypeMarche = "TX"
        End If
        Dim Periode As String = DateDebutMarche.Text & " - " & DateFinMarche.Text

        'Recuperation des comptes à marché qui sont sur les activités de la période, du bailleur et de la convention indiqué
        query = "select DISTINCT P.NumeroComptable,B.InitialeBailleur from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebutMarche.Text) & "' AND DateFinPartition<='" & dateconvert(DateFinMarche.Text) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & cmbTypeMarche.Text & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and B.InitialeBailleur='" & BailleurConcerne.Text & "' and R.MontantBailleur<>'0' and R.CodeConvention='" & CmbConvention.Text & "'"
        Dim dtAllocation As DataTable = ExcecuteSelectQuery(query)
        If dtAllocation.Rows.Count = 0 Then
            GridPlanMarche.Rows.Clear()
            Dim NbCol As Decimal = GridPlanMarche.ColumnCount
            If (NbCol > 0) Then
                For i As Integer = 1 To NbCol - 1
                    GridPlanMarche.Columns.Remove("A")
                Next
            End If
            'On verifie si il y'a des activites sur la periode pour personnaliser le message de retour
            query = "SELECT COUNT(*) FROM T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebutMarche.Text) & "' AND DateFinPartition<='" & dateconvert(DateFinMarche.Text) & "' and CodeProjet='" & ProjetEnCours & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                SuccesMsg("Aucun compte à marché trouvé.")
                Return -1
            End If
            Return -2
        End If

        For Each rwAlloc As DataRow In dtAllocation.Rows
            query = "select * from T_Marche where CodeProjet='" & ProjetEnCours & "' and NumeroComptable='" & rwAlloc("NumeroComptable") & "' and PeriodeMarche='" & Periode & "' and TypeMarche='" & cmbTypeMarche.Text & "' and InitialeBailleur='" & BailleurConcerne.Text & "' and CodeConvention='" & CmbConvention.Text & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                Exit For 'On ignore le marche déjà enregistré sur le compte à marché de la période et de la convention
            Next

            Dim LibelleCompte As String = ""
            query = "select LIBELLE_SC from T_COMP_SOUS_CLASSE where CODE_SC='" & rwAlloc("NumeroComptable") & "'"
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                LibelleCompte = MettreApost(rw("LIBELLE_SC").ToString)
            Next

            'Insertion du marché
            query = "INSERT INTO T_Marche(CodeProjet,NumeroComptable,TypeMarche,DescriptionMarche,PeriodeMarche,InitialeBailleur,CodeConvention,JoursCompte,RefPPM) "
            query &= "VALUES('" & ProjetEnCours & "','" & rwAlloc("NumeroComptable") & "','" & cmbTypeMarche.Text & "','" & EnleverApost(LibelleCompte) & "','" & Trim(Periode) & "','" & BailleurConcerne.Text & "','" & CmbConvention.Text & "','" & JoursCompte & "','" & RefPlan & "')"
            ExecuteNonQuery(query)

            'Liaison du marché avec les étapes de son type de marché **************************************************
            Dim DernierIndex As Decimal = 0
            query = "select RefMarche from T_Marche where CodeProjet='" & ProjetEnCours & "' and NumeroComptable='" & rwAlloc("NumeroComptable") & "' and PeriodeMarche='" & Periode & "' and TypeMarche='" & cmbTypeMarche.Text & "' and CodeConvention='" & CmbConvention.Text & "'"
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                DernierIndex = rw(0)
            Next

            'Recuperation du montant estimatif du marche
            Dim MontantEstim As Decimal = 0
            query = "select P.RefBesoinPartition,R.MontantBailleur from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebutMarche.Text) & "' AND DateFinPartition<='" & dateconvert(DateFinMarche.Text) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & cmbTypeMarche.Text & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and B.InitialeBailleur='" & BailleurConcerne.Text & "' and R.MontantBailleur<>'0' and R.CodeConvention='" & CmbConvention.Text & "'"
            Dim dtRepartition As DataTable = ExcecuteSelectQuery(query)
            For Each rwRepartition As DataRow In dtRepartition.Rows
                query = "UPDATE T_RepartitionParBailleur SET RefMarche='" & DernierIndex & "' where RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "' and CodeConvention='" & CmbConvention.Text & "' AND MontantBailleur<>'0'"   'RefBesoinPartition='" & ListeRefBesoin(w) & "' and 
                ExecuteNonQuery(query)

                query = "DELETE from T_BesoinMarche where RefMarche='" & DernierIndex & "'"
                ExecuteNonQuery(query)

                query = "INSERT INTO T_BesoinMarche(RefBesoinPartition,RefMarche) VALUES('" & rwRepartition("RefBesoinPartition") & "','" & DernierIndex & "')"
                ExecuteNonQuery(query)
                MontantEstim += rwRepartition("MontantBailleur")
            Next

            'Verification de methode auto
            Dim MethodeAuto As Boolean = True
            query = "select MethodeMarcheAuto from T_ParamTechProjet where CodeProjet='" & ProjetEnCours & "' and MethodeMarcheAuto='NON'"
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                MethodeAuto = False
                Exit For
            Next
            Dim LaMethode As String = ""
            Dim LaRevue As String = ""
            Dim ExceptMethode As String = ""
            Dim CodeMethode As Decimal = 0
            Dim KodSeuil As Decimal = 0
            If (MethodeAuto = True) Then

                query = "select P.CodeProcAO,P.AbregeAO,P.TypeMarcheAO,S.CodeProcAO,S.MontantPlanche,S.PlancheInclu,S.MontantPlafond,S.PlafondInclu,S.TypeExamenAO,S.ExceptionRevue,S.CodeSeuil from T_ProcAO as P,T_Seuil as S where P.CodeProcAO=S.CodeProcAO and P.TypeMarcheAO='" & cmbTypeMarche.Text & "' and P.CodeProjet='" & ProjetEnCours & "' and P.RechAuto='OUI' order by S.MontantPlanche"
                dt = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    If (rw("PlancheInclu").ToString = "OUI") Then
                        If (rw("PlafondInclu").ToString = "OUI") Then
                            If CDec(rw("MontantPlanche")) <= MontantEstim Then
                                LaMethode = rw("CodeProcAO")
                                LaRevue = rw("TypeExamenAO")
                                ExceptMethode = rw("ExceptionRevue")
                                CodeMethode = rw("CodeProcAO")
                                KodSeuil = rw("CodeSeuil")
                            End If
                        ElseIf (rw("MontantPlafond").ToString = "NL") Then
                            If CDec(rw("MontantPlanche")) <= MontantEstim Then
                                LaMethode = rw("CodeProcAO")
                                LaRevue = rw("TypeExamenAO")
                                ExceptMethode = rw("ExceptionRevue")
                                CodeMethode = rw("CodeProcAO")
                                KodSeuil = rw("CodeSeuil")
                            End If
                        Else
                            If CDec(rw("MontantPlanche")) <= MontantEstim And CDec(rw("MontantPlafond")) > MontantEstim Then
                                LaMethode = rw("CodeProcAO")
                                LaRevue = rw("TypeExamenAO")
                                ExceptMethode = rw("ExceptionRevue")
                                CodeMethode = rw("CodeProcAO")
                                KodSeuil = rw("CodeSeuil")
                            End If
                        End If
                    Else
                        If (rw("PlafondInclu").ToString = "OUI") Then
                            If CDec(rw("MontantPlanche")) < MontantEstim Then
                                LaMethode = rw("CodeProcAO")
                                LaRevue = rw("TypeExamenAO")
                                ExceptMethode = rw("ExceptionRevue")
                                CodeMethode = rw("CodeProcAO")
                                KodSeuil = rw("CodeSeuil")
                            End If
                        Else
                            If (rw("MontantPlafond").ToString <> "TM" And rw("MontantPlafond").ToString <> "NL") Then
                                If CDec(rw("MontantPlanche")) < MontantEstim Then
                                    LaMethode = rw("CodeProcAO")
                                    LaRevue = rw("TypeExamenAO")
                                    ExceptMethode = rw("ExceptionRevue")
                                    CodeMethode = rw("CodeProcAO")
                                    KodSeuil = rw("CodeSeuil")
                                End If
                            ElseIf (rw("MontantPlanche") <> "TM") Then
                                If (rw("MontantPlafond").ToString = "NL") Then
                                    If (CDec(rw("MontantPlanche")) < MontantEstim) Then
                                        LaMethode = rw("CodeProcAO")
                                        LaRevue = rw("TypeExamenAO")
                                        ExceptMethode = rw("ExceptionRevue")
                                        CodeMethode = rw("CodeProcAO")
                                        KodSeuil = rw("CodeSeuil")
                                    End If

                                ElseIf (rw("MontantPlanche") = "TM") Then
                                    LaMethode = rw("CodeProcAO")
                                    LaRevue = rw("TypeExamenAO")
                                    ExceptMethode = rw("ExceptionRevue")
                                    CodeMethode = rw("CodeProcAO")
                                    KodSeuil = rw("CodeSeuil")
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            If (ExceptMethode <> "") Then LaRevue = LaRevue & "*"

            'Mise à jour des montants estimatifs, Méthodes et Revues dans la table marché *************************************************************
            query = "UPDATE T_Marche SET MontantEstimatif='" & MontantEstim & "',MethodeMarche ='" & LaMethode & "',RevuePrioPost ='" & LaRevue & "', CodeProcAO ='" & CodeMethode & "', CodeSeuil ='" & KodSeuil & "', DerniereMaj ='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' WHERE RefMarche='" & DernierIndex & "'"
            ExecuteNonQuery(query)
        Next
        Me.Cursor = Cursors.Default
        Return 0
    End Function
    Private Sub RemplirTableauPPM(RefPlan As Integer)
        'If (MarcheAConsulter.SelectedIndex > -1 And BailleurConcerne.SelectedIndex > -1 And CmbConvention.SelectedIndex > -1) Then
        If RefPlan <= 0 Then
            GridPlanMarche.Rows.Clear()
            Exit Sub
        End If

        GridPlanMarche.Rows.Clear()
        Dim NbCol As Decimal = GridPlanMarche.ColumnCount
        If (NbCol > 0) Then
            For i As Integer = 1 To NbCol - 1
                GridPlanMarche.Columns.Remove("A")
            Next
        End If

        Dim dtPlan As DataTable = ExcecuteSelectQuery("SELECT * FROM t_ppm_marche WHERE CodeProjet='" & ProjetEnCours & "' AND RefPPM='" & RefPlan & "'")
        If dtPlan.Rows.Count <= 0 Then
            Exit Sub
        End If
        Dim rwPlan As DataRow = dtPlan.Rows(0)
        Dim TypeM As String = rwPlan("TypeMarche").ToString
        Dim PeriodeM As String = rwPlan("PeriodePlan").ToString
        'Dim InitialeBailleur As String = rwPlan("InitialeBailleur").ToString
        'Dim CodeConvention As String = rwPlan("CodeConvention").ToString
        Dim NumSuivi As Decimal = 0

        DebutChargement(True, "Chargement du plan en cours...")

        Dim TotEstimDoll As Decimal = 0
        Dim TotEstimFcfa As Decimal = 0
        ColDescription()
        If (TypeM = "Consultants") Then
            If ElaboPPM = "Tous les bailleurs" Then
                DonneesDeBase(3, "Bailleur")
            Else
                DonneesDeBase(3)
            End If
        Else
            If ElaboPPM = "Tous les bailleurs" Then
                DonneesDeBase(2, "Bailleur")
            Else
                DonneesDeBase(2)
            End If
        End If

        AfficherLesAutresColonnes()

        'La ligne des durées prévues ********************************************************
        Dim b As Decimal = GridPlanMarche.Rows.Add()
        GridPlanMarche.Rows.Item(b).DefaultCellStyle.BackColor = CouleurSeparateur.Color
        GridPlanMarche.Rows.Item(b).ReadOnly = True
        GridPlanMarche.Rows.Item(b).Height = 15
        If ModePPM = "Genere" Then
            query = "Select * from T_Marche where CodeProjet='" & ProjetEnCours & "' and RefPPM='" & RefPlan & "' ORDER BY DescriptionMarche"
        Else
            query = "Select * from T_Marche where CodeProjet='" & ProjetEnCours & "' and RefPPM='" & RefPlan & "' ORDER BY RefMarche"
        End If
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        Dim NbreColAjout As Integer = 0

        For Each rw As DataRow In dt.Rows
            NumSuivi = NumSuivi + 1
            If (TypeM <> "Consultants") Then
                'La ligne Plan Prevu ****************************************************************
                Dim n As Decimal = GridPlanMarche.Rows.Add()
                GridPlanMarche.Rows.Item(n).DefaultCellStyle.ForeColor = CouleurTexte.Color
                GridPlanMarche.Rows.Item(n).DefaultCellStyle.BackColor = CouleurPlan.Color
                GridPlanMarche.Rows.Item(n).Height = 35

                GridPlanMarche.Rows.Item(n).Cells(0).Value = "P" & rw("RefMarche").ToString
                GridPlanMarche.Rows.Item(n).Cells(0).ReadOnly = True

                GridPlanMarche.Rows.Item(n).Cells(1).Value = NumSuivi.ToString
                GridPlanMarche.Rows.Item(n).Cells(1).ReadOnly = True
                GridPlanMarche.Rows.Item(n).Cells(1).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                GridPlanMarche.Rows.Item(n).Cells(1).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)

                Dim DescM As String = rw("DescriptionMarche")
                GridPlanMarche.Rows.Item(n).Cells(2).Value = MettreApost(DescM)
                GridPlanMarche.Rows.Item(n).Cells(2).ReadOnly = True
                GridPlanMarche.Rows.Item(n).Cells(2).Style.Alignment = DataGridViewContentAlignment.BottomLeft
                GridPlanMarche.Rows.Item(n).Cells(2).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)

                'Les bailleurs et convention si c'est par projet
                If ElaboPPM = "Tous les bailleurs" Then
                    NbreColAjout = 2
                    GridPlanMarche.Rows.Item(n).Cells(3).Value = rw("InitialeBailleur").ToString
                    GridPlanMarche.Rows.Item(n).Cells(3).ReadOnly = True
                    GridPlanMarche.Rows.Item(n).Cells(3).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                    GridPlanMarche.Rows.Item(n).Cells(3).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)

                    GridPlanMarche.Rows.Item(n).Cells(4).Value = rw("CodeConvention").ToString
                    GridPlanMarche.Rows.Item(n).Cells(4).ReadOnly = True
                    GridPlanMarche.Rows.Item(n).Cells(4).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                    GridPlanMarche.Rows.Item(n).Cells(4).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)
                End If

                'Les conventions pour un bailleur
                'If CodeConvention = "Toutes" Then
                '    NbreColAjout = 1
                '    GridPlanMarche.Rows.Item(n).Cells(3).Value = rw("CodeConvention").ToString
                '    GridPlanMarche.Rows.Item(n).Cells(3).ReadOnly = True
                '    GridPlanMarche.Rows.Item(n).Cells(3).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                '    GridPlanMarche.Rows.Item(n).Cells(3).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)
                'End If
                'Les codes lot
                Dim NumeroDAO As String = ""
                query = "select NumeroDAO from t_dao where RefMarche='" & rw("RefMarche").ToString & "' AND Statut_DAO <>'Annuler' order by NumeroDAO"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw0 As DataRow In dt0.Rows
                    NumeroDAO = rw0(0).ToString
                Next

                'GridPlanMarche.Rows.Item(n).Cells(3 + NbreColAjout).Value = rw("NumeroDAO").ToString
                GridPlanMarche.Rows.Item(n).Cells(3 + NbreColAjout).Value = NumeroDAO
                GridPlanMarche.Rows.Item(n).Cells(3 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(n).Cells(3 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                GridPlanMarche.Rows.Item(n).Cells(3 + NbreColAjout).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)

                'Les codes lot
                Dim LesLots As String = ""
                'query = "select CodeLot from T_LotDAO where NumeroDAO='" & rw("NumeroDAO").ToString & "' order by CodeLot"
                query = "select CodeLot from T_LotDAO where NumeroDAO='" & NumeroDAO & "' order by CodeLot"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw0 As DataRow In dt1.Rows
                    LesLots = LesLots & "(" & rw0(0).ToString & ")"
                Next

                GridPlanMarche.Rows.Item(n).Cells(4 + NbreColAjout).Value = LesLots       'rw(7).ToString 'Les codes lot *****
                GridPlanMarche.Rows.Item(n).Cells(4 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(n).Cells(4 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                GridPlanMarche.Rows.Item(n).Cells(4 + NbreColAjout).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)

                Dim MontTaux As Decimal = 1
                'query = "select TauxDevise from T_Devise where AbregeDevise='US$'"
                query = "select TauxDevise from T_Devise where AbregeDevise='" & EnleverApost(cmbDevise.Text) & "'"
                dt0 = ExcecuteSelectQuery(query)
                For Each rw0 As DataRow In dt0.Rows
                    MontTaux = CDec(rw0(0))
                    Exit For
                Next

                Dim MontDollar As Decimal = CDec(IIf(rw("MontantEstimatif").ToString = "", 0, rw("MontantEstimatif").ToString))

                TotEstimFcfa = TotEstimFcfa + MontDollar    'Total des montants en cfa ***************

                MontDollar = MontDollar / MontTaux

                TotEstimDoll = TotEstimDoll + MontDollar     'Total des montants en $ *****************

                MontDollar = Math.Round(MontDollar, 2)
                GridPlanMarche.Rows.Item(n).Cells(5 + NbreColAjout).Value = AfficherMonnaie(MontDollar.ToString)
                GridPlanMarche.Rows.Item(n).Cells(5 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(n).Cells(5 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.BottomRight
                GridPlanMarche.Rows.Item(n).Cells(5 + NbreColAjout).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)

                GridPlanMarche.Rows.Item(n).Cells(6 + NbreColAjout).Value = AfficherMonnaie(rw("MontantEstimatif").ToString)
                GridPlanMarche.Rows.Item(n).Cells(6 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(n).Cells(6 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.BottomRight
                GridPlanMarche.Rows.Item(n).Cells(6 + NbreColAjout).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)

                GridPlanMarche.Rows.Item(n).Cells(7 + NbreColAjout).Value = GetMethode(rw("CodeProcAO").ToString)
                GridPlanMarche.Rows.Item(n).Cells(7 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(n).Cells(7 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                GridPlanMarche.Rows.Item(n).Cells(7 + NbreColAjout).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)

                GridPlanMarche.Rows.Item(n).Cells(8 + NbreColAjout).Value = ""
                GridPlanMarche.Rows.Item(n).Cells(8 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(n).Cells(8 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                GridPlanMarche.Rows.Item(n).Cells(8 + NbreColAjout).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)

                GridPlanMarche.Rows.Item(n).Cells(9 + NbreColAjout).Value = rw("RevuePrioPost").ToString
                GridPlanMarche.Rows.Item(n).Cells(9 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(n).Cells(9 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.BottomLeft
                GridPlanMarche.Rows.Item(n).Cells(9 + NbreColAjout).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)

                GridPlanMarche.Rows.Item(n).Cells(10 + NbreColAjout).Value = "Prévu"
                GridPlanMarche.Rows.Item(n).Cells(10 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(n).Cells(10 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.TopCenter

                'Les valeurs des colonnes etapes ***************
                For Ne As Integer = 1 To NbreColoEtape

                    query = "select DebutPrevu,FinPrevue from T_PlanMarche where RefMarche='" & rw("RefMarche") & "' and NumeroOrdre='" & Ne & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    For Each rw0 As DataRow In dt0.Rows
                        Dim ValEtp As String = "__/__/____"
                        GridPlanMarche.Rows.Item(n).Cells(10 + Ne).Style.ForeColor = Color.DarkGray
                        If (rw0(1).ToString <> "") Then
                            ValEtp = rw0(1).ToString
                            GridPlanMarche.Rows.Item(n).Cells(10 + Ne).Style.ForeColor = Color.Black
                        End If
                        GridPlanMarche.Rows.Item(n).Cells(10 + NbreColAjout + Ne).Value = ValEtp
                        GridPlanMarche.Rows.Item(n).Cells(10 + NbreColAjout + Ne).ReadOnly = True
                        GridPlanMarche.Rows.Item(n).Cells(10 + NbreColAjout + Ne).Style.Alignment = DataGridViewContentAlignment.TopCenter
                    Next

                Next

                'La ligne Plan Réalisé ***************************************************************************
                Dim m As Decimal = GridPlanMarche.Rows.Add()
                GridPlanMarche.Rows.Item(m).DefaultCellStyle.BackColor = CouleurRealise.Color
                GridPlanMarche.Rows.Item(m).DefaultCellStyle.ForeColor = CouleurTexte2.Color
                GridPlanMarche.Rows.Item(m).Height = 15

                GridPlanMarche.Rows.Item(m).Cells(0).Value = "R" & rw("RefMarche").ToString
                GridPlanMarche.Rows.Item(m).Cells(0).ReadOnly = True

                GridPlanMarche.Rows.Item(m).Cells(1).Value = ""                     'NumSuivi.ToString
                GridPlanMarche.Rows.Item(m).Cells(1).ReadOnly = True
                GridPlanMarche.Rows.Item(m).Cells(1).Style.Alignment = DataGridViewContentAlignment.TopCenter

                GridPlanMarche.Rows.Item(m).Cells(2).Value = ""
                GridPlanMarche.Rows.Item(m).Cells(2).ReadOnly = True
                GridPlanMarche.Rows.Item(m).Cells(2).Style.Alignment = DataGridViewContentAlignment.MiddleLeft

                GridPlanMarche.Rows.Item(m).Cells(6 + NbreColAjout).Value = ""
                GridPlanMarche.Rows.Item(m).Cells(6 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(m).Cells(6 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.MiddleRight

                GridPlanMarche.Rows.Item(m).Cells(7 + NbreColAjout).Value = ""
                GridPlanMarche.Rows.Item(m).Cells(7 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(m).Cells(7 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                GridPlanMarche.Rows.Item(m).Cells(8 + NbreColAjout).Value = ""
                GridPlanMarche.Rows.Item(m).Cells(8 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(m).Cells(8 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                GridPlanMarche.Rows.Item(m).Cells(9 + NbreColAjout).Value = ""
                GridPlanMarche.Rows.Item(m).Cells(9 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(m).Cells(9 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.MiddleLeft

                GridPlanMarche.Rows.Item(m).Cells(10 + NbreColAjout).Value = "Réalisé"
                GridPlanMarche.Rows.Item(m).Cells(10 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(m).Cells(10 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                GridPlanMarche.Rows.Item(m).Cells(10 + NbreColAjout).Style.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)

                'For Ne As Integer = 1 To NbreColoEtape
                '    query = "select DebutEffectif,DebutPrevu,FinEffective,FinPrevue from T_PlanMarche where RefMarche='" & rw("RefMarche") & "' and NumeroOrdre='" & Ne & "'"
                '    dt0 = ExcecuteSelectQuery(query)
                '    For Each rw0 As DataRow In dt0.Rows
                '        Dim Result1 As String = "__/__/____"
                '        GridPlanMarche.Rows.Item(m).Cells(10 + Ne).Style.ForeColor = Color.DarkGray
                '        If (rw0(2).ToString <> "") Then
                '            Result1 = rw0(2).ToString
                '            GridPlanMarche.Rows.Item(m).Cells(10 + Ne).Style.ForeColor = Color.Black
                '        End If
                '        GridPlanMarche.Rows.Item(m).Cells(10 + Ne).Value = Result1
                '        GridPlanMarche.Rows.Item(m).Cells(10 + Ne).ReadOnly = True
                '        GridPlanMarche.Rows.Item(m).Cells(10 + Ne).Style.Alignment = DataGridViewContentAlignment.BottomCenter

                '        If (rw0(2).ToString <> rw0(3).ToString And Result1 <> "__/__/____") Then
                '            GridPlanMarche.Rows.Item(m).Cells(10 + Ne).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)
                '        Else
                '            GridPlanMarche.Rows.Item(m).Cells(10 + Ne).Style.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
                '        End If
                '    Next
                'Next
                For Ne As Integer = 1 To NbreColoEtape
                    'query = "select DebutEffectif,DebutPrevu,FinEffective,FinPrevue from T_PlanMarche where RefMarche='" & rw("RefMarche") & "' and NumeroOrdre='" & Ne & "'"
                    query = "select FinEffective from T_PlanMarche where RefMarche='" & rw("RefMarche") & "' and NumeroOrdre='" & Ne & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    For Each rw0 As DataRow In dt0.Rows
                        Dim Result1 As String = "__/__/____"
                        GridPlanMarche.Rows.Item(m).Cells(10 + NbreColAjout + Ne).Style.ForeColor = Color.DarkGray
                        If (rw0(0).ToString <> "") Then
                            Result1 = rw0(0).ToString
                            GridPlanMarche.Rows.Item(m).Cells(10 + NbreColAjout + Ne).Style.ForeColor = Color.Black
                        End If
                        GridPlanMarche.Rows.Item(m).Cells(10 + NbreColAjout + Ne).Value = Result1
                        GridPlanMarche.Rows.Item(m).Cells(10 + NbreColAjout + Ne).ReadOnly = True
                        GridPlanMarche.Rows.Item(m).Cells(10 + NbreColAjout + Ne).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                    Next
                Next
                'la ligne separatrice *************************************************************************
                Dim k As Decimal = GridPlanMarche.Rows.Add()
                GridPlanMarche.Rows.Item(k).DefaultCellStyle.BackColor = CouleurSeparateur.Color
                GridPlanMarche.Rows.Item(k).ReadOnly = True
                GridPlanMarche.Rows.Item(k).Height = 10

            ElseIf (TypeM = "Consultants") Then

                'La ligne Plan Prevu ****************************************************************
                Dim n As Decimal = GridPlanMarche.Rows.Add()
                GridPlanMarche.Rows.Item(n).DefaultCellStyle.ForeColor = CouleurTexte.Color
                GridPlanMarche.Rows.Item(n).DefaultCellStyle.BackColor = CouleurPlan.Color
                GridPlanMarche.Rows.Item(n).Height = 35

                GridPlanMarche.Rows.Item(n).Cells(0).Value = "P" & rw("RefMarche").ToString
                GridPlanMarche.Rows.Item(n).Cells(0).ReadOnly = True

                GridPlanMarche.Rows.Item(n).Cells(1).Value = NumSuivi.ToString
                GridPlanMarche.Rows.Item(n).Cells(1).ReadOnly = True
                GridPlanMarche.Rows.Item(n).Cells(1).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                GridPlanMarche.Rows.Item(n).Cells(1).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)

                Dim DescM As String = rw("DescriptionMarche")
                GridPlanMarche.Rows.Item(n).Cells(2).Value = MettreApost(DescM)
                GridPlanMarche.Rows.Item(n).Cells(2).ReadOnly = True
                GridPlanMarche.Rows.Item(n).Cells(2).Style.Alignment = DataGridViewContentAlignment.BottomLeft
                GridPlanMarche.Rows.Item(n).Cells(2).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)

                'Les bailleurs et convention si c'est par projet
                If ElaboPPM = "Tous les bailleurs" Then
                    NbreColAjout = 2
                    GridPlanMarche.Rows.Item(n).Cells(3).Value = rw("InitialeBailleur").ToString
                    GridPlanMarche.Rows.Item(n).Cells(3).ReadOnly = True
                    GridPlanMarche.Rows.Item(n).Cells(3).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                    GridPlanMarche.Rows.Item(n).Cells(3).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)

                    GridPlanMarche.Rows.Item(n).Cells(4).Value = rw("CodeConvention").ToString
                    GridPlanMarche.Rows.Item(n).Cells(4).ReadOnly = True
                    GridPlanMarche.Rows.Item(n).Cells(4).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                    GridPlanMarche.Rows.Item(n).Cells(4).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)
                End If

                ''Les conventions pour un bailleur
                'If CodeConvention = "Toutes" Then
                '    NbreColAjout = 1
                '    GridPlanMarche.Rows.Item(n).Cells(3).Value = rw("CodeConvention").ToString
                '    GridPlanMarche.Rows.Item(n).Cells(3).ReadOnly = True
                '    GridPlanMarche.Rows.Item(n).Cells(3).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                '    GridPlanMarche.Rows.Item(n).Cells(3).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)
                'End If

                GridPlanMarche.Rows.Item(n).Cells(3 + NbreColAjout).Value = rw("Forfait_TpsPasse")
                GridPlanMarche.Rows.Item(n).Cells(3 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(n).Cells(3 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                GridPlanMarche.Rows.Item(n).Cells(3 + NbreColAjout).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)

                Dim MontTaux As Decimal = 1
                'query = "select TauxDevise from T_Devise where AbregeDevise='US$'"
                query = "select TauxDevise from T_Devise where AbregeDevise='" & EnleverApost(cmbDevise.Text) & "'"
                Dim dt0 = ExcecuteSelectQuery(query)
                For Each rw0 As DataRow In dt0.Rows
                    MontTaux = CDec(rw0(0))
                    Exit For
                Next

                Dim MontDollar As Decimal = Val(rw("MontantEstimatif").ToString())

                TotEstimFcfa = TotEstimFcfa + MontDollar    'Total des montants en cfa ***************

                MontDollar = MontDollar / MontTaux

                TotEstimDoll = TotEstimDoll + MontDollar     'Total des montants en $ *****************

                MontDollar = Math.Round(MontDollar, 2)
                GridPlanMarche.Rows.Item(n).Cells(4 + NbreColAjout).Value = AfficherMonnaie(MontDollar.ToString)
                GridPlanMarche.Rows.Item(n).Cells(4 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(n).Cells(4 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.BottomRight
                GridPlanMarche.Rows.Item(n).Cells(4 + NbreColAjout).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)

                GridPlanMarche.Rows.Item(n).Cells(5 + NbreColAjout).Value = AfficherMonnaie(rw("MontantEstimatif").ToString)
                GridPlanMarche.Rows.Item(n).Cells(5 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(n).Cells(5 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.BottomRight
                GridPlanMarche.Rows.Item(n).Cells(5 + NbreColAjout).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)

                GridPlanMarche.Rows.Item(n).Cells(6 + NbreColAjout).Value = GetMethode(rw("MethodeMarche").ToString)
                GridPlanMarche.Rows.Item(n).Cells(6 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(n).Cells(6 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                GridPlanMarche.Rows.Item(n).Cells(6 + NbreColAjout).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)

                GridPlanMarche.Rows.Item(n).Cells(7 + NbreColAjout).Value = rw("RevuePrioPost").ToString
                GridPlanMarche.Rows.Item(n).Cells(7 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(n).Cells(7 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                GridPlanMarche.Rows.Item(n).Cells(7 + NbreColAjout).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)

                GridPlanMarche.Rows.Item(n).Cells(8 + NbreColAjout).Value = "Prévu"
                GridPlanMarche.Rows.Item(n).Cells(8 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(n).Cells(8 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.TopCenter

                'Les valeurs des colonnes etapes ***************
                For Ne As Integer = 1 To NbreColoEtape

                    query = "select DebutPrevu,FinPrevue from T_PlanMarche where RefMarche='" & rw("RefMarche") & "' and NumeroOrdre='" & Ne & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    For Each rw0 As DataRow In dt0.Rows
                        Dim ValEtp2 As String = "__/__/____"
                        GridPlanMarche.Rows.Item(n).Cells(8 + NbreColAjout + Ne).Style.ForeColor = Color.DarkGray
                        If (rw0(1).ToString <> "") Then
                            ValEtp2 = rw0(1).ToString
                            GridPlanMarche.Rows.Item(n).Cells(8 + NbreColAjout + Ne).Style.ForeColor = Color.Black
                        End If
                        GridPlanMarche.Rows.Item(n).Cells(8 + NbreColAjout + Ne).Value = ValEtp2
                        GridPlanMarche.Rows.Item(n).Cells(8 + NbreColAjout + Ne).ReadOnly = True
                        GridPlanMarche.Rows.Item(n).Cells(8 + NbreColAjout + Ne).Style.Alignment = DataGridViewContentAlignment.TopCenter
                    Next

                Next

                'La ligne Plan Réalisé ***************************************************************************
                Dim m As Decimal = GridPlanMarche.Rows.Add()
                GridPlanMarche.Rows.Item(m).DefaultCellStyle.BackColor = CouleurRealise.Color
                GridPlanMarche.Rows.Item(m).DefaultCellStyle.ForeColor = CouleurTexte2.Color
                GridPlanMarche.Rows.Item(m).Height = 15

                GridPlanMarche.Rows.Item(m).Cells(0).Value = "R" & rw("RefMarche").ToString
                GridPlanMarche.Rows.Item(m).Cells(0).ReadOnly = True

                GridPlanMarche.Rows.Item(m).Cells(1).Value = ""                     'NumSuivi.ToString
                GridPlanMarche.Rows.Item(m).Cells(1).ReadOnly = True
                GridPlanMarche.Rows.Item(m).Cells(1).Style.Alignment = DataGridViewContentAlignment.TopCenter

                GridPlanMarche.Rows.Item(m).Cells(2).Value = ""
                GridPlanMarche.Rows.Item(m).Cells(2).ReadOnly = True
                GridPlanMarche.Rows.Item(m).Cells(2).Style.Alignment = DataGridViewContentAlignment.MiddleLeft

                GridPlanMarche.Rows.Item(m).Cells(6).Value = ""
                GridPlanMarche.Rows.Item(m).Cells(6).ReadOnly = True
                GridPlanMarche.Rows.Item(m).Cells(6).Style.Alignment = DataGridViewContentAlignment.MiddleRight

                GridPlanMarche.Rows.Item(m).Cells(7).Value = ""
                GridPlanMarche.Rows.Item(m).Cells(7).ReadOnly = True
                GridPlanMarche.Rows.Item(m).Cells(7).Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                GridPlanMarche.Rows.Item(m).Cells(8 + NbreColAjout).Value = "Réalisé"
                GridPlanMarche.Rows.Item(m).Cells(8 + NbreColAjout).ReadOnly = True
                GridPlanMarche.Rows.Item(m).Cells(8 + NbreColAjout).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                GridPlanMarche.Rows.Item(m).Cells(8 + NbreColAjout).Style.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)

                'For Ne As Integer = 1 To NbreColoEtape

                '    query = "select DebutEffectif,DebutPrevu,FinEffective,FinPrevue from T_PlanMarche where RefMarche='" & rw("RefMarche") & "' and NumeroOrdre='" & Ne & "'"
                '    dt0 = ExcecuteSelectQuery(query)
                '    For Each rw0 As DataRow In dt0.Rows
                '        Dim Result As String = "__/__/____"
                '        GridPlanMarche.Rows.Item(m).Cells(8 + Ne).Style.ForeColor = Color.DarkGray
                '        If (rw0(2).ToString <> "") Then
                '            Result = rw0(2).ToString
                '            GridPlanMarche.Rows.Item(m).Cells(8 + Ne).Style.ForeColor = Color.Black
                '        End If
                '        GridPlanMarche.Rows.Item(m).Cells(8 + Ne).Value = Result
                '        GridPlanMarche.Rows.Item(m).Cells(8 + Ne).ReadOnly = True
                '        GridPlanMarche.Rows.Item(m).Cells(8 + Ne).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                '        If IsDBNull(rw0(2)) Or IsDBNull(rw0(3)) Then
                '            GridPlanMarche.Rows.Item(m).Cells(8 + Ne).Style.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
                '        Else
                '            If (rw0(2) <> rw0(3) And Result <> "__/__/____") Then
                '                GridPlanMarche.Rows.Item(m).Cells(8 + Ne).Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)
                '            Else
                '                GridPlanMarche.Rows.Item(m).Cells(8 + Ne).Style.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
                '            End If
                '        End If
                '    Next

                'Next
                For Ne As Integer = 1 To NbreColoEtape
                    query = "select FinEffective from T_PlanMarche where RefMarche='" & rw("RefMarche") & "' and NumeroOrdre='" & Ne & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    For Each rw0 As DataRow In dt0.Rows
                        Dim Result1 As String = "__/__/____"
                        GridPlanMarche.Rows.Item(m).Cells(10 + NbreColAjout + Ne).Style.ForeColor = Color.DarkGray
                        If (rw0(0).ToString <> "") Then
                            Result1 = rw0(0).ToString
                            GridPlanMarche.Rows.Item(m).Cells(10 + NbreColAjout + Ne).Style.ForeColor = Color.Black
                        End If
                        GridPlanMarche.Rows.Item(m).Cells(10 + NbreColAjout + Ne).Value = Result1
                        GridPlanMarche.Rows.Item(m).Cells(10 + NbreColAjout + Ne).ReadOnly = True
                        GridPlanMarche.Rows.Item(m).Cells(10 + NbreColAjout + Ne).Style.Alignment = DataGridViewContentAlignment.BottomCenter
                    Next
                Next

                'la ligne separatrice *************************************************************************
                Dim k As Decimal = GridPlanMarche.Rows.Add()
                GridPlanMarche.Rows.Item(k).DefaultCellStyle.BackColor = CouleurSeparateur.Color
                GridPlanMarche.Rows.Item(k).ReadOnly = True
                GridPlanMarche.Rows.Item(k).Height = 10

            End If
        Next

        Dim p As Decimal = GridPlanMarche.Rows.Add()
        GridPlanMarche.Rows.Item(p).DefaultCellStyle.BackColor = CouleurTotaux.Color
        GridPlanMarche.Rows.Item(p).DefaultCellStyle.ForeColor = CouleurTexteTot.Color
        GridPlanMarche.Rows.Item(p).DefaultCellStyle.Font = New Font("Times New Roman", 10, FontStyle.Bold)

        GridPlanMarche.Rows.Item(p).Cells(2).Value = "TOTAUX"
        GridPlanMarche.Rows.Item(p).Cells(2).ReadOnly = True
        GridPlanMarche.Rows.Item(p).Cells(2).Style.Alignment = DataGridViewContentAlignment.BottomLeft

        Dim Absent As Integer = 0
        If (TypeM = "Consultants") Then Absent = 1

        GridPlanMarche.Rows.Item(p).Cells((5 + NbreColAjout) - Absent).Value = AfficherMonnaie((Math.Round(TotEstimDoll, 2)).ToString)
        GridPlanMarche.Rows.Item(p).Cells((5 + NbreColAjout) - Absent).ReadOnly = True
        GridPlanMarche.Rows.Item(p).Cells((5 + NbreColAjout) - Absent).Style.Alignment = DataGridViewContentAlignment.BottomRight

        GridPlanMarche.Rows.Item(p).Cells((6 + NbreColAjout) - Absent).Value = AfficherMonnaie((Math.Round(TotEstimFcfa, 2)).ToString)
        GridPlanMarche.Rows.Item(p).Cells((6 + NbreColAjout) - Absent).ReadOnly = True
        GridPlanMarche.Rows.Item(p).Cells((6 + NbreColAjout) - Absent).Style.Alignment = DataGridViewContentAlignment.BottomRight

        FinChargement()

        'End If


    End Sub

    Private Sub BtNouveauPlan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtNouveauPlan.Click
        NewPlan = True
        VoirPlan = False
        BtNouveauPlan.Enabled = False
        BtConsulterPlan.Enabled = False
        BtSupprimerPlan.Enabled = False
        btImprimerPlan.Enabled = False
        BtRetour.Enabled = True
        'BtImportPlan.Enabled = True
        If ModePPM = "Genere" Then
            btGenererPlan.Enabled = True
            Label1.Enabled = True
            Label2.Enabled = True
            Label3.Enabled = True
            Label5.Enabled = True
            cmbTypeMarche.Enabled = True
            DateDebutMarche.Enabled = True
            DateFinMarche.Enabled = True
            Label13.Enabled = True
            BailleurConcerne.Enabled = True
            txtNumPlan.Enabled = True
            Label6.Enabled = True
            CmbConvention.Enabled = True
        Else
            btImportPPSD.Enabled = True
            btSaisiePPM.Enabled = True
        End If


        Label4.Enabled = False
        MarcheAConsulter.Enabled = False
        LabelDevise.Enabled = False
        cmbDevise.Enabled = False
        cmbTypeMarche.Select()
    End Sub

    Private Sub BtConsulterPlan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtConsulterPlan.Click
        NewPlan = False
        VoirPlan = True
        BtNouveauPlan.Enabled = False
        BtConsulterPlan.Enabled = False
        BtRetour.Enabled = True
        btGenererPlan.Enabled = False
        'BtImportPlan.Enabled = False

        Label1.Enabled = False
        Label2.Enabled = False
        Label3.Enabled = False
        cmbTypeMarche.Enabled = False
        DateDebutMarche.Enabled = False
        DateFinMarche.Enabled = False
        Label13.Enabled = False
        Label5.Enabled = False
        BailleurConcerne.Enabled = False
        txtNumPlan.Enabled = False
        Label6.Enabled = False

        Label4.Enabled = True
        MarcheAConsulter.Enabled = True
        LabelDevise.Enabled = True
        cmbDevise.Enabled = True
        MarcheAConsulter.Select()
    End Sub

    Public Sub RemplirMarcheAConsulter()
        If ModePPM = "PPSD" Then
            query = "SELECT * FROM t_ppm_marche WHERE CodeProjet='" & ProjetEnCours & "' AND ModePlanMarche='PPSD' ORDER BY LibellePPM"
        Else
            If ElaboPPM = "Tous les bailleurs" Then
                query = "SELECT * FROM t_ppm_marche WHERE CodeProjet='" & ProjetEnCours & "' AND ModePlanMarche='Genere' AND ElaboPPM='Tous les bailleurs' ORDER BY LibellePPM"
            Else
                query = "SELECT * FROM t_ppm_marche WHERE CodeProjet='" & ProjetEnCours & "' AND ModePlanMarche='Genere' AND ElaboPPM='Bailleur'  ORDER BY LibellePPM"
            End If
        End If
        Dim dtPPM As DataTable = ExcecuteSelectQuery(query)
        MarcheAConsulter.Properties.Items.Clear()
        If dtPPM.Rows.Count > 0 Then
            ReDim RefPPM(dtPPM.Rows.Count)
        End If
        Dim i As Integer = 0
        For Each rwPPM As DataRow In dtPPM.Rows
            MarcheAConsulter.Properties.Items.Add(MettreApost(rwPPM("LibellePPM")))
            RefPPM(i) = rwPPM("RefPPM")
            i += 1
        Next

        'Dim ListeMarche(100) As String
        'Dim NbreMarche As Decimal = 0
        'Dim Tampon As String = ""

        'query = "select TypeMarche, PeriodeMarche, InitialeBailleur, CodeConvention from T_Marche where CodeProjet='" & ProjetEnCours & "'"
        'Dim dt As DataTable = ExcecuteSelectQuery(query)
        'For Each rw As DataRow In dt.Rows
        '    Tampon = rw(0).ToString & "_" & rw(1).ToString & "_" & rw(2).ToString & "_" & rw(3).ToString
        '    Dim Existe As Boolean = False
        '    For i As Integer = 0 To NbreMarche - 1
        '        If (ListeMarche(i) = Tampon) Then
        '            Existe = True
        '        End If
        '    Next

        '    If (Existe = False) Then
        '        ListeMarche(NbreMarche) = Tampon
        '        NbreMarche = NbreMarche + 1
        '    End If
        'Next
        'MarcheAConsulter.Properties.Items.Clear()
        'For j As Integer = 0 To NbreMarche - 1
        '    MarcheAConsulter.Properties.Items.Add(ListeMarche(j))
        '    ListeMarche(j) = 0
        'Next

        'NbreMarche = 0

    End Sub

    Private Sub MarcheAConsulter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MarcheAConsulter.SelectedIndexChanged

        If MarcheAConsulter.SelectedIndex > -1 Then
            BtActualiserPlan.Enabled = True
            btAjout.Enabled = True
            If cmbDevise.SelectedIndex = -1 Then
                SuccesMsg("Veuillez choisir la deuxième devise")
                MarcheAConsulter.Text = ""
                Exit Sub
            End If
            If ModePPM = "Genere" Then
                Dim Criteres() As String = (MarcheAConsulter.Text).Split("_"c)
                cmbTypeMarche.Text = Criteres(0)
                DateDebutMarche.Text = Split(Criteres(1), " - ")(0)
                DateFinMarche.Text = Split(Criteres(1), " - ")(1)
                BailleurConcerne.Text = Criteres(2)
                If BailleurConcerne.Text = "Tous" Then
                    CmbConvention.Text = ""
                Else
                    CmbConvention.Text = Criteres(3)
                End If
            Else
            End If
            CurrentRefPPM = RefPPM(MarcheAConsulter.SelectedIndex)
            txtNumPlan.Text = ExecuteScallar("SELECT NumeroPlan FROM t_ppm_marche WHERE RefPPM='" & CurrentRefPPM & "'")
        Else
            CurrentRefPPM = -1
            BtActualiserPlan.Enabled = False
            btAjout.Enabled = False
        End If
        RemplirTableauPPM(CurrentRefPPM)
    End Sub

    Private Sub RaserFenetre()
        BtNouveauPlan.Enabled = True
        BtConsulterPlan.Enabled = True
        BtRetour.Enabled = False
        btGenererPlan.Enabled = False
        'BtImportPlan.Enabled = False
        NewPlan = False
        VoirPlan = False
        If ModePPM = "Genere" Then
            cmbTypeMarche.Text = ""
            cmbTypeMarche.Enabled = False
            DateDebutMarche.Text = ""
            DateDebutMarche.Enabled = False
            DateFinMarche.Text = ""
            DateFinMarche.Enabled = False
            RemplirBailleur()
            BailleurConcerne.Text = ""
            BailleurConcerne.Enabled = False
            txtNumPlan.Text = ""
            txtNumPlan.Enabled = False
            CmbConvention.Text = ""
            CmbConvention.Enabled = False
            Label1.Enabled = False
            Label2.Enabled = False
            Label3.Enabled = False
            Label13.Enabled = False
            Label6.Enabled = False
            Label5.Enabled = False
        Else
            btSaisiePPM.Enabled = False
            btImportPPSD.Enabled = False
        End If
        RemplirMarcheAConsulter()
        MarcheAConsulter.Text = ""
        MarcheAConsulter.Enabled = False
        Label4.Enabled = False
        LabelDevise.Enabled = False
        cmbDevise.Enabled = False
        GridPlanMarche.Rows.Clear()
        Dim NbCol As Decimal = GridPlanMarche.ColumnCount
        If (NbCol > 0) Then
            For i As Integer = 1 To NbCol - 1
                GridPlanMarche.Columns.Remove("A")
            Next
        End If
    End Sub

    Private Sub RemplirTypeMarche()
        query = "select TypeMarche from T_TypeMarche order by TypeMarche"
        cmbTypeMarche.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cmbTypeMarche.Properties.Items.Add(MettreApost(rw("TypeMarche").ToString))
        Next
    End Sub

    Private Sub CouleurPlan_ColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CouleurPlan.ColorChanged
        If (GridPlanMarche.ColumnCount > 1) Then
            RemplirTableauPPM(CurrentRefPPM)
        End If
    End Sub

    Private Sub CouleurRealise_ColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CouleurRealise.ColorChanged
        If (GridPlanMarche.ColumnCount > 1) Then
            RemplirTableauPPM(CurrentRefPPM)
        End If
    End Sub

    Private Sub CouleurSeparateur_ColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CouleurSeparateur.ColorChanged
        If (GridPlanMarche.ColumnCount > 1) Then
            RemplirTableauPPM(CurrentRefPPM)
        End If
    End Sub

    Private Sub CouleurTexte_ColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CouleurTexte.ColorChanged
        If (GridPlanMarche.ColumnCount > 1) Then
            RemplirTableauPPM(CurrentRefPPM)
        End If
    End Sub

    Private Sub CouleurTexte2_ColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CouleurTexte2.ColorChanged
        If (GridPlanMarche.ColumnCount > 1) Then
            RemplirTableauPPM(CurrentRefPPM)
        End If
    End Sub

    Private Sub GridPlanMarche_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridPlanMarche.CellDoubleClick
        If (VoirPlan = True) Then
            Dim NewEtapeMarche As New PlanMarcheSuite
            If BailleurConcerne.Enabled Then
                NewEtapeMarche.Bailleur = BailleurConcerne.Text
                NewEtapeMarche.CodeConvention = CmbConvention.Text
            Else
                Dim Criteres() As String = (MarcheAConsulter.Text).Split("_"c)
                NewEtapeMarche.RefPPM = CurrentRefPPM
                If ModePPM = "Genere" Then
                    NewEtapeMarche.Bailleur = Criteres(2)
                    If NewEtapeMarche.Bailleur = "Tous" Then
                        NewEtapeMarche.CodeConvention = ""
                    Else
                        NewEtapeMarche.CodeConvention = Criteres(3)
                    End If
                End If
            End If
            NewEtapeMarche.ShowDialog()
        End If

        Exit Sub
        If (VoirPlan = True) Then

            Dim DatSet As New DataSet
            Dim DatAdapt As MySqlDataAdapter
            Dim DatTable As DataTable
            Dim DatRow As DataRow
            Dim CmdBuilder As MySqlCommandBuilder

            Dim AutoActive As Boolean = False
            query = "select MethodeMarcheAuto from T_ParamTechProjet where CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                If (rw(0).ToString = "OUI") Then
                    AutoActive = True
                End If
            Next

            Dim NumLigNe As Integer = GridPlanMarche.CurrentCell.RowIndex
            Dim NumColonNe As Integer = GridPlanMarche.CurrentCell.ColumnIndex

            Dim Prop() As String = (MarcheAConsulter.Text).Split("_"c)
            Dim TypeM As String = Prop(0)
            Dim CodeMarche As Decimal = 0
            Dim TypeLigne As String = ""

            'A revoir -- lorsqu'on click sur la ligne du plan0
            Try
                If ((GridPlanMarche.Rows.Item(NumLigNe).Cells(0).Value).ToString <> "") Then
                    CodeMarche = CInt(Mid(GridPlanMarche.Rows.Item(NumLigNe).Cells(0).Value, 2))
                    TypeLigne = Mid(GridPlanMarche.Rows.Item(NumLigNe).Cells(0).Value, 1, 1)
                End If
            Catch ex As Exception
                Exit Sub
            End Try

            If (CodeMarche <> 0 And (TypeM = "Fournitures" Or TypeM = "Travaux") And (NumColonNe = 3 Or NumColonNe = 4) And TypeLigne = "P") Then
                SuccesMsg("Insérez ce marché dans un appel d'offres pour renseigner ce champ.")

            ElseIf (CodeMarche <> 0 And NumColonNe = 2 And TypeLigne = "P") Then
                ReponseDialog = GridPlanMarche.Rows.Item(NumLigNe).Cells(NumColonNe).Value.ToString
                ReponseDialog = InputBox("Nouvelle description", "Description", ReponseDialog)
                If (ReponseDialog <> "") Then
                    DatSet = New DataSet
                    query = "select * from T_Marche where RefMarche='" & CodeMarche & "'"
                    Dim sqlconn As New MySqlConnection
                    BDOPEN(sqlconn)
                    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                    DatAdapt = New MySqlDataAdapter(Cmd)
                    CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                    DatAdapt.Fill(DatSet, "T_Marche")

                    DatSet.Tables!T_Marche.Rows(0)!DescriptionMarche = ReponseDialog
                    DatAdapt.Update(DatSet, "T_Marche")

                    DatSet.Clear()
                    BDQUIT(sqlconn)
                    Dim Marche As String = MarcheAConsulter.Text
                    RemplirMarcheAConsulter()
                    MarcheAConsulter.Text = Marche

                    'RemplirTableauPPM(CurrentRefPPM)
                End If


            ElseIf (CodeMarche <> 0 And (((TypeM <> "Consultants") And NumColonNe >= 11) Or (TypeM = "Consultants" And NumColonNe >= 9)) And TypeLigne = "R") Then
                ' Calcul normal des dates des étapes *************************

            ElseIf (CodeMarche <> 0 And (((TypeM = "Fournitures" Or TypeM = "Travaux") And NumColonNe = 11) Or (TypeM = "Consultants" And NumColonNe = 9)) And TypeLigne = "P") Then
                ProgEtape.DateDebutEtape.Enabled = True

                Dim ChampDaoExist As Boolean = False
                Dim NumEtapeChamp As Decimal = 0

                If (TypeM = "Fournitures" Or TypeM = "Travaux") Then
                    If (GridPlanMarche.Rows.Item(NumLigNe).Cells(7).Value.ToString = "") Then
                        MsgBox("Définissez d'abord la méthode de passation de marchés!", MsgBoxStyle.Information)
                        GridPlanMarche.Rows.Item(NumLigNe).Cells(7).Style.BackColor = Color.DarkRed
                        GridPlanMarche.Rows.Item(NumLigNe + 1).Cells(7).Style.BackColor = Color.DarkRed
                        Exit Sub
                    End If
                ElseIf (TypeM = "Consultants") Then
                    If (GridPlanMarche.Rows.Item(NumLigNe).Cells(6).Value.ToString = "") Then
                        MsgBox("Définissez d'abord la méthode de passation de marchés!", MsgBoxStyle.Information)
                        GridPlanMarche.Rows.Item(NumLigNe).Cells(6).Style.BackColor = Color.DarkRed
                        GridPlanMarche.Rows.Item(NumLigNe + 1).Cells(6).Style.BackColor = Color.DarkRed
                        Exit Sub
                    End If
                End If

                Dim KodeProc As Decimal = 0
                query = "select CodeProcAO from T_Marche where RefMarche='" & Mid(GridPlanMarche.Rows.Item(NumLigNe).Cells(0).Value.ToString, 2) & "'"
                dt = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    KodeProc = rw(0)
                Next

                ' Verifier s'il n'y a pas d'étape (sans delai) liée au DAO du marché *************************************
                For i1 As Decimal = 1 To NbreColoEtape
                    Dim CoDetp1 As Decimal = 0
                    query = "select RefEtape from T_EtapeMarche where CodeProjet='" & ProjetEnCours & "'and TypeMarche='" & TypeM & "' and NumeroOrdre='" & i1 & "'"
                    dt = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        CoDetp1 = rw(0)
                    Next

                    query = "select DelaiEtape from T_DelaiEtape where CodeProcAO='" & KodeProc & "'and RefEtape='" & CoDetp1 & "' and DelaiEtape='DE-DAO'"
                    dt = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        ChampDaoExist = True
                        NumEtapeChamp = i1
                    Next

                Next

                '********************************************************************************************************

                Dim MarchePrio As Boolean = True

                If (TypeM = "Consultants" And GridPlanMarche.Rows.Item(NumLigNe).Cells(7).Value.ToString.Length > 6) Then
                    MarchePrio = False
                ElseIf ((TypeM = "Fournitures" Or TypeM = "Travaux") And GridPlanMarche.Rows.Item(NumLigNe).Cells(9).Value.ToString.Length > 6) Then
                    MarchePrio = False
                End If


                Dim ExceptExist As Boolean = False
                Dim NbreMarc As Decimal = 0
                Dim NMarcExist As Decimal = 0
                query = "select NbreMarche from T_NombreMarche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='" & TypeM & "' and CodeProcAO='" & KodeProc & "'"
                dt = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    NMarcExist = CInt(rw(0))
                Next

                If (MarchePrio = False) Then

                    query = "select S.ExceptionRevue from T_Seuil as S,T_Marche as M where M.CodeProjet='" & ProjetEnCours & "' and S.CodeSeuil=M.CodeSeuil and S.ExceptionRevue<>'' and M.RefMarche='" & Mid(GridPlanMarche.Rows.Item(NumLigNe).Cells(0).Value.ToString, 2) & "'"
                    dt = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        ExceptExist = True
                        Dim PartExcep() As String = rw(0).ToString.Split(" "c)
                        NbreMarc = CInt(PartExcep(0))
                    Next


                    MsgBox("CodeProc=" & KodeProc.ToString & " Nbre marché except=" & NbreMarc.ToString & " Nb marche exist=" & NMarcExist.ToString, MsgBoxStyle.Information)
                    If (NbreMarc > 0) Then

                        If (NbreMarc > NMarcExist And ExceptExist = True) Then
                            MarchePrio = True
                            MsgBox("Ce marché sera soumis à un examen à priorie", MsgBoxStyle.Information)

                            query = "update T_Marche set RevuePrioPost='Priori' where RefMarche='" & Mid(GridPlanMarche.Rows.Item(NumLigNe).Cells(0).Value.ToString, 2) & "'"
                            ExecuteNonQuery(query)

                        End If
                    End If
                End If

                'Maj du nombre de marchés *********************************

                ' Maintenant si on est dans un marché à posteriori, on ecrase toutes les étapes qui concernent les marchés à priori
                If (MarchePrio = False) Then
                    query = "select RefEtape from T_EtapeMarche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='" & TypeM & "' and Posteriori<>'OUI'"
                    dt = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        Dim KodAsupp As Decimal = rw(0)
                        query = "DELETE from T_PlanMarche where RefMarche='" & Mid(GridPlanMarche.Rows.Item(NumLigNe).Cells(0).Value.ToString, 2) & "' and RefEtape='" & KodAsupp & "'"
                        ExecuteNonQuery(query)
                    Next

                End If

                Dim PremiereEtape As Boolean = False
                For cp As Decimal = 1 To NbreColoEtape
                    DureeEtpPlan = ""
                    TitreEtpPlan = ""
                    Dim CodePlan As Decimal = 0
                    Dim KodeMarche As Decimal = 0
                    Dim KodEtape As Decimal = 0
                    Dim msgText As String = GridPlanMarche.Rows.Item(NumLigNe).Cells(2).Value.ToString

                    query = "select P.RefPPM,P.RefMarche,P.RefEtape,P.NumeroOrdre,E.RefEtape,E.CodeProjet,E.TypeMarche,E.TitreEtape from T_PlanMarche as P,T_EtapeMarche as E where P.RefMarche='" & CodeMarche & "' and E.CodeProjet='" & ProjetEnCours & "' and E.TypeMarche='" & TypeM & "' and P.NumeroOrdre='" & cp & "' and P.RefEtape=E.RefEtape"
                    dt = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        CodePlan = rw(0)
                        KodeMarche = rw(1)
                        KodEtape = rw(2)
                        TitreEtpPlan = MettreApost(rw(7))
                    Next

                    query = "select DelaiEtape from T_DelaiEtape where RefEtape='" & KodEtape & "' and CodeProcAO='" & KodeProc & "'"
                    dt = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        DureeEtpPlan = rw(0)
                    Next

                    If (ChampDaoExist = True And cp = NumEtapeChamp) Then

                        ' Code de recherche de la date qui est dans le DAO *****************************************
                        If (TypeM = "Travaux" Or TypeM = "Fournitures") Then
                            If (GridPlanMarche.Rows.Item(NumLigNe).Cells(3).Value.ToString = "") Then
                                DureeEtpPlan = ""
                            Else
                                Dim DaoConcerne As String = GridPlanMarche.Rows.Item(NumLigNe).Cells(3).Value.ToString
                                query = "select DelaiExecution from T_DAO where NumeroDAO='" & DaoConcerne & "' and CodeProjet='" & ProjetEnCours & "'"
                                dt = ExcecuteSelectQuery(query)
                                For Each rw As DataRow In dt.Rows
                                    DureeEtpPlan = rw(0)
                                Next
                            End If
                        ElseIf (TypeM = "Consultants") Then

                            Dim DpConcerne As String = ""
                            query = "select NumeroDAO from T_Marche where RefMarche='" & CodeMarche & "' and CodeProjet='" & ProjetEnCours & "'"
                            dt = ExcecuteSelectQuery(query)
                            For Each rw As DataRow In dt.Rows
                                DpConcerne = rw(0)
                            Next

                            DureeEtpPlan = ""
                            query = "select DureeTaf from T_DP where NumeroDP='" & DpConcerne & "' and CodeProjet='" & ProjetEnCours & "'"
                            dt = ExcecuteSelectQuery(query)
                            For Each rw As DataRow In dt.Rows
                                DureeEtpPlan = rw(0)
                            Next
                        End If

                        '********************************************************************************************
                    End If


                    If (cp > 1) Then
                        ProgEtape.DateDebutEtape.Enabled = False
                    End If

                    If DureeEtpPlan.ToString = "" Then
                    ElseIf DureeEtpPlan.ToString <> "" Then
                        ProgEtape.LabelNumeroEtape.Text = "Etape N°" & cp.ToString
                        ProgEtape.TitreEtape.Text = "Cette étape n'est pas prise en compte dans ce marché." & vbNewLine & "Cliquez sur [OK] pour passer."
                        ProgEtape.ShowDialog()
                    End If

                    If (DureeEtpPlan <> "" And DateEtpPlan <> "") Then

                        Dim PartieDuree() As String = DureeEtpPlan.Split(" "c)
                        Dim NumDuree As Decimal = CInt(PartieDuree(0))
                        Dim UniteDuree As String = PartieDuree(1)
                        Dim LaDateDeb As Date = CDate(DateEtpPlan)

                        Dim LaDateFin As Date

                        If (UniteDuree = "Semaines") Then
                            NumDuree = NumDuree * 7
                            LaDateFin = LaDateDeb.AddDays(NumDuree)
                        ElseIf (UniteDuree = "Mois") Then
                            LaDateFin = LaDateDeb.AddMonths(NumDuree)
                        Else
                            LaDateFin = LaDateDeb.AddDays(NumDuree)
                        End If


                        Try
                            DatSet = New DataSet
                            query = "select * from T_PlanMarche where RefPPM='" & CodePlan & "'"
                            Dim sqlconn As New MySqlConnection
                            BDOPEN(sqlconn)
                            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                            DatAdapt = New MySqlDataAdapter(Cmd)
                            CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                            DatAdapt.Fill(DatSet, "T_PlanMarche")

                            DatSet.Tables!T_PlanMarche.Rows(0)!DebutPrevu = LaDateDeb.ToShortDateString
                            If (cp = 1 Or PremiereEtape = False) Then
                                DatSet.Tables!T_PlanMarche.Rows(0)!DebutEffectif = LaDateDeb.ToShortDateString
                            End If

                            DatSet.Tables!T_PlanMarche.Rows(0)!FinPrevue = LaDateFin.ToShortDateString
                            DatSet.Tables!T_PlanMarche.Rows(0)!CodeOperateur = RespoEtape
                            DatAdapt.Update(DatSet, "T_PlanMarche")

                            DatSet.Clear()

                            'Envoi du premier message **************************************
                            If (cp = 1 Or PremiereEtape = False) Then
                                DebutChargement(True, "Envoi du message en cours ...")

                                If (Directory.Exists(line & "\Courrier") = False) Then
                                    Directory.CreateDirectory(line & "\Courrier")
                                End If

                                Dim DateCourr As String = Now.ToShortDateString & " " & Now.ToLongTimeString
                                Dim nomFile As String = CodeOperateurEnCours & "_" & DateCourr.Replace(" ", "").Replace("/", "").Replace(":", "")
                                If (Directory.Exists(line & "\Courrier\" & nomFile) = False) Then
                                    Directory.CreateDirectory(line & "\Courrier\" & nomFile)
                                End If

                                msgText = "Libellé du Marché : " & msgText & vbNewLine & "Type de Marché : " & TypeM & vbNewLine
                                msgText = msgText & "Financement : Part " & BailleurConcerne.Text & vbNewLine
                                msgText = msgText & "Financement n° " & CmbConvention.Text & vbNewLine
                                msgText = msgText & "Période couverte : du " & DateDebutMarche.Text & " au " & DateFinMarche.Text

                                Dim WD = CreateObject("Word.Application")
                                WD.Documents.Add()
                                WD.Visible = False
                                WD.Documents(1).Range.InsertAfter(msgText)
                                WD.Documents(1).SaveAs(line & "\Courrier\" & nomFile & "\Message.docx")


                                DatSet = New DataSet
                                query = "select * from T_Courrier"

                                Cmd = New MySqlCommand(query, sqlconn)
                                DatAdapt = New MySqlDataAdapter(Cmd)
                                DatAdapt.Fill(DatSet, "T_Courrier")
                                DatTable = DatSet.Tables("T_Courrier")
                                DatRow = DatSet.Tables("T_Courrier").NewRow()

                                DatRow("DateCourrier") = DateCourr
                                DatRow("ProjetExp") = "N"
                                DatRow("Destinataire") = RespoEtape
                                DatRow("DestinExterieur") = ""
                                DatRow("TypeCourrier") = 0
                                DatRow("CodeMarche") = Mid(GridPlanMarche.Rows.Item(NumLigNe).Cells(0).Value.ToString, 2)
                                DatRow("Objet") = "Exécution Plan de Passation de Marché"
                                DatRow("ExtraitMessage") = Mid(EnleverApost(msgText), 1, 200)
                                DatRow("CheminFichier") = nomFile & "\Message.docx"
                                DatRow("Priorite") = "Normale"
                                DatRow("Suivi") = "N"
                                DatRow("DateLecture") = ""
                                DatRow("Rayon") = ""
                                DatRow("CodeOperateur") = CodeOperateurEnCours
                                DatRow("DateEnvoi") = DateCourr
                                DatRow("CodeProjet") = ProjetEnCours

                                DatSet.Tables("T_Courrier").Rows.Add(DatRow)
                                CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                                DatAdapt.Update(DatSet, "T_Courrier")

                                DatSet.Clear()

                                If (Proj_mailHote <> "" And Proj_mailCompte <> "") Then
                                    EnvoiMail(RespoEtape, "Bonjour," & vbNewLine & "Vous avez reçu un dossier à traiter dans le système de gestion du " & ProjetEnCours & "." & vbNewLine & "Veuillez vous connecter à votre session pour plus d'informations." & vbNewLine & "Cordialement ClearProject.", "")
                                End If

                                If (Proj_smsTerminal <> "") Then
                                    EnvoiSms(RespoEtape, "Bonjour," & vbNewLine & "Vous avez reçu un dossier à traiter dans ClearProject." & vbNewLine & "Veuillez vous connecter à votre session pour plus d'informations." & vbNewLine & ProjetEnCours)
                                End If

                                FinChargement()
                            End If
                            '**************************************************************
                            PremiereEtape = True
                            BDQUIT(sqlconn)
                        Catch ex As Exception
                            MsgBox(ex.ToString, MsgBoxStyle.Critical)
                        End Try
                        DateEtpPlan = (LaDateFin.ToShortDateString).ToString
                    End If
                Next

                'Ici c'est la verification de la date fin prevue de notification pour decaler les activites concernees !!!!!!!!!!
                Dim dateDecal As String = ""
                query = "select P.FinPrevue from T_PlanMarche as P, T_EtapeMarche as E where P.RefMarche='" & Mid(GridPlanMarche.Rows.Item(NumLigNe).Cells(0).Value.ToString, 2) & "' and P.FinPrevue<>'' and P.RefEtape=E.RefEtape and E.NotifDemarr='OUI'"
                dt = ExcecuteSelectQuery(query)
                If dt.Rows.Count > 0 Then
                    dateDecal = dt.Rows(0).Item(0).ToString
                End If

                If (dateDecal <> "") Then
                    DecalerActivites(Mid(GridPlanMarche.Rows.Item(NumLigNe).Cells(0).Value.ToString, 2), CDate(dateDecal))
                Else
                    MsgBox("Aucune étape de Démarrage de services trouvée pour ce marché!", MsgBoxStyle.Exclamation)
                End If

                '****************************************************************************************************************
                'RemplirTableauPPM(CurrentRefPPM)

                ' Calcul à l'envers des dates des étapes *************************
            ElseIf (CodeMarche <> 0 And (((TypeM = "Fournitures" Or TypeM = "Travaux") And NumColonNe = GridPlanMarche.ColumnCount - 1) Or (TypeM = "Consultants" And NumColonNe = GridPlanMarche.ColumnCount - 1)) And TypeLigne = "P") Then
                ProgEtape.DateDebutEtape.Enabled = True

                If (TypeM = "Fournitures" Or TypeM = "Travaux") Then
                    If (GridPlanMarche.Rows.Item(NumLigNe).Cells(7).Value.ToString = "") Then
                        MsgBox("Définissez d'abord la méthode de passation de marchés!", MsgBoxStyle.Information)
                        GridPlanMarche.Rows.Item(NumLigNe).Cells(7).Style.BackColor = Color.DarkRed
                        GridPlanMarche.Rows.Item(NumLigNe + 1).Cells(7).Style.BackColor = Color.DarkRed
                        Exit Sub
                    End If
                ElseIf (TypeM = "Consultants") Then
                    If (GridPlanMarche.Rows.Item(NumLigNe).Cells(6).Value.ToString = "") Then
                        MsgBox("Définissez d'abord la méthode de passation de marchés!", MsgBoxStyle.Information)
                        GridPlanMarche.Rows.Item(NumLigNe).Cells(6).Style.BackColor = Color.DarkRed
                        GridPlanMarche.Rows.Item(NumLigNe + 1).Cells(6).Style.BackColor = Color.DarkRed
                        Exit Sub
                    End If
                End If

                Dim KodeProc As Decimal = 0
                query = "select CodeProcAO from T_Marche where RefMarche='" & Mid(GridPlanMarche.Rows.Item(NumLigNe).Cells(0).Value.ToString, 2) & "'"
                dt = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows
                    KodeProc = rw(0)
                Next

                Dim MarchePrio As Boolean = True
                If (TypeM = "Consultants" And GridPlanMarche.Rows.Item(NumLigNe).Cells(7).Value.ToString.Length > 6) Then
                    MarchePrio = False
                ElseIf ((TypeM = "Fournitures" Or TypeM = "Travaux") And GridPlanMarche.Rows.Item(NumLigNe).Cells(9).Value.ToString.Length > 6) Then
                    MarchePrio = False
                End If

                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                If (MarchePrio = False) Then
                    ' Pour les methodes avec exceptions 
                    '  1  recuperation des infos du plan
                    Dim ExceptExist As Boolean = False
                    Dim NbreMarc As Decimal = 0
                    Dim NMarcExist As Decimal = 0
                    query = "select S.ExceptionRevue from T_Seuil as S,T_Marche as M where M.CodeProjet='" & ProjetEnCours & "' and S.CodeSeuil=M.CodeSeuil and S.ExceptionRevue<>''"
                    dt = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        Dim PartExcep() As String = rw(0).ToString.Split(" "c)
                        NbreMarc = CInt(PartExcep(0))
                        ' cherchons si le nombre de marché est atteint
                        query = "select NbreMarche from T_NombreMarche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='" & TypeM & "' and CodeProcAO='" & KodeProc & "'"
                        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw0 As DataRow In dt0.Rows
                            NMarcExist = CInt(rw0(0))
                        Next
                    Next
                    If (NbreMarc > 0) Then
                        DatSet = New DataSet
                        query = "select * from T_NombreMarche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='" & TypeM & "' and CodeProcAO='" & KodeProc & "'"
                        Dim Cmd = New MySqlCommand(query, sqlconn)
                        DatAdapt = New MySqlDataAdapter(Cmd)
                        CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                        DatAdapt.Fill(DatSet, "T_NombreMarche")
                        DatSet.Tables!T_NombreMarche.Rows(0)!NbreMarche = (NMarcExist + 1)
                        DatAdapt.Update(DatSet, "T_NombreMarche")

                        DatSet.Clear()
                        BDQUIT(sqlconn)

                        If (NbreMarc > NMarcExist) Then
                            MarchePrio = True
                            MsgBox("Ce marché sera soumis à un examen à priorie", MsgBoxStyle.Information)
                        End If
                    End If
                End If

                ' Maintenant si on est dans un marché à posteriori, on ecrase toutes les étapes qui concernent les marchés à priori
                If (MarchePrio = False) Then
                    query = "select RefEtape from T_EtapeMarche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='" & TypeM & "' and Posteriori<>'OUI'"
                    dt = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        Dim KodAsupp As Decimal = rw(0)
                        query = "DELETE from T_PlanMarche where RefMarche='" & Mid(GridPlanMarche.Rows.Item(NumLigNe).Cells(0).Value.ToString, 2) & "' and RefEtape='" & KodAsupp & "'"
                        ExecuteNonQuery(query)
                    Next

                End If

                Dim PlanDebut As Decimal = 1000
                Dim RespoDebut As Decimal = 0
                Dim xDateDeb As Date
                For cp As Decimal = 1 To NbreColoEtape
                    Dim NumEtape As Decimal = (NbreColoEtape + 1) - cp
                    DureeEtpPlan = ""
                    TitreEtpPlan = ""
                    Dim CodePlan As Decimal = 0
                    Dim KodeMarche As Decimal = 0
                    Dim KodEtape As Decimal = 0
                    query = "select P.RefPPM,P.RefMarche,P.RefEtape,P.NumeroOrdre,E.RefEtape,E.CodeProjet,E.TypeMarche,E.TitreEtape from T_PlanMarche as P,T_EtapeMarche as E where P.RefMarche='" & CodeMarche & "' and E.CodeProjet='" & ProjetEnCours & "' and E.TypeMarche='" & TypeM & "' and P.NumeroOrdre='" & NumEtape & "' and P.RefEtape=E.RefEtape"
                    dt = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        CodePlan = rw(0)
                        KodeMarche = rw(1)
                        KodEtape = rw(2)
                        TitreEtpPlan = MettreApost(rw(7))
                    Next

                    query = "select DelaiEtape from T_DelaiEtape where RefEtape='" & KodEtape & "' and CodeProcAO='" & KodeProc & "'"
                    dt = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        DureeEtpPlan = rw(0)
                    Next
                    If (NumEtape < NbreColoEtape) Then
                        ProgEtape.DateDebutEtape.Enabled = False
                    End If
                    ProgEtape.ShowDialog()

                    If (DureeEtpPlan <> "" And DateEtpPlan <> "") Then
                        Dim PartieDuree() As String = DureeEtpPlan.Split(" "c)
                        Dim NumDuree As Decimal = CInt(PartieDuree(0))
                        Dim UniteDuree As String = PartieDuree(1)
                        Dim LaDateFin As Date = CDate(DateEtpPlan)
                        Dim LaDateDeb As Date

                        If (UniteDuree = "Semaines") Then
                            NumDuree = NumDuree * 7
                            LaDateDeb = LaDateFin.AddDays(-NumDuree)
                        ElseIf (UniteDuree = "Mois") Then
                            LaDateDeb = LaDateFin.AddMonths(-NumDuree)
                        Else
                            LaDateDeb = LaDateFin.AddDays(-NumDuree)
                        End If

                        If (CodePlan < PlanDebut) Then
                            PlanDebut = CodePlan
                            RespoDebut = RespoEtape
                            xDateDeb = LaDateDeb
                        End If

                        Try
                            DatSet = New DataSet
                            query = "select * from T_PlanMarche where RefPPM='" & CodePlan & "'"
                            Dim Cmd = New MySqlCommand(query, sqlconn)
                            DatAdapt = New MySqlDataAdapter(Cmd)
                            CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                            DatAdapt.Fill(DatSet, "T_PlanMarche")
                            DatSet.Tables!T_PlanMarche.Rows(0)!DebutPrevu = LaDateDeb.ToShortDateString
                            DatSet.Tables!T_PlanMarche.Rows(0)!FinPrevue = LaDateFin.ToShortDateString
                            DatSet.Tables!T_PlanMarche.Rows(0)!CodeOperateur = RespoEtape
                            DatAdapt.Update(DatSet, "T_PlanMarche")
                            BDQUIT(sqlconn)
                            DatSet.Clear()
                        Catch ex As Exception
                            MsgBox(ex.ToString, MsgBoxStyle.Critical)
                        End Try
                        DateEtpPlan = (LaDateDeb.ToShortDateString).ToString
                    End If

                Next

                ' Correction premiere date effective et message *************************************
                DebutChargement(True, "Envoi du message en cours ...")
                Dim msgText As String = GridPlanMarche.Rows.Item(NumLigNe).Cells(2).Value.ToString
                Try
                    DatSet = New DataSet
                    query = "select * from T_PlanMarche where RefPPM='" & PlanDebut & "'"
                    Dim Cmd = New MySqlCommand(query, sqlconn)
                    DatAdapt = New MySqlDataAdapter(Cmd)
                    CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                    DatAdapt.Fill(DatSet, "T_PlanMarche")
                    DatSet.Tables!T_PlanMarche.Rows(0)!DebutEffectif = xDateDeb.ToShortDateString
                    DatAdapt.Update(DatSet, "T_PlanMarche")
                    BDQUIT(sqlconn)
                    DatSet.Clear()
                Catch ex As Exception

                End Try

                If (Directory.Exists(line & "\Courrier") = False) Then
                    Directory.CreateDirectory(line & "\Courrier")
                End If

                Dim DateCourr As String = Now.ToShortDateString & " " & Now.ToLongTimeString
                Dim nomFile As String = CodeOperateurEnCours & "_" & DateCourr.Replace(" ", "").Replace("/", "").Replace(":", "")
                If (Directory.Exists(line & "\Courrier\" & nomFile) = False) Then
                    Directory.CreateDirectory(line & "\Courrier\" & nomFile)
                End If

                msgText = "Libellé du Marché : " & msgText & vbNewLine & "Type de Marché : " & TypeM & vbNewLine
                msgText = msgText & "Financement : Part " & BailleurConcerne.Text & vbNewLine
                msgText = msgText & "Financement n° " & CmbConvention.Text & vbNewLine
                msgText = msgText & "Période couverte : du " & DateDebutMarche.Text & " au " & DateFinMarche.Text

                Dim WD = CreateObject("Word.Application")
                WD.Documents.Add()
                WD.Visible = False
                WD.Documents(1).Range.InsertAfter(msgText)
                WD.Documents(1).SaveAs(line & "\Courrier\" & nomFile & "\Message.docx")


                DatSet = New DataSet
                query = "select * from T_Courrier"
                Dim Cmd1 = New MySqlCommand(query, sqlconn)
                DatAdapt = New MySqlDataAdapter(Cmd1)
                DatAdapt.Fill(DatSet, "T_Courrier")
                DatTable = DatSet.Tables("T_Courrier")
                DatRow = DatSet.Tables("T_Courrier").NewRow()

                DatRow("DateCourrier") = DateCourr
                DatRow("ProjetExp") = "N"
                DatRow("Destinataire") = RespoDebut
                DatRow("DestinExterieur") = ""
                DatRow("TypeCourrier") = 0
                DatRow("CodeMarche") = Mid(GridPlanMarche.Rows.Item(NumLigNe).Cells(0).Value.ToString, 2)
                DatRow("Objet") = "Exécution Plan de Passation de Marché"
                DatRow("ExtraitMessage") = Mid(EnleverApost(msgText), 1, 200)
                DatRow("CheminFichier") = nomFile & "\Message.docx"
                DatRow("Priorite") = "Normale"
                DatRow("Suivi") = "N"
                DatRow("DateLecture") = ""
                DatRow("Rayon") = ""
                DatRow("CodeOperateur") = CodeOperateurEnCours
                DatRow("DateEnvoi") = DateCourr
                DatRow("CodeProjet") = ProjetEnCours

                DatSet.Tables("T_Courrier").Rows.Add(DatRow)
                CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "T_Courrier")

                DatSet.Clear()
                BDQUIT(sqlconn)

                If (Proj_mailHote <> "" And Proj_mailCompte <> "") Then
                    EnvoiMail(RespoDebut, "Bonjour," & vbNewLine & "Vous avez reçu un dossier à traiter dans le système de gestion du " & ProjetEnCours & "." & vbNewLine & "Veuillez vous connecter à votre session pour plus d'informations." & vbNewLine & "Cordialement ClearProject.", "")
                End If

                If (Proj_smsTerminal <> "") Then
                    EnvoiSms(RespoDebut, "Bonjour," & vbNewLine & "Vous avez reçu un dossier à traiter dans ClearProject." & vbNewLine & "Veuillez vous connecter à votre session pour plus d'informations." & vbNewLine & ProjetEnCours)
                End If

                FinChargement()
                '*************************************************************************************
                'RemplirTableauPPM(CurrentRefPPM)

            ElseIf (CodeMarche <> 0 And ((TypeM = "Consultants" And NumColonNe = 6) Or (TypeM = "Fournitures" And NumColonNe = 7) Or (TypeM = "Travaux" And NumColonNe = 7)) And TypeLigne = "P") Then
                ReponseDialog = CodeMarche.ToString
                TypeRessource = TypeM
                DialogMethodeConsult.ShowDialog()
                TypeRessource = ""
                'RemplirTableauPPM(CurrentRefPPM)

            ElseIf (CodeMarche <> 0 And ((TypeM = "Consultants" And NumColonNe = 7) Or (TypeM = "Fournitures" And NumColonNe = 9) Or (TypeM = "Travaux" And NumColonNe = 9)) And TypeLigne = "P") Then
                If ((TypeM = "Fournitures" Or TypeM = "Travaux") And GridPlanMarche.Rows.Item(NumLigNe).Cells(7).Value.ToString = "") Then
                    MsgBox("Renseignez d'abord la méthode de passation de marché.", MsgBoxStyle.Information)
                End If

            End If  ' fin des doubles click ***********************************

        End If
    End Sub

    Private Sub GridPlanMarche_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles GridPlanMarche.ColumnHeaderMouseClick
        RemplirTableauPPM(CurrentRefPPM)
    End Sub

    Private Sub CouleurTotaux_ColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CouleurTotaux.ColorChanged
        If (GridPlanMarche.ColumnCount > 1) Then
            RemplirTableauPPM(CurrentRefPPM)
        End If
    End Sub

    Private Sub CouleurTexteTot_ColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CouleurTexteTot.ColorChanged
        If (GridPlanMarche.ColumnCount > 1) Then
            RemplirTableauPPM(CurrentRefPPM)
        End If
    End Sub

    Private Sub RemplirBailleur()
        query = "select InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' order by InitialeBailleur"
        BailleurConcerne.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            BailleurConcerne.Properties.Items.Add(rw(0))
        Next
    End Sub

    Private Sub BailleurConcerne_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BailleurConcerne.SelectedIndexChanged
        query = "select CodeBailleur, InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "' and InitialeBailleur='" & BailleurConcerne.Text & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            If NewPlan = True Then
                CmbConvention.Enabled = True
            End If
            For Each rw As DataRow In dt.Rows
                CodeBailleurCache.Text = rw(0)
            Next
            ChargerConvention(CodeBailleurCache.Text)
        Else
            CmbConvention.Text = ""
            CmbConvention.Enabled = False
        End If

    End Sub

    Private Sub ChargerConvention(ByVal bail As String)
        query = "select CodeConvention from T_Convention where CodeBailleur='" & bail & "' order by CodeConvention"
        CmbConvention.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbConvention.Properties.Items.Add(rw(0).ToString)
        Next
        If (CmbConvention.Properties.Items.Count > 0) Then
            CmbConvention.SelectedIndex = 0
        Else
            CmbConvention.ResetText()
        End If
    End Sub

    Private Sub BtRetour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRetour.Click
        RaserFenetre()
    End Sub

    Private Sub PlanMarche_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub BtActualiserPlan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtActualiserPlan.Click
        If ModePPM = "Genere" Then
            If (MarcheAConsulter.SelectedIndex <= -1) Then
                SuccesMsg("Veuillez choisir un plan.")
                Exit Sub
            End If

            If ConfirmMsg("Voulez-vous actualiser le plan en cours?") = DialogResult.No Then
                Exit Sub
            End If
            DebutChargement(True, "Actualisation du plan en cours...")
            'If ElaboPPM = "Tous les bailleurs" Then
            Dim CurrentRefPPM As String = RefPPM(MarcheAConsulter.SelectedIndex)

            '    query = "SELECT * FROM t_marche WHERE RefPPM='" & CurrentRefPPM & "' AND NumeroDAO IS NULL"
            '    Dim dtMarche As DataTable = ExcecuteSelectQuery(query)
            '    For Each rwMarche As DataRow In dtMarche.Rows
            '        query = "SELECT SUM(MontantBailleur) FROM t_repartitionparbailleur WHERE CodeConvention='" & rwMarche("CodeConvention") & "' AND RefMarche='" & rwMarche("RefMarche") & "'"
            '        Dim MontantEstim As Decimal = Val(ExecuteScallar(query))

            '        Dim LaMethode As String = ""
            '        Dim LaRevue As String = ""
            '        Dim ExceptMethode As String = ""
            '        Dim CodeMethode As Decimal = 0
            '        Dim KodSeuil As Decimal = 0

            '        query = "select P.CodeProcAO,P.AbregeAO,P.TypeMarcheAO,S.CodeProcAO,S.MontantPlanche,S.PlancheInclu,S.MontantPlafond,S.PlafondInclu,S.TypeExamenAO,S.ExceptionRevue,S.CodeSeuil from T_ProcAO as P,T_Seuil as S where P.CodeProcAO=S.CodeProcAO and P.TypeMarcheAO='" & cmbTypeMarche.Text & "' and P.CodeProjet='" & ProjetEnCours & "' and P.RechAuto='OUI' order by S.MontantPlanche"
            '        Dim dt As DataTable = ExcecuteSelectQuery(query)
            '        For Each rw As DataRow In dt.Rows
            '            If (rw("PlancheInclu").ToString = "OUI") Then
            '                If (rw("PlafondInclu").ToString = "OUI") Then
            '                    If CDec(rw("MontantPlanche")) <= MontantEstim Then
            '                        LaMethode = rw("CodeProcAO")
            '                        LaRevue = rw("TypeExamenAO")
            '                        ExceptMethode = rw("ExceptionRevue")
            '                        CodeMethode = rw("CodeProcAO")
            '                        KodSeuil = rw("CodeSeuil")
            '                    End If
            '                ElseIf (rw("MontantPlafond").ToString = "NL") Then
            '                    If CDec(rw("MontantPlanche")) <= MontantEstim Then
            '                        LaMethode = rw("CodeProcAO")
            '                        LaRevue = rw("TypeExamenAO")
            '                        ExceptMethode = rw("ExceptionRevue")
            '                        CodeMethode = rw("CodeProcAO")
            '                        KodSeuil = rw("CodeSeuil")
            '                    End If
            '                Else
            '                    If CDec(rw("MontantPlanche")) <= MontantEstim And CDec(rw("MontantPlafond")) > MontantEstim Then
            '                        LaMethode = rw("CodeProcAO")
            '                        LaRevue = rw("TypeExamenAO")
            '                        ExceptMethode = rw("ExceptionRevue")
            '                        CodeMethode = rw("CodeProcAO")
            '                        KodSeuil = rw("CodeSeuil")
            '                    End If
            '                End If
            '            Else
            '                If (rw("PlafondInclu").ToString = "OUI") Then
            '                    If CDec(rw("MontantPlanche")) < MontantEstim Then
            '                        LaMethode = rw("CodeProcAO")
            '                        LaRevue = rw("TypeExamenAO")
            '                        ExceptMethode = rw("ExceptionRevue")
            '                        CodeMethode = rw("CodeProcAO")
            '                        KodSeuil = rw("CodeSeuil")
            '                    End If
            '                Else
            '                    If (rw("MontantPlafond").ToString <> "TM" And rw("MontantPlafond").ToString <> "NL") Then
            '                        If CDec(rw("MontantPlanche")) < MontantEstim Then
            '                            LaMethode = rw("CodeProcAO")
            '                            LaRevue = rw("TypeExamenAO")
            '                            ExceptMethode = rw("ExceptionRevue")
            '                            CodeMethode = rw("CodeProcAO")
            '                            KodSeuil = rw("CodeSeuil")
            '                        End If
            '                    ElseIf (rw("MontantPlanche") <> "TM") Then
            '                        If (rw("MontantPlafond").ToString = "NL") Then
            '                            If (CDec(rw("MontantPlanche")) < MontantEstim) Then
            '                                LaMethode = rw("CodeProcAO")
            '                                LaRevue = rw("TypeExamenAO")
            '                                ExceptMethode = rw("ExceptionRevue")
            '                                CodeMethode = rw("CodeProcAO")
            '                                KodSeuil = rw("CodeSeuil")
            '                            End If

            '                        ElseIf (rw("MontantPlanche") = "TM") Then
            '                            LaMethode = rw("CodeProcAO")
            '                            LaRevue = rw("TypeExamenAO")
            '                            ExceptMethode = rw("ExceptionRevue")
            '                            CodeMethode = rw("CodeProcAO")
            '                            KodSeuil = rw("CodeSeuil")
            '                        End If
            '                    End If
            '                End If
            '            End If
            '        Next
            '        If (ExceptMethode <> "") Then LaRevue = LaRevue & "*"

            '        query = "UPDATE T_Marche SET MontantEstimatif='" & MontantEstim & "',MethodeMarche ='" & LaMethode & "',RevuePrioPost ='" & LaRevue & "', CodeProcAO ='" & CodeMethode & "', CodeSeuil ='" & KodSeuil & "', DerniereMaj ='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' WHERE RefMarche='" & rwMarche("RefMarche") & "'"
            '        ExecuteNonQuery(query)

            '        query = "UPDATE t_ppm_marche SET dateModif='" & dateconvert(Now) & "'"
            '        ExecuteNonQuery(query)
            '    Next
            'Else
            'End If
            RevisionPPM(CurrentRefPPM)

            FinChargement()

            RemplirTableauPPM(CurrentRefPPM)

            SuccesMsg("Mise à jour terminée avec succès.")
        ElseIf ModePPM = "PPSD" Then
            If (MarcheAConsulter.SelectedIndex <= -1) Then
                SuccesMsg("Veuillez choisir un plan.")
                Exit Sub
            End If
            Dim CurrentRefPPM As String = RefPPM(MarcheAConsulter.SelectedIndex)
            RemplirTableauPPM(CurrentRefPPM)
        End If


    End Sub

    Private Sub BtSupprimerPlan_Click(sender As Object, e As System.EventArgs) Handles BtSupprimerPlan.Click
        If MarcheAConsulter.SelectedIndex <= -1 Then
            SuccesMsg("Veuillez choisir un élement dans le plan.")
            Exit Sub
        End If

        Dim CurrentRefPPM As String = RefPPM(MarcheAConsulter.SelectedIndex)
        Dim SplitSelection As String() = MarcheAConsulter.Text.Split("_"c)
        'Dim CodeTypeMarche As String = String.Empty
        'If SplitSelection(0) = "Consultants".ToLower() Then
        '    CodeTypeMarche = "CS"
        'ElseIf SplitSelection(0) = "Fournitures".ToLower() Then
        '    CodeTypeMarche = "FR"
        'ElseIf SplitSelection(0) = "Services autres que les services de consultants".ToLower() Then
        '    CodeTypeMarche = "SA"
        'ElseIf SplitSelection(0) = "Travaux".ToLower() Then
        '    CodeTypeMarche = "TX"
        'End If
        Dim DateDebut As String = Split(SplitSelection(1), " - ")(0)
        Dim DateFin As String = Split(SplitSelection(1), " - ")(1)
        Dim CodeTypeMarche As String = String.Empty
        If cmbTypeMarche.Text.ToLower = "Consultants".ToLower() Then
            CodeTypeMarche = "CS"
        ElseIf cmbTypeMarche.Text.ToLower = "Fournitures".ToLower() Then
            CodeTypeMarche = "FR"
        ElseIf cmbTypeMarche.Text.ToLower = "Services autres que les services de consultants".ToLower() Then
            CodeTypeMarche = "SA"
        ElseIf cmbTypeMarche.Text.ToLower = "Travaux".ToLower() Then
            CodeTypeMarche = "TX"
        End If
        'Vérifié si il y a une ligne du plan qui est utilisé
        Dim DossierExiste As Boolean = False
        query = "SELECT * FROM t_marche WHERE RefPPM='" & CurrentRefPPM & "' AND CodeProjet='" & ProjetEnCours & "'"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            If rw("TypeMarche").ToString = "Consultants" Then
                If Val(ExecuteScallar("SELECT COUNT(*) FROM t_marche as M, t_ami as D WHERE M.RefMarche=D.RefMarche AND M.RefMarche='" & rw("RefMarche").ToString & "'")) > 0 Then DossierExiste = True
                If Val(ExecuteScallar("SELECT COUNT(*) FROM t_marche as M, t_dp as D WHERE M.RefMarche=D.RefMarche AND D.Statut<>'Annuler' AND M.RefMarche='" & rw("RefMarche").ToString & "'")) > 0 Then DossierExiste = True
                Exit For
            Else
                If Val(ExecuteScallar("SELECT COUNT(*) FROM t_marche as M, t_dao as D WHERE D.statut_DAO<>'Annuler' AND M.RefMarche=D.RefMarche AND M.RefMarche='" & rw("RefMarche").ToString & "'")) > 0 Then
                    DossierExiste = True
                    Exit For
                End If

            End If
        Next
        'query = "SELECT COUNT(*) FROM t_marche WHERE RefPPM='" & CurrentRefPPM & "' AND NumeroDAO IS NOT NULL AND CodeProjet='" & ProjetEnCours & "'"
        'If Val(ExecuteScallar(query)) > 0 Then
        '    FailMsg("Impossible de supprimer ce plan.")
        '    Exit Sub
        'End If
        If DossierExiste = True Then
            FailMsg("Impossible de supprimer ce plan. Car il comporte des marchés déjà élaborés.")
            Exit Sub
        End If
        Dim stringValue As String = "Voulez-vous supprimer le plan selectionné?"
        Dim DontDeletePlan As Boolean = False
        'query = "SELECT COUNT(*) FROM t_marche WHERE RefPPM='" & CurrentRefPPM & "' AND CodeProjet='" & ProjetEnCours & "' AND (NumeroDAO IS NOT NULL AND NumeroDAO<>'')"
        'If Val(Val(ExecuteScallar(query))) > 0 Then
        '    stringValue = "Ce plan comporte des marchés déjà élaborés" & vbNewLine & "Seul les marchés non élaborés seront supprimés." & vbNewLine & "Voulez-vous continuer?"
        '    DontDeletePlan = True
        'End If

        If ConfirmMsg(stringValue) = DialogResult.Yes Then
            query = "delete from t_planmarche WHERE RefMarche IN(SELECT RefMarche FROM t_marche WHERE RefPPM='" & CurrentRefPPM & "' AND CodeProjet='" & ProjetEnCours & "')"
            ExecuteNonQuery(query)
            If ModePPM = "PPSD" Then
                query = "delete from t_ppm_repartitionbailleur WHERE RefPPM='" & CurrentRefPPM & "' AND RefMarche In(Select RefMarche FROM t_marche WHERE RefPPM='" & CurrentRefPPM & "' AND CodeProjet='" & ProjetEnCours & "')"
                ExecuteNonQuery(query)
            End If

            If ModePPM = "Genere" Then
                query = "delete from t_besoinmarche WHERE RefMarche IN(SELECT RefMarche FROM t_marche WHERE RefPPM='" & CurrentRefPPM & "' AND CodeProjet='" & ProjetEnCours & "')"
                ExecuteNonQuery(query)
            End If
            query = "delete from t_marche WHERE RefPPM='" & CurrentRefPPM & "' AND CodeProjet='" & ProjetEnCours & "'"
            ExecuteNonQuery(query)
            If ModePPM = "Genere" Then
                If ElaboPPM = "Tous les bailleurs" Then
                    query = "select DISTINCT P.NumeroComptable, B.InitialeBailleur, SUM(R.MontantBailleur) as MontantBailleurs from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebutMarche.Text) & "' AND DateDebutPartition<='" & dateconvert(DateFinMarche.Text) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & cmbTypeMarche.Text & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and R.MontantBailleur<>'0' GROUP BY P.NumeroComptable"
                    Dim dtAllocation = ExcecuteSelectQuery(query)
                    For Each rwAlloc In dtAllocation.Rows
                        query = "select P.RefBesoinPartition,R.MontantBailleur, R.CodeConvention from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebut) & "' AND DateDebutPartition<='" & dateconvert(DateFin) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & cmbTypeMarche.Text & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and R.MontantBailleur<>'0'"
                        Dim dtRepartition0 = ExcecuteSelectQuery(query)
                        For Each rwRepartition As DataRow In dtRepartition0.Rows
                            query = "update t_repartitionparbailleur set RefMarche='0' where RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "' AND MontantBailleur<>'0'"   'RefBesoinPartition='" & ListeRefBesoin(w) & "' and "
                            ExecuteNonQuery(query)
                        Next
                    Next
                Else
                    query = "select DISTINCT P.NumeroComptable,B.InitialeBailleur from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebut) & "' AND DateFinPartition<='" & dateconvert(DateFin) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & SplitSelection(0) & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and B.InitialeBailleur='" & SplitSelection(2) & "' and R.MontantBailleur<>'0' and R.CodeConvention='" & SplitSelection(3) & "'"
                    Dim dtAllocation As DataTable = ExcecuteSelectQuery(query)
                    For Each rwAlloc As DataRow In dtAllocation.Rows
                        query = "select R.RefRepartBailleur, R.RefBesoinPartition from T_BesoinPartition as P,T_Bailleur as B,T_RepartitionParBailleur as R, T_COMP_SOUS_CLASSE as S where P.NumeroComptable='" & rwAlloc("NumeroComptable") & "' AND P.CodePartition IN(select DISTINCT CodePartition from T_Partition where LENGTH(LibelleCourt)>='5' AND DateDebutPartition>='" & dateconvert(DateDebut) & "' AND DateDebutPartition<='" & dateconvert(DateFin) & "' and CodeProjet='" & ProjetEnCours & "') AND S.CODE_SC=P.NumeroComptable AND S.TypeCompte='" & CodeTypeMarche & "' AND S.CompteMarche='O' and P.TypeBesoin='" & SplitSelection(0) & "' and B.CodeBailleur=R.CodeBailleur and R.RefBesoinPartition=P.RefBesoinPartition and B.InitialeBailleur='" & SplitSelection(2) & "' and R.MontantBailleur<>'0' and R.CodeConvention='" & SplitSelection(3) & "'"
                        Dim dtRepartition As DataTable = ExcecuteSelectQuery(query)
                        For Each rwRepartition As DataRow In dtRepartition.Rows
                            query = "update t_repartitionparbailleur set RefMarche='0' where RefBesoinPartition='" & rwRepartition("RefBesoinPartition") & "' and CodeConvention='" & CmbConvention.Text & "' AND MontantBailleur<>'0'"   'RefBesoinPartition='" & ListeRefBesoin(w) & "' and "
                            ExecuteNonQuery(query)
                        Next
                    Next
                End If

            End If

            If Not DontDeletePlan Then
                query = "DELETE FROM t_ppm_marche WHERE RefPPM='" & CurrentRefPPM & "'"
                ExecuteNonQuery(query)
                BtRetour.PerformClick()
                SuccesMsg("Plan supprimé avec succès.")
                btAjout.Enabled = False
                BtActualiserPlan.Enabled = False
            Else
                BtRetour.PerformClick()
                SuccesMsg("Les éléments ont été correctement supprimé.")
            End If

        End If

    End Sub

    Private Sub PlanMarche_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        BtRetour.PerformClick()
    End Sub

    Private Sub BtNouveauPlan_EnabledChanged(sender As Object, e As EventArgs) Handles BtNouveauPlan.EnabledChanged
        BtSupprimerPlan.Enabled = Not BtNouveauPlan.Enabled
        btImprimerPlan.Enabled = Not BtNouveauPlan.Enabled
    End Sub
    Private Sub CouleursLignes_Paint(sender As Object, e As PaintEventArgs) Handles CouleursLignes.Paint

    End Sub

    Private Sub btImportPPSD_Click(sender As Object, e As EventArgs) Handles btImportPPSD.Click
        Try
            Dim OpFile As New OpenFileDialog
            OpFile.Filter = "Excel|*.xlsx;*.xls"
            If OpFile.ShowDialog() = DialogResult.OK Then
                DebutChargement(True, "Vérification des données du fichier Excel en cours...")
                Textimpfiche.Text = OpFile.FileName
                Dim app As New Excel.Application
                app.Workbooks.Open(OpFile.FileName)
                Dim AucuneDonnee As Boolean = False 'Va nous permettre de savoir si aucune ligne d'imputation n'a étét défini
                Dim LstActivite As New List(Of String) 'Va nous permettre de savoir si aucun coût direct n'a été défini
                Dim Contents As New List(Of Object) 'On va sauvegarder toutes les coûts directs pendant le processus de verification afin de ne pas reparcourir le fichier pour l'importation
                Dim Headers As New List(Of Object) 'On va sauvegarder toutes les infos relatives aux fiches durant le processus de verification afin de ne pas reparcourir le fichier pour l'importation
                Dim Conventions As New List(Of Object) 'On va sauvegarder toutes les infos relatives aux fiches durant le processus de verification afin de ne pas reparcourir le fichier pour l'importation
                For i As Integer = 1 To app.Workbooks(1).Worksheets.Count() - 1
                    Dim Feuille = app.Workbooks(1).Worksheets(i)
                    Dim Titre As String = Feuille.Range("A1").Value.ToString
                    If Titre <> "PLAN DE PASSATION DE MARCHÉ" Then
                        FinChargement()
                        FailMsg("Le plan de passation de marché " & Feuille.Name & " n'a pas le bon format d'importation")
                        app.Quit()
                        Exit Sub
                    End If
                    Dim NomProjet As String = ""
                    Dim CodeProjet As String = ""
                    Dim NumeroPlan As String = ""
                    Dim DateDebutPlan As String = ""
                    Dim DateFinPlan As String = ""
                    Dim TypeMarche As String = ""
                    Dim LieuActivite As String = ""
                    Dim ResponsableActivite As String = ""
                    Dim Composante As String = ""
                    Dim SousComposante As String = ""
                    'Dim Description As String = ""
                    Dim Justification As String = ""
                    Dim ResultatActivite As String = ""
                    Dim DateElaboration As String = ""
                    Dim IndicateurPerformance As String = ""
                    Dim Editeur As String = ""
                    Dim RowCount = Feuille.Cells(Feuille.Rows.Count, 1).End(Excel.XlDirection.xlUp).Row
                    Dim ColCount = Feuille.Cells.Find("*", , , , Excel.XlSearchOrder.xlByColumns, Excel.XlSearchDirection.xlPrevious).Column
                    Dim nbConvention As Decimal = ColCount - 6
                    Dim GetToatl As Decimal = RowCount
                    Try
                        NomProjet = Feuille.Range("B2").Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        CodeProjet = Feuille.Range("B3").Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        NumeroPlan = Feuille.Range("B4").Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        DateDebutPlan = Feuille.Range("B5").Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        DateFinPlan = Feuille.Range("B6").Value.ToString()
                    Catch ex As Exception
                    End Try
                    Try
                        TypeMarche = Feuille.Range("B7").Value.ToString()
                    Catch ex As Exception
                    End Try
                    'Try
                    '    LieuActivite = Feuille.Range("B6").Value.ToString()
                    'Catch ex As Exception
                    'End Try
                    'Try
                    '    ResponsableActivite = Feuille.Range("B7").Value.ToString()
                    'Catch ex As Exception
                    'End Try
                    'Try
                    '    Composante = Feuille.Range("C9").Value.ToString()
                    'Catch ex As Exception
                    'End Try
                    'Try
                    '    SousComposante = Feuille.Range("C10").Value.ToString()
                    'Catch ex As Exception
                    'End Try
                    'Try
                    '    Description = Feuille.Range("C11").Value.ToString()
                    'Catch ex As Exception
                    'End Try
                    'Try
                    '    Justification = Feuille.Range("C12").Value.ToString()
                    'Catch ex As Exception
                    'End Try
                    'Try
                    '    ResultatActivite = Feuille.Range("C" & (RowCount - 1)).Value.ToString()
                    'Catch ex As Exception
                    'End Try
                    'Try
                    '    DateElaboration = Feuille.Range("E" & RowCount).Value.ToString()
                    'Catch ex As Exception
                    'End Try
                    'Try
                    '    IndicateurPerformance = Feuille.Range("C" & (RowCount - 2)).Value.ToString()
                    'Catch ex As Exception
                    'End Try
                    'Try
                    '    Editeur = Feuille.Range("B" & RowCount).Value.ToString()
                    'Catch ex As Exception
                    'End Try

                    If NomProjet.ToString().Length = 0 Then
                        FinChargement()
                        FailMsg("Entrer le nom du projet sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    If CodeProjet.ToString().Length = 0 Then
                        FinChargement()
                        FailMsg("Entrer l'abreviation du projet sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    If NumeroPlan.ToString().Length = 0 Then
                        FinChargement()
                        FailMsg("Entrer le numero du plan sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    If DateDebutPlan.ToString().Length = 0 Or Not IsDate(DateDebutPlan.ToString()) Then
                        FinChargement()
                        FailMsg("Entrer la date de début de période sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    If DateFinPlan.ToString().Length = 0 Or Not IsDate(DateFinPlan.ToString()) Then
                        FinChargement()
                        FailMsg("Entrer la date de fin de période sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    If CDate(DateDebutPlan) > CDate(DateFinPlan) Then
                        FinChargement()
                        FailMsg("La date de début de période ne peut pas supérieur à la date de fin sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    ElseIf CDate(DateDebutPlan) = CDate(DateFinPlan) Then
                        FinChargement()
                        FailMsg("La date de début  de période ne peut pas être égale à la date de fin sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    'If LieuActivite.ToString().Length = 0 Then
                    '    FinChargement()
                    '    FailMsg("Entrer le lieu de l'activité sur la feuille " & Feuille.Name)
                    '    app.Quit()
                    '    Exit Sub
                    'End If
                    'If ResponsableActivite.ToString().Length = 0 Then
                    '    FinChargement()
                    '    FailMsg("Entrer le nom et prénoms du responsable de l'activité sur la feuille " & Feuille.Name)
                    '    app.Quit()
                    '    Exit Sub
                    'End If
                    If TypeMarche.ToString().Length = 0 Then
                        FinChargement()
                        FailMsg("Choisissez le type de marché sur la feuille " & Feuille.Name)
                        app.Quit()
                        Exit Sub
                    End If
                    'If Composante.ToString().Length = 0 Then
                    '    FinChargement()
                    '    FailMsg("Entrer la composante de l'activité sur la feuille " & Feuille.Name)
                    '    app.Quit()
                    '    Exit Sub
                    'End If
                    'If SousComposante.ToString().Length = 0 Then
                    '    FinChargement()
                    '    FailMsg("Entrer la sous composante de l'activité sur la feuille " & Feuille.Name)
                    '    app.Quit()
                    '    Exit Sub
                    'End If
                    'If Description.ToString().Length = 0 Then
                    '    FinChargement()
                    '    FailMsg("Entrer la description de l'activité sur la feuille " & Feuille.Name)
                    '    app.Quit()
                    '    Exit Sub
                    'End If
                    'If Justification.ToString().Length = 0 Then
                    '    FinChargement()
                    '    FailMsg("Entrer la justification de l'activité sur la feuille " & Feuille.Name)
                    '    app.Quit()
                    '    Exit Sub
                    'End If
                    'If IndicateurPerformance.ToString().Length = 0 Then
                    '    FinChargement()
                    '    FailMsg("Entrer l'indicateur de performance sur la feuille " & Feuille.Name)
                    '    app.Quit()
                    '    Exit Sub
                    'End If
                    'If ResultatActivite.ToString().Length = 0 Then
                    '    FinChargement()
                    '    FailMsg("Entrer le résultat attendu de l'activité sur la feuille " & Feuille.Name)
                    '    app.Quit()
                    '    Exit Sub
                    'End If
                    'If Editeur.ToString().Length = 0 Then
                    '    FinChargement()
                    '    FailMsg("Entrer le nom et prénoms de celui qui a établi l'activité sur la feuille " & Feuille.Name)
                    '    app.Quit()
                    '    Exit Sub
                    'End If
                    'If DateElaboration.ToString().Length = 0 Or Not IsDate(DateElaboration.ToString()) Then
                    '    FinChargement()
                    '    FailMsg("Entrer la date d'élaboration sur la " & Feuille.Name)
                    '    app.Quit()
                    '    Exit Sub
                    'End If

                    query = "SELECT CodeProjet from t_projet WHERE CodeProjet='" & CodeProjet & "'"
                    Dim dtResult As DataTable = ExcecuteSelectQuery(query)
                    If dtResult.Rows.Count = 0 Then
                        FinChargement()
                        FailMsg("Le code du projet de la feuille " & Feuille.Name & " n'existe pas.")
                        app.Quit()
                        Exit Sub
                    End If

                    'query = "SELECT EMP_ID from t_grh_employe WHERE TRIM(CONCAT(EMP_NOM,' ',EMP_PRENOMS))='" & EnleverApost(ResponsableActivite) & "'"
                    'dtResult = ExcecuteSelectQuery(query)
                    'If dtResult.Rows.Count = 0 Then
                    '    FinChargement()
                    '    FailMsg("Le responsable de la feuille " & Feuille.Name & " n'existe pas.")
                    '    app.Quit()
                    '    Exit Sub
                    'Else
                    'End If
                    query = "SELECT NumeroPlan from t_ppm_marche WHERE NumeroPlan='" & EnleverApost(NumeroPlan) & "'"
                    dtResult = ExcecuteSelectQuery(query)
                    If dtResult.Rows.Count = 0 Then
                        FinChargement()
                        app.Quit()
                        Exit Sub
                    End If

                    query = "SELECT CodeTypeMarche from t_typemarche WHERE TypeMarche='" & EnleverApost(TypeMarche) & "'"
                    dtResult = ExcecuteSelectQuery(query)
                    If dtResult.Rows.Count = 0 Then
                        FinChargement()
                        FailMsg("Le type demarché choisir pour le PPM sur la feuille " & Feuille.Name & " n'existe pas.")
                        app.Quit()
                        Exit Sub
                    End If

                    query = "select PeriodeMarche,DescriptionMarche from T_Marche where CodeProjet='" & CodeProjet & "' and TypeMarche='" & TypeMarche & "'"
                    Dim dt As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        Dim GroupDate() As String = (rw("PeriodeMarche").ToString).Split(" "c)
                        Dim PeriodeDebut As Date = CDate(GroupDate(0))
                        Dim PeriodeFin As Date = CDate(GroupDate(2))
                        If ((Date.Compare(CDate(DateDebutPlan), PeriodeDebut) >= 0 And Date.Compare(CDate(DateDebutPlan), PeriodeFin) <= 0) Or (Date.Compare(CDate(DateFinPlan), PeriodeDebut) >= 0 And Date.Compare(CDate(DateFinPlan), PeriodeFin) <= 0)) Then
                            FinChargement()
                            FailMsg("Impossible de poursuivre l'importation de ce PPSD." & vbNewLine & "Soit des marchés existent déjà ou la période chevauche une déjà existante.")
                            app.Quit()
                            Exit Sub
                        End If
                    Next
                    'query = "SELECT codepartition from t_partition WHERE CodePartitionMere=" & dtResult.Rows(0).Item("codepartition") & " and LibellePartition='" & EnleverApost(SousComposante) & "' and CodeClassePartition='2'"
                    'dtResult = ExcecuteSelectQuery(query)
                    'If dtResult.Rows.Count = 0 Then
                    '    FinChargement()
                    '    FailMsg("La sous composante " & SousComposante & " n'existe pas sur la composante " & Composante & " sur la feuille " & Feuille.Name & ".")
                    '    app.Quit()
                    '    Exit Sub
                    'End If

                    Dim Titreconvention As String = Feuille.Range("F8").Value.ToString
                    If Titreconvention <> "Répartition du montant par convention" Then
                        FinChargement()
                        FailMsg("La feuille de calcul " & Feuille.Name & " n'a pas le bon format d'importation")
                        app.Quit()
                        Exit Sub
                    End If
                    Dim TitreTotal As String = Feuille.Range("A" & GetToatl).Value
                    If TitreTotal <> "TOTAL" Then
                        FinChargement()
                        FailMsg("La feuille de calcul " & Feuille.Name & " n'a pas le bon format d'importation")
                        app.Quit()
                        Exit Sub
                    End If
                    If nbConvention <= 0 Then
                        FinChargement()
                        FailMsg("La feuille de calcul " & Feuille.Name & " n'a pas le bon format d'importation")
                        app.Quit()
                        Exit Sub
                    End If
                    Dim dtConvention As New DataTable
                    dtConvention.Columns.Add("CodeConvention")
                    dtConvention.Rows.Clear()
                    For l = 7 To ColCount
                        Dim Conv As String = ""
                        Try
                            Conv = Feuille.Cells(10, l).Value.ToString()
                        Catch ex As Exception
                        End Try
                        If Conv.Length = 0 Then
                            FinChargement()
                            FailMsg("Veuillez entrer toutes les conventions sur la feuille " & Feuille.Name & ".")
                            app.Quit()
                            Exit Sub
                        Else
                            query = "SELECT CodeConvention from t_convention WHERE CodeConvention='" & EnleverApost(Conv) & "'"
                            dtResult = ExcecuteSelectQuery(query)
                            If dtResult.Rows.Count = 0 Then
                                FinChargement()
                                FailMsg("La convention " & Conv & " de la feuille " & Feuille.Name & " n'existe pas.")
                                app.Quit()
                                Exit Sub
                            Else
                                dtConvention.Rows.Add(Conv)
                            End If
                        End If
                    Next

                    'Enregistrement des conventions dans notre liste
                    Conventions.Add(dtConvention)

                    Dim doublonConv As Boolean = False
                    Dim ConvDouble As String = ""
                    For k = 0 To (dtConvention.Rows.Count - 2)
                        If dtConvention.Rows(k).Item("CodeConvention") = dtConvention.Rows(k + 1).Item("CodeConvention") Then
                            doublonConv = True
                            ConvDouble = dtConvention.Rows(k).Item("CodeConvention")
                        End If
                    Next
                    If doublonConv Then
                        FinChargement()
                        FailMsg("La convention " & ConvDouble & " est repétée sur la feuille " & Feuille.Name & ".")
                        app.Quit()
                        Exit Sub
                    End If

                    'On va sauvegarder notre entête dans notre liste d'objet
                    Dim dtHeaders As New DataTable
                    dtHeaders.Columns.Add("CodeProjet")
                    dtHeaders.Columns.Add("DebutPlan")
                    dtHeaders.Columns.Add("FinPlan")
                    dtHeaders.Columns.Add("TypeMarche")
                    dtHeaders.Rows.Clear()
                    'dtHeaders.Rows.Add(CodeProjet, DateDebutPlan, DateFinPlan, LieuActivite, ResponsableActivite, TypeMarche, Composante, SousComposante, Description, Justification, IndicateurPerformance, ResultatActivite, Editeur, DateElaboration)
                    dtHeaders.Rows.Add(CodeProjet, DateDebutPlan, DateFinPlan, TypeMarche)
                    Headers.Add(dtHeaders)

                    Dim dtLigne As New DataTable
                    dtLigne.Columns.Add("N°")
                    dtLigne.Columns.Add("Description")
                    dtLigne.Columns.Add("Montant")
                    dtLigne.Columns.Add("TypeExamen")
                    dtLigne.Columns.Add("MethodePPM")
                    For k = 0 To (dtConvention.Rows.Count - 1)
                        dtLigne.Columns.Add("MontantConv" & k)
                    Next
                    dtLigne.Rows.Clear()
                    'On parcoure les lignes des imputations
                    For l = 11 To (GetToatl - 1) 'On va de la ligne 14 jusqu'a la ligne qui précède le TOTAL
                        Dim Numero As String = ""
                        Dim Description As String = ""
                        Dim Montant As String = ""
                        Dim TypeExamen As String = ""
                        Dim MethodePPM As String = ""
                        Try
                            Numero = Feuille.Range("A" & l).Value.ToString()
                        Catch ex As Exception
                        End Try
                        Try
                            Description = Feuille.Range("B" & l).Value.ToString()
                        Catch ex As Exception
                        End Try
                        Try
                            Montant = Feuille.Range("C" & l).Value.ToString()
                        Catch ex As Exception
                        End Try
                        Try
                            TypeExamen = Feuille.Range("D" & l).Value.ToString()
                        Catch ex As Exception
                        End Try
                        Try
                            MethodePPM = Feuille.Range("E" & l).Value.ToString()
                        Catch ex As Exception
                        End Try

                        If Val(Numero) = 0 And Description.Length = 0 And TypeExamen.Length = 0 And MethodePPM.Length = 0 And Val(Montant) = 0 Then
                            If Not LstActivite.Contains(Feuille.Name) Then
                                LstActivite.Add(Feuille.Name)
                            End If
                            Continue For
                        End If

                        If Description.Length = 0 Then
                            FinChargement()
                            FailMsg("Veuillez entrer la description du marché à la ligne " & l & " de la feuille " & Feuille.Name & ".")
                            app.Quit()
                            Exit Sub
                        End If
                        If Val(Montant) = 0 Then
                            FinChargement()
                            FailMsg("Veuillez entrer le montant estimatif du marché à la ligne " & l & " de la feuille " & Feuille.Name & ".")
                            app.Quit()
                            Exit Sub
                        End If
                        If TypeExamen.Length = 0 Then
                            FinChargement()
                            FailMsg("Veuillez entrer le type d'examen à la ligne " & l & " de la feuille " & Feuille.Name & ".")
                            app.Quit()
                            Exit Sub
                        Else
                            If TypeExamen.ToLower <> "priori" And TypeExamen.ToLower <> "postériori" Then
                                FinChargement()
                                FailMsg("Le type d'examen entrer à la ligne " & l & " de la feuille " & Feuille.Name & " n'est pas correcte.")
                                app.Quit()
                                Exit Sub
                            End If
                        End If

                        If MethodePPM.Length = 0 Then
                            FinChargement()
                            FailMsg("Veuillez entrer la methode de passation de marchés à la ligne " & l & " de la feuille " & Feuille.Name & ".")
                            app.Quit()
                            Exit Sub
                        Else
                            query = "SELECT AbregeAO from t_procao WHERE AbregeAO='" & EnleverApost(MethodePPM) & "' AND TypeMarcheAO='" & EnleverApost(TypeMarche) & "' AND CodeProjet='" & EnleverApost(CodeProjet) & "'"
                            dtResult = ExcecuteSelectQuery(query)
                            If dtResult.Rows.Count = 0 Then
                                FinChargement()
                                FailMsg("La méthode de passation de marchés " & MethodePPM & " à la ligne " & l & " de la feuille " & Feuille.Name & " n'existe pas.")
                                app.Quit()
                                Exit Sub
                            End If
                        End If

                        Dim MTotalConvention As Decimal = 0
                        For k = 7 To ColCount
                            Dim MontantConv As String = ""
                            Try
                                If Val(Feuille.Cells(l, k).Value.ToString()) = 0 Then
                                    Dim Conv = Feuille.Cells(9, k).Value.ToString()
                                    FinChargement()
                                    FailMsg("Entrer correctement le montant de la convention " & Conv & " à la ligne " & l & " de la feuille " & Feuille.Name & ".")
                                    app.Quit()
                                    Exit Sub
                                End If
                                MontantConv = Feuille.Cells(l, k).Value.ToString()
                                MTotalConvention += Val(MontantConv)
                            Catch ex As Exception
                            End Try
                        Next
                        If MTotalConvention <> Val(Montant) Then
                            FinChargement()
                            FailMsg("Veuillez répartir correctement le montant estimatif du marché sur les conventions à la ligne " & l & " sur la feuille " & Feuille.Name & ".")
                            app.Quit()
                            Exit Sub
                        End If
                        Dim NewRow As DataRow = dtLigne.NewRow
                        NewRow("N°") = Numero
                        NewRow("Description") = Description
                        NewRow("TypeExamen") = TypeExamen
                        NewRow("MethodePPM") = MethodePPM
                        NewRow("Montant") = Montant
                        For k = 7 To ColCount
                            Dim MontantConv As String = ""
                            Try

                                If Val(Feuille.Cells(l, k).Value.ToString()) = 0 Then
                                    Dim Conv = Feuille.Cells(9, k).Value.ToString()
                                    FinChargement()
                                    FailMsg("Entrer correctement le montant de la convention " & Conv & " à la ligne " & l & " de la feuille " & Feuille.Name & ".")
                                    app.Quit()
                                    Exit Sub
                                End If
                                MontantConv = Feuille.Cells(l, k).Value.ToString()
                                MTotalConvention += Val(MontantConv)
                                NewRow("MontantConv" & (k - 7)) = Val(MontantConv)
                            Catch ex As Exception
                                NewRow("MontantConv" & (k - 7)) = 0
                            End Try
                        Next
                        dtLigne.Rows.Add(NewRow)
                        AucuneDonnee = True
                    Next
                    Contents.Add(dtLigne)
                Next

                If Not AucuneDonnee Then
                    Dim str As String = String.Empty
                    For i = 0 To (LstActivite.Count - 1)
                        str += "=> " & LstActivite.Item(i)
                    Next
                    FinChargement()
                    FailMsg("Nous avons détecté des plans sans marchées :" & vbNewLine & str)
                    app.Quit()
                    Exit Sub
                Else
                    FinChargement()
                    If MessageBox.Show("Vérification du fichier terminée avec succès." & "Voulez-vous commencer l'importation?", "ClearProject", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                        app.Quit()
                        Exit Sub
                    End If
                End If

                DebutChargement(True, "Importation des données Excel en cours...")
                app.Quit()
                Threading.Thread.Sleep(2000)
                'FinChargement()
                Dim countError As Decimal = 0
                Dim Errors As String = String.Empty
                Dim nbFiche As Decimal = Headers.Count
                For i = 0 To (nbFiche - 1)
                    Dim dtHeader As DataTable = CType(Headers(i), DataTable)
                    Dim dtContent As DataTable = CType(Contents(i), DataTable)
                    Dim dtConvention As DataTable = CType(Conventions(i), DataTable)
                    For k = 0 To (dtHeader.Rows.Count - 1)
                        Dim rwHeader As DataRow = dtHeader.Rows(k)
                        'On enregistre les marchés
                        'On collecte les données pour inserer la fiche d'activité
                        'Declaration des variables
                        Dim CodeProjet = rwHeader("CodeProjet").ToString
                        Dim periode = CDate(rwHeader("DebutPlan")) & " - " & CDate(rwHeader("FinPlan"))
                        Dim DateDeb As Date = CDate(rwHeader("DebutPlan"))
                        Dim Datefin As Date = CDate(rwHeader("FinPlan"))
                        Dim TypeMarche = rwHeader("TypeMarche").ToString
                        'Insertion du ppm
                        Dim CodeNewPlan As String = String.Empty
                        Try
                            query = "insert into t_ppm_marche values (NULL,'" & EnleverApost(TypeMarche) & "_" & EnleverApost(periode) & "','" & EnleverApost(TypeMarche) & "','" & EnleverApost(periode) & "','Tous',NULL,'PPSD','" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "','" & dateconvert(Now.ToShortDateString) & " " & Now.ToLongTimeString & "','" & CodeProjet & "','" & CodeUtilisateur & "')"
                            ExecuteNonQuery(query)
                            CodeNewPlan = ExecuteScallar("SELECT MAX(RefPPM) FROM t_ppm_marche")
                        Catch ex As Exception
                            FinChargement()
                            FailMsg("Nous n'avons pas pu importer le plan de type " & TypeMarche)
                            Exit Sub
                            'Dim rep As DialogResult = MessageBox.Show("Nous n'avons pas pu importer le plan de type " & TypeMarche & vbNewLine & "Voulez-vous continuer l'importation?", "ClearProject", MessageBoxButtons.YesNo, MessageBoxIcon.Error)
                            'If rep = DialogResult.No Then
                            '    Exit Sub
                            'Else
                            '    DebutChargement(True, "Importation des données Excel en cours...")
                            '    countError += 1
                            '    Errors += rwHeader("TypeMarche") & vbNewLine
                            'End If
                        End Try
                        'Enregistrements des marchés
                        For l = 0 To (dtContent.Rows.Count - 1)
                            'Declaration des variables
                            Dim rwMarche = dtContent.Rows(l)
                            Dim TypeExamen As String = rwMarche("TypeExamen")
                            Dim Description As String = rwMarche("Description")
                            Dim MethodePPM As String = rwMarche("MethodePPM")
                            Dim Montant As String = rwMarche("Montant")
                            Dim CodeBailleur As String = rwMarche("Montant")
                            Try
                                Dim Bailleurs As New ArrayList
                                Dim Convent As New ArrayList
                                For c = 5 To (dtContent.Columns.Count - 1)
                                    Dim TConv As String = dtContent.Columns(c).ColumnName
                                    Dim MontConv As Decimal = CDec(rwMarche(TConv))
                                    If MontConv > 0 Then
                                        Dim Conv As String = dtConvention.Rows((c - 5)).Item("CodeConvention")
                                        Dim Bailleur As String = ExecuteScallar("select B.InitialeBailleur from t_bailleur as B, t_convention as C where B.CodeBailleur=C.CodeBailleur AND C.CodeConvention='" & Conv & "'")
                                        Bailleurs.Add(Bailleur)
                                        Convent.Add(Conv & "&" & MontConv)
                                    End If
                                Next
                                Dim lesBailleurs As String = ""
                                Dim lesConventions As String = ""
                                If Bailleurs.Count > 1 Then
                                    For j = 0 To Bailleurs.Count - 1
                                        lesBailleurs += Bailleurs.Item(j) & " | "
                                    Next
                                Else
                                    For j = 0 To Bailleurs.Count - 1
                                        lesBailleurs = Bailleurs.Item(j)
                                    Next
                                End If
                                If Convent.Count > 1 Then
                                    For j = 0 To Convent.Count - 1
                                        lesConventions += Convent.Item(j).ToString.Split("&")(0) & " | "
                                    Next
                                Else
                                    For j = 0 To Convent.Count - 1
                                        lesConventions = Convent.Item(j).ToString.Split("&")(0)
                                    Next
                                End If
                                Dim IdMethodePPM As Integer = Val(ExecuteScallar("SELECT CodeProcAO FROM t_procao WHERE AbregeAO='" & EnleverApost(MethodePPM) & "' AND TypeMarcheAO='" & TypeMarche & "'"))
                                query = "insert into t_marche(CodeProjet,TypeMarche,NumeroComptable,DescriptionMarche,MontantEstimatif,RevuePrioPost,PeriodeMarche,InitialeBailleur,CodeConvention,CodeProcAO,RefPPM,DerniereMaj,Convention_ChefFile) values('" & EnleverApost(CodeProjet) & "','" & EnleverApost(TypeMarche) & "',NULL,'" & EnleverApost(Description) & "','" & EnleverApost(Montant) & "','" & EnleverApost(TypeExamen) & "','" & EnleverApost(periode) & "','" & lesBailleurs & "','" & lesConventions.Split("&")(0).ToString & "','" & IdMethodePPM & "','" & CodeNewPlan & "','" & Now.ToShortDateString & " " & Now.ToLongTimeString & "','" & chefFile & "')"
                                ExecuteNonQuery(query)
                            Catch ex As Exception
                                FailMsg(ex.ToString)
                                'query = "DELETE FROM t_marche WHERE RefPPM='" & CodeNewPlan & "'"
                                'ExecuteNonQuery(query)
                                'query = "DELETE FROM t_ppm_marche WHERE RefPPM='" & CodeNewPlan & "'"
                                'ExecuteNonQuery(query)
                                'countError += 1
                                'Errors += rwHeader("TypeMarche") & vbNewLine
                            End Try
                            Dim LastRefMarche As String = ExecuteScallar("SELECT MAX(RefMarche) FROM t_marche")

                            For c = 5 To (dtContent.Columns.Count - 1)
                                Dim TConv As String = dtContent.Columns(c).ColumnName
                                Dim MontConv As Decimal = CDec(rwMarche(TConv))
                                If MontConv > 0 Then
                                    Dim Conv As String = dtConvention.Rows((c - 5)).Item("CodeConvention")
                                    Try
                                        query = "insert into t_ppm_repartitionbailleur values(NULL,'" & CodeNewPlan & "','" & LastRefMarche & "','" & Conv & "','" & MontConv & "')"
                                        ExecuteNonQuery(query)
                                    Catch ex As Exception
                                    End Try
                                End If
                            Next
                        Next
                    Next
                Next
                FinChargement()
                If countError = 0 Then
                    SuccesMsg("Importation terminée avec succès")
                    cmbDevise.Text = "US$"
                    RemplirMarcheAConsulter()
                    query = "SELECT max(RefPPM), LibellePPM FROM t_ppm_marche WHERE CodeProjet='" & ProjetEnCours & "'"
                    Dim dtPPM As DataTable = ExcecuteSelectQuery(query)
                    For Each rwPPM As DataRow In dtPPM.Rows
                        MarcheAConsulter.Text = (MettreApost(rwPPM("LibellePPM")))
                    Next
                Else
                    SuccesMsg("Les marchés suivants n'ont pas pu être importées :" & vbNewLine & Errors)
                End If

                Exit Sub

            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
            FinChargement()
        End Try
    End Sub

    Private Sub GridPlanMarche_CellMouseMove(sender As Object, e As DataGridViewCellMouseEventArgs) Handles GridPlanMarche.CellMouseMove
        GridPlanMarche.Cursor = Cursors.Hand
    End Sub
    Private Sub GridPlanMarche_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles GridPlanMarche.CellMouseLeave
        GridPlanMarche.Cursor = Cursors.Default
    End Sub

    Private Sub btSaisiePPM_Click(sender As Object, e As EventArgs) Handles btSaisiePPM.Click
        Dim NewSaisi As New SaisiePPM
        NewSaisi.ShowDialog()
    End Sub

    Private Sub cmbDevise_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDevise.SelectedIndexChanged
        If MarcheAConsulter.SelectedIndex > -1 Then
            CurrentRefPPM = RefPPM(MarcheAConsulter.SelectedIndex)
        Else
            CurrentRefPPM = -1
        End If
        RemplirTableauPPM(CurrentRefPPM)
    End Sub

    Private Sub btAjout_Click(sender As Object, e As EventArgs) Handles btAjout.Click
        Dim newAjout = New SaisiePPM
        newAjout.IDPlan = CurrentRefPPM
        newAjout.Text = "Ajout de nouvelle ligne au plan"
        newAjout.ShowDialog()
    End Sub

    Private Sub ModifierLaLigne_Click(sender As Object, e As EventArgs) Handles ModifierLaLigne.Click
        If RefMarche <> "" Then
            If Mid(RefMarche.ToString, 1, 1) = "P" Then
                If ConfirmMsg("Voulez-vous vraiment modifier la ligne N° " & ligne & " ?") = DialogResult.Yes Then
                    Dim NewModLigne As New ModifLignePPM
                    NewModLigne.IDPlan = CurrentRefPPM
                    NewModLigne.RefMarcheMod = Mid(RefMarche.ToString, 2)
                    NewModLigne.ShowDialog()
                End If
            End If
        End If
    End Sub

    Private Sub SupprimerLaLigne_Click(sender As Object, e As EventArgs) Handles SupprimerLaLigne.Click
        If RefMarche <> "" Then
            If Mid(RefMarche.ToString, 1, 1) = "P" Then
                If ConfirmMsg("Voulez-vous vraiment suprimer la ligne N° " & ligne & " ?") = DialogResult.Yes Then
                    Dim RefMarcheSUP = Mid(RefMarche.ToString, 2)
                    query = "DELETE FROM t_ppm_repartitionbailleur WHERE RefPPM='" & CurrentRefPPM & "' AND RefMarche='" & RefMarcheSUP & "'"
                    ExecuteNonQuery(query)
                    query = "DELETE FROM t_marche WHERE RefPPM='" & CurrentRefPPM & "' AND RefMarche='" & RefMarcheSUP & "'"
                    ExecuteNonQuery(query)
                    SuccesMsg("Ligne supprimée avec succès.")
                    BtActualiserPlan.PerformClick()
                End If
            End If
        End If
        'MsgBox(GridPlanMarche.Rows.Item(GridPlanMarche.CurrentCell.RowIndex).Cells(0).Value)
    End Sub

    Private Sub GridPlanMarche_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles GridPlanMarche.CellMouseUp
        'RefMarche = GridPlanMarche.Rows.Item(GridPlanMarche.CurrentCell.RowIndex).Cells(0).Value
    End Sub

    Private Sub GridPlanMarche_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles GridPlanMarche.CellMouseClick
        'RefMarche = GridPlanMarche.Rows.Item(GridPlanMarche.CurrentCell.RowIndex).Cells(0).Value
    End Sub

    Private Sub GridPlanMarche_MouseClick(sender As Object, e As MouseEventArgs) Handles GridPlanMarche.MouseClick
        If ModePPM = "PPSD" Then
            If (e.Button = System.Windows.Forms.MouseButtons.Right) Then
                'ContextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y)
                ContextMenuStrip1.Show(GridPlanMarche.PointToScreen(e.Location))
                ContextMenuStrip1.Items(0).Visible = True
                ContextMenuStrip1.Items(1).Visible = True
                ContextMenuStrip1.Items(2).Visible = False
            End If
        Else
            If (e.Button = System.Windows.Forms.MouseButtons.Right) Then
                ContextMenuStrip1.Show(GridPlanMarche.PointToScreen(e.Location))
                ContextMenuStrip1.Items(0).Visible = False
                ContextMenuStrip1.Items(1).Visible = False
                ContextMenuStrip1.Items(2).Visible = True
            End If
        End If
    End Sub
    Private Sub GridPlanMarche_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles GridPlanMarche.CellMouseDown
        'If ModePPM = "PPSD" Then
        If e.RowIndex <> -1 And e.ColumnIndex <> -1 Then
            If (e.Button = MouseButtons.Right) Then
                Try
                    GridPlanMarche.CurrentCell = GridPlanMarche.Rows(e.RowIndex).Cells(e.ColumnIndex)
                    ' Can leave these here - doesn't hurt
                    GridPlanMarche.Rows(e.RowIndex).Selected = True
                    GridPlanMarche.Focus()
                    RefMarche = Convert.ToString(GridPlanMarche.Rows(e.RowIndex).Cells(0).Value)
                    NumeroDAO = Convert.ToString(GridPlanMarche.Rows(e.RowIndex).Cells(0).Value)
                    ligne = Convert.ToInt32(GridPlanMarche.Rows(e.RowIndex).Cells(1).Value)
                Catch ex As Exception
                End Try
            Else
                ContextMenuStrip1.Hide()
            End If
        End If
        'Else
        'End If
    End Sub

    Private Sub ModifierLaMéthodeDePassationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModifierLaMéthodeDePassationToolStripMenuItem.Click
        If RefMarche <> "" Then
            If Mid(RefMarche.ToString, 1, 1) = "P" Then
                If ConfirmMsg("Voulez-vous vraiment modifier la méthode de passation de marché de  ligne N° " & ligne & " ?") = DialogResult.Yes Then
                    Dim NewModMethode As New ModifMethode
                    NewModMethode.IDPlan = CurrentRefPPM
                    NewModMethode.RefMarcheMod = Mid(RefMarche.ToString, 2)
                    NewModMethode.ShowDialog()
                    If NewModMethode.DialogResult = DialogResult.Yes Then
                        RemplirTableauPPM(CurrentRefPPM)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btImprimerPlan_Click(sender As Object, e As EventArgs) Handles btImprimerPlan.Click
        Dim NewModMethode As New ImpressionPlan
        NewModMethode.IDPlan = CurrentRefPPM
        NewModMethode.ShowDialog()
    End Sub
End Class