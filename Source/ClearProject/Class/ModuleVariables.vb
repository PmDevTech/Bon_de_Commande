Imports System.Data
Imports System.IO
Imports System
Imports Microsoft
Imports MySql.Data.MySqlClient
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.Math
Imports System.Text.RegularExpressions
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen
Imports AxSms
Imports AxEmail

Module ModuleVariables
    Public SessionID As String = String.Empty
    Public ForcerAction As Boolean = False
    Public ChangementOk As Boolean = False
    Public FenetreFlotte As Boolean = False
    Public AjoutResMatEnCours As Boolean = False
    Public AjoutPersResEnCours As Boolean = False
    Public ModifProprieteGridActif As Boolean = False
    Public photoVeh As String
    Public majVeh As Boolean = False
    Public majContAss As Boolean = False
    Public majPaVeh As Boolean = False
    Public majAttVeh As Boolean = False
    Public majParam As Boolean = False
    Public majFrsPrest As Boolean = False
    Public majRepVeh As Boolean = False
    Public majEntVeh As Boolean = False
    Public majVisiteVeh As Boolean = False
    Public majVteVeh As Boolean = False
    Public majStockCarb As Boolean = False
    Public majAttCarb As Boolean = False
    Public majCarnetB As Boolean = False
    Public AjoutPartition As Boolean = False
    Public majFamillestock As Boolean = False
    Public majMagasin As Boolean = False
    Public majDPD As Boolean = False

    Public parametre As String = String.Empty, dateRest As String = String.Empty, dateTraitementPa As String = String.Empty, idPanne As String = String.Empty, idReparation As String = String.Empty, idEntretien As String = String.Empty, idVisite As String = String.Empty, facureRep As String = String.Empty, facureEnt As String = String.Empty, AssuranceVeh As String = String.Empty, FicheDC As String = String.Empty, VehCarteG As String = String.Empty
    Public _comments As String
    Public Jour As String
    Public _firstName As String
    Public _secondName As String
    Public CodeActiviteAjout As String = ""
    Public CodeMereActivite As String = ""
    Public CodeActiviteModif As String = ""
    Public FrequenceDialogResult As String = ""
    Public PubMontEch As String = ""
    Public MonnaieEvalOffre As String = ""
    Public Proj_smsTerminal As String = ""
    Public Proj_smsCodePin As String = ""
    Public CODENATURE As String = ""
    Public LibelleBesoin As String = ""
    Public NumCompte As String = ""
    Public ExceptRevue2 As String = ""
    Public Pu As String = ""
    Public CategorieDep As String = ""
    Public LibPartition As String = ""
    Public DelaiEtap As String = ""
    Public ExceptRevue As String = ""
    Public Speciftech As String = ""
    Public AccessWall As String = ""
    Public ReponseDialog As String = ""
    Public TypeRessource As String = ""
    Public DureeEtpPlan As String = ""
    Public TitreEtpPlan As String = ""
    Public DateEtpPlan As String = ""
    Public ExerciceComptable As DataTable


    Public BailleurEnCours(10) As Decimal
    Public CodeNatureBesoin(50) As Decimal
    Public TpsInfo As Decimal = 0
    Public NbTotActivite As Decimal = 0
    Public NbBailleurActivite As Decimal = 0
    Public PubCodeEch As Decimal = 0
    Public LignePropGridAModifier As Decimal = -1
    Public LigneMaterielsGridAModifier As Decimal = -1
    Public LigneEnCours As Decimal = -1
    Public RefBsoinPart As Decimal 'pour prendre la reference dans la table T_BesoinPartition 
    Public CODEPARTITION As Decimal
    Public MNTBESOIN As Double
    Public CurrEmpId As Decimal = -1
    

    Public TotalPU As Decimal = 0
    Public TotalPT As Decimal = 0
    Public TotalBailleur() As Decimal = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    Public GrandTotalGAP As Decimal = 0


    Public ecran As Screen = Screen.PrimaryScreen
    Public DateDuJour As Date = My.Computer.Clock.LocalTime.ToString
    'Public sqlconn As New MySqlConnection()
    'Public DatAdapt As MySqlDataAdapter
    'Public DatTable As DataTable
    'Public DatRow As DataRow
    'Public CmdBuilder As MySqlCommandBuilder
    'Public DatSet As DataSet


    Public OkDate As Boolean = False
    Public NomUtilisateur As String = String.Empty, PrenUtilisateur As String = String.Empty, ProjetEnCours As String = String.Empty, FonctionUtilisateur As String = String.Empty, CodeUtilisateur As String = String.Empty, NiveauAcces1 As String = String.Empty, DateHeureEnCours As String = String.Empty, DateEnCours As String = String.Empty
    Public CodeOperateurEnCours As Decimal = 0
    Public TpsInactif As Decimal = 0

    
    Public RespoEtape As Decimal = 0
    Public NbreColoEtape As Decimal = 0
    Public boolimmo As Boolean = False

    'Public dr, dr1, dr2, dr3, dr4, dr5, dr6, dr7, dr8, dr9, dr10, dr11, dr12, dr13, dr14, dr15, dr16, dr17, dr18, dr19, dr20 As MySqlDataReader
    Public query As String = String.Empty, p1 As String = String.Empty, dater As String = String.Empty, etat As String = String.Empty
    Public imageimmo As String = String.Empty
    Public selection As Decimal = 0, duree As Decimal = 0, res As Decimal = 0, mois As Decimal = 0, annee As Decimal = 0, annee1 As Decimal = 0, amort As Decimal = 0, amort1 As Decimal = 0, amort2 As Decimal = 0, amort3 As Decimal = 0, amort4 As Decimal = 0, amort5 As Decimal = 0, valeur As Decimal = 0, valeur1 As Decimal = 0, valeur2 As Decimal = 0, valeur3 As Decimal = 0, valeur4 As Decimal = 0, valeur5 As Decimal = 0, valeur6 As Decimal = 0, valeur7 As Decimal
    Public result As TimeSpan
    Public dc As DataColumn
    Public dt As DataTable
    Public sd As MySqlDataAdapter
    Public rownum, rownum2 As Decimal
    Public dts As New DataSet

    '*************** Mail & Sms **********************
    Public Proj_mailHote As String = ""
    Public Proj_mailCompte As String = ""
    Public Proj_mailPasse As String = ""
    Public Proj_mailAuthent As Boolean = False
    Public Proj_mailSecur As Boolean = False
    Public Proj_mailPort As String = ""

    Public Proj_smsVitesse As Decimal = 0
    
    Public Proj_smsEncodage As Decimal = 0
    Public Proj_smsModele As String = ""

    Public dtimmo = New DataTable()
    Public dtfam = New DataTable()
    Public dtloc = New DataTable()
    Public dtcb = New DataTable()
    Public dtechloc = New DataTable()
    Public dtechcb = New DataTable()
    Public dtCompteTier = New DataTable()
    Public dtListeRespoPPM = New DataTable()
    Public dtMutation = New DataTable()
    Public dtService = New DataTable()
    Public dtcombj = New DataTable()
    Public dtop = New DataTable()
    Public dtveh = New DataTable()
    Public dtcarb = New DataTable()
    Public dtass = New DataTable()
    Public dtcarnet = New DataTable()
    Public dtcongeprev = New DataTable()
    Public dtconge = New DataTable()
    Public dtdrf = New DataTable()
    Public dtdrd = New DataTable()
    Public dtfamm = New DataTable()
    Public dtarticle = New DataTable()
    Public dtrep = New DataTable()
    Public dtvalid = New DataTable()
    Public dtent = New DataTable()
    Public dtvisit = New DataTable()
    Public dtvente = New DataTable()
    Public dtmr = New DataTable()
    Public dtrapact = New DataTable()
    Public dtcateg = New DataTable()
    Public dtSousCategorie = New DataTable()
    Public dtGroupe = New DataTable()
    Public dtEngagement = New DataTable()
    Public dtAutre = New DataTable()
    Public dtcomptable = New DataTable()

    Public TabTrue(50000) As Boolean
    Public nbTab As Decimal = 0

    Public objMail As AxEmail.Message = New AxEmail.Message()
    Public objSmtpServer As AxEmail.Smtp = New AxEmail.Smtp()
    Public objConstants As AxEmail.Constants = New AxEmail.Constants()
    '************************************************

    Public DatAdapt1 As MySqlDataAdapter
    Public DatTable1 As DataTable
    Public DatSet1, DatSet2 As DataSet

    'Public Reader10 As MySqlDataReader
    Public DatAdapt10 As MySqlDataAdapter
    Public DatTable10 As DataTable
    Public DatSet10 As DataSet

    Public DatAdapt100 As MySqlDataAdapter
    Public DatTable100 As DataTable
    Public DatSet100 As DataSet

    Public PaieMsg As New AxEmail.Message
    Public PaieSMTP As New AxEmail.Smtp


    Public PanierRefPrix(20) As String
    Public PrixChoixPanier(20) As String
    Public LibellePanier(20) As String
    Public nbPanier As Decimal = 0

    '*********************************************************************************************
    Public DescPartition As String = String.Empty, DureePartition As String = String.Empty, JustifPartition As String = String.Empty, RespoPartition As String = String.Empty, ResAttendu As String = String.Empty, ResObtenu As String = String.Empty, IndicPerformance As String = String.Empty, Elaborateur As String = String.Empty
    Public DateDebutPartition As String = String.Empty, DateFinPartition As String = String.Empty, Reference As String = String.Empty, MoyenDeVerif As String = String.Empty
    Public CodeComp, CodeSComp, CodeDernierBailleur As Decimal
    Public Activite As String = String.Empty, S_Comp As String = String.Empty, Comp As String = String.Empty, NomOperateur As String = String.Empty, IdentifiantProjetEnCours As String = String.Empty, NomProjetEnCours As String = String.Empty, NomComposante As String = String.Empty, AnneeDesFiches As String = String.Empty
    Public ListBailleur, ListAnnée As New ArrayList()
    Public EditionFicheActivite, EditionFicheBudgParActivité, EditionFicheBudgParComp, EditionFicheSuiviBudg, EditionchainelogiquePFCTCAL As Boolean
    Public EditionPAActivité, EditionChronoPTA, EditionFinancementPTA, EditionRapportMissionSuiv, EditionFicheSuiviActiv As Boolean
    Public EditionRapportTrimestriel, EditionRapportAnnuel, EditionCalendExecut, EditionBudgPComp, EditionBudgPSComp, EditionPrévBudg As Boolean
    Public BudgetTotal As Decimal
    Public DateDébutRapport As String = String.Empty, DateFinRapport As String = String.Empty, CheminLogo As String = String.Empty
    '**********************************************************************************************
    '::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    'Pour passation de marché
    Public NbreDEtapePrPlan As Decimal
    Public TypeDeMarchePrPlan As String = String.Empty, BailleurMarchePrPlan As String = String.Empty
    '::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    'Pour Comptabilité
    Public DateDeDebut As String = String.Empty, DateDeFin As String = String.Empty, TypeDEtats As String = String.Empty, Tiers As String = String.Empty, TypeDEtatsAnal As String = String.Empty, CompteInf As String = String.Empty, CompteSup As String = String.Empty
    Public BalStand, Bal6, Bal8 As Boolean
    '**********************************************************************************************
    '::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    'Pour Gestion Financière
    Public DateRapproche As Date
    Public TiersRapproche As String
    '**********************************************************************************************

    Public DatAdapt11 As MySqlDataReader
    Public DatTable11 As DataTable
    Public DatSet11 As DataSet
    Public SplasFerme As Boolean = True
    Public TextChargmt As String = ""
    Public SkinActu As String = "Style DevExpress"

    Public maPhoto As String = String.Empty, monCV As String = String.Empty, monDiplome As String = String.Empty, maLettre As String = String.Empty, monContrat As String = String.Empty, myData As String = String.Empty, sex As String = String.Empty, monAvenant As String = String.Empty, monRapport As String = String.Empty
    Public chaine As String = String.Empty, line As String = String.Empty, lineEtat As String = String.Empty, LigneServer As String = String.Empty, cur_empID As String = String.Empty, RapTypID As String = String.Empty, RapType As String = String.Empty, curMisID As String = String.Empty, LaDevise As String = String.Empty
    Public searchBySpe, searchByServ, searchByStat As Boolean
    Public cur_User As String = String.Empty
    Public cur_form As Form
    Public tr_spec, tr_emp As Boolean
    Public dtListEmploye = New DataTable()
    Public dtSalaire = New DataTable()
    Public dtContrat = New DataTable()
    Public dtAvenant = New DataTable()
    Public dtCategorie = New DataTable()
    Public dtjournaux = New DataTable()
    Public drx, drx1 As DataRow

    Public facture As String
    Public id_table = "", id_table2 = "", id_serv = "", id_zone = "", id_tctp = "", codepart = "", utilisateur, type_journal, cpt_classe As String

    'Variable DP et pour l'envoi des rapport et dossier aux bailleur et au consultant
    Public rwDossDPAMISA As DataTable = New DataTable()
    Public EmailResponsablePM As String = String.Empty
    Public EmailCoordinateurProjet As String = String.Empty
    Public NomBailleurRetenu As String = String.Empty
    Public EmailDestinatauer As String = String.Empty
End Module
