Imports System.IO
Imports System.Net.Mail
Imports System.Security.Cryptography
Imports System.Text

Public Class PassationMarche
    Public Shared Function GetMethode(CodeProcAO As String) As String
        query = "SELECT AbregeAO FROM t_procao WHERE CodeProcAO='" & CodeProcAO & "'"
        Return ExecuteScallar(query)
    End Function

    Public Function GetMethode1(CodeProcAO As String) As String
        query = "SELECT AbregeAO FROM t_procao WHERE CodeProcAO='" & CodeProcAO & "'"
        Return ExecuteScallar(query)
    End Function

    Public Shared Function VerifEtapePlan(RefMarche As String) As Boolean
        query = "SELECT COUNT(*) FROM t_planmarche WHERE RefMarche='" & RefMarche & "' AND DebutPrevu IS NOT NULL"
        Dim verif As String = Val(ExecuteScallar(query))
        If verif > 0 Then
            Return True
        End If
        Return False
    End Function

    Public Shared Function VerifierTraiterMethode(CodeMethod As String) As Boolean
        Try
            Dim ListeMethode As New List(Of String) From {"SFQC", "SCBD", "SMC", "3CV", "SFQ", "SQC", "SD", "ED"}
            For i = 0 To ListeMethode.Count - 1
                If ListeMethode(i) = CodeMethod.ToString.ToUpper Then
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Function

#Region "AMI ET DP"

    Public Shared Function GetInitialbailleur(ByVal CodeConvention As String) As String
        query = "SELECT B.InitialeBailleur FROM t_bailleur as B, t_convention as C WHERE B.CodeBailleur=C.CodeBailleur AND C.CodeConvention='" & CodeConvention.ToString & "' and B.CodeProjet='" & ProjetEnCours & "'"
        Return ExecuteScallar(query)
    End Function

    Public Shared Function NewVerifierMontMarche(ByVal RefMarche As String) As Decimal
        Dim MontantMarcheConsome As Decimal = 0
        Try
            'Marche utiliser pour elaborer un ami
            MontantMarcheConsome = Val(ExecuteScallar("SELECT SUM(MontantMarche) FROM t_ami where RefMarche='" & RefMarche & "' and CodeProjet='" & ProjetEnCours & "'"))

            'Tous les marches elaborer a partir de la DP, qui ne proviennent pas d'un AMI. et qui ne sont pas engagé c'est-à-dire en cours d'execution
            MontantMarcheConsome += Val(ExecuteScallar("SELECT SUM(MontantMarche) FROM T_DP where RefMarche='" & RefMarche & "' and NumeroAMI='' and CodeProjet='" & ProjetEnCours & "' and Statut='En cours' and NumeroDp NOT IN (SELECT NumeroDAO FROM t_marchesigne WHERE CodeProjet='" & ProjetEnCours & "' and TypeMarche='Consultants')"))

            'Sum des montant des marches engagés
            MontantMarcheConsome += Val(ExecuteScallar("SELECT SUM(MontantHT) FROM t_marchesigne where RefMarche='" & RefMarche & "' and TypeMarche='Consultants' and EtatMarche<>'Annuler'"))

            Return MontantMarcheConsome
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Function

    Public Shared Function ChargerLesDonneEmail_AMI_DP_SERVICEAUTRES(ByVal NumeroDossier As String, ByVal TypeRequette As String, Optional ControlsProcessus As Boolean = True) As Boolean

        Try
            If TypeRequette = "AMI" Then
                rwDossDPAMISA = ExcecuteSelectQuery("SELECT m.RevuePrioPost, m.Convention_ChefFile, a.CodeConvention, b.InitialeBailleur, b.TitreTTL, b.NomTTL, b.PrenomTTL, b.MailTTL from t_marche as m, t_ami as a, t_bailleur as b, t_convention as c where a.RefMarche=m.RefMarche and m.Convention_ChefFile=c.CodeConvention and c.CodeBailleur=b.CodeBailleur and a.NumeroDAMI='" & EnleverApost(NumeroDossier.ToString) & "' and a.CodeProjet='" & ProjetEnCours & "'")
            ElseIf TypeRequette = "DP" Then
                rwDossDPAMISA = ExcecuteSelectQuery("SELECT m.RevuePrioPost, m.Convention_ChefFile, d.CodeConvention, b.InitialeBailleur, b.TitreTTL, b.NomTTL, b.PrenomTTL, b.MailTTL from t_marche as m, t_dp as d, t_bailleur as b, t_convention as c where d.RefMarche=m.RefMarche and m.Convention_ChefFile=c.CodeConvention and c.CodeBailleur=b.CodeBailleur and d.NumeroDp='" & EnleverApost(NumeroDossier.ToString) & "' and d.CodeProjet='" & ProjetEnCours & "'")
            End If

            EmailResponsablePM = ExecuteScallar("SELECT EMP_EMAIL from t_grh_employe where PROJ_ID='" & ProjetEnCours & "' and ResponsablePM='1'")
            EmailCoordinateurProjet = ExecuteScallar("SELECT EMP_EMAIL from t_grh_employe where PROJ_ID='" & ProjetEnCours & "' and Emp_Cordonnateur='1'")

            If rwDossDPAMISA.Rows.Count = 0 Then
                FailMsg("Nous n'avons pas pu recupéré toutes les informations du bailleur")
                Return False
            End If

            If rwDossDPAMISA.Rows(0)("RevuePrioPost").ToString = "" Then
                FailMsg("Veuillez definir la revu")
                Return False
            End If

            If ControlsProcessus = True Then
                If rwDossDPAMISA.Rows(0)("RevuePrioPost").ToString = "Postériori" Then
                    SuccesMsg("Le dossier étant à posteriori le bailleur de fond intervient à la fin du processus")
                    Return False
                End If
            End If

            If rwDossDPAMISA.Rows(0)("MailTTL").ToString = "" Then
                SuccesMsg("L'email du bailleur de fond est vide")
                Return False
            End If

            'email responsable Passation de marche
            If EmailResponsablePM.ToString = "" Then
                FailMsg("L'email de reponse [responsable de la passation de marché] est vide")
                Return False
            End If

            'email coordinateur
            If EmailCoordinateurProjet.ToString = "" Then
                FailMsg("L'email de reponse [coordinateur] est vide")
                Return False
            End If

            NomBailleurRetenu = MettreApost(rwDossDPAMISA.Rows(0)("TitreTTL").ToString) & " " & MettreApost(rwDossDPAMISA.Rows(0)("NomTTL").ToString) & " " & MettreApost(rwDossDPAMISA.Rows(0)("PrenomTTL").ToString)
            EmailDestinatauer = MettreApost(rwDossDPAMISA.Rows(0)("MailTTL").ToString)

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
        Return True
    End Function
#End Region

    Public Shared Function GetEtapeInfo(RefEtape As String) As DataRow
        Dim dt As New DataTable
        dt.Columns.Add("Libelle", Type.GetType("System.String"))
        dt.Columns.Add("NumeroOrdre", Type.GetType("System.String"))
        dt.Columns.Add("Delai", Type.GetType("System.String"))
        dt.Columns.Add("CodeProcAO", Type.GetType("System.String"))
        Dim Libelle As String = String.Empty
        Dim NumeroOrdre As String = String.Empty
        Dim DelaiEtape As String = String.Empty
        Dim CodeProcAO As String = String.Empty
        query = "select * from t_etapemarche where RefEtape='" & RefEtape & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        If dt0.Rows.Count > 0 Then
            Libelle = dt0.Rows(0).Item("TitreEtape")
            NumeroOrdre = dt0.Rows(0).Item("NumeroOrdre")
            DelaiEtape = dt0.Rows(0).Item("DelaiEtape")
            CodeProcAO = dt0.Rows(0).Item("CodeProcAO")
        End If

        dt.Rows.Add({Libelle, NumeroOrdre, DelaiEtape, CodeProcAO})
        Return dt.Rows(0)
    End Function

#Region "DAO"
    Public Shared Function GetSousLot(NumLot As Integer, NumDossier As String) As Object()
        query = "SELECT COUNT(*) FROM t_lotdao_souslot WHERE NumeroDAO='" & NumDossier & "' AND RefLot='" & GetRefLot(NumLot, NumDossier) & "'"
        Dim NbreSousLot As Integer = Val(ExecuteScallar(query))
        query = "SELECT * FROM t_lotdao_souslot WHERE NumeroDAO='" & NumDossier & "' AND RefLot='" & GetRefLot(NumLot, NumDossier) & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        Return {NbreSousLot, dt}
    End Function
    Public Shared Function GetRefLot(NumLot As Integer, NumDossier As String) As Integer
        query = "SELECT RefLot FROM t_lotdao WHERE NumeroDAO='" & NumDossier & "' AND CodeLot='" & NumLot & "'"
        Return Val(ExecuteScallar(query))
    End Function
#End Region
    Public Shared Function GetKeyFromPassword(ByVal password As String, ByVal salt As Byte()) As Byte()
        Dim derivator As Rfc2898DeriveBytes = New Rfc2898DeriveBytes(password, salt, 100)
        Return derivator.GetBytes(32)
    End Function
    Public Shared Function EncryptWithAes(ByVal plainContent As Byte(), ByVal key As Byte()) As Byte()
        If plainContent Is Nothing OrElse plainContent.Length = 0 Then
            Throw New ArgumentNullException("plainText")
        End If

        If key Is Nothing OrElse key.Length = 0 Then
            Throw New ArgumentNullException("key")
        End If

        Dim encrypted As Byte()

        Using aes As Aes = Aes.Create()

            Using sha256 As SHA256 = SHA256.Create()
                Dim encryptor As ICryptoTransform
                Dim signature As Byte() = sha256.ComputeHash(plainContent)
                aes.GenerateIV()
                aes.Mode = CipherMode.CBC
                aes.Key = key

                If aes.IV Is Nothing OrElse aes.IV.Length <> 16 Then
                    Throw New Exception("Invalid initialization vector")
                End If

                encryptor = aes.CreateEncryptor(aes.Key, aes.IV)

                Using memoryStream As MemoryStream = New MemoryStream()
                    memoryStream.Write(aes.IV, 0, aes.IV.Length)

                    Using cryptoStream As CryptoStream = New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
                        cryptoStream.WriteByte(1)
                        cryptoStream.Write(signature, 0, signature.Length)
                        cryptoStream.Write(plainContent, 0, plainContent.Length)
                    End Using

                    encrypted = memoryStream.ToArray()
                End Using
            End Using
        End Using

        Return encrypted
    End Function
    Public Shared Function GenererToken(ByVal NumDossier As String, ByVal ID_COJO As String, ByVal TypeDos As String, Optional DB As String = "") As String
        Dim strTest As String = DB & ":" & ProjetEnCours & ":" & NumDossier & ":" & ID_COJO & ":" & TypeDos
        Dim token As String
        Dim salt As String
        Dim tokenExiste = True
        Do
            Dim provider As RNGCryptoServiceProvider = New RNGCryptoServiceProvider()
            Dim byteArray = New Byte(7) {}
            provider.GetBytes(byteArray)
            salt = BitConverter.ToString(byteArray).Replace("-", "")
            Dim password = "5TGB&YHN7UJM(IK<5TGB&YHN7UJM(IK<"
            Dim key = GetKeyFromPassword(password, byteArray)
            Dim texteachiffre = Encoding.UTF8.GetBytes(strTest)
            Dim txtcrypt = EncryptWithAes(texteachiffre, key)
            token = Convert.ToBase64String(txtcrypt)
            tokenExiste = VerifieToken(token)
        Loop While tokenExiste = True
        Return token & ":" & salt
    End Function
    Private Shared Function VerifieToken(ByVal token As String) As Boolean
        query = "SELECT COUNT(*) FROM t_commission WHERE AuthKey='" & token & "'"
        Dim verif As Integer = Val(ExecuteScallar(query))
        If verif > 0 Then
            Return True
        End If
        Return False
    End Function

    Public Shared Sub envoieMail(ByVal nomPrenom As String, ByVal EmailDestinateur As String, ByVal AuthKey As String)
        Dim email As String = EmailDestinateur
        Dim nom As String = nomPrenom
        Dim ID() = AuthKey.Split(":")
        Dim token = ID(0).ToString
        Dim salt = ID(1).ToString
        Dim mail As MailMessage = New MailMessage()

        Dim MailExp As String = "support@clearproject.online"
        Dim ModpassExp As String = "D9akt36*"

        Dim content As String = "Bonjour " & nomPrenom.ToString & "<br><br> Vous êtes invité (es) à intégrer une commission d'ouverture sur ClearProject. <br><br> Votre clé d'authenfication est le suivant: <br> <b>" & AuthKey & "</b>"
        Dim MailDesti As String = EmailDestinateur.ToString

        Dim objets As String = "Invitation ClearProject"

        'ENVOI D'EMAIL A L'EXTERIEUR
        Try
            ' variable de message d'email' 
            Dim Message As New System.Net.Mail.MailMessage
            Message.IsBodyHtml = True
            Message.SubjectEncoding = System.Text.Encoding.UTF8
            'objet du message'
            Message.Subject = objets

            'email de la personne envoyant le message'
            Message.From = New Net.Mail.MailAddress(MailExp)

            'le corps du message'
            Message.Body = content & "<br><br> Veuillez <a href='http://localhost/clearProject/index.php?ID1=" & salt & "&amp;ID2=" & token & "'title='Cliquer pour accepter l'invitation'> <b> Cliquer ici pour l'accepter </b> </a> <br><br> Merci, <br> <b>Le service passation des marchés</b> <br><br> "

            ' email du destinataire'
            With Message.To
                .Add(New Net.Mail.MailAddress(MailDesti))
            End With

            'l'adresse mail et le port du serveur'
            Dim Smtp As New System.Net.Mail.SmtpClient("webmail.clearproject.online", 25)

            Smtp.EnableSsl = False

            'email et mot de passe de celui qui envoi le message'
            Smtp.Credentials = New Net.NetworkCredential(MailExp, ModpassExp)

            'l'envoie du message'
            Smtp.Send(Message)
            Message.Dispose()
        Catch ex As Exception
            FailMsg("Echec d'envoie du client de connection.")
            Exit Sub
        End Try
    End Sub

    Public Shared Sub envoieMail2(ByVal nomEntreprise As String, ByVal NumeroDao As String, ByVal EmailDestinateur As String, Optional reçu As String = "")
        Dim email As String = EmailDestinateur
        Dim nom As String = nomEntreprise
        Dim mail As MailMessage = New MailMessage()

        Dim MailExp As String = "support@clearproject.online"
        Dim ModpassExp As String = "D9akt36*"

        Dim content As String = "Bonjour " & nomEntreprise.ToString & ", <br><br> Veuillez recevoir en fichier joint le reçu du dossier d'appel d'offre N° " & NumeroDao & ". <br><b>"
        Dim MailDesti As String = EmailDestinateur.ToString

        Dim objets As String = "Reçu de rétrait de dossier d'appel d'offre"
        Dim PieceJointe As New Attachment(reçu)

        'ENVOI D'EMAIL A L'EXTERIEUR
        Try
            ' variable de message d'email' 
            Dim Message As New System.Net.Mail.MailMessage
            Message.IsBodyHtml = True
            Message.SubjectEncoding = System.Text.Encoding.UTF8
            'objet du message'
            Message.Subject = objets

            'email de la personne envoyant le message'
            Message.From = New Net.Mail.MailAddress(MailExp)

            'le corps du message'
            Message.Body = content & "<br><b>Le service passation des marchés</b><br><br> "

            'joindre le reçu'
            Message.Attachments.Add(PieceJointe)
            ' email du destinataire'
            With Message.To
                .Add(New Net.Mail.MailAddress(MailDesti))
            End With

            'l'adresse mail et le port du serveur'
            Dim Smtp As New System.Net.Mail.SmtpClient("webmail.clearproject.online", 25)

            Smtp.EnableSsl = False

            'email et mot de passe de celui qui envoi le message'
            Smtp.Credentials = New Net.NetworkCredential(MailExp, ModpassExp)

            'l'envoie du message'
            Smtp.Send(Message)
            Message.Dispose()
        Catch ex As Exception
            FailMsg("Echec d'envoie du client de connection.")
            Exit Sub
        End Try
    End Sub


    Public Shared Sub EnvoiMailRapport(ByVal nomEntreprise As String, ByVal NumeroAMI_DP As String, ByVal EmailDestinateur As String, ByVal CheminDoc As String, ByVal EmailCoordinateur As String, ByVal EmailResponsablePM As String, ByVal TypeDoc As String)
        Dim mail As MailMessage = New MailMessage()
        Dim MailExp As String = "support@clearproject.online"
        Dim ModpassExp As String = "D9akt36*"
        Dim content As String = ""

        'TypeDoc permet de personnaliser le message
        If TypeDoc = "RapportEvalTechAMI" Then
            content = "Bonjour " & nomEntreprise.ToString & ", <br><br> Veuillez recevoir en fichier joint le rapport d'évaluation technique de l'avis à manifestation d'intérêt N° " & NumeroAMI_DP & ".  <br><br>  Pour toutes modifications veuillez télécharger le fichier joint. <br><br> Après avoir appliqué vos modifications, veuillez envoyer le fichier aux adresses suivantes: <b>" & EmailCoordinateur.ToString & "</b>, <b>" & EmailResponsablePM.ToString & "</b>"
        ElseIf TypeDoc = "ConsultantsDP" Then
            content = "Bonjour " & nomEntreprise.ToString & ", <br><br> Veuillez recevoir en fichier joint le dossier de la demande de proposition N° " & NumeroAMI_DP & ". <br><br> Pour toutes modifications veuillez télécharger le fichier joint."
        ElseIf TypeDoc = "RapportEvalTechDP" Then
            content = "Bonjour " & nomEntreprise.ToString & ", <br><br> Veuillez recevoir en fichier joint le rapport d'évaluation technique de la demande de proposition N° " & NumeroAMI_DP & ". <br><br> Pour toutes modifications veuillez télécharger le fichier joint. <br><br> Après avoir appliqué vos modifications, veuillez envoyer le fichier aux adresses suivantes: <b>" & EmailCoordinateur.ToString & "</b>, <b>" & EmailResponsablePM.ToString & "</b>"
        ElseIf TypeDoc = "DossierDP" Then
            content = "Bonjour " & nomEntreprise.ToString & ", <br><br> Veuillez recevoir en fichier joint le dossier de la demande de proposition N° " & NumeroAMI_DP & ". <br><br> Pour toutes modifications veuillez télécharger le fichier joint. <br><br> Après avoir appliqué vos modifications, veuillez envoyer le fichier aux adresses suivantes: <b>" & EmailCoordinateur.ToString & "</b>, <b>" & EmailResponsablePM.ToString & "</b>"
        End If

        Dim MailDesti As String = EmailDestinateur.ToString

        Dim objets As String = ""
        If TypeDoc = "DossierDP" Or TypeDoc = "ConsultantsDP" Then
            objets = "Demande de proposition"
        Else
            objets = "Rapport d'évaluation technique"
        End If

        Dim PieceJointe As New System.Net.Mail.Attachment(CheminDoc)

        'ENVOI D'EMAIL A L'EXTERIEUR
        Try
            ' variable de message d'email' 
            Dim Message As New System.Net.Mail.MailMessage
            Message.IsBodyHtml = True
            Message.SubjectEncoding = System.Text.Encoding.UTF8
            'objet du message'
            Message.Subject = objets

            'email de la personne envoyant le message'
            Message.From = New Net.Mail.MailAddress(MailExp)

            'le corps du message'
            Message.Body = content & "<br><b>Le service passation des marchés</b><br><br> "

            'joindre le reçu'
            Message.Attachments.Add(PieceJointe)

            ' email du destinataire'
            With Message.To
                .Add(New Net.Mail.MailAddress(MailDesti))
            End With

            'l'adresse mail et le port du serveur'
            Dim Smtp As New System.Net.Mail.SmtpClient("webmail.clearproject.online", 25)

            Smtp.EnableSsl = False

            'email et mot de passe de celui qui envoi le message'
            Smtp.Credentials = New Net.NetworkCredential(MailExp, ModpassExp)

            'l'envoie du message'
            Smtp.Send(Message)
            Message.Dispose()
        Catch ex As Exception
            FailMsg("Echec d'envoie de l'email.")
        End Try
    End Sub
End Class

Public Class DaoSpecTechLot
    Private _LibLot As String
    Private _RefLot As String
    Private _DataTableValue As DataTable
    Private _AreSousLot As Boolean
    Private _SousLot As New List(Of DaoSpecTechSousLot)

    Public Sub New()
        _LibLot = String.Empty
        _RefLot = String.Empty
        _AreSousLot = False
    End Sub

    Public Property CodeLot() As String
        Get
            Return _LibLot
        End Get
        Set(value As String)
            _LibLot = value
        End Set
    End Property
    Public Property RefLot() As String
        Get
            Return _RefLot
        End Get
        Set(value As String)
            _RefLot = value
        End Set
    End Property
    Public Property DataTable() As DataTable
        Get
            Return _DataTableValue
        End Get
        Set(value As DataTable)
            _DataTableValue = value
        End Set
    End Property
    Public Property AreSousLot() As Boolean
        Get
            Return _AreSousLot
        End Get
        Set(value As Boolean)
            _AreSousLot = value
        End Set
    End Property
    Public Sub AddSousLot(Value As DaoSpecTechSousLot)
        _SousLot.Add(Value)
    End Sub
    Public ReadOnly Property GetSousLot() As List(Of DaoSpecTechSousLot)
        Get
            Return _SousLot
        End Get
    End Property
End Class
Public Class DaoSpecTechSousLot
    Private _LibSousLot As String
    Private _RefSousLot As String
    Private _DataTableValue As DataTable

    Public Sub New()
        _LibSousLot = String.Empty
        _RefSousLot = String.Empty
    End Sub

    Public Property CodeSousLot() As String
        Get
            Return _LibSousLot
        End Get
        Set(value As String)
            _LibSousLot = value
        End Set
    End Property
    Public Property RefSousLot() As String
        Get
            Return _RefSousLot
        End Get
        Set(value As String)
            _RefSousLot = value
        End Set
    End Property
    Public Property DataTable() As DataTable
        Get
            Return _DataTableValue
        End Get
        Set(value As DataTable)
            _DataTableValue = value
        End Set
    End Property


End Class