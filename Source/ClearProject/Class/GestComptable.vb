Imports System.Math
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class GestComptable
    Public Shared Function IsCompteAMarche(ByVal CodeSC As String) As Boolean
        query = "select CODE_SC from T_COMP_SOUS_CLASSE where TypeCompte<>'' and CompteMarche='O' And CODE_SC='" & CodeSC & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function IsCompteSurMarche(ByVal CodeSC As String) As Boolean
        query = "select ms.NumeroMarche, m.DescriptionMarche, ms.MontantHt from T_Marche m, T_MarcheSigne ms where m.RefMarche=ms.RefMarche and m.RefMarche IN (SELECT DISTINCT RefMarche from t_acteng where NumeroComptable='" & CodeSC & "') and ms.NumMarcheDMP<>'' Group by ms.numeroMarche, m.DescriptionMarche, ms.MontantHt"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        Dim nbremarche As Decimal = 0
        For Each rw As DataRow In dt.Rows
            Dim nbreact As Decimal = 0
            Dim numvide As Decimal = 0
            query = "select count(*) from t_comp_activite where numeromarche='" & rw(0).ToString & "'"
            numvide = Val(ExecuteScallar(query))

            If numvide = 0 Then
                nbremarche += 1
            Else
                query = "select count(*) from t_comp_activite where numeromarche='" & rw(0).ToString & "' having sum(montant_act)<'" & rw(2).ToString & "'"
                nbreact = Val(ExecuteScallar(query))
                If nbreact > 0 Then
                    nbremarche += 1
                End If
            End If
        Next
        If nbremarche > 0 Then
            Return True
        End If
        Return False
    End Function
    Public Shared Function IsCompteDeCharge(ByVal CodeSC As String) As Boolean
        Dim Code As String = Mid(CodeSC, 1, 2)
        Dim Classe As String = Mid(Code, 1, 1)
        Dim Exceptions() As String = {"28", "29", "68", "69", "81", "85", "87"} 'Ce sont les comptes de charge non decaissable, A ne pas considerer comme une charge
        If Classe = "2" Or Classe = "6" Then
            If Array.IndexOf(Exceptions, Code) = -1 Then 'On verifie si le code n'est pas dans les exceptions
                Return True
            End If
        ElseIf Classe = "8" Then
            If Val(Code) Mod 2 <> 0 Then
                If Array.IndexOf(Exceptions, Code) = -1 Then 'On verifie si le code n'est pas dans les exceptions
                    Return True
                End If
            End If
        End If
        Return False
    End Function
    Public Shared Function IsCompteDeChargeResultat(ByVal CodeSC As String) As Boolean
        Dim Code As String = Mid(CodeSC, 1, 2)
        Dim Classe As String = Mid(Code, 1, 1)
        If Classe = "6" Then
            Return True
        ElseIf Classe = "8" Then
            If Val(Code) Mod 2 <> 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Shared Function IsCompteDeProduitResultat(ByVal CodeSC As String) As Boolean
        Dim Code As String = Mid(CodeSC, 1, 2)
        Dim Classe As String = Mid(Code, 1, 1)
        If Classe = "7" Then
            Return True
        ElseIf Classe = "8" Then
            If Val(Code) Mod 2 = 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Shared Function EstSurLaPriseEncharge(CodeE As String, CodeJ As String, CodeTiers As String) As Boolean
        query = "SELECT * FROM t_comp_activite WHERE CODE_E='" & CodeE & "'" 'On cherche les prises en charge
        Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
        If dtVerif.Rows.Count > 0 Then 'Il y'a au moins une prise en charge sur le numero de piece, on enregistre tous les comptes de tiers qui on été pris en charge pour en faire une comparaison juste après
            For Each rwverif As DataRow In dtVerif.Rows
                query = "select * from t_comp_ligne_ecriture where CODE_E='" & rwverif("CODE_E") & "' and CODE_J='" & rwverif("CODE_J") & "' and DATE_LE='" & dateconvert(rwverif("Date_act")) & "' and CODE_SC<>'" & rwverif("CODE_SC") & "'"
                Dim dtTiers As DataTable = ExcecuteSelectQuery(query)
                For Each rwTiers As DataRow In dtTiers.Rows
                    If rwTiers("CODE_CPT").ToString().Length > 0 Then
                        If rwTiers("CODE_CPT") = CodeTiers Then
                            Return True
                        End If
                    End If
                Next
            Next
        End If
        Return False
    End Function

    'Public Shared Sub CleanReport(Annee As Decimal)
    '    query = "SELECT DISTINCT `CODE_CPT` FROM `t_comp_ligne_ecriture` WHERE YEAR(`DATE_LE`)='" & Annee & "' AND LENGTH(`CODE_CPT`)<>0"
    '    Dim dt As DataTable = ExcecuteSelectQuery(query)
    '    For Each rw As DataRow In dt.Rows
    '        query = "SELECT code_cpt FROM report_cpt WHERE code_cpt='" & rw("code_cpt") & "' AND YEAR(DATE_LE)='" & Annee & "'"
    '        Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
    '        If dtVerif.Rows.Count = 0 Then
    '            query = "INSERT INTO report_cpt VALUES(NULL,'" & rw("code_cpt") & "','0','0','0','0','" & Annee & "-01-01')"
    '            ExecuteNonQuery(query)
    '        End If
    '    Next

    '    query = "SELECT DISTINCT `CODE_SC` FROM `t_comp_ligne_ecriture` WHERE YEAR(`DATE_LE`)='" & Annee & "'"
    '    dt = ExcecuteSelectQuery(query)
    '    For Each rw As DataRow In dt.Rows
    '        query = "SELECT code_sc FROM report_sc WHERE code_sc='" & rw("CODE_SC") & "' AND YEAR(DATE_LE)='" & Annee & "'"
    '        Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
    '        If dtVerif.Rows.Count = 0 Then
    '            query = "INSERT INTO report_sc VALUES(NULL,'" & rw("code_sc") & "','0','0','0','0','" & Annee & "-01-01')"
    '            ExecuteNonQuery(query)
    '        End If

    '        Dim Code_CL0 As String = Mid(rw("code_sc"), 1, 1)
    '        query = "SELECT code_cl0 FROM report_cl0 WHERE code_cl0='" & Code_CL0 & "' AND YEAR(DATE_LE)='" & Annee & "'"
    '        dtVerif = ExcecuteSelectQuery(query)
    '        If dtVerif.Rows.Count = 0 Then
    '            query = "INSERT INTO report_cl0 VALUES(NULL,'" & Code_CL0 & "','0','0','" & Annee & "-01-01')"
    '            ExecuteNonQuery(query)
    '        End If

    '        Dim Code_CL As String = Mid(rw("code_sc"), 1, 2)
    '        query = "SELECT code_cl FROM report_cl WHERE code_cl='" & Code_CL & "' AND YEAR(DATE_LE)='" & Annee & "'"
    '        dtVerif = ExcecuteSelectQuery(query)
    '        If dtVerif.Rows.Count = 0 Then
    '            query = "INSERT INTO report_cl VALUES(NULL,'" & Code_CL & "','0','0','" & Annee & "-01-01')"
    '            ExecuteNonQuery(query)
    '        End If
    '    Next
    'End Sub
    Public Shared Sub CleanReport(Annee As Decimal)
        Dim DateDebut As String = dateconvert(CDate("01/01/" & Annee))

        Dim DateFin As String = dateconvert(CDate("31/12/" & Annee))

        Dim DateN1 As String = dateconvert(CDate("01/01/" & (Annee + 1)))

        'Dim DateDuJour As String = dateconvert(Now.ToShortDateString())

        query = "select code_sc from T_COMP_SOUS_CLASSE WHERE code_sc NOT IN(SELECT CODE_SC FROM report_sc WHERE DATE_LE='" & DateN1 & "')"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            query = "insert into report_sc values (NULL, '" & rw("code_sc").ToString & "','0', '0','0', '0','" & DateN1 & "')"
            ExecuteNonQuery(query)
        Next

        query = "select code_sc from t_comp_ligne_ecriture WHERE (DEBIT_LE<>0 OR CREDIT_LE<>0) AND (DATE_LE BETWEEN '" & DateDebut & "' AND '" & DateFin & "') AND (code_sc NOT IN(SELECT CODE_SC FROM report_sc WHERE DATE_LE='" & DateN1 & "'))"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            query = "insert into report_sc values (NULL, '" & rw("code_sc").ToString & "','0', '0','0', '0','" & DateN1 & "')"
            ExecuteNonQuery(query)
        Next

        query = "select distinct (code_cl) from T_COMP_SOUS_CLASSE WHERE code_cl NOT IN(SELECT code_cl FROM report_cl WHERE DATE_LE='" & DateN1 & "')"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            query = "insert into report_cl values (NULL, '" & rw("code_cl").ToString & "','0', '0', '" & DateN1 & "')"
            ExecuteNonQuery(query)
        Next

        query = "select code_cl0 from T_COMP_CLASSE0 WHERE code_cl0 NOT IN(SELECT code_cl0 FROM report_cl0 WHERE DATE_LE='" & DateN1 & "')"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            query = "insert into report_cl0 values (NULL, '" & rw("code_cl0").ToString & "','0', '0', '" & DateN1 & "')"
            ExecuteNonQuery(query)
        Next

        query = "select code_cpt from T_COMP_COMPTE WHERE CODE_CPT NOT IN(SELECT CODE_CPT FROM report_cpt WHERE DATE_LE='" & DateN1 & "')"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            query = "insert into report_cpt values (NULL, '" & rw("code_cpt").ToString & "','0','0','0', '0','" & DateN1 & "')"
            ExecuteNonQuery(query)
        Next

    End Sub
    Public Shared Sub UpdateReport(Annee As Decimal)
        Dim DateDebut As String = dateconvert(CDate("01/01/" & Annee))

        Dim DateFin As String = dateconvert(CDate("31/12/" & Annee))

        Dim DateN1 As String = dateconvert(CDate("01/01/" & (Annee + 1)))

        Dim DateDuJour As String = dateconvert(Now.ToShortDateString())

        'On met à 0 les montants des reports
        query = "UPDATE report_cl SET DEBIT_LE='0', CREDIT_LE='0' WHERE DATE_LE='" & DateN1 & "' AND code_cl NOT LIKE 'RES%'"
        ExecuteNonQuery(query)
        query = "UPDATE report_cl0 SET DEBIT_LE='0', CREDIT_LE='0' WHERE DATE_LE='" & DateN1 & "'"
        ExecuteNonQuery(query)
        query = "UPDATE report_sc SET DEBIT_LE='0', CREDIT_LE='0' WHERE DATE_LE='" & DateN1 & "'"
        ExecuteNonQuery(query)
        query = "UPDATE report_cpt SET DEBIT_LE='0', CREDIT_LE='0' WHERE DATE_LE='" & DateN1 & "'"
        ExecuteNonQuery(query)

        query = "select  SUM(l.DEBIT_LE), SUM(l.CREDIT_LE), s.CODE_CL FROM T_COMP_LIGNE_ECRITURE l, T_COMP_SOUS_CLASSE s where s.CODE_SC=l.CODE_SC AND l.DATE_LE <= '" & DateFin & "' and (code_cl like '1%' or code_cl like '2%' or code_cl like '3%' or code_cl like '4%' or code_cl like '5%') GROUP BY s.CODE_CL"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            query = "update report_cl set debit_le='" & rw(0).ToString & "', credit_le='" & rw(1).ToString & "' where code_cl='" & rw(2).ToString & "' and DATE_LE='" & DateN1 & "'"
            ExecuteNonQuery(query)
        Next

        query = "select SUM(l.DEBIT_LE), SUM(l.CREDIT_LE), c1.CODE_CL0 FROM T_COMP_LIGNE_ECRITURE l, T_COMP_SOUS_CLASSE s, T_COMP_CLASSE c, T_COMP_CLASSE0 c1 where s.CODE_SC=l.CODE_SC AND s.CODE_CL=c.CODE_CL AND c.CODE_CL0=c1.CODE_CL0 AND l.DATE_LE <= '" & DateFin & "' and (c1.CODE_CL0 = '1' or c1.CODE_CL0 ='2' or c1.CODE_CL0 ='3' or c1.CODE_CL0 = '4' or c1.CODE_CL0 = '5') GROUP BY c1.CODE_CL0"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            query = "update report_cl0 set debit_le='" & rw(0).ToString & "', credit_le='" & rw(1).ToString & "' where code_cl0='" & rw(2).ToString & "' and DATE_LE='" & DateN1 & "'"
            ExecuteNonQuery(query)
        Next

        query = "select SUM(DEBIT_LE), SUM(CREDIT_LE), CODE_SC FROM T_COMP_LIGNE_ECRITURE where DATE_LE <= '" & DateFin & "' and (code_sc like '1%' or code_sc like '2%' or code_sc like '3%' or code_sc like '4%' or code_sc like '5%') GROUP BY CODE_SC"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            query = "update report_sc set debit_le='" & rw(0).ToString & "', credit_le='" & rw(1).ToString & "' where code_sc='" & rw(2).ToString & "' and DATE_LE='" & DateN1 & "'"
            ExecuteNonQuery(query)
        Next

        query = "select SUM(DEBIT_LE), SUM(CREDIT_LE), CODE_CPT FROM T_COMP_LIGNE_ECRITURE where DATE_LE <= '" & DateFin & "' and (code_sc like '1%' or code_sc like '2%' or code_sc like '3%' or code_sc like '4%' or code_sc like '5%') GROUP BY CODE_CPT"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            query = "update report_cpt SET debit_le='" & rw(0).ToString & "', credit_le='" & rw(1).ToString & "' where CODE_CPT='" & rw("CODE_CPT") & "' and DATE_LE='" & DateN1 & "'"
            ExecuteNonQuery(query)
        Next

        'Il faut ajouter des lignes à montant null dans la table t_comp_ligne_ecriture pour que les comptes reportés sans aucun mouvement à l'exercice sortent sur l'état.
        query = "select CODE_SC from report_sc WHERE (debit_le<>0 OR credit_le<>0) AND date_le='" & DateN1 & "' AND (code_sc NOT IN(SELECT CODE_SC FROM T_COMP_LIGNE_ECRITURE WHERE YEAR(DATE_LE)='" & (Annee + 1) & "'))"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            query = "insert into T_COMP_LIGNE_ECRITURE values (NULL,'','" & rw("CODE_SC").ToString & "','', '','" & DateN1 & "','0','0','','','" & ProjetEnCours & "','non','non','" & CodeUtilisateur & "','" & DateDuJour & "', '" & DateDuJour & "','0','non','')"
            ExecuteNonQuery(query)
        Next

        query = "select code_cpt from report_cpt WHERE (debit_le<>0 OR credit_le<>0) AND date_le='" & DateN1 & "' AND (code_cpt NOT IN(SELECT code_cpt FROM T_COMP_LIGNE_ECRITURE WHERE YEAR(DATE_LE)='" & (Annee + 1) & "'))"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            query = "insert into T_COMP_LIGNE_ECRITURE values (NULL,'','','', '','" & DateN1 & "','0','0','','" & rw("code_cpt").ToString & "','" & ProjetEnCours & "','non','non','" & CodeUtilisateur & "','" & DateDuJour & "', '" & DateDuJour & "','0','non','')"
            ExecuteNonQuery(query)
        Next
    End Sub
    Public Shared Sub UpdateResultat(dated As Date, datef As Date)
        Dim reportCompteresultat As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table

        Dim Chemin As String = lineEtat & "\Comptabilite\CompteResultat\"

        Dim DatSet = New DataSet
        reportCompteresultat.Load(Chemin & "Compte_resultat_corps.rpt")
        reportCompteresultat.SetDataSource(DatSet)

        With crConnectionInfo
            .ServerName = ODBCNAME
            .DatabaseName = DB
            .UserID = USERNAME
            .Password = PWD
        End With

        CrTables = reportCompteresultat.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        query = "DELETE FROM tampon2 WHERE CodeProjet='" & ProjetEnCours & "' AND CodeUtils='SYSTEME'"
        ExecuteNonQuery(query)

        'On va recuperer les elements du Compte de résultat
        query = "SELECT * FROM `t_comp_rubrique` WHERE ETAT_RUB='Compte de résultat'"
        Dim dtRubrique As DataTable = ExcecuteSelectQuery(query)
        For Each rwRubrique As DataRow In dtRubrique.Rows
            Dim TotalMontantRubriqueN As Decimal = 0
            Dim TotalMontantRubriqueN_1 As Decimal = 0

            'On va recuperer les comptes comptables liés aux rubriques
            query = "SELECT * FROM t_comp_type_rubrique WHERE ETAT_RUB='Compte de résultat' AND CODE_RUB='" & rwRubrique("CODE_RUB") & "'"
            Dim dtParams As DataTable = ExcecuteSelectQuery(query)
            For Each rwParams As DataRow In dtParams.Rows

                'Recueration de l'exercice N-1
                Dim datedebutExercice As Date = dated
                Dim datedebutExerciceN_1 As Date = dated.AddYears(-1)
                Dim Existe As Integer = -1
                Dim DateDebutN_1 As Date = Nothing
                Dim DateFinN_1 As Date = Nothing
                query = "SELECT * FROM t_comp_exercice WHERE datedebut='" & dateconvert(datedebutExerciceN_1) & "'"
                Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
                If dtVerif.Rows.Count > 0 Then
                    Existe = 1
                    DateDebutN_1 = CDate(dtVerif.Rows(0)("datedebut")).Date
                    DateFinN_1 = CDate(dtVerif.Rows(0)("datefin")).Date
                End If

                'On va recuperer toutes les saisies passees sur chaque compte paramettre sur la periode
                query = "SELECT SUM(DEBIT_LE) As TDebit, SUM(CREDIT_LE) As TCredit, `CODE_SC` FROM `t_comp_ligne_ecriture` WHERE `CODE_SC` IN( SELECT DISTINCT(CODE_SC) as CODE_SC FROM `t_comp_ligne_ecriture` WHERE `DATE_LE`<='" & dateconvert(datef) & "' AND `DATE_LE`>='" & dateconvert(dated) & "' AND CODE_SC LIKE '" & rwParams("CODE_SC") & "%') AND (DATE_LE<='" & dateconvert(datef) & "' AND DATE_LE>='" & dateconvert(dated) & "') GROUP by `CODE_SC`;"
                Dim dtEcrituresN As DataTable = ExcecuteSelectQuery(query)
                For Each rwEcrituresN In dtEcrituresN.Rows
                    Dim TDebitN As Decimal = 0
                    Dim TCreditN As Decimal = 0
                    Dim SoldeN As Decimal = 0
                    'On verifie si le retour du moteur de bd n'est pas des valeur null
                    If Not IsDBNull(rwEcrituresN("TDebit")) Then
                        TDebitN = CDec(rwEcrituresN("TDebit"))
                    End If
                    If Not IsDBNull(rwEcrituresN("TCredit")) Then
                        TCreditN = CDec(rwEcrituresN("TCredit"))
                    End If

                    SoldeN = TDebitN - TCreditN

                    If TDebitN <> 0 Or TCreditN <> 0 Then
                        'On a des ecritures passees
                        If rwParams("condition") = "Débiteur" Then
                            If SoldeN > 0 Then
                                TotalMontantRubriqueN += Abs(SoldeN)
                            End If
                        ElseIf rwParams("condition") = "Créditeur" Then
                            If SoldeN < 0 Then
                                TotalMontantRubriqueN += Abs(SoldeN)
                            End If
                        ElseIf rwParams("condition") = "Les Deux" Then
                            TotalMontantRubriqueN += Abs(SoldeN)
                        End If
                    End If
                Next

                'On va recuperer toutes les saisies passees sur chaque compte paramettre pour l'exercice N-1
                Dim dtEcrituresN_1 As DataTable = Nothing
                If Existe <> -1 Then
                    query = "SELECT SUM(DEBIT_LE) As TDebit, SUM(CREDIT_LE) As TCredit, `CODE_SC` FROM `t_comp_ligne_ecriture` WHERE `CODE_SC` IN( SELECT DISTINCT(CODE_SC) as CODE_SC FROM `t_comp_ligne_ecriture` WHERE `DATE_LE`<='" & dateconvert(DateFinN_1) & "' AND `DATE_LE`>='" & dateconvert(DateDebutN_1) & "' AND CODE_SC LIKE '" & rwParams("CODE_SC") & "%') AND (DATE_LE<='" & dateconvert(DateFinN_1) & "' AND DATE_LE>='" & dateconvert(DateDebutN_1) & "') GROUP by `CODE_SC`;"
                    dtEcrituresN_1 = ExcecuteSelectQuery(query)
                    For Each rwEcrituresN_1 In dtEcrituresN_1.Rows
                        Dim TDebitN_1 As Decimal = 0
                        Dim TCreditN_1 As Decimal = 0
                        Dim SoldeN_1 As Decimal = 0

                        'On verifie si le retour du moteur de bd n'est pas des valeur null
                        If Not IsDBNull(rwEcrituresN_1("TDebit")) Then
                            TDebitN_1 = CDec(rwEcrituresN_1("TDebit"))
                        End If
                        If Not IsDBNull(rwEcrituresN_1("TCredit")) Then
                            TCreditN_1 = CDec(rwEcrituresN_1("TCredit"))
                        End If

                        SoldeN_1 = TDebitN_1 - TCreditN_1
                        If TDebitN_1 <> 0 Or TCreditN_1 <> 0 Then
                            'On a des ecritures passees
                            If rwParams("Type") <> "AMORT/DEPREC" Then
                                If rwParams("condition") = "Débiteur" Then
                                    If SoldeN_1 > 0 Then
                                        TotalMontantRubriqueN_1 += Abs(SoldeN_1)
                                    End If
                                ElseIf rwParams("condition") = "Créditeur" Then
                                    If SoldeN_1 < 0 Then
                                        TotalMontantRubriqueN_1 += Abs(SoldeN_1)
                                    End If
                                ElseIf rwParams("condition") = "Les Deux" Then
                                    TotalMontantRubriqueN_1 += Abs(SoldeN_1)
                                End If
                            End If
                        End If
                    Next
                End If
            Next

            If TotalMontantRubriqueN > 0 Or TotalMontantRubriqueN_1 <> 0 Then 'La rubrique a ete alimente sur la periode, donc il faut l'enregistrer

                Dim CodeRub As String = rwRubrique("CODE_RUB")
                Dim TextSigne As TextObject
                Try
                    TextSigne = reportCompteresultat.ReportDefinition.ReportObjects(CodeRub & "Signe")
                    If TextSigne.Text.Length <> 0 Then
                        If TextSigne.Text = "-" Then
                            TotalMontantRubriqueN *= -1
                            TotalMontantRubriqueN_1 *= -1
                        End If
                    End If

                Catch ex As Exception
                End Try
                query = "INSERT INTO tampon2 VALUES(NULL,'" & rwRubrique("CODE_RUB") & "','" & rwRubrique("LIBELLE_RUB") & "','" & TotalMontantRubriqueN & "','" & ProjetEnCours & "','" & TotalMontantRubriqueN_1 & "','','SYSTEME')"
                ExecuteNonQuery(query)
            End If

        Next

        Dim XA As String() = {"TA", "RA", "RD"}
        Dim XB As String() = {"TA", "TB", "TC", "TD"}
        Dim XCPrime As String() = {"TE", "TF", "TG", "TH", "TI", "RA", "RB", "RC", "RD", "RE", "RF", "RG", "RH", "RI", "RJ"}
        Dim XDPrime As String() = {"RK"}
        Dim XEPrime As String() = {"TJ", "RL"}
        Dim XF As String() = {"TK", "TL", "TM", "RM", "RN"}
        Dim XH As String() = {"TN", "TO", "RO", "RP"}
        Dim XIPrime As String() = {"RQ", "RS"}

        Dim XATotal As Decimal() = {0, 0}, XBTotal As Decimal() = {0, 0}, XCPrimeTotal As Decimal() = {0, 0}, XDPrimeTotal As Decimal() = {0, 0}, XEPrimeTotal As Decimal() = {0, 0}, XFTotal As Decimal() = {0, 0}, XHTotal As Decimal() = {0, 0}, XIPrimeTotal As Decimal() = {0, 0}
        Dim XCTotal As Decimal() = {0, 0}, XDTotal As Decimal() = {0, 0}, XETotal As Decimal() = {0, 0}, XGTotal As Decimal() = {0, 0}, XITotal As Decimal() = {0, 0}

        query = "SELECT * FROM tampon2 WHERE CodeProjet='" & ProjetEnCours & "' AND CodeUtils='SYSTEME'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            If CDec(rw("MONTANT_RUB")) <> 0 Then

                If Array.IndexOf(XA, rw("CODE_RUB")) <> -1 Then
                    XATotal(0) += CDec(rw("MONTANT_RUB"))
                End If
                If Array.IndexOf(XB, rw("CODE_RUB")) <> -1 Then
                    XBTotal(0) += CDec(rw("MONTANT_RUB"))
                End If
                If Array.IndexOf(XCPrime, rw("CODE_RUB")) <> -1 Then
                    XCPrimeTotal(0) += CDec(rw("MONTANT_RUB"))
                End If
                If Array.IndexOf(XDPrime, rw("CODE_RUB")) <> -1 Then
                    XDPrimeTotal(0) += CDec(rw("MONTANT_RUB"))
                End If
                If Array.IndexOf(XEPrime, rw("CODE_RUB")) <> -1 Then
                    XEPrimeTotal(0) += CDec(rw("MONTANT_RUB"))
                End If
                If Array.IndexOf(XF, rw("CODE_RUB")) <> -1 Then
                    XFTotal(0) += CDec(rw("MONTANT_RUB"))
                End If
                If Array.IndexOf(XH, rw("CODE_RUB")) <> -1 Then
                    XHTotal(0) += CDec(rw("MONTANT_RUB"))
                End If
                If Array.IndexOf(XIPrime, rw("CODE_RUB")) <> -1 Then
                    XIPrimeTotal(0) += CDec(rw("MONTANT_RUB"))
                End If

            End If

            If CDec(rw("MONTANT_RUB1")) <> 0 Then

                If Array.IndexOf(XA, rw("CODE_RUB")) <> -1 Then
                    XATotal(1) += CDec(rw("MONTANT_RUB1"))
                End If
                If Array.IndexOf(XB, rw("CODE_RUB")) <> -1 Then
                    XBTotal(1) += CDec(rw("MONTANT_RUB1"))
                End If
                If Array.IndexOf(XCPrime, rw("CODE_RUB")) <> -1 Then
                    XCPrimeTotal(1) += CDec(rw("MONTANT_RUB1"))
                End If
                If Array.IndexOf(XDPrime, rw("CODE_RUB")) <> -1 Then
                    XDPrimeTotal(1) += CDec(rw("MONTANT_RUB1"))
                End If
                If Array.IndexOf(XEPrime, rw("CODE_RUB")) <> -1 Then
                    XEPrimeTotal(1) += CDec(rw("MONTANT_RUB1"))
                End If
                If Array.IndexOf(XF, rw("CODE_RUB")) <> -1 Then
                    XFTotal(1) += CDec(rw("MONTANT_RUB1"))
                End If
                If Array.IndexOf(XH, rw("CODE_RUB")) <> -1 Then
                    XHTotal(1) += CDec(rw("MONTANT_RUB1"))
                End If
                If Array.IndexOf(XIPrime, rw("CODE_RUB")) <> -1 Then
                    XIPrimeTotal(1) += CDec(rw("MONTANT_RUB1"))
                End If

            End If

        Next

        'Mise a jour des sous-totaux et entete de l'actif
        For i = 0 To 1
            XCTotal(i) += (XCPrimeTotal(i) + XBTotal(i))
            XDTotal(i) += (XDPrimeTotal(i) + XCTotal(i))
            XETotal(i) += (XEPrimeTotal(i) + XDTotal(i))
            XGTotal(i) += (XETotal(i) + XFTotal(i))
            XITotal(i) += (XIPrimeTotal(i) + XGTotal(i) + XHTotal(i))
        Next

        'Mise à jour du résultat sur les comptes comptables
        If XITotal(0) <> 0 Then
            query = "SELECT * FROM report_cl WHERE code_cl='Resultat' AND YEAR(DATE_LE)='" & dated.Year & "'"
            Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
            If dtVerif.Rows.Count = 0 Then
                query = "INSERT INTO report_cl VALUES(NULL,'Resultat','" & XITotal(0) & "',0,'" & dated.Year & "-01-01')"
                ExecuteNonQuery(query)
            Else
                query = "UPDATE report_cl SET debit_le='" & XITotal(0) & "' WHERE code_cl='Resultat' AND DATE_LE='" & dated.Year & "-01-01'"
                ExecuteNonQuery(query)
            End If
        End If

        If XITotal(1) <> 0 Then
            query = "SELECT * FROM report_cl WHERE code_cl='Resultat' AND YEAR(DATE_LE)='" & (dated.Year - 1) & "'"
            Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
            If dtVerif.Rows.Count = 0 Then
                query = "INSERT INTO report_cl VALUES(NULL,'Resultat','" & XITotal(1) & "',0,'" & (dated.Year - 1) & "-01-01')"
                ExecuteNonQuery(query)
            Else
                query = "UPDATE report_cl SET debit_le='" & XITotal(1) & "' WHERE code_cl='Resultat' AND DATE_LE='" & (dated.Year - 1) & "-01-01'"
                ExecuteNonQuery(query)
            End If
        End If

        query = "DELETE FROM tampon2 WHERE CodeProjet='" & ProjetEnCours & "' AND CodeUtils='SYSTEME'"
        ExecuteNonQuery(query)
    End Sub
    Public Shared Function GetResultat(ByVal Annee As Integer) As Decimal
        Dim Resultat As Decimal = 0
        Try
            Dim DateDebut As String = dateconvert(CDate("01/01/" & Annee))
            Dim DateFin As String = dateconvert(CDate("31/12/" & Annee))

            Dim SoldeCharges As Decimal = 0
            Dim SoldeProduits As Decimal = 0
            'Les charges
            query = "select SUM(DEBIT_LE) as TDebit, SUM(CREDIT_LE) as TCredit, CODE_SC FROM T_COMP_LIGNE_ECRITURE where DATE_LE <= '" & DateFin & "' AND DATE_LE >= '" & DateDebut & "' AND CODE_PROJET='" & ProjetEnCours & "' GROUP BY CODE_SC"
            Dim dtCharges As DataTable = ExcecuteSelectQuery(query)
            For Each rwCharges As DataRow In dtCharges.Rows
                If IsCompteDeChargeResultat(rwCharges("CODE_SC")) Then
                    If Not IsDBNull(rwCharges("TDebit")) Then
                        SoldeCharges += CDec(rwCharges("TDebit"))
                    End If
                    If Not IsDBNull(rwCharges("TCredit")) Then
                        SoldeCharges -= CDec(rwCharges("TCredit"))
                    End If
                End If
            Next

            'Les produits
            query = "select SUM(DEBIT_LE) as TDebit, SUM(CREDIT_LE) as TCredit, CODE_SC FROM T_COMP_LIGNE_ECRITURE where DATE_LE <= '" & DateFin & "' AND DATE_LE >= '" & DateDebut & "' AND CODE_PROJET='" & ProjetEnCours & "' GROUP BY CODE_SC"
            Dim dtProduits As DataTable = ExcecuteSelectQuery(query)
            For Each rwProduits As DataRow In dtProduits.Rows
                If IsCompteDeProduitResultat(rwProduits("CODE_SC")) Then
                    If Not IsDBNull(rwProduits("TDebit")) Then
                        SoldeProduits += CDec(rwProduits("TDebit"))
                    End If
                    If Not IsDBNull(rwProduits("TCredit")) Then
                        SoldeProduits -= CDec(rwProduits("TCredit"))
                    End If
                End If
            Next
            Resultat = (SoldeCharges + SoldeProduits) * -1
        Catch ex As Exception
            Return Resultat
        End Try
        Return Resultat
    End Function
End Class
