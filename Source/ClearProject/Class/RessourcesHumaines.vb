Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Math
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid

Module RessourcesHumaines

    Sub RemplirDatagridValidationConge(ByVal mondg As DevExpress.XtraGrid.GridControl, ByVal grid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal requete As String)

        Try
            Dim dtcongeprev As New DataTable
            dtcongeprev.Columns.Clear()
            dtcongeprev.Columns.Add("Choix", Type.GetType("System.Boolean"))
            dtcongeprev.Columns.Add("N°", Type.GetType("System.String"))
            dtcongeprev.Columns.Add("id_emp", Type.GetType("System.String"))
            dtcongeprev.Columns.Add("Nom", Type.GetType("System.String"))
            dtcongeprev.Columns.Add("Prénoms", Type.GetType("System.String"))
            dtcongeprev.Columns.Add("Fonction", Type.GetType("System.String"))
            dtcongeprev.Columns.Add("Nombre de jours ouvrables", Type.GetType("System.String"))
            dtcongeprev.Columns.Add("Montant", Type.GetType("System.String"))
            dtcongeprev.Columns.Add("Date de départ", Type.GetType("System.String"))
            dtcongeprev.Columns.Add("Date de retour", Type.GetType("System.String"))
            dtcongeprev.Columns.Add("Statut", Type.GetType("System.String"))
            dtcongeprev.Columns.Add("id_traitement", Type.GetType("System.String"))
            dtcongeprev.Columns.Add("id_conge", Type.GetType("System.String"))
            dtcongeprev.Rows.Clear()

            Dim cptr As Integer = 0
            Dim dt As DataTable = ExcecuteSelectQuery(requete)
            For Each rw In dt.Rows
                cptr += 1
                Dim drS = dtcongeprev.NewRow()
                drS(0) = False
                drS(1) = cptr
                drS(2) = rw(0).ToString
                drS(3) = MettreApost(rw(1).ToString)
                drS(4) = MettreApost(rw(2).ToString)
                drS(5) = MettreApost(Trim(rw(3).ToString))
                drS(6) = rw(4).ToString
                drS(7) = AfficherMonnaie(rw(5).ToString)
                drS(8) = CDate(rw(6)).ToString("dd/MM/yyyy")
                drS(9) = CDate(rw(7)).ToString("dd/MM/yyyy")
                If rw(10).ToString() = String.Empty Then
                    drS(10) = If(rw(10).ToString() = "", "En cours", rw(10).ToString())
                Else
                    drS(10) = If((rw(10).ToString() = "V0" Or rw(10).ToString() = "Validé" Or rw(10).ToString() = "Validée"), "Validée", "Refusée")
                End If
                drS(11) = rw(11).ToString
                drS(12) = rw(12).ToString
                dtcongeprev.Rows.Add(drS)
            Next


            mondg.DataSource = dtcongeprev
            'GRHCongesPrevoir.LblNombre.Text = "Nbre de Congés Prévu : " & cptr.ToString
            Dim edit As New RepositoryItemCheckEdit
            edit.ValueChecked = True
            edit.ValueUnchecked = False
            'AddHandler edit.CheckedChanged, AddressOf 
            grid.Columns("Choix").ColumnEdit = edit
            grid.OptionsView.ColumnAutoWidth = True
            grid.Columns(0).Width = 20
            grid.Columns(1).Width = 50
            grid.Columns(2).Visible = False
            grid.Columns(3).Width = 200
            grid.Columns(4).Width = 200
            grid.Columns(5).Width = 320
            grid.Columns(6).Width = 130
            grid.Columns(7).Width = 100
            grid.Columns(8).Width = 100
            grid.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            grid.Columns(7).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            grid.Columns(8).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Columns(9).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Columns(10).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Columns(11).Visible = False
            grid.Columns(12).Visible = False
            grid.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
            grid.OptionsBehavior.Editable = True
            grid.OptionsBehavior.ReadOnly = False
            For Each col As DevExpress.XtraGrid.Columns.GridColumn In grid.Columns
                col.OptionsColumn.AllowEdit = False
            Next
            grid.Columns("Choix").OptionsColumn.AllowEdit = True

            grid.OptionsSelection.MultiSelect = True
            grid.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect

        Catch ex As Exception
            SuccesMsg(ex.ToString())
        End Try
    End Sub
    Sub RemplirDatagridPaie(ByVal marekete As String, ByVal mondg As DevExpress.XtraGrid.GridControl, ByRef bull As DevExpress.XtraGrid.Views.Grid.GridView)
        Try

            dtSalaire.Columns.Clear()
            dtSalaire.Columns.Add("N°", Type.GetType("System.String"))
            dtSalaire.Columns.Add("id", Type.GetType("System.String"))
            dtSalaire.Columns.Add("id_emp", Type.GetType("System.String"))
            dtSalaire.Columns.Add("Nom", Type.GetType("System.String"))
            dtSalaire.Columns.Add("Prénoms", Type.GetType("System.String"))
            dtSalaire.Columns.Add("Salaire brut", Type.GetType("System.String"))
            dtSalaire.Columns.Add("ITS", Type.GetType("System.String"))
            dtSalaire.Columns.Add("CN", Type.GetType("System.String"))
            dtSalaire.Columns.Add("IGR", Type.GetType("System.String"))
            dtSalaire.Columns.Add("Prestation Familiale", Type.GetType("System.String"))
            dtSalaire.Columns.Add("Régime Ret.", Type.GetType("System.String"))
            dtSalaire.Columns.Add("Accident de Travail", Type.GetType("System.String"))
            dtSalaire.Columns.Add("Net à Payer", Type.GetType("System.String"))
            dtSalaire.Columns.Add("Date d'édition", Type.GetType("System.String"))
            dtSalaire.Columns.Add("Début Période", Type.GetType("System.String"))
            dtSalaire.Columns.Add("Fin Période", Type.GetType("System.String"))
            dtSalaire.Rows.Clear()

            Dim nligne = 0
            Dim dt As DataTable = ExcecuteSelectQuery(marekete)
            For Each rw In dt.Rows

                Dim drS = dtSalaire.NewRow()
                drS(0) = nligne + 1
                drS(1) = rw(0).ToString
                drS(2) = rw(1).ToString
                drS(3) = MettreApost(rw(2).ToString)
                drS(4) = MettreApost(rw(3).ToString)
                drS(5) = AfficherMonnaie(rw(4).ToString)
                drS(6) = AfficherMonnaie(rw(5).ToString)
                drS(7) = AfficherMonnaie(rw(6).ToString)
                drS(8) = AfficherMonnaie(rw(7).ToString)
                drS(9) = AfficherMonnaie(rw(8).ToString)
                drS(10) = AfficherMonnaie(rw(9).ToString)
                drS(11) = AfficherMonnaie(rw(10).ToString)
                drS(12) = AfficherMonnaie(rw(11).ToString)
                drS(13) = CDate(rw(12)).ToString("dd/MM/yyyy")
                drS(14) = CDate(rw(13)).ToString("dd/MM/yyyy")
                drS(15) = CDate(rw(14)).ToString("dd/MM/yyyy")

                dtSalaire.Rows.Add(drS)
                nligne = nligne + 1

            Next

            mondg.DataSource = dtSalaire
            bull.Columns(0).Visible = False
            bull.Columns(1).Visible = False
            bull.Columns(2).Visible = False
            bull.Columns(3).Width = 150
            bull.Columns(4).Width = 150
            bull.Columns(5).Width = 150
            bull.Columns(6).Width = 150
            bull.Columns(7).Width = 150
            bull.Columns(8).Width = 150
            bull.Columns(9).Width = 150
            bull.Columns(10).Width = 150
            bull.Columns(11).Width = 150
            bull.Columns(12).Width = 150
            bull.Columns(13).Width = 150
            bull.Columns(14).Width = 150
            bull.Columns(15).Width = 150

            bull.Columns(3).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            bull.Columns(4).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Catch ex As Exception
            SuccesMsg("Erreur : Information non disponible : " & ex.ToString)
        End Try
    End Sub
    Sub RemplirDatagridAvenant(ByVal marekete As String, ByVal mondg As DevExpress.XtraGrid.GridControl)
        Try
            dtAvenant.Columns.Clear()
            dtAvenant.Columns.Add("N°", Type.GetType("System.String"))
            dtAvenant.Columns.Add("id Avenant", Type.GetType("System.String"))
            dtAvenant.Columns.Add("Libellé", Type.GetType("System.String"))
            dtAvenant.Columns.Add("Contrat fichier", Type.GetType("System.String"))
            dtAvenant.Columns.Add("Date de début", Type.GetType("System.String"))
            dtAvenant.Columns.Add("Date de fin", Type.GetType("System.String"))
            dtAvenant.Rows.Clear()

            Dim nligne = 0
            Dim dt As DataTable = ExcecuteSelectQuery(marekete)
            For Each rw In dt.Rows

                Dim drS = dtAvenant.NewRow()
                drS(0) = nligne + 1
                drS(1) = rw(0).ToString
                drS(2) = MettreApost(rw(1).ToString)
                drS(3) = MettreApost(rw(2).ToString)
                drS(4) = CDate(rw(3).ToString).ToShortDateString
                If Not IsDBNull(rw(4)) Then
                    drS(5) = CDate(rw(4).ToString).ToShortDateString
                Else
                    drS(5) = rw(4).ToString
                End If
                dtAvenant.Rows.Add(drS)
                nligne = nligne + 1

            Next
            mondg.DataSource = dtAvenant

        Catch ex As Exception
            SuccesMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub
    Public Function MettreAJourCongeEmployes() As Boolean
        query = "select DISTINCT(e.emp_id), e.emp_nom, e.emp_prenoms, f.libellefonction, s.NomService "
        query &= "from t_grh_employe e, t_grh_contrat c, T_Service s, T_Fonction F, t_grh_travailler t "
        query &= "where CONT_STAT_EMP='Employé' AND (e.emp_id=c.emp_id and e.emp_id=t.emp_id and t.CodeService=F.RefFonction and s.CodeService=F.CodeService and t.PosteActu='O') order by e.emp_nom"
        'sql3 = "where CONT_TYPE<>'Stage' AND (CONT_DATE_FIN IS NULL OR CONT_DATE_FIN>='" & dateconvert(Now.ToShortDateString) & "') AND (e.emp_id=c.emp_id and e.emp_id=t.emp_id and t.CodeService=F.RefFonction and s.CodeService=F.CodeService and t.PosteActu='O') and e.EMP_ID not IN(SELECT EMP_ID FROM t_grh_congesprev) order by e.emp_nom"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                Try
                    MettreAJourCongeEmploye(rw("emp_id"))
                Catch ex As Exception
                    FailMsg(ex.ToString())
                End Try

                ''date de depart
                'Dim datedeb As Date
                'Dim ContratID As String = String.Empty
                'query = "select DATE_FORMAT(MAX(CONT_DATE_DEB),'%d-%m-%Y') as CONT_DATE_DEB,CONT_ID from t_grh_contrat where CONT_STAT_EMP='Employé' AND (CONT_DATE_FIN IS NULL OR CONT_DATE_FIN>='" & dateconvert(Now.ToShortDateString) & "') AND emp_id='" & rw(0).ToString & "' GROUP BY CONT_ID"
                'Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                'If dt0.Rows.Count = 0 Then
                '    query = "insert into t_grh_congesprev values (NULL,'" & rw("emp_id") & "','0','0','" & dateconvert(Now.ToShortDateString) & "','" & dateconvert(Now.ToShortDateString) & "','" & ContratID & "')"
                '    Try
                '        ExecuteNonQuery(query)
                '        Continue For
                '    Catch ex As Exception
                '        query = "update t_grh_congesprev set PREV_NBJR='0',PREV_MONT='0',PREV_DATEDEB='" & dateconvert(Now.ToShortDateString) & "',PREV_DATEFIN='" & dateconvert(Now.ToShortDateString) & "', CONT_ID='" & ContratID & "' WHERE EMP_ID='" & rw("emp_id") & "'"
                '        ExecuteNonQuery(query)
                '        Continue For
                '    End Try
                '    'FinChargement()
                '    'MsgBox("L'employé " & MettreApost(rw(1)) & " " & MettreApost(rw(2)) & " n'a pas de contrat de travail valide." & vbNewLine & "Veuillez vérifier ses contrats svp.")
                '    'Return False
                'End If
                'If Not IsDBNull(dt0.Rows(0)(0)) Then
                '    If dt0.Rows(0)(0) = "00-00-0000" Then
                '        Continue For
                '    End If
                'End If
                'For Each rw0 As DataRow In dt0.Rows
                '    datedeb = CDate(rw0(0))
                '    ContratID = rw0(1)
                'Next
                'Dim MaDate As Date = CDate("31/12/" & datedeb.Year)
                'If DateDiff(DateInterval.Day, datedeb, Now.Date) >= MaDate.DayOfYear Then

                '    Dim EmpId As String = ""
                '    Dim DateDepart As Date
                '    Dim DateRetour As Date
                '    Dim Mont As String = ""

                '    Dim jr As String = datedeb.Day.ToString
                '    If jr.Length = 1 Then
                '        jr = "0" & jr
                '    End If
                '    Dim mois As String = datedeb.Month.ToString
                '    If mois.Length = 1 Then
                '        mois = "0" & mois
                '    End If

                '    Dim NbreJour As String


                '    EmpId = rw(0).ToString
                '    Dim res As String() = CalculerCongeEmploye(EmpId)
                '    If res.Length = 5 Then
                '        'Insertion dans la base de données...
                '        NbreJour = res(0)
                '        Mont = res(1)
                '    Else
                '        NbreJour = 0
                '        Mont = 0
                '    End If
                '    'SuccesMsg("Id => " & EmpId & vbNewLine & "NbreJour => " & NbreJour)
                '    DateDepart = CDate(jr.ToString & "/" & mois.ToString & "/" & Now.Date.Year.ToString)
                '    DateRetour = CDate(DateSansJourWeekEnd(DateDepart, CInt(NbreJour)))


                '    'Convert the date to English format
                '    Dim str(3) As String
                '    str = DateDepart.ToShortDateString.Split("/")
                '    Dim tempdt As String = String.Empty
                '    For j As Integer = 2 To 0 Step -1
                '        tempdt += str(j) & "-"
                '    Next
                '    tempdt = tempdt.Substring(0, 10)

                '    str = DateRetour.ToShortDateString.Split("/")
                '    Dim tempdt1 As String = String.Empty
                '    For j As Integer = 2 To 0 Step -1
                '        tempdt1 += str(j) & "-"
                '    Next
                '    tempdt1 = tempdt1.Substring(0, 10)
                '    '

                '    query = "insert into t_grh_congesprev values (NULL,'" & EmpId & "','" & Math.Ceiling(Val(NbreJour.Replace(".", ","))) & "','" & Mont & "','" & tempdt & "','" & tempdt1 & "','" & ContratID & "')"
                '    Try
                '        ExecuteNonQuery(query)
                '    Catch ex As MySqlException

                '        query = "update t_grh_congesprev set PREV_NBJR='" & Math.Ceiling(Val(NbreJour.Replace(".", ","))) & "',PREV_MONT='" & Mont & "',PREV_DATEDEB='" & tempdt & "',PREV_DATEFIN='" & tempdt1 & "', CONT_ID='" & ContratID & "' WHERE EMP_ID='" & EmpId & "'"
                '        'InputBox(0, 1, query)
                '        ExecuteNonQuery(query)
                '    Catch e As Exception
                '        SuccesMsg("Erreur : " & vbNewLine & e.ToString())
                '        Return False
                '    End Try
                'Else
                '    query = "update t_grh_congesprev set PREV_NBJR='0',PREV_MONT='0',PREV_DATEDEB='" & dateconvert(Now.ToShortDateString) & "',PREV_DATEFIN='" & dateconvert(Now.ToShortDateString) & "', CONT_ID='" & ContratID & "' WHERE EMP_ID='" & rw("emp_id") & "'"
                '    'InputBox(0, 1, query)
                '    Try
                '        ExecuteNonQuery(query)

                '    Catch ex As Exception
                '        FailMsg(ex.ToString)
                '    End Try
                'End If
            Next
            Return True
        End If
    End Function
    Public Function MettreAJourCongeEmployes(SendReturn As Boolean) As Boolean
        'query = "TRUNCATE `t_grh_congesprev`"
        'ExecuteNonQuery(query)
        query = "select DISTINCT(e.emp_id), e.emp_nom, e.emp_prenoms, f.libellefonction, s.NomService "
        query &= "from t_grh_employe e, t_grh_contrat c, T_Service s, T_Fonction F, t_grh_travailler t "
        query &= "where CONT_TYPE<>'Stage' AND (CONT_DATE_FIN IS NULL OR CONT_DATE_FIN>='" & dateconvert(Now.ToShortDateString) & "') AND (e.emp_id=c.emp_id and e.emp_id=t.emp_id and t.CodeService=F.RefFonction and s.CodeService=F.CodeService and t.PosteActu='O') and e.EMP_ID not IN(SELECT EMP_ID FROM t_grh_congesprev) order by e.emp_nom"
        dt = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                'date de depart
                Dim datedeb As Date
                Dim ContratID As String = String.Empty
                query = "select DATE_FORMAT(MAX(CONT_DATE_DEB),'%d-%m-%Y') as CONT_DATE_DEB,CONT_ID from t_grh_contrat where CONT_TYPE<>'Stage' AND (CONT_DATE_FIN IS NULL OR CONT_DATE_FIN>='" & dateconvert(Now.ToShortDateString) & "') AND emp_id='" & rw(0).ToString & "' GROUP BY CONT_ID"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                If dt0.Rows.Count = 0 Then
                    FinChargement()
                    'MsgBox("L'employé " & MettreApost(rw(1)) & " " & MettreApost(rw(2)) & " n'a pas de contrat de travail valide." & vbNewLine & "Veuillez vérifier ses contrats svp.")
                    Return False
                End If
                If Not IsDBNull(dt0.Rows(0)(0)) Then
                    If dt0.Rows(0)(0) = "00-00-0000" Then
                        Continue For
                    End If
                End If
                For Each rw0 As DataRow In dt0.Rows
                    datedeb = CDate(rw0(0))
                    ContratID = rw0(1)
                Next
                Dim MaDate As Date = CDate("31/12/" & datedeb.Year)
                If DateDiff(DateInterval.Day, datedeb, Now.Date) >= MaDate.DayOfYear Then

                    Dim EmpId As String = ""
                    Dim DateDepart As Date
                    Dim DateRetour As Date
                    Dim Mont As String = ""

                    Dim jr As String = datedeb.Day.ToString
                    If jr.Length = 1 Then
                        jr = "0" & jr
                    End If
                    Dim mois As String = datedeb.Month.ToString
                    If mois.Length = 1 Then
                        mois = "0" & mois
                    End If

                    Dim NbreJour As String


                    EmpId = rw(0).ToString
                    Dim res As String() = CalculerCongeEmploye(EmpId)
                    If res.Length = 5 Then
                        'Insertion dans la base de données...
                        NbreJour = res(0)
                        Mont = res(1)
                    Else
                        NbreJour = 0
                        Mont = 0
                    End If
                    DateDepart = CDate(jr.ToString & "/" & mois.ToString & "/" & Now.Date.Year.ToString)
                    DateRetour = CDate(DateSansJourWeekEnd(DateDepart, CInt(NbreJour)))


                    'Convert the date to English format
                    Dim str(3) As String
                    str = DateDepart.ToShortDateString.Split("/")
                    Dim tempdt As String = String.Empty
                    For j As Integer = 2 To 0 Step -1
                        tempdt += str(j) & "-"
                    Next
                    tempdt = tempdt.Substring(0, 10)

                    str = DateRetour.ToShortDateString.Split("/")
                    Dim tempdt1 As String = String.Empty
                    For j As Integer = 2 To 0 Step -1
                        tempdt1 += str(j) & "-"
                    Next
                    tempdt1 = tempdt1.Substring(0, 10)
                    query = "insert into t_grh_congesprev values (NULL,'" & EmpId & "','" & Math.Ceiling(Val(NbreJour.Replace(".", ","))) & "','" & Mont & "','" & tempdt & "','" & tempdt1 & "','" & ContratID & "')"
                    Try
                        ExecuteNonQuery(query)
                    Catch ex As MySql.Data.MySqlClient.MySqlException

                    Catch e As Exception
                        'MsgBox("Erreur : " & vbNewLine & e.ToString())
                        Return False
                    End Try
                End If
            Next
            Return True
        End If
    End Function
    Public Function MettreAJourCongeEmploye(EMP_ID As Integer) As Boolean
        query = "DELETE FROM `t_grh_congesprev` WHERE EMP_ID=" & EMP_ID
        ExecuteNonQuery(query)
        query = "select DISTINCT(e.emp_id), e.emp_nom, e.emp_prenoms, f.libellefonction, s.NomService "
        query &= "from t_grh_employe e, t_grh_contrat c, T_Service s, T_Fonction F, t_grh_travailler t "
        query &= "where e.emp_id=" & EMP_ID & " And CONT_TYPE<>'Stage' AND (CONT_DATE_FIN IS NULL OR CONT_DATE_FIN>='" & dateconvert(Now.ToShortDateString) & "') AND (e.emp_id=c.emp_id and e.emp_id=t.emp_id and t.CodeService=F.RefFonction and s.CodeService=F.CodeService and t.PosteActu='O') order by e.emp_nom"
        dt = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                'date de depart
                Dim datedeb As Date
                Dim ContratID As String = String.Empty
                query = "select DATE_FORMAT(MAX(CONT_DATE_DEB),'%d-%m-%Y') as CONT_DATE_DEB,CONT_ID from t_grh_contrat where CONT_TYPE<>'Stage' AND (CONT_DATE_FIN IS NULL OR CONT_DATE_FIN>='" & dateconvert(Now.ToShortDateString) & "') AND emp_id='" & rw(0).ToString & "' GROUP BY CONT_ID"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                If dt0.Rows.Count = 0 Then
                    FinChargement()
                    SuccesMsg("L'employé " & MettreApost(rw(1)) & " " & MettreApost(rw(2)) & " n'a pas de contrat de travail valide." & vbNewLine & "Veuillez vérifier ses contrats svp.")
                    Return False
                End If
                If Not IsDBNull(dt0.Rows(0)(0)) Then
                    If dt0.Rows(0)(0) = "00-00-0000" Then
                        Continue For
                    End If
                End If
                For Each rw0 As DataRow In dt0.Rows
                    datedeb = CDate(rw0(0))
                    ContratID = rw0(1)
                Next
                Dim MaDate As Date = CDate("31/12/" & datedeb.Year)
                If DateDiff(DateInterval.Day, datedeb, Now.Date) >= MaDate.DayOfYear Then

                    Dim EmpId As String = ""
                    Dim DateDepart As Date
                    Dim DateRetour As Date
                    Dim Mont As String = ""

                    Dim jr As String = datedeb.Day.ToString
                    If jr.Length = 1 Then
                        jr = "0" & jr
                    End If
                    Dim mois As String = datedeb.Month.ToString
                    If mois.Length = 1 Then
                        mois = "0" & mois
                    End If

                    Dim NbreJour As String


                    EmpId = rw(0).ToString
                    Dim res As String() = CalculerCongeEmploye(EmpId)
                    If res.Length = 5 Then
                        'Insertion dans la base de données...
                        NbreJour = res(0)
                        Mont = res(1)
                    Else
                        NbreJour = 0
                        Mont = 0
                    End If
                    DateDepart = CDate(jr.ToString & "/" & mois.ToString & "/" & Now.Date.Year.ToString)
                    DateRetour = CDate(DateSansJourWeekEnd(DateDepart, CInt(NbreJour)))


                    'Convert the date to English format
                    Dim str(3) As String
                    str = DateDepart.ToShortDateString.Split("/")
                    Dim tempdt As String = String.Empty
                    For j As Integer = 2 To 0 Step -1
                        tempdt += str(j) & "-"
                    Next
                    tempdt = tempdt.Substring(0, 10)

                    str = DateRetour.ToShortDateString.Split("/")
                    Dim tempdt1 As String = String.Empty
                    For j As Integer = 2 To 0 Step -1
                        tempdt1 += str(j) & "-"
                    Next
                    tempdt1 = tempdt1.Substring(0, 10)
                    query = "insert into t_grh_congesprev values (NULL,'" & EmpId & "','" & NbreJour & "','" & Mont & "','" & tempdt & "','" & tempdt1 & "','" & ContratID & "')"
                    Try
                        ExecuteNonQuery(query)
                    Catch ex As MySql.Data.MySqlClient.MySqlException

                    Catch e As Exception
                        SuccesMsg("Erreur : " & vbNewLine & e.ToString())
                        Return False
                    End Try
                End If
            Next
            Return True
        End If
    End Function
    Public Function CalculerCongeEmploye(ByVal emp_id As String) As Object
        'LastDate doit etre la date de reprise de service

        query = "select DISTINCT(e.emp_id), e.emp_nom, e.emp_prenoms, f.libellefonction, s.NomService "
        query &= "from t_grh_employe e, t_grh_contrat c, T_Service s, T_Fonction F, t_grh_travailler t "
        query &= "where e.emp_id='" & emp_id & "' AND CONT_TYPE<>'Stage' AND (CONT_DATE_FIN IS NULL OR CONT_DATE_FIN>='" & dateconvert(Now.ToShortDateString) & "') AND (e.emp_id=c.emp_id and e.emp_id=t.emp_id and t.CodeService=F.RefFonction and s.CodeService=F.CodeService and t.PosteActu='O') order by e.emp_nom"
        dt = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                'date de depart
                Dim datedeb As Date
                Dim ContratID As String = String.Empty
                query = "select DATE_FORMAT(MAX(CONT_DATE_DEB),'%d-%m-%Y') as CONT_DATE_DEB, CONT_ID from t_grh_contrat where CONT_TYPE<>'Stage' AND (CONT_DATE_FIN IS NULL OR CONT_DATE_FIN>='" & dateconvert(Now.ToShortDateString) & "') AND emp_id='" & emp_id & "' group by CONT_ID"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                If dt0.Rows.Count = 0 Then
                    FinChargement()
                    Return {"Votre contrat n'a pas été enregistré.", "0", "0", "0", "0", "0"}
                End If
                If Not IsDBNull(dt0.Rows(0)(0)) Then
                    If dt0.Rows(0)(0) = "00-00-0000" Then
                        Continue For
                    End If
                Else

                End If
                For Each rw0 As DataRow In dt0.Rows
                    datedeb = CDate(rw0(0))
                    ContratID = rw0(1)
                Next
                Dim MaDate As Date = CDate("31/12/" & datedeb.Year)
                If DateDiff(DateInterval.Day, datedeb, Now.Date) >= MaDate.DayOfYear Then

                    Dim EmpId As String = emp_id
                    Dim DateDepart As Date
                    Dim DateRetour As Date
                    Dim Mont As String = ""

                    Dim jr As String = datedeb.Day.ToString
                    If jr.Length = 1 Then
                        jr = "0" & jr
                    End If
                    Dim mois As String = datedeb.Month.ToString
                    If mois.Length = 1 Then
                        mois = "0" & mois
                    End If

                    EmpId = rw(0).ToString
                    DateDepart = CDate(jr.ToString & "/" & mois.ToString & "/" & Now.Date.Year.ToString)

                    'nombre de jour ouvrable
                    Dim nbmois As Decimal = DateDiff(DateInterval.Day, datedeb, Now.Date) / 30
                    Dim NbreJour As String = Ceiling(nbmois * 2.2)

                    'date de fin
                    DateRetour = CDate(DateSansJourWeekEnd(DateDepart, CInt(NbreJour)))

                    'categorie et salaire de bases
                    Dim id_cat = ""
                    query = "select cat_id from t_grh_appcat where emp_id='" & rw(0).ToString & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    If dt0.Rows.Count = 0 Then
                        FinChargement()
                        Return {"Le systéme n'a pas pu obtenir votre catégorie salariale.", "0", "0", "0", "0", "0"}
                    End If

                    For Each rw0 As DataRow In dt0.Rows
                        id_cat = rw0(0).ToString()
                    Next

                    Dim SalBase As Double = 0
                    query = "select cat_sal_base,cat_lib from t_grh_categorie where cat_id='" & id_cat.ToString & "'"
                    dt0 = ExcecuteSelectQuery(query)
                    For Each rw0 As DataRow In dt0.Rows
                        If IsDBNull(rw0(0)) Or Len(rw0(0)) = 0 Then
                            FinChargement()
                            Return {"Le système n'a pas pu récupérer votre salaire de base.", "0", "0", "0", "0", "0"}
                        End If
                        SalBase = rw0(0)
                    Next

                    'montant des congés
                    NbreJour += JoursSupplementaires(emp_id)

                    NbreJour = Math.Ceiling(Val(NbreJour)).ToString

                    ''Retranche du nombre de jour les congés déjà pris
                    'query = "select cong_nbjr, cong_mont from t_grh_conges where emp_id='" & EmpId & "' AND CONG_ETAT<>'Refusé'"
                    'Dim dt = ExcecuteSelectQuery(query)
                    'Dim NbJrCong = 0
                    'For Each rwConge As DataRow In dt.Rows
                    '    NbJrCong += rw("cong_nbjr")
                    'Next
                    'NbreJour -= NbJrCong

                    Dim rep As String() = MontantConge(emp_id, NbreJour)
                    If rep.Length = 2 And rep(0) = "True" Then
                        Mont = rep(1)
                    Else
                        Mont = 0
                    End If

                    query = "SELECT `CONG_ID`,`EMP_ID`,`CONG_NBJR`,`CONG_MONT`,DATE_FORMAT(`CONG_DATEDEB`,'%d-%m-%Y') AS CONG_DATEDEB,DATE_FORMAT(`CONG_DATEFIN`,'%d-%m-%Y') AS CONG_DATEFIN,DATE_FORMAT(`CONG_DATE_DEMANDE`,'%d-%m-%Y') AS CONG_DATE_DEMANDE,`CONG_ETAT`,`CONT_ID` FROM `t_grh_conges` where CONT_ID='" & ContratID & "' and CONG_ETAT<>'Refusé' order by CONG_DATE_DEMANDE asc"
                    dt0 = ExcecuteSelectQuery(query)
                    If dt0.Rows.Count > 0 Then
                        'Dim lastDate As Date
                        For Each rw0 As DataRow In dt0.Rows
                            NbreJour -= rw0("CONG_NBJR")
                            'lastDate = CDate(rw0("CONG_DATE_DEMANDE"))
                        Next

                        If NbreJour >= 0 Then
                            'Calcul des valeurs
                            NbreJour = Math.Ceiling(Val(NbreJour)).ToString

                            NbreJour += JoursSupplementaires(emp_id)
                            rep = MontantConge(emp_id, NbreJour)
                            If rep.Length = 2 And rep(0) = "True" Then
                                Mont = rep(1)
                            Else
                                Mont = 0
                            End If
                            DateRetour = DateAdd(DateInterval.Day, CInt(NbreJour), Now.Date)
                            'Retour des valeurs calculés.
                            Return {NbreJour, Mont, Now.ToShortDateString(), DateRetour.ToShortDateString(), ContratID}
                        Else
                            FinChargement()
                            Return {"Vous ne pouvez pas demander de congé actuellement.", "0", "0", Now.ToShortDateString(), Now.ToShortDateString(), "-1"}
                            '& vbNewLine & "{{" & NbreJour & "},{" & Mont & "},{" & Now.ToShortDateString() & "},{" & DateRetour.ToShortDateString() & "},{" & ContratID & "}}"
                        End If
                    End If

                    Return {NbreJour, Mont, DateDepart.ToShortDateString(), DateRetour.ToShortDateString(), ContratID}
                Else
                    FinChargement()
                    Return {"Vous ne pouvez pas demander de congé actuellement.", "0", "0", Now.ToShortDateString(), Now.ToShortDateString(), "-1"}
                End If
            Next
        Else
            FinChargement()
            Return {"Vous n'avez pas de contrat enregistré ou vous n'êtes pas encore affecté à un poste.", "0", "0", "0", "0", "0"}
        End If
    End Function
    Public Function GetSuperieurEmpID(EmpID As Integer) As Object
        '-----------STRUCTURE DU TABLEU RETOURNE (Index value) 
        'Tim.Dev +225 779-419-09
        'En cas d'erreur Le table aura la longeur 1
        '0 -> "Trouvé" ou Le message d'erreur
        '1 -> Le ID du supérieur
        '2 -> Le Matricule du supérieur
        '3 -> Le Nom du supérieur
        '4 -> Le Prénom du supérieur
        '5 -> Le Sexe du supérieur
        '6 -> La Référence de la fonction qu'occupe son supérieur
        '7 -> Le code de la fonction qu'occupe son supérieur
        '8 -> Le Nom de la fonction qu'occupe son supérieur
        '9 -> Le Code Operateur du supérieur

        query = "SELECT CodeService FROM `t_grh_travailler` where EMP_ID=" & EmpID & " and PosteActu='O'" 'Recherche du code de son poste actuel
        Dim dt As DataTable = ExcecuteSelectQuery(query)

        If dt.Rows.Count > 0 Then
            query = "SELECT CodeBoss FROM `t_fonction` where RefFonction=" & dt.Rows(0).Item(0) 'Recherche du Code de la fonction de son superieur
            Dim CodeBoss As String = ExecuteScallar(query)

            If Val(CodeBoss) = 0 And dt.Rows.Count > 1 Then
                Return {"Le découpage administratif n'est pas bien défini, car le système a détecté plusieurs supérieurs administratifs indépendants." & vbNewLine & "Veuillez vérifier votre découpage administratif svp."}
            End If

            If Val(CodeBoss) = 0 Then 'On a le 1er responsable du projet.

                query = "SELECT * FROM `t_fonction` where RefFonction=" & CInt(dt.Rows(0).Item(0)) 'Recherche des infos de la fonction de son superieur
                dt = ExcecuteSelectQuery(query)

                query = "SELECT * FROM `t_grh_employe` where EMP_ID=" & EmpID 'Récupération des informations de l'employé.
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                query = "SELECT * FROM t_operateur where EMP_ID=" & EmpID
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                If dt1.Rows.Count > 0 Then
                    If dt1.Rows.Count > 1 Then
                        Return {"Votre supérieur a pas plus de 1 accès à l'application."}
                    Else
                        Return {"Trouvé", dt0.Rows(0).Item("EMP_ID").ToString, dt0.Rows(0).Item("EMP_MAT").ToString, dt0.Rows(0).Item("EMP_NOM").ToString, dt0.Rows(0).Item("EMP_PRENOMS").ToString, dt0.Rows(0).Item("EMP_SEXE").ToString, dt.Rows(0).Item("RefFonction").ToString, dt.Rows(0).Item("CodeFonction").ToString, dt.Rows(0).Item("LibelleFonction").ToString, dt1.Rows(0).Item("CodeOperateur").ToString}
                    End If
                Else
                    Return {"Votre supérieur n'est pas défini comme opérateur."}
                End If
            Else
                query = "SELECT * FROM `t_fonction` where RefFonction=" & CInt(CodeBoss) 'Recherche des infos de la fonction de son superieur
                dt = ExcecuteSelectQuery(query)

                If dt.Rows.Count > 0 Then
                    query = "SELECT DISTINCT(EMP_ID) FROM `t_grh_travailler` where CodeService=" & dt.Rows(0).Item("RefFonction") & " And PosteActu='O'" 'Recherche l'employé qui occupe ce poste.
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                    If dt0.Rows.Count > 0 Then
                        If dt0.Rows.Count > 1 Then
                            Return {"Le système a trouvé plusieurs employés au poste de " & MettreApost(dt.Rows(0).Item("LibelleFonction")) & " qui le supérieur de l'employé."}
                        Else
                            query = "SELECT * FROM `t_grh_employe` where EMP_ID=" & dt0.Rows(0).Item(0) 'Récupération des informations de l'employé qui occupe ce poste.
                            dt0 = ExcecuteSelectQuery(query)
                            If dt0.Rows.Count > 0 Then
                                query = "SELECT * FROM t_operateur where EMP_ID=" & dt0.Rows(0).Item(0)
                                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                                If dt1.Rows.Count > 0 Then
                                    If dt1.Rows.Count > 1 Then
                                        Return {"Votre supérieur a pas plus de 1 accès à l'application."}
                                    Else
                                        Return {"Trouvé", dt0.Rows(0).Item("EMP_ID").ToString, dt0.Rows(0).Item("EMP_MAT").ToString, dt0.Rows(0).Item("EMP_NOM").ToString, dt0.Rows(0).Item("EMP_PRENOMS").ToString, dt0.Rows(0).Item("EMP_SEXE").ToString, dt.Rows(0).Item("RefFonction").ToString, dt.Rows(0).Item("CodeFonction").ToString, dt.Rows(0).Item("LibelleFonction").ToString, dt1.Rows(0).Item("CodeOperateur").ToString}
                                    End If
                                Else
                                    Return {"Votre supérieur n'est pas défini comme opérateur."}
                                End If
                            Else
                                Return {"L'employé qui occupait le poste de " & MettreApost(dt.Rows(0).Item("LibelleFonction").ToString) & " a été supprimé."}
                            End If
                        End If
                    Else
                        Return {"Aucun employé occupe le poste de " & MettreApost(dt.Rows(0).Item("LibelleFonction").ToString) & " qui est défini comme votre supérieur."}
                    End If
                Else
                    Return {"Le poste de votre supérieur n'a pas été configuré."}
                End If
            End If
        Else
            Return {"Vous n'a pas de poste actuel."}
        End If
    End Function
    Public Function GetDRHInfo() As Object
        '-----------STRUCTURE DU TABLEU RETOURNE (Index value) 
        'Tim.Dev +225 779-419-09
        'En cas d'erreur Le table aura la longeur 1
        '0 -> "Trouvé" ou Le message d'erreur
        '1 -> Le ID du DRH
        '2 -> Le Matricule du DRH
        '3 -> Le Nom du DRH
        '4 -> Le Prénom du DRH
        '5 -> Le Sexe du DRH
        '6 -> La Référence de la fonction qu'occupe le DRH
        '7 -> Le code de la fonction qu'occupe le DRH
        '8 -> Le Nom de la fonction qu'occupe le DRH
        '9 -> Le Code Operateur le DRH
        Dim dt As DataTable
        If Val(ExecuteScallar("SELECT COUNT(*) FROM t_grh_param_dem_conge")) > 0 Then
            query = "SELECT DrhEmpID FROM t_grh_param_dem_conge"
            Dim DRHEmpID As Decimal = CDec(ExecuteScallar(query))
            If DRHEmpID > -1 Then
                query = "SELECT * FROM `t_grh_employe` where EMP_ID='" & DRHEmpID & "'" 'Récupération des informations du Directeur des ressources humaines.
                dt = ExcecuteSelectQuery(query)
                If dt.Rows.Count > 0 Then
                    query = "SELECT * FROM t_operateur where EMP_ID='" & DRHEmpID & "' LIMIT 1"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    If dt1.Rows.Count > 0 Then
                        Dim InfoEmp As DataRow = GetEmpInfo(DRHEmpID)
                        query = "SELECT * FROM t_fonction WHERE RefFonction='" & GetEmpInfo(DRHEmpID)("FONCTION_ID") & "'"
                        Dim dtInfoFonction As DataTable = ExcecuteSelectQuery(query)
                        If dtInfoFonction.Rows.Count = 0 Then
                            Return {"L'employé " & MettreApost(dt.Rows(0)("EMP_NOM") & " " & dt.Rows(0)("EMP_PRENOMS")).Trim() & " n'est pas défini à un poste.", "-1"}
                        End If
                        Dim InfoFonction As DataRow = dtInfoFonction.Rows(0)
                        Return {"Trouvé", dt.Rows(0).Item("EMP_ID").ToString, dt.Rows(0).Item("EMP_MAT").ToString, dt.Rows(0).Item("EMP_NOM").ToString, dt.Rows(0).Item("EMP_PRENOMS").ToString, dt.Rows(0).Item("EMP_SEXE").ToString, InfoFonction("RefFonction"), InfoFonction("CodeFonction").ToString, MettreApost(InfoFonction("LibelleFonction").ToString), dt1.Rows(0).Item("CodeOperateur").ToString}
                    Else
                        Return {"L'employé " & MettreApost(dt.Rows(0)("EMP_NOM") & " " & dt.Rows(0)("EMP_PRENOMS")).Trim() & " n'a pas d'accès à l'application ou n'est pas lié à son compte d'accès.", "-1"}
                    End If
                Else
                    Return {"Il semble que l'employé a été supprimé du personnel.", "-1"}
                End If
            End If
        End If
        Return {"Veuillez définir l'employé qui est chargé de gérer les congés dans les paramètres.", "-1"}



        query = "SELECT RefDecoupAdmin FROM `t_divisionadministrative` where LibelleDivision LIKE '%Ressources Humaines%' OR LibelleDivision LIKE '%Ressources Humaine%'" 'Recherche du code de la direction des ressources humaines
        dt = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            If dt.Rows.Count > 1 Then
                Return {"Nous avons trouvé plusieurs directions des Ressources Humaines" & vbNewLine & "Veuillez consulter le plan administratif svp.", "-1"}
            Else
                query = "SELECT CodeService FROM `t_service` where (NomService LIKE '%Ressources Humaines%' OR NomService LIKE '%Ressources Humaine%') AND RefDecoupAdmin=" & dt.Rows(0)(0) 'Recherche du code du PERSONNEL RATTACHÉ aux ressources humaines
                dt = ExcecuteSelectQuery(query)

                query = "SELECT * FROM `t_fonction` where (LibelleFonction LIKE 'Chef%' OR LibelleFonction LIKE 'Directeur%' OR LibelleFonction LIKE 'Directrice' OR LibelleFonction LIKE 'Specialiste%' OR LibelleFonction LIKE 'Spécialiste%' OR LibelleFonction LIKE 'Chargé%' OR LibelleFonction LIKE 'Chargée%' OR LibelleFonction LIKE 'Responsable%') AND CodeService=" & dt.Rows(0)(0) 'Recherche du code du directeur des ressources humaines
                dt = ExcecuteSelectQuery(query)

                query = "SELECT DISTINCT(EMP_ID) FROM `t_grh_travailler` where CodeService=" & dt.Rows(0).Item("RefFonction") & " And PosteActu='O'" 'Recherche l'employé qui occupe ce poste de Directeur des ressources humaines.
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)

                If dt0.Rows.Count > 0 Then
                    If dt0.Rows.Count > 1 Then
                        Return {"Le système a trouvé plusieurs employés au poste de " & MettreApost(dt.Rows(0).Item("LibelleFonction")) & ".", "-1"}
                    Else
                        query = "SELECT * FROM `t_grh_employe` where EMP_ID=" & dt0.Rows(0).Item(0) 'Récupération des informations du Directeur des ressources humaines.
                        dt0 = ExcecuteSelectQuery(query)
                        If dt0.Rows.Count > 0 Then
                            query = "SELECT * FROM t_operateur where EMP_ID=" & dt0.Rows(0).Item(0)
                            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                            If dt1.Rows.Count > 0 Then
                                If dt1.Rows.Count > 1 Then
                                    Return {"Le " & MettreApost(dt.Rows(0).Item("LibelleFonction")) & " a pas plus de 1 accès à l'application.", "-1"}
                                Else
                                    Return {"Trouvé", dt0.Rows(0).Item("EMP_ID").ToString, dt0.Rows(0).Item("EMP_MAT").ToString, dt0.Rows(0).Item("EMP_NOM").ToString, dt0.Rows(0).Item("EMP_PRENOMS").ToString, dt0.Rows(0).Item("EMP_SEXE").ToString, dt.Rows(0).Item("RefFonction").ToString, dt.Rows(0).Item("CodeFonction").ToString, dt.Rows(0).Item("LibelleFonction").ToString, dt1.Rows(0).Item("CodeOperateur").ToString}
                                End If
                            Else
                                Return {MettreApost(dt.Rows(0).Item("LibelleFonction").ToString) & " n'est pas défini comme opérateur.", "-1"}
                            End If
                        Else
                            Return {"L'employé qui occupait le poste de " & MettreApost(dt.Rows(0).Item("LibelleFonction")) & " a été supprimé.", "-1"}
                        End If
                    End If
                Else
                    Return {"Aucun employé occupe le poste de " & MettreApost(dt.Rows(0).Item("LibelleFonction").ToString) & ".", "-1"}
                End If
            End If
        Else
            query = "SELECT CodeService FROM `t_service` where NomService LIKE '%Ressources Humaines%' OR NomService LIKE '%Ressources Humaine%'" 'Recherche d'un service des ressources humaines
            dt = ExcecuteSelectQuery(query)
            If dt.Rows.Count > 0 Then
                If dt.Rows.Count > 1 Then
                    Return {"Nous avons trouvé plusieurs services des Ressources Humaines" & vbNewLine & "Veuillez consulter les paramètres du projet svp.", "-1"}
                Else
                    query = "SELECT * FROM `t_fonction` where (LibelleFonction LIKE 'Chef%' OR LibelleFonction LIKE 'Directeur%' OR LibelleFonction LIKE 'Directrice' OR LibelleFonction LIKE 'Specialiste%' OR LibelleFonction LIKE 'Spécialiste%' OR LibelleFonction LIKE 'Chargé%' OR LibelleFonction LIKE 'Chargée%' OR LibelleFonction LIKE 'Responsable%') AND CodeService=" & dt.Rows(0)(0) 'Recherche du code du directeur des ressources humaines
                    dt = ExcecuteSelectQuery(query)
                    If dt.Rows.Count = 0 Then
                        Return {"Nous n'avons pas pu trouver le poste du responsable  des Ressources Humaines.", "-1"}
                    End If
                    query = "SELECT DISTINCT(EMP_ID) FROM `t_grh_travailler` where CodeService=" & dt.Rows(0).Item("RefFonction") & " And PosteActu='O'" 'Recherche l'employé qui occupe ce poste de Directeur des ressources humaines.
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)

                    If dt0.Rows.Count > 0 Then
                        If dt0.Rows.Count > 1 Then
                            Return {"Le système a trouvé plusieurs employés au poste de " & MettreApost(dt.Rows(0).Item("LibelleFonction")) & ".", "-1"}
                        Else
                            query = "SELECT * FROM `t_grh_employe` where EMP_ID=" & dt0.Rows(0).Item(0) 'Récupération des informations du Directeur des ressources humaines.
                            dt0 = ExcecuteSelectQuery(query)
                            If dt0.Rows.Count > 0 Then
                                query = "SELECT * FROM t_operateur where EMP_ID=" & dt0.Rows(0).Item(0)
                                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                                If dt1.Rows.Count > 0 Then
                                    If dt1.Rows.Count > 1 Then
                                        Return {"Le " & MettreApost(dt.Rows(0).Item("LibelleFonction")) & " a pas plus de 1 accès à l'application.", "-1"}
                                    Else
                                        Return {"Trouvé", dt0.Rows(0).Item("EMP_ID").ToString, dt0.Rows(0).Item("EMP_MAT").ToString, dt0.Rows(0).Item("EMP_NOM").ToString, dt0.Rows(0).Item("EMP_PRENOMS").ToString, dt0.Rows(0).Item("EMP_SEXE").ToString, dt.Rows(0).Item("RefFonction").ToString, dt.Rows(0).Item("CodeFonction").ToString, dt.Rows(0).Item("LibelleFonction").ToString, dt1.Rows(0).Item("CodeOperateur").ToString}
                                    End If
                                Else
                                    Return {MettreApost(dt.Rows(0).Item("LibelleFonction").ToString) & " n'est pas défini comme opérateur.", "-1"}
                                End If
                            Else
                                Return {"L'employé qui occupait le poste de " & MettreApost(dt.Rows(0).Item("LibelleFonction")) & " a été supprimé.", "-1"}
                            End If
                        End If
                    Else
                        Return {"Aucun employé occupe le poste de " & MettreApost(dt.Rows(0).Item("LibelleFonction").ToString) & ".", "-1"}
                    End If
                End If
            Else
                Return {"Nous n'avons pas pu trouver une direction ou un service des Ressources Humaines.", "-1"}
            End If
        End If
    End Function
    Public Function DemandeCongeMessage(NbJourTotal As Integer, NbrJourDemande As Integer, DateDepart As String, DateRetour As String, Optional Sexe As Char = "M"c) As String
        If CDate(DateRetour).DayOfWeek = DayOfWeek.Monday Then
            DateRetour = CDate(DateRetour).AddDays(-3).ToShortDateString()
        Else
            DateRetour = CDate(DateRetour).AddDays(-1).ToShortDateString()
        End If
        Dim str As String = "<?xml version=""1.0"" encoding=""utf-8""?><?mso-application progid=""Word.Document""?>" &
             "<w:wordDocument xml:space=""preserve"" xmlns:w=""http://schemas.microsoft.com/office/word/2003/wordml"">" &
             "<w:lists /><w:styles><w:style w:type=""paragraph"" w:styleId=""P0"" w:default=""on""><w:name w:val=""Normal"" />" &
             "<w:pPr /><w:rPr /></w:style><w:style w:type=""character"" w:styleId=""C0"" w:default=""on"">" &
             "<w:name w:val=""Default Paragraph Font"" /><w:semiHidden w:val=""on"" /><w:rPr /></w:style>" &
             "<w:style w:type=""character"" w:styleId=""C1""><w:name w:val=""Line Number"" /><w:basedOn w:val=""C0"" />" &
             "<w:semiHidden w:val=""on"" /><w:rPr /></w:style><w:style w:type=""character"" w:styleId=""C2"">" &
             "<w:name w:val=""Hyperlink"" /><w:rPr><w:color w:val=""0000FF"" /><w:u w:val=""single"" /></w:rPr>" &
             "</w:style><w:style w:type=""table"" w:styleId=""T0"" w:default=""on""><w:name w:val=""Normal Table"" />" &
             "<w:tblPr><w:tblCellMar><w:top w:w=""0"" w:type=""dxa"" /><w:left w:w=""108"" w:type=""dxa"" />" &
             "<w:bottom w:w=""0"" w:type=""dxa"" /><w:right w:w=""108"" w:type=""dxa"" /></w:tblCellMar></w:tblPr>" &
             "</w:style><w:style w:type=""table"" w:styleId=""T1""><w:name w:val=""Table Simple 1"" /><w:basedOn w:val=""T0"" />" &
             "<w:tblPr><w:tblBorders><w:bottom w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:insideH w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:insideV w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:left w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:right w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:top w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /></w:tblBorders><w:tblCellMar><w:left w:w=""108"" w:type=""dxa"" /><w:right w:w=""108"" " &
             "w:type=""dxa"" /></w:tblCellMar></w:tblPr></w:style></w:styles><w:docPr><w:autoHyphenation w:val=""off"" />" &
             "<w:defaultTabStop w:val=""720"" /><w:evenAndOddHeaders w:val=""off"" /></w:docPr><w:body><wx:sect " &
             "xmlns:wx=""http://schemas.microsoft.com/office/word/2003/auxHint""><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>" &
             "Bonjour " & IIf(Sexe = "M"c, "Monsieur", "Madame") & ",</w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t></w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>A la date du " & Now.ToShortDateString() & ", j'ai acquis </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>" & IIf(Len(NbJourTotal.ToString()) = 1, "0" & NbJourTotal, NbJourTotal) & "</w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> jours de </w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>congés annuels</w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> au titre de l'année " & Now.Year & ".</w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>Je souhaiterais " &
             "prendre </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>" & IIf(Len(NbrJourDemande.ToString()) = 1, "0" & NbrJourDemande, NbrJourDemande) & "</w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> de ces jours pour la période allant du </w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>" & DateDepart.Replace("-", "/") & "</w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> au </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>" & DateRetour.Replace("-", "/") & "</w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> inclus.</w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t></w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>Par la présente, je sollicite " &
             "votre accord.</w:t></w:r><w:r><w:rPr><w:color w:val=""000000"" /></w:rPr>" &
             "<w:t xml:space=""preserve""> </w:t></w:r></w:p><w:sectPr><w:type w:val=""next-page"" /></w:sectPr></wx:sect>" &
             "</w:body></w:wordDocument>"
        Return str
    End Function
    Public Function DemandeCongeMessageSuperieur(DateDemande As String, NbJourTotal As Integer, NbrJourDemande As Integer, DateDepart As String, DateRetour As String, EmpID As Integer, Optional SexeSup As Char = "M"c) As String
        query = "SELECT * FROM `t_grh_employe` where EMP_ID=" & EmpID  'Récupération des informations de l'employé qui a fait la demande
        dt = ExcecuteSelectQuery(query)
        Dim rw As DataRow = dt.Rows(0)

        query = "SELECT * FROM `t_fonction` where RefFonction=(select CodeService from t_grh_travailler where EMP_ID=" & EmpID & " And PosteActu ='O')" 'Récupération des informations du poste de l'employé qui a fait la demande
        dt = ExcecuteSelectQuery(query)
        Dim rwPoste As DataRow = dt.Rows(0)
        query = "SELECT * FROM `t_service` where CodeService=" & rwPoste("Codeservice")
        dt = ExcecuteSelectQuery(query)
        Dim rwService As DataRow = dt.Rows(0)

        If CDate(DateRetour).DayOfWeek = DayOfWeek.Monday Then
            DateRetour = CDate(DateRetour).AddDays(-3).ToShortDateString()
        Else
            DateRetour = CDate(DateRetour).AddDays(-1).ToShortDateString()
        End If

        Dim str As String = "<?xml version=""1.0"" encoding=""utf-8""?><?mso-application progid=""Word.Document""?>" &
             "<w:wordDocument xml:space=""preserve"" xmlns:w=""http://schemas.microsoft.com/office/word/2003/wordml"">" &
             "<w:lists /><w:styles><w:style w:type=""paragraph"" w:styleId=""P0"" w:default=""on""><w:name w:val=""Normal"" />" &
             "<w:pPr /><w:rPr /></w:style><w:style w:type=""character"" w:styleId=""C0"" w:default=""on"">" &
             "<w:name w:val=""Default Paragraph Font"" /><w:semiHidden w:val=""on"" /><w:rPr /></w:style>" &
             "<w:style w:type=""character"" w:styleId=""C1""><w:name w:val=""Line Number"" /><w:basedOn w:val=""C0"" />" &
             "<w:semiHidden w:val=""on"" /><w:rPr /></w:style><w:style w:type=""character"" w:styleId=""C2"">" &
             "<w:name w:val=""Hyperlink"" /><w:rPr><w:color w:val=""0000FF"" /><w:u w:val=""single"" /></w:rPr>" &
             "</w:style><w:style w:type=""table"" w:styleId=""T0"" w:default=""on""><w:name w:val=""Normal Table"" />" &
             "<w:tblPr><w:tblCellMar><w:top w:w=""0"" w:type=""dxa"" /><w:left w:w=""108"" w:type=""dxa"" />" &
             "<w:bottom w:w=""0"" w:type=""dxa"" /><w:right w:w=""108"" w:type=""dxa"" /></w:tblCellMar></w:tblPr>" &
             "</w:style><w:style w:type=""table"" w:styleId=""T1""><w:name w:val=""Table Simple 1"" /><w:basedOn w:val=""T0"" />" &
             "<w:tblPr><w:tblBorders><w:bottom w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:insideH w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:insideV w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:left w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:right w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:top w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /></w:tblBorders><w:tblCellMar><w:left w:w=""108"" w:type=""dxa"" /><w:right w:w=""108"" " &
             "w:type=""dxa"" /></w:tblCellMar></w:tblPr></w:style></w:styles><w:docPr><w:autoHyphenation w:val=""off"" />" &
             "<w:defaultTabStop w:val=""720"" /><w:evenAndOddHeaders w:val=""off"" /></w:docPr><w:body><wx:sect " &
             "xmlns:wx=""http://schemas.microsoft.com/office/word/2003/auxHint""><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>" &
             "Bonjour " & IIf(SexeSup = "M"c, "Monsieur", "Madame") & ",</w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t></w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>A la date du " & CDate(DateDemande).ToShortDateString() & ", " &
             IIf(rw("EMP_SEXE") = "M"c, "M.", "Mme") & " " & Trim(rw("EMP_NOM") & " " & rw("EMP_PRENOMS")) & " au poste de " &
             MettreApost(rwPoste("LibelleFonction")) & " du " & MettreApost(rwService("NomService")) & ", a acquis </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>" & IIf(Len(NbJourTotal.ToString()) = 1, "0" & NbJourTotal, NbJourTotal) & "</w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> jours de </w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>congés annuels</w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> au titre de l'année " & CDate(DateDemande).Year & ".</w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t></w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>" & IIf(rw("EMP_SEXE") = "M"c, "Il", "Elle") & " souhaiterais " &
             "prendre </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>" & IIf(Len(NbrJourDemande.ToString()) = 1, "0" & NbrJourDemande, NbrJourDemande) & "</w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> de ces jours pour la période allant du </w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>" & DateDepart.Replace("-", "/") & "</w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> au </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>" & DateRetour.Replace("-", "/") & " </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>inclus.</w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t> </w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>Par la présente, je sollicite " &
             "votre accord.</w:t></w:r><w:r><w:rPr><w:color w:val=""000000"" /></w:rPr>" &
             "<w:t xml:space=""preserve""> </w:t></w:r></w:p><w:sectPr><w:type w:val=""next-page"" /></w:sectPr></wx:sect>" &
             "</w:body></w:wordDocument>"
        Return str
    End Function
    Public Function DemandeCongeMessageGRH(DateDemande As String, NbJourTotal As Integer, NbrJourDemande As Integer, DateDepart As String, DateRetour As String, EmpID As Integer, Optional SexeSup As Char = "M"c) As String
        query = "SELECT * FROM `t_grh_employe` where EMP_ID=" & EmpID  'Récupération des informations de l'employé qui a fait la demande
        dt = ExcecuteSelectQuery(query)
        Dim rw As DataRow = dt.Rows(0)

        query = "SELECT * FROM `t_fonction` where RefFonction=(select CodeService from t_grh_travailler where EMP_ID=" & EmpID & " And PosteActu ='O')" 'Récupération des informations du poste de l'employé qui a fait la demande
        dt = ExcecuteSelectQuery(query)
        Dim rwPoste As DataRow = dt.Rows(0)
        query = "SELECT * FROM `t_service` where CodeService=" & rwPoste("Codeservice")
        dt = ExcecuteSelectQuery(query)
        Dim rwService As DataRow = dt.Rows(0)

        If CDate(DateRetour).DayOfWeek = DayOfWeek.Monday Then
            DateRetour = CDate(DateRetour).AddDays(-3).ToShortDateString()
        Else
            DateRetour = CDate(DateRetour).AddDays(-1).ToShortDateString()
        End If

        Dim str As String = "<?xml version=""1.0"" encoding=""utf-8""?><?mso-application progid=""Word.Document""?>" &
            "<w:wordDocument xml:space=""preserve"" xmlns:w=""http://schemas.microsoft.com/office/word/2003/wordml"">" &
             "<w:lists /><w:styles><w:style w:type=""paragraph"" w:styleId=""P0"" w:default=""on""><w:name w:val=""Normal"" />" &
             "<w:pPr /><w:rPr /></w:style><w:style w:type=""character"" w:styleId=""C0"" w:default=""on"">" &
             "<w:name w:val=""Default Paragraph Font"" /><w:semiHidden w:val=""on"" /><w:rPr /></w:style>" &
             "<w:style w:type=""character"" w:styleId=""C1""><w:name w:val=""Line Number"" /><w:basedOn w:val=""C0"" />" &
             "<w:semiHidden w:val=""on"" /><w:rPr /></w:style><w:style w:type=""character"" w:styleId=""C2"">" &
             "<w:name w:val=""Hyperlink"" /><w:rPr><w:color w:val=""0000FF"" /><w:u w:val=""single"" /></w:rPr>" &
             "</w:style><w:style w:type=""table"" w:styleId=""T0"" w:default=""on""><w:name w:val=""Normal Table"" />" &
             "<w:tblPr><w:tblCellMar><w:top w:w=""0"" w:type=""dxa"" /><w:left w:w=""108"" w:type=""dxa"" />" &
             "<w:bottom w:w=""0"" w:type=""dxa"" /><w:right w:w=""108"" w:type=""dxa"" /></w:tblCellMar></w:tblPr>" &
             "</w:style><w:style w:type=""table"" w:styleId=""T1""><w:name w:val=""Table Simple 1"" /><w:basedOn w:val=""T0"" />" &
             "<w:tblPr><w:tblBorders><w:bottom w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:insideH w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:insideV w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:left w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:right w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:top w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /></w:tblBorders><w:tblCellMar><w:left w:w=""108"" w:type=""dxa"" /><w:right w:w=""108"" " &
             "w:type=""dxa"" /></w:tblCellMar></w:tblPr></w:style></w:styles><w:docPr><w:autoHyphenation w:val=""off"" />" &
             "<w:defaultTabStop w:val=""720"" /><w:evenAndOddHeaders w:val=""off"" /></w:docPr><w:body><wx:sect " &
             "xmlns:wx=""http://schemas.microsoft.com/office/word/2003/auxHint""><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>" &
             "Bonjour " & IIf(SexeSup = "M"c, "Monsieur", "Madame") & ",</w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t></w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>A la date du " & CDate(DateDemande).ToShortDateString() & ", " &
             IIf(rw("EMP_SEXE") = "M"c, "M.", "Mme") & " " & Trim(rw("EMP_NOM") & " " & rw("EMP_PRENOMS")) & " au poste de " &
             MettreApost(rwPoste("LibelleFonction")) & " du " & MettreApost(rwService("NomService")) & ", a acquis </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>" & IIf(Len(NbJourTotal.ToString()) = 1, "0" & NbJourTotal, NbJourTotal) & "</w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> jours de </w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>congé annuel</w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> au titre de l'année " & Now.Year & ".</w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t></w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>" & IIf(rw("EMP_SEXE") = "M"c, "Il", "Elle") & " souhaiterais " &
             "prendre </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>" & IIf(Len(NbrJourDemande.ToString()) = 1, "0" & NbrJourDemande, NbrJourDemande) & "</w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> de ces jours pour la période allant du </w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>" & DateDepart.Replace("-", "/") & "</w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> au </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>" & DateRetour.Replace("-", "/") & " </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>inclus.</w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t></w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>Par la présente, veuillez " & IIf(SexeSup = "M"c, "Monsieur", "Madame") &
             " accorder à cet employé ces congés pour valoir ce que de droit.</w:t></w:r><w:r><w:rPr><w:color w:val=""000000"" /></w:rPr>" &
             "<w:t xml:space=""preserve""> </w:t></w:r></w:p><w:sectPr><w:type w:val=""next-page"" /></w:sectPr></wx:sect>" &
             "</w:body></w:wordDocument>"
        Return str
    End Function
    Public Function DemandeCongeMessageGRHDG(DateDemande As String, NbJourTotal As Integer, NbrJourDemande As Integer, DateDepart As String, DateRetour As String, EmpID As Integer, Optional SexeSup As Char = "M"c) As String
        query = "SELECT * FROM `t_grh_employe` where EMP_ID=" & EmpID  'Récupération des informations de l'employé qui a fait la demande
        dt = ExcecuteSelectQuery(query)
        Dim rw As DataRow = dt.Rows(0)

        query = "SELECT * FROM `t_fonction` where RefFonction=(select CodeService from t_grh_travailler where EMP_ID=" & EmpID & " And PosteActu ='O')" 'Récupération des informations du poste de l'employé qui a fait la demande
        dt = ExcecuteSelectQuery(query)
        Dim rwPoste As DataRow = dt.Rows(0)
        query = "SELECT * FROM `t_service` where CodeService=" & rwPoste("Codeservice")
        dt = ExcecuteSelectQuery(query)
        Dim rwService As DataRow = dt.Rows(0)

        If CDate(DateRetour).DayOfWeek = DayOfWeek.Monday Then
            DateRetour = CDate(DateRetour).AddDays(-3).ToShortDateString()
        Else
            DateRetour = CDate(DateRetour).AddDays(-1).ToShortDateString()
        End If

        Dim str As String = "<?xml version=""1.0"" encoding=""utf-8""?><?mso-application progid=""Word.Document""?>" &
            "<w:wordDocument xml:space=""preserve"" xmlns:w=""http://schemas.microsoft.com/office/word/2003/wordml"">" &
             "<w:lists /><w:styles><w:style w:type=""paragraph"" w:styleId=""P0"" w:default=""on""><w:name w:val=""Normal"" />" &
             "<w:pPr /><w:rPr /></w:style><w:style w:type=""character"" w:styleId=""C0"" w:default=""on"">" &
             "<w:name w:val=""Default Paragraph Font"" /><w:semiHidden w:val=""on"" /><w:rPr /></w:style>" &
             "<w:style w:type=""character"" w:styleId=""C1""><w:name w:val=""Line Number"" /><w:basedOn w:val=""C0"" />" &
             "<w:semiHidden w:val=""on"" /><w:rPr /></w:style><w:style w:type=""character"" w:styleId=""C2"">" &
             "<w:name w:val=""Hyperlink"" /><w:rPr><w:color w:val=""0000FF"" /><w:u w:val=""single"" /></w:rPr>" &
             "</w:style><w:style w:type=""table"" w:styleId=""T0"" w:default=""on""><w:name w:val=""Normal Table"" />" &
             "<w:tblPr><w:tblCellMar><w:top w:w=""0"" w:type=""dxa"" /><w:left w:w=""108"" w:type=""dxa"" />" &
             "<w:bottom w:w=""0"" w:type=""dxa"" /><w:right w:w=""108"" w:type=""dxa"" /></w:tblCellMar></w:tblPr>" &
             "</w:style><w:style w:type=""table"" w:styleId=""T1""><w:name w:val=""Table Simple 1"" /><w:basedOn w:val=""T0"" />" &
             "<w:tblPr><w:tblBorders><w:bottom w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:insideH w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:insideV w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:left w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:right w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:top w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /></w:tblBorders><w:tblCellMar><w:left w:w=""108"" w:type=""dxa"" /><w:right w:w=""108"" " &
             "w:type=""dxa"" /></w:tblCellMar></w:tblPr></w:style></w:styles><w:docPr><w:autoHyphenation w:val=""off"" />" &
             "<w:defaultTabStop w:val=""720"" /><w:evenAndOddHeaders w:val=""off"" /></w:docPr><w:body><wx:sect " &
             "xmlns:wx=""http://schemas.microsoft.com/office/word/2003/auxHint""><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>" &
             "Bonjour " & IIf(SexeSup = "M"c, "Monsieur", "Madame") & ",</w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t></w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>A la date du " & CDate(DateDemande).ToShortDateString() &
             ", j 'ai acquis </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>" & IIf(Len(NbJourTotal.ToString()) = 1, "0" & NbJourTotal, NbJourTotal) & "</w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> jours de </w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>congés annuels</w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> au titre de l'année " & Now.Year & ".</w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t></w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>Je souhaiterais " &
             "prendre </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>" & IIf(Len(NbrJourDemande.ToString()) = 1, "0" & NbrJourDemande, NbrJourDemande) & "</w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> de ces jours pour la période allant du </w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>" & DateDepart.Replace("-", "/") & "</w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> au </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>" & DateRetour.Replace("-", "/") & " </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>inclus.</w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t></w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>Par la présente, veuillez " & IIf(SexeSup = "M"c, "Monsieur", "Madame") &
             " m'accorder mes congés pour valoir ce que de droit.</w:t></w:r><w:r><w:rPr><w:color w:val=""000000"" /></w:rPr>" &
             "<w:t xml:space=""preserve""> </w:t></w:r></w:p><w:sectPr><w:type w:val=""next-page"" /></w:sectPr></wx:sect>" &
             "</w:body></w:wordDocument>"
        Return str
    End Function
    Public Function RefusDemandeConge(Motif As String, DateDemande As String, NbrJourDemande As Integer, DateDepart As String, DateRetour As String, EmpID As Integer, Optional SexeEmp As Char = "M"c) As String
        query = "SELECT * FROM `t_grh_employe` where EMP_ID=" & EmpID  'Récupération des informations de l'employé qui a fait la demande
        dt = ExcecuteSelectQuery(query)
        Dim rw As DataRow = dt.Rows(0)

        If CDate(DateRetour).DayOfWeek = DayOfWeek.Monday Then
            DateRetour = CDate(DateRetour).AddDays(-3).ToShortDateString()
        Else
            DateRetour = CDate(DateRetour).AddDays(-1).ToShortDateString()
        End If

        Dim str As String = "<?xml version=""1.0"" encoding=""utf-8""?><?mso-application progid=""Word.Document""?>" &
             "<w:wordDocument xml:space=""preserve"" xmlns:w=""http://schemas.microsoft.com/office/word/2003/wordml"">" &
             "<w:lists /><w:styles><w:style w:type=""paragraph"" w:styleId=""P0"" w:default=""on""><w:name w:val=""Normal"" />" &
             "<w:pPr /><w:rPr /></w:style><w:style w:type=""character"" w:styleId=""C0"" w:default=""on"">" &
             "<w:name w:val=""Default Paragraph Font"" /><w:semiHidden w:val=""on"" /><w:rPr /></w:style>" &
             "<w:style w:type=""character"" w:styleId=""C1""><w:name w:val=""Line Number"" /><w:basedOn w:val=""C0"" />" &
             "<w:semiHidden w:val=""on"" /><w:rPr /></w:style><w:style w:type=""character"" w:styleId=""C2"">" &
             "<w:name w:val=""Hyperlink"" /><w:rPr><w:color w:val=""0000FF"" /><w:u w:val=""single"" /></w:rPr>" &
             "</w:style><w:style w:type=""table"" w:styleId=""T0"" w:default=""on""><w:name w:val=""Normal Table"" />" &
             "<w:tblPr><w:tblCellMar><w:top w:w=""0"" w:type=""dxa"" /><w:left w:w=""108"" w:type=""dxa"" />" &
             "<w:bottom w:w=""0"" w:type=""dxa"" /><w:right w:w=""108"" w:type=""dxa"" /></w:tblCellMar></w:tblPr>" &
             "</w:style><w:style w:type=""table"" w:styleId=""T1""><w:name w:val=""Table Simple 1"" /><w:basedOn w:val=""T0"" />" &
             "<w:tblPr><w:tblBorders><w:bottom w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:insideH w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:insideV w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:left w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:right w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:top w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /></w:tblBorders><w:tblCellMar><w:left w:w=""108"" w:type=""dxa"" /><w:right w:w=""108"" " &
             "w:type=""dxa"" /></w:tblCellMar></w:tblPr></w:style></w:styles><w:docPr><w:autoHyphenation w:val=""off"" />" &
             "<w:defaultTabStop w:val=""720"" /><w:evenAndOddHeaders w:val=""off"" /></w:docPr><w:body><wx:sect " &
             "xmlns:wx=""http://schemas.microsoft.com/office/word/2003/auxHint""><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>" &
             "Bonjour " & IIf(SexeEmp = "M"c, "Monsieur", "Madame") & ",</w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t></w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>En reponse à votre demande de congé " &
             "payé du " & CDate(DateDemande).ToShortDateString() & " pour une période de </w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" /><w:i w:val=""off"" />" &
             "<w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>" &
             IIf(Len(NbrJourDemande.ToString()) = 1, "0" & NbrJourDemande, NbrJourDemande) & "</w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> jours, allant du </w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>" & DateDepart.Replace("-", "/") & "</w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> au </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>" & DateRetour.Replace("-", "/") & " </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>inclus, je tiens à vous informer que je ne peux malheureusement accéder " &
             "à votre demande.</w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t></w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>En effet, " & Motif & "</w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t></w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>En espérant pouvoir répondre positivement à " &
             "votre prochaine demande, je vous prie de bien vouloir agréer, " & IIf(SexeEmp = "M"c, "Monsieur", "Madame") & ", " &
             "l'expression de mes salutations distinguées.</w:t></w:r><w:r><w:rPr><w:color w:val=""000000"" /></w:rPr>" &
             "<w:t xml:space=""preserve""> </w:t></w:r></w:p><w:sectPr><w:type w:val=""next-page"" /></w:sectPr></wx:sect>" &
             "</w:body></w:wordDocument>"
        Return str
    End Function
    Public Function RefusDemandeCongeSuperieur(Motif As String, DateDemande As String, NbrJourDemande As Integer, DateDepart As String, DateRetour As String, EmpID As Integer, EmpIDSup As Integer) As String
        query = "SELECT * FROM `t_grh_employe` where EMP_ID=" & EmpID  'Récupération des informations de l'employé qui a fait la demande
        dt = ExcecuteSelectQuery(query)
        Dim rw As DataRow = dt.Rows(0)

        query = "SELECT * FROM `t_fonction` where RefFonction=(select CodeService from t_grh_travailler where EMP_ID=" & EmpID & " And PosteActu ='O')" 'Récupération des informations du poste de l'employé qui a fait la demande
        dt = ExcecuteSelectQuery(query)
        Dim rwPoste As DataRow = dt.Rows(0)
        query = "SELECT * FROM `t_service` where CodeService=" & rwPoste("Codeservice")
        dt = ExcecuteSelectQuery(query)
        Dim rwService As DataRow = dt.Rows(0)

        query = "SELECT * FROM `t_grh_employe` where EMP_ID=" & EmpIDSup  'Récupération des informations du supérieur
        dt = ExcecuteSelectQuery(query)
        Dim rwSup As DataRow = dt.Rows(0)

        If CDate(DateRetour).DayOfWeek = DayOfWeek.Monday Then
            DateRetour = CDate(DateRetour).AddDays(-3).ToShortDateString()
        Else
            DateRetour = CDate(DateRetour).AddDays(-1).ToShortDateString()
        End If

        Dim str As String = "<?xml version=""1.0"" encoding=""utf-8""?><?mso-application progid=""Word.Document""?>" &
             "<w:wordDocument xml:space=""preserve"" xmlns:w=""http://schemas.microsoft.com/office/word/2003/wordml"">" &
             "<w:lists /><w:styles><w:style w:type=""paragraph"" w:styleId=""P0"" w:default=""on""><w:name w:val=""Normal"" />" &
             "<w:pPr /><w:rPr /></w:style><w:style w:type=""character"" w:styleId=""C0"" w:default=""on"">" &
             "<w:name w:val=""Default Paragraph Font"" /><w:semiHidden w:val=""on"" /><w:rPr /></w:style>" &
             "<w:style w:type=""character"" w:styleId=""C1""><w:name w:val=""Line Number"" /><w:basedOn w:val=""C0"" />" &
             "<w:semiHidden w:val=""on"" /><w:rPr /></w:style><w:style w:type=""character"" w:styleId=""C2"">" &
             "<w:name w:val=""Hyperlink"" /><w:rPr><w:color w:val=""0000FF"" /><w:u w:val=""single"" /></w:rPr>" &
             "</w:style><w:style w:type=""table"" w:styleId=""T0"" w:default=""on""><w:name w:val=""Normal Table"" />" &
             "<w:tblPr><w:tblCellMar><w:top w:w=""0"" w:type=""dxa"" /><w:left w:w=""108"" w:type=""dxa"" />" &
             "<w:bottom w:w=""0"" w:type=""dxa"" /><w:right w:w=""108"" w:type=""dxa"" /></w:tblCellMar></w:tblPr>" &
             "</w:style><w:style w:type=""table"" w:styleId=""T1""><w:name w:val=""Table Simple 1"" /><w:basedOn w:val=""T0"" />" &
             "<w:tblPr><w:tblBorders><w:bottom w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:insideH w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:insideV w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:left w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:right w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /><w:top w:val=""single"" w:sz=""4"" w:space=""0"" w:shadow=""off"" w:frame=""off"" " &
             "w:color=""000000"" /></w:tblBorders><w:tblCellMar><w:left w:w=""108"" w:type=""dxa"" /><w:right w:w=""108"" " &
             "w:type=""dxa"" /></w:tblCellMar></w:tblPr></w:style></w:styles><w:docPr><w:autoHyphenation w:val=""off"" />" &
             "<w:defaultTabStop w:val=""720"" /><w:evenAndOddHeaders w:val=""off"" /></w:docPr><w:body><wx:sect " &
             "xmlns:wx=""http://schemas.microsoft.com/office/word/2003/auxHint""><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>" &
             "Bonjour " & IIf(rwSup("EMP_SEXE") = "M"c, "Monsieur", "Madame") & ",</w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t></w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>En reponse de la demande de congé annuel de " & IIf(rw("EMP_SEXE") = "M"c, "M.", "Mme") & " " & Trim(rw("EMP_NOM") & " " & rw("EMP_PRENOMS")) & " au poste de " &
             MettreApost(rwPoste("LibelleFonction")) & " du " & MettreApost(rwService("NomService")) & " émis le " & CDate(DateDemande).ToShortDateString() & " pour une période de </w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" /><w:i w:val=""off"" />" &
             "<w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>" &
             IIf(Len(NbrJourDemande.ToString()) = 1, "0" & NbrJourDemande, NbrJourDemande) & "</w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> jours, allant du </w:t></w:r>" &
             "<w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>" & DateDepart.Replace("-", "/") & "</w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t> au </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""on"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>" & DateRetour.Replace("-", "/") & " </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=""arial"" w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" />" &
             "<w:i w:val=""off"" /><w:color w:val=""222222"" /><w:highlight w:val=""white"" /><w:u w:val=""none"" />" &
             "<w:strike w:val=""off"" /></w:rPr><w:t>inclus, je tiens à vous informer que je ne peux malheureusement" &
             " accéder à cette demande.</w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t></w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>En effet, " & Motif & "</w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t></w:t></w:r></w:p><w:p><w:r><w:rPr><w:rFonts w:ascii=""arial"" " &
             "w:h-ansi=""arial"" /><w:sz w:val=""24"" /><w:b w:val=""off"" /><w:i w:val=""off"" /><w:color w:val=""222222"" />" &
             "<w:highlight w:val=""white"" /><w:u w:val=""none"" /><w:strike w:val=""off"" /></w:rPr><w:t>En espérant pouvoir répondre positivement à la prochaine demande, je " &
             "vous prie de bien vouloir agréer, " & IIf(rwSup("EMP_SEXE") = "M"c, "Monsieur", "Madame") & ", l'expression " &
             "de mes salutations distinguées.</w:t></w:r><w:r><w:rPr><w:color w:val=""000000"" /></w:rPr>" &
             "<w:t xml:space=""preserve""> </w:t></w:r></w:p><w:sectPr><w:type w:val=""next-page"" /></w:sectPr></wx:sect>" &
             "</w:body></w:wordDocument>"
        Return str
    End Function
    Public Function CalculSalaire(ByVal id_emp As String, ByVal datedeb As Date, ByVal datefin As Date) As String
        Dim BUL_ID As Decimal = 0
        Try
            Dim str(3) As String
            str = datedeb.ToShortDateString().Split("/")
            Dim tempdt As String = String.Empty
            For j As Integer = 2 To 0 Step -1
                tempdt += str(j) & "-"
            Next
            tempdt = tempdt.Substring(0, 10)

            Dim str1(3) As String
            str1 = datefin.ToShortDateString().Split("/")
            Dim tempdt1 As String = String.Empty
            For j As Integer = 2 To 0 Step -1
                tempdt1 += str1(j) & "-"
            Next
            tempdt1 = tempdt1.Substring(0, 10)

            Dim str2(3) As String
            str2 = CDate(Now).ToString("dd/MM/yyyy").Split("/")
            Dim tempdt2 As String = String.Empty
            For j As Integer = 2 To 0 Step -1
                tempdt2 += str2(j) & "-"
            Next
            tempdt2 = tempdt2.Substring(0, 10)

            'Déclaration des variables
            Dim NbreJourPaie As Integer = 0
            Dim NbreHeurePaie As Double = 173.33
            Dim NbreHeureLegal As Double = 173.33
            Dim NbreJourTravail As Integer = 0
            Dim NbreHeureTravail As Double = 173.33
            Dim TauxHoraire As Double = 0.0
            Dim SalCategoriel As Double = 0.0
            Dim SalContrat As Double = 0.0
            Dim SurSalaire As Double = 0.0
            Dim SalBase As Double = 0.0
            Dim SalBrut As Double = 0.0
            Dim SalBrutConge As Double = 0.0
            Dim SalNAP As Double = 0.0
            Dim SalReel As Double = 0.0
            Dim SalBrutImposable As Double = 0.0
            Dim SalBrutImposableConge As Double = 0.0
            Dim SalBrutSocial As Double = 0.0
            Dim SalBrutSocialConge As Double = 0.0
            Dim SalBrutFacultatif As Double = 0.0
            Dim SalBrutFacultatifConge As Double = 0.0
            Dim SitMatri As String = String.Empty
            Dim NbreEnfant As Integer = 0
            Dim TypeContrat As String = String.Empty
            Dim TypeRenumeration As String = String.Empty
            Dim DateDebContrat As Date
            Dim DateFinContrat As Object
            'Heures supplementaires et les retenues
            Dim MontantHeureSup As Double = 0
            Dim HS_41_46H As Double = 0
            Dim HS_47_55H As Double = 0
            Dim HS_NJO As Double = 0
            Dim HS_JDJF As Double = 0
            Dim HS_NDJF As Double = 0
            Dim Acompte As Double = 0
            Dim Avance As Double = 0
            Dim Opposition As Double = 0
            Dim NbreHeureAbsence As Double = 0
            'Taux des taxes et impots
            Dim TauxIts As Double = 0
            Dim TauxAppren As Double = 0
            Dim TauxForm As Double = 0
            Dim TauxPrestFam As Double = 0
            Dim TauxAccTrav As Double = 0
            Dim Tauxcne As Double = 0
            'Avantages et Accessoires
            Dim PrimAncien As Double = 0
            Dim Accessoires As New DataTable
            Dim Avantages As New DataTable
            Dim MontantCongeAnnuel As Double = 0.0
            Dim MontantAccessoireImpossable As Double = 0.0
            Dim MontantAccessoireNonImpossable As Double = 0.0
            Dim MontantAvantage As Double = 0.0
            Dim NbreJourConge As Double = 0
            Dim NbreHeureConge As Double = 0

            Dim MontantTotalPret As Double = 0

            query = "select EMP_ID, CONCAT(EMP_NOM,' ',EMP_PRENOMS) as NomPrens, emp_situat, emp_nb_enf, EMP_SEXE, EMP_CNAM,EMP_EMAIL,IgnorerPaie,EMP_RETRAITE from t_grh_employe where EMP_ID=" & id_emp
            Dim dtEmploye As DataTable = ExcecuteSelectQuery(query)
            If dtEmploye.Rows.Count < 1 Then
                Exit Function
                'Return "L'employé avec le ID n° " & id_emp & " n'existe pas!"
            End If
            Dim rwEmploye As DataRow = dtEmploye.Rows(0)

            'Check si l'employe disponible
            If Not DisponibiliteEmp(rwEmploye("EMP_ID"), datefin) Then
                Exit Function
            End If

            'Check si l'employe doit etre ignore dans la paie
            If CBool(rwEmploye("IgnorerPaie")) = True Then
                Exit Function
            End If

            'Requete pour identifier le dernier contrat de travail de l'employe
            query = "select * from t_grh_contrat where EMP_ID=" & rwEmploye("EMP_ID") & " AND CONT_STAT_EMP='Employé' AND (CONT_DATE_FIN IS NULL OR CONT_DATE_FIN>='" & tempdt1 & "' OR (EXTRACT(YEAR_MONTH FROM CONT_DATE_FIN)=EXTRACT(YEAR_MONTH FROM '" & tempdt1 & "'))) AND (CONT_DATE_DEB<='" & tempdt & "' OR (EXTRACT(YEAR_MONTH FROM CONT_DATE_DEB)=EXTRACT(YEAR_MONTH FROM '" & tempdt & "'))) ORDER BY `CONT_DATE_DEB` DESC"
            'query = "select * from t_grh_contrat where EMP_ID=" & rwEmploye("EMP_ID") & " AND cont_date_deb<='" & tempdt & "' and CONT_TYPE<>'Stage' AND (CONT_DATE_FIN IS NULL OR (CONT_DATE_FIN BETWEEN '" & tempdt & "' AND '" & tempdt1 & "') OR CONT_DATE_FIN>='" & tempdt1 & "') ORDER BY `CONT_DATE_DEB` DESC"
            Dim dtContrat As DataTable = ExcecuteSelectQuery(query)
            If dtContrat.Rows.Count < 1 Then
                Return "L'employé " & MettreApost(Trim(rwEmploye("NomPrens"))) & " n'a pas de contrat de travail en cours."
            End If

            'Requete pour identifier le dernier contrat de travail de l'employe
            query = "select EMP_ID from t_grh_travailler where PosteActu='O' And EMP_ID=" & rwEmploye("EMP_ID")
            Dim dtWorkPlace As DataTable = ExcecuteSelectQuery(query)
            If dtWorkPlace.Rows.Count = 0 Then
                Return "L'employé " & MettreApost(Trim(rwEmploye("NomPrens"))) & " n'est affecté à aucun poste."
            End If

            Dim rwContrat As DataRow = dtContrat.Rows(0)

            'Situation matrimoniale
            SitMatri = rwEmploye("emp_situat")

            'Nombre d'enfant
            NbreEnfant = rwEmploye("emp_nb_enf")

            'Nature du contrat
            TypeContrat = rwContrat("CONT_STAT_EMP")

            'Type du montant du contrat
            TypeRenumeration = rwContrat("cont_mont_typ")

            'Date de debut de contrat
            DateDebContrat = CDate(rwContrat("CONT_DATE_DEB"))

            'Date de fin de contrat
            If IsDBNull(rwContrat("CONT_DATE_FIN")) Then
                DateFinContrat = Nothing
            Else
                DateFinContrat = CDate(rwContrat("CONT_DATE_FIN"))
            End If

            If TypeContrat = "Employé" Then

                'Montrant enregistrer dans le contrat
                SalContrat = IIf(Len(rwContrat("cont_mont_renum")) = 0, 0, rwContrat("cont_mont_renum"))

                Dim id_cat = Val(ExecuteScallar("select cat_id from t_grh_appcat where emp_id=" & rwEmploye("EMP_ID") & " ORDER BY `APPCAT_DATE` DESC LIMIT 1"))

                If id_cat = 0 Then
                    Return "L'employé " & MettreApost(Trim(rwEmploye("NomPrens"))) & " n'a pas de salaire de base"
                End If

                'Vérifier si l'employé occupe un poste
                Dim dtVerif As DataTable = ExcecuteSelectQuery("select TRAV_ID from t_grh_travailler where emp_id=" & rwEmploye("EMP_ID") & " and PosteActu='O'")
                If dtVerif.Rows.Count = 0 Then
                    Return "L'employé " & MettreApost(Trim(rwEmploye("NomPrens"))) & " n'a pas encore de poste défini."
                End If

                'Récupération du salaire catégoriel
                SalCategoriel = Val(ExecuteScallar("select cat_sal_base from t_grh_categorie where cat_id=" & id_cat))
                If SalCategoriel = 0 Then
                    Return "L'employé " & MettreApost(Trim(rwEmploye("NomPrens"))) & " n'a pas de salaire de base"
                End If

                'Sursalaire
                If (SalContrat - SalCategoriel) < 0 Then
                    Return "Le salaire catégoriel de l'employé " & MettreApost(Trim(rwEmploye("NomPrens"))) & " est supérieur au salaire saisi dans son contrat en cours."
                End If


                'Calcul salaire Horaire
                NbreJourPaie = DateDiff(DateInterval.Day, datedeb, datefin)
                NbreJourPaie += 1
                If NbreJourPaie > 27 Then
                    NbreJourPaie = 30
                Else
                    NbreHeurePaie = (NbreHeureLegal * NbreJourPaie) / 30
                End If


                Try
                    If Not DateFinContrat = Nothing Then
                        If CDate(DateFinContrat) >= datefin Then
                            NbreJourTravail = DateDiff(DateInterval.Day, DateDebContrat, datefin)
                        Else
                            NbreJourTravail = DateDiff(DateInterval.Day, datedeb, DateFinContrat)
                        End If
                    Else
                        NbreJourTravail = DateDiff(DateInterval.Day, DateDebContrat, datefin)
                    End If
                    NbreJourTravail += 1
                    'Dim str3 As String = rwEmploye("NomPrens") & " Worked Day => " & NbreJourTravail
                    'str3 += vbNewLine & rwEmploye("NomPrens") & " datedeb => " & datedeb & " ; datefin => " & datefin
                    'Return str3
                Catch ex As Exception
                    Return "Error "
                End Try

                If NbreJourTravail >= NbreJourPaie And NbreJourTravail >= 28 Then
                    NbreJourTravail = NbreJourPaie
                    NbreHeureTravail = (NbreHeureLegal * NbreJourTravail) / 30
                ElseIf NbreJourTravail >= 27 Then
                    NbreJourTravail = NbreJourPaie
                    NbreHeureTravail = (NbreHeureLegal * NbreJourTravail) / 30
                ElseIf NbreJourTravail > 0 Then
                    NbreHeureTravail = (NbreHeureLegal * NbreJourTravail) / 30
                Else
                    Exit Function
                End If

                If TypeRenumeration.ToUpper() = "BRUT" Then

                    query = "select * from t_grh_salaire where emp_id='" & id_emp.ToString & "' and SAL_DATEFIN='" & tempdt1 & "'"
                    Dim dt As DataTable = ExcecuteSelectQuery(query)
                    If dt.Rows.Count = 1 Then
                        For Each rw As DataRow In dt.Rows
                            Acompte = CDbl(rw("SAL_ACCOMPTE"))
                            Avance = CDbl(rw("SAL_AVCE"))
                            Opposition = CDbl(rw("SAL_OPP"))
                            NbreHeureAbsence = CDbl(rw("SAL_NBRHEUREABS"))
                        Next

                        query = "select * from t_grh_heuresup where SAL_ID='" & dt.Rows(0)(0).ToString() & "'"
                        dt = ExcecuteSelectQuery(query)
                        For Each rw As DataRow In dt.Rows
                            HS_41_46H = CDbl(rw("HS_41_46H"))
                            HS_47_55H = CDbl(rw("HS_47_55H"))
                            HS_NJO = CDbl(rw("HS_NJO"))
                            HS_JDJF = CDbl(rw("HS_JDJF"))
                            HS_NDJF = CDbl(rw("HS_NDJF"))
                        Next

                    End If

                    'Retranche les absences des heures de travail
                    NbreHeureTravail -= NbreHeureAbsence

                    'Récupération des Accessoires de l'employé
                    query = "SELECT T.ACCESS_ID, ACCESS_LIB, Montant, Imposable FROM t_grh_employe_accessoire T, t_grh_accessoire A where A.ACCESS_ID=T.ACCESS_ID AND T.EMP_ID=" & id_emp
                    dt = ExcecuteSelectQuery(query)
                    Accessoires.Columns.Add("Libelle", Type.GetType("System.String"))
                    Accessoires.Columns.Add("Montant", Type.GetType("System.String"))
                    Accessoires.Columns.Add("Imposable", Type.GetType("System.String"))
                    Accessoires.Columns.Add("Exoneration", Type.GetType("System.String"))
                    For Each rw As DataRow In dt.Rows
                        Dim MontantExo As Decimal = 0
                        If CBool(rw("Imposable")) = True Then
                            'Verification des exonerations
                            Dim cpte As Integer = Val(ExecuteScallar("select count(*) from t_grh_accessoire_exoneration where ACCESS_ID=" & rw("ACCESS_ID")))
                            If cpte > 0 Then
                                query = "select Zone from t_grh_travailler where TRAV_ID=" & GetEmpInfo(Val(rwEmploye("EMP_ID"))).Item("TRAV_ID")
                                Dim CodeZoneAffectation As Integer = Val(ExecuteScallar(query))
                                query = "select * from t_grh_accessoire_exoneration where ACCESS_ID=" & rw("ACCESS_ID") & " And CodeZone=" & CodeZoneAffectation
                                Dim dtExoneration As DataTable = ExcecuteSelectQuery(query)
                                If dtExoneration.Rows.Count > 0 Then
                                    'Il y a une exoneration particuliere pour la localite du service
                                    MontantExo = CDec(dtExoneration.Rows(0).Item("Montant"))
                                Else
                                    query = "select * from t_grh_accessoire_exoneration where ACCESS_ID=" & rw("ACCESS_ID") & " And TypeExo='All'"
                                    dtExoneration = ExcecuteSelectQuery(query)
                                    If dtExoneration.Rows.Count > 0 Then
                                        'Il y a une exoneration pour le reste des localites, a donc a appliquer pour ce service
                                        MontantExo = CDec(dtExoneration.Rows(0).Item("Montant"))
                                        'Le Else est pour dire qu'il n'y a pas d'exoneration pour le reste des couvertures
                                    End If
                                End If
                                If MontantExo <> 0 Then
                                    If BackToTauxHoraire(CDbl(rw("Montant")), NbreHeureTravail) > MontantExo Then
                                        MontantAccessoireImpossable += BackToTauxHoraire(CDbl(rw("Montant")) - MontantExo, NbreHeureTravail)
                                        MontantAccessoireNonImpossable += MontantExo
                                    Else
                                        MontantAccessoireNonImpossable += BackToTauxHoraire(CDbl(rw("Montant")), NbreHeureTravail)
                                    End If
                                Else
                                    MontantAccessoireImpossable += BackToTauxHoraire(CDbl(rw("Montant")), NbreHeureTravail)
                                End If

                            Else
                                'L'accessoire n'est pas exonere
                                MontantAccessoireImpossable += BackToTauxHoraire(CDbl(rw("Montant")), NbreHeureTravail)
                            End If
                        Else
                            'L'accessoire n'est pas imposable
                            MontantAccessoireNonImpossable += BackToTauxHoraire(CDbl(rw("Montant")), NbreHeureTravail)
                        End If
                        Accessoires.Rows.Add(rw("ACCESS_LIB"), BackToTauxHoraire(CDbl(rw("Montant")), NbreHeureTravail), rw("Imposable"), MontantExo)
                    Next

                    query = "select CodeService from t_grh_travailler where PosteActu='O' and EMP_ID=" & rwEmploye("EMP_ID")
                    dt = ExcecuteSelectQuery(query)
                    Avantages.Columns.Add("Libelle", Type.GetType("System.String"))
                    Avantages.Columns.Add("Montant", Type.GetType("System.String"))
                    If dt.Rows.Count = 1 Then
                        'Récupération des Avantages lié au poste de l'employé
                        query = "SELECT TYPEAVAN_LIB,Montant FROM t_grh_type_avantage T, t_grh_avantage A where A.TYPEAVAN_ID=T.TYPEAVAN_ID AND A.RefFonction=" & dt.Rows(0)(0)
                        dt = ExcecuteSelectQuery(query)
                        For Each rw As DataRow In dt.Rows
                            Avantages.Rows.Add(rw("TYPEAVAN_LIB"), BackToTauxHoraire(CDbl(rw("Montant")), NbreHeureTravail))
                            MontantAvantage += BackToTauxHoraire(CDbl(rw("Montant")), NbreHeureTravail)
                        Next
                    End If

                    'Calcul du sursalaire
                    SurSalaire = SalContrat - SalCategoriel
                    'Application horaire
                    SurSalaire = BackToTauxHoraire(SurSalaire, NbreHeureTravail)

                    '*********************************************************************************************************
                    'Calcul du salaire
                    '*********************************************************************************************************

                    'Prime d'anciennete
                    Dim nbrjour = DateDiff(DateInterval.Day, DateDebContrat, datedeb) '365 jours * 2 = 730
                    Dim nb_an = (nbrjour \ 365)
                    If nb_an >= 2 And nb_an <= 25 Then
                        PrimAncien = (SalCategoriel * nb_an) / 100
                    ElseIf nb_an > 25 Then
                        PrimAncien = (SalCategoriel * 25) / 100
                    End If
                    'Dim stro As String = rwEmploye("NomPrens") & " P.A => " & PrimAncien & vbNewLine &
                    '     rwEmploye("NomPrens") & " Nbre Jour => " & nbrjour & vbNewLine &
                    '     rwEmploye("NomPrens") & " Nbre Annee => " & nb_an & vbNewLine &
                    '     rwEmploye("NomPrens") & " Salaire Categorie => " & SalCategoriel
                    'Return stro & vbNewLine
                    'Ajout de la prime d'ancienneté dans la liste des accessoires et des montants imposables
                    If (PrimAncien > 0) Then
                        Accessoires.Rows.Add("PRIME D'ANCIENNETE", PrimAncien, 1, 0)
                        MontantAccessoireImpossable += PrimAncien
                    End If

                    '************************* Verification des conges a payer **********************************************
                    '
                    '
                    '************************* Calcul des droits de conges **********************************************
                    Dim CongeID As String = String.Empty
                    query = "select CONG_ID from t_grh_conges where EMP_ID='" & rwEmploye("EMP_ID") & "' And CONG_ETAT='Validée0'"
                    Dim dtConge As DataTable = ExcecuteSelectQuery(query)
                    For Each rwConge As DataRow In dtConge.Rows
                        CongeID &= MontantCongePaie(rwConge("CONG_ID"), rwEmploye("EMP_ID"))(0) & ","
                        MontantCongeAnnuel += MontantCongePaie(rwConge("CONG_ID"), rwEmploye("EMP_ID"))(1)
                    Next
                    If CongeID <> String.Empty Then
                        CongeID = Mid(CongeID, 1, (CongeID.Length - 1))
                    End If

                    Dim dtRemb As DataTable

                    If MontantCongeAnnuel > 0 Then 'On vérifie si il y'a un pret a rembourser sur le montant en conge
                        query = "select * from t_grh_remboursement_pret R, t_grh_pret_employe P where R.PRETEMP_ID=P.PRETEMP_ID AND P.EMP_ID='" & rwEmploye("EMP_ID") & "' AND REMB_DATE='" & dateconvert(DateAdd(DateInterval.Month, 1, datefin)) & "'"
                        dtRemb = ExcecuteSelectQuery(query)
                        If (dtRemb.Rows.Count > 0) Then
                            'Retrancher le pret dans le montant du conge
                            MontantTotalPret += CDec(dtRemb.Rows(0).Item("REMB_ANNUITE"))

                            query = "select max(BUL_ID) from t_grh_salairebul"
                            Dim dtMax As DataTable = ExcecuteSelectQuery(query)
                            If IsDBNull(dtMax.Rows(0).Item(0)) Then
                                BUL_ID = 1
                            Else
                                BUL_ID = CDec(dtMax.Rows(0).Item(0)) + 1
                            End If
                            'Mise a jour du pret
                            query = "update t_grh_remboursement_pret set REMB_STATUT='Clôturé',BUL_ID=" & BUL_ID & ",Conge=1 where REMB_ID=" & dtRemb.Rows(0).Item("REMB_ID")
                            ExecuteNonQuery(query)
                            query = "select count(*) from t_grh_remboursement_pret where PRETEMP_ID=" & dtRemb.Rows(0).Item("PRETEMP_ID")
                            Dim cpte As Integer = Val(ExecuteScallar(query))
                            query = "select count(*) from t_grh_remboursement_pret where REMB_STATUT='Clôturé' And PRETEMP_ID=" & dtRemb.Rows(0).Item("PRETEMP_ID")
                            Dim cpteCloture As Integer = Val(ExecuteScallar(query))
                            If cpte = cpteCloture Then
                                query = "update t_grh_pret_employe set STATUT='Terminé' where PRETEMP_ID=" & dtRemb.Rows(0).Item("PRETEMP_ID")
                                ExecuteNonQuery(query)
                            Else
                                query = "update t_grh_pret_employe set STATUT='En cours1' where PRETEMP_ID=" & dtRemb.Rows(0).Item("PRETEMP_ID")
                                ExecuteNonQuery(query)
                            End If
                        End If

                        'Enregistrer l'ACP comme élément de salaire imposable
                        Accessoires.Rows.Add("ALLOCATION DE CONGES PAYES", MontantCongeAnnuel, 1, 0)
                        MontantAccessoireImpossable += MontantCongeAnnuel
                    End If

                    'Calcul du Montant des Heures Supplementaires
                    Dim PrimesHS As Double = 0
                    For Each rw As DataRow In Accessoires.Rows
                        If rw("Libelle").ToString().ToLower() = "prime de rendement" Or rw("Libelle").ToString().ToLower() = "prime de technicité" Or rw("Libelle").ToString().ToLower() = "prime de fonction" Then
                            PrimesHS += BackToTauxHoraire(CDbl(rw("Montant")), NbreHeureTravail)
                        End If
                    Next
                    SalReel = SalCategoriel + SurSalaire + PrimesHS
                    Dim TauxHauraireHS As Double = (SalReel / NbreHeurePaie)
                    Dim MHS_41_46H As Double = 0
                    Dim MHS_47_55H As Double = 0
                    Dim MHS_NJO As Double = 0
                    Dim MHS_JDJF As Double = 0
                    Dim MHS_NDJF As Double = 0
                    MHS_41_46H = (TauxHauraireHS * 1.15) * HS_41_46H
                    MHS_47_55H = (TauxHauraireHS * 1.5) * HS_47_55H
                    MHS_NJO = (TauxHauraireHS * 1.75) * HS_NJO
                    MHS_JDJF = (TauxHauraireHS * 1.75) * HS_JDJF
                    MHS_NDJF = (TauxHauraireHS * 2) * HS_NDJF
                    MontantHeureSup = MHS_41_46H + MHS_47_55H + MHS_NJO + MHS_JDJF + MHS_NDJF

                    'Calcul du salaire brut
                    TauxHoraire = (SalContrat / NbreHeureLegal)
                    SalBrut = TauxHoraire * NbreHeureTravail

                    'On impute les heures supplementaires et les droits au conges sur le salaire brut
                    SalBrut += MontantHeureSup + MontantAvantage + (MontantAccessoireImpossable + MontantAccessoireNonImpossable)

                    'Calcul du Salaire brut
                    SalBrutImposable = SalBrut - MontantAccessoireNonImpossable

                    'Calcul contribution national (cn) "Se calcul avec le salaire cellulaire"
                    '******************************************************************************************************
                    'Calcul du salaire cellulaire, il est egal à un pourcentage du salaire brut imposable en fonction des intervalles (voir formules)
                    'recuperation des taux cn
                    Dim Montinf_cn As Double = 0
                    Dim Montsup_cn As Double = 0
                    Dim Taux_cn As Double = 0
                    Dim Montdeduire_cn As Double = 0
                    Dim Montcn As Double = 0
                    Dim salaireCellulaire As Double = (SalBrutImposable * 0.8)

                    query = "select * from t_grh_taux_cn"
                    dt = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        Montinf_cn = CDbl(rw("montant_inf"))
                        Montsup_cn = CDbl(rw("montant_sup"))
                        Taux_cn = CDbl(rw("taux"))
                        Montdeduire_cn = CDbl(rw("montant_deduire"))

                        If (Montsup_cn = 0) And (Montinf_cn <> 0) Then

                            If salaireCellulaire < Montinf_cn Then
                                Montcn = 0
                            End If

                        ElseIf (Montsup_cn <> 0) And (Montinf_cn <> 0) Then

                            If salaireCellulaire > Montinf_cn And salaireCellulaire <= Montsup_cn Then
                                Montcn = ((salaireCellulaire * Taux_cn) - Montdeduire_cn)
                            End If

                        ElseIf (Montsup_cn <> 0) And (Montinf_cn = 0) Then

                            If salaireCellulaire > Montsup_cn Then
                                Montcn = ((salaireCellulaire * Taux_cn) - Montdeduire_cn)
                            End If

                        End If
                    Next

                    '*****************************************************************************************************
                    'Calcul des taux des impôts
                    query = "select * from t_grh_tauxsalaire"
                    dt = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt.Rows
                        TauxIts = CDbl(rw("TAUX_ITS_PAT"))
                        TauxAppren = CDbl(rw("TAUX_TAX_APR"))
                        TauxForm = CDbl(rw("TAUX_TAX_FOR"))
                        TauxPrestFam = CDbl(rw("TAUX_PRES_FAM"))
                        TauxAccTrav = CDbl(rw("TAUX_ACC_TRA"))
                        'Tauxcne = CDbl(rw("TAUX_CN_EMP"))
                    Next

                    'calcul de l'ITS
                    Dim MontITS As Double = ((TauxIts / 100) * CDbl(SalBrutImposable))

                    'calcul de l'IGR
                    '******************************************************************************
                    Dim MontIGR As Double = 0
                    Dim R As Double = ((0.8 * SalBrutImposable) - (CDbl(MontITS) + CDbl(Montcn))) * 0.85
                    Dim NPart = 0
                    'Nombre de part en fonction de la situation matrimoniale

                    If SitMatri = "Marié" Or SitMatri = "Marié (e)" Or SitMatri = "Mariée" Then
                        NPart = 2
                    Else
                        NPart = 1
                    End If

                    'nombre de part en fonction du nombre d'enfts
                    Dim totPart As Double = NPart
                    If NbreEnfant > 0 Then
                        If (SitMatri = "Marié" Or SitMatri = "Marié (e)" Or SitMatri = "Mariée") Then
                            totPart += (NbreEnfant * 0.5)
                        ElseIf (SitMatri = "Veuf" Or SitMatri = "Veuf (ve)" Or SitMatri = "Veuve") Then
                            totPart += 1 + (NbreEnfant * 0.5)
                        Else
                            totPart += 0.5 + (NbreEnfant * 0.5)
                        End If
                    End If
                    If totPart > 5 Then
                        totPart = 5
                    End If

                    'calcul du quotient
                    Dim Q As Double = R / totPart

                    'recherche de la tranche et calcul d'IGR

                    'recuperation des taux igr
                    Dim Montinf_igr As Double = 0
                    Dim Montsup_igr As Double = 0
                    Dim Taux_igr As Double = 0
                    Dim Base_igr As Double = 0
                    Dim Montdeduire_igr As Double = 0


                    query = "select * from t_grh_taux_igr"
                    Dim dt0 As Data.DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dt0.Rows
                        Montinf_igr = CDbl(rw("montant_inf"))
                        Montsup_igr = CDbl(rw("montant_sup"))
                        Taux_igr = CDbl(rw("taux"))
                        Base_igr = CDbl(rw("base_taux"))
                        Montdeduire_igr = CDbl(rw("montant_deduire"))

                        If (Montsup_igr = 0) And (Montinf_igr <> 0) Then

                            If Q < Montinf_igr Then
                                MontIGR = 0
                            End If

                        ElseIf (Montsup_igr <> 0) And (Montinf_igr <> 0) Then

                            If Q > Montinf_igr And Q < Montsup_igr Then
                                MontIGR = ((R * Taux_igr / Base_igr) - (Montdeduire_igr * CDbl(totPart)))
                                'Dim str0 As String = rwEmploye("NomPrens") & " Taux => " & Taux_igr & " Base => " & Base_igr &
                                '    " M. Deduire => " & Montdeduire_igr & " TotalPart => " & totPart & " IGR => " & MontIGR
                                'Return str0
                            End If

                        ElseIf (Montsup_igr <> 0) And (Montinf_igr = 0) Then

                            If Q > Montsup_igr Then
                                MontIGR = ((R * Taux_igr / Base_igr) - (Montdeduire_igr * CDbl(totPart)))
                                'Dim str0 As String = rwEmploye("NomPrens") & " Taux => " & Taux_igr & " Base => " & Base_igr &
                                '    " M. Deduire => " & Montdeduire_igr & " TotalPart => " & totPart & " IGR => " & MontIGR
                                'Return str0
                            End If

                        End If
                    Next

                    'Calcul cnps
                    Dim cnps As Double = 0
                    Try
                        If rwEmploye("EMP_RETRAITE") = "CNPS" Then
                            If SalBrutImposable < "1647315" Then
                                cnps = ((SalBrutImposable * 6.3) / 100)
                            Else
                                cnps = ((1647315 * 6.3) / 100)
                            End If
                        ElseIf Val(rwEmploye("EMP_RETRAITE")) = "CGRAE" Then 'Calcul de la CGRAE

                        End If
                    Catch ex As Exception

                    End Try

                    'calcul du saliare net à payer
                    Dim MontRegRet As Double = 0
                    Dim MontSolid As Double = 0
                    Dim MontAppr As Double = ((TauxAppren * SalBrutImposable) / 100)
                    Dim IPS_CNAM As Decimal = 0
                    IPS_CNAM = rwEmploye("EMP_CNAM")
                    Dim PartCNAM As Decimal = Val(ExecuteScallar("SELECT PartCNAM FROM T_grh_tauxsalaire WHERE 1"))
                    IPS_CNAM *= PartCNAM

                    'Arrondi des cotisations
                    MontITS = Round(MontITS, 0, MidpointRounding.AwayFromZero)
                    MontIGR = Round(MontIGR, 0, MidpointRounding.AwayFromZero)
                    Montcn = Round(Montcn, 0, MidpointRounding.AwayFromZero)
                    cnps = Round(cnps, 0, MidpointRounding.AwayFromZero)

                    SalBrutSocial = MontITS + MontIGR + Montcn + cnps + IPS_CNAM
                    SalBrutFacultatif = Acompte + Avance + Opposition

                    SalNAP = SalBrutImposable + MontantAccessoireNonImpossable - (SalBrutSocial + SalBrutFacultatif)

                    'Recherche de pret
                    query = "select * from t_grh_remboursement_pret R, t_grh_pret_employe P where R.PRETEMP_ID=P.PRETEMP_ID AND P.EMP_ID='" & rwEmploye("EMP_ID") & "' AND REMB_DATE='" & dateconvert(datefin) & "'"
                    dtRemb = ExcecuteSelectQuery(query)
                    If (dtRemb.Rows.Count > 0) Then
                        'Retrancher le pret dans le NAP
                        MontantTotalPret += CDec(dtRemb.Rows(0).Item("REMB_ANNUITE"))

                        query = "select max(BUL_ID) from t_grh_salairebul"
                        Dim dtMax As DataTable = ExcecuteSelectQuery(query)
                        If IsDBNull(dtMax.Rows(0).Item(0)) Then
                            BUL_ID = 1
                        Else
                            BUL_ID = CDec(dtMax.Rows(0).Item(0)) + 1
                        End If
                        'Mise a jour du pret
                        query = "update t_grh_remboursement_pret set REMB_STATUT='Clôturé',BUL_ID=" & BUL_ID & ",Conge=0 where REMB_ID=" & dtRemb.Rows(0).Item("REMB_ID")
                        ExecuteNonQuery(query)
                        query = "select count(*) from t_grh_remboursement_pret where PRETEMP_ID=" & dtRemb.Rows(0).Item("PRETEMP_ID")
                        Dim cpte As Integer = Val(ExecuteScallar(query))
                        query = "select count(*) from t_grh_remboursement_pret where REMB_STATUT='Clôturé' And PRETEMP_ID=" & dtRemb.Rows(0).Item("PRETEMP_ID")
                        Dim cpteCloture As Integer = Val(ExecuteScallar(query))
                        If cpte = cpteCloture Then
                            query = "update t_grh_pret_employe set STATUT='Terminé' where PRETEMP_ID=" & dtRemb.Rows(0).Item("PRETEMP_ID")
                            ExecuteNonQuery(query)
                        Else
                            query = "update t_grh_pret_employe set STATUT='En cours1' where PRETEMP_ID=" & dtRemb.Rows(0).Item("PRETEMP_ID")
                            ExecuteNonQuery(query)
                        End If
                    End If



                    'On enregistre
                    Dim MontPestFam As Double = 0
                    If SalBrutImposable < 70000 Then
                        MontPestFam = ((TauxPrestFam * SalBrutImposable) / 100)
                    Else
                        MontPestFam = ((TauxPrestFam * 70000) / 100)
                    End If
                    Dim MontAccTrav As Double = 0

                    Dim Nat_cont = SeardID("t_grh_contrat", "cont_nature", "emp_id", id_emp.ToString)

                    Dim BaseAcc As Double = 0
                    If SalBrutImposable < 70000 Then
                        BaseAcc = SalBrutImposable
                    Else
                        BaseAcc = 70000
                    End If

                    'If Nat_cont = "Local" Or Nat_cont = "Locale" Or Nat_cont = "local" Or Nat_cont = "locale" Then
                    MontAccTrav = Round(((TauxAccTrav * BaseAcc) / 100), 0, MidpointRounding.AwayFromZero)
                    'Else
                    'MontAccTrav = (TauxAccExp * SalBrutImposable) / 100
                    'End If

                    'On ajoute sur le NAP le montant du conge paye
                    'SalNAP += MontantCongeAnnuel
                    Dim conge As Integer = 0
                    If MontantCongeAnnuel > 0 Then
                        conge = 1
                    End If

                    'On retranche dans le montant à payer le montant total de pret
                    SalNAP -= MontantTotalPret

                    query = "insert into t_grh_salairebul values (NULL,'" & id_emp.ToString & "','" & PrimAncien.ToString() & "','" & MontantAvantage.ToString & "','" & Round(SalBrut, 0, MidpointRounding.AwayFromZero).ToString() & "','" & SurSalaire.ToString & "','" & SalBrutImposable.ToString().Replace(",", ".") & "','" & MontITS.ToString & "' "
                    query &= ",'" & Montcn.ToString & "','" & MontIGR.ToString & "','" & MontPestFam.ToString & "','" & cnps.ToString & "','" & MontAccTrav.ToString & "','" & MontAppr.ToString & "','" & MontSolid.ToString & "','" & Round(SalNAP, 0, MidpointRounding.AwayFromZero).ToString() & "' "
                    query &= ",'" & tempdt2 & "','" & tempdt & "','" & tempdt1 & "','" & totPart.ToString & "'," & Replace(NbreHeureTravail, ",", ".") & "," & NbreJourConge.ToString().Replace(",", ".") & "," & conge & "," & IPS_CNAM & ")"
                    'Dimquery= "insert into t_grh_salairebul values (NULL,'" & id_emp.ToString & "','" & PrimAncien.ToString() & "','" & MontantAvantage.ToString & "','" & Math.Round(SalBrut, 0).ToString() & "','" & SurSalaire.ToString & "','" & SalBrutImposable.ToString & "','" & MontITS.ToString & "' "
                    'Dimquery = ",'" & Montcn.ToString & "','" & MontIGR.ToString & "','" & MontPestFam.ToString & "','" & cnps.ToString & "','" & MontAccTrav.ToString & "','" & MontAppr.ToString & "','" & MontSolid.ToString & "','" & SalNAP.ToString & "' "
                    'Dimquery = ",'" & tempdt2 & "','" & tempdt & "','" & tempdt1 & "','" & totPart.ToString & "'," & Replace(NbreHeureTravail, ",", ".") & "," & NbreJourConge.ToString().Replace(",", ".") & "," & MontantCongeAnnuel.ToString().Replace(",", ".") & "," & IPS_CNAM & ")"
                    ExecuteNonQuery(query)
                    Dim lastSal As String = ExecuteScallar("select max(BUL_ID) from t_grh_salairebul")

                    'On actualise le conge à payer au cas où on a eu un congé à payer en cours
                    If MontantCongeAnnuel > 0 Then
                        If CongeID.Contains(",") Then
                            Dim LesCongeID As String() = Split(CongeID, ",")
                            For Each ID As String In LesCongeID
                                query = "update t_grh_conges set CONG_ETAT='Validée', PaieDate='" & dateconvert(datefin) & "' where CONG_ID='" & ID & "'"
                                ExecuteNonQuery(query)
                            Next
                        Else
                            query = "update t_grh_conges set CONG_ETAT='Validée', PaieDate='" & dateconvert(datefin) & "' where CONG_ID='" & CongeID & "'"
                            ExecuteNonQuery(query)
                        End If
                    End If

                    For Each rw As DataRow In Accessoires.Rows
                        query = "insert into t_grh_salairebul_accessoire values(null," & lastSal & ",'" & EnleverApost(rw(0)) & "','" & rw(1).ToString().Replace(",", ".") & "'," & rw(2) & ",'" & rw(3) & "')"
                        ExecuteNonQuery(query)
                    Next
                    For Each rw As DataRow In Avantages.Rows
                        query = "insert into t_grh_salairebul_avantage values(null," & lastSal & ",'" & EnleverApost(rw(0)) & "'," & rw(1) & ")"
                        ExecuteNonQuery(query)
                    Next

                    Return ""

                ElseIf TypeRenumeration.ToUpper() = "NET" Then 'Calcul a l'envers


                End If

            End If
            Return ""
        Catch ex As MySqlException
            'Annulation de la mise a jour du pret
            Dim oldquery As String = query
            query = "select * from t_grh_remboursement_pret where REMB_DATE='" & dateconvert(datefin) & "'"
            Dim dtRemb As DataTable = ExcecuteSelectQuery(query)
            If dtRemb.Rows.Count > 0 Then
                query = "update t_grh_remboursement_pret set REMB_STATUT='Non Clôturé',BUL_ID=" & BUL_ID & ",Conge=1 where BUL_ID=" & BUL_ID
                ExecuteNonQuery(query)

                query = "select count(*) from t_grh_remboursement_pret where REMB_STATUT='Clôturé' And PRETEMP_ID=" & dtRemb.Rows(0).Item("PRETEMP_ID")
                Dim cpteCloture As Integer = Val(ExecuteScallar(query))
                If cpteCloture = 0 Then
                    query = "update t_grh_pret_employe set STATUT='En cours0' where PRETEMP_ID=" & dtRemb.Rows(0).Item("PRETEMP_ID")
                    ExecuteNonQuery(query)
                Else
                    query = "update t_grh_pret_employe set STATUT='En cours1' where PRETEMP_ID=" & dtRemb.Rows(0).Item("PRETEMP_ID")
                    ExecuteNonQuery(oldquery)
                End If
            End If

            Return "Mysql : " & ex.ToString & " query -> " & oldquery
        Catch ep As Exception
            'Annulation de la mise a jour du pret
            Dim oldquery As String = query
            query = "select * from t_grh_remboursement_pret where REMB_DATE='" & dateconvert(datefin) & "'"
            Dim dtRemb As DataTable = ExcecuteSelectQuery(query)
            If dtRemb.Rows.Count > 0 Then
                query = "update t_grh_remboursement_pret set REMB_STATUT='Clôturé',BUL_ID=" & BUL_ID & ",Conge=1 where REMB_ID=" & dtRemb.Rows(0).Item("REMB_ID")
                ExecuteNonQuery(query)
                query = "select count(*) from t_grh_remboursement_pret where PRETEMP_ID=" & dtRemb.Rows(0).Item("PRETEMP_ID")
                Dim cpte As Integer = Val(ExecuteScallar(query))
                query = "select count(*) from t_grh_remboursement_pret where REMB_STATUT='Clôturé' And PRETEMP_ID=" & dtRemb.Rows(0).Item("PRETEMP_ID")
                Dim cpteCloture As Integer = Val(ExecuteScallar(query))
                If cpte = cpteCloture Then
                    query = "update t_grh_pret_employe set STATUT='Terminé' where PRETEMP_ID=" & dtRemb.Rows(0).Item("PRETEMP_ID")
                    ExecuteNonQuery(query)
                Else
                    query = "update t_grh_pret_employe set STATUT='En cours1' where PRETEMP_ID=" & dtRemb.Rows(0).Item("PRETEMP_ID")
                    ExecuteNonQuery(query)
                End If
            End If

            Return ep.ToString
        End Try
        Return ""
    End Function
    Public Function ArchivageBulletin(EMP_ID As Integer, datedeb As Date, datefin As Date) As String
        On Error Resume Next
        'Archivage du bulletin
        Dim Bulletin As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table
        Dim Chemin As String = lineEtat & "\GRH\"

        Dim DatSet = New DataSet
        Bulletin.Load(Chemin & "BulletinSalaire.rpt")

        With crConnectionInfo
            .ServerName = ODBCNAME
            .DatabaseName = DB
            .UserID = USERNAME
            .Password = PWD
        End With

        CrTables = Bulletin.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        Bulletin.SetDataSource(DatSet)
        Bulletin.SetParameterValue("Date1", datedeb)                                  '''''                            ''''
        Bulletin.SetParameterValue("Date2", datefin)                                  '''''  Parametre du bulletin     ''''
        Bulletin.SetParameterValue("CodeEmp", EMP_ID)                    '''''                            ''''
        Bulletin.SetParameterValue("CodeProjet", ProjetEnCours)                       '''''                            ''''

        Bulletin.SetParameterValue("Date1", datedeb, "Prime.rpt")                     '''''                            ''''
        Bulletin.SetParameterValue("Date2", datefin, "Prime.rpt")                     '''''  Parametre du SR Prime     ''''
        Bulletin.SetParameterValue("CodeEmp", EMP_ID, "Prime.rpt")       '''''                            ''''
        Bulletin.SetParameterValue("CodeProjet", ProjetEnCours, "Prime.rpt")          '''''                            ''''

        Bulletin.SetParameterValue("Date1", datedeb, "PrimeNI.rpt")                   '''''                            ''''
        Bulletin.SetParameterValue("Date2", datefin, "PrimeNI.rpt")                   '''''  Parametre du SR PrimeNI   ''''
        Bulletin.SetParameterValue("CodeEmp", EMP_ID, "PrimeNI.rpt")     '''''                            ''''
        Bulletin.SetParameterValue("CodeProjet", ProjetEnCours, "PrimeNI.rpt")        '''''                            ''''

        Bulletin.SetParameterValue("Date1", datedeb, "Conge.rpt")                     '''''                            ''''
        Bulletin.SetParameterValue("Date2", datefin, "Conge.rpt")                     '''''  Parametre du SR Conge     ''''
        Bulletin.SetParameterValue("CodeEmp", EMP_ID, "Conge.rpt")       '''''                            ''''
        Bulletin.SetParameterValue("CodeProjet", ProjetEnCours, "Conge.rpt")          '''''                            ''''

        Bulletin.SetParameterValue("Date1", datedeb, "Pret.rpt")                      '''''                            ''''
        Bulletin.SetParameterValue("Date2", datefin, "Pret.rpt")                      '''''   Parametre du SR Pret     ''''
        Bulletin.SetParameterValue("CodeEmp", EMP_ID, "Pret.rpt")        '''''                            ''''
        Bulletin.SetParameterValue("CodeProjet", ProjetEnCours, "Pret.rpt")           '''''                            ''''


        Dim path = line & "\Pochettes\Salaires\Les bulletins de salaire\EMP_" & EMP_ID

        If Not Directory.Exists(path) Then
            Directory.CreateDirectory(path)
        End If
        Dim POCHDOC_ID As Integer = Val(ExecuteScallar("select POCHDOC_ID from t_grh_pochette_document where POCHDOC_LIB='Les bulletins de salaire'"))
        If POCHDOC_ID = 0 Then
            Dim POCH_ID As Integer = Val(ExecuteScallar("select POCH_ID from t_grh_pochette where POCH_LIB='Salaires'"))
            If POCH_ID <> 0 Then
                query = "insert into t_grh_pochette_document values(null,'Les bulletins de salaire',1," & POCH_ID & ")"
                ExecuteNonQuery(query)
                POCHDOC_ID = Val(ExecuteScallar("select POCHDOC_ID from t_grh_pochette_document where POCHDOC_LIB='Les bulletins de salaire'"))
            Else
                query = "insert into t_grh_pochette values(null,'Salaires')"
                ExecuteNonQuery(query)
                POCH_ID = Val(ExecuteScallar("Select POCH_ID from t_grh_pochette where POCH_LIB='Salaires'"))
                query = "insert into t_grh_pochette_document values(null,'Les bulletins de salaire',1," & POCH_ID & ")"
                ExecuteNonQuery(query)
                POCHDOC_ID = Val(ExecuteScallar("select POCHDOC_ID from t_grh_pochette_document where POCHDOC_LIB='Les bulletins de salaire'"))
            End If
        End If


        'Enregistrement du PDF sur le Disque
        Dim LesMois As String() = {"Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septemptre", "Octobre", "Novembre", "Décembre"}
        Dim LeMois As String = LesMois(datefin.Month - 1)
        Dim Lannee As String = datefin.Year
        Dim PathBulletin As String = path & "\Bulletin de salaire de " & LeMois & " " & Lannee & ".pdf"
        Bulletin.ExportToDisk(ExportFormatType.PortableDocFormat, PathBulletin)
        Dim NewFileName = SplitFileName(PathBulletin)(0) & ".pdf"
        query = "SELECT COUNT(*) FROM t_grh_pochette_employe WHERE FileName='" & NewFileName & "' AND EMP_ID='" & EMP_ID & "'"
        If Val(ExecuteScallar(query)) = 0 Then
            query = "insert into t_grh_pochette_employe values(null,'" & NewFileName & "','" & dateconvert(Now.ToShortDateString()) & "'," & Lannee & "," & POCHDOC_ID & "," & EMP_ID & ")"
            ExecuteNonQuery(query)
        End If
        Return PathBulletin
    End Function
    Private Function BackToTauxHoraire(ByRef Montant As Double, ByVal Heure As Double) As Double
        Return ((Montant / 173.33) * Heure)
    End Function
    ''' <summary>
    ''' Permet d'obtenir les IDs du contrat, du poste, du service, du supérieur hiérachique en cours de l'employé passé en paramètre
    ''' CONTRAT_ID
    ''' FONCTION_ID
    ''' SERVICE_ID
    ''' TRAV_ID
    ''' CodeBoss
    ''' </summary>
    ''' <param name="EMP_ID">ID de l'employé</param>
    ''' <returns>CONTRAT_ID, FONCTION_ID, SERVICE_ID, TRAV_ID, CodeBoss</returns>
    Public Function GetEmpInfo(EMP_ID As Double) As DataRow
        Dim dt As New DataTable
        dt.Columns.Add("CONTRAT_ID", Type.GetType("System.String"))
        dt.Columns.Add("FONCTION_ID", Type.GetType("System.String"))
        dt.Columns.Add("SERVICE_ID", Type.GetType("System.String"))
        dt.Columns.Add("TRAV_ID", Type.GetType("System.String"))
        dt.Columns.Add("CodeBoss", Type.GetType("System.String"))
        'dt.Columns.Add("RESPONSABLE_ID", Type.GetType("System.String"))
        Dim CONTRAT_ID As String = String.Empty
        Dim FONCTION_ID As String = String.Empty
        Dim SERVICE_ID As String = String.Empty
        Dim TRAV_ID As String = String.Empty
        Dim CodeBoss As String = String.Empty
        'Dim RESPONSABLE_ID As String = String.Empty
        query = "select CONT_ID from t_grh_contrat where EMP_ID=" & EMP_ID & " AND CONT_TYPE<>'Stage' ORDER BY CONT_DATE_DEB DESC"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        If dt0.Rows.Count > 0 Then
            CONTRAT_ID = dt0.Rows(0).Item(0)
        End If

        query = "select TRAV_ID,CodeService from t_grh_travailler where PosteActu='O' And EMP_ID=" & EMP_ID
        dt0 = ExcecuteSelectQuery(query)
        If dt0.Rows.Count > 0 Then
            FONCTION_ID = dt0.Rows(0).Item("CodeService")
            TRAV_ID = dt0.Rows(0).Item("TRAV_ID")
            'Recuperation du service
            query = "select CodeService,CodeBoss from t_fonction where RefFonction=" & FONCTION_ID
            dt0 = ExcecuteSelectQuery(query)
            If dt0.Rows.Count > 0 Then

                SERVICE_ID = dt0.Rows(0).Item("CodeService")
                CodeBoss = dt0.Rows(0).Item("CodeBoss")
            End If

        End If
        dt.Rows.Add({CONTRAT_ID, FONCTION_ID, SERVICE_ID, TRAV_ID, CodeBoss})
        Return dt.Rows(0)

    End Function
    Public Function GetInfoEmp(EMP_ID As String) As DataRow
        Dim dt As New DataTable
        dt.Columns.Add("Nom", Type.GetType("System.String"))
        dt.Columns.Add("Prenoms", Type.GetType("System.String"))
        dt.Columns.Add("Sexe", Type.GetType("System.String"))
        dt.Columns.Add("NomPrenoms", Type.GetType("System.String"))
        Dim Nom As String = String.Empty
        Dim Prenoms As String = String.Empty
        Dim Sexe As String = String.Empty
        Dim NomPrenoms As String = String.Empty
        query = "select * from t_grh_employe where EMP_ID='" & EMP_ID & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        If dt0.Rows.Count > 0 Then
            Nom = dt0.Rows(0).Item("EMP_NOM")
            Prenoms = dt0.Rows(0).Item("EMP_PRENOMS")
            Sexe = dt0.Rows(0).Item("EMP_SEXE")
            NomPrenoms = (dt0.Rows(0).Item("EMP_NOM") & " " & dt0.Rows(0).Item("EMP_PRENOMS")).ToString().Trim()
        End If

        dt.Rows.Add({Nom, Prenoms, Sexe, NomPrenoms})
        Return dt.Rows(0)

    End Function
    Public Function GetFistEmpInfo() As DataRow
        Dim dt As New DataTable
        dt.Columns.Add("CONTRAT_ID", Type.GetType("System.String"))
        dt.Columns.Add("FONCTION_ID", Type.GetType("System.String"))
        dt.Columns.Add("SERVICE_ID", Type.GetType("System.String"))
        dt.Columns.Add("TRAV_ID", Type.GetType("System.String"))
        dt.Columns.Add("EMP_ID", Type.GetType("System.String"))
        Dim CONTRAT_ID As String = String.Empty
        Dim FONCTION_ID As String = String.Empty
        Dim SERVICE_ID As String = String.Empty
        Dim EMP_ID As String = String.Empty
        Dim TRAV_ID As String = String.Empty
        'Dim RESPONSABLE_ID As String = String.Empty

        query = "select EMP_ID from t_grh_travailler where PosteActu='O' And CodeService=(SELECT RefFonction FROM t_fonction where CodeBoss=0)"
        Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
        If dtVerif.Rows.Count > 0 Then
            EMP_ID = dtVerif.Rows(0)("EMP_ID")


            query = "select CONT_ID from t_grh_contrat where EMP_ID=" & EMP_ID & " AND CONT_TYPE<>'Stage' ORDER BY CONT_DATE_DEB DESC"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            If dt0.Rows.Count > 0 Then
                CONTRAT_ID = dt0.Rows(0).Item(0)
            End If

            query = "select TRAV_ID,CodeService from t_grh_travailler where PosteActu='O' And EMP_ID=" & EMP_ID
            dt0 = ExcecuteSelectQuery(query)
            If dt0.Rows.Count > 0 Then
                FONCTION_ID = dt0.Rows(0).Item("CodeService")
                TRAV_ID = dt0.Rows(0).Item("TRAV_ID")
                'Recuperation du service
                query = "select CodeService from t_fonction where RefFonction=" & FONCTION_ID
                dt0 = ExcecuteSelectQuery(query)
                If dt0.Rows.Count > 0 Then
                    SERVICE_ID = dt0.Rows(0).Item(0)
                End If

            End If

        End If

        dt.Rows.Add({CONTRAT_ID, FONCTION_ID, SERVICE_ID, TRAV_ID, EMP_ID})
        Return dt.Rows(0)

    End Function
    Public Function SendBulletin(Msg As String, Subject As String, Dest As String, FileName As String) As Boolean
        'Envoie du mail
        'If rwEmploye("EMP_EMAIL").ToString().Length <> 0 Then
        '    'Envoie du mail a l'employe
        '    Dim MsgText As String = "<html><body><p>Ci-joint votre bulletin <b><u>de salaire</u></b></p></body></html>"
        '    Dim Subj As String = "Paie du mois de " & datefin.ToShortDateString().Replace("/", "-")
        '    Dim FromName As String = "ClearProject - Paie"
        '    Dim From As String = "support@ClearProject.online"
        '    SendBulletin(MsgText, Subj, From, FromName, rwEmploye("EMP_EMAIL"), PathBulletin)
        'End If

        Dim strAccount As String
        Dim strPassword As String

        Dim response As String = String.Empty
        Dim LastSmtpResponse As String = String.Empty

        ' Mail: Clear (good pratise)
        PaieSMTP.Clear()
        If Not Directory.Exists(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\ClearProject") Then
            Directory.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\ClearProject")
        End If
        PaieSMTP.LogFile = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\ClearProject\Logs.log"

        ' Mail: From
        PaieMsg.FromName = "ClearProject"
        PaieMsg.FromAddress = "no-reply@clearproject.online"

        ' Mail: Subject
        PaieMsg.Subject = Subject

        ' Mail: Priority
        PaieMsg.Priority = 1    '1 - Highest Priority 2 - High Priority 3 - Normal Priority 4 - Low Priority 5 - Lowest Priority

        ' Mail: Encoding
        PaieMsg.Encoding = objConstants.EMAIL_MESSAGE_ENCODING_UTF8

        PaieMsg.BodyHtml = Msg

        ' Mail: TO recipient(s)
        PaieMsg.AddTo(Dest, Dest)

        ' Mail: Add attachment(s)
        PaieMsg.AddAttachment(FileName)


        ' Mail: If a function failed then quit
        'If (PaieMsg.LastError <> 0) Then
        '    txtResult.Text = PaieMsg.LastError.ToString() & ": " & PaieMsg.GetErrorDescription(PaieMsg.LastError)
        '    Exit Sub
        'End If


        ' Smtp: Set Secure if secure communications is required
        'PaieSMTP.SetSecure(465)
        PaieSMTP.HostPort = 25

        ' Smtp: Account and Password - if any
        strAccount = "no-reply@clearproject.online"
        strPassword = "_Y21qcr5"

        ' Smtp: Connect
        PaieSMTP.Connect("webmail.clearproject.online", strAccount, strPassword)

        ' Smtp: Send
        If (PaieSMTP.LastError = 0) Then
            PaieSMTP.Send(PaieMsg)
        End If

        'txtResult.Text = PaieSMTP.LastError.ToString() & ": " & PaieSMTP.GetErrorDescription(PaieSMTP.LastError)
        'txtLastSmtpResponse.Text = PaieSMTP.LastSmtpResponse

        ' Smtp: Disconnect
        PaieSMTP.Disconnect()
        Return True

    End Function
    Public Function MontantConge(EMP_ID As Integer, Duree As Integer) As String()
        Dim SBIGlobal As Decimal = 0
        query = "SELECT SUM(BUL_SALBRUT) AS TotalBrute FROM t_grh_salairebul WHERE EMP_ID='" & EMP_ID & "' AND BUL_DATEFIN <='" & dateconvert(Now.ToShortDateString) & "' AND BUL_DATEFIN >= '" & dateconvert(DateAdd(DateInterval.Month, -12, Now).ToShortDateString) & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            If IsDBNull(rw("TotalBrute")) Then
                Return {"True", 0}
            Else
                SBIGlobal = CDec(rw("TotalBrute"))
            End If
        Next
        Dim SMM As Decimal = SBIGlobal / 12
        Dim ACP As Decimal = Round(((SMM * Duree) / 30), 0)
        Return {"True", ACP}

    End Function
    Private Function MontantCongePaie(CONG_ID As String, EMP_ID As String) As String()
        query = "select CONG_NBJR,CONG_ID from t_grh_conges where CONG_ID='" & CONG_ID & "' And CONG_ETAT='Validée0'"
        Dim dtConge As DataTable = ExcecuteSelectQuery(query)
        If dtConge.Rows.Count > 0 Then

            Dim SBIGlobal As Decimal = 0
            query = "SELECT SUM(BUL_SALBRUT) AS TotalBrute FROM t_grh_salairebul WHERE EMP_ID='" & EMP_ID & "' AND BUL_DATEFIN <='" & dateconvert(Now.ToShortDateString) & "' AND BUL_DATEFIN >= '" & dateconvert(DateAdd(DateInterval.Month, -12, Now).ToShortDateString) & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                If IsDBNull(rw("TotalBrute")) Then
                    Return {"0", "0"}
                Else
                    SBIGlobal = CDec(rw("TotalBrute"))
                End If
            Next
            Dim SMM As Decimal = SBIGlobal / 12
            Dim ACP As Decimal = Round(((SMM * dtConge.Rows(0)("CONG_NBJR")) / 30), 0)
            Return {dtConge.Rows(0)("CONG_ID"), ACP}
        Else
            Return {"0", "0"}
        End If

    End Function
    Public Function GetNewLogin(ByVal Nom As String, ByVal Prenom As String) As String
        Dim Login = Mid(Nom.Trim, 1, 1) & Prenom.Trim.Split(" "c)(0).Replace("'", "")
        query = "select UtilOperateur from T_Operateur where UtilOperateur ='" & Login & "'"
        Dim dts As DataTable = ExcecuteSelectQuery(query)
        Dim Trouver As Integer = dts.Rows.Count
        Dim cpte As Integer = 1

        While Trouver <> 0
            Login = Mid(Nom.Trim, 1, 1) & Prenom.Trim.Split(" "c)(0).Replace("'", "") & cpte
            query = "select UtilOperateur from T_Operateur where UtilOperateur ='" & Login & "'"
            dts = ExcecuteSelectQuery(query)
            Trouver = dts.Rows.Count
            cpte += 1
        End While
        Return Login
    End Function
    Public Function IsChefService(emp_id As Integer) As Boolean
        Dim dtInfo = GetEmpInfo(emp_id)

        query = "SELECT ChefService FROM t_fonction WHERE RefFonction='" & dtInfo("FONCTION_ID") & "'"

        Dim ChefService As Boolean = False
        Try
            ChefService = CBool(ExecuteScallar(query))

        Catch ex As Exception

        End Try

        If ChefService = True Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function IsSupHierachique(empID As String) As Integer
        Dim dtInfo = GetEmpInfo(empID)
        query = "SELECT count(RefFonction) FROM t_fonction WHERE CodeBoss ='" & dtInfo("FONCTION_ID") & "'"
        Dim count As Integer = 0
        Try
            count = Val(ExecuteScallar(query))
        Catch ex As Exception

        End Try

        If count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function JoursSupplementaires(EmpID As Integer) As Integer
        Dim dtInfoEmp As DataTable = ExcecuteSelectQuery("SELECT * FROM t_grh_employe where emp_id='" & EmpID & "'")
        Dim rwInfoEmp As DataRow = dtInfoEmp.Rows(0)
        Dim Sexe As String = rwInfoEmp("EMP_SEXE")
        Dim NbEnfantACharge As String = rwInfoEmp("EMP_NB_ENF_CHARGE")
        Dim age As Integer = DateDiff(DateInterval.Year, CDate(rwInfoEmp("EMP_DATENAIS")), Now)
        Dim NbreTotalJourSupl As Integer = 0
        Dim datedebContrat As Date
        Dim Anciennete As Integer = 0
        Dim jourSuppAnciennete As Integer = 0
        query = "select DATE_FORMAT(MAX(CONT_DATE_DEB),'%d-%m-%Y') as CONT_DATE_DEB, CONT_ID from t_grh_contrat where CONT_TYPE<>'Stage' AND (CONT_DATE_FIN IS NULL OR CONT_DATE_FIN>='" & dateconvert(Now.ToShortDateString) & "') AND emp_id='" & EmpID & "' group by CONT_ID"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        If dt0.Rows.Count = 0 Then
            Return NbreTotalJourSupl
        End If

        For Each rw0 As DataRow In dt0.Rows
            datedebContrat = CDate(rw0(0))
        Next

        Dim nbrjour = DateDiff(DateInterval.Day, datedebContrat, CDate(Now.ToShortDateString)) '365 jours * 2 = 730
        Anciennete = (nbrjour \ 365) ' Le nombre d'anciennete

        'JOURS SUPPLEMENTAIRE SELON l'ANCIENETE


        If (Anciennete > 5 And Anciennete <= 10) Then
            jourSuppAnciennete += 1

        ElseIf (Anciennete > 10 And Anciennete <= 15) Then
            jourSuppAnciennete += 2

        ElseIf (Anciennete > 15 And Anciennete <= 20) Then
            jourSuppAnciennete += 3

        ElseIf (Anciennete > 20 And Anciennete <= 25) Then
            jourSuppAnciennete += 5

        ElseIf (Anciennete > 25 AndAlso Anciennete <= 30) Then
            jourSuppAnciennete += 7

        ElseIf Anciennete > 30 Then
            jourSuppAnciennete += 8
        Else

            jourSuppAnciennete = 0
        End If

        'JOURS SUPPLEMENTAIRE ADDITIONEL SELON LE SEXE 
        If Sexe = "F" Then
            If age < 21 Then

                NbreTotalJourSupl = (NbEnfantACharge * 2) + jourSuppAnciennete
                'SuccesMsg("Id => " & EmpID & vbNewLine & "Sexe => " & Sexe & vbNewLine & "NbreTotalJourSupl => " & NbreTotalJourSupl)
            Else
                If NbEnfantACharge >= 4 Then
                    NbEnfantACharge -= 3
                    NbreTotalJourSupl = (NbEnfantACharge * 2) + jourSuppAnciennete

                End If
            End If
        Else
            NbreTotalJourSupl = jourSuppAnciennete

        End If
        'SuccesMsg("Id => " & EmpID & vbNewLine & "Sexe => " & Sexe & vbNewLine & "NbreTotalJourSupl => " & NbreTotalJourSupl)
        Return NbreTotalJourSupl
    End Function
    Function DisponibiliteEmp(EmpID As Integer, DateFin As Date) As Boolean
        Try
            query = "SELECT PaieDate FROM t_grh_conges WHERE EMP_ID='" & EmpID & "' AND CONG_ETAT='Validée' AND PaieDate IS NOT NULL ORDER BY PaieDate DESC LIMIT 1"
            Dim DerniereDateConge As String = ExecuteScallar(query)
            If DerniereDateConge <> "" Then
                Dim DateLastConge As Date = CDate(DerniereDateConge)
                DateLastConge = DateAdd(DateInterval.Month, 1, DateLastConge)
                If DateLastConge.Month = DateFin.Month Then
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
        End Try

    End Function

    Sub RemplirDatagridconges(ByVal mondg As DevExpress.XtraGrid.GridControl, ByVal grid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal requete As String)
        Try
            dtconge.Columns.Clear()
            dtconge.Columns.Add("N°", Type.GetType("System.String"))
            dtconge.Columns.Add("id_conges", Type.GetType("System.String"))
            dtconge.Columns.Add("id_emp", Type.GetType("System.String"))
            dtconge.Columns.Add("Nom", Type.GetType("System.String"))
            dtconge.Columns.Add("Prénoms", Type.GetType("System.String"))
            dtconge.Columns.Add("Nombre de jours ouvrables", Type.GetType("System.String"))
            dtconge.Columns.Add("Montant", Type.GetType("System.String"))
            dtconge.Columns.Add("Date de départ", Type.GetType("System.String"))
            dtconge.Columns.Add("Date de retour", Type.GetType("System.String"))
            dtconge.Columns.Add("Etat", Type.GetType("System.String"))
            dtconge.Rows.Clear()

            Dim cptr As Integer = 0
            Dim dt As DataTable = ExcecuteSelectQuery(requete)
            For Each rw In dt.Rows
                cptr += 1
                Dim drS = dtconge.NewRow()
                drS(0) = cptr
                drS(1) = rw(0).ToString
                drS(2) = rw(1).ToString

                drS(3) = MettreApost(rw(2).ToString)
                drS(4) = MettreApost(rw(3).ToString)
                'drS(5) = rw(4).ToString
                drS(5) = rw(4).ToString
                drS(6) = AfficherMonnaie(rw(5).ToString)
                drS(7) = CDate(rw(6)).ToString("dd/MM/yyyy")
                drS(8) = CDate(rw(7)).ToString("dd/MM/yyyy")
                If (rw(8).ToString = "Refusé") Then
                    drS(9) = "Refusée"
                Else
                    drS(9) = IIf((rw(8).ToString <> "Validée" And rw(8).ToString <> "Validée0"), "En cours", "Validée")
                End If
                dtconge.Rows.Add(drS)
            Next

            mondg.DataSource = dtconge
            grid.OptionsView.ColumnAutoWidth = True
            grid.Columns(0).Width = 50
            grid.Columns(1).Visible = False
            grid.Columns(2).Visible = False
            grid.Columns(3).Width = 300
            grid.Columns(4).Width = 400
            grid.Columns(5).Width = 150
            grid.Columns(6).Width = 200
            grid.Columns(7).Width = 100
            grid.Columns(8).Width = 100
            grid.Columns(9).Width = 100
            grid.Columns(5).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            grid.Columns(6).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            grid.Columns(7).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Columns(8).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            grid.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)

        Catch ex As Exception
            SuccesMsg(ex.ToString())
        End Try
    End Sub

End Module
